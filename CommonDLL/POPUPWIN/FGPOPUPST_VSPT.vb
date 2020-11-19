Imports COMMON.SVar
Imports Oracle.DataAccess.Client

Public Class FGPOPUPST_VSPT
    Inherits System.Windows.Forms.Form

    Private Const mc_sFile As String = "File : FGPOPUPST_VSPT.vb, Class : FGPOPUPST_VSPT" & vbTab

    Private Const mc_iXmargin_right As Integer = 5
    Private Const mc_iYmargin_bottom As Integer = 20

    Private m_frm As Windows.Forms.Form
    Private m_dbCn As OracleConnection
    Private msBcNo As String = ""
    Private msTClsCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

    Private mbSave As Boolean = False
    Private mbActivated As Boolean = False
    Friend WithEvents btnClear As System.Windows.Forms.Button

    Protected psCdSep As String = ""

    Public ReadOnly Property Append() As Boolean
        Get
            Append = True
        End Get
    End Property

    Public WriteOnly Property UserID() As String
        Set(ByVal Value As String)
            msUsrID = Value
        End Set
    End Property

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_dbCn As OracleConnection, _
                                    ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm
        m_dbCn = r_dbCn
        msBcNo = rsBcNo
        msTClsCd = rsTClsCd
        msTNm = rsTNm

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbDisplayInit()

            Me.Cursor = Windows.Forms.Cursors.Default

            Me.ShowDialog(r_frm)

            Dim STU_StDataInfo As STU_StDataInfo
            Dim al_return As New ArrayList

            If mbSave Then
                STU_StDataInfo = New STU_StDataInfo
                STU_StDataInfo.Data = fnGet_CdAll()
                STU_StDataInfo.Alignment = 0
                al_return.Add(STU_StDataInfo)
                STU_StDataInfo = Nothing
            End If

            Return al_return

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        psCdSep = "SPT"
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
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rdoTitle As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSeq As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdoUse As System.Windows.Forms.RadioButton
    Friend WithEvents spdCd As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtCdOne As System.Windows.Forms.TextBox
    Friend WithEvents txtCdAll As System.Windows.Forms.TextBox
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents txtCdSeq As System.Windows.Forms.TextBox
    Friend WithEvents txtCdTitle As System.Windows.Forms.TextBox
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents txtWhere As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_VSPT))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoTitle = New System.Windows.Forms.RadioButton
        Me.rdoSeq = New System.Windows.Forms.RadioButton
        Me.txtCdOne = New System.Windows.Forms.TextBox
        Me.txtCdAll = New System.Windows.Forms.TextBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnSel = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCdSeq = New System.Windows.Forms.TextBox
        Me.txtCdTitle = New System.Windows.Forms.TextBox
        Me.btnDel = New System.Windows.Forms.Button
        Me.btnReg = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rdoAll = New System.Windows.Forms.RadioButton
        Me.rdoUse = New System.Windows.Forms.RadioButton
        Me.txtWhere = New System.Windows.Forms.TextBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.spdCd = New AxFPSpreadADO.AxfpSpread
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdCd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdoTitle)
        Me.Panel1.Controls.Add(Me.rdoSeq)
        Me.Panel1.Location = New System.Drawing.Point(4, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(160, 28)
        Me.Panel1.TabIndex = 1
        '
        'rdoTitle
        '
        Me.rdoTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTitle.Location = New System.Drawing.Point(88, 6)
        Me.rdoTitle.Name = "rdoTitle"
        Me.rdoTitle.Size = New System.Drawing.Size(64, 18)
        Me.rdoTitle.TabIndex = 1
        Me.rdoTitle.Text = "제목"
        '
        'rdoSeq
        '
        Me.rdoSeq.Checked = True
        Me.rdoSeq.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSeq.Location = New System.Drawing.Point(8, 6)
        Me.rdoSeq.Name = "rdoSeq"
        Me.rdoSeq.Size = New System.Drawing.Size(64, 18)
        Me.rdoSeq.TabIndex = 0
        Me.rdoSeq.TabStop = True
        Me.rdoSeq.Text = "코드"
        '
        'txtCdOne
        '
        Me.txtCdOne.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCdOne.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCdOne.Location = New System.Drawing.Point(4, 260)
        Me.txtCdOne.Multiline = True
        Me.txtCdOne.Name = "txtCdOne"
        Me.txtCdOne.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCdOne.Size = New System.Drawing.Size(696, 54)
        Me.txtCdOne.TabIndex = 9
        Me.txtCdOne.TabStop = False
        Me.txtCdOne.Text = "────────────────────────────────────────────────────────"
        '
        'txtCdAll
        '
        Me.txtCdAll.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCdAll.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCdAll.Location = New System.Drawing.Point(4, 352)
        Me.txtCdAll.Multiline = True
        Me.txtCdAll.Name = "txtCdAll"
        Me.txtCdAll.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCdAll.Size = New System.Drawing.Size(696, 232)
        Me.txtCdAll.TabIndex = 13
        Me.txtCdAll.TabStop = False
        Me.txtCdAll.Text = resources.GetString("txtCdAll.Text")
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(704, 548)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(96, 36)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "닫기(Esc)"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(704, 506)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(96, 36)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "저장(F2)"
        '
        'btnSel
        '
        Me.btnSel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSel.Location = New System.Drawing.Point(252, 316)
        Me.btnSel.Name = "btnSel"
        Me.btnSel.Size = New System.Drawing.Size(112, 32)
        Me.btnSel.TabIndex = 12
        Me.btnSel.Text = "▼   선택"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Navy
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(4, 236)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 21)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "코드"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Navy
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(134, 236)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 21)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "제목"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCdSeq
        '
        Me.txtCdSeq.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCdSeq.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCdSeq.Location = New System.Drawing.Point(68, 236)
        Me.txtCdSeq.MaxLength = 5
        Me.txtCdSeq.Name = "txtCdSeq"
        Me.txtCdSeq.Size = New System.Drawing.Size(45, 21)
        Me.txtCdSeq.TabIndex = 7
        Me.txtCdSeq.Text = "00000"
        '
        'txtCdTitle
        '
        Me.txtCdTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCdTitle.Location = New System.Drawing.Point(195, 236)
        Me.txtCdTitle.MaxLength = 200
        Me.txtCdTitle.Name = "txtCdTitle"
        Me.txtCdTitle.Size = New System.Drawing.Size(505, 21)
        Me.txtCdTitle.TabIndex = 8
        Me.txtCdTitle.Text = "0001"
        '
        'btnDel
        '
        Me.btnDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDel.Location = New System.Drawing.Point(704, 289)
        Me.btnDel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(96, 25)
        Me.btnDel.TabIndex = 11
        Me.btnDel.Text = "삭제"
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReg.Location = New System.Drawing.Point(704, 263)
        Me.btnReg.Margin = New System.Windows.Forms.Padding(0)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(96, 25)
        Me.btnReg.TabIndex = 10
        Me.btnReg.Text = "등록"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rdoAll)
        Me.Panel2.Controls.Add(Me.rdoUse)
        Me.Panel2.Location = New System.Drawing.Point(4, 64)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(160, 28)
        Me.Panel2.TabIndex = 3
        '
        'rdoAll
        '
        Me.rdoAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAll.Location = New System.Drawing.Point(88, 6)
        Me.rdoAll.Name = "rdoAll"
        Me.rdoAll.Size = New System.Drawing.Size(64, 18)
        Me.rdoAll.TabIndex = 1
        Me.rdoAll.Text = "전체"
        '
        'rdoUse
        '
        Me.rdoUse.Checked = True
        Me.rdoUse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoUse.Location = New System.Drawing.Point(8, 6)
        Me.rdoUse.Name = "rdoUse"
        Me.rdoUse.Size = New System.Drawing.Size(72, 18)
        Me.rdoUse.TabIndex = 0
        Me.rdoUse.TabStop = True
        Me.rdoUse.Text = "사용가능"
        '
        'txtWhere
        '
        Me.txtWhere.Location = New System.Drawing.Point(4, 36)
        Me.txtWhere.Name = "txtWhere"
        Me.txtWhere.Size = New System.Drawing.Size(160, 21)
        Me.txtWhere.TabIndex = 2
        Me.txtWhere.Text = "12345678901234567890"
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.spdCd)
        Me.Panel3.Location = New System.Drawing.Point(172, 6)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(628, 225)
        Me.Panel3.TabIndex = 93
        '
        'spdCd
        '
        Me.spdCd.DataSource = Nothing
        Me.spdCd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdCd.Location = New System.Drawing.Point(0, 0)
        Me.spdCd.Name = "spdCd"
        Me.spdCd.OcxState = CType(resources.GetObject("spdCd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCd.Size = New System.Drawing.Size(628, 225)
        Me.spdCd.TabIndex = 6
        '
        'chkAll
        '
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Location = New System.Drawing.Point(12, 100)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(152, 20)
        Me.chkAll.TabIndex = 4
        Me.chkAll.Text = "모든 등록자 코드 보기"
        '
        'btnSearch
        '
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Location = New System.Drawing.Point(20, 136)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(132, 52)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "검색"
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Location = New System.Drawing.Point(704, 236)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(96, 25)
        Me.btnClear.TabIndex = 94
        Me.btnClear.Text = "초기화"
        '
        'FGPOPUPST_VSPT
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(804, 582)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.txtWhere)
        Me.Controls.Add(Me.txtCdTitle)
        Me.Controls.Add(Me.txtCdSeq)
        Me.Controls.Add(Me.txtCdAll)
        Me.Controls.Add(Me.txtCdOne)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnSel)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Panel1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(812, 616)
        Me.MinimumSize = New System.Drawing.Size(812, 616)
        Me.Name = "FGPOPUPST_VSPT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "특수검사 모듈 ː 소견"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdCd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Function fnGet_CdAll() As String
        Dim sFn As String = "fnGet_CdAll"

        Try
            Dim sLeftMargin As String = "".PadRight(6)
            Dim sBuf As String = ""
            Dim sTmp As String = ""

            sBuf = Me.txtCdAll.Text.Replace(vbCr, "").Replace(vbLf, Convert.ToChar(1))

            If sBuf.IndexOf(Convert.ToChar(1)) >= 0 Then
                'Multi-line
                For i As Integer = 1 To sBuf.Split(Convert.ToChar(1)).Length
                    If sTmp.Length > 0 Then sTmp += vbCrLf + sLeftMargin

                    sTmp += sBuf.Split(Convert.ToChar(1))(i - 1)
                Next

                sBuf = sTmp
            Else
                'Single-line
                sBuf = sBuf
            End If

            sBuf += vbCrLf

            Return sBuf

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Sub sbDel()
        Dim sFn As String = "sbDel"

        Try
            If Me.spdCd.ActiveRow < 1 Then Return

            Dim iRow As Integer = Me.spdCd.ActiveRow

            Dim sCdSeq As String = COMMON.CommFN.Ctrl.Get_Code(Me.spdCd, "cdseq", iRow)
            Dim sCdTitle As String = COMMON.CommFN.Ctrl.Get_Code(Me.spdCd, "cdtitle", iRow)

            Dim sMsg As String = "코드 : " + sCdSeq + ", 제목 : " + sCdTitle + vbCrLf + vbCrLf

            If MsgBox("삭제하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.No Then
                Return
            End If

            If LISAPP.APP_G.CommFn.Del_CdList(m_dbCn, psCdSep, sCdSeq, msUsrID) Then
                sbDisplayInit_Cd()

                COMMON.CommFN.Ctrl.DisplayAfterDelete(Me.spdCd)
            End If

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg()
        Dim sFn As String = "sbReg"

        Try
            Dim sCdSeq As String = Me.txtCdSeq.Text
            Dim sCdTitle As String = Me.txtCdTitle.Text
            Dim sCdCont As String = Me.txtCdOne.Text

            If LISAPP.APP_G.CommFn.Set_CdList(m_dbCn, psCdSep, sCdSeq, sCdTitle, sCdCont, msUsrID) Then
                '재조회
                sbDisplay_CdList()
            End If

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_CdList()
        Dim sFn As String = "sbDisplay_CdList"

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            Dim dt As DataTable

            Dim sField As String = ""
            Dim sValue As String = ""

            If Me.rdoSeq.Checked Then
                sField = "cdseq"
            Else
                sField = "cdtitle"
            End If

            sValue = Me.txtWhere.Text

            dt = LISAPP.APP_G.CommFn.Get_CdList(m_dbCn, Me.rdoAll.Checked, psCdSep, sField, sValue, IIf(chkAll.Checked, "", msUsrID).ToString)

            '초기화
            Me.spdCd.MaxRows = 0

            If dt.Rows.Count > 0 Then
                With Me.spdCd
                    .ReDraw = False

                    .MaxRows = dt.Rows.Count

                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            Dim iCol As Integer = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = dt.Rows(i).Item(j).ToString
                            End If
                        Next

                        If IsNumeric(dt.Rows(i).Item("diffday").ToString()) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 1
                                .BlockMode = True : .BackColor = System.Drawing.Color.FromArgb(255, 220, 220) : .BlockMode = False
                            End If
                        End If
                    Next

                    .ReDraw = True
                End With
            End If

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Focus()

            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            '타이틀

            '위치
            Dim iLeft As Integer = COMMON.CommFN.Ctrl.FindControlLeft(m_frm)
            Dim iTop As Integer = COMMON.CommFN.Ctrl.FindControlTop(m_frm) + COMMON.CommFN.Ctrl.menuHeight

            iLeft += m_frm.Width - Me.Width - mc_iXmargin_right
            iTop += m_frm.Height - Me.Height - mc_iYmargin_bottom

            Me.Left = iLeft
            Me.Top = iTop

            '초기화
            Me.txtWhere.Text = ""

            sbDisplayInit_Cd()

            Me.txtCdAll.Text = ""

            Me.spdCd.MaxRows = 0

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_Cd()
        Dim sFn As String = "sbDisplayInit_Cd"

        Try
            '초기화
            Me.txtCdSeq.Text = ""
            Me.txtCdTitle.Text = ""
            Me.txtCdOne.Text = ""

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    '<----- Control Event ----->
    Private Sub Form_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If Me.spdCd.MaxRows > 0 Then Return
        If mbActivated Then Return

        mbActivated = True

        sbDisplay_CdList()
    End Sub

    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            btnClose_Click(Me.btnClose, Nothing)
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then
            btnSave_Click(Me.btnSave, Nothing)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click
        sbDel()
    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        sbReg()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        mbSave = True

        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        sbDisplay_CdList()
    End Sub

    Private Sub btnSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSel.Click
        If Me.txtCdOne.Text.Length = 0 Then Return

        Dim sBuf As String = Me.txtCdAll.Text

        If sBuf.Length > 0 Then sBuf += vbCrLf

        Me.txtCdAll.Text = sBuf + Me.txtCdOne.Text

        sbDisplayInit_Cd()

    End Sub

    Private Sub rdoAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAll.CheckedChanged
        If Me.rdoAll.Checked Then
            Me.btnReg.Enabled = False
            Me.btnDel.Enabled = False
        Else
            Me.btnReg.Enabled = True
            Me.btnDel.Enabled = True
        End If

        sbDisplayInit_Cd()

        Me.spdCd.MaxRows = 0
    End Sub

    Private Sub spdCd_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdCd.ClickEvent
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        Dim sCdSeq As String = COMMON.CommFN.Ctrl.Get_Code(Me.spdCd, "cdseq", e.row)
        Dim sCdTitle As String = COMMON.CommFN.Ctrl.Get_Code(Me.spdCd, "cdtitle", e.row)
        Dim sCdCont As String = COMMON.CommFN.Ctrl.Get_Code(Me.spdCd, "cdcont", e.row)

        Me.txtCdSeq.Text = sCdSeq
        Me.txtCdTitle.Text = sCdTitle
        Me.txtCdOne.Text = sCdCont
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        sbDisplayInit_Cd()

    End Sub
End Class

