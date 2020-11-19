'>>> [18] 배양균속별 항균제

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF18
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF17.vb, Class : FDF18" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_BACGEN_ANTI
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label

    Private Function fnCollectItemTable_240(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_230(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it240 As New LISAPP.ItemTableCollection

            With it240
                Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAntiList

                '< add freety 2005/11/29 : index 오류
                Dim iCnt As Integer = 0

                For i As Integer = 1 To spd.MaxRows
                    Dim sChk As String = Ctrl.Get_Code(spd, "CHK", i)

                    If sChk = "1" Then
                        iCnt += 1

                        .SetItemTable("BACGENCD", 1, iCnt, Ctrl.Get_Code(Me.cboBacgen))
                        .SetItemTable("ANTICD", 2, iCnt, Ctrl.Get_Code(spd, "ANTICD", i))
                        .SetItemTable("TESTMTD", 3, iCnt, Ctrl.Get_Item(Me.cboTestMtd))
                        .SetItemTable("REGDT", 4, iCnt, rsRegDT)
                        .SetItemTable("REGID", 5, iCnt, USER_INFO.USRID)
                        .SetItemTable("DISPSEQ", 6, iCnt, Ctrl.Get_Code(spd, "DISPSEQ", i))
                        .SetItemTable("REFR", 7, iCnt, Ctrl.Get_Code(spd, "REFR", i))
                        .SetItemTable("REFS", 8, iCnt, Ctrl.Get_Code(spd, "REFS", i))
                        .SetItemTable("REGIP", 9, iCnt, USER_INFO.LOCALIP)
                    End If
                Next
                '>
            End With

            fnCollectItemTable_240 = it240
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it240 As New LISAPP.ItemTableCollection
            Dim iRegType240 As Integer = 0
            Dim sRegDT As String

            iRegType240 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it240 = fnCollectItemTable_240(sRegDT)

            If mobjDAF.TransBacgenAntiInfo(it240, iRegType240, Ctrl.Get_Code(Me.cboBacgen), Ctrl.Get_Item(Me.cboTestMtd), USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsBacgenCd As String, ByVal rsTestMtd As String) As String
        Dim sFn As String = "Private Function fnFindConflict(ByVal asBacgenCd As String, ByVal asTestMtd As String, ByVal asUSDT As String) As String"

        Try
            Dim dt As DataTable = mobjDAF.GetRecentBacgenAntiInfo(rsBacgenCd, rsTestMtd)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString + "인 동일 " + "배양균속별 항균제" + "가 존재합니다." + vbCrLf + vbCrLf

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
            If Ctrl.Get_Code(Me.cboBacgen) = "" Then
                MsgBox("균속코드를 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If cboTestMtd.SelectedIndex < 0 Then
                MsgBox("항균제 검사방법을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAntiList
            Dim iChkCol As Integer = spd.SearchCol(spd.GetColFromID("CHK"), 0, spd.MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

            If iChkCol < 1 Then
                MsgBox("항균제를 하나이상 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If


            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Ctrl.Get_Code(Me.cboBacgen), Ctrl.Get_Item(Me.cboTestMtd))

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

    Public Sub sbDisplayCdDetail(ByVal rsBacgenCd As String, ByVal rsTestMtd As String, ByVal rsModId As String, ByVal rsModDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            If Not IsNothing(Me.Owner) Then
                If Not CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    sbDisplayCdList_Ref()
                End If
            End If

            sbDisplayCdDetail_BacgenAnti(rsBacgenCd, rsTestMtd, rsMODID, rsModDt)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_BacgenAnti(ByVal rsBacgenCd As String, ByVal rsTestMtd As String, ByVal rsModId As String, ByVal rsModDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Bac(ByVal asBacCd As String, ByVal asUSDT As String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As New DataTable
            Dim cctrl As System.Windows.Forms.Control

            If rsModDt = "" Or rsModId = "" Then
                dt = mobjDAF.GetBacgenAntiInfo(rsBacgenCd, rsTestMtd)
            Else
                dt = mobjDAF.GetBacgenAntiInfo(rsBacgenCd, rsTestMtd, rsModDt, rsModId)
            End If
            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            sbInitialize()

            Dim cbo As System.Windows.Forms.ComboBox

            'cboBacgen
            cbo = Me.cboBacgen
            For i As Integer = 1 To cbo.Items.Count
                If cbo.Items.Item(i - 1).ToString().StartsWith("[" + rsBacgenCd + "]") Then
                    cbo.SelectedIndex = i - 1

                    Exit For
                End If
            Next

            'cboTestMtd
            cbo = Me.cboTestMtd
            For i As Integer = 1 To cbo.Items.Count
                If cbo.Items.Item(i - 1).ToString() = rsTestMtd Then
                    cbo.SelectedIndex = i - 1

                    Exit For
                End If
            Next

            If dt.Rows.Count > 0 Then
                'spdAntiList 표시
                Ctrl.DisplayAfterSelect(Me.spdAntiList, dt, "U")

                Me.txtRegDT.Text = dt.Rows(0).Item("regdt").ToString()
                Me.txtRegID.Text = dt.Rows(0).Item("regid").ToString()

                Me.txtModDT.Text = dt.Rows(0).Item("moddt").ToString()
                Me.txtModNm.Text = dt.Rows(0).Item("modnm").ToString()
                Me.txtRegNm.Text = dt.Rows(0).Item("regnm").ToString()

            Else
                Exit Sub
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref()
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref()"

        Try
            miSelectKey = 1

            sbDisplayCdList_Ref_Bacgen(Me.cboBacgen)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_Bacgen(ByVal actrl As System.Windows.Forms.ComboBox)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_Bacgen(ByVal actrl As System.Windows.Forms.ComboBox, ByVal asUSDT As String, ByVal asUEDT As String)"

        Try
            Dim dt As DataTable = mobjDAF.GetBacgenInfo()

            actrl.Items.Clear()

            If dt.Rows.Count < 0 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("bacgen"))
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
            Dim dt As New DataTable

            If iMode = 0 Then
                Me.cboBacgen.SelectedIndex = -1 : Me.cboTestMtd.SelectedIndex = -1
                : btnUE.Visible = False
                'txtUSDT.Text = "" : txtUEDT.Text = ""
                txtRegDT.Text = "" : txtRegID.Text = "" : txtRegNm.Text = ""

                dt = mobjDAF.GetBacgenAntiInfo("", "")

            End If

            If dt.Rows.Count > 0 Then
                'spdAntiList 표시
                Ctrl.DisplayAfterSelect(Me.spdAntiList, dt, "U")

            Else
                Exit Sub
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



            '신규 시작일시에 맞는 CdList를 불러옴
            sbDisplayCdList_Ref()
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
    Friend WithEvents pnlBack As System.Windows.Forms.Panel
    Friend WithEvents tbcControl As System.Windows.Forms.TabControl
    Friend WithEvents tbcPage As System.Windows.Forms.TabPage
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents lblBacgenCd As System.Windows.Forms.Label
    Friend WithEvents spdAntiList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblOReqItem As System.Windows.Forms.Label
    Friend WithEvents lblGuide1 As System.Windows.Forms.Label
    Friend WithEvents lblGuide3 As System.Windows.Forms.Label
    Friend WithEvents lblGuide2 As System.Windows.Forms.Label
    Friend WithEvents cboTestMtd As System.Windows.Forms.ComboBox
    Friend WithEvents lblTestMtd As System.Windows.Forms.Label
    Friend WithEvents cboBacgen As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF18))
        Me.pnlBack = New System.Windows.Forms.Panel
        Me.tbcControl = New System.Windows.Forms.TabControl
        Me.tbcPage = New System.Windows.Forms.TabPage
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.lblGuide2 = New System.Windows.Forms.Label
        Me.lblGuide3 = New System.Windows.Forms.Label
        Me.lblGuide1 = New System.Windows.Forms.Label
        Me.lblOReqItem = New System.Windows.Forms.Label
        Me.spdAntiList = New AxFPSpreadADO.AxfpSpread
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnUE = New System.Windows.Forms.Button
        Me.lblTestMtd = New System.Windows.Forms.Label
        Me.cboTestMtd = New System.Windows.Forms.ComboBox
        Me.cboBacgen = New System.Windows.Forms.ComboBox
        Me.lblBacgenCd = New System.Windows.Forms.Label
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlBack.SuspendLayout()
        Me.tbcControl.SuspendLayout()
        Me.tbcPage.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.spdAntiList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCd.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlBack
        '
        Me.pnlBack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBack.Controls.Add(Me.tbcControl)
        Me.pnlBack.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBack.Location = New System.Drawing.Point(0, 0)
        Me.pnlBack.Name = "pnlBack"
        Me.pnlBack.Size = New System.Drawing.Size(792, 605)
        Me.pnlBack.TabIndex = 0
        '
        'tbcControl
        '
        Me.tbcControl.Controls.Add(Me.tbcPage)
        Me.tbcControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcControl.Location = New System.Drawing.Point(0, 0)
        Me.tbcControl.Name = "tbcControl"
        Me.tbcControl.SelectedIndex = 0
        Me.tbcControl.Size = New System.Drawing.Size(788, 601)
        Me.tbcControl.TabIndex = 0
        Me.tbcControl.TabStop = False
        '
        'tbcPage
        '
        Me.tbcPage.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcPage.Controls.Add(Me.txtModNm)
        Me.tbcPage.Controls.Add(Me.lblModNm)
        Me.tbcPage.Controls.Add(Me.txtModDT)
        Me.tbcPage.Controls.Add(Me.lblModDT)
        Me.tbcPage.Controls.Add(Me.txtRegNm)
        Me.tbcPage.Controls.Add(Me.txtRegDT)
        Me.tbcPage.Controls.Add(Me.lblUserNm)
        Me.tbcPage.Controls.Add(Me.lblRegDT)
        Me.tbcPage.Controls.Add(Me.txtRegID)
        Me.tbcPage.Controls.Add(Me.grpCdInfo1)
        Me.tbcPage.Controls.Add(Me.grpCd)
        Me.tbcPage.Location = New System.Drawing.Point(4, 21)
        Me.tbcPage.Name = "tbcPage"
        Me.tbcPage.Size = New System.Drawing.Size(780, 576)
        Me.tbcPage.TabIndex = 0
        Me.tbcPage.Text = "배양균속별 항균제 정보"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(302, 545)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 192
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(217, 545)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 191
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(104, 545)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 190
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(19, 545)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(84, 21)
        Me.lblModDT.TabIndex = 189
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(702, 545)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 188
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(510, 545)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 11
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(617, 545)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 21)
        Me.lblUserNm.TabIndex = 5
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(425, 545)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 4
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(702, 545)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 6
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.lblGuide2)
        Me.grpCdInfo1.Controls.Add(Me.lblGuide3)
        Me.grpCdInfo1.Controls.Add(Me.lblGuide1)
        Me.grpCdInfo1.Controls.Add(Me.lblOReqItem)
        Me.grpCdInfo1.Controls.Add(Me.spdAntiList)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 56)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 478)
        Me.grpCdInfo1.TabIndex = 1
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "배양균속별 항균제 정보"
        '
        'lblGuide2
        '
        Me.lblGuide2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblGuide2.Location = New System.Drawing.Point(192, 20)
        Me.lblGuide2.Name = "lblGuide2"
        Me.lblGuide2.Size = New System.Drawing.Size(64, 21)
        Me.lblGuide2.TabIndex = 129
        Me.lblGuide2.Text = "M : MIC"
        Me.lblGuide2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGuide3
        '
        Me.lblGuide3.BackColor = System.Drawing.Color.PowderBlue
        Me.lblGuide3.Location = New System.Drawing.Point(264, 20)
        Me.lblGuide3.Name = "lblGuide3"
        Me.lblGuide3.Size = New System.Drawing.Size(80, 21)
        Me.lblGuide3.TabIndex = 128
        Me.lblGuide3.Text = "E : e-Test"
        Me.lblGuide3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGuide1
        '
        Me.lblGuide1.BackColor = System.Drawing.Color.MistyRose
        Me.lblGuide1.Location = New System.Drawing.Point(120, 20)
        Me.lblGuide1.Name = "lblGuide1"
        Me.lblGuide1.Size = New System.Drawing.Size(64, 21)
        Me.lblGuide1.TabIndex = 127
        Me.lblGuide1.Text = "D : DISK"
        Me.lblGuide1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOReqItem
        '
        Me.lblOReqItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblOReqItem.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOReqItem.ForeColor = System.Drawing.Color.Black
        Me.lblOReqItem.Location = New System.Drawing.Point(12, 20)
        Me.lblOReqItem.Name = "lblOReqItem"
        Me.lblOReqItem.Size = New System.Drawing.Size(103, 20)
        Me.lblOReqItem.TabIndex = 125
        Me.lblOReqItem.Text = "항균제 검사방법"
        Me.lblOReqItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'spdAntiList
        '
        Me.spdAntiList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdAntiList.DataSource = Nothing
        Me.spdAntiList.Location = New System.Drawing.Point(12, 48)
        Me.spdAntiList.Name = "spdAntiList"
        Me.spdAntiList.OcxState = CType(resources.GetObject("spdAntiList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdAntiList.Size = New System.Drawing.Size(575, 418)
        Me.spdAntiList.TabIndex = 4
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.lblTestMtd)
        Me.grpCd.Controls.Add(Me.cboTestMtd)
        Me.grpCd.Controls.Add(Me.cboBacgen)
        Me.grpCd.Controls.Add(Me.lblBacgenCd)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 6)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 0
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
        Me.btnUE.TabIndex = 3
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'lblTestMtd
        '
        Me.lblTestMtd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestMtd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestMtd.ForeColor = System.Drawing.Color.White
        Me.lblTestMtd.Location = New System.Drawing.Point(331, 15)
        Me.lblTestMtd.Name = "lblTestMtd"
        Me.lblTestMtd.Size = New System.Drawing.Size(103, 21)
        Me.lblTestMtd.TabIndex = 11
        Me.lblTestMtd.Text = "항균제 검사방법"
        Me.lblTestMtd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboTestMtd
        '
        Me.cboTestMtd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTestMtd.Items.AddRange(New Object() {"D", "M", "E"})
        Me.cboTestMtd.Location = New System.Drawing.Point(435, 16)
        Me.cboTestMtd.MaxDropDownItems = 10
        Me.cboTestMtd.Name = "cboTestMtd"
        Me.cboTestMtd.Size = New System.Drawing.Size(60, 20)
        Me.cboTestMtd.TabIndex = 2
        Me.cboTestMtd.TabStop = False
        Me.cboTestMtd.Tag = "TESTMTD"
        '
        'cboBacgen
        '
        Me.cboBacgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBacgen.Location = New System.Drawing.Point(73, 16)
        Me.cboBacgen.MaxDropDownItems = 10
        Me.cboBacgen.Name = "cboBacgen"
        Me.cboBacgen.Size = New System.Drawing.Size(240, 20)
        Me.cboBacgen.TabIndex = 1
        Me.cboBacgen.TabStop = False
        Me.cboBacgen.Tag = ""
        '
        'lblBacgenCd
        '
        Me.lblBacgenCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBacgenCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBacgenCd.ForeColor = System.Drawing.Color.White
        Me.lblBacgenCd.Location = New System.Drawing.Point(10, 15)
        Me.lblBacgenCd.Name = "lblBacgenCd"
        Me.lblBacgenCd.Size = New System.Drawing.Size(62, 21)
        Me.lblBacgenCd.TabIndex = 7
        Me.lblBacgenCd.Text = "균속코드"
        Me.lblBacgenCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF18
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlBack)
        Me.Name = "FDF18"
        Me.Text = "[18] 배양균속별 항균제"
        Me.pnlBack.ResumeLayout(False)
        Me.tbcControl.ResumeLayout(False)
        Me.tbcPage.ResumeLayout(False)
        Me.tbcPage.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        CType(Me.spdAntiList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCd.ResumeLayout(False)
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        If Me.cboBacgen.SelectedIndex < 0 Then Exit Sub
        If Me.cboTestMtd.SelectedIndex < 0 Then Exit Sub

        Try

            Dim sMsg As String = "균속코드 : " & Ctrl.Get_Code(Me.cboBacgen) & vbCrLf
            sMsg &= "항균제 검사방법 : " & Ctrl.Get_Item(Me.cboTestMtd) & vbCrLf & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransBacgenAntiInfo_UE(Ctrl.Get_Code(Me.cboBacgen), Ctrl.Get_Item(Me.cboTestMtd), USER_INFO.USRID) Then
                MsgBox("해당 배양균속별 항균제정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

  
    Private Sub FDF18_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

End Class
