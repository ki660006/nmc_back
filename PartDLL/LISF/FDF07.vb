'>>> [07] 위탁기관
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF07
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF07.vb, Class : FDF07" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_EXLAB
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents chkDelflg As System.Windows.Forms.CheckBox

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            dt = mobjDAF.GetUsUeCd_ExLab(Me.txtExLabCd.Text)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransExLabInfo_DEL(Me.txtExLabCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

            If bReturn Then
                MsgBox("해당 코드가 삭제되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox("해당 코드 삭제에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Function fnCollectItemTable_50(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_50() As LISAPP.ItemTableCollection"

        Try
            Dim it50 As New LISAPP.ItemTableCollection

            With it50
                .SetItemTable("EXLABCD", 1, 1, Me.txtExLabCd.Text)
                .SetItemTable("REGDT", 2, 1, rsRegDT)
                .SetItemTable("REGID", 3, 1, USER_INFO.USRID)
                .SetItemTable("EXLABNM", 4, 1, Me.txtExLabNm.Text)
                .SetItemTable("EXLABNMS", 5, 1, Me.txtExLabNmS.Text)
                .SetItemTable("EXLABNMD", 6, 1, Me.txtExLabNmD.Text)
                .SetItemTable("EXLABNMP", 7, 1, Me.txtExLabNmP.Text)
                .SetItemTable("EXLABNMBP", 8, 1, Me.txtExLabNmBP.Text)
                .SetItemTable("DELFLG", 9, 1, IIf(chkDelflg.Checked, "1", "0").ToString)
                .SetItemTable("REGIP", 10, 1, USER_INFO.LOCALIP)
            End With

            fnCollectItemTable_50 = it50
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
            Dim dt As DataTable = mobjDAF.GetNewRegDT

            If dt.Rows.Count > 0 Then
                fnGetSystemDT = dt.Rows(0).Item(0).ToString
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
            Dim it50 As New LISAPP.ItemTableCollection
            Dim iRegType50 As Integer = 0
            Dim sRegDT As String

            iRegType50 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it50 = fnCollectItemTable_50(sRegDT)

            If mobjDAF.TransExLabInfo(it50, iRegType50, Me.txtExLabCd.Text, USER_INFO.USRID) Then
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
            If Len(Me.txtExLabCd.Text.Trim) < 3 Then
                MsgBox("위탁기관코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtExLabNm.Text.Trim = "" Then
                MsgBox("위탁기관명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtExLabNmS.Text.Trim = "" Then
                MsgBox("위탁기관명(약어)를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtExLabNmD.Text.Trim = "" Then
                MsgBox("위탁기관명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtExLabNmP.Text.Trim = "" Then
                MsgBox("위탁기관명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " + errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsExLabCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_ExLab(rsExLabCd)
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_ExLab(ByVal rsExLabCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_ExLab()"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mobjDAF.GetExLabInfo(rsExLabCd)

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
                btnDel.Enabled = True
            Else
                btnDel.Enabled = False
            End If

            miSelectKey = 1

            sbInitialize_ErrProvider()

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

    Private Sub sbInitialize_Control(Optional ByVal riMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If riMode = 0 Then
                'tpg1 초기화
                Me.txtExLabCd.Text = "" : Me.btnDel.Visible = False
                Me.txtExLabNm.Text = "" : Me.txtExLabNmS.Text = "" : Me.txtExLabNmD.Text = "" : Me.txtExLabNmP.Text = "" : Me.txtExLabNmBP.Text = ""
                Me.txtUSDT.Text = "" : Me.txtUEDT.Text = "" : Me.txtRegDT.Text = "" : Me.txtRegID.Text = "" : Me.txtRegNm.Text = ""
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
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lbluserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents lblExLabCd As System.Windows.Forms.Label
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
    Friend WithEvents lblExLabNmS As System.Windows.Forms.Label
    Friend WithEvents lblExLabNmP As System.Windows.Forms.Label
    Friend WithEvents lblExLabNmD As System.Windows.Forms.Label
    Friend WithEvents lblExLabNm As System.Windows.Forms.Label
    Friend WithEvents lblExLabNmBP As System.Windows.Forms.Label
    Friend WithEvents txtExLabCd As System.Windows.Forms.TextBox
    Friend WithEvents txtExLabNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtExLabNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtExLabNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtExLabNm As System.Windows.Forms.TextBox
    Friend WithEvents txtExLabNmBP As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tpg1 = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtUEDT = New System.Windows.Forms.TextBox
        Me.lblUEDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.txtUSDT = New System.Windows.Forms.TextBox
        Me.lbluserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.lblUSDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.lblExLabNmBP = New System.Windows.Forms.Label
        Me.lblExLabNmS = New System.Windows.Forms.Label
        Me.txtExLabNmS = New System.Windows.Forms.TextBox
        Me.lblExLabNmP = New System.Windows.Forms.Label
        Me.txtExLabNmP = New System.Windows.Forms.TextBox
        Me.lblExLabNmD = New System.Windows.Forms.Label
        Me.txtExLabNmD = New System.Windows.Forms.TextBox
        Me.lblExLabNm = New System.Windows.Forms.Label
        Me.txtExLabNm = New System.Windows.Forms.TextBox
        Me.txtExLabNmBP = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.chkDelflg = New System.Windows.Forms.CheckBox
        Me.btnDel = New System.Windows.Forms.Button
        Me.lblExLabCd = New System.Windows.Forms.Label
        Me.txtExLabCd = New System.Windows.Forms.TextBox
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
        Me.tpg1.Controls.Add(Me.txtRegNm)
        Me.tpg1.Controls.Add(Me.txtUEDT)
        Me.tpg1.Controls.Add(Me.lblUEDT)
        Me.tpg1.Controls.Add(Me.txtRegDT)
        Me.tpg1.Controls.Add(Me.txtUSDT)
        Me.tpg1.Controls.Add(Me.lbluserNm)
        Me.tpg1.Controls.Add(Me.lblRegDT)
        Me.tpg1.Controls.Add(Me.lblUSDT)
        Me.tpg1.Controls.Add(Me.txtRegID)
        Me.tpg1.Controls.Add(Me.grpCdInfo1)
        Me.tpg1.Controls.Add(Me.grpCd)
        Me.tpg1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpg1.Location = New System.Drawing.Point(4, 21)
        Me.tpg1.Name = "tpg1"
        Me.tpg1.Size = New System.Drawing.Size(780, 576)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "위탁기관정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(702, 548)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 10
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUEDT.Location = New System.Drawing.Point(314, 548)
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
        Me.lblUEDT.Location = New System.Drawing.Point(216, 548)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(97, 21)
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
        Me.txtRegDT.Location = New System.Drawing.Point(510, 548)
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
        Me.txtUSDT.Location = New System.Drawing.Point(106, 548)
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUSDT.TabIndex = 0
        Me.txtUSDT.TabStop = False
        Me.txtUSDT.Tag = "USDT"
        '
        'lbluserNm
        '
        Me.lbluserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbluserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lbluserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbluserNm.ForeColor = System.Drawing.Color.Black
        Me.lbluserNm.Location = New System.Drawing.Point(616, 548)
        Me.lbluserNm.Name = "lbluserNm"
        Me.lbluserNm.Size = New System.Drawing.Size(85, 21)
        Me.lbluserNm.TabIndex = 0
        Me.lbluserNm.Text = "최종등록자"
        Me.lbluserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(424, 548)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(85, 21)
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
        Me.lblUSDT.Location = New System.Drawing.Point(8, 548)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(97, 21)
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
        Me.txtRegID.Location = New System.Drawing.Point(702, 548)
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
        Me.grpCdInfo1.Controls.Add(Me.lblExLabNmBP)
        Me.grpCdInfo1.Controls.Add(Me.lblExLabNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtExLabNmS)
        Me.grpCdInfo1.Controls.Add(Me.lblExLabNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtExLabNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblExLabNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtExLabNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblExLabNm)
        Me.grpCdInfo1.Controls.Add(Me.txtExLabNm)
        Me.grpCdInfo1.Controls.Add(Me.txtExLabNmBP)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 55)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 485)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "위탁기관정보"
        '
        'lblExLabNmBP
        '
        Me.lblExLabNmBP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblExLabNmBP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExLabNmBP.ForeColor = System.Drawing.Color.White
        Me.lblExLabNmBP.Location = New System.Drawing.Point(8, 114)
        Me.lblExLabNmBP.Name = "lblExLabNmBP"
        Me.lblExLabNmBP.Size = New System.Drawing.Size(123, 21)
        Me.lblExLabNmBP.TabIndex = 0
        Me.lblExLabNmBP.Text = "위탁기관명(바코드)"
        Me.lblExLabNmBP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblExLabNmS
        '
        Me.lblExLabNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblExLabNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExLabNmS.ForeColor = System.Drawing.Color.White
        Me.lblExLabNmS.Location = New System.Drawing.Point(8, 48)
        Me.lblExLabNmS.Name = "lblExLabNmS"
        Me.lblExLabNmS.Size = New System.Drawing.Size(123, 21)
        Me.lblExLabNmS.TabIndex = 0
        Me.lblExLabNmS.Text = "위탁기관명(약어)"
        Me.lblExLabNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExLabNmS
        '
        Me.txtExLabNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExLabNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtExLabNmS.Location = New System.Drawing.Point(132, 48)
        Me.txtExLabNmS.MaxLength = 10
        Me.txtExLabNmS.Name = "txtExLabNmS"
        Me.txtExLabNmS.Size = New System.Drawing.Size(128, 21)
        Me.txtExLabNmS.TabIndex = 5
        Me.txtExLabNmS.Tag = "EXLABNMS"
        '
        'lblExLabNmP
        '
        Me.lblExLabNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblExLabNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExLabNmP.ForeColor = System.Drawing.Color.White
        Me.lblExLabNmP.Location = New System.Drawing.Point(8, 92)
        Me.lblExLabNmP.Name = "lblExLabNmP"
        Me.lblExLabNmP.Size = New System.Drawing.Size(123, 21)
        Me.lblExLabNmP.TabIndex = 0
        Me.lblExLabNmP.Text = "위탁기관명(출력)"
        Me.lblExLabNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExLabNmP
        '
        Me.txtExLabNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExLabNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtExLabNmP.Location = New System.Drawing.Point(132, 92)
        Me.txtExLabNmP.MaxLength = 20
        Me.txtExLabNmP.Name = "txtExLabNmP"
        Me.txtExLabNmP.Size = New System.Drawing.Size(128, 21)
        Me.txtExLabNmP.TabIndex = 7
        Me.txtExLabNmP.Tag = "EXLABNMP"
        '
        'lblExLabNmD
        '
        Me.lblExLabNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblExLabNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExLabNmD.ForeColor = System.Drawing.Color.White
        Me.lblExLabNmD.Location = New System.Drawing.Point(8, 70)
        Me.lblExLabNmD.Name = "lblExLabNmD"
        Me.lblExLabNmD.Size = New System.Drawing.Size(123, 21)
        Me.lblExLabNmD.TabIndex = 0
        Me.lblExLabNmD.Text = "위탁기관명(화면)"
        Me.lblExLabNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExLabNmD
        '
        Me.txtExLabNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExLabNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtExLabNmD.Location = New System.Drawing.Point(132, 70)
        Me.txtExLabNmD.MaxLength = 20
        Me.txtExLabNmD.Name = "txtExLabNmD"
        Me.txtExLabNmD.Size = New System.Drawing.Size(128, 21)
        Me.txtExLabNmD.TabIndex = 6
        Me.txtExLabNmD.Tag = "EXLABNMD"
        '
        'lblExLabNm
        '
        Me.lblExLabNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblExLabNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExLabNm.ForeColor = System.Drawing.Color.White
        Me.lblExLabNm.Location = New System.Drawing.Point(8, 26)
        Me.lblExLabNm.Name = "lblExLabNm"
        Me.lblExLabNm.Size = New System.Drawing.Size(123, 21)
        Me.lblExLabNm.TabIndex = 0
        Me.lblExLabNm.Text = "위탁기관명"
        Me.lblExLabNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExLabNm
        '
        Me.txtExLabNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExLabNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtExLabNm.Location = New System.Drawing.Point(132, 26)
        Me.txtExLabNm.MaxLength = 20
        Me.txtExLabNm.Name = "txtExLabNm"
        Me.txtExLabNm.Size = New System.Drawing.Size(128, 21)
        Me.txtExLabNm.TabIndex = 4
        Me.txtExLabNm.Tag = "EXLABNM"
        '
        'txtExLabNmBP
        '
        Me.txtExLabNmBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExLabNmBP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtExLabNmBP.Location = New System.Drawing.Point(132, 114)
        Me.txtExLabNmBP.MaxLength = 10
        Me.txtExLabNmBP.Name = "txtExLabNmBP"
        Me.txtExLabNmBP.Size = New System.Drawing.Size(68, 21)
        Me.txtExLabNmBP.TabIndex = 8
        Me.txtExLabNmBP.Tag = "EXLABNMBP"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.chkDelflg)
        Me.grpCd.Controls.Add(Me.btnDel)
        Me.grpCd.Controls.Add(Me.lblExLabCd)
        Me.grpCd.Controls.Add(Me.txtExLabCd)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 5)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        Me.grpCd.Text = "위탁기관 코드"
        '
        'chkDelflg
        '
        Me.chkDelflg.AutoSize = True
        Me.chkDelflg.Location = New System.Drawing.Point(137, 20)
        Me.chkDelflg.Name = "chkDelflg"
        Me.chkDelflg.Size = New System.Drawing.Size(72, 16)
        Me.chkDelflg.TabIndex = 10
        Me.chkDelflg.Tag = "DELFLG"
        Me.chkDelflg.Text = "사용안함"
        Me.chkDelflg.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnDel.Enabled = False
        Me.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDel.ForeColor = System.Drawing.Color.White
        Me.btnDel.Location = New System.Drawing.Point(686, 12)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(72, 27)
        Me.btnDel.TabIndex = 9
        Me.btnDel.TabStop = False
        Me.btnDel.Text = "코드삭제"
        Me.btnDel.UseVisualStyleBackColor = False
        '
        'lblExLabCd
        '
        Me.lblExLabCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblExLabCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExLabCd.ForeColor = System.Drawing.Color.White
        Me.lblExLabCd.Location = New System.Drawing.Point(8, 17)
        Me.lblExLabCd.Name = "lblExLabCd"
        Me.lblExLabCd.Size = New System.Drawing.Size(83, 21)
        Me.lblExLabCd.TabIndex = 0
        Me.lblExLabCd.Text = "위탁기관코드"
        Me.lblExLabCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExLabCd
        '
        Me.txtExLabCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExLabCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtExLabCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtExLabCd.Location = New System.Drawing.Point(92, 17)
        Me.txtExLabCd.MaxLength = 3
        Me.txtExLabCd.Name = "txtExLabCd"
        Me.txtExLabCd.Size = New System.Drawing.Size(39, 21)
        Me.txtExLabCd.TabIndex = 3
        Me.txtExLabCd.Tag = "EXLABCD"
        '
        'FDF07
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF07"
        Me.Text = "[07] 위탁기관"
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

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim sFn As String = "Private Sub btnDel_Click"

        If Me.txtExLabCd.Text = "" Then Return

        Try
            Dim sMsg As String = ""

            sMsg = ""
            sMsg += Me.lblExLabCd.Text + " : " + Me.txtExLabCd.Text + vbCrLf
            sMsg += Me.lblExLabNm.Text + " : " + Me.lblExLabNm.Text + vbCrLf + vbCrLf
            sMsg += "의 " + "코드를 삭제 하시겠습니까?" + vbCrLf + vbCrLf + vbCrLf
            sMsg += ">>> " + Me.btnDel.Text + "는 주의를 요하는 작업이므로 신중히 실행하시기 바랍니다!!" + vbTab + vbCrLf

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo, Me.btnDel.Text + " 확인") = MsgBoxResult.No Then Return

            sbEditUseDt_Del()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub txtExLabNm_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtExLabNm.Validating
        If miSelectKey = 1 Then Exit Sub


        If Me.txtExLabNmS.Text.Trim = "" Then
            If Me.txtExLabNm.Text.Length > Me.txtExLabNmS.MaxLength Then
                Me.txtExLabNmS.Text = Me.txtExLabNm.Text.Substring(0, Me.txtExLabNmS.MaxLength)
            Else
                Me.txtExLabNmS.Text = Me.txtExLabNm.Text
            End If
        End If

        If Me.txtExLabNmD.Text.Trim = "" Then
            If Me.txtExLabNm.Text.Length > Me.txtExLabNmD.MaxLength Then
                Me.txtExLabNmD.Text = Me.txtExLabNm.Text.Substring(0, txtExLabNmD.MaxLength)
            Else
                Me.txtExLabNmD.Text = Me.txtExLabNm.Text
            End If
        End If

        If Me.txtExLabNmP.Text.Trim = "" Then
            If Me.txtExLabNm.Text.Length > Me.txtExLabNmP.MaxLength Then
                Me.txtExLabNmP.Text = Me.txtExLabNm.Text.Substring(0, Me.txtExLabNmP.MaxLength)
            Else
                Me.txtExLabNmP.Text = txtExLabNm.Text
            End If
        End If
    End Sub

    Private Sub FDF07_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtExLabCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtExLabCd.KeyDown, txtExLabNm.KeyDown, txtExLabNmBP.KeyDown, txtExLabNmD.KeyDown, txtExLabNmP.KeyDown, txtExLabNmS.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

End Class
