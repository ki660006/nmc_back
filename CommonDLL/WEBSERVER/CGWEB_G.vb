Imports System.Windows.Forms
Imports System.IO
Imports System.Xml

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Public Class CGWEB_G


    Public Function ExecuteDo(ByVal r_stu As STU_GVINFO) As String
        Dim sFn As String = "Public Function ExecuteDo(ArrayList) As Boolean"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            '추가처방 MIC, Disk 선택 기능 추가 20150427 허용석
            Dim sRetVal As String = ""
            Dim sRetVal2 As String = "" 'MIC, Disk 둘다 체크 시 Disk 값을 임시로 담아줌.

            Dim sURL = ""
            Dim sURL2 = "" 'MIC, Disk 둘다 체크 시 Disk에서 사용
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse
            Dim wbRep2 As Net.WebResponse 'MIC, Disk 둘다 체크 시 Disk에서 사용

#If DEBUG Then
            sURL += PRG_CONST.SERVERIP_DEV
            sURL2 += PRG_CONST.SERVERIP_DEV 'MIC, Disk 둘다 체크 시 Disk에서 사용
#Else
            sURL += PRG_CONST.SERVERIP
            sURL2 += PRG_CONST.SERVERIP 'MIC, Disk 둘다 체크 시 Disk에서 사용
#End If
            If r_stu.ORDCD IsNot "" And r_stu.ORDCD2 IsNot "" Then
                sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00401&business_id=lis"
                sURL += "&regno=" + r_stu.REGNO
                sURL += "&status=" + r_stu.STATUS
                sURL += "&deptcd=" + IIf(r_stu.DEPTCD_USR = "", "2200000000", r_stu.DEPTCD_USR).ToString
                sURL += "&deptnm=" + IIf(r_stu.DEPTNM_USR = "", "진단검사의학과", r_stu.DEPTNM_USR).ToString

                If r_stu.STATUS.IndexOf(",") >= 0 Then

                    If r_stu.STATUS.Split(","c)(1) = "M" Then
                        sURL += "&usrid=" + r_stu.ORDDRID
                        sURL += "&usrnm=" + r_stu.ORDDRNM
                    Else
                        sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "210003", USER_INFO.USRID).ToString
                        sURL += "&usrnm=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "정보경", USER_INFO.USRID).ToString

                    End If

                Else
                    sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "210003", USER_INFO.USRID).ToString
                    sURL += "&usrnm=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "정보경", USER_INFO.USRID).ToString
                End If

                sURL += "&ip=" + USER_INFO.LOCALIP


                sURL += "&ordcd=" + r_stu.ORDCD    'MIC
                sURL += "&sugacd=" + r_stu.SUGACD

                sURL += "&spccd=" + r_stu.SPCCD
                sURL += "&ioflag=" + "I"
                sURL += "&"

                wbReq = Net.WebRequest.Create(sURL)
                wbRep = wbReq.GetResponse()

                sURL2 += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00401&business_id=lis"
                sURL2 += "&regno=" + r_stu.REGNO
                sURL2 += "&status=" + r_stu.STATUS
                sURL2 += "&deptcd=" + IIf(r_stu.DEPTCD_USR = "", "2200000000", r_stu.DEPTCD_USR).ToString
                sURL2 += "&deptnm=" + IIf(r_stu.DEPTNM_USR = "", "진단검사의학과", r_stu.DEPTNM_USR).ToString


                If r_stu.STATUS.IndexOf(",") >= 0 Then

                    If r_stu.STATUS.Split(","c)(1) = "M" Then
                        sURL2 += "&usrid=" + r_stu.ORDDRID
                        sURL2 += "&usrnm=" + r_stu.ORDDRNM
                    Else
                        sURL2 += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "210003", USER_INFO.USRID).ToString
                        sURL2 += "&usrnm=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "정보경", USER_INFO.USRID).ToString

                    End If

                Else
                    sURL2 += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "210003", USER_INFO.USRID).ToString
                    sURL2 += "&usrnm=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "정보경", USER_INFO.USRID).ToString
                End If

                sURL2 += "&ip=" + USER_INFO.LOCALIP


                sURL2 += "&ordcd=" + r_stu.ORDCD2    'DISK
                sURL2 += "&sugacd=" + r_stu.SUGACD2

                sURL2 += "&spccd=" + r_stu.SPCCD
                sURL2 += "&ioflag=" + "I"
                sURL2 += "&"

                wbReq = Net.WebRequest.Create(sURL2)
                wbRep2 = wbReq.GetResponse()

            ElseIf r_stu.ORDCD IsNot "" Then
                ' sURL = PRG_CONST.SERVERIP
                sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00401&business_id=lis"
                sURL += "&regno=" + r_stu.REGNO
                sURL += "&status=" + r_stu.STATUS
                sURL += "&deptcd=" + IIf(r_stu.DEPTCD_USR = "", "2200000000", r_stu.DEPTCD_USR).ToString
                sURL += "&deptnm=" + IIf(r_stu.DEPTNM_USR = "", "진단검사의학과", r_stu.DEPTNM_USR).ToString


                If r_stu.STATUS.IndexOf(",") >= 0 Then

                    If r_stu.STATUS.Split(","c)(1) = "M" Then
                        sURL += "&usrid=" + r_stu.ORDDRID
                        sURL += "&usrnm=" + r_stu.ORDDRNM
                    Else
                        sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "210003", USER_INFO.USRID).ToString
                        sURL += "&usrnm=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "정보경", USER_INFO.USRID).ToString

                    End If

                Else
                    sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "210003", USER_INFO.USRID).ToString
                    sURL += "&usrnm=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "정보경", USER_INFO.USRID).ToString
                End If

                sURL += "&ip=" + USER_INFO.LOCALIP


                sURL += "&ordcd=" + r_stu.ORDCD    'MIC
                sURL += "&sugacd=" + r_stu.SUGACD

                sURL += "&spccd=" + r_stu.SPCCD
                sURL += "&ioflag=" + "I"
                sURL += "&"

                wbReq = Net.WebRequest.Create(sURL)
                wbRep = wbReq.GetResponse()

            ElseIf r_stu.ORDCD2 IsNot "" Then
                sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00401&business_id=lis"
                sURL += "&regno=" + r_stu.REGNO
                sURL += "&status=" + r_stu.STATUS
                sURL += "&deptcd=" + IIf(r_stu.DEPTCD_USR = "", "2200000000", r_stu.DEPTCD_USR).ToString
                sURL += "&deptnm=" + IIf(r_stu.DEPTNM_USR = "", "진단검사의학과", r_stu.DEPTNM_USR).ToString


                If r_stu.STATUS.IndexOf(",") >= 0 Then

                    If r_stu.STATUS.Split(","c)(1) = "M" Then
                        sURL += "&usrid=" + r_stu.ORDDRID
                        sURL += "&usrnm=" + r_stu.ORDDRNM
                    Else
                        sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "210003", USER_INFO.USRID).ToString
                        sURL += "&usrnm=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "정보경", USER_INFO.USRID).ToString

                    End If

                Else
                    sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "210003", USER_INFO.USRID).ToString
                    sURL += "&usrnm=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "정보경", USER_INFO.USRID).ToString
                End If

                sURL += "&ip=" + USER_INFO.LOCALIP


                sURL += "&ordcd=" + r_stu.ORDCD2    'DISK
                sURL += "&sugacd=" + r_stu.SUGACD2

                sURL += "&spccd=" + r_stu.SPCCD
                sURL += "&ioflag=" + "I"
                sURL += "&"

                wbReq = Net.WebRequest.Create(sURL)
                wbRep = wbReq.GetResponse()
            End If

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            sRetVal = fnGet_XmlParsing(sr)

            Dim sBcNo As String = ""
            Dim sBcNo2 As String = "" 'MIC, Disk 둘다 체크 하고 진행 시 Disk에서 사용.

            If sRetVal.StartsWith("00") Then
                If sRetVal.Length < 18 Then
                    sRetVal = sRetVal.Substring(0, 2)
                Else
                    sBcNo = sRetVal.Substring(2, 15) : sRetVal = sRetVal.Substring(0, 2)
                End If
            End If

            If r_stu.ORDCD IsNot "" And r_stu.ORDCD2 IsNot "" And sRetVal.StartsWith("00") Then 'MIC, Disk 둘다 체크, MIC 성공 시

                Dim sr2 As System.IO.StreamReader = New System.IO.StreamReader(wbRep2.GetResponseStream(), System.Text.Encoding.UTF8)
                sRetVal2 = fnGet_XmlParsing(sr2)

                If sRetVal2.StartsWith("00") Then
                    If sRetVal2.Length < 18 Then
                        sRetVal = sRetVal2.Substring(0, 2) 'MIC, Disk 둘다 체크 실행 후, 전부 성공하면 반환값 담아줌
                    Else
                        sBcNo2 = sRetVal2.Substring(2, 15) : sRetVal = sRetVal2.Substring(0, 2) 'MIC, Disk 둘다 체크 실행 후, 전부 성공하면 반환값 담아줌
                    End If
                End If
            End If

            Return sRetVal
            ' 20150427 END

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
