'>>> CGDA_OCS
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports Common.CommFN
Imports Common.CommLogin.LOGIN
Imports Common.SVar

Namespace OcsLink

    Public Class SData
        Private Const msFile As String = "File : CGDA_OCS.vb, Class : SData@OcsLink" & vbTab

        Public Shared Function fn_GetPastTnsList(ByVal rsRegno As String, Optional ByVal rsDate As String = "", Optional ByVal rsTnsnum As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fn_GetPastTnsList(ByVal rsRegno As String, Optional ByVal rsdate As String = "", Optional ByVal rstnsnum As String = "") As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT fn_ack_get_tnsjubsuno_full(a.tnsjubsuno)  as tnsjubsuno  "
                sSql += "     , CASE WHEN a.tnsgbn = '1' THEN '준비'                 "
                sSql += "            WHEN a.tnsgbn = '2' THEN '수혈'                 "
                sSql += "            WHEN a.tnsgbn = '3' THEN '응급'                 "
                sSql += "            WHEN a.tnsgbn = '4' THEN 'Irra'                 "
                sSql += "       END                                   as tnsgbn      "
                sSql += "     , b.comnmd                              as comnm       "
                sSql += "     , a.reqqnt                                             "
                sSql += "     , a.outqnt                                             "
                sSql += "     , a.rtnqnt                                             "
                sSql += "     , a.abnqnt                                             "
                sSql += "     , a.cancelqnt                                          "
                sSql += "  FROM (SELECT a.tnsjubsuno                                 "
                sSql += "             , a.tnsgbn                                     "
                sSql += "             , a.regno                                      "
                sSql += "             , a.orddt                                      "
                sSql += "             , a.delflg                                     "
                sSql += "             , b.comcd                                      "
                sSql += "             , b.spccd                                      "
                sSql += "             , b.reqqnt                                     "
                sSql += "             , b.outqnt                                     "
                sSql += "             , b.rtnqnt                                     "
                sSql += "             , b.abnqnt                                     "
                sSql += "             , b.cancelqnt                                  "
                sSql += "             , a.jubsudt                                    "
                sSql += "          FROM lb040m a, /* 수혈접수정보 */                 "
                sSql += "               lb042m b  /* 수혈의뢰정보 */                 "
                sSql += "         WHERE a.tnsjubsuno =  b.tnsjubsuno                 "
                sSql += "           AND a.regno      =  :regno                            "

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                If rstnsnum <> "" Then
                    sSql += "           AND a.tnsjubsuno <> :tnsno "
                    alParm.Add(New OracleParameter("tnsno", OracleDbType.Varchar2, rstnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstnsnum))
                End If

                If rsDate <> "" Then
                    sSql += "       AND a.jubsudt <= :jubsudt || '235959' ) a "
                    alParm.Add(New OracleParameter("jubsudt", OracleDbType.Varchar2, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-"c, "")))
                End If

                sSql += "     , lf120m b /* 성분제제 */                              "
                sSql += " WHERE a.comcd                 = b.comcd                    "
                sSql += "   AND a.spccd                 = b.spccd                    "
                sSql += "   AND a.delflg                <> 'D'                       "
                sSql += "   AND NVL(b.ftcd, 'N') = 'N'                        "
                sSql += "ORDER BY tnsjubsuno DESC                                    "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_Remark(ByVal rsBcNo As String) As String
            Dim sFn As String = "Public Shared Function fnGet_Remark(String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT fn_ack_get_dr_remark(:bcno) remark FROM DUAL"

                Dim al As New ArrayList

                For i As Integer = 1 To 1
                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                Next

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt Is Nothing Then
                    Return ""
                Else
                    If dt.Rows.Count = 1 Then
                        Return dt.Rows(0).Item(0).ToString
                    Else
                        Return ""
                    End If
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_LisCmt(ByVal rsFkocs As String) As String
            Dim sFn As String = "Public Shared Function fnGet_LisCmt(String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT fn_ack_get_liscmt(:fkocs) remark FROM DUAL"

                Dim al As New ArrayList

                For i As Integer = 1 To 1
                    al.Add(New OracleParameter("fkocs", OracleDbType.Varchar2, rsFkocs.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkocs))
                Next

                DbCommand()

                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt Is Nothing Then
                    Return ""
                Else
                    If dt.Rows.Count = 1 Then
                        If dt.Rows(0).Item(0).ToString.Trim() = "" Then
                            Return ""
                        Else
                            Return "c " & dt.Rows(0).Item(0).ToString
                        End If
                    Else
                        Return ""
                    End If
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnget_ABO(ByVal regno As String) As String
            Dim sFn As String = "Public Shared Function fnGet_ABO(String) As String"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += " SELECT ABO || RH AS ABO "
                sSql += "   FROM LR070M "
                sSql += "  WHERE regno = :regno "


                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, regno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, regno))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return ""
                Else
                    Return "*"
                End If
               
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_PatInfo_Durg(ByVal rsRegNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, ByVal rsSlipCd As String, Optional ByVal rsIgdtCd As String = "") As DataTable
            Dim sFn As String = "Function GeneralTestSelectNew"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList


                sSql += "SELECT DISTINCT"
                sSql += "       orddate   orddt,"
                sSql += "       drugnm,   dcomnm,    drugqnt,  drugunit,"
                sSql += "       drugmeth, drugspeed, drugtime, drugcnt, drugday"
                sSql += "  FROM vw_ack_ocs_pat_drug_info"
                sSql += " WHERE patno    = :regno"
                sSql += "   AND orddate >= :dates"
                sSql += "   AND orddate <= :datee"
                sSql += " order by orddt DESC, drugnm"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsOrdDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsOrdDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtE))

                DbCommand(False)
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_OcsUsr_Info(ByVal rsUsrId As String) As String
            ' 
            Dim sFn As String = "Public Shared Function fnGet_OcsUsr_Info(String) As String"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT usrid, usrnm, 1 seq"
                sSql += "  FROM lf090m"
                sSql += " WHERE usrid = :usrid"
                sSql += "  AND NVL(delflg, '0') = '0'"
                sSql += " UNION "
                sSql += "SELECT usrid, usrnm, 0 seq"
                sSql += "  FROM vw_ack_ocs_user_info"
                sSql += " WHERE usrid = :usrid"
                sSql += "   AND startdt <= SYSDATE"
                sSql += "   AND enddt   >  SYSDATE"
                sSql += " ORDER BY seq"

                alParm.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrId))
                alParm.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrId))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count < 1 Then
                    Return ""
                Else
                    Return dt.Rows(0).Item("usrnm").ToString
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_BldPatInfo(ByVal rsRegno As String, ByVal rsOrddt As String, ByVal rsTnsNo As String) As DataTable
            ' 수혈 환자 정보 조회
            Dim sFn As String = "Public Shared Function fnGet_BldPatInfo(ByVal rsRegno As String, ByVal rsOrderDate As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                rsOrddt = rsOrddt.Replace("-", "")

                sSql += "pkg_ack_ocs.pkg_get_tns_info"

                alParm.Add(New OracleParameter("rs_regno", rsRegno))
                alParm.Add(New OracleParameter("rs_orddt", rsOrddt.Substring(0, 8)))
                alParm.Add(New OracleParameter("rs_tnsno", rsTnsNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function


        Public Shared Function fnGet_DonPatInfo(ByVal rsRegno As String, ByVal rsFkOcs As String) As DataTable
            ' 수혈 환자 정보 조회
            Dim sFn As String = "Public Shared Function fnGet_DonPatInfo(ByVal rsRegno As String, ByVal rsOrderDate As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try

                'If rsFkOcs.IndexOf("/") < 0 Then
                '    sSql += "SELECT a.bunho                                                  "
                '    sSql += "     , fn_ack_get_pat_info(a.bunho, '', '') patinfo                      "
                '    sSql += "     , CASE WHEN NVL(a.emergency, ' ') = ' ' THEN '' ELSE  '응급' END ernm "
                '    sSql += "     , fn_ack_date_str(a.order_date, 'yyyy-mm-dd') order_date   "
                '    sSql += "     , fn_ack_date_str(a.ipwon_date, 'yyyy-mm-dd') ipwon_date   "
                '    sSql += "     , fn_ack_date_str(a.opdt, 'yyyy-mm-dd') opdt               "
                '    sSql += "     , a.gwa deptcd                                             "
                '    sSql += "     , fn_ack_get_dr_name(a.doctor)  doctornm               "
                '    sSql += "     , a.ho_dong  wardno                                        "
                '    sSql += "     , a.ho_code  roomno                                        "
                '    sSql += "     , fn_ack_get_pat_diag_name(a.bunho, a.order_date) dignm           "
                '    sSql += "     , a.remark drmk                                            "
                '    sSql += "     , fn_ack_get_infection(a.bunho) infection                  "
                '    'sSql += "     , fn_ack_get_bank_remark(a.bunho) sprmk                      "
                '    sSql += "     , (SELECT SUBSTR(XMLAGG(XMLELEMENT(T, ',' || FN_ACK_DATE_STR(T.REGDT, 'YYYY-MM-DD') || ' ' || T.CMTCONT) "
                '    sSql += "                      ORDER BY T.REGDT, T.CMTCONT).EXTRACT('//text()'), 2)"
                '    sSql += "          FROM LB041M T"
                '    sSql += "         WHERE T.REGNO = A.PID"
                '    sSql += "       ) sprmk"
                '    sSql += "     , a.height                                                 "
                '    sSql += "     , a.weight                                                "
                '    sSql += "     , b.jubsudt"
                '    sSql += "  FROM mts0001_lis a, lb010m b"
                '    sSql += " WHERE a.bunho         = :regno                                      "
                '    sSql += "   AND a.fkocs         = :fkocs                                      "
                '    sSql += "   AND a.fkocs         = b.fkocs"

                '    alParm.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                '    alParm.Add(New OracleParameter("fkocs",  OracleDbType.Varchar2, rsFkOcs.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs))
                'Else
                '    sSql += "SELECT a.patno bunho                                                  "
                '    sSql += "     , fn_ack_get_pat_info(a.patno, '', '') patinfo                      "
                '    sSql += "     , CASE WHEN NVL(a.eryn, ' ') = 'Y' THEN '응급' ELSE  '' END ernm "
                '    sSql += "     , fn_ack_date_str(a.orddate,  'yyyy-mm-dd') order_date      "
                '    sSql += "     , fn_ack_date_str(a.admdate,  'yyyy-mm-dd') ipwon_date     "
                '    sSql += "     , fn_ack_date_str(a.opexdate, 'yyyy-mm-dd') opdt                                                "
                '    sSql += "     , a.deptcd                                        "
                '    sSql += "     , fn_ack_get_dr_name(a.orddr)  doctornm               "
                '    sSql += "     , a.wardno  wardno                                           "
                '    sSql += "     , a.roomno  roomno                                           "
                '    sSql += "     , fn_ack_get_pat_diag_name(a.patno, a.orddate) dignm            "
                '    sSql += "     , a.remark drmk                                           "
                '    sSql += "     , fn_ack_get_infection(a.patno) infection                   "
                '    'sSql += "     , fn_ack_get_bank_remark(a.patno) sprmk                       "
                '    sSql += "     , (SELECT SUBSTR(xmlagg(xmlelement(b41, ',' || fn_ack_date_str(b41.regdt, 'yyyy-mm-dd') || ' ' || cmtcont"
                '    sSql += "          FROM lb041m b41"
                '    sSql += "         WHERE regno = a.bunho"
                '    sSql += "       ) sprmk"
                '    sSql += "     , NULL height                                              "
                '    sSql += "     , NULL weight                                              "
                '    sSql += "     , b.jubsudt"
                '    sSql += "  FROM vw_ack_ocs_ord_info a, lb010m b             "
                '    sSql += " WHERE a.patno     = :regno                                      "
                '    sSql += "   AND a.orddate   = :orddt"
                '    sSql += "   AND a.ordseqno  = :ordno"
                '    sSql += "   AND a.instcd    = '" + PRG_CONST.SITECD + "'"
                '    sSql += "   AND a.ioflag || '/' || a.patno||'/'||a.orddate||'/'||TO_CHAR(a.ordseqno) = b.fkocs"

                '    alParm.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                '    alParm.Add(New OracleParameter("orddt",  OracleDbType.Varchar2, rsFkOcs.Split("/"c)(2).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs.Split("/"c)(2)))
                '    alParm.Add(New OracleParameter("ordno", OracleDbType.Number, rsFkOcs.Split("/"c)(3).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs.Split("/"c)(3)))

                'End If

                'DbCommand()
                'Return DbExecuteQuery(sSql, alParm)

                Return New DataTable

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_OrdHistory_LIS(ByVal rsRegNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, Optional ByVal rsPartCd As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_OrdHistory_LIS(ByVal rsNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = "pkg_ack_spc.pkg_get_pat_history_tot"
                alParm.Add(New OracleParameter("rs_regno", rsRegNo))
                alParm.Add(New OracleParameter("rs_ord1", rsOrdDtS))
                alParm.Add(New OracleParameter("rs_ord2", rsOrdDtE))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm, False)
                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_OrdHistory_RIS(ByVal rsRegNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_OrdHistory_RIS(ByVal rsNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = "pkg_ack_spc.pkg_get_pat_history_ris"
                alParm.Add(New OracleParameter("rs_regno", rsRegNo))
                alParm.Add(New OracleParameter("rs_ord1", rsOrdDtS))
                alParm.Add(New OracleParameter("rs_ord2", rsOrdDtE))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm, False)
                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_OrdHistory_TOTAL(ByVal rsRegNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_OrdHistory_TOTAL(ByVal rsNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = "pkg_ack_spc.pkg_get_pat_history_tot"
                alParm.Add(New OracleParameter("rs_regno", rsRegNo))
                alParm.Add(New OracleParameter("rs_ord1", rsOrdDtS))
                alParm.Add(New OracleParameter("rs_ord2", rsOrdDtE))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm, False)


                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_PatInfo_FGS06(ByVal rsBcNo As String, ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_PatInfo_FGS06(String, String) As DataTable"

            Dim sSql As String = ""
            Dim al As New ArrayList

            Try
                If rsBcNo <> "" Then
                    sSql = ""
                    sSql += "SELECT fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.regno,"
                    sSql += "       j.sex || '/' || j.age sexage,"
                    sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                    sSql += "       CASE WHEN j.iogbn = 'I' THEN '입원' WHEN j.iogbn = 'C' THEN '수탁' ELSE '외래' END iogbn,"
                    sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm, j.deptcd,"
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                    sSql += "       j.wardno || '/' || j.roomno wardroom"
                    sSql += "  FROM lj010m j"
                    sSql += " WHERE "

                    If rsBcNo.Length > 13 Then
                        sSql += " j.bcno like :bcno || '%'"
                    Else
                        sSql += " j.bcno = fn_ack_get_bcno_prt(:bcno)"
                    End If

                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                    sSql += " UNION "

                    sSql += "SELECT fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.regno,"
                    sSql += "       j.sex || '/' || j.age sexage,"
                    sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                    sSql += "       CASE WHEN j.iogbn = 'I' THEN '입원' WHEN j.iogbn = 'C' THEN '수탁' ELSE '외래' END iogbn,"
                    sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm, j.deptcd,"
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                    sSql += "       j.wardno || '/' || j.roomno wardroom"
                    sSql += "  FROM rj010m j"
                    sSql += " WHERE "

                    If rsBcNo.Length > 13 Then
                        sSql += " j.bcno like :bcno || '%'"
                    Else
                        sSql += " j.bcno = fn_ack_get_bcno_prt(:bcno)"
                    End If

                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                    DbCommand()
                    Return DbExecuteQuery(sSql, al)

                Else
                    sSql = "PKG_ACK_SPC.PKG_GET_PATINFO"

                    al.Add(New OracleParameter("rs_regno", rsRegNo))

                    DbCommand()
                    Return DbExecuteQuery(sSql, al, False)
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_Ord_TestList_FGS04(ByVal rsDate As String, ByVal rsRegNo As String, _
                                                        ByVal rsIoGbn As String, ByVal rsOwnGbn As String, _
                                                        Optional ByVal rsBcNo As String = "") As DataTable
            Dim sFn As String = "Function FGS04_Get_TestList_ord"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = "PKG_ACK_SPC.PKG_GET_PAT_ORDER_LIST"
                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_orddt", rsDate))
                al.Add(New OracleParameter("rs_owngbn", rsOwnGbn))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al, False)

                If rsIoGbn <> "" Then
                    If rsIoGbn = "I" Then
                        dt = Fn.ChangeToDataTable(dt.Select("iogbn IN ('I', 'D', 'E')", "orddt, testcd"))
                    Else
                        dt = Fn.ChangeToDataTable(dt.Select("iogbn NOT IN ('I', 'D', 'E')", "orddt, testcd"))
                    End If
                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try

        End Function

        ' 미채혈 조회 
        Public Shared Function fnGet_NotColl_FGS04(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIoGbn As String, _
                                                   Optional ByVal rsWard As String = "", _
                                                   Optional ByVal rsPartSlip As String = "", Optional ByVal rsTGrpCd As String = "", _
                                                   Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "Public Shared Function FGS04_Query0(string, string, string, [string], [string], [string], [string]) as datatable"

            Dim sSql As String = ""
            Dim al As New ArrayList

            Try
                If rsRegNo = "" Then
                    sSql = "PKG_ACK_SPC.PKG_GET_NOTCOLL_LIST"
                Else
                    sSql = "PKG_ACK_SPC.PKG_GET_NOTCOLL_REGNO"

                    al.Add(New OracleParameter("rs_regno", rsRegNo))
                End If

                al.Add(New OracleParameter("rs_orddt1", rsDateS))
                al.Add(New OracleParameter("rs_orddt2", rsDateE))

                Dim dt As DataTable = DbExecuteQuery(sSql, al, False)


                Dim sWhere As String = ""

                If rsIoGbn = "외래" Then
                    sWhere += "IOGBN NOT IN ('I', 'D', 'E')"
                ElseIf rsIoGbn = "입원" Then
                    sWhere += "IOGBN NOT IN ('I', 'D', 'E')"
                    '  입원일경우 병동구분
                    If rsWard <> "" Then
                        sWhere += " AND WARDNO = '" + rsWard + "'"
                    End If
                End If

                If rsTGrpCd <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "TGRPCD = '" + rsTGrpCd + "'"

                If rsPartSlip.Length = 1 Then
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "PARTCD = '" + rsPartSlip + "'"
                ElseIf rsPartSlip.Length = 1 Then
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "PARTCD = '" + rsPartSlip.Substring(0, 1) + "' AND SLIPCD = '" + rsPartSlip.Substring(1, 1) + "'"
                End If

                dt = Fn.ChangeToDataTable(dt.Select(sWhere, "orddt, regno"))

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function


        Public Shared Function fnGet_DeptDoctorList(ByVal rsDeptCd As String, ByVal rsDoctorCd As String) As DataTable
            Dim sFn As String = "fnGet_DeptDoctorList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, a.deptcd, a.deptnm, b.drcd doctorcd, b.drnm doctornm"
                sSql += "  FROM vw_ack_ocs_dept_info a, vw_ack_ocs_dr_info b"
                sSql += " WHERE b.deptcd  = a.deptcd"
                sSql += "   AND b.startdt <= fn_ack_SYSDATE"
                sSql += "   AND b.enddt   >= fn_ack_SYSDATE"

                If rsDeptCd <> "" And rsDeptCd <> "AK" Then
                    sSql += "   AND b.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If

                If rsDoctorCd <> "" Then
                    sSql += "   AND b.drcd = :drcd"
                    alParm.Add(New OracleParameter("drcd", OracleDbType.Varchar2, rsDoctorCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDoctorCd))
                End If

                'If rsDeptCd.Equals("0000000000") Then
                '    sSql += " UNION ALL "
                '    sSql += " SELECT '' chk, '0000000000' deptcd, 'test부서' deptnm, 'test' doctorcd, 'test의사' doctornm"
                '    sSql += "  from dual "
                'End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_DoctorList(ByVal rsDeptCd As String, _
                                                Optional ByVal rsDrCd As String = "", _
                                                Optional ByVal rsDrNm As String = "") As DataTable
            Dim sFn As String = "fnGet_DoctorList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       a.drcd doctorcd, a.drnm doctornm,"
                sSql += "       CASE WHEN NVL(b.fldval, ' ') = ' ' THEN a.phone ELSE b.fldval END doctortel"
                sSql += "  FROM vw_ack_ocs_dr_info a"
                sSql += "       LEFT OUTER JOIN"
                sSql += "            lf097m b ON (a.drcd = b.usrid AND b.fldgbn = '1')"

                If rsDeptCd <> "" Then
                    sSql += " WHERE deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If

                If rsDrCd <> "" Then
                    sSql += IIf(sSql.IndexOf("WHERE") < 0, " WHERE ", "   AND ").ToString + "drcd = :drcd"
                    alParm.Add(New OracleParameter("drcd", OracleDbType.Varchar2, rsDrCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDrCd))
                End If

                If rsDrNm <> "" Then
                    sSql += IIf(sSql.IndexOf("WHERE") < 0, " WHERE ", "   AND ").ToString + "drnm LIKE :drnm || '%'"
                    alParm.Add(New OracleParameter("drnm", OracleDbType.Varchar2, rsDrNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDrNm))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_RoomList(ByVal rsWardNo As String, Optional ByVal rsRoomno As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_RoomList(String) As DataTable"


            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT '' chk, wardno, roomno"
                sSql += "  FROM vw_ack_ocs_ward_info"
                If rsWardNo <> "" Then
                    sSql += " WHERE wardno = :wardno"
                    al.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardNo))

                    If rsRoomno <> "" Then
                        sSql += "   AND roomno = :roomno"
                        al.Add(New OracleParameter("roomno", OracleDbType.Varchar2, rsRoomno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRoomno))
                    End If

                End If

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_WardList(Optional ByVal rsWardno As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_WardList() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, deptcd, wardno, wardnm"
                sSql += "  FROM vw_ack_ocs_ward_info"
                sSql += " WHERE USDATE <= SYSDATE"
                sSql += "   AND UEDATE >  SYSDATE"

                If rsWardno <> "" Then
                    sSql += "   AND deptcd = :wardno"
                    al.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardno))
                End If

                sSql += " ORDER BY wardnm"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_DeptList(Optional ByVal rsDeptCd As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += " SELECT DISTINCT '' chk, deptcd, deptnm, deptnmd"
                sSql += "   FROM vw_ack_ocs_dept_info"
                If rsDeptCd <> "" Then
                    sSql += " WHERE deptcd = :deptcd"
                    al.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If
                'sSql += "  UNION ALL "
                'sSql += "  select '' chk, '0000000000' deptcd, 'test부서' deptnm ,'test' deptnmd "
                'sSql += "    from dual "
                'sSql += "  ORDER BY deptnm"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '수혈 처방 내역 조회

        'Public Shared Function fnGet_TnsOrdList(ByVal rsOrdS As String, ByVal rsOrdE As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
        '    Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

        '    Try
        '        Dim sSql As String = ""
        '        Dim al As New ArrayList

        '        sSql = ""
        '        sSql += "SELECT        a.bunho, FN_ACK_DATE_STR(a.order_date || a.order_time || '00', 'yyyy-mm-dd hh24:mi:ss') orddt, " + vbCrLf
        '        sSql += "              FN_ACK_DATE_STR(a.hope_date, 'yyyy-mm-dd') hope_date, " + vbCrLf
        '        sSql += "              a.bunho regno, " + vbCrLf
        '        sSql += "               fn_ack_get_pat_info(a.bunho, '', '') patinfo, " + vbCrLf
        '        sSql += "               fn_ack_get_dept_name(a.in_out_gubun, a.gwa) deptnm, " + vbCrLf
        '        sSql += "               fn_ack_get_dr_name(a.doctor) doctor, " + vbCrLf
        '        sSql += "               CASE WHEN a.in_out_gubun = 'I' THEN FN_ACK_GET_WARD_NAME(a.wardno) || '/' || FN_ACK_GET_ROOM_NAME(A.WARDNO, A.ROOMNO) ELSE '' END wardroom," + vbCrLf
        '        sSql += "               CASE WHEN b.comgbn = '1' THEN '준비'" + vbCrLf
        '        sSql += "                    WHEN b.comgbn = '2' AND NVL(a.eryn, 'N') = 'N' THEN '수혈' " + vbCrLf
        '        sSql += "                    WHEN b.comgbn = '2' AND NVL(a.eryn, 'N') <> 'N' THEN '교차미필' " + vbCrLf
        '        sSql += "                    WHEN b.comgbn = '4' THEN 'Irra' " + vbCrLf
        '        sSql += "               END gbn, b.comgbn ," + vbCrLf
        '        sSql += "               b.comcd, a.ordnm comnmd, a.owngbn, a.in_out_gubun iogbn," + vbCrLf
        '        sSql += "               b.spccd, a.qty," + vbCrLf
        '        '<<<20180831 수혈 출고 갯수 수정 
        '        'sSql += "               (select b42.outqnt " + vbCrLf
        '        'sSql += "                  from lb043m b43 , lb042m b42" + vbCrLf
        '        'sSql += "                 where b43.regno = a.patno " + vbCrLf
        '        'sSql += "                    and b43.fkocs = a.fkocs " + vbCrLf
        '        'sSql += "                    and b43.tnsjubsuno = b42.tnsjubsuno " + vbCrLf
        '        'sSql += "                    and b43.comcd = b42.comcd  and b42.delflg = '0' ) outqnt, " + vbCrLf
        '        sSql += "                (select  replace(count(*),'0','')  " + vbCrLf
        '        sSql += "                   from lb043m " + vbCrLf
        '        sSql += "                  where tnsjubsuno =  (select b43.tnsjubsuno  " + vbCrLf
        '        sSql += "                                         from lb043m b43  " + vbCrLf
        '        sSql += "                                        where b43.regno = a.patno " + vbCrLf
        '        sSql += "                                         and b43.fkocs = a.fkocs " + vbCrLf
        '        sSql += "                                            and rownum = 1)      " + vbCrLf
        '        sSql += "                    and comcd = b.comcd  and state = '4') outqnt , " + vbCrLf
        '        '>>>20180831
        '        sSql += "                    (select ABO||RH from lr070m where regno = a.bunho) aborh , " + vbCrLf
        '        sSql += "               case PROCSTAT when '000' then '처방' " + vbCrLf
        '        sSql += "                             when '100' then '간호확인'" + vbCrLf
        '        sSql += "                             when '500' then '접수'" + vbCrLf
        '        sSql += "                             else (select detldesc from com.zbcmcode where cdgrupid = 'M0011' and cdid = procstat )" + vbCrLf
        '        sSql += "               end state, " + vbCrLf
        '        sSql += "               FN_ACK_GET_PAT_DIAG_NAME(a.bunho, a.order_date) diagnm," + vbCrLf
        '        sSql += "               CASE WHEN a.in_out_gubun = 'I' THEN 'Y' " + vbCrLf
        '        sSql += "                    ELSE CASE WHEN NVL(a.sunab_date, ' ') = ' ' THEN 'N' ELSE 'Y' END" + vbCrLf
        '        sSql += "               END sunabyn," + vbCrLf
        '        sSql += "               CASE WHEN NVL(a.eryn,    'N') = 'N' THEN ' ' ELSE '○' END er, " + vbCrLf
        '        sSql += "               CASE WHEN NVL(a.irradyn, 'N') = 'N' THEN ' ' ELSE '○' END irryn," + vbCrLf
        '        sSql += "               CASE WHEN NVL(a.filtyn,  'N') = 'N' THEN ' ' ELSE '○' END ftyn, " + vbCrLf
        '        sSql += "               a.rmk remark, " + vbCrLf
        '        sSql += "               a.hangmog_code comordcd, " + vbCrLf
        '        sSql += "               b.comcd        comcd_out," + vbCrLf
        '        sSql += "               a.order_date ||'|'|| a.bunho  ||'|'|| a.gwa  ||'|'|| a.doctor || '|'||A.WARDNO ||'|'|| A.ROOMNO ||'|' || A.HANGMOG_CODE  treesortkey," + vbCrLf
        '        sSql += "               fn_ack_op_state (bunho) opstat" + vbCrLf
        '        sSql += "          FROM ( SELECT orddate order_date, patno bunho, deptcd gwa, orddr doctor," + vbCrLf
        '        sSql += "                       ordcd hangmog_code, spccd specimen_code, iogbn in_out_gubun, 'O' owngbn," + vbCrLf
        '        sSql += "                       WARDNO, roomno,  MIN(PROCSTAT) PROCSTAT, ordnm ,patno ," + vbCrLf '<<<20180830 상태값 sum 하면 다른게 있어서 임시로 최소값으로 표시 
        '        sSql += "                       iogbn||'/'||patno||'/'||ORDDATE||'/'||MIN(ordseqno) fkocs,    " + vbCrLf
        '        sSql += "                       COUNT(*) qty, " + vbCrLf
        '        sSql += "                       filtyn," + vbCrLf
        '        sSql += "                       irradyn," + vbCrLf
        '        sSql += "                       MIN(hopedate)  hope_date," + vbCrLf
        '        sSql += "                       MAX(rcpdate)   sunab_date," + vbCrLf
        '        sSql += "                       NVL(eryn, '')  eryn," + vbCrLf
        '        sSql += "                       ordtext        rmk, " + vbCrLf
        '        sSql += "                       MAX(ordtime)   order_time" + vbCrLf
        '        sSql += "                  FROM VW_ACK_OCS_ORD_INFO" + vbCrLf
        '        sSql += "                 WHERE INSTCD            = '031'" + vbCrLf
        '        sSql += "                   AND HOPEDATE         >= :hopedds " + vbCrLf
        '        sSql += "                   AND HOPEDATE         <= :hopedde " + vbCrLf
        '        sSql += "                   AND PRCPCLSCD          = 'B4'" + vbCrLf
        '        sSql += "                   AND HSCTTEMPPRCPFLAG   = 'N' " + vbCrLf
        '        sSql += "                   AND PRCPAUTHFLAG      <> '7'" + vbCrLf
        '        sSql += "                   AND PRCPHISTCD         = 'O'" + vbCrLf
        '        sSql += "                   AND EXECPRCPHISTCD     = 'O'" + vbCrLf
        '        sSql += "                   AND NVL(discyn,   'N') = 'N'" + vbCrLf
        '        sSql += "                 GROUP BY orddate, patno, deptcd, orddr, ordcd, spccd, iogbn, WARDNO, roomno, filtyn, irradyn," + vbCrLf
        '        sSql += "                       NVL(eryn, ''), ordtext ,ordnm ,patno              ) a,  " + vbCrLf
        '        sSql += "               lf120m b " + vbCrLf
        '        sSql += "         WHERE a.hangmog_code  = b.comordcd " + vbCrLf
        '        sSql += "           AND a.specimen_code = b.spccd" + vbCrLf
        '        sSql += "         ORDER BY orddt desc" + vbCrLf

        '        al.Add(New OracleParameter("hopedds", OracleDbType.Varchar2, rsOrdS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdS))
        '        al.Add(New OracleParameter("hopedde", OracleDbType.Varchar2, rsOrdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdE))

        '        DbCommand()
        '        Dim dt As DataTable = DbExecuteQuery(sSql, al)

        '        Dim dr As DataRow()
        '        Dim sWhare As String = ""

        '        If rsRegno <> "" Then sWhare = "bunho = '" + rsRegno + "'"
        '        If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
        '        If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

        '        If sWhare <> "" Then
        '            dr = dt.Select(sWhare, "")
        '            dt = Fn.ChangeToDataTable(dr)
        '        End If

        '        Return dt

        '    Catch ex As Exception
        '        Throw (New Exception(ex.Message + " @" + sFn, ex))
        '    End Try
        'End Function

        Public Shared Function fnGet_TnsOrdList(ByVal rsOrdS As String, ByVal rsOrdE As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT a.bunho regno,																																																																			" + vbCrLf
                sSql += "       FN_ACK_DATE_STR(a.order_date || a.order_time || '00', 'yyyy-mm-dd hh24:mi:ss') ORDDT,                                                              " + vbCrLf
                sSql += "       FN_ACK_DATE_STR(a.hope_date, 'yyyy-mm-dd') HOPE_DATE,                                                                                              " + vbCrLf
                sSql += "       FN_ACK_GET_PAT_INFO(a.bunho, '', '') PATINFO,                                                                                                      " + vbCrLf
                sSql += "       FN_ACK_GET_DEPT_NAME(a.iogbn, a.gwa) DEPTNM,                                                                                                       " + vbCrLf
                sSql += "       FN_ACK_GET_DR_NAME(a.doctor) DOCTOR,                                                                                                               " + vbCrLf
                sSql += "       CASE WHEN a.iogbn = 'I' THEN FN_ACK_GET_WARD_NAME(a.wardno) || '/' || FN_ACK_GET_ROOM_NAME(A.WARDNO, A.ROOMNO) ELSE '' END wardroom,               " + vbCrLf
                sSql += "       CASE WHEN a.comgbn = '1' THEN '준비'                                                                                                               " + vbCrLf
                sSql += "            WHEN a.comgbn = '2' AND NVL(a.eryn, 'N') = 'N' THEN '수혈'                                                                                    " + vbCrLf
                sSql += "            WHEN a.comgbn = '2' AND NVL(a.eryn, 'N') <> 'N' THEN '교차미필'                                                                               " + vbCrLf
                sSql += "            WHEN a.comgbn = '4' THEN 'Irra'                                                                                                               " + vbCrLf
                sSql += "       END gbn, a.comgbn ,                                                                                                                                " + vbCrLf
                sSql += "       a.comcd, a.ordnm comnmd, a.owngbn, a.iogbn,                                                                                                        " + vbCrLf
                sSql += "       a.spccd, a.qty,                                                                                                                                    " + vbCrLf
                sSql += "        (select  replace(count(*),'0','')                                                                                                                 " + vbCrLf
                sSql += "                  from lb043m                                                                                                                             " + vbCrLf
                sSql += "                 where tnsjubsuno = a.tnsjubsuno                                                                                                          " + vbCrLf
                sSql += "                   and comcd = a.comcd                                                                                                                    " + vbCrLf
                sSql += "                   and state = '4') outqty ,                                                                                                              " + vbCrLf
                sSql += "         (select ABO||RH from lr070m where regno = a.bunho) aborh ,                                                                                       " + vbCrLf
                sSql += "               case PROCSTAT when '000' then '처방'                                                                                                       " + vbCrLf
                sSql += "                             when '100' then '간호확인'                                                                                                   " + vbCrLf
                sSql += "                             when '500' then '접수'                                                                                                       " + vbCrLf
                sSql += "                             else (select detldesc from com.zbcmcode where cdgrupid = 'M0011' and cdid = procstat )                                       " + vbCrLf
                sSql += "               end state,                                                                                                                                 " + vbCrLf
                sSql += "               FN_ACK_GET_PAT_DIAG_NAME(a.bunho, a.order_date) diagnm,                                                                                    " + vbCrLf
                sSql += "               CASE WHEN a.iogbn = 'I' THEN 'Y'                                                                                                           " + vbCrLf
                sSql += "                    ELSE CASE WHEN NVL(a.sunab_date, ' ') = ' ' THEN 'N' ELSE 'Y' END                                                                     " + vbCrLf
                sSql += "               END sunabyn,                                                                                                                               " + vbCrLf
                sSql += "               CASE WHEN NVL(a.eryn,    'N') = 'N' THEN ' ' ELSE '○' END er,                                                                             " + vbCrLf
                sSql += "               CASE WHEN NVL(a.irradyn, 'N') = 'N' THEN ' ' ELSE '○' END irryn,                                                                          " + vbCrLf
                sSql += "               CASE WHEN NVL(a.filtyn,  'N') = 'N' THEN ' ' ELSE '○' END ftyn,                                                                           " + vbCrLf
                sSql += "               a.rmk remark,                                                                                                                              " + vbCrLf
                sSql += "               a.hangmog_code comordcd,                                                                                                                   " + vbCrLf
                sSql += "               a.order_date ||'|'|| a.bunho  ||'|'|| a.gwa  ||'|'|| a.doctor || '|'||A.WARDNO ||'|'|| A.ROOMNO ||'|' || A.HANGMOG_CODE  treesortkey,      " + vbCrLf
                sSql += "               fn_ack_op_state (bunho) opstat ,a.PREPPRCPFLAG                                                                                                            " + vbCrLf
                sSql += " from (SELECT order_date , bunho , gwa ,doctor ,hangmog_code, o.spccd , o.iogbn ,                                                                         " + vbCrLf
                sSql += "              o.owngbn , WARDNO , roomno , min(procstat) procstat , ordnm , patno ,  eryn , rmk ,                                                         " + vbCrLf
                sSql += "              o.comcd , filtyn , irradyn , o.comgbn , b.tnsjubsuno,                                                                                       " + vbCrLf
                sSql += "              min(hope_date) hope_date , max(sunab_date) sunab_date  , MAX(order_time) order_time ,   decode(o.PREPPRCPFLAG , 'Y' , o.qty ,    COUNT(*)) qty,  o.PREPPRCPFLAG                                         " + vbCrLf
                sSql += "         from (SELECT o.orddate order_date, o.patno bunho, o.deptcd gwa, o.orddr doctor  ,                                                              " + vbCrLf
                sSql += "                        o.ordcd hangmog_code, o.spccd , o.iogbn , 'O' owngbn,                                                                             " + vbCrLf
                sSql += "                        o.WARDNO, o.roomno,  o.PROCSTAT , o.ordnm ,o.patno , f.comgbn ,                                                                   " + vbCrLf
                sSql += "                        DECODE(o.iogbn,'E','I',o.iogbn)||'/'||o.patno||'/'||o.ORDDATE||'/'||o.ordseqno fkocs,                                                                     " + vbCrLf
                sSql += "                        f.comcd,                                                                                                                          " + vbCrLf
                sSql += "                        o.filtyn,                                                                                                                         " + vbCrLf
                sSql += "                        o.irradyn,                                                                                                                        " + vbCrLf
                sSql += "                        o.hopedate  hope_date,                                                                                                            " + vbCrLf
                sSql += "                        o.rcpdate   sunab_date,                                                                                                           " + vbCrLf
                sSql += "                        NVL(o.eryn, '')  eryn,                                                                                                            " + vbCrLf
                sSql += "                        o.ordtext        rmk,                                                                                                             " + vbCrLf
                sSql += "                        o.ordtime   order_time    , o.PREPPRCPFLAG  ,  fn_ack_get_reqbldqty(o.ORDDATE,o.patno, o.ordseqno, o.iogbn) qty              " + vbCrLf
                sSql += "                FROM VW_ACK_OCS_ORD_INFO o  , lf120m f                                                                                                    " + vbCrLf
                sSql += "                WHERE o.INSTCD            = '031'                                                                                                         " + vbCrLf
                sSql += "                    AND o.HOPEDATE         >= :hopedds                                                                                                  " + vbCrLf
                sSql += "                    AND o.HOPEDATE         <= :hopedde                                                                                                  " + vbCrLf
                sSql += "                    AND o.PRCPCLSCD          = 'B4'                                                                                                       " + vbCrLf
                sSql += "                    AND o.HSCTTEMPPRCPFLAG   = 'N'                                                                                                        " + vbCrLf
                sSql += "                    AND o.PRCPAUTHFLAG      <> '7'                                                                                                        " + vbCrLf
                sSql += "                    AND o.PRCPHISTCD         = 'O'                                                                                                        " + vbCrLf
                sSql += "                    AND o.EXECPRCPHISTCD     = 'O'                                                                                                        " + vbCrLf
                sSql += "                    AND NVL(o.discyn,   'N') = 'N'                                                                                                        " + vbCrLf
                sSql += "                    and o.ordcd  = f.comordcd                                                                                                             " + vbCrLf
                sSql += "                    AND o.spccd = f.spccd ) o LEFT OUTER JOIN lb043m b                                                                                    " + vbCrLf
                sSql += "                                                    on b.regno = o.patno                                                                                  " + vbCrLf
                sSql += "                                                    AND b.fkocs = o.fkocs                                                                                 " + vbCrLf
                sSql += "                                                    AND b.comcd = o.comcd                                                                                 " + vbCrLf
                sSql += "                GROUP BY o.order_date, o.bunho, o.gwa, o.doctor, o.hangmog_code, o.spccd,o.iogbn, o.owngbn, o.comgbn ,                                    " + vbCrLf
                sSql += "                        o.comcd ,o.WARDNO, o.roomno, o.filtyn, o.irradyn, o.eryn, o.rmk ,o.ordnm ,o.patno , b.tnsjubsuno ,o.PREPPRCPFLAG,o.qty ) a                              " + vbCrLf
                'sSql += "           ORDER BY a.bunho , a.order_date , a.hangmog_code  desc  " + vbCrLf
                sSql += "           ORDER BY a.bunho , a.order_date || a.order_time || '00' , a.hangmog_code  desc  " + vbCrLf

               
                al.Add(New OracleParameter("hopedds", OracleDbType.Varchar2, rsOrdS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdS))
                al.Add(New OracleParameter("hopedde", OracleDbType.Varchar2, rsOrdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdE))
            

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Dim dr As DataRow()
                Dim sWhare As String = ""

                If rsRegno <> "" Then sWhare = "regno = '" + rsRegno + "'"
                If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
                If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

                If sWhare <> "" Then
                    dr = dt.Select(sWhare, "")
                    dt = Fn.ChangeToDataTable(dr)
                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_TnsOrdList_new(ByVal rsOrdS As String, ByVal rsOrdE As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT FN_ACK_DATE_STR(a.TNSSTRDDTM, 'yyyy-mm-dd hh24:mi:ss') TNSSTRDDTM, a.bunho regno,																																																																			" + vbCrLf
                sSql += "       FN_ACK_DATE_STR(a.order_date || a.order_time || '00', 'yyyy-mm-dd hh24:mi:ss') ORDDT,                                                              " + vbCrLf
                sSql += "       FN_ACK_DATE_STR(a.hope_date, 'yyyy-mm-dd') HOPE_DATE,                                                                                              " + vbCrLf
                sSql += "       FN_ACK_GET_PAT_INFO(a.bunho, '', '') PATINFO,                                                                                                      " + vbCrLf
                sSql += "       FN_ACK_GET_DEPT_NAME(a.iogbn, a.gwa) DEPTNM,                                                                                                       " + vbCrLf
                sSql += "       FN_ACK_GET_DR_NAME(a.doctor) DOCTOR,                                                                                                               " + vbCrLf
                sSql += "       CASE WHEN a.iogbn = 'I' THEN FN_ACK_GET_WARD_NAME(a.wardno) || '/' || FN_ACK_GET_ROOM_NAME(A.WARDNO, A.ROOMNO) ELSE '' END wardroom,               " + vbCrLf
                sSql += "       CASE WHEN a.comgbn = '1' THEN '준비'                                                                                                               " + vbCrLf
                sSql += "            WHEN a.comgbn = '2' AND NVL(a.eryn, 'N') = 'N' THEN '수혈'                                                                                    " + vbCrLf
                sSql += "            WHEN a.comgbn = '2' AND NVL(a.eryn, 'N') <> 'N' THEN '교차미필'                                                                               " + vbCrLf
                sSql += "            WHEN a.comgbn = '4' THEN 'Irra'                                                                                                               " + vbCrLf
                sSql += "       END gbn, a.comgbn ,                                                                                                                                " + vbCrLf
                sSql += "       a.comcd, a.ordnm comnmd, a.owngbn, a.iogbn,                                                                                                        " + vbCrLf
                sSql += "       a.spccd, a.qty,                                                                                                                                    " + vbCrLf
                'sSql += "        (select  replace(count(*),'0','')                                                                                                                 " + vbCrLf
                'sSql += "                  from lb043m                                                                                                                             " + vbCrLf
                'sSql += "                 where tnsjubsuno = a.tnsjubsuno                                                                                                          " + vbCrLf
                'sSql += "                   and comcd = a.comcd                                                                                                                    " + vbCrLf
                'sSql += "                   and state = '4') outqty ,                                                                                                              " + vbCrLf
                sSql += "         a.cnt outqty,                                                                                                                                     " + vbCrLf
                sSql += "         (select ABO||RH from lr070m where regno = a.bunho) aborh ,                                                                                       " + vbCrLf
                sSql += "               case PROCSTAT when '000' then '처방'                                                                                                       " + vbCrLf
                sSql += "                             when '100' then '간호확인'                                                                                                   " + vbCrLf
                sSql += "                             when '500' then '접수'                                                                                                       " + vbCrLf
                sSql += "                             else (select detldesc from com.zbcmcode where cdgrupid = 'M0011' and cdid = procstat )                                       " + vbCrLf
                sSql += "               end state,                                                                                                                                 " + vbCrLf
                sSql += "               FN_ACK_GET_PAT_DIAG_NAME(a.bunho, a.order_date) diagnm,                                                                                    " + vbCrLf
                sSql += "               CASE WHEN a.iogbn = 'I' THEN 'Y'                                                                                                           " + vbCrLf
                sSql += "                    ELSE CASE WHEN NVL(a.sunab_date, ' ') = ' ' THEN 'N' ELSE 'Y' END                                                                     " + vbCrLf
                sSql += "               END sunabyn,                                                                                                                               " + vbCrLf
                sSql += "               CASE WHEN NVL(a.eryn,    'N') = 'N' THEN ' ' ELSE '○' END er,                                                                             " + vbCrLf
                sSql += "               CASE WHEN NVL(a.irradyn, 'N') = 'N' THEN ' ' ELSE '○' END irryn,                                                                          " + vbCrLf
                sSql += "               CASE WHEN NVL(a.filtyn,  'N') = 'N' THEN ' ' ELSE '○' END ftyn,                                                                           " + vbCrLf
                sSql += "               a.rmk remark,                                                                                                                              " + vbCrLf
                sSql += "               a.hangmog_code comordcd,                                                                                                                   " + vbCrLf
                sSql += "               a.order_date ||'|'|| a.bunho  ||'|'|| a.gwa  ||'|'|| a.doctor || '|'||A.WARDNO ||'|'|| A.ROOMNO ||'|' || A.HANGMOG_CODE  treesortkey,      " + vbCrLf
                sSql += "               fn_ack_op_state (bunho) opstat ,a.PREPPRCPFLAG                                                                                                            " + vbCrLf
                sSql += " from (SELECT order_date , bunho , gwa ,doctor ,hangmog_code, o.spccd , o.iogbn ,                                                                         " + vbCrLf
                sSql += "              o.owngbn , WARDNO , roomno , min(procstat) procstat , ordnm , patno ,  eryn , rmk ,                                                         " + vbCrLf
                sSql += "              o.comcd , filtyn , irradyn , o.comgbn , b.tnsjubsuno,                                                                                       " + vbCrLf
                sSql += "              min(hope_date) hope_date , max(sunab_date) sunab_date  , MAX(order_time) order_time ,   decode(o.PREPPRCPFLAG , 'Y' , o.qty ,    COUNT(*)) qty,  o.PREPPRCPFLAG                                         " + vbCrLf
                sSql += "              , c.tnsstrddtm, replace(case when b.state = '4' then count(*) else 0 end, '0','') cnt                                                         " + vbCrLf
                sSql += "         from (SELECT o.orddate order_date, o.patno bunho, o.deptcd gwa, o.orddr doctor  ,                                                              " + vbCrLf
                sSql += "                        o.ordcd hangmog_code, o.spccd , o.iogbn , 'O' owngbn,                                                                             " + vbCrLf
                sSql += "                        o.WARDNO, o.roomno,  o.PROCSTAT , o.ordnm ,o.patno , f.comgbn ,                                                                   " + vbCrLf
                sSql += "                        DECODE(o.iogbn,'E','I',o.iogbn)||'/'||o.patno||'/'||o.ORDDATE||'/'||o.ordseqno fkocs,                                             " + vbCrLf
                sSql += "                        f.comcd,                                                                                                                          " + vbCrLf
                sSql += "                        o.filtyn,                                                                                                                         " + vbCrLf
                sSql += "                        o.irradyn,                                                                                                                        " + vbCrLf
                sSql += "                        o.hopedate  hope_date,                                                                                                            " + vbCrLf
                sSql += "                        o.rcpdate   sunab_date,                                                                                                           " + vbCrLf
                sSql += "                        NVL(o.eryn, '')  eryn,                                                                                                            " + vbCrLf
                sSql += "                        o.ordtext        rmk,                                                                                                             " + vbCrLf
                sSql += "                        o.ordtime   order_time    , o.PREPPRCPFLAG  ,  fn_ack_get_reqbldqty(o.ORDDATE,o.patno, o.ordseqno, o.iogbn) qty              " + vbCrLf
                sSql += "                        , o.prcpno                                                                                                                      " + vbCrLf
                sSql += "                FROM VW_ACK_OCS_ORD_INFO o  , lf120m f                                                                                                    " + vbCrLf
                sSql += "                WHERE o.INSTCD            = '031'                                                                                                         " + vbCrLf

                If rsRegno <> "" Then
                    sSql += "                   AND o.patno         = :regno                                                                                                     " + vbCrLf
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                sSql += "                    AND o.HOPEDATE         >= :hopedds                                                                                                  " + vbCrLf
                sSql += "                    AND o.HOPEDATE         <= :hopedde                                                                                                  " + vbCrLf
                sSql += "                    AND o.PRCPCLSCD          = 'B4'                                                                                                       " + vbCrLf
                sSql += "                    AND o.HSCTTEMPPRCPFLAG   = 'N'                                                                                                        " + vbCrLf
                sSql += "                    AND o.PRCPAUTHFLAG      <> '7'                                                                                                        " + vbCrLf
                sSql += "                    AND o.PRCPHISTCD         = 'O'                                                                                                        " + vbCrLf
                sSql += "                    AND o.EXECPRCPHISTCD     = 'O'                                                                                                        " + vbCrLf
                sSql += "                    AND NVL(o.discyn,   'N') = 'N'                                                                                                        " + vbCrLf
                sSql += "                    and o.ordcd  = f.comordcd                                                                                                             " + vbCrLf
                sSql += "                    AND o.spccd = f.spccd ) o LEFT OUTER JOIN lb043m b                                                                                    " + vbCrLf
                sSql += "                                                    on b.regno = o.patno                                                                                  " + vbCrLf
                sSql += "                                                   AND b.fkocs = o.fkocs                                                                                 " + vbCrLf
                sSql += "                                                   AND b.comcd = o.comcd                                                                                 " + vbCrLf
                sSql += "                                              LEFT OUTER JOIN EMR.MNRMTNSM c                                                                            " + vbCrLf
                sSql += "                                                    on o.patno        = c.pid                                                                           " + vbCrLf
                sSql += "                                                   and o.prcpno       = c.prcpno                                                                        " + vbCrLf
                sSql += "                                                   and o.hangmog_code = c.prcpcd                                                                        " + vbCrLf
                sSql += "                                                   and c.histstat     = 'O'                                                                             " + vbCrLf
                sSql += "                GROUP BY o.order_date, o.bunho, o.gwa, o.doctor, o.hangmog_code, o.spccd,o.iogbn, o.owngbn, o.comgbn ,                                    " + vbCrLf
                sSql += "                        o.comcd ,o.WARDNO, o.roomno, o.filtyn, o.irradyn, o.eryn, o.rmk ,o.ordnm ,o.patno , b.tnsjubsuno ,o.PREPPRCPFLAG, o.qty, c.TNSSTRDDTM, b.state ) a                              " + vbCrLf
                sSql += "           ORDER BY a.bunho , a.order_date || a.order_time , a.hangmog_code  desc, PREPPRCPFLAG DESC, a.TNSSTRDDTM " + vbCrLf


                al.Add(New OracleParameter("hopedds", OracleDbType.Varchar2, rsOrdS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdS))
                al.Add(New OracleParameter("hopedde", OracleDbType.Varchar2, rsOrdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdE))


                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Dim dr As DataRow()
                Dim sWhare As String = ""

                'If rsRegno <> "" Then sWhare = "regno = '" + rsRegno + "'"
                If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
                If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

                If sWhare <> "" Then
                    dr = dt.Select(sWhare, "")
                    dt = Fn.ChangeToDataTable(dr)
                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_TnsOrdList_new_new(ByVal rsOrdS As String, ByVal rsOrdE As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += " SELECT   FN_ACK_DATE_STR(to_char(c.FSTRGSTDT, 'yyyymmddhh24miss'), 'yyyy-mm-dd hh24:mi:ss') fstrgstdt, " + vbCrLf
                sSql += "          FN_ACK_DATE_STR(o.orddate || MAX(o.ordtime), 'yyyy-mm-dd hh24:mi:ss') order_date," + vbCrLf
                sSql += "          o.patno bunho," + vbCrLf
                sSql += "          FN_ACK_GET_PAT_INFO(o.patno, '', '') PATINFO," + vbCrLf
                sSql += "          FN_ACK_GET_DEPT_NAME(o.iogbn, o.deptcd) DEPTNM," + vbCrLf
                sSql += "          FN_ACK_GET_DR_NAME(o.orddr) DOCTOR," + vbCrLf
                sSql += "          CASE WHEN o.iogbn  = 'I' THEN FN_ACK_GET_WARD_NAME(o.wardno) || '/' || FN_ACK_GET_ROOM_NAME(o.WARDNO, o.ROOMNO) ELSE '' END wardroom," + vbCrLf
                sSql += "          CASE WHEN f.comgbn = '1' THEN '준비'                                                                                                            " + vbCrLf
                sSql += "               WHEN f.comgbn = '2' AND NVL(o.eryn, 'N') = 'N' THEN '수혈'                                                                                " + vbCrLf
                sSql += "               WHEN f.comgbn = '2' AND NVL(o.eryn, 'N') <> 'N' THEN '교차미필'                                                                             " + vbCrLf
                sSql += "               WHEN f.comgbn = '4' THEN 'Irra'                                                                                                         " + vbCrLf
                sSql += "          END gbn, CASE WHEN f.comgbn = '2' AND NVL(o.eryn, 'N') <> 'N' THEN '3' ELSE f.comgbn END comgbn," + vbCrLf
                sSql += "          (SELECT ABO||RH FROM lr070m WHERE regno = o.patno) aborh,             " + vbCrLf
                sSql += "          o.ordnm, /*COUNT (*) qty, */                                               " + vbCrLf
                sSql += "          CASE WHEN o.prepprcpflag = 'Y' then fn_ack_get_reqbldqty(o.ORDDATE,o.patno, max(o.ordseqno), o.iogbn) else to_char(COUNT(*)) end qty, "
                sSql += "          (SELECT cdnm FROM com.zbcmcode                                        " + vbCrLf
                sSql += "            WHERE CDGRUPID = 'M0011'                                            " + vbCrLf
                sSql += "              AND cdid = o.PROCSTAT                                             " + vbCrLf
                sSql += "          ) state,                                                              " + vbCrLf
                sSql += "          CASE WHEN NVL(o.eryn,    'N') = 'N' THEN ' ' ELSE '○' END er,        " + vbCrLf
                sSql += "          o.ordcd hangmog_code,                                                 " + vbCrLf
                sSql += "          o.spccd specimen_code,                                                " + vbCrLf
                sSql += "          o.iogbn in_out_gubun,                                                 " + vbCrLf
                sSql += "          o.filtyn,                                                             " + vbCrLf
                sSql += "          o.irradyn,                                                            " + vbCrLf
                sSql += "          o.ordtext rmk,                                                        " + vbCrLf
                sSql += "          o.PREPPRCPFLAG                                                        " + vbCrLf
                sSql += "   FROM   VW_ACK_OCS_ORD_INFO o LEFT OUTER JOIN EMR.MNRMDEEX c                  " + vbCrLf
                sSql += "                                             ON c.prcpdd         = o.orddate    " + vbCrLf
                sSql += "                                            AND c.instcd         = '031'        " + vbCrLf
                sSql += "                                            AND c.prcphistno     = o.prcphistno " + vbCrLf
                sSql += "                                            AND c.execprcpuniqno = o.ordseqno   " + vbCrLf
                sSql += "                                            AND c.prcpno         = o.prcpno     " + vbCrLf
                sSql += "          ,lf120m f                         " + vbCrLf
                sSql += "  WHERE   o.INSTCD    = '031' " + vbCrLf '--기관코드
                sSql += "    AND   o.HOPEDATE >= :rsOrdS" + vbCrLf
                sSql += "    AND   o.HOPEDATE <= :rsOrdE" + vbCrLf

                al.Add(New OracleParameter("rsOrdS", OracleDbType.Varchar2, rsOrdS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdS))
                al.Add(New OracleParameter("rsOrdE", OracleDbType.Varchar2, rsOrdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdE))

                If rsRegno <> "" Then
                    sSql += "                   AND o.patno         = :regno                                                                                                     " + vbCrLf
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                sSql += "    AND   o.PRCPCLSCD          = 'B4'                " + vbCrLf '--수혈
                'sSql += "    AND   o.TEMPPRCPFLAG       = 'N'                 " + vbCrLf '--임시처방구분
                sSql += "    AND   o.HSCTTEMPPRCPFLAG   = 'N'                 " + vbCrLf '-- HSCT 임시처방구분
                sSql += "    AND   o.PRCPAUTHFLAG      <> '7'                 " + vbCrLf
                sSql += "    AND   o.PRCPHISTCD         = 'O'                 " + vbCrLf '--처방종류 (처방)
                sSql += "    AND   o.EXECPRCPHISTCD     = 'O'                 " + vbCrLf '--실시처방에 대한 변경이력 (처방)
                sSql += "    AND   NVL (o.discyn, 'N')  = 'N'                 " + vbCrLf '-- dc여부
                'sSql += "    AND   o.PREPPRCPFLAG       = 'N'                 " + vbCrLf '-- PREP 구분
                sSql += "    AND   o.ordcd              = f.comordcd          " + vbCrLf
                sSql += "    AND   o.spccd              = f.spccd             " + vbCrLf
                sSql += "  GROUP BY c.FSTRGSTDT,o.orddate, o.patno, o.deptcd, o.orddr, o.ordcd, o.spccd, o.iogbn, o.WARDNO, o.roomno, o.filtyn, o.irradyn, NVL (o.eryn, ''), o.ordtext, o.ordnm, f.comgbn, o.eryn, o.PROCSTAT, o.PREPPRCPFLAG" + vbCrLf
                sSql += "  ORDER BY order_date, bunho,   hangmog_code DESC, specimen_code , PREPPRCPFLAG desc , fstrgstdt" + vbCrLf

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Dim dr As DataRow()
                Dim sWhare As String = ""

                'If rsRegno <> "" Then sWhare = "regno = '" + rsRegno + "'"
                If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
                If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

                If sWhare <> "" Then
                    dr = dt.Select(sWhare, "")
                    dt = Fn.ChangeToDataTable(dr)
                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_TnsOrdListNew(ByVal rsOrdS As String, ByVal rsOrdE As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += " SELECT rgstdt, FSTRGSTDT, ORDER_DATE, BUNHO, PATINFO, DEPTNM, DOCTOR, WARDROOM, GBN, COMGBN, ABORH, ORDNM, comnmd,     " + vbCrLf
                sSql += "        CASE WHEN (PREPPRCPFLAG = 'Y' AND QTY <> '1') THEN QTY ELSE TO_CHAR(COUNT(*)) END QTY, " + vbCrLf
                sSql += "        STATE, ER, HANGMOG_CODE, IN_OUT_GUBUN, FILTYN, IRRADYN, RMK, PREPPRCPFLAG, comcd " + vbCrLf
                sSql += "   FROM (SELECT FN_ACK_DATE_STR(o.rgstdt, 'yyyy-mm-dd hh24:mi:ss') rgstdt,  "
                sSql += "                FN_ACK_DATE_STR(TO_CHAR(C.FSTRGSTDT, 'yyyymmddhh24miss'), 'yyyy-mm-dd hh24:mi:ss') FSTRGSTDT, " + vbCrLf
                sSql += "                FN_ACK_DATE_STR(O.ORDDATE || O.ORDTIME, 'yyyy-mm-dd hh24:mi:ss') ORDER_DATE," + vbCrLf
                sSql += "                O.PATNO BUNHO," + vbCrLf
                sSql += "                FN_ACK_GET_PAT_INFO(o.patno, '', '') PATINFO, " + vbCrLf
                sSql += "                FN_ACK_GET_DEPT_NAME (o.iogbn, o.deptcd) DEPTNM, " + vbCrLf
                sSql += "                FN_ACK_GET_DR_NAME (o.orddr) DOCTOR, " + vbCrLf
                sSql += "                CASE WHEN o.iogbn = 'I' THEN FN_ACK_GET_WARD_NAME(o.wardno) || '-' || FN_ACK_GET_ROOM_NAME(o.wardno, o.roomno)" + vbCrLf
                sSql += "                     ELSE '' END wardroom, " + vbCrLf
                sSql += "                CASE WHEN f.comgbn = '1' THEN '준비'" + vbCrLf
                sSql += "                     WHEN f.comgbn = '2' AND nvl(o.eryn, 'N') = 'N' THEN '수혈'" + vbCrLf
                sSql += "                     WHEN f.comgbn = '2' AND nvl(o.eryn, 'N') <> 'N' THEN '교차미필' " + vbCrLf
                sSql += "                     WHEN f.comgbn = '4' THEN 'Irra'" + vbCrLf
                sSql += "                     END gbn," + vbCrLf
                sSql += "                CASE WHEN f.comgbn = '2' AND nvl(o.eryn, 'N') <> 'N' THEN '3'" + vbCrLf
                sSql += "                     ELSE f.comgbn END comgbn," + vbCrLf
                sSql += "                (SELECT ABO || RH " + vbCrLf
                sSql += "                   FROM lr070m " + vbCrLf
                sSql += "                  WHERE regno = o.patno) aborh, o.ordnm, " + vbCrLf
                sSql += "                CASE WHEN o.prepprcpflag = 'Y' THEN fn_ack_get_reqbldqty (o.ORDDATE, o.patno, o.ordseqno, o.iogbn)                          " + vbCrLf
                sSql += "                     ELSE TO_CHAR(COUNT(*)) END qty,                                                                                        " + vbCrLf
                sSql += "                (SELECT cdnm FROM com.zbcmcode WHERE CDGRUPID = 'M0011' AND cdid = o.PROCSTAT) state,                                       " + vbCrLf
                sSql += "                CASE WHEN nvl(o.eryn, 'N') = 'N' THEN ' ' ELSE '○' END er,                                                                 " + vbCrLf
                sSql += "                o.ordcd hangmog_code, o.spccd specimen_code, o.iogbn in_out_gubun, o.filtyn, o.irradyn, o.ordtext rmk, o.prepprcpflag,      " + vbCrLf
                sSql += "                f.comcd, f.comnmd" + vbCrLf
                sSql += "           FROM VW_ACK_OCS_ORD_INFO o LEFT OUTER JOIN EMR.MNRMDEEX c                                                                        " + vbCrLf
                sSql += "                                                   ON c.prcpdd = o.orddate                                                                  " + vbCrLf
                sSql += "                                                  AND c.instcd = '031'                                                                      " + vbCrLf
                sSql += "                                                  /*AND c.prcphistno = o.prcphistno*/                                                        " + vbCrLf
                sSql += "                                                  AND c.execprcpuniqno = o.ordseqno                                                         " + vbCrLf
                sSql += "                                                  AND c.prcpno = o.prcpno                                                                   " + vbCrLf
                sSql += "                , lf120m f" + vbCrLf
                sSql += "          WHERE o.instcd = '031'           " + vbCrLf
                sSql += "            AND o.hopedate >= :rsOrdS      " + vbCrLf
                sSql += "            AND o.hopedate <= :rsOrdE      " + vbCrLf

                al.Add(New OracleParameter("rsOrdS", OracleDbType.Varchar2, rsOrdS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdS))
                al.Add(New OracleParameter("rsOrdE", OracleDbType.Varchar2, rsOrdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdE))

                sSql += "            AND o.prcpclscd = 'B4'         " + vbCrLf
                sSql += "            AND o.hscttempprcpflag = 'N'   " + vbCrLf
                sSql += "            AND o.prcpauthflag <> '7'      " + vbCrLf
                sSql += "            AND o.prcphistcd = 'O'         " + vbCrLf
                sSql += "            AND o.execprcphistcd = 'O'     " + vbCrLf
                sSql += "            AND nvl(o.discyn, 'N') = 'N'   " + vbCrLf
                sSql += "            AND o.ordcd = f.comordcd       " + vbCrLf
                sSql += "            AND o.spccd = f.spccd          " + vbCrLf

                If rsRegno <> "" Then
                    sSql += "                   AND o.patno         = :regno                                                                                                     " + vbCrLf
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                sSql += "       GROUP BY C.FSTRGSTDT, O.ORDDATE, O.ORDTIME, O.PATNO, O.DEPTCD, O.ORDDR, O.ORDCD, O.SPCCD, O.IOGBN, O.WARDNO, O.ROOMNO, O.FILTYN, O.IRRADYN, " + vbCrLf
                sSql += "                NVL(O.ERYN, ''), O.ORDTEXT, O.ORDNM, F.COMGBN, O.ERYN, O.PROCSTAT, O.PREPPRCPFLAG, O.ORDSEQNO, f.comcd, f.comnmd, o.rgstdt " + vbCrLf
                sSql += "         )" + vbCrLf
                sSql += "     GROUP BY FSTRGSTDT, ORDER_DATE, BUNHO, PATINFO, DEPTNM, DOCTOR, WARDROOM, GBN, COMGBN, ABORH, ORDNM, " + vbCrLf
                sSql += "              STATE, ER, HANGMOG_CODE, IN_OUT_GUBUN, FILTYN, IRRADYN, RMK, PREPPRCPFLAG, QTY, comcd, comnmd, rgstdt " + vbCrLf
                sSql += "     ORDER BY ORDER_DATE, BUNHO, HANGMOG_CODE DESC, PREPPRCPFLAG DESC,  FSTRGSTDT " + vbCrLf
                
                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Dim dr As DataRow()
                Dim sWhare As String = ""

                If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
                If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

                If sWhare <> "" Then
                    dr = dt.Select(sWhare, "")
                    dt = Fn.ChangeToDataTable(dr)
                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        ' 수술 확정 조회
        Public Shared Function fnGet_OpInfo_List(ByVal rsOpdt As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_OpInfo_List(String) As DataTable"

            Try
                Dim dbCn As oracleConnection = GetDbConnection()
                Dim dbCmd As New oracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                With dbCmd
                    .Connection = dbCn
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pkg_ack_ocs.pkg_get_op_list"

                End With

                dbDa = New OracleDataAdapter(dbCmd)

                dbDa.SelectCommand.Parameters.Clear()
                dbDa.SelectCommand.Parameters.Add("rs_opdt1", OracleDbType.Varchar2).Value = rsOpdt
                dbDa.SelectCommand.Parameters.Add("rs_opdt2", OracleDbType.Varchar2).Value = rsOpdt
                dbDa.SelectCommand.Parameters.Add("io_cursor", OracleDbType.RefCursor).Value = ""
                dbDa.SelectCommand.Parameters("io_cursor").Direction = ParameterDirection.Output

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function
    End Class

    Public Class Pat
        Private Const msFile As String = "File : CGDA_OCS.vb, Class : Pat@OcsLink" & vbTab

        '< add freety 2007/04/24 : 환자 현재 정보 조회
        Public Shared Function fnGet_PatInfo_Current(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Function fnGet_PatInfo_Current"

            Try
                Dim sSql As String = ""

                sSql += "pkg_ack_coll.pkg_get_patinfo_current"

                DbCommand()

                Dim al As New ArrayList

                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_qryid", USER_INFO.USRID))
                al.Add(New OracleParameter("rs_qryip", USER_INFO.LOCALIP))

                Dim dt As DataTable = DbExecuteQuery(sSql, al, False)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_Patinfo(ByVal rsRegNo As String, ByVal rsPatNm As String) As DataTable
            Dim sFn As String = "fnGet_DoctorListfnGet_DeptDoctorList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT "
                sSql += "       bunho, suname, sex, birth,"
                sSql += "       sujumin1 || '-' || sujumin2 idno_full, sujumin1 || '-' || SUBSTR(sujumin2, 1, 1) || '******' idno,"
                sSql += "       tel1, tel2, address1, seq"
                sSql += "  FROM (SELECT 0 seq, patno bunho, patnm suname, sex, FN_ACK_DATE_STR(birtdate, 'yyyy-mm-dd') birth,"
                sSql += "               resno1 sujumin1, resno2 sujumin2,"
                sSql += "               telno1 tel1, telno2 tel2, address1"
                sSql += "          FROM vw_ack_ocs_pat_info"
                sSql += "         WHERE instcd = '" + PRG_CONST.SITECD + "'"
                If rsRegNo <> "" Then
                    sSql += "           AND patno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                ElseIf rsPatNm <> "" Then
                    sSql += "           AND patnm LIKE :patnm || '%'"
                    alParm.Add(New OracleParameter("patnm", OracleDbType.Varchar2, rsPatNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPatNm))
                End If
                sSql += "         UNION"
                sSql += "        SELECT seq, bunho, suname, sex, fn_ack_date_str(birth, 'yyyy-mm-dd'), sujumin1, sujumin2, tel1, tel2, address1"
                sSql += "          FROM mts0002_lis"
                If rsRegNo <> "" Then
                    sSql += "         WHERE bunho = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                ElseIf rsPatNm <> "" Then
                    sSql += "         WHERE suname LIKE :patnm || '%'"
                    alParm.Add(New OracleParameter("patnm", OracleDbType.Varchar2, rsPatNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPatNm))
                End If
                sSql += "       ) p"
                sSql += " ORDER BY seq"


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function


        Public Shared Function fnGet_PatInfo_ByIDNO(ByVal rsIdNoL As String, ByVal rsIdNoR As String) As DataTable
            Dim sFn As String = "Function fnGet_PatInfo_ByIDNO"
            Dim sSql As String = ""
            Dim sWhere As String = ""
            Dim dt As New DataTable

            Dim al As New ArrayList

            Try
                sSql = ""
                sSql += "SELECT DISTINCT bunho, suname, sex, age, sujumin, address1"
                sSql += "  FROM ("
                sSql += "        SELECT patno bunho, patnm suname, sex,"
                sSql += "               MONTHS_BETWEEN(SYSDATE, birtdate) / 12  age,"
                sSql += "               resno1 || '-' || resno2 sujumin, address1, fn_ack_get_pat_wardroom(patno) wardroom"
                sSql += "          FROM vw_ack_ocs_pat_info"
                sSql += "         WHERE instcd = '" + PRG_CONST.SITECD + "'"
                If rsIdNoL <> "" Then
                    sWhere = "           AND resno1 = :idonl"
                    al.Add(New OracleParameter("idnol", OracleDbType.Varchar2, rsIdNoL.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIdNoL))
                End If

                If rsIdNoR <> "" Then
                    sWhere += "           AND resno2 = :rsidnor"
                    al.Add(New OracleParameter("idnor", OracleDbType.Varchar2, rsIdNoR.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIdNoR))
                End If
                sSql += sWhere

                sSql += "         UNION "
                sSql += "        SELECT bunho, suname, sex,"
                sSql += "               MONTHS_BETWEEN(SYSDATE, TO_DATE(birth, 'yyyymmdd')) / 12 age,"
                sSql += "               sujumin1 || '-' || sujumin2 sujumin, address1, fn_ack_get_pat_wardroom(bunho) wardroom "
                sSql += "          FROM mts0002_lis"

                sWhere = ""
                If rsIdNoL <> "" Then
                    sWhere = "         WHERE sujumin1 = :idnol"
                    al.Add(New OracleParameter("idnol", OracleDbType.Varchar2, rsIdNoL.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIdNoL))
                End If

                If rsIdNoR <> "" Then
                    sWhere += IIf(sWhere = "", " WHERE ", " AND ").ToString + " sujumin2 = :idnor"
                    al.Add(New OracleParameter("idnor", OracleDbType.Varchar2, rsIdNoR.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIdNoR))
                End If
                sSql += sWhere
                sSql += "       ) p"


                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try
        End Function

        Public Shared Function fnGet_PatInfo_byNm(ByVal rsNm As String, Optional ByVal rsIdNoL As String = "", Optional ByVal rsIdNoR As String = "") As DataTable
            Dim sFn As String = "Function fnGet_PatInfo_byNm"
            Dim sSql As String = ""
            Dim al As New ArrayList
            Dim sWhere As String = ""

            Try
                If rsNm <> "" Then
                    sSql = "pkg_ack_coll.pkg_get_patinfo_byNm"
                    al.Add(New OracleParameter("rs_patnm", rsNm))

                    If rsIdNoL <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "idnol = '" + rsIdNoL + "'"
                    If rsIdNoR <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "idnor = '" + rsIdNoR + "'"
                Else
                    sSql = "pkg_ack_coll.pkg_get_patinfo_byId"
                    al.Add(New OracleParameter("rs_idnol", rsIdNoL))

                End If

                If rsIdNoR <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "idnor = '" + rsIdNoR + "'"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al, False)
                If sWhere <> "" Then dt = Fn.ChangeToDataTable(dt.Select(sWhere))

                Return dt



            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try
        End Function

        Public Shared Function fnGet_DiagNm(ByVal rsRegNo As String, ByVal rsOrdDayB As String, ByVal rsOrdDayE As String, ByVal rsOwnGbn As String) As String
            Dim sFn As String = "Public Shared Function fnGet_DiagNm(String, String, String, String) As String"

            Dim sSql As String = ""

            Dim sDiagNm As String = ""
            Dim sDiagNmE As String = ""

            Try
                Dim al As New ArrayList

                sSql += "SELECT *"
                sSql += "  FROM (SELECT DIAGNM_HAN KORNAME,"
                sSql += "               DIAGNM_ENG ENGNAME,"
                sSql += "               FN_ACK_DATE_STR(MEDDATE, 'YYYY-MM-DD') ORDER_DATE,"
                sSql += "               '1' sort_1,"
                sSql += "               '1' sort_2"
                sSql += "          FROM VW_ACK_OCS_PAT_DIAG_INFO"
                sSql += "         WHERE INSTCD   = '" + PRG_CONST.SITECD + "'"
                sSql += "           AND PATNO    = :regno"
                sSql += "           AND MEDDATE <= :orddt"
                sSql += "           and MAINDIAG = 'Y' "
                sSql += "         ORDER BY MEDDATE DESC"
                sSql += "       ) a"
                sSql += " WHERE ROWNUM < 2"
                sSql += " ORDER BY sort_1, sort_2, order_date desc"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrdDayE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDayE.Replace("-", "")))

                DbCommand()

                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Dim sReturn As String = ""
                Dim sDiag_K As String = ""
                Dim sDiag_E As String = ""

                If dt.Rows.Count > 0 Then
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        If ix > 0 Then
                            sDiag_K += vbCrLf
                            sDiag_E += vbCrLf
                        End If

                        sDiag_K += dt.Rows(0).Item(0).ToString.Trim
                        sDiag_E += dt.Rows(0).Item(1).ToString.Trim
                    Next
                Else
                    sReturn = "" + Convert.ToChar(124) + ""
                End If

                Return sDiag_K + Convert.ToChar(124) + sDiag_E

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try
        End Function

        Public Shared Function fnGet_Diag_Info(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Diag_Info(String) As DataTable"

            Try
                Dim al As New ArrayList
                Dim sSql As String = ""

                sSql += "SELECT DIAGNM_HAN KORNAME,"
                sSql += "       DIAGNM_ENG ENGNAME,"
                sSql += "       FN_ACK_DATE_STR(meddate, 'yyyy-mm-dd') orddt"
                sSql += "  FROM VW_ACK_OCS_PAT_DIAG_INFO"
                sSql += " WHERE INSTCD   = '" + PRG_CONST.SITECD + "'"
                sSql += "   AND PATNO    = :regno"
                sSql += " ORDER BY orddt DESC"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '2020-03-11 JJH 백혈병 진단명
        Public Shared Function fnGet_Diag_Leukemia() As DataTable
            Dim sFn As String = "Function fnGet_Diag_Leukemia() As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable

            Try
                sSql = ""
                sSql += " SELECT DIAGENGNM AS DIAG_ENG, " '영문
                sSql += "        DIAGHNGNM AS DIAG_HNG  " '한글
                sSql += "   FROM EMR.MMBVDIAG         "
                'sSql += "  WHERE DIAGCD IN ('C9100.000.00','C9100.001.00','C9100.002.00','C9100.003.00','C9100.003.01', "
                'sSql += "                   'C9100.003.02','C9100.003.03','C9101.000.00','C9101.000.01','C9101.001.00', "
                'sSql += "                   'C9101.002.00','C9102.000.00','C9108.000.00','C9108.001.00','C9108.002.00', "
                'sSql += "                   'C9108.002.01','C9108.003.00','C9108.004.00','C9108.004.01','C9108.004.02', "
                'sSql += "                   'C9108.005.00','C9108.005.01','C9108.006.00','C9108.006.01','C9108.007.00', "
                'sSql += "                   'C9108.007.01','C9108.008.00','C9108.008.01','C9108.009.00','C9108.010.00', "
                'sSql += "                   'C9108.011.00','C9108.012.00','C9108.013.00','C9108.014.00','C9108.015.00', "
                'sSql += "                   'C9108.016.00','C9108.017.00'                                               "
                'sSql += "                  ) "
                'sSql += "    AND TERMTODD = '99991231' "

                sSql += "  WHERE ICD10CD IN ( "
                sSql += "                   'C9100', 'C9101', 'C9102', 'C9108', 'C911', 'C913', 'C914', 'C915', 'C916', 'C917',"
                sSql += "                   'C918', 'C919', 'C9200', 'C9201', 'C9208', 'C921', 'C922', 'C923', 'C924', 'C925',"
                sSql += "                   'C926', 'C927', 'C928', 'C929', 'C930', 'C931', 'C933', 'C937', 'C939', 'C940',"
                sSql += "                   'C942', 'C943', 'C946', 'C947', 'C950', 'C951', 'C957', 'C959', 'C820', 'C821',"
                sSql += "                   'C822', 'C823', 'C824', 'C825', 'C826', 'C827', 'C829', 'C830', 'C831', 'C8330',"
                sSql += "                   'C8331', 'C8338', 'C835', 'C837', 'C838', 'C839', 'C840', 'C841', 'C844', 'C845',"
                sSql += "                   'C846', 'C847', 'C848', 'C849', 'C851', 'C852', 'C857', 'C859', 'C860', 'C861',"
                sSql += "                   'C862', 'C863', 'C864', 'C865', 'C866', 'D474', 'D471', 'C944', 'D460', 'D461',"
                sSql += "                   'D462', 'D464', 'D465', 'D466', 'D467', 'D469'"
                sSql += "                   )"
                sSql += "    AND TERMTODD = '99991231'"

                DbCommand()
                dt = DbExecuteQuery(sSql)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try
        End Function

        Public Shared Function fnGet_Diag_Leukemia_Chk(ByVal rsRegno As String) As String
            Dim sFn As String = "Public Shared Function fnGet_Diag_Leukemia_Chk() As String"

            Try

                Dim sSql As String = ""
                Dim dt As DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += " SELECT "
                sSql += "        CASE WHEN COUNT(*) > 0 THEN 'Y' ELSE 'N' END YN"
                sSql += "   FROM LJ010M A, LJ013M B,"
                sSql += "        ( SELECT DIAGENGNM AS DIAG_ENG, "
                sSql += "                 DIAGHNGNM AS DIAG_HNG  "
                sSql += "            FROM EMR.MMBVDIAG "
                sSql += "           WHERE ICD10CD IN ( "
                sSql += "                 'C9100', 'C9101', 'C9102', 'C9108', 'C911', 'C913', 'C914', 'C915', 'C916', 'C917',"
                sSql += "                 'C918', 'C919', 'C9200', 'C9201', 'C9208', 'C921', 'C922', 'C923', 'C924', 'C925',"
                sSql += "                 'C926', 'C927', 'C928', 'C929', 'C930', 'C931', 'C933', 'C937', 'C939', 'C940',"
                sSql += "                 'C942', 'C943', 'C946', 'C947', 'C950', 'C951', 'C957', 'C959', 'C820', 'C821',"
                sSql += "                 'C822', 'C823', 'C824', 'C825', 'C826', 'C827', 'C829', 'C830', 'C831', 'C8330',"
                sSql += "                 'C8331', 'C8338', 'C835', 'C837', 'C838', 'C839', 'C840', 'C841', 'C844', 'C845',"
                sSql += "                 'C846', 'C847', 'C848', 'C849', 'C851', 'C852', 'C857', 'C859', 'C860', 'C861',"
                sSql += "                 'C862', 'C863', 'C864', 'C865', 'C866', 'D474', 'D471', 'C944', 'D460', 'D461',"
                sSql += "                 'D462', 'D464', 'D465', 'D466', 'D467', 'D469'"
                sSql += "                             )       "
                sSql += "             AND TERMTODD = '99991231'"
                sSql += "        ) C    "
                sSql += " WHERE A.REGNO      = :regno"
                sSql += "   AND A.BCNO       = B.BCNO"
                sSql += "   AND B.DIAGNM_ENG = C.DIAG_ENG"
                sSql += "   AND B.DIAGNM     = C.DIAG_HNG"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("YN").ToString
                Else
                    Return "N"
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function


        Public Shared Function fnGet_AntiDurg_Info(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_AntiDurg_Info(ByVal rsRegNo As String) As DataTable"

            Try
                Dim al As New ArrayList
                Dim sSql As String = ""

                sSql += "SELECT PID, "
                sSql += "       prcpdd,"
                sSql += "       PRCPNM,"
                sSql += "       COMDESC,"
                sSql += "       HOSINHOSOUTFLAGNM,"
                sSql += "       PRCPCLSCD,"
                sSql += "       PRCPQTY,"
                sSql += "       PRCPDAYNO,"
                sSql += "       UNIT,"
                sSql += "       GRUPNM"
                sSql += "       , to_char(to_date(prcpdd ,'yyyymmdd')+  prcpdayno , 'yyyy-mm-dd') endprcp"
                sSql += "  FROM EMR.VW_ANTICO_ORDER_INFO "
                sSql += " WHERE PID    = :regno"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '< yjlee 2009-02-11
        ' 환자 특이사항 조회
        Public Shared Function fnGet_Pat_SpCmt(ByVal rsRegNo As String, ByVal rsIOGBN As String) As String
            Dim sFn As String = "Public Shared Function fnGet_Pat_SpCmt(String, String) As String"

            Dim sSql As String = ""

            Dim sDrugNm As String = ""

            Try
                Dim al As New ArrayList

                sSql += "SELECT remark FROM lj040m"
                sSql += "  WHERE regno = :regno"
                sSql += "    AND iogbn = :iogbn "

                DbCommand()

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIOGBN.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIOGBN))

                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Dim sReturn As String = ""

                If dt.Rows.Count > 0 Then
                    sReturn = dt.Rows(0).Item(0).ToString
                Else
                    sReturn = ""
                End If

                Return sReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        '> yjlee 2009-02-11

        '-- 감염여부
        Public Shared Function fnGet_Pat_Infection(ByVal rsRegNo As String, ByVal rbPrtNm As Boolean) As String
            Dim sFn As String = "Public Shared Function fnGet_Pat_Infection(String, Boolean) As strubg"

            Dim sSql As String = ""
            Dim sReturn As String = ""

            Try
                sSql = ""
                If rbPrtNm Then
                    sSql += "SELECT fn_ack_get_infection_prt(:regno) infinfo FROM DUAL"

                Else
                    sSql += "SELECT fn_ack_get_infection(:regno) infinfo FROM DUAL"
                End If

                Dim al As New ArrayList

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt Is Nothing Then Return sReturn
                If dt.Rows.Count > 0 Then
                    sReturn = dt.Rows(0).Item("infinfo").ToString.Replace(",", "/")
                End If

                Return sReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 혈액형결과
        Public Shared Function fnGet_Pat_AboRh(ByVal rsRegNo As String) As String
            Dim sFn As String = "Public Shared Function fnGet_Pat_AboRh(String, Boolean) As strubg"

            Dim sSql As String = ""
            Dim sReturn As String = ""

            Try
                sSql = ""
                sSql += "SELECT abo || rh aborh FROM lr070m WHERE regno = :regno"

                Dim al As New ArrayList

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt Is Nothing Then Return sReturn
                If dt.Rows.Count > 0 Then
                    sReturn = dt.Rows(0).Item("aborh").ToString.Replace(",", "/")
                End If

                Return sReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 뇌졸증 여부
        Public Shared Function fnGet_Pat_Type(ByVal rsRegNo As String, ByVal rsOrdDt As String) As String
            Dim sFn As String = "Public Shared Function fnGet_Pat_Type(String) As strubg"

            Dim sSql As String = ""
            Dim sReturn As String = ""

            rsOrdDt = rsOrdDt.Replace("-", "").Replace(":", "").Replace(" ", "").Substring(0, 8)

            Try
                sSql = ""
                sSql += "SELECT fn_ack_get_pat_erinfo(:regno, :orddt) patinfo FROM DUAL"

                Dim al As New ArrayList

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt Is Nothing Then Return sReturn
                If dt.Rows.Count > 0 Then
                    sReturn = dt.Rows(0).Item("patinfo").ToString
                End If

                Return sReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

    End Class

    Public Class Ord
        Private Const msFile As String = "CGDA_OCS.vb, Class : Ord@OcsLink" & vbTab

        Protected Shared m_dbCn As OracleConnection
        Protected Shared m_dbTran As OracleTransaction

        '-- 주치의
        Public Shared Function fnGet_GenDr_Name(ByVal rsBcNo As String, ByVal rsRegNo As String) As String
            Dim sFn As String = "Public Shared Function fnGet_GenDr_name(String) As strubg"

            Dim sSql As String = ""
            Dim sDrNm_1 As String = "" '-- 주치의
            Dim sDrNm_2 As String = "" '-- 담당의

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                If rsBcNo.Substring(8, 2) = "V1" Then '<<< 20160621 종합검증 주치의 관련 수정 
                    sSql += "SELECT fn_ack_get_ocs_gendr_bcno_gv(:bcno) drnm FROM DUAL"
                Else
                    sSql += "SELECT fn_ack_get_ocs_gendr_bcno(:bcno) drnm FROM DUAL"
                End If


                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then sDrNm_1 = dt.Rows(0).Item("drnm").ToString

                Return sDrNm_1

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        '-- 예약일자
        Public Shared Function fnGet_Ord_ResdtInfo_fkocs(ByVal rsFkocs As String) As String
            Dim sFn As String = "Public Shared Function fnGet_Ord_ResdtInfo_fkocs(String) As strubg"

            Dim sSql As String = ""
            Dim sReturn As String = ""

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
                sSql = "SELECT fn_ack_get_ocs_resdt_fkocs(:fkocs) infinfo FROM DUAL"

                Dim al As New ArrayList

                al.Add(New OracleParameter("fkocs", OracleDbType.Varchar2, rsFkocs.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkocs))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt Is Nothing Then Return sReturn
                If dt.Rows.Count > 0 Then
                    sReturn = dt.Rows(0).Item("infinfo").ToString.Replace(",", "/")
                End If

                Return sReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        '-- 예약일자
        Public Shared Function fnGet_Ord_ResdtInfo_bcno(ByVal rsBcNo As String) As String
            Dim sFn As String = "Public Shared Function fnGet_Ord_ResdtInfo_bcno(String) As strubg"

            Dim sSql As String = ""
            Dim sReturn As String = ""

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
                sSql = "SELECT fn_ack_get_ocs_resdt_bcno(:bcno) infinfo FROM DUAL"

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt Is Nothing Then Return sReturn
                If dt.Rows.Count > 0 Then
                    sReturn = dt.Rows(0).Item("infinfo").ToString.Replace(",", "/")
                End If

                Return sReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Public Shared Function fnGet_Coll_PatList(ByVal r_stu As STU_COLLINFO) As DataTable
            Dim sFn As String = "fnGet_Coll_PatList(Object) As  As DataTable"

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sSql As String = ""
                Dim alPram As New ArrayList

                If r_stu.REGNO <> "" Then
                    sSql += "pkg_ack_coll.pkg_get_patlist_regno"

                    alPram.Add(New OracleParameter("rs_regno", r_stu.REGNO))
                Else
                    sSql += "pkg_ack_coll.pkg_get_patlist"
                End If

                alPram.Add(New OracleParameter("rs_ord1", r_stu.ORDDT1.Replace("-", "")))
                alPram.Add(New OracleParameter("rs_ord2", r_stu.ORDDT2.Replace("-", "")))
                alPram.Add(New OracleParameter("rs_spcflg1", r_stu.SPCFLG1))
                alPram.Add(New OracleParameter("rs_spcflg2", r_stu.SPCFLG2))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alPram, False)

                Dim sWhere As String = ""

                If r_stu.PARTGBN <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "partgbn = '" + r_stu.PARTGBN + "'"

                If r_stu.WARDCD <> "" Then
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "wardno = '" + r_stu.WARDCD + "'"
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

                dt = Fn.ChangeToDataTable(dt.Select(sWhere, "patinfo"))

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try


        End Function

        Public Shared Function fnGet_Coll_PatList_poct(ByVal rsOrdDtS As String, ByVal rsOrdDtE As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Coll_PatList_poct(String, String) As String"

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
                Dim sSql As String = ""
                Dim alPram As New ArrayList

                sSql += "pkg_ack_coll.pkg_get_patlist_fkocs"

                alPram.Add(New OracleParameter("rs_ord1 ", rsOrdDtS.Replace("-", "")))
                alPram.Add(New OracleParameter("rs_ord2 ", rsOrdDtE.Replace("-", "")))

                DbCommand()
                Return DbExecuteQuery(sSql, alPram, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Public Shared Function fnGet_Coll_PatList_bcno(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Coll_PatList_poct(String, String) As String"

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sSql As String = ""
                Dim alPram As New ArrayList

                sSql += "pkg_ack_coll.pkg_get_patlist_bcno"

                alPram.Add(New OracleParameter("rs_bcno ", rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alPram, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Public Shared Function fnGet_Coll_PatList_RegNo(ByVal rsRegNo As String, _
                                                        ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                                        ByVal rsSpcFlgS As String, ByVal rsSpcFlgE As String, _
                                                        ByVal rsPatGbn As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Coll_PatList_RegNo(String, String, String, String, String) As String"

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sSql As String = ""
                Dim alPram As New ArrayList

                If rsPatGbn = "" Then
                    sSql += "pkg_ack_coll.pkg_get_patlist_regno"

                Else
                    sSql += "pkg_ack_coll.pkg_get_patlist_regno_part"
                    alPram.Add(New OracleParameter("rs_partgbn", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPatGbn))
                End If

                alPram.Add(New OracleParameter("rs_regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alPram.Add(New OracleParameter("rs_ord1", OracleDbType.Varchar2, rsOrdDtS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtS.Replace("-", "")))
                alPram.Add(New OracleParameter("rs_ord2", OracleDbType.Varchar2, rsOrdDtE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtE.Replace("-", "")))
                alPram.Add(New OracleParameter("rs_spcflg1", OracleDbType.Varchar2, rsSpcFlgS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcFlgS))
                alPram.Add(New OracleParameter("rs_spcflg2", OracleDbType.Varchar2, rsSpcFlgE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcFlgE))

                DbCommand()
                Return DbExecuteQuery(sSql, alPram, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Public Shared Function fnGet_Coll_Order(ByVal r_stu As STU_COLLINFO, ByVal rbQryMode As Boolean, ByVal rbHopeday As Boolean) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Coll_Order(Object) As String"

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
                Dim sSql As String = ""
                Dim alPram As New ArrayList

                If r_stu.REGNO = "" Then
                    sSql += "pkg_ack_coll.pkg_get_order_batch"
                Else
                    sSql += "pkg_ack_coll.pkg_get_order_regno"

                    alPram.Add(New OracleParameter("rs_regno", r_stu.REGNO))
                End If

                alPram.Add(New OracleParameter("rs_orddt1", r_stu.ORDDT1))
                alPram.Add(New OracleParameter("rs_orddt2", r_stu.ORDDT2))
                alPram.Add(New OracleParameter("rs_spcflg1", r_stu.SPCFLG1))
                alPram.Add(New OracleParameter("rs_spcflg2", r_stu.SPCFLG2))


                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alPram, False)

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
                                sSort = "ordday desc, deptcd, doctorcd, patinfo, regno, exlabcd, bcclscd, spccd, tubecd, poctyn, bconeyn, seqtmi, sortslip, sortl, testcd"
                            End If
                        End If
                    End If
                End If

                Dim sWhere As String = ""

                If r_stu.IOGBN = "O" Then
                    sWhere += "IOGBN NOT IN ('I', 'E')"
                Else
                    sWhere += "IOGBN IN ('I', 'E')"
                End If

                If r_stu.PARTGBN <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "partgbn = '" + r_stu.PARTGBN + "'"
                If r_stu.WARDCD <> "" Then
                    sWhere += IIf(sWhere = "", "", " AND ").ToString + "wardno = '" + r_stu.WARDCD + "'"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function


        Public Shared Function fnSet_PatInfo(ByVal r_dr As DataRow, ByVal rdtSysDate As Date) As STU_PatInfo
            Dim sFn As String = "Public Function fnSet_PatInfo(DataRow, date) As stu_PatInfo"

            Dim cpi As New STU_PatInfo

            Try
                Dim sBuf() As String = r_dr.Item("patinfo").ToString.Split(Chr(124))

                If sBuf(2).Trim = "" Then
                    sBuf(2) = Format(Now, "yyyy-MM-dd").ToString
                End If

                '< 나이계산
                Dim dtBirthDay As Date = CDate(sBuf(2).Trim)
                Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, rdtSysDate), Integer)

                If Format(dtBirthDay, "MMdd").ToString > Format(rdtSysDate, "MMdd").ToString Then iAge -= 1
                '>

                cpi.ROOMNO = r_dr.Item("roomno").ToString
                cpi.REGNO = r_dr.Item("regno").ToString
                cpi.PATNM = sBuf(0).Trim
                cpi.SEX = sBuf(1).Trim
                cpi.AGE = iAge.ToString
                cpi.IDNOL = sBuf(6).Trim
                cpi.IDNOR = sBuf(7).Trim
                cpi.BIRTHDAY = IIf(sBuf(2).Trim.Length = 10, sBuf(2), Fn.Format_Day8ToDay10(sBuf(2).Trim)).ToString
                cpi.IDNO = cpi.IDNOL + "-" + cpi.IDNOR '.Substring(0, 1) + "******"
                cpi.TEL1 = sBuf(4).Trim
                cpi.TEL2 = sBuf(5).Trim
                cpi.WARD = r_dr.Item("wardno").ToString
                cpi.DEPTCD = r_dr.Item("deptcd").ToString
                cpi.DEPTNM = r_dr.Item("deptnm").ToString
                cpi.DOCTORCD = r_dr.Item("doctor").ToString
                cpi.DOCTORNM = r_dr.Item("doctornm").ToString
                cpi.ENTDT = r_dr.Item("ibday").ToString
                cpi.OWNGBN = r_dr.Item("owngbn").ToString

                '< yjlee 2009-02-24
                cpi.GUBUN = sBuf(9).ToString()        '환자유형
                cpi.SOGAE = sBuf(10).ToString()       '직원관계
                cpi.VIP = sBuf(11).ToString()         'VIP관계
                '> yjlee 2009-02-24

                Return cpi

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function SetOrderChgCollState(ByVal r_css As ChgOcsState, ByVal rsToColl As Boolean) As Integer
            Dim sFn As String = "Public Shared Function SetOrderChgCollState(ChgCollState) As Integer"

            Dim dbCmd As New OracleCommand
            Dim iRows As Integer = 0
            Dim sSql As String = ""
            Dim alFkOcs As New ArrayList

            Try
                Dim sFkOcs() As String = r_css.TotFkOcs.Split(","c)

                For ix As Integer = 0 To sFkOcs.Length - 1

                    If alFkOcs.Contains(sFkOcs(ix)) = False Then
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

                            .Parameters.Add(New OracleParameter("rs_regno", r_css.RegNo))
                            .Parameters.Add(New OracleParameter("rs_owngbn", r_css.OwnGbn))
                            .Parameters.Add(New OracleParameter("rs_fkocs", sFkOcs(ix)))
                            .Parameters.Add(New OracleParameter("rs_bcno", r_css.BcNo))

                            If rsToColl Then
                                .Parameters.Add(New OracleParameter("rs_spcflg", "2"))
                                .Parameters.Add(New OracleParameter("rs_acptdt", r_css.CollDt))
                            Else
                                .Parameters.Add(New OracleParameter("rs_spcflg", "1"))
                                .Parameters.Add(New OracleParameter("rs_acptdt", r_css.CollDt))
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

                        End With

                        alFkOcs.Add(sFkOcs(ix))
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

        Public Shared Function SetOrderChgCollState(ByVal r_css As ChgOcsState, ByVal rbToColl As Boolean, ByVal r_lisDbCn As OracleConnection, ByVal r_lisDbTran As OracleTransaction) As Integer
            Dim sFn As String = "Public Shared Function SetOrderChgCollState(ChgCollState, LisDbConnection, LisDbTransaction) As Integer"

            m_dbCn = r_lisDbCn
            m_dbTran = r_lisDbTran

            Return SetOrderChgCollState(r_css, rbToColl)
        End Function

        Public Shared Function SetOrderChgLisCmt(ByVal r_css As ChgOcsState) As Integer
            Dim sFn As String = "Public Shared Function SetOrderChgLabCmt(ChgCollState) As Integer"

            Dim dbCmd As New OracleCommand
            Dim iRows As Integer = 0
            Dim sSql As String = ""

            Try

                sSql = "pro_ack_exe_ocs_liscmt"

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

                    .Parameters.Add(New OracleParameter("rs_regno", r_css.RegNo))
                    .Parameters.Add(New OracleParameter("rs_owngbn", r_css.OwnGbn))
                    .Parameters.Add(New OracleParameter("rs_fkocs", r_css.TotFkOcs))
                    .Parameters.Add(New OracleParameter("rs_liscmt", r_css.LabCmt))
                    .Parameters.Add(New OracleParameter("rs_usrid", USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("rs_ip", USER_INFO.LOCALIP))

                    .Parameters.Add("ri_retval", OracleDbType.Int32)
                    .Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("ri_retval").Value = -1

                    .ExecuteNonQuery()

                    iRows = CType(.Parameters(6).Value.ToString, Integer)

                End With

                Return iRows

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose()
                    dbCmd = Nothing
                End If

            End Try
        End Function

        Public Shared Function SetOrderChgLisCmt(ByVal r_css As ChgOcsState, ByVal r_lisDbCn As OracleConnection, ByVal r_lisDbTran As OracleTransaction) As Integer
            Dim sFn As String = "Public Shared Function SetOrderChgLabCmt(String, String, LisDbConnection, LisDbTransaction) As Integer"

            m_dbCn = r_lisDbCn
            m_dbTran = r_lisDbTran

            Return SetOrderChgLisCmt(r_css)
        End Function

        Private Shared Function SetOrderChgCancelState(ByVal r_css As ChgOcsState) As String
            Dim sFn As String = "Public Shared Function SetOrderChgCancelState(ChgCollState) As string"

            Dim dbCmd = New OracleCommand
            Dim iRows As Integer = 0
            Dim sSql As String = ""

            Try
                Dim sFkOcs() As String = r_css.TotFkOcs.Split(","c)

                For ix As Integer = 0 To sFkOcs.Length - 1
                    sSql = "pro_ack_exe_ocs_cancel"

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

                        .Parameters.Add(New OracleParameter("rs_bcno", r_css.BcNo))
                        .Parameters.Add(New OracleParameter("rs_regno", r_css.RegNo))
                        .Parameters.Add(New OracleParameter("rs_owngbn", r_css.OwnGbn))
                        .Parameters.Add(New OracleParameter("rs_fkocs", sFkOcs(ix)))
                        .Parameters.Add(New OracleParameter("rs_canclegbn", r_css.CancelGbn + r_css.LabCmt))
                        .Parameters.Add(New OracleParameter("rs_usrid", USER_INFO.USRID))
                        .Parameters.Add(New OracleParameter("rs_ip", USER_INFO.LOCALIP))

                        .Parameters.Add("rs_retval", OracleDbType.Varchar2)
                        .Parameters("rs_retval").Size = 2000
                        .Parameters("rs_retval").Direction = ParameterDirection.Output
                        .Parameters("rs_retval").Value = ""

                        .ExecuteNonQuery()

                        Dim sRet As String = .Parameters(7).Value.ToString
                        If sRet.StartsWith("00") Then
                            iRows += 1
                        Else
                            Throw (New Exception(sRet.Substring(2)))
                        End If
                    End With
                Next

                If iRows > 0 Then
                    Return ""
                Else
                    Return "처방상태 변경에 오류가 발생했습니다."
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose()
                    dbCmd = Nothing
                End If

            End Try
        End Function

        Public Shared Function SetOrderChgCancelState(ByVal r_css As ChgOcsState, ByVal r_dbCn As OracleConnection, ByVal r_dbTran As OracleTransaction) As String
            Dim sFn As String = "Public Shared Function SetOrderChgCaneclState(String, String, LisDbConnection, LisDbTransaction) As string"

            m_dbCn = r_dbCn
            m_dbTran = r_dbTran

            Return SetOrderChgCancelState(r_css)
        End Function


        Private Shared Function SetPassState(ByVal r_dt As DataTable) As String
            Dim sFn As String = "Public Shared Function SetPassState(DataTable) As String"

            Dim dbCmd As New OracleCommand
            Dim iRows As Integer = 0
            Dim sSql As String = ""
            Dim sErrMsg As String = ""

            Try

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    sSql = "pro_ack_exe_ocs_pass"

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
                        .Parameters.Add(New OracleParameter("rs_regno", r_dt.Rows(ix).Item("regno").ToString))
                        .Parameters.Add(New OracleParameter("rs_owngbn", r_dt.Rows(ix).Item("owngbn").ToString))
                        .Parameters.Add(New OracleParameter("rs_fkocs", r_dt.Rows(ix).Item("fkocs").ToString))

                        .Parameters.Add(New OracleParameter("rs_acptdt", r_dt.Rows(ix).Item("curdt").ToString))

                        .Parameters.Add(New OracleParameter("rs_usrid", USER_INFO.USRID))
                        .Parameters.Add(New OracleParameter("rs_ip", USER_INFO.LOCALIP))

                        .Parameters.Add("ri_retval", OracleDbType.Int32)
                        .Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                        .Parameters("ri_retval").Value = -1

                        .Parameters.Add("rs_retval", OracleDbType.Varchar2)
                        .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                        .Parameters("rs_retval").Value = ""

                        .ExecuteNonQuery()

                        Dim sRet As String = .Parameters(6).Value.ToString
                        Dim iRet As Integer = CType(.Parameters(6).Value.ToString, Integer)

                        iRows = CType(.Parameters(6).Value.ToString, Integer)

                        If iRows < 1 Then Return sErrMsg

                    End With
                Next

                If iRows > 0 Then
                    Return ""
                Else
                    Return "처방상태 변경에 오류가 발생했습니다."
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            Finally
                If dbCmd IsNot Nothing Then
                    dbCmd.Dispose() : dbCmd = Nothing
                End If

            End Try
        End Function

        Public Shared Function SetPassState(ByVal r_dt As DataTable, ByVal r_lisDbCn As OracleConnection, ByVal r_lisDbTran As OracleTransaction) As String
            Dim sFn As String = "Public Shared Function SetPassState(DataTable, LisDbConnection, LisDbTransaction) As string"

            m_dbCn = r_lisDbCn
            m_dbTran = r_lisDbTran

            Return SetPassState(r_dt)
        End Function

    End Class


    Public Class ChgOcsState
        Public RegNo As String = ""
        Public BcNo As String = ""
        Public CollDt As String = ""
        Public TotFkOcs As String = ""
        Public OwnGbn As String = ""
        Public LabCmt As String = ""
        Public IOGBN As String = ""
        '< yjlee 2009-01-05 부천순천향병원 
        Public FKOCSTORDCD As String = ""
        '> 

        Public CancelGbn As String = ""

    End Class
End Namespace
