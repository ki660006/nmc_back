'>>> [05] 작업그룹
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF05
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF05.vb, Class : FDF05" + vbTab

    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT

    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_WKGRP

    Public gsModDT As String = ""
    Friend WithEvents cboWgrpGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblWgrpGbn As System.Windows.Forms.Label
    Friend WithEvents btnAddSlip As System.Windows.Forms.Button
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents spdTestCd As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblSlip As System.Windows.Forms.Label
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents chkSpcGbn As System.Windows.Forms.CheckBox
    Public gsModID As String = ""
    Dim Modpartslip As String = ""

    Private Sub sbDisplay_CDHELP_test()
        Dim sFn As String = "btnAddTest_Click"

        Try

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_testspc_list(Ctrl.Get_Code(Me.cboSlip), "")
            Dim a_dr As DataRow() = dt.Select("tcdgbn IN ('S', 'P', 'B')", "")
            dt = Fn.ChangeToDataTable(a_dr)

            Dim sTestCds As String = ""

            For ix As Integer = 1 To spdTestCd.MaxRows
                With spdTestCd
                    .Row = ix
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                    .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text

                    sTestCds += sTestCd.PadRight(8, " "c) + sSpcCd + "|"
                End With
            Next

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "검사정보"

            objHelp.Distinct = True
            objHelp.KeyCodes = sTestCds
            objHelp.MaxRows = 30

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("testspcd", "코드", 0, , , True, , "Y")
            objHelp.AddField("partslip", "코드", 0, , , True)
            objHelp.AddField("tcdgbn", "구분", 0, , , True)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.lblSlip)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.lblSlip.Left, pntFrmXY.Y + pntCtlXY.Y + Me.lblSlip.Height + 80, dt)

            If alList.Count > 0 Then

                For ix1 As Integer = 0 To alList.Count - 1
                    With spdTestCd

                        Dim iRow As Integer = 0
                        For ix2 As Integer = 1 To .MaxRows
                            .Row = ix2
                            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                            .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text

                            If alList.Item(ix1).ToString.Split("|"c)(0) = sTestCd And alList.Item(ix1).ToString.Split("|"c)(1) = sSpcCd Then
                                iRow = ix1
                                Exit For
                            End If
                        Next

                        If iRow = 0 Then
                            .MaxRows += 1
                            .Row = .MaxRows
                            .Col = .GetColFromID("testcd") : .Text = alList.Item(ix1).ToString.Split("|"c)(0)
                            .Col = .GetColFromID("spccd") : .Text = alList.Item(ix1).ToString.Split("|"c)(1)
                            .Col = .GetColFromID("tnmd") : .Text = alList.Item(ix1).ToString.Split("|"c)(2)
                            .Col = .GetColFromID("partslip") : .Text = alList.Item(ix1).ToString.Split("|"c)(5)
                        End If
                    End With
                Next

            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_CHHELP_spc()
        Dim sFn As String = "Handles btnCdHelp_test.Click"
        Try

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim alTest As New ArrayList

            Dim sPartCd As String = Ctrl.Get_Code(cboSlip).Substring(0, 1)
            Dim sSlipCd As String = Ctrl.Get_Code(cboSlip).Substring(1, 1)
            Dim sSpcCds As String = ""

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Spc_List("", sPartCd, sSlipCd, "", "", "", "")

            With Me.spdTestCd
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
                    .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text

                    If alList.Contains(sSpcCd) = False Then
                        sSpcCds += IIf(alList.Count = 0, "", "|").ToString + sSpcCd
                        alList.Add(sSpcCd)
                    End If

                    alTest.Add(sTestcd + sSpcCd)
                Next
            End With

            objHelp.FormText = "검체정보"

            objHelp.MaxRows = 20
            objHelp.Distinct = True
            objHelp.KeyCodes = sSpcCds

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("spcnmd", "검체명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.lblSlip)

            alList = New ArrayList
            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.lblSlip.Left, pntFrmXY.Y + pntCtlXY.Y + Me.lblSlip.Height + 80, dt)

            sSpcCds = ""
            If alList.Count > 0 Then

                For ix As Integer = 0 To alList.Count - 1
                    Dim sSpcCd As String = alList.Item(ix).ToString.Split("|"c)(1)

                    sSpcCds += IIf(ix = 0, "", ",").ToString + sSpcCd
                Next
            End If

            If sSpcCds = "" Then Return

            dt = mobjDAF.fnGet_TestInfo_spc(sPartCd + sSlipCd, sSpcCds, Me.txtWkGrpCd.Text)
            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                If alTest.Contains(dt.Rows(ix).Item("testcd").ToString + dt.Rows(ix).Item("spccd").ToString) = False Then
                    With spdTestCd
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                        .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString
                        .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                        .Col = .GetColFromID("partslip") : .Text = dt.Rows(ix).Item("slipcd").ToString

                        alTest.Add(dt.Rows(ix).Item("testcd").ToString + dt.Rows(ix).Item("spccd").ToString)
                    End With
                End If
            Next


        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Function fnCollectItemTable_69(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_69() As LISAPP.ItemTableCollection"

        Try
            Dim it69 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdTestCd

            Dim iRow As Integer = 0

            With it69
                For i As Integer = 1 To spd.MaxRows
                    spd.Row = i
                    spd.Col = spd.GetColFromID("delflg") : Dim sDelFlg As String = spd.Text

                    If sDelFlg <> "D" Then
                        iRow += 1

                        spd.Row = i
                        spd.Col = spd.GetColFromID("testcd") : Dim sTestcd As String = spd.Text
                        spd.Col = spd.GetColFromID("spccd") : Dim sSpccd As String = spd.Text
                        spd.Col = spd.GetColFromID("partslip") : Dim sPartSlip As String = spd.Text

                        .SetItemTable("wkgrpcd", 1, iRow, Me.txtWkGrpCd.Text)
                        .SetItemTable("testcd", 2, iRow, sTestcd)
                        .SetItemTable("spccd", 3, iRow, sSpccd)

                        .SetItemTable("wkgrpnm", 4, iRow, Me.txtWkGrpNm.Text)
                        .SetItemTable("wkgrpnms", 5, iRow, Me.txtWkGrpNmS.Text)
                        .SetItemTable("wkgrpnmd", 6, iRow, Me.txtWkGrpNmD.Text)
                        .SetItemTable("wkgrpnmbp", 7, iRow, Me.txtWkGrpNmP.Text)
                        .SetItemTable("wkgrpgbn", 8, iRow, Ctrl.Get_Code(Me.cboWgrpGbn))

                        .SetItemTable("partcd", 9, iRow, sPartSlip.Substring(0, 1))
                        .SetItemTable("slipcd", 10, iRow, sPartSlip.Substring(1, 1))

                        .SetItemTable("regdt", 11, iRow, rsRegDT)
                        .SetItemTable("regid", 12, iRow, USER_INFO.USRID)
                        .SetItemTable("regip", 13, iRow, USER_INFO.LOCALIP)
                    End If
                Next
            End With

            fnCollectItemTable_69 = it69
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
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
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsWGrpCd As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentWGrpInfo(rsWGrpCd)

            If dt.Rows.Count > 0 Then
                Return "작업그룹코드(" + dt.Rows(0).Item(0).ToString + ")는 이미 사용 중입니다." + vbCrLf + vbCrLf + _
                       "확인하여 주십시요!!"
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

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it69 As New LISAPP.ItemTableCollection
            Dim iRegType69 As Integer = 0
            Dim sRegDT As String

            iRegType69 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it69 = fnCollectItemTable_69(sRegDT)

            If mobjDAF.TransWGrpInfo(it69, iRegType69, Me.txtWkGrpCd.Text, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Len(Me.txtWkGrpCd.Text.Trim) < 2 Then
                MsgBox("작업그룹코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtWkGrpCd.Text)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Me.txtWkGrpNm.Text.Trim = "" Then
                MsgBox("작업그룹명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtWkGrpNmS.Text.Trim = "" Then
                MsgBox("작업그룹명(약어)를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtWkGrpNmD.Text.Trim = "" Then
                MsgBox("작업그룹명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtWkGrpNmP.Text.Trim = "" Then
                MsgBox("작업그룹명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
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

    Public Sub sbDisplayCdDetail(ByVal rsWGrpCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                sbDisplayCdList_Ref()
            End If

            sbDisplayCdDetail_WGrp(rsWGrpCd)
            sbDisplayCdDetail_WGrp_Test(rsWGrpCd)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_WGrp(ByVal rsWGrpCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_TGrp(String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As New DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetWGrpInfo(rsWGrpCd)
            Else
                dt = mobjDAF.GetWGrpInfo(gsModDT, gsModID.Replace("-", "").Replace(":", "").Replace(" ", ""), rsWGrpCd)
            End If

            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            sbInitialize()

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

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_WGrp_Test(ByVal rsWGrpCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_TGrp_Test()"

        Dim iCol As Integer = 0
        Modpartslip = cboSlip.Text

        Try
            Me.spdTestCd.MaxRows = 0

            Dim dt As New DataTable

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetWGrpInfo_Test(rsWGrpCd)
            Else
                dt = mobjDAF.GetWGrpInfo_Test(gsModDT, gsModID, rsWGrpCd)
            End If

            Dim alTestCd As New ArrayList

            With Me.spdTestCd
                For ix As Integer = 0 To dt.Rows.Count - 1
                    If alTestCd.Contains(dt.Rows(ix).Item("testcd").ToString + "/" + dt.Rows(ix).Item("spccd").ToString) Then
                    Else
                        alTestCd.Add(dt.Rows(ix).Item("testcd").ToString + "/" + dt.Rows(ix).Item("spccd").ToString)

                        .MaxRows += 1
                        .Row = .MaxRows
                        iCol = .GetColFromID("testcd") : If iCol > 0 Then .Col = iCol : .Text = dt.Rows(ix).Item("testcd").ToString
                        iCol = .GetColFromID("spccd") : If iCol > 0 Then .Col = iCol : .Text = dt.Rows(ix).Item("spccd").ToString
                        iCol = .GetColFromID("tnmd") : If iCol > 0 Then .Col = iCol : .Text = dt.Rows(ix).Item("tnmd").ToString
                        iCol = .GetColFromID("partslip") : If iCol > 0 Then .Col = iCol : .Text = dt.Rows(ix).Item("partslip").ToString
                    End If
                Next

            End With
            'Ctrl.DisplayAfterSelect(Me.spdTestCd, dt, "L", True)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref()
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref()"

        Try
            miSelectKey = 1

            sbDisplayCdList_Ref_slip()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_slip()
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_slip()"

        Try
            Me.cboSlip.Items.Clear()

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()

            With dt
                If .Rows.Count = 0 Then Return

                For i As Integer = 1 To .Rows.Count
                    Me.cboSlip.Items.Add("[" + .Rows(i - 1).Item("slipcd").ToString + "] " + .Rows(i - 1).Item("slipnmd").ToString())
                Next
            End With

            If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

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
            sbDisplayCdList_Ref_slip()

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

    Private Sub sbInitialize_Control(Optional ByVal riMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If riMode = 0 Then
                'tpg1 초기화
                Me.txtWkGrpCd.Text = "" : Me.btnUE.Visible = False
                Me.txtWkGrpNm.Text = "" : txtWkGrpNmS.Text = "" : Me.txtWkGrpNmD.Text = ""
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtWkGrpNmP.Text = "" : Me.txtRegNm.Text = ""

                Me.cboWgrpGbn.SelectedIndex = -1
                Me.spdTestCd.MaxRows = 0
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
    Friend WithEvents tbcWkg As System.Windows.Forms.TabControl
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents lblWkGrpNmS As System.Windows.Forms.Label
    Friend WithEvents lblWkGrpNm As System.Windows.Forms.Label
    Friend WithEvents lblWkGrpNmD As System.Windows.Forms.Label
    Friend WithEvents lblWkGrpNmP As System.Windows.Forms.Label
    Friend WithEvents txtWkGrpNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtWkGrpNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtWkGrpNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtWkGrpNm As System.Windows.Forms.TextBox
    Friend WithEvents txtWkGrpCd As System.Windows.Forms.TextBox
    Friend WithEvents lblWkGrpCd As System.Windows.Forms.Label
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF05))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tbcWkg = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtModID = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.chkSpcGbn = New System.Windows.Forms.CheckBox
        Me.btnAddSlip = New System.Windows.Forms.Button
        Me.cboWgrpGbn = New System.Windows.Forms.ComboBox
        Me.lblWgrpGbn = New System.Windows.Forms.Label
        Me.cboSlip = New System.Windows.Forms.ComboBox
        Me.spdTestCd = New AxFPSpreadADO.AxfpSpread
        Me.lblSlip = New System.Windows.Forms.Label
        Me.lblWkGrpNmP = New System.Windows.Forms.Label
        Me.txtWkGrpNmP = New System.Windows.Forms.TextBox
        Me.lblWkGrpNmD = New System.Windows.Forms.Label
        Me.txtWkGrpNmD = New System.Windows.Forms.TextBox
        Me.lblWkGrpNmS = New System.Windows.Forms.Label
        Me.txtWkGrpNmS = New System.Windows.Forms.TextBox
        Me.lblWkGrpNm = New System.Windows.Forms.Label
        Me.txtWkGrpNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.lblWkGrpCd = New System.Windows.Forms.Label
        Me.txtWkGrpCd = New System.Windows.Forms.TextBox
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tbcWkg.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.spdTestCd, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTop.Controls.Add(Me.tbcWkg)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 116
        '
        'tbcWkg
        '
        Me.tbcWkg.Controls.Add(Me.tbcTpg)
        Me.tbcWkg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcWkg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcWkg.ItemSize = New System.Drawing.Size(84, 17)
        Me.tbcWkg.Location = New System.Drawing.Point(0, 0)
        Me.tbcWkg.Name = "tbcWkg"
        Me.tbcWkg.SelectedIndex = 0
        Me.tbcWkg.Size = New System.Drawing.Size(788, 601)
        Me.tbcWkg.TabIndex = 0
        Me.tbcWkg.TabStop = False
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtModNm)
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.txtModID)
        Me.tbcTpg.Controls.Add(Me.lblModNm)
        Me.tbcTpg.Controls.Add(Me.txtModDT)
        Me.tbcTpg.Controls.Add(Me.lblModDT)
        Me.tbcTpg.Controls.Add(Me.txtRegDT)
        Me.tbcTpg.Controls.Add(Me.lblUserNm)
        Me.tbcTpg.Controls.Add(Me.lblRegDT)
        Me.tbcTpg.Controls.Add(Me.txtRegID)
        Me.tbcTpg.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg.Controls.Add(Me.grpCd)
        Me.tbcTpg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcTpg.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg.Name = "tbcTpg"
        Me.tbcTpg.Size = New System.Drawing.Size(780, 576)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "작업그룹정보"
        Me.tbcTpg.UseVisualStyleBackColor = True
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(298, 547)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 183
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(700, 547)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 11
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(298, 547)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(68, 21)
        Me.txtModID.TabIndex = 10
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        Me.txtModID.Visible = False
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(213, 547)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 9
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(93, 547)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 8
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(8, 547)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(84, 21)
        Me.lblModDT.TabIndex = 7
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(495, 547)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 0
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(615, 547)
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
        Me.lblRegDT.Location = New System.Drawing.Point(410, 547)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(700, 547)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.chkSpcGbn)
        Me.grpCdInfo1.Controls.Add(Me.btnAddSlip)
        Me.grpCdInfo1.Controls.Add(Me.cboWgrpGbn)
        Me.grpCdInfo1.Controls.Add(Me.lblWgrpGbn)
        Me.grpCdInfo1.Controls.Add(Me.cboSlip)
        Me.grpCdInfo1.Controls.Add(Me.spdTestCd)
        Me.grpCdInfo1.Controls.Add(Me.lblSlip)
        Me.grpCdInfo1.Controls.Add(Me.lblWkGrpNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtWkGrpNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblWkGrpNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtWkGrpNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblWkGrpNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtWkGrpNmS)
        Me.grpCdInfo1.Controls.Add(Me.lblWkGrpNm)
        Me.grpCdInfo1.Controls.Add(Me.txtWkGrpNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 58)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 483)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "작업그룹정보"
        '
        'chkSpcGbn
        '
        Me.chkSpcGbn.AutoSize = True
        Me.chkSpcGbn.Location = New System.Drawing.Point(664, 18)
        Me.chkSpcGbn.Name = "chkSpcGbn"
        Me.chkSpcGbn.Size = New System.Drawing.Size(90, 16)
        Me.chkSpcGbn.TabIndex = 8
        Me.chkSpcGbn.TabStop = False
        Me.chkSpcGbn.Text = "검체로 설정"
        Me.chkSpcGbn.UseVisualStyleBackColor = True
        '
        'btnAddSlip
        '
        Me.btnAddSlip.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddSlip.Image = CType(resources.GetObject("btnAddSlip.Image"), System.Drawing.Image)
        Me.btnAddSlip.Location = New System.Drawing.Point(634, 15)
        Me.btnAddSlip.Name = "btnAddSlip"
        Me.btnAddSlip.Size = New System.Drawing.Size(26, 21)
        Me.btnAddSlip.TabIndex = 7
        Me.btnAddSlip.TabStop = False
        Me.btnAddSlip.UseVisualStyleBackColor = True
        '
        'cboWgrpGbn
        '
        Me.cboWgrpGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWgrpGbn.FormattingEnabled = True
        Me.cboWgrpGbn.IntegralHeight = False
        Me.cboWgrpGbn.Items.AddRange(New Object() {"[1] 일", "[2] 월", "[3] 년"})
        Me.cboWgrpGbn.Location = New System.Drawing.Point(132, 104)
        Me.cboWgrpGbn.Name = "cboWgrpGbn"
        Me.cboWgrpGbn.Size = New System.Drawing.Size(121, 20)
        Me.cboWgrpGbn.TabIndex = 5
        Me.cboWgrpGbn.Tag = "wkgrpgbn_01"
        '
        'lblWgrpGbn
        '
        Me.lblWgrpGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWgrpGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWgrpGbn.ForeColor = System.Drawing.Color.White
        Me.lblWgrpGbn.Location = New System.Drawing.Point(8, 104)
        Me.lblWgrpGbn.Name = "lblWgrpGbn"
        Me.lblWgrpGbn.Size = New System.Drawing.Size(123, 21)
        Me.lblWgrpGbn.TabIndex = 145
        Me.lblWgrpGbn.Text = "번호발생구분"
        Me.lblWgrpGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.Location = New System.Drawing.Point(500, 15)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(132, 20)
        Me.cboSlip.TabIndex = 6
        Me.cboSlip.Tag = "TGRPTYPE_01"
        '
        'spdTestCd
        '
        Me.spdTestCd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdTestCd.DataSource = Nothing
        Me.spdTestCd.Location = New System.Drawing.Point(378, 39)
        Me.spdTestCd.Name = "spdTestCd"
        Me.spdTestCd.OcxState = CType(resources.GetObject("spdTestCd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTestCd.Size = New System.Drawing.Size(378, 423)
        Me.spdTestCd.TabIndex = 9
        '
        'lblSlip
        '
        Me.lblSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlip.ForeColor = System.Drawing.Color.White
        Me.lblSlip.Location = New System.Drawing.Point(378, 15)
        Me.lblSlip.Name = "lblSlip"
        Me.lblSlip.Size = New System.Drawing.Size(120, 21)
        Me.lblSlip.TabIndex = 140
        Me.lblSlip.Text = "검사분야/검사항목"
        Me.lblSlip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblWkGrpNmP
        '
        Me.lblWkGrpNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWkGrpNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWkGrpNmP.ForeColor = System.Drawing.Color.White
        Me.lblWkGrpNmP.Location = New System.Drawing.Point(8, 82)
        Me.lblWkGrpNmP.Name = "lblWkGrpNmP"
        Me.lblWkGrpNmP.Size = New System.Drawing.Size(123, 21)
        Me.lblWkGrpNmP.TabIndex = 9
        Me.lblWkGrpNmP.Text = "작업그룹명(바코드)"
        Me.lblWkGrpNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWkGrpNmP
        '
        Me.txtWkGrpNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkGrpNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkGrpNmP.Location = New System.Drawing.Point(132, 82)
        Me.txtWkGrpNmP.MaxLength = 2
        Me.txtWkGrpNmP.Name = "txtWkGrpNmP"
        Me.txtWkGrpNmP.Size = New System.Drawing.Size(30, 21)
        Me.txtWkGrpNmP.TabIndex = 4
        Me.txtWkGrpNmP.Tag = "wkgrpnmbp"
        '
        'lblWkGrpNmD
        '
        Me.lblWkGrpNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWkGrpNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWkGrpNmD.ForeColor = System.Drawing.Color.White
        Me.lblWkGrpNmD.Location = New System.Drawing.Point(8, 60)
        Me.lblWkGrpNmD.Name = "lblWkGrpNmD"
        Me.lblWkGrpNmD.Size = New System.Drawing.Size(123, 21)
        Me.lblWkGrpNmD.TabIndex = 7
        Me.lblWkGrpNmD.Text = "검사그룹명(화면)"
        Me.lblWkGrpNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWkGrpNmD
        '
        Me.txtWkGrpNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkGrpNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkGrpNmD.Location = New System.Drawing.Point(132, 60)
        Me.txtWkGrpNmD.MaxLength = 20
        Me.txtWkGrpNmD.Name = "txtWkGrpNmD"
        Me.txtWkGrpNmD.Size = New System.Drawing.Size(156, 21)
        Me.txtWkGrpNmD.TabIndex = 3
        Me.txtWkGrpNmD.Tag = "wkgrpnmd"
        '
        'lblWkGrpNmS
        '
        Me.lblWkGrpNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWkGrpNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWkGrpNmS.ForeColor = System.Drawing.Color.White
        Me.lblWkGrpNmS.Location = New System.Drawing.Point(8, 38)
        Me.lblWkGrpNmS.Name = "lblWkGrpNmS"
        Me.lblWkGrpNmS.Size = New System.Drawing.Size(123, 21)
        Me.lblWkGrpNmS.TabIndex = 0
        Me.lblWkGrpNmS.Text = "작업그룹명(약어)"
        Me.lblWkGrpNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWkGrpNmS
        '
        Me.txtWkGrpNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkGrpNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkGrpNmS.Location = New System.Drawing.Point(132, 38)
        Me.txtWkGrpNmS.MaxLength = 10
        Me.txtWkGrpNmS.Name = "txtWkGrpNmS"
        Me.txtWkGrpNmS.Size = New System.Drawing.Size(156, 21)
        Me.txtWkGrpNmS.TabIndex = 2
        Me.txtWkGrpNmS.Tag = "wkgrpnms"
        '
        'lblWkGrpNm
        '
        Me.lblWkGrpNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWkGrpNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWkGrpNm.ForeColor = System.Drawing.Color.White
        Me.lblWkGrpNm.Location = New System.Drawing.Point(8, 16)
        Me.lblWkGrpNm.Name = "lblWkGrpNm"
        Me.lblWkGrpNm.Size = New System.Drawing.Size(123, 21)
        Me.lblWkGrpNm.TabIndex = 0
        Me.lblWkGrpNm.Text = "작업그룹명"
        Me.lblWkGrpNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWkGrpNm
        '
        Me.txtWkGrpNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkGrpNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkGrpNm.Location = New System.Drawing.Point(132, 16)
        Me.txtWkGrpNm.MaxLength = 20
        Me.txtWkGrpNm.Name = "txtWkGrpNm"
        Me.txtWkGrpNm.Size = New System.Drawing.Size(156, 21)
        Me.txtWkGrpNm.TabIndex = 1
        Me.txtWkGrpNm.Tag = "wkgrpnm"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.lblWkGrpCd)
        Me.grpCd.Controls.Add(Me.txtWkGrpCd)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 8)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
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
        'lblWkGrpCd
        '
        Me.lblWkGrpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWkGrpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWkGrpCd.ForeColor = System.Drawing.Color.White
        Me.lblWkGrpCd.Location = New System.Drawing.Point(8, 15)
        Me.lblWkGrpCd.Name = "lblWkGrpCd"
        Me.lblWkGrpCd.Size = New System.Drawing.Size(84, 21)
        Me.lblWkGrpCd.TabIndex = 0
        Me.lblWkGrpCd.Text = "작업그룹코드"
        Me.lblWkGrpCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWkGrpCd
        '
        Me.txtWkGrpCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkGrpCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWkGrpCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkGrpCd.Location = New System.Drawing.Point(93, 15)
        Me.txtWkGrpCd.MaxLength = 2
        Me.txtWkGrpCd.Name = "txtWkGrpCd"
        Me.txtWkGrpCd.Size = New System.Drawing.Size(28, 21)
        Me.txtWkGrpCd.TabIndex = 0
        Me.txtWkGrpCd.Tag = "wkgrpcd"
        '
        'FDF05
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF05"
        Me.Text = "[05] 작업그룹"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tbcWkg.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.spdTestCd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnAddTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSlip.Click
        Dim sFn As String = "btnAddTest_Click"

        If chkSpcGbn.Checked Then
            sbDisplay_CHHELP_spc()
        Else
            sbDisplay_CDHELP_test()
        End If

    End Sub

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        If txtWkGrpCd.Text = "" Then Exit Sub

        Try
            Dim sMsg As String = "작업그룹코드   : " & txtWkGrpCd.Text & vbCrLf
            sMsg &= "작업그룹명     : " & txtWkGrpNm.Text & vbCrLf & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransWGrpInfo_UE(txtWkGrpCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 작업그룹정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub txtTGrpNm_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtWkGrpNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If txtWkGrpNmS.Text.Trim = "" Then
            If txtWkGrpNm.Text.Length > txtWkGrpNmS.MaxLength Then
                txtWkGrpNmS.Text = txtWkGrpNm.Text.Substring(0, txtWkGrpNmS.MaxLength)
            Else
                txtWkGrpNmS.Text = txtWkGrpNm.Text
            End If
        End If

        If txtWkGrpNmD.Text.Trim = "" Then
            If txtWkGrpNm.Text.Length > txtWkGrpNmD.MaxLength Then
                txtWkGrpNmD.Text = txtWkGrpNm.Text.Substring(0, txtWkGrpNmD.MaxLength)
            Else
                txtWkGrpNmD.Text = txtWkGrpNm.Text
            End If
        End If

        If txtWkGrpNmP.Text.Trim = "" Then
            If txtWkGrpNm.Text.Length > txtWkGrpNmP.MaxLength Then
                'txtTGrpNmP.Text = txtTGrpNm.Text.Substring(0, txtTGrpNmP.MaxLength)
            Else
                txtWkGrpNmP.Text = txtWkGrpNm.Text
            End If
        End If
    End Sub

    Private Sub spdTestCd_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdTestCd.DblClick
        Dim sFn As String = "spdTestCd_DblClick"

        Try

            If e.row < 1 Then Return

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdTestCd

            Dim dblRowHeight As Double = spd.get_RowHeight(e.row)
            Dim sDelFlg As String = Ctrl.Get_Code(spd, "delflg", e.row)

            'With spd
            '    .Row = .ActiveRow
            '    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            '    .MaxRows -= 1
            'End With
            With spd
                .Col = 1 : .Col2 = .MaxCols
                .Row = e.row : .Row2 = e.row
                .BlockMode = True

                If sDelFlg = "D" Then
                    .FontStrikethru = False
                Else
                    .FontStrikethru = True
                End If

                .set_RowHeight(e.row, dblRowHeight)

                .BlockMode = False

                .SetText(.GetColFromID("delflg"), e.row, IIf(sDelFlg = "D", "", "D"))
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub


    Private Sub FDF09_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtWkGrpCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWkGrpCd.KeyDown, txtWkGrpNm.KeyDown, txtWkGrpNmD.KeyDown, txtWkGrpNmP.KeyDown, txtWkGrpNmS.KeyDown, cboWgrpGbn.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        Dim rsWGrpCd As String = txtWkGrpCd.Text
        If Modpartslip <> Me.cboSlip.Text Then
            Me.spdTestCd.MaxRows = 0
        Else
            sbDisplayCdDetail_WGrp_Test(rsWGrpCd)
        End If
    End Sub
End Class
