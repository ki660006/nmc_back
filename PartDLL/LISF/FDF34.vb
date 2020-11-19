'>>> [34] 반납폐기사유(수혈)
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF34
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF34.vb, Class : FDF4" + vbTab
    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1

    Private mobjDAF As New LISAPP.APP_F_RTNCD

    Public gsModDT As String = ""
    Public gsModID As String = ""
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents chkCostGbn As System.Windows.Forms.CheckBox
    Friend WithEvents lblCostGbn As System.Windows.Forms.Label

    Private Function fnCollectItemTable_170(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_170(String) As LISAPP.ItemTableCollection"

        Try
            Dim it As New LISAPP.ItemTableCollection

            With it
                Dim sCmtGbnCd As String = Ctrl.Get_Code(Me.cboCmtGbn)

                .SetItemTable("CMTGBN", 1, 1, sCmtGbnCd.Substring(0, 1))
                .SetItemTable("CMTCD", 2, 1, Me.txtCmtCd.Text)
                .SetItemTable("REGDT", 3, 1, rsRegDT)
                .SetItemTable("REGID", 4, 1, USER_INFO.USRID)
                .SetItemTable("CMTCONT", 5, 1, Me.txtCmtCont.Text)
                .SetItemTable("REALGBN", 6, 1, sCmtGbnCd.Substring(1, 1))
                .SetItemTable("CHGGBN", 7, 1, sCmtGbnCd.Substring(2, 1))
                .SetItemTable("STOPGBN", 8, 1, IIf(Me.chkStopGbn.Checked, "1", "0").ToString())
                .SetItemTable("COSTGBN", 9, 1, IIf(Me.chkCostGbn.Checked, "1", "0").ToString())
                .SetItemTable("REGIP", 10, 1, USER_INFO.LOCALIP)
            End With

            Return it

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it170 As New LISAPP.ItemTableCollection
            Dim iRegType170 As Integer = 0
            Dim sRegDT As String = ""

            iRegType170 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it170 = fnCollectItemTable_170(sRegDT)

            If mobjDAF.TransRtnCdInfo(it170, iRegType170, Ctrl.Get_Code(Me.cboCmtGbn), Me.txtCmtCd.Text, USER_INFO.USRID) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Function fnFindConflict(ByVal rsCmtGbnCd As String, ByVal rsRtnCd As String) As String
        Dim sFn As String = "fnFindConflict(String) As String"

        Try
            Dim dt As DataTable = mobjDAF.GetRecentRtnCdInfo(rsCmtGbnCd, rsRtnCd)

            If dt.Rows.Count > 0 Then
                Return "동일 " + Me.tbcTpg.Text + "가 존재합니다." + vbCrLf + vbCrLf + _
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
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")

        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Me.cboCmtGbn.SelectedIndex < 0 Then
                MsgBox(Me.lblCmtGbn.Text + "을(를) 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtCmtCd.Text.Trim) < Me.txtCmtCd.MaxLength Then
                MsgBox(Me.lblCmtCd.Text + "을(를) (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtCmtCont.Text.Trim) < 1 Then
                MsgBox(Me.lblCmtCont.Text + "을(를) (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Ctrl.Get_Code(Me.cboCmtGbn), Me.txtCmtCd.Text)

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

    Public Sub sbDisplayCdDetail(ByVal rsCmtGbnCd As String, ByVal rsRtnCd As String)
        Dim sFn As String = "sbDisplayCdDetail(String, String)"

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                sbDisplayCdList_Ref()
            End If

            sbDisplayCdDetail_RtnCd(rsCmtGbnCd, rsRtnCd)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdDetail_RtnCd(ByVal rsCmtGbnCd As String, ByVal rsRtnCd As String)
        Dim sFn As String = "sbDisplayCdDetail_RtnCd(String)"
        Dim iCol% = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex% = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetRtnCdInfo(rsCmtGbnCd, rsRtnCd)
            Else
                dt = mobjDAF.GetRtnCdInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsCmtGbnCd, rsRtnCd)
            End If

            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            '''    sbInitialize()

            ''초기화할 것은 Query라벨
            'sbInitialize_Test_QueryLabel()

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

    Private Sub sbDisplayCdList_Ref()
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref()"

        Try
            miSelectKey = 1

            sbDisplayCdList_Ref_CmtGbn(Me.cboCmtGbn)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_CmtGbn(ByVal actrl As Windows.Forms.ComboBox)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_CmtGbn(Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable

            dt = mobjDAF.GetCmtGbnInfo()

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add("[" + dt.Rows(i).Item("cmtgbncd").ToString + "] " + dt.Rows(i).Item("cmtgbnnm").ToString)
                Next
            End With
           

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

            sbInitialize_ErrProvider()

            sbInitialize_Control()

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn$ = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbInitialize_Control(Optional ByVal iMode% = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode% = 0)"

        Try
            If iMode = 0 Then
                cboCmtGbn.SelectedIndex = -1
                Me.txtCmtCd.Text = "" : Me.btnUE.Visible = False
                Me.txtCmtCont.Text = ""
                Me.chkStopGbn.Checked = False : Me.chkCostGbn.Checked = False

                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtModNm.Text = "" : Me.txtRegNm.Text = ""
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
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents tbcBody As System.Windows.Forms.TabControl
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents lblCmtGbn As System.Windows.Forms.Label
    Friend WithEvents lblCmtCd As System.Windows.Forms.Label
    Friend WithEvents txtCmtCd As System.Windows.Forms.TextBox
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents lblStop As System.Windows.Forms.Label
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    Friend WithEvents cboCmtGbn As System.Windows.Forms.ComboBox
    Friend WithEvents chkStopGbn As System.Windows.Forms.CheckBox
    Friend WithEvents lblCmtCont As System.Windows.Forms.Label
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.tbcBody = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
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
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.cboCmtGbn = New System.Windows.Forms.ComboBox
        Me.txtCmtCd = New System.Windows.Forms.TextBox
        Me.lblCmtGbn = New System.Windows.Forms.Label
        Me.lblCmtCd = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.chkCostGbn = New System.Windows.Forms.CheckBox
        Me.lblCostGbn = New System.Windows.Forms.Label
        Me.chkStopGbn = New System.Windows.Forms.CheckBox
        Me.lblStop = New System.Windows.Forms.Label
        Me.lblCmtCont = New System.Windows.Forms.Label
        Me.txtCmtCont = New System.Windows.Forms.TextBox
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tbcBody.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbcBody
        '
        Me.tbcBody.Controls.Add(Me.tbcTpg)
        Me.tbcBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcBody.Location = New System.Drawing.Point(0, 0)
        Me.tbcBody.Name = "tbcBody"
        Me.tbcBody.SelectedIndex = 0
        Me.tbcBody.Size = New System.Drawing.Size(788, 601)
        Me.tbcBody.TabIndex = 0
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.txtModNm)
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
        Me.tbcTpg.Text = "반납폐기사유(수혈)정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(706, 528)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 196
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(292, 528)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 195
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(292, 528)
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
        Me.lblModNm.Location = New System.Drawing.Point(207, 528)
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
        Me.txtModDT.Location = New System.Drawing.Point(95, 528)
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
        Me.lblModDT.Location = New System.Drawing.Point(10, 528)
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
        Me.txtRegDT.Location = New System.Drawing.Point(509, 528)
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
        Me.lblUserNm.Location = New System.Drawing.Point(621, 528)
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
        Me.lblRegDT.Location = New System.Drawing.Point(424, 528)
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
        Me.txtRegID.Location = New System.Drawing.Point(706, 528)
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
        Me.grpCd.Controls.Add(Me.cboCmtGbn)
        Me.grpCd.Controls.Add(Me.txtCmtCd)
        Me.grpCd.Controls.Add(Me.lblCmtGbn)
        Me.grpCd.Controls.Add(Me.lblCmtCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 0
        Me.grpCd.TabStop = False
        '
        'cboCmtGbn
        '
        Me.cboCmtGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCmtGbn.Location = New System.Drawing.Point(73, 16)
        Me.cboCmtGbn.Name = "cboCmtGbn"
        Me.cboCmtGbn.Size = New System.Drawing.Size(196, 20)
        Me.cboCmtGbn.TabIndex = 1
        Me.cboCmtGbn.Tag = "CMTGBN_01"
        '
        'txtCmtCd
        '
        Me.txtCmtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCd.Location = New System.Drawing.Point(341, 15)
        'Me.txtCmtCd.MaxLength = 4  2018_09_17 이전소스 
        Me.txtCmtCd.MaxLength = 6 '2018_09_17 방창현 :혈액은행쪽 요청으로 사유코드 입력 최대 수 4 > 6 자리로 증가 
        Me.txtCmtCd.Name = "txtCmtCd"
        Me.txtCmtCd.Size = New System.Drawing.Size(40, 21)
        Me.txtCmtCd.TabIndex = 2
        Me.txtCmtCd.Tag = "CMTCD"
        '
        'lblCmtGbn
        '
        Me.lblCmtGbn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCmtGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtGbn.ForeColor = System.Drawing.Color.White
        Me.lblCmtGbn.Location = New System.Drawing.Point(8, 15)
        Me.lblCmtGbn.Name = "lblCmtGbn"
        Me.lblCmtGbn.Size = New System.Drawing.Size(64, 21)
        Me.lblCmtGbn.TabIndex = 0
        Me.lblCmtGbn.Text = "사유구분"
        Me.lblCmtGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCmtCd
        '
        Me.lblCmtCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCmtCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtCd.ForeColor = System.Drawing.Color.White
        Me.lblCmtCd.Location = New System.Drawing.Point(276, 15)
        Me.lblCmtCd.Name = "lblCmtCd"
        Me.lblCmtCd.Size = New System.Drawing.Size(64, 21)
        Me.lblCmtCd.TabIndex = 0
        Me.lblCmtCd.Text = "사유코드"
        Me.lblCmtCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(688, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 3
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.chkCostGbn)
        Me.grpCdInfo1.Controls.Add(Me.lblCostGbn)
        Me.grpCdInfo1.Controls.Add(Me.chkStopGbn)
        Me.grpCdInfo1.Controls.Add(Me.lblStop)
        Me.grpCdInfo1.Controls.Add(Me.lblCmtCont)
        Me.grpCdInfo1.Controls.Add(Me.txtCmtCont)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 52)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(768, 472)
        Me.grpCdInfo1.TabIndex = 1
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "반납폐기사유(수혈)정보"
        '
        'chkCostGbn
        '
        Me.chkCostGbn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCostGbn.Location = New System.Drawing.Point(76, 72)
        Me.chkCostGbn.Name = "chkCostGbn"
        Me.chkCostGbn.Size = New System.Drawing.Size(147, 20)
        Me.chkCostGbn.TabIndex = 6
        Me.chkCostGbn.Tag = "COSTGBN"
        Me.chkCostGbn.Text = "폐기시 환불여부"
        '
        'lblCostGbn
        '
        Me.lblCostGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCostGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCostGbn.ForeColor = System.Drawing.Color.White
        Me.lblCostGbn.Location = New System.Drawing.Point(8, 68)
        Me.lblCostGbn.Name = "lblCostGbn"
        Me.lblCostGbn.Size = New System.Drawing.Size(64, 21)
        Me.lblCostGbn.TabIndex = 3
        Me.lblCostGbn.Text = "환불여부"
        Me.lblCostGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkStopGbn
        '
        Me.chkStopGbn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkStopGbn.Location = New System.Drawing.Point(76, 50)
        Me.chkStopGbn.Name = "chkStopGbn"
        Me.chkStopGbn.Size = New System.Drawing.Size(147, 20)
        Me.chkStopGbn.TabIndex = 5
        Me.chkStopGbn.Tag = "STOPGBN"
        Me.chkStopGbn.Text = "수혈중단 사유로 처리"
        '
        'lblStop
        '
        Me.lblStop.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblStop.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblStop.ForeColor = System.Drawing.Color.White
        Me.lblStop.Location = New System.Drawing.Point(8, 46)
        Me.lblStop.Name = "lblStop"
        Me.lblStop.Size = New System.Drawing.Size(64, 21)
        Me.lblStop.TabIndex = 0
        Me.lblStop.Text = "수혈중단"
        Me.lblStop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCmtCont
        '
        Me.lblCmtCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtCont.ForeColor = System.Drawing.Color.White
        Me.lblCmtCont.Location = New System.Drawing.Point(8, 24)
        Me.lblCmtCont.Name = "lblCmtCont"
        Me.lblCmtCont.Size = New System.Drawing.Size(64, 21)
        Me.lblCmtCont.TabIndex = 0
        Me.lblCmtCont.Text = "사유명"
        Me.lblCmtCont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCmtCont
        '
        Me.txtCmtCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCont.Location = New System.Drawing.Point(73, 24)
        Me.txtCmtCont.MaxLength = 200
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(608, 21)
        Me.txtCmtCont.TabIndex = 4
        Me.txtCmtCont.Tag = "CMTCONT"
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
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF34
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF34"
        Me.Text = "[34] 반납폐기사유(수혈)"
        Me.tbcBody.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click(Object, System.EventArgs) Handles btnUE.Click"

        If Me.cboCmtGbn.SelectedIndex < 0 Then Return
        If Me.txtCmtCd.Text = "" Then Return

        Try
            Dim sMsg As String = Me.lblCmtGbn.Text & " : " & Ctrl.Get_Item(Me.cboCmtGbn) & vbCrLf
            sMsg &= Me.lblCmtCd.Text & " : " & Me.txtCmtCd.Text & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransRtnCdInfo_UE(Ctrl.Get_Code(Me.cboCmtGbn), Me.txtCmtCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 " + Me.tbcTpg.Text + "가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub FDF34_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtCmtCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown, cboCmtGbn.KeyDown, txtCmtCont.KeyDown, chkCostGbn.KeyDown, chkStopGbn.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub
End Class
