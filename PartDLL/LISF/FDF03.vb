'>>> [03] 검체
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF03
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF03.vb, Class : FDF03" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_SPC
    Friend WithEvents btnGetExcel As System.Windows.Forms.Button
    Friend WithEvents chkBldGbn As System.Windows.Forms.CheckBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            dt = mobjDAF.GetUsUeCd_Spc(Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransSpcInfo_DEL(Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

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
            dt = mobjDAF.GetUsUeDupl_Spc(Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransSpcInfo_UPD_US(Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransSpcInfo_UPD_UE(Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
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
                .txtCd.Text = Me.txtSpcCd.Text
                .txtNm.Text = Me.txtSpcNmD.Text

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

    Private Function fnCollectItemTable_30(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_30() As LISAPP.ItemTableCollection"

        Try
            Dim it30 As New LISAPP.ItemTableCollection

            With it30
                .SetItemTable("SPCCD", 1, 1, Me.txtSpcCd.Text)
                .SetItemTable("USDT", 2, 1, Me.txtUSDay.Text.Replace("-", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString)

                If Me.txtUEDT.Text = "" Then
                    .SetItemTable("UEDT", 3, 1, msUEDT)
                Else
                    .SetItemTable("UEDT", 3, 1, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                End If

                .SetItemTable("REGDT", 4, 1, rsRegDT)
                .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                .SetItemTable("SPCNM", 6, 1, Me.txtSpcNm.Text)
                .SetItemTable("SPCNMS", 7, 1, Me.txtSpcNmS.Text)
                .SetItemTable("SPCNMD", 8, 1, Me.txtSpcNmD.Text)
                .SetItemTable("SPCNMP", 9, 1, Me.txtSpcNmP.Text)
                .SetItemTable("SPCNMBP", 10, 1, Me.txtSpcNmBP.Text)
                .SetItemTable("SPCIFCD", 11, 1, Me.txtIFCd.Text)
                .SetItemTable("SPCWNCD", 12, 1, Me.txtWNCd.Text)
                .SetItemTable("REQCMT", 13, 1, IIf(Me.chkReqCmt.Checked, "1", "0").ToString)
                .SetItemTable("MBSPCYN", 14, 1, IIf(Me.chkMBSpcYN.Checked, "1", "0").ToString)
                .SetItemTable("BLDGBN", 15, 1, IIf(Me.chkBldGbn.Checked, "1", "0").ToString)
                .SetItemTable("REGIP", 16, 1, USER_INFO.LOCALIP)
            End With

            fnCollectItemTable_30 = it30
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnCollectItemTable_33(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_33() As LISAPP.ItemTableCollection"

        Try
            Dim it33 As New LISAPP.ItemTableCollection
            Dim iCnt As Integer = 0

            With spdOrdSlip
                For i As Integer = 1 To .MaxRows
                    .Col = 1 : .Row = i : Dim sChk As String = .Text
                    .Col = 2 : .Row = i : Dim sOrdSlip As String = Ctrl.Get_Code(spdOrdSlip, .GetColFromID("tordslip"), i, True)
                    .Col = 3 : .Row = i : Dim sDispNo As String = .Text
                    .Col = 4 : .Row = i : Dim sUseFlg As String = .Text

                    If sChk = "1" Then
                        iCnt += 1
                        it33.SetItemTable("tordslip", 1, iCnt, sOrdSlip)
                        it33.SetItemTable("spccd", 2, iCnt, Me.txtSpcCd.Text)
                        it33.SetItemTable("regdt", 3, iCnt, rsRegDT)
                        it33.SetItemTable("regid", 4, iCnt, USER_INFO.USRID)
                        it33.SetItemTable("dispseq", 5, iCnt, sDispNo)
                        it33.SetItemTable("useflg", 6, iCnt, sUseFlg)
                        it33.SetItemTable("regip", 7, iCnt, USER_INFO.LOCALIP)
                    End If
                Next
            End With

            fnCollectItemTable_33 = it33
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            fnCollectItemTable_33 = New LISAPP.ItemTableCollection
        End Try
    End Function

    'Private Function fnCollectItemTable_34(ByVal asRegDT As String) As LISAPP.ItemTableCollection
    '    Dim sFn As String = "Private Function fnCollectItemTable_34() As LISAPP.ItemTableCollection"

    '    Try
    '        Dim it34 As New LISAPP.ItemTableCollection
    '        Dim iCnt As Integer = 0

    '        With spdSubSpc
    '            For i As Integer = 1 To .MaxRows
    '                .Col = 1 : .Row = i : Dim sSubSpcNm As String = .Text
    '                .Col = 2 : .Row = i : Dim sReqCmt As String = .Text

    '                If Not sSubSpcNm.Trim = "" Then
    '                    iCnt += 1
    '                    it34.SetItemTable("SPCCD", 1, iCnt, txtSpcCd.Text)
    '                    it34.SetItemTable("DSPCCD", 2, iCnt, i.ToString)
    '                    it34.SetItemTable("DSPCNM", 3, iCnt, sSubSpcNm)
    '                    it34.SetItemTable("REGDT", 4, iCnt, asRegDT)
    '                    it34.SetItemTable("REGID", 5, iCnt, msUserID)
    '                    it34.SetItemTable("REQCMT", 6, iCnt, sReqCmt)
    '                End If
    '            Next
    '        End With

    '        fnCollectItemTable_34 = it34
    '    Catch ex As Exception
    '        Fn.log(mcFile + sFn, Err)
    '        MsgBox(mcFile + sFn + vbCrLf + ex.Message)

    '        fnCollectItemTable_34 = New LISAPP.ItemTableCollection

    '    End Try
    'End Function

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

    Private Function fnFindConflict(ByVal rsSpcCd As String, ByVal rsUSDT As String) As String
        Dim sFn As String = ""

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetRecentSpcInfo(rsSpcCd, rsUSDT)

            If DTable.Rows.Count > 0 Then
                Return "시작일시가 " + DTable.Rows(0).Item(0).ToString + "인 동일 검체 코드가 존재합니다." + vbCrLf + vbCrLf + _
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
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            fnGetSystemDT = Format(Now, "yyyy-MM-dd HH:mm:ss")
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it30 As New LISAPP.ItemTableCollection
            Dim it33 As New LISAPP.ItemTableCollection
            Dim iRegType30 As Integer = 0, iRegType33 As Integer = 0
            Dim sRegDT As String

            iRegType30 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType33 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it30 = fnCollectItemTable_30(sRegDT)
            it33 = fnCollectItemTable_33(sRegDT)

            If mobjDAF.TransSpcInfo(it30, iRegType30, it33, iRegType33, _
                                    Me.txtSpcCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID) Then
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
            If Me.txtSpcCd.Text = "" Then
                MsgBox("검체코드를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtSpcCd.Text.Trim) <> 3 And Len(Me.txtSpcCd.Text.Trim) <> 5 Then
                'If Len(Me.txtSpcCd.Text.Trim) < PRG_CONST.Len_SpcCd Then
                MsgBox("검체코드를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsDate(Me.txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtSpcCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Me.txtSpcNm.Text.Trim = "" Then
                MsgBox("검체명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtSpcNmS.Text.Trim = "" Then
                MsgBox("검체명(처방)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtSpcNmD.Text.Trim = "" Then
                MsgBox("검체명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtSpcNmP.Text.Trim = "" Then
                MsgBox("검체명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
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

    Public Sub sbDisplayCdDetail(ByVal asSpcCd As String, ByVal asUSDT As String, Optional ByVal asUEDT As String = "30000101", Optional ByVal aiMode As Integer = 0)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_Spc(asSpcCd, asUSDT)
            sbDisplayCdDetail_SpcOrdSlip(asSpcCd)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Spc(ByVal rsSpcCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail(ByVal asBuf As String, ByVal asTCd As String)"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mobjDAF.GetSpcInfo(rsSpcCd, rsUsDt)

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

    Private Sub sbDisplayCdDetail_SpcOrdSlip(ByVal rsSpcCd As String)
        Dim sFn As String = ""

        Try

            Dim iCol As Integer = 0

            Dim dt As DataTable = mobjDAF.GetSpcOrdSlipInfo(rsSpcCd)

            '스프레드 초기화
            'sbInitialize_spdOrdSlip()

            If dt.Rows.Count < 1 Then Return

            With spdOrdSlip
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName)

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.trim
                        End If
                    Next
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    'Private Sub sbDisplayCdDetail_SubSpc(ByVal asSpcCd As String)
    '    Dim sFn As String = ""

    '    Try
    '        Dim DTable As DataTable
    '        Dim iCol As Integer = 0

    '        DTable = mobjDAF.GetSubSpcInfo(asSpcCd)

    '        '스프레드 초기화
    '        sbInitialize_spdSubSpc()

    '        If DTable.Rows.Count > 0 Then
    '            With spdSubSpc
    '                .ReDraw = False

    '                .MaxRows = DTable.Rows.Count

    '                For i As Integer = 0 To DTable.Rows.Count - 1
    '                    For j As Integer = 0 To DTable.Columns.Count - 1
    '                        iCol = 0
    '                        iCol = .GetColFromID(DTable.Columns(j).ColumnName)

    '                        If iCol > 0 Then
    '                            .Col = iCol
    '                            .Row = i + 1
    '                            .Text = DTable.Rows(i).Item(j).ToString
    '                        End If
    '                    Next
    '                Next

    '                .ReDraw = True
    '            End With
    '        Else
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        Fn.log(mcFile + sFn, Err)
    '        MsgBox(mcFile + sFn + vbCrLf + ex.Message)
    '    End Try
    'End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRID = "ACK" Then btnGetExcel.Visible = False

            If USER_INFO.USRLVL = "S" Then
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

            Me.txtSpcCd.MaxLength = PRG_CONST.Len_SpcCd

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
                'tpgSpc1 초기화
                txtSpcCd.Text = "" : btnUE.Visible = False

                txtSpcNm.Text = "" : txtSpcNmD.Text = "" : txtSpcNmP.Text = "" : txtSpcNmBP.Text = "" : txtUSDT.Text = "" : txtUEDT.Text = "" : txtRegDT.Text = "" : txtRegNm.Text = ""

                chkMBSpcYN.Checked = False
                txtIFCd.Text = "" : txtWNCd.Text = ""

                txtSpcNmS.Text = ""
                chkReqCmt.Checked = False : chkBldGbn.Checked = False

                'txtSpcCd0.Text = "" : txtUSDT.Text = "" : txtUEDT.Text = "" : txtRegDT.Text = "" : txtRegID.Text = ""

                sbDisplayCdDetail_SpcOrdSlip("")
                'spdSubSpc.MaxRows = 0 : spdSubSpc.MaxRows = 20
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

    Private Sub sbInitialize_spdOrdSlip()
        With spdOrdSlip
            .MaxRows = 0
        End With
    End Sub

    Public Sub sbSetNewUSDT()
        Dim sFn As String = ""

        Try
            Dim sDate As String = fnGetSystemDT()
            sDate = sDate.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")

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
    Friend WithEvents lblTNmS As System.Windows.Forms.Label
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpTInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpTestCd As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtWNCd As System.Windows.Forms.TextBox
    Friend WithEvents lblWNCd As System.Windows.Forms.Label
    Friend WithEvents txtIFCd As System.Windows.Forms.TextBox
    Friend WithEvents lblIFCd As System.Windows.Forms.Label
    Friend WithEvents lblSpcNmBP As System.Windows.Forms.Label
    Friend WithEvents txtSpcNmBP As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcNmP As System.Windows.Forms.Label
    Friend WithEvents txtSpcNmP As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcNmD As System.Windows.Forms.Label
    Friend WithEvents txtSpcNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcNmS As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents txtSpcNm As System.Windows.Forms.TextBox
    Friend WithEvents chkReqCmt As System.Windows.Forms.CheckBox
    Friend WithEvents tpgSpc1 As System.Windows.Forms.TabPage
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents spdOrdSlip As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkMBSpcYN As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF03))
        Me.txtWNCd = New System.Windows.Forms.TextBox
        Me.lblWNCd = New System.Windows.Forms.Label
        Me.txtIFCd = New System.Windows.Forms.TextBox
        Me.lblIFCd = New System.Windows.Forms.Label
        Me.lblSpcNmBP = New System.Windows.Forms.Label
        Me.txtSpcNmBP = New System.Windows.Forms.TextBox
        Me.lblSpcNmP = New System.Windows.Forms.Label
        Me.txtSpcNmP = New System.Windows.Forms.TextBox
        Me.lblSpcNmD = New System.Windows.Forms.Label
        Me.txtSpcNmD = New System.Windows.Forms.TextBox
        Me.lblTNmS = New System.Windows.Forms.Label
        Me.txtSpcNmS = New System.Windows.Forms.TextBox
        Me.lblSpcNm = New System.Windows.Forms.Label
        Me.txtSpcNm = New System.Windows.Forms.TextBox
        Me.txtUEDT = New System.Windows.Forms.TextBox
        Me.lblUEDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.txtUSDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.lblUSDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpTInfo1 = New System.Windows.Forms.GroupBox
        Me.chkBldGbn = New System.Windows.Forms.CheckBox
        Me.chkMBSpcYN = New System.Windows.Forms.CheckBox
        Me.btnGetExcel = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.spdOrdSlip = New AxFPSpreadADO.AxfpSpread
        Me.chkReqCmt = New System.Windows.Forms.CheckBox
        Me.grpTestCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker
        Me.txtUSDay = New System.Windows.Forms.TextBox
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker
        Me.lblUSDayTime = New System.Windows.Forms.Label
        Me.lblSpcCd = New System.Windows.Forms.Label
        Me.txtSpcCd = New System.Windows.Forms.TextBox
        Me.tpgSpc1 = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.grpTInfo1.SuspendLayout()
        CType(Me.spdOrdSlip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTestCd.SuspendLayout()
        Me.tpgSpc1.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtWNCd
        '
        Me.txtWNCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWNCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWNCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWNCd.Location = New System.Drawing.Point(110, 195)
        Me.txtWNCd.MaxLength = 10
        Me.txtWNCd.Name = "txtWNCd"
        Me.txtWNCd.Size = New System.Drawing.Size(57, 21)
        Me.txtWNCd.TabIndex = 10
        Me.txtWNCd.Tag = "SPCWNCD"
        '
        'lblWNCd
        '
        Me.lblWNCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWNCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWNCd.ForeColor = System.Drawing.Color.White
        Me.lblWNCd.Location = New System.Drawing.Point(8, 195)
        Me.lblWNCd.Name = "lblWNCd"
        Me.lblWNCd.Size = New System.Drawing.Size(101, 21)
        Me.lblWNCd.TabIndex = 0
        Me.lblWNCd.Text = "WHONET코드"
        Me.lblWNCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIFCd
        '
        Me.txtIFCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIFCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtIFCd.Location = New System.Drawing.Point(110, 173)
        Me.txtIFCd.MaxLength = 10
        Me.txtIFCd.Name = "txtIFCd"
        Me.txtIFCd.Size = New System.Drawing.Size(57, 21)
        Me.txtIFCd.TabIndex = 9
        Me.txtIFCd.Tag = "SPCIFCD"
        '
        'lblIFCd
        '
        Me.lblIFCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIFCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIFCd.ForeColor = System.Drawing.Color.White
        Me.lblIFCd.Location = New System.Drawing.Point(8, 173)
        Me.lblIFCd.Name = "lblIFCd"
        Me.lblIFCd.Size = New System.Drawing.Size(101, 21)
        Me.lblIFCd.TabIndex = 0
        Me.lblIFCd.Text = "IF코드"
        Me.lblIFCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSpcNmBP
        '
        Me.lblSpcNmBP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcNmBP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcNmBP.ForeColor = System.Drawing.Color.White
        Me.lblSpcNmBP.Location = New System.Drawing.Point(8, 151)
        Me.lblSpcNmBP.Name = "lblSpcNmBP"
        Me.lblSpcNmBP.Size = New System.Drawing.Size(101, 21)
        Me.lblSpcNmBP.TabIndex = 0
        Me.lblSpcNmBP.Text = "검체명(바코드)"
        Me.lblSpcNmBP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcNmBP
        '
        Me.txtSpcNmBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcNmBP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcNmBP.Location = New System.Drawing.Point(110, 151)
        Me.txtSpcNmBP.MaxLength = 10
        Me.txtSpcNmBP.Name = "txtSpcNmBP"
        Me.txtSpcNmBP.Size = New System.Drawing.Size(57, 21)
        Me.txtSpcNmBP.TabIndex = 8
        Me.txtSpcNmBP.Tag = "SPCNMBP"
        '
        'lblSpcNmP
        '
        Me.lblSpcNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcNmP.ForeColor = System.Drawing.Color.White
        Me.lblSpcNmP.Location = New System.Drawing.Point(8, 91)
        Me.lblSpcNmP.Name = "lblSpcNmP"
        Me.lblSpcNmP.Size = New System.Drawing.Size(86, 21)
        Me.lblSpcNmP.TabIndex = 0
        Me.lblSpcNmP.Text = "검체명(출력)"
        Me.lblSpcNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcNmP
        '
        Me.txtSpcNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcNmP.Location = New System.Drawing.Point(95, 91)
        Me.txtSpcNmP.MaxLength = 60
        Me.txtSpcNmP.Name = "txtSpcNmP"
        Me.txtSpcNmP.Size = New System.Drawing.Size(283, 21)
        Me.txtSpcNmP.TabIndex = 6
        Me.txtSpcNmP.Tag = "SPCNMP"
        '
        'lblSpcNmD
        '
        Me.lblSpcNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcNmD.ForeColor = System.Drawing.Color.White
        Me.lblSpcNmD.Location = New System.Drawing.Point(8, 69)
        Me.lblSpcNmD.Name = "lblSpcNmD"
        Me.lblSpcNmD.Size = New System.Drawing.Size(86, 21)
        Me.lblSpcNmD.TabIndex = 0
        Me.lblSpcNmD.Text = "검체명(화면)"
        Me.lblSpcNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcNmD
        '
        Me.txtSpcNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcNmD.Location = New System.Drawing.Point(95, 69)
        Me.txtSpcNmD.MaxLength = 60
        Me.txtSpcNmD.Name = "txtSpcNmD"
        Me.txtSpcNmD.Size = New System.Drawing.Size(283, 21)
        Me.txtSpcNmD.TabIndex = 5
        Me.txtSpcNmD.Tag = "SPCNMD"
        '
        'lblTNmS
        '
        Me.lblTNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTNmS.ForeColor = System.Drawing.Color.White
        Me.lblTNmS.Location = New System.Drawing.Point(8, 113)
        Me.lblTNmS.Name = "lblTNmS"
        Me.lblTNmS.Size = New System.Drawing.Size(86, 21)
        Me.lblTNmS.TabIndex = 0
        Me.lblTNmS.Text = "검체명(처방)"
        Me.lblTNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcNmS
        '
        Me.txtSpcNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcNmS.Location = New System.Drawing.Point(95, 113)
        Me.txtSpcNmS.MaxLength = 50
        Me.txtSpcNmS.Name = "txtSpcNmS"
        Me.txtSpcNmS.Size = New System.Drawing.Size(283, 21)
        Me.txtSpcNmS.TabIndex = 7
        Me.txtSpcNmS.Tag = "SPCNMS"
        '
        'lblSpcNm
        '
        Me.lblSpcNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcNm.ForeColor = System.Drawing.Color.White
        Me.lblSpcNm.Location = New System.Drawing.Point(8, 47)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(86, 21)
        Me.lblSpcNm.TabIndex = 0
        Me.lblSpcNm.Text = "검체명"
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcNm
        '
        Me.txtSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcNm.Location = New System.Drawing.Point(95, 47)
        Me.txtSpcNm.MaxLength = 60
        Me.txtSpcNm.Name = "txtSpcNm"
        Me.txtSpcNm.Size = New System.Drawing.Size(283, 21)
        Me.txtSpcNm.TabIndex = 4
        Me.txtSpcNm.Tag = "SPCNM"
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
        Me.txtRegDT.Location = New System.Drawing.Point(503, 548)
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
        Me.txtUSDT.Location = New System.Drawing.Point(108, 548)
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
        Me.lblUserNm.Location = New System.Drawing.Point(610, 548)
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
        Me.lblRegDT.Location = New System.Drawing.Point(418, 548)
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
        Me.txtRegID.Location = New System.Drawing.Point(695, 548)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(74, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'grpTInfo1
        '
        Me.grpTInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpTInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTInfo1.Controls.Add(Me.chkBldGbn)
        Me.grpTInfo1.Controls.Add(Me.chkMBSpcYN)
        Me.grpTInfo1.Controls.Add(Me.btnGetExcel)
        Me.grpTInfo1.Controls.Add(Me.Label2)
        Me.grpTInfo1.Controls.Add(Me.spdOrdSlip)
        Me.grpTInfo1.Controls.Add(Me.chkReqCmt)
        Me.grpTInfo1.Controls.Add(Me.txtWNCd)
        Me.grpTInfo1.Controls.Add(Me.lblWNCd)
        Me.grpTInfo1.Controls.Add(Me.txtIFCd)
        Me.grpTInfo1.Controls.Add(Me.lblIFCd)
        Me.grpTInfo1.Controls.Add(Me.lblSpcNmBP)
        Me.grpTInfo1.Controls.Add(Me.txtSpcNmBP)
        Me.grpTInfo1.Controls.Add(Me.lblSpcNmP)
        Me.grpTInfo1.Controls.Add(Me.txtSpcNmP)
        Me.grpTInfo1.Controls.Add(Me.lblSpcNmD)
        Me.grpTInfo1.Controls.Add(Me.txtSpcNmD)
        Me.grpTInfo1.Controls.Add(Me.lblTNmS)
        Me.grpTInfo1.Controls.Add(Me.txtSpcNmS)
        Me.grpTInfo1.Controls.Add(Me.lblSpcNm)
        Me.grpTInfo1.Controls.Add(Me.txtSpcNm)
        Me.grpTInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpTInfo1.Location = New System.Drawing.Point(8, 80)
        Me.grpTInfo1.Name = "grpTInfo1"
        Me.grpTInfo1.Size = New System.Drawing.Size(764, 460)
        Me.grpTInfo1.TabIndex = 2
        Me.grpTInfo1.TabStop = False
        Me.grpTInfo1.Text = "검체정보"
        '
        'chkBldGbn
        '
        Me.chkBldGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkBldGbn.Location = New System.Drawing.Point(174, 244)
        Me.chkBldGbn.Name = "chkBldGbn"
        Me.chkBldGbn.Size = New System.Drawing.Size(129, 20)
        Me.chkBldGbn.TabIndex = 12
        Me.chkBldGbn.Tag = "BLDGBN"
        Me.chkBldGbn.Text = "수혈검체로 설정"
        '
        'chkMBSpcYN
        '
        Me.chkMBSpcYN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkMBSpcYN.Location = New System.Drawing.Point(10, 244)
        Me.chkMBSpcYN.Name = "chkMBSpcYN"
        Me.chkMBSpcYN.Size = New System.Drawing.Size(135, 20)
        Me.chkMBSpcYN.TabIndex = 11
        Me.chkMBSpcYN.Tag = "MBSPCYN"
        Me.chkMBSpcYN.Text = "미생물검체로 설정"
        '
        'btnGetExcel
        '
        Me.btnGetExcel.Location = New System.Drawing.Point(678, 17)
        Me.btnGetExcel.Name = "btnGetExcel"
        Me.btnGetExcel.Size = New System.Drawing.Size(62, 25)
        Me.btnGetExcel.TabIndex = 14
        Me.btnGetExcel.Text = "Excel"
        Me.btnGetExcel.UseVisualStyleBackColor = True
        Me.btnGetExcel.Visible = False
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Location = New System.Drawing.Point(393, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 415)
        Me.Label2.TabIndex = 0
        Me.Label2.Visible = False
        '
        'spdOrdSlip
        '
        Me.spdOrdSlip.DataSource = Nothing
        Me.spdOrdSlip.Location = New System.Drawing.Point(433, 47)
        Me.spdOrdSlip.Name = "spdOrdSlip"
        Me.spdOrdSlip.OcxState = CType(resources.GetObject("spdOrdSlip.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrdSlip.Size = New System.Drawing.Size(307, 404)
        Me.spdOrdSlip.TabIndex = 15
        Me.spdOrdSlip.Visible = False
        '
        'chkReqCmt
        '
        Me.chkReqCmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkReqCmt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkReqCmt.ForeColor = System.Drawing.Color.Black
        Me.chkReqCmt.Location = New System.Drawing.Point(433, 20)
        Me.chkReqCmt.Name = "chkReqCmt"
        Me.chkReqCmt.Size = New System.Drawing.Size(280, 21)
        Me.chkReqCmt.TabIndex = 13
        Me.chkReqCmt.Tag = "REQCMT"
        Me.chkReqCmt.Text = "Other 설정(처방 시 Remark로 전달)"
        Me.chkReqCmt.UseVisualStyleBackColor = False
        Me.chkReqCmt.Visible = False
        '
        'grpTestCd
        '
        Me.grpTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTestCd.Controls.Add(Me.btnUE)
        Me.grpTestCd.Controls.Add(Me.dtpUSTime)
        Me.grpTestCd.Controls.Add(Me.txtUSDay)
        Me.grpTestCd.Controls.Add(Me.dtpUSDay)
        Me.grpTestCd.Controls.Add(Me.lblUSDayTime)
        Me.grpTestCd.Controls.Add(Me.lblSpcCd)
        Me.grpTestCd.Controls.Add(Me.txtSpcCd)
        Me.grpTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpTestCd.Location = New System.Drawing.Point(8, 7)
        Me.grpTestCd.Name = "grpTestCd"
        Me.grpTestCd.Size = New System.Drawing.Size(764, 67)
        Me.grpTestCd.TabIndex = 1
        Me.grpTestCd.TabStop = False
        Me.grpTestCd.Text = "검체코드"
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(684, 24)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 7
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(194, 15)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 2
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(95, 15)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(77, 21)
        Me.txtUSDay.TabIndex = 1
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(173, 15)
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
        Me.lblUSDayTime.Size = New System.Drawing.Size(86, 21)
        Me.lblUSDayTime.TabIndex = 0
        Me.lblUSDayTime.Text = "시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(8, 37)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(86, 21)
        Me.lblSpcCd.TabIndex = 0
        Me.lblSpcCd.Text = "검체코드"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcCd
        '
        Me.txtSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSpcCd.Location = New System.Drawing.Point(95, 37)
        Me.txtSpcCd.MaxLength = 4
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.Size = New System.Drawing.Size(77, 21)
        Me.txtSpcCd.TabIndex = 3
        Me.txtSpcCd.Tag = "SPCCD"
        '
        'tpgSpc1
        '
        Me.tpgSpc1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgSpc1.Controls.Add(Me.txtRegNm)
        Me.tpgSpc1.Controls.Add(Me.txtUEDT)
        Me.tpgSpc1.Controls.Add(Me.lblUEDT)
        Me.tpgSpc1.Controls.Add(Me.txtRegDT)
        Me.tpgSpc1.Controls.Add(Me.txtUSDT)
        Me.tpgSpc1.Controls.Add(Me.lblUserNm)
        Me.tpgSpc1.Controls.Add(Me.lblRegDT)
        Me.tpgSpc1.Controls.Add(Me.lblUSDT)
        Me.tpgSpc1.Controls.Add(Me.txtRegID)
        Me.tpgSpc1.Controls.Add(Me.grpTInfo1)
        Me.tpgSpc1.Controls.Add(Me.grpTestCd)
        Me.tpgSpc1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpgSpc1.Location = New System.Drawing.Point(4, 21)
        Me.tpgSpc1.Name = "tpgSpc1"
        Me.tpgSpc1.Size = New System.Drawing.Size(780, 576)
        Me.tpgSpc1.TabIndex = 0
        Me.tpgSpc1.Text = "검체기본정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(695, 548)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(74, 21)
        Me.txtRegNm.TabIndex = 127
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tpgSpc1)
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
        'pnlTop
        '
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.Controls.Add(Me.tclSpc)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 1
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF03
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF03"
        Me.Text = "[03] 검체"
        Me.grpTInfo1.ResumeLayout(False)
        Me.grpTInfo1.PerformLayout()
        CType(Me.spdOrdSlip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTestCd.ResumeLayout(False)
        Me.grpTestCd.PerformLayout()
        Me.tpgSpc1.ResumeLayout(False)
        Me.tpgSpc1.PerformLayout()
        Me.tclSpc.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If Me.txtSpcCd.Text = "" Then Exit Sub

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "   검체코드 : " + txtSpcCd.Text + vbCrLf
            sMsg += "   검체명   : " & txtSpcNm.Text + vbCrLf + vbCrLf
            sMsg += "   을(를) 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mobjDAF.TransSpcInfo_UE(Me.txtSpcCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID, sUeDate + sUeTime) Then
                MsgBox("해당 검체정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub spdOrdSlip_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdOrdSlip.ButtonClicked
        With spdOrdSlip
            If e.col = 1 Then
                .Col = e.col : .Row = e.row : Dim sChk As String = .Text
                .Col = 4 : .Row = e.row : .Text = sChk
            End If
        End With
    End Sub

    Private Sub txtSpcNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSpcNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If txtSpcNmS.Text.Trim = "" Then
            If txtSpcNm.Text.Length > txtSpcNmS.MaxLength Then
                txtSpcNmS.Text = txtSpcNm.Text.Substring(0, txtSpcNmS.MaxLength)
            Else
                txtSpcNmS.Text = txtSpcNm.Text
            End If
        End If

        If txtSpcNmD.Text.Trim = "" Then
            If txtSpcNm.Text.Length > txtSpcNmD.MaxLength Then
                txtSpcNmD.Text = txtSpcNm.Text.Substring(0, txtSpcNmD.MaxLength)
            Else
                txtSpcNmD.Text = txtSpcNm.Text
            End If
        End If

        If txtSpcNmP.Text.Trim = "" Then
            If txtSpcNm.Text.Length > txtSpcNmP.MaxLength Then
                txtSpcNmP.Text = txtSpcNm.Text.Substring(0, txtSpcNmP.MaxLength)
            Else
                txtSpcNmP.Text = txtSpcNm.Text
            End If
        End If
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetExcel.Click

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing


        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim dt As New DataTable

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open("c:\as\검체코드.xls")

            xlsWkS = CType(xlsWkB.Sheets("Sheet1"), Excel.Worksheet)

            For iLine As Integer = 2 To 84

                Dim sSpcCd As String = xlsWkS.Range("H" + CStr(iLine)).Value.ToString
                Dim sSpcNm As String = xlsWkS.Range("B" + CStr(iLine)).Value.ToString
                Dim sSpcNms As String = xlsWkS.Range("C" + CStr(iLine)).Value.ToString
                Dim sSpcNmd As String = xlsWkS.Range("D" + CStr(iLine)).Value.ToString
                Dim sSpcNmp As String = xlsWkS.Range("E" + CStr(iLine)).Value.ToString
                Dim sSpcNmbp As String = xlsWkS.Range("F" + CStr(iLine)).Value.ToString
                Dim sSpcIfcd As String = xlsWkS.Range("G" + CStr(iLine)).Value.ToString

                dt = mobjDAF.GetSpcInfo(1, "")
                Dim a_dr As DataRow()

                a_dr = dt.Select("SPCCD = '" + sSpcCd.PadLeft(4, "0"c) + "'")

                dt = Fn.ChangeToDataTable(a_dr)
                If dt.Rows.Count < 1 Then
                    Dim it30 As New LISAPP.ItemTableCollection
                    Dim it33 As New LISAPP.ItemTableCollection
                    Dim it34 As New LISAPP.ItemTableCollection
                    Dim iRegType30 As Integer = 0, iRegType33 As Integer = 0, iRegType34 As Integer = 0
                    Dim sRegDT As String

                    sRegDT = fnGetSystemDT()

                    With it30
                        .SetItemTable("SPCCD", 1, 1, sSpcCd.PadLeft(4, "0"c))
                        .SetItemTable("USDT", 2, 1, "20010101000000")
                        .SetItemTable("UEDT", 3, 1, msUEDT)
                        .SetItemTable("REGDT", 4, 1, sRegDT.Replace("-", "").Replace(":", "").Replace(" ", ""))
                        .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                        .SetItemTable("SPCNM", 6, 1, sSpcNm)
                        .SetItemTable("SPCNMS", 7, 1, sSpcNms)
                        .SetItemTable("SPCNMD", 8, 1, sSpcNmd)
                        .SetItemTable("SPCNMP", 9, 1, sSpcNmp)
                        .SetItemTable("SPCNMBP", 10, 1, sSpcNmbp)
                        .SetItemTable("SPCIFCD", 11, 1, sSpcIfcd)
                        .SetItemTable("SPCWNCD", 12, 1, "")
                        .SetItemTable("REQCMT", 13, 1, "0")
                        .SetItemTable("MBSPCYN", 14, 1, "")
                        .SetItemTable("REGIP", 15, 1, USER_INFO.LOCALIP)
                    End With

                    If mobjDAF.TransSpcInfo(it30, 0, it33, 0, sSpcCd, "20110310000000", USER_INFO.USRID) Then
                    Else
                        MsgBox("등록오류")
                    End If

                End If

            Next

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try

    End Sub

    Private Sub FDF03_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtSpcCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSpcCd.KeyDown, txtSpcNm.KeyDown, txtSpcNmBP.KeyDown, txtSpcNmD.KeyDown, txtSpcNmP.KeyDown, txtSpcNmS.KeyDown, txtWNCd.KeyDown, txtIFCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub
End Class
