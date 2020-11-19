Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Namespace APP_WL
    Public Class Qry
        Private Const msFile As String = "File : CGLISAPP_WL.vb, Class : LISAPP.APP_WL.Qry" + vbTab

        Public Shared Function fnGet_wl_title(ByVal rsPartSlip As String, ByVal rsWLUid As String, ByVal rsWLYmdS As String, ByVal rsWLYmdE As String, ByVal rsRstFlg As String) As DataTable
            Dim sFn As String = "fnGet_wl_title(String, String)"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       a.wluid, a.wlymd, a.wltitle, a.wltype"
                If rsRstFlg <> "" Then
                    sSql += ", minrstflg = (SELECT min(NVL(rstflg, '') FROM rr010m"
                    sSql += "                WHERE bcno   = b.bcno"
                    sSql += "                  AND testcd = b.testcd"
                    sSql += "              )"
                End If
                sSql += "  FROM rrw10m a, rrw11m b, rf060m f"
                sSql += " WHERE a.wluid   = :wluid"
                sSql += "   AND a.wlymd  >= :wlymds"
                sSql += "   AND a.wlymd  <= :wlymde"
                sSql += "   AND a.wluid   = b.wluid"
                sSql += "   AND a.wlymd   = b.wlymd"
                sSql += "   AND a.wltitle = b.wltitle"
                sSql += "   AND b.testcd  = f.testcd"
                sSql += "   AND b.spccd   = f.spccd"
                sSql += "   AND f.usdt   <= b.regdt"
                sSql += "   AND f.uedt   >  b.regdt"
                sSql += "   AND f.partcd  = :partcd"
                sSql += "   AND f.slipcd  = :slipcd"

                If rsRstFlg = "N" Then
                    sSql += "   AND minrstflg IN ('', '0')"
                ElseIf rsRstFlg = "F" Then
                    sSql += "   AND minrstflg = '3'"
                End If

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymds", OracleDbType.Varchar2, rsWLYmdS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmdS))
                alParm.Add(New OracleParameter("wlymde", OracleDbType.Varchar2, rsWLYmdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmdE))
                alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_wl_List(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String) As DataTable
            Dim sFn As String = "fnGet_wl_title(String, String)"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm, j.sex || '/'|| j.age sexage,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno, r.viewrst,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       f3.spcnmp, f3.spcnmd, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       r.testcd, f6.tnmd, f6.tnmp, fn_ack_get_pat_befviewrst(r.bcno, r.testcd, r.spccd) bfviewrst,"
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM rj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       j3.diagnm, w.wlseq, r.spccd, f6.dispseql"
                sSql += "  FROM rrw11m w , rr010m r, rf060m f6, lf030m f3, rj010m j, rj013m j3"
                sSql += " WHERE w.wluid   = :wluid"
                sSql += "   AND w.wlymd   = :wlymd"
                sSql += "   AND w.wltitle = :wltitle"
                sSql += "   AND w.bcno    = j.bcno"
                sSql += "   AND w.bcno    = r.bcno"
                sSql += "   AND w.testcd  = r.testcd"
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.spccd   = f6.spccd"
                sSql += "   AND r.tkdt   >= f6.usdt"
                sSql += "   AND r.tkdt   <  f6.uedt"
                sSql += "   AND r.spccd   = f3.spccd"
                sSql += "   AND r.tkdt   >= f3.usdt"
                sSql += "   AND r.tkdt   <  f3.uedt"
                sSql += "   AND j.bcno    = j3.bcno (+)"
                sSql += " ORDER BY w.wlseq, f6.dispseql, r.testcd"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_wl_test(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String) As DataTable
            Dim sFn As String = "fnGet_wl_test"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList


                sSql += "SELECT f6.testcd, MAX(f6.tnmd) tnmd,"
                sSql += "       MAX(f2.dispseq) sort1,"
                sSql += "       MAX(f6.dispseql) sort2"
                sSql += "  FROM rrw11m w, rf060m f6, rf021m f2"
                sSql += " WHERE w.wluid   = :wluid"
                sSql += "   AND w.wlymd   = :wlymd"
                sSql += "   AND w.wltitle = :wltitle"
                sSql += "   AND w.testcd  = f6.testcd"
                sSql += "   AND w.spccd   = f6.spccd"
                sSql += "   AND w.regdt  >= f6.usdt"
                sSql += "   AND w.regdt  <  f6.uedt"
                sSql += "   AND f6.partcd = f2.partcd"
                sSql += "   AND f6.slipcd = f2.slipcd"
                sSql += "   AND w.regdt  >= f2.usdt"
                sSql += "   AND w.regdt  <  f2.uedt"
                sSql += " GROUP BY f6.testcd"
                sSql += " ORDER BY sort1, sort2, testcd"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_wl_testspc(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String) As DataTable
            Dim sFn As String = "fnGet_wl_test"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList


                sSql += "SELECT f6.testcd || f6.spccd testspc, MAX(f6.tnmd) tnmd,"
                sSql += "       MAX(f2.dispseq) sort1,"
                sSql += "       MAX(f6.dispseql) sort2"
                sSql += "  FROM rrw11m w, rf060m f6, rf021m f2"
                sSql += " WHERE w.wluid   = :wluid"
                sSql += "   AND w.wlymd   = :wlymd"
                sSql += "   AND w.wltitle = :wltitle"
                sSql += "   AND w.testcd  = f6.testcd"
                sSql += "   AND w.spccd   = f6.spccd"
                sSql += "   AND w.regdt  >= f6.usdt"
                sSql += "   AND w.regdt  <  f6.uedt"
                sSql += "   AND f6.partcd = f2.partcd"
                sSql += "   AND f6.slipcd = f2.slipcd"
                sSql += "   AND w.regdt  >= f2.usdt"
                sSql += "   AND w.regdt  <  f2.uedt"
                sSql += " GROUP BY f6.testcd, f6.spccd"
                sSql += " ORDER BY sort1, sort2, testspc"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

    Public Class Reg
        Private Const msFile As String = "File : CGLISAPP_WL.vb, Class : LISAPP.APP_WL.reg" & vbTab

        Private Shared m_dbCn As OracleConnection
        Private Shared m_dbTran As OracleTransaction

        Public Sub New()
            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Private Function fnGet_Server_DateTime() As String

            Dim sFn As String = "Private Function fnGet_Server_DateTime() As string"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql += "SELECT fn_ack_sysdate srvdate FROM DUAL"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("srvdate").ToString()
                Else
                    Return Format(Now, "yyyyMMddHHmmss").ToString
                End If

            Catch ex As Exception
                Return Format(Now, "yyyyMMddHHmmss").ToString
            End Try

        End Function

        Public Function ExecuteDo(ByVal rsWLUId As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsWLType As String, ByVal ra_List As ArrayList) As Boolean
            Dim sFn As String = "Public Function ExecuteDo(ByVal rsWLUId As String, String, String, String, ArrayList) As Boolean"

            Dim dbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sSvrDate As String = fnGet_Server_DateTime()

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                With dbCmd
                    sSql = ""
                    sSql += "INSERT INTO rrw10h SELECT fn_ack_sysdate, :modid, :modip, r.*  FROM rrw10m r"
                    sSql += " WHERE wluid   = :wluid"
                    sSql += "   AND wlymd   = :wlymd"
                    sSql += "   AND wltitle = :wltitle"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle

                    .ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rrw10m"
                    sSql += " WHERE wluid   = :wluid"
                    sSql += "   AND wlymd   = :wlymd"
                    sSql += "   AND wltitle = :wltitle"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle

                    .ExecuteNonQuery()

                    sSql = ""
                    sSql += "INSERT INTO rrw11h SELECT fn_ack_sysdate, :modid, :modip, r.*  FROM rrw11m r"
                    sSql += " WHERE wluid   = :wluid"
                    sSql += "   AND wlymd   = :wlymd"
                    sSql += "   AND wltitle = :wltitle"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle

                    .ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rrw11m"
                    sSql += " WHERE wluid   = :wluid"
                    sSql += "   AND wlymd   = :wlymd"
                    sSql += "   AND wltitle = :wltitle"

                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle

                    .ExecuteNonQuery()

                    sSql = ""
                    sSql += "INSERT INTO rrw10m(  wluid,  wlymd,  wltitle,  wltype,  regid,  regip, regdt )"
                    sSql += "            VALUES( :wluid, :wlymd, :wltitle, :wltype, :regid, :regip, fn_ack_sysdate )"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle
                    .Parameters.Add("wltype", OracleDbType.Varchar2).Value = rsWLType
                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    iRet += .ExecuteNonQuery()
                End With

                For ix As Integer = 0 To ra_List.Count - 1
                    Dim sBcNo As String = ra_List(ix).ToString.Split("|"c)(0)
                    Dim sSpcCd As String = ra_List(ix).ToString.Split("|"c)(1)
                    Dim sTestCd() As String = ra_List(ix).ToString.Split("|"c)(2).Split("^"c)
                    Dim sWlCmt As String = ra_List(ix).ToString.Split("|"c)(3)

                    For ix2 As Integer = 0 To sTestCd.Length - 1
                        If sTestCd(ix2).Trim = "" Then Exit For

                        With dbCmd
                            sSql = ""
                            sSql += "INSERT INTO rrw11m(  wluid,  wlymd,  wltitle,  bcno,  testcd,  spccd,  wlseq,  wlcmt,  regid,  regip, regdt )"
                            sSql += "            VALUES( :wluid, :wlymd, :wltitle, :bcno, :testcd, :spccd, :wlseq, :wlcmt, :regid, :regip, fn_ack_sysdate )"
                            .CommandText = sSql

                            .Parameters.Clear()

                            .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                            .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                            .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = sBcNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = sTestCd(ix2).Trim
                            .Parameters.Add("spccd", OracleDbType.Varchar2).Value = sSpcCd
                            .Parameters.Add("wlseq", OracleDbType.Int32).Value = ix + 1
                            .Parameters.Add("wlcmt", OracleDbType.Varchar2).Value = sWlCmt
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            iRet += .ExecuteNonQuery()
                        End With

                        With dbCmd
                            sSql = ""
                            sSql += "UPDATE rr010m SET wkdt = :wkdt"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND testcd = :testcd"

                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("wkdt", OracleDbType.Varchar2).Value = sSvrDate
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = sBcNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = sTestCd(ix2).Trim

                            iRet += .ExecuteNonQuery()
                        End With

                    Next

                Next

                If iRet < 2 Then
                    m_dbTran.Rollback()
                    Return False
                Else
                    m_dbTran.Commit()
                    Return True

                End If

            Catch ex As Exception
                m_dbTran.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        Public Function DeleteDo(ByVal rsWLUId As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsWLType As String) As Boolean
            Dim sFn As String = "Public Function DeleteDo(ByVal rsWLUId As String, String, String, String) As Boolean"

            Dim dbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sSvrDate As String = fnGet_Server_DateTime()

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                With dbCmd
                    sSql = ""
                    sSql += "INSERT INTO rrw10h SELECT fn_ack_sysdate, :modid, :modip, r.*  FROM rrw10m r"
                    sSql += " WHERE wluid   = :wluid"
                    sSql += "   AND wlymd   = :wlymd"
                    sSql += "   AND wltitle = :wltitle"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle

                    .ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rrw10m"
                    sSql += " WHERE wluid   = :wluid"
                    sSql += "   AND wlymd   = :wlymd"
                    sSql += "   AND wltitle = :wltitle"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle

                    iRet += .ExecuteNonQuery()

                    sSql = ""
                    sSql += "INSERT INTO rrw11h SELECT fn_ack_sysdate, :modid, :modip, r.*  FROM rrw11m r"
                    sSql += " WHERE wluid   = :wluid"
                    sSql += "   AND wlymd   = :wlymd"
                    sSql += "   AND wltitle = :wltitle"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle

                    .ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rrw11m"
                    sSql += " WHERE wluid   = :wluid"
                    sSql += "   AND wlymd   = :wlymd"
                    sSql += "   AND wltitle = :wltitle"

                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("wluid", OracleDbType.Varchar2).Value = rsWLUId
                    .Parameters.Add("wlymd", OracleDbType.Varchar2).Value = rsWLYmd
                    .Parameters.Add("wltitle", OracleDbType.Varchar2).Value = rsWLTitle

                    iRet += .ExecuteNonQuery()

                End With

                If iRet < 2 Then
                    m_dbTran.Rollback()
                    Return False
                Else
                    m_dbTran.Commit()
                    Return True

                End If

            Catch ex As Exception
                m_dbTran.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

    End Class
End Namespace
