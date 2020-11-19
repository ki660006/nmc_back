Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Imports System.Net
Imports System.Web
Imports System.Text


Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports System.Net.Json

Public Class CGWEB_S

    Dim request As HttpWebRequest
    Dim response As HttpWebResponse = Nothing
    Dim reader As StreamReader
    Dim adderss As Uri
    Dim appid As String
    Dim context As String
    Dim query As String
    Dim data As StringBuilder
    Dim bytedata() As Byte
    Dim postStream As Stream = Nothing
    Public Function UTF8EN(ByVal T As String) As String
        Dim Bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(T)
        Dim S(UBound(Bytes)) As String
        Dim ResultStr As New StringBuilder
        Dim TempStr As String
        For Each b As Byte In Bytes

            '//공백값처리를 위해 공백값은 "+"로 표현한다.
            If b = 32 Then
                TempStr = "+"
            Else

                '//인코딩값중 한글이 아닌 (영어,기호,숫자)문자는 그냥 문자로..
                '//한글은 설정된 아스키코드앞에 %표시하고 아스키코드를 16진수 값으로 변환
                'TempStr = CType(IIf(b < 32 Or b > 127, "%" & Hex(b), Chr(b)), String)
                TempStr = CType(IIf(b < 32 Or b > 127, "%" & Hex(b), Chr(b)), String)

                If (b < 32 Or b > 127) Then  '한글 
                    TempStr = "%" & Hex(b)
                Else
                    If (b = 61 Or b = 44) Then '특수문자일경우 ( = , ) 
                        TempStr = "%" & Hex(b)
                    Else
                        TempStr = Chr(b)
                    End If

                End If

            End If

            '//변환된 값을 StringBuilder에 저장한다.
            ResultStr.Append(TempStr)

        Next

        'For i As Integer = 0 To UBound(Bytes)
        '    S(i) = Join({"%", Hex(Bytes(i))}, vbNullString)
        'Next

        Return ResultStr.ToString
    End Function

    Public Shared Function encode(ByVal str As String) As String
        Dim utf8Encoding As New System.Text.UTF8Encoding
        Dim encodedString() As Byte
        Dim ResultStr As New StringBuilder
        encodedString = utf8Encoding.GetBytes(str)

        For Each b As Byte In encodedString
            ResultStr.Append(encodedString)
        Next

        Return ResultStr.ToString()
    End Function

    Public Function fnRegWebServer_for_KCDC(ByVal rarrRefList As ArrayList) As String

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse
            Dim sOgcr As String = "cn=국립중앙의료원,ou=건강보험,ou=MOHW RA센터,ou=등록기관,ou=licensedCA,o=KICA,c=KR"

            Dim sRHospiCd As String = CType(rarrRefList.Item(0), REFLIST).RHospiCd
            Dim sRHospiNm As String = CType(rarrRefList.Item(0), REFLIST).RHospiNm
            Dim sRHospiUsr As String = CType(rarrRefList.Item(0), REFLIST).RHospiUsr
            Dim sPatnm As String = CType(rarrRefList.Item(0), REFLIST).SpcName
            Dim sRegno As String = CType(rarrRefList.Item(0), REFLIST).SpcRegno
            Dim sSex As String = CType(rarrRefList.Item(0), REFLIST).SpcSex
            sSex = IIf(sSex = "M", "1", "2")
            Dim sDept As String = CType(rarrRefList.Item(0), REFLIST).SpcDept
            Dim sBirth As String = CType(rarrRefList.Item(0), REFLIST).SpcBirTh

            Dim sSpcspc As String = CType(rarrRefList.Item(0), REFLIST).Spc
            Dim sSpcspcetc As String = CType(rarrRefList.Item(0), REFLIST).Spcetc
            Dim sTest As String = CType(rarrRefList.Item(0), REFLIST).Test
            Dim sTestretc As String = CType(rarrRefList.Item(0), REFLIST).Testetc

            Dim sHospicd As String = CType(rarrRefList.Item(0), REFLIST).Refcd

            Dim sTkdt As String = CType(rarrRefList.Item(0), REFLIST).Tkdt
            Dim sfndt As String = CType(rarrRefList.Item(0), REFLIST).fndt

            Dim sTestUsr As String = CType(rarrRefList.Item(0), REFLIST).TestUsr

            Dim sEtc As String = "" '비고 

            sURL = "https://152.99.73.139:8443/indigo/PthgnRgstr?" '질병관리본부 연계 서버 URL
            sURL += "&ogcr=" + UTF8EN(sOgcr)  '1) 사용자(기관)인증정보 [필] 
            sURL += "&reqestinstt_charger_nm=" + UTF8EN(sRHospiUsr) '2) 검사기관_담당자_성명 [필]
            sURL += "&inspctinstt_charger_nm=" + UTF8EN(sTestUsr)  '3) 검사기관_검사자_성명 [필]
            sURL += "&patnt_nm=" + UTF8EN(sPatnm)                   '4) 환자성명 [필]ㅣ
            sURL += "&patnt_sexdstn_cd=" + sSex             '5) 환자성별코드 [필]
            sURL += "&patnt_lifyea_md=" + sBirth            '6) 환자생년월일 [필](YYYYMMDD)
            sURL += "&patnt_regist_no=" + sRegno            '7) 환자등록번호 [필]
            sURL += "&kwa_ward_nm=" + UTF8EN(sDept)                 '8)과 병동 명
            sURL += "&spm_ty_list=" + UTF8EN(sSpcspc)  '9)검체유형리스트[필]
            sURL += "&spm_ty_etc=" + UTF8EN(sSpcspcetc) '10)검체유형기타
            sURL += "&inspct_mth_ty_list=" + UTF8EN(sTest) '11)검사방법유형 리스트 [필]
            sURL += "&inspct_mth_ty_etc=" + UTF8EN(sTestretc) '12) 검사방법유형기타
            sURL += "&pthgogan_cd=" + UTF8EN(sHospicd) '13)병원체코드 [필]
            sURL += "&reqest_de=" + UTF8EN(sTkdt) '14) 의뢰일자 [필] (YYYYMMDD)   
            sURL += "&dgnss_de=" + UTF8EN(sfndt) '15) 진검단일자 [필] (YYYYMMDD)
            sURL += "&rm_info=" + UTF8EN(sEtc) '16) 비고정보
            sURL += "&hsptl_swbser=" + UTF8EN("ACK") '17) 병원 소프트웨어 개발사 (사업자)
            sURL += "&hsptl_swknd=" + UTF8EN("Ack@LIS_NMC") '18) 병원 소프트웨어 종류 (버전)
            sURL += "&dplct_at=" + "1" '19)중복여부 test시에는 0으로 보낼것 [필] , 실 사용시는 1 
            sURL += "&rspns_mssage_ty=0" '20) 응답 형식 0 :xml , 1:json [필]
            '<<<20170801 추가부분 
            sURL += "&spm_ty_list=01"
            sURL += "&inspct_mth_ty_list=02"
            '>>>
            'sURL += "&mdlcnst_kcn_instt_id=0000" '요향기관기호
            'sURL += "&icd_cd=A0001	" '감염병코드 
            'sURL += "&atfss_de=20160601" ' 발병일자

            'Dim sTest As String = rs.HospiCd
            '
            ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(Function(sender, certificate, chain, sslPolicyErrors) True) '(Function(sender, certificate, chain, sslPolicyErrors) True
            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            Dim sFields As String = ""

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            
            Dim sRetVal = fnGet_XmlParsing_for_KCDC(sr, sFields)



            Return sURL + Chr(124) + sRetVal

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function fnRegWebServer_for_KCDC_test(ByVal rarrRefList As ArrayList) As DataTable

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse
            Dim sOgcr As String = "cn=국립중앙의료원,ou=건강보험,ou=MOHW RA센터,ou=등록기관,ou=licensedCA,o=KICA,c=KR"

            Dim sRHospiCd As String = CType(rarrRefList.Item(0), REFLIST).RHospiCd
            Dim sRHospiNm As String = CType(rarrRefList.Item(0), REFLIST).RHospiNm
            Dim sRHospiUsr As String = CType(rarrRefList.Item(0), REFLIST).RHospiUsr
            Dim sPatnm As String = CType(rarrRefList.Item(0), REFLIST).SpcName
            Dim sRegno As String = CType(rarrRefList.Item(0), REFLIST).SpcRegno
            Dim sSex As String = CType(rarrRefList.Item(0), REFLIST).SpcSex
            sSex = IIf(sSex = "M", "1", "2")
            Dim sDept As String = CType(rarrRefList.Item(0), REFLIST).SpcDept
            Dim sBirth As String = CType(rarrRefList.Item(0), REFLIST).SpcBirTh

            Dim sSpcspc As String = CType(rarrRefList.Item(0), REFLIST).Spc
            Dim sSpcspcetc As String = CType(rarrRefList.Item(0), REFLIST).Spcetc
            Dim sTest As String = CType(rarrRefList.Item(0), REFLIST).Test
            Dim sTestretc As String = CType(rarrRefList.Item(0), REFLIST).Testetc

            Dim sRefcd As String = CType(rarrRefList.Item(0), REFLIST).Refcd

            Dim sTkdt As String = CType(rarrRefList.Item(0), REFLIST).Tkdt
            Dim sfndt As String = CType(rarrRefList.Item(0), REFLIST).fndt

            Dim sTestUsr As String = CType(rarrRefList.Item(0), REFLIST).TestUsr

            Dim sEtc As String = "" '비고 

            sURL = "https://152.99.73.139:8443/indigo/PthgnRgstr?&ogcr=cn%3D%EA%B5%AD%EB%A6%BD%EC%A4%91%EC%95%99%EC%9D%98%EB%A3%8C%EC%9B%90%2Cou%3D%EA%B1%B4%EA%B0%95%EB%B3%B4%ED%97%98%2Cou%3DMOHW+RA%EC%84%BC%ED%84%B0%2Cou%3D%EB%93%B1%EB%A1%9D%EA%B8%B0%EA%B4%80%2Cou%3DlicensedCA%2Co%3DKICA%2Cc%3DKR&reqestinstt_charger_nm=%ED%99%A9%EC%98%81%EC%9B%85&inspctinstt_charger_nm=&patnt_nm=%EA%B9%80%EC%A7%84%ED%9D%AC&patnt_sexdstn_cd=2&patnt_lifyea_md=19580915&patnt_regist_no=00012114&kwa_ward_nm=EM/ER&spm_ty_list=&spm_ty_etc=URINE&inspct_mth_ty_list=&inspct_mth_ty_etc=&pthgogan_cd=citdiv&reoest_de=&dgnss_de=&rm_info=&hsptl_swbser=ACK&hsptl_swknd=Ack@LIS_NMC&dplct_at=0&rspns_mssage_ty=0&spm_ty_list=01&inspct_mth_ty_list=02"
            '>>>
            'sURL += "&mdlcnst_kcn_instt_id=0000" '요향기관기호
            'sURL += "&icd_cd=A0001	" '감염병코드 
            'sURL += "&atfss_de=20160601" ' 발병일자

            'Dim sTest As String = rs.HospiCd
            '
            ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(Function(sender, certificate, chain, sslPolicyErrors) True) '(Function(sender, certificate, chain, sslPolicyErrors) True
            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            Dim sFields As String = ""

            Dim sRetVal = fnGet_XmlParsing(sr, sFields)

            Dim dt As New DataTable

            Dim sRow() As String = sRetVal.Split(Chr(4))

            For ix1 As Integer = 0 To sRow.Length - 1
                If sRow(ix1) = "" Then Exit For
                If ix1 = 0 Then
                    Dim sBufField() As String = sFields.Split(Chr(4))(0).Split(Chr(3))
                    Dim dbCols As New DataColumn

                    For ix2 As Integer = 0 To sBufField.Length - 1
                        dbCols = New DataColumn
                        dbCols.ColumnName = sBufField(ix2) : dt.Columns.Add(dbCols) : dbCols = Nothing
                    Next
                End If

                'Row 추가
                Dim dr As DataRow = dt.NewRow()
                Dim sBuf() As String = sRow(ix1).Split(Chr(3))

                'Dim o_fdinfo() As System.Reflection.FieldInfo = dt.GetType().GetFields()

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            Return dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function fnGetRegist_for_KCDC(ByVal rsUrl As String) As String

        Try
            'Dim request As HttpWebRequest
            'Dim response As HttpWebResponse = Nothing
            'Dim adderss As Uri
            'Dim appid As String
            'Dim context As String
            'Dim query As String
            'Dim data As StringBuilder
            'Dim bytedata() As Byte
            'Dim postStream As Stream = Nothing
            Dim sTest As String = ""

            adderss = New Uri(rsUrl)
            request = DirectCast(WebRequest.Create(adderss), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"

            data = New StringBuilder()
            data.Append("&dplct_at=" + HttpUtility.UrlEncode("1"))
            data.Append("&patnp_mbtlnum=" + HttpUtility.UrlEncode("010-1234-1234"))
            data.Append("&patnp_rn+zip=" + HttpUtility.UrlEncode("12345"))

            bytedata = UTF8Encoding.UTF8.GetBytes(data.ToString())
            request.ContentLength = bytedata.Length

            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            MsgBox(reader.ReadToEnd)
            'Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)

            Return sTest

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function fnGetRegist_for_KCDC_old(ByVal rsUrl As String) As String

        Try
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse = Nothing
            Dim adderss As Uri

            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse
            Dim sTest As String = ""
            Dim arrRegist As ArrayList

            adderss = New Uri(rsUrl)

            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            'Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)

            Dim datastream As Stream = wbRep.GetResponseStream()
            Dim reader As New StreamReader(datastream)
            Dim responseFromServer As String = reader.ReadToEnd()

            Dim parser = New JsonTextParser
            Dim Jsobj As JsonObject = parser.Parse(responseFromServer)

            Dim jsCol As JsonArrayCollection = Jsobj

            For ix As Integer = 0 To jsCol.Count - 1
                Dim test As String = jsCol(ix).GetValue()
            Next

            'Dim sRetVal = fnGet_XmlParsing(sr)
            Return sTest

        Catch ex As Exception
            MsgBox(ex.Message)
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


    Private Function fnGet_XmlParsing(ByVal r_sr As System.IO.StreamReader, ByRef rsField As String) As String

        Dim sFn As String = "fnGet_XmlParsing"

        Dim xmlReader As XmlTextReader = New Xml.XmlTextReader(r_sr)
        Dim sValue As String = ""

        Try
            While xmlReader.Read()
                Select Case xmlReader.NodeType
                    Case XmlNodeType.Comment
                    Case XmlNodeType.Element
                        If xmlReader.Name.ToLower = "patorderlist" Then
                            If sValue <> "" Then
                                sValue += Chr(4)
                                rsField += Chr(4)
                            End If
                        ElseIf xmlReader.Name.ToLower <> "root" Then
                            rsField += xmlReader.Name.ToLower + Chr(3)
                        End If

                    Case XmlNodeType.EndEntity
                    Case XmlNodeType.Text

                        sValue += IIf(xmlReader.Value.Trim = "-", "", xmlReader.Value.Trim).ToString + Chr(3)
                    Case XmlNodeType.CDATA
                        sValue += IIf(xmlReader.Value.Trim = "-", "", xmlReader.Value.Trim).ToString + Chr(3)
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



    Private Function fnGet_XmlParsing_for_KCDC(ByVal r_sr As System.IO.StreamReader, ByRef rsField As String) As String

        Dim sFn As String = "fnGet_XmlParsing"

        Dim xmlReader As XmlTextReader = New Xml.XmlTextReader(r_sr)
        Dim sValue As String = ""

        Try
            While xmlReader.Read()
                Select Case xmlReader.NodeType
                    Case XmlNodeType.Comment
                    Case XmlNodeType.Element
                        'If xmlReader.Name.ToLower = "patorderlist" Then
                        '    If sValue <> "" Then
                        '        sValue += Chr(4)
                        '        rsField += Chr(4)
                        '    End If
                        'ElseIf xmlReader.Name.ToLower <> "root" Then
                        '    rsField += xmlReader.Name.ToLower + Chr(3)
                        'End If
                        sValue += xmlReader.Name.ToLower + Chr(4)
                        'If sValue <> "" Then
                        '    sValue += xmlReader.Name.ToLower + Chr(4)
                        '    'rsField += Chr(4)
                        'End If
                    Case XmlNodeType.EndEntity

                    Case XmlNodeType.Text

                        sValue += IIf(xmlReader.Value.Trim = "-", "", xmlReader.Value.Trim).ToString + Chr(3)
                    Case XmlNodeType.CDATA
                        sValue += IIf(xmlReader.Value.Trim = "-", "", xmlReader.Value.Trim).ToString + Chr(3)
                        'Case XmlNodeType.EndElement
                        '    sValue += xmlReader.Name.ToLower + Chr(3)
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
