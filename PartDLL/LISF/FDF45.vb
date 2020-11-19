'>>> [45] KEYPAD
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class FDF45
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF45.vb, Class : FDF45" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0
    Private miAddModeKey As Integer = 0

    Private mobjDAF As New LISAPP.APP_F_KEYPAD

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Private Sub sbDisplay_Items()

        Dim dt As DataTable = mobjDAF.GetTclsCdsInfo(Me.txtTestCd.Text, Me.txtSpcCd.Text)

        spdItem.MaxRows = dt.Rows.Count
        If dt.Rows.Count < 1 Then Exit Sub

        Dim strItems As String = "[       ] 없음" + Chr(9)

        With spdItem
            For intRow As Integer = 1 To dt.Rows.Count
                .Row = intRow
                .Col = .GetColFromID("testcd") : .Text = dt.Rows(intRow - 1).Item("testcd").ToString
                .Col = .GetColFromID("tnmd") : .Text = dt.Rows(intRow - 1).Item("tnmd").ToString

                strItems += "[" + dt.Rows(intRow - 1).Item("testcd").ToString + "] " + dt.Rows(intRow - 1).Item("tnmd").ToString + Chr(9)

            Next

            .Row = 1 : .Row2 = .MaxRows
            .Col = .GetColFromID("pertestcd") : .Col2 = .GetColFromID("pertestcd")
            .BlockMode = True
            .TypeComboBoxList = strItems
            .BlockMode = False


            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("pertestcd") : .TypeComboBoxCurSel = 0
            Next
        End With


    End Sub

    Public Sub sbDisplayCdDetail(ByVal rsTclsCd As String, ByVal rsSpcCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            Call sbDisplayCdDetail_KeyPad(rsTclsCd, rsSpcCd)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_KeyPad(ByVal rsTestCd As String, ByVal rsSpcCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Calc_RST(String, String,)"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control = Nothing
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetKeyPadInfo(rsTestCd, rsSpcCd)
            Else
                dt = mobjDAF.GetKeyPadInfo(gsModDT, gsModID, rsTestCd, rsSpcCd)
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

                Me.txtRegDT.Text = .Rows(0).Item("regdt").ToString()
                Me.txtRegID.Text = .Rows(0).Item("regid").ToString()
                Me.txtModNm.Text = .Rows(0).Item("modnm").ToString()
                Me.txtRegNm.Text = .Rows(0).Item("regnm").ToString()

                Me.cboFormGbn.SelectedIndex = CInt(.Rows(0).Item("formgbn").ToString)

                Dim blnFind As Boolean = False

                txtWbcTcd.Text = .Rows(0).Item("wbctestcd").ToString()
                txtWbcTnm.Text = .Rows(0).Item("wbctnmd").ToString()

                sbDisplay_Items()
            End With

            For intIdx As Integer = 0 To dt.Rows.Count - 1
                With Me.spdItem
                    For intRow As Integer = 1 To .MaxRows
                        .Row = intRow
                        .Col = .GetColFromID("testcd")
                        If .Text = dt.Rows(intIdx).Item("pertestcd").ToString Then
                            .Col = .GetColFromID("chk") : .Text = "1"

                            If dt.Rows(intIdx).Item("pertestcd").ToString <> "" Then
                                Dim blnFind As Boolean = False

                                .Col = .GetColFromID("pertestcd")
                                For intIx2 As Integer = 1 To .TypeComboBoxCount
                                    .TypeComboBoxCurSel = Convert.ToInt16(intIx2)
                                    If Ctrl.Get_Code(.Text) = dt.Rows(intIdx).Item("pertestcd").ToString Then
                                        blnFind = True
                                        Exit For
                                    End If
                                Next

                                If blnFind = False Then .TypeComboBoxCurSel = 0

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
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

            txtSpcCd.MaxLength = PRG_CONST.Len_SpcCd

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
                Me.spdItem.MaxRows = 0

                Me.cboFormGbn.SelectedIndex = 0

                Me.txtWbcTcd.Text = "" : Me.txtWbcTnm.Text = ""

                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtModNm.Text = "" : Me.txtRegNm.Text = ""
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

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            Dim intCnt As Integer = 0

            With spdItem
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text = "1" Then intCnt += 1
                Next
            End With

            If intCnt > 0 Then
            Else
                MsgBox(lblTitle.Text + "  선택하세요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function


    Public Function fnReg() As Boolean
        Dim sFn As String = ""

        Try
            Dim it42 As New LISAPP.ItemTableCollection
            Dim iRegType42 As Integer = 0
            Dim sRegDT As String

            iRegType42 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it42 = fnCollectItemTable_42(sRegDT)
            If it42.ItemTables.Count < 1 Then
                fnReg = False
                Exit Function
            End If

            If mobjDAF.TransKeyPadInfo(it42, iRegType42, Me.txtTestCd.Text, Me.txtSpcCd.Text, USER_INFO.USRID) Then
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

    Private Function fnCollectItemTable_42(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_83(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it42 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdItem
            Dim strCalForm$ = ""
            Dim intCnt As Integer = 0

            With it42

                For intRow As Integer = 1 To spd.MaxRows

                    spd.Row = intRow
                    spd.Col = spd.GetColFromID("chk")
                    If spd.Text = "1" Then
                        intCnt += 1

                        .SetItemTable("TESTCD", 1, intCnt, Me.txtTestCd.Text.Trim)
                        .SetItemTable("SPCCD", 2, intCnt, Me.txtSpcCd.Text.Trim)
                        .SetItemTable("FORMGBN", 3, intCnt, Me.cboFormGbn.SelectedIndex.ToString)
                        .SetItemTable("REGDT", 4, intCnt, rsRegDT)
                        .SetItemTable("REGID", 5, intCnt, USER_INFO.USRID)

                        spd.Col = spd.GetColFromID("testcd")
                        .SetItemTable("CNTTESTCD", 6, intCnt, spd.Text)

                        spd.Col = spd.GetColFromID("pertestcd")
                        .SetItemTable("PERTESTCD", 7, intCnt, Ctrl.Get_Code(spd.Text))

                        .SetItemTable("WBCTESTCD", 8, intCnt, Me.txtWbcTcd.Text)
                        .SetItemTable("REGIP", 9, intCnt, USER_INFO.LOCALIP)

                    End If
                Next
            End With

            fnCollectItemTable_42 = it42

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing

        End Try
    End Function


    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Dim sFn As String = "btnAddTest_Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnCdHelp_test) + Me.btnCdHelp_test.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnCdHelp_test)

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_testspc_list("", "", Me.txtTestCd.Text, Me.txtSpcCd.Text)
            Dim a_dr As DataRow() = dt.Select("tcdgbn = 'P'", "")
            dt = Fn.ChangeToDataTable(a_dr)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            objHelp.FormText = "검사정보"
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("slipcd", "", , , , True)

            aryList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If aryList.Count > 0 Then
                miSelectKey = 1

                Me.txtTestCd.Text = aryList.Item(0).ToString.Split("|"c)(0)
                Me.txtSpcCd.Text = aryList.Item(0).ToString.Split("|"c)(1)
                Me.txtTNmD.Text = aryList.Item(0).ToString.Split("|"c)(2)
                Me.txtSpcNmD.Text = aryList.Item(0).ToString.Split("|"c)(3)
                Me.txtSlipCd.Text = aryList.Item(0).ToString.Split("|"c)(4)

                sbDisplay_Items()
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

    Private Sub txtWbcTcd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWbcTcd.Validating

        Dim dt As DataTable = mobjDAF.GetTestCdInfo(Me.txtWbcTcd.Text, Me.txtSpcCd.Text)

        Me.txtWbcTnm.Text = ""

        If dt.Rows.Count > 0 Then
            Me.txtWbcTnm.Text = dt.Rows(0).Item("tnmd").ToString()
        End If

    End Sub

    Private Sub btnWbcHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWbcHelp.Click
        Dim sFn As String = "btnWbcHelp_Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnWbcHelp) + Me.btnWbcHelp.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnWbcHelp)

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_testspc_list(Me.txtSlipCd.Text, "", "", Me.txtSpcCd.Text)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "검사정보"
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                Me.txtWbcTcd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.txtWbcTnm.Text = alList.Item(0).ToString.Split("|"c)(1)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
            miSelectKey = 0

        End Try


    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click"

        If Me.txtTestCd.Text = "" Or Me.txtSpcCd.Text = "" Then Return

        Try

            Dim sMsg As String = "   검사코드 : " + Me.txtTestCd.Text + vbCrLf
            sMsg += "   검체코드 : " & Me.txtSpcCd.Text + vbCrLf
            sMsg += "   의 KeyPad 설정을 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransKeyPadInfo_UE(Me.txtTestCd.Text, Me.txtSpcCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 검사의 KeyPad 설정이 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub FDF45_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtTestCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestCd.KeyDown, txtSpcCd.KeyDown, txtWbcTcd.KeyDown, cboFormGbn.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        If CType(sender, Windows.Forms.TextBox).Name.ToUpper = "TXTTESTCD" Then
            Me.txtTNmD.Text = ""
            If Me.txtSpcCd.Text = "" Then Me.txtSpcNmD.Text = ""

            If Me.txtTestCd.Text = "" Then
                SendKeys.Send("{TAB}")
            Else
                btnCdHelp_test_Click(Nothing, Nothing)
            End If
        ElseIf CType(sender, Windows.Forms.TextBox).Name.ToUpper = "TXTSPCCD" Then
            Me.txtSpcNmD.Text = ""
            If Me.txtSpcCd.Text = "" Then
                SendKeys.Send("{TAB}")
            Else
                btnCdHelp_test_Click(Nothing, Nothing)
            End If
        Else
            SendKeys.Send("{TAB}")
        End If

    End Sub
End Class