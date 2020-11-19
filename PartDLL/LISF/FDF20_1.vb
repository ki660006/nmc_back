Imports System.Drawing
Imports System.IO

Public Class FDF20_1

    Private originSize As Size

    Private bm As Bitmap
    Private img As Image
    Private ms As MemoryStream



    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Dim strRTF As String = Me.rtbSt.get_SelRTF(True)
        Dim intPos As Integer = strRTF.IndexOf("[PAGE SKIP]")
        Dim intCnt As Integer = 1

        Dim strRTF_p As String = strRTF
        Dim strRTF_t As String = ""
        Dim strFont As String = ""
        Dim intfnt1 As Integer = -1

        Do While intPos >= 0

            If intCnt = 1 Then
                Me.rtbSt.set_SelRTF(strRTF_p.Substring(0, intPos) + "}", True)
                Me.rtbSt.print_Data()
            Else
                Me.rtbSt.set_SelRTF("", True)
                strRTF_t = Me.rtbSt.get_SelRTF(True)

                Me.rtbSt.set_SelRTF(strRTF_t.Substring(0, strRTF_t.Length - 3) + strRTF_p.Substring(0, intPos) + "}", True)
                Me.rtbSt.print_Data()
            End If


            strRTF_p = strRTF_p.Substring(intPos + 11)
            intPos = strRTF_p.IndexOf("[PAGE SKIP]")
            intCnt += 1
        Loop

        If intCnt = 1 Then
            Me.rtbSt.print_Data()
        Else
            Me.rtbSt.set_SelRTF("", True)
            strRTF_t = Me.rtbSt.get_SelRTF(True)

            Me.rtbSt.set_SelRTF(strRTF_t.Substring(0, strRTF_t.Length - 3) + strRTF_p, True)
            Me.rtbSt.print_Data()
        End If
        Me.rtbSt.set_SelRTF(strRTF, True)

        'Me.rtbSt.print_Data()

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub FDF20_1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim desktopSize As Size
        'desktopSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize

        'Dim wB As Double = 800 / 1600
        'Dim hB As Double = 1180 / 1200

        'Dim w2B As Double = 744 / 1600
        'Dim h2B As Double = 1130 / 1200


        'If desktopSize.Width <> 1600 Then


        '    Dim dWidth As Double = desktopSize.Width * wB
        '    Dim dHeight As Double = desktopSize.Height * hB

        '    Dim d2Width As Double = desktopSize.Width * w2B
        '    Dim d2Height As Double = desktopSize.Height * h2B

        '    Dim ratioX As Double = dWidth / Me.Size.Width
        '    Dim ratioY As Double = dHeight / Me.Size.Height

        '    Dim ratioX2 As Double = d2Width / Me.rtbPrint.Size.Width
        '    Dim ratioY2 As Double = d2Height / Me.rtbPrint.Size.Height

        '    Me.Size = New Size(Convert.ToInt32(dWidth), Convert.ToInt32(dHeight))
        '    Me.rtbPrint.Size = New Size(Convert.ToInt32(d2Width), Convert.ToInt32(d2Height))

        '    ' Me.Scale(ratioX, ratioY)

        '    'For Each con As Windows.Forms.Control In Me.Controls

        '    '    con.Scale(ratioX2, ratioY2)

        '    'Next
        '    Me.rtbPrint.ZoomFactor = Convert.ToSingle(ratioY2)

        '    Me.btnPrint.Location = New Point(12, 5)

        '    originSize = Me.Size
        'Else
        '    originSize = Me.Size
        'End If



    End Sub

    Private Sub FDF20_1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'sbScaleControls()
    End Sub


    Private Sub FDF20_1_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        'sbScaleControls()
    End Sub

    Public Sub sbScaleControls()

        If originSize.IsEmpty Then
            originSize = Me.Size
        End If

        Dim ratioX As Double = Me.Size.Width / Me.originSize.Width
        Dim ratioY As Double = Me.Size.Height / Me.originSize.Height
        Dim i As Integer = 0

        For Each con As Windows.Forms.Control In Me.Controls

            con.Scale(Convert.ToSingle(ratioX), Convert.ToSingle(ratioY))

        Next

        Dim a As Double = Me.rtbPrint.ZoomFactor
        Me.rtbPrint.ZoomFactor = Convert.ToSingle(ratioY)
        originSize = Me.Size
    End Sub

    'Public Sub sbImgMake()

    '    bm = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)

    '    Try

    '        ms = New MemoryStream

    '        bm.Save(CType(ms, Stream), Imaging.ImageFormat.Bmp)
    '        img = Image.FromStream(CType(ms, Stream))

    '        Dim grfx As Graphics = Me.CreateGraphics.FromImage(img)

    '        Dim strfmt As New StringFormat



    '        strfmt.Alignment = StringAlignment.Center

    '        strfmt.LineAlignment = StringAlignment.Center



    '        grfx.Clear(Color.White)

    '        grfx.DrawString(Me.rtbSt.get_SelRTF, Me.Font, New SolidBrush(Me.ForeColor), _
    '                            grfx.VisibleClipBounds.Width / 2, grfx.VisibleClipBounds.Height / 2, strfmt)

    '        grfx.Dispose()
    '        Me.PictureBox1.Image = img

    '    Catch ex As Exception

    '        MsgBox(ex.ToString())

    '    Finally

    '        ms.Close()

    '    End Try


    'End Sub
End Class