Imports System.Drawing

Public Class RSTCHART03
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
    Public Event ChangeDblClick(ByVal chxData As AxTeeChart.AxTChart)

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

        With chxData
            .AutoRepaint = False
            .ClearChart()
            .Aspect.View3D = False
            .Scroll.Enable = TeeChart.EChartScroll.pmBoth
            .Zoom.Enable = False
            .Panel.Color = 16777215
            .Panel.BorderStyle = TeeChart.EBorderStyle.bsNone
            .Panel.MarginLeft = 2
            .Panel.MarginRight = 2
            .Panel.MarginTop = 2
            .Panel.MarginBottom = 2
            .AutoRepaint = True
            .Repaint()
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
                sRefL = sBuf(0)
                sRefH = sBuf(1)

                If Not IsNumeric(sRefL) Then sRefL = ""
                If Not IsNumeric(sRefH) Then sRefH = ""

            ElseIf msRefTxt.IndexOf("<=") > 0 Then
                iMaxRow = 2
                sRefH = msRefTxt.Substring(2)

                If Not IsNumeric(sRefH) Then sRefH = ""

            ElseIf msRefTxt.IndexOf(">=") > 0 Then
                iMaxRow = 2
                sRefL = msRefTxt.Substring(2)

                If Not IsNumeric(sRefL) Then sRefH = ""

            ElseIf msRefTxt.IndexOf("<") > 0 Then
                iMaxRow = 2
                sRefH = msRefTxt.Substring(1)

                If Not IsNumeric(sRefH) Then sRefH = ""
            ElseIf msRefTxt.IndexOf(">") > 0 Then
                iMaxRow = 2
                sRefL = msRefTxt.Substring(1)

                If Not IsNumeric(sRefL) Then sRefH = ""
            Else
                iMaxRow = 1
            End If

            Dim dbMaxValue As Double = 0
            Dim dbMinValue As Double = 0

            If sRefH <> "" Then
                dbMaxValue = CDbl(sRefH)
            End If

            If sRefH <> "" Then
                dbMinValue = CDbl(sRefL)
            End If

            With chxData
                .RemoveAllSeries()

                .Header.Text.Clear()
                .Header.Text.Add(rsExmNm.Replace(".", ""))
                .Header.Font.Size = 16
                .Header.Font.Name = "굴림체"
                .Header.Font.Bold = True
                .Header.Font.Color = 0

                '.Legend.ResizeChart = True
                '.Legend.Alignment = TeeChart.ELegendAlignment.laBottom
                '.Legend.CheckBoxes = False
                .Legend.HorizMargin = 10
                .Legend.VertMargin = 10
                '.Legend.Frame.Style = TeeChart.EChartPenStyle.psSmallDots
                '.Legend.Symbol.Width = 10
                '.Legend.Symbol.WidthUnits = TeeChart.ELegendSymbolSize.lcsPixels
                '.Legend.Symbol.Continuous = False
                '.Legend.ShadowSize = 0
                '.Legend.TextStyle = TeeChart.ELegendTextStyle.ltsValue
                '.Legend.Font.Name = "굴림체"
                '.Legend.Font.Size = 9.5
                '.Legend.FontSeriesColor = True
                .Legend.Visible = False

                .Axis.Left.Labels.RoundFirstLabel = True
                .Axis.Right.Labels.RoundFirstLabel = True
                '.Axis.Left.StartPosition = 2
                '.Axis.Right.StartPosition = 2

                .AddSeries(TeeChart.ESeriesClass.scFastLine)
                .Series(0).Title = "결과값"
                .Series(0).Color = 0
                .Series(0).ShowInLegend = True
                .Series(0).Marks.Visible = True
                .Series(0).Marks.Style = TeeChart.EMarkStyle.smsXValue

                '.Series(0).Marks.Transparent = True

                For ix As Integer = 0 To alList.Count - 1
                    Dim dbRst As Double = 0
                    Dim sRst As String = CType(alList.Item(ix), AxAckResultViewer.ChartInfo).sRstVal.Replace("H", "").Replace("L", "").Replace("D", "").Replace("P", "").Trim
                    Dim sDate As String = CType(alList.Item(ix), AxAckResultViewer.ChartInfo).sRstDte

                    If IsNumeric(sRst) Then dbRst = Convert.ToDouble(sRst)

                    .Series(0).AddNullXY(ix + 1, dbRst, sDate)
                    .Series(0).PointValue(ix + 1) = dbRst

                    If dbMaxValue < dbRst Then dbMaxValue = dbRst
                    If dbMinValue > dbRst Then dbMinValue = dbRst

                Next

                iLine += 1

                If sRefH <> "" Then
                    .AddSeries(TeeChart.ESeriesClass.scFastLine)
                    .Series(iLine).Color = 255 '-- red
                    .Series(iLine).Title = "참고치 상한"

                    .Series(iLine).AddNullXY(1, Convert.ToDouble(sRefH), sRefH)
                    .Series(iLine).AddNullXY(alList.Count, Convert.ToDouble(sRefH), sRefH)

                    iLine += 1
                End If


                If sRefL <> "" Then
                    .AddSeries(TeeChart.ESeriesClass.scFastLine)
                    .Series(iLine).Color = 16711680 '-- blue

                    .Series(iLine).Title = "참고치 하한"

                    .Series(iLine).AddNullXY(1, Convert.ToDouble(sRefL), sRefL)
                    .Series(iLine).AddNullXY(alList.Count, Convert.ToDouble(sRefL), sRefL)

                End If

            End With


        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)
        End Try

    End Sub

    Private Sub RSTCHART03_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        RaiseEvent ChangeDblClick(chxData)
    End Sub

    Private Sub chxData_OnClickSeries(ByVal sender As Object, ByVal e As AxTeeChart.ITChartEvents_OnClickSeriesEvent) Handles chxData.OnClickSeries

        'MsgBox(chxData.Series(e.seriesIndex).PointValue(e.valueIndex))

    End Sub
End Class
