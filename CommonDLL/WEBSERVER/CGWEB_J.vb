Imports System.Windows.Forms
Imports System.IO
Imports System.Xml

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar


Public Class CGWEB_J
    'HttpResultSvr("0", "0", sOcsHead, sOcsData, sOcsTemp)
    Public Function fngetResult_SCL_WEb(ByVal sOcsHead As String, ByVal sOcsData As String, ByVal sOcsTemp As String) As String
        Dim sFn As String = "Public Function ExecuteDo_TaKe(String, String) As string"
        Dim sURL = ""
        Dim wbReq As Net.WebRequest
        Dim wbRep As Net.WebResponse
        'http://esmart.scllab.co.kr/scl/ResultSvr?JOBGBN=0&RTNGBN=0&OCSHEAD=023035|2|2|20160725|20160726||0||0&OCSDATA=&OCSTEMP=
        sURL = "http://esmart.scllab.co.kr/scl/ResultSvr?" 'SCL 서버 URL
        sURL += "JOBGBN=0"
        sURL += "&RTNGBN=0"
        sURL += "&OCSHEAD=" + sOcsHead
        sURL += "&OCSDATA=" + sOcsData
        sURL += "&OCSTEMP=" + sOcsTemp
        

        'ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(Function(sender, certificate, chain, sslPolicyErrors) True) '(Function(sender, certificate, chain, sslPolicyErrors) True
        wbReq = Net.WebRequest.Create(sURL)
        wbRep = wbReq.GetResponse()

        Dim sFields As String = ""

        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)

        Dim sReturn As String = sr.ReadToEnd

        Return sReturn
        'Dim sRetVal As String = (New WEBSERVER.CGWEB_J).fngetResult_SCL_WEb(arrRefinfo)
    End Function

    Public Function ExecuteDo_TaKe(ByVal rsBcNo As String, ByVal rsPassId As String, ByVal rsUseBfWknoYN As String, ByVal rsPartgbn As String) As String
        Dim sFn As String = "Public Function ExecuteDo_TaKe(String, String) As string"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP
#End If
            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00101"
            sURL += "&business_id=lis"
            sURL += "&instcd=" + PRG_CONST.SITECD   '기관코드
            sURL += "&rs_bcno=" + rsBcNo            '검체번호
            sURL += "&rs_passid=" + rsPassId        '전달자ID
            sURL += "&rs_usrid=" + USER_INFO.USRID  '사용자ID
            sURL += "&rs_ip=" + USER_INFO.LOCALIP   '사용자IP
            sURL += "&rs_wknoyn=" + rsUseBfWknoYN   '작업번호YN
            sURL += "&partgbn=" + rsPartgbn         '파트구분(lis,ris)
            sURL += "&rs_retval="
            sURL += "&"

            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            Dim sRetVal = fnGet_XmlParsing(sr)


            Return sRetVal.Replace(Chr(3), "").Replace(Chr(4), "")

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function ExecuteDo_Cancel(ByVal r_stu As STU_CANCELWEB, ByVal rsPartgbn As String) As String
        Dim sFn As String = "Public Function ExecuteDo_Cancel(STU_CANCELWEB) As string"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP
#End If
            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00102&business_id=lis"
            sURL += "&instcd=" + PRG_CONST.SITECD
            sURL += "&jobgbn=" + r_stu.JOBGBN
            sURL += "&cmtcd=" + r_stu.CMTCD
            sURL += "&cmtcont=" + r_stu.CMTCONT
            sURL += "&regno=" + r_stu.REGNO
            sURL += "&owngbn=" + r_stu.OWNGBN
            sURL += "&bcnos=" + r_stu.BCNOS
            sURL += "&testcds=" + r_stu.TESTCDS
            sURL += "&spccd=" + r_stu.SPCCD
            sURL += "&fkocss=" + r_stu.FKOCSS
            sURL += "&partgbn=" + rsPartgbn
            sURL += "&usrid=" + USER_INFO.USRID
            sURL += "&usrip=" + USER_INFO.LOCALIP + "&"


            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            Dim sRetVal = fnGet_XmlParsing(sr)

            'Dim sWorkNo As String = ""

            If sRetVal = "" Then sRetVal = "99취소오류"

            Return sRetVal.Replace(Chr(3), "").Replace(Chr(4), "")

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Private Function fnGet_XmlParsing(ByVal r_sr As System.IO.StreamReader) As String

        Dim sFn As String = "fnGet_XmlParsing"

        Dim xmlReader As XmlTextReader = New Xml.XmlTextReader(r_sr)
        Dim sValue As String = ""

        Try
            While xmlReader.Read()
                Select Case xmlReader.NodeType
                    Case XmlNodeType.Comment
                    Case XmlNodeType.Element
                        If xmlReader.Name.ToLower = "data" Then
                            If sValue <> "" Then
                                sValue += Chr(4)
                            End If
                        End If
                    Case XmlNodeType.EndEntity
                    Case XmlNodeType.Text
                        sValue += xmlReader.Value.Trim + Chr(3)
                    Case XmlNodeType.CDATA
                        sValue += xmlReader.Value.Trim + Chr(3)
                    Case Else
                End Select
            End While

        Catch ex As XmlException
            Throw (New Exception(ex.Message, ex))

        Finally
            xmlReader.Close()
        End Try

        Return sValue

    End Function


End Class

