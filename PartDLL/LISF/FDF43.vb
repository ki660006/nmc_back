'>>> [43] 결과값 자동변환
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF43
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF43.vb, Class : FDF43" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_CVT_RST

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboTSectNmD As System.Windows.Forms.ComboBox
    Friend WithEvents txtTSectCd As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxRow As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMaxCol As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdSpc As AxFPSpreadADO.AxfpSpread

    Public Sub sbDisplayCdDetail(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            Call sbDisplayCdDetail_Calc(rsTestCd, rsSpcCd, rsRstCd)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Calc(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Calc_RST(String, String, String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control = Nothing
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetCvtRstInfo(rsTestCd, rsSpcCd, rsRstCd)
            Else
                dt = mobjDAF.GetCvtRstInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsTestCd, rsSpcCd, rsRstCd)
            End If

            '초기화
            sbInitialize()

            If dt.Rows.Count < 1 Then Return

            miSelectKey = 1

            With dt
                Me.txtTestCd.Text = .Rows(0).Item("testcd").ToString()
                Me.txtSpcCd.Text = .Rows(0).Item("spccd").ToString()
                Me.txtTNmD.Text = .Rows(0).Item("tnmd").ToString()
                Me.txtSpcNmD.Text = .Rows(0).Item("spcnmd").ToString()
                Me.txtRstCd.Text = .Rows(0).Item("rstcdseq").ToString()
                Me.txtRstCont.Text = .Rows(0).Item("rstcont").ToString

                Me.rdoFldGbnR.Checked = CType(IIf(.Rows(0).Item("cvtfldgbn").ToString = "R", True, False), Boolean)
                Me.rdoFldGbnC.Checked = CType(IIf(.Rows(0).Item("cvtfldgbn").ToString = "C", True, False), Boolean)
                
                Me.rdoCvtBcNo.Checked = CType(IIf(.Rows(0).Item("cvtrange").ToString = "B", True, False), Boolean)
                Me.rdoCvtRegNo.Checked = CType(IIf(.Rows(0).Item("cvtrange").ToString = "R", True, False), Boolean)

                Me.rdoCvtTypeM.Checked = CType(IIf(.Rows(0).Item("cvttype").ToString = "M", True, False), Boolean)
                Me.rdoCvtTypeA.Checked = CType(IIf(.Rows(0).Item("cvttype").ToString = "A", True, False), Boolean)

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
                            If dt.Rows(intIdx).Item("refls").ToString = "" Or dt.Rows(intIdx).Item("refl").ToString.Trim = "" Then
                                .Col = .GetColFromID("refls") : .TypeComboBoxCurSel = -1
                            Else
                                .Col = .GetColFromID("refls") : .TypeComboBoxCurSel = Convert.ToInt16(dt.Rows(intIdx).Item("refls").ToString.Trim)
                            End If
                            .Col = .GetColFromID("testcd") : .Text = dt.Rows(intIdx).Item("ctestcd").ToString.Trim
                            .Col = .GetColFromID("spccd") : .Text = dt.Rows(intIdx).Item("cspccd").ToString.Trim
                            .Col = .GetColFromID("tnmd") : .Text = dt.Rows(intIdx).Item("ctnmd").ToString.Trim
                            .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(intIdx).Item("cspcnmd").ToString.Trim

                            .Col = .GetColFromID("refh") : .Text = dt.Rows(intIdx).Item("refh").ToString.Trim
                            If dt.Rows(intIdx).Item("refhs").ToString.Trim = "" Or dt.Rows(intIdx).Item("refh").ToString.Trim = "" Then
                                .Col = .GetColFromID("refhs") : .TypeComboBoxCurSel = -1
                            Else
                                .Col = .GetColFromID("refhs") : .TypeComboBoxCurSel = Convert.ToInt16(dt.Rows(intIdx).Item("refhs").ToString.Trim)
                            End If

                            .Col = .GetColFromID("reflt") : .Text = dt.Rows(intIdx).Item("reflt").ToString
                            If dt.Rows(intIdx).Item("reflts").ToString.Trim = "" Or dt.Rows(intIdx).Item("reflt").ToString.Trim = "" Then
                                .Col = .GetColFromID("reflts") : .TypeComboBoxCurSel = -1
                            Else
                                .Col = .GetColFromID("reflts") : .TypeComboBoxCurSel = Convert.ToInt16(dt.Rows(intIdx).Item("reflts").ToString.Trim)
                            End If

                            Exit For
                        End If
                    Next
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

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then
                Me.btnUE.Enabled = True
            Else
                Me.btnUE.Enabled = False
            End If

            Me.txtSpcCd.MaxLength = PRG_CONST.Len_SpcCd

            With Me.spdCvtTest
                .Row = -1 : .Col = .GetColFromID("spccd") : .TypeEditLen = PRG_CONST.Len_SpcCd
            End With

            miSelectKey = 1

            sbInitialize_ErrProvider()
            sbInitialize_Control()

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
                Me.txtTestCd.Text = "" : Me.txtSpcCd.Text = "" : Me.txtTNmD.Text = "" : Me.txtSpcNmD.Text = "" : Me.btnUE.Visible = False
                Me.txtRstCd.Text = "" : Me.txtRstCont.Text = ""

                Me.rdoCvtBcNo.Checked = True
                Me.rdoCvtTypeM.Checked = True
                Me.rdoFldGbnR.Checked = True

                With Me.spdCvtTest
                    .ClearRange(2, 1, .MaxCols, .MaxRows, True)
                End With

                Me.txtCvtForm.Text = ""
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtRegNm.Text = ""
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Len(Me.txtTestCd.Text.Trim) < 1 Then
                MsgBox("검사코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtSpcCd.Text.Trim) < 1 Then
                MsgBox("검체코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtRstCd.Text.Trim) < 1 Then
                MsgBox("결과코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtCvtForm.Text.Trim) < 1 Then
                MsgBox("계산식을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            Dim iCnt As Integer = 0
            Dim sCvtForm As String = Me.txtCvtForm.Text.Trim

            With Me.spdCvtTest
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
                            iCnt += 1
                            sCvtForm = sCvtForm.Replace(sGbn, "").Trim
                        End If
                    End If
                Next

                sCvtForm = sCvtForm.Replace("$$", "").Trim()
                sCvtForm = sCvtForm.Replace("||", "").Trim()
                sCvtForm = sCvtForm.Replace("(", "").Trim()
                sCvtForm = sCvtForm.Replace(")", "").Trim()

                If sCvtForm.Trim = "" And iCnt > 0 Then
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


    Public Function fnReg() As Boolean
        Dim sFn As String = ""

        Try
            Dim it83 As New LISAPP.ItemTableCollection
            Dim it84 As New LISAPP.ItemTableCollection
            Dim iRegType83 As Integer = 0, iRegType84 As Integer = 0
            Dim sRegDT As String

            iRegType83 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType84 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it83 = fnCollectItemTable_83(sRegDT)
            If it83.ItemTables.Count < 1 Then
                fnReg = False
                Exit Function
            End If

            it84 = fnCollectItemTable_84(sRegDT)
            If it84.ItemTables.Count < 1 Then
                fnReg = False
                Exit Function
            End If

            If mobjDAF.TransCvtRstInfo(it83, iRegType83, it84, iRegType84, _
                                        txtTestCd.Text, txtSpcCd.Text, txtRstCd.Text, USER_INFO.USRID) Then
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

    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim dt As DataTable = mobjDAF.GetNewRegDT

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                MsgBox("시스템의 날짜를 초기화하지 못했습니다. 관리자에게 문의하시기 바랍니다!!", MsgBoxStyle.Information)
                Return Format(Now, "yyyyMMddHHmmss")
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")
        End Try
    End Function

    Private Function fnCollectItemTable_83(ByVal asRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_83(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it83 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCvtTest
            Dim strCalForm$ = ""

            With it83
                .SetItemTable("TESTCD", 1, 1, Me.txtTestCd.Text.Trim)
                .SetItemTable("SPCCD", 2, 1, Me.txtSpcCd.Text.Trim)
                .SetItemTable("RSTCDSEQ", 3, 1, Me.txtRstCd.Text.Trim)
                .SetItemTable("CVTFLDGBN", 4, 1, IIf(Me.rdoFldGbnR.Checked, "R", "C").ToString)
                .SetItemTable("CVTRANGE", 5, 1, IIf(Me.rdoCvtBcNo.Checked, "B", "R").ToString)
                .SetItemTable("CVTTYPE", 6, 1, IIf(Me.rdoCvtTypeA.Checked, "A", "M").ToString)
                .SetItemTable("CVTVIEW", 7, 1, "M")
                .SetItemTable("CVTFORM", 8, 1, Me.txtCvtForm.Text.Trim)
                .SetItemTable("REGDT", 9, 1, asRegDT)
                .SetItemTable("REGID", 10, 1, USER_INFO.USRID)
                .SetItemTable("REGIP", 11, 1, USER_INFO.LOCALIP)
            End With

            fnCollectItemTable_83 = it83

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing

        End Try
    End Function

    Private Function fnCollectItemTable_84(ByVal asRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_83(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it84 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCvtTest

            With spdCvtTest
                For iRow As Integer = 1 To .MaxRows
                    Dim sGbn$ = "", sRefGbn$ = "", sRefLs$ = "", sRefL$ = "", sRefHs$ = "", sRefH$ = ""
                    Dim sRefLt$ = "", sRefLts$ = "", sTestCd$ = "", sSpcCd$ = ""

                    .Row = iRow
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
                        it84.SetItemTable("TESTCD", 1, iRow, Me.txtTestCd.Text.Trim)
                        it84.SetItemTable("SPCCD", 2, iRow, Me.txtSpcCd.Text.Trim)
                        it84.SetItemTable("RSTCDSEQ", 3, iRow, Me.txtRstCd.Text.Trim)
                        it84.SetItemTable("CTESTCD", 4, iRow, sTestCd)
                        it84.SetItemTable("CSPCCD", 5, iRow, sSpcCd)
                        it84.SetItemTable("CVTPARAM", 6, iRow, sGbn)
                        it84.SetItemTable("REFLGBN", 7, iRow, sRefGbn)
                        it84.SetItemTable("REFLS", 8, iRow, sRefLs)
                        it84.SetItemTable("REFL", 9, iRow, sRefL)
                        it84.SetItemTable("REFHGBN", 10, iRow, sRefGbn)
                        it84.SetItemTable("REFHS", 11, iRow, sRefHs)
                        it84.SetItemTable("REFH", 12, iRow, sRefH)
                        it84.SetItemTable("REFLTS", 13, iRow, sRefLts)
                        it84.SetItemTable("REFLT", 14, iRow, sRefLt)
                    End If
                Next
            End With

            fnCollectItemTable_84 = it84

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing

        End Try
    End Function

    Private Sub btnCdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp.Click
        Dim sFn As String = "Handles btnCdHelp.Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnCdHelp) + Me.btnCdHelp.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnCdHelp)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = mobjDAF.fnGet_testspc_autorst(Me.txtTestCd.Text, Me.txtSpcCd.Text)

            objHelp.FormText = "검사정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "", 0, , , True)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                miSelectKey = 1

                Me.txtTestCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.txtSpcCd.Text = alList.Item(0).ToString.Split("|"c)(1)
                Me.txtTNmD.Text = alList.Item(0).ToString.Split("|"c)(2)
                Me.txtSpcNmD.Text = alList.Item(0).ToString.Split("|"c)(3)

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

    Private Sub txtTestCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestCd.KeyDown, txtSpcCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        If CType(sender, Windows.Forms.TextBox).Text = "" Then Return

        If miSelectKey = 1 Then Return
        btnCdHelp_Click(Nothing, Nothing)

    End Sub

    Private Sub spdCvtTest_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdCvtTest.ButtonClicked
        Dim sFn As String = "spdCalTest_ButtonClicked"

        If e.row < 1 Then Return
        If e.col <> Me.spdCvtTest.GetColFromID("hlp") Then Return

        If Len(Me.txtTestCd.Text.Trim) < 1 Or Len(Me.txtTNmD.Text.Trim) < 1 Then Return
        If Len(Me.txtSpcCd.Text.Trim) < 1 Or Len(Me.txtSpcNmD.Text.Trim) < 1 Then Return
        If Len(Me.txtRstCd.Text.Trim) < 1 Or Len(Me.txtRstCont.Text.Trim) < 1 Then Return

        Dim iTop As Integer = miMouseY
        Dim iLeft As Integer = miMouseX

        Try
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_testspc_list("", "", "", IIf(Me.rdoCvtBcNo.Checked, Me.txtSpcCd.Text, "").ToString)
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

    Private Sub rdoItemRst_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoFldGbnR.CheckedChanged, rdoFldGbnC.CheckedChanged
        If rdoFldGbnC.Checked Then
            rdoCvtTypeA.Checked = False
            rdoCvtTypeA.Checked = True
            rdoCvtTypeA.Enabled = False
            rdoCvtTypeM.Enabled = False
        Else
            rdoCvtTypeA.Enabled = True
            rdoCvtTypeM.Enabled = True
        End If
    End Sub

    Private Sub btnCdHelp_rst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_rst.Click
        Dim sFn As String = "btnSelRst_Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnCdHelp) + Me.btnCdHelp.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnCdHelp)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TestRst_list(Me.txtTestCd.Text)

            If Me.txtRstCd.Text <> "" Then
                Dim a_dr As DataRow() = dt.Select("rstcdseq = '" + Me.txtRstCd.Text + "'", "")
                dt = Fn.ChangeToDataTable(a_dr)
            End If

            objHelp.FormText = "결과코드 정보"
            objHelp.MaxRows = 15

            objHelp.AddField("rstcdseq", "결과코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("keypad", "단축키", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("rstcont", "결과내용", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                miSelectKey = 1

                Me.txtRstCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.txtRstCont.Text = alList.Item(0).ToString.Split("|"c)(2)

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

    Private Sub spdCvtTest_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdCvtTest.Change
        If e.row < 1 Then Exit Sub

        Dim sTestCd As String = "", sSpcCd As String = "", sTnmd$ = "", sSpcNmd$ = ""
        Dim dt As New DataTable

        With spdCvtTest
            .Row = e.row
            If e.col = .GetColFromID("testcd") Then
                .Col = .GetColFromID("testcd") : sTestCd = .Text

                dt = LISAPP.COMM.cdfn.fnGet_testspc_BatteryParentSingle("", sTestCd)
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

                dt = LISAPP.COMM.cdfn.fnGet_testspc_BatteryParentSingle("", sTestCd, sSpcCd)
                Dim dr As DataRow()

                dr = dt.Select("(tcdgbn in ('S', 'C') OR (tcdgbn in ('P', 'B') AND titleyn = '0'))", "")

                If dr.Length > 0 Then
                    .Col = .GetColFromID("tnmd") : .Text = dr(0).Item("tnmd").ToString()
                    .Col = .GetColFromID("spcnmd") : .Text = dr(0).Item("spcnmd").ToString()
                End If
            End If

        End With
    End Sub

    Private Sub spdCvtTest_MouseDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseDownEvent) Handles spdCvtTest.MouseDownEvent
        miMouseX = Ctrl.FindControlLeft(Me.spdCvtTest) + e.x
        miMouseY = Ctrl.FindControlTop(Me.spdCvtTest) + e.y
    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click"

        If Me.txtTestCd.Text = "" Or Me.txtSpcCd.Text = "" Or Me.txtRstCd.Text = "" Then Return

        Try

            Dim sMsg As String = "   검사코드 : " + Me.txtTestCd.Text + vbCrLf
            sMsg += "   검체코드 : " & Me.txtSpcCd.Text + vbCrLf
            sMsg += "   결과코드 : " & Me.txtRstCd.Text & vbCrLf + vbCrLf
            sMsg += "   의 자동결과 변환을 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransCvtRstInfo_UE(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtRstCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 검사코드의 자동결과 변환 정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub FDF43_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FDF43_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
    End Sub

    Private Sub txtRstCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRstCd.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        If txtRstCd.Text = "" Then Return

        If miSelectKey = 1 Then Return
        btnCdHelp_rst_Click(Nothing, Nothing)
    End Sub


End Class