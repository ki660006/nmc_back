'>>> 검사부서별 검체접수

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports common.commlogin.login
Imports LISAPP.APP_J
Imports LISAPP.APP_J.TkFn

Public Class FGJ04
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGJ04.vb, Class : J01" & vbTab

    Private Const msXMLDir As String = "\XML"
    Private msPART As String = Application.StartupPath + msXMLDir & "\FGJ04_PARTINFO.XML"

    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCollNm As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTkNm As System.Windows.Forms.Label
    Friend WithEvents lblBcColor3 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpTkDt1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTkDt0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboPart As System.Windows.Forms.ComboBox
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm3 As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm2 As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm1 As System.Windows.Forms.Label
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Public WithEvents lblBcColor0 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnSelBCPRT As System.Windows.Forms.Button
    Friend WithEvents lblBarPrinter As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkBarInit As System.Windows.Forms.CheckBox

    Private msSEP_Display As String = "，"

#Region " Local 함수 "

    Private Sub sbPrint_BarCode(ByVal rsBcNo As String)

        Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(Me.Name)
        Try
            Dim dt As DataTable = fnGet_Jubsu_BarCode_Info(rsBcNo, "J2")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim alBcNo As New ArrayList

                alBcNo.Add(rsBcNo)

                If dt.Rows(ix).Item("mbttype").ToString = "2" Or dt.Rows(ix).Item("mbttype").ToString = "3" Then
                    objBCPrt.PrintDo_Micro(alBcNo, "1")
                Else
                    objBCPrt.PrintDo(alBcNo, "1")
                End If
            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub sbSetWorkNo(ByVal rsBcNo As String, ByVal rsWorkNo As String)

        Dim strBcno As String

        For intRow As Integer = spdList.MaxRows To 1 Step -1
            With spdList
                .Row = intRow
                .Col = .GetColFromID("bcno")
                strBcno = .Text

                If strBcno.Substring(0, 14) = rsBcNo.Substring(0, 14) Then
                    .Row = intRow
                    .Col = .GetColFromID("workno")
                    If .Text = "" Then
                        .Col = .GetColFromID("workno_old")
                        .Text = rsWorkNo.Replace("-", "")
                    End If
                End If
            End With
        Next

    End Sub

    Private Sub sbDisplay_Color_bccls()
        Dim sFn As String = "Private Sub sbGet_Data_LisCmt"
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_bccls_color
            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(ix).Item("colorgbn").ToString
                        Case "1"
                            lblBcclsNm1.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor1.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor1.ForeColor = COLOR_BCCLSCD.FrColor(dt.Rows(ix).Item("colorgbn").ToString)
                        Case "2"
                            lblBcclsNm2.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor2.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor2.ForeColor = COLOR_BCCLSCD.FrColor(dt.Rows(ix).Item("colorgbn").ToString)
                        Case "3"
                            lblBcclsNm3.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor3.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor3.ForeColor = COLOR_BCCLSCD.FrColor(dt.Rows(ix).Item("colorgbn").ToString)
                    End Select
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
        End Try

    End Sub

#End Region


#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbFormInitialize()

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCollDt As System.Windows.Forms.Label
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents lblTkDt As System.Windows.Forms.Label
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents lblBcColor2 As System.Windows.Forms.Label
    Friend WithEvents lblBcColor1 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGJ04))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblTkNm = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblCollNm = New System.Windows.Forms.Label
        Me.lblCollDt = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblSpcNm = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblTkDt = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.btnSelBCPRT = New System.Windows.Forms.Button
        Me.lblBarPrinter = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblUserId = New System.Windows.Forms.Label
        Me.btnExcel = New CButtonLib.CButton
        Me.btnReg = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblBcColor0 = New System.Windows.Forms.Label
        Me.lblBcclsNm3 = New System.Windows.Forms.Label
        Me.lblBcclsNm2 = New System.Windows.Forms.Label
        Me.lblBcclsNm1 = New System.Windows.Forms.Label
        Me.lblBcColor3 = New System.Windows.Forms.Label
        Me.lblBcColor2 = New System.Windows.Forms.Label
        Me.lblBcColor1 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.btnToggle = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.cboPart = New System.Windows.Forms.ComboBox
        Me.dtpTkDt1 = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtpTkDt0 = New System.Windows.Forms.DateTimePicker
        Me.btnQuery = New CButtonLib.CButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblRemark = New System.Windows.Forms.Label
        Me.chkBarInit = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Panel1.Location = New System.Drawing.Point(4, 44)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1224, 464)
        Me.Panel1.TabIndex = 4
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1220, 460)
        Me.spdList.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.lblTkNm)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.lblCollNm)
        Me.GroupBox3.Controls.Add(Me.lblCollDt)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblSpcNm)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.lblTkDt)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(556, 509)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(374, 83)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(213, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 22)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "1차 접수자"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTkNm
        '
        Me.lblTkNm.BackColor = System.Drawing.Color.White
        Me.lblTkNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTkNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTkNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTkNm.Location = New System.Drawing.Point(282, 58)
        Me.lblTkNm.Name = "lblTkNm"
        Me.lblTkNm.Size = New System.Drawing.Size(86, 22)
        Me.lblTkNm.TabIndex = 9
        Me.lblTkNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(213, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 22)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "채혈자"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCollNm
        '
        Me.lblCollNm.BackColor = System.Drawing.Color.White
        Me.lblCollNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCollNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCollNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCollNm.Location = New System.Drawing.Point(282, 35)
        Me.lblCollNm.Name = "lblCollNm"
        Me.lblCollNm.Size = New System.Drawing.Size(86, 22)
        Me.lblCollNm.TabIndex = 7
        Me.lblCollNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCollDt
        '
        Me.lblCollDt.BackColor = System.Drawing.Color.White
        Me.lblCollDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCollDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCollDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCollDt.Location = New System.Drawing.Point(79, 35)
        Me.lblCollDt.Name = "lblCollDt"
        Me.lblCollDt.Size = New System.Drawing.Size(132, 22)
        Me.lblCollDt.TabIndex = 3
        Me.lblCollDt.Text = "2009-12-12 10:12:23"
        Me.lblCollDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(5, 58)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 22)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "1차접수일시"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcNm
        '
        Me.lblSpcNm.BackColor = System.Drawing.Color.White
        Me.lblSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSpcNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSpcNm.Location = New System.Drawing.Point(79, 12)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(289, 22)
        Me.lblSpcNm.TabIndex = 1
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(5, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 22)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "채혈일시"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTkDt
        '
        Me.lblTkDt.BackColor = System.Drawing.Color.White
        Me.lblTkDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTkDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTkDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTkDt.Location = New System.Drawing.Point(79, 58)
        Me.lblTkDt.Name = "lblTkDt"
        Me.lblTkDt.Size = New System.Drawing.Size(132, 22)
        Me.lblTkDt.TabIndex = 5
        Me.lblTkDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(5, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "검체명"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.lblUserNm)
        Me.Panel3.Controls.Add(Me.lblUserId)
        Me.Panel3.Controls.Add(Me.btnExcel)
        Me.Panel3.Controls.Add(Me.btnReg)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.btnExit)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 595)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1234, 34)
        Me.Panel3.TabIndex = 7
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkBarInit)
        Me.Panel5.Controls.Add(Me.btnSelBCPRT)
        Me.Panel5.Controls.Add(Me.lblBarPrinter)
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Location = New System.Drawing.Point(6, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(306, 24)
        Me.Panel5.TabIndex = 191
        '
        'btnSelBCPRT
        '
        Me.btnSelBCPRT.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSelBCPRT.ForeColor = System.Drawing.Color.Black
        Me.btnSelBCPRT.Image = CType(resources.GetObject("btnSelBCPRT.Image"), System.Drawing.Image)
        Me.btnSelBCPRT.Location = New System.Drawing.Point(277, 0)
        Me.btnSelBCPRT.Name = "btnSelBCPRT"
        Me.btnSelBCPRT.Size = New System.Drawing.Size(30, 24)
        Me.btnSelBCPRT.TabIndex = 188
        Me.btnSelBCPRT.UseVisualStyleBackColor = False
        '
        'lblBarPrinter
        '
        Me.lblBarPrinter.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBarPrinter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBarPrinter.ForeColor = System.Drawing.Color.Black
        Me.lblBarPrinter.Location = New System.Drawing.Point(92, 1)
        Me.lblBarPrinter.Name = "lblBarPrinter"
        Me.lblBarPrinter.Size = New System.Drawing.Size(185, 23)
        Me.lblBarPrinter.TabIndex = 102
        Me.lblBarPrinter.Text = "AUTO LABELER (외래채혈실)"
        Me.lblBarPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 23)
        Me.Label7.TabIndex = 101
        Me.Label7.Text = " 출력프린터"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(416, 8)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(76, 20)
        Me.lblUserNm.TabIndex = 1
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(344, 8)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(68, 20)
        Me.lblUserId.TabIndex = 0
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems1
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5384616!
        Me.btnExcel.FocalPoints.CenterPtY = 0.4!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker2
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(830, 4)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(91, 25)
        Me.btnExcel.TabIndex = 190
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems2
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.4672897!
        Me.btnReg.FocalPoints.CenterPtY = 0.16!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker4
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(922, 4)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(107, 25)
        Me.btnReg.TabIndex = 189
        Me.btnReg.Text = "부서접수(F5)"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
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
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1030, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 187
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
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
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1138, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(91, 25)
        Me.btnExit.TabIndex = 188
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.lblBcColor0)
        Me.GroupBox2.Controls.Add(Me.lblBcclsNm3)
        Me.GroupBox2.Controls.Add(Me.lblBcclsNm2)
        Me.GroupBox2.Controls.Add(Me.lblBcclsNm1)
        Me.GroupBox2.Controls.Add(Me.lblBcColor3)
        Me.GroupBox2.Controls.Add(Me.lblBcColor2)
        Me.GroupBox2.Controls.Add(Me.lblBcColor1)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(933, 509)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(295, 83)
        Me.GroupBox2.TabIndex = 164
        Me.GroupBox2.TabStop = False
        '
        'lblBcColor0
        '
        Me.lblBcColor0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcColor0.BackColor = System.Drawing.Color.White
        Me.lblBcColor0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor0.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor0.Location = New System.Drawing.Point(5, 35)
        Me.lblBcColor0.Name = "lblBcColor0"
        Me.lblBcColor0.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor0.TabIndex = 204
        Me.lblBcColor0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBcColor0.Visible = False
        '
        'lblBcclsNm3
        '
        Me.lblBcclsNm3.Location = New System.Drawing.Point(226, 51)
        Me.lblBcclsNm3.Name = "lblBcclsNm3"
        Me.lblBcclsNm3.Size = New System.Drawing.Size(64, 12)
        Me.lblBcclsNm3.TabIndex = 25
        Me.lblBcclsNm3.Text = "기타"
        Me.lblBcclsNm3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBcclsNm2
        '
        Me.lblBcclsNm2.Location = New System.Drawing.Point(125, 51)
        Me.lblBcclsNm2.Name = "lblBcclsNm2"
        Me.lblBcclsNm2.Size = New System.Drawing.Size(72, 12)
        Me.lblBcclsNm2.TabIndex = 24
        Me.lblBcclsNm2.Text = "분자유전"
        Me.lblBcclsNm2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBcclsNm1
        '
        Me.lblBcclsNm1.Location = New System.Drawing.Point(28, 51)
        Me.lblBcclsNm1.Name = "lblBcclsNm1"
        Me.lblBcclsNm1.Size = New System.Drawing.Size(70, 12)
        Me.lblBcclsNm1.TabIndex = 23
        Me.lblBcclsNm1.Text = "혈액은행"
        Me.lblBcclsNm1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBcColor3
        '
        Me.lblBcColor3.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblBcColor3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcColor3.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor3.Location = New System.Drawing.Point(202, 48)
        Me.lblBcColor3.Name = "lblBcColor3"
        Me.lblBcColor3.Size = New System.Drawing.Size(18, 18)
        Me.lblBcColor3.TabIndex = 22
        Me.lblBcColor3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor2
        '
        Me.lblBcColor2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblBcColor2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcColor2.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor2.Location = New System.Drawing.Point(103, 48)
        Me.lblBcColor2.Name = "lblBcColor2"
        Me.lblBcColor2.Size = New System.Drawing.Size(18, 18)
        Me.lblBcColor2.TabIndex = 21
        Me.lblBcColor2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor1
        '
        Me.lblBcColor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(19, Byte), Integer))
        Me.lblBcColor1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcColor1.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor1.Location = New System.Drawing.Point(5, 48)
        Me.lblBcColor1.Name = "lblBcColor1"
        Me.lblBcColor1.Size = New System.Drawing.Size(18, 18)
        Me.lblBcColor1.TabIndex = 20
        Me.lblBcColor1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label15.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(5, 13)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(284, 22)
        Me.Label15.TabIndex = 19
        Me.Label15.Text = "범   례"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnToggle)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txtSearch)
        Me.GroupBox4.Controls.Add(Me.lblSearch)
        Me.GroupBox4.Controls.Add(Me.cboPart)
        Me.GroupBox4.Controls.Add(Me.dtpTkDt1)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.dtpTkDt0)
        Me.GroupBox4.Controls.Add(Me.btnQuery)
        Me.GroupBox4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(5, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(991, 36)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(837, 11)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(35, 21)
        Me.btnToggle.TabIndex = 171
        Me.btnToggle.Text = "<->"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(10, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 101
        Me.Label6.Text = "검사부서"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(711, 11)
        Me.txtSearch.MaxLength = 18
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(125, 21)
        Me.txtSearch.TabIndex = 170
        Me.txtSearch.Text = "20090805-C1-0003-0"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(645, 11)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(65, 21)
        Me.lblSearch.TabIndex = 172
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboPart
        '
        Me.cboPart.DropDownHeight = 200
        Me.cboPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPart.IntegralHeight = False
        Me.cboPart.ItemHeight = 12
        Me.cboPart.Location = New System.Drawing.Point(77, 11)
        Me.cboPart.Name = "cboPart"
        Me.cboPart.Size = New System.Drawing.Size(200, 20)
        Me.cboPart.TabIndex = 100
        '
        'dtpTkDt1
        '
        Me.dtpTkDt1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpTkDt1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTkDt1.Location = New System.Drawing.Point(503, 11)
        Me.dtpTkDt1.Name = "dtpTkDt1"
        Me.dtpTkDt1.Size = New System.Drawing.Size(88, 21)
        Me.dtpTkDt1.TabIndex = 167
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(334, 11)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 21)
        Me.Label14.TabIndex = 169
        Me.Label14.Text = "접수일자"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(489, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 16)
        Me.Label3.TabIndex = 168
        Me.Label3.Text = "~"
        '
        'dtpTkDt0
        '
        Me.dtpTkDt0.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpTkDt0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTkDt0.Location = New System.Drawing.Point(400, 11)
        Me.dtpTkDt0.Name = "dtpTkDt0"
        Me.dtpTkDt0.Size = New System.Drawing.Size(88, 21)
        Me.dtpTkDt0.TabIndex = 166
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems5.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems5
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 1.0!
        Me.btnQuery.FocalPoints.CenterPtY = 0.4090909!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker10
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(905, 10)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(75, 22)
        Me.btnQuery.TabIndex = 165
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblRemark)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 509)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(549, 83)
        Me.GroupBox1.TabIndex = 165
        Me.GroupBox1.TabStop = False
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(5, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 66)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "의뢰의사 Remark"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRemark
        '
        Me.lblRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRemark.BackColor = System.Drawing.Color.White
        Me.lblRemark.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRemark.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRemark.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRemark.Location = New System.Drawing.Point(76, 12)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(467, 66)
        Me.lblRemark.TabIndex = 8
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkBarInit
        '
        Me.chkBarInit.AutoSize = True
        Me.chkBarInit.Location = New System.Drawing.Point(73, 6)
        Me.chkBarInit.Name = "chkBarInit"
        Me.chkBarInit.Size = New System.Drawing.Size(15, 14)
        Me.chkBarInit.TabIndex = 225
        Me.chkBarInit.UseVisualStyleBackColor = True
        '
        'FGJ04
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1234, 629)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGJ04"
        Me.Text = "검사부서별 검체접수"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub FGJ04_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim sFn As String = "FGJ04_Activated"

        Try
            Me.txtSearch.Focus()
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

#Region " 메인 버튼 처리 "
    ' Function Key정의
    Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim sFn As String = "Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown"

        'F4 : 화면정리 
        'F5 : 파트접수
        'F10: 화면종료 

        If e.KeyCode = Keys.F5 Then
            btnReg_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.F4 Then
            btnClear_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()

        ElseIf e.KeyCode = Keys.Delete Then
            Try
                Debug.WriteLine("Mybase_KeyDown")

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
            End Try

        End If

    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.ButtonClick"

        Try
            If MsgBox("부서접수를 하시겠습니까?", MsgBoxStyle.YesNo, "일괄 파트 접수") = MsgBoxResult.Yes Then
                fnReg()
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = "Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.ButtonClick"

        Try
            sbForm_Clear()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region

#Region " Form내부 함수 "

    ' Form초기화
    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub fnFormInitialize()"

        Try
            Me.spdList.Tag = ""

            ' 로그인정보 설정
            Me.lblUserId.Text = USER_INFO.USRID
            Me.lblUserNm.Text = USER_INFO.USRNM

            sbSpreadColHidden(True)

            ' 기본 바코드프린터 설정
            Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    ' 칼럼 Hidden 유무
    Private Sub sbSpreadColHidden(ByVal rbFlag As Boolean)
        Dim sFn As String = "Private Sub fnSpreadColHidden(ByVal abFlag As Boolean)"

        Try
            With spdList
                .Col = .GetColFromID("spcflg") : .ColHidden = rbFlag
                .Col = .GetColFromID("workno_old") : .ColHidden = rbFlag
                .Col = .GetColFromID("takeyn") : .ColHidden = rbFlag
                .Col = .GetColFromID("tkdt") : .ColHidden = rbFlag
                .Col = .GetColFromID("tknm") : .ColHidden = rbFlag
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 검체부서
    Private Sub sbDisplay_part()
        Dim sFn As String = "Private Sub sbDisplay_slip()"

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Part_List(True) '' 정은 수정 

            cboPart.Items.Clear()

            If dt.Rows.Count > 0 Then
                cboPart.Items.Add("[  ] 전체")

                For ix As Integer = 0 To dt.Rows.Count - 1
                    cboPart.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
                Next

                cboPart.SelectedIndex = 0
            End If

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msPART, "PART")
            If sTmp <> "" Then
                If Me.cboPart.Items.Count > Convert.ToInt16(sTmp) Then Me.cboPart.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 검체선택후 해당 내역 표시
    ' 개별항목 접수는 바로 접수 처리, 일괄항목 접수는 리스트 표시 
    Private Function fnSelectList(ByVal rsBcNo As String, ByVal riCnt As Integer) As Boolean
        Dim sfn As String = "Private Sub fnSelectList(ByVal asBCNO As String, ByVal aiCCOUNT As Integer)"

        Try
            Dim sBcno_Full As String = Fn.BCNO_View(rsBcNo, True)

            If Fn.SpdColSearch(spdList, sBcno_Full, spdList.GetColFromID("bcno")) > 0 Then
                Return True
            End If

            Dim dt As DataTable = fnGet_Take2_PatInfo(rsBcNo, Ctrl.Get_Code(cboPart))

            If dt.Rows.Count < 1 Then
                Return False
            End If

            Dim dr As DataRow = dt.Rows(0)

            sBcno_Full = Fn.BCNO_View(dr.Item("bcno").ToString.Trim, True)

            With spdList
                Dim iRow As Integer = .SearchCol(.GetColFromID("bcno"), 1, .MaxRows, sBcno_Full, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iRow < 1 Then
                    .MaxRows += 1
                    iRow = .MaxRows
                End If


                .Row = iRow
                sbViewSelect(dr, .Row)

                .Focus()
            End With

            Return True

        Catch ex As Exception
            Fn.log(msFile & sfn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    ' 조회한 DaraRow의 내용을 Spread에 표시 
    Private Sub sbViewSelect(ByVal r_dr As DataRow, ByVal riRow As Integer)
        Dim sFn As String = "Private Sub fnViewSelect(DataRow, Integer, String)"

        Try
            With spdList
                .Row = riRow
                .Col = .GetColFromID("workno_old") : .Text = r_dr.Item("workno_old").ToString.Trim
                .Col = .GetColFromID("bcno") : .Text = r_dr.Item("bcno").ToString.Trim
                .Col = .GetColFromID("regno") : .Text = r_dr.Item("regno").ToString.Trim
                .Col = .GetColFromID("orddt") : .Text = r_dr.Item("orddt").ToString.Trim
                .Col = .GetColFromID("patnm") : .Text = r_dr.Item("patnm").ToString.Trim
                .Col = .GetColFromID("sexage") : .Text = r_dr.Item("sexage").ToString.Trim
                .Col = .GetColFromID("deptward") : .Text = r_dr.Item("deptward").ToString.Trim
                .Col = .GetColFromID("doctornm") : .Text = r_dr.Item("doctornm").ToString.Trim
                .Col = .GetColFromID("doctorrmk") : .Text = r_dr.Item("doctorrmk").ToString.Trim
                .Col = .GetColFromID("tnmd") : .Text = r_dr.Item("tnmd").ToString.Trim

                .Col = .GetColFromID("tkdt") : .Text = r_dr.Item("tkdt").ToString.Trim
                .Col = .GetColFromID("tknm") : .Text = r_dr.Item("tknm").ToString.Trim

                .Col = .GetColFromID("statgbn")
                If r_dr.Item("statgbn").ToString.Trim = "1" Then
                    .ForeColor = System.Drawing.Color.Red : .FontBold = True
                    .Text = "Y"
                    .set_RowHeight(.Row, 12.27)
                Else
                    .Text = ""
                End If

                .Col = .GetColFromID("tnmd")
                Select Case r_dr.Item("colorgbn").ToString.Trim
                    Case "1"  '''혈액은행
                        .BackColor = Me.lblBcColor1.BackColor
                        .ForeColor = Me.lblBcColor1.ForeColor
                    Case "2"  ''' 외부 
                        .BackColor = Me.lblBcColor2.BackColor
                        .ForeColor = Me.lblBcColor2.ForeColor
                    Case "3"  ''' 기타 
                        .BackColor = Me.lblBcColor3.BackColor
                        .ForeColor = Me.lblBcColor3.ForeColor
                    Case Else
                        .BackColor = Me.lblBcColor0.BackColor
                        .ForeColor = Me.lblBcColor0.ForeColor
                End Select

            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    ' 개별접수
    Private Sub fnReg(ByVal rsBcNo As String, Optional ByVal rsBcNo_1 As String = "")
        Dim sFn As String = "Private Sub fnReg(String, String)"
        Dim objJubSu As New LISAPP.APP_J.TAKE2
        Dim sWorkNos As String = ""

        Dim sPreWorkNo As String = ""
        Dim bUsePreWkNo As Boolean = False
        Dim alBcNo As New ArrayList

        Try
            sPreWorkNo = fnGet_Workno_old(rsBcNo)
            ' 과거 작업번호가 있는경우
            If sPreWorkNo <> "" Then
                If MsgBox("검체번호[ " + Fn.BCNO_View(rsBcNo, True) & " ]의 이전 작업번호[ " & Fn.WKNO_View(sPreWorkNo) & " ]가 있습니다. " + vbCrLf + vbCrLf + _
                          "이전 작업번호를 사용하시겠습니까? ", MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.Yes Then
                    bUsePreWKNO = True
                Else
                    bUsePreWKNO = False
                End If
            End If

            With objJubSu
                .Init()
                ' 이전 작업번호 사용시 처리 
                If bUsePreWkNo = True Then .UseWknoOld = sPreWorkNo

                If .ExecuteDo(rsBcNo, sWorkNos) = False Then
                    Throw (New Exception(sWorkNos))
                Else
                    With spdList
                        Dim iFindRow As Integer = spdList.SearchCol(spdList.GetColFromID("bcno"), 0, spdList.MaxRows, Fn.BCNO_View(rsBcNo, True), 0)

                        .Row = iFindRow
                        .Col = .GetColFromID("spcflg") : .Text = "1"
                        If sWorkNos <> "" Then
                            .Col = .GetColFromID("workno") : .Text = sWorkNos
                        Else
                            .Col = .GetColFromID("workno") : .Text = "-"
                        End If

                        ' 접수완료시 BackColor변경
                        .Row = iFindRow : .Col = -1
                        .BackColor = Drawing.Color.FromArgb(213, 255, 170)
                        .Col = 0

                        .Action = FPSpreadADO.ActionConstants.ActionGotoCell
                    End With

                    '-- 바코드 출력 루틴
                    sbPrint_BarCode(rsBcNo)
                    '-- 바코드 출력 루틴 끝

                End If
            End With

        Catch ex As Exception
            spdList.MaxRows -= 1

            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    ' 파트별 검체접수 (2차접수) 
    Private Sub fnReg()
        Dim sFn As String = "Private Sub fnReg(ByVal asUserId As String)"
        Dim objTake2 As New LISAPP.APP_J.TAKE2
        Dim bJobFlag As Boolean = True
        Dim sWorkNos As String = ""

        Try
            With spdList
                If .MaxRows > 0 Then
                    For intCnt = .MaxRows To 1 Step -1
                        .Row = intCnt
                        .Col = .GetColFromID("workno") : Dim sWorkNo As String = .Text

                        If sWorkNo = "" Then  ''' 정은 수정 

                            .Col = .GetColFromID("workno_old") : Dim sWorkNo_old As String = .Text.Trim.Replace("-", "")
                            .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.ToString.Replace("-", "")

                            Dim bUseWorkNo_old As Boolean = False

                            ' 과거 작업번호가 있는경우
                            If sWorkNo_old <> "" Then
                                If MsgBox("검체번호[ " + Fn.BCNO_View(sBcNo, True) + " ]의 이전 작업번호[ " + sWorkNo_old + " ]가 있습니다. " + vbCrLf + vbCrLf + _
                                          "이전 작업번호를 사용하시겠습니까? ", MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.Yes Then
                                    bUseWorkNo_old = True
                                Else
                                    bUseWorkNo_old = False
                                End If
                            End If


                            With objTake2
                                .Init()

                                ' 이전 작업번호 사용시 처리 
                                If bUseWorkNo_old = True Then .UseWknoOld = "Y"

                                If .ExecuteDo(sBcNo, sWorkNos) = False Then
                                    'Throw (New Exception(sWorkNos))
                                    bJobFlag = False
                                Else

                                    '-- 바코드 출력 루틴
                                    sbPrint_BarCode(sBcNo)
                                    '-- 바코드 출력 루틴 끝

                                    With spdList
                                        .Row = intCnt
                                        .Col = .GetColFromID("spcflg") : .Text = "1"
                                        If sWorkNos <> "" Then
                                            .Col = .GetColFromID("workno") : .Text = sWorkNos
                                        Else
                                            .Col = .GetColFromID("workno")
                                            If .Text = "" Then
                                                .Text = "-"
                                            End If
                                        End If

                                        ' 접수완료시 BackColor변경
                                        .Row = intCnt : .Col = -1
                                        .BackColor = Drawing.Color.FromArgb(213, 255, 170)
                                    End With
                                    'bJobFlag = True ' 접수처리 구분
                                End If
                            End With
                        End If
                    Next
                End If
            End With

            If bJobFlag = True Then
                MsgBox("정상적으로 접수 되었습니다.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 선택한 항목 리스트에서 삭제
    Private Sub fnDeleteRow()
        Dim sFn As String = "Private Sub fnDeleteRow()"

        Try

            With spdList
                If .IsBlockSelected = True Or .SelectionCount > 0 Then
                    If .SelectionCount = 1 Then
                        Dim strBCNO As String
                        Dim strName As String

                        ' 단일 삭제
                        With spdList
                            .Row = .SelBlockRow
                            .Col = .GetColFromID("검체번호") : strBCNO = .Text
                            .Col = .GetColFromID("성명") : strName = .Text
                        End With

                        If strBCNO <> "" Then
                            If MsgBox("[검체번호: " & strBCNO & ", 성명: " & strName & "] 항목을" & vbCrLf & vbCrLf _
                                    & "리스트에서 삭제 하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Me.Text) = MsgBoxResult.Yes Then
                                With spdList
                                    .DeleteRows(.SelBlockRow, 1) : .MaxRows -= 1
                                End With
                                sbForm_Clear(1)
                            End If
                        End If

                    ElseIf .SelectionCount > 0 Then
                        ' 멀티 삭제
                        'Dim objCol As Object
                        'Dim objCol2 As Object
                        'Dim objRow As Object
                        'Dim objRow2 As Object

                        With spdList

                            If .SelBlockRow > 0 Then
                                If MsgBox("[" & .SelBlockRow.ToString & "번 ~" & .SelBlockRow2.ToString & "번] 항목을" & vbCrLf & vbCrLf _
                                        & "리스트에서 삭제 하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Me.Text) = MsgBoxResult.Yes Then
                                    With spdList
                                        .DeleteRows(.SelBlockRow, .SelBlockRow2 - .SelBlockRow + 1) : .MaxRows -= .SelBlockRow2 - .SelBlockRow + 1
                                    End With
                                    sbForm_Clear(1)
                                End If
                            End If

                        End With

                    End If
                End If

            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

#End Region

#Region " Control Event 처리 "

    Private Sub btnToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Dim CommFn As New COMMON.CommFN.Fn
        Fn.SearchToggle(lblSearch, btnToggle, enumToggle.BcnoToRegno, txtSearch)
        txtSearch.Text = ""
        txtSearch.Focus()
    End Sub

    ' ClickEvent와 LeaveCell의 Event의 분리를 위해서
    Private Sub spdList_MouseDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseDownEvent) Handles spdList.MouseDownEvent
        spdList.Tag = "1"
    End Sub

    Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick
        Dim sFn As String = "Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick"

        Try
            fnDeleteRow()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub spdList_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdList.RightClick
        Dim sFn As String = "Private Sub spdList_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdList.RightClick"

        Try
            fnDeleteRow()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub spdList_TextTipFetch(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles spdList.TextTipFetch
        Fn.SpreadToolTipView(spdList, Me.CreateGraphics, e, spdList.GetColFromID("처방일시"), True)
    End Sub

    Private Sub dtpCollDt_ValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub


#End Region

    '엑셀연동
    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        With spdList
            .ReDraw = False

            .MaxRows += 4
            .InsertRows(1, 3)

            .Col = 8
            .Row = 1
            .Text = "파트 접수 리스트"
            .FontBold = True
            .FontSize = 15
            .ForeColor = System.Drawing.Color.Red

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 3 : .Row2 = 3
            .Clip = sColHeaders

            .InsertRows(4, 1)

            If spdList.ExportToExcel("파트 접수 리스트" + Now.ToShortDateString() + ".xls", "list", "") Then
                Process.Start("파트 접수 리스트" + Now.ToShortDateString() + ".xls")
            End If

            .DeleteRows(1, 4)
            .MaxRows -= 4

            .ReDraw = True

        End With
    End Sub

    Private Sub FGJ04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sFn As String = ""

        Try
            sbDisplay_part() ''' part slip으로 변경 
            sbDisplay_Color_bccls()

            sbDisplay_Init()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sbDisplay_Init()
        Dim sFn As String = "sbDisplay_Init"

        Try
            sbSpreadColHidden(True)
            sbForm_Clear()

            Me.txtSearch.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbSpread_Init()
        Dim sFn As String = "sbSpread_Init"

        Try
            With spdList
                .Col = .GetColFromID("")
                .ColHidden = True

            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbForm_Clear(Optional ByVal riPhase As Integer = 0)
        Dim sFn As String = "sbForm_Init"

        Try
            If riPhase = 0 Then Me.spdList.MaxRows = 0

            Me.txtSearch.Text = ""

            Me.lblCollDt.Text = ""
            Me.lblCollNm.Text = ""
            Me.lblTkDt.Text = ""
            Me.lblTkNm.Text = ""
            Me.lblSpcNm.Text = ""
            Me.lblRemark.Text = ""

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGJ04_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.DoubleClick
        If USER_INFO.USRLVL <> "S" Then Exit Sub

#If DEBUG Then
        Static blnChk As Boolean = False

        '-- 컬럼내용모두 보기/감추기
        sbSpreadColHidden(blnChk)
        blnChk = Not blnChk
#End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim sFn As String = "btnQuery_Click"

        Try
            sbForm_Clear()
            Dim dt As DataTable = fnGet_Take2_PatInfo(Me.dtpTkDt0.Text.Replace("-", ""), Me.dtpTkDt1.Text.Replace("-", ""), Ctrl.Get_Code(cboPart))

            sbForm_Clear(1)

            If dt.Rows.Count < 1 Then Return

            Dim sBcNo As String = ""

            With spdList
                For ix As Integer = 0 To dt.Rows.Count - 1

                    If sBcNo <> dt.Rows(ix).Item("bcno").ToString().Replace("-", "") Then
                        .MaxRows += 1
                        sbViewSelect(dt.Rows(ix), .MaxRows)

                    End If
                    sBcNo = dt.Rows(ix).Item("bcno").ToString().Replace("-", "")
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Dim sFn As String = "txtSearch_Click"

        Try
            Me.txtSearch.SelectAll()
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FG_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        COMMON.CommXML.setOneElementXML(msXMLDir, msPART, "PART", Me.cboPart.SelectedIndex.ToString)
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub cboPart_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPart.SelectedIndexChanged

        Me.spdList.MaxRows = 0

    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim sFn As String = "Handles txtSearch.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtSearch.Text.Trim() = "" Then Return

        Try
            Dim sBcNo As String = ""
            Dim sRegNo As String = ""

            Me.txtSearch.Text = Me.txtSearch.Text.Replace("-", "").Trim()

            If Me.lblSearch.Text = "검체번호" Then
                If Me.txtSearch.Text.Length.Equals(11) Then

                    Dim objCommDBFN As New LISAPP.APP_DB.DbFn
                    Me.txtSearch.Text = objCommDBFN.GetBCPrtToView(Me.txtSearch.Text)

                ElseIf Me.txtSearch.Text.Length.Equals(14) Then

                ElseIf Me.txtSearch.Text.Length.Equals(15) Then

                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "잘못된 검체번호 입니다.")
                    Me.txtSearch.Focus()
                    Return
                End If

                sBcNo = Me.txtSearch.Text
            Else
                If IsNumeric(Me.txtSearch.Text.Substring(0, 1)) Then
                    Me.txtSearch.Text = Me.txtSearch.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                Else
                    Me.txtSearch.Text = Me.txtSearch.Text.Substring(0, 1).ToUpper + Me.txtSearch.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If

                sRegNo = Me.txtSearch.Text
            End If


            Dim dt As DataTable = fnGet_tk_PatInfo(sRegNo, sBcNo, Ctrl.Get_Code(cboPart))

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "환자조회"
            objHelp.MaxRows = 15
            objHelp.OnRowReturnYN = True

            objHelp.AddField("'' CHK", "", 3, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("bcno", "검체번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("regno", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("patnm", "성명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sexage", "성별/나이", 12, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("orddt", "처방일시", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("deptward", "진료과", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("doctornm", "의뢰의사", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmds", "검사내역", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtSearch)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtSearch.Height + 80, dt)

            If alList.Count > 0 Then
                For ix As Integer = 0 To alList.Count - 1
                    If fnSelectList(alList.Item(ix).ToString.Split("|"c)(0).Replace("-", ""), alList.Count) Then
                        fnReg(sBcNo, alList.Item(ix).ToString.Split("|"c)(0).Replace("-", ""))
                    End If
                Next

                'Me.txtSearch.Text = ""
                Me.txtSearch.SelectAll()
                Me.txtSearch.Focus()
            Else
                If Me.lblSearch.Text = "검체번호" Then
                    dt = LISAPP.APP_J.TkFn.fnGet_Take2Yn(sBcNo, Ctrl.Get_Code(cboPart))

                    If dt.Rows(0).Item("rstflg").ToString() <> "" Then
                        If dt.Rows(0).Item("rstflg").ToString() > "0" Then
                            MsgBox("이미 결과보고 한 검체번호 입니다.", MsgBoxStyle.Information, Me.Text)
                        Else
                            MsgBox("이미 2차 접수된 검체번호 입니다.", MsgBoxStyle.Information, Me.Text)
                        End If
                    Else
                        MsgBox("해당하는 검체번호가 없습니다.", MsgBoxStyle.Information, Me.Text)
                    End If
                Else
                    MsgBox("해당하는 환자가 없습니다.", MsgBoxStyle.Information, Me.Text)
                End If

                'Me.txtSearch.Text = ""
                Me.txtSearch.SelectAll()
                Me.txtSearch.Focus()
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnSelBCPRT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelBCPRT.Click
        Dim sFn As String = "Private Sub btnSelBCPRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelBCPRT.Click"
        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC("FGJ04", Me.chkBarInit.Checked)

        Try
            objFrm.ShowDialog()
            lblBarPrinter.Text = objFrm.mPrinterName

            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub
End Class