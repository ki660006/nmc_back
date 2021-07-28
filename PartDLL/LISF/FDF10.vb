'>>> [10] 결과코드
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF10
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF10.vb, Class : FDF10" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_RSTCD

    Private msSpcCd As String = "".PadLeft(PRG_CONST.Len_SpcCd, "0"c)

    Public gsModDT As String = ""
    Friend WithEvents btnCdHelp_test As System.Windows.Forms.Button
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents lblCRTWarning As System.Windows.Forms.Label
    Public gsModID As String = ""

    Private Function fnCollectItemTable_64(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_64(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it64 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRstCd

            With it64
                For i As Integer = 1 To spd.MaxRows
                    .SetItemTable("testcd", 1, i, Me.txtTestCd.Text)
                    .SetItemTable("spccd", 2, i, msSpcCd)
                    .SetItemTable("rstcdseq", 3, i, Ctrl.Get_Code(spd, "rstcdseq", i))
                    .SetItemTable("regdt", 4, i, rsRegDT)
                    .SetItemTable("regid", 5, i, USER_INFO.USRID)
                    .SetItemTable("keypad", 6, i, Ctrl.Get_Code(spd, "keypad", i))
                    .SetItemTable("rstcont", 7, i, Ctrl.Get_Code(spd, "rstcont", i))
                    .SetItemTable("grade", 8, i, Ctrl.Get_Code(spd, "grade", i))
                    .SetItemTable("rstlvl", 9, i, Ctrl.Get_Code(spd, "rstlvl", i))
                    .SetItemTable("regip", 10, i, USER_INFO.LOCALIP)
                    .SetItemTable("crtval", 11, i, Ctrl.Get_Code(spd, "crtval", i))
                Next
            End With

            fnCollectItemTable_64 = it64
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it64 As New LISAPP.ItemTableCollection
            Dim iRegType64 As Integer = 0
            Dim sRegDT As String

            iRegType64 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it64 = fnCollectItemTable_64(sRegDT)

            If mobjDAF.TransRstCdInfo(it64, iRegType64, Me.txtTestCd.Text, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rstTestCd As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentRstCdInfo(rstTestCd)

            If dt.Rows.Count > 0 Then
                Return "검사코드 " + dt.Rows(0).Item(0).ToString + "에는 이미 결과코드 내용이 존재합니다." + vbCrLf + vbCrLf + _
                       "코드를 재조정 하십시요!!"
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

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            '20210713 jhs 수정 시에 확인 한번 더 
            'AFBC NTM 검사 일때 수정 시 확인 팝업창 추가 
            If PRG_CONST.AFBC_test(Me.txtTestCd.Text) <> "" Or PRG_CONST.AFBC_NTM_test(Me.txtTestCd.Text) <> "" Then
                If MsgBox(Me.lblCRTWarning.Text, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                Else
                    Exit Function
                End If
            End If
            '----------------------------

            If Len(Me.txtTestCd.Text.Trim) < 1 Or Len(Me.txtTNmD.Text.Trim) < 1 Then
                MsgBox("검사코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtTestCd.Text)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " + errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            'Validate spd
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRstCd

            With spd
                'delete empty row
                For i As Integer = .MaxRows To 1 Step -1
                    Dim sSeq As String = Ctrl.Get_Code(spd, "rstcdseq", i)
                    Dim sKeyPad As String = Ctrl.Get_Code(spd, "keypad", i)
                    Dim sCont As String = Ctrl.Get_Code(spd, "rstcont", i)

                    If Len(sSeq) * Len(sKeyPad) * Len(sCont) = 0 Then
                        .DeleteRows(i, 1)
                        .MaxRows -= 1
                    End If
                Next

                'find duplicated rstcdseq
                Dim al As New ArrayList

                Dim iFind As Integer = 0

                For i As Integer = 1 To .MaxRows
                    If al.Contains(Ctrl.Get_Code(spd, "rstcdseq", i)) Then
                        iFind = i
                        Exit For
                    Else
                        al.Add(Ctrl.Get_Code(spd, "rstcdseq", i))
                    End If
                Next

                If iFind > 0 Then
                    MsgBox("중복된 SEQ가 존재합니다. 확인하여 주십시요!!", MsgBoxStyle.Critical)
                    .SetActiveCell(.GetColFromID("rstcdseq"), iFind)
                    Exit Function
                End If
            End With

            fnValidate = True

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rstTestCd As String, ByVal rimode As Integer)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_RstCd(rstTestCd, rimode)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_RstCd(ByVal rstTestCd As String, ByVal rimode As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_RstCd(ByVal asTClsCd As String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As New DataTable
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then

                dt = mobjDAF.GetRstCdInfo(rstTestCd, rimode)
            Else
                dt = mobjDAF.GetRstCdInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rstTestCd)
            End If


            '초기화
            sbInitialize()

            If dt.Rows.Count < 1 Then Return

            With dt
                Me.txtTestCd.Text = .Rows(0).Item("testcd").ToString()
                Me.txtTNmD.Text = .Rows(0).Item("tnmd").ToString()
                Me.txtRegNm.Text = .Rows(0).Item("regnm").ToString()
                Me.txtModNm.Text = .Rows(0).Item("modnm").ToString()
            End With

            Ctrl.DisplayAfterSelect(Me.spdRstCd, dt, "L")

            Dim a_dr As DataRow() = dt.Select("", "regdt desc")

            With a_dr(0)
                Me.txtRegDT.Text = .Item("regdt").ToString()
                Me.txtRegID.Text = .Item("regid").ToString()
            End With

            Me.txtModDT.Text = gsModDT
            Me.txtModID.Text = gsModID

            '20210712 jhs AFB,NTM 검사 한번더 확인하고 수정하기 위해 경고 레이블 설정
            If PRG_CONST.AFBC_test(Me.txtTestCd.Text) <> "" Then
                Me.lblCRTWarning.Visible = True
                Me.lblCRTWarning.Text = "지난 5년간 NTM, MTB 결과로 보고된 경우 최초 1회만 critical value report 표시되는 코드입니다."
                Me.lblCRTWarning.ForeColor = Drawing.Color.DarkRed
            ElseIf PRG_CONST.AFBC_NTM_test(Me.txtTestCd.Text) <> "" Then
                Me.lblCRTWarning.Visible = True
                Me.lblCRTWarning.Text = "지난 5년간 NTM, MTB 결과로 보고된 경우 최초 1회만 critical value report 표시되는 코드입니다."
                Me.lblCRTWarning.ForeColor = Drawing.Color.DarkRed
            Else
                Me.lblCRTWarning.Visible = False
                Me.lblCRTWarning.Text = ""
                Me.lblCRTWarning.ForeColor = Drawing.Color.Black
            End If
            '-------------------------------------------------

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
                Me.txtTestCd.Text = "" : Me.txtTNmD.Text = "" : Me.btnUE.Visible = False
                Me.spdRstCd.MaxRows = 0
                Me.chkML.Checked = False : Me.txtRstCont.Enabled = False : Me.txtRstCont.Text = "" : Me.btnML.Enabled = False
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtRegNm.Text = ""
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
    Friend WithEvents tbcTpg1 As System.Windows.Forms.TabPage
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents spdRstCd As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtRstCont As System.Windows.Forms.TextBox
    Friend WithEvents btnML As System.Windows.Forms.Button
    Friend WithEvents chkML As System.Windows.Forms.CheckBox
    Friend WithEvents txtTNmD As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF10))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tclSpc = New System.Windows.Forms.TabControl()
        Me.tbcTpg1 = New System.Windows.Forms.TabPage()
        Me.txtModNm = New System.Windows.Forms.TextBox()
        Me.txtModID = New System.Windows.Forms.TextBox()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.lblModNm = New System.Windows.Forms.Label()
        Me.txtModDT = New System.Windows.Forms.TextBox()
        Me.lblModDT = New System.Windows.Forms.Label()
        Me.txtRegDT = New System.Windows.Forms.TextBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblRegDT = New System.Windows.Forms.Label()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox()
        Me.lblCRTWarning = New System.Windows.Forms.Label()
        Me.btnML = New System.Windows.Forms.Button()
        Me.txtRstCont = New System.Windows.Forms.TextBox()
        Me.chkML = New System.Windows.Forms.CheckBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.spdRstCd = New AxFPSpreadADO.AxfpSpread()
        Me.grpCd = New System.Windows.Forms.GroupBox()
        Me.btnCdHelp_test = New System.Windows.Forms.Button()
        Me.txtTNmD = New System.Windows.Forms.TextBox()
        Me.txtTestCd = New System.Windows.Forms.TextBox()
        Me.lblTestCd = New System.Windows.Forms.Label()
        Me.btnUE = New System.Windows.Forms.Button()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg1.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.spdRstCd, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTop.Size = New System.Drawing.Size(795, 715)
        Me.pnlTop.TabIndex = 118
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tbcTpg1)
        Me.tclSpc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tclSpc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tclSpc.ItemSize = New System.Drawing.Size(84, 17)
        Me.tclSpc.Location = New System.Drawing.Point(0, 0)
        Me.tclSpc.Name = "tclSpc"
        Me.tclSpc.SelectedIndex = 0
        Me.tclSpc.Size = New System.Drawing.Size(791, 711)
        Me.tclSpc.TabIndex = 0
        Me.tclSpc.TabStop = False
        '
        'tbcTpg1
        '
        Me.tbcTpg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg1.Controls.Add(Me.txtModNm)
        Me.tbcTpg1.Controls.Add(Me.txtModID)
        Me.tbcTpg1.Controls.Add(Me.txtRegNm)
        Me.tbcTpg1.Controls.Add(Me.lblModNm)
        Me.tbcTpg1.Controls.Add(Me.txtModDT)
        Me.tbcTpg1.Controls.Add(Me.lblModDT)
        Me.tbcTpg1.Controls.Add(Me.txtRegDT)
        Me.tbcTpg1.Controls.Add(Me.lblUserNm)
        Me.tbcTpg1.Controls.Add(Me.lblRegDT)
        Me.tbcTpg1.Controls.Add(Me.txtRegID)
        Me.tbcTpg1.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg1.Controls.Add(Me.grpCd)
        Me.tbcTpg1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcTpg1.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg1.Name = "tbcTpg1"
        Me.tbcTpg1.Size = New System.Drawing.Size(783, 686)
        Me.tbcTpg1.TabIndex = 0
        Me.tbcTpg1.Text = "결과코드정보"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(300, 651)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 7
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(300, 651)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(68, 21)
        Me.txtModID.TabIndex = 6
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        Me.txtModID.Visible = False
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(700, 651)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 8
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(215, 651)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 5
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(95, 651)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 4
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(10, 651)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(84, 21)
        Me.lblModDT.TabIndex = 3
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(500, 651)
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
        Me.lblUserNm.Location = New System.Drawing.Point(615, 651)
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
        Me.lblRegDT.Location = New System.Drawing.Point(415, 651)
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
        Me.txtRegID.Location = New System.Drawing.Point(700, 651)
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
        Me.grpCdInfo1.Controls.Add(Me.lblCRTWarning)
        Me.grpCdInfo1.Controls.Add(Me.btnML)
        Me.grpCdInfo1.Controls.Add(Me.txtRstCont)
        Me.grpCdInfo1.Controls.Add(Me.chkML)
        Me.grpCdInfo1.Controls.Add(Me.btnAdd)
        Me.grpCdInfo1.Controls.Add(Me.btnDel)
        Me.grpCdInfo1.Controls.Add(Me.spdRstCd)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 52)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 593)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "결과코드정보"
        '
        'lblCRTWarning
        '
        Me.lblCRTWarning.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCRTWarning.AutoSize = True
        Me.lblCRTWarning.Location = New System.Drawing.Point(8, 489)
        Me.lblCRTWarning.Name = "lblCRTWarning"
        Me.lblCRTWarning.Size = New System.Drawing.Size(179, 12)
        Me.lblCRTWarning.TabIndex = 11
        Me.lblCRTWarning.Text = "ntm, afbstain 검사 일 때 사용"
        '
        'btnML
        '
        Me.btnML.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnML.Location = New System.Drawing.Point(692, 448)
        Me.btnML.Name = "btnML"
        Me.btnML.Size = New System.Drawing.Size(64, 32)
        Me.btnML.TabIndex = 10
        Me.btnML.Text = "적용(+A)"
        '
        'txtRstCont
        '
        Me.txtRstCont.Location = New System.Drawing.Point(114, 448)
        Me.txtRstCont.MaxLength = 200
        Me.txtRstCont.Multiline = True
        Me.txtRstCont.Name = "txtRstCont"
        Me.txtRstCont.Size = New System.Drawing.Size(576, 32)
        Me.txtRstCont.TabIndex = 9
        '
        'chkML
        '
        Me.chkML.Location = New System.Drawing.Point(8, 448)
        Me.chkML.Name = "chkML"
        Me.chkML.Size = New System.Drawing.Size(104, 18)
        Me.chkML.TabIndex = 8
        Me.chkML.Text = "다중라인 사용"
        '
        'btnAdd
        '
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(676, 14)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(40, 20)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.TabStop = False
        Me.btnAdd.Text = "＋"
        '
        'btnDel
        '
        Me.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDel.Location = New System.Drawing.Point(716, 14)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(40, 20)
        Me.btnDel.TabIndex = 6
        Me.btnDel.TabStop = False
        Me.btnDel.Text = "－"
        '
        'spdRstCd
        '
        Me.spdRstCd.DataSource = Nothing
        Me.spdRstCd.Location = New System.Drawing.Point(8, 36)
        Me.spdRstCd.Name = "spdRstCd"
        Me.spdRstCd.OcxState = CType(resources.GetObject("spdRstCd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRstCd.Size = New System.Drawing.Size(748, 408)
        Me.spdRstCd.TabIndex = 7
        '
        'grpCd
        '
        Me.grpCd.Controls.Add(Me.btnCdHelp_test)
        Me.grpCd.Controls.Add(Me.txtTNmD)
        Me.grpCd.Controls.Add(Me.txtTestCd)
        Me.grpCd.Controls.Add(Me.lblTestCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        '
        'btnCdHelp_test
        '
        Me.btnCdHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_test.Image = CType(resources.GetObject("btnCdHelp_test.Image"), System.Drawing.Image)
        Me.btnCdHelp_test.Location = New System.Drawing.Point(160, 15)
        Me.btnCdHelp_test.Name = "btnCdHelp_test"
        Me.btnCdHelp_test.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_test.TabIndex = 1
        Me.btnCdHelp_test.TabStop = False
        Me.btnCdHelp_test.UseVisualStyleBackColor = True
        '
        'txtTNmD
        '
        Me.txtTNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTNmD.Location = New System.Drawing.Point(187, 15)
        Me.txtTNmD.Name = "txtTNmD"
        Me.txtTNmD.ReadOnly = True
        Me.txtTNmD.Size = New System.Drawing.Size(484, 21)
        Me.txtTNmD.TabIndex = 3
        Me.txtTNmD.TabStop = False
        Me.txtTNmD.Tag = "TNMD"
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestCd.Location = New System.Drawing.Point(87, 15)
        Me.txtTestCd.MaxLength = 7
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(72, 21)
        Me.txtTestCd.TabIndex = 0
        Me.txtTestCd.Tag = "TESTCD"
        '
        'lblTestCd
        '
        Me.lblTestCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(8, 15)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(78, 21)
        Me.lblTestCd.TabIndex = 7
        Me.lblTestCd.Text = "검사코드"
        Me.lblTestCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(685, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 4
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'FDF10
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(795, 715)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF10"
        Me.Text = "[10] 결과코드"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg1.ResumeLayout(False)
        Me.tbcTpg1.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.spdRstCd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRstCd
        Dim cnt = spd.MaxRows + 1
        Dim row = spd.Row

        With spd
            .MaxRows += 1
            .Col = .GetColFromID("rstcdseq") : .Row = .MaxRows : .Text = cnt.ToString
        End With
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRstCd

        With spd
            .Row = .ActiveRow
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1
        End With
    End Sub

    Private Sub btnML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnML.Click
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRstCd

        If spd.ActiveRow < 1 Then Return

        spd.SetText(spd.GetColFromID("rstcont"), spd.ActiveRow, Me.txtRstCont.Text)
    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Dim sFn As String = "Handles btnCdHelp_test.Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnCdHelp_test) + Me.btnCdHelp_test.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_list("", "", "")
            Dim sSql As String = "((tcdgbn IN ('B', 'P') AND titleyn = '0') OR tcdgbn IN ('S', 'C'))" + IIf(Me.txtTestCd.Text = "", "", " AND testcd = '" + Me.txtTestCd.Text + "'").ToString
            Dim a_dr As DataRow() = dt.Select(sSql, "")

            dt = Fn.ChangeToDataTable(a_dr)

            objHelp.FormText = "검사정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "구분", 0, , , True)

            aryList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If aryList.Count > 0 Then
                miSelectKey = 1

                Me.txtTestCd.Text = aryList.Item(0).ToString.Split("|"c)(0)
                Me.txtTNmD.Text = aryList.Item(0).ToString.Split("|"c)(1)

                miSelectKey = 0
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
            miSelectKey = 0

        End Try
    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        If Me.txtTestCd.Text = "" Then Exit Sub

        Try
            Dim sMsg As String = "검사코드 : " + Me.txtTestCd.Text + vbCrLf
            sMsg += "검사명  : " + Me.txtTNmD.Text + vbCrLf + vbCrLf
            sMsg += "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransRstCdInfo_UE(Me.txtTestCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 결과코드정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub chkML_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkML.CheckedChanged
        If miSelectKey = 1 Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRstCd

        If spd.ActiveRow < 1 Then Return

        If Me.chkML.Checked Then
            Me.txtRstCont.Text = Ctrl.Get_Code(spd, "RSTCONT", spd.ActiveRow)
            Me.txtRstCont.Enabled = True
            Me.btnML.Enabled = True
        Else
            Me.txtRstCont.Text = ""
            Me.txtRstCont.Enabled = False
            Me.btnML.Enabled = False
        End If
    End Sub

    Private Sub spdRstCd_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdRstCd.LeaveCell
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRstCd

        If e.row < 1 Then Return
        If e.newRow < 1 Then Return

        If Me.chkML.Checked Then
            Me.txtRstCont.Text = Ctrl.Get_Code(spd, "RSTCONT", e.newRow)
        End If
    End Sub

    Private Sub txtTestCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub txtTestCd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTestCd.Validating
        If miSelectKey = 1 Then Return

        btnCdHelp_test_Click(Nothing, Nothing)

    End Sub

    Private Sub FDF10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub
End Class
