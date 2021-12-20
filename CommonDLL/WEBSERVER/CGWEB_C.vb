Imports System.Windows.Forms
Imports System.IO
Imports System.Xml

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Public Class CGWEB_C

    'Private m_DbCn As OracleConnection
    'Private m_dt As New DataTable

    Public Sub New()

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
            'm_DbCn = GetDbConnection()

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
    '2019-07-22 환자리스트 조회 속도 문제로 추가
    Public Function fnGet_PatList_WARD(ByVal r_stu As STU_COLLINFO) As DataTable

        Dim sFn As String = "fnGet_PatList_WARD"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse


            '#If DEBUG Then
            '            sURL += PRG_CONST.SERVERIP_DEV 
            '#Else
            '            sURL += PRG_CONST.SERVERIP 
            '#End If
            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00105&business_id=lis"
            sURL += "&instcd=" + PRG_CONST.SITECD
            sURL += "&ord1=" + r_stu.ORDDT1
            sURL += "&ord2=" + r_stu.ORDDT2
            sURL += "&spcflg1=" + r_stu.SPCFLG1
            sURL += "&spcflg2=" + r_stu.SPCFLG2
            sURL += "&regno=" + r_stu.REGNO
            sURL += "&refflag=" + "pat"
            '2019-07-22 추가
            sURL += "&wardcd=" + r_stu.WARDCD

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

                If r_stu.IOGBN = "O" Then
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "iogbn NOT IN ('I', 'D', 'E')"
                Else
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "iogbn IN ('I', 'D', 'E')"
                End If

                'If r_stu.WARDCD <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "wardno = '" + r_stu.WARDCD + "'"
                If r_stu.DEPTCD <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "deptcd = '" + r_stu.DEPTCD + "'"
                If r_stu.PARTGBN <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "partgbn = '" + r_stu.PARTGBN + "'"


                dt = Fn.ChangeToDataTable(dt.Select(sWhere, "regno"))
            End If

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            'If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
            'm_DbCn.Dispose()
        End Try

    End Function

    Public Function fnGet_PatList(ByVal r_stu As STU_COLLINFO) As DataTable

        Dim sFn As String = "fnGet_PatList"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse


            '#If DEBUG Then
            '            sURL += PRG_CONST.SERVERIP_DEV 
            '#Else
            '            sURL += PRG_CONST.SERVERIP 
            '#End If
            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00105&business_id=lis"
            sURL += "&instcd=" + PRG_CONST.SITECD
            sURL += "&ord1=" + r_stu.ORDDT1
            sURL += "&ord2=" + r_stu.ORDDT2
            sURL += "&spcflg1=" + r_stu.SPCFLG1
            sURL += "&spcflg2=" + r_stu.SPCFLG2
            sURL += "&regno=" + r_stu.REGNO
            sURL += "&refflag=" + "pat"
            sURL += "&wardcd=" + ""
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

                If r_stu.IOGBN = "O" Then
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "iogbn NOT IN ('I', 'D', 'E')"
                Else
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "iogbn IN ('I', 'D', 'E')"
                End If

                If r_stu.WARDCD <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "wardno = '" + r_stu.WARDCD + "'"
                If r_stu.DEPTCD <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "deptcd = '" + r_stu.DEPTCD + "'"
                If r_stu.PARTGBN <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "partgbn = '" + r_stu.PARTGBN + "'"


                dt = Fn.ChangeToDataTable(dt.Select(sWhere, "regno"))
            End If

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            'If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
            'm_DbCn.Dispose()
        End Try

    End Function


    Public Function fnGet_OrdList(ByVal r_stu As STU_COLLINFO, ByVal rbQryMode As Boolean, ByVal rbHopeday As Boolean) As DataTable

        Dim sFn As String = "fnGet_OrdList(Object) As  As DataTable"

        Try
            '//web추가 
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

            'lisif.pkg_ack_coll.pkg_get_order_regno
#If DEBUG Then
            sURL += PRG_CONST.SERVERIP_DEV
#Else
            sURL += PRG_CONST.SERVERIP 
#End If
            sURL = PRG_CONST.SERVERIP
            'sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00101&business_id=lis"
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00104&business_id=lis"
            sURL += "&instcd=" + PRG_CONST.SITECD
            sURL += "&ord1=" + r_stu.ORDDT1
            sURL += "&ord2=" + r_stu.ORDDT2
            sURL += "&spcflg1=" + r_stu.SPCFLG1
            sURL += "&spcflg2=" + r_stu.SPCFLG2
            sURL += "&regno=" + r_stu.REGNO
            sURL += "&refflag=" + "ord"
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

                'Dim o_fdinfo() As System.Reflection.FieldInfo = dt.GetType().GetFields()

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            Dim sSort As String = ""

            If rbQryMode Then
                If r_stu.REGNO = "" Then
                    If r_stu.IOGBN = "I" Then
                        sSort = "patinfo, regno, bcno, roomno, ordday desc, deptcd, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                    Else
                        sSort = "patinfo, regno, bcno, ordday desc, deptcd, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                    End If
                Else
                    If r_stu.IOGBN = "I" Then
                        sSort = "spcinfo, roomno, ordday desc, patinfo, regno, deptcd, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                    Else
                        sSort = "spcinfo, ordday desc, patinfo, regno, deptcd, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                    End If
                End If
            Else
                If r_stu.REGNO = "" Then
                    If r_stu.IOGBN = "I" Then
                        sSort = "wardno, roomno, patinfo, regno, hopeday desc, deptcd, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                    Else
                        sSort = "patinfo, regno, ordday desc, deptcd, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                    End If
                Else
                    If rbHopeday Then
                        sSort = "hopeday desc, deptcd, patinfo, regno, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd, ordday"
                    Else
                        If r_stu.IOGBN = "I" Then
                            If r_stu.WARDCD = "" Then
                                sSort = "wardno, roomno, ordday desc, patinfo, regno, deptcd, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                            Else
                                sSort = "roomno, ordday desc, patinfo, regno, deptcd, doctorcd, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                            End If
                        Else
                            sSort = "ordday desc, hopeday, deptcd, doctorcd, patinfo, regno, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                        End If
                    End If
                End If
            End If

            Dim sWhere As String = ""

            If r_stu.PARTGBN <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "partgbn = '" + r_stu.PARTGBN + "'"
            If r_stu.WARDCD <> "" Then
                sWhere += IIf(sWhere = "", "", " AND ").ToString + " wardno = '" + r_stu.WARDCD + "'"
            ElseIf r_stu.DEPTCD <> "" Then
                Dim sDeptCds As String = ""

                If PRG_CONST.DEPT_HC.Contains(r_stu.DEPTCD) Then

                    For ix = 0 To PRG_CONST.DEPT_HC.Count - 1
                        If ix > 0 Then sDeptCds += ","
                        sDeptCds += PRG_CONST.DEPT_HC.Item(ix).ToString
                    Next

                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "deptcd IN ('" + sDeptCds.Replace(",", "','") + "')"
                Else
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "deptcd = '" + r_stu.DEPTCD + "'"
                End If
            End If


            dt = Fn.ChangeToDataTable(dt.Select(sWhere, sSort))


            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            'm_dt = Nothing
        End Try

    End Function

    Public Function fnGet_PatInfo_ByNm(ByVal rsPatNm As String, ByVal rsIdnol As String, ByVal rsIdnoR As String) As DataTable

        Dim sFn As String = "fnGet_PatInfo_ByNm"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

#If DEBUG Then
            sURL += PRG_CONST.SERVERIP_DEV
#Else
            sURL += PRG_CONST.SERVERIP 
#End If

            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLII00101&business_id=lis"
            sURL += "&instcd=" + PRG_CONST.SITECD
            sURL += "&patnm=" + rsPatNm
            sURL += "&idnol=" + rsIdnol
            sURL += "&idnor=" + rsIdnoR
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

                Dim dr As DataRow = dt.NewRow()
                Dim sBuf() As String = sRow(ix1).Split(Chr(3))

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            If dt.Rows.Count > 0 Then
                Dim sWhere As String = ""
                If rsIdnol <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "idnol = '" + rsIdnol + "'"
                If rsIdnoR <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "idnor = '" + rsIdnoR + "'"

                dt = Fn.ChangeToDataTable(dt.Select(sWhere))
            End If

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            'If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
            'm_DbCn.Dispose()
        End Try

    End Function

    Public Function fnGet_DeptList(Optional ByVal rsDeptCd As String = "") As DataTable
        Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

#If DEBUG Then
            sURL += PRG_CONST.SERVERIP_DEV
#Else
            sURL += PRG_CONST.SERVERIP 
#End If
            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00103&business_id=lis"
            sURL += "&deptcd=" + rsDeptCd
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

                Dim dr As DataRow = dt.NewRow()
                Dim sBuf() As String = sRow(ix1).Split(Chr(3))

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            If dt.Rows.Count > 0 Then
                Dim sWhere As String = ""
                If rsDeptCd <> "" Then sWhere += "deptcd = '" + rsDeptCd + "'"

                dt = Fn.ChangeToDataTable(dt.Select(sWhere, "deptnm"))

            End If

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            'If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
            'm_DbCn.Dispose()
        End Try

    End Function

    Public Function fnGet_WardList(Optional ByVal rsWardCd As String = "") As DataTable
        Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

        Try
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse

#If DEBUG Then
            sURL += PRG_CONST.SERVERIP_DEV
#Else
            sURL += PRG_CONST.SERVERIP 
#End If
            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TRLIW00102&business_id=lis"
            sURL += "&wardcd=" + rsWardCd
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

                'Dim o_fdinfo() As System.Reflection.FieldInfo = dt.GetType().GetFields()

                For ix2 As Integer = 0 To sBuf.Length - 1
                    dr.Item(ix2) = sBuf(ix2)
                Next

                dt.Rows.Add(dr)

            Next

            If dt.Rows.Count > 0 Then
                Dim sWhere As String = ""
                If rsWardCd <> "" Then sWhere += " AND wardno = '" + rsWardCd + "'"

                dt = Fn.ChangeToDataTable(dt.Select(sWhere, "wardnm"))
            End If

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""

            'If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
            'm_DbCn.Dispose()
        End Try

    End Function

    Public Function ExecuteDo_One(ByVal r_stu As STU_COLLWEB, ByVal rbToColl As Boolean, ByVal rbToTk As Boolean, ByVal pGbn As String) As String
        Dim sFn As String = "Public Function ExecuteDo_One(STU_COLLWEB, Boolean, Boolean) As String"

        Try
            'lisif.pro_ack_exe_ocs_coll
            '
            Dim sURL = ""
            Dim wbReq As Net.WebRequest
            Dim wbRep As Net.WebResponse
            Dim sFkocs As String = ""

#If DEBUG Then
            sURL += PRG_CONST.SERVERIP_DEV
#Else
            sURL += PRG_CONST.SERVERIP
#End If
            sURL = PRG_CONST.SERVERIP
            sURL += "/webapps/com/commonweb/xrw/.live?submit_id=TXLIW00301&business_id=lis"
            sURL += "&regno=" + r_stu.REGNO
            sURL += "&owngbn=" + r_stu.OWNGBN




            For ix As Integer = 0 To r_stu.FKOCS.Split(","c).Length - 1

                If ix > 0 Then
                    sFkocs += ","
                End If

                If r_stu.OWNGBN = "L" Then
                    sFkocs += r_stu.FKOCS.Split(","c)(ix)
                Else
                    sFkocs += r_stu.IOFLAG + "/" + r_stu.REGNO + "/" + r_stu.ORDDT.Substring(0, 8) + "/" + r_stu.FKOCS.Split(","c)(ix)
                End If


            Next

            sURL += "&fkocs=" + sFkocs

            sURL += "&bcno=" + r_stu.BCNO
            sURL += "&ToColl=" + rbToColl.ToString()
            sURL += "&usrid=" + USER_INFO.USRID
            sURL += "&ip=" + USER_INFO.LOCALIP
            sURL += "&orddt=" + r_stu.ORDDT
            sURL += "&ordno=" + r_stu.FKOCS
            sURL += "&testcd=" + r_stu.TCLSCD
            sURL += "&spccd=" + r_stu.SPCCD
            sURL += "&ioflag=" + r_stu.IOFLAG
            sURL += "&colldt=" + r_stu.COLLDT
            sURL += "&ergbn=" + r_stu.STATGBN
            sURL += "&height=" + r_stu.HEIGHT
            sURL += "&weight=" + r_stu.WEIGHT
            sURL += "&diagnm=" + r_stu.DIAGNM
            sURL += "&diagnm_eng=" + r_stu.DIAGNM_ENG
            sURL += "&seqymd=" + r_stu.COLLDT
            sURL += "&seqgbn=" + r_stu.BCCLSCD
            sURL += "&seqno=" + r_stu.SERIES.ToString()
            sURL += "&pgbn=" + pGbn
            sURL += "&rbToTk=" + rbToTk.ToString()


            sURL += "&"

            wbReq = Net.WebRequest.Create(sURL)
            wbRep = wbReq.GetResponse()

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(wbRep.GetResponseStream(), System.Text.Encoding.UTF8)
            Dim sField As String = ""

            Dim sRetVal = fnGet_XmlParsing(sr, sField)

            Dim sBcNo As String = ""

            If sRetVal.StartsWith("00") Then sBcNo = sRetVal.Substring(2, 15)

            Return sBcNo

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function


End Class

