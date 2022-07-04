Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Namespace APP_KS
    Public Class KsFn
        Private Const msFile As String = "File : CGLISAPP_KSRACK.vb, Class : LISAPP.APP_KSRAK" + vbTab

        Public Shared Function fnGet_KsRackInfo(Optional ByVal rsRackID As String = "", Optional ByVal rsBcclscd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_KsRackInfo([String], [String]) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.*"
                sSql += "  FROM lf160m a, lf010m b"
                sSql += " WHERE a.bcclscd = :bcclscd"
                sSql += "   AND a.bcclscd = b.bcclscd"
                sSql += "   AND a.regdt  >= b.usdt"
                sSql += "   AND a.regdt  <  b.uedt"

                If rsBcclscd <> "" Then
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclscd))
                Else
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, PRG_CONST.BCCLS_BloodBank.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.BCCLS_BloodBank))
                End If

                If rsRackID.Trim <> "" Then
                    sSql += "   AND a.rackid = :rackid"
                    sSql += "   AND ROWNUM = 1"

                    alParm.Add(New OracleParameter("rackid", OracleDbType.Varchar2, rsRackID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRackID))
                End If

                sSql += " ORDER BY a.bcclscd, a.rackid, a.spccd "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_Bcno_YesNo(ByVal rsRow As String, ByVal rsCol As String, _
                                                Optional ByVal rsRackId As String = "", _
                                                Optional ByVal r_o_KsInfo As STU_KsRack = Nothing) As DataTable
            Dim sFn As String = "Function fnGet_Bcno_YesNo(String, String, String, Object) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                If rsRackId.Trim.Equals("") Then       ' 검체 이동시에 체크되는 부분
                    sSql += "SELECT * FROM lk010m"
                    sSql += " WHERE bcclscd  IN ('" + PRG_CONST.BCCLS_BldCrossMatch + "',  :bcclscd)"
                    sSql += "   AND rackid   = :rackid"
                    sSql += "   AND spccd    = :spccd"
                    sSql += "   AND numrow   = :numrow"
                    sSql += "   AND numcol   = :numcol"

                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, r_o_KsInfo.Bcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.Bcclscd))
                    alParm.Add(New OracleParameter("rackid", OracleDbType.Varchar2, r_o_KsInfo.RackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.RackId))
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, r_o_KsInfo.SpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.SpcCd))
                    alParm.Add(New OracleParameter("numrow", OracleDbType.Varchar2, rsRow.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRow))
                    alParm.Add(New OracleParameter("numcol", OracleDbType.Varchar2, rsCol.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCol))

                Else                                    ' 새롭게 검체 입력시에 체크
                    sSql += "SELECT * FROM lk010m"
                    sSql += " WHERE rackid   = :rackid"
                    sSql += "   AND numrow   = :numrow"
                    sSql += "   AND numcol   = :numcol"

                    alParm.Add(New OracleParameter("rackid", OracleDbType.Varchar2, rsRackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRackId))
                    alParm.Add(New OracleParameter("numrow", OracleDbType.Varchar2, rsRow.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRow))
                    alParm.Add(New OracleParameter("numcol", OracleDbType.Varchar2, rsCol.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCol))

                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_Bcno_YesNo(ByVal rsFlag As String, ByVal r_o_KsInfo As STU_KsRack) As DataTable
            Dim sFn As String = "Function CheckBcno_YesNo(string, Object) As DataTable"
            Dim sSql As String = ""

            Dim al As New ArrayList

            Try
                If rsFlag.Trim = "M" Then       ' 검체 이동시에 체크되는 부분
                    sSql += "SELECT * FROM lk010m"
                    sSql += " WHERE bcclscd  = :bcclscd"
                    sSql += "   AND rackid   = :rackid"
                    sSql += "   AND spccd    = :spccd"
                    sSql += "   AND numrow   = :numrow"
                    sSql += "   AND numcol   = :numcol"

                    al.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, r_o_KsInfo.Bcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.Bcclscd))
                    al.Add(New OracleParameter("rackid", OracleDbType.Varchar2, r_o_KsInfo.RackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.RackId))
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, r_o_KsInfo.SpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.SpcCd))
                    al.Add(New OracleParameter("numrow", OracleDbType.Varchar2, r_o_KsInfo.NumRow.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.NumRow))
                    al.Add(New OracleParameter("numcol", OracleDbType.Varchar2, r_o_KsInfo.NumCol.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.NumCol))

                Else                                    ' 새롭게 검체 입력시에 체크
                    sSql += "SELECT * FROM lk010m"
                    sSql += " WHERE bcclscd  = :bcclscd"
                    sSql += "   AND rackid   = :rackid"
                    sSql += "   AND numrow   = :numrow"
                    sSql += "   AND numcol   = :numcol"

                    al.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, r_o_KsInfo.Bcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.Bcclscd))
                    al.Add(New OracleParameter("rackid", OracleDbType.Varchar2, r_o_KsInfo.RackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.RackId))
                    al.Add(New OracleParameter("numrow", OracleDbType.Varchar2, r_o_KsInfo.NumRow.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.NumRow))
                    al.Add(New OracleParameter("numcol", OracleDbType.Varchar2, r_o_KsInfo.NumCol.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.NumCol))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, al)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_KsBcnoInfo_regno(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Function fnGet_KsBcnoInfo_regno(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT rackid, numrow, numcol, fn_ack_get_bcno_full(bcno) bcno, other"
                sSql += "  FROM lk010m"
                sSql += " WHERE bcclscd = :bcclscd"
                sSql += "   AND bcno   IN (SELECT bcno FROM lj010m WHERE regno = :regno)"
                sSql += " ORDER BY rackid, numrow"

                al.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, PRG_CONST.BCCLS_BloodBank.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.BCCLS_BloodBank))
                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_KsBcnoInfo(ByVal rsBcno As String, ByVal r_o_KsInfo As STU_KsRack) As DataTable
            Dim sFn As String = "Function fnGet_KsBcnoInfo(String, Object) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                rsBcno = rsBcno.Replace("-", "")

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       j.bcno, j.regno, j.patnm,"
                sSql += "       k.other, k.bcno, fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi:ss') colldt,"
                sSql += "       r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '') wkno"
                sSql += "  FROM lj010m j,"
                sSql += "       (SELECT other, bcno  FROM lk010m"
                sSql += "         WHERE bcclscd = :bcclscd"
                sSql += "           AND rackid  = :rackid"
                sSql += "           AND spccd   = :spccd"
                sSql += "           AND bcno    = :bcno"
                sSql += "       ) k, lj011m j1 LEFT OUTER JOIN"
                sSql += "       " + sTableNm + " r ON (j1.bcno = r.bcno AND j1.tclscd = r.tclscd)"
                sSql += " WHERE j.bcno    = :bcno"
                sSql += "   AND j.bcno    = k.bcno"
                sSql += "   AND j.bcno    = j1.bcno"

                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, r_o_KsInfo.Bcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.Bcclscd))
                alParm.Add(New OracleParameter("rackid", OracleDbType.Varchar2, r_o_KsInfo.RackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.RackId))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, r_o_KsInfo.SpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.SpcCd))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_KsBcnoInfo(ByVal r_o_KsInfo As STU_KsRack) As DataTable
            Dim sFn As String = "Function fnGetKeep_BcnoInfo(Object) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT bcno, numrow, numcol, other"
                sSql += "  FROM lk010m"
                sSql += " WHERE bcclscd = :bcclscd"
                sSql += "   AND rackid  = :rackid"
                sSql += " ORDER BY numrow ASC"

                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, r_o_KsInfo.Bcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.Bcclscd))
                alParm.Add(New OracleParameter("rackid", OracleDbType.Varchar2, r_o_KsInfo.RackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.RackId))


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_KsBcno_Regno(ByVal rsBcclsCd As String, ByVal rsRegNo As String, ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_KsBcno_Regno(string, string, string) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                rsBcNo = rsBcNo.Replace("-", "")

                sSql += "SELECT fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       r.rackid, r.numrow, r.numcol, j.regno, j.patnm,"
                sSql += "       j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE '' END wardroom,"
                sSql += "       '[' || f.bcclscd || '] ' ||  f.bcclsnmd bcclsnmd"
                sSql += "  FROM lk010m r, lj010m j, lf010m f"

                If rsRegNo <> "" Then
                    sSql += " WHERE j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                Else
                    sSql += " WHERE j.bcno  = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                If rsBcclsCd <> "" Then
                    sSql += "   AND j.bcclscd = :bcclscd"
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND j.bcclscd  = f.bcclscd"
                sSql += "   AND j.bcprtdt >= f.usdt"
                sSql += "   AND j.bcprtdt <  f.uedt"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_Use_Spcinfo(ByVal rsBcclsCd As String, ByVal rsRackId As String) As DataTable
            Dim sFn As String = "Function fnGet_Use_Spcinfo(string, string) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT f3.spccd, f3.spcnmd"
                sSql += "  FROM lf160m f16, lf030m f3"
                sSql += " WHERE f16.bcclscd = :bcclscd"
                sSql += "   AND f16.rackid  = :rackid"
                sSql += "   AND f16.spccd   = f3.spccd"
                sSql += "   AND f3.usdt    <= fn_ack_sysdate"
                sSql += "   AND f3.uedt    >  fn_ack_sysdate"

                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                alParm.Add(New OracleParameter("rackid", OracleDbType.Varchar2, rsRackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRackId))


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_KsBcno_cmt(ByVal rsBcNo As String) As String
            Dim sFn As String = "Function fnGet_KsBcno_cmt(string) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                rsBcNo = rsBcNo.Replace("-", "")

                sSql += "SELECT f21.bacnmd"
                sSql += "  FROM lj010m j, lm010m r, lm012M r12, lf210m f21"
                sSql += " WHERE j.bcno    = :bcno"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.bcno    = r12.bcno"
                sSql += "   AND r.testcd  = r12.testcd"
                sSql += "   AND r12.baccd = r21.baccd"
                sSql += "   AND f21.usdt <= r.tkdt"
                sSql += "   AND f21.uedt >  r.tkdt"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Dim sValue As String = ""

                For ix As Integer = 0 To dt.Rows.Count - 1
                    If ix <> 0 Then sValue += ","
                    sValue += dt.Rows(ix).Item("bacnmd").ToString
                Next

                Return sValue

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
    End Class

    Public Class ExecFn
        Private Const msFile As String = "File : CGLISAPP_KSRACK.vb, Class : LISAPP.APP_KSRAK" + vbTab


        Public Shared Function InsertBcno_NewPlace(ByVal r_o_KsInfo As STU_KsRack, ByVal rsBcno As String, _
                                                   ByVal r_o_ToKsInfo As STU_KsRack, ByVal rsComment As String) As Boolean
            Dim sFn As String = "Function InsertBcno_NewPlace(ByVal objBcno As csKeepBcno_Info, ByVal as_BCNO As String,"
            sFn += " ByVal as_ToBcno As csKeepBcno_Info, ByVal as_Comment As String) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            InsertBcno_NewPlace = False

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO lk010h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lk010m a"
                    sSql += " WHERE bcclscd = :bcclscd"
                    sSql += "   AND rackid  = :rackid"
                    sSql += "   AND spccd   = :spccd"
                    sSql += "   AND bcno    = :bcno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bcclscd", OracleDbType.Varchar2).Value = r_o_KsInfo.Bcclscd
                    .Parameters.Add("rackid", OracleDbType.Varchar2).Value = r_o_KsInfo.RackId
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_o_KsInfo.SpcCd
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno.Replace("-", "")


                    iRet = .ExecuteNonQuery()


                    sSql = ""
                    sSql += "UPDATE lk010m SET"
                    sSql += "       rackid  = :rackidchg,"
                    sSql += "       numrow  = :numrow,"
                    sSql += "       numcol  = :numcol,"
                    sSql += "       other   = :other,"
                    sSql += "       regdt   = fn_ack_sysdate,"
                    sSql += "       regid   = :regid,"
                    sSql += "       editdt  = fn_ack_sysdate,"
                    sSql += "       editid  = :editid,"
                    sSql += "       editip  = :editip"
                    sSql += " WHERE bcclscd = :bcclscd"
                    sSql += "   AND rackid  = :rackid"
                    sSql += "   AND spccd   = :spccd"
                    sSql += "   AND bcno    = :bcno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rackidchg", OracleDbType.Varchar2).Value = r_o_ToKsInfo.RackId
                    .Parameters.Add("numrow", OracleDbType.Varchar2).Value = r_o_ToKsInfo.NumRow
                    .Parameters.Add("numcol", OracleDbType.Varchar2).Value = r_o_ToKsInfo.NumCol
                    .Parameters.Add("other", OracleDbType.Varchar2).Value = rsComment
                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("bcclscd", OracleDbType.Varchar2).Value = r_o_KsInfo.Bcclscd
                    .Parameters.Add("rackid", OracleDbType.Varchar2).Value = r_o_KsInfo.RackId.Trim
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_o_KsInfo.SpcCd
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno.Replace("-", "")


                    iRet = .ExecuteNonQuery()
                End With

                dbTran.Commit()
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

        Public Shared Sub Insert_KeepBcno(ByVal r_o_KsInfo As STU_KsRack, ByVal rsBcno As String, ByVal rsComment As String)
            Dim sFn As String = "Sub Insert_KeepBcno(STU_KsRack, String, String)"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "INSERT INTO lk010m"
                sSql += "          (  bcclscd,  rackid,  spccd,  bcno, regdt,           regid,  numrow,  numcol,  other,  editid,  editip, editdt )"
                sSql += "    VALUES( :bcclscd, :rackid, :spccd, :bcno, fn_ack_sysdate, :regid, :numrow, :numcol, :other, :editid, :editip, fn_ack_sysdate )"

                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, r_o_KsInfo.Bcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.Bcclscd))
                alParm.Add(New OracleParameter("rackid", OracleDbType.Varchar2, r_o_KsInfo.RackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.RackId))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, r_o_KsInfo.SpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.SpcCd))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno.Replace("-", "")))
                alParm.Add(New OracleParameter("regid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                alParm.Add(New OracleParameter("numrow", OracleDbType.Varchar2, r_o_KsInfo.NumRow.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.NumRow))
                alParm.Add(New OracleParameter("numcol", OracleDbType.Varchar2, r_o_KsInfo.NumCol.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_o_KsInfo.NumCol))
                alParm.Add(New OracleParameter("other", OracleDbType.Varchar2, rsComment.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComment))
                alParm.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                alParm.Add(New OracleParameter("editip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))

                DbCommand()
                DbExecute(sSql, alParm, True)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Sub

        Public Shared Function Discard_Bcno(ByVal r_o_KsInfo As STU_KsRack, ByVal rsBcNo As String) As Boolean
            Dim sFn As String = "Function Discard_Bcno(ByVal objBcno As csKeepBcno_Info, ByVal as_BCNO As String) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New oracleCommand

            Try
                rsBcNo = rsBcNo.Replace("-", "")

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                DbCmd.Connection = DbCn
                dbCmd.Transaction = dbTran

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                sSql = ""
                sSql += "INSERT INTO lk010h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.* FROM lk010m a"
                sSql += " WHERE bcclscd = :bcclscd"
                sSql += "   AND rackid  = :rackid"
                sSql += "   AND spccd   = :spccd"
                sSql += "   AND bcno    = :bcno"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("bcclscd", OracleDbType.Varchar2).Value = r_o_KsInfo.Bcclscd
                    .Parameters.Add("rackid", OracleDbType.Varchar2).Value = r_o_KsInfo.RackId
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_o_KsInfo.SpcCd
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery()
                End With


                sSql = ""
                sSql += "DELETE lk010m "
                sSql += " WHERE bcclscd = :bcclscd"
                sSql += "   AND rackid  = :rackid"
                sSql += "   AND spccd   = :spccd"
                sSql += "   AND bcno    = :bcno"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcclscd", OracleDbType.Varchar2).Value = r_o_KsInfo.Bcclscd
                    .Parameters.Add("rackid", OracleDbType.Varchar2).Value = r_o_KsInfo.RackId
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_o_KsInfo.SpcCd
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery()
                End With

                If iRet > 0 Then
                    dbTran.Commit()
                    Return True
                Else
                    dbTran.Rollback()
                    Return False
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

        Public Shared Function DiscardAll_Bcno(ByVal r_o_KsInfo As STU_KsRack, ByVal r_al_bcno As ArrayList) As Boolean
            Dim sFn As String = "Function Discard_Bcno(STU_KsRacko, ArrayListg) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New oracleCommand

            Try

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTran

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                For ix As Integer = 0 To r_al_bcno.Count - 1

                    Dim sBcno As String = CType(r_al_bcno.Item(ix), String)

                    sSql = ""
                    sSql += "INSERT INTO lk010h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip , a.* FROM lk010m a"
                    sSql += " WHERE bcclscd = :bcclscd"
                    sSql += "   AND rackid  = :rackid"
                    sSql += "   AND spccd   = :spccd"
                    sSql += "   AND bcno    = :bcno"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("bcclscd", OracleDbType.Varchar2).Value = r_o_KsInfo.Bcclscd
                        .Parameters.Add("rackid", OracleDbType.Varchar2).Value = r_o_KsInfo.RackId
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_o_KsInfo.SpcCd
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = sBcno.Replace("-", "")

                        .ExecuteNonQuery()
                    End With
                Next

                sSql = ""
                sSql += "DELETE lk010m"
                sSql += " WHERE bcclscd = :bcclscd"
                sSql += "   AND rackid  = :rackid"
                sSql += "   AND spccd   = :spccd"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcclscd", OracleDbType.Varchar2).Value = r_o_KsInfo.Bcclscd
                    .Parameters.Add("rackid", OracleDbType.Varchar2).Value = r_o_KsInfo.RackId
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = r_o_KsInfo.SpcCd

                    iRet += .ExecuteNonQuery()
                End With

                If iRet > 0 Then
                    dbTran.Commit()
                    Return True
                Else
                    dbTran.Rollback()
                    Return False
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

        Public Shared Function fnExe_KeepBcnoComment(ByVal rsComment As String, ByVal rsBcNo As String, ByVal ro_KeepInfo As STU_KsRack) As Boolean
            Dim sFn As String = "Function Update_KeepBcnoComment(String, String, csKeepBcno_Info) As Boolean"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "UPDATE lk010m SET other = :other, editdt = fn_ack_sysdate, editid = :editid, editip = :editip"
                sSql += " WHERE bcclscd = :bcclscd"
                sSql += "   AND rackid  = :rackid"
                sSql += "   AND spccd   = :spccd"
                sSql += "   AND bcno    = :bcno"

                alParm.Add(New OracleParameter("other", OracleDbType.Varchar2, rsComment.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComment))
                alParm.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                alParm.Add(New OracleParameter("editip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))

                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, ro_KeepInfo.Bcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ro_KeepInfo.Bcclscd))
                alParm.Add(New OracleParameter("rackid", OracleDbType.Varchar2, ro_KeepInfo.RackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ro_KeepInfo.RackId))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, ro_KeepInfo.SpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ro_KeepInfo.SpcCd))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo.Replace("-", "")))

                DbCommand()
                DbExecute(sSql, alParm, True)

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

End Namespace
