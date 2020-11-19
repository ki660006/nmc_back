'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGCOMMON_XML.vb                                                        */
'/* PartName     :                                                                        */
'/* Description  : XML 관련                                                               */
'/* Design       : 2003-07-29 Ju Jin Ho                                                   */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System.Windows.Forms
Imports System.IO

Public Class CommXML
    Private Shared Sub getAttribute(ByVal rsLine As String, ByRef rsAttribute As String, ByRef rsValue As String)
        Dim sAttr As String = ""
        Dim iPosS As Integer = 0
        Dim iPosE As Integer = 0

        rsAttribute = ""
        rsValue = ""

        iPosS = InStr(rsLine, "<")
        iPosE = InStr(rsLine, ">")

        If iPosS > 0 And iPosE > 0 Then
            sAttr = Mid(rsLine, iPosS + 1, iPosE - iPosS - 1)
            iPosS = InStr(iPosE, rsLine, "</" & sAttr)
            If iPosS > 0 Then
                rsAttribute = sAttr
                rsValue = Mid(rsLine, iPosE + 1, iPosS - iPosE - 1)
                Debug.WriteLine(rsAttribute)
                Debug.WriteLine(rsValue)
            End If
        End If

    End Sub

    Public Shared Function getOneElementXML(ByVal rsDir As String, ByVal rsFileNm As String, ByVal rsElement As String) As String

        getOneElementXML = ""

        Dim sDir As String = ""

        If rsDir.IndexOf(":") > 0 Then
            sDir = rsDir
        Else
            sDir = Application.StartupPath + rsDir
        End If

        If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

        If Dir(rsFileNm) > "" Then

            Dim sr As StreamReader = New StreamReader(rsFileNm, System.Text.Encoding.GetEncoding("euc-kr"))
            Dim sLine As String = ""
            Dim sAttribute As String = ""
            Dim sValue As String = ""

            Do
                sLine = sr.ReadLine()

                getAttribute(sLine, sAttribute, sValue)

                Select Case sAttribute
                    Case rsElement
                        getOneElementXML = sValue

                End Select

                'Debug.WriteLine(sLine)
            Loop Until sLine Is Nothing
            sr.Close()
        End If

    End Function

    Public Shared Sub setOneElementXML(ByVal rsDir As String, ByVal rsFileNm As String, ByVal rsElement As String, ByVal rsValue As String)

        Dim sDir As String = ""

        If rsDir.IndexOf(":") > 0 Then
            sDir = rsDir
        Else
            sDir = Application.StartupPath + rsDir
        End If
        '>

        If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

        Dim xmlWriter As Xml.XmlTextWriter = Nothing

        xmlWriter = New Xml.XmlTextWriter(rsFileNm, System.Text.Encoding.GetEncoding("euc-kr"))
        xmlWriter.Formatting = Xml.Formatting.Indented
        xmlWriter.WriteStartDocument(False)
        xmlWriter.WriteStartElement("ROOT")
        xmlWriter.WriteElementString(rsElement, rsValue)
        xmlWriter.WriteEndElement()

        xmlWriter.Close()
    End Sub


End Class
