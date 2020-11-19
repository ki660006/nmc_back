'>>> [53] 병원체 검사 코드 마스터 
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF54
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF53.vb, Class : FDF53" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0        'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_REF

    Friend WithEvents cboGroup As System.Windows.Forms.ComboBox
    Friend WithEvents lblColorGbn As System.Windows.Forms.Label
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents BtnDel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents spdFile As AxFPSpreadADO.AxfpSpread
    Friend WithEvents ofdFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox

    Private Sub sbEditUseDt_Edit(ByVal rsUseTag As String, ByVal rsUseDt As String)
        Dim sFn As String = "Sub sbEditUseDt_Edit"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 사용중복 조사
            'dt = mobjDAF.GetUsUeDupl_bccls(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", ""), rsUseTag.ToUpper, rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                'bReturn = mobjDAF.TransBcclsInfo_UPD_US(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", ""))
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                'bReturn = mobjDAF.TransBcclsInfo_UPD_UE(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", ""))
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

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            'dt = mobjDAF.GetUsUeCd_bccls(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            'bReturn = mobjDAF.TransBcclsInfo_DEL(Me.txtBcclsCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

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

    Public Sub sbEditUseDt(ByVal rsUseTag As String)
        Dim sFn As String = "Public Sub sbEditUseDt"

        Try
            Dim fgf03 As New FGF03

            With fgf03
                .txtCd.Text = "" 'Me.txtBcclsCd.Text
                .txtNm.Text = Me.txtRefcd.Text

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

    Private Function fnCollectItemTable_54(ByVal rsRegDt As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_10() As LISAPP.ItemTableCollection"

        Try
            Dim it53 As New LISAPP.ItemTableCollection
            Dim sFilenm As String = ""
            If cboGroup.SelectedIndex < 0 Then cboGroup.SelectedIndex = 0

            With it53

                For i As Integer = 1 To spdFile.MaxRows



                    .SetItemTable("regdt", 1, 1, rsRegDt)
                    .SetItemTable("regid", 2, 1, USER_INFO.USRID)

                    spdFile.Row = i
                    spdFile.Col = spdFile.GetColFromID("filenm")
                    If IO.File.Exists(sFilenm) Then .SetItemTable("filenm", 3, 1, spdFile.Text)

                Next



            End With

            Return it53
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
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")
        End Try

    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnRegSpc() As Boolean"

        Try
            Dim it54 As New LISAPP.ItemTableCollection
            Dim iRegType10 As Integer = 0, iRegType11 As Integer = 0
            Dim sRegDT As String

            iRegType10 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType11 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()
            CType(cboGroup.SelectedItem, String).Substring(1, CType(cboGroup.SelectedItem, String).IndexOf("]") - 1)
            it54 = fnCollectItemTable_54(sRegDT)

            If mobjDAF.TransTestDOc(it54, iRegType10, Me.txtRefcd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    'Private Function fnFindConflict(ByVal rsSlipCd As String, ByVal rsUsDt As String) As String
    '    Dim sFn As String = ""

    '    Try
    '        Dim dt As DataTable = mobjDAF.GetRecentSlipInfo(rsPartCd, rsSlipCd, rsUsDt)

    '        If dt.Rows.Count > 0 Then
    '            Return "시작일시가 " + dt.Rows(0).Item(0).ToString + "인 동일 " + dt.Rows(0).Item(1).ToString + " (분야)슬립 코드가 존재합니다." + vbCrLf + vbCrLf + _
    '                   "부서코드,분야코드 또는 시작일시를 재조정하십시오!!"
    '        Else
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        Fn.log(mcFile + sFn, Err)
    '        MsgBox(mcFile + sFn + vbCrLf + ex.Message)

    '        Return "Error"
    '    End Try
    'End Function

    Private Function fnFindConflict_Ref(ByVal rsRefCd As String, ByVal rsUsDt As String, ByVal rsRefnm As String, Optional ByVal riRegType As Integer = 0) As String
        Dim sFn As String = "fnFindConflict_BC"

        Try

            If riRegType = 0 Then '신규
                Dim dt As DataTable = mobjDAF.GetSameRef(rsRefCd, rsUsDt, rsRefnm, riRegType)

                If dt.Rows.Count > 0 Then
                    Return "병원체코드나 출력명(바코드)가 기존 바코드분류코드 : " + dt.Rows(0).Item(0).ToString + dt.Rows(0).Item("partgbn").ToString + "와 동일합니다." + vbCrLf + vbCrLf + _
                           "병원체코드나 출력명(바코드)를 수정 하십시요!!"
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
    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Len(Me.txtRefcd.Text.Trim) < 1 Then
                MsgBox("병원체코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsDate(Me.txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then      '신규
                    Dim sTmp As String = fnFindConflict_Ref(txtRefcd.Text, txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""), txtRefnm.Text, 0)

                    If Not sTmp = "" Then
                        MsgBox(sTmp, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                Else        '수정
                    Dim sTmp As String = fnFindConflict_Ref(txtRefcd.Text, txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""), txtRefnm.Text, 1)

                    If Not sTmp = "" Then
                        MsgBox(sTmp, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Me.txtRefcd.Text.Trim = "" Then
                MsgBox("병원체코드명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtRefnm.Text.Trim = "" Then
                MsgBox("감염병명를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtRefnmd.Text.Trim = "" Then
                MsgBox("세세부명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtSeq.Text.Trim = "" Then
                MsgBox("순번을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNumeric(Me.txtSeq.Text.Trim) Then
                MsgBox("순번을 숫자로 입력하여 주십시요!!", MsgBoxStyle.Critical)
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

    Public Sub sbDisplayCdDetail(ByVal rsRefCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1
            sbDisplayCdDetail_Ref(rsRefCd)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Ref(ByVal rsRefCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Sect()"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mobjDAF.GetTestDocuInfo(rsRefCd)

            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()

            sbInitialize_CtrlCollection()

            fnFindChildControl(Me.Controls)

            If dt.Rows.Count < 1 Then Return
            spdFile.MaxRows = dt.Rows.Count

            For i As Integer = 0 To dt.Rows.Count - 1
                With spdFile
                    .Row = i + 1
                    .Col = .GetColFromID("regdt") : .Text = dt.Rows(i).Item("regdt").ToString
                    .Col = .GetColFromID("regid") : .Text = dt.Rows(i).Item("regid").ToString
                    .Col = .GetColFromID("filenm") : .Text = dt.Rows(i).Item("filenm").ToString



                End With
            Next

            'For i As Integer = 0 To dt.Rows.Count - 1
            '    For Each cctrl In mchildctrlcol
            '        For j As Integer = 0 To dt.Columns.Count - 1
            '            If cctrl.Tag.ToString.ToUpper = dt.Columns(j).ColumnName().ToUpper Then
            '                mchildctrlcol.Remove(1)

            '                If TypeOf (cctrl) Is System.Windows.Forms.ComboBox Then
            '                    If cctrl.Tag.ToString.EndsWith("_01") = True Then
            '                        iCurIndex = -1

            '                        For k As Integer = 0 To CType(cctrl, System.Windows.Forms.ComboBox).Items.Count - 1
            '                            If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.EndsWith(dt.Rows(i).Item(j).ToString) = True Then
            '                                iCurIndex = k

            '                                Exit For
            '                            End If

            '                            If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.StartsWith(dt.Rows(i).Item(j).ToString) = True Then
            '                                iCurIndex = k

            '                                Exit For
            '                            End If
            '                        Next

            '                        CType(cctrl, Windows.Forms.ComboBox).SelectedIndex = iCurIndex
            '                    End If

            '                ElseIf TypeOf (cctrl) Is System.Windows.Forms.TextBox Then
            '                    cctrl.Text = dt.Rows(i).Item(j).ToString.Trim

            '                ElseIf TypeOf (cctrl) Is System.Windows.Forms.CheckBox Then
            '                    CType(cctrl, System.Windows.Forms.CheckBox).Checked = CType(IIf(dt.Rows(i).Item(j).ToString = "1", True, False), Boolean)

            '                ElseIf TypeOf (cctrl) Is System.Windows.Forms.RadioButton Then
            '                    CType(cctrl, System.Windows.Forms.RadioButton).Checked = CType(IIf(dt.Rows(i).Item(j).ToString = "1", True, False), Boolean)

            '                End If

            '                Exit For
            '            End If
            '        Next
            '    Next
            'Next

            'If dt.Rows(0).Item("groupcd").ToString() <> "" Then
            '    Me.cboGroup.SelectedIndex = CInt(dt.Rows(0).Item("groupcd").ToString) - 1
            'End If

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

                Me.txtRefcd.Text = "" : Me.btnUE.Visible = False
                Me.txtRefcd.ReadOnly = False : Me.txtRefnm.ReadOnly = False : Me.txtRefnmd.ReadOnly = False : Me.txtSeq.ReadOnly = False

                Me.txtRefcd.Text = "" : Me.txtRefnm.Text = "" : Me.txtRefnmd.Text = "" : Me.txtSeq.Text = "" : Me.txtRefnm.Text = ""
                Me.txtRegNm.Text = ""
                Me.txtRefcd.Text = "" : Me.txtUSDT.Text = "" : Me.txtUEDT.Text = "" : Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""

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
    Friend WithEvents lblBcclsNmP As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNmD As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm As System.Windows.Forms.Label
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
    Friend WithEvents txtRefnmd As System.Windows.Forms.TextBox
    Friend WithEvents txtRefcd As System.Windows.Forms.TextBox
    Friend WithEvents lblBcclsNmS As System.Windows.Forms.Label
    Friend WithEvents txtRefnm As System.Windows.Forms.TextBox
    Friend WithEvents txtSeq As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF54))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tclSpc = New System.Windows.Forms.TabControl()
        Me.tpg1 = New System.Windows.Forms.TabPage()
        Me.txtUEDT = New System.Windows.Forms.TextBox()
        Me.lblUEDT = New System.Windows.Forms.Label()
        Me.txtRegDT = New System.Windows.Forms.TextBox()
        Me.txtUSDT = New System.Windows.Forms.TextBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblRegDT = New System.Windows.Forms.Label()
        Me.lblUSDT = New System.Windows.Forms.Label()
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox()
        Me.btnReg = New System.Windows.Forms.Button()
        Me.BtnDel = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.spdFile = New AxFPSpreadADO.AxfpSpread()
        Me.btnUE = New System.Windows.Forms.Button()
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker()
        Me.txtUSDay = New System.Windows.Forms.TextBox()
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker()
        Me.lblUSDayTime = New System.Windows.Forms.Label()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.lblColorGbn = New System.Windows.Forms.Label()
        Me.cboGroup = New System.Windows.Forms.ComboBox()
        Me.lblBcclsNmS = New System.Windows.Forms.Label()
        Me.txtRefnm = New System.Windows.Forms.TextBox()
        Me.lblBcclsNmP = New System.Windows.Forms.Label()
        Me.txtSeq = New System.Windows.Forms.TextBox()
        Me.lblBcclsNmD = New System.Windows.Forms.Label()
        Me.txtRefnmd = New System.Windows.Forms.TextBox()
        Me.lblBcclsNm = New System.Windows.Forms.Label()
        Me.txtRefcd = New System.Windows.Forms.TextBox()
        Me.ofdFile = New System.Windows.Forms.OpenFileDialog()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tpg1.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.spdFile, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tclSpc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
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
        Me.tpg1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpg1.Location = New System.Drawing.Point(4, 21)
        Me.tpg1.Name = "tpg1"
        Me.tpg1.Size = New System.Drawing.Size(780, 548)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "검사의뢰지침서 관리"
        Me.tpg1.UseVisualStyleBackColor = True
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
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
        Me.lblUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Location = New System.Drawing.Point(208, 509)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(99, 21)
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
        Me.txtUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
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
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(599, 509)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(83, 21)
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
        Me.lblRegDT.Location = New System.Drawing.Point(412, 509)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(83, 21)
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
        Me.lblUSDT.Location = New System.Drawing.Point(4, 509)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(99, 21)
        Me.lblUSDT.TabIndex = 0
        Me.lblUSDT.Text = "시작일시(선택)"
        Me.lblUSDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.btnReg)
        Me.grpCdInfo1.Controls.Add(Me.BtnDel)
        Me.grpCdInfo1.Controls.Add(Me.btnAdd)
        Me.grpCdInfo1.Controls.Add(Me.spdFile)
        Me.grpCdInfo1.Controls.Add(Me.btnUE)
        Me.grpCdInfo1.Controls.Add(Me.dtpUSTime)
        Me.grpCdInfo1.Controls.Add(Me.txtUSDay)
        Me.grpCdInfo1.Controls.Add(Me.dtpUSDay)
        Me.grpCdInfo1.Controls.Add(Me.lblUSDayTime)
        Me.grpCdInfo1.Controls.Add(Me.txtRegNm)
        Me.grpCdInfo1.Controls.Add(Me.txtRegID)
        Me.grpCdInfo1.Controls.Add(Me.lblColorGbn)
        Me.grpCdInfo1.Controls.Add(Me.cboGroup)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtRefnm)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtSeq)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtRefnmd)
        Me.grpCdInfo1.Controls.Add(Me.lblBcclsNm)
        Me.grpCdInfo1.Controls.Add(Me.txtRefcd)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(0, 3)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(775, 543)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        '
        'btnReg
        '
        Me.btnReg.Location = New System.Drawing.Point(538, 95)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(90, 30)
        Me.btnReg.TabIndex = 24
        Me.btnReg.Text = "파일저장"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'BtnDel
        '
        Me.BtnDel.Location = New System.Drawing.Point(634, 95)
        Me.BtnDel.Name = "BtnDel"
        Me.BtnDel.Size = New System.Drawing.Size(90, 30)
        Me.BtnDel.TabIndex = 23
        Me.BtnDel.Text = "파일삭제"
        Me.BtnDel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(442, 95)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(90, 30)
        Me.btnAdd.TabIndex = 22
        Me.btnAdd.Text = "파일추가"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'spdFile
        '
        Me.spdFile.DataSource = Nothing
        Me.spdFile.Location = New System.Drawing.Point(39, 131)
        Me.spdFile.Name = "spdFile"
        Me.spdFile.OcxState = CType(resources.GetObject("spdFile.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdFile.Size = New System.Drawing.Size(685, 235)
        Me.spdFile.TabIndex = 21
        '
        'btnUE
        '
        Me.btnUE.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(676, 14)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 20
        Me.btnUE.Text = "코드삭제"
        Me.btnUE.UseVisualStyleBackColor = False
        Me.btnUE.Visible = False
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(614, 17)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 19
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        Me.dtpUSTime.Visible = False
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(515, 17)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(77, 21)
        Me.txtUSDay.TabIndex = 17
        Me.txtUSDay.Visible = False
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(593, 17)
        Me.dtpUSDay.Name = "dtpUSDay"
        Me.dtpUSDay.Size = New System.Drawing.Size(20, 21)
        Me.dtpUSDay.TabIndex = 18
        Me.dtpUSDay.TabStop = False
        Me.dtpUSDay.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        Me.dtpUSDay.Visible = False
        '
        'lblUSDayTime
        '
        Me.lblUSDayTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Location = New System.Drawing.Point(412, 17)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(102, 21)
        Me.lblUSDayTime.TabIndex = 16
        Me.lblUSDayTime.Text = "시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblUSDayTime.Visible = False
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(683, 506)
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
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(683, 506)
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
        Me.lblColorGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblColorGbn.ForeColor = System.Drawing.Color.White
        Me.lblColorGbn.Location = New System.Drawing.Point(8, 104)
        Me.lblColorGbn.Name = "lblColorGbn"
        Me.lblColorGbn.Size = New System.Drawing.Size(126, 21)
        Me.lblColorGbn.TabIndex = 13
        Me.lblColorGbn.Text = "그룹"
        Me.lblColorGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblColorGbn.Visible = False
        '
        'cboGroup
        '
        Me.cboGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGroup.FormattingEnabled = True
        Me.cboGroup.Items.AddRange(New Object() {"[1] 1군", "[2] 2군", "[3] 3군", "[4] 4군"})
        Me.cboGroup.Location = New System.Drawing.Point(135, 104)
        Me.cboGroup.Name = "cboGroup"
        Me.cboGroup.Size = New System.Drawing.Size(122, 20)
        Me.cboGroup.TabIndex = 12
        Me.cboGroup.Tag = "COLORGBN_01"
        Me.cboGroup.Visible = False
        '
        'lblBcclsNmS
        '
        Me.lblBcclsNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNmS.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNmS.Location = New System.Drawing.Point(8, 38)
        Me.lblBcclsNmS.Name = "lblBcclsNmS"
        Me.lblBcclsNmS.Size = New System.Drawing.Size(126, 21)
        Me.lblBcclsNmS.TabIndex = 5
        Me.lblBcclsNmS.Text = "감염병명"
        Me.lblBcclsNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBcclsNmS.Visible = False
        '
        'txtRefnm
        '
        Me.txtRefnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefnm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRefnm.Location = New System.Drawing.Point(135, 39)
        Me.txtRefnm.MaxLength = 10
        Me.txtRefnm.Name = "txtRefnm"
        Me.txtRefnm.Size = New System.Drawing.Size(347, 21)
        Me.txtRefnm.TabIndex = 2
        Me.txtRefnm.Tag = "refnm"
        Me.txtRefnm.Visible = False
        '
        'lblBcclsNmP
        '
        Me.lblBcclsNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNmP.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNmP.Location = New System.Drawing.Point(8, 82)
        Me.lblBcclsNmP.Name = "lblBcclsNmP"
        Me.lblBcclsNmP.Size = New System.Drawing.Size(126, 21)
        Me.lblBcclsNmP.TabIndex = 0
        Me.lblBcclsNmP.Text = "순번"
        Me.lblBcclsNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBcclsNmP.Visible = False
        '
        'txtSeq
        '
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSeq.Location = New System.Drawing.Point(135, 83)
        Me.txtSeq.MaxLength = 20
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.Size = New System.Drawing.Size(57, 21)
        Me.txtSeq.TabIndex = 4
        Me.txtSeq.Tag = "seq"
        Me.txtSeq.Visible = False
        '
        'lblBcclsNmD
        '
        Me.lblBcclsNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNmD.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNmD.Location = New System.Drawing.Point(8, 60)
        Me.lblBcclsNmD.Name = "lblBcclsNmD"
        Me.lblBcclsNmD.Size = New System.Drawing.Size(126, 21)
        Me.lblBcclsNmD.TabIndex = 0
        Me.lblBcclsNmD.Text = "세세부명"
        Me.lblBcclsNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBcclsNmD.Visible = False
        '
        'txtRefnmd
        '
        Me.txtRefnmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefnmd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRefnmd.Location = New System.Drawing.Point(135, 61)
        Me.txtRefnmd.MaxLength = 20
        Me.txtRefnmd.Name = "txtRefnmd"
        Me.txtRefnmd.Size = New System.Drawing.Size(347, 21)
        Me.txtRefnmd.TabIndex = 3
        Me.txtRefnmd.Tag = "refnmd"
        Me.txtRefnmd.Visible = False
        '
        'lblBcclsNm
        '
        Me.lblBcclsNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsNm.ForeColor = System.Drawing.Color.White
        Me.lblBcclsNm.Location = New System.Drawing.Point(8, 16)
        Me.lblBcclsNm.Name = "lblBcclsNm"
        Me.lblBcclsNm.Size = New System.Drawing.Size(126, 21)
        Me.lblBcclsNm.TabIndex = 0
        Me.lblBcclsNm.Text = "병원체코드"
        Me.lblBcclsNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBcclsNm.Visible = False
        '
        'txtRefcd
        '
        Me.txtRefcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefcd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRefcd.Location = New System.Drawing.Point(135, 17)
        Me.txtRefcd.MaxLength = 20
        Me.txtRefcd.Name = "txtRefcd"
        Me.txtRefcd.Size = New System.Drawing.Size(128, 21)
        Me.txtRefcd.TabIndex = 1
        Me.txtRefcd.Tag = "refcd"
        Me.txtRefcd.Visible = False
        '
        'ofdFile
        '
        Me.ofdFile.FileName = "OpenFileDialog1"
        '
        'FDF54
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 577)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF54"
        Me.Text = "[53] 병원체 코드"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tpg1.ResumeLayout(False)
        Me.tpg1.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.spdFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sFn As String = "Private Sub btnUE_Click"

        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If Me.txtRefcd.Text = "" Then Exit Sub

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "   바코드분류코드   : " + Me.txtRefcd.Text + vbCrLf
            sMsg += "   바코드분류명     : " + Me.txtRefcd.Text + vbCrLf
            sMsg += "   을(를) 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mobjDAF.TransRefInfo_UE(Me.txtRefcd.Text, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID, sUeDate + sUeTime) Then
                MsgBox("해당 바코드분류정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub dtpUSDay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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


    Private Sub txtBCCLSNM_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRefcd.Validating
        If miSelectKey = 1 Then Exit Sub

        If Me.txtRefnm.Text.Trim = "" Then
            If Me.txtRefcd.Text.Length > Me.txtRefnm.MaxLength Then
                Me.txtRefnm.Text = Me.txtRefcd.Text.Substring(0, Me.txtRefnm.MaxLength)
            Else
                Me.txtRefnm.Text = txtRefcd.Text
            End If
        End If

        If Me.txtRefnmd.Text.Trim = "" Then
            If Me.txtRefcd.Text.Length > Me.txtRefnmd.MaxLength Then
                Me.txtRefnmd.Text = Me.txtRefcd.Text.Substring(0, txtRefnmd.MaxLength)
            Else
                Me.txtRefnmd.Text = Me.txtRefcd.Text
            End If
        End If

        If Me.txtSeq.Text.Trim = "" Then
            If Me.txtRefcd.Text.Length > Me.txtSeq.MaxLength Then
                Me.txtSeq.Text = Me.txtRefcd.Text.Substring(0, txtSeq.MaxLength)
            Else
                Me.txtSeq.Text = Me.txtRefcd.Text
            End If
        End If
    End Sub

    Private Sub txtBcclsCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRefcd.KeyDown, txtRefnmd.KeyDown, txtSeq.KeyDown, txtRefnm.KeyDown, cboGroup.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ofdFile.InitialDirectory = ""
        ofdFile.FilterIndex = 2
        ofdFile.RestoreDirectory = True

        If ofdFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            With spdFile
                .MaxRows += 1
                .Row = .MaxRows
                .Col = .GetColFromID("filenm")
                .Text = ofdFile.FileName

                .Col = .GetColFromID("chk")
                .Text = "1"
            End With
        End If
    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFilenm As String = "" : Dim al_File As New ArrayList : Dim sRegDT As String = ""
        With spdFile
            For i As Integer = 1 To .MaxRows
                .Row = i
                .Col = .GetColFromID("chk")
                If .Text = "1" Then
                    .Row = i
                    .Col = .GetColFromID("filenm")
                    sFilenm = .Text
                    If IO.File.Exists(sFilenm) Then al_File.Add(sFilenm)
                End If

            Next
            sRegDT = fnGetSystemDT()
            If mobjDAF.TransTestDocInfo(al_File, sRegDT) = True Then
                MsgBox("파일이 정상 등록되었습니다!", MsgBoxStyle.Information)

            End If
        End With
    End Sub

    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDel.Click

        Dim Filearry As New ArrayList

        With spdFile
            For i As Integer = 0 To .MaxRows
                .Row = i
                .Col = .GetColFromID("chk") : Dim chk As String = .Text

                If chk = "1" Then
                    .Col = .GetColFromID("filenm") : Dim filenm As String = .Text

                    Filearry.Add(filenm)
                End If
            Next
        End With

        If mobjDAF.TransTestDocInfo_Del(Filearry) = True Then
            MsgBox("파일이 삭제되었습니다!", MsgBoxStyle.Information)
        End If

    End Sub
End Class
