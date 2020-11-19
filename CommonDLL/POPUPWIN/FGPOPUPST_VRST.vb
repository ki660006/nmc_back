Imports Oracle.DataAccess.Client

Imports COMMON.SVar

Public Class FGPOPUPST_VRST
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_VRST.vb, Class : FGPOPUPST_VRST" & vbTab

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

    Private msBetweenDay As String = "  ~  "

    Private miFontSize_10 As Integer = 10
    Friend WithEvents spclst1 As AxAckResultViewer.SPCLIST03
    Friend WithEvents trst1 As AxAckResultViewer.TOTRST03
    Private miFontSize_9 As Integer = 9

    Public ReadOnly Property Append() As Boolean
        Get
            Append = False
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
                STU_StDataInfo.Data = fnGet_Verify()
                STU_StDataInfo.Alignment = 0
                al_return.Add(STU_StDataInfo)
                STU_StDataInfo = Nothing
            End If

            Return al_return

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        Me.trst1.UseLab = True

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
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lstItem As System.Windows.Forms.ListBox
    Friend WithEvents lblEntDay As System.Windows.Forms.Label
    Friend WithEvents lblRstDay As System.Windows.Forms.Label
    Friend WithEvents lstRst As System.Windows.Forms.ListBox
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents rtb1 As AxAckRichTextBox.AxAckRichTextBox
    Friend WithEvents chkAutoSel As System.Windows.Forms.CheckBox
    Friend WithEvents btnUpDown As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstItem = New System.Windows.Forms.ListBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblEntDay = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblRstDay = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lstRst = New System.Windows.Forms.ListBox
        Me.btnSel = New System.Windows.Forms.Button
        Me.lblRegNo = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.rtb1 = New AxAckRichTextBox.AxAckRichTextBox
        Me.chkAutoSel = New System.Windows.Forms.CheckBox
        Me.btnUpDown = New System.Windows.Forms.Button
        Me.spclst1 = New AxAckResultViewer.SPCLIST03
        Me.trst1 = New AxAckResultViewer.TOTRST03
        Me.SuspendLayout()
        '
        'lstItem
        '
        Me.lstItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstItem.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstItem.ItemHeight = 12
        Me.lstItem.Items.AddRange(New Object() {"LA11001 Segmented neutrophils#", "Segmented neutrophils%"})
        Me.lstItem.Location = New System.Drawing.Point(704, 208)
        Me.lstItem.Name = "lstItem"
        Me.lstItem.ScrollAlwaysVisible = True
        Me.lstItem.Size = New System.Drawing.Size(300, 386)
        Me.lstItem.Sorted = True
        Me.lstItem.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(704, 640)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(300, 36)
        Me.btnClose.TabIndex = 85
        Me.btnClose.Text = "닫기(Esc)"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(704, 599)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(300, 36)
        Me.btnSave.TabIndex = 84
        Me.btnSave.Text = "저장(F2)"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.SlateBlue
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.LemonChiffon
        Me.Label1.Location = New System.Drawing.Point(704, 180)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(300, 26)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "검증항목"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Brown
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(148, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 22)
        Me.Label2.TabIndex = 87
        Me.Label2.Text = "입원일자"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEntDay
        '
        Me.lblEntDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblEntDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEntDay.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblEntDay.ForeColor = System.Drawing.Color.Black
        Me.lblEntDay.Location = New System.Drawing.Point(213, 5)
        Me.lblEntDay.Name = "lblEntDay"
        Me.lblEntDay.Size = New System.Drawing.Size(70, 22)
        Me.lblEntDay.TabIndex = 88
        Me.lblEntDay.Text = "2006-03-30"
        Me.lblEntDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(288, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 22)
        Me.Label4.TabIndex = 89
        Me.Label4.Text = "검사기간"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRstDay
        '
        Me.lblRstDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblRstDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRstDay.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstDay.ForeColor = System.Drawing.Color.Black
        Me.lblRstDay.Location = New System.Drawing.Point(353, 5)
        Me.lblRstDay.Name = "lblRstDay"
        Me.lblRstDay.Size = New System.Drawing.Size(169, 22)
        Me.lblRstDay.TabIndex = 90
        Me.lblRstDay.Text = "2006-03-30  ~  2006-03-30"
        Me.lblRstDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.SlateBlue
        Me.Label6.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.LemonChiffon
        Me.Label6.Location = New System.Drawing.Point(523, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(481, 21)
        Me.Label6.TabIndex = 91
        Me.Label6.Text = "유의한 결과"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lstRst
        '
        Me.lstRst.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstRst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstRst.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstRst.ItemHeight = 12
        Me.lstRst.Items.AddRange(New Object() {"1234567890123456789012345678901234567890123456789012345678901234567890", "LA001 CBC with Diff                 LA11501 Segmented neutrophil%       Result"})
        Me.lstRst.Location = New System.Drawing.Point(524, 29)
        Me.lstRst.Name = "lstRst"
        Me.lstRst.ScrollAlwaysVisible = True
        Me.lstRst.Size = New System.Drawing.Size(480, 146)
        Me.lstRst.Sorted = True
        Me.lstRst.TabIndex = 92
        '
        'btnSel
        '
        Me.btnSel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSel.Location = New System.Drawing.Point(479, 29)
        Me.btnSel.Name = "btnSel"
        Me.btnSel.Size = New System.Drawing.Size(44, 146)
        Me.btnSel.TabIndex = 93
        Me.btnSel.Text = "▶  선택"
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblRegNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRegNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNo.ForeColor = System.Drawing.Color.Black
        Me.lblRegNo.Location = New System.Drawing.Point(72, 5)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(70, 22)
        Me.lblRegNo.TabIndex = 95
        Me.lblRegNo.Text = "12345678"
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Navy
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(6, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 22)
        Me.Label5.TabIndex = 94
        Me.Label5.Text = "등록번호"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rtb1
        '
        Me.rtb1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rtb1.Location = New System.Drawing.Point(4, 620)
        Me.rtb1.Name = "rtb1"
        Me.rtb1.Size = New System.Drawing.Size(696, 56)
        Me.rtb1.TabIndex = 96
        Me.rtb1.Visible = False
        '
        'chkAutoSel
        '
        Me.chkAutoSel.BackColor = System.Drawing.Color.SlateBlue
        Me.chkAutoSel.ForeColor = System.Drawing.Color.White
        Me.chkAutoSel.Location = New System.Drawing.Point(530, 7)
        Me.chkAutoSel.Name = "chkAutoSel"
        Me.chkAutoSel.Size = New System.Drawing.Size(80, 18)
        Me.chkAutoSel.TabIndex = 97
        Me.chkAutoSel.Text = "자동 선택"
        Me.chkAutoSel.UseVisualStyleBackColor = False
        '
        'btnUpDown
        '
        Me.btnUpDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpDown.Location = New System.Drawing.Point(971, 5)
        Me.btnUpDown.Name = "btnUpDown"
        Me.btnUpDown.Size = New System.Drawing.Size(33, 21)
        Me.btnUpDown.TabIndex = 98
        Me.btnUpDown.Text = "▼"
        '
        'spclst1
        '
        Me.spclst1.CheckUseMode = False
        Me.spclst1.Location = New System.Drawing.Point(6, 30)
        Me.spclst1.Name = "spclst1"
        Me.spclst1.Size = New System.Drawing.Size(469, 145)
        Me.spclst1.TabIndex = 99
        Me.spclst1.UseDebug = False
        Me.spclst1.UseMode = 0
        Me.spclst1.UseTempRstState = False
        '
        'trst1
        '
        Me.trst1.FastTestDateTime = False
        Me.trst1.Location = New System.Drawing.Point(6, 180)
        Me.trst1.Name = "trst1"
        Me.trst1.Size = New System.Drawing.Size(695, 494)
        Me.trst1.TabIndex = 100
        Me.trst1.UseDblCheck = False
        Me.trst1.UseDebug = False
        Me.trst1.UseLab = False
        Me.trst1.ViewMark = False
        Me.trst1.ViewReportOnly = False
        '
        'FGPOPUPST_VRST
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(1008, 681)
        Me.Controls.Add(Me.spclst1)
        Me.Controls.Add(Me.btnUpDown)
        Me.Controls.Add(Me.lstRst)
        Me.Controls.Add(Me.chkAutoSel)
        Me.Controls.Add(Me.rtb1)
        Me.Controls.Add(Me.lblRegNo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnSel)
        Me.Controls.Add(Me.lstItem)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblRstDay)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblEntDay)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.trst1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(1016, 708)
        Me.Name = "FGPOPUPST_VRST"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "종합검증 모듈 ː 검증항목 및 유의한 결과"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Function fnGet_Verify() As String
        Dim sFn As String = "fnGet_Verify"

        Try
            Return Me.rtb1.get_SelRTF(True)

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Sub sbDisplay_EntInfo()
        Dim sFn As String = "sbDisplay_EntInfo"

        Try
            Dim dt As DataTable = (New DA_V).Get_EntInfo(m_dbCn, msBcNo)

            If dt.Rows.Count > 0 Then
                Me.lblRegNo.Text = dt.Rows(0).Item("regno").ToString()
                Me.lblEntDay.Text = dt.Rows(0).Item("entday").ToString()

                '< rem freety 2007/01/17 : 입원전날부터의 결과를 보기를 원함
                'Me.lblRstDay.Text = dt.Rows(0).Item("rstdays").ToString() + msBetweenDay + dt.Rows(0).Item("rstdaye").ToString()
                '>

                '< add freety 2007/01/17 : 입원전날부터의 결과를 보기를 원함
                Dim dtEntDay As Date = CType(Me.lblEntDay.Text, Date)

                Me.lblRstDay.Text = dtEntDay.Subtract(TimeSpan.FromDays(1)).ToShortDateString() + msBetweenDay + dt.Rows(0).Item("rstdaye").ToString()
                '>
            End If

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Focus()

        End Try
    End Sub

    Private Sub sbDisplay_SpcList()
        Dim sFn As String = "sbDisplay_SpcList"

        Try
            Dim iErr As Integer = 0

            Dim sRegNo As String = Me.lblRegNo.Text

            If Me.lblRegNo.Text.Length = 0 Then
                iErr += 1
            End If

            Dim sBuf As String = Me.lblRstDay.Text.Replace(msBetweenDay, "")
            Dim sRstDayS As String = ""
            Dim sRstDayE As String = ""

            If sBuf.Length = 20 Then
                sRstDayS = sBuf.Substring(0, 10).Replace("-", "")
                sRstDayE = sBuf.Substring(10, 10).Replace("-", "")
            Else
                iErr += 1
            End If

            If iErr > 0 Then
                MsgBox("해당 환자의 내역을 조회할 수 없습니다!1", MsgBoxStyle.Information, Me.Text)

                Return
            End If

            With Me.spclst1
                .Display_OrderList(sRegNo, sRstDayS, sRstDayE)

                If .RowCount < 1 Then
                    MsgBox("해당하는 결과가 없습니다!!", MsgBoxStyle.Information, Me.Text)
                End If
            End With

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Focus()

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
            Me.lblRegNo.Text = ""
            Me.lblEntDay.Text = ""
            Me.lblRstDay.Text = ""

            Me.spclst1.Clear()
            Me.trst1.Clear()

            Me.lstItem.Items.Clear()
            Me.lstRst.Items.Clear()

            Me.rtb1.set_SelRTF("", True)

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSave()

        sbSave_Item()
        sbSave_Rst()

    End Sub

    Private Sub sbSave_Item()
        Dim sFn As String = "sbSave_Item"

        Try
            If Me.lstItem.Items.Count < 1 Then Return

            Dim sHeader As String = "☞검증항목 (검사기간 : " + Me.lblRstDay.Text + ")" + vbCrLf

            Me.rtb1.set_SelText(sHeader, 0, miFontSize_10)

            Dim fn As COMMON.CommFN.Fn
            Dim fv As COMMON.CommConst.FixedVariable

            Dim sLeftMargin As String = "".PadRight(4)
            Dim sBuf As String = ""
            Dim sTmp As String = ""
            Dim sBody As String = ""

            For i As Integer = 1 To Me.lstItem.Items.Count
                If i = Me.lstItem.Items.Count Then
                    sTmp = Me.lstItem.Items.Item(i - 1).ToString().Substring(Me.trst1.Len_Cd1)
                Else
                    sTmp = Me.lstItem.Items.Item(i - 1).ToString().Substring(Me.trst1.Len_Cd1) + ", "
                End If

                If fn.LengthH(sBuf) + fn.LengthH(sTmp) > fv.FindLineLength(miFontSize_9) - sLeftMargin.Length Then
                    '다음 라인
                    sBuf += vbCrLf
                    sBody += sLeftMargin + sBuf

                    sBuf = ""
                    sBuf += sTmp
                Else
                    '현재 라인
                    sBuf += sTmp
                End If
            Next

            sBody += sLeftMargin + sBuf + vbCrLf + vbCrLf

            Me.rtb1.set_SelText(sBody, 0, miFontSize_9)

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSave_Rst()
        Dim sFn As String = "sbSave_Rst"

        Try
            If Me.lstRst.Items.Count < 1 Then Return

            Dim sHeader As String = "☞비정상 혹은 유의한 결과를 보이는 검사" + vbCrLf

            Me.rtb1.set_SelText(sHeader, 0, miFontSize_10)

            Dim fn As COMMON.CommFN.Fn
            Dim fv As COMMON.CommConst.FixedVariable

            Dim sLeftMargin As String = "".PadRight(4)
            Dim sBuf As String = ""
            Dim sTmp As String = ""
            Dim sBody As String = ""

            Dim sTBC1 As String = "", sTBC1_p As String = ""    '-- 2007-11-20 YEJ add
            Dim sTNm1 As String = "", sTNm1_p As String = ""
            Dim sTNm2 As String = "", sTNm2_p As String = ""
            Dim sRst As String = "", sRst_p As String = ""

            Dim sSEP1 As String = Convert.ToChar(2).ToString()
            Dim sSEP2 As String = Convert.ToChar(3).ToString()

            Dim sSEP3 As String = Convert.ToChar(5).ToString()
            Dim sSEP4 As String = Convert.ToChar(4).ToString()

            For i As Integer = 1 To Me.lstRst.Items.Count

                sTBC1 = fn.SubstringH(Me.lstRst.Items.Item(i - 1).ToString(), 0, Me.trst1.Len_Cd0)
                sTNm1 = fn.SubstringH(Me.lstRst.Items.Item(i - 1).ToString(), Me.trst1.Len_Cd0 + Me.trst1.Len_Cd1, Me.trst1.Len_Tot1 - Me.trst1.Len_Cd1)
                sTNm2 = fn.SubstringH(Me.lstRst.Items.Item(i - 1).ToString(), Me.trst1.Len_Cd0 + Me.trst1.Len_Tot1 + Me.trst1.Len_Cd2, Me.trst1.Len_Tot2 - Me.trst1.Len_Cd2)
                sRst = fn.SubstringH(Me.lstRst.Items.Item(i - 1).ToString(), Me.trst1.Len_Cd0 + Me.trst1.Len_Tot1 + Me.trst1.Len_Tot2)

                sTBC1 = sTBC1.Trim()
                sTNm1 = sTNm1.Trim()
                sTNm2 = sTNm2.Trim()
                sRst = sRst.Trim()

                'CBCWBC : 1.0RBC : 2.0<CR><LF>                                 -- 1)
                'CBCHGB : 3.0  SGOT(AST)4.05.0  SGPT(ALT)6.0<CR><LF>     -- 2)
                'BUN7.0<CR><LF>                                                  -- 3)
                '1) CBC [WBC : 1.0, RBC : 2.0]<CR><LF>
                '2) CBC [HGB : 3.0], SGOT(AST) [4.0, 5.0]  SGPT(ALT) [6.0]<CR><LF>
                '3) BUN [7.0]<CR><LF>
                If sTNm2.Length = 0 Then
                    sTmp = sTNm1 + sSEP3 + sSEP3 + sRst + sSEP4
                Else
                    sTmp = sTNm1 + sSEP1 + sSEP1 + sTNm2 + " : " + sRst + sSEP2
                End If

                If sTBC1 = sTBC1_p Then
                Else
                    If sTBC1.Length = 15 Then
                        sBody += IIf(sBuf = "", "", sLeftMargin).ToString + sBuf + IIf(sBuf = "", "", vbCrLf).ToString + sLeftMargin + _
                                 sTBC1.Substring(0, 8) + "-" + sTBC1.Substring(8, 2) + "-" + sTBC1.Substring(10, 4) + "-" + sTBC1.Substring(14, 1) + vbCrLf
                    Else

                        sBody += IIf(sBuf = "", "", sLeftMargin).ToString + sBuf + IIf(sBuf = "", "", vbCrLf).ToString + sLeftMargin + _
                                 sTBC1 + vbCrLf
                    End If
                    sTNm1_p = ""
                    sBuf = ""
                End If

                If sTNm1 = sTNm1_p Then
                    sTmp = sTmp.Replace(sTNm1 + sSEP1, "")
                    sTmp = sTmp.Replace(sTNm1 + sSEP3, "")
                Else
                    sTmp = "  " + sTmp
                End If

                If fn.LengthH(sBuf) + fn.LengthH(sTmp) > fv.FindLineLength(miFontSize_9) - sLeftMargin.Length Then
                    '다음 라인
                    sBuf += vbCrLf
                    sBody += sLeftMargin + sBuf

                    sBuf = ""
                    sBuf += sTmp

                    '앞뒤 공백제거
                    sBuf = sBuf.Trim()

                    'sSEP1 or sSEP3으로 시작되면 앞에 sTNm1 추가
                    If sBuf.StartsWith(sSEP1) Then
                        sBuf = sTNm1 + sSEP1 + sBuf
                    End If

                    If sBuf.StartsWith(sSEP3) Then
                        sBuf = sTNm1 + sSEP3 + sBuf
                    End If
                Else
                    '현재 라인
                    sBuf += sTmp

                    '앞뒤 공백제거
                    sBuf = sBuf.Trim()
                End If

                sTBC1_p = sTBC1
                sTNm1_p = sTNm1
                sTNm2_p = sTNm2
            Next

            'sSEP1 or sSEP3으로 시작되면 앞에 sTNm1 추가
            If sBuf.StartsWith(sSEP1) Then
                sBuf = sTNm1 + sSEP1 + sBuf
            End If

            If sBuf.StartsWith(sSEP3) Then
                sBuf = sTNm1 + sSEP3 + sBuf
            End If

            sBody += sLeftMargin + sBuf + vbCrLf

            '<sSEP2><CR><LF> = <CR><LF>, <sSEP4><CR><LF> = <CR><LF> --> "]<CR><LF>"
            sBody = sBody.Replace(sSEP2 + vbCrLf, "]" + vbCrLf)
            sBody = sBody.Replace(sSEP4 + vbCrLf, "]" + vbCrLf)

            '<sSEP2><Space><Space> = "  ", <sSEP4><Space><Space> = "  " --> "], "
            sBody = sBody.Replace(sSEP2 + "  ", "], ")
            sBody = sBody.Replace(sSEP4 + "  ", "], ")

            '<sSEP2><sSEP1> = , <sSEP4><sSEP3> =  --> ", "
            sBody = sBody.Replace(sSEP2 + sSEP1, ", ")
            sBody = sBody.Replace(sSEP4 + sSEP3, ", ")

            '<sSEP1><sSEP1> = , <sSEP3><sSEP3> =  --> " ["
            sBody = sBody.Replace(sSEP1 + sSEP1, " [")
            sBody = sBody.Replace(sSEP3 + sSEP3, " [")

            Me.rtb1.set_SelText(sBody, 0, miFontSize_9)

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    '<----- Control Event ----->
    Private Sub Form_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If Me.spclst1.RowCount > 0 Then Return
        If mbActivated Then Return

        mbActivated = True

        sbDisplay_EntInfo()
        sbDisplay_SpcList()

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

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        mbSave = True

        sbSave()

        Me.Close()
    End Sub

    Private Sub btnSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSel.Click
        With Me.trst1
            Dim al_return As ArrayList = .Find_Checked_Result()

            For i As Integer = 1 To al_return.Count
                If Not Me.lstRst.Items.Contains(al_return(i - 1)) Then
                    Me.lstRst.Items.Add(al_return(i - 1))
                End If
            Next
        End With
    End Sub

    Private Sub btnUpDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpDown.Click
        If Me.btnUpDown.Text = "▲" Then
            Me.lstRst.Height = 146
            Me.btnUpDown.Text = "▼"
        Else
            Me.lstRst.Height = 146 + Me.trst1.Height
            Me.btnUpDown.Text = "▲"
        End If
    End Sub

    Private Sub chkAutoSel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAutoSel.CheckedChanged
        If mbActivated = False Then Return

        If Me.chkAutoSel.Checked = False Then
            If Me.lstRst.Items.Count < 1 Then Return

            If MsgBox("유의한 결과의 전체내용을 지우시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Information, Me.Text) = MsgBoxResult.Yes Then
                Me.lstRst.Items.Clear()
            End If
        End If
    End Sub

    Private Sub lst_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstItem.DoubleClick, lstRst.DoubleClick
        Dim lst As Windows.Forms.ListBox = CType(sender, Windows.Forms.ListBox)

        Dim iSelindex As Integer = lst.SelectedIndex

        Dim sMsg As String = lst.SelectedItem().ToString() + vbCrLf + vbCrLf
        sMsg += "을(를) 삭제하시겠습니까?"

        If MsgBox(sMsg, MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
            Return
        End If

        lst.Items.RemoveAt(iSelindex)
    End Sub

    Private Sub spclst1_ChangeSelectedRow(ByVal r_al_bcno As System.Collections.ArrayList, ByVal r_al_TOrdSlip As System.Collections.ArrayList) Handles spclst1.ChangeSelectedRow
        If r_al_bcno.Count < 1 Then Return

        With Me.trst1
            .Display_Result(r_al_bcno, r_al_TOrdSlip)

            Dim al_return As ArrayList = .Check_Result()

            For i As Integer = 1 To al_return.Count
                If Not Me.lstItem.Items.Contains(al_return(i - 1)) Then
                    Me.lstItem.Items.Add(al_return(i - 1))
                End If
            Next
        End With

        If Me.chkAutoSel.Checked Then
            btnSel_Click(Me.btnSel, Nothing)
        End If

    End Sub
End Class

