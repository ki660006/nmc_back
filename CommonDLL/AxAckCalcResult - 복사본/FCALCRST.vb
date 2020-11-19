Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommConst

Public Class FCALCRST
    Private Const mc_sFile As String = "File : FCALCRST.vb, Class : FCALCRST" + vbTab

    Public CalcRstInfos As New ArrayList
    Public FrmLocation As New Drawing.Point
    Public msSexAge As String = ""

    Private m_al_UrVol As New ArrayList
    Private m_al_CoPeriod As New ArrayList

    Private m_color_chg As Drawing.Color = Drawing.Color.Lavender

    Private m_pt_Mouse As New Drawing.Point

    Private Sub sbCalculate()
        Dim sFn As String = "sbCalculate"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            Dim al_CTestCds As New ArrayList

            With spd
                For i As Integer = 1 To .MaxRows
                    Dim sCTestCd As String = Ctrl.Get_Code(spd, "ctestcd", i)
                    Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)

                    If sCTestCd = sTestCd Then
                        al_CTestCds.Add(sCTestCd)
                    End If
                Next
            End With

            For i As Integer = 1 To al_CTestCds.Count
                sbCalculate_Detail(al_CTestCds(i - 1).ToString)
            Next

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbCalculate_Detail(ByVal rsCTestcd As String)
        Dim sFn As String = "sbCalculate_Detail"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            With spd
                Dim iRow As Integer = .SearchCol(.GetColFromID("ctestcd"), 0, .MaxRows, rsCTestcd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iRow < 1 Then Return

                Dim sCalForm As String = Ctrl.Get_Code(spd, "calform", iRow)
                Dim sCalItems As String = Ctrl.Get_Code(spd, "calitems", iRow)
                Dim a_sCalItemTmp As String() = sCalItems.Split(CChar("/"))
                Dim a_sCalItem As String() = Nothing

                For i As Integer = 1 To a_sCalItemTmp.Length
                    If a_sCalItemTmp(i - 1).Trim = "" Then
                        Exit For
                    End If

                    ReDim Preserve a_sCalItem(i - 1)

                    a_sCalItem(i - 1) = a_sCalItemTmp(i - 1).Trim
                Next

                If a_sCalItem.Length < 1 Then Return

                Dim iCntCalc As Integer = 0

                For i As Integer = 1 To a_sCalItem.Length
                    Dim sSymbol As String = Chr(Asc("A") + i - 1)
                    Dim sTestCd As String = a_sCalItem(i - 1).Substring(0, "LTEST99".Length).Trim
                    Dim sSpcCd As String = a_sCalItem(i - 1).Substring("LTEST99".Length).Trim

                    Dim iRowC1 As Integer = .SearchCol(.GetColFromID("calform"), iRow, .MaxRows, sSymbol, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                    Dim iRowC2 As Integer = .SearchCol(.GetColFromID("testcd"), iRow, .MaxRows, sTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    'If iRowC1 <> iRowC2 Then Return
                    If iRowC1 <= iRow Then Return

                    Dim sOrgRst As String = Ctrl.Get_Code(spd, "orgrst", iRowC1)

                    If IsNumeric(sOrgRst) = False Then Return

                    sCalForm = sCalForm.Replace(sSymbol, sOrgRst)

                    iCntCalc += 1
                Next

                If msSexAge.IndexOf("/"c) >= 0 Then
                    sCalForm = sCalForm.Replace("~", IIf(msSexAge.Split("/"c)(0) = "M", "1", "0").ToString) '-- 남자
                    sCalForm = sCalForm.Replace("!", IIf(msSexAge.Split("/"c)(0) = "F", "1", "0").ToString) '-- 여자
                    sCalForm = sCalForm.Replace("@", msSexAge.Split("/"c)(1))                               '-- 나이
                End If

                If iCntCalc <> a_sCalItem.Length Then Return

                Dim sRstCalc As String = DB_CALC.fnFind_Calculated_Result(sCalForm)
                Dim iLenDot As Integer = 0

                If sRstCalc.IndexOf(".") >= 0 Then
                    iLenDot = sRstCalc.Substring(sRstCalc.IndexOf(".") + 1).Trim.Length
                End If

                If IsNumeric(sRstCalc) Then
                    If iLenDot > COMMON.CommLogin.LOGIN.PRG_CONST.CalcRst_DefFmt.Replace("0.", "").Length Then
                        sRstCalc = Val(sRstCalc).ToString(COMMON.CommLogin.LOGIN.PRG_CONST.CalcRst_DefFmt)
                    End If

                    .SetText(.GetColFromID("orgrst"), iRow, sRstCalc)
                    sbProc_ChkChangeRst(iRow)
                End If
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbClear_Init()
        Dim sFn As String = "sbClear_Init"

        Try
            Me.Location = FrmLocation

            Me.txtBcNo.BackColor = Color.Gainsboro

            With Me.spdRst
                .MaxRows = 0
            End With

            '#If DEBUG Then
            '            Me.chkOptCalc.Checked = False
            '#Else
            Me.chkOptCalc.Checked = True
            '#End If

            Me.btnCalc.Visible = Not Me.chkOptCalc.Checked

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisp_BcNo_CalcRstInfo()
        Dim sFn As String = "sbDisp_BcNo_CalcRstInfo"

        Try
            Dim sBcNo As String = Me.txtBcNo.Text.Trim.Replace("-", "")

            Me.Cursor = Cursors.WaitCursor

            Dim dt As DataTable = DB_CALC.fnGet_CalcRstInfo_BcNo(sBcNo)

            Ctrl.DisplayFastAfterSelect(Me.spdRst, dt, "L")

            With Me.spdRst
                .ReDraw = False

                For i As Integer = 1 To .MaxRows
                    Dim sCTestCd As String = ""
                    Dim sTestcd As String = ""

                    .Row = i
                    .Col = .GetColFromID("ctestcd") : sCTestCd = .Text
                    .Col = .GetColFromID("testcd") : sTestcd = .Text

                    If sCTestCd = sTestcd Then
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = i : .Row2 = i
                        .BlockMode = True
                        .BackColor = Ctrl.color_LightRed
                        .BlockMode = False

                        .Col = .GetColFromID("orgrst")
                        .Row = i
                        .Lock = True
                    End If

                    .Col = .GetColFromID("orgrst")
                    .Row = i
                    .CellTag = .Text

                    .Col = .GetColFromID("rstflg")
                    .Row = i
                    .CellTag = .Text

                    Select Case .Text
                        Case "3", "4"
                            .Text = FixedVariable.gsRstFlagF
                            .ForeColor = FixedVariable.g_color_FN
                        Case "2"
                            .Text = FixedVariable.gsRstFlagM
                        Case "1"
                            .Text = FixedVariable.gsRstFlagR
                    End Select
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.spdRst.ReDraw = True
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub sbDisp_BcNo_UrVolInfo()
        Dim sFn As String = "sbDisp_BcNo_UrVolInfo"

        Try
            m_al_UrVol.Clear()

            Dim sBcNo As String = Me.txtBcNo.Text.Trim.Replace("-", "")
            Dim sTGrpUv As String = "'" + Replace(Ctrl.Get_Code(Me.lblUrVol), ",", "','") + "'"

            Dim dt As DataTable = DB_CALC.fnGet_CalcUrVolInfo_BcNo(sBcNo, sTGrpUv)

            Dim bEmpty As Boolean = False

            If dt Is Nothing Then bEmpty = True
            If dt.Rows.Count = 0 Then bEmpty = True

            If bEmpty Then
                Me.lblUrVol.Visible = False
                Me.txtUrVol.Visible = False

                sbFocus_spdRst()

                Return
            End If

            Me.lblUrVol.Visible = True
            Me.txtUrVol.Visible = True

            Me.txtUrVol.Focus()

            For i As Integer = 1 To dt.Rows.Count
                m_al_UrVol.Add(dt.Rows(i - 1).Item("testcd").ToString)
            Next

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisp_BcNo_UrVolRst()
        Dim sFn As String = "sbDisp_BcNo_UrVolRst"

        Try
            With Me.spdRst
                For i As Integer = 1 To m_al_UrVol.Count
                    Dim iRow As Integer = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, m_al_UrVol(i - 1).ToString, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRow < 1 Then Continue For

                    .SetText(.GetColFromID("orgrst"), iRow, Me.txtUrVol.Text)
                    sbProc_ChkChangeRst(iRow)
                Next
            End With

            If Me.chkOptCalc.Checked Then
                sbCalculate()
            End If

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisp_BcNo_CoPeriodInfo()
        Dim sFn As String = "sbDisp_BcNo_CoPeriodInfo"

        Try
            m_al_CoPeriod.Clear()

            Dim sBcNo As String = Me.txtBcNo.Text.Trim.Replace("-", "")
            Dim sTGrpUv As String = "'" + Replace(Ctrl.Get_Code(Me.lblCoPeriod.Text), ",", "','") + "'"

            Dim dt As DataTable = DB_CALC.fnGet_CalcUrVolInfo_BcNo(sBcNo, sTGrpUv)

            Dim bEmpty As Boolean = False

            If dt Is Nothing Then bEmpty = True
            If dt.Rows.Count = 0 Then bEmpty = True

            If bEmpty Then
                Me.lblCoPeriod.Visible = False
                Me.txtCoPeriod.Visible = False

                sbFocus_spdRst()

                Return
            End If

            Me.lblCoPeriod.Visible = True
            Me.txtCoPeriod.Visible = True

            Me.txtCoPeriod.Focus()

            For i As Integer = 1 To dt.Rows.Count
                m_al_CoPeriod.Add(dt.Rows(i - 1).Item("testcd").ToString)
            Next

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisp_BcNo_CoPeriodRst()
        Dim sFn As String = "sbDisp_BcNo_CoPeriodRst"

        Try
            With Me.spdRst
                For i As Integer = 1 To m_al_CoPeriod.Count
                    Dim iRow As Integer = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, m_al_CoPeriod(i - 1).ToString, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRow < 1 Then Continue For

                    .SetText(.GetColFromID("orgrst"), iRow, Me.txtCoPeriod.Text)
                    sbProc_ChkChangeRst(iRow)
                Next
            End With

            If Me.chkOptCalc.Checked Then
                sbCalculate()
            End If

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisp_Pat_CalcRstInfo()
        Dim sFn As String = "sbDisp_Pat_CalcRstInfo"

        Me.Cursor = Cursors.WaitCursor

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            Dim sBcNo As String = Me.txtBcNo.Text.Trim.Replace("-", "")

            Dim al_CTestCds As New ArrayList

            With spd
                For i As Integer = 1 To .MaxRows
                    Dim sCTestcd As String = Ctrl.Get_Code(spd, "ctestcd", i)
                    Dim stestcd As String = Ctrl.Get_Code(spd, "testcd", i)

                    If sCTestcd = stestcd Then
                        al_CTestCds.Add(sCTestcd)
                    End If
                Next
            End With

            For i As Integer = 1 To al_CTestCds.Count
                sbDisp_Pat_CalcRstInfo_Detail(sBcNo, al_CTestCds(i - 1).ToString)
            Next

            If Me.chkOptCalc.Checked Then sbCalculate()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub sbDisp_Pat_CalcRstInfo_Detail(ByVal rsBcNo As String, ByVal rsCTestCd As String)
        Dim sFn As String = "sbDisp_Pat_CalcRstInfo_Detail"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            With spd
                .ReDraw = False

                Dim iRow As Integer = .SearchCol(.GetColFromID("ctestcd"), 0, .MaxRows, rsCTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iRow < 1 Then Return

                Dim sCalForm As String = Ctrl.Get_Code(spd, "calform", iRow)
                Dim sCalItems As String = Ctrl.Get_Code(spd, "calitems", iRow)
                Dim sCalDays As String = Ctrl.Get_Code(spd, "caldays", iRow)

                Dim a_sCalItemTmp As String() = sCalItems.Split(CChar("/"))
                Dim a_sCalItem As String() = Nothing

                For i As Integer = 1 To a_sCalItemTmp.Length
                    If a_sCalItemTmp(i - 1).Trim = "" Then
                        Exit For
                    End If

                    ReDim Preserve a_sCalItem(i - 1)

                    a_sCalItem(i - 1) = a_sCalItemTmp(i - 1).Trim
                Next

                If a_sCalItem.Length < 1 Then Return

                Dim iCntCalc As Integer = 0

                For i As Integer = 1 To a_sCalItem.Length
                    Dim sSymbol As String = Chr(Asc("A") + i - 1)
                    Dim sTestCd As String = a_sCalItem(i - 1).Substring(0, "LTEST99".Length).Trim
                    Dim sSpcCd As String = a_sCalItem(i - 1).Substring("LTEST99".Length).Trim

                    Dim iRowC1 As Integer = .SearchCol(.GetColFromID("calform"), iRow, .MaxRows, sSymbol, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                    Dim iRowC2 As Integer = .SearchCol(.GetColFromID("testcd"), iRow, .MaxRows, sTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRowC1 = iRowC2 And iRowC1 > iRow Then Continue For

                    Dim sCalRangeB As String = ""

                    .Row = iRow
                    .Col = .GetColFromID("calrange")
                    sCalRangeB = .Text
                    ' If iCalRangeB = "B" Then Continue For

                    iRowC2 = iRow + i
                    .MaxRows += 1
                    .InsertRows(iRowC2, 1)

                    .SetText(.GetColFromID("calform"), iRowC2, sSymbol)
                    .SetText(.GetColFromID("calitems"), iRowC2, "")
                    .SetText(.GetColFromID("ctestcd"), iRowC2, rsCTestCd)
                    .SetText(.GetColFromID("testcd"), iRowC2, sTestCd)
                    .SetText(.GetColFromID("caldays"), iRowC2, sCalDays)

                    If sCalDays = "" Then sCalDays = "9999"
                    Dim dt As DataTable = DB_CALC.fnGet_CalcRstInfo_Pat(rsBcNo, sTestCd, sSpcCd, sCalDays, sCalRangeB)

                    If dt.Rows.Count = 0 Then Continue For

                    .SetText(.GetColFromID("tnmd"), iRowC2, dt.Rows(0).Item("tnmd"))
                    .SetText(.GetColFromID("orgrst"), iRowC2, dt.Rows(0).Item("orgrst"))
                    .SetText(.GetColFromID("rstflg"), iRowC2, dt.Rows(0).Item("rstflg"))
                    .SetText(.GetColFromID("bcno"), iRowC2, dt.Rows(0).Item("bcno"))

                    .Col = .GetColFromID("orgrst")
                    .Row = iRowC2
                    If dt.Rows(0).Item("bcno").ToString <> Me.txtBcNo.Text Then
                        .Lock = True
                        .BackColor = Color.Gainsboro
                    End If

                    .Col = .GetColFromID("orgrst")
                    .Row = iRowC2
                    .CellTag = .Text

                    .Col = .GetColFromID("rstflg")
                    .Row = iRowC2
                    .CellTag = .Text

                    Select Case .Text
                        Case "3", "4"
                            .Text = FixedVariable.gsRstFlagF
                            .ForeColor = FixedVariable.g_color_FN
                        Case "2"
                            .Text = FixedVariable.gsRstFlagM
                        Case "1"
                            .Text = FixedVariable.gsRstFlagR
                    End Select
                Next
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbFocus_spdRst()
        Dim sFn As String = "sbFocus_spdRst()"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            With spd
                .Focus()

                Dim iRow As Integer = .SearchCol(.GetColFromID("calform"), 0, .MaxRows, "A", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iRow > 0 Then
                    .SetActiveCell(.GetColFromID("orgrst"), iRow)
                End If
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbMake_Return_CRIs()
        Dim sFn As String = "sbMake_Return_CRIs"

        Try
            CalcRstInfos.Clear()

            Dim sBcNo As String = Me.txtBcNo.Text.Trim.Replace("-", "")

            With Me.spdRst
                For i As Integer = 1 To .MaxRows
                    Dim cri As New CalcRstInfo

                    Dim sBcNoTmp As String = Ctrl.Get_Code(Me.spdRst, "bcno", i)
                    Dim sTestCd As String = Ctrl.Get_Code(Me.spdRst, "testcd", i)
                    Dim sOrgRst As String = Ctrl.Get_Code(Me.spdRst, "orgrst", i)

                    If sBcNo = sBcNoTmp Then
                        cri.TestCd = sTestCd
                        cri.OrgRst = sOrgRst

                        CalcRstInfos.Add(cri)
                    End If

                    cri = Nothing
                Next
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbProc_ChkChangeRst(ByVal riRow As Integer)
        Dim sFn As String = "sbProc_ChkChangeRst(Integer)"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            Dim sOrgRst As String = Ctrl.Get_Code(spd, "orgrst", riRow).Trim
            Dim sOrgRstTag As String = Ctrl.Get_Code_Tag(spd, "orgrst", riRow).Trim

            With spd
                If sOrgRst = sOrgRstTag Then
                    .Col = .GetColFromID("orgrst")
                    .Row = riRow
                    .BackColor = Drawing.Color.White
                Else
                    .Col = .GetColFromID("orgrst")
                    .Row = riRow
                    .BackColor = m_color_chg
                End If
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbProc_ChkUnlockRst(ByVal riCol As Integer, ByVal riRow As Integer)
        Dim sFn As String = "sbProc_ChkUnlockRst(Integer)"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            If riCol <> spd.GetColFromID("orgrst") Then Return
            If riRow < 1 Then Return

            Dim sMsg As String = ""

            With spd
                .Col = riCol
                .Row = riRow

                If .Lock Then
                    sMsg = ""
                    sMsg += "계산식과 관련하여 잠겨있는 결과란입니다." + vbCrLf + vbCrLf
                    sMsg += "잠금을 해제하시겠습니까?" + vbCrLf + vbCrLf

                    If MsgBox(sMsg, MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "잠금 해제 확인") = MsgBoxResult.No Then Return

                    .Col = riCol
                    .Row = riRow
                    .Lock = False
                End If
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    '<--- Control Event --->
    Private Sub FCALCRST_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FCALCRST_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sbClear_Init()

        sbDisp_BcNo_CalcRstInfo()

        sbDisp_BcNo_UrVolInfo()
        sbDisp_BcNo_CoPeriodInfo()

    End Sub

    Private Sub FCALCRST_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        sbDisp_Pat_CalcRstInfo()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        sbMake_Return_CRIs()

        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalc.Click
        m_pt_Mouse = Windows.Forms.Cursor.Position

        sbCalculate()

        Me.btnApply.Focus()

        Windows.Forms.Cursor.Position = New Drawing.Point(m_pt_Mouse.X + Me.btnCalc.Width, m_pt_Mouse.Y)
    End Sub

    Private Sub chkOptCalc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptCalc.CheckedChanged
        Me.btnCalc.Visible = Not Me.chkOptCalc.Checked
        If Me.chkOptCalc.Checked Then sbCalculate()
    End Sub

    '    Private Sub lblBcNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBcNo.Click
    '#If DEBUG Then
    '        'IF 테스트
    '        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

    '        Dim sBcNoCur As String = Me.txtBcNo.Text.Trim.Replace("-", "")

    '        Dim al_ri As New ArrayList

    '        With spd
    '            For i As Integer = 1 To .MaxRows
    '                Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)
    '                Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
    '                Dim sOrgRst As String = Ctrl.Get_Code(spd, "orgrst", i)

    '                If sBcNoCur <> sBcNo Then Continue For
    '                If sOrgRst = "" Then Continue For

    '                Dim ri As DB_CALC.ResultInfo = New LISAPP.APP_R.ResultInfo

    '                ri.TestCd = sTestCd
    '                ri.OrgRst = sOrgRst
    '                ri.RstCmt = ""

    '                al_ri.Add(ri)

    '                ri = Nothing
    '            Next
    '        End With

    '        Dim si As New STU_SampleInfo

    '        si.BCNo = sBcNoCur
    '        si.EqCd = ""
    '        si.UsrID = COMMON.CommLogin.LOGIN.USER_INFO.USRID
    '        si.IntSeqNo = ""
    '        si.Rack = ""
    '        si.Pos = ""
    '        si.EqBCNo = ""
    '        si.QcDay = ""
    '        si.SenderID = Me.Name
    '        si.RegStep = "3"

    '        Dim al_suc As New ArrayList
    '        Dim da_regrst As New LISAPP.APP_R.RegFn(0)
    '        Dim iReg As Integer = 0

    '        Try
    '            iReg = da_regrst.RegServer(al_ri, si, al_suc)

    '            da_regrst.DbRollback()

    '            MsgBox(iReg.ToString)

    '            For i As Integer = 1 To al_suc.Count
    '                Debug.WriteLine(al_suc(i - 1).ToString)
    '            Next

    '        Catch ex As Exception
    '            da_regrst.DbRollback()

    '            MsgBox(ex.ToString)

    '        End Try
    '#End If
    '    End Sub

    Private Sub spdRst_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdRst.Change
        If e.row < 1 Then Return
        If e.col <> Me.spdRst.GetColFromID("orgrst") Then Return

        sbProc_ChkChangeRst(e.row)

        If Me.chkOptCalc.Checked = False Then Return

        sbCalculate()
    End Sub

    Private Sub spdRst_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdRst.KeyDownEvent
        If Me.spdRst.MaxRows < 1 Then Return

        If e.keyCode = Keys.Enter Then
            If Me.spdRst.ActiveRow = Me.spdRst.MaxRows Then
                '> Focus 이동
                If Me.chkOptCalc.Checked Then
                    Me.btnApply.Focus()
                Else
                    Me.btnCalc.Focus()
                End If
            End If

        ElseIf e.keyCode = Keys.F2 Then
            sbProc_ChkUnlockRst(Me.spdRst.ActiveCol, Me.spdRst.ActiveRow)

        End If
    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown
#If DEBUG Then
        If e.KeyCode = Keys.Enter Then
            sbDisp_BcNo_CalcRstInfo()

            sbDisp_BcNo_UrVolInfo()
            sbDisp_BcNo_CoPeriodInfo()

            sbDisp_Pat_CalcRstInfo()

        ElseIf e.KeyCode = Keys.F2 Then
            Me.txtBcNo.ReadOnly = False
            Me.txtBcNo.BackColor = Color.White

        End If
#End If
    End Sub

    Private Sub txtUrVol_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUrVol.KeyDown
        If e.KeyCode = Keys.Enter Then
            sbDisp_BcNo_UrVolRst()

            '> Focus 이동
            If Me.chkOptCalc.Checked Then
                If txtCoPeriod.Visible Then
                    txtCoPeriod.SelectAll()
                    txtCoPeriod.Focus()
                Else
                    Me.btnApply.Focus()
                End If
                'Me.btnApply.Focus()
            Else
                If txtCoPeriod.Visible Then
                    txtCoPeriod.SelectAll()
                    txtCoPeriod.Focus()
                Else
                    Me.btnCalc.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtUrVol_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUrVol.KeyPress
        Select Case e.KeyChar
            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "."c
                e.Handled = False

            Case Convert.ToChar(Windows.Forms.Keys.Back)
                e.Handled = False

            Case Else
                e.Handled = True

        End Select
    End Sub

    Private Sub txtCoPeriod_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCoPeriod.KeyDown
        If e.KeyCode = Keys.Enter Then
            sbDisp_BcNo_CoPeriodRst()

            '> Focus 이동
            If Me.chkOptCalc.Checked Then
                Me.btnApply.Focus()
            Else
                Me.btnCalc.Focus()
            End If
        End If
    End Sub

    Private Sub txtCoPeriod_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCoPeriod.KeyPress
        Select Case e.KeyChar
            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "."c
                e.Handled = False

            Case Convert.ToChar(Windows.Forms.Keys.Back)
                e.Handled = False

            Case Else
                e.Handled = True

        End Select
    End Sub

End Class

