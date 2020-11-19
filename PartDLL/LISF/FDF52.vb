'>>> [51] 배지
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF52
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF52.vb, Class : FDF5" + vbTab
    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1

    Private mobjDAF As New LISAPP.APP_F_CULT

    Public gsModDT As String = ""
    Public gsModID As String = ""
    Friend WithEvents tbcBody As System.Windows.Forms.TabControl
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents lblCultNm As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents txtCultNm As System.Windows.Forms.TextBox
    Friend WithEvents txtUseDayS As System.Windows.Forms.TextBox
    Friend WithEvents lblUseDayS As System.Windows.Forms.Label
    Friend WithEvents txtUseDayE As System.Windows.Forms.TextBox
    Friend WithEvents lblUseDayE As System.Windows.Forms.Label
    Friend WithEvents txtBcCNT As System.Windows.Forms.TextBox
    Friend WithEvents lblBcpCNT As System.Windows.Forms.Label
    Friend WithEvents btnHelp_test As System.Windows.Forms.Button
    Friend WithEvents txtTnmd As System.Windows.Forms.TextBox
    Friend WithEvents txtSelSpc As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents btnHelp_spc As System.Windows.Forms.Button
    Friend WithEvents txtSpccd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Private Function fnCollectItemTable_410(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_170(String) As LISAPP.ItemTableCollection"

        Try
            Dim it As New LISAPP.ItemTableCollection

            With it

                If Me.txtSelSpc.Text = "" Then Me.txtSelSpc.Tag = Me.txtSpccd.Text + "|"c

                For ix As Integer = 1 To Me.txtSelSpc.Tag.ToString.Split("|"c).Length
                    If Me.txtSelSpc.Tag.ToString.Split("|"c)(ix - 1) = "" Then Exit For

                    .SetItemTable("TESTCD", 1, ix, Me.txtTestCd.Text)
                    .SetItemTable("SPCCD", 2, ix, Me.txtSelSpc.Tag.ToString.Split("|"c)(ix - 1))
                    .SetItemTable("CULTNM", 3, ix, Me.txtCultNm.Text)
                    .SetItemTable("USEDAYS", 4, ix, Me.txtUseDayS.Text)
                    .SetItemTable("USEDAYE", 5, ix, Me.txtUseDayE.Text)
                    .SetItemTable("BCCNT", 6, ix, Me.txtBcCNT.Text)
                    .SetItemTable("REGDT", 7, ix, rsRegDT)
                    .SetItemTable("REGID", 8, ix, USER_INFO.USRID)
                    .SetItemTable("REGIP", 9, ix, USER_INFO.LOCALIP)
                Next

            End With

            Return it

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it410 As New LISAPP.ItemTableCollection
            Dim iRegType410 As Integer = 0
            Dim sRegDT As String = ""

            iRegType410 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it410 = fnCollectItemTable_410(sRegDT)

            If mobjDAF.TransCultInfo(it410, iRegType410, Me.txtCultNm.Text, Me.txtTestCd.Text, Me.txtSpccd.Text, Me.txtUseDayS.Text, USER_INFO.USRID) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function


    Private Function fnGetSystemDT() As String
        Dim sFn$ = "Private Function fnGetSystemDT() As String"

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

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Me.txtCultNm.Text = "" Then
                MsgBox("배지명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTestCd.Text = "" Then
                MsgBox("검사코드를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtSelSpc.Text = "" And Me.txtSpccd.Text = "" Then
                MsgBox("검체코드를 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtUseDayS.Text.Length <> 4 Then
                MsgBox("시작월일을 확인해 주세요!!(예:0403)", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtUseDayE.Text.Length <> 4 Then
                MsgBox("종료월일을 확인해 주세요!!(예:0403)", MsgBoxStyle.Critical)
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

    Public Sub sbDisplayCdDetail(ByVal rsCult As String, ByVal rsTestcd As String, ByVal rsSpccd As String, ByVal rsUsedays As String, ByVal rsModid As String, ByVal rsModdt As String)
        Dim sFn As String = "sbDisplayCdDetail(String, String, String, String, String)"

        Try
            miSelectKey = 1

            sbDisplayCdDetail_Cult(rsCult, rsTestcd, rsSpccd, rsUsedays, rsModid, rsModdt)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub


    Private Sub sbDisplayCdDetail_Cult(ByVal rsCult As String, ByVal rsTestcd As String, ByVal rsSpccd As String, ByVal rsUsedays As String, ByVal rsModid As String, ByVal rsModdt As String)
        Dim sFn As String = "sbDisplayCdDetail_Cult(String, String, String, String, String, String)"
        Dim iCol% = 0

        Try
            Dim dt As New DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex% = -1

            If rsModdt.Equals("") Or rsModid.Equals("") Then
                dt = mobjDAF.GetCultInfo(rsCult, rsTestcd, rsSpccd, rsUsedays)
            Else
                dt = mobjDAF.GetCultInfo(rsCult, rsTestcd, rsSpccd, rsUsedays, rsModid, rsModdt)
            End If

            sbInitialize()

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

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
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

            sbInitialize_ErrProvider()

            sbInitialize_Control()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn$ = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbInitialize_Control(Optional ByVal iMode% = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode% = 0)"

        Try
            If iMode = 0 Then
                Me.btnUE.Visible = False
                Me.txtCultNm.Text = ""
                Me.txtTestCd.Text = ""
                Me.txtSelSpc.Text = "" : Me.txtSelSpc.Tag = ""
                Me.txtUseDayE.Text = "" : Me.txtUseDayS.Text = "" : Me.txtTnmd.Text = "" : Me.txtSelSpc.Text = "" : Me.txtBcCNT.Text = ""
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = "" : Me.txtRegNm.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtModNm.Text = "" : Me.txtRegNm.Text = ""
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
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF52))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tbcBody = New System.Windows.Forms.TabControl
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
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.txtCultNm = New System.Windows.Forms.TextBox
        Me.lblCultNm = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.btnHelp_spc = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSelSpc = New System.Windows.Forms.TextBox
        Me.txtTnmd = New System.Windows.Forms.TextBox
        Me.btnHelp_test = New System.Windows.Forms.Button
        Me.txtBcCNT = New System.Windows.Forms.TextBox
        Me.lblBcpCNT = New System.Windows.Forms.Label
        Me.txtUseDayE = New System.Windows.Forms.TextBox
        Me.lblUseDayE = New System.Windows.Forms.Label
        Me.txtUseDayS = New System.Windows.Forms.TextBox
        Me.lblUseDayS = New System.Windows.Forms.Label
        Me.lblTestCd = New System.Windows.Forms.Label
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.txtSpccd = New System.Windows.Forms.TextBox
        Me.pnlTop.SuspendLayout()
        Me.tbcBody.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.Controls.Add(Me.tbcBody)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 1
        '
        'tbcBody
        '
        Me.tbcBody.Controls.Add(Me.tbcTpg)
        Me.tbcBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcBody.Location = New System.Drawing.Point(0, 0)
        Me.tbcBody.Name = "tbcBody"
        Me.tbcBody.SelectedIndex = 0
        Me.tbcBody.Size = New System.Drawing.Size(788, 601)
        Me.tbcBody.TabIndex = 1
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
        Me.tbcTpg.Controls.Add(Me.grpCd)
        Me.tbcTpg.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg.Name = "tbcTpg"
        Me.tbcTpg.Size = New System.Drawing.Size(780, 576)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "배지정보"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(292, 538)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 187
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(706, 538)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 187
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(292, 538)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(68, 21)
        Me.txtModID.TabIndex = 22
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(207, 538)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 21
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(94, 538)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 20
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(9, 538)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(84, 21)
        Me.lblModDT.TabIndex = 19
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(507, 538)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 16
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(621, 538)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 21)
        Me.lblUserNm.TabIndex = 15
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(422, 538)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 18
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(706, 538)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 17
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.txtCultNm)
        Me.grpCd.Controls.Add(Me.lblCultNm)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 8)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 54)
        Me.grpCd.TabIndex = 0
        Me.grpCd.TabStop = False
        '
        'txtCultNm
        '
        Me.txtCultNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCultNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCultNm.Location = New System.Drawing.Point(73, 18)
        Me.txtCultNm.MaxLength = 20
        Me.txtCultNm.Name = "txtCultNm"
        Me.txtCultNm.Size = New System.Drawing.Size(160, 21)
        Me.txtCultNm.TabIndex = 1
        Me.txtCultNm.Tag = "CULTNM"
        '
        'lblCultNm
        '
        Me.lblCultNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCultNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCultNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCultNm.ForeColor = System.Drawing.Color.White
        Me.lblCultNm.Location = New System.Drawing.Point(8, 18)
        Me.lblCultNm.Name = "lblCultNm"
        Me.lblCultNm.Size = New System.Drawing.Size(64, 21)
        Me.lblCultNm.TabIndex = 0
        Me.lblCultNm.Text = "배지명"
        Me.lblCultNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(687, 13)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 2
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.txtSpccd)
        Me.grpCdInfo1.Controls.Add(Me.btnHelp_spc)
        Me.grpCdInfo1.Controls.Add(Me.Label1)
        Me.grpCdInfo1.Controls.Add(Me.txtSelSpc)
        Me.grpCdInfo1.Controls.Add(Me.txtTnmd)
        Me.grpCdInfo1.Controls.Add(Me.btnHelp_test)
        Me.grpCdInfo1.Controls.Add(Me.txtBcCNT)
        Me.grpCdInfo1.Controls.Add(Me.lblBcpCNT)
        Me.grpCdInfo1.Controls.Add(Me.txtUseDayE)
        Me.grpCdInfo1.Controls.Add(Me.lblUseDayE)
        Me.grpCdInfo1.Controls.Add(Me.txtUseDayS)
        Me.grpCdInfo1.Controls.Add(Me.lblUseDayS)
        Me.grpCdInfo1.Controls.Add(Me.lblTestCd)
        Me.grpCdInfo1.Controls.Add(Me.txtTestCd)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 68)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(768, 456)
        Me.grpCdInfo1.TabIndex = 1
        Me.grpCdInfo1.TabStop = False
        '
        'btnHelp_spc
        '
        Me.btnHelp_spc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_spc.Image = CType(resources.GetObject("btnHelp_spc.Image"), System.Drawing.Image)
        Me.btnHelp_spc.Location = New System.Drawing.Point(73, 47)
        Me.btnHelp_spc.Name = "btnHelp_spc"
        Me.btnHelp_spc.Size = New System.Drawing.Size(26, 21)
        Me.btnHelp_spc.TabIndex = 12
        Me.btnHelp_spc.TabStop = False
        Me.btnHelp_spc.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(8, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 21)
        Me.Label1.TabIndex = 11
        Me.Label1.Tag = ""
        Me.Label1.Text = "검체코드"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSelSpc
        '
        Me.txtSelSpc.Location = New System.Drawing.Point(146, 47)
        Me.txtSelSpc.Name = "txtSelSpc"
        Me.txtSelSpc.ReadOnly = True
        Me.txtSelSpc.Size = New System.Drawing.Size(613, 21)
        Me.txtSelSpc.TabIndex = 7
        Me.txtSelSpc.TabStop = False
        Me.txtSelSpc.Tag = "SPCNMD"
        '
        'txtTnmd
        '
        Me.txtTnmd.Location = New System.Drawing.Point(174, 25)
        Me.txtTnmd.Name = "txtTnmd"
        Me.txtTnmd.ReadOnly = True
        Me.txtTnmd.Size = New System.Drawing.Size(585, 21)
        Me.txtTnmd.TabIndex = 6
        Me.txtTnmd.TabStop = False
        Me.txtTnmd.Tag = "TNMD"
        '
        'btnHelp_test
        '
        Me.btnHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_test.Image = CType(resources.GetObject("btnHelp_test.Image"), System.Drawing.Image)
        Me.btnHelp_test.Location = New System.Drawing.Point(146, 25)
        Me.btnHelp_test.Name = "btnHelp_test"
        Me.btnHelp_test.Size = New System.Drawing.Size(26, 21)
        Me.btnHelp_test.TabIndex = 5
        Me.btnHelp_test.TabStop = False
        Me.btnHelp_test.UseVisualStyleBackColor = True
        '
        'txtBcCNT
        '
        Me.txtBcCNT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcCNT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcCNT.Location = New System.Drawing.Point(131, 120)
        Me.txtBcCNT.MaxLength = 2
        Me.txtBcCNT.Name = "txtBcCNT"
        Me.txtBcCNT.Size = New System.Drawing.Size(40, 21)
        Me.txtBcCNT.TabIndex = 10
        Me.txtBcCNT.Tag = "BCCNT"
        '
        'lblBcpCNT
        '
        Me.lblBcpCNT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcpCNT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcpCNT.ForeColor = System.Drawing.Color.White
        Me.lblBcpCNT.Location = New System.Drawing.Point(8, 120)
        Me.lblBcpCNT.Name = "lblBcpCNT"
        Me.lblBcpCNT.Size = New System.Drawing.Size(122, 21)
        Me.lblBcpCNT.TabIndex = 7
        Me.lblBcpCNT.Text = "바코드 출력 매수"
        Me.lblBcpCNT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUseDayE
        '
        Me.txtUseDayE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUseDayE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUseDayE.Location = New System.Drawing.Point(131, 98)
        Me.txtUseDayE.MaxLength = 4
        Me.txtUseDayE.Name = "txtUseDayE"
        Me.txtUseDayE.Size = New System.Drawing.Size(40, 21)
        Me.txtUseDayE.TabIndex = 9
        Me.txtUseDayE.Tag = "USEDAYE"
        '
        'lblUseDayE
        '
        Me.lblUseDayE.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUseDayE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUseDayE.ForeColor = System.Drawing.Color.White
        Me.lblUseDayE.Location = New System.Drawing.Point(8, 98)
        Me.lblUseDayE.Name = "lblUseDayE"
        Me.lblUseDayE.Size = New System.Drawing.Size(122, 21)
        Me.lblUseDayE.TabIndex = 5
        Me.lblUseDayE.Text = "사용기간 종료월일"
        Me.lblUseDayE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUseDayS
        '
        Me.txtUseDayS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUseDayS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUseDayS.Location = New System.Drawing.Point(131, 76)
        Me.txtUseDayS.MaxLength = 4
        Me.txtUseDayS.Name = "txtUseDayS"
        Me.txtUseDayS.Size = New System.Drawing.Size(40, 21)
        Me.txtUseDayS.TabIndex = 8
        Me.txtUseDayS.Tag = "USEDAYS"
        '
        'lblUseDayS
        '
        Me.lblUseDayS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUseDayS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUseDayS.ForeColor = System.Drawing.Color.White
        Me.lblUseDayS.Location = New System.Drawing.Point(8, 76)
        Me.lblUseDayS.Name = "lblUseDayS"
        Me.lblUseDayS.Size = New System.Drawing.Size(122, 21)
        Me.lblUseDayS.TabIndex = 3
        Me.lblUseDayS.Text = "사용기간 시작월일"
        Me.lblUseDayS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTestCd
        '
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(8, 25)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(64, 21)
        Me.lblTestCd.TabIndex = 0
        Me.lblTestCd.Tag = ""
        Me.lblTestCd.Text = "검사코드"
        Me.lblTestCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTestCd.Location = New System.Drawing.Point(73, 25)
        Me.txtTestCd.MaxLength = 5
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(72, 21)
        Me.txtTestCd.TabIndex = 3
        Me.txtTestCd.Tag = "TESTCD"
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'txtSpccd
        '
        Me.txtSpccd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpccd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpccd.Location = New System.Drawing.Point(73, 47)
        Me.txtSpccd.MaxLength = 5
        Me.txtSpccd.Name = "txtSpccd"
        Me.txtSpccd.Size = New System.Drawing.Size(72, 21)
        Me.txtSpccd.TabIndex = 13
        Me.txtSpccd.Tag = "SPCCD"
        '
        'FDF52
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.KeyPreview = True
        Me.Name = "FDF52"
        Me.Text = "[52] 배지"
        Me.pnlTop.ResumeLayout(False)
        Me.tbcBody.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click(Object, System.EventArgs) Handles btnUE.Click"

        If Me.txtCultNm.Text = "" Then Return
        If Me.txtTestCd.Text = "" Then Return
        If Me.txtSpccd.Text = "" Then Return

        Try

            Dim sMsg As String = "배지명 : " & Me.txtCultNm.Text & vbCrLf
            sMsg &= "검사코드 : " & Me.txtTestCd.Text & vbCrLf
            sMsg &= "검체코드 : " & Me.txtSelSpc.Text & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransCultInfo_UE(Me.txtCultNm.Text, Me.txtTestCd.Text, Me.txtSpccd.Text, Me.txtUseDayS.Text, USER_INFO.USRID) Then
                MsgBox("해당 배지정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub FDF42_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp_test.Click
        Dim sFn As String = "btnTestcd_Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnHelp_test) + Me.btnHelp_test.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnHelp_test)

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_list("", "", "", Me.txtTestCd.Text)
            Dim a_dr As DataRow() = dt.Select("tcdgbn IN ('P', 'S') AND mbttype IN ('2', '3')")
            dt = Fn.ChangeToDataTable(a_dr)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "검사정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                miSelectKey = 1

                Me.txtTestCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.txtTnmd.Text = alList.Item(0).ToString.Split("|"c)(1)

                miSelectKey = 0
            End If


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
            miSelectKey = 0

        End Try
    End Sub

    Private Sub txtCultNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCultNm.KeyDown, txtTestCd.KeyDown, txtBcCNT.KeyDown, txtUseDayE.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        If CType(sender, Windows.Forms.TextBox).Name.ToUpper = "TXTTESTCD" Then
            Me.txtTnmd.Text = ""
            Me.txtSelSpc.Text = "" : Me.txtSelSpc.Tag = ""

            If CType(sender, Windows.Forms.TextBox).Text = "" Then
                SendKeys.Send("{TAB}")
            Else
                btnCdHelp_test_Click(Nothing, Nothing)
            End If
        ElseIf CType(sender, Windows.Forms.TextBox).Name.ToUpper = "TXTSPCCD" Then
            Me.txtSelSpc.Text = ""

            If CType(sender, Windows.Forms.TextBox).Text = "" Then
                SendKeys.Send("{TAB}")
            Else
                btnCdHelp_test_Click(Nothing, Nothing)
            End If
        Else
            SendKeys.Send("{TAB}")
        End If

    End Sub

    Private Sub btnHelp_spc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_spc.Click
        Dim sFn As String = "Handles btnCdHelp_spc.Click"
        Try
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnHelp_test) + Me.btnHelp_test.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List("", "", "", "", "", Me.txtTestCd.Text, "")

            objHelp.FormText = "검체정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("spcnmd", "검체명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then

                Dim sSpcCds As String = "", sSpcNmds As String = ""

                For ix As Integer = 0 To alList.Count - 1
                    Dim sSpccd As String = alList.Item(ix).ToString.Split("|"c)(1)
                    Dim sSpcnmd As String = alList.Item(ix).ToString.Split("|"c)(0)

                    If ix > 0 Then
                        sSpcCds += "|" : sSpcNmds += "|"
                    End If

                    sSpcCds += sSpccd : sSpcNmds += sSpcnmd
                Next

                Me.txtSelSpc.Text = sSpcNmds.Replace("|", ",")
                Me.txtSelSpc.Tag = sSpcCds
            Else
                Me.txtSelSpc.Text = ""
                Me.txtSelSpc.Tag = ""
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
