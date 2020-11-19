'>>> [16] 배양균
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports common.commlogin.login
Imports COMMON.CommConst

Public Class FDF16
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF16.vb, Class : FDF16" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_BAC
    Friend WithEvents btnGetExcel As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents lblSameCd As System.Windows.Forms.Label
    Friend WithEvents txtSameCd As System.Windows.Forms.TextBox

    Private Sub sbEditUseDt_Edit(ByVal rsUseTag As String, ByVal rsUseDt As String)
        Dim sFn As String = "Sub sbEditUseDt_Edit"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            rsUseDt = rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", "")

            '> 사용중복 조사
            dt = mobjDAF.GetUsUeDupl_Bac(Me.txtBacCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransTestInfo_UPD_US(Me.txtBacCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransTestInfo_UPD_UE(Me.txtBacCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseDt)
            End If

            If bReturn Then
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + "가 수정되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + " 수정에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            dt = mobjDAF.GetUsUeCd_Bac(Me.txtBacCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransTestInfo_DEL(Me.txtBacCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

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

    Public Sub sbEditUseDt(ByVal rsUseTag As String)
        Dim sFn As String = "Public Sub sbEditUseDt"

        Try
            Dim fgf02 As New FGF03

            With fgf02
                .txtCd.Text = Me.txtBacCd.Text
                .txtNm.Text = Me.txtBacNm.Text

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
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.AccessibleName = ""

        End Try
    End Sub

    Private Function fnCollectItemTable_210(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_210(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it210 As New LISAPP.ItemTableCollection

            With it210
                .SetItemTable("BACCD", 1, 1, Me.txtBacCd.Text)
                .SetItemTable("USDT", 2, 1, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)

                If txtUEDT.Text = "" Then
                    .SetItemTable("UEDT", 3, 1, msUEDT)
                Else
                    .SetItemTable("UEDT", 3, 1, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                End If

                .SetItemTable("REGDT", 4, 1, rsRegDT)
                .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                .SetItemTable("REGIP", 6, 1, USER_INFO.LOCALIP)
                .SetItemTable("BACNM", 7, 1, Me.txtBacNm.Text)
                .SetItemTable("BACNMS", 8, 1, Me.txtBacNmS.Text)
                .SetItemTable("BACNMD", 9, 1, Me.txtBacNmD.Text)
                .SetItemTable("BACNMP", 10, 1, Me.txtBacNmP.Text)
                .SetItemTable("BACGENCD", 11, 1, Ctrl.Get_Code(Me.cboBacgen))
                .SetItemTable("BACIFCD", 12, 1, Me.txtIFCd.Text)
                .SetItemTable("BACWNCD", 13, 1, Me.txtWNCd.Text)
                .SetItemTable("SAMECD", 14, 1, Me.txtSameCd.Text)
            End With

            fnCollectItemTable_210 = it210
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            fnCollectItemTable_210 = New LISAPP.ItemTableCollection
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it210 As New LISAPP.ItemTableCollection
            Dim iRegType210 As Integer = 0
            Dim sRegDT As String

            iRegType210 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it210 = fnCollectItemTable_210(sRegDT)

            If mobjDAF.TransBacInfo(it210, iRegType210, Me.txtBacCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsBacCd As String, ByVal rsUSDT As String) As String
        Dim sFn As String = ""

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetRecentBacInfo(rsBacCd, rsUSDT)

            If DTable.Rows.Count > 0 Then
                Return "시작일시가 " + DTable.Rows(0).Item(0).ToString + "인 동일 " + Me.lblBacCd.Text + "가 존재합니다." + vbCrLf + vbCrLf + _
                       "시작일시를 재조정 하십시요!!"
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
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

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
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")
        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Len(Me.txtBacCd.Text.Trim) < 1 Then
                MsgBox("배양균코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtBacNm.Text.Trim) < 1 Then
                MsgBox("배양균명을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtBacNmS.Text.Trim) < 1 Then
                MsgBox("배양균명(약어)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtBacNmD.Text.Trim) < 1 Then
                MsgBox("배양균명(화면)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtBacNmP.Text.Trim) < 1 Then
                MsgBox("배양균명(출력)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Ctrl.Get_Code(Me.cboBacgen) = "" Then
                MsgBox("배양균속을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsDate(Me.txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtBacCd.Text, txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""))

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

    Public Sub sbDisplayCdDetail(ByVal rsBacCd As String, ByVal rsUsDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                If Not CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    sbDisplayCdList_Ref()
                End If
            End If

            sbDisplayCdDetail_Bac(rsBacCd, rsUsDt)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Bac(ByVal rsBacCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Bac(String, String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As New DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            dt = mobjDAF.GetBacInfo(rsBacCd, rsUsDt)

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

            If Not IsNothing(Me.Owner) Then
                If Not CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Me.txtUSDay.Text = rsUsDt.Insert(4, "-").Insert(7, "-").Substring(0, 10)
                    Me.dtpUSTime.Value = CDate(rsUsDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":"))
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref()
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref(ByVal asUSDT As String, Optional ByVal asUEDT As String)"

        Try
            miSelectKey = 1

            sbDisplayCdList_Ref_Bacgen(Me.cboBacgen)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_Bacgen(ByVal actrl As System.Windows.Forms.ComboBox)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_Bacgen(ByVal actrl As System.Windows.Forms.ComboBox, ByVal asUSDT As String, ByVal asUEDT As String)"

        Try
            Dim dt As DataTable = mobjDAF.GetBacgenInfo()

            actrl.Items.Clear()

            If dt.Rows.Count < 0 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("bacgen"))
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRID = "ACK" Then btnGetExcel.Visible = True

            If USER_INFO.USRLVL = "S" Then      '권한이 있어야 "사용종료"를 할 수 있음
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

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
                Me.txtBacCd.Text = "" : Me.btnUE.Visible = False
                Me.txtBacNm.Text = "" : Me.txtBacNmS.Text = "" : Me.txtBacNmD.Text = "" : Me.txtBacNmP.Text = ""
                Me.cboBacgen.SelectedIndex = -1
                Me.txtIFCd.Text = "" : Me.txtWNCd.Text = "" : Me.txtSameCd.Text = ""
                Me.txtUSDT.Text = "" : Me.txtUEDT.Text = "" : Me.txtRegDT.Text = "" : Me.txtRegID.Text = "" : Me.txtRegNm.Text = ""
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

            txtUSDay.Text = sSysDT.Substring(0, 10)
            dtpUSDay.Value = CType(sSysDT, Date)
            dtpUSTime.Value = CType(sSysDT, Date)

            '신규 시작일시에 맞는 CdList를 불러옴
            sbDisplayCdList_Ref()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage As System.Windows.Forms.TabPage
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents txtWNCd As System.Windows.Forms.TextBox
    Friend WithEvents lblWNCd As System.Windows.Forms.Label
    Friend WithEvents txtIFCd As System.Windows.Forms.TextBox
    Friend WithEvents lblIFCd As System.Windows.Forms.Label
    Friend WithEvents lblBacgen As System.Windows.Forms.Label
    Friend WithEvents txtBacCd As System.Windows.Forms.TextBox
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtBacNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtBacNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtBacNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtBacNm As System.Windows.Forms.TextBox
    Friend WithEvents lblBacCd As System.Windows.Forms.Label
    Friend WithEvents lblBacNmS As System.Windows.Forms.Label
    Friend WithEvents lblBacNmP As System.Windows.Forms.Label
    Friend WithEvents lblBacNmD As System.Windows.Forms.Label
    Friend WithEvents lblBacNm As System.Windows.Forms.Label
    Friend WithEvents cboBacgen As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnGetExcel = New System.Windows.Forms.Button
        Me.txtBacCd = New System.Windows.Forms.TextBox
        Me.lblBacCd = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker
        Me.txtUSDay = New System.Windows.Forms.TextBox
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker
        Me.lblUSDayTime = New System.Windows.Forms.Label
        Me.txtUEDT = New System.Windows.Forms.TextBox
        Me.lblUEDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.txtUSDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.lblUSDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.lblSameCd = New System.Windows.Forms.Label
        Me.txtSameCd = New System.Windows.Forms.TextBox
        Me.lblWNCd = New System.Windows.Forms.Label
        Me.lblIFCd = New System.Windows.Forms.Label
        Me.cboBacgen = New System.Windows.Forms.ComboBox
        Me.lblBacgen = New System.Windows.Forms.Label
        Me.lblBacNmS = New System.Windows.Forms.Label
        Me.txtBacNmS = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblBacNmP = New System.Windows.Forms.Label
        Me.txtBacNmP = New System.Windows.Forms.TextBox
        Me.lblBacNmD = New System.Windows.Forms.Label
        Me.txtBacNmD = New System.Windows.Forms.TextBox
        Me.lblBacNm = New System.Windows.Forms.Label
        Me.txtBacNm = New System.Windows.Forms.TextBox
        Me.txtIFCd = New System.Windows.Forms.TextBox
        Me.txtWNCd = New System.Windows.Forms.TextBox
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(792, 605)
        Me.Panel1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(788, 601)
        Me.TabControl1.TabIndex = 999
        Me.TabControl1.TabStop = False
        '
        'TabPage
        '
        Me.TabPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage.Controls.Add(Me.txtRegNm)
        Me.TabPage.Controls.Add(Me.grpCd)
        Me.TabPage.Controls.Add(Me.txtUEDT)
        Me.TabPage.Controls.Add(Me.lblUEDT)
        Me.TabPage.Controls.Add(Me.txtRegDT)
        Me.TabPage.Controls.Add(Me.txtUSDT)
        Me.TabPage.Controls.Add(Me.lblUserNm)
        Me.TabPage.Controls.Add(Me.lblRegDT)
        Me.TabPage.Controls.Add(Me.lblUSDT)
        Me.TabPage.Controls.Add(Me.txtRegID)
        Me.TabPage.Controls.Add(Me.grpCdInfo1)
        Me.TabPage.Location = New System.Drawing.Point(4, 21)
        Me.TabPage.Name = "TabPage"
        Me.TabPage.Size = New System.Drawing.Size(780, 576)
        Me.TabPage.TabIndex = 0
        Me.TabPage.Text = "배양균정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(702, 548)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 138
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnGetExcel)
        Me.grpCd.Controls.Add(Me.txtBacCd)
        Me.grpCd.Controls.Add(Me.lblBacCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.dtpUSTime)
        Me.grpCd.Controls.Add(Me.txtUSDay)
        Me.grpCd.Controls.Add(Me.dtpUSDay)
        Me.grpCd.Controls.Add(Me.lblUSDayTime)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(9, 10)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 12
        Me.grpCd.TabStop = False
        '
        'btnGetExcel
        '
        Me.btnGetExcel.Location = New System.Drawing.Point(605, 13)
        Me.btnGetExcel.Name = "btnGetExcel"
        Me.btnGetExcel.Size = New System.Drawing.Size(75, 23)
        Me.btnGetExcel.TabIndex = 4
        Me.btnGetExcel.TabStop = False
        Me.btnGetExcel.Text = "Excel"
        Me.btnGetExcel.UseVisualStyleBackColor = True
        Me.btnGetExcel.Visible = False
        '
        'txtBacCd
        '
        Me.txtBacCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtBacCd.Location = New System.Drawing.Point(359, 16)
        Me.txtBacCd.MaxLength = 6
        Me.txtBacCd.Name = "txtBacCd"
        Me.txtBacCd.Size = New System.Drawing.Size(76, 21)
        Me.txtBacCd.TabIndex = 3
        Me.txtBacCd.Tag = "BACCD"
        '
        'lblBacCd
        '
        Me.lblBacCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBacCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacCd.ForeColor = System.Drawing.Color.White
        Me.lblBacCd.Location = New System.Drawing.Point(278, 16)
        Me.lblBacCd.Name = "lblBacCd"
        Me.lblBacCd.Size = New System.Drawing.Size(80, 21)
        Me.lblBacCd.TabIndex = 7
        Me.lblBacCd.Text = "배양균코드"
        Me.lblBacCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(686, 11)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 5
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(199, 15)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 2
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(106, 15)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(72, 21)
        Me.txtUSDay.TabIndex = 1
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(179, 15)
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
        Me.lblUSDayTime.Size = New System.Drawing.Size(97, 21)
        Me.lblUSDayTime.TabIndex = 0
        Me.lblUSDayTime.Text = "시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUEDT.Location = New System.Drawing.Point(311, 548)
        Me.txtUEDT.Name = "txtUEDT"
        Me.txtUEDT.ReadOnly = True
        Me.txtUEDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUEDT.TabIndex = 9
        Me.txtUEDT.TabStop = False
        Me.txtUEDT.Tag = "UEDT"
        '
        'lblUEDT
        '
        Me.lblUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUEDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Location = New System.Drawing.Point(213, 548)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUEDT.TabIndex = 8
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
        Me.txtRegDT.Location = New System.Drawing.Point(510, 548)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 11
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'txtUSDT
        '
        Me.txtUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUSDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUSDT.Location = New System.Drawing.Point(106, 548)
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUSDT.TabIndex = 10
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
        Me.lblUserNm.TabIndex = 5
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(425, 548)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 4
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUSDT
        '
        Me.lblUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDT.ForeColor = System.Drawing.Color.Black
        Me.lblUSDT.Location = New System.Drawing.Point(8, 548)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUSDT.TabIndex = 7
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
        Me.txtRegID.TabIndex = 6
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.lblSameCd)
        Me.grpCdInfo1.Controls.Add(Me.txtSameCd)
        Me.grpCdInfo1.Controls.Add(Me.lblWNCd)
        Me.grpCdInfo1.Controls.Add(Me.lblIFCd)
        Me.grpCdInfo1.Controls.Add(Me.cboBacgen)
        Me.grpCdInfo1.Controls.Add(Me.lblBacgen)
        Me.grpCdInfo1.Controls.Add(Me.lblBacNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtBacNmS)
        Me.grpCdInfo1.Controls.Add(Me.Label10)
        Me.grpCdInfo1.Controls.Add(Me.lblBacNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtBacNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblBacNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtBacNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblBacNm)
        Me.grpCdInfo1.Controls.Add(Me.txtBacNm)
        Me.grpCdInfo1.Controls.Add(Me.txtIFCd)
        Me.grpCdInfo1.Controls.Add(Me.txtWNCd)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 65)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 467)
        Me.grpCdInfo1.TabIndex = 3
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "배양균정보"
        '
        'lblSameCd
        '
        Me.lblSameCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSameCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSameCd.ForeColor = System.Drawing.Color.White
        Me.lblSameCd.Location = New System.Drawing.Point(8, 196)
        Me.lblSameCd.Name = "lblSameCd"
        Me.lblSameCd.Size = New System.Drawing.Size(97, 21)
        Me.lblSameCd.TabIndex = 138
        Me.lblSameCd.Text = "대표코드(통계)"
        Me.lblSameCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSameCd
        '
        Me.txtSameCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSameCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSameCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSameCd.Location = New System.Drawing.Point(106, 196)
        Me.txtSameCd.MaxLength = 6
        Me.txtSameCd.Name = "txtSameCd"
        Me.txtSameCd.Size = New System.Drawing.Size(76, 21)
        Me.txtSameCd.TabIndex = 13
        Me.txtSameCd.Tag = "SAMECD"
        '
        'lblWNCd
        '
        Me.lblWNCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWNCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWNCd.ForeColor = System.Drawing.Color.White
        Me.lblWNCd.Location = New System.Drawing.Point(8, 174)
        Me.lblWNCd.Name = "lblWNCd"
        Me.lblWNCd.Size = New System.Drawing.Size(97, 21)
        Me.lblWNCd.TabIndex = 130
        Me.lblWNCd.Text = "WHONET코드"
        Me.lblWNCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIFCd
        '
        Me.lblIFCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIFCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIFCd.ForeColor = System.Drawing.Color.White
        Me.lblIFCd.Location = New System.Drawing.Point(8, 152)
        Me.lblIFCd.Name = "lblIFCd"
        Me.lblIFCd.Size = New System.Drawing.Size(97, 21)
        Me.lblIFCd.TabIndex = 131
        Me.lblIFCd.Text = "IF코드"
        Me.lblIFCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboBacgen
        '
        Me.cboBacgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBacgen.Location = New System.Drawing.Point(106, 104)
        Me.cboBacgen.MaxDropDownItems = 20
        Me.cboBacgen.Name = "cboBacgen"
        Me.cboBacgen.Size = New System.Drawing.Size(364, 20)
        Me.cboBacgen.TabIndex = 10
        Me.cboBacgen.Tag = "BACGENNMD_01"
        '
        'lblBacgen
        '
        Me.lblBacgen.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacgen.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacgen.ForeColor = System.Drawing.Color.White
        Me.lblBacgen.Location = New System.Drawing.Point(8, 104)
        Me.lblBacgen.Name = "lblBacgen"
        Me.lblBacgen.Size = New System.Drawing.Size(97, 21)
        Me.lblBacgen.TabIndex = 134
        Me.lblBacgen.Text = "배양균속코드"
        Me.lblBacgen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBacNmS
        '
        Me.lblBacNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacNmS.ForeColor = System.Drawing.Color.White
        Me.lblBacNmS.Location = New System.Drawing.Point(8, 38)
        Me.lblBacNmS.Name = "lblBacNmS"
        Me.lblBacNmS.Size = New System.Drawing.Size(97, 21)
        Me.lblBacNmS.TabIndex = 5
        Me.lblBacNmS.Text = "배양균명(약어)"
        Me.lblBacNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBacNmS
        '
        Me.txtBacNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBacNmS.Location = New System.Drawing.Point(106, 38)
        'Me.txtBacNmS.MaxLength = 30
        Me.txtBacNmS.MaxLength = 90
        Me.txtBacNmS.Name = "txtBacNmS"
        Me.txtBacNmS.Size = New System.Drawing.Size(364, 21)
        Me.txtBacNmS.TabIndex = 7
        Me.txtBacNmS.Tag = "BACNMS"
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 255)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(756, 2)
        Me.Label10.TabIndex = 0
        '
        'lblBacNmP
        '
        Me.lblBacNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacNmP.ForeColor = System.Drawing.Color.White
        Me.lblBacNmP.Location = New System.Drawing.Point(8, 82)
        Me.lblBacNmP.Name = "lblBacNmP"
        Me.lblBacNmP.Size = New System.Drawing.Size(97, 21)
        Me.lblBacNmP.TabIndex = 0
        Me.lblBacNmP.Text = "배양균명(출력)"
        Me.lblBacNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBacNmP
        '
        Me.txtBacNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBacNmP.Location = New System.Drawing.Point(106, 82)
        Me.txtBacNmP.MaxLength = 90
        Me.txtBacNmP.Name = "txtBacNmP"
        Me.txtBacNmP.Size = New System.Drawing.Size(364, 21)
        Me.txtBacNmP.TabIndex = 9
        Me.txtBacNmP.Tag = "BACNMP"
        '
        'lblBacNmD
        '
        Me.lblBacNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacNmD.ForeColor = System.Drawing.Color.White
        Me.lblBacNmD.Location = New System.Drawing.Point(8, 60)
        Me.lblBacNmD.Name = "lblBacNmD"
        Me.lblBacNmD.Size = New System.Drawing.Size(97, 21)
        Me.lblBacNmD.TabIndex = 0
        Me.lblBacNmD.Text = "배양균명(화면)"
        Me.lblBacNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBacNmD
        '
        Me.txtBacNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBacNmD.Location = New System.Drawing.Point(106, 60)
        Me.txtBacNmD.MaxLength = 90
        Me.txtBacNmD.Name = "txtBacNmD"
        Me.txtBacNmD.Size = New System.Drawing.Size(364, 21)
        Me.txtBacNmD.TabIndex = 8
        Me.txtBacNmD.Tag = "BACNMD"
        '
        'lblBacNm
        '
        Me.lblBacNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacNm.ForeColor = System.Drawing.Color.White
        Me.lblBacNm.Location = New System.Drawing.Point(8, 16)
        Me.lblBacNm.Name = "lblBacNm"
        Me.lblBacNm.Size = New System.Drawing.Size(97, 21)
        Me.lblBacNm.TabIndex = 0
        Me.lblBacNm.Text = "배양균명"
        Me.lblBacNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBacNm
        '
        Me.txtBacNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBacNm.Location = New System.Drawing.Point(106, 16)
        '20191230nbm 배양균명 길이수정(60->90)
        Me.txtBacNm.MaxLength = 90
        Me.txtBacNm.Name = "txtBacNm"
        Me.txtBacNm.Size = New System.Drawing.Size(364, 21)
        Me.txtBacNm.TabIndex = 6
        Me.txtBacNm.Tag = "BACNM"
        '
        'txtIFCd
        '
        Me.txtIFCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIFCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtIFCd.Location = New System.Drawing.Point(106, 152)
        Me.txtIFCd.MaxLength = 10
        Me.txtIFCd.Name = "txtIFCd"
        Me.txtIFCd.Size = New System.Drawing.Size(76, 21)
        Me.txtIFCd.TabIndex = 11
        Me.txtIFCd.Tag = "BACIFCD"
        '
        'txtWNCd
        '
        Me.txtWNCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWNCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWNCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWNCd.Location = New System.Drawing.Point(106, 174)
        Me.txtWNCd.MaxLength = 10
        Me.txtWNCd.Name = "txtWNCd"
        Me.txtWNCd.Size = New System.Drawing.Size(76, 21)
        Me.txtWNCd.TabIndex = 12
        Me.txtWNCd.Tag = "BACWNCD"
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF16
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "FDF16"
        Me.Text = "[16] 배양균"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage.ResumeLayout(False)
        Me.TabPage.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If txtBacCd.Text = "" Then Exit Sub

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "배양균코드 : " & txtBacCd.Text & vbCrLf
            sMsg &= "배양균명   : " & txtBacNm.Text & vbCrLf & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mobjDAF.TransBacInfo_UE(txtBacCd.Text, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, sUeDate + sUeTime) Then
                MsgBox("해당 배양균정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub dtpUSDay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUSDay.ValueChanged
        If miSelectKey = 1 Then Exit Sub
        If txtUSDay.Text.Trim = "" Then Exit Sub

        txtUSDay.Text = Format(dtpUSDay.Value, "yyyy-MM-dd").Substring(0, 10)

        If IsNothing(Me.Owner) Then Exit Sub

        If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
            sbDisplayCdList_Ref()
        End If
    End Sub

    Private Sub txtBacNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBacNm.Validating
        If miSelectKey = 1 Then Exit Sub


        If txtBacNmS.Text.Trim = "" Then
            If txtBacNm.Text.Length > txtBacNmS.MaxLength Then
                txtBacNmS.Text = txtBacNm.Text.Substring(0, txtBacNmS.MaxLength)
            Else
                txtBacNmS.Text = txtBacNm.Text
            End If
        End If

        If txtBacNmD.Text.Trim = "" Then
            If txtBacNm.Text.Length > txtBacNmD.MaxLength Then
                txtBacNmD.Text = txtBacNm.Text.Substring(0, txtBacNmD.MaxLength)
            Else
                txtBacNmD.Text = txtBacNm.Text
            End If
        End If

        If txtBacNmP.Text.Trim = "" Then
            If txtBacNm.Text.Length > txtBacNmP.MaxLength Then
                txtBacNmP.Text = txtBacNm.Text.Substring(0, txtBacNmP.MaxLength)
            Else
                txtBacNmP.Text = txtBacNm.Text
            End If
        End If
    End Sub

    Private Sub txtUSDay_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUSDay.TextChanged
        If miSelectKey = 1 Then Exit Sub
        If txtUSDay.Text.Trim = "" Then Exit Sub
        If Not IsDate(txtUSDay.Text) Then Exit Sub
        If IsNothing(Me.Owner) Then Exit Sub
        If Not txtUSDay.Text.Length = txtUSDay.MaxLength Then Exit Sub

        If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
            sbDisplayCdList_Ref()

            If Not txtUSDT.Text.Trim = "" Then
                If DateDiff(DateInterval.Second, CDate(txtUSDT.Text), CDate(txtUSDay.Text & " " & Format(dtpUSTime.Value, "HH:mm:ss"))) <= 0 Then
                    Dim sMsg As String = "시작일시가 시작일시(선택)보다 같거나 이전입니다. 이런 경우에는 신규로 등록하실 수 없습니다!!" & vbCrLf
                    sMsg &= "시작일시를 다시 설정하십시요!!"

                    MsgBox(sMsg)

                    sbSetNewUSDT()
                End If
            End If
        End If
    End Sub

    Private Sub btnGetExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetExcel.Click
        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Dim intLine As Integer = 2

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim dt As New DataTable

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open("c:\as\균코드.xls")

            xlsWkS = CType(xlsWkB.Sheets("phoenix균명"), Excel.Worksheet)

            For iLine As Integer = 2 To 1909
                Dim sUSDT As String = "20100101000000"
                Dim sUEDT As String = "30000101000000"

                Dim sBacCd As String = xlsWkS.Range("A" + CStr(iLine)).Value.ToString
                Dim sBacNms As String = xlsWkS.Range("B" + CStr(iLine)).Value.ToString
                Dim sBacNmd As String = xlsWkS.Range("C" + CStr(iLine)).Value.ToString

                dt = mobjDAF.GetBacInfo(sBacCd, sUSDT)
                If dt.Rows.Count < 1 Then

                    Dim it210 As New LISAPP.ItemTableCollection

                    Dim sRegDT As String = fnGetSystemDT()

                    With it210
                        .SetItemTable("BACCD", 1, 1, sBacCd)
                        .SetItemTable("USDT", 2, 1, sUSDT)
                        .SetItemTable("UEDT", 3, 1, sUEDT)

                        .SetItemTable("REGDT", 4, 1, "20110623000000")
                        .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                        .SetItemTable("REGIP", 6, 1, USER_INFO.LOCALIP)
                        .SetItemTable("BACNM", 7, 1, sBacNmd)
                        .SetItemTable("BACNMS", 8, 1, sBacNms)
                        .SetItemTable("BACNMD", 9, 1, sBacNmd)
                        .SetItemTable("BACNMP", 10, 1, sBacNmd)
                        .SetItemTable("BACGENCD", 11, 1, "--")
                        .SetItemTable("BACIFCD", 12, 1, sBacCd)
                        .SetItemTable("BACWNCD", 13, 1, "")
                    End With

                    mobjDAF.TransBacInfo(it210, 0, sBacCd, sUSDT)
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

    Private Sub FDF16_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtBacCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBacCd.KeyDown, txtBacNm.KeyDown, txtBacNmD.KeyDown, txtBacNmP.KeyDown, txtBacNmS.KeyDown, txtIFCd.KeyDown, txtSameCd.KeyDown, txtWNCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

            SendKeys.Send("{TAB}")

    End Sub
End Class
