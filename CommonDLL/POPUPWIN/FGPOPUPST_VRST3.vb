Imports Oracle.DataAccess.Client

Imports COMMON.SVar

Public Class FGPOPUPST_VRST3
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_VRST3.vb, Class : FGPOPUPST_VRST3" & vbTab

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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblEntDay As System.Windows.Forms.Label
    Friend WithEvents lblRstDay As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents rtb1 As AxAckRichTextBox.AxAckRichTextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnClose = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblEntDay = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblRstDay = New System.Windows.Forms.Label
        Me.lblRegNo = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.rtb1 = New AxAckRichTextBox.AxAckRichTextBox
        Me.spclst1 = New AxAckResultViewer.SPCLIST03
        Me.trst1 = New AxAckResultViewer.TOTRST03
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(481, 31)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(342, 144)
        Me.btnClose.TabIndex = 85
        Me.btnClose.Text = "닫기(Esc)"
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
        Me.rtb1.Location = New System.Drawing.Point(4, 613)
        Me.rtb1.Name = "rtb1"
        Me.rtb1.Size = New System.Drawing.Size(696, 56)
        Me.rtb1.TabIndex = 96
        Me.rtb1.Visible = False
        '
        'spclst1
        '
        Me.spclst1.CheckUseMode = False
        Me.spclst1.Location = New System.Drawing.Point(6, 30)
        Me.spclst1.MinimumSize = New System.Drawing.Size(469, 0)
        Me.spclst1.Name = "spclst1"
        Me.spclst1.Size = New System.Drawing.Size(469, 145)
        Me.spclst1.TabIndex = 99
        Me.spclst1.UseDebug = False
        Me.spclst1.UseMode = 0
        Me.spclst1.UseTempRstState = False
        '
        'trst1
        '
        Me.trst1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trst1.FastTestDateTime = False
        Me.trst1.Location = New System.Drawing.Point(5, 180)
        Me.trst1.Name = "trst1"
        Me.trst1.Size = New System.Drawing.Size(818, 490)
        Me.trst1.TabIndex = 100
        Me.trst1.UseDblCheck = False
        Me.trst1.UseDebug = False
        Me.trst1.UseLab = False
        Me.trst1.ViewMark = False
        Me.trst1.ViewReportOnly = False
        '
        'FGPOPUPST_VRST3
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(826, 674)
        Me.Controls.Add(Me.spclst1)
        Me.Controls.Add(Me.rtb1)
        Me.Controls.Add(Me.lblRegNo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblRstDay)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblEntDay)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.trst1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(834, 708)
        Me.Name = "FGPOPUPST_VRST3"
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

            Me.rtb1.set_SelRTF("", True)

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    '<----- Control Event ----->
    Private Sub Form_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If Me.spclst1.RowCount > 0 Then Return
        If mbActivated Then Return

        mbActivated = True

        spclst1.UseSPrst = True
        sbDisplay_EntInfo()
        sbDisplay_SpcList()

    End Sub

    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            btnClose_Click(Me.btnClose, Nothing)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub spclst1_ChangeSelectedRow(ByVal r_al_bcno As System.Collections.ArrayList, ByVal r_al_TOrdSlip As System.Collections.ArrayList) Handles spclst1.ChangeSelectedRow
        If r_al_bcno.Count < 1 Then Return

        With Me.trst1
            .Display_Result(r_al_bcno, r_al_TOrdSlip)
        End With

    End Sub
End Class

