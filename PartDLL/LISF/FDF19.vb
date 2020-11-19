'>>> [19] 균 결과 코드

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class FDF19
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF47.vb, Class : FDF47" + vbTab
    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1

    Private mobjDAF As New LISAPP.APP_F_BAC_RST

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Private Function fnCollectItemTable_221(ByVal rsRegDt As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_221(String) As LISAPP.ItemTableCollection"

        Try
            Dim it As New LISAPP.ItemTableCollection
            Dim iCnt As Integer = 1

            If Me.txtSpcNmd.Text <> "" Then iCnt = Me.txtSpcNmd.Tag.ToString.Split("|"c).Length

            With it
                For ix As Integer = 1 To iCnt
                    .SetItemTable("TESTCD", 1, ix, Me.txtTestCd.Text)

                    If Me.chkSpcGbn.Checked And Me.txtSpcNmd.Text <> "" Then
                        .SetItemTable("SPCCD", 2, ix, Me.txtSpcNmd.Tag.ToString.Split("|"c)(ix - 1))
                    Else
                        .SetItemTable("SPCCD", 2, ix, txtSpcCd.Text)
                    End If

                    .SetItemTable("INCRSTCD", 3, ix, Me.txtIncCd.Text)
                    .SetItemTable("INCRSTNM", 4, ix, Me.txtIncNm.Text)
                    .SetItemTable("REGDT", 5, ix, rsRegDt)
                    .SetItemTable("REGID", 6, ix, USER_INFO.USRID)
                    .SetItemTable("REGIP", 7, ix, USER_INFO.LOCALIP)
                Next

            End With

            Return it

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it221 As New LISAPP.ItemTableCollection
            Dim iRegType221 As Integer = 0
            Dim sRegDT As String = ""

            iRegType221 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it221 = fnCollectItemTable_221(sRegDT)

            If mobjDAF.TransBacRstInfo(it221, iRegType221, Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtIncCd.Text, USER_INFO.USRID) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn$ = "Private Function fnGetSystemDT() As String"

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetNewRegDT

            If DTable.Rows.Count > 0 Then
                Return DTable.Rows(0).Item(0).ToString
            Else
                MsgBox("시스템의 날짜를 초기화하지 못했습니다. 관리자에게 문의하시기 바랍니다!!", MsgBoxStyle.Information)

                Return Format(Now, "yyyyMMddHHmmss")
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")

        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Me.txtIncCd.Text = "" Then
                MsgBox(Me.lblIncCd.Text + "을(를) (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtIncNm.Text.Trim) < 1 Then
                MsgBox(Me.lblIncNm.Text + "을(를) (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsIncCd As String, ByVal rsModid As String, ByVal rsModdt As String)
        Dim sFn As String = "sbDisplayCdDetail(String string string)"

        Try
            miSelectKey = 1

            sbDisplayCdDetail_BacRst(rsTestCd, rsSpcCd, rsIncCd, rsModid, rsModdt)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdDetail_BacRst(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsIncRstCd As String, ByVal rsModID As String, ByVal rsModDT As String)
        Dim sFn As String = "sbDisplayCdDetail_BacRst(String)"
        Dim iCol% = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex% = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetBacRstInfo(rsTestCd, rsSpcCd, rsIncRstCd)
            Else
                dt = mobjDAF.GetBacRstInfo(gsModDT, gsModID, rsTestCd, rsSpcCd, rsIncRstCd)
            End If

            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            sbInitialize()

            Ctrl.FindChildControl(Me.Controls, mchildctrlcol)

            If dt.Rows.Count < 1 Then Return

            For i As Integer = 0 To dt.Rows.Count - 1
                For Each cctrl In mchildctrlcol
                    For j As Integer = 0 To dt.Columns.Count - 1
                        If cctrl.Tag.ToString.ToUpper = dt.Columns(j).ColumnName().ToUpper Then
                            mchildctrlcol.Remove(1)

                            If TypeOf (cctrl) Is System.Windows.Forms.ComboBox Then
                                If cctrl.Tag.ToString.EndsWith("_01") = True Then
                                    iCurIndex = -1

                                    For k As Integer = 0 To CType(cctrl, System.Windows.Forms.ComboBox).Items.Count - 1
                                        If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.EndsWith(dt.Rows(i).Item(j).ToString) = True Then
                                            iCurIndex = k

                                            Exit For
                                        End If

                                        If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.StartsWith(dt.Rows(i).Item(j).ToString) = True Then
                                            iCurIndex = k

                                            Exit For
                                        End If
                                    Next

                                    CType(cctrl, Windows.Forms.ComboBox).SelectedIndex = iCurIndex
                                End If

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.TextBox Then
                                cctrl.Text = dt.Rows(i).Item(j).ToString.Trim

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.CheckBox Then
                                CType(cctrl, System.Windows.Forms.CheckBox).Checked = CType(IIf(dt.Rows(i).Item(j).ToString = "1", True, False), Boolean)

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.RadioButton Then
                                CType(cctrl, System.Windows.Forms.RadioButton).Checked = CType(IIf(dt.Rows(i).Item(j).ToString = "1", True, False), Boolean)

                            End If

                            Exit For
                        End If
                    Next
                Next
            Next
            
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then      '권한이 있어야 "사용종료"를 할 수 있음
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

            sbInitialize_ErrProvider()

            sbInitialize_Control()

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn$ = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbInitialize_Control(Optional ByVal iMode% = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode% = 0)"

        Try
            If iMode = 0 Then
                Me.txtTestCd.Text = "" : Me.txtTNmD.Text = ""
                Me.txtSpcCd.Text = "" : Me.txtSpcCd.MaxLength = PRG_CONST.Len_SpcCd : Me.txtSpcNmd.Text = ""

                Me.txtIncCd.Text = "" : Me.btnUE.Visible = False
                Me.txtIncNm.Text = ""

                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtModNm.Text = "" : Me.txtRegNm.Text = ""
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_CtrlCollection()
        mchildctrlcol = Nothing
        mchildctrlcol = New Collection
    End Sub

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click(Object, System.EventArgs) Handles btnUE.Click"

        If Me.txtIncCd.Text = "" Or Me.txtTNmD.Text = "" Then Return

        Try
            Dim sMsg As String = ""
            sMsg += Me.lblIncCd.Text + " : " + Me.txtIncCd.Text & vbCrLf
            sMsg += "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransBacRstInfo_UE(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtIncCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 " + Me.tbcTpg.Text + "가 사용종료 되었습니다!!", MsgBoxStyle.Information)

                sbInitialize()
                CType(Me.Owner, FGF01).sbDeleteCdList()
            Else
                MsgBox("사용종료에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub txtIncNm_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIncNm.GotFocus
        txtIncNm.SelectAll()
    End Sub

    Private Sub FDF19_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtTestCd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestCd.GotFocus

        With CType(sender, Windows.Forms.TextBox)
            .SelectionStart = 0
            .SelectAll()
        End With

    End Sub

    Private Sub txtTestCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestCd.KeyDown

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return
        If (Me.txtTestCd.Text + Me.txtSpcCd.Text).Length < 1 Then Return

        btnCdHelp_test_Click(Nothing, Nothing)

    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Dim sFn As String = " Handles btnCdHelp_test.Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnCdHelp_test) + Me.btnCdHelp_test.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_testspc_list_m(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.chkSpcGbn.Checked)

            objHelp.FormText = "검사정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("TESTCD", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("SPCCD", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("TNMD", "검사명", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("SPCNMD", "검체명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            aryList = objHelp.Display_Result(Me, iLeft, iTop, dt)
            If aryList.Count > 0 Then
                miSelectKey = 1

                Me.txtTestCd.Text = aryList.Item(0).ToString.Split("|"c)(0)
                Me.txtTNmD.Text = aryList.Item(0).ToString.Split("|"c)(2)

                If Me.chkSpcGbn.Checked = False Then
                    Me.txtSpcCd.Text = aryList.Item(0).ToString.Split("|"c)(1)
                    Me.txtSpcNmd.Text = aryList.Item(0).ToString.Split("|"c)(3)
                End If

                miSelectKey = 0
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
            miSelectKey = 0

        End Try
    End Sub

    Private Sub chkSpcGbn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSpcGbn.CheckedChanged

        If chkSpcGbn.Checked Then
            Me.txtSpcCd.ReadOnly = False
            Me.txtSpcCd.Text = "" : Me.txtSpcNmd.Text = "" : Me.txtSpcNmd.Tag = ""

            Me.btnClear_spc.Visible = True
        Else
            Me.txtSpcCd.ReadOnly = True
            Me.txtSpcCd.Text = "" : Me.txtSpcNmd.Text = "" : Me.txtSpcNmd.Tag = ""

            Me.btnClear_spc.Visible = False
        End If

    End Sub

    Private Sub btnClear_spc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_spc.Click
        Me.txtSpcCd.Text = ""
        Me.txtSpcNmd.Text = "" : Me.txtSpcNmd.Tag = ""
    End Sub

    Private Sub btnCdHelp_spc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_spc.Click
        Dim sFn As String = " Handles btnCdHelp_spc.Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnCdHelp_test) + Me.btnCdHelp_test.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_spc_list_m(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.chkSpcGbn.Checked)

            objHelp.FormText = "검체정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            If chkSpcGbn.Checked Then
                objHelp.AddField("chk", "선택", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, "CHECKBOX")
            End If
            objHelp.AddField("spccd", "검체코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("SPCNMD", "검체명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            aryList = objHelp.Display_Result(Me, iLeft, iTop, dt)
            If aryList.Count > 0 Then
                miSelectKey = 1

                Me.txtSpcNmd.Text = ""
                Me.txtSpcNmd.Tag = ""

                If chkSpcGbn.Checked Then
                    For ix As Integer = 0 To aryList.Count - 1
                        Me.txtSpcNmd.Text += IIf(ix = 0, "", ",").ToString + aryList.Item(ix).ToString.Split("|"c)(1)
                        Me.txtSpcNmd.Tag = Me.txtSpcNmd.Tag.ToString + IIf(ix = 0, "", "|").ToString + aryList.Item(ix).ToString.Split("|"c)(0)
                    Next
                Else
                    Me.txtSpcCd.Text = aryList.Item(0).ToString.Split("|"c)(0)
                    Me.txtSpcNmd.Text = aryList.Item(0).ToString.Split("|"c)(1)
                    Me.txtSpcNmd.Tag = ""
                End If

                miSelectKey = 0
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
            miSelectKey = 0

        End Try
    End Sub
End Class