Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports System.printing

Imports COMMON.CommFN

Public Class AxAckRichTextBox
    Inherits System.Windows.Forms.UserControl

    Private Const WM_PASTE As Integer = &H302
    Private Const WM_VSCROLL As Integer = &H115

    Private Const SB_VERT As Integer = 1

    Private localPrintServer As LocalPrintServer
    Private Timer1 As New Windows.Forms.Timer
    Dim defaultPrintQueue As PrintQueue

    Private Const SB_LINEUP As Integer = 0
    Private Const SB_LINEDOWN As Integer = 1
    Friend WithEvents rtbTmp As System.Windows.Forms.RichTextBox
    Friend WithEvents tbbtnPageGbn As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnCmd As System.Windows.Forms.ToolBarButton

    Private Structure DOCINFO
        Public cbSize As Integer
        Public lpszDocName As String
        Public lpszOutput As String
    End Structure

    'Private Declare Function SendMessage Lib "USER32" Alias "SendMessageA" (ByVal hWnd As Long, ByVal wMsg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
    Private Declare Auto Function SendMessage Lib "user32" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr

    Private Declare Function OpenPrinter Lib "winspool.drv" Alias "OpenPrinterA" (ByVal pPrinterName As String, ByRef phPrinter As Integer, ByVal pDefault As Integer) As Integer
    Private Declare Function ClosePrinter Lib "winspool.drv" (ByVal hPrinter As Integer) As Integer

    Private Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As Integer, ByVal lpInitData As Integer) As Integer
    Private Declare Auto Function GetDC Lib "user32" Alias "GetDC" (ByVal hwnd As IntPtr) As IntPtr
    Private Declare Function DeleteDC Lib "gdi32" (ByVal hdc As Integer) As Integer

    Private Declare Function StartDoc Lib "gdi32" Alias "StartDocA" (ByVal hdc As Integer, ByRef lpdi As DOCINFO) As Integer
    Private Declare Function SetScrollPos Lib "user32" (ByVal hwnd As Integer, ByVal nBar As Integer, ByVal nPos As Integer, ByVal bRedraw As Integer) As Integer
    Private Declare Function GetScrollPos Lib "user32" (ByVal hwnd As Integer, ByVal nBar As Integer) As Integer

    Private miSkip As Integer
    Private miPageGbn As Integer = 0
    Private miPageCount As Integer = 1
    Private miPageMaxCount As Integer = 0

    Public msBcNo As String = ""
    Friend WithEvents picBuf As System.Windows.Forms.PictureBox
    Friend WithEvents txtLength As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rtb1 As RichTextBoxPrint.RichTextBoxPrint.RichTextBoxPrint
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Private moForm As Windows.Forms.Form


    Public Function get_LinesLength() As Integer
        rtbTmp.Rtf = rtb1.Rtf
        get_LinesLength = rtbTmp.Lines.Length
    End Function

    Public Function get_LinesRTF(ByVal riValue As Integer) As String
        rtbTmp.Rtf = rtb1.Rtf
        get_LinesRTF = rtbTmp.Lines(riValue)
    End Function

    Public Function get_LinesText(ByVal riValue As Integer) As String
        rtbTmp.Text = rtb1.Text
        get_LinesText = rtbTmp.Lines(riValue)
    End Function

    Public Function get_Find(ByVal rsValue As String) As Integer

        get_Find = Me.rtb1.Find(rsValue)

    End Function

    Public Function get_SelText() As String
        Return Me.rtb1.SelectedText
    End Function

    Public Function get_SelText(ByVal rbAll As Boolean) As String
        If rbAll Then
            Me.rtb1.SelectionStart = 0

            Return Me.rtb1.Text
        Else
            Return Me.rtb1.SelectedText
        End If
    End Function

    Public Function get_SelRTF() As String
        Return Me.rtb1.SelectedRtf
    End Function

    Public Function get_SelRTF(ByVal rbAll As Boolean) As String
        If rbAll Then
            Me.rtb1.SelectionStart = 0

            Return Me.rtb1.Rtf
        Else
            Return Me.rtb1.SelectedRtf
        End If
    End Function

    Public Sub set_SelStart(ByVal riValue As Integer)
        Me.rtb1.SelectionStart = riValue
    End Sub

    Public Sub set_Select()
        Me.rtb1.Select()
    End Sub

    Public Sub set_ScrollBarV_First()
        'Dim iPos As Integer = 0

        'Do
        '    iPos = GetScrollPos(Me.rtb1.Hwnd, SB_VERT)

        '    If iPos = 0 Then
        '        SetScrollPos(Me.rtb1.Hwnd, SB_VERT, iPos, CInt(True))

        '        Exit Do
        '    End If

        '    SetScrollPos(Me.rtb1.Hwnd, SB_VERT, iPos - 1, CInt(False))
        '    SendMessage(Me.rtb1.Handle, WM_VSCROLL, SB_LINEUP, vbNull)
        'Loop Until iPos = 0
    End Sub

    Public Sub set_ScrollBarV_Last()
        'Dim iPosBef As Integer = 0
        'Dim iPosAft As Integer = 0

        'Do
        '    iPosBef = GetScrollPos(Me.rtb1.Hwnd, SB_VERT)

        '    SetScrollPos(Me.rtb1.Hwnd, SB_VERT, iPosBef + 1, CInt(False))
        '    SendMessage(Me.rtb1.Handle, WM_VSCROLL, SB_LINEDOWN, vbNull)

        '    iPosAft = GetScrollPos(Me.rtb1.Hwnd, SB_VERT)

        '    If iPosBef = iPosAft Then
        '        SetScrollPos(Me.rtb1.Hwnd, SB_VERT, iPosAft, CInt(True))

        '        Exit Do
        '    End If
        'Loop Until iPosBef = iPosAft
    End Sub

    '<< JJH 닫을때 페이지총수 리셋
    Private Sub PrintPreviewDialog1_Clodse(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintPreviewDialog1.FormClosed
        miPageMaxCount = 0
    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        miPageGbn = 0
        miPageCount = 1
    End Sub

    '<< JJH 페이지총수
    Private Sub PrintDocument1_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint
        miPageMaxCount = miPageCount
    End Sub

    Private Sub sbPrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ' Print the content of the RichTextBox. Store the last character printed.
        miPageGbn = Me.rtb1.Print(miPageGbn, Me.rtb1.TextLength, e)

        ' Look for more pages
        If miPageGbn < Me.rtb1.TextLength Then
            e.HasMorePages = True '다음페이지추가

            '<< JJH miPageMaxCount = 페이지총수
            '       PrintDocument1.PrintPage 이벤트 진행후 PrintDocument1.EndPrint 이벤트에서 총페이지수 miPageMaxCount에 담아줌
            '       miPageMaxCount 가 0이 아닐때(0=인새미리기보기 상태) 하단에 현재페이지수/총페이지수 출력
            'If miPageMaxCount <> 0 Then e.Graphics.DrawString(miPageCount.ToString + " / " + miPageMaxCount.ToString, New Drawing.Font("굴림체", 10, FontStyle.Regular), Brushes.Black, 360, 1130)
            If miPageMaxCount <> 0 Then e.Graphics.DrawString(miPageCount.ToString + " / " + miPageMaxCount.ToString, New Drawing.Font("굴림체", 10, FontStyle.Regular), Brushes.Black, 360, 1110) '20201029 jhs
            miPageCount += 1 '현재페이지수
        Else

            '<< JJH miPageMaxCount = 페이지총수
            '       PrintDocument1.PrintPage 이벤트 진행후 PrintDocument1.EndPrint 이벤트에서 총페이지수 miPageMaxCount에 담아줌
            '       miPageMaxCount 가 0이 아닐때(0=인새미리기보기 상태) 하단에 현재페이지수/총페이지수 출력
            'If miPageMaxCount <> 0 Then e.Graphics.DrawString(miPageCount.ToString + " / " + miPageMaxCount.ToString, New Drawing.Font("굴림체", 10, FontStyle.Regular), Brushes.Black, 360, 1130)
            If miPageMaxCount <> 0 Then e.Graphics.DrawString(miPageCount.ToString + " / " + miPageMaxCount.ToString, New Drawing.Font("굴림체", 10, FontStyle.Regular), Brushes.Black, 360, 1110) '20201029 jhs
            e.HasMorePages = False
        End If
    End Sub


    Public Function print_image(ByVal rsFileName As String, ByVal rsPrintNm As String) As Boolean

        Try
            PageSetupDialog1.PageSettings.Margins.Top = 0
            PageSetupDialog1.PageSettings.Margins.Left = 10
            PageSetupDialog1.PageSettings.Margins.Right = 0
            PageSetupDialog1.PageSettings.Margins.Bottom = 50

            Try
                Dim sPrintNm_Cur As String = PrintDialog1.PrinterSettings.PrinterName

                'defaultPrintQueue = localPrintServer.GetDefaultPrintQueue()
                defaultPrintQueue = localPrintServer.GetPrintQueue(rsPrintNm)
                Dim jobs As PrintJobInfoCollection = defaultPrintQueue.GetPrintJobInfoCollection


                For Each job As PrintSystemJobInfo In jobs
                    job.Refresh()
                    job.Cancel()
                    System.Threading.Thread.Sleep(2000)
                    If job.IsInError = True Then
                        MsgBox("프린터 출력 오류 - 다시 인증해주시기 바랍니다.", MsgBoxStyle.Information)
                        job.Cancel()
                        Return False
                    End If

                    If job.IsDeleted = False Then
                        MsgBox("프린터 출력 오류 - 다시 인증해주시기 바랍니다.", MsgBoxStyle.Information)
                        job.Cancel()
                        Return False
                    End If

                    If job.IsPrinting = True Then
                        MsgBox("프린터 출력 오류 - 다시 인증해주시기 바랍니다.", MsgBoxStyle.Information)
                        job.Cancel()
                        Return False
                    End If

                    If job.IsSpooling = True Then
                        MsgBox("프린터 출력 오류 - 다시 인증해주시기 바랍니다.", MsgBoxStyle.Information)
                        job.Cancel()
                        Return False
                    End If
                Next

                Process.Start("C:\ACK\AIS\ImageServerInAIBorker2005.exe", rsFileName)
                System.Threading.Thread.Sleep(5000)

                PrintDialog1.PrinterSettings.PrinterName = rsPrintNm

                PrintDocument1.Print()

                'Process.Start("C:\ACK\AIS\ImageServerInAIBorker2005.exe", rsFileName)


                PrintDialog1.PrinterSettings.PrinterName = sPrintNm_Cur

                Return True

            Catch ex As Exception
                PrintPreviewDialog1.ShowDialog()
            End Try

        Catch ex As Exception
            MsgBox("출력 오류 - " + ex.Message, MsgBoxStyle.Information)
            Return False

        End Try

    End Function

    Public Sub print_data()

        Try
            PageSetupDialog1.PageSettings.Margins.Top = 10
            PageSetupDialog1.PageSettings.Margins.Left = 10
            PageSetupDialog1.PageSettings.Margins.Right = 0
            PageSetupDialog1.PageSettings.Margins.Bottom = 50 '20130802 정선영 수정



            PrintPreviewDialog1.ShowDialog()

            'If PrintDialog1.ShowDialog() = DialogResult.OK Then
            '    prtDocument.Print()
            'End If


        Catch ex As Exception
            MsgBox("출력 오류 - " + ex.Message, MsgBoxStyle.Information)
            Return
        End Try

    End Sub

    Public Sub print_data(ByVal rsPrinterNm As String)


        Try
            PageSetupDialog1.PageSettings.Margins.Top = 10
            PageSetupDialog1.PageSettings.Margins.Left = 10 '기존
            PageSetupDialog1.PageSettings.Margins.Right = 0
            PageSetupDialog1.PageSettings.Margins.Bottom = 10

            Try
                PrintDialog1.PrinterSettings.PrinterName = rsPrinterNm
                PrintDocument1.Print()
            Catch ex As Exception
                PrintPreviewDialog1.ShowDialog()
            End Try

        Catch ex As Exception
            MsgBox("출력 오류 - " + ex.Message, MsgBoxStyle.Information)
            Return

        End Try
    End Sub

    '> add freety 2008/03/25 : 프린터명, 문서이름 지정
    Public Sub print_Data(ByVal rsPrtNm As String, ByVal rsDocNm As String)

        Try
            PageSetupDialog1.PageSettings.Margins.Top = 10
            PageSetupDialog1.PageSettings.Margins.Left = 10
            PageSetupDialog1.PageSettings.Margins.Right = 0
            PageSetupDialog1.PageSettings.Margins.Bottom = 10

            PrintDialog1.Document.DocumentName = rsDocNm

            Try

                PrintDialog1.PrinterSettings.PrinterName = rsPrtNm


                PrintDocument1.Print()
            Catch ex As Exception
                PrintPreviewDialog1.ShowDialog()
            End Try

        Catch ex As Exception
            MsgBox("출력 오류 - " + ex.Message, MsgBoxStyle.Information)
            Return

        End Try
    End Sub

    Public Sub set_Change_ButtonState_Image(ByVal rbEnable As Boolean)
        Me.tbbtnImage.Enabled = rbEnable
    End Sub

    Public Sub set_DbField_Value(ByVal rsStart As String, ByVal rsField As String, ByVal rsEnd As String, ByVal rsValue As String)
        Dim iStartIndex As Integer = 0

        Do
            If Me.rtb1.Text.Length < iStartIndex Then Exit Do

            iStartIndex = Me.rtb1.Text.IndexOf(rsStart + rsField, iStartIndex)

            If iStartIndex < 0 Then
                Exit Do
            End If

            Dim iEndIndex As Integer = Me.rtb1.Text.IndexOf(rsEnd, iStartIndex)

            If iEndIndex < 0 Or iStartIndex >= iEndIndex Then
                Exit Do
            End If

            Me.rtb1.SelectionStart = iStartIndex + 2
            Me.rtb1.SelectionLength = 4

            Dim sFieldLen As String = fnParse_RealLen(Me.rtb1.SelectedText)

            If rsField.Substring(0, 1) = "Z" Then
                sFieldLen = "2000"
            ElseIf rsField.Substring(0, 1) = "V" Then
                sFieldLen = "2000"
            ElseIf rsField.Substring(0, 1) = "Y" Then
                sFieldLen = "2000"
            Else
                If IsNumeric(sFieldLen) = False Then Exit Do
            End If
            'If IsNumeric(sFieldLen) = False Then Exit Do

            Me.rtb1.SelectionStart = iStartIndex
            Me.rtb1.SelectionLength = iEndIndex - iStartIndex + 1


            Me.rtb1.SelectedText = fnHan_PadRight(fnHan_Substring(rsValue, 0, Convert.ToInt32(sFieldLen)), iEndIndex - iStartIndex + 1)

            Me.rtb1.SelectionStart = 0

            iStartIndex = iEndIndex
        Loop
    End Sub

    Public Sub set_Image(ByVal rsFileNm As String, ByVal rbAlignOrigin As Boolean)
        Dim bmpBuf As Bitmap = New Bitmap(rsFileNm)

        Me.pic1.Image = CType(bmpBuf, Image)

        Clipboard.Clear()
        Clipboard.SetImage(Me.pic1.Image)

        Me.rtb1.Focus()

        SendMessage(Me.rtb1.Handle, WM_PASTE, 0, 0)

        If rbAlignOrigin = False Then
            Me.rtb1.SelectionAlignment = HorizontalAlignment.Center
        End If

        Me.pic1.Image.Dispose() : Me.pic1.Image = Nothing

    End Sub

    Public Sub set_Image(ByVal r_img As Drawing.Image, ByVal rbAlignOrigin As Boolean)
        If r_img Is Nothing Then Return

        Me.pic1.Image = r_img

        Clipboard.Clear()
        Clipboard.SetImage(Me.pic1.Image)

        Me.rtb1.Focus()

        SendMessage(Me.rtb1.Handle, WM_PASTE, 0, 0)

        If rbAlignOrigin = False Then
            Me.rtb1.SelectionAlignment = HorizontalAlignment.Center
        End If

        Me.pic1.Image.Dispose() : Me.pic1.Image = Nothing

    End Sub

    Public Sub set_Image(ByVal rsFileNm As String, ByVal riAlign As Integer)
        Dim bmpBuf As Bitmap = New Bitmap(rsFileNm)

        Me.pic1.Image = CType(bmpBuf, Image)

        Clipboard.Clear()
        Clipboard.SetDataObject(Me.pic1.Image)

        Me.rtb1.Focus()

        SendMessage(Me.rtb1.Handle, WM_PASTE, 0, 0)

        Select Case riAlign
            Case 0
                Me.rtb1.SelectionAlignment = HorizontalAlignment.Left
            Case 1
                Me.rtb1.SelectionAlignment = HorizontalAlignment.Right
            Case Else
                Me.rtb1.SelectionAlignment = HorizontalAlignment.Center

        End Select

        Me.pic1.Image.Dispose() : Me.pic1.Image = Nothing
    End Sub

    Public Sub set_Image(ByVal r_img As Drawing.Image, ByVal riAlign As Integer)
        Me.pic1.Image = r_img

        Clipboard.Clear()
        Clipboard.SetDataObject(Me.pic1.Image)

        Me.rtb1.Focus()

        SendMessage(Me.rtb1.Handle, WM_PASTE, 0, 0)

        Select Case riAlign
            Case 0
                Me.rtb1.SelectionAlignment = HorizontalAlignment.Left
            Case 1
                Me.rtb1.SelectionAlignment = HorizontalAlignment.Right
            Case Else
                Me.rtb1.SelectionAlignment = HorizontalAlignment.Center

        End Select

        Me.pic1.Image.Dispose() : Me.pic1.Image = Nothing
    End Sub

    Public Sub set_Focus()
        Me.rtb1.Focus()
    End Sub

    Public Sub set_Lock(ByVal rbLock As Boolean)
        Me.cboFontNm.Enabled = Not rbLock
        Me.cboFontSize.Enabled = Not rbLock

        Me.tbbtnBold.Enabled = Not rbLock
        Me.tbbtnCenter.Enabled = Not rbLock
        Me.tbbtnColor.Enabled = Not rbLock
        Me.tbbtnImage.Enabled = Not rbLock
        Me.tbbtnItalic.Enabled = Not rbLock
        Me.tbbtnLeft.Enabled = Not rbLock
        Me.tbbtnRight.Enabled = Not rbLock
        Me.tbbtnUnderline.Enabled = Not rbLock
        Me.rtb1.ReadOnly = rbLock

    End Sub

    Public Sub set_SelRTF(ByVal rsRTF As String)
        Me.rtb1.SelectedRtf = rsRTF
    End Sub

    Public Sub set_BcNo(ByVal rsBcNo As String)
        msBcNo = rsBcNo
    End Sub

    Public Sub set_SelRTF(ByVal rsRTF As String, ByVal rbAll As Boolean)
        If rbAll Then
            Me.rtb1.Rtf = rsRTF
        Else
            Me.rtb1.SelectedRtf = rsRTF
        End If
    End Sub

    Public Sub set_SelText(ByVal rsText As String)
        Try
            Me.rtb1.SelectedText = rsText

        Catch ex As Exception
            'MsgBox("set_SelText 오류 - " + ex.Message, MsgBoxStyle.Information, "ACK RTF Warning")

        End Try
    End Sub

    Public Sub set_SelText(ByVal rsText As String, ByVal riAlign As Integer)
        Try
            If Me.rtb1.SelectionLength = 0 Then
                Me.rtb1.SelectionStart = Me.rtb1.Text.Length
            End If

            Me.rtb1.SelectedText = rsText

            Try
                Me.rtb1.SelectionStart = Me.rtb1.Text.Length - rsText.Length
            Catch ex As Exception

            End Try

            Try
                Me.rtb1.SelectionLength = rsText.Length
            Catch ex As Exception
            End Try

            Select Case riAlign
                Case 0
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Left
                Case 1
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Right
                Case Else
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Center

            End Select

            Me.rtb1.SelectionStart = Me.rtb1.Text.Length

        Catch ex As Exception
            'MsgBox("set_SelText 오류 - " + ex.Message, MsgBoxStyle.Information, "ACK RTF Warning")

        End Try
    End Sub

    Public Sub set_SelText(ByVal rsText As String, ByVal riAlign As Integer, ByVal riFontSize As Integer)
        Try
            If Me.rtb1.SelectionLength = 0 Then
                Me.rtb1.SelectionStart = Me.rtb1.Text.Length
            End If

            Me.rtb1.SelectedText = rsText

            Try
                Me.rtb1.SelectionStart = Me.rtb1.Text.Length - rsText.Length
            Catch ex As Exception

            End Try

            Try
                Me.rtb1.SelectionLength = rsText.Length
            Catch ex As Exception
            End Try

            Select Case riAlign
                Case 0
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Left
                Case 1
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Right
                Case Else
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Center

            End Select

            Me.rtb1.SelectionFont = New Font(Me.cboFontNm.Text, riFontSize)

            Me.rtb1.SelectionStart = Me.rtb1.Text.Length

        Catch ex As Exception
            'MsgBox("set_SelText 오류 - " + ex.Message, MsgBoxStyle.Information, "ACK RTF Warning")

        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbDisp_Init()
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
    Friend WithEvents tbbtnBold As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnItalic As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnUnderline As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnSep1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnLeft As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnCenter As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnRight As System.Windows.Forms.ToolBarButton
    Friend WithEvents cboFontNm As System.Windows.Forms.ComboBox
    Friend WithEvents tbbtnColor As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnSep2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbbtnImage As System.Windows.Forms.ToolBarButton
    Friend WithEvents cboFontSize As System.Windows.Forms.ComboBox
    Friend WithEvents pic1 As System.Windows.Forms.PictureBox
    Friend WithEvents imglst1 As System.Windows.Forms.ImageList
    Friend WithEvents tbUpper As System.Windows.Forms.ToolBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxAckRichTextBox))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.tbUpper = New System.Windows.Forms.ToolBar
        Me.tbbtnBold = New System.Windows.Forms.ToolBarButton
        Me.tbbtnItalic = New System.Windows.Forms.ToolBarButton
        Me.tbbtnUnderline = New System.Windows.Forms.ToolBarButton
        Me.tbbtnColor = New System.Windows.Forms.ToolBarButton
        Me.tbbtnSep1 = New System.Windows.Forms.ToolBarButton
        Me.tbbtnLeft = New System.Windows.Forms.ToolBarButton
        Me.tbbtnCenter = New System.Windows.Forms.ToolBarButton
        Me.tbbtnRight = New System.Windows.Forms.ToolBarButton
        Me.tbbtnSep2 = New System.Windows.Forms.ToolBarButton
        Me.tbbtnImage = New System.Windows.Forms.ToolBarButton
        Me.tbbtnPageGbn = New System.Windows.Forms.ToolBarButton
        Me.tbbtnCmd = New System.Windows.Forms.ToolBarButton
        Me.imglst1 = New System.Windows.Forms.ImageList(Me.components)
        Me.cboFontNm = New System.Windows.Forms.ComboBox
        Me.cboFontSize = New System.Windows.Forms.ComboBox
        Me.pic1 = New System.Windows.Forms.PictureBox
        Me.rtbTmp = New System.Windows.Forms.RichTextBox
        Me.picBuf = New System.Windows.Forms.PictureBox
        Me.txtLength = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rtb1 = New RichTextBoxPrint.RichTextBoxPrint.RichTextBoxPrint
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.pic1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBuf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(117, 26)
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(116, 22)
        Me.ToolStripMenuItem2.Text = "111111"
        '
        'tbUpper
        '
        Me.tbUpper.AutoSize = False
        Me.tbUpper.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tbbtnBold, Me.tbbtnItalic, Me.tbbtnUnderline, Me.tbbtnColor, Me.tbbtnSep1, Me.tbbtnLeft, Me.tbbtnCenter, Me.tbbtnRight, Me.tbbtnSep2, Me.tbbtnImage, Me.tbbtnPageGbn, Me.tbbtnCmd})
        Me.tbUpper.ButtonSize = New System.Drawing.Size(24, 22)
        Me.tbUpper.Dock = System.Windows.Forms.DockStyle.None
        Me.tbUpper.DropDownArrows = True
        Me.tbUpper.ImageList = Me.imglst1
        Me.tbUpper.Location = New System.Drawing.Point(212, 0)
        Me.tbUpper.Name = "tbUpper"
        Me.tbUpper.ShowToolTips = True
        Me.tbUpper.Size = New System.Drawing.Size(298, 26)
        Me.tbUpper.TabIndex = 2
        '
        'tbbtnBold
        '
        Me.tbbtnBold.ImageIndex = 0
        Me.tbbtnBold.Name = "tbbtnBold"
        Me.tbbtnBold.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbbtnBold.Tag = "bold"
        Me.tbbtnBold.ToolTipText = "굵게"
        '
        'tbbtnItalic
        '
        Me.tbbtnItalic.ImageIndex = 1
        Me.tbbtnItalic.Name = "tbbtnItalic"
        Me.tbbtnItalic.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbbtnItalic.Tag = "italic"
        Me.tbbtnItalic.ToolTipText = "기울임꼴"
        '
        'tbbtnUnderline
        '
        Me.tbbtnUnderline.ImageIndex = 2
        Me.tbbtnUnderline.Name = "tbbtnUnderline"
        Me.tbbtnUnderline.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbbtnUnderline.Tag = "underline"
        Me.tbbtnUnderline.ToolTipText = "밑줄"
        '
        'tbbtnColor
        '
        Me.tbbtnColor.ImageIndex = 6
        Me.tbbtnColor.Name = "tbbtnColor"
        Me.tbbtnColor.Tag = "color"
        Me.tbbtnColor.ToolTipText = "글색상"
        '
        'tbbtnSep1
        '
        Me.tbbtnSep1.Name = "tbbtnSep1"
        Me.tbbtnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tbbtnLeft
        '
        Me.tbbtnLeft.ImageIndex = 3
        Me.tbbtnLeft.Name = "tbbtnLeft"
        Me.tbbtnLeft.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbbtnLeft.Tag = "left"
        Me.tbbtnLeft.ToolTipText = "왼쪽 맞춤"
        '
        'tbbtnCenter
        '
        Me.tbbtnCenter.ImageIndex = 4
        Me.tbbtnCenter.Name = "tbbtnCenter"
        Me.tbbtnCenter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbbtnCenter.Tag = "center"
        Me.tbbtnCenter.ToolTipText = "가운데 맞춤"
        '
        'tbbtnRight
        '
        Me.tbbtnRight.ImageIndex = 5
        Me.tbbtnRight.Name = "tbbtnRight"
        Me.tbbtnRight.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tbbtnRight.Tag = "right"
        Me.tbbtnRight.ToolTipText = "오른쪽 맞춤"
        '
        'tbbtnSep2
        '
        Me.tbbtnSep2.Name = "tbbtnSep2"
        Me.tbbtnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tbbtnImage
        '
        Me.tbbtnImage.ImageIndex = 8
        Me.tbbtnImage.Name = "tbbtnImage"
        Me.tbbtnImage.Tag = "image"
        Me.tbbtnImage.ToolTipText = "그림"
        '
        'tbbtnPageGbn
        '
        Me.tbbtnPageGbn.ImageIndex = 9
        Me.tbbtnPageGbn.Name = "tbbtnPageGbn"
        Me.tbbtnPageGbn.Tag = "pagegbn"
        '
        'tbbtnCmd
        '
        Me.tbbtnCmd.ImageIndex = 10
        Me.tbbtnCmd.Name = "tbbtnCmd"
        Me.tbbtnCmd.Tag = "cmd"
        '
        'imglst1
        '
        Me.imglst1.ImageStream = CType(resources.GetObject("imglst1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglst1.TransparentColor = System.Drawing.Color.Transparent
        Me.imglst1.Images.SetKeyName(0, "")
        Me.imglst1.Images.SetKeyName(1, "")
        Me.imglst1.Images.SetKeyName(2, "")
        Me.imglst1.Images.SetKeyName(3, "")
        Me.imglst1.Images.SetKeyName(4, "")
        Me.imglst1.Images.SetKeyName(5, "")
        Me.imglst1.Images.SetKeyName(6, "")
        Me.imglst1.Images.SetKeyName(7, "")
        Me.imglst1.Images.SetKeyName(8, "")
        Me.imglst1.Images.SetKeyName(9, "pagegbn.bmp")
        Me.imglst1.Images.SetKeyName(10, "30.bmp")
        '
        'cboFontNm
        '
        Me.cboFontNm.Location = New System.Drawing.Point(1, 3)
        Me.cboFontNm.MaxDropDownItems = 10
        Me.cboFontNm.Name = "cboFontNm"
        Me.cboFontNm.Size = New System.Drawing.Size(136, 20)
        Me.cboFontNm.TabIndex = 0
        '
        'cboFontSize
        '
        Me.cboFontSize.Items.AddRange(New Object() {"8", "9", "10", "11", "12", "13", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72"})
        Me.cboFontSize.Location = New System.Drawing.Point(140, 3)
        Me.cboFontSize.MaxDropDownItems = 10
        Me.cboFontSize.Name = "cboFontSize"
        Me.cboFontSize.Size = New System.Drawing.Size(64, 20)
        Me.cboFontSize.TabIndex = 1
        '
        'pic1
        '
        Me.pic1.Location = New System.Drawing.Point(465, 3)
        Me.pic1.Name = "pic1"
        Me.pic1.Size = New System.Drawing.Size(30, 30)
        Me.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pic1.TabIndex = 3
        Me.pic1.TabStop = False
        Me.pic1.Visible = False
        '
        'rtbTmp
        '
        Me.rtbTmp.Location = New System.Drawing.Point(536, 5)
        Me.rtbTmp.Name = "rtbTmp"
        Me.rtbTmp.Size = New System.Drawing.Size(187, 20)
        Me.rtbTmp.TabIndex = 5
        Me.rtbTmp.Text = ""
        Me.rtbTmp.Visible = False
        '
        'picBuf
        '
        Me.picBuf.Location = New System.Drawing.Point(498, 3)
        Me.picBuf.Name = "picBuf"
        Me.picBuf.Size = New System.Drawing.Size(300, 200)
        Me.picBuf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBuf.TabIndex = 6
        Me.picBuf.TabStop = False
        Me.picBuf.Visible = False
        '
        'txtLength
        '
        Me.txtLength.Location = New System.Drawing.Point(575, 4)
        Me.txtLength.Name = "txtLength"
        Me.txtLength.ReadOnly = True
        Me.txtLength.Size = New System.Drawing.Size(56, 21)
        Me.txtLength.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(522, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 12)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Length"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rtb1)
        Me.Panel1.Location = New System.Drawing.Point(0, 29)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(562, 49)
        Me.Panel1.TabIndex = 10
        '
        'rtb1
        '
        Me.rtb1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtb1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rtb1.Location = New System.Drawing.Point(0, 2)
        Me.rtb1.Margin = New System.Windows.Forms.Padding(0, 10, 0, 10)
        Me.rtb1.Name = "rtb1"
        Me.rtb1.ShowSelectionMargin = True
        Me.rtb1.Size = New System.Drawing.Size(562, 42)
        Me.rtb1.TabIndex = 0
        Me.rtb1.Text = ""
        '
        'PrintDialog1
        '
        Me.PrintDialog1.Document = Me.PrintDocument1
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.PrintDocument1
        '
        'AxAckRichTextBox
        '
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtLength)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.picBuf)
        Me.Controls.Add(Me.rtbTmp)
        Me.Controls.Add(Me.pic1)
        Me.Controls.Add(Me.cboFontSize)
        Me.Controls.Add(Me.cboFontNm)
        Me.Controls.Add(Me.tbUpper)
        Me.Location = New System.Drawing.Point(0, 7)
        Me.Name = "AxAckRichTextBox"
        Me.Size = New System.Drawing.Size(565, 78)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.pic1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBuf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

        localPrintServer = New LocalPrintServer()
        defaultPrintQueue = localPrintServer.GetDefaultPrintQueue

    End Sub

#End Region

    Private Function fnHan_PadRight(ByVal rsBuf As String, ByVal riLen As Integer) As String
        Dim a_btBuf As Byte() = System.Text.Encoding.Default.GetBytes(rsBuf)
        Dim sReturn As String = ""

        If a_btBuf.Length > riLen Then
            sReturn = System.Text.Encoding.Default.GetString(a_btBuf)
        Else
            sReturn = System.Text.Encoding.Default.GetString(a_btBuf) + "".PadRight(riLen - a_btBuf.Length)
        End If

        Return sReturn
    End Function

    Private Function fnHan_Substring(ByVal rsBuf As String, ByVal riIndex As Integer, ByVal riLen As Integer) As String
        Dim a_btBuf As Byte() = System.Text.Encoding.Default.GetBytes(rsBuf)
        Dim sReturn As String = ""

        If a_btBuf.Length > riIndex + riLen Then
            sReturn = System.Text.Encoding.Default.GetString(a_btBuf, riIndex, riLen)
        Else
            sReturn = System.Text.Encoding.Default.GetString(a_btBuf, riIndex, a_btBuf.Length - riIndex)
        End If

        Return sReturn
    End Function

    Private Function fnParse_RealLen(ByVal rsBuf As String) As String
        Dim sReturn As String = ""

        fnParse_RealLen = ""

        For i As Integer = 1 To rsBuf.Length
            If IsNumeric(rsBuf.Substring(i - 1, 1)) Then
                sReturn += rsBuf.Substring(i - 1, 1)
            Else
                Return sReturn
            End If
        Next
    End Function

    Private Sub sbDisp_Init()
        Me.rtb1.Text = ""

        sbDisplay_Font()
    End Sub

    Private Sub sbDisplay_Font()
        Try
            miSkip = 1

            Me.cboFontNm.Items.Clear()

            For i As Integer = 1 To FontFamily.Families.Length
                Me.cboFontNm.Items.Add(FontFamily.Families(i - 1).Name)
            Next

            Me.cboFontNm.Text = Me.rtb1.Font.FontFamily.Name
            Me.cboFontSize.Text = Convert.ToInt32(Me.rtb1.Font.Size).ToString()

        Catch ex As Exception

        Finally
            miSkip = 0

        End Try
    End Sub

    '<-- Control Event -->
    Private Sub cboFontNm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFontNm.SelectedIndexChanged
        If miSkip = 1 Then Return

        Me.rtb1.Focus()
    End Sub

    Private Sub cboFontNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboFontNm.Validating
        If miSkip = 1 Then Return

        If Me.cboFontNm.Items.Contains(Me.cboFontNm.Text) = False Then
            MsgBox("올바른 글꼴이 아닙니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
            Me.cboFontNm.Text = Me.rtb1.SelectionFont.Name.ToString()
            Return
        End If

        Me.rtb1.SelectionFont = New Font(Me.cboFontNm.Text, Convert.ToSingle(Me.cboFontSize.Text))
    End Sub

    Private Sub cboFontSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFontSize.SelectedIndexChanged
        If miSkip = 1 Then Return

        Me.rtb1.Focus()
    End Sub

    Private Sub cboFontSize_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboFontSize.Validating
        If miSkip = 1 Then Return

        If IsNumeric(Me.cboFontSize.Text) = False Then
            MsgBox("올바른 숫자가 아닙니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
            Me.cboFontSize.Text = Me.rtb1.SelectionFont.Size.ToString()
            Return
        End If

        Me.rtb1.SelectionFont = New Font(Me.cboFontNm.Text, Convert.ToSingle(Me.cboFontSize.Text))
    End Sub

    Private Sub rtb1_SelChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb1.SelectionChanged

        'Try
        '    miSkip = 1

        '    If IsDBNull(Me.rtb1.SelectionFont.Name) Then
        '        Me.cboFontNm.Text = ""
        '    Else
        '        Me.cboFontNm.Text = Me.rtb1.SelectionFont.Name.ToString()
        '    End If

        '    If IsDBNull(Me.rtb1.SelectionFont.Size) Then
        '        Me.cboFontSize.Text = ""
        '    Else
        '        Me.cboFontSize.Text = Convert.ToInt32(Me.rtb1.SelectionFont.Size).ToString()
        '    End If

        '    If Me.rtb1.SelectionFont.Bold Then
        '        Me.tbbtnBold.Pushed = True
        '    Else
        '        Me.tbbtnBold.Pushed = False
        '    End If

        '    If Me.rtb1.SelectionFont.Italic Then
        '        Me.tbbtnItalic.Pushed = True
        '    Else
        '        Me.tbbtnItalic.Pushed = False
        '    End If

        '    If Me.rtb1.SelectionFont.Underline Then
        '        Me.tbbtnUnderline.Pushed = True
        '    Else
        '        Me.tbbtnUnderline.Pushed = False
        '    End If

        '    'Left : 0, Right : 1, Center : 2
        '    If IsDBNull(Me.rtb1.SelectionAlignment) Then
        '        Me.tbbtnLeft.Pushed = False
        '        Me.tbbtnRight.Pushed = False
        '        Me.tbbtnCenter.Pushed = False

        '    ElseIf Convert.ToInt32(Me.rtb1.SelectionAlignment) = 0 Then
        '        Me.tbbtnLeft.Pushed = True
        '        Me.tbbtnRight.Pushed = False
        '        Me.tbbtnCenter.Pushed = False

        '    ElseIf Convert.ToInt32(Me.rtb1.SelectionAlignment) = 1 Then
        '        Me.tbbtnLeft.Pushed = False
        '        Me.tbbtnRight.Pushed = True
        '        Me.tbbtnCenter.Pushed = False

        '    ElseIf Convert.ToInt32(Me.rtb1.SelectionAlignment) = 2 Then
        '        Me.tbbtnLeft.Pushed = False
        '        Me.tbbtnRight.Pushed = False
        '        Me.tbbtnCenter.Pushed = True

        '    End If


        '    txtLength.Text = CStr(rtb1.Text.Length)

        'Catch ex As Exception

        'Finally
        '    miSkip = 0

        'End Try
    End Sub

    Private Sub tbUpper_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbUpper.ButtonClick
        Dim oFont As Drawing.Font = Me.rtb1.SelectionFont

        Select Case e.Button.Tag.ToString().ToLower
            Case "bold"
                If e.Button.Pushed Then
                    oFont = New Font(Me.cboFontNm.Text, Convert.ToSingle(Me.cboFontSize.Text), FontStyle.Bold)
                Else
                    oFont = New Font(Me.cboFontNm.Text, Convert.ToSingle(Me.cboFontSize.Text), FontStyle.Regular)
                End If

            Case "italic"
                If e.Button.Pushed Then
                    oFont = New Font(Me.cboFontNm.Text, Convert.ToSingle(Me.cboFontSize.Text), FontStyle.Italic)
                Else
                    oFont = New Font(Me.cboFontNm.Text, Convert.ToSingle(Me.cboFontSize.Text), FontStyle.Regular)
                End If

            Case "underline"
                If e.Button.Pushed Then
                    oFont = New Font(Me.cboFontNm.Text, Convert.ToSingle(Me.cboFontSize.Text), FontStyle.Underline)
                Else
                    oFont = New Font(Me.cboFontNm.Text, Convert.ToSingle(Me.cboFontSize.Text), FontStyle.Regular)
                End If

            Case "left"
                If e.Button.Pushed Then
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Left
                End If

            Case "right"
                If e.Button.Pushed Then
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Right
                End If

            Case "center"
                If e.Button.Pushed Then
                    Me.rtb1.SelectionAlignment = HorizontalAlignment.Center
                End If

            Case "color"
                Dim colordlg As New ColorDialog

                If colordlg.ShowDialog() = DialogResult.OK Then
                    Me.rtb1.SelectionColor = colordlg.Color
                End If


            Case "image"
                Dim filedlg As New OpenFileDialog

                filedlg.Multiselect = False
                filedlg.Title = "그림 파일 불러오기"
                filedlg.Filter = "그림파일(*.bmp;*jpg;*.gif;*.tif)|*.bmp;*.jpg;*.gif;*.tif|모든파일(*.*)|*.*"

                If filedlg.ShowDialog() = DialogResult.OK Then
                    If filedlg.FileName.Length > 0 Then

                        Dim bmpBuf As Bitmap = New Bitmap(filedlg.FileName)
                        Me.picBuf.Image = bmpBuf

                        Me.picBuf.Refresh()

                        Dim imgTot As Drawing.Image = Me.picBuf.Image

                        '그림소스의 너비, 높이
                        Dim iTotalW As Integer = Me.picBuf.Width
                        Dim iTotalH As Integer = Me.picBuf.Height

                        '자르고자하는 영역의 X, Y, 너비, 높이
                        Dim iAreaX As Integer = 0
                        Dim iAreaY As Integer = 0
                        Dim iAreaW As Integer = Convert.ToInt32(Me.picBuf.Width)
                        Dim iAreaH As Integer = Convert.ToInt32(Me.picBuf.Height)

                        Dim bmpArea As Drawing.Bitmap = New Drawing.Bitmap(iAreaW, iAreaH)

                        Me.picBuf.Image = bmpArea

                        Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(Me.picBuf.Image)

                        g.DrawImage(imgTot, -iAreaX, -iAreaY, iTotalW, iTotalH)
                        g.Dispose()

                        set_Lock(False)
                        set_SelRTF("", True)
                        set_Image(Me.picBuf.Image, 2)
                        set_Lock(True)

                        Me.rtb1.Focus()

                        Me.picBuf.Image.Dispose()
                        Me.picBuf.Image = Nothing
                    End If
                End If

            Case "pagegbn"

                'Me.rtb1.SelectedText = vbCrLf + "[PAGE SKIP]" + vbCrLf

            Case "cmd"

                Dim strTclsCd As String = ""
                Dim pntCtlXY As New Point
                Dim pntFrmXY As New Point

                Dim objHelp As New CDHELP.FGCDHELP01
                Dim arlList As New ArrayList

                objHelp.FormText = "소견코드"
                objHelp.TableNm = "lf080m"
                objHelp.Where = "dbo.fn_nvl(cmtgbn, '0') = '0' AND(partcd IN (SELECT f.partcd FROM lr010m r, lf060m f WHERE r.bcno = '" + msBcNo.Replace("-", "") + "' AND r.testcd = f.testcd AND r.spccd = f.spccd AND r.tkdt >= f.usdt AND r.tkdt < f.uedt) OR dbo.fn_nvl(partcd, '0') = '0')"

                objHelp.GroupBy = ""
                objHelp.OrderBy = ""
                objHelp.MaxRows = 15
                objHelp.Distinct = True

                objHelp.AddField("'' CHK", "", CInt(2.5), FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
                objHelp.AddField("CMTPCD", "주코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("CMTSCD", "부코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("CMTCONT", "내용", 60, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

                pntFrmXY = Fn.CtrlLocationXY(Me)
                pntCtlXY = Fn.CtrlLocationXY(tbUpper)

                moForm = New Windows.Forms.Form

                arlList = objHelp.Display_Result(moForm, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + tbUpper.Height + 80)

                Dim strCont As String = ""
                If arlList.Count > 0 Then

                    For intIdx As Integer = 0 To arlList.Count - 1
                        If intIdx <> 0 Then strCont += vbCrLf
                        strCont += arlList.Item(intIdx).ToString.Split("|"c)(2)
                    Next
                End If

                If strCont <> "" Then
                    Me.rtb1.SelectedText = vbCrLf + strCont + vbCrLf
                Else

                End If

        End Select

        Me.rtb1.SelectionFont = oFont

        Me.rtb1_SelChange(Nothing, Nothing)
    End Sub
End Class
