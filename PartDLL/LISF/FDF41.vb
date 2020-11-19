'>>> [40] 처방슬립
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF41
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF41.vb, Class : FDF41" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_KSRACK

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Public gsModDT As String = ""
    Friend WithEvents lblBccls As System.Windows.Forms.Label
    Friend WithEvents cboBcclsNmD As System.Windows.Forms.ComboBox
    Friend WithEvents txtBcclsCd As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxRow As System.Windows.Forms.TextBox
    Friend WithEvents lblMaxRow As System.Windows.Forms.Label
    Friend WithEvents txtMaxCol As System.Windows.Forms.TextBox
    Friend WithEvents lblMaxCol As System.Windows.Forms.Label
    Friend WithEvents grpCdInfo2 As System.Windows.Forms.GroupBox
    Friend WithEvents spdSpc As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Public gsModID As String = ""

    Private Sub sbDisplayCdDetail_KSSpc(ByVal rsBcclscd As String, ByVal rsRackid As String)
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetKSSpcInfo(rsBcclscd, rsRackid)
            Dim iCol As Integer = 0

            If dt.Rows.Count < 1 Then Return

            With spdSpc
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToUpper)

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    .Col = .GetColFromID("CHK") : .Row = i + 1

                    If .Text = "1" Then
                        .Col = 1 : .Col2 = .MaxCols : .Row = i + 1 : .Row2 = i + 1
                        .BlockMode = True
                        .BackColor = System.Drawing.Color.LavenderBlush
                        .BlockMode = False
                    Else
                        .Col = 1 : .Col2 = .MaxCols : .Row = i + 1 : .Row2 = i + 1
                        .BlockMode = True
                        .BackColor = System.Drawing.Color.White
                        .BlockMode = False
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_bccls(ByVal actrl As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_bccls(ByVal actrl As System.Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable = mobjDAF.GetBcclsInfo(rsUsDt)

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("bcclsnmd"))
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref(ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref(String)"

        Try
            miSelectKey = 1

            sbDisplayCdList_Ref_bccls(cboBcclsNmD, rsUsDt)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    txtTSectCd_Validating(txtBcclsCd, Nothing)
                End If
            End If
        End Try
    End Sub

    Private Function fnCollectItemTable_0(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_0(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it0 As New LISAPP.ItemTableCollection
            Dim iCnt As Integer = 0

            With spdSpc
                For i As Integer = 1 To .MaxRows
                    .Col = .GetColFromID("CHK") : .Row = i : Dim sChk As String = .Text

                    If sChk = "1" Then
                        iCnt += 1

                        it0.SetItemTable("BCCLSCD", 1, iCnt, Me.txtBcclsCd.Text)

                        .Col = .GetColFromID("SPCCD") : .Row = i
                        it0.SetItemTable("SPCCD", 2, iCnt, .Text)

                        it0.SetItemTable("RACKID", 3, iCnt, Me.txtRackId.Text.Trim)
                        it0.SetItemTable("REGDT", 4, iCnt, rsRegDT)
                        it0.SetItemTable("REGID", 5, iCnt, USER_INFO.USRID)
                        it0.SetItemTable("ALARMTERM", 6, iCnt, Me.txtAlarmterm.Text.Trim)
                        it0.SetItemTable("MAXCOL", 7, iCnt, Me.txtMaxCol.Text)
                        it0.SetItemTable("MAXROW", 8, iCnt, Me.txtMaxRow.Text)
                        it0.SetItemTable("REGIP", 9, iCnt, USER_INFO.LOCALIP)
                    End If
                Next
            End With

            fnCollectItemTable_0 = it0
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

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg_BKRack() As Boolean"

        Try
            Dim it0 As New LISAPP.ItemTableCollection
            Dim it1 As New LISAPP.ItemTableCollection

            Dim iRegType0 As Integer = 0, iRegType1 As Integer = 0
            Dim sRegDT As String

            iRegType0 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType1 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it0 = fnCollectItemTable_0(sRegDT)

            If mobjDAF.TransKSRackInfo(it0, iRegType0, Me.txtBcclsCd.Text, Me.txtRackId.Text.Trim, USER_INFO.USRID) Then
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
            If Len(Me.txtBcclsCd.Text.Trim) < 1 Then
                MsgBox("검사계를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtRackId.Text.Trim) < 1 Then
                MsgBox("보관검체 Rack ID를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If IsNumeric(Me.txtMaxCol.Text.Trim) = False Then
                MsgBox("Max Col를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If IsNumeric(Me.txtMaxRow.Text.Trim) = False Then
                MsgBox("Max Row (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
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

    Public Sub sbDisplayCdDetail(ByVal rsBCCLSCD As String, ByVal rsRackId As String, ByVal rsSpcCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1
            sbDisplayCdDetail_KSRack(rsBCCLSCD, rsRackId)
            sbDisplayCdDetail_KSSpc(rsBCCLSCD, rsRackId)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_KSRack(ByVal rsBcclscd As String, ByVal rsRackId As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_OSlip(String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetKSRackInfo(rsBcclscd, rsRackId)
            Else
                dt = mobjDAF.GetKSRackInfo(rsBcclscd, rsRackId, gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID)
            End If

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
            sbDisplayCdList_Ref(Format(Now, "yyyyMMdd").ToString)

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
                'tpg1 초기화
                txtBcclsCd.Text = "" : cboBcclsNmD.SelectedIndex = -1
                txtRackId.Text = "" : btnUE.Visible = False

                txtAlarmterm.Text = "" : txtAlarmterm.ReadOnly = False
                txtMaxCol.Text = "" : txtMaxRow.Text = ""

                txtModDT.Text = "" : txtModID.Text = "" : txtRegDT.Text = "" : txtRegID.Text = "" : txtRegNm.Text = ""

                sbDisplayCdDetail_KSSpc("", "")
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
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tbcKSRack As System.Windows.Forms.TabControl
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtRackId As System.Windows.Forms.TextBox
    Friend WithEvents lblAlarmterm As System.Windows.Forms.Label
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtAlarmterm As System.Windows.Forms.TextBox
    Friend WithEvents lblRackId As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF41))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tbcKSRack = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.grpCdInfo2 = New System.Windows.Forms.GroupBox
        Me.spdSpc = New AxFPSpreadADO.AxfpSpread
        Me.txtModID = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.txtMaxRow = New System.Windows.Forms.TextBox
        Me.lblMaxRow = New System.Windows.Forms.Label
        Me.txtMaxCol = New System.Windows.Forms.TextBox
        Me.lblMaxCol = New System.Windows.Forms.Label
        Me.lblAlarmterm = New System.Windows.Forms.Label
        Me.txtAlarmterm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.cboBcclsNmD = New System.Windows.Forms.ComboBox
        Me.txtBcclsCd = New System.Windows.Forms.TextBox
        Me.lblBccls = New System.Windows.Forms.Label
        Me.lblRackId = New System.Windows.Forms.Label
        Me.txtRackId = New System.Windows.Forms.TextBox
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tbcKSRack.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCdInfo2.SuspendLayout()
        CType(Me.spdSpc, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTop.Controls.Add(Me.tbcKSRack)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 116
        '
        'tbcKSRack
        '
        Me.tbcKSRack.Controls.Add(Me.tbcTpg)
        Me.tbcKSRack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcKSRack.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcKSRack.ItemSize = New System.Drawing.Size(84, 17)
        Me.tbcKSRack.Location = New System.Drawing.Point(0, 0)
        Me.tbcKSRack.Name = "tbcKSRack"
        Me.tbcKSRack.SelectedIndex = 0
        Me.tbcKSRack.Size = New System.Drawing.Size(788, 601)
        Me.tbcKSRack.TabIndex = 0
        Me.tbcKSRack.TabStop = False
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.txtModNm)
        Me.tbcTpg.Controls.Add(Me.grpCdInfo2)
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
        Me.tbcTpg.Text = "보관검체 Rack 설정"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(702, 544)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 148
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(295, 544)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(78, 21)
        Me.txtModNm.TabIndex = 143
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'grpCdInfo2
        '
        Me.grpCdInfo2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo2.Controls.Add(Me.spdSpc)
        Me.grpCdInfo2.Location = New System.Drawing.Point(298, 90)
        Me.grpCdInfo2.Name = "grpCdInfo2"
        Me.grpCdInfo2.Size = New System.Drawing.Size(474, 450)
        Me.grpCdInfo2.TabIndex = 21
        Me.grpCdInfo2.TabStop = False
        Me.grpCdInfo2.Text = "검체선택"
        '
        'spdSpc
        '
        Me.spdSpc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdSpc.DataSource = Nothing
        Me.spdSpc.Location = New System.Drawing.Point(18, 25)
        Me.spdSpc.Name = "spdSpc"
        Me.spdSpc.OcxState = CType(resources.GetObject("spdSpc.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSpc.Size = New System.Drawing.Size(377, 414)
        Me.spdSpc.TabIndex = 0
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(295, 544)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(68, 21)
        Me.txtModID.TabIndex = 17
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(210, 544)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 16
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(93, 544)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 15
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(8, 544)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(84, 21)
        Me.lblModDT.TabIndex = 14
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(500, 544)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 19
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(617, 544)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 21)
        Me.lblUserNm.TabIndex = 20
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(415, 544)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 17
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(702, 544)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 11
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.txtMaxRow)
        Me.grpCdInfo1.Controls.Add(Me.lblMaxRow)
        Me.grpCdInfo1.Controls.Add(Me.txtMaxCol)
        Me.grpCdInfo1.Controls.Add(Me.lblMaxCol)
        Me.grpCdInfo1.Controls.Add(Me.lblAlarmterm)
        Me.grpCdInfo1.Controls.Add(Me.txtAlarmterm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 90)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(290, 450)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "보건검체 Rack 정보"
        '
        'txtMaxRow
        '
        Me.txtMaxRow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxRow.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtMaxRow.Location = New System.Drawing.Point(92, 69)
        Me.txtMaxRow.MaxLength = 2
        Me.txtMaxRow.Name = "txtMaxRow"
        Me.txtMaxRow.Size = New System.Drawing.Size(72, 21)
        Me.txtMaxRow.TabIndex = 13
        Me.txtMaxRow.Tag = "MAXROW"
        '
        'lblMaxRow
        '
        Me.lblMaxRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblMaxRow.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMaxRow.ForeColor = System.Drawing.Color.White
        Me.lblMaxRow.Location = New System.Drawing.Point(6, 69)
        Me.lblMaxRow.Name = "lblMaxRow"
        Me.lblMaxRow.Size = New System.Drawing.Size(85, 20)
        Me.lblMaxRow.TabIndex = 12
        Me.lblMaxRow.Text = " Max Row"
        Me.lblMaxRow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMaxCol
        '
        Me.txtMaxCol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxCol.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtMaxCol.Location = New System.Drawing.Point(92, 48)
        Me.txtMaxCol.MaxLength = 2
        Me.txtMaxCol.Name = "txtMaxCol"
        Me.txtMaxCol.Size = New System.Drawing.Size(72, 21)
        Me.txtMaxCol.TabIndex = 11
        Me.txtMaxCol.Tag = "MAXCOL"
        '
        'lblMaxCol
        '
        Me.lblMaxCol.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblMaxCol.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMaxCol.ForeColor = System.Drawing.Color.White
        Me.lblMaxCol.Location = New System.Drawing.Point(6, 48)
        Me.lblMaxCol.Name = "lblMaxCol"
        Me.lblMaxCol.Size = New System.Drawing.Size(85, 20)
        Me.lblMaxCol.TabIndex = 10
        Me.lblMaxCol.Text = " Max Col"
        Me.lblMaxCol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAlarmterm
        '
        Me.lblAlarmterm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblAlarmterm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAlarmterm.ForeColor = System.Drawing.Color.White
        Me.lblAlarmterm.Location = New System.Drawing.Point(6, 26)
        Me.lblAlarmterm.Name = "lblAlarmterm"
        Me.lblAlarmterm.Size = New System.Drawing.Size(85, 21)
        Me.lblAlarmterm.TabIndex = 8
        Me.lblAlarmterm.Text = " 경고기간"
        Me.lblAlarmterm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAlarmterm
        '
        Me.txtAlarmterm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlarmterm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAlarmterm.Location = New System.Drawing.Point(92, 26)
        Me.txtAlarmterm.MaxLength = 10
        Me.txtAlarmterm.Name = "txtAlarmterm"
        Me.txtAlarmterm.Size = New System.Drawing.Size(72, 21)
        Me.txtAlarmterm.TabIndex = 9
        Me.txtAlarmterm.Tag = "ALARMTERM"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.cboBcclsNmD)
        Me.grpCd.Controls.Add(Me.txtBcclsCd)
        Me.grpCd.Controls.Add(Me.lblBccls)
        Me.grpCd.Controls.Add(Me.lblRackId)
        Me.grpCd.Controls.Add(Me.txtRackId)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 80)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        Me.grpCd.Text = " 보관검체 Rack ID"
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(681, 17)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 6
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'cboBcclsNmD
        '
        Me.cboBcclsNmD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBcclsNmD.Location = New System.Drawing.Point(123, 21)
        Me.cboBcclsNmD.MaxDropDownItems = 10
        Me.cboBcclsNmD.Name = "cboBcclsNmD"
        Me.cboBcclsNmD.Size = New System.Drawing.Size(212, 20)
        Me.cboBcclsNmD.TabIndex = 2
        Me.cboBcclsNmD.TabStop = False
        Me.cboBcclsNmD.Tag = "BCCLSNMD_01"
        '
        'txtBcclsCd
        '
        Me.txtBcclsCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcclsCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBcclsCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtBcclsCd.Location = New System.Drawing.Point(92, 20)
        Me.txtBcclsCd.MaxLength = 2
        Me.txtBcclsCd.Name = "txtBcclsCd"
        Me.txtBcclsCd.Size = New System.Drawing.Size(30, 21)
        Me.txtBcclsCd.TabIndex = 1
        Me.txtBcclsCd.Tag = "BCCLSCD"
        '
        'lblBccls
        '
        Me.lblBccls.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBccls.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBccls.ForeColor = System.Drawing.Color.White
        Me.lblBccls.Location = New System.Drawing.Point(8, 20)
        Me.lblBccls.Name = "lblBccls"
        Me.lblBccls.Size = New System.Drawing.Size(83, 21)
        Me.lblBccls.TabIndex = 0
        Me.lblBccls.Text = " 검체분류"
        Me.lblBccls.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRackId
        '
        Me.lblRackId.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRackId.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRackId.ForeColor = System.Drawing.Color.White
        Me.lblRackId.Location = New System.Drawing.Point(8, 42)
        Me.lblRackId.Name = "lblRackId"
        Me.lblRackId.Size = New System.Drawing.Size(83, 21)
        Me.lblRackId.TabIndex = 6
        Me.lblRackId.Text = " Rack Id"
        Me.lblRackId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRackId
        '
        Me.txtRackId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRackId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRackId.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtRackId.Location = New System.Drawing.Point(92, 42)
        Me.txtRackId.MaxLength = 6
        Me.txtRackId.Name = "txtRackId"
        Me.txtRackId.Size = New System.Drawing.Size(71, 21)
        Me.txtRackId.TabIndex = 7
        Me.txtRackId.Tag = "RACKID"
        '
        'FDF41
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF41"
        Me.Text = "[41] 보관검체 Rack 설정"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tbcKSRack.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo2.ResumeLayout(False)
        CType(Me.spdSpc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub txtBcclsCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcclsCd.KeyDown, txtAlarmterm.KeyDown, txtRackId.KeyDown, txtMaxCol.KeyDown, txtMaxRow.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub txtTSectCd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBcclsCd.Validating
        Dim sFn As String = "Private Sub txtTSectCd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) txtSpcCd.Validating, txtTSectCd.Validating"

        If miSelectKey = 1 Then Return

        Try
            Dim ctrl As Windows.Forms.TextBox
            Dim cbo As Windows.Forms.ComboBox
            Dim iCurIndex As Integer = -1

            ctrl = CType(sender, Windows.Forms.TextBox)

            If ctrl.Text = "" Then Return

            cbo = CType(ctrl.Parent.GetNextControl(ctrl, True), Windows.Forms.ComboBox)

            For i As Integer = 0 To cbo.Items.Count - 1
                If cbo.Items.Item(i).ToString.StartsWith("[" + ctrl.Text + "]") = True Then
                    iCurIndex = i

                    Exit For
                End If
            Next

            miSelectKey = 1
            cbo.SelectedIndex = iCurIndex

            If iCurIndex = -1 Then
                errpd.SetIconAlignment(ctrl, Windows.Forms.ErrorIconAlignment.TopRight)
                errpd.SetError(ctrl, "존재하지 않는 코드입니다. 확인하여 주십시요!!")
                errpd.SetError(CType(Me.Owner, FGF01).btnReg, "존재하지 않는 코드입니다. 확인하여 주십시요!!")
                e.Cancel = True
            Else
                errpd.SetError(ctrl, "")
                errpd.SetError(CType(Me.Owner, FGF01).btnReg, "")
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub cboTSectNmD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBcclsNmD.SelectedIndexChanged

        Dim sFn As String = "Private Sub cboTSectNmD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSpcNmD.SelectedIndexChanged, cboTSectNmD.SelectedIndexChanged"

        If miSelectKey = 1 Then Return

        Try
            Dim ctrl As Windows.Forms.Control
            Dim cbo As Windows.Forms.ComboBox

            cbo = CType(sender, Windows.Forms.ComboBox)

            ctrl = cbo.Parent.GetNextControl(cbo, False)

            miSelectKey = 1
            If cbo.SelectedIndex > -1 Then
                ctrl.Text = cbo.SelectedItem.ToString.Substring(1, cbo.SelectedItem.ToString.IndexOf("]") - 1)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub FDF41_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub spdSpc_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdSpc.ButtonClicked
        Dim sFn As String = ""

        If miSelectKey = 1 Then Exit Sub
        If e.row < 1 Then Exit Sub
        If Not e.col = 1 Then Exit Sub

        Try
            With spdSpc
                .ReDraw = False

                .Col = 1 : .Row = e.row : Dim sChk As String = .Text

                If sChk = "1" Then
                    .Col = 1 : .Col2 = .MaxCols : .Row = e.row : .Row2 = e.row
                    .BlockMode = True
                    .BackColor = System.Drawing.Color.LavenderBlush
                    .BlockMode = False

                    If e.row = 1 Then
                        For intRow As Integer = 2 To .MaxRows
                            .Row = intRow
                            .Col = 1
                            If .Text = "1" Then .Text = ""
                        Next
                    Else
                        .Row = 1
                        .Col = 1
                        If .Text = "1" Then .Text = ""
                    End If
                Else
                    .Col = 1 : .Col2 = .MaxCols : .Row = e.row : .Row2 = e.row
                    .BlockMode = True
                    .BackColor = System.Drawing.Color.White
                    .BlockMode = False
                End If
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            spdSpc.ReDraw = True
            miSelectKey = 0
        End Try
    End Sub

    Private Sub spdSpc_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdSpc.ClickEvent
        Dim sFn As String = ""

        If miSelectKey = 1 Then Exit Sub

        Try
            With spdSpc
                If e.row < 1 Then Exit Sub
                If e.col < 2 Then Exit Sub

                .ReDraw = False

                .Col = .GetColFromID("CHK") : .Row = e.row : Dim sChk As String = .Text

                If sChk = "1" Then
                    miSelectKey = 1
                    .Col = .GetColFromID("CHK") : .Row = e.row : .Text = "0"

                    .Col = 1 : .Col2 = .MaxCols : .Row = e.row : .Row2 = e.row
                    .BlockMode = True
                    .BackColor = System.Drawing.Color.White
                    .BlockMode = False
                Else
                    miSelectKey = 1
                    .Col = .GetColFromID("CHK") : .Row = e.row : .Text = "1"

                    .Col = 1 : .Col2 = .MaxCols : .Row = e.row : .Row2 = e.row
                    .BlockMode = True
                    .BackColor = System.Drawing.Color.LavenderBlush
                    .BlockMode = False
                End If
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            spdSpc.ReDraw = True
            miSelectKey = 0
        End Try
    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click"

        If Me.txtBcclsCd.Text = "" Or Me.txtRackId.Text = "" Then Return

        Try

            Dim sMsg As String = "   검사파트 : " + Me.cboBcclsNmD.Text + vbCrLf
            sMsg += "   Rack Id : " & Me.txtRackId.Text + vbCrLf
            sMsg += "   의 보관검체 Rack 설정을 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransKSRackInfo_UE(Me.txtBcclsCd.Text, Me.txtRackId.Text, USER_INFO.USRID) Then
                MsgBox("해당 보관검체 Rack 설정 정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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
End Class
