﻿'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : 관동대명지병원 Laboratory Information System(KMC_LIS)                  */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_J.vb                                                              */
'/* PartName     : 접수관리                                                               */
'/* Description  : 접수관리의 Data Query구문관련 Class                                    */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     : 2004-02-19 Jin Hwa Ji : 혈액은행 기본검사 취소 추가                    */
'/*                                        Class DB_Cancel                                */
'/* Modified     : 2010-09-09 박정은 : 리뉴얼 관련 프로그램 수정                          */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Imports OCSAPP

Namespace APP_J

    Public Class TkFn
        Private Const msFile As String = "File : CGLISAPP_J.vb, Class : LISAPP.APP_J.Qry" + vbTab

        Public Shared Function fn_ExcuteDoBrainTake(ByVal rsBcno As String) As Boolean
            Dim sFn As String = " Public Shared Function fn_ExcuteDoBrainTake(ByVal rsBcno As String) As String"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim iRet As Integer = 0

          
            Try
                sSql = "SELECT cancelyn FROM lj014m "
                sSql += "WHERE bcno = '" + rsBcno + "'"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql)


                If dt.Rows.Count <= 0 Then
                    sSql = "INSERT INTO lj014m (bcno, brainyn, cancelyn)"
                    sSql += "VALUES(:bcno, 'Y', 'N') "

                    alParm.Clear()
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                    DbCommand()
                    iRet += DbExecute(sSql, alParm)
                ElseIf dt.Rows.Count > 0 And dt.Rows(0).Item("cancelyn").ToString.Equals("Y") Then
                    sSql = "UPDATE lj014m SET cancelyn = 'N'"
                    sSql += "WHERE bcno = :bcno "

                    alParm.Clear()
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                    DbCommand()
                    iRet += DbExecute(sSql, alParm)

                ElseIf dt.Rows.Count > 0 And dt.Rows(0).Item("cancelyn").ToString.Equals("N") Then
                    Return True
                End If

                If iRet > 0 Then

                    Return True
                Else

                    Return False
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
              

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function


        Public Shared Function fnGet_Abn_List_regno(ByVal rsRegno As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Abn_List_regno(ByVal rsRegno As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Try
                sSql = " SELECT b.patnm, b.sex||'/'||b.age as sexage,  a.regno ,  a.bcno , a.cmtcont," + vbCrLf
                sSql += "       c.testcd,c.spccd,d.tnmd,e.spcnmd, c.viewrst ,  fn_ack_date_str(c.fndt, 'yyyy-mm-dd hh24:mi') as fndt, " + vbCrLf
                sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi') as regdt , fn_ack_get_usr_name(a.regid) as regid , fn_ack_get_usr_name(c.fnid) as fnid" + vbCrLf
                sSql += "  FROM lr050m a " + vbCrLf
                sSql += "  JOIN lj010m b " + vbCrLf
                sSql += "    ON a.bcno = b.bcno " + vbCrLf
                sSql += "   AND b.spcflg = '4' " + vbCrLf
                sSql += "  JOIN lr010m c " + vbCrLf
                sSql += "    ON b.bcno = c.bcno " + vbCrLf
                sSql += "   AND b.spccd = c.spccd " + vbCrLf
                sSql += "  JOIN lf060m d " + vbCrLf
                sSql += "    ON d.testcd = c.testcd " + vbCrLf
                sSql += "   AND d.spccd = c.spccd " + vbCrLf
                sSql += "   AND d.usdt <= c.tkdt " + vbCrLf
                sSql += "   AND d.uedt > c.tkdt " + vbCrLf
                sSql += "  JOIN lf030m e " + vbCrLf
                sSql += "    ON e.spccd = d.spccd " + vbCrLf
                sSql += "   AND e.usdt <= c.tkdt " + vbCrLf
                sSql += "   AND e.uedt > c.tkdt " + vbCrLf
                sSql += " WHERE a.regno = :regno " + vbCrLf
                sSql += " ORDER BY a.regdt desc " + vbCrLf

                alParm.Add(New OracleParameter("regno", rsRegno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_PatInfo_List(ByVal rsRegNo As String, ByVal rsBcNo As String, ByVal rsDeptCd As String, ByVal rsWardNo As String) As DataTable
            Dim sFn As String = "Public Function fnGet_PatInfo_List(String, String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT '' chk, fn_ack_get_bcno_full(j.bcno) bcno, j.regno,"
                sSql += "       j.patnm, j.sex ||  '/' || j.age sexage,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-ss hh24:mi') orddt, fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptward,"
                sSql += "       CASE WHEN NVL(j.rstflg, '0') <> '0' THEN '결과'"
                sSql += "            ELSE CASE WHEN j.spcflg = '1' THEN '바코드발행'"
                sSql += "                      WHEN j.spcflg = '2' THEN '채혈'"
                sSql += "                      WHEN j.spcflg = '3' THEN '검체전달'"
                sSql += "                      ELSE '접수'"
                sSql += "                 END"
                sSql += "       END spcflg,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmds,"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmds,"
                sSql += "       j.doctorcd"
                sSql += "  FROM lj010m j"
                sSql += " WHERE j.spcflg IN ('1', '2', '3', '4')"
                sSql += "   AND j.owngbn <> 'H'"

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", rsRegNo))
                End If

                If rsBcNo.Length = 14 Then
                    sSql += "   AND j.bcno >= :bcno || '0' AND j.bcno <= :bcno || '9'"
                    alParm.Add(New OracleParameter("bcno", rsBcNo))
                    alParm.Add(New OracleParameter("bcno", rsBcNo))
                ElseIf rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", rsBcNo))
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", rsDeptCd))
                End If

                If rsWardNo <> "" Then
                    sSql += "   AND j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", rsWardNo))
                End If

                sSql += " ORDER BY orddt DESC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try


        End Function

        Public Shared Function fnGet_Coll_PatInfo(ByVal rsRegNo As String, ByVal rsBcNo As String, ByVal rsBcclsCd As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Coll_PatInfo(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT '' chk, fn_ack_get_bcno_full(j.bcno) bcno, j.regno,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-ss hh24:mi') orddt,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmds,"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmds, "
                sSql += " (SELECT b.cwarning "
                sSql += " FROM lj011m a, lf060m b "
                sSql += " WHERE(a.bcno = j.bcno)"
                sSql += " AND a.tclscd = b.testcd"
                sSql += " AND a.spccd = b.spccd"
                sSql += " AND b.usdt <= j.bcprtdt"
                sSql += " AND b.uedt > j.bcprtdt"
                sSql += " and rownum < 2"
                sSql += "  ) cwarning, "
                sSql += "       j.spcflg"
                sSql += "  FROM lj010m j"
                sSql += " WHERE j.spcflg IN ('2', '3')"
                sSql += "   AND j.owngbn <> 'H'"

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsBcNo.Length = 14 Then
                    sSql += "   AND j.bcno >= :bcno || '0' AND j.bcno <= :bcno || '9'"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                ElseIf rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                If rsBcclsCd <> "" Then
                    sSql += "   AND j.bcclscd = :bcclscd"
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                Else
                    If PRG_CONST.BCCLS_BldCrossMatch <> "" Then sSql += "   AND NVL(j.bcclscd, ' ') <> '" + PRG_CONST.BCCLS_BldCrossMatch + "'"
                End If

                sSql += " ORDER BY orddt DESC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try


        End Function

        Public Shared Function fnGet_Pass_PatInfo(ByVal rsRegNo As String, ByVal rsBcNo As String, ByVal rsBcclsCd As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Coll_PatInfo(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT '' chk, fn_ack_get_bcno_full(j.bcno) bcno, j.regno,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-ss hh24:mi') orddt,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptward,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmds"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmds"
                sSql += "  FROM lj010m j"
                sSql += " WHERE j.spcflg = '2'"
                sSql += "   AND j.owngbn <> 'H'"

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsBcNo.Length = 14 Then
                    sSql += "   AND j.bcno >= :bcno || '0' AND j.bcno <= :bcno || '9'"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                ElseIf rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                If rsBcclsCd <> "" Then
                    sSql += "   AND j.bcclscd = :bcclscd"
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                ElseIf PRG_CONST.BCCLS_BldCrossMatch <> "" Then
                    sSql += "   AND j.bcclscd <> '" + PRG_CONST.BCCLS_BldCrossMatch + "'"
                End If

                sSql += " ORDER BY orddt DESC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try


        End Function

        Public Shared Function fnGet_tk_PatInfo(ByVal rsRegNo As String, ByVal rsBcNo As String, ByVal rsPartCd As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Coll_PatInfo(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT '' chk, fn_ack_get_bcno_full(j.bcno) bcno, j.regno,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-ss hh24:mi') orddt,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptward,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmds"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmds"
                sSql += "  FROM lj010m j"
                sSql += " WHERE j.spcflg = '4'"
                sSql += "   AND j.owngbn <> 'H'"

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsBcNo.Length = 14 Then
                    sSql += "   AND j.bcno >= :bcno ||'0' AND j.bcno <= :bcno ||'9'"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                ElseIf rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                If rsPartCd <> "" Then
                    sSql += "   AND j.bcno IN"
                    sSql += "       (SELECT j1.bcno FROM lj011m j1, lf060m f"
                    sSql += "         WHERE j1.bcno    = j.bcno"
                    sSql += "           AND j1.tclscd  = f.testcd"
                    sSql += "           AND j1.spccd   = f.spccd"
                    sSql += "           AND j1.colldt >= f.usdt"
                    sSql += "           AND j1.colldt <  f.uedt"
                    sSql += "           AND f.partcd   = :partcd"
                    sSql += "       )"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
                End If

                sSql += " ORDER BY orddt DESC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try


        End Function


#Region " 접수 "
        Public Shared Function fnGet_Jubsu_BarCode_Info(ByVal rsBcNo As String, ByVal rsBcCnt As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Jubsu_BarCode_Info(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       f.bccnt, MAX(NVL(f.mbttype, '0')) mbttype"
                sSql += "  FROM lj010m j, " + sTableNm + " r, lf060m f"
                sSql += " WHERE j.bcno     = :bcno"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND r.testcd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND j.bcprtdt >= f.usdt"
                sSql += "   AND j.bcprtdt <  f.uedt"

                If rsBcCnt = "J" Then
                    sSql += "   AND f.bccnt IN ('J', 'AJ')"
                ElseIf rsBcCnt = "J2" Then
                    sSql += "   AND f.bccnt IN ('J2', 'A2')"
                End If
                sSql += " GROUP BY f.bccnt"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function


        Public Shared Function fnGet_Take2Yn(ByVal rsBcNo As String, ByVal rsPartCd As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Take2Yn(String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) rstflg"
                sSql += "  FROM " + sTableNm + " r"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND r.partcd = :partcd"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_Take2_PatInfo(ByVal rsBcNo As String, ByVal rsPartCd As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Take2_PatInfo(String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim sTableNm As String = "lr010m"

                If PRG_CONST.PART_MicroBio = rsPartCd Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.iogbn,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptward,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM lj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       fn_ack_get_workno_old_yn(j.bcno) workno_old, j.bcclscd,"
                sSql += "       fn_ack_date_str(r.tkdt,'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       fn_ack_get_usr_name(r.tkid) tknm,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmd,"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmd,"
                sSql += "       j.statgbn, f1.colorgbn"
                sSql += "  FROM lj010m j, " + sTableNm + " r, lf010m f1"
                sSql += " WHERE j.bcno     = :bcno"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND r.partcd   = :partcd"
                sSql += "   AND j.owngbn  <> 'H'"
                sSql += "   AND j.spcflg   = '4'"
                sSql += "   AND NVL(r.wkymd, ' ') = ' '"
                sSql += "   AND j.bcclscd  = f1.bcclscd"
                sSql += "   AND j.bcprtdt >= f1.usdt"
                sSql += "   AND j.bcprtdt <  f1.uedt"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


            End Try

        End Function

        Public Shared Function fnGet_Take2_PatInfo(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartCd As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Take2_PatInfo(String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim sTableNm As String = "lr010m"

                If PRG_CONST.PART_MicroBio = rsPartCd Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.iogbn,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptward,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM lj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       fn_ack_get_workno_old_yn(j.bcno) workno_old, j.bcclscd,"
                sSql += "       fn_ack_date_str(r.tkdt,'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       fn_ack_get_usr_name(r.tkid) tknm,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmd,"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmd,"
                sSql += "       j.statgbn, f1.colorgbn"
                sSql += "  FROM lj010m j, " + sTableNm + " r, lf010m f1"
                sSql += " WHERE r.tkdt    >= :dates"
                sSql += "   AND r.tkdt    <= :datee || '235959'"
                sSql += "   AND r.partcd   = :partcd"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND j.spcflg   = '4'"
                sSql += "   AND NVL(r.wkymd, ' ') = ' '"
                sSql += "   AND j.bcclscd = f1.bcclscd"
                sSql += "   AND j.bcprtdt >= f1.usdt"
                sSql += "   AND j.bcprtdt <  f1.uedt"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
                alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


            End Try

        End Function

        Public Shared Function fnGet_Coll_PatInfo_bcno(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Private Function fnGet_Coll_PatList_bcno(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.bcclscd, j.regno,"
                sSql += "       j.patnm patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward, j.statgbn,"
                sSql += "	    fn_ack_get_workno_old_yn(j.bcno) workno_old,"
                sSql += "       fn_ack_date_diff(j1.colldt, fn_ack_sysdate, '3') tat_mi,"
                sSql += "       f3.spcnmd, f1.colorgbn,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmds"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmds ,"
                sSql += "       (SELECT b.cwarning " '20160225
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "          and rownum <2 "
                sSql += "       ) cwarning "
                sSql += "  FROM lj010m j, lj011m j1, lf030m f3, lf010m f1"
                sSql += " WHERE j.bcno     = :bcno"
                sSql += "   AND j.spcflg  IN ('2', '3')"
                sSql += "   AND j.bcno     = j1.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"
                sSql += "   AND j.bcclscd  = f1.bcclscd"
                sSql += "   AND j.bcprtdt >= f1.usdt"
                sSql += "   AND j.bcprtdt <  f1.uedt"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_Coll_PatList(ByVal rsCollDts As String, ByVal rsCollDte As String, ByVal rsBcclsCd As String) As DataTable
            Dim sFn As String = "Private Function fnGet_Coll_PatList(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                rsCollDts = rsCollDts.Replace("-", "")
                rsCollDte = rsCollDte.Replace("-", "")

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.bcclscd, j.regno,"
                sSql += "       j.patnm, j.sex || '/' ||  j.age sexage,"
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptward,"
                sSql += "       j.statgbn,"
                sSql += "	    fn_ack_get_workno_old_yn(j.bcno) workno_old,"
                sSql += "       fn_ack_date_diff(j1.colldt, fn_ack_sysdate, '3') tat_mi,"
                sSql += "       f3.spcnmd, f1.colorgbn,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmds"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmds"
                sSql += "  FROM lj010m j, lj011m j1, lf030m f3, lf010m f1"
                sSql += " WHERE j1.colldt >= :dates || '000000'"
                sSql += "   AND j1.colldt <= :datee || '235959'"
                sSql += "   AND j.spcflg   = '2'"
                sSql += "   AND j.bcno     = j1.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt < f3.uedt"
                sSql += "   AND j.bcclscd = f1.bcclscd"
                sSql += "   AND j.bcprtdt >= f1.usdt"
                sSql += "   AND j.bcprtdt < f1.uedt"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsCollDts.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCollDts))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsCollDte.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCollDte))

                If rsBcclsCd <> "" Then
                    sSql += "   AND j.bcclscd = :bcclscd"
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_Pass_PatList(ByVal rsCollDts As String, ByVal rsCollDte As String, ByVal rsBcclsCd As String) As DataTable
            Dim sFn As String = "Private Function fnGet_Pass_PatList(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                rsCollDts = rsCollDts.Replace("-", "")
                rsCollDte = rsCollDte.Replace("-", "")

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.bcclscd, j.regno,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptward,"
                sSql += "       j.statgbn,"
                sSql += "	    fn_ack_get_workno_old_yn(j.bcno) workno_old,"
                sSql += "       fn_ack_date_diff(j1.colldt, fn_ack_sysdate, '3') tat_mi,"
                sSql += "       f3.spcnmd, f1.colorgbn,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmds"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnmds ,  "
                sSql += "       (SELECT b.cwarning "
                sSql += "         FROM lj011m a, lf060m b"
                sSql += "        WHERE a.bcno   = j.bcno"
                sSql += "          AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "          AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "          AND rownum <2 "
                sSql += "       ) cwarning "
                sSql += "  FROM lj010m j, lj011m j1, lf030m f3, lf010m f1"
                sSql += " WHERE j1.colldt >= :dates || '000000'"
                sSql += "   AND j1.colldt <= :datee || '235959'"
                sSql += "   AND j.spcflg   = '3'"
                ' sSql += "   AND F6.SPCCD = f3.spccd "
                'sSql += "   AND F6.TESTCD = J1.TCLSCD "
                sSql += "   AND j.bcno     = j1.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt < f3.uedt"
                sSql += "   AND j.bcclscd = f1.bcclscd"
                sSql += "   AND j.bcprtdt >= f1.usdt"
                sSql += "   AND j.bcprtdt < f1.uedt"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsCollDts.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCollDts))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsCollDte.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCollDte))

                If rsBcclsCd <> "" Then
                    sSql += "   AND j.bcclscd = :bcclscd"
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        ' 환자 검사리스트 조회
        Public Shared Function FGJ01_GetOrderList(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Function FGJ01_GetOrderList(String) As DataTable"

            Dim sSql As String = ""
            Dim sTableNm As String = "lr010m"

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo) Then sTableNm = "lm010m"

            sSql += "SELECT j.tclscd, f6.tnmd, fn_ack_date_str(j.colldt, 'yyyy-mm-dd hh24:mi') colldt_c,"
            sSql += "       fn_ack_get_usr_name(j.collid) collnm, f3.spcnmd, f6.bcclscd,"
            sSql += "       NVL(f6.dispseql, 999) sort1, j.doctorrmk, fn_ack_date_diff(j.colldt, fn_ack_sysdate, 3) tat_mi,"
            sSql += "       f6.frptmi, fn_ack_date_str(r.tkdt,'yyyy-mm-dd hh24:mi') tkdt, fn_ack_get_usr_name(r.tkid) tknm,"
            sSql += "       f1.colorgbn"
            sSql += "  FROM lf060m f6, lf030m f3, lf010m f1 lj011m j LEFT OUTER JOIN " + sTableNm + " r ON (j.bcno = r.bcno AND j.tclscd = r.tclscd)"
            sSql += " WHERE j.bcno   = :bcno"
            sSql += "   AND j.tclscd = f6.testcd"
            sSql += "   AND j.spccd  = f6.spccd"
            sSql += "   AND f6.usdt <= j.colldt"
            sSql += "   AND f6.uedt >  j.colldt"
            sSql += "   AND f3.spccd = j.spccd"
            sSql += "   AND f3.usdt <= j.colldt"
            sSql += "   AND f3.uedt >  j.colldt"
            sSql += "   AND f6.bcclscd = f1.bcclscd "
            sSql += "   AND f1.usdt <= j.colldt"
            sSql += "   AND f1.uedt >  j.colldt"
            sSql += " ORDER BY sort1, j.tclscd"

            Try
                Dim alParm As New ArrayList

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        ' 해당검체 접수 유/무 조회
        Public Shared Function fnGet_bcno_state(ByVal rsBcno As String) As DataTable
            ' 정은 수정완료 2010-09-08
            Dim sFn As String = "Public Function FGJ01_bcno_state(ByVal rsBcno As String) As DataTable"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                'sSql += "SELECT DISTINCT"
                'sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.spcflg"
                'sSql += "  FROM lj010m j"
                'sSql += " WHERE bcno    = :bcno"
                'sSql += "   AND owngbn <> 'H'"
                'sSql += "   AND spcflg IN ('0', '1', '3', '4', 'R')"

                '<20141126 접수화면에서도 검체상태에따라 팝업가능하게 수정
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.spcflg , nvl(r.regdt,'0') wrYn"
                sSql += "  FROM lj011m j left outer join lr053m r "
                sSql += "    on r.bcno = j.bcno "
                sSql += " WHERE j.bcno    = :bcno"
                sSql += "   AND j.owngbn <> 'H'"
                sSql += "   AND j.spcflg IN ('0', '1', '3', '4', 'R')"


                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '이전 작업번호 가져오기
        Public Shared Function fnGet_Workno_old(ByVal rsBcNo As String) As String
            Dim sFn As String = "Public Shared Function fnGet_Workno_old(ByVal rsBcNo As String) As String"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT fn_ack_get_workno_old(:bcno, :testcd, '') wkno FROM DUAL"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, "".Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ""))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("wkno").ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

#Region " 취소 "

        'JJH 코로나 특수보고서 결과값
        Public Shared Function FnExe_Delete_LRS17M(ByVal rsBcno As String, ByVal rsTestcd As String) As Integer
            Dim sFn As String = "FGJ02_GetOrderList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += " DELETE LRS17M "
                sSql += "  WHERE BCNO   = :BCNO "
                sSql += "    AND TESTCD = :TESTCD "

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestcd))

                DbCommand()
                Return DbExecute(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        'JJH 자체응급 삭제
        Public Shared Function FnExe_Delete_LJ015M(ByVal rsBcno As String, ByVal rsRegno As String) As Integer
            Dim sFn As String = "FGJ02_GetOrderList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += " DELETE LJ015M "
                sSql += "  WHERE BCNO   = :BCNO "
                sSql += "    AND REGNO  = :REGNO "

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                Return DbExecute(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '환자 검사리스트 조회
        Public Shared Function FGJ02_GetOrderList(ByVal rsBcno As String, Optional ByVal rbUnfitScp As Boolean = False) As DataTable
            Dim sFn As String = "FGJ02_GetOrderList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) Then sTableNm = "lm010m"
                If PRG_CONST.BCCLS_RIS = rsBcno.Substring(8, 2) Then sTableNm = "rr010m"

                sSql += "SELECT DISTINCT"
                sSql += "       j.bcno, j.regno, j.patnm, j.sex|| '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       fn_ack_get_dept_name(j.iogbn, j.deptcd) deptnm, j.spcflg, j1.owngbn, j1.iogbn, j1.fkocs, j1.spcflg spcflg_j1,"
                sSql += "       j1.tclscd, j1.spccd, f6.tnmd, f3.spcnmd, f6.tcdgbn, j1.rstflg, j.bcclscd,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(j1.passdt, 'yyyy-mm-dd hh24:mi') passdt, fn_ack_get_usr_name(j1.passid) passnm,"
                sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi')  tkdt, fn_ack_get_usr_name(j1.tkid) tknm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN fn_ack_get_ward_abbr(j.wardno) || '/' || j.roomno ELSE '' END wardroom,"
                sSql += "       NVL(f6.dispseql, 999) sort2,"
                sSql += "       j1.doctorrmk, f6.tordcd,"
                'sSql += "       fn_ack_get_bcno_exlabstate(j1.bcno, j1.tclscd) exlabstate"
                sSql += "       CASE WHEN (SELECT COUNT(a.testcd)"
                sSql += "                    FROM " + sTableNm + " a, lre11m b"
                sSql += "                   WHERE a.bcno   = b.bcno"
                sSql += "                     AND a.testcd = b.testcd "
                sSql += "                     AND a.bcno   = j1.bcno"
                sSql += "                     AND a.tclscd = j1.tclscd"
                sSql += "                 ) > 0 THEN '의뢰' "
                sSql += "            ELSE '' "
                sSql += "       END exlabstate,"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) Then
                    sSql += "       (SELECT MAX(NVL(cfmsign, '')) FROM lm010m WHERE bcno = j1.bcno AND tclscd = j1.tclscd) cfmsign"
                Else
                    sSql += "       (SELECT MAX(NVL(cfmsign, '')) FROM lr010m WHERE bcno = j1.bcno AND tclscd = j1.tclscd) cfmsign"
                End If

                sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf030m f3"
                sSql += " WHERE (j1.fkocs) IN (SELECT fkocs FROM lj011m WHERE bcno = :bcno)"
                sSql += "   AND j.bcno = j1.bcno"
                sSql += "   AND j1.tclscd = f6.testcd"
                sSql += "   AND j1.spccd  = f6.spccd"
                sSql += "   AND f6.usdt  <= j.bcprtdt"
                sSql += "   AND f6.uedt  >  j.bcprtdt"
                sSql += "   AND j1.spccd  = f3.spccd"
                sSql += "   AND f3.usdt  <= j.bcprtdt"
                sSql += "   AND f3.uedt  >  j.bcprtdt"

                If rbUnfitScp Then
                    sSql += "   AND NVL(j.spcflg,  '0') = '4'"
                    sSql += "   AND NVL(j1.spcflg, '0') = '4'"
                Else
                    sSql += "   AND NVL(j.spcflg,  '0') > '0'"
                    sSql += "   AND NVL(j1.spcflg, '0') > '0'"
                End If
                sSql += "   AND NVL(j1.spcflg, '0') <> 'R'"
                sSql += " ORDER BY bcno, sort2, j1.tclscd"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

#Region " 바코드재출력 "
        ' 바코드재출력 쿼리수정 박정은 2010-09-09
        Public Shared Function FGJ03_ListView(ByVal rsDateS As String, ByVal rsDateE As String, _
                                              ByVal rsRegNo As String, ByVal rsBcNo As String, _
                                              ByVal rsBcclsCd As String, ByVal rsDeptCd As String, _
                                              ByVal rsWardno As String, ByVal rsRoomNo As String) As DataTable
            Dim sFn As String = "Public Shared Function FGJ03_ListView(ByVal adtDate0 As Date, ByVal adtDate1 As Date) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm," + vbCrLf
                sSql += "       j.sex || '/' || j.age sexage," + vbCrLf
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.iogbn," + vbCrLf
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward," + vbCrLf
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm," + vbCrLf
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)" + vbCrLf
                sSql += "          FROM lj011m ff" + vbCrLf
                sSql += "         WHERE bcno    = j.bcno" + vbCrLf
                sSql += "           AND spcflg IN ('1', '2', '3', '4')" + vbCrLf
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '" + vbCrLf
                sSql += "       ) doctorrmk," + vbCrLf
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnmd,"+ vbCrLf 
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)" + vbCrLf
                sSql += "          FROM lj011m a, lf060m b" + vbCrLf
                sSql += "         WHERE a.bcno   = j.bcno" + vbCrLf
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd" + vbCrLf
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt" + vbCrLf
                sSql += "       ) tnmd," + vbCrLf
                'sSql += "       fn_ack_get_test_nmbp_list(j.bcno) tnmbp,"
                sSql += "       (SELECT listagg(b.tnmbp,',') within group (order by b.dispseql)" + vbCrLf
                sSql += "          FROM lj011m a, lf060m b" + vbCrLf
                sSql += "         WHERE a.bcno   = j.bcno" + vbCrLf
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd  = b.spccd" + vbCrLf
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt   > j.bcprtdt" + vbCrLf
                sSql += "       ) tnmbp," + vbCrLf
                sSql += "       fn_ack_get_tgrp_nmbp_list(j.bcno) tgrpnmbp," + vbCrLf
                sSql += "       f3.spcnmd, f3.spcnmbp, j.statgbn," + vbCrLf
                sSql += "       j.bcclscd, j.iogbn, f4.tubenmbp || ' ' || f6.minspcvol tubenmbp," + vbCrLf
                sSql += "       fn_ack_get_bcno_prt(j.bcno) bcprtno," + vbCrLf
                sSql += "       CASE WHEN NVL(j.rstflg, '0') = '2' THEN '검사완료'" + vbCrLf
                sSql += "            WHEN NVL(j.rstflg, '0') = '1' THEN '검사중'" + vbCrLf
                sSql += "            ELSE CASE WHEN NVL(j.spcflg, '0') = '4' THEN '접수'" + vbCrLf
                sSql += "                      WHEN NVL(j.spcflg, '0') IN ('2', '3') THEN '채혈'" + vbCrLf
                sSql += "                      WHEN NVL(j.spcflg, '0') = '1' THEN '바코드발행'" + vbCrLf
                sSql += "                      WHEN NVL(j.spcflg, '0') = 'R' THEN 'Reject'" + vbCrLf
                sSql += "                      ELSE '미채혈'" + vbCrLf
                sSql += "                 END" + vbCrLf
                sSql += "       END status," + vbCrLf
                sSql += "       f1.colorgbn," + vbCrLf
                sSql += "       CASE WHEN f6.bccnt = 'B' THEN f6.bccnt ELSE '1' END bccnt" + vbCrLf
                sSql += "       , CASE WHEN nvl(j5.bcno, ' ') <> ' ' THEN 'R' ELSE '' END eryn " + vbCrLf  '-- JJH 자체응급 추가
                sSql += "       , f6.testcd " ' 20210429 jhs 검사코드 추가
                sSql += "  FROM lj010m j,  lj011m j1, lf060m f6," + vbCrLf
                sSql += "       lf030m f3, lf040m f4, lf010m f1" + vbCrLf
                sSql += "       , lj015m j5 " + vbCrLf  '-- JJH 자체응급 추가

                If rsBcNo <> "" Then
                    sSql += " WHERE j.bcno = :bcno" + vbCrLf
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                Else
                    sSql += " WHERE j1.colldt >= :dates" + vbCrLf
                    sSql += "   AND j1.colldt <= :datee || '235959'" + vbCrLf

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                    If rsRegNo <> "" Then
                        sSql += "   AND j.regno = :regno" + vbCrLf
                        alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    End If
                End If
                sSql += "   AND j1.owngbn <> 'H'" + vbCrLf
                sSql += "   AND NVL(j.spcflg, '0') > '0'" + vbCrLf

                If rsBcclsCd <> "" Then
                    sSql += "   AND j.bcclscd = :bcclscd" + vbCrLf
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                If rsDeptCd <> "" Then
                    If PRG_CONST.DEPT_HC.Contains(rsDeptCd) Then
                        sSql += "   AND ("
                        For ix As Integer = 0 To PRG_CONST.DEPT_HC.Count - 1
                            If ix > 0 Then sSql += " OR "
                            sSql += "a.deptcd = '" + PRG_CONST.DEPT_HC.Item(ix).ToString + "'"
                        Next
                        sSql += ")"
                    Else
                        sSql += "   AND j.deptcd = :deptcd" + vbCrLf
                        alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                    End If
                End If

                sSql += "   AND j.bcno   = j1.bcno" + vbCrLf
                If rsWardno <> "" Then
                    sSql += "   AND j.wardno  = :wardno" + vbCrLf
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardno))

                    If rsRoomNo <> "" Then
                        sSql += "   AND j.roomno  = :roomno" + vbCrLf
                        alParm.Add(New OracleParameter("roomno", OracleDbType.Varchar2, rsRoomNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRoomNo))
                    End If
                End If

                sSql += "   AND j1.tclscd  = f6.testcd" + vbCrLf
                sSql += "   AND j1.spccd   = f6.spccd" + vbCrLf
                sSql += "   AND j1.colldt >= f6.usdt" + vbCrLf
                sSql += "   AND j1.colldt <  f6.uedt" + vbCrLf
                sSql += "   AND f6.spccd   = f3.spccd" + vbCrLf
                sSql += "   AND j1.colldt >= f3.usdt" + vbCrLf
                sSql += "   AND j1.colldt <  f3.uedt" + vbCrLf
                sSql += "   AND f6.tubecd  = f4.tubecd" + vbCrLf
                sSql += "   AND j1.colldt >= f4.usdt" + vbCrLf
                sSql += "   AND j1.colldt <  f4.uedt" + vbCrLf
                sSql += "   AND f6.bcclscd = f1.bcclscd" + vbCrLf
                sSql += "   AND j1.colldt >= f1.usdt" + vbCrLf
                sSql += "   AND j1.colldt <  f1.uedt" + vbCrLf
                sSql += "   AND j.bcno     = j5.bcno(+)" + vbCrLf '-- JJH 자체응급 추가

                If USER_INFO.USRLVL = "W" Or USER_INFO.USRLVL = "O" Then
                    sSql += " ORDER BY deptward, j.regno, j.bcclscd, bcno" + vbCrLf
                Else
                    sSql += " ORDER BY bcno" + vbCrLf
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

    End Class

#Region " 접 수 : Class Reg"
    Public Class TAKE
        Private Const msFile As String = "File : CGLISAPP_J.vb, Class : LISAPP.APP_J.TAKE" + vbTab

        Private m_s_Sysdate As String
        Private msUse_Wkno_Old As String = ""    '이전 작업번호

        Public WriteOnly Property UseWknoOld() As String
            Set(ByVal Value As String)
                msUse_Wkno_Old = Value.Trim
            End Set
        End Property

        ' Stored Procedure이용하여 접수 실행
        Public Function ExecuteDo(ByVal rsBcNo As String, ByRef rsRetMsg As String) As Boolean
            Dim sFn As String = "Public Function ExecuteDo(String, String) As Boolean"

            Dim o_Parm As New DBORA.DbParrameter
            Dim sErrVal As String = ""
            Dim sRetVal As String = ""
            Dim alTmp As String()

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction
            Dim dbCmd As New OracleCommand

            dbCmd.Connection = dbCn

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                With dbCmd
                    .Transaction = dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_take"

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("rs_bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                    .Parameters.Add(New OracleParameter("rs_wknoyn", OracleDbType.Varchar2, msUse_Wkno_Old.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, msUse_Wkno_Old))
                    .Parameters.Add(New OracleParameter("rs_usrid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("rs_ip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = ""

                    .ExecuteNonQuery()

                    sRetVal = .Parameters(4).Value.ToString
                End With

                sErrVal = sRetVal.Substring(0, 2)   '에러코드
                rsRetMsg = sRetVal.Substring(2)      '작업번호 or 에러메세지
                If rsRetMsg <> "" Then alTmp = Split(rsRetMsg, ":")

                If sErrVal = "00" Then
                    '정상적으로 접수
                    dbTran.Commit()
                ElseIf sErrVal = "01" Then

                    Throw (New Exception(sRetVal))

                ElseIf sErrVal = "02" Then
                    '검사항목 조회 오류
                    Throw (New Exception(sRetVal))

                Else
                    Throw (New Exception(sRetVal))
                End If

                Return True

            Catch ex As Exception
                dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        ' Stored Procedure이용하여 접수 실행
        Public Function ExecuteDo(ByVal rsBcNo As String, ByVal rsPassId As String, ByRef rsRetMsg As String) As Boolean
            Dim sFn As String = "Public Function ExecuteDo(String, String, String) As Boolean"

            Dim oParam As New DBORA.DbParrameter
            Dim sErrVal As String = ""
            Dim sRetVal As String = ""
            Dim alTmp As String()

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction
            Dim dbCmd As New OracleCommand

            dbCmd.Connection = dbCn

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                With dbCmd
                    .Transaction = dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_takeandpass"

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("rs_bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                    .Parameters.Add(New OracleParameter("rs_wknoyn", OracleDbType.Varchar2, msUse_Wkno_Old.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, msUse_Wkno_Old))
                    .Parameters.Add(New OracleParameter("rs_passid", OracleDbType.Varchar2, rsPassId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPassId))
                    .Parameters.Add(New OracleParameter("rs_usrid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("rs_ip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = ""

                    .ExecuteNonQuery()

                    sRetVal = .Parameters(5).Value.ToString
                End With

                sErrVal = sRetVal.Substring(0, 2)   '에러코드
                rsRetMsg = sRetVal.Substring(2)      '작업번호 or 에러메세지
                If rsRetMsg <> "" Then alTmp = Split(rsRetMsg, ":")

                If sErrVal = "00" Then
                    '정상적으로 접수
                    dbTran.Commit()
                ElseIf sErrVal = "01" Then

                    Throw (New Exception(sRetVal))

                ElseIf sErrVal = "02" Then
                    '검사항목 조회 오류
                    Throw (New Exception(sRetVal))

                Else
                    Throw (New Exception(sRetVal))

                End If

                Return True

            Catch ex As Exception
                dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        '-- 검체전달
        Public Function ExecuteDo_Pass(ByVal rsBcNo As String, ByVal rsPassId As String) As Boolean

            Dim sFn As String = "Public Function ExecuteDo_Pass(String, String) As String"

            Dim alParm As New ArrayList
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction
            Dim dbCmd As New OracleCommand

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            dbCmd.Connection = dbCn

            Try

                With dbCmd
                    .Transaction = dbTran
                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO lj011h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, j.* FROM lj011m WHERE bcno = :bcno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                    iRet = .ExecuteNonQuery()

                    sSql = ""
                    sSql += "UPDATE lj011m SET passid = :usrid, passdt = fn_ack_sysdate, spcflg = '3', editid = :usrid, editip = :editip, editdt = fn_ack_sysdate"
                    sSql += " WHERE bcno = :bcno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("usrid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("usrid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("editip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                    iRet = .ExecuteNonQuery()

                End With

                If iRet > 0 Then
                    dbTran.Commit()
                    Return True
                Else
                    dbTran.Rollback()
                    Throw (New Exception("검체전달시 오류가 발생했습니다.!!"))
                End If

            Catch ex As Exception
                dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        Public Sub Init()
            m_s_Sysdate = ""
            msUse_Wkno_Old = ""
        End Sub

    End Class
#End Region

#Region " 2차 접수 : Class TAKE2"

    Public Class TAKE2
        Private msFile As String = "LISAPP.APP_J.TAKE2"

        Private msUse_Wkno_Old As String = ""    '이전 작업번호

        Public Sub Init()
            Dim sFn As String = ""

            Try
                msUse_Wkno_Old = ""
            Catch ex As Exception

            End Try
        End Sub

        Public WriteOnly Property UseWknoOld() As String
            Set(ByVal Value As String)
                msUse_Wkno_Old = Value.Trim
            End Set
        End Property

        Public Function ExecuteDo(ByVal rsBcNo As String, ByRef rsRetMsg As String) As Boolean
            Dim sFn As String = "ExecuteDo"

            Dim oParam As New DBORA.DbParrameter
            Dim sErrVal As String = ""
            Dim sRetVal As String = ""
            Dim alTmp As String()

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                With oParam
                    .AddItem("rs_bcno", OracleDbType.Varchar2, ParameterDirection.Input, rsBcNo.Length, rsBcNo)
                    .AddItem("rs_usrid", OracleDbType.Varchar2, ParameterDirection.Input, USER_INFO.USRID.Length, USER_INFO.USRID)
                    .AddItem("rs_ip", OracleDbType.Varchar2, ParameterDirection.Input, USER_INFO.LOCALIP.Length, USER_INFO.LOCALIP)
                    .AddItem("rs_wknoyn", OracleDbType.Varchar2, ParameterDirection.Input, msUse_Wkno_Old.Length, msUse_Wkno_Old)
                    .AddItem("rs_retval", OracleDbType.Varchar2, ParameterDirection.InputOutput, 2000, sRetVal)
                End With

                DbCommand()
                DbExecute("pro_ack_exe_take_part", oParam, False)

                sRetVal = oParam.Item(4).Value.ToString

                sErrVal = sRetVal.Substring(0, 2)   '에러코드
                rsRetMsg = sRetVal.Substring(2)      '작업번호 or 에러메세지
                If rsRetMsg <> "" Then alTmp = Split(rsRetMsg, ":")

                If sErrVal = "00" Then
                    '정상적으로 접수

                ElseIf sErrVal = "01" Then
                    '이미 접수된 검체번호
                    Return False

                ElseIf sErrVal = "02" Then
                    '검사항목 조회 오류
                    Throw (New Exception(rsRetMsg))

                Else
                    Throw (New Exception(rsRetMsg))
                End If

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function
    End Class
#End Region

#Region " 채혈/접수취소 : Class Cancel"
    Public Class Cancel
        Private Const msFile As String = "File : CGDA_R.vb, Class : DA_RegRst" + vbTab

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Private m_al_CancelInfo As New ArrayList
        Private m_s_CancelCd As String = ""
        Private m_s_CancelCmt As String = ""

        Private m_e_Cancel As enumCANCEL
        Private m_s_SysDate As String = ""

        Private m_b_NotApplyMTS As Boolean = False

        Public WriteOnly Property CancelTItem() As ArrayList
            Set(ByVal Value As ArrayList)
                m_al_CancelInfo = Value
            End Set
        End Property

        Public WriteOnly Property CancelCmt() As String
            Set(ByVal Value As String)
                m_s_CancelCmt = Value
            End Set
        End Property

        Public WriteOnly Property CancelCd() As String
            Set(ByVal Value As String)
                m_s_CancelCd = Value
            End Set
        End Property

        Public WriteOnly Property NotApplyMTS() As Boolean
            Set(ByVal Value As Boolean)
                m_b_NotApplyMTS = Value
            End Set
        End Property

        Public Sub New()
            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Public Sub New(ByVal r_dbCn As OracleConnection, ByVal r_dbTran As OracleTransaction)
            m_dbCn = r_dbCn
            m_dbTran = r_dbTran
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

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

        Public Function ExecuteDo(ByVal rsJobGbn As String, ByVal r_al_CancelInfo As ArrayList) As String
            Dim sFn As String = "Public Sub ExecuteDo(ByVal aeCancel As enumCANCEL, ByVal asUseId As String) As String"
            Dim dbCmd As New OracleCommand

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sBcNo As String = ""
                Dim sCmtCd As String = ""
                Dim sCmtCont As String = ""
                Dim sSvrDt As String = Format(Now, "yyyyMMddHHmmdd")

                With dbCmd
                    .Connection = m_dbCn

                    If m_dbTran IsNot Nothing Then
                        If m_dbTran.Connection IsNot Nothing Then
                            .Transaction = m_dbTran
                        End If
                    End If

                    For ix As Integer = 0 To r_al_CancelInfo.Count - 1
                        Dim stu As STU_CANCELINFO = CType(r_al_CancelInfo(ix), STU_CANCELINFO)

                        .CommandType = CommandType.StoredProcedure

                        If sBcNo <> "" And sBcNo <> stu.BCNO Then

                            .CommandText = "pro_ack_exe_cancel_spc"

                            .Parameters.Clear()
                            .Parameters.Add("rs_jobgbn", OracleDbType.Varchar2).Value = rsJobGbn
                            .Parameters.Add("rs_jobdt", OracleDbType.Varchar2).Value = sSvrDt
                            .Parameters.Add("rs_cmtcd", OracleDbType.Varchar2).Value = sCmtCd
                            .Parameters.Add("rs_cmtcont", OracleDbType.Varchar2).Value = sCmtCont
                            .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = sBcNo

                            .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            .Parameters.Add("rs_retval", OracleDbType.Varchar2)
                            .Parameters("rs_retval").Size = 2000
                            .Parameters("rs_retval").Direction = ParameterDirection.Output
                            .Parameters("rs_retval").Value = ""

                            .ExecuteNonQuery()

                            If .Parameters(7).Value.ToString <> "00" Then
                                Throw (New Exception(.Parameters(7).Value.ToString.Substring(3) + "@" + msFile + sFn))
                            End If
                        End If


                        .CommandText = "pro_ack_exe_cancel_test"

                        .Parameters.Clear()
                        .Parameters.Add("rs_jobgbn", OracleDbType.Varchar2).Value = rsJobGbn
                        .Parameters.Add("rs_jobdt", OracleDbType.Varchar2).Value = sSvrDt
                        .Parameters.Add("rs_cmtcd", OracleDbType.Varchar2).Value = stu.CANCELCD
                        .Parameters.Add("rs_cmtcont", OracleDbType.Varchar2).Value = stu.CANCELCMT
                        .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = stu.BCNO
                        .Parameters.Add("rs_tclscd", OracleDbType.Varchar2).Value = stu.TCLSCD
                        .Parameters.Add("rs_spccd", OracleDbType.Varchar2).Value = stu.SPCCD

                        .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("rs_retval", OracleDbType.Varchar2).Value = ""
                        .Parameters("rs_retval").Size = 2000
                        .Parameters("rs_retval").Direction = ParameterDirection.Output

                        .ExecuteNonQuery()

                        If .Parameters(9).Value.ToString <> "00" Then
                            Throw (New Exception(.Parameters(9).Value.ToString.Substring(3) + "@" + msFile + sFn))
                        End If

                        Dim sSql As String = "pro_ack_exe_ocs_cancel"

                        .CommandText = "pro_ack_exe_ocs_cancel"

                        .Parameters.Clear()
                        .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = stu.BCNO
                        .Parameters.Add("rs_regno", OracleDbType.Varchar2).Value = stu.REGNO
                        .Parameters.Add("rs_owngbn", OracleDbType.Varchar2).Value = stu.OWNGBN
                        .Parameters.Add("rs_fkocs", OracleDbType.Varchar2).Value = stu.FKOCS
                        .Parameters.Add("rs_cancelgbn", OracleDbType.Varchar2).Value = rsJobGbn + stu.CANCELCMT

                        .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("rs_retval", OracleDbType.Varchar2).Value = ""
                        .Parameters("rs_retval").Size = 2000
                        .Parameters("rs_retval").Direction = ParameterDirection.Output

                        .ExecuteNonQuery()

                        If .Parameters(7).Value.ToString <> "00" Then
                            Throw (New Exception(.Parameters(7).Value.ToString.Substring(2) + "@" + msFile + sFn))
                        End If

                        '-- 서비스

                        sBcNo = stu.BCNO
                        sCmtCd = stu.CANCELCD
                        sCmtCont = stu.CANCELCMT

                    Next

                    If sBcNo <> "" Then
                        .CommandText = "pro_ack_exe_cancel_spc"

                        .Parameters.Clear()
                        .Parameters.Add("rs_jobgbn", OracleDbType.Varchar2).Value = rsJobGbn
                        .Parameters.Add("rs_jobdt", OracleDbType.Varchar2).Value = sSvrDt
                        .Parameters.Add("rs_cmtcd", OracleDbType.Varchar2).Value = sCmtCd
                        .Parameters.Add("rs_cmtcont", OracleDbType.Varchar2).Value = sCmtCont
                        .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = sBcNo

                        .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("rs_retval", OracleDbType.Varchar2).Value = ""
                        .Parameters("rs_retval").Size = 2000
                        .Parameters("rs_retval").Direction = ParameterDirection.InputOutput

                        .ExecuteNonQuery()

                        If .Parameters(7).Value.ToString <> "00" Then
                            Throw (New Exception(.Parameters(7).Value.ToString.Substring(3) + "@" + msFile + sFn))
                        End If

                    End If
                End With

                m_dbTran.Commit()
                Return "00"

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        Public Function ExecuteDo(ByVal r_e_CancelGbn As enumCANCEL, ByVal rsUseId As String) As String
            Dim sFn As String = "Public Sub ExecuteDo(ByVal aeCancel As enumCANCEL, ByVal asUseId As String) As String"

            Try
                m_e_Cancel = r_e_CancelGbn
                m_s_SysDate = fnGet_Server_DateTime()
                m_s_SysDate = m_s_SysDate.Replace("-", "").Replace(":", "").Replace(" ", "")

                Dim sRet As String = ""

                If m_e_Cancel = enumCANCEL.채혈접수취소 Then
                    sRet = fnCancel_Collect("0")
                    If sRet <> "" Then Return sRet
                ElseIf m_e_Cancel = enumCANCEL.채혈취소 Then
                    sRet = fnCancel_Collect("0")
                    If sRet <> "" Then Return sRet
                ElseIf m_e_Cancel = enumCANCEL.접수취소 Then
                    sRet = fnCancel_JubSu("2")
                    If sRet <> "" Then Return sRet
                ElseIf m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                    sRet = fnCancel_Collect("R")
                    If sRet <> "" Then Return sRet
                ElseIf m_e_Cancel = enumCANCEL.BLOOD_REJECT Then
                    '혈액은행계의 헌혈자 기본검사 취소
                    sRet = fnCancel_Bank("R")
                    If sRet <> "" Then Return sRet

                ElseIf m_e_Cancel = enumCANCEL.일괄채혈취소 Then
                    sRet = fnCancel_Batch("0")
                    If sRet <> "" Then Return sRet
                End If

                Return ""

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))
                Return ex.Message
            End Try

        End Function

        Public Function ExecuteDo_UnfitSpc(ByVal r_e_CanGbn As enumCANCEL, ByVal rsUseId As String) As String
            Dim sFn As String = "Public Sub ExecuteDo_UnfitSpc(ByVal aeCancel As enumCANCEL, ByVal asUseId As String) As String"

            m_s_SysDate = fnGet_Server_DateTime()
            m_s_SysDate = m_s_SysDate.Replace("-", "").Replace(":", "").Replace(" ", "")

            Try
                Dim stuInfo As New STU_CancelInfo

                For ix As Integer = 0 To m_al_CancelInfo.Count - 1
                    stuInfo = CType(m_al_CancelInfo.Item(ix), STU_CancelInfo)

                    If fnExe_LR053M(stuInfo) < 1 Then
                        m_dbTran.Rollback()
                        Return "테이블 [lr053m]에서 오류가 발생 했습니다."
                    End If

                Next

                m_dbTran.Commit()
                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))
                Return ex.Message
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function


        Private Function fnGet_ExLab_State(ByVal rsBcNo As String, ByVal rsTclsCd As String) As Boolean

            Dim sFn As String = "Private Function fnGet_ExLab_State() As string"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDA As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql += "SELECT * FROM lre11m WHERE bcno = :bcno"

                If rsTclsCd <> "" Then
                    sSql += " AND testcd IN (SELECT testcd FROM lr010m WHERE bcno = :bcno AND tclscd = :tclscd"
                    sSql += "                 UNION "
                    sSql += "                SELECT testcd FROM lm010m WHERE bcno = :bcno AND tclscd = :tclscd"
                    sSql += "                 UNION "
                    sSql += "                SELECT testcd FROM rr010m WHERE bcno = :bcno AND tclscd = :tclscd"
                    sSql += "               )"
                End If

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                If rsTclsCd <> "" Then
                    dbCmd.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    dbCmd.Parameters.Add("tclscd", OracleDbType.Varchar2).Value = rsTclsCd
                    dbCmd.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    dbCmd.Parameters.Add("tclscd", OracleDbType.Varchar2).Value = rsTclsCd
                    dbCmd.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    dbCmd.Parameters.Add("tclscd", OracleDbType.Varchar2).Value = rsTclsCd
                End If

                dbDA = New OracleDataAdapter(dbCmd)

                dt.Reset()
                dbDA.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
                Return False
            End Try

        End Function

        Private Function fnCancel_JubSu(ByVal rsSpcFlg As String) As String

            Dim sFn As String = "Private function fnCancel_JubSu(String) as String"

            Try
                Dim stuInfo As New STU_CancelInfo
                Dim sBcclsCd As String = ""
                Dim sBcNo_Old As String = ""

                For intIdx As Integer = 0 To m_al_CancelInfo.Count - 1
                    stuInfo = CType(m_al_CancelInfo.Item(intIdx), STU_CancelInfo)

                    Dim sErrMsg As String = ""

                    Dim cos As New OcsLink.ChgOcsState

                    With cos
                        .RegNo = stuInfo.REGNO
                        .BcNo = stuInfo.BCNO
                        .OwnGbn = stuInfo.OWNGBN
                        .IOGBN = stuInfo.IOGBN
                        .TotFkOcs = stuInfo.FKOCS
                        .LabCmt = m_s_CancelCmt

                        Select Case m_e_Cancel
                            Case enumCANCEL.채혈접수취소 : .CancelGbn = "0"
                            Case enumCANCEL.채혈취소 : .CancelGbn = "1"
                            Case enumCANCEL.접수취소 : .CancelGbn = "2"
                            Case enumCANCEL.REJECT : .CancelGbn = "3"
                            Case enumCANCEL.BLOOD_REJECT : .CancelGbn = "4"
                            Case enumCANCEL.일괄채혈취소 : .CancelGbn = "5"
                        End Select
                    End With

                    sErrMsg = OCSAPP.OcsLink.Ord.SetOrderChgCancelState(cos, m_dbCn, m_dbTran)
                    If sErrMsg <> "" Then
                        m_dbTran.Rollback()
                        MsgBox(sErrMsg)
                        Return "처방 테이블에서 오류가 발생 했습니다."
                    End If

                    'sErrMsg = fnExe_MTS001(objCancelTitem, rsSpcFlag)
                    'If sErrMsg <> "" Then
                    '    m_dbTran.Rollback()
                    '    MsgBox(sErrMsg)
                    '    Return "테이블 [MTS0001]에서 오류가 발생 했습니다."
                    'End If

                    If intIdx = 0 Then sBcclsCd = stuInfo.BCCLSCD

                    If fnExe_LJ031M(stuInfo) < 1 Then ' 취소내역 삽입
                        m_dbTran.Rollback()
                        Return "취소내역 테이블[LJ031M]에서 오류가 발생 했습니다."
                    End If

                    If sBcNo_Old <> stuInfo.BCNO Then
                        Dim intRet As Integer = 0

                        If PRG_CONST.BCCLS_MicorBio.Contains(sBcclsCd) Then
                            intRet = fnExe_LM010M(stuInfo)    ' 미생물결과
                        Else
                            intRet = fnExe_LR010M(stuInfo)    ' 일반결과
                        End If

                        If intRet < 1 And stuInfo.SPCFLG = "2" Then
                            m_dbTran.Rollback()
                            Return "결과 테이블에서 오류가 발생 했습니다."
                        End If

                        If fnExe_LJ011M(stuInfo, rsSpcFlg) < 1 Then
                            m_dbTran.Rollback()
                            Return "접수내역 테이블[LJ011M]에서 오류가 발생 했습니다."
                        End If

                        If fnExe_LJ010M(stuInfo, rsSpcFlg) < 1 Then
                            m_dbTran.Rollback()
                            Return "접수마스터 테이블[LJ010M]에서 오류가 발생 했습니다."
                        End If

                        If fnExe_LJ030M(stuInfo) < 1 Then
                            m_dbTran.Rollback()
                            Return "취소마스터 테이블[LJ030M]에서 오류가 발생 했습니다."
                        End If

                        'If m_e_Cancel <> enumCANCEL.접수취소 Then
                        '    If fnExe_LB010M(stuInfo) < 1 Then
                        '        m_dbTran.Rollback()
                        '        Return "테이블 [LB010M]에서 오류가 발생 했습니다."
                        '    End If
                        'End If


                        'If fnGet_ExLab_State(objCancelTitem.BCNO, "") Then
                        '    m_dbTran.Rollback()
                        '    MsgBox("위탁의뢰된 검체는 취소할 수 없습니다.!!")
                        '    Return False
                        'End If

                        sBcNo_Old = stuInfo.BCNO
                    End If
                Next

                m_dbTran.Commit()
                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))
                Return ex.Message
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        ' 채혈 취소 or REJECT
        Private Function fnCancel_Collect(ByVal rsSpcFlg As String) As String
            Dim sFn As String = "Private function fnCancel_Collect(ByVal asSpcFlag As String) as boolean"

            Try
                Dim stuInfo As New STU_CancelInfo
                Dim sBcNo_Old As String = ""
                Dim alBcno_Idx As New ArrayList

                For ix As Integer = 0 To m_al_CancelInfo.Count - 1
                    stuInfo = CType(m_al_CancelInfo.Item(ix), STU_CancelInfo)

                    Dim iRet As Integer = 0
                    Dim sErrMsg As String = ""

                    If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                        If PRG_CONST.BCCLS_MicorBio.Contains(stuInfo.BCCLSCD) Then
                            iRet = fnExe_LM010M(stuInfo)     ' 미생물결과
                        Else
                            iRet = fnExe_LR010M(stuInfo)     ' 일반결과
                        End If

                        If iRet < 1 And stuInfo.SPCFLG = "2" Then
                            m_dbTran.Rollback()
                            Return "테이블 [LR010M]에서 오류가 발생 했습니다."
                        End If
                    End If

                    Dim cos As New OcsLink.ChgOcsState

                    With cos
                        .RegNo = stuInfo.REGNO
                        .BcNo = stuInfo.BCNO
                        .OwnGbn = stuInfo.OWNGBN
                        .IOGBN = stuInfo.IOGBN
                        .TotFkOcs = stuInfo.FKOCS
                        .LabCmt = m_s_CancelCmt

                        Select Case m_e_Cancel
                            Case enumCANCEL.채혈접수취소 : .CancelGbn = "0"
                            Case enumCANCEL.채혈취소 : .CancelGbn = "1"
                            Case enumCANCEL.접수취소 : .CancelGbn = "2"
                            Case enumCANCEL.REJECT : .CancelGbn = "3"
                            Case enumCANCEL.BLOOD_REJECT : .CancelGbn = "4"
                            Case enumCANCEL.일괄채혈취소 : .CancelGbn = "5"
                            Case enumCANCEL.부적합검등록 : .CancelGbn = "6"
                        End Select
                    End With

                    sErrMsg = OCSAPP.OcsLink.Ord.SetOrderChgCancelState(cos, m_dbCn, m_dbTran)
                    If sErrMsg <> "" Then
                        m_dbTran.Rollback()
                        Return sErrMsg
                    End If

                    If m_e_Cancel = enumCANCEL.부적합검등록 Then

                        If fnExe_LR053M(stuInfo) < 1 Then
                            m_dbTran.Rollback()
                            Return "부적합검체 등록 테이블[LR053M]에서 오류가 발생 했습니다."
                        End If

                        'If OCSAPP.OcsLink.Ord.SetOrderChgLisCmt(cos, m_dbCn, m_dbTran) < 1 Then
                        '    m_dbTran.Rollback()
                        '    Return "처방테이블에서 메세지 전달 오류가 발생 했습니다."
                        'End If
                    End If

                    If fnExe_LJ011M(stuInfo, rsSpcFlg) < 1 Then
                        m_dbTran.Rollback()
                        Return "접수내역 테이블[LJ011M]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_LJ031M(stuInfo) < 1 Then
                        m_dbTran.Rollback()
                        Return "취소내역 테이블 [LJ031M]에서 오류가 발생 했습니다."
                    End If

                    'If m_e_Cancel <> enumCANCEL.접수취소 Then
                    '    If fnExe_LB010M(stuInfo) < 1 Then
                    '        m_dbTran.Rollback()
                    '        Return "테이블 [LB010M]에서 오류가 발생 했습니다."
                    '    End If
                    'End If

                    If fnGet_ExLab_State(stuInfo.BCNO, stuInfo.TCLSCD) Then
                        m_dbTran.Rollback()
                        Return "위탁의뢰된 검체는 취소할 수 없습니다.!!"
                    End If

                    If sBcNo_Old <> stuInfo.BCNO Then
                        alBcno_Idx.Add(ix.ToString)
                        sBcNo_Old = stuInfo.BCNO
                    End If
                Next

                For ix As Integer = 0 To alBcno_Idx.Count - 1
                    stuInfo = CType(m_al_CancelInfo.Item(CInt(alBcno_Idx(ix))), STU_CancelInfo)

                    If fnExe_LJ030M(stuInfo) < 1 Then
                        m_dbTran.Rollback()
                        Return "취소 마스터 테이블 [LJ030M]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_LJ010M(stuInfo) < 1 Then
                        m_dbTran.Rollback()
                        Return "취소내역 테이블 [LJ010M]에서 오류가 발생 했습니다."
                    End If
                Next

                m_dbTran.Commit()
                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))
                Return ex.Message
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        ' 혈액은행 기본검사 취소
        Private Function fnCancel_Bank(ByVal rsSpcflg As String) As String
            Dim sFn As String = "Private function fnCancel_Bank(ByVal asSpcFlag As String) As String"

            Try
                Dim stuInfo As New STU_CancelInfo

                For ix As Integer = 0 To m_al_CancelInfo.Count - 1
                    stuInfo = CType(m_al_CancelInfo.Item(ix), STU_CancelInfo)

                    If fnExe_LJ031M(stuInfo) < 1 Then             ' 취소내역 삽입(항목별)
                        m_dbTran.Rollback()
                        Return "취소내역 테이블[LJ031M]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_LJ030M(stuInfo) < 1 Then             ' 취소내역 삽입
                        m_dbTran.Rollback()
                        Return "취소마스터 테이블[LJ030M]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_LR010M(stuInfo) < 1 Then           ' 일반결과
                        m_dbTran.Rollback()
                        Return "결과 테이블[LR010M]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_LJ011M(stuInfo, rsSpcflg) < 1 Then
                        m_dbTran.Rollback()
                        Return "접수내역 테이블[LJ011M]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_LJ010M(stuInfo, rsSpcflg) < 1 Then
                        m_dbTran.Rollback()
                        Return "접수마스터 테이블[LJ010M]에서 오류가 발생 했습니다."
                    End If

                    If fnGet_ExLab_State(stuInfo.BCNO, stuInfo.TCLSCD) Then
                        m_dbTran.Rollback()
                        Return "위탁의뢰된 검체는 취소할 수 없습니다.!!"
                    End If
                Next

                m_dbTran.Commit()
                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message, ex))
                Return ex.Message
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        Private Function fnCancel_Batch(ByVal rsSpcflg As String) As String
            Dim sFn As String = "Private Sub fnCancel_Batch(ByVal asspcflag As String) String"

            Try
                Dim stuInfo As New STU_CancelInfo

                For ix As Integer = 0 To m_al_CancelInfo.Count - 1
                    Dim sErrMsg As String = ""

                    stuInfo = CType(m_al_CancelInfo.Item(ix), STU_CancelInfo)

                    If fnExe_LJ031M(stuInfo) < 1 Then            ' 취소내역 삽입(항목별)
                        m_dbTran.Rollback()
                        Return "테이블 [LJ031M]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_LJ030M(stuInfo) < 1 Then             ' 취소내역 삽입
                        m_dbTran.Rollback()
                        Return "테이블 [LJ030M]에서 오류가 발생 했습니다."
                    End If

                    Dim ocs As New OcsLink.ChgOcsState

                    With ocs
                        .RegNo = stuInfo.REGNO
                        .BcNo = stuInfo.BCNO
                        .OwnGbn = stuInfo.OWNGBN
                        .IOGBN = stuInfo.IOGBN
                        .TotFkOcs = stuInfo.FKOCS

                        Select Case m_e_Cancel
                            Case enumCANCEL.채혈접수취소 : .CancelGbn = "0"
                            Case enumCANCEL.채혈취소 : .CancelGbn = "1"
                            Case enumCANCEL.접수취소 : .CancelGbn = "2"
                            Case enumCANCEL.REJECT : .CancelGbn = "3"
                            Case enumCANCEL.BLOOD_REJECT : .CancelGbn = "4"
                            Case enumCANCEL.일괄채혈취소 : .CancelGbn = "5"
                        End Select
                    End With

                    sErrMsg = OCSAPP.OcsLink.Ord.SetOrderChgCancelState(ocs, m_dbCn, m_dbTran)
                    If sErrMsg <> "" Then
                        m_dbTran.Rollback()
                        MsgBox(sErrMsg)
                        Return "테이블 [MTS0001]에서 오류가 발생 했습니다."
                    End If

                    'sErrMsg = fnExe_MTS001(objCancelTItem, asSpcflag)
                    'If sErrMsg <> "" Then
                    '    m_dbTran.Rollback()
                    '    MsgBox(sErrMsg)
                    '    Return "테이블 [MTS0001]에서 오류가 발생 했습니다."
                    'End If

                    If fnExe_LJ011M(stuInfo, rsSpcflg) < 1 Then
                        m_dbTran.Rollback()
                        Return "테이블 [LJ011M]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_LJ010M(stuInfo, rsSpcflg) < 1 Then
                        m_dbTran.Rollback()
                        Return "테이블 [LJ010M]에서 오류가 발생 했습니다."
                    End If

                    If fnGet_ExLab_State(stuInfo.BCNO, "") Then
                        m_dbTran.Rollback()
                        Return "위탁의뢰된 검체는 취소할 수 없습니다.!!"
                    End If

                Next

                m_dbTran.Commit()
                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))
                Return ex.Message
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        ' 취소 세부내역 삽입 ( LJ031M ) 
        Private Function fnExe_LJ031M(ByVal roData As STU_CancelInfo) As Integer
            Dim sFn As String = "Private function fnExe_LJ031M(J01.clsCancelTItem)"

            Try
                Dim dbCmd As New OracleCommand
                Dim sqlDoc As String = ""

                If m_e_Cancel = enumCANCEL.BLOOD_REJECT Then
                    sqlDoc += "INSERT INTO lj031m("
                    sqlDoc += "            canceldt, cancelgbn, bcno, tclscd, spccd, tcancelcmt) "
                    sqlDoc += "SELECT fn_ack_sysdate, :cancelgbn, :bcno, tclscd, spccd, :tcancelcmt"
                    sqlDoc += "  FROM lj011m"
                    sqlDoc += " WHERE bcno   = :bcno"
                    sqlDoc += "   AND spcflg > '0'"

                ElseIf m_e_Cancel = enumCANCEL.일괄채혈취소 Then

                    sqlDoc += "INSERT INTO lj031m("
                    sqlDoc += "            canceldt, cancelgbn, bcno, tclscd, spccd, tcancelcmt) "
                    sqlDoc += "SELECT fn_ack_sysdate, :cancelgbn, :bcno, tclscd, spccd, :tcancelcmt"
                    sqlDoc += "  FROM lj011m"
                    sqlDoc += " WHERE bcno   = :bcno"
                    sqlDoc += "   AND spcflg > '0'"

                Else

                    sqlDoc += "INSERT INTO lj031m("
                    sqlDoc += "            canceldt, cancelgbn, bcno, tclscd, spccd, tcancelcmt) "
                    sqlDoc += "    values( fn_ack_sysdate, :cancelgbn, :bcno, :tclscd, :spccd, :tcancelcmt)"
                End If

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sqlDoc

                    .Parameters.Clear()

                    .Parameters.Add("cancelgbn", OracleDbType.Varchar2).Value = CStr(m_e_Cancel)
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    If Not (m_e_Cancel = enumCANCEL.BLOOD_REJECT Or m_e_Cancel = enumCANCEL.일괄채혈취소) Then
                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = roData.TCLSCD
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = roData.SPCCD
                        .Parameters.Add("tcancelcmt", OracleDbType.Varchar2).Value = roData.CANCELCMT
                    Else
                        .Parameters.Add("tcancelcmt", OracleDbType.Varchar2).Value = roData.CANCELCMT
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO
                    End If

                    Return dbCmd.ExecuteNonQuery()

                End With

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return 0
            End Try
        End Function

        ' 일반결과 테이블 취소( LR010M )  fnExe_LR010M_new 로 새로만듬  
        Private Function fnExe_LR010M(ByVal roData As STU_CancelInfo) As Integer
            Dim sFn As String = "Private Function fnExe_LR010M(Object)"

            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""

                ' History Table로 삽입
                sSql = ""
                sSql += "INSERT INTO lr010h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.*"
                sSql += "  FROM lr010m r"
                sSql += " WHERE bcno   = :bcno"

                If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                    sSql += "   AND tclscd = :tclscd"
                    sSql += "   AND spccd  = :spccd"
                End If

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = roData.TCLSCD
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = roData.SPCCD
                    End If

                    .ExecuteNonQuery()
                End With

                ' 결과테이블 삭제
                sSql = ""
                sSql += "DELETE lr010m"
                sSql += " WHERE bcno   = :bcno"
                If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                    sSql += "   AND tclscd = :tclscd"
                    sSql += "   AND spccd  = :spccd"
                End If

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = roData.TCLSCD
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = roData.SPCCD
                    End If

                    Return .ExecuteNonQuery()
                End With

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return 0
            End Try

        End Function

        Private Function fnExe_LM010M(ByVal roData As STU_CancelInfo) As Integer  ''' 정은 추가함 
            Dim sFn As String = "Private Function fnExe_LM010M(STU_CancelInfo)"

            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""

                ' History Table로 삽입
                sSql = ""
                sSql += "INSERT INTO lm010h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.*"
                sSql += "  FROM lm010m r"
                sSql += " WHERE bcno   = :bcno"

                If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                    sSql += "   AND tclscd = :tclscd"
                    sSql += "   AND spccd  = :spccd"
                End If

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = roData.TCLSCD
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = roData.SPCCD
                    End If

                    .ExecuteNonQuery()
                End With

                ' 결과테이블 삭제
                sSql = ""
                sSql += "DELETE lm010m"
                sSql += " WHERE bcno = :bcno"
                If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                    sSql += "   AND tclscd = :tclscd"
                    sSql += "   AND spccd  = :spccd"
                End If

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    If m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = roData.TCLSCD
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = roData.SPCCD
                    End If

                    Return .ExecuteNonQuery()
                End With

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return 0
            End Try
        End Function

        ' 부적합검체 등록  
        Private Function fnExe_LR053M(ByVal roData As STU_CancelInfo) As Integer
            Dim sFn As String = "Private Function fnExe_LR010M(J01.clsCancelTItem)"

            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""

                ' History Table로 삽입
                sSql = ""
                sSql += "INSERT INTO lr053m(  regdt,  regid,  regip,  bcno,  testcd,  cmtcd,  cmtcont )"
                sSql += "            VALUES( :regdt, :regid, :regip, :bcno, :testcd, :cmtcd, :cmtcont )"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_s_SysDate
                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = roData.TCLSCD
                    .Parameters.Add("cmtcd", OracleDbType.Varchar2).Value = m_s_CancelCd
                    .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = m_s_CancelCmt

                    Return .ExecuteNonQuery()
                End With


            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return 0
            End Try

        End Function

        ' 검체채혈정보 취소( LJ011M )
        Private Function fnExe_LJ011M(ByVal roData As STU_CancelInfo, ByVal rsSpcFlg As String) As Integer
            Dim sFn As String = "Private Function fnExe_LJ011M(J01.clsCancelTItem, string) as integer"

            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""

                sSql += "INSERT INTO lj011h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, j.*"
                sSql += "  FROM lj011m j"
                sSql += " WHERE bcno = :bcno"

                If m_e_Cancel = enumCANCEL.접수취소 Then
                    sSql += "   AND NVL(rstflg, '0') = '0'"
                    sSql += "   AND spcflg IN ('1', '2', '3', '4')"
                ElseIf m_e_Cancel = enumCANCEL.채혈취소 Then
                    sSql += "   AND NVL(rstflg, '0') = '0'"
                    sSql += "   AND spcflg IN ('1', '2', '3')"
                    sSql += "   AND tclscd  = :tclscd"
                ElseIf m_e_Cancel = enumCANCEL.채혈접수취소 Then
                    sSql += "   AND NVL(rstflg, '0') = '0'"
                    sSql += "   AND spcflg IN ('1', '2', '3', '4')"
                    sSql += "   AND tclscd  = :tclscd"
                ElseIf m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                    sSql += "   AND tclscd  = :tclscd"
                End If

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    If m_e_Cancel = enumCANCEL.채혈취소 Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = roData.TCLSCD
                    End If

                    .ExecuteNonQuery()
                End With

                sSql = "UPDATE lj011M SET spcflg = :spcflg,"

                If m_e_Cancel <> enumCANCEL.접수취소 Then
                    'sSql += "       collid = NULL, colldt = NULL,"
                    'sSql += "       passid = NULL, passdt = NULL,"
                    'sSql += "       tkid   = NULL, tkdt   = NULL,"
                    sSql += "       rstflg = NULL, rstdt  = NULL,"
                ElseIf m_e_Cancel = enumCANCEL.접수취소 Then
                    'sSql += "       passid = NULL, passdt = NULL,"
                    'sSql += "       tkid   = NULL, tkdt   = NULL,"
                    sSql += "       rstflg = NULL, rstdt  = NULL,"
                End If

                sSql += "       editdt = fn_ack_sysdate,"
                sSql += "       editid = :editid,"
                sSql += "       editip = :editip"
                sSql += " WHERE bcno = :bcno"

                If m_e_Cancel = enumCANCEL.접수취소 Then
                    sSql += "   AND NVL(rstflg, '0') = '0'"
                    sSql += "   AND spcflg IN ('1', '2','3', '4')"
                ElseIf m_e_Cancel = enumCANCEL.채혈취소 Then
                    sSql += "   AND NVL(rstflg, '0') = '0'"
                    sSql += "   AND spcflg IN ('1','2', '3')"
                    sSql += "   AND tclscd = :tclscd"
                ElseIf m_e_Cancel = enumCANCEL.채혈접수취소 Then
                    sSql += "   AND NVL(rstflg, '0') = '0'"
                    sSql += "   AND spcflg IN ('1','2', '3', '4')"
                    sSql += "   AND tclscd  = :tclscd"
                ElseIf m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                    sSql += "   AND tclscd  = :tclscd"
                End If

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("spcflg", OracleDbType.Varchar2).Value = rsSpcFlg
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    If m_e_Cancel = enumCANCEL.채혈취소 Or m_e_Cancel = enumCANCEL.채혈접수취소 Or m_e_Cancel = enumCANCEL.REJECT Or m_e_Cancel = enumCANCEL.부적합검등록 Then
                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = roData.TCLSCD
                    End If

                    Return .ExecuteNonQuery()
                End With

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return 0
            End Try

        End Function

        ' 검체정보 취소( LJ010M )
        Private Function fnExe_LJ010M(ByVal roData As STU_CancelInfo, Optional ByVal rsSpcFlg As String = "") As Integer
            Dim sFn As String = "Private Function fnExe_LJ010M(J01.clsCancelTItem, [String]) As integer"

            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""

                sSql = ""
                sSql += "INSERT INTO lj010h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, j.*"
                sSql += "  FROM lj010m j"
                sSql += " WHERE j.bcno = :bcno"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += "UPDATE lj010m"
                sSql += "   SET spcflg = (SELECT MAX(CASE WHEN NVL(spcflg, '0') = 'R' THEN '0' ELSE NVL(spcflg, '0') END)"
                sSql += "                   FROM lj011m"
                sSql += "                  WHERE bcno = :bcno"
                sSql += "                ),"
                sSql += "       rstflg = fn_ack_get_rstflg_lj011m(:bcno),"
                sSql += "       editdt = fn_ack_sysdate,"
                sSql += "       editid = :editid,"
                sSql += "       editip = :editip"
                sSql += " WHERE bcno   = :bcno"

                With dbCmd
                    .Connection = m_dbCn

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    Return .ExecuteNonQuery()
                End With


            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return 0
            End Try
        End Function

        ' 취소내역 삽입 ( LJ030M ) 
        Private Function fnExe_LJ030M(ByVal roData As STU_CancelInfo) As Integer
            Dim sFn As String = "Private Sub fnExe_LJ030M(J01.clsCancelTItem) as integer"

            Try
                Dim dbCmd As New OracleCommand
                Dim sqlDoc As String = ""

                sqlDoc = ""
                sqlDoc += "INSERT INTO lj030m"
                sqlDoc += "          (  canceldt,  cancelgbn,  bcno,  cancelid,  cancelcd,  cancelcmt,  editid,  editip, editdt )"
                sqlDoc += "    VALUES( :canceldt, :cancelgbn, :bcno, :cancelid, :cancelcd, :cancelcmt, :editid, :editip, fn_ack_sysdate)"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sqlDoc

                    .Parameters.Clear()

                    .Parameters.Add("canceldt", OracleDbType.Varchar2).Value = m_s_SysDate
                    .Parameters.Add("cancelgbn", OracleDbType.Varchar2).Value = CStr(m_e_Cancel)
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO
                    .Parameters.Add("cancelid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("cancelcd", OracleDbType.Varchar2).Value = m_s_CancelCd
                    .Parameters.Add("cancelcmt", OracleDbType.Varchar2).Value = m_s_CancelCmt
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    Return .ExecuteNonQuery()
                End With

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return 0
            End Try

        End Function

        ' 헌혈사전검사 취소( LB010M )
        Private Function fnExe_LB010M(ByVal roData As STU_CancelInfo) As Integer
            Dim sFn As String = "Private Function fnLJ010M_Script(STU_CancelInfo) As integer "

            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_ocs_cancel_doner"

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("rs_bcno", roData.BCNO))
                    .Parameters.Add(New OracleParameter("rs_regno", roData.REGNO))
                    .Parameters.Add(New OracleParameter("rs_owngbn", roData.OWNGBN))

                    .Parameters.Add("rs_retval", OracleDbType.Int32)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = -1

                    .ExecuteNonQuery()

                    iRet = CType(.Parameters(3).Value.ToString, Integer)

                    'If iRet < 1 Then Return 0
                End With

                sSql = ""
                sSql += "INSERT INTO lb010h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.* FROM lb010m a"
                sSql += " WHERE regno  = :regno"
                sSql += "   AND bcno   = :bcno"
                sSql += "   AND dongbn = '4'"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = roData.REGNO
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    iRet += .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += "DELETE lb010m"
                sSql += " WHERE regno  = :regno"
                sSql += "   AND bcno   = :bcno"
                sSql += "   AND dongbn = '4'"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = roData.REGNO
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roData.BCNO

                    iRet += .ExecuteNonQuery()
                End With

                Return 1

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return 0
            End Try

        End Function


    End Class

#End Region

#Region "검체전달"
    Public Class PASS
        Private Const msFile As String = "File : CGLISAPP_J.vb, Class : LISAPP.APP_J.PASS" + vbTab

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Public Sub New()
            m_dbCn = GetDbConnection()
        End Sub

        Public Sub New(ByVal r_dbCn As OracleConnection)
            m_dbCn = r_dbCn
        End Sub

        Private Function fnGet_Server_DateTime() As String

            Dim sFn As String = "Private Function fnGet_Server_DateTime() As string"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDA As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql += "SELECT fn_ack_date_str(fn_ack_sysdate, 'yyyy-mm-dd hh24:mi:ss') srvdate FROM DUAL"

                dbCmd.Connection = m_dbCn
                'dbCmd.Transaction = m_dbTran
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
                Return Format(Now, "yyyy-MM-dd HH:mm:ss").ToString
            End Try

        End Function

        Public Function ExecuteDo(ByVal rsBcNo As String, ByVal rsPassId As String) As String
            Dim sFn As String = "Public Sub ExecuteDo(String, String) As String"

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                Dim dbCmd As New OracleCommand
                Dim dbDA As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT regno, owngbn, fkocs, bcno, fn_ack_sysdate curdt"
                sSql += "  FROM lj011m"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND spcflg = '2'"

                dbCmd.Connection = m_dbCn
                'dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDA = New OracleDataAdapter(dbCmd)

                With dbDA
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dt.Reset()
                dbDA.Fill(dt)

                If dt.Rows.Count < 1 Then Return "검체전달할 자료가 없습니다.!!"

                m_dbTran = m_dbCn.BeginTransaction()
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                sSql = ""
                sSql += "UPDATE lj010m SET spcflg = '3'"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND spcflg = '2'"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet += .ExecuteNonQuery()
                End With

                If iRet = 0 Then
                    m_dbTran.Rollback()
                    Return "테이블 [LJ010M]에서 검체전달시 오류가 발생했습니다.!!"
                End If

                sSql = ""
                sSql += "UPDATE lj011m SET spcflg = '3', passid = :passid, passdt = :passdt"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND spcflg = '2'"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("passid", OracleDbType.Varchar2).Value = rsPassId
                    .Parameters.Add("passdt", OracleDbType.Varchar2).Value = dt.Rows(0).Item("curdt").ToString
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery()
                End With

                If iRet = 0 Then
                    m_dbTran.Rollback()
                    Return "테이블 [LJ011M]에서 검체전달시 오류가 발생했습니다.!!"
                End If

                Dim sErrMsg As String = OCSAPP.OcsLink.Ord.SetPassState(dt, m_dbCn, m_dbTran)
                If sErrMsg <> "" Then
                    m_dbTran.Rollback()
                    Return "테이블 [MTS0001]에서 오류가 발생 했습니다."
                End If

                m_dbTran.Commit()

                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()

                Fn.log(msFile + sFn, Err)

                Return ex.Message
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

    End Class
#End Region


End Namespace

