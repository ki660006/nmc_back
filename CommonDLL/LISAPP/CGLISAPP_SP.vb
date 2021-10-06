'*****************************************************************************************/
'/*                                                                                      */
'/* Project Name : Laboratory Information System(ACK@LIS)                                */
'/*                                                                                      */
'/*                                                                                      */
'/* FileName     : CGLISAPP_SP.vb                                                        */
'/* PartName     : 결과관리                                                              */
'/* Description  : 특수결과 관련 함수                                                    */
'/* Design       :                                                                       */
'/* Coded        :                                                                       */
'/* Modified     :                                                                       */
'/*                                                                                      */
'/*                                                                                      */
'/*                                                                                      */
'/****************************************************************************************/

'-- 왜 안올라가
Imports System.IO
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports COMMON.CommConst

Public Class APP_SP
    Private Const msFile As String = "File : CGLISAPP_SP.vb, Class : LISAPP.APP_SP.CommFn" + vbTab

    Public Shared Event OnQueryList(ByVal ra_sFieldName As String(), ByVal ra_sFieldValue As String())
    Public Shared Event OnReceivedBytes(ByVal riRcvLen As Integer, ByVal riFileLen As Integer)

    Private Shared Function fnCopyToBytes(ByVal r_a_btFrom As Byte(), ByRef r_a_btTo As Byte()) As Boolean
        Dim iIndexDest As Integer = 0
        Dim iLength As Integer = 0

        If r_a_btTo Is Nothing Then
            iIndexDest = 0
        Else
            iIndexDest = r_a_btTo.Length
        End If

        iLength = r_a_btFrom.Length

        ReDim Preserve r_a_btTo(iIndexDest + iLength - 1)

        Array.ConstrainedCopy(r_a_btFrom, 0, r_a_btTo, iIndexDest, iLength)
    End Function

    Private Shared Function fnCopyToBytes(ByVal r_a_btFrom As Byte(), ByRef r_a_btTo As Byte(), ByVal riLength As Integer) As Boolean
        Dim iIndexDest As Integer = 0

        If r_a_btTo Is Nothing Then
            iIndexDest = 0
        Else
            iIndexDest = r_a_btTo.Length
        End If

        If riLength > r_a_btFrom.Length Then
            riLength = 0
        End If

        ReDim Preserve r_a_btTo(iIndexDest + riLength - 1)

        Array.ConstrainedCopy(r_a_btFrom, 0, r_a_btTo, iIndexDest, riLength)
    End Function

    '-- 2009/04/09 특수보고서에서 의뢰의사 면허번호
    Public Shared Function fnGet_MediNoInfo_Sp(ByVal rsBcno As String) As DataTable
        Dim sFn As String = "Public Shared Function fnGet_MediNoInfo_Sp(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql = ""
            sSql += "SELECT b.medino"
            sSql += "  FROM rj010m a, rf322m b"
            sSql += " WHERE a.bcno     = :bcno"
            sSql += "   AND a.doctorcd = b.doctorcd"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    '-- lrs12m 정보
    Public Shared Function fnGet_AddFile_Binary(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsRstNo As String, Optional ByVal rbViewYn As Boolean = False) As String

        Dim sFn As String = "Public Shared Function Get_AddFile_Binary(String, String, string) As DataTable"

        Try
            Dim dbCmd As New OracleCommand
            Dim dbDA As OracleDataAdapter
            Dim dt As New DataTable

            Dim sSql As String = ""
            Dim sFileNm As String = ""

            sSql = ""
            sSql += "SELECT filenm, filelen FROM lrs12m"
            sSql += " WHERE bcno   = :bcno"
            sSql += "   AND testcd = :testcd"
            sSql += "   AND rstno  = :rstno"

            dbCmd.Connection = GetDbConnection()
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            dbDA = New OracleDataAdapter(dbCmd)

            With dbDA
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                .SelectCommand.Parameters.Add("rstno", OracleDbType.Varchar2).Value = rsRstNo
            End With

            dt.Reset()
            dbDA.Fill(dt)

            If dt.Rows.Count < 0 Then Return ""

            sFileNm = dt.Rows(0).Item("filenm").ToString

            sSql = ""
            sSql += "SELECT filebin FROM lrs12m"
            sSql += " WHERE bcno   = '" + rsBcNo + "'"
            sSql += "   AND testcd = '" + rsTestCd + "'"
            sSql += "   AND rstno  = " + rsRstNo

            Dim dbCmd_b As New OracleCommand(sSql, GetDbConnection())
            Dim dbDr As OracleDataReader = dbCmd_b.ExecuteReader(CommandBehavior.SequentialAccess)

            Dim o_fs As FileStream
            Dim bwrWriter As BinaryWriter
            Dim a_btReturn() As Byte
            Dim sFilePath As String = System.Windows.Forms.Application.StartupPath + "\SpTest\AddFile\"

            If rbViewYn Then sFilePath = "C:\ACK\AddFile\"

            Dim intSIndex As Integer = 0
            Dim lngRet As Long

            Try
                If My.Computer.FileSystem.DirectoryExists(sFilePath) Then
                    IO.Directory.Delete(sFilePath, True)
                End If
            Catch ex As Exception

            End Try

            If Dir(sFilePath, FileAttribute.Directory) = "" Then MkDir(sFilePath)

            Do While dbDr.Read()

                Dim sFile_Tmp As String = sFileNm.Substring(0, sFileNm.IndexOf(".") - 1) + "_t" + sFileNm.Substring(sFileNm.IndexOf("."))

                ' Create a file to hold the output.
                o_fs = New FileStream(sFilePath + sFile_Tmp, FileMode.OpenOrCreate, FileAccess.Write)
                bwrWriter = New BinaryWriter(o_fs)

                Dim iBufSize As Integer = 1048576
                Dim a_btBuffer(iBufSize - 1) As Byte

                intSIndex = 0
                lngRet = dbDr.GetBytes(0, intSIndex, a_btBuffer, 0, iBufSize)

                Do While lngRet = iBufSize
                    fnCopyToBytes(a_btBuffer, a_btReturn)

                    RaiseEvent OnReceivedBytes(a_btReturn.Length, iBufSize)

                    ReDim a_btBuffer(iBufSize - 1)

                    intSIndex += iBufSize
                    lngRet = dbDr.GetBytes(0, intSIndex, a_btReturn, 0, iBufSize)
                Loop

                fnCopyToBytes(a_btBuffer, a_btReturn, CType(lngRet, Integer))

                RaiseEvent OnReceivedBytes(a_btReturn.Length, iBufSize)
            Loop

            dbDr.Close()
            If a_btReturn Is Nothing Then Return ""

            Threading.Thread.Sleep(500)

            IO.File.WriteAllBytes(sFilePath + sFileNm, a_btReturn)
            Return sFilePath + sFileNm

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    '-- 2008/02/13 특수보고서에서 사용(접수시간, 검체코드 얻기)
    Public Shared Function fnGet_SpcInfo_TkSpcRegno(ByVal rsBcno As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim sTableNm As String = "lr010m"

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) Then sTableNm = "lm010m"

            sSql = ""
            sSql += "SELECT fn_ack_date_str(tkdt, 'yyyymmdd') tkdt, spccd, regno"
            sSql += "  FROM " + sTableNm
            sSql += " WHERE bcno   = :bcno"
            sSql += "   AND testcd = :testcd"


            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
            alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    '# 환자정보 조회 ( 검체번호 기준 )
    Public Shared Function fnGet_SpcInfo_bcno(ByVal rsBcNo As String) As DataTable
        Dim sFn As String = "Function fnGet_SpcInfo_bcno"

        Try
            Dim sTableNm As String = "lr010m"
            Dim sSql As String = ""

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
            sSql += "       j.regno,  j.sex || '/' || j.age sexage,"
            sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
            sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm, j.deptcd, j.wardno || '/' || j.roomno wardroom,"
            sSql += "       fn_ack_date_str(j.entdt, 'yyyy-mm-dd hh24:mi:ss') entdt,"
            sSql += "       CASE WHEN j.statgbn = '1' THEN 'Y' ELSE '' END statgbn, '' hw,"
            sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi:ss') colldt,"
            sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
            sSql += "       CASE WHEN j.rstflg = '2' THEN fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi:ss') ELSE '' END fndt,"
            sSql += "       f3.spcnmd, fn_ack_get_pat_diag_name(j.regno, j.orddt) diagnm, '' drugnm, j.spccd,"
            'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
            sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
            sSql += "          FROM lj011m ff"
            sSql += "         WHERE bcno    = j.bcno"
            sSql += "           AND spcflg IN ('1', '2', '3', '4')"
            sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
            sSql += "       ) doctorrmk,"
            sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
            sSql += "       '' wkno,"
            sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno"
            sSql += "  FROM lj010m j, lf030m f3,"
            sSql += "       (SELECT bcno, MAX(colldt) colldt FROM lj011m WHERE bcno = :bcno GROUP BY bcno) j1,"
            sSql += "       (select bcno, MAX(wkymd || NVL(wkgrpcd, '') || NVL(wkno, '')) workno, MAX(tkdt) tkdt, MAX(fndt) fndt"
            sSql += "          FROM " + sTableNm
            sSql += "         WHERE bcno = :bcno"
            sSql += "         GROUP BY bcno"
            sSql += "       ) r"
            sSql += " WHERE j.bcno   = :bcno"
            sSql += "   AND j.bcno   = r.bcno"
            sSql += "   AND j.bcno   = j1.bcno"
            sSql += "   AND j.spccd  = f3.spccd"
            sSql += "   AND r.tkdt  >= f3.usdt"
            sSql += "   AND r.tkdt   < f3.uedt"

            Dim al As New ArrayList

            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            DbCommand()
            Return DbExecuteQuery(sSql, al)


        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    '-- 2008/03/03 YOOEJ add(IMG)
    Public Shared Function fnGet_Rst_SpTest_img(ByVal rsBcNo As String) As DataTable
        Dim sFn As String = "Function fnGet_Rst_SpTest_img(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim sTableNm As String = "lr010m"

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       a.testcd, a.spccd, a.orgrst, a.viewrst"
            sSql += "  FROM " + sTableNm + " a, lf310m b"
            sSql += " WHERE a.bcno       = :bcno"
            sSql += "   AND a.testcd     = b.testcd"
            sSql += "   AND b.stsubexprg = 'IMG'"
            sSql += "   AND NVL(a.rstflg, '0') <> '3'"
            sSql += "   AND a.testcd NOT IN (SELECT testcd FROM lrs10m WHERE bcno = :bcno)"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    '-- 2008/02/21 YOOEJ add(TYPE1, TYPE2, TYPE3)
    Public Shared Function fnGet_Rst_SpTest_Sub(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsRstFlg As String) As DataTable
        Dim sFn As String = "Function fnGet_Rst_SpTest_Sub( String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim sTableNm As String = "lr010m"

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       testcd, spccd, orgrst, viewrst, rstcmt, eqflag"
            sSql += "  FROM " + sTableNm
            sSql += " WHERE bcno   = :bcno"
            sSql += "   AND testcd LIKE :testcd || '%'"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            If rsRstFlg = "" Then
                sSql += "   AND rstflg = '3'"
            Else
                sSql += "   AND NVL(rstflg, '0') < :rstflg"
                sSql += "   AND NVL(orgrst, ' ') <> ' '"

                alParm.Add(New OracleParameter("rstflg", OracleDbType.Varchar2, rsRstFlg.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstFlg))
            End If


            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    '-- 2008/02/13 YOODJ add(관련검사결과)
    Public Shared Function fnGet_Rst_SpTest_Ref(ByVal rsRegNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsTkDt As String) As DataTable
        Dim sFn As String = "Function fnGet_Rst_SpTest_Ref(String, String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            Dim alParm As New ArrayList

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       a.bcno, a.testcd, a.spccd, a.orgrst, a.viewrst"
            sSql += "  FROM lr010m a,"
            sSql += "       (SELECT MAX(r.bcno) bcno, r.testcd, r.spccd"
            sSql += "          FROM lj010m j, lr010m r,"
            sSql += "               (SELECT testcd, spccd, reftestcd, refspccd FROM lf063m"
            sSql += "                 WHERE testcd = :testcd"
            sSql += "                 UNION "
            sSql += "                SELECT a.testcd, a.spccd, b.testcd reftestcd, b.spccd refspccd"
            sSql += "                  FROM lf063m a, lf062m b"
            sSql += "                 WHERE a.testcd = :testcd"
            sSql += "                   AND a.testcd = b.tclscd"
            sSql += "                   AND a.spccd  = b.tspccd"
            sSql += "               ) f"
            sSql += "         WHERE j.regno  = :regno"
            sSql += "           AND j.spcflg = '4'"
            sSql += "           AND j.bcno   = r.bcno"
            sSql += "           AND r.tkdt  <= :tkdt"
            sSql += "           AND f.testcd = :testcd"
            sSql += "           AND f.spccd  = :spccd"
            sSql += "           AND r.rstflg = '3'"
            sSql += "           AND r.testcd = f.reftestcd"
            sSql += "           AND r.spccd  = f.refspccd"
            sSql += "         GROUP BY r.testcd, r.spccd"
            sSql += "        ) b"
            sSql += " WHERE a.bcno   = b.bcno"
            sSql += "   AND a.testcd = b.testcd"
            sSql += "   AND a.spccd  = b.spccd"

            alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            alParm.Add(New OracleParameter("tkdt", OracleDbType.Varchar2, rsTkDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDt))
            alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    '-- 특수검사 결과정보
    Public Shared Function fnGet_Rst_SpTest(ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Function fnGet_Rst_SpTest"

        Try

            Dim sSql As String = ""
            Dim sTableNm As String = "lr010m"

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

            sSql = ""
            sSql += "SELECT r.bcno,  r.testcd, r.spccd, NVL(r.orgrst, r.viewrst) orgrst,"
            sSql += "       r.regid, fn_ack_get_usr_name(r.regid) regnm, fn_ack_date_str(r.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
            sSql += "       r.mwid,  fn_ack_get_usr_name(r.mwid)  mwnm,  fn_ack_date_str(r.mwdt,  'yyyy-mm-dd hh24:mi:ss') mwdt,"
            sSql += "       r.fnid,  fn_ack_get_usr_name(r.fnid)  fnnm,  fn_ack_date_str(r.fndt,  'yyyy-mm-dd hh24:mi:ss') fndt,"
            sSql += "       NVL(r.rstflg, '0') rstflg, r.viewrst, r.rstcmt, rs.rstdt, rs.rstid, rs.rstflg, rs.rstrtf,"
            sSql += "       f.strsttxtr, f.strsttxtm, f.strsttxtf, f.stsubexprg"
            sSql += "  FROM " + sTableNm + " r, lrs10m rs, lf310m f"
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd = :testcd"
            sSql += "   AND r.testcd = f.testcd"
            sSql += "   AND r.bcno   = rs.bcno (+)"
            sSql += "   AND r.testcd = rs.testcd (+)"
            '  sSql += "   AND ROWNUM   = 1"

            Dim al As New ArrayList

            al.Add(New OracleParameter("bcno", rsBcNo))
            al.Add(New OracleParameter("testcd", rsTestCd))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    '-- 특수검사 결과정보
    Public Shared Function fnGet_Rst_SpTest_MULTI(ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Function fnGet_Rst_SpTest"

        Try

            Dim sSql As String = ""
            Dim sTableNm As String = "lr010m"

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

            sSql = ""
            sSql += "SELECT r.bcno,  r.testcd, r.spccd, NVL(r.orgrst, r.viewrst) orgrst,"
            sSql += "       r.regid, fn_ack_get_usr_name(r.regid) regnm, fn_ack_date_str(r.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
            sSql += "       r.mwid,  fn_ack_get_usr_name(r.mwid)  mwnm,  fn_ack_date_str(r.mwdt,  'yyyy-mm-dd hh24:mi:ss') mwdt,"
            sSql += "       r.fnid,  fn_ack_get_usr_name(r.fnid)  fnnm,  fn_ack_date_str(r.fndt,  'yyyy-mm-dd hh24:mi:ss') fndt,"
            sSql += "       NVL(r.rstflg, '0') rstflg, r.viewrst, r.rstcmt, rs.rstdt, rs.rstid, rs.rstflg, rs.rstrtf,"
            sSql += "       f.strsttxtr, f.strsttxtm, f.strsttxtf, f.stsubexprg"
            sSql += "  FROM " + sTableNm + " r"
            sSql += "  LEFT JOIN lrs10m rs"
            sSql += "         ON r.bcno = rs.bcno"
            sSql += "        AND r.testcd = rs.testcd"
            sSql += "  LEFT JOIN lf310m f"
            sSql += "         ON f.stsubseq = rs.migymd"
            sSql += "        AND r.testcd = f.testcd"
            sSql += " WHERE r.bcno = :bcno"
            sSql += "   AND r.testcd = :testcd"


           
            '  sSql += "   AND ROWNUM   = 1"

            Dim al As New ArrayList

            al.Add(New OracleParameter("bcno", rsBcNo))
            al.Add(New OracleParameter("testcd", rsTestCd))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    '-- 특수검사 대상자 조회
    Public Shared Function fnGet_SpcList_Sp_Tk(ByVal rsPartSlip As String, ByVal rsTkDtS As String, _
                                               ByVal rsTkDtE As String, ByVal rsOpt As String, ByVal rsTestCds As String, Optional ByVal riOpt As Integer = 0) As DataTable
        Dim sFn As String = "fnGet_SpcList_Sp_Tk"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            'A : 전체, F : 완료, NF : 미완료, NR : 미검사
            'If rsOpt = "NR" Then
            '    sSql = ""
            '    sSql += "SELECT DISTINCT"
            '    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm,"
            '    sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
            '    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd,"
            '    sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg, s.spcnmd, f.partcd || f.slipcd partslip"
            '    sSql += "  FROM lf060m f, lj010m j, lf030m s,"
            '    sSql += "       ("
            '    sSql += "  		 SELECT bcno, NVL(rstflg, '0') rstflg, tkdt, testcd, spccd, wkymd, wkgrpcd, wkno"
            '    sSql += " 		   FROM lr010m"
            '    sSql += "         WHERE tkdt >= :dates"
            '    sSql += " 		    AND tkdt <= :datee || '235959'"
            '    sSql += "           AND testcd IN (" + rsTestCds + ")"
            '    sSql += "           AND NVL(rstflg, '0') IN ('0', '1')"
            '    sSql += "         UNION "
            '    sSql += "  		 SELECT bcno, NVL(rstflg, '0') rstflg, tkdt, testcd, spccd, wkymd, wkgrpcd, wkno"
            '    sSql += " 		   FROM lm010m"
            '    sSql += "         WHERE tkdt >= :dates"
            '    sSql += " 		    AND tkdt <= :datee || '235959'"
            '    sSql += "           AND testcd IN (" + rsTestCds + ")"
            '    sSql += "           AND NVL(rstflg, '0') IN ('0', '1')"
            '    sSql += " 		) r"
            '    sSql += " WHERE j.bcno    = r.bcno"
            '    sSql += "   AND f.testcd  = r.testcd"
            '    sSql += "   AND f.spccd   = r.spccd"
            '    sSql += "   AND f.usdt   <= r.tkdt and f.uedt >  r.tkdt"
            '    sSql += "   AND s.spccd   = r.spccd"
            '    sSql += "   AND s.usdt   <= r.tkdt and s.uedt >  r.tkdt"
            '    sSql += "   AND j.spcflg  = '4'"
            '    sSql += "   AND NVL(r.wkymd, ' ') <> ' '"

            '    al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
            '    al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
            '    al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
            '    al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

            '    If rsPartSlip <> "" Then
            '        sSql += "   AND f.partcd = :partcd"
            '        sSql += "   AND f.slipcd = :slipcd"

            '        al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
            '        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
            '    End If

            'Else
            '    sSql = ""
            '    sSql += "SELECT /*+ INDEX(J PK_LJ010M, R IDX_LR010M_3) "
            '    sSql += "       DISTINCT"
            '    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm,"
            '    sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
            '    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd,"
            '    sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg, s.spcnmd, f.partcd || f.slipcd partslip"
            '    sSql += "  FROM lr010m r, lf060m f, lj010m j, lf030m s"
            '    sSql += " WHERE r.tkdt   >= :dates"
            '    sSql += "   AND r.tkdt   <= :datee || '235959'"
            '    sSql += "   AND r.testcd IN (" + rsTestCds + ")"
            '    sSql += "   AND j.bcno    = r.bcno"
            '    sSql += "   AND f.testcd  = r.testcd"
            '    sSql += "   AND f.spccd   = r.spccd"
            '    sSql += "   AND f.usdt   <= r.tkdt   AND f.uedt >  r.tkdt"
            '    sSql += "   AND s.spccd   = r.spccd"
            '    sSql += "   AND s.usdt   <= r.tkdt   AND s.uedt >  r.tkdt"
            '    sSql += "   AND j.spcflg   = '4'"

            '    al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
            '    al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

            '    Select Case rsOpt.Substring(0, 1)
            '        Case "N"
            '            sSql += "   AND r.rstflg = '2'"

            '        Case "F"
            '            sSql += "   AND r.rstflg = '3'"

            '        Case Else

            '    End Select

            '    sSql += "    AND NVL(r.wkymd, ' ') <> ' '"

            '    If rsPartSlip <> "" Then
            '        sSql += "   AND f.partcd = :partcd"
            '        sSql += "   AND f.slipcd = :slipcd"

            '        al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
            '        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
            '    End If

            '    sSql += " UNION "
            '    sSql += "SELECT /*+ INDEX(J PK_LJ010M, R IDX_LM010M_3) "
            '    sSql += "       DISTINCT"
            '    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm,"
            '    sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
            '    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd,"
            '    sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg, s.spcnmd, f.partcd || f.slipcd partslip"
            '    sSql += "  FROM lm010m r, lf060m f, lj010m j, lf030m s"
            '    sSql += " WHERE r.tkdt   >= :dates"
            '    sSql += "   AND r.tkdt   <= :datee || '235959'"
            '    sSql += "   AND r.testcd IN (" + rsTestCds + ")"
            '    sSql += "   AND j.bcno    = r.bcno"
            '    sSql += "   AND f.testcd  = r.testcd"
            '    sSql += "   AND f.spccd   = r.spccd"
            '    sSql += "   AND f.usdt   <= r.tkdt   AND f.uedt >  r.tkdt"
            '    sSql += "   AND s.spccd   = r.spccd"
            '    sSql += "   AND s.usdt   <= r.tkdt   AND s.uedt >  r.tkdt"
            '    sSql += "   AND j.spcflg   = '4'"

            '    al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
            '    al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

            '    Select Case rsOpt.Substring(0, 1)
            '        Case "N"
            '            sSql += "   AND r.rstflg = '2'"

            '        Case "F"
            '            sSql += "   AND r.rstflg = '3'"

            '        Case Else

            '    End Select

            '    sSql += "    AND NVL(r.wkymd, ' ') <> ' '"

            '    If rsPartSlip <> "" Then
            '        sSql += "   AND f.partcd = :partcd"
            '        sSql += "   AND f.slipcd = :slipcd"

            '        al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
            '        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
            '    End If

            'End If

            'sSql += " ORDER BY tkdt, workno, bcno"

            sSql = ""
            sSql += "SELECT DISTINCT" + vbCrLf
            sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm," + vbCrLf
            sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno," + vbCrLf
            sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd," + vbCrLf
            sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg, s.spcnmd, f.partcd || f.slipcd partslip" + vbCrLf
            sSql += "  FROM lf060m f, lj010m j, lf030m s," + vbCrLf
            sSql += "       (" + vbCrLf
            sSql += "  		 SELECT bcno, NVL(rstflg, '0') rstflg, tkdt, testcd, spccd, wkymd, wkgrpcd, wkno" + vbCrLf
            sSql += " 		   FROM lr010m" + vbCrLf
            sSql += "         WHERE tkdt >= :dates" + vbCrLf
            sSql += " 		    AND tkdt <= :datee || '235959'" + vbCrLf
            sSql += "           AND testcd IN (" + rsTestCds + ")" + vbCrLf

            Select Case rsOpt
                Case "NF"
                    sSql += "           AND rstflg = '2'" + vbCrLf

                Case "F"
                    sSql += "           AND rstflg = '3'" + vbCrLf
                Case "NR"
                    sSql += "           AND NVL(rstflg, '0') IN ('0', '1')" + vbCrLf
                Case Else

            End Select

            sSql += "         UNION " + vbCrLf
            sSql += "  		 SELECT bcno, NVL(rstflg, '0') rstflg, tkdt, testcd, spccd, wkymd, wkgrpcd, wkno" + vbCrLf
            sSql += " 		   FROM lm010m" + vbCrLf
            sSql += "         WHERE tkdt >= :dates" + vbCrLf
            sSql += " 		    AND tkdt <= :datee || '235959'" + vbCrLf
            sSql += "           AND testcd IN (" + rsTestCds + ")" + vbCrLf

            Select Case rsOpt
                Case "NF"
                    sSql += "           AND rstflg = '2'" + vbCrLf

                Case "F"
                    sSql += "           AND rstflg = '3'" + vbCrLf
                Case "NR"
                    sSql += "           AND NVL(rstflg, '0') IN ('0', '1')" + vbCrLf
                Case Else

            End Select

            sSql += " 		) r" + vbCrLf
            sSql += " WHERE j.bcno    = r.bcno" + vbCrLf
            sSql += "   AND f.testcd  = r.testcd" + vbCrLf
            sSql += "   AND f.spccd   = r.spccd" + vbCrLf
            sSql += "   AND f.usdt   <= r.tkdt and f.uedt >  r.tkdt" + vbCrLf
            sSql += "   AND s.spccd   = r.spccd" + vbCrLf
            sSql += "   AND s.usdt   <= r.tkdt and s.uedt >  r.tkdt" + vbCrLf
            sSql += "   AND j.spcflg  = '4'" + vbCrLf
            sSql += "   AND NVL(r.wkymd, ' ') <> ' '" + vbCrLf

            al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
            al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
            al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
            al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

            If rsPartSlip <> "" Then
                sSql += "   AND f.partcd = :partcd" + vbCrLf
                sSql += "   AND f.slipcd = :slipcd" + vbCrLf

                al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
            End If

            sSql += " ORDER BY tkdt, workno, bcno" + vbCrLf

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    '-- 특수검사 대상자 조회(결과일자)
    Public Shared Function fnGet_SpcList_Sp_RstDt(ByVal rsPartSlip As String, ByVal rsRstDtS As String, _
                                                  ByVal rsRstDtE As String, ByVal rsOpt As String, ByVal rsTestCds As String) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList
            Dim sTableNm As String = "lr010m"

            'A : 전체, F : 완료, NF : 미완료, NR : 미검사
            If rsOpt = "NR" Then
                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd,"
                sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg, s.spcnmd, f.partcd || f.slipcd partslip"
                sSql += "  FROM lf060m f, lj010m j, lf030m s,"
                sSql += "       ("
                sSql += "  	     SELECT bcno, NVL(rstflg, '0') rstflg, tkdt, testcd, spccd, wkymd, wkgrpcd, wkno"
                sSql += " 		   FROM lr010m"
                sSql += "         WHERE rstdt  >= :dates"
                sSql += " 		    AND rstdt  <= :datee || '235959'"
                sSql += "           AND testcd IN (" + rsTestCds + ")"
                sSql += "           AND NVL(rstflg, '0') in ('0', '1')"
                sSql += "         UNION "
                sSql += "  	     SELECT bcno, NVL(rstflg, '0') rstflg, tkdt, testcd, spccd, wkymd, wkgrpcd, wkno"
                sSql += " 		   FROM lm010m"
                sSql += "         WHERE rstdt  >= :dates"
                sSql += " 		    AND rstdt  <= :datee || '235959'"
                sSql += "           AND testcd IN (" + rsTestCds + ")"
                sSql += "           AND NVL(rstflg, '0') in ('0', '1')"
                sSql += " 		) r"
                sSql += " WHERE j.bcno    = r.bcno"
                sSql += "   AND f.testcd  = r.testcd"
                sSql += "   AND f.spccd   = r.spccd"
                sSql += "   AND f.usdt   <= r.tkdt   AND f.uedt >  r.tkdt"
                sSql += "   AND s.spccd   = r.spccd"
                sSql += "   AND s.usdt   <= r.tkdt   AND s.uedt >  r.tkdt"
                sSql += "   AND j.spcflg  = '4'"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                If rsPartSlip <> "" Then
                    sSql += "   AND f.partcd = :partcd"
                    sSql += "   AND f.slipcd = :slipcd"

                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If
            Else
                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd,"
                sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg, s.spcnmd, f.partcd || f.slipcd partslip"
                sSql += "  FROM lr010m r, lf060m f, lj010m j, lf030m s"
                sSql += " WHERE r.rstdt  >= :dates"
                sSql += "   AND r.rstdt  <= :datee || '235959'"
                sSql += "   AND r.testcd IN (" + rsTestCds + ")"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND r.spccd   = s.spccd"
                sSql += "   AND r.tkdt   >= s.usdt"
                sSql += "   AND r.tkdt   <  s.uedt"
                sSql += "   AND j.spcflg  = '4'"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                Select Case rsOpt.Substring(0, 1)
                    Case "N"
                        sSql += "   AND r.rstflg = '2'"

                    Case "F"
                        sSql += "   AND r.rstflg = '3'"

                    Case Else

                End Select

                If rsPartSlip <> "" Then
                    sSql += "   AND f.partcd = :partcd"
                    sSql += "   AND f.slipcd = :slipcd"

                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += " UNION "
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd,"
                sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg, s.spcnmd, f.partcd || f.slipcd partslip"
                sSql += "  FROM lm010m r, lf060m f, lj010m j, lf030m s"
                sSql += " WHERE r.rstdt  >= :dates"
                sSql += "   AND r.rstdt  <= :datee || '235959'"
                sSql += "   AND r.testcd IN (" + rsTestCds + ")"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND r.spccd   = s.spccd"
                sSql += "   AND r.tkdt   >= s.usdt"
                sSql += "   AND r.tkdt   <  s.uedt"
                sSql += "   AND j.spcflg  = '4'"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                Select Case rsOpt.Substring(0, 1)
                    Case "N"
                        sSql += "   AND r.rstflg = '2'"

                    Case "F"
                        sSql += "   AND r.rstflg = '3'"

                    Case Else

                End Select

                If rsPartSlip <> "" Then
                    sSql += "   AND f.partcd = :partcd"
                    sSql += "   AND f.slipcd = :slipcd"

                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

            End If
            sSql += " ORDER BY tkdt, workno, bcno"

            DbCommand()
            Return DbExecuteQuery(sSql, al)


        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    '-- 특수검사 대상자 조회(검체번호)
    Public Shared Function fnGet_SpcList_Sp_bcno(ByVal rsBcNo As String, Optional ByVal rsTestCd As String = "") As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim sTableNm As String = "lr010m"

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

            Dim al As New ArrayList

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm patnm,"
            sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
            sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd,"
            sSql += "       CASE WHEN j.rstflg = '2' THEN 'Y' ELSE 'N' END rstflg"
            sSql += "  FROM lf060m f, lj010m j, " + sTableNm + " r"
            sSql += " WHERE j.bcno    = :bcno"
            sSql += "   AND j.bcno    = r.bcno"
            sSql += "   AND f.testcd  = r.testcd"
            sSql += "   AND f.spccd   = r.spccd"
            sSql += "   AND f.usdt   <= r.tkdt AND f.uedt >  r.tkdt"
            sSql += "   AND j.spcflg  = '4'"
            sSql += "   AND f.tcdgbn IN ('S', 'P')"
            sSql += "   AND f.ctgbn   = '1'"

            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            If rsTestCd <> "" Then
                sSql += "   AND r.testcd = :testcd"
                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            End If

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    '-- 특수검사 대상자 조회(등록번호)
    Public Shared Function fnGet_SpcList_Sp_Regno(ByVal rsRegNo As String, ByVal rsPartSlip As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, ByVal rsTestCds As String) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += "SELECT j.bcno, fn_ack_get_bcno_full(j.bcno) cbcno, j.regno, j.patnm patnm,"
            sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) cworkno,"
            sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd, DECODE(j.rstflg, '3', 'Y', 'N') rstflg"
            sSql += "  FROM lj010m j, lr010m r, lf060m f"
            sSql += " WHERE j.regno   = :regno"
            sSql += "   AND j.bcno    = r.bcno"
            sSql += "   AND f.testcd  = r.testcd"
            sSql += "   AND f.spccd   = r.spccd"
            sSql += "   AND f.usdt   <= r.tkdt and f.uedt >  r.tkdt"
            sSql += "   AND j.spcflg  = '4'"
            sSql += "   AND f.tcdgbn IN ('S', 'P')"
            sSql += "   AND f.ctgbn   = '1'"
            sSql += "   AND j.owngbn <> 'H'"

            al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

            If rsTestCds.Length > 0 Then
                sSql += "   AND r.testcd IN (" + rsTestCds + ")"
            End If

            If rsTkDtS.Length * rsTkDtE.Length > 0 Then
                sSql += "    AND r.tkdt >= :dates"
                sSql += "    AND r.tkdt <= :datee || '235959'"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
            End If

            If rsPartSlip <> "" Then
                sSql += "   AND f.partcd = :partcd"
                sSql += "   AND f.slipcd = :slipcd"

                al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
            End If

            sSql += " UNION "
            sSql += "SELECT j.bcno, fn_ack_get_bcno_full(j.bcno) cbcno, j.regno, j.patnm patnm,"
            sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) cworkno,"
            sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, f.tnmd, DECODE(j.rstflg, '3', 'Y', 'N') rstflg"
            sSql += "  FROM lj010m j, lm010m r, lf060m f"
            sSql += " WHERE j.regno  = :regno"
            sSql += "   AND j.bcno   = r.bcno"
            sSql += "   AND f.testcd = r.testcd"
            sSql += "   AND f.spccd  = r.spccd"
            sSql += "   AND f.usdt  <= r.tkdt and f.uedt >  r.tkdt"
            sSql += "   AND j.spcflg = '4'"
            sSql += "   AND f.tcdgbn IN ('S', 'P')"
            sSql += "   AND f.ctgbn   = '1'"
            sSql += "   AND j.owngbn <> 'H'"

            al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

            If rsTestCds.Length > 0 Then
                sSql += "   AND r.testcd IN (" + rsTestCds + ")"
            End If

            If rsTkDtS.Length * rsTkDtE.Length > 0 Then
                sSql += "    AND r.tkdt >= :dates"
                sSql += "    AND r.tkdt <= :datee || '235959'"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
            End If

            If rsPartSlip <> "" Then
                sSql += "   AND f.partcd = :partcd"
                sSql += "   AND f.slipcd = :slipcd"

                al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
            End If

            sSql += " ORDER BY tkdt, cworkno, bcno"

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    '-- 처방의 검사소견
    Public Shared Function fnGet_Dr_TestCont_Sp(ByVal rsBcNo As String, ByVal rsTestCd As String) As String
        Dim sFn As String = "Function fnGet_Dr_TestCont_Sp"

        Try

            Dim sSql As String = ""

            sSql += "SELECT fn_ack_get_dr_testcont(:bcno, :testcd) drcont FROM DUAL"

            Dim al As New ArrayList

            al.Add(New OracleParameter("bcno", rsBcNo))
            al.Add(New OracleParameter("testcd", rsTestCd))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            If dt.Rows.Count < 1 Then Return ""

            Return dt.Rows(0).Item("drcont").ToString.Trim

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Shared Function fnGet_TestList_sp(ByVal rsPartSlip As String, ByVal rsWkDayS As String, ByVal rsWkDayE As String) As DataTable
        Dim sFn As String = "Function fnGet_TestList_sp(String, String, String)"

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT '1' chk, f60.testcd, f60.tnmd"
            sSql += "  FROM (SELECT testcd FROM lf310m GROUP BY testcd) f31,"
            sSql += "       (SELECT testcd, MIN(tnmd) tnmd,"
            sSql += " 	            MIN(tcdgbn) min_tc, MAX(tcdgbn) max_tc,"
            sSql += " 			    MIN(ctgbn)  min_ct, MAX(ctgbn)  max_ct,"
            sSql += " 			    MIN(partcd || slipcd) min_ps, MAX(partcd || slipcd) max_ps"
            sSql += " 	       FROM lf060m"
            sSql += " 		  WHERE usdt <= :dates"
            sSql += "           AND usdt <  :datee || '235959'"
            sSql += " 		  GROUP BY testcd"
            sSql += "        ) f60"
            sSql += "  WHERE ( (f60.min_tc = 'S' AND f60.max_tc = 'S') OR (f60.min_tc = 'P' AND f60.max_tc = 'P') )"
            'sSql += "    AND f60.min_ct = '1'"
            sSql += "    AND f60.max_ct = '1'"
            sSql += "    AND f60.testcd = f31.testcd"

            Dim al As New ArrayList

            al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsWkDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkDayS))
            al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsWkDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkDayE))

            If rsPartSlip.Length > 0 Then
                sSql += "    AND f60.min_ps = :partslip"
                sSql += "    AND f60.max_ps = :partslip"

                al.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                al.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
            End If

            DbCommand()
            Return DbExecuteQuery(sSql, al)


        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Shared Function fnGet_TestList_sp(ByVal rsPartSlip As String, ByVal rsWkDayS As String, ByVal rsWkDayE As String, _
                                             ByVal riUseMode As Integer, ByVal rsCd As String) As DataTable

        Dim sFn As String = "Function fnGet_TestList_sp(string, String, String, String, integer, String"

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT '1' chk, f60.testcd, f60.tnmd"
            sSql += "  FROM (SELECT testcd FROM lf310m GROUP BY  testcd) f31,"
            sSql += "       (SELECT testcd, MIN(tnmd) tnmd,"
            sSql += " 	            MIN(tcdgbn) min_tc, MAX(tcdgbn) max_tc,"
            sSql += " 			    MIN(ctgbn)  min_ct, MAX(ctgbn) max_ct,"
            sSql += " 			    MIN(partcd || slipcd) min_ps, MAX(partcd || slipcd) max_ps"
            sSql += " 	      FROM lf060m"
            sSql += " 		 WHERE usdt <= :dates || '000000'"
            sSql += "          AND uedt >  :datee || '235959'"

            'riUseMode = 0 --> 일반, riUseMode = 1 --> rsCd만 포함, riUseMode = 2 --> rsCd를 제외
            If rsCd <> "" Then
                Select Case riUseMode
                    Case 0

                    Case 1
                        sSql += "          AND testcd = :testcd"

                    Case 2
                        sSql += "          AND testcd <> :testcd"

                End Select
            End If

            sSql += " 		 GROUP BY testcd"
            sSql += "       ) f60"
            sSql += " WHERE ( (f60.min_tc = 'S' and f60.max_tc = 'S') or (f60.min_tc = 'P' and f60.max_tc = 'P') )"
            'sSql += "   AND f60.min_ct = '1'"
            sSql += "   AND f60.max_ct = '1'"
            sSql += "   AND f60.testcd = f31.testcd"

            Dim al As New ArrayList

            al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsWkDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkDayS))
            al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsWkDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkDayE))

            If rsCd <> "" Then
                'riUseMode = 0 --> 일반, riUseMode = 1 --> rsCd만 포함, riUseMode = 2 --> rsCd를 제외
                Select Case riUseMode
                    Case 0

                    Case 1, 2
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))

                End Select
            End If

            If rsPartSlip.Length > 0 Then
                sSql += "   AND f60.min_ps = :partslip"
                sSql += "   AND f60.max_ps = :partslip"

                al.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                al.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
            End If

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

End Class


