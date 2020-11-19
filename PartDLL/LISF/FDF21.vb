'>>> [11] 소견
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF21
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF21.vb, Class : FDF21" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_SPTEST_CMT

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents btnReg_dispseq As System.Windows.Forms.Button
    Friend WithEvents txtCmtseq As System.Windows.Forms.TextBox
    Friend WithEvents txtCmtHide As System.Windows.Forms.TextBox
    Friend WithEvents txtTestcdHide As System.Windows.Forms.TextBox
    Friend WithEvents lblCmtSeq As System.Windows.Forms.Label


    Private Function fnCollectItemTable_21(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_80(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it21 As New LISAPP.ItemTableCollection

            With it21
                .SetItemTable("TESTCD", 1, 1, Me.txtTestcd.Text)
                .SetItemTable("CMTSEQ", 2, 1, Me.txtCmtseq.Text)
                .SetItemTable("CMTCONT", 3, 1, Me.txtCmtCont.Text.Replace("'", "`"))
                .SetItemTable("REGDT", 4, 1, rsRegDT)
                .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                '.SetItemTable("REGID", 6, 1, USER_INFO.USRID)
                '.SetItemTable("REGIP", 7, 1, USER_INFO.LOCALIP)
            End With

            fnCollectItemTable_21 = it21
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it80 As New LISAPP.ItemTableCollection
            Dim iRegType21 As Integer = 0
            Dim sRegDT As String

            iRegType21 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it80 = fnCollectItemTable_21(sRegDT)

            If mobjDAF.TransSpCmtTestInfo(it80, iRegType21, Me.txtTestcdHide.Text, Me.txtCmtHide.Text, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsTestcd As String, ByVal rsCmtseq As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentSpCmtTestInfo(rsTestcd, rsCmtseq)

            If dt.Rows.Count > 0 Then
                Return "검사코드 : " + rsTestcd + vbCrLf + _
                       "소견코드 : " + rsCmtseq + "인 동일 소견이 존재합니다." + vbCrLf + vbCrLf + _
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
            If Len(Me.txtTestcd.Text.Trim) < 1 Then
                MsgBox("검사코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtCmtseq.Text.Trim) < 1 Then
                MsgBox("소견코드내용을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtCmtCont.Text.Trim) < 1 Then
                MsgBox("소견내용을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then

                    Dim sBuf As String = fnFindConflict(txtTestcd.Text, txtCmtseq.Text)

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
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsCmtCd As String, ByVal rsCmtseq As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_Cmt(rsCmtCd, rsCmtseq)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Cmt(ByVal rsTestcd As String, ByVal rsCmtseq As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Cmt(String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetSpCmtTestInfo(rsTestcd, rsCmtseq)
            Else
                dt = mobjDAF.GetSpCmtTestInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsTestcd, rsCmtseq)
            End If

            If dt.Rows.Count < 1 Then Return


            Me.txtTestcd.Text = dt.Rows(0).Item("testcd").ToString
            Me.txtCmtseq.Text = dt.Rows(0).Item("cmtseq").ToString
            Me.txtCmtCont.Text = dt.Rows(0).Item("cmtcont").ToString

            Me.txtTestcdHide.Text = rsTestcd
            Me.txtCmtHide.Text = rsCmtseq

            Me.txtRegDT.Text = dt.Rows(0).Item("regdt").ToString
            Me.txtRegNm.Text = dt.Rows(0).Item("regnm").ToString

            Me.txtModDT.Text = gsModDT
            Me.txtModNm.Text = gsModID

            ''초기화할 것은 ErrorProvider
            'sbInitialize_ErrProvider()

            'sbInitialize_CtrlCollection()

            'Ctrl.FindChildControl(Me.Controls, mchildctrlcol)

            'If dt.Rows.Count < 1 Then Return

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
                'If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0
                Me.txtTestcd.Text = "" : Me.btnUE.Visible = False
                Me.txtCmtCont.Text = "" : Me.txtCmtseq.Text = ""
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
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents txtTestcd As System.Windows.Forms.TextBox
    Friend WithEvents lblCmtCont As System.Windows.Forms.Label
    Friend WithEvents lblTestcd As System.Windows.Forms.Label
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblSlip As System.Windows.Forms.Label
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tclSpc = New System.Windows.Forms.TabControl()
        Me.tpg1 = New System.Windows.Forms.TabPage()
        Me.txtModNm = New System.Windows.Forms.TextBox()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.txtModID = New System.Windows.Forms.TextBox()
        Me.lblModNm = New System.Windows.Forms.Label()
        Me.txtModDT = New System.Windows.Forms.TextBox()
        Me.lblModDT = New System.Windows.Forms.Label()
        Me.txtRegDT = New System.Windows.Forms.TextBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblRegDT = New System.Windows.Forms.Label()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox()
        Me.btnReg_dispseq = New System.Windows.Forms.Button()
        Me.cboSlip = New System.Windows.Forms.ComboBox()
        Me.lblSlip = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblCmtCont = New System.Windows.Forms.Label()
        Me.txtCmtCont = New System.Windows.Forms.TextBox()
        Me.grpCd = New System.Windows.Forms.GroupBox()
        Me.txtCmtseq = New System.Windows.Forms.TextBox()
        Me.lblCmtSeq = New System.Windows.Forms.Label()
        Me.txtTestcd = New System.Windows.Forms.TextBox()
        Me.lblTestcd = New System.Windows.Forms.Label()
        Me.btnUE = New System.Windows.Forms.Button()
        Me.txtCmtHide = New System.Windows.Forms.TextBox()
        Me.txtTestcdHide = New System.Windows.Forms.TextBox()
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
        Me.pnlTop.Size = New System.Drawing.Size(795, 603)
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
        Me.tclSpc.Size = New System.Drawing.Size(791, 599)
        Me.tclSpc.TabIndex = 0
        Me.tclSpc.TabStop = False
        '
        'tpg1
        '
        Me.tpg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpg1.Controls.Add(Me.txtModNm)
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
        Me.tpg1.Size = New System.Drawing.Size(783, 574)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "소견정보"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(293, 545)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 140
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(700, 544)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 140
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(293, 545)
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
        Me.lblModNm.Location = New System.Drawing.Point(208, 545)
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
        Me.txtModDT.Location = New System.Drawing.Point(93, 544)
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
        Me.lblModDT.Location = New System.Drawing.Point(8, 544)
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
        Me.lblUserNm.Location = New System.Drawing.Point(615, 544)
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
        Me.txtRegID.Location = New System.Drawing.Point(700, 544)
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
        Me.grpCdInfo1.Controls.Add(Me.btnReg_dispseq)
        Me.grpCdInfo1.Controls.Add(Me.cboSlip)
        Me.grpCdInfo1.Controls.Add(Me.lblSlip)
        Me.grpCdInfo1.Controls.Add(Me.Label10)
        Me.grpCdInfo1.Controls.Add(Me.lblCmtCont)
        Me.grpCdInfo1.Controls.Add(Me.txtCmtCont)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 60)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 398)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "소견정보"
        '
        'btnReg_dispseq
        '
        Me.btnReg_dispseq.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnReg_dispseq.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReg_dispseq.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg_dispseq.ForeColor = System.Drawing.Color.Black
        Me.btnReg_dispseq.Location = New System.Drawing.Point(409, 215)
        Me.btnReg_dispseq.Margin = New System.Windows.Forms.Padding(1)
        Me.btnReg_dispseq.Name = "btnReg_dispseq"
        Me.btnReg_dispseq.Size = New System.Drawing.Size(103, 21)
        Me.btnReg_dispseq.TabIndex = 140
        Me.btnReg_dispseq.Text = "정렬 순서"
        Me.btnReg_dispseq.UseVisualStyleBackColor = False
        Me.btnReg_dispseq.Visible = False
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.Location = New System.Drawing.Point(89, 215)
        Me.cboSlip.MaxDropDownItems = 20
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(304, 20)
        Me.cboSlip.TabIndex = 139
        Me.cboSlip.Tag = "SLIPNMD_01"
        Me.cboSlip.Visible = False
        '
        'lblSlip
        '
        Me.lblSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlip.ForeColor = System.Drawing.Color.White
        Me.lblSlip.Location = New System.Drawing.Point(8, 215)
        Me.lblSlip.Name = "lblSlip"
        Me.lblSlip.Size = New System.Drawing.Size(80, 21)
        Me.lblSlip.TabIndex = 138
        Me.lblSlip.Text = "검사분야"
        Me.lblSlip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSlip.Visible = False
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.Location = New System.Drawing.Point(2, 201)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(756, 2)
        Me.Label10.TabIndex = 0
        '
        'lblCmtCont
        '
        Me.lblCmtCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtCont.ForeColor = System.Drawing.Color.White
        Me.lblCmtCont.Location = New System.Drawing.Point(8, 16)
        Me.lblCmtCont.Name = "lblCmtCont"
        Me.lblCmtCont.Size = New System.Drawing.Size(57, 174)
        Me.lblCmtCont.TabIndex = 0
        Me.lblCmtCont.Text = "소견내용"
        Me.lblCmtCont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCmtCont
        '
        Me.txtCmtCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCont.Location = New System.Drawing.Point(66, 16)
        Me.txtCmtCont.MaxLength = 1000
        Me.txtCmtCont.Multiline = True
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(687, 174)
        Me.txtCmtCont.TabIndex = 9
        Me.txtCmtCont.Tag = "CMTCONT"
        Me.txtCmtCont.Text = "123456789012345678901234567890123456789012345678901234567890123456789012345678901" & _
            "23456789012345678901234567890123456789012345678901234567890123456789012345678901" & _
            "234567890123456789012345678901234567890"
        '
        'grpCd
        '
        Me.grpCd.Controls.Add(Me.txtTestcdHide)
        Me.grpCd.Controls.Add(Me.txtCmtHide)
        Me.grpCd.Controls.Add(Me.txtCmtseq)
        Me.grpCd.Controls.Add(Me.lblCmtSeq)
        Me.grpCd.Controls.Add(Me.txtTestcd)
        Me.grpCd.Controls.Add(Me.lblTestcd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 50)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        '
        'txtCmtseq
        '
        Me.txtCmtseq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtseq.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCmtseq.Location = New System.Drawing.Point(248, 10)
        Me.txtCmtseq.MaxLength = 5
        Me.txtCmtseq.Name = "txtCmtseq"
        Me.txtCmtseq.Size = New System.Drawing.Size(72, 21)
        Me.txtCmtseq.TabIndex = 8
        Me.txtCmtseq.Tag = "CMTCD"
        '
        'lblCmtSeq
        '
        Me.lblCmtSeq.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCmtSeq.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtSeq.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtSeq.ForeColor = System.Drawing.Color.White
        Me.lblCmtSeq.Location = New System.Drawing.Point(167, 10)
        Me.lblCmtSeq.Name = "lblCmtSeq"
        Me.lblCmtSeq.Size = New System.Drawing.Size(80, 21)
        Me.lblCmtSeq.TabIndex = 9
        Me.lblCmtSeq.Text = "소견코드"
        Me.lblCmtSeq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestcd
        '
        Me.txtTestcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestcd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestcd.Location = New System.Drawing.Point(89, 10)
        Me.txtTestcd.MaxLength = 5
        Me.txtTestcd.Name = "txtTestcd"
        Me.txtTestcd.Size = New System.Drawing.Size(72, 21)
        Me.txtTestcd.TabIndex = 4
        Me.txtTestcd.Tag = "CMTCD"
        '
        'lblTestcd
        '
        Me.lblTestcd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTestcd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestcd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestcd.ForeColor = System.Drawing.Color.White
        Me.lblTestcd.Location = New System.Drawing.Point(8, 10)
        Me.lblTestcd.Name = "lblTestcd"
        Me.lblTestcd.Size = New System.Drawing.Size(80, 21)
        Me.lblTestcd.TabIndex = 7
        Me.lblTestcd.Text = "검사코드"
        Me.lblTestcd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(680, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 6
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'txtCmtHide
        '
        Me.txtCmtHide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtHide.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCmtHide.Location = New System.Drawing.Point(582, 29)
        Me.txtCmtHide.MaxLength = 5
        Me.txtCmtHide.Name = "txtCmtHide"
        Me.txtCmtHide.Size = New System.Drawing.Size(72, 21)
        Me.txtCmtHide.TabIndex = 10
        Me.txtCmtHide.Tag = ""
        Me.txtCmtHide.Visible = False
        '
        'txtTestcdHide
        '
        Me.txtTestcdHide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestcdHide.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestcdHide.Location = New System.Drawing.Point(582, 2)
        Me.txtTestcdHide.MaxLength = 5
        Me.txtTestcdHide.Name = "txtTestcdHide"
        Me.txtTestcdHide.Size = New System.Drawing.Size(72, 21)
        Me.txtTestcdHide.TabIndex = 11
        Me.txtTestcdHide.Tag = ""
        Me.txtTestcdHide.Visible = False
        '
        'FDF21
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(795, 603)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF21"
        Me.Text = "[21] 특수보고서 소견"
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

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        If txtTestcd.Text = "" Then Exit Sub

        Try

            Dim sMsg As String = lblTestcd.Text & " : " & txtTestcd.Text & " , " & lblCmtSeq.Text & " : " & txtCmtseq.Text & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransSpCmtTestInfo_UE(txtTestcd.Text, txtCmtseq.Text, USER_INFO.USRID) Then
                MsgBox("해당 소견정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub FDF11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnReg_dispseq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_dispseq.Click
        Dim sFn As String = "Handles btnReg_dispseql.ButtonClick"

        Dim frmChild As Windows.Forms.Form
        frmChild = New FDF11_S01()

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = System.Windows.Forms.FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()

    End Sub

    Private Sub txtCmtCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestcd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub txtCmtseq_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtseq.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub
End Class

