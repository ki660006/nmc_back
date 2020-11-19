Imports System.Drawing

Public Class RSTCHART04
    Public msRegNo As String = ""
    Public msExamCd As String = ""
    Public msExamNm As String = ""
    Public msEndDate As String = ""
    Public msRefTxt As String = ""
    Public msDecimal As String = ""
    Public mbPointLabelVisible As Boolean = False
    Public mbDataGridVisible As Boolean = False
    Public mbMviewer As Boolean = False
    Public mbAxisVisible As Boolean = False
    Public Event ChangeDblClick(ByVal picChart As System.Windows.Forms.PictureBox)

    Private miTop As Integer = 0
    Private miLeft As Integer = 0
    Private miBottom As Integer = 0
    Private miHeight As Integer
    Private miWidth As Integer = 0

    Public Property RegNo() As String
        Get
            Return msRegNo
        End Get
        Set(ByVal Value As String)
            msRegNo = Value
        End Set
    End Property

    Public Property ExamCd() As String
        Get
            Return msExamCd
        End Get
        Set(ByVal Value As String)
            msExamCd = Value
        End Set
    End Property

    Public Property ExamNm() As String
        Get
            Return msExamNm
        End Get
        Set(ByVal Value As String)
            msExamNm = Value
        End Set
    End Property

    Public Property EndDate() As String
        Get
            Return msEndDate
        End Get
        Set(ByVal Value As String)
            msEndDate = Value
        End Set
    End Property

    Public Property RefTxt() As String
        Get
            Return msRefTxt
        End Get
        Set(ByVal Value As String)
            msRefTxt = Value
        End Set
    End Property

    Public Property DataGridVisible() As Boolean
        Get
            Return mbDataGridVisible
        End Get
        Set(ByVal Value As Boolean)
            mbDataGridVisible = Value

            sbDisplay_Change("DATAGRID")
        End Set
    End Property

    Public Property PointLabelVisible() As Boolean
        Get
            Return mbPointLabelVisible
        End Get
        Set(ByVal Value As Boolean)
            mbPointLabelVisible = Value

            sbDisplay_Change("POINT")
        End Set
    End Property

    Public Property Viewer() As Boolean
        Get
            Return mbMviewer
        End Get
        Set(ByVal Value As Boolean)
            mbMviewer = Value

            Me.btnView.Visible = mbMviewer
        End Set
    End Property

    Public Property AxisVisible() As Boolean
        Get
            Return mbAxisVisible
        End Get
        Set(ByVal Value As Boolean)
            mbAxisVisible = Value

            sbDisplay_Change("LABEL")
        End Set
    End Property

    Public Sub Clear()

        With picChart
            Me.picChart.Image = Nothing

            '0) 이미지 및 그래픽 개체 생성
            Dim bmpData As New System.Drawing.Bitmap(picChart.Width, picChart.Height)

            Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(bmpData)

            g.Clear(Drawing.Color.White)

            miTop = 30 : miLeft = 50 : miBottom = 30

            miHeight = picChart.Height - miTop - miBottom
            miWidth = picChart.Width - miLeft * 2

            g.DrawRectangle(New Pen(Color.Black, 2), New Drawing.Rectangle(miLeft - 10, miTop + 5, miWidth + 20, miHeight + 10))

            Me.picChart.Image = bmpData

        End With


    End Sub

    Private Sub sbDisplay_Change(ByVal rsPropertyNm As String)
        'Dim sFn As String = ""

        'Try
        '    With chxData
        '        If .Series.Count > 0 Then
        '            If rsPropertyNm = "POINT" Then
        '                .Series(0).PointLabels.Visible = mbPointLabelVisible
        '            ElseIf rsPropertyNm = "LABEL" Then
        '                .AxisX.Visible = mbAxisVisible
        '            Else
        '                .DataGrid.Visible = mbDataGridVisible
        '            End If
        '            .Refresh()
        '        End If
        '    End With
        'Catch ex As Exception
        '    Throw New Exception(ex.Message + " @" + sFn)
        'End Try

    End Sub

    Public Sub Display_Chart(ByVal raList As ArrayList, ByVal rsExmNm As String)
        Dim sFn As String = "Sub Display_Chart(arrylist, string)"

        Try
            Dim iMaxCol As Integer = raList.Count
            Dim iMaxRow As Integer = 1
            Dim bTkDt As Boolean = False

            Dim sRefL As String = ""
            Dim sRefH As String = ""
            Dim sBuf() As String
            Dim iLine As Integer = 0

            Dim alList As New ArrayList

            alList = raList
            If alList.Count = 0 Then Exit Sub

            If msRefTxt = "" Then
                iMaxRow = 0
            ElseIf msRefTxt.IndexOf("~") > 0 Then
                iMaxRow = 3
                sBuf = msRefTxt.Split("~"c)
                sRefL = sBuf(0).Trim
                sRefH = sBuf(1).Trim

                If Not IsNumeric(sRefL) Then sRefL = ""
                If Not IsNumeric(sRefH) Then sRefH = ""

            ElseIf msRefTxt.IndexOf("<=") > 0 Then
                iMaxRow = 2
                sRefH = msRefTxt.Substring(2).Trim

                If Not IsNumeric(sRefH) Then sRefH = ""

            ElseIf msRefTxt.IndexOf(">=") > 0 Then
                iMaxRow = 2
                sRefL = msRefTxt.Substring(2).Trim

                If Not IsNumeric(sRefL) Then sRefH = ""

            ElseIf msRefTxt.IndexOf("<") > 0 Then
                iMaxRow = 2
                sRefH = msRefTxt.Substring(1).Trim

                If Not IsNumeric(sRefH) Then sRefH = ""
            ElseIf msRefTxt.IndexOf(">") > 0 Then
                iMaxRow = 2
                sRefL = msRefTxt.Substring(1).Trim

                If Not IsNumeric(sRefL) Then sRefH = ""
            Else
                iMaxRow = 1
            End If

            Dim dbMaxValue As Double = 0
            Dim dbMinValue As Double = 0
            Dim iDpint As Integer = 0
            Dim iBaseVal As Integer = 0

            If sRefH <> "" Then
                dbMaxValue = Convert.ToDouble(sRefH)
            End If

            If sRefH <> "" Then
                dbMinValue = Convert.ToDouble(sRefL)
            End If

            For ix As Integer = 0 To alList.Count - 1
                Dim dbRst As Double = 0
                Dim sRst As String = CType(alList.Item(ix), AxAckResultViewer.ChartInfo).sRstVal.Trim

                If sRst.EndsWith(".") Then sRst += "0"

                If sRst.IndexOf(".") >= 0 Then
                    Dim iTmp As Integer = sRst.Substring(sRst.IndexOf(".") + 1).Length
                    If iDpint < iTmp Then iDpint = iTmp
                End If


                If IsNumeric(sRst) Then dbRst = Convert.ToDouble(sRst)

                If dbMaxValue < dbRst Then dbMaxValue = dbRst
                If dbMinValue > dbRst Then dbMinValue = dbRst
            Next

            If dbMinValue < 0 Then iBaseVal = dbMinValue * -1

            If iDpint > 0 Then
                iDpint = Convert.ToInt32("1" + "0".PadLeft(iDpint, "0"c))
            Else
                iDpint = 1
            End If

            dbMinValue = (dbMinValue + iBaseVal) * iDpint
            dbMaxValue = (dbMaxValue + iBaseVal) * iDpint

            Dim sgDotY As Single = (miHeight / (dbMaxValue - dbMinValue)) '* 100
            Dim iDotX As Integer = (miWidth / (alList.Count - 1)) '* 100
            Dim iBottom As Integer = Me.picChart.Height - miBottom

            '0) 이미지 및 그래픽 개체 생성
            Dim bmpData As New System.Drawing.Bitmap(picChart.Width, picChart.Height)

            Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(bmpData)
            Dim sf_c As New Drawing.StringFormat
            Dim sf_r As New Drawing.StringFormat
            Dim pen As New Pen(Color.Black, 2)
            Dim rect As New Drawing.RectangleF


            sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
            sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

            g.Clear(Drawing.Color.White)
            g.DrawRectangle(New Pen(Color.Black, 2), New Drawing.Rectangle(miLeft - 10, miTop, miWidth + 20, miHeight))

            Dim iCnt As Integer = -1
            Dim iRst_Bef As Integer = 0

            For ix As Integer = 0 To alList.Count - 1
                Dim sRst As String = CType(alList.Item(ix), AxAckResultViewer.ChartInfo).sRstVal.Trim
                If IsNumeric(sRst) Then
                    iCnt += 1
                    Dim iPosY As Integer = iBottom - (Convert.ToDouble(sRst) * iDpint - dbMinValue) * sgDotY

                    rect = New Drawing.RectangleF(miLeft + iDotX * iCnt, iPosY, 8, 8)
                    g.DrawString("●", New Font("굴림체", 9, FontStyle.Regular), Drawing.Brushes.Black, rect, sf_c)

                    g.DrawString(sRst, New Font("굴림체", 9, FontStyle.Regular), Brushes.Black, miLeft + iDotX * iCnt, iPosY + 10)

                    If iCnt > 0 Then
                        g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 1), miLeft + iDotX * (iCnt - 1), iRst_Bef, miLeft + iDotX * iCnt, iPosY)
                    End If

                    iRst_Bef = iPosY
                End If
            Next

            If sRefL <> "" Then
                Dim iPosY As Integer = iBottom - Convert.ToInt16(Convert.ToDouble(sRefL * iDpint) - dbMinValue) * sgDotY

                g.DrawLine(New Drawing.Pen(Drawing.Color.Blue, 2), miLeft - 10, iPosY, miLeft + miWidth + 10, iPosY)

                rect = New Drawing.RectangleF(0, iPosY, miLeft - 14, 15)
                g.DrawString(sRefL, New Font("굴림체", 9, FontStyle.Regular), Drawing.Brushes.Black, rect, sf_r)

            End If

            If sRefH <> "" Then
                Dim iPosY As Integer = iBottom - Convert.ToInt16(Convert.ToDouble(sRefH * iDpint) - dbMinValue) * sgDotY

                g.DrawLine(New Drawing.Pen(Drawing.Color.Red, 2), miLeft - 10, iPosY, miLeft + miWidth + 10, iPosY)

                rect = New Drawing.RectangleF(0, iPosY, miLeft - 14, 15)
                g.DrawString(sRefH, New Font("굴림체", 9, FontStyle.Regular), Drawing.Brushes.Black, rect, sf_r)
            End If

            rect = New Drawing.RectangleF(0, 5, Me.picChart.Width, 20)
            g.DrawString(msExamNm, New Font("굴림체", 12, FontStyle.Bold), Drawing.Brushes.Black, rect, sf_c)

            picChart.Image = bmpData
            picChart.Refresh()

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)
        End Try

    End Sub
End Class
