'>>> [02] 부서/분야
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF02
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF02.vb, Class : FDF02" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_SLIP
    Friend WithEvents txtDispSeq As System.Windows.Forms.TextBox
    Friend WithEvents lblDispSeq As System.Windows.Forms.Label
    Friend WithEvents chkTake2Yn As System.Windows.Forms.CheckBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents lblPartGbn As System.Windows.Forms.Label
    Friend WithEvents cboPartGbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtTelNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Public giClearKey As Integer = 0

    Public Sub sbEditUseDt(ByVal rsUseTag As String)
        Dim sFn As String = "Public Sub sbEditUseDt"

        Try
            Dim fgf02 As New FGF03

            With fgf02
                .txtCd.Text = Me.txtPartCd.Text + Me.txtSlipCd.Text
                .txtNm.Text = Me.txtPartNm.Text + "/" + Me.txtSlipNm.Text

                .lblUseDt.Text = IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString
                .lblUseDtA.Text = IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString
                .btnEditUseDt.Text = .btnEditUseDt.Text.Replace("사용일시", IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString)
                .txtUseDt.Text = IIf(rsUseTag.ToUpper = "USDT", Me.txtUSDT.Text, Me.txtUEDT.Text).ToString

                .Owner = Me
                .StartPosition = Windows.Forms.FormStartPosition.CenterParent
                .ShowDialog()
            End With

            If IsDate(Me.AccessibleName) Then
                If CDate(Me.AccessibleName) = Date.MinValue Then
                    'Delete
                    sbEditUseDt_Del()
                Else
                    'Edit
                    sbEditUseDt_Edit(rsUseTag, Me.AccessibleName)
                End If

            Else
                Return

            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.AccessibleName = ""

        End Try
    End Sub

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            dt = mobjDAF.GetUsUeCd_Slip(Me.txtPartCd.Text, Me.txtSlipCd.Text, Me.txtUSDT.Text)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransSlipInfo_DEL(Me.txtPartCd.Text, Me.txtSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

            If bReturn Then
                MsgBox("해당 코드가 삭제되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox("해당 코드 삭제에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbEditUseDt_Edit(ByVal rsUseTag As String, ByVal rsUseDt As String)
        Dim sFn As String = "Sub sbEditUseDt_Edit"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            rsUseDt = rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", "")

            '> 사용중복 조사
            dt = mobjDAF.GetUsUeDupl_Slip(Me.txtPartCd.Text, Me.txtSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransSlipInfo_UPD_US(Me.txtPartCd.Text, Me.txtSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransSlipInfo_UPD_UE(Me.txtPartCd.Text, Me.txtSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
            End If

            If bReturn Then
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + "가 수정되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + " 수정에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Function fnCollectItemTable_20(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_20(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it20 As New LISAPP.ItemTableCollection

            With it20
                .SetItemTable("partcd", 1, 1, Me.txtPartCd.Text)
                .SetItemTable("usdt", 2, 1, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)

                If txtUEDT.Text = "" Then
                    .SetItemTable("uedt", 3, 1, msUEDT)
                Else
                    .SetItemTable("uedt", 3, 1, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                End If

                .SetItemTable("regdt", 4, 1, rsRegDT)
                .SetItemTable("regid", 5, 1, USER_INFO.USRID)
                .SetItemTable("partnm", 6, 1, Me.txtPartNm.Text)
                .SetItemTable("partnms", 7, 1, Me.txtPartNmS.Text)
                .SetItemTable("partnmd", 8, 1, Me.txtPartNmD.Text)
                .SetItemTable("partnmp", 9, 1, Me.txtPartNmP.Text)
                .SetItemTable("partgbn", 10, 1, Ctrl.Get_Code(Me.cboPartGbn))
                .SetItemTable("telno", 11, 1, Me.txtTelNo.Text)
                .SetItemTable("regip", 12, 1, USER_INFO.LOCALIP)
                '.SetItemTable("take2yn", 13, 1, IIf(Me.chkTake2Yn.Checked, "1", "0").ToString)
            End With

            Return it20

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return Nothing
        End Try
    End Function

    Private Function fnCollectItemTable_21(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_21(String) As LISAPP.ItemTableCollection"

        Try
            Dim it21 As New LISAPP.ItemTableCollection

            With it21
                .SetItemTable("partcd", 1, 1, Me.txtPartCd.Text)
                .SetItemTable("slipcd", 2, 1, Me.txtSlipCd.Text)
                .SetItemTable("usdt", 3, 1, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)

                If txtUEDT.Text = "" Then
                    .SetItemTable("uedt", 4, 1, msUEDT)
                Else
                    .SetItemTable("uedt", 4, 1, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                End If

                .SetItemTable("regdt", 5, 1, rsRegDT)
                .SetItemTable("regid", 6, 1, USER_INFO.USRID)
                .SetItemTable("slipnm", 7, 1, Me.txtSlipNm.Text)
                .SetItemTable("slipnms", 8, 1, Me.txtSlipNmS.Text)
                .SetItemTable("slipnmd", 9, 1, Me.txtSlipNmD.Text)
                .SetItemTable("slipnmp", 10, 1, Me.txtSlipNmP.Text)
                .SetItemTable("dispseq", 11, 1, Me.txtDispSeq.Text)
                .SetItemTable("regip", 12, 1, USER_INFO.LOCALIP)
                .SetItemTable("take2yn", 13, 1, IIf(Me.chkTake2Yn.Checked, "1", "0").ToString)
            End With

            Return it21

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function fnFindChildControl(ByVal actrlCol As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control
        Dim sFn As String = "Private Function fnFindChildControl(ByVal actrlCol As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control"

        Try
            Dim ctrl As System.Windows.Forms.Control

            For Each ctrl In actrlCol
                If ctrl.Controls.Count > 0 Then
                    fnFindChildControl(ctrl.Controls)
                ElseIf ctrl.Controls.Count = 0 Then
                    If CType(ctrl.Tag, String) <> "" Then
                        mchildctrlcol.Add(ctrl)
                    End If
                End If
            Next
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentSlipInfo(rsPartCd, rsSlipCd, rsUsDt)

            If dt.Rows.Count > 0 Then
                Return "시작일시가 " + dt.Rows(0).Item(0).ToString + "인 동일 " + dt.Rows(0).Item(1).ToString + " (분야)슬립 코드가 존재합니다." + vbCrLf + vbCrLf + _
                       "부서코드,분야코드 또는 시작일시를 재조정하십시오!!"
            Else
                Return ""
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return "Error"
        End Try
    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim dt As DataTable = mobjDAF.GetNewRegDT

            If dt.Rows.Count > 0 Then
                fnGetSystemDT = dt.Rows(0).Item(0).ToString
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

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnRegSpc() As Boolean"

        Try
            Dim it20 As New LISAPP.ItemTableCollection
            Dim it21 As New LISAPP.ItemTableCollection

            Dim iRegType20 As Integer = 0, iRegType21 As Integer = 0
            Dim sRegDT As String

            iRegType20 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType21 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it20 = fnCollectItemTable_20(sRegDT)
            it21 = fnCollectItemTable_21(sRegDT)

            If mobjDAF.TransSlipInfo(it20, iRegType20, it21, iRegType21, _
                                     Me.txtPartCd.Text, Me.txtSlipCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Len(Me.txtPartCd.Text.Trim) < 1 Then
                MsgBox("분야코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtSlipCd.Text.Trim) < 1 Then
                MsgBox("슬립코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsDate(Me.txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtDispSeq.Text = "" Then Me.txtDispSeq.Text = "999"

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtPartCd.Text, Me.txtSlipCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Me.txtPartNm.Text.Trim = "" Then
                MsgBox("분야명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtPartNmS.Text.Trim = "" Then
                MsgBox("분야명(약어)를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtPartNmD.Text.Trim = "" Then
                MsgBox("분야명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtPartNmP.Text.Trim = "" Then
                MsgBox("분야명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtSlipNm.Text.Trim = "" Then
                MsgBox("슬립명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtSlipNmS.Text.Trim = "" Then
                MsgBox("슬립명(약어)를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtSlipNmD.Text.Trim = "" Then
                MsgBox("슬립명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtSlipNmP.Text.Trim = "" Then
                MsgBox("슬립명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
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

    Public Sub sbDisplayCdDetail(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_Slip(rsPartCd, rsSlipCd, rsUsDt)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Slip(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Slip()"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mobjDAF.GetSlipInfo(rsPartCd, rsSlipCd, rsUsDt)

            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()
            sbInitialize_CtrlCollection()
            fnFindChildControl(Me.Controls)

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

            If Not IsNothing(Me.Owner) Then
                If Not CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Me.txtUSDay.Text = rsUsDt.Insert(4, "-").Insert(7, "-").Substring(0, 10)
                    Me.dtpUSTime.Value = CDate(rsUsDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":"))
                End If
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_PartOnly(ByVal rsPartCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_PartOnly(ByVal asBuf As String, ByVal asTCd As String)"

        Try
            Dim dt As DataTable = mobjDAF.GetOnlyPartInfo(rsPartCd, rsUsDt)

            If dt.Rows.Count > 0 Then
                Me.txtPartNm.Text = dt.Rows(0).Item("partnm").ToString()
                Me.txtPartNmS.Text = dt.Rows(0).Item("partnms").ToString()
                Me.txtPartNmD.Text = dt.Rows(0).Item("partnmd").ToString()
                Me.txtPartNmP.Text = dt.Rows(0).Item("partnmp").ToString()
                Me.txtTelNo.Text = dt.Rows(0).Item("telno").ToString()

                If dt.Rows(0).Item("take2yn").ToString = "1" Then Me.chkTake2Yn.Checked = True

                For ix As Integer = 0 To Me.cboPartGbn.Items.Count - 1
                    Me.cboPartGbn.SelectedIndex = ix
                    If Me.cboPartGbn.Text.Trim = dt.Rows(0).Item("partgbn_01").ToString().Trim Then
                        Exit For
                    End If
                Next

                Me.txtPartNm.ReadOnly = True
                Me.txtPartNmS.ReadOnly = True
                Me.txtPartNmD.ReadOnly = True
                Me.txtPartNmP.ReadOnly = True
            Else
                Me.txtPartNm.ReadOnly = False
                Me.txtPartNmS.ReadOnly = False
                Me.txtPartNmD.ReadOnly = False
                Me.txtPartNmP.ReadOnly = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
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

            miSelectKey = 1

            sbInitialize_ErrProvider()
            sbInitialize_Control()

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn As String = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_Control(Optional ByVal iMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If iMode = 0 Then
                'tpg1 초기화
                Me.txtPartCd.Text = "" : Me.txtSlipCd.Text = "" : Me.btnUE.Visible = False

                Me.txtPartNm.Text = "" : Me.txtPartNmS.Text = "" : Me.txtPartNmD.Text = "" : Me.txtPartNmP.Text = "" : Me.txtDispSeq.Text = ""
                Me.txtPartNm.ReadOnly = False : Me.txtPartNmS.ReadOnly = False : Me.txtPartNmD.ReadOnly = False : Me.txtPartNmP.ReadOnly = False
                Me.txtTelNo.Text = ""

                Me.txtSlipNm.Text = "" : Me.txtSlipNmS.Text = "" : Me.txtSlipNmD.Text = "" : Me.txtSlipNmP.Text = "" : Me.txtRegNm.Text = ""

                Me.txtUSDT.Text = "" : Me.txtUEDT.Text = "" : Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
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

    Public Sub sbSetNewUSDT()
        Dim sFn As String = ""

        Try
            Dim sDate As String = fnGetSystemDT()
            sDate = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2) + " " + sDate.Substring(8, 2) + ":" + sDate.Substring(10, 2) + ":" + sDate.Substring(12, 2)

#If DEBUG Then
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 0, CType(sDate, Date)), "yyyy-MM-dd 00:00:00")
#Else
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 1, CType(sDate, Date)), "yyyy-MM-dd 00:00:00")
#End If
            miSelectKey = 1

            Me.txtUSDay.Text = sSysDT.Substring(0, 10)
            Me.dtpUSDay.Value = CType(sSysDT, Date)
            Me.dtpUSTime.Value = CType(sSysDT, Date)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbInitialize()
    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents lblPartcd As System.Windows.Forms.Label
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
    Friend WithEvents lblSlipCd As System.Windows.Forms.Label
    Friend WithEvents lblSlipNmS As System.Windows.Forms.Label
    Friend WithEvents lblSlipNmP As System.Windows.Forms.Label
    Friend WithEvents lblSlipNmD As System.Windows.Forms.Label
    Friend WithEvents lblSlipNm As System.Windows.Forms.Label
    Friend WithEvents lblPartNmS As System.Windows.Forms.Label
    Friend WithEvents lblPartNmP As System.Windows.Forms.Label
    Friend WithEvents lblPartNmD As System.Windows.Forms.Label
    Friend WithEvents lblPartNm As System.Windows.Forms.Label
    Friend WithEvents txtPartNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtPartNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtPartNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtPartNm As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNm As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipCd As System.Windows.Forms.TextBox
    Friend WithEvents txtPartCd As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tpg1 = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtUEDT = New System.Windows.Forms.TextBox
        Me.lblUEDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.txtUSDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.lblUSDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.txtTelNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblPartGbn = New System.Windows.Forms.Label
        Me.cboPartGbn = New System.Windows.Forms.ComboBox
        Me.chkTake2Yn = New System.Windows.Forms.CheckBox
        Me.txtDispSeq = New System.Windows.Forms.TextBox
        Me.lblDispSeq = New System.Windows.Forms.Label
        Me.lblSlipNmS = New System.Windows.Forms.Label
        Me.txtSlipNmS = New System.Windows.Forms.TextBox
        Me.lblSlipNmP = New System.Windows.Forms.Label
        Me.txtSlipNmP = New System.Windows.Forms.TextBox
        Me.lblSlipNmD = New System.Windows.Forms.Label
        Me.txtSlipNmD = New System.Windows.Forms.TextBox
        Me.lblSlipNm = New System.Windows.Forms.Label
        Me.txtSlipNm = New System.Windows.Forms.TextBox
        Me.lblPartNmS = New System.Windows.Forms.Label
        Me.txtPartNmS = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblPartNmP = New System.Windows.Forms.Label
        Me.txtPartNmP = New System.Windows.Forms.TextBox
        Me.lblPartNmD = New System.Windows.Forms.Label
        Me.txtPartNmD = New System.Windows.Forms.TextBox
        Me.lblPartNm = New System.Windows.Forms.Label
        Me.txtPartNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.lblSlipCd = New System.Windows.Forms.Label
        Me.txtSlipCd = New System.Windows.Forms.TextBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker
        Me.txtUSDay = New System.Windows.Forms.TextBox
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker
        Me.lblUSDayTime = New System.Windows.Forms.Label
        Me.lblPartcd = New System.Windows.Forms.Label
        Me.txtPartCd = New System.Windows.Forms.TextBox
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tpg1.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.SuspendLayout()
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'pnlTop
        '
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.Controls.Add(Me.tclSpc)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 116
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tpg1)
        Me.tclSpc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tclSpc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tclSpc.ItemSize = New System.Drawing.Size(84, 17)
        Me.tclSpc.Location = New System.Drawing.Point(0, 0)
        Me.tclSpc.Name = "tclSpc"
        Me.tclSpc.SelectedIndex = 0
        Me.tclSpc.Size = New System.Drawing.Size(788, 601)
        Me.tclSpc.TabIndex = 0
        Me.tclSpc.TabStop = False
        '
        'tpg1
        '
        Me.tpg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpg1.Controls.Add(Me.txtRegNm)
        Me.tpg1.Controls.Add(Me.txtUEDT)
        Me.tpg1.Controls.Add(Me.lblUEDT)
        Me.tpg1.Controls.Add(Me.txtRegDT)
        Me.tpg1.Controls.Add(Me.txtUSDT)
        Me.tpg1.Controls.Add(Me.lblUserNm)
        Me.tpg1.Controls.Add(Me.lblRegDT)
        Me.tpg1.Controls.Add(Me.lblUSDT)
        Me.tpg1.Controls.Add(Me.txtRegID)
        Me.tpg1.Controls.Add(Me.grpCdInfo1)
        Me.tpg1.Controls.Add(Me.grpCd)
        Me.tpg1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpg1.Location = New System.Drawing.Point(4, 21)
        Me.tpg1.Name = "tpg1"
        Me.tpg1.Size = New System.Drawing.Size(780, 576)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "부서/분야정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(702, 548)
        Me.txtRegNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(74, 21)
        Me.txtRegNm.TabIndex = 12
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUEDT.Location = New System.Drawing.Point(312, 548)
        Me.txtUEDT.Name = "txtUEDT"
        Me.txtUEDT.ReadOnly = True
        Me.txtUEDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUEDT.TabIndex = 0
        Me.txtUEDT.TabStop = False
        Me.txtUEDT.Tag = "UEDT"
        '
        'lblUEDT
        '
        Me.lblUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUEDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Location = New System.Drawing.Point(214, 548)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUEDT.TabIndex = 0
        Me.lblUEDT.Tag = ""
        Me.lblUEDT.Text = "종료일시(선택)"
        Me.lblUEDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(506, 548)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 0
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'txtUSDT
        '
        Me.txtUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUSDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUSDT.Location = New System.Drawing.Point(107, 548)
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUSDT.TabIndex = 0
        Me.txtUSDT.TabStop = False
        Me.txtUSDT.Tag = "USDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(617, 548)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 21)
        Me.lblUserNm.TabIndex = 0
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(421, 548)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUSDT
        '
        Me.lblUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDT.ForeColor = System.Drawing.Color.Black
        Me.lblUSDT.Location = New System.Drawing.Point(9, 548)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUSDT.TabIndex = 0
        Me.lblUSDT.Text = "시작일시(선택)"
        Me.lblUSDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(702, 548)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.txtTelNo)
        Me.grpCdInfo1.Controls.Add(Me.Label1)
        Me.grpCdInfo1.Controls.Add(Me.lblPartGbn)
        Me.grpCdInfo1.Controls.Add(Me.cboPartGbn)
        Me.grpCdInfo1.Controls.Add(Me.chkTake2Yn)
        Me.grpCdInfo1.Controls.Add(Me.txtDispSeq)
        Me.grpCdInfo1.Controls.Add(Me.lblDispSeq)
        Me.grpCdInfo1.Controls.Add(Me.lblSlipNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtSlipNmS)
        Me.grpCdInfo1.Controls.Add(Me.lblSlipNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtSlipNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblSlipNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtSlipNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblSlipNm)
        Me.grpCdInfo1.Controls.Add(Me.txtSlipNm)
        Me.grpCdInfo1.Controls.Add(Me.lblPartNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtPartNmS)
        Me.grpCdInfo1.Controls.Add(Me.Label10)
        Me.grpCdInfo1.Controls.Add(Me.lblPartNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtPartNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblPartNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtPartNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblPartNm)
        Me.grpCdInfo1.Controls.Add(Me.txtPartNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 70)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 470)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "부서/분야 정보"
        '
        'txtTelNo
        '
        Me.txtTelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTelNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTelNo.Location = New System.Drawing.Point(519, 39)
        Me.txtTelNo.MaxLength = 20
        Me.txtTelNo.Name = "txtTelNo"
        Me.txtTelNo.Size = New System.Drawing.Size(119, 21)
        Me.txtTelNo.TabIndex = 10
        Me.txtTelNo.Tag = "TELNO"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(431, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 21)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "내선번호"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPartGbn
        '
        Me.lblPartGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPartGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPartGbn.ForeColor = System.Drawing.Color.White
        Me.lblPartGbn.Location = New System.Drawing.Point(431, 61)
        Me.lblPartGbn.Name = "lblPartGbn"
        Me.lblPartGbn.Size = New System.Drawing.Size(87, 21)
        Me.lblPartGbn.TabIndex = 19
        Me.lblPartGbn.Text = "부서구분"
        Me.lblPartGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboPartGbn
        '
        Me.cboPartGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartGbn.FormattingEnabled = True
        Me.cboPartGbn.Items.AddRange(New Object() {"[0] ", "[1] 종합검증", "[2] 미생물", "[3] 혈액은행", "[4] 핵의학체외"})
        Me.cboPartGbn.Location = New System.Drawing.Point(519, 61)
        Me.cboPartGbn.Name = "cboPartGbn"
        Me.cboPartGbn.Size = New System.Drawing.Size(119, 20)
        Me.cboPartGbn.TabIndex = 11
        Me.cboPartGbn.Tag = "PARTGBN_01"
        '
        'chkTake2Yn
        '
        Me.chkTake2Yn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkTake2Yn.Location = New System.Drawing.Point(432, 132)
        Me.chkTake2Yn.Name = "chkTake2Yn"
        Me.chkTake2Yn.Size = New System.Drawing.Size(171, 21)
        Me.chkTake2Yn.TabIndex = 12
        Me.chkTake2Yn.Tag = "TAKE2YN"
        Me.chkTake2Yn.Text = "2차 접수여부"
        Me.chkTake2Yn.UseVisualStyleBackColor = False
        '
        'txtDispSeq
        '
        Me.txtDispSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispSeq.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtDispSeq.Location = New System.Drawing.Point(519, 16)
        Me.txtDispSeq.MaxLength = 3
        Me.txtDispSeq.Name = "txtDispSeq"
        Me.txtDispSeq.Size = New System.Drawing.Size(27, 21)
        Me.txtDispSeq.TabIndex = 9
        Me.txtDispSeq.Tag = "DISPSEQ"
        '
        'lblDispSeq
        '
        Me.lblDispSeq.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDispSeq.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDispSeq.ForeColor = System.Drawing.Color.White
        Me.lblDispSeq.Location = New System.Drawing.Point(431, 16)
        Me.lblDispSeq.Name = "lblDispSeq"
        Me.lblDispSeq.Size = New System.Drawing.Size(87, 21)
        Me.lblDispSeq.TabIndex = 9
        Me.lblDispSeq.Text = "화면표시순서"
        Me.lblDispSeq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSlipNmS
        '
        Me.lblSlipNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlipNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlipNmS.ForeColor = System.Drawing.Color.White
        Me.lblSlipNmS.Location = New System.Drawing.Point(8, 150)
        Me.lblSlipNmS.Name = "lblSlipNmS"
        Me.lblSlipNmS.Size = New System.Drawing.Size(129, 21)
        Me.lblSlipNmS.TabIndex = 0
        Me.lblSlipNmS.Text = "검사분야명(약어)"
        Me.lblSlipNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSlipNmS
        '
        Me.txtSlipNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSlipNmS.Location = New System.Drawing.Point(138, 150)
        Me.txtSlipNmS.MaxLength = 10
        Me.txtSlipNmS.Name = "txtSlipNmS"
        Me.txtSlipNmS.Size = New System.Drawing.Size(264, 21)
        Me.txtSlipNmS.TabIndex = 6
        Me.txtSlipNmS.Tag = "SLIPNMS"
        '
        'lblSlipNmP
        '
        Me.lblSlipNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlipNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlipNmP.ForeColor = System.Drawing.Color.White
        Me.lblSlipNmP.Location = New System.Drawing.Point(8, 194)
        Me.lblSlipNmP.Name = "lblSlipNmP"
        Me.lblSlipNmP.Size = New System.Drawing.Size(129, 21)
        Me.lblSlipNmP.TabIndex = 0
        Me.lblSlipNmP.Text = "검사분야명(출력)"
        Me.lblSlipNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSlipNmP
        '
        Me.txtSlipNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSlipNmP.Location = New System.Drawing.Point(138, 194)
        Me.txtSlipNmP.MaxLength = 20
        Me.txtSlipNmP.Name = "txtSlipNmP"
        Me.txtSlipNmP.Size = New System.Drawing.Size(264, 21)
        Me.txtSlipNmP.TabIndex = 8
        Me.txtSlipNmP.Tag = "SLIPNMP"
        '
        'lblSlipNmD
        '
        Me.lblSlipNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlipNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlipNmD.ForeColor = System.Drawing.Color.White
        Me.lblSlipNmD.Location = New System.Drawing.Point(8, 172)
        Me.lblSlipNmD.Name = "lblSlipNmD"
        Me.lblSlipNmD.Size = New System.Drawing.Size(129, 21)
        Me.lblSlipNmD.TabIndex = 0
        Me.lblSlipNmD.Text = "검사분야명(화면)"
        Me.lblSlipNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSlipNmD
        '
        Me.txtSlipNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSlipNmD.Location = New System.Drawing.Point(138, 172)
        Me.txtSlipNmD.MaxLength = 20
        Me.txtSlipNmD.Name = "txtSlipNmD"
        Me.txtSlipNmD.Size = New System.Drawing.Size(264, 21)
        Me.txtSlipNmD.TabIndex = 7
        Me.txtSlipNmD.Tag = "SLIPNMD"
        '
        'lblSlipNm
        '
        Me.lblSlipNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlipNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlipNm.ForeColor = System.Drawing.Color.White
        Me.lblSlipNm.Location = New System.Drawing.Point(8, 128)
        Me.lblSlipNm.Name = "lblSlipNm"
        Me.lblSlipNm.Size = New System.Drawing.Size(129, 21)
        Me.lblSlipNm.TabIndex = 0
        Me.lblSlipNm.Text = "검사분야"
        Me.lblSlipNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSlipNm
        '
        Me.txtSlipNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSlipNm.Location = New System.Drawing.Point(138, 128)
        Me.txtSlipNm.MaxLength = 20
        Me.txtSlipNm.Name = "txtSlipNm"
        Me.txtSlipNm.Size = New System.Drawing.Size(264, 21)
        Me.txtSlipNm.TabIndex = 5
        Me.txtSlipNm.Tag = "SLIPNM"
        '
        'lblPartNmS
        '
        Me.lblPartNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPartNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPartNmS.ForeColor = System.Drawing.Color.White
        Me.lblPartNmS.Location = New System.Drawing.Point(8, 38)
        Me.lblPartNmS.Name = "lblPartNmS"
        Me.lblPartNmS.Size = New System.Drawing.Size(129, 21)
        Me.lblPartNmS.TabIndex = 5
        Me.lblPartNmS.Text = "검사부서명(약어)"
        Me.lblPartNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPartNmS
        '
        Me.txtPartNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtPartNmS.Location = New System.Drawing.Point(138, 38)
        Me.txtPartNmS.MaxLength = 10
        Me.txtPartNmS.Name = "txtPartNmS"
        Me.txtPartNmS.Size = New System.Drawing.Size(264, 21)
        Me.txtPartNmS.TabIndex = 2
        Me.txtPartNmS.Tag = "PARTNMS"
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 116)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(756, 2)
        Me.Label10.TabIndex = 0
        '
        'lblPartNmP
        '
        Me.lblPartNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPartNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPartNmP.ForeColor = System.Drawing.Color.White
        Me.lblPartNmP.Location = New System.Drawing.Point(8, 82)
        Me.lblPartNmP.Name = "lblPartNmP"
        Me.lblPartNmP.Size = New System.Drawing.Size(129, 21)
        Me.lblPartNmP.TabIndex = 0
        Me.lblPartNmP.Text = "검사부서명(출력)"
        Me.lblPartNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPartNmP
        '
        Me.txtPartNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtPartNmP.Location = New System.Drawing.Point(138, 82)
        Me.txtPartNmP.MaxLength = 20
        Me.txtPartNmP.Name = "txtPartNmP"
        Me.txtPartNmP.Size = New System.Drawing.Size(264, 21)
        Me.txtPartNmP.TabIndex = 4
        Me.txtPartNmP.Tag = "PARTNMP"
        '
        'lblPartNmD
        '
        Me.lblPartNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPartNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPartNmD.ForeColor = System.Drawing.Color.White
        Me.lblPartNmD.Location = New System.Drawing.Point(8, 60)
        Me.lblPartNmD.Name = "lblPartNmD"
        Me.lblPartNmD.Size = New System.Drawing.Size(129, 21)
        Me.lblPartNmD.TabIndex = 0
        Me.lblPartNmD.Text = "검사부서명(화면)"
        Me.lblPartNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPartNmD
        '
        Me.txtPartNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtPartNmD.Location = New System.Drawing.Point(138, 60)
        Me.txtPartNmD.MaxLength = 20
        Me.txtPartNmD.Name = "txtPartNmD"
        Me.txtPartNmD.Size = New System.Drawing.Size(264, 21)
        Me.txtPartNmD.TabIndex = 3
        Me.txtPartNmD.Tag = "PARTNMD"
        '
        'lblPartNm
        '
        Me.lblPartNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPartNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPartNm.ForeColor = System.Drawing.Color.White
        Me.lblPartNm.Location = New System.Drawing.Point(8, 16)
        Me.lblPartNm.Name = "lblPartNm"
        Me.lblPartNm.Size = New System.Drawing.Size(129, 21)
        Me.lblPartNm.TabIndex = 0
        Me.lblPartNm.Text = "검사부서명"
        Me.lblPartNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPartNm
        '
        Me.txtPartNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtPartNm.Location = New System.Drawing.Point(138, 16)
        Me.txtPartNm.MaxLength = 20
        Me.txtPartNm.Name = "txtPartNm"
        Me.txtPartNm.Size = New System.Drawing.Size(264, 21)
        Me.txtPartNm.TabIndex = 1
        Me.txtPartNm.Tag = "PARTNM"
        '
        'grpCd
        '
        Me.grpCd.Controls.Add(Me.lblSlipCd)
        Me.grpCd.Controls.Add(Me.txtSlipCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.dtpUSTime)
        Me.grpCd.Controls.Add(Me.txtUSDay)
        Me.grpCd.Controls.Add(Me.dtpUSDay)
        Me.grpCd.Controls.Add(Me.lblUSDayTime)
        Me.grpCd.Controls.Add(Me.lblPartcd)
        Me.grpCd.Controls.Add(Me.txtPartCd)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 10)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        Me.grpCd.Text = "부서/분야 코드"
        '
        'lblSlipCd
        '
        Me.lblSlipCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlipCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlipCd.ForeColor = System.Drawing.Color.White
        Me.lblSlipCd.Location = New System.Drawing.Point(375, 15)
        Me.lblSlipCd.Name = "lblSlipCd"
        Me.lblSlipCd.Size = New System.Drawing.Size(57, 21)
        Me.lblSlipCd.TabIndex = 0
        Me.lblSlipCd.Text = "분야코드"
        Me.lblSlipCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSlipCd
        '
        Me.txtSlipCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSlipCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSlipCd.Location = New System.Drawing.Point(433, 15)
        Me.txtSlipCd.MaxLength = 1
        Me.txtSlipCd.Name = "txtSlipCd"
        Me.txtSlipCd.Size = New System.Drawing.Size(20, 21)
        Me.txtSlipCd.TabIndex = 5
        Me.txtSlipCd.Tag = "SLIPCD"
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(686, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 6
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(210, 15)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 3
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(111, 15)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(77, 21)
        Me.txtUSDay.TabIndex = 1
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(189, 15)
        Me.dtpUSDay.Name = "dtpUSDay"
        Me.dtpUSDay.Size = New System.Drawing.Size(20, 21)
        Me.dtpUSDay.TabIndex = 2
        Me.dtpUSDay.TabStop = False
        Me.dtpUSDay.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'lblUSDayTime
        '
        Me.lblUSDayTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Location = New System.Drawing.Point(8, 15)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(102, 21)
        Me.lblUSDayTime.TabIndex = 0
        Me.lblUSDayTime.Text = "시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPartcd
        '
        Me.lblPartcd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPartcd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPartcd.ForeColor = System.Drawing.Color.White
        Me.lblPartcd.Location = New System.Drawing.Point(286, 15)
        Me.lblPartcd.Name = "lblPartcd"
        Me.lblPartcd.Size = New System.Drawing.Size(61, 21)
        Me.lblPartcd.TabIndex = 0
        Me.lblPartcd.Text = "부서코드"
        Me.lblPartcd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPartCd
        '
        Me.txtPartCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPartCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPartCd.Location = New System.Drawing.Point(348, 15)
        Me.txtPartCd.MaxLength = 1
        Me.txtPartCd.Name = "txtPartCd"
        Me.txtPartCd.Size = New System.Drawing.Size(21, 21)
        Me.txtPartCd.TabIndex = 4
        Me.txtPartCd.Tag = "PARTCD"
        '
        'FDF02
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF02"
        Me.Text = "[02] 부서/분야"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tpg1.ResumeLayout(False)
        Me.tpg1.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If Me.txtPartCd.Text = "" Or Me.txtSlipCd.Text = "" Then Exit Sub

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "분야코드   : " + Me.txtPartCd.Text + ", 슬립코드 : " + Me.txtSlipCd.Text & vbCrLf
            sMsg += "분야명     : " + Me.txtPartNm.Text + vbCrLf
            sMsg += "슬립명 : " + Me.txtSlipNm.Text + vbCrLf + vbCrLf
            sMsg += "을(를) 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mobjDAF.TransSlipInfo_UE(Me.txtPartCd.Text, Me.txtSlipCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID, sUeDate + sUeTime) Then
                MsgBox("해당 (분야)슬립정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub dtpUSDay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUSDay.ValueChanged
        If miSelectKey = 1 Then Exit Sub
        If txtUSDay.Text.Trim = "" Then Exit Sub

        txtUSDay.Text = Format(dtpUSDay.Value, "yyyy-MM-dd").Substring(0, 10)
    End Sub

    Private Sub txtPartCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPartCd.KeyDown, txtPartNm.KeyDown, txtPartNmS.KeyDown, txtPartNmD.KeyDown, txtPartNmP.KeyDown, txtSlipCd.KeyDown, txtSlipNm.KeyDown, txtSlipNmD.KeyDown, txtSlipNmP.KeyDown, txtSlipNmS.KeyDown, cboPartGbn.KeyDown, chkTake2Yn.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub txtPartCd_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPartCd.TextChanged
        If Not CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then Exit Sub

        If Me.txtPartCd.Text.Length = Me.txtPartCd.MaxLength Then
            sbDisplayCdDetail_PartOnly(Me.txtPartCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString)
        End If
    End Sub

    Private Sub FDF02_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub



    Private Sub txtPartNm_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPartNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If Me.txtPartNmS.Text.Trim = "" Then
            If Me.txtPartNm.Text.Length > Me.txtPartNmS.MaxLength Then
                Me.txtPartNmS.Text = Me.txtPartNm.Text.Substring(0, Me.txtPartNmS.MaxLength)
            Else
                Me.txtPartNmS.Text = Me.txtPartNm.Text
            End If
        End If

        If Me.txtPartNmD.Text.Trim = "" Then
            If Me.txtPartNm.Text.Length > Me.txtPartNmD.MaxLength Then
                Me.txtPartNmD.Text = Me.txtPartNm.Text.Substring(0, Me.txtPartNmD.MaxLength)
            Else
                Me.txtPartNmD.Text = Me.txtPartNm.Text
            End If
        End If

        If Me.txtPartNmP.Text.Trim = "" Then
            If Me.txtPartNm.Text.Length > Me.txtPartNmP.MaxLength Then
                Me.txtPartNmP.Text = Me.txtPartNm.Text.Substring(0, Me.txtPartNmP.MaxLength)
            Else
                Me.txtPartNmP.Text = Me.txtPartNm.Text
            End If
        End If
    End Sub

    Private Sub txtSlipNm_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSlipNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If Me.txtSlipNmS.Text.Trim = "" Then
            If Me.txtSlipNm.Text.Length > Me.txtSlipNmS.MaxLength Then
                Me.txtSlipNmS.Text = Me.txtSlipNm.Text.Substring(0, Me.txtSlipNmS.MaxLength)
            Else
                Me.txtSlipNmS.Text = Me.txtSlipNm.Text
            End If
        End If

        If Me.txtSlipNmD.Text.Trim = "" Then
            If Me.txtSlipNm.Text.Length > Me.txtSlipNmD.MaxLength Then
                Me.txtSlipNmD.Text = Me.txtSlipNm.Text.Substring(0, Me.txtSlipNmD.MaxLength)
            Else
                Me.txtSlipNmD.Text = Me.txtSlipNm.Text
            End If
        End If

        If Me.txtSlipNmP.Text.Trim = "" Then
            If Me.txtSlipNm.Text.Length > Me.txtSlipNmP.MaxLength Then
                Me.txtSlipNmP.Text = Me.txtSlipNm.Text.Substring(0, Me.txtSlipNmP.MaxLength)
            Else
                Me.txtSlipNmP.Text = Me.txtSlipNm.Text
            End If
        End If
    End Sub
End Class
