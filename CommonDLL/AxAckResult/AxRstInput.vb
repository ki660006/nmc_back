Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports LISAPP.APP_BT
Imports CDHELP.FGCDHELPFN

Public Class AxRstInput
    'test위해 삽입

    Private moForm As Windows.Forms.Form
    Private _a_dr As Object

    Public Event ChangedBcNo(ByVal BcNo As String)
    Public Event ChangedTestCd(ByVal BcNo As String, ByVal TestCd As String)
    Public Event FunctionKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event Call_SpRst(ByVal BcNo As String, ByVal TestCd As String)


    Private msFormID As String = ""
    Private msRegNo As String = ""
    Private msDateS As String = ""
    Private msDateE As String = ""
    Private msPatNm As String = ""
    Private msSexAge As String = ""
    Private msAboRh As String = ""

    Private msDeptCd As String = ""
    Private msPartSlip As String = ""
    Private msCaseGbn As String = ""

    Private msBcNo As String = ""
    Private msFnDt As String = ""

    Private msTestCds As String = ""
    Private msWkGrpCd As String = ""
    Private msEqCd As String = ""
    Private msRstFlg As String = ""
    Private test As String

    Private mbBatchMode As Boolean = False
    Private mbDoctorMode As Boolean = False
    Private mbBloodBank As Boolean = False
    Private msBlood_ABO_C As String = ""
    Private msBlood_ABO_S As String = ""
    Private msBlood_Rh As String = ""
    Private msRegNoCmt As String = ""
    Private msXpertTcd As Boolean = False
    Private msXpertC As Boolean = False

    Private mbQueryView As Boolean = False

    Private msWbcCount As Boolean = False

    Private mbColHiddenYn As Boolean
    Private mbCodeEscKey As Boolean = False
    Private sBuf3() As String

    Private m_al_Slip_bcno As New ArrayList
    Private m_dt_Cmt_bcno As DataTable
    Private m_dt_RstUsr As DataTable
    Private m_dt_RstCdHelp As DataTable
    Private m_dt_Alert_Rule As DataTable

    Private mbLostFocusGbn As Boolean = True
    Private mbLeveCellGbn As Boolean = True
    Private m_dbl_RowHeightt As Double = 0

    Public WriteOnly Property FORMID() As String
        Set(ByVal Value As String)
            msFormID = Value
        End Set
    End Property

    Public Property ColHiddenYn() As Boolean
        Get
            ColHiddenYn = mbColHiddenYn
        End Get
        Set(ByVal value As Boolean)
            mbColHiddenYn = value

            Dim iCol As Integer
            With spdResult
                If mbColHiddenYn Then
                    For iCol = 1 To .MaxCols
                        If iCol = .GetColFromID("bcno") Then
                            If Not mbDoctorMode Then
                                .Col = iCol : .ColHidden = True
                            End If
                        ElseIf iCol = .GetColFromID("chk") Or iCol = .GetColFromID("tnmd") Or iCol = .GetColFromID("orgrst") Or iCol = .GetColFromID("viewrst") Or _
                               iCol = .GetColFromID("rerunflg") Or iCol = .GetColFromID("history") Or iCol = .GetColFromID("reftxt") Or iCol = .GetColFromID("rstunit") Or _
                               iCol = .GetColFromID("hlmark") Or iCol = .GetColFromID("panicmark") Or iCol = .GetColFromID("deltamark") Or _
                               iCol = .GetColFromID("criticalmark") Or iCol = .GetColFromID("alertmark") Or iCol = .GetColFromID("rstflgmark") Or _
                               iCol = .GetColFromID("rstcmt") Or iCol = .GetColFromID("bfviewrst2") Or iCol = .GetColFromID("bffndt2") Or iCol = .GetColFromID("eqnm") Or _
                               iCol = .GetColFromID("testcd") Or iCol = .GetColFromID("spccd") Or iCol = .GetColFromID("tordcd") Or _
                               iCol = .GetColFromID("reftcls") Or iCol = .GetColFromID("eqflag") Or iCol = .GetColFromID("rerunrst") Or _
                               iCol = .GetColFromID("slipcd") Or iCol = .GetColFromID("rrptst") Then
                            '20210419 jhs rrptst 추가
                        Else
                            .Col = iCol : .ColHidden = True
                        End If
                    Next

                    Me.btnDebug_cmt.Visible = False
                Else
                    For iCol = 1 To .MaxCols
                        .Col = iCol : .ColHidden = False
                    Next
                    Me.btnDebug_cmt.Visible = True
                End If
            End With
        End Set
    End Property

    Public WriteOnly Property AboRh() As String
        Set(ByVal value As String)
            msAboRh = value
        End Set
    End Property

    Public WriteOnly Property Form() As Windows.Forms.Form
        Set(ByVal value As Windows.Forms.Form)
            moForm = value
        End Set
    End Property

    Public WriteOnly Property RegNo() As String
        Set(ByVal value As String)
            msRegNo = value
        End Set
    End Property

    Public WriteOnly Property PatName() As String
        Set(ByVal value As String)
            msPatNm = value
        End Set
    End Property

    Public WriteOnly Property SexAge() As String
        Set(ByVal value As String)
            msSexAge = value
        End Set
    End Property

    Public WriteOnly Property DeptCd() As String
        Set(ByVal value As String)
            msDeptCd = value
        End Set
    End Property

    Public WriteOnly Property FnDt() As String
        Set(ByVal value As String)
            msFnDt = value
        End Set
    End Property

    Public WriteOnly Property TestCds() As String
        Set(ByVal value As String)
            msTestCds = value
        End Set
    End Property

    Public WriteOnly Property TgrpCds() As String
        Set(ByVal value As String)
            msTestCds = fnGet_tgrp_testspc(value)
        End Set
    End Property

    Public WriteOnly Property WKgrpCd() As String
        Set(ByVal value As String)
            msWkGrpCd = value
        End Set
    End Property

    Public WriteOnly Property EqCd() As String
        Set(ByVal value As String)
            msEqCd = value
        End Set
    End Property

    Public WriteOnly Property RstFlg() As String
        Set(ByVal value As String)
            msRstFlg = value
        End Set
    End Property

    Public WriteOnly Property BatchMode() As Boolean
        Set(ByVal value As Boolean)
            mbBatchMode = value
        End Set
    End Property

    Public ReadOnly Property BCNO() As String
        Get
            If msBcNo = "" Then
                BCNO = ""
            Else
                BCNO = IIf(txtBcNo.Text = "", msBcNo, txtBcNo.Text).ToString
            End If
        End Get
    End Property

    Public ReadOnly Property ABO_RST_C() As String
        Get
            ABO_RST_C = lblABO.Text
        End Get
    End Property

    Public ReadOnly Property ABO_RST_O() As String
        Get
            ABO_RST_O = lblABO_bf.Text
        End Get
    End Property

    Public WriteOnly Property SlipCd() As String
        Set(ByVal value As String)
            msPartSlip = value
        End Set
    End Property

    Public WriteOnly Property CaseGbnCd() As String
        Set(ByVal value As String)
            msCaseGbn = value
        End Set
    End Property

    Public Property BcNoAll() As Boolean
        Get
            Return Me.chkBcnoAll.Checked
        End Get

        Set(ByVal value As Boolean)
            Me.chkBcnoAll.Checked = value
        End Set
    End Property

    Public Property UseDoctor() As Boolean
        Get
            Return mbDoctorMode
        End Get
        Set(ByVal value As Boolean)
            mbDoctorMode = value

            If mbDoctorMode Then
                With Me.spdResult
                    .Col = .GetColFromID("bcno")
                    .ColHidden = False
                    .set_ColWidth(.GetColFromID("bcno"), 15)
                End With

                Me.cboBcNos.Visible = True
                Me.cboSlip.Left = Me.cboBcNos.Left + Me.cboBcNos.Width + 1
                Me.cboSlip.Width = Me.txtCmtCont.Width - Me.cboBcNos.Left - Me.cboBcNos.Width - 1
            Else
                With spdResult
                    .Col = .GetColFromID("bcno")
                    .ColHidden = True
                End With

                Me.cboBcNos.Visible = False

            End If
        End Set
    End Property

    Public Property UseBloodBank() As Boolean
        Get
            Return mbBloodBank
        End Get
        Set(ByVal value As Boolean)
            mbBloodBank = value
            If mbBloodBank Then
                Me.lblCABOLabel.Text = "현 혈액"
                Me.lblABO.Size = lblABO_bf.Size

                Me.lblOABOLabel.Visible = True
                Me.lblABO_bf.Visible = True

                Me.gbxABO.Visible = True
                Me.gbxComment.Width = Me.Width - Me.grpRstInfo.Width - Me.gbxABO.Width

                Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_BBTType_List()

                If dt.Rows.Count > 0 Then
                    For intIdx As Integer = 0 To dt.Rows.Count - 1
                        Select Case dt.Rows(intIdx).Item("bbgbn").ToString
                            Case "1" : msBlood_ABO_C = dt.Rows(intIdx).Item("testcd").ToString
                            Case "3" : msBlood_ABO_S = dt.Rows(intIdx).Item("testcd").ToString
                            Case "2" : msBlood_Rh = dt.Rows(intIdx).Item("testcd").ToString
                        End Select
                    Next
                End If
            Else
                'lblCABOLabel.Text = "혈액형"
                'lblABO.Width = 164
                'lblABO.Height = 72

                'lblOABOLabel.Visible = False
                'lblABO_bf.Visible = False

                Me.gbxABO.Visible = False
                Me.gbxComment.Width = Me.Width - Me.grpRstInfo.Width
            End If
        End Set
    End Property

    Private Property a_dr(ByVal ix As Integer) As Object
        Get
            Return _a_dr
        End Get
        Set(ByVal value As Object)
            _a_dr = value
        End Set
    End Property

    Private Sub sbSet_Cmt_BcNo_Add(ByVal r_ci As CMT_INFO)
        Dim sFn As String = "sbSet_Cmt_BcNo_Add"

        Try
            With m_dt_Cmt_bcno
                'Row 추가
                Dim dr As DataRow = .NewRow()

                Dim a_fieldinfo() As System.Reflection.FieldInfo = r_ci.GetType().GetFields()

                For j As Integer = 1 To a_fieldinfo.Length
                    Dim sFieldName As String = a_fieldinfo(j - 1).Name.ToLower
                    Dim sFieldValue As String = a_fieldinfo(j - 1).GetValue(r_ci).ToString()

                    If Not sFieldValue = "" Then
                        dr.Item(sFieldName) = sFieldValue
                    End If
                Next

                'status
                dr.Item("status") = "I"

                .Rows.Add(dr)
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbSet_Cmt_BcNo_Edit(ByVal r_ci As CMT_INFO)
        Dim sFn As String = "sbSet_Cmt_BcNo_Edit"

        Try
            With m_dt_Cmt_bcno
                Dim iRow As Integer = -1

                For ix As Integer = 0 To .Rows.Count - 1
                    If .Rows(ix).Item("bcno").ToString = r_ci.BcNo And .Rows(ix).Item("partslip").ToString = r_ci.PartSlip Then
                        iRow = ix
                        Exit For
                    End If
                Next

                If iRow < 0 Then
                    sbSet_Cmt_BcNo_Add(r_ci)
                Else
                    Dim a_fieldinfo() As System.Reflection.FieldInfo = r_ci.GetType().GetFields()
                    Dim sStatus As String = "S"

                    For ix As Integer = 0 To a_fieldinfo.Length - 1
                        Dim sFieldName As String = a_fieldinfo(ix).Name.ToLower
                        Dim sFieldValue As String = a_fieldinfo(ix).GetValue(r_ci).ToString()

                        '수정된 부분이 있는 지 조사하고 있으면 변경
                        If Not .Rows(iRow).Item(sFieldName).ToString() = sFieldValue Then
                            .Rows(iRow).Item(sFieldName) = sFieldValue
                            sStatus = "U"
                        End If
                    Next

                    'status
                    If .Rows(iRow).Item("status").ToString() = "S" Then
                        .Rows(iRow).Item("status") = sStatus
                    End If

                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Cmt_One_slipcd(ByVal rsBcNo As String, ByVal rsSlipCd As String)
        Dim sFn As String = "sbDisplay_Cmt_One_slipcd"

        Try
            Me.txtCmtCont.Text = ""

            Dim a_dr As DataRow()
            Dim a_dt As DataTable = New DataTable

            If rsSlipCd = "" Then
                a_dr = m_dt_Cmt_bcno.Select("bcno = '" + rsBcNo + "'", "partslip")
            Else
                a_dr = m_dt_Cmt_bcno.Select("bcno = '" + rsBcNo + "' AND partslip = '" + rsSlipCd + "'")
            End If

            If rsSlipCd = "" Then
                For ix As Integer = 0 To a_dr.Length - 1
                    Me.txtCmtCont.Text += "[" + a_dr(ix).Item("slipnmd").ToString.Trim + "]" + vbCrLf
                    Me.txtCmtCont.Text += a_dr(ix).Item("cmtcont").ToString + vbCrLf
                Next
            Else
                If a_dr.Length > 0 Then
                    Me.txtCmtCont.Text = a_dr(0).Item("cmtcont").ToString
                End If
            End If

            If rsSlipCd = "" Then
                Me.txtCmtCont.ReadOnly = True
            Else
                Me.txtCmtCont.ReadOnly = False
            End If

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_slip(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_slip"

        Try
            Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_SlipInfo_bcno(rsBcNo)

            Me.cboSlip.Items.Clear()
            Me.cboSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                If m_al_Slip_bcno.Contains(dt.Rows(ix).Item("slipcd").ToString) Then
                    Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
                    If dt.Rows(ix).Item("slipcd").ToString = msPartSlip Then Me.cboSlip.SelectedIndex = cboSlip.Items.Count - 1
                End If
            Next

            If msPartSlip = "" Then Me.cboSlip.SelectedIndex = 0

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Function fnGet_tgrp_testspc(ByVal rsTgrpCds As String) As String
        Dim sFn As String = "fnGet_tgrp_testinfo"

        Try

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_Test_List(rsTgrpCds)
            If dt.Rows.Count < 1 Then Return ""

            Dim sTestCds As String = ""

            For ix As Integer = 0 To dt.Rows.Count - 1
                If ix > 0 Then sTestCds += ","
                sTestCds += dt.Rows(ix).Item("testcd").ToString.Replace(" ", "")
            Next

            Return sTestCds

        Catch ex As Exception
            sbLog_Exception(ex.Message)
            Return ""
        End Try

    End Function

    Private Sub sbGet_Alert_Rule()
        Dim sFn As String = "sbGet_Alert_Rule"

        Try

            m_dt_Alert_Rule = LISAPP.APP_R.RstFn.fnGet_Alert_Rule()

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_KeyPad(ByVal rsFormGbn As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsTnmd As String)
        Dim sFn As String = "Sub sbDisplay_KeyPad(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdOrdListR.RightClick"
        Try
            If rsFormGbn = "" Then Return

            Dim sWBCRst As String = ""
            Dim sBfViewRsts As String = ""
            Dim sPartslip As String = ""
            Dim sBcNo As String = ""

            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntCtlXY As Point = Fn.CtrlLocationXY(spdResult)
            Dim al_RstInfo As New ArrayList

            With spdResult
                Dim sWbcTestCd = LISAPP.COMM.RstFn.fnGet_ManualDiff_WBC_TestCd(rsTestCd, rsSpcCd)

                If sWbcTestCd <> "" Then
                    sWBCRst = LISAPP.COMM.RstFn.fnGet_ManualDiff_WBC_Rst(msBcNo, sWbcTestCd)

                    If sWBCRst = "" Then
                        For iRow As Integer = 1 To .MaxRows
                            If Ctrl.Get_Code(Me.spdResult, "testcd", iRow) = sWbcTestCd Then
                                .Row = iRow
                                .Col = .GetColFromID("orgrst") : sWBCRst = .Text
                                Exit For
                            End If
                        Next
                    End If
                End If

                For iRow As Integer = 1 To .MaxRows
                    Dim sTmp As String = Ctrl.Get_Code(Me.spdResult, "testcd", iRow)
                    If sTmp <> "" Then sTmp = sTmp.Substring(0, 5)

                    If sTmp = rsTestCd Then

                        Dim sTestCd As String = "", sBfView As String = "", sBfFnDt2 As String = ""

                        .Row = iRow
                        .Col = .GetColFromID("testcd") : sTestCd = .Text
                        .Col = .GetColFromID("bfviewrst2") : sBfView = .Text
                        .Col = .GetColFromID("bfbcno1") : sBfFnDt2 = .Text

                        If sBfView <> "" Then
                            sBfViewRsts += sTestCd + "^" + sBfView + "^" + sBfFnDt2 + "|"
                        End If
                    End If
                Next

                Dim sDiffCmt As String = ""

                Select Case rsFormGbn
                    Case "0"
                        sDiffCmt = (New FGDIFF01).Display_Result(moForm, pntFrmXY.X + Me.Width, pntFrmXY.Y + pntCtlXY.Y + spdResult.Height, rsTestCd, rsSpcCd, msRegNo, msPatNm, msSexAge, sWBCRst, sBfViewRsts, al_RstInfo)
                    Case "1"
                        sDiffCmt = (New FGDIFF02).Display_Result(moForm, pntFrmXY.X + Me.Width, pntFrmXY.Y + pntCtlXY.Y + spdResult.Height, rsTestCd, rsSpcCd, msRegNo, msPatNm, msSexAge, sWBCRst, sBfViewRsts, al_RstInfo)
                    Case Else
                        Return
                End Select

                Dim iPos As Integer = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, rsTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                If iPos > 0 Then
                    .Row = iPos
                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("slipcd") : sPartslip = .Text
                    .Col = .GetColFromID("bcno") : sBcNo = .Text
                End If

                For iRow As Integer = 1 To al_RstInfo.Count
                    iPos = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                    If iPos > 0 Then
                        .Row = iPos

                        If sPartslip <> "" Then
                            .Col = .GetColFromID("slipcd") : sPartslip = .Text
                        End If

                        .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
                        .Col = .GetColFromID("orgrst") : Dim sOrgRst As String = .Text

                        If sTnmd.ToLower.IndexOf("neu#(anc)") >= 0 And IsNumeric(sOrgRst) And IsNumeric(CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst) Then
                            If Val(sOrgRst) <= 0.5 And Val(CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst) >= 0.5 Then
                                MsgBox("검사[" + sTnmd + "] 값을 확인하세요.!")
                            End If
                        End If

                        If sOrgRst <> "" And CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst = "" Then
                            CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst = "0"
                        End If

                        .Col = .GetColFromID("orgrst") : .Text = CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst
                        .Col = .GetColFromID("viewrst")

                        If CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgViewRst <> "" Then
                            .Text = CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgViewRst
                        Else
                            .Text = CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst
                        End If

                        If CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst <> "" Then
                            .Row = iPos
                            If .RowHidden Then .RowHidden = False
                        End If

                        sbSet_ResultView(iPos)
                        sbGet_Calc_Rst(iPos) '-- 결과 계산

                    End If
                Next

                If sDiffCmt <> "" Then
                    If sPartslip <> "" Then
                        .Row = 1
                        .Col = .GetColFromID("slipcd") : sPartslip = .Text
                    End If

                    Dim ci As New CMT_INFO
                    With ci
                        .BcNo = sBcNo
                        .PartSlip = sPartslip
                        '.CmtCont = "[" + rsTnmd + "] " + sDiffCmt '<<<20180612 diffcount 소견 변경 
                        .CmtCont = sDiffCmt
                    End With

                    sbSet_Cmt_BcNo_Edit(ci)

                    If Ctrl.Get_Code(Me.cboSlip) <> sPartslip Then
                        For ix As Integer = 0 To Me.cboSlip.Items.Count - 1
                            Me.cboSlip.SelectedIndex = ix
                            If Ctrl.Get_Code(Me.cboSlip) = sPartslip Then Exit For
                        Next
                    End If
                    'If Me.cboSlip.SelectedIndex > 0 Then Me.txtCmtCont.Text += "[" + rsTnmd + "] " + sDiffCmt '<<<20180612 diffcount 소견 변경 
                    If Me.cboSlip.SelectedIndex > 0 Then Me.txtCmtCont.Text += sDiffCmt
                End If

                Return


            End With
        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbGet_Calc_Rst(ByVal riRow As Integer)

        Dim dt As New DataTable

        Try
            For intIx1 As Integer = 1 To spdResult.MaxRows

                If intIx1 = riRow Then Continue For

                Dim sBcNo As String = ""
                Dim sTestCd As String = ""
                Dim sSpcCd As String = ""
                Dim sCalGbn As String = ""

                With spdResult
                    .Row = intIx1
                    .Col = .GetColFromID("bcno") : sBcNo = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                    .Col = .GetColFromID("calcgbn") : sCalGbn = .Text
                    .Col = .GetColFromID("rstflg") : Dim sRstFlg As String = .Text

                    If riRow = 0 And sRstFlg = "3" Then Return

                End With

                If sCalGbn = "1" Then
                    dt = LISAPP.COMM.CalcFn.fnGet_CalcTests(sBcNo, sTestCd, sSpcCd)
                    If dt.Rows.Count < 1 Then Return

                    Dim sCalForm As String = ""
                    Dim iCalCnt As Integer = 0

                    sCalForm = dt.Rows(0).Item("calform").ToString
                    iCalCnt = Convert.ToInt16(dt.Rows(0).Item("paramcnt"))

                    For intIdx As Integer = 0 To iCalCnt - 1
                        Dim sChr As String = Chr(65 + intIdx)
                        Dim sTCd As String = dt.Rows(0).Item("param" + intIdx.ToString).ToString
                        Dim sOrgRst As String = ""

                        For intRow As Integer = 1 To spdResult.MaxRows
                            With spdResult
                                .Row = intRow
                                .Col = .GetColFromID("testcd")
                                If .Text.Trim = sTCd.Substring(0, 7).Trim Then
                                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text.Trim
                                    Exit For
                                End If
                            End With
                        Next

                        If sOrgRst <> "" Then sCalForm = sCalForm.Replace(sChr, sOrgRst)
                    Next

                    If msSexAge.IndexOf("/"c) >= 0 Then
                        sCalForm = sCalForm.Replace("~", IIf(msSexAge.Split("/"c)(0) = "M", "1", "0").ToString).Replace("♂", IIf(msSexAge.Split("/"c)(0) = "M", "1", "0").ToString) '-- 남자
                        sCalForm = sCalForm.Replace("!", IIf(msSexAge.Split("/"c)(0) = "F", "1", "0").ToString).Replace("♀", IIf(msSexAge.Split("/"c)(0) = "F", "1", "0").ToString) '-- 여자
                        sCalForm = sCalForm.Replace("@", msSexAge.Split("/"c)(1))                               '-- 나이
                    End If

                    Try
                        Dim strRst As String = LISAPP.COMM.CalcFn.fnGet_CFCompute(sCalForm)
                        If strRst <> "" Then
                            strRst = fnRstTypeCheck(intIx1, strRst)

                            With spdResult
                                .Row = intIx1
                                .Col = .GetColFromID("orgrst") : .Text = strRst
                                .Col = .GetColFromID("viewrst") : .Text = strRst

                                sbSet_ResultView(intIx1)
                            End With
                        End If
                    Catch ex As Exception

                    End Try

                End If
            Next
        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbGet_Calc_Rst(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal riRow As Integer)

        Dim objDTable As New DataTable

        Try
            objDTable = LISAPP.COMM.CalcFn.fnGet_CalcTests(rsBcNo, rsTestCd, rsSpcCd)
            If objDTable.Rows.Count < 1 Then Return

            Dim sCalForm As String = ""
            Dim iCalCnt As Integer = 0

            sCalForm = objDTable.Rows(0).Item("calform").ToString
            iCalCnt = Convert.ToInt16(objDTable.Rows(0).Item("paramcnt"))

            For ix As Integer = 0 To iCalCnt - 1
                Dim sChr As String = Chr(65 + ix)
                Dim sTCd As String = objDTable.Rows(0).Item("param" + ix.ToString).ToString
                Dim sOrgRst As String = ""

                For iRow As Integer = 1 To spdResult.MaxRows
                    With spdResult
                        .Row = iRow
                        .Col = .GetColFromID("testcd")
                        If .Text = sTCd.Substring(0, 7) Then
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text.Trim
                            Exit For
                        End If
                    End With
                Next

                If sOrgRst <> "" Then sCalForm = sCalForm.Replace(sChr, sOrgRst)
            Next

            Try
                Dim strRst As String = LISAPP.COMM.CalcFn.fnGet_CFCompute(sCalForm)
                If strRst <> "" Then
                    With spdResult
                        .Row = riRow : .Col = .GetColFromID("orgrst") : .Text = strRst
                        sbSet_ResultView(riRow)
                    End With
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try
    End Sub

    Public Sub sbFocus()

        Dim intUnLockRow As Integer = 0

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("orgrst")
                If Not .Lock And .RowHidden = False Then
                    If intUnLockRow = 0 Then intUnLockRow = intRow

                    If .Text = "" Then
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then .ForeColor = Drawing.Color.Black
                        .SetActiveCell(.GetColFromID("orgrst"), intRow)
                        .Focus()

                        .Row = intRow
                        .Col = .GetColFromID("slipcd") : Dim sSlipCd As String = .Text

                        For ix As Integer = 0 To Me.cboSlip.Items.Count - 1
                            Me.cboSlip.SelectedIndex = ix
                            If Ctrl.Get_Code(Me.cboSlip) = sSlipCd Then Exit For
                        Next

                        spdResult_ClickEvent(spdResult, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("orgrst"), intRow))

                        intUnLockRow = 0
                        Exit For
                    End If
                End If
            Next

            If intUnLockRow > 0 Then
                If .MaxRows > 0 Then
                    .SetActiveCell(.GetColFromID("orgrst"), intUnLockRow)
                    .Focus()

                    .Row = intUnLockRow
                    .Col = .GetColFromID("slipcd") : Dim sSlipCd As String = .Text

                    For ix As Integer = 0 To Me.cboSlip.Items.Count - 1
                        Me.cboSlip.SelectedIndex = ix
                        If Ctrl.Get_Code(Me.cboSlip) = sSlipCd Then Exit For
                    Next

                    spdResult_ClickEvent(spdResult, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("orgrst"), intUnLockRow))
                End If
            End If
        End With

    End Sub

    Private Sub sbDisplayCalRst_Info(ByVal r_al As ArrayList)
        Dim sFn As String = "sbDisplayCalRst_Info"

        Try

            If r_al.Count = 0 Then Return

            For i As Integer = 1 To r_al.Count
                Dim sTestcd As String = CType(r_al(i - 1), AxAckCalcResult.CalcRstInfo).TestCd
                Dim sOrgRst As String = CType(r_al(i - 1), AxAckCalcResult.CalcRstInfo).OrgRst

                With Me.spdResult
                    Dim iRow As Integer = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, sTestcd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRow < 1 Then
                        'MsgBox("검사코드 찾기 오류 : " + sTestCd)
                        Continue For
                    End If

                    .SetText(.GetColFromID("orgrst"), iRow, sOrgRst)
                    .SetActiveCell(.GetColFromID("viewrst"), iRow)

                    If sOrgRst <> "" Then
                        .Row = iRow
                        If .RowHidden Then .RowHidden = False
                    End If

                    Me.spdResult_KeyDownEvent(Me.spdResult, New AxFPSpreadADO._DSpreadEvents_KeyDownEvent(13, 0))
                End With
            Next

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Function fnSet_Result_Test(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsOrgRst As String) As RST_INFO

        Dim sBcNo$ = "", sTestCd$ = ""
        Dim objRst As New RST_INFO

        If rsOrgRst = "" Then Return objRst

        With spdResult
            If .MaxRows = 0 Then sbDisplay_Data(rsBcNo, False)

            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                .Col = .GetColFromID("testcd") : sTestCd = .Text

                If rsBcNo = sBcNo And rsTestCd = sTestCd Then

                    .Col = .GetColFromID("orgrst") : .Text = rsOrgRst
                    .Col = .GetColFromID("viewrst") : .Text = rsOrgRst

                    sbSet_ResultView(intRow, True)
                    sbGet_Calc_Rst(intRow)  '-- 결과 계산

                    .Row = intRow
                    .Col = .GetColFromID("cvtfgbn") : Dim strCvtGbn As String = .Text
                    .Col = .GetColFromID("cvtfldgbn") : Dim strCvtFldGbn As String = .Text

                    If strCvtGbn <> "☞" Or strCvtFldGbn = "C" Then sbGet_CvtRstInfo(sBcNo, sTestCd)

                    .Row = intRow
                    .Col = .GetColFromID("iud") : objRst.IUD = .Text
                    .Col = .GetColFromID("rsttype") : objRst.RstType = .Text
                    .Col = .GetColFromID("rstulen") : objRst.RstULen = .Text
                    .Col = .GetColFromID("rstllen") : objRst.RstLLen = .Text
                    .Col = .GetColFromID("cutopt") : objRst.CutOpt = .Text
                    .Col = .GetColFromID("refgbn") : objRst.RefGbn = .Text
                    .Col = .GetColFromID("judgtype") : objRst.JudgType = .Text
                    .Col = .GetColFromID("refls") : objRst.RefLs = .Text
                    .Col = .GetColFromID("refl") : objRst.RefL = .Text
                    .Col = .GetColFromID("refhs") : objRst.RefHs = .Text
                    .Col = .GetColFromID("refh") : objRst.RefH = .Text
                    .Col = .GetColFromID("panicgbn") : objRst.PanicGbn = .Text
                    .Col = .GetColFromID("panicl") : objRst.PanicL = .Text
                    .Col = .GetColFromID("pnaich") : objRst.PanicH = .Text
                    .Col = .GetColFromID("spccd") : objRst.SpcCd = .Text
                    .Col = .GetColFromID("ujudglt1") : objRst.UJudglt1 = .Text
                    .Col = .GetColFromID("ujudglt2") : objRst.UJudglt2 = .Text
                    .Col = .GetColFromID("ujudglt3") : objRst.UJudglt3 = .Text
                    .Col = .GetColFromID("deltagbn") : objRst.DeltaGbn = .Text
                    .Col = .GetColFromID("deltal") : objRst.DeltaL = .Text
                    .Col = .GetColFromID("delth") : objRst.DeltaH = .Text
                    .Col = .GetColFromID("deltaday") : objRst.DeltaDay = .Text
                    .Col = .GetColFromID("criticalgbn") : objRst.CriticalGbn = .Text
                    .Col = .GetColFromID("criticall") : objRst.CriticalL = .Text
                    .Col = .GetColFromID("criticalh") : objRst.CriticalH = .Text
                    .Col = .GetColFromID("aleartgbn") : objRst.AlertGbn = .Text
                    .Col = .GetColFromID("alertl") : objRst.AlertL = .Text
                    .Col = .GetColFromID("alerth") : objRst.AlertH = .Text
                    .Col = .GetColFromID("alimitgbn") : objRst.AlimitGbn = .Text
                    .Col = .GetColFromID("alimitls") : objRst.AlimitLs = .Text
                    .Col = .GetColFromID("alimitl") : objRst.AlimitL = .Text
                    .Col = .GetColFromID("alimith") : objRst.AlimitH = .Text
                    .Col = .GetColFromID("alimiths") : objRst.AlimitHs = ""

                    .Col = .GetColFromID("orgrst") : objRst.OrgRst = .Text
                    .Col = .GetColFromID("viewrst") : objRst.ViewRst = .Text
                    .Col = .GetColFromID("bforgrst1") : objRst.BfOrgRst = .Text
                    .Col = .GetColFromID("bfviewrst1") : objRst.BfViewRst = .Text
                    .Col = .GetColFromID("bffndt1") : objRst.BfFnDt = .Text
                    .Col = .GetColFromID("rstflgmark") : objRst.RstFlg = .Text
                    .Col = .GetColFromID("reftxt") : objRst.RefTxt = .Text

                    .Col = .GetColFromID("regnm") : objRst.RegNm = .Text
                    .Col = .GetColFromID("mwnm") : objRst.MwNm = .Text
                    .Col = .GetColFromID("fnnm") : objRst.FnNm = .Text
                    .Col = .GetColFromID("rstcmt") : objRst.RstCmt = .Text

                    .Col = .GetColFromID("hlmark") : objRst.HLMark = .Text
                    .Col = .GetColFromID("panicmark") : objRst.PanicMark = .Text
                    .Col = .GetColFromID("deltamark") : objRst.DeltaMark = .Text
                    .Col = .GetColFromID("criticalmark") : objRst.CriticalMark = .Text
                    .Col = .GetColFromID("alertmark") : objRst.AlertMark = .Text

                    objRst.SpcNm = LISAPP.COMM.RstFn.fnGet_SpcNmInfo(sBcNo)

                    objRst.ABO_Cur = lblABO.Text
                    objRst.ABO_Old = lblABO_bf.Text

                    .Col = .GetColFromID("tcdgbn") : Dim strTCdgbn As String = .Text

                    If strTCdgbn = "C" Then
                        For intIdx = intRow - 1 To 1 Step -1
                            .Row = intIdx
                            .Col = .GetColFromID("testcd") : Dim strTmp1 As String = .Text
                            .Col = .GetColFromID("tcdgbn") : Dim strTmp2 As String = .Text

                            If strTmp1 = sTestCd.Substring(0, 5) And strTmp2 = "P" And objRst.IUD = "1" Then
                                .Row = intIdx
                                .Col = .GetColFromID("chk") : .Text = "1"
                                Exit For
                            End If
                        Next
                    End If

                    Return objRst
                    Exit For
                End If
            Next
        End With

        Return Nothing

    End Function

    Private Sub sbLog_Exception(ByVal rsMsg As String)
        Me.lstEx.Items.Insert(0, rsMsg)
    End Sub

    Private Sub sbConvertFormat(ByVal riRow As Integer)
        Dim sFn As String = "sbConvertFormat"

        Dim strRst As String = ""
        Dim strViewRst As String = ""
        Dim intLen As Integer

        Try
            With spdResult
                .Row = riRow
                .Col = .GetColFromID("orgrst") : strRst = .Text
                .Col = .GetColFromID("viewrst") : strViewRst = .Text
                If IsNumeric(strRst) Then
                    If strRst.IndexOf(".") > -1 Then Return
                    If strRst.IndexOf("-") > -1 Then Return
                    If strRst.IndexOf("+") > -1 Then Return
                    If strRst.IndexOf("<") > -1 Then Return
                    If strRst.IndexOf(">") > -1 Then Return

                    'If strViewRst.IndexOf(".") > -1 Then Return
                    If strViewRst.IndexOf("-") > -1 Then Return
                    If strViewRst.IndexOf("+") > -1 Then Return
                    If strViewRst.IndexOf("<") > -1 Then Return
                    If strViewRst.IndexOf(">") > -1 Then Return
                    If Not IsNumeric(strViewRst) Then Return

                    intLen = strRst.Replace(".", "").Length
                    Dim dblRst As Double = CDbl(strRst)

                    Select Case intLen
                        Case 3, 4, 5
                            .Col = .GetColFromID("viewrst") : .Text = Format(dblRst, "#,##0")
                        Case 6, 7, 8
                            .Col = .GetColFromID("viewrst") : .Text = Format(dblRst, "#,##0,000")
                        Case 9
                            .Col = .GetColFromID("viewrst") : .Text = Format(dblRst, "#,##0,000,000")
                        Case Else
                            .Col = .GetColFromID("viewrst") : .Text = strRst
                    End Select
                End If

            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString())

        End Try
    End Sub
    Private Sub sbSet_ResultView(ByVal riRow As Integer, Optional ByVal rbTest As Boolean = False)

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst")

            sbRstTypeCheck(riRow)
            sbHLCheck(riRow)
            sbPanicCheck(riRow, m_dt_RstCdHelp)
            sbUJudgCheck(riRow)
            sbDeltaCheck(riRow, m_dt_RstCdHelp)
            sbCriticalCheck(riRow)
            'sbCriticalCheck2(riRow, m_dt_RstCdHelp) '문자열 크리티컬 임시 막음
            sbAlertCheck(riRow)
            sbAlimitCheck(riRow)

            sbConvertFormat(riRow)

            .Row = riRow
            .Col = .GetColFromID("orgrst")

            Dim sOrgRst$ = "", sViewRst$ = "", sRstCmt$ = ""
            Dim sOrgRst_old$ = "", sViewRst_old$ = "", sRstCmt_old$ = ""

            .Row = riRow
            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
            .Col = .GetColFromID("viewrst") : sViewRst = .Text
            .Col = .GetColFromID("rstcmt") : sRstCmt = .Text

            .Col = .GetColFromID("corgrst") : sOrgRst_old = .Text
            .Col = .GetColFromID("cviewrst") : sViewRst_old = .Text
            .Col = .GetColFromID("crstcmt") : sRstCmt_old = .Text

            If sOrgRst <> sOrgRst_old Or sViewRst <> sViewRst_old Or sRstCmt <> sRstCmt_old Then
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Text = "1"
                Else
                    For intRow As Integer = riRow - 1 To 1 Step -1
                        .Row = intRow
                        .Col = .GetColFromID("chk")
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            .Text = "1"
                            Exit For
                        End If
                    Next
                End If

            End If
        End With

        '-- 자동소견변환
        'If mbQueryView = False Then sbGet_CvtCmtInfo(msBcNo, rbTest)
        'JJH 소견 파트별로
        If mbQueryView = False Then sbGet_CvtCmtInfo_TestCd(msBcNo, rbTest)

    End Sub
    ' 결과 체크
    Private Sub sbSet_JudgRst()

        With spdResult
            Dim sRst As String = ""

            For iRow As Integer = 1 To .MaxRows
                .Row = iRow
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("iud") : Dim sIUD As String = .Text

                If sChk = "1" Or sIUD = "1" Then
                    If .GetColFromID("orgrst") > 0 Then
                        .Col = .GetColFromID("orgrst") : sRst = .Text.Replace("'", "`") : .Text = sRst
                        .Col = .GetColFromID("viewrst") : .Text = sRst
                        .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                        .Col = .GetColFromID("cvtgbn") : Dim sCvtGbn As String = .Text

                        If sRst <> "" Or sCvtGbn <> "" Then
                            sbRstTypeCheck(iRow)    '-- 실제결과 -> 결과에 표시
                            sbHLCheck(iRow)
                            sbPanicCheck(iRow, m_dt_RstCdHelp)
                            sbUJudgCheck(iRow)
                            sbDeltaCheck(iRow, m_dt_RstCdHelp)
                            sbCriticalCheck(iRow)
                            'sbCriticalCheck2(iRow, m_dt_RstCdHelp) '<<<20180802 문자크리티컬
                            sbAlertCheck(iRow)
                            sbAlimitCheck(iRow)

                            sbGet_CvtRstInfo(sBcNo, sTestCd)
                            sbConvertFormat(iRow)
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    ' 결과저장 가능 확인
    Private Function fnChecakReg(ByVal rsRstFlg As String, ByRef raCmtCont As ArrayList) As ArrayList

        Dim sFn As String = "Function fnChecakGeneralTestReg(String) As ArrayList"
        Try
            Dim alMsg As New ArrayList
            Dim sChk$ = "", sOrgRst$ = "", sViewRst$ = "", sRstCmt$ = "", sRstFlg$ = ""
            Dim sOrgRst_o$ = "", sViewRst_o$ = "", sRstCmt_o$ = ""
            Dim sBcno$ = "", sSlipCd$ = "", sTestCd$ = "", sTnmd$ = "", sTcdGbn$ = "", sTitleYn$ = "", sReqSub$ = ""
            Dim sAlert$ = "", sPanic$ = "", sDelta$ = "", sCritical$ = ""
            Dim sBbtType$ = "", sBldGbn$ = "", sUsrId1$ = "", sUsrId2$ = "", sRst1$ = "", sRst2$ = "", sRstUnit$ = ""
            Dim sBfViewRst As String = ""
            Dim sETX As String = Convert.ToChar(3)

            Dim sBcNo_OLD As String = "", sSlipCd_old As String = ""
            Dim sCmtCont As String = ""

            Dim bFlag As Boolean = False

            With Me.spdResult
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("bcno") : sBcno = .Text
                    .Col = .GetColFromID("tnmd") : sTnmd = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                    '.Col = .GetColFromID("iud") : sIud = .Text
                    .Col = .GetColFromID("chk") : sChk = .Text
                    .Col = .GetColFromID("titleyn") : sTitleYn = .Text

                    If sChk = "1" And sTcdGbn = "P" And sTitleYn = "1" Then
                        For intidx As Integer = iRow + 1 To .MaxRows
                            .Row = intidx
                            .Col = .GetColFromID("iud") : sChk = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : sReqSub = .Text

                            If sOrgRst = "" And sReqSub = "1" Then
                                .Row = intidx
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd Then
                                    .Row = intidx
                                    .Col = .GetColFromID("rstflg")
                                    If .Text < rsRstFlg Then
                                        .Row = iRow
                                        .Col = .GetColFromID("iud") : .Text = ""
                                        Exit For
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next

                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("bcno") : sBcno = .Text
                    .Col = .GetColFromID("slipcd") : sSlipCd = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tnmd") : sTnmd = .Text
                    .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                    .Col = .GetColFromID("titleyn") : sTitleYn = .Text
                    .Col = .GetColFromID("reqsub") : sReqSub = .Text

                    .Col = .GetColFromID("iud") : sChk = .Text
                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text.Trim
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text.Trim
                    .Col = .GetColFromID("rstcmt") : sRstCmt = .Text
                    .Col = .GetColFromID("rstflg") : sRstFlg = .Text

                    .Col = .GetColFromID("alertmark") : sAlert = .Text
                    .Col = .GetColFromID("panicmark") : sPanic = .Text
                    .Col = .GetColFromID("deltamark") : sDelta = .Text
                    .Col = .GetColFromID("criticalmark") : sCritical = .Text

                    .Col = .GetColFromID("corgrst") : sOrgRst_o = .Text
                    .Col = .GetColFromID("cviewrst") : sViewRst_o = .Text
                    .Col = .GetColFromID("crstcmt") : sRstCmt_o = .Text

                    .Col = .GetColFromID("rstunit") : sRstUnit = .Text

                    If sChk = "1" And sOrgRst <> "" Then

                        If sBcno + sSlipCd <> sBcNo_OLD + sSlipCd_old And sCmtCont <> "" Then

                            sCmtCont = sCmtCont.Substring(0, sCmtCont.Length - 1)
                            sCmtCont = "다음과 같이 최종보고 되었던 자료 입니다." + vbCrLf + "[" + sCmtCont.Trim + "]"

                            Dim objCmt As New CMT_INFO

                            objCmt.BcNo = sBcNo_OLD
                            objCmt.PartSlip = sSlipCd_old
                            objCmt.CmtCont = sCmtCont

                            raCmtCont.Add(objCmt)

                            sCmtCont = ""
                        End If

                        sBcNo_OLD = sBcno : sSlipCd_old = sSlipCd

                        bFlag = False

                        If rsRstFlg = "3" Then '최종보고할예정 이고 
                            If sRstFlg = "3" Then '이전결과상태가 최종보고인경우 
                                If ((sTcdGbn = "P" Or sTcdGbn = "B") And sTitleYn = "1") Then  ' Or sTcdGbn = "C" Then
                                Else
                                    If sOrgRst = sOrgRst_o And sViewRst = sViewRst_o And sRstCmt = sRstCmt_o Then
                                        alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 바뀐정보가 없습니다.")
                                        bFlag = True
                                    ElseIf sOrgRst <> sOrgRst_o Or sViewRst <> sViewRst_o Then
                                        'sCmtCont += sTnmd + "(" + sOrgRst_o + "/" + sViewRst_o + ")|"
                                        sCmtCont += sTnmd + sETX + sViewRst + " " + sRstUnit + sETX + sViewRst_o + " " + sRstUnit + sETX + sTnmd + "{" + sOrgRst_o + "/" + sViewRst_o + "}|"
                                    End If
                                End If
                            End If
                        End If

                        If rsRstFlg = "2" Then
                            If sRstFlg = "3" Then
                                If (sOrgRst <> sOrgRst_o Or sViewRst <> sViewRst_o) And sAlert = "" Then
                                    'sCmtCont += sTnmd + "(" + sOrgRst_o + "/" + sViewRst_o + ")|"
                                    sCmtCont += sTnmd + sETX + sViewRst + " " + sRstUnit + sETX + sViewRst_o + " " + sRstUnit + sETX + sTnmd + "{" + sOrgRst_o + "/" + sViewRst_o + "}|"
                                Else
                                    alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 최종보고된 자료 입니다.")
                                    bFlag = True
                                End If
                            ElseIf sRstFlg = "2" Then
                                If ((sTcdGbn = "P" Or sTcdGbn = "B") And sTitleYn = "1") Then ' Or sTcdGbn = "C" Then
                                Else
                                    If sOrgRst = sOrgRst_o And sViewRst = sViewRst_o And sRstCmt = sRstCmt_o Then
                                        alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 바뀐정보가 없습니다.")
                                        bFlag = True
                                    End If
                                End If
                            End If
                        End If

                        If rsRstFlg = "1" Then
                            If sRstFlg = "3" Or sRstFlg = "2" Then
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 " + IIf(sRstFlg = "3", "최종보고", "중간보고").ToString + "된 자료 입니다.")
                                bFlag = True
                            Else
                                If (sTcdGbn = "P" Or sTcdGbn = "B") And sTitleYn = "1" Then
                                Else
                                    If sOrgRst = sOrgRst_o And sViewRst = sViewRst_o And sRstCmt = sRstCmt_o Then
                                        alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 바뀐정보가 없습니다.")
                                        bFlag = True
                                    End If
                                End If
                            End If
                        End If

                        If sAlert = "A" And STU_AUTHORITY.AFNReg <> "1" Then
                            bFlag = True
                            alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 Aleart에 대한 보고권한이 없습니다.")
                        End If

                        If sPanic = "P" And STU_AUTHORITY.PDFNReg <> "1" Then
                            bFlag = True
                            alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 Panic에 대한 보고권한이 없습니다.")
                        End If

                        If sDelta = "D" And STU_AUTHORITY.DFNReg <> "1" Then
                            bFlag = True
                            alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 Delta에 대한 보고권한이 없습니다.")
                        End If

                        'If sCritical = "C" And STU_AUTHORITY.CFNReg <> "1" Then
                        '    bFlag = True
                        '    alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 Critical에 대한 보고권한이 없습니다.")
                        'End If
                        '<<<20180710 critical 보고시 메시지 확인 창 
                        If sCritical = "C" Then
                            If STU_AUTHORITY.CFNReg <> "1" Then
                                bFlag = True
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 Critical에 대한 보고권한이 없습니다.")
                            Else
                                If MsgBox(" 검사항목: [" + sTestCd + "]" + sTnmd + "  Critical 결과가 포함 되어있습니다. 계속하시겠습니까?", MsgBoxStyle.Critical Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                Else
                                    bFlag = True
                                    MsgBox("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 Critical에 대한 보고는 제외 됩니다.")
                                    alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 Critical에 대한 보고는 취소 됩니다.")
                                End If
                            End If
                        End If

                        If sRstFlg = "3" Then
                            If sOrgRst <> sOrgRst_o And STU_AUTHORITY.FNUpdate <> "1" Then
                                bFlag = True
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 최종보고수정에 대한 보고권한이 없습니다.")
                            End If
                        Else
                            If sOrgRst_o <> "" And sOrgRst <> sOrgRst_o And STU_AUTHORITY.RstUpdate <> "1" Then
                                bFlag = True
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 결과수정에 대한 보고권한이 없습니다.")
                            End If
                        End If

                        If bFlag Then
                            .Row = iRow
                            .Col = .GetColFromID("iud") : .Text = ""
                        End If
                    End If
                Next

                '-- 2010/06/09 추가
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                    .Col = .GetColFromID("iud") : sChk = .Text

                    If sChk = "1" And sTcdGbn = "P" Then
                        For intidx As Integer = iRow + 1 To .MaxRows
                            .Row = intidx
                            .Col = .GetColFromID("iud") : sChk = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : sReqSub = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text

                            If sOrgRst <> "" Then
                                .Row = intidx
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) <> sTestCd Then Exit For

                                .Row = intidx : .Col = .GetColFromID("iud") : .Text = "1"
                            End If
                        Next
                    End If
                Next
                '-- 2010/06/09


                If sCmtCont <> "" Then
                    sCmtCont = sCmtCont.Substring(0, sCmtCont.Length - 1)
                    'sCmtCont = "다음과 같이 최종보고 되었던 자료 입니다." + vbCrLf + "[" + sCmtCont.Trim + "]"
                    'sCmtCont  =. WBC^3^2^. WBC{2/2}|. Hgb^2^1^. Hgb{1/1}
                    Dim sCmtList As String() = sCmtCont.Split("|"c)
                    Dim sCmtContAll As String = ""
                    Dim sCmtConOrg As String = ""
                    sCmtContAll = "다음과 같이 최종보고 되었던 자료 입니다." + vbCrLf
                    Dim sRstDt As String = Format(Now, "yyyy-MM-dd").ToString
                    sCmtContAll += "수정날짜 : " + sRstDt + vbCrLf

                    For ix As Integer = 0 To sCmtList.Length - 1
                        Dim sCmtInfo As String() = sCmtList(ix).Split(CChar(sETX))
                        sCmtContAll += "검사항목 : " + sCmtInfo(0) + vbCrLf
                        sCmtContAll += "수정 전 결과 : " + sCmtInfo(2) + vbCrLf
                        sCmtContAll += "수정 후 결과 : " + sCmtInfo(1) + vbCrLf
                        sCmtConOrg += sCmtInfo(3) + vbCrLf + "|"
                    Next

                    Dim objCmt As New CMT_INFO

                    objCmt.BcNo = sBcNo_OLD
                    objCmt.PartSlip = sSlipCd_old
                    objCmt.CmtCont = sCmtContAll + "@" + sCmtConOrg

                    raCmtCont.Add(objCmt)

                    sCmtCont = ""
                End If

                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                    .Col = .GetColFromID("titleyn") : sTitleYn = .Text
                    .Col = .GetColFromID("rstflg") : sRstFlg = .Text

                    .Col = .GetColFromID("chk") : sChk = .Text

                    'If iRow = .MaxRows Then MsgBox("A")

                    If sChk = "1" And sTcdGbn = "P" And sTitleYn <> "0" Then
                        Dim iCnt% = 0
                        For ix As Integer = iRow + 1 To .MaxRows
                            '< 2016-12-20 윤장열 수정 (Parent검사 단독일 때 결과검증 안 되는 버그 수정 .Row = ix(Child부터 체크) -> .Row = iRow(Parent포함))
                            .Row = ix
                            '>
                            .Col = .GetColFromID("iud") : Dim strIUD As String = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : sReqSub = .Text
                            .Col = .GetColFromID("rstflg") : Dim strSubRstFlg As String = .Text
                            .Col = .GetColFromID("testcd") : Dim sTsubCd As String = .Text

                            If sTestCd <> sTsubCd.Substring(0, 5) Then Exit For
                            'If intidx = .MaxRows Then MsgBox("B")

                            If strIUD = "1" Then
                                .Row = ix
                                .Col = .GetColFromID("testcd")
                                Dim test As String = .Text.Substring(0, 5) 
                                If .Text.Substring(0, 5) = sTestCd Then
                                    iCnt += 1
                                Else
                                    Exit For
                                End If
                            ElseIf sReqSub = "1" And sOrgRst = "" Then
                                iCnt = 99
                                Exit For
                            ElseIf sRstFlg < strSubRstFlg Then
                                iCnt = 1
                            End If
                        Next

                        If iCnt = 0 Then
                            .Row = iRow
                            .Col = .GetColFromID("chk") : .Text = ""
                            .Col = .GetColFromID("iud") : .Text = ""
                        ElseIf iCnt = 99 Then
                            .Row = iRow
                            .Col = .GetColFromID("iud") : .Text = ""
                        End If

                    End If
                Next

                '-- 혈액은행인 경우
                If mbBloodBank And (rsRstFlg.Substring(0, 1) = "2" Or rsRstFlg = "3") Then
                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("chk") : sChk = .Text
                        .Col = .GetColFromID("bbttype") : sBbtType = .Text
                        .Col = .GetColFromID("bldgbn") : sBldGbn = .Text
                        .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                        .Col = .GetColFromID("tnmd") : sTnmd = .Text

                        .Col = .GetColFromID("viewrst") : sViewRst = .Text
                        .Col = .GetColFromID("bfviewrst1") : sBfViewRst = .Text

                        If sChk = "1" And sBldGbn = "00" And sBbtType = "1" Then
                            .Row = iRow + 1
                            .Col = .GetColFromID("orgrst") : sRst1 = .Text
                            .Col = .GetColFromID("fnid") : sUsrId1 = .Text

                            If sUsrId1 = "" And sRst1 <> "" Then
                                .Col = .GetColFromID("wmid") : sUsrId1 = .Text
                            End If

                            If sUsrId1 = "" And sRst1 <> "" Then
                                .Col = .GetColFromID("regid") : sUsrId1 = .Text
                            End If

                            .Row = iRow + 2
                            .Col = .GetColFromID("orgrst") : sRst2 = .Text
                            .Col = .GetColFromID("fnid") : sUsrId2 = .Text
                            If sUsrId2 = "" And sRst2 <> "" Then
                                .Col = .GetColFromID("wmid") : sUsrId2 = .Text
                            End If
                            If sUsrId2 = "" And sRst2 <> "" Then
                                .Col = .GetColFromID("regid") : sUsrId2 = .Text
                            End If

                            If sRst1 <> "" And sRst2 <> "" Then
                                If sRst1 <> "" And sUsrId1 = "" Then sUsrId1 = STU_AUTHORITY.UsrID
                                If sRst2 <> "" And sUsrId2 = "" Then sUsrId2 = STU_AUTHORITY.UsrID

                                If sUsrId1 = sUsrId2 Then
                                    alMsg.Add("'검사항목: " + sTnmd + "' 1차, 2차 결과 등록자가 같습니다.")

                                    .Row = iRow : .Col = .GetColFromID("chk") : .Text = ""
                                    .Row = iRow : .Col = .GetColFromID("iud") : .Text = ""

                                ElseIf sRst1 <> sRst2 Then
                                    alMsg.Add("'검사항목: " + sTnmd + "' 1차, 2차 결과 값이 들립니다.")

                                    .Row = iRow : .Col = .GetColFromID("chk") : .Text = ""
                                    .Row = iRow : .Col = .GetColFromID("iud") : .Text = ""
                                Else
                                    .Row = iRow : .Col = .GetColFromID("iud") : .Text = "1"
                                    .Row = iRow : .Col = .GetColFromID("orgrst") : .Text = sRst1
                                    .Row = iRow : .Col = .GetColFromID("viewrst") : .Text = sRst1
                                End If
                            ElseIf sRst1 <> "" Then
                                .Row = iRow : .Col = .GetColFromID("iud") : .Text = "1"
                                .Row = iRow : .Col = .GetColFromID("orgrst") : .Text = sRst1
                                .Row = iRow : .Col = .GetColFromID("viewrst") : .Text = sRst1
                            End If

                            If sUsrId2 = "" And sRst2 <> "" Then
                                .Col = .GetColFromID("mwid") : sUsrId2 = .Text
                            End If
                        ElseIf sChk = "1" Then

                            '<혈액형 결과 없고 크로스매칭 검사 진행시 팝업 추가 2019-04-26

                            Dim dt As DataTable = New DataTable
                            dt = CGDA_BT.fnGet_ABORh(msRegNo)

                            .Row = iRow
                            .Col = .GetColFromID("testcd")
                            If dt.Rows.Count <= 0 And (.Text = "LB141" Or .Text = "LB142") Then
                                If MsgBox("혈액형 결과가 없는 초진 환자입니다. 그래도 크로스매칭 검사를 진행하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    bFlag = True
                                End If
                                If bFlag Then
                                    .Row = iRow
                                    .Col = .GetColFromID("iud") : .Text = ""
                                End If
                            End If


                        End If
                    Next
                End If
            End With

            Return alMsg
        Catch ex As Exception
            Return New ArrayList
        End Try

    End Function

    Private Function fnGetRst(ByVal rsRstflg As String, ByVal rsCfmNm As String, ByVal rsCfmSign As String) As ArrayList
        Dim sFn As String = "Function fnGetRst(string) As ArrayList"
        Try
            Dim sRstflg = ""
            Dim strORst_o$ = "", strVRst_o$ = "", strCmt_o$ = ""

            Dim aryRst As New ArrayList
            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("iud")
                    Dim a = .Text
                    If .Text = "1" Then
                        Dim objRst As New ResultInfo_Test

                        .Col = .GetColFromID("bcno") : objRst.mBCNO = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : objRst.mTestCd = .Text
                        .Col = .GetColFromID("spccd") : objRst.mSpcCd = .Text
                        .Col = .GetColFromID("orgrst") : objRst.mOrgRst = .Text
                        .Col = .GetColFromID("viewrst") : objRst.mViewRst = .Text
                        .Col = .GetColFromID("panicmark") : objRst.mPanicMark = .Text
                        .Col = .GetColFromID("deltamark") : objRst.mDeltaMark = .Text
                        .Col = .GetColFromID("alertmark") : objRst.mAlertMark = .Text
                        .Col = .GetColFromID("criticalmark") : objRst.mCriticalMark = .Text
                        .Col = .GetColFromID("hlmark") : objRst.mHLMark = .Text
                        .Col = .GetColFromID("rstcmt") : objRst.mRstCmt = .Text
                        .Col = .GetColFromID("bfbcno1") : objRst.mBFBCNO = .Text.Replace("-", "")
                        .Col = .GetColFromID("bffndt1") : objRst.mBFFNDT = .Text.Replace("-", "").Replace(":", "").Replace(" ", "")
                        .Col = .GetColFromID("bforgrst1") : objRst.mBFORGRST = .Text
                        .Col = .GetColFromID("bfviewrst1") : objRst.mBFVIEWRST = .Text
                        .Col = .GetColFromID("cfmnm") : Dim sCfmNm As String = .Text

                        .Col = .GetColFromID("reftxt") : objRst.mRefTxt = .Text

                        '< yjlee 2009-01-16
                        .Col = .GetColFromID("titleyn") : objRst.mDetailYN = .Text
                        .Col = .GetColFromID("tcdgbn") : Dim sTCdGbn As String : sTCdGbn = .Text
                        '> yjlee 2009-01-16

                        .Col = .GetColFromID("rstflg") : sRstflg = .Text
                        .Col = .GetColFromID("corgrst") : strORst_o = .Text
                        .Col = .GetColFromID("cviewrst") : strVRst_o = .Text
                        .Col = .GetColFromID("ccmt") : strCmt_o = .Text

                        objRst.mEqCd = ""
                        objRst.mIntSeqNo = ""
                        objRst.mRack = ""
                        objRst.mPos = ""
                        objRst.mEQBCNO = ""
                        objRst.mEqFlag = ""
                        objRst.mCfmNm = ""
                        objRst.mCfmSign = ""

                        If rsRstflg = "2" Then
                            If objRst.mAlertMark <> "" Or objRst.mDeltaMark <> "" Then
                                objRst.mRstFlg = "1"

                            ElseIf objRst.mPanicMark <> "" Or objRst.mCriticalMark <> "" Then
                                objRst.mRstFlg = "2"
                            Else
                                objRst.mRstFlg = "3"
                                objRst.mCfmNm = IIf(rsCfmNm = "", sCfmNm, rsCfmNm).ToString
                                objRst.mCfmSign = rsCfmSign
                            End If
                        Else ''' 결과저장 
                            objRst.mRstFlg = rsRstflg.Substring(0, 1)
                            If rsRstflg = "3" Then
                                objRst.mCfmNm = IIf(rsCfmNm = "", sCfmNm, rsCfmNm).ToString
                                objRst.mCfmSign = rsCfmSign
                            End If
                        End If

                        ''' sRstflg=조회된결과상태  rsRstflg=버튼누른상태  최종보고된거 결과저장 안되도록 
                        If rsRstflg >= sRstflg Then  ''' ACK 박정은 추가 2010-10-26 
                            '< yjlee 2009-01-16
                            If (objRst.mOrgRst <> "" And (strORst_o <> objRst.mOrgRst Or strVRst_o <> objRst.mViewRst Or _
                                                          sRstflg <> objRst.mRstFlg Or strCmt_o <> objRst.mRstCmt)) Or _
                               (objRst.mDetailYN = "1" And sTCdGbn = "P") Then
                                aryRst.Add(objRst)
                            End If
                            '> yjlee 2009-01-16
                        End If ''' ACK 박정은 추가 2010-10-26 


                    End If
                Next

                Return aryRst
            End With
        Catch ex As Exception
            Return New ArrayList
        End Try

    End Function

    Private Sub sbDisplay_Blood_Alert()

        Try
            Dim sABOc_rst As String = "", sABOs_Rst As String = "", sRh_rst As String = ""

            With spdResult
                For intRow As Integer = 1 To .MaxRows

                    .Row = intRow
                    .Col = .GetColFromID("testcd") : Dim sTestCd = .Text

                    Select Case sTestCd
                        Case msBlood_ABO_S
                            .Col = .GetColFromID("orgrst") : sABOs_Rst = .Text
                        Case msBlood_ABO_C
                            .Col = .GetColFromID("orgrst") : sABOc_rst = .Text
                        Case msBlood_Rh
                            .Col = .GetColFromID("orgrst") : sRh_rst = .Text
                    End Select
                Next
            End With

            If (sABOs_Rst <> "" Or sABOc_rst <> "") And sRh_rst <> "" And lblABO_bf.Text <> "" Then
                If sABOs_Rst <> "" And sABOs_Rst + sRh_rst <> lblABO_bf.Text Then
                    MsgBox("이전 혈액형 결과와 다릅니다.  확인하세요.!!", , "결과입력")
                End If

                If sABOc_rst <> "" And sABOc_rst + sRh_rst <> lblABO_bf.Text Then
                    MsgBox("이전 혈액형 결과와 다릅니다.  확인하세요.!!", , "결과입력")
                End If
            End If

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try

    End Sub

    Private Sub sbGet_CvtRst_Blood(ByVal rsOrgRst As String)
        Try
            Dim arlRst As New ArrayList
            Dim sTestCd$ = ""

            With spdResult
                For intRow As Integer = 1 To .MaxRows

                    .Row = intRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text

                    Select Case sTestCd
                        Case msBlood_ABO_S
                            .Col = .GetColFromID("orgrst") : .Text = rsOrgRst
                            sbSet_ResultView(intRow)
                        Case msBlood_Rh
                            .Col = .GetColFromID("orgrst") : .Text = "(+)"
                            sbSet_ResultView(intRow)
                    End Select
                Next

            End With
        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try

    End Sub

    Private Sub sbGet_CvtRstInfo(ByVal rsBcNo As String, Optional ByVal rsTestCd As String = "", Optional ByVal rsIFGbn As Boolean = False)
        Try
            Dim alRst As New ArrayList
            Dim sBcNo As String = ""
            Dim sTestCd$ = "", sSpcCd$ = "", sOrgRst$ = "", sViewRst$ = "", sHLmark$ = ""

            With spdResult
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text
                    .Col = .GetColFromID("hlmark") : sHLmark = .Text

                    If sOrgRst <> "" Then
                        Dim objRst As New STU_RstInfo_cvt

                        objRst.BcNo = sBcNo
                        objRst.TestCd = sTestCd
                        objRst.SpcCd = sSpcCd
                        objRst.OrgRst = sOrgRst
                        objRst.ViewRst = sViewRst
                        objRst.HlMark = sHLmark

                        alRst.Add(objRst)
                    End If
                Next
            End With

            Dim alCvtRst As New ArrayList

            If rsTestCd = "" Then
                alCvtRst = LISAPP.COMM.CvtRst.fnCvtRstInfo(rsBcNo, alRst, rsIFGbn)
            Else
                alCvtRst = LISAPP.COMM.CvtRst.fnCvtRstInfo(rsBcNo, rsTestCd, alRst, rsIFGbn)
            End If

            If alCvtRst.Count < 1 Then Exit Sub

            With spdResult
                For ix As Integer = 0 To alCvtRst.Count - 1

                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : sTestCd = .Text

                        If CType(alCvtRst(ix), STU_RstInfo_cvt).BcNo = sBcNo And CType(alCvtRst(ix), STU_RstInfo_cvt).TestCd = sTestCd Then
                            If CType(alCvtRst(ix), STU_RstInfo_cvt).CvtFldGbn <> "C" Then

                                If CType(alCvtRst(ix), STU_RstInfo_cvt).CvtRange = "B" Then
                                Else
                                    .Col = .GetColFromID("orgrst") : .Text = CType(alCvtRst(ix), STU_RstInfo_cvt).OrgRst
                                End If

                                .Col = .GetColFromID("viewrst") : .Text = CType(alCvtRst(ix), STU_RstInfo_cvt).ViewRst

                                .Col = .GetColFromID("tcdgbn")
                                If .Text = "C" Then
                                    For intIx2 As Integer = iRow - 1 To 1 Step -1

                                        .Row = intIx2
                                        .Col = .GetColFromID("tcdgbn") : Dim strTcdGbn As String = .Text
                                        .Col = .GetColFromID("testcd")

                                        If strTcdGbn = "P" And .Text = sTestCd.Substring(0, 5) Then
                                            .Col = .GetColFromID("chk") : .Text = "1"
                                            Exit For
                                        End If
                                    Next
                                End If

                                sbSet_ResultView(iRow)
                            Else
                                .Col = .GetColFromID("rstcmt") : .Text = CType(alCvtRst(ix), STU_RstInfo_cvt).RstCmt
                            End If
                            Exit For
                        End If
                    Next
                Next

            End With
        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbGet_CvtCmtInfo(ByVal rsBcNo As String, ByVal rbLisMode As Boolean)

        Try
            Dim alRst As New ArrayList
            Dim sBcNo As String = ""
            Dim sTestCd As String = "", sSpcCd As String = "", sOrgRst As String = "", sViewRst As String = "", sHLmark As String = "", sEqFlag As String = "", sRegNo As String = ""
            Dim a_dt As DataTable = New DataTable

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text
                    .Col = .GetColFromID("hlmark") : sHLmark = .Text
                    .Col = .GetColFromID("eqflag") : sEqFlag = .Text

                    If sOrgRst <> "" Then
                        Dim objRst As New STU_CvtCmtInfo

                        objRst.BcNo = sBcNo
                        objRst.TestCd = sTestCd
                        objRst.OrgRst = sOrgRst
                        objRst.ViewRst = sViewRst
                        objRst.HlMark = sHLmark
                        objRst.EqFlag = sEqFlag

                        alRst.Add(objRst)
                    End If
                Next
            End With

            Dim sSlipCd As String = msPartSlip
            If sSlipCd = "" Then sSlipCd = Ctrl.Get_Code(Me.cboSlip)
            Dim alCvtCmt As New ArrayList

            'If msXpertTcd = True Then '2019-09-17 yjy Xpert PCR Critical 판정 시 결과소견 자동 입력 추가 (msXpertTcd = True -> LG104검사가 Critical판정일 경우 1주일 검사결과 소견으로 추가)
            '    sbDisplay_XPertCmt(cboBcNos.Text.Replace("-", ""))
            'End If


            alCvtCmt = LISAPP.COMM.CvtCmt.fnCvtCmtInfo(rsBcNo, alRst, sSlipCd, rbLisMode)

            Dim sCmt$ = ""
            Dim sCmt2 As String = ""


            If alCvtCmt.Count < 1 Then Exit Sub


            For intIdx As Integer = 0 To alCvtCmt.Count - 1
                If sSlipCd = CType(alCvtCmt(intIdx), STU_CvtCmtInfo).SlipCd Then

                    sCmt += CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont

                    If CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont = "" Then
                        Me.txtCmtCont.Text = Me.txtCmtCont.Text.Replace(CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont_Base + vbCrLf, "")
                        Me.txtCmtCont.Text = Me.txtCmtCont.Text.Replace(CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont_Base, "")
                    End If

                Else
                    Dim ci As New CMT_INFO
                    With ci
                        .BcNo = rsBcNo
                        .PartSlip = CType(alCvtCmt(intIdx), STU_CvtCmtInfo).SlipCd

                        If CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont = "" Then
                            .CmtCont = CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont
                        Else
                            .CmtCont = CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont_Base
                        End If

                    End With

                    sbSet_Cmt_BcNo_Edit(ci)
                End If
            Next


            '< 2016-11-22 YJY 결핵검사 진행 시 환자의 최근 CBC검사항목 결과 가져와 소견으로 Display.
            If sTestCd = "LI611" Or sTestCd = "LI612" Or sTestCd = "LI613" Then
                Dim stestisno611 As String = "", stestisno612 As String = "", stest611rdt As String = "", stest611rst As String = "", stest611rstunit As String = "", _
                stest612rstunit As String = "", stest612rdt As String = "", stest612rst As String = ""
                'If msRegNoCmt <> "" Then 'LI611, LI612 검사로 판단되었다면
                a_dt = LISAPP.COMM.RstFn.fnGet_Pat_Recent_Rst(msRegNo) '환자의 최근 CBC검사 항목 가져오기

                '불러온 이전 결과 없을 경우 "기존의뢰 없음" 표시 
                If a_dt.Rows.Count = 0 Then
                    If sCmt2 = "" Then
                        sCmt2 += "4. 과거 일반혈액 검사결과 " '2019-07-10 JJH 3->4 수정
                        sCmt2 += vbNewLine
                        sCmt2 += "   검사항목                                   검사시행날짜      실제결과 "
                        sCmt2 += vbNewLine
                        sCmt2 += "   WBC Count (CBC)                            기존의뢰 없음"
                        sCmt2 += vbNewLine
                        sCmt2 += "   Lymphocyte Count (WBC Differential Count)  기존의뢰 없음"
                    End If
                Else
                    '-결과 있을 경우 이전 결과 변수 담기
                    For i As Integer = 0 To a_dt.Rows.Count - 1
                        If a_dt.Rows(i).Item("testcd").ToString.Equals("LH101") Then
                            stest611rdt = a_dt.Rows(i).Item("rstdtd").ToString
                            stest611rst = a_dt.Rows(i).Item("viewrst").ToString
                            stest611rstunit = a_dt.Rows(i).Item("rstunit").ToString
                        ElseIf a_dt.Rows(i).Item("testcd").ToString.Equals("LH12103") Then
                            stest612rdt = a_dt.Rows(i).Item("rstdtd").ToString
                            stest612rst = a_dt.Rows(i).Item("viewrst").ToString
                            stest612rstunit = a_dt.Rows(i).Item("rstunit").ToString
                        End If
                    Next
                    '-
                    If stest611rdt = "" Then
                        stestisno611 = "기존의뢰 없음"
                    ElseIf stest612rdt = "" Then
                        stestisno612 = "기존의뢰 없음"
                    End If
                    '-자동 소견 양식 만들고 이전 결과 넣어 주기
                    sCmt2 += "4. 과거 일반혈액 검사결과 "  '2019-07-10 JJH 3->4 수정
                    sCmt2 += vbNewLine
                    sCmt2 += "   검사항목                                   검사시행날짜      실제결과 "
                    sCmt2 += vbNewLine
                    If stestisno611 = "" Then
                        sCmt2 += "   WBC Count (CBC)                            " + stest611rdt + Space(8) + stest611rst + Space(1) + stest611rstunit
                    Else
                        sCmt2 += "   WBC Count (CBC)                            " + stestisno611
                    End If
                    sCmt2 += vbNewLine
                    If stestisno612 = "" Then
                        sCmt2 += "   Lymphocyte Count (WBC Differential Count)  " + stest612rdt + Space(8) + stest612rst + Space(1) + stest612rstunit
                    Else
                        sCmt2 += "   Lymphocyte Count (WBC Differential Count)  " + stestisno612
                    End If
                    '-
                End If

            End If
            '>

            Dim alTmp As New ArrayList
            Dim sBuf1() As String = Me.txtCmtCont.Text.Replace(Chr(10), "").Split(Chr(13))
            Dim sBuf2() As String = sCmt.Replace(Chr(10), "").Split(Chr(13))
            Dim sBuf3() As String = sCmt2.Replace(Chr(10), "").Split(Chr(13))


            For ix As Integer = 0 To sBuf1.Length - 1

                alTmp.Add(sBuf1(ix).Trim())
            Next


            sCmt = ""
            sCmt2 = ""


            '결과소견 변경여부 체크
            For ix As Integer = 0 To sBuf2.Length - 1
                If alTmp.Contains(sBuf2(ix).Trim) = False Then
                    sCmt += sBuf2(ix) + vbCrLf
                End If
            Next

            '결핵균검사 변경여부 체크
            For ix As Integer = 0 To sBuf3.Length - 1
                If alTmp.Contains(sBuf3(ix).Trim) = False Then
                    'If sCmt2.Length = 0 Then
                    '    sCmt2 += sBuf3(ix) + vbCrLf
                    'Else
                    '    sCmt2 += sBuf3(ix) + vbCrLf
                    'End If
                    sCmt2 += sBuf3(ix) + vbCrLf
                End If
            Next

            '결과자동소견 넣기
            If sCmt <> "" Then
                If Me.txtCmtCont.Text = "" Then
                    Me.txtCmtCont.Text = sCmt
                Else
                    Me.txtCmtCont.Text += vbCrLf + sCmt
                End If
            End If

            '결핵균검사 소견 넣기
            If sCmt2 <> "" Then
                If Me.txtCmtCont.Text = "" Then
                    Me.txtCmtCont.Text = sCmt2
                Else
                    Me.txtCmtCont.Text += vbCrLf + sCmt2
                End If
            End If


            txtCmtCont_LostFocus(Nothing, Nothing)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Private Function fnGet_Rst_ReRun(ByVal rsRerunGbn As String, ByRef rsCmtCont As String, ByRef rsErrMsg As String) As ArrayList
        Dim sFn As String = "Function fnGet_Rst_ReRun(string) As ArrayList"
        Try
            Dim aryRst As New ArrayList
            Dim strBcNo As String = "", sTestcd As String = "", sTclsCd As String = "", strTcdGbn As String = "", strTitleYn As String = ""
            Dim sRstflg As String = "", strTnmd As String = "", strEqCd As String = "", strOrgRst As String = "", strViewRst As String = ""
            Dim blnFlag As Boolean = False

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("iud")
                    If .Text = "1" Then
                        .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : sTestcd = .Text
                        .Col = .GetColFromID("rstflg") : sRstflg = .Text
                        .Col = .GetColFromID("tcdgbn") : strTcdGbn = .Text
                        .Col = .GetColFromID("testcd") : sTclsCd = .Text : If sTclsCd <> "" Then sTclsCd = sTclsCd.Substring(0, 5)
                        .Col = .GetColFromID("tnmd") : strTnmd = .Text
                        .Col = .GetColFromID("eqcd") : strEqCd = .Text
                        .Col = .GetColFromID("orgrst") : strOrgRst = .Text
                        .Col = .GetColFromID("viewrst") : strViewRst = .Text
                        .Col = .GetColFromID("titleyn") : strTitleYn = .Text
                        Dim objRst As New RERUN_INFO

                        blnFlag = True
                        If sRstflg = "3" Then
                            If STU_AUTHORITY.FNUpdate = "1" Then
                                sRstflg = ""

                                rsCmtCont += strTnmd + "{" + strOrgRst + "/" + strViewRst + "}|"
                            Else
                                rsErrMsg += strTnmd + "|"
                                blnFlag = False
                            End If
                            'ElseIf sRstflg = "2" Or sRstflg = "1" Then
                            '    sRstflg = "1"
                        Else
                            sRstflg = ""
                        End If

                        'If (blnFlag And strEqCd <> "" And strOrgRst <> "") Or (strEqCd <> "" And strTcdGbn = "P") Then
                        'If (blnFlag And (strEqCd <> "" And strOrgRst <> "") Or (strEqCd = "" And strOrgRst = "")) Or (strEqCd <> "" And strTcdGbn = "P") Then
                        If blnFlag And strOrgRst <> "" Then
                            objRst.msRstFlg = sRstflg
                            objRst.msBcNo = strBcNo
                            objRst.msTestCd = sTestcd
                            objRst.msRerunGbn = rsRerunGbn

                            aryRst.Add(objRst)
                            objRst = Nothing
                        End If

                    End If
                Next

                If rsCmtCont <> "" Then
                    rsCmtCont = rsCmtCont.Substring(0, rsCmtCont.Length - 1)
                    rsCmtCont = "다음과 같이 최종보고 되었던 자료 입니다." + vbCrLf + "[" + rsCmtCont.Trim + "]"
                End If

                fnGet_Rst_ReRun = aryRst
            End With
        Catch ex As Exception
            fnGet_Rst_ReRun = New ArrayList
        End Try

    End Function

    Private Function fnGet_Rst_Erase() As ArrayList
        Dim sFn As String = "Function fnGet_Rst_Erase() As ArrayList"
        Try
            Dim aryRst As New ArrayList

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("rstflg") : Dim sRstflg As String = .Text
                    .Col = .GetColFromID("chk")
                    If .Text = "1" Then

                        Dim objRst As New ResultInfo_Test

                        .Col = .GetColFromID("bcno") : objRst.mBCNO = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : objRst.mTestCd = .Text

                        objRst.mAlertMark = ""
                        objRst.mBatchCmt = ""
                        objRst.mBatchRstChk = ""
                        objRst.mBFBCNO = ""
                        objRst.mBFFNDT = ""
                        objRst.mBFORGRST = ""
                        objRst.mBFVIEWRST = ""
                        objRst.mRstCmt = ""
                        objRst.mCriticalMark = ""
                        objRst.mDeltaMark = ""
                        objRst.mDetailYN = ""
                        objRst.mDGTestCd = ""
                        objRst.mEQBCNO = ""
                        objRst.mIntSeqNo = ""
                        objRst.mHLMark = ""
                        objRst.mOrgRst = ""
                        objRst.mPanicMark = ""
                        objRst.mPos = ""
                        objRst.mRack = ""
                        objRst.mSpcCd = ""
                        objRst.mTestNm = ""
                        objRst.mViewRst = ""
                        objRst.mUpdateYN = ""

                        aryRst.Add(objRst)

                        Dim sTestCd_p As String = ""
                        Dim sTcdGbn As String = ""

                        .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                        .Col = .GetColFromID("testcd") : sTestCd_p = .Text

                        If sTcdGbn = "P" Then
                            For intIdx = intRow + 1 To .MaxRows
                                .Row = intIdx
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd_p Then

                                    objRst = New ResultInfo_Test

                                    .Col = .GetColFromID("bcno") : objRst.mBCNO = .Text.Replace("-", "")
                                    .Col = .GetColFromID("testcd") : objRst.mTestCd = .Text

                                    objRst.mAlertMark = ""
                                    objRst.mBatchCmt = ""
                                    objRst.mBatchRstChk = ""
                                    objRst.mBFBCNO = ""
                                    objRst.mBFFNDT = ""
                                    objRst.mBFORGRST = ""
                                    objRst.mBFVIEWRST = ""
                                    objRst.mRstCmt = ""
                                    objRst.mCriticalMark = ""
                                    objRst.mDeltaMark = ""
                                    objRst.mDetailYN = ""
                                    objRst.mDGTestCd = ""
                                    objRst.mEQBCNO = ""
                                    objRst.mIntSeqNo = ""
                                    objRst.mHLMark = ""
                                    objRst.mOrgRst = ""
                                    objRst.mPanicMark = ""
                                    objRst.mPos = ""
                                    objRst.mRack = ""
                                    objRst.mSpcCd = ""
                                    objRst.mTestNm = ""
                                    objRst.mViewRst = ""
                                    objRst.mUpdateYN = ""

                                    aryRst.Add(objRst)
                                Else
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Next

                fnGet_Rst_Erase = aryRst
            End With
        Catch ex As Exception
            fnGet_Rst_Erase = New ArrayList
        End Try

    End Function

    '-- 결과소거
    Public Function fnReg_Erase() As Boolean
        Dim alRst As New ArrayList

        Try
            If STU_AUTHORITY.RstClear = "1" Then
                alRst = fnGet_Rst_Erase()

                If alRst.Count > 0 Then

                    Dim objRst As New LISAPP.APP_R.AxRstFn
                    Return objRst.fnRsg_RstClear(STU_AUTHORITY.UsrID, alRst)
                End If
            End If



            Return True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return False
        End Try
    End Function

    '-- Rerun 설정
    Public Function fnReRun(ByVal rsReRunGbn As String) As Boolean

        Dim arlRst As New ArrayList
        Dim arlCmt As New ArrayList

        Dim strErrMsg As String = ""

        Try

            Dim strCmtCont As String = ""

            arlRst = fnGet_Rst_ReRun(rsReRunGbn, strCmtCont, strErrMsg)
            If strErrMsg <> "" Then
                MsgBox("[검사명 : " + strErrMsg.Substring(0, strErrMsg.Length - 1) + "]는 최종보고된 자료입니다.!!" + vbCrLf + _
                       "최종보고 수정 권한이 없어 재검할 수 없습니다.")
            End If

            If strCmtCont <> "" Then
                Dim frm As New FGFINAL_CMT

                frm.msBcNo = msBcNo
                frm.msCmt = strCmtCont
                Dim strRet As String = frm.Display_Result()

                If strRet = "" Then Return True

                If Me.txtCmtCont.Text.IndexOf(strCmtCont) < 0 Then
                    If txtCmtCont.Text <> "" Then
                        strCmtCont = txtCmtCont.Text + vbCrLf + strCmtCont
                    End If

                    Dim sSlipCd As String = msPartSlip
                    If sSlipCd = "" Then SlipCd = Ctrl.Get_Code(Me.cboSlip)

                    Dim ci As New CMT_INFO
                    With ci
                        .BcNo = msBcNo
                        .PartSlip = sSlipCd
                        .CmtCont = Me.txtCmtCont.Text
                    End With
                    sbSet_Cmt_BcNo_Edit(ci)

                    Dim arlBuf() As String

                    arlBuf = Me.txtCmtCont.Text.Replace(Chr(10), "").Split(Chr(13))

                    For intIdx As Integer = 0 To arlBuf.Length - 1
                        Dim objBR As New ResultInfo_Cmt
                        objBR.BcNo = msBcNo
                        objBR.PartSlip = sSlipCd
                        objBR.TestCd = ""

                        objBR.RstSeq = Convert.ToString(intIdx).PadLeft(2, "0"c)
                        objBR.Cmt = arlBuf(intIdx)
                        objBR.RstFlg = ""

                        arlCmt.Add(objBR)
                    Next
                End If
            End If

            If arlRst.Count > 0 Then
                Dim objRst As New LISAPP.APP_R.AxRstFn

                Return objRst.fnReg_rerun(arlRst, arlCmt)
            End If


            Return True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return False
        End Try

    End Function

    Public Function fnReg(ByVal rsRstflg As String, Optional ByVal rsCfmNm As String = "", Optional ByVal rsCfmSign As String = "", Optional ByVal rbBFNReg As Boolean = False) As Boolean
        ''' rsRstflg  1=결과저장 2=결과확인 3=결과검증
        Dim alReturn As New ArrayList

        Dim alRst As New ArrayList
        Dim alCmt As New ArrayList
        Dim alRstLog As New ArrayList
        Dim srMsg As String = ""
        Dim alCmtPS As String = ""

        Try
            '20210312 JHS WBC DIFFCOUNT 검사 내용 로그로 남기기 
            alRstLog = fnReg_log_info_before(rsRstflg, "1") ' 저장 전
            '------------------------------------------------
            '20220127 jhs WBC diffCount 100인지 확인하는 로직 구현
            Dim chk_WEBCount As Double = fnChk_WBCCount()
            If chk_WEBCount <> 100 And msWbcCount Then
                If fn_PopConfirm(moForm, "E"c, "WBC Count의 합이 100이 아닙니다." + vbCrLf + "현재 Count : " + chk_WEBCount.ToString + vbCrLf + "계속진행 하시겠습니까?") <> True Then
                    Return False
                End If
            End If
            '-------------------------------------------

            mbLeveCellGbn = False

            sbGet_Alert_Rule()

            If fnFind_Diff_ABO_Type() Then Return False

            If rbBFNReg = False Then
                sbDisplay_Update()
                sbSet_JudgRst()
            End If

            If mbBatchMode = False Then
                With Me.spdResult
                    Dim sBcNo As String = ""
                    Dim sBcNo_t As String = ""

                    For iRow As Integer = 1 To Me.spdResult.MaxRows

                        .Row = iRow
                        .Col = .GetColFromID("bcno") : sBcNo_t = .Text.Replace("-", "")

                        If sBcNo_t <> sBcNo Then
                            If sBcNo <> "" Then
                                sbGet_CvtCmtInfo(sBcNo, False)
                            End If
                        End If
                        sBcNo = sBcNo_t
                    Next
                    'sbGet_CvtCmtInfo(sBcNo, False)
                    sbGet_CvtCmtInfo_TestCd(sBcNo, False)
                End With

            End If

            If rbBFNReg = False Then Me.txtCmtCont_LostFocus(Nothing, Nothing)

            Dim alCmtCont As New ArrayList

            alReturn = fnChecakReg(rsRstflg, alCmtCont)

            If alReturn.Count > 0 And mbBloodBank Then
                Dim sMsg As String = ""
                For ix1 As Integer = 0 To alReturn.Count - 1
                    sMsg += alReturn.Item(ix1).ToString + vbCrLf
                Next

                MsgBox(sMsg, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "확인")
            End If

            If alCmtCont.Count > 0 Then
                For ix1 As Integer = 0 To alCmtCont.Count - 1
                    Dim frm As New FGFINAL_CMT2 '분야별 결과저장에서는 최종보고 수정시 소견 변경으로 FGFINAL_CMT2를 사용함.

                    frm.msBcNo = CType(alCmtCont.Item(ix1), CMT_INFO).BcNo
                    frm.msPartSlip = CType(alCmtCont.Item(ix1), CMT_INFO).PartSlip
                    frm.msCmt = CType(alCmtCont.Item(ix1), CMT_INFO).CmtCont

                    Dim sRet As String = frm.Display_Result()

                    If sRet.Split("|"c)(0) <> "OK" Then Return False

                    For ix2 As Integer = 0 To Me.cboBcNos.Items.Count - 1
                        Dim sBcno_Full As String = Fn.BCNO_View(CType(alCmtCont.Item(ix1), CMT_INFO).BcNo, True)
                        Me.cboBcNos.SelectedIndex = ix2
                        If Me.cboBcNos.Text = sBcno_Full Then Exit For
                    Next

                    For ix2 As Integer = 0 To Me.cboSlip.Items.Count - 1
                        Me.cboSlip.SelectedIndex = ix2
                        If cboSlip.Text.StartsWith("[" + CType(alCmtCont.Item(ix1), CMT_INFO).PartSlip + "]") Then Exit For
                    Next

                    If Me.txtCmtCont.Text.IndexOf(CType(alCmtCont.Item(ix1), CMT_INFO).CmtCont) < 0 Then '지금 들어갈 소견이 현재 소견text에 없을경우

                        Dim sTempCmt As String = CType(alCmtCont.Item(ix1), CMT_INFO).CmtCont.Split("@"c)(0)

                        If Me.txtCmtCont.Text <> "" Then '소견이 비어있지 않을경우
                            'Me.txtCmtCont.Text += vbCrLf + CType(alCmtCont.Item(ix1), CMT_INFO).CmtCont
                            Me.txtCmtCont.Text += vbCrLf + sTempCmt + "결과 수정 사유 : " + sRet.Split("|"c)(1)
                        Else
                            'Me.txtCmtCont.Text += CType(alCmtCont.Item(ix1), CMT_INFO).CmtCont
                            Me.txtCmtCont.Text += sTempCmt + "결과 수정 사유 : " + sRet.Split("|"c)(1)
                        End If

                        Dim ci As New CMT_INFO
                        With ci
                            .BcNo = CType(alCmtCont.Item(ix1), CMT_INFO).BcNo
                            .PartSlip = CType(alCmtCont.Item(ix1), CMT_INFO).PartSlip
                            .CmtCont = Me.txtCmtCont.Text
                        End With
                        sbSet_Cmt_BcNo_Edit(ci)
                    End If
                Next
            End If

            alRst = fnGetRst(rsRstflg, rsCfmNm, rsCfmSign)

            Dim a_dr As DataRow()
            a_dr = m_dt_Cmt_bcno.Select() '--"status <> 'S'")

            If a_dr.Length > 0 Then

                For ix As Integer = 0 To a_dr.Length - 1
                    Dim arlBuf() As String

                    arlBuf = a_dr(ix).Item("cmtcont").ToString.Replace(Chr(10), "").Split(Chr(13))

                    For ix2 As Integer = 0 To arlBuf.Length - 1
                        Dim objBR As New ResultInfo_Cmt
                        objBR.BcNo = a_dr(ix).Item("bcno").ToString
                        objBR.PartSlip = a_dr(ix).Item("partslip").ToString
                        objBR.TestCd = ""

                        objBR.RstSeq = Convert.ToString(ix2).PadLeft(2, "0"c)
                        objBR.Cmt = arlBuf(ix2)
                        objBR.RstFlg = rsRstflg

                        alCmt.Add(objBR)
                    Next

                Next
            End If

            Dim objRst As New LISAPP.APP_R.AxRstFn
            Dim chkBool As Boolean = objRst.fnReg(STU_AUTHORITY.UsrID, alRst, alCmt)  ''' 결과등록 


            '20210312 JHS WBC DIFFCOUNT 검사 내용 로그로 남기기
            If alRstLog.Count > 0 Then
                Dim alRstLogtotal As New ArrayList

                Dim testinfo As TESTINFO_LOG = CType(alRstLog(0), TESTINFO_LOG)
                Dim bcnoDt As DataTable = objRst.fnget_LR010M_log(testinfo.BCNO.ToString)


                alRstLogtotal = fnReg_log_info_after(bcnoDt, alRstLog, rsRstflg) ' 저장 후

                Fn.log(alRstLogtotal)
            End If
            '------------------------------------------------

            Return chkBool

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return False
        Finally
            mbLeveCellGbn = True
            msWbcCount = False
        End Try

    End Function
    '20210312 jhs wbc diff count 로그 남기기 위해 만든 함수
    Public Function fnReg_log_info_before(ByVal rsRstflg As String, ByVal ProcessNum As String) As ArrayList
        Try
            Dim alTestInfoList As New ArrayList
            Dim testcd As String = ""
            With Me.spdResult
                For ix = 0 To .MaxRows - 1
                    Dim tmptestinfo_log As New TESTINFO_LOG
                    .Row = ix
                    .Col = .GetColFromID("testcd")
                    Dim test As String = .Text
                    If test = "LH103" Or test = "LH104" Or test = "LH105" Or test = "LH106" Or test = "LH107" Or test = "LH108" Or test = "LH108" Or test = "LH110" Or test = "LH111" Or test = "LH112" Or test = "LH12101" Or test = "LH12102" Or test = "LH12103" Or test = "LH12104" Or test = "LH12105" Or test = "LH12106" Then
                        .Col = .GetColFromID("bcno") : tmptestinfo_log.BCNO = .Text
                        .Col = .GetColFromID("testcd") : tmptestinfo_log.TESTCD = .Text
                        .Col = .GetColFromID("spccd") : tmptestinfo_log.SPCCD = .Text
                        .Col = .GetColFromID("tnmd") : tmptestinfo_log.TNMD = .Text
                        .Col = .GetColFromID("slipcd") : tmptestinfo_log.PARTCD = .Text.Substring(0, 1)
                        .Col = .GetColFromID("slipcd") : tmptestinfo_log.SLIPCD = .Text.Substring(1, 1)
                        .Col = .GetColFromID("viewrst") : tmptestinfo_log.VIEWRST = .Text
                        .Col = .GetColFromID("MWID") : tmptestinfo_log.MWID = .Text
                        .Col = .GetColFromID("MWDT") : tmptestinfo_log.MWDT = .Text
                        .Col = .GetColFromID("FNID") : tmptestinfo_log.FNID = .Text
                        .Col = .GetColFromID("FNDT") : tmptestinfo_log.FNDT = .Text
                        If rsRstflg = "22" Then
                            tmptestinfo_log.CHKMW = True
                        Else
                            tmptestinfo_log.CHKMW = False
                        End If
                        tmptestinfo_log.ProcessNum = ProcessNum

                        alTestInfoList.Add(tmptestinfo_log)
                    End If
                Next
            End With

            Return alTestInfoList
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
        End Try
    End Function
    '20220127 jhs WBCCount 100인지 체크하는 함수
    Public Function fnChk_WBCCount() As Double
        'WBCCount 가 딱 100인지 체크하는 함수
        Try
            Dim sChkCount As Double = 0
            With Me.spdResult
                For ix = 0 To .MaxRows - 1
                    Dim tmptestinfo_log As New TESTINFO_LOG
                    .Row = ix
                    .Col = .GetColFromID("testcd")
                        Dim test As String = .Text
                    If test.StartsWith("LH121") Then
                        msWbcCount = True
                        .Col = .GetColFromID("viewrst")
                        Dim rsSpdNum As Double
                        If Double.TryParse(.Text, rsSpdNum) Then
                            sChkCount += rsSpdNum
                        End If
                    End If
                Next
            End With

            Return Math.Round(sChkCount, 3)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
        End Try
    End Function
    '--------------------------------------------------
    Public Function fnReg_log_info_after(ByVal dt As DataTable, ByVal alRstLog As ArrayList, ByVal rsRstflg As String) As ArrayList
        Try
            With Me.spdResult
                For ix = 0 To dt.Rows.Count - 1
                    Dim tmptestinfo_log As New TESTINFO_LOG
                    .Row = ix
                    tmptestinfo_log.BCNO = dt.Rows(ix).Item("bcno").ToString
                    tmptestinfo_log.TESTCD = dt.Rows(ix).Item("testcd").ToString
                    tmptestinfo_log.SPCCD = dt.Rows(ix).Item("spccd").ToString
                    tmptestinfo_log.TNMD = dt.Rows(ix).Item("tnmd").ToString
                    tmptestinfo_log.PARTCD = dt.Rows(ix).Item("partcd").ToString
                    tmptestinfo_log.SLIPCD = dt.Rows(ix).Item("slipcd").ToString
                    tmptestinfo_log.VIEWRST = dt.Rows(ix).Item("viewrst").ToString
                    tmptestinfo_log.MWID = dt.Rows(ix).Item("mwid").ToString
                    tmptestinfo_log.MWDT = dt.Rows(ix).Item("mwdt").ToString
                    tmptestinfo_log.FNID = dt.Rows(ix).Item("fnid").ToString
                    tmptestinfo_log.FNDT = dt.Rows(ix).Item("fndt").ToString
                    If rsRstflg = "22" Then
                        tmptestinfo_log.CHKMW = True
                    Else
                        tmptestinfo_log.CHKMW = False
                    End If
                    tmptestinfo_log.ProcessNum = "2" ' 저장 후 

                    alRstLog.Add(tmptestinfo_log)
                Next
            End With

            Return alRstLog
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
        End Try
    End Function
    '-----------------------------------------------------------

    Public Function fnReg(ByVal rsRstflg As String, ByVal raRstVal As ArrayList, Optional ByVal rsCfmNm As String = "", Optional ByVal rsCfmSign As String = "") As Boolean
        '-- 검사항목별 결과 저장

        Dim aryReturn As New ArrayList
        Dim arySql As New ArrayList
        Dim aryTSql As New ArrayList

        Dim arlRst As New ArrayList
        Dim arlCmt As New ArrayList
        Dim strMsg As String = ""

        Try
            mbLeveCellGbn = False

            sbGet_Alert_Rule()

            If fnFind_Diff_ABO_Type() Then Return False

            sbResult_Setting(raRstVal)

            sbDisplay_Update()
            sbSet_JudgRst()

            If mbBatchMode = False Then
                Dim strBcNo As String = ""
                Dim strBcNo_t As String = ""

                With spdResult
                    For intRow As Integer = 1 To spdResult.MaxRows
                        .Row = intRow
                        .Col = .GetColFromID("bcno") : strBcNo_t = .Text.Replace("-", "")
                        If strBcNo_t <> strBcNo Then
                            If strBcNo <> "" Then
                                sbGet_CvtCmtInfo(strBcNo, True)
                            End If
                        End If
                        strBcNo = strBcNo_t
                    Next
                End With

                'sbGet_CvtCmtInfo(strBcNo, True)
                sbGet_CvtCmtInfo_TestCd(strBcNo, True)
            End If

            Me.txtCmtCont_LostFocus(Nothing, Nothing)

            Dim arlCmtCont As New ArrayList

            aryReturn = fnChecakReg(rsRstflg, arlCmtCont)
            If aryReturn.Count > 0 Then
                For intIdx As Integer = 0 To aryReturn.Count - 1
                    strMsg += aryReturn.Item(intIdx).ToString + vbCrLf
                Next

                'MsgBox(strMsg + vbCrLf + "위 자료는 결과를 저장할 수 없습니다.", MsgBoxStyle.Information)
            End If

            If arlCmtCont.Count > 0 Then
                For intIdx As Integer = 0 To arlCmtCont.Count - 1
                    Dim frm As New FGFINAL_CMT

                    frm.msBcNo = CType(arlCmtCont.Item(intIdx), CMT_INFO).BcNo
                    frm.msPartSlip = CType(arlCmtCont.Item(intIdx), CMT_INFO).PartSlip
                    frm.msCmt = CType(arlCmtCont.Item(intIdx), CMT_INFO).CmtCont

                    Dim strRet As String = frm.Display_Result()

                    If strRet <> "OK" Then Return False

                    For ix As Integer = 0 To cboBcNos.Items.Count - 1
                        Dim sBcno_Full As String = Fn.BCNO_View(CType(arlCmtCont.Item(intIdx), CMT_INFO).BcNo, True)
                        cboBcNos.SelectedIndex = ix
                        If cboBcNos.Text = sBcno_Full Then Exit For
                    Next

                    For ix As Integer = 0 To cboSlip.Items.Count - 1
                        cboSlip.SelectedIndex = ix
                        If cboSlip.Text.StartsWith("[" + CType(arlCmtCont.Item(intIdx), CMT_INFO).PartSlip + "]") Then Exit For
                    Next

                    If Me.txtCmtCont.Text.IndexOf(CType(arlCmtCont.Item(intIdx), CMT_INFO).CmtCont) < 0 Then
                        If txtCmtCont.Text <> "" Then
                            Me.txtCmtCont.Text += vbCrLf + CType(arlCmtCont.Item(intIdx), CMT_INFO).CmtCont
                        Else
                            Me.txtCmtCont.Text += CType(arlCmtCont.Item(intIdx), CMT_INFO).CmtCont
                        End If

                        Dim ci As New CMT_INFO
                        With ci
                            .BcNo = CType(arlCmtCont.Item(intIdx), CMT_INFO).BcNo
                            .PartSlip = CType(arlCmtCont.Item(intIdx), CMT_INFO).PartSlip
                            .CmtCont = Me.txtCmtCont.Text
                        End With
                        sbSet_Cmt_BcNo_Edit(ci)
                    End If
                Next
            End If

            arlRst = fnGetRst(rsRstflg, rsCfmNm, rsCfmSign)

            Dim a_dr As DataRow()
            a_dr = m_dt_Cmt_bcno.Select("status <> 'S'")

            If a_dr.Length > 0 Then

                For ix As Integer = 0 To a_dr.Length - 1
                    Dim arlBuf() As String

                    'arlBuf = a_dr(ix).Item("cmt").ToString.Replace(Chr(10), "").Split(Chr(13)) 'ori
                    arlBuf = a_dr(ix).Item("cmtcont").ToString.Replace(Chr(10), "").Split(Chr(13))

                    For ix2 As Integer = 0 To arlBuf.Length - 1
                        Dim objBR As New ResultInfo_Cmt
                        objBR.BcNo = a_dr(ix).Item("bcno").ToString
                        objBR.PartSlip = a_dr(ix).Item("partslip").ToString
                        objBR.TestCd = ""

                        objBR.RstSeq = Convert.ToString(ix2).PadLeft(2, "0"c)
                        objBR.Cmt = arlBuf(ix2)
                        objBR.RstFlg = rsRstflg

                        arlCmt.Add(objBR)
                    Next
                Next
            End If

            Dim objRst As New LISAPP.APP_R.AxRstFn

            Return objRst.fnReg(STU_AUTHORITY.UsrID, arlRst, arlCmt)

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Information)
            fnReg = False
        Finally
            mbLeveCellGbn = True
        End Try

    End Function

    Public Sub sbDisplay_Init(ByVal rsType As String)

        Me.spdResult.TextTip = FPSpreadADO.TextTipConstants.TextTipFloating

        Me.lstEx.Items.Clear()

        If rsType = "ALL" Then
            Me.spdResult.MaxRows = 0

            Me.txtCmtCont.Text = ""
            Me.txtCmtCont.Tag = ""
            Me.lblABO.Text = ""
            Me.lblABO_bf.Text = ""

            Me.lstCode.Hide()
            Me.pnlCode.Visible = False
        End If

        Me.chkSelect.Checked = False
        Me.txtBcNo.Text = ""

        '결과상태, 결과저장, 중간보고, 최종보고
        Me.lblSampleStatus.Text = ""
        Me.lblReg.Text = ""
        Me.lblMW.Text = ""
        Me.lblFN.Text = ""
        Me.lblCfm.Text = ""

        Me.txtOrgRst.Text = ""
        Me.txtTestCd.Text = ""

        Me.txtOrgRst.Visible = False
        Me.txtTestCd.Visible = False
        Me.txtBcNo.Visible = False
#If DEBUG Then
        Me.txtOrgRst.Visible = True
        Me.txtTestCd.Visible = True
        Me.txtBcNo.Visible = True
#End If
        m_al_Slip_bcno.Clear()
        Me.cboSlip.Items.Clear()
        Me.cboBcNos.Items.Clear()

    End Sub

    Public Sub sbDisplay_Data(ByVal rsBcNo As String, Optional ByVal rbBatch_Mode As Boolean = False)
        msBcNo = rsBcNo
        mbBatchMode = rbBatch_Mode

        Dim dt As New DataTable

        Try
            mbQueryView = True
            sbDisplay_Init("ALL")
            sbDisplay_RegNm(rsBcNo.Substring(0, 14))
            sbDisplay_Result(rsBcNo, False)

            sbGet_Alert_Rule()

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        Finally
            mbQueryView = False
        End Try
    End Sub

    Public Sub sbDisplay_RegNm(ByVal rsBcNo As String)
        Dim sFn As String = "Sub sbDisplay_RegNm()"

        Try
            Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_RstUsrInfo(rsBcNo)

            Dim sID As String = ""
            Dim sNM As String = ""
            Dim sDT As String = ""

            sbDisplay_Init("")

            Dim a_dr As DataRow()

            a_dr = dt.Select("rstflg >= '1'", "regdt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("regid").ToString().Trim
                sNM = a_dr(i - 1).Item("regnm").ToString().Trim
                sDT = a_dr(i - 1).Item("regdt").ToString().Trim

                If Not sID + sNM + sDT = "" Then
                    Me.lblSampleStatus.Text = "검사"
                    'Me.lblReg.Text = sDT + vbCrLf + sNM
                    Me.lblReg.Text = sDT + " / " + sNM
                    Exit For
                End If
            Next

            a_dr = dt.Select("rstflg >= '2'", "mwdt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("mwid").ToString().Trim
                sNM = a_dr(i - 1).Item("mwnm").ToString().Trim
                sDT = a_dr(i - 1).Item("mwdt").ToString().Trim

                If Not sID + sNM + sDT = "" Then
                    Me.lblSampleStatus.Text = "Review"
                    'Me.lblMW.Text = sDT + vbCrLf + sNM
                    Me.lblMW.Text = sDT + " / " + sNM

                    Exit For
                End If
            Next

            a_dr = dt.Select("rstflg = '3'", "fndt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("fnid").ToString().Trim
                sNM = a_dr(i - 1).Item("fnnm").ToString().Trim
                sDT = a_dr(i - 1).Item("fndt").ToString().Trim

                If Not sID + sNM + sDT = "" Then
                    If msFnDt = "" Then
                        Me.lblSampleStatus.Text = "예비보고"
                    Else
                        Me.lblSampleStatus.Text = "결과완료"
                    End If

                    'Me.lblFN.Text = sDT + vbCrLf + sNM
                    Me.lblFN.Text = sDT + " / " + sNM

                    Me.lblCfm.Text = a_dr(i - 1).Item("cfmnm").ToString().Trim   '-- 확인의
                    Exit For
                End If
            Next

            If Me.lblSampleStatus.Text = "결과완료" Then
                Me.lblSampleStatus.BackColor = Drawing.Color.FromArgb(128, 128, 255)
                Me.lblSampleStatus.ForeColor = Drawing.Color.White
            Else
                Me.lblSampleStatus.BackColor = Drawing.Color.FromArgb(255, 192, 128)
                Me.lblSampleStatus.ForeColor = Drawing.Color.Black
            End If

            m_dt_RstUsr = dt.Copy


        Catch ex As Exception

            sbLog_Exception(sFn + ":" + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_XPertCmt(ByVal rsBcno As String)
        Dim dt As DataTable

        dt = LISAPP.COMM.RstFn.fnGet_Xpert_Comment(rsBcno)
        Dim sCmtCont1 As String = ""
        Dim sCmtCont2 As String = ""
        Dim sViewrst As String = ""


        If dt.Rows.Count > 0 Then
            'If msXpertC = True Then
            '    sCmtCont1 += "*Critical value 통보 [소속: /피통보자:]" + vbCrLf + vbCrLf
            'End If
            sCmtCont2 += "*최근 1주일 결핵균 및 리팜핀 내성 [Xpert PCR] 검사 결과" + vbCrLf + vbCrLf
            sCmtCont2 += "검사시행날짜" + Space(8) + "검체번호" + Space(17) + "검사결과" + vbCrLf

            For ix = 1 To dt.Rows.Count
                If ix <> 1 Then sCmtCont2 += vbCrLf
                If dt.Rows(ix - 1).Item("viewrst").ToString.IndexOf(Chr(13)) > 0 Then
                    Dim sViewrst1 As String = dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))(0)
                    Dim sViewrst2 As String = dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))(1)
                    sViewrst = sViewrst1.Replace(Chr(10), "") + " " + sViewrst2.Replace(Chr(10), "")
                Else
                    sViewrst = dt.Rows(ix - 1).Item("viewrst").ToString
                End If

                sCmtCont2 += dt.Rows(ix - 1).Item("fndt").ToString + Space(10) + dt.Rows(ix - 1).Item("bcno").ToString + Space(10) + sViewrst

            Next
        ElseIf dt.Rows.Count <= 0 Then
            'If msXpertC = True Then
            '    sCmtCont1 += "*Critical value 통보 [소속: /피통보자:]" + vbCrLf + vbCrLf
            'End If
            sCmtCont2 += "*최근 1주일 결핵균 및 리팜핀 내성 [Xpert PCR] 검사 결과 : 검사이력 없음" + vbCrLf
        End If

        'If sCmtCont1 <> "" Then
        '    If Me.txtCmtCont.Text.Trim.Contains("*Critical value 통보") = False Then
        '        If Me.txtCmtCont.Text.Trim.Contains("*최근 1주일 결핵균 및 리팜핀 내성 [Xpert PCR]") = False Then
        '            Me.txtCmtCont.Text += sCmtCont1 + vbCrLf
        '        Else
        '            Me.txtCmtCont.Text = sCmtCont1 + vbCrLf + Me.txtCmtCont.Text + vbCrLf
        '        End If

        '    End If
        'End If

        If sCmtCont2 <> "" Then
            If Me.txtCmtCont.Text.Trim.Contains("*최근 1주일 결핵균 및 리팜핀 내성 [Xpert PCR]") = False Then
                If Me.txtCmtCont.Text <> "" Then
                    Me.txtCmtCont.Text += vbCrLf + vbCrLf + sCmtCont2 + vbCrLf
                Else
                    Me.txtCmtCont.Text += sCmtCont2 + vbCrLf
                End If

            End If
        End If


        'If sCmtCont <> "" Then
        '    If Me.txtCmtCont.Text = "" Then
        '        Me.txtCmtCont.Text += sCmtCont
        '    Else
        '        If Me.txtCmtCont.Text.Trim.Contains("*Critical value 통보") = False Then
        '            If Me.txtCmtCont.Text.Substring(Me.txtCmtCont.Text.Length - 1) = vbCrLf Then
        '                Me.txtCmtCont.Text += sCmtCont
        '            Else
        '                Me.txtCmtCont.Text += vbCrLf + sCmtCont
        '            End If
        '        End If

        '        If Me.txtCmtCont.Text.Trim.Contains("*최근 1주일 결핵균 및 리팜핀 내성 [Xpert PCR]") = False Then '중복내용있는지 체크
        '            If Me.txtCmtCont.Text.Substring(Me.txtCmtCont.Text.Length - 1) = vbCrLf Then
        '                Me.txtCmtCont.Text += sCmtCont
        '            Else
        '                Me.txtCmtCont.Text += vbCrLf + sCmtCont
        '            End If
        '        End If
        '    End If
        'End If

    End Sub
    Private Sub sbDisplay_Result(ByVal rsBcNo As String, ByVal rbAddFlg As Boolean)
        Dim sFn As String = "Sub sbDisplay_Result(string, boolean)"

        Dim dt As New DataTable

        Try
            '-- 검사결과
            dt = LISAPP.COMM.RstFn.fnGet_Result_bcno(rsBcNo, msPartSlip, Me.chkBcnoAll.Checked, msTestCds, msWkGrpCd, msEqCd)
            sbDisplay_ResultViewAdd(dt)

            dt = LISAPP.COMM.RstFn.fnGet_Rst_Comment_slip(rsBcNo)
          
            If dt.Rows.Count < 1 Then
                dt = LISAPP.COMM.RstFn.fnGet_Rst_Comment_test(rsBcNo)
            End If
            m_dt_Cmt_bcno = dt

            If Me.cboBcNos.Items.Count > 0 Then Me.cboBcNos.SelectedIndex = 0

            'sbDisplay_CommentViewAdd(rsBcNo, dt)

            '-- 검사항목별 결과코드 help
            m_dt_RstCdHelp = LISAPP.COMM.RstFn.fnGet_test_rstinfo(rsBcNo, Nothing)

            sbGet_CvtRstInfo(rsBcNo)    '-- 결과값 자동변환
            sbGet_Calc_Rst(0)           '-- 계산식결과 표시

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("orgrst")
                    If Not .Lock Then
                        .ForeColor = Drawing.Color.Black
                        .SetActiveCell(.Col, .Row)
                        .Focus()
                        Exit For
                    End If
                Next
            End With

            If mbBatchMode Then
                Me.axCalcRst.SEXAGE = ""
                Me.axCalcRst.BcNo = ""
            Else
                Me.axCalcRst.SEXAGE = msSexAge
                Me.axCalcRst.BcNo = rsBcNo
            End If

        Catch ex As Exception
            sbLog_Exception(sFn + ":" + ex.Message)
        End Try

    End Sub

    Protected Sub sbDisplay_ResultView(ByVal r_dt As DataTable, Optional ByRef rbRstflgNotFN As Boolean = False)
        Dim sFn As String = "Protected Sub Display_ResultView(DataTable)"

        Try
            mbLeveCellGbn = False

            Dim sRst_abo$ = "", sRst_rh$ = "", sRst_abo_bf$ = "", sRst_rh_bf$ = ""

            With Me.spdResult
                .MaxRows = 0

                .ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For ix As Integer = 1 To r_dt.Rows.Count

                    Dim sBcNo_full As String = Fn.BCNO_View(r_dt.Rows(ix - 1).Item("bcno").ToString, True)

                    If cboBcNos.Items.Contains(sBcNo_full) Then
                    Else
                        cboBcNos.Items.Add(sBcNo_full)
                    End If

                    If r_dt.Rows(ix - 1).Item("bcno").ToString = msBcNo And Not m_al_Slip_bcno.Contains(r_dt.Rows(ix - 1).Item("slipcd").ToString) Then
                        m_al_Slip_bcno.Add(r_dt.Rows(ix - 1).Item("slipcd").ToString)
                    End If

                    .Row = ix
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix - 1).Item("bcno").ToString().Trim           '30
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix - 1).Item("testcd").ToString().Trim         '27
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix - 1).Item("spccd").ToString().Trim          '28
                    .Col = .GetColFromID("slipcd") : .Text = r_dt.Rows(ix - 1).Item("slipcd").ToString().Trim
                    .Col = .GetColFromID("tclscd") : .Text = r_dt.Rows(ix - 1).Item("tclscd").ToString().Trim         '38
                    .Col = .GetColFromID("regnm") : .Text = r_dt.Rows(ix - 1).Item("regnm").ToString().Trim           '32
                    .Col = .GetColFromID("regid") : .Text = r_dt.Rows(ix - 1).Item("regid").ToString().Trim           '33
                    .Col = .GetColFromID("mwnm") : .Text = r_dt.Rows(ix - 1).Item("mwnm").ToString().Trim            '35
                    .Col = .GetColFromID("mwid") : .Text = r_dt.Rows(ix - 1).Item("mwid").ToString().Trim            '34
                    .Col = .GetColFromID("fnnm") : .Text = r_dt.Rows(ix - 1).Item("fnnm").ToString().Trim            '37
                    .Col = .GetColFromID("fnid") : .Text = r_dt.Rows(ix - 1).Item("fnid").ToString().Trim            '36
                    .Col = .GetColFromID("bbttype") : .Text = r_dt.Rows(ix - 1).Item("bbttype").ToString().Trim         '40 
                    .Col = .GetColFromID("bldgbn") : .Text = r_dt.Rows(ix - 1).Item("bldgbn").ToString().Trim '41
                    .Col = .GetColFromID("reqsub") : .Text = r_dt.Rows(ix - 1).Item("reqsub").ToString().Trim        '45
                    .Col = .GetColFromID("rsttype") : .Text = r_dt.Rows(ix - 1).Item("rsttype").ToString().Trim      '46
                    .Col = .GetColFromID("rstllen") : .Text = r_dt.Rows(ix - 1).Item("rstllen").ToString().Trim     '47
                    .Col = .GetColFromID("rstulen") : .Text = r_dt.Rows(ix - 1).Item("rstulen").ToString().Trim     '47
                    .Col = .GetColFromID("cutopt") : .Text = r_dt.Rows(ix - 1).Item("cutopt").ToString().Trim       '48
                    .Col = .GetColFromID("rerunflg") : .Text = r_dt.Rows(ix - 1).Item("rerunflg").ToString().Trim     '7
                    .Col = .GetColFromID("rstunit") : .Text = r_dt.Rows(ix - 1).Item("rstunit").ToString().Trim      '12
                    .Col = .GetColFromID("judgtype") : .Text = r_dt.Rows(ix - 1).Item("judgtype").ToString().Trim    '49
                    .Col = .GetColFromID("ujudglt1") : .Text = r_dt.Rows(ix - 1).Item("ujudglt1").ToString().Trim    '50
                    .Col = .GetColFromID("ujudglt2") : .Text = r_dt.Rows(ix - 1).Item("ujudglt2").ToString().Trim    '51
                    .Col = .GetColFromID("ujudglt3") : .Text = r_dt.Rows(ix - 1).Item("ujudglt3").ToString().Trim    '52
                    .Col = .GetColFromID("refgbn") : .Text = r_dt.Rows(ix - 1).Item("refgbn").ToString().Trim        '53
                    .Col = .GetColFromID("refls") : .Text = r_dt.Rows(ix - 1).Item("refls").ToString().Trim          '54
                    .Col = .GetColFromID("refhs") : .Text = r_dt.Rows(ix - 1).Item("refhs").ToString().Trim         '55
                    .Col = .GetColFromID("refl") : .Text = r_dt.Rows(ix - 1).Item("refl").ToString().Trim          '56
                    .Col = .GetColFromID("refh") : .Text = r_dt.Rows(ix - 1).Item("refh").ToString().Trim           '57
                    .Col = .GetColFromID("alimitgbn") : .Text = r_dt.Rows(ix - 1).Item("alimitgbn").ToString().Trim   '58
                    .Col = .GetColFromID("alimitl") : .Text = r_dt.Rows(ix - 1).Item("alimitl").ToString().Trim     '59
                    .Col = .GetColFromID("alimitls") : .Text = r_dt.Rows(ix - 1).Item("alimitls").ToString().Trim     '60
                    .Col = .GetColFromID("alimith") : .Text = r_dt.Rows(ix - 1).Item("alimith").ToString().Trim     '61
                    .Col = .GetColFromID("alimiths") : .Text = r_dt.Rows(ix - 1).Item("alimiths").ToString().Trim     '62
                    .Col = .GetColFromID("panicgbn") : .Text = r_dt.Rows(ix - 1).Item("panicgbn").ToString().Trim     '63
                    .Col = .GetColFromID("panicl") : .Text = r_dt.Rows(ix - 1).Item("panicl").ToString().Trim       '64
                    .Col = .GetColFromID("panich") : .Text = r_dt.Rows(ix - 1).Item("panich").ToString().Trim        '65
                    .Col = .GetColFromID("criticalgbn") : .Text = r_dt.Rows(ix - 1).Item("criticalgbn").ToString().Trim   '66
                    .Col = .GetColFromID("criticall") : .Text = r_dt.Rows(ix - 1).Item("criticall").ToString().Trim      '67
                    .Col = .GetColFromID("criticalh") : .Text = r_dt.Rows(ix - 1).Item("criticalh").ToString().Trim      '68
                    .Col = .GetColFromID("alertgbn") : .Text = r_dt.Rows(ix - 1).Item("alertgbn").ToString().Trim       '69
                    .Col = .GetColFromID("alertl") : .Text = r_dt.Rows(ix - 1).Item("alertl").ToString().Trim         '70
                    .Col = .GetColFromID("alerth") : .Text = r_dt.Rows(ix - 1).Item("alerth").ToString().Trim          '71
                    .Col = .GetColFromID("deltagbn") : .Text = r_dt.Rows(ix - 1).Item("deltagbn").ToString().Trim       '72
                    .Col = .GetColFromID("deltal") : .Text = r_dt.Rows(ix - 1).Item("deltal").ToString().Trim         '73
                    .Col = .GetColFromID("deltah") : .Text = r_dt.Rows(ix - 1).Item("deltah").ToString().Trim         '74
                    .Col = .GetColFromID("deltaday") : .Text = r_dt.Rows(ix - 1).Item("deltaday").ToString().Trim        '75
                    .Col = .GetColFromID("bfbcno1") : .Text = r_dt.Rows(ix - 1).Item("bfbcno1").ToString().Trim       '76
                    .Col = .GetColFromID("bforgrst1") : .Text = r_dt.Rows(ix - 1).Item("bforgrst1").ToString().Trim     '77
                    .Col = .GetColFromID("bfviewrst1") : .Text = r_dt.Rows(ix - 1).Item("bfviewrst1").ToString().Trim     '78
                    .Col = .GetColFromID("bffndt1") : .Text = r_dt.Rows(ix - 1).Item("bffndt1").ToString().Trim       '79
                    .Col = .GetColFromID("bfbcno2") : .Text = r_dt.Rows(ix - 1).Item("bfbcno2").ToString().Trim         '24
                    .Col = .GetColFromID("bfviewrst2") : .Text = r_dt.Rows(ix - 1).Item("bfviewrst2").ToString().Trim    '25
                    .Col = .GetColFromID("bffndt2") : .Text = r_dt.Rows(ix - 1).Item("bffndt2").ToString().Trim          '26
                    .Col = .GetColFromID("eqcd") : .Text = r_dt.Rows(ix - 1).Item("eqcd").ToString().Trim          '21
                    .Col = .GetColFromID("eqnm") : .Text = r_dt.Rows(ix - 1).Item("eqnm").ToString().Trim             '22
                    .Col = .GetColFromID("eqbcno") : .Text = r_dt.Rows(ix - 1).Item("eqbcno").ToString().Trim          '23
                    .Col = .GetColFromID("tnmp") : .Text = r_dt.Rows(ix - 1).Item("tnmp").ToString().Trim            '80
                    .Col = .GetColFromID("tordcd") : .Text = r_dt.Rows(ix - 1).Item("tordcd").ToString().Trim           '29
                    .Col = .GetColFromID("calcgbn") : .Text = r_dt.Rows(ix - 1).Item("calcgbn").ToString().Trim          '85
                    .Col = .GetColFromID("viwsub") : .Text = r_dt.Rows(ix - 1).Item("viwsub").ToString().Trim          '86
                    .Col = .GetColFromID("rerunrst") : .Text = r_dt.Rows(ix - 1).Item("rerunrst").ToString().Trim
                    .Col = .GetColFromID("cfmnm") : .Text = r_dt.Rows(ix - 1).Item("cfmnm").ToString().Trim

                    .Col = .GetColFromID("reftxt") : .Text = r_dt.Rows(ix - 1).Item("reftxt").ToString().Trim            '11
                    If r_dt.Rows(ix - 1).Item("reftxt").ToString().Trim <> "" Then
                        .CellNoteIndicator = FPSpreadADO.CellNoteIndicatorConstants.CellNoteIndicatorShowAndFireEvent
                    End If
                    .Col = .GetColFromID("rstflg") : .Text = r_dt.Rows(ix - 1).Item("rstflg").ToString().Trim           '31
                    .Col = .GetColFromID("rstflgmark")
                    '18
                    Select Case r_dt.Rows(ix - 1).Item("rstflg").ToString().Trim
                        Case "3"    ' 최종결과 표시
                            .ForeColor = Drawing.Color.DarkGreen
                            .Text = "◆"

                        Case "2"    ' 중간보고 표시
                            .Text = "○"
                            rbRstflgNotFN = True
                        Case "1"
                            .Text = "△"
                            rbRstflgNotFN = True
                        Case Else
                            .Text = ""
                    End Select

                    .Col = .GetColFromID("titleyn")
                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" Or r_dt.Rows(ix - 1).Item("bldgbn").ToString().Trim = "1" Then
                        .Col = .GetColFromID("orgrst")
                        .Lock = True
                        If r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim <> "" And r_dt.Rows(ix - 1).Item("bldgbn").ToString().Trim = "1" Then
                            .BackColor = Drawing.Color.LightPink
                            .ForeColor = Drawing.Color.LightPink
                        Else
                            .BackColor = Drawing.Color.LightGray
                            .ForeColor = Drawing.Color.LightGray
                        End If

                        .Col = .GetColFromID("viewrst")
                        If r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim <> "" And r_dt.Rows(ix - 1).Item("bldgbn").ToString().Trim = "1" Then
                            .BackColor = Drawing.Color.LightPink
                            .ForeColor = Drawing.Color.LightPink
                        Else
                            .BackColor = Drawing.Color.LightGray
                            .ForeColor = Drawing.Color.LightGray
                        End If
                        .Lock = True

                        '.Col = .GetColFromID("rerunrst")
                        '.BackColor = Drawing.Color.LightGray
                        '.ForeColor = Drawing.Color.LightGray
                        '.Lock = True
                    End If

                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" And r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "B" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""
                    End If

                    .Col = .GetColFromID("tcdgbn") : .Text = r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim

                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""

                        If r_dt.Rows(ix - 1).Item("viwsub").ToString.Trim = "0" And _
                           r_dt.Rows(ix - 1).Item("orgrst").ToString.Trim = "" And r_dt.Rows(ix - 1).Item("bforgrst1").ToString.Trim = "" Then
                            .Row = .MaxRows
                            .RowHidden = True
                        End If
                    End If

                    .Col = .GetColFromID("hlmark") : .Text = r_dt.Rows(ix - 1).Item("hlmark").ToString().Trim    '13
                    If r_dt.Rows(ix - 1).Item("hlmark").ToString() = "L" Then
                        .BackColor = Color.FromArgb(221, 240, 255)
                        .ForeColor = Color.FromArgb(0, 0, 255)
                    ElseIf r_dt.Rows(ix - 1).Item("hlmark").ToString() = "H" Then
                        .BackColor = Color.FromArgb(255, 230, 231)
                        .ForeColor = Color.FromArgb(255, 0, 0)
                    End If

                    .Col = .GetColFromID("panicmark") : .Text = r_dt.Rows(ix - 1).Item("panicmark").ToString().Trim   '14
                    If r_dt.Rows(ix - 1).Item("panicmark").ToString() = "P" Then
                        .BackColor = Color.FromArgb(150, 150, 255)
                        .ForeColor = Color.FromArgb(255, 255, 255)
                    End If

                    .Col = .GetColFromID("deltamark") : .Text = r_dt.Rows(ix - 1).Item("deltamark").ToString().Trim   '15
                    If r_dt.Rows(ix - 1).Item("deltamark").ToString() = "D" Then
                        .BackColor = Color.FromArgb(150, 255, 150)
                        .ForeColor = Color.FromArgb(0, 128, 64)
                    End If

                    .Col = .GetColFromID("criticalmark") : .Text = r_dt.Rows(ix - 1).Item("criticalmark").ToString().Trim     '16
                    If r_dt.Rows(ix - 1).Item("criticalmark").ToString() = "C" Then
                        .BackColor = Color.FromArgb(255, 150, 255)
                        .ForeColor = Color.FromArgb(255, 255, 255)
                    End If

                    .Col = .GetColFromID("alertmark") : .Text = r_dt.Rows(ix - 1).Item("alertmark").ToString().Trim           '17
                    If r_dt.Rows(ix - 1).Item("alertmark").ToString() <> "" Then
                        .BackColor = Color.FromArgb(255, 255, 150)
                        .ForeColor = Color.FromArgb(0, 0, 0)
                    End If

                    '-- 검사명 표시
                    .Col = .GetColFromID("tnmd")                                                                            '3
                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        If r_dt.Rows(ix - 1).Item("tclscd").ToString = r_dt.Rows(ix - 1).Item("testcd").ToString.Substring(1, 5) Then
                            .Text = "... " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                        Else
                            .Text = ".... " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                        End If
                    ElseIf r_dt.Rows(ix - 1).Item("tcdgbn").ToString.Trim = "B" Or _
                           r_dt.Rows(ix - 1).Item("testcd").ToString.Trim = r_dt.Rows(ix - 1).Item("tclscd").ToString.Trim Then
                        .Text = r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                    Else
                        .Text = ". " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                    End If

                    .Col = .GetColFromID("cvtfldgbn") : .Text = r_dt.Rows(ix - 1).Item("cvtfldgbn").ToString().Trim           '84
                    If r_dt.Rows(ix - 1).Item("cvtfldgbn").ToString().Trim <> "" Then                                        '9
                        .Col = .GetColFromID("cvtgbn") : .Text = "C"
                    Else
                        .Col = .GetColFromID("cvtgbn") : .Text = ""
                    End If

                    .Col = .GetColFromID("reftcls")                                                                         '10
                    If r_dt.Rows(ix - 1).Item("reftcls").ToString().Trim = "1" Then
                        .Col = .GetColFromID("reftcls") : .Text = "☞"
                    Else
                        .Col = .GetColFromID("reftcls") : .Text = ""
                    End If

                    .Col = .GetColFromID("rstno") : .Text = r_dt.Rows(ix - 1).Item("rstno").ToString().Trim                   '6

                    If r_dt.Rows(ix - 1).Item("rstno").ToString() > "1" Then                                            '8
                        .Col = .GetColFromID("history")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                        .TypePictStretch = False
                        .TypePictMaintainScale = False
                        .TypePictPicture = GetImgList.getMultiRst()
                    End If

                    .Col = .GetColFromID("orgrst")                                                                          '4
                    .Text = r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim
                    If r_dt.Rows(ix - 1).Item("bbttype").ToString <> "2" Then
                        .ForeColor = .BackColor
                    End If
                    .Col = .GetColFromID("corgrst") : .Text = r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim                '81

                    If (r_dt.Rows(ix - 1).Item("bcno").ToString <> msBcNo Or (LOGIN.PRG_CONST.RST_BCNO_EXE = "0" And msEqCd = "" And msPartSlip <> "" And r_dt.Rows(ix - 1).Item("slipcd").ToString <> msPartSlip) Or _
                       (msTestCds <> "" And (msTestCds + ",").IndexOf(r_dt.Rows(ix - 1).Item("testspc").ToString.Trim + ",") < 0)) And mbDoctorMode = False Then
                        .Col = .GetColFromID("orgrst")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .BackColor = Color.Silver
                        .ForeColor = Color.Silver

                        .Col = .GetColFromID("chk") : .Lock = True
                    End If

                    .Col = .GetColFromID("viewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim               '5
                    .Col = .GetColFromID("cviewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim             '82

                    .Col = .GetColFromID("rstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim                      '19
                    .Col = .GetColFromID("crstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim                      '83

                    .Col = .GetColFromID("eqflag")

                    If r_dt.Rows(ix - 1).Item("eqflag").ToString().Trim().Trim = "" Then
                    Else
                        .Text = r_dt.Rows(ix - 1).Item("eqflag").ToString()                '20 
                        .BackColor = Color.PaleVioletRed
                        .ForeColor = Color.White
                    End If

                    .Row = ix
                    If r_dt.Rows(ix - 1).Item("rstflg").ToString.Trim.Trim = "1" Or r_dt.Rows(ix - 1).Item("rstflg").ToString.Trim = "2" Then
                        .Col = .GetColFromID("chk")
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox And .Lock = False Then
                            .Text = "1"
                            .Col = .GetColFromID("iud") : .Text = "1"
                        ElseIf r_dt.Rows(ix - 1).Item("tcdgbn").ToString = "C" Then
                            .Col = .GetColFromID("iud") : .Text = "1"
                            For ix2 As Integer = ix - 1 To 1 Step -1
                                .Row = ix2
                                .Col = .GetColFromID("testcd")
                                If .Text = r_dt.Rows(ix - 1).Item("testcd").ToString.Substring(0, 5) Then
                                    .Col = .GetColFromID("chk") : .Text = "1"
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    If mbBloodBank Then
                        If msBlood_ABO_S = r_dt.Rows(ix - 1).Item("testcd").ToString.Trim Or msBlood_ABO_C = r_dt.Rows(ix - 1).Item("testcd").ToString.Trim Then
                            sRst_abo = r_dt.Rows(ix - 1).Item("viewrst").ToString.Trim
                            sRst_abo_bf = r_dt.Rows(ix - 1).Item("bfviewrst1").ToString.Trim
                        End If

                        If msBlood_Rh = r_dt.Rows(ix - 1).Item("testcd").ToString.Trim Then
                            sRst_rh = r_dt.Rows(ix - 1).Item("viewrst").ToString.Replace("(", "").Replace(")", "").Trim
                            sRst_rh_bf = r_dt.Rows(ix - 1).Item("bfviewrst1").ToString.Replace("(", "").Replace(")", "").Trim
                        End If
                    End If
                Next

                If mbBloodBank Then
                    Dim bFlg_abo As Boolean = False
                    Dim bFlg_rh As Boolean = False
                    Dim sRstFlg_abo As String = ""
                    Dim sRstFlg_rh As String = ""

                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("bldgbn") : Dim sBldGbn As String = .Text
                        .Col = .GetColFromID("bbttype") : Dim sBbtType As String = .Text
                        .Col = .GetColFromID("rstflg") : Dim sRstflg As String = .Text
                        .Col = .GetColFromID("regid") : Dim sRegid As String = .Text
                        .Col = .GetColFromID("mwid") : Dim sMwid As String = .Text

                        If sBldGbn.Substring(0, 1) = "1" Then
                            If sRstFlg_abo < sRstflg Then sRstFlg_abo = sRstflg
                        ElseIf sBldGbn.Substring(0, 1) = "2" Then
                            If sRstFlg_rh < sRstflg Then sRstFlg_rh = sRstflg
                        End If

                        If sRegid = STU_AUTHORITY.UsrID Or sMwid = STU_AUTHORITY.UsrID Or (sBldGbn = "11" And sRstflg = "3") Then
                            .Col = .GetColFromID("orgrst")
                            .Lock = False
                            .BackColor = Color.White
                            .ForeColor = Color.Black

                            .Col = .GetColFromID("viewrst")
                            .Lock = False
                            .BackColor = Color.White
                            .ForeColor = Color.Black

                            bFlg_abo = True

                        ElseIf sRegid = STU_AUTHORITY.UsrID Or sMwid = STU_AUTHORITY.UsrID Or (sBldGbn = "21" And sRstflg = "3") Then
                            .Col = .GetColFromID("orgrst")
                            .Lock = False
                            .BackColor = Color.White
                            .ForeColor = Color.Black

                            .Col = .GetColFromID("viewrst")
                            .Lock = False
                            .BackColor = Color.White
                            .ForeColor = Color.Black

                            bFlg_rh = True

                        End If
                    Next

                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("bldgbn") : Dim sBldGbn As String = .Text
                        .Col = .GetColFromID("bbttype") : Dim sBbtType As String = .Text

                        If sBbtType = "1" Then
                            If sRstFlg_abo > "0" And bFlg_abo = False And sBldGbn = "12" Then
                                .Col = .GetColFromID("orgrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                                .Col = .GetColFromID("viewrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black
                            ElseIf sRstFlg_rh > "0" And bFlg_abo = False And sBldGbn = "22" Then
                                .Col = .GetColFromID("orgrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                                .Col = .GetColFromID("viewrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                            ElseIf sRstFlg_abo = "" And bFlg_abo = False And sBldGbn = "11" Then
                                .Col = .GetColFromID("orgrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                                .Col = .GetColFromID("viewrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black
                            ElseIf sRstFlg_rh = "" And bFlg_rh = False And sBldGbn = "21" Then
                                .Col = .GetColFromID("orgrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                                .Col = .GetColFromID("viewrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black
                            End If

                        End If
                    Next

                    Me.lblABO.Text = sRst_abo + sRst_rh
                    Me.lblABO_bf.Text = sRst_abo_bf + sRst_rh_bf
                Else
                    Me.lblABO.Text = msAboRh
                End If


                .ReDraw = CType(IIf(mbBatchMode, False, True), Boolean)

                .Row = .MaxRows
                .Col = .GetColFromID("chk") : Dim strChk As String = .Text
                If strChk = "" And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Col = .GetColFromID("iud") : .Text = ""
                End If


            End With

        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)

        Finally
            Me.spdResult.ReDraw = CType(IIf(mbBatchMode, False, True), Boolean)
            mbLeveCellGbn = True

        End Try
    End Sub

    Protected Sub sbDisplay_ResultViewAdd(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub Display_ResultView(DataTable)"

        Dim sRst_abo$ = "", sRst_rh$ = "", sRst_abo_bf$ = "", sRst_rh_bf$ = ""


        Try
            With Me.spdResult
                .ReDraw = False
                For ix As Integer = 1 To r_dt.Rows.Count

                    Dim sBcNo_full As String = Fn.BCNO_View(r_dt.Rows(ix - 1).Item("bcno").ToString, True)

                    If cboBcNos.Items.Contains(sBcNo_full) Then
                    Else
                        cboBcNos.Items.Add(sBcNo_full)
                    End If

                    If r_dt.Rows(ix - 1).Item("bcno").ToString = msBcNo And Not m_al_Slip_bcno.Contains(r_dt.Rows(ix - 1).Item("slipcd").ToString) Then
                        m_al_Slip_bcno.Add(r_dt.Rows(ix - 1).Item("slipcd").ToString)
                    End If

                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix - 1).Item("bcno").ToString().Trim             '30
                    .Col = .GetColFromID("fnid") : .Text = r_dt.Rows(ix - 1).Item("fnid").ToString().Trim             '36
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix - 1).Item("testcd").ToString().Trim         '27
                    If r_dt.Rows(ix - 1).Item("testcd").ToString().Trim = "LG104" Then
                        msXpertTcd = True
                        If r_dt.Rows(ix - 1).Item("criticalmark").ToString.Trim = "C" Then
                            msXpertC = True
                        Else
                            msXpertC = False
                        End If
                    Else
                        msXpertTcd = False
                    End If
                    .Col = .GetColFromID("slipcd") : .Text = r_dt.Rows(ix - 1).Item("slipcd").ToString().Trim
                    .Col = .GetColFromID("tclscd") : .Text = r_dt.Rows(ix - 1).Item("tclscd").ToString().Trim        '38
                    .Col = .GetColFromID("regnm") : .Text = r_dt.Rows(ix - 1).Item("regnm").ToString().Trim          '32
                    .Col = .GetColFromID("regid") : .Text = r_dt.Rows(ix - 1).Item("regid").ToString().Trim          '33
                    .Col = .GetColFromID("mwnm") : .Text = r_dt.Rows(ix - 1).Item("mwnm").ToString().Trim            '35
                    .Col = .GetColFromID("mwid") : .Text = r_dt.Rows(ix - 1).Item("mwid").ToString().Trim            '34
                    .Col = .GetColFromID("fnnm") : .Text = r_dt.Rows(ix - 1).Item("fnnm").ToString().Trim           '37
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix - 1).Item("spccd").ToString().Trim          '28
                    .Col = .GetColFromID("bbttype") : .Text = r_dt.Rows(ix - 1).Item("bbttype").ToString().Trim          '40 
                    .Col = .GetColFromID("bldgbn") : .Text = r_dt.Rows(ix - 1).Item("bldgbn").ToString().Trim       '41
                    .Col = .GetColFromID("reqsub") : .Text = r_dt.Rows(ix - 1).Item("reqsub").ToString().Trim        '45
                    .Col = .GetColFromID("rsttype") : .Text = r_dt.Rows(ix - 1).Item("rsttype").ToString().Trim       '46
                    .Col = .GetColFromID("rstllen") : .Text = r_dt.Rows(ix - 1).Item("rstllen").ToString().Trim       '47
                    .Col = .GetColFromID("rstulen") : .Text = r_dt.Rows(ix - 1).Item("rstulen").ToString().Trim      '47
                    .Col = .GetColFromID("cutopt") : .Text = r_dt.Rows(ix - 1).Item("cutopt").ToString().Trim        '48
                    .Col = .GetColFromID("rerunflg") : .Text = r_dt.Rows(ix - 1).Item("rerunflg").ToString().Trim    '7
                    .Col = .GetColFromID("rstunit") : .Text = r_dt.Rows(ix - 1).Item("rstunit").ToString().Trim      '12
                    .Col = .GetColFromID("judgtype") : .Text = r_dt.Rows(ix - 1).Item("judgtype").ToString().Trim     '49
                    .Col = .GetColFromID("ujudglt1") : .Text = r_dt.Rows(ix - 1).Item("ujudglt1").ToString().Trim    '50
                    .Col = .GetColFromID("ujudglt2") : .Text = r_dt.Rows(ix - 1).Item("ujudglt2").ToString().Trim    '51
                    .Col = .GetColFromID("ujudglt3") : .Text = r_dt.Rows(ix - 1).Item("ujudglt3").ToString().Trim     '52
                    .Col = .GetColFromID("refgbn") : .Text = r_dt.Rows(ix - 1).Item("refgbn").ToString().Trim        '53
                    .Col = .GetColFromID("refls") : .Text = r_dt.Rows(ix - 1).Item("refls").ToString().Trim          '54
                    .Col = .GetColFromID("refhs") : .Text = r_dt.Rows(ix - 1).Item("refhs").ToString().Trim         '55
                    .Col = .GetColFromID("refl") : .Text = r_dt.Rows(ix - 1).Item("refl").ToString().Trim            '56
                    .Col = .GetColFromID("refh") : .Text = r_dt.Rows(ix - 1).Item("refh").ToString().Trim            '57
                    .Col = .GetColFromID("alimitgbn") : .Text = r_dt.Rows(ix - 1).Item("alimitgbn").ToString().Trim   '58
                    .Col = .GetColFromID("alimitl") : .Text = r_dt.Rows(ix - 1).Item("alimitl").ToString().Trim       '59
                    .Col = .GetColFromID("alimitls") : .Text = r_dt.Rows(ix - 1).Item("alimitls").ToString().Trim     '60
                    .Col = .GetColFromID("alimith") : .Text = r_dt.Rows(ix - 1).Item("alimith").ToString().Trim      '61
                    .Col = .GetColFromID("alimiths") : .Text = r_dt.Rows(ix - 1).Item("alimiths").ToString().Trim    '62
                    .Col = .GetColFromID("panicgbn") : .Text = r_dt.Rows(ix - 1).Item("panicgbn").ToString().Trim     '63
                    .Col = .GetColFromID("panicl") : .Text = r_dt.Rows(ix - 1).Item("panicl").ToString().Trim        '64
                    .Col = .GetColFromID("panich") : .Text = r_dt.Rows(ix - 1).Item("panich").ToString().Trim        '65
                    .Col = .GetColFromID("criticalgbn") : .Text = r_dt.Rows(ix - 1).Item("criticalgbn").ToString().Trim   '66
                    .Col = .GetColFromID("criticall") : .Text = r_dt.Rows(ix - 1).Item("criticall").ToString().Trim      '67
                    .Col = .GetColFromID("criticalh") : .Text = r_dt.Rows(ix - 1).Item("criticalh").ToString().Trim      '68
                    .Col = .GetColFromID("alertgbn") : .Text = r_dt.Rows(ix - 1).Item("alertgbn").ToString().Trim        '69
                    .Col = .GetColFromID("alertl") : .Text = r_dt.Rows(ix - 1).Item("alertl").ToString().Trim            '70
                    .Col = .GetColFromID("alerth") : .Text = r_dt.Rows(ix - 1).Item("alerth").ToString().Trim           '71
                    .Col = .GetColFromID("deltagbn") : .Text = r_dt.Rows(ix - 1).Item("deltagbn").ToString().Trim        '72
                    .Col = .GetColFromID("deltal") : .Text = r_dt.Rows(ix - 1).Item("deltal").ToString().Trim            '73
                    .Col = .GetColFromID("deltah") : .Text = r_dt.Rows(ix - 1).Item("deltah").ToString().Trim            '74
                    .Col = .GetColFromID("deltaday") : .Text = r_dt.Rows(ix - 1).Item("deltaday").ToString().Trim         '75
                    .Col = .GetColFromID("bfbcno1") : .Text = r_dt.Rows(ix - 1).Item("bfbcno1").ToString().Trim         '76
                    .Col = .GetColFromID("bforgrst1") : .Text = r_dt.Rows(ix - 1).Item("bforgrst1").ToString().Trim      '77
                    .Col = .GetColFromID("bfviewrst1") : .Text = r_dt.Rows(ix - 1).Item("bfviewrst1").ToString().Trim    '78
                    .Col = .GetColFromID("bffndt1") : .Text = r_dt.Rows(ix - 1).Item("bffndt1").ToString().Trim          '79
                    .Col = .GetColFromID("bfbcno2") : .Text = r_dt.Rows(ix - 1).Item("bfbcno2").ToString().Trim          '24
                    .Col = .GetColFromID("bfviewrst2") : .Text = r_dt.Rows(ix - 1).Item("bfviewrst2").ToString().Trim    '25
                    .Col = .GetColFromID("bffndt2") : .Text = r_dt.Rows(ix - 1).Item("bffndt2").ToString().Trim           '26
                    .Col = .GetColFromID("eqcd") : .Text = r_dt.Rows(ix - 1).Item("eqcd").ToString().Trim                 '21
                    .Col = .GetColFromID("eqnm") : .Text = r_dt.Rows(ix - 1).Item("eqnm").ToString().Trim             '22
                    .Col = .GetColFromID("eqbcno") : .Text = r_dt.Rows(ix - 1).Item("eqbcno").ToString().Trim           '23
                    .Col = .GetColFromID("tnmp") : .Text = r_dt.Rows(ix - 1).Item("tnmp").ToString().Trim             '80
                    .Col = .GetColFromID("tordcd") : .Text = r_dt.Rows(ix - 1).Item("tordcd").ToString().Trim           '29
                    .Col = .GetColFromID("calcgbn") : .Text = r_dt.Rows(ix - 1).Item("calcgbn").ToString().Trim          '85
                    .Col = .GetColFromID("viwsub") : .Text = r_dt.Rows(ix - 1).Item("viwsub").ToString().Trim             '86
                    .Col = .GetColFromID("rerunrst") : .Text = r_dt.Rows(ix - 1).Item("rerunrst").ToString().Trim
                    .Col = .GetColFromID("cfmnm") : .Text = r_dt.Rows(ix - 1).Item("cfmnm").ToString().Trim
                    '20210312 jhs 중간보고 최종보고일시 추가
                    .Col = .GetColFromID("mwdt") : .Text = r_dt.Rows(ix - 1).Item("mwdt").ToString().Trim
                    .Col = .GetColFromID("fndt") : .Text = r_dt.Rows(ix - 1).Item("fndt").ToString().Trim
                    .Col = .GetColFromID("rrptst") : .Text = r_dt.Rows(ix - 1).Item("rrptst").ToString().Trim
                    '---------------------------------------------

                    .Col = .GetColFromID("reftxt") : .Text = r_dt.Rows(ix - 1).Item("reftxt").ToString().Trim

                    If r_dt.Rows(ix - 1).Item("reftxt").ToString().Trim <> "" Then
                        .CellNoteIndicator = FPSpreadADO.CellNoteIndicatorConstants.CellNoteIndicatorShowAndFireEvent
                    End If

                    .Col = .GetColFromID("rstflg") : .Text = r_dt.Rows(ix - 1).Item("rstflg").ToString().Trim
                    .Col = .GetColFromID("rstflgmark")
                    Select Case r_dt.Rows(ix - 1).Item("rstflg").ToString().Trim
                        Case "3"    ' 최종결과 표시
                            .ForeColor = Drawing.Color.DarkGreen
                            .Text = "◆"

                        Case "2"    ' 중간보고 표시
                            .Text = "○"

                        Case "1"
                            .Text = "△"
                        Case Else
                            .Text = ""

                    End Select

                    .Col = .GetColFromID("titleyn") : .Text = r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim
                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" Or r_dt.Rows(ix - 1).Item("bbttype").ToString().Trim = "1" Then
                        .Col = .GetColFromID("orgrst")
                        .Lock = True
                        If r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim <> "" And r_dt.Rows(ix - 1).Item("bbttype").ToString().Trim = "1" Then
                            .BackColor = Drawing.Color.LightPink
                            .ForeColor = Drawing.Color.LightPink
                        Else
                            .BackColor = Drawing.Color.LightGray
                            .ForeColor = Drawing.Color.LightGray
                        End If

                        .Col = .GetColFromID("viewrst")
                        If r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim <> "" And r_dt.Rows(ix - 1).Item("bbttype").ToString().Trim = "1" Then
                            .BackColor = Drawing.Color.LightPink
                            .ForeColor = Drawing.Color.LightPink
                        Else
                            .BackColor = Drawing.Color.LightGray
                            .ForeColor = Drawing.Color.LightGray
                        End If
                        .Lock = True

                        '.Col = .GetColFromID("rerunrst")
                        '.BackColor = Drawing.Color.LightGray
                        '.ForeColor = Drawing.Color.LightGray
                        '.Lock = True
                    End If

                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" And r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "B" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""
                    End If

                    .Col = .GetColFromID("tcdgbn") : .Text = r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim

                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""

                        If r_dt.Rows(ix - 1).Item("viwsub").ToString.Trim = "0" And _
                           r_dt.Rows(ix - 1).Item("orgrst").ToString.Trim = "" And r_dt.Rows(ix - 1).Item("bforgrst1").ToString.Trim = "" Then
                            .Row = .MaxRows
                            .RowHidden = True
                        End If
                    End If


                    .Col = .GetColFromID("hlmark") : .Text = r_dt.Rows(ix - 1).Item("hlmark").ToString().Trim
                    If r_dt.Rows(ix - 1).Item("hlmark").ToString() = "L" Then
                        .BackColor = Color.FromArgb(221, 240, 255)
                        .ForeColor = Color.FromArgb(0, 0, 255)
                    ElseIf r_dt.Rows(ix - 1).Item("hlmark").ToString() = "H" Then
                        .BackColor = Color.FromArgb(255, 230, 231)
                        .ForeColor = Color.FromArgb(255, 0, 0)
                    End If

                    .Col = .GetColFromID("panicmark") : .Text = r_dt.Rows(ix - 1).Item("panicmark").ToString().Trim
                    If r_dt.Rows(ix - 1).Item("panicmark").ToString() = "P" Then
                        .BackColor = Color.FromArgb(150, 150, 255)
                        .ForeColor = Color.FromArgb(255, 255, 255)
                    End If

                    .Col = .GetColFromID("deltamark") : .Text = r_dt.Rows(ix - 1).Item("deltamark").ToString().Trim
                    If r_dt.Rows(ix - 1).Item("deltamark").ToString() = "D" Then
                        .BackColor = Color.FromArgb(150, 255, 150)
                        .ForeColor = Color.FromArgb(0, 128, 64)
                    End If

                    .Col = .GetColFromID("criticalmark") : .Text = r_dt.Rows(ix - 1).Item("criticalmark").ToString()
                    If r_dt.Rows(ix - 1).Item("criticalmark").ToString() = "C" Then
                        .BackColor = Color.FromArgb(255, 150, 255)
                        .ForeColor = Color.FromArgb(255, 255, 255)
                    End If

                    .Col = .GetColFromID("alertmark") : .Text = r_dt.Rows(ix - 1).Item("alertmark").ToString().Trim
                    If r_dt.Rows(ix - 1).Item("alertmark").ToString() <> "" Then
                        .BackColor = Color.FromArgb(255, 255, 150)
                        .ForeColor = Color.FromArgb(0, 0, 0)
                    End If

                    '-- 검사명 표시
                    .Col = .GetColFromID("tnmd")
                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        If r_dt.Rows(ix - 1).Item("tclscd").ToString.Trim = r_dt.Rows(ix - 1).Item("testcd").ToString.Substring(1, 5).Trim Then
                            .Text = "... " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                        Else
                            .Text = ".... " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                        End If
                    ElseIf r_dt.Rows(ix - 1).Item("tcdgbn").ToString.Trim = "B" Or _
                           r_dt.Rows(ix - 1).Item("testcd").ToString.Trim = r_dt.Rows(ix - 1).Item("tclscd").ToString.Trim Then
                        .Text = r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                    Else
                        .Text = ". " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                    End If

                    .Col = .GetColFromID("cvtfldgbn") : .Text = r_dt.Rows(ix - 1).Item("cvtfldgbn").ToString().Trim
                    If r_dt.Rows(ix - 1).Item("cvtfldgbn").ToString().Trim <> "" Then
                        .Col = .GetColFromID("cvtgbn") : .Text = "C"
                    Else
                        .Col = .GetColFromID("cvtgbn") : .Text = ""
                    End If

                    .Col = .GetColFromID("reftcls")
                    If r_dt.Rows(ix - 1).Item("reftcls").ToString().Trim = "1" Then
                        .Col = .GetColFromID("reftcls") : .Text = "☞"
                    Else
                        .Col = .GetColFromID("reftcls") : .Text = ""
                    End If

                    .Col = .GetColFromID("rstno") : .Text = r_dt.Rows(ix - 1).Item("rstno").ToString().Trim
                    If r_dt.Rows(ix - 1).Item("rstno").ToString().Trim > "1" Then
                        .Col = .GetColFromID("history")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                        .TypePictStretch = False
                        .TypePictMaintainScale = False
                        .TypePictPicture = GetImgList.getMultiRst()
                    End If

                    .Col = .GetColFromID("orgrst") : .Text = r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim : .ForeColor = .BackColor
                    .Col = .GetColFromID("corgrst") : .Text = r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim
                    If (r_dt.Rows(ix - 1).Item("bcno").ToString <> msBcNo Or (LOGIN.PRG_CONST.RST_BCNO_EXE = "0" And msEqCd = "" And msPartSlip <> "" And r_dt.Rows(ix - 1).Item("slipcd").ToString <> msPartSlip) Or _
                       (msTestCds <> "" And (msTestCds + ",").IndexOf(r_dt.Rows(ix - 1).Item("testspc").ToString.Trim + ",") < 0)) And mbDoctorMode = False Then
                        .Col = .GetColFromID("orgrst")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .BackColor = Color.Silver
                        .ForeColor = Color.Silver

                        .Col = .GetColFromID("chk") : .Lock = True
                    End If

                    .Col = .GetColFromID("viewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim
                    .Col = .GetColFromID("cviewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim

                    .Col = .GetColFromID("rstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim
                    .Col = .GetColFromID("crstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim

                    .Col = .GetColFromID("eqflag") : .Text = r_dt.Rows(ix - 1).Item("eqflag").ToString().Trim              '20
                    If r_dt.Rows(ix - 1).Item("eqflag").ToString().Trim <> "" Then
                        .BackColor = Color.DeepPink
                    End If

                    .Row = .MaxRows
                    If r_dt.Rows(ix - 1).Item("rstflg").ToString.Trim = "1" Or r_dt.Rows(ix - 1).Item("rstflg").ToString.Trim = "2" Then
                        .Col = .GetColFromID("chk")
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox And .Lock = False Then
                            .Text = "1"
                            .Col = .GetColFromID("iud") : .Text = "1"
                        ElseIf r_dt.Rows(ix - 1).Item("tcdgbn").ToString = "C" Then
                            .Col = .GetColFromID("iud") : .Text = "1"

                            For ix2 As Integer = .MaxRows - 1 To 1 Step -1
                                .Row = ix2
                                .Col = .GetColFromID("testcd")
                                If .Text = r_dt.Rows(ix - 1).Item("testcd").ToString.Substring(0, 5) Then
                                    .Col = .GetColFromID("chk") : .Text = "1"
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    If mbBloodBank Then
                        If msBlood_ABO_S = r_dt.Rows(ix - 1).Item("testcd").ToString.Trim Or msBlood_ABO_C = r_dt.Rows(ix - 1).Item("testcd").ToString.Trim Then
                            sRst_abo = r_dt.Rows(ix - 1).Item("viewrst").ToString.Trim
                            sRst_abo_bf = r_dt.Rows(ix - 1).Item("bfviewrst1").ToString.Trim
                        End If

                        If msBlood_Rh = r_dt.Rows(ix - 1).Item("testcd").ToString.Trim Then
                            sRst_rh = r_dt.Rows(ix - 1).Item("viewrst").ToString.Replace("(", "").Replace(")", "").Trim
                            sRst_rh_bf = r_dt.Rows(ix - 1).Item("bfviewrst1").ToString.Replace("(", "").Replace(")", "").Trim
                        End If
                    End If

                    If r_dt.Rows(ix - 1).Item("viewrst").ToString.IndexOf(vbCr) >= 0 Then
                        Dim sBuf() As String = r_dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))
                        .set_RowHeight(.MaxRows, m_dbl_RowHeightt * sBuf.Length)
                    End If

                Next

                If mbBloodBank Then
                    Dim bFlg_abo As Boolean = False
                    Dim bFlg_rh As Boolean = False
                    Dim sRstFlg_abo As String = ""
                    Dim sRstFlg_rh As String = ""

                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("bldgbn") : Dim sBldGbn As String = .Text
                        .Col = .GetColFromID("bbttype") : Dim sBbtType As String = .Text
                        .Col = .GetColFromID("rstflg") : Dim sRstflg As String = .Text
                        .Col = .GetColFromID("regid") : Dim sRegid As String = .Text
                        .Col = .GetColFromID("mwid") : Dim sMwid As String = .Text

                        If sBldGbn.Substring(0, 1) = "1" Then
                            If sRstFlg_abo < sRstflg Then sRstFlg_abo = sRstflg
                        ElseIf sBldGbn.Substring(0, 1) = "2" Then
                            If sRstFlg_rh < sRstflg Then sRstFlg_rh = sRstflg
                        End If

                        If sRegid = STU_AUTHORITY.UsrID Or sMwid = STU_AUTHORITY.UsrID Then
                            .Col = .GetColFromID("orgrst")
                            .Lock = False
                            .BackColor = Color.White
                            .ForeColor = Color.Black

                            .Col = .GetColFromID("viewrst")
                            .Lock = False
                            .BackColor = Color.White
                            .ForeColor = Color.Black

                            If sBldGbn = "11" Then
                                bFlg_abo = True
                            ElseIf sBldGbn = "21" Then
                                bFlg_rh = True
                            End If
                        ElseIf sRstflg = "3" Then
                            .Col = .GetColFromID("orgrst")
                            .Lock = False
                            .BackColor = Color.White
                            .ForeColor = Color.Black

                            .Col = .GetColFromID("viewrst")
                            .Lock = False
                            .BackColor = Color.White
                            .ForeColor = Color.Black
                        End If
                    Next

                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("bldgbn") : Dim sBldGbn As String = .Text
                        .Col = .GetColFromID("bbttype") : Dim sBbtType As String = .Text

                        If sBbtType = "1" Then
                            If sRstFlg_abo > "0" And bFlg_abo = False And sBldGbn = "12" Then
                                .Col = .GetColFromID("orgrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                                .Col = .GetColFromID("viewrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black
                            ElseIf sRstFlg_rh > "0" And bFlg_rh = False And sBldGbn = "22" Then
                                .Col = .GetColFromID("orgrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                                .Col = .GetColFromID("viewrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                            ElseIf sRstFlg_abo = "" And bFlg_abo = False And sBldGbn = "11" Then
                                .Col = .GetColFromID("orgrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                                .Col = .GetColFromID("viewrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black
                            ElseIf sRstFlg_rh = "" And bFlg_rh = False And sBldGbn = "21" Then
                                .Col = .GetColFromID("orgrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                                .Col = .GetColFromID("viewrst")
                                .Lock = False
                                .BackColor = Color.White
                                .ForeColor = Color.Black

                            End If

                        End If

                    Next

                    Me.lblABO.Text = sRst_abo + sRst_rh
                    Me.lblABO_bf.Text = sRst_abo_bf + sRst_rh_bf
                Else
                    Me.lblABO.Text = msAboRh
                End If

                .ReDraw = True
            End With

        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)

        Finally
            Me.spdResult.ReDraw = True
        End Try
    End Sub

    Private Sub sbDisplay_CommentView(ByVal r_dt As DataTable)
        txtCmtCont.Text = ""

        For intIdx As Integer = 0 To r_dt.Rows.Count - 1
            txtCmtCont.Text += r_dt.Rows(intIdx).Item(11).ToString + vbCrLf
        Next

        txtCmtCont.Tag = txtCmtCont.Text

        If txtCmtCont.Text.Replace(Chr(13), "").Replace(Chr(10), "").Trim = "" Then
            txtCmtCont.Text = ""
            txtCmtCont.Tag = ""
        End If
    End Sub

    Private Sub spdResult_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdResult.ButtonClicked

        If e.row < 1 Then Exit Sub

        Dim strBcNo As String = ""
        Dim sTestCd_p As String = ""

        Dim strTBcNo As String = ""
        Dim sTestCd As String = ""

        With spdResult
            If e.col = .GetColFromID("chk") Then
                .Row = e.row
                .Col = e.col
                If .Text = "1" Then
                    .Col = .GetColFromID("iud") : .Text = "1"
                    .Col = .GetColFromID("tcdgbn")
                    If .Text = "P" Then
                        .Col = .GetColFromID("testcd") : sTestCd_p = .Text : If sTestCd_p <> "" Then sTestCd_p = sTestCd_p.Substring(0, 5)
                        .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")

                        For intRow As Integer = e.row + 1 To .MaxRows
                            .Row = intRow
                            .Col = .GetColFromID("testcd") : sTestCd = .Text : If sTestCd <> "" Then sTestCd = sTestCd.Substring(0, 5)
                            .Col = .GetColFromID("bcno") : strTBcNo = .Text.Replace("-", "")

                            If sTestCd_p = sTestCd And strBcNo = strTBcNo Then
                                .Col = .GetColFromID("iud") : .Text = "1"
                            End If
                        Next
                    End If
                Else
                    .Row = e.row
                    .Col = .GetColFromID("iud") : .Text = ""
                    .Col = .GetColFromID("tcdgbn")
                    If .Text = "P" Then
                        .Col = .GetColFromID("testcd") : sTestCd_p = .Text : If sTestCd_p <> "" Then sTestCd_p = sTestCd_p.Substring(0, 5)
                        .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")

                        For intRow As Integer = e.row + 1 To .MaxRows
                            .Row = intRow

                            .Col = .GetColFromID("testcd") : sTestCd = .Text : If sTestCd <> "" Then sTestCd = sTestCd.Substring(0, 5)
                            .Col = .GetColFromID("bcno") : strTBcNo = .Text.Replace("-", "")

                            If sTestCd_p = sTestCd And strBcNo = strTBcNo Then
                                .Row = intRow : .Col = .GetColFromID("iud") : .Text = ""
                            End If
                        Next
                    End If
                End If
            End If

        End With
    End Sub

    Public Sub sbDisplay_RegNm_Test(ByVal rsTestcd As String)
        Dim sFn As String = "Sub sbDisplay_RegNm_Test()"

        Try
            Dim sID As String = ""
            Dim sNM As String = ""
            Dim sDT As String = ""

            '결과저장, 중간보고, 최종보고
            Me.lblReg.Text = ""
            Me.lblMW.Text = ""
            Me.lblFN.Text = ""
            Me.lblCfm.Text = ""

            Dim a_dr As DataRow()

            a_dr = m_dt_RstUsr.Select("testcd = '" + rsTestcd + "'")

            If a_dr.Length < 1 Then Return

            Dim sRstflg As String = a_dr(0).Item("rstflg").ToString()

            If sRstflg = "" Then Return

            For i As Integer = 1 To Convert.ToInt32(sRstflg)
                If i = 1 Then
                    sID = a_dr(0).Item("regid").ToString().Trim
                    sNM = a_dr(0).Item("regnm").ToString().Trim
                    sDT = a_dr(0).Item("regdt").ToString().Trim

                    If Not sID + sNM + sDT = "" Then
                        Me.lblReg.Text = sDT + " / " + sNM
                    End If
                ElseIf i = 2 Then
                    sID = a_dr(0).Item("mwid").ToString().Trim
                    sNM = a_dr(0).Item("mwnm").ToString().Trim
                    sDT = a_dr(0).Item("mwdt").ToString().Trim

                    If Not sID + sNM + sDT = "" Then
                        Me.lblMW.Text = sDT + " / " + sNM
                    End If
                ElseIf i = 3 Then
                    sID = a_dr(0).Item("fnid").ToString().Trim
                    sNM = a_dr(0).Item("fnnm").ToString().Trim
                    sDT = a_dr(0).Item("fndt").ToString().Trim

                    If Not sID + sNM + sDT = "" Then
                        Me.lblFN.Text = sDT + " / " + sNM
                        Me.lblCfm.Text = a_dr(0).Item("cfmnm").ToString().Trim
                    End If
                End If
            Next

        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)
        End Try
    End Sub

    Private Sub chkSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelect.Click

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    If chkSelect.Checked Then
                        .Col = .GetColFromID("testcd") : Dim sTestCd_p As String = .Text
                        .Col = .GetColFromID("tcdgbn") : Dim strTcdGbn As String = .Text
                        .Col = .GetColFromID("rstflg") : Dim sRstflg As String = .Text : If sRstflg = "0" Then sRstflg = ""
                        .Col = .GetColFromID("orgrst")

                        If .Text = "" And strTcdGbn = "P" Then
                            For intIdx As Integer = intRow + 1 To .MaxRows
                                .Row = intIdx
                                .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text
                                .Col = .GetColFromID("orgrst")
                                If .Text <> "" And sTestcd.StartsWith(sTestCd_p) Then
                                    .Row = intRow
                                    .Col = .GetColFromID("chk") : .Text = "1"
                                    Exit For
                                End If
                            Next
                        Else
                            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then
                                .Col = .GetColFromID("chk") : .Text = "1"
                            End If
                        End If
                    Else
                        .Col = .GetColFromID("chk") : .Text = ""
                    End If
                End If
            Next
        End With

    End Sub

    Private Sub spdResult_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdResult.ClickEvent

        If e.row = 0 Then Me.txtTestCd.Text = "" : Return

        Dim sBcNo As String = ""
        Dim sTnmd As String = ""
        Dim sTestCd As String = ""
        Dim sSpcCd As String = ""
        Dim sTCdGbn As String = ""

        Me.btnExmAdd.Enabled = False
        Me.mnuSpRst.Visible = False
        Me.mnuKeypad.Visible = False

        With spdResult
            .Row = e.row
            .Col = .GetColFromID("testcd") : Me.txtTestCd.Text = .Text
            .Col = .GetColFromID("spccd") : sSpcCd = .Text

            RaiseEvent ChangedTestCd(sBcNo, sTestCd)

            Dim sSpRstYn As String = LISAPP.COMM.RstFn.fnGet_SpRst_yn(IIf(Me.txtBcNo.Text = "", msBcNo, Me.txtBcNo.Text).ToString.Replace("-", ""), Me.txtTestCd.Text.Substring(0, 5))
            Dim sFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(Me.txtTestCd.Text.Substring(0, 5), sSpcCd)

            If sSpRstYn <> "" Then mnuSpRst.Visible = True
            If sFormGbn <> "" Then mnuKeypad.Visible = True
            .Col = .GetColFromID("tnmd") : Dim test As String = .Text


            If (e.col = .GetColFromID("orgrst") Or e.col = .GetColFromID("tnmd")) And e.row > 0 Then
                .Row = e.row
                .Col = .GetColFromID("tcdgbn") : sTCdGbn = .Text

                If sTCdGbn = "P" Or sTCdGbn = "C" Then
                    .Col = .GetColFromID("testcd") : sTestCd = .Text.Substring(0, 5)
                    sbDisplay_ExmAdd(sTestCd)
                End If

                .Row = e.row
                .Col = .GetColFromID("orgrst")
                If .Text.Trim = "{null}" Then
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("tnmd") : sTnmd = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text.Replace("-", "")

                    Dim strst As New AxAckResultViewer.STRST01

                    strst.SpecialTestName = sTnmd
                    strst.BcNo = sBcNo
                    strst.TestCd = sTestCd

                    strst.Left = CType(moForm.ParentForm.Left + (moForm.ParentForm.Width - strst.Width) / 2, Integer)
                    strst.Top = moForm.ParentForm.Top + Ctrl.menuHeight

                    strst.ShowDialog(moForm)
                End If
            ElseIf e.col = .GetColFromID("history") And e.row > 0 Then

                .Row = e.row
                .Col = .GetColFromID("rstno")
                If .Text >= "1" Then
                    Dim objForm As New FGHISTORY
                    Dim aryRst As New ArrayList

                    .Row = e.row
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")

                    aryRst = objForm.Display_Data(moForm, sBcNo)

                    If aryRst.Count > 0 Then
                        For intIdx As Integer = 0 To aryRst.Count - 1
                            For intRow = 1 To .MaxRows
                                .Row = intRow
                                .Col = .GetColFromID("testcd")
                                If .Text = CType(aryRst.Item(intIdx), RST_INFO).TestCd Then
                                    .Col = .GetColFromID("orgrst") : .Text = CType(aryRst.Item(intIdx), RST_INFO).OrgRst
                                    .Col = .GetColFromID("viewrst") : .Text = CType(aryRst.Item(intIdx), RST_INFO).OrgRst
                                    sbSet_ResultView(intRow)
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If
            ElseIf e.col = .GetColFromID("cvtgbn") And e.row > 0 Then
                .Row = e.row
                .Col = .GetColFromID("cvtgbn")
                If .Text = "C" Then

                End If
            ElseIf e.col = .GetColFromID("reftcls") And e.row > 0 Then
                .Row = e.row
                .Col = .GetColFromID("reftcls")
                If .Text = "☞" Then
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text.Replace("-", "")
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text.Replace("-", "")

                    Dim objForm As New FGRST_REF
                    objForm.Display_Data(moForm, sBcNo, sTestCd, sSpcCd)
                End If
            ElseIf e.col = .GetColFromID("eqflag") And e.row > 0 Then
                .Row = e.row
                .Col = e.col : If .Text <> "" Then MsgBox(.Text, MsgBoxStyle.OkOnly, "장비FLAG")
            ElseIf e.col = .GetColFromID("reftxt") And e.row > 0 Then
                Dim sTmp As String = fnGetTextTipFetch(Me.spdResult, e.row)
                If sTmp <> "" Then MsgBox(sTmp, MsgBoxStyle.OkOnly, "참고치")
            End If

        End With

        sTestCd = Ctrl.Get_Code(Me.spdResult, "testcd", e.row)
        sbDisplay_RegNm_Test(sTestCd)

    End Sub

    Private Sub spdResult_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdResult.DblClick
        If e.row < 1 Or e.col <> spdResult.GetColFromID("viewrst") Then Return

        With Me.spdResult
            Dim iRow As Integer = e.row

            .Row = iRow
            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
            .Col = .GetColFromID("orgrst") : Dim sOrgRst As String = .Text

            If .CellType <> FPSpreadADO.CellTypeConstants.CellTypeEdit Then Return

            Dim frm As New FGMULTILLINERST
            Dim sRst As String = frm.Display_Result(sTestCd, sOrgRst)

            If sRst <> "" Then
                .Row = iRow
                .Col = .GetColFromID("orgrst") : .Text = sRst
                .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                .ForeColor = Color.Black
                .Focus()
            End If
        End With
    End Sub

    Private Sub spdResult_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdResult.GotFocus
        With spdResult
            If .ActiveCol <> .GetColFromID("orgrst") Then Return

            .Row = .ActiveRow
            .Col = .GetColFromID("orgrst") : .ForeColor = Color.Black
        End With
    End Sub

    Private Sub spdResult_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdResult.KeyDownEvent
        Dim sFn As String = "Sub spdResult_KeyDownEvent(Object, AxFPSpreadADO._DSpreadEvents_KeyDownEvent)"
        Try
            Select Case Convert.ToInt32(e.keyCode)
                Case Keys.PageUp, Keys.PageDown
                    e.keyCode = 0

                Case 37, 39, 229, 27 ' 화살표 키
                    Me.lstCode.Items.Clear()
                    Me.lstCode.Hide()
                    Me.pnlCode.Visible = False

                Case 38 ' 방향 위키
                    If Me.lstCode.Visible = True Then
                        If Me.lstCode.SelectedIndex > -1 Then
                            If Me.lstCode.SelectedIndex > 0 Then
                                Me.lstCode.SelectedIndex -= 1
                            End If
                        Else
                            Me.lstCode.SelectedIndex = lstCode.Items.Count - 1
                        End If
                        e.keyCode = 0
                    End If


                Case 40 ' 방향 아래키
                    If Me.lstCode.Visible = True Then
                        If Me.lstCode.SelectedIndex > -1 Then
                            If Me.lstCode.Items.Count - 1 > Me.lstCode.SelectedIndex Then
                                Me.lstCode.SelectedIndex += 1
                            End If
                        Else
                            Me.lstCode.SelectedIndex = 0
                        End If
                        e.keyCode = 0
                    End If

                Case 13             ' Enter키
                    Dim sRst As String = ""
                    Dim sBcNo As String = ""
                    Dim sTestCd As String = ""
                    Dim sColorO As Color

                    With Me.spdResult
                        Dim iRow As Integer = .ActiveRow

                        .Row = iRow
                        .Col = .GetColFromID("orgrst") : sColorO = .BackColor
                        If sColorO <> Color.LightGray Then '< 20131107 회색(입력불가)시 이벤트 실행하지 않음
                            .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "`")
                            .Col = .GetColFromID("orgrst") : sRst = .Text.Replace("'", "`") : .Text = sRst
                            .Col = .GetColFromID("testcd") : sTestCd = .Text

                            If Me.lstCode.Visible Then
                                If Me.lstCode.SelectedIndex >= 0 Then
                                    sRst = lstCode.Text.Split(Chr(9))(1)
                                    .Col = .GetColFromID("orgrst") : .Text = sRst
                                End If
                            End If
                            .Col = .GetColFromID("viewrst") : .Text = sRst

                            sbSet_ResultView(iRow)
                            sbGet_CvtRstInfo(sBcNo, sTestCd)
                            sbGet_Calc_Rst(iRow) '-- 결과 계산

                            If mbBloodBank Then
                                If sRst <> "" And (sTestCd = msBlood_ABO_C Or sTestCd = msBlood_ABO_S Or sTestCd = msBlood_Rh) Then sbDisplay_Blood_Alert()
                            End If
                        Else
                            .Col = .GetColFromID("orgrst") : .Text = ""
                            .Col = .GetColFromID("viewrst") : .Text = ""
                        End If
                    End With

                    Me.lstCode.Items.Clear()
                    Me.lstCode.Hide()
                    Me.pnlCode.Visible = False

                Case Else
                    With Me.spdResult
                        Dim iRow As Integer = .ActiveRow
                        Dim sColorO As Color

                        .Row = iRow
                        .Col = .GetColFromID("orgrst") : sColorO = .BackColor
                        If sColorO <> Color.LightGray Then
                            RaiseEvent FunctionKeyDown(sender, New System.Windows.Forms.KeyEventArgs(CType(e.keyCode, System.Windows.Forms.Keys)))
                        Else
                            .Col = .GetColFromID("orgrst") : .Text = ""
                            .Col = .GetColFromID("viewrst") : .Text = ""
                        End If
                    End With
            End Select
        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)
        End Try
    End Sub

    ' 결과유형 체크
    Private Function fnRstTypeCheck(ByVal riRow As Integer, ByVal rsRst As String) As String

        Dim strRstLLen As String = ""
        Dim strRstULen As String = ""
        Dim strRstType As String = ""
        Dim strCutOpt As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("rsttype") : strRstType = .Text
            .Col = .GetColFromID("rstllen") : strRstLLen = .Text
            .Col = .GetColFromID("rstulen") : strRstULen = .Text
            .Col = .GetColFromID("cutopt") : strCutOpt = .Text

            If (strRstType = "0" Or strRstType = "1") And strRstLLen <> "" And rsRst <> "" And IsNumeric(rsRst) Then
                Dim intPos As Integer
                intPos = InStr(rsRst, ".")

                If Val(strRstLLen) >= 0 Then
                    .Col = .GetColFromID("rsttype")

                    Dim strDecimal As String = "0"
                    Dim intDecimal As Integer = CInt(strRstLLen)
                    If intDecimal > 0 Then
                        strDecimal = strDecimal & "." & New String(Chr(Asc("0")), intDecimal)
                    End If

                    Select Case strCutOpt
                        Case "0", "3"   ' 0 : 반올림처리없음(입력그대로). 3 : 내림
                            If intPos > 0 Then
                                If Len(rsRst) >= intPos + intDecimal Then
                                    rsRst = Mid(rsRst, 1, intPos + intDecimal)
                                End If
                            End If
                        Case "1"    ' 1 : 올림
                            If intPos > 0 Then
                                If Len(rsRst) >= intPos + intDecimal Then
                                    Dim strRstTmp As String
                                    strRstTmp = Mid(rsRst, 1, intPos + intDecimal)
                                    If Len(rsRst) >= intPos + intDecimal + 1 Then
                                        If Mid(rsRst, intPos + intDecimal + 1, 1) > "0" Then
                                            strRstTmp += "9"
                                        End If
                                    End If
                                    rsRst = strRstTmp
                                End If
                            End If
                        Case "2"    ' 2 : 반올림
                    End Select

                    rsRst = Format(Val(rsRst), strDecimal).ToString
                End If

                If Val(strRstULen) > 0 Then
                    If CInt(strRstULen) < intPos - 1 Then
                        Dim sMsg As String = "결과정수크기" & strRstULen & " 보다 큰 값이 입력되었습니다."
                        If mbBatchMode Then
                        Else
                            MsgBox(sMsg, MsgBoxStyle.Information)
                        End If
                    End If
                End If
            End If

            If strRstType = "1" And rsRst <> "" And IsNumeric(rsRst) = False Then
                Dim sMsg As String = "숫자 결과만 입력할 수 있습니다."
                If mbBatchMode Then
                Else
                    MsgBox(sMsg, MsgBoxStyle.Information)
                End If
            End If
        End With

        fnRstTypeCheck = rsRst

    End Function

    ' 결과유형 체크
    Private Sub sbRstTypeCheck(ByVal riRow As Integer)

        Dim strRstLLen As String = ""
        Dim strRstULen As String = ""
        Dim strRstType As String = ""
        Dim strCutOpt As String = ""
        Dim strRst As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : strRst = .Text
            .Col = .GetColFromID("rsttype") : strRstType = .Text
            .Col = .GetColFromID("rstllen") : strRstLLen = .Text
            .Col = .GetColFromID("rstulen") : strRstULen = .Text
            .Col = .GetColFromID("cutopt") : strCutOpt = .Text

            If (strRstType = "0" Or strRstType = "1") And strRstLLen <> "" And strRst <> "" And IsNumeric(strRst) Then
                Dim intPos As Integer
                intPos = InStr(strRst, ".")

                If Val(strRstLLen) >= 0 Then
                    .Col = .GetColFromID("rsttype")

                    Dim strDecimal As String = "0"
                    Dim intDecimal As Integer = CInt(strRstLLen)
                    If intDecimal > 0 Then
                        strDecimal = strDecimal & "." & New String(Chr(Asc("0")), intDecimal)
                    End If

                    Select Case strCutOpt
                        Case "0", "3"   ' 0 : 반올림처리없음(입력그대로). 3 : 내림
                            If intPos > 0 Then
                                If Len(strRst) >= intPos + intDecimal Then
                                    strRst = Mid(strRst, 1, intPos + intDecimal)
                                End If
                            End If
                        Case "1"    ' 1 : 올림
                            If intPos > 0 Then
                                If Len(strRst) >= intPos + intDecimal Then
                                    Dim strRstTmp As String
                                    strRstTmp = Mid(strRst, 1, intPos + intDecimal)
                                    If Len(strRst) >= intPos + intDecimal + 1 Then
                                        If Mid(strRst, intPos + intDecimal + 1, 1) > "0" Then
                                            strRstTmp += "9"
                                        End If
                                    End If
                                    strRst = strRstTmp
                                End If
                            End If
                        Case "2"    ' 2 : 반올림

                    End Select
                    .Col = .GetColFromID("viewrst") : .Text = Format(Val(strRst), strDecimal).ToString
                End If

                If Val(strRstULen) > 0 Then
                    If CInt(strRstULen) < intPos - 1 Then
                        Dim sMsg As String = "결과정수크기" & strRstULen & " 보다 큰 값이 입력되었습니다."
                        If mbBatchMode Then
                        Else
                            MsgBox(sMsg, MsgBoxStyle.Information)
                        End If

                        .Col = .GetColFromID("orgrst") : .Text = ""
                        .Col = .GetColFromID("viewrst") : .Text = ""
                    End If
                End If
            End If

            If strRstType = "1" And strRst <> "" And IsNumeric(strRst) = False Then
                Dim sMsg As String = "숫자 결과만 입력할 수 있습니다."
                If mbBatchMode Then
                    '.SetText(.GetColFromID("Comment"), iActiveRow, sMsg)
                Else
                    MsgBox(sMsg, MsgBoxStyle.Information)
                End If
            End If
        End With
    End Sub

    Private Sub sbUJudgCheck(ByVal riRow As Integer)
        Dim sRefL As String = ""
        Dim sRefH As String = ""
        Dim sRefHs As String = ""
        Dim sRefLs As String = ""

        Dim sJudgType As String = ""

        Dim sRefGbn As String = ""
        Dim sHLmark As String = ""
        Dim sRst As String = "", sViewRst As String = "", sOrgRst As String = "", sMark As String = ""
        Dim sUJRst As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("refgbn") : sRefGbn = .Text

            If sRefGbn = "2" Or sRefGbn = "1" Then
                .Col = .GetColFromID("judgtype") : sJudgType = .Text.Trim
                Select Case Len(sJudgType)
                    Case 6
                        .Col = .GetColFromID("refh") : sRefH = .Text
                        .Col = .GetColFromID("refhs") : sRefHs = .Text

                        If sRefH = "" Then
                            .Col = .GetColFromID("refl") : sRefH = .Text
                            .Col = .GetColFromID("refls") : sRefHs = .Text
                        End If

                        If sRefH = "" Then Exit Sub
                        .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                        .Col = .GetColFromID("viewrst") : sViewRst = .Text

                        If sOrgRst = "" Then Return

                        If sOrgRst.Trim.StartsWith(">=") Or sOrgRst.Trim.StartsWith("<=") Then
                            sMark = sOrgRst.Substring(0, 2)
                            sRst = sOrgRst.Substring(2).Trim

                        ElseIf sOrgRst.Trim.StartsWith(">") Or sOrgRst.Trim.StartsWith("<") Then
                            sMark = sOrgRst.Substring(0, 1)
                            sRst = sOrgRst.Substring(1).Trim
                        Else
                            sRst = sOrgRst
                        End If

                        If IsNumeric(sRst) = False Then Exit Sub

                        Select Case sRefHs
                            Case "0"
                                If Val(sRst) > Val(sRefH) Then
                                    sHLmark = "H"
                                End If
                            Case "1"
                                If Val(sRst) >= Val(sRefH) Then
                                    sHLmark = "H"
                                End If
                        End Select

                        If sHLmark = "" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt1") : sUJRst = .Text

                            Select Case Mid(sJudgType, 1, 3)
                                Case "210"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "211"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "212"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sViewRst & ")"
                                Case "213"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sViewRst & ""
                                Case "214"
                                    .Col = .GetColFromID("viewrst") : .Text = sViewRst & " " & sUJRst & ""
                            End Select
                        End If

                        If sHLmark = "H" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt2") : sUJRst = .Text

                            Select Case Mid(sJudgType, 4, 3)
                                Case "220"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "221"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "222"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "223"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "224"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sUJRst & ""
                            End Select
                        End If

                    Case 9
                        .Col = .GetColFromID("refl") : sRefL = .Text
                        .Col = .GetColFromID("refh") : sRefH = .Text
                        .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                        .Col = .GetColFromID("viewrst") : sViewRst = .Text
                        .Col = .GetColFromID("refls") : sRefLs = .Text
                        .Col = .GetColFromID("refhs") : sRefHs = .Text

                        If sOrgRst = "" Then Return

                        If sOrgRst.Trim.StartsWith(">") Or sOrgRst.Trim.StartsWith("<") Then
                            sMark = sOrgRst.Substring(0, 1)
                            sRst = sOrgRst.Substring(1).Trim
                        ElseIf sOrgRst.Trim.StartsWith(">=") Or sOrgRst.Trim.StartsWith("<=") Then
                            sMark = sOrgRst.Substring(0, 1)
                            sRst = sOrgRst.Substring(1).Trim
                        Else
                            sRst = sOrgRst
                        End If

                        If IsNumeric(sRst) = False Then Exit Sub

                        Select Case sRefLs
                            Case "0"
                                If Val(sRst) < Val(sRefL) And sRefL <> "" Then
                                    sHLmark = "L"
                                End If
                            Case "1"
                                If Val(sRst) <= Val(sRefL) And sRefL <> "" Then
                                    sHLmark = "L"
                                End If
                        End Select
                        Select Case sRefHs
                            Case "0"
                                If Val(sRst) > Val(sRefH) And sRefH <> "" Then
                                    sHLmark = "H"
                                End If
                            Case "1"
                                If Val(sRst) >= Val(sRefH) And sRefH <> "" Then
                                    sHLmark = "H"
                                End If
                        End Select

                        If sHLmark = "L" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt1") : sUJRst = .Text
                            Select Case Mid(sJudgType, 1, 3)
                                Case "310"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "311"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "312"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "313"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "314"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sUJRst & ""
                            End Select
                        End If

                        If sRefL = "" And sRefH = "" Then
                            sHLmark = "E"
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If
                        End If

                        If sHLmark = "" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt2") : sUJRst = .Text
                            Select Case Mid(sJudgType, 4, 3)
                                Case "320"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "321"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "322"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "323"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "324"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sUJRst & ""
                            End Select
                        End If

                        If sHLmark = "H" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt3") : sUJRst = .Text
                            Select Case Mid(sJudgType, 7, 3)
                                Case "330"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "331"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "332"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "333"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "334"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sUJRst & ""
                            End Select
                        End If

                    Case Else

                End Select
            End If

        End With
    End Sub

    ' High, Low 체크
    Private Sub sbHLCheck(ByVal riRow As Integer)
        Dim sRefL As String = ""
        Dim sRefH As String = ""
        Dim sRefLS As String = ""
        Dim sRefHS As String = ""
        Dim sRst As String = ""
        Dim sHLmark As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("refgbn")
            If .Text = "2" Then
                .Col = .GetColFromID("judgtype")
                If .Text <> "1" Then
                    .BackColor = Color.White
                    .ForeColor = Color.Black
                    .SetText(.GetColFromID("hlmark"), riRow, "")
                    Return
                End If

                .Col = .GetColFromID("refl") : sRefL = .Text
                .Col = .GetColFromID("refls") : sRefLS = .Text
                .Col = .GetColFromID("refh") : sRefH = .Text
                .Col = .GetColFromID("refhs") : sRefHS = .Text
                .Col = .GetColFromID("orgrst") : sRst = .Text

                sRst = sRst.Replace(">", "").Replace("<", "").Replace("=", "").Trim

                If IsNumeric(sRst) Then
                    Select Case sRefLS
                        Case "0"
                            If Val(sRst) < Val(sRefL) And sRefL <> "" Then
                                sHLmark = "L"
                            End If
                        Case "1"
                            If Val(sRst) <= Val(sRefL) And sRefL <> "" Then
                                sHLmark = "L"
                            End If
                    End Select

                    Select Case sRefHS
                        Case "0"
                            If Val(sRst) > Val(sRefH) And sRefH <> "" Then
                                sHLmark = "H"
                            End If
                        Case "1"
                            If Val(sRst) >= Val(sRefH) And sRefH <> "" Then
                                sHLmark = "H"
                            End If
                    End Select

                End If

                .Col = .GetColFromID("hlmark")
                Select Case sHLmark
                    Case "L"
                        .BackColor = Color.FromArgb(221, 240, 255)
                        .ForeColor = Color.FromArgb(0, 0, 255)
                        .SetText(.GetColFromID("hlmark"), riRow, sHLmark)
                    Case "H"
                        .BackColor = Color.FromArgb(255, 230, 231)
                        .ForeColor = Color.FromArgb(255, 0, 0)
                        .SetText(.GetColFromID("hlmark"), riRow, sHLmark)
                    Case Else
                        .BackColor = Color.White
                        .ForeColor = Color.Black
                        .SetText(.GetColFromID("hlmark"), riRow, "")
                End Select
            End If

            .Col = .GetColFromID("testcd") : Dim testcd As String = .Text

            ''-- JJH 해당검사들 결과값이 존재할때 H표시되도록 (진검실 요청)
            If testcd = "LH12107" Or testcd = "LH12108" Or testcd = "LH12109" Or testcd = "LH12110" Or testcd = "LH12111" Or testcd = "LH12112" Or testcd = "LH12113" Then
                .Col = .GetColFromID("orgrst")

                If .Text <> "" Then
                    .Col = .GetColFromID("hlmark")

                    .BackColor = Color.FromArgb(255, 230, 231)
                    .ForeColor = Color.FromArgb(255, 0, 0)
                    .SetText(.GetColFromID("hlmark"), riRow, "H")
                End If

            End If

        End With
    End Sub


    ' 패닉 체크
    ' 2 : 패닉 하한치, 상한치 사용
    Private Sub sbPanicCheck(ByVal riRow As Integer, Optional ByVal rdt_RstCd As DataTable = Nothing)

        Dim sOrgRst As String = ""
        Dim sPanicGbn As String = ""
        Dim sPanicL As String = ""
        Dim sPanicH As String = ""
        Dim sGrade As String = ""
        Dim sTestCd As String = ""
        Dim sPanicMark As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
            .Col = .GetColFromID("panicgbn") : sPanicGbn = .Text

            sOrgRst = sOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

            Select Case sPanicGbn
                Case "4", "5", "6"
                    .Col = .GetColFromID("testcd") : sTestCd = .Text

                    If rdt_RstCd Is Nothing Then Exit Sub

                    Dim dr As DataRow() = rdt_RstCd.Select("testcd = '" & sTestCd & "'")

                    Dim r As DataRow
                    For Each r In dr
                        If sOrgRst = r.Item("rstcont").ToString.Trim Then
                            sGrade = r.Item("grade").ToString
                            Exit For
                        End If
                    Next r
            End Select

            Select Case sPanicGbn
                Case "1"    ' 패닉하한치만 사용
                    .Col = .GetColFromID("panicl") : sPanicL = .Text

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "2"    ' 패닉상한치만 사용
                    .Col = .GetColFromID("panich") : sPanicH = .Text
                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "3"    ' 모두 사용
                    .Col = .GetColFromID("panicl") : sPanicL = .Text
                    .Col = .GetColFromID("panich") : sPanicH = .Text

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                        If Val(sOrgRst) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If
                Case "4"    ' 하한치만 사용(Grade)                        

                    .Col = .GetColFromID("panicl") : sPanicL = .Text

                    If sGrade <> "" Then
                        If Val(sGrade) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "5"    ' 상한치만 사용(Grade)                        

                    .Col = .GetColFromID("panich") : sPanicH = .Text

                    If sGrade <> "" Then
                        If Val(sGrade) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "6"    ' 모두 사용(Grade)

                    .Col = .GetColFromID("panicl") : sPanicL = .Text

                    .Col = .GetColFromID("panich") : sPanicH = .Text

                    If sGrade <> "" Then
                        If Val(sGrade) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                        If Val(sGrade) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If

            End Select

            If sPanicMark = "P" Then
                .Col = .GetColFromID("panicmark") : .Text = sPanicMark

                .BackColor = Color.FromArgb(150, 150, 255)
                .ForeColor = Color.FromArgb(255, 255, 255)
            Else
                .Col = .GetColFromID("panicmark") : .Text = ""

                .BackColor = Color.White
                .ForeColor = Color.Black
            End If
            'End If
        End With
    End Sub

    ' Delta 체크
    Private Sub sbDeltaCheck(ByVal riRow As Integer, Optional ByVal r_dt_RstCd As DataTable = Nothing)
        Dim sFn As String = "Sub deltaCheck(Integer)"
        Try
            Dim lngDateDiff As Long = 0
            Dim strDateDiff As String = ""
            Dim dteBFFNDT As Date
            Dim strDeltaGbn As String = ""
            Dim strRst As String = ""
            Dim strOldRst As String = ""
            Dim strDeltaL As String = ""
            Dim strDeltaH As String = ""
            Dim strDeltaMark As String = ""

            With spdResult
                .Row = riRow
                If .GetColFromID("bffndt1") < 0 Then
                    Exit Sub
                End If
                .Col = .GetColFromID("bffndt1")
                If .Text <> "" Then
                    dteBFFNDT = CDate(.Text)
                    lngDateDiff = DateDiff(DateInterval.Day, dteBFFNDT, MainServerDateTime.mServerDateTime)
                    If lngDateDiff < 1 Then
                        strDateDiff = "1"
                    Else
                        strDateDiff = Str(lngDateDiff).Trim
                    End If
                    .Col = .GetColFromID("deltaday")
                    If Val(strDateDiff) > Val(.Text) Then Exit Sub
                Else
                    Exit Sub
                End If

                .Col = .GetColFromID("deltagbn") : strDeltaGbn = .Text
                .Col = .GetColFromID("orgrst") : strRst = .Text

                strRst = strRst.Replace(">", "").Replace("<", "").Replace("=", "")

                If strRst = "" Then
                    .Col = .GetColFromID("deltamark") : .Text = ""
                    .BackColor = Color.White
                    .ForeColor = Color.Black
                    Exit Sub
                End If
                .Col = .GetColFromID("bforgrst1") : strOldRst = .Text

                strOldRst = strOldRst.Replace(">", "").Replace("<", "").Replace("=", "")

                If strRst.Trim = "" Then Exit Sub
                If strOldRst.Trim = "" Then Exit Sub

                .Col = .GetColFromID("deltah") : strDeltaH = .Text
                .Col = .GetColFromID("deltal") : strDeltaL = .Text

                Select Case strDeltaGbn
                    Case "1", "2", "3", "4"
                        If IsNumeric(strRst) = False Then Exit Sub
                        If IsNumeric(strOldRst) = False Then Exit Sub
                End Select

                Select Case strDeltaGbn
                    Case "1"    ' 1 : 변화차 = 현재결과 - 이전결과,
                        If strDeltaH <> "" And Val(strRst) - Val(strOldRst) > Val(strDeltaH) Then
                            strDeltaMark = "D"
                        End If

                        If strDeltaL <> "" And Val(strRst) - Val(strOldRst) < Val(strDeltaL) Then
                            strDeltaMark = "D"
                        End If

                    Case "2"    ' 2: 변화비율 = 변화차/이전결과  * 100
                        If Val(strOldRst) = 0 Then
                            strDeltaMark = "D"
                        Else
                            If strDeltaH <> "" And ((Val(strRst) - Val(strOldRst)) / Val(strOldRst)) * 100 > Val(strDeltaH) Then
                                strDeltaMark = "D"
                            End If

                            If strDeltaL <> "" And ((Val(strRst) - Val(strOldRst)) / Val(strOldRst)) * 100 < Val(strDeltaL) Then
                                strDeltaMark = "D"
                            End If
                        End If

                    Case "3"    '기간당 변화차 = 변화차/기간
                        .Col = .GetColFromID("bffndt1")
                        If .Text <> "" Then
                            If strDeltaH <> "" And (Val(strRst) - Val(strOldRst)) / Val(strDateDiff) > Val(strDeltaH) Then
                                strDeltaMark = "D"
                            End If

                            If strDeltaL <> "" And (Val(strRst) - Val(strOldRst)) / Val(strDateDiff) < Val(strDeltaL) Then
                                strDeltaMark = "D"
                            End If
                        End If

                    Case "4"    '기간당 변화비율 = 변화비율/기간
                        .Col = .GetColFromID("bffndt1")
                        If .Text <> "" Then
                            If strDeltaH <> "" And ((Val(strRst) - Val(strOldRst)) / Val(strOldRst)) * 100 / Val(strDateDiff) > Val(strDeltaH) Then
                                strDeltaMark = "D"
                            End If

                            If strDeltaL <> "" And ((Val(strRst) - Val(strOldRst)) / Val(strOldRst)) * 100 / Val(strDateDiff) < Val(strDeltaL) Then
                                strDeltaMark = "D"
                            End If
                        End If

                    Case "5"    'Grade Delta = 현재Grade - 이전Grade
                        Dim sTestCd As String
                        Dim sSpcCd As String

                        .Row = riRow
                        .Col = .GetColFromID("testcd") : sTestCd = .Text
                        .Col = .GetColFromID("spccd") : sSpcCd = .Text

                        Dim strGrade As String = ""
                        Dim strGrade_Old As String = ""

                        If r_dt_RstCd Is Nothing Then Exit Sub

                        Dim dr As DataRow() = r_dt_RstCd.Select("testcd = '" + sTestCd + "'")
                        Dim dt As DataTable = Fn.ChangeToDataTable(dr)

                        For intIdx As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(intIdx).Item("rstcont").ToString.Trim = strRst Then
                                strGrade = dt.Rows(intIdx).Item("grade").ToString
                                Exit For
                            End If
                        Next

                        For intIdx As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(intIdx).Item("rstcont").ToString.Trim = strOldRst Then
                                strGrade_Old = dt.Rows(intIdx).Item("grade").ToString
                                Exit For
                            End If
                        Next

                        If strGrade <> "" And strGrade_Old <> "" Then
                            If Math.Abs(Val(strGrade) - Val(strGrade_Old)) > Math.Abs(Val(strDeltaH)) Then
                                strDeltaMark = "D"
                            End If
                        End If

                End Select

                .Col = .GetColFromID("deltamark")
                If strDeltaMark = "D" Then
                    .Text = strDeltaMark
                    .BackColor = Color.FromArgb(150, 255, 150)
                    .ForeColor = Color.FromArgb(0, 128, 64)
                Else
                    .Text = ""
                    .BackColor = Color.White
                    .ForeColor = Color.Black
                End If
            End With
        Catch ex As Exception
            'sbLog_Exception(sFn + " : " + ex.Message)
        End Try

    End Sub

    ' Critical 체크
    Private Sub sbCriticalCheck(ByVal riRow As Integer)
        Dim strRst As String = ""
        Dim strCriticalGbn As String = ""
        Dim strCriticalL As String = ""
        Dim strCriticalH As String = ""
        Dim strCriticalMark As String = ""
        Dim strTclscd As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : strRst = .Text
            .Col = .GetColFromID("criticalgbn") : strCriticalGbn = .Text
            .Col = .GetColFromID("criticall") : strCriticalL = .Text
            .Col = .GetColFromID("criticalh") : strCriticalH = .Text
            .Col = .GetColFromID("testcd") : strTclscd = .Text

            'If strTclscd = "LG104" Then Exit Sub '2018-11-06 Xpert pcr 서술형 검사는 detected결과 시 인터페이스에서 Critical보내주므로 LIS에서 판단안하고 return 

            strRst = strRst.Replace(">", "").Replace("<", "").Replace("=", "")

            Select Case strCriticalGbn
                Case "1"    ' 위험하한치만 사용

                    If strCriticalL = "" Then Exit Sub
                    If IsNumeric(strCriticalL) = False Then Exit Sub

                    If IsNumeric(strRst) Then
                        If Val(strRst) < Val(strCriticalL) Then
                            strCriticalMark = "C"
                        End If
                    End If

                Case "2"    '  위험상한치만 사용
                    If strCriticalH = "" Then Exit Sub
                    If IsNumeric(strCriticalH) = False Then Exit Sub

                    If IsNumeric(strRst) Then
                        If Val(strRst) > Val(strCriticalH) Then
                            strCriticalMark = "C"
                        End If
                    End If
                Case "3"    ' 모두 사용
                    If strCriticalL = "" Then Exit Sub
                    If IsNumeric(strCriticalL) = False Then Exit Sub
                    If strCriticalH = "" Then Exit Sub
                    If IsNumeric(strCriticalH) = False Then Exit Sub

                    If IsNumeric(strRst) Then
                        If Val(strRst) < Val(strCriticalL) Then
                            strCriticalMark = "C"
                        End If
                        If Val(strRst) > Val(strCriticalH) Then
                            strCriticalMark = "C"
                        End If
                    End If
                Case "7"
                    'Critical 문자값 판단 추가(검사마스터에서 Critical 구분 [7] 문자결과(결과코드 설정) 선택, 기초마스터 결과코드에 Critical 설정한 경우 )
                    Dim sTxtCritical As String = ""
                    sTxtCritical = LISAPP.COMM.RstFn.fnGet_GraedValue_C(strTclscd, strRst)

                    If strTclscd = "LG104" Then 'xpert pcr 검사가 Critical이라도 해당 환자의 1주일전 pcr검사 이력이 Deteted(Critical)일 경우 Normal결과로 판단
                        Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_Xpert_Comment(msBcNo, True)

                        If dt.Rows.Count > 0 Then
                            Exit Sub
                        ElseIf dt.Rows.Count <= 0 Then
                            strCriticalMark = sTxtCritical
                            If sTxtCritical = "C" Then msXpertC = True Else msXpertC = False
                        End If
                        'ElseIf strTclscd = "LB151" Or strTclscd = "LB11201" Or strTclscd = "LB11202" Then
                        '    strCriticalMark = sTxtCritical
                    Else
                        '20220223 jhs 문제 없는 것 같아 풀어서 배포
                        strCriticalMark = sTxtCritical
                    End If


            End Select

            .Col = .GetColFromID("criticalmark")
            If strCriticalMark = "C" Then
                .Text = strCriticalMark
                .BackColor = Color.FromArgb(255, 150, 150)
                .ForeColor = Color.FromArgb(255, 255, 255)
            Else
                .Text = ""
                .BackColor = Color.White
                .ForeColor = Color.Black
            End If
        End With
    End Sub
    ' Critical 체크(문자결과추가)
    Private Sub sbCriticalCheck2(ByVal riRow As Integer, Optional ByVal r_dt_RstCd As DataTable = Nothing)
        Dim strRst As String = ""
        Dim strCriticalGbn As String = ""
        Dim strCriticalL As String = ""
        Dim strCriticalH As String = ""
        Dim strCriticalMark As String = ""
        Dim sTestcd As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : strRst = .Text
            .Col = .GetColFromID("criticalgbn") : strCriticalGbn = .Text
            .Col = .GetColFromID("criticall") : strCriticalL = .Text
            .Col = .GetColFromID("criticalh") : strCriticalH = .Text
            .Col = .GetColFromID("testcd") : sTestcd = .Text

            strRst = strRst.Replace(">", "").Replace("<", "").Replace("=", "")

            Select Case strCriticalGbn
                Case "1"    ' 위험하한치만 사용

                    If strCriticalL = "" Then Exit Sub
                    If IsNumeric(strCriticalL) = False Then Exit Sub

                    If IsNumeric(strRst) Then
                        If Val(strRst) < Val(strCriticalL) Then
                            strCriticalMark = "C"
                        End If
                    End If

                Case "2"    '  위험상한치만 사용
                    If strCriticalH = "" Then Exit Sub
                    If IsNumeric(strCriticalH) = False Then Exit Sub

                    If IsNumeric(strRst) Then
                        If Val(strRst) > Val(strCriticalH) Then
                            strCriticalMark = "C"
                        End If
                    End If
                Case "3"    ' 모두 사용
                    If strCriticalL = "" Then Exit Sub
                    If IsNumeric(strCriticalL) = False Then Exit Sub
                    If strCriticalH = "" Then Exit Sub
                    If IsNumeric(strCriticalH) = False Then Exit Sub

                    If IsNumeric(strRst) Then
                        If Val(strRst) < Val(strCriticalL) Then
                            strCriticalMark = "C"
                        End If
                        If Val(strRst) > Val(strCriticalH) Then
                            strCriticalMark = "C"
                        End If
                    End If
                Case "7"
                    Dim dr As DataRow() = r_dt_RstCd.Select("testcd = '" + sTestcd + "'")

                    Dim r As DataRow
                    For Each r In dr
                        If strRst = r.Item("rstcont").ToString.Trim Then
                            strCriticalMark = r.Item("crtval").ToString.Trim
                            Exit For
                        End If
                    Next r
            End Select

            .Col = .GetColFromID("criticalmark")
            If strCriticalMark = "C" Then
                .Text = strCriticalMark
                .BackColor = Color.FromArgb(255, 150, 150)
                .ForeColor = Color.FromArgb(255, 255, 255)
            Else
                .Text = ""
                .BackColor = Color.White
                .ForeColor = Color.Black
            End If
        End With
    End Sub    ' alert 체크
    Private Sub sbAlertCheck(ByVal rirow As Integer)
        Dim strORst As String = "", strVRst As String = "", strEqFlag As String = ""
        Dim sTestCd As String = "", strSpcCd As String = "", sTclsCd As String = "", sPanicMark As String = "", sDeltaMark As String = ""
        Dim strAlertGbn As String = ""
        Dim strAlertL As String = ""
        Dim strAlertH As String = ""
        Dim strAlertMark As String = ""

        With spdResult
            .Row = rirow
            .Col = .GetColFromID("testcd") : sTestCd = .Text
            .Col = .GetColFromID("spccd") : strSpcCd = .Text

            .Col = .GetColFromID("tclscd") : sTclsCd = .Text

            .Col = .GetColFromID("orgrst") : strORst = .Text
            .Col = .GetColFromID("viewrst") : strVRst = .Text
            .Col = .GetColFromID("eqflag") : strEqFlag = .Text

            .Col = .GetColFromID("panicmark") : sPanicMark = .Text
            .Col = .GetColFromID("deltamark") : sDeltaMark = .Text

            .Col = .GetColFromID("alertgbn") : strAlertGbn = .Text
            .Col = .GetColFromID("alertl") : strAlertL = .Text
            .Col = .GetColFromID("alerth") : strAlertH = .Text

            Select Case strAlertGbn
                Case "1", "A"   ' 경고하한치만 사용
                    If strAlertL = "" Then Exit Sub
                    If IsNumeric(strAlertL) = False Then Exit Sub

                    If IsNumeric(strORst) Then
                        If Val(strORst) < Val(strAlertL) Then
                            strAlertMark = "A"
                        End If
                    End If

                Case "2", "B"    ' 경고상한치만 사용
                    If strAlertH = "" Then Exit Sub
                    If IsNumeric(strAlertH) = False Then Exit Sub

                    If IsNumeric(strORst) Then
                        If Val(strORst) > Val(strAlertH) Then
                            strAlertMark = "A"
                        End If
                    End If
                Case "3", "C"    ' 모두 사용
                    If strAlertL = "" Then Exit Sub
                    If IsNumeric(strAlertL) = False Then Exit Sub
                    If strAlertH = "" Then Exit Sub
                    If IsNumeric(strAlertH) = False Then Exit Sub

                    If IsNumeric(strORst) Then
                        If Val(strORst) < Val(strAlertL) Then
                            strAlertMark = "A"
                        End If
                        If Val(strORst) > Val(strAlertH) Then
                            strAlertMark = "A"
                        End If
                    End If

                Case "4"    '-- 문자값 비교
                    If strAlertL = "" And strAlertH = "" Then Exit Sub
                    If strAlertL = "" Then strAlertL = strAlertH

                    If strORst.ToUpper = strAlertL.ToUpper Then strAlertMark = "A"
                Case "7" '-- 결과코드 
                    '20210810 jhs 결과코드 추가 
                    'Alter 문자값 판단 추가(검사마스터에서 Alter 구분 [7] 문자결과(결과코드 설정) 선택, 기초마스터 결과코드에 Alter 설정한 경우 )
                    Dim sTxtAlter As String = ""
                    sTxtAlter = LISAPP.COMM.RstFn.fnGet_GraedValue_A(sTestCd, strORst)

                    If sTxtAlter = "A" Then
                        strAlertMark = "A"
                    End If
                    '----------------------------------------------------
            End Select

            If strAlertMark = "" And (strAlertGbn = "5" Or strAlertGbn = "A" Or strAlertGbn = "B" Or strAlertGbn = "C") Then
                '-- Alert Rule
                Dim dr As DataRow() = m_dt_Alert_Rule.Select("testcd = '" + sTestCd + "'")


                If dr.Length > 0 Then
                    Dim intCnt As Integer = 0, intAlert As Integer = 0

                    If dr(0).Item("orgrst").ToString.Trim <> "" Then
                        intCnt += 1
                        If dr(0).Item("orgrst").ToString().IndexOf(strORst + ",") >= 0 Then intAlert += 1
                    End If

                    If dr(0).Item("viewrst").ToString.Trim <> "" Then
                        intCnt += 1
                        If dr(0).Item("viewrst").ToString().IndexOf(strVRst + ",") >= 0 Then intAlert += 1
                    End If

                    If sPanicMark <> "" Then
                        intCnt += 1
                        intAlert += 1
                    End If

                    If sDeltaMark <> "" Then
                        intCnt += 1
                        intAlert += 1
                    End If

                    If dr(0).Item("eqflag").ToString.Trim <> "" Then
                        intCnt += 1

                        If strEqFlag <> "" Then
                            If dr(0).Item("eqflag").ToString().IndexOf("^") >= 0 Then
                                Dim strBuf() As String = dr(0).Item("eqflag").ToString().Split("^"c)

                                If strBuf(1) = "" Then
                                    If strBuf(0) = "" Then
                                        intAlert += 1
                                    Else
                                        strBuf(0) += ","
                                        If strBuf(0).IndexOf(strEqFlag + ",") >= 0 Then intAlert += 1
                                    End If
                                Else
                                    If strBuf(0) = "" Then
                                        strBuf(1) += ","
                                        If strBuf(1).IndexOf(sTclsCd + ",") >= 0 Then intAlert += 1
                                    Else
                                        strBuf(0) += "," : strBuf(1) += ","
                                        If strBuf(0).IndexOf(strEqFlag + ",") >= 0 And strBuf(1).IndexOf(sTclsCd + ",") >= 0 Then intAlert += 1
                                    End If
                                End If
                            Else
                                If dr(0).Item("eqflag").ToString().IndexOf(strEqFlag + ",") >= 0 Then intAlert += 1
                            End If
                        End If

                    End If

                    If dr(0).Item("sex").ToString.Trim <> "" Then
                        intCnt += 1
                        If msSexAge.StartsWith(dr(0).Item("sex").ToString()) Then intAlert += 1
                    End If

                    If dr(0).Item("deptcds").ToString.Trim <> "" Then
                        intCnt += 1
                        If dr(0).Item("deptcds").ToString().IndexOf(msDeptCd + ",") >= 0 Then intAlert += 1
                    End If

                    If dr(0).Item("spccds").ToString.Trim <> "" Then
                        intCnt += 1
                        If dr(0).Item("spccds").ToString().IndexOf(strSpcCd + ",") >= 0 Then intAlert += 1
                    End If

                    If intCnt = intAlert Then strAlertMark = "A"
                End If
            End If

            .Col = .GetColFromID("alertmark")
            If strAlertMark = "A" Then
                .Text = strAlertMark
                .BackColor = Color.FromArgb(255, 255, 150)
                .ForeColor = Color.FromArgb(0, 0, 0)
            Else
                .Text = ""
                .BackColor = Color.White
                .ForeColor = Color.Black
            End If
        End With
    End Sub

    ' alert 체크
    Private Sub sbAlimitCheck(ByVal rirow As Integer)

        Dim sRst As String = ""
        Dim sAlimitL As String = ""
        Dim sAlimitH As String = ""
        Dim sAlimitLs As String = ""
        Dim sAlimitHs As String = ""
        Dim sAlimitGbn As String = ""

        With spdResult
            .Row = rirow
            .Col = .GetColFromID("orgrst") : sRst = .Text
            .Col = .GetColFromID("alimitgbn") : sAlimitGbn = .Text
            .Col = .GetColFromID("alimitl") : sAlimitL = .Text
            .Col = .GetColFromID("alimith") : sAlimitH = .Text
            .Col = .GetColFromID("alimitls") : sAlimitLs = .Text
            .Col = .GetColFromID("alimiths") : sAlimitHs = .Text

            sRst = sRst.Replace(">", "").Replace("<", "").Replace("=", "").Replace(",", "")

            Select Case sAlimitGbn
                Case "1"    ' 허용하한치만 사용
                    If sAlimitL = "" Then Exit Sub
                    If IsNumeric(sAlimitL) = False Then Exit Sub

                    If IsNumeric(sRst) Then
                        If Val(sRst) <= Val(sAlimitL) Then
                            Select Case sAlimitLs
                                Case "1"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL
                                Case "2"
                                    .Col = .GetColFromID("viewrst") : .Text = "< " + sAlimitL
                                Case "3"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL + " 이하"
                                Case "4"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL + " 미만"
                                Case "5"
                                    .Col = .GetColFromID("viewrst") : .Text = "<= " + sAlimitL
                            End Select
                            'sAlertMark = "A"
                        End If
                    End If

                Case "2"    ' 허용상한치만 사용
                    If sAlimitH = "" Then Exit Sub
                    If IsNumeric(sAlimitH) = False Then Exit Sub

                    If IsNumeric(sRst) Then
                        If Val(sRst) >= Val(sAlimitH) Then
                            Select Case sAlimitHs
                                Case "1"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH
                                Case "2"
                                    .Col = .GetColFromID("viewrst") : .Text = "> " + sAlimitH
                                Case "3"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH + " 이상"
                                Case "4"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH + " 초과"
                                Case "5"
                                    .Col = .GetColFromID("viewrst") : .Text = ">= " + sAlimitH
                            End Select
                            'sAlertMark = "A"
                        End If
                    End If
                Case "3"    ' 모두 사용

                    If sAlimitL = "" Then Exit Sub
                    If IsNumeric(sAlimitL) = False Then Exit Sub
                    If sAlimitH = "" Then Exit Sub
                    If IsNumeric(sAlimitH) = False Then Exit Sub

                    If IsNumeric(sRst) Then
                        If Val(sRst) <= Val(sAlimitL) Then
                            Select Case sAlimitLs
                                Case "1"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL
                                Case "2"
                                    .Col = .GetColFromID("viewrst") : .Text = "< " + sAlimitL
                                Case "3"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL + " 이하"
                                Case "4"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL + " 미만"
                                Case "5"
                                    .Col = .GetColFromID("viewrst") : .Text = "<= " + sAlimitL
                            End Select
                            'sAlertMark = "A"
                        End If
                        If Val(sRst) >= Val(sAlimitH) Then
                            Select Case sAlimitHs
                                Case "1"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH
                                Case "2"
                                    .Col = .GetColFromID("viewrst") : .Text = "> " + sAlimitH
                                Case "3"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH + " 이상"
                                Case "4"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH + " 초과"
                                Case "5"
                                    .Col = .GetColFromID("viewrst") : .Text = ">= " + sAlimitH
                            End Select
                        End If
                    End If

            End Select

        End With
    End Sub

    Private Function fnGetTextTipFetch(ByVal roSpd As AxFPSpreadADO.AxfpSpread, ByVal riRow As Integer) As String

        Dim sHelp As String = "" & vbCrLf
        With roSpd
            .Row = riRow
            Dim sRef As String = ""
            Dim sRefTmp As String = ""
            .Col = .GetColFromID("refl")
            sRefTmp = .Text
            If sRefTmp <> "" Then
                sRefTmp = ""
                .Col = .GetColFromID("refls")
                If .Text = "0" Then sRef = sRefTmp & " " & "<= "
                If .Text = "1" Then sRef = sRefTmp & " " & "< "
            End If
            .Col = .GetColFromID("refh")
            sRefTmp = .Text
            If sRefTmp <> "" Then
                sRefTmp = ""
                .Col = .GetColFromID("refhs")
                If .Text = "0" Then sRef &= " ~ <= " & sRefTmp
                If .Text = "1" Then sRef &= " ~ < " & sRefTmp
            End If
            .Col = .GetColFromID("refgbn")
            Dim sRefGbn As String
            sRefGbn = .Text
            .Col = .GetColFromID("judgtype")
            Dim sJudgType As String

            sJudgType = .Text
            .Col = .GetColFromID("reftxt")
            If sRef <> "" Then
                sHelp &= fnGetTipLine("참고치부등호 : " & sRef)
            End If

            If sRefGbn = "2" And Len(sJudgType) = 6 Then
                .Col = .GetColFromID("ujudglt1")
                Dim sJudg As String
                sJudg = "사용자판정문자 : " & .Text & " / "
                .Col = .GetColFromID("ujudglt2")
                sJudg &= .Text
                sHelp += fnGetTipLine(sJudg)
            End If

            If sRefGbn = "2" And Len(sJudgType) = 9 Then
                .Col = .GetColFromID("ujudglt1")
                Dim sJudg As String
                sJudg = "사용자판정문자 : " & .Text & " / "
                .Col = .GetColFromID("ujudglt2")
                sJudg &= .Text & " / "
                .Col = .GetColFromID("ujudglt3")
                sJudg &= .Text
                sHelp += fnGetTipLine(sJudg)
            End If

            If sRefGbn = "2" Then
                .Col = .GetColFromID("panicgbn")
                Dim sPanic As String = ""
                Select Case .Text
                    Case "1"    ' 패닉상한치만 사용                                    
                        .Col = .GetColFromID("panicl")
                        sPanic = "Panic 하한치 : "
                        sPanic &= .Text & "     판정기준 : 하한치"
                        sHelp += fnGetTipLine(sPanic)
                    Case "2"    ' 패닉상한치만 사용                                    
                        .Col = .GetColFromID("panich")
                        sPanic = "Panic 상한치 : "
                        sPanic &= .Text & "     판정기준 : 상한치"
                        sHelp += fnGetTipLine(sPanic)
                    Case "3"    ' 모두 사용
                        .Col = .GetColFromID("panicl")
                        sPanic = "Panic 하한치 : " & .Text & "  "
                        .Col = .GetColFromID("panich")
                        sPanic &= "상한치 : " & .Text & "   "
                        sHelp += fnGetTipLine(sPanic)
                    Case "4"    ' 패닉상한치만 사용(Grade)                                    
                        .Col = .GetColFromID("panicl")
                        sPanic = "Panic 하한치 : "
                        sPanic &= .Text & "     판정기준 : 하한치(Grade)"
                        sHelp += fnGetTipLine(sPanic)
                    Case "5"    ' 패닉상한치만 사용(Grade)                                    
                        .Col = .GetColFromID("panich")
                        sPanic = "Panic 상한치 : "
                        sPanic &= .Text & "     판정기준 : 상한치(Grade)"
                        sHelp += fnGetTipLine(sPanic)
                    Case "6"    ' 모두 사용(Grade)
                        .Col = .GetColFromID("panicl")
                        sPanic = "Panic 하한치 : " & .Text & "  "
                        .Col = .GetColFromID("panich")
                        sPanic &= " 상한치 : " & .Text & "     판정기준 : (Grade)"
                        sHelp += fnGetTipLine(sPanic)
                End Select

            End If

            If sRefGbn = "2" Then
                .Col = .GetColFromID("deltagbn")
                Dim sDelta As String = ""
                Select Case .Text
                    Case "1"    ' 델타하한치만 사용                                    
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 변화차 = 현재결과 - 이전결과"
                        sHelp += fnGetTipLine(sDelta)
                    Case "2"    ' 패닉상한치만 사용                                    
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 변화비율 = 변화차/이전결과  * 100"
                        sHelp += fnGetTipLine(sDelta)
                    Case "3"    ' 모두 사용
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 기간당 변화차 = 변화차/기간"
                        sHelp += fnGetTipLine(sDelta)
                    Case "4"    ' 패닉상한치만 사용(Grade)                                    
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 기간당 변화비율 = 변화비율/기간"
                        sHelp += fnGetTipLine(sDelta)
                    Case "5"    ' 패닉상한치만 사용(Grade)                                    
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 절대변화비율 = 변화차/이전결과"
                        sHelp += fnGetTipLine(sDelta)
                    Case "6"    ' Grade Delta = 현재Grade - 이전Grade
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta : " '& .Text & " ~ "

                        sDelta &= .Text & "     판정기준 : Grade Delta = 현재Grade - 이전Grade"
                        sHelp += fnGetTipLine(sDelta)
                End Select

                If sDelta <> "" Then
                    .Col = .GetColFromID("deltaday")
                    sHelp += fnGetTipLine("Delta 기간일 : " & .Text)
                End If

            End If

            'If sRefGbn = "2" And sJudgType = "1" Then
            If sRefGbn = "2" Then
                .Col = .GetColFromID("criticalgbn")
                Dim sCritical As String = ""

                Select Case .Text
                    Case "1"    ' 위험하한치만 사용                                    
                        .Col = .GetColFromID("criticall")
                        sCritical = "Critical 하한치 : "
                        sCritical &= .Text & "     판정기준 : 하한치"
                        sHelp += fnGetTipLine(sCritical)
                    Case "2"    ' 위험상한치만 사용                                    
                        .Col = .GetColFromID("criticalh")
                        sCritical = "Critical 상한치 : "
                        sCritical &= .Text & "     판정기준 : 상한치"
                        sHelp += fnGetTipLine(sCritical)
                    Case "3"    ' 모두 사용
                        .Col = .GetColFromID("criticall")
                        sCritical = "Critical 하한치 : " & .Text & "  "
                        .Col = .GetColFromID("criticalh")
                        sCritical &= "상한치 : " & .Text & "   "
                        sHelp += fnGetTipLine(sCritical)
                End Select

            End If

            'If sRefGbn = "2" And sJudgType = "1" Then
            If sRefGbn = "2" Then
                .Col = .GetColFromID("alertgbn")
                Dim sAlert As String = ""

                Select Case .Text
                    Case "1", "A"   ' 경고하한치만 사용                                    
                        .Col = .GetColFromID("alertl")
                        sAlert = "Alert 하한치 : "
                        sAlert &= .Text & "     판정기준 : 하한치"
                        sHelp += fnGetTipLine(sAlert)
                    Case "2", "B"   ' 경고상한치만 사용                                    
                        .Col = .GetColFromID("alerth")
                        sAlert = "Alert 상한치 : "
                        sAlert &= .Text & "     판정기준 : 상한치"
                        sHelp += fnGetTipLine(sAlert)
                    Case "3", "C"   ' 모두 사용
                        .Col = .GetColFromID("alertl")
                        sAlert = "Alert 하한치 : " & .Text & " ~ "
                        .Col = .GetColFromID("alerth")
                        sAlert &= "상한치 : " & .Text
                        sHelp += fnGetTipLine(sAlert)
                End Select

            End If
        End With
        fnGetTextTipFetch = sHelp
    End Function

    Private Function fnGetTipLine(ByVal sStr As String) As String
        fnGetTipLine = Space(4) & sStr & Space(4) & vbCrLf
    End Function

    Private Sub sbDisplay_ExmAdd(ByVal rsTestCd As String)

        Dim sTCdGbn As String = ""

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("tcdgbn") : sTCdGbn = .Text

                .Col = .GetColFromID("testcd")
                If .Text.Substring(0, 5) = rsTestCd And sTCdGbn = "C" Then
                    .Col = .GetColFromID("viwsub")
                    If .Text = "0" Then
                        btnExmAdd.Enabled = True
                        Exit For
                    End If
                End If
            Next
        End With

    End Sub

    Private Sub sbResult_Setting(ByVal raRst As ArrayList)


        For intIdx As Integer = 0 To raRst.Count - 1
            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("testcd")
                    If .Text = CType(raRst(intIdx), RST_INFO).TestCd Then
                        .Col = .GetColFromID("orgrst") : .Text = CType(raRst(intIdx), RST_INFO).OrgRst
                        .Col = .GetColFromID("viewrst") : .Text = CType(raRst(intIdx), RST_INFO).ViewRst
                        .Col = .GetColFromID("tcdgbn") : Dim strTCdGbn As String = .Text

                        .Col = .GetColFromID("chk")
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            .Text = "1"
                        End If

                        .Col = .GetColFromID("iud") : .Text = "1"

                        If strTCdGbn = "C" Then
                            For intIx1 As Integer = intRow - 1 To 1 Step -1
                                .Row = intIx1
                                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                                .Col = .GetColFromID("tcdgbn") : Dim sTcdGbn As String = .Text
                                If sTcdGbn = "P" And sTestCd = CType(raRst(intIdx), RST_INFO).TestCd.Substring(0, 5) Then
                                    .Col = .GetColFromID("chk") : .Text = "1"
                                    Exit For
                                End If
                            Next
                        End If
                        Exit For
                    End If
                Next
            End With
        Next

    End Sub

    Private Sub sbDisplay_Update()

        Dim aryRst As New ArrayList
        Dim sBcNo As String = ""
        Dim sTestCd As String = ""
        Dim sOrgRst As String = ""
        Dim sViewRst As String = ""
        Dim sRstCmt As String = ""
        Dim sChk As String = ""
        Dim sIUD As String = ""

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk") : sChk = .Text
                .Col = .GetColFromID("iud") : sIUD = .Text
                .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                .Col = .GetColFromID("viewrst") : sViewRst = .Text
                .Col = .GetColFromID("rstcmt") : sRstCmt = .Text
                .Col = .GetColFromID("testcd") : sTestCd = .Text
                .Col = .GetColFromID("bcno") : sBcNo = .Text

                Dim objRstInfo As New RST_INFO

                With objRstInfo
                    .BcNo = sBcNo
                    .Chk = sChk
                    .IUD = sIUD
                    .TestCd = sTestCd
                    .OrgRst = sOrgRst
                    .ViewRst = sViewRst
                    .RstCmt = sRstCmt
                End With

                aryRst.Add(objRstInfo)
            Next
        End With


        Dim dt As New DataTable
        dt = LISAPP.COMM.RstFn.fnGet_Result_bcno(msBcNo, msPartSlip, Me.chkBcnoAll.Checked, msTestCds, msWkGrpCd, msEqCd)

        sbDisplay_ResultView(dt)

        With Me.spdResult
            For iRow = 1 To .MaxRows
                For intidx = 0 To aryRst.Count - 1
                    .Row = iRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("bcno") : sBcNo = .Text

                    If sBcNo = CType(aryRst.Item(intidx), RST_INFO).BcNo And sTestCd = CType(aryRst(intidx), RST_INFO).TestCd Then
                        .Row = iRow : .Col = .GetColFromID("chk") : .Text = CType(aryRst.Item(intidx), RST_INFO).Chk
                        .Row = iRow : .Col = .GetColFromID("iud") : .Text = CType(aryRst.Item(intidx), RST_INFO).IUD
                        .Row = iRow : .Col = .GetColFromID("orgrst") : .Text = CType(aryRst.Item(intidx), RST_INFO).OrgRst
                        .Row = iRow : .Col = .GetColFromID("viewrst") : .Text = CType(aryRst.Item(intidx), RST_INFO).ViewRst
                        .Row = iRow : .Col = .GetColFromID("rstcmt") : .Text = CType(aryRst.Item(intidx), RST_INFO).RstCmt
                        Exit For
                    End If
                Next

            Next

        End With
    End Sub

    Private Function fnFind_Diff_ABO_Type() As Boolean
        Dim sFn As String = "fnFind_Diff_ABO_Type() as boolean"

        Try
            Dim iRow As Integer = 0
            Dim iDiff As Integer = 0

            Dim sRstCur As String = ""
            Dim sRstPre As String = ""

            With Me.spdResult
                iRow = .SearchCol(.GetColFromID("bbttype"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iRow < 1 Then Return False

                For intIdx As Integer = 1 To .MaxRows
                    .Row = intIdx
                    .Col = .GetColFromID("bbttype")
                    If Not .Text = "" Then
                        .Col = .GetColFromID("viewrst") : sRstCur = .Text.Trim
                        .Col = .GetColFromID("bfviewrst1") : sRstPre = .Text.Trim

                        If sRstCur.Length * sRstPre.Length > 0 Then
                            If sRstCur <> sRstPre Then
                                iDiff += 1
                            End If
                        End If
                    End If
                Next

                If iDiff > 0 Then
                    If MsgBox("입력한 결과가 이전 결과와 다릅니다. 계속하시겠습니까?", MsgBoxStyle.Critical Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return False
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(sFn + ": " + ex.Message)

        End Try
    End Function

    Private Sub spdResult_KeyUpEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdResult.KeyUpEvent
        Dim sFn As String = "Sub spdOrdList_KeyUpEvent(Object, AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdOrdListR.KeyUpEvent"

        Dim sTestCd As String = ""
        Dim sRst As String = ""
        Dim sColorO As Color

        Select Case Convert.ToInt32(e.keyCode)
            Case 37, 38, 39, 40, 229 ' 화살표 키                
            Case 27     ' ESC
            Case Keys.F4, Keys.F9, Keys.F11, Keys.F12
            Case 13
            Case Else
                With Me.spdResult
                    If .ActiveCol <> .GetColFromID("orgrst") Then
                        Exit Sub
                    End If
                    .Row = .ActiveRow
                    .Col = .GetColFromID("orgrst") : sColorO = .BackColor

                    If sColorO <> Color.LightGray Then '<20131107 회색(입력불가)시 이벤트 실행하지 않음
                        .Col = .GetColFromID("testcd") : sTestCd = .Text
                        .Col = .GetColFromID("orgrst") : sRst = .Text

                        DP_Common.sbDispaly_test_rstcd(m_dt_RstCdHelp, Convert.ToString(sTestCd), lstCode)  ' 검사항목별 결과코드 표시

                        Me.txtOrgRst.Text = sRst
                        Me.txtTestCd.Text = sTestCd

                        DP_Common.sbFindPosition(Me.lstCode, sRst)

                        '결과입력 불가로 주석처리
                        If Me.pnlCode.Visible = False Then
                            If Me.lstCode.Items.Count > 0 Then
                                Me.pnlCode.Visible = True
                            Else
                                Me.pnlCode.Visible = False
                            End If
                        End If
                    Else
                        .Col = .GetColFromID("orgrst") : .Text = ""
                        .Col = .GetColFromID("viewrst") : .Text = ""
                    End If
                End With
        End Select

    End Sub

    Private Sub spdResult_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdResult.LeaveCell

        If mbLeveCellGbn = False Then Return

        With Me.spdResult

            If e.row > 0 And e.col = .GetColFromID("orgrst") Then
                .Row = e.row
                .Col = e.col

                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then .ForeColor = CType(IIf(.BackColor = Color.White, Color.White, .BackColor), Color)
            End If

            Dim sBcNo As String = ""
            Dim sTestCd As String = ""

            If e.newRow > 0 Then
                .Row = e.newRow
                .Col = .GetColFromID("bcno") : sBcNo = .Text
                .Col = .GetColFromID("testcd") : sTestCd = .Text

                RaiseEvent ChangedTestCd(sBcNo, sTestCd)

                If e.newCol = .GetColFromID("orgrst") Then
                    Me.btnExmAdd.Enabled = False

                    .Row = e.newRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text.Substring(0, 5)
                    .Col = .GetColFromID("bbttype") : Dim sBbtType As String = .Text

                    sbDisplay_ExmAdd(sTestCd)

                    .Row = e.newRow
                    .Col = e.newCol
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit And (sBbtType <> "1" Or .BackColor = Color.White) Then
                        .ForeColor = Color.Black
                    End If

                    .Row = e.newRow
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("slipcd") : Dim sSlipCd As String = .Text

                    For ix As Integer = 0 To Me.cboSlip.Items.Count - 1
                        Me.cboSlip.SelectedIndex = ix
                        If Ctrl.Get_Code(Me.cboSlip) = sSlipCd Then Exit For
                    Next

                    If Me.txtBcNo.Text <> sBcNo Then
                        For ix As Integer = 0 To cboBcNos.Items.Count - 1
                            Me.cboBcNos.SelectedIndex = ix
                            If Fn.BCNO_View(sBcNo, True) = Me.cboBcNos.Text Then Exit For
                        Next

                        If Me.txtCmtCont.Text.Trim = vbCrLf Then Me.txtCmtCont.Text = ""

                        Me.txtBcNo.Text = sBcNo

                        RaiseEvent ChangedBcNo(Me.txtBcNo.Text)
                    End If
                    sbDisplay_RegNm_Test(sTestCd)
                End If
            End If
        End With

    End Sub

    'Private Sub spdResult_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdResult.RightClick
    '    Dim sFn As String = "Sub spdOrdListR_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdOrdListR.RightClick"
    '    Try
    '        If e.row < 1 Then Me.txtTestCd.Text = "" : Return

    '        Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me)
    '        Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me.spdResult)

    '        Dim sTnmd As String = ""

    '        With Me.spdResult
    '            .Row = e.row
    '            .Col = .GetColFromID("testcd") : Me.txtTestCd.Text = .Text

    '            If e.col = .GetColFromID("orgrst") And e.row > 0 Then
    '                .Row = e.row
    '                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
    '                .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
    '                .Col = .GetColFromID("tnmd") : sTnmd = .Text

    '                If sTestCd.Length > 5 Then

    '                    For ix = e.row - 1 To 1 Step -1
    '                        .Row = ix
    '                        .Col = .GetColFromID("testcd") : Dim sTemp1 As String = .Text
    '                        .Col = .GetColFromID("tnmd") : Dim sTemp2 As String = .Text

    '                        If sTemp1.Length = 5 Then
    '                            sTnmd = sTemp2
    '                            Exit For
    '                        End If
    '                    Next
    '                End If

    '                If sTestCd <> "" Then sTestCd = sTestCd.Substring(0, 5)

    '                Dim sFormGbn As String = COMM.RstFN.fnGet_ManualDiff_FormGbn(sTestCd, sSpcCd)   ''' diffformgbn 있는지 조회 

    '                If sFormGbn <> "" Then sbDisplay_KeyPad(sFormGbn, sTestCd, sSpcCd, sTnmd)
    '            End If
    '        End With
    '    Catch ex As Exception
    '        sbLog_Exception(ex.Message)
    '    End Try

    'End Sub

    Private Sub spdResult_TextTipFetch(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles spdResult.TextTipFetch

        With spdResult
            Select Case e.col
                Case .GetColFromID("normal")
                    e.showTip = True
                    .Col = e.col
                    .Row = e.row
                    e.tipText = .CellNote
                Case .GetColFromID("reftxt")
                    e.showTip = True

                    Dim sHelp As String = fnGetTextTipFetch(Me.spdResult, e.row)

                    e.tipText = sHelp

                    Dim sBuf() As String = Split(sHelp, vbCrLf)
                    Dim sMaxHelp As Single
                    Dim sHelpWidth As Single
                    For iRow As Integer = 0 To UBound(sBuf)
                        sHelpWidth = Me.CreateGraphics.MeasureString(sBuf(iRow), .Font).Width
                        If sMaxHelp < sHelpWidth Then
                            sMaxHelp = sHelpWidth
                        End If
                        '
                    Next
                    e.tipWidth = CInt(sMaxHelp) * 14
            End Select

        End With

    End Sub

    Private Sub lstCode_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCode.DoubleClick

        Dim sRstCd As String = ""
        Dim sBufRstCd() As String

        Dim sRst As String = ""
        Dim sRstCmt As String = ""
        Try
            With spdResult
                If lstCode.SelectedIndex > -1 Then
                    For i As Integer = 0 To lstCode.SelectedIndices.Count - 1
                        sBufRstCd = Split(lstCode.Items(lstCode.SelectedIndices(i)).ToString(), Chr(9))
                        sRst = sBufRstCd(1)
                        If sBufRstCd(2).Trim <> "" Then
                            sRstCmt = sBufRstCd(2)
                        End If
                    Next

                    .Row = .ActiveRow
                    .Col = .GetColFromID("orgrst") : .Text = sRst.Replace("'", "`")
                    If .GetColFromID("rstcmt") > 0 Then
                        .Col = .GetColFromID("rstcmt") : .Text = sRstCmt
                    End If
                End If
                If .GetColFromID("orgrst") > 0 Then
                    .Row = .ActiveRow
                    .Col = .GetColFromID("orgrst") : sRst = .Text.Replace("'", "`")
                    .Col = .GetColFromID("viewrst") : .Text = sRst
                End If

                sbSet_ResultView(spdResult.ActiveRow)

                .Col = .GetColFromID("orgrst")
                .Focus()
            End With

            Me.lstCode.Items.Clear()
            Me.lstCode.Hide()
            Me.pnlCode.Visible = False

        Catch ex As Exception

        End Try

    End Sub

    Private Sub lstCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstCode.KeyDown

        Try
            Select Case e.KeyCode
                Case Windows.Forms.Keys.Escape
                    Me.lstCode.Hide()
                    Me.pnlCode.Visible = False
                Case Windows.Forms.Keys.Enter
                    Me.lstCode_DoubleClick(lstCode, New System.EventArgs())
                Case Else
                    Dim sRst As String = ""
                    With Me.spdResult
                        If .ActiveCol <> .GetColFromID("orgrst") Then
                            Exit Sub
                        End If
                        .Row = .ActiveRow
                        .Col = .GetColFromID("orgrst") : sRst = .Text

                        Me.txtOrgRst.Text = Me.txtOrgRst.Text + Convert.ToChar(e.KeyCode).ToString()

                    End With
            End Select
        Catch ex As Exception
            MsgBox("lstCode_KeyDown")
        End Try

    End Sub

    Private Sub btnExamAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExmAdd.Click

        Try

            With spdResult
                .Row = .ActiveRow
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text.Substring(0, 5)
                .Col = .GetColFromID("spccd") : Dim sSpccd As String = .Text

                Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_WithParent(sTestCd, sSpccd)
                Dim a_dr As DataRow() = dt.Select("viwsub = '0'", "")
                dt = Fn.ChangeToDataTable(a_dr)

                Dim objHelp As New CDHELP.FGCDHELP01
                Dim alList As New ArrayList

                objHelp.FormText = "추가항목"
                objHelp.MaxRows = 15

                objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
                objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

                alList = objHelp.Display_Result(moForm, btnExmAdd.Left, btnExmAdd.Top, dt)

                If alList.Count > 0 Then
                    For intidx As Integer = 0 To alList.Count - 1
                        For introw As Integer = 1 To .MaxRows
                            .Row = introw
                            .Col = .GetColFromID("testcd")

                            If alList.Item(intidx).ToString.Split("|"c)(1) = .Text Then
                                .Row = introw
                                .RowHidden = False
                            End If
                        Next
                    Next
                End If

            End With
        Catch ex As Exception
            sbLog_Exception("btnExamAdd_Click : " + ex.Message)
        End Try
    End Sub

    Private Sub axCalcRst_OnSelectedCalcRstInfos(ByVal r_al As System.Collections.ArrayList) Handles axCalcRst.OnSelectedCalcRstInfos
        sbDisplayCalRst_Info(r_al)
    End Sub

    Private Sub AxRstInput_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        RaiseEvent FunctionKeyDown(sender, New System.Windows.Forms.KeyEventArgs(e.KeyCode))
    End Sub

    Private Sub btnHelp_Cmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_Cmt.Click
        Dim sFn As String = "Handles btnHelp_Cmt.Click"

        If Ctrl.Get_Code(Me.cboSlip).Trim = "" Then
            CDHELP.FGCDHELPFN.fn_PopMsg(moForm, "I"c, "검사분야를 선택하세요.!!")
            Return
        End If

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim arlList As New ArrayList

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_cmtcont_slip(Ctrl.Get_Code(Me.cboSlip), Me.txtCmtCd.Text)

            objHelp.FormText = "소견정보"

            objHelp.GroupBy = ""
            objHelp.OrderBy = ""
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("'' chk", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("cmtcd", "코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("cmtcont", "내용", 60, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(btnHelp_Cmt)

            arlList = objHelp.Display_Result(moForm, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + btnHelp_Cmt.Height + 80, dt)

            Dim sCmtCont As String = ""

            If arlList.Count > 0 Then

                For ix = 0 To arlList.Count - 1
                    If ix <> 0 Then sCmtCont += vbCrLf
                    sCmtCont += arlList.Item(ix).ToString.Split("|"c)(1)
                Next
            End If

            If sCmtCont <> "" Then
                If Me.txtCmtCont.Text = "" Then
                    Me.txtCmtCont.Text += sCmtCont
                Else
                    If Me.txtCmtCont.Text.Substring(Me.txtCmtCont.Text.Length - 1) = vbCrLf Then
                        Me.txtCmtCont.Text += sCmtCont
                    Else
                        Me.txtCmtCont.Text += vbCrLf + sCmtCont
                    End If
                End If

                Me.txtCmtCont.Focus()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Sub btnKeyPad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeyPad.Click

        Dim alTest As New ArrayList

        With spdResult
            For iRow As Integer = 1 To .MaxRows
                .Row = iRow
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
                .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text

                If sTestCd <> "" Then sTestCd = sTestCd.Substring(0, 5)

                If alTest.Contains(sTestCd) Then
                Else
                    alTest.Add(sTestCd)

                    Dim sFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(sTestCd, sSpcCd)
                    sbDisplay_KeyPad(sFormGbn, sTestCd, sSpcCd, sTnmd)
                End If
            Next
        End With

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        spdResult.ColsFrozen = spdResult.GetColFromID("tnmd")

        m_dbl_RowHeightt = spdResult.get_RowHeight(1)

    End Sub

    Private Sub btnCmt_Clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCmt_Clear.Click

        If Ctrl.Get_Code(Me.cboSlip).Trim = "" Then
            CDHELP.FGCDHELPFN.fn_PopMsg(moForm, "I"c, "검사분야를 선택하세요.!!")
            Return
        End If

        Me.txtCmtCont.Text = ""

        Dim ci As New CMT_INFO

        With ci
            .BcNo = cboBcNos.Text.Replace("-", "")
            .PartSlip = Ctrl.Get_Code(cboSlip)
            .CmtCont = Me.txtCmtCont.Text
        End With

        sbSet_Cmt_BcNo_Edit(ci)
    End Sub

    Private Sub txtOrgRst_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrgRst.TextChanged
        DP_Common.sbFindPosition(lstCode, Convert.ToString(txtOrgRst.Text))
    End Sub

    Private Sub mnuSpRst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSpRst.Click
        If Me.txtTestCd.Text = "" Then Return

        Dim sSpRstYn As String = LISAPP.COMM.RstFn.fnGet_SpRst_yn(IIf(Me.txtBcNo.Text = "", msBcNo, Me.txtBcNo.Text).ToString.Replace("-", ""), Me.txtTestCd.Text.Substring(0, 5))
        If sSpRstYn = "" Then Return

        RaiseEvent Call_SpRst(IIf(Me.txtBcNo.Text = "", msBcNo, Me.txtBcNo.Text).ToString.Replace("-", ""), Me.txtTestCd.Text.Substring(0, 5))
    End Sub

    Private Sub mnuKeypad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuKeypad.Click

        If Me.txtTestCd.Text = "" Then
            MsgBox("검사항목이 선택되지 않았습니다.!!" + vbCrLf + "다시 클릭 후 실행하세요.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information)
            Return
        End If

        With Me.spdResult
            .Row = .ActiveRow
            .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
            .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text

            Dim sFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(Me.txtTestCd.Text.Substring(0, 5), sSpcCd)
            If sFormGbn <> "" Then sbDisplay_KeyPad(sFormGbn, Me.txtTestCd.Text.Substring(0, 5), sSpcCd, sTnmd)

        End With
    End Sub

    Private Sub txtCmtCd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCmtCd.GotFocus
        Me.txtCmtCd.SelectionStart = 0
        Me.txtCmtCd.SelectAll()
    End Sub

    Private Sub txtCmtCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        btnHelp_Cmt_Click(Nothing, Nothing)

        Me.txtCmtCd.Text = ""
    End Sub

    Private Sub chkBcnoAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBcnoAll.CheckedChanged
        Me.spdResult.ReDraw = False
        Me.spdResult.MaxRows = 0
        If msBcNo = "" Then Return

        sbDisplay_Result(msBcNo, False)
    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbDisplay_Cmt_One_slipcd(cboBcNos.Text.Replace("-", ""), Ctrl.Get_Code(Me.cboSlip))
        'If msXpertTcd = True Then '2019-09-17 yjy Xpert PCR Critical 판정 시 결과소견 자동 입력 추가 (msXpertTcd = True -> LG104검사가 Critical판정일 경우 1주일 검사결과 소견으로 추가)
        '    sbDisplay_XPertCmt(cboBcNos.Text.Replace("-", ""))
        'End If
    End Sub

    Private Sub txtCmtCont_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCmtCont.LostFocus

        If Ctrl.Get_Code(Me.cboSlip).Trim = "" Then
            'CDHELP.FGCDHELPFN.fn_PopMsg(moForm, "I"c, "검사분야를 선택하세요.!!")
            Return
        End If

        Dim ci As New CMT_INFO

        With ci
            .BcNo = Me.cboBcNos.Text.Replace("-", "")
            .PartSlip = Ctrl.Get_Code(Me.cboSlip)
            .CmtCont = Me.txtCmtCont.Text
        End With

        sbSet_Cmt_BcNo_Edit(ci)

    End Sub

    Private Sub cboBcNos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBcNos.SelectedIndexChanged
        sbDisplay_slip(Me.cboBcNos.Text.Replace("-", ""))
    End Sub

    Private Sub btnReg_UnFit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_UnFit.Click
        Dim sFn As String = "Handles btnReg_UnFit.Click"

        Dim alTclsCds As New ArrayList

        With spdResult
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("tclscd") : Dim sTclsCd As String = .Text

                If sChk = "1" And alTclsCds.Contains(sTclsCd) = False Then
                    alTclsCds.Add(sTclsCd)
                End If
            Next

        End With


        Dim frmChild As Windows.Forms.Form
        frmChild = New FGUNFITSPC(msBcNo, alTclsCds)

        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()
    End Sub

    Private Sub btnQryFNModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQryFNModify.Click
        Dim objForm As New FGMODIFY
        objForm.Display_Data(moForm, msBcNo)
    End Sub

    Private Sub btnReg_Abn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_Abn.Click
        Dim sFn As String = "Handles btnReg_UnFit.Click"

        Dim alTclsCds As New ArrayList

        With spdResult
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                If sChk = "1" And alTclsCds.Contains(sTestCd) = False Then
                    alTclsCds.Add(sTestCd)
                End If
            Next

        End With

        Dim sSlipCd As String = msPartSlip
        If sSlipCd = "" Then SlipCd = Ctrl.Get_Code(Me.cboSlip)

        Dim frmChild As Windows.Forms.Form
        frmChild = New FGABNORMAL(msBcNo, sSlipCd, mbBloodBank, False, alTclsCds)

        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()
    End Sub

    Private Sub btnReg_tat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_tat.Click
        Dim sFn As String = "Handles btnReg_tat_Click.Click"

        Dim alTestCd As New ArrayList

        With spdResult
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                If sChk = "1" And alTestCd.Contains(sTestCd) = False Then
                    alTestCd.Add(sTestCd)
                End If
            Next

        End With

        Dim sSlipCd As String = msPartSlip
        If sSlipCd = "" Then sSlipCd = Ctrl.Get_Code(Me.cboSlip)

        Dim frmChild As Windows.Forms.Form
        frmChild = New FGTAT(msBcNo, sSlipCd, alTestCd)

        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()
    End Sub

    Private Sub btnBldInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBldInfo.Click
        Dim sFn As String = "Handles btnBldInfo_Click.Click"
        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim sRstDt As String = Me.lblReg.Text
            If sRstDt = "" Then
                sRstDt = Format(Now, "yyyyMMdd").ToString
            Else
                sRstDt = sRstDt.Substring(0, 10).Replace("-", "")
            End If

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim dt As DataTable = LISAPP.APP_BT.CGDA_BT.fn_GetPastTnsList(msRegNo, sRstDt)

            objHelp.FormText = "검체정보"

            objHelp.MaxRows = 10
            objHelp.Distinct = True

            objHelp.AddField("tnsjubsuno", "수혈의뢰 접수번호", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnsgbn", "수혈구분", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("comnm", "성분제제", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("reqqnt", "의뢰", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("outqnt", "출고", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("rtnqnt", "반납", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("abnqnt", "폐기", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("cancelqnt", "취소", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(moForm)
            pntCtlXY = Fn.CtrlLocationXY(Me.btnBldInfo)

            objHelp.Display_Result(moForm, pntFrmXY.X + pntCtlXY.X - Me.btnBldInfo.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnBldInfo.Height + 80, dt)

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub mnuHelp_rst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelp_rst.Click
        Dim sFn As String = "Handles btnHelp_Cmt.Click"

        If Ctrl.Get_Code(Me.cboSlip) = "" Then Return

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TestRst_list(Me.txtTestCd.Text)

            objHelp.FormText = "결과코드"

            objHelp.GroupBy = ""
            objHelp.OrderBy = ""
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("'' chk", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("keypad", "코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("rstcont", "내용", 60, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(btnHelp_Cmt)

            alList = objHelp.Display_Result(moForm, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + btnHelp_Cmt.Height + 80, dt)

            Dim sRstVal As String = ""

            If alList.Count > 0 Then

                For ix = 0 To alList.Count - 1
                    If ix <> 0 Then sRstVal += vbCrLf
                    sRstVal += alList.Item(ix).ToString.Split("|"c)(1)
                Next
            End If

            If sRstVal <> "" Then
                With Me.spdResult
                    .Row = .ActiveRow
                    .Col = .GetColFromID("orgrst") : .Text = sRstVal

                    Dim sBuf() As String = sRstVal.Split(Chr(13))
                    .set_RowHeight(.ActiveRow, m_dbl_RowHeightt * sBuf.Length)
                End With

                Me.spdResult_KeyDownEvent(Me.spdResult, New AxFPSpreadADO._DSpreadEvents_KeyDownEvent(13, 0))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDebug_cmt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        Dim fdebug As New FDEBUG

        fdebug.TopMost = True

        dt = m_dt_Cmt_bcno

        With fdebug.spd
            .MaxCols = dt.Columns.Count
            .MaxRows = dt.Rows.Count

            For j As Integer = 1 To dt.Columns.Count
                .SetText(j, 0, dt.Columns(j - 1).ColumnName)
            Next

            For i As Integer = 1 To dt.Rows.Count
                For j As Integer = 1 To dt.Columns.Count
                    .SetText(j, i, dt.Rows(i - 1).Item(j - 1).ToString())
                Next
            Next
        End With

        fdebug.Show()
    End Sub

    Private Sub btnQuery_Abn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery_Abn.Click


        Dim frmChild As Windows.Forms.Form
        frmChild = New FGABNQUERY(msRegNo)

        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()
    End Sub

    Private Sub btnCVRsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCVRsend.Click

        Try

            Dim Info_arry As New ArrayList

            With spdResult

                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("iud") : Dim chk As String = .Text

                    If chk = "1" Then

                        Dim CvrInfo As New LIS_CVR_INFO

                        .Col = .GetColFromID("tnmd") : Dim tnmd As String = .Text
                        .Col = .GetColFromID("bcno") : Dim bcno As String = .Text
                        .Col = .GetColFromID("tclscd") : Dim tclscd As String = .Text
                        .Col = .GetColFromID("testcd") : Dim testcd As String = .Text
                        .Col = .GetColFromID("viewrst") : Dim rst As String = .Text
                        .Col = .GetColFromID("rstflg") : Dim rstflg As String = .Text
                        .Col = .GetColFromID("tcdgbn") : Dim tcdgbn As String = .Text
                        .Col = .GetColFromID("titleyn") : Dim titleyn As String = .Text
                        .Col = .GetColFromID("rstunit") : Dim rstunit As String = .Text

                        If ((tcdgbn = "C" And rstflg = "") Or _
                            (tcdgbn = "B" And titleyn = "1") Or _
                            (tcdgbn = "P" And titleyn = "1")) Or rst = "" Then

                            Continue For
                        End If

                        Dim dt As DataTable = LISAPP.COMM.RstFn.Fnget_Fkocs(bcno, tclscd)
                        Dim fkocs As String = ""
                        Dim orddt As String = ""

                        If dt.Rows.Count > 0 Then
                            fkocs = dt.Rows(0).Item("ocs_key").ToString
                            orddt = dt.Rows(0).Item("orddt").ToString
                        End If

                        CvrInfo.Orddt = orddt
                        CvrInfo.Fkocs = fkocs
                        CvrInfo.Tnmd = tnmd
                        CvrInfo.Testcd = testcd
                        CvrInfo.Rst = rst
                        CvrInfo.RstUnit = rstunit

                        Dim a_dr As DataRow()
                        a_dr = m_dt_RstUsr.Select("testcd = '" + testcd + "'")

                        Dim Rstdt As String = ""
                        Dim Rstid As String = ""

                        Select Case rstflg

                            Case "1" '결과저장
                                Rstdt = a_dr(0).Item("regdt").ToString.Replace("-", "").Replace(":", "").Replace(" ", "")
                                Rstid = a_dr(0).Item("regid").ToString
                            Case "2" '중간보고
                                Rstdt = a_dr(0).Item("mwdt").ToString.Replace("-", "").Replace(":", "").Replace(" ", "")
                                Rstid = a_dr(0).Item("mwid").ToString
                            Case "3" '최종보고
                                Rstdt = a_dr(0).Item("fndt").ToString.Replace("-", "").Replace(":", "").Replace(" ", "")
                                Rstid = a_dr(0).Item("fnid").ToString

                        End Select

                        CvrInfo.Rstdt = Rstdt
                        CvrInfo.Rstid = Rstid

                        Info_arry.Add(CvrInfo)

                    End If

                Next

                '<< CVR 등록
                If Info_arry.Count > 0 Then

                    With (New LISAPP.APP_R.AxRstFn)
                        If .fnExe_CVR(msBcNo, msRegNo, Info_arry, USER_INFO.USRID, USER_INFO.USRNM) = "" Then
                            MsgBox("CVR 등록되었습니다.")


                        End If
                    End With

                Else
                    MsgBox("CVR 등록할 검사항목을 선택하세요.")
                End If

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnCVRList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCVRList.Click

        Try

            Dim frm As New AxAckResult.FPOPUP_CVR
            frm.Display_Data(frm, msBcNo)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'JJH 자동소견 >> 기초마스터에 설정된 검사코드의 파트별로 들어가도록 수정   ( 기존 : 왼쪽 조회성 분야 combobox에 따라 들어가다보니 한 검체에 여러 파트가 있을경우 중복으로 들어가는 문제 )
    Private Sub sbGet_CvtCmtInfo_TestCd(ByVal rsBcNo As String, ByVal rbLisMode As Boolean)

        Try
            Dim alRst As New ArrayList
            Dim sBcNo As String = ""
            Dim sTestCd As String = "", sSpcCd As String = "", sOrgRst As String = "", sViewRst As String = "", sHLmark As String = "", sEqFlag As String = "", sRegNo As String = "",
                rSlipcd As String = ""


            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text
                    .Col = .GetColFromID("hlmark") : sHLmark = .Text
                    .Col = .GetColFromID("eqflag") : sEqFlag = .Text
                    .Col = .GetColFromID("slipcd") : rSlipcd = .Text

                    If sOrgRst <> "" Then
                        Dim objRst As New STU_CvtCmtInfo

                        objRst.BcNo = sBcNo
                        objRst.TestCd = sTestCd
                        objRst.OrgRst = sOrgRst
                        objRst.ViewRst = sViewRst
                        objRst.HlMark = sHLmark
                        objRst.EqFlag = sEqFlag
                        objRst.SlipCd = rSlipcd

                        alRst.Add(objRst)

                        Dim alCvtCmt As New ArrayList
                        alCvtCmt = LISAPP.COMM.CvtCmt.fnCvtCmtInfo(rsBcNo, alRst, rSlipcd, rbLisMode)

                        If alCvtCmt.Count < 1 Then Continue For

                        Dim sCmt$ = ""
                        Dim sCmt2 As String = ""

                        For intIdx As Integer = 0 To alCvtCmt.Count - 1

                            sCmt += CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont

                            If CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont = "" Then
                                Me.txtCmtCont.Text = Me.txtCmtCont.Text.Replace(CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont_Base + vbCrLf, "")
                                Me.txtCmtCont.Text = Me.txtCmtCont.Text.Replace(CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont_Base, "")
                            End If

                            '// 검사의 분야를 combobox index 설정
                            For intSlip As Integer = 0 To Me.cboSlip.Items.Count - 1
                                If rSlipcd = Ctrl.Get_Code(Me.cboSlip.Items(intSlip).ToString) Then
                                    Me.cboSlip.SelectedIndex = intSlip
                                End If
                            Next
                        Next

                        '// YJY 결핵검사 진행 시 환자의 최근 CBC검사항목 결과 가져와 소견으로 Display.
                        sCmt2 = Pat_CBC_Rst(sTestCd)

                        Dim alTmp As New ArrayList
                        Dim sBuf1() As String = Me.txtCmtCont.Text.Replace(Chr(10), "").Split(Chr(13))
                        Dim sBuf2() As String = sCmt.Replace(Chr(10), "").Split(Chr(13))
                        Dim sBuf3() As String = sCmt2.Replace(Chr(10), "").Split(Chr(13))


                        For ix As Integer = 0 To sBuf1.Length - 1
                            alTmp.Add(sBuf1(ix).Trim())
                        Next


                        sCmt = ""
                        sCmt2 = ""

                        '20220323 원본
                        '결과소견 변경여부 체크
                        For ix As Integer = 0 To sBuf2.Length - 1
                            If alTmp.Contains(sBuf2(ix).Trim) = False Then
                                sCmt += sBuf2(ix) + vbCrLf
                            ElseIf sBuf2(ix) = " " Then '2022.03.24 JJH 소견 엔터값 추가
                                sCmt += vbCrLf
                            End If
                        Next

                        '2022.03.24 JJH 소견 엔터값 추가
                        If sCmt.Replace(vbCrLf, "") = "" Then
                            sCmt = ""
                        End If

                        '결핵균검사 변경여부 체크
                        For ix As Integer = 0 To sBuf3.Length - 1
                            If alTmp.Contains(sBuf3(ix).Trim) = False Then
                                'If sCmt2.Length = 0 Then
                                '    sCmt2 += sBuf3(ix) + vbCrLf
                                'Else
                                '    sCmt2 += sBuf3(ix) + vbCrLf
                                'End If
                                sCmt2 += sBuf3(ix) + vbCrLf
                            End If
                        Next

                        '결과자동소견 넣기
                        If sCmt <> "" Then
                            If Me.txtCmtCont.Text = "" Then
                                Me.txtCmtCont.Text = sCmt
                            Else
                                Me.txtCmtCont.Text += vbCrLf + sCmt
                            End If
                        End If

                        '결핵균검사 소견 넣기
                        If sCmt2 <> "" Then
                            If Me.txtCmtCont.Text = "" Then
                                Me.txtCmtCont.Text = sCmt2
                            Else
                                Me.txtCmtCont.Text += vbCrLf + sCmt2
                            End If
                        End If


                        txtCmtCont_LostFocus(Nothing, Nothing)

                    End If
                Next
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Private Function Pat_CBC_Rst(ByVal sTestcd As String) As String

        Dim sCmt2 As String = ""

        '< 2016-11-22 YJY 결핵검사 진행 시 환자의 최근 CBC검사항목 결과 가져와 소견으로 Display.
        If sTestcd = "LI611" Or sTestcd = "LI612" Or sTestcd = "LI613" Or sTestcd = "LI620" Then
            Dim a_dt As DataTable = New DataTable
            Dim stestisno611 As String = "", stestisno612 As String = "", stest611rdt As String = "", stest611rst As String = "", stest611rstunit As String = "",
            stest612rstunit As String = "", stest612rdt As String = "", stest612rst As String = ""

            'If msRegNoCmt <> "" Then 'LI611, LI612 검사로 판단되면
            a_dt = LISAPP.COMM.RstFn.fnGet_Pat_Recent_Rst(msRegNo) '환자의 최근 CBC검사 항목 가져오기

            '불러온 이전 결과 없을 경우 "기존의뢰 없음" 표시 
            If a_dt.Rows.Count = 0 Then
                If sCmt2 = "" Then
                    sCmt2 += "4. 과거 일반혈액 검사결과 " '2019-07-10 JJH 3->4 수정
                    sCmt2 += vbNewLine
                    sCmt2 += "   검사항목                                   검사시행날짜      실제결과 "
                    sCmt2 += vbNewLine
                    sCmt2 += "   WBC Count (CBC)                            기존의뢰 없음"
                    sCmt2 += vbNewLine
                    sCmt2 += "   Lymphocyte Count (WBC Differential Count)  기존의뢰 없음"
                End If
            Else
                '-결과 있을 경우 이전 결과 변수 담기
                For i As Integer = 0 To a_dt.Rows.Count - 1
                    If a_dt.Rows(i).Item("testcd").ToString.Equals("LH101") Then
                        stest611rdt = a_dt.Rows(i).Item("rstdtd").ToString
                        stest611rst = a_dt.Rows(i).Item("viewrst").ToString
                        stest611rstunit = a_dt.Rows(i).Item("rstunit").ToString
                    ElseIf a_dt.Rows(i).Item("testcd").ToString.Equals("LH12103") Then
                        stest612rdt = a_dt.Rows(i).Item("rstdtd").ToString
                        stest612rst = a_dt.Rows(i).Item("viewrst").ToString
                        stest612rstunit = a_dt.Rows(i).Item("rstunit").ToString
                    End If
                Next
                '-
                If stest611rdt = "" Then
                    stestisno611 = "기존의뢰 없음"
                ElseIf stest612rdt = "" Then
                    stestisno612 = "기존의뢰 없음"
                End If
                '-자동 소견 양식 만들고 이전 결과 넣어 주기
                sCmt2 += "4. 과거 일반혈액 검사결과 "  '2019-07-10 JJH 3->4 수정
                sCmt2 += vbNewLine
                sCmt2 += "   검사항목                                   검사시행날짜      실제결과 "
                sCmt2 += vbNewLine
                If stestisno611 = "" Then
                    sCmt2 += "   WBC Count (CBC)                            " + stest611rdt + Space(8) + stest611rst + Space(1) + stest611rstunit
                Else
                    sCmt2 += "   WBC Count (CBC)                            " + stestisno611
                End If
                sCmt2 += vbNewLine
                If stestisno612 = "" Then
                    sCmt2 += "   Lymphocyte Count (WBC Differential Count)  " + stest612rdt + Space(8) + stest612rst + Space(1) + stest612rstunit
                Else
                    sCmt2 += "   Lymphocyte Count (WBC Differential Count)  " + stestisno612
                End If
                '-
            End If

        End If

        Return sCmt2

    End Function

End Class

