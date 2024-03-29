Imports COMMON.CommFN

Public Class FGPOPUPCD
    Inherits System.Windows.Forms.Form

    'Input
    Public Title As String = ""
    Public Columns As ArrayList = Nothing
    Public MultiRowEnable As Boolean = False
    Public TopPoint As Integer = 0
    Public LeftPoint As Integer = 0
    Public HeightPoint As Integer = 0
    Public WidthPoint As Integer = 0
    Public FilterTitle As String = ""
    Public objSender As Object
    Public HideSortIndicator As Boolean = False

    'Output
    Public OutData As DataTable = Nothing

    Private Const mc_sSelTxt As String = "▶"

    Private msFile As String = "File : FGPOPUPCD.vb, Class : FGPOPUPCD" + vbTab

    Private mbCenter As Boolean = False

    Private miSearchCol As Integer = 1
    Private miProcessing As Integer = 0

    Private m_dt_Filter As DataTable

    Private ma_dr As DataRow()

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
    End Sub

    Public Sub New(ByVal riTop As Integer, ByVal riLeft As Integer, ByVal riHeight As Integer, ByVal riWidth As Integer)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        Me.Top = riTop
        Me.Left = riLeft
        Me.Height = riHeight
        Me.Width = riWidth
    End Sub

    Public Sub New(ByVal rbCenter As Boolean)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        mbCenter = True
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
    Friend WithEvents imgCheck As System.Windows.Forms.ImageList
    Friend WithEvents pnlUpper As System.Windows.Forms.Panel
    Friend WithEvents pnlLower As System.Windows.Forms.Panel
    Friend WithEvents pnlCenter As System.Windows.Forms.Panel
    Friend WithEvents grpFilter As System.Windows.Forms.GroupBox
    Friend WithEvents cboFilter As System.Windows.Forms.ComboBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents grpSelect As System.Windows.Forms.GroupBox
    Friend WithEvents btnNone As System.Windows.Forms.Button
    Friend WithEvents btnAll As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFocus As System.Windows.Forms.TextBox
    Friend WithEvents lblWord As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents spdCH As AxFPSpreadADO.AxfpSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FGPOPUPCD))
        Me.pnlUpper = New System.Windows.Forms.Panel
        Me.grpFilter = New System.Windows.Forms.GroupBox
        Me.cboFilter = New System.Windows.Forms.ComboBox
        Me.lblFilter = New System.Windows.Forms.Label
        Me.grpSelect = New System.Windows.Forms.GroupBox
        Me.btnNone = New System.Windows.Forms.Button
        Me.btnAll = New System.Windows.Forms.Button
        Me.imgCheck = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlLower = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtFocus = New System.Windows.Forms.TextBox
        Me.lblWord = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.pnlCenter = New System.Windows.Forms.Panel
        Me.spdCH = New AxFPSpreadADO.AxfpSpread
        Me.pnlUpper.SuspendLayout()
        Me.grpFilter.SuspendLayout()
        Me.grpSelect.SuspendLayout()
        Me.pnlLower.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlCenter.SuspendLayout()
        CType(Me.spdCH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlUpper
        '
        Me.pnlUpper.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlUpper.Controls.Add(Me.grpFilter)
        Me.pnlUpper.Controls.Add(Me.grpSelect)
        Me.pnlUpper.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUpper.Location = New System.Drawing.Point(0, 0)
        Me.pnlUpper.Name = "pnlUpper"
        Me.pnlUpper.Size = New System.Drawing.Size(458, 36)
        Me.pnlUpper.TabIndex = 2
        '
        'grpFilter
        '
        Me.grpFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFilter.BackColor = System.Drawing.Color.Gainsboro
        Me.grpFilter.Controls.Add(Me.cboFilter)
        Me.grpFilter.Controls.Add(Me.lblFilter)
        Me.grpFilter.Location = New System.Drawing.Point(0, -6)
        Me.grpFilter.Name = "grpFilter"
        Me.grpFilter.Size = New System.Drawing.Size(278, 42)
        Me.grpFilter.TabIndex = 0
        Me.grpFilter.TabStop = False
        '
        'cboFilter
        '
        Me.cboFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilter.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboFilter.Location = New System.Drawing.Point(61, 13)
        Me.cboFilter.MaxDropDownItems = 20
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(209, 20)
        Me.cboFilter.TabIndex = 0
        '
        'lblFilter
        '
        Me.lblFilter.BackColor = System.Drawing.Color.SlateGray
        Me.lblFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilter.ForeColor = System.Drawing.Color.White
        Me.lblFilter.Location = New System.Drawing.Point(8, 13)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(52, 21)
        Me.lblFilter.TabIndex = 0
        Me.lblFilter.Text = "필터"
        Me.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpSelect
        '
        Me.grpSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSelect.BackColor = System.Drawing.Color.Gainsboro
        Me.grpSelect.Controls.Add(Me.btnNone)
        Me.grpSelect.Controls.Add(Me.btnAll)
        Me.grpSelect.Location = New System.Drawing.Point(276, -6)
        Me.grpSelect.Name = "grpSelect"
        Me.grpSelect.Size = New System.Drawing.Size(182, 42)
        Me.grpSelect.TabIndex = 1
        Me.grpSelect.TabStop = False
        '
        'btnNone
        '
        Me.btnNone.BackColor = System.Drawing.SystemColors.Control
        Me.btnNone.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNone.Location = New System.Drawing.Point(91, 12)
        Me.btnNone.Name = "btnNone"
        Me.btnNone.Size = New System.Drawing.Size(84, 24)
        Me.btnNone.TabIndex = 1
        Me.btnNone.Text = "선택취소(&C)"
        '
        'btnAll
        '
        Me.btnAll.BackColor = System.Drawing.SystemColors.Control
        Me.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAll.Location = New System.Drawing.Point(7, 12)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(83, 24)
        Me.btnAll.TabIndex = 0
        Me.btnAll.Text = "전체선택(&A)"
        '
        'imgCheck
        '
        Me.imgCheck.ImageSize = New System.Drawing.Size(13, 13)
        Me.imgCheck.ImageStream = CType(resources.GetObject("imgCheck.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgCheck.TransparentColor = System.Drawing.Color.Transparent
        '
        'pnlLower
        '
        Me.pnlLower.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlLower.Controls.Add(Me.GroupBox1)
        Me.pnlLower.Controls.Add(Me.GroupBox2)
        Me.pnlLower.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLower.Location = New System.Drawing.Point(0, 401)
        Me.pnlLower.Name = "pnlLower"
        Me.pnlLower.Size = New System.Drawing.Size(458, 38)
        Me.pnlLower.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Gainsboro
        Me.GroupBox1.Controls.Add(Me.txtFocus)
        Me.GroupBox1.Controls.Add(Me.lblWord)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(278, 43)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtFocus
        '
        Me.txtFocus.Font = New System.Drawing.Font("굴림", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFocus.Location = New System.Drawing.Point(252, 15)
        Me.txtFocus.Name = "txtFocus"
        Me.txtFocus.Size = New System.Drawing.Size(16, 18)
        Me.txtFocus.TabIndex = 0
        Me.txtFocus.Text = ""
        '
        'lblWord
        '
        Me.lblWord.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWord.BackColor = System.Drawing.SystemColors.Window
        Me.lblWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWord.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWord.Location = New System.Drawing.Point(61, 13)
        Me.lblWord.Name = "lblWord"
        Me.lblWord.Size = New System.Drawing.Size(209, 21)
        Me.lblWord.TabIndex = 0
        Me.lblWord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SlateBlue
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(8, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "검색어"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Gainsboro
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.Controls.Add(Me.btnOK)
        Me.GroupBox2.Location = New System.Drawing.Point(276, -5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(182, 43)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(92, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(83, 24)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "취소 Esc"
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.SystemColors.Control
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOK.Location = New System.Drawing.Point(8, 12)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(83, 24)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "확인 Enter"
        '
        'pnlCenter
        '
        Me.pnlCenter.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlCenter.Controls.Add(Me.spdCH)
        Me.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCenter.Location = New System.Drawing.Point(0, 36)
        Me.pnlCenter.Name = "pnlCenter"
        Me.pnlCenter.Size = New System.Drawing.Size(458, 365)
        Me.pnlCenter.TabIndex = 1
        '
        'spdCH
        '
        Me.spdCH.ContainingControl = Me
        Me.spdCH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdCH.Location = New System.Drawing.Point(0, 0)
        Me.spdCH.Name = "spdCH"
        Me.spdCH.OcxState = CType(resources.GetObject("spdCH.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCH.Size = New System.Drawing.Size(458, 365)
        Me.spdCH.TabIndex = 0
        '
        'FGPOPUPCD
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(458, 439)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlCenter)
        Me.Controls.Add(Me.pnlLower)
        Me.Controls.Add(Me.pnlUpper)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(96, 96)
        Me.Name = "FGPOPUPCD"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FPOPUPCD"
        Me.pnlUpper.ResumeLayout(False)
        Me.grpFilter.ResumeLayout(False)
        Me.grpSelect.ResumeLayout(False)
        Me.pnlLower.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.pnlCenter.ResumeLayout(False)
        CType(Me.spdCH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CheckSelectedData()
        Dim sFn As String = "Sub GetSelectedData"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

            With spd
                If .ActiveRow < 1 Then Return

                Dim sChk As String = Ctrl.Get_Code(spd, "chk", .ActiveRow)

                If MultiRowEnable Then
                    .SetText(.GetColFromID("chk"), .ActiveRow, "1")
                Else
                    .ClearRange(.GetColFromID("chk"), 1, .GetColFromID("chk"), .MaxRows, True)
                    .SetText(.GetColFromID("chk"), .ActiveRow, mc_sSelTxt)
                End If
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Sub

    Private Sub CheckToggleData()
        Dim sFn As String = "Sub CheckToggleData"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

            With spd
                If .ActiveRow < 1 Then Return

                Dim sChk As String = Ctrl.Get_Code(spd, "chk", .ActiveRow)

                If sChk = "1" Or sChk = mc_sSelTxt Then
                    .SetText(.GetColFromID("chk"), .ActiveRow, "")
                Else
                    If MultiRowEnable Then
                        .SetText(.GetColFromID("chk"), .ActiveRow, "1")
                    Else
                        .ClearRange(.GetColFromID("chk"), 1, .GetColFromID("chk"), .MaxRows, True)
                        .SetText(.GetColFromID("chk"), .ActiveRow, mc_sSelTxt)
                    End If
                End If
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Sub

    Public Sub DisplayData(ByVal r_dt As DataTable)
        Dim sFn As String = "Sub DisplayData"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

            If r_dt Is Nothing Then Return

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Ctrl.DisplayAfterSelect(spd, r_dt.Select(), True)

            With spd
                If HideSortIndicator Then
                    .set_ColUserSortIndicator(.GetColFromID("chk") + miSearchCol, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorNone)
                    .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSortNoIndicator
                Else
                    .set_ColUserSortIndicator(.GetColFromID("chk") + miSearchCol, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)
                    .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort
                End If
            End With

            spd.SetActiveCell(0, 0)

            If TopPoint > 0 And LeftPoint > 0 Then
                Me.Top = TopPoint
                Me.Left = LeftPoint
            End If

            If HeightPoint > 0 And WidthPoint > 0 Then
                Me.Height = HeightPoint
                Me.Width = WidthPoint
            End If

            Me.lblWord.Text = ""

            Me.grpFilter.Visible = False

            If MultiRowEnable = False Then
                Me.pnlUpper.Height = 0
            End If

            Me.txtFocus.Focus()

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            Me.ShowDialog()

        End Try
    End Sub

    Public Sub DisplayData(ByVal ra_dr As DataRow())
        Dim sFn As String = "Sub DisplayData"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

            If ra_dr Is Nothing Then Return

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Ctrl.DisplayAfterSelect(spd, ra_dr, True)

            With spd
                If HideSortIndicator Then
                    .set_ColUserSortIndicator(.GetColFromID("chk") + miSearchCol, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorNone)
                    .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSortNoIndicator
                Else
                    .set_ColUserSortIndicator(.GetColFromID("chk") + miSearchCol, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)
                    .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort
                End If
            End With

            spd.SetActiveCell(0, 0)

            If TopPoint > 0 And LeftPoint > 0 Then
                Me.Top = TopPoint
                Me.Left = LeftPoint
            End If

            If HeightPoint > 0 And WidthPoint > 0 Then
                Me.Height = HeightPoint
                Me.Width = WidthPoint
            End If

            Me.lblWord.Text = ""

            Me.grpFilter.Visible = False

            If MultiRowEnable = False Then
                Me.pnlUpper.Height = 0
            End If

            Me.txtFocus.Focus()

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            Me.ShowDialog()

        End Try
    End Sub

    Public Sub DisplayData(ByVal ra_dr As DataRow(), ByVal r_dt As DataTable)
        Dim sFn As String = "Sub DisplayData"

        Try
            miProcessing = 1

            Me.lblFilter.Text = FilterTitle

            Me.cboFilter.Items.Clear()

            For i As Integer = 1 To ra_dr.Length
                Dim sCd As String = "[" + ra_dr(i - 1).Item(0).ToString() + "]"
                Dim sNm As String = ra_dr(i - 1).Item(1).ToString()

                Me.cboFilter.Items.Add(sCd + " " + sNm)
            Next

            If Me.cboFilter.Items.Count > 0 Then
                Me.cboFilter.SelectedIndex = 0
            End If

            With Me.spdCH
                If HideSortIndicator Then
                    .set_ColUserSortIndicator(.GetColFromID("chk") + miSearchCol, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorNone)
                    .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSortNoIndicator
                Else
                    .set_ColUserSortIndicator(.GetColFromID("chk") + miSearchCol, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)
                    .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort
                End If
            End With

            If TopPoint > 0 And LeftPoint > 0 Then
                Me.Top = TopPoint
                Me.Left = LeftPoint
            End If

            If HeightPoint > 0 And WidthPoint > 0 Then
                Me.Height = HeightPoint
                Me.Width = WidthPoint
            End If

            Dim sCd_Filter As String = Ctrl.Get_Code(Me.cboFilter)

            'Filer할 필드명 저장
            Me.cboFilter.AccessibleName = ra_dr(0).Table.Columns(0).ColumnName.ToLower

            'Filter할 DataTable 저장
            m_dt_Filter = r_dt

            DisplayFilteredData(sCd_Filter)

            Me.lblWord.Text = ""

            Me.txtFocus.Focus()

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            miProcessing = 0

            Me.ShowDialog()

        End Try
    End Sub

    Public Sub DisplayData(ByVal ra_dr As DataRow(), ByVal r_dt As DataTable, ByVal rbHidden As Boolean)
        Dim sFn As String = "Sub DisplayData"

        Try
            miProcessing = 1

            Me.Hide()

            Me.lblFilter.Text = FilterTitle

            Me.cboFilter.Items.Clear()

            For i As Integer = 1 To ra_dr.Length
                Dim sCd As String = "[" + ra_dr(i - 1).Item(0).ToString() + "]"
                Dim sNm As String = ra_dr(i - 1).Item(1).ToString()

                Me.cboFilter.Items.Add(sCd + " " + sNm)
            Next

            If Me.cboFilter.Items.Count > 0 Then
                Me.cboFilter.SelectedIndex = 0
            End If

            With Me.spdCH
                If HideSortIndicator Then
                    .set_ColUserSortIndicator(.GetColFromID("chk") + miSearchCol, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorNone)
                    .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSortNoIndicator
                Else
                    .set_ColUserSortIndicator(.GetColFromID("chk") + miSearchCol, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)
                    .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort
                End If
            End With

            If TopPoint > 0 And LeftPoint > 0 Then
                Me.Top = TopPoint
                Me.Left = LeftPoint
            End If

            If HeightPoint > 0 And WidthPoint > 0 Then
                Me.Height = HeightPoint
                Me.Width = WidthPoint
            End If

            Dim sCd_Filter As String = Ctrl.Get_Code(Me.cboFilter)

            'Filer할 필드명 저장
            Me.cboFilter.AccessibleName = ra_dr(0).Table.Columns(0).ColumnName.ToLower

            'Filter할 DataTable 저장
            m_dt_Filter = r_dt

            DisplayFilteredData(sCd_Filter)

            Me.lblWord.Text = ""

            Me.txtFocus.Focus()

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            miProcessing = 0

            Me.ShowDialog()

        End Try
    End Sub

    Public Sub DisplayInit()
        Dim sFn As String = "Sub DisplayInit"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

        Try
            'Form
            Me.Text = Title
            Me.KeyPreview = True
            Me.Hide()

            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable

            '가운데 나타내기 경우
            If mbCenter Then
                Me.Top = Me.Owner.Top + +(Me.Owner.Height - Me.Height) \ 2
                Me.Left = Me.Owner.Left + (Me.Owner.Width - Me.Width) \ 2
            End If

            'Focus만을 위한 용도이므로 뒤로 숨김
            Me.txtFocus.SendToBack()

            'spdCH
            With spd
                .ReDraw = False

                .MaxCols = 1

                .Col = 1
                .ColID = "chk"
            End With

            For i As Integer = 1 To Columns.Count
                Dim sColNm As String = CType(Columns(i - 1), ColumnInfo).ColumnName
                Dim sColCa As String = CType(Columns(i - 1), ColumnInfo).ColumnCaption
                Dim iColWidth As Integer = CType(Columns(i - 1), ColumnInfo).ColumnSize

                'spdCH
                With Me.spdCH
                    .MaxCols += 1
                    .Col = .MaxCols

                    .set_ColWidth(.Col, iColWidth)

                    Dim sColID As String = ""

                    If sColNm.IndexOf(".") >= 0 Then
                        sColID = sColNm.Split("."c)(1)
                    Else
                        sColID = sColNm
                    End If

                    .ColID = sColID

                    .Row = 0
                    .Text = sColCa
                End With
            Next

            'spdCH
            With spd
                .Col = .GetColFromID("chk") + miSearchCol
                .Col2 = .MaxCols
                .Row = -1
                .Row2 = -1

                .BlockMode = True
                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                .BlockMode = False

                .Col = .GetColFromID("chk")
                .Col2 = .GetColFromID("chk")
                .Row = -1
                .Row2 = -1

                .BlockMode = True
                If MultiRowEnable Then
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox
                    .TypeCheckCenter = True
                Else
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                End If
                .BlockMode = False

                If .MaxCols > 1 Then
                    .Col = .GetColFromID("chk") + miSearchCol
                    .Col2 = .GetColFromID("chk") + miSearchCol
                    .Row = -1
                    .Row2 = -1

                    .BlockMode = True
                    .Font = New System.Drawing.Font("굴림체", 9, Drawing.FontStyle.Regular)
                    .BlockMode = False
                End If
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub DisplayFilteredData(ByVal rsCd As String)
        Dim sFn As String = "Sub DisplayFilteredData"

        If rsCd = "" Then Return

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

            If m_dt_Filter Is Nothing Then Return

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim sSortCol As String = ""
            Dim sSortOri As String = ""

            With spd
                For i As Integer = .GetColFromID("chk") + miSearchCol To .MaxCols
                    Select Case .get_ColUserSortIndicator(i)
                        Case FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending
                            .Col = i
                            sSortCol = .ColID
                            sSortOri = "asc"

                            Exit For

                        Case FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorDescending
                            .Col = i
                            sSortCol = .ColID
                            sSortOri = "desc"

                            Exit For

                    End Select
                Next
            End With

            Dim a_dr As DataRow()

            If sSortCol <> "" And sSortOri <> "" Then
                a_dr = m_dt_Filter.Select(Me.cboFilter.AccessibleName + " = '" + rsCd + "'", sSortCol + " " + sSortOri)
            Else
                a_dr = m_dt_Filter.Select(Me.cboFilter.AccessibleName + " = '" + rsCd + "'")
            End If

            Ctrl.DisplayAfterSelect(spd, a_dr, True)

            spd.SetActiveCell(0, 0)

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_Cd()
        Dim sFn As String = "Sub sbDisplay_BacCd"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

        Try
            With spd
                If ma_dr Is Nothing Then
                    .MaxRows = 0

                    Return
                End If

                .MaxRows = 0

                .ReDraw = False

                .MaxRows = ma_dr.Length

                For i As Integer = 1 To ma_dr.Length
                    For j As Integer = 1 To ma_dr(i - 1).Table.Columns.Count
                        Dim iCol As Integer = .GetColFromID(ma_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                        If iCol > 0 And i > 0 Then
                            .SetText(iCol, i, ma_dr(i - 1).Item(j - 1).ToString())
                        End If
                    Next
                Next
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub FindList(ByVal rsBuf As String)
        Dim sFn As String = "Sub FindList"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

            With spd
                'If rsBuf = "" Then Return

                Dim iFindRow As Integer = .SearchCol(.GetColFromID("chk") + miSearchCol, 0, .MaxRows, rsBuf, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)

                Do
                    Dim sCd As String = Ctrl.Get_Code(spd, .GetColFromID("chk") + miSearchCol, iFindRow)

                    If sCd.StartsWith(rsBuf) Then
                        Exit Do
                    Else
                        iFindRow = .SearchCol(.GetColFromID("chk") + miSearchCol, iFindRow, .MaxRows, rsBuf, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)
                    End If
                Loop While iFindRow > 0

                If iFindRow < 0 Then iFindRow = 0

                Ctrl.ChangeBackColor(spd, 1, .MaxCols, iFindRow, iFindRow)
                .SetActiveCell(.GetColFromID("chk") + miSearchCol, iFindRow)

                If MultiRowEnable = False Then
                    If iFindRow > 0 Then
                        CheckSelectedData()
                    Else
                        .ClearRange(.GetColFromID("chk"), 1, .GetColFromID("chk"), .MaxRows, True)
                    End If
                End If
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub GetSelectedData()
        Dim sFn As String = "Sub GetSelectedData"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

            With spd
                OutData = New DataTable

                'Col 정의
                For i As Integer = 1 To .MaxCols
                    .Col = i
                    .Row = 0

                    Dim dc As DataColumn = New DataColumn
                    dc.ColumnName = .ColID
                    dc.DataType = Type.GetType("System.String")
                    dc.Caption = .Text

                    OutData.Columns.Add(dc)
                Next

                For i As Integer = 1 To .MaxRows
                    Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)

                    If sChk = "1" Or sChk = mc_sSelTxt Then
                        'Row 추가
                        Dim dr As DataRow = OutData.NewRow()

                        For j As Integer = 1 To .MaxCols
                            .Col = j
                            .Row = i

                            dr.Item(.ColID) = .Text
                        Next

                        OutData.Rows.Add(dr)
                    End If
                Next
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    '<------- Control Event ------->
    Private Sub FGPOPUP02_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode = Windows.Forms.Keys.F1 Then
            MsgBox("T : " + Me.Top.ToString() + ",  L : " + Me.Left.ToString() + ",  H : " + Me.Height.ToString() + ",  W : " + Me.Width.ToString())
            Return
        End If

        Select Case e.KeyCode
            Case Windows.Forms.Keys.Enter
                Me.btnOK.PerformClick()

            Case Windows.Forms.Keys.Escape
                Me.btnCancel.PerformClick()

        End Select
    End Sub

    Private Sub FGPOPUP02_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Select Case Convert.ToInt32(e.KeyChar)
            Case Windows.Forms.Keys.Space
                CheckToggleData()

                Me.lblWord.Text = ""

            Case Windows.Forms.Keys.Back
                If Not Me.lblWord.Text = "" Then
                    Me.lblWord.Text = Me.lblWord.Text.Substring(0, Me.lblWord.Text.Length - 1)
                End If

            Case Windows.Forms.Keys.Delete
                Me.lblWord.Text = ""

            Case Else
                If Char.IsControl(e.KeyChar) = False Then
                    Me.lblWord.Text += e.KeyChar.ToString()
                End If

        End Select

        e.Handled = True
    End Sub

    Private Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAll.Click
        Ctrl.CheckYesAll(Me.spdCH, Me.spdCH.GetColFromID("chk"))
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        OutData = Nothing

        Me.Close()
    End Sub

    Private Sub btnNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNone.Click
        Ctrl.CheckNoAll(Me.spdCH, Me.spdCH.GetColFromID("chk"))
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        GetSelectedData()

        Me.Close()
    End Sub

    Private Sub cboFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFilter.SelectedIndexChanged
        If miProcessing = 1 Then Return

        DisplayFilteredData(Ctrl.Get_Code(Me.cboFilter))
    End Sub

    Private Sub spdCH_DblClick(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdCH.DblClick
        If e.row < 1 Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

        Ctrl.ChangeBackColor(spd, 1, spd.MaxCols, spd.ActiveRow, spd.ActiveRow)

        If MultiRowEnable = False Then
            CheckSelectedData()

            Me.btnOK.PerformClick()
        End If
    End Sub

    Private Sub spdCH_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdCH.ClickEvent
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        CheckToggleData()
    End Sub

    Private Sub spdCH_LeaveCell(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdCH.LeaveCell
        If e.col < 0 Then Return
        If e.row < 0 Then Return
        If e.newCol < 1 Then Return
        If e.newRow < 1 Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCH

        Ctrl.ChangeBackColor(spd, 1, spd.MaxCols, e.newRow, e.newRow)
    End Sub

    Private Sub lblWord_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWord.TextChanged
        If Me.spdCH.MaxRows < 1 Then Return

        FindList(Me.lblWord.Text)
    End Sub
End Class

Public Class ColumnInfo
    Public ColumnName As String
    Public ColumnCaption As String
    Public ColumnSize As Integer
End Class