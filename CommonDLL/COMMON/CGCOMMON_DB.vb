'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGCOMMON_DB.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : 공통함수 ( Server 관련 ) Class                                         */
'/* Design       : 2003-11-12 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports System.Windows.forms

Imports COMMON.CommFN

Namespace CommDb
    Public Class STU_CONNSTR
        Public USEDP As String = ""
        Public PROVIDER As String = ""
        Public CATEGORY As String = ""
        Public DATASOURCE As String = ""
        Public USERID As String = ""
        Public PASSWORD As String = ""
        Public DESCRIPTION As String = ""

        Public Sub New()
            MyBase.NEW()
        End Sub
    End Class

    Public Class Info
        Private Const sFile As String = "File : CGCOMMON_DB.vb, Class : CommDb.Info" & vbTab

        Private msFullDir As String = Application.StartupPath + "\XML"

        ' DB Connection String 설정
        Public Function SetConnStr(ByVal r_o_stu As STU_CONNSTR) As Boolean
            Dim sFn As String = "Private Function SetConnStr(ByVal aoConnStr As clsCONN_STR) As Boolean"
            Dim strFullFile As String = msFullDir & "\DBSERVER.XML"

            Try
                SetConnStr = False

                If Dir(msFullDir, FileAttribute.Directory) = "" Then MkDir(msFullDir)
                'Threading.Thread.Sleep(2000)
                Dim XMLWriter As Xml.XmlTextWriter = New Xml.XmlTextWriter(strFullFile, System.Text.Encoding.GetEncoding("EUC-KR"))
                With XMLWriter
                    .Formatting = Xml.Formatting.Indented
                    .WriteStartDocument(False)
                    .WriteStartElement("ROOT")
                    .WriteElementString("USEDP", r_o_stu.USEDP)   '
                    .WriteElementString("PROVIDER", r_o_stu.PROVIDER)
                    .WriteElementString("CATEGORY", r_o_stu.CATEGORY)
                    .WriteElementString("DATASOURCE", r_o_stu.DATASOURCE)
                    .WriteElementString("USERID", r_o_stu.USERID)
                    .WriteElementString("PASSWORD", (New CommFN.DES).Encode(r_o_stu.PASSWORD, ""))
                    .WriteElementString("DESCRIPTION", r_o_stu.DESCRIPTION)
                    .WriteEndElement()
                    .Close()
                End With

                SetConnStr = True
            Catch ex As Exception
                'SetConnStr = False
                'Fn.log(sFile & sFn, Err)
                'Throw (New Exception(ex.Message, ex))
            End Try

        End Function

        ' DB Connection String 가져오기
        Public Function GetConnStr() As STU_CONNSTR
            Dim sFn As String = "Private Function GetConnStr() As clsCONN_STR"
            Dim sFullFile As String = msFullDir + "\DBSERVER.XML"
            Dim o_stu As New STU_CONNSTR
            Dim XMLReader As Xml.XmlTextReader
            Try

                If Dir(msFullDir, FileAttribute.Directory) = "" Then MkDir(msFullDir)

                If Dir(sFullFile) <> "" Then
                    XMLReader = New Xml.XmlTextReader(sFullFile)
                    With XMLReader
                        .ReadStartElement("ROOT")
                        o_stu.USEDP = .ReadElementString("USEDP")
                        o_stu.PROVIDER = .ReadElementString("PROVIDER")
                        o_stu.CATEGORY = .ReadElementString("CATEGORY")
                        o_stu.DATASOURCE = .ReadElementString("DATASOURCE")
                        o_stu.USERID = .ReadElementString("USERID")
                        o_stu.PASSWORD = (New DES).Decode(.ReadElementString("PASSWORD"), "")
                        o_stu.DESCRIPTION = .ReadElementString("DESCRIPTION")
                        .ReadEndElement()
                        .Close()
                    End With
                Else
                End If

            Catch ex As Exception
                Fn.log(sFile + sFn, Err)

                o_stu.USEDP = "2"
                o_stu.PROVIDER = "OraOLEDB.Oracle"
                o_stu.CATEGORY = ""
                o_stu.DATASOURCE = "(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.95.21.142)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = EMRDB)))"
                o_stu.USERID = "lisif"
                o_stu.PASSWORD = "lisif"
                o_stu.DESCRIPTION = "PROD_EMRDB2"

            Finally
                XMLReader.Close()
                GetConnStr = o_stu

            End Try

        End Function


        ' DB Connection String 가져오기
        Public Function GetConnStr_mssql_scl() As STU_CONNSTR
            Dim sFn As String = "Public Function GetConnStr_mssql_scl() As STU_CONNSTR"
            Dim sFullFile As String = msFullDir + "\DBSERVER_MSSQL_SCL.XML"
            Dim o_stu As New STU_CONNSTR
            Dim XMLReader As Xml.XmlTextReader
            Try

                If Dir(msFullDir, FileAttribute.Directory) = "" Then MkDir(msFullDir)

                If Dir(sFullFile) <> "" Then
                    XMLReader = New Xml.XmlTextReader(sFullFile)
                    With XMLReader
                        .ReadStartElement("ROOT")
                        o_stu.USEDP = .ReadElementString("USEDP")
                        o_stu.PROVIDER = .ReadElementString("PROVIDER")
                        o_stu.CATEGORY = .ReadElementString("CATEGORY")
                        o_stu.DATASOURCE = .ReadElementString("DATASOURCE")
                        o_stu.USERID = .ReadElementString("USERID")
                        'o_stu.PASSWORD = (New DES).Decode(.ReadElementString("PASSWORD"), "")
                        o_stu.PASSWORD = .ReadElementString("PASSWORD")
                        o_stu.DESCRIPTION = .ReadElementString("DESCRIPTION")
                        .ReadEndElement()
                        .Close()
                    End With
                Else
                End If

            Catch ex As Exception
                Fn.log(sFile + sFn, Err)

                o_stu.USEDP = "2"
                o_stu.PROVIDER = "OraOLEDB.Oracle"
                o_stu.CATEGORY = ""
                o_stu.DATASOURCE = "(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.95.21.142)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = EMRDB)))"
                o_stu.USERID = "lisif"
                o_stu.PASSWORD = "lisif"
                o_stu.DESCRIPTION = "PROD_EMRDB2"

            Finally
                XMLReader.Close()
                GetConnStr_mssql_scl = o_stu

            End Try

        End Function

        ' 연결할 서버선택
        Public Sub SetServerId(ByVal asServerID As String)
            Dim sFn As String = "Private Sub SetServerId(ByVal asServerID As String)"
            Dim strFullFile As String = msFullDir & "\SERVER_ID.XML"

            Try
                If Dir(msFullDir, FileAttribute.Directory) = "" Then MkDir(msFullDir)

                Dim XMLWriter As Xml.XmlTextWriter = New Xml.XmlTextWriter(strFullFile, System.Text.Encoding.GetEncoding("EUC-KR"))
                With XMLWriter
                    .Formatting = Xml.Formatting.Indented
                    .WriteStartDocument(False)
                    .WriteStartElement("ROOT")
                    .WriteElementString("ServerID", asServerID)
                    .WriteEndElement()
                    .Close()
                End With

            Catch ex As Exception
                Fn.log(sFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try


        End Sub

        ' 연결할 서버 리턴
        Public Function GetServerId() As enumSID
            Dim sFn As String = "Private Function GetServerId() As String"
            Dim strFullFile As String = msFullDir & "\SERVER_ID.XML"

            Try
                If Dir(msFullDir, FileAttribute.Directory) = "" Then MkDir(msFullDir)

                If Dir(strFullFile) <> "" Then
                    Dim XMLReader As Xml.XmlTextReader = New Xml.XmlTextReader(strFullFile)
                    With XMLReader
                        .ReadStartElement("ROOT")
                        GetServerId = CType(.ReadElementString("ServerID"), enumSID)
                        .ReadEndElement()
                        .Close()
                    End With

                Else
                    SetServerId(CStr(enumSID.LIS))
                    GetServerId = enumSID.LIS

                End If

            Catch ex As Exception
                Fn.log(sFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try

        End Function

    End Class

End Namespace

