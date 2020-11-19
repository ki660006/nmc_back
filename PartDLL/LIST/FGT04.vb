'-- 최종보고 통계
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports common.commlogin.login
Imports LISAPP.APP_T

Public Class FGT04
    Inherits System.Windows.Forms.Form

    Private Const mi_Analysis_Or_Reanalysis As Integer = 1

    Private miSelectKey As Integer = 0
    Private miMaxDiffDay As Integer = 100
    Private miMaxDiffMonth As Integer = 24
    Private miMaxDiffYear As Integer = 2

    Friend WithEvents rdoYear As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdStatistics As AxFPSpreadADO.AxfpSpread
    Friend WithEvents split1 As System.Windows.Forms.Splitter
    Friend WithEvents rdoIOC As System.Windows.Forms.RadioButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnSearch As CButtonLib.CButton
    Friend WithEvents btnAnalysis As CButtonLib.CButton
    Private m_fgt04_anal As New FGT04_ANALSVR

    Private Function fnDisplayStatistics() As Boolean
        Dim bReturn As Boolean = False

        Try
            Dim sStType As String = "", sDMYGbn As String = "", sDT1 As String = "", sDT2 As String = ""
            Dim sIO As String = "", sDept As String = "", sWard As String = ""
            Dim a_sDMY As String() = Nothing
            Dim iDMYDiff As Integer = 0, iSum As Integer = 0, iCnt As Integer = 0

            '기준시간 구분
            If Me.rdoOptDT1.Checked Then
                sStType = "T"
            Else
                sStType = "F"
            End If

            If Me.dtpDT1.Value > Me.dtpDT2.Value Then
                MsgBox("날짜구간 설정이 잘못되었습니다. 시작을 끝보다 작거나 같게 설정하십시요!!")

                Return False
            End If

            '> 일별/월별/연별 구분
            If Me.rdoDay.Checked Then
                '일별
                sDT1 = Me.dtpDT1.Value.ToString("yyyy-MM-dd")
                sDT2 = Me.dtpDT2.Value.ToString("yyyy-MM-dd")
                sDMYGbn = "D"

                iDMYDiff = CInt(DateDiff(DateInterval.Day, CDate(sDT1), CDate(sDT2)))

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Day, i - 1, CDate(sDT1)).ToShortDateString
                Next

                If a_sDMY.Length > miMaxDiffDay - 1 Then
                    MsgBox("일별로는 " & miMaxDiffDay.ToString & "개의 구간 까지만 검사통계를 조회할 수 있습니다. 날짜구간 또는 시간대를 다시 설정하십시요!!")

                    Return False
                End If

            ElseIf Me.rdoMonth.Checked Then
                '월별
                sDT1 = Me.dtpDT1.Value.ToString("yyyy-MM")
                sDT2 = Me.dtpDT2.Value.ToString("yyyy-MM")

                sDMYGbn = "M"

                iDMYDiff = CInt(DateDiff(DateInterval.Month, CDate(sDT1), CDate(sDT2)))

                If iDMYDiff > miMaxDiffMonth - 1 Then
                    MsgBox("월별로는 " & miMaxDiffMonth.ToString & "개월 까지의 검사통계를 조회할 수 있습니다. 날짜구간을 다시 설정하십시요!!")

                    Return False
                End If

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Month, i - 1, CDate(sDT1)).ToString("yyyy-MM")
                Next

            ElseIf Me.rdoYear.Checked Then
                '연별
                sDT1 = Me.dtpDT1.Value.ToString("yyyy")
                sDT2 = Me.dtpDT2.Value.ToString("yyyy")

                sDMYGbn = "Y"

                iDMYDiff = CInt(DateDiff(DateInterval.Year, CDate(sDT1 + "-01"), CDate(sDT2 + "-12")))

                If iDMYDiff > miMaxDiffMonth - 1 Then
                    MsgBox("연별로는 " & miMaxDiffYear.ToString & "년 까지의 검사통계를 조회할 수 있습니다. 날짜구간을 다시 설정하십시요!!")

                    Return False
                End If

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Year, i - 1, CDate(sDT1 + "-01")).ToString("yyyy")
                Next

            End If

            '전체

            '외래/입원
            If Me.rdoIOO.Checked Then
                sIO = "O"
            ElseIf Me.rdoIOI.Checked Then
                sIO = "I"
            ElseIf Me.rdoIOC.Checked Then
                sIO = "C"
            End If

            '진료과
            If Me.rdoDeptS.Enabled And Me.rdoDeptS.Checked Then sDept = Me.cboDept.Text.Split("|"c)(1)

            '병동
            If Me.rdoWardS.Enabled And Me.rdoWardS.Checked Then sWard = Me.cboWard.Text.Split("|"c)(1)

            sbInitialize_spdStatistics(a_sDMY)

            Dim dt As DataTable

            Dim strKey As String = ""
            Dim arlExm As New ArrayList
            Dim lngCnt1 As Long
            Dim lngCnt2 As Long
            Dim intCol As Integer

            dt = (New SrhFn).fnGet_Final_Statistics(sStType, sDMYGbn, sDT1.Replace("-", ""), sDT2.Replace("-", ""), sIO, sDept, sWard)

            If dt.Rows.Count > 0 Then
                With Me.spdStatistics
                    .ReDraw = False

                    For i As Integer = 0 To dt.Rows.Count - 1

                        strKey = dt.Rows(i).Item("slipcd").ToString
                        If arlExm.Contains(strKey) = False Then
                            arlExm.Add(strKey)

                            If .MaxRows > 0 Then
                                intCol = .GetColFromID("total")
                                If intCol > 0 Then
                                    .Row = .MaxRows
                                    .Col = intCol + 0 : .Text = lngCnt1.ToString
                                    .Col = intCol + 1 : .Text = lngCnt2.ToString
                                    If lngCnt1 = 0 Then
                                        .Col = intCol + 2 : .Text = "0.000"
                                    Else
                                        .Col = intCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.000")
                                    End If
                                End If
                            End If

                            .MaxRows += 1

                            .Row = .MaxRows
                            .Col = .GetColFromID("slipcd".ToLower) : .Text = dt.Rows(i).Item("slipcd").ToString
                            .Col = .GetColFromID("slipnmd".ToLower) : .Text = dt.Rows(i).Item("slipnmd").ToString

                            Dim intTmp As Integer = 0
                            intTmp = .GetColFromID("slipnmd".ToLower)

                            lngCnt1 = 0
                            lngCnt2 = 0
                        End If

                        Dim strDays As String = ""
                        strDays = dt.Rows(i).Item("days").ToString
                        If strDays.Length = 8 Then
                            strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2) + "-" + strDays.Substring(6, 2)
                        ElseIf strDays.Length = 6 Then
                            strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2)
                        End If

                        intCol = .GetColFromID(strDays)
                        If intCol > 0 Then
                            .Row = .MaxRows
                            .Col = intCol + 0 : .Text = dt.Rows(i).Item("cnt1").ToString
                            .Col = intCol + 1 : .Text = dt.Rows(i).Item("cnt2").ToString
                            If dt.Rows(i).Item("cnt3").ToString = "" Then
                                .Col = intCol + 2 : .Text = "0.000"
                            Else
                                .Col = intCol + 2 : .Text = Format(Convert.ToDouble(dt.Rows(i).Item("cnt3").ToString), "0.000")
                            End If

                            Dim strTmp As String = ""
                            strTmp = dt.Rows(i).Item("cnt3").ToString

                            lngCnt1 += Convert.ToInt32(Val(dt.Rows(i).Item("cnt1").ToString))
                            lngCnt2 += Convert.ToInt32(Val(dt.Rows(i).Item("cnt2").ToString))
                        End If
                    Next

                    intCol = .GetColFromID("total")
                    If intCol > 0 Then
                        .Row = .MaxRows
                        .Col = intCol + 0 : .Text = lngCnt1.ToString
                        .Col = intCol + 1 : .Text = lngCnt2.ToString
                        If lngCnt1 = 0 Then
                            .Col = intCol + 2 : .Text = "0.000"
                        Else
                            .Col = intCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.000")
                        End If
                    End If

                    .MaxRows += 1
                    .Row = .MaxRows
                    .Col = .GetColFromID("slipnmd".ToLower) : .Text = "Total"
                    For intIdx As Integer = .GetColFromID("slipnmd") + 1 To .MaxCols Step 3
                        Dim lngRstCnt As Long = 0
                        Dim lngModCnt As Long = 0

                        For intRow As Integer = 1 To .MaxRows - 1
                            Dim strTmp As String

                            .Row = intRow
                            .Col = intIdx + 0 : strTmp = .Text.Replace(",", "")
                            lngRstCnt += Convert.ToInt32(Val(strTmp))

                            .Col = intIdx + 1 : strTmp = .Text.Replace(",", "")
                            lngModCnt += Convert.ToInt32(Val(strTmp))
                        Next

                        .Row = .MaxRows
                        .Col = intIdx + 0 : .Text = lngRstCnt.ToString
                        .Col = intIdx + 1 : .Text = lngModCnt.ToString
                        If lngRstCnt > 0 Then
                            .Col = intIdx + 2 : .Text = Format((lngModCnt / lngRstCnt) * 100, "0.000")
                        Else
                            .Col = intIdx + 2 : .Text = "0.000"
                        End If

                    Next
                    .ReDraw = True
                End With
            Else
                spdStatistics.MaxRows = 0
            End If

            Return True
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Function

    Private Sub sbGetDeptInfo()

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboDept.Items.Add(dt.Rows(i).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(i).Item("deptcd").ToString)
                Next
            End If

            If Me.cboDept.Items.Count > 0 Then Me.cboDept.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbGetWardInfo()

        Try
            Dim dt As DataTable

            dt = OCSAPP.OcsLink.SData.fnGet_WardList

            Me.cboWard.Items.Clear()

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboWard.Items.Add(dt.Rows(i).Item("wardnm").ToString + Space(200) + "|" + dt.Rows(i).Item("wardno").ToString)
                Next

                If Me.cboWard.Items.Count > 1 Then Me.cboWard.SelectedIndex = 0
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbInitialize()

        Try
            miSelectKey = 1
            Dim sCurSysDate As String = ""

            Me.rdoDay.Checked = True
            '------------------------------
            Me.rdoOptDT1.Checked = True
            '------------------------------
            sCurSysDate = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")
            Me.dtpDT1.CustomFormat = "yyyy-MM-dd" : Me.dtpDT1.Value = CType(sCurSysDate & " 00:00:00", Date)
            Me.dtpDT2.CustomFormat = "yyyy-MM-dd" : Me.dtpDT2.Value = CType(sCurSysDate & " 23:59:59", Date)
            '------------------------------
            Me.rdoIOA.Checked = True

            Me.rdoDeptA.Checked = True
            Me.pnlDept.Enabled = False
            Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False

            Me.rdoWardA.Checked = True
            Me.pnlWard.Enabled = False
            Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False
            '------------------------------

            Dim bAuthority As Boolean = USER_SKILL.Authority("T01", mi_Analysis_Or_Reanalysis)

            Me.btnAnalysis.Enabled = bAuthority

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbInitialize_spdStatistics(ByVal ra_sDMY As String())

        Try
            With Me.spdStatistics
                .ReDraw = False

                .MaxRows = 0
                '코드, 검사파트, Total
                .MaxCols = 2

                If ra_sDMY.Length > 1 Then
                    .MaxCols += 3

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                    .Col = 3 : .ColID = "total" : .Text = "Total"

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                    .Col = 3 : .Text = "검사"
                    .Col = 4 : .Text = "수정"
                    .Col = 5 : .Text = "수정율"

                    .AddCellSpan(3, 0, 5, 1)
                End If

                For i As Integer = 0 To ra_sDMY.Length - 1
                    .MaxCols += 3

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                    .Col = .MaxCols - 2 : .ColID = ra_sDMY(i) : .Text = ra_sDMY(i)

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                    .Col = .MaxCols - 2 : .Text = "검사"
                    .Col = .MaxCols - 1 : .Text = "수정"
                    .Col = .MaxCols - 0 : .Text = "수정율"

                    .AddCellSpan(.MaxCols - 2, 0, .MaxCols, 1)
                Next

                .set_ColWidth(.GetColFromID("tsectnmd"), 12)

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

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
    Friend WithEvents tclStatistics As System.Windows.Forms.TabControl
    Friend WithEvents tpgVar As System.Windows.Forms.TabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpDT2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDT1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDt As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDay As System.Windows.Forms.RadioButton
    Friend WithEvents lblMonth As System.Windows.Forms.Label
    Friend WithEvents lblOptDT As System.Windows.Forms.Label
    Friend WithEvents lblIO As System.Windows.Forms.Label
    Friend WithEvents pnlIO As System.Windows.Forms.Panel
    Friend WithEvents pnlDept As System.Windows.Forms.Panel
    Friend WithEvents lbldept As System.Windows.Forms.Label
    Friend WithEvents lblWard As System.Windows.Forms.Label
    Friend WithEvents cboWard As System.Windows.Forms.ComboBox
    Friend WithEvents rdoWardS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoWardA As System.Windows.Forms.RadioButton
    Friend WithEvents cboDept As System.Windows.Forms.ComboBox
    Friend WithEvents rdoDeptS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDeptA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOO As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOI As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOptDT2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOptDT1 As System.Windows.Forms.RadioButton
    Friend WithEvents pnlWard As System.Windows.Forms.Panel

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGT04))
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
        Me.tclStatistics = New System.Windows.Forms.TabControl
        Me.tpgVar = New System.Windows.Forms.TabPage
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.spdStatistics = New AxFPSpreadADO.AxfpSpread
        Me.split1 = New System.Windows.Forms.Splitter
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblMonth = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoYear = New System.Windows.Forms.RadioButton
        Me.rdoMonth = New System.Windows.Forms.RadioButton
        Me.rdoDay = New System.Windows.Forms.RadioButton
        Me.lblOptDT = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rdoOptDT2 = New System.Windows.Forms.RadioButton
        Me.rdoOptDT1 = New System.Windows.Forms.RadioButton
        Me.lblDt = New System.Windows.Forms.Label
        Me.dtpDT1 = New System.Windows.Forms.DateTimePicker
        Me.dtpDT2 = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblIO = New System.Windows.Forms.Label
        Me.pnlIO = New System.Windows.Forms.Panel
        Me.rdoIOC = New System.Windows.Forms.RadioButton
        Me.rdoIOO = New System.Windows.Forms.RadioButton
        Me.rdoIOI = New System.Windows.Forms.RadioButton
        Me.rdoIOA = New System.Windows.Forms.RadioButton
        Me.lbldept = New System.Windows.Forms.Label
        Me.pnlDept = New System.Windows.Forms.Panel
        Me.rdoDeptS = New System.Windows.Forms.RadioButton
        Me.rdoDeptA = New System.Windows.Forms.RadioButton
        Me.cboDept = New System.Windows.Forms.ComboBox
        Me.lblWard = New System.Windows.Forms.Label
        Me.pnlWard = New System.Windows.Forms.Panel
        Me.rdoWardS = New System.Windows.Forms.RadioButton
        Me.rdoWardA = New System.Windows.Forms.RadioButton
        Me.cboWard = New System.Windows.Forms.ComboBox
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExcel = New CButtonLib.CButton
        Me.btnSearch = New CButtonLib.CButton
        Me.btnAnalysis = New CButtonLib.CButton
        Me.tclStatistics.SuspendLayout()
        Me.tpgVar.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdStatistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlIO.SuspendLayout()
        Me.pnlDept.SuspendLayout()
        Me.pnlWard.SuspendLayout()
        Me.SuspendLayout()
        '
        'tclStatistics
        '
        Me.tclStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tclStatistics.Controls.Add(Me.tpgVar)
        Me.tclStatistics.Location = New System.Drawing.Point(0, 0)
        Me.tclStatistics.Name = "tclStatistics"
        Me.tclStatistics.SelectedIndex = 0
        Me.tclStatistics.Size = New System.Drawing.Size(1016, 599)
        Me.tclStatistics.TabIndex = 0
        '
        'tpgVar
        '
        Me.tpgVar.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgVar.Controls.Add(Me.Panel4)
        Me.tpgVar.Controls.Add(Me.split1)
        Me.tpgVar.Controls.Add(Me.Panel3)
        Me.tpgVar.Location = New System.Drawing.Point(4, 21)
        Me.tpgVar.Name = "tpgVar"
        Me.tpgVar.Size = New System.Drawing.Size(1008, 574)
        Me.tpgVar.TabIndex = 0
        Me.tpgVar.Text = "조회조건설정"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(409, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(599, 574)
        Me.Panel4.TabIndex = 129
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.spdStatistics)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(600, 584)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'spdStatistics
        '
        Me.spdStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdStatistics.Location = New System.Drawing.Point(0, 10)
        Me.spdStatistics.Name = "spdStatistics"
        Me.spdStatistics.OcxState = CType(resources.GetObject("spdStatistics.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStatistics.Size = New System.Drawing.Size(599, 574)
        Me.spdStatistics.TabIndex = 0
        '
        'split1
        '
        Me.split1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.split1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.split1.Location = New System.Drawing.Point(404, 0)
        Me.split1.MinSize = 224
        Me.split1.Name = "split1"
        Me.split1.Size = New System.Drawing.Size(5, 574)
        Me.split1.TabIndex = 128
        Me.split1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel3.Controls.Add(Me.lblMonth)
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Controls.Add(Me.lblOptDT)
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Controls.Add(Me.lblDt)
        Me.Panel3.Controls.Add(Me.dtpDT1)
        Me.Panel3.Controls.Add(Me.dtpDT2)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.lblIO)
        Me.Panel3.Controls.Add(Me.pnlIO)
        Me.Panel3.Controls.Add(Me.lbldept)
        Me.Panel3.Controls.Add(Me.pnlDept)
        Me.Panel3.Controls.Add(Me.cboDept)
        Me.Panel3.Controls.Add(Me.lblWard)
        Me.Panel3.Controls.Add(Me.pnlWard)
        Me.Panel3.Controls.Add(Me.cboWard)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(404, 574)
        Me.Panel3.TabIndex = 24
        '
        'lblMonth
        '
        Me.lblMonth.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblMonth.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMonth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblMonth.Location = New System.Drawing.Point(4, 4)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(92, 21)
        Me.lblMonth.TabIndex = 24
        Me.lblMonth.Text = "일별/월별구분"
        Me.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Beige
        Me.Panel1.Controls.Add(Me.rdoYear)
        Me.Panel1.Controls.Add(Me.rdoMonth)
        Me.Panel1.Controls.Add(Me.rdoDay)
        Me.Panel1.Location = New System.Drawing.Point(97, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(168, 21)
        Me.Panel1.TabIndex = 25
        '
        'rdoYear
        '
        Me.rdoYear.BackColor = System.Drawing.Color.Beige
        Me.rdoYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoYear.Location = New System.Drawing.Point(114, 1)
        Me.rdoYear.Name = "rdoYear"
        Me.rdoYear.Size = New System.Drawing.Size(48, 19)
        Me.rdoYear.TabIndex = 14
        Me.rdoYear.Text = "연별"
        Me.rdoYear.UseVisualStyleBackColor = False
        '
        'rdoMonth
        '
        Me.rdoMonth.BackColor = System.Drawing.Color.Beige
        Me.rdoMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoMonth.Location = New System.Drawing.Point(60, 1)
        Me.rdoMonth.Name = "rdoMonth"
        Me.rdoMonth.Size = New System.Drawing.Size(48, 19)
        Me.rdoMonth.TabIndex = 12
        Me.rdoMonth.Text = "월별"
        Me.rdoMonth.UseVisualStyleBackColor = False
        '
        'rdoDay
        '
        Me.rdoDay.BackColor = System.Drawing.Color.Beige
        Me.rdoDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDay.Location = New System.Drawing.Point(4, 1)
        Me.rdoDay.Name = "rdoDay"
        Me.rdoDay.Size = New System.Drawing.Size(48, 19)
        Me.rdoDay.TabIndex = 11
        Me.rdoDay.Text = "일별"
        Me.rdoDay.UseVisualStyleBackColor = False
        '
        'lblOptDT
        '
        Me.lblOptDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOptDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOptDT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblOptDT.Location = New System.Drawing.Point(4, 26)
        Me.lblOptDT.Name = "lblOptDT"
        Me.lblOptDT.Size = New System.Drawing.Size(92, 21)
        Me.lblOptDT.TabIndex = 22
        Me.lblOptDT.Text = "기준시간 구분"
        Me.lblOptDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.AliceBlue
        Me.Panel2.Controls.Add(Me.rdoOptDT2)
        Me.Panel2.Controls.Add(Me.rdoOptDT1)
        Me.Panel2.Location = New System.Drawing.Point(97, 26)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(160, 21)
        Me.Panel2.TabIndex = 26
        '
        'rdoOptDT2
        '
        Me.rdoOptDT2.BackColor = System.Drawing.Color.AliceBlue
        Me.rdoOptDT2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOptDT2.Location = New System.Drawing.Point(84, 1)
        Me.rdoOptDT2.Name = "rdoOptDT2"
        Me.rdoOptDT2.Size = New System.Drawing.Size(72, 19)
        Me.rdoOptDT2.TabIndex = 12
        Me.rdoOptDT2.Text = "보고일시"
        Me.rdoOptDT2.UseVisualStyleBackColor = False
        '
        'rdoOptDT1
        '
        Me.rdoOptDT1.BackColor = System.Drawing.Color.AliceBlue
        Me.rdoOptDT1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOptDT1.Location = New System.Drawing.Point(4, 1)
        Me.rdoOptDT1.Name = "rdoOptDT1"
        Me.rdoOptDT1.Size = New System.Drawing.Size(72, 19)
        Me.rdoOptDT1.TabIndex = 11
        Me.rdoOptDT1.Text = "접수일시"
        Me.rdoOptDT1.UseVisualStyleBackColor = False
        '
        'lblDt
        '
        Me.lblDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblDt.Location = New System.Drawing.Point(4, 48)
        Me.lblDt.Name = "lblDt"
        Me.lblDt.Size = New System.Drawing.Size(92, 20)
        Me.lblDt.TabIndex = 27
        Me.lblDt.Text = "날짜구간 설정"
        Me.lblDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDT1
        '
        Me.dtpDT1.CustomFormat = "yyyy-MM-dd"
        Me.dtpDT1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDT1.Location = New System.Drawing.Point(97, 48)
        Me.dtpDT1.Name = "dtpDT1"
        Me.dtpDT1.Size = New System.Drawing.Size(96, 21)
        Me.dtpDT1.TabIndex = 28
        Me.dtpDT1.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'dtpDT2
        '
        Me.dtpDT2.CustomFormat = "yyyy-MM-dd"
        Me.dtpDT2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDT2.Location = New System.Drawing.Point(222, 49)
        Me.dtpDT2.Name = "dtpDT2"
        Me.dtpDT2.Size = New System.Drawing.Size(96, 21)
        Me.dtpDT2.TabIndex = 29
        Me.dtpDT2.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(200, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 16)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "~"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIO
        '
        Me.lblIO.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblIO.Location = New System.Drawing.Point(4, 69)
        Me.lblIO.Name = "lblIO"
        Me.lblIO.Size = New System.Drawing.Size(92, 21)
        Me.lblIO.TabIndex = 37
        Me.lblIO.Text = "외래/입원구분"
        Me.lblIO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlIO
        '
        Me.pnlIO.BackColor = System.Drawing.Color.Cornsilk
        Me.pnlIO.Controls.Add(Me.rdoIOC)
        Me.pnlIO.Controls.Add(Me.rdoIOO)
        Me.pnlIO.Controls.Add(Me.rdoIOI)
        Me.pnlIO.Controls.Add(Me.rdoIOA)
        Me.pnlIO.Location = New System.Drawing.Point(97, 69)
        Me.pnlIO.Name = "pnlIO"
        Me.pnlIO.Size = New System.Drawing.Size(220, 21)
        Me.pnlIO.TabIndex = 40
        '
        'rdoIOC
        '
        Me.rdoIOC.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOC.ForeColor = System.Drawing.Color.Black
        Me.rdoIOC.Location = New System.Drawing.Point(172, 1)
        Me.rdoIOC.Name = "rdoIOC"
        Me.rdoIOC.Size = New System.Drawing.Size(46, 19)
        Me.rdoIOC.TabIndex = 14
        Me.rdoIOC.Text = "수탁"
        Me.rdoIOC.UseVisualStyleBackColor = False
        '
        'rdoIOO
        '
        Me.rdoIOO.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOO.ForeColor = System.Drawing.Color.Black
        Me.rdoIOO.Location = New System.Drawing.Point(60, 1)
        Me.rdoIOO.Name = "rdoIOO"
        Me.rdoIOO.Size = New System.Drawing.Size(48, 19)
        Me.rdoIOO.TabIndex = 13
        Me.rdoIOO.Text = "외래"
        Me.rdoIOO.UseVisualStyleBackColor = False
        '
        'rdoIOI
        '
        Me.rdoIOI.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOI.ForeColor = System.Drawing.Color.Black
        Me.rdoIOI.Location = New System.Drawing.Point(116, 1)
        Me.rdoIOI.Name = "rdoIOI"
        Me.rdoIOI.Size = New System.Drawing.Size(48, 19)
        Me.rdoIOI.TabIndex = 12
        Me.rdoIOI.Text = "입원"
        Me.rdoIOI.UseVisualStyleBackColor = False
        '
        'rdoIOA
        '
        Me.rdoIOA.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOA.ForeColor = System.Drawing.Color.Black
        Me.rdoIOA.Location = New System.Drawing.Point(4, 1)
        Me.rdoIOA.Name = "rdoIOA"
        Me.rdoIOA.Size = New System.Drawing.Size(48, 19)
        Me.rdoIOA.TabIndex = 11
        Me.rdoIOA.Text = "전체"
        Me.rdoIOA.UseVisualStyleBackColor = False
        '
        'lbldept
        '
        Me.lbldept.BackColor = System.Drawing.Color.Lavender
        Me.lbldept.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbldept.ForeColor = System.Drawing.Color.Black
        Me.lbldept.Location = New System.Drawing.Point(96, 91)
        Me.lbldept.Name = "lbldept"
        Me.lbldept.Size = New System.Drawing.Size(52, 21)
        Me.lbldept.TabIndex = 41
        Me.lbldept.Text = " 진료과"
        Me.lbldept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDept
        '
        Me.pnlDept.BackColor = System.Drawing.Color.Honeydew
        Me.pnlDept.Controls.Add(Me.rdoDeptS)
        Me.pnlDept.Controls.Add(Me.rdoDeptA)
        Me.pnlDept.Location = New System.Drawing.Point(149, 91)
        Me.pnlDept.Name = "pnlDept"
        Me.pnlDept.Size = New System.Drawing.Size(99, 21)
        Me.pnlDept.TabIndex = 42
        '
        'rdoDeptS
        '
        Me.rdoDeptS.BackColor = System.Drawing.Color.Honeydew
        Me.rdoDeptS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDeptS.ForeColor = System.Drawing.Color.Black
        Me.rdoDeptS.Location = New System.Drawing.Point(50, 1)
        Me.rdoDeptS.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoDeptS.Name = "rdoDeptS"
        Me.rdoDeptS.Size = New System.Drawing.Size(46, 19)
        Me.rdoDeptS.TabIndex = 13
        Me.rdoDeptS.Text = "선택"
        Me.rdoDeptS.UseVisualStyleBackColor = False
        '
        'rdoDeptA
        '
        Me.rdoDeptA.BackColor = System.Drawing.Color.Honeydew
        Me.rdoDeptA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDeptA.ForeColor = System.Drawing.Color.Black
        Me.rdoDeptA.Location = New System.Drawing.Point(4, 1)
        Me.rdoDeptA.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoDeptA.Name = "rdoDeptA"
        Me.rdoDeptA.Size = New System.Drawing.Size(46, 19)
        Me.rdoDeptA.TabIndex = 11
        Me.rdoDeptA.Text = "전체"
        Me.rdoDeptA.UseVisualStyleBackColor = False
        '
        'cboDept
        '
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboDept.Location = New System.Drawing.Point(249, 92)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.Size = New System.Drawing.Size(150, 20)
        Me.cboDept.TabIndex = 43
        Me.cboDept.Tag = ""
        '
        'lblWard
        '
        Me.lblWard.BackColor = System.Drawing.Color.Lavender
        Me.lblWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWard.ForeColor = System.Drawing.Color.Black
        Me.lblWard.Location = New System.Drawing.Point(96, 114)
        Me.lblWard.Name = "lblWard"
        Me.lblWard.Size = New System.Drawing.Size(70, 21)
        Me.lblWard.TabIndex = 44
        Me.lblWard.Text = " 병동 선택"
        Me.lblWard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlWard
        '
        Me.pnlWard.BackColor = System.Drawing.Color.LavenderBlush
        Me.pnlWard.Controls.Add(Me.rdoWardS)
        Me.pnlWard.Controls.Add(Me.rdoWardA)
        Me.pnlWard.Location = New System.Drawing.Point(167, 113)
        Me.pnlWard.Name = "pnlWard"
        Me.pnlWard.Size = New System.Drawing.Size(99, 21)
        Me.pnlWard.TabIndex = 45
        '
        'rdoWardS
        '
        Me.rdoWardS.BackColor = System.Drawing.Color.LavenderBlush
        Me.rdoWardS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWardS.ForeColor = System.Drawing.Color.Black
        Me.rdoWardS.Location = New System.Drawing.Point(50, 1)
        Me.rdoWardS.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoWardS.Name = "rdoWardS"
        Me.rdoWardS.Size = New System.Drawing.Size(46, 19)
        Me.rdoWardS.TabIndex = 13
        Me.rdoWardS.Text = "선택"
        Me.rdoWardS.UseVisualStyleBackColor = False
        '
        'rdoWardA
        '
        Me.rdoWardA.BackColor = System.Drawing.Color.LavenderBlush
        Me.rdoWardA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWardA.ForeColor = System.Drawing.Color.Black
        Me.rdoWardA.Location = New System.Drawing.Point(4, 1)
        Me.rdoWardA.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoWardA.Name = "rdoWardA"
        Me.rdoWardA.Size = New System.Drawing.Size(46, 19)
        Me.rdoWardA.TabIndex = 11
        Me.rdoWardA.Text = "전체"
        Me.rdoWardA.UseVisualStyleBackColor = False
        '
        'cboWard
        '
        Me.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWard.Location = New System.Drawing.Point(267, 113)
        Me.cboWard.Name = "cboWard"
        Me.cboWard.Size = New System.Drawing.Size(132, 20)
        Me.cboWard.TabIndex = 46
        Me.cboWard.Tag = "TCDGBN_01"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems1
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker2
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(904, 601)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(107, 25)
        Me.btnExit.TabIndex = 199
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems2
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4859813!
        Me.btnClear.FocalPoints.CenterPtY = 0.16!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker4
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(796, 601)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 198
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems3
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
        Me.btnExcel.FocalPoints.FocusPtX = 0.03738318!
        Me.btnExcel.FocalPoints.FocusPtY = 0.04!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker6
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(688, 601)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(107, 25)
        Me.btnExcel.TabIndex = 197
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnSearch.ColorFillBlend = CBlendItems4
        Me.btnSearch.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSearch.Corners.All = CType(6, Short)
        Me.btnSearch.Corners.LowerLeft = CType(6, Short)
        Me.btnSearch.Corners.LowerRight = CType(6, Short)
        Me.btnSearch.Corners.UpperLeft = CType(6, Short)
        Me.btnSearch.Corners.UpperRight = CType(6, Short)
        Me.btnSearch.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSearch.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSearch.FocalPoints.CenterPtX = 0.4859813!
        Me.btnSearch.FocalPoints.CenterPtY = 0.16!
        Me.btnSearch.FocalPoints.FocusPtX = 0.0!
        Me.btnSearch.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.FocusPtTracker = DesignerRectTracker8
        Me.btnSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = Nothing
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.ImageIndex = 0
        Me.btnSearch.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSearch.Location = New System.Drawing.Point(580, 601)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSearch.SideImage = Nothing
        Me.btnSearch.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSearch.Size = New System.Drawing.Size(107, 25)
        Me.btnSearch.TabIndex = 196
        Me.btnSearch.Text = "통계조회"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSearch.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnAnalysis
        '
        Me.btnAnalysis.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnAnalysis.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnAnalysis.ColorFillBlend = CBlendItems5
        Me.btnAnalysis.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnAnalysis.Corners.All = CType(6, Short)
        Me.btnAnalysis.Corners.LowerLeft = CType(6, Short)
        Me.btnAnalysis.Corners.LowerRight = CType(6, Short)
        Me.btnAnalysis.Corners.UpperLeft = CType(6, Short)
        Me.btnAnalysis.Corners.UpperRight = CType(6, Short)
        Me.btnAnalysis.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnAnalysis.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnAnalysis.FocalPoints.CenterPtX = 0.4859813!
        Me.btnAnalysis.FocalPoints.CenterPtY = 0.16!
        Me.btnAnalysis.FocalPoints.FocusPtX = 0.0!
        Me.btnAnalysis.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnAnalysis.FocusPtTracker = DesignerRectTracker10
        Me.btnAnalysis.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnAnalysis.ForeColor = System.Drawing.Color.White
        Me.btnAnalysis.Image = Nothing
        Me.btnAnalysis.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAnalysis.ImageIndex = 0
        Me.btnAnalysis.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnAnalysis.Location = New System.Drawing.Point(4, 601)
        Me.btnAnalysis.Name = "btnAnalysis"
        Me.btnAnalysis.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnAnalysis.SideImage = Nothing
        Me.btnAnalysis.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnalysis.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnAnalysis.Size = New System.Drawing.Size(196, 25)
        Me.btnAnalysis.TabIndex = 200
        Me.btnAnalysis.Text = "최종보고 수정율 분석/재분석"
        Me.btnAnalysis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAnalysis.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnAnalysis.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGT04
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 629)
        Me.Controls.Add(Me.btnAnalysis)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.tclStatistics)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGT04"
        Me.Text = "최종보고 수정율 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tclStatistics.ResumeLayout(False)
        Me.tpgVar.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.spdStatistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlIO.ResumeLayout(False)
        Me.pnlDept.ResumeLayout(False)
        Me.pnlWard.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnExcel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sBuf As String = ""

        With spdStatistics
            .ReDraw = False

            .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

            .MaxRows += 2
            .InsertRows(1, 2)

            .Col = 1 : .Col2 = .MaxCols
            .Row = 1 : .Row2 = 2
            .BlockMode = True
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .BlockMode = False

            For i As Integer = 1 To .MaxCols
                .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0 : sBuf = .Text
                .Col = i : .Row = 1 : .Text = sBuf

                .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1 : sBuf = .Text
                .Col = i : .Row = 2 : .Text = sBuf
            Next
            If .ExportToExcel("statistics.xls", "Statistics", "") Then
                Process.Start("statistics.xls")
            End If

            .DeleteRows(1, 2)
            .MaxRows -= 2

            .ReDraw = True
        End With
    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Try
            Me.Close()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnSearch_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            fnDisplayStatistics()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub rdoDept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDeptA.CheckedChanged, rdoDeptS.CheckedChanged

        If miSelectKey = 1 Then Return

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoDeptA.Checked Then
                '전체
                Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False

            ElseIf Me.rdoDeptS.Checked Then
                '선택
                If Not Me.cboDept.Items.Count > 0 Then
                    sbGetDeptInfo()
                End If

                If Me.cboDept.Items.Count = 0 Then Return

                Me.cboDept.SelectedIndex = 0 : Me.cboDept.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoIO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIOA.CheckedChanged, rdoIOO.CheckedChanged, rdoIOI.CheckedChanged, rdoIOC.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoIOA.Checked Then
                '전체
                Me.rdoDeptA.Checked = True
                Me.rdoWardA.Checked = True

                Me.pnlDept.Enabled = False
                Me.pnlWard.Enabled = False

                Me.cboDept.Enabled = False
                Me.cboWard.Enabled = False

            ElseIf Me.rdoIOO.Checked Or Me.rdoIOC.Checked Then
                '외래
                Me.rdoWardA.Checked = True

                Me.pnlDept.Enabled = True
                Me.pnlWard.Enabled = False

            ElseIf Me.rdoIOI.Checked Then
                '입원
                Me.rdoDeptA.Checked = True

                Me.pnlDept.Enabled = False
                Me.pnlWard.Enabled = True

            End If

        Catch ex As Exception
           CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoWard_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoWardA.CheckedChanged, rdoWardS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoWardA.Checked Then
                '전체
                Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False

            ElseIf Me.rdoWardS.Checked Then
                '선택
                If Not Me.cboWard.Items.Count > 0 Then
                    sbGetWardInfo()
                End If

                If Me.cboWard.Items.Count = 0 Then Return

                Me.cboWard.SelectedIndex = 0 : Me.cboWard.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnAnalysis_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnalysis.Click

        Try
            '> m_fgt02_anal의 Control 값 변경
            If Me.rdoOptDT1.Checked Then
                m_fgt04_anal.lblDay.Text = "접수일자"

            ElseIf Me.rdoOptDT2.Checked Then
                m_fgt04_anal.lblDay.Text = "보고일자"

            End If

            Dim dtB As Date, dtE As Date

            If Me.rdoDay.Checked Then
                '일별
                dtB = Me.dtpDT1.Value
                dtE = Me.dtpDT2.Value

                m_fgt04_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt04_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            ElseIf Me.rdoMonth.Checked Then
                '월별
                dtB = CDate(Me.dtpDT1.Value.ToString("yyyy-MM") + "-" + "01")
                dtE = CDate(Me.dtpDT2.Value.ToString("yyyy-MM") + "-" + Date.DaysInMonth(Me.dtpDT2.Value.Year, Me.dtpDT2.Value.Month).ToString("00"))

                m_fgt04_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt04_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            ElseIf Me.rdoYear.Checked Then
                '연별
                dtB = CDate(Me.dtpDT1.Value.ToString("yyyy") + "-" + "01-01")
                dtE = CDate(Me.dtpDT2.Value.ToString("yyyy") + "-" + "12-31")

                m_fgt04_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt04_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            End If

            m_fgt04_anal.Display_ResultOfAnalysis()

            m_fgt04_anal.TopLevel = True
            m_fgt04_anal.Show()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub rdoDayMonthYear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDay.CheckedChanged, rdoMonth.CheckedChanged, rdoYear.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoDay.Checked Then
                '일별 체크 시
                Me.dtpDT1.CustomFormat = "yyyy-MM-dd"
                Me.dtpDT2.CustomFormat = "yyyy-MM-dd"

            ElseIf Me.rdoMonth.Checked Then
                '월별 체크 시
                Me.dtpDT1.CustomFormat = "yyyy-MM"
                Me.dtpDT2.CustomFormat = "yyyy-MM"

            ElseIf Me.rdoYear.Checked Then
                '연별 체크 시
                Me.dtpDT1.CustomFormat = "yyyy"
                Me.dtpDT2.CustomFormat = "yyyy"

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub FGT04_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGT04_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_ButtonClick(Nothing, Nothing)
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnClear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        spdStatistics.MaxRows = 0
    End Sub

End Class
