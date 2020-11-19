'>>> [09] 검사그룹
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF09
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF09.vb, Class : FDF09" + vbTab

    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT

    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_TGRP

    Public gsModDT As String = ""
    Friend WithEvents btnAddSlip As System.Windows.Forms.Button
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents spdTestCd As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblSlip As System.Windows.Forms.Label
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents TxtModNm As System.Windows.Forms.TextBox
    Public gsModID As String = ""

    Private Function fnCollectItemTable_63(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_63() As LISAPP.ItemTableCollection"

        Try
            Dim it63 As New LISAPP.ItemTableCollection

            For ix As Integer = 1 To Me.spdTestCd.MaxRows

                Dim sTestCd As String = "", sSpcCd As String = ""

                With Me.spdTestCd
                    .Row = ix
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                End With

                With it63
                    .SetItemTable("tgrpcd", 1, ix, Me.txtTGrpCd.Text)
                    .SetItemTable("testcd", 2, ix, sTestCd)
                    .SetItemTable("spccd", 3, ix, sSpcCd)

                    .SetItemTable("tgrpnm", 4, ix, Me.txtTGrpNm.Text)
                    .SetItemTable("tgrpnms", 5, ix, Me.txtTGrpNmS.Text)
                    .SetItemTable("tgrpnmd", 6, ix, Me.txtTGrpNmD.Text)
                    .SetItemTable("tgrpnmbp", 7, ix, Me.txtTGrpNmP.Text)

                    .SetItemTable("regdt", 8, ix, rsRegDT)
                    .SetItemTable("regid", 9, ix, USER_INFO.USRID)
                    .SetItemTable("regip", 10, ix, USER_INFO.LOCALIP)

                End With
            Next

            fnCollectItemTable_63 = it63
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

    Private Function fnFindConflict(ByVal rsTGrpCd As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentTGrpInfo(rsTGrpCd)

            If dt.Rows.Count > 0 Then
                Return "검사그룹코드(" + dt.Rows(0).Item(0).ToString + ")는 이미 사용 중입니다." + vbCrLf + vbCrLf + _
                       "확인하여 주십시요!!"
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
            Dim it63 As New LISAPP.ItemTableCollection
            Dim iRegType63 As Integer = 0
            Dim sRegDT As String

            iRegType63 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it63 = fnCollectItemTable_63(sRegDT)

            If mobjDAF.TransTGrpInfo(it63, iRegType63, Me.txtTGrpCd.Text, USER_INFO.USRID) Then
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
            If Len(Me.txtTGrpCd.Text.Trim) < 2 Then
                MsgBox("검사그룹코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtTGrpCd.Text)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Me.txtTGrpNm.Text.Trim = "" Then
                MsgBox("검사그룹명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTGrpNmS.Text.Trim = "" Then
                MsgBox("검사그룹명(약어)를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTGrpNmD.Text.Trim = "" Then
                MsgBox("검사그룹명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            'If Me.txtTGrpNmP.Text.Trim = "" Then
            '    MsgBox("검사그룹명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
            '    Exit Function
            'End If

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

    Public Sub sbDisplayCdDetail(ByVal rsTGrpCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                sbDisplayCdList_Ref()
            End If

            sbDisplayCdDetail_TGrp(rsTGrpCd)
            sbDisplayCdDetail_TGrp_Test(rsTGrpCd)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_TGrp(ByVal rsTGrpCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_TGrp(String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As New DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetTGrpInfo(rsTGrpCd)
            Else
                dt = mobjDAF.GetTGrpInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsTGrpCd)
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
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_TGrp_Test(ByVal rsTGrpCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_TGrp_Test()"
        Dim iCol As Integer = 0

        Try
            Me.spdTestCd.MaxRows = 0

            Dim dt As New DataTable

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetTGrpInfo_Test(rsTGrpCd)
            Else
                dt = mobjDAF.GetTGrpInfo_Test(gsModDT, gsModID, rsTGrpCd)
            End If

            Ctrl.DisplayAfterSelect(Me.spdTestCd, dt, "L", True)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref()
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref()"

        Try
            miSelectKey = 1

            sbDisplayCdList_Ref_Slip()

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_Slip()
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_Slip()"

        Try
            Me.cboSlip.Items.Clear()

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()

            If dt.Rows.Count = 0 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString())
            Next

            If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0

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
                Me.txtTGrpCd.Text = "" : Me.btnUE.Visible = False
                Me.txtTGrpNm.Text = "" : Me.txtTGrpNmS.Text = "" : Me.txtTGrpNmD.Text = "" : Me.txtTGrpNmP.Text = ""
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtRegNm.Text = ""

                Me.spdTestCd.MaxRows = 0
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
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
    Friend WithEvents lblTGrpNmS As System.Windows.Forms.Label
    Friend WithEvents lblTGrpNm As System.Windows.Forms.Label
    Friend WithEvents lblTGrpNmD As System.Windows.Forms.Label
    Friend WithEvents lblTGrpNmP As System.Windows.Forms.Label
    Friend WithEvents txtTGrpNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtTGrpNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtTGrpNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtTGrpNm As System.Windows.Forms.TextBox
    Friend WithEvents txtTGrpCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTGrpCd As System.Windows.Forms.Label
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF09))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tpg1 = New System.Windows.Forms.TabPage
        Me.TxtModNm = New System.Windows.Forms.TextBox
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
        Me.btnAddSlip = New System.Windows.Forms.Button
        Me.cboSlip = New System.Windows.Forms.ComboBox
        Me.spdTestCd = New AxFPSpreadADO.AxfpSpread
        Me.lblSlip = New System.Windows.Forms.Label
        Me.lblTGrpNmP = New System.Windows.Forms.Label
        Me.txtTGrpNmP = New System.Windows.Forms.TextBox
        Me.lblTGrpNmD = New System.Windows.Forms.Label
        Me.txtTGrpNmD = New System.Windows.Forms.TextBox
        Me.lblTGrpNmS = New System.Windows.Forms.Label
        Me.txtTGrpNmS = New System.Windows.Forms.TextBox
        Me.lblTGrpNm = New System.Windows.Forms.Label
        Me.txtTGrpNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.lblTGrpCd = New System.Windows.Forms.Label
        Me.txtTGrpCd = New System.Windows.Forms.TextBox
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tpg1.SuspendLayout()
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
        Me.tpg1.Controls.Add(Me.TxtModNm)
        Me.tpg1.Controls.Add(Me.txtRegNm)
        Me.tpg1.Controls.Add(Me.txtModID)
        Me.tpg1.Controls.Add(Me.lblModNm)
        Me.tpg1.Controls.Add(Me.txtModDT)
        Me.tpg1.Controls.Add(Me.lblModDT)
        Me.tpg1.Controls.Add(Me.txtRegDT)
        Me.tpg1.Controls.Add(Me.lblUserNm)
        Me.tpg1.Controls.Add(Me.lblRegDT)
        Me.tpg1.Controls.Add(Me.txtRegID)
        Me.tpg1.Controls.Add(Me.grpCdInfo1)
        Me.tpg1.Controls.Add(Me.grpCd)
        Me.tpg1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpg1.Location = New System.Drawing.Point(4, 21)
        Me.tpg1.Name = "tpg1"
        Me.tpg1.Size = New System.Drawing.Size(780, 576)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "검사그룹정보"
        '
        'TxtModNm
        '
        Me.TxtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtModNm.BackColor = System.Drawing.Color.LightGray
        Me.TxtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TxtModNm.Location = New System.Drawing.Point(298, 547)
        Me.TxtModNm.Name = "TxtModNm"
        Me.TxtModNm.ReadOnly = True
        Me.TxtModNm.Size = New System.Drawing.Size(68, 21)
        Me.TxtModNm.TabIndex = 183
        Me.TxtModNm.TabStop = False
        Me.TxtModNm.Tag = "MODNM"
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
        Me.grpCdInfo1.Controls.Add(Me.btnAddSlip)
        Me.grpCdInfo1.Controls.Add(Me.cboSlip)
        Me.grpCdInfo1.Controls.Add(Me.spdTestCd)
        Me.grpCdInfo1.Controls.Add(Me.lblSlip)
        Me.grpCdInfo1.Controls.Add(Me.lblTGrpNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtTGrpNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblTGrpNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtTGrpNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblTGrpNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtTGrpNmS)
        Me.grpCdInfo1.Controls.Add(Me.lblTGrpNm)
        Me.grpCdInfo1.Controls.Add(Me.txtTGrpNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 58)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 483)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "검사그룹정보"
        '
        'btnAddSlip
        '
        Me.btnAddSlip.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddSlip.Image = CType(resources.GetObject("btnAddSlip.Image"), System.Drawing.Image)
        Me.btnAddSlip.Location = New System.Drawing.Point(729, 14)
        Me.btnAddSlip.Name = "btnAddSlip"
        Me.btnAddSlip.Size = New System.Drawing.Size(26, 21)
        Me.btnAddSlip.TabIndex = 8
        Me.btnAddSlip.TabStop = False
        Me.btnAddSlip.UseVisualStyleBackColor = True
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.Location = New System.Drawing.Point(499, 15)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(228, 20)
        Me.cboSlip.TabIndex = 7
        Me.cboSlip.Tag = ""
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
        'lblTGrpNmP
        '
        Me.lblTGrpNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTGrpNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTGrpNmP.ForeColor = System.Drawing.Color.White
        Me.lblTGrpNmP.Location = New System.Drawing.Point(8, 82)
        Me.lblTGrpNmP.Name = "lblTGrpNmP"
        Me.lblTGrpNmP.Size = New System.Drawing.Size(123, 21)
        Me.lblTGrpNmP.TabIndex = 9
        Me.lblTGrpNmP.Text = "검사그룹명(바코드)"
        Me.lblTGrpNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTGrpNmP
        '
        Me.txtTGrpNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTGrpNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTGrpNmP.Location = New System.Drawing.Point(132, 82)
        Me.txtTGrpNmP.MaxLength = 2
        Me.txtTGrpNmP.Name = "txtTGrpNmP"
        Me.txtTGrpNmP.Size = New System.Drawing.Size(20, 21)
        Me.txtTGrpNmP.TabIndex = 6
        Me.txtTGrpNmP.Tag = "TGRPNMBP"
        '
        'lblTGrpNmD
        '
        Me.lblTGrpNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTGrpNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTGrpNmD.ForeColor = System.Drawing.Color.White
        Me.lblTGrpNmD.Location = New System.Drawing.Point(8, 60)
        Me.lblTGrpNmD.Name = "lblTGrpNmD"
        Me.lblTGrpNmD.Size = New System.Drawing.Size(123, 21)
        Me.lblTGrpNmD.TabIndex = 7
        Me.lblTGrpNmD.Text = "검사그룹명(화면)"
        Me.lblTGrpNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTGrpNmD
        '
        Me.txtTGrpNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTGrpNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTGrpNmD.Location = New System.Drawing.Point(132, 60)
        Me.txtTGrpNmD.MaxLength = 20
        Me.txtTGrpNmD.Name = "txtTGrpNmD"
        Me.txtTGrpNmD.Size = New System.Drawing.Size(156, 21)
        Me.txtTGrpNmD.TabIndex = 5
        Me.txtTGrpNmD.Tag = "TGRPNMD"
        '
        'lblTGrpNmS
        '
        Me.lblTGrpNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTGrpNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTGrpNmS.ForeColor = System.Drawing.Color.White
        Me.lblTGrpNmS.Location = New System.Drawing.Point(8, 38)
        Me.lblTGrpNmS.Name = "lblTGrpNmS"
        Me.lblTGrpNmS.Size = New System.Drawing.Size(123, 21)
        Me.lblTGrpNmS.TabIndex = 0
        Me.lblTGrpNmS.Text = "검사그룹명(약어)"
        Me.lblTGrpNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTGrpNmS
        '
        Me.txtTGrpNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTGrpNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTGrpNmS.Location = New System.Drawing.Point(132, 38)
        Me.txtTGrpNmS.MaxLength = 10
        Me.txtTGrpNmS.Name = "txtTGrpNmS"
        Me.txtTGrpNmS.Size = New System.Drawing.Size(156, 21)
        Me.txtTGrpNmS.TabIndex = 4
        Me.txtTGrpNmS.Tag = "TGRPNMS"
        '
        'lblTGrpNm
        '
        Me.lblTGrpNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTGrpNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTGrpNm.ForeColor = System.Drawing.Color.White
        Me.lblTGrpNm.Location = New System.Drawing.Point(8, 16)
        Me.lblTGrpNm.Name = "lblTGrpNm"
        Me.lblTGrpNm.Size = New System.Drawing.Size(123, 21)
        Me.lblTGrpNm.TabIndex = 0
        Me.lblTGrpNm.Text = "검사그룹명"
        Me.lblTGrpNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTGrpNm
        '
        Me.txtTGrpNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTGrpNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTGrpNm.Location = New System.Drawing.Point(132, 16)
        Me.txtTGrpNm.MaxLength = 20
        Me.txtTGrpNm.Name = "txtTGrpNm"
        Me.txtTGrpNm.Size = New System.Drawing.Size(156, 21)
        Me.txtTGrpNm.TabIndex = 3
        Me.txtTGrpNm.Tag = "TGRPNM"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.lblTGrpCd)
        Me.grpCd.Controls.Add(Me.txtTGrpCd)
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
        Me.btnUE.TabIndex = 2
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'lblTGrpCd
        '
        Me.lblTGrpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTGrpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTGrpCd.ForeColor = System.Drawing.Color.White
        Me.lblTGrpCd.Location = New System.Drawing.Point(8, 15)
        Me.lblTGrpCd.Name = "lblTGrpCd"
        Me.lblTGrpCd.Size = New System.Drawing.Size(84, 21)
        Me.lblTGrpCd.TabIndex = 0
        Me.lblTGrpCd.Text = "검사그룹코드"
        Me.lblTGrpCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTGrpCd
        '
        Me.txtTGrpCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTGrpCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTGrpCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTGrpCd.Location = New System.Drawing.Point(93, 15)
        Me.txtTGrpCd.MaxLength = 2
        Me.txtTGrpCd.Name = "txtTGrpCd"
        Me.txtTGrpCd.Size = New System.Drawing.Size(28, 21)
        Me.txtTGrpCd.TabIndex = 1
        Me.txtTGrpCd.Tag = "TGRPCD"
        '
        'FDF09
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF09"
        Me.Text = "[09] 검사그룹"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tpg1.ResumeLayout(False)
        Me.tpg1.PerformLayout()
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

        Try
            Dim iHeight As Integer = Convert.ToInt32(spdTestCd.Height)
            Dim iWidth As Integer = Convert.ToInt32(spdTestCd.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Me.Top + Me.btnAddSlip.Top + Me.btnAddSlip.Height + Ctrl.menuHeight

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Me.Left + Me.btnAddSlip.Left

            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - Me.btnAddSlip.Width)

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_testspc_list(Ctrl.Get_Code(cboSlip), "")
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
            objHelp.MaxRows = 15

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("testspcd", "코드", 0, , , True, , "Y")
            objHelp.AddField("partslip", "코드", 0, , , True)
            objHelp.AddField("tcdgbn", "구분", 0, , , True)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

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
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        If txtTGrpCd.Text = "" Then Exit Sub

        Try
            Dim sMsg As String = "검사그룹코드   : " + Me.txtTGrpCd.Text + vbCrLf
            sMsg += "검사그룹명     : " + Me.txtTGrpNm.Text + vbCrLf + vbCrLf
            sMsg += "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransTGrpInfo_UE(Me.txtTGrpCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 검사그룹정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub txtTGrpNm_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTGrpNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If Me.txtTGrpNmS.Text.Trim = "" Then
            If Me.txtTGrpNm.Text.Length > Me.txtTGrpNmS.MaxLength Then
                Me.txtTGrpNmS.Text = txtTGrpNm.Text.Substring(0, Me.txtTGrpNmS.MaxLength)
            Else
                Me.txtTGrpNmS.Text = Me.txtTGrpNm.Text
            End If
        End If

        If txtTGrpNmD.Text.Trim = "" Then
            If txtTGrpNm.Text.Length > txtTGrpNmD.MaxLength Then
                txtTGrpNmD.Text = txtTGrpNm.Text.Substring(0, txtTGrpNmD.MaxLength)
            Else
                txtTGrpNmD.Text = txtTGrpNm.Text
            End If
        End If

        If Me.txtTGrpNmP.Text.Trim = "" Then
            If Me.txtTGrpNm.Text.Length > Me.txtTGrpNmP.MaxLength Then
                'txtTGrpNmP.Text = txtTGrpNm.Text.Substring(0, txtTGrpNmP.MaxLength)
            Else
                Me.txtTGrpNmP.Text = txtTGrpNm.Text
            End If
        End If
    End Sub

    Private Sub spdTestCd_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdTestCd.DblClick
        Dim sFn As String = "spdTestCd_DblClick"

        Try
            If e.row < 1 Then Return

            With Me.spdTestCd
                .Row = e.row
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1
            End With

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

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

    Private Sub txtTGrpCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTGrpCd.KeyDown, txtTGrpNm.KeyDown, txtTGrpNmD.KeyDown, txtTGrpNmP.KeyDown, txtTGrpNmS.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub
End Class
