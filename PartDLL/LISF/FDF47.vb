'>>> [47] 기타코드
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF47
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF47.vb, Class : FDF47" + vbTab
    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1

    Private mobjDAF As New LISAPP.APP_F_COLLTKCD

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Private Function fnCollectItemTable_410(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_170(String) As LISAPP.ItemTableCollection"

        Try
            Dim it As New LISAPP.ItemTableCollection

            With it

                Dim sCmtGbnCd As String = Ctrl.Get_Code(Me.cboCmtGbn)

                .SetItemTable("CMTGBN", 1, 1, sCmtGbnCd)
                .SetItemTable("CMTCD", 2, 1, Me.txtCmtCd.Text)
                .SetItemTable("REGDT", 3, 1, rsRegDT)
                .SetItemTable("REGID", 4, 1, USER_INFO.USRID)
                .SetItemTable("CMTCONT", 5, 1, Me.txtCmtCont.Text)
                .SetItemTable("DELFLG", 6, 1, IIf(Me.chkDelflg.Checked, "1", "0").ToString)
                .SetItemTable("REGIP", 7, 1, USER_INFO.LOCALIP)
            End With

            Return it

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it410 As New LISAPP.ItemTableCollection
            Dim iRegType410 As Integer = 0
            Dim sRegDT As String = ""

            iRegType410 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it410 = fnCollectItemTable_410(sRegDT)

            If mobjDAF.TransCollTkCdInfo(it410, iRegType410, Ctrl.Get_Code(Me.cboCmtGbn), Me.txtCmtCd.Text, USER_INFO.USRID) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Function fnFindConflict(ByVal rsCmtGbnCd As String, ByVal rsRtnCd As String) As String
        Dim sFn As String = "fnFindConflict(String) As String"

        Try
            Dim dt As DataTable = mobjDAF.GetRecentCollTkCdInfo(rsCmtGbnCd, rsRtnCd)

            If dt.Rows.Count > 0 Then
                Return "동일 " + Me.tbcTpg.Text + "가 존재합니다." + vbCrLf + vbCrLf + _
                       "코드를 재조정 하십시요!!"
            Else
                Return ""
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return "Error"
        End Try
    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn$ = "Private Function fnGetSystemDT() As String"

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

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Me.cboCmtGbn.SelectedIndex < 0 Then
                MsgBox(Me.lblCmtGbn.Text + "을(를) 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtCmtCd.Text.Trim) < Me.txtCmtCd.MaxLength Then
                MsgBox(Me.lblCmtCd.Text + "을(를) (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtCmtCont.Text.Trim) < 1 Then
                MsgBox(Me.lblCmtCont.Text + "을(를) (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Ctrl.Get_Code(Me.cboCmtGbn), Me.txtCmtCd.Text)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsGbnCd As String, ByVal rsCd As String)
        Dim sFn As String = "sbDisplayCdDetail(String, String)"

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                sbDisplayCdList_Ref()
            End If

            sbDisplayCdDetail_AbnRstCd(rsGbnCd, rsCd)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdDetail_AbnRstCd(ByVal rsGbnCd As String, ByVal rsCd As String)
        Dim sFn As String = "sbDisplayCdDetail_RtnCd(String)"
        Dim iCol% = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex% = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetCollTkCdInfo(rsGbnCd, rsCd)
            Else
                dt = mobjDAF.GetCollTkCdInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsGbnCd, rsCd)
            End If

            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()

            sbInitialize_CtrlCollection()

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
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_CmtGbn(ByVal actrl As Windows.Forms.ComboBox)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_CmtGbn(Windows.Forms.ComboBox)"

        Try

            sbInitialize()
            Dim dt As DataTable = mobjDAF.GetCmtGbnInfo("ETC")

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add("[" + dt.Rows(i).Item("cmtgbncd").ToString + "] " + dt.Rows(i).Item("cmtgbnnm").ToString)
                Next
            End With


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref()
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref()"

        Try
            miSelectKey = 1
            sbDisplayCdList_Ref_CmtGbn(Me.cboCmtGbn)

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
            If USER_INFO.USRLVL = "S" Then      '권한이 있어야 "사용종료"를 할 수 있음
                btnDel.Enabled = True
            Else
                btnDel.Enabled = False
            End If

            sbInitialize_ErrProvider()

            sbInitialize_Control()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn$ = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbInitialize_Control(Optional ByVal iMode% = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode% = 0)"

        Try
            If iMode = 0 Then
                Me.txtCmtCd.Text = "" : Me.btnDel.Visible = False
                Me.txtCmtCont.Text = ""

                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = ""
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_CtrlCollection()
        mchildctrlcol = Nothing

        mchildctrlcol = New Collection
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim sFn As String = "Private Sub btnDel_Click"

        If Me.txtCmtCd.Text = "" Then Return

        Try
            Dim sMsg As String = ""

            sMsg = ""
            sMsg += Me.lblCmtCd.Text + " : " + Me.txtCmtCd.Text + vbCrLf
            sMsg += Me.lblCmtCont.Text + " : " + Me.txtCmtCont.Text + vbCrLf + vbCrLf
            sMsg += "의 " + "코드를 삭제 하시겠습니까?" + vbCrLf + vbCrLf + vbCrLf
            sMsg += ">>> " + Me.btnDel.Text + "는 주의를 요하는 작업이므로 신중히 실행하시기 바랍니다!!" + vbTab + vbCrLf

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo, Me.btnDel.Text + " 확인") = MsgBoxResult.No Then Return

            Dim bReturn As Boolean = mobjDAF.TransCollTkCdInfo_DEL(Ctrl.Get_Code(cboCmtGbn), Me.txtCmtCd.Text, USER_INFO.USRID)

            If bReturn Then
                MsgBox("해당 코드가 삭제되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox("해당 코드 삭제에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub FDF47_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtCmtCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown, txtCmtCont.KeyDown, cboCmtGbn.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub
End Class