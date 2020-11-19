'*****************************************************************************************/
'/*                                                                                      */
'/* Project Name : 원자력병원 Laboratory Information System(KMC_LIS)                     */
'/*                                                                                      */
'/*                                                                                      */
'/* FileName     : CGDA_EXLAB.vb                                                         */
'/* PartName     : 위탁검사에 사용되는 공유 Data Access                                  */
'/* Description  : 위탁검사 공유 Data Access Class                                       */
'/* Design       :                                                                       */
'/* Coded        : 2007-10-23 hyde                                                       */
'/* Modified     :                                                                       */
'/*                                                                                      */
'/*                                                                                      */
'/*                                                                                      */
'/****************************************************************************************/
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Public Class APP_EXLAB

    Private Const msFile As String = "File : CGLISAPP_EXLAB.vb, Class : LISAPP.APP_EXLAB" + vbTab

    Public Shared Function fnExe_UpLoad(ByVal rsExLabCd As String, ByVal rsFileNm As String, ByVal rsUsrId As String, ByVal rsCmtCont As String, ByVal raData As ArrayList) As String
        Dim sFn As String = "Function fnExe_UpLoad(arraylist) As DataTable"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "UPDATE rre10m SET regid = :regid, regdt = fn_ack_sysdate"
            sSql += " WHERE exlabcd = :exlabcd"
            sSql += "   AND filenm  = :filenm"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm

                iRet = .ExecuteNonQuery()
            End With

            If iRet = 0 Then
                sSql = ""
                sSql += "INSERT INTO rre10m(  exlabcd,  filenm, fregdt,         regdt,           regid)"
                sSql += "            VALUES( :exlabcd, :filenm, fn_ack_sysdate, fn_ack_sysdate, :regid)"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm
                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId

                    iRet = .ExecuteNonQuery()
                End With

                If iRet = 0 Then
                    dbTran.Rollback()
                    Return "[오류] rre10m 데이블에 입력하지 못 했습니다.!!"
                End If
            End If

            sSql = ""
            sSql += "DELETE rre11m"
            sSql += " WHERE exlabcd = :exlabcd"
            sSql += "   AND filenm  = :filenm"

            With dbCmd
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm

                iRet = .ExecuteNonQuery()
            End With

            sSql = ""
            sSql += "DELETE rre12m"
            sSql += " WHERE exlabcd = :exlabcd"
            sSql += "   AND filenm  = :filenm"

            With dbCmd
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm

                iRet = .ExecuteNonQuery()
            End With

            For intIdx As Integer = 0 To raData.Count - 1
                Dim strBcNo As String = raData.Item(intIdx).ToString.Split("|"c)(0)
                Dim strTclsCd As String = raData.Item(intIdx).ToString.Split("|"c)(1)
                Dim strSpcCd As String = raData.Item(intIdx).ToString.Split("|"c)(2)
                Dim strRemark As String = raData.Item(intIdx).ToString.Split("|"c)(3)

                sSql = ""
                sSql += "INSERT INTO rre11m(  exlabcd,  filenm,  bcno,  testcd,  spccd,  remark, regdt)"
                sSql += "            VALUES( :exlabcd, :filenm, :bcno, :testcd, :spccd, :remark, fn_ack_sysdate)"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = strBcNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = strTclsCd
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = strSpcCd
                    .Parameters.Add("remark", OracleDbType.Varchar2).Value = strRemark

                    iRet = .ExecuteNonQuery()
                End With
            Next

            If rsCmtCont <> "" Then
                sSql = ""
                sSql += "INSERT INTO rre12m(  exlabcd,  filenm,  cmtcont, regdt )"
                sSql += "            VALUES( :exlabcd, :filenm, :cmtcont, fn_ack_sysdate )"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm
                    .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = rsCmtCont

                    iRet = .ExecuteNonQuery()
                End With
            End If

            dbTran.Commit()
            Return ""

        Catch ex As Exception
            dbTran.Rollback()
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Shared Function fnExe_UpLoad_Del(ByVal rsExLabCd As String, ByVal rsUsrId As String, ByVal rsCmtCont As String, ByVal raData As ArrayList) As String
        Dim sFn As String = "Function fnExe_UpLoad(arraylist) As DataTable"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand
        Dim dbDA As OracleDataAdapter

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim dt As New DataTable
            Dim strDate As String = ""
            Dim arlFileNms As New ArrayList


            sSql = ""
            sSql += "SELECT fn_ack_sysdate srvdate FROM DUAL"

            dbCmd.Connection = dbCn
            dbCmd.Transaction = dbTran
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            dbDA = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDA.Fill(dt)

            If dt.Rows.Count > 0 Then
                strDate = dt.Rows(0).Item("srvdate").ToString()
            Else
                strDate = Format(Now, "yyyyMMddHHmmss").ToString
            End If

            For ix As Integer = 0 To raData.Count - 1
                Dim strBcNo As String = raData.Item(ix).ToString.Split("|"c)(0)
                Dim strTclsCd As String = raData.Item(ix).ToString.Split("|"c)(1)
                Dim strSpcCd As String = raData.Item(ix).ToString.Split("|"c)(2)
                Dim strRemark As String = raData.Item(ix).ToString.Split("|"c)(3)
                Dim strFileNm As String = raData.Item(ix).ToString.Split("|"c)(4)

                sSql = ""
                sSql += "DELETE rre11m"
                sSql += " WHERE exlabcd = :exlabcd"
                sSql += "   AND filenm  = :filenm"
                sSql += "   AND bcno    = :bcno"
                sSql += "   AND testcd  = :testcd"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = strFileNm
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = strBcNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = strTclsCd

                    iRet = .ExecuteNonQuery()
                End With

                If arlFileNms.Contains(strFileNm) = False Then arlFileNms.Add(strFileNm)
            Next

            For intIdx As Integer = 0 To arlFileNms.Count - 1
                sSql = ""
                sSql += "DELETE rre12m"
                sSql += " WHERE exlabcd = :exlabcd"
                sSql += "   AND filenm  = :filenm"
                sSql += "   AND (SELECT count(*) FROM rre11m WHERE exlabcd = :exlabcd AND filenm = :filenm) = 0"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = arlFileNms.Item(intIdx).ToString
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = arlFileNms.Item(intIdx).ToString

                    iRet = .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += "DELETE rre10m"
                sSql += " WHERE exlabcd = :exlabcd"
                sSql += "   AND filenm  = :filenm"
                sSql += "   AND (SELECT count(*) FROM rre11m WHERE exlabcd = :exlabcd AND filenm = :filenm) = 0"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = arlFileNms.Item(intIdx).ToString
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = arlFileNms.Item(intIdx).ToString

                    iRet = .ExecuteNonQuery()
                End With
            Next
            dbTran.Commit()
            Return ""

        Catch ex As Exception
            dbTran.Rollback()
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Shared Function fnGet_UpLoad_FileList(ByVal rsDateS As String, ByVal rsDateE As String) As DataTable
        Dim sFn As String = "Function fnGet_UpLoad_List(string, string, string) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql = ""
            sSql += "SELECT a.exlabcd, a.filenm,"
            sSql += "       fn_ack_get_usr_name(a.regid) regnm,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd') regdt,"
            sSql += "       (SELECT exlabnmd FROM rf050m WHERE exlabcd = a.exlabcd) exlabnmd"
            sSql += "  FROM rre10m a"
            sSql += " WHERE a.regdt >= :dates"
            sSql += "   AND a.regdt <= :datee || '235959'"
            sSql += " ORDER BY a.regdt"

            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_UpLoad_List(ByVal rsExLabCd As String, ByVal rsFileNm As String) As DataTable
        Dim sFn As String = "Function fnGet_UpLoad_List(string, string, string) As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql = ""
            sSql += "SELECT e11.bcno, e11.testcd, e11.spccd, f6.tnmd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e11.remark, e11.filenm, e12.cmtcont, r.rstflg"
            sSql += "  FROM rj010m j, rj011m j1, rr010m r,"
            sSql += "       rf060m f6, lf030m f3,"
            sSql += "       rre11m e11 LEFT OUTER JOIN"
            sSql += "       rre12m e12 ON (e11.exlabcd = e12.exlabcd AND e11.filenm = e12.filenm)"
            sSql += " WHERE e11.exlabcd = :exlabcd"
            sSql += "   AND e11.filenm  = :filenm"
            sSql += "   AND e11.bcno    = r.bcno"
            sSql += "   AND e11.testcd  = r.testcd"
            sSql += "   AND e11.spccd   = r.spccd"
            sSql += "   AND j.bcno      = j1.bcno"
            sSql += "   AND j1.bcno     = r.bcno"
            sSql += "   AND j1.tclscd   = r.tclscd"
            sSql += "   AND r.testcd    = f6.testcd"
            sSql += "   AND r.spccd     = f6.spccd"
            sSql += "   AND f6.usdt    <= r.tkdt"
            sSql += "   AND f6.uedt    >  r.tkdt"
            sSql += "   AND r.spccd     = f3.spccd"
            sSql += "   AND f3.usdt    <= r.tkdt"
            sSql += "   AND f3.uedt    > r.tkdt"

            alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            alParm.Add(New OracleParameter("filenm", OracleDbType.Varchar2, rsFileNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFileNm))

            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_UpLoad_List(ByVal rsExLabCd As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsRegNo As String) As DataTable
        Dim sFn As String = "Function fnGet_UpLoad_List(string, string, string) As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT e1.bcno, e1.testcd, e1.spccd, f6.tnmd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.remark, e12.cmtcont, r.rstflg,"
            sSql += "       (SELECT MAX(regdt) FROM rre11m WHERE bcno = r.bcno AND testcd = r.testcd) regdt"
            sSql += "  FROM rj010m j,  rj011m j1, rr010m r,"
            sSql += "       rf060m f6, lf030m f3,"
            sSql += "       rre11m e1,"
            sSql += "       rre10m e LEFT OUTER JOIN"
            sSql += "       rre12m e12 ON (e.exlabcd = e12.exlabcd AND e.filenm = e12.filenm)"
            sSql += " WHERE e.regdt  >= :dates"
            sSql += "   AND e.regdt  <= :datee || '235959'"
            sSql += "   AND e.exlabcd = e1.exlabcd"
            sSql += "   AND e.filenm  = e1.filenm"
            sSql += "   AND e1.bcno   = r.bcno"
            sSql += "   AND e1.testcd = r.testcd"
            sSql += "   AND e1.spccd  = r.spccd"
            sSql += "   AND j.bcno    = j1.bcno"
            sSql += "   AND j1.bcno   = r.bcno"
            sSql += "   AND j1.tclscd = r.tclscd"
            sSql += "   AND r.testcd  = f6.testcd"
            sSql += "   AND r.spccd   = f6.spccd"
            sSql += "   AND f6.usdt  <= r.tkdt"
            sSql += "   AND f6.uedt  >  r.tkdt"
            sSql += "   AND r.spccd   = f3.spccd"
            sSql += "   AND f3.usdt  <= r.tkdt"
            sSql += "   AND f3.uedt  >  r.tkdt"

            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

            If rsExLabCd <> "" Then
                sSql += "   AND e.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            End If

            If rsRegNo <> "" Then
                sSql += "   and j.regno = :regno"
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            End If

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_SpcInfo_ExLab(ByVal rsExLabCd As String, ByVal rsBcclsCd As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rbFlagAll As Boolean) As DataTable

        Dim sFn As String = "Function fnGet_SpcInfo_ExLab(string, string, string, string, boolean) As DataTable"
        Try
            Dim sSql As String
            Dim alParm As New ArrayList

            rsExLabCd = rsExLabCd.Replace("000", "")

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       j.bcno, r.testcd, f6.tnmd, r.spccd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.filenm, e1.remark,"
            'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.slipcd, r.tkdt) sort1,"
            sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
            sSql += "       NVL(f6.dispseqO, 999) sort2"
            sSql += "  FROM rj010m j, rj011m j1, rf060m f6, lf030m f3,"
            If rbFlagAll Then
                sSql += "       rr010m r"
            Else
                sSql += "       (SELECT bcno, tclscd, testcd, spccd, tkdt, rstflg FROM rr010m"
                sSql += "          where tkdt >= :dates"
                sSql += "            AND tkdt <= :datee || '235959'"
                sSql += "            AND (bcno, testcd) NOT IN"
                sSql += "                (SELECT r.bcno, r.testcd FROM rr010m r, rre11m e"
                sSql += "                  WHERE r.tkdt  >= :dates"
                sSql += "                    AND r.tkdt  <= :datee || '235959'"
                sSql += "                    AND r.bcno   = e.bcno"
                sSql += "                    AND r.testcd = e.testcd"
                sSql += "                )"
                sSql += "       ) r"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
            End If

            sSql += "       LEFT OUTER JOIN"
            sSql += "       rre11m e1 ON (r.bcno = e1.bcno AND r.testcd = e1.testcd)"
            sSql += " WHERE r.tkdt    >= :dates"
            sSql += "   AND r.tkdt    <= :datee || '235959'"
            sSql += "   AND j.bcno     = j1.bcno"
            sSql += "   AND j1.bcno    = r.bcno"
            sSql += "   AND j1.tclscd  = r.tclscd"
            sSql += "   AND r.testcd   = f6.testcd"
            sSql += "   AND r.spccd    = f6.spccd"
            sSql += "   AND r.tkdt    >= f6.usdt"
            sSql += "   AND r.tkdt    <  f6.uedt"
            sSql += "   AND f6.exlabyn = '1'"
            sSql += "   AND f6.tcdgbn <> 'C'"
            sSql += "   AND r.spccd    = f3.spccd"
            sSql += "   AND r.tkdt    >= f3.usdt"
            sSql += "   AND r.tkdt    <  f3.uedt"
            sSql += "   AND NVL(r.rstflg, '0') IN ('', '0')"
            sSql += "   AND j.spcflg  = '4'"
            sSql += "   AND j1.spcflg = '4'"

            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

            If rsBcclsCd <> "" Then
                sSql += "   and f6.bcclscd = :bcclscd"
                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
            End If

            If rsExLabCd <> "" Then
                sSql += "   and f6.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            End If


            sSql += " ORDER BY bcno, sort1, sort2, testcd"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_SpcInfo_ExLab(ByVal rsExLabCd As String, ByVal rsBcclsCd As String, ByVal rsBcNo As String, ByVal rbFlagAll As Boolean) As DataTable

        Dim sFn As String = "Function fnGet_SpcInfo_ExLab(string, string, string, boolean) As DataTable"
        Try
            Dim sSql As String
            Dim alParm As New ArrayList

            rsExLabCd = rsExLabCd.Replace("000", "")

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       j.bcno, r.testcd, f6.tnmd, r.spccd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.filenm, e1.remark,"
            'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.slipcd, r.tkdt) sort1,"
            sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
            sSql += "       NVL(f6.dispseqO, 999) sort2"
            sSql += "  FROM rj010m j, rj011m j1, rf060m f6, lf030m f3,"
            If rbFlagAll Then
                sSql += "       rr010m r"
            Else
                sSql += "       (SELECT bcno, tclscd, testcd, spccd, tkdt, rstflg FROM rr010m"
                sSql += "          where bcno = :bcno"
                sSql += "            AND (bcno, testcd) NOT IN"
                sSql += "                (SELECT r.bcno, r.testcd FROM rr010m r, rre11m e"
                sSql += "                  WHERE r.bcno   = :bcno"
                sSql += "                    AND r.bcno   = e.bcno"
                sSql += "                    AND r.testcd = e.testcd"
                sSql += "                )"
                sSql += "       ) r"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            End If

            sSql += "       LEFT OUTER JOIN"
            sSql += "       rre11m e1 ON (r.bcno = e1.bcno AND r.testcd = e1.testcd)"
            sSql += " WHERE r.bcno     = :bcno"
            sSql += "   AND j.bcno     = j1.bcno"
            sSql += "   AND j1.bcno    = r.bcno"
            sSql += "   AND j1.tclscd  = r.tclscd"
            sSql += "   and r.testcd   = f6.testcd"
            sSql += "   and r.spccd    = f6.spccd"
            sSql += "   and r.tkdt    >= f6.usdt"
            sSql += "   and r.tkdt    <  f6.uedt"
            sSql += "   and f6.exlabyn = '1'"
            sSql += "   aND f6.tcdgbn <> 'C'"
            sSql += "   and r.spccd    = f3.spccd"
            sSql += "   and r.tkdt    >= f3.usdt"
            sSql += "   and r.tkdt    <  f3.uedt"
            sSql += "   and NVL(r.rstflg, '0') IN ('', '0')"
            sSql += "   and j.spcflg = '4'"
            sSql += "   and j1.spcflg = '4'"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            If rsBcclsCd <> "" Then
                sSql += "   and f6.bcclscd = :bcclscd"
                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
            End If

            If rsExLabCd <> "" Then
                sSql += "   and f6.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            End If

            sSql += " ORDER BY bcno, sort1, sort2, testcd"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_SpcInfo(ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public fnGet_SpcInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT r.bcno, r.spccd, r.rstflg, r.orgrst, r.regno, j.spcflg, f.tnmd, NVL(f.titleyn, '0') titleyn, f.tcdgbn,"
            sSql += "       f.partcd || f.slipcd partslip"
            sSql += "  FROM rr010m r, rj010m j, rf060m f"
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd = :testcd"
            sSql += "   AND r.bcno   = j.bcno"
            sSql += "   AND r.testcd = f.testcd"
            sSql += "   AND r.spccd  = f.spccd"
            sSql += "   AND r.tkdt  >= f.usdt"
            sSql += "   AND r.tkdt  <  f.uedt"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_PatInfo_IdNo(ByVal rsBcNo As String) As String

        Dim sFn As String = "Function fnGet_PatInfo_IdNo(string, string) As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT fn_get_patinf(j.bcno, '', '') patinfo"
            sSql += "  FROM rj010m "
            sSql += " WHERE bcno = :bcno"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

            If dt.Rows.Count > 0 Then
                Dim sPatInfo() = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

                Return sPatInfo(6) + sPatInfo(7)
            End If

            Return ""

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function


    Public Shared Function fnGet_PartSlip_ExLab() As DataTable
        Dim sFn As String = "Function fnGet_PartSlip_ExLab(string, string) As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT f2.partcd || f2.slipcd partslip, f2.slipnmd"
            sSql += "  FROM rf021m f2, rf060m f6"
            sSql += " WHERE f2.partcd  = f6.partcd"
            sSql += "   AND f2.slipcd  = f6.slipcd"
            sSql += "   AND f2.usdt   <= fn_ack_sysdate"
            sSql += "   AND f2.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.exlabyn = '1'"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
           Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

End Class

