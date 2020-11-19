'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_COMMON05.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : 공통함수 ( 출력 관련 ) Class                                           */
'/* Design       : 2004-07-14 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar

Namespace CommPrint

#Region " 출력 기본 Class : BaseIF "
    ' 기본 프린터 InterFace
    Public Interface IPrintableObject
        Sub Print()
        Sub PrintPreview()
        Sub RenderPage(ByVal sender As Object, _
            ByVal ev As System.Drawing.Printing.PrintPageEventArgs)
    End Interface

    ' 기본 프린터 Object
    Public Class ObjectPrinter
        Private WithEvents MyDoc As PrintDocument
        Private printObject As IPrintableObject

        Public mbLandscape As Boolean = False
        Public moPRTInfo As PRT_Printer.clsPRTInfo

        Public Sub Print(ByVal obj As IPrintableObject)
            printObject = obj
            MyDoc = New PrintDocument
            If moPRTInfo.PRTNM <> "" Then MyDoc.PrinterSettings.PrinterName = moPRTInfo.PRTNM
            MyDoc.DefaultPageSettings.Landscape = mbLandscape

            MyDoc.Print()
        End Sub

        Public Sub PrintPreview(ByVal obj As IPrintableObject)
            Dim PPdlg As PrintPreviewDialog = New PrintPreviewDialog

            printObject = obj
            MyDoc = New PrintDocument
            If moPRTInfo.PRTNM <> "" Then MyDoc.PrinterSettings.PrinterName = moPRTInfo.PRTNM
            MyDoc.DefaultPageSettings.Landscape = mbLandscape

            PPdlg.Document = MyDoc
            PPdlg.ShowDialog()
        End Sub

        Private Sub PrintPage(ByVal sender As Object, ByVal ev As System.Drawing.Printing.PrintPageEventArgs) Handles MyDoc.PrintPage
            printObject.RenderPage(sender, ev)
        End Sub

    End Class

    ' 좌표정의 Class
    Public Class clsPRT_Design
        Private msngX As New ArrayList
        Private msngY As New ArrayList

        Public Sub Add_PointX(ByVal asngPoint As Single, Optional ByVal asngBasePoint As Single = 0)
            If msngX.Count = 0 Then
                '-- 초기값인 경우
                msngX.Add(asngPoint + asngBasePoint)
            Else
                '-- 추가
                msngX.Add(asngPoint + CSng(msngX.Item(msngX.Count - 1)))
            End If
        End Sub

        Public Sub Add_PointY(ByVal asngPoint As Single, Optional ByVal asngBasePoint As Single = 0)
            If msngY.Count = 0 Then
                '-- 초기값인 경우
                msngY.Add(asngPoint + asngBasePoint)
            Else
                '-- 추가
                msngY.Add(asngPoint + CSng(msngY.Item(msngY.Count - 1)))
            End If
        End Sub

        Public Function RectangleF(ByVal aiX As Integer, ByVal aiY As Integer, _
                                       Optional ByVal aiX2 As Integer = 0, Optional ByVal aiY2 As Integer = 0) As RectangleF

            Dim objRectangleF As New RectangleF

            If aiX2 = 0 Then aiX2 = aiX
            If aiY2 = 0 Then aiY2 = aiY

            If aiX < 1 Or aiX > msngX.Count - 1 Then Exit Function
            If aiX2 < 1 Or aiX2 > msngX.Count - 1 Then Exit Function

            If aiY < 1 Or aiY > msngY.Count - 1 Then Exit Function
            If aiY2 < 1 Or aiY2 > msngY.Count - 1 Then Exit Function

            With objRectangleF
                .X = CSng(msngX.Item(aiX - 1))
                .Y = CSng(msngY.Item(aiY - 1))

                .Width = CSng(msngX.Item(aiX2)) - CSng(msngX.Item(aiX - 1))
                .Height = CSng(msngY.Item(aiY2)) - CSng(msngY.Item(aiY - 1)) + 2
            End With

            RectangleF = objRectangleF

        End Function

        Public Function Rectangle(ByVal aiX As Integer, ByVal aiY As Integer, _
                                      Optional ByVal aiX2 As Integer = 0, Optional ByVal aiY2 As Integer = 0) As Rectangle

            Dim objRectangle As New Rectangle

            If aiX2 = 0 Then aiX2 = aiX
            If aiY2 = 0 Then aiY2 = aiY

            If aiX < 1 Or aiX > msngX.Count - 1 Then Exit Function
            If aiX2 < 1 Or aiX2 > msngX.Count - 1 Then Exit Function

            If aiY < 1 Or aiY > msngY.Count - 1 Then Exit Function
            If aiY2 < 1 Or aiY2 > msngY.Count - 1 Then Exit Function

            With objRectangle
                .X = CInt(msngX.Item(aiX - 1))
                .Y = CInt(msngY.Item(aiY - 1))

                .Width = CInt(msngX.Item(aiX2)) - CInt(msngX.Item(aiX - 1))
                .Height = CInt(msngY.Item(aiY2)) - CInt(msngY.Item(aiY - 1)) + 2
            End With

            Rectangle = objRectangle

        End Function

        Public Function PointF(ByVal aiX As Integer, ByVal aiY As Integer) As PointF
            Dim objPointF As New PointF

            If aiX < 0 Or aiX > msngX.Count - 1 Then Exit Function
            If aiY < 0 Or aiY > msngY.Count - 1 Then Exit Function

            With objPointF
                .X = CSng(msngX.Item(aiX))
                .Y = CSng(msngY.Item(aiY))
            End With

            PointF = objPointF

        End Function

        Public Function Point(ByVal aiX As Integer, ByVal aiY As Integer) As Point
            Dim objPoint As New Point

            If aiX < 0 Or aiX > msngX.Count - 1 Then Exit Function
            If aiY < 0 Or aiY > msngY.Count - 1 Then Exit Function

            With objPoint
                .X = CInt(msngX.Item(aiX))
                .Y = CInt(msngY.Item(aiY))
            End With

            Point = objPoint

        End Function

        Public Sub New()
            MyBase.New()
        End Sub

    End Class

    ' Cm to Point 변경 Class
    Public Class clsPrinterBase
        Protected Const ms1InCm As Single = 2.54
        Protected Const msPoint As Single = 100
        Public Function CmToPoint(ByVal iCm As Single) As Single
            CmToPoint = CType((iCm / ms1InCm) * msPoint, Single)
        End Function
    End Class
#End Region

#Region " 설치된 프린터 가져오기 : PRT_Printer "
    Public Class PRT_Printer
        Private Const sFile As String = "File : CGCOMMON05.vb, Class : COMMON.CommPrint.PRT_Printer" & vbTab

        Private msXmlDir As String = System.Windows.Forms.Application.StartupPath & "\XML"
        Private msXmlFile As String = ""

        Private moPRT_INFO As New clsPRTInfo

        Public Sub New(ByVal asLoadFrm As String)
            MyBase.New()

            msXmlFile = msXmlDir & "\" & asLoadFrm & "_PrinterINFO.XML"

            ' 생성시 프린터정보 읽기
            fnReadPrtInfo()
        End Sub

        ' 프린터정보 읽기( Client 기준 ) 
        Private Sub fnReadPrtInfo()
            Dim sFn As String = "Private Sub fnReadPrtInfo()"

            Try
                If Dir(msXmlDir, FileAttribute.Directory) = "" Then MkDir(msXmlDir)

                If Dir(msXmlFile) > "" Then
                    Dim XMLReader As Xml.XmlTextReader = New Xml.XmlTextReader(msXmlFile)
                    With XMLReader
                        .ReadStartElement("ROOT")
                        moPRT_INFO.PRTNM = .ReadElementString("PRTNM")
                        moPRT_INFO.LEFTM = .ReadElementString("LEFTM")
                        moPRT_INFO.TOPM = .ReadElementString("TOPM")
                        moPRT_INFO.COPIES = .ReadElementString("COPIES")
                        moPRT_INFO.OUTPORT = .ReadElementString("OUTPORT")
                        .ReadEndElement()
                        .Close()
                    End With

                Else
                    Dim pd As New PrintDocument
                    'moPRT_INFO.PRTNM = pd.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName
                    moPRT_INFO.PRTNM = ""
                    moPRT_INFO.LEFTM = "0"
                    moPRT_INFO.TOPM = "0"
                    moPRT_INFO.COPIES = "1"
                    moPRT_INFO.OUTPORT = "LPT1"

                    fnWritePrtInfo()
                End If


            Catch ex As Exception
                Fn.log(sFile & sFn, Err)

            End Try

        End Sub

        Private Sub fnWritePrtInfo()
            Dim sFn As String = "Private Sub fnWritePrtInfo()"

            Try
                If Dir(msXmlDir, FileAttribute.Directory) = "" Then MkDir(msXmlDir)

                Dim XMLWriter As Xml.XmlTextWriter = New Xml.XmlTextWriter(msXmlFile, System.Text.Encoding.GetEncoding("EUC-KR"))
                With XMLWriter
                    .Formatting = Xml.Formatting.Indented
                    .WriteStartDocument(False)
                    .WriteStartElement("ROOT")
                    .WriteElementString("PRTNM", moPRT_INFO.PRTNM)
                    .WriteElementString("LEFTM", moPRT_INFO.LEFTM)
                    .WriteElementString("TOPM", moPRT_INFO.TOPM)
                    .WriteElementString("COPIES", moPRT_INFO.COPIES)
                    .WriteElementString("OUTPORT", moPRT_INFO.OUTPORT)
                    .WriteEndElement()
                    .Close()
                End With

            Catch ex As Exception
                Fn.log(sFile & sFn, Err)

            End Try

        End Sub

        Public ReadOnly Property GetInfo() As clsPRTInfo
            Get
                GetInfo = moPRT_INFO
            End Get
        End Property

        Public Sub SetInfo(ByVal asPRTNM As String, ByVal asLEFTM As String, ByVal asTOPM As String, ByVal asCOPIES As String, Optional ByVal asOUTPORT As String = "")
            Dim sFn As String = "Public Sub SetInfo(ByVal asPRTNM As String, ByVal asLEFTM As String, ByVal asTOPM As String, ByVal asCOPIES As String, [string])"

            Try
                With moPRT_INFO
                    .PRTNM = asPRTNM
                    .LEFTM = asLEFTM
                    .TOPM = asTOPM
                    .COPIES = asCOPIES
                    .OUTPORT = asOUTPORT
                End With

                fnWritePrtInfo()

            Catch ex As Exception
                Fn.log(sFile & sFn, Err)

            End Try

        End Sub

#Region " clsPRTInfo "
        Public Class clsPRTInfo
            Public PRTNM As String = ""
            Public LEFTM As String = "0"
            Public TOPM As String = "0"
            Public COPIES As String = "1"
            Public OUTPORT As String = ""   '-- 2007-10-26 YOOEJ ADD

            Public ReadOnly Property PRTNM_S() As String
                Get
                    Dim strPrintName As String = ""
                    Dim iIdx As Integer

                    iIdx = InStr(3, PRTNM, "\", CompareMethod.Text)
                    If iIdx > 0 Then
                        strPrintName = PRTNM.Substring(iIdx)
                    Else
                        strPrintName = PRTNM
                    End If

                    PRTNM_S = strPrintName
                End Get
            End Property

            Public Sub New()
                MyBase.new()
            End Sub
        End Class
#End Region

    End Class

#End Region

End Namespace


