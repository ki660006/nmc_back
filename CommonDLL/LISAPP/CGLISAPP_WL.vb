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
                    If PRG_CONST.PART_MicroBio = rsPartSlip.Substring(0, 1) Then
                        sSql += ", minrstflg = (SELECT min(NVL(rstflg, '0')) FROM lm010m"
                    Else
                        sSql += ", minrstflg = (SELECT min(NVL(rstflg, '0')) FROM lr010m"
                    End If
                    sSql += "                WHERE bcno   = b.bcno"
                    sSql += "                  AND testcd = b.testcd"
                    sSql += "              )"
                End If
                sSql += "  FROM lrw10m a, lrw11m b, lf060m f"
                sSql += " WHERE a.wluid   = :wluid"
                sSql += "   AND a.wlymd  >= :wlymds"
                sSql += "   AND a.wlymd  <= :wlymde"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymds", OracleDbType.Varchar2, rsWLYmdS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmdS))
                alParm.Add(New OracleParameter("wlymde", OracleDbType.Varchar2, rsWLYmdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmdE))

                sSql += "   AND a.wluid   = b.wluid"
                sSql += "   AND a.wlymd   = b.wlymd"
                sSql += "   AND a.wltitle = b.wltitle"
                sSql += "   AND b.testcd  = f.testcd"
                sSql += "   AND b.spccd   = f.spccd"
                sSql += "   AND f.usdt   <= b.regdt"
                sSql += "   AND f.uedt   >  b.regdt"

                If rsPartSlip.Length = 1 Then
                    sSql += "   AND f.partcd  = :partcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                ElseIf rsPartSlip.Length = 2 Then
                    sSql += "   AND f.partcd  = :partcd"
                    sSql += "   AND f.slipcd  = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsRstFlg = "N" Then
                    sSql += "   AND NVL(minrstflg, '0') = '0'"
                ElseIf rsRstFlg = "F" Then
                    sSql += "   AND minrstflg = '3'"
                End If



                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return New DataTable
            End Try
        End Function

        Public Shared Function fnGet_wl_List(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsRstFlg As String, ByVal rbMicroYn As Boolean) As DataTable
            Dim sFn As String = "fnGet_wl_title(String, String)"

            Try
                Dim sSql As String = ""
                Dim sWhere As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If rbMicroYn Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm, j.sex || '/'|| j.age sexage,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptinfo,"
                sSql += "       f3.spcnmp, f3.spcnmd, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       r.testcd, f6.tnmd, f6.tnmp, fn_ack_get_pat_befviewrst(r.bcno, r.testcd, r.spccd) bfviewrst,"
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM lj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       j3.diagnm, w.wlseq, r.spccd, f6.dispseql"
                sSql += "  FROM lrw11m w , " + sTableNm + " r, lf060m f6, lf030m f3,"
                sSql += "       lj010m j, lj013m j3"
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
                If rsRstFlg.Substring(0, 1) = "1" Then sWhere = "NVL(r.rstflg, '0') = '0'"
                If rsRstFlg.Substring(1, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '1'"
                If rsRstFlg.Substring(2, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '2'"
                If rsRstFlg.Substring(3, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '3'"

                If sWhere <> "" Then
                    sSql += " AND (" + sWhere + ")"
                End If

                sSql += " ORDER BY w.wlseq, f6.dispseql, r.testcd"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

                Return New DataTable
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
                sSql += "  FROM lrw11m w, lf060m f6, lf021m f2"
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
                Fn.log(msFile + sFn, Err)
                MsgBox(msFile + sFn + vbCrLf + ex.Message)

                Return New DataTable
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
                sSql += "  FROM lrw11m w, lf060m f6, lf021m f2"
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
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle.Trim))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                MsgBox(msFile + sFn + vbCrLf + ex.Message)

                Return New DataTable
            End Try

        End Function

    End Class

    Public Class Reg
        Private Const msFile As String = "File : CGLISAPP_WL.vb, Class : LISAPP.APP_WL.reg" & vbTab

        Private Shared m_dbCn As oracleConnection
        Private Shared m_dbTran As oracleTransaction

        Public Sub New()
            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Public Function ExecuteDo(ByVal rsWLUId As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsWLType As String, ByVal ra_List As ArrayList) As Boolean

            Dim dbCmd As New oracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                With dbCmd
                    sSql = ""
                    sSql += "INSERT INTO lrw10h SELECT fn_ack_sysdate, :modid, :modip, r.*  FROM lrw10m r"
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
                    sSql += "DELETE lrw10m"
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
                    sSql += "INSERT INTO lrw11h SELECT fn_ack_sysdate, :modid, :modip, r.*  FROM lrw11m r"
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
                    sSql += "DELETE lrw11m"
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
                    sSql += "INSERT INTO lrw10m(  wluid,  wlymd,  wltitle,  wltype,  regid,  regip, regdt )"
                    sSql += "            VALUES( :wluid, :wlymd, :wltitle, :wltype, :regid, :regip, fn_ack_sysdate)"

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
                            sSql += "INSERT INTO lrw11m(  wluid,  wlymd,  wltitle,  bcno,  testcd,  spccd,  wlseq,  wlcmt,  regid,  regip, regdt )"
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

                Throw (New Exception(ex.Message, ex))
                Return False
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        Public Function DeleteDo(ByVal rsWLUId As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsWLType As String) As Boolean
            Dim sFn As String = "Public Function DeleteDo(ByVal rsWLUId As String, String, String, String) As Boolean"

            Dim dbCmd As New oracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                With dbCmd
                    sSql = ""
                    sSql += "INSERT INTO lrw10h SELECT fn_ack_sysdate, :modid, :modip, r.*  FROM lrw10m r"
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
                    sSql += "DELETE lrw10m"
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
                    sSql += "INSERT INTO lrw11h SELECT fn_ack_sysdate, :modid, :modip, r.*  FROM lrw11m r"
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
                    sSql += "DELETE lrw11m"
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
