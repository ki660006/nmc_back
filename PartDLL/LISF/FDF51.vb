'>>> [51] Alert Rule
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF51
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF51.vb, Class : FDF51" + vbTab
    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1

    Private mobjDAF As New LISAPP.APP_F_ALERT_RULE

    Public gsModDT As String = ""
    Public gsModID As String = ""
    Public gsREGID As String = ""
    Public gsREGDT As String = ""

    Private Function fnCollectItemTable_180(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_180(String) As LISAPP.ItemTableCollection"

        Try
            Dim it As New LISAPP.ItemTableCollection

            With it

                Dim sTclsCd As String = Ctrl.Get_Code(Me.cboTestCd)

                .SetItemTable("TESTCD", 1, 1, sTclsCd)
                .SetItemTable("REGDT", 2, 1, rsRegDT)
                .SetItemTable("REGID", 3, 1, USER_INFO.USRID)
                .SetItemTable("REGIP", 4, 1, USER_INFO.LOCALIP)

                For intRow As Integer = 1 To spdItem.MaxRows
                    spdItem.Row = intRow
                    spdItem.Col = spdItem.GetColFromID("item") : Dim strItem As String = spdItem.CellTag
                    spdItem.Col = spdItem.GetColFromID("value") : Dim strValue As String = spdItem.Text

                    If strItem Is Nothing Then Exit For

                    If Not (strItem.ToLower = "sex" Or strItem.ToLower = "eqflag" Or strItem.ToLower = "antic" Or strItem.ToLower = "panic" Or strItem.ToLower = "delta") And _
                       strValue <> "" Then
                        If strValue.Substring(strValue.Length - 1) <> "," Then strValue += ","
                    End If

                    If strItem <> "" Then
                        .SetItemTable(strItem, 3 + intRow, 1, strValue)
                    End If
                Next

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
            Dim it180 As New LISAPP.ItemTableCollection
            Dim iRegType180 As Integer = 0
            Dim sRegDT As String = ""

            iRegType180 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it180 = fnCollectItemTable_180(sRegDT)

            If mobjDAF.TransAlertRuleInfo(it180, iRegType180, Ctrl.Get_Code(Me.cboTestCd), USER_INFO.USRID) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Function fnFindConflict(ByVal rsTclsCd As String) As String
        Dim sFn As String = "fnFindConflict(String) As String"

        Try
            Dim dt As DataTable = mobjDAF.GetRecentAlertRuleInfo(rsTclsCd)

            If dt.Rows.Count > 0 Then
                Return "동일 " + Me.lblTestCd.Text + "가 존재합니다." + vbCrLf + vbCrLf + _
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
            If Me.cboTestCd.SelectedIndex < 0 Then
                MsgBox(Me.lblTestCd.Text + "을(를) 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Ctrl.Get_Code(Me.cboTestCd))

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

    Public Sub sbDisplayCdDetail(ByVal rsCd As String)
        Dim sFn As String = "sbDisplayCdDetail(String, String)"

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                sbDisplayCdList()
            End If

            sbDisplayCdDetail_AlertRule(rsCd)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdDetail_AlertRule(ByVal rsTclsCd As String)
        Dim sFn As String = "sbDisplayCdDetail_AlertRule(String)"
        Dim iCol% = 0

        Try
            Dim dt As DataTable
            Dim iCurIndex% = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetAlertRuleInfo(rsTclsCd)
            Else
                dt = mobjDAF.GetAlertRuleInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsTclsCd)
            End If

            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            sbInitialize()

            sbInitialize_CtrlCollection()

            Ctrl.FindChildControl(Me.Controls, mchildctrlcol)

            If dt.Rows.Count < 1 Then Return

            miSelectKey = 1

            With dt
                For ix As Integer = 0 To cboTestCd.Items.Count - 1
                    If Ctrl.Get_Code(Me.cboTestCd.Items(ix).ToString.Trim) = Ctrl.Get_Code(.Rows(0).Item("tnmd_01").ToString.Trim) Then
                        cboTestCd.SelectedIndex = ix
                        Exit For
                    End If
                Next

                For intCol As Integer = 0 To .Columns.Count - 1
                    For intRow As Integer = 1 To spdItem.MaxRows
                        spdItem.Row = intRow
                        spdItem.Col = spdItem.GetColFromID("item") : Dim strItem As String = spdItem.CellTag

                        If strItem Is Nothing Then Exit For

                        If strItem.ToString.ToUpper = .Columns(intCol).ColumnName().ToUpper Then
                            spdItem.Row = intRow
                            spdItem.Col = spdItem.GetColFromID("value") : spdItem.Text = .Rows(0).Item(intCol).ToString.Trim
                            Exit For
                        End If
                    Next

                Next
                Me.txtModDT.Text = .Rows(0).Item("moddt").ToString()
                Me.txtModID.Text = .Rows(0).Item("modid").ToString()
                Me.txtRegDT.Text = .Rows(0).Item("regdt").ToString()
                Me.txtRegID.Text = .Rows(0).Item("regid").ToString()
                Me.txtModNm.Text = .Rows(0).Item("modnm").ToString()
                Me.txtRegNm.Text = .Rows(0).Item("regnm").ToString()
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_TclsCds(ByVal actrl As Windows.Forms.ComboBox)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_TclsCds(Windows.Forms.ComboBox)"

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetAlertRlue_Tcls()

            actrl.Items.Clear()

            If DTable.Rows.Count > 0 Then
                With actrl
                    For i As Integer = 0 To DTable.Rows.Count - 1
                        actrl.Items.Add(DTable.Rows(i).Item("tnmd_01").ToString)
                    Next
                End With
            Else
                Exit Sub
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList()
        Dim sFn As String = "Private Sub sbDisplayCdList()"

        Try
            miSelectKey = 1
            sbDisplayCdList_TclsCds(Me.cboTestCd)

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
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
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
                cboTestCd.Enabled = False : btnUE.Visible = False

                With spdItem
                    .Col = .GetColFromID("value") : .Col2 = .GetColFromID("value")
                    .Row = 1 : .Row2 = .MaxRows
                    .BlockMode = True
                    .Action = FPSpreadADO.ActionConstants.ActionClearText
                    .BlockMode = False
                End With

                txtRegDT.Text = "" : txtRegID.Text = ""
                txtModDT.Text = "" : txtModID.Text = "" : txtRegNm.Text = ""
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_CtrlCollection()
        mchildctrlcol = Nothing

        With spdItem
            .Col = .GetColFromID("value") : .Col2 = .GetColFromID("value")
            .Row = 1 : .Row2 = .MaxRows
            .BlockMode = True
            .Action = FPSpreadADO.ActionConstants.ActionClearText
            .BlockMode = False
        End With
        mchildctrlcol = New Collection
    End Sub

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click(Object, System.EventArgs) Handles btnUE.Click"

        If Me.cboTestCd.SelectedIndex < 0 Then Return

        Try

            Dim sMsg As String = ""
            sMsg += Me.cboTestCd.Text.Substring(Me.cboTestCd.Text.IndexOf("]") + 1).Trim + " : " + Ctrl.Get_Code(Me.cboTestCd) + vbCrLf
            sMsg += "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransAlertRuleInfo_UE(Ctrl.Get_Code(Me.cboTestCd), USER_INFO.USRID) Then
                MsgBox("해당 " + Me.tbcTpg.Text + "가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub cboTclsCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTestCd.SelectedIndexChanged

        sbDisplayCdDetail_AlertRule(Ctrl.Get_Code(cboTestCd))

    End Sub

    Private Sub FDF51_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FDF51_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblGuide1.Text = "※ 항균제결과 입력방법 : (#B = '균코드' && [항균제코드] = 'DEC 결과') || [항균제코드] = 'DEC 결과'"
        Me.lblGuide2.Text = "※ &&: and, ||: or"

    End Sub
End Class