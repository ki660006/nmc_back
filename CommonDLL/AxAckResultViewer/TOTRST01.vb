Imports COMMON.CommFN
Imports COMMON.SVar.Login


Imports System.Drawing
Imports System.Windows.Forms

Public Class TOTRST01
    Inherits System.Windows.Forms.UserControl

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

    Private msIPAddress As String = ""
    Private msHostName As String = ""

    Private mcSEP As Char = Convert.ToChar(1)



    Public Property FastTestDateTime() As Boolean
        Get
            Return mbFastTestDateTime
        End Get
        Set(ByVal Value As Boolean)
            mbFastTestDateTime = Value
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

            dt = DA01.DA_SF.Get_Rst_Total(BcNo, dt_m, dt_c)

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
                    sFilter += " and "
                End If

                sFilter += "TORDSLIP = '" + OrdSlip + "'"
            End If

            If LabSlip.Length > 0 Then
                If sFilter.Length > 0 Then
                    sFilter += " and "
                End If

                sFilter += "labslip = '" + LabSlip + "'"
            End If

            Dim a_dr() As DataRow = dt.Select(sFilter, "")

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
        Dim sFn As String = "Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal rsOrdSlip As String)"

        Try
            OrdSlip = rsOrdSlip
            Display_Result(r_al_bcno)

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal rsOrdSlip As String, ByVal rsLabSlip As String)
        Dim sFn As String = "Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal rsOrdSlip As String, ByVal rsLabSlip As String)"

        Try
            OrdSlip = rsOrdSlip
            LabSlip = rsLabSlip

            Display_Result(r_al_bcno)

        Catch ex As Exception
            sbLog_Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

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

    Public Sub Print_Result(ByVal rbPreview As Boolean, ByVal riPrintMode As Integer)
        Dim sFn As String = "Sub Print_Result(ByVal rbPreview As Boolean)"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try


            Dim sAppID As String = "결과조회출력"
            msHostName = Net.Dns.GetHostName()
            msIPAddress = Fn.GetIPAddress(msHostName)
            Dim sPrtBcNo As String = fnFind_PrtBcNo()

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            '출력로그 DB 기록
            'DA01.CommDBFN.PrintMonitoring.printlog(sAppID, USER_INFO.USRID, sPrtBcNo, msHostName, msIPAddress)

            Dim al_cols As ArrayList = fnFind_PrtInfo_Cols()
            Dim al_labels As ArrayList = fnFind_PrtInfo_Labels()

            Dim prtrst As New PrintResult

            prtrst.Landscape = False

            Select Case riPrintMode
                Case 0
                    prtrst.Title = "검사결과 일일보고서"

                Case 1
                    prtrst.Title = "검사결과보고서"

                Case 2
                    prtrst.Title = "검사결과보고서(예비보고)"

            End Select

            prtrst.Cols = al_cols
            prtrst.Labels = al_labels
            prtrst.Tail = PRG_CONST.Tail_RstReport

            prtrst.Left_Margin_cm = 1.4
            prtrst.Right_Margin_cm = 1.3
            prtrst.Top_Margin_cm = 1.5
            prtrst.Bottom_Margin_cm = 1.5
            prtrst.mPrtPreview = rbPreview

            If rbPreview Then
                prtrst.PrintPreview(Me.spdRst)
            Else
                prtrst.Print(Me.spdRst)
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
    Friend WithEvents spdRst As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lstEx As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TOTRST01))
        Me.pnl = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstEx = New System.Windows.Forms.ListBox
        Me.spdRst = New AxFPSpreadADO.AxfpSpread
        Me.pnl.SuspendLayout()
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnl.Controls.Add(Me.spdRst)
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
        Me.Label4.Text = "△ 예비결과"
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
        'spdRst
        '
        Me.spdRst.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdRst.DataSource = Nothing
        Me.spdRst.Location = New System.Drawing.Point(0, 0)
        Me.spdRst.Name = "spdRst"
        Me.spdRst.OcxState = CType(resources.GetObject("spdRst.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRst.Size = New System.Drawing.Size(717, 524)
        Me.spdRst.TabIndex = 1
        '
        'TOTRST01
        '
        Me.Controls.Add(Me.pnl)
        Me.Name = "TOTRST01"
        Me.Size = New System.Drawing.Size(717, 548)
        Me.pnl.ResumeLayout(False)
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).EndInit()
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

            Dim sJM As String = Ctrl.Get_Code(spd, "judgmark", riRow)
            Dim sPM As String = Ctrl.Get_Code(spd, "panicmark", riRow)
            Dim sDM As String = Ctrl.Get_Code(spd, "deltamark", riRow)

            If sBcNo = "" Then Return ""
            If sTestCd = "" Then Return ""
            If sTNm = "" Then Return ""

            Dim sReturn As String = ""

            With spd
                'Check Abnormal
                If sJM.Length + sPM.Length + sDM.Length > 0 Then
                    .SetText(.GetColFromID("chk"), riRow, "1")
                End If

                '-- 2007-11-20 YEJ 수정
                'sReturn = fnFind_TNm(sBcNo, riRow)
                sReturn = fnFind_TNm(sBcNo, riRow)
                '-- 2007-11-20 YEJ end
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
                    sBuf = sBuf.Replace(vbCrLf, mcSEP)

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

            Dim sJM As String = Ctrl.Get_Code(spd, "judgmark", riRow)
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

            sReturn = sBcNo.PadRight(15) + " " + Fn.PadRightH(sTNm1, miLen_Tot1) + Fn.PadRightH(sTNm2, miLen_Tot2) + fnFind_Rst(sBcNo, riRow)

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

            Dim sJM As String = Ctrl.Get_Code(spd, "judgmark", riRow)
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

            Return ""
        End Try
    End Function

    Private Function fnFind_RstDtUsr(ByVal ra_dr() As DataRow, ByRef rsTestDt As String, ByRef rsTestUsr As String, _
                                        ByRef rsFnDt As String, ByRef rsFnUsr As String) As String
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
                    a_dr = dt.Select(sFilter + " and rstflg = '3' and fixrptusr <> ''", "rstdt desc")

                    If a_dr.Length > 0 Then
                        sLabDrNm = a_dr(0).Item("fixrptusr").ToString()
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
            prtcfg.PrtText = "  검사항목명"
            prtcfg.PrtX_Cm = 0
            prtcfg.PrtSize_Cm = 5.5
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "viewrst"
            prtcfg.PrtText = "결과"
            prtcfg.PrtX_Cm = 5.5
            prtcfg.PrtSize_Cm = 15.5
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "judgmark"
            prtcfg.PrtText = ""
            prtcfg.PrtX_Cm = 8.5
            prtcfg.PrtSize_Cm = 0.5
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "panicmark"
            prtcfg.PrtText = ""
            prtcfg.PrtX_Cm = 8.7
            prtcfg.PrtSize_Cm = 0.5
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "reftxt"
            prtcfg.PrtText = "참고치"
            prtcfg.PrtX_Cm = 11.5
            prtcfg.PrtSize_Cm = 9.5
            al_return.Add(prtcfg)

            prtcfg = New PrintCfg
            prtcfg.PrtAlign = PrintCfg.Align.Left
            prtcfg.PrtFont = New Drawing.Font("굴림체", 10)
            prtcfg.PrtID = "rstunit"
            prtcfg.PrtText = "단위"
            prtcfg.PrtX_Cm = 16.5
            prtcfg.PrtSize_Cm = 4.5
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
            prtcfg.PrtText = "출력일시 " + (New DA01.CommDBFN.ServerDateTime().GetDateTime).ToString("yyyy-MM-dd HH:mm")
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

            If sReturn = "" Then sReturn = sTestCd.PadRight(miLen_Cd1) + sTNm.Trim()
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
                                '결과 표시 <-- rstflg, UseViewReportOnly
                                sbDisplay_Result_ViewResult(ra_dr(i - 1), iLastRow + i)

                            Case "judgmark", "panicmark", "deltamark"
                                'Mark 표시 및 색상
                                sbDisplay_Result_Mark(ra_dr(i - 1), iLastRow + i, ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                            Case "rstflg"
                                '결과상태 표시
                                sbDisplay_Result_RstFlag(ra_dr(i - 1), iLastRow + i)

                            Case "srpt"
                                '특수보고서
                                sbDisplay_Result_SpRpt(ra_dr(i - 1), iLastRow + i)

                            Case Else
                                iCol = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                                If iCol > 0 Then
                                    .Col = iCol
                                    .Row = iLastRow + i
                                    .CellTag = ra_dr(i - 1).Item(j - 1).ToString()
                                    .Text = ra_dr(i - 1).Item(j - 1).ToString()
                                End If

                        End Select
                    Next
                Next

                '2) 멀티라인 결과 표시
                sbDisplay_Result_MultiLine_Rst(ra_dr, iLastRow + 1)

                '3) 멀티라인 참고치 표시
                sbDisplay_Result_MultiLine_Ref(ra_dr)

                '4) 미생물일 경우만 배양균, 항균제 표시
                If ra_dr(0).Item("bcno").ToString().Substring(8, 1) = PRG_CONST.BCCLS_MicorBio.Item(0).ToString.Substring(0, 1) Or (r_dt_m.Rows.Count > 0 And Not IsNumeric(ra_dr(0).Item("bcno").ToString().Substring(0, 1))) Then
                    sbDisplay_Result_Micro(r_dt_m)
                End If

                '5) 소견 표시
                sbDisplay_Result_Cmt(r_dt_c)

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
                If r_dr.Item("bcno").ToString().Substring(8, 1) = PRG_CONST.BCCLS_MicorBio.Item(0).ToString.Substring(0, 1) Then
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
                'iRowe --> 해당 BcNo의 Last Row !!

                Select Case r_dt_c.Rows(i - 1).Item("rstflg").ToString()
                    Case "3", "2"
                        '중간보고, 최종보고 --> 소견 그대로
                        If Fn.RemoveRightCrLf(r_dt_c.Rows(i - 1).Item("cmt").ToString()).Length > 0 Then
                            bExist = True

                            Exit For
                        End If

                    Case "1"
                        '결과저장 --> ViewReportOnly가 True : '', False : 소견 그대로
                        If mbViewReportOnly = False Then
                            If Fn.RemoveRightCrLf(r_dt_c.Rows(i - 1).Item("cmt").ToString()).Length > 0 Then
                                bExist = True

                                Exit For
                            End If
                        End If

                End Select
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
                .AddCellSpan(iCols, iRowe + 1, iCole - iCols + 1, 1)

                '0-3) 타이틀
                .SetText(.GetColFromID("tnm"), iRowe + 1, FixedVariable.gsMsg_Cmt)

                iRowe += 1
            End With

            '1) 소견의 rstflg에 따른 처리
            For i As Integer = 1 To r_dt_c.Rows.Count
                'iRowe --> 해당 BcNo의 Last Row !!

                Select Case r_dt_c.Rows(i - 1).Item("rstflg").ToString()
                    Case "3", "2"
                        '중간보고, 최종보고 --> 소견 그대로
                        iRowe = fnDisplay_Cmt(r_dt_c.Rows(i - 1), iRowe)

                    Case "1"
                        '결과저장 --> ViewReportOnly가 True : '', False : 소견 그대로
                        If mbViewReportOnly = False Then
                            iRowe = fnDisplay_Cmt(r_dt_c.Rows(i - 1), iRowe)
                        End If

                End Select
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

                    Case "J"
                        If r_dr.Item("judgmark").ToString() = "L" Then
                            .Col = .GetColFromID("judgmark")
                            .Row = riRow
                            .BackColor = FixedVariable.g_color_LM_Bg
                            .ForeColor = FixedVariable.g_color_LM_Fg

                            If r_dr.Item("panicmark").ToString() <> "P" Then
                                .Col = .GetColFromID("viewrst")
                                .Row = riRow
                                .BackColor = Color.FromArgb(221, 240, 255)
                                .ForeColor = Color.FromArgb(0, 0, 255)
                            End If
                        End If

                        If r_dr.Item("judgmark").ToString() = "H" Then
                            .Col = .GetColFromID("judgmark")
                            .Row = riRow
                            .BackColor = FixedVariable.g_color_HM_Bg
                            .ForeColor = FixedVariable.g_color_HM_Fg

                            If r_dr.Item("panicmark").ToString() <> "P" Then
                                .Col = .GetColFromID("viewrst")
                                .Row = riRow
                                .BackColor = Color.FromArgb(255, 230, 231)
                                .ForeColor = Color.FromArgb(255, 0, 0)
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

                        '배양균 결과와 항균제 결과에 따른 처리
                        For m As Integer = 1 To a_dr.Length
                            If a_dr(m - 1).Item("anticd").ToString() = "" Then
                                '1) 배양균
                                If a_dr.Length > 1 And m < a_dr.Length - 1 Then
                                    '다음 배양균과 같은 지(seq까지 포함한)의 여부
                                    If a_dr(m).Item("seq").ToString() = a_dr(m - 1).Item("seq").ToString() _
                                            And a_dr(m).Item("baccd").ToString() = a_dr(m - 1).Item("baccd").ToString() Then
                                        sbDisplay_Result_Micro_Bac(a_dr(m - 1), iRowe + m, True)
                                    Else
                                        sbDisplay_Result_Micro_Bac(a_dr(m - 1), iRowe + m, False)
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
                .SetText(iCols, riRow, r_dr.Item("antirst").ToString().PadRight(8) + r_dr.Item("decrst").ToString().Trim)

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

                '2) Expand란에 Picture 설정
                If rbMoreAnti Then
                    .Col = .GetColFromID("expand")
                    .Row = riRow
                    .CellTag = "1"
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                    .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)
                    .TypePictStretch = False
                End If

                Dim iCols As Integer = .GetColFromID("viewrst")
                'Dim iCole As Integer = .GetColFromID("deltamark")
                Dim iCole As Integer = .GetColFromID("rstunit")

                '3) Cell 합치기(결과란 ~ Delta란)
                .AddCellSpan(iCols, riRow, iCole - iCols + 1, 1)

                '4) 결과(배양균명)
                .SetText(iCols, riRow, "".PadRight(2) + r_dr.Item("bacnmd").ToString())

                '4-1) 결과 색상 처리
                .Col = iCols : .Col2 = iCole
                .Row = riRow : .Row2 = riRow
                .BlockMode = True : .BackColor = m_color_rst : .BlockMode = False

                iCols = .GetColFromID("reftxt")
                iCole = .GetColFromID("rstunit")

                '5) Cell 합치기(참고치란 ~ 단위란)
                .AddCellSpan(iCols, riRow, iCole - iCols + 1, 1)

                '6) 결과(증식정도)
                .SetText(iCols, riRow, r_dr.Item("incrst"))

                '6-1) 참고치 색상 처리
                .Col = iCols : .Col2 = iCole
                .Row = riRow : .Row2 = riRow
                .BlockMode = True : .BackColor = m_color_ref : .BlockMode = False

                '7) bcno, testcd, rowid 저장
                .SetText(.GetColFromID("bcno"), riRow, r_dr.Item("bcno").ToString())
                .SetText(.GetColFromID("testcd"), riRow, r_dr.Item("testcd").ToString())
                .SetText(.GetColFromID("rowid"), riRow, r_dr.Item("seq").ToString() + "," + r_dr.Item("baccd").ToString())
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
                                    If sBcNo.ToString().Substring(8, 1) = PRG_CONST.BCCLS_MicorBio.Item(0).ToString.Substring(0, 1) Then
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
                        If Ctrl.Get_Code_Tag(spd, "reftxt", i) <> sBuf Then
                            .SetText(.GetColFromID("reftxt"), i, sBuf)
                        End If
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
                    'Dim sBuf As String = Ctrl.Get_Code_Tag(spd, "viewrst", i)
                    Dim sBuf As String = Ctrl.Get_Code(spd, "viewrst", i)
                    Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)
                    Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
                    Dim sRowid As String = Ctrl.Get_Code(spd, "rowid", i)

                    Dim iMultiCnt As Integer = 0

                    If sBuf.IndexOf(vbCrLf) >= 0 Then
                        sBuf = sBuf.Replace(vbCrLf, mcSEP)

                        iMultiCnt = sBuf.Split(mcSEP).Length

                        If iMultiCnt > 1 Then
                            'InsertRow i + 1 행 앞에
                            .MaxRows += iMultiCnt - 1
                            .InsertRows(i + 1, iMultiCnt - 1)

                            For k As Integer = 1 To iMultiCnt
                                .SetText(.GetColFromID("viewrst"), i + k - 1, sBuf.Split(mcSEP)(k - 1))

                                If k > 1 Then
                                    ''1) 아이콘 지움 --> Cell을 StaticText로
                                    '.Col = .GetColFromID("chk") : .Row = i + k - 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                    '2) 결과란 Cell 합치기 <-- 미생물만
                                    Dim iCols As Integer = .GetColFromID("viewrst")
                                    Dim iCole As Integer = .GetColFromID("deltamark")

                                    '미생물일 경우만 Cell 합치기
                                    If ra_dr(i - riRows).Item("bcno").ToString().Substring(8, 1) = PRG_CONST.BCCLS_MicorBio.Item(0).ToString.Substring(0, 1) Then
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
                        If r_dr.Item("spcflg").ToString() = "1" Then
                            .SetText(.GetColFromID("viewrst"), riRow, FixedVariable.gsMsg_NoTk)
                        Else
                            If r_dr.Item("tcdgbn").ToString().Equals("B") = False Then
                                .SetText(.GetColFromID("viewrst"), riRow, FixedVariable.gsMsg_NoRpt)
                            End If
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
                .MaxRows = riRow + 7

                '1-1) 아이콘 지움 --> Cell을 StaticText로
                .Col = .GetColFromID("chk") : .Row = riRow + 1
                .Col2 = .GetColFromID("chk") : .Row2 = riRow + 7
                .BlockMode = True
                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                .BlockMode = False

                '1-2) Cell 합치기
                For i As Integer = 1 To 7
                    .AddCellSpan(iCols, riRow + i, iCole - iCols + 1, 1)
                Next

                '2) 검체정보 표시
                '2-0) 검체정보 저장
                With si
                    .BcNo = ra_dr(0).Item("bcno").ToString()
                    .SpcNm = ra_dr(0).Item("spcnmd").ToString()
                    .RegNo = ra_dr(0).Item("regno").ToString()
                    .DeptNm = ra_dr(0).Item("deptnm").ToString()
                    .OrdDt = ra_dr(0).Item("orddt").ToString()
                    .DoctorNm = ra_dr(0).Item("doctornm").ToString()
                    .PatNm = ra_dr(0).Item("patnm").ToString()
                    .WardRoom = ra_dr(0).Item("wardroom").ToString()
                    .CollDt = ra_dr(0).Item("colldt").ToString()
                    .CollUsr = ra_dr(0).Item("collusr").ToString()
                    .SexAge = ra_dr(0).Item("sexage").ToString()
                    .IdNo = ra_dr(0).Item("idno").ToString()
                    .EntDt = ra_dr(0).Item("entday").ToString()
                    .TkDt = ra_dr(0).Item("tkdt").ToString()
                    .TkUsr = ra_dr(0).Item("tkusr").ToString()

                    '.TestDt = ra_dr(0).Item("fndt").ToString()
                    '.TestUsr = ra_dr(0).Item("fnusr").ToString()
                    '.FnDt = ra_dr(0).Item("fndt").ToString()
                    '.LabDrNm = ra_dr(0).Item("labdrnm").ToString()
                    .LabDrNm = fnFind_RstDtUsr(ra_dr, .TestDt, .TestUsr, .FnDt, .FnUsr)

                    .DiagNm = ra_dr(0).Item("diagnm").ToString()
                    '.DrugNm = ra_dr(0).Item("drugnm").ToString()
                End With

                m_al_spcinfo.Add(si)

                '2-1) 위 라인
                '──────────────────────────────────────────────────
                sSpcInfo = ""
                If FixedVariable.giLen_Line Mod Fn.LengthH(FixedVariable.gsCharLine) = 0 Then
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine), Convert.ToChar(FixedVariable.gsCharLine))
                Else
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine) + 1, Convert.ToChar(FixedVariable.gsCharLine))
                End If
                .SetText(iCols, riRow + 1, sSpcInfo)

                '2-2)
                '등록번호 12345678            진료과 진단검사의학과    처방일시 2005-12-34 12:34  의뢰의사 아무개의사
                sSpcInfo = ""
                sSpcInfo += "등록번호 " + Fn.PadRightH(si.RegNo, 20)
                sSpcInfo += "진료과 " + Fn.PadRightH(si.DeptNm, 18)
                sSpcInfo += "처방일시 " + Fn.PadRightH(si.OrdDt, 18)
                sSpcInfo += "의뢰의사 " + si.DoctorNm
                .SetText(iCols, riRow + 2, sSpcInfo)

                '2-3)
                '성명     아무개아기아무개12  병동   75A/7501          채혈일시 2005-12-34 12:34  채혈자   채혈실담당
                sSpcInfo = ""
                sSpcInfo += "성명     " + Fn.PadRightH(si.PatNm, 20)
                sSpcInfo += "병동   " + Fn.PadRightH(si.WardRoom, 18)
                sSpcInfo += "채혈일시 " + Fn.PadRightH(si.CollDt, 18)
                sSpcInfo += "채혈자   " + si.CollUsr
                .SetText(iCols, riRow + 3, sSpcInfo)

                '2-4)
                'Sex/Age  F/33 (720121-1*)    입원일 2006-02-01        접수일시 2005-12-34 12:34  접수자   Serum TLA 
                sSpcInfo = ""
                sSpcInfo += "Sex/Age  " + Fn.PadRightH(si.SexAge, 5) + "(" + Fn.SubstringH(si.IdNo, 0, 9) + ")    "
                sSpcInfo += "입원일 " + Fn.PadRightH(si.EntDt, 18)
                sSpcInfo += "접수일시 " + Fn.PadRightH(si.TkDt, 18)
                sSpcInfo += "접수자   " + si.TkUsr
                .SetText(iCols, riRow + 4, sSpcInfo)

                '2-5)
                '검사일시 2005-12-34 12:34    검사자 검사실담당        보고일시 2005-12-34 12:34  확인의사 검사실의사
                sSpcInfo = ""
                sSpcInfo += "검사일시 " + Fn.PadRightH(si.TestDt, 20)
                sSpcInfo += "검사/확인" + " " + Fn.PadRightH(si.TestUsr, 15)
                sSpcInfo += "보고일시 " + Fn.PadRightH(si.FnDt, 18)
                sSpcInfo += "담당의사 " + si.LabDrNm
                .SetText(iCols, riRow + 5, sSpcInfo)

                '2-6)
                '검체번호 20060228-A0-0464-0  검체명 EDTA Whole Blood
                sSpcInfo = ""
                If si.BcNo.Length <> 15 Then
                    sSpcInfo += "검체번호 " + si.BcNo + "  "
                Else
                    sSpcInfo += "검체번호 " + Fn.BCNO_View(si.BcNo, True) + "  "
                End If
                sSpcInfo += "검체명 " + si.SpcNm
                .SetText(iCols, riRow + 6, sSpcInfo)

                '2-7) 아래 라인
                '─────────────────────────────────────────────────────
                sSpcInfo = ""
                If FixedVariable.giLen_Line Mod Fn.LengthH(FixedVariable.gsCharLine) = 0 Then
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine), Convert.ToChar(FixedVariable.gsCharLine))
                Else
                    sSpcInfo += "".PadRight(FixedVariable.giLen_Line \ Fn.LengthH(FixedVariable.gsCharLine) + 1, Convert.ToChar(FixedVariable.gsCharLine))
                End If
                .SetText(iCols, riRow + 7, sSpcInfo)

                If mbUseDebug = False Then
                    'Row 숨기기
                    For i As Integer = 1 To 7
                        If i <> 6 Then
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
                Dim sCmt As String = r_dr.Item("cmt").ToString()

                Select Case sTCdGbn
                    Case "B"
                        'Space(2) + Battery
                        .SetText(.GetColFromID("tnm"), riRow, "".PadRight(2) + sTNm.Trim)

                    Case "S", "P"
                        If r_dr.Item("tclscd").ToString() = "" Then
                            'Space(2) + 단독항목
                            .SetText(.GetColFromID("tnm"), riRow, "".PadRight(2) + sTNm.Trim)
                        Else
                            'Space(4) + Battery 내부항목
                            .SetText(.GetColFromID("tnm"), riRow, "".PadRight(4) + sTNm.Trim)
                        End If

                    Case "C"
                        If r_dr.Item("tclscd").ToString() = "" Then
                            'Space(2) + '… '
                            .SetText(.GetColFromID("tnm"), riRow, "".PadRight(2) + "… " + sTNm)
                        Else
                            'Space(4) + '… ' <-- Battery 내부항목
                            .SetText(.GetColFromID("tnm"), riRow, "".PadRight(4) + "… " + sTNm)
                        End If

                End Select

                .Row = riRow
                .Col = .GetColFromID("tnm") : .CellNote = sCmt

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

                Select Case r_dr.Item("rstflg").ToString()
                    Case "3", "2"
                        '중간보고, 최종보고 --> 결과 그대로
                        .SetText(.GetColFromID("viewrst"), riRow, r_dr.Item("viewrst"))

                    Case "1"
                        '결과저장 --> ViewReportOnly가 True : 검사중 메세지, False : 결과 그대로
                        If mbViewReportOnly Then
                            .SetText(.GetColFromID("viewrst"), riRow, FixedVariable.gsMsg_NoRpt)
                        Else
                            .SetText(.GetColFromID("viewrst"), riRow, r_dr.Item("viewrst"))
                        End If

                    Case Else
                        '채혈, 접수인 경우 --> 미접수, 검사중
                        If r_dr.Item("spcflg").ToString() = "1" Then
                            .SetText(.GetColFromID("viewrst"), riRow, FixedVariable.gsMsg_NoTk)
                        Else
                            If r_dr.Item("tcdgbn").ToString().Equals("B") = False Then
                                .SetText(.GetColFromID("viewrst"), riRow, FixedVariable.gsMsg_NoRpt)
                            End If
                        End If

                End Select
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

                    .Col = .GetColFromID("deltamark")
                    .ColHidden = False

                    .Col = .GetColFromID("rstflg")
                    .ColHidden = False

                    '.set_ColWidth(.GetColFromID("chk"), 2)
                    '.set_ColWidth(.GetColFromID("deltamark"), 2)
                    '.set_ColWidth(.GetColFromID("rstflg"), 2)
                Else
                    .Col = .GetColFromID("chk")
                    .ColHidden = True

                    .Col = .GetColFromID("deltamark")
                    .ColHidden = True

                    '.Col = .GetColFromID("rstflg")
                    '.ColHidden = False

                    '.set_ColWidth(.GetColFromID("chk"), 0)
                    '.set_ColWidth(.GetColFromID("deltamark"), 0)
                    ''.set_ColWidth(.GetColFromID("rstflg"), 0)
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

        End Select
    End Sub

    Private Sub spdRst_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdRst.LeaveCell
        If e.newRow < 1 Then Return
        If e.row = e.newRow Then Return
        If msBcNo = "" Then Return
        If m_al_spcinfo Is Nothing Then Return

        Dim sBcNo As String = Ctrl.Get_Code(Me.spdRst, "bcno", e.newRow)

        If sBcNo = "" Then Return

        If sBcNo <> msBcNo And m_al_spcinfo.Count > 0 Then
            For i As Integer = 1 To m_al_spcinfo.Count
                If CType(m_al_spcinfo(i - 1), SpecimenInfo).BcNo = sBcNo Then
                    msBcNo = sBcNo

                    RaiseEvent ChangedBcNo(CType(m_al_spcinfo(i - 1), SpecimenInfo))

                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub spdRst_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdRst.DblClick
        If mbUseDblCheck = False Then Exit Sub

        Select Case e.col
            Case Me.spdRst.GetColFromID("viewrst")
                sbDisplay_ChartView(e.row)

        End Select
    End Sub
End Class


