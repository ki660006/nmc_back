Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Public Class AxRstInput_wl
    Private Const msFile As String = "File : AxRstInput_wl.vb, Class : AxAckResult.AxRstInput_wl" + vbTab

    Private moForm As Windows.Forms.Form

    Public Event ChangedBcNo(ByVal BcNo As String)
    Public Event FunctionKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event Call_SpRst(ByVal BcNo As String, ByVal TestCd As String)

    Private msFormID As String = ""

    Private mbColHiddenYn As Boolean

    Private m_dt_RstUsr As DataTable
    Private m_dt_RstCdHelp As DataTable
    Private m_dt_Alert_Rule As DataTable

    Private mbLeveCellGbn As Boolean = True

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

            With spdResult
                If mbColHiddenYn Then
                    For iCol As Integer = 1 To .MaxCols
                        If iCol = .GetColFromID("chk") Or iCol = .GetColFromID("tnmd") Or iCol = .GetColFromID("orgrst") Or iCol = .GetColFromID("viewrst") Or _
                               iCol = .GetColFromID("rerunflg") Or iCol = .GetColFromID("history") Or iCol = .GetColFromID("reftxt") Or iCol = .GetColFromID("rstunit") Or _
                               iCol = .GetColFromID("hlmark") Or iCol = .GetColFromID("panicmark") Or iCol = .GetColFromID("deltamark") Or _
                               iCol = .GetColFromID("criticalmark") Or iCol = .GetColFromID("alertmark") Or iCol = .GetColFromID("rstflgmark") Or _
                               iCol = .GetColFromID("rstcmt") Or iCol = .GetColFromID("bfviewrst2") Or iCol = .GetColFromID("bffndt2") Or iCol = .GetColFromID("eqnm") Or _
                               iCol = .GetColFromID("testcd") Or iCol = .GetColFromID("spccd") Or iCol = .GetColFromID("tordcd") Or _
                               iCol = .GetColFromID("reftcls") Or iCol = .GetColFromID("eqflag") Or iCol = .GetColFromID("rerunrst") Or _
                               iCol = .GetColFromID("regno") Or iCol = .GetColFromID("patnm") Or iCol = .GetColFromID("sexage") Or _
                               iCol = .GetColFromID("deptward") Or iCol = .GetColFromID("spcnmd") Or iCol = .GetColFromID("workno") Or iCol = .GetColFromID("bcno") Then
                        Else
                            .Col = iCol : .ColHidden = True
                        End If
                    Next
                Else
                    For iCol = 1 To .MaxCols
                        .Col = iCol : .ColHidden = False
                    Next
                End If
            End With
        End Set
    End Property

    Public WriteOnly Property Form() As Windows.Forms.Form
        Set(ByVal value As Windows.Forms.Form)
            moForm = value
        End Set
    End Property

    Private Sub sbGet_Alert_Rule()
        Dim sFn As String = "sbGet_Alert_Rule"

        Try

            m_dt_Alert_Rule = LISAPP.APP_R.RstFn.fnGet_Alert_Rule()

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try

    End Sub

    Private Sub sbGet_Calc_Rst(ByVal riRow As Integer)

        Dim dt As New DataTable

        Try
            For ix1 As Integer = 1 To spdResult.MaxRows

                If ix1 = riRow Then Continue For

                Dim sBcNo As String = ""
                Dim sTestCd As String = ""
                Dim sSpcCd As String = ""
                Dim sCalGbn As String = ""

                With spdResult
                    .Row = ix1
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

                    For ix2 As Integer = 0 To iCalCnt - 1
                        Dim sChr As String = Chr(65 + ix2)
                        Dim sTCd As String = dt.Rows(0).Item("param" + ix2.ToString).ToString
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

                    Try
                        Dim sRst As String = LISAPP.COMM.CalcFn.fnGet_CFCompute(sCalForm)
                        If sRst <> "" Then
                            sRst = fnRstTypeCheck(ix1, sRst)

                            With spdResult
                                .Row = ix1
                                .Col = .GetColFromID("orgrst") : .Text = sRst
                                .Col = .GetColFromID("viewrst") : .Text = sRst

                                sbSet_ResultView(ix1)
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

    Public Sub sbFocus()

        Dim iUnLockRow As Integer = 0

        With Me.spdResult
            For iRow As Integer = 1 To .MaxRows
                .Row = iRow
                .Col = .GetColFromID("orgrst")
                If Not .Lock And .RowHidden = False Then
                    If iUnLockRow = 0 Then iUnLockRow = iRow

                    If .Text = "" Then
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then .ForeColor = Drawing.Color.Black
                        .SetActiveCell(.GetColFromID("orgrst"), iRow)
                        .Focus()

                        spdResult_ClickEvent(spdResult, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("orgrst"), iRow))

                        iUnLockRow = 0
                        Exit For
                    End If
                End If
            Next

            If iUnLockRow > 0 Then
                If .MaxRows > 0 Then
                    .SetActiveCell(.GetColFromID("orgrst"), iUnLockRow)
                    .Focus()

                    spdResult_ClickEvent(Me.spdResult, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("orgrst"), iUnLockRow))
                End If
            End If
        End With

    End Sub

    Private Sub sbLog_Exception(ByVal rsMsg As String)
        Me.lstEx.Items.Insert(0, rsMsg)
    End Sub

    Private Sub sbConvertFormat(ByVal riRow As Integer)
        Dim sFn As String = "sbConvertFormat"

        Dim sRst As String = ""
        Dim sViewRst As String = ""
        Dim iLen As Integer

        Try
            With spdResult
                .Row = riRow
                .Col = .GetColFromID("orgrst") : sRst = .Text
                .Col = .GetColFromID("viewrst") : sViewRst = .Text
                If IsNumeric(sRst) Then
                    If sRst.IndexOf(".") > -1 Then Return
                    If sRst.IndexOf("-") > -1 Then Return
                    If sRst.IndexOf("+") > -1 Then Return
                    If sRst.IndexOf("<") > -1 Then Return
                    If sRst.IndexOf(">") > -1 Then Return

                    If sViewRst.IndexOf("-") > -1 Then Return
                    If sViewRst.IndexOf("+") > -1 Then Return
                    If sViewRst.IndexOf("<") > -1 Then Return
                    If sViewRst.IndexOf(">") > -1 Then Return
                    If Not IsNumeric(sViewRst) Then Return

                    iLen = sRst.Replace(".", "").Length
                    Dim dblRst As Double = CDbl(sRst)

                    Select Case iLen
                        Case 3, 4, 5
                            .Col = .GetColFromID("viewrst") : .Text = Format(dblRst, "#,##0")
                        Case 6, 7, 8
                            .Col = .GetColFromID("viewrst") : .Text = Format(dblRst, "#,##0,000")
                        Case 9
                            .Col = .GetColFromID("viewrst") : .Text = Format(dblRst, "#,##0,000,000")
                        Case Else
                            .Col = .GetColFromID("viewrst") : .Text = sRst
                    End Select
                End If

            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString())

        End Try
    End Sub
    Private Sub sbSet_ResultView(ByVal riRow As Integer, Optional ByVal rbTest As Boolean = False)

        With Me.spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst")

            sbRstTypeCheck(riRow)
            sbHLCheck(riRow)
            sbPanicCheck(riRow, m_dt_RstCdHelp)
            sbUJudgCheck(riRow)
            sbDeltaCheck(riRow, m_dt_RstCdHelp)
            sbCriticalCheck(riRow)
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

    End Sub

    ' 결과 체크
    Private Sub sbSet_JudgRst()

        With Me.spdResult
            Dim strRst As String

            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk") : Dim strChk As String = .Text
                .Col = .GetColFromID("iud") : Dim strIUD As String = .Text

                If strChk = "1" Or strIUD = "1" Then
                    If .GetColFromID("orgrst") > 0 Then
                        .Col = .GetColFromID("orgrst") : strRst = .Text.Replace("'", "`") : .Text = strRst
                        .Col = .GetColFromID("viewrst") : .Text = strRst

                        If strRst <> "" Then
                            sbRstTypeCheck(intRow)    '-- 실제결과 -> 결과에 표시
                            sbHLCheck(intRow)
                            sbPanicCheck(intRow, m_dt_RstCdHelp)
                            sbUJudgCheck(intRow)
                            sbDeltaCheck(intRow, m_dt_RstCdHelp)
                            sbCriticalCheck(intRow)
                            sbAlertCheck(intRow)
                            sbAlimitCheck(intRow)
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
            Dim aryMsg As New ArrayList
            Dim strChk$ = "", strOrgRst$ = "", strViewRst$ = "", strRstCmt$ = "", sRstFlg$ = ""
            Dim strOrgRst_o$ = "", strViewRst_o$ = "", strRstCmt_o$ = ""
            Dim sBcno$ = "", sSlipCd$ = "", sTestCd$ = "", strTnmd$ = "", strTcdGbn$ = "", strTitleYn$ = "", strReqSub$ = ""
            Dim strAlert$ = "", strPanic$ = "", strDelta$ = "", strCritical$ = ""
            Dim strPlGbn$ = "", strBldGbn$ = "", strUsrId1$ = "", strUsrId2$ = "", strRst1$ = "", strRst2$ = ""
            Dim strBfViewRst As String = ""

            Dim sBcNo_OLD As String = "", sSlipCd_old As String = ""
            Dim sCmtCont As String = ""

            Dim blnFlag As Boolean = False

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("bcno") : sBcno = .Text
                    .Col = .GetColFromID("tnmd") : strTnmd = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : strTcdGbn = .Text
                    .Col = .GetColFromID("iud") : strChk = .Text
                    .Col = .GetColFromID("chk") : strChk = .Text

                    If strChk = "1" And strTcdGbn = "P" Then
                        For intidx As Integer = intRow + 1 To .MaxRows
                            .Row = intidx
                            .Col = .GetColFromID("iud") : strChk = .Text
                            .Col = .GetColFromID("orgrst") : strOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : strReqSub = .Text

                            If strOrgRst = "" And strReqSub = "1" Then
                                .Row = intidx
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd Then
                                    .Row = intidx
                                    .Col = .GetColFromID("rstflg")
                                    If .Text < rsRstFlg Then
                                        .Row = intRow
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

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("bcno") : sBcno = .Text
                    .Col = .GetColFromID("slipcd") : sSlipCd = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tnmd") : strTnmd = .Text
                    .Col = .GetColFromID("tcdgbn") : strTcdGbn = .Text
                    .Col = .GetColFromID("titleyn") : strTitleYn = .Text
                    .Col = .GetColFromID("reqsub") : strReqSub = .Text

                    .Col = .GetColFromID("iud") : strChk = .Text
                    .Col = .GetColFromID("orgrst") : strOrgRst = .Text
                    .Col = .GetColFromID("viewrst") : strViewRst = .Text
                    .Col = .GetColFromID("rstcmt") : strRstCmt = .Text
                    .Col = .GetColFromID("rstflg") : sRstFlg = .Text

                    .Col = .GetColFromID("alertmark") : strAlert = .Text
                    .Col = .GetColFromID("panicmark") : strPanic = .Text
                    .Col = .GetColFromID("deltamark") : strDelta = .Text
                    .Col = .GetColFromID("criticalmark") : strCritical = .Text

                    .Col = .GetColFromID("corgrst") : strOrgRst_o = .Text
                    .Col = .GetColFromID("cviewrst") : strViewRst_o = .Text
                    .Col = .GetColFromID("crstcmt") : strRstCmt_o = .Text

                    If strChk = "1" And strOrgRst <> "" Then

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

                        blnFlag = False

                        If rsRstFlg = "3" Then
                            If sRstFlg = "3" Then
                                If (strTcdGbn = "P" Or strTcdGbn = "B") And strTitleYn = "1" Then
                                Else
                                    If strOrgRst = strOrgRst_o And strViewRst = strViewRst_o And strRstCmt = strRstCmt_o Then
                                        aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 바뀐정보가 없습니다.")
                                        blnFlag = True
                                    ElseIf strOrgRst <> strOrgRst_o Or strViewRst <> strViewRst_o Then
                                        sCmtCont += strTnmd + "(" + strOrgRst_o + "/" + strViewRst_o + ")|"
                                    End If
                                End If
                            End If
                        End If

                        If rsRstFlg = "2" Then
                            If sRstFlg = "3" Then
                                If (strOrgRst <> strOrgRst_o Or strViewRst <> strViewRst_o) And strAlert = "" Then
                                    sCmtCont += strTnmd + "(" + strOrgRst_o + "/" + strViewRst_o + ")|"
                                Else
                                    aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 최종보고된 자료 입니다.")
                                    blnFlag = True
                                End If
                            ElseIf sRstFlg = "2" Then
                                If (strTcdGbn = "P" Or strTcdGbn = "B") And strTitleYn = "1" Then
                                Else
                                    If strOrgRst = strOrgRst_o And strViewRst = strViewRst_o And strRstCmt = strRstCmt_o Then
                                        aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 바뀐정보가 없습니다.")
                                        blnFlag = True
                                    End If
                                End If
                            End If
                        End If

                        If rsRstFlg = "1" Then
                            If sRstFlg = "3" Or sRstFlg = "2" Then
                                aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 " + IIf(sRstFlg = "3", "최종보고", "중간보고").ToString + "된 자료 입니다.")
                                blnFlag = True
                            Else
                                If (strTcdGbn = "P" Or strTcdGbn = "B") And strTitleYn = "1" Then
                                Else
                                    If strOrgRst = strOrgRst_o And strViewRst = strViewRst_o And strRstCmt = strRstCmt_o Then
                                        aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 바뀐정보가 없습니다.")
                                        blnFlag = True
                                    End If
                                End If
                            End If
                        End If

                        If strAlert = "A" And STU_AUTHORITY.AFNReg <> "1" Then
                            blnFlag = True
                            aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 Aleart에 대한 보고권한이 없습니다.")
                        End If

                        If strPanic = "P" And STU_AUTHORITY.PDFNReg <> "1" Then
                            blnFlag = True
                            aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 Panic에 대한 보고권한이 없습니다.")
                        End If

                        If strDelta = "D" And STU_AUTHORITY.DFNReg <> "1" Then
                            blnFlag = True
                            aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 Delta에 대한 보고권한이 없습니다.")
                        End If

                        If strCritical = "C" And STU_AUTHORITY.CFNReg <> "1" Then
                            blnFlag = True
                            aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 Critical에 대한 보고권한이 없습니다.")
                        End If

                        If sRstFlg = "3" Then
                            If strOrgRst <> strOrgRst_o And STU_AUTHORITY.FNUpdate <> "1" Then
                                blnFlag = True
                                aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 최종보고수정에 대한 보고권한이 없습니다.")
                            End If
                        Else
                            If strOrgRst <> strOrgRst_o And STU_AUTHORITY.RstUpdate <> "1" Then
                                blnFlag = True
                                aryMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 결과수정에 대한 보고권한이 없습니다.")
                            End If
                        End If

                        If blnFlag Then
                            .Row = intRow
                            .Col = .GetColFromID("iud") : .Text = ""
                        End If
                    End If
                Next

                '-- 2010/06/09 추가
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : strTcdGbn = .Text
                    .Col = .GetColFromID("iud") : strChk = .Text

                    If strChk = "1" And strTcdGbn = "P" Then
                        For intidx As Integer = intRow + 1 To .MaxRows
                            .Row = intidx
                            .Col = .GetColFromID("iud") : strChk = .Text
                            .Col = .GetColFromID("orgrst") : strOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : strReqSub = .Text
                            .Col = .GetColFromID("orgrst") : strOrgRst = .Text

                            If strOrgRst <> "" Then
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
                    sCmtCont = "다음과 같이 최종보고 되었던 자료 입니다." + vbCrLf + "[" + sCmtCont.Trim + "]"

                    Dim objCmt As New CMT_INFO

                    objCmt.BcNo = sBcNo_OLD
                    objCmt.PartSlip = sSlipCd_old
                    objCmt.CmtCont = sCmtCont

                    raCmtCont.Add(objCmt)

                    sCmtCont = ""
                End If

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : strTcdGbn = .Text
                    .Col = .GetColFromID("titleyn") : strTitleYn = .Text
                    .Col = .GetColFromID("rstflg") : sRstFlg = .Text

                    .Col = .GetColFromID("chk") : strChk = .Text

                    'If intRow = .MaxRows Then MsgBox("A")

                    If strChk = "1" And strTcdGbn = "P" And strTitleYn <> "0" Then
                        Dim intCnt% = 0
                        For intidx As Integer = intRow + 1 To .MaxRows
                            .Row = intidx
                            .Col = .GetColFromID("iud") : Dim strIUD As String = .Text
                            .Col = .GetColFromID("orgrst") : strOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : strReqSub = .Text
                            .Col = .GetColFromID("rstflg") : Dim strSubRstFlg As String = .Text
                            .Col = .GetColFromID("testcd") : Dim sTsubCd As String = .Text

                            If sTestCd <> sTsubCd.Substring(0, 5) Then Exit For
                            'If intidx = .MaxRows Then MsgBox("B")

                            If strIUD = "1" Then
                                .Row = intidx
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd Then
                                    intCnt += 1
                                Else
                                    Exit For
                                End If
                            ElseIf strReqSub = "1" And strOrgRst = "" Then
                                intCnt = 99
                                Exit For
                            ElseIf sRstFlg < strSubRstFlg Then
                                intCnt = 1
                            End If
                        Next

                        If intCnt = 0 Then
                            .Row = intRow
                            .Col = .GetColFromID("chk") : .Text = ""
                            .Col = .GetColFromID("iud") : .Text = ""
                        ElseIf intCnt = 99 Then
                            .Row = intRow
                            .Col = .GetColFromID("iud") : .Text = ""
                        End If

                    End If
                Next
            End With

            fnChecakReg = aryMsg
        Catch ex As Exception
            fnChecakReg = New ArrayList
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
                            If objRst.mAlertMark <> "" Or objRst.mPanicMark <> "" Or objRst.mDeltaMark <> "" Or objRst.mCriticalMark <> "" Then
                                objRst.mRstFlg = "2"
                            Else
                                objRst.mRstFlg = "3"
                                objRst.mCfmNm = IIf(rsCfmNm = "", sCfmNm, rsCfmNm).ToString
                                objRst.mCfmSign = rsCfmSign
                            End If
                        Else ''' 결과저장 
                            objRst.mRstFlg = rsRstflg
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

                fnGetRst = aryRst
            End With
        Catch ex As Exception
            fnGetRst = New ArrayList
        End Try

    End Function

    Private Sub sbGet_CvtRstInfo(ByVal rsBcNo As String, Optional ByVal rsTestCd As String = "", Optional ByVal rsIFGbn As Boolean = False)
        Try
            Dim alRst As New ArrayList
            Dim sBcNo As String = ""
            Dim sTestCd$ = "", sSpcCd$ = "", sOrgRst$ = "", sViewRst$ = "", sHLmark$ = ""

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
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
                            If CType(alCvtRst(ix), STU_RstInfo_cvt).CvtFldGbn = "R" Then

                                If CType(alCvtRst(ix), STU_RstInfo_cvt).CvtFldGbn = "B" Then
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

    Public Function fnReg(ByVal rsRstflg As String) As String
        Dim sFn As String = "Sub fnReg(string)"

        Dim RstInfo As New STU_RstInfo
        Dim SmpInfo As New STU_SampleInfo

        Dim alRstInfo As New ArrayList
        Dim alCmtInfo As New ArrayList
        Dim alOldCmmt As New ArrayList

        Dim alBcNotestcd As New ArrayList

        Dim sRstCmt As String = ""
        Dim sOrgRst As String = ""
        Dim sTestCd As String = ""
        Dim sRegNo As String = ""
        Dim sBcNo_cur As String = ""
        Dim sBcNo_old As String = ""
        Dim iCmtNo As Integer = 0

        mbLeveCellGbn = False

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim iDisable As Integer = 0

            Dim sMsg As String = ""

            Select Case rsRstflg
                Case "1"
                    sMsg += "결과저장 하시겠습니까?"
                Case "2"
                    sMsg += "결과확인 하시겠습니까?"
                Case "3"
                    sMsg += "결과검증 하시겠습니까?"
            End Select

            If MsgBox(sMsg, MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return ""

            sMsg = ""

            With Me.spdResult
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text

                    If sChk = "1" Then
                        .Row = iRow
                        .Col = .GetColFromID("bcno") : sBcNo_cur = .Text.Replace("-", "")
                        .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                        .Col = .GetColFromID("rstcmt") : sRstCmt = .Text
                        .Col = .GetColFromID("testcd") : sTestCd = .Text

                        If sBcNo_cur <> "" And sBcNo_cur <> sBcNo_old Then
                            If alRstInfo.Count > 0 Then

                                Dim alEditSuc As New ArrayList
                                Dim da_regrst As New LISAPP.APP_R.RegFn
                                Dim iReg As Integer

                                If PRG_CONST.BCCLS_MicorBio.Contains(sBcNo_old.Substring(8, 2)) Then
                                    iReg = da_regrst.RegServer(alRstInfo, SmpInfo, alEditSuc)
                                Else
                                    iReg = da_regrst.RegServer(alRstInfo, SmpInfo, alEditSuc, False)
                                End If

                                If iReg < 1 Then
                                    If sMsg <> "" Then
                                        sMsg += ", "
                                    End If
                                    sMsg += sBcNo_old

                                End If

                                sbDisplay_ResultOK(alBcNotestcd, iReg)
                            End If

                            alRstInfo.Clear()
                            alCmtInfo.Clear()
                            alOldCmmt.Clear()

                            alBcNotestcd.Clear()

                            iCmtNo = 0

                        End If

                        sBcNo_old = sBcNo_cur

                        .Row = iRow
                        .Col = .GetColFromID("regno") : sRegNo = .Text

                        If sOrgRst <> "" Then
                            RstInfo = New STU_RstInfo

                            RstInfo.TestCd = sTestCd
                            RstInfo.OrgRst = sOrgRst
                            RstInfo.RstCmt = sRstCmt

                            alRstInfo.Add(RstInfo)

                            alBcNotestcd.Add(sBcNo_cur + "|" + sTestCd)
                        End If

                        SmpInfo.BCNo = sBcNo_cur
                        SmpInfo.EqCd = ""
                        SmpInfo.UsrID = USER_INFO.USRID
                        SmpInfo.UsrIP = USER_INFO.LOCALIP
                        SmpInfo.IntSeqNo = ""
                        SmpInfo.Rack = ""
                        SmpInfo.Pos = ""
                        SmpInfo.EqBCNo = ""

                        '>
                        SmpInfo.SenderID = ""
                        SmpInfo.RegStep = rsRstflg

                    End If
                Next

                If alRstInfo.Count > 0 Then
                    Dim alEditSuc As New ArrayList
                    Dim da_regrst As New LISAPP.APP_R.RegFn
                    Dim iReg As Integer

                    If PRG_CONST.BCCLS_MicorBio.Contains(sBcNo_old.Substring(8, 2)) Then
                        iReg = da_regrst.RegServer(alRstInfo, SmpInfo, alEditSuc)
                    Else
                        iReg = da_regrst.RegServer(alRstInfo, SmpInfo, alEditSuc, False)
                    End If

                    If iReg < 1 Then
                        If sMsg <> "" Then
                            sMsg += ", "
                        End If
                        sMsg += sBcNo_old
                    End If

                    sbDisplay_ResultOK(alBcNotestcd, iReg)
                End If
            End With

            If sMsg <> "" Then
                Return "검체번호 [" + sMsg + "]를 저장하지 못 했습니다."
            Else
                Return ""
            End If

            Cursor.Current = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default
            mbLeveCellGbn = True
        End Try

    End Function

    Private Sub sbDisplay_ResultOK(ByVal r_al_List As ArrayList, ByVal riCnt As Integer)
        Dim sFn As String = "Sub sbDisplay_ResultOK(ArrayList, integer)"

        Try
            If r_al_List.Count < 1 Then Exit Try

            For ix As Integer = 0 To r_al_List.Count - 1
                Dim sBuff() As String

                sBuff = Split(r_al_List.Item(ix).ToString, "|")
                With Me.spdResult
                    For iRow As Integer = 1 To .MaxRows

                        .Row = iRow
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text

                        If sBcNo = sBuff(0) And sTestcd = sBuff(1) Then
                            If riCnt > 0 Then
                                .Row = iRow
                                .Col = -1
                                .BackColor = Drawing.Color.FromArgb(213, 255, 170)
                            End If
                            Exit For
                        End If
                    Next
                End With

            Next

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub


    Public Sub sbDisplay_Init(ByVal rsType As String)

        Me.spdResult.TextTip = FPSpreadADO.TextTipConstants.TextTipFloating

        Me.lstEx.Items.Clear()

        If rsType = "ALL" Then
            Me.spdResult.MaxRows = 0

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

    End Sub

    Public Sub sbDisplay_Data_wgrp(ByVal rsWkYmd As String, ByVal rsWkGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                   ByVal rsTestCds As String, ByVal rsRegFlg As String)
        Try

            sbDisplay_Init("ALL")
            sbDisplay_Result_wgrp(rsWkYmd, rsWkGrpCd, rsWkNoS, rsWkNoE, rsTestCds, rsRegFlg)

            sbGet_Alert_Rule()

            With Me.spdResult
                If .MaxRows < 1 Then Return
                .Row = 1
                .Col = .GetColFromID("bcno") : Me.txtBcNo.Text = .Text.Replace("-", "")
                sbDisplay_RegNm(Me.txtBcNo.Text.Substring(0, 14))

                .set_ColWidth(.GetColFromID("workno"), 4.5)
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Sub sbDisplay_Data_tgrp(ByVal rsTGrpCd As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, _
                                   ByVal rsTestCds As String, ByVal rsRegFlg As String)
        Try

            sbDisplay_Init("ALL")
            sbDisplay_Result_tgrp(rsTGrpCd, rsTkDtS, rsTkDtE, rsTestCds, rsRegFlg)

            sbGet_Alert_Rule()

            With Me.spdResult
                If .MaxRows < 1 Then Return
                .Row = 1
                .Col = .GetColFromID("bcno") : Me.txtBcNo.Text = .Text.Replace("-", "")
                sbDisplay_RegNm(Me.txtBcNo.Text.Substring(0, 14))

                .set_ColWidth(.GetColFromID("workno"), 4.5)
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Sub sbDisplay_Data_wl(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsRegFlg As String)
        Try
            sbDisplay_Init("ALL")
            sbDisplay_Result_wl(rsWLUid, rsWLYmd, rsWLTitle, rsRegFlg)

            sbGet_Alert_Rule()

            With Me.spdResult
                If .MaxRows < 1 Then Return
                .Row = 1
                .Col = .GetColFromID("bcno") : Me.txtBcNo.Text = .Text.Replace("-", "")
                sbDisplay_RegNm(Me.txtBcNo.Text.Substring(0, 14))

                .set_ColWidth(.GetColFromID("workno"), 4.5)
            End With
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
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
                    Me.lblReg.Text = sDT + vbCrLf + sNM
                    'Me.lblReg.Text = sDT + " / " + sNM
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
                    Me.lblMW.Text = sDT + vbCrLf + sNM
                    'Me.lblMW.Text = sDT + " / " + sNM

                    Exit For
                End If
            Next

            a_dr = dt.Select("rstflg = '3'", "fndt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("fnid").ToString().Trim
                sNM = a_dr(i - 1).Item("fnnm").ToString().Trim
                sDT = a_dr(i - 1).Item("fndt").ToString().Trim

                Dim sRstFlg_j As String = a_dr(i - 1).Item("rstflg_j").ToString().Trim

                If Not sID + sNM + sDT = "" Then
                    If sRstFlg_j <> "2" Then
                        Me.lblSampleStatus.Text = "예비보고"
                    Else
                        Me.lblSampleStatus.Text = "결과완료"
                    End If

                    Me.lblFN.Text = sDT + vbCrLf + sNM
                    'Me.lblFN.Text = sDT + " / " + sNM

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

    Private Sub sbDisplay_Result_wl(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsRegFlg As String)
        Dim sFn As String = "Sub sbDisplay_Result_wl(string...)"

        Dim dt As New DataTable
        Dim sBcNo As String = ""

        Try
            '-- 검사결과
            dt = LISAPP.COMM.RstFn.fnGet_Result_wl(rsWLUid, rsWLYmd, rsWLTitle, rsRegFlg)
            If dt.Rows.Count < 1 Then Return

            sbDisplay_ResultViewAdd(dt)
            '-- 검사항목별 결과코드 help
            m_dt_RstCdHelp = LISAPP.COMM.RstFn.fnGet_test_rstinfo_wl(rsWLUid, rsWLYmd, rsWLTitle)

            sbGet_Calc_Rst(0)           '-- 계산식결과 표시
            sbGet_CvtRstInfo(sBcNo)     '-- 결과값 자동변환

            'Me.axCalcRst.BcNo = ""

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    Private Sub sbDisplay_Result_wgrp(ByVal rsWkYmd As String, ByVal rsWkGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                      ByVal rsTestCds As String, ByVal rsRegFlg As String)
        Dim sFn As String = "Sub sbDisplay_Result_wl(string...)"

        Dim dt As New DataTable
        Dim sBcNo As String = ""

        Try
            '-- 검사결과
            dt = LISAPP.COMM.RstFn.fnGet_Result_wgrp(rsWkYmd, rsWkGrpCd, rsWkNoS, rsWkNoE, rsTestCds, rsRegFlg)
            If dt.Rows.Count < 1 Then Return

            sbDisplay_ResultViewAdd(dt)
            '-- 검사항목별 결과코드 help
            m_dt_RstCdHelp = LISAPP.COMM.RstFn.fnGet_test_rstinfo_wgrp(rsWkYmd, rsWkGrpCd, rsWkNoS, rsWkNoE)

            sbGet_Calc_Rst(0)           '-- 계산식결과 표시
            sbGet_CvtRstInfo(sBcNo)     '-- 결과값 자동변환

            'Me.axCalcRst.BcNo = ""

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    Private Sub sbDisplay_Result_tgrp(ByVal rsTGrpCd As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, _
                                      ByVal rsTestCds As String, ByVal rsRegFlg As String)
        Dim sFn As String = "Sub sbDisplay_Result_tgrp(string...)"

        Dim dt As New DataTable
        Dim sBcNo As String = ""

        Try
            '-- 검사결과
            dt = LISAPP.COMM.RstFn.fnGet_Result_tgrp(rsTGrpCd, rsTkDtS, rsTkDtE, rsTestCds, rsRegFlg)
            If dt.Rows.Count < 1 Then Return

            sbDisplay_ResultViewAdd(dt)
            '-- 검사항목별 결과코드 help
            m_dt_RstCdHelp = LISAPP.COMM.RstFn.fnGet_test_rstinfo_tgrp(rsTGrpCd, rsTkDtS, rsTkDtE)

            sbGet_Calc_Rst(0)           '-- 계산식결과 표시
            sbGet_CvtRstInfo(sBcNo)     '-- 결과값 자동변환

            'Me.axCalcRst.BcNo = ""

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub


    Protected Sub sbDisplay_ResultView(ByVal r_dt As DataTable, Optional ByRef rbRstflgNotFN As Boolean = False)
        Dim sFn As String = "Protected Sub Display_ResultView(DataTable)"

        Try
            mbLeveCellGbn = False

            With Me.spdResult
                .MaxRows = 0

                .ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For ix As Integer = 1 To r_dt.Rows.Count

                    .Row = ix
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix - 1).Item("regno").ToString().Trim             '30
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix - 1).Item("patnm").ToString().Trim             '30
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix - 1).Item("sexage").ToString().Trim             '30
                    .Col = .GetColFromID("deptward") : .Text = r_dt.Rows(ix - 1).Item("deptward").ToString().Trim             '30
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix - 1).Item("spcnmd").ToString().Trim             '30
                    .Col = .GetColFromID("partslip") : .Text = r_dt.Rows(ix - 1).Item("partslip").ToString()          '30
                    .Col = .GetColFromID("rstflg_j") : .Text = r_dt.Rows(ix - 1).Item("rstflg_j").ToString()          '30
                    .Col = .GetColFromID("bcno") : .Text = Fn.BCNO_View(r_dt.Rows(ix - 1).Item("bcno").ToString().Trim, True)             '30
                    .Col = .GetColFromID("workno") : .Text = r_dt.Rows(ix - 1).Item("workno").ToString().Trim             '30
                    .Col = .GetColFromID("fnid") : .Text = r_dt.Rows(ix - 1).Item("fnid").ToString().Trim             '36
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix - 1).Item("testcd").ToString().Trim         '27
                    .Col = .GetColFromID("tclscd") : .Text = r_dt.Rows(ix - 1).Item("tclscd").ToString().Trim        '38
                    .Col = .GetColFromID("regnm") : .Text = r_dt.Rows(ix - 1).Item("regnm").ToString().Trim          '32
                    .Col = .GetColFromID("regid") : .Text = r_dt.Rows(ix - 1).Item("regid").ToString().Trim          '33
                    .Col = .GetColFromID("mwnm") : .Text = r_dt.Rows(ix - 1).Item("mwnm").ToString().Trim            '35
                    .Col = .GetColFromID("mwid") : .Text = r_dt.Rows(ix - 1).Item("mwid").ToString().Trim            '34
                    .Col = .GetColFromID("fnnm") : .Text = r_dt.Rows(ix - 1).Item("fnnm").ToString().Trim           '37
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix - 1).Item("spccd").ToString().Trim          '28
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
                    .Col = .GetColFromID("rerunrst") : .Text = r_dt.Rows(ix - 1).Item("rerunrst").ToString().Trim
                    .Col = .GetColFromID("cfmnm") : .Text = r_dt.Rows(ix - 1).Item("cfmnm").ToString().Trim

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
                    .Col = .GetColFromID("tcdgbn") : .Text = r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim

                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "P" Then
                        Select Case r_dt.Rows(ix - 1).Item("plgbn").ToString.Trim
                            Case "1", "2", "3", "4", "5"
                                If r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> "" And r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> STU_AUTHORITY.UsrID Then
                                    .Col = .GetColFromID("orgrst")
                                    .Lock = True
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.LightPink

                                    .Col = .GetColFromID("viewrst")
                                    .Lock = True
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.LightPink
                                End If
                        End Select

                    ElseIf r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""

                        Select Case r_dt.Rows(ix - 1).Item("plgbn").ToString.Trim
                            Case "1", "2", "3", "4", "5"
                                If r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> "" And r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> STU_AUTHORITY.UsrID Then
                                    .Col = .GetColFromID("orgrst")
                                    .Lock = True
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.LightPink

                                    .Col = .GetColFromID("viewrst")
                                    .Lock = True
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.LightPink
                                End If
                        End Select

                    End If

                    .Col = .GetColFromID("titleyn")
                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" Then
                        .Col = .GetColFromID("orgrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True

                        .Col = .GetColFromID("viewrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True

                        .Col = .GetColFromID("rerunrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True
                    End If

                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" And r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "B" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""
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
                    .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim

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
                    .Col = .GetColFromID("viewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim
                    .Col = .GetColFromID("cviewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim
                    .Col = .GetColFromID("rstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim
                    .Col = .GetColFromID("crstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim

                    .Col = .GetColFromID("eqflag") : .Text = r_dt.Rows(ix - 1).Item("eqflag").ToString().Trim              '20

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

                Next
                .ReDraw = True

                .Row = .MaxRows
                .Col = .GetColFromID("chk") : Dim strChk As String = .Text
                If strChk = "" And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Col = .GetColFromID("iud") : .Text = ""
                End If


            End With

        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)

        Finally
            Me.spdResult.ReDraw = True
            mbLeveCellGbn = True

        End Try
    End Sub

    Protected Sub sbDisplay_ResultViewAdd(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub Display_ResultView(DataTable)"

        Try
            With Me.spdResult
                .ReDraw = False
                For ix As Integer = 1 To r_dt.Rows.Count

                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix - 1).Item("regno").ToString().Trim             '30
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix - 1).Item("patnm").ToString().Trim             '30
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix - 1).Item("sexage").ToString().Trim             '30
                    .Col = .GetColFromID("deptward") : .Text = r_dt.Rows(ix - 1).Item("deptward").ToString().Trim             '30
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix - 1).Item("spcnmd").ToString().Trim             '30
                    .Col = .GetColFromID("partslip") : .Text = r_dt.Rows(ix - 1).Item("partslip").ToString()          '30
                    .Col = .GetColFromID("rstflg_j") : .Text = r_dt.Rows(ix - 1).Item("rstflg_j").ToString()          '30
                    .Col = .GetColFromID("bcno") : .Text = Fn.BCNO_View(r_dt.Rows(ix - 1).Item("bcno").ToString().Trim, True)             '30
                    .Col = .GetColFromID("workno") : .Text = r_dt.Rows(ix - 1).Item("workno").ToString().Trim             '30
                    .Col = .GetColFromID("fnid") : .Text = r_dt.Rows(ix - 1).Item("fnid").ToString().Trim             '36
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix - 1).Item("testcd").ToString().Trim         '27
                    .Col = .GetColFromID("tclscd") : .Text = r_dt.Rows(ix - 1).Item("tclscd").ToString().Trim        '38
                    .Col = .GetColFromID("regnm") : .Text = r_dt.Rows(ix - 1).Item("regnm").ToString().Trim          '32
                    .Col = .GetColFromID("regid") : .Text = r_dt.Rows(ix - 1).Item("regid").ToString().Trim          '33
                    .Col = .GetColFromID("mwnm") : .Text = r_dt.Rows(ix - 1).Item("mwnm").ToString().Trim            '35
                    .Col = .GetColFromID("mwid") : .Text = r_dt.Rows(ix - 1).Item("mwid").ToString().Trim            '34
                    .Col = .GetColFromID("fnnm") : .Text = r_dt.Rows(ix - 1).Item("fnnm").ToString().Trim           '37
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix - 1).Item("spccd").ToString().Trim          '28
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
                    .Col = .GetColFromID("rerunrst") : .Text = r_dt.Rows(ix - 1).Item("rerunrst").ToString().Trim
                    .Col = .GetColFromID("cfmnm") : .Text = r_dt.Rows(ix - 1).Item("cfmnm").ToString().Trim

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
                    .Col = .GetColFromID("tcdgbn") : .Text = r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim

                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "P" Then
                        Select Case r_dt.Rows(ix - 1).Item("plgbn").ToString.Trim
                            Case "1", "2", "3", "4", "5"
                                If r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> "" And r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> STU_AUTHORITY.UsrID Then
                                    .Col = .GetColFromID("orgrst")
                                    .Lock = True
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.LightPink

                                    .Col = .GetColFromID("viewrst")
                                    .Lock = True
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.LightPink
                                End If
                        End Select

                    ElseIf r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""

                        Select Case r_dt.Rows(ix - 1).Item("plgbn").ToString.Trim
                            Case "1", "2", "3", "4", "5"
                                If r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> "" And r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> STU_AUTHORITY.UsrID Then
                                    .Col = .GetColFromID("orgrst")
                                    .Lock = True
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.LightPink

                                    .Col = .GetColFromID("viewrst")
                                    .Lock = True
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.LightPink
                                End If
                        End Select

                    End If

                    .Col = .GetColFromID("titleyn")
                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" Then
                        .Col = .GetColFromID("orgrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True

                        .Col = .GetColFromID("viewrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True

                        .Col = .GetColFromID("rerunrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True
                    End If

                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" And r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "B" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""
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
                    .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                    
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
                    .Col = .GetColFromID("viewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim
                    .Col = .GetColFromID("cviewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim
                    .Col = .GetColFromID("rstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim
                    .Col = .GetColFromID("crstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim

                    .Col = .GetColFromID("eqflag") : .Text = r_dt.Rows(ix - 1).Item("eqflag").ToString().Trim              '20

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

                Next
                .ReDraw = True
            End With

        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)

        Finally
            Me.spdResult.ReDraw = True
        End Try
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

        Me.mnuSpRst.Visible = False

        With spdResult
            .Row = e.row
            .Col = .GetColFromID("testcd") : Me.txtTestCd.Text = .Text
            .Col = .GetColFromID("spccd") : sSpcCd = .Text
            .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")

            Dim sSpRstYn As String = LISAPP.COMM.RstFn.fnGet_SpRst_yn(sBcNo, Me.txtTestCd.Text.Substring(0, 5))
            Dim sFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(Me.txtTestCd.Text.Substring(0, 5), sSpcCd)

            If sSpRstYn <> "" Then mnuSpRst.Visible = True

            If e.col = .GetColFromID("orgrst") And e.row > 0 Then
                .Row = e.row
                .Col = .GetColFromID("tcdgbn") : sTCdGbn = .Text
                If sTCdGbn = "P" Or sTCdGbn = "C" Then
                    .Col = .GetColFromID("testcd") : sTestCd = .Text.Substring(0, 5)
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
            End If
        End With

        sTestCd = Ctrl.Get_Code(Me.spdResult, "testcd", e.row)
        sbDisplay_RegNm(sBcNo)
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

                    With Me.spdResult
                        Dim iRow As Integer = .ActiveRow

                        .Row = iRow
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
                        sbGet_Calc_Rst(iRow) '-- 결과 계산
                        sbGet_CvtRstInfo(sBcNo, sTestCd)

                    End With

                    Me.lstCode.Items.Clear()
                    Me.lstCode.Hide()
                    Me.pnlCode.Visible = False

                Case Else
                    RaiseEvent FunctionKeyDown(sender, New System.Windows.Forms.KeyEventArgs(CType(e.keyCode, System.Windows.Forms.Keys)))
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
                        MsgBox(sMsg, MsgBoxStyle.Information)
                    End If
                End If
            End If

            If strRstType = "1" And rsRst <> "" And IsNumeric(rsRst) = False Then
                Dim sMsg As String = "숫자 결과만 입력할 수 있습니다."
                MsgBox(sMsg, MsgBoxStyle.Information)
            End If
        End With

        Return rsRst

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
                        MsgBox(sMsg, MsgBoxStyle.Information)

                        .Col = .GetColFromID("orgrst") : .Text = ""
                        .Col = .GetColFromID("viewrst") : .Text = ""
                    End If
                End If
            End If

            If strRstType = "1" And strRst <> "" And IsNumeric(strRst) = False Then
                Dim sMsg As String = "숫자 결과만 입력할 수 있습니다."
                MsgBox(sMsg, MsgBoxStyle.Information)
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
        Dim strRefL As String = ""
        Dim strRefH As String = ""
        Dim strRefLS As String = ""
        Dim strRefHS As String = ""
        Dim strRst As String = ""
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

                .Col = .GetColFromID("refl") : strRefL = .Text
                .Col = .GetColFromID("refls") : strRefLS = .Text
                .Col = .GetColFromID("refh") : strRefH = .Text
                .Col = .GetColFromID("refhs") : strRefHS = .Text
                .Col = .GetColFromID("orgrst") : strRst = .Text

                strRst = strRst.Replace(">", "").Replace("<", "").Replace("=", "")

                If IsNumeric(strRst) Then
                    Select Case strRefLS
                        Case "0"
                            If Val(strRst) < Val(strRefL) And strRefL <> "" Then
                                sHLmark = "L"
                            End If
                        Case "1"
                            If Val(strRst) <= Val(strRefL) And strRefL <> "" Then
                                sHLmark = "L"
                            End If
                    End Select

                    Select Case strRefHS
                        Case "0"
                            If Val(strRst) > Val(strRefH) And strRefH <> "" Then
                                sHLmark = "H"
                            End If
                        Case "1"
                            If Val(strRst) >= Val(strRefH) And strRefH <> "" Then
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

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : strRst = .Text
            .Col = .GetColFromID("criticalgbn") : strCriticalGbn = .Text
            .Col = .GetColFromID("criticall") : strCriticalL = .Text
            .Col = .GetColFromID("criticalh") : strCriticalH = .Text

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

    ' alert 체크
    Private Sub sbAlertCheck(ByVal rirow As Integer)
        Dim sORst As String = "", sVRst As String = "", sEqFlag As String = ""
        Dim sTestCd As String = "", sSpcCd As String = "", sTclsCd As String = "", sPanicMark As String = "", sDeltaMark As String = ""
        Dim sDeptCd As String = "", sSexAge As String = ""
        Dim sAlertGbn As String = ""
        Dim sAlertL As String = ""
        Dim sAlertH As String = ""
        Dim sAlertMark As String = ""

        With spdResult
            .Row = rirow
            .Col = .GetColFromID("deptward") : sDeptCd = .Text : If sDeptCd.IndexOf("/") >= 0 Then sDeptCd = sDeptCd.Split("/"c)(0)
            .Col = .GetColFromID("sexage") : sSexAge = .Text
            .Col = .GetColFromID("testcd") : sTestCd = .Text
            .Col = .GetColFromID("spccd") : sSpcCd = .Text

            .Col = .GetColFromID("tclscd") : sTclsCd = .Text

            .Col = .GetColFromID("orgrst") : sORst = .Text
            .Col = .GetColFromID("viewrst") : sVRst = .Text
            .Col = .GetColFromID("eqflag") : sEqFlag = .Text

            .Col = .GetColFromID("panicmark") : sPanicMark = .Text
            .Col = .GetColFromID("deltamark") : sDeltaMark = .Text

            .Col = .GetColFromID("alertgbn") : sAlertGbn = .Text
            .Col = .GetColFromID("alertl") : sAlertL = .Text
            .Col = .GetColFromID("alerth") : sAlertH = .Text

            Select Case sAlertGbn
                Case "1", "A"   ' 경고하한치만 사용
                    If sAlertL = "" Then Return
                    If IsNumeric(sAlertL) = False Then Return

                    If IsNumeric(sORst) Then
                        If Val(sORst) < Val(sAlertL) Then sAlertMark = "A"
                    End If

                Case "2", "B"    ' 경고상한치만 사용
                    If sAlertH = "" Then Return
                    If IsNumeric(sAlertH) = False Then Return

                    If IsNumeric(sORst) Then
                        If Val(sORst) > Val(sAlertH) Then
                            sAlertMark = "A"
                        End If
                    End If
                Case "3", "C"    ' 모두 사용
                    If sAlertL = "" Then Return
                    If IsNumeric(sAlertL) = False Then Return
                    If sAlertH = "" Then Return
                    If IsNumeric(sAlertH) = False Then Return

                    If IsNumeric(sORst) Then
                        If Val(sORst) < Val(sAlertL) Or Val(sORst) > Val(sAlertH) Then sAlertMark = "A"
                    End If

                Case "4"    '-- 문자값 비교
                    If sAlertL = "" And sAlertH = "" Then Return
                    If sAlertL = "" Then sAlertL = sAlertH

                    If sORst.ToUpper = sAlertL.ToUpper Then sAlertMark = "A"
            End Select

            If sAlertMark = "" And (sAlertGbn = "5" Or sAlertGbn = "A" Or sAlertGbn = "B" Or sAlertGbn = "C") Then
                '-- Alert Rule
                Dim dr As DataRow() = m_dt_Alert_Rule.Select("testcd = '" + sTestCd + "'")


                If dr.Length > 0 Then
                    Dim iCnt As Integer = 0, iAlert As Integer = 0

                    If dr(0).Item("orgrst").ToString.Trim <> "" Then
                        iCnt += 1
                        If dr(0).Item("orgrst").ToString().IndexOf(sORst + ",") >= 0 Then iAlert += 1
                    End If

                    If dr(0).Item("viewrst").ToString.Trim <> "" Then
                        iCnt += 1
                        If dr(0).Item("viewrst").ToString().IndexOf(sVRst + ",") >= 0 Then iAlert += 1
                    End If

                    If sPanicMark <> "" Then
                        iCnt += 1
                        iAlert += 1
                    End If

                    If sDeltaMark <> "" Then
                        iCnt += 1
                        iAlert += 1
                    End If

                    If dr(0).Item("eqflag").ToString.Trim <> "" Then
                        iCnt += 1

                        If sEqFlag <> "" Then
                            If dr(0).Item("eqflag").ToString().IndexOf("^") >= 0 Then
                                Dim strBuf() As String = dr(0).Item("eqflag").ToString().Split("^"c)

                                If strBuf(1) = "" Then
                                    If strBuf(0) = "" Then
                                        iAlert += 1
                                    Else
                                        strBuf(0) += ","
                                        If strBuf(0).IndexOf(sEqFlag + ",") >= 0 Then iAlert += 1
                                    End If
                                Else
                                    If strBuf(0) = "" Then
                                        strBuf(1) += ","
                                        If strBuf(1).IndexOf(sTclsCd + ",") >= 0 Then iAlert += 1
                                    Else
                                        strBuf(0) += "," : strBuf(1) += ","
                                        If strBuf(0).IndexOf(sEqFlag + ",") >= 0 And strBuf(1).IndexOf(sTclsCd + ",") >= 0 Then iAlert += 1
                                    End If
                                End If
                            Else
                                If dr(0).Item("eqflag").ToString().IndexOf(sEqFlag + ",") >= 0 Then iAlert += 1
                            End If
                        End If

                    End If

                    If dr(0).Item("sex").ToString.Trim <> "" Then
                        iCnt += 1
                        If sSexAge.StartsWith(dr(0).Item("sex").ToString()) Then iAlert += 1
                    End If

                    If dr(0).Item("deptcds").ToString.Trim <> "" Then
                        iCnt += 1
                        If dr(0).Item("deptcds").ToString().IndexOf(sDeptCd + ",") >= 0 Then iAlert += 1
                    End If

                    If dr(0).Item("spccds").ToString.Trim <> "" Then
                        iCnt += 1
                        If dr(0).Item("spccds").ToString().IndexOf(sSpcCd + ",") >= 0 Then iAlert += 1
                    End If

                    If iCnt = iAlert Then sAlertMark = "A"
                End If
            End If

            .Col = .GetColFromID("alertmark")
            If sAlertMark = "A" Then
                .Text = sAlertMark
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

            sRst = sRst.Replace(">", "").Replace("<", "").Replace("=", "")

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

    Private Sub spdResult_KeyUpEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdResult.KeyUpEvent
        Dim sFn As String = "Sub spdOrdList_KeyUpEvent(Object, AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdOrdListR.KeyUpEvent"

        Dim sTestCd As String = ""
        Dim sRst As String = ""

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

            If e.newRow > 0 And e.newCol = .GetColFromID("orgrst") Then

                .Row = e.newRow
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text.Substring(0, 5)

                .Row = e.newRow
                .Col = e.newCol
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then
                    .ForeColor = Color.Black
                End If

                .Row = e.newRow
                .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                .Col = .GetColFromID("slipcd") : Dim sSlipCd As String = .Text

                If Me.txtBcNo.Text <> sBcNo Then
                    Me.txtBcNo.Text = sBcNo
                    sbDisplay_RegNm(sBcNo)
                End If
                sbDisplay_RegNm_Test(sTestCd)
            End If
        End With

    End Sub

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

    Private Sub axCalcRst_OnSelectedCalcRstInfos(ByVal r_al As System.Collections.ArrayList) Handles axCalcRst.OnSelectedCalcRstInfos
        sbDisplayCalRst_Info(r_al)
    End Sub

    Private Sub AxRstInput_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        RaiseEvent FunctionKeyDown(sender, New System.Windows.Forms.KeyEventArgs(e.KeyCode))
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        spdResult.ColsFrozen = spdResult.GetColFromID("tnmd")

        With spdResult
            .Col = .GetColFromID("bcno") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = .GetColFromID("regno") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = .GetColFromID("patnm") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = .GetColFromID("sexage") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = .GetColFromID("deptward") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = .GetColFromID("spcnmd") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = .GetColFromID("workno") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
        End With

    End Sub

    Private Sub txtOrgRst_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrgRst.TextChanged
        DP_Common.sbFindPosition(lstCode, Convert.ToString(txtOrgRst.Text))
    End Sub

    Private Sub mnuSpRst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSpRst.Click
        If Me.txtTestCd.Text = "" Then Return

        Dim sSpRstYn As String = LISAPP.COMM.RstFn.fnGet_SpRst_yn(Me.txtBcNo.Text.Replace("-", ""), Me.txtTestCd.Text.Substring(0, 5))
        If sSpRstYn = "" Then Return

        RaiseEvent Call_SpRst(Me.txtBcNo.Text.Replace("-", ""), Me.txtTestCd.Text.Substring(0, 5))
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
        frmChild = New FGUNFITSPC(Me.txtBcNo.Text.Replace("-", ""), alTclsCds)

        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()
    End Sub

    Private Sub btnQryFNModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQryFNModify.Click
        Dim objForm As New FGMODIFY
        objForm.Display_Data(moForm, Me.txtBcNo.Text.Replace("-", ""))
    End Sub

    Private Sub btnReg_Abn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_Abn.Click
        Dim sFn As String = "Handles btnReg_UnFit.Click"

        Dim alTclsCds As New ArrayList
        Dim sPartSlip As String = ""

        With spdResult
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                .Col = .GetColFromID("partslip") : sPartSlip = .Text
                If sChk = "1" And alTclsCds.Contains(sTestCd) = False Then
                    alTclsCds.Add(sTestCd)
                End If
            Next

        End With


        Dim frmChild As Windows.Forms.Form
        frmChild = New FGABNORMAL(Me.txtBcNo.Text.Replace("-", ""), sPartSlip, False, False, alTclsCds)

        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()
    End Sub


End Class
