'>>> 위탁검사 결과 저장 및 보고

Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports System.Runtime.InteropServices

Public Class FGJ06
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGJ06.vb, Class : J01" & vbTab

    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboBcclsCd As System.Windows.Forms.ComboBox
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnUpLoad As System.Windows.Forms.Button

    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents txtEtc As System.Windows.Forms.TextBox
    Friend WithEvents chkQryAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnGetUpLoad As System.Windows.Forms.Button
    Friend WithEvents btnSelDel As System.Windows.Forms.Button
    Friend WithEvents tabUpLoad_List As System.Windows.Forms.TabPage
    Friend WithEvents btnUpLoad_Del As System.Windows.Forms.Button
    Friend WithEvents lblFileNm As System.Windows.Forms.Label
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpUpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpUpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents spdUpLoad_List As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboExlab_up As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnPrint_sp As System.Windows.Forms.Button
    Friend WithEvents chkSelChk As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Private m_al_StSub As New ArrayList

    Private Sub sbPrint_Data()
        Dim sFn As String = "Sub sbPrint_Data()"

        Try
            Dim arlPrint As New ArrayList
            Dim objPat As New FGR06_PATINFO

            Dim strBcNo_Cur As String = ""
            Dim intCnt As Integer = 0

            With spdList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("bcno") : Dim strBcno As String = .Text
                    .Col = .GetColFromID("testcd") : Dim strtestcd As String = .Text
                    .Col = .GetColFromID("tnmd") : Dim strTnmd As String = .Text
                    .Col = .GetColFromID("spccd") : Dim strSpcCd As String = .Text
                    .Col = .GetColFromID("spcnmd") : Dim strSpcNmd As String = .Text
                    .Col = .GetColFromID("regno") : Dim strRegNo As String = .Text
                    .Col = .GetColFromID("patnm") : Dim strPatNm As String = .Text
                    .Col = .GetColFromID("idno") : Dim strIdNo As String = .Text
                    .Col = .GetColFromID("sex") : Dim strSex As String = .Text
                    .Col = .GetColFromID("wardcd") : Dim strWard As String = .Text
                    .Col = .GetColFromID("deptcd") : Dim strDept As String = .Text
                    .Col = .GetColFromID("colldt") : Dim strCollDt As String = .Text
                    .Col = .GetColFromID("etc") : Dim strEtc As String = .Text

                    objPat = New FGR06_PATINFO

                    With objPat

                        If strBcno <> strBcNo_Cur Then
                            intCnt += 1
                            .sSeqNo = intCnt.ToString
                            .sBcNo = strBcno
                        End If
                        strBcNo_Cur = strBcno

                        .stestcd = strtestcd
                        .sTnmd = strTnmd
                        .sSpcCd = strSpcCd
                        .sSpcNmd = strSpcNmd
                        .sRegNo = strRegNo
                        .sPatNm = strPatNm
                        .sIdNo = strIdNo
                        .sSex = strSex
                        .sWard = strWard
                        .sDeptNm = strDept
                        .sCollDt = strCollDt
                        .sEtc = strEtc

                        .sComment = ""
                    End With

                    arlPrint.Add(objPat)
                Next

                objPat = New FGR06_PATINFO

                With objPat
                    .sBcNo = ""
                    .sComment = ""
                End With

                arlPrint.Add(objPat)

                txtEtc.Text = txtEtc.Text.Replace(vbLf, "")
                Dim strBuf() As String = txtEtc.Text.Split(Convert.ToChar(vbCr))
                For intIdx As Integer = 0 To strBuf.Length - 1

                    objPat = New FGR06_PATINFO

                    With objPat
                        .sBcNo = ""
                        .sComment = strBuf(intIdx)
                    End With

                    arlPrint.Add(objPat)
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGR06_PRINT
                prt.msTitle = "외주검사 대장(" + cboExLab.Text + ")"
                prt.maPrtData = arlPrint

                With spdList
                    For iCol As Integer = 1 To .MaxCols
                        .Col = iCol
                        prt.ma_col.Add(.ColID)
                    Next
                End With

                prt.sbPrint_Preview()
                'prt.sbPrint()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_List(ByVal r_dt As DataTable)

        Try
            With spdList
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    Dim bFlag As Boolean = False

                    For intRow As Integer = 1 To .MaxRows
                        .Row = intRow
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                        .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                        If r_dt.Rows(ix).Item("bcno").ToString.Trim = sBcNo And r_dt.Rows(ix).Item("testcd").ToString.Trim = sTestCd Then
                            bFlag = True
                            Exit For
                        End If
                    Next

                    If bFlag = False Then
                        .MaxRows += 1
                        .Row = .MaxRows

                        Dim sPatInfo() As String = r_dt.Rows(ix).Item("patinfo").ToString.Split("|"c)

                        .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString.Trim
                        .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix).Item("testcd").ToString.Trim
                        .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix).Item("tnmd").ToString.Trim
                        .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix).Item("spccd").ToString.Trim
                        .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                        .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                        .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                        .Col = .GetColFromID("idno") : .Text = sPatInfo(3).Trim
                        .Col = .GetColFromID("sex") : .Text = sPatInfo(1).Trim
                        .Col = .GetColFromID("age") : .Text = r_dt.Rows(ix).Item("age").ToString.Trim '20190410 삼광 업로드시 필요하여 나이 추가
                        .Col = .GetColFromID("wardno") : .Text = r_dt.Rows(ix).Item("wardno").ToString.Trim
                        .Col = .GetColFromID("deptcd") : .Text = r_dt.Rows(ix).Item("deptcd").ToString.Trim
                        .Col = .GetColFromID("colldt") : .Text = r_dt.Rows(ix).Item("colldt").ToString.Trim
                        .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString.Trim
                        .Col = .GetColFromID("etc") : .Text = r_dt.Rows(ix).Item("remark").ToString.Trim
                        .Col = .GetColFromID("filenm") : .Text = r_dt.Rows(ix).Item("filenm").ToString.Trim
                        '20210104 jhs 채혈시 주의사항 항목 추가 
                        .Col = .GetColFromID("cwarning") : .Text = r_dt.Rows(ix).Item("cwarning").ToString.Trim
                        '----------------------------
                        '<20121107 위수탁검사 처방의명 가져오기
                        .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                        .Col = .GetColFromID("imgyn") : .Text = r_dt.Rows(ix).Item("imgyn").ToString.Trim
                        If Ctrl.Get_Code(cboExLab) = "005" Then
                            .Col = .GetColFromID("chk") : .Text = "1"
                        End If
                    End If
                Next

            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub
    Private Sub chkSelChk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelChk.Click

        With Me.spdList
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Text = IIf(chkSelChk.Checked, "1", "").ToString
                End If
            Next
        End With

    End Sub

    Private Sub sbDisplay_Bccls(ByVal rsExLab As String)

        Dim dt As New DataTable

        Me.cboBcclsCd.Items.Clear()
        Me.cboBcclsCd.Items.Add("[  ] 전체")

        dt = LISAPP.COMM.cdfn.fnGet_Bccls_ExLab_List(rsExLab)

        If dt.Rows.Count > 0 Then
            For intIdx As Integer = 0 To dt.Rows.Count - 1
                Me.cboBcclsCd.Items.Add("[" + dt.Rows(intIdx).Item("bcclscd").ToString().Trim + "] " + dt.Rows(intIdx).Item("bcclsnmd").ToString().Trim)
            Next
        End If

        If cboBcclsCd.Items.Count > 0 Then cboBcclsCd.SelectedIndex = 0

    End Sub


#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

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
    Friend WithEvents cboExLab As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents ofdExLab As System.Windows.Forms.OpenFileDialog
    Friend WithEvents fbdPath As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents tabExLab As System.Windows.Forms.TabControl
    Friend WithEvents tabUpLoad As System.Windows.Forms.TabPage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGJ06))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.cboExLab = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.ofdExLab = New System.Windows.Forms.OpenFileDialog()
        Me.tabExLab = New System.Windows.Forms.TabControl()
        Me.tabUpLoad = New System.Windows.Forms.TabPage()
        Me.chkSelChk = New System.Windows.Forms.CheckBox()
        Me.btnPrint_sp = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFileNm = New System.Windows.Forms.Label()
        Me.btnUpLoad_Del = New System.Windows.Forms.Button()
        Me.btnSelDel = New System.Windows.Forms.Button()
        Me.btnGetUpLoad = New System.Windows.Forms.Button()
        Me.chkQryAll = New System.Windows.Forms.CheckBox()
        Me.txtEtc = New System.Windows.Forms.TextBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnUpLoad = New System.Windows.Forms.Button()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.txtBcNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboBcclsCd = New System.Windows.Forms.ComboBox()
        Me.tabUpLoad_List = New System.Windows.Forms.TabPage()
        Me.cboExlab_up = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.spdUpLoad_List = New AxFPSpreadADO.AxfpSpread()
        Me.txtRegNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpUpDateE = New System.Windows.Forms.DateTimePicker()
        Me.dtpUpDateS = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.fbdPath = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnQuery = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.tabExLab.SuspendLayout()
        Me.tabUpLoad.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabUpLoad_List.SuspendLayout()
        CType(Me.spdUpLoad_List, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboExLab
        '
        Me.cboExLab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExLab.Items.AddRange(New Object() {"녹십자", "네오딘", "랩지노믹스"})
        Me.cboExLab.Location = New System.Drawing.Point(87, 36)
        Me.cboExLab.Name = "cboExLab"
        Me.cboExLab.Size = New System.Drawing.Size(223, 20)
        Me.cboExLab.TabIndex = 123
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(14, 36)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(72, 20)
        Me.Label39.TabIndex = 122
        Me.Label39.Text = "위탁기관명"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabExLab
        '
        Me.tabExLab.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.tabExLab.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabExLab.Controls.Add(Me.tabUpLoad)
        Me.tabExLab.Controls.Add(Me.tabUpLoad_List)
        Me.tabExLab.Location = New System.Drawing.Point(8, 4)
        Me.tabExLab.Multiline = True
        Me.tabExLab.Name = "tabExLab"
        Me.tabExLab.Padding = New System.Drawing.Point(4, 3)
        Me.tabExLab.SelectedIndex = 0
        Me.tabExLab.Size = New System.Drawing.Size(1229, 588)
        Me.tabExLab.TabIndex = 111
        '
        'tabUpLoad
        '
        Me.tabUpLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabUpLoad.Controls.Add(Me.chkSelChk)
        Me.tabUpLoad.Controls.Add(Me.btnPrint_sp)
        Me.tabUpLoad.Controls.Add(Me.Label1)
        Me.tabUpLoad.Controls.Add(Me.cboExLab)
        Me.tabUpLoad.Controls.Add(Me.Label39)
        Me.tabUpLoad.Controls.Add(Me.lblFileNm)
        Me.tabUpLoad.Controls.Add(Me.btnUpLoad_Del)
        Me.tabUpLoad.Controls.Add(Me.btnSelDel)
        Me.tabUpLoad.Controls.Add(Me.btnGetUpLoad)
        Me.tabUpLoad.Controls.Add(Me.chkQryAll)
        Me.tabUpLoad.Controls.Add(Me.txtEtc)
        Me.tabUpLoad.Controls.Add(Me.btnPrint)
        Me.tabUpLoad.Controls.Add(Me.btnUpLoad)
        Me.tabUpLoad.Controls.Add(Me.spdList)
        Me.tabUpLoad.Controls.Add(Me.txtBcNo)
        Me.tabUpLoad.Controls.Add(Me.Label4)
        Me.tabUpLoad.Controls.Add(Me.Label3)
        Me.tabUpLoad.Controls.Add(Me.dtpDateE)
        Me.tabUpLoad.Controls.Add(Me.dtpDateS)
        Me.tabUpLoad.Controls.Add(Me.Label2)
        Me.tabUpLoad.Controls.Add(Me.cboBcclsCd)
        Me.tabUpLoad.Location = New System.Drawing.Point(4, 4)
        Me.tabUpLoad.Name = "tabUpLoad"
        Me.tabUpLoad.Size = New System.Drawing.Size(1221, 562)
        Me.tabUpLoad.TabIndex = 2
        Me.tabUpLoad.Text = " [ 위탁검사 리스트 작성 ]"
        Me.tabUpLoad.UseVisualStyleBackColor = True
        '
        'chkSelChk
        '
        Me.chkSelChk.AutoSize = True
        Me.chkSelChk.Checked = True
        Me.chkSelChk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSelChk.Location = New System.Drawing.Point(54, 83)
        Me.chkSelChk.Name = "chkSelChk"
        Me.chkSelChk.Size = New System.Drawing.Size(15, 14)
        Me.chkSelChk.TabIndex = 126
        Me.chkSelChk.UseVisualStyleBackColor = True
        '
        'btnPrint_sp
        '
        Me.btnPrint_sp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint_sp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint_sp.Location = New System.Drawing.Point(744, 42)
        Me.btnPrint_sp.Margin = New System.Windows.Forms.Padding(1)
        Me.btnPrint_sp.Name = "btnPrint_sp"
        Me.btnPrint_sp.Size = New System.Drawing.Size(83, 22)
        Me.btnPrint_sp.TabIndex = 125
        Me.btnPrint_sp.Text = "의뢰서출력"
        Me.btnPrint_sp.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(319, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 20)
        Me.Label1.TabIndex = 124
        Me.Label1.Text = "검체분류"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFileNm
        '
        Me.lblFileNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFileNm.BackColor = System.Drawing.Color.Gray
        Me.lblFileNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFileNm.ForeColor = System.Drawing.Color.White
        Me.lblFileNm.Location = New System.Drawing.Point(829, 42)
        Me.lblFileNm.Name = "lblFileNm"
        Me.lblFileNm.Size = New System.Drawing.Size(376, 22)
        Me.lblFileNm.TabIndex = 17
        Me.lblFileNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnUpLoad_Del
        '
        Me.btnUpLoad_Del.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpLoad_Del.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpLoad_Del.Location = New System.Drawing.Point(1013, 7)
        Me.btnUpLoad_Del.Margin = New System.Windows.Forms.Padding(1)
        Me.btnUpLoad_Del.Name = "btnUpLoad_Del"
        Me.btnUpLoad_Del.Size = New System.Drawing.Size(90, 34)
        Me.btnUpLoad_Del.TabIndex = 16
        Me.btnUpLoad_Del.Text = "선택항목 DB 에서 삭제"
        Me.btnUpLoad_Del.UseVisualStyleBackColor = True
        '
        'btnSelDel
        '
        Me.btnSelDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelDel.Location = New System.Drawing.Point(1105, 7)
        Me.btnSelDel.Margin = New System.Windows.Forms.Padding(1)
        Me.btnSelDel.Name = "btnSelDel"
        Me.btnSelDel.Size = New System.Drawing.Size(100, 34)
        Me.btnSelDel.TabIndex = 14
        Me.btnSelDel.Text = "선택항목 화면에서 삭제"
        Me.btnSelDel.UseVisualStyleBackColor = True
        '
        'btnGetUpLoad
        '
        Me.btnGetUpLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGetUpLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGetUpLoad.Location = New System.Drawing.Point(914, 7)
        Me.btnGetUpLoad.Margin = New System.Windows.Forms.Padding(1)
        Me.btnGetUpLoad.Name = "btnGetUpLoad"
        Me.btnGetUpLoad.Size = New System.Drawing.Size(97, 34)
        Me.btnGetUpLoad.TabIndex = 13
        Me.btnGetUpLoad.Text = "Up Load File 불러오기"
        Me.btnGetUpLoad.UseVisualStyleBackColor = True
        '
        'chkQryAll
        '
        Me.chkQryAll.AutoSize = True
        Me.chkQryAll.Location = New System.Drawing.Point(342, 12)
        Me.chkQryAll.Name = "chkQryAll"
        Me.chkQryAll.Size = New System.Drawing.Size(96, 16)
        Me.chkQryAll.TabIndex = 12
        Me.chkQryAll.Text = "Up Load 포함"
        Me.chkQryAll.UseVisualStyleBackColor = True
        '
        'txtEtc
        '
        Me.txtEtc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEtc.Location = New System.Drawing.Point(12, 494)
        Me.txtEtc.Multiline = True
        Me.txtEtc.Name = "txtEtc"
        Me.txtEtc.Size = New System.Drawing.Size(1192, 63)
        Me.txtEtc.TabIndex = 11
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Location = New System.Drawing.Point(829, 7)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(1)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(83, 34)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "출력"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnUpLoad
        '
        Me.btnUpLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpLoad.BackColor = System.Drawing.Color.LightCoral
        Me.btnUpLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpLoad.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUpLoad.Location = New System.Drawing.Point(744, 7)
        Me.btnUpLoad.Margin = New System.Windows.Forms.Padding(1)
        Me.btnUpLoad.Name = "btnUpLoad"
        Me.btnUpLoad.Size = New System.Drawing.Size(83, 34)
        Me.btnUpLoad.TabIndex = 9
        Me.btnUpLoad.Text = "Up Load"
        Me.btnUpLoad.UseVisualStyleBackColor = False
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(13, 67)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1192, 422)
        Me.spdList.TabIndex = 8
        '
        'txtBcNo
        '
        Me.txtBcNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBcNo.Location = New System.Drawing.Point(624, 36)
        Me.txtBcNo.MaxLength = 15
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.Size = New System.Drawing.Size(109, 21)
        Me.txtBcNo.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(551, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 21)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "검체번호"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(204, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "~"
        '
        'dtpDateE
        '
        Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateE.Location = New System.Drawing.Point(219, 12)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(114, 21)
        Me.dtpDateE.TabIndex = 3
        '
        'dtpDateS
        '
        Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateS.Location = New System.Drawing.Point(87, 12)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(114, 21)
        Me.dtpDateS.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(14, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "접수일자"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboBcclsCd
        '
        Me.cboBcclsCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBcclsCd.FormattingEnabled = True
        Me.cboBcclsCd.Items.AddRange(New Object() {"[  ] 전체"})
        Me.cboBcclsCd.Location = New System.Drawing.Point(392, 36)
        Me.cboBcclsCd.Name = "cboBcclsCd"
        Me.cboBcclsCd.Size = New System.Drawing.Size(152, 20)
        Me.cboBcclsCd.TabIndex = 0
        '
        'tabUpLoad_List
        '
        Me.tabUpLoad_List.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabUpLoad_List.Controls.Add(Me.cboExlab_up)
        Me.tabUpLoad_List.Controls.Add(Me.Label8)
        Me.tabUpLoad_List.Controls.Add(Me.spdUpLoad_List)
        Me.tabUpLoad_List.Controls.Add(Me.txtRegNo)
        Me.tabUpLoad_List.Controls.Add(Me.Label7)
        Me.tabUpLoad_List.Controls.Add(Me.Label5)
        Me.tabUpLoad_List.Controls.Add(Me.dtpUpDateE)
        Me.tabUpLoad_List.Controls.Add(Me.dtpUpDateS)
        Me.tabUpLoad_List.Controls.Add(Me.Label6)
        Me.tabUpLoad_List.Location = New System.Drawing.Point(4, 4)
        Me.tabUpLoad_List.Name = "tabUpLoad_List"
        Me.tabUpLoad_List.Size = New System.Drawing.Size(1221, 562)
        Me.tabUpLoad_List.TabIndex = 3
        Me.tabUpLoad_List.Text = "[위탁검사 처리내역]"
        Me.tabUpLoad_List.UseVisualStyleBackColor = True
        '
        'cboExlab_up
        '
        Me.cboExlab_up.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExlab_up.Items.AddRange(New Object() {"녹십자", "네오딘", "랩지노믹스"})
        Me.cboExlab_up.Location = New System.Drawing.Point(426, 9)
        Me.cboExlab_up.Name = "cboExlab_up"
        Me.cboExlab_up.Size = New System.Drawing.Size(199, 20)
        Me.cboExlab_up.TabIndex = 125
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(353, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 20)
        Me.Label8.TabIndex = 124
        Me.Label8.Text = "위탁기관명"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdUpLoad_List
        '
        Me.spdUpLoad_List.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdUpLoad_List.DataSource = Nothing
        Me.spdUpLoad_List.Location = New System.Drawing.Point(12, 33)
        Me.spdUpLoad_List.Name = "spdUpLoad_List"
        Me.spdUpLoad_List.OcxState = CType(resources.GetObject("spdUpLoad_List.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdUpLoad_List.Size = New System.Drawing.Size(1200, 516)
        Me.spdUpLoad_List.TabIndex = 12
        '
        'txtRegNo
        '
        Me.txtRegNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegNo.Location = New System.Drawing.Point(723, 9)
        Me.txtRegNo.MaxLength = 8
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(109, 21)
        Me.txtRegNo.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.Location = New System.Drawing.Point(654, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 21)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "등록번호"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(220, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "~"
        '
        'dtpUpDateE
        '
        Me.dtpUpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUpDateE.Location = New System.Drawing.Point(238, 9)
        Me.dtpUpDateE.Name = "dtpUpDateE"
        Me.dtpUpDateE.Size = New System.Drawing.Size(91, 21)
        Me.dtpUpDateE.TabIndex = 7
        '
        'dtpUpDateS
        '
        Me.dtpUpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUpDateS.Location = New System.Drawing.Point(122, 9)
        Me.dtpUpDateS.Name = "dtpUpDateS"
        Me.dtpUpDateS.Size = New System.Drawing.Size(91, 21)
        Me.dtpUpDateS.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(14, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 21)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "UpLoad 일자"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.btnQuery)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.btnExit)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 595)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1244, 34)
        Me.Panel3.TabIndex = 112
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(550, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 190
        Me.Button1.Text = "테스트"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems1
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.4672897!
        Me.btnQuery.FocalPoints.CenterPtY = 0.16!
        Me.btnQuery.FocalPoints.FocusPtX = 0!
        Me.btnQuery.FocalPoints.FocusPtY = 0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(931, 4)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(107, 25)
        Me.btnQuery.TabIndex = 189
        Me.btnQuery.Text = "조  회(F3)"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems2
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4672897!
        Me.btnClear.FocalPoints.CenterPtY = 0.16!
        Me.btnClear.FocalPoints.FocusPtX = 0!
        Me.btnClear.FocalPoints.FocusPtY = 0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker4
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1040, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 187
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems3
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5164835!
        Me.btnExit.FocalPoints.CenterPtY = 0.8!
        Me.btnExit.FocalPoints.FocusPtX = 0!
        Me.btnExit.FocalPoints.FocusPtY = 0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker6
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1148, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(91, 25)
        Me.btnExit.TabIndex = 188
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGJ06
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1244, 629)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.tabExLab)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGJ06"
        Me.Text = "위탁검사 리스트 작성"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tabExLab.ResumeLayout(False)
        Me.tabUpLoad.ResumeLayout(False)
        Me.tabUpLoad.PerformLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabUpLoad_List.ResumeLayout(False)
        Me.tabUpLoad_List.PerformLayout()
        CType(Me.spdUpLoad_List, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbDiplay_Init()

        With spdList
            .MaxRows = 0
        End With

        With spdUpLoad_List
            .MaxRows = 0
        End With

        Me.txtEtc.Text = ""
        Me.txtBcNo.Text = ""
        Me.chkQryAll.Checked = False

#If DEBUG Then
        Me.Button1.Visible = True
#Else
        Me.Button1.Visible = false
#End If

        Me.lblFileNm.Text = ""

    End Sub

    Private Sub FGR06_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim sFn As String = ""

        spdList.AllowColMove = False
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGR06_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3
                btnQuery_Click(Nothing, Nothing)
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGR06_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

        Me.dtpDateS.Value = CDate(Format(DateAdd(DateInterval.Day, -1, Now), "yyyy-MM-dd").ToString + " 13:00:00")
        Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")

        sbDiplay_Init()

        sbDisplay_Bccls("")
        sbDisplay_ExLab()

        spdList.MaxRows = 0

    End Sub

    Private Sub sbDisplay_ExLab()
        Dim sFn As String = "Sub sbDisplay_ExLab()"

        Try

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_ExLab_List()

            Me.cboExLab.Items.Clear() : Me.cboExlab_up.Items.Clear()
            Me.cboExlab_up.Items.Add("[   ] 전체")
            For ix = 0 To dt.Rows.Count - 1
                Me.cboExLab.Items.Add("[" + dt.Rows(ix).Item("exlabcd").ToString.Trim + "] " + dt.Rows(ix).Item("exlabnmd").ToString.Trim)
                Me.cboExlab_up.Items.Add("[" + dt.Rows(ix).Item("exlabcd").ToString.Trim + "] " + dt.Rows(ix).Item("exlabnmd").ToString.Trim)
            Next

            If Me.cboExLab.Items.Count > 0 Then Me.cboExLab.SelectedIndex = 0
            If Me.cboExlab_up.Items.Count > 0 Then Me.cboExlab_up.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Me.Close()

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDiplay_Init()
    End Sub

    Private Sub cboExLab_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboExLab.SelectedIndexChanged, cboExlab_up.SelectedValueChanged

        sbDiplay_Init()

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            If Me.tabExLab.SelectedTab.Text.Trim = "[ 위탁검사 리스트 작성 ]" Then
                If chkQryAll.Checked = False Then spdList.MaxRows = 0

                Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_SpcInfo_ExLab(Ctrl.Get_Code(cboExLab), Ctrl.Get_Code(cboBcclsCd), Me.dtpDateS.Text.Replace("-", "").Replace(" ", ""), Me.dtpDateE.Text.Replace("-", "").Replace(" ", ""), Me.chkQryAll.Checked)
                If dt.Rows.Count > 0 Then
                    sbDisplay_List(dt)
                End If
            Else

                Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_UpLoad_List(Ctrl.Get_Code(Me.cboExlab_up), Me.dtpUpDateS.Text.Replace("-", "").Replace(" ", ""), Me.dtpUpDateE.Text.Replace("-", "").Replace(" ", ""), Me.txtRegNo.Text)
                If dt.Rows.Count > 0 Then
                    sbDisplay_List_up(dt)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    Private Sub txtBcNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcNo.GotFocus

        Me.txtBcNo.SelectionStart = 0
        Me.txtBcNo.SelectAll()

    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Try

            Me.txtBcNo.Text = Me.txtBcNo.Text.Replace("-", "").Trim

            If Me.txtBcNo.Text.Length <> 15 Then
                Me.txtBcNo.Text = LISAPP.COMM.BcnoFn.fnFind_BcNo(Me.txtBcNo.Text.Trim)
            End If

            Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_SpcInfo_ExLab(Ctrl.Get_Code(cboExLab), Ctrl.Get_Code(cboBcclsCd), Me.txtBcNo.Text, Me.chkQryAll.Checked)
            If dt.Rows.Count > 0 Then
                sbDisplay_List(dt)
                For ix As Integer = 1 To spdList.MaxRows
                    With spdList
                        .Row = ix
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text

                        If sBcNo = Me.txtBcNo.Text Then
                            .Row = ix
                            .Col = -1
                            .BackColor = Color.LightGreen
                        End If
                    End With
                Next
                Me.txtBcNo.Text = ""
            End If
            Me.txtBcNo.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub spdULoad_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick

        If e.col = spdList.GetColFromID("idno") Then
            With spdList
                .Row = e.row
                .Col = .GetColFromID("bcno") : Dim strBCNO As String = .Text
                .Col = .GetColFromID("idno") : Dim strIdno As String = .Text
                If strIdno.IndexOf("*"c) > 0 Then
                    .Col = .GetColFromID("idno") : .Text = LISAPP.APP_EXLAB.fnGet_PatInfo_IdNo(strBCNO)
                Else
                    .Text = .Text.Substring(0, 7) + "******"
                End If
            End With
        End If

    End Sub

    <DllImport("HttpDll.dll", SetLastError:=True, _
  CharSet:=CharSet.Ansi, ExactSpelling:=True, _
  CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function HttpReceiptSvr(ByVal OCSHEAD As String, ByVal OCSDATA As String, ByVal OCSTEMP As String) As String

    End Function

    Private Sub btnUpLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpLoad.Click

        Dim sfdlg As New System.Windows.Forms.SaveFileDialog
        Dim sInitDir As String = "c:\수탁검사"
        Dim sFileNm As String = ""

        Try
            If IO.Directory.Exists(sInitDir) = False Then
                IO.Directory.CreateDirectory(sInitDir)
            End If
            'SCL 
            If Ctrl.Get_Code(cboExLab) = "006" Then
                Dim alListSCL As New ArrayList

                With Me.spdList
                    .Col = .GetColFromID("chk") : .ColHidden = True

                    Dim sResult As String = ""
                    Dim sOcsHead As String = "022429|1"
                    Dim sOcsData As String = ""
                    Dim sOcsTemp As String = ""

                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                        .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text
                        .Col = .GetColFromID("tnmd") : Dim sTnm As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
                        .Col = .GetColFromID("spcnmd") : Dim sSpcNm As String = .Text
                        .Col = .GetColFromID("regno") : Dim sRegno As String = .Text
                        .Col = .GetColFromID("patnm") : Dim sPatnm As String = .Text
                        .Col = .GetColFromID("idno") : Dim sIdno As String = .Text.Replace("-", "").Substring(0, 7)
                        .Col = .GetColFromID("sex") : Dim sSex As String = .Text
                        .Col = .GetColFromID("wardno") : Dim sWard As String = .Text
                        .Col = .GetColFromID("deptcd") : Dim sDept As String = .Text
                        .Col = .GetColFromID("colldt") : Dim Colldt As String = .Text
                        .Col = .GetColFromID("etc") : Dim sEtc As String = .Text
                        .Col = .GetColFromID("doctornm") : Dim sDoctornm As String = .Text
                        .Col = .GetColFromID("imgyn") : Dim sImgyn As String = .Text

                        If sBcNo = "" Then Exit For

                        sOcsData += sBcNo + Convert.ToChar(124).ToString + sTestcd + "|" + sTnm + "|" + sSpcCd + "|" + sSpcNm + "|" + sRegno + "|" + _
                                    sPatnm + "|" + sIdno + "|" + sSex + "|" + sWard + "|" + sDept + "|" + Colldt + "|" + sEtc + "|" + sDoctornm + "|" + sImgyn

                        If iRow <> .MaxRows Then
                            sOcsData += Convert.ToChar(4).ToString + ""
                        End If

                    Next

                    sResult = HttpReceiptSvr(sOcsHead, sOcsData, sOcsTemp)

                    If sResult.Split("|"c)(0) <> "00000" Then
                        MsgBox("SCL 위탁접수 업로드 오류 : " + sResult + " errorcode :" + sResult.Split("|"c)(0))
                        Return
                    End If
                End With
            ElseIf Ctrl.Get_Code(cboExLab) = "005" Then '삼광
                Dim aryRst As New ArrayList

                With spdList
                    .Col = .GetColFromID("chk") : .ColHidden = True

                    For iRow As Integer = 1 To .MaxRows
                        Dim objRst As New SML_Data
                        .Row = iRow
                        .Col = .GetColFromID("chk")
                        If .Text = "1" Then
                            objRst.sCUCD = "45436" '병원구분코드(거래처 코드)
                            .Col = .GetColFromID("tkdt") : objRst.sJSDT = .Text '접수일자
                            .Col = .GetColFromID("bcno") : objRst.sKSEQ = .Text '검체ID
                            .Col = .GetColFromID("testcd") : objRst.sHGCD = .Text '병원검사코드
                            .Col = .GetColFromID("tnmd") : objRst.sHGNM = .Text '병원검사명
                            .Col = .GetColFromID("spccd") : objRst.sKCCD = .Text '검체코드
                            .Col = .GetColFromID("spcnmd") : objRst.sKCNM = .Text '검체명
                            .Col = .GetColFromID("regno") : objRst.sCHNO = .Text '차트번호
                            .Col = .GetColFromID("patnm") : objRst.sPTNM = .Text '수진자명
                            .Col = .GetColFromID("idno") : objRst.sJNID = .Text.Replace("-", "").Substring(0, 7) '주민번호
                            .Col = .GetColFromID("sex") : objRst.sSEXX = .Text '성별
                            .Col = .GetColFromID("age") : objRst.sAGEE = .Text '나이
                            .Col = .GetColFromID("doctornm") : objRst.sMENM = .Text '의사명
                            .Col = .GetColFromID("wardno") : objRst.sWARD = .Text '병동
                            .Col = .GetColFromID("deptcd") : objRst.sJKNM = .Text '진료과
                            .Col = .GetColFromID("colldt") : objRst.sPIDT = .Text '채취일자

                            aryRst.Add(objRst) '배열리스트에 대입
                        End If
                    Next

                    Dim sErMsg As String = LISAPP.APP_EXLAB.fnExe_UpLoad_SML(aryRst)

                    If sErMsg <> "" Then
                        MsgBox(sErMsg)
                        Return
                    End If
                End With

            ElseIf Ctrl.Get_Code(cboExLab) = "008" Then '녹십자
                Dim aryRst As New ArrayList

                With spdList
                    .Col = .GetColFromID("chk") : .ColHidden = True

                    For iRow As Integer = 1 To .MaxRows
                        Dim objRst As New GCLAB_Data
                        .Row = iRow
                        .Col = .GetColFromID("chk")
                        If .Text = "1" Then

                            objRst.sCSTCD = "41666" '병원코드(거래처코드)
                            .Col = .GetColFromID("bcno") : objRst.sSAMPLENO = .Text '검체번호
                            .Col = .GetColFromID("testcd") : objRst.sCSTITEMCD = .Text '검사코드
                            .Col = .GetColFromID("tnmd") : objRst.sCSTITEMNM = .Text '검사명
                            .Col = .GetColFromID("regno") : objRst.sHOSNO = .Text '등록번호
                            .Col = .GetColFromID("patnm") : objRst.sPATNM = .Text '환자명
                            .Col = .GetColFromID("spccd") : objRst.sSAMPLECD = .Text '검체코드
                            .Col = .GetColFromID("spcnmd") : objRst.sSAMPLENM = .Text '검체명
                            .Col = .GetColFromID("idno") : objRst.sBIRDTE = .Text.Replace("-", "").Substring(0, 7) '주민번호
                            .Col = .GetColFromID("sex") : objRst.sSEX = .Text '성별
                            .Col = .GetColFromID("wardno") : objRst.sHOSLOC = .Text '병동
                            .Col = .GetColFromID("deptcd") : objRst.sHOSPLC = .Text '진료과
                            .Col = .GetColFromID("doctornm") : objRst.sDOCNM = .Text '의사명
                            .Col = .GetColFromID("colldt") : Dim colldt As String = .Text '채혈시간
                            objRst.sSAMDTE = colldt.Substring(0, 8) '채혈일자

                            aryRst.Add(objRst) '배열리스트에 대입
                        End If
                    Next

                    Dim sErMsg As String = LISAPP.APP_EXLAB.fnExe_UpLoad_GCRL(aryRst)

                    If sErMsg <> "" Then
                        MsgBox(sErMsg)
                        Return
                    End If
                End With
            End If


            With sfdlg
                .CheckPathExists = True

                .DefaultExt = "xls"
                .Filter = "Excel files (*.xls)|*.xls"
                .InitialDirectory = sInitDir
                .FileName = Ctrl.Get_Code(cboExLab) + "_" + Format(Now, "yyMMdd").ToString
                .OverwritePrompt = True

                If Ctrl.Get_Code(cboExLab) = "006" Or Ctrl.Get_Code(cboExLab) = "005" Or Ctrl.Get_Code(cboExLab) = "008" Then
                    sFileNm = .FileName
                Else
                    If .ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                        Return
                    Else
                        sFileNm = .FileName
                    End If
                End If
                

            End With

            If sFileNm = "" Then Return

            Me.lblFileNm.Text = sFileNm

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim alList As New ArrayList

            With Me.spdList
                .Col = .GetColFromID("chk") : .ColHidden = True

                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("chk")
                    If .Text = "1" Then
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                        .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
                        .Col = .GetColFromID("etc") : Dim sEtc As String = .Text

                        If sBcNo = "" Then Exit For

                        alList.Add(sBcNo + "|" + sTestcd + "|" + sSpcCd + "|" + sEtc + "|")
                    End If
                Next
            End With

            Dim sErrMsg As String = LISAPP.APP_EXLAB.fnExe_UpLoad(Ctrl.Get_Code(cboExLab), sFileNm, STU_AUTHORITY.UsrID, Me.txtEtc.Text, alList)
            If sErrMsg <> "" Then
                MsgBox(sErrMsg)
                Return
            End If

            If Me.spdList.ExportToExcel(sFileNm, "Up Load", "") Then
                MsgBox("UpLoad 성공" + vbCrLf + vbCrLf + sFileNm, MsgBoxStyle.Information)
            End If

            Me.spdList.Col = spdList.GetColFromID("chk") : spdList.ColHidden = False



        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        End Try

    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        sbPrint_Data()

    End Sub

    Private Sub btnGetUpLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetUpLoad.Click

        Dim sFn As String = "Sub btnHelp_Tcls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp_Tcls.Click"
        Try
            spdList.MaxRows = 0

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_UpLoad_FileList(Me.dtpDateS.Text.Replace("-", "").Replace(" ", ""), Me.dtpDateE.Text.Replace("-", "").Replace(" ", ""))

            objHelp.FormText = "위탁검사 의뢰내역"
            objHelp.MaxRows = 15
            objHelp.Distinct = True

            objHelp.AddField("filenm", "파일명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("regnm", "작성자", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, , , "regnm")
            objHelp.AddField("regdt", "작성일자", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, , , "regdt")
            objHelp.AddField("exlabcd", "코드", 0, , , True, "exlabcd")
            objHelp.AddField("exlabnmd", "위탁기관", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , "exlabnmd")

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnGetUpLoad)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - btnGetUpLoad.Left, pntFrmXY.Y + pntCtlXY.Y + btnGetUpLoad.Height + 80, dt)

            Dim sFileNm As String = "", sExLabCd As String = ""

            If alList.Count > 0 Then
                sFileNm = alList.Item(0).ToString.Split("|"c)(0)
                sExLabCd = alList.Item(0).ToString.Split("|"c)(3)
            End If

            If sFileNm = "" Then Return
            Me.lblFileNm.Text = sFileNm.Trim

            dt = LISAPP.APP_EXLAB.fnGet_UpLoad_List(sExLabCd, sFileNm)

            If dt.Rows.Count < 1 Then Return

            Me.spdList.ReDraw = False

            For intIdx As Integer = 0 To dt.Rows.Count - 1
                If intIdx = 0 Then Me.txtEtc.Text = dt.Rows(intIdx).Item("cmtcont").ToString

                Dim sPatInfo() As String = dt.Rows(intIdx).Item("patinfo").ToString.Split("|"c)

                With spdList
                    .MaxRows += 1
                    .Row = .MaxRows
                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(intIdx).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(intIdx).Item("testcd").ToString.Trim
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(intIdx).Item("tnmd").ToString.Trim
                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(intIdx).Item("spccd").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(intIdx).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(intIdx).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                    .Col = .GetColFromID("idno") : .Text = sPatInfo(3).Trim
                    .Col = .GetColFromID("sex") : .Text = sPatInfo(1).Trim
                    .Col = .GetColFromID("wardno") : .Text = dt.Rows(intIdx).Item("wardno").ToString.Trim
                    .Col = .GetColFromID("deptcd") : .Text = dt.Rows(intIdx).Item("deptcd").ToString.Trim
                    .Col = .GetColFromID("colldt") : .Text = dt.Rows(intIdx).Item("colldt").ToString.Trim
                    .Col = .GetColFromID("etc") : .Text = dt.Rows(intIdx).Item("remark").ToString.Trim
                    .Col = .GetColFromID("filenm") : .Text = sFileNm
                End With
            Next
            spdList.ReDraw = True

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub btnSelDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelDel.Click
        With spdList
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .Text = "1" Then
                    .Row = intRow
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1
                    intRow -= 1

                End If

                If intRow < 0 Then Exit For
            Next
        End With
    End Sub

    Private Sub btnUpLoad_Del_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpLoad_Del.Click
        Dim sfdlg As New System.Windows.Forms.SaveFileDialog

        Try

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim arlList As New ArrayList

            With spdList

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("bcno") : Dim strBcNo As String = .Text
                    .Col = .GetColFromID("testcd") : Dim strtestcd As String = .Text
                    .Col = .GetColFromID("spccd") : Dim strSpcCd As String = .Text
                    .Col = .GetColFromID("etc") : Dim strEtc As String = .Text
                    .Col = .GetColFromID("filenm") : Dim strFileNm As String = .Text
                    .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                    If strBcNo = "" Then Exit For

                    If strChk = "1" Then
                        arlList.Add(strBcNo + "|" + strtestcd + "|" + strSpcCd + "|" + strEtc + "|" + strFileNm + "|")
                    End If
                Next
            End With

            Dim strErrMsg As String = LISAPP.APP_EXLAB.fnExe_UpLoad_Del(Ctrl.Get_Code(cboExLab), STU_AUTHORITY.UsrID, txtEtc.Text, arlList)
            If strErrMsg <> "" Then
                MsgBox(strErrMsg)
                Return
            End If

            btnQuery_Click(Nothing, Nothing)

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        End Try
    End Sub


    Private Sub sbDisplay_List_up(ByVal r_dt As DataTable)
        Try
            With spdUpLoad_List
                .MaxRows = 0

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    Dim sPatInfo() As String = r_dt.Rows(ix).Item("patinfo").ToString.Split("|"c)


                    .MaxRows += 1
                    .Row = .MaxRows
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix).Item("testcd").ToString.Trim
                    .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix).Item("tnmd").ToString.Trim
                    .Col = .GetColFromID("state")

                    Select Case r_dt.Rows(ix).Item("rstflg").ToString.Trim
                        Case "1"
                            .Text = "결과"
                        Case "2"
                            .Text = "중간보고"
                        Case "3"
                            .Text = "최종보고"
                        Case Else
                            .Text = "UpLoad"
                    End Select
                    .Col = .GetColFromID("imgyn") : .Text = r_dt.Rows(ix).Item("imgyn").ToString.Trim
                    .Col = .GetColFromID("remark") : .Text = r_dt.Rows(ix).Item("remark").ToString.Trim
                    .Col = .GetColFromID("regdt") : .Text = r_dt.Rows(ix).Item("regdt").ToString.Trim

                    '20210104 jhs 채혈시 주의사항 항목 추가 
                    .Col = .GetColFromID("cwarning") : .Text = r_dt.Rows(ix).Item("cwarning").ToString.Trim
                    '----------------------------
                Next
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub
    Private Sub txtRegNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.GotFocus
        Me.txtRegNo.SelectionStart = 0
        Me.txtRegNo.SelectAll()
    End Sub

    Private Sub txtRegNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRegNo.Click
        txtRegNo.SelectAll()
    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        If IsNumeric(Me.txtRegNo.Text.Substring(0, 1)) Then
            Me.txtRegNo.Text = Me.txtRegNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
        Else
            Me.txtRegNo.Text = Me.txtRegNo.Text.Substring(0, 1) + Me.txtRegNo.Text.Substring(PRG_CONST.Len_RegNo - 1).PadLeft(9, "0"c)
        End If

        Me.btnQuery_Click(Nothing, Nothing)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        Try

            '-- 테스트용



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
End Class

Public Class ExLabInfo
    Public ExLabDate As String = ""
    Public RegNo As String = ""
    Public BcNo As String = ""
    Public PatNm As String = ""
    Public testcd As String = ""
    Public RstVal As String = ""
    Public Comment As String = ""
    Public SpcCd As String = ""
    Public SpcFlag As String = ""
    Public RstFlag As String = ""
    Public Tnmd As String = ""
    Public OldRst As String = ""
    Public CRegNo As String = ""
End Class

Public Class FGR06_PATINFO
    Public sSeqNo As String = ""
    Public sBcNo As String = ""
    Public stestcd As String = ""
    Public sTnmd As String = ""
    Public sSpcCd As String = ""
    Public sSpcNmd As String = ""
    Public sRegNo As String = ""
    Public sPatNm As String = ""
    Public sIdNo As String = ""
    Public sSex As String = ""
    Public sDeptNm As String = ""
    Public sWard As String = ""
    Public sCollDt As String = ""
    Public sEtc As String = ""

    Public sComment As String = ""
End Class


Public Class FGR06_PRINT
    Private Const msFile As String = "File : FGR06.vb, Class : J01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0
    Private miTitle_ExmCnt As Integer = 0
    Private miCCol As Integer = 1

    Public ma_col As New ArrayList


    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msTitle As String
    Public maPrtData As ArrayList
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd hh:mm")

    Public Sub sbPrint_Preview()
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = True

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miCIdx = 0
        miCCol = 1
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 9, FontStyle.Regular)
        Dim fnt_Tnmd As New Font("굴림체", 8, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
        Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = fnt_Body.GetHeight(e.Graphics)

        Dim rect As New Drawing.RectangleF

        Dim sngTmp As Single = 0
        Dim intCnt As Integer = 0

        If miCIdx = 0 Then miPageNo = 0

        Dim intIdx As Integer = miCCol
        For intIdx = miCIdx To maPrtData.Count - 1

            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            If CType(maPrtData.Item(intIdx), FGR06_PATINFO).stestcd = "" Then
                '-- 코멘트
                rect = New Drawing.RectangleF(msgPosX(1), sngPosY, msgPosX(14) - msgPosX(0), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sComment, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Else
                '-- 번호
                rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sSeqNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                '-- 검체번호
                rect = New Drawing.RectangleF(msgPosX(1), sngPosY, msgPosX(2) - msgPosX(1), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sBcNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 환자명
                rect = New Drawing.RectangleF(msgPosX(2), sngPosY, msgPosX(3) - msgPosX(2), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 등록번호
                rect = New Drawing.RectangleF(msgPosX(3), sngPosY, msgPosX(4) - msgPosX(3), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 검사코드
                rect = New Drawing.RectangleF(msgPosX(4), sngPosY, msgPosX(5) - msgPosX(4), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).stestcd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 검사명
                rect = New Drawing.RectangleF(msgPosX(5), sngPosY, msgPosX(6) - msgPosX(5), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sTnmd, fnt_Tnmd, Drawing.Brushes.Black, rect, sf_l)
                '-- 검체코드
                rect = New Drawing.RectangleF(msgPosX(6), sngPosY, msgPosX(7) - msgPosX(6), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sSpcCd, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                '-- 검체명
                rect = New Drawing.RectangleF(msgPosX(7), sngPosY, msgPosX(8) - msgPosX(7), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sSpcNmd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 주민등록번호
                rect = New Drawing.RectangleF(msgPosX(8), sngPosY, msgPosX(9) - msgPosX(8), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sIdNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 성별
                rect = New Drawing.RectangleF(msgPosX(9), sngPosY, msgPosX(10) - msgPosX(9), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sSex, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                '-- 병동
                rect = New Drawing.RectangleF(msgPosX(10), sngPosY, msgPosX(11) - msgPosX(10), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sWard, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 진료과
                rect = New Drawing.RectangleF(msgPosX(11), sngPosY, msgPosX(12) - msgPosX(11), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sDeptNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 접수일
                rect = New Drawing.RectangleF(msgPosX(12), sngPosY, msgPosX(13) - msgPosX(12), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sCollDt, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                '-- 비고
                rect = New Drawing.RectangleF(msgPosX(13), sngPosY, msgPosX(14) - msgPosX(13), sngPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGR06_PATINFO).sEtc, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            End If

            miCIdx += 1

            sngPosY += sngPrtH
            If msgHeight - sngPrtH * 4 < sngPosY Then Exit For

        Next

        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

        If miCIdx < maPrtData.Count - 1 Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1

        Dim sngPosX(0 To 14) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 30
        sngPosX(2) = sngPosX(1) + 110
        sngPosX(3) = sngPosX(2) + 60
        sngPosX(4) = sngPosX(3) + 80
        sngPosX(5) = sngPosX(4) + 60
        sngPosX(6) = sngPosX(5) + 240
        sngPosX(7) = sngPosX(6) + 40
        sngPosX(8) = sngPosX(7) + 100
        sngPosX(9) = sngPosX(8) + 110
        sngPosX(10) = sngPosX(9) + 20
        sngPosX(11) = sngPosX(10) + 40
        sngPosX(12) = sngPosX(11) + 80
        sngPosX(13) = sngPosX(12) + 100
        sngPosX(14) = msgWidth

        'sngPosX(0) = msgLeft
        'sngPosX(1) = sngPosX(0) + 30
        'sngPosX(2) = sngPosX(1) + 110
        'sngPosX(3) = sngPosX(2) + 60
        'sngPosX(4) = sngPosX(3) + 240
        'sngPosX(5) = sngPosX(4) + 40
        'sngPosX(6) = sngPosX(5) + 100
        'sngPosX(7) = sngPosX(6) + 80
        'sngPosX(8) = sngPosX(7) + 60
        'sngPosX(9) = sngPosX(8) + 110
        'sngPosX(10) = sngPosX(9) + 20
        'sngPosX(11) = sngPosX(10) + 40
        'sngPosX(12) = sngPosX(11) + 80
        'sngPosX(13) = sngPosX(12) + 100
        'sngPosX(14) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = fnt_Head.GetHeight(e.Graphics)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - msgLeft - 25, sngPrt), sf_r)

        sngPosY += sngPrt * 2

        sngPrt = Convert.ToInt16(sngPrt * 1.7)
        fnPrtTitle = sngPosY + sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY + sngPrt * 0, sngPosX(1) - sngPosX(0), sngPrt), sf_c)

        e.Graphics.DrawString("검체번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY, sngPosX(2) - sngPosX(1), sngPrt), sf_c)
        e.Graphics.DrawString("환자명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, sngPosX(3) - sngPosX(2), sngPrt), sf_c)
        e.Graphics.DrawString("등록번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY, sngPosX(4) - sngPosX(3), sngPrt), sf_c)
        e.Graphics.DrawString("검사코드", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, sngPosX(5) - sngPosX(4), sngPrt), sf_c)
        e.Graphics.DrawString("검사명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5), sngPosY, sngPosX(6) - sngPosX(5), sngPrt), sf_c)

        e.Graphics.DrawString("검체코드", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(6), sngPosY, sngPosX(7) - sngPosX(6), sngPrt), sf_c)
        e.Graphics.DrawString("검체명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(7), sngPosY, sngPosX(8) - sngPosX(7), sngPrt), sf_c)

        e.Graphics.DrawString("주민번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(8), sngPosY, sngPosX(9) - sngPosX(8), sngPrt), sf_c)
        e.Graphics.DrawString("성별", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(9), sngPosY, sngPosX(10) - sngPosX(9), sngPrt), sf_c)
        e.Graphics.DrawString("병동", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(10), sngPosY, sngPosX(11) - sngPosX(10), sngPrt), sf_c)
        e.Graphics.DrawString("진료과", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(11), sngPosY, sngPosX(12) - sngPosX(11), sngPrt), sf_c)
        e.Graphics.DrawString("접수일", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(12), sngPosY, sngPosX(13) - sngPosX(12), sngPrt), sf_c)
        e.Graphics.DrawString("기타", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(13), sngPosY, sngPosX(14) - sngPosX(13), sngPrt), sf_c)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt, msgWidth, sngPosY + sngPrt)

        msgPosX = sngPosX

    End Function

End Class




















