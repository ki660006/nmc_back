'>>> [44] 소견값 자동변환
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF44
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF43.vb, Class : FDF43" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_CVT_CMT

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

            With spdCvtTest
                .Row = -1 : .Col = .GetColFromID("spccd") : .TypeEditLen = PRG_CONST.Len_SpcCd
            End With

            miSelectKey = 1

            sbInitialize_ErrProvider()
            sbInitialize_Control()

            '<< JJH 스프레드 작동 버그로 포커스 추가
            Me.spdCvtTest.Focus()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn As String = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_Control(Optional ByVal iMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If iMode = 0 Then
                Me.txtCmtCd.Text = "" : Me.txtSlipNmd.Text = "" : Me.btnUE.Visible = False
                Me.txtCmtCont.Text = ""

                With Me.spdCvtTest
                    .MaxRows = 26
                    For ix As Integer = 1 To 26
                        .Row = ix
                        .Col = 1 : .Text = Convert.ToChar(ix + 64).ToString
                    Next
                    .ClearRange(2, 1, .MaxCols, .MaxRows, True)
                End With

                Me.txtCvtForm.Text = ""
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = "" : Me.txtRegNm.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = ""
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdDetail(ByVal rsCmtCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            Call sbDisplayCdDetail_Calc(rsCmtCd)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Calc(ByVal rsCmtCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Calc_RST(String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control = Nothing
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetCvtCmtInfo(rsCmtCd)
            Else
                dt = mobjDAF.GetCvtCmtInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsCmtCd)
            End If

            '초기화
            sbInitialize()

            If dt.Rows.Count < 1 Then Return

            miSelectKey = 1

            With dt
                Me.txtCmtCd.Text = .Rows(0).Item("cmtcd").ToString()
                Me.txtSlipNmd.Text = .Rows(0).Item("slipnmd").ToString()
                Me.txtCmtCont.Text = .Rows(0).Item("cmtcont").ToString

                Me.txtCvtForm.Text = .Rows(0).Item("cvtform").ToString()

                Me.txtRegDT.Text = .Rows(0).Item("regdt").ToString()
                Me.txtRegID.Text = .Rows(0).Item("regid").ToString()
                Me.txtModNm.Text = .Rows(0).Item("modnm").ToString()
                Me.txtRegNm.Text = .Rows(0).Item("regnm").ToString()
            End With

            For intIdx As Integer = 0 To dt.Rows.Count - 1
                With Me.spdCvtTest

                    Dim intRow As Integer = 0
                    For intIx2 As Integer = 1 To .MaxRows
                        .Row = intIx2
                        .Col = .GetColFromID("param")
                        If .Text = dt.Rows(intIdx).Item("cvtparam").ToString.Trim Then

                            If dt.Rows(intIdx).Item("reflgbn").ToString.Trim = "" Then
                                .Col = .GetColFromID("refgbn") : .TypeComboBoxCurSel = 0
                            Else
                                .Col = .GetColFromID("refgbn") : .TypeComboBoxCurSel = Convert.ToInt16(dt.Rows(intIdx).Item("reflgbn").ToString.Trim)
                            End If

                            .Col = .GetColFromID("refl") : .Text = dt.Rows(intIdx).Item("refl").ToString.Trim
                            If dt.Rows(intIdx).Item("refls").ToString.Trim = "" Or dt.Rows(intIdx).Item("refl").ToString.Trim = "" Then
                                .Col = .GetColFromID("refls") : .TypeComboBoxCurSel = -1
                            Else
                                .Col = .GetColFromID("refls") : .TypeComboBoxCurSel = Convert.ToInt16(dt.Rows(intIdx).Item("refls").ToString.Trim)
                            End If
                            .Col = .GetColFromID("testcd") : .Text = dt.Rows(intIdx).Item("testcd").ToString.Trim
                            .Col = .GetColFromID("spccd") : .Text = dt.Rows(intIdx).Item("spccd").ToString.Trim
                            .Col = .GetColFromID("tnmd") : .Text = dt.Rows(intIdx).Item("tnmd").ToString.Trim
                            .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(intIdx).Item("spcnmd").ToString.Trim

                            .Col = .GetColFromID("refh") : .Text = dt.Rows(intIdx).Item("refh").ToString.Trim
                            If dt.Rows(intIdx).Item("refhs").ToString.Trim = "" Or dt.Rows(intIdx).Item("refh").ToString.Trim = "" Then
                                .Col = .GetColFromID("refhs") : .TypeComboBoxCurSel = -1
                            Else
                                .Col = .GetColFromID("refhs") : .TypeComboBoxCurSel = Convert.ToInt16(dt.Rows(intIdx).Item("refhs").ToString.Trim)
                            End If

                            .Col = .GetColFromID("reflt") : .Text = dt.Rows(intIdx).Item("reflt").ToString.Trim
                            If dt.Rows(intIdx).Item("reflts").ToString.Trim = "" Or dt.Rows(intIdx).Item("reflt").ToString.Trim = "" Then
                                .Col = .GetColFromID("reflts") : .TypeComboBoxCurSel = -1
                            Else
                                .Col = .GetColFromID("reflts") : .TypeComboBoxCurSel = Convert.ToInt16(dt.Rows(intIdx).Item("reflts").ToString.Trim)
                            End If

                            Exit For
                        End If
                    Next
                    .Refresh()
                End With
            Next

            Me.txtModDT.Text = gsModDT
            Me.txtModID.Text = gsModID


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Len(txtCmtCd.Text.Trim) < 1 Then
                MsgBox("소견코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(txtCmtCont.Text.Trim) < 1 Then
                MsgBox("결과코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            Dim intCnt As Integer = 0
            Dim strCvtForm As String = txtCvtForm.Text.Trim

            With spdCvtTest
                For intRow As Integer = 1 To .MaxRows
                    Dim sGbn$ = "", sRefLGbn$ = "", sRefLs$ = "", sRefL$ = "", sRefHGbn$ = "", sRefHs$ = "", sRefH$ = ""
                    Dim sRefLt$ = "", sRefLts$ = "", sTestCd$ = "", sSpcCd$ = ""

                    .Row = intRow
                    .Col = .GetColFromID("param") : sGbn = "[" + .Text + "]"
                    .Col = .GetColFromID("refgbn") : sRefLGbn = .TypeComboBoxCurSel.ToString
                    .Col = .GetColFromID("refls") : sRefLs = .TypeComboBoxCurSel.ToString
                    .Col = .GetColFromID("refl") : sRefL = .Text
                    .Col = .GetColFromID("refhs") : sRefHs = .TypeComboBoxCurSel.ToString
                    .Col = .GetColFromID("refgbn") : sRefHGbn = .TypeComboBoxCurSel.ToString
                    .Col = .GetColFromID("refh") : sRefH = .Text
                    .Col = .GetColFromID("reflt") : sRefLt = .Text
                    .Col = .GetColFromID("reflts") : sRefLts = .TypeComboBoxCurSel.ToString
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text

                    If CInt(sRefLGbn) > 0 And CInt(sRefLs) >= 0 And sRefL <> "" Then
                    Else
                        sRefLGbn = "" : sRefLs = "" : sRefL = ""
                    End If

                    If CInt(sRefHGbn) > 0 And CInt(sRefHs) >= 0 And sRefH <> "" Then
                    Else
                        sRefHGbn = "" : sRefHs = "" : sRefH = ""
                    End If

                    If sRefLt <> "" And CInt(sRefLts) >= 0 Then
                    Else
                        sRefLt = "" : sRefLts = ""
                    End If

                    If (sRefLGbn + sRefLs + sRefL + sRefHGbn + sRefHs + sRefH + sRefLt + sRefLts).Length = 0 Then
                    Else
                        If sTestCd.Length >= 5 And sSpcCd <> "" Then
                            intCnt += 1
                            strCvtForm = strCvtForm.Replace(sGbn, "").Trim
                        End If
                    End If
                Next

                strCvtForm = strCvtForm.Replace("$$", "").Trim()
                strCvtForm = strCvtForm.Replace("||", "").Trim()
                strCvtForm = strCvtForm.Replace("(", "").Trim()
                strCvtForm = strCvtForm.Replace(")", "").Trim()

                If strCvtForm.Trim = "" And intCnt > 0 Then
                Else
                    MsgBox("계산식이 틀립니다.  정확하게 입력하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If

            End With

            fnValidate = True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetNewRegDT

            If DTable.Rows.Count > 0 Then
                fnGetSystemDT = DTable.Rows(0).Item(0).ToString
            Else
                MsgBox("시스템의 날짜를 초기화하지 못했습니다. 관리자에게 문의하시기 바랍니다!!", MsgBoxStyle.Information)
                fnGetSystemDT = Format(Now, "yyyy-MM-dd HH:mm:ss")

                Exit Function
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            fnGetSystemDT = Format(Now, "yyyy-MM-dd HH:mm:ss")
        End Try
    End Function

    Private Function fnCollectItemTable_81(ByVal asRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_82(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it81 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCvtTest
            Dim strCalForm$ = ""

            With it81
                .SetItemTable("CMTCD", 1, 1, Me.txtCmtCd.Text.Trim)
                .SetItemTable("CVTFORM", 2, 1, Me.txtCvtForm.Text.Trim)
                .SetItemTable("REGDT", 3, 1, asRegDT)
                .SetItemTable("REGID", 4, 1, USER_INFO.USRID)
                .SetItemTable("REGIP", 5, 1, USER_INFO.LOCALIP)
            End With

            fnCollectItemTable_81 = it81

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing

        End Try
    End Function

    Private Function fnCollectItemTable_82(ByVal asRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_82(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it82 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCvtTest

            With spdCvtTest
                For intRow As Integer = 1 To .MaxRows
                    Dim sGbn$ = "", sRefGbn$ = "", sRefLs$ = "", sRefL$ = "", sRefHs$ = "", sRefH$ = ""
                    Dim sRefLt$ = "", sRefLts$ = "", sTestCd$ = "", sSpcCd$ = ""

                    .Row = intRow
                    .Col = .GetColFromID("param") : sGbn = .Text
                    .Col = .GetColFromID("refgbn") : sRefGbn = .TypeComboBoxCurSel.ToString : If sRefGbn = "-1" Then sRefGbn = ""
                    .Col = .GetColFromID("refls") : sRefLs = .TypeComboBoxCurSel.ToString : If sRefLs = "-1" Then sRefLs = ""
                    .Col = .GetColFromID("refl") : sRefL = .Text
                    .Col = .GetColFromID("refhs") : sRefHs = .TypeComboBoxCurSel.ToString : If sRefHs = "-1" Then sRefHs = ""
                    .Col = .GetColFromID("refh") : sRefH = .Text
                    .Col = .GetColFromID("reflts") : sRefLts = .TypeComboBoxCurSel.ToString : If sRefLts = "-1" Then sRefLts = ""
                    .Col = .GetColFromID("reflt") : sRefLt = .Text

                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text

                    If sRefLs <> "" And sRefL <> "" Then
                    Else
                        sRefLs = "" : sRefL = ""
                    End If

                    If sRefHs <> "" And sRefH <> "" Then
                    Else
                        sRefHs = "" : sRefH = ""
                    End If

                    If sRefLt <> "" And sRefLts <> "" Then
                    Else
                        sRefLt = "" : sRefLts = ""
                    End If

                    If (sRefLs + sRefL + sRefHs + sRefH + sRefLt + sRefLts).Length < 1 Then
                        sRefGbn = ""
                    End If

                    If sTestCd <> "" And sSpcCd <> "" And (sRefGbn + sRefLs + sRefL + sRefHs + sRefH + sRefLt + sRefLts).Length > 0 Then
                        it82.SetItemTable("CMTCD", 1, intRow, Me.txtCmtCd.Text.Trim)
                        it82.SetItemTable("CVTPARAM", 2, intRow, sGbn)
                        it82.SetItemTable("TESTCD", 3, intRow, sTestCd)
                        it82.SetItemTable("SPCCD", 4, intRow, sSpcCd)
                        it82.SetItemTable("REFLGBN", 5, intRow, sRefGbn)
                        it82.SetItemTable("REFLS", 6, intRow, sRefLs)
                        it82.SetItemTable("REFL", 7, intRow, sRefL)
                        it82.SetItemTable("REFHGBN", 8, intRow, sRefGbn)
                        it82.SetItemTable("REFHS", 9, intRow, sRefHs)
                        it82.SetItemTable("REFH", 10, intRow, sRefH)
                        it82.SetItemTable("REFLTS", 11, intRow, sRefLts)
                        it82.SetItemTable("REFLT", 12, intRow, sRefLt)
                    End If
                Next
            End With

            fnCollectItemTable_82 = it82

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing

        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = ""

        Try
            Dim it81 As New LISAPP.ItemTableCollection
            Dim it82 As New LISAPP.ItemTableCollection
            Dim iRegType81 As Integer = 0, iRegType82 As Integer = 0
            Dim sRegDT As String

            iRegType81 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType82 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it81 = fnCollectItemTable_81(sRegDT)
            If it81.ItemTables.Count < 1 Then
                fnReg = False
                Exit Function
            End If

            it82 = fnCollectItemTable_82(sRegDT)
            If it82.ItemTables.Count < 1 Then
                fnReg = False
                Exit Function
            End If

            If mobjDAF.TransCvtCmtInfo(it81, iRegType81, it82, iRegType82, txtCmtCd.Text, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            fnReg = False
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Function

    Private Sub btnSelCmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelCmt.Click
        Dim sFn As String = "btnSelCmt_Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnSelCmt) + Me.btnSelCmt.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnSelCmt)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_cmtcont_slip("", Me.txtCmtCd.Text)

            objHelp.FormText = "소견코드 정보"

            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("cmtcd", "소견코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("cmtcont", "소견내용", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("slipnmd", "검사분류", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                miSelectKey = 1

                Me.txtCmtCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.txtCmtCont.Text = alList.Item(0).ToString.Split("|"c)(1)
                Me.txtSlipNmd.Text = alList.Item(0).ToString.Split("|"c)(2)

                miSelectKey = 0
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
            miSelectKey = 0
        End Try
    End Sub

    Private Sub spdCvtTest_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdCvtTest.ButtonClicked
        Dim sFn As String = "spdCalTest_ButtonClicked"

        If e.row < 1 Then Return
        If e.col <> Me.spdCvtTest.GetColFromID("hlp") Then Return

        If Len(Me.txtCmtCd.Text.Trim) < 1 Then Return
        If Len(Me.txtCmtCont.Text.Trim) < 1 Then Return

        Dim iTop As Integer = miMouseY
        Dim iLeft As Integer = miMouseX

        Try
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim sSlipCd As String = Ctrl.Get_Code(txtSlipNmd.Text)
            If sSlipCd = "00" Then sSlipCd = ""

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_list(sSlipCd, "")
            Dim a_dr As DataRow() = dt.Select("(tcdgbn IN ('P', 'B') AND titleyn = '0' OR tcdgbn IN ('S', 'C'))")
            dt = Fn.ChangeToDataTable(a_dr)

            objHelp.FormText = "검사정보"

            objHelp.MaxRows = 15
            objHelp.OnRowReturnYN = True

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "", 0, , , True)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                With Me.spdCvtTest
                    .SetText(.GetColFromID("testcd"), e.row, alList.Item(0).ToString.Split("|"c)(0))
                    .SetText(.GetColFromID("spccd"), e.row, alList.Item(0).ToString.Split("|"c)(1))
                    .SetText(.GetColFromID("tnmd"), e.row, alList.Item(0).ToString.Split("|"c)(2))
                    .SetText(.GetColFromID("spcnmd"), e.row, alList.Item(0).ToString.Split("|"c)(3))
                End With
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
            miSelectKey = 0

        End Try
    End Sub

    Private Sub spdCvtTest_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdCvtTest.Change
        If e.row < 1 Then Exit Sub

        Dim sTestCd As String = "", sSpcCd As String = "", sTnmd$ = "", sSpcNmd$ = ""
        Dim dt As New DataTable

        With spdCvtTest
            .Row = e.row
            If e.col = .GetColFromID("testcd") Then
                .Col = .GetColFromID("testcd") : sTestCd = .Text

                dt = LISAPP.COMM.CdFn.fnGet_testspc_BatteryParentSingle("", sTestCd)
                Dim dr As DataRow()

                dr = dt.Select("(tcdgbn in ('S', 'C') OR (tcdgbn in ('P', 'B') AND titleyn = '0'))", "")

                .Col = .GetColFromID("tnmd") : .Text = ""

                If dr.Length > 0 Then
                    .Col = .GetColFromID("tnmd") : .Text = dr(0).Item("tnmd").ToString()
                End If
            ElseIf e.col = .GetColFromID("spccd") Then
                .Col = .GetColFromID("testcd") : sTestCd = .Text
                .Col = .GetColFromID("spccd") : sSpcCd = .Text
                .Col = .GetColFromID("spcnmd") : .Text = ""

                dt = LISAPP.COMM.CdFn.fnGet_testspc_BatteryParentSingle("", sTestCd, sSpcCd)
                Dim dr As DataRow()

                dr = dt.Select("(tcdgbn in ('S', 'C') OR (tcdgbn in ('P', 'B') AND titleyn = '0'))", "")

                If dr.Length > 0 Then
                    .Col = .GetColFromID("tnmd") : .Text = dr(0).Item("tnmd").ToString()
                    .Col = .GetColFromID("spcnmd") : .Text = dr(0).Item("spcnmd").ToString()
                End If
            End If
        End With
    End Sub

    Private Sub txtCmtCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub txtCmtPCd_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCmtCd.LostFocus

        If Me.txtCmtCd.Text = "" Then Return

        btnSelCmt_Click(Nothing, Nothing)

    End Sub

    Private Sub spdCvtTest_MouseDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseDownEvent) Handles spdCvtTest.MouseDownEvent
        miMouseX = Ctrl.FindControlLeft(Me.spdCvtTest) + e.x
        miMouseY = Ctrl.FindControlTop(Me.spdCvtTest) + e.y
    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click"

        If Me.txtCmtCd.Text = "" Then Return

        Try

            Dim sMsg As String = "   소견코드 : " + Me.txtCmtCd.Text + vbCrLf
            sMsg += "   의 자동소견 변환을 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransCvtCmtInfo_UE(Me.txtCmtCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 소견에 자동소견 변환 정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

                sbInitialize()
                CType(Me.Owner, FGF01).sbDeleteCdList()
            Else
                MsgBox("사용종료에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub FDF44_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FDF44_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
    End Sub
End Class