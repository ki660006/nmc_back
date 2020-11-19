Namespace ComDb
    Public Enum enumSID
        LIS = 0
        OCS = 1
        LIS_MSSQL = 2

        LIS_LIVE = 0
        LIS_DEV1 = 1
        LIS_DEV2 = 2
    End Enum

    Public Class STU_CONNSTR
        Public USEDP As String = ""
        Public PROVIDER As String = ""
        Public CATEGORY As String = ""
        Public DATASOURCE As String = ""
        Public USERID As String = ""
        Public PASSWORD As String = ""

        Public Sub New()
            MyBase.NEW()
        End Sub
    End Class

    Public Class Info
        Private Const msFile As String = "File : CGCOM_DB.vb, Class : ComDb.Info" + vbTab
        Private msDir As String = Environment.CurrentDirectory + "\XML"

        ' DB Connection String 설정
        Public Function SetConnStr(ByVal r_stuCnStr As STU_CONNSTR) As Boolean
            Dim sFn As String = "Public Function SetConnStr(STU_CONNSTR) As Boolean"
            Dim sFile As String = msDir + "\DBSERVER.XML"

            Try
                If Dir(msDir, FileAttribute.Directory) = "" Then MkDir(msDir)

                Dim XMLWriter As Xml.XmlTextWriter = New Xml.XmlTextWriter(sFile, System.Text.Encoding.GetEncoding("EUC-KR"))
                With XMLWriter
                    .Formatting = Xml.Formatting.Indented
                    .WriteStartDocument(False)
                    .WriteStartElement("ROOT")
                    .WriteElementString("USEDP", r_stuCnStr.USEDP)   '
                    .WriteElementString("PROVIDER", r_stuCnStr.PROVIDER)
                    .WriteElementString("CATEGORY", r_stuCnStr.CATEGORY)
                    .WriteElementString("DATASOURCE", r_stuCnStr.DATASOURCE)
                    .WriteElementString("USERID", r_stuCnStr.USERID)
                    .WriteElementString("PASSWORD", (New ComFN.DES).Encode(r_stuCnStr.PASSWORD, ""))
                    .WriteEndElement()
                    .Close()
                End With

                SetConnStr = True
            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try

        End Function

        ' DB Connection String 가져오기
        Public Function GetConnStr() As STU_CONNSTR
            Dim sFn As String = "Public Function GetConnStr() As STU_CONNSTR"
            Dim sFile As String = msDir + "\DBSERVER.XML"
            Dim stuCnStr As New STU_CONNSTR
            Dim XMLReader As Xml.XmlTextReader

            Try
                '-- 테스트용 
                'Fn.log("##### GetConnStr : msFullDir=" & msFullDir & ", strFullFile=" & strFullFile)

                If Dir(msDir, FileAttribute.Directory) = "" Then MkDir(msDir)

                If Dir(sFile) <> "" Then
                    XMLReader = New Xml.XmlTextReader(sFile)
                    With XMLReader
                        .ReadStartElement("ROOT")
                        stuCnStr.USEDP = .ReadElementString("USEDP")
                        stuCnStr.PROVIDER = .ReadElementString("PROVIDER")
                        stuCnStr.CATEGORY = .ReadElementString("CATEGORY")
                        stuCnStr.DATASOURCE = .ReadElementString("DATASOURCE")
                        stuCnStr.USERID = .ReadElementString("USERID")
                        stuCnStr.PASSWORD = (New ComFN.DES).Decode(.ReadElementString("PASSWORD"), "")
                        .ReadEndElement()
                        .Close()
                    End With
                Else
                End If

            Catch ex As Exception
                ComFN.Fn.Log(sFile & sFn, Err)
                'Throw (New Exception(ex.Message, ex))
                stuCnStr.USEDP = "2"
                stuCnStr.PROVIDER = "SQLOLEDB"
                stuCnStr.CATEGORY = "fklis"
                stuCnStr.DATASOURCE = "192.168.3.192\fkhisdbdev"
                stuCnStr.USERID = "fklis  "
                stuCnStr.PASSWORD = "fklis12#$"

            Finally
                XMLReader.Close()
                GetConnStr = stuCnStr
            End Try

        End Function

        ' 연결할 서버선택
        Public Sub SetServerId(ByVal rsServerID As String)
            Dim sFn As String = "Public Sub SetServerId(String)"
            Dim strFullFile As String = msDir + "\SERVER_ID.XML"

            Try
                If Dir(msDir, FileAttribute.Directory) = "" Then MkDir(msDir)

                Dim XMLWriter As Xml.XmlTextWriter = New Xml.XmlTextWriter(strFullFile, System.Text.Encoding.GetEncoding("EUC-KR"))
                With XMLWriter
                    .Formatting = Xml.Formatting.Indented
                    .WriteStartDocument(False)
                    .WriteStartElement("ROOT")
                    .WriteElementString("ServerID", rsServerID)
                    .WriteEndElement()
                    .Close()
                End With

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))

            End Try


        End Sub

        ' 연결할 서버 리턴
        Public Function GetServerId() As enumSID
            Dim sFn As String = "Private Function GetServerId() As String"
            Dim sFile As String = msDir + "\SERVER_ID.XML"

            Try
                If Dir(msDir, FileAttribute.Directory) = "" Then MkDir(msDir)

                If Dir(sFile) <> "" Then
                    Dim XMLReader As Xml.XmlTextReader = New Xml.XmlTextReader(sFile)
                    With XMLReader
                        .ReadStartElement("ROOT")
                        GetServerId = CType(.ReadElementString("ServerID"), enumSID)
                        .ReadEndElement()
                        .Close()
                    End With

                Else
                    SetServerId(CStr(enumSID.LIS))
                    Return enumSID.LIS

                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try

        End Function

    End Class

End Namespace
