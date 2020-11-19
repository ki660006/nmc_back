Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.SVar

Imports System.Drawing
Imports System.Windows.Forms

Public Class AxRstInput_poct

    Private moForm As Windows.Forms.Form

    Public Event ChangedBcNo(ByVal BcNo As String)
    Public Event FunctionKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event Call_SpRst(ByVal BcNo As String, ByVal TestCd As String)

    Private msFormID As String = ""
    Private msRegNo As String = ""
    Private msDateS As String = ""
    Private msDateE As String = ""
    Private msPatNm As String = ""
    Private msSexAge As String = ""

    Private msDeptCd As String = ""
    Private msCaseGbn As String = ""

    Private msFnDt As String = ""

    Private msOwnGbn As String = ""
    Private msFkOcs As String = ""
    Private msOrddt As String = ""
    Private msTestCd As String = ""
    Private msRstFlg As String = ""

    Private msObJName As String
    Private mbQueryView As Boolean = False

    Private mbColHiddenYn As Boolean
    Private mbCodeEscKey As Boolean = False

    Public m_dt_RstUsr As DataTable
    Private m_dt_RstCdHelp As DataTable
    Private m_dt_Alert_Rule As DataTable

    Private mbLostFocusGbn As Boolean = True
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

            Dim alRst As Integer = 0

            With spdResult
                If mbColHiddenYn Then
                    For alRst = 1 To .MaxCols
                        If alRst = .GetColFromID("chk") Or alRst = .GetColFromID("tnmd") Or alRst = .GetColFromID("orgrst") Or alRst = .GetColFromID("viewrst") Or _
                               alRst = .GetColFromID("rerunflg") Or alRst = .GetColFromID("history") Or alRst = .GetColFromID("reftxt") Or alRst = .GetColFromID("rstunit") Or _
                               alRst = .GetColFromID("hlmark") Or alRst = .GetColFromID("panicmark") Or alRst = .GetColFromID("deltamark") Or _
                               alRst = .GetColFromID("criticalmark") Or alRst = .GetColFromID("alertmark") Or alRst = .GetColFromID("rstflgmark") Or _
                               alRst = .GetColFromID("rstcmt") Or alRst = .GetColFromID("bfviewrst2") Or alRst = .GetColFromID("bffndt2") Or alRst = .GetColFromID("eqnm") Or _
                               alRst = .GetColFromID("testcd") Or alRst = .GetColFromID("spccd") Or alRst = .GetColFromID("tordcd") Or _
                               alRst = .GetColFromID("reftcls") Or alRst = .GetColFromID("eqflag") Or alRst = .GetColFromID("rerunrst") Then
                        Else
                            .Col = alRst : .ColHidden = True
                        End If
                    Next
                Else
                    For alRst = 1 To .MaxCols
                        .Col = alRst : .ColHidden = False
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

    Public WriteOnly Property TestCd() As String
        Set(ByVal value As String)
            msTestCd = value
        End Set
    End Property

    Public WriteOnly Property OwnGbn() As String
        Set(ByVal value As String)
            msOwnGbn = value
        End Set
    End Property

    Public WriteOnly Property FkOcs() As String
        Set(ByVal value As String)
            msFkOcs = value
        End Set
    End Property

    Public WriteOnly Property RstFlg() As String
        Set(ByVal value As String)
            msRstFlg = value
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

                    spdResult_ClickEvent(spdResult, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("orgrst"), intUnLockRow))
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
        Dim iLen As Integer = 0

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

                    'If sViewRst.IndexOf(".") > -1 Then Return
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

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst")

            sbRstTypeCheck(riRow)
            sbHLCheck(riRow)
            sbPanicCheck(riRow, m_dt_RstCdHelp)
            sbUJudgCheck(riRow)
            sbDeltaCheck(riRow, m_dt_RstCdHelp)
            'sbCriticalCheck(riRow)
            'sbAlertCheck(riRow)
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

        With spdResult
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
            Dim alMsg As New ArrayList
            Dim sChk$ = "", sOrgRst$ = "", sViewRst$ = "", sRstCmt$ = "", sRstFlg$ = ""
            Dim sOrgRst_o$ = "", sViewRst_o$ = "", sRstCmt_o$ = ""
            Dim sBcno$ = "", sSlipCd$ = "", sTestCd$ = "", sTnmd$ = "", sTcdGbn$ = "", sTitleYn$ = "", sReqSub$ = ""
            Dim sAlert$ = "", sPanic$ = "", sDelta$ = "", sCritical$ = ""
            Dim sPlGbn$ = "", sBldGbn$ = "", sUsrId1$ = "", sUsrId2$ = "", sRst1$ = "", sRst2$ = ""
            Dim sBfViewRst As String = ""

            Dim sBcNo_OLD As String = "", sSlipCd_old As String = ""
            Dim sCmtCont As String = ""

            Dim bFlag As Boolean = False

            With spdResult
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("bcno") : sBcno = .Text
                    .Col = .GetColFromID("tnmd") : sTnmd = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                    .Col = .GetColFromID("iud") : sChk = .Text
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1" And sTcdGbn = "P" Then
                        For ix As Integer = iRow + 1 To .MaxRows
                            .Row = ix
                            .Col = .GetColFromID("iud") : sChk = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : sReqSub = .Text

                            If sOrgRst = "" And sReqSub = "1" Then
                                .Row = ix
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd Then
                                    .Row = ix
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
                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text
                    .Col = .GetColFromID("rstcmt") : sRstCmt = .Text
                    .Col = .GetColFromID("rstflg") : sRstFlg = .Text

                    .Col = .GetColFromID("alertmark") : sAlert = .Text
                    .Col = .GetColFromID("panicmark") : sPanic = .Text
                    .Col = .GetColFromID("deltamark") : sDelta = .Text
                    .Col = .GetColFromID("criticalmark") : sCritical = .Text

                    .Col = .GetColFromID("corgrst") : sOrgRst_o = .Text
                    .Col = .GetColFromID("cviewrst") : sViewRst_o = .Text
                    .Col = .GetColFromID("crstcmt") : sRstCmt_o = .Text

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

                        If rsRstFlg = "3" Then
                            If sRstFlg = "3" Then
                                If (sTcdGbn = "P" Or sTcdGbn = "B") And sTitleYn = "1" Then
                                Else
                                    If sOrgRst = sOrgRst_o And sViewRst = sViewRst_o And sRstCmt = sRstCmt_o Then
                                        alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 바뀐정보가 없습니다.")
                                        bFlag = True
                                    ElseIf sOrgRst <> sOrgRst_o Or sViewRst <> sViewRst_o Then
                                        sCmtCont += sTnmd + "(" + sOrgRst_o + "/" + sViewRst_o + ")|"
                                    End If
                                End If
                            End If
                        End If

                        If rsRstFlg = "2" Then
                            If sRstFlg = "3" Then
                                If (sOrgRst <> sOrgRst_o Or sViewRst <> sViewRst_o) And sAlert = "" Then
                                    sCmtCont += sTnmd + "(" + sOrgRst_o + "/" + sViewRst_o + ")|"
                                Else
                                    alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 최종보고된 자료 입니다.")
                                    bFlag = True
                                End If
                            ElseIf sRstFlg = "2" Then
                                If (sTcdGbn = "P" Or sTcdGbn = "B") And sTitleYn = "1" Then
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

                        If sCritical = "C" And STU_AUTHORITY.CFNReg <> "1" Then
                            bFlag = True
                            alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 Critical에 대한 보고권한이 없습니다.")
                        End If

                        If sRstFlg = "3" Then
                            If sOrgRst <> sOrgRst_o And STU_AUTHORITY.FNUpdate <> "1" Then
                                bFlag = True
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + sTnmd + "'은 최종보고수정에 대한 보고권한이 없습니다.")
                            End If
                        Else
                            If sOrgRst <> sOrgRst_o And STU_AUTHORITY.RstUpdate <> "1" Then
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
                        For ix As Integer = iRow + 1 To .MaxRows
                            .Row = ix
                            .Col = .GetColFromID("iud") : sChk = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : sReqSub = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text

                            If sOrgRst <> "" Then
                                .Row = ix
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) <> sTestCd Then Exit For

                                .Row = ix : .Col = .GetColFromID("iud") : .Text = "1"
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
                            .Row = ix
                            .Col = .GetColFromID("iud") : Dim sIUD As String = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : sReqSub = .Text
                            .Col = .GetColFromID("rstflg") : Dim sSubRstFlg As String = .Text
                            .Col = .GetColFromID("testcd") : Dim sTsubCd As String = .Text

                            If sTestCd <> sTsubCd.Substring(0, 5) Then Exit For
                            'If ix = .MaxRows Then MsgBox("B")

                            If sIUD = "1" Then
                                .Row = ix
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd Then
                                    iCnt += 1
                                Else
                                    Exit For
                                End If
                            ElseIf sReqSub = "1" And sOrgRst = "" Then
                                iCnt = 99
                                Exit For
                            ElseIf sRstFlg < sSubRstFlg Then
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

            End With

            fnChecakReg = alMsg
        Catch ex As Exception
            fnChecakReg = New ArrayList
        End Try

    End Function

    Private Function fnGetRst(ByVal rsRstflg As String, ByVal rsCfmNm As String, ByVal rsCfmSign As String) As ArrayList
        Dim sFn As String = "Function fnGetRst(string) As ArrayList"
        Try
            Dim sRstflg = ""
            Dim sORst_o$ = "", sVRst_o$ = "", sCmt_o$ = ""

            Dim alRst As New ArrayList
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
                        .Col = .GetColFromID("corgrst") : sORst_o = .Text
                        .Col = .GetColFromID("cviewrst") : sVRst_o = .Text
                        .Col = .GetColFromID("ccmt") : sCmt_o = .Text

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
                            If (objRst.mOrgRst <> "" And (sORst_o <> objRst.mOrgRst Or sVRst_o <> objRst.mViewRst Or _
                                                          sRstflg <> objRst.mRstFlg Or sCmt_o <> objRst.mRstCmt)) Or _
                               (objRst.mDetailYN = "1" And sTCdGbn = "P") Then
                                alRst.Add(objRst)
                            End If
                            '> yjlee 2009-01-16
                        End If ''' ACK 박정은 추가 2010-10-26 


                    End If
                Next

                fnGetRst = alRst
            End With
        Catch ex As Exception
            fnGetRst = New ArrayList
        End Try

    End Function

    Public Function fnReg(Optional ByVal rsCfmNm As String = "", Optional ByVal rsCfmSign As String = "") As Boolean
        ''' rsRstflg  1=결과저장 2=결과확인 3=결과검증
        Dim alReturn As New ArrayList

        Dim alRst As New ArrayList

        Try
            mbLeveCellGbn = False

            sbGet_Alert_Rule()

            Dim alCmtCont As New ArrayList

            alReturn = fnChecakReg("3", alCmtCont)

            If alCmtCont.Count > 0 Then
                For ix As Integer = 0 To alCmtCont.Count - 1
                    Dim frm As New FGFINAL_CMT

                    frm.msBcNo = CType(alCmtCont.Item(ix), CMT_INFO).BcNo
                    frm.msPartSlip = CType(alCmtCont.Item(ix), CMT_INFO).PartSlip
                    frm.msCmt = CType(alCmtCont.Item(ix), CMT_INFO).CmtCont

                    Dim sRet As String = frm.Display_Result()

                    If sRet <> "OK" Then Return False

                Next
            End If

            alRst = fnGetRst("3", rsCfmNm, rsCfmSign)

            Dim objRst As New LISAPP.APP_R.AxRstFn

            Return objRst.fnReg(msRegNo, msFkOcs, STU_AUTHORITY.usrid, alRst)  ''' 결과등록 

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return False
        Finally
            mbLeveCellGbn = True
        End Try

    End Function

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

        '결과상태, 결과저장, 최종보고
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

    Public Sub sbDisplay_Data()

        Dim dt As New DataTable

        Try

            mbQueryView = True
            sbDisplay_Init("ALL")
            sbDisplay_Init("")
            sbDisplay_Result(msOwnGbn, msFkOcs, msRegNo, msTestCd)

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
                    Me.lblReg.Text = sDT + vbCrLf + sNM
                    Exit For
                End If
            Next

            a_dr = dt.Select("rstflg = '3'", "fndt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("fnid").ToString().Trim
                sNM = a_dr(i - 1).Item("fnnm").ToString().Trim
                sDT = a_dr(i - 1).Item("fndt").ToString().Trim

                If Not sID + sNM + sDT = "" Then
                    Me.lblFN.Text = sDT + vbCrLf + sNM
                    Exit For
                End If
            Next

            m_dt_RstUsr = dt.Copy

        Catch ex As Exception

            sbLog_Exception(sFn + ":" + ex.Message)

        End Try
    End Sub


    Private Sub sbDisplay_Result(ByVal rsOwnGbn As String, ByVal rsFkOcs As String, ByVal rsRegNo As String, ByVal rsTestCd As String)
        Dim sFn As String = "Sub sbDisplay_Result(string)"

        Dim dt As New DataTable

        Try
            '-- 검사결과
            dt = LISAPP.APP_R.PoctFn.fnGet_Result_fkocs(rsOwnGbn, rsFkOcs, rsRegNo)
            sbDisplay_ResultView(dt)

            '-- 검사항목별 결과코드 help
            m_dt_RstCdHelp = LISAPP.APP_R.PoctFn.fnGet_poct_rstinfo(rsTestCd)

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

                    .Row = ix
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix - 1).Item("bcno").ToString().Trim           '30
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix - 1).Item("testcd").ToString().Trim         '27
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix - 1).Item("spccd").ToString().Trim          '28
                    .Col = .GetColFromID("tclscd") : .Text = r_dt.Rows(ix - 1).Item("tclscd").ToString().Trim         '38
                    .Col = .GetColFromID("regnm") : .Text = r_dt.Rows(ix - 1).Item("regnm").ToString().Trim           '32
                    .Col = .GetColFromID("regid") : .Text = r_dt.Rows(ix - 1).Item("regid").ToString().Trim           '33
                    .Col = .GetColFromID("mwnm") : .Text = r_dt.Rows(ix - 1).Item("mwnm").ToString().Trim            '35
                    .Col = .GetColFromID("mwid") : .Text = r_dt.Rows(ix - 1).Item("mwid").ToString().Trim            '34
                    .Col = .GetColFromID("fnnm") : .Text = r_dt.Rows(ix - 1).Item("fnnm").ToString().Trim            '37
                    .Col = .GetColFromID("fnid") : .Text = r_dt.Rows(ix - 1).Item("fnid").ToString().Trim            '36
                    .Col = .GetColFromID("plgbn") : .Text = r_dt.Rows(ix - 1).Item("plgbn").ToString().Trim         '40 
                    .Col = .GetColFromID("reqsub") : .Text = r_dt.Rows(ix - 1).Item("reqsub").ToString().Trim        '45
                    .Col = .GetColFromID("rsttype") : .Text = r_dt.Rows(ix - 1).Item("rsttype").ToString().Trim      '46
                    .Col = .GetColFromID("rstllen") : .Text = r_dt.Rows(ix - 1).Item("rstllen").ToString().Trim     '47
                    .Col = .GetColFromID("rstulen") : .Text = r_dt.Rows(ix - 1).Item("rstulen").ToString().Trim     '47
                    .Col = .GetColFromID("cutopt") : .Text = r_dt.Rows(ix - 1).Item("cutopt").ToString().Trim       '48
                    '.Col = .GetColFromID("rerunflg") : .Text = r_dt.Rows(ix - 1).Item("rerunflg").ToString().Trim     '7
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
                    '.Col = .GetColFromID("criticalgbn") : .Text = r_dt.Rows(ix - 1).Item("criticalgbn").ToString().Trim   '66
                    '.Col = .GetColFromID("criticall") : .Text = r_dt.Rows(ix - 1).Item("criticall").ToString().Trim      '67
                    '.Col = .GetColFromID("criticalh") : .Text = r_dt.Rows(ix - 1).Item("criticalh").ToString().Trim      '68
                    '.Col = .GetColFromID("alertgbn") : .Text = r_dt.Rows(ix - 1).Item("alertgbn").ToString().Trim       '69
                    '.Col = .GetColFromID("alertl") : .Text = r_dt.Rows(ix - 1).Item("alertl").ToString().Trim         '70
                    '.Col = .GetColFromID("alerth") : .Text = r_dt.Rows(ix - 1).Item("alerth").ToString().Trim          '71
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
                    '.Col = .GetColFromID("eqbcno") : .Text = r_dt.Rows(ix - 1).Item("eqbcno").ToString().Trim          '23
                    .Col = .GetColFromID("tnmp") : .Text = r_dt.Rows(ix - 1).Item("tnmp").ToString().Trim            '80
                    .Col = .GetColFromID("tordcd") : .Text = r_dt.Rows(ix - 1).Item("tordcd").ToString().Trim           '29
                    .Col = .GetColFromID("viwsub") : .Text = r_dt.Rows(ix - 1).Item("viwsub").ToString().Trim          '86
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
                    .Col = .GetColFromID("tcdgbn") : .Text = r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim             '44

                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""

                        Select Case r_dt.Rows(ix - 1).Item("plgbn").ToString.Trim
                            Case "2"
                                If r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> "" And r_dt.Rows(ix - 1).Item("mwid").ToString.Trim <> STU_AUTHORITY.usrid And _
                                    r_dt.Rows(ix - 1).Item("orgrst").ToString.Trim <> "" And r_dt.Rows(ix - 1).Item("rstflg").ToString.Trim <> "3" Then
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

                        If r_dt.Rows(ix - 1).Item("viwsub").ToString.Trim <> "1" And _
                           r_dt.Rows(ix - 1).Item("orgrst").ToString.Trim = "" And r_dt.Rows(ix - 1).Item("bforgrst1").ToString.Trim = "" Then
                            .Row = ix
                            .RowHidden = True
                        End If
                    End If

                    .Col = .GetColFromID("titleyn") : .Text = r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim       '43
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

                    '.Col = .GetColFromID("criticalmark") : .Text = r_dt.Rows(ix - 1).Item("criticalmark").ToString().Trim     '16
                    'If r_dt.Rows(ix - 1).Item("criticalmark").ToString() = "C" Then
                    '    .BackColor = Color.FromArgb(255, 150, 255)
                    '    .ForeColor = Color.FromArgb(255, 255, 255)
                    'End If

                    '.Col = .GetColFromID("alertmark") : .Text = r_dt.Rows(ix - 1).Item("alertmark").ToString().Trim           '17
                    'If r_dt.Rows(ix - 1).Item("alertmark").ToString() <> "" Then
                    '    .BackColor = Color.FromArgb(255, 255, 150)
                    '    .ForeColor = Color.FromArgb(0, 0, 0)
                    'End If

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
                    If r_dt.Rows(ix - 1).Item("plgbn").ToString <> "2" Then
                        .ForeColor = .BackColor
                    End If
                    .Col = .GetColFromID("corgrst") : .Text = r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim                '81

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

    Private Sub spdResult_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdResult.ButtonClicked

        If e.row < 1 Then Exit Sub

        Dim sBcNo As String = ""
        Dim sTestCd_p As String = ""

        Dim sBcNo_t As String = ""
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
                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")

                        For intRow As Integer = e.row + 1 To .MaxRows
                            .Row = intRow
                            .Col = .GetColFromID("testcd") : sTestCd = .Text : If sTestCd <> "" Then sTestCd = sTestCd.Substring(0, 5)
                            .Col = .GetColFromID("bcno") : sBcNo_t = .Text.Replace("-", "")

                            If sTestCd_p = sTestCd And sBcNo = sBcNo_t Then
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
                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")

                        For intRow As Integer = e.row + 1 To .MaxRows
                            .Row = intRow

                            .Col = .GetColFromID("testcd") : sTestCd = .Text : If sTestCd <> "" Then sTestCd = sTestCd.Substring(0, 5)
                            .Col = .GetColFromID("bcno") : sBcNo_t = .Text.Replace("-", "")

                            If sTestCd_p = sTestCd And sBcNo = sBcNo_t Then
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

            If m_dt_RstUsr Is Nothing Then Return

            Dim sID As String = ""
            Dim sNM As String = ""
            Dim sDT As String = ""

            '결과저장, 중간보고, 최종보고
            Me.lblReg.Text = ""
            Me.lblFN.Text = ""

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
                        Me.lblReg.Text = sDT + vbCrLf + sNM
                    End If
                ElseIf i = 3 Then
                    sID = a_dr(0).Item("fnid").ToString().Trim
                    sNM = a_dr(0).Item("fnnm").ToString().Trim
                    sDT = a_dr(0).Item("fndt").ToString().Trim

                    If Not sID + sNM + sDT = "" Then
                        Me.lblFN.Text = sDT + vbCrLf + sNM
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

        If e.row = 0 Then txtTestCd.Text = "" : Return

        Dim sBcNo As String = ""
        Dim sTnmd As String = ""
        Dim sTestCd As String = ""
        Dim sSpcCd As String = ""
        Dim sTcdGbn As String = ""

        With spdResult
            .Row = e.row
            .Col = .GetColFromID("testcd") : txtTestCd.Text = .Text
            .Col = .GetColFromID("bcno") : sBcNo = .Text

            If Me.txtBcNo.Text <> sBcNo Then
                Me.txtBcNo.Text = sBcNo
                If sBcNo <> "" Then sbDisplay_RegNm(sBcNo)
            End If

        End With

        sTestCd = Ctrl.Get_Code(Me.spdResult, "testcd", e.row)
        sbDisplay_RegNm_Test(sTestCd)

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
            Dim sRst As String = ""
            Dim sBcNo As String = ""
            Dim sTestCd As String = ""

            Select Case Convert.ToInt32(e.keyCode)
                Case Keys.PageUp, Keys.PageDown
                    e.keyCode = 0

                Case 37, 39, 229, 27 ' 화살표 키
                    lstCode.Items.Clear()
                    lstCode.Hide()
                    pnlCode.Visible = False

                Case 38 ' 방향 위키
                    If lstCode.Visible = True Then
                        If lstCode.SelectedIndex > -1 Then
                            If lstCode.SelectedIndex > 0 Then
                                lstCode.SelectedIndex -= 1
                            End If
                        Else
                            lstCode.SelectedIndex = lstCode.Items.Count - 1
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
                    With Me.spdResult
                        Dim iRow As Integer = .ActiveRow

                        .Row = iRow
                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "`")
                        .Col = .GetColFromID("orgrst") : sRst = .Text.Replace("'", "`") : .Text = sRst
                        .Col = .GetColFromID("testcd") : sTestCd = .Text

                        If Me.lstCode.Visible Then
                            If Me.lstCode.SelectedIndex >= 0 Then
                                sRst = Me.lstCode.Text.Split(Chr(9))(1)
                                .Col = .GetColFromID("orgrst") : .Text = sRst
                            End If
                        End If
                        .Col = .GetColFromID("viewrst") : .Text = sRst

                        sbSet_ResultView(iRow)

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

        Dim sRstLLen As String = ""
        Dim sRstULen As String = ""
        Dim sRstType As String = ""
        Dim sCutOpt As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("rsttype") : sRstType = .Text
            .Col = .GetColFromID("rstllen") : sRstLLen = .Text
            .Col = .GetColFromID("rstulen") : sRstULen = .Text
            .Col = .GetColFromID("cutopt") : sCutOpt = .Text

            If (sRstType = "0" Or sRstType = "1") And sRstLLen <> "" And rsRst <> "" And IsNumeric(rsRst) Then
                Dim iPos As Integer = InStr(rsRst, ".")

                If Val(sRstLLen) >= 0 Then
                    .Col = .GetColFromID("rsttype")

                    Dim sDecimal As String = "0"
                    Dim iDecimal As Integer = CInt(sRstLLen)
                    If iDecimal > 0 Then
                        sDecimal = sDecimal & "." & New String(Chr(Asc("0")), iDecimal)
                    End If

                    Select Case sCutOpt
                        Case "0", "3"   ' 0 : 반올림처리없음(입력그대로). 3 : 내림
                            If iPos > 0 Then
                                If Len(rsRst) >= iPos + iDecimal Then
                                    rsRst = Mid(rsRst, 1, iPos + iDecimal)
                                End If
                            End If
                        Case "1"    ' 1 : 올림
                            If iPos > 0 Then
                                If Len(rsRst) >= iPos + iDecimal Then
                                    Dim strRstTmp As String
                                    strRstTmp = Mid(rsRst, 1, iPos + iDecimal)
                                    If Len(rsRst) >= iPos + iDecimal + 1 Then
                                        If Mid(rsRst, iPos + iDecimal + 1, 1) > "0" Then
                                            strRstTmp += "9"
                                        End If
                                    End If
                                    rsRst = strRstTmp
                                End If
                            End If
                        Case "2"    ' 2 : 반올림
                    End Select

                    rsRst = Format(Val(rsRst), sDecimal).ToString
                End If

                If Val(sRstULen) > 0 Then
                    If CInt(sRstULen) < iPos - 1 Then
                        Dim sMsg As String = "결과정수크기" + sRstULen + " 보다 큰 값이 입력되었습니다."
                        MsgBox(sMsg, MsgBoxStyle.Information)
                    End If
                End If
            End If

            If sRstType = "1" And rsRst <> "" And IsNumeric(rsRst) = False Then
                Dim sMsg As String = "숫자 결과만 입력할 수 있습니다."
                MsgBox(sMsg, MsgBoxStyle.Information)
            End If
        End With

        fnRstTypeCheck = rsRst

    End Function

    ' 결과유형 체크
    Private Sub sbRstTypeCheck(ByVal riRow As Integer)

        Dim sRstLLen As String = ""
        Dim sRstULen As String = ""
        Dim sRstType As String = ""
        Dim sCutOpt As String = ""
        Dim sRst As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : sRst = .Text
            .Col = .GetColFromID("rsttype") : sRstType = .Text
            .Col = .GetColFromID("rstllen") : sRstLLen = .Text
            .Col = .GetColFromID("rstulen") : sRstULen = .Text
            .Col = .GetColFromID("cutopt") : sCutOpt = .Text

            If (sRstType = "0" Or sRstType = "1") And sRstLLen <> "" And sRst <> "" And IsNumeric(sRst) Then
                Dim iPos As Integer = InStr(sRst, ".")

                If Val(sRstLLen) >= 0 Then
                    .Col = .GetColFromID("rsttype")

                    Dim sDecimal As String = "0"
                    Dim iDecimal As Integer = CInt(sRstLLen)
                    If iDecimal > 0 Then
                        sDecimal = sDecimal + "." + New String(Chr(Asc("0")), iDecimal)
                    End If

                    Select Case sCutOpt
                        Case "0", "3"   ' 0 : 반올림처리없음(입력그대로). 3 : 내림
                            If iPos > 0 Then
                                If Len(sRst) >= iPos + iDecimal Then
                                    sRst = Mid(sRst, 1, iPos + iDecimal)
                                End If
                            End If
                        Case "1"    ' 1 : 올림
                            If iPos > 0 Then
                                If Len(sRst) >= iPos + iDecimal Then
                                    Dim sRstTmp As String
                                    sRstTmp = Mid(sRst, 1, iPos + iDecimal)
                                    If Len(sRst) >= iPos + iDecimal + 1 Then
                                        If Mid(sRst, iPos + iDecimal + 1, 1) > "0" Then
                                            sRstTmp += "9"
                                        End If
                                    End If
                                    sRst = sRstTmp
                                End If
                            End If
                        Case "2"    ' 2 : 반올림

                    End Select
                    .Col = .GetColFromID("viewrst") : .Text = Format(Val(sRst), sDecimal).ToString
                End If

                If Val(sRstULen) > 0 Then
                    If CInt(sRstULen) < iPos - 1 Then
                        Dim sMsg As String = "결과정수크기" + sRstULen + " 보다 큰 값이 입력되었습니다."
                        MsgBox(sMsg, MsgBoxStyle.Information)

                        .Col = .GetColFromID("orgrst") : .Text = ""
                        .Col = .GetColFromID("viewrst") : .Text = ""
                    End If
                End If
            End If

            If sRstType = "1" And sRst <> "" And IsNumeric(sRst) = False Then
                Dim sMsg As String = "숫자 결과만 입력할 수 있습니다."
                MsgBox(sMsg, MsgBoxStyle.Information)
            End If
        End With
    End Sub

    Private Sub sbUJudgCheck(ByVal riRow As Integer)
        Dim sRefL As String
        Dim sRefH As String
        Dim sRefHs As String
        Dim sRefLs As String

        Dim sJudgType As String = ""

        Dim sRefGbn As String = ""
        Dim sHLmark As String = ""
        Dim sRst As String = "", sViewRst As String = "", sOrgRst As String = "", sMark As String = ""
        Dim sURst As String = ""

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

                            .Col = .GetColFromID("ujudglt1") : sURst = .Text

                            Select Case Mid(sJudgType, 1, 3)
                                Case "210"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "211"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "212"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & "(" & sViewRst & ")"
                                Case "213"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & " " & sViewRst & ""
                                Case "214"
                                    .Col = .GetColFromID("viewrst") : .Text = sViewRst & " " & sURst & ""
                            End Select
                        End If

                        If sHLmark = "H" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt2") : sURst = .Text

                            Select Case Mid(sJudgType, 4, 3)
                                Case "220"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "221"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "222"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & "(" & sOrgRst & ")"
                                Case "223"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & " " & sOrgRst & ""
                                Case "224"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sURst & ""
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

                            .Col = .GetColFromID("ujudglt1") : sURst = .Text
                            Select Case Mid(sJudgType, 1, 3)
                                Case "310"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "311"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "312"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & "(" & sOrgRst & ")"
                                Case "313"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & " " & sOrgRst & ""
                                Case "314"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sURst & ""
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

                            .Col = .GetColFromID("ujudglt2") : sURst = .Text
                            Select Case Mid(sJudgType, 4, 3)
                                Case "320"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "321"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "322"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & "(" & sOrgRst & ")"
                                Case "323"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & " " & sOrgRst & ""
                                Case "324"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sURst & ""
                            End Select
                        End If

                        If sHLmark = "H" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt3") : sURst = .Text
                            Select Case Mid(sJudgType, 7, 3)
                                Case "330"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "331"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst
                                Case "332"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & "(" & sOrgRst & ")"
                                Case "333"
                                    .Col = .GetColFromID("viewrst") : .Text = sURst & " " & sOrgRst & ""
                                Case "334"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sURst & ""
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

                sRst = sRst.Replace(">=", "").Replace("<=", "").Replace(">", "").Replace("<", "")

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
        End With
    End Sub

    ' 패닉 체크
    ' 2 : 패닉 하한치, 상한치 사용
    Private Sub sbPanicCheck(ByVal riRow As Integer, Optional ByVal rdt_RstCd As DataTable = Nothing)

        Dim sRst As String = ""
        Dim sPanicGbn As String = ""
        Dim sPanicL As String = ""
        Dim sPanicH As String = ""
        Dim sGrade As String = ""
        Dim sTestCd As String = ""
        Dim sPanicMark As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : sRst = .Text
            .Col = .GetColFromID("panicgbn") : sPanicGbn = .Text

            sRst = sRst.Replace(">=", "").Replace("<=", "").Replace(">", "").Replace("<", "")

            Select Case sPanicGbn
                Case "4", "5", "6"
                    .Col = .GetColFromID("testcd") : sTestCd = .Text

                    If rdt_RstCd Is Nothing Then Exit Sub

                    Dim foundRows As DataRow() = rdt_RstCd.Select("testcd = '" & sTestCd & "'")

                    Dim r As DataRow
                    For Each r In foundRows
                        If sRst = r.Item("rstcont").ToString Then
                            sGrade = r.Item("grade").ToString
                            Exit For
                        End If
                    Next r
            End Select

            Select Case sPanicGbn
                Case "1"    ' 패닉하한치만 사용
                    .Col = .GetColFromID("panicl") : sPanicL = .Text

                    If IsNumeric(sRst) Then
                        If Val(sRst) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "2"    ' 패닉상한치만 사용
                    .Col = .GetColFromID("panich") : sPanicH = .Text
                    If IsNumeric(sRst) Then
                        If Val(sRst) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "3"    ' 모두 사용
                    .Col = .GetColFromID("panicl") : sPanicL = .Text
                    .Col = .GetColFromID("panich") : sPanicH = .Text

                    If IsNumeric(sRst) Then
                        If Val(sRst) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                        If Val(sRst) > Val(sPanicH) Then
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
            Dim lgDateDiff As Long = 0
            Dim strDateDiff As String = ""
            Dim dtBFFNDT As Date
            Dim sDeltaGbn As String = ""
            Dim sRst As String = ""
            Dim sOldRst As String = ""
            Dim sDeltaL As String = ""
            Dim sDeltaH As String = ""
            Dim sDeltaMark As String = ""

            With spdResult
                .Row = riRow
                If .GetColFromID("bffndt1") < 0 Then
                    Exit Sub
                End If
                .Col = .GetColFromID("bffndt1")
                If .Text <> "" Then
                    dtBFFNDT = CDate(.Text)
                    lgDateDiff = DateDiff(DateInterval.Day, dtBFFNDT, MainServerDateTime.mServerDateTime)
                    If lgDateDiff < 1 Then
                        strDateDiff = "1"
                    Else
                        strDateDiff = Str(lgDateDiff).Trim
                    End If
                    .Col = .GetColFromID("deltaday")
                    If Val(strDateDiff) > Val(.Text) Then Exit Sub
                Else
                    Exit Sub
                End If

                .Col = .GetColFromID("deltagbn") : sDeltaGbn = .Text
                .Col = .GetColFromID("orgrst") : sRst = .Text

                sRst = sRst.Replace(">=", "").Replace("<=", "").Replace(">", "").Replace("<", "")

                If sRst = "" Then
                    .Col = .GetColFromID("deltamark") : .Text = ""
                    .BackColor = Color.White
                    .ForeColor = Color.Black
                    Exit Sub
                End If
                .Col = .GetColFromID("bforgrst1") : sOldRst = .Text

                sOldRst = sOldRst.Replace(">=", "").Replace("<=", "").Replace(">", "").Replace("<", "")

                If sRst.Trim = "" Then Exit Sub
                If sOldRst.Trim = "" Then Exit Sub

                .Col = .GetColFromID("deltah") : sDeltaH = .Text
                .Col = .GetColFromID("deltal") : sDeltaL = .Text

                Select Case sDeltaGbn
                    Case "1", "2", "3", "4"
                        If IsNumeric(sRst) = False Then Exit Sub
                        If IsNumeric(sOldRst) = False Then Exit Sub
                End Select

                Select Case sDeltaGbn
                    Case "1"    ' 1 : 변화차 = 현재결과 - 이전결과,
                        If sDeltaH <> "" And Val(sRst) - Val(sOldRst) > Val(sDeltaH) Then
                            sDeltaMark = "D"
                        End If

                        If sDeltaL <> "" And Val(sRst) - Val(sOldRst) < Val(sDeltaL) Then
                            sDeltaMark = "D"
                        End If

                    Case "2"    ' 2: 변화비율 = 변화차/이전결과  * 100
                        If Val(sOldRst) = 0 Then
                            sDeltaMark = "D"
                        Else
                            If sDeltaH <> "" And ((Val(sRst) - Val(sOldRst)) / Val(sOldRst)) * 100 > Val(sDeltaH) Then
                                sDeltaMark = "D"
                            End If

                            If sDeltaL <> "" And ((Val(sRst) - Val(sOldRst)) / Val(sOldRst)) * 100 < Val(sDeltaL) Then
                                sDeltaMark = "D"
                            End If
                        End If

                    Case "3"    '기간당 변화차 = 변화차/기간
                        .Col = .GetColFromID("bffndt1")
                        If .Text <> "" Then
                            If sDeltaH <> "" And (Val(sRst) - Val(sOldRst)) / Val(strDateDiff) > Val(sDeltaH) Then
                                sDeltaMark = "D"
                            End If

                            If sDeltaL <> "" And (Val(sRst) - Val(sOldRst)) / Val(strDateDiff) < Val(sDeltaL) Then
                                sDeltaMark = "D"
                            End If
                        End If

                    Case "4"    '기간당 변화비율 = 변화비율/기간
                        .Col = .GetColFromID("bffndt1")
                        If .Text <> "" Then
                            If sDeltaH <> "" And ((Val(sRst) - Val(sOldRst)) / Val(sOldRst)) * 100 / Val(strDateDiff) > Val(sDeltaH) Then
                                sDeltaMark = "D"
                            End If

                            If sDeltaL <> "" And ((Val(sRst) - Val(sOldRst)) / Val(sOldRst)) * 100 / Val(strDateDiff) < Val(sDeltaL) Then
                                sDeltaMark = "D"
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
                            If dt.Rows(intIdx).Item("rstcont").ToString.Trim = sRst Then
                                strGrade = dt.Rows(intIdx).Item("grade").ToString
                                Exit For
                            End If
                        Next

                        For intIdx As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(intIdx).Item("rstcont").ToString.Trim = sOldRst Then
                                strGrade_Old = dt.Rows(intIdx).Item("grade").ToString
                                Exit For
                            End If
                        Next

                        If strGrade <> "" And strGrade_Old <> "" Then
                            If Math.Abs(Val(strGrade) - Val(strGrade_Old)) > Math.Abs(Val(sDeltaH)) Then
                                sDeltaMark = "D"
                            End If
                        End If

                End Select

                .Col = .GetColFromID("deltamark")
                If sDeltaMark = "D" Then
                    .Text = sDeltaMark
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

            strRst = strRst.Replace(">=", "").Replace("<=", "").Replace(">", "").Replace("<", "")

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
        Dim sAlertGbn As String = ""
        Dim sAlertL As String = ""
        Dim sAlertH As String = ""
        Dim sAlertMark As String = ""

        With spdResult
            .Row = rirow
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
                    If sAlertL = "" Then Exit Sub
                    If IsNumeric(sAlertL) = False Then Exit Sub

                    If IsNumeric(sORst) Then
                        If Val(sORst) < Val(sAlertL) Then
                            sAlertMark = "A"
                        End If
                    End If

                Case "2", "B"    ' 경고상한치만 사용
                    If sAlertH = "" Then Exit Sub
                    If IsNumeric(sAlertH) = False Then Exit Sub

                    If IsNumeric(sORst) Then
                        If Val(sORst) > Val(sAlertH) Then
                            sAlertMark = "A"
                        End If
                    End If
                Case "3", "C"    ' 모두 사용
                    If sAlertL = "" Then Exit Sub
                    If IsNumeric(sAlertL) = False Then Exit Sub
                    If sAlertH = "" Then Exit Sub
                    If IsNumeric(sAlertH) = False Then Exit Sub

                    If IsNumeric(sORst) Then
                        If Val(sORst) < Val(sAlertL) Then
                            sAlertMark = "A"
                        End If
                        If Val(sORst) > Val(sAlertH) Then
                            sAlertMark = "A"
                        End If
                    End If

                Case "4"    '-- 문자값 비교
                    If sAlertL = "" And sAlertH = "" Then Exit Sub
                    If sAlertL = "" Then sAlertL = sAlertH

                    If sORst.ToUpper = sAlertL.ToUpper Then sAlertMark = "A"
            End Select

            If sAlertMark = "" And (sAlertGbn = "5" Or sAlertGbn = "A" Or sAlertGbn = "B" Or sAlertGbn = "C") Then
                '-- Alert Rule
                Dim dr As DataRow() = m_dt_Alert_Rule.Select("testcd = '" + sTestCd + "'")


                If dr.Length > 0 Then
                    Dim intCnt As Integer = 0, intAlert As Integer = 0

                    If dr(0).Item("orgrst").ToString <> "" Then
                        intCnt += 1
                        If dr(0).Item("orgrst").ToString().IndexOf(sORst + ",") >= 0 Then intAlert += 1
                    End If

                    If dr(0).Item("viewrst").ToString <> "" Then
                        intCnt += 1
                        If dr(0).Item("viewrst").ToString().IndexOf(sVRst + ",") >= 0 Then intAlert += 1
                    End If

                    If sPanicMark <> "" Then
                        intCnt += 1
                        intAlert += 1
                    End If

                    If sDeltaMark <> "" Then
                        intCnt += 1
                        intAlert += 1
                    End If

                    If dr(0).Item("eqflag").ToString <> "" Then
                        intCnt += 1

                        If sEqFlag <> "" Then
                            If dr(0).Item("eqflag").ToString().IndexOf("^") >= 0 Then
                                Dim strBuf() As String = dr(0).Item("eqflag").ToString().Split("^"c)

                                If strBuf(1) = "" Then
                                    If strBuf(0) = "" Then
                                        intAlert += 1
                                    Else
                                        strBuf(0) += ","
                                        If strBuf(0).IndexOf(sEqFlag + ",") >= 0 Then intAlert += 1
                                    End If
                                Else
                                    If strBuf(0) = "" Then
                                        strBuf(1) += ","
                                        If strBuf(1).IndexOf(sTclsCd + ",") >= 0 Then intAlert += 1
                                    Else
                                        strBuf(0) += "," : strBuf(1) += ","
                                        If strBuf(0).IndexOf(sEqFlag + ",") >= 0 And strBuf(1).IndexOf(sTclsCd + ",") >= 0 Then intAlert += 1
                                    End If
                                End If
                            Else
                                If dr(0).Item("eqflag").ToString().IndexOf(sEqFlag + ",") >= 0 Then intAlert += 1
                            End If
                        End If

                    End If

                    If dr(0).Item("sex").ToString <> "" Then
                        intCnt += 1
                        If msSexAge.StartsWith(dr(0).Item("sex").ToString()) Then intAlert += 1
                    End If

                    If dr(0).Item("deptcds").ToString <> "" Then
                        intCnt += 1
                        If dr(0).Item("deptcds").ToString().IndexOf(msDeptCd + ",") >= 0 Then intAlert += 1
                    End If

                    If dr(0).Item("spccds").ToString <> "" Then
                        intCnt += 1
                        If dr(0).Item("spccds").ToString().IndexOf(sSpcCd + ",") >= 0 Then intAlert += 1
                    End If

                    If intCnt = intAlert Then sAlertMark = "A"
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

    'Private Sub sbDisplay_Update()

    '    Dim aryRst As New ArrayList
    '    Dim strBcNo As String = ""
    '    Dim sTestCd As String = ""
    '    Dim strOrgRst As String = ""
    '    Dim strViewRst As String = ""
    '    Dim strRstCmt As String = ""
    '    Dim strChk As String = ""
    '    Dim strIUD As String = ""

    '    With spdResult
    '        For intRow As Integer = 1 To .MaxRows
    '            .Row = intRow
    '            .Col = .GetColFromID("chk") : strChk = .Text
    '            .Col = .GetColFromID("iud") : strIUD = .Text
    '            .Col = .GetColFromID("orgrst") : strOrgRst = .Text
    '            .Col = .GetColFromID("viewrst") : strViewRst = .Text
    '            .Col = .GetColFromID("rstcmt") : strRstCmt = .Text
    '            .Col = .GetColFromID("testcd") : sTestCd = .Text
    '            .Col = .GetColFromID("bcno") : strBcNo = .Text

    '            Dim objRstInfo As New RST_INFO

    '            With objRstInfo
    '                .msBcNo = strBcNo
    '                .msChk = strChk
    '                .msIUD = strIUD
    '                .msTestCd = sTestCd
    '                .msBcNo = strBcNo
    '                .msOrgRst = strOrgRst
    '                .msViewRst = strViewRst
    '                .msRstCmt = strRstCmt
    '            End With

    '            aryRst.Add(objRstInfo)
    '        Next
    '    End With


    '    Dim dt As New DataTable
    '    dt = LISAPP.DA_R.fnGet_Result_bcno("", "", False, msTestCd, "", "")

    '    sbDisplay_ResultView(dt)

    '    With spdResult
    '        For introw = 1 To .MaxRows
    '            For intidx = 0 To aryRst.Count - 1
    '                .Row = introw
    '                .Col = .GetColFromID("testcd") : sTestCd = .Text
    '                .Col = .GetColFromID("bcno") : strBcNo = .Text

    '                If strBcNo = CType(aryRst.Item(intidx), RST_INFO).msBcNo And sTestCd = CType(aryRst(intidx), RST_INFO).msTestCd Then
    '                    .Row = introw : .Col = .GetColFromID("chk") : .Text = CType(aryRst.Item(intidx), RST_INFO).msChk
    '                    .Row = introw : .Col = .GetColFromID("iud") : .Text = CType(aryRst.Item(intidx), RST_INFO).msIUD
    '                    .Row = introw : .Col = .GetColFromID("orgrst") : .Text = CType(aryRst.Item(intidx), RST_INFO).msOrgRst
    '                    .Row = introw : .Col = .GetColFromID("viewrst") : .Text = CType(aryRst.Item(intidx), RST_INFO).msViewRst
    '                    .Row = introw : .Col = .GetColFromID("rstcmt") : .Text = CType(aryRst.Item(intidx), RST_INFO).msRstCmt
    '                    Exit For
    '                End If
    '            Next

    '        Next

    '    End With
    'End Sub

    Private Sub spdResult_KeyUpEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdResult.KeyUpEvent
        Dim sFn As String = "Sub spdOrdList_KeyUpEvent(Object, AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdOrdListR.KeyUpEvent"

        Dim sTestCd As String = ""
        Dim strRst As String = ""
        Dim intPos As Integer = Me.lstEx.Location.Y

        Select Case Convert.ToInt32(e.keyCode)
            Case 37, 38, 39, 40, 229 ' 화살표 키                
            Case 27     ' ESC
            Case Keys.F4, Keys.F9, Keys.F11, Keys.F12
            Case 13
            Case Else
                msObJName = spdResult.Name
                With Me.spdResult
                    If .ActiveCol <> .GetColFromID("orgrst") Then
                        Exit Sub
                    End If
                    .Row = .ActiveRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("orgrst") : strRst = .Text
                    If intPos = 0 Then
                        pnlCode.Top = 444
                    Else
                        pnlCode.Top = intPos
                    End If

                    DP_Common.sbDispaly_test_rstcd(m_dt_RstCdHelp, Convert.ToString(sTestCd), lstCode)  ' 검사항목별 결과코드 표시

                    txtOrgRst.Text = strRst
                    txtTestCd.Text = sTestCd

                    DP_Common.sbFindPosition(lstCode, Convert.ToString(strRst))

                    '결과입력 불가로 주석처리
                    'spdResult.Action = FPSpreadADO.ActionConstants.ActionActiveCell
                    If pnlCode.Visible = False Then
                        If lstCode.Items.Count > 0 Then
                            pnlCode.Visible = True
                        Else
                            pnlCode.Visible = False
                        End If
                    End If
                End With
        End Select

    End Sub

    Private Sub spdResult_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdResult.LeaveCell

        If mbLeveCellGbn = False Then Return

        With spdResult

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

                    Dim sHelp As String = fnGetTextTipFetch(spdResult, e.row)

                    e.tipText = sHelp

                    Dim asHelp() As String = Split(sHelp, vbCrLf)
                    Dim sMaxHelp As Single
                    Dim sHelpWidth As Single
                    For iRow As Integer = 0 To UBound(asHelp)
                        sHelpWidth = Me.CreateGraphics.MeasureString(asHelp(iRow), .Font).Width
                        If sMaxHelp < sHelpWidth Then
                            sMaxHelp = sHelpWidth
                        End If
                        '
                    Next
                    e.tipWidth = CInt(sMaxHelp) * 14
            End Select

        End With

    End Sub

    Private Sub lstCode_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strRstCd As String = ""
        Dim arlRstCd() As String

        Dim strRst As String = ""
        Dim strRstCmt As String = ""
        Try
            Select Case msObJName
                Case "spdResult"
                    With spdResult
                        If lstCode.SelectedIndex > -1 Then
                            For i As Integer = 0 To lstCode.SelectedIndices.Count - 1
                                arlRstCd = Split(lstCode.Items(lstCode.SelectedIndices(i)).ToString(), Chr(9))
                                strRst = arlRstCd(1)
                                If arlRstCd(2).Trim <> "" Then
                                    strRstCmt = arlRstCd(2)
                                End If
                            Next

                            .Row = .ActiveRow
                            .Col = .GetColFromID("orgrst") : .Text = strRst.Replace("'", "`")
                            If .GetColFromID("rstcmt") > 0 Then
                                .Col = .GetColFromID("rstcmt") : .Text = strRstCmt
                            End If
                        End If
                        If .GetColFromID("orgrst") > 0 Then
                            .Row = .ActiveRow
                            .Col = .GetColFromID("orgrst") : strRst = .Text.Replace("'", "`")
                            .Col = .GetColFromID("viewrst") : .Text = strRst
                        End If

                        sbSet_ResultView(spdResult.ActiveRow)

                        .Col = .GetColFromID("orgrst")
                        .Focus()
                    End With

            End Select
            lstCode.Items.Clear()
            lstCode.Hide()
            pnlCode.Visible = False

        Catch ex As Exception

        End Try

    End Sub

    Private Sub lstCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try
            Select Case e.KeyCode
                Case Windows.Forms.Keys.Escape
                    lstCode.Hide()
                    pnlCode.Visible = False
                Case Windows.Forms.Keys.Enter
                    lstCode_DoubleClick(lstCode, New System.EventArgs())
                Case Else
                    Dim strRst As String = ""
                    With Me.spdResult
                        If .ActiveCol <> .GetColFromID("orgrst") Then
                            Exit Sub
                        End If
                        .Row = .ActiveRow
                        .Col = .GetColFromID("orgrst") : strRst = .Text

                        txtOrgRst.Text = txtOrgRst.Text + Convert.ToChar(e.KeyCode).ToString()

                    End With
                    'DP_Common.findPosition(lstCode, Convert.ToString(strRst))
            End Select
        Catch ex As Exception
            MsgBox("lstCode_KeyDown")
        End Try

    End Sub

    Private Sub lstCode_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Try
        '    Dim strRst As String = ""
        '    With Me.spdResult
        '        If .ActiveCol <> .GetColFromID("orgrst") Then
        '            Exit Sub
        '        End If
        '        .Row = .ActiveRow
        '        .Col = .GetColFromID("orgrst") : strRst = .Text

        '        txt2.Text = txt2.Text + e.KeyData
        '    End With
        '    DP_Common.findPosition(lstCode, Convert.ToString(strRst))
        'Catch ex As Exception
        '    MsgBox("lstCode_KeyUp")
        'End Try

    End Sub

    Private Sub AxRstInput_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        RaiseEvent FunctionKeyDown(sender, New System.Windows.Forms.KeyEventArgs(e.KeyCode))
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        spdResult.ColsFrozen = spdResult.GetColFromID("tnmd")

    End Sub

    Private Sub txtOrgRst_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrgRst.TextChanged
        DP_Common.sbFindPosition(lstCode, Convert.ToString(txtOrgRst.Text))
    End Sub

End Class



