'>>> [40] 처방슬립
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF40
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF40.vb, Class : FDF40" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_OSLIP

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Public gsModDT As String = ""
    Friend WithEvents txtDoctNm2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDoctNm1 As System.Windows.Forms.TextBox
    Friend WithEvents lblDoctor2 As System.Windows.Forms.Label
    Friend WithEvents txtDoctorID2 As System.Windows.Forms.TextBox
    Friend WithEvents lblDoctor1 As System.Windows.Forms.Label
    Friend WithEvents txtDoctorID1 As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents btnDoctor_HLP2 As System.Windows.Forms.Button
    Friend WithEvents btnDoctor_HLP1 As System.Windows.Forms.Button
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Public gsModID As String = ""

    Private Function fnCollectItemTable_0(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_0(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it0 As New LISAPP.ItemTableCollection

            With it0
                .SetItemTable("tordslip", 1, 1, Me.txtTOSlipCd.Text.Trim)
                .SetItemTable("regdt", 2, 1, rsRegDT)
                .SetItemTable("regid", 3, 1, USER_INFO.USRID)
                .SetItemTable("tordslipnm", 4, 1, Me.txtTOSlipNm.Text.Trim)
                .SetItemTable("dispseq", 5, 1, Me.txtDispSeq.Text)
                .SetItemTable("doctorid1", 6, 1, Me.txtDoctorID1.Text)
                .SetItemTable("doctorid2", 7, 1, Me.txtDoctorID2.Text)
                .SetItemTable("usdt", 8, 1, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)
                .SetItemTable("regip", 9, 1, USER_INFO.LOCALIP)

                If txtUEDT.Text = "" Then
                    .SetItemTable("uedt", 10, 1, msUEDT)
                Else
                    .SetItemTable("uedt", 10, 1, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                End If

            End With

            fnCollectItemTable_0 = it0
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

    Private Function fnFindConflict(ByVal rsOSlipCD As String, ByVal rsUsDt As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentOSlipInfo(rsOSlipCD, rsUsDt)

            If dt.Rows.Count > 0 Then
                Return "검사처방슬립코드 " + dt.Rows(0).Item(0).ToString + "에는 이미 검사처방슬립 내용이 존재합니다." + vbCrLf + vbCrLf + _
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
            Dim DTable As DataTable

            DTable = mobjDAF.GetNewRegDT

            If DTable.Rows.Count > 0 Then
                fnGetSystemDT = DTable.Rows(0).Item(0).ToString
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
            Dim it0 As New LISAPP.ItemTableCollection
            Dim it1 As New LISAPP.ItemTableCollection

            Dim iRegType0 As Integer = 0, iRegType1 As Integer = 0
            Dim sRegDT As String

            iRegType0 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType1 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it0 = fnCollectItemTable_0(sRegDT)

            If mobjDAF.TransOSlipInfo(it0, iRegType0, it1, iRegType1, Me.txtTOSlipCd.Text.Trim, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID) Then
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
            If Len(Me.txtTOSlipCd.Text.Trim) < 1 Then
                MsgBox("검사처방슬립코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtTOSlipNm.Text.Trim) < 1 Then
                MsgBox("검사처방슬립명을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            Me.txtDispSeq.Text = Me.txtDispSeq.Text.Trim

            If Me.txtDispSeq.Text.Length > 0 Then
                If IsNumeric(Me.txtDispSeq.Text) = False Then
                    MsgBox("화면표시순서를 숫자로 입력하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtTOSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

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

    Public Sub sbDisplayCdDetail(ByVal rsOrdSlip As String, ByVal rsUsDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_OSlip(rsOrdSlip, rsUsDt)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_OSlip(ByVal rsOrdSlip As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_OSlip(String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As New DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetOSlipInfo(rsOrdSlip, rsUsDt)
            Else
                dt = mobjDAF.GetOSlipInfo(gsModDT, gsModID, rsOrdSlip)
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

                        If Not IsNothing(Me.Owner) Then
                            If Not CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                                Me.txtUSDay.Text = rsUsDt.Insert(4, "-").Insert(7, "-").Substring(0, 10)
                                Me.dtpUSTime.Value = CDate(rsUsDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":"))
                            End If
                        End If

                    Next
                Next
            Next
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
                Me.txtTOSlipCd.Text = "" : Me.btnUE.Visible = False

                Me.txtTOSlipNm.Text = "" : Me.txtTOSlipNm.ReadOnly = False

                Me.txtDispSeq.Text = "" : Me.txtDispSeq.ReadOnly = False

                Me.txtDoctorID1.Text = "" : Me.txtDoctorID2.Text = "" : Me.txtDoctNm1.Text = "" : Me.txtDoctNm2.Text = ""
                Me.txtUSDT.Text = "" : Me.txtUEDT.Text = "" : Me.txtRegNm.Text = ""

                'With Me.spdDr
                '    .MaxRows = 0
                'End With

                txtRegDT.Text = "" : txtRegID.Text = ""
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
    Friend WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtTOSlipCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTOrdSlipNm As System.Windows.Forms.Label
    Friend WithEvents lblDispSeq As System.Windows.Forms.Label
    Friend WithEvents txtDispSeq As System.Windows.Forms.TextBox
    Friend WithEvents txtTOSlipNm As System.Windows.Forms.TextBox
    Friend WithEvents lblTOSlipCd As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF40))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtUEDT = New System.Windows.Forms.TextBox
        Me.lblUEDT = New System.Windows.Forms.Label
        Me.txtUSDT = New System.Windows.Forms.TextBox
        Me.lblUSDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.btnDoctor_HLP2 = New System.Windows.Forms.Button
        Me.btnDoctor_HLP1 = New System.Windows.Forms.Button
        Me.txtDoctNm2 = New System.Windows.Forms.TextBox
        Me.txtDoctNm1 = New System.Windows.Forms.TextBox
        Me.lblDoctor2 = New System.Windows.Forms.Label
        Me.txtDoctorID2 = New System.Windows.Forms.TextBox
        Me.lblDoctor1 = New System.Windows.Forms.Label
        Me.txtDoctorID1 = New System.Windows.Forms.TextBox
        Me.lblDispSeq = New System.Windows.Forms.Label
        Me.txtDispSeq = New System.Windows.Forms.TextBox
        Me.lblLine = New System.Windows.Forms.Label
        Me.lblTOrdSlipNm = New System.Windows.Forms.Label
        Me.txtTOSlipNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker
        Me.lblUSDayTime = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.lblTOSlipCd = New System.Windows.Forms.Label
        Me.txtTOSlipCd = New System.Windows.Forms.TextBox
        Me.txtUSDay = New System.Windows.Forms.TextBox
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
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
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 116
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tbcTpg)
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
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.txtUEDT)
        Me.tbcTpg.Controls.Add(Me.lblUEDT)
        Me.tbcTpg.Controls.Add(Me.txtUSDT)
        Me.tbcTpg.Controls.Add(Me.lblUSDT)
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
        Me.tbcTpg.Text = "검사처방슬립정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(705, 535)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 183
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUEDT.Location = New System.Drawing.Point(319, 535)
        Me.txtUEDT.Name = "txtUEDT"
        Me.txtUEDT.ReadOnly = True
        Me.txtUEDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUEDT.TabIndex = 13
        Me.txtUEDT.TabStop = False
        Me.txtUEDT.Tag = "UEDT"
        '
        'lblUEDT
        '
        Me.lblUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUEDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Location = New System.Drawing.Point(218, 535)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(100, 21)
        Me.lblUEDT.TabIndex = 14
        Me.lblUEDT.Tag = ""
        Me.lblUEDT.Text = "종료일시(선택)"
        Me.lblUEDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUSDT
        '
        Me.txtUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUSDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUSDT.Location = New System.Drawing.Point(109, 535)
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUSDT.TabIndex = 11
        Me.txtUSDT.TabStop = False
        Me.txtUSDT.Tag = "USDT"
        '
        'lblUSDT
        '
        Me.lblUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDT.ForeColor = System.Drawing.Color.Black
        Me.lblUSDT.Location = New System.Drawing.Point(8, 535)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(100, 21)
        Me.lblUSDT.TabIndex = 12
        Me.lblUSDT.Text = "시작일시(선택)"
        Me.lblUSDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(512, 535)
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
        Me.lblUserNm.Location = New System.Drawing.Point(620, 535)
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
        Me.lblRegDT.Location = New System.Drawing.Point(427, 535)
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
        Me.txtRegID.Location = New System.Drawing.Point(705, 535)
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
        Me.grpCdInfo1.Controls.Add(Me.btnDoctor_HLP2)
        Me.grpCdInfo1.Controls.Add(Me.btnDoctor_HLP1)
        Me.grpCdInfo1.Controls.Add(Me.txtDoctNm2)
        Me.grpCdInfo1.Controls.Add(Me.txtDoctNm1)
        Me.grpCdInfo1.Controls.Add(Me.lblDoctor2)
        Me.grpCdInfo1.Controls.Add(Me.txtDoctorID2)
        Me.grpCdInfo1.Controls.Add(Me.lblDoctor1)
        Me.grpCdInfo1.Controls.Add(Me.txtDoctorID1)
        Me.grpCdInfo1.Controls.Add(Me.lblDispSeq)
        Me.grpCdInfo1.Controls.Add(Me.txtDispSeq)
        Me.grpCdInfo1.Controls.Add(Me.lblLine)
        Me.grpCdInfo1.Controls.Add(Me.lblTOrdSlipNm)
        Me.grpCdInfo1.Controls.Add(Me.txtTOSlipNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 67)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 449)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "검사처방슬립 정보"
        '
        'btnDoctor_HLP2
        '
        Me.btnDoctor_HLP2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDoctor_HLP2.Image = CType(resources.GetObject("btnDoctor_HLP2.Image"), System.Drawing.Image)
        Me.btnDoctor_HLP2.Location = New System.Drawing.Point(604, 39)
        Me.btnDoctor_HLP2.Name = "btnDoctor_HLP2"
        Me.btnDoctor_HLP2.Size = New System.Drawing.Size(26, 21)
        Me.btnDoctor_HLP2.TabIndex = 12
        Me.btnDoctor_HLP2.TabStop = False
        Me.btnDoctor_HLP2.UseVisualStyleBackColor = True
        '
        'btnDoctor_HLP1
        '
        Me.btnDoctor_HLP1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDoctor_HLP1.Image = CType(resources.GetObject("btnDoctor_HLP1.Image"), System.Drawing.Image)
        Me.btnDoctor_HLP1.Location = New System.Drawing.Point(604, 17)
        Me.btnDoctor_HLP1.Name = "btnDoctor_HLP1"
        Me.btnDoctor_HLP1.Size = New System.Drawing.Size(26, 21)
        Me.btnDoctor_HLP1.TabIndex = 9
        Me.btnDoctor_HLP1.TabStop = False
        Me.btnDoctor_HLP1.UseVisualStyleBackColor = True
        '
        'txtDoctNm2
        '
        Me.txtDoctNm2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoctNm2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtDoctNm2.Location = New System.Drawing.Point(531, 39)
        Me.txtDoctNm2.MaxLength = 30
        Me.txtDoctNm2.Name = "txtDoctNm2"
        Me.txtDoctNm2.ReadOnly = True
        Me.txtDoctNm2.Size = New System.Drawing.Size(72, 21)
        Me.txtDoctNm2.TabIndex = 11
        Me.txtDoctNm2.TabStop = False
        Me.txtDoctNm2.Tag = "DOCNM2"
        '
        'txtDoctNm1
        '
        Me.txtDoctNm1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoctNm1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtDoctNm1.Location = New System.Drawing.Point(531, 17)
        Me.txtDoctNm1.MaxLength = 30
        Me.txtDoctNm1.Name = "txtDoctNm1"
        Me.txtDoctNm1.ReadOnly = True
        Me.txtDoctNm1.Size = New System.Drawing.Size(72, 21)
        Me.txtDoctNm1.TabIndex = 8
        Me.txtDoctNm1.TabStop = False
        Me.txtDoctNm1.Tag = "DOCNM1"
        '
        'lblDoctor2
        '
        Me.lblDoctor2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDoctor2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDoctor2.ForeColor = System.Drawing.Color.White
        Me.lblDoctor2.Location = New System.Drawing.Point(326, 39)
        Me.lblDoctor2.Name = "lblDoctor2"
        Me.lblDoctor2.Size = New System.Drawing.Size(131, 21)
        Me.lblDoctor2.TabIndex = 131
        Me.lblDoctor2.Text = "2차 보고 담당의사 ID"
        Me.lblDoctor2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDoctorID2
        '
        Me.txtDoctorID2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoctorID2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtDoctorID2.Location = New System.Drawing.Point(458, 39)
        Me.txtDoctorID2.MaxLength = 30
        Me.txtDoctorID2.Name = "txtDoctorID2"
        Me.txtDoctorID2.Size = New System.Drawing.Size(72, 21)
        Me.txtDoctorID2.TabIndex = 10
        Me.txtDoctorID2.Tag = "DOCTORID2"
        '
        'lblDoctor1
        '
        Me.lblDoctor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDoctor1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDoctor1.ForeColor = System.Drawing.Color.White
        Me.lblDoctor1.Location = New System.Drawing.Point(326, 17)
        Me.lblDoctor1.Name = "lblDoctor1"
        Me.lblDoctor1.Size = New System.Drawing.Size(131, 21)
        Me.lblDoctor1.TabIndex = 129
        Me.lblDoctor1.Text = "1차 보고 담당의사 ID"
        Me.lblDoctor1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDoctorID1
        '
        Me.txtDoctorID1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoctorID1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtDoctorID1.Location = New System.Drawing.Point(458, 17)
        Me.txtDoctorID1.MaxLength = 30
        Me.txtDoctorID1.Name = "txtDoctorID1"
        Me.txtDoctorID1.Size = New System.Drawing.Size(72, 21)
        Me.txtDoctorID1.TabIndex = 7
        Me.txtDoctorID1.Tag = "DOCTORID1"
        '
        'lblDispSeq
        '
        Me.lblDispSeq.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDispSeq.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDispSeq.ForeColor = System.Drawing.Color.White
        Me.lblDispSeq.Location = New System.Drawing.Point(8, 38)
        Me.lblDispSeq.Name = "lblDispSeq"
        Me.lblDispSeq.Size = New System.Drawing.Size(100, 21)
        Me.lblDispSeq.TabIndex = 5
        Me.lblDispSeq.Text = "화면표시순서"
        Me.lblDispSeq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDispSeq
        '
        Me.txtDispSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispSeq.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtDispSeq.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDispSeq.Location = New System.Drawing.Point(109, 38)
        Me.txtDispSeq.MaxLength = 3
        Me.txtDispSeq.Name = "txtDispSeq"
        Me.txtDispSeq.Size = New System.Drawing.Size(36, 21)
        Me.txtDispSeq.TabIndex = 6
        Me.txtDispSeq.Tag = "DISPSEQ"
        '
        'lblLine
        '
        Me.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblLine.Location = New System.Drawing.Point(4, 68)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(756, 2)
        Me.lblLine.TabIndex = 0
        '
        'lblTOrdSlipNm
        '
        Me.lblTOrdSlipNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTOrdSlipNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTOrdSlipNm.ForeColor = System.Drawing.Color.White
        Me.lblTOrdSlipNm.Location = New System.Drawing.Point(8, 16)
        Me.lblTOrdSlipNm.Name = "lblTOrdSlipNm"
        Me.lblTOrdSlipNm.Size = New System.Drawing.Size(100, 21)
        Me.lblTOrdSlipNm.TabIndex = 0
        Me.lblTOrdSlipNm.Text = "검사처방슬립명"
        Me.lblTOrdSlipNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTOSlipNm
        '
        Me.txtTOSlipNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTOSlipNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTOSlipNm.Location = New System.Drawing.Point(109, 16)
        Me.txtTOSlipNm.MaxLength = 30
        Me.txtTOSlipNm.Name = "txtTOSlipNm"
        Me.txtTOSlipNm.Size = New System.Drawing.Size(192, 21)
        Me.txtTOSlipNm.TabIndex = 5
        Me.txtTOSlipNm.Tag = "TORDSLIPNM"
        '
        'grpCd
        '
        Me.grpCd.Controls.Add(Me.dtpUSTime)
        Me.grpCd.Controls.Add(Me.dtpUSDay)
        Me.grpCd.Controls.Add(Me.lblUSDayTime)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.lblTOSlipCd)
        Me.grpCd.Controls.Add(Me.txtTOSlipCd)
        Me.grpCd.Controls.Add(Me.txtUSDay)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 10)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        Me.grpCd.Text = "검사처방슬립 코드"
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(210, 15)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 2
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(189, 15)
        Me.dtpUSDay.Name = "dtpUSDay"
        Me.dtpUSDay.Size = New System.Drawing.Size(20, 21)
        Me.dtpUSDay.TabIndex = 1
        Me.dtpUSDay.TabStop = False
        Me.dtpUSDay.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'lblUSDayTime
        '
        Me.lblUSDayTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Location = New System.Drawing.Point(8, 15)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(102, 21)
        Me.lblUSDayTime.TabIndex = 7
        Me.lblUSDayTime.Text = "시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.btnUE.TabIndex = 4
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'lblTOSlipCd
        '
        Me.lblTOSlipCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTOSlipCd.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTOSlipCd.ForeColor = System.Drawing.Color.White
        Me.lblTOSlipCd.Location = New System.Drawing.Point(326, 15)
        Me.lblTOSlipCd.Name = "lblTOSlipCd"
        Me.lblTOSlipCd.Size = New System.Drawing.Size(130, 21)
        Me.lblTOSlipCd.TabIndex = 0
        Me.lblTOSlipCd.Text = "검사처방슬립코드"
        Me.lblTOSlipCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTOSlipCd
        '
        Me.txtTOSlipCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTOSlipCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTOSlipCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTOSlipCd.Location = New System.Drawing.Point(457, 15)
        Me.txtTOSlipCd.MaxLength = 3
        Me.txtTOSlipCd.Name = "txtTOSlipCd"
        Me.txtTOSlipCd.Size = New System.Drawing.Size(24, 21)
        Me.txtTOSlipCd.TabIndex = 3
        Me.txtTOSlipCd.Tag = "TORDSLIP"
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(111, 15)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(77, 21)
        Me.txtUSDay.TabIndex = 0
        Me.txtUSDay.Tag = ""
        '
        'FDF40
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF40"
        Me.Text = "[40] 검사처방슬립"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If Me.txtTOSlipCd.Text = "" Then Exit Sub

        Try
            Dim sMsg As String = "검사처방슬립코드   : " + Me.txtTOSlipCd.Text & vbCrLf
            sMsg += "검사처방슬립명     : " + Me.txtTOSlipNm.Text + vbCrLf
            sMsg += "을(를) 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mobjDAF.TransOSlipInfo_UE(Me.txtTOSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, sUeDate + sUeTime) Then
                MsgBox("해당 검사처방슬립정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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


    Private Sub FDF40_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub HLP1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoctor_HLP1.Click, btnDoctor_HLP2.Click
        Dim sFn As String = "spdDr_ButtonClicked"
        Try
            Dim iTop As Integer = 230
            Dim iLeft As Integer = 1450

            Dim sUsrId As String = ""
            Dim objName As Windows.Forms.TextBox
            Dim objCode As Windows.Forms.TextBox

            If CType(sender, Windows.Forms.Button).Name.IndexOf("1") >= 0 Then
                objCode = Me.txtDoctorID1
                objName = Me.txtDoctNm1
            Else
                objCode = Me.txtDoctorID2
                objName = Me.txtDoctNm2
            End If

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Usr_List(True, objCode.Text)

            objHelp.FormText = "담당의사 정보"
            objHelp.MaxRows = 15

            objHelp.AddField("USRID", "담당의사 ID", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("USRNM", "담당의사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(objCode)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)
            If alList.Count > 0 Then
                miSelectKey = 1

                objCode.Text = alList.Item(0).ToString.Split("|"c)(0)
                objName.Text = alList.Item(0).ToString.Split("|"c)(1)

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

    Private Sub sbEditUseDt_Edit(ByVal rsUseTag As String, ByVal rsUseDt As String)
        Dim sFn As String = "Sub sbEditUseDt_Edit"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            rsUseDt = rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", "")

            '> 사용중복 조사
            dt = mobjDAF.GetUsUeDupl_OSlip(Me.txtTOSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransOSlipInfo_UPD_US(Me.txtTOSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransOSlipInfo_UPD_UE(Me.txtTOSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
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
            dt = mobjDAF.GetUsUeCd_tordslip(Me.txtTOSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransOSlipInfo_DEL(Me.txtTOSlipCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

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
                .txtCd.Text = Me.txtTOSlipCd.Text
                .txtNm.Text = Me.txtTOSlipNm.Text

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

    Private Sub txtTOSlipCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTOSlipCd.KeyDown, txtTOSlipNm.KeyDown, txtDispSeq.KeyDown, txtDoctNm1.KeyDown, txtDoctNm2.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub
End Class
