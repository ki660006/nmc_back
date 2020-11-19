Imports COMMON.CommFN

Public Class FPOPUPFT
    Inherits System.Windows.Forms.Form

    'Input
    Public Columns As ArrayList = Nothing
    Public TopPoint As Integer
    Public LeftPoint As Integer

    'Output
    Public OutData_Cont As String = ""
    Public OutData_Syntax As String = ""
    Friend WithEvents chkPare2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPare1 As System.Windows.Forms.CheckBox

    Public Event ReturnPopupFilter(ByVal rsCont As String, ByVal rsSyntax As String)

    Private msFile As String = "File : FPOPUPCD.vb, Class : FPOPUPCD" + vbTab

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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnAddOr As System.Windows.Forms.Button
    Friend WithEvents btnAddAnd As System.Windows.Forms.Button
    Friend WithEvents btnAddSng As System.Windows.Forms.Button
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents cboOps As System.Windows.Forms.ComboBox
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents spdFilter As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents lblOps As System.Windows.Forms.Label
    Friend WithEvents lblColumns As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FPOPUPFT))
        Me.pnlUpper = New System.Windows.Forms.Panel
        Me.grpFilter = New System.Windows.Forms.GroupBox
        Me.chkPare2 = New System.Windows.Forms.CheckBox
        Me.chkPare1 = New System.Windows.Forms.CheckBox
        Me.btnAddOr = New System.Windows.Forms.Button
        Me.lblValue = New System.Windows.Forms.Label
        Me.lblOps = New System.Windows.Forms.Label
        Me.btnAddAnd = New System.Windows.Forms.Button
        Me.btnAddSng = New System.Windows.Forms.Button
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.cboOps = New System.Windows.Forms.ComboBox
        Me.cboColumns = New System.Windows.Forms.ComboBox
        Me.lblColumns = New System.Windows.Forms.Label
        Me.imgCheck = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlLower = New System.Windows.Forms.Panel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.pnlCenter = New System.Windows.Forms.Panel
        Me.spdFilter = New AxFPSpreadADO.AxfpSpread
        Me.pnlUpper.SuspendLayout()
        Me.grpFilter.SuspendLayout()
        Me.pnlLower.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlCenter.SuspendLayout()
        CType(Me.spdFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlUpper
        '
        Me.pnlUpper.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlUpper.Controls.Add(Me.grpFilter)
        Me.pnlUpper.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUpper.Location = New System.Drawing.Point(0, 0)
        Me.pnlUpper.Name = "pnlUpper"
        Me.pnlUpper.Size = New System.Drawing.Size(318, 115)
        Me.pnlUpper.TabIndex = 0
        '
        'grpFilter
        '
        Me.grpFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpFilter.Controls.Add(Me.chkPare2)
        Me.grpFilter.Controls.Add(Me.chkPare1)
        Me.grpFilter.Controls.Add(Me.btnAddOr)
        Me.grpFilter.Controls.Add(Me.lblValue)
        Me.grpFilter.Controls.Add(Me.lblOps)
        Me.grpFilter.Controls.Add(Me.btnAddAnd)
        Me.grpFilter.Controls.Add(Me.btnAddSng)
        Me.grpFilter.Controls.Add(Me.txtValue)
        Me.grpFilter.Controls.Add(Me.cboOps)
        Me.grpFilter.Controls.Add(Me.cboColumns)
        Me.grpFilter.Controls.Add(Me.lblColumns)
        Me.grpFilter.Location = New System.Drawing.Point(0, 0)
        Me.grpFilter.Name = "grpFilter"
        Me.grpFilter.Size = New System.Drawing.Size(317, 112)
        Me.grpFilter.TabIndex = 10
        Me.grpFilter.TabStop = False
        '
        'chkPare2
        '
        Me.chkPare2.AutoSize = True
        Me.chkPare2.Location = New System.Drawing.Point(101, 90)
        Me.chkPare2.Name = "chkPare2"
        Me.chkPare2.Size = New System.Drawing.Size(76, 16)
        Me.chkPare2.TabIndex = 8
        Me.chkPare2.Text = "닫기 괄호"
        Me.chkPare2.UseVisualStyleBackColor = True
        '
        'chkPare1
        '
        Me.chkPare1.AutoSize = True
        Me.chkPare1.Location = New System.Drawing.Point(8, 90)
        Me.chkPare1.Name = "chkPare1"
        Me.chkPare1.Size = New System.Drawing.Size(76, 16)
        Me.chkPare1.TabIndex = 7
        Me.chkPare1.Text = "열기 괄호"
        Me.chkPare1.UseVisualStyleBackColor = True
        '
        'btnAddOr
        '
        Me.btnAddOr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddOr.BackColor = System.Drawing.Color.White
        Me.btnAddOr.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddOr.Location = New System.Drawing.Point(224, 65)
        Me.btnAddOr.Name = "btnAddOr"
        Me.btnAddOr.Size = New System.Drawing.Size(83, 23)
        Me.btnAddOr.TabIndex = 5
        Me.btnAddOr.Text = "Or 추가"
        Me.btnAddOr.UseVisualStyleBackColor = False
        '
        'lblValue
        '
        Me.lblValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblValue.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblValue.ForeColor = System.Drawing.Color.White
        Me.lblValue.Location = New System.Drawing.Point(8, 64)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(61, 21)
        Me.lblValue.TabIndex = 6
        Me.lblValue.Text = "조건값"
        Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOps
        '
        Me.lblOps.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOps.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOps.ForeColor = System.Drawing.Color.White
        Me.lblOps.Location = New System.Drawing.Point(8, 40)
        Me.lblOps.Name = "lblOps"
        Me.lblOps.Size = New System.Drawing.Size(61, 21)
        Me.lblOps.TabIndex = 5
        Me.lblOps.Text = "연산자"
        Me.lblOps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAddAnd
        '
        Me.btnAddAnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddAnd.BackColor = System.Drawing.Color.White
        Me.btnAddAnd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddAnd.Location = New System.Drawing.Point(224, 40)
        Me.btnAddAnd.Name = "btnAddAnd"
        Me.btnAddAnd.Size = New System.Drawing.Size(83, 23)
        Me.btnAddAnd.TabIndex = 4
        Me.btnAddAnd.Text = "And 추가"
        Me.btnAddAnd.UseVisualStyleBackColor = False
        '
        'btnAddSng
        '
        Me.btnAddSng.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddSng.BackColor = System.Drawing.Color.White
        Me.btnAddSng.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddSng.Location = New System.Drawing.Point(224, 15)
        Me.btnAddSng.Name = "btnAddSng"
        Me.btnAddSng.Size = New System.Drawing.Size(83, 23)
        Me.btnAddSng.TabIndex = 3
        Me.btnAddSng.Text = "Single 추가"
        Me.btnAddSng.UseVisualStyleBackColor = False
        '
        'txtValue
        '
        Me.txtValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtValue.Location = New System.Drawing.Point(72, 64)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(146, 21)
        Me.txtValue.TabIndex = 2
        '
        'cboOps
        '
        Me.cboOps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOps.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboOps.Items.AddRange(New Object() {"* like *", "like *", "* like", "=", "<>", ">", ">=", "<", "<="})
        Me.cboOps.Location = New System.Drawing.Point(72, 40)
        Me.cboOps.MaxDropDownItems = 20
        Me.cboOps.Name = "cboOps"
        Me.cboOps.Size = New System.Drawing.Size(146, 20)
        Me.cboOps.TabIndex = 1
        '
        'cboColumns
        '
        Me.cboColumns.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColumns.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboColumns.Location = New System.Drawing.Point(72, 16)
        Me.cboColumns.MaxDropDownItems = 20
        Me.cboColumns.Name = "cboColumns"
        Me.cboColumns.Size = New System.Drawing.Size(146, 20)
        Me.cboColumns.TabIndex = 0
        '
        'lblColumns
        '
        Me.lblColumns.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColumns.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblColumns.ForeColor = System.Drawing.Color.White
        Me.lblColumns.Location = New System.Drawing.Point(8, 16)
        Me.lblColumns.Name = "lblColumns"
        Me.lblColumns.Size = New System.Drawing.Size(61, 21)
        Me.lblColumns.TabIndex = 0
        Me.lblColumns.Text = "컬럼명"
        Me.lblColumns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'imgCheck
        '
        Me.imgCheck.ImageStream = CType(resources.GetObject("imgCheck.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgCheck.TransparentColor = System.Drawing.Color.Transparent
        Me.imgCheck.Images.SetKeyName(0, "")
        Me.imgCheck.Images.SetKeyName(1, "")
        '
        'pnlLower
        '
        Me.pnlLower.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlLower.Controls.Add(Me.GroupBox2)
        Me.pnlLower.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLower.Location = New System.Drawing.Point(0, 327)
        Me.pnlLower.Name = "pnlLower"
        Me.pnlLower.Size = New System.Drawing.Size(318, 48)
        Me.pnlLower.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.btnOK)
        Me.GroupBox2.Controls.Add(Me.btnCancel)
        Me.GroupBox2.Controls.Add(Me.btnClear)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(318, 47)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.BackColor = System.Drawing.Color.White
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOK.Location = New System.Drawing.Point(140, 14)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(83, 24)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "확인 Enter"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(226, 14)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(83, 24)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "닫기 Esc"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.White
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear.Location = New System.Drawing.Point(8, 14)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(136, 24)
        Me.btnClear.TabIndex = 0
        Me.btnClear.Text = "필터내용 모두 지우기"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'pnlCenter
        '
        Me.pnlCenter.BackColor = System.Drawing.Color.Gainsboro
        Me.pnlCenter.Controls.Add(Me.spdFilter)
        Me.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCenter.Location = New System.Drawing.Point(0, 115)
        Me.pnlCenter.Name = "pnlCenter"
        Me.pnlCenter.Size = New System.Drawing.Size(318, 212)
        Me.pnlCenter.TabIndex = 1
        '
        'spdFilter
        '
        Me.spdFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdFilter.Location = New System.Drawing.Point(0, 0)
        Me.spdFilter.Name = "spdFilter"
        Me.spdFilter.OcxState = CType(resources.GetObject("spdFilter.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdFilter.Size = New System.Drawing.Size(318, 212)
        Me.spdFilter.TabIndex = 1
        '
        'FPOPUPFT
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(318, 375)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlCenter)
        Me.Controls.Add(Me.pnlLower)
        Me.Controls.Add(Me.pnlUpper)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(96, 96)
        Me.Name = "FPOPUPFT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "필터 내역 설정"
        Me.pnlUpper.ResumeLayout(False)
        Me.grpFilter.ResumeLayout(False)
        Me.grpFilter.PerformLayout()
        Me.pnlLower.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.pnlCenter.ResumeLayout(False)
        CType(Me.spdFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub Display()
        Me.Hide()

        If TopPoint > 0 And LeftPoint > 0 Then
            Me.Top = TopPoint
            Me.Left = LeftPoint
        End If

        Me.Show()
    End Sub

    Public Sub DisplayInit()
        Dim sFn As String = "DisplayInit"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdFilter

        Try
            Me.KeyPreview = True
            Me.Hide()

            If Not Columns Is Nothing Then
                Me.cboColumns.Items.Clear()

                For i As Integer = 1 To Columns.Count
                    Me.cboColumns.Items.Add(Columns(i - 1))
                Next
            End If

            spd.MaxRows = 0

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Sub

    Private Function fnFind_Cont() As String
        Dim sFn As String = "fnFind_Cont"

        Try
            Dim sReturn As String = ""

            sReturn += Me.cboColumns.SelectedItem.ToString().Substring(0, 100).TrimEnd()
            sReturn += " " + Me.cboOps.SelectedItem.ToString()
            sReturn += " " + Me.txtValue.Text

            Return sReturn

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Function

    Private Function fnFind_Syntax() As String
        Dim sFn As String = "fnFind_Syntax"

        Try
            Dim sColBuf As String = Ctrl.Get_Code(Me.cboColumns)
            Dim sOpBuf As String = Me.cboOps.SelectedItem.ToString()

            If sOpBuf = "" Then Return ""

            Dim sReturn As String = ""

            Dim r_txt As System.Windows.Forms.TextBox = Me.txtValue

            Select Case sOpBuf.ToLower
                Case "=", ">", "<", ">=", "<=", "<>"
                    sReturn = sColBuf + " " + sOpBuf.ToLower + " '" + r_txt.Text + "'"

                Case "* like *"
                    sReturn = "UPPER(" + sColBuf + ")" + " like UPPER( '%" + r_txt.Text + "%')"

                Case "* like"
                    sReturn = "UPPER(" + sColBuf + ")" + " like UPPER( '%" + r_txt.Text + " ')"

                Case "like *"
                    sReturn = "UPPER(" + sColBuf + ")" + " like UPPER( '" + r_txt.Text + "%')"

            End Select

            Return sReturn

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Function

    Private Sub sbAdd_Filter(ByVal rsOpt As String)
        Dim sFn As String = "sbAdd_Filter"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdFilter

            If Me.cboColumns.SelectedIndex < 0 Then
                MsgBox(Me.lblColumns.Text + "이(가) 선택되지 않았습니다. 확인하여 주십시요!!")
                Return
            End If

            If Me.cboOps.SelectedIndex < 0 Then
                MsgBox(Me.lblOps.Text + "이(가) 선택되지 않았습니다. 확인하여 주십시요!!")
                Return
            End If

            With spd
                Dim sCont As String = fnFind_Cont()
                Dim sSyntax As String = fnFind_Syntax()

                If sCont = "" Or sSyntax = "" Then
                    MsgBox("필터내용에 오류가 발생하였습니다!!")
                    Return
                End If

                .MaxRows += 1

                Select Case rsOpt
                    Case "S"
                        If chkPare1.Checked Then
                            .SetText(.GetColFromID("cont"), .MaxRows, "(" + sCont)
                            .SetText(.GetColFromID("syntax"), .MaxRows, "(" + sSyntax)
                        ElseIf chkPare2.Checked Then
                            .SetText(.GetColFromID("cont"), .MaxRows, sCont + ")")
                            .SetText(.GetColFromID("syntax"), .MaxRows, sSyntax + ")")
                        Else
                            .SetText(.GetColFromID("cont"), .MaxRows, sCont)
                            .SetText(.GetColFromID("syntax"), .MaxRows, sSyntax)
                        End If

                    Case "A"
                        If chkPare1.Checked Then
                            .SetText(.GetColFromID("cont"), .MaxRows, "And (" + sCont)
                            .SetText(.GetColFromID("syntax"), .MaxRows, "And (" + sSyntax)
                        ElseIf chkPare2.Checked Then
                            .SetText(.GetColFromID("cont"), .MaxRows, "And " + sCont + ")")
                            .SetText(.GetColFromID("syntax"), .MaxRows, "And " + sSyntax + ")")
                        Else
                            .SetText(.GetColFromID("cont"), .MaxRows, "And " + sCont)
                            .SetText(.GetColFromID("syntax"), .MaxRows, "And " + sSyntax)
                        End If

                    Case "O"
                        If chkPare1.Checked Then
                            .SetText(.GetColFromID("cont"), .MaxRows, "Or (" + sCont)
                            .SetText(.GetColFromID("syntax"), .MaxRows, "Or (" + sSyntax)
                        ElseIf chkPare2.Checked Then
                            .SetText(.GetColFromID("cont"), .MaxRows, "Or " + sCont + ")")
                            .SetText(.GetColFromID("syntax"), .MaxRows, "Or " + sSyntax + ")")
                        Else
                            .SetText(.GetColFromID("cont"), .MaxRows, "Or " + sCont)
                            .SetText(.GetColFromID("syntax"), .MaxRows, "Or " + sSyntax)
                        End If

                End Select
            End With

            chkPare1.Checked = False
            chkPare2.Checked = False

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Sub

    Private Sub sbGet_Filter_String()
        Dim sFn As String = "sbGet_Filter_String"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdFilter

            With spd
                OutData_Syntax = ""
                OutData_Cont = ""

                For i As Integer = 1 To .MaxRows
                    Dim sCont As String = Ctrl.Get_Code(spd, "cont", i)
                    Dim sSyntax As String = Ctrl.Get_Code(spd, "syntax", i)

                    If sSyntax <> "" Then
                        OutData_Syntax += " " + sSyntax
                    End If

                    If sCont <> "" Then
                        OutData_Cont += " " + sCont
                    End If
                Next
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Sub

    '<------- Control Event ------->

    Private Sub FPOPUP02_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub btnAddAnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAnd.Click
        If Me.spdFilter.MaxRows < 1 Then
            MsgBox("필터내용이 하나도 없으므로 추가하시려면 Single 추가를 사용하십시요!!")
            Return
        End If

        sbAdd_Filter("A")
    End Sub

    Private Sub btnAddOr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddOr.Click
        If Me.spdFilter.MaxRows < 1 Then
            MsgBox("필터내용이 하나도 없으므로 추가하시려면 Single 추가를 사용하십시요!!")
            Return
        End If

        sbAdd_Filter("O")
    End Sub

    Private Sub btnAddSng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSng.Click
        If Me.spdFilter.MaxRows > 0 Then
            MsgBox("이미 필터내용이 존재하므로 추가하시려면 And 추가 또는 Or 추가를 사용하십시요!!")
            Return
        End If

        sbAdd_Filter("S")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdFilter.MaxRows = 0

        RaiseEvent ReturnPopupFilter("", "")

        Me.Hide()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        sbGet_Filter_String()

        RaiseEvent ReturnPopupFilter(OutData_Cont, OutData_Syntax)

        Me.Hide()
    End Sub

    Private Sub spdFilter_DblClick(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdFilter.DblClick
        If e.row < 1 Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdFilter

        spd.DeleteRows(e.row, 1)
        spd.MaxRows -= 1
    End Sub

    Private Sub chkPare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPare1.Click, chkPare2.Click

        If CType(sender, Windows.Forms.CheckBox).Name = "chkPare1" Then
            If chkPare1.Checked Then
                If chkPare2.Checked Then chkPare2.Checked = False
            End If
        Else
            If chkPare2.Checked Then
                If chkPare1.Checked Then chkPare1.Checked = False
            End If
        End If

    End Sub
End Class