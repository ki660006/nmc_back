'>>> [15] 배양균속
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF15
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF15.vb, Class : FDF15" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_BACGEN
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents txRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtDispSeq As System.Windows.Forms.TextBox
    Friend WithEvents lblDispSeq As System.Windows.Forms.Label

    Private Function fnCollectItemTable_220(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_220(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it220 As New LISAPP.ItemTableCollection

            With it220
                .SetItemTable("BACGENCD", 1, 1, Me.txtBacgenCd.Text)
                .SetItemTable("REGDT", 2, 1, rsRegDT)
                .SetItemTable("REGID", 3, 1, USER_INFO.USRID)
                .SetItemTable("BACGENNM", 4, 1, Me.txtBacgenNm.Text)
                .SetItemTable("BACGENNMS", 5, 1, Me.txtBacgenNmS.Text)
                .SetItemTable("BACGENNMD", 6, 1, Me.txtBacgenNmD.Text)
                .SetItemTable("BACGENNMP", 7, 1, Me.txtBacgenNmP.Text)
                .SetItemTable("DISPSEQ", 8, 1, Me.txtDispSeq.Text)
                .SetItemTable("REGIP", 9, 1, USER_INFO.LOCALIP)
            End With

            Return it220
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return New LISAPP.ItemTableCollection

        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it220 As New LISAPP.ItemTableCollection
            Dim iRegType220 As Integer = 0
            Dim sRegDT As String

            iRegType220 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it220 = fnCollectItemTable_220(sRegDT)

            If mobjDAF.TransBacgenInfo(it220, iRegType220, Me.txtBacgenCd.Text, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsBacgenCd As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable= mobjDAF.GetRecentBacgenInfo(rsBacgenCd)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString + "인 동일 " + Me.lblBacgenCd.Text + "가 존재합니다." + vbCrLf + vbCrLf

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
            Dim dt As DataTable= mobjDAF.GetNewRegDT

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
            If Len(Me.txtBacgenCd.Text.Trim) < 1 Then
                MsgBox("배양균속코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtBacgenNm.Text.Trim) < 1 Then
                MsgBox("배양균속명을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtBacgenNmS.Text.Trim) < 1 Then
                MsgBox("배양균속명(약어)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtBacgenNmD.Text.Trim) < 1 Then
                MsgBox("배양균속명(화면)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtBacgenNmP.Text.Trim) < 1 Then
                MsgBox("배양균속명(출력)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtBacgenCd.Text)

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

            fnValidate = True

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsBacgenCd As String, ByVal rsModId As String, ByVal rsModDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1
            sbDisplayCdDetail_Bacgen(1, rsBacgenCd, rsModId, rsModDt)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Bacgen(ByVal riMode As Integer, ByVal rsBacgenCd As String, ByVal rsModId As String, ByVal rsModDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Bacgen(String, String, String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As New DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            If rsModId = "" Or rsModDt = "" Then

                dt = mobjDAF.GetBacgenInfo(rsBacgenCd)
            Else
                dt = mobjDAF.GetBacgenInfo(rsBacgenCd, rsModDt.Replace("-", "").Replace(":", "").Replace(" ", ""), rsModId)
            End If

            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            sbInitialize()

            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()

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

    Private Sub sbInitialize_Control(Optional ByVal riMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If riMode = 0 Then
                Me.txtBacgenCd.Text = "" : Me.btnUE.Visible = False : Me.txRegNm.Text = ""
                Me.txtBacgenNm.Text = "" : Me.txtBacgenNmS.Text = "" : Me.txtBacgenNmD.Text = "" : Me.txtBacgenNmP.Text = "" : Me.txtDispSeq.Text = ""
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
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
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
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
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents tbcTpg1 As System.Windows.Forms.TabPage
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents txtBacgenNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtBacgenNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtBacgenNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtBacgenNm As System.Windows.Forms.TextBox
    Friend WithEvents txtBacgenCd As System.Windows.Forms.TextBox
    Friend WithEvents lblBacgenCd As System.Windows.Forms.Label
    Friend WithEvents lblBacgenNm As System.Windows.Forms.Label
    Friend WithEvents lblBacgenNmS As System.Windows.Forms.Label
    Friend WithEvents lblBacgenNmP As System.Windows.Forms.Label
    Friend WithEvents lblBacgenNmD As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tbcTpg1 = New System.Windows.Forms.TabPage
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.txtDispSeq = New System.Windows.Forms.TextBox
        Me.lblDispSeq = New System.Windows.Forms.Label
        Me.lblBacgenNmS = New System.Windows.Forms.Label
        Me.txtBacgenNmS = New System.Windows.Forms.TextBox
        Me.lblLine = New System.Windows.Forms.Label
        Me.lblBacgenNmP = New System.Windows.Forms.Label
        Me.txtBacgenNmP = New System.Windows.Forms.TextBox
        Me.lblBacgenNmD = New System.Windows.Forms.Label
        Me.txtBacgenNmD = New System.Windows.Forms.TextBox
        Me.lblBacgenNm = New System.Windows.Forms.Label
        Me.txtBacgenNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.txtBacgenCd = New System.Windows.Forms.TextBox
        Me.lblBacgenCd = New System.Windows.Forms.Label
        Me.txRegNm = New System.Windows.Forms.TextBox
        Me.txtRegID = New System.Windows.Forms.TextBox
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg1.SuspendLayout()
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
        Me.pnlTop.Size = New System.Drawing.Size(928, 606)
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
        Me.tclSpc.Size = New System.Drawing.Size(924, 602)
        Me.tclSpc.TabIndex = 0
        Me.tclSpc.TabStop = False
        '
        'tbcTpg1
        '
        Me.tbcTpg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg1.Controls.Add(Me.txtModNm)
        Me.tbcTpg1.Controls.Add(Me.lblModNm)
        Me.tbcTpg1.Controls.Add(Me.txtRegDT)
        Me.tbcTpg1.Controls.Add(Me.txtModDT)
        Me.tbcTpg1.Controls.Add(Me.lblModDT)
        Me.tbcTpg1.Controls.Add(Me.lblUserNm)
        Me.tbcTpg1.Controls.Add(Me.lblRegDT)
        Me.tbcTpg1.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg1.Controls.Add(Me.grpCd)
        Me.tbcTpg1.Controls.Add(Me.txRegNm)
        Me.tbcTpg1.Controls.Add(Me.txtRegID)
        Me.tbcTpg1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcTpg1.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg1.Name = "tbcTpg1"
        Me.tbcTpg1.Size = New System.Drawing.Size(916, 577)
        Me.tbcTpg1.TabIndex = 0
        Me.tbcTpg1.Text = "배양균속정보"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(299, 547)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 187
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(214, 547)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 186
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(507, 547)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 0
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(94, 547)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 185
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(9, 547)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(84, 21)
        Me.lblModDT.TabIndex = 184
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(616, 547)
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
        Me.lblRegDT.Location = New System.Drawing.Point(422, 547)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.txtDispSeq)
        Me.grpCdInfo1.Controls.Add(Me.lblDispSeq)
        Me.grpCdInfo1.Controls.Add(Me.lblBacgenNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtBacgenNmS)
        Me.grpCdInfo1.Controls.Add(Me.lblLine)
        Me.grpCdInfo1.Controls.Add(Me.lblBacgenNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtBacgenNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblBacgenNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtBacgenNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblBacgenNm)
        Me.grpCdInfo1.Controls.Add(Me.txtBacgenNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 57)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 483)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "배양균속정보"
        '
        'txtDispSeq
        '
        Me.txtDispSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispSeq.Font = New System.Drawing.Font("굴림체", 9.0!)
        Me.txtDispSeq.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDispSeq.Location = New System.Drawing.Point(118, 126)
        Me.txtDispSeq.MaxLength = 3
        Me.txtDispSeq.Name = "txtDispSeq"
        Me.txtDispSeq.Size = New System.Drawing.Size(68, 21)
        Me.txtDispSeq.TabIndex = 131
        Me.txtDispSeq.Tag = "DISPSEQ"
        '
        'lblDispSeq
        '
        Me.lblDispSeq.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDispSeq.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDispSeq.ForeColor = System.Drawing.Color.White
        Me.lblDispSeq.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDispSeq.Location = New System.Drawing.Point(6, 126)
        Me.lblDispSeq.Name = "lblDispSeq"
        Me.lblDispSeq.Size = New System.Drawing.Size(111, 21)
        Me.lblDispSeq.TabIndex = 132
        Me.lblDispSeq.Text = "정렬순서"
        Me.lblDispSeq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBacgenNmS
        '
        Me.lblBacgenNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacgenNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacgenNmS.ForeColor = System.Drawing.Color.White
        Me.lblBacgenNmS.Location = New System.Drawing.Point(8, 38)
        Me.lblBacgenNmS.Name = "lblBacgenNmS"
        Me.lblBacgenNmS.Size = New System.Drawing.Size(111, 21)
        Me.lblBacgenNmS.TabIndex = 5
        Me.lblBacgenNmS.Text = "배양균속명(약어)"
        Me.lblBacgenNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBacgenNmS
        '
        Me.txtBacgenNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacgenNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBacgenNmS.Location = New System.Drawing.Point(120, 38)
        Me.txtBacgenNmS.MaxLength = 30
        Me.txtBacgenNmS.Name = "txtBacgenNmS"
        Me.txtBacgenNmS.Size = New System.Drawing.Size(364, 21)
        Me.txtBacgenNmS.TabIndex = 4
        Me.txtBacgenNmS.Tag = "BACGENNMS"
        '
        'lblLine
        '
        Me.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblLine.Location = New System.Drawing.Point(4, 114)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(756, 2)
        Me.lblLine.TabIndex = 0
        '
        'lblBacgenNmP
        '
        Me.lblBacgenNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacgenNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacgenNmP.ForeColor = System.Drawing.Color.White
        Me.lblBacgenNmP.Location = New System.Drawing.Point(8, 82)
        Me.lblBacgenNmP.Name = "lblBacgenNmP"
        Me.lblBacgenNmP.Size = New System.Drawing.Size(111, 21)
        Me.lblBacgenNmP.TabIndex = 0
        Me.lblBacgenNmP.Text = "배양균속명(출력)"
        Me.lblBacgenNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBacgenNmP
        '
        Me.txtBacgenNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacgenNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBacgenNmP.Location = New System.Drawing.Point(120, 82)
        Me.txtBacgenNmP.MaxLength = 60
        Me.txtBacgenNmP.Name = "txtBacgenNmP"
        Me.txtBacgenNmP.Size = New System.Drawing.Size(364, 21)
        Me.txtBacgenNmP.TabIndex = 6
        Me.txtBacgenNmP.Tag = "BACGENNMP"
        '
        'lblBacgenNmD
        '
        Me.lblBacgenNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacgenNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacgenNmD.ForeColor = System.Drawing.Color.White
        Me.lblBacgenNmD.Location = New System.Drawing.Point(8, 60)
        Me.lblBacgenNmD.Name = "lblBacgenNmD"
        Me.lblBacgenNmD.Size = New System.Drawing.Size(111, 21)
        Me.lblBacgenNmD.TabIndex = 0
        Me.lblBacgenNmD.Text = "배양균속명(화면)"
        Me.lblBacgenNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBacgenNmD
        '
        Me.txtBacgenNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacgenNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBacgenNmD.Location = New System.Drawing.Point(120, 60)
        Me.txtBacgenNmD.MaxLength = 60
        Me.txtBacgenNmD.Name = "txtBacgenNmD"
        Me.txtBacgenNmD.Size = New System.Drawing.Size(364, 21)
        Me.txtBacgenNmD.TabIndex = 5
        Me.txtBacgenNmD.Tag = "BACGENNMD"
        '
        'lblBacgenNm
        '
        Me.lblBacgenNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacgenNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacgenNm.ForeColor = System.Drawing.Color.White
        Me.lblBacgenNm.Location = New System.Drawing.Point(8, 16)
        Me.lblBacgenNm.Name = "lblBacgenNm"
        Me.lblBacgenNm.Size = New System.Drawing.Size(111, 21)
        Me.lblBacgenNm.TabIndex = 0
        Me.lblBacgenNm.Text = "배양균속명"
        Me.lblBacgenNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBacgenNm
        '
        Me.txtBacgenNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacgenNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBacgenNm.Location = New System.Drawing.Point(120, 16)
        Me.txtBacgenNm.MaxLength = 60
        Me.txtBacgenNm.Name = "txtBacgenNm"
        Me.txtBacgenNm.Size = New System.Drawing.Size(364, 21)
        Me.txtBacgenNm.TabIndex = 3
        Me.txtBacgenNm.Tag = "BACGENNM"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.txtBacgenCd)
        Me.grpCd.Controls.Add(Me.lblBacgenCd)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(11, 3)
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
        Me.btnUE.Location = New System.Drawing.Point(686, 11)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 2
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'txtBacgenCd
        '
        Me.txtBacgenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBacgenCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtBacgenCd.Location = New System.Drawing.Point(117, 14)
        Me.txtBacgenCd.MaxLength = 4
        Me.txtBacgenCd.Name = "txtBacgenCd"
        Me.txtBacgenCd.Size = New System.Drawing.Size(72, 21)
        Me.txtBacgenCd.TabIndex = 1
        Me.txtBacgenCd.Tag = "BACGENCD"
        '
        'lblBacgenCd
        '
        Me.lblBacgenCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBacgenCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacgenCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacgenCd.ForeColor = System.Drawing.Color.White
        Me.lblBacgenCd.Location = New System.Drawing.Point(8, 14)
        Me.lblBacgenCd.Name = "lblBacgenCd"
        Me.lblBacgenCd.Size = New System.Drawing.Size(108, 21)
        Me.lblBacgenCd.TabIndex = 7
        Me.lblBacgenCd.Text = "배양균속코드"
        Me.lblBacgenCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txRegNm
        '
        Me.txRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txRegNm.Location = New System.Drawing.Point(701, 547)
        Me.txRegNm.Name = "txRegNm"
        Me.txRegNm.ReadOnly = True
        Me.txRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txRegNm.TabIndex = 12
        Me.txRegNm.TabStop = False
        Me.txRegNm.Tag = "REGNM"
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(701, 546)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'FDF15
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(928, 606)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF15"
        Me.Text = "[15] 배양균속"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg1.ResumeLayout(False)
        Me.tbcTpg1.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub txtBacGenNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBacgenNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If txtBacgenNmS.Text.Trim = "" Then
            If txtBacgenNm.Text.Length > txtBacgenNmS.MaxLength Then
                txtBacgenNmS.Text = txtBacgenNm.Text.Substring(0, txtBacgenNmS.MaxLength)
            Else
                txtBacgenNmS.Text = txtBacgenNm.Text
            End If
        End If

        If txtBacgenNmD.Text.Trim = "" Then
            If txtBacgenNm.Text.Length > txtBacgenNmD.MaxLength Then
                txtBacgenNmD.Text = txtBacgenNm.Text.Substring(0, txtBacgenNmD.MaxLength)
            Else
                txtBacgenNmD.Text = txtBacgenNm.Text
            End If
        End If

        If txtBacgenNmP.Text.Trim = "" Then
            If txtBacgenNm.Text.Length > txtBacgenNmP.MaxLength Then
                txtBacgenNmP.Text = txtBacgenNm.Text.Substring(0, txtBacgenNmP.MaxLength)
            Else
                txtBacgenNmP.Text = txtBacgenNm.Text
            End If
        End If
    End Sub

    Private Sub FDF15_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        If txtBacgenCd.Text = "" Then Exit Sub

        Try

            Dim sMsg As String = lblBacgenCd.Text + " : " + Me.txtBacgenCd.Text + vbCrLf
            sMsg += lblBacgenNm.Text + "  : " + Me.txtBacgenNm.Text + vbCrLf + vbCrLf
            sMsg += "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransBacgenInfo_UE(txtBacgenCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 배양균속정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub txtBacgenCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBacgenCd.KeyDown, txtBacgenNm.KeyDown, txtBacgenNmD.KeyDown, txtBacgenNmP.KeyDown, txtBacgenNmS.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

End Class
