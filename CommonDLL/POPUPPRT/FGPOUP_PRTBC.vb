Imports COMMON.SVar
Imports common.commlogin.login
Imports COMMON.CommFN

Imports System.Windows.Forms
Imports System.Drawing

Public Class FGPOUP_PRTBC
    Inherits System.Windows.Forms.Form
    Private Const sFile As String = "File : POPUPWIN.vb, Class : FGPWIN02" & vbTab

    Public msOwnerFrm As String = ""
    Dim objBCPrinter As PRTAPP.APP_BC.BCPrinter

    Private msXmlDir As String = Application.StartupPath & "\XML"
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTMargin As System.Windows.Forms.TextBox
    Friend WithEvents cboOutPort As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLMargin As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtOutIP As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboBarPrint As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private msXmlFileCheck As String = msXmlDir & "\FGPWIN02_CheckMode.XML"

    Public ReadOnly Property mPrinterName() As String
        Get
            mPrinterName = objBCPrinter.GetInfo.PRTNM
        End Get
    End Property

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New(ByVal rsOwnerFrm As String, Optional ByVal rbInit As Boolean = False)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        If rbInit Then
            Try
                If IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\XML\" + rsOwnerFrm + "_BCPrinterINFO.XML") Then
                    IO.File.Delete(System.Windows.Forms.Application.StartupPath + "\XML\" + rsOwnerFrm + "_BCPrinterINFO.XML")
                End If

            Catch ex As Exception

            End Try
        End If

        objBCPrinter = New PRTAPP.APP_BC.BCPrinter(rsOwnerFrm)

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
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtTMargin = New System.Windows.Forms.TextBox
        Me.cboOutPort = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtLMargin = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtOutIP = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboBarPrint = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(209, 103)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(84, 26)
        Me.btnCancel.TabIndex = 25
        Me.btnCancel.Text = "취  소"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(125, 103)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(84, 26)
        Me.btnOk.TabIndex = 24
        Me.btnOk.Text = "선  택"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(151, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 21)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Top Margin"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTMargin
        '
        Me.txtTMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTMargin.Location = New System.Drawing.Point(240, 70)
        Me.txtTMargin.Name = "txtTMargin"
        Me.txtTMargin.Size = New System.Drawing.Size(52, 21)
        Me.txtTMargin.TabIndex = 22
        '
        'cboOutPort
        '
        Me.cboOutPort.DisplayMember = "AUTO LABELER (외래채혈실)"
        Me.cboOutPort.Items.AddRange(New Object() {" ", "COM1", "COM2", "LPT1", "LPT2"})
        Me.cboOutPort.Location = New System.Drawing.Point(93, 48)
        Me.cboOutPort.Name = "cboOutPort"
        Me.cboOutPort.Size = New System.Drawing.Size(199, 20)
        Me.cboOutPort.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(4, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 21)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Left Margin"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLMargin
        '
        Me.txtLMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLMargin.Location = New System.Drawing.Point(93, 70)
        Me.txtLMargin.Name = "txtLMargin"
        Me.txtLMargin.Size = New System.Drawing.Size(54, 21)
        Me.txtLMargin.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(4, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 20)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "출력위치"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtOutIP
        '
        Me.txtOutIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutIP.Location = New System.Drawing.Point(93, 25)
        Me.txtOutIP.Name = "txtOutIP"
        Me.txtOutIP.Size = New System.Drawing.Size(199, 21)
        Me.txtOutIP.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(4, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 21)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "출력방향(IP)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboBarPrint
        '
        Me.cboBarPrint.DisplayMember = "AUTO LABELER (외래채혈실)"
        Me.cboBarPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBarPrint.Location = New System.Drawing.Point(93, 3)
        Me.cboBarPrint.Name = "cboBarPrint"
        Me.cboBarPrint.Size = New System.Drawing.Size(199, 20)
        Me.cboBarPrint.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(4, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 20)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "바코드 프린터"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGPWIN02
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(296, 136)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtTMargin)
        Me.Controls.Add(Me.cboOutPort)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtLMargin)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtOutIP)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboBarPrint)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGPWIN02"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "바코드프린터 설정"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region " 메인 버튼 처리 "

    Private Sub FGPWIN02_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fnSave_CheckMode()
    End Sub

    ' CheckMode 저장
    Private Sub fnSave_CheckMode()
        Dim sFn As String = ""
        Dim strFullDir As String = msXmlDir
        Dim strFullFile As String = msXmlFileCheck


        Try
            If Dir(strFullDir, FileAttribute.Directory) = "" Then MkDir(strFullDir)

            Dim XMLWriter As Xml.XmlTextWriter = New Xml.XmlTextWriter(strFullFile, System.Text.Encoding.GetEncoding("EUC-KR"))
            With XMLWriter
                .Formatting = Xml.Formatting.Indented
                .WriteStartDocument(False)
                .WriteStartElement("ROOT")
                .WriteElementString("CheckMode", "0")
                .WriteEndElement()
                .Close()
            End With

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' MyBase Function Key정의
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim sFn As String = "Handles btnReg.ButtonClick"

        Try
            With objBCPrinter
                .PrtID = cboBarPrint.SelectedIndex
                .SetOutIP = txtOutIP.Text
                .SetIOPort = cboOutPort.Text
                .SetLeftMargin = txtLMargin.Text
                .SetTopMargin = txtTMargin.Text
                .SetPrtType = ""

                .WritePrtInfo()
            End With

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

        Try
            ' 출력 가능한 바코드 프린터명 표시
            cboBarPrint.Items.Clear()

            With objBCPrinter
                For intCnt As Integer = 0 To .GetCnt - 1
                    cboBarPrint.Items.Add(.GetInfo(intCnt).PRTNM)
                Next
                cboBarPrint.SelectedIndex = .PrtID
                txtOutIP.Text = .GetInfo.OUTIP
                cboOutPort.Text = .GetInfo.IOPORT
                txtLMargin.Text = .GetInfo.LEFTMARGIN
                txtTMargin.Text = .GetInfo.TOPMARGIN
            End With

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

#End Region

#Region " Control Event 처리 "
    Private Sub cboBarPrint_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBarPrint.SelectedIndexChanged
        If objBCPrinter.GetInfo(cboBarPrint.SelectedIndex).SUPPORTIP = "1" Then
            txtOutIP.Enabled = True
            txtOutIP.Text = objBCPrinter.GetInfo(cboBarPrint.SelectedIndex).OUTIP
            cboOutPort.Text = objBCPrinter.GetInfo(cboBarPrint.SelectedIndex).IOPORT
            txtLMargin.Text = objBCPrinter.GetInfo(cboBarPrint.SelectedIndex).LEFTMARGIN
            txtTMargin.Text = objBCPrinter.GetInfo(cboBarPrint.SelectedIndex).TOPMARGIN

            txtOutIP.Focus()
        Else
            txtOutIP.Enabled = False
            txtOutIP.Text = ""
        End If
    End Sub

    Private Sub cboBarPrint_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboBarPrint.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True

            If txtOutIP.Enabled = True Then
                SendKeys.Send("{TAB}")
            Else
                btnOk_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub cboOutPort_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboOutPort.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True
            btnOk_Click(Nothing, Nothing)
        End If
    End Sub

#End Region

    Private Sub FGPWIN02_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sFn As String = ""

        Try
#If DEBUG Then
            cboOutPort.Items.Add("COM6")
#End If

            For Each sPrtNm As String In Printing.PrinterSettings.InstalledPrinters
                cboOutPort.Items.Add(sPrtNm)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboOutPort_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOutPort.SelectedValueChanged
        Me.txtOutIP.Text = ""
    End Sub
End Class
