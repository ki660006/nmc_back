﻿'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : 관동대명지병원 Laboratory Information System(KMC_LIS)                  */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_O.vb                                                              */
'/* PartName     : 처방관리                                                               */
'/* Description  : 처방관리의 Data Query구문관련 Class                                    */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN

#Region " 수탁검사 "
Public Class LISAPP_O_UCOST
    Inherits APP_F
    Private Const msFile As String = "File : CGDA_O.vb, Class : DA01.DA_O_UCOST" & vbTab

    Public Function fnGet_testinfo_slip(ByVal rsPartCd As String, ByVal rsSlipCd As String) As DataTable
        Dim sFn As String = " Public Function fnGet_testinfo_slip(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT a.tclscd, a.spccd, a.tcdgbn, a.tnmd, b.ucost"
            sSql += "  FROM lf060m a LEFT OUTER JOIN"
            sSql += "       (SELECT fa.testcd, fa.spccd, fa.ucost"
            sSql += "          FROM lf910m fa, "
            sSql += "               (SELECT testcd, spccd, MAX(usdt) usdt"
            sSql += "                  FROM lf910m"
            sSql += "                 WHERE usdt < SUBSTR(fn_ack_sysdate, 1, 4) + '0101000000'"
            sSql += "                 GROUP BY testcd, spccd"
            sSql += "               ) fb"
            sSql += "         WHERE fa.tclscd = fb.testcd"
            sSql += "           AND fa.spccd = fb.spccd"
            sSql += "           AND fa.usdt = fb.usdt"
            sSql += "       ) b ON (a.testcd = b.testcd AND a.spccd = b.spccd)"
            sSql += " WHERE a.usdt <= fn_ack_sysdate"
            sSql += "   AND a.uedt >  fn_ack_sysdate"
            sSql += "   AND a.sugacd IS NOT NULL"
            If rsPartCd <> "" Then
                sSql += "   AND a.partcd = :partcd"
                al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
            End If

            If rsSlipCd <> "" Then
                sSql += "   AND a.slipcd = :slipcd"
                al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
            End If
            sSql += " ORDER BY a.testcd, a.spccd"

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransUCostInfo_UPD_UE(ByVal rsUsDt As String, ByVal rsRegID As String, ByVal rsUeDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_UPD_UE() As Boolean"

        Try
            Dim sSql As String = ""
            Dim alTest As New ArrayList

            'LF910M : 검사항목별 단가 마스터
            '   LF910H Insert
            sSql = ""
            sSql += " INSERT INTO lf910h"
            sSql += " SELECT fn_ack_sysdate, '" + rsRegID + "', f.* from lf910m f"
            sSql += "  WHERE usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            '   LF910M Update
            sSql = ""
            sSql += " UPDATE lf030m SET"
            sSql += "        uedt = '" + rsUeDtNew + "',"
            sSql += "        regdt = fn_ack_sysdate,"
            sSql += "        regid = '" + rsRegID + "',"
            sSql += "  WHERE usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransUCostInfo_UPD_US(ByVal rsUsDt As String, ByVal rsRegID As String, ByVal rsUsDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_UPD_US() As Boolean"

        Try
            Dim sSql As String = ""
            Dim alTest As New ArrayList

            'LF910M : 검사항목별 단가 마스터
            '   LF910H Insert
            sSql = ""
            sSql += " INSERT INTO lf910h"
            sSql += " SELECT fn_ack_sysdate, '" + rsRegID + "', f.* from lf910m f"
            sSql += "  WHERE usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            '   LF910M Update
            sSql = ""
            sSql += " UPDATE lf910m SET"
            sSql += "        usdt = '" + rsUsDtNew + "',"
            sSql += "        regdt = fn_ack_sysdate,"
            sSql += "        regid = '" + rsRegID + "'"
            sSql += "  WHERE usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransUCostInfo_DEL(ByVal rsUsDt As String, ByVal rsRegID As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_DEL() As Boolean"

        Try
            Dim sSql As String = ""
            Dim alTest As New ArrayList

            'LF910M : 검사항목별 단가 마스터
            '   LF910H Insert
            sSql = ""
            sSql += " INSERT INTO lf910h"
            sSql += " SELECT fn_ack_sysdate, '" + rsRegID + "', f.* from lF910m f"
            sSql += "  WHERE usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            '   LF910M Delete
            sSql = ""
            sSql += " DELETE FROM lf910m"
            sSql += "  WHERE usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetUsUeCd_UCost(ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_UCost() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += " SELECT tclscd, spccd"
            sSql += "   FROM lj011m"
            sSql += "  WHERE iogbn NOT IN ('I', 'O', 'Z')"
            sSql += "    AND colldt BETWEEN :usdt AND :usdt || '1231235959'"
            sSql += "    AND ROWNUM = 1"

            al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, 4, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt.Replace("-", "").Substring(0, 4)))
            al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, 4, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt.Replace("-", "").Substring(0, 4)))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetUsUeDupl_UCost(ByVal rsUsDt As String, ByVal rsUseTag As String, ByVal rsCompDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeDupl_UCost() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += " SELECT a.*"
            sSql += "   FROM ("
            sSql += "         SELECT usdt, uedt"
            sSql += "           FROM lf910m"
            sSql += "          WHERE usdt <" + IIf(rsUseTag = "USDT", "=", "").ToString + " :compdt"
            sSql += "            AND uedt >" + IIf(rsUseTag = "USDT", "", "=").ToString + " :compdt"
            sSql += "        ) a LEFT OUTER JOIN"
            sSql += "        ("
            sSql += "         SELECT usdt, uedt"
            sSql += "           FROM lf910m"
            sSql += "          WHERE usdt = :usdt"
            sSql += "        ) b"
            sSql += "        ON (a.usdt = b.usdt)"
            sSql += "  WHERE b.usdt IS NULL"

            al.Add(New OracleParameter("compdt", OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("compdt", OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetUCostInfo(ByVal iMode As Integer) As DataTable
        Dim sFn As String = "Public Overloads Function GetUCostInfo(ByVal iMode As Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If iMode = 0 Then
                sSql += " SELECT DISTINCT fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdtd, usdt, uedt"
                sSql += "   FROM lf910m"
                sSql += "  WHERE uedt >= fn_ack_sysdate"
                sSql += "  ORDER BY usdt"
            ElseIf iMode = 1 Then
                sSql += " SELECT DISTINCT fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdtd, usdt, uedt, uedt - sysdate diffday"
                sSql += "   FROM lf910m"
                sSql += "  ORDER BY usdt"
            End If

            DbCommand()
            GetUCostInfo = DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Overloads Function GetUCostInfo(ByVal riMode As Integer, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetUCostInfo(ByVal iMode As Integer,  ByVal asUSDT As String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            rsUSDT = rsUSDT.Replace("-", "").Replace(" ", "").Replace(":", "")

            If riMode = 1 Then
                sSql += " SELECT a.testcd, a.spccd, b.tcdgbn, b.tnmd, a.ucost,"
                sSql += "        fn_ack_date_str(a.usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
                sSql += "        fn_ack_date_str(a.uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
                sSql += "        fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid"
                sSql += "   FROM lf910m a, lf060m b"
                sSql += "  WHERE a.usdt = :usdt"
                sSql += "    AND a.testcd = b.testcd"
                sSql += "    AND a.spccd  = b.spccd"
                sSql += "    AND a.usdt  >= b.usdt"
                sSql += "    AND a.usdt  <  b.uedt"

                alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUSDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUSDT))
            End If

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetRecentUCostInfo(ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetRecentUCostInfo(ByVal asUSDT As String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            rsUSDT = rsUSDT.Replace("-", "").Replace(" ", "").Replace(":", "")

            sSql += " SELECT usdt"
            sSql += "   FROM (SELECT usdt"
            sSql += "           FROM lf910m"
            sSql += "          WHERE usdt >= :usdt"
            sSql += "          ORDER BY usdt DESC"
            sSql += "        ) a"
            sSql += "  WHERE ROWNUM = 1"

            alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            GetRecentUCostInfo = DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransUCostInfo(ByVal rITcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                   ByVal rsUSDT As String, ByVal rsRegID As String) As Boolean
        Dim sFn As String = "Public Function TransUCostInfo() As Boolean"

        Try
            Dim sSql_ins As String = "", sSql_up As String = "", sSql_Del As String = ""
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""
            Dim alTest As New ArrayList

            'LF910M : 검사항목별 단가
            Select Case riType1
                Case 0      '----- 신규
                    With rITcol1
                        'update uedt of previous record
                        sSql_up = ""
                        sSql_up += " UPDATE lf910m SET uedt = '" + rsUSDT + "'"
                        sSql_up += "  WHERE usdt IN (SELECT a.usdt"
                        sSql_up += "                   FROM (SELECT usdt as usdt"
                        sSql_up += " 					       FROM lf910m"
                        sSql_up += " 						  WHERE usdt <= '" + rsUSDT + "'"
                        sSql_up += " 							AND uedt >  '" + rsUSDT + "'"
                        sSql_up += " 						  ORDER BY usdt DESC"
                        sSql_up += "                        ) a"
                        sSql_up += "                  WHERE ROWNUM = 1"
                        sSql_up += "                )"

                        alTest.Add(sSql_up)

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = "'" + CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value + "'"
                                sValues += sValue + ","
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql_ins = "INSERT INTO lf910m (" + sFields + ") VALUES (" + sValues + ")"

                            alTest.Add(sSql_ins)
                        Next
                    End With

                Case 1      '----- 수정
                    With rITcol1
                        'LF030H Backup
                        sSql_ins = ""
                        sSql_ins += " INSERT INTO lf910h SELECT fn_ack_sysdate, '" + rsRegID + "', f.* FROM lf910m f"
                        sSql_ins += " WHERE usdt = '" + rsUSDT + "'"

                        alTest.Add(sSql_ins)

                        sSql_Del = ""
                        sSql_Del += " DELETE FROM lf910m WHERE usdt = '" + rsUSDT + "'"

                        alTest.Add(sSql_Del)

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = "'" + CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value + "'"
                                sValues += sValue + ","
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql_ins = "INSERT INTO lf910m (" + sFields + ") VALUES (" + sValues + ")"

                            alTest.Add(sSql_ins)
                        Next
                    End With
            End Select

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransUCostInfo_UE(ByVal asUSDT As String, ByVal asUEDT As String, ByVal asRegID As String) As Boolean
        Dim sFn As String = " Public Function TransUCostInfo_UE(ByVal asUSDT As String, byval asUEDT as string, ByVal asRegID As String) As Boolean"

        Try
            Dim sSql_ins As String = "", sSql_up As String = "", sSql_Del As String = ""
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = "", sMsg As String = ""
            Dim alTest As New ArrayList

            sMsg = ifExistOtherUsableData("lf910", "USDT", asUSDT, asUSDT)

            If IsNothing(sMsg) Then
                MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
                Exit Function
            End If

            If Not sMsg = "" Then
                MsgBox(sMsg, MsgBoxStyle.Critical)
                Exit Function
            End If

            'LF910M : 검사항목별 단가
            '   LF910H Insert
            sSql_ins = ""
            sSql_ins += " INSERT INTO lf910h"
            sSql_ins += " SELECT fn_ack_sysdate, '" + asRegID + "', f.* FROM lf910m f"
            sSql_ins += "  WHERE usdt = '" + asUSDT + "'"
            alTest.Add(sSql_ins)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransUCostInfo_UE(ByVal asUSDT As String, ByVal asRegID As String) As Boolean
        Dim sFn As String = " Public Function TransUCostInfo_UE(ByVal asUSDT As String, byval asUEDT as string, ByVal asRegID As String) As Boolean"

        Try
            Dim sSql_ins As String = "", sSql_up As String = "", sSql_Del As String = ""
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = "", sMsg As String = ""
            Dim alTest As New ArrayList

            'sMsg = ifExistOtherUsableData("LF910", "USDT", asUSDT, asUSDT)

            'If IsNothing(sMsg) Then
            '    MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
            '    Exit Function
            'End If

            'If Not sMsg = "" Then
            '    MsgBox(sMsg, MsgBoxStyle.Critical)
            '    Exit Function
            'End If

            'LF910M : 검사항목별 단가
            '   LF910H Insert
            sSql_ins = " INSERT INTO lf910h"
            sSql_ins += " SELECT fn_ack_sysdate, '" + asRegID + "', f.* FROM lf910m f"
            sSql_ins += "  WHERE usdt = TO_DATE('" + asUSDT + "', 'yyyy-mm-dd hh24:mi:ss')"
            alTest.Add(sSql_ins)

            '   LF910M Update
            sSql_up = " UPDATE lf910m SET uedt = '" + asUSDT + "', regid = '" + asRegID + "'"
            sSql_up += " WHERE usdt = '" + asUSDT + "'"
            alTest.Add(sSql_up)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function
End Class

Public Class LISAPP_O_CUST_ORD
    Private Const msFile As String = "File : CGDA_O.vb, Class : DA01.DA_O_CUST_ORD" & vbTab

    Public Function fnGet_Cust_List(ByVal rsCustCd As String, ByVal rsDateS As String, ByVal rsDateE As String) As DataTable

        Dim sFn As String = "fnGet_Cust_List() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT f.sugacd, f.tnmp, count(j11.tclscd) tcnt, s.fee danga, count(j11.tclscd) * s.fee cost_t,"
            sSql += "       NVL(dispseql, 999) sortl, f.bcclscd"
            sSql += "  FROM lj010m j, lj011m j11, lf060m f, ocs_db..v_feecodes_for_ack s"
            sSql += " WHERE j.orddt      >= :dates || '000000'"
            sSql += "   AND j.orddt      <= :datee || '235959'"
            sSql += "   AND j.spcflg     IN ('1', '2')"
            sSql += "   AND j.deptcd      = :deptcd"
            sSql += "   AND j.bcno        = j11.bcno"
            sSql += "   AND j.spccd       = j11.spccd"
            sSql += "   AND j11.tclscd    = f.tclscd"
            sSql += "   AND j11.spccd     = f.spccd"
            sSql += "   AND f.sugacd      = s.feecode"
            sSql += "   AND s.useflag     = '1'"
            sSql += "   AND s.lastfeeflag = '1'"
            sSql += " GROUP BY f.sugacd, f.tnmp, s.fee, NVL(dispseql, 999), f.bcclscd"
            sSql += " UNION "
            sSql += "SELECT f.sugacd, f.comnmp tnmp, count(j11.comcd) tcnt, s.fee danga, count(j11.comcd) * s.fee cost_t,"
            sSql += "       NVL(dispseql, 999) sortl, 'ZZ' bcclscd"
            sSql += "  FROM lb040m j, lb043m j11, lf120m f, ocs_db..v_feecodes_for_ack s"
            sSql += " WHERE j.orddt      >= :dates || '000000'"
            sSql += "   AND j.orddt      <= :datee || '235959'"
            sSql += "   AND j.deptcd      = :deptcd"
            sSql += "   AND j.delflg      = '0'"
            sSql += "   AND j.tnsjubsuno  = j11.tnsjubsuno"
            sSql += "   AND j11.comcd     = f.comcd"
            sSql += "   AND j11.spccd     = f.spccd"
            sSql += "   AND f.sugacd      = s.feecode"
            sSql += "   AND s.useflag     = '1'"
            sSql += "   AND s.lastfeeflag = '1'"
            sSql += " GROUP BY f.sugacd, f.comnmp, s.fee, NVL(dispseql, 999)"


            sSql += " ORDER BY bcclscd, sortl, tnmp"

            al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
            al.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsCustCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCustCd))

            al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
            al.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsCustCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCustCd))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function fnGet_Cust_PatList(ByVal rsCustCd As String, ByVal rsDateS As String, ByVal rsDateE As String) As DataTable

        Dim sFn As String = "fnGet_Cust_PatList() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt, j.regno, j.hregno,"
            sSql += "       j.patnm, f.sugacd, f.tnmp, s.fee danga,"
            sSql += "       NVL(f.dispseql, 999) sortl, f.bcclscd, j.bcno"
            sSql += "  FROM lj010m j, lj011m j11, lf060m f, ocs_db..v_feecodes_for_ack s"
            sSql += " WHERE j.orddt      >= :dates || '000000'"
            sSql += "   AND j.orddt      <= :datee || '235959'"
            sSql += "   AND j.spcflg     IN ('1', '2')"
            sSql += "   AND j.deptcd      = :deptcd"
            sSql += "   AND j.bcno        = j11.bcno"
            sSql += "   AND j.spccd       = j11.spccd"
            sSql += "   AND j11.tclscd    = f.tclscd"
            sSql += "   AND j11.spccd     = f.spccd"
            sSql += "   AND f.sugacd      = s.feecode"
            sSql += "   AND s.useflag     = '1'"
            sSql += "   AND s.lastfeeflag = '1'"
            sSql += " UNION "
            sSql += "SELECT fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt, j.regno, j.hregno,"
            sSql += "       j.patnm, f.sugacd, f.comnmp tnmp, s.fee danga,"
            sSql += "       NVL(f.dispseql, 999) sortl, 'ZZ' bcclscd, j.tnsjubsuno bcno"
            sSql += "  FROM lb040m j, lb043m j11, lf120m f, ocs.v_feecodes_for_ack s"
            sSql += " WHERE j.orddt     >= :dates || '000000'"
            sSql += "   AND j.orddt     <= :datee || '235959'"
            sSql += "   AND j.deptcd      = :deptcd"
            sSql += "   AND j.delflg      = '0'"
            sSql += "   AND j.tnsjubsuno  = j11.tnsjubsuno"
            sSql += "   AND j11.comcd     = f.comcd"
            sSql += "   AND j11.spccd     = f.spccd"
            sSql += "   AND f.sugacd      = s.feecode"
            sSql += "   AND s.useflag     = '1'"
            sSql += "   AND s.lastfeeflag = '1'"
            sSql += " ORDER BY orddt, patnm, regno, bcclscd, sortl"

            al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
            al.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsCustCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCustCd))

            al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
            al.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsCustCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCustCd))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function fnGet_PatInfo(ByVal rsCustCd As String, ByVal rsRegNo As String) As DataTable
        Dim sFn As String = "fnGet_PatInfo() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT '|' liscmt, patnm, fn_ack_date_str(birth, 'yyyy-mm-dd') birth, idnol, idnor, sex,"
            sSql += "       tel1, tel2, zipno zipno1, address address1, patgbn, foreginyn, regno custregno, null remark"
            sSql += "  FROM mts0004_lis"
            sSql += " WHERE custcd = :custcd"
            sSql += "   AND regno  = :regno"

            al.Add(New OracleParameter("custcd", OracleDbType.Varchar2, rsCustCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCustCd))
            al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function fnGet_JubsuInfo(ByVal rsOrdDt As String, ByVal rsRegNo As String) As DataTable
        Dim sFn As String = "fnGet_JubsuInfo() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            rsOrdDt = rsOrdDt.Replace("-", "")

            sSql += "SELECT DISTINCT"
            sSql += "       o.remark, p.suname patnm, fn_ack_get_date_string(p.birth, 'yyyy-mm-dd') birth, p.sujumin1 idnol, p.sujumin2 idnor, p.sex,"
            sSql += "       p.tel1, p.tel2, p.zip_code1 zipno1, p.address1, c.patgbn, c.foreginyn, c.regno custregno, o.fkocs,"
            sSql += "       f6.tnmd, f3.spcnmd, f6.testcd, f6.spccd, f6.sugacd, f6.insugbn, f6.bcclscd,"
            sSql += "       f6.minspcvol, f6.tordcd, f6.tcdgbn, f6.tclscd||f6.spccd tcd, o.slip_gubun, f6.filter, f6.comgbn"
            sSql += "  FROM mts0004_lis c, mts0001_lis o,"
            sSql += "       (SELECT 1 seq, patno bunho, patnm suname, fn_ack_date_str(birtdate, 'yyyy-mm-dd') birth,"
            sSql += "               resno1 sujumin1, resno2 sujumin2, sex, telno1 tel1, telno2 tel2, zipcd zip_code1, address1"
            sSql += "          FROM vw_ack_ocs_pat_info"
            sSql += "         WHERE patno  = :regno"
            sSql += "           AND instcd = '" + COMMON.CommLogin.LOGIN.PRG_CONST.SITECD + "'"
            sSql += "         UNION "
            sSql += "        SELECT 2 seq, bunho, suname, birth, sujumin1, sujumin2, sex, tel1, tel2, zip_code1, address1 FROM mts0002_lis"
            sSql += "         WHERE bunho = :regno"
            sSql += "         ORDER BY seq"
            sSql += "       ) p,"
            sSql += "       (SELECT testcd, spccd, usdt, uedt, tcdgbn, tnmd, sugacd, insugbn,"
            sSql += "               bcclscd, minspcvol, tordcd, '' filter, '' comgbn"
            sSql += "          FROM lf060m"
            sSql += "         UNION"
            sSql += "        SELECT comcd testcd, spccd, usdt, uedt, 'S' tcdgbn, comnmd tnmd, sugacd, null insugbn,"
            sSql += "               null bcclscd, null minspcvol, comcdo trodcd,"
            sSql += "               CASE WHEN NVL(ftcd, '000') = '000' THEN '' ELSE '○'END filter, comgbn"
            sSql += "          FROM lf120m"
            sSql += "       ) f6, lf030m f3"
            sSql += " WHERE o.bunho      = :regno"
            sSql += "   AND o.order_date = :orddt"
            sSql += "   AND o.in_out_gubun = 'C'"
            sSql += "   AND NVL(dc_yn, 'N') = 'N'"
            sSql += "   AND o.bunho = p.bunho"
            sSql += "   AND o.gwa = c.custcd"
            sSql += "   AND o.req_remark = c.regno"
            sSql += "   AND o.hangmog_code = f6.tordcd"
            sSql += "   AND o.specimen_code = f6.spccd"
            sSql += "   AND o.order_date >= f6.usdt"
            sSql += "   AND o.order_date < f6.uedt"
            sSql += "   AND o.specimen_code = f3.spccd"
            sSql += "   AND o.order_date >= f3.usdt"
            sSql += "   AND o.order_date < f3.uedt"
            sSql += " ORDER BY fkocs"

            al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            al.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function fnGet_CustList() As DataTable
        Dim sFn As String = "fnGet_CustList() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT  '[' || custcd || '] ' ||  custnm cust"
            sSql += "  FROM lf920m"
            sSql += " WHERE usdt <= fn_ack_sysdate"
            sSql += "   AND uedt >  fn_ack_sysdate"

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function



    Public Function fnGet_OrderList(ByVal rsOrdDt As String, ByVal rsCustCd As String) As DataTable
        Dim sFn As String = "fnGet_OrderList() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       fn_ack_get_dept_abbr(o.in_out_gubun, o.gwa) custnm, fn_ack_get_pat_info(o.bunho, '', '') patinfo,"
            sSql += "       remark_req cregno, fn_ack_date_str(o.order_date, 'yyyy-mm-dd') orddt, o.bunho regno"
            sSql += "  FROM mts0001_lis o"
            sSql += " WHERE o.order_date   = :orddt"
            sSql += "   AND o.in_out_gubun = 'C'"
            sSql += "   AND o.gwa          = :deptcd"
            sSql += "   AND NVL(o.dc_yn, 'N') = 'N'"
            sSql += " ORDER BY regno"

            rsOrdDt = rsOrdDt.Replace("-", "")

            al.Clear()
            al.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))
            al.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsCustCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCustCd))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

End Class

Public Class LISAP_O_CUST
    Inherits APP_F

    Private Const msFile As String = "File : CGDA_O.vb, Class : DA01.DA_O_CUST" & vbTab

    Public Shared Function fnExe_Change_HRegNo(ByVal rsRegno As String, ByVal rsHRegNo As String) As Integer
        Dim sFn As String = "Public Function fnExe_Change_HRegNo(string) As String"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Dim iRet As Integer = 0

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
                .CommandText = "UPDATE lj010m SET hregno = :hregno WHERE regno = :regno"

                .Parameters.Clear()
                .Parameters.Add("hregno", OracleDbType.Varchar2).Value = rsHRegNo
                .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegno

                iRet = .ExecuteNonQuery()

                .CommandText = "UPDATE lb040m SET hregno = :hregno WHERE regno = :regno"

                .Parameters.Clear()
                .Parameters.Add("hregno", OracleDbType.Varchar2).Value = rsHRegNo
                .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegno

                iRet += .ExecuteNonQuery()

            End With

            dbTran.Commit()

            Return iRet


        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        Finally
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Function TransCustInfo_UPD_UE(ByVal rsCode As String, ByVal rsUsDt As String, ByVal rsRegID As String, ByVal rsUeDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransCustInfo_UPD_UE() As Boolean"

        Try
            Dim sSql As String = ""
            Dim alTest As New ArrayList

            'LF920M : 거래처코드
            '   LF920H Insert
            sSql = ""
            sSql += " INSERT INTO lf920h"
            sSql += " SELECT fn_ack_sysdate, '" + rsRegID + "', f.* FROM lf920m f"
            sSql += "  WHERE custcd = '" + rsCode + "'"
            sSql += "    AND usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            '   LF920M Update
            sSql = ""
            sSql += " UPDATE lf920m SET"
            sSql += "        uedt = '" + rsUeDtNew + "',"
            sSql += "        regdt = fn_ack_sysdate,"
            sSql += "        regid = '" + rsRegID + "',"
            sSql += "  WHERE custcd = '" + rsCode + "'"
            sSql += "    AND usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransCustInfo_UPD_US(ByVal rsCode As String, ByVal rsUsDt As String, ByVal rsRegID As String, ByVal rsUsDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_UPD_US() As Boolean"

        Try
            Dim sSql As String = ""
            Dim alTest As New ArrayList

            'LF920M : 거래처코드
            '   LF920H Insert
            sSql = ""
            sSql += " INSERT INTO lf920h"
            sSql += " SELECT fn_ack_sysdate, '" + rsRegID + "', f.* FROM lf920m f"
            sSql += "  WHERE custcd = '" + rsCode + "'"
            sSql += "    AND usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            '   LF920M Update
            sSql = ""
            sSql += " UPDATE lf920m SET"
            sSql += "        usdt = '" + rsUsDtNew + "',"
            sSql += "        regdt = fn_ack_sysdate,"
            sSql += "        regid = '" + rsRegID + "',"
            sSql += "  WHERE custcd = '" + rsCode + "'"
            sSql += "    AND usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransCustInfo_DEL(ByVal rsCode As String, ByVal rsUsDt As String, ByVal rsRegID As String) As Boolean
        Dim sFn As String = " Public Function TransCustInfo_DEL() As Boolean"

        Try
            Dim sSql As String = ""
            Dim alTest As New ArrayList

            'LF920M : 거래처코드
            '   LF920H Insert
            sSql = ""
            sSql += " INSERT INTO lf920h"
            sSql += " SELECT fn_ack_sysdate, '" + rsRegID + "', f.* FROM lf920m f"
            sSql += "  WHERE custcd = '" + rsCode + "'"
            sSql += "    AND usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            '   LF920M Update
            sSql = ""
            sSql += " DELETE lf920m"
            sSql += "  WHERE custcd = '" + rsCode + "'"
            sSql += "    AND usdt = '" + rsUsDt + "'"
            alTest.Add(sSql)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetUsUeCd_Cust(ByVal rsIoGbn As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_Cust() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += " SELECT tclscd, spccd"
            sSql += "   FROM lj011m"
            sSql += "  WHERE iogbn  = :iogbn"
            sSql += "    AND colldt BETWEEN :usdt AND :usdt || '1231235959'"
            sSql += "    AND ROWNUM = 1"

            al.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))
            al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, 4, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, 4, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetUsUeDupl_Cust(ByVal rsCd As String, ByVal rsUsDt As String, ByVal rsUseTag As String, ByVal rsCompDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeDupl_Cust() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += " SELECT a.*"
            sSql += "   FROM ("
            sSql += "         SELECT custcd, custnm, usdt, uedt"
            sSql += "           FROM lf920m"
            sSql += "          WHERE custcd = :custcd"
            sSql += "            AND usdt <" + IIf(rsUseTag = "USDT", "=", "").ToString + " :compdt"
            sSql += "            AND uedt >" + IIf(rsUseTag = "USDT", "", "=").ToString + " :compdt"
            sSql += "        ) a LEFT OUTER JOIN"
            sSql += "        ("
            sSql += "         SELECT custcd, custnm, usdt, uedt"
            sSql += "           FROM lf920m"
            sSql += "          WHERE custcd = :custcd"
            sSql += "            AND usdt   = :usdt"
            sSql += "        ) b ON a.custcd = b.custcd AND a.usdt = b.usdt"
            sSql += "  WHERE b.custcd IS NULL"

            al.Add(New OracleParameter("custcd", OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("compcd", OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("compcd", OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("custcd", OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Overloads Function GetCustInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Overloads Function GetCustInfo(ByVal iMode As Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += " SELECT custcd, custnm, telno, address, custdc, usdt, uedt"
                sSql += "   FROM lf920m"
                sSql += "  WHERE uedt >= fn_ack_sysdate"
                sSql += "  ORDER BY custcd"
            ElseIf riMode = 1 Then
                sSql += " SELECT custcd, custnm, telno, address, custdc, usdt, uedt, TO_DATE(uedt, 'yyyymmddhh24miss') - SYSDATE diffday"
                sSql += "   FROM lf920m"
                sSql += "  ORDER BY custcd"
            End If

            DbCommand()
            GetCustInfo = DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Overloads Function GetCustInfo(ByVal riMode As Integer, ByVal rsCustCd As String, ByVal rsUSDT As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetCustInfo(Integer, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 1 Then
                sSql += " SELECT custcd, custnm, telno, address, custdc,"
                sSql += "        fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
                sSql += "        fn_ack_date_str(uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
                sSql += "        fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid"
                sSql += "   FROM lf920m "
                sSql += "  WHERE custcd = '" + rsCustCd + "' "
                sSql += "    AND usdt   = '" + rsUSDT + "'"
            End If

            DbCommand()
            GetCustInfo = DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetRecentCustInfo(ByVal rsCustCd As String, ByVal rsUSDT As String) As DataTable
        Dim sFn As String = "Public Function GetRecentCustInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += " SELECT usdt"
            sSql += "   FROM (SELECT usdt"
            sSql += "           FROM lf920m"
            sSql += "          WHERE custcd = '" + rsCustCd + "'"
            sSql += "            AND usdt  >= '" + rsUSDT + "'"
            sSql += "          ORDER BY usdt DESC"
            sSql += "        ) a"
            sSql += "  WHERE ROWNUM = 1"

            DbCommand()
            GetRecentCustInfo = DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransCustInfo(ByVal aITcol1 As ItemTableCollection, ByVal aiType1 As Integer, _
                                  ByVal asCustCd As String, ByVal asUSDT As String, ByVal asRegID As String) As Boolean
        Dim sFn As String = "Public Function TransSpcInfo() As Boolean"

        Try
            Dim sSql_ins As String = "", sSql_up As String = "", sSql_Del As String = ""
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""
            Dim alTest As New ArrayList

            'LF920M : 거래처
            Select Case aiType1
                Case 0      '----- 신규
                    With aITcol1
                        'update uedt of previous record
                        sSql_up = ""
                        sSql_up += " UPDATE lf920m set uedt = '" + asUSDT + "'"
                        sSql_up += "  WHERE (custcd, usdt) IN (SELECT a.custcd, a.usdt"
                        sSql_up += "                             FROM (SELECT custcd as custcd, usdt as usdt"
                        sSql_up += " 						 		     FROM lf920m"
                        sSql_up += " 							        WHERE custcd = '" + asCustCd + "'"
                        sSql_up += " 									  AND usdt  <= '" + asUSDT + "'"
                        sSql_up += " 									  AND uedt  >  '" + asUSDT + "'"
                        sSql_up += " 								    ORDER BY usdt DESC"
                        sSql_up += "                                  ) a"
                        sSql_up += "                            WHERE ROWNUM = 1"
                        sSql_up += " 						  )"

                        alTest.Add(sSql_up)

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = "'" + CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value + "'"
                                sValues += sValue + ","
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql_ins = "INSERT INTO lf920m (" + sFields + ") VALUES (" + sValues + ")"

                            alTest.Add(sSql_ins)
                        Next
                    End With

                Case 1      '----- 수정
                    With aITcol1
                        'LF920H Backup
                        sSql_ins = " INSERT INTO lf920h SELECT fn_ack_sysdate, '" + asRegID + "', f.* FROM lf920m f"
                        sSql_ins += " WHERE custcd = '" + asCustCd + "'"
                        sSql_ins += "   AND usdt = to_date('" + asUSDT + "', 'yyyy-mm-dd hh24:mi:ss')"

                        alTest.Add(sSql_ins)

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = "'" + CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value + "'"

                                Select Case sField.ToUpper
                                    Case "CUSTCD", "USDT"

                                    Case Else
                                        sFields += sField + " = " + sValue + ","
                                End Select
                            Next

                            'update record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sSql_up = "UPDATE lf920m SET " + sFields
                            sSql_up &= " WHERE custcd = '" + asCustCd + "'"
                            sSql_up &= "   AND usdt = '" + asUSDT + "'"

                            alTest.Add(sSql_up)
                        Next
                    End With
            End Select

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransCustInfo_UE(ByVal asCustCd As String, ByVal asUSDT As String, ByVal asUEDT As String, ByVal asRegID As String) As Boolean
        Dim sFn As String = " Public Function TransCustInfo_UE(String, String, String, String) As Boolean"

        Try
            Dim sSql_ins As String = "", sSql_up As String = "", sSql_Del As String = ""
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = "", sMsg As String = ""
            Dim alTest As New ArrayList

            sMsg = ifExistOtherUsableData("lf030", "CUSTCD", asCustCd, asUSDT)

            If IsNothing(sMsg) Then
                MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
                Exit Function
            End If

            If Not sMsg = "" Then
                MsgBox(sMsg, MsgBoxStyle.Critical)
                Exit Function
            End If

            'LF920M : 거래처 마스터
            '   LF920H Insert
            sSql_ins = " INSERT INTO lf920h SELECT fn_ack_sysdate, '" + asCustCd + "', f.* FROM lf920m f"
            sSql_ins += " WHERE custcd = '" + asCustCd + "'"
            sSql_ins += "   AND usdt = '" & asUSDT & "'"
            alTest.Add(sSql_ins)

            '   LF920M Update
            sSql_up = " UPDATE lf920m SET uedt = '" + asUSDT + "', regid = '" + asRegID + "'"
            sSql_up += " WHERE custcd = '" + asCustCd + "'"
            sSql_up += "   AND usdt = '" + asUSDT + "'"
            alTest.Add(sSql_up)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransCustInfo_UE(ByVal asCustCd As String, ByVal asUSDT As String, ByVal asRegID As String) As Boolean
        Dim sFn As String = " Public Function TransCustInfo_UE(String, String, String) As Boolean"

        Try
            Dim sSql_ins As String = "", sSql_up As String = "", sSql_Del As String = ""
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = "", sMsg As String = ""
            Dim alTest As New ArrayList

            sMsg = ifExistOtherUsableData("lf920M", "CUSTCD", asCustCd, asUSDT)

            If IsNothing(sMsg) Then
                MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
                Exit Function
            End If

            If Not sMsg = "" Then
                MsgBox(sMsg, MsgBoxStyle.Critical)
                Exit Function
            End If

            'LF920M : 거래처 마스터
            '   LF920H Insert
            sSql_ins = " INSERT INTO lf920h"
            sSql_ins += " SELECT fn_ack_sysdate, '" + asRegID + "', f.* FROM lf920m f"
            sSql_ins += "  WHERE custcd = '" + asCustCd + "'"
            sSql_ins += "    AND usdt = '" + asUSDT + "'"
            alTest.Add(sSql_ins)

            '   LF920M Update
            sSql_up = " UPDATE lf920m SET uedt = '" + asUSDT + "', regid = '" + asRegID + "'"
            sSql_up += " WHERE custcd = '" + asCustCd + "'"
            sSql_up += "   AND usdt = '" + asUSDT + "'"
            alTest.Add(sSql_up)

            If LISAPP.APP_DB.DBSql.ExcuteSql(alTest) = False Then
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

#End Region


Namespace APP_O
    Public Class OrdFn
        Private Const msFile As String = "File : CGLISAPP_0.vb, Class : LISAPP.APP_O.O01" + vbTab

#Region " 처방내역 조회 "
        Public Shared Function fnGet_Order_Info(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsRegNo As String, ByVal rbComCd As Boolean) As DataTable
            Dim sFn As String = "GetMTSList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = "pkg_ack_coll.pkg_get_order_info"

                alParm.Add(New OracleParameter("rs_regno", rsRegNo))
                alParm.Add(New OracleParameter("rs_orddt1", rsDateS))
                alParm.Add(New OracleParameter("rs_orddt2", rsDateE))
                alParm.Add(New OracleParameter("rs_bldyn", IIf(rbComCd, "Y", "").ToString))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Sub fnExe_Order_Status(ByVal rsOwnGbn As String, ByVal rsIoGbn As String, ByVal rsFkOcs As String, ByVal rsSpcFlg As String)
            Dim sFn As String = "fnExe_Order_Status"

            If rsFkOcs = "" Then Return

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rsOwnGbn = "L" Then
                    sSql = ""
                    sSql += "UPDATE mts0001_lis"
                    sSql += "   SET spcflg = '" + rsSpcFlg + "'"
                    sSql += " WHERE fkocs  = '" + rsFkOcs + "'"

                    DbCommand()
                    DbExecute(sSql, True)
                Else

                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Public Shared Sub fnExe_Order_dcyn(ByVal rsOwnGbn As String, ByVal rsIoGbn As String, ByVal rsFkOcs As String)
            Dim sFn As String = "fnExe_Order_dcyn"

            If rsFkOcs = "" Then Return

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rsOwnGbn = "L" Then
                    sSql = ""
                    sSql += "UPDATE mts0001_lis"
                    sSql += "   SET dc_yn  = 'Y'"
                    sSql += " WHERE fkocs  = '" + rsFkOcs + "'"

                    DbCommand()
                    DbExecute(sSql, True)
                Else

                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub
#End Region

    End Class

#Region " DB_MTS_Order MTS에 삽입: Class DB_MTS_Order "
    Public Class DB_MTS_Order
        Private Const msFile As String = "File : CGDA_0.vb, Class : DA01.DB_MTS_Order" & vbTab

        Private msORDER_TABLE_Nm As String
        Private msPat_TABLE_Nm As String

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Private msTime As String = ""

        Public Sub New()
            msTime = (New LISAPP.APP_DB.ServerDateTime).GetTime24("")

            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Public Sub New(ByVal r_dbCn As OracleConnection, ByVal r_dbTran As OracleTransaction)
            msTime = (New LISAPP.APP_DB.ServerDateTime).GetTime24("")

            m_dbCn = r_dbCn
            m_dbTran = r_dbTran
        End Sub

        Private Function fnGet_FKOCS(ByVal rsDate As String, ByVal rsGbn As String) As String
            Dim sFn As String = "Public Function fnGet_FKOCS(String, String) As String"
            Dim dbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            Dim strErrVal As String = ""

            Try

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sValue As String = ""

                sSql = ""
                sSql += "UPDATE ln010m SET seqno = seqno + 1"
                sSql += " WHERE seqymd = :seqymd"
                sSql += "   AND seqgbn = :seqgbn"
                sSql += "   AND jobgbn = :jobgbn"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("seqymd", OracleDbType.Varchar2).Value = rsDate
                    .Parameters.Add("seqgbn", OracleDbType.Varchar2).Value = rsGbn
                    .Parameters.Add("jobgbn", OracleDbType.Varchar2).Value = "9"

                    iRet = .ExecuteNonQuery()

                End With

                If iRet = 0 Then
                    sSql = ""
                    sSql += "INSERT INTO ln010m (seqymd, seqgbn, seqno, jobgbn) VALUES (:seqymd, :seqgbn, 1, :jobgbn)"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("seqymd", OracleDbType.Varchar2).Value = rsDate
                        .Parameters.Add("seqgbn", OracleDbType.Varchar2).Value = rsGbn
                        .Parameters.Add("jobgbn", OracleDbType.Varchar2).Value = "9"
                        .ExecuteNonQuery()

                    End With

                    sValue = "1"
                Else
                    sSql = ""
                    sSql += "SELECT seqno FROM ln010m"
                    sSql += " WHERE seqymd = :seqymd"
                    sSql += "   AND seqgbn = :seqgbn"
                    sSql += "   AND jobgbn = :jobgbn"

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbDa = New OracleDataAdapter(dbCmd)

                    With dbDa
                        .SelectCommand.Parameters.Clear()
                        .SelectCommand.Parameters.Add("seqymd", OracleDbType.Varchar2).Value = rsDate
                        .SelectCommand.Parameters.Add("seqgbn", OracleDbType.Varchar2).Value = rsGbn
                        .SelectCommand.Parameters.Add("jobgbn", OracleDbType.Varchar2).Value = "9"
                    End With

                    dt.Reset()
                    dbDa.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        sValue = dt.Rows(0).Item("seqno").ToString
                    Else
                        sValue = ""
                    End If
                End If

                Return sValue

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnExe_Mts0001(ByVal raOrdList As ArrayList, ByVal rsUsrId As String) As String
            Dim sFn As String = "fnExe_Mts0001() as boolean"
            Dim dbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim sFkOcs As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                ' 검사항목 Query Script 생성
                For ix As Integer = 0 To raOrdList.Count - 1
                    With CType(raOrdList.Item(ix), clsMTS0001)

                        sFkOcs = fnGet_FKOCS(.ORDER_DATE, "OR")
                        If sFkOcs = "" Then Return "번호발생 오류"

                        sFkOcs = .ORDER_DATE + sFkOcs.PadLeft(4, "0"c)

                        .FKOCS = sFkOcs
                        .ORDER_TIME = msTime.Substring(0, 4)

                        sSql = ""
                        sSql += "INSERT INTO mts0001_lis("
                        sSql += "            in_out_gubun, fkocs, bunho, gwa, ipwon_date, resident, doctor, ho_dong, ho_code, ho_bed,"
                        sSql += "            order_date, order_time, hangmog_code, slip_gubun, specimen_code, suryang, hope_date, hope_time, dc_yn, sunab_date,"
                        sSql += "            emergency, remark, remark_nrs, opdt, nrs_time, sys_date, user_id, upd_date, seq"
                        sSql += "          )"
                        sSql += "    values( :iogbn, :fkocs,  :regno, :deptcd,  :ipwondt, :resident, :dcotor, :wardno, :roomno, :bedno,"
                        sSql += "            :orddt, :ordtm,  :ordcd, :slipgbn, :spccd,   :sugryang, :hopedt,  :hopetm, :dcyn, :sunabdt,"
                        sSql += "            :eryn,  :remark, :remark_nrs, :opdt, fn_ack_sysdate, fn_ack_sysdate, :usrid, fn_ack_sysdate, sq_mts0001_lis.nextval"
                        sSql += "          )"

                        dbCmd.CommandText = sSql
                        dbCmd.Parameters.Clear()

                        dbCmd.Parameters.Add("iogbn", OracleDbType.Varchar2).Value = .IN_OUT_GUBUN
                        dbCmd.Parameters.Add("fkocs", OracleDbType.Varchar2).Value = .FKOCS
                        dbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = .BUNHO
                        dbCmd.Parameters.Add("deptcd", OracleDbType.Varchar2).Value = .GWA
                        dbCmd.Parameters.Add("ipwondt", OracleDbType.Varchar2).Value = .IPWON_DATE
                        dbCmd.Parameters.Add("resident", OracleDbType.Varchar2).Value = .RESIDENT
                        dbCmd.Parameters.Add("dcotor", OracleDbType.Varchar2).Value = .DOCTOR
                        dbCmd.Parameters.Add("wardno", OracleDbType.Varchar2).Value = .HO_DONG
                        dbCmd.Parameters.Add("roomno", OracleDbType.Varchar2).Value = .HO_CODE
                        dbCmd.Parameters.Add("bedno", OracleDbType.Varchar2).Value = .HO_BED

                        dbCmd.Parameters.Add("orddt", OracleDbType.Varchar2).Value = .ORDER_DATE
                        dbCmd.Parameters.Add("ordtm", OracleDbType.Varchar2).Value = .ORDER_TIME
                        dbCmd.Parameters.Add("ordcd", OracleDbType.Varchar2).Value = .HANGMOG_CODE
                        dbCmd.Parameters.Add("slipgbn", OracleDbType.Varchar2).Value = .SLIP_GUBUN
                        dbCmd.Parameters.Add("spccd", OracleDbType.Varchar2).Value = .SPECIMEN_CODE
                        dbCmd.Parameters.Add("sugryang", OracleDbType.Int16).Value = .SURYANG
                        dbCmd.Parameters.Add("hopedt", OracleDbType.Varchar2).Value = .HOPE_DATE
                        dbCmd.Parameters.Add("hopetm", OracleDbType.Varchar2).Value = .HOPE_TIME
                        dbCmd.Parameters.Add("dcyn", OracleDbType.Varchar2).Value = .DC_YN
                        dbCmd.Parameters.Add("sunabdt", OracleDbType.Varchar2).Value = .SUNAB_DATE

                        dbCmd.Parameters.Add("eryn", OracleDbType.Varchar2).Value = .EMERGENCY
                        dbCmd.Parameters.Add("remark", OracleDbType.Varchar2).Value = .REMARK
                        dbCmd.Parameters.Add("remark_nrs", OracleDbType.Varchar2).Value = .REQ_REMARK
                        dbCmd.Parameters.Add("opdt", OracleDbType.Varchar2).Value = .OPDT
                        dbCmd.Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrId

                        dbCmd.ExecuteNonQuery()

                    End With
                Next

                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnExe_MTS0903(ByVal raDrugList As ArrayList) As String
            Dim sFn As String = "fnExe_MTS0903() as boolean"
            Dim dbCmd As New OracleCommand

            Try
                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sSql As String = ""



                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function


        Private Function fnExe_MTS0101(ByVal raDiagList As ArrayList) As String
            Dim sFn As String = "fnExe_MTS0903() as boolean"
            Dim dbCmd As New OracleCommand

            Try
                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sSql As String = ""


                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnExe_MTS0002(ByVal raPatInfo As clsMTS0002) As String
            Dim sFn As String = "fnExe_MTS0903() as boolean"
            Dim dbCmd As New OracleCommand

            Try
                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                With dbCmd
                    sSql = ""
                    sSql += "UPDATE mts0002_lis"
                    sSql += "   SET suname    = :patnm,"
                    sSql += "       birth     = :birth,"
                    sSql += "       sujumin1  = :juminl,"
                    sSql += "       sujumin2  = :juminr,"
                    sSql += "       zip_code1 = :zipcd1,"
                    sSql += "       zip_code2 = :zipcd2,"
                    sSql += "       address1  = :addr1,"
                    sSql += "       address2  = :addr2,"
                    sSql += "       tel1      = :tel1,"
                    sSql += "       tel2      = :tel2,"
                    sSql += "       user_id   = 'U',"
                    sSql += "       sex       = :sex,"
                    sSql += "       upd_date  = fn_ack_sysdate"
                    sSql += " WHERE bunho     = :regno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("patnm", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUNAME
                    .Parameters.Add("birth", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).BIRTH
                    .Parameters.Add("juminl", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUJUMIN1
                    .Parameters.Add("juminr", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUJUMIN2
                    .Parameters.Add("zipcd1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ZIP_CODE1
                    .Parameters.Add("zipcd2", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ZIP_CODE2
                    .Parameters.Add("addr1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ADDRESS1
                    .Parameters.Add("addr2", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ADDRESS2
                    .Parameters.Add("tel1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).TEL1
                    .Parameters.Add("tel2", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).TEL2
                    .Parameters.Add("sex", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SEX

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).BUNHO

                    iRet = .ExecuteNonQuery()
                    If iRet = 0 Then

                        sSql = ""
                        sSql += "INSERT INTO mts0002_lis("
                        sSql += "            bunho, suname, birth, sujumin1, sujumin2, zip_code1, zip_code2, address1, address2,"
                        sSql += "            tel1, tel2, sex, user_id, sys_date, upd_date, seq"
                        sSql += "          ) "
                        sSql += "    VALUES( :regno, :patnm, :birth, :juminl, :juminr, :zipcd1, :zipcd2, :addr1, :addr2,"
                        sSql += "            :tel1, :tel2, :sex, 'U', fn_ack_sysdate, fn_ack_sysdate, sq_mts0002_lis.nextval"
                        sSql += "          )"


                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()

                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).BUNHO
                        .Parameters.Add("patnm", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUNAME
                        .Parameters.Add("birth", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).BIRTH
                        .Parameters.Add("juminl", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUJUMIN1
                        .Parameters.Add("juminr", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUJUMIN2
                        .Parameters.Add("zipcd1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ZIP_CODE1
                        .Parameters.Add("zipcd2", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ZIP_CODE2
                        .Parameters.Add("addr1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ADDRESS1
                        .Parameters.Add("addr2", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ADDRESS2

                        .Parameters.Add("tel1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).TEL1
                        .Parameters.Add("tel2", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).TEL2
                        .Parameters.Add("sex", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SEX

                        .ExecuteNonQuery()
                    End If
                End With

                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnExe_MTS0004(ByVal raPatInfo As clsMTS0002, ByVal rsUsrId As String) As String
            Dim sFn As String = "fnExe_MTS0903() as boolean"
            Dim dbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            Try
                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sSql As String = ""

                sSql += "SELECT regno FROM mts0004_lis"
                sSql += " WHERE custcd = :custcd"
                sSql += "   AND regno  = :regno"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("custcd", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).CUSTCD
                    .SelectCommand.Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).CREGNO
                End With

                dt.Reset()
                dbDa.Fill(dt)

                With dbCmd
                    If dt.Rows.Count > 0 Then
                        sSql = ""
                        sSql += "UPDATE mts0004_lis"
                        sSql += "   SET patnm     = :patnm,"
                        sSql += "       birth     = :birth,"
                        sSql += "       idnol     = :idnol,"
                        sSql += "       idnor     = :idonr,"
                        sSql += "       zipno     = :zipno,"
                        sSql += "       address   = :addr1,"
                        sSql += "       tel1      = :tel1,"
                        sSql += "       tel2      = :tel2,"
                        sSql += "       sex       = :sex,"
                        sSql += "       foreginyn = :foreginyn,"
                        sSql += "       regdt     = fn_ack_sysdate,"
                        sSql += "       regid     = :regid"
                        sSql += " WHERE custcd    = :custcd"
                        sSql += "   AND regno     = :regno"

                        .CommandText = sSql

                        .Parameters.Clear()

                        .Parameters.Add("patnm", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUNAME
                        .Parameters.Add("birth", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).BIRTH
                        .Parameters.Add("idnol", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUJUMIN1
                        .Parameters.Add("idnor", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUJUMIN2
                        .Parameters.Add("zipno", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ZIP_CODE1
                        .Parameters.Add("addr1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ADDRESS1
                        .Parameters.Add("tel1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).TEL1
                        .Parameters.Add("tel2", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).TEL2
                        .Parameters.Add("sex", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SEX
                        .Parameters.Add("foreginyn", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).FOREGINYN
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId

                        .Parameters.Add("custcd", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).CUSTCD
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).CREGNO

                    Else
                        sSql = ""
                        sSql += "INSERT INTO mts0004_lis("
                        sSql += "            custcd, regno, patnm, birth, idnol, idnor, zipno, address, tel1, tel2,"
                        sSql += "            sex, foreginyn, regdt, regid"
                        sSql += "          ) "
                        sSql += "    VALUES( :custcd, :regno, :patnm, :birth, :idnol, :idnor, :zipno, :addr1, :tel1, :tel2,"
                        sSql += "            :sex, :foreginyn, fn_ack_sysdate, :regid"
                        sSql += "          )"

                        .CommandText = sSql

                        .Parameters.Clear()

                        .Parameters.Add("custcd", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).CUSTCD
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).CREGNO
                        .Parameters.Add("patnm", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUNAME
                        .Parameters.Add("birth", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).BIRTH
                        .Parameters.Add("idnol", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUJUMIN1
                        .Parameters.Add("idnor", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SUJUMIN2
                        .Parameters.Add("zipno", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ZIP_CODE1
                        .Parameters.Add("addr1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).ADDRESS1

                        .Parameters.Add("tel1", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).TEL1
                        .Parameters.Add("tel2", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).TEL2
                        .Parameters.Add("sex", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).SEX
                        .Parameters.Add("foreginyn", OracleDbType.Varchar2).Value = CType(raPatInfo, clsMTS0002).FOREGINYN
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                    End If


                    .ExecuteNonQuery()
                End With

                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


        Public Function ExecuteDo_DC(ByVal raOrdList As ArrayList, ByVal rsUsrId As String) As String
            Dim sFn As String = "Public Sub ExecuteDo_DC(ArrayList) as string"
            Dim dbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim sFkOcs As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                ' 검사항목 Query Script 생성
                For ix As Integer = 0 To raOrdList.Count - 1
                    With CType(raOrdList.Item(ix), clsMTS0001)

                        sSql = ""
                        sSql += "UPDATE mts0001_lis SET dc_yn = 'Y', USER_ID = :usrid, UPD_DATE = fn_ack_sysdate"
                        sSql += " WHERE bunho        = :regno"
                        sSql += "   AND in_out_gubun = :iogbn"
                        sSql += "   AND order_date   = :orddt"
                        sSql += "   AND fkocs        = :fkcos"

                        dbCmd.CommandText = sSql
                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrId
                        dbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = .BUNHO
                        dbCmd.Parameters.Add("iogbn", OracleDbType.Varchar2).Value = .IN_OUT_GUBUN
                        dbCmd.Parameters.Add("orddt", OracleDbType.Varchar2).Value = .ORDER_DATE.Replace("-", "")
                        dbCmd.Parameters.Add("fkcos", OracleDbType.Int64).Value = .FKOCS

                        Dim iRet As Integer = dbCmd.ExecuteNonQuery()

                    End With
                Next

                m_dbTran.Commit()

                Return ""
            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        ' 일반항목 가상처방
        Public Function ExecuteDo(ByVal raOrdList As ArrayList, ByVal raPatInfo As clsMTS0002, _
                                    ByVal raDrugList As ArrayList, ByVal raDiagList As ArrayList, _
                                      ByVal rsUsrId As String, Optional ByVal rsCustYN As String = "") As String
            Dim sFn As String = "Public Sub ExecuteDo(ByVal alOrderList As ArrayList, ByVal asREGNO As String, ByVal adtOrdDate As Date)"

            Try
                If m_dbCn.State <> ConnectionState.Open Then
                    Throw (New Exception("서버 접속 오류" + " @" + msFile + sFn))
                End If

                Dim strErr As String = ""

                ' 검사항목 Query Script 생성
                strErr = fnExe_Mts0001(raOrdList, rsUsrId)
                If strErr <> "" Then
                    m_dbTran.Rollback()
                    Return strErr
                End If

                If Not IsNothing(raDrugList) Then
                    strErr = fnExe_MTS0903(raDrugList)
                    If strErr <> "" Then
                        m_dbTran.Rollback()
                        Return "MTS0903_LIS 입력시 오류 발생"
                    End If
                End If

                If Not IsNothing(raDiagList) Then
                    strErr = fnExe_MTS0101(raDiagList)
                    If strErr <> "" Then
                        m_dbTran.Rollback()
                        Return strErr
                    End If
                End If

                strErr = fnExe_MTS0002(raPatInfo)
                If strErr <> "" Then
                    m_dbTran.Rollback()
                    Return strErr
                End If

                If rsCustYN = "Y" Then
                    strErr = fnExe_MTS0004(raPatInfo, rsUsrId)
                    If strErr <> "" Then
                        m_dbTran.Rollback()
                        Return "MTS0004_LIS 입력시 오류 발생"
                    End If
                End If

                m_dbTran.Commit()

                Return ""
            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

#Region " clsMTS0001 "
        Public Class clsMTS0001
            Public SEQ As String = ""
            Public IN_OUT_GUBUN As String = ""
            Public FKOCS As String = ""
            Public BUNHO As String = ""
            Public GWA As String = ""
            Public IPWON_DATE As String = ""
            Public RESIDENT As String = ""
            Public DOCTOR As String = ""
            Public HO_DONG As String = ""
            Public HO_CODE As String = ""
            Public HO_BED As String = ""
            Public ORDER_DATE As String = ""
            Public ORDER_TIME As String = ""
            Public HANGMOG_CODE As String = ""
            Public SLIP_GUBUN As String = ""
            Public SPECIMEN_CODE As String = ""
            Public SURYANG As String = ""
            Public HOPE_DATE As String = ""
            Public HOPE_TIME As String = ""
            Public DC_YN As String = ""
            Public APPEND_YN As String = ""
            Public SUNAB_DATE As String = ""
            Public SOURCE_FKOCS As String = ""
            Public EMERGENCY As String = ""
            Public REMARK As String = ""
            Public REQ_REMARK As String = ""
            Public HEIGHT As String = ""
            Public WEGHT As String = ""
            Public SEND_DATE As String = ""
            Public RECV_DATE As String = ""
            Public IUD As String = ""
            Public FLAG As String = ""
            Public OPDT As String = ""
            Public SPCFLAG As String = ""
            Public COLLDT As String = ""
            Public TKDT As String = ""
            Public RSTFLAG As String = ""
            Public RSTDT As String = ""
            Public INPUT_PART As String = ""

            Public REMARK2 As String = ""
            Public LISCMT As String = ""

            Public Sub New()
                MyBase.new()
            End Sub
        End Class
#End Region

#Region " clsMTS0002 "
        Public Class clsMTS0002
            Public SEQ As String = ""
            Public BUNHO As String = ""
            Public SUNAME As String = ""
            Public BIRTH As String = ""
            Public SUJUMIN1 As String = ""
            Public SUJUMIN2 As String = ""
            Public ZIP_CODE1 As String = ""
            Public ZIP_CODE2 As String = ""
            Public ADDRESS1 As String = ""
            Public ADDRESS2 As String = ""
            Public TEL1 As String = ""
            Public TEL2 As String = ""
            Public SEND_DATE As String = ""
            Public RECV_DATE As String = ""
            Public IUD As String = ""
            Public FLAG As String = ""
            Public SEX As String = ""

            Public FOREGINYN As String = ""
            Public CUSTCD As String = ""
            Public CREGNO As String = ""

            Public Sub New()
                MyBase.New()
            End Sub
        End Class
#End Region

#Region " clsMTS0101 "
        Public Class clsMTS0101
            Public SEQ As String = ""
            Public ORDER_DATE As String = ""
            Public BUNHO As String = ""
            Public SANG_CODE As String = ""
            Public SANG_ENAME As String = ""
            Public SANG_HNAME As String = ""
            Public SEND_DATE As String = ""
            Public RECV_DATE As String = ""
            Public IUD As String = ""
            Public FLAG As String = ""

            Public Sub New()
                MyBase.New()
            End Sub
        End Class
#End Region

#Region " clsMTS0903 "
        Public Class clsMTS0903
            Public SEQ As String = ""
            Public BUNHO As String = ""
            Public DRUG_CODE As String = ""
            Public DRUG_NAME As String = ""
            Public SEND_DATE As String = ""
            Public RECV_DATE As String = ""
            Public IUD As String = ""
            Public FLAG As String = ""
            Public NALSU As String = ""
            Public ORDER_DATE As String = ""
            Public SURYANG As String = ""

            Public Sub New()
                MyBase.New()
            End Sub
        End Class
#End Region

    End Class

#End Region

End Namespace


