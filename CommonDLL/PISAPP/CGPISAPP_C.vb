'>> 병리채혈 루틴
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports Common.CommFN
Imports Common.CommLogin.LOGIN
Imports Common.SVar

Imports PISAPP.DPIS01.OcsLink.Ord

Namespace DPIS01

#Region " CGDPIS_COMMON.VB"
    Public Class ServerDateTime
        Private Const msFile As String = "File : CGDPIS_COMMON.vb, Class : ServerDateTime" & vbTab

        Dim mDateTime As Date = Now

        Public Sub New()
            MyBase.New()
        End Sub

        Public ReadOnly Property GetDateTime() As Date
            Get
                GetSVRDateTime()
                GetDateTime = mDateTime
            End Get
        End Property

        Public ReadOnly Property GetDateTime24() As String
            Get
                GetSVRDateTime()
                GetDateTime24 = Format(mDateTime, "yyyy-MM-dd HH:mm:ss")
            End Get
        End Property

        Public ReadOnly Property GetDate(Optional ByVal asDelimiter As String = "") As String
            Get
                GetSVRDateTime()
                GetDate = mDateTime.Year.ToString & asDelimiter _
                        & Format(CInt(mDateTime.Month.ToString), "0#") & asDelimiter _
                        & Format(CInt(mDateTime.Day.ToString), "0#")
            End Get
        End Property

        Public ReadOnly Property GetTime24(Optional ByVal asDelimiter As String = ":") As String
            Get
                GetSVRDateTime()
                GetTime24 = Format(CInt(mDateTime.Hour.ToString), "0#") & asDelimiter _
                          & Format(CInt(mDateTime.Minute.ToString), "0#") & asDelimiter _
                          & Format(CInt(mDateTime.Second.ToString), "0#")
            End Get
        End Property

        Private Function GetSVRDateTime() As Boolean
            Dim sFn As String = "Function GetSVRDateTime() As Boolean"

            Try

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery("SELECT fn_ack_date_str(fn_ack_sysdate, 'yyyy-mm-dd hh24:mi:ss') FROM DUAL")

                If dt.Rows.Count > 0 Then
                    mDateTime = CType(dt.Rows(0).Item(0), Date)
                End If

                Return True
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

#End Region

#Region " CGDPIS_OCS.VB"
    Namespace OcsLink
        Public Class SData
            Private Const msFile As String = "File : CGDPIS_OCS.vb, Class : SData@OcsLink" & vbTab

            Public Shared Function fnGet_OcsUsr_Info(ByVal rsUsrId As String) As String
                ' 
                Dim sFn As String = "Public Shared Function fnGet_OcsUsr_Info(String) As String"
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Try
                    sSql += "SELECT user_id usrid, user_nm usrnm"
                    sSql += "  FROM vw_itf_lis_user_info"
                    sSql += " WHERE user_id = ?"
                    sSql += "   AND join_dt <= fn_ack_sysdate"
                    sSql += "   AND rsgt_dt >  fn_ack_sysdate"
                    sSql += " UNION "
                    sSql += "SELECT 'ACK' usrid, '관리자' usrnm"

                    alParm.Add(New OracleParameter("@param0", rsUsrId))

                    DbCommand()
                    Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt.Rows.Count < 1 Then
                        Return ""
                    Else
                        Return dt.Rows(0).Item("usrnm").ToString
                    End If

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try

            End Function

            Public Shared Function fnGet_BldPatInfo(ByVal rsRegno As String, ByVal rsOrderDate As String) As DataTable
                ' 수혈 환자 정보 조회
                Dim sFn As String = "Public Shared Function fnGet_BldPatInfo(ByVal rsRegno As String, ByVal rsOrderDate As String) As DataTable"
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Try
                    sSql += "SELECT a.bunho                                                  "
                    sSql += "     , fn_ack_get_pat_info(a.bunho, '', '') patinfo                      "
                    sSql += "     , r.abo || r.rh aborh      "
                    sSql += "     , r.abo abo          "
                    sSql += "     , r.rh  rh            "
                    sSql += "     , CASE WHEN NVL(a.emergency, '') = '' THEN '' ELSE  '응급' END ernm "
                    sSql += "     , fn_ack_date_str(a.order_date, 'yyyy-mm-dd') order_date   "
                    sSql += "     , fn_ack_date_str(a.ipwon_date, 'yyyy-mm-dd') ipwon_date   "
                    sSql += "     , fn_ack_date_str(a.opdt, 'yyyy-mm-dd') opdt               "
                    sSql += "     , a.gwa deptcd                                             "
                    sSql += "     , fn_ack_get_dr_name(a.doctor)  doctornm               "
                    sSql += "     , a.ho_dong  wardno                                        "
                    sSql += "     , a.ho_code  roomno                                        "
                    sSql += "     , fn_ack_get_pat_diag_name(a.bunho, a.order_date) dignm           "
                    sSql += "     , a.remark drmk                                            "
                    sSql += "     , fn_ack_get_infection(a.bunho) infection                  "
                    sSql += "     , fn_ack_get_bank_remark(a.bunho) sprmk                      "
                    sSql += "     , a.height                                                 "
                    sSql += "     , a.weight                                                 "
                    sSql += "  FROM lf120m b INNER JOIN"
                    sSql += "       mts0001_pis a ON (a.hangmog_code  = b.comordcd AND a.specimen_code = b.spccd)"
                    sSql += "       LEFT OUTER JOIN lr070m r ON (a.bunho = r.regno) "
                    sSql += " WHERE a.bunho         = ?                                      "
                    sSql += "   AND a.order_date    = ?                                      "

                    alParm.Add(New OracleParameter("@param0", rsRegno))
                    alParm.Add(New OracleParameter("@param1", rsOrderDate.Substring(0, 8)))

                    sSql += "   AND b.usdt <= fn_ack_sysdate"
                    sSql += "   AND b.uedt >  fn_ack_sysdate"

                    sSql += " UNION "
                    sSql += "SELECT a.ptnt_no bunho                                                  "
                    sSql += "     , fn_ack_get_pat_info(a.ptnt_no, '', '') patinfo                      "
                    sSql += "     , r.abo || r.rh aborh      "
                    sSql += "     , r.abo abo          "
                    sSql += "     , r.rh  rh            "
                    sSql += "     , CASE WHEN NVL(a.emer_yn, '') = 'Y' THEN '응급' ELSE  '' END ernm "
                    sSql += "     , fn_ack_date_str(a.ord_ymd, 'yyyy-mm-dd') order_date      "
                    sSql += "     , fn_ack_date_str(a.admi_ymd, 'yyyy-mm-dd') ipwon_date     "
                    sSql += "     , NULL opdt                                                "
                    sSql += "     , a.ord_dept deptcd                                        "
                    sSql += "     , fn_ack_get_dr_name(a.ord_dr)  doctornm               "
                    sSql += "     , a.ward  wardno                                           "
                    sSql += "     , a.room  roomno                                           "
                    sSql += "     , fn_ack_get_pat_diag_name(a.ptnt_no, a.ord_ymd) dignm            "
                    sSql += "     , a.ord_rmk drmk                                           "
                    sSql += "     , fn_ack_get_infection(a.ptnt_no) infection                   "
                    sSql += "     , fn_ack_get_bank_remark(a.ptnt_no) sprmk                       "
                    sSql += "     , NULL height                                              "
                    sSql += "     , NULL weight                                              "
                    sSql += "  FROM lf120m b INNER JOIN"
                    sSql += "       fkitf..vw_itf_lis_ord_info a ON (a.ord_cd = b.comordcd  AND a.spc_cd = b.spccd)"
                    sSql += "       LEFT OUTER JOIN lr070m r ON (a.ptnt_no = r.regno) "
                    sSql += " WHERE a.ptnt_no       = ?                                      "
                    sSql += "   AND a.ord_ymd       = ?                                      "

                    alParm.Add(New OracleParameter("@param0", rsRegno))
                    alParm.Add(New OracleParameter("@param1", rsOrderDate.Substring(0, 8)))

                    sSql += "   AND b.usdt <= fn_ack_sysdate"
                    sSql += "   AND b.uedt >  fn_ack_sysdate"

                    DbCommand()
                    Return DbExecuteQuery(sSql, alParm)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

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
                    sSql += "       sujumin1 || '-' || sujumin2 idno_full, sujumin1 || '-' || SUBSTR(sujumin2, 1, 1) || '******' idno"
                    sSql += "  FROM (SELECT 0 seq, patno bunho, patnm suname, sex, fn_ack_date_str(birtdate, 'yyyy-mm-dd') birth,"
                    sSql += "               resno1 sujumin1, resno2 sujumin2, telno1 tel1, telno2 tel2"
                    sSql += "          FROM vw_ack_ocs_pat_info"
                    If rsRegNo <> "" Then
                        sSql += "         WHERE patno = ?"
                        alParm.Add(New OracleParameter("regno", rsRegNo))
                    ElseIf rsPatNm <> "" Then
                        sSql += "         WHERE patnm LIKE ? || '%'"
                        alParm.Add(New OracleParameter("patnm", rsPatNm))
                    End If
                    sSql += "         UNION"
                    sSql += "        SELECT seq, bunho, suname, sex, birth, sujumin1, sujumin2, tel1, tel2"
                    sSql += "          FROM mts0002_pis"
                    If rsRegNo <> "" Then
                        sSql += "         WHERE bunho = ?"
                        alParm.Add(New OracleParameter("regno", rsRegNo))
                    ElseIf rsPatNm <> "" Then
                        sSql += "         WHERE suname LIKE ? || '%'"
                        alParm.Add(New OracleParameter("patnm", rsPatNm))
                    End If
                    sSql += "       ) p"
                    sSql += " ORDER BY seq"


                    DbCommand()
                    Return DbExecuteQuery(sSql, alParm)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try

            End Function

            Public Shared Function fnGet_DeptDoctorList(ByVal rsDeptCd As String, ByVal rsDoctorCd As String) As DataTable
                Dim sFn As String = "fnGet_DoctorListfnGet_DeptDoctorList"

                Try
                    Dim sSql As String = ""
                    Dim alParm As New ArrayList

                    sSql = ""
                    sSql += "SELECT DISTINCT"
                    sSql += "       '' chk, a.cln_dept_cd deptcd, a.cln_dept_nm deptnm, b.dr_id doctorcd, b.dr_nm doctornm"
                    sSql += "  FROM fkitf..vw_itf_lis_cln_dept_info a, fkitf..vw_itf_lis_dr_info b"
                    sSql += " WHERE b.dept_cd     = a.cln_dept_cd"
                    sSql += "   AND b.join_dt    <= SYSDATE"
                    sSql += "   AND b.rsgt_dt    >  SYSDATE"
                    sSql += "   AND b.app_str_dt <= SYSDATE"
                    sSql += "   AND b.app_end_dt >  SYSDATE"

                    If rsDeptCd <> "" Then
                        sSql += "   AND b.dept_cd = ?"
                        alParm.Add(New OracleParameter("dept", rsDeptCd))
                    End If

                    If rsDoctorCd <> "" Then
                        sSql += "   AND b.dr_id = ?"
                        alParm.Add(New OracleParameter("doctor", rsDoctorCd))
                    End If

                    DbCommand()
                    Return DbExecuteQuery(sSql, alParm)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try

            End Function

            Public Shared Function fnGet_DoctorList(ByVal rsDeptCd As String, Optional ByVal rsDoctorCd As String = "") As DataTable
                Dim sFn As String = "fnGet_DoctorList"

                Try
                    Dim sSql As String = ""
                    Dim alParm As New ArrayList

                    sSql = ""
                    sSql += "SELECT DISTINCT"
                    sSql += "       dr_id doctorcd, dr_nm doctornm, hp_no doctortel"
                    sSql += "  FROM fkitf..vw_itf_lis_dr_info"

                    If rsDeptCd <> "" Then
                        sSql += " WHERE trim(dept_cd) = ?"
                        alParm.Add(New OracleParameter("dept", rsDeptCd))
                    End If

                    If rsDoctorCd <> "" Then
                        sSql += IIf(sSql.IndexOf("WHERE") < 0, " WHERE ", "   AND ").ToString + "dr_id = ?"
                        alParm.Add(New OracleParameter("doctor", rsDoctorCd))
                    End If

                    DbCommand()
                    Return DbExecuteQuery(sSql, alParm)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try

            End Function

            Public Shared Function fnGet_RoomList(ByVal rsWardNo As String, Optional ByVal rsRoomno As String = "") As DataTable
                Dim sFn As String = "Public Shared Function fnGet_RoomList(String) As DataTable"


                Try
                    Dim sSql As String = ""
                    Dim al As New ArrayList

                    sSql = ""
                    sSql += "SELECT '' chk, ward wardno, room roomno"
                    sSql += "  FROM fkitf..vw_itf_lis_ward_info"
                    If rsWardNo <> "" Then
                        sSql += " WHERE ward = ?"
                        al.Add(New OracleParameter("wardcd", rsWardNo))

                        If rsRoomno <> "" Then
                            sSql += "   AND room = ?"
                            al.Add(New OracleParameter("room", rsRoomno))
                        End If

                    End If

                    DbCommand()
                    Return DbExecuteQuery(sSql, al)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try
            End Function

            Public Shared Function fnGet_WardList(Optional ByVal rsWardno As String = "") As DataTable
                Dim sFn As String = "Public Shared Function fnGet_WardList() As DataTable"

                Try
                    Dim sSql As String = ""
                    Dim al As New ArrayList

                    sSql = ""
                    sSql += "SELECT DISTINCT"
                    sSql += "       '' chk, ward wardno, ward_nm wardnm"
                    sSql += "  FROM fkitf..vw_itf_lis_ward_info"

                    If rsWardno <> "" Then
                        sSql += " WHERE ward = ?"
                        al.Add(New OracleParameter("ward", rsWardno))
                    End If

                    sSql += " ORDER BY ward"

                    DbCommand()
                    Return DbExecuteQuery(sSql, al)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try
            End Function

            Public Shared Function fnGet_DeptList(Optional ByVal rsDeptCd As String = "") As DataTable
                Dim sFn As String = "Public Shared Function fnGet_DeptList() As DataTable"

                Try
                    Dim sSql As String = ""
                    Dim al As New ArrayList

                    sSql = ""
                    sSql += " SELECT DISTINCT '' chk, cln_dept_cd deptcd, cln_dept_nm deptnm"
                    sSql += "   FROM fkitf..vw_itf_lis_cln_dept_info"
                    If rsDeptCd <> "" Then
                        sSql += " WHERE cln_dept_cd = ?"
                        al.Add(New OracleParameter("dept", rsDeptCd))
                    End If
                    sSql += "  ORDER BY cln_dept_cd"

                    DbCommand()
                    Return DbExecuteQuery(sSql, al)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

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

                    al.Add(New OracleParameter("regno", rsRegNo))

                    Dim dt As DataTable = DbExecuteQuery(sSql, al, False)

                    Return dt

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


                End Try
            End Function


            Public Shared Function fnGet_PatInfo_byNm(ByVal rsNm As String, Optional ByVal rsIdNoL As String = "", Optional ByVal rsIdNoR As String = "") As DataTable
                Dim sFn As String = "Function fnGet_PatInfo_byNm"
                Dim sSql As String = ""
                Dim dt As New DataTable

                Dim al As New ArrayList

                Try
                    sSql = ""
                    sSql += "SELECT p.bunho, p.suname, p.sex, p.sujumin1 + RPAD(SUBSTR(p.sujumin2, 1, 1), 7, '*') sujumin,"
                    sSql += "       fn_ack_get_pat_wardroom(p.bunho) wardroom, p.address1"
                    sSql += "  FROM (SELECT patno bunho, patnm suname, a.sex, resno1 sujumin1, resno2 sujumin2, address1"
                    sSql += "          FROM vw_ack_ocs_pat_info a"
                    sSql += "         WHERE a.patnm LIKE '%' || ? || '%'"

                    al.Add(New OracleParameter("patnm", rsNm))

                    If rsIdNoL <> "" Then
                        sSql += "           AND a.regno1 LIKE ? || '%'"
                        al.Add(New OracleParameter("regno1", rsIdNoL))
                    End If

                    If rsIdNoR <> "" Then
                        sSql += "           AND a.regno2 LIKE ? || '%'"
                        al.Add(New OracleParameter("regno2", rsIdNoR))
                    End If

                    sSql += "           AND EXISTS (SELECT 'x'"
                    sSql += "                         FROM fkitf..vw_itf_lis_ord_info z"
                    sSql += "                        WHERE z.ptnt_no = a.ptnt_no"
                    sSql += "                        UNION ALL"
                    sSql += "                       SELECT 'x'"
                    sSql += "                         FROM mts0001_lis z"
                    sSql += "                        WHERE z.bunho = a.ptnt_no"
                    sSql += "                      )"
                    sSql += "         UNION "
                    sSql += "        SELECT a.bunho, a.suname, a.sex, a.sujumin1, a.sujumin2, address1"
                    sSql += "          FROM mts0002_pis a"
                    sSql += "         WHERE a.suname LIKE '%' ||  ? || '%'"

                    al.Add(New OracleParameter("patnm", rsNm))

                    If rsIdNoL <> "" Then
                        sSql += "           AND a.sujumin1 LIKE ? || '%'"
                        al.Add(New OracleParameter("patnm", rsIdNoL))
                    End If

                    If rsIdNoR <> "" Then
                        sSql += "           AND a.sujumin2 LIKE ? || '%'"
                        al.Add(New OracleParameter("patnm", rsIdNoR))
                    End If

                    sSql += "           AND EXISTS (SELECT 'x'"
                    sSql += "                         FROM fkitf..vw_itf_lis_ord_info z"
                    sSql += "                        WHERE z.ptnt_no = a.bunho"
                    sSql += "                        UNION ALL"
                    sSql += "                       SELECT 'x'"
                    sSql += "                         FROM mts0001_lis z"
                    sSql += "                        WHERE z.bunho = a.bunho"
                    sSql += "                      )"
                    sSql += "       ) p"



                    DbCommand()
                    dt = DbExecuteQuery(sSql, al)

                    Return dt

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


                End Try
            End Function

            Public Shared Function fnGet_DiagNm(ByVal rsRegNo As String, ByVal rsOrdDayB As String, ByVal rsOrdDayE As String, ByVal rsOwnGbn As String) As String
                Dim sFn As String = "Public Shared Function fnGet_DiagNm(String, String, String, String) As String"

                Dim sSql As String = ""

                Dim sDiagNm As String = ""
                Dim sDiagNmE As String = ""

                Try
                    Dim al As New ArrayList

                    sSql += " SELECT *"
                    sSql += "   FROM (SELECT diag_nm_kr korname,"
                    sSql += "                diag_nm_en engname, cln_ymd order_date"
                    sSql += "           FROM fkitf..vw_itf_lis_ptnt_diag_info"
                    sSql += "          WHERE ptnt_no  = ?"
                    sSql += "            AND cln_ymd <= ?"
                    sSql += "            AND diag_yn  = 'Y'"
                    sSql += "        ) a"
                    sSql += " WHERE ROWNUM = 1"
                    sSql += " ORDER BY order_date DESC"

                    al.Add(New OracleParameter("idno1", rsRegNo))
                    al.Add(New OracleParameter("rcvdaye", rsOrdDayE))

                    DbCommand()

                    Dim dt As DataTable = DbExecuteQuery(sSql, al)

                    Dim sReturn As String = ""

                    If dt.Rows.Count > 0 Then
                        sReturn = dt.Rows(0).Item(0).ToString + Convert.ToChar(124) + dt.Rows(0).Item(1).ToString
                    Else
                        sReturn = "" + Convert.ToChar(124) + ""
                    End If

                    Return sReturn

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


                End Try
            End Function

            '-- 감염여부
            Public Shared Function fnGet_Pat_Infection(ByVal rsRegNo As String, ByVal rbPrtNm As Boolean) As String
                Dim sFn As String = "Public Shared Function fnGet_Pat_Infection(String, Boolean) As strubg"

                Dim sSql As String = ""
                Dim sReturn As String = ""

                Try
                    sSql = ""
                    If rbPrtNm Then
                        sSql += "SELECT fn_ack_get_infection_prt(?) infinfo FROM DUAL"
                    Else
                        sSql += "SELECT fn_ack_get_infection(?) infinfo FROM DUAL"
                    End If

                    Dim al As New ArrayList

                    al.Add(New OracleParameter("id", rsRegNo))

                    DbCommand()
                    Dim dt As DataTable = DbExecuteQuery(sSql, al)

                    If dt Is Nothing Then Return sReturn
                    If dt.Rows.Count > 0 Then
                        sReturn = dt.Rows(0).Item("infinfo").ToString.Replace(",", "/")
                    End If

                    Return sReturn

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try
            End Function

        End Class

        Public Class Ord
            Private Const msFile As String = "CGDA_OCS.vb, Class : Ord@OcsLink" & vbTab

            Protected Shared m_dbCn As OracleConnection
            Protected Shared m_dbTran As OracleTransaction

            Public Shared Function fnGet_Coll_PatList(ByVal rsIoGbn As String, ByVal rsDptOrWard As String, _
                                                              ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                                              ByVal rsSpcFlgS As String, ByVal rsSpcFlgE As String) As DataTable
                Dim sFn As String = "Public Shared Function fnGet_Coll_PatList(String, String, String, String, String, String) As String"

                Try
                    Dim sSql As String = ""
                    Dim alPram As New ArrayList

                    If rsIoGbn = "I" Then
                        sSql += "pkg_pis.pkg_get_patlist_in"
                    Else
                        If rsDptOrWard = "" Then
                            sSql += "pkg_pis.pkg_get_patlist_out"
                        Else
                            sSql += "pkg_pis.pkg_get_patlist_dept"
                        End If
                    End If

                    If rsIoGbn = "O" And rsDptOrWard = "" Then
                    Else
                        alPram.Add(New OracleParameter("dptward", rsDptOrWard))
                    End If
                    alPram.Add(New OracleParameter("orddt1", rsOrdDtS.Replace("-", "")))
                    alPram.Add(New OracleParameter("orddt2", rsOrdDtE.Replace("-", "")))
                    alPram.Add(New OracleParameter("spcflg1", rsSpcFlgS))
                    alPram.Add(New OracleParameter("spcflg2", rsSpcFlgE))

                    DbCommand()
                    Return DbExecuteQuery(sSql, alPram, False)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try
            End Function

            Public Shared Function fnGet_Coll_PatList_RegNo(ByVal rsRegNo As String, _
                                                            ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                                            ByVal rsSpcFlgS As String, ByVal rsSpcFlgE As String) As DataTable
                Dim sFn As String = "Public Shared Function fnGet_Coll_PatList_RegNo(String, String, String, String, String) As String"

                Try
                    Dim sSql As String = ""
                    Dim alPram As New ArrayList

                    sSql += "pkg_pis.pkg_get_patlist_regno"

                    alPram.Add(New OracleParameter("regno", rsRegNo))
                    alPram.Add(New OracleParameter("orddt1", rsOrdDtS.Replace("-", "")))
                    alPram.Add(New OracleParameter("orddt2", rsOrdDtE.Replace("-", "")))
                    alPram.Add(New OracleParameter("spcflg1", rsSpcFlgS))
                    alPram.Add(New OracleParameter("spcflg2", rsSpcFlgE))

                    DbCommand()
                    Return DbExecuteQuery(sSql, alPram, False)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try
            End Function

            Public Shared Function fnGet_Coll_Order_Ward(ByVal rsRegNo As String, ByVal rsWardCd As String, _
                                                           ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                                           ByVal rsSpcFlgS As String, ByVal rsSpcFlgE As String) As DataTable
                Dim sFn As String = "Public Shared Function fnGet_Coll_Order_Ward(String, String, String, String, String, String) As String"

                Try
                    Dim sSql As String = ""
                    Dim alPram As New ArrayList

                    sSql += "pkg_pis.pkg_get_order_ward"

                    alPram.Add(New OracleParameter("regno", rsRegNo))
                    alPram.Add(New OracleParameter("wardcd", rsWardCd))
                    alPram.Add(New OracleParameter("orddt1", rsOrdDtS.Replace("-", "")))
                    alPram.Add(New OracleParameter("orddt2", rsOrdDtE.Replace("-", "")))
                    alPram.Add(New OracleParameter("spcflg1", rsSpcFlgS))
                    alPram.Add(New OracleParameter("spcflg2", rsSpcFlgE))

                    DbCommand()
                    Return DbExecuteQuery(sSql, alPram, False)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try
            End Function

            Public Shared Function fnGet_Coll_Order_Dept(ByVal rsRegNo As String, ByVal rsDeptCd As String, _
                                                         ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                                         ByVal rsSpcFlgS As String, ByVal rsSpcFlgE As String) As DataTable
                Dim sFn As String = "Public Shared Function fnGet_Coll_OrdList_Dept(String, String, String, String, String, String) As String"

                Try
                    Dim sSql As String = ""
                    Dim alPram As New ArrayList

                    If rsDeptCd = "" Then
                        sSql += "pkg_pis.pkg_get_order_regno"
                        alPram.Add(New OracleParameter("regno", rsRegNo))
                    Else
                        sSql += "pkg_pis.pkg_get_order_dept"
                        alPram.Add(New OracleParameter("deptcd", rsDeptCd))
                    End If

                    alPram.Add(New OracleParameter("orddt1", rsOrdDtS.Replace("-", "")))
                    alPram.Add(New OracleParameter("orddt2", rsOrdDtE.Replace("-", "")))
                    alPram.Add(New OracleParameter("spcflg1", rsSpcFlgS))
                    alPram.Add(New OracleParameter("spcflg2", rsSpcFlgE))

                    DbCommand()
                    Return DbExecuteQuery(sSql, alPram, False)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try
            End Function

            Public Shared Function fnGet_Coll_Order_batch(ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                                          ByVal rsSpcFlgS As String, ByVal rsSpcFlgE As String) As DataTable
                Dim sFn As String = "Public Shared Function fnGet_Coll_Order_batch(String, String, String, String) As String"

                Try
                    Dim sSql As String = ""
                    Dim alPram As New ArrayList

                    sSql += "pkg_pis.pkg_get_order_batch"

                    alPram.Add(New OracleParameter("orddt1", rsOrdDtS.Replace("-", "").Replace(":", "").Replace(" ", "")))
                    alPram.Add(New OracleParameter("orddt2", rsOrdDtE.Replace("-", "").Replace(":", "").Replace(" ", "")))
                    alPram.Add(New OracleParameter("spcflg1", rsSpcFlgS))
                    alPram.Add(New OracleParameter("spcflg2", rsSpcFlgE))

                    DbCommand()
                    Return DbExecuteQuery(sSql, alPram, False)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                End Try
            End Function

            Public Shared Function fnSet_PatInfo(ByVal r_dr As DataRow, ByVal rdtSysDate As Date) As STU_PatInfo
                Dim sFn As String = "Public Function fnSet_PatInfo(DataRow, date) As stu_PatInfo"

                Dim cpi As New STU_PatInfo

                Try
                    Dim sBuf() As String = r_dr.Item("patinfo").ToString.Split(Chr(124))

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
                    cpi.WARD = r_dr.Item("wardcd").ToString
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
                    Fn.log(msFile & sFn, Err)
                    Return cpi
                End Try
            End Function

            Public Shared Function SetOrderChgCollState(ByVal r_css As ChgOcsState, ByVal rsToColl As Boolean) As Integer
                Dim sFn As String = "Public Shared Function SetOrderChgCollState(ChgCollState) As Integer"

                Dim dbCmd As New OracleCommand
                Dim iRows As Integer = 0
                Dim sSql As String = ""

                Try
                    Dim sFkOcs() As String = r_css.TotFkOcs.Split(","c)

                    For ix As Integer = 0 To sFkOcs.Length - 1
                        sSql = "pro_ack_exe_ocs_coll_pis"

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

                            .Parameters.Add(New OracleParameter("regno", r_css.RegNo))
                            .Parameters.Add(New OracleParameter("owngbn", r_css.OwnGbn))
                            .Parameters.Add(New OracleParameter("fkocs", sFkOcs(ix)))

                            If rsToColl Then
                                .Parameters.Add(New OracleParameter("spcflg", "2"))
                                .Parameters.Add(New OracleParameter("colldt", r_css.CollDt))
                            Else
                                .Parameters.Add(New OracleParameter("spcflg", "1"))
                                .Parameters.Add(New OracleParameter("colldt", ""))
                            End If

                            .Parameters.Add(New OracleParameter("usrid", USER_INFO.USRID))
                            .Parameters.Add(New OracleParameter("ip   ", USER_INFO.LOCALIP))

                            .Parameters.Add("retval", OracleDbType.Int32)
                            .Parameters("retval").Direction = ParameterDirection.InputOutput
                            .Parameters("retval").Value = -1

                            .ExecuteNonQuery()

                            iRows += CType(.Parameters(7).Value, Integer)

                        End With
                    Next

                    Return iRows

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    If dbCmd IsNot Nothing Then
                        dbCmd.Dispose()
                        dbCmd = Nothing
                    End If

                End Try
            End Function

            Public Shared Function SetOrderChgCollState(ByVal r_css As ChgOcsState, ByVal rbToColl As Boolean, ByVal r_dbCn As OracleConnection, ByVal r_lisDbTran As OracleTransaction) As Integer
                Dim sFn As String = "Public Shared Function SetOrderChgCollState(ChgCollState, LisDbConnection, LisDbTransaction) As Integer"

                m_dbCn = r_dbCn
                m_dbTran = r_lisDbTran

                Return SetOrderChgCollState(r_css, rbToColl)
            End Function

            Public Shared Function SetOrderChgLisCmt(ByVal r_css As ChgOcsState) As Integer
                Dim sFn As String = "Public Shared Function SetOrderChgLabCmt(ChgCollState) As Integer"

                Dim dbCmd As New OracleCommand
                Dim iRows As Integer = 0
                Dim sSql As String = ""

                Try

                    sSql = "pro_ack_exe_ocs_liscmt_pis"
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

                        .Parameters.Add(New OracleParameter("regno ", r_css.RegNo))
                        .Parameters.Add(New OracleParameter("owngbn", r_css.OwnGbn))
                        .Parameters.Add(New OracleParameter("fkocs ", r_css.TotFkOcs))
                        .Parameters.Add(New OracleParameter("liscmt", r_css.LabCmt))
                        .Parameters.Add(New OracleParameter("usrid ", USER_INFO.USRID))
                        .Parameters.Add(New OracleParameter("ip    ", USER_INFO.LOCALIP))

                        .Parameters.Add("retval", OracleDbType.Int32)
                        .Parameters("retval").Direction = ParameterDirection.InputOutput
                        .Parameters("retval").Value = -1

                        .ExecuteNonQuery()

                        iRows = CType(.Parameters(6).Value, Integer)

                    End With

                    Return iRows

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    If dbCmd IsNot Nothing Then
                        dbCmd.Dispose()
                        dbCmd = Nothing
                    End If

                End Try
            End Function

            Public Shared Function SetOrderChgLisCmt(ByVal r_css As ChgOcsState, ByVal r_dbCn As OracleConnection, ByVal r_lisDbTran As OracleTransaction) As Integer
                Dim sFn As String = "Public Shared Function SetOrderChgLabCmt(String, String, LisDbConnection, LisDbTransaction) As Integer"

                m_dbCn = r_dbCn
                m_dbTran = r_lisDbTran

                Return SetOrderChgLisCmt(r_css)
            End Function

            Private Shared Function SetOrderChgCancelState(ByVal r_css As ChgOcsState) As String
                Dim sFn As String = "Public Shared Function SetOrderChgCancelState(ChgCollState) As string"

                Dim dbCmd As New OracleCommand
                Dim iRows As Integer = 0
                Dim sSql As String = ""

                Try
                    Dim sFkOcs() As String = r_css.TotFkOcs.Split(","c)

                    For ix As Integer = 0 To sFkOcs.Length - 1
                        sSql = "pro_ack_exe_ocs_cancel_pis"

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

                            .Parameters.Add(New OracleParameter("bcno  ", r_css.BcNo))
                            .Parameters.Add(New OracleParameter("regno ", r_css.RegNo))
                            .Parameters.Add(New OracleParameter("owngbn", r_css.OwnGbn))
                            .Parameters.Add(New OracleParameter("fkocs ", sFkOcs(ix)))
                            .Parameters.Add(New OracleParameter("cancel", r_css.CancelGbn))
                            .Parameters.Add(New OracleParameter("usrid ", USER_INFO.USRID))
                            .Parameters.Add(New OracleParameter("cancel", USER_INFO.LOCALIP))

                            .Parameters.Add("retval", OracleDbType.Int32)
                            .Parameters("retval").Direction = ParameterDirection.InputOutput
                            .Parameters("retval").Value = -1

                            .ExecuteNonQuery()

                            iRows += CType(.Parameters(7).Value, Integer)

                        End With
                    Next

                    If iRows > 0 Then
                        Return ""
                    Else
                        Return "처방상태 변경에 오류가 발생했습니다."
                    End If


                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    If dbCmd IsNot Nothing Then
                        dbCmd.Dispose()
                        dbCmd = Nothing
                    End If

                End Try
            End Function

            Public Shared Function SetOrderChgCancelState(ByVal r_css As ChgOcsState, ByVal r_dbCn As OracleConnection, ByVal r_lisDbTran As OracleTransaction) As String
                Dim sFn As String = "Public Shared Function SetOrderChgCaneclState(String, String, LisDbConnection, LisDbTransaction) As string"

                m_dbCn = r_dbCn
                m_dbTran = r_lisDbTran

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
                        sSql = "pro_exe_tx_ocs_pass_pis"

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
                            .Parameters.Add(New OracleParameter("regno", r_dt.Rows(ix).Item("regno").ToString))
                            .Parameters.Add(New OracleParameter("owngbn", r_dt.Rows(ix).Item("owngbn").ToString))
                            .Parameters.Add(New OracleParameter("fkocs", r_dt.Rows(ix).Item("fkocs").ToString))

                            .Parameters.Add(New OracleParameter("passdt", r_dt.Rows(ix).Item("curdt").ToString))

                            .Parameters.Add(New OracleParameter("usrid", USER_INFO.USRID))
                            .Parameters.Add(New OracleParameter("ip   ", USER_INFO.LOCALIP))

                            .Parameters.Add("retval", OracleDbType.Int32)
                            .Parameters("retval").Direction = ParameterDirection.InputOutput
                            .Parameters("retval").Value = -1


                            .ExecuteNonQuery()

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
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    If dbCmd IsNot Nothing Then
                        dbCmd.Dispose()
                        dbCmd = Nothing
                    End If

                End Try
            End Function

            Public Shared Function SetPassState(ByVal r_dt As DataTable, ByVal r_dbCn As OracleConnection, ByVal r_lisDbTran As OracleTransaction) As String
                Dim sFn As String = "Public Shared Function SetPassState(DataTable, LisDbConnection, LisDbTransaction) As string"

                m_dbCn = r_dbCn
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

#End Region

#Region " CDDPIS_C.VB"

    Namespace Coll_PIS
#Region " 채혈관련 쿼리"
        Public Class SData
            Inherits Exec
            Private Const msFile As String = "File : CDDPIS_C.vb, Class : DPIS_C_QRY" & vbTab

            Public Sub New()
                MyBase.New()
            End Sub

            Public Shared Function fnGet_Collect_CancelData(ByVal rsBcNo As String) As DataTable
                Dim sFn As String = "Public fnGet_Collect_CancelData(String) As DataTable"

                Try
                    Dim sSql As String = ""
                    Dim alPara As New ArrayList

                    sSql += "SELECT DISTINCT"
                    sSql += "       j.ptntno regno, j.barcd_no bcno, j.inspshpcd tclscd, j.smpore spccd, j.fkocs, 'S' tcdgbn,"
                    sSql += "       j.hlcr_vsthsp_dvs_cd iogbn, CASE WHEN j.ocs_key1 IS NULL THEN 'P' ELSE 'O' END owngbn,"
                    sSql += "       'PP' bcclscd, CASE WHEN j.rcv_state_cd <= '4' THEN j.rcv_state_cd ELSE '4' END spcflg,"
                    sSql += "       j.inspshpcd"
                    sSql += "  FROM fkpis.rcv_info j"
                    sSql += " WHERE j.barcd_no  = ?"
                    sSql += "   AND j.hlcr_vsthsp_dvs_cd IN ('1', '2')"

                    alPara.Clear()
                    alPara.Add(New OracleParameter("bcno", rsBcNo))

                    DbCommand()
                    Return DbExecuteQuery(sSql, alPara)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
                End Try

            End Function

            Public Shared Function fnGet_CollectInfo_bcnos(ByVal rsBcNos As String) As DataTable
                Dim sFn As String = "Public fnGet_CollectInfo(String) As DataTable"

                Try
                    Dim sSql As String = ""

                    sSql += "SELECT DISTINCT"
                    sSql += "       fn_ack_get_bcno_full_pis(j.barcd_no) bcno, "
                    sSql += "       CASE WHEN j.rcv_state_cd <= '4' THEN j.rcv_state_cd ELSE '4' END spcflg,"
                    sSql += "       f.insp_nm testnms,"
                    sSql += "       CASE WHEN j.rcv_state_cd <= '4' THEN '0' ELSE j.rcv_state_cd END rstflg,"
                    sSql += "       fn_ack_get_bcno_fkocs_pis(j.bcno) bcno_fkocs,"
                    sSql += "       j.ptntno regno, fn_ack_get_pat_info(j.ptntno, '', '') patinfo"
                    sSql += "  FROM fkpis..rcv_info j, fkpis..inspshpcd f"
                    sSql += " WHERE j.inspshpcd = f.inspshpcd"
                    sSql += "   AND j.rcv_state_cd IN ('1', '2')"
                    sSql += " ORDER BY bcno"

                    DbCommand()
                    Return DbExecuteQuery(sSql)

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
                End Try

            End Function

            ' 검체번호(바코드번호) 가져오기
            Public Shared Function fnGet_BCNO(ByVal rsSeqDate As String, ByVal rsSeqGbn As String) As String
                Dim sFn As String = "Public Function GetBCNO(ByVal asDATE As String, ByVal asGBN As String) As String"

                Try
                    Dim sSql As String = "pro_ack_exe_seqno_bc_pis"
                    Dim oleParm As New DBORA.DbParrameter

                    With oleParm
                        .AddItem("seqymd", OracleDbType.Varchar2, ParameterDirection.Input, rsSeqDate)
                        .AddItem("seqgbn", OracleDbType.Varchar2, ParameterDirection.Input, rsSeqGbn)
                    End With

                    DbCommand()
                    DbExecute(sSql, oleParm, False)

                    Return Format(CType(oleParm.Item(2).Value.ToString, Integer), "000#")

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
                End Try

            End Function


            Public Shared Function fnGet_Remark(ByVal rsBcNo As String) As String
                Dim sFn As String = "Public Shared Function fnGet_Remark(String) As DataTable"

                Try
                    Dim sSql As String = ""

                    sSql = ""
                    sSql += "SELECT fn_ack_get_dr_remark_pis(?) remark FROM DUAL"

                    Dim al As New ArrayList

                    For i As Integer = 1 To 1
                        al.Add(New OracleParameter("@param" + Format(i, "00"), rsBcNo))
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
                    Return ""
                End Try
            End Function

            Public Shared Function fnGet_COMMENT(ByVal rsFkocs As String) As String
                Dim sFn As String = "Public Shared Function fnGet_COMMENT(String) As DataTable"

                Try
                    Dim sSql As String = ""

                    sSql = ""
                    sSql += "SELECT fn_ack_get_liscmt_pis(?) remark FROM DUAL"

                    Dim al As New ArrayList

                    For i As Integer = 1 To 1
                        al.Add(New OracleParameter("@param" + Format(i, "00"), rsFkocs))
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
                    Return ""
                End Try
            End Function


        End Class

#End Region

#Region " 채혈시행 "
        Public Class Exec
            Inherits ClassErr

            Private Const msFile As String = "File : CGDA_C.vb, Class : LISAPP.APP_C.DB_Collect" & vbTab

            Private m_DbCn As OracleConnection
            Private m_DbTran As OracleTransaction

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

            '> 개별 채혈
            Public Function ExecuteDo(ByVal r_al_bcinfo As ArrayList, ByVal r_al_diag As ArrayList, _
                                      ByVal rsForm As String, _
                                      ByVal rsPrinterName As String, _
                                      ByVal rbToColl As Boolean, _
                                      ByVal rbAutoTkMode As Boolean, _
                                      ByVal rbBcPrt As Boolean) As ArrayList
                Dim sFn As String = "Public Function ExecuteDo(ArrayList, ArrayList, String, Boolean, Boolean) As ArrayList"

                Dim al_return As New ArrayList

                Dim sTimeS As String = ""
                Dim sTimeE As String = ""

                Dim sTimePS As String = ""
                Dim sTimePE As String = ""


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
                                Dim objBCPrt As New Coll_PIS.BCPrinter(rsForm)
                                objBCPrt.PrintDoBarcode(arr, 1, rsForm, True, rsPrinterName)
                            End If

                            al_return.Add(listcollData)
                        End If
                    Next

                    Return al_return

                Catch ex As Exception
                    SetError(Err.Number, Err.Description)
                    Fn.log(msFile & sFn, Err)

                    Return al_return
                End Try
            End Function

            'ByVal rbRegCollReg As Boolean, _
            '> 개별 채혈 NEW 
            Public Function ExecuteDo_Coll(ByVal r_al_bcinfo As ArrayList, ByVal r_al_diag As ArrayList, _
                                              ByVal rbRegCollDt As Boolean, _
                                                Optional ByVal rsForm As String = "", _
                                                 Optional ByVal rbFirst As Boolean = False, _
                                                     Optional ByVal rsPrinterName As String = "", _
                                                        Optional ByVal rbAutoTkMode As Boolean = False) As ArrayList
                Dim sFn As String = "Public Function ExecuteDo_Coll(ArrayList, ArrayList, String, Boolean) As ArrayList"

                Dim al_return As New ArrayList

                Dim sTimeS As String = ""
                Dim sTimeE As String = ""

                Dim sTimePS As String = ""
                Dim sTimePE As String = ""


                Try
                    m_al_DiagData = r_al_diag

                    m_DbCn = GetDbConnection()

                    For i As Integer = 1 To r_al_bcinfo.Count
                        Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_bcinfo(i - 1), List(Of STU_CollectInfo))

                        Dim sReturn As String = ExecuteDo_One_Coll(listcollData, r_al_diag, rbAutoTkMode, rbRegCollDt)

                        If sReturn <> "" Then
                            For Each collData As STU_CollectInfo In listcollData
                                collData.BCNO = sReturn
                            Next

                            Dim arr As New ArrayList
                            arr.Add(listcollData)
                            Dim objBCPrt As New Coll_PIS.BCPrinter(rsForm)
                            objBCPrt.PrintDoBarcode(arr, 1, rsForm, rbFirst, rsPrinterName)
                            al_return.Add(listcollData)
                        End If
                    Next

                    Return al_return

                Catch ex As Exception
                    SetError(Err.Number, Err.Description)
                    Fn.log(msFile & sFn, Err)

                    Return al_return

                End Try
            End Function


            '> 개별 채혈 + 접수
            Public Function ExecuteDo(ByVal r_al_bcinfo As ArrayList, ByVal r_al_diag As ArrayList, _
                                      ByVal rbTk As Boolean) As ArrayList
                Dim sFn As String = "Public Function ExecuteDo(String, ArrayList, ArrayList, ArrayList, ArrayList, Boolean) As ArrayList"

                Dim al_return As New ArrayList

                Try
                    m_al_DiagData = r_al_diag

                    m_DbCn = GetDbConnection()

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
                    SetError(Err.Number, Err.Description)
                    Fn.log(msFile & sFn, Err)

                    Return al_return

                End Try
            End Function

            '> 개별 채혈 + 혈액은행 접수
            Public Function ExecuteDo(ByVal r_al_bcinfo As ArrayList, ByVal r_al_diag As ArrayList, _
                                           ByVal rbToColl As Boolean, ByVal rbTk As Boolean, ByVal rbBnk As Boolean) As ArrayList
                Dim sFn As String = "Public Function ExecuteDo(String, ArrayList, ArrayList, Boolean) As ArrayList"

                Dim al_return As New ArrayList

                Try
                    m_al_DiagData = r_al_diag

                    m_DbCn = GetDbConnection()

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
                    SetError(Err.Number, Err.Description)
                    Fn.log(msFile & sFn, Err)

                    Return al_return

                End Try
            End Function

            Public Function ExecuteDo_One(ByVal r_listcollData As List(Of STU_CollectInfo), _
                                          ByVal r_al_diag As ArrayList, ByVal rbToColl As Boolean) As String
                Dim sFn As String = "Public Function ExecuteDo_One(List(Of STU_CollectInfo), ArrayList, Boolean) As String"

                Try
                    m_al_DiagData = r_al_diag

                    If m_DbCn Is Nothing Then m_DbCn = GetDbConnection()

                    m_DbTran = m_DbCn.BeginTransaction()

                    COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                    '> get bcno
                    Dim sBcNo As String = GetNewBcNo(r_listcollData)

                    If sBcNo = "" Then
                        m_DbTran.Rollback()

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

                        iRowsO = SetOrderChgCollState(css, rbToColl, m_DbCn, m_DbTran)
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

                        iRowsL = SetOrderChgCollState(css, rbToColl, m_DbCn, m_DbTran)
                    End If

                    If iRowsO + iRowsL < 1 Then 'If iRowsO + iRowsL <> r_listcollData.Count Then
                        m_DbTran.Rollback()

                        Return ""
                    End If

                    '> add collect info -> lj011m
                    iRows = ExecuteDo_One_AddColl(sBcNo, r_listcollData, rbToColl)

                    If iRows = 0 Then
                        m_DbTran.Rollback()

                        Return ""
                    End If

                    m_DbTran.Commit()

                    Return sBcNo

                Catch ex As Exception
                    If m_DbTran IsNot Nothing Then
                        If m_DbTran.Connection IsNot Nothing Then
                            m_DbTran.Rollback()
                        End If
                    End If

                    SetError(Err.Number, Err.Description)
                    Fn.log(msFile + sFn, Err)
                Finally
                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try
            End Function

            Public Function ExecuteDo_One(ByVal r_listcollData As List(Of STU_CollectInfo), ByVal r_al_diag As ArrayList, ByVal rbToColl As Boolean, ByVal rbTk As Boolean) As String
                Dim sFn As String = "Public Function ExecuteDo_One(List(Of STU_CollectInfo), ArrayList,  Boolean, Boolean) As String"

                Try
                    m_al_DiagData = r_al_diag

                    m_DbCn = GetDbConnection()
                    m_DbTran = m_DbCn.BeginTransaction()

                    COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                    '> get bcno
                    Dim sBcNo As String = GetNewBcNo(r_listcollData)

                    If sBcNo = "" Then
                        m_DbTran.Rollback()

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
                            .CollDt = r_listcollData.Item(0).COLLDT.Replace("-", "").Replace(":", "").Replace(" ", "")
                            .OwnGbn = "O"
                            .RegNo = sRegNo
                            .TotFkOcs = sFkOcsO
                            .IOGBN = sIOGBN
                            .FKOCSTORDCD = sFKOcsTOrdCdO
                        End With

                        iRowsO = SetOrderChgCollState(css, rbToColl, m_DbCn, m_DbTran)
                    End If

                    If sFkOcsL.Length > 0 Then
                        With css
                            .BcNo = sBcNo
                            .CollDt = r_listcollData.Item(0).COLLDT.Replace("-", "").Replace(":", "").Replace(" ", "")
                            .OwnGbn = "P"
                            .RegNo = sRegNo
                            .TotFkOcs = sFkOcsL
                            .IOGBN = sIOGBN
                            .FKOCSTORDCD = sFKOcsTOrdCdL
                        End With

                        iRowsL = SetOrderChgCollState(css, rbToColl, m_DbCn, m_DbTran)
                    End If

                    If iRowsO + iRowsL <> r_listcollData.Count Then
                        m_DbTran.Rollback()

                        Return ""
                    End If

                    '> add collect info -> rlctinfo(채혈)
                    iRows = ExecuteDo_One_AddColl(sBcNo, r_listcollData, rbToColl)

                    If iRows = 0 Then
                        m_DbTran.Rollback()

                        Return ""
                    End If


                    'If rbTk Or (r_listcollData.Item(0).POCTYN = "1" And r_listcollData.Item(0).IOGBN = "O") Then
                    '    '> 접수작업까지 처리
                    '    iRows = ExecuteDo_One_AddTake(sBcNo, r_listcollData.Item(0).COLLID)

                    '    If iRows = 0 Then
                    '        m_DbTran.Rollback()

                    '        Return ""
                    '    End If
                    'End If

                    m_DbTran.Commit()

                    Return sBcNo

                Catch ex As Exception
                    m_DbTran.Rollback()
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    m_DbTran.Dispose() : m_DbTran = Nothing
                    If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                    m_DbCn.Dispose() : m_DbCn = Nothing

                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try
            End Function


            Public Function ExecuteDo_One_Coll(ByVal r_listcollData As List(Of STU_CollectInfo), ByVal r_al_diag As ArrayList, _
                                               ByVal rbToColl As Boolean, ByVal rbTk As Boolean) As String
                Dim sFn As String = "Public Function ExecuteDo_One_Coll(List(Of STU_CollectInfo), ArrayList, Boolean) As String"

                Try
                    m_al_DiagData = r_al_diag

                    m_DbCn = GetDbConnection()
                    m_DbTran = m_DbCn.BeginTransaction()

                    COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                    '> get bcno
                    Dim sBcNo As String = GetNewBcNo(r_listcollData)

                    If sBcNo = "" Then
                        m_DbTran.Rollback()

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

                        iRowsO = SetOrderChgCollState(css, rbToColl, m_DbCn, m_DbTran)
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

                        iRowsL = SetOrderChgCollState(css, rbToColl, m_DbCn, m_DbTran)
                    End If

                    If iRowsO + iRowsL <> r_listcollData.Count Then
                        m_DbTran.Rollback()

                        Return ""
                    End If

                    '> add collect info -> lj011m
                    iRows = ExecuteDo_One_AddColl(sBcNo, r_listcollData, rbToColl)

                    If iRows = 0 Then
                        m_DbTran.Rollback()

                        Return ""
                    End If

                    m_DbTran.Commit()

                    Return sBcNo

                Catch ex As Exception
                    m_DbTran.Rollback()
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    m_DbTran.Dispose() : m_DbTran = Nothing
                    If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                    m_DbCn.Dispose() : m_DbCn = Nothing


                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try
            End Function

            Public Function ExecuteDo_One(ByVal r_listcollData As List(Of STU_CollectInfo), ByVal r_al_diag As ArrayList, ByVal rbToColl As Boolean, ByVal rbTk As Boolean, ByVal rbBnk As Boolean) As String
                Dim sFn As String = "Public Function ExecuteDo_One(List(Of STU_CollectInfo), ArrayList, Boolean Boolean) As String"

                Try
                    m_al_DiagData = r_al_diag

                    m_DbCn = GetDbConnection()
                    m_DbTran = m_DbCn.BeginTransaction()

                    COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                    '> get bcno
                    Dim sBcNo As String = GetNewBcNo(r_listcollData)

                    If sBcNo = "" Then
                        m_DbTran.Rollback()

                        Return ""
                    End If

                    Dim sRegNo As String = ""
                    Dim sIOGBN As String = ""
                    Dim sFkOcsO As String = "" : Dim sFKOcsTOrdCd As String = ""
                    Dim sFkOcsL As String = "" : Dim sFKOcsTOrdCdL As String = ""
                    Dim iRows As Integer = 0
                    Dim iRowsO As Integer = 0
                    Dim iRowsL As Integer = 0

                    For i As Integer = 1 To r_listcollData.Count
                        Dim collData As STU_CollectInfo = r_listcollData.Item(i - 1)

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

                        iRowsO = SetOrderChgCollState(css, rbToColl, m_DbCn, m_DbTran)
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

                        iRowsL = SetOrderChgCollState(css, rbToColl, m_DbCn, m_DbTran)
                    End If

                    If iRowsO + iRowsL <> r_listcollData.Count Then
                        m_DbTran.Rollback()

                        Return ""
                    End If

                    '> add collect info -> lj011m
                    iRows = ExecuteDo_One_AddColl(sBcNo, r_listcollData, rbToColl)

                    If iRows = 0 Then
                        m_DbTran.Rollback()

                        Return ""
                    End If

                    m_DbTran.Commit()

                    Return sBcNo

                Catch ex As Exception
                    m_DbTran.Rollback()
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    m_DbTran.Dispose() : m_DbTran = Nothing
                    If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                    m_DbCn.Dispose() : m_DbCn = Nothing

                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try
            End Function

            Protected Function ExecuteDo_One_AddColl(ByVal rsBcNo As String, ByVal r_listcollData As List(Of STU_CollectInfo), ByVal rbToColl As Boolean) As Integer
                Dim sFn As String = "Protected Function ExecuteDo_One_AddColl(String, List(Of STU_CollectInfo), Boolean) As Integer"

                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbParam As New OracleParameter

                Dim iRow As Integer = 0
                Dim iRows As Integer = 0

                Try
                    With dbCmd
                        .Connection = m_DbCn

                        If m_DbTran IsNot Nothing Then
                            If m_DbTran.Connection IsNot Nothing Then
                                .Transaction = m_DbTran
                            End If
                        End If

                        .CommandType = CommandType.Text

                        For ix As Integer = 1 To r_listcollData.Count
                            sSql = ""
                            sSql += "INSERT INTO fkpis..rlctinfo("
                            sSql += "            barcdno, patno, ocs_key1, ocs_key2, orddate, orddttm, inspcd, spccd, rcvstatcd, reqsdptcd, prscrtdrid,"
                            sSql += "            gendrid, inoutcd, blduserid, blddttm, fkocs)"
                            sSql += "    VALUES( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                            sSql += "            ?, ?, ?, ?, ?)"

                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                            .Parameters.Add("regno", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).REGNO

                            If r_listcollData.Item(ix - 1).FKOCS.IndexOf("/"c) >= 0 Then
                                .Parameters.Add("ocskey1", OracleDbType.Int64).Value = r_listcollData.Item(ix - 1).FKOCS.Split("/"c)(0)
                                .Parameters.Add("ocskey2", OracleDbType.Int64).Value = r_listcollData.Item(ix - 1).REGNO.Split("/"c)(1)
                            Else
                                .Parameters.Add("ocskey1", OracleDbType.Int64).Value = r_listcollData.Item(ix - 1).FKOCS.Substring(0, 8)
                                .Parameters.Add("ocskey2", OracleDbType.Int64).Value = r_listcollData.Item(ix - 1).FKOCS.Substring(9, 4)
                            End If

                            .Parameters.Add("orddt", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).ORDDT.Replace("-", "")
                            .Parameters.Add("ordtm", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).ORDDT
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).TCLSCD
                            .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).SPCCD

                            .Parameters.Add("spcflg", OracleDbType.Varchar2).Value = "B"

                            .Parameters.Add("deptcd", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).DEPTCD
                            .Parameters.Add("orddr", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).DOCTORCD
                            .Parameters.Add("gendr", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).GENDRCD
                            .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).IOGBN

                            .Parameters.Add("collid", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).COLLID
                            .Parameters.Add("colldt", OracleDbType.Date).Value = r_listcollData.Item(ix - 1).COLLDT

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_listcollData.Item(ix - 1).FKOCS

                            iRow = .ExecuteNonQuery()

                            iRows += iRow
                        Next
                    End With

                    Return iRows

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    If dbCmd IsNot Nothing Then
                        dbCmd.Dispose()
                        dbCmd = Nothing
                    End If

                End Try
            End Function

            Protected Function ExecuteDo_One_AddTake(ByVal rsBcNo As String, ByVal rsUsrId As String) As Integer
                Dim sFn As String = "Protected Function ExecuteDo_One_AddTake(String, string) As Integer"

                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbParam As New OracleParameter

                Dim iRow As Integer = 0

                Try

                    Dim sErrVal As String = ""

                    With dbCmd
                        .Connection = m_DbCn

                        If m_DbTran IsNot Nothing Then
                            If m_DbTran.Connection IsNot Nothing Then
                                .Transaction = m_DbTran
                            End If
                        End If

                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "pro_ack_exe_take_ocs_pis"

                        .Parameters.Clear()
                        .Parameters.Add("bcno  ", OracleDbType.Varchar2).Value = rsBcNo
                        .Parameters.Add("wknoyn", OracleDbType.Varchar2).Value = "N"
                        .Parameters.Add("tkid  ", OracleDbType.Varchar2).Value = rsUsrId
                        .Parameters.Add("ip    ", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("retval", OracleDbType.Varchar2, 4000)
                        .Parameters("retval").Direction = ParameterDirection.InputOutput
                        .Parameters("retval").Value = sErrVal

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
                        dbCmd.Dispose()
                        dbCmd = Nothing
                    End If

                End Try
            End Function

            Protected Function ExecuteDo_One_GetCollDtPrtBcNo(ByVal rsBcNo As String) As DataTable
                Dim sFn As String = "Protected Function ExecuteDo_One_AddEnt(String, ArrayList) As Integer"

                Dim sSql As String = ""
                Dim iRow As Integer = 0

                Dim dbCmd As New OracleCommand

                Try
                    With dbCmd
                        .Connection = m_DbCn

                        If m_DbTran IsNot Nothing Then
                            If m_DbTran.Connection IsNot Nothing Then
                                .Transaction = m_DbTran
                            End If
                        End If

                        .CommandType = CommandType.Text

                        sSql = ""
                        sSql += "SELECT fn_ack_date_str(fn_ack_sysdate, 'yyyy-mm-dd hh24:mi:ss') sysdt, fn_ack_get_bcno_prt_pis(?) prtbcno FROM DUAL"

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    End With

                    Dim lisDbDa As New OracleDataAdapter(dbCmd)

                    Dim dt As New DataTable

                    lisDbDa.Fill(dt)

                    Return dt

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    If dbCmd IsNot Nothing Then
                        dbCmd.Dispose()
                        dbCmd = Nothing
                    End If

                End Try
            End Function

            Public Function ExecuteDo_Comment(ByVal r_listcollData As List(Of STU_CollectInfo)) As Boolean
                Dim sFn As String = "Public Sub ExecuteDo_Comment(List(Of STU_CollectInfo))"

                Try
                    If m_DbCn Is Nothing Then m_DbCn = GetDbConnection()

                    m_DbTran = m_DbCn.BeginTransaction()

                    COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                    '> set laborder
                    Dim iRows As Integer = 0

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

                        Dim iRow As Integer = SetOrderChgLisCmt(css, m_DbCn, m_DbTran)

                        iRows += iRow
                    Next

                    If iRows > 0 Then
                        m_DbTran.Commit()

                        Return True
                    Else
                        m_DbTran.Rollback()

                        Return False
                    End If

                Catch ex As Exception
                    m_DbTran.Rollback()
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


                Finally
                    m_DbTran.Dispose() : m_DbTran = Nothing
                    If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                    m_DbCn.Dispose() : m_DbCn = Nothing

                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try
            End Function

            ' 채혈일시 등록
            Public Function ExecuteDo_CollDt(ByVal rsBcNo As String, ByVal rsUsrId As String, ByVal rbTakeYn As Boolean) As Boolean
                Dim sFn As String = "Protected Function ExecuteDo_One_AddTake(String, List(Of STU_CollectInfo)) As Integer"

                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbParam As New OracleParameter

                Dim iRow As Integer = 0

                Try
                    m_DbCn = GetDbConnection()
                    m_DbTran = m_DbCn.BeginTransaction()

                    COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                    dbCmd = New OracleCommand
                    Dim sErrVal As String = ""

                    With dbCmd
                        .Connection = m_DbCn
                        .Transaction = m_DbTran

                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "pro_ack_exe_collector_colldt_pis"

                        .Parameters.Clear()
                        .Parameters.Add("bcno  ", OracleDbType.Varchar2).Value = rsBcNo
                        .Parameters.Add("usrid ", OracleDbType.Varchar2).Value = rsUsrId
                        .Parameters.Add("ip    ", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("retval", OracleDbType.Varchar2, 4000)
                        .Parameters("retval").Direction = ParameterDirection.InputOutput
                        .Parameters("retval").Value = sErrVal

                        .ExecuteNonQuery()

                        sErrVal = .Parameters(3).Value.ToString
                    End With

                    If IsNumeric(sErrVal.Substring(0, 2)) Then
                        If sErrVal.Substring(0, 2) = "00" Then
                            Dim iRows As Integer = ExecuteDo_One_AddTake(rsBcNo, rsUsrId)

                            If iRows = 0 Then
                                m_DbTran.Rollback()
                                Return False
                            End If
                            m_DbTran.Commit()
                            Return True
                        Else
                            m_DbTran.Rollback()
                            Return False
                        End If
                    Else
                        m_DbTran.Rollback()
                        Return False
                    End If

                Catch ex As Exception
                    m_DbTran.Rollback()
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

                Finally
                    If dbCmd IsNot Nothing Then dbCmd = Nothing
                    m_DbTran.Dispose() : m_DbTran = Nothing
                    If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                    m_DbCn.Dispose() : m_DbCn = Nothing

                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try

            End Function

            Public Function GetNewBcNo(ByVal r_listcollData As List(Of STU_CollectInfo)) As String
                Dim sFn As String = "Public Function GetNewBcNo(List(Of STU_CollectInfo)) As String"

                Dim sSql As String = "pro_ack_exe_seqno_bc_pis"

                Dim dbCmd As New OracleCommand
                Dim dbParam As New OracleParameter  'New DBORA.DbParrameter

                Try

                    Dim iSeqNo As Integer = 0

                    With dbCmd
                        .Connection = m_DbCn

                        If m_DbTran IsNot Nothing Then
                            If m_DbTran.Connection IsNot Nothing Then
                                .Transaction = m_DbTran
                            End If
                        End If

                        .CommandType = CommandType.StoredProcedure
                        .CommandText = sSql

                        .Parameters.Clear()

                        '<
                        dbParam = New OracleParameter()

                        With dbParam
                            .ParameterName = "yyyymmdd" : .DbType = DbType.String : .Direction = ParameterDirection.Input : .Value = r_listcollData.Item(0).COLLDT.Replace("-", "").Substring(0, 8)
                        End With

                        .Parameters.Add(dbParam)

                        dbParam = Nothing
                        '>

                        '<
                        dbParam = New OracleParameter()

                        With dbParam
                            .ParameterName = "gbn" : .DbType = DbType.String : .Direction = ParameterDirection.Input : .Value = "COL"
                        End With

                        .Parameters.Add(dbParam)

                        dbParam = Nothing
                        '>

                        '<
                        dbParam = New OracleParameter()

                        With dbParam
                            .ParameterName = "seqno" : .DbType = DbType.Int32 : .Direction = ParameterDirection.InputOutput : .Value = iSeqNo
                        End With

                        .Parameters.Add(dbParam)

                        dbParam = Nothing
                        '>

                        .ExecuteNonQuery()
                    End With

                    Dim sBcNo As String = ""

                    iSeqNo = CInt(dbCmd.Parameters("seqno").Value)

                    If iSeqNo < 1 Or iSeqNo > PRG_CONST.Max_BcNoSeq + 90000 Then
                        sBcNo = ""
                    Else
                        sBcNo = r_listcollData.Item(0).COLLDT.Replace("-", "").Substring(0, 8) + iSeqNo.ToString("D5")
                    End If

                    msBcNoBuf = sBcNo

                    Return sBcNo

                Catch ex As Exception
                    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


                Finally
                    If dbCmd IsNot Nothing Then
                        dbCmd.Dispose()
                        dbCmd = Nothing
                    End If

                End Try
            End Function

        End Class
#End Region

#Region " 채혈취소 "
        Public Class Coll_Cancel_ITEM
            Public BCNO As String = ""
            Public TESTCD As String = ""
            Public SPCCD As String = ""
            Public TCDGBN As String = ""
            Public IOGBN As String = ""
            Public FKOCS As String = ""
            Public TORDCD As String = "" '< yjlee 
            Public OWNGBN As String = ""
            Public CANCELRMK As String = ""

            Public REGNO As String = ""
            Public SPCFLG As String = ""

            Public Sub New()
                MyBase.New()
            End Sub
        End Class

        Public Class Exec_Canecl
            Inherits ClassErr

            Private Const msFile As String = "File : CGDA_C.vb, Class : LISAPP.APP_C.DB_Collect" & vbTab

            Private m_DbCn As OracleConnection
            Private m_DbTran As OracleTransaction
            Private mlCancelTItem As New ArrayList
            Private msCancelRMK As String = ""
            Private msCancelCd As String = ""

            Private meCancel As enumCANCEL
            Private msUserId As String = ""
            Private msSrvDate As String = ""

            Private mNotApplyMTS As Boolean = False

            Public WriteOnly Property CancelRMK() As String
                Set(ByVal Value As String)
                    msCancelRMK = Value
                End Set
            End Property

            Public WriteOnly Property CancelCd() As String
                Set(ByVal Value As String)
                    msCancelCd = Value
                End Set
            End Property

            Public WriteOnly Property NotApplyMTS() As Boolean
                Set(ByVal Value As Boolean)
                    mNotApplyMTS = Value
                End Set
            End Property

            Public Sub New()
                m_DbCn = GetDbConnection()
                m_DbTran = m_DbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            End Sub

            Public Sub New(ByVal r_dbCn As OracleConnection, ByVal r_dbTran As OracleTransaction)
                m_DbCn = r_dbCn
                m_DbTran = r_dbTran

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
            End Sub

            Public Function ExecuteDo(ByVal r_o_CancelInfo As Coll_Cancel_ITEM, ByVal rsUsrId As String) As String
                Dim sFn As String = "Public Sub ExecuteDo(String, String) As String"

                Try
                    meCancel = enumCANCEL.채혈취소
                    msUserId = rsUsrId
                    msSrvDate = (New ServerDateTime).GetDateTime24()
                    msSrvDate = msSrvDate.Replace("-", "").Replace(":", "").Replace(" ", "")

                    Dim sRet As String = ""
                    Dim iRet As Integer = 0
                    Dim sErrMsg As String = ""
                    Dim cos As New OcsLink.ChgOcsState

                    With cos
                        .RegNo = r_o_CancelInfo.REGNO
                        .BcNo = r_o_CancelInfo.BCNO
                        .OwnGbn = r_o_CancelInfo.OWNGBN
                        .IOGBN = r_o_CancelInfo.IOGBN
                        .TotFkOcs = r_o_CancelInfo.FKOCS
                        .LabCmt = msCancelRMK

                        Select Case meCancel
                            Case enumCANCEL.채혈접수취소 : .CancelGbn = "0"
                            Case enumCANCEL.채혈취소 : .CancelGbn = "1"
                            Case enumCANCEL.접수취소 : .CancelGbn = "2"
                            Case enumCANCEL.REJECT : .CancelGbn = "3"
                            Case enumCANCEL.BLOOD_REJECT : .CancelGbn = "4"
                            Case enumCANCEL.일괄채혈취소 : .CancelGbn = "5"
                            Case enumCANCEL.부적합검등록 : .CancelGbn = "6"
                        End Select
                    End With

                    sErrMsg = PISAPP.DPIS01.OcsLink.Ord.SetOrderChgCancelState(cos, m_DbCn, m_DbTran)
                    If sErrMsg <> "" Then
                        m_DbTran.Rollback()
                        Return "데이블 [MTS0001]에서 오류가 발생 했습니다."
                    End If

                    If fnExe_rlctinfo(r_o_CancelInfo) < 1 Then
                        m_DbTran.Rollback()
                        Return "테이블 [RCV_INFO]에서 오류가 발생 했습니다.."
                    End If


                    m_DbTran.Commit()

                    Return ""

                Catch ex As Exception
                    m_DbTran.Rollback()

                    SetError(Err.Number, Err.Description)
                    Fn.log(msFile & sFn, Err)

                    Return ex.Message
                Finally
                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try

            End Function

            ' 검체정보 취소( LJ010M )
            Private Function fnExe_rlctinfo(ByVal r_o_CancelInfo As Coll_Cancel_ITEM, Optional ByVal rsSpcFlg As String = "") As Integer
                Dim sFn As String = "Private Function fnExe_rlctinfo(J01.clsCancelTItem, [String]) As integer"

                Try
                    Dim dbCmd As New OracleCommand
                    Dim sSql As String = ""

                    sSql = ""
                    sSql += "UPDATE rlctinfo"
                    sSql += "   SET rcvstatcd = 'C',"
                    sSql += "       cncldt    = SUBSTR(fn_ack_sysdate, 1, 8),"
                    sSql += "       cncldttm  = SYSDATE,"
                    sSql += "       cnclid    = ?,"
                    sSql += "       cnclyn    = 'Y',"
                    sSql += "       cnclrsn   = ?"
                    sSql += " WHERE barcdno   = ?"

                    With dbCmd
                        .Connection = m_DbCn

                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()

                        .Parameters.Add("bcno   ", OracleDbType.Varchar2).Value = r_o_CancelInfo.BCNO
                        .Parameters.Add("usrid  ", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("cnclrsn", OracleDbType.Varchar2).Value = r_o_CancelInfo.CANCELRMK

                        Return .ExecuteNonQuery()
                    End With


                Catch ex As Exception
                    Fn.log(msFile + sFn, Err)
                    Throw (New Exception(ex.Message, ex))

                    Return 0
                End Try
            End Function

        End Class

#End Region
#Region " 바코드 출력 : Class BCNO_Print "
        Public Class BCPrinter
            Private Const msFile As String = "File : CGDA_C.vb, Class : PRTAPP.APP_BC.BCPrinter" & vbTab
            Private mlPRTInfo As New ArrayList

            Private msXmlDir As String = System.Windows.Forms.Application.StartupPath & "\XML"
            Private msXmlFile As String = ""

            Private miSelPRTID As Integer = 0  ' 선택된프린터

            Private msMsg As String
            Private miCnt As Integer

            Private msFldSep As String = CStr(Chr(32))
            Private miMaxLenCmt As Integer = 34
            Private msSymbolMore As String = "..."

            Public Sub New(ByVal rsLoadFrm As String)
                MyBase.New()

                msXmlFile = msXmlDir + "\" + rsLoadFrm & "_BCPrinterINFO.XML"

                ' 생성시 바코드프린터정보 읽기
                sbReadPrtInfo()
            End Sub

            ' 바코드 프린터정보 읽기( Client 기준 ) 
            Private Sub sbReadPrtInfo()
                Dim sFn As String = ""

                Try
                    If Dir(msXmlDir, FileAttribute.Directory) = "" Then MkDir(msXmlDir)

                    If Dir(msXmlFile) > "" Then
                        Dim XmlRead As Xml.XmlTextReader

                        XmlRead = New Xml.XmlTextReader(msXmlFile)
                        While XmlRead.Read

                            XmlRead.ReadStartElement("ROOT")
                            Do While (True)
                                XmlRead.ReadStartElement("PRTINFO")
                                Dim PRTInfo As New clsPRTInfo
                                With PRTInfo
                                    .PRTID = XmlRead.ReadElementString("PRTID")
                                    .PRTNM = XmlRead.ReadElementString("PRTNM")
                                    .OUTIP = XmlRead.ReadElementString("OUTIP")
                                    .OUTPORT = XmlRead.ReadElementString("OUTPORT")
                                    .SUPPORTIP = XmlRead.ReadElementString("SUPPORTIP")
                                    .SELECTED = XmlRead.ReadElementString("SELECTED")
                                    .IOPORT = XmlRead.ReadElementString("IOPORT")
                                    .LEFTMARGIN = XmlRead.ReadElementString("LEFTMARGIN")
                                    .PRTTYPE = XmlRead.ReadElementString("PRTTYPE")
                                    ' 선택된 프린터 설정
                                    If .SELECTED = "1" Then miSelPRTID = CInt(.PRTID)
                                End With
                                mlPRTInfo.Add(PRTInfo)
                                XmlRead.ReadEndElement()
                                XmlRead.Read()

                                If XmlRead.Name <> "PRTINFO" Then Exit Do
                            Loop
                            XmlRead.Close()
                        End While

                    Else
                        Dim moBCPRT As New BCPRT01.BCPRT
                        For intCnt As Integer = 0 To moBCPRT.BCPRINTERS.Count - 1
                            Dim PRTInfo As New clsPRTInfo
                            With PRTInfo
                                .PRTID = CType(moBCPRT.BCPRINTERS(intCnt), BCPRT01.BCPRINTER_CFG).PrinterID.ToString
                                .PRTNM = CType(moBCPRT.BCPRINTERS(intCnt), BCPRT01.BCPRINTER_CFG).PrinterName.ToString
                                .SUPPORTIP = IIf(CType(moBCPRT.BCPRINTERS(intCnt), BCPRT01.BCPRINTER_CFG).SupportTCPIP = True, "1", "").ToString
                                .OUTIP = ""
                                .OUTPORT = ""
                                If .SUPPORTIP = "1" Then
                                    .OUTIP = "127.0.0.1"
                                    .OUTPORT = CType(moBCPRT.BCPRINTERS(intCnt), BCPRT01.BCPRINTER_CFG).PortNo.ToString
                                End If
                                .SELECTED = ""
                                .SELECTED = ""
                                .IOPORT = ""
                                .LEFTMARGIN = ""
                                .TOPMARGIN = ""
                            End With
                            mlPRTInfo.Add(PRTInfo)
                        Next

                        For intCnt As Integer = 0 To 2

                        Next

                        WritePrtInfo()
                    End If

                    mlPRTInfo.TrimToSize()

                Catch ex As Exception
                    Fn.log(msFile & sFn, Err)

                End Try

            End Sub

            ' 수정된 바코드 프린터정보 쓰기
            Public Sub WritePrtInfo()
                Dim sFn As String = ""

                Try
                    If mlPRTInfo.Count > 0 Then
                        If Dir(msXmlDir, FileAttribute.Directory) = "" Then MkDir(msXmlDir)

                        Dim XmlWrite As Xml.XmlTextWriter = Nothing
                        XmlWrite = New Xml.XmlTextWriter(msXmlFile, System.Text.Encoding.GetEncoding("utf-8"))
                        With XmlWrite
                            .Formatting = Xml.Formatting.Indented
                            .Indentation = 4
                            .IndentChar = " "c
                            .WriteStartDocument(False)

                            .WriteStartElement("ROOT")
                            For intRow As Integer = 0 To mlPRTInfo.Count - 1
                                .WriteStartElement("PRTINFO")
                                .WriteElementString("PRTID", CType(mlPRTInfo(intRow), clsPRTInfo).PRTID)
                                .WriteElementString("PRTNM", CType(mlPRTInfo(intRow), clsPRTInfo).PRTNM)
                                .WriteElementString("OUTIP", CType(mlPRTInfo(intRow), clsPRTInfo).OUTIP)
                                .WriteElementString("OUTPORT", CType(mlPRTInfo(intRow), clsPRTInfo).OUTPORT)
                                .WriteElementString("SUPPORTIP", CType(mlPRTInfo(intRow), clsPRTInfo).SUPPORTIP)
                                .WriteElementString("SELECTED", CType(mlPRTInfo(intRow), clsPRTInfo).SELECTED)
                                .WriteElementString("IOPORT", CType(mlPRTInfo(intRow), clsPRTInfo).IOPORT)
                                .WriteElementString("LEFTMARGIN", CType(mlPRTInfo(intRow), clsPRTInfo).LEFTMARGIN)
                                .WriteElementString("PRTTYPE", CType(mlPRTInfo(intRow), clsPRTInfo).PRTTYPE)
                                .WriteEndElement()
                            Next
                            .WriteEndElement()
                            .Close()
                        End With

                    Else
                        If Dir(msXmlFile) <> "" Then Kill(msXmlFile)
                    End If

                Catch ex As Exception
                    Fn.log(msFile & sFn, Err)

                End Try

            End Sub

            ' PRTID가 없으면 선택된 프린터
            Public ReadOnly Property GetInfo(Optional ByVal aiPRTID As Integer = -1) As clsPRTInfo
                Get
                    If aiPRTID < 0 Then
                        GetInfo = CType(mlPRTInfo(miSelPRTID), clsPRTInfo)
                    Else
                        GetInfo = CType(mlPRTInfo(aiPRTID), clsPRTInfo)
                    End If
                End Get
            End Property

            ' 선택가능 프린터 수
            Public ReadOnly Property GetCnt() As Integer
                Get
                    GetCnt = mlPRTInfo.Count
                End Get
            End Property

            ' 출력프린터 설정
            Public Property PrtID() As Integer
                Get
                    PrtID = miSelPRTID
                End Get
                Set(ByVal Value As Integer)
                    miSelPRTID = Value

                    For intCnt As Integer = 0 To mlPRTInfo.Count - 1
                        CType(mlPRTInfo(intCnt), clsPRTInfo).SELECTED = ""
                        If miSelPRTID = intCnt Then
                            CType(mlPRTInfo(intCnt), clsPRTInfo).SELECTED = "1"
                        End If
                    Next
                End Set
            End Property


            ' 선택한 프린터 IP설정
            Public WriteOnly Property SetOutIP(Optional ByVal aiPRTID As Integer = -1) As String
                Set(ByVal Value As String)
                    If aiPRTID = -1 Then
                        CType(mlPRTInfo(miSelPRTID), clsPRTInfo).OUTIP = Value
                    Else
                        CType(mlPRTInfo(aiPRTID), clsPRTInfo).OUTIP = Value
                    End If
                End Set
            End Property

            '-- 2007-10-16 YOOEJ ADD
            ' 선택한 프린터 포트설정
            Public WriteOnly Property SetIOPort(Optional ByVal aiPRTID As Integer = -1) As String
                Set(ByVal Value As String)
                    If aiPRTID = -1 Then
                        CType(mlPRTInfo(miSelPRTID), clsPRTInfo).IOPORT = Value
                    Else
                        CType(mlPRTInfo(aiPRTID), clsPRTInfo).IOPORT = Value
                    End If
                End Set
            End Property

            '-- 2007-10-16 YOOEJ ADD
            ' 선택한 인쇄 마진 설정
            Public WriteOnly Property SetLeftMargin(Optional ByVal aiPRTID As Integer = -1) As String
                Set(ByVal Value As String)
                    If aiPRTID = -1 Then
                        CType(mlPRTInfo(miSelPRTID), clsPRTInfo).LEFTMARGIN = Value
                    Else
                        CType(mlPRTInfo(aiPRTID), clsPRTInfo).LEFTMARGIN = Value
                    End If
                End Set
            End Property

            '-- 2007-10-19 YOOEJ ADD
            ' 선택한 인쇄 마진 설정
            Public WriteOnly Property SetTopMargin(Optional ByVal aiPRTID As Integer = -1) As String
                Set(ByVal Value As String)
                    If aiPRTID = -1 Then
                        CType(mlPRTInfo(miSelPRTID), clsPRTInfo).TOPMARGIN = Value
                    Else
                        CType(mlPRTInfo(aiPRTID), clsPRTInfo).TOPMARGIN = Value
                    End If
                End Set
            End Property


            '-- 2008-12-23 yjlee
            ' 프린트타입 설정
            Public WriteOnly Property SetPrtType(Optional ByVal aiPRTID As Integer = -1) As String
                Set(ByVal Value As String)
                    If aiPRTID = -1 Then
                        CType(mlPRTInfo(miSelPRTID), clsPRTInfo).PRTTYPE = Value
                    Else
                        CType(mlPRTInfo(aiPRTID), clsPRTInfo).PRTTYPE = Value
                    End If
                End Set
            End Property

            Private maPrtData As New ArrayList
            Private mbFirst As Boolean
            Private mTrd As System.Threading.Thread

            Public Sub PrintDo(ByVal ra_PrtData As ArrayList, _
                                  ByVal rbFirst As Boolean, ByVal rsPrinterName As String)
                Dim sFn As String = "Public Sub PrintDo(String, Integer, Boolean, String)"

                Try
                    maPrtData = ra_PrtData
                    mbFirst = rbFirst

                    sbPrint(rsPrinterName)

                Catch ex As Exception
                    Fn.log(msFile & sFn, Err)

                End Try

            End Sub


            ' 바코드 출력하기
            Public Sub PrintDo(ByVal ra_PrtData As ArrayList, ByVal rbFirst As Boolean)
                Dim sFn As String = "Public Sub PrintDo(ArrayList,  Boolean)"

                Try
                    maPrtData = ra_PrtData
                    mbFirst = rbFirst

                    sbPrint()

                Catch ex As Exception
                    Fn.log(msFile & sFn, Err)

                End Try

            End Sub


            Public Sub PrintDoBarcode(ByVal r_al_BcNos As ArrayList, ByVal riCount As Integer, _
                                         Optional ByVal roForm As String = "", Optional ByVal rbFirst As Boolean = False)
                Dim sFN As String = ""

                Try
                    Dim sBcNos As String = ""
                    Dim arlBcData As New ArrayList

                    For ix As Integer = 0 To r_al_BcNos.Count - 1
                        Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_BcNos(ix), List(Of STU_CollectInfo))
                        Dim bpi As STU_BCPRTINFO = fnFind_BcPrtItem(listcollData)

                        arlBcData.Add(bpi)

                        If sBcNos.Length > 0 Then sBcNos += ", "
                        sBcNos += bpi.BCNO.Replace("-", "").Trim()
                    Next

                    Dim bReturn As Boolean = False

                    Call (New BCPrinter(roForm)).PrintDo(arlBcData, rbFirst)

                Catch ex As Exception

                End Try
            End Sub

            Public Sub PrintDoBarcode(ByVal r_al_BcNos As ArrayList, ByVal riCount As Integer, _
                                         ByVal roForm As String, ByVal rbFirst As Boolean, ByVal rsPrinterName As String)
                Dim sFN As String = ""

                Try
                    Dim sBcNos As String = ""
                    Dim alBcData As New ArrayList

                    For i As Integer = 1 To r_al_BcNos.Count
                        Dim sPrtMsgOne As String = ""
                        Dim sPrtCntOne As String = ""

                        Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_BcNos(i - 1), List(Of STU_CollectInfo))

                        Dim bpi As STU_BCPRTINFO = fnFind_BcPrtItem(listcollData)

                        alBcData.Add(bpi)

                        If sBcNos.Length > 0 Then sBcNos += ","
                        sBcNos += bpi.BCNO.Replace("-", "").Trim()

                    Next

                    Dim bReturn As Boolean = False

                    Call (New BCPrinter(roForm)).PrintDo(alBcData, rbFirst)

                Catch ex As Exception

                End Try
            End Sub

            Private Function fnFind_BcPrtItem(ByVal r_listcollData As List(Of STU_CollectInfo)) As STU_BCPRTINFO
                Dim sFn As String = "Private Function fnFind_BcPrtItem(List(Of STU_CollectInfo)) As String"

                Try
                    Dim bpi As New STU_BCPRTINFO

                    With bpi
                        .BCNOPRT = r_listcollData.Item(0).PRTBCNO
                        .REGNO = r_listcollData.Item(0).REGNO
                        .PATNM = r_listcollData.Item(0).PATNM
                        .SEXAGE = r_listcollData.Item(0).SEX + "/" + r_listcollData.Item(0).AGE
                        .BCCLSCD = r_listcollData.Item(0).BCCLSCD
                        If r_listcollData.Item(0).IOGBN = "O" Then
                            .DEPTWARD = r_listcollData.Item(0).DEPTCD
                        Else
                            .DEPTWARD = r_listcollData.Item(0).WARDNO + "/" + r_listcollData.Item(0).ROOMNO
                        End If
                        .IOGBN = r_listcollData.Item(0).IOGBN
                        .BCNO = Fn.BCNO_PIS_View(r_listcollData.Item(0).BCNO)
                        .HREGNO = r_listcollData.Item(0).HREGNO
                        .TUBENM = r_listcollData.Item(0).TUBENMBP

                        Dim sTNmBP As String = ""
                        Dim sTmpTgrpnm As String = ""

                        If .BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Then
                            sTNmBP = r_listcollData.Item(0).TNMBP + msFldSep + r_listcollData.Count.ToString + "unit(s)"
                        Else
                            For r As Integer = 1 To r_listcollData.Count
                                Dim collData As STU_CollectInfo = CType(r_listcollData(r - 1), STU_CollectInfo)

                                Dim sTNmOne As String = collData.TNMBP.Trim

                                If sTNmOne.IndexOf(">") >= 0 Then
                                    sTNmOne = sTNmOne.Substring(0, sTNmOne.IndexOf(">")).Trim
                                End If

                                If sTNmOne.IndexOf("<") >= 0 Then
                                    sTNmOne = sTNmOne.Substring(sTNmOne.IndexOf("<") + 1).Trim
                                End If

                                If sTNmBP.Length > 0 Then sTNmBP += msFldSep

                                If Fn.LengthH(sTNmBP + sTNmOne) > miMaxLenCmt - msSymbolMore.Length Then
                                    If r = r_listcollData.Count Then
                                        If Fn.LengthH(sTNmBP + sTNmOne) > miMaxLenCmt Then
                                            sTNmBP = sTNmBP.Trim + msSymbolMore
                                        Else
                                            sTNmBP += sTNmOne
                                        End If
                                    Else
                                        sTNmBP = sTNmBP.Trim + msSymbolMore
                                    End If

                                    'Exit For
                                Else
                                    sTNmBP += sTNmOne
                                End If

                                If collData.TGRPNM <> "" Then
                                    If sTmpTgrpnm.IndexOf(collData.TGRPNM) < 0 Then
                                        sTmpTgrpnm += collData.TGRPNM
                                    End If
                                End If
                            Next
                        End If

                        .TESTNMS = Fn.PadRightH(sTNmBP, 50)

                        Dim sStat As String = ""

                        For r As Integer = 1 To r_listcollData.Count
                            sStat = r_listcollData.Item(r - 1).STATGBN

                            If sStat <> "" Then Exit For
                        Next

                        '기타1 -> 응급(1) + 병실(9)
                        .EMER = sStat

                        '기타2 -> 검체명(10)
                        .SPCNM = r_listcollData.Item(0).SPCNMBP

                        '기타3 -> 감염정보(10)
                        .INFINFO = r_listcollData.Item(0).INFINFO

                        '기타4 -> 검사그룹(12)
                        .TGRPNM = sTmpTgrpnm

                        For ix As Integer = 0 To r_listcollData.Count - 1
                            If r_listcollData.Item(ix).BCCNT <> "" Then
                                .BCCNT = Fn.PadRightH(r_listcollData.Item(ix).BCCNT, 1) 'Fn.PadRightH(fnFind_BcCrossMatchingCheck(r_listcollData.Item(0).BCNO.Trim().Replace("-", "")), 4)
                            End If
                        Next

                        Dim sRemark As String = SData.fnGet_Remark(r_listcollData.Item(0).BCNO)

                        If sRemark = "" Then
                            .REMARK = SData.fnGet_COMMENT(r_listcollData.Item(0).FKOCS)
                        Else
                            .REMARK = sRemark
                        End If
                    End With

                    Return bpi

                Catch ex As Exception

                    Return Nothing

                End Try
            End Function

            Public Sub PrintDo(ByVal ra_Bcno As ArrayList, ByVal rsBarCnt As String)
                Dim sFn As String = "PrintDo"
                Try
                    Dim alBcData As New ArrayList

                    For ix As Integer = 0 To ra_Bcno.Count - 1
                        Dim sSql As String = ""
                        Dim alParm As New ArrayList
                        Dim sTableNm As String = "lr010m"

                        If PRG_CONST.PART_MicroBio.Contains(ra_Bcno(ix).ToString) Then sTableNm = "lm010m"

                        sSql = ""
                        sSql += "SELECT DISTINCT"
                        sSql += "       j.regno,   j.patnm, j.sex || '/' || j.age sexage, j.bcclscd,"
                        sSql += "       CASE WHEN j.iogbn = 'I' THEN j.deptcd || '/' || j.wardno || '/' || j.roomno ELSE j.deptcd deptinfo,"
                        sSql += "       j.iogbn, f3.spcnmbp, f4.tubenmbp + ' ' + f6.minspcvol tubenmbp, j.statgbn,"
                        sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                        sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                        sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk."
                        sSql += "       REPLACE(fn_ack_get_infection_prt(j.bcno), ',', '/') infinfo,"
                        sSql += "       fn_ack_get_test_nmbp_list(j.bcno) testnms,"
                        sSql += "       fn_ack_get_tgrp_nmbp_list(j.bcno) tgrpnms"
                        sSql += "  FROM lj010m a,  " + sTableNm + " r,  lf060m f6,"
                        sSql += "       lf030m f3, lf040m f4"
                        sSql += " WHERE j.bcno    = ?"
                        sSql += "   AND j.bcno    = r.bcno"
                        sSql += "   AND r.spccd   = f3.spccd"
                        sSql += "   AND r.tkdt   >= f3.usdt"
                        sSql += "   AND r.tkdt   <  f3.uedt"
                        sSql += "   AND r.testcd  = f6.testcd"
                        sSql += "   AND r.spccd   = f6.spccd"
                        sSql += "   AND r.tkdt   >= f6.usdt"
                        sSql += "   AND r.tkdt   <  f6.uedt"
                        sSql += "   AND f6.tubecd = f4.tubecd"
                        sSql += "   AND r.tkdt   >= f4.usdt"
                        sSql += "   AND r.tkdt   <  f4.uedt"

                        alParm.Add(New OracleParameter("bcno", ra_Bcno(ix).ToString()))

                        DbCommand()
                        Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                        If dt.Rows.Count > 0 Then
                            For ix2 As Integer = 0 To dt.Rows.Count - 1

                                Dim objBcInfo As New STU_BCPRTINFO
                                With objBcInfo
                                    .BCCLSCD = dt.Rows(ix2).Item("bcclscd").ToString
                                    .BCCNT = rsBarCnt
                                    .BCNO = dt.Rows(ix2).Item("bcno").ToString
                                    .BCNO_MB = ""
                                    .BCNOPRT = dt.Rows(ix2).Item("prtbcno").ToString
                                    .BCTYPE = ""
                                    .DEPTWARD = dt.Rows(ix2).Item("deptinfo").ToString
                                    .EMER = dt.Rows(ix2).Item("statgbn").ToString
                                    .HREGNO = ""
                                    .INFINFO = dt.Rows(ix2).Item("infinfo").ToString
                                    .IOGBN = dt.Rows(ix2).Item("iogbn").ToString
                                    .PATNM = dt.Rows(ix2).Item("patnm").ToString
                                    .REGNO = dt.Rows(ix2).Item("regno").ToString
                                    .REMARK = dt.Rows(ix2).Item("doctorrmk").ToString
                                    .SEXAGE = dt.Rows(ix2).Item("sexage").ToString
                                    .SPCNM = dt.Rows(ix2).Item("spcnmbp").ToString
                                    .TGRPNM = dt.Rows(ix2).Item("tgrpnm").ToString
                                    .TESTNMS = dt.Rows(ix2).Item("testnms").ToString
                                    .TUBENM = dt.Rows(ix2).Item("tubenmbp").ToString
                                    .XMATCH = ""
                                End With

                                alBcData.Add(objBcInfo)
                            Next
                        End If
                    Next

                Catch ex As Exception
                    Fn.log(msFile + sFn, Err)

                End Try

            End Sub

            Private Sub sbPrint()
                Dim sFn As String = "Private Sub sbPrint()"

                Try

                    Debug.WriteLine(GetInfo.PRTID & ", " & GetInfo.OUTIP & ", " & GetInfo.PRTNM & ", " & GetInfo.OUTPORT)

                    Dim bRetVal As Boolean = (New BCPRT01.BCPRT).BarCodePrtOut_PIS(maPrtData, CInt(GetInfo.PRTID), GetInfo.IOPORT, _
                                                       GetInfo.OUTIP, mbFirst, _
                                                       CInt(Val(GetInfo.LEFTMARGIN)), CInt(Val(GetInfo.TOPMARGIN)), GetInfo.PRTTYPE)

                Catch ex As Exception
                    Fn.log(msFile & sFn, Err)

                End Try

            End Sub

            Private Sub sbPrint(ByVal rsPrinterName As String)
                Dim sFn As String = "Private Sub fnPrint(String)"

                Try
                    Debug.WriteLine(GetInfo.PRTID & ", " & GetInfo.OUTIP & ", " & GetInfo.PRTNM & ", " & rsPrinterName)

                    Dim blnRetVal As Boolean = (New BCPRT01.BCPRT).BarCodePrtOut_PIS(maPrtData, CInt(GetInfo.PRTID), GetInfo.IOPORT, GetInfo.OUTIP, _
                                                       mbFirst, CInt(Val(GetInfo.LEFTMARGIN)), CInt(Val(GetInfo.TOPMARGIN)), _
                                                       GetInfo.PRTTYPE)

                Catch ex As Exception
                    Fn.log(msFile & sFn, Err)

                End Try

            End Sub


#Region " clsPRTInfo "
            Public Class clsPRTInfo
                Public PRTID As String = ""
                Public PRTNM As String = ""
                Public OUTIP As String = ""
                Public OUTPORT As String = ""
                Public SUPPORTIP As String = ""
                Public SELECTED As String = ""
                Public IOPORT As String = ""
                Public LEFTMARGIN As String = ""
                Public TOPMARGIN As String = ""

                '< yjlee
                Public PRTTYPE As String = ""

                Public Sub New()
                    MyBase.new()
                End Sub
            End Class
#End Region

        End Class
#End Region

#Region " 클래스 에러 : Class CLassErr "
        Public Class ClassErr
            Private mErrNo As Integer = 0
            Private mErrFlag As Boolean = False
            Private mErrMsg As String = ""

            Public Sub New()
                MyBase.New()
            End Sub

            Public ReadOnly Property ErrNo() As Integer
                Get
                    ErrNo = mErrNo
                End Get
            End Property

            Public ReadOnly Property ErrFlag() As Boolean
                Get
                    ErrFlag = mErrFlag
                End Get
            End Property

            Public ReadOnly Property ErrMsg() As String
                Get
                    ErrMsg = mErrMsg
                End Get
            End Property

            Friend Sub SetError(ByVal aiErrNo As Integer, ByVal asErrMsg As String)
                mErrFlag = True
                mErrNo = aiErrNo
                mErrMsg = asErrMsg
            End Sub

        End Class
#End Region
    End Namespace

#End Region


End Namespace

