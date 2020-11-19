
Imports System.Windows.Forms
Imports COMMON.CommFN
Imports System.Drawing.Printing

Public Class FGPOUP_PRT
    Inherits System.Windows.Forms.Form
    Private Const sFile As String = "File : POPUPWIN.vb, Class : FGPWIN08" & vbTab

    Public msOwnerFrm As String = ""
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCopies As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents cboPort As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboPrinter As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTopM As System.Windows.Forms.TextBox
    Friend WithEvents txtLeftM As System.Windows.Forms.TextBox
    Dim objPrinter As COMMON.CommPrint.PRT_Printer

    Public ReadOnly Property mPrinterName() As String
        Get
            mPrinterName = objPrinter.GetInfo.PRTNM_S
        End Get
    End Property

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New(ByVal rsOwnerFrm As String)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        objPrinter = New COMMON.CommPrint.PRT_Printer(rsOwnerFrm)

        fnFormInitialize()

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCopies = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnSelect = New System.Windows.Forms.Button
        Me.cboPort = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.cboPrinter = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtTopM = New System.Windows.Forms.TextBox
        Me.txtLeftM = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(160, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 21)
        Me.Label3.TabIndex = 197
        Me.Label3.Text = "Top Margin"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCopies
        '
        Me.txtCopies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopies.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCopies.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCopies.Location = New System.Drawing.Point(90, 58)
        Me.txtCopies.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCopies.MaxLength = 2
        Me.txtCopies.Multiline = True
        Me.txtCopies.Name = "txtCopies"
        Me.txtCopies.Size = New System.Drawing.Size(40, 21)
        Me.txtCopies.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.Location = New System.Drawing.Point(281, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(22, 11)
        Me.Label6.TabIndex = 196
        Me.Label6.Text = "Cm"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(205, 120)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 26)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "취  소"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(130, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 11)
        Me.Label5.TabIndex = 194
        Me.Label5.Text = "Cm"
        '
        'btnSelect
        '
        Me.btnSelect.Location = New System.Drawing.Point(107, 120)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(96, 26)
        Me.btnSelect.TabIndex = 5
        Me.btnSelect.Text = "선  택"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'cboPort
        '
        Me.cboPort.DisplayMember = "AUTO LABELER (외래채혈실)"
        Me.cboPort.Items.AddRange(New Object() {"COM1", "COM2", "LPT1", "LPT2", "IP_172.17.112.32"})
        Me.cboPort.Location = New System.Drawing.Point(160, 81)
        Me.cboPort.Name = "cboPort"
        Me.cboPort.Size = New System.Drawing.Size(140, 20)
        Me.cboPort.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(160, 58)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(140, 22)
        Me.Label10.TabIndex = 202
        Me.Label10.Text = "출력위치(Port)"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Location = New System.Drawing.Point(5, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(300, 2)
        Me.Label9.TabIndex = 200
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.Location = New System.Drawing.Point(132, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(16, 11)
        Me.Label8.TabIndex = 199
        Me.Label8.Text = "매"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 21)
        Me.Label7.TabIndex = 198
        Me.Label7.Text = "출력매수"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboPrinter
        '
        Me.cboPrinter.DisplayMember = "AUTO LABELER (외래채혈실)"
        Me.cboPrinter.Location = New System.Drawing.Point(90, 9)
        Me.cboPrinter.Name = "cboPrinter"
        Me.cboPrinter.Size = New System.Drawing.Size(213, 20)
        Me.cboPrinter.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 21)
        Me.Label1.TabIndex = 195
        Me.Label1.Text = "프린터"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(9, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 21)
        Me.Label2.TabIndex = 190
        Me.Label2.Text = "Left Margin"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTopM
        '
        Me.txtTopM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTopM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTopM.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTopM.Location = New System.Drawing.Point(241, 33)
        Me.txtTopM.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTopM.MaxLength = 4
        Me.txtTopM.Multiline = True
        Me.txtTopM.Name = "txtTopM"
        Me.txtTopM.Size = New System.Drawing.Size(40, 21)
        Me.txtTopM.TabIndex = 2
        '
        'txtLeftM
        '
        Me.txtLeftM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLeftM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLeftM.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtLeftM.Location = New System.Drawing.Point(90, 33)
        Me.txtLeftM.Margin = New System.Windows.Forms.Padding(1)
        Me.txtLeftM.MaxLength = 4
        Me.txtLeftM.Multiline = True
        Me.txtLeftM.Name = "txtLeftM"
        Me.txtLeftM.Size = New System.Drawing.Size(40, 21)
        Me.txtLeftM.TabIndex = 1
        '
        'FGPOUP_PRT
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(310, 155)
        Me.Controls.Add(Me.txtLeftM)
        Me.Controls.Add(Me.txtTopM)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCopies)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.cboPort)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboPrinter)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "FGPOUP_PRT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "프린터 설정"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region " 메인 버튼 처리 "
    ' MyBase Function Key정의
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim sFn As String = "Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.ButtonClick"
        Dim strPrinter As String

        Try
            If txtLeftM.Text = "" Or IsNumeric(txtLeftM.Text) = False Then txtLeftM.Text = "0"
            If txtTopM.Text = "" Or IsNumeric(txtTopM.Text) = False Then txtTopM.Text = "0"
            If txtCopies.Text = "" Or IsNumeric(txtCopies.Text) = False Then txtCopies.Text = "1"
            If cboPort.Text = "" Then cboPort.Text = "COM1"

            If CInt(txtCopies.Text) > 2 Then
                MsgBox("출력매수는 2매를 초과 할수 없습니다", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If cboPrinter.Text = "*** 프린터 미지정 ***" Then
                strPrinter = ""
            Else
                strPrinter = cboPrinter.Text
            End If

            objPrinter.SetInfo(strPrinter, txtLeftM.Text, txtTopM.Text, txtCopies.Text, cboPort.Text)

            Me.Close()
        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#End Region

#Region " Form내부 함수 "

    ' Form초기화
    Private Sub fnFormInitialize()
        Dim sFn As String = "Private Sub fnFormInitialize()"
        Dim scPrinters As PrinterSettings.StringCollection = PrinterSettings.InstalledPrinters

        Try
            fnFormClear(0)

            ' 출력 가능한 바코드 프린터명 표시
            cboPrinter.Items.Add("*** 프린터 미지정 ***")
            For Each sPrinter As String In scPrinters
                Dim pd As New PrintDocument
                If pd.PrinterSettings.IsValid Then cboPrinter.Items.Add(sPrinter)
            Next

            With objPrinter.GetInfo
                If .PRTNM = "" Then
                    cboPrinter.Text = "*** 프린터 미지정 ***"
                Else
                    cboPrinter.Text = .PRTNM
                End If
                txtLeftM.Text = .LEFTM
                txtTopM.Text = .TOPM
                txtCopies.Text = .COPIES
                cboPort.Text = .OUTPORT
            End With

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    ' 화면정리
    Private Sub fnFormClear(ByVal aiPhase As Integer)
        Dim sFn As String = "Private Sub fnFormClear(ByVal aiPhase As Integer)"

        Try
            If InStr("0", aiPhase.ToString, CompareMethod.Text) > 0 Then
                txtLeftM.Text = "0"
                txtTopM.Text = "0"
                txtCopies.Text = "1"
            End If

            If InStr("01", aiPhase.ToString, CompareMethod.Text) > 0 Then
            End If

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

#End Region

#Region " Control Event 처리 "
    Private Sub ntxtCopies_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCopies.GotFocus, txtLeftM.GotFocus, txtTopM.GotFocus
        CType(sender, TextBox).SelectAll()
    End Sub

    Private Sub txtCopies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCopies.KeyDown, txtLeftM.KeyDown, txtTopM.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub ntxtCopies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCopies.KeyPress, txtLeftM.KeyPress, txtTopM.KeyPress
        Fn.sbNumericTextBox(CType(sender, Windows.Forms.TextBox), e)
    End Sub

#End Region

End Class
