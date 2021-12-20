Imports System.Windows.Forms
Imports System.IO
Imports System.Xml

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Public Class CGWEB_B


    Public Function ExecuteDo_Out(ByVal r_al_OutInfo As ArrayList) As Boolean
        Dim sFn As String = "Public Function ExecuteDo_Out(ArrayList) As Boolean"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sRetVal As String
            Dim bRetval As Boolean


            For ix As Integer = 0 To r_al_OutInfo.Count - 1

                Dim sURL = ""
                Dim wbReq As Net.WebRequest
                Dim wbRep As Net.WebResponse

#If DEBUG Then
                sURL += PRG_CONST.SERVERIP_DEV
#Else
            sURL += PRG_CONST.SERVERIP
#End If
                sURL = PRG_CONST.SERVERIP
                sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00201&business_id=lis"
                sURL += "&instcd=" + PRG_CONST.SITECD
                sURL += "&bldno=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                sURL += "&comcd_out=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                sURL += "&tnsjubsuno=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                sURL += "&comcd=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                sURL += "&owngbn=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN
                sURL += "&fkocs=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                sURL += "&regno=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).REGNO
                sURL += "&recid=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).RECID
                sURL += "&recnm=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).RECNM
                sURL += "&abo=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).ABO

                If CType(r_al_OutInfo(ix), STU_TnsJubsu).RH = "+" Then
                    sURL += "&rh=＋"
                ElseIf CType(r_al_OutInfo(ix), STU_TnsJubsu).RH = "-" Then
                    sURL += "&rh=－"
                End If
                sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "410276", USER_INFO.USRID).ToString
                sURL += "&usrip=" + USER_INFO.LOCALIP
                sURL += "&rs_retval="
                sURL += "&"

                wbReq = Net.WebRequest.Create(sURL)
                wbRep = wbReq.GetResponse()

                Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
                sRetVal = fnGet_XmlParsing(sr)

            Next

            If sRetVal.Substring(0, 2) = "00" Then
                bRetval = True
            Else
                bRetval = False
            End If


            Return bRetval

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    'fnExe_Out_NotCross



    Public Function ExecuteDo_Out_cancle(ByVal r_al_OutInfo As ArrayList, ByVal rsOutGbn As String) As Boolean
        Dim sFn As String = "Public Function ExecuteDo_Out_cancle(ArrayList) As Boolean"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sRetVal As String
            Dim bRetval As Boolean


            For ix As Integer = 0 To r_al_OutInfo.Count - 1

                Dim sURL = ""
                Dim wbReq As Net.WebRequest
                Dim wbRep As Net.WebResponse

#If DEBUG Then
                sURL += PRG_CONST.SERVERIP_DEV
#Else
            sURL += PRG_CONST.SERVERIP
#End If
                'sURL = PRG_CONST.SERVERIP
                sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00202&business_id=lis"
                sURL += "&instcd=" + PRG_CONST.SITECD
                sURL += "&outgbn=" + rsOutGbn
                sURL += "&bldno=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                sURL += "&comcd_out=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                sURL += "&tnsjubsuno=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                sURL += "&comcd=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                sURL += "&owngbn=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN
                sURL += "&fkocs=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                sURL += "&regno=" + CType(r_al_OutInfo(ix), STU_TnsJubsu).REGNO

                sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "410276", USER_INFO.USRID).ToString
                sURL += "&usrip=" + USER_INFO.LOCALIP
                sURL += "&rs_retval="
                sURL += "&"

                wbReq = Net.WebRequest.Create(sURL)
                wbRep = wbReq.GetResponse()

                Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
                sRetVal = fnGet_XmlParsing(sr)

            Next

            If sRetVal.Substring(0, 2) = "00" Then
                bRetval = True
            Else
                bRetval = False
            End If


            Return bRetval

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
    Public Function ExecuteDo_Bld_Rtn(ByVal r_al_RtnInfo As ArrayList, ByVal rsGbn As String) As Boolean
        Dim sFn As String = "Public Function ExecuteDo_Bld_Rtn(ArrayList) As Boolean"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sRetVal As String = ""
            Dim bRetval As Boolean


            For ix As Integer = 0 To r_al_RtnInfo.Count - 1

                Dim sURL = ""
                Dim wbReq As Net.WebRequest
                Dim wbRep As Net.WebResponse

#If DEBUG Then
                sURL += PRG_CONST.SERVERIP_DEV
#Else
            sURL += PRG_CONST.SERVERIP
#End If
                sURL = PRG_CONST.SERVERIP
                sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00203&business_id=lis"
                sURL += "&instcd=" + PRG_CONST.SITECD
                sURL += "&rtngbn=" + IIf(rsGbn = "R", "1", "2").ToString
                sURL += "&costyn=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).TEMP01
                sURL += "&bldno=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                sURL += "&comcd_out=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT
                sURL += "&tnsjubsuno=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                sURL += "&comcd=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD
                sURL += "&owngbn=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).OWNGBN
                sURL += "&fkocs=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                sURL += "&regno=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).REGNO
                sURL += "&reqid=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNREQID
                sURL += "&reqnm=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNREQNM
                sURL += "&rsncd=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNRSNCD
                sURL += "&rsncmt=" + CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNRSNCMT
                sURL += "&usrid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "410276", USER_INFO.USRID).ToString
                sURL += "&usrip=" + USER_INFO.LOCALIP
                sURL += "&rs_retval="
                sURL += "&"

                wbReq = Net.WebRequest.Create(sURL)
                wbRep = wbReq.GetResponse()

                Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
                sRetVal = fnGet_XmlParsing(sr)

            Next

            If sRetVal.Substring(0, 2) = "00" Then '결과반환
                bRetval = True
            Else
                bRetval = False
            End If


            Return bRetval

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function ExecuteDo_Bldqnt_chg(ByVal r_stu As STU_TNSCHG, ByVal rsGbn As String) As String
        Dim sFn As String = "Public Function ExecuteDo_Bld_Rtn(ArrayList) As Boolean"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sRetVal As String = ""

            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP
#End If
            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00204&business_id=lis"
            sURL += "&otpt_pid=" + r_stu.REGNO
            sURL += "&otpt_cretno=" + r_stu.CRETNO
            sURL += "&otpt_orddd=" + r_stu.ADMDATE
            sURL += "&otpt_medamtestmyn=" + r_stu.MEDAMTESTMYN

            sURL += "&sess_userid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "410276", USER_INFO.USRID).ToString
            sURL += "&instcd=" + "031"

            sURL += "&ioflag=" + r_stu.IOFLAG
            sURL += "&prcpdd=" + r_stu.ORDDATE
            sURL += "&prcpno=" + r_stu.ORDNO
            sURL += "&prcphistno=" + r_stu.ORDHISTNO
            sURL += "&updtprcpcd=" + r_stu.ORDCD_CHG
            sURL += "&updtcalcscorcd=" + r_stu.SUGACD_CHG
            sURL += "&updtdrugmthdspccd=" + r_stu.SPCCD_CHG
            sURL += "&updtprcpstatcd=" + r_stu.ORDSTATCD
            sURL += "&updtblodno=" + r_stu.BLDNO_CHG
            sURL += "&deptcd=" + IIf(r_stu.DEPTCD_USR = "", "2200000000", r_stu.DEPTCD_USR).ToString
            sURL += "&deptnm=" + IIf(r_stu.DEPTNM_USR = "", "진단검사의학과", r_stu.DEPTNM_USR).ToString

            sURL += "&userid=" + IIf(USER_INFO.USRID.IndexOf("ACK") >= 0, "410276", USER_INFO.USRID).ToString
            sURL += "&usernm=" + USER_INFO.USRNM
            sURL += "&userip=" + USER_INFO.LOCALIP
            sURL += "&tnsno=" + r_stu.TNSNO
            sURL += "&execprcpuniqno=" + r_stu.EXECPRCPUNIQNO
            sURL += "&"

            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            sRetVal = fnGet_XmlParsing(sr)



            Return sRetVal

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
