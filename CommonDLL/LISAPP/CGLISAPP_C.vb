'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : 관동대명지병원 Laboratory Information System(KMC_LIS)                  */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_C.vb                                                              */
'/* PartName     : 채혈관리                                                               */
'/* Description  : 채관리의 Data Query구문관련 Class                                      */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     : 2004-02-09 : JJH Remark 대기자 화면에서는 해당일자의 모든 처방보기     */
'/*                2004-01-19 : JJH --> 대기환자 리스트에서 종합검진과 표시 안함          */
'/*                2004-04-27 : JJH --> 채혈전 간호확인 체크                              */
'/*                2007-08-21 : SSH --> 원자력병원용 (Group 항목채혈관리 가능하도록)      */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports System.Windows.Forms
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports OCSAPP
Imports OCSAPP.OcsLink.Ord

Namespace APP_C
    Public Class Collfn
        Private Const msFile As String = "File : CGLISAPP.vb, Class : LISAPP.APP_C.Collfn" & vbTab

        Public Sub New()
            MyBase.New()
        End Sub
       

        ' 검사분야 조회
        Public Shared Function fnGet_PartSlip_List() As DataTable
            Dim sFn As String = "Function fnGet_Slip_List() As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT partcd || slipcd slipcd, slipnmd, dispseq, '1' sort1"
                sSql += "  FROM lf021m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += " UNION "
                sSql += "SELECT partcd || slipcd slipcd, slipnmd, dispseq, '2' sort1"
                sSql += "  FROM rf021m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += " ORDER BY sort1, dispseq, slipcd"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

#Region "채혈/접수 취소내역"
        Public Shared Function fnGet_CollTk_Cancel_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIoGbn As String, _
                                                ByVal rsCancelGbn As String, ByVal rbDetailGbn As Boolean, ByVal rsDeptWards As String, _
                                                Optional ByVal rsPartSlip As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CollTk_Cancel_List(String, String, String, String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(j3.canceldt, 'yyyy-mm-dd hh24:mi:ss') deldt,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_get_usr_name(r.tkid) tknm,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, j.deptcd,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptinfo,"
                sSql += "       j.wardno || '/' || j.roomno wardroom, j.iogbn,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       j3.cancelcmt, j3.cancelcd, j3.cancelgbn,"
                sSql += "       fn_ack_get_usr_name(j3.cancelid) cancelnm,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnm"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(b, ',' || b.tnmd)).extract('//text()'), 2)"
                sSql += "          FROM lj011m a, lf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnm"
                sSql += "  FROM lj010m j, lj030m j3,"
                sSql += "       (SELECT bcno, MAX(colldt) colldt, MAX(collid) collid"
                sSql += "          FROM lj011h"
                sSql += "         WHERE bcno IN (SELECT bcno FROM lj030m WHERE canceldt >= :dates AND canceldt <= :datee || '235959')"
                sSql += "         GROUP BY bcno"
                sSql += "       ) j1, "
                sSql += "       (SELECT bcno, MAX(tkdt) tkdt, MAX(tkid) tkid FROM lr010h"
                sSql += "         WHERE bcno IN (SELECT bcno FROM lj030m WHERE canceldt >= :dates AND canceldt <= :datee || '235959')"
                sSql += "         GROUP BY bcno"
                sSql += "         UNION"
                sSql += "        SELECT bcno, MAX(tkdt) tkdt, MAX(tkid) tkid FROM lm010h"
                sSql += "         WHERE bcno IN (SELECT bcno FROM lj030m WHERE canceldt >= :dates AND canceldt <= :datee || '235959')"
                sSql += "         GROUP BY bcno"
                sSql += "       )"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                sSql += " WHERE j3.canceldt  >= :dates "
                sSql += "   AND j3.canceldt  <= :datee || '235959'"
                sSql += "   AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"
                sSql += "   AND j3.bcno       = j.bcno"
                sSql += "   AND j.bcno        = j1.bcno"
                sSql += "   AND j1.bcno       = r.bcno (+)"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsIoGbn <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))
                    If rsDeptWards <> "" Then
                        If rsIoGbn = "I" Then
                            sSql += "   AND j.wardno " + IIf(rbDetailGbn, " NOT ", "").ToString + "IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "   AND j.deptcd " + IIf(rbDetailGbn, " NOT ", "").ToString + "IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                End If

                If rsPartSlip <> "" Then
                    sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate AND partcd || slipcd = :partslip)"
                    alParm.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                End If

                '-- 핵의학
                sSql += " UNION "
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(j3.canceldt, 'yyyy-mm-dd hh24:mi:ss') deldt,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_get_usr_name(r.tkid) tknm,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, j.deptcd,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptinfo,"
                sSql += "       j.wardno || '/' || j.roomno wardroom, j.iogbn,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       j3.cancelcmt, j3.cancelcd, j3.cancelgbn,"
                sSql += "       fn_ack_get_usr_name(j3.cancelid) cancelnm,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnm"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(b, ',' || b.tnmd)).extract('//text()'), 2)"
                sSql += "          FROM rj011m a, rf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnm"
                sSql += "  FROM rj030m j3, rj010m j,"
                sSql += "       (SELECT bcno, MAX(colldt) colldt, MAX(collid) collid"
                sSql += "          FROM rj011h"
                sSql += "         WHERE bcno IN (SELECT bcno FROM lj030m WHERE canceldt >= :dates AND canceldt <= :datee || '235959')"
                sSql += "         GROUP BY bcno"
                sSql += "       ) j1,"
                sSql += "       (SELECT bcno, MAX(tkdt) tkdt, MAX(tkid) tkid FROM rr010h"
                sSql += "         WHERE bcno IN (SELECT bcno FROM lj030m WHERE canceldt >= :dates AND canceldt <= :datee || '235959')"
                sSql += "         GROUP BY bcno"
                sSql += "       ) r"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                sSql += " WHERE j3.canceldt  >= :dates"
                sSql += "   AND j3.canceldt  <= :datee || '235959'"
                sSql += "   AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"
                sSql += "   AND j3.bcno       = j.bcno"
                sSql += "   AND j.bcno        = j1.bcno"
                sSql += "   AND j1.bcno       = r.bcno"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsIoGbn <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))
                    If rsDeptWards <> "" Then
                        If rsIoGbn = "I" Then
                            sSql += "   AND j.wardno " + IIf(rbDetailGbn, " NOT ", "").ToString + "IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "   AND j.deptcd " + IIf(rbDetailGbn, " NOT ", "").ToString + "IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                End If

                If rsPartSlip <> "" Then
                    sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf060m WHERE usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate AND partcd || slipcd = :partslip)"
                    alParm.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                End If
                sSql += " ORDER BY deldt, orddt, regno, bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))

            End Try

        End Function

        Public Shared Function fnGet_CollTk_Cancel_Statistics(ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, ByVal rsIOGBN As String, _
                                                              ByVal rsCancelGbn As String, ByVal rbDetailGbn As Boolean, ByVal rsDeptWards As String, _
                                                              Optional ByVal rsSlipCd As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CollTk_Cancel_List(String, String, String, String, boolean, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT canceldt, cancelcd, cancelcmt, COUNT(bcno) cnt"
                sSql += "  FROM ("
                sSql += "        SELECT j3.cancelcmt, j3.cancelcd, fn_ack_date_str(j3.canceldt, 'yyyy-mm') canceldt, j3.bcno"
                sSql += "          FROM lj030m j3, lj010m j"
                sSql += "         WHERE j3.bcno = j.bcno"
                sSql += "           AND j3.canceldt >= :dates"
                sSql += "           AND j3.canceldt <= :datee || '235959'"
                sSql += "           AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsOrdDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsOrdDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtE))

                If rsIOGBN = "I" Then
                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            sSql += "           AND j.wardno NOT IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "           AND j.wardno IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                ElseIf rsIOGBN = "O" Then
                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            sSql += "           AND j.deptcd NOT IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "           AND j.deptcd IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                End If


                If rsSlipCd <> "" Then
                    sSql += "           AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate AND partcd || slipcd = :partslip)"
                    alParm.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If

                '--- 핵의학
                sSql += "         UNION"
                sSql += "        SELECT j3.cancelcmt, j3.cancelcd, fn_ack_date_str(j3.canceldt, 'yyyy-mm') canceldt, j3.bcno"
                sSql += "          FROM rj030m j3, rj010m j"
                sSql += "         WHERE j3.bcno       = j.bcno"
                sSql += "           AND j3.canceldt  >= :dates"
                sSql += "           AND j3.canceldt  <= :datee || '235959'"
                sSql += "           AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsOrdDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsOrdDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtE))

                If rsIOGBN = "I" Then
                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            sSql += "           AND j.wardno NOT IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "           AND j.wardno IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                ElseIf rsIOGBN = "O" Then
                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            sSql += "           AND j.deptcd NOT IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "           AND j.deptcd IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                End If


                If rsSlipCd <> "" Then
                    sSql += "           AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate AND partcd || slipcd = :partslip)"
                    alParm.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If
                sSql += "       ) a"

                sSql += " GROUP BY cancelcmt, cancelcd, canceldt"


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try

        End Function


#End Region

       
        Public Shared Function fnGet_Comment_pat(ByVal rsIoGbn As String, ByVal rsRegNo As String) As String
            Dim sFn As String = "Public fnGet_Comment_pat(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT remark  FROM lj040m"
                sSql += " WHERE regno = :regno"

                alParm.Clear()
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count < 1 Then Return ""

                Dim sCmt As String = ""
                For ix As Integer = 0 To dt.Rows.Count - 1
                    If ix > 0 Then sCmt += vbCrLf
                    sCmt += dt.Rows(ix).Item("remark").ToString
                Next

                Return sCmt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_CollectInfo(ByVal rsBcNo As String, ByVal rbTakeYn As Boolean) As DataTable
            Dim sFn As String = "Public fnGet_CollectInfo(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.spcflg,"
                'ssql += "       fn_ack_get_test_name_list(j.bcno) testnms,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(b, ',' || b.tnmd)).extract('//text()'), 2)"
                sSql += "          FROM lj011m a, lf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) testnms,"
                sSql += "       CASE WHEN (SELECT MAX(NVL(rstflg, '0')) rstflg FROM lr010m WHERE bcno = j.bcno) > '0' OR"
                sSql += "                 (SELECT MAX(NVL(rstflg, '0')) rstflg FROM lm010m WHERE bcno = j.bcno) > '0' THEN '1' ELSE '0'"
                sSql += "       END rstflg,"
                'sSql += "       fn_ack_get_bcno_fkocs(j.bcno) bcno_fkocs,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(a, ',' || a.bcno)).extract('//text()'), 2)"
                sSql += "          FROM lj011m a"
                sSql += "         WHERE a.fkocs  IN (SELECT fkocs FROM lj011m WHERE bcno = j.bcno AND fkocs <> '0')"
                sSql += "           AND a.spcflg IN ('1', '2', '3', '4')"
                sSql += "       ) bcno_fkocs,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo"
                sSql += "  FROM lj010m j, lj011m j1"
                sSql += " WHERE j.bcno = :bcno"
                sSql += "   AND j.bcno = j1.bcno"

                If rbTakeYn Then
                    sSql += "   AND j1.spcflg IN ('1', '2', '4')"
                Else
                    sSql += "   AND j1.spcflg IN ('1', '2')"
                End If
                sSql += " ORDER BY bcno"

                alParm.Clear()
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_CollectInfo_bcnos(ByVal rsBcNos As String) As DataTable
            Dim sFn As String = "Public fnGet_CollectInfo(String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.spcflg,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) testnms,"
                sSql += "      (SELECT SUBSTR(xmlagg(xmlelement(b, ',' || b.tnmd)).extract('//text()'), 2)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) testnms,"
                sSql += "       CASE WHEN (SELECT MAX(NVL(rstflg, '0')) rstflg FROM lr010m WHERE bcno = j.bcno) > '0' OR"
                sSql += "                 (SELECT MAX(NVL(rstflg, '0')) rstflg FROM lm010m WHERE bcno = j.bcno) > '0' THEN '1' ELSE '0'"
                sSql += "       END rstflg,"
                'sSql += "       fn_ack_get_bcno_fkocs(j0.bcno) bcno_fkocs,"
                'sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(a, ',' || a.bcno)).extract('//text()'), 2)"
                'sSql += "          FROM lj011m a"
                'sSql += "         WHERE a.fkocs  IN (SELECT fkocs FROM lj011m WHERE bcno = j.bcno AND fkocs <> '0')"
                'sSql += "           AND a.spcflg IN ('1', '2', '3', '4')"
                'sSql += "       ) bcno_fkocs,"
                sSql += "       '' bcno_fkocs,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo"
                sSql += "  FROM lj010m j, lj011m j1"
                sSql += " WHERE j.bcno = j1.bcno"
                sSql += "   AND (j1.fkocs) IN (SELECT fkocs FROM lj011m WHERE bcno IN (" + rsBcNos + "))"
                sSql += "   AND j1.spcflg IN ('1', '2')"
                sSql += " UNION "
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.spcflg,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) testnms,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(b, ',' || b.tnmd)).extract('//text()'), 2)"
                sSql += "          FROM rj011m a, rf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) testnms,"
                sSql += "       CASE WHEN (SELECT MAX(NVL(rstflg, '0')) rstflg FROM rr010m WHERE bcno = j.bcno) > '0' THEN '1' ELSE '0'"
                sSql += "       END rstflg,"
                'sSql += "       fn_ack_get_bcno_fkocs(j0.bcno) bcno_fkocs,"
                'sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(a, ',' || a.bcno)).extract('//text()'), 2)"
                'sSql += "          FROM rj011m a "
                'sSql += "         WHERE a.fkocs  IN (SELECT fkocs FROM rj011m WHERE bcno = j.bcno AND fkocs <> '0')"
                'sSql += "           AND a.spcflg IN ('1', '2', '3', '4')"
                'sSql += "       ) bcno_fkocs,"
                sSql += "       '' bcno_fkocs,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo"
                sSql += "  FROM rj010m j, rj011m j1"
                sSql += " WHERE j.bcno = j1.bcno"
                sSql += "   AND (j1.fkocs) IN (SELECT fkocs FROM rj011m WHERE bcno IN (" + rsBcNos + "))"
                sSql += "   AND j1.spcflg IN ('1', '2')"
                sSql += " ORDER BY bcno"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_Collect_CancelData(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public fnGet_Collect_CancelData(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT j0.regno, j0.bcno,   j1.tclscd,  j1.spccd,  j1.fkocs, f6.tcdgbn,"
                sSql += "       j0.iogbn, j0.owngbn, j0.bcclscd, j0.spcflg, f6.tordcd, 'L' partgbn"
                sSql += "  FROM lj010m j0, lj011m j1, lf060m f6"
                sSql += " WHERE j1.fkocs  IN (SELECT fkocs FROM lj011m WHERE bcno = :bcno)"
                sSql += "   AND j0.bcno    = j1.bcno"
                sSql += "   AND j0.spcflg IN ('1', '2')"
                sSql += "   AND j1.tclscd  = f6.testcd"
                sSql += "   AND j1.spccd   = f6.spccd"
                sSql += "   AND f6.usdt   <= NVL(j1.colldt, j1.sysdt)"
                sSql += "   AND f6.uedt   >  NVL(j1.colldt, j1.sysdt)"
                sSql += " UNION "
                sSql += "SELECT j0.regno, j0.bcno,   j1.tclscd,  j1.spccd,  j1.fkocs, f6.tcdgbn,"
                sSql += "       j0.iogbn, j0.owngbn, j0.bcclscd, j0.spcflg, f6.tordcd, 'R' partgbn"
                sSql += "  FROM rj010m j0, rj011m j1, rf060m f6"
                sSql += " WHERE j1.fkocs  IN (SELECT fkocs FROM rj011m WHERE bcno = :bcno)"
                sSql += "   AND j0.bcno    = j1.bcno"
                sSql += "   AND j0.spcflg IN ('1', '2')"
                sSql += "   AND j1.tclscd  = f6.testcd"
                sSql += "   AND j1.spccd   = f6.spccd"
                sSql += "   AND f6.usdt   <= NVL(j1.colldt, j1.sysdt)"
                sSql += "   AND f6.uedt   >  NVL(j1.colldt, j1.sysdt)"

                alParm.Clear()
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 검체번호(바코드번호) 가져오기
        Public Shared Function GetBCNO(ByVal rsSeqDate As String, ByVal rsSeqGbn As String) As String
            Dim sFn As String = "Public Function GetBCNO(ByVal asDATE As String, ByVal asGBN As String) As String"

            Try
                Dim sSql As String = "pro_ack_exe_seqno_bc"
                Dim stu_param As New DBORA.DbParrameter

                With stu_param
                    .AddItem("rs_seqymd", OracleDbType.Varchar2, ParameterDirection.Input, rsSeqDate)
                    .AddItem("rs_seqgbn", OracleDbType.Varchar2, ParameterDirection.Input, rsSeqGbn)
                    .AddItem("rn_seqno", OracleDbType.Varchar2, ParameterDirection.Output, 0)
                End With

                DbCommand()
                DbExecute(sSql, stu_param, False)

                Return Format(CType(stu_param.Item(2).Value.ToString, Integer), "000#")

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '음식관련 채혈 주의사항 '2017.07.14
        Public Shared Function Fn_FoodWaring_C(ByVal rsTestcd As String, ByVal rsSpccd As String) As DataTable
            Dim sFn As String = ""

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList


                sSql = ""
                sSql += "   SELECT FWGBN  FROM LF060M " + vbCrLf
                sSql += " WHERE TESTCD = :testcd "
                sSql += " AND SPCCD = :spccd "
                sSql += "   AND USDT <= fn_ack_sysdate "
                sSql += " AND UEDT > fn_ack_sysdate "


                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestcd))
                al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpccd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '채혈 주의사항 '2019.09.20
        Public Shared Function Fn_CWaring_C(ByVal rsTestcd As String, ByVal rsSpccd As String) As DataTable
            Dim sFn As String = ""

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList


                sSql = ""
                sSql += "   SELECT TESTCD , TNMD ,CWARNING , cwgbn FROM LF060M " + vbCrLf
                sSql += " WHERE TESTCD = :testcd "
                sSql += " AND SPCCD = :spccd "
                sSql += "   AND USDT <= fn_ack_sysdate "
                sSql += " AND UEDT > fn_ack_sysdate "
                sSql += " AND cwgbn = '1' "

                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestcd))
                al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpccd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '20220121 jhs 검상항목 모조리 불러오기 
        Public Shared Function Fn_Chk_testcd(ByVal rsTordCd As String, ByVal rsSpccd As String) As String
            Dim sFn As String = ""
            Dim dt As DataTable
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += " select NVL((SELECT SUBSTR(XMLAGG(XMLELEMENT(FF, ',' || FF.TESTCD) ORDER BY FF.TESTCD).EXTRACT('//text()'), 2)  " + vbCrLf
                sSql += "               From LF062M ff                                                                                   " + vbCrLf
                sSql += "              WHERE TCLSCD = F6.TESTCD                                                                          " + vbCrLf
                sSql += "                AND SPCCD  = F6.SPCCD), F6.TESTCD) dtestcd from lf060m f6                                       " + vbCrLf
                sSql += " where tordcd = :rsTordCd                                                                                       " + vbCrLf
                sSql += "   and spccd  = :rsSpcCd                                                                                        " + vbCrLf

                al.Add(New OracleParameter("rsTordCd", OracleDbType.Varchar2, rsTordCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTordCd))
                al.Add(New OracleParameter("rsTordCd", OracleDbType.Varchar2, rsSpccd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString().Trim()
                Else
                    Return ""
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '------------------------------------------------------------------------------
        '20220121 jhs 검사항목 하나로 통합 
        Public Shared Function Fn_Combine_TestCd(ByVal rsTestCds As String) As String
            Dim sFn As String = ""
            Dim dt As DataTable
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql = "Select SUBSTR(XMLAGG(XMLELEMENT(b, ',' || b.split_result) ORDER BY b.split_result).EXTRACT('//text()'), 2)          " + vbCrLf
                sSql += "      from ( Select distinct regexp_substr(a.langlist, '[^,]+',1 ,level) as split_result                           " + vbCrLf
                sSql += "               from (select :rsTestCds as langlist from dual) a                                                    " + vbCrLf
                sSql += "            connect by level <= length(regexp_replace(a.langlist,'[^,]+','')) + 1) b                               " + vbCrLf

                al.Add(New OracleParameter("rsTordCd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString().Trim()
                Else
                    Return ""
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '------------------------------------------------------------------------------
        '< add yjlee 
        Public Shared Function FindInfectionInfoD(ByVal rsRegNo As String) As String
            Dim sFn As String = "FindInfectionInfoD"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT fn_ack_get_infection_prt(:regno) FROM DUAL"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()

                dt = DbExecuteQuery(sSql, al)

                Dim sTmpInfection As String = ""

                For iCnt As Integer = 0 To dt.Rows.Count - 1
                    If sTmpInfection.Length > 0 Then sTmpInfection += "/"

                    sTmpInfection += dt.Rows(iCnt).Item(0).ToString().Trim()
                Next

                Return sTmpInfection

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function FindInfectionInfoP(ByVal rsRegNo As String) As String
            Dim sFn As String = "FindInfectionInfoP"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT itemnmp"
                sSql += "  FROM lr060m"
                sSql += " WHERE regno = :regno"
                sSql += "   AND deldt is null"
                sSql += "   AND delid is null"
                sSql += " ORDER BY regdt"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()

                dt = DbExecuteQuery(sSql, al)

                Dim sTmpInfection As String = ""

                For iCnt As Integer = 0 To dt.Rows.Count - 1
                    If sTmpInfection.Length > 0 Then sTmpInfection += "/"

                    sTmpInfection += dt.Rows(iCnt).Item("itemnmp").ToString().Trim()
                Next

                Return sTmpInfection

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '< add yjlee 
        Public Shared Function FindAboRhInfo(ByVal rsRegNo As String) As String
            Dim sFn As String = "FindAboRh"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT abo || rh aborh FROM lr070m WHERE regno = :regno"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()

                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count < 1 Then
                    Return ""
                Else
                    Return dt.Rows(0).Item("aborh").ToString
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        Public Shared Function FindOrder_TUBECOLOR(ByVal rsTubecd As String) As String
            Dim sFn As String = " Public Shared Function FindOrder_TUBECOLOR(ByVal rsTubecd As String) As String"
            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT tubecolor FROM lf040m"
                sSql += "  WHERE tubecd = :tubecd "

                al.Add(New OracleParameter("tubecd", OracleDbType.Varchar2, rsTubecd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTubecd))


                DbCommand()

                dt = DbExecuteQuery(sSql, al)

                If dt.Rows(0).Item("tubecolor").ToString = "" Then
                    Return ""
                Else
                    Return dt.Rows(0).Item("tubecolor").ToString
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        Public Shared Function FindOrder_AboRh(ByVal rsRegNo As String, ByVal rsOrdDt As String) As String
            Dim sFn As String = "FindAboRh"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT fn_ack_get_aborh_orderyn(:regno, :orddt) orddt FROM DUAL"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))

                DbCommand()

                dt = DbExecuteQuery(sSql, al)

                If dt.Rows(0).Item("orddt").ToString = "" Then
                    Return ""
                Else
                    Return "1"
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_BcList(ByVal rsRegno As String, ByVal rsPrtDt As String) As DataTable
            Dim sFn As String = ""

            Try
                rsPrtDt = rsPrtDt.Replace("-", "")

                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_prt(j0.bcno) bcnoprt,"
                sSql += "       fn_ack_get_date_string(j0.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_date_string(j0.bcprtdt, 'yyyy-mm-dd hh24:mi') bcprtdt,"
                'sSql += "       fn_ack_get_test_name_list(j0.bcno) testnms,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(b, ',' || b.tnmd)).extract('//text()'), 2)"
                sSql += "          FROM lj011m a, lf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) testnms,"
                sSql += "       j0.bcclscd, f1.colorgbn, j0.spcflg,"
                sSql += "       CASE WHEN j0.bcclscd IN (SELECT bcclscd FROM lf010m WHERE bcclsgbn = '2')"
                sSql += "            THEN (SELECT MIN(rstflg) FROM lm010m WHERE bcno = j1.bcno AND tclscd = j1.tclscd AND NVL(rstflg, '0') > '0')"
                sSql += "            ELSE (SELECT MIN(rstflg) FROM lr010m WHERE bcno = j1.bcno AND tclscd = j1.tclscd AND NVL(rstflg, '0') > '0')"
                sSql += "       END rstflg"
                sSql += "  FROM lj010m j0, lj011m j1, lf060m f6, lf010m f1"
                sSql += " WHERE j0.regno    = :regno"
                sSql += "   AND j0.bcprtdt >= :prtdt || '000000'"
                sSql += "   AND j0.bcprtdt <= :prtdt || '235959'"
                sSql += "   AND j0.owngbn  <> 'H'"
                sSql += "   AND CASE WHEN NVL(j0.spcflg, '0') = 'R' THEN '0' ELSE NVL(j0.spcflg, '0') END > '0'"
                sSql += "   AND j0.bcno     = j0.bcno"
                sSql += "   AND j1.tclscd   = f6.testcd"
                sSql += "   AND j1.spccd    = f6.spccd"
                sSql += "   AND j0.bcprtdt >= f6.usdt"
                sSql += "   AND j0.bcprtdt <  f6.uedt"
                sSql += "   AND j0.bcclscd  = f1.bcclscd"
                sSql += "   AND j0.bcprtdt >= f1.usdt"
                sSql += "   AND j0.bcprtdt <  f1.uedt"
                sSql += "  ORDER BY bcprtdt DESC"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                al.Add(New OracleParameter("prtdt", OracleDbType.Varchar2, rsPrtDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPrtDt))
                al.Add(New OracleParameter("prtdt", OracleDbType.Varchar2, rsPrtDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPrtDt))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


    End Class

#Region " 채 혈 : Class DB_Collect DB_Collect "
    Public Class CollReg_Web

        Private Const msFile As String = "File : CGDA_C.vb, Class : LISAPP.APP_C.CollReg_Web" & vbTab

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Private malBCNO As New ArrayList                ' 검체번호 리스트
        Private malCollectData As New ArrayList         ' 채혈내역  리스트

        Private miCollectItemCnt As Integer             ' 채혈된 검사항목 수
        Private msBCPrtMsg As String = ""               ' 출력 바코드 메세지
        Private mblnBCNO_ORDDT_GBN As Boolean = True    ' 바코드생성 규칙 true : 처방일시(default), false : 처방일
        Private mblnOrderGbn As Boolean = True          ' 오더유무

        '> 연속검사 샘플용 구분자
        Private miPlural As Integer = 0
        Private msBcNoBuf As String = ""

        Private msSTAT_ORDER As String = ""

        Public Sub New()
            MyBase.New()
        End Sub

        Public ReadOnly Property BCNO() As ArrayList
            Get
                BCNO = malBCNO
            End Get
        End Property

        Public ReadOnly Property CollectItemCnt() As Integer
            Get
                CollectItemCnt = miCollectItemCnt
            End Get
        End Property

        Public ReadOnly Property BCPrtMsg() As String
            Get
                BCPrtMsg = msBCPrtMsg
            End Get
        End Property

        Public WriteOnly Property CollectData() As ArrayList
            Set(ByVal Value As ArrayList)
                malCollectData = Value
            End Set
        End Property

        Private Function fnGet_Server_DateTime() As String

            Dim sFn As String = "Private Function fnGet_Server_DateTime() As string"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDA As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql += "SELECT fn_ack_date_str(fn_ack_sysdate, 'yyyy-mm-dd hh24:mi:ss') srvdate FROM DUAL"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDA = New OracleDataAdapter(dbCmd)

                dt.Reset()
                dbDA.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("srvdate").ToString()
                Else
                    Return Format(Now, "yyyy-MM-dd HH:mm:ss").ToString
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
                Return Format(Now, "yyyy-MM-dd HH:mm:ss").ToString
            End Try

        End Function

        '> 개별 채혈
        Public Function ExecuteDo(ByVal r_al_bcinfo As ArrayList, _
                                  ByVal r_stu_diag As STU_DiagInfo, _
                                  ByVal rsForm As String, _
                                  ByVal rsPrinterName As String, _
                                  ByVal rbToColl As Boolean, _
                                  ByVal rbAutoTkMode As Boolean, _
                                  ByVal rbBcPrt As Boolean) As ArrayList
            Dim sFn As String = "Public Function ExecuteDo(ArrayList, ArrayList, String, Boolean, Boolean) As ArrayList"

            Dim al_return As New ArrayList

            Try
                Dim sPartGbn As String = ""

                For ix1 As Integer = 0 To r_al_bcinfo.Count - 1
                    Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_bcinfo(ix1), List(Of STU_CollectInfo))
                    Dim stu_coll As New STU_COLLWEB

                    For ix2 As Integer = 0 To listcollData.Count - 1
                        Dim collData As STU_CollectInfo = listcollData.Item(ix2)

                        If PRG_CONST.BCCLS_RIS.Contains(collData.BCCLSCD) Then
                            sPartGbn = "ris"
                        Else
                            sPartGbn = "lis"
                        End If

                        stu_coll.BCCLSCD = collData.BCCLSCD
                        stu_coll.REGNO = collData.REGNO
                        stu_coll.ORDDT = collData.ORDDT.Substring(0, 8)
                        stu_coll.SPCFLG = IIf(rbAutoTkMode, "4", IIf(rbToColl, "2", "1").ToString).ToString
                        stu_coll.SERIES = collData.SERIES
                        stu_coll.STATGBN = collData.STATGBN
                        stu_coll.OWNGBN = collData.OWNGBN
                        stu_coll.IOGBN = collData.IOGBN
                        stu_coll.BCNO = collData.BCNO
                        stu_coll.DIAGCD = r_stu_diag.DIAGCD
                        stu_coll.DIAGNM = r_stu_diag.DIAGNM
                        stu_coll.DIAGNM_ENG = r_stu_diag.DIAGNM_ENG

                        stu_coll.HEIGHT = collData.HEIGHT
                        stu_coll.WEIGHT = collData.WEIGHT

                        stu_coll.SPCCD = collData.SPCCD

                        If ix2 > 0 Then
                            stu_coll.FKOCS += ","
                            stu_coll.TCLSCD += ","
                        End If

                        stu_coll.TCLSCD += collData.TCLSCD

                        If collData.OWNGBN = "L" Then
                            stu_coll.IOFLAG = ""
                            stu_coll.FKOCS += collData.FKOCS
                        Else
                            stu_coll.IOFLAG = collData.FKOCS.Split("/"c)(0)
                            stu_coll.FKOCS += collData.FKOCS.Split("/"c)(3)
                        End If

                        stu_coll.ERPRTYN = collData.ERPRTYN '<<<20180802 응급프린트

                    Next

                    Dim sReturn As String = ""


                    'sReturn = ExecuteDo_One(stu_coll, rbToColl, rbAutoTkMode)
                    sReturn = (New WEBSERVER.CGWEB_C).ExecuteDo_One(stu_coll, rbToColl, rbAutoTkMode, sPartGbn) '

                    If sReturn <> "" Then
                        For Each collData As STU_CollectInfo In listcollData
                            collData.DEPTCD = collData.DEPTABBR
                            collData.WARDNO = collData.WARDABBR
                            collData.PRTBCNO = (New LISAPP.APP_DB.DbFn).GetViewToBCPrt(sReturn)
                            collData.BCNO = sReturn


                            '<< JJH 자체응급 테이블 INSERT
                            If collData.ERPRTYN = "Y" Then
                                ExecuteDo_ER(collData.BCNO, collData.REGNO, collData.COLLDT)
                            End If
                            '>>

                        Next

                        If rbBcPrt Then
                            Dim arr As New ArrayList
                            arr.Add(listcollData)
                            Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(rsForm)
                            objBCPrt.PrintDoBarcode(arr, 1, rsForm, True, rsPrinterName)
                        End If

                        al_return.Add(listcollData)
                    End If
                Next

                Return al_return

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '<< JJH 자체응급 테이블 INSERT
        Public Function ExecuteDo_ER(ByVal rsBcno As String, ByVal rsRegno As String, ByVal rsColldt As String) As Integer
            Dim sFn As String = "Public Function ExecuteDo_ER(ByVal rsBcno As String, ByVal rsRegno As String) As Integer"

            Try

                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                Dim sSql As String = ""
                Dim al As New ArrayList

                Dim dbCmd As New OracleCommand
                Dim dbDA As OracleDataAdapter
                Dim dt As New DataTable

                Dim sDonRegNo As String = ""
                Dim iRet As Integer = 0

                dbCmd.Connection = m_dbCn

                If m_dbTran IsNot Nothing Then
                    If m_dbTran.Connection IsNot Nothing Then
                        dbCmd.Transaction = m_dbTran
                    End If
                End If

                sSql = ""
                sSql += " SELECT BCNO "
                sSql += "   FROM LJ015M "
                sSql += "  WHERE BCNO = :BCNO"
                sSql += "    AND REGNO = :REGNO "

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDA = New OracleDataAdapter(dbCmd)

                With dbDA
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .SelectCommand.Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegno
                End With

                dt.Reset()
                dbDA.Fill(dt)

                If dt.Rows.Count > 0 Then Return 1

                sSql = ""
                sSql += " INSERT INTO LJ015M "
                sSql += "             ( BCNO,  REGNO,     REGDT        ) "
                sSql += "      VALUES (:BCNO, :REGNO, fn_ack_sysdate() ) "

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegno

                    iRet = .ExecuteNonQuery

                End With

                m_dbTran.Commit()

                Return 1

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing
            End Try
        End Function

        Public Function ExecuteDo_One(ByVal r_stu_coll As STU_COLLWEB, ByVal rbToColl As Boolean, ByVal rbToTk As Boolean) As String
            Dim sFn As String = "Public Function ExecuteDo_One(List(Of STU_CollectInfo), ArrayList, Boolean) As String"

            Try

                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                r_stu_coll.COLLDT = fnGet_Server_DateTime()
                '> get bcno
                Dim sBcNo As String = fnGet_NewBcNo(r_stu_coll)


                If sBcNo = "" Then
                    m_dbTran.Rollback()
                    Return ""
                End If

                r_stu_coll.BCNO = sBcNo

                '> set ocs
                Dim iRows As Integer = ExecuteDo_Set_OrderState(r_stu_coll, rbToColl)
                If iRows <> r_stu_coll.TCLSCD.Split(","c).Length And r_stu_coll.TCDGBN <> "G" Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj011m
                iRows = ExecuteDo_One_AddColl(sBcNo, r_stu_coll, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj010m
                iRows = ExecuteDo_One_AddSpc(sBcNo, r_stu_coll, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj030m
                ExecuteDo_One_AddHeight(sBcNo, r_stu_coll)

                '> add diag info -> lj040m
                ExecuteDo_One_AddDiag(sBcNo, r_stu_coll)

                iRows = ExecuteDo_One_Exists(sBcNo, r_stu_coll.TCLSCD)
                If iRows = 0 Then
                    m_dbTran.Rollback()
                    Return ""
                End If

                If rbToTk Then
                    '> 접수작업까지 처리
                    iRows = ExecuteDo_One_AddTake(sBcNo)

                    If iRows = 0 Then
                        m_dbTran.Rollback()

                        Return ""
                    End If
                End If

                m_dbTran.Commit()
                Return sBcNo

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Public Function ExecuteDo_Set_OrderState(ByVal r_stu As STU_COLLWEB, ByVal rsToColl As Boolean) As Integer
            Dim sFn As String = "Public Shared Function ExecuteDo_Set_OrderState(ChgCollState) As Integer"

            Dim dbCmd As New OracleCommand
            Dim iRows As Integer = 0
            Dim sSql As String = ""
            Dim alFkOcs As New ArrayList

            Try
                For ix As Integer = 0 To r_stu.FKOCS.Split(","c).Length - 1

                    If r_stu.FKOCS.Split(","c)(ix) = "" Then Exit For

                    If alFkOcs.Contains(r_stu.FKOCS.Split(","c)(ix)) = False Then
                        sSql = "pro_ack_exe_ocs_coll"

                        With dbCmd
                            .Connection = m_dbCn

                            If m_dbTran IsNot Nothing Then
                                If m_dbTran.Connection IsNot Nothing Then
                                    .Transaction = m_dbTran
                                End If
                            End If

                            .CommandType = CommandType.StoredProcedure
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add(New OracleParameter("rs_regno", r_stu.REGNO))
                            .Parameters.Add(New OracleParameter("rs_owngbn", r_stu.OWNGBN))

                            Dim sFkOcs As String = ""
                            If r_stu.OWNGBN = "L" Then
                                sFkOcs = r_stu.FKOCS.Split(","c)(ix)
                            Else
                                sFkOcs = r_stu.IOFLAG + "/" + r_stu.REGNO + "/" + r_stu.ORDDT.Substring(0, 8) + "/" + r_stu.FKOCS.Split(","c)(ix)
                            End If

                            .Parameters.Add(New OracleParameter("rs_fkocs", sFkOcs))
                            .Parameters.Add(New OracleParameter("rs_bcno", r_stu.BCNO))

                            If rsToColl Then
                                .Parameters.Add(New OracleParameter("rs_spcflg", "2"))
                                .Parameters.Add(New OracleParameter("rs_acptdt", r_stu.COLLDT.Replace("-", "").Replace(" ", "").Replace(":", "")))
                            Else
                                .Parameters.Add(New OracleParameter("rs_spcflg", "1"))
                                .Parameters.Add(New OracleParameter("rs_acptdt", r_stu.COLLDT.Replace("-", "").Replace(" ", "").Replace(":", "")))
                            End If

                            .Parameters.Add(New OracleParameter("rs_usrid", USER_INFO.USRID))
                            .Parameters.Add(New OracleParameter("rs_ip", USER_INFO.LOCALIP))

                            .Parameters.Add("ri_retval", OracleDbType.Int32)
                            .Parameters("ri_retval").Direction = ParameterDirection.Output
                            .Parameters("ri_retval").Value = -1

                            .Parameters.Add("rs_retmsg", OracleDbType.Varchar2)
                            .Parameters("rs_retmsg").Size = 2000
                            .Parameters("rs_retmsg").Direction = ParameterDirection.Output
                            .Parameters("rs_retmsg").Value = ""

                            .ExecuteNonQuery()

                            iRows += CType(.Parameters(8).Value.ToString, Integer)

                            Dim sEsg As String = .Parameters(9).Value.ToString


                        End With

                        alFkOcs.Add(r_stu.FKOCS.Split(","c)(ix))
                    End If
                Next

                Return iRows

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddColl(ByVal rsBcNo As String, ByVal r_stu As STU_COLLWEB, ByVal rbToColl As Boolean) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddColl(String, List(Of STU_CollectInfo), Boolean) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0
            Dim iRows As Integer = 0

            Try
                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    For ix As Integer = 0 To r_stu.TCLSCD.Split(","c).Length - 1
                        If r_stu.TCLSCD.Split(","c)(ix) = "" Then Exit For

                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "pro_ack_exe_coll_lj011m"

                        .Parameters.Clear()
                        .Parameters.Add("rs_regno", OracleDbType.Varchar2).Value = r_stu.REGNO
                        .Parameters.Add("rs_orddt", OracleDbType.Varchar2).Value = r_stu.ORDDT.Substring(0, 8)
                        .Parameters.Add("rs_ordno", OracleDbType.Varchar2).Value = r_stu.FKOCS.Split(","c)(ix)
                        .Parameters.Add("rs_testcd", OracleDbType.Varchar2).Value = r_stu.TCLSCD.Split(","c)(ix)
                        .Parameters.Add("rs_spccd", OracleDbType.Varchar2).Value = r_stu.SPCCD
                        .Parameters.Add("rs_ioflag", OracleDbType.Varchar2).Value = r_stu.IOFLAG
                        .Parameters.Add("rs_owngbn", OracleDbType.Varchar2).Value = r_stu.OWNGBN

                        .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                        .Parameters.Add("rs_colldt", OracleDbType.Varchar2).Value = r_stu.COLLDT.Replace("-", "").Replace(":", "").Replace(" ", "")

                        If rbToColl Then
                            .Parameters.Add("rs_spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_Coll
                        Else
                            .Parameters.Add("rs_spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_BcPrt
                        End If

                        .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("rs_retval", OracleDbType.Varchar2)
                        .Parameters("rs_retval").Size = 2000
                        .Parameters("rs_retval").Direction = ParameterDirection.Output
                        .Parameters("rs_retval").Value = ""

                        .ExecuteNonQuery()

                        Dim sMsgErr As String = .Parameters(12).Value.ToString

                        If .Parameters(12).Value.ToString = "00" Then
                            iRows += 1
                        End If
                    Next

                    Return iRows


                    .CommandType = CommandType.Text

                    Dim sTableNm As String = "lf"
                    If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "rf"

                    sSql = ""

                    If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                        sSql += "INSERT INTO rj011m("
                    Else
                        sSql += "INSERT INTO lj011m("
                    End If

                    sSql += "            bcno, tclscd, spccd, regno, "

                    If rbToColl Then sSql += "collid, colldt,"

                    sSql += "            owngbn,   iogbn,     fkocs, orddt, doctorrmk, collvol, spcflg,"
                    sSql += "            orgorddt, orgdeptcd, orgdoctorcd, ordslip, ocs_key, sysdt,"
                    sSql += "            editdt,   editid, editip"
                    sSql += "          ) "

                    If r_stu.OWNGBN = "O" Then
                        sSql += "SELECT :bcno, f6.testcd, f6.spccd, o.patno,"
                        If rbToColl Then
                            sSql += ":collid, :colldt,"
                        End If
                        sSql += "       :owngbn, o.iogbn, o.ioflag || '/' || o.patno || '/' || o.orddate || '/' || TRIM(TO_CHAR(o.ordseqno)), :orddt, o.ordtext, f6.minspcvol, :spcflg,"
                        sSql += "       o.orddate || o.ordtime, o.deptcd, o.orddr, f6.tordslip, o.ordseqno, fn_ack_sysdate,"
                        sSql += "       fn_ack_sysdate, :editid, :editip"
                        sSql += "  FROM vw_ack_ord_info o, " + sTableNm + "060m f6"
                        sSql += " WHERE o.instcd        = '" + PRG_CONST.SITECD + "'"
                        sSql += "   AND o.procstat     >= '100'"
                        sSql += "   AND o.procstat     <  '400'"
                        sSql += "   AND o.patno         = :regno"
                        sSql += "   AND o.orddate       = :ordday"
                        sSql += "   AND o.ordseqno    IN (" + r_stu.FKOCS + ")"
                        sSql += "   AND o.ioflag        = '" + r_stu.IOFLAG + "'"
                        sSql += "   AND o.ordcd         = f6.tordcd"
                        sSql += "   AND o.spccd         = f6.spccd"
                        sSql += "   AND f6.usdt        <= fn_ack_sysdate"
                        sSql += "   AND f6.uedt        >  fn_ack_sysdate"
                        sSql += "   AND f6.tcdgbn      IN ('S', 'B', 'P')"
                        sSql += "   AND f6.testcd      IN ('" + r_stu.TCLSCD.Replace(",", "','") + "')"
                        sSql += "   AND f6.spccd        = :spccd"
                        sSql += " UNION "
                        sSql += "SELECT :bcno, f6.testcd, f6.spccd, o.patno,"
                        If rbToColl Then
                            sSql += ":collid, :colldt,"
                        End If
                        sSql += "       :owngbn, o.iogbn, o.ioflag || '/' || o.patno || '/' || o.orddate || '/' || TRIM(TO_CHAR(o.ordseqno)), :orddt, o.ordtext, f6.minspcvol, :spcflg,"
                        sSql += "       o.orddate || o.ordtime, o.deptcd, o.orddr, f6.tordslip, o.ordseqno, fn_ack_sysdate,"
                        sSql += "       fn_ack_sysdate, :editid, :editip"
                        sSql += "  FROM vw_ack_ord_info o, " + sTableNm + "060m fo"
                        sSql += "       (SELECT a.tclscd, a.tspccd, a.testcd, a.spccd, b.minspcvol, b.tordslip"
                        sSql += "          FROM " + sTableNm + "062m a, " + sTableNm + "060m b"
                        sSql += "         WHERE a.testcd  = b.testcd"
                        sSql += "           AND a.tspccd  = b.spccd"
                        sSql += "           AND b.usdt   <= fn_ack_sysdate"
                        sSql += "           AND b.uedt   >  fn_ack_sysdate"
                        sSql += "       ) f6"
                        sSql += " WHERE o.instcd        = '031'"
                        sSql += "   AND o.patno         = :regno"
                        sSql += "   AND o.orddate       = :ordday"
                        sSql += "   AND o.ordseqno     IN (" + r_stu.FKOCS + ")"
                        sSql += "   AND o.ioflag        = '" + r_stu.IOFLAG + "'"
                        sSql += "   AND o.ord cd        = f.tordcd"
                        sSql += "   AND o.spccd         = f.tspccd"
                        sSql += "   AND f.usdt        <= fn_ack_sysdate"
                        sSql += "   AND f.uedt        >  fn_ack_sysdate"
                        sSql += "   AND f6.tcdgbn       = 'G'"
                        sSql += "   AND fo.testcd       = f6.testcd"
                        sSql += "   AND fo.spccd        = f6.spccd"
                        sSql += "   AND f6.testcd      IN ('" + r_stu.TCLSCD.Replace(",", "','") + "')"
                        sSql += "   AND f6.spccd        = :spccd"
                    Else
                        sSql += "SELECT :bcno, f6.testcd, f6.spccd, o.patno,"
                        If rbToColl Then
                            sSql += ":collid, :colldt,"
                        End If
                        sSql += "       :owngbn, o.in_out_gubun, fkocs, :orddt, o.remark, f6.minspcvol, :spcflg,"
                        sSql += "       o.order_date || o.order_time, o.gwq, o.doctor, f6.tordslip, 0, fn_ack_sysdate,"
                        sSql += "       fn_ack_sysdate, :editid, :editip"
                        sSql += "  FROM mts0001_lis o, " + sTableNm + "060m f6"
                        sSql += " WHERE o.bunho         = :regno"
                        sSql += "   AND o.order_date    = :ordday"
                        sSql += "   AND o.fkocs        IN ('" + r_stu.FKOCS.Replace(",", "','") + "')"
                        sSql += "   AND o.hangmog_code  = f6.tordcd"
                        sSql += "   AND o.specimen_code = f6.spccd"
                        sSql += "   AND f6.usdt        <= fn_ack_sysdate"
                        sSql += "   AND f6.uedt        >  fn_ack_sysdate"
                        sSql += "   AND f6.tcdgbn      IN ('S', 'B', 'P')"
                        sSql += "   AND f6.testcd      IN ('" + r_stu.TCLSCD.Replace(",", "','") + "')"""
                        sSql += "   AND f6.spccd        = :spccd"
                        sSql += " UNION "
                        sSql += "SELECT :bcno, f6.testcd, f6.spccd, o.patno,"
                        If rbToColl Then
                            sSql += ":collid, :colldt,"
                        End If
                        sSql += "       :owngbn, o.in_out_gubun, fkocs, :orddt, o.remark, f6.minspcvol, :spcflg,"
                        sSql += "       o.order_date || o.order_time, o.gwq, o.doctor, f6.tordslip, 0, fn_ack_sysdate,"
                        sSql += "       fn_ack_sysdate, :editid, :editip"
                        sSql += "  FROM mts0001_lis o, " + sTableNm + "060m fo"
                        sSql += "       (SELECT a.tclscd, a.tspccd, a.testcd, a.spccd, b.minspcvol, b.tordslip"
                        sSql += "          FROM " + sTableNm + "062m a, " + sTableNm + "060m b"
                        sSql += "         WHERE a.testcd  = b.testcd"
                        sSql += "           AND a.tspccd  = b.spccd"
                        sSql += "           AND b.usdt   <= fn_ack_sysdate"
                        sSql += "           AND b.uedt   >  fn_ack_sysdate"
                        sSql += "       ) f6"
                        sSql += " WHERE o.bunho         = :regno"
                        sSql += "   AND o.order_date    = :ordday"
                        sSql += "   AND o.fkocs        IN ('" + r_stu.FKOCS.Replace(",", "','") + "')"
                        sSql += "   AND o.ioflag        = '" + r_stu.IOFLAG + "'"
                        sSql += "   AND o.hangmog_code  = f.tordcd"
                        sSql += "   AND o.specimen_code = f.spccd"
                        sSql += "   AND f.usdt         <= fn_ack_sysdate"
                        sSql += "   AND f.uedt         >  fn_ack_sysdate"
                        sSql += "   AND f6.tcdgbn       = 'G'"
                        sSql += "   AND fo.testcd       = f6.testcd"
                        sSql += "   AND fo.spccd        = f6.spccd"
                        sSql += "   AND f6.testcd      IN ('" + r_stu.TCLSCD.Replace(",", "','") + "')"""
                        sSql += "   AND f6.spccd        = :spccd"
                    End If

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    If rbToColl Then
                        .Parameters.Add("collid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("colldt", OracleDbType.Varchar2).Value = r_stu.COLLDT.Replace("-", "").Replace(":", "").Replace(" ", "")
                    End If

                    .Parameters.Add("owngbn", OracleDbType.Varchar2).Value = r_stu.OWNGBN
                    .Parameters.Add("orddt", OracleDbType.Varchar2).Value = r_stu.ORDDT

                    If rbToColl Then
                        .Parameters.Add("spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_Coll
                    Else
                        .Parameters.Add("spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_BcPrt
                    End If

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP


                    '-- where
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_stu.REGNO
                    .Parameters.Add("ordday", OracleDbType.Varchar2).Value = r_stu.ORDDT.Substring(0, 8)
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_stu.SPCCD

                    iRow = .ExecuteNonQuery()
                    iRows += iRow
                End With

                Return iRows

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddSpc(ByVal rsBcNo As String, ByVal r_stu As STU_COLLWEB, ByVal rbToColl As Boolean) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddSpc(String, STU_COLLWEB, Boolean) As Integer"

            Dim dbCmd As New OracleCommand

            Try
                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_coll_lj010m"

                    .Parameters.Clear()
                    .Parameters.Add("rs_regno", OracleDbType.Varchar2).Value = r_stu.REGNO
                    .Parameters.Add("rs_orddt", OracleDbType.Varchar2).Value = r_stu.ORDDT.Substring(0, 8)
                    .Parameters.Add("rs_ordno", OracleDbType.Varchar2).Value = r_stu.FKOCS.Split(","c)(0)
                    .Parameters.Add("rs_ioflag", OracleDbType.Varchar2).Value = r_stu.IOFLAG

                    .Parameters.Add("rs_owngbn", OracleDbType.Varchar2).Value = r_stu.OWNGBN
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_ergbn", OracleDbType.Varchar2).Value = r_stu.STATGBN

                    If rbToColl Then
                        .Parameters.Add("rs_spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_Coll
                    Else
                        .Parameters.Add("rs_spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_BcPrt
                    End If

                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2)
                    .Parameters("rs_retval").Size = 2000
                    .Parameters("rs_retval").Direction = ParameterDirection.Output
                    .Parameters("rs_retval").Value = ""

                    .ExecuteNonQuery()

                    If .Parameters(10).Value.ToString = "00" Then
                        Return 1
                    Else
                        Return 0
                    End If

                End With

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddHeight(ByVal rsBcNo As String, ByVal r_stu As STU_COLLWEB) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddHeight(String, List(Of STU_CollectInfo)) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 1

            Try

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                        sSql += "INSERT INTO rj012m( bcno, height, weight )"
                    Else
                        sSql += "INSERT INTO lj012m( bcno, height, weight )"
                    End If
                    sSql += "    VALUES( :bcno, :height, :weight )"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    If r_stu.HEIGHT = "" Then
                        .Parameters.Add("height", OracleDbType.Varchar2).Value = DBNull.Value
                    Else
                        .Parameters.Add("height", OracleDbType.Varchar2).Value = r_stu.HEIGHT
                    End If

                    .Parameters.Add("weight", OracleDbType.Varchar2).Value = r_stu.WEIGHT

                    iRow = .ExecuteNonQuery()
                End With

                Return iRow

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddDiag(ByVal rsBcNo As String, ByVal r_stu As STU_COLLWEB) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddDiag(String, ArrayList) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try
                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""

                    If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                        sSql += "INSERT INTO rj013m ( bcno, diagnm, diagnm_eng )"
                    Else
                        sSql += "INSERT INTO lj013m ( bcno, diagnm, diagnm_eng )"
                    End If

                    sSql += "    VALUES( :bcno, :diagnm, :diagnme )"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("diagnm", OracleDbType.Varchar2).Value = r_stu.DIAGNM
                    .Parameters.Add("diagnme", OracleDbType.Varchar2).Value = r_stu.DIAGNM_ENG

                    iRow = .ExecuteNonQuery()
                End With

                Return iRow

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_Exists(ByVal rsBcNo As String, ByVal rsTestCds As String) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_Exists(String, ArrayList) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Dim sDiagNm As String = ""
            Dim sDiagNmE As String = ""

            Try
                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""

                    If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                        sSql += "UPDATE rj011m SET spcflg = spcflg"
                        sSql += " WHERE fkocs  IN (SELECT fkocs FROM rj011m WHERE bcno = :bcno)"
                        sSql += "   AND bcno   <> :bcno"
                        sSql += "   AND tclscd IN ('" + rsTestCds.Replace(",", "','") + "')"
                        sSql += "   AND spcflg NOT IN ('0', 'R')"

                    Else
                        sSql += "UPDATE lj011m SET spcflg = spcflg"
                        sSql += " WHERE fkocs  IN (SELECT fkocs FROM lj011m WHERE bcno = :bcno)"
                        sSql += "   AND bcno   <> :bcno"
                        sSql += "   AND tclscd IN ('" + rsTestCds.Replace(",", "','") + "')"
                        sSql += "   AND spcflg NOT IN ('0', 'R')"

                    End If

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRow = .ExecuteNonQuery()
                End With

                If iRow > 0 Then
                    Return 0
                Else
                    Return 1
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddTake(ByVal rsBcNo As String) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddTake(String) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try
                Dim sErrVal As String = ""

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_take_ocs"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_wknoyn", OracleDbType.Varchar2).Value = "N"
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(4).Value.ToString
                End With

                If IsNumeric(sErrVal.Substring(0, 2)) Then
                    If sErrVal.Substring(0, 2) = "00" Then
                        '정상적으로 접수
                        Return 1
                    Else
                        '이미 접수된 검체번호 or '검사항목 조회 오류
                        Return 0
                    End If
                Else
                    '기타 오류
                    Return 0
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_Add_Doner(ByVal rsBcNo As String, ByVal r_listcollData As List(Of STU_CollectInfo)) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_Add_Doner(String, List(Of STU_CollectInfo)) As Integer"


            Try
                Dim sSql As String = ""
                Dim iRow As Integer = 0

                Dim dbCmd As New OracleCommand
                Dim dbDA As OracleDataAdapter
                Dim dt As New DataTable

                Dim sJubSuDt As String = r_listcollData.Item(0).COLLDT

                Dim sDonRegNo As String = ""
                Dim iRet As Integer = 0

                dbCmd.Connection = m_dbCn

                If m_dbTran IsNot Nothing Then
                    If m_dbTran.Connection IsNot Nothing Then
                        dbCmd.Transaction = m_dbTran
                    End If
                End If

                sSql = ""
                sSql += "SELECT o.bunho"
                sSql += "  FROM mts0001_lis o, lf060m f"
                sSql += " WHERE o.bunho = :regno"
                sSql += "   AND o.hangmog_code = f.tordcd"
                sSql += "   AND o.specimen_code = f.spccd"
                sSql += "   AND f.usdt <= fn_ack_sysdate"
                sSql += "   AND f.uedt >  fn_ack_sysdate"
                sSql += "   AND f.bbtype IN ('5', '7', 'A', 'B', 'C', 'D')"
                sSql += "   AND (o.fkocs) IN (SELECT fkocs FROM lj011m WHERE bcno = :bcno)"
                sSql += "   AND o.spcflg = '1'"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDA = New OracleDataAdapter(dbCmd)

                With dbDA
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dt.Reset()
                dbDA.Fill(dt)

                If dt.Rows.Count < 1 Then Return 1

                With dbCmd
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_exe_seqno_donregno"

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = sJubSuDt.Substring(0, 4)

                    .Parameters.Add("retval", OracleDbType.Varchar2, 4000)
                    .Parameters("retval").Direction = ParameterDirection.InputOutput
                    .Parameters("retval").Value = ""

                    .ExecuteNonQuery()

                    sDonRegNo = sJubSuDt.Substring(0, 4) + .Parameters.Item(1).Value.ToString.PadLeft(6, "0"c)
                End With

                sSql = ""
                sSql += "INSERT INTO lb010m("
                sSql += "            donregno, donregdt,  dongbn,   donexpcnt, rctdondt, haddress, hadd_delail, htel,  celphone, age,"
                sSql += "            donseq,   doncnt,    juminno,  patnm,     sex,      donflg,   owngbn,      iogbn, fkocs,    regno,"
                sSql += "            tnsnm,    tnssexage, tnsjumin, orddate,   tordcd,   bcno)"
                sSql += "select :donregno, :donregdt, CASE WHEN f.bbttype IN ('5', '7') THEN '4' ELSE '3' END, 0, :rctdondt, p.address1, p.address2, p.tel1, p.tel2, :age,"
                sSql += "       CASE WHEN f.bbttype = 'A' THEN '1' WHEN f.bbttype = 'B' THEN '2' WHEN f.bbttype = 'C' THEN '3' WHEN f.bbttype = 'D' THEN '4' ELSE '' END,"
                sSql += "       0, p.sujumin1 || sujumin2, p.suname, p.sex, '0', o.owngbn, o.in_out_gubun, o.fkocs, o.bunho,"
                sSql += "       p.suname, p.sex || '/'|| :age, p.sujumin1 || p.sujumin2, o.order_date, o.hangmog_code, :bcno"
                sSql += "  FROM mts0001_lis o,"
                sSql += "      (SELECT bunho, suname, sujumin1, sujumin2, address1, address2, tel1, tel2, sex, owngbn"
                sSql += "         FROM (SELECT 0 seq, bunho, suname, birth, sujumin1, sujumin2, zip_code1, zip_code2, address1, address2,"
                sSql += "                      tel1, tel2, sex, 'O' owngbn"
                sSql += "                 FROM ocs_db..vw_mts0002"
                sSql += "                WHERE bunho = :regno"
                sSql += " 	             UNION ALL"
                sSql += "               SELECT 0 seq, bunho, suname, birth, sujumin1, sujumin2, zip_code1, zip_code2, address1, address2,"
                sSql += "                      tel1, tel2, sex, 'L' owngbn"
                sSql += "                 FROM mtsS0002_lis"
                sSql += "                WHERE bunho = :regno"
                sSql += "              ) t"
                sSql += "        WHERE ROWNUM = 1"
                sSql += "        ORDER BY seq"
                sSql += "      ) p, lf060m f"
                sSql += " WHERE o.bunho = :regno"
                sSql += "   AND o.bunho = p.bunho"
                sSql += "   AND o.hangmog_code = f.tordcd"
                sSql += "   AND o.specimen_code = f.spccd"
                sSql += "   AND f.usdt <= fn_ack_sysdate"
                sSql += "   AND f.UEDT >  fn_ack_sysdate"
                sSql += "   AND f.bbttype IN ('5', '7', 'A', 'B', 'C', 'D')"
                sSql += "   AND (o.fkocs) IN (SELECT fkocs FROM lj011m WHERE bcno = :bcno)"
                sSql += "   AND o.spcflg ='1'"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("donregno", OracleDbType.Varchar2).Value = sDonRegNo
                    .Parameters.Add("donregdt", OracleDbType.Varchar2).Value = sJubSuDt
                    .Parameters.Add("rctdondt", OracleDbType.Varchar2).Value = sJubSuDt
                    .Parameters.Add("age", OracleDbType.Varchar2).Value = r_listcollData.Item(0).AGE
                    .Parameters.Add("age", OracleDbType.Varchar2).Value = r_listcollData.Item(0).AGE
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery

                End With

                If iRet < 1 Then Return 0

                If r_listcollData.Item(0).OWNGBN = "L" Then
                    sSql = "UPDATE mts0001_lis"
                Else
                    sSql = "UPDATE ocs_db..mts0001"
                End If
                sSql += "   SET spcflg = '1', colldt = :colldt"
                sSql += " WHERE bunho = :regno"
                sSql += "   AND (fkocs, hangmog_code) IN "
                sSql += "       (SELECT fkocs, tordcd FROM lb010m WHERE donregno = :donregno)"
                sSql += "   AND NVL(spcflg, '0') = '0'"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("colldt", OracleDbType.Varchar2).Value = r_listcollData.Item(0).COLLDT

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .Parameters.Add("donregno", OracleDbType.Varchar2).Value = sDonRegNo

                    iRet = .ExecuteNonQuery
                End With

                Return 1

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Protected Function ExecuteDo_One_GetCollDtPrtBcNo(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Protected Function ExecuteDo_One_AddEnt(String, ArrayList) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "SELECT fn_ack_sysdate sysdt, fn_ack_get_bcno_prt(:bcno) prtbcno FROM DUAL"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                Dim dbDa As New OracleDataAdapter(dbCmd)

                Dim dt As New DataTable

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_GetCollDtOwnGbn(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Protected Function ExecuteDo_One_GetCollDtOwnGbn(String, ArrayList) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try
                dbCmd = New OracleCommand

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "SELECT fn_ack_sysdate sysdt, owngbn"
                    sSql += "  FROM lj010m"
                    sSql += " WHERE bcno = :bcno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                Dim dbDa As New OracleDataAdapter(dbCmd)

                Dim dt As New DataTable

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Public Function ExecuteDo_Comment(ByVal r_listcollData As List(Of STU_CollectInfo)) As Boolean
            Dim sFn As String = "Public Sub ExecuteDo_Comment(List(Of STU_CollectInfo))"

            Try
                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim dbCmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim iRows As Integer = 0
                Dim sSql As String = ""

                For i As Integer = 1 To r_listcollData.Count
                    Dim collData As STU_CollectInfo = r_listcollData.Item(i - 1)

                    Dim css As New OcsLink.ChgOcsState

                    With css
                        .LabCmt = collData.COMMENT
                        .OwnGbn = collData.OWNGBN
                        .RegNo = collData.REGNO
                        .TotFkOcs = collData.FKOCS
                        .IOGBN = collData.IOGBN
                    End With

                    'Dim iRow As Integer = SetOrderChgLisCmt(css, m_dbCn, m_dbTran)

                    'If iRow > 0 Then
                    Dim iRow As Integer = 0

                    With dbCmd
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "PRO_ACK_EXE_COLL_LO010M"

                        .Parameters.Clear()
                        If collData.OWNGBN = "L" Then
                            .Parameters.Add("rs_regno", OracleDbType.Varchar2).Value = collData.REGNO
                            .Parameters.Add("rs_orddt", OracleDbType.Varchar2).Value = collData.ORDDT.Substring(0, 8)
                            .Parameters.Add("rs_ordno", OracleDbType.Varchar2).Value = collData.FKOCS
                            .Parameters.Add("rs_ioflag", OracleDbType.Varchar2).Value = ""
                        Else
                            .Parameters.Add("rs_regno", OracleDbType.Varchar2).Value = collData.FKOCS.Split("/"c)(1)
                            .Parameters.Add("rs_orddt", OracleDbType.Varchar2).Value = collData.FKOCS.Split("/"c)(2)
                            .Parameters.Add("rs_ordno", OracleDbType.Varchar2).Value = collData.FKOCS.Split("/"c)(3)
                            .Parameters.Add("rs_ioflag", OracleDbType.Varchar2).Value = collData.FKOCS.Split("/"c)(0)
                        End If

                        .Parameters.Add("rs_owngbn", OracleDbType.Varchar2).Value = collData.OWNGBN
                        .Parameters.Add("cmdcont", OracleDbType.Varchar2).Value = collData.COMMENT
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("rs_retval", OracleDbType.Varchar2)
                        .Parameters("rs_retval").Size = 2000
                        .Parameters("rs_retval").Direction = ParameterDirection.Output
                        .Parameters("rs_retval").Value = ""

                        .ExecuteNonQuery()

                        If dbCmd.Parameters(8).Value.ToString = "00" Then
                            iRow = 1
                        Else
                            Throw (New Exception(dbCmd.Parameters(8).Value.ToString.Substring(3)))
                        End If
                    End With

                    'End If
                    iRows += iRow
                Next

                If iRows > 0 Then
                    m_dbTran.Commit()

                    Return True
                Else
                    m_dbTran.Rollback()

                    Return False
                End If

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

        ' 채혈일시 등록
        Public Function ExecuteDo_CollDt(ByVal rsBcNo As String, ByVal rbTakeYn As Boolean) As Boolean
            Dim sFn As String = "Public Function ExecuteDo_CollDt(String, Boolean) As Boolean"

            Dim DbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try
                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sErrVal As String = ""

                With DbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_collector_colldt"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If IsNumeric(sErrVal.Substring(0, 2)) Then
                    If sErrVal.Substring(0, 2) = "00" Then

                        If rbTakeYn Then
                            Dim iRows As Integer = ExecuteDo_One_AddTake(rsBcNo)

                            If iRows = 0 Then
                                m_dbTran.Rollback()
                                Return False
                            End If
                        End If

                        m_dbTran.Commit()
                        Return True
                    Else
                        m_dbTran.Rollback()
                        Return False
                    End If
                Else
                    m_dbTran.Rollback()
                    Return False
                End If

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If DbCmd IsNot Nothing Then DbCmd = Nothing
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        ' 새로운 바코드번호 생성여부 판정.
        Public Function fnNewBCNO_Judge(ByVal aoCurRowData As STU_TestItemInfo, ByRef aoOldRowData As STU_TestItemInfo) As Boolean
            Dim sFn As String = "Private Function fnNewBCNO_Judge(ByVal aoCurRowData As clsTestItem_Info, ByRef aoOldRowData As clsTestItem_Info) As Boolean"
            Dim blnNewBCNO As Boolean = False

            Try
                '0. 등록번호가 틀리면 새로운 검체번호 발생
                If aoOldRowData.REGNo <> aoCurRowData.REGNo Then

                    aoOldRowData.REGNo = aoCurRowData.REGNo
                    blnNewBCNO = True
                End If

                '1. 검사계의 검체가 틀린경우 새로운 검체번호 발생
                If aoOldRowData.BCCLSCD <> aoCurRowData.BCCLSCD Or _
                   aoOldRowData.SPCCD <> aoCurRowData.SPCCD Then

                    aoOldRowData.BCCLSCD = aoCurRowData.BCCLSCD
                    aoOldRowData.SPCCD = aoCurRowData.SPCCD
                    blnNewBCNO = True
                End If

                '2. 처방일시가 틀린경우 조건에 따라 다른번호 발생
                If aoOldRowData.ORDDT <> aoCurRowData.ORDDT Then
                    aoOldRowData.ORDDT = aoCurRowData.ORDDT
                    ' 처방일시별로 바코드 생성될경우에만 체크(기본)
                    If mblnBCNO_ORDDT_GBN = True Then blnNewBCNO = True
                End If

                '2.1 진료과코드가 틀린경우 
                If aoOldRowData.DEPTCD <> aoCurRowData.DEPTCD Then
                    aoOldRowData.DEPTCD = aoCurRowData.DEPTCD
                    blnNewBCNO = True
                End If

                '2.2 의뢰의사가 틀린경우
                If aoOldRowData.DOCTORCD <> aoCurRowData.DOCTORCD Then
                    aoOldRowData.DOCTORCD = aoCurRowData.DOCTORCD
                    blnNewBCNO = True
                End If

                '3. 검사계의 검체가 같고, TubeCd가 틀린경우
                If aoOldRowData.TUBECD <> aoCurRowData.TUBECD Then
                    aoOldRowData.TUBECD = aoCurRowData.TUBECD
                    blnNewBCNO = True
                End If

                '4. 검사계의 검체가 같고, 외주검사 인경우
                If aoOldRowData.EXLABYN = "1" Then
                    blnNewBCNO = True
                End If

                '5. 검체코드가 틀린경우
                If aoOldRowData.BCNO <> aoCurRowData.BCNO Then
                    blnNewBCNO = True
                    aoOldRowData.BCNO = aoCurRowData.BCNO
                End If

                '6. 같은 검사인 경우 분리
                If aoOldRowData.TCLS_SPC = aoCurRowData.TESTCD + aoCurRowData.SPCCD Then
                    blnNewBCNO = True
                End If
                aoOldRowData.TCLS_SPC = aoCurRowData.TESTCD + aoCurRowData.SPCCD

                fnNewBCNO_Judge = blnNewBCNO

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        ' 새로운 바코드번호 생성여부 판정.
        Public Function fnNewBCNO_Judge_Bundle(ByVal aoCurRowData As STU_TestItemInfo, ByRef aoOldRowData As STU_TestItemInfo) As Boolean
            Dim sFn As String = "Private Function fnNewBCNO_Judge(ByVal aoCurRowData As clsTestItem_Info, ByRef aoOldRowData As clsTestItem_Info) As Boolean"
            Dim blnNewBCNO As Boolean = False

            Try
                '0. 등록번호가 틀리면 새로운 검체번호 발생
                If aoOldRowData.REGNo <> aoCurRowData.REGNo Then

                    aoOldRowData.REGNo = aoCurRowData.REGNo
                    blnNewBCNO = True
                End If

                '1. 검사계의 검체가 틀린경우 새로운 검체번호 발생
                If aoOldRowData.BCCLSCD <> aoCurRowData.BCCLSCD Or _
                   aoOldRowData.SPCCD <> aoCurRowData.SPCCD Then

                    aoOldRowData.BCCLSCD = aoCurRowData.BCCLSCD
                    aoOldRowData.SPCCD = aoCurRowData.SPCCD
                    blnNewBCNO = True
                End If

                '2. 처방일시가 틀린경우 조건에 따라 다른번호 발생
                If aoOldRowData.NRS_TIME <> aoCurRowData.NRS_TIME Then
                    aoOldRowData.NRS_TIME = aoCurRowData.NRS_TIME
                    ' 처방일시별로 바코드 생성될경우에만 체크(기본)
                    If mblnBCNO_ORDDT_GBN = True Then blnNewBCNO = True
                End If

                '2.1 진료과코드가 틀린경우 
                If aoOldRowData.DEPTCD <> aoCurRowData.DEPTCD Then
                    aoOldRowData.DEPTCD = aoCurRowData.DEPTCD
                    blnNewBCNO = True
                End If

                '2.2 의뢰의사가 틀린경우
                If aoOldRowData.DOCTORCD <> aoCurRowData.DOCTORCD Then
                    aoOldRowData.DOCTORCD = aoCurRowData.DOCTORCD
                    blnNewBCNO = True
                End If

                '3. 검사계의 검체가 같고, TubeCd가 틀린경우
                If aoOldRowData.TUBECD <> aoCurRowData.TUBECD Then
                    aoOldRowData.TUBECD = aoCurRowData.TUBECD
                    blnNewBCNO = True
                End If

                ''4. 검사계의 검체가 같고, 외주검사 인경우
                'If aoOldRowData.EXLABYN = "1" Then
                '    blnNewBCNO = True
                'End If

                '5. 검체코드가 틀린경우
                If aoOldRowData.BCNO <> aoCurRowData.BCNO Then
                    blnNewBCNO = True
                    aoOldRowData.BCNO = aoCurRowData.BCNO
                End If

                '6. 같은 검사인 경우 분리
                If aoOldRowData.TCLS_SPC = aoCurRowData.TESTCD + aoCurRowData.SPCCD Then
                    blnNewBCNO = True
                End If
                aoOldRowData.TCLS_SPC = aoCurRowData.TESTCD + aoCurRowData.SPCCD

                fnNewBCNO_Judge_Bundle = blnNewBCNO

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 이전검체번호와 같고 복수구분 증가여부 판정. 
        Public Function fnPluralBCNO_Judge(ByVal aoCurRowData As STU_TestItemInfo, ByVal abNewBCNO As Boolean, ByVal asSeqtYn As String) As Boolean
            Dim sFn As String
            Dim blnNewPLURAL As Boolean = False

            Try
                '1. 연속검사인경우 바코드번호 발생 안하고 복수구분 증가
                If aoCurRowData.SEQTYN = "1" And (asSeqtYn = aoCurRowData.SEQTYN Or asSeqtYn = "") Then blnNewPLURAL = True

                '2. 새로운 검체번호 발생시 혈액은행계의 검사에서 ABO가 있는경우 ( 검체가 틀려도 복수구분 설정 ) 
                '   이전의 ABO검사와 검체번호 같고 복수 구분 변경
                If aoCurRowData.BCCLSCD.Substring(0, 1) = "B" And _
                   abNewBCNO = True And aoCurRowData.DBLTSEQ = "1" Then blnNewPLURAL = True

                fnPluralBCNO_Judge = blnNewPLURAL

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 새로운 바코드번호 생성
        Private Sub sbNewBCNO(ByRef BCNO_info As clsBCNO_Info, ByVal asDate As String, ByVal asGBN As String)
            Dim sFn As String = "Private Sub fnNewBCNO(ByRef BCNO_info As clsBCNO_Info, ByVal asDate As String, ByVal asGBN As String)"
            Dim aoBCNO As New clsBCNO_Info

            Try
                With aoBCNO
                    .YYYYMMDD = asDate
                    .BCCLSCD = asGBN
                    .SPCSEQNO = (New Collfn).GetBCNO(.YYYYMMDD, .BCCLSCD)
                End With

                BCNO_info = aoBCNO
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Public Function fnGet_NewBcNo(ByVal r_stu As STU_COLLWEB) As String
            Dim sFn As String = "Public Function fnGet_NewBcNo(Object) As String"

            Dim sSql As String = "pro_ack_exe_seqno_bc"

            Dim dbCmd As OracleCommand
            Dim dbParam As New OracleParameter  'New DBORA.DbParrameter

            Try
                '> 연속검사 샘플 판별
                If r_stu.SERIES Then
                    miPlural += 1
                Else
                    miPlural = 0
                End If

                If miPlural > 0 Then
                    If msBcNoBuf.Length = PRG_CONST.Len_BcNo Then
                        If miPlural > 9 Then
                            miPlural = 0
                        Else
                            Return msBcNoBuf.Substring(0, PRG_CONST.Len_BcNo - 1) + miPlural.ToString
                        End If
                    Else
                        miPlural = 0
                    End If
                End If

                Dim iSeqNo As Integer = 0

                dbCmd = New OracleCommand

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()

                    '<
                    dbParam = New OracleParameter()

                    With dbParam
                        .ParameterName = "rs_seqymd" : .DbType = DbType.String : .Direction = ParameterDirection.Input : .Value = r_stu.COLLDT.Substring(0, 10).Replace("-", "")
                    End With

                    .Parameters.Add(dbParam)

                    dbParam = Nothing
                    '>

                    '<
                    dbParam = New OracleParameter()

                    With dbParam
                        .ParameterName = "rs_seqgbn" : .DbType = DbType.String : .Direction = ParameterDirection.Input : .Value = r_stu.BCCLSCD
                    End With

                    .Parameters.Add(dbParam)

                    dbParam = Nothing
                    '>

                    '<
                    dbParam = New OracleParameter()

                    With dbParam
                        .ParameterName = "rn_seqno" : .DbType = DbType.Int32 : .Direction = ParameterDirection.InputOutput : .Value = iSeqNo
                    End With

                    .Parameters.Add(dbParam)

                    dbParam = Nothing
                    '>

                    .ExecuteNonQuery()
                End With

                Dim sBcNo As String = ""

                iSeqNo = CInt(dbCmd.Parameters("rn_seqno").Value)

                If iSeqNo < 1 Or iSeqNo > PRG_CONST.Max_BcNoSeq Then
                    sBcNo = ""
                Else
                    sBcNo = r_stu.COLLDT.Substring(0, 10).Replace("-", "") + r_stu.BCCLSCD + iSeqNo.ToString("D4") + miPlural.ToString("D1")
                End If

                msBcNoBuf = sBcNo

                Return sBcNo

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose()
                    dbCmd = Nothing
                End If

            End Try
        End Function

#Region " clsBCNO_Info "
        Private Class clsBCNO_Info
            Public YYYYMMDD As String = ""      ' 년월일(yyyymmdd)
            Private msBCCLSGBN As String = ""   ' 검사계구분
            Public BCCLSCD As String            ' 검체분류코드
            Public SPCSEQNO As String = ""      ' 검체순번
            Public PLURAL As String = "0"       ' 복수구분

            Public PRTADDYN As String = ""      '-- 추가바코드 여부("Y/'')
            Public STATGBN As Boolean = False   ' 현재검체번호의 응급여부


            Public Property BCNO() As String
                Get
                    Dim strBCNO As String

                    strBCNO = YYYYMMDD & BCCLSCD & SPCSEQNO & PLURAL
                    If strBCNO = "0" Then
                        BCNO = ""
                    Else
                        BCNO = strBCNO
                    End If

                End Get

                Set(ByVal Value As String)
                    If Value.Length = 15 Then
                        YYYYMMDD = Value.Substring(0, 8)
                        BCCLSCD = Value.Substring(8, 2)
                        SPCSEQNO = Value.Substring(10, 4)
                        PLURAL = Value.Substring(14, 1)
                    Else
                        YYYYMMDD = ""
                        BCCLSCD = ""
                        SPCSEQNO = ""
                        PLURAL = "0"
                    End If
                End Set
            End Property

            ' 복수구분 1증가 
            Public Sub PluralAdd()
                PLURAL = (CInt(PLURAL) + 1).ToString
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

        End Class
#End Region

    End Class


    Public Class CollReg

        Private Const msFile As String = "File : CGDA_C.vb, Class : LISAPP.APP_C.DB_Collect" & vbTab

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Private malBCNO As New ArrayList                ' 검체번호 리스트
        Private malCollectData As New ArrayList         ' 채혈내역  리스트
        Private malDiagData As New ArrayList            ' 상병내역 리스트
        Private malDrugData As New ArrayList            ' 투여약물 리스트
        Private malEntData As New ArrayList             ' 입원정보 리스트

        Private m_al_DiagData As New ArrayList            ' 상병내역 리스트

        Private miCollectItemCnt As Integer             ' 채혈된 검사항목 수
        Private msBCPrtMsg As String = ""               ' 출력 바코드 메세지
        Private mblnBCNO_ORDDT_GBN As Boolean = True    ' 바코드생성 규칙 true : 처방일시(default), false : 처방일
        Private mblnOrderGbn As Boolean = True          ' 오더유무

        '> 연속검사 샘플용 구분자
        Private miPlural As Integer = 0
        Private msBcNoBuf As String = ""

        Private msSTAT_ORDER As String = ""

        Public Sub New()
            MyBase.New()
        End Sub

        Public WriteOnly Property BCNO_ORDDT_GBN() As Boolean
            Set(ByVal Value As Boolean)
                mblnBCNO_ORDDT_GBN = Value
            End Set
        End Property

        Public WriteOnly Property OrderGbn() As Boolean
            Set(ByVal Value As Boolean)
                mblnOrderGbn = Value
            End Set
        End Property

        Public ReadOnly Property BCNO() As ArrayList
            Get
                BCNO = malBCNO
            End Get
        End Property

        Public ReadOnly Property CollectItemCnt() As Integer
            Get
                CollectItemCnt = miCollectItemCnt
            End Get
        End Property

        Public ReadOnly Property BCPrtMsg() As String
            Get
                BCPrtMsg = msBCPrtMsg
            End Get
        End Property

        Public WriteOnly Property CollectData() As ArrayList
            Set(ByVal Value As ArrayList)
                malCollectData = Value
            End Set
        End Property

        Public WriteOnly Property DiagData() As ArrayList
            Set(ByVal Value As ArrayList)
                malDiagData = Value
            End Set
        End Property

        Public WriteOnly Property DrugData() As ArrayList
            Set(ByVal Value As ArrayList)
                malDrugData = Value
            End Set
        End Property

        Public WriteOnly Property EntData() As ArrayList
            Set(ByVal Value As ArrayList)
                malEntData = Value
            End Set
        End Property

        ' 채혈할 검사항목 가져오기
        Private Function fnGetTestList(ByVal rsDate As String, ByVal raSpcDataList As ArrayList) As DataTable
            Dim sFn As String = "Public Function GetTestList(ByVal adtDate As Date) As DataTable"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim sRegNo As String = ""
                Dim sOrdList As String = ""

                Dim sTmpTime As String = ""
                Dim sOrdDt As String = ""
                Dim sOrdTm As String = ""
                Dim sFkOcsList As String = ""

                Dim sDeptCd As String = ""
                Dim sWard As String = ""

                Dim sWhere As String = ""

                ' 처방항목 조합
                If raSpcDataList.Count > 0 Then
                    For iCnt As Integer = 0 To raSpcDataList.Count - 1
                        With CType(raSpcDataList.Item(iCnt), STU_CollectInfo)
                            If iCnt > 0 Then
                                sOrdList += ","
                                sFkOcsList += ","
                            End If

                            sRegNo = .REGNO ' 조회용 등록번호
                            sOrdDt = .ORDDT.Substring(1, 8)

                            sOrdList += .TCLSCD + .SPCCD
                            sFkOcsList += .FKOCS

                            sTmpTime = .ORDDT.Substring(9, 4)

                            If (sOrdTm + ",").IndexOf(sTmpTime + ",") < 0 Then sOrdTm += IIf(sOrdTm = "", "", ",").ToString + sTmpTime
                            If (sDeptCd + ",").IndexOf(.DEPTCD + ",") < 0 Then sDeptCd += IIf(sDeptCd = "", "", ",").ToString + .DEPTCD

                            ' 응급 검사 여부
                            If .STATGBN = "1" Then msSTAT_ORDER += IIf(msSTAT_ORDER = "", "", ",").ToString + .TCLSCD + .SPCCD

                            ' 입원/외래 구분
                            sWhere = ""
                            If iCnt = 0 Then
                                '외래: 수납여부 체크, 응급인경우는 수납여부 미체크
                                '-- 원자력에서는 응급실인 경우도 수납해야 함.
                                'If .IOGBN = "O" And .DEPTCD <> "EM" Then strWhere = " and SUNAB_DATE is not null "
                                If .IOGBN = "O" Then sWhere = "           AND NVL(sunab_date, '') <> ''"

                                '입원: 해당병동 내역만 채혈
                                If .IOGBN = "I" Then sWhere &= "           AND NVL(nrs_time, '') <> '' AND ho_dong = '" & .WARDNO & "' "
                            End If
                        End With
                    Next
                End If

                If mblnOrderGbn Then
                    sSql += "SELECT F6.bcclscd, f6.spccd, f3.spcnmbp,"
                    sSql += "       o.orddt, f6.tubecd, f4.tubenmbp + ' ' + f6.minspcvol tubenmbp, f6.seqtmi, f6.testcd,"
                    sSql += "       f6.dbltseq, f6.sugacd, f6.insugbn, f6.tcdgbn, f6.partcd, o.owngbn,"
                    sSql += "       CASE WHEN fg.dbltseq = '1' THEN '0' ELSE fg.dbltseq) dbltseq_sort,"
                    sSql += "       NVL(f6.seqtyn, '0') seqtyn,"
                    sSql += "       NVL(f6.exlabyn, '0') exlabyn, f6.tnmbp, o.deptcd, o.doctorcd, f6.bccnt,"
                    sSql += "       CASE WHEN NVL(o.nrs_time, '') = '' THEN 'N' ELSE 'Y' END NRS_CFM_YN, f6.tnmd, o.fkocs,"
                    sSql += "      '2' sort_key1, NVL(f6.dispseql, 999) sort_key2"
                    sSql += "  FROM lf060m f6,"
                    sSql += "       (SELECT hangmog_code, specimen_code, gwa deptcd, doctor doctorcd,"
                    sSql += "               order_date + order_time orddt, owngbn, in_out_gubun, fkocs"
                    sSql += "          FROM mtsS0001_lis"
                    sSql += "         WHERE bunho      = '" + "" + "'"
                    sSql += "           AND order_date = :orddt"
                    sSql += "           AND order_time IN ('" + sOrdTm.Replace(",", "','") + "')"
                    sSql += "           AND gwa IN  ('" + sDeptCd.Replace(",", "','") + "')"
                    sSql += "           AND fkocs IN ('" + sFkOcsList.Replace(",", "','") + "')"
                    sSql += "           AND dc_yn = 'N'"
                    sSql += "           AND NVL(spcflg, '') = ''"
                    sSql += sWhere
                    sSql += "       ) o,"
                    sSql += "       lf040m f4, lf030m f3"
                    sSql += " WHERE f6.tcdgbn IN ( 'P', 'B', 'S' ) "
                    sSql += "   AND f6.usdt <= '" + rsDate + "'"
                    sSql += "   AND f6.uedt >  '" + rsDate + "'"
                    sSql += "   AND f6.testcd + f6.spccd IN ('" + sOrdList.Replace(",", "','") + "')"
                    sSql += "   AND f6.tordcd = o.hangmog_code"
                    sSql += "   AND f6.spccd = o.specimen_code"
                    sSql += "   AND f6.tubcd = f4.tubecd"
                    sSql += "   AND f4.usdt <= '" + rsDate + "'"
                    sSql += "   AND f4.uedt >  '" + rsDate + "'"
                    sSql += "   AND f6.tubecd > '00'"
                    sSql += "   AND f6.spccd = f3.spccd"
                    sSql += "   AND f3.usdt <= '" + rsDate + "'"
                    sSql += "   AND f3.uedt >  '" + rsDate + "'"
                    sSql += " UNION ALLl "
                    sSql += "SELECT fg.bcclscd, fg.spccd,  f3.spcnmbp,"
                    sSql += "       o.orddt, fg.tubecd, f4.tubenmbp + ' ' + fg.minspcvol tubenmbp, fg.seqtmi, fg.testcd,"
                    sSql += "       fg.dbltseq, fg.sugacd, fg.insugbn, fg.tcdgbn, fg.partcd, o.owngbn,"
                    sSql += "       CASE WHEN fg.dbltseq = '1' THEN '0' ELSE fg.dbltseq) dbltseq_sort,"
                    sSql += "       NVL(fg.seqtyn, '0') seqtyn,"
                    sSql += "       NVL(fg.exlabyn, '0') exlabyn, fg.tnmbp, o.deptcd, o.doctorcd, fg.bccnt,"
                    sSql += "       CASE WHEN NVL(o.nrs_time, '') = '' THEN 'N' ELSE 'Y' END nrs_cfm_yn, fg.tnmd, o.fkocs,"
                    sSql += "      '1' sort_key1, NVL(fg.dispseql, 999) sort_key2"
                    sSql += "  FROM lf060m f6,"
                    sSql += "       (SELECT hangmog_code, specimen_code, gwa deptcd, doctor doctorcd,"
                    sSql += "               order_date + order_time orddt, owngbn, in_out_gubun, fkocs"
                    sSql += "          FROM mtsS0001_lis"
                    sSql += "         WHERE bunho = :regno"
                    sSql += "           AND order_date = :orddt"
                    sSql += "           AND order_time IN ('" + sOrdTm.Replace(",", "','") + "')"
                    sSql += "           AND gwa IN  ('" + sDeptCd.Replace(",", "','") + "')"
                    sSql += "           AND fkocs IN ('" + sFkOcsList.Replace(",", "','") + "')"
                    sSql += "           AND dc_yn = 'N'"
                    sSql += "           AND NVL(spcflg, '') = ''"
                    sSql += sWhere
                    sSql += "       ) o,"
                    sSql += "       (SELECT DISTINCT"
                    sSql += "               f62.tclscd, f62.tspccd, f62.testcd, f62.spccd, f.tnmd, f.tnmbp, f.bcclscd, f.minspcvol, f.dispseql, f.exlabyn,"
                    sSql += "               f.sugacd, f.insugbn, f.tcdgbn, f.partcd, f.slipcd, f.seqtyn, f.seqtmi, f.cwarning, f.tubecd, f.dbltseq, f.tordcd,"
                    sSql += "               f.tubecd"
                    sSql += "          FROM lf062m f62, lf060m f"
                    sSql += "         WHERE f.testcd = f62.testcd"
                    sSql += "           AND f.spccd = f62.spccd"
                    sSql += "           AND f.usdt <= '" + rsDate + "'"
                    sSql += "           AND f.uedt >  '" + rsDate + "'"
                    sSql += "       ) fg,"
                    sSql += "       lf040m f4, lf030m f3"
                    sSql += " WHERE f6.tcdgbn IN ( 'P', 'B', 'S' ) "
                    sSql += "   AND f6.usdt <= '" + rsDate + "'"
                    sSql += "   AND f6.uedt >  '" + rsDate + "'"
                    sSql += "   AND f6.tordcd = o.hangmog_code"
                    sSql += "   AND f6.spccd = o.specimen_code"
                    sSql += "   AND f6.tubecd > '00'"
                    sSql += "   AND f6.testcd = fg.tclscd"
                    sSql += "   AND f6.spccd = f.tspccd"
                    sSql += "   and fg.testcd + fg.spccd IN ('" + sOrdList.Replace(",", "','").ToString + "')"
                    sSql += "   and fg.tubecd = f4.tubecd"
                    sSql += "   AND f4.usdt <= '" + rsDate + "'"
                    sSql += "   AND f4.uedt >  '" + rsDate + "'"
                    sSql += "   AND fg.tubecd > '00'"
                    sSql += "   and fg.spccd = f3.spccd"
                    sSql += "   and D.USDT     <= '" + rsDate + "'"
                    sSql += "   AND f3.usdt <= '" + rsDate + "'"
                    sSql += "   AND f3.uedt >  '" + rsDate + "'"
                Else
                    sSql += "SELECT DISTINCT"
                    sSql += "       f6.bcclscd, f6.spccd, f3.spcnmbp,"
                    sSql += "       '" + sOrdDt + sTmpTime + "' orddt, f6.tubecd, f4.tubenmbp + ' ' + f6.minspcvol tubenmbp, f6.spqtmi, f6.testcd,"
                    sSql += "       f6.dbltseq, f6.sugacd, f6.insugbn, f6.tcdgbn, f6.partcd, '' owngbn,"
                    sSql += "       CASE WHEN fg.dbltseq = '1' THEN '0' ELSE fg.dbltseq) dbltseq_sort,"
                    sSql += "       NVL(f6.seqtyn, '0') seqtyn,"
                    sSql += "       NVL(f6.exlabyn, '0') exlabyn, f6.tnmbp, '' deptcd, '' doctorcd, f6.bccnt,"
                    sSql += "       'Y' nrs_cfm_yn, f6.tnmd, '' fkocs"
                    sSql += "  FROM lf060m f6, lf040m f4, lf030m f3"
                    sSql += " WHERE f6.tcdgbn IN ( 'P', 'B', 'S' ) "
                    sSql += "   AND f6.usdt <= '" + rsDate + "'"
                    sSql += "   AND f6.uedt >  '" + rsDate + "'"
                    sSql += "   AND f6.testcd + f6.spccd IN ('" + sOrdList.Replace(",", "','") + "')"
                    sSql += "   AND f6.tubcd = f4.tubecd"
                    sSql += "   AND f4.usdt <= '" + rsDate + "'"
                    sSql += "   AND f4.uedt >  '" + rsDate + "'"
                    sSql += "   AND f6.tubecd > '00'"
                    sSql += "   AND f6.spccd = f3.spccd"
                    sSql += "   AND f3.usdt <= '" + rsDate + "'"
                    sSql += "   AND f3.uedt >  '" + rsDate + "'"
                End If

                If mblnBCNO_ORDDT_GBN Then
                    sSql += " ORDER BY orddt, bcclscd, spccd, tubecd, seqtyn, seqtmi, tcdgbn"
                Else
                    sSql += " ORDER BY bcclscd, spccd, exlabyn, tubecd, seqtyn, seqtmi, testcd, dbltseq_sort"
                End If

                DbCommand()
                Return DbExecuteQuery(sSql)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '> 개별 채혈
        Public Function ExecuteDo(ByVal r_al_bcinfo As ArrayList, ByVal r_al_diag As ArrayList, _
                                  ByVal rsForm As String, _
                                  ByVal rsPrinterName As String, _
                                  ByVal rbToColl As Boolean, _
                                  ByVal rbAutoTkMode As Boolean, _
                                  ByVal rbBcPrt As Boolean) As ArrayList
            Dim sFn As String = "Public Function ExecuteDo(ArrayList, ArrayList, String, Boolean, Boolean) As ArrayList"

            Dim al_return As New ArrayList

            Try
                m_al_DiagData = r_al_diag

                For i As Integer = 1 To r_al_bcinfo.Count
                    Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_bcinfo(i - 1), List(Of STU_CollectInfo))

                    Dim sReturn As String = ExecuteDo_One(listcollData, r_al_diag, rbToColl, rbAutoTkMode)

                    If sReturn <> "" Then
                        For Each collData As STU_CollectInfo In listcollData
                            collData.BCNO = sReturn
                        Next

                        If rbBcPrt Then
                            Dim arr As New ArrayList
                            arr.Add(listcollData)
                            Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(rsForm)
                            objBCPrt.PrintDoBarcode(arr, 1, rsForm, True, rsPrinterName)
                        End If

                        al_return.Add(listcollData)
                    End If
                Next

                Return al_return

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '> 개별 채혈 NEW 
        Public Function ExecuteDo_Coll(ByVal r_al_bcinfo As ArrayList, ByVal r_al_diag As ArrayList, _
                                          ByVal rbRegCollDt As Boolean, _
                                            Optional ByVal rsForm As String = "", _
                                             Optional ByVal rbFirst As Boolean = False, _
                                                 Optional ByVal rsPrinterName As String = "", _
                                                    Optional ByVal rbAutoTkMode As Boolean = False) As ArrayList
            Dim sFn As String = "Public Function ExecuteDo_Coll(ArrayList, ArrayList, String, Boolean) As ArrayList"

            Dim al_return As New ArrayList

            Try
                m_al_DiagData = r_al_diag

                For i As Integer = 1 To r_al_bcinfo.Count
                    Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_bcinfo(i - 1), List(Of STU_CollectInfo))

                    Dim sReturn As String = ExecuteDo_One_Coll(listcollData, r_al_diag, rbAutoTkMode, rbRegCollDt)

                    If sReturn <> "" Then
                        For Each collData As STU_CollectInfo In listcollData
                            collData.BCNO = sReturn
                        Next

                        Dim arr As New ArrayList
                        arr.Add(listcollData)
                        Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(rsForm)
                        objBCPrt.PrintDoBarcode(arr, 1, rsForm, rbFirst, rsPrinterName)
                        al_return.Add(listcollData)
                    End If
                Next

                Return al_return

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


        '> 개별 채혈 + 접수
        Public Function ExecuteDo(ByVal r_al_bcinfo As ArrayList, ByVal r_al_diag As ArrayList, _
                                  ByVal rbTk As Boolean) As ArrayList
            Dim sFn As String = "Public Function ExecuteDo(String, ArrayList, ArrayList, ArrayList, ArrayList, Boolean) As ArrayList"

            Dim al_return As New ArrayList

            Try
                m_al_DiagData = r_al_diag

                For i As Integer = 1 To r_al_bcinfo.Count
                    Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_bcinfo(i - 1), List(Of STU_CollectInfo))

                    Dim sReturn As String = ExecuteDo_One(listcollData, r_al_diag, rbTk)

                    If sReturn <> "" Then
                        For Each collData As STU_CollectInfo In listcollData
                            collData.BCNO = sReturn
                        Next

                        al_return.Add(listcollData)
                    End If
                Next

                Return al_return

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '> 개별 채혈 + 혈액은행 접수
        Public Function ExecuteDo(ByVal r_al_bcinfo As ArrayList, ByVal r_al_diag As ArrayList, _
                                       ByVal rbToColl As Boolean, ByVal rbTk As Boolean, ByVal rbBnk As Boolean) As ArrayList
            Dim sFn As String = "Public Function ExecuteDo(String, ArrayList, ArrayList, Boolean) As ArrayList"

            Dim al_return As New ArrayList

            Try
                m_al_DiagData = r_al_diag

                For i As Integer = 1 To r_al_bcinfo.Count
                    Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_bcinfo(i - 1), List(Of STU_CollectInfo))
                    Dim sReturn As String = ExecuteDo_One(listcollData, r_al_diag, rbToColl, rbTk, rbBnk)

                    If sReturn <> "" Then
                        For Each collData As STU_CollectInfo In listcollData
                            collData.BCNO = sReturn
                        Next

                        al_return.Add(listcollData)
                    End If
                Next

                Return al_return

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function ExecuteDo_One(ByVal r_listcollData As List(Of STU_CollectInfo), _
                                      ByVal r_al_diag As ArrayList, ByVal rbToColl As Boolean) As String
            Dim sFn As String = "Public Function ExecuteDo_One(List(Of STU_CollectInfo), ArrayList, Boolean) As String"

            Try
                m_al_DiagData = r_al_diag

                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                '> get bcno
                Dim sBcNo As String = GetNewBcNo(r_listcollData)

                If sBcNo = "" Then
                    m_dbTran.Rollback()
                    Return ""
                End If

                Dim sRegNo As String = ""
                Dim sIOGBN As String = ""
                Dim sFkOcsO As String = "" : Dim sFKOcsTOrdCdO As String = ""
                Dim sFkOcsL As String = "" : Dim sFKOcsTOrdCdL As String = ""
                Dim iRows As Integer = 0
                Dim iRowsO As Integer = 0
                Dim iRowsL As Integer = 0
                Dim sTestCds As String = ""

                For i As Integer = 1 To r_listcollData.Count
                    Dim collData As STU_CollectInfo = r_listcollData.Item(i - 1)

                    If i > 1 Then sTestCds += ","
                    sTestCds += "'" + collData.TCLSCD + "'"

                    If sRegNo.Length = 0 Then sRegNo = collData.REGNO
                    sIOGBN = collData.IOGBN

                    If collData.OWNGBN = "O" Then
                        If sFkOcsO.Length > 0 Then sFkOcsO += "," : sFKOcsTOrdCdO += ","
                        sFkOcsO += collData.FKOCS
                        '< yjlee 
                        sFKOcsTOrdCdO += collData.FKOCS + collData.TORDCD
                        '>
                    Else
                        If sFkOcsL.Length > 0 Then sFkOcsL += "," : sFKOcsTOrdCdL += ","
                        sFkOcsL += collData.FKOCS
                        '< yjlee 
                        sFKOcsTOrdCdL += collData.FKOCS + collData.TORDCD
                        '> 
                    End If
                Next

                '> get colldt, prtbcno
                Dim dt As New DataTable

                dt = ExecuteDo_One_GetCollDtPrtBcNo(sBcNo)

                For Each collData As STU_CollectInfo In r_listcollData
                    collData.COLLDT = CDate(dt.Rows(0).Item("sysdt")).ToString("yyyy-MM-dd HH:mm:ss")
                    collData.PRTBCNO = dt.Rows(0).Item("prtbcno").ToString
                Next

                '> set laborder
                Dim css As New OcsLink.ChgOcsState

                If sFkOcsO.Length > 0 Then
                    With css
                        .BcNo = sBcNo
                        .CollDt = r_listcollData.Item(0).COLLDT
                        .OwnGbn = "O"
                        .RegNo = sRegNo
                        .TotFkOcs = sFkOcsO
                        .IOGBN = sIOGBN
                        '< yjlee 
                        .FKOCSTORDCD = sFKOcsTOrdCdO
                        '> 
                    End With

                    iRowsO = SetOrderChgCollState(css, rbToColl, m_dbCn, m_dbTran)
                End If

                If sFkOcsL.Length > 0 Then
                    With css
                        .BcNo = sBcNo
                        .CollDt = r_listcollData.Item(0).COLLDT
                        .OwnGbn = "L"
                        .RegNo = sRegNo
                        .TotFkOcs = sFkOcsL
                        .IOGBN = sIOGBN
                        '< yjlee 
                        .FKOCSTORDCD = sFKOcsTOrdCdL
                        '>
                    End With

                    iRowsL = SetOrderChgCollState(css, rbToColl, m_dbCn, m_dbTran)
                End If

                If iRowsO + iRowsL < 1 Then 'If iRowsO + iRowsL <> r_listcollData.Count Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj011m
                iRows = ExecuteDo_One_AddColl(sBcNo, r_listcollData, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj010m
                iRows = ExecuteDo_One_AddSpc(sBcNo, r_listcollData, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj030m
                ExecuteDo_One_AddHeight(sBcNo, r_listcollData)

                '> add diag info -> lj040m
                ExecuteDo_One_AddDiag(sBcNo, r_listcollData(0).PARTGBN, r_al_diag)

                iRows = ExecuteDo_One_Exists(sBcNo, sTestCds)
                If iRows = 0 Then
                    m_dbTran.Rollback()
                    Return ""
                End If

                ''-- 헌혈 마스터 
                'iRows = ExecuteDo_One_Add_Doner(sBcNo, r_listcollData)
                'If iRows = 0 Then
                '    m_LisDbTran.Rollback()
                '    Return ""
                'End If

                m_dbTran.Commit()

                Return sBcNo

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Public Function ExecuteDo_One(ByVal r_listcollData As List(Of STU_CollectInfo), ByVal r_al_diag As ArrayList, ByVal rbToColl As Boolean, ByVal rbTk As Boolean) As String
            Dim sFn As String = "Public Function ExecuteDo_One(List(Of STU_CollectInfo), ArrayList,  Boolean, Boolean) As String"

            Try
                m_al_DiagData = r_al_diag

                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                '> get bcno
                Dim sBcNo As String = GetNewBcNo(r_listcollData)

                If sBcNo = "" Then
                    m_dbTran.Rollback()
                    Return ""
                End If

                Dim sRegNo As String = ""
                Dim sIOGBN As String = ""
                Dim sTCdGbn As String = ""
                Dim sFkOcsO As String = "" : Dim sFKOcsTOrdCdO As String = ""
                Dim sFkOcsL As String = "" : Dim sFKOcsTOrdCdL As String = ""
                Dim iRows As Integer = 0
                Dim iRowsO As Integer = 0
                Dim iRowsL As Integer = 0
                Dim sTestCds As String = ""

                For i As Integer = 1 To r_listcollData.Count
                    Dim collData As STU_CollectInfo = r_listcollData.Item(i - 1)

                    If i > 1 Then sTestCds += ","
                    sTestCds += "'" + collData.TCLSCD + "'"

                    If sRegNo.Length = 0 Then sRegNo = collData.REGNO
                    sIOGBN = collData.IOGBN
                    sTCdGbn = collData.TCDGBN

                    If collData.OWNGBN = "O" Then
                        If sFkOcsO.Length > 0 Then sFkOcsO += "," : sFKOcsTOrdCdO += ","
                        sFkOcsO += collData.FKOCS
                        sFKOcsTOrdCdO += collData.FKOCS + collData.TORDCD
                    Else
                        If sFkOcsL.Length > 0 Then sFkOcsL += "," : sFKOcsTOrdCdL += ","
                        sFkOcsL += collData.FKOCS
                        sFKOcsTOrdCdL += collData.FKOCS + collData.TORDCD
                    End If
                Next

                '> get colldt, prtbcno
                Dim dt As New DataTable

                dt = ExecuteDo_One_GetCollDtPrtBcNo(sBcNo)

                For Each collData As STU_CollectInfo In r_listcollData
                    collData.COLLDT = dt.Rows(0).Item("sysdt").ToString
                    collData.PRTBCNO = dt.Rows(0).Item("prtbcno").ToString
                Next

                '> set laborder
                Dim css As New OcsLink.ChgOcsState

                If sFkOcsO.Length > 0 Then
                    With css
                        .BcNo = sBcNo
                        .CollDt = r_listcollData.Item(0).COLLDT
                        .OwnGbn = "O"
                        .RegNo = sRegNo
                        .TotFkOcs = sFkOcsO
                        .BcNo = sBcNo
                        .IOGBN = sIOGBN
                        .FKOCSTORDCD = sFKOcsTOrdCdO
                    End With

                    iRowsO = SetOrderChgCollState(css, rbToColl, m_dbCn, m_dbTran)
                End If

                If sFkOcsL.Length > 0 Then
                    With css
                        .BcNo = sBcNo
                        .CollDt = r_listcollData.Item(0).COLLDT
                        .OwnGbn = "L"
                        .RegNo = sRegNo
                        .TotFkOcs = sFkOcsL
                        .BcNo = sBcNo
                        .IOGBN = sIOGBN
                        .FKOCSTORDCD = sFKOcsTOrdCdL
                    End With

                    iRowsL = SetOrderChgCollState(css, rbToColl, m_dbCn, m_dbTran)
                End If

                If iRowsO + iRowsL < r_listcollData.Count And sTCdGbn <> "G" Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj011m
                iRows = ExecuteDo_One_AddColl(sBcNo, r_listcollData, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj010m
                iRows = ExecuteDo_One_AddSpc(sBcNo, r_listcollData, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj012m(키,몸무게)
                ExecuteDo_One_AddHeight(sBcNo, r_listcollData)

                '> add diag info -> lj013m(진단명
                ExecuteDo_One_AddDiag(sBcNo, r_listcollData(0).PARTGBN, r_al_diag)

                iRows = ExecuteDo_One_Exists(sBcNo, sTestCds)
                If iRows = 0 Then
                    m_dbTran.Rollback()
                    Return ""
                End If

                If rbTk Or (r_listcollData.Item(0).POCTYN = "1" And r_listcollData.Item(0).IOGBN = "O") Then
                    '> 접수작업까지 처리
                    iRows = ExecuteDo_One_AddTake(sBcNo)

                    If iRows = 0 Then
                        m_dbTran.Rollback()

                        Return ""
                    End If
                End If

                ''-- 헌혈 마스터 
                'iRows = ExecuteDo_One_Add_Doner(sBcNo, r_listcollData)
                'If iRows = 0 Then
                '    m_LisDbTran.Rollback()
                '    Return ""
                'End If

                m_dbTran.Commit()

                Return sBcNo

            Catch ex As Exception
                If m_dbTran IsNot Nothing Then
                    If m_dbTran.Connection IsNot Nothing Then
                        m_dbTran.Rollback()
                    End If
                End If

                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function


        Public Function ExecuteDo_One_Coll(ByVal r_listcollData As List(Of STU_CollectInfo), ByVal r_al_diag As ArrayList, _
                                           ByVal rbToColl As Boolean, ByVal rbTk As Boolean) As String
            Dim sFn As String = "Public Function ExecuteDo_One_Coll(List(Of STU_CollectInfo), ArrayList, Boolean) As String"

            Try
                m_al_DiagData = r_al_diag

                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                '> get bcno
                Dim sBcNo As String = GetNewBcNo(r_listcollData)

                If sBcNo = "" Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                Dim sRegNo As String = ""
                Dim sIOGBN As String = ""
                Dim sFkOcsO As String = "" : Dim sFKOcsTOrdCdO As String = ""
                Dim sFkOcsL As String = "" : Dim sFKOcsTOrdCdL As String = ""
                Dim iRows As Integer = 0
                Dim iRowsO As Integer = 0
                Dim iRowsL As Integer = 0

                For i As Integer = 1 To r_listcollData.Count
                    Dim collData As STU_CollectInfo = r_listcollData.Item(i - 1)

                    If sRegNo.Length = 0 Then sRegNo = collData.REGNO
                    sIOGBN = collData.IOGBN

                    If collData.OWNGBN = "O" Then
                        If sFkOcsO.Length > 0 Then sFkOcsO += "," : sFKOcsTOrdCdO += ","
                        sFkOcsO += collData.FKOCS
                        sFKOcsTOrdCdO += collData.FKOCS + collData.TORDCD
                    Else
                        If sFkOcsL.Length > 0 Then sFkOcsL += ","
                        sFkOcsL += collData.FKOCS
                        sFKOcsTOrdCdL += collData.FKOCS + collData.TORDCD
                    End If
                Next

                '> get colldt, prtbcno
                Dim dt As New DataTable

                dt = ExecuteDo_One_GetCollDtPrtBcNo(sBcNo)

                For Each collData As STU_CollectInfo In r_listcollData
                    collData.COLLDT = CDate(dt.Rows(0).Item("sysdt")).ToString("yyyy-MM-dd HH:mm:ss")
                    collData.PRTBCNO = dt.Rows(0).Item("prtbcno").ToString
                Next

                '> set laborder
                Dim css As New OcsLink.ChgOcsState

                If sFkOcsO.Length > 0 Then
                    With css
                        .BcNo = sBcNo
                        .CollDt = r_listcollData.Item(0).COLLDT
                        .OwnGbn = "O"
                        .RegNo = sRegNo
                        .TotFkOcs = sFkOcsO
                        .IOGBN = sIOGBN
                        .FKOCSTORDCD = sFKOcsTOrdCdO
                    End With

                    iRowsO = SetOrderChgCollState(css, rbToColl, m_dbCn, m_dbTran)
                End If

                If sFkOcsL.Length > 0 Then
                    With css
                        .BcNo = sBcNo
                        .CollDt = r_listcollData.Item(0).COLLDT
                        .OwnGbn = "L"
                        .RegNo = sRegNo
                        .TotFkOcs = sFkOcsL
                        .IOGBN = sIOGBN
                        .FKOCSTORDCD = sFKOcsTOrdCdL
                    End With

                    iRowsL = SetOrderChgCollState(css, rbToColl, m_dbCn, m_dbTran)
                End If

                If iRowsO + iRowsL <> r_listcollData.Count Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj011m
                iRows = ExecuteDo_One_AddColl(sBcNo, r_listcollData, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj010m
                iRows = ExecuteDo_One_AddSpc(sBcNo, r_listcollData, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj030m
                ExecuteDo_One_AddHeight(sBcNo, r_listcollData)

                '> add diag info -> lj040m
                ExecuteDo_One_AddDiag(sBcNo, r_listcollData(0).PARTGBN, r_al_diag)

                If rbTk Then
                    If sBcNo.Substring(8, 1) = PRG_CONST.BCCLS_BloodBank.Substring(0, 1) Then
                        '> 접수작업까지 처리
                        iRows = ExecuteDo_One_AddTake(sBcNo)

                        If iRows = 0 Then
                            m_dbTran.Rollback()

                            Return ""
                        End If
                    End If
                End If

                ''-- 헌혈 마스터 
                'iRows = ExecuteDo_One_Add_Doner(sBcNo, r_listcollData)
                'If iRows = 0 Then
                '    m_LisDbTran.Rollback()
                '    Return ""
                'End If

                m_dbTran.Commit()

                Return sBcNo

            Catch ex As Exception
                If m_dbTran IsNot Nothing Then
                    If m_dbTran.Connection IsNot Nothing Then
                        m_dbTran.Rollback()
                    End If
                End If

                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Public Function ExecuteDo_One(ByVal r_listcollData As List(Of STU_CollectInfo), ByVal r_al_diag As ArrayList, ByVal rbToColl As Boolean, ByVal rbTk As Boolean, ByVal rbBnk As Boolean) As String
            Dim sFn As String = "Public Function ExecuteDo_One(List(Of STU_CollectInfo), ArrayList, Boolean Boolean) As String"

            Try
                m_al_DiagData = r_al_diag

                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                '> get bcno
                Dim sBcNo As String = GetNewBcNo(r_listcollData)

                If sBcNo = "" Then
                    m_dbTran.Rollback()
                    Return ""
                End If

                Dim sRegNo As String = ""
                Dim sIOGBN As String = ""
                Dim sFkOcsO As String = "" : Dim sFKOcsTOrdCd As String = ""
                Dim sFkOcsL As String = "" : Dim sFKOcsTOrdCdL As String = ""
                Dim iRows As Integer = 0
                Dim iRowsO As Integer = 0
                Dim iRowsL As Integer = 0
                Dim sTestCds As String = ""

                For i As Integer = 1 To r_listcollData.Count
                    Dim collData As STU_CollectInfo = r_listcollData.Item(i - 1)

                    If i > 1 Then sTestCds += ","
                    sTestCds += "'" + collData.TCLSCD + "'"

                    If sRegNo.Length = 0 Then sRegNo = collData.REGNO
                    sIOGBN = collData.IOGBN

                    If collData.OWNGBN = "O" Then
                        If sFkOcsO.Length > 0 Then sFkOcsO += "," : sFKOcsTOrdCd += ","
                        sFkOcsO += collData.FKOCS
                        '< yjlee 
                        sFKOcsTOrdCd += collData.FKOCS + collData.TORDCD
                        '>
                    Else
                        If sFkOcsL.Length > 0 Then sFkOcsL += "," : sFKOcsTOrdCdL += ","
                        sFkOcsL += collData.FKOCS
                        '< yjlee 
                        sFKOcsTOrdCdL += collData.FKOCS + collData.TORDCD
                        '> 
                    End If
                Next

                '> get colldt, prtbcno
                Dim dt As New DataTable

                dt = ExecuteDo_One_GetCollDtPrtBcNo(sBcNo)

                For Each collData As STU_CollectInfo In r_listcollData
                    collData.COLLDT = CDate(dt.Rows(0).Item("sysdt")).ToString("yyyy-MM-dd HH:mm:ss")
                    collData.PRTBCNO = dt.Rows(0).Item("prtbcno").ToString
                Next

                '> set laborder
                Dim css As New OcsLink.ChgOcsState

                If sFkOcsO.Length > 0 Then
                    With css
                        .BcNo = sBcNo
                        .CollDt = r_listcollData.Item(0).COLLDT
                        .OwnGbn = "O"
                        .RegNo = sRegNo
                        .TotFkOcs = sFkOcsO
                        .IOGBN = sIOGBN
                        '< yjlee 
                        .FKOCSTORDCD = sFKOcsTOrdCd
                        '> 
                    End With

                    iRowsO = SetOrderChgCollState(css, rbToColl, m_dbCn, m_dbTran)
                End If

                If sFkOcsL.Length > 0 Then
                    With css
                        .BcNo = sBcNo
                        .CollDt = r_listcollData.Item(0).COLLDT
                        .OwnGbn = "L"
                        .RegNo = sRegNo
                        .TotFkOcs = sFkOcsL
                        .IOGBN = sIOGBN
                        '< yjlee 
                        .FKOCSTORDCD = sFKOcsTOrdCdL
                        '>
                    End With

                    iRowsL = SetOrderChgCollState(css, rbToColl, m_dbCn, m_dbTran)
                End If

                If iRowsO + iRowsL <> r_listcollData.Count Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj011m
                iRows = ExecuteDo_One_AddColl(sBcNo, r_listcollData, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj010m
                iRows = ExecuteDo_One_AddSpc(sBcNo, r_listcollData, rbToColl)

                If iRows = 0 Then
                    m_dbTran.Rollback()

                    Return ""
                End If

                '> add collect info -> lj030m
                ExecuteDo_One_AddHeight(sBcNo, r_listcollData)

                '> add diag info -> lj040m
                ExecuteDo_One_AddDiag(sBcNo, r_listcollData(0).PARTGBN, r_al_diag)

                iRows = ExecuteDo_One_Exists(sBcNo, sTestCds)
                If iRows = 0 Then
                    m_dbTran.Rollback()
                    Return ""
                End If

                If rbTk Then
                    If sBcNo.Substring(8, 1) = PRG_CONST.BCCLS_BloodBank.Substring(0, 1) Then
                        '> 접수작업까지 처리
                        iRows = ExecuteDo_One_AddTake(sBcNo)

                        If iRows = 0 Then
                            m_dbTran.Rollback()

                            Return ""
                        End If
                    End If
                End If

                ''-- 헌혈 마스터 
                'iRows = ExecuteDo_One_Add_Doner(sBcNo, r_listcollData)
                'If iRows = 0 Then
                '    m_LisDbTran.Rollback()
                '    Return ""
                'End If

                m_dbTran.Commit()

                Return sBcNo

            Catch ex As Exception
                If m_dbTran IsNot Nothing Then
                    If m_dbTran.Connection IsNot Nothing Then
                        m_dbTran.Rollback()
                    End If
                End If

                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Protected Function ExecuteDo_One_AddColl(ByVal rsBcNo As String, ByVal r_listcollData As List(Of STU_CollectInfo), ByVal rbToColl As Boolean) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddColl(String, List(Of STU_CollectInfo), Boolean) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0
            Dim iRows As Integer = 0

            Try
                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    For ix As Integer = 1 To r_listcollData.Count

                        sSql = ""

                        sSql += "INSERT INTO " + IIf(r_listcollData.Item(ix - 1).PARTGBN = "R", "rj011m", "lj011m").ToString + "("
                        sSql += "            bcno, tclscd, spccd, regno, "
                        If rbToColl Then
                            sSql += "collid, colldt,"
                        End If
                        sSql += "            owngbn,   iogbn,     fkocs, orddt, doctorrmk, collvol, spcflg,"
                        sSql += "            orgorddt, orgdeptcd, orgdoctorcd, ordslip, ocs_key, sysdt,"
                        sSql += "            editdt,   editid, editip"
                        sSql += "          ) "
                        sSql += "    VALUES( :bcno, :tclscd, :spccd, :regno, "
                        If rbToColl Then
                            sSql += ":collid, :colldt,"
                        End If
                        sSql += "            :owngbn, :iogbn, :fkocs, :orddt, :drrmk, :collvol, :spcflg,"
                        sSql += "            :orgorddt, :orgdeptcd, :orgorddrcd, :ordslip, :ocskey, fn_ack_sysdate,"
                        sSql += "            fn_ack_sysdate, :editid, :editip"
                        sSql += "          )"

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).TCLSCD
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).SPCCD
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).REGNO

                        If rbToColl Then
                            .Parameters.Add("collid", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).COLLID
                            .Parameters.Add("colldt", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).COLLDT
                        End If

                        .Parameters.Add("owngbn", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).OWNGBN
                        .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).IOGBN
                        .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).FKOCS
                        .Parameters.Add("orddt", OracleDbType.Varchar2).Value = r_listcollData.Item(0).ORDDT

                        If r_listcollData.Item(ix - 1).REMARK = "-" Then r_listcollData.Item(ix - 1).REMARK = ""

                        .Parameters.Add("drrmk", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).REMARK

                        .Parameters.Add("collvol", OracleDbType.Varchar2).Value = DBNull.Value

                        If rbToColl Then
                            .Parameters.Add("spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_Coll
                        Else
                            .Parameters.Add("spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_BcPrt
                        End If

                        .Parameters.Add("orgorddt", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).ORDDT
                        .Parameters.Add("orgdeptcd", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).DEPTCD
                        .Parameters.Add("orgorddrcd", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).DOCTORCD
                        .Parameters.Add("ordslip", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).ORDSLIP

                        If r_listcollData.Item(ix - 1).FKOCS.IndexOf("/"c) >= 0 Then
                            Dim sOcsKey() As String = r_listcollData.Item(ix - 1).FKOCS.Split("/"c)

                            .Parameters.Add("ocskey", OracleDbType.Int64).Value = sOcsKey(3)
                        Else
                            .Parameters.Add("ocskey", OracleDbType.Int64).Value = DBNull.Value
                        End If

                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        iRow = .ExecuteNonQuery()
                        iRows += iRow
                    Next
                End With

                Return iRows

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddSpc(ByVal rsBcNo As String, ByVal r_listcollData As List(Of STU_CollectInfo), ByVal rbToColl As Boolean) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddSpc(String, List(Of STU_CollectInfo), Boolean) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0
            Dim sStat As String = ""

            Try
                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO " + IIf(r_listcollData.Item(0).PARTGBN = "R", "rj010m", "lj010m").ToString + "("
                    sSql += "            bcno, spccd, regno, patnm, sex, age, dage, owngbn, iogbn, orddt,"
                    sSql += "            deptcd, doctorcd, wardno, roomno, entdt, statgbn, opdt, resdt, jubsugbn, bcclscd,"
                    sSql += "            spcflg, rstflg, bcprtdt, bcprtid, hregno, editdt, editid, editip"
                    sSql += "          )"
                    sSql += "    VALUES( :bcno,          :spccd,   :regno,         :patnm,   :sex,    :age,     :dage, :owngbn, :iogbn,    :orddt,"
                    sSql += "            :deptcd,        :orddrcd, :wardno,        :roomno,  :entdt,  :statgbn, :opdt, :resdt,  :jubsugbn, :bcclscd,"
                    sSql += "            :spcflg,        :rstflg,  fn_ack_sysdate, :bcprtid, :hregno,"
                    sSql += "            fn_ack_sysdate, :editid,  :editip"
                    sSql += "          )"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_listcollData.Item(0).SPCCD
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .Parameters.Add("patnm", OracleDbType.Varchar2).Value = r_listcollData.Item(0).PATNM
                    .Parameters.Add("sex", OracleDbType.Varchar2).Value = r_listcollData.Item(0).SEX

                    .Parameters.Add("age", OracleDbType.Int32).Value = r_listcollData.Item(0).AGE
                    .Parameters.Add("dage", OracleDbType.Int32).Value = r_listcollData.Item(0).DAGE
                    .Parameters.Add("owngbn", OracleDbType.Varchar2).Value = r_listcollData.Item(0).OWNGBN
                    .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = r_listcollData.Item(0).IOGBN
                    .Parameters.Add("orddt", OracleDbType.Varchar2).Value = r_listcollData.Item(0).ORDDT

                    .Parameters.Add("deptcd", OracleDbType.Varchar2).Value = r_listcollData.Item(0).DEPTCD
                    .Parameters.Add("orddrcd", OracleDbType.Varchar2).Value = r_listcollData.Item(0).DOCTORCD
                    .Parameters.Add("wardno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).WARDNO
                    .Parameters.Add("roomno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).ROOMNO
                    .Parameters.Add("entdt", OracleDbType.Varchar2).Value = r_listcollData.Item(0).ENTDT

                    For i As Integer = 1 To r_listcollData.Count
                        sStat = r_listcollData.Item(i - 1).STATGBN

                        If sStat <> "" Then Exit For
                    Next

                    If sStat.Trim <> "" Then sStat = sStat.Substring(0, 1)

                    .Parameters.Add("statgbn", OracleDbType.Varchar2).Value = sStat
                    .Parameters.Add("opdt", OracleDbType.Varchar2).Value = r_listcollData.Item(0).OPDT
                    .Parameters.Add("resdt", OracleDbType.Varchar2).Value = r_listcollData.Item(0).RESDT
                    .Parameters.Add("jubsugbn", OracleDbType.Varchar2).Value = r_listcollData.Item(0).JUBSUGBN
                    .Parameters.Add("bcclscd", OracleDbType.Varchar2).Value = rsBcNo.Substring(8, 2)

                    If rbToColl Then
                        .Parameters.Add("spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_Coll
                    Else
                        .Parameters.Add("spcflg", OracleDbType.Varchar2).Value = PRG_CONST.Flg_BcPrt
                    End If

                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = DBNull.Value
                    .Parameters.Add("bcprtid", OracleDbType.Varchar2).Value = r_listcollData.Item(0).COLLID

                    If r_listcollData.Item(0).IOGBN = "C" Then
                        .Parameters.Add("hregno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REMARK_NRS
                    Else
                        .Parameters.Add("hregno", OracleDbType.Varchar2).Value = DBNull.Value
                    End If

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    iRow = .ExecuteNonQuery()
                End With

                Return iRow

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddHeight(ByVal rsBcNo As String, ByVal r_listcollData As List(Of STU_CollectInfo)) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddHeight(String, List(Of STU_CollectInfo)) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Dim sStat As String = ""

            If r_listcollData Is Nothing Then Return 0
            If r_listcollData.Count = 0 Then Return 0

            Try

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO " + IIf(r_listcollData.Item(0).PARTGBN = "R", "rj012m", "lj012m").ToString + "( bcno, height, weight )"
                    sSql += "    VALUES( :bcno, :height, :weight )"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("height", OracleDbType.Varchar2).Value = r_listcollData.Item(0).HEIGHT
                    .Parameters.Add("weight", OracleDbType.Varchar2).Value = r_listcollData.Item(0).WEIGHT

                    iRow = .ExecuteNonQuery()
                End With

                Return iRow

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddDiag(ByVal rsBcNo As String, ByVal rsPartGbn As String, ByVal r_al_diag As ArrayList) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddDiag(String, ArrayList) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Dim sDiagNm As String = ""
            Dim sDiagNmE As String = ""

            If r_al_diag Is Nothing Then Return 0
            If r_al_diag.Count = 0 Then Return 0

            For i As Integer = 1 To r_al_diag.Count
                If sDiagNm.Length > 0 Then sDiagNm += ", "
                If sDiagNmE.Length > 0 Then sDiagNmE += ", "

                If Not sDiagNm.Contains(CType(r_al_diag(i - 1), STU_DiagInfo).DIAGNM) Then
                    sDiagNm += CType(r_al_diag(i - 1), STU_DiagInfo).DIAGNM
                End If

                If Not sDiagNmE.Contains(CType(r_al_diag(i - 1), STU_DiagInfo).DIAGNM_ENG) Then
                    sDiagNmE += CType(r_al_diag(i - 1), STU_DiagInfo).DIAGNM_ENG
                End If
            Next

            Try
                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO " + IIf(rsPartGbn = "R", "rj013m", "lj013m").ToString + " ( bcno, diagnm, diagnm_eng )"
                    sSql += "    VALUES( :bcno, :diagnm, :diagnme )"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("diagnm", OracleDbType.Varchar2).Value = sDiagNm
                    .Parameters.Add("diagnme", OracleDbType.Varchar2).Value = sDiagNmE

                    iRow = .ExecuteNonQuery()
                End With

                Return iRow

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_Exists(ByVal rsBcNo As String, ByVal rsTestCds As String) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_Exists(String, ArrayList) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Dim sDiagNm As String = ""
            Dim sDiagNmE As String = ""

            Try
                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "UPDATE lj011m SET spcflg = spcflg"
                    sSql += " WHERE fkocs  IN (SELECT fkocs FROM lj011m WHERE bcno = :bcno)"
                    sSql += "   AND bcno   <> :bcno"
                    sSql += "   AND tclscd IN (" + rsTestCds + ")"
                    sSql += "   AND spcflg NOT IN ('0', 'R')"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRow = .ExecuteNonQuery()
                End With

                If iRow > 0 Then
                    Return 0
                Else
                    Return 1
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_AddTake(ByVal rsBcNo As String) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_AddTake(String) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try
                Dim sErrVal As String = ""

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_take_ocs"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_wknoyn", OracleDbType.Varchar2).Value = "N"
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(4).Value.ToString
                End With

                If IsNumeric(sErrVal.Substring(0, 2)) Then
                    If sErrVal.Substring(0, 2) = "00" Then
                        '정상적으로 접수
                        Return 1
                    Else
                        '이미 접수된 검체번호 or '검사항목 조회 오류
                        Return 0
                    End If
                Else
                    '기타 오류
                    Return 0
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_Add_Doner(ByVal rsBcNo As String, ByVal r_listcollData As List(Of STU_CollectInfo)) As Integer
            Dim sFn As String = "Protected Function ExecuteDo_One_Add_Doner(String, List(Of STU_CollectInfo)) As Integer"


            Try
                Dim sSql As String = ""
                Dim iRow As Integer = 0

                Dim dbCmd As New OracleCommand
                Dim dbDA As OracleDataAdapter
                Dim dt As New DataTable

                Dim sJubSuDt As String = r_listcollData.Item(0).COLLDT

                Dim sDonRegNo As String = ""
                Dim iRet As Integer = 0

                dbCmd.Connection = m_dbCn

                If m_dbTran IsNot Nothing Then
                    If m_dbTran.Connection IsNot Nothing Then
                        dbCmd.Transaction = m_dbTran
                    End If
                End If

                sSql = ""
                sSql += "SELECT o.bunho"
                sSql += "  FROM mts0001_lis o, lf060m f"
                sSql += " WHERE o.bunho = :regno"
                sSql += "   AND o.hangmog_code = f.tordcd"
                sSql += "   AND o.specimen_code = f.spccd"
                sSql += "   AND f.usdt <= fn_ack_sysdate"
                sSql += "   AND f.uedt >  fn_ack_sysdate"
                sSql += "   AND f.bbtype IN ('5', '7', 'A', 'B', 'C', 'D')"
                sSql += "   AND (o.fkocs) IN (SELECT fkocs FROM lj011m WHERE bcno = :bcno)"
                sSql += "   AND o.spcflg = '1'"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDA = New OracleDataAdapter(dbCmd)

                With dbDA
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dt.Reset()
                dbDA.Fill(dt)

                If dt.Rows.Count < 1 Then Return 1

                With dbCmd
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_exe_seqno_donregno"

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = sJubSuDt.Substring(0, 4)

                    .Parameters.Add("retval", OracleDbType.Varchar2, 4000)
                    .Parameters("retval").Direction = ParameterDirection.InputOutput
                    .Parameters("retval").Value = ""

                    .ExecuteNonQuery()

                    sDonRegNo = sJubSuDt.Substring(0, 4) + .Parameters.Item(1).Value.ToString.PadLeft(6, "0"c)
                End With

                sSql = ""
                sSql += "INSERT INTO lb010m("
                sSql += "            donregno, donregdt,  dongbn,   donexpcnt, rctdondt, haddress, hadd_delail, htel,  celphone, age,"
                sSql += "            donseq,   doncnt,    juminno,  patnm,     sex,      donflg,   owngbn,      iogbn, fkocs,    regno,"
                sSql += "            tnsnm,    tnssexage, tnsjumin, orddate,   tordcd,   bcno)"
                sSql += "select :donregno, :donregdt, CASE WHEN f.bbttype IN ('5', '7') THEN '4' ELSE '3' END, 0, :rctdondt, p.address1, p.address2, p.tel1, p.tel2, :age,"
                sSql += "       CASE WHEN f.bbttype = 'A' THEN '1' WHEN f.bbttype = 'B' THEN '2' WHEN f.bbttype = 'C' THEN '3' WHEN f.bbttype = 'D' THEN '4' ELSE '' END,"
                sSql += "       0, p.sujumin1 || sujumin2, p.suname, p.sex, '0', o.owngbn, o.in_out_gubun, o.fkocs, o.bunho,"
                sSql += "       p.suname, p.sex || '/'|| :age, p.sujumin1 || p.sujumin2, o.order_date, o.hangmog_code, :bcno"
                sSql += "  FROM mts0001_lis o,"
                sSql += "       (SELECT bunho, suname, sujumin1, sujumin2, address1, address2, tel1, tel2, sex, owngbn"
                sSql += "          FROM (SELECT 0 seq, bunho, suname, birth, sujumin1, sujumin2, zip_code1, zip_code2, address1, address2,"
                sSql += "                      tel1, tel2, sex, 'O' owngbn"
                sSql += "                 FROM ocs_db..vw_mts0002"
                sSql += "                WHERE bunho = :regno"
                sSql += " 	             UNION ALL"
                sSql += "               SELECT 0 seq, bunho, suname, birth, sujumin1, sujumin2, zip_code1, zip_code2, address1, address2,"
                sSql += "                      tel1, tel2, sex, 'L' owngbn"
                sSql += "                 FROM mtsS0002_lis"
                sSql += "                WHERE bunho = :regno"
                sSql += "              ) t"
                sSql += "        WHERE ROWNUM = 1"
                sSql += "        ORDER BY seq"
                sSql += "      ) p, lf060m f"
                sSql += " WHERE o.bunho = :regno"
                sSql += "   AND o.bunho = p.bunho"
                sSql += "   AND o.hangmog_code = f.tordcd"
                sSql += "   AND o.specimen_code = f.spccd"
                sSql += "   AND f.usdt <= fn_ack_sysdate"
                sSql += "   AND f.UEDT >  fn_ack_sysdate"
                sSql += "   AND f.bbttype IN ('5', '7', 'A', 'B', 'C', 'D')"
                sSql += "   AND (o.fkocs) IN (SELECT fkocs FROM lj011m WHERE bcno = :bcno)"
                sSql += "   AND o.spcflg ='1'"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("donregno", OracleDbType.Varchar2).Value = sDonRegNo
                    .Parameters.Add("donregdt", OracleDbType.Varchar2).Value = sJubSuDt
                    .Parameters.Add("rctdondt", OracleDbType.Varchar2).Value = sJubSuDt
                    .Parameters.Add("age", OracleDbType.Varchar2).Value = r_listcollData.Item(0).AGE
                    .Parameters.Add("age", OracleDbType.Varchar2).Value = r_listcollData.Item(0).AGE
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery

                End With

                If iRet < 1 Then Return 0

                sSql = "UPDATE mts0001_lis"
                sSql += "   SET spcflg = '1', colldt = :colldt"
                sSql += " WHERE bunho = :regno"
                sSql += "   AND (fkocs, hangmog_code) IN "
                sSql += "       (SELECT iogbn, tordcd FROM lb010m WHERE donregno = :donregno)"
                sSql += "   AND NVL(spcflg, '0') = '0'"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("colldt", OracleDbType.Varchar2).Value = r_listcollData.Item(0).COLLDT

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(0).REGNO
                    .Parameters.Add("donregno", OracleDbType.Varchar2).Value = sDonRegNo

                    iRet = .ExecuteNonQuery
                End With

                Return 1

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Protected Function ExecuteDo_One_GetCollDtPrtBcNo(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Protected Function ExecuteDo_One_AddEnt(String, ArrayList) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "SELECT fn_ack_sysdate sysdt, fn_ack_get_bcno_prt(:bcno) prtbcno FROM DUAL"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                Dim dbDa As New OracleDataAdapter(dbCmd)

                Dim dt As New DataTable

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Protected Function ExecuteDo_One_GetCollDtOwnGbn(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Protected Function ExecuteDo_One_GetCollDtOwnGbn(String, ArrayList) As Integer"

            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try
                dbCmd = New OracleCommand

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "SELECT fn_ack_sysdate sysdt, owngbn"
                    sSql += "  FROM lj010m"
                    sSql += " WHERE bcno = :bcno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                Dim dbDa As New OracleDataAdapter(dbCmd)

                Dim dt As New DataTable

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Public Function ExecuteDo_Comment(ByVal r_listcollData As List(Of STU_CollectInfo)) As Boolean
            Dim sFn As String = "Public Sub ExecuteDo_Comment(List(Of STU_CollectInfo))"

            Try
                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim dbCmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim iRows As Integer = 0
                Dim sSql As String = ""

                For i As Integer = 1 To r_listcollData.Count
                    Dim collData As STU_CollectInfo = r_listcollData.Item(i - 1)

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = "DELETE lo010m WHERE fkocs = :fkocs"

                        .Parameters.Clear()
                        .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = collData.FKOCS

                        .ExecuteNonQuery()

                        .Parameters.Clear()

                        sSql = ""
                        sSql += "INSERT INTO lo010m( fkocs, regno, orddt, ocs_key, cmtcont, editid, editip, editdt )"

                        If collData.OWNGBN = "L" Then
                            sSql += "SELECT :fkocs, bunho, order_date,  0, :cmtcont, :editid, :editip, fn_ack_sysdate"
                            sSql += "  FROM mts0001_lis"
                            sSql += " WHERE fkocs = :fkocs"

                            .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = collData.FKOCS
                            .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = collData.COMMENT
                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                            .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = collData.FKOCS

                        Else
                            sSql += "SELECT :fkocs, pid, prcpdd, execprcpuniqno, :cmtcont, :editid, :editip, fn_ack_sysdate"

                            If collData.FKOCS.Split("/"c)(0) = "I" Then
                                sSql += "  FROM emr.mmodexip"
                            Else
                                sSql += "  FROM emr.mmodexop"
                            End If
                            sSql += " WHERE instcd         = '" + PRG_CONST.SITECD + "'"
                            sSql += "   AND prcpdd         = :orddt"
                            sSql += "   AND execprcpuniqno = :ordno"

                            .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = collData.FKOCS
                            .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = collData.COMMENT
                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                            .Parameters.Add("orddt", OracleDbType.Varchar2).Value = collData.FKOCS.Split("/"c)(2)
                            .Parameters.Add("ordno", OracleDbType.Int64).Value = collData.FKOCS.Split("/"c)(3)
                        End If

                        .CommandText = sSql

                        iRows += .ExecuteNonQuery()
                    End With
                Next

                If iRows > 0 Then
                    m_dbTran.Commit()

                    Return True
                Else
                    m_dbTran.Rollback()

                    Return False
                End If

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

        ' 채혈일시 등록
        Public Function ExecuteDo_CollDt(ByVal rsBcNo As String, ByVal rbTakeYn As Boolean) As Boolean
            Dim sFn As String = "Public Function ExecuteDo_CollDt(String, Boolean) As Boolean"

            Dim DbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRow As Integer = 0

            Try
                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sErrVal As String = ""

                With DbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_collector_colldt"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If IsNumeric(sErrVal.Substring(0, 2)) Then
                    If sErrVal.Substring(0, 2) = "00" Then

                        If rbTakeYn Then
                            Dim iRows As Integer = ExecuteDo_One_AddTake(rsBcNo)

                            If iRows = 0 Then
                                m_dbTran.Rollback()
                                Return False
                            End If
                        End If

                        m_dbTran.Commit()
                        Return True
                    Else
                        m_dbTran.Rollback()
                        Return False
                    End If
                Else
                    m_dbTran.Rollback()
                    Return False
                End If

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If DbCmd IsNot Nothing Then DbCmd = Nothing
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        ' 새로운 바코드번호 생성여부 판정.
        Public Function fnNewBCNO_Judge(ByVal aoCurRowData As STU_TestItemInfo, ByRef aoOldRowData As STU_TestItemInfo) As Boolean
            Dim sFn As String = "Private Function fnNewBCNO_Judge(ByVal aoCurRowData As clsTestItem_Info, ByRef aoOldRowData As clsTestItem_Info) As Boolean"
            Dim blnNewBCNO As Boolean = False

            Try
                '0. 등록번호가 틀리면 새로운 검체번호 발생
                If aoOldRowData.REGNo <> aoCurRowData.REGNo Then

                    aoOldRowData.REGNo = aoCurRowData.REGNo
                    blnNewBCNO = True
                End If

                '1. 검사계의 검체가 틀린경우 새로운 검체번호 발생
                If aoOldRowData.BCCLSCD <> aoCurRowData.BCCLSCD Or _
                   aoOldRowData.SPCCD <> aoCurRowData.SPCCD Then

                    aoOldRowData.BCCLSCD = aoCurRowData.BCCLSCD
                    aoOldRowData.SPCCD = aoCurRowData.SPCCD
                    blnNewBCNO = True
                End If

                '2. 처방일시가 틀린경우 조건에 따라 다른번호 발생
                If aoOldRowData.ORDDT <> aoCurRowData.ORDDT Then
                    aoOldRowData.ORDDT = aoCurRowData.ORDDT
                    ' 처방일시별로 바코드 생성될경우에만 체크(기본)
                    If mblnBCNO_ORDDT_GBN = True Then blnNewBCNO = True
                End If

                '2.1 진료과코드가 틀린경우 
                If aoOldRowData.DEPTCD <> aoCurRowData.DEPTCD Then
                    aoOldRowData.DEPTCD = aoCurRowData.DEPTCD
                    blnNewBCNO = True
                End If

                '2.2 의뢰의사가 틀린경우
                If aoOldRowData.DOCTORCD <> aoCurRowData.DOCTORCD Then
                    aoOldRowData.DOCTORCD = aoCurRowData.DOCTORCD
                    blnNewBCNO = True
                End If

                '3. 검사계의 검체가 같고, TubeCd가 틀린경우
                If aoOldRowData.TUBECD <> aoCurRowData.TUBECD Then
                    aoOldRowData.TUBECD = aoCurRowData.TUBECD
                    blnNewBCNO = True
                End If

                '4. 검사계의 검체가 같고, 외주검사 인경우
                If aoOldRowData.EXLABYN = "1" Then
                    blnNewBCNO = True
                End If

                '5. 검체코드가 틀린경우
                If aoOldRowData.BCNO <> aoCurRowData.BCNO Then
                    blnNewBCNO = True
                    aoOldRowData.BCNO = aoCurRowData.BCNO
                End If

                '6. 같은 검사인 경우 분리
                If aoOldRowData.TCLS_SPC = aoCurRowData.TESTCD + aoCurRowData.SPCCD Then
                    blnNewBCNO = True
                End If
                aoOldRowData.TCLS_SPC = aoCurRowData.TESTCD + aoCurRowData.SPCCD

                fnNewBCNO_Judge = blnNewBCNO

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        ' 새로운 바코드번호 생성여부 판정.
        Public Function fnNewBCNO_Judge_Bundle(ByVal aoCurRowData As STU_TestItemInfo, ByRef aoOldRowData As STU_TestItemInfo) As Boolean
            Dim sFn As String = "Private Function fnNewBCNO_Judge(ByVal aoCurRowData As clsTestItem_Info, ByRef aoOldRowData As clsTestItem_Info) As Boolean"
            Dim blnNewBCNO As Boolean = False

            Try
                '0. 등록번호가 틀리면 새로운 검체번호 발생
                If aoOldRowData.REGNo <> aoCurRowData.REGNo Then

                    aoOldRowData.REGNo = aoCurRowData.REGNo
                    blnNewBCNO = True
                End If

                '1. 검사계의 검체가 틀린경우 새로운 검체번호 발생
                If aoOldRowData.BCCLSCD <> aoCurRowData.BCCLSCD Or _
                   aoOldRowData.SPCCD <> aoCurRowData.SPCCD Then

                    aoOldRowData.BCCLSCD = aoCurRowData.BCCLSCD
                    aoOldRowData.SPCCD = aoCurRowData.SPCCD
                    blnNewBCNO = True
                End If

                '2. 처방일시가 틀린경우 조건에 따라 다른번호 발생
                If aoOldRowData.NRS_TIME <> aoCurRowData.NRS_TIME Then
                    aoOldRowData.NRS_TIME = aoCurRowData.NRS_TIME
                    ' 처방일시별로 바코드 생성될경우에만 체크(기본)
                    If mblnBCNO_ORDDT_GBN = True Then blnNewBCNO = True
                End If

                '2.1 진료과코드가 틀린경우 
                If aoOldRowData.DEPTCD <> aoCurRowData.DEPTCD Then
                    aoOldRowData.DEPTCD = aoCurRowData.DEPTCD
                    blnNewBCNO = True
                End If

                '2.2 의뢰의사가 틀린경우
                If aoOldRowData.DOCTORCD <> aoCurRowData.DOCTORCD Then
                    aoOldRowData.DOCTORCD = aoCurRowData.DOCTORCD
                    blnNewBCNO = True
                End If

                '3. 검사계의 검체가 같고, TubeCd가 틀린경우
                If aoOldRowData.TUBECD <> aoCurRowData.TUBECD Then
                    aoOldRowData.TUBECD = aoCurRowData.TUBECD
                    blnNewBCNO = True
                End If

                ''4. 검사계의 검체가 같고, 외주검사 인경우
                'If aoOldRowData.EXLABYN = "1" Then
                '    blnNewBCNO = True
                'End If

                '5. 검체코드가 틀린경우
                If aoOldRowData.BCNO <> aoCurRowData.BCNO Then
                    blnNewBCNO = True
                    aoOldRowData.BCNO = aoCurRowData.BCNO
                End If

                '6. 같은 검사인 경우 분리
                If aoOldRowData.TCLS_SPC = aoCurRowData.TESTCD + aoCurRowData.SPCCD Then
                    blnNewBCNO = True
                End If
                aoOldRowData.TCLS_SPC = aoCurRowData.TESTCD + aoCurRowData.SPCCD

                fnNewBCNO_Judge_Bundle = blnNewBCNO

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 이전검체번호와 같고 복수구분 증가여부 판정. 
        Public Function fnPluralBCNO_Judge(ByVal aoCurRowData As STU_TestItemInfo, ByVal abNewBCNO As Boolean, ByVal asSeqtYn As String) As Boolean
            Dim sFn As String
            Dim blnNewPLURAL As Boolean = False

            Try
                '1. 연속검사인경우 바코드번호 발생 안하고 복수구분 증가
                If aoCurRowData.SEQTYN = "1" And (asSeqtYn = aoCurRowData.SEQTYN Or asSeqtYn = "") Then blnNewPLURAL = True

                '2. 새로운 검체번호 발생시 혈액은행계의 검사에서 ABO가 있는경우 ( 검체가 틀려도 복수구분 설정 ) 
                '   이전의 ABO검사와 검체번호 같고 복수 구분 변경
                If aoCurRowData.BCCLSCD.Substring(0, 1) = "B" And _
                   abNewBCNO = True And aoCurRowData.DBLTSEQ = "1" Then blnNewPLURAL = True

                fnPluralBCNO_Judge = blnNewPLURAL

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 새로운 바코드번호 생성
        Private Sub sbNewBCNO(ByRef BCNO_info As clsBCNO_Info, ByVal asDate As String, ByVal asGBN As String)
            Dim sFn As String = "Private Sub fnNewBCNO(ByRef BCNO_info As clsBCNO_Info, ByVal asDate As String, ByVal asGBN As String)"
            Dim aoBCNO As New clsBCNO_Info

            Try
                With aoBCNO
                    .YYYYMMDD = asDate
                    .BCCLSCD = asGBN
                    .SPCSEQNO = (New Collfn).GetBCNO(.YYYYMMDD, .BCCLSCD)
                End With

                BCNO_info = aoBCNO
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Public Function GetNewBcNo(ByVal r_listcollData As List(Of STU_CollectInfo)) As String
            Dim sFn As String = "Public Function GetNewBcNo(List(Of STU_CollectInfo)) As String"

            Dim sSql As String = "pro_ack_exe_seqno_bc"

            Dim dbCmd As OracleCommand
            Dim dbParam As New OracleParameter  'New DBORA.DbParrameter

            Try
                '> 연속검사 샘플 판별
                If r_listcollData.Item(0).SERIES Then
                    miPlural += 1
                Else
                    miPlural = 0
                End If

                If miPlural > 0 Then
                    If msBcNoBuf.Length = PRG_CONST.Len_BcNo Then
                        If miPlural > 9 Then
                            miPlural = 0
                        Else
                            Return msBcNoBuf.Substring(0, PRG_CONST.Len_BcNo - 1) + miPlural.ToString
                        End If
                    Else
                        miPlural = 0
                    End If
                End If

                Dim iSeqNo As Integer = 0

                dbCmd = New OracleCommand

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()

                    '<
                    dbParam = New OracleParameter()

                    With dbParam
                        .ParameterName = "rs_seqymd" : .DbType = DbType.String : .Direction = ParameterDirection.Input : .Value = r_listcollData.Item(0).COLLDT.Replace("-", "").Substring(0, 8)
                    End With

                    .Parameters.Add(dbParam)

                    dbParam = Nothing
                    '>

                    '<
                    dbParam = New OracleParameter()

                    With dbParam
                        .ParameterName = "rs_seqgbn" : .DbType = DbType.String : .Direction = ParameterDirection.Input : .Value = r_listcollData.Item(0).BCCLSCD
                    End With

                    .Parameters.Add(dbParam)

                    dbParam = Nothing
                    '>

                    '<
                    dbParam = New OracleParameter()

                    With dbParam
                        .ParameterName = "rn_seqno" : .DbType = DbType.Int32 : .Direction = ParameterDirection.InputOutput : .Value = iSeqNo
                    End With

                    .Parameters.Add(dbParam)

                    dbParam = Nothing
                    '>

                    .ExecuteNonQuery()
                End With

                Dim sBcNo As String = ""

                iSeqNo = CInt(dbCmd.Parameters("rn_seqno").Value)

                If iSeqNo < 1 Or iSeqNo > PRG_CONST.Max_BcNoSeq Then
                    sBcNo = ""
                Else
                    sBcNo = r_listcollData.Item(0).COLLDT.Replace("-", "").Substring(0, 8) + r_listcollData.Item(0).BCCLSCD + iSeqNo.ToString("D4") + miPlural.ToString("D1")
                End If

                msBcNoBuf = sBcNo

                Return sBcNo

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose()
                    dbCmd = Nothing
                End If

            End Try
        End Function



#Region " clsBCNO_Info "
        Private Class clsBCNO_Info
            Public YYYYMMDD As String = ""      ' 년월일(yyyymmdd)
            Private msBCCLSGBN As String = ""   ' 검사계구분
            Public BCCLSCD As String            ' 검체분류코드
            Public SPCSEQNO As String = ""      ' 검체순번
            Public PLURAL As String = "0"       ' 복수구분

            Public PRTADDYN As String = ""      '-- 추가바코드 여부("Y/'')
            Public STATGBN As Boolean = False   ' 현재검체번호의 응급여부


            Public Property BCNO() As String
                Get
                    Dim strBCNO As String

                    strBCNO = YYYYMMDD & BCCLSCD & SPCSEQNO & PLURAL
                    If strBCNO = "0" Then
                        BCNO = ""
                    Else
                        BCNO = strBCNO
                    End If

                End Get

                Set(ByVal Value As String)
                    If Value.Length = 15 Then
                        YYYYMMDD = Value.Substring(0, 8)
                        BCCLSCD = Value.Substring(8, 2)
                        SPCSEQNO = Value.Substring(10, 4)
                        PLURAL = Value.Substring(14, 1)
                    Else
                        YYYYMMDD = ""
                        BCCLSCD = ""
                        SPCSEQNO = ""
                        PLURAL = "0"
                    End If
                End Set
            End Property

            ' 복수구분 1증가 
            Public Sub PluralAdd()
                PLURAL = (CInt(PLURAL) + 1).ToString
            End Sub

            Public Sub New()
                MyBase.New()
            End Sub

        End Class
#End Region

    End Class

    '-- 환자특이사항 등록
    Public Class SpCmtReg
        Private Const msFile As String = "File : CGLISAPP_C.vb, Class : LISAPP.APP_C.SpCmtReg" + vbTab

        Private m_dbCn As oracleConnection
        Private m_dbTran As OracleTransaction

        Public Function Reg_SpecalComment(ByVal rsRegNo As String, ByVal rsSpComment As String, ByVal rsIoGbn As String, _
                                          ByVal riCmtGbn As Integer, ByVal rsUsrId As String, _
                                          Optional ByVal rbDel As Boolean = False) As Integer
            Dim sFn As String = "Reg_SpecalComment"

            Dim sSql As String = ""

            Dim dbCmd As New OracleCommand

            Dim iRow As Integer = 0

            Try
                If m_dbCn Is Nothing Then m_dbCn = GetDbConnection()

                m_dbTran = m_dbCn.BeginTransaction()
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "SELECT regno  FROM lj040m"
                    sSql += " WHERE regno = :regno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo

                    Dim dt As New DataTable
                    Dim objDAdapter As New OracleDataAdapter(dbCmd)
                    objDAdapter.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        sSql = ""
                        sSql += "INSERT INTO lj040h("
                        sSql += "       moddt, modid, regno ,iogbn, remark, regid, regdt, editid, editdt, editip"
                        sSql += "     ) "
                        sSql += "SELECT fn_ack_sysdate, :modid, a.*  FROM lj040m a"
                        sSql += " WHERE a.regno = :regno"

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("modid", OracleDbType.Varchar2).Value = rsUsrId
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo


                        iRow = .ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE FROM lj040m"
                        sSql += " WHERE regno = :regno"

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo

                        iRow = .ExecuteNonQuery()

                    End If

                    If Not rbDel Then
                        sSql = ""
                        sSql += "INSERT INTO lj040m("
                        sSql += "            regno, iogbn, remark, regid, regdt, editid, editdt, editip)"
                        sSql += "    VALUES( :regno, :iogbn, :remark, :regid, fn_ack_sysdate,:editid,fn_ack_sysdate,:editip )"

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo
                        .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = rsIoGbn
                        .Parameters.Add("remark", OracleDbType.Varchar2).Value = rsSpComment
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = rsUsrId
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP


                        iRow = .ExecuteNonQuery()
                    End If
                End With

                If iRow > 0 Then
                    m_dbTran.Commit()
                End If

                Return iRow

            Catch ex As Exception
                If m_dbTran IsNot Nothing Then
                    If m_dbTran.Connection IsNot Nothing Then
                        m_dbTran.Rollback()
                    End If
                End If

                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then dbCmd = Nothing
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

    End Class

#End Region

#Region " 헌혈자 검사항목등록 (채혈, 혈액은행계 검체접수) : Class DB_DonTestCdReg "
    Public Class DonTestReg
        Private Const msFile As String = "File : CGDA_C.vb, Class : DA01.DataAccess.DB_DonTestCdReg" & vbTab

        Public Class PatInfo
            Public PATNM As String = ""
            Public SEX As String = ""
            Public AGE As String = ""
            Public IDNOL As String = ""
            Public IDNOR As String = ""
            Public TEL1 As String = ""
            Public TEL2 As String = ""
            Public HEIGHT As String = ""
            Public WEIGHT As String = ""
            Public ORDDT As String = ""
        End Class

        Private mPatinfo As New PatInfo
        Private malBCNO As New ArrayList
        Private msBCPrtMsg As String

        Private mbErrCollect As Boolean = True
        Private mbErrJubSu As Boolean = True

        Public Sub New()
            MyBase.New()
        End Sub

        ' 바코드 출력 메세지
        Public ReadOnly Property BCPrtMsg() As String
            Get
                BCPrtMsg = msBCPrtMsg
            End Get
        End Property

        ' 채혈된 검체번호 반환
        Public ReadOnly Property BCNO() As ArrayList
            Get
                BCNO = malBCNO
            End Get
        End Property

        ' 채혈 에러여부 
        Public ReadOnly Property ErrCollect() As Boolean
            Get
                ErrCollect = mbErrCollect
            End Get
        End Property

        ' 접수 에러여부 
        Public ReadOnly Property ErrJubSu() As Boolean
            Get
                ErrJubSu = mbErrJubSu
            End Get
        End Property

        Private Sub fnValidation()
            Dim sFn As String = "Private Function fnValidation() As Boolean"

            Try
                With mPatinfo
                    If .PATNM.Trim.Equals("") Then Throw (New Exception("필수항목 입력오류[이름]"))
                    If .SEX.Trim.Equals("") Then Throw (New Exception("필수항목 입력오류[성별]"))
                    If .AGE.Trim.Equals("") Then Throw (New Exception("필수항목 입력오류[나이]"))
                    If .IDNOL.Trim.Equals("") Then Throw (New Exception("필수항목 입력오류[주민등록번호]"))
                    If .IDNOR.Trim.Equals("") Then Throw (New Exception("필수항목 입력오류[주민등록번호]"))
                    'If .TEL1.Trim.Equals("") Then Throw (New Exception("필수항목 입력오류[연락처1]"))
                    'If .HEIGHT.Trim.Equals("") Then Throw (New Exception("필수항목 입력오류[키]"))
                    'If .WEIGHT.Trim.Equals("") Then Throw (New Exception("필수항목 입력오류[몸무게]"))
                    If IsNothing(.ORDDT) Then Throw (New Exception("필수항목 입력오류[등록일시]"))
                End With

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        ' 등록할 검사코드 로드
        Private Sub fnSetDonTestList(ByRef rlDonTestList As ArrayList, ByVal reDonGbn As enumDonGbn)
            Dim sFn As String = "Private Sub SetDonTestList()"
            Dim objDTable As New DataTable
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Dim CollectData As STU_CollectInfo
            Dim dtBirthDay As Date

            Try

                sSql += "SELECT '" + mPatinfo.ORDDT + "' orddt,"
                sSql += "       a.testcd, a.spccd, a.bbgbn, b.bcclscd bcclscd,"
                sSql += "       decode(b.exlabyn, 1, '1', '0') exlabyn, b.tubecd"
                sSql += "  FROM lf140m a, lf060m b"
                sSql += " WHERE a.testcd = b.testcd"
                sSql += "   AND a.spccd  = b.spccd"
                sSql += "   AND b.usdt  <= TO_DATE(:orddt, 'yyyy-mm-dd hh24:mi:ss')"
                sSql += "   and b.uedt  >  TO_DATE(:orddt, 'yyyy-mm-dd hh24:mi:ss')"

                If reDonGbn = enumDonGbn.일반 Then
                    sSql += "   AND a.tordgbn = '1'"  ' 일반 Order항목만 조회
                ElseIf reDonGbn = enumDonGbn.지정 Or reDonGbn = enumDonGbn.성분 Then
                    sSql += "   AND a.dordgbn = '1'"  ' 지정, 성분 Order항목만 조회
                ElseIf reDonGbn = enumDonGbn.자가 Then
                    sSql += "   AND a.aordgbn = '1'"  ' 자가 Order항목만 조회
                End If
                sSql += " ORDER BY a.sortkey"

                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, mPatinfo.ORDDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, mPatinfo.ORDDT))
                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, mPatinfo.ORDDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, mPatinfo.ORDDT))

                DbCommand()
                objDTable = DbExecuteQuery(sSql, alParm)

                If objDTable.Rows.Count > 0 Then
                    For intCnt As Integer = 0 To objDTable.Rows.Count - 1
                        CollectData = New STU_CollectInfo
                        With CollectData
                            .ORDDT = objDTable.Rows(intCnt).Item("ORDDT").ToString
                            .REGNO = ""                         ' 등록번호
                            .PATNM = mPatinfo.PATNM             ' 성명
                            .SEX = mPatinfo.SEX                 ' 성별
                            .AGE = mPatinfo.AGE                 ' 나이
                            If mPatinfo.IDNOL.Length.Equals(6) Then
                                If mPatinfo.IDNOR.Length > 0 Then
                                    Select Case mPatinfo.IDNOR.Substring(0, 1)
                                        Case "1", "2", "5", "6"
                                            dtBirthDay = CDate("19" + mPatinfo.IDNOL.Substring(0, 2) & "-" _
                                                             & mPatinfo.IDNOL.Substring(2, 2) & "-" _
                                                             & mPatinfo.IDNOL.Substring(4, 2))
                                        Case "3", "4", "7", "8"
                                            dtBirthDay = CDate("20" + mPatinfo.IDNOL.Substring(0, 2) & "-" _
                                                             & mPatinfo.IDNOL.Substring(2, 2) & "-" _
                                                             & mPatinfo.IDNOL.Substring(4, 2))
                                        Case "9", "0"
                                            dtBirthDay = CDate("18" + mPatinfo.IDNOL.Substring(0, 2) & "-" _
                                                             & mPatinfo.IDNOL.Substring(2, 2) & "-" _
                                                             & mPatinfo.IDNOL.Substring(4, 2))
                                    End Select
                                    .BIRTHDAY = Format(dtBirthDay, "yyyy-MM-dd hh:mm")
                                Else
                                    dtBirthDay = CDate(mPatinfo.IDNOL.Substring(0, 2) & "-" _
                                                     & mPatinfo.IDNOL.Substring(2, 2) & "-" _
                                                     & mPatinfo.IDNOL.Substring(4, 2))
                                    .BIRTHDAY = Format(dtBirthDay, "yyyy-MM-dd hh:mm")

                                End If
                                .DAGE = CType(DateDiff(DateInterval.Day, dtBirthDay, CDate(mPatinfo.ORDDT)), String)    ' 일 환산 나이
                            Else
                                .DAGE = ""
                            End If
                            .IDNOL = mPatinfo.IDNOL             ' 주민등록번호 왼쪽
                            .IDNOR = mPatinfo.IDNOR             ' 주민등록번호 오른쪽
                            .TEL1 = mPatinfo.TEL1               ' 연락처1
                            .TEL2 = mPatinfo.TEL2               ' 연락처2
                            .JUBSUGBN = "10"                    ' 접수구분 -> 헌혈
                            .ORDDT = Format(mPatinfo.ORDDT, "yyyy-MM-dd hh:mm:ss") ' 처방일시

                            .HEIGHT = mPatinfo.HEIGHT
                            .WEIGHT = mPatinfo.WEIGHT

                            .IOGBN = "9"                        ' O:외래, I:입원구분
                            .FKOCS = ""                         ' OCSKey
                            .BCPRTDT = ""                       ' 바코드출력일시

                            .ORDDT = Format(mPatinfo.ORDDT, "yyyy-MM-dd hh:mm")
                            .TCLSCD = objDTable.Rows(intCnt).Item("TCLSCD").ToString
                            .SPCCD = objDTable.Rows(intCnt).Item("SPCCD").ToString
                            .OWNGBN = "L"

                            .BCCLSCD = objDTable.Rows(intCnt).Item("bcclscd").ToString
                            .EXLABYN = objDTable.Rows(intCnt).Item("EXLABYN").ToString
                            .TUBECD = objDTable.Rows(intCnt).Item("TUBECD").ToString
                        End With
                        rlDonTestList.Add(CollectData)
                    Next
                    rlDonTestList.TrimToSize()

                Else
                    Throw (New Exception("헌혈자 검사항목 Load 오류"))

                End If

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub


    End Class

#End Region

End Namespace

