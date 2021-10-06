Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class TOTRST03
    Inherits System.Windows.Forms.UserControl

    Private moForm As Windows.Forms.Form
    Private mbDoner As Boolean

    Public AppendMode As Boolean = False
    Public BcNo As String = ""
    Public OrdSlip As String = ""
    Public LabSlip As String = ""

    Public Event ChangedBcNo(ByVal spcinfo As SpecimenInfo)
    Public Event ShowGraphReport(ByVal rsBcNo As String)
    Public Event ChangeSelectedRow(ByVal rsTCd As String, ByVal rsTnm As String)
    Public Event ChangeDblClick(ByVal rsTCd As String, ByVal rsTnm As String)

    Private mbFastTestDateTime As Boolean = False
    Private mbUseDebug As Boolean = False
    Private mbUseLab As Boolean = False
    Private mbUseDblCheck As Boolean = False
    Private mbViewReportOnly As Boolean = False
    Private mbSkipRedraw As Boolean = False
    Private mbViewMark As Boolean = False

    Private mbPrintSpdVisible As Boolean = False

    Private m_Spd As ArrayList

    Private miLen_Cd0 As Integer = 16
    Private miLen_Cd1 As Integer = 6
    Private miLen_Cd2 As Integer = 8
    Private miLen_Tot1 As Integer = 30
    Private miLen_Tot2 As Integer = 35

    Private miProcessing As Integer = 0

    Private m_color_rst As Drawing.Color = Drawing.Color.LightCyan
    Private m_color_ref As Drawing.Color = Drawing.Color.LightYellow

    Private m_al_spcinfo As ArrayList

    Private msBcNo As String = ""
    Public msRegNo As String = ""
    Public msStartDt As String = ""
    Public msEndDt As String = ""

    Public mbMicro As Boolean = False

    Private msIPAddress As String = ""
    Private msHostName As String = ""
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Public WithEvents spdRst As AxFPSpreadADO.AxfpSpread

    Private mcSEP As Char = Convert.ToChar(1)
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtCmt As System.Windows.Forms.TextBox
    Friend WithEvents spdPrint As AxFPSpreadADO.AxfpSpread

    Private mprtrst As PrintResult
    Friend WithEvents cmuLink As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuTestInfo As System.Windows.Forms.ToolStripMenuItem
    Public rtbStRst As New AxAckRichTextBox.AxAckRichTextBox

    Public WriteOnly Property Form() As Windows.Forms.Form
        Set(ByVal value As Windows.Forms.Form)
            moForm = value
        End Set
    End Property

    'Public Property DonerYn() As Boolean
    '    Set(ByVal value As Boolean)
    '        mbDoner = value

    '        If value Then
    '            Me.SplitContainer1.Panel2.Height = 26
    '        Else
    '            Me.SplitContainer1.Panel2.Height = 75
    '        End If
    '    End Set
    '    Get
    '        Return mbDoner
    '    End Get
    'End Property

    Public Property FastTestDateTime() As Boolean
        Get
            Return mbFastTestDateTime
        End Get
        Set(ByVal Value As Boolean)
            mbFastTestDateTime = Value
        End Set
    End Property

    Public WriteOnly Property PrintSpdVisible() As Boolean
        Set(ByVal Value As Boolean)
            mbPrintSpdVisible = Value
            spdPrint.Visible = Value
        End Set
    End Property


    Public ReadOnly Property Len_Cd0() As Integer
        Get
            Return miLen_Cd0
        End Get
    End Property

    Public ReadOnly Property Len_Cd1() As Integer
        Get
            Return miLen_Cd1
        End Get
    End Property

    Public ReadOnly Property Len_Cd2() As Integer
        Get
            Return miLen_Cd2
        End Get
    End Property

    Public ReadOnly Property Len_Tot1() As Integer
        Get
            Return miLen_Tot1
        End Get
    End Property

    Public ReadOnly Property Len_Tot2() As Integer
        Get
            Return miLen_Tot2
        End Get
    End Property

    Public ReadOnly Property RowCount() As Integer
        Get
            Return Me.spdRst.MaxRows
        End Get
    End Property

    Public Property UseDebug() As Boolean
        Get
            Return mbUseDebug
        End Get
        Set(ByVal Value As Boolean)
            mbUseDebug = Value

            sbDisplayInit()
        End Set
    End Property

    Public Property UseLab() As Boolean
        Get
            Return mbUseLab
        End Get
        Set(ByVal Value As Boolean)
            mbUseLab = Value

            sbDisplayInit()
        End Set
    End Property

    Public Property UseDblCheck() As Boolean
        Get
            Return mbUseDblCheck
        End Get
        Set(ByVal Value As Boolean)
            mbUseDblCheck = Value
        End Set
    End Property

    Public Property ViewReportOnly() As Boolean
        Get
            Return mbViewReportOnly
        End Get
        Set(ByVal Value As Boolean)
            mbViewReportOnly = Value
        End Set
    End Property

    'Public WriteOnly Property chkMicro() As Boolean
    '    Set(ByVal Value As Boolean)
    '        spdRst.ReDraw = False
    '        spdRst.Col = spdRst.GetColFromID("regdt")
    '        If Value Then
    '            spdRst.ColHidden = False
    '        Else
    '            spdRst.ColHidden = True
    '        End If

    '        spdRst.ReDraw = True
    '    End Set
    'End Property

    Public Property ViewMark() As Boolean
        Get
            Return mbViewMark
        End Get
        Set(ByVal Value As Boolean)
            mbViewMark = Value
        End Set
    End Property

    Public Sub Clear()
        Dim sFn As String = "Sub Clear()"

        Try
            Me.spdRst.MaxRows = 0

            Me.spdPrint.MaxRows = 0

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Function Check_Result() As ArrayList
        Dim sFn As String = "Function Check_Result()"

        Try
            Dim al_return As New ArrayList

            With Me.spdRst
                For i As Integer = 1 To .MaxRows
                    Dim sReturn As String = fnCheck_Result(i)

                    If sReturn.Length > 0 Then
                        al_return.Add(sReturn)
                    End If
                Next
            End With

            Return al_return

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)
            Return Nothing

        End Try
    End Function

    Public Sub Display_Result()
        Dim sFn As String = "Sub Display_Result()"

        Try
            If BcNo Is Nothing Then BcNo = ""
            If OrdSlip Is Nothing Then OrdSlip = ""
            If LabSlip Is Nothing Then LabSlip = ""

            If BcNo = "" Then Return

            Me.ParentForm.Cursor = Windows.Forms.Cursors.WaitCursor

            Dim dt As DataTable, dt_m As New DataTable, dt_c As New DataTable

            dt = LISAPP.APP_V.CommFn.fnGet_Rst_Total(BcNo, dt_m, dt_c)

            If dt.Rows.Count < 1 Then Return

            If AppendMode = False Then
                Me.spdRst.MaxRows = 0

                If m_al_spcinfo Is Nothing Then
                    m_al_spcinfo = New ArrayList
                End If

                m_al_spcinfo.Clear()
            End If

            Dim sFilter As String = ""

            If OrdSlip.Length > 0 Then
                If sFilter.Length > 0 Then
                    sFilter += " AND "
                End If

                sFilter += "tordslip = '" + OrdSlip + "'"
            End If

            If LabSlip.Length > 0 Then
                If sFilter.Length > 0 Then
                    sFilter += " AND "
                End If

                sFilter += "labslip = '" + LabSlip + "'"
            End If

            Dim a_dr() As DataRow = dt.Select(sFilter)

            sbDisplay_Result(a_dr, dt_m, dt_c)

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        Finally
            Me.ParentForm.Cursor = Windows.Forms.Cursors.Default

            AppendMode = False
            BcNo = ""
            OrdSlip = ""
            LabSlip = ""

        End Try
    End Sub

    Public Sub Display_Result(ByVal rbAppend As Boolean)
        Dim sFn As String = "Sub Display_Result(ByVal rbAppend As Boolean)"

        Try
            AppendMode = True

            Display_Result()

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal r_al_OrdSlip As ArrayList)
        Dim sFn As String = "Sub Display_Result(ByVal r_al_bcno As ArrayList)"

        Try
            txtCmt.Text = ""

            'ViewReportOnly = ViewReportOnly

            'Dim sOrdSlip As String = OrdSlip
            Dim sLabSlip As String = LabSlip

            For i As Integer = 1 To r_al_bcno.Count
                'AppenMode 설정
                If i = 1 Then
                    AppendMode = False
                Else
                    AppendMode = True
                End If

                'mbSkipRedraw 설정
                If i = r_al_bcno.Count Then
                    mbSkipRedraw = False
                Else
                    mbSkipRedraw = True
                End If

                BcNo = r_al_bcno(i - 1).ToString()
                OrdSlip = r_al_OrdSlip(i - 1).ToString()
                LabSlip = sLabSlip

                Display_Result()
            Next

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub Display_Result(ByVal r_al_bcno As ArrayList)
        Dim sFn As String = "Sub Display_Result(ByVal r_al_bcno As ArrayList)"

        Try
            Dim sOrdSlip As String = OrdSlip
            Dim sLabSlip As String = LabSlip

            For i As Integer = 1 To r_al_bcno.Count
                'AppenMode 설정
                If i = 1 Then
                    AppendMode = False
                Else
                    AppendMode = True
                End If

                'mbSkipRedraw 설정
                If i = r_al_bcno.Count Then
                    mbSkipRedraw = False
                Else
                    mbSkipRedraw = True
                End If

                BcNo = r_al_bcno(i - 1).ToString()
                OrdSlip = sOrdSlip
                LabSlip = sLabSlip

                Display_Result()
            Next

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal rsOrdSlip As String)
        Dim sFn As String = "Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal rsOrdSlip As ArrayList)"

        Try
            OrdSlip = rsOrdSlip
            Display_Result(r_al_bcno)

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    'Public Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal rsOrdSlip As String, ByVal rsLabSlip As String)
    '    Dim sFn As String = "Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal rsOrdSlip As String, ByVal rsLabSlip As String)"

    '    Try
    '        OrdSlip = rsOrdSlip
    '        LabSlip = rsLabSlip

    '        Display_Result(r_al_bcno)

    '    Catch ex As Exception
    '        sbLog_Exception(ex.Message + " @" + sFn)

    '    End Try
    'End Sub

    Public Sub Display_Result(ByVal rsBcNo As String, ByVal rbAppend As Boolean)
        Dim sFn As String = "Sub Display_Result(ByVal rsBcNo As String, ByVal rbAppend As Boolean)"

        Try
            AppendMode = rbAppend
            BcNo = rsBcNo

            Display_Result()

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub Display_Result(ByVal rsBcNo As String, ByVal rsOrdSlip As String, ByVal rbAppend As Boolean)
        Dim sFn As String = "Sub Display_Result(ByVal rsBcNo As String, ByVal rsOrdSlip As String, ByVal rbAppend As Boolean)"

        Try
            AppendMode = True
            BcNo = rsBcNo
            OrdSlip = rsOrdSlip

            Display_Result()

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub Display_Result(ByVal rsBcNo As String, ByVal rsOrdSlip As String, ByVal rsLabSlip As String, ByVal rbAppend As Boolean)
        Dim sFn As String = "Sub Display_Result(ByVal rsBcNo As String, ByVal rsOrdSlip As String, ByVal rsLabSlip As String, ByVal rbAppend As Boolean)"

        Try
            AppendMode = True
            BcNo = rsBcNo
            OrdSlip = rsOrdSlip
            LabSlip = rsLabSlip

            Display_Result()

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub Display_SpecialTest()
        With Me.spdRst
            For i As Integer = 1 To .MaxRows
                sbDisplay_StRst(i)
            Next
        End With
    End Sub

    Public Function Find_Checked_Result() As ArrayList
        Dim sFn As String = "Function Find_Check_Result()"

        Try
            Dim al_return As New ArrayList

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            With spd
                al_return.Clear()
                For i As Integer = 1 To .MaxRows
                    Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)
                    Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)

                    Dim sReturn As String = ""

                    If sChk = "1" Then
                        'sReturn : TNm1/TNm2/Result
                        sReturn = fnFind_Checked_Result(i)

                        If sReturn.Length > 0 Then
                            'If al_return.Count = 0 Then
                            '    al_return.Add(sBcNo)
                            'End If
                            al_return.Add(sReturn)
                        End If
                    End If
                Next
            End With

            Return al_return

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

            Return Nothing
        End Try
    End Function

    Public Sub ShowDialog()
        mprtrst.ShowDialog()
    End Sub
    Public Sub Print_Result(ByVal rbPreview As Boolean, ByVal riPrintMode As Integer)
        Dim sFn As String = "Sub Print_Result(ByVal rbPreview As Boolean)"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            Dim sAppID As String = "결과조회출력"
            msHostName = Net.Dns.GetHostName()
            msIPAddress = Fn.GetIPAddress(msHostName)
            Dim sPrtBcNo As String = fnFind_PrtBcNo()

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim al_cols As ArrayList = fnFind_PrtInfo_Cols()
            Dim al_labels As ArrayList = fnFind_PrtInfo_Labels()

            Dim prtrst As New PrintResult

            prtrst.Landscape = False

            Dim sfilenm As String = Application.ExecutablePath()

            Dim afilenm() As String = sfilenm.Split("\")

            sfilenm = afilenm(afilenm.Length - 1)


            If sfilenm.ToUpper = "ACK@LISO.EXE" Or sfilenm.ToUpper = "ACK@LISo.exe" Then
                prtrst.Title = "◆ 진단검사의학과 결과 보고서 ◆"
            ElseIf sfilenm.ToUpper = "ACK@RISO.EXE" Or sfilenm = "ACK@RISo.exe" Then
                prtrst.Title = "◆ 핵의학과 결과 보고서 ◆"
            End If



            prtrst.Cols = al_cols
            prtrst.Labels = al_labels
            prtrst.Tail = PRG_CONST.Tail_RstReport

            prtrst.Left_Margin_cm = 1.0
            prtrst.Right_Margin_cm = 1.7
            prtrst.Top_Margin_cm = 1.5
            prtrst.Bottom_Margin_cm = 1.5
            prtrst.mPrtPreview = rbPreview

            If rbPreview Then
                prtrst.PrintPreview(Me.spdRst)
            Else
                prtrst.Print(Me.spdRst)
            End If

            With Me.spdRst
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                    .Col = .GetColFromID("srpt")
                    If .CellTag = "S" Then

                        If rbPreview Then
                            sbDisplay_StRst(ix)
                        Else
                            Dim dt As DataTable = LISAPP.APP_V.CommFn.fnGet_Rst_Special(sBcNo, sTestCd)

                            If dt.Rows.Count > 0 Then
                                For intIdx As Integer = 0 To dt.Rows.Count - 1
                                    Dim intStRst As Integer = 0

                                    Me.rtbStRst.set_SelRTF(dt.Rows(intIdx).Item("rstrtf").ToString, True)
                                    Me.rtbStRst.print_Data()

                                Next
                            End If
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Public Sub Print_Result_arr(ByVal rbPreview As Boolean, ByVal riPrintMode As Integer)
        Dim sFn As String = "Sub Print_Result(ByVal rbPreview As Boolean)"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            Dim sAppID As String = "결과조회출력"
            msHostName = Net.Dns.GetHostName()
            msIPAddress = Fn.GetIPAddress(msHostName)
            Dim sPrtBcNo As String = fnFind_PrtBcNo()

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim al_cols As ArrayList = fnFind_PrtInfo_Cols()
            Dim al_labels As ArrayList = fnFind_PrtInfo_Labels()

            Dim prtrst As New PrintResultArr

            prtrst.Landscape = False

            prtrst.Title = "◆ 진단검사의학과 결과 보고서 ◆"

            prtrst.Cols = al_cols
            prtrst.Labels = al_labels
            prtrst.Tail = PRG_CONST.Tail_RstReport

            prtrst.Left_Margin_cm = 1.0
            prtrst.Right_Margin_cm = 1.7
            prtrst.Top_Margin_cm = 1.5
            prtrst.Bottom_Margin_cm = 2.0
            prtrst.mPrtPreview = rbPreview

            If rbPreview Then
                prtrst.PrintPreview(m_Spd)
            Else
                'prtrst.Print(m_Spd)
            End If

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Public Sub CreatePdialog()
        mprtrst = New PrintResult
        mprtrst.CreatePdialog()
    End Sub
    Public Sub Print_Result_Multi(ByVal rbPreview As Boolean, ByVal riPrintMode As Integer)
        Dim sFn As String = "Sub Print_Result(ByVal rbPreview As Boolean)"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            Dim sAppID As String = "결과조회출력"
            msHostName = Net.Dns.GetHostName()
            msIPAddress = Fn.GetIPAddress(msHostName)
            Dim sPrtBcNo As String = fnFind_PrtBcNo()

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim al_cols As ArrayList = fnFind_PrtInfo_Cols()
            Dim al_labels As ArrayList = fnFind_PrtInfo_Labels()

            mprtrst.Landscape = False

            Select Case riPrintMode
                Case 0
                    mprtrst.Title = "검사결과 일일보고서"

                Case 1
                    mprtrst.Title = "검사결과보고서"

                Case 2
                    mprtrst.Title = "검사결과보고서(예비보고)"

            End Select

            mprtrst.Cols = al_cols
            mprtrst.Labels = al_labels
            mprtrst.Tail = PRG_CONST.Tail_RstReport

            mprtrst.Left_Margin_cm = 1.4
            mprtrst.Right_Margin_cm = 1.3
            mprtrst.Top_Margin_cm = 1.5
            mprtrst.Bottom_Margin_cm = 3
            mprtrst.mPrtPreview = rbPreview

            If rbPreview Then
                mprtrst.PrintPreviewMulti(Me.spdRst)
            Else
                mprtrst.Print(Me.spdRst)
            End If

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbDisplayInit()
    End Sub

    'UserControl1은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents lstEx As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TOTRST03))
        Me.pnl = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstEx = New System.Windows.Forms.ListBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cmuLink = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuTestInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.spdRst = New AxFPSpreadADO.AxfpSpread()
        Me.spdPrint = New AxFPSpreadADO.AxfpSpread()
        Me.txtCmt = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pnl.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.cmuLink.SuspendLayout()
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnl
        '
        Me.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl.Controls.Add(Me.Label4)
        Me.pnl.Controls.Add(Me.Label3)
        Me.pnl.Controls.Add(Me.Label2)
        Me.pnl.Controls.Add(Me.Label1)
        Me.pnl.Controls.Add(Me.lstEx)
        Me.pnl.Controls.Add(Me.SplitContainer1)
        Me.pnl.Controls.Add(Me.Button1)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Location = New System.Drawing.Point(0, 0)
        Me.pnl.Name = "pnl"
        Me.pnl.Size = New System.Drawing.Size(717, 548)
        Me.pnl.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(34, 523)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 24)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "△ 검사"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(114, 523)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 24)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "○ 중간보고"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label2.Location = New System.Drawing.Point(194, 523)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 24)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "◆ 최종보고"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(-1, 523)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "범례"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstEx
        '
        Me.lstEx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstEx.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstEx.ItemHeight = 12
        Me.lstEx.Location = New System.Drawing.Point(276, 524)
        Me.lstEx.Name = "lstEx"
        Me.lstEx.Size = New System.Drawing.Size(439, 24)
        Me.lstEx.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.ContextMenuStrip = Me.cmuLink
        Me.SplitContainer1.Panel1.Controls.Add(Me.spdPrint)
        Me.SplitContainer1.Panel1.Controls.Add(Me.spdRst)
        Me.SplitContainer1.Panel1MinSize = 35
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtCmt)
        Me.SplitContainer1.Panel2MinSize = 26
        Me.SplitContainer1.Size = New System.Drawing.Size(715, 524)
        Me.SplitContainer1.SplitterDistance = 445
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 6
        '
        'cmuLink
        '
        Me.cmuLink.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuTestInfo})
        Me.cmuLink.Name = "cmuRstList"
        Me.cmuLink.Size = New System.Drawing.Size(153, 26)
        Me.cmuLink.Text = "상황에 맞는 메뉴"
        '
        'mnuTestInfo
        '
        Me.mnuTestInfo.Name = "mnuTestInfo"
        Me.mnuTestInfo.Size = New System.Drawing.Size(152, 22)
        Me.mnuTestInfo.Text = "검사정보 보기"
        '
        'spdRst
        '
        Me.spdRst.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdRst.ContextMenuStrip = Me.cmuLink
        Me.spdRst.DataSource = Nothing
        Me.spdRst.Location = New System.Drawing.Point(0, 1)
        Me.spdRst.Name = "spdRst"
        Me.spdRst.OcxState = CType(resources.GetObject("spdRst.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRst.Size = New System.Drawing.Size(712, 444)
        Me.spdRst.TabIndex = 4
        '
        'spdPrint
        '
        Me.spdPrint.DataSource = Nothing
        Me.spdPrint.Location = New System.Drawing.Point(8, 9)
        Me.spdPrint.Name = "spdPrint"
        Me.spdPrint.OcxState = CType(resources.GetObject("spdPrint.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdPrint.Size = New System.Drawing.Size(583, 281)
        Me.spdPrint.TabIndex = 5
        Me.spdPrint.Visible = False
        '
        'txtCmt
        '
        Me.txtCmt.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmt.Location = New System.Drawing.Point(-1, 1)
        Me.txtCmt.Multiline = True
        Me.txtCmt.Name = "txtCmt"
        Me.txtCmt.ReadOnly = True
        Me.txtCmt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCmt.Size = New System.Drawing.Size(717, 75)
        Me.txtCmt.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(637, 498)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TOTRST03
        '
        Me.Controls.Add(Me.pnl)
        Me.Name = "TOTRST03"
        Me.Size = New System.Drawing.Size(717, 548)
        Me.pnl.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.cmuLink.ResumeLayout(False)
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Function fnCheck_Result(ByVal riRow As Integer) As String
        Dim sFn As String = "fnCheck_Result"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", riRow)
            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", riRow)

            Dim sJM As String = Ctrl.Get_Code(spd, "hlmark", riRow)
            Dim sPM As String = Ctrl.Get_Code(spd, "panicmark", riRow)
            Dim sDM As String = Ctrl.Get_Code(spd, "deltamark", riRow)

            If sBcNo = "" Then Return ""
            If sTestCd = "" Then Return ""
            If sTNm = "" Then Return ""

            Dim sReturn As String = ""

            With spd
                If sJM.Length + sPM.Length + sDM.Length > 0 Then
                    .SetText(.GetColFromID("chk"), riRow, "1")
                End If

                sReturn = fnFind_TNm(sBcNo, riRow)
            End With

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)
            Return ""
        End Try
    End Function

    Private Function fnDisplay_Cmt(ByVal r_dr As DataRow, ByVal riLastRow As Integer) As Integer
        Dim sFn As String = "fnDisplay_Cmt"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBuf As String = Fn.RemoveRightCrLf(r_dr.Item("cmt").ToString())

            Dim iMultiCnt As Integer = 0

            Dim sBuf_one As String = sBuf

            Dim al_cmt As New ArrayList

            With spd
                If sBuf.IndexOf(vbCrLf) >= 0 Then
                    'Multi-Line
                    sBuf = sBuf.Replace(vbCrLf, mcSEP).Replace(Chr(13), mcSEP).Replace(Chr(10), mcSEP)

                    iMultiCnt = sBuf.Split(mcSEP).Length

                    For i As Integer = 1 To iMultiCnt
                        sBuf_one = sBuf.Split(mcSEP)(i - 1)

                        Dim al_cmt_buf As ArrayList = Fn.SplitFixedLength(FixedVariable.gsMsg_Cmt_Dot + sBuf_one, _
                                                        FixedVariable.giLen_Line - Fn.LengthH(FixedVariable.gsMsg_Cmt_Indent))

                        For j As Integer = 1 To al_cmt_buf.Count
                            al_cmt.Add(al_cmt_buf(j - 1).ToString())
                        Next
                    Next

                    If al_cmt.Count > 1 Then
                        'InsertRow riLastRow + 1 행 앞에
                        .MaxRows += al_cmt.Count
                        .InsertRows(riLastRow + 1, al_cmt.Count)

                        For k As Integer = 1 To al_cmt.Count
                            '1) 아이콘 지움 --> Cell을 StaticText로
                            .Col = .GetColFromID("chk") : .Row = riLastRow + k : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                            '2) Cell 합치기(검사명란 ~ R란)
                            Dim iCols As Integer = .GetColFromID("tnm")
                            Dim iCole As Integer = .GetColFromID("rstflg")

                            .AddCellSpan(iCols, riLastRow + k, iCole - iCols + 1, 1)

                            '3) 소견
                            .SetText(.GetColFromID("tnm"), riLastRow + k, FixedVariable.gsMsg_Cmt_Indent + al_cmt(k - 1).ToString())
                        Next
                    End If
                Else
                    'Single Line
                    Dim al_cmt_buf As ArrayList = Fn.SplitFixedLength(FixedVariable.gsMsg_Cmt_Dot + sBuf_one, _
                                                    FixedVariable.giLen_Line - Fn.LengthH(FixedVariable.gsMsg_Cmt_Indent))

                    For j As Integer = 1 To al_cmt_buf.Count
                        al_cmt.Add(al_cmt_buf(j - 1).ToString())
                    Next

                    If al_cmt.Count > 0 Then
                        'InsertRow riLastRow + 1 행 앞에
                        .MaxRows += al_cmt.Count
                        .InsertRows(riLastRow + 1, al_cmt.Count)

                        For k As Integer = 1 To al_cmt.Count
                            '1) 아이콘 지움 --> Cell을 StaticText로
                            .Col = .GetColFromID("chk") : .Row = riLastRow + k : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                            '2) Cell 합치기(검사명란 ~ R란)
                            Dim iCols As Integer = .GetColFromID("tnm")
                            Dim iCole As Integer = .GetColFromID("rstflg")

                            .AddCellSpan(iCols, riLastRow + k, iCole - iCols + 1, 1)

                            '3) 소견
                            .SetText(.GetColFromID("tnm"), riLastRow + k, FixedVariable.gsMsg_Cmt_Indent + al_cmt(k - 1).ToString())
                        Next
                    End If
                End If
            End With

            Return riLastRow + al_cmt.Count

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_Checked_Result(ByVal riRow As Integer) As String
        Dim sFn As String = "fnFind_Checked_Result"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", riRow)
            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", riRow)

            Dim sJM As String = Ctrl.Get_Code(spd, "hlmark", riRow)
            Dim sPM As String = Ctrl.Get_Code(spd, "panicmark", riRow)
            Dim sDM As String = Ctrl.Get_Code(spd, "deltamark", riRow)

            If sBcNo = "" Then Return ""
            If sTestCd = "" Then Return ""

            Dim sReturn As String = ""
            Dim sTNm1 As String = ""
            Dim sTNm2 As String = ""

            If sTNm = "" Then
                'Multi-line Rst, Micro-Bac
                sTNm1 = fnFind_TNm(sBcNo, riRow, True)
            Else
                sTNm1 = fnFind_TNm(sBcNo, riRow)
            End If

            sTNm2 = fnFind_TNm2(sBcNo, riRow)

            '-- 2007-11-21 YEJ modify
            ''TNm1(6 + 24), TNm2(8 + 27)
            'sReturn = Fn.PadRightH(sTNm1, miLen_Tot1) + Fn.PadRightH(sTNm2, miLen_Tot2) + fnFind_Rst(sBcNo, riRow)
            'bcno(16), TNm1(6 + 24), TNm2(8 + 27)
            sReturn = sBcNo + " " + Fn.PadRightH(sTNm1, miLen_Tot1) + Fn.PadRightH(sTNm2, miLen_Tot2) + fnFind_Rst(sBcNo, riRow)
            '-- 2007-11-21 YEJ modify end

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)
            Return ""
        End Try
    End Function

    Private Function fnFind_Rst(ByVal rsBcNo As String, ByVal riRow As Integer) As String
        Dim sFn As String = "fnFind_Rst"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", riRow)
            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", riRow)
            Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", riRow)

            Dim sRst As String = Ctrl.Get_Code(spd, "viewrst", riRow)
            Dim sRef As String = Ctrl.Get_Code(spd, "reftxt", riRow)

            Dim sJM As String = Ctrl.Get_Code(spd, "hlmark", riRow)
            Dim sPM As String = Ctrl.Get_Code(spd, "panicmark", riRow)
            Dim sDM As String = Ctrl.Get_Code(spd, "deltamark", riRow)

            Dim sReturn As String = ""

            If sTNm = "" Then
                If sRowid.IndexOf(",") >= 0 Then
                    'Micro-Bac
                    If mbViewMark Then
                        sReturn = (sRst + " " + sRef + " " + sJM + sPM + sDM).Trim()
                    Else
                        sReturn = (sRst + " " + sRef).Trim()
                    End If
                Else
                    'Multi-line Rst
                    If mbViewMark Then
                        sReturn = (sRst + " " + sJM + sPM + sDM).Trim()
                    Else
                        sReturn = sRst.Trim()
                    End If
                End If
            Else
                If mbViewMark Then
                    sReturn = (sRst + " " + sJM + sPM + sDM).Trim()
                Else
                    sReturn = sRst.Trim()
                End If
            End If

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_RstDtUsr(ByVal ra_dr() As DataRow, ByRef rsTestDt As String, ByRef rsTestUsr As String, _
                                        ByRef rsFnDt As String, ByRef rsFnUsr As String, ByRef rsRegDt As String, ByRef rsRegUsr As String) As String
        Dim sFn As String = "fnFind_RstDtUsr"

        Try
            Dim dt As DataTable
            Dim a_dr() As DataRow

            Dim sFilter As String = ""
            Dim sSort As String = ""

            dt = Fn.ChangeToDataTable(ra_dr)

            sFilter = "tcdgbn in ('S', 'P') and rstdt <> '' and rstflg <> ''"

            a_dr = dt.Select(sFilter)

            '모두 접수상태인 것은 Return
            If a_dr.Length = 0 Then
                Return ""
            End If

            Dim sLabDrNm As String = a_dr(0).Item("labdrnm").ToString()

            sFilter = "tcdgbn in ('S', 'P')"
            sSort = "rstflg asc"

            a_dr = dt.Select(sFilter, sSort)

            Dim sMinFlag As String = a_dr(0).Item("rstflg").ToString()

            Select Case sMinFlag
                Case "3"
                    '최종
                    a_dr = dt.Select(sFilter + " and rstflg = '3'", "rstdt desc")

                    rsFnDt = a_dr(0).Item("rstdt").ToString()
                    rsFnUsr = a_dr(0).Item("rstusr").ToString()

                    rsRegDt = a_dr(0).Item("regdt").ToString
                    rsRegUsr = a_dr(0).Item("regusr").ToString
                    If mbFastTestDateTime Then
                        '결과시간 중 가장 빠른 시간 --> TestDt
                        a_dr = dt.Select(sFilter, "rstdt asc")

                        rsTestDt = a_dr(0).Item("rstdt").ToString()
                        rsTestUsr = a_dr(0).Item("rstusr").ToString()

                    Else
                        '최종보고시간 중 가장 빠른 시간 --> TestDt
                        rsTestDt = a_dr(a_dr.Length - 1).Item("rstdt").ToString()
                        rsTestUsr = a_dr(a_dr.Length - 1).Item("rstusr").ToString()
                    End If

                    'Return 확인의사 : decode(fixrptusr, '', labdrnm, fixrptusr)
                    a_dr = dt.Select(sFilter + " and rstflg = '3' and labdrnm <> ''", "rstdt desc")

                    If a_dr.Length > 0 Then
                        sLabDrNm = a_dr(0).Item("labdrnm").ToString()
                    End If

                Case Else
                    '예비
                    rsFnDt = ""
                    rsFnUsr = ""

                    If mbFastTestDateTime Then
                        '결과시간 중 가장 빠른 시간 --> TestDt
                        a_dr = dt.Select(sFilter, "rstdt asc")

                        rsTestDt = a_dr(0).Item("rstdt").ToString()
                        rsTestUsr = a_dr(0).Item("rstusr").ToString()
                    Else
                        rsTestDt = ""
                        rsTestUsr = ""
                    End If

                    sLabDrNm = ""

            End Select

            Return sLabDrNm

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_MultiLine_Cnt_Rst(ByVal riRow As Integer, ByVal rsTestCd As String, ByVal rsRowid As String) As Integer
        Dim sFn As String = "fnFind_MultiLine_Cnt_Rst"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim iReturn As Integer = 0

            With spd
                For i As Integer = riRow To .MaxRows
                    Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
                    Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", i)

                    If sTestCd = rsTestCd And rsRowid = sRowid Then
                        iReturn += 1
                    Else
                        Exit For
                    End If
                Next
            End With

            Return iReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_PrtBcNo() As String
        Dim sFn As String = "fnFind_PrtBcNo"

        Dim sReturn As String = ""

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim al_prtbcno As New ArrayList

            Dim sPrtBcNo As String = ""

            For i As Integer = 1 To spd.MaxRows
                Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)

                If sBcNo.Length > 0 Then
                    If al_prtbcno.Contains(sBcNo) = False Then
                        al_prtbcno.Add(sBcNo)
                    End If
                End If
            Next

            For i As Integer = 1 To al_prtbcno.Count
                If sPrtBcNo.Length > 0 Then sPrtBcNo += ", "

                sPrtBcNo += al_prtbcno(i - 1).ToString()
            Next

            If OrdSlip.Length > 0 Then
                sPrtBcNo = "처방슬립 : " + OrdSlip + ", 검체번호 : " + sPrtBcNo
            Else
                sPrtBcNo = "검체번호 : " + sPrtBcNo
            End If

            sPrtBcNo = sReturn

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_PrtInfo_Cols() As ArrayList
        Dim sFn As String = "fnFind_PrtInfo_Cols"

        Dim al_return As New ArrayList

        Try
            Dim prtcfg As PrintCfg

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "tnm"
            prtcfg.PrtText = "      검 사 명    "
            prtcfg.PrtX_Cm = 0
            prtcfg.PrtSize_Cm = 10 '4.5
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "hlmark"
            prtcfg.PrtText = "HL"
            prtcfg.PrtX_Cm = 8.8
            prtcfg.PrtSize_Cm = 1
            al_return.Add(prtcfg)


            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "viewrst"
            prtcfg.PrtText = " 결 과 값 "
            prtcfg.PrtX_Cm = 9   '<<<20180725 결과값 시작위치 변경 수정 
            prtcfg.PrtSize_Cm = 16.0
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "rstunit"
            prtcfg.PrtText = "결과단위"
            prtcfg.PrtX_Cm = 11.0
            prtcfg.PrtSize_Cm = 4.5
            al_return.Add(prtcfg)


            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "reftxt"
            prtcfg.PrtText = "정 상 치"
            prtcfg.PrtX_Cm = 14.0
            prtcfg.PrtSize_Cm = 9.5
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "regdt"
            prtcfg.PrtText = "결과일시"
            prtcfg.PrtX_Cm = 16.7
            prtcfg.PrtSize_Cm = 5
            al_return.Add(prtcfg)

            Return al_return

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_PrtInfo_Labels() As ArrayList
        Dim sFn As String = "fnFind_PrtInfo_Labels"

        Dim al_return As New ArrayList

        Try
            Dim prtcfg As PrintCfg

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtX_Cm = 12.5
            prtcfg.PrtY_Cm = 0.4
            prtcfg.PrtSize_Cm = 8.5
            prtcfg.PrtFont = New Drawing.Font("굴림체", 9)
            prtcfg.PrtText = "출력위치 " + msIPAddress + "(" + msHostName + ")"
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtX_Cm = 12.5
            prtcfg.PrtY_Cm = 0.8
            prtcfg.PrtSize_Cm = 8.5
            prtcfg.PrtFont = New Drawing.Font("굴림체", 9)
            prtcfg.PrtText = "출력일시 " + (New LISAPP.APP_DB.ServerDateTime().GetDateTime).ToString("yyyy-MM-dd HH:mm")
            al_return.Add(prtcfg)

            Return al_return

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_TNm(ByVal rsBcNo As String, ByVal riRow As Integer) As String
        Dim sFn As String = "fnFind_TNm"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", riRow)

            Dim sReturn As String = ""

            If sTNm.StartsWith("".PadRight(4)) Then
                'Space(4) + Battery 내부항목
                sReturn = fnFind_TNm_Battery(rsBcNo, riRow)
            Else
                If sTNm.StartsWith("".PadRight(2) + "… ") Then
                    'Space(2) + '… '
                    sReturn = fnFind_TNm_Parent(rsBcNo, riRow, sTestCd)
                Else
                    'Space(2) + 단독항목
                    sReturn = sTestCd.PadRight(miLen_Cd1) + sTNm.Substring(2).Trim()
                End If
            End If

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_TNm(ByVal rsBcNo As String, ByVal riRow As Integer, ByVal rbOther As Boolean) As String
        Dim sFn As String = "fnFind_TNm"

        Try
            If rbOther = False Then Return fnFind_TNm(rsBcNo, riRow)

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", riRow)

            Dim sReturn As String = ""

            Dim iRows As Integer = 0, iRowe As Integer = 0

            Dim al_collid As New ArrayList
            Dim al_value As New ArrayList

            If sRowid.IndexOf(",") >= 0 Then
                'Micro-Bac
                al_collid.Add("bcno")
                al_value.Add(rsBcNo)

                al_collid.Add("testcd")
                al_value.Add(sTestCd)

                iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)
            Else
                'Multi-line Rst
                al_collid.Add("bcno")
                al_value.Add(rsBcNo)

                al_collid.Add("testcd")
                al_value.Add(sTestCd)

                al_collid.Add("rowid")
                al_value.Add(sRowid)

                iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)
            End If

            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", iRows)

            If sTNm.StartsWith("".PadRight(4)) Then
                'Space(4) + Battery 내부항목
                sReturn = fnFind_TNm_Battery(rsBcNo, iRows)
            Else
                If sTNm.StartsWith("".PadRight(2) + "… ") Then
                    'Space(2) + '… '
                    sReturn = fnFind_TNm_Parent(rsBcNo, iRows, sTestCd)
                Else
                    'Space(2) + 단독항목
                    sReturn = sTestCd.PadRight(miLen_Cd1) + sTNm.Substring(2).Trim()
                End If
            End If

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Function

    Private Function fnFind_TNm_Battery(ByVal rsBcNo As String, ByVal riRow As Integer) As String
        Dim sFn As String = "fnFind_TNm_Battery"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sReturn As String = ""

            For i As Integer = riRow - 1 To 1 Step -1
                Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)
                Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
                Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", i)

                If rsBcNo = sBcNo And sTNm.StartsWith("".PadRight(2)) And sTNm.StartsWith("".PadRight(4)) = False Then
                    sReturn = sTestCd.PadRight(miLen_Cd1) + sTNm.Trim

                    Exit For
                End If
            Next

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

            Return ""
        End Try
    End Function

    Private Function fnFind_TNm_Parent(ByVal rsBcNo As String, ByVal riRow As Integer, ByVal rsTestCd As String) As String
        Dim sFn As String = "fnFind_TNm_Parent"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sReturn As String = ""

            For i As Integer = riRow - 1 To 1 Step -1
                Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)
                Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
                Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", i)

                If rsBcNo = sBcNo And sTNm.StartsWith("".PadRight(2)) And sTNm.StartsWith("".PadRight(2) + "… ") = False _
                        And rsTestCd.StartsWith(sTestCd) Then
                    sReturn = sTestCd.PadRight(miLen_Cd1) + sTNm.Trim

                    Exit For
                End If
            Next

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)
            Return ""
        End Try
    End Function

    Private Function fnFind_TNm2(ByVal rsBcNo As String, ByVal riRow As Integer) As String
        Dim sFn As String = "fnFind_TNm2"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", riRow)
            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", riRow)

            Dim sReturn As String = ""

            Dim iRows As Integer = 0, iRowe As Integer = 0

            Dim al_collid As New ArrayList
            Dim al_value As New ArrayList

            Dim sTestCd1 As String = ""
            Dim sTNm1 As String = ""

            If sTNm = "" Then
                If sRowid.IndexOf(",") >= 0 Then
                    'Micro-Bac
                    al_collid.Add("bcno")
                    al_value.Add(rsBcNo)

                    al_collid.Add("testcd")
                    al_value.Add(sTestCd)

                    iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)
                Else
                    'Multi-line Rst
                    al_collid.Add("bcno")
                    al_value.Add(rsBcNo)

                    al_collid.Add("testcd")
                    al_value.Add(sTestCd)

                    al_collid.Add("rowid")
                    al_value.Add(sRowid)

                    iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)
                End If

                'iRows에 해당하는 검사코드와 검사명이 riRow의 검사코드와 검사명임
                sTestCd = Ctrl.Get_Code(spd, "testcd", iRows)
                sTNm = Ctrl.Get_Code(spd, "tnm", iRows).Trim()

                'iRows의 Battery or Parent 검사명을 조사
                sTNm1 = fnFind_TNm(rsBcNo, iRows).Substring(miLen_Cd1)
            Else
                'riRow의 Battery or Parent 검사명을 조사
                sTNm1 = fnFind_TNm(rsBcNo, riRow).Substring(miLen_Cd1)
            End If

            If sTNm.Trim() = sTNm1.Trim() Then
                sReturn = ""
            Else
                'Child Sub 고려하여 Cd(7) + Space(1)
                sReturn = sTestCd.PadRight(miLen_Cd2) + sTNm.Trim()
            End If

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)
            Return ""
        End Try
    End Function

    Private Sub sbDisplay_AntiRst(ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_AntiRst"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", riRow)
            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", riRow)

            If sBcNo = "" Then Return
            If sTestCd = "" Then Return
            If sRowid = "" Then Return

            With spd
                .ReDraw = False

                If Ctrl.Get_Code_Tag(spd, "expand", riRow) = "" Then Return

                Dim iRows As Integer = 0
                Dim iRowe As Integer = 0

                Dim al_collid As New ArrayList
                Dim al_value As New ArrayList

                al_collid.Add("bcno")
                al_value.Add(sBcNo)

                al_collid.Add("testcd")
                al_value.Add(sTestCd)

                al_collid.Add("rowid")
                al_value.Add(sRowid)

                iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)

                If iRows >= iRowe Then Return

                Dim bPlus As Boolean = False

                If Ctrl.Get_Code_Tag(spd, "expand", riRow) = "1" Then
                    bPlus = True

                    '그림 - 로
                    .Col = .GetColFromID("expand")
                    .Row = riRow
                    .CellTag = "0"
                    .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Minus)
                Else
                    bPlus = False

                    '그림 + 로
                    .Col = .GetColFromID("expand")
                    .Row = riRow
                    .CellTag = "1"
                    .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)
                End If

                For i As Integer = iRows + 1 To iRowe
                    .Row = i

                    If bPlus Then
                        '보이기
                        .RowHidden = False
                    Else
                        '숨기기
                        .RowHidden = True
                    End If
                Next
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplay_Result(ByVal ra_dr() As DataRow, ByVal r_dt_m As DataTable, ByVal r_dt_c As DataTable)
        Dim sFn As String = "sbDisplay_Result"

        Try
            Dim iLastRow As Integer = 0
            Dim iCol As Integer = 0
            Dim sSlipCd As String = ""
            Dim a_dr_c() As DataRow

            If ra_dr.Length = 0 Then Return

            With Me.spdRst
                .ReDraw = False

                iLastRow = .MaxRows

                '0) 검체정보 표시
                sbDisplay_Result_SpcInfo(ra_dr, iLastRow)

                '1) 결과 표시
                iLastRow = .MaxRows
                .MaxRows += ra_dr.Length

                For i As Integer = 1 To ra_dr.Length
                    For j As Integer = 1 To ra_dr(i - 1).Table.Columns.Count
                        If j = 1 Then sbDisplay_Result_Base(ra_dr(i - 1), iLastRow + i, i)

                        Select Case ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower()
                            Case "tnms", "tnmd"
                                '검사명(처방), 검사명(화면)
                                sbDisplay_Result_TNm(ra_dr(i - 1), iLastRow + i, ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                            Case "viewrst"
                                '결과 표시 <-- RstFlag, UseViewReportOnly

                                sbDisplay_Result_ViewResult_new(ra_dr(i - 1), iLastRow + i)

                            Case "hlmark", "panicmark", "deltamark"
                                'Mark 표시 및 색상
                                sbDisplay_Result_Mark(ra_dr(i - 1), iLastRow + i, ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                            Case "rstflg"
                                '결과상태 표시
                                sbDisplay_Result_RstFlag(ra_dr(i - 1), iLastRow + i)

                            Case "srpt"
                                '특수보고서
                                sbDisplay_Result_SpRpt(ra_dr(i - 1), iLastRow + i)

                            Case "rstdt"
                                iCol = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())
                                If iCol > 0 Then
                                    .Col = iCol
                                    .Row = iLastRow + i
                                    .Text = ra_dr(i - 1).Item(j - 1).ToString()
                                    .CellTag = ra_dr(i - 1).Item(j - 1).ToString()
                                End If
                            Case "mwdt"
                                iCol = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())
                                If iCol > 0 Then
                                    .Col = iCol
                                    .Row = iLastRow + i
                                    .Text = ra_dr(i - 1).Item(j - 1).ToString()
                                    .CellTag = ra_dr(i - 1).Item(j - 1).ToString()
                                End If
                                'Case "rstunit"
                                '    iCol = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())
                                '    If iCol > 0 Then
                                '        .Col = iCol
                                '        .Row = iLastRow + i
                                '        .SetText(.GetColFromID("rstunit"), .Row, ra_dr(i - 1).Item(j - 1).ToString() + "".PadLeft(100))
                                '        '.Text = ra_dr(i - 1).Item(j - 1).ToString()
                                '        .CellTag = ra_dr(i - 1).Item(j - 1).ToString()

                                '    End If

                            Case Else
                                iCol = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                                If iCol > 0 Then
                                    .Col = iCol
                                    .Row = iLastRow + i
                                    .CellTag = ra_dr(i - 1).Item(j - 1).ToString()

                                    If ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower() = "reftxt" Then
                                        Dim sRef As String = ra_dr(i - 1).Item(j - 1).ToString()
                                        '20210802 jhs 세부검사참조 생략
                                        If sRef.IndexOf("~") > -1 Then
                                            If ra_dr(i - 1).Item(j - 1).ToString().Trim.Replace(" ", "").ToString.StartsWith("세부") Then
                                                .Text = ""
                                            Else
                                                Dim test As String = ra_dr(i - 1).Item(j - 1).ToString()
                                                .Text = sRef
                                            End If
                                        Else
                                            If ra_dr(i - 1).Item(j - 1).ToString().Trim.Replace(" ", "").ToString.StartsWith("세부") Then
                                                Dim test As String = ra_dr(i - 1).Item(j - 1).ToString()
                                                .Text = ""
                                            Else
                                                Dim test As String = ra_dr(i - 1).Item(j - 1).ToString()
                                                .Text = ra_dr(i - 1).Item(j - 1).ToString()
                                            End If
                                        End If
                                        '-------------------------------------
                                        'ElseIf ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower() = "rstunit" Then

                                        '    iCol = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())
                                        '    If iCol > 0 Then
                                        '        .Col = iCol
                                        '        .Row = iLastRow + i
                                        '        .SetText(.GetColFromID("rstunit"), .Row, ra_dr(i - 1).Item(j - 1).ToString() + "".PadLeft(100))
                                        '        '.Text = ra_dr(i - 1).Item(j - 1).ToString()
                                        '        .CellTag = ra_dr(i - 1).Item(j - 1).ToString()

                                        '    End If


                                    Else
                                        Dim test As String = ra_dr(i - 1).Item(j - 1).ToString()
                                        .Text = ra_dr(i - 1).Item(j - 1).ToString()


                                    End If
                                End If

                        End Select

                    Next

                    '-- 분야별 소견
                    If sSlipCd <> "" And sSlipCd <> ra_dr(i - 1).Item("slipbcno").ToString And r_dt_c.Rows.Count > 0 Then
                        a_dr_c = r_dt_c.Select("bcno = '" + sSlipCd.Substring(2) + "' AND gbn = '1' AND slipcd = '" + sSlipCd.Substring(0, 2) + "'")

                        sbDisplay_Result_Cmt(True, a_dr_c)
                    End If
                    sSlipCd = ra_dr(i - 1).Item("slipbcno").ToString
                Next

                sbDisplay_Result_MultiLine_Tnm(ra_dr, iLastRow + 1)

                '2) 멀티라인 결과 표시
                sbDisplay_Result_MultiLine_Rst(ra_dr, iLastRow + 1)

                '3) 멀티라인 참고치 표시
                sbDisplay_Result_MultiLine_Ref(ra_dr)

                '4) 미생물일 경우만 배양균, 항균제 표시
                'If ra_dr(0).Item("bcno").ToString().Substring(8, 1) = AppCfg.Const_Sect_MicroBio Or (r_dt_m.Rows.Count > 0 And Not IsNumeric(ra_dr(0).Item("bcno").ToString().Substring(0, 1))) Then
                If r_dt_m.Rows.Count > 0 Then
                    sbDisplay_Result_Micro(r_dt_m)
                End If


                ''1-1) 검체정보 표시 마지막
                'sbDisplay_Result_SpcInfo_Tail(.MaxRows)

                '5) 분야별 소견
                If sSlipCd <> "" And r_dt_c.Rows.Count > 0 Then
                    a_dr_c = r_dt_c.Select("bcno = '" + ra_dr(0).Item("bcno").ToString + "' AND gbn = '1' AND slipcd = '" + sSlipCd.Substring(0, 2) + "'")
                    sbDisplay_Result_Cmt(False, a_dr_c)
                End If

                '-- 검체 소견
                If r_dt_c.Rows.Count > 0 Then
                    a_dr_c = r_dt_c.Select("bcno = '" + ra_dr(0).Item("bcno").ToString + "' AND gbn = '0'")
                    sbDisplay_Result_Cmt(True, a_dr_c)
                End If

            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        Finally
            If mbSkipRedraw = False Then
                Me.spdRst.ReDraw = True
                Me.spdRst.Refresh()

                If Not m_al_spcinfo Is Nothing Then
                    If m_al_spcinfo.Count > 0 Then
                        msBcNo = CType(m_al_spcinfo(0), SpecimenInfo).BcNo.Replace("-", "")

                        RaiseEvent ChangedBcNo(CType(m_al_spcinfo(0), SpecimenInfo))
                    End If
                End If
            End If

        End Try
    End Sub

    Private Sub sbDisplay_Result_Base(ByVal r_dr As DataRow, ByVal riRow As Integer, ByVal riRowid As Integer)
        Dim sFn As String = "sbDisplay_Result_Base"

        Try
            With Me.spdRst

                '1) DataRow와 1 : 1 관계의 Rowid 저장
                .SetText(.GetColFromID("rowid"), riRow, riRowid)

                '2) 아이콘 지움 --> Cell을 StaticText로
                Select Case r_dr.Item("tcdgbn").ToString()
                    Case "B"
                        .Col = .GetColFromID("chk") : .Row = riRow : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                    Case "S", "P"
                        '타이틀
                        If r_dr.Item("titleyn").ToString() = "1" Then
                            .Col = .GetColFromID("chk") : .Row = riRow : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        End If

                    Case "C"

                End Select

                '3) 결과란 Cell 합치기 <-- 미생물만
                Dim iCols As Integer = .GetColFromID("viewrst")
                Dim iCole As Integer = .GetColFromID("deltamark")

                '미생물일 경우만 Cell 합치기
                If PRG_CONST.BCCLS_MicorBio.Contains(r_dr.Item("bcno").ToString().Substring(8, 2)) Then
                    .AddCellSpan(iCols, riRow, iCole - iCols + 1, 1)
                End If

                '3) 타이틀, 결과 색상 처리
                Select Case r_dr.Item("tcdgbn").ToString()
                    Case "B"
                        '모두 타이틀 색상 처리
                        .Col = iCols : .Col2 = iCols
                        .Row = riRow : .Row2 = riRow
                        .BlockMode = True : .BackColor = Drawing.Color.WhiteSmoke : .BlockMode = False

                    Case "S", "P"
                        If r_dr.Item("titleyn").ToString() = "1" Then
                            '타이틀 색상 처리
                            .Col = iCols : .Col2 = iCols
                            .Row = riRow : .Row2 = riRow
                            .BlockMode = True : .BackColor = Drawing.Color.WhiteSmoke : .BlockMode = False
                        Else
                            '결과 색상 처리
                            .Col = iCols : .Col2 = iCols
                            .Row = riRow : .Row2 = riRow
                            .BlockMode = True : .BackColor = m_color_rst : .BlockMode = False
                        End If

                    Case "C"
                        '모두 결과 색상 처리
                        .Col = iCols : .Col2 = iCols
                        .Row = riRow : .Row2 = riRow
                        .BlockMode = True : .BackColor = m_color_rst : .BlockMode = False

                End Select

                '4) 참고치 색상 처리
                .Col = .GetColFromID("reftxt")
                .Row = riRow
                .BackColor = m_color_ref
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_Cmt(ByVal r_dt_c As DataTable)
        Dim sFn As String = "sbDisplay_Result_Cmt"

        Try
            If r_dt_c.Rows.Count = 0 Then Return

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo As String = r_dt_c.Rows(0).Item("bcno").ToString()

            Dim iRows As Integer = 0
            Dim iRowe As Integer = 0

            Dim al_collid As New ArrayList
            Dim al_value As New ArrayList

            al_collid.Add("bcno")
            al_value.Add(sBcNo)

            iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)

            al_collid = Nothing
            al_value = Nothing

            Dim bExist As Boolean = False

            '소견의 RstFlag에 따른 처리 --> 보고할 소견이 존재하는지 조사
            For i As Integer = 1 To r_dt_c.Rows.Count
                If Fn.RemoveRightCrLf(r_dt_c.Rows(i - 1).Item("cmt").ToString()).Length > 0 Then
                    bExist = True

                    Exit For
                End If
            Next

            If bExist = False Then Return

            '0) <소견> 타이틀
            With spd
                'InsertRow iRowe + 1 앞에
                .MaxRows += 1
                .InsertRows(iRowe + 1, 1)

                '0-1) 아이콘 지움 --> Cell을 StaticText로
                .Col = .GetColFromID("chk") : .Row = iRowe + 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                Dim iCols As Integer = .GetColFromID("tnm")
                Dim iCole As Integer = .GetColFromID("rstflg")

                '0-2) Cell 합치기(검사명란 ~ R란)
                .AddCellSpan(iCols, iRowe + 1, 8, 1)

                '.AddCellSpan(iCols, iRowe + 1, iCole, 1)

                '0-3) 타이틀
                .SetText(.GetColFromID("tnm"), iRowe + 1, FixedVariable.gsMsg_Cmt)

                iRowe += 1
            End With

            '1) 소견의 RstFlag에 따른 처리
            For i As Integer = 1 To r_dt_c.Rows.Count
                'iRowe --> 해당 BcNo의 Last Row !!
                iRowe = fnDisplay_Cmt(r_dt_c.Rows(i - 1), iRowe)
            Next

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_Cmt(ByVal rbSlipCmtFlg As Boolean, ByVal r_dr_c() As DataRow)
        Dim sFn As String = "sbDisplay_Result_Cmt"

        Try
            If r_dr_c.Length = 0 Then Return

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo As String = r_dr_c(0).Item("bcno").ToString()
            Dim sSlip As String = r_dr_c(0).Item("slipcd").ToString

            Dim iRows As Integer = 0
            Dim iRowe As Integer = 0

            Dim al_collid As New ArrayList
            Dim al_value As New ArrayList

            If rbSlipCmtFlg Then
                al_collid.Add("bcnoslip")
                al_value.Add(sBcNo + sSlip)
            Else
                al_collid.Add("bcno")
                al_value.Add(sBcNo)
            End If

            iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)

            al_collid = Nothing
            al_value = Nothing

            Dim bExist As Boolean = False

            '소견의 RstFlag에 따른 처리 --> 보고할 소견이 존재하는지 조사
            For i As Integer = 1 To r_dr_c.Length
                'iRowe --> 해당 BcNo의 Last Row !!

                If Fn.RemoveRightCrLf(r_dr_c(i - 1).Item("cmt").ToString()).Length > 0 Then
                    bExist = True

                    Exit For
                End If
            Next

            If bExist = False Then Return

            '0) <소견> 타이틀
            With spd
                'InsertRow iRowe + 1 앞에
                .MaxRows += 1
                '.InsertRows(iRowe + 1, 1)

                iRowe = .MaxRows - 1

                '0-1) 아이콘 지움 --> Cell을 StaticText로
                .Col = .GetColFromID("chk") : .Row = iRowe + 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                Dim iCols As Integer = .GetColFromID("tnm")
                Dim iCole As Integer = .GetColFromID("rstflg")

                '0-2) Cell 합치기(검사명란 ~ R란)
                .AddCellSpan(iCols, iRowe + 1, 8, 1)

                '.AddCellSpan(iCols, iRowe + 1, iCole, 1)

                '0-3) 타이틀
                If rbSlipCmtFlg Then
                    .SetText(.GetColFromID("tnm"), iRowe + 1, FixedVariable.gsMsg_Cmt)
                Else
                    .SetText(.GetColFromID("tnm"), iRowe + 1, FixedVariable.gsMsg_Cmt_bcno)
                End If

                iRowe += 1
            End With

            '1) 소견의 RstFlag에 따른 처리
            For i As Integer = 1 To r_dr_c.Length
                'iRowe --> 해당 BcNo의 Last Row !!

                '중간보고, 최종보고 --> 소견 그대로
                iRowe = fnDisplay_Cmt(r_dr_c(i - 1), iRowe)
            Next

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_Mark(ByVal r_dr As DataRow, ByVal riRow As Integer, ByVal rsColNm As String)
        Dim sFn As String = "sbDisplay_Result_Mark"

        Try
            Dim sViewRst As String = Ctrl.Get_Code(Me.spdRst, "viewrst", riRow)

            '결과저장 --> ViewReportOnly가 True : 검사중 메세지, False : 결과 그대로
            If mbViewReportOnly Then
                If FixedVariable.gsMsg_NoRpt = sViewRst Then
                    Return
                End If
            End If

            With Me.spdRst
                .SetText(.GetColFromID(rsColNm), riRow, r_dr.Item(rsColNm))

                Select Case rsColNm.Substring(0, 1).ToUpper
                    Case "P"
                        If r_dr.Item("panicmark").ToString() = "P" Then
                            .Col = .GetColFromID("panicmark")
                            .Row = riRow
                            .BackColor = FixedVariable.g_color_PM_Bg
                            .ForeColor = FixedVariable.g_color_PM_Fg

                            .Col = .GetColFromID("viewrst")
                            .Row = riRow
                            .BackColor = Color.FromArgb(150, 150, 255)
                            .ForeColor = Color.FromArgb(0, 0, 255)
                        End If

                    Case "D"
                        If r_dr.Item("deltamark").ToString() = "D" Then
                            .Col = .GetColFromID("deltamark")
                            .Row = riRow
                            .BackColor = FixedVariable.g_color_DM_Bg
                            .ForeColor = FixedVariable.g_color_DM_Fg
                        End If

                    Case "C"
                        If r_dr.Item("criticalmark").ToString() = "C" Then
                            .Col = .GetColFromID("criticalamark")
                            .Row = riRow
                            .BackColor = FixedVariable.g_color_DM_Bg
                            .ForeColor = FixedVariable.g_color_DM_Fg
                        End If

                    Case "H"
                        If r_dr.Item("hlmark").ToString() = "L" Then
                            .Col = .GetColFromID("hlmark")
                            .Row = riRow
                            .BackColor = FixedVariable.g_color_LM_Bg
                            .ForeColor = FixedVariable.g_color_LM_Fg

                            .Col = .GetColFromID("viewrst")
                            .Row = riRow
                            .ForeColor = FixedVariable.g_color_LM_Fg

                            If r_dr.Item("panicmark").ToString() <> "P" Then
                                .Col = .GetColFromID("viewrst")
                                .Row = riRow
                                .BackColor = Color.FromArgb(221, 240, 255)
                                .ForeColor = FixedVariable.g_color_LM_Fg
                            End If
                        End If

                        If r_dr.Item("hlmark").ToString() = "H" Then
                            .Col = .GetColFromID("hlmark")
                            .Row = riRow
                            .BackColor = FixedVariable.g_color_HM_Bg
                            .ForeColor = FixedVariable.g_color_HM_Fg

                            .Col = .GetColFromID("viewrst")
                            .Row = riRow
                            .ForeColor = FixedVariable.g_color_HM_Fg


                            If r_dr.Item("panicmark").ToString() <> "P" Then
                                .Col = .GetColFromID("viewrst")
                                .Row = riRow
                                .BackColor = Color.FromArgb(255, 230, 231)
                                .ForeColor = FixedVariable.g_color_LM_Fg
                            End If
                        End If

                End Select
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_Micro(ByVal r_dt_m As DataTable)
        Dim sFn As String = "sbDisplay_Result_Micro"

        Try
            'mbMicro = False

            If r_dt_m.Rows.Count = 0 Then Return

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo_b As String = ""
            Dim sTestCd_b As String = ""

            '검사코드별로 배양균 및 항균제 결과 추가
            For i As Integer = 1 To r_dt_m.Rows.Count
                Dim sBcNo As String = r_dt_m.Rows(i - 1).Item("bcno").ToString()
                Dim sTestCd As String = r_dt_m.Rows(i - 1).Item("testcd").ToString()

                Dim a_dr() As DataRow

                Dim iRows As Integer = 0
                Dim iRowe As Integer = 0

                Dim al_collid As New ArrayList
                Dim al_value As New ArrayList

                If sBcNo + "," + sTestCd <> sBcNo_b + "," + sTestCd_b Then
                    a_dr = r_dt_m.Select("bcno = '" + sBcNo + "' and testcd = '" + sTestCd + "'")

                    al_collid.Add("bcno")
                    al_value.Add(sBcNo)

                    al_collid.Add("testcd")
                    al_value.Add(sTestCd)

                    iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)

                    With spd
                        'InsertRow iRowe + 1 행 앞에
                        .MaxRows += a_dr.Length
                        .InsertRows(iRowe + 1, a_dr.Length)

                        '.Row = iRowe + 1
                        '.RowHidden = True

                        '배양균 결과와 항균제 결과에 따른 처리
                        For m As Integer = 1 To a_dr.Length
                            If a_dr(m - 1).Item("anticd").ToString() = "" Then
                                '1) 배양균
                                If a_dr.Length > 1 And m < a_dr.Length Then '20151217 항균제표시 오류 수정  
                                    '다음 배양균과 같은 지(seq까지 포함한)의 여부
                                    If a_dr(m).Item("seq").ToString() = a_dr(m - 1).Item("seq").ToString() _
                                            And a_dr(m).Item("baccd").ToString() = a_dr(m - 1).Item("baccd").ToString() Then
                                        sbDisplay_Result_Micro_Bac(a_dr(m - 1), iRowe + m, True)

                                        '< 2009-06-22 yjlee 부천순천향
                                        '.Row = iRowe + m
                                        '.RowHidden = True
                                        '> 2009-06-22 yjlee 
                                    Else
                                        sbDisplay_Result_Micro_Bac(a_dr(m - 1), iRowe + m, False)
                                    End If

                                    If Not mbMicro Then
                                        mbMicro = True
                                    End If
                                Else
                                    sbDisplay_Result_Micro_Bac(a_dr(m - 1), iRowe + m, False)
                                End If
                            Else
                                '2) 항균제
                                sbDisplay_Result_Micro_Anti(a_dr(m - 1), iRowe + m)
                            End If
                        Next
                    End With
                End If

                sBcNo_b = sBcNo
                sTestCd_b = sTestCd

                al_collid = Nothing
                al_value = Nothing
            Next

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_Micro_Anti(ByVal r_dr As DataRow, ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Result_Micro_Anti"
        Dim sTmpRst As Object = ""

        Try
            With Me.spdRst
                '1) 아이콘 지움 --> Cell을 StaticText로
                .Col = .GetColFromID("chk") : .Row = riRow : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                Dim iCols As Integer = .GetColFromID("viewrst")
                Dim iCole As Integer = .GetColFromID("deltamark")
                'Me.spdRst.GetColFromID("deltamark").ToString
                '2) Cell 합치기(결과란 ~ Delta란)
                .AddCellSpan(iCols, riRow, iCole - iCols + 1, 1)

                '3) 결과(항균제명)
                .SetText(iCols, riRow, "".PadRight(4) + r_dr.Item("antinmd").ToString())

                '3-1) 결과 색상 처리
                .Col = iCols : .Col2 = iCole
                .Row = riRow : .Row2 = riRow
                .BlockMode = True : .BackColor = m_color_rst : .BlockMode = False

                iCols = .GetColFromID("reftxt")
                iCole = .GetColFromID("rstunit")

                '4) Cell 합치기(참고치란 ~ 단위란)
                .AddCellSpan(iCols, riRow, iCole - iCols + 1, 1)

                ''''5) 결과(RIS)
                '''.SetText(iCols, riRow, r_dr.Item("decrst"))

                ''' 2007/10/29 ssh (항생제 결과 수치도 화면에 표시되도록 수정함.)
                '5) 결과(RIS)


                If r_dr.Item("testmtd").ToString() = "D" Then
                    .SetText(iCols, riRow, r_dr.Item("decrst").ToString().Trim)
                Else
                    .SetText(iCols, riRow, r_dr.Item("decrst").ToString().PadRight(14) + r_dr.Item("antirst").ToString().Trim)
                End If

                '5-1) 참고치 색상 처리
                .Col = iCols : .Col2 = iCole
                .Row = riRow : .Row2 = riRow
                .BlockMode = True : .BackColor = m_color_ref : .BlockMode = False

                '6) bcno, testcd, rowid 저장
                .SetText(.GetColFromID("bcno"), riRow, r_dr.Item("bcno").ToString())
                .SetText(.GetColFromID("testcd"), riRow, r_dr.Item("testcd").ToString())
                .SetText(.GetColFromID("rowid"), riRow, r_dr.Item("seq").ToString() + "," + r_dr.Item("baccd").ToString())

                '7) Default RowHidden
                .Row = riRow
                .RowHidden = True
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_Micro_Bac(ByVal r_dr As DataRow, ByVal riRow As Integer, ByVal rbMoreAnti As Boolean)
        Dim sFn As String = "sbDisplay_Result_Micro_Bac"

        Try
            With Me.spdRst
                ''1) 아이콘 지움 --> Cell을 StaticText로
                '.Col = .GetColFromID("chk") : .Row = riRow : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                  
                Dim iCols As Integer = .GetColFromID("viewrst")
                'Dim iCole As Integer = .GetColFromID("deltamark")
                Dim iCole As Integer = .GetColFromID("rstunit")

                '3) Cell 합치기(결과란 ~ Delta란)
                .AddCellSpan(iCols, riRow, iCole - iCols + 1, 1)

                '4) 결과(배양균명)
                '< yjlee 2010-06-15 미생물 선생님 요구사항으로 인하여 수정.
                '.SetText(iCols, riRow, "".PadRight(2) + r_dr.Item("bacnmd").ToString() + "".PadRight(10, " "c) + r_dr.Item("incrst").ToString())
                .SetText(iCols, riRow, "".PadRight(2) + r_dr.Item("bacnmd").ToString() + "".PadRight(3, " "c) + r_dr.Item("incrst").ToString())


                '4-1) 결과 색상 처리
                .Col = iCols : .Col2 = iCole
                .Row = riRow : .Row2 = riRow
                .BlockMode = True : .BackColor = m_color_rst : .BlockMode = False

                iCols = .GetColFromID("reftxt")
                iCole = .GetColFromID("rstunit")

                '5) Cell 합치기(참고치란 ~ 단위란)
                .AddCellSpan(iCols, riRow, iCole - iCols + 1, 1)



                '6-1) 참고치 색상 처리
                .Col = iCols : .Col2 = iCole
                .Row = riRow : .Row2 = riRow
                .BlockMode = True : .BackColor = m_color_ref : .BlockMode = False

                '7) bcno, testcd, rowid 저장
                .SetText(.GetColFromID("bcno"), riRow, r_dr.Item("bcno").ToString())
                .SetText(.GetColFromID("testcd"), riRow, r_dr.Item("testcd").ToString())
                .SetText(.GetColFromID("rowid"), riRow, r_dr.Item("seq").ToString() + "," + r_dr.Item("baccd").ToString())


                ''6) 결과(증식정도)
                '.SetText(8, riRow, r_dr.Item("incrst"))

            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_MultiLine_Ref(ByVal ra_dr() As DataRow)
        Dim sFn As String = "sbDisplay_Result_MultiLine_Ref"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo As String = ra_dr(0).Item("bcno").ToString()

            With spd
                Dim iRows As Integer = 0
                Dim iRowe As Integer = 0

                Dim al_collid As New ArrayList
                Dim al_value As New ArrayList

                al_collid.Add("bcno")
                al_value.Add(sBcNo)

                iRows = Ctrl.FindMatchRow(spd, al_collid, al_value, iRowe)

                For i As Integer = iRowe To iRows Step -1
                    Dim sBuf As String = Fn.RemoveRightCrLf(Ctrl.Get_Code_Tag(spd, "reftxt", i))
                    Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
                    Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", i)

                    Dim iMultiCnt As Integer = 0
                    Dim iMultiCntR As Integer = 0


                    If sBuf.IndexOf(vbCrLf) >= 0 Then
                        sBuf = sBuf.Replace(vbCrLf, mcSEP)

                        iMultiCnt = sBuf.Split(mcSEP).Length

                        If iMultiCnt > 1 Then
                            iMultiCntR = fnFind_MultiLine_Cnt_Rst(i, sTestCd, sRowid)

                            If iMultiCnt > iMultiCntR Then
                                'InsertRow i + iMultiCntR 행 앞에
                                .MaxRows += iMultiCnt - iMultiCntR
                                .InsertRows(i + iMultiCntR, iMultiCnt - iMultiCntR)
                            End If

                            For k As Integer = 1 To iMultiCnt
                                .SetText(.GetColFromID("reftxt"), i + k - 1, sBuf.Split(mcSEP)(k - 1))

                                If k > 1 Then
                                    '1) 아이콘 지움 --> Cell을 StaticText로
                                    .Col = .GetColFromID("chk") : .Row = i + k - 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                    '2) 결과란 Cell 합치기 <-- 미생물만
                                    Dim iCols As Integer = .GetColFromID("viewrst")
                                    Dim iCole As Integer = .GetColFromID("deltamark")

                                    '미생물일 경우만 Cell 합치기
                                    If PRG_CONST.BCCLS_MicorBio.Contains(sBcNo.ToString().Substring(8, 2)) Then
                                        .AddCellSpan(iCols, i + k - 1, iCole - iCols + 1, 1)
                                    End If

                                    '3) 결과 색상 처리
                                    .Col = iCols : .Col2 = iCols
                                    .Row = i + k - 1 : .Row2 = i + k - 1
                                    .BlockMode = True : .BackColor = m_color_rst : .BlockMode = False

                                    '4) 참고치 색상 처리
                                    .Col = .GetColFromID("reftxt")
                                    .Row = i + k - 1
                                    .BackColor = m_color_ref

                                    '5) bcno, testcd, rowid 저장
                                    .SetText(.GetColFromID("bcno"), i + k - 1, sBcNo)
                                    .SetText(.GetColFromID("testcd"), i + k - 1, sTestCd)
                                    .SetText(.GetColFromID("rowid"), i + k - 1, sRowid)
                                End If
                            Next
                        End If
                    Else
                        'Negative<CR><LF> --> Negative로 변형된 경우 : 멀티라인은 아니지만 변경된 참고치 표시함
                        '20210819 jhs 세부검사참조 조건 변경
                        If Ctrl.Get_Code_Tag(spd, "reftxt", i) <> sBuf And sBuf.StartsWith("세부") = False Then
                            'If Ctrl.Get_Code_Tag(spd, "reftxt", i) <> sBuf Then
                            .SetText(.GetColFromID("reftxt"), i, sBuf)
                        End If
                        '-----------------------------
                    End If
                Next
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_MultiLine_Rst(ByVal ra_dr() As DataRow, ByVal riRows As Integer)
        Dim sFn As String = "sbDisplay_Result_MultiLine_Rst"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            With spd
                Dim iRowe As Integer = riRows + ra_dr.Length - 1

                For i As Integer = iRowe To riRows Step -1
                    '검사중 메세지의 경우에는 원결과와 보이는결과가 다르므로 현재 보이는 결과로 처리
                    Dim sBuf As String = Ctrl.Get_Code(spd, "viewrst", i)
                    Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)
                    Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
                    Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", i)

                    '20210719 jhs 결과값이 길면 참고치와 겹쳐져서 멀티라인 구현하기
                    '멀티 라인일시 공백으로 쪼개서 배열로 담음
                    Dim test As String() = sBuf.Trim.Replace(Chr(13), " ").Split(" ")
                    Dim tmpStr As String = ""
                    sBuf = ""

                    For x As Integer = 0 To test.Length
                        If x = test.Length Then '맨마지막 은 그냥 합치기
                            sBuf += "   " + tmpStr
                        Else ' 중간에 생성되는 문자 정리
                            If tmpStr.Length >= 16 Then '합치는 도중 15자가 넘으면 더하기 
                                sBuf += "   " + tmpStr + Chr(13)
                                tmpStr = ""
                                tmpStr += test(x) + " "
                            Else '15이하일때는 문자단위로 계속 합치기 
                                tmpStr += test(x).Trim + " "
                            End If
                        End If
                    Next
                    '-------------------------------------
                    .Col = .GetColFromID("tnm")
                    .Row = i
                    Dim sBufCmt As String = ""
                    Dim iMultiCnt As Integer = 0


                    If sBuf.IndexOf(vbCrLf) >= 0 Or sBuf.IndexOf(Chr(13)) >= 0 Or sBuf.IndexOf(Chr(10)) >= 0 Then
                        If sBuf.IndexOf(Chr(13)) >= 0 Or sBuf.IndexOf(Chr(10)) >= 0 Then
                            sBuf = sBuf.Replace(Chr(13), mcSEP)
                        Else
                            sBuf = sBuf.Replace(vbCrLf, mcSEP)
                        End If


                        iMultiCnt = sBuf.Split(mcSEP).Length

                        If iMultiCnt > 1 Then
                            'InsertRow i + 1 행 앞에
                            .MaxRows += iMultiCnt - 1
                            .InsertRows(i + 1, iMultiCnt - 1)

                            For k As Integer = 1 To iMultiCnt
                                .SetText(.GetColFromID("viewrst"), i + k - 1, sBuf.Split(mcSEP)(k - 1))

                                If k > 1 Then
                                    ''1) 아이콘 지움 --> Cell을 StaticText로
                                    .Col = .GetColFromID("chk") : .Row = i + k - 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                    '2) 결과란 Cell 합치기 <-- 미생물만
                                    Dim iCols As Integer = .GetColFromID("viewrst")
                                    Dim iCole As Integer = .GetColFromID("deltamark")

                                    '미생물일 경우만 Cell 합치기
                                    If PRG_CONST.BCCLS_MicorBio.Contains(ra_dr(i - riRows).Item("bcno").ToString().Substring(8, 2)) Then
                                        .AddCellSpan(iCols, i + k - 1, iCole - iCols + 1, 1)
                                    End If

                                    '3) 결과 색상 처리
                                    .Col = iCols : .Col2 = iCols
                                    .Row = i + k - 1 : .Row2 = i + k - 1
                                    .BlockMode = True : .BackColor = m_color_rst : .BlockMode = False

                                    '4) 참고치 색상 처리
                                    .Col = .GetColFromID("reftxt")
                                    .Row = i + k - 1
                                    .BackColor = m_color_ref

                                    '5) bcno, testcd, rowid 저장
                                    .SetText(.GetColFromID("bcno"), i + k - 1, sBcNo)
                                    .SetText(.GetColFromID("testcd"), i + k - 1, sTestCd)
                                    .SetText(.GetColFromID("rowid"), i + k - 1, sRowid)
                                End If
                            Next
                        End If
                    End If
                Next
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_MultiLine_Tnm(ByVal ra_dr() As DataRow, ByVal riRows As Integer)
        Dim sFn As String = "sbDisplay_Result_MultiLine_Rst"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            With spd
                Dim iRowe As Integer = riRows + ra_dr.Length - 1

                For i As Integer = iRowe To riRows Step -1
                    '검사중 메세지의 경우에는 원결과와 보이는결과가 다르므로 현재 보이는 결과로 처리
                    Dim sBuf As String = Fn.RemoveRightCrLf(Ctrl.Get_Code(spd, "tnm", i))
                    Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)
                    Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
                    Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", i)

                    .Col = .GetColFromID("tnm")
                    .Row = i
                    Dim sBufCmt As String = ""
                    Dim iMultiCnt As Integer = 0

                    If sBuf.IndexOf(vbCrLf) >= 0 Or sBuf.IndexOf(Chr(13)) >= 0 Or sBuf.IndexOf(Chr(10)) >= 0 Then
                        If sBuf.IndexOf(Chr(13)) >= 0 Or sBuf.IndexOf(Chr(10)) >= 0 Then
                            sBuf = sBuf.Replace(Chr(13), mcSEP).Replace(Chr(10), "")
                        Else
                            sBuf = sBuf.Replace(vbCrLf, mcSEP)
                        End If


                        iMultiCnt = sBuf.Split(mcSEP).Length

                        If iMultiCnt > 1 Then
                            'InsertRow i + 1 행 앞에
                            .MaxRows += iMultiCnt - 1
                            .InsertRows(i + 1, iMultiCnt - 1)

                            For k As Integer = 1 To iMultiCnt
                                .SetText(.GetColFromID("tnm"), i + k - 1, sBuf.Split(mcSEP)(k - 1))

                                If k > 1 Then
                                    ''1) 아이콘 지움 --> Cell을 StaticText로
                                    '.Col = .GetColFromID("chk") : .Row = i + k - 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                    '2) 결과란 Cell 합치기 <-- 미생물만
                                    Dim iCols As Integer = .GetColFromID("viewrst")
                                    Dim iCole As Integer = .GetColFromID("deltamark")

                                    '미생물일 경우만 Cell 합치기
                                    If PRG_CONST.BCCLS_MicorBio.Contains(ra_dr(i - riRows).Item("bcno").ToString().Substring(8, 2)) Then
                                        .AddCellSpan(iCols, i + k - 1, iCole - iCols + 1, 1)
                                    End If

                                    '3) 결과 색상 처리
                                    .Col = iCols : .Col2 = iCols
                                    .Row = i + k - 1 : .Row2 = i + k - 1
                                    .BlockMode = True : .BackColor = m_color_rst : .BlockMode = False

                                    '4) 참고치 색상 처리
                                    .Col = .GetColFromID("reftxt")
                                    .Row = i + k - 1
                                    .BackColor = m_color_ref

                                    '5) bcno, testcd, rowid 저장
                                    .SetText(.GetColFromID("bcno"), i + k - 1, sBcNo)
                                    .SetText(.GetColFromID("testcd"), i + k - 1, sTestCd)
                                    .SetText(.GetColFromID("rowid"), i + k - 1, sRowid)
                                End If
                            Next
                        End If
                    End If
                Next
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_RstFlag(ByVal r_dr As DataRow, ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Result_RstFlag"

        Try
            'Child Sub의 경우에는 표시안함
            If r_dr.Item("tcdgbn").ToString() = "C" Then Return

            With Me.spdRst
                Select Case r_dr.Item("rstflg").ToString()
                    Case "3"
                        '최종보고
                        .Col = .GetColFromID("rstflg")
                        .Row = riRow
                        .Text = FixedVariable.gsRstFlagF
                        .ForeColor = FixedVariable.g_color_FN

                    Case "2"
                        '중간보고
                        .Col = .GetColFromID("rstflg")
                        .Row = riRow
                        .Text = FixedVariable.gsRstFlagM

                    Case "1"
                        '결과저장 --> ViewReportOnly가 True : 검사중 메세지, False : 결과 그대로
                        If mbViewReportOnly = False Then
                            .Col = .GetColFromID("rstflg")
                            .Row = riRow
                            .Text = FixedVariable.gsRstFlagR
                        End If

                    Case Else
                        '채혈, 접수인 경우 --> 미접수, 검사중
                        If r_dr.Item("spcflg").ToString() = "4" Then
                            If r_dr.Item("tcdgbn").ToString().Equals("B") = False Then
                                .SetText(.GetColFromID("viewrst"), riRow, FixedVariable.gsMsg_NoRpt)
                            End If
                        Else
                            .SetText(.GetColFromID("viewrst"), riRow, FixedVariable.gsMsg_NoTk)
                        End If

                End Select
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_SpcInfo(ByVal ra_dr() As DataRow, ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Result_SpcInfo"

        Dim si As New SpecimenInfo

        Dim bNewSpcInfo As Boolean = CBool(IIf(riRow = 0, True, False))

        Dim iAddRow As Integer = 7
        Dim iMinusRow As Integer = 4

        If bNewSpcInfo Then
            iAddRow += 4
            iMinusRow = 0
        End If

        Try
            With Me.spdRst
                Dim iCols As Integer = .GetColFromID("tnm")
                Dim iCole As Integer = .GetColFromID("rstflg")

                Dim sSpcInfo As String = ""

                If riRow > 0 Then
                    '중간마다 빈 Row 넣기
                    .MaxRows = riRow + 1

                    '아이콘 지움 --> Cell을 StaticText로
                    .Col = .GetColFromID("chk") : .Row = riRow + 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                    'Cell 합치기
                    .AddCellSpan(iCols, riRow + 1, iCole - iCols + 1, 1)

                    riRow = .MaxRows
                End If

                '1) 기본 Row 설정
                .MaxRows = riRow + iAddRow

                '1-1) 아이콘 지움 --> Cell을 StaticText로
                .Col = .GetColFromID("chk") : .Row = riRow + 1
                .Col2 = .GetColFromID("chk") : .Row2 = riRow + iAddRow
                .BlockMode = True
                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                .BlockMode = False

                '1-2) Cell 합치기
                For i As Integer = 1 To iAddRow
                    .AddCellSpan(iCols, riRow + i, iCole - iCols + 1, 1)
                Next

                Dim sPatInfo() As String = ra_dr(0).Item("patinfo").ToString.Split("|"c)

                '2) 검체정보 표시
                '2-0) 검체정보 저장
                With si

                    .BcNo = ra_dr(0).Item("bcno").ToString().Trim
                    .SpcNm = ra_dr(0).Item("spcnmd").ToString().Trim
                    .RegNo = ra_dr(0).Item("regno").ToString().Trim
                    .DeptNm = ra_dr(0).Item("deptnm").ToString().Trim
                    .DeptCd = ra_dr(0).Item("deptcd").ToString().Trim
                    .OrdDt = ra_dr(0).Item("orddt").ToString().Trim
                    .DoctorNm = ra_dr(0).Item("doctornm").ToString().Trim
                    .PatNm = sPatInfo(0).Trim
                    .WardRoom = ra_dr(0).Item("wardroom").ToString().Trim
                    .CollDt = ra_dr(0).Item("colldt").ToString().Trim
                    .CollUsr = ra_dr(0).Item("collusr").ToString().Trim
                    .SexAge = ra_dr(0).Item("sexage").ToString().Trim

                    If sPatInfo.Length > 1 Then
                        .IdNo = sPatInfo(3).Trim
                    Else
                        .IdNo = ""
                    End If

                    .EntDt = ra_dr(0).Item("entday").ToString().Trim
                    .TkDt = ra_dr(0).Item("tkdt").ToString().Trim
                    .TkUsr = ra_dr(0).Item("tkusr").ToString().Trim
                    .MwUsr = ra_dr(0).Item("mwusr").ToString().Trim
                    .LabDrNm = fnFind_RstDtUsr(ra_dr, .TestDt, .TestUsr, .FnDt, .FnUsr, .RegDt, .RegUsr).Trim
                    .DiagNm = ra_dr(0).Item("diagnm").ToString().Trim
                    .Remark = ra_dr(0).Item("docrmk").ToString().Replace(vbCrLf, "").Replace(Chr(10), "").Replace(Chr(13), "")
                    .Remark2 = ""
                    .Slip = ra_dr(0).Item("tordslip").ToString()
                    .slipName = ra_dr(0).Item("tordslipnm").ToString()
                    .RstCmt = ra_dr(0).Item("cmt").ToString()

                    If sPatInfo.Length > 1 Then
                        .Injong = sPatInfo(8)
                    Else

                        .Injong = ""
                    End If

                    'Select Case sPatInfo(8)
                    '    Case "Y" : .Injong = "황인"
                    '    Case "W" : .Injong = "백인"
                    '    Case "B" : .Injong = "흑인"
                    'End Select
                End With

                m_al_spcinfo.Add(si)

                If bNewSpcInfo Then
                    '2-0) 출력일
                    sSpcInfo = ""
                    sSpcInfo += Fn.PadLeftH("출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP, 70)
                    sSpcInfo += Fn.PadLeftH("출력일 : " + Format(Now, "yyyy-MM-dd"), 30)
                    .SetText(iCols, riRow + 1 - iMinusRow, sSpcInfo)

                    '2-1) 두줄 라인
                    '──────────────────────────────────────────────────
                    sSpcInfo = ""
                    If FixedVariable.giLen_Line Mod Fn.LengthH(FixedVariable.gsCharLine) = 0 Then
                        sSpcInfo += "".PadRight(FixedVariable.giLen_Line2 \ Fn.LengthH(FixedVariable.gsCharLine2), Convert.ToChar(FixedVariable.gsCharLine2))
                    Else
                        sSpcInfo += "".PadRight(FixedVariable.giLen_Line2 \ Fn.LengthH(FixedVariable.gsCharLine2) + 1, Convert.ToChar(FixedVariable.gsCharLine2))
                    End If

                    .SetText(iCols, riRow + 2 - iMinusRow, sSpcInfo & "2")

                    '2-2) 등록번호 : 001111111 성 명 : 홍길동      주민번호 : 999999-1******
                    sSpcInfo = ""
                    If si.Remark.IndexOf("|"c) >= 0 Then
                        sSpcInfo += "등록번호 : " + Fn.PadRightH(si.RegNo + "(거래처: " + si.Remark.Split("|"c)(0) + ")", 30)
                    Else
                        sSpcInfo += "등록번호 : " + Fn.PadRightH(si.RegNo, 30)
                    End If
                    sSpcInfo += "성  명 : " + Fn.PadRightH(si.PatNm, 20)
                    sSpcInfo += "주민번호 : " + Fn.PadRightH(si.IdNo, 20)
                    .SetText(iCols, riRow + 3 - iMinusRow, sSpcInfo)
                End If

                '< 2-3) 두줄 라인 
                sSpcInfo = ""
                If FixedVariable.giLen_Line Mod Fn.LengthH(FixedVariable.gsCharLine) = 0 Then
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line2 \ Fn.LengthH(FixedVariable.gsCharLine2), Convert.ToChar(FixedVariable.gsCharLine2))
                Else
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line2 \ Fn.LengthH(FixedVariable.gsCharLine2) + 1, Convert.ToChar(FixedVariable.gsCharLine2))
                End If

                .SetText(iCols, riRow + 4 - iMinusRow, sSpcInfo & "2")
                '>

                ' 2-4 ) 진료과 : PD    의뢰의사 : 홍길동   병 동 : ICU    검체 : Serum 

                If si.Remark.IndexOf("|"c) >= 0 Then
                    sSpcInfo = ""
                    sSpcInfo += "거래처 : " + Fn.PadRightH(si.DeptCd + "(" + si.Remark.Split("|"c)(1) + ")", 35) + Space(6)
                    sSpcInfo += "의뢰의사 : " + Fn.PadRightH(si.Remark.Split("|"c)(2), 12) + Space(6)
                    sSpcInfo += "검  체 : " + Fn.PadRightH(si.SpcNm, 30)
                Else
                    sSpcInfo = ""
                    sSpcInfo += "진료과 : " + Fn.PadRightH(si.DeptNm, 12)
                    sSpcInfo += "의뢰의사 : " + Fn.PadRightH(si.DoctorNm, 12) + Space(6)
                    sSpcInfo += "병  동 : " + Fn.PadRightH(si.WardRoom, 12) + Space(6)
                    sSpcInfo += "검  체 : " + Fn.PadRightH(si.SpcNm, 10)
                    sSpcInfo += "전문의 : " + Fn.PadRightH(si.LabDrNm, 12)
                End If
                .SetText(iCols, riRow + 5 - iMinusRow, sSpcInfo)


                ' 2-5 ) 처방일 : 2009-01-01  접 수 일 : 2009-10-10  보 고 일 : 2009-10-10 검사자 : 홍길동 보고자 : 보고자(111)
                sSpcInfo = ""
                sSpcInfo += "채취일 : " + Fn.PadRightH(If(si.CollDt <> "", si.CollDt.Substring(0, 10), ""), 12) '< 20140402 과거자료 조회시 채혈일자 안가져와서 오류 수정

                If si.TkDt.Trim() = "" Then
                    sSpcInfo += "접 수 일 : " + " ".PadLeft(18)
                Else
                    sSpcInfo += "접 수 일 : " + Fn.PadRightH(si.TkDt, 18)
                End If

                If si.Remark.IndexOf("|"c) >= 0 Then
                    If si.FnDt.Trim() = "" Then
                        sSpcInfo += "보 고 일 : "
                    Else
                        sSpcInfo += "보 고 일 : " + IIf(si.FnDt.Trim <> "", Fn.PadRightH(si.FnDt, 18), "").ToString()
                    End If
                Else
                    If si.FnDt.Trim() = "" Then
                        sSpcInfo += "보고일 : "
                    Else
                        sSpcInfo += "보고일 : " + IIf(si.FnDt.Trim <> "", Fn.PadRightH(si.FnDt, 18), "").ToString()
                    End If
                End If

                '1안
                'sSpcInfo += Fn.PadRightH(Fn.PadRightH("최  종", 8), 10)

                'sSpcInfo += Space(9) + Fn.PadRightH("결  과", 12)
                '.SetText(iCols, riRow + 6 - iMinusRow, sSpcInfo)

                'sSpcInfo = ""
                'sSpcInfo += Space(77) + "보고자 : " + Fn.PadRightH(Fn.PadRightH(si.TestUsr, 8), 10)

                'sSpcInfo += "확인의 : " + Fn.PadRightH(si.LabDrNm, 12)
                '.SetText(iCols, riRow + 7 - iMinusRow, sSpcInfo)

                '2안
                'sSpcInfo += "최종보고자:" + si.TestUsr
                'sSpcInfo += Space(1) + "결과확인의:" + Fn.PadRightH(si.LabDrNm, 12)
                '.SetText(iCols, riRow + 6 - iMinusRow, sSpcInfo)


                'original
                sSpcInfo += "검사자 : " + Fn.PadRightH(Fn.PadRightH(si.RegUsr, 8), 10)
                sSpcInfo += "보고자 : " + Fn.PadRightH(si.TestUsr, 12)
                'sSpcInfo += "보고자 : " + Fn.PadRightH(si.LabDrNm, 12)
                .SetText(iCols, riRow + 6 - iMinusRow, sSpcInfo)

                '< 2-6) 한줄라인 
                sSpcInfo = ""
                If FixedVariable.giLen_Line Mod Fn.LengthH(FixedVariable.gsCharLine) = 0 Then
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine), Convert.ToChar(FixedVariable.gsCharLine))
                Else
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine) + 1, Convert.ToChar(FixedVariable.gsCharLine))
                End If

                .SetText(iCols, riRow + 7 - iMinusRow, sSpcInfo)
                '>

                '< 2-8)    검 사 명         결 과 값                            단위              참고치  
                sSpcInfo = ""
                ' sSpcInfo += "     검 사 명                    결 과 값                    단위                    참고치         확인일시 " '<<<20180725 결과 출력지 위치변경
                sSpcInfo += "     검 사 명                                          결 과 값    단위               참고치          확인일시 "
                .SetText(iCols, riRow + 8 - iMinusRow, sSpcInfo)


                '< 2-9) 한줄라인 
                sSpcInfo = ""
                If FixedVariable.giLen_Line Mod Fn.LengthH(FixedVariable.gsCharLine) = 0 Then
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine), Convert.ToChar(FixedVariable.gsCharLine))
                Else
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine) + 1, Convert.ToChar(FixedVariable.gsCharLine))
                End If

                .SetText(iCols, riRow + 9 - iMinusRow, sSpcInfo)
                '>

                '2-6)
                '검체번호 20060228-A0-0464-0  검체명 EDTA Whole Blood
                sSpcInfo = ""
                If si.BcNo.Length = 10 Then
                    sSpcInfo += "검체번호 " + si.BcNo + "  "
                Else
                    sSpcInfo += "검체번호 " + Fn.BCNO_View(si.BcNo, True) + "  "
                End If
                sSpcInfo += si.Remark2
                .SetText(iCols, riRow + 10 - iMinusRow, sSpcInfo)

                sSpcInfo = ""
                'sSpcInfo += "검체명 " + Fn.PadRightH(si.SpcNm, 30)
                sSpcInfo += "검체명 " + si.SpcNm
                .SetText(iCols, riRow + 11 - iMinusRow, sSpcInfo)

                Dim iViewRow As Integer = riRow + 10 - iMinusRow

                Dim ii As Integer = 1
                If Not bNewSpcInfo Then
                    ii = 0
                End If
                If mbUseDebug = False Then
                    'Row 숨기기
                    For i As Integer = ii To iAddRow
                        'If riRow + i <> iViewRow Then
                        '    .Row = riRow + i
                        '    .RowHidden = True
                        'End If
                        'If riRow + i <> iViewRow + 1 Then
                        '    .Row = riRow + i + 1
                        '    .RowHidden = True
                        'End If

                        If riRow + i = iViewRow Or riRow + i = iViewRow + 1 Then

                        Else
                            .Row = riRow + i
                            .RowHidden = True
                        End If
                    Next
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        Finally
            si = Nothing

        End Try
    End Sub

    Private Sub sbDisplay_Result_SpcInfo_Tail(ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Result_SpcInfo_Tail"

        Dim si As New SpecimenInfo

        Try
            With Me.spdRst
                Dim iCols As Integer = .GetColFromID("tnm")
                Dim iCole As Integer = .GetColFromID("rstflg")

                Dim sSpcInfo As String = ""

                si = CType(m_al_spcinfo(0), SpecimenInfo)

                Dim arr_Cmt() As String
                arr_Cmt = si.RstCmt.Split(vbCrLf.ToCharArray()(0))

                '1) 기본 Row 설정
                .MaxRows = riRow + 4 + arr_Cmt.Length

                '1-1) 아이콘 지움 --> Cell을 StaticText로
                .Col = .GetColFromID("chk") : .Row = riRow + 1
                .Col2 = .GetColFromID("chk") : .Row2 = riRow + 4 + arr_Cmt.Length
                .BlockMode = True
                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                .BlockMode = False

                '1-2) Cell 합치기
                For i As Integer = 1 To 4 + arr_Cmt.Length
                    .AddCellSpan(iCols, riRow + i, iCole - iCols + 1, 1)
                Next

                '< 1) 시작 라인 
                sSpcInfo = ""
                If FixedVariable.giLen_Line Mod Fn.LengthH(FixedVariable.gsCharLine) = 0 Then
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine), Convert.ToChar(FixedVariable.gsCharLine))
                Else
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine) + 1, Convert.ToChar(FixedVariable.gsCharLine))
                End If
                .SetText(iCols, riRow + 1, sSpcInfo)
                '>  

                '2)
                '접수일자 : 
                sSpcInfo = ""
                If si.TkDt.Length > 0 Then
                    sSpcInfo += "접수일자 " + Fn.PadRightH(si.TkDt.Substring(0, 10), 12)
                    sSpcInfo += "접수시간 " + Fn.PadRightH(si.TkDt.Substring(11), 8)
                Else
                    sSpcInfo += "접수일자 " + Fn.PadRightH(" ", 12)
                    sSpcInfo += "접수시간 " + Fn.PadRightH(" ", 8)
                End If

                sSpcInfo += "의뢰일자 " + Fn.PadRightH(si.OrdDt.Substring(0, 10), 12)
                sSpcInfo += "검사자 " + Fn.PadRightH(si.TestUsr, 16)
                sSpcInfo += "보고의사 " + si.LabDrNm
                .SetText(iCols, riRow + 2, sSpcInfo)

                If arr_Cmt.Length > 1 Then
                    '3)
                    'Comment : 
                    sSpcInfo = ""
                    sSpcInfo += "소견 : "
                    .SetText(iCols, riRow + 3, sSpcInfo)

                    For iCurRow As Integer = 0 To arr_Cmt.Length - 1
                        sSpcInfo = ""

                        sSpcInfo = arr_Cmt(iCurRow).Trim()

                        .SetText(iCols, riRow + 3 + (iCurRow + 1), sSpcInfo)
                    Next
                End If

                If si.Remark.Length > 0 Then
                    '5)
                    'Remark  : 
                    sSpcInfo = ""
                    sSpcInfo += "Remark  : " + si.Remark
                    .SetText(iCols, riRow + 4 + arr_Cmt.Length + 1, sSpcInfo)
                End If

                If mbUseDebug = False Then
                    'Row 숨기기
                    For i As Integer = 1 To 4 + arr_Cmt.Length
                        .Row = riRow + i
                        .RowHidden = True
                    Next
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        Finally
            si = Nothing

        End Try
    End Sub

    Private Sub sbDisplay_Result_SpRpt(ByVal r_dr As DataRow, ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Result_SpRpt"

        Try
            If r_dr.Item("tcdgbn").ToString() = "C" Then Return

            With Me.spdRst
                Dim sBuf As String = r_dr.Item("srpt").ToString()

                'srpt란에 Picture 설정
                For i As Integer = 1 To sBuf.Length
                    'Graph Report 존재
                    If sBuf.Substring(i - 1, 1).Trim.Equals("G") Then
                        .Col = .GetColFromID("srpt")
                        .Row = riRow
                        .CellTag = "G"
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                        .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                        .TypePictPicture = GetImgList.getImgOther("TXT")
                        .TypePictStretch = False
                    End If

                    'Special Report 존재
                    If sBuf.Substring(i - 1, 1).Trim.Equals("S") Then
                        .Col = .GetColFromID("srpt")
                        .Row = riRow
                        .CellTag = "S"
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                        .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                        .TypePictPicture = GetImgList.getImgOther("LEAF")
                        .TypePictStretch = False
                    End If
                Next
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_TNm(ByVal r_dr As DataRow, ByVal riRow As Integer, ByVal rsColNm As String)
        Dim sFn As String = "sbDisplay_Result_TNm"

        Try
            If mbUseLab Then
                If rsColNm = "tnms" Then Return
            Else
                If rsColNm = "tnmd" Then Return
            End If

            With Me.spdRst
                Dim sTNm As String = r_dr.Item(rsColNm).ToString()
                Dim sTCdGbn As String = r_dr.Item("tcdgbn").ToString()
                Dim sRstCmt As String = r_dr.Item("cmt").ToString()
                Dim sTmp As String = r_dr.Item("testcd").ToString().PadRight(9, " "c)

                Select Case sTCdGbn
                    Case "B"
                        'Space(2) + Battery
                        .SetText(.GetColFromID("tnm"), riRow, "".PadRight(1) + sTNm.Trim)
                        sTmp += sTNm.Trim
                    Case "S", "P"
                        If r_dr.Item("tclscd").ToString().Trim = r_dr.Item("testcd").ToString().Trim Then
                            'Space(2) + 단독항목
                            .SetText(.GetColFromID("tnm"), riRow, "".PadRight(1) + sTNm.Trim)
                            sTmp += sTNm.Trim
                        Else
                            'Space(4) + Battery 내부항목
                            .SetText(.GetColFromID("tnm"), riRow, "".PadRight(2) + ". " + sTNm.Trim)
                            sTmp += ". " + sTNm.Trim
                        End If

                    Case "C"
                        If r_dr.Item("tclscd").ToString().Trim = r_dr.Item("testcd").ToString().Substring(0, 5) Then
                            'Space(2) + '… '
                            .SetText(.GetColFromID("tnm"), riRow, "".PadRight(1) + "... " + sTNm.Trim)
                            sTmp += "... " + sTNm.Trim
                        Else
                            '<-- Battery 내부항목
                            .SetText(.GetColFromID("tnm"), riRow, "".PadRight(2) + ".... " + sTNm.Trim)
                            sTmp += ".... " + sTNm.Trim
                        End If
                    Case Else
                        .SetText(.GetColFromID("tnm"), riRow, "".PadRight(1) + sTNm.Trim)
                        sTmp += sTNm.Trim
                End Select

                .Row = riRow

                If Not sRstCmt = "" Then
                    .Col = .GetColFromID("tnm") : .CellNote = sRstCmt
                End If


            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_Result_ViewResult(ByVal r_dr As DataRow, ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Result_ViewResult"

        Try
            With Me.spdRst
                '멀티라인을 포함한 원결과를 CellTag에 저장
                .Col = .GetColFromID("viewrst")
                .Row = riRow
                .CellTag = r_dr.Item("viewrst").ToString().Replace(FixedVariable.gsMsg_NoTk, "")

                Dim sViewRst As String = r_dr.Item("viewrst").ToString

                If IsNumeric(sViewRst) And sViewRst.StartsWith(".") Then sViewRst = "0" + sViewRst

                Select Case r_dr.Item("rstflg").ToString()
                    Case "3", "2"
                        '중간보고, 최종보고 --> 결과 그대로

                        'If r_dr.Item("bcno").ToString.IndexOf(AppCfg.Const_Sect_MicroBio) > -1 Then
                        '    If r_dr.Item("viewrst").ToString.IndexOf(FixedVariable.gsRst_Growth) > -1 Then
                        '        .SetText(.GetColFromID("viewrst"), riRow, "★")
                        '    ElseIf r_dr.Item("viewrst").ToString.IndexOf(FixedVariable.gsRst_Nogrowth) > -1 Then
                        '        .SetText(.GetColFromID("viewrst"), riRow, "☆")
                        '    Else
                        '        .SetText(.GetColFromID("viewrst"), riRow, strViewRst)
                        '    End If
                        'Else
                        '    .SetText(.GetColFromID("viewrst"), riRow, strViewRst)
                        'End If

                        .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + sViewRst)

                    Case "1"
                        '결과저장 --> ViewReportOnly가 True : 검사중 메세지, False : 결과 그대로
                        If mbViewReportOnly Then
                            .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + FixedVariable.gsMsg_NoRpt)
                        Else
                            'If r_dr.Item("bcno").ToString.IndexOf(AppCfg.Const_Sect_MicroBio) > -1 Then
                            '    If r_dr.Item("viewrst").ToString.IndexOf(FixedVariable.gsRst_Growth) > -1 Then
                            '        .SetText(.GetColFromID("viewrst"), riRow, "★")
                            '    ElseIf r_dr.Item("viewrst").ToString.IndexOf(FixedVariable.gsRst_Nogrowth) > -1 Then
                            '        .SetText(.GetColFromID("viewrst"), riRow, "☆")
                            '    Else
                            '        .SetText(.GetColFromID("viewrst"), riRow, strViewRst)
                            '    End If
                            'Else
                            '    .SetText(.GetColFromID("viewrst"), riRow, strViewRst)
                            'End If
                            .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + sViewRst)
                        End If

                    Case Else
                        '채혈, 접수인 경우 --> 미접수, 검사중
                        If r_dr.Item("spcflg").ToString() = "4" Then
                            If r_dr.Item("tcdgbn").ToString().Equals("B") = False Then
                                .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + FixedVariable.gsMsg_NoRpt)
                            End If
                        Else
                            .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + FixedVariable.gsMsg_NoTk)
                        End If

                End Select

                .Row = riRow
                .Col = .GetColFromID("viewrst")
                If IsNumeric(sViewRst) Then
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                Else
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub
    Private Sub sbDisplay_Result_ViewResult_new(ByVal r_dr As DataRow, ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Result_ViewResult"

        Try
            With Me.spdRst
                Dim sViewRst As String = r_dr.Item("viewrst").ToString


                '멀티라인을 포함한 원결과를 CellTag에 저장
                .Col = .GetColFromID("viewrst")
                .Row = riRow
                .CellTag = r_dr.Item("viewrst").ToString().Replace(FixedVariable.gsMsg_NoTk, "")

                If IsNumeric(sViewRst) And sViewRst.StartsWith(".") Then sViewRst = "0" + sViewRst

                Select Case r_dr.Item("rstflg").ToString()
                    Case "3", "2"
                        '중간보고, 최종보고 --> 결과 그대로

                        'If r_dr.Item("bcno").ToString.IndexOf(AppCfg.Const_Sect_MicroBio) > -1 Then
                        '    If r_dr.Item("viewrst").ToString.IndexOf(FixedVariable.gsRst_Growth) > -1 Then
                        '        .SetText(.GetColFromID("viewrst"), riRow, "★")
                        '    ElseIf r_dr.Item("viewrst").ToString.IndexOf(FixedVariable.gsRst_Nogrowth) > -1 Then
                        '        .SetText(.GetColFromID("viewrst"), riRow, "☆")
                        '    Else
                        '        .SetText(.GetColFromID("viewrst"), riRow, strViewRst)
                        '    End If
                        'Else
                        '    .SetText(.GetColFromID("viewrst"), riRow, strViewRst)
                        'End If

                        .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + sViewRst)

                    Case "1"
                        '결과저장 --> ViewReportOnly가 True : 검사중 메세지, False : 결과 그대로
                        If mbViewReportOnly Then
                            .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + FixedVariable.gsMsg_NoRpt)
                        Else
                            'If r_dr.Item("bcno").ToString.IndexOf(AppCfg.Const_Sect_MicroBio) > -1 Then
                            '    If r_dr.Item("viewrst").ToString.IndexOf(FixedVariable.gsRst_Growth) > -1 Then
                            '        .SetText(.GetColFromID("viewrst"), riRow, "★")
                            '    ElseIf r_dr.Item("viewrst").ToString.IndexOf(FixedVariable.gsRst_Nogrowth) > -1 Then
                            '        .SetText(.GetColFromID("viewrst"), riRow, "☆")
                            '    Else
                            '        .SetText(.GetColFromID("viewrst"), riRow, strViewRst)
                            '    End If
                            'Else
                            '    .SetText(.GetColFromID("viewrst"), riRow, strViewRst)
                            'End If
                            .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + sViewRst)
                        End If

                    Case Else
                        '채혈, 접수인 경우 --> 미접수, 검사중
                        If r_dr.Item("spcflg").ToString() = "4" Then
                            If r_dr.Item("tcdgbn").ToString().Equals("B") = False Then
                                .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + FixedVariable.gsMsg_NoRpt)
                            End If
                        Else
                            .SetText(.GetColFromID("viewrst"), riRow, "".PadRight(3) + FixedVariable.gsMsg_NoTk)
                        End If

                End Select

                .Row = riRow
                .Col = .GetColFromID("viewrst")
                If IsNumeric(sViewRst) Then
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                Else
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_StRst(ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_StRst"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", riRow)
            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", riRow)

            If sBcNo = "" Then Return
            If sTestCd = "" Then Return

            With spd
                .Col = .GetColFromID("srpt")
                .Row = riRow

                If Not .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then Return

                If .CellTag = "S" Then
                    'Special Report
                    Dim strst As New STRST01

                    strst.SpecialTestName = sTNm
                    strst.BcNo = sBcNo
                    strst.TestCd = sTestCd

                    strst.Left = CType(Me.ParentForm.Left + (Me.ParentForm.Width - strst.Width) / 2, Integer)
                    strst.Top = Me.ParentForm.Top + Ctrl.menuHeight

                    strst.ShowDialog(Me)
                ElseIf .CellTag = "G" Then
                    'Graph Report
                    RaiseEvent ShowGraphReport(sBcNo)
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplay_TestHistory(ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_TestHistory(integer)"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst 

            Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", riRow)
            Dim sTCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", riRow)

            If sBcNo = "" Then Return
            If sTCd = "" Then Return

            RaiseEvent ChangeSelectedRow(sTCd, sTNm)

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try

    End Sub

    Private Sub sbDisplay_ChartView(ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_ChartView(integer)"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

            Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", riRow)
            Dim sTCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sTNm As String = Ctrl.Get_Code(spd, "tnm", riRow)

            If sBcNo = "" Then Return
            If sTCd = "" Then Return

            RaiseEvent ChangeDblClick(sTCd, sTNm)

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try

    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            With Me.spdRst
                .Font = New Font("굴림체", 9, FontStyle.Regular)

                .SelBackColor = Drawing.Color.FromArgb(213, 215, 255)
                .SelForeColor = SystemColors.InactiveBorder

                .ShadowColor = Drawing.Color.FromArgb(165, 186, 222)
                .ShadowDark = Color.DimGray
                .ShadowText = SystemColors.ControlText

                .GrayAreaBackColor = Drawing.Color.FromArgb(236, 242, 255)

                If mbUseDebug Then
                    .Col = .GetColFromID("bcno") : .ColHidden = False
                    .Col = .GetColFromID("testcd") : .ColHidden = False
                    .Col = .GetColFromID("tordcd") : .ColHidden = False
                    .Col = .GetColFromID("rowid") : .ColHidden = False
                Else
                    .Col = .GetColFromID("bcno") : .ColHidden = True
                    .Col = .GetColFromID("testcd") : .ColHidden = True
                    .Col = .GetColFromID("tordcd") : .ColHidden = True
                    .Col = .GetColFromID("rowid") : .ColHidden = True
                End If

                If mbUseLab Then
                    Dim intCol As Integer
                    intCol = .GetColFromID("chk")
                    .Col = .GetColFromID("chk")
                    .ColHidden = False

                    '.Col = .GetColFromID("deltamark")
                    '.ColHidden = False

                    .Col = .GetColFromID("rstflg")
                    .ColHidden = False

                Else
                    .Col = .GetColFromID("chk")
                    .ColHidden = True

                    '.Col = .GetColFromID("deltamark")
                    '.ColHidden = True
                End If

                .SetActiveCell(1, 1)
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbLog_Exception(ByVal rsMsg As String)
        Me.lstEx.Items.Insert(0, rsMsg)
    End Sub

    Private Sub spdRst_BlockSelected(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BlockSelectedEvent) Handles spdRst.BlockSelected
        If e.blockCol <> e.blockCol2 Then Return
        If e.blockRow <> e.blockRow2 Or e.blockRow > 0 Then Return
        If e.blockCol <> Me.spdRst.GetColFromID("chk") Then Return

        With Me.spdRst
            Dim iRow As Integer = .SearchCol(e.blockCol, 0, Me.spdRst.MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

            If iRow < 1 Then
                '.Col = e.blockCol : .Col2 = e.blockCol
                '.Row = 1 : .Row2 = .MaxRows
                '.BlockMode = True
                '.Text = "1"
                '.BlockMode = False
            Else
                .Col = e.blockCol : .Col2 = e.blockCol
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True
                .Text = ""
                .BlockMode = False
            End If

            If .IsBlockSelected Then .ClearSelection()
        End With
    End Sub
     
    Public Sub sbPrintClear()
        Dim sFn As String = ""

        Try
            m_Spd = New ArrayList

        Catch ex As Exception

        End Try
    End Sub
    Public Sub addSpdread(ByVal rSpd As AxFPSpreadADO.AxfpSpread)

        Dim sFn As String = ""

        m_Spd.add(rSpd)

    End Sub
    '
    Public Sub sbAddPrint(ByVal rSpd As AxFPSpreadADO.AxfpSpread)
        Dim sFn As String = ""

        Dim iMaxRow As Integer = rSpd.MaxRows
        Dim iPrintMaxRow As Integer = spdPrint.MaxRows

        With spdPrint
            .ReDraw = False
            For iRow As Integer = .MaxRows + 1 To (.MaxRows + iMaxRow) - 1
                .MaxRows += 1

                '.Row = .MaxRows

                For iCol As Integer = 1 To .MaxCols
                    .Col = iCol

                    rSpd.Row = iRow - iPrintMaxRow + 1
                    rSpd.Col = iCol

                    Dim sValues As String = rSpd.Text.Trim()

                    .Row = .MaxRows
                    .Col = iCol

                    .Text = sValues
                Next
            Next
            .ReDraw = True
        End With
      
    End Sub

    Private Sub spdRst_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdRst.ClickEvent
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        If miProcessing = 1 Then Return

        Select Case e.col
            Case Me.spdRst.GetColFromID("expand")
                sbDisplay_AntiRst(e.row)

            Case Me.spdRst.GetColFromID("srpt")
                sbDisplay_StRst(e.row)

            Case Me.spdRst.GetColFromID("viewrst")
                sbDisplay_TestHistory(e.row)
            Case Else
                sbDisplay_TestHistory(e.row)

        End Select
    End Sub

    Private Sub spdRst_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdRst.LeaveCell
        If e.newRow < 1 Then Return
        If e.row = e.newRow Then Return
        If msBcNo = "" Then Return
        If m_al_spcinfo Is Nothing Then Return

        Dim sBcNo As String = Ctrl.Get_Code(Me.spdRst, "bcno", e.newRow)

        If sBcNo = "" Then Return

        '<  
        txtCmt.Text = ""

        spdRst.Col = e.newCol
        spdRst.Row = e.newRow


        If Not spdRst.CellNote Is Nothing Then
            txtCmt.Text = spdRst.CellNote
            
        End If
        '>

        'If sBcNo <> msBcNo And m_al_spcinfo.Count > 0 Then
        For i As Integer = 1 To m_al_spcinfo.Count
            If CType(m_al_spcinfo(i - 1), SpecimenInfo).BcNo = sBcNo Then
                msBcNo = sBcNo

                RaiseEvent ChangedBcNo(CType(m_al_spcinfo(i - 1), SpecimenInfo))

                Exit For
            End If
        Next
        'End If
    End Sub

    Private Sub spdRst_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdRst.DblClick
        If mbUseDblCheck = False Then Exit Sub

        Select Case e.col
            Case Me.spdRst.GetColFromID("viewrst")
                sbDisplay_ChartView(e.row)

        End Select
    End Sub

    Private Sub mnuTestInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuTestInfo.Click

        Dim sTestCd As String = ""
        Dim sTestGbn As String = IIf(mbViewReportOnly, "O", "T").ToString

        With Me.spdRst
            .Row = .ActiveRow
            If sTestGbn = "T" Then
                .Col = .GetColFromID("testcd") : sTestCd = .Text
            Else
                .Col = .GetColFromID("tordcd") : sTestCd = .Text
            End If
        End With

        Dim frm As Windows.Forms.Form = New CDHELP.FGCDHELP_TEST(sTestGbn, sTestCd)

        moForm.AddOwnedForm(frm)
        frm.WindowState = FormWindowState.Normal
        frm.Activate()
        frm.Show()

    End Sub

End Class



Public Class PrintResultArr
    Private Const mc_sFile As String = "File : TOTRST01.vb, Class : PrintResult" & vbTab

    '1 point = 1 / 72 inch, 1 inch = 2.5399 cm, 1 Margin(Bounds) point = 1 / 100 inch
    Public Left_Margin_cm As Single = 0
    Public Right_Margin_cm As Single = 0
    Public Top_Margin_cm As Single = 0
    Public Bottom_Margin_cm As Single = 0

    Public UseCustomPaper As Boolean = False
    Public Landscape As Boolean = False

    Public Title As String = ""
    Public Labels As ArrayList = Nothing
    Public Headers As ArrayList = Nothing
    Public Cols As ArrayList = Nothing
    Public Tail As String = ""
    Public PrintDateTime As String = ""

    Public Separator As String = Convert.ToChar(1)

    Public FontSize_Title As Single = 16
    Public FontSize_Between_Title_Header As Single = 10
    Public FontSize_Header As Single = 10
    Public FontSize_Body As Single = 9
    Public FontSize_CharLine As Single = 8.65
    Public FontSize_Tail As Single = 10

    Public PaperSize_Height As Integer = 100
    Public PaperSize_Width As Integer = 100

    Public CharLine As Char = Convert.ToChar(FixedVariable.gsCharLine)
    Public CharLine2 As Char = Convert.ToChar(FixedVariable.gsCharLine2)

    Protected Inch_per_DrawPt As Integer = 72
    Protected DrawPt_per_inch As Single = 1 / 72
    Protected Inch_per_Cm As Single = 2.5399
    Protected Cm_per_inch As Single = 1 / 2.5399
    Protected Inch_per_MarginPt As Integer = 100
    Protected MarginPt_per_inch As Single = 1 / 100
    Protected DrawPt_per_MarginPt As Single = 72 / 100
    Protected MarginPt_per_DrawPt As Single = 100 / 72
    Protected Cm_per_DrawPt As Single = 2.5399 / 72
    Protected DrawPt_per_Cm As Single = 72 / 2.5399
    Protected Cm_per_MarginPt As Single = 2.5399 / 100
    Protected MarginPt_per_Cm As Single = 100 / 2.5399

    Protected p_spd As AxFPSpreadADO.AxfpSpread

    Protected psngX As Single = 0
    Protected psngY As Single = 0
    Protected psngW As Single = 0
    Protected psngH As Single = 0

    Protected psngPrtX As Single = 0
    Protected psngPrtY As Single = 0
    Protected piRow_Body As Integer = 0
    Protected piRow_Start As Integer = 1
    Protected piRow_Body2 As Integer = 0
    Protected piRow_Start2 As Integer = 1

    Protected psSEP As String = " "

    Protected psFontName As String = "굴림체"

    Private mcSEP As Char = Convert.ToChar(1)

    Protected WithEvents p_pd As Drawing.Printing.PrintDocument

    Private m_ppdialog As Windows.Forms.PrintPreviewDialog

    Public mPrtPreview As Boolean  '-- 2007-10-25 YOOEJ ADD

    Dim sss As String = ""

    Public Sub CreatePdialog()
        m_ppdialog = New Windows.Forms.PrintPreviewDialog
    End Sub

    Public Overridable Function Find_Height_Row(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Single
        Dim sFn As String = "Function Find_Height_Row"

        Try
            Dim sngLineHeight As Single = 0

            Dim sLine As String = Ctrl.Get_Code(p_spd, "tnm", riRow)
            Dim sBcNo As String = ""

            Dim iLastSpcInfo As Integer = riRow

            If sLine.StartsWith(CharLine.ToString()) Then
                For i As Integer = riRow + 1 To p_spd.MaxRows
                    sBcNo = Ctrl.Get_Code(p_spd, "bcno", i)

                    If sBcNo.Length > 0 Then
                        Exit For
                    End If

                    iLastSpcInfo = i
                Next
            End If

            sngLineHeight = (New Drawing.Font(psFontName, FontSize_Body)).GetHeight(e.Graphics) * (iLastSpcInfo - riRow + 1)

            Return sngLineHeight

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Function Find_Height_Tail(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
        Dim sFn As String = "Function Find_Height_Tail"

        Try
            Dim sngLineHeight_L As Single = 0
            Dim sngLineHeight_T As Single = 0

            sngLineHeight_L = (New Drawing.Font(psFontName, FontSize_CharLine)).GetHeight(e.Graphics)
            sngLineHeight_T = (New Drawing.Font(psFontName, FontSize_Tail)).GetHeight(e.Graphics)

            Dim iLineCnt As Integer = 0

            If PRG_CONST.Tail_RstReport.IndexOf(vbCrLf) > 0 Then
                Dim sBuf As String = PRG_CONST.Tail_RstReport.Replace(vbCrLf, mcSEP)

                iLineCnt = sBuf.Split(mcSEP).Length
            Else
                iLineCnt = 1
            End If

            Return Convert.ToSingle(sngLineHeight_L + iLineCnt * sngLineHeight_T)

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Function Print(ByVal r_spd As AxFPSpreadADO.AxfpSpread) As Integer
        Dim sFn As String = "Function Print"

        Try
            p_pd = New Drawing.Printing.PrintDocument

            If UseCustomPaper Then
                p_pd.DefaultPageSettings.PaperSize = New Drawing.Printing.PaperSize("Custom01", PaperSize_Width, PaperSize_Height)
            End If

            p_pd.DefaultPageSettings.Landscape = Landscape

            p_spd = r_spd

            piRow_Start = 1

            p_pd.Print()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Sub ShowDialog()
        m_ppdialog.ShowDialog()
    End Sub

    Public Overridable Function PrintPreviewMulti(ByVal r_spd As AxFPSpreadADO.AxfpSpread) As Integer
        Dim sFn As String = "Function PrintPreview"

        Try
            p_pd = New Drawing.Printing.PrintDocument

            If UseCustomPaper Then
                p_pd.DefaultPageSettings.PaperSize = New Drawing.Printing.PaperSize("Custom01", PaperSize_Width, PaperSize_Height)
            End If

            p_pd.DefaultPageSettings.Landscape = Landscape

            'Dim ppdialog As New Windows.Forms.PrintPreviewDialog

            'm_ppdialog = ppdialog

            m_ppdialog.Document = p_pd

            p_spd = r_spd

            piRow_Start = 1

            m_ppdialog.StartPosition = FormStartPosition.CenterParent
            m_ppdialog.Width = Convert.ToInt32(r_spd.Height * 4 / 3)
            m_ppdialog.Height = r_spd.Height

            'ppdialog.ShowDialog()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Function PrintPreview(ByVal r_arr As ArrayList) As Integer
        Dim sFn As String = "Function PrintPreview"
        'ByVal r_spd As AxFPSpreadADO.AxfpSpread

        Dim r_spd As AxFPSpreadADO.AxfpSpread = Nothing
        Dim ppdialog As New Windows.Forms.PrintPreviewDialog

        p_pd = New Drawing.Printing.PrintDocument

        p_pd.DefaultPageSettings.Landscape = Landscape

        Try
            If r_arr.Count > 0 Then
                For iCnt As Integer = 0 To r_arr.Count - 1
                    r_spd = CType(r_arr(iCnt), AxFPSpreadADO.AxfpSpread)

                    If UseCustomPaper Then
                        p_pd.DefaultPageSettings.PaperSize = New Drawing.Printing.PaperSize("Custom01", PaperSize_Width, PaperSize_Height)
                    End If

                    'ppdialog.Document.

                    ppdialog.Document = p_pd

                    p_spd = r_spd

                    piRow_Start = 1 

                    ppdialog.Width = Convert.ToInt32(r_spd.Height * 4 / 3)
                    ppdialog.Height += r_spd.Height
                Next

                ppdialog.StartPosition = FormStartPosition.CenterParent


                ppdialog.ShowDialog()
            End If
        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Sub BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles p_pd.BeginPrint
        piRow_Start = 1
        piRow_Body = 0
    End Sub



    Public Overridable Sub RenderPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles p_pd.PrintPage
        Dim sFn As String = "Sub RenderPage"

        e.Graphics.PageUnit = Drawing.GraphicsUnit.Point

        Try
            '여백 조정
            Dim iAutoMargin As Integer = 0

            If Left_Margin_cm = 0 Then iAutoMargin += 1
            If Right_Margin_cm = 0 Then iAutoMargin += 1
            If Top_Margin_cm = 0 Then iAutoMargin += 1
            If Bottom_Margin_cm = 0 Then iAutoMargin += 1

            If iAutoMargin > 0 Then
                psngX = e.MarginBounds.X * DrawPt_per_MarginPt
                psngY = e.MarginBounds.Y * DrawPt_per_MarginPt
                psngW = e.MarginBounds.Width * DrawPt_per_MarginPt
                psngH = e.MarginBounds.Height * DrawPt_per_MarginPt
            Else
                psngX = Left_Margin_cm * DrawPt_per_Cm
                psngY = Top_Margin_cm * DrawPt_per_Cm
                psngW = e.PageBounds.Width * DrawPt_per_MarginPt - (Left_Margin_cm + Right_Margin_cm) * DrawPt_per_Cm
                psngH = e.PageBounds.Height * DrawPt_per_MarginPt - (Top_Margin_cm + Bottom_Margin_cm) * DrawPt_per_Cm
            End If

            Dim iNewPage As Integer = 0

            psngPrtX = psngX
            psngPrtY = psngY

            With p_spd
                For i As Integer = piRow_Start To .MaxRows
                    If i = piRow_Start Then
                        iNewPage = 0
                    Else
                        ' If psngPrtY + Find_Height_Row(e, i) + Find_Height_Tail(e) > psngY + psngH Then
                        If psngPrtY + Find_Height_Tail(e) > psngY + psngH Then
                            iNewPage = -1
                        Else
                            iNewPage = 1
                        End If
                    End If

                    If iNewPage < 1 Then
                        If iNewPage = -1 Then
                            RenderPage_Tail(e, True)

                            e.HasMorePages = True

                            piRow_Start = i

                            piRow_Body = 0

                            Return
                        End If

                        psngPrtY = RenderPage_Title(e)

                        'psngPrtY = RenderPage_Headers(e)

                        'psngPrtY = RenderPage_Cols(e)

                        'RenderPage_Labels(e)
                    End If

                    psngPrtY = RenderPage_Body(e, i)

                    'If iNewPage < 1 Then
                    '    psngPrtY = RenderPage_Cols(e)
                    'End If
                Next

                RenderPage_Tail(e, False)
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Overridable Function RenderPage_Body(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Single
        Dim sFn As String = "Function RenderPage_Body"

        Try
            Dim font As Drawing.Font = New Drawing.Font(psFontName, FontSize_Body)
            'End If

            Dim sBcNo As String = Ctrl.Get_Code(p_spd, "bcno", riRow)

            Dim sBuf As String = ""
            Dim sBuf_tnm As String = Ctrl.Get_Code(p_spd, "tnm", riRow)
            Dim sBuf_viewrst As String = Ctrl.Get_Code(p_spd, "viewrst", riRow)
            Dim sBuf_hlmark As String = Ctrl.Get_Code(p_spd, "hlmark", riRow)
            Dim sBuf_reftxt As String = Ctrl.Get_Code(p_spd, "reftxt", riRow)
            Dim sBuf_rstunit As String = Ctrl.Get_Code(p_spd, "rstunit", riRow)

            Dim sngX_tnm As Single = 0, sngW_tnm As Single = 0
            Dim sngX_viewrst As Single = 0, sngW_viewrst As Single = 0
            Dim sngX_hlmark As Single = 0, sngW_hlmark As Single = 0
            Dim sngX_panicmark As Single = 0, sngW_panicmark As Single = 0
            Dim sngX_reftxt As Single = 0, sngW_reftxt As Single = 0
            Dim sngX_rstunit As Single = 0, sngW_rstunit As Single = 0

            'If riRow = 8 Then
            '    sBuf_tnm = ""
            'End If

            Dim sngLineHeight As Single
            sngLineHeight = (New Drawing.Font(psFontName, FontSize_Body)).GetHeight(e.Graphics)

            If sBuf_tnm.StartsWith(CharLine.ToString()) Or sBuf_tnm.StartsWith(CharLine2.ToString()) Then
                '라인인 경우는 라인 로직에 따라 처리 후 Return
                Dim iLineLen As Integer = 0
                Dim fontL As Drawing.Font

                If sBuf_tnm.EndsWith("B") Then
                    fontL = New Drawing.Font(psFontName, FontSize_CharLine, FontStyle.Bold)
                Else
                    fontL = New Drawing.Font(psFontName, FontSize_CharLine, FontStyle.Regular)
                End If


                If sBuf_tnm.EndsWith("2") Then
                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine2.ToString) = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine2.ToString)
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine2.ToString) + 1
                    End If

                Else
                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine.ToString) = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString)
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString) + 1
                    End If

                End If

                '< 진하게찍는 경우 길이 하나 줄임.
                If sBuf_tnm.EndsWith("B") Then
                    iLineLen -= 1
                End If

                ''< 두줄 찍히는 경우 길이 하나 줄임. 
                'If sBuf_tnm.EndsWith("2") Then
                '    iLineLen -= 1
                'End If


                If sBuf_tnm.EndsWith("2") Then
                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine2), fontL, Drawing.Brushes.Black, _
                                psngX, psngPrtY)
                Else
                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                psngX, psngPrtY)
                End If


                psngPrtY += sngLineHeight
                Return psngPrtY
            End If

            If Cols Is Nothing Then
                sBuf = sBuf_tnm + " " + sBuf_viewrst + " " + sBuf_hlmark + " " + sBuf_reftxt + " " + sBuf_rstunit
                e.Graphics.DrawString(sBuf, font, Drawing.Brushes.Black, psngX + psngPrtX, psngPrtY)
            Else
                For i As Integer = 1 To Cols.Count
                    Select Case CType(Cols(i - 1), PrintCfg).PrtID
                        Case "tnm"
                            sngX_tnm = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_tnm = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "hlmark"
                            sngX_hlmark = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_hlmark = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "viewrst"
                            sngX_viewrst = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_viewrst = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "reftxt"
                            sngX_reftxt = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_reftxt = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "rstunit"
                            sngX_rstunit = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_rstunit = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                    End Select
                Next

                For i As Integer = 1 To Cols.Count
                    Dim rectF As Drawing.RectangleF
                    Dim sf As New Drawing.StringFormat
                    Dim prtcfg As PrintCfg = CType(Cols(i - 1), PrintCfg)

                    Select Case CType(Cols(i - 1), PrintCfg).PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.Right, PrintCfg.Align.Center
                            rectF = New Drawing.RectangleF(psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY, _
                                                            prtcfg.PrtSize_Cm * DrawPt_per_Cm, font.GetHeight(e.Graphics))

                        Case PrintCfg.Align.PageLeft, PrintCfg.Align.PageRight, PrintCfg.Align.PageCenter
                            rectF = New Drawing.RectangleF(psngX, psngPrtY, _
                                                            psngW, font.GetHeight(e.Graphics) + 1)
                    End Select

                    sf.LineAlignment = StringAlignment.Center

                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.PageLeft
                            sf.Alignment = StringAlignment.Near

                        Case PrintCfg.Align.Right, PrintCfg.Align.PageRight
                            sf.Alignment = StringAlignment.Far

                        Case PrintCfg.Align.Center, PrintCfg.Align.PageCenter
                            sf.Alignment = StringAlignment.Center

                    End Select

                    If sBcNo.Length = 0 Then
                        '타이틀 및 소견
                        Select Case prtcfg.PrtID
                            Case "tnm"
                                If Not sBuf_tnm = "" Then
                                    e.Graphics.DrawString(sBuf_tnm, font, Drawing.Brushes.Black, psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY)
                                    'e.Graphics.DrawString(sBuf_tnm, font, Drawing.Brushes.Black, rectF, sf)
                                End If
                        End Select
                    Else
                        '검사결과
                        Select Case prtcfg.PrtID
                            Case "tnm"
                                e.Graphics.DrawString(sBuf_tnm, font, Drawing.Brushes.Black, rectF, sf)
                                'e.Graphics.DrawString(sBuf_tnm, font, Drawing.Brushes.Black, psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY)
                            Case "hlmark"
                                If sBuf_hlmark.Length > 0 Then
                                    If sBuf_hlmark = "H" Then
                                        sBuf_hlmark = "▲"
                                    ElseIf sBuf_hlmark = "L" Then
                                        sBuf_hlmark = "▼"
                                    End If
                                    e.Graphics.DrawString(sBuf_hlmark, font, Drawing.Brushes.Black, rectF, sf)
                                End If

                            Case "viewrst"
                                If e.Graphics.MeasureString(sBuf_viewrst + " ", font).Width > sngX_reftxt - sngX_viewrst Then
                                    If sBuf_tnm.Length = 0 Then
                                        If sBuf_reftxt.Length = 0 Then
                                            If sBuf_rstunit.Length = 0 Then
                                                e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, rectF, sf)
                                            Else
                                                e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, _
                                                                        sngX_rstunit - e.Graphics.MeasureString(sBuf_viewrst + " ", font).Width, psngPrtY)
                                            End If
                                        Else
                                            e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, _
                                                                    sngX_reftxt - e.Graphics.MeasureString(sBuf_viewrst + " ", font).Width, psngPrtY)
                                        End If
                                    Else
                                        e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, rectF, sf)
                                    End If
                                Else
                                    e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, rectF, sf)
                                End If

                            Case "reftxt"
                                If sBuf_reftxt.Length > 0 Then
                                    e.Graphics.DrawString(sBuf_reftxt, font, Drawing.Brushes.Black, rectF, sf)
                                End If

                            Case "rstunit"
                                If sBuf_rstunit.Length > 0 Then
                                    e.Graphics.DrawString(sBuf_rstunit, font, Drawing.Brushes.Black, rectF, sf)
                                End If

                        End Select
                    End If
                Next
            End If

            psngPrtY += sngLineHeight

            Return psngPrtY

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            'Page내의 Body만의 Row 수
            piRow_Body += 1

        End Try
    End Function


    Public Overridable Function RenderPage_Cols(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
        Dim sFn As String = "Sub RenderPage_Cols"

        Try
            If Cols Is Nothing Then Return psngPrtY

            Dim font_col As Drawing.Font

            For i As Integer = 1 To Cols.Count
                Dim prtcfg As PrintCfg = CType(Cols(i - 1), PrintCfg)
                Dim font As Drawing.Font = prtcfg.PrtFont

                Dim rectF As Drawing.RectangleF
                Dim sf As New Drawing.StringFormat

                Dim fontL As New Drawing.Font(psFontName, FontSize_CharLine)
                Dim iLineLen As Integer = 0

                'Cols Upper Line 표시
                If i = 1 Then
                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine.ToString) = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString)
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString) + 1
                    End If

                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, psngPrtY)

                    psngPrtY += fontL.GetHeight(e.Graphics)
                End If

                If prtcfg.PrtText.Length > 0 Then
                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.Right, PrintCfg.Align.Center
                            rectF = New Drawing.RectangleF(psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY, _
                                                            prtcfg.PrtSize_Cm * DrawPt_per_Cm, prtcfg.PrtFont.GetHeight(e.Graphics))

                        Case PrintCfg.Align.PageLeft, PrintCfg.Align.PageRight, PrintCfg.Align.PageCenter
                            rectF = New Drawing.RectangleF(psngX, psngPrtY, _
                                                            psngW, prtcfg.PrtFont.GetHeight(e.Graphics) + 1)
                    End Select

                    sf.LineAlignment = StringAlignment.Center

                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.PageLeft
                            sf.Alignment = StringAlignment.Near

                        Case PrintCfg.Align.Right, PrintCfg.Align.PageRight
                            sf.Alignment = StringAlignment.Far

                        Case PrintCfg.Align.Center, PrintCfg.Align.PageCenter
                            sf.Alignment = StringAlignment.Center

                    End Select

                    e.Graphics.DrawString(prtcfg.PrtText, font, Drawing.Brushes.Black, rectF, sf)

                    font_col = font
                End If

                'Cols Lower Line 표시
                If i = Cols.Count Then
                    psngPrtY += font_col.GetHeight(e.Graphics)

                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine.ToString) = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString)
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString) + 1
                    End If

                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, psngPrtY)

                    psngPrtY += fontL.GetHeight(e.Graphics)
                End If
            Next

            Return psngPrtY

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Function RenderPage_Headers(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
        Dim sFn As String = "Function RenderPage_Headers"

        Dim iY As Integer = 0

        Try
            'Between Title and Header : 빈 공간 추가
            Dim font_th As New Drawing.Font(psFontName, FontSize_Between_Title_Header)
            Dim sngHeight_th As Single = font_th.GetHeight(e.Graphics)

            e.Graphics.DrawString("", font_th, Drawing.Brushes.White, psngX, psngPrtY)

            psngPrtY += sngHeight_th

            If Headers Is Nothing Then Return psngPrtY

            Dim font_h As Drawing.Font

            For i As Integer = 1 To Headers.Count
                Dim prtcfg As PrintCfg = CType(Headers(i - 1), PrintCfg)
                Dim font As Drawing.Font = prtcfg.PrtFont

                Dim rectF As Drawing.RectangleF
                Dim sf As New Drawing.StringFormat

                Dim fontL As New Drawing.Font(psFontName, FontSize_CharLine)
                Dim iLineLen As Integer = 0

                'Headers Upper Line 표시
                If i = 1 Then
                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod CharLine.ToString.Length = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ CharLine.ToString.Length
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ CharLine.ToString.Length + 1
                    End If

                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, psngPrtY)

                    psngPrtY += fontL.GetHeight(e.Graphics)
                End If

                If prtcfg.PrtText.Length > 0 Then
                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.Right, PrintCfg.Align.Center
                            rectF = New Drawing.RectangleF(psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY, _
                                                            prtcfg.PrtSize_Cm * DrawPt_per_Cm, prtcfg.PrtFont.GetHeight(e.Graphics))

                        Case PrintCfg.Align.PageLeft, PrintCfg.Align.PageRight, PrintCfg.Align.PageCenter
                            rectF = New Drawing.RectangleF(psngX, psngPrtY, _
                                                            psngW, prtcfg.PrtFont.GetHeight(e.Graphics) + 1)
                    End Select

                    sf.LineAlignment = StringAlignment.Center

                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.PageLeft
                            sf.Alignment = StringAlignment.Near

                        Case PrintCfg.Align.Right, PrintCfg.Align.PageRight
                            sf.Alignment = StringAlignment.Far

                        Case PrintCfg.Align.Center, PrintCfg.Align.PageCenter
                            sf.Alignment = StringAlignment.Center

                    End Select

                    e.Graphics.DrawString(prtcfg.PrtText, font, Drawing.Brushes.Black, rectF, sf)

                    font_h = font
                End If

                'Headers Lower Line 표시
                If i = Cols.Count Then
                    psngPrtY += font_h.GetHeight(e.Graphics)

                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod CharLine.ToString.Length = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ CharLine.ToString.Length
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ CharLine.ToString.Length + 1
                    End If

                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, psngPrtY)

                    psngPrtY += fontL.GetHeight(e.Graphics)
                End If
            Next

            Return psngPrtY

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Sub RenderPage_Labels(ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Dim sFn As String = "Sub RenderPage_Labels"

        Try
            If Labels Is Nothing Then Return

            For i As Integer = 3 To Labels.Count
                Dim prtcfg As PrintCfg = CType(Labels(i - 1), PrintCfg)
                Dim font As Drawing.Font = prtcfg.PrtFont

                Dim rectF As Drawing.RectangleF

                Select Case prtcfg.PrtAlign
                    Case PrintCfg.Align.Left, PrintCfg.Align.Right, PrintCfg.Align.Center
                        rectF = New Drawing.RectangleF(psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngY + prtcfg.PrtY_Cm * DrawPt_per_Cm, _
                                                        prtcfg.PrtSize_Cm * DrawPt_per_Cm, prtcfg.PrtFont.GetHeight(e.Graphics))

                    Case PrintCfg.Align.PageLeft, PrintCfg.Align.PageRight, PrintCfg.Align.PageCenter
                        rectF = New Drawing.RectangleF(psngX, psngY + prtcfg.PrtY_Cm * DrawPt_per_Cm, _
                                                        psngW, prtcfg.PrtFont.GetHeight(e.Graphics) + 1)
                End Select

                Dim sf As New Drawing.StringFormat

                sf.LineAlignment = StringAlignment.Center

                Select Case prtcfg.PrtAlign
                    Case PrintCfg.Align.Left, PrintCfg.Align.PageLeft
                        sf.Alignment = StringAlignment.Near

                    Case PrintCfg.Align.Right, PrintCfg.Align.PageRight
                        sf.Alignment = StringAlignment.Far

                    Case PrintCfg.Align.Center, PrintCfg.Align.PageCenter
                        sf.Alignment = StringAlignment.Center

                End Select

                e.Graphics.DrawString(prtcfg.PrtText, font, Drawing.Brushes.Black, rectF, sf)
            Next

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Overridable Sub RenderPage_Tail(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal rbMore As Boolean)
        Dim sFn As String = "Sub RenderPage_Tail"

        Try
            Dim fontL As New Drawing.Font(psFontName, FontSize_CharLine)
            Dim fontT As New Drawing.Font(psFontName, FontSize_Tail)
            Dim sf As New Drawing.StringFormat

            Dim sTail As String = PRG_CONST.Tail_RstReport

            'Tail 바로 앞 Line 표시
            Dim iLineLen As Integer = 0
            Dim iLineCnt As Integer = 0

            If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine.ToString) = 0 Then
                iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString)
            Else
                iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString) + 1
            End If

            If sTail.IndexOf(vbCrLf) > 0 Then
                sTail = sTail.Replace(vbCrLf, mcSEP)

                iLineCnt = sTail.Split(mcSEP).Length
            Else
                iLineCnt = 1
            End If

            e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, Convert.ToSingle(psngY + psngH - iLineCnt * fontT.GetHeight(e.Graphics) - fontL.GetHeight(e.Graphics)))

            'Tail 텍스트 표시
            For i As Integer = 1 To iLineCnt
                If iLineCnt = 1 Then
                    e.Graphics.DrawString(sTail, fontT, Drawing.Brushes.Black, _
                                    psngX, Convert.ToSingle(psngY + psngH - (iLineCnt - i) * fontT.GetHeight(e.Graphics) - fontL.GetHeight(e.Graphics)))
                Else
                    If i = iLineCnt Then
                        fontT = New Drawing.Font(psFontName, FontSize_Tail, FontStyle.Regular)
                    Else
                        fontT = New Drawing.Font(psFontName, FontSize_Tail, FontStyle.Bold)
                    End If
                    e.Graphics.DrawString(sTail.Split(mcSEP)(i - 1), fontT, Drawing.Brushes.Black, _
                                    psngX, Convert.ToSingle(psngY + psngH - (iLineCnt - i) * fontT.GetHeight(e.Graphics) - fontL.GetHeight(e.Graphics)))
                End If
            Next

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Overridable Function RenderPage_Title(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
        Dim sFn As String = "Function RenderPage_Title"

        Try
            Dim font As New Drawing.Font(psFontName, FontSize_Title, FontStyle.Bold) 'Or FontStyle.Underline)
            Dim sngPrtH As Single

            Dim sf As New Drawing.StringFormat
            sf.LineAlignment = StringAlignment.Center
            sf.Alignment = Drawing.StringAlignment.Center

            sngPrtH = Convert.ToSingle(font.GetHeight(e.Graphics))

            Dim bax2 As New Drawing.Rectangle(Convert.ToInt32(psngX + 3), Convert.ToInt32(psngPrtY), 240, Convert.ToInt32(sngPrtH * 4))
            Dim rect1 As New Drawing.RectangleF(psngX, psngPrtY, psngW, sngPrtH * 4)

            e.Graphics.DrawString(Title, font, Drawing.Brushes.Black, rect1, sf)

#If DEBUG Then
            Dim rect As Drawing.Rectangle = New Drawing.Rectangle(Convert.ToInt32(psngX), Convert.ToInt32(psngPrtY), Convert.ToInt32(psngW), Convert.ToInt32(psngH))

            e.Graphics.DrawRectangle(Pens.LightSlateGray, rect)
#End If
            'Return : 변경된 Y
            Return psngPrtY + font.GetHeight(e.Graphics) * 3 '6

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Sub New()

    End Sub
End Class