'>>> [01] ��/�˻��
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF01
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF01.vb, Class : FDF01" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0        'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_BCCLS

    Friend WithEvents cboColorGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblColorGbn As System.Windows.Forms.Label
    Friend WithEvents lblBcclsCd As System.Windows.Forms.Label
    Friend WithEvents txtBcclsCd As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents lblBcclsGbn As System.Windows.Forms.Label
    Friend WithEvents cboBcclsGbn As System.Windows.Forms.ComboBox

    Private Sub sbEditUseDt_Edit(ByVal rsUseTag As String, ByVal rsUseDt As String)
        Dim sFn As String = "Sub sbEditUseDt_Edit"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> ����ߺ� ����
            dt = mobjDAF.GetUsUeDupl_bccls(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", ""), rsUseTag.ToUpper, rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("����Ͻ� ������ ������ �ڵ尡 �����մϴ�. �׷��� �����Ͻðڽ��ϱ�?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "����Ͻ� ���� �����ڵ� Ȯ��") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransBcclsInfo_UPD_US(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", ""))
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransBcclsInfo_UPD_UE(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", ""))
            End If

            If bReturn Then
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "�����Ͻ�", "�����Ͻ�").ToString + "�� �����Ǿ����ϴ�!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "�����Ͻ�", "�����Ͻ�").ToString + " ������ �����Ͽ����ϴ�!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> �ڵ��뿩�� ����
            dt = mobjDAF.GetUsUeCd_bccls(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("������� �ڵ��Դϴ�. �׷��� �����Ͻðڽ��ϱ�?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "���� Ȯ��") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransBcclsInfo_DEL(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

            If bReturn Then
                MsgBox("�ش� �ڵ尡 �����Ǿ����ϴ�!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox("�ش� �ڵ� ������ �����Ͽ����ϴ�!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub sbEditUseDt(ByVal rsUseTag As String)
        Dim sFn As String = "Public Sub sbEditUseDt"

        Try
            Dim fgf03 As New FGF03

            With fgf03
                .txtCd.Text = Me.txtBcclsCd.Text
                .txtNm.Text = Me.txtBcclsNm.Text

                .lblUseDt.Text = IIf(rsUseTag.ToUpper = "USDT", "�����Ͻ�", "�����Ͻ�").ToString
                .lblUseDtA.Text = IIf(rsUseTag.ToUpper = "USDT", "�����Ͻ�", "�����Ͻ�").ToString
                .btnEditUseDt.Text = .btnEditUseDt.Text.Replace("����Ͻ�", IIf(rsUseTag.ToUpper = "USDT", "�����Ͻ�", "�����Ͻ�").ToString)
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

    Private Function fnCollectItemTable_10(ByVal rsRegDt As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_10() As LISAPP.ItemTableCollection"

        Try
            Dim it10 As New LISAPP.ItemTableCollection

            If cboColorGbn.SelectedIndex < 0 Then cboColorGbn.SelectedIndex = 0

            With it10
                .SetItemTable("bcclscd", 1, 1, Me.txtBcclsCd.Text)
                .SetItemTable("usdt", 2, 1, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)

                If Me.txtUEDT.Text = "" Then
                    .SetItemTable("uedt", 3, 1, msUEDT)
                Else
                    .SetItemTable("uedt", 3, 1, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                End If

                .SetItemTable("regdt", 4, 1, rsRegDt)
                .SetItemTable("regid", 5, 1, USER_INFO.USRID)
                .SetItemTable("regip", 6, 1, USER_INFO.LOCALIP)
                .SetItemTable("bcclsnm", 7, 1, Me.txtBcclsNm.Text)
                .SetItemTable("bcclsnms", 8, 1, Me.txtBcclsNmS.Text)
                .SetItemTable("bcclsnmd", 9, 1, Me.txtBcclsNmD.Text)
                .SetItemTable("bcclsnmp", 10, 1, Me.txtBcclsNmP.Text)
                .SetItemTable("bcclsnmbp", 11, 1, Me.txtTBcclsNmBP.Text)
                .SetItemTable("colorgbn", 12, 1, Ctrl.Get_Code(cboColorGbn))
                .SetItemTable("bcclsgbn", 13, 1, Ctrl.Get_Code(cboBcclsGbn))
            End With

            Return it10
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
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

    Private Function fnFindConflict(ByVal rsBcclsCd As String, ByVal rsUsDt As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentBcclsInfo(rsBcclsCd, rsUsDt)

            If dt.Rows.Count > 0 Then
                Return "�����Ͻð� " + dt.Rows(0).Item(0).ToString + dt.Rows(0).Item("partgbn").ToString + "�� ���� ���ڵ�з� �ڵ尡 �����մϴ�." + vbCrLf + vbCrLf + _
                       "�����Ͻø� ������ �Ͻʽÿ�!!"
            Else
                Return ""
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return "Error"
        End Try
    End Function

    Private Function fnFindConflict_BC(ByVal rsBcclsCd As String, ByVal rsUsDt As String, ByVal rsBcclsnmbp As String, Optional ByVal riRegType As Integer = 0) As String
        Dim sFn As String = "fnFindConflict_BC"

        Try

            If riRegType = 0 Then '�ű�
                Dim dt As DataTable = mobjDAF.GetSameBC(rsBcclsCd, rsUsDt, rsBcclsnmbp, riRegType)

                If dt.Rows.Count > 0 Then
                    Return "���ڵ�з��ڵ峪 ��¸�(���ڵ�)�� ���� ���ڵ�з��ڵ� : " + dt.Rows(0).Item(0).ToString + dt.Rows(0).Item("partgbn").ToString + "�� �����մϴ�." + vbCrLf + vbCrLf + _
                           "���ڵ�з��ڵ峪 ��¸�(���ڵ�)�� ���� �Ͻʽÿ�!!"
                Else
                    Return ""
                End If

            Else  '����
                Dim dt As DataTable = mobjDAF.GetSameBC(rsBcclsCd, rsUsDt, rsBcclsnmbp, riRegType)

                If dt.Rows.Count > 0 Then
                    Return "���ڵ�з��ڵ峪 ��¸�(���ڵ�)�� ���� ���ڵ�з��ڵ� : " + dt.Rows(0).Item(0).ToString + dt.Rows(0).Item("partgbn").ToString + "�� �����մϴ�." + vbCrLf + vbCrLf + _
                           "���ڵ�з��ڵ峪 ��¸�(���ڵ�)�� ���� �Ͻʽÿ�!!"
                Else
                    Return ""
                End If

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
                Return dt.Rows(0).Item(0).ToString
            Else
                MsgBox("�ý����� ��¥�� �ʱ�ȭ���� ���߽��ϴ�. �����ڿ��� �����Ͻñ� �ٶ��ϴ�!!", MsgBoxStyle.Information)
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
            Dim it10 As New LISAPP.ItemTableCollection
            Dim iRegType10 As Integer = 0, iRegType11 As Integer = 0
            Dim sRegDT As String

            iRegType10 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType11 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()
            CType(cboColorGbn.SelectedItem, String).Substring(1, CType(cboColorGbn.SelectedItem, String).IndexOf("]") - 1)
            it10 = fnCollectItemTable_10(sRegDT)

            If mobjDAF.TransBcclsInfo(it10, iRegType10, Me.txtBcclsCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID) Then
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
            If Len(Me.txtBcclsCd.Text.Trim) < 1 Then
                MsgBox("���ڵ�з��ڵ带 (��Ȯ��) �Է��Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsDate(Me.txtUSDay.Text) Then
                MsgBox("�����Ͻø� ��Ȯ�� �Է��Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then      '�ű�
                    Dim sTmp As String = fnFindConflict_BC(txtBcclsCd.Text, txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""), txtTBcclsNmBP.Text, 0)

                    If Not sTmp = "" Then
                        MsgBox(sTmp, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                Else        '����
                    Dim sTmp As String = fnFindConflict_BC(txtBcclsCd.Text, txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""), txtTBcclsNmBP.Text, 1)

                    If Not sTmp = "" Then
                        MsgBox(sTmp, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtBcclsCd.Text, txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""))

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Me.txtBcclsNm.Text.Trim = "" Then
                MsgBox("���ڵ�з����� �Է��Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtBcclsNmS.Text.Trim = "" Then
                MsgBox("���ڵ�з���(���)�� �Է��Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtBcclsNmD.Text.Trim = "" Then
                MsgBox("���ڵ�з���(ȭ��)�� �Է��Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtBcclsNmP.Text.Trim = "" Then
                MsgBox("���ڵ�з���(���)�� �Է��Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTBcclsNmBP.Text.Trim = "" Then
                MsgBox("���ڵ�з���(���ڵ�)�� �Է��Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNumeric(Me.txtTBcclsNmBP.Text.Trim) Then
                MsgBox("���ڵ�з���(���ڵ�)�� ���ڷ� �Է��Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Critical)
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

    Public Sub sbDisplayCdDetail(ByVal rsBclclsCd As String, ByVal rsUsDt As String, ByVal rsUeDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1
            sbDisplayCdDetail_Bccls(rsBclclsCd, rsUsDt, rsUeDt)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Bccls(ByVal rsBcclsCd As String, ByVal rsUsdt As String, ByVal rsUedt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Sect()"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mobjDAF.GetBcclsInfo(rsBcclsCd, rsUsdt, rsUedt)

            '�ʱ�ȭ�� ���� ErrorProvider
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
                    Me.txtUSDay.Text = rsUsdt.Insert(4, "-").Insert(7, "-").Substring(0, 10)
                    Me.dtpUSTime.Value = CDate(rsUsdt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":"))
                End If
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
                'tpg1 �ʱ�ȭ

                Me.txtBcclsCd.Text = "" : Me.btnUE.Visible = False
                Me.txtBcclsNm.ReadOnly = False : Me.txtBcclsNmS.ReadOnly = False : Me.txtBcclsNmD.ReadOnly = False : Me.txtBcclsNmP.ReadOnly = False

                Me.txtBcclsNm.Text = "" : Me.txtBcclsNmS.Text = "" : Me.txtBcclsNmD.Text = "" : Me.txtBcclsNmP.Text = "" : Me.txtTBcclsNmBP.Text = ""
                Me.txtRegNm.Text = ""
                Me.txtBcclsCd.Text = "" : Me.txtUSDT.Text = "" : Me.txtUEDT.Text = "" : Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.cboBcclsGbn.SelectedIndex = 0 : Me.cboColorGbn.SelectedIndex = 0
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

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.
        sbInitialize()
    End Sub

    'Form�� Dispose�� �������Ͽ� ���� ��� ����� �����մϴ�.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form �����̳ʿ� �ʿ��մϴ�.
    Private components As System.ComponentModel.IContainer

    '����: ���� ���ν����� Windows Form �����̳ʿ� �ʿ��մϴ�.
    'Windows Form �����̳ʸ� ����Ͽ� ������ �� �ֽ��ϴ�.  
    '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
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
    Friend WithEvents lblBcclsNmP As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNmD As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
    Friend WithEvents txtBcclsNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtBcclsNm As System.Windows.Forms.TextBox
    Friend WithEvents lblBcclsNmS As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNmBP As System.Windows.Forms.Label
    Friend WithEvents txtBcclsNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtBcclsNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtTBcclsNmBP As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tpg1 = New System.Windows.Forms.TabPage
        Me.txtUEDT = New System.Windows.Forms.TextBox
        Me.lblUEDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.txtUSDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.lblUSDT = New System.Windows.Forms.Label
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.lblBcclsGbn = New System.Windows.Forms.Label
        Me.cboBcclsGbn = New System.Windows.Forms.ComboBox
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.lblColorGbn = New System.Windows.Forms.Label
        Me.cboColorGbn = New System.Windows.Forms.ComboBox
        Me.lblBcclsNmBP = New System.Windows.Forms.Label
        Me.lblBcclsNmS = New System.Windows.Forms.Label
        Me.txtBcclsNmS = New System.Windows.Forms.TextBox
        Me.txtTBcclsNmBP = New System.Windows.Forms.TextBox
        Me.lblBcclsNmP = New System.Windows.Forms.Label
        Me.txtBcclsNmP = New System.Windows.Forms.TextBox
        Me.lblBcclsNmD = New System.Windows.Forms.Label
        Me.txtBcclsNmD = New System.Windows.Forms.TextBox
        Me.lblBcclsNm = New System.Windows.Forms.Label
        Me.txtBcclsNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.txtBcclsCd = New System.Windows.Forms.TextBox
        Me.lblBcclsCd = New System.Windows.Forms.Label
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker
        Me.txtUSDay = New System.Windows.Forms.TextBox
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker
        Me.lblUSDayTime = New System.Windows.Forms.Label
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
        Me.pnlTop.Size = New System.Drawing.Size(792, 577)
        Me.pnlTop.TabIndex = 116
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tpg1)
        Me.tclSpc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tclSpc.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tclSpc.ItemSize = New System.Drawing.Size(84, 17)
        Me.tclSpc.Location = New System.Drawing.Point(0, 0)
        Me.tclSpc.Name = "tclSpc"
        Me.tclSpc.SelectedIndex = 0
        Me.tclSpc.Size = New System.Drawing.Size(788, 573)
        Me.tclSpc.TabIndex = 0
        Me.tclSpc.TabStop = False
        '
        'tpg1
        '
        Me.tpg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpg1.Controls.Add(Me.txtUEDT)
        Me.tpg1.Controls.Add(Me.lblUEDT)
        Me.tpg1.Controls.Add(Me.txtRegDT)
        Me.tpg1.Controls.Add(Me.txtUSDT)
        Me.tpg1.Controls.Add(Me.lblUserNm)
        Me.tpg1.Controls.Add(Me.lblRegDT)
        Me.tpg1.Controls.Add(Me.lblUSDT)
        Me.tpg1.Controls.Add(Me.grpCdInfo1)
        Me.tpg1.Controls.Add(Me.grpCd)
        Me.tpg1.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpg1.Location = New System.Drawing.Point(4, 21)
        Me.tpg1.Name = "tpg1"
        Me.tpg1.Size = New System.Drawing.Size(780, 548)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "���ڵ�з�����"
        Me.tpg1.UseVisualStyleBackColor = True
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUEDT.Location = New System.Drawing.Point(308, 509)
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
        Me.lblUEDT.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Location = New System.Drawing.Point(208, 509)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(99, 21)
        Me.lblUEDT.TabIndex = 0
        Me.lblUEDT.Tag = ""
        Me.lblUEDT.Text = "�����Ͻ�(����)"
        Me.lblUEDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(496, 509)
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
        Me.txtUSDT.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUSDT.Location = New System.Drawing.Point(104, 509)
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
        Me.lblUserNm.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(599, 509)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(83, 21)
        Me.lblUserNm.TabIndex = 0
        Me.lblUserNm.Text = "���������"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(412, 509)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(83, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "��������Ͻ�"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUSDT
        '
        Me.lblUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUSDT.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDT.ForeColor = System.Drawing.Color.Black
        Me.lblUSDT.Location = New System.Drawing.Point(4, 509)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(99, 21)
        Me.lblUSDT.TabIndex = 0
        Me.lblUSDT.Text = "�����Ͻ�(����)"
        Me.lblUSDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsGbn)
        Me.grpCdInfo1.Controls.Add(Me.cboBcclsGbn)
        Me.grpCdInfo1.Controls.Add(Me.txtRegNm)
        Me.grpCdInfo1.Controls.Add(Me.txtRegID)
        Me.grpCdInfo1.Controls.Add(Me.lblColorGbn)
        Me.grpCdInfo1.Controls.Add(Me.cboColorGbn)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNmBP)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtBcclsNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtTBcclsNmBP)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtBcclsNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtBcclsNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNm)
        Me.grpCdInfo1.Controls.Add(Me.txtBcclsNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(0, 48)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(775, 498)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "�˻�з�����"
        '
        'lblBcclsGbn
        '
        Me.lblBcclsGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsGbn.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsGbn.ForeColor = System.Drawing.Color.White
        Me.lblBcclsGbn.Location = New System.Drawing.Point(314, 61)
        Me.lblBcclsGbn.Name = "lblBcclsGbn"
        Me.lblBcclsGbn.Size = New System.Drawing.Size(61, 21)
        Me.lblBcclsGbn.TabIndex = 17
        Me.lblBcclsGbn.Text = "��ü����"
        Me.lblBcclsGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboBcclsGbn
        '
        Me.cboBcclsGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBcclsGbn.FormattingEnabled = True
        Me.cboBcclsGbn.Items.AddRange(New Object() {"[0] ", "[1] ���հ���", "[2] �̻���", "[3] ��������", "[6] ��Ź��ü", "[7] ��������", "[8] ������", "[9] ������"})
        Me.cboBcclsGbn.Location = New System.Drawing.Point(376, 61)
        Me.cboBcclsGbn.Name = "cboBcclsGbn"
        Me.cboBcclsGbn.Size = New System.Drawing.Size(122, 20)
        Me.cboBcclsGbn.TabIndex = 16
        Me.cboBcclsGbn.Tag = "BCCLSGBN_01"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(683, 461)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(87, 21)
        Me.txtRegNm.TabIndex = 15
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(683, 461)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(58, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'lblColorGbn
        '
        Me.lblColorGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblColorGbn.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblColorGbn.ForeColor = System.Drawing.Color.White
        Me.lblColorGbn.Location = New System.Drawing.Point(314, 39)
        Me.lblColorGbn.Name = "lblColorGbn"
        Me.lblColorGbn.Size = New System.Drawing.Size(61, 21)
        Me.lblColorGbn.TabIndex = 13
        Me.lblColorGbn.Text = "������"
        Me.lblColorGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboColorGbn
        '
        Me.cboColorGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColorGbn.FormattingEnabled = True
        Me.cboColorGbn.Items.AddRange(New Object() {"[0] ���", "[1] �����", "[2] �����", "[3] ��Ȳ��"})
        Me.cboColorGbn.Location = New System.Drawing.Point(376, 39)
        Me.cboColorGbn.Name = "cboColorGbn"
        Me.cboColorGbn.Size = New System.Drawing.Size(122, 20)
        Me.cboColorGbn.TabIndex = 12
        Me.cboColorGbn.Tag = "COLORGBN_01"
        '
        'lblBcclsNmBP
        '
        Me.lblBcclsNmBP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNmBP.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNmBP.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNmBP.Location = New System.Drawing.Point(314, 17)
        Me.lblBcclsNmBP.Name = "lblBcclsNmBP"
        Me.lblBcclsNmBP.Size = New System.Drawing.Size(163, 21)
        Me.lblBcclsNmBP.TabIndex = 0
        Me.lblBcclsNmBP.Text = "���ڵ�з���¸�(���ڵ�)"
        Me.lblBcclsNmBP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBcclsNmS
        '
        Me.lblBcclsNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNmS.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNmS.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNmS.Location = New System.Drawing.Point(8, 38)
        Me.lblBcclsNmS.Name = "lblBcclsNmS"
        Me.lblBcclsNmS.Size = New System.Drawing.Size(126, 21)
        Me.lblBcclsNmS.TabIndex = 5
        Me.lblBcclsNmS.Text = "���ڵ�з���(���)"
        Me.lblBcclsNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBcclsNmS
        '
        Me.txtBcclsNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcclsNmS.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcclsNmS.Location = New System.Drawing.Point(135, 39)
        Me.txtBcclsNmS.MaxLength = 10
        Me.txtBcclsNmS.Name = "txtBcclsNmS"
        Me.txtBcclsNmS.Size = New System.Drawing.Size(128, 21)
        Me.txtBcclsNmS.TabIndex = 2
        Me.txtBcclsNmS.Tag = "BCCLSNMS"
        '
        'txtTBcclsNmBP
        '
        Me.txtTBcclsNmBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTBcclsNmBP.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTBcclsNmBP.Location = New System.Drawing.Point(478, 17)
        Me.txtTBcclsNmBP.MaxLength = 2
        Me.txtTBcclsNmBP.Name = "txtTBcclsNmBP"
        Me.txtTBcclsNmBP.Size = New System.Drawing.Size(20, 21)
        Me.txtTBcclsNmBP.TabIndex = 9
        Me.txtTBcclsNmBP.Tag = "BCCLSNMBP"
        '
        'lblBcclsNmP
        '
        Me.lblBcclsNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNmP.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNmP.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNmP.Location = New System.Drawing.Point(8, 82)
        Me.lblBcclsNmP.Name = "lblBcclsNmP"
        Me.lblBcclsNmP.Size = New System.Drawing.Size(126, 21)
        Me.lblBcclsNmP.TabIndex = 0
        Me.lblBcclsNmP.Text = "���ڵ�з���(���)"
        Me.lblBcclsNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBcclsNmP
        '
        Me.txtBcclsNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcclsNmP.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcclsNmP.Location = New System.Drawing.Point(135, 83)
        Me.txtBcclsNmP.MaxLength = 20
        Me.txtBcclsNmP.Name = "txtBcclsNmP"
        Me.txtBcclsNmP.Size = New System.Drawing.Size(128, 21)
        Me.txtBcclsNmP.TabIndex = 4
        Me.txtBcclsNmP.Tag = "BCCLSNMP"
        '
        'lblBcclsNmD
        '
        Me.lblBcclsNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNmD.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNmD.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNmD.Location = New System.Drawing.Point(8, 60)
        Me.lblBcclsNmD.Name = "lblBcclsNmD"
        Me.lblBcclsNmD.Size = New System.Drawing.Size(126, 21)
        Me.lblBcclsNmD.TabIndex = 0
        Me.lblBcclsNmD.Text = "���ڵ�з���(ȭ��)"
        Me.lblBcclsNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBcclsNmD
        '
        Me.txtBcclsNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcclsNmD.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcclsNmD.Location = New System.Drawing.Point(135, 61)
        Me.txtBcclsNmD.MaxLength = 20
        Me.txtBcclsNmD.Name = "txtBcclsNmD"
        Me.txtBcclsNmD.Size = New System.Drawing.Size(128, 21)
        Me.txtBcclsNmD.TabIndex = 3
        Me.txtBcclsNmD.Tag = "BCCLSNMD"
        '
        'lblBcclsNm
        '
        Me.lblBcclsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNm.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNm.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNm.Location = New System.Drawing.Point(8, 16)
        Me.lblBcclsNm.Name = "lblBcclsNm"
        Me.lblBcclsNm.Size = New System.Drawing.Size(126, 21)
        Me.lblBcclsNm.TabIndex = 0
        Me.lblBcclsNm.Text = "���ڵ�з���"
        Me.lblBcclsNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBcclsNm
        '
        Me.txtBcclsNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcclsNm.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcclsNm.Location = New System.Drawing.Point(135, 17)
        Me.txtBcclsNm.MaxLength = 20
        Me.txtBcclsNm.Name = "txtBcclsNm"
        Me.txtBcclsNm.Size = New System.Drawing.Size(128, 21)
        Me.txtBcclsNm.TabIndex = 1
        Me.txtBcclsNm.Tag = "BCCLSNM"
        '
        'grpCd
        '
        Me.grpCd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.txtBcclsCd)
        Me.grpCd.Controls.Add(Me.lblBcclsCd)
        Me.grpCd.Controls.Add(Me.dtpUSTime)
        Me.grpCd.Controls.Add(Me.txtUSDay)
        Me.grpCd.Controls.Add(Me.dtpUSDay)
        Me.grpCd.Controls.Add(Me.lblUSDayTime)
        Me.grpCd.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(0, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(776, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        Me.grpCd.Text = "�˻�з� �ڵ�"
        '
        'btnUE
        '
        Me.btnUE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(698, 10)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 6
        Me.btnUE.Text = "�������"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'txtBcclsCd
        '
        Me.txtBcclsCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcclsCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBcclsCd.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcclsCd.Location = New System.Drawing.Point(399, 16)
        Me.txtBcclsCd.MaxLength = 2
        Me.txtBcclsCd.Name = "txtBcclsCd"
        Me.txtBcclsCd.Size = New System.Drawing.Size(78, 21)
        Me.txtBcclsCd.TabIndex = 10
        Me.txtBcclsCd.Tag = "BCCLSCD"
        '
        'lblBcclsCd
        '
        Me.lblBcclsCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBcclsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsCd.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsCd.ForeColor = System.Drawing.Color.White
        Me.lblBcclsCd.Location = New System.Drawing.Point(306, 16)
        Me.lblBcclsCd.Name = "lblBcclsCd"
        Me.lblBcclsCd.Size = New System.Drawing.Size(107, 21)
        Me.lblBcclsCd.TabIndex = 7
        Me.lblBcclsCd.Text = "���ڵ�з��ڵ�"
        Me.lblBcclsCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(193, 15)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 3
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(94, 15)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(77, 21)
        Me.txtUSDay.TabIndex = 1
        Me.txtUSDay.Tag = ""
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(172, 15)
        Me.dtpUSDay.Name = "dtpUSDay"
        Me.dtpUSDay.Size = New System.Drawing.Size(20, 21)
        Me.dtpUSDay.TabIndex = 2
        Me.dtpUSDay.TabStop = False
        Me.dtpUSDay.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'lblUSDayTime
        '
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Location = New System.Drawing.Point(8, 15)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(85, 21)
        Me.lblUSDayTime.TabIndex = 0
        Me.lblUSDayTime.Text = "�����Ͻ�"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FDF01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 577)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF01"
        Me.Text = "[01] ���ڵ�з�"
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

        If Me.txtBcclsCd.Text = "" Then Exit Sub

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("�̹� �������� �׸��Դϴ�. Ȯ���Ͽ� �ֽʽÿ�!!")
                Return
            End If

            Dim sMsg As String = "   ���ڵ�з��ڵ�   : " + Me.txtBcclsCd.Text + vbCrLf
            sMsg += "   ���ڵ�з���     : " + Me.txtBcclsNm.Text + vbCrLf
            sMsg += "   ��(��) ��������Ͻðڽ��ϱ�?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mobjDAF.TransBcclsInfo_UE(Me.txtBcclsCd.Text, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID, sUeDate + sUeTime) Then
                MsgBox("�ش� ���ڵ�з������� ������� �Ǿ����ϴ�!!", MsgBoxStyle.Information)

                sbInitialize()
                CType(Me.Owner, FGF01).sbDeleteCdList()
            Else
                MsgBox("������ῡ �����Ͽ����ϴ�!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub dtpUSDay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUSDay.ValueChanged
        If miSelectKey = 1 Then Exit Sub
        If Me.txtUSDay.Text.Trim = "" Then Exit Sub

        Me.txtUSDay.Text = Format(dtpUSDay.Value, "yyyy-MM-dd").Substring(0, 10)
    End Sub


    Private Sub FDF01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select
    End Sub


    Private Sub txtBCCLSNM_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBcclsNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If Me.txtBcclsNmS.Text.Trim = "" Then
            If Me.txtBcclsNm.Text.Length > Me.txtBcclsNmS.MaxLength Then
                Me.txtBcclsNmS.Text = Me.txtBcclsNm.Text.Substring(0, Me.txtBcclsNmS.MaxLength)
            Else
                Me.txtBcclsNmS.Text = txtBcclsNm.Text
            End If
        End If

        If Me.txtBcclsNmD.Text.Trim = "" Then
            If Me.txtBcclsNm.Text.Length > Me.txtBcclsNmD.MaxLength Then
                Me.txtBcclsNmD.Text = Me.txtBcclsNm.Text.Substring(0, txtBcclsNmD.MaxLength)
            Else
                Me.txtBcclsNmD.Text = Me.txtBcclsNm.Text
            End If
        End If

        If Me.txtBcclsNmP.Text.Trim = "" Then
            If Me.txtBcclsNm.Text.Length > Me.txtBcclsNmP.MaxLength Then
                Me.txtBcclsNmP.Text = Me.txtBcclsNm.Text.Substring(0, txtBcclsNmP.MaxLength)
            Else
                Me.txtBcclsNmP.Text = Me.txtBcclsNm.Text
            End If
        End If
    End Sub

    Private Sub txtBcclsCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcclsCd.KeyDown, txtBcclsNm.KeyDown, txtBcclsNmD.KeyDown, txtBcclsNmP.KeyDown, txtBcclsNmS.KeyDown, txtTBcclsNmBP.KeyDown, cboBcclsGbn.KeyDown, cboColorGbn.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub
End Class
