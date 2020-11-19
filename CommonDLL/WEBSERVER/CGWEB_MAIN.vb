Imports System.Windows.Forms
Imports System.IO
Imports System.Xml

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Public Class CGWEB_MAIN

    Public Sub New()

        Try
            'Dim sDbCnStrig As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\MDB\NMC.MDB"

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    Private Function fnGet_XmlParsing(ByVal r_sr As System.IO.StreamReader, ByRef rsField As String) As String

        Dim sFn As String = "fnGet_XmlParsing"

        Dim xmlReader As XmlTextReader = New Xml.XmlTextReader(r_sr)
        Dim sValue As String = ""

        Try
            While xmlReader.Read()
                Select Case xmlReader.NodeType
                    Case XmlNodeType.Comment
                    Case XmlNodeType.Element
                        If xmlReader.Name.ToLower = "maindata" Then
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

    '-- LogOut Time
    Public Function fnGet_LogOutTime() As String
        Dim sFn As String = "Public Shared Function fnGet_LogOutTime() As String"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            '' PKG_ACK_MAIN.PRO_GET_LOGOUT_TIME_INFO'

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP 
#End If
            If sURL.Trim = "" Then
#If DEBUG Then
                sURL = "http://his999dev.nmc.or.kr:8088/himed"
#Else
                sURL = "http://hi.nmc.or.kr/himed"
#End If
            End If

            sURL = "http://hi.nmc.or.kr/himed"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00204&business_id=lis"
            sURL += "&"

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

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString()

            Else
                Return ""
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Function fnGet_DepFile_NewVersion(ByVal rsPrgId As String, ByVal rsFileNm As String, ByVal rsFileVer As String) As String
        Dim sFn As String = "Public Shared Function fnGet_LogOutTime() As String"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            'pkg_ack_main.PRO_GET_DEPFILE_VAR_INFO"


#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP 
#End If
            If sURL.Trim = "" Then
#If DEBUG Then
                sURL = "http://his999dev.nmc.or.kr:8088/himed"
#Else
                sURL = "http://hi.nmc.or.kr/himed"
#End If
            End If

            sURL = "http://hi.nmc.or.kr/himed"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00203&business_id=lis"
            sURL += "&prgid=" + rsPrgId
            sURL += "&filenm=" + rsFileNm
            sURL += "&filever=" + rsFileVer
            sURL += "&"

            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            Dim sFields As String = ""
            Dim sRetVal = fnGet_XmlParsing(sr, sFields)

            Return sRetVal

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    '-- 사용권한
    Public Function fnGet_PrgInfo(ByVal rsSklGrpCd As String) As DataTable
        Dim sFn As String = "Public Shared Function fnGet_PrgInfo() As DataTable"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            'PKG_ACK_MAIN.PKG_GET_PRG_INFO

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP 
#End If
            If sURL.Trim = "" Then
#If DEBUG Then
                sURL = "http://his999dev.nmc.or.kr:8088/himed"
#Else
                sURL = "http://hi.nmc.or.kr/himed"
#End If
            End If

            sURL = "http://hi.nmc.or.kr/himed"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00206&business_id=lis"
            sURL += "&"

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

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)
            Next

            If dt.Rows.Count > 0 Then
                Dim sWhere As String = ""

                If rsSklGrpCd <> "" Then sWhere += "sklgrp = '000'"

                dt = Fn.ChangeToDataTable(dt.Select(sWhere))
            End If

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try


    End Function

    '-- 로그인 정보
    Public Function fnGet_UsrInfo(ByVal rsUsrId As String) As DataTable
        Dim sFn As String = "Public Shared Function fnGet_UsrInfo(rsUsrId) As DataTable"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            'pkg_ack_main.pkg_get_user_info

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP 
#End If

            If sURL.Trim = "" Then
#If DEBUG Then
                sURL = "http://his999dev.nmc.or.kr:8088/himed"
#Else
                sURL = "http://hi.nmc.or.kr/himed"
#End If
            End If

            sURL = "http://hi.nmc.or.kr/himed"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00207&business_id=lis"
            sURL += "&instcd=" + PRG_CONST.SITECD
            sURL += "&usrid=" + rsUsrId
            sURL += "&"

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

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    '-- 사용자 권한
    Public Function fnGet_UsrSkill(ByVal rsUsrId As String) As DataTable
        Dim sFn As String = "Public Shared Function fnGet_UsrInfo(rsUsrId) As DataTable"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            'pkg_ack_main.PKG_GET_USER_SKILL_INFO"

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP 
#End If

            If sURL.Trim = "" Then
#If DEBUG Then
                sURL = "http://his999dev.nmc.or.kr:8088/himed"
#Else
                sURL = "http://hi.nmc.or.kr/himed"
#End If
            End If

            sURL = "http://hi.nmc.or.kr/himed"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00208&business_id=lis"
            sURL += "&usrid=" + rsUsrId
            sURL += "&"

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

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function fnGet_CONFIG_INFO() As DataTable
        Dim sFn As String = "Public Function fnGet_CONFIG_INFO() As DataTable"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            'PKG_ACK_MAMIN.PKG_GET_CONFIG_INFO()

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP 
#End If
            If sURL.Trim = "" Then
#If DEBUG Then
                sURL = "http://his999dev.nmc.or.kr:8088/himed"
#Else
                sURL = "http://hi.nmc.or.kr/himed"
#End If

            End If

            sURL = "http://hi.nmc.or.kr/himed"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00202&business_id=lis"
            sURL += "&"

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

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function


    Public Function fnGet_MenuInfo(ByVal rsUsrId) As DataTable
        Dim sFn As String = "Public Shared Function fnGet_MenuInfo(rsUsrId) As DataTable"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            'PKG_ACK_MAIN.PKG_GET_MENU_INFO

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP 
#End If
            If sURL.Trim = "" Then
#If DEBUG Then
                sURL = "http://his999dev.nmc.or.kr:8088/himed"
#Else
                sURL = "http://hi.nmc.or.kr/himed"
#End If
            End If

            sURL = "http://hi.nmc.or.kr/himed"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00205&business_id=lis"
            sURL += "&usrid=" + rsUsrId
            sURL += "&"

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

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function fnExe_UserInfo(ByVal rsUsrid As String, ByVal rsUsrnm As String, ByVal rsUsrlvl As String, ByVal rsOther As String) As Boolean
        Dim sFn As String = "Public Function fnExe_UserInfo(STU_COLLWEB, Boolean, Boolean) As String"

        Try

            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            'PKG_ACK_MAIN.PRO_EXE_USER_INFO

#If DEBUG Then
            sURL = PRG_CONST.SERVERIP_DEV
#Else
            sURL = PRG_CONST.SERVERIP
#End If
            If sURL.Trim = "" Then
#If DEBUG Then
                sURL = "http://his999dev.nmc.or.kr:8088/himed"
#Else
                sURL = "http://hi.nmc.or.kr/himed"
#End If

            End If

            sURL = "http://hi.nmc.or.kr/himed"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00201&business_id=lis"
            sURL += "&usrid=" + rsUsrid
            sURL += "&usrnm=" + rsUsrnm
            sURL += "&usrlvl=" + rsUsrlvl
            sURL += "&other=" + rsOther
            sURL += "&"

            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            Dim sFields As String = ""
            Dim sRetVal = fnGet_XmlParsing(sr, sFields)

            If sRetVal.StartsWith("00") Then
                Return True
            Else
                Throw (New Exception(sRetVal))
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

