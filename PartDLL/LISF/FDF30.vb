'>>> [30] 성분제제
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF30
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF30.vb, Class : FDF30" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_COMCD
    Friend WithEvents lblComGbn As System.Windows.Forms.Label
    Friend WithEvents cboComGbn As System.Windows.Forms.ComboBox
    Friend WithEvents cboGOrdCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblGOrdCd As System.Windows.Forms.Label
    Friend WithEvents chkBagOrdYn As System.Windows.Forms.CheckBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents lblCrossLevel As System.Windows.Forms.Label
    Friend WithEvents cboCrossLevel As System.Windows.Forms.ComboBox
    Friend WithEvents txtCLisCd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBldCd2 As System.Windows.Forms.TextBox
    Friend WithEvents lblBldCd2 As System.Windows.Forms.Label
    Friend WithEvents cboOReqItem As System.Windows.Forms.ComboBox

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            dt = mobjDAF.GetUsUeCd_ComCd(Me.txtComCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransComCdInfo_DEL(Me.txtComCd.Text, Ctrl.Get_Code(cboSpcCd), Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

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
            dt = mobjDAF.GetUsUeDupl_ComCd(Me.txtComCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransComCdInfo_UPD_US(Me.txtComCd.Text, Ctrl.Get_Code(Me.cboSpcCd), Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransComCdInfo_UPD_UE(Me.txtComCd.Text, Ctrl.Get_Code(Me.cboSpcCd), Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
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

    Public Sub sbEditUseDt(ByVal rsUseTag As String)
        Dim sFn As String = "Public Sub sbEditUseDt"

        Try
            Dim fgf02 As New FGF03

            With fgf02
                .txtCd.Text = Me.txtComCd.Text
                .txtNm.Text = Me.txtComNm.Text

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

    Private Function fnCollectItemTable_120(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_120(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it120 As New LISAPP.ItemTableCollection

            With it120
                .SetItemTable("COMCD", 1, 1, Me.txtComCd.Text)
                .SetItemTable("SPCCD", 2, 1, Ctrl.Get_Code(Me.cboSpcCd))
                .SetItemTable("USDT", 3, 1, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)

                If txtUEDT.Text = "" Then
                    .SetItemTable("UEDT", 4, 1, msUEDT)
                Else
                    .SetItemTable("UEDT", 4, 1, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                End If

                .SetItemTable("REGDT", 5, 1, rsRegDT)
                .SetItemTable("REGID", 6, 1, USER_INFO.USRID)
                .SetItemTable("COMNM", 7, 1, Me.txtComNm.Text)
                .SetItemTable("COMNMS", 8, 1, Me.txtComNmS.Text)
                .SetItemTable("COMNMD", 9, 1, Me.txtComNmD.Text)
                .SetItemTable("COMNMP", 10, 1, Me.txtComNmP.Text)

                If Me.rdo400.Checked Then
                    .SetItemTable("DONQNT", 11, 1, "400")
                ElseIf Me.rod320.Checked Then
                    .SetItemTable("DONQNT", 11, 1, "320")
                Else
                    .SetItemTable("DONQNT", 11, 1, "")
                End If

                .SetItemTable("AVAILMI", 12, 1, (Convert.ToInt32(Me.cboAvailMi.SelectedItem.ToString()) * 24 * 60).ToString())

                .SetItemTable("COMORDCD", 13, 1, Me.txtTOrdCd.Text)
                .SetItemTable("PSCOMCD", 14, 1, Ctrl.Get_Code(Me.cboPSComCd).Trim)
                .SetItemTable("FTCD", 15, 1, Ctrl.Get_Code(Me.cboFTCD).Trim)
                .SetItemTable("DISPSEQO", 16, 1, Me.txtDispseqO.Text)
                .SetItemTable("DISPSEQL", 17, 1, Me.txtDispseqL.Text)
                .SetItemTable("BLDCD", 18, 1, Me.txtBldCd.Text)
                .SetItemTable("SUGACD", 19, 1, Me.txtSugaCd.Text)
                .SetItemTable("ORDSLIP", 20, 1, Ctrl.Get_Code(Me.cboOrdSlip))
                .SetItemTable("EMERGBN", 21, 1, CType(IIf(Me.chkEmerGbn.Checked, "1", "0"), String))
                .SetItemTable("PEDGBN", 22, 1, CType(IIf(Me.chkPTGbn.Checked, "1", "0"), String))
                .SetItemTable("IOGBN", 23, 1, CType(IIf(Me.chkGbnO.Checked And Me.chkGbnI.Checked, "0", IIf(Me.chkGbnO.Checked, "1", "2")), String))
                .SetItemTable("DSPCCD1", 24, 1, Ctrl.Get_Code(Me.cboDSpcNm1))
                '20200716 jhs 혈액원 성분제재 수정 시에 초기화 되기 때문에 추가
                '.SetItemTable("DSPCCD2", 25, 1, "")
                .SetItemTable("DSPCCD2", 25, 1, Me.txtBldCd2.Text)
                '--------------------------------------------------------
                .SetItemTable("PTGBN", 26, 1, "")
                .SetItemTable("EXEDAY", 27, 1, CType(IIf(chkExeDay1.Checked, "1", "0"), String) _
                                             + CType(IIf(chkExeDay2.Checked, "1", "0"), String) _
                                             + CType(IIf(chkExeDay3.Checked, "1", "0"), String) _
                                             + CType(IIf(chkExeDay4.Checked, "1", "0"), String) _
                                             + CType(IIf(chkExeDay5.Checked, "1", "0"), String) _
                                             + CType(IIf(chkExeDay6.Checked, "1", "0"), String) _
                                             + CType(IIf(chkExeDay7.Checked, "1", "0"), String))
                .SetItemTable("OWARNINGGBN", 28, 1, CType(IIf(Me.cboOWarningGbn.SelectedIndex = -1, "", Me.cboOWarningGbn.SelectedIndex), String))
                .SetItemTable("OWARNING", 28, 1, Me.txtOWarning.Text)

                .SetItemTable("OREQITEM", 30, 1, CType(IIf(chkOReqItem1.Checked, "1", "0"), String) _
                                               + CType(IIf(chkOReqItem2.Checked, "1", "0"), String) _
                                               + CType(IIf(chkOReqItem3.Checked, "1", "0"), String) _
                                               + CType(IIf(chkOReqItem4.Checked, Ctrl.Get_Code(Me.cboOReqItem), "0"), String))

                .SetItemTable("ORDHIDE", 31, 1, CType(IIf(Me.chkOrdHIde.Checked, "1", "0"), String))
                .SetItemTable("COMGBN", 32, 1, Me.cboComGbn.Text.Substring(1, 1))

                Dim strGOrdCd As String = "", strGSpcCd As String = ""
                If Ctrl.Get_Code(Me.cboGOrdCd) <> "" Then
                    strGOrdCd = Ctrl.Get_Code(Me.cboGOrdCd).Trim
                    strGSpcCd = Ctrl.Get_Code(Me.cboSpcCd)
                End If

                .SetItemTable("GORDCD", 33, 1, strGOrdCd)
                .SetItemTable("GSPCCD", 34, 1, strGSpcCd)
                .SetItemTable("BAGORDYN", 35, 1, IIf(Me.chkBagOrdYn.Checked, "1", "").ToString)
                .SetItemTable("CROSSLEVEL", 36, 1, Ctrl.Get_Code(Me.cboCrossLevel))
                .SetItemTable("COMLISCD", 37, 1, Me.txtCLisCd.Text)
                .SetItemTable("REGIP", 38, 1, USER_INFO.LOCALIP)

            End With

            fnCollectItemTable_120 = it120
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsComCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String) As String
        Dim sFn As String = "Function fnFindConflict(ByVal asComCd As String, ByVal asUSDT As String) As String"

        Try
            Dim dt As DataTable = mobjDAF.GetRecentComCdInfo(rsComCd, rsSpcCd, rsUsDt)

            If dt.Rows.Count > 0 Then
                Return "시작일시가 " + dt.Rows(0).Item(0).ToString() + "인 동일 성분제제 코드가 존재합니다." + vbCrLf + vbCrLf + _
                       "시작일시를 재조정 하십시요!!"
            Else
                Return ""
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try

    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim dt As DataTable = New LISAPP.APP_F().GetNewRegDT

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
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
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it120 As New LISAPP.ItemTableCollection
            Dim iRegType120 As Integer = 0
            Dim sRegDT As String

            iRegType120 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it120 = fnCollectItemTable_120(sRegDT)

            If mobjDAF.TransComCdInfo(it120, iRegType120, _
                                      Me.txtComCd.Text, Ctrl.Get_Code(Me.cboSpcCd), Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID) Then
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
            If Len(Me.txtComCd.Text.Trim) < 1 Then
                MsgBox("성분제제코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            Dim sSpcCd As String = Ctrl.Get_Code(Me.cboSpcCd)

            If Len(sSpcCd) < 1 Then
                MsgBox("검체코드를 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtComNm.Text.Trim) < 1 Then
                MsgBox("성분제제명을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtComNmS.Text.Trim) < 1 Then
                MsgBox("성분제제명(처방)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtComNmD.Text.Trim) < 1 Then
                MsgBox("성분제제명(화면)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtComNmP.Text.Trim) < 1 Then
                MsgBox("성분제제명(출력)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsDate(Me.txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            'If cboTCdGbn.SelectedIndex = -1 Then
            '    MsgBox("검사코드구분을 선택해 주십시요!!", MsgBoxStyle.Critical)
            '    Exit Function
            'End If

            If cboOrdSlip.SelectedIndex = -1 Then
                MsgBox("검사처방슬립을 선택해 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            ''Group과 Child를 제외하고 나머지 경우에는 검사처방코드를 입력해야 한다.
            'If Not (CType(cboTCdGbn.SelectedItem, String).StartsWith("[G]") Or CType(cboTCdGbn.SelectedItem, String).StartsWith("[C]")) Then
            '    If txtTOrdCd.Text.Trim = "" Then
            '        MsgBox("검사처방코드를 입력하여 주십시요!!", MsgBoxStyle.Critical)
            '        Exit Function
            '    End If
            'End If

            '검사처방 사용하지 않는 경우를 제외하고 검사처방순번을 입력해야 한다.
            If Not Me.chkOrdHIde.Checked Then
                If IsNumeric(Me.txtDispseqO.Text) = False Then
                    MsgBox("검사처방순번을 숫자로 입력하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If
            End If

            '검사처방조건 중 외래, 병동을 적어도 하나 선택해야 한다.
            If Not (Me.chkGbnO.Checked Or Me.chkGbnI.Checked) Then
                MsgBox("검사처방조건을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            '기본처방검체를 입력해야 한다.
            If Me.cboDSpcNm1.SelectedIndex = -1 Then
                MsgBox("기본처방검체를 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            '기본처방검체를 입력해야 한다.
            If Me.cboComGbn.SelectedIndex = -1 Then
                MsgBox("성분제제구분을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            Dim sAvailDay As String = Ctrl.Get_Item(Me.cboAvailMi)

            If Not IsNumeric(sAvailDay) Then
                MsgBox("유효기간을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtComCd.Text, sSpcCd, txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""))

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            ' ErrProvider
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

    Public Sub sbDisplayCdDetail(ByVal rsComCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                If Not CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    sbDisplayCdList_Ref(rsUsDt)
                End If
            End If

            sbDisplayCdDetail_ComCd(rsComCd, rsSpcCd, rsUsDt)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_ComCd(ByVal rsComCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_ComCd(String, String, String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            dt = mobjDAF.GetComCdInfo(rsComCd, rsSpcCd, rsUsDt)

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
                                Else
                                    If CType(cctrl, Windows.Forms.ComboBox).DropDownStyle = Windows.Forms.ComboBoxStyle.DropDown Then
                                        CType(cctrl, Windows.Forms.ComboBox).SelectedItem = dt.Rows(i).Item(j).ToString
                                    ElseIf CType(cctrl, Windows.Forms.ComboBox).DropDownStyle = Windows.Forms.ComboBoxStyle.DropDownList Then
                                        CType(cctrl, Windows.Forms.ComboBox).SelectedItem = dt.Rows(i).Item(j).ToString
                                    End If
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

    Private Sub sbDisplayCdList_Ref(ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref(String)"

        Try
            miSelectKey = 1

            If rsUsDt = "" Then rsUsDt = "20100101000000"

            sbDisplayCdList_Ref_TOrdSlip(Me.cboOrdSlip, rsUsDt)
            sbDisplayCdList_Ref_FtCd(Me.cboFTCD)
            sbDisplayCdList_Ref_ComCd(Me.cboPSComCd, rsUsDt)
            'sbDisplayCdList_Ref_GOrdCd(Me.cboGOrdCd, rsUsDt)
            sbDisplayCdList_Ref_ComCd(Me.cboGOrdCd, rsUsDt)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_ComCd(ByVal actrl As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_ComCd(System.Windows.Forms.ComboBox, String)"

        Try
            Dim dt As DataTable = mobjDAF.GetComCdInfo(rsUsDt)

            actrl.Items.Clear()
            actrl.Items.Add("[     ] 선택안함")
            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("comnmd"))
                Next
            End With

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_GOrdCd(ByVal actrl As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_GOrdCd(ByVal actrl As System.Windows.Forms.ComboBox, String)"

        Try
            Dim dt As DataTable = mobjDAF.GetGOrdCdInfo(rsUsDt)

            actrl.Items.Clear()
            actrl.Items.Add("")

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("comnmd"))
                Next
            End With
            
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_FtCd(ByVal actrl As System.Windows.Forms.ComboBox)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_FtCd(ByVal actrl As System.Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable = mobjDAF.GetFtCdInfo()

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            actrl.Items.Add("[   ] 없음")
            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("FTNM"))
                Next
            End With
            
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_TOrdSlip(ByVal actrl As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_TOrdSlip(ByVal actrl As System.Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable = mobjDAF.GetTOrdSlipInfo(rsUsDt)

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("tordslipnmd"))
                Next
            End With

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

            miSelectKey = 1

            cboComGbn.SelectedIndex = 0

            sbInitialize_ErrProvider()
            sbInitialize_Control()
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbInitialize_Control(Optional ByVal riMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try


            If riMode = 0 Then

                Dim dt As DataTable = mobjDAF.fnGet_SPCList

                If dt.Rows.Count > 0 Then
                    Me.cboSpcCd.Items.Clear()
                    Me.cboDSpcNm1.Items.Clear()
                    For intIdx As Integer = 0 To dt.Rows.Count - 1
                        Me.cboSpcCd.Items.Add(dt.Rows(intIdx).Item("spcinfo").ToString)
                        Me.cboDSpcNm1.Items.Add(dt.Rows(intIdx).Item("spcinfo").ToString)
                    Next
                End If

                Me.txtComCd.Text = "" : Me.cboSpcCd.SelectedIndex = -1 : Me.btnUE.Visible = False

                Me.txtComNm.Text = "" : Me.txtComNmD.Text = "" : Me.txtComNmP.Text = "" : Me.txtComNmS.Text = ""

                Me.cboComGbn.SelectedIndex = -1 : Me.cboOrdSlip.SelectedIndex = -1 : Me.txtDispseqO.Text = "" : Me.chkOrdHIde.Checked = False

                Me.txtTOrdCd.Text = "" : Me.cboDSpcNm1.SelectedIndex = -1 : Me.chkGbnO.Checked = False : Me.chkGbnI.Checked = False

                Me.txtSugaCd.Text = "" : Me.chkEmerGbn.Checked = False : Me.chkPTGbn.Checked = False

                Me.chkExeDay1.Checked = False : Me.chkExeDay2.Checked = False : Me.chkExeDay3.Checked = False : Me.chkExeDay4.Checked = False
                Me.chkExeDay5.Checked = False : Me.chkExeDay6.Checked = False : Me.chkExeDay7.Checked = False

                Me.chkOReqItem1.Checked = False : Me.chkOReqItem2.Checked = False : Me.chkOReqItem3.Checked = False

                Me.cboOWarningGbn.SelectedIndex = -1 : txtOWarning.Text = "" : cboCrossLevel.SelectedIndex = -1
                Me.cboOWarningGbn_SelectedIndexChanged(Nothing, Nothing)

                Me.rdo400.Checked = True : Me.rod320.Checked = False : Me.rdoNot.Checked = False

                Me.cboAvailMi.SelectedIndex = -1 : Me.cboFTCD.SelectedIndex = -1 : Me.cboPSComCd.SelectedIndex = -1
                Me.cboGOrdCd.SelectedIndex = -1
                Me.chkBagOrdYn.Checked = False

                Me.txtBldCd.Text = "" : Me.txtDispseqL.Text = "" : Me.txtCLisCd.Text = ""

                Me.txtUSDT.Text = "" : Me.txtUEDT.Text = "" : Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""

            ElseIf riMode = 1 Then


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

    Private Sub sbInitialize_ErrProvider()
        Dim sFn As String = "Sub sbInitialize_ErrProvider()"

        Try
            errpd.Dispose()             ' component 에서 사용하는 모드 리소스를 해제한다.
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Public Sub sbSetNewUSDT()
        Dim sFn As String = "Public Sub sbSetNewUSDT()"

        Try
            Dim sDate As String = fnGetSystemDT()
            sDate = sDate.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")

#If DEBUG Then
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 0, CType(sDate, Date)), "yyyy-MM-dd 00:00:00")
#Else
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 1, CType(sDate, Date)), "yyyy-MM-dd 00:00:00")
#End If
            miSelectKey = 1

            txtUSDay.Text = sSysDT.Substring(0, 10)
            dtpUSDay.Value = CType(sSysDT, Date)
            dtpUSTime.Value = CType(sSysDT, Date)

            '신규 시작일시에 맞는 CdList를 불러옴
            sbDisplayCdList_Ref(sSysDT.Replace("-", "").Replace(":", ""))
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
    Friend WithEvents tbcPage As System.Windows.Forms.TabPage
    Friend WithEvents grpTop As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents lblComCd As System.Windows.Forms.Label
    Friend WithEvents lblLine1 As System.Windows.Forms.Label
    Friend WithEvents lblBloodVol As System.Windows.Forms.Label
    Friend WithEvents lblSeqTMi As System.Windows.Forms.Label
    Friend WithEvents lblDispseqL As System.Windows.Forms.Label
    Friend WithEvents lblBldCd As System.Windows.Forms.Label
    Friend WithEvents lblFTCD As System.Windows.Forms.Label
    Friend WithEvents lblPSComCd As System.Windows.Forms.Label
    Friend WithEvents txtComNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtComNm As System.Windows.Forms.TextBox
    Friend WithEvents txtComCd As System.Windows.Forms.TextBox
    Friend WithEvents lblComNmS As System.Windows.Forms.Label
    Friend WithEvents lblComNm As System.Windows.Forms.Label
    Friend WithEvents txtDispseqL As System.Windows.Forms.TextBox
    Friend WithEvents txtBldCd As System.Windows.Forms.TextBox
    Friend WithEvents cboFTCD As System.Windows.Forms.ComboBox
    Friend WithEvents cboPSComCd As System.Windows.Forms.ComboBox
    Friend WithEvents rdo400 As System.Windows.Forms.RadioButton
    Friend WithEvents rod320 As System.Windows.Forms.RadioButton
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents cboAvailMi As System.Windows.Forms.ComboBox
    Friend WithEvents lblAvailMi As System.Windows.Forms.Label
    Friend WithEvents txtComNmP As System.Windows.Forms.TextBox
    Friend WithEvents lblComNmP As System.Windows.Forms.Label
    Friend WithEvents txtComNmD As System.Windows.Forms.TextBox
    Friend WithEvents lblComNmD As System.Windows.Forms.Label
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents cboSpcCd As System.Windows.Forms.ComboBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblIOGbn As System.Windows.Forms.Label
    Friend WithEvents cboDSpcNm1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblDSpc As System.Windows.Forms.Label
    Friend WithEvents cboOrdSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblOrdSlip As System.Windows.Forms.Label
    Friend WithEvents chkGbnI As System.Windows.Forms.CheckBox
    Friend WithEvents chkGbnO As System.Windows.Forms.CheckBox
    Friend WithEvents lblDispseqO As System.Windows.Forms.Label
    Friend WithEvents txtTOrdCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTOrdCd As System.Windows.Forms.Label
    Friend WithEvents lblORGbn As System.Windows.Forms.Label
    Friend WithEvents chkPTGbn As System.Windows.Forms.CheckBox
    Friend WithEvents chkEmerGbn As System.Windows.Forms.CheckBox
    Friend WithEvents lblSuga As System.Windows.Forms.Label
    Friend WithEvents chkOReqItem4 As System.Windows.Forms.CheckBox
    Friend WithEvents lblOReqItem As System.Windows.Forms.Label
    Friend WithEvents chkOReqItem3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkOReqItem1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkOReqItem2 As System.Windows.Forms.CheckBox
    Friend WithEvents cboOWarningGbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtOWarning As System.Windows.Forms.TextBox
    Friend WithEvents lblOWarning As System.Windows.Forms.Label
    Friend WithEvents btnExeDay As System.Windows.Forms.Button
    Friend WithEvents chkExeDay7 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay1 As System.Windows.Forms.CheckBox
    Friend WithEvents lblExeDay As System.Windows.Forms.Label
    Friend WithEvents chkOrdHIde As System.Windows.Forms.CheckBox
    Friend WithEvents lblLine2 As System.Windows.Forms.Label
    Friend WithEvents tclCom As System.Windows.Forms.TabControl
    Friend WithEvents txtDispseqO As System.Windows.Forms.TextBox
    Friend WithEvents txtSugaCd As System.Windows.Forms.TextBox
    Friend WithEvents rdoNot As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tclCom = New System.Windows.Forms.TabControl()
        Me.tbcPage = New System.Windows.Forms.TabPage()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.txtUEDT = New System.Windows.Forms.TextBox()
        Me.lblUEDT = New System.Windows.Forms.Label()
        Me.txtRegDT = New System.Windows.Forms.TextBox()
        Me.txtUSDT = New System.Windows.Forms.TextBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblRegDT = New System.Windows.Forms.Label()
        Me.lblUSDT = New System.Windows.Forms.Label()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboOReqItem = New System.Windows.Forms.ComboBox()
        Me.txtCLisCd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboCrossLevel = New System.Windows.Forms.ComboBox()
        Me.lblCrossLevel = New System.Windows.Forms.Label()
        Me.chkBagOrdYn = New System.Windows.Forms.CheckBox()
        Me.cboGOrdCd = New System.Windows.Forms.ComboBox()
        Me.lblGOrdCd = New System.Windows.Forms.Label()
        Me.cboComGbn = New System.Windows.Forms.ComboBox()
        Me.lblComGbn = New System.Windows.Forms.Label()
        Me.rdoNot = New System.Windows.Forms.RadioButton()
        Me.lblLine2 = New System.Windows.Forms.Label()
        Me.chkOrdHIde = New System.Windows.Forms.CheckBox()
        Me.chkOReqItem4 = New System.Windows.Forms.CheckBox()
        Me.lblOReqItem = New System.Windows.Forms.Label()
        Me.chkOReqItem3 = New System.Windows.Forms.CheckBox()
        Me.chkOReqItem1 = New System.Windows.Forms.CheckBox()
        Me.chkOReqItem2 = New System.Windows.Forms.CheckBox()
        Me.cboOWarningGbn = New System.Windows.Forms.ComboBox()
        Me.txtOWarning = New System.Windows.Forms.TextBox()
        Me.lblOWarning = New System.Windows.Forms.Label()
        Me.btnExeDay = New System.Windows.Forms.Button()
        Me.chkExeDay7 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay6 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay5 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay4 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay3 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay2 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay1 = New System.Windows.Forms.CheckBox()
        Me.lblExeDay = New System.Windows.Forms.Label()
        Me.lblORGbn = New System.Windows.Forms.Label()
        Me.chkPTGbn = New System.Windows.Forms.CheckBox()
        Me.chkEmerGbn = New System.Windows.Forms.CheckBox()
        Me.txtSugaCd = New System.Windows.Forms.TextBox()
        Me.lblSuga = New System.Windows.Forms.Label()
        Me.lblIOGbn = New System.Windows.Forms.Label()
        Me.cboDSpcNm1 = New System.Windows.Forms.ComboBox()
        Me.lblDSpc = New System.Windows.Forms.Label()
        Me.cboOrdSlip = New System.Windows.Forms.ComboBox()
        Me.lblOrdSlip = New System.Windows.Forms.Label()
        Me.chkGbnI = New System.Windows.Forms.CheckBox()
        Me.chkGbnO = New System.Windows.Forms.CheckBox()
        Me.txtDispseqO = New System.Windows.Forms.TextBox()
        Me.lblDispseqO = New System.Windows.Forms.Label()
        Me.txtTOrdCd = New System.Windows.Forms.TextBox()
        Me.lblTOrdCd = New System.Windows.Forms.Label()
        Me.lblAvailMi = New System.Windows.Forms.Label()
        Me.cboAvailMi = New System.Windows.Forms.ComboBox()
        Me.cboPSComCd = New System.Windows.Forms.ComboBox()
        Me.lblPSComCd = New System.Windows.Forms.Label()
        Me.cboFTCD = New System.Windows.Forms.ComboBox()
        Me.lblFTCD = New System.Windows.Forms.Label()
        Me.txtBldCd = New System.Windows.Forms.TextBox()
        Me.lblBldCd = New System.Windows.Forms.Label()
        Me.rod320 = New System.Windows.Forms.RadioButton()
        Me.rdo400 = New System.Windows.Forms.RadioButton()
        Me.txtDispseqL = New System.Windows.Forms.TextBox()
        Me.lblDispseqL = New System.Windows.Forms.Label()
        Me.lblSeqTMi = New System.Windows.Forms.Label()
        Me.lblBloodVol = New System.Windows.Forms.Label()
        Me.lblLine1 = New System.Windows.Forms.Label()
        Me.lblComNmS = New System.Windows.Forms.Label()
        Me.txtComNmS = New System.Windows.Forms.TextBox()
        Me.lblComNmP = New System.Windows.Forms.Label()
        Me.lblComNmD = New System.Windows.Forms.Label()
        Me.txtComNmD = New System.Windows.Forms.TextBox()
        Me.lblComNm = New System.Windows.Forms.Label()
        Me.txtComNmP = New System.Windows.Forms.TextBox()
        Me.txtComNm = New System.Windows.Forms.TextBox()
        Me.grpTop = New System.Windows.Forms.GroupBox()
        Me.cboSpcCd = New System.Windows.Forms.ComboBox()
        Me.lblSpcCd = New System.Windows.Forms.Label()
        Me.txtComCd = New System.Windows.Forms.TextBox()
        Me.lblComCd = New System.Windows.Forms.Label()
        Me.btnUE = New System.Windows.Forms.Button()
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker()
        Me.txtUSDay = New System.Windows.Forms.TextBox()
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker()
        Me.lblUSDayTime = New System.Windows.Forms.Label()
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.txtBldCd2 = New System.Windows.Forms.TextBox()
        Me.lblBldCd2 = New System.Windows.Forms.Label()
        Me.tclCom.SuspendLayout()
        Me.tbcPage.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpTop.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'tclCom
        '
        Me.tclCom.Controls.Add(Me.tbcPage)
        Me.tclCom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tclCom.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tclCom.Location = New System.Drawing.Point(0, 0)
        Me.tclCom.Name = "tclCom"
        Me.tclCom.SelectedIndex = 0
        Me.tclCom.Size = New System.Drawing.Size(788, 601)
        Me.tclCom.TabIndex = 0
        '
        'tbcPage
        '
        Me.tbcPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcPage.Controls.Add(Me.txtRegNm)
        Me.tbcPage.Controls.Add(Me.txtUEDT)
        Me.tbcPage.Controls.Add(Me.lblUEDT)
        Me.tbcPage.Controls.Add(Me.txtRegDT)
        Me.tbcPage.Controls.Add(Me.txtUSDT)
        Me.tbcPage.Controls.Add(Me.lblUserNm)
        Me.tbcPage.Controls.Add(Me.lblRegDT)
        Me.tbcPage.Controls.Add(Me.lblUSDT)
        Me.tbcPage.Controls.Add(Me.txtRegID)
        Me.tbcPage.Controls.Add(Me.GroupBox2)
        Me.tbcPage.Controls.Add(Me.grpTop)
        Me.tbcPage.Location = New System.Drawing.Point(4, 22)
        Me.tbcPage.Name = "tbcPage"
        Me.tbcPage.Size = New System.Drawing.Size(780, 575)
        Me.tbcPage.TabIndex = 0
        Me.tbcPage.Text = "성분제제정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(705, 545)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 189
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUEDT.Location = New System.Drawing.Point(317, 544)
        Me.txtUEDT.Name = "txtUEDT"
        Me.txtUEDT.ReadOnly = True
        Me.txtUEDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUEDT.TabIndex = 17
        Me.txtUEDT.TabStop = False
        Me.txtUEDT.Tag = "UEDT"
        '
        'lblUEDT
        '
        Me.lblUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUEDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Location = New System.Drawing.Point(219, 544)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUEDT.TabIndex = 16
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
        Me.txtRegDT.Location = New System.Drawing.Point(513, 544)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 19
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'txtUSDT
        '
        Me.txtUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUSDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUSDT.Location = New System.Drawing.Point(109, 544)
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUSDT.TabIndex = 18
        Me.txtUSDT.TabStop = False
        Me.txtUSDT.Tag = "USDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(619, 545)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(85, 21)
        Me.lblUserNm.TabIndex = 13
        Me.lblUserNm.Text = "최종등록자ID"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(427, 544)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(85, 21)
        Me.lblRegDT.TabIndex = 12
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUSDT
        '
        Me.lblUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDT.ForeColor = System.Drawing.Color.Black
        Me.lblUSDT.Location = New System.Drawing.Point(11, 544)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUSDT.TabIndex = 15
        Me.lblUSDT.Text = "시작일시(선택)"
        Me.lblUSDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(705, 545)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 14
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.txtBldCd2)
        Me.GroupBox2.Controls.Add(Me.lblBldCd2)
        Me.GroupBox2.Controls.Add(Me.cboOReqItem)
        Me.GroupBox2.Controls.Add(Me.txtCLisCd)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cboCrossLevel)
        Me.GroupBox2.Controls.Add(Me.lblCrossLevel)
        Me.GroupBox2.Controls.Add(Me.chkBagOrdYn)
        Me.GroupBox2.Controls.Add(Me.cboGOrdCd)
        Me.GroupBox2.Controls.Add(Me.lblGOrdCd)
        Me.GroupBox2.Controls.Add(Me.cboComGbn)
        Me.GroupBox2.Controls.Add(Me.lblComGbn)
        Me.GroupBox2.Controls.Add(Me.rdoNot)
        Me.GroupBox2.Controls.Add(Me.lblLine2)
        Me.GroupBox2.Controls.Add(Me.chkOrdHIde)
        Me.GroupBox2.Controls.Add(Me.chkOReqItem4)
        Me.GroupBox2.Controls.Add(Me.lblOReqItem)
        Me.GroupBox2.Controls.Add(Me.chkOReqItem3)
        Me.GroupBox2.Controls.Add(Me.chkOReqItem1)
        Me.GroupBox2.Controls.Add(Me.chkOReqItem2)
        Me.GroupBox2.Controls.Add(Me.cboOWarningGbn)
        Me.GroupBox2.Controls.Add(Me.txtOWarning)
        Me.GroupBox2.Controls.Add(Me.lblOWarning)
        Me.GroupBox2.Controls.Add(Me.btnExeDay)
        Me.GroupBox2.Controls.Add(Me.chkExeDay7)
        Me.GroupBox2.Controls.Add(Me.chkExeDay6)
        Me.GroupBox2.Controls.Add(Me.chkExeDay5)
        Me.GroupBox2.Controls.Add(Me.chkExeDay4)
        Me.GroupBox2.Controls.Add(Me.chkExeDay3)
        Me.GroupBox2.Controls.Add(Me.chkExeDay2)
        Me.GroupBox2.Controls.Add(Me.chkExeDay1)
        Me.GroupBox2.Controls.Add(Me.lblExeDay)
        Me.GroupBox2.Controls.Add(Me.lblORGbn)
        Me.GroupBox2.Controls.Add(Me.chkPTGbn)
        Me.GroupBox2.Controls.Add(Me.chkEmerGbn)
        Me.GroupBox2.Controls.Add(Me.txtSugaCd)
        Me.GroupBox2.Controls.Add(Me.lblSuga)
        Me.GroupBox2.Controls.Add(Me.lblIOGbn)
        Me.GroupBox2.Controls.Add(Me.cboDSpcNm1)
        Me.GroupBox2.Controls.Add(Me.lblDSpc)
        Me.GroupBox2.Controls.Add(Me.cboOrdSlip)
        Me.GroupBox2.Controls.Add(Me.lblOrdSlip)
        Me.GroupBox2.Controls.Add(Me.chkGbnI)
        Me.GroupBox2.Controls.Add(Me.chkGbnO)
        Me.GroupBox2.Controls.Add(Me.txtDispseqO)
        Me.GroupBox2.Controls.Add(Me.lblDispseqO)
        Me.GroupBox2.Controls.Add(Me.txtTOrdCd)
        Me.GroupBox2.Controls.Add(Me.lblTOrdCd)
        Me.GroupBox2.Controls.Add(Me.lblAvailMi)
        Me.GroupBox2.Controls.Add(Me.cboAvailMi)
        Me.GroupBox2.Controls.Add(Me.cboPSComCd)
        Me.GroupBox2.Controls.Add(Me.lblPSComCd)
        Me.GroupBox2.Controls.Add(Me.cboFTCD)
        Me.GroupBox2.Controls.Add(Me.lblFTCD)
        Me.GroupBox2.Controls.Add(Me.txtBldCd)
        Me.GroupBox2.Controls.Add(Me.lblBldCd)
        Me.GroupBox2.Controls.Add(Me.rod320)
        Me.GroupBox2.Controls.Add(Me.rdo400)
        Me.GroupBox2.Controls.Add(Me.txtDispseqL)
        Me.GroupBox2.Controls.Add(Me.lblDispseqL)
        Me.GroupBox2.Controls.Add(Me.lblSeqTMi)
        Me.GroupBox2.Controls.Add(Me.lblBloodVol)
        Me.GroupBox2.Controls.Add(Me.lblLine1)
        Me.GroupBox2.Controls.Add(Me.lblComNmS)
        Me.GroupBox2.Controls.Add(Me.txtComNmS)
        Me.GroupBox2.Controls.Add(Me.lblComNmP)
        Me.GroupBox2.Controls.Add(Me.lblComNmD)
        Me.GroupBox2.Controls.Add(Me.txtComNmD)
        Me.GroupBox2.Controls.Add(Me.lblComNm)
        Me.GroupBox2.Controls.Add(Me.txtComNmP)
        Me.GroupBox2.Controls.Add(Me.txtComNm)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 92)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(768, 433)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "성분제제정보"
        '
        'cboOReqItem
        '
        Me.cboOReqItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOReqItem.Items.AddRange(New Object() {"[1] 없음", "[A] 적혈구 제제", "[B] 혈소판 제제"})
        Me.cboOReqItem.Location = New System.Drawing.Point(359, 159)
        Me.cboOReqItem.Name = "cboOReqItem"
        Me.cboOReqItem.Size = New System.Drawing.Size(139, 20)
        Me.cboOReqItem.TabIndex = 153
        Me.cboOReqItem.Tag = "OREQITEM4GBN_01"
        '
        'txtCLisCd
        '
        Me.txtCLisCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCLisCd.Location = New System.Drawing.Point(515, 227)
        Me.txtCLisCd.MaxLength = 20
        Me.txtCLisCd.Name = "txtCLisCd"
        Me.txtCLisCd.Size = New System.Drawing.Size(103, 21)
        Me.txtCLisCd.TabIndex = 151
        Me.txtCLisCd.Tag = "COMLISCD"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(375, 227)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(139, 21)
        Me.Label1.TabIndex = 152
        Me.Label1.Text = "성분제제코드(LIS)"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboCrossLevel
        '
        Me.cboCrossLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCrossLevel.Items.AddRange(New Object() {"[1] 1차", "[2] 2차", "[3] 3차", "[4] 4차"})
        Me.cboCrossLevel.Location = New System.Drawing.Point(186, 360)
        Me.cboCrossLevel.Name = "cboCrossLevel"
        Me.cboCrossLevel.Size = New System.Drawing.Size(162, 20)
        Me.cboCrossLevel.TabIndex = 44
        Me.cboCrossLevel.Tag = "CROSSLEVEL_01"
        '
        'lblCrossLevel
        '
        Me.lblCrossLevel.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCrossLevel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCrossLevel.ForeColor = System.Drawing.Color.White
        Me.lblCrossLevel.Location = New System.Drawing.Point(8, 359)
        Me.lblCrossLevel.Name = "lblCrossLevel"
        Me.lblCrossLevel.Size = New System.Drawing.Size(177, 21)
        Me.lblCrossLevel.TabIndex = 150
        Me.lblCrossLevel.Text = "Cross Matching 보고범위"
        Me.lblCrossLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkBagOrdYn
        '
        Me.chkBagOrdYn.BackColor = System.Drawing.Color.Transparent
        Me.chkBagOrdYn.Location = New System.Drawing.Point(375, 318)
        Me.chkBagOrdYn.Name = "chkBagOrdYn"
        Me.chkBagOrdYn.Size = New System.Drawing.Size(104, 20)
        Me.chkBagOrdYn.TabIndex = 42
        Me.chkBagOrdYn.Tag = "BAGORDYN"
        Me.chkBagOrdYn.Text = "BAG 처방 여부"
        Me.chkBagOrdYn.UseVisualStyleBackColor = False
        '
        'cboGOrdCd
        '
        Me.cboGOrdCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGOrdCd.Items.AddRange(New Object() {"[1] 혈액준비(Prep)", "[2] 혈액수혈(Tranf)", "[3] 응급수혈(Emer)", "[4] Irradiation"})
        Me.cboGOrdCd.Location = New System.Drawing.Point(120, 339)
        Me.cboGOrdCd.MaxDropDownItems = 10
        Me.cboGOrdCd.Name = "cboGOrdCd"
        Me.cboGOrdCd.Size = New System.Drawing.Size(475, 20)
        Me.cboGOrdCd.TabIndex = 43
        Me.cboGOrdCd.TabStop = False
        Me.cboGOrdCd.Tag = "GORDCD_01"
        '
        'lblGOrdCd
        '
        Me.lblGOrdCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblGOrdCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGOrdCd.ForeColor = System.Drawing.Color.White
        Me.lblGOrdCd.Location = New System.Drawing.Point(8, 338)
        Me.lblGOrdCd.Name = "lblGOrdCd"
        Me.lblGOrdCd.Size = New System.Drawing.Size(111, 20)
        Me.lblGOrdCd.TabIndex = 147
        Me.lblGOrdCd.Text = "관련처방코드"
        Me.lblGOrdCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboComGbn
        '
        Me.cboComGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComGbn.Items.AddRange(New Object() {"[1] 혈액준비(Prep)", "[2] 혈액수혈(Tranf)", "[3] 응급수혈(Emer)", "[4] Irradiation"})
        Me.cboComGbn.Location = New System.Drawing.Point(120, 317)
        Me.cboComGbn.MaxDropDownItems = 10
        Me.cboComGbn.Name = "cboComGbn"
        Me.cboComGbn.Size = New System.Drawing.Size(228, 20)
        Me.cboComGbn.TabIndex = 41
        Me.cboComGbn.TabStop = False
        Me.cboComGbn.Tag = "COMGBN_01"
        '
        'lblComGbn
        '
        Me.lblComGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComGbn.ForeColor = System.Drawing.Color.White
        Me.lblComGbn.Location = New System.Drawing.Point(8, 317)
        Me.lblComGbn.Name = "lblComGbn"
        Me.lblComGbn.Size = New System.Drawing.Size(111, 20)
        Me.lblComGbn.TabIndex = 145
        Me.lblComGbn.Text = "성분제제구분"
        Me.lblComGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdoNot
        '
        Me.rdoNot.BackColor = System.Drawing.Color.Transparent
        Me.rdoNot.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoNot.Location = New System.Drawing.Point(265, 229)
        Me.rdoNot.Name = "rdoNot"
        Me.rdoNot.Size = New System.Drawing.Size(68, 20)
        Me.rdoNot.TabIndex = 35
        Me.rdoNot.Tag = "DONQNT2"
        Me.rdoNot.Text = "미정"
        Me.rdoNot.UseVisualStyleBackColor = False
        '
        'lblLine2
        '
        Me.lblLine2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine2.Location = New System.Drawing.Point(6, 218)
        Me.lblLine2.Name = "lblLine2"
        Me.lblLine2.Size = New System.Drawing.Size(756, 2)
        Me.lblLine2.TabIndex = 143
        '
        'chkOrdHIde
        '
        Me.chkOrdHIde.BackColor = System.Drawing.Color.Transparent
        Me.chkOrdHIde.Location = New System.Drawing.Point(646, 94)
        Me.chkOrdHIde.Name = "chkOrdHIde"
        Me.chkOrdHIde.Size = New System.Drawing.Size(116, 20)
        Me.chkOrdHIde.TabIndex = 13
        Me.chkOrdHIde.Tag = "ORDHIDE"
        Me.chkOrdHIde.Text = "검사처방 미사용"
        Me.chkOrdHIde.UseVisualStyleBackColor = False
        '
        'chkOReqItem4
        '
        Me.chkOReqItem4.AutoSize = True
        Me.chkOReqItem4.BackColor = System.Drawing.Color.Transparent
        Me.chkOReqItem4.Location = New System.Drawing.Point(305, 161)
        Me.chkOReqItem4.Name = "chkOReqItem4"
        Me.chkOReqItem4.Size = New System.Drawing.Size(48, 16)
        Me.chkOReqItem4.TabIndex = 23
        Me.chkOReqItem4.Tag = "OREQITEM4"
        Me.chkOReqItem4.Text = "사유"
        Me.chkOReqItem4.UseVisualStyleBackColor = False
        '
        'lblOReqItem
        '
        Me.lblOReqItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOReqItem.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOReqItem.ForeColor = System.Drawing.Color.White
        Me.lblOReqItem.Location = New System.Drawing.Point(8, 159)
        Me.lblOReqItem.Name = "lblOReqItem"
        Me.lblOReqItem.Size = New System.Drawing.Size(111, 21)
        Me.lblOReqItem.TabIndex = 140
        Me.lblOReqItem.Text = "처방입력사항"
        Me.lblOReqItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkOReqItem3
        '
        Me.chkOReqItem3.AutoSize = True
        Me.chkOReqItem3.BackColor = System.Drawing.Color.Transparent
        Me.chkOReqItem3.Location = New System.Drawing.Point(245, 161)
        Me.chkOReqItem3.Name = "chkOReqItem3"
        Me.chkOReqItem3.Size = New System.Drawing.Size(48, 16)
        Me.chkOReqItem3.TabIndex = 22
        Me.chkOReqItem3.Tag = "OREQITEM3"
        Me.chkOReqItem3.Text = "체중"
        Me.chkOReqItem3.UseVisualStyleBackColor = False
        '
        'chkOReqItem1
        '
        Me.chkOReqItem1.AutoSize = True
        Me.chkOReqItem1.BackColor = System.Drawing.Color.Transparent
        Me.chkOReqItem1.Location = New System.Drawing.Point(125, 161)
        Me.chkOReqItem1.Name = "chkOReqItem1"
        Me.chkOReqItem1.Size = New System.Drawing.Size(48, 16)
        Me.chkOReqItem1.TabIndex = 20
        Me.chkOReqItem1.Tag = "OREQITEM1"
        Me.chkOReqItem1.Text = "수량"
        Me.chkOReqItem1.UseVisualStyleBackColor = False
        '
        'chkOReqItem2
        '
        Me.chkOReqItem2.AutoSize = True
        Me.chkOReqItem2.BackColor = System.Drawing.Color.Transparent
        Me.chkOReqItem2.Location = New System.Drawing.Point(186, 161)
        Me.chkOReqItem2.Name = "chkOReqItem2"
        Me.chkOReqItem2.Size = New System.Drawing.Size(48, 16)
        Me.chkOReqItem2.TabIndex = 21
        Me.chkOReqItem2.Tag = "OREQITEM2"
        Me.chkOReqItem2.Text = "신장"
        Me.chkOReqItem2.UseVisualStyleBackColor = False
        '
        'cboOWarningGbn
        '
        Me.cboOWarningGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOWarningGbn.Items.AddRange(New Object() {"[0] : 없음", "[1] : 일반", "[2] : 팝업"})
        Me.cboOWarningGbn.Location = New System.Drawing.Point(605, 137)
        Me.cboOWarningGbn.Name = "cboOWarningGbn"
        Me.cboOWarningGbn.Size = New System.Drawing.Size(153, 20)
        Me.cboOWarningGbn.TabIndex = 31
        Me.cboOWarningGbn.Tag = "OWARNINGGBN_01"
        '
        'txtOWarning
        '
        Me.txtOWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOWarning.Location = New System.Drawing.Point(512, 159)
        Me.txtOWarning.MaxLength = 100
        Me.txtOWarning.Multiline = True
        Me.txtOWarning.Name = "txtOWarning"
        Me.txtOWarning.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOWarning.Size = New System.Drawing.Size(245, 47)
        Me.txtOWarning.TabIndex = 32
        Me.txtOWarning.Tag = "OWARNING"
        '
        'lblOWarning
        '
        Me.lblOWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOWarning.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOWarning.ForeColor = System.Drawing.Color.White
        Me.lblOWarning.Location = New System.Drawing.Point(512, 137)
        Me.lblOWarning.Name = "lblOWarning"
        Me.lblOWarning.Size = New System.Drawing.Size(92, 21)
        Me.lblOWarning.TabIndex = 134
        Me.lblOWarning.Text = "처방주의사항"
        Me.lblOWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnExeDay
        '
        Me.btnExeDay.BackColor = System.Drawing.SystemColors.Control
        Me.btnExeDay.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExeDay.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExeDay.Location = New System.Drawing.Point(121, 181)
        Me.btnExeDay.Name = "btnExeDay"
        Me.btnExeDay.Size = New System.Drawing.Size(48, 22)
        Me.btnExeDay.TabIndex = 14
        Me.btnExeDay.TabStop = False
        Me.btnExeDay.Text = "매일"
        Me.btnExeDay.UseVisualStyleBackColor = False
        '
        'chkExeDay7
        '
        Me.chkExeDay7.Location = New System.Drawing.Point(377, 181)
        Me.chkExeDay7.Name = "chkExeDay7"
        Me.chkExeDay7.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay7.TabIndex = 30
        Me.chkExeDay7.Tag = "EXEDAY7"
        Me.chkExeDay7.Text = "일"
        '
        'chkExeDay6
        '
        Me.chkExeDay6.Location = New System.Drawing.Point(341, 181)
        Me.chkExeDay6.Name = "chkExeDay6"
        Me.chkExeDay6.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay6.TabIndex = 29
        Me.chkExeDay6.Tag = "EXEDAY6"
        Me.chkExeDay6.Text = "토"
        '
        'chkExeDay5
        '
        Me.chkExeDay5.Location = New System.Drawing.Point(309, 181)
        Me.chkExeDay5.Name = "chkExeDay5"
        Me.chkExeDay5.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay5.TabIndex = 28
        Me.chkExeDay5.Tag = "EXEDAY5"
        Me.chkExeDay5.Text = "금"
        '
        'chkExeDay4
        '
        Me.chkExeDay4.Location = New System.Drawing.Point(273, 181)
        Me.chkExeDay4.Name = "chkExeDay4"
        Me.chkExeDay4.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay4.TabIndex = 27
        Me.chkExeDay4.Tag = "EXEDAY4"
        Me.chkExeDay4.Text = "목"
        '
        'chkExeDay3
        '
        Me.chkExeDay3.Location = New System.Drawing.Point(241, 181)
        Me.chkExeDay3.Name = "chkExeDay3"
        Me.chkExeDay3.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay3.TabIndex = 26
        Me.chkExeDay3.Tag = "EXEDAY3"
        Me.chkExeDay3.Text = "수"
        '
        'chkExeDay2
        '
        Me.chkExeDay2.Location = New System.Drawing.Point(205, 181)
        Me.chkExeDay2.Name = "chkExeDay2"
        Me.chkExeDay2.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay2.TabIndex = 26
        Me.chkExeDay2.Tag = "EXEDAY2"
        Me.chkExeDay2.Text = "화"
        '
        'chkExeDay1
        '
        Me.chkExeDay1.Location = New System.Drawing.Point(173, 181)
        Me.chkExeDay1.Name = "chkExeDay1"
        Me.chkExeDay1.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay1.TabIndex = 25
        Me.chkExeDay1.Tag = "EXEDAY1"
        Me.chkExeDay1.Text = "월"
        '
        'lblExeDay
        '
        Me.lblExeDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblExeDay.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExeDay.ForeColor = System.Drawing.Color.White
        Me.lblExeDay.Location = New System.Drawing.Point(8, 181)
        Me.lblExeDay.Name = "lblExeDay"
        Me.lblExeDay.Size = New System.Drawing.Size(111, 21)
        Me.lblExeDay.TabIndex = 125
        Me.lblExeDay.Text = "실시요일"
        Me.lblExeDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblORGbn
        '
        Me.lblORGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblORGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblORGbn.ForeColor = System.Drawing.Color.White
        Me.lblORGbn.Location = New System.Drawing.Point(8, 137)
        Me.lblORGbn.Name = "lblORGbn"
        Me.lblORGbn.Size = New System.Drawing.Size(111, 21)
        Me.lblORGbn.TabIndex = 120
        Me.lblORGbn.Text = "검사처방설정"
        Me.lblORGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkPTGbn
        '
        Me.chkPTGbn.AutoSize = True
        Me.chkPTGbn.BackColor = System.Drawing.Color.Transparent
        Me.chkPTGbn.Location = New System.Drawing.Point(245, 139)
        Me.chkPTGbn.Name = "chkPTGbn"
        Me.chkPTGbn.Size = New System.Drawing.Size(126, 16)
        Me.chkPTGbn.TabIndex = 19
        Me.chkPTGbn.Tag = "PEDGBN"
        Me.chkPTGbn.Text = "신생아검사로 설정"
        Me.chkPTGbn.UseVisualStyleBackColor = False
        '
        'chkEmerGbn
        '
        Me.chkEmerGbn.AutoSize = True
        Me.chkEmerGbn.BackColor = System.Drawing.Color.Transparent
        Me.chkEmerGbn.Location = New System.Drawing.Point(125, 139)
        Me.chkEmerGbn.Name = "chkEmerGbn"
        Me.chkEmerGbn.Size = New System.Drawing.Size(114, 16)
        Me.chkEmerGbn.TabIndex = 18
        Me.chkEmerGbn.Tag = "EMERGBN"
        Me.chkEmerGbn.Text = "응급검사로 설정"
        Me.chkEmerGbn.UseVisualStyleBackColor = False
        '
        'txtSugaCd
        '
        Me.txtSugaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSugaCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSugaCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSugaCd.Location = New System.Drawing.Point(120, 115)
        Me.txtSugaCd.MaxLength = 20
        Me.txtSugaCd.Name = "txtSugaCd"
        Me.txtSugaCd.Size = New System.Drawing.Size(75, 21)
        Me.txtSugaCd.TabIndex = 14
        Me.txtSugaCd.Tag = "SUGACD"
        '
        'lblSuga
        '
        Me.lblSuga.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSuga.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSuga.ForeColor = System.Drawing.Color.White
        Me.lblSuga.Location = New System.Drawing.Point(8, 115)
        Me.lblSuga.Name = "lblSuga"
        Me.lblSuga.Size = New System.Drawing.Size(111, 21)
        Me.lblSuga.TabIndex = 114
        Me.lblSuga.Text = "수가코드"
        Me.lblSuga.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIOGbn
        '
        Me.lblIOGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIOGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIOGbn.ForeColor = System.Drawing.Color.White
        Me.lblIOGbn.Location = New System.Drawing.Point(512, 115)
        Me.lblIOGbn.Name = "lblIOGbn"
        Me.lblIOGbn.Size = New System.Drawing.Size(92, 21)
        Me.lblIOGbn.TabIndex = 113
        Me.lblIOGbn.Text = "검사처방조건"
        Me.lblIOGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboDSpcNm1
        '
        Me.cboDSpcNm1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDSpcNm1.Items.AddRange(New Object() {"[091] 혈액준비(Prep)", "[001] 혈액수혈(Tranf)", "[093] 응급수혈(Emer)", "[094] Irradiation"})
        Me.cboDSpcNm1.Location = New System.Drawing.Point(314, 93)
        Me.cboDSpcNm1.MaxDropDownItems = 10
        Me.cboDSpcNm1.Name = "cboDSpcNm1"
        Me.cboDSpcNm1.Size = New System.Drawing.Size(185, 20)
        Me.cboDSpcNm1.TabIndex = 11
        Me.cboDSpcNm1.TabStop = False
        Me.cboDSpcNm1.Tag = "DSPCCD1_01"
        '
        'lblDSpc
        '
        Me.lblDSpc.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDSpc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDSpc.ForeColor = System.Drawing.Color.White
        Me.lblDSpc.Location = New System.Drawing.Point(202, 93)
        Me.lblDSpc.Name = "lblDSpc"
        Me.lblDSpc.Size = New System.Drawing.Size(111, 21)
        Me.lblDSpc.TabIndex = 102
        Me.lblDSpc.Text = "기본처방검체"
        Me.lblDSpc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboOrdSlip
        '
        Me.cboOrdSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrdSlip.Location = New System.Drawing.Point(314, 116)
        Me.cboOrdSlip.MaxDropDownItems = 10
        Me.cboOrdSlip.Name = "cboOrdSlip"
        Me.cboOrdSlip.Size = New System.Drawing.Size(185, 20)
        Me.cboOrdSlip.TabIndex = 15
        Me.cboOrdSlip.Tag = "ORDSLIP_01"
        '
        'lblOrdSlip
        '
        Me.lblOrdSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOrdSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdSlip.ForeColor = System.Drawing.Color.White
        Me.lblOrdSlip.Location = New System.Drawing.Point(202, 115)
        Me.lblOrdSlip.Name = "lblOrdSlip"
        Me.lblOrdSlip.Size = New System.Drawing.Size(111, 21)
        Me.lblOrdSlip.TabIndex = 112
        Me.lblOrdSlip.Text = "검사처방슬립"
        Me.lblOrdSlip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkGbnI
        '
        Me.chkGbnI.BackColor = System.Drawing.Color.Transparent
        Me.chkGbnI.ForeColor = System.Drawing.Color.Black
        Me.chkGbnI.Location = New System.Drawing.Point(670, 117)
        Me.chkGbnI.Name = "chkGbnI"
        Me.chkGbnI.Size = New System.Drawing.Size(68, 20)
        Me.chkGbnI.TabIndex = 17
        Me.chkGbnI.Tag = "IOGBN1"
        Me.chkGbnI.Text = "병동"
        Me.chkGbnI.UseVisualStyleBackColor = False
        '
        'chkGbnO
        '
        Me.chkGbnO.BackColor = System.Drawing.Color.Transparent
        Me.chkGbnO.ForeColor = System.Drawing.Color.Black
        Me.chkGbnO.Location = New System.Drawing.Point(609, 117)
        Me.chkGbnO.Name = "chkGbnO"
        Me.chkGbnO.Size = New System.Drawing.Size(55, 20)
        Me.chkGbnO.TabIndex = 16
        Me.chkGbnO.Tag = "IOGBN0"
        Me.chkGbnO.Text = "외래"
        Me.chkGbnO.UseVisualStyleBackColor = False
        '
        'txtDispseqO
        '
        Me.txtDispseqO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispseqO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDispseqO.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDispseqO.Location = New System.Drawing.Point(605, 93)
        Me.txtDispseqO.MaxLength = 3
        Me.txtDispseqO.Name = "txtDispseqO"
        Me.txtDispseqO.Size = New System.Drawing.Size(28, 21)
        Me.txtDispseqO.TabIndex = 12
        Me.txtDispseqO.Tag = "DISPSEQO"
        '
        'lblDispseqO
        '
        Me.lblDispseqO.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDispseqO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDispseqO.ForeColor = System.Drawing.Color.White
        Me.lblDispseqO.Location = New System.Drawing.Point(512, 93)
        Me.lblDispseqO.Name = "lblDispseqO"
        Me.lblDispseqO.Size = New System.Drawing.Size(92, 21)
        Me.lblDispseqO.TabIndex = 101
        Me.lblDispseqO.Text = "검사처방순번"
        Me.lblDispseqO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTOrdCd
        '
        Me.txtTOrdCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTOrdCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTOrdCd.Location = New System.Drawing.Point(120, 93)
        Me.txtTOrdCd.MaxLength = 20
        Me.txtTOrdCd.Name = "txtTOrdCd"
        Me.txtTOrdCd.Size = New System.Drawing.Size(75, 21)
        Me.txtTOrdCd.TabIndex = 10
        Me.txtTOrdCd.Tag = "COMORDCD"
        '
        'lblTOrdCd
        '
        Me.lblTOrdCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTOrdCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTOrdCd.ForeColor = System.Drawing.Color.White
        Me.lblTOrdCd.Location = New System.Drawing.Point(8, 93)
        Me.lblTOrdCd.Name = "lblTOrdCd"
        Me.lblTOrdCd.Size = New System.Drawing.Size(111, 21)
        Me.lblTOrdCd.TabIndex = 100
        Me.lblTOrdCd.Text = "검사처방코드"
        Me.lblTOrdCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAvailMi
        '
        Me.lblAvailMi.Location = New System.Drawing.Point(194, 256)
        Me.lblAvailMi.Name = "lblAvailMi"
        Me.lblAvailMi.Size = New System.Drawing.Size(32, 16)
        Me.lblAvailMi.TabIndex = 93
        Me.lblAvailMi.Text = "(일)"
        '
        'cboAvailMi
        '
        Me.cboAvailMi.Items.AddRange(New Object() {"1", "5", "34", "35", "365"})
        Me.cboAvailMi.Location = New System.Drawing.Point(120, 251)
        Me.cboAvailMi.MaxDropDownItems = 10
        Me.cboAvailMi.MaxLength = 10
        Me.cboAvailMi.Name = "cboAvailMi"
        Me.cboAvailMi.Size = New System.Drawing.Size(72, 20)
        Me.cboAvailMi.TabIndex = 36
        Me.cboAvailMi.Tag = "AVAILDAY"
        '
        'cboPSComCd
        '
        Me.cboPSComCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPSComCd.Location = New System.Drawing.Point(120, 295)
        Me.cboPSComCd.Name = "cboPSComCd"
        Me.cboPSComCd.Size = New System.Drawing.Size(228, 20)
        Me.cboPSComCd.TabIndex = 39
        Me.cboPSComCd.Tag = "PSCOMCD_01"
        '
        'lblPSComCd
        '
        Me.lblPSComCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPSComCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPSComCd.ForeColor = System.Drawing.Color.White
        Me.lblPSComCd.Location = New System.Drawing.Point(8, 295)
        Me.lblPSComCd.Name = "lblPSComCd"
        Me.lblPSComCd.Size = New System.Drawing.Size(111, 21)
        Me.lblPSComCd.TabIndex = 90
        Me.lblPSComCd.Text = "출고성분제제"
        Me.lblPSComCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboFTCD
        '
        Me.cboFTCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFTCD.Location = New System.Drawing.Point(120, 273)
        Me.cboFTCD.Name = "cboFTCD"
        Me.cboFTCD.Size = New System.Drawing.Size(228, 20)
        Me.cboFTCD.TabIndex = 37
        Me.cboFTCD.Tag = "FTCD_01"
        '
        'lblFTCD
        '
        Me.lblFTCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblFTCD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFTCD.ForeColor = System.Drawing.Color.White
        Me.lblFTCD.Location = New System.Drawing.Point(8, 273)
        Me.lblFTCD.Name = "lblFTCD"
        Me.lblFTCD.Size = New System.Drawing.Size(111, 21)
        Me.lblFTCD.TabIndex = 85
        Me.lblFTCD.Text = "필터종류"
        Me.lblFTCD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBldCd
        '
        Me.txtBldCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldCd.Location = New System.Drawing.Point(515, 272)
        Me.txtBldCd.MaxLength = 20
        Me.txtBldCd.Name = "txtBldCd"
        Me.txtBldCd.Size = New System.Drawing.Size(103, 21)
        Me.txtBldCd.TabIndex = 38
        Me.txtBldCd.Tag = "BLDCD"
        '
        'lblBldCd
        '
        Me.lblBldCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBldCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBldCd.ForeColor = System.Drawing.Color.White
        Me.lblBldCd.Location = New System.Drawing.Point(375, 272)
        Me.lblBldCd.Name = "lblBldCd"
        Me.lblBldCd.Size = New System.Drawing.Size(139, 21)
        Me.lblBldCd.TabIndex = 83
        Me.lblBldCd.Text = "혈액코드(혈액원)"
        Me.lblBldCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rod320
        '
        Me.rod320.BackColor = System.Drawing.Color.Transparent
        Me.rod320.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rod320.Location = New System.Drawing.Point(193, 229)
        Me.rod320.Name = "rod320"
        Me.rod320.Size = New System.Drawing.Size(68, 20)
        Me.rod320.TabIndex = 34
        Me.rod320.Tag = "DONQNT1"
        Me.rod320.Text = "320 ml"
        Me.rod320.UseVisualStyleBackColor = False
        '
        'rdo400
        '
        Me.rdo400.BackColor = System.Drawing.Color.Transparent
        Me.rdo400.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdo400.Location = New System.Drawing.Point(121, 229)
        Me.rdo400.Name = "rdo400"
        Me.rdo400.Size = New System.Drawing.Size(68, 21)
        Me.rdo400.TabIndex = 33
        Me.rdo400.Tag = "DONQNT0"
        Me.rdo400.Text = "400 ml"
        Me.rdo400.UseVisualStyleBackColor = False
        '
        'txtDispseqL
        '
        Me.txtDispseqL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispseqL.Location = New System.Drawing.Point(515, 294)
        Me.txtDispseqL.MaxLength = 10
        Me.txtDispseqL.Name = "txtDispseqL"
        Me.txtDispseqL.Size = New System.Drawing.Size(103, 21)
        Me.txtDispseqL.TabIndex = 40
        Me.txtDispseqL.Tag = "DISPSEQL"
        '
        'lblDispseqL
        '
        Me.lblDispseqL.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDispseqL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDispseqL.ForeColor = System.Drawing.Color.White
        Me.lblDispseqL.Location = New System.Drawing.Point(375, 294)
        Me.lblDispseqL.Name = "lblDispseqL"
        Me.lblDispseqL.Size = New System.Drawing.Size(139, 21)
        Me.lblDispseqL.TabIndex = 75
        Me.lblDispseqL.Text = "정렬순서 LIS"
        Me.lblDispseqL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSeqTMi
        '
        Me.lblSeqTMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSeqTMi.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSeqTMi.ForeColor = System.Drawing.Color.White
        Me.lblSeqTMi.Location = New System.Drawing.Point(8, 251)
        Me.lblSeqTMi.Name = "lblSeqTMi"
        Me.lblSeqTMi.Size = New System.Drawing.Size(111, 21)
        Me.lblSeqTMi.TabIndex = 18
        Me.lblSeqTMi.Text = "유효기간 "
        Me.lblSeqTMi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBloodVol
        '
        Me.lblBloodVol.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBloodVol.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBloodVol.ForeColor = System.Drawing.Color.White
        Me.lblBloodVol.Location = New System.Drawing.Point(8, 229)
        Me.lblBloodVol.Name = "lblBloodVol"
        Me.lblBloodVol.Size = New System.Drawing.Size(111, 21)
        Me.lblBloodVol.TabIndex = 15
        Me.lblBloodVol.Text = "혈액용량"
        Me.lblBloodVol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLine1
        '
        Me.lblLine1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine1.Location = New System.Drawing.Point(6, 80)
        Me.lblLine1.Name = "lblLine1"
        Me.lblLine1.Size = New System.Drawing.Size(756, 2)
        Me.lblLine1.TabIndex = 14
        '
        'lblComNmS
        '
        Me.lblComNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComNmS.ForeColor = System.Drawing.Color.White
        Me.lblComNmS.Location = New System.Drawing.Point(384, 44)
        Me.lblComNmS.Name = "lblComNmS"
        Me.lblComNmS.Size = New System.Drawing.Size(111, 21)
        Me.lblComNmS.TabIndex = 13
        Me.lblComNmS.Text = "성분제제명(처방)"
        Me.lblComNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComNmS
        '
        Me.txtComNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtComNmS.Location = New System.Drawing.Point(496, 44)
        Me.txtComNmS.MaxLength = 25
        Me.txtComNmS.Name = "txtComNmS"
        Me.txtComNmS.Size = New System.Drawing.Size(262, 21)
        Me.txtComNmS.TabIndex = 9
        Me.txtComNmS.Tag = "COMNMS"
        '
        'lblComNmP
        '
        Me.lblComNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComNmP.ForeColor = System.Drawing.Color.White
        Me.lblComNmP.Location = New System.Drawing.Point(8, 44)
        Me.lblComNmP.Name = "lblComNmP"
        Me.lblComNmP.Size = New System.Drawing.Size(111, 21)
        Me.lblComNmP.TabIndex = 8
        Me.lblComNmP.Text = "성분제제명(출력)"
        Me.lblComNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComNmD
        '
        Me.lblComNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComNmD.ForeColor = System.Drawing.Color.White
        Me.lblComNmD.Location = New System.Drawing.Point(384, 22)
        Me.lblComNmD.Name = "lblComNmD"
        Me.lblComNmD.Size = New System.Drawing.Size(111, 21)
        Me.lblComNmD.TabIndex = 7
        Me.lblComNmD.Text = "성분제제명(화면)"
        Me.lblComNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComNmD
        '
        Me.txtComNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtComNmD.Location = New System.Drawing.Point(496, 22)
        Me.txtComNmD.MaxLength = 50
        Me.txtComNmD.Name = "txtComNmD"
        Me.txtComNmD.Size = New System.Drawing.Size(262, 21)
        Me.txtComNmD.TabIndex = 7
        Me.txtComNmD.Tag = "COMNMD"
        '
        'lblComNm
        '
        Me.lblComNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComNm.ForeColor = System.Drawing.Color.White
        Me.lblComNm.Location = New System.Drawing.Point(8, 22)
        Me.lblComNm.Name = "lblComNm"
        Me.lblComNm.Size = New System.Drawing.Size(111, 21)
        Me.lblComNm.TabIndex = 6
        Me.lblComNm.Text = "성분제제명"
        Me.lblComNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComNmP
        '
        Me.txtComNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtComNmP.Location = New System.Drawing.Point(120, 44)
        Me.txtComNmP.MaxLength = 50
        Me.txtComNmP.Name = "txtComNmP"
        Me.txtComNmP.Size = New System.Drawing.Size(255, 21)
        Me.txtComNmP.TabIndex = 8
        Me.txtComNmP.Tag = "COMNMP"
        '
        'txtComNm
        '
        Me.txtComNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtComNm.Location = New System.Drawing.Point(120, 22)
        Me.txtComNm.MaxLength = 50
        Me.txtComNm.Name = "txtComNm"
        Me.txtComNm.Size = New System.Drawing.Size(255, 21)
        Me.txtComNm.TabIndex = 6
        Me.txtComNm.Tag = "COMNM"
        '
        'grpTop
        '
        Me.grpTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTop.Controls.Add(Me.cboSpcCd)
        Me.grpTop.Controls.Add(Me.lblSpcCd)
        Me.grpTop.Controls.Add(Me.txtComCd)
        Me.grpTop.Controls.Add(Me.lblComCd)
        Me.grpTop.Controls.Add(Me.btnUE)
        Me.grpTop.Controls.Add(Me.dtpUSTime)
        Me.grpTop.Controls.Add(Me.txtUSDay)
        Me.grpTop.Controls.Add(Me.dtpUSDay)
        Me.grpTop.Controls.Add(Me.lblUSDayTime)
        Me.grpTop.Location = New System.Drawing.Point(10, 13)
        Me.grpTop.Name = "grpTop"
        Me.grpTop.Size = New System.Drawing.Size(768, 65)
        Me.grpTop.TabIndex = 0
        Me.grpTop.TabStop = False
        Me.grpTop.Text = "성분제제코드"
        '
        'cboSpcCd
        '
        Me.cboSpcCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpcCd.Items.AddRange(New Object() {"[091] 혈액준비(Prep)", "[001] 혈액수혈(Tranf)", "[093] 응급수혈(Emer)", "[094] Irradiation"})
        Me.cboSpcCd.Location = New System.Drawing.Point(378, 38)
        Me.cboSpcCd.Name = "cboSpcCd"
        Me.cboSpcCd.Size = New System.Drawing.Size(156, 20)
        Me.cboSpcCd.TabIndex = 5
        Me.cboSpcCd.Tag = "SPCCD_01"
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(300, 38)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(77, 21)
        Me.lblSpcCd.TabIndex = 15
        Me.lblSpcCd.Text = "검체코드"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComCd
        '
        Me.txtComCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtComCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtComCd.Location = New System.Drawing.Point(120, 38)
        Me.txtComCd.MaxLength = 10
        Me.txtComCd.Name = "txtComCd"
        Me.txtComCd.Size = New System.Drawing.Size(150, 21)
        Me.txtComCd.TabIndex = 4
        Me.txtComCd.Tag = "COMCD"
        '
        'lblComCd
        '
        Me.lblComCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComCd.ForeColor = System.Drawing.Color.White
        Me.lblComCd.Location = New System.Drawing.Point(6, 38)
        Me.lblComCd.Name = "lblComCd"
        Me.lblComCd.Size = New System.Drawing.Size(113, 21)
        Me.lblComCd.TabIndex = 13
        Me.lblComCd.Text = "성분제제코드"
        Me.lblComCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(687, 21)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 0
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(214, 16)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 3
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(120, 16)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(72, 21)
        Me.txtUSDay.TabIndex = 1
        Me.txtUSDay.Tag = ""
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(193, 16)
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
        Me.lblUSDayTime.Location = New System.Drawing.Point(6, 16)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(113, 21)
        Me.lblUSDayTime.TabIndex = 8
        Me.lblUSDayTime.Text = "시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'pnlTop
        '
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.Controls.Add(Me.tclCom)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 1
        '
        'txtBldCd2
        '
        Me.txtBldCd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldCd2.Location = New System.Drawing.Point(515, 250)
        Me.txtBldCd2.MaxLength = 20
        Me.txtBldCd2.Name = "txtBldCd2"
        Me.txtBldCd2.Size = New System.Drawing.Size(103, 21)
        Me.txtBldCd2.TabIndex = 156
        Me.txtBldCd2.Tag = "DSPCCD2"
        '
        'lblBldCd2
        '
        Me.lblBldCd2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBldCd2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBldCd2.ForeColor = System.Drawing.Color.White
        Me.lblBldCd2.Location = New System.Drawing.Point(375, 250)
        Me.lblBldCd2.Name = "lblBldCd2"
        Me.lblBldCd2.Size = New System.Drawing.Size(139, 21)
        Me.lblBldCd2.TabIndex = 157
        Me.lblBldCd2.Text = "혈액제재코드(혈액원)"
        Me.lblBldCd2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FDF30
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FDF30"
        Me.Text = "[30] 성분제제"
        Me.tclCom.ResumeLayout(False)
        Me.tbcPage.ResumeLayout(False)
        Me.tbcPage.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpTop.ResumeLayout(False)
        Me.grpTop.PerformLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnExeDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExeDay.Click
        chkExeDay1.Checked = True
        chkExeDay2.Checked = True
        chkExeDay3.Checked = True
        chkExeDay4.Checked = True
        chkExeDay5.Checked = True
        chkExeDay6.Checked = True
        chkExeDay7.Checked = True
    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click"
        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If Me.txtComCd.Text = "" Then Return
        If Ctrl.Get_Code(Me.cboSpcCd) = "" Then Return

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "   성분제제코드 : " & Me.txtComCd.Text & vbCrLf
            sMsg &= "   성분제제구분 : " & Me.cboSpcCd.SelectedItem.ToString() & vbCrLf
            sMsg &= "   성분제제명 : " & Me.txtComNm.Text & vbCrLf & vbCrLf
            sMsg &= "   을(를) 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString
            sUeTime = CType(objFrm, FGF02).UETime.ToString

            If mobjDAF.TransComCdInfo_UE(Me.txtComCd.Text, Ctrl.Get_Code(Me.cboSpcCd), txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, sUeDate.Replace("-", "") + sUeTime.Replace(":", ""), USER_INFO.USRID) Then
                MsgBox("해당 성분제제 정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub cboOWarningGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOWarningGbn.SelectedIndexChanged
        Select Case cboOWarningGbn.SelectedIndex
            Case -1, 0
                txtOWarning.Enabled = False
            Case Else
                txtOWarning.Enabled = True
        End Select
    End Sub

    Private Sub dtpUSDay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUSDay.ValueChanged
        If miSelectKey = 1 Then Exit Sub
        If txtUSDay.Text.Trim = "" Then Exit Sub

        txtUSDay.Text = Format(dtpUSDay.Value, "yyyy-MM-dd").Substring(0, 10)
    End Sub

    Private Sub txtComNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtComNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If txtComNmS.Text.Trim = "" Then
            If txtComNm.Text.Length > txtComNmS.MaxLength Then
                txtComNmS.Text = txtComNm.Text.Substring(0, txtComNmS.MaxLength)
            Else
                txtComNmS.Text = txtComNm.Text
            End If
        End If

        If txtComNmD.Text.Trim = "" Then
            If txtComNm.Text.Length > txtComNmD.MaxLength Then
                txtComNmD.Text = txtComNm.Text.Substring(0, txtComNmD.MaxLength)
            Else
                txtComNmD.Text = txtComNm.Text
            End If
        End If

        If txtComNmP.Text.Trim = "" Then
            If txtComNm.Text.Length > txtComNmP.MaxLength Then
                txtComNmP.Text = txtComNm.Text.Substring(0, txtComNmP.MaxLength)
            Else
                txtComNmP.Text = txtComNm.Text
            End If
        End If
    End Sub

    Private Sub FDF30_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

End Class
