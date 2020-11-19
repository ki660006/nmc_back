'>>> [04] 검체그룹
Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF04
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF00.vb, Class : FDF00" + vbTab
    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_SPCGRP

    Private Function fnCollectItemTable_31(ByVal asRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_31(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it31 As New LISAPP.ItemTableCollection
            Dim iCnt As Integer = 0

            With spdSpc
                For i As Integer = 1 To .MaxRows
                    .Col = .GetColFromID("CHK") : .Row = i : Dim sChk As String = .Text

                    If sChk = "1" Then
                        iCnt += 1

                        it31.SetItemTable("SPCGRPCD", 1, iCnt, txtSpcGrpCd.Text)

                        .Col = .GetColFromID("SPCCD") : .Row = i
                        it31.SetItemTable("SPCCD", 2, iCnt, .Text, OracleDbType.Varchar2)

                        it31.SetItemTable("USDT", 3, iCnt, asRegDT)
                        it31.SetItemTable("UEDT", 4, iCnt, msUEDT)
                        it31.SetItemTable("REGDT", 5, iCnt, asRegDT)
                        it31.SetItemTable("REGID", 6, iCnt, USER_INFO.USRID)
                        it31.SetItemTable("SPCGRPNM", 7, iCnt, txtSpcGrpNm.Text)
                        it31.SetItemTable("SPCGRPNMS", 8, iCnt, txtSpcGrpNmS.Text)
                        it31.SetItemTable("SPCGRPNMD", 9, iCnt, txtSpcGrpNmD.Text)
                        it31.SetItemTable("SPCGRPNMP", 10, iCnt, txtSpcGrpNmP.Text)
                        it31.SetItemTable("REGIP", 11, iCnt, USER_INFO.LOCALIP)
                    End If
                Next
            End With

            fnCollectItemTable_31 = it31
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

    Private Function fnFindConflict(ByVal asSpcGrpCd As String) As String
        Dim sFn As String = ""

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetRecentSpcGrpInfo(asSpcGrpCd)

            If DTable.Rows.Count > 0 Then
                Return "검체그룹코드(" + DTable.Rows(0).Item(0).ToString + ")는 이미 사용 중입니다." + vbCrLf + vbCrLf + _
                       "확인하여 주십시요!!"
            Else
                Return ""
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
            Return ""
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
        Dim sFn As String = "Public Function fnRegSpc() As Boolean"

        Try
            Dim it31 As New LISAPP.ItemTableCollection
            Dim iRegType31 As Integer = 0
            Dim sRegDT As String

            iRegType31 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it31 = fnCollectItemTable_31(sRegDT)

            If mobjDAF.TransSpcGrpInfo(it31, iRegType31, _
                                        txtSpcGrpCd.Text, USER_INFO.USRID) Then
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
            If txtSpcGrpCd.Text = "" Then
                MsgBox("검체그룹코드를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtSpcGrpNm.Text = "" Then
                MsgBox("검체그룹명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtSpcGrpNmS.Text = "" Then
                MsgBox("검체그룹명(약어)를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtSpcGrpNmD.Text = "" Then
                MsgBox("검체그룹명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtSpcGrpNmP.Text = "" Then
                MsgBox("검체그룹명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtSpcGrpCd.Text)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            Dim iChk As Integer = 0

            With spdSpc
                For i As Integer = 1 To .MaxRows
                    .Col = .GetColFromID("CHK") : .Row = i

                    If .Text = "1" Then
                        iChk = 1

                        Exit For
                    End If
                Next
            End With

            If iChk = 0 Then
                MsgBox("검체그룹에 포함할 검체를 선택해 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
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

    Public Sub sbDisplayCdDetail(ByVal asSpcGrpCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_SpcGrp(asSpcGrpCd)

            sbDisplayCdDetail_SpcGrpSpc(asSpcGrpCd)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_SpcGrp(ByVal asSpcGrpCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_SpcGrp()"
        Dim iCol As Integer = 0

        Try
            Dim DTable As New DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            DTable = mobjDAF.GetSpcGrpInfo(asSpcGrpCd)

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
                Next
            Else
                Exit Sub
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_SpcGrpSpc(ByVal asSpcGrpCd As String)
        Dim sFn As String = ""

        Try
            Dim DTable As DataTable
            Dim iCol As Integer = 0

            DTable = mobjDAF.GetSpcGrpSpcInfo(asSpcGrpCd)

            '스프레드 초기화
            'sbInitialize_spdSkill()

            If DTable.Rows.Count > 0 Then
                With spdSpc
                    .ReDraw = False

                    .MaxRows = DTable.Rows.Count

                    For i As Integer = 0 To DTable.Rows.Count - 1
                        For j As Integer = 0 To DTable.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(DTable.Columns(j).ColumnName)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = DTable.Rows(i).Item(j).ToString
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
                'tpgSpc1 초기화

                txtSpcGrpCd.Text = "" : btnUE.Visible = False

                txtSpcGrpNm.Text = "" : txtSpcGrpNmS.Text = "" : txtSpcGrpNmP.Text = "" : txtSpcGrpNmD.Text = ""

                txtBPcnt.Text = ""

                txtSpcGrpCd0.Text = "" : txtRegDT.Text = "" : txtRegID.Text = ""

                sbDisplayCdDetail_SpcGrpSpc("")
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

    Private Sub sbInitialize_spdSpc()
        With spdSpc
            .MaxRows = 0
        End With
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
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
    Friend WithEvents lblTSectCd As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents spdSpc As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSpcGrpCd0 As System.Windows.Forms.TextBox
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblRegID As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSpcGrpNmP As System.Windows.Forms.Label
    Friend WithEvents txtSpcGrpNmP As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcGrpNmD As System.Windows.Forms.Label
    Friend WithEvents txtSpcGrpNmD As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcGrpNmS As System.Windows.Forms.Label
    Friend WithEvents txtSpcGrpNmS As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcGrpNm As System.Windows.Forms.Label
    Friend WithEvents txtSpcGrpNm As System.Windows.Forms.TextBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents lblWrGrpCd As System.Windows.Forms.Label
    Friend WithEvents txtSpcGrpCd As System.Windows.Forms.TextBox
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBPcnt As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF04))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tpg1 = New System.Windows.Forms.TabPage
        Me.lblTSectCd = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.spdSpc = New AxFPSpreadADO.AxfpSpread
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSpcGrpCd0 = New System.Windows.Forms.TextBox
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblRegID = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBPcnt = New System.Windows.Forms.TextBox
        Me.lblSpcGrpNmP = New System.Windows.Forms.Label
        Me.txtSpcGrpNmP = New System.Windows.Forms.TextBox
        Me.lblSpcGrpNmD = New System.Windows.Forms.Label
        Me.txtSpcGrpNmD = New System.Windows.Forms.TextBox
        Me.lblSpcGrpNmS = New System.Windows.Forms.Label
        Me.txtSpcGrpNmS = New System.Windows.Forms.TextBox
        Me.lblSpcGrpNm = New System.Windows.Forms.Label
        Me.txtSpcGrpNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.lblWrGrpCd = New System.Windows.Forms.Label
        Me.txtSpcGrpCd = New System.Windows.Forms.TextBox
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tpg1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdSpc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCdInfo1.SuspendLayout()
        Me.grpCd.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.Controls.Add(Me.tclSpc)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 119
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
        Me.tpg1.Controls.Add(Me.lblTSectCd)
        Me.tpg1.Controls.Add(Me.GroupBox1)
        Me.tpg1.Controls.Add(Me.Label1)
        Me.tpg1.Controls.Add(Me.txtSpcGrpCd0)
        Me.tpg1.Controls.Add(Me.txtRegDT)
        Me.tpg1.Controls.Add(Me.lblRegID)
        Me.tpg1.Controls.Add(Me.lblRegDT)
        Me.tpg1.Controls.Add(Me.txtRegID)
        Me.tpg1.Controls.Add(Me.grpCdInfo1)
        Me.tpg1.Controls.Add(Me.grpCd)
        Me.tpg1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpg1.Location = New System.Drawing.Point(4, 21)
        Me.tpg1.Name = "tpg1"
        Me.tpg1.Size = New System.Drawing.Size(780, 576)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "검체그룹정보"
        '
        'lblTSectCd
        '
        Me.lblTSectCd.Location = New System.Drawing.Point(20, 548)
        Me.lblTSectCd.Name = "lblTSectCd"
        Me.lblTSectCd.Size = New System.Drawing.Size(88, 16)
        Me.lblTSectCd.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.spdSpc)
        Me.GroupBox1.Location = New System.Drawing.Point(308, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(464, 486)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "검체그룹 설정"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(48, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(368, 20)
        Me.Label4.TabIndex = 119
        Me.Label4.Text = "검체그룹에 포함할 검체 설정"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdSpc
        '
        Me.spdSpc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdSpc.Location = New System.Drawing.Point(48, 40)
        Me.spdSpc.Name = "spdSpc"
        Me.spdSpc.OcxState = CType(resources.GetObject("spdSpc.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSpc.Size = New System.Drawing.Size(368, 435)
        Me.spdSpc.TabIndex = 118
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(16, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "검체그룹코드"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Visible = False
        '
        'txtSpcGrpCd0
        '
        Me.txtSpcGrpCd0.BackColor = System.Drawing.Color.LightGray
        Me.txtSpcGrpCd0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcGrpCd0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcGrpCd0.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcGrpCd0.Location = New System.Drawing.Point(117, 44)
        Me.txtSpcGrpCd0.Name = "txtSpcGrpCd0"
        Me.txtSpcGrpCd0.ReadOnly = True
        Me.txtSpcGrpCd0.Size = New System.Drawing.Size(20, 21)
        Me.txtSpcGrpCd0.TabIndex = 0
        Me.txtSpcGrpCd0.TabStop = False
        Me.txtSpcGrpCd0.Tag = "SPCGRPCD"
        Me.txtSpcGrpCd0.Visible = False
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
        'lblRegID
        '
        Me.lblRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegID.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegID.ForeColor = System.Drawing.Color.Black
        Me.lblRegID.Location = New System.Drawing.Point(620, 544)
        Me.lblRegID.Name = "lblRegID"
        Me.lblRegID.Size = New System.Drawing.Size(84, 20)
        Me.lblRegID.TabIndex = 0
        Me.lblRegID.Text = "최종등록자ID"
        Me.lblRegID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(428, 544)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 20)
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
        Me.txtRegID.Location = New System.Drawing.Point(704, 544)
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
        Me.grpCdInfo1.Controls.Add(Me.Label2)
        Me.grpCdInfo1.Controls.Add(Me.txtBPcnt)
        Me.grpCdInfo1.Controls.Add(Me.lblSpcGrpNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtSpcGrpNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblSpcGrpNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtSpcGrpNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblSpcGrpNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtSpcGrpNmS)
        Me.grpCdInfo1.Controls.Add(Me.lblSpcGrpNm)
        Me.grpCdInfo1.Controls.Add(Me.txtSpcGrpNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 52)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(304, 486)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "사용자기본정보"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(11, 200)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(155, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "기본 BARCODE 출력 매수"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBPcnt
        '
        Me.txtBPcnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBPcnt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBPcnt.Location = New System.Drawing.Point(172, 200)
        Me.txtBPcnt.MaxLength = 2
        Me.txtBPcnt.Name = "txtBPcnt"
        Me.txtBPcnt.Size = New System.Drawing.Size(88, 21)
        Me.txtBPcnt.TabIndex = 8
        Me.txtBPcnt.Tag = "BPCNT"
        '
        'lblSpcGrpNmP
        '
        Me.lblSpcGrpNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcGrpNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcGrpNmP.ForeColor = System.Drawing.Color.White
        Me.lblSpcGrpNmP.Location = New System.Drawing.Point(11, 160)
        Me.lblSpcGrpNmP.Name = "lblSpcGrpNmP"
        Me.lblSpcGrpNmP.Size = New System.Drawing.Size(115, 20)
        Me.lblSpcGrpNmP.TabIndex = 5
        Me.lblSpcGrpNmP.Text = "검체그룹명(출력)"
        Me.lblSpcGrpNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcGrpNmP
        '
        Me.txtSpcGrpNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcGrpNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcGrpNmP.Location = New System.Drawing.Point(132, 160)
        Me.txtSpcGrpNmP.MaxLength = 20
        Me.txtSpcGrpNmP.Name = "txtSpcGrpNmP"
        Me.txtSpcGrpNmP.Size = New System.Drawing.Size(128, 21)
        Me.txtSpcGrpNmP.TabIndex = 6
        Me.txtSpcGrpNmP.Tag = "SPCGRPNMP"
        '
        'lblSpcGrpNmD
        '
        Me.lblSpcGrpNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcGrpNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcGrpNmD.ForeColor = System.Drawing.Color.White
        Me.lblSpcGrpNmD.Location = New System.Drawing.Point(11, 80)
        Me.lblSpcGrpNmD.Name = "lblSpcGrpNmD"
        Me.lblSpcGrpNmD.Size = New System.Drawing.Size(115, 20)
        Me.lblSpcGrpNmD.TabIndex = 3
        Me.lblSpcGrpNmD.Text = "검체그룹명(화면)"
        Me.lblSpcGrpNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcGrpNmD
        '
        Me.txtSpcGrpNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcGrpNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcGrpNmD.Location = New System.Drawing.Point(132, 80)
        Me.txtSpcGrpNmD.MaxLength = 20
        Me.txtSpcGrpNmD.Name = "txtSpcGrpNmD"
        Me.txtSpcGrpNmD.Size = New System.Drawing.Size(128, 21)
        Me.txtSpcGrpNmD.TabIndex = 4
        Me.txtSpcGrpNmD.Tag = "SPCGRPNMD"
        '
        'lblSpcGrpNmS
        '
        Me.lblSpcGrpNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcGrpNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcGrpNmS.ForeColor = System.Drawing.Color.White
        Me.lblSpcGrpNmS.Location = New System.Drawing.Point(11, 120)
        Me.lblSpcGrpNmS.Name = "lblSpcGrpNmS"
        Me.lblSpcGrpNmS.Size = New System.Drawing.Size(115, 20)
        Me.lblSpcGrpNmS.TabIndex = 0
        Me.lblSpcGrpNmS.Text = "검체그룹명(약어)"
        Me.lblSpcGrpNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcGrpNmS
        '
        Me.txtSpcGrpNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcGrpNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcGrpNmS.Location = New System.Drawing.Point(132, 120)
        Me.txtSpcGrpNmS.MaxLength = 10
        Me.txtSpcGrpNmS.Name = "txtSpcGrpNmS"
        Me.txtSpcGrpNmS.Size = New System.Drawing.Size(128, 21)
        Me.txtSpcGrpNmS.TabIndex = 2
        Me.txtSpcGrpNmS.Tag = "SPCGRPNMS"
        '
        'lblSpcGrpNm
        '
        Me.lblSpcGrpNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcGrpNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcGrpNm.ForeColor = System.Drawing.Color.White
        Me.lblSpcGrpNm.Location = New System.Drawing.Point(11, 40)
        Me.lblSpcGrpNm.Name = "lblSpcGrpNm"
        Me.lblSpcGrpNm.Size = New System.Drawing.Size(115, 20)
        Me.lblSpcGrpNm.TabIndex = 0
        Me.lblSpcGrpNm.Text = "검체그룹명"
        Me.lblSpcGrpNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcGrpNm
        '
        Me.txtSpcGrpNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcGrpNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcGrpNm.Location = New System.Drawing.Point(132, 40)
        Me.txtSpcGrpNm.MaxLength = 20
        Me.txtSpcGrpNm.Name = "txtSpcGrpNm"
        Me.txtSpcGrpNm.Size = New System.Drawing.Size(128, 21)
        Me.txtSpcGrpNm.TabIndex = 1
        Me.txtSpcGrpNm.Tag = "SPCGRPNM"
        '
        'grpCd
        '
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.lblWrGrpCd)
        Me.grpCd.Controls.Add(Me.txtSpcGrpCd)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        Me.grpCd.Text = "검체그룹 코드"
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.IndianRed
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.btnUE.Location = New System.Drawing.Point(692, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(64, 24)
        Me.btnUE.TabIndex = 6
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'lblWrGrpCd
        '
        Me.lblWrGrpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWrGrpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWrGrpCd.ForeColor = System.Drawing.Color.White
        Me.lblWrGrpCd.Location = New System.Drawing.Point(8, 16)
        Me.lblWrGrpCd.Name = "lblWrGrpCd"
        Me.lblWrGrpCd.Size = New System.Drawing.Size(84, 20)
        Me.lblWrGrpCd.TabIndex = 0
        Me.lblWrGrpCd.Text = "검체그룹코드"
        Me.lblWrGrpCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcGrpCd
        '
        Me.txtSpcGrpCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcGrpCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSpcGrpCd.Location = New System.Drawing.Point(109, 16)
        Me.txtSpcGrpCd.MaxLength = 2
        Me.txtSpcGrpCd.Name = "txtSpcGrpCd"
        Me.txtSpcGrpCd.Size = New System.Drawing.Size(20, 21)
        Me.txtSpcGrpCd.TabIndex = 1
        Me.txtSpcGrpCd.Tag = "SPCGRPCD"
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF04
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF04"
        Me.Text = "[04] 검체그룹"
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tpg1.ResumeLayout(False)
        Me.tpg1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.spdSpc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

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
                Else
                    .Col = 1 : .Col2 = .MaxCols : .Row = e.row : .Row2 = e.row
                    .BlockMode = True
                    .BackColor = System.Drawing.Color.White
                    .BlockMode = False
                End If
            End With
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
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
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            spdSpc.ReDraw = True
            miSelectKey = 0
        End Try
    End Sub

    Private Sub txtSpcGrpNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSpcGrpNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If txtSpcGrpNmS.Text.Trim = "" Then
            If txtSpcGrpNm.Text.Length > txtSpcGrpNmS.MaxLength Then
                txtSpcGrpNmS.Text = txtSpcGrpNm.Text.Substring(0, txtSpcGrpNmS.MaxLength)
            Else
                txtSpcGrpNmS.Text = txtSpcGrpNm.Text
            End If
        End If

        If txtSpcGrpNmD.Text.Trim = "" Then
            If txtSpcGrpNm.Text.Length > txtSpcGrpNmD.MaxLength Then
                txtSpcGrpNmD.Text = txtSpcGrpNm.Text.Substring(0, txtSpcGrpNmD.MaxLength)
            Else
                txtSpcGrpNmD.Text = txtSpcGrpNm.Text
            End If
        End If

        If txtSpcGrpNmP.Text.Trim = "" Then
            If txtSpcGrpNm.Text.Length > txtSpcGrpNmP.MaxLength Then
                txtSpcGrpNmP.Text = txtSpcGrpNm.Text.Substring(0, txtSpcGrpNmP.MaxLength)
            Else
                txtSpcGrpNmP.Text = txtSpcGrpNm.Text
            End If
        End If
    End Sub

    Private Sub FDF04_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

End Class
