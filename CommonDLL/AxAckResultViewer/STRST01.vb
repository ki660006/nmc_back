Imports COMMON.CommFN
Imports System.Drawing
Imports System.Windows.Forms

Public Class STRST01
    Inherits System.Windows.Forms.Form

    Private Const mc_sFile As String = "File : STRST01.vb, Class : STRST01" & vbTab

    Public BcNo As String = ""
    Public TestCd As String = ""
    Public printIF As Boolean = False
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Public SpecialTestName As String = ""
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument

    Private originSize As Size

    Dim msSpSubExPrg As String
    Dim gBcno As String
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Dim gTclscd As String
    Dim gWi As Integer
    Friend WithEvents btnPrintManual As System.Windows.Forms.Button
    Dim gIF As Boolean = True

    Private msPrtNm As String = ""
    Friend WithEvents cboAddFile As System.Windows.Forms.ComboBox
    Private msDocNm As String = ""
    Private mbAddFileGbn As Boolean = True

    Public Sub Print_Automatically(ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsSpTNm As String, _
                                    ByVal rsPrtNm As String, ByVal rsDocNm As String)
        Dim sFn As String = "Print_Automatically"

        Try
            sbDisplayInit()

            sbDisplay_BcNo_Rst(rsBcNo, rsTClsCd)

            Dim sDir As String = System.Windows.Forms.Application.StartupPath + "\SpecialTestUncompress"
            Dim sFile As String = rsBcNo + "_" + rsTClsCd

            If msSpSubExPrg = "IMG" And IO.File.Exists(sDir + "\" + sFile + "\" + sFile + ".jpg") Then
                PrintDocument1.PrinterSettings.PrinterName = rsPrtNm
                PrintDocument1.DocumentName = rsDocNm
                PrintDocument1.Print()

            Else
                Me.rtbStRst.print_Data(rsPrtNm, rsDocNm)

            End If

        Catch ex As Exception
            Throw New Exception("PrintAutomatically 오류 - " + ex.Message)

        Finally
            Me.Close()

        End Try
    End Sub

    Public Sub Print_Manually(ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsSpTNm As String, _
                                    ByVal rsPrtNm As String, ByVal rsDocNm As String)
        Dim sFn As String = "Print_Manually"

        Try
            BcNo = rsBcNo
            TestCd = rsTClsCd

            Me.btnPrint.Visible = False
            Me.btnPrintManual.Visible = True

            msPrtNm = rsPrtNm
            msDocNm = rsDocNm

            Me.StartPosition = FormStartPosition.CenterScreen
            Me.ShowDialog()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

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
    Friend WithEvents rtbStRst As AxAckRichTextBox.AxAckRichTextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(STRST01))
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.btnPrintManual = New System.Windows.Forms.Button
        Me.cboAddFile = New System.Windows.Forms.ComboBox
        Me.rtbStRst = New AxAckRichTextBox.AxAckRichTextBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.Lavender
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(1, 1)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(99, 26)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "   출력(&P)"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Lavender
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(101, 1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(104, 26)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "   닫기(Esc)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 30)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ShortcutsEnabled = False
        Me.RichTextBox1.Size = New System.Drawing.Size(791, 967)
        Me.RichTextBox1.TabIndex = 184
        Me.RichTextBox1.Text = ""
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(342, 554)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(108, 51)
        Me.PictureBox1.TabIndex = 185
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(342, 611)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(108, 51)
        Me.PictureBox2.TabIndex = 186
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'PrintDocument1
        '
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(656, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(46, 23)
        Me.Button1.TabIndex = 187
        Me.Button1.Text = "확대"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(704, 4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(46, 23)
        Me.Button4.TabIndex = 187
        Me.Button4.Text = "축소"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'btnPrintManual
        '
        Me.btnPrintManual.BackColor = System.Drawing.Color.Lavender
        Me.btnPrintManual.Enabled = False
        Me.btnPrintManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintManual.Image = CType(resources.GetObject("btnPrintManual.Image"), System.Drawing.Image)
        Me.btnPrintManual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrintManual.Location = New System.Drawing.Point(1, 1)
        Me.btnPrintManual.Name = "btnPrintManual"
        Me.btnPrintManual.Size = New System.Drawing.Size(99, 26)
        Me.btnPrintManual.TabIndex = 188
        Me.btnPrintManual.Text = "     출력 Ctrl+P"
        Me.btnPrintManual.UseCompatibleTextRendering = True
        Me.btnPrintManual.UseVisualStyleBackColor = False
        Me.btnPrintManual.Visible = False
        '
        'cboAddFile
        '
        Me.cboAddFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAddFile.FormattingEnabled = True
        Me.cboAddFile.Items.AddRange(New Object() {"[첨부파일]"})
        Me.cboAddFile.Location = New System.Drawing.Point(468, 5)
        Me.cboAddFile.Name = "cboAddFile"
        Me.cboAddFile.Size = New System.Drawing.Size(182, 20)
        Me.cboAddFile.TabIndex = 189
        Me.cboAddFile.Visible = False
        '
        'rtbStRst
        '
        Me.rtbStRst.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rtbStRst.AutoScroll = True
        Me.rtbStRst.Location = New System.Drawing.Point(-1, 1)
        Me.rtbStRst.Name = "rtbStRst"
        Me.rtbStRst.Size = New System.Drawing.Size(792, 989)
        Me.rtbStRst.TabIndex = 0
        Me.rtbStRst.Visible = False
        '
        'STRST01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(792, 1002)
        Me.Controls.Add(Me.cboAddFile)
        Me.Controls.Add(Me.btnPrintManual)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.rtbStRst)
        Me.KeyPreview = True
        Me.Name = "STRST01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "특수검사 보고서"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbDisplay_BcNo_Rst(ByVal rsBcNo As String, ByVal rsTClsCd As String)
        Dim sFn As String = "sbDisplay_BcNo_Rst"

        If rsBcNo.Length = 0 Then Return
        If rsTClsCd.Length = 0 Then Return

        Try
            gBcno = rsBcNo
            gTclscd = rsTClsCd
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            Dim dt As DataTable
            Dim a_dr As DataRow()

            dt = LISAPP.APP_SP.fnGet_Rst_SpTest_MULTI(rsBcNo, rsTClsCd)

            a_dr = dt.Select("rstflg > '0'")
            '한장에 한 이미지 표현하기 위한 For문 
            For i As Integer = 0 To a_dr.Length - 1
                Me.rtbStRst.set_SelRTF(a_dr(i).Item("rstrtf").ToString)
            Next
            ' Me.rtbStRst.set_SelRTF(a_dr(0).Item("rstrtf").ToString)

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.rtbStRst.set_Lock(True)

            Me.rtbStRst.set_ScrollBarV_First()

            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub


    Private Sub sbTextSizeChange()

        Dim desktopSize As Size
        desktopSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize

        Dim wB As Double = 800 / 1600
        Dim hB As Double = 1180 / 1200

        Dim w2B As Double = 744 / 1600
        Dim h2B As Double = 1130 / 1200

        If desktopSize.Width <> 1600 Then


            Dim dWidth As Double = desktopSize.Width * wB
            Dim dHeight As Double = desktopSize.Height * hB

            Dim d2Width As Double = desktopSize.Width * w2B
            Dim d2Height As Double = desktopSize.Height * h2B

            Dim ratioX As Double = dWidth / Me.Size.Width
            Dim ratioY As Double = dHeight / Me.Size.Height

            Dim ratioX2 As Double = d2Width / Me.RichTextBox1.Size.Width
            Dim ratioY2 As Double = d2Height / Me.RichTextBox1.Size.Height

            Me.Size = New Size(CInt(dWidth), CInt(dHeight))
            Me.RichTextBox1.Size = New Size(CInt(d2Width), CInt(d2Height))

            ' Me.Scale(ratioX, ratioY)

            'For Each con As Windows.Forms.Control In Me.Controls

            '    con.Scale(ratioX2, ratioY2)

            'Next
            Me.RichTextBox1.ZoomFactor = CSng(ratioY2)

            originSize = Me.Size
        Else
            originSize = Me.Size
        End If
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try


            'btnPrintManual.Enabled = printIF
            'Me.btnPrint.Enabled = printIF

            Me.Location = New Point(0, 0)

            Me.Text += " ： " + SpecialTestName

            'Me.Height = CType(Me.Owner.Height * 0.9, Integer)

            '< add freety 2008/03/25 : Height 조정, CenterScreen인 경우 화면가운데 오도록
            Me.Height = Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height

            If Me.StartPosition = FormStartPosition.CenterScreen Then
                Me.Left = CInt((Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2)
            End If
            '>

            cboAddFile.Items.Clear()
            cboAddFile.Items.Add("[첨부파일]")
            cboAddFile.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub STRST01_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        Me.rtbStRst.set_ScrollBarV_Last()
    End Sub

    Private Sub STRST01_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.rtbStRst.set_SelRTF("", True)
    End Sub

    '<----- Control Event ----->
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sbDisplayInit()

        sbDisplay_BcNo_Rst(BcNo, TestCd)

        ' 2008-05-19 yjlee mod 
        Me.Button1.PerformClick()
        ' 2008-05-19 yjlee mod 
    End Sub

    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnClose_Click(Me.btnClose, Nothing)
        End If

        If e.Modifiers = Keys.Control And e.KeyCode = Keys.P Then
            Me.btnPrintManual.PerformClick()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Dim sDir As String = System.Windows.Forms.Application.StartupPath & "\SpecialTestUncompress"
        Dim sFile As String = gBcno + "_" + gTclscd
        Dim strRTF As String = Me.rtbStRst.get_SelRTF(True)
        Dim intCnt As Integer = 1

        If msSpSubExPrg = "IMG" And IO.File.Exists(sDir + "\" + sFile + "\" + sFile + ".jpg") Then

            PrintDocument1.Print()

        Else
            Dim intPos As Integer = strRTF.IndexOf("[PAGE SKIP]")

            Dim strRTF_p As String = strRTF
            Dim strRTF_t As String = ""
            Dim strFont As String = ""
            Dim intfnt1 As Integer = -1

            Do While intPos >= 0

                If intCnt = 1 Then
                    Me.rtbStRst.set_SelRTF(strRTF_p.Substring(0, intPos) + "}", True)
                    Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_" + Convert.ToString(intCnt))
                Else
                    Me.rtbStRst.set_SelRTF("", True)
                    strRTF_t = Me.rtbStRst.get_SelRTF(True)

                    Me.rtbStRst.set_SelRTF(strRTF_t.Substring(0, strRTF_t.Length - 3) + strRTF_p.Substring(0, intPos) + "}", True)
                    Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_" + Convert.ToString(intCnt))
                End If


                strRTF_p = strRTF_p.Substring(intPos + 11)
                intPos = strRTF_p.IndexOf("[PAGE SKIP]")
                intCnt += 1
            Loop

            If intCnt = 1 Then
                'Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_" + Convert.ToString(intCnt))
                Me.rtbStRst.print_Data()
            Else
                Me.rtbStRst.set_SelRTF("", True)
                strRTF_t = Me.rtbStRst.get_SelRTF(True)

                Me.rtbStRst.set_SelRTF(strRTF_t.Substring(0, strRTF_t.Length - 3) + strRTF_p, True)
                Me.rtbStRst.print_Data()
                'Me.rtbStRst.set_SelRTF("", True)
                'strRTF_t = Me.rtbStRst.get_SelRTF(True)

                'Me.rtbStRst.set_SelRTF(strRTF_t.Substring(0, strRTF_t.Length - 3) + strRTF_p, True)
                'Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_" + Convert.ToString(intCnt))
            End If





            'Me.rtbStRst.set_SelRTF(strRTF, True)

            Me.Cursor = Windows.Forms.Cursors.Default


            'Me.rtbStRst.set_SelRTF(strRTF, True)

            'If Me.rtbStRst.get_Find("[PAGE SKIP]") > 0 Then
            '    Dim strRTF As String = Me.rtbStRst.get_SelRTF(True)
            '    Dim strRTF_p As String = ""

            '    Dim intCnt As Integer = 1

            '    Dim strRTF_p1 As String = strRTF.Substring(0, intPos)
            '    Dim strRTF_p2 As String = strRTF.Substring(intPos + 11)

            '    Me.rtbStRst.set_SelRTF("", True)
            '    strRTF_p = Me.rtbStRst.get_SelRTF(True)

            '    strRTF_p2 = strRTF_p.Substring(0, strRTF_p.Length - 3) + strRTF_p2

            '    Me.rtbStRst.set_SelRTF(strRTF_p1 + "}", True)
            '    Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_1")

            '    Me.rtbStRst.set_SelRTF(strRTF_p2, True)
            '    Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_2")

            '    Me.rtbStRst.set_SelRTF(strRTF, True)
            'Else
            '    Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_1")
            'End If

        End If

        'mbAddFileGbn = False
        'For intIdx As Integer = 1 To cboAddFile.Items.Count - 1

        '    cboAddFile.SelectedIndex = intIdx
        '    Me.rtbStRst.set_SelRTF("", True)
        '    Me.rtbStRst.set_Image(cboAddFile.Text, True)
        '    Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_" + Convert.ToString(intCnt))

        '    intCnt += 1
        'Next
        'mbAddFileGbn = True

        Me.rtbStRst.set_SelRTF("", True)
        Me.rtbStRst.set_SelRTF(strRTF, True)

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnPrintManual_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintManual.Click
        Dim sFn As String = "btnPrintManual_Click"

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            Dim sDir As String = System.Windows.Forms.Application.StartupPath + "\SpecialTestUncompress"
            Dim sFile As String = BcNo + "_" + TestCd

            If msSpSubExPrg = "IMG" And IO.File.Exists(sDir + "\" + sFile + "\" + sFile + ".jpg") Then
                PrintDocument1.PrinterSettings.PrinterName = msPrtNm
                PrintDocument1.DocumentName = msDocNm
                PrintDocument1.Print()

            Else
                If Me.rtbStRst.get_Find(vbCrLf + Chr(1) + vbCrLf) > 0 Then
                    Dim strRTF As String = ""
                    Dim strRTF_p1 As String = ""
                    Dim strRTF_p2 As String = ""

                    Dim intPos As Integer = Me.rtbStRst.get_Find(vbCrLf + Chr(1) + vbCrLf)

                    strRTF = Me.rtbStRst.get_SelRTF
                    strRTF_p1 = strRTF.Substring(0, intPos - 1) + strRTF.Substring(strRTF.Length - 1)
                    strRTF_p2 = strRTF.Substring(0, 1) + strRTF.Substring(intPos + 3)

                    Me.rtbStRst.set_SelRTF(strRTF_p1, True)
                    Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_1")

                    Me.rtbStRst.set_SelRTF(strRTF_p2, True)
                    Me.rtbStRst.print_Data(msPrtNm, msDocNm + "_2")

                    Me.rtbStRst.set_SelRTF(strRTF, True)

                Else
                    Me.rtbStRst.print_Data(msPrtNm, msDocNm)
                End If
            End If

            Me.Cursor = Windows.Forms.Cursors.Default

            Dim sMsg As String = ""

            sMsg = ""
            sMsg += "특수보고서" + vbCrLf + vbCrLf
            sMsg += msDocNm + vbCrLf + vbCrLf
            sMsg += "이(가) 출력되었습니다!!" + vbCrLf

            MsgBox(sMsg, MsgBoxStyle.Information)

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim sFn As String = "PrintDocument1_PrintPage"
        Try


            With e

                .Graphics.DrawImage(Me.PictureBox1.Image, 0, 0)

            End With
        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.rtbStRst.Visible = True
        Me.RichTextBox1.Visible = False
        If gIF = True Then
            gWi = Me.Width
            gIF = False
        End If

        Me.Width = (Me.rtbStRst.Width)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.rtbStRst.Visible = False
        Me.RichTextBox1.Visible = True

        If gIF = False Then
            Me.Width = gWi
            gWi = Me.Width
        End If


    End Sub

    Private Sub cboAddFile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAddFile.SelectedIndexChanged
        If mbAddFileGbn = False Or cboAddFile.SelectedIndex < 1 Then Return

        Dim frm As New STRST01_S01

        frm.Display_Result(cboAddFile.Text)
        frm.ShowDialog()

        frm = Nothing

    End Sub
End Class
