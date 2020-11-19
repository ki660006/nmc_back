'>>> [12] 계산식
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF12
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF12.vb, Class : FDF12" + vbTab

    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_CALC

    Private miMaxParam As Integer = 10

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Public gsModDT As String = ""
    Friend WithEvents pnlCalView As System.Windows.Forms.Panel
    Friend WithEvents rdoCalViewA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCalViewM As System.Windows.Forms.RadioButton
    Friend WithEvents lblCalView As System.Windows.Forms.Label
    Friend WithEvents lblDayInfo As System.Windows.Forms.Label
    Friend WithEvents cboCalDays As System.Windows.Forms.ComboBox
    Friend WithEvents lblCalDays As System.Windows.Forms.Label
    Friend WithEvents btnSelSpc As System.Windows.Forms.Button
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public gsModID As String = ""

    Private Function fnCollectItemTable_66(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_66(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it66 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCalTest

            With it66
                .SetItemTable("TESTCD", 1, 1, Me.txtTestCd.Text.Trim)
                .SetItemTable("SPCCD", 2, 1, Me.txtSpcCd.Text.Trim)
                .SetItemTable("CALRANGE", 3, 1, IIf(Me.rdoCalBcNo.Checked, "B", "R").ToString)
                .SetItemTable("REGDT", 4, 1, rsRegDT)
                .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                .SetItemTable("PARAMCNT", 6, 1, spd.Tag.ToString)

                For i As Integer = 1 To spd.MaxRows
                    Dim sTClsCd As String = ""
                    Dim sSpcCd As String = ""

                    If i > Val(spd.Tag) Then
                        .SetItemTable("PARAM" + (i - 1).ToString, 6 + i, 1, "")
                    Else
                        sTClsCd = Ctrl.Get_Code(spd, "TESTCD", i)
                        sSpcCd = Ctrl.Get_Code(spd, "SPCCD", i)

                        .SetItemTable("PARAM" + (i - 1).ToString, 6 + i, 1, sTClsCd.PadRight(7) + sSpcCd.PadRight(PRG_CONST.Len_SpcCd))
                    End If
                Next
                .SetItemTable("REGIP", 6 + spd.MaxRows + 1, 1, USER_INFO.LOCALIP)
                .SetItemTable("CALFORM", 6 + spd.MaxRows + 2, 1, Me.txtCalForm.Text.Trim)
                .SetItemTable("CALTYPE", 6 + spd.MaxRows + 3, 1, IIf(Me.rdoCalTypeA.Checked, "A", "M").ToString)
                .SetItemTable("CALVIEW", 6 + spd.MaxRows + 4, 1, IIf(Me.rdoCalViewA.Checked, "A", "M").ToString)
                .SetItemTable("CALDAYS", 6 + spd.MaxRows + 5, 1, cboCalDays.Text)

            End With

            fnCollectItemTable_66 = it66

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return Nothing

        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            '< 계산식만 추가된 내용
            If Me.btnVerify.Tag Is Nothing Then
                MsgBox("계산식이 검증되지 않았습니다!!", MsgBoxStyle.Exclamation)

                Return False
            End If

            If Val(Me.btnVerify.Tag) < 1 Then
                MsgBox("계산식이 검증되지 않았습니다!!", MsgBoxStyle.Exclamation)

                Return False
            End If
            '>

            Dim it66 As New LISAPP.ItemTableCollection
            Dim iRegType66 As Integer = 0
            Dim sRegDT As String

            iRegType66 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it66 = fnCollectItemTable_66(sRegDT)

            If mobjDAF.TransCalcInfo(it66, iRegType66, Me.txtTestCd.Text, Me.txtSpcCd.Text, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal asTClsCd As String, ByVal asSpcCd As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentCalcInfo(asTClsCd, asSpcCd)

            If dt.Rows.Count > 0 Then
                Return "검사코드 " + dt.Rows(0).Item(0).ToString + "에는 이미 계산식 내용이 존재합니다." + vbCrLf + vbCrLf + _
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
            If Len(Me.txtTestCd.Text.Trim) < 1 Or Len(Me.txtTNmD.Text.Trim) < 1 Then
                MsgBox("검사코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtSpcCd.Text.Trim) < 1 Or Len(Me.txtSpcNmD.Text.Trim) < 1 Then
                MsgBox("검체코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtTestCd.Text, Me.txtSpcCd.Text)

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

            'Validate spd
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCalTest

            Dim iFind As Integer = 0

            With spd
                'clear empty row
                For i As Integer = 1 To .MaxRows
                    Dim sTClsCd As String = Ctrl.Get_Code(spd, "TESTCD", i).Trim
                    Dim sSpcCd As String = Ctrl.Get_Code(spd, "SPCCD", i).Trim
                    Dim sTNmD As String = Ctrl.Get_Code(spd, "TNMD", i).Trim
                    Dim sSpcNmD As String = Ctrl.Get_Code(spd, "SPCNMD", i).Trim

                    If sTClsCd.Length * sSpcCd.Length * sTNmD.Length * sSpcNmD.Length = 0 Then
                        iFind = i

                        Exit For
                    End If
                Next

                'ParamCnt 저장
                If iFind = 0 Then
                    Me.spdCalTest.Tag = Me.spdCalTest.MaxRows
                Else
                    Me.spdCalTest.Tag = iFind - 1
                End If

                If Val(Me.spdCalTest.Tag) < 1 Then
                    MsgBox("관련검사 설정을 확인하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If

                .ClearRange(.GetColFromID("TESTCD"), iFind, .GetColFromID("SPCNMD"), .MaxRows, True)
            End With

            fnValidate = True

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsTestCd As String, ByVal rsSpcCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_Calc(rsTestCd, rsSpcCd)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Calc(ByVal asTClsCd As String, ByVal asSpcCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Calc(String, String)"
        Dim iCol As Integer = 0

        Try
            Dim DTable As DataTable
            Dim cctrl As System.Windows.Forms.Control = Nothing
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                DTable = mobjDAF.GetCalcInfo(asTClsCd, asSpcCd)
            Else
                DTable = mobjDAF.GetCalcInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, asTClsCd, asSpcCd)
            End If

            '초기화
            sbInitialize()

            If DTable.Rows.Count < 1 Then Return

            miSelectKey = 1

            With DTable
                Me.txtTestCd.Text = .Rows(0).Item("testcd").ToString()
                Me.txtSpcCd.Text = .Rows(0).Item("spccd").ToString()
                Me.txtTNmD.Text = .Rows(0).Item("tnmd").ToString()
                Me.txtSpcNmD.Text = .Rows(0).Item("spcnmd").ToString()

                Me.rdoCalBcNo.Checked = CType(IIf(.Rows(0).Item("calrange").ToString = "B", True, False), Boolean)
                Me.rdoCalRegNo.Checked = CType(IIf(.Rows(0).Item("calrange").ToString = "R", True, False), Boolean)

                Me.rdoCalTypeM.Checked = CType(IIf(.Rows(0).Item("caltype").ToString = "M", True, False), Boolean)
                Me.rdoCalTypeA.Checked = CType(IIf(.Rows(0).Item("caltype").ToString = "A", True, False), Boolean)

                Me.txtCalForm.Text = .Rows(0).Item("calform").ToString()

                Me.rdoCalViewM.Checked = CType(IIf(.Rows(0).Item("calview").ToString = "M", True, False), Boolean)
                Me.rdoCalViewA.Checked = CType(IIf(.Rows(0).Item("calview").ToString = "A", True, False), Boolean)

                Me.cboCalDays.Text = .Rows(0).Item("caldays").ToString

                Me.txtRegDT.Text = .Rows(0).Item("regdt").ToString()
                Me.txtRegID.Text = .Rows(0).Item("regid").ToString()
                Me.txtModNm.Text = .Rows(0).Item("modnm").ToString()
                Me.txtRegNm.Text = .Rows(0).Item("regnm").ToString()
            End With

            With Me.spdCalTest
                .Tag = CType(DTable.Rows(0).Item("paramcnt"), Integer)

                For i As Integer = 1 To .MaxRows
                    If i > Val(.Tag) Then
                        Exit For
                    End If

                    Dim sParam As String = DTable.Rows(0).Item("param" + (i - 1).ToString).ToString()

                    .SetText(.GetColFromID("TESTCD"), i, sParam.Substring(0, 7).Trim)
                    .SetText(.GetColFromID("SPCCD"), i, sParam.Substring(7).Trim)

                    Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_list("", "", sParam.Substring(0, 7).Trim, sParam.Substring(7).Trim)

                    If dt.Rows.Count > 0 Then
                        .SetText(.GetColFromID("TNMD"), i, dt.Rows(0).Item("tnmd"))
                        .SetText(.GetColFromID("SPCNMD"), i, dt.Rows(0).Item("spcnmd"))
                    End If
                Next
            End With

            Me.txtModDT.Text = gsModDT
            Me.txtModID.Text = gsModID

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then      '권한이 있어야 "사용종료"를 할 수 있음
                Me.btnUE.Enabled = True
            Else
                Me.btnUE.Enabled = False
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
                Me.txtTestCd.Text = "" : Me.txtSpcCd.Text = "" : Me.txtTNmD.Text = "" : Me.txtSpcNmD.Text = "" : Me.btnUE.Visible = False

                Me.rdoCalBcNo.Checked = True
                Me.rdoCalTypeM.Checked = True

                With Me.spdCalTest
                    .ClearRange(.GetColFromID("TESTCD"), 1, .GetColFromID("SPCNMD"), .MaxRows, True)
                    .Tag = 0
                End With

                Me.txtCalForm.Text = "" : Me.txtRegNm.Text = ""
                Me.cboCalDays.Text = ""

                Me.btnVerify.Enabled = False : Me.btnVerify.Tag = 0
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = ""

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
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
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
    Friend WithEvents txtTNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents lblCalR As System.Windows.Forms.Label
    Friend WithEvents rdoCalBcNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCalRegNo As System.Windows.Forms.RadioButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents lblCal As System.Windows.Forms.Label
    Friend WithEvents txtCalForm As System.Windows.Forms.TextBox
    Friend WithEvents btnVerify As System.Windows.Forms.Button
    Friend WithEvents spdCalTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtSpcNmD As System.Windows.Forms.TextBox
    Friend WithEvents lblGuide As System.Windows.Forms.Label
    Friend WithEvents lblGuide2 As System.Windows.Forms.Label
    Friend WithEvents spdCalBuf As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlCalR As System.Windows.Forms.Panel
    Friend WithEvents pnlCalType As System.Windows.Forms.Panel
    Friend WithEvents rdoCalTypeM As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCalTypeA As System.Windows.Forms.RadioButton
    Friend WithEvents lblCalType As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF12))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tpg1 = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.txtModID = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblDayInfo = New System.Windows.Forms.Label
        Me.cboCalDays = New System.Windows.Forms.ComboBox
        Me.lblCalDays = New System.Windows.Forms.Label
        Me.pnlCalView = New System.Windows.Forms.Panel
        Me.rdoCalViewA = New System.Windows.Forms.RadioButton
        Me.rdoCalViewM = New System.Windows.Forms.RadioButton
        Me.lblCalView = New System.Windows.Forms.Label
        Me.pnlCalType = New System.Windows.Forms.Panel
        Me.rdoCalTypeM = New System.Windows.Forms.RadioButton
        Me.rdoCalTypeA = New System.Windows.Forms.RadioButton
        Me.lblCalType = New System.Windows.Forms.Label
        Me.pnlCalR = New System.Windows.Forms.Panel
        Me.rdoCalBcNo = New System.Windows.Forms.RadioButton
        Me.rdoCalRegNo = New System.Windows.Forms.RadioButton
        Me.spdCalBuf = New AxFPSpreadADO.AxfpSpread
        Me.lblGuide2 = New System.Windows.Forms.Label
        Me.lblGuide = New System.Windows.Forms.Label
        Me.lblCal = New System.Windows.Forms.Label
        Me.lblInfo = New System.Windows.Forms.Label
        Me.lblCalR = New System.Windows.Forms.Label
        Me.btnVerify = New System.Windows.Forms.Button
        Me.txtCalForm = New System.Windows.Forms.TextBox
        Me.spdCalTest = New AxFPSpreadADO.AxfpSpread
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnSelSpc = New System.Windows.Forms.Button
        Me.txtSpcNmD = New System.Windows.Forms.TextBox
        Me.txtSpcCd = New System.Windows.Forms.TextBox
        Me.lblSpcCd = New System.Windows.Forms.Label
        Me.txtTNmD = New System.Windows.Forms.TextBox
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.lblTestCd = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tpg1.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        Me.pnlCalView.SuspendLayout()
        Me.pnlCalType.SuspendLayout()
        Me.pnlCalR.SuspendLayout()
        CType(Me.spdCalBuf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdCalTest, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTop.Size = New System.Drawing.Size(795, 607)
        Me.pnlTop.TabIndex = 118
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
        Me.tclSpc.Size = New System.Drawing.Size(791, 603)
        Me.tclSpc.TabIndex = 0
        Me.tclSpc.TabStop = False
        '
        'tpg1
        '
        Me.tpg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpg1.Controls.Add(Me.txtRegNm)
        Me.tpg1.Controls.Add(Me.txtModNm)
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
        Me.tpg1.Size = New System.Drawing.Size(783, 578)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "계산식정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(701, 544)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 146
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(294, 544)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 141
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(294, 544)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(68, 21)
        Me.txtModID.TabIndex = 6
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(209, 544)
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
        Me.txtModDT.Location = New System.Drawing.Point(95, 544)
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
        Me.lblModDT.Location = New System.Drawing.Point(10, 544)
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
        Me.txtRegDT.Location = New System.Drawing.Point(500, 544)
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
        Me.lblUserNm.Location = New System.Drawing.Point(616, 544)
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
        Me.lblRegDT.Location = New System.Drawing.Point(415, 544)
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
        Me.txtRegID.Location = New System.Drawing.Point(701, 544)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.TextBox2)
        Me.grpCdInfo1.Controls.Add(Me.TextBox1)
        Me.grpCdInfo1.Controls.Add(Me.Label4)
        Me.grpCdInfo1.Controls.Add(Me.Label1)
        Me.grpCdInfo1.Controls.Add(Me.TextBox4)
        Me.grpCdInfo1.Controls.Add(Me.TextBox3)
        Me.grpCdInfo1.Controls.Add(Me.Label3)
        Me.grpCdInfo1.Controls.Add(Me.Label2)
        Me.grpCdInfo1.Controls.Add(Me.lblDayInfo)
        Me.grpCdInfo1.Controls.Add(Me.cboCalDays)
        Me.grpCdInfo1.Controls.Add(Me.lblCalDays)
        Me.grpCdInfo1.Controls.Add(Me.pnlCalView)
        Me.grpCdInfo1.Controls.Add(Me.lblCalView)
        Me.grpCdInfo1.Controls.Add(Me.pnlCalType)
        Me.grpCdInfo1.Controls.Add(Me.lblCalType)
        Me.grpCdInfo1.Controls.Add(Me.pnlCalR)
        Me.grpCdInfo1.Controls.Add(Me.spdCalBuf)
        Me.grpCdInfo1.Controls.Add(Me.lblGuide2)
        Me.grpCdInfo1.Controls.Add(Me.lblGuide)
        Me.grpCdInfo1.Controls.Add(Me.lblCal)
        Me.grpCdInfo1.Controls.Add(Me.lblInfo)
        Me.grpCdInfo1.Controls.Add(Me.lblCalR)
        Me.grpCdInfo1.Controls.Add(Me.btnVerify)
        Me.grpCdInfo1.Controls.Add(Me.txtCalForm)
        Me.grpCdInfo1.Controls.Add(Me.spdCalTest)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 52)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 488)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "계산식정보"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TextBox4.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBox4.Location = New System.Drawing.Point(137, 407)
        Me.TextBox4.MaxLength = 200
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(14, 14)
        Me.TextBox4.TabIndex = 158
        Me.TextBox4.Text = "♀"
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TextBox3.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBox3.Location = New System.Drawing.Point(84, 407)
        Me.TextBox3.MaxLength = 200
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(14, 14)
        Me.TextBox3.TabIndex = 157
        Me.TextBox3.Text = "♂"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(82, 406)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 12)
        Me.Label3.TabIndex = 156
        Me.Label3.Text = "♂:남자, ♀:여자, @:나이"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(82, 424)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(149, 12)
        Me.Label2.TabIndex = 149
        Me.Label2.Text = "지수계산: A^2 => ^(A, 2)"
        '
        'lblDayInfo
        '
        Me.lblDayInfo.AutoSize = True
        Me.lblDayInfo.Location = New System.Drawing.Point(715, 20)
        Me.lblDayInfo.Name = "lblDayInfo"
        Me.lblDayInfo.Size = New System.Drawing.Size(17, 12)
        Me.lblDayInfo.TabIndex = 145
        Me.lblDayInfo.Text = "일"
        '
        'cboCalDays
        '
        Me.cboCalDays.FormattingEnabled = True
        Me.cboCalDays.Items.AddRange(New Object() {"1", "30", "60", "90", "365", "9999"})
        Me.cboCalDays.Location = New System.Drawing.Point(631, 15)
        Me.cboCalDays.Margin = New System.Windows.Forms.Padding(0)
        Me.cboCalDays.Name = "cboCalDays"
        Me.cboCalDays.Size = New System.Drawing.Size(81, 20)
        Me.cboCalDays.TabIndex = 144
        Me.cboCalDays.Tag = "CALDAYS"
        '
        'lblCalDays
        '
        Me.lblCalDays.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCalDays.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCalDays.ForeColor = System.Drawing.Color.White
        Me.lblCalDays.Location = New System.Drawing.Point(494, 15)
        Me.lblCalDays.Name = "lblCalDays"
        Me.lblCalDays.Size = New System.Drawing.Size(136, 21)
        Me.lblCalDays.TabIndex = 143
        Me.lblCalDays.Text = "이전결과 적용일 기간"
        Me.lblCalDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCalView
        '
        Me.pnlCalView.Controls.Add(Me.rdoCalViewA)
        Me.pnlCalView.Controls.Add(Me.rdoCalViewM)
        Me.pnlCalView.Location = New System.Drawing.Point(73, 60)
        Me.pnlCalView.Name = "pnlCalView"
        Me.pnlCalView.Size = New System.Drawing.Size(388, 21)
        Me.pnlCalView.TabIndex = 142
        '
        'rdoCalViewA
        '
        Me.rdoCalViewA.Checked = True
        Me.rdoCalViewA.Location = New System.Drawing.Point(21, 1)
        Me.rdoCalViewA.Name = "rdoCalViewA"
        Me.rdoCalViewA.Size = New System.Drawing.Size(159, 19)
        Me.rdoCalViewA.TabIndex = 131
        Me.rdoCalViewA.TabStop = True
        Me.rdoCalViewA.Text = "자동으로 화면보기"
        Me.rdoCalViewA.UseCompatibleTextRendering = True
        '
        'rdoCalViewM
        '
        Me.rdoCalViewM.Location = New System.Drawing.Point(195, 1)
        Me.rdoCalViewM.Name = "rdoCalViewM"
        Me.rdoCalViewM.Size = New System.Drawing.Size(159, 19)
        Me.rdoCalViewM.TabIndex = 132
        Me.rdoCalViewM.Text = "수동으로 화면보기"
        Me.rdoCalViewM.UseCompatibleTextRendering = True
        '
        'lblCalView
        '
        Me.lblCalView.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCalView.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCalView.ForeColor = System.Drawing.Color.White
        Me.lblCalView.Location = New System.Drawing.Point(8, 60)
        Me.lblCalView.Name = "lblCalView"
        Me.lblCalView.Size = New System.Drawing.Size(64, 21)
        Me.lblCalView.TabIndex = 141
        Me.lblCalView.Text = "표시방법"
        Me.lblCalView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCalType
        '
        Me.pnlCalType.Controls.Add(Me.rdoCalTypeM)
        Me.pnlCalType.Controls.Add(Me.rdoCalTypeA)
        Me.pnlCalType.Location = New System.Drawing.Point(73, 38)
        Me.pnlCalType.Name = "pnlCalType"
        Me.pnlCalType.Size = New System.Drawing.Size(388, 21)
        Me.pnlCalType.TabIndex = 140
        '
        'rdoCalTypeM
        '
        Me.rdoCalTypeM.Checked = True
        Me.rdoCalTypeM.Location = New System.Drawing.Point(21, 1)
        Me.rdoCalTypeM.Name = "rdoCalTypeM"
        Me.rdoCalTypeM.Size = New System.Drawing.Size(159, 19)
        Me.rdoCalTypeM.TabIndex = 131
        Me.rdoCalTypeM.TabStop = True
        Me.rdoCalTypeM.Text = "수동 결과 입력 → 계산"
        Me.rdoCalTypeM.UseCompatibleTextRendering = True
        '
        'rdoCalTypeA
        '
        Me.rdoCalTypeA.Location = New System.Drawing.Point(195, 1)
        Me.rdoCalTypeA.Name = "rdoCalTypeA"
        Me.rdoCalTypeA.Size = New System.Drawing.Size(159, 19)
        Me.rdoCalTypeA.TabIndex = 132
        Me.rdoCalTypeA.Text = "자동 결과 전송 → 계산"
        Me.rdoCalTypeA.UseCompatibleTextRendering = True
        '
        'lblCalType
        '
        Me.lblCalType.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCalType.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCalType.ForeColor = System.Drawing.Color.White
        Me.lblCalType.Location = New System.Drawing.Point(8, 38)
        Me.lblCalType.Name = "lblCalType"
        Me.lblCalType.Size = New System.Drawing.Size(64, 21)
        Me.lblCalType.TabIndex = 139
        Me.lblCalType.Text = "계산방식"
        Me.lblCalType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCalR
        '
        Me.pnlCalR.Controls.Add(Me.rdoCalBcNo)
        Me.pnlCalR.Controls.Add(Me.rdoCalRegNo)
        Me.pnlCalR.Location = New System.Drawing.Point(73, 16)
        Me.pnlCalR.Name = "pnlCalR"
        Me.pnlCalR.Size = New System.Drawing.Size(388, 21)
        Me.pnlCalR.TabIndex = 138
        '
        'rdoCalBcNo
        '
        Me.rdoCalBcNo.Checked = True
        Me.rdoCalBcNo.Location = New System.Drawing.Point(21, 1)
        Me.rdoCalBcNo.Name = "rdoCalBcNo"
        Me.rdoCalBcNo.Size = New System.Drawing.Size(136, 19)
        Me.rdoCalBcNo.TabIndex = 131
        Me.rdoCalBcNo.TabStop = True
        Me.rdoCalBcNo.Text = "동일 검체번호 기준"
        '
        'rdoCalRegNo
        '
        Me.rdoCalRegNo.Location = New System.Drawing.Point(195, 1)
        Me.rdoCalRegNo.Name = "rdoCalRegNo"
        Me.rdoCalRegNo.Size = New System.Drawing.Size(136, 19)
        Me.rdoCalRegNo.TabIndex = 132
        Me.rdoCalRegNo.Text = "동일 등록번호 기준"
        '
        'spdCalBuf
        '
        Me.spdCalBuf.DataSource = Nothing
        Me.spdCalBuf.Location = New System.Drawing.Point(687, 382)
        Me.spdCalBuf.Name = "spdCalBuf"
        Me.spdCalBuf.OcxState = CType(resources.GetObject("spdCalBuf.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCalBuf.Size = New System.Drawing.Size(66, 21)
        Me.spdCalBuf.TabIndex = 137
        '
        'lblGuide2
        '
        Me.lblGuide2.Location = New System.Drawing.Point(6, 387)
        Me.lblGuide2.Name = "lblGuide2"
        Me.lblGuide2.Size = New System.Drawing.Size(80, 21)
        Me.lblGuide2.TabIndex = 136
        Me.lblGuide2.Text = "입력가능문자"
        Me.lblGuide2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGuide
        '
        Me.lblGuide.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGuide.Location = New System.Drawing.Point(84, 388)
        Me.lblGuide.Name = "lblGuide"
        Me.lblGuide.Size = New System.Drawing.Size(596, 21)
        Me.lblGuide.TabIndex = 135
        Me.lblGuide.Text = "A~J  0~9 + - * / (  )"
        '
        'lblCal
        '
        Me.lblCal.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCal.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCal.ForeColor = System.Drawing.Color.White
        Me.lblCal.Location = New System.Drawing.Point(8, 360)
        Me.lblCal.Name = "lblCal"
        Me.lblCal.Size = New System.Drawing.Size(64, 21)
        Me.lblCal.TabIndex = 134
        Me.lblCal.Text = "계산식"
        Me.lblCal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblInfo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(8, 105)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(64, 21)
        Me.lblInfo.TabIndex = 133
        Me.lblInfo.Text = "관련검사"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCalR
        '
        Me.lblCalR.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCalR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCalR.ForeColor = System.Drawing.Color.White
        Me.lblCalR.Location = New System.Drawing.Point(8, 16)
        Me.lblCalR.Name = "lblCalR"
        Me.lblCalR.Size = New System.Drawing.Size(64, 21)
        Me.lblCalR.TabIndex = 130
        Me.lblCalR.Text = "계산범위"
        Me.lblCalR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnVerify
        '
        Me.btnVerify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVerify.Location = New System.Drawing.Point(687, 359)
        Me.btnVerify.Name = "btnVerify"
        Me.btnVerify.Size = New System.Drawing.Size(64, 22)
        Me.btnVerify.TabIndex = 129
        Me.btnVerify.Text = "검증(&V)"
        '
        'txtCalForm
        '
        Me.txtCalForm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCalForm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCalForm.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCalForm.Location = New System.Drawing.Point(73, 360)
        Me.txtCalForm.MaxLength = 200
        Me.txtCalForm.Name = "txtCalForm"
        Me.txtCalForm.Size = New System.Drawing.Size(612, 21)
        Me.txtCalForm.TabIndex = 128
        '
        'spdCalTest
        '
        Me.spdCalTest.DataSource = Nothing
        Me.spdCalTest.Location = New System.Drawing.Point(8, 126)
        Me.spdCalTest.Name = "spdCalTest"
        Me.spdCalTest.OcxState = CType(resources.GetObject("spdCalTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCalTest.Size = New System.Drawing.Size(748, 226)
        Me.spdCalTest.TabIndex = 120
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnSelSpc)
        Me.grpCd.Controls.Add(Me.txtSpcNmD)
        Me.grpCd.Controls.Add(Me.txtSpcCd)
        Me.grpCd.Controls.Add(Me.lblSpcCd)
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
        'btnSelSpc
        '
        Me.btnSelSpc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSelSpc.Image = CType(resources.GetObject("btnSelSpc.Image"), System.Drawing.Image)
        Me.btnSelSpc.Location = New System.Drawing.Point(252, 16)
        Me.btnSelSpc.Name = "btnSelSpc"
        Me.btnSelSpc.Size = New System.Drawing.Size(26, 21)
        Me.btnSelSpc.TabIndex = 182
        Me.btnSelSpc.UseVisualStyleBackColor = True
        '
        'txtSpcNmD
        '
        Me.txtSpcNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSpcNmD.Location = New System.Drawing.Point(482, 16)
        Me.txtSpcNmD.Name = "txtSpcNmD"
        Me.txtSpcNmD.ReadOnly = True
        Me.txtSpcNmD.Size = New System.Drawing.Size(188, 21)
        Me.txtSpcNmD.TabIndex = 96
        '
        'txtSpcCd
        '
        Me.txtSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSpcCd.Location = New System.Drawing.Point(217, 16)
        Me.txtSpcCd.MaxLength = 4
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.Size = New System.Drawing.Size(34, 21)
        Me.txtSpcCd.TabIndex = 94
        Me.txtSpcCd.Tag = "TCLSCD"
        '
        'lblSpcCd
        '
        Me.lblSpcCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(152, 16)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(64, 21)
        Me.lblSpcCd.TabIndex = 95
        Me.lblSpcCd.Text = "검체코드"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTNmD
        '
        Me.txtTNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTNmD.Location = New System.Drawing.Point(279, 16)
        Me.txtTNmD.Name = "txtTNmD"
        Me.txtTNmD.ReadOnly = True
        Me.txtTNmD.Size = New System.Drawing.Size(202, 21)
        Me.txtTNmD.TabIndex = 1
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestCd.Location = New System.Drawing.Point(73, 16)
        Me.txtTestCd.MaxLength = 7
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(72, 21)
        Me.txtTestCd.TabIndex = 0
        Me.txtTestCd.Tag = "TCLSCD"
        '
        'lblTestCd
        '
        Me.lblTestCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(8, 16)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(64, 21)
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
        Me.btnUE.Location = New System.Drawing.Point(682, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 2
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TextBox2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBox2.Location = New System.Drawing.Point(232, 455)
        Me.TextBox2.MaxLength = 200
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(14, 14)
        Me.TextBox2.TabIndex = 162
        Me.TextBox2.Text = "↑"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.TextBox1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBox1.Location = New System.Drawing.Point(232, 440)
        Me.TextBox1.MaxLength = 200
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(14, 14)
        Me.TextBox1.TabIndex = 161
        Me.TextBox1.Text = "↓"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(82, 455)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(197, 12)
        Me.Label4.TabIndex = 160
        Me.Label4.Text = "큰값   비교: MAX(1,2) => ↑(1,2)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(82, 440)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(197, 12)
        Me.Label1.TabIndex = 159
        Me.Label1.Text = "작은값 비교: MIN(1,2) => ↓(1,2)"
        '
        'FDF12
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(795, 607)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF12"
        Me.Text = "[12] 계산식"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tpg1.ResumeLayout(False)
        Me.tpg1.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.pnlCalView.ResumeLayout(False)
        Me.pnlCalType.ResumeLayout(False)
        Me.pnlCalR.ResumeLayout(False)
        CType(Me.spdCalBuf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdCalTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnSelTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelSpc.Click
        Dim sFn As String = "btnAddTest_Click"

        Try

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnSelSpc) + Me.btnSelSpc.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnSelSpc)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_list("", "", Me.txtTestCd.Text)
            Dim sSql As String = "((tcdgbn IN ('B', 'P') AND titleyn = '1') OR tcdgbn IN ('S', 'C'))" + IIf(Me.txtTestCd.Text = "", "", " AND testcd = '" + Me.txtTestCd.Text + "'").ToString
            Dim a_dr As DataRow() = dt.Select(sSql, "")

            dt = Fn.ChangeToDataTable(a_dr)

            objHelp.FormText = "검사정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "구분", 0, , , True)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)
            If alList.Count > 0 Then
                miSelectKey = 1

                Me.txtTestCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.txtSpcCd.Text = alList.Item(0).ToString.Split("|"c)(1)
                Me.txtTNmD.Text = alList.Item(0).ToString.Split("|"c)(2)
                Me.txtSpcNmD.Text = alList.Item(0).ToString.Split("|"c)(3)

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

            Dim sMsg As String = "검사코드 : " & Me.txtTestCd.Text & vbCrLf
            sMsg &= "검체코드  : " & Me.txtSpcCd.Text & vbCrLf & vbCrLf
            sMsg &= "검사명  : " & Me.txtTNmD.Text & vbCrLf & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransCalcInfo_UE(Me.txtTestCd.Text, Me.txtSpcCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 계산식정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click
        Dim sFn As String = "btnVerify_Click"

        If fnValidate() = False Then Return
        If Me.spdCalTest.Tag Is Nothing Then Return

        Try
            Dim sCF As String = Me.txtCalForm.Text.Trim

            Dim iParamCnt As Integer = Convert.ToInt32(Val(Me.spdCalTest.Tag))

            With Me.spdCalTest
                For i As Integer = 1 To iParamCnt
                    .Col = .GetColFromID("CID")
                    .Row = i

                    If sCF.IndexOf(.Text) < 0 Then
                        MsgBox("관련검사가 계산식에 존재하지 않습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

                        Return
                    End If
                Next

                For i As Integer = 1 To miMaxParam
                    If sCF.IndexOf(Convert.ToChar(65 + i - 1)) >= 0 Then
                        If i > iParamCnt Then
                            MsgBox("계산식에 포함되는 관련검사가 설정되지 않았습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

                            Return
                        End If
                    End If
                Next
            End With

            'Dim bErrCF As Boolean = False

            'bErrCF = Fn.FindErrCalcFormula(sCF)

            'If bErrCF Then
            '    MsgBox("계산식에 오류가 있습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

            '    Return
            'End If

            For i As Integer = 1 To miMaxParam
                sCF = sCF.Replace(Convert.ToChar(65 + i - 1).ToString, i.ToString)
            Next

            sCF = sCF.Replace("^", "").Replace(",", "+").Replace("~", "1").Replace("!", "0").Replace("@", "1").Replace("♂", "1").Replace("♀", "0").Replace("↓", "").Replace("↑", "")

            Try
                With Me.spdCalBuf
                    .Col = 1 : .Row = 1 : .Text = ""
                    .Formula = sCF

                    If IsNumeric(.Text) Then
                        Me.btnVerify.Enabled = False
                        Me.btnVerify.Tag = 1
                    Else
                        MsgBox("계산식에 오류가 있습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)
                    End If
                End With

            Catch ex As Exception
                MsgBox("계산식에 오류가 있습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)
            End Try

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub spdCalTest_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdCalTest.ButtonClicked
        Dim sFn As String = "spdCalTest_ButtonClicked"

        If e.row < 1 Then Return
        If e.col <> Me.spdCalTest.GetColFromID("HLP") Then Return

        If Len(Me.txtTestCd.Text.Trim) < 1 Or Len(Me.txtTNmD.Text.Trim) < 1 Then Return
        If Len(Me.txtSpcCd.Text.Trim) < 1 Or Len(Me.txtSpcNmD.Text.Trim) < 1 Then Return

        Try

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = miMouseY

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = miMouseX

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_list("", "", "", IIf(Me.rdoCalBcNo.Checked, Me.txtSpcCd.Text, "").ToString)
            Dim a_dr As DataRow() = dt.Select("(tcdgbn IN ('P', 'B') AND titleyn = '0' OR tcdgbn IN ('S', 'C'))")
            dt = Fn.ChangeToDataTable(a_dr)

            objHelp.FormText = "검사정보"

            objHelp.MaxRows = 15
            objHelp.OnRowReturnYN = True

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "", 0, , , True)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                miSelectKey = 1

                With Me.spdCalTest
                    .SetText(.GetColFromID("TESTCD"), e.row, alList.Item(0).ToString.Split("|"c)(0))
                    .SetText(.GetColFromID("SPCCD"), e.row, alList.Item(0).ToString.Split("|"c)(1))
                    .SetText(.GetColFromID("TNMD"), e.row, alList.Item(0).ToString.Split("|"c)(2))
                    .SetText(.GetColFromID("SPCNMD"), e.row, alList.Item(0).ToString.Split("|"c)(3))
                End With
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

    Private Sub spdCalTest_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdCalTest.DblClick
        If e.row < 1 Then Return

        If MsgBox("관련검사에서 제거하시겠습니까?", MsgBoxStyle.Information Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return

        With Me.spdCalTest
            .ClearRange(.GetColFromID("TESTCD"), e.row, .GetColFromID("SPCNMD"), e.row, True)
        End With
    End Sub

    Private Sub spdCalTest_MouseDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseDownEvent) Handles spdCalTest.MouseDownEvent
        miMouseX = Ctrl.FindControlLeft(Me.spdCalTest) + e.x
        miMouseY = Ctrl.FindControlTop(Me.spdCalTest) + e.y
    End Sub

    Private Sub txtCalForm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCalForm.TextChanged
        If miSelectKey = 1 Then Return

        Me.btnVerify.Enabled = True
        Me.btnVerify.Tag = 0
    End Sub

    Private Sub txtSpcCd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSpcCd.Validating
        If miSelectKey = 1 Then Return

        If Me.txtTNmD.Text.Length < 1 Or Me.txtSpcCd.Text = "" Then Return

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_BatteryParentSingle("", Me.txtTestCd.Text, Me.txtSpcCd.Text)
        Dim dr As DataRow()

        dr = dt.Select("(tcdgbn IN ('S', 'C') OR (tcdgbn IN ('P', 'B') AND titleyn = '0'))", "")

        Me.txtTNmD.Text = ""
        Me.txtSpcNmD.Text = ""

        If dr.Length > 0 Then
            Me.txtTNmD.Text = dr(0).Item("tnmd").ToString()
            Me.txtSpcNmD.Text = dr(0).Item("spcnmd").ToString()
        End If
    End Sub

    Private Sub txtTClsCd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTestCd.Validating
        If miSelectKey = 1 Then Return

        If txtTestCd.Text = "" Then Return

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TestCd(Me.txtTestCd.Text, 0)
        Dim dr As DataRow()

        dr = dt.Select("", "")

        Me.txtTNmD.Text = ""

        If dr.Length > 0 Then
            Me.txtTNmD.Text = dr(0).Item("tnmd").ToString()
            If dr.Length = 1 Then
                Me.txtSpcCd.Text = dr(0).Item("spccd").ToString()
                Me.txtSpcNmD.Text = dr(0).Item("spcnmd").ToString()
            End If
        End If
    End Sub

    Private Sub FDF12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FDF12_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
    End Sub

End Class
