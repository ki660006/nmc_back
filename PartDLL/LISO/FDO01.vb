'>>> [01] 검사항목별 단가
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class FDO01
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF03.vb, Class : FDF03" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2
    Private msUserID As String = USER_INFO.USRID

    Private mobjDAF As New LISAPP.LISAPP_O_UCOST
    Friend WithEvents spdTclsList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents spdUCostList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Public giClearKey As Integer = 0

    Private Sub sbDisplay_TclsList(ByVal rsPartCd As String, ByVal rsSlipCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_TSect()"

        Try

            Dim dt As DataTable = mobjDAF.fnGet_testinfo_slip(rsPartCd, rsSlipCd)

            If dt.Rows.Count < 1 Then Return

            With spdTclsList
                .ReDraw = False
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("tclscd") : .Text = dt.Rows(ix).Item("tclscd").ToString
                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                    .Col = .GetColFromID("tcdgbn") : .Text = dt.Rows(ix).Item("tcdgbn").ToString
                    .Col = .GetColFromID("ucost") : .Text = dt.Rows(ix).Item("ucost").ToString
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_slip()
        Dim sFn As String = "Private Sub sbDisplayCdList_slip()"

        Try
            Me.cboSlip.Items.Clear()
            Me.cboSlip.Items.Add("[  ] 전체")

            Dim DTable As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()

            With DTable
                If .Rows.Count = 0 Then Return

                For i As Integer = 1 To .Rows.Count
                    Me.cboSlip.Items.Add("[" + .Rows(i - 1).Item("slipcd").ToString + "] " + .Rows(i - 1).Item("TSECTNMD").ToString())
                Next
            End With

            If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0

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
            dt = mobjDAF.GetUsUeCd_UCost(Me.txtUSDT.Text)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransUCostInfo_DEL(Me.txtUSDT.Text, msUserID)

            If bReturn Then
                MsgBox("해당 코드가 삭제되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGO91).sbRefreshCdList()
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

            '> 사용중복 조사
            dt = mobjDAF.GetUsUeDupl_UCost(Me.txtUSDT.Text, rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransUCostInfo_UPD_US(Me.txtUSDT.Text, msUserID, rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransUCostInfo_UPD_UE(Me.txtUSDT.Text, msUserID, rsUseDt)
            End If

            If bReturn Then
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + "가 수정되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGO91).sbRefreshCdList()
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
            Dim fgf93 As New FGO93

            With fgf93
                .txtCd.Text = Me.txtUSDay.Text
                .txtNm.Text = ""

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

    Private Function fnCollectItemTable_93(ByVal asRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_31(ByVal asRegDT As String) As DA01.ItemTableCollection"

        Try
            Dim it93 As New LISAPP.ItemTableCollection
            Dim iCnt As Integer = 0

            With spdUCostList
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text

                    'If sChk = "1" Then
                    iCnt += 1

                    .Col = .GetColFromID("tclscd") : Dim strTClsCd As String = .Text
                    .Col = .GetColFromID("spccd") : Dim strSpcCd As String = .Text
                    .Col = .GetColFromID("ucost") : Dim strUCost As String = .Text

                    it93.SetItemTable("tclscd", 1, iCnt, strTClsCd)
                    it93.SetItemTable("spccd", 2, iCnt, strSpcCd)

                    it93.SetItemTable("usdt", 3, iCnt, txtUSDay.Text & " " & Format(dtpUSTime.Value, "HH:mm:ss"))
                    it93.SetItemTable("uedt", 4, iCnt, msUEDT + " 00:00:00")
                    it93.SetItemTable("ucost", 5, iCnt, strUCost.Replace(",", ""))
                    it93.SetItemTable("regdt", 6, iCnt, asRegDT)
                    it93.SetItemTable("regid", 7, iCnt, msUserID)
                    'End If
                Next
            End With

            fnCollectItemTable_93 = it93
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

    Private Function fnFindConflict(ByVal asUSDT As String) As String
        Dim sFn As String = ""

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetRecentUCostInfo(asUSDT)

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
            Dim it91 As New LISAPP.ItemTableCollection
            Dim iRegType91 As Integer = 0
            Dim sRegDT As String

            iRegType91 = CType(IIf(CType(Me.Owner, FGO91).rbnWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it91 = fnCollectItemTable_93(sRegDT)

            If mobjDAF.TransUCostInfo(it91, iRegType91, txtUSDay.Text & " " & Format(dtpUSTime.Value, "HH:mm:ss"), msUserID) Then
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
            For ix As Integer = 1 To spdUCostList.MaxRows
                With spdUCostList
                    .Row = ix : .Col = .GetColFromID("ucost") : Dim strUCost As String = .Text
                    .Row = ix : .Col = .GetColFromID("tclscd") : Dim strTclsCd As String = .Text
                    .Row = ix : .Col = .GetColFromID("spccd") : Dim strSpcCd As String = .Text
                    .Row = ix : .Col = .GetColFromID("tnmd") : Dim strtnmd As String = .Text

                    If strUCost.Replace(",", "") = "" Then
                        MsgBox("검사코드: " + strTclsCd + vbCrLf + ", 검체코드: " + strSpcCd + vbCrLf + "검사명: " + strtnmd + vbCrLf + "의 단가를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End With
            Next

            If Not IsDate(txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGO91).rbnWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""))

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGO91).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGO91).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsCd As String, ByVal rsUSDT As String, Optional ByVal rsUEDT As String = "30000101", Optional ByVal aiMode As Integer = 0)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                sbDisplayCdList_slip()
            End If

            sbDisplayCdDetail_UCust(rsCd, rsUSDT)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_UCust(ByVal rsCd As String, ByVal rsUSDT As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_UCust(String)"
        Dim iCol As Integer = 0

        Try
            Dim DTable As DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            DTable = mobjDAF.GetUCostInfo(1, rsUSDT.Replace("-", "").Replace(" ", "").Replace(":", ""))

            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            '''    sbInitialize()

            ''초기화할 것은 Query라벨
            'sbInitialize_Test_QueryLabel()

            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()

            sbInitialize_CtrlCollection()

            fnFindChildControl(Me.Controls)

            If DTable.Rows.Count > 0 Then
                For i As Integer = 0 To DTable.Rows.Count - 1
                    For Each cctrl In mchildctrlcol
                        For j As Integer = 0 To DTable.Columns.Count - 1
                            If cctrl.Tag.ToString.ToUpper = DTable.Columns(j).ColumnName().ToUpper Then
                                mchildctrlcol.Remove(1)

                                If TypeOf (cctrl) Is System.Windows.Forms.ComboBox Then
                                    If cctrl.Tag.ToString.EndsWith("_01") = True Then
                                        iCurIndex = -1

                                        For k As Integer = 0 To CType(cctrl, System.Windows.Forms.ComboBox).Items.Count - 1
                                            If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.EndsWith(DTable.Rows(i).Item(j).ToString) = True Then
                                                iCurIndex = k

                                                Exit For
                                            End If

                                            If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.StartsWith(DTable.Rows(i).Item(j).ToString) = True Then
                                                iCurIndex = k

                                                Exit For
                                            End If
                                        Next

                                        CType(cctrl, Windows.Forms.ComboBox).SelectedIndex = iCurIndex
                                    End If

                                ElseIf TypeOf (cctrl) Is System.Windows.Forms.TextBox Then
                                    cctrl.Text = DTable.Rows(i).Item(j).ToString

                                ElseIf TypeOf (cctrl) Is System.Windows.Forms.CheckBox Then
                                    CType(cctrl, System.Windows.Forms.CheckBox).Checked = CType(IIf(DTable.Rows(i).Item(j).ToString = "1", True, False), Boolean)

                                ElseIf TypeOf (cctrl) Is System.Windows.Forms.RadioButton Then
                                    CType(cctrl, System.Windows.Forms.RadioButton).Checked = CType(IIf(DTable.Rows(i).Item(j).ToString = "1", True, False), Boolean)

                                End If

                                Exit For
                            End If
                        Next
                    Next

                    With spdUCostList
                        If i = 0 Then .MaxRows = 0

                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("tclscd") : .Text = DTable.Rows(i).Item("tclscd").ToString
                        .Col = .GetColFromID("spccd") : .Text = DTable.Rows(i).Item("spccd").ToString
                        .Col = .GetColFromID("tnmd") : .Text = DTable.Rows(i).Item("tnmd").ToString
                        .Col = .GetColFromID("tcdgbn") : .Text = DTable.Rows(i).Item("tcdgbn").ToString
                        .Col = .GetColFromID("ucost") : .Text = DTable.Rows(i).Item("ucost").ToString
                    End With
                Next

                If Not IsNothing(Me.Owner) Then
                    If Not CType(Me.Owner, FGO91).rbnWorkOpt2.Checked Then
                        txtUSDay.Text = rsUSDT.Insert(4, "-").Insert(7, "-").Substring(0, 10)
                        dtpUSTime.Value = CDate(rsUSDT.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":"))
                    End If
                End If
            Else
                Exit Sub
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
            sbInitialize_spdTclsList()
            sbInitialize_spdUCostList()

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
                btnUE.Visible = False

                txtJumSu.Text = ""
                txtUSDT.Text = "" : txtUEDT.Text = "" : txtRegDT.Text = "" : txtRegID.Text = ""
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

    Private Sub sbInitialize_spdTclsList()
        With spdTclsList
            .MaxRows = 0
        End With
    End Sub

    Private Sub sbInitialize_spdUCostList()
        With spdUCostList
            .MaxRows = 0
        End With
    End Sub

    Public Sub sbSetNewUSDT()
        Dim sFn As String = ""

        Try
#If DEBUG Then
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 0, CType(fnGetSystemDT(), Date)), "yyyy-MM-dd 00:00:00")
#Else
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 1, CType(fnGetSystemDT(), Date)), "yyyy-MM-dd 00:00:00")
#End If
            miSelectKey = 1

            txtUSDay.Text = sSysDT.Substring(0, 10)
            dtpUSDay.Value = CType(sSysDT, Date)
            dtpUSTime.Value = CType(sSysDT, Date)
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
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblRegID As System.Windows.Forms.Label
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
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents lblJPlus As System.Windows.Forms.Label
    Friend WithEvents txtJumSu As System.Windows.Forms.TextBox
    Friend WithEvents tpgSpc1 As System.Windows.Forms.TabPage
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDO01))
        Me.lblJPlus = New System.Windows.Forms.Label
        Me.txtJumSu = New System.Windows.Forms.TextBox
        Me.txtUEDT = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.txtUSDT = New System.Windows.Forms.TextBox
        Me.lblRegID = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.lblUSDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpTInfo1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnDel = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.spdUCostList = New AxFPSpreadADO.AxfpSpread
        Me.cboSlip = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.spdTclsList = New AxFPSpreadADO.AxfpSpread
        Me.grpTestCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker
        Me.txtUSDay = New System.Windows.Forms.TextBox
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker
        Me.lblUSDayTime = New System.Windows.Forms.Label
        Me.tpgSpc1 = New System.Windows.Forms.TabPage
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.grpTInfo1.SuspendLayout()
        CType(Me.spdUCostList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdTclsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTestCd.SuspendLayout()
        Me.tpgSpc1.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblJPlus
        '
        Me.lblJPlus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJPlus.BackColor = System.Drawing.Color.SlateBlue
        Me.lblJPlus.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJPlus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblJPlus.Location = New System.Drawing.Point(618, 20)
        Me.lblJPlus.Name = "lblJPlus"
        Me.lblJPlus.Size = New System.Drawing.Size(72, 21)
        Me.lblJPlus.TabIndex = 0
        Me.lblJPlus.Text = " 증가율"
        Me.lblJPlus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJumSu
        '
        Me.txtJumSu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtJumSu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJumSu.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtJumSu.Location = New System.Drawing.Point(692, 20)
        Me.txtJumSu.MaxLength = 60
        Me.txtJumSu.Name = "txtJumSu"
        Me.txtJumSu.Size = New System.Drawing.Size(64, 21)
        Me.txtJumSu.TabIndex = 1
        Me.txtJumSu.Tag = ""
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUEDT.Location = New System.Drawing.Point(316, 544)
        Me.txtUEDT.Name = "txtUEDT"
        Me.txtUEDT.ReadOnly = True
        Me.txtUEDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUEDT.TabIndex = 0
        Me.txtUEDT.TabStop = False
        Me.txtUEDT.Tag = "UEDT"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.Navy
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.Label8.Location = New System.Drawing.Point(220, 544)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 21)
        Me.Label8.TabIndex = 0
        Me.Label8.Tag = ""
        Me.Label8.Text = "종료일시(선택)"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(512, 544)
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
        Me.txtUSDT.Location = New System.Drawing.Point(108, 544)
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUSDT.TabIndex = 0
        Me.txtUSDT.TabStop = False
        Me.txtUSDT.Tag = "USDT"
        '
        'lblRegID
        '
        Me.lblRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegID.BackColor = System.Drawing.Color.Navy
        Me.lblRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegID.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.lblRegID.Location = New System.Drawing.Point(620, 544)
        Me.lblRegID.Name = "lblRegID"
        Me.lblRegID.Size = New System.Drawing.Size(84, 21)
        Me.lblRegID.TabIndex = 0
        Me.lblRegID.Text = "최종등록자ID"
        Me.lblRegID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.Navy
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.lblRegDT.Location = New System.Drawing.Point(428, 544)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUSDT
        '
        Me.lblUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUSDT.BackColor = System.Drawing.Color.Navy
        Me.lblUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDT.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.lblUSDT.Location = New System.Drawing.Point(12, 544)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(96, 21)
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
        Me.txtRegID.Location = New System.Drawing.Point(704, 544)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpTInfo1
        '
        Me.grpTInfo1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTInfo1.Controls.Add(Me.Label1)
        Me.grpTInfo1.Controls.Add(Me.btnDel)
        Me.grpTInfo1.Controls.Add(Me.btnAdd)
        Me.grpTInfo1.Controls.Add(Me.spdUCostList)
        Me.grpTInfo1.Controls.Add(Me.cboSlip)
        Me.grpTInfo1.Controls.Add(Me.Label3)
        Me.grpTInfo1.Controls.Add(Me.spdTclsList)
        Me.grpTInfo1.Controls.Add(Me.lblJPlus)
        Me.grpTInfo1.Controls.Add(Me.txtJumSu)
        Me.grpTInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpTInfo1.Location = New System.Drawing.Point(8, 52)
        Me.grpTInfo1.Name = "grpTInfo1"
        Me.grpTInfo1.Size = New System.Drawing.Size(764, 488)
        Me.grpTInfo1.TabIndex = 2
        Me.grpTInfo1.TabStop = False
        Me.grpTInfo1.Text = "검사항목별 단가 정보"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Navy
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(413, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 20)
        Me.Label1.TabIndex = 126
        Me.Label1.Text = " 검사항목별 단가"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnDel
        '
        Me.btnDel.Location = New System.Drawing.Point(320, 155)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(75, 35)
        Me.btnDel.TabIndex = 125
        Me.btnDel.Text = "<<"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(320, 114)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 35)
        Me.btnAdd.TabIndex = 124
        Me.btnAdd.Text = ">>"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'spdUCostList
        '
        Me.spdUCostList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdUCostList.Location = New System.Drawing.Point(413, 43)
        Me.spdUCostList.Name = "spdUCostList"
        Me.spdUCostList.OcxState = CType(resources.GetObject("spdUCostList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdUCostList.Size = New System.Drawing.Size(342, 438)
        Me.spdUCostList.TabIndex = 123
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.FormattingEnabled = True
        Me.cboSlip.Location = New System.Drawing.Point(81, 20)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(216, 20)
        Me.cboSlip.TabIndex = 122
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Navy
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(8, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 20)
        Me.Label3.TabIndex = 121
        Me.Label3.Text = " 검사분야"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'spdTclsList
        '
        Me.spdTclsList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdTclsList.Location = New System.Drawing.Point(8, 43)
        Me.spdTclsList.Name = "spdTclsList"
        Me.spdTclsList.OcxState = CType(resources.GetObject("spdTclsList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTclsList.Size = New System.Drawing.Size(290, 439)
        Me.spdTclsList.TabIndex = 2
        '
        'grpTestCd
        '
        Me.grpTestCd.Controls.Add(Me.btnUE)
        Me.grpTestCd.Controls.Add(Me.dtpUSTime)
        Me.grpTestCd.Controls.Add(Me.txtUSDay)
        Me.grpTestCd.Controls.Add(Me.dtpUSDay)
        Me.grpTestCd.Controls.Add(Me.lblUSDayTime)
        Me.grpTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpTestCd.Location = New System.Drawing.Point(8, 4)
        Me.grpTestCd.Name = "grpTestCd"
        Me.grpTestCd.Size = New System.Drawing.Size(764, 44)
        Me.grpTestCd.TabIndex = 1
        Me.grpTestCd.TabStop = False
        Me.grpTestCd.Text = "시작일시"
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.IndianRed
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.btnUE.Location = New System.Drawing.Point(692, 13)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(64, 24)
        Me.btnUE.TabIndex = 7
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(192, 15)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 3
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(100, 15)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(72, 21)
        Me.txtUSDay.TabIndex = 1
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
        Me.lblUSDayTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUSDayTime.BackColor = System.Drawing.Color.Navy
        Me.lblUSDayTime.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.lblUSDayTime.Location = New System.Drawing.Point(8, 15)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(92, 21)
        Me.lblUSDayTime.TabIndex = 0
        Me.lblUSDayTime.Text = " 시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tpgSpc1
        '
        Me.tpgSpc1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgSpc1.Controls.Add(Me.txtUEDT)
        Me.tpgSpc1.Controls.Add(Me.Label8)
        Me.tpgSpc1.Controls.Add(Me.txtRegDT)
        Me.tpgSpc1.Controls.Add(Me.txtUSDT)
        Me.tpgSpc1.Controls.Add(Me.lblRegID)
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
        'FDO01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDO01"
        Me.Text = "[01] 검사항목별 단가"
        Me.grpTInfo1.ResumeLayout(False)
        Me.grpTInfo1.PerformLayout()
        CType(Me.spdUCostList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdTclsList, System.ComponentModel.ISupportInitialize).EndInit()
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

    Private Sub dtpUSDay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUSDay.ValueChanged
        If miSelectKey = 1 Then Exit Sub
        If txtUSDay.Text.Trim = "" Then Exit Sub

        txtUSDay.Text = Format(dtpUSDay.Value, "yyyy-MM-dd").Substring(0, 10)
    End Sub

    Private Sub FDF03_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGO91).btnReg_Click(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGO91).btnClear_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub cboTSect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        Dim sTSectCd As String = Ctrl.Get_Code(cboSlip).Trim

        If sTSectCd = "" Then
            sbDisplay_TclsList("", "")
        Else
            sbDisplay_TclsList(sTSectCd.Substring(0, 1), sTSectCd.Substring(1, 1))
        End If

    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        For ix1 As Integer = 1 To spdTclsList.MaxRows

            Dim strChk As String = ""
            Dim strTclsCd As String = ""
            Dim strspccd As String = ""
            Dim strtcdgbn As String = ""
            Dim strTnmd As String = ""
            Dim strUCost As String = ""

            With spdTclsList
                .Row = ix1
                .Col = .GetColFromID("chk") : strChk = spdTclsList.Text
                .Col = .GetColFromID("tclscd") : strTclsCd = spdTclsList.Text
                .Col = .GetColFromID("spccd") : strspccd = spdTclsList.Text
                .Col = .GetColFromID("tcdgbn") : strtcdgbn = spdTclsList.Text
                .Col = .GetColFromID("tnmd") : strTnmd = spdTclsList.Text
                .Col = .GetColFromID("ucost") : strUCost = spdTclsList.Text
            End With

            If strChk = "1" Then
                Dim intRow As Integer = 0

                With spdUCostList
                    For ix2 As Integer = 1 To .MaxRows
                        .Row = ix2
                        .Col = .GetColFromID("tclscd") : Dim strTCd As String = spdTclsList.Text
                        .Col = .GetColFromID("spccd") : Dim strSCd As String = spdTclsList.Text

                        If strTclsCd = strTCd And strspccd = strSCd Then
                            intRow = ix2
                            Exit For
                        End If
                    Next

                    If intRow = 0 Then
                        .MaxRows += 1
                        .Col = .GetColFromID("tclscd") : .Text = strTclsCd
                        .Col = .GetColFromID("spccd") : .Text = strspccd
                        .Col = .GetColFromID("tcdgbn") : .Text = strtcdgbn
                        .Col = .GetColFromID("tnmd") : .Text = strTnmd
                        .Col = .GetColFromID("ucost") : .Text = strUCost
                    End If
                End With
            End If
        Next
    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click
        For ix1 As Integer = 1 To spdTclsList.MaxRows
            With spdUCostList
                .Row = ix1
                .Col = .GetColFromID("chk") : Dim strChk As String = spdTclsList.Text

                If strChk = "1" Then
                    .Row = ix1
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1

                    ix1 -= 1
                End If

                If ix1 < 1 Then Exit For
            End With
        Next
    End Sub

    Private Sub txtJumSu_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtJumSu.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        With spdUCostList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("ucost") : Dim sUCost As String = .Text

                If sUCost <> "" Then
                    .Text = (Val(sUCost) * Val(txtJumSu.Text)).ToString
                End If
            Next
        End With

    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        If txtUSDay.Text = "" Then Exit Sub

        Try
            If Convert.ToDateTime(fnGetSystemDT) >= Convert.ToDateTime(Me.txtUEDT.Text) Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "   시작일 : " & txtUSDay.Text & vbCrLf
            sMsg &= " 의 검사항목별 단가를 사용종료하시겠습니까?"

            If mobjDAF.TransUCostInfo_UE(txtUSDay.Text & " " & Format(dtpUSTime.Value, "HH:mm:ss"), msUserID) Then
                MsgBox("해당 거래처정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

                sbInitialize()
                CType(Me.Owner, FGO91).sbDeleteCdList()
            Else
                MsgBox("사용종료에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub
End Class
