﻿'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : 관동대명지병원 Laboratory Information System(KMC_LIS)                  */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : RISAPP_F.vb                                                              */
'/* PartName     : 기초자료관리                                                           */
'/* Description  : 기초자료관리의 Data Query구문관련 Class                                */
'/* Design       : 2003-08-23 freety                                                      */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst
Imports COMMON.CommFN

Public Class APP_F
    Private Const msFile As String = "File : RISAPP_F.vb, Class : RISAPP.RISAPP_F" + vbTab

    Public Function GetUsUeCd_Test(ByVal rsCd1 As String, ByVal rsCd2 As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_Test() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT bcno  FROM rj011m"
            sSql += " WHERE tclscd  = :tclscd"
            sSql += "   AND spccd   = :spccd"
            sSql += "   AND colldt >= :usdt"
            sSql += "   AND ROWNUM  = 1"
            sSql += " UNION ALL "
            sSql += "SELECT bcno  FROM rr010m"
            sSql += " WHERE testcd  = :tclscd"
            sSql += "   AND spccd   = :spccd"
            sSql += "   AND tkdt   >= :usdt"
            sSql += "   AND ROWNUM  = 1"

            al.Add(New OracleParameter("tclscd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            al.Add(New OracleParameter("tclscd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Public Function GetUsUeDupl_Test(ByVal rsCd1 As String, ByVal rsCd2 As String, ByVal rsUsDt As String, ByVal rsUseTag As String, ByVal rsCompDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeDupl_Test() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += "SELECT a.*"
            sSql += "  FROM ("
            sSql += "        SELECT testcd, spccd, tnm, usdt, uedt"
            sSql += "          FROM rf060m"
            sSql += "         WHeRE testcd = :testcd"
            sSql += "           AND spccd  = :spccd"
            sSql += "           AND usdt   <" + IIf(rsUseTag = "USDT", "=", "").ToString + " :compdt"
            sSql += "           AND uedt   >" + IIf(rsUseTag = "USDT", "", "=").ToString + " :compdt"
            sSql += "       ) a LEFT OUTER JOIN"
            sSql += "       ("
            sSql += "        SELECT testcd, spccd, tnm, usdt, uedt"
            sSql += "          FROM rf060m"
            sSql += "         WHERE testcd = :testcd"
            sSql += "           AND spccd  = :spccd"
            sSql += "           AND usdt   = :usdt"
            sSql += "       ) b ON (a.testcd = b.testcd AND a.spccd = b.spccd AND a.usdt = b.usdt)"
            sSql += " WHERE NVL(b.testcd, ' ') = ' '"
            sSql += "   AND NVL(b.spccd,  ' ') = ' '"

            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Public Function GetUsUeCd_ExLab(ByVal rsCd As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_ExLab() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT exlabcd  FROM rf060m"
            sSql += " WHERE exlabcd = :exlabcd"

            al.Add(New OracleParameter("exlabcd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Public Function GetUsUeCd_Tube(ByVal rsCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_Tube() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT tubecd"
            sSql += "  FROM rf060m"
            sSql += " WHERE tubecd = :tubecd"

            al.Add(New OracleParameter("tubecd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))

            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Public Function GetUsUeDupl_Tube(ByVal rsCd As String, ByVal rsUsDt As String, ByVal rsUseTag As String, ByVal rsCompDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeDupl_Tube() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += "SELECT a.*"
            sSql += "  FROM ("
            sSql += "        SELECT tubecd, tubenmd, usdt, uedt"
            sSql += "          FROM lf040m"
            sSql += "         WHERE tubecd = :tubecd"
            sSql += "           AND usdt <" + IIf(rsUseTag = "USDT", "=", "").ToString + " :compdt"
            sSql += "           AND uedt >" + IIf(rsUseTag = "USDT", "", "=").ToString + " :compdt"
            sSql += "       ) a LEFT OUTER JOIN"
            sSql += "       ("
            sSql += "        SELECT tubecd, tubenmd, usdt, uedt"
            sSql += "          FROM lf040m"
            sSql += "         WHERE tubecd = :tubecd"
            sSql += "           AND usdt   = :usdt"
            sSql += "        ) b ON (a.tubecd = b.tubecd AND a.usdt = b.usdt)"
            sSql += " WHERE NVL(b.tubecd, ' ') = ' '"

            al.Add(New OracleParameter("tubecd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("tubecd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Public Function GetUsUeCd_Slip(ByVal rsCd1 As String, ByVal rsCd2 As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_Slip() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT testcd, spccd, tnm, usdt, uedt"
            sSql += "  FROM rf060m"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt  >= :usdt"

            al.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Public Function GetUsUeCd_Spc(ByVal rsCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_Test(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT spccd  FROM rj011m WHERE spccd = :spccd UNION ALL "
            sSql += "SELECT spccd  FROM rj011m WHERE spccd = :spccd UNION ALL "
            sSql += "SELECT spccd  FROM rr010m WHERE spccd = :spccd UNION ALL "
            sSql += "SELECT spccd  FROM lm010m WHERE spccd = :spccd UNION ALL "
            sSql += "SELECT spccd  FROM rr010m WHERE spccd = :spccd"

            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Public Function GetUsUeCd_CollTkCd(ByVal rsCmtGbn As String, ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_CollTkCD() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT cancelcd FROM rj030m"
            sSql += " WHERE cancelcd = :cancelcd"

            al.Add(New OracleParameter("cancelcd",  OracleDbType.Varchar2, (rsCmtGbn + rsCmtCd).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtGbn + rsCmtCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetUsUeDupl_Slip(ByVal rsCd1 As String, ByVal rsCd2 As String, ByVal rsUsDt As String, ByVal rsUseTag As String, ByVal rsCompDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_Slip() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += "SELECT a.*"
            sSql += "  FROM ("
            sSql += "        SELECT partcd, slipcd, slipnm, usdt, uedt"
            sSql += "          FROM rf021m"
            sSql += "         WHERE partcd = :partcd"
            sSql += "           AND slipcd = :slipcd"
            sSql += "           AND usdt   <" + IIf(rsUseTag = "USDT", "=", "").ToString + " :compdt"
            sSql += "           AND uedt   >" + IIf(rsUseTag = "USDT", "", "=").ToString + " :compdt"
            sSql += "       ) a LEFT OUTER JOIN"
            sSql += "       ("
            sSql += "        SELECT partcd, slipcd, slipnm, usdt, uedt"
            sSql += "          FROM rf021m"
            sSql += "         WHERE partcd = :partcd"
            sSql += "           AND slipcd = :slipcd"
            sSql += "           AND usdt   = :usdt"
            sSql += "        ) b ON (a.partcd = b.partcd AND a.slipcd = b.slipcd AND a.usdt = b.usdt)"
            sSql += " WHERE NVL(b.partcd, ' ') = ' '"
            sSql += "   AND NVL(b.slipcd, ' ') = ' '"

            al.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetUsUeDupl_bccls(ByVal rsCd1 As String, ByVal rsUsDt As String, ByVal rsUseTag As String, ByVal rsCompDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeDupl_bccls() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += "SELECT a.*"
            sSql += "  FROM ("
            sSql += "        SELECT bcclscd, bcclsnm, usdt, uedt"
            sSql += "          FROM rf010m"
            sSql += "         WHERE bcclscd = :bcclscd"
            sSql += "           AND usdt    <" + IIf(rsUseTag = "USDT", "=", "").ToString + " :compdt"
            sSql += "           AND uedt    >" + IIf(rsUseTag = "USDT", "", "=").ToString + " :compdt"
            sSql += "       ) a LEFT OUTER JOIN"
            sSql += "       ("
            sSql += "        SELECT bcclscd, bcclsnm, usdt, uedt"
            sSql += "          FROM rf010m"
            sSql += "         WHERE bcclscd = :bcclscd"
            sSql += "           AND usdt    = :usdt"
            sSql += "        ) b ON (a.bcclscd = b.bcclscd AND a.usdt = b.usdt)"
            sSql += " WHERE NVL(b.bcclscd, ' ') = :bcclscd"

            al.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            al.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetUsUeCd_bccls(ByVal rsBcclscd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_bccls() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT testcd, spccd, tnm, usdt, uedt"
            sSql += "  FROM rf060m"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt   >= :usdt"

            al.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclscd))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetUsUeCd_ordslip(ByVal rsOrdSlip As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_tordslip(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT testcd, spccd, tnm, usdt, uedt"
            sSql += "  FROM rf060m"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND uedt    >= fn_ack_sysdate"

            al.Add(New OracleParameter("tordslip",  OracleDbType.Varchar2, rsOrdSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdSlip))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetUsUeCd_Eq(ByVal rsCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeCd_Eqmt() As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT eqcd FROM rr010m"
            sSql += " WHERE eqcd = :eqcd"

            al.Add(New OracleParameter("eqcd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetNewUsDt() As DataTable
        Dim sFn As String = "Public Function GetNewUsDt() As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT TO_CHAR(SYSDATE, 'yyyymmdd') || '000000' newusdt FROM DUAL"

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetNewRegDt() As DataTable
        Dim sFn As String = "Public Function GetNewRegDt() As DataTable"

        Try
            Dim sSql As String = ""

            sSql = "SELECT fn_ack_sysdate FROM DUAL"

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    '-- 검사코드 리스트
    Public Function fnGet_testspc_autorst(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Function fnGet_testspc_autorst(String, String) As DataTable"
        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       '' chk, f6.tnmd, f6.tnmp tnmp, f6.testcd, f6.spccd, RPAD(f6.testcd, 8, ' ') || f6.spccd testspc,"
            sSql += "       f6.tordcd, f6.tordslip, f6.tcdgbn, f6.partcd || f6.slipcd partslip, NVL(f6.titleyn, '0') titleyn,"
            sSql += "       NVL(f6.mbttype, '0') mbttype, NVL(f6.poctyn, '0') poctyn,"
            sSql += "       f3.spcnmd, NVL(f2.dispseq, 999) sort1, NVL(f6.dispseql, 999) sort2"
            sSql += "  FROM rf060m f6, rf021m f2, lf030m f3"
            sSql += " WHERE f6.usdt <= fn_ack_sysdate"
            sSql += "   AND f6.uedt >  fn_ack_sysdate"

            If rsTestCd <> "" Then
                sSql += "   AND f6.testcd = :testcd"
                al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            End If

            If rsTestCd <> "" Then
                sSql += "   AND f6.spccd = :spccd"
                al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSPcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSPcCd))
            End If

            sSql += "   AND f6.testcd IN (SELECT testcd FROM rf083m)"
            sSql += "   AND f6.partcd = f2.partcd"
            sSql += "   AND f6.slipcd = f2.slipcd"
            sSql += "   AND f2.usdt  <= fn_ack_sysdate"
            sSql += "   AND f2.uedt   > fn_ack_sysdate"
            sSql += "   AND f6.spccd  = f3.spccd"
            sSql += "   AND f3.usdt  <= fn_ack_sysdate"
            sSql += "   AND f3.uedt   > fn_ack_sysdate"
            sSql += " ORDER BY sort1, sort2, testspc"

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetTestCdInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String, Optional ByVal rsUsDt As String = "") As DataTable
        Dim sFn As String = "Public Function GetTestCdInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT f6.testcd, f6.spccd,"
            sSql += "       MIN(CASE WHEN f6.tcdgbn = 'C' THEN '-- ' || f6.tnmd ELSE f6.tnmd END) tnmd, MIN(f3.spcnmd) spcnmd,"
            sSql += "       f6.tcdgbn, NVL(f6.mbttype, '0') mbttype, f6.bcclscd, f6.titleyn"
            sSql += "  FROM rf060m f6, lf030m f3"
            sSql += " WHERE f6.usdt  <= fn_ack_sysdate"
            sSql += "   AND f6.uedt  >  fn_ack_sysdate"
            sSql += "   AND f6.spccd  = f3.spccd"
            sSql += "   AND f3.usdt  <= fn_ack_sysdate"
            sSql += "   AND f3.uedt  >  fn_ack_sysdate"
            sSql += "   AND f6.testcd = :testcd"

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            If rsSpcCd <> "" Then
                sSql += "    AND f6.spccd = :spccd"
                alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            End If

            If rsUsDt <> "" Then
                sSql += "   AND f6.usdt = :usdt"
                alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            End If

            sSql += " GROUP BY f6.testcd, f6.spccd, f6.tcdgbn, f6.mbttype, f6.bcclscd, f6.titleyn"
            sSql += " ORDER BY testcd, spccd"


            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetBcclsInfo(ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetTSectInfo(ByVal asUSDT As String, ByVal asUEDT As String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT '[' || bcclscd || '] ' ||  bcclsnmd bcclsnmd"
            sSql += "  FROM rf010m"

            If rsUsDt = "" Then
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
            Else
                sSql += " WHERE usdt <= :usdt"
                sSql += "   AND uedt >  :usdt"

                alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            End If
            sSql += " ORDER BY bcclscd"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetSpcInfo(ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetSpcInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT '[' || spccd || '] ' ||  spcnmd spcnmd"
            sSql += "  FROM lf030m"
            sSql += " WHERE usdt <= :usdt"
            sSql += "   AND uedt >  :usdt"
            sSql += " ORDER BY spccd"

            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetTubeInfo(ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetTubeInfo(ByVal asUSDT As String, ByVal asUEDT As String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT '[' || tubecd || '] ' ||  tubenmd tubenmd"
            sSql += "  FROM lf040m"
            sSql += " WHERE usdt <= :usdt"
            sSql += "   AND uedt >  :usdt"
            sSql += " ORDER BY tubecd"

            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetTOrdSlipInfo(ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetTOrdSlipInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList


            sSql += "SELECT '[' || tordslip || '] ' || tordslipnm tordslipnmd"
            sSql += "  FROM lf100m"
            If rsUsDt = "" Then
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
            Else
                sSql += " WHERE usdt <= :usdt"
                sSql += "   AND uedt >  :usdt"

                alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            End If
            sSql += " ORDER BY dispseq, tordslip"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetExLabInfo() As DataTable
        Dim sFn As String = "Public Function GetExLabInfo() As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT '[' || exlabcd || '] ' || exlabnmd exlabnmd"
            sSql += "  FROM lf050m"
            sSql += " WHERE NVL(delflg, '0') = '0'"
            sSql += " ORDER BY exlabcd"


            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function ifExistOtherUsableData(ByVal rsCurTblNm As String, ByVal rsColNm1 As String, ByVal rsCd1 As String, ByVal rsUsDt As String) As String
        Dim sFn As String = "ifExistOtherUsableData"

        Try
            Dim sReturn As String = ""
            Dim sTblNm As String = ""

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT table_name"
            sSql += "  FROM user_tab_columns"
            sSql += " WHERE column_name = :colnm"

            alParm.Add(New OracleParameter("colnm",  OracleDbType.Varchar2, rsColNm1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsColNm1))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

            If dt.Rows.Count < 1 Then Return ""

            For ix As Integer = 0 To dt.Rows.Count - 1
                sTblNm = dt.Rows(ix).Item("table_name").ToString

                If sTblNm.StartsWith(rsCurTblNm) Then GoTo next_i
                If Not sTblNm.StartsWith("rf") Then GoTo next_i
                If sTblNm.EndsWith("H") Then GoTo next_i
                If sTblNm.IndexOf("rf061M") >= 0 Then GoTo next_i

                Fn.log(rsCurTblNm + " 관련 TABLE_NAME : " + sTblNm)

                sSql = ""
                sSql += "SELECT column_name"
                sSql += "  FROM user_tab_columns"
                sSql += " WHERE table_name  = :tblnm"
                sSql += "   AND column_name = 'UEDT'"

                alParm.Clear()
                alParm.Add(New OracleParameter("tblnm",  OracleDbType.Varchar2, sTblNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTblNm))

                DbCommand()
                Dim dt2 As DataTable = DbExecuteQuery(sSql, alParm)

                If dt2.Rows.Count > 0 Then
                    sSql = ""
                    sSql += "SELECT " + rsColNm1
                    sSql += "  FROM " + sTblNm + ""
                    sSql += " WHERE " + rsColNm1 + " = :val"
                    sSql += "   AND uedt > :uedt"

                    alParm.Clear()
                    alParm.Add(New OracleParameter("val",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
                    alParm.Add(New OracleParameter("uedt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

                    DbCommand()
                    Dim dt3 As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt3.Rows.Count < 0 Then
                        sReturn += "해당 코드를 사용하는 데이터 확인을 필요로 합니다!!(" + sTblNm + ")" + vbCrLf
                    End If
                Else
                    sSql = ""
                    sSql += "SELECT " + rsColNm1
                    sSql += "  FROM " + sTblNm + ""
                    sSql += " WHERE " + rsColNm1 + " = :val"

                    alParm.Clear()
                    alParm.Add(New OracleParameter("val",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))

                    DbCommand()
                    Dim dt3 As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt3.Rows.Count < 0 Then
                        sReturn += "해당 코드를 사용하는 데이터 확인을 필요로 합니다!!(" + sTblNm + ")" + vbCrLf
                    End If
                End If
next_i:
            Next

            Return sReturn
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function ifExistOtherUsableData(ByVal rsCurTblNm As String, ByVal rsColNm1 As String, ByVal rsColNm2 As String, ByVal rsCd1 As String, ByVal rsCd2 As String, ByVal rsUsDt As String) As String
        Dim sFn As String = "ifExistOtherUsableData"

        Try
            Dim sReturn As String = ""
            Dim sTblNm As String = ""

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT table_name"
            sSql += "  FROM user_tab_columns"
            sSql += " WHERE column_name = :colnm"
            sSql += " INTERSECT "
            sSql += "SELECT table_name"
            sSql += "  FROM user_tab_columns"
            sSql += " WHERE column_name = :colnm"

            alParm.Add(New OracleParameter("colnm",  OracleDbType.Varchar2, rsColNm1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsColNm1))
            alParm.Add(New OracleParameter("colnm",  OracleDbType.Varchar2, rsColNm2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsColNm2))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

            If dt.Rows.Count < 1 Then Return ""

            For ix As Integer = 0 To dt.Rows.Count - 1
                sTblNm = dt.Rows(ix).Item("table_name").ToString

                If sTblNm.StartsWith(rsCurTblNm) Then GoTo next_i
                If Not sTblNm.StartsWith("rf") Then GoTo next_i
                If sTblNm.EndsWith("H") Then GoTo next_i
                If Not sTblNm = "rf021M" Then GoTo next_i

                Fn.log(rsCurTblNm + " 관련 TABLE_NAME : " + sTblNm)

                sSql = ""
                sSql += "SELECT column_name"
                sSql += "  FROM user_tab_columns"
                sSql += " WHERE table_name = :tblnm"
                sSql += "   AND column_name = 'UEDT'"

                alParm.Clear()
                alParm.Add(New OracleParameter("tblnm",  OracleDbType.Varchar2, sTblNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTblNm))

                DbCommand()
                Dim dt2 As DataTable = DbExecuteQuery(sSql, alParm)

                If dt2.Rows.Count > 0 Then
                    sSql = ""
                    sSql += "SELECT " + rsColNm1
                    sSql += "  FROM " + sTblNm + ""
                    sSql += " WHERE " + rsColNm1 + " = :val1"
                    sSql += "   AND " + rsColNm2 + " = :val2"
                    sSql += "   AND uedt > :uedt"

                    alParm.Clear()
                    alParm.Add(New OracleParameter("val1",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
                    alParm.Add(New OracleParameter("val2",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))
                    alParm.Add(New OracleParameter("uedt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))


                    DbCommand()
                    Dim dt3 As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt3.Rows.Count > 0 Then
                        sReturn += "해당 코드를 사용하는 데이터 확인을 필요로 합니다!!(" + sTblNm & ")" + vbCrLf
                    End If
                Else
                    sSql = ""
                    sSql += "SELECT " + rsColNm1
                    sSql += "  FROM " + sTblNm + ""
                    sSql += " WHERE " + rsColNm1 + " = :val1"
                    sSql += "   AND " + rsColNm2 + " = :val2"

                    alParm.Clear()
                    alParm.Add(New OracleParameter("val1",  OracleDbType.Varchar2, rsCd1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd1))
                    alParm.Add(New OracleParameter("val2",  OracleDbType.Varchar2, rsCd2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd2))

                    DbCommand()
                    Dim dt3 As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt3.Rows.Count > 0 Then
                        sReturn += "해당 코드를 사용하는 데이터 확인을 필요로 합니다!!(" + sTblNm + ")" + vbCrLf
                    End If
                End If
next_i:
            Next

            Return sReturn
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function ifExistOtherUsableData(ByVal ra_RTblNm() As String, ByVal rsColNm1 As String, ByVal rsColNm2 As String, ByVal rsCd1 As String, ByVal rsCd2 As String, ByVal rsUsDt As String) As String
        Dim sFn As String = "ifExistOtherUsableData"

        Try
            Dim sReturn As String = ""
            Dim sTblNm As String = ""

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT table_name"
            sSql += "  FROM user_tab_columns"
            sSql += " WHERE column_name = :colnm"
            sSql += " INTERSECT "
            sSql += "SELECT table_name"
            sSql += "  FROM user_tab_columns"
            sSql += " WHERE column_name = :colnm"

            alParm.Add(New OracleParameter("colnm",  OracleDbType.Varchar2, rsColNm1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsColNm1))
            alParm.Add(New OracleParameter("colnm",  OracleDbType.Varchar2, rsColNm2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsColNm2))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

            If dt.Rows.Count < 1 Then Return ""

            For ix As Integer = 0 To dt.Rows.Count - 1
                sTblNm = dt.Rows(ix).Item("table_name").ToString

                For j As Integer = 0 To ra_RTblNm.Length - 1
                    If sTblNm.StartsWith(ra_RTblNm(j).ToString) Then GoTo next_i
                Next

                If Not sTblNm.StartsWith("rf") Then GoTo next_i
                If sTblNm.EndsWith("H") Then GoTo next_i

                Fn.log(ra_RTblNm(0).ToString + " 관련 TABLE_NAME : " + sTblNm)

                sSql = ""
                sSql += "SELECT column_name"
                sSql += "  FROM user_tab_columns"
                sSql += " WHERE table_name = :tblnm"
                sSql += "   AND column_name = 'UEDT'"

                alParm.Clear()
                alParm.Add(New OracleParameter("tblnm",  OracleDbType.Varchar2, sTblNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTblNm))

                DbCommand()
                Dim dt2 As DataTable = DbExecuteQuery(sSql, alParm)

                If dt2.Rows.Count > 0 Then
                    sSql = " SELECT " + rsColNm1 + ", " + rsColNm2
                    sSql += "  FROM " + sTblNm + ""
                    sSql += " WHERE " + rsColNm1 + " = '" + rsCd1 + "'"
                    sSql += "   AND " + rsColNm2 + " = '" + rsCd2 + "'"
                    sSql += "   AND uedt > '" + rsUsDt + "'"

                    Dim objDT3 As DataTable

                    DbCommand()
                    objDT3 = DbExecuteQuery(sSql)

                    If objDT3.Rows.Count > 0 Then
                        sReturn += "해당 코드를 사용하는 데이터 확인을 필요로 합니다!!(" & sTblNm & ")" & vbCrLf
                    End If
                Else
                    sSql = " SELECT " + rsColNm1 + ", " + rsColNm2
                    sSql += "  FROM " + sTblNm + ""
                    sSql += " WHERE " + rsColNm1 + " = '" + rsCd1 + "'"
                    sSql += "   AND " + rsColNm2 + " = '" + rsCd2 + "'"

                    Dim objDT3 As DataTable

                    DbCommand()
                    objDT3 = DbExecuteQuery(sSql)

                    If objDT3.Rows.Count > 0 Then
                        sReturn += "해당 코드를 사용하는 데이터 확인을 필요로 합니다!!(" & sTblNm & ")" & vbCrLf
                    End If
                End If
next_i:
            Next

            Return sReturn
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function


End Class

Public Class APP_F_KEYPAD
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_KEYPAD" & vbTab

    Public Function GetTestCdsInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetTestCdsInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim arlParm As New ArrayList

            sSql += "SELECT testcd, tnmd"
            sSql += "  FROM rf060m"
            sSql += " WHERE testcd LIKE :testcd || '%'"
            sSql += "   AND testcd <> :testcd"
            sSql += "   AND spccd  =  :spccd"
            sSql += "   AND usdt   <= fn_ack_sysdate"
            sSql += "   AND uedt   >  fn_ack_sysdate"

            sSql += " UNION "
            sSql += "SELECT a.testcd, b.tnmd"
            sSql += "  FROM rf062m a, rf060m b"
            sSql += " WHERE a.tclscd = :testcd"
            sSql += "   AND a.tspccd = :spccd"
            sSql += "   AND a.testcd = b.testcd"
            sSql += "   AND a.tspccd = b.spccd"
            sSql += "   AND b.usdt  <= fn_ack_sysdate"
            sSql += "   AND b.uedt  >  fn_ack_sysdate"

            arlParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            arlParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            arlParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            arlParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            arlParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, arlParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetKeyPadInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetKeyPadInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT testspc, tnmd, spcnmd, formgbn, testcd, spccd"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT"
                sSql += "               RPAD(f42.testcd, 6, ' ') || f42.spccd testspc,"
                sSql += "               f60.tnmd, f30.spcnmd, f42.testcd, f42.spccd,"
                sSql += "               CASE WHEN f42.formgbn = '0' THEN '숫자' ELSE '알파벳' END formgbn"
                sSql += "          FROM rf420m f42, rf060m f60, lf030m f30"
                sSql += "         WHERE f42.testcd = f60.testcd"
                sSql += "           AND f42.spccd  = f60.spccd"
                sSql += "           AND f42.spccd  = f30.spccd"
                sSql += "       ) a"

            ElseIf riMode = 1 Then
                sSql += "SELECT testspc, tnmd, spcnmd, formgbn, testcd, spccd, diffday, moddt, modid"
                sSql += "  FROM ("
                sSql += "        SELECT RPAD(f42.testcd, 6, ' ') || f42.spccd testspc,"
                sSql += "               f60.tnmd, f30.spcnmd, f42.testcd, f42.spccd,"
                sSql += "               CASE WHEN f42.formgbn = '0' THEN '숫자' ELSE '알파벳' END formgbn,"
                sSql += "               null diffday, '' moddt, '' modid"
                sSql += "          FROM rf420m f42, rf060m f60, lf030m f30"
                sSql += "         WHERE f42.testcd = f60.testcd"
                sSql += "           AND f42.spccd  = f60.spccd"
                sSql += "           AND f42.spccd  = f30.spccd"
                sSql += "         UNION ALL "
                sSql += "        SELECT RPAD(f42.testcd, 6, ' ') || f42.spccd testspc,"
                sSql += "               f60.tnmd, f30.spcnmd, f42.testcd, f42.spccd, "
                sSql += "               CASE WHEN f42.formgbn = '0' THEN '숫자' ELSE '알파벳' END formgbn, -1 diffday,"
                sSql += "               fn_ack_date_str(f42.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f42.modid"
                sSql += "          FROM rf420h f42, rf060m f60, lf030m f30"
                sSql += "        WHERE f42.testcd = f60.testcd"
                sSql += "          AND f42.spccd  = f60.spccd"
                sSql += "          AND f42.spccd  = f30.spccd"
                sSql += "       ) a"
                sSql += " ORDER BY testspc, moddt, modid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetKeyPadInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetKeyPadInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       a.testcd, a.spccd, a.formgbn, a.cnttestcd, a.pertestcd, a.wbctestcd, d.tnmd wbctnmd,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
            sSql += "       null moddt, null modid, null modnm, fn_ack_get_usr_name(a.regid) regnm,"
            sSql += "       b.tnmd, c.spcnmd"
            sSql += "  FROM rf060m b, lf030m c, rf420m a LEFT OUTER JOIN"
            sSql += "       rf060m d ON (a.wbctestcd = d.testcd AND a.spccd = d.spccd)"
            sSql += " WHERE a.testcd =  b.testcd"
            sSql += "   AND a.spccd  =  b.spccd"
            sSql += "   AND b.usdt   <= fn_ack_sysdate"
            sSql += "   AND b.uedt   >  fn_ack_sysdate"
            sSql += "   AND a.spccd  =  c.spccd"
            sSql += "   AND c.usdt   <= fn_ack_sysdate"
            sSql += "   AND c.uedt   >  fn_ack_sysdate"
            sSql += "   AND a.testcd =  :testcd"
            sSql += "   AND a.spccd  =  :spccd"

            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetKeyPadInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetKeyPadInfo(string, string, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       a.testcd, a.spccd, a.formgbn, a.cnttestcd, a.pertestcd, a.wbctestcd, d.tnmd wbctnmd,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
            sSql += "       fn_ack_date_str(a.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, a.modid,"
            sSql += "       fn_ack_get_usr_name(a.modid)  modnm, fn_ack_get_usr_name(a.regid) regnm,"
            sSql += "       b.tnmd, c.spcnmd"
            sSql += "  FROM rf060m b, lf030m c, rf420h a, rf060m d"
            sSql += " WHERE a.testcd    = b.testcd"
            sSql += "   AND a.spccd     = b.spccd"
            sSql += "   AND b.usdt     <= fn_ack_sysdate"
            sSql += "   AND b.uedt     >  fn_ack_sysdate"
            sSql += "   AND a.spccd     =  c.spccd"
            sSql += "   AND c.usdt     <= fn_ack_sysdate"
            sSql += "   AND c.uedt     >  fn_ack_sysdate"
            sSql += "   AND a.wbctestcd = d.testcd (+)"
            sSql += "   AND a.spccd     = d.spccd (+)"
            sSql += "   AND a.moddt     = :moddt"
            sSql += "   AND a.modid     = :modid"
            sSql += "   AND a.testcd    = :testcd"
            sSql += "   AND a.spccd     = :spccd"

            al.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            al.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModId))
            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function TransKeyPadInfo(ByVal rITcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsTestCd As String, ByVal rsSpcCd As String) As Boolean
        Dim sFn As String = "Public Function TransCalcInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction
        Dim dbCmd As New OracleCommand

        Try
            
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf420M :KEYPAD 설정 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With rITcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                If sValue = "" Then
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).value = DBNull.Value
                                Else
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).value = sValue
                                End If

                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf420m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With rITcol1
                        'rf420H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf420h SELECT fn_ack_sysdate, :mpdid, :modip, f.* FROM rf420m f"
                        sSql += " WHERE testcd = :testcd"
                        sSql += "   AND spccd  = :spccd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf420m"
                        sSql += " WHERE testcd = :testcd"
                        sSql += "   AND spccd  = :spccd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                If sValue = "" Then
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = DBNull.Value
                                Else
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End If
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf420M (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransKeyPadInfo_UE(ByVal rsTestCd As String, ByVal rsSpcCd As String) As Boolean
        Dim sFn As String = "Public Function TransCvtRstInfo_UE(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf420H Backup
            sSql = ""
            sSql += "INSERT INTO rf420h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf420m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()


            sSql = ""
            sSql += "DELETE rf420m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_CVT_RST
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : RISAPP.APP_F_CVT_RST" & vbTab

    Public Function GetCvtRstInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetCalcRstInfo(Integer, ByVal Serch As String) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT testspc, testcd, spccd, cvtform, cvtrange, cvtfldgbn, tnmd, spcnmd, rstcdseq, keypad, rstcont"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT"
                sSql += "               RPAD(f83.testcd, 8, ' ') || f83.spccd testspc,"
                sSql += "               f83.testcd, f83.spccd, f83.cvtform, f83.cvtrange, f83.cvtfldgbn, MIN(f60.tnmd) tnmd, MIN(f30.spcnmd) spcnmd,"
                sSql += "               f64.rstcdseq, f64.keypad, f64.rstcont"
                sSql += "          FROM rf084m f83, rf060m f60, lf030m f30, rf083m f64"
                sSql += "         WHERE f83.testcd   = f60.testcd"
                sSql += "           AND f83.spccd   IN ('" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "', f60.spccd)"
                sSql += "           AND f83.spccd   IN ('" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "', f30.spccd)"
                sSql += "           AND f83.testcd   = f64.testcd"
                sSql += "           AND f83.rstcdseq = f64.rstcdseq"
                sSql += "         GROUP BY f83.testcd, f83.spccd, f83.cvtform, f83.cvtrange, f83.cvtfldgbn, f64.rstcdseq, f64.keypad, f64.rstcont"
                sSql += "       ) a"
            ElseIf riMode = 1 Then
                sSql += "SELECT testspc, testcd, spccd, cvtform, cvtrange, cvtfldgbn, tnmd, spcnmd, rstcdseq, keypad, rstcont,"
                sSql += "       diffday, moddt, modid"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT"
                sSql += "               RPAD(f83.testcd, 8, ' ') || f83.spccd testspc,"
                sSql += "               f83.testcd, f83.spccd, f83.cvtform, f83.cvtrange, f83.cvtfldgbn, MIN(f60.tnmd) tnmd, MIN(f30.spcnmd) spcnmd,"
                sSql += "               f64.rstcdseq, f64.keypad, f64.rstcont,"
                sSql += "               NULL diffday, NULL moddt, NULL modid"
                sSql += "          FROM rf084m f83, rf060m f60, lf030m f30, rf083m f64"
                sSql += "         WHERE f83.testcd   = f60.testcd"
                sSql += "           AND f83.spccd   IN ('" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "', f60.spccd)"
                sSql += "           AND f83.spccd   IN ('" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "', f30.spccd)"
                sSql += "           AND f83.testcd   = f64.testcd"
                sSql += "           AND f83.rstcdseq = f64.rstcdseq"
                sSql += "         GROUP BY f83.testcd, f83.spccd, f83.cvtform, f83.cvtrange, f83.cvtfldgbn, f64.rstcdseq, f64.keypad, f64.rstcont"
                sSql += "         UNION ALL  "
                sSql += "        SELECT DISTINCT"
                sSql += "               RPAD(f83.testcd, 8, ' ') || f83.spccd testspc,"
                sSql += "               f83.testcd, f83.spccd, f83.cvtform, f83.cvtrange, f83.cvtfldgbn, MIN(f60.tnmd) tnmd, MIN(f30.spcnmd) spcnmd,"
                sSql += "               f64.rstcdseq, f64.keypad, f64.rstcont,"
                sSql += "               -1 diffday,"
                sSql += "               fn_ack_date_str(f83.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f83.modid"
                sSql += "          FROM rf084h f83, rf060m f60, lf030m f30, rf083m f64"
                sSql += "         WHERE f83.testcd   = f60.testcd"
                sSql += "           AND f83.spccd   IN ('" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "', f60.spccd)"
                sSql += "           AND f83.spccd   IN ('" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "', f30.spccd)"
                sSql += "           AND f83.testcd   = f64.testcd"
                sSql += "           AND f83.rstcdseq = f64.rstcdseq"
                sSql += "         GROUP BY f83.testcd, f83.spccd, f83.cvtform, f83.cvtrange, f83.cvtfldgbn, f64.rstcdseq, f64.keypad, f64.rstcont, f83.moddt, f83.modid"
                sSql += "       ) a"
                sSql += " ORDER BY testcd, spccd, moddt, modid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetRstCdInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String) As DataTable
        Dim sFn As String = "Public Function GetCalcRstInfo(String, String, string) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT rstcont FROM rf083m"
            sSql += " WHERE testcd   = :testcd"
            sSql += "   AND spccd    = :spccd"
            sSql += "   AND rstcdseq = :rstcd"

            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            al.Add(New OracleParameter("rstcd",  OracleDbType.Varchar2, rsRstCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
           Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetCvtRstInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String) As DataTable
        Dim sFn As String = "Public Function GetCalcRstInfo(String, String, string) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            If rsSpcCd = "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) Then
                sSql += "SELECT DISTINCT"
                sSql += "       a.testcd, a.spccd, a.rstcdseq, a.cvtfldgbn, a.cvttype, a.cvtrange, a.cvtview, a.cvtform,"
                sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
                sSql += "       fn_ack_get_usr_name(a.regid) regnm, null moddt, null modid, null modnm,"
                sSql += "       MAX(b.tnmd) tnmd, '' spcnmd, d.rstcont,"
                sSql += "       e.cvtparam, e.reflgbn, e.refl, e.refls, e.ctestcd, e.cspccd, MAX(f.tnmd) ctnmd, "
                sSql += "       e.refhs, e.refhgbn, e.refh, e.reflt, e.reflts, '' cspcnmd"
                sSql += "  FROM rf084m a, rf060m b, rf083m d,"
                sSql += "       rf085m e, rf060m f"
                sSql += " WHERE a.testcd = b.testcd"
                sSql += "   AND b.usdt    <= fn_ack_sysdate"
                sSql += "   AND b.uedt    >  fn_ack_sysdate"
                sSql += "   AND a.testcd   = d.testcd"
                sSql += "   AND a.rstcdseq = d.rstcdseq"
                sSql += "   AND a.testcd   = e.testcd"
                sSql += "   AND a.spccd    = e.spccd"
                sSql += "   AND a.rstcdseq = e.rstcdseq"
                sSql += "   AND e.ctestcd  = f.testcd"
                sSql += "   AND f.usdt    <= fn_ack_sysdate"
                sSql += "   AND f.uedt    >  fn_ack_sysdate"
                sSql += "   AND a.testcd   = :testcd"
                sSql += "   AND a.spccd    = :spccd"
                sSql += "   AND a.rstcdseq = :rstcd"
                sSql += " GROUP BY a.testcd, a.spccd, a.rstcdseq, a.cvtfldgbn, a.cvttype, a.cvtrange, a.cvtview, a.cvtform,"
                sSql += "          a.regdt, a.regid, a.regid, d.rstcont, e.cvtparam, e.reflgbn, e.refl, e.refls, e.ctestcd, e.cspccd,"
                sSql += "          e.refhs, e.refhgbn, e.refh, e.reflt, e.reflts"
            Else
                sSql += "SELECT DISTINCT"
                sSql += "       a.testcd, a.spccd, a.rstcdseq, a.cvtfldgbn, a.cvttype, a.cvtrange, a.cvtview, a.cvtform,"
                sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
                sSql += "       fn_ack_get_usr_name(a.regid) regnm, null moddt, null modid, null modnm,"
                sSql += "       b.tnmd, c.spcnmd, d.rstcont,"
                sSql += "       e.cvtparam, e.reflgbn, e.refl, e.refls, e.ctestcd, e.cspccd, f.tnmd ctnmd, "
                sSql += "       e.refhs, e.refhgbn, e.refh, e.reflt, e.reflts, g.spcnmd cspcnmd"
                sSql += "  FROM rf084m a, rf060m b, lf030m c, rf083m d,"
                sSql += "       rf085m e, rf060m f, lf030m g"
                sSql += " WHERE a.testcd = b.testcd"
                sSql += "   AND a.spccd  = b.spccd"
                sSql += "   AND b.usdt    <= fn_ack_sysdate"
                sSql += "   AND b.UEDT    >  fn_ack_sysdate"
                sSql += "   AND a.spccd    = c.spccd"
                sSql += "   AND c.usdt    <= fn_ack_sysdate"
                sSql += "   AND c.uedt    >  fn_ack_sysdate"
                sSql += "   AND a.testcd   = d.testcd"
                sSql += "   AND a.rstcdseq = d.rstcdseq"
                sSql += "   AND a.testcd   = e.testcd"
                sSql += "   AND a.spccd    = e.spccd"
                sSql += "   AND a.rstcdseq = e.rstcdseq"
                sSql += "   AND e.ctestcd  = f.testcd"
                sSql += "   AND e.cspccd   = f.spccd"
                sSql += "   AND f.usdt    <= fn_ack_sysdate"
                sSql += "   AND f.uedt    >  fn_ack_sysdate"
                sSql += "   AND e.spccd    = g.spccd"
                sSql += "   AND g.usdt    <= fn_ack_sysdate"
                sSql += "   AND g.uedt    >  fn_ack_sysdate"
                sSql += "   AND a.testcd   = :testcd"
                sSql += "   AND a.spccd    = :spccd"
                sSql += "   AND a.rstcdseq = :rstcd"
            End If


            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            al.Add(New OracleParameter("rstcd",  OracleDbType.Varchar2, rsRstCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
           Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetCvtRstInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String) As DataTable
        Dim sFn As String = "Public Function GetCalcRstInfo(string, string, String, String, string) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       a.testcd, a.spccd, a.rstcdseq, a.cvtfldgbn, a.cvttype, a.cvtrange, a.cvtview, a.cvtform,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
            sSql += "       fn_ack_get_usr_name(a.regid) regnm, null moddt, null modid, null modnm,"
            sSql += "       b.tnmd, c.spcnmd, d.rstcont,"
            sSql += "       e.cvtparam, e.reflgbn, e.refl, e.refls, e.ctestcd, e.cspccd, f.tnmd ctnmd, "
            sSql += "       e.refhs, e.refhgbn, e.refh, e.reflt, e.reflts, g.spcnmd cspcnmd"
            sSql += "  FROM rf084h a, rf060m b, lf030m c, rf083m d,"
            sSql += "       rf085h e, rf060m f, lf030m g"
            sSql += " WHERE a.testcd   = b.testcd"
            sSql += "   AND a.spccd    = b.spccd"
            sSql += "   AND b.usdt    <= fn_ack_sysdate"
            sSql += "   AND b.uedt    >  fn_ack_sysdate"
            sSql += "   AND a.spccd    = c.spccd"
            sSql += "   AND c.usdt    <= fn_ack_sysdate"
            sSql += "   AND c.uedt    >  fn_ack_sysdate"
            sSql += "   AND a.testcd   = d.testcd"
            sSql += "   AND a.rstcdseq = d.rstcdseq"
            sSql += "   AND a.testcd   = e.testcd"
            sSql += "   AND a.spccd    = e.spccd"
            sSql += "   AND a.rstcdseq = e.rstcdseq"
            sSql += "   AND e.ctestcd  = f.testcd"
            sSql += "   AND e.cspccd   = f.spccd"
            sSql += "   AND f.usdt    <= fn_ack_sysdate"
            sSql += "   AND f.uedt    >  fn_ack_sysdate"
            sSql += "   AND e.spccd    = g.spccd"
            sSql += "   AND g.usdt    <= fn_ack_sysdate"
            sSql += "   AND g.uedt    >  fn_ack_sysdate"
            sSql += "   AND a.moddt    = :moddt"
            sSql += "   AND a.modid    = :modid"
            sSql += "   AND a.testcd   = :testcd"
            sSql += "   AND a.spccd    = :spccd"
            sSql += "   AND a.rstcdseq = :rstcd"

            al.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            al.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModId))
            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            al.Add(New OracleParameter("rstcd",  OracleDbType.Varchar2, rsRstCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
           Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function TransCvtRstInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                     ByVal ro_Tcol2 As ItemTableCollection, ByVal riType2 As Integer, _
                                     ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String) As Boolean
        Dim sFn As String = "Public Function TransCalcInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf084M : 계산식 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue

                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf084m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf084H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf084h SELECT fn_ack_sysdate, :Modid, :modip, f.* FROM rf084m f"
                        sSql += " WHERE testcd   = :testcd"
                        sSql += "   AND spccd    = :spccd"
                        sSql += "   AND rstcdseq = :rstcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                        dbCmd.Parameters.Add("rstcd",  OracleDbType.Varchar2).Value = rsRstCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf084m"
                        sSql += " WHERE testcd   = :testcd"
                        sSql += "   AND spccd    = :spccd"
                        sSql += "   AND rstcdseq = :rstcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                        dbCmd.Parameters.Add("rstcd",  OracleDbType.Varchar2).Value = rsRstCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf084m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With
            End Select

            'rf085M : 계산식 검사내용
            Select Case riType2
                Case 0      '----- 신규
                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf085m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    'rf061H Backup
                    sSql = ""
                    sSql += "INSERT INTO rf085h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf085m f"
                    sSql += " WHERE testcd   = :testcd"
                    sSql += "   AND spccd    = :spccd"
                    sSql += "   AND rstcdseq = :rstcd"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                    dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                    dbCmd.Parameters.Add("rstcd",  OracleDbType.Varchar2).Value = rsRstCd

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rf085m"
                    sSql += " WHERE testcd   = :testcd"
                    sSql += "   AND spccd    = :spccd"
                    sSql += "   AND rstcdseq = :rstcd"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                    dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                    dbCmd.Parameters.Add("rstcd",  OracleDbType.Varchar2).Value = rsRstCd

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf085m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransCvtRstInfo_UE(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String) As Boolean
        Dim sFn As String = "Public Function TransCvtRstInfo_UE(String, String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "INSERT INTO rf084h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf084m f"
            sSql += " WHERE testcd   = :testcd"
            sSql += "   AND spccd    = :spccd"
            sSql += "   AND rstcdseq = :rstno"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("rstno",  OracleDbType.Varchar2).Value = rsRstCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf084m"
            sSql += " WHERE testcd   = :testcd"
            sSql += "   AND spccd    = :spccd"
            sSql += "   AND rstcdseq = :rstno"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("rstno",  OracleDbType.Varchar2).Value = rsRstCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf061H Backup
            sSql = ""
            sSql += "INSERT INTO rf085h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf085m f"
            sSql += " WHERE testcd   = :testcd"
            sSql += "   AND spccd    = :spccd"
            sSql += "   AND rstcdseq = :rstno"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("rstno",  OracleDbType.Varchar2).Value = rsRstCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf085m"
            sSql += " WHERE testcd   = :testcd"
            sSql += "   AND spccd    = :spccd"
            sSql += "   AND rstcdseq = :rstno"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("rstno",  OracleDbType.Varchar2).Value = rsRstCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()


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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_CVT_CMT
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : RISAPP_F.APP_F_CALC_CMT" + vbTab

    Public Function GetCmtInfo(ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetCmtInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT f8.cmtcd, f8.cmtcont,"
            sSql += "       '[' || NVL(f8.partcd, '') || NVL(f8.slipcd, '') || '] ' ||  NVL(f2.slipnmd,"
            sSql += "       CASE WHEN f8.cmtcd = '00' THEN '공통' ELSE f2.slipnmd END) slipnmd"
            sSql += "  FROM rf080m f8 LEFT OUTER JOIN "
            sSql += "       rf021m f2 ON (f8.partcd = f2.partcd AND f8.slipcd = f2.slipcd)"
            sSql += " WHERE f2.usdt <= fn_ack_sysdate"
            sSql += "   AND f2.uedt >  fn_ack_sysdate"
            If rsCmtCd <> "" Then
                sSql += "   AND f8.cmtcd = :cmtcd"
                alParm.Add(New OracleParameter("cmtcd",  OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))

            End If
            sSql += " ORDER BY cmtcd"

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function GetTestCdInfo(ByVal rsSlipCd As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetTestCdInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT f6.testcd, f6.spccd,"
            sSql += "       MIN(CAS WHEN f6.tcdgbn = 'C' TEHN '-- ' || f6.tnmd ELSE f6.tnmd END) tnmd, MIN(f3.spcnmd) spcnmd,"
            sSql += "       f6.tcdgbn, NVL(f6.mbttype, '0') mbttype, f6.titleyn"
            sSql += "  FROM rf060m f6, lf030m f3"
            sSql += " WHERE f6.usdt <= fn_ack_sysdate"
            sSql += "   AND f6.uedt >  fn_ack_sysdate"
            sSql += "   AND f6.spccd = f3.spccd"
            sSql += "   AND f3.usdt <= f6.usdt AND f3.uedt > f6.usdt"

            If rsSlipCd <> "" Then
                sSql += "   AND f6.partcd || f6.slipcd = :partslip"
                al.Add(New OracleParameter("partslip",  OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
            Else
                If rsTestCd <> "" Then
                    sSql += "   AND f6.testcd = :testcd"
                    al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsTestCd <> "" Then
                    sSql += "   AND f6.spccd = :spccd"
                    al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If
            End If
            sSql += " GROUP BY f6.testcd, f6.spccd, f6.tcdgbn, f6.mbttype, f6.titleyn"
            sSql += " ORDER BY testcd, spccd"

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetCvtCmtInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = " Public Function GetCvtCmtInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT cmtcd, cvtform, cmtcont"
                sSql += "  FROM ( "
                sSql += "        SELECT DISTINCT"
                sSql += "               f81.cmtcd, f81.cvtform, f80.cmtcont"
                sSql += "          FROM rf081m f81, rf080m f80"
                sSql += "         WHERE f81.cmtcd = f80.cmtcd"
                sSql += "       ) a"

            ElseIf riMode = 1 Then
                sSql += "SELECT cmtcd, cvtform, cmtcont, diffday, moddt, modid"
                sSql += "  FROM ("
                sSql += "        SELECT f81.cmtcd, f81.cvtform, f80.cmtcont,"
                sSql += "               NULL diffday, NULL moddt, NULL modid"
                sSql += "          FROM rf081m f81, rf080m f80"
                sSql += "         WHERE f81.cmtcd = f80.cmtcd"
                sSql += "         UNION ALL "
                sSql += "        SELECT f81.cmtcd, f81.cvtform, f80.cmtcont,"
                sSql += "               -1 diffday,"
                sSql += "               fn_ack_date_str(f81.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f81.modid"
                sSql += "          FROM rf081h f81, rf080m f80"
                sSql += "         WHERE f81.cmtcd = f80.cmtcd"
                sSql += "       ) a"
                sSql += " ORDER BY cmtcd, moddt, modid"

            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function GetCvtCmtInfo(ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetCalcRstInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       a.cmtcd, a.cvtform,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
            sSql += "        NULL moddt, NULL modid, NULL modnm, fn_ack_get_usr_name(a.regid) regnm,"
            sSql += "       '[' || b.partcd || b.slipcd || '] ' ||  NVL(c.slipnmd, '공통') slipnmd, b.cmtcont,"
            sSql += "       e.cvtparam, e.reflgbn, e.refl, e.refls, e.testcd, e.spccd, f.tnmd, e.refhs, e.refhgbn, e.refh, e.reflt, e.reflts, g.spcnmd spcnmd"
            sSql += "  FROM rf081m a, rf082m e, rf060m f, lf030m g,"
            sSql += "       rf080m b LEFT OUTER JOIN"
            sSql += "       rf021m c ON ( b.partcd = c.partcd AND b.slipcd = c.slipcd AND c.usdt <= fn_ack_sysdate AND c.uedt >  fn_ack_sysdate)"
            sSql += " WHERE a.cmtcd  = b.cmtcd"
            sSql += "   AND a.cmtcd  = e.cmtcd"
            sSql += "   AND e.testcd = f.testcd"
            sSql += "   AND e.spccd  = f.spccd"
            sSql += "   AND f.usdt  <= fn_ack_sysdate"
            sSql += "   AND f.uedt  >  fn_ack_sysdate"
            sSql += "   AND e.spccd  = g.spccd"
            sSql += "   AND g.usdt  <= fn_ack_sysdate"
            sSql += "   AND g.uedt  >  fn_ack_sysdate"
            sSql += "   AND a.cmtcd  = :cmtcd"

            al.Add(New OracleParameter("cmtcd",  OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function GetCvtCmtInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetCalcRstInfo(string, string, String, String, string) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       a.cmtcd, a.cvtform,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
            sSql += "       fn_ack_date_str(a.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, a.modid,"
            sSql += "       fn_ack_get_usr_name(a.modid) modnm, fn_ack_get_usr_name(a.regid) regnm,"
            sSql += "       '[' || b.partcd || b.slipcd || '] ' ||  NVL(c.slipnmd, '공통') slipnmd, b.cmtcont,"
            sSql += "       e.cvtparam, e.reflgbn, e.refl, e.refls, e.testcd, e.spccd, f.tnmd, e.refhs, e.refhgbn, e.refh, e.reflt, e.reflts, g.spcnmd spcnmd"
            sSql += "  FROM rf081h a, rf082m e, rf060m f, lf030m g,"
            sSql += "       rf080m b LEFT OUTER JOIN"
            sSql += "       rf021m c ON ( b.partcd = c.partcd AND b.slipcd = c.slipcd) AND c.usdt <= fn_ack_sysdate AND c.uedt >  fn_ack_sysdate"
            sSql += " WHERE a.cmtcd   = b.cmtcd"
            sSql += "   AND a.cmtcd   = e.cmtcd"
            sSql += "   AND e.testcd  = f.testcd"
            sSql += "   AND e.spccd   = f.spccd"
            sSql += "   AND f.usdt   <= fn_ack_sysdate"
            sSql += "   AND f.uedt   >  fn_ack_sysdate"
            sSql += "   AND e.spccd   = g.spccd"
            sSql += "   AND g.usdt   <= fn_ack_sysdate"
            sSql += "   AND g.uedt   >  fn_ack_sysdate"
            sSql += "   AND a.moddt   = :moddt"
            sSql += "   AND a.modid   = :modid"
            sSql += "   AND a.cmtcd   = :cmtcd"

            al.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            al.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModId))
            al.Add(New OracleParameter("cmtcd",  OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function TransCvtCmtInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                    ByVal ro_Tcol2 As ItemTableCollection, ByVal riType2 As Integer, _
                                    ByVal rsCmtCd As String) As Boolean
        Dim sFn As String = "Public Function TransCalcInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf084M : 계산식 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf081m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf081H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf081h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf081m f"
                        sSql += " WHERE cmtcd = :cmtcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf081m"
                        sSql += " WHERE cmtcd = :cmtcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf081m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

            'rf085M : 계산식 검사내용
            Select Case riType2
                Case 0      '----- 신규
                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf082m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    'rf061H Backup
                    sSql = ""
                    sSql += "INSERT INTO rf082h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf082m f"
                    sSql += " WHERE cmtcd = :cmtcd"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rf082m"
                    sSql += " WHERE cmtcd = :cmtcd"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf082m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransCvtCmtInfo_UE(ByVal rsCmtCd As String) As Boolean
        Dim sFn As String = "Public Function TransCvtRstInfo_UE(String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf081H Backup
            sSql = ""
            sSql += "INSERT INTO rf081h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf081m f"
            sSql += " WHERE cmtcd = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE FROM rf081m"
            sSql += " WHERE cmtcd = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf061H Backup
            sSql = ""
            sSql += "INSERT INTO rf082h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf082m f"
            sSql += " WHERE cmtcd = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf082m"
            sSql += " WHERE cmtcd = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
End Class

Public Class APP_F_COLLTKCD
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_COLLTKCD" + vbTab

    Private Const ms_Cmt0 As String = "병동"
    Private Const ms_Cmt1 As String = "진단검사"
    Private Const ms_Cmt2 As String = "환자특이사항"
    Private Const ms_Cmt3 As String = "미채혈사유"
    Private Const ms_CmtA As String = "특이결과"
    Private Const ms_CmtB As String = "결과수정 사유"
    Private Const ms_CmtC As String = "TAT 사유"
    Private Const ms_CmtD As String = "혈액은행 환자특이사항"
    Private Const ms_CmtE As String = "부적합검체 등록 사유"
    Private Const ms_CmtF As String = "혈액은행 접수취소 사유"
    Private Const ms_CmtG As String = "특이결과 조치내용"
    Private Const ms_CmtH As String = "수혈의뢰 사유"

    Public Function GetCmtGbnInfo(Optional ByVal rsGbn As String = "") As DataTable
        Dim sFn As String = "Public Function GetCmtGbnInfo() As DataTable"

        Try
            Dim sSql As String = ""

            If rsGbn = "" Then
                sSql += "SELECT '0' cmtgbncd, '" + ms_Cmt0 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT '1' cmtgbncd, '" + ms_Cmt1 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT 'E' cmtgbncd, '" + ms_CmtE + "' cmtgbnnm, 3 cmtgbnsort FROM DUAL"
            Else
                sSql += "SELECT '2' cmtgbncd, '" + ms_Cmt2 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT '3' cmtgbncd, '" + ms_Cmt3 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT 'A' cmtgbncd, '" + ms_CmtA + "' cmtgbnnm, 3 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT 'B' cmtgbncd, '" + ms_CmtB + "' cmtgbnnm, 4 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT 'C' cmtgbncd, '" + ms_CmtC + "' cmtgbnnm, 5 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT 'D' cmtgbncd, '" + ms_CmtD + "' cmtgbnnm, 6 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT 'F' cmtgbncd, '" + ms_CmtF + "' cmtgbnnm, 7 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT 'G' cmtgbncd, '" + ms_CmtG + "' cmtgbnnm, 8 cmtgbnsort FROM DUAL"
                sSql += " UNION ALL "
                sSql += "SELECT 'H' cmtgbncd, '" + ms_CmtH + "' cmtgbnnm, 9 cmtgbnsort FROM DUAL"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetCollTkCdInfo(ByVal riMode As Integer, ByVal rsCmtGbn As String) As DataTable
        Dim sFn As String = "Public Function GetCollTkCdInfo(Integer, String) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT cmtgbn_01, cmtcd, cmtcont, regdt, regid, diffday, cmtgbnsort, CASE WHEN DELFLG='0' THEN 'Y'WHEN DELFLG='1' THEN 'N' END AS USEYN"
                sSql += "  FROM ("
                sSql += "        SELECT '[' || cmtgbn || '] ' ||  b.cmtgbnnm cmtgbn_01, a.cmtcd, a.cmtcont, delflg,"
                sSql += "               fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid, null diffday, null moddt, null modid, b.cmtgbnsort"
                sSql += "          FROM rf410m a,"
                sSql += "               ("
                If rsCmtGbn = "" Then
                    sSql += "                SELECT '0' cmtgbncd, '" + ms_Cmt0 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL"
                    sSql += "                SELECT '1' cmtgbncd, '" + ms_Cmt1 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'E' cmtgbncd, '" + ms_CmtE + "' cmtgbnnm, 3 cmtgbnsort FROM DUAL"
                Else
                    sSql += "                SELECT '2' cmtgbncd, '" + ms_Cmt2 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT '3' cmtgbncd, '" + ms_Cmt3 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'A' cmtgbncd, '" + ms_CmtA + "' cmtgbnnm, 3 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'B' cmtgbncd, '" + ms_CmtB + "' cmtgbnnm, 4 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'C' cmtgbncd, '" + ms_CmtC + "' cmtgbnnm, 5 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'D' cmtgbncd, '" + ms_CmtD + "' cmtgbnnm, 6 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'F' cmtgbncd, '" + ms_CmtF + "' cmtgbnnm, 7 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'G' cmtgbncd, '" + ms_CmtG + "' cmtgbnnm, 8 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'H' cmtgbncd, '" + ms_CmtH + "' cmtgbnnm, 9 cmtgbnsort FROM DUAL"
                End If
                sSql += "               ) b"
                sSql += "         WHERE a.cmtgbn = b.cmtgbncd"
                sSql += "       ) a"
                sSql += " ORDER BY cmtgbnsort, cmtcd"
            ElseIf riMode = 1 Then
                sSql += "SELECT cmtgbn_01, cmtcd, cmtcont, regdt, regid, diffday, cmtgbnsort"
                sSql += "  FROM ("
                sSql += "        SELECT '[' || cmtgbn || '] ' ||  b.cmtgbnnm cmtgbn_01, a.cmtcd, a.cmtcont,"
                sSql += "               fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid, TO_NUMBER(NVL(a.delflg, '0')) * -1 diffday, b.cmtgbnsort"
                sSql += "          FROM rf410m a,"
                sSql += "               ("
                If rsCmtGbn = "" Then
                    sSql += "                SELECT '0' cmtgbncd, '" + ms_Cmt0 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL"
                    sSql += "                SELECT '1' cmtgbncd, '" + ms_Cmt1 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'E' cmtgbncd, '" + ms_CmtE + "' cmtgbnnm, 3 cmtgbnsort FROM DUAL"
                Else
                    sSql += "                SELECT '2' cmtgbncd, '" + ms_Cmt2 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT '3' cmtgbncd, '" + ms_Cmt3 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'A' cmtgbncd, '" + ms_CmtA + "' cmtgbnnm, 3 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'B' cmtgbncd, '" + ms_CmtB + "' cmtgbnnm, 4 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'C' cmtgbncd, '" + ms_CmtC + "' cmtgbnnm, 5 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'D' cmtgbncd, '" + ms_CmtD + "' cmtgbnnm, 6 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'F' cmtgbncd, '" + ms_CmtF + "' cmtgbnnm, 7 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'G' cmtgbncd, '" + ms_CmtG + "' cmtgbnnm, 8 cmtgbnsort FROM DUAL"
                    sSql += "                 UNION ALL "
                    sSql += "                SELECT 'H' cmtgbncd, '" + ms_CmtH + "' cmtgbnnm, 9 cmtgbnsort FROM DUAL"
                End If
                sSql += "               ) b"
                sSql += "         WHERE a.cmtgbn = b.cmtgbncd"
                sSql += "       ) a"
                sSql += " ORDER BY cmtgbnsort, cmtcd"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetCollTkCdInfo(ByVal rsCmtGbn As String, ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetCollTkCdInfo(Integer, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT '[' || a.cmtgbn || '] ' ||  b.cmtgbnnm cmtgbn_01, a.cmtcd, a.cmtcont, a.delflg,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid, NULL moddt, NULL modid, NULL modnm,"
            sSql += "       fn_ack_get_usr_name(a.regid) regnm"
            sSql += "  FROM rf410m a,"
            sSql += "       ("
            sSql += "        SELECT '0' cmtgbncd, '" + ms_Cmt0 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL"
            sSql += "        SELECT '1' cmtgbncd, '" + ms_Cmt1 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT '2' cmtgbncd, '" + ms_Cmt2 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT '3' cmtgbncd, '" + ms_Cmt3 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'A' cmtgbncd, '" + ms_CmtA + "' cmtgbnnm, 3 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'B' cmtgbncd, '" + ms_CmtB + "' cmtgbnnm, 4 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'C' cmtgbncd, '" + ms_CmtC + "' cmtgbnnm, 5 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'D' cmtgbncd, '" + ms_CmtD + "' cmtgbnnm, 6 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'E' cmtgbncd, '" + ms_CmtE + "' cmtgbnnm, 7 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'F' cmtgbncd, '" + ms_CmtF + "' cmtgbnnm, 8 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'G' cmtgbncd, '" + ms_CmtG + "' cmtgbnnm, 8 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'H' cmtgbncd, '" + ms_CmtH + "' cmtgbnnm, 9 cmtgbnsort FROM DUAL"
            sSql += "       ) b"
            sSql += " WHERE a.cmtgbn = b.cmtgbncd"
            sSql += "   AND a.cmtgbn = :cmtgbn"
            sSql += "   AND a.cmtcd  = :cmtcd"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("cmtgbn",  OracleDbType.Varchar2, rsCmtGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtGbn))
            alParm.Add(New OracleParameter("cmtcd",  OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetCollTkCdInfo(ByVal rsModDT As String, ByVal rsModID As String, ByVal rsCmtGbn As String, ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetCollTkCdInfo(String, String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT '[' || a.cmtgbn || '] ' ||  b.cmtgbnnm cmtgbn_01, a.cmtcd, a.cmtcont, a.delflg,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
            sSql += "       fn_ack_date_str(a.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, a.modid,"
            sSql += "       fn_ack_get_usr_name(a.modid) modnm, fn_ack_get_usr_name(a.regid) regnm"
            sSql += "  FROM rf410h a,"
            sSql += "       ("
            sSql += "        SELECT '0' cmtgbncd, '" + ms_Cmt0 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL"
            sSql += "        SELECT '1' cmtgbncd, '" + ms_Cmt1 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT '2' cmtgbncd, '" + ms_Cmt2 + "' cmtgbnnm, 1 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT '3' cmtgbncd, '" + ms_Cmt3 + "' cmtgbnnm, 2 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'A' cmtgbncd, '" + ms_CmtA + "' cmtgbnnm, 3 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'B' cmtgbncd, '" + ms_CmtB + "' cmtgbnnm, 4 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'C' cmtgbncd, '" + ms_CmtC + "' cmtgbnnm, 5 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'D' cmtgbncd, '" + ms_CmtD + "' cmtgbnnm, 6 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'E' cmtgbncd, '" + ms_CmtE + "' cmtgbnnm, 7 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'F' cmtgbncd, '" + ms_CmtF + "' cmtgbnnm, 8 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'G' cmtgbncd, '" + ms_CmtG + "' cmtgbnnm, 8 cmtgbnsort FROM DUAL"
            sSql += "         UNION ALL "
            sSql += "        SELECT 'H' cmtgbncd, '" + ms_CmtH + "' cmtgbnnm, 9 cmtgbnsort FROM DUAL"
            sSql += "       ) b"
            sSql += " WHERE a.cmtgbn = b.cmtgbncd"
            sSql += "   AND a.cmtgbn = :cmtgbn"
            sSql += "   AND a.cmtcd  = :cmtcd"
            sSql += "   AND a.moddt  = :moddt"
            sSql += "   AND a.modid  = :modid"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("cmtgbn",  OracleDbType.Varchar2, rsCmtGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtGbn))
            alParm.Add(New OracleParameter("cmtcd",  OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDT))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentCollTkCdInfo(ByVal rsGbnCd As String, ByVal rsCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentCollTkCdInfo(String, String) As DataTable"

        Dim sSql As String = ""

        Try
            sSql += "SELECT cmtgbn, cmtcd"
            sSql += "  FROM rf410m"
            sSql += " WHERE cmtgbn = :cmtgbn"
            sSql += "   AND cmtcd  = :cmtcd"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("cmtgbn",  OracleDbType.Varchar2, rsGbnCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsGbnCd))
            alParm.Add(New OracleParameter("cmtcd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    '< yjlee 2009-02-11
    Public Function fnGet_CollTK_Cancel_ContInfo(ByVal rsGbnCd As String) As DataTable
        Dim sFn As String = "Public Function fnGet_CollTK_Cancel_ContInfo(String) As DataTable"

        Dim sSql As String = ""

        Try
            sSql += "SELECT cmtgbn, cmtcd, cmtcont"
            sSql += "  FROM rf410m"
            sSql += " WHERE cmtgbn = :cmtgbn"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("cmtgbn",  OracleDbType.Varchar2, rsGbnCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsGbnCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function
    '> yjlee 2009-02-11

    Public Function TransCollTkCdInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                      ByVal rsCmtGbn As String, ByVal rsCmtCd As String) As Boolean
        Dim sFn As String = "Public Function TransCollTkCdInfo(ItemTableCollection, Integer,  String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf410m : 채혈/접수 취소 사유 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf410m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'LF410H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf410h "
                        sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                        sSql += "  FROM rf410m a"
                        sSql += " WHERE cmtgbn = :cmtgbn"
                        sSql += "   AND cmtcd  = :cmtcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("cmtgbn",  OracleDbType.Varchar2).Value = rsCmtGbn
                        dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf410m"
                        sSql += " WHERE cmtgbn = :cmtgbn"
                        sSql += "   AND cmtcd  = :cmtcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("cmtgbn",  OracleDbType.Varchar2).Value = rsCmtGbn
                        dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf410m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransCollTkCdInfo_DEL(ByVal rsCmtGbn As String, ByVal rsCmtCd As String) As Boolean
        Dim sFn As String = " Public Function TransCollTkCdInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "INSERT INTO rf410h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf410m f"
            sSql += " WHERE cmtgbn = :cmtgbn"
            sSql += "   AND cmtcd  = :cmtcd"

            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("cmtgbn",  OracleDbType.Varchar2).Value = rsCmtGbn
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf410m"
            sSql += " WHERE cmtgbn = :cmtgbn"
            sSql += "   AND cmtcd  = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("cmtgbn",  OracleDbType.Varchar2).Value = rsCmtGbn
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd


            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransCollTkCdInfo_UE(ByVal rsCmtGbn As String, ByVal rsCmtCd As String) As Boolean
        Dim sFn As String = "Public Function TransCollTkCdInfo_UE(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf410m : 채혈/접수 취소 사유
            sSql = ""
            sSql += "INSERT INTO lf410h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.*"
            sSql += "  FROM rf410m f"
            sSql += " WHERE cmtgbn = :cmtgbn"
            sSql += "   AND cmtcd  = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("cmtgbn",  OracleDbType.Varchar2).Value = rsCmtGbn
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf410m Delete
            sSql = ""
            sSql += "DELETE FROM rf410m"
            sSql += " WHERE cmtgbn = :cmtgbn"
            sSql += "   AND cmtcd  = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("cmtgbn",  OracleDbType.Varchar2).Value = rsCmtGbn
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
End Class

Public Class APP_F_ALERT_RULE
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_ALERT_RULE" & vbTab

    Public Function GetAlertRlue_Test() As DataTable
        Dim sFn As String = "Public Function GetAlertRuleInfo() As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT '[' || testcd || '] ' ||  MAX(tnmd) tnmd_01"
            sSql += "  FROM rf060m"
            sSql += " WHERE usdt <= fn_ack_sysdate"
            sSql += "   AND uedt >  fn_ack_sysdate"
            sSql += "   AND alertgbn IN ('A', 'B', 'C', '5')"
            sSql += " GROUP BY testcd"

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try


    End Function

    Public Function GetAlertRuleInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetAlertRuleInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT testcd, tnmd, regdt, regid"
                sSql += "  FROM ("
                sSql += "        SELECT f18.testcd, MAX(f60.tnmd) tnmd, fn_ack_date_str(f18.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f18.regid"
                sSql += "          FROM rf180m f18, rf060m f60"
                sSql += "         WHERE f18.testcd = f60.testcd"
                sSql += "           AND f60.usdt  <= f18.regdt"
                sSql += "           AND f60.uedt  >  f18.regdt"
                sSql += "         GROUP BY f18.testcd, f18.regdt, f18.regid"
                sSql += "       ) a"

            ElseIf riMode = 1 Then
                sSql += "SELECT testcd, tnmd, regdt, regid, diffday, moddt, modid"
                sSql += "   FROM ("
                sSql += "         SELECT f18.testcd, MAX(f60.tnmd) tnmd, NULL diffday, NULL moddt, NULL modid,"
                sSql += "                fn_ack_date_str(f18.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f18.regid"
                sSql += "           FROM rf180m f18, rf060m f60"
                sSql += "          WHERE f18.testcd = f60.testcd"
                sSql += "            AND f60.usdt  <= f18.regdt"
                sSql += "            AND f60.uedt  >  f18.regdt"
                sSql += "          GROUP BY f18.testcd, f18.regdt, f18.regid"
                sSql += "          UNION ALL"
                sSql += "         SELECT f18.testcd, MAX(f60.tnmd) tnmd,"
                sSql += "                -1 diffday, "
                sSql += "                fn_ack_date_str(f18.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f18.modid,"
                sSql += "                fn_ack_date_str(f18.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f18.regid"
                sSql += "           FROM rf180h f18, rf060m f60"
                sSql += "          WHERE f18.testcd = f60.testcd"
                sSql += "            AND f60.usdt  <= f18.regdt"
                sSql += "            AND f60.uedt  >  f18.regdt"
                sSql += "          GROUP BY f18.testcd, f18.moddt, f18.modid, f18.regdt, f18.regid"
                sSql += "        ) a"
                sSql += " ORDER BY testcd, moddt, modid"

            End If

            DbCommand()

            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function GetAlertRuleInfo(ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetAlertRuleInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT '[' || f18.testcd || '] ' ||  f60.tnmd tnmd_01,"
            sSql += "       f18.sex, f18.deptcds, f18.spccds, f18.orgrst, f18.viewrst, f18.eqflag, f18.baccds, f18.antic,"
            sSql += "       f18.panic, f18.delta,"
            sSql += "       fn_ack_date_str(f18.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f18.regid,"
            sSql += "       NULL moddt, NULL modid, NULL modnm, fn_ack_get_usr_name(f18.regid) regnm"
            sSql += "  FROM rf180m f18,"
            sSql += "       (SELECT testcd, MAX(tnmd) tnmd"
            sSql += "          FROM rf060m"
            sSql += "         GROUP BY testcd"
            sSql += "       ) f60"
            sSql += " WHERE f18.testcd = f60.testcd"
            sSql += "   AND f18.testcd = :testcd"

            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function GetAlertRuleInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetAlertRuleInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT '[' || f18.testcd || '] ' ||  f60.tnmd tnmd_01,"
            sSql += "       f18.sex, f18.deptcds, f18.spccds, f18.orgrst, f18.viewrst, F18.eqflag, f18.baccds, f18.antic,"
            sSql += "       f18.panic, f18.delta,"
            sSql += "       fn_ack_date_str(f18.regdt, 'yyyy-mm-dd hh24:mi:ss') retdt, f18.regid,"
            sSql += "       fn_ack_date_str(f18.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f18.modid,"
            sSql += "       fn_ack_get_usr_name(f18.modid) modnm, fn_ack_get_usr_name (f18.regid) regnm"
            sSql += "  FROM rf180h f18,"
            sSql += "       (SELECT testcd, MIN(tnmd) tnmd FROM rf060m GROUP BY testcd) f60"
            sSql += " WHERE f18.testcd = f60.testcd"
            sSql += "   AND f18.testcd = :testcd"
            sSql += "   AND f18.moddt  = :moddt"
            sSql += "   AND f18.modid  = :modid"

            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            al.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            al.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModId))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function GetRecentAlertRuleInfo(ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentAlertRuleInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim arlParm As New ArrayList

            sSql += "SELECT testcd FROM rf180m WHERE testcd = :testcd"

            arlParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, arlParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransAlertRuleInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsTestCd As String) As Boolean
        Dim sFn As String = "Public Function TransAlertRuleInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf180M : Alert Rule
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf180m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf180H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf180h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf180m f"
                        sSql += " WHERE testcd = :testcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf180m"
                        sSql += " WHERE testcd = :testcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf180m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransAlertRuleInfo_UE(ByVal rsTestCd As String) As Boolean
        Dim sFn As String = "Public Function TransAlertRuleInfo_UE(String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf180M : 계산식 마스터
            '   rf180H Insert
            sSql = ""
            sSql += "INSERT INTO rf180h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf180m f"
            sSql += " WHERE testcd = :testcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf180M Delete
            sSql = ""
            sSql += "DELETE rf180m"
            sSql += " WHERE testcd = :testcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_CALC
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_CALC" & vbTab

    Public Function GetCalcInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetCalcInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT testcd, spccd, tnmd, spcnmd"
                sSql += "  FROM ("
                sSql += "        SELECT f66.testcd, f66.spccd, MIN(f60.tnmd) tnmd, MIN(f30.spcnmd) spcnmd"
                sSql += "          FROM (rf069m f66 LEFT OUTER JOIN"
                sSql += "                rf060m f60 ON (f66.testcd = f60.testcd AND f66.spccd = f60.spccd)"
                sSql += "               ) LEFT OUTER JOIN lf030m f30 ON (f66.spccd = f30.spccd)"
                sSql += "         GROUP BY f66.testcd, f66.spccd"
                sSql += "       ) a"

            ElseIf riMode = 1 Then
                sSql += "SELECT testcd, spccd, tnmd, spcnmd, diffday, moddt, modid"
                sSql += "  FROM ("
                sSql += "        SELECT f66.testcd, f66.spccd, MIN(f60.tnmd) tnmd, MIN(f30.spcnmd) spcnmd,"
                sSql += "               NULL diffday, NULL moddt, NULL modid"
                sSql += "          FROM (rf069m f66 LEFT OUTER JOIN"
                sSql += "                rf060m f60 ON (f66.testcd = f60.testcd AND f66.spccd = f60.spccd)"
                sSql += "               ) LEFT OUTER JOIN lf030m f30 ON (f66.spccd = f30.spccd)"
                sSql += "         GROUP BY f66.testcd, f66.spccd"
                sSql += "         UNION ALL"
                sSql += "        SELECT f66.testcd, f66.spccd, MIN(f60.tnmd) tnmd, MIN(f30.spcnmd) spcnmd,"
                sSql += "               -1 diffday,"
                sSql += "               fn_ack_date_str(f66.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f66.modid modid"
                sSql += "          FROM (rf069h f66 LEFT OUTER JOIN"
                sSql += "                rf060m f60 ON (f66.testcd = f60.testcd AND f66.spccd = f60.spccd)"
                sSql += "               ) LEFT OUTER JOIN lf030m f30 ON (f66.spccd = f30.spccd)"
                sSql += "         GROUP BY  f66.testcd, f66.spccd, f66.moddt, f66.modid"
                sSql += "     ) a"
                sSql += " ORDER BY testcd, spccd, moddt, modid"

            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetCalcInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetCalcInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT f66.testcd, f66.spccd, f60.tnmd, f30.spcnmd, f66.calrange,"
            sSql += "       NVL(f66.caltype, 'M') caltype, f66.paramcnt,"
            sSql += "       f66.param0, f66.param1, f66.param2, f66.param3, f66.param4,"
            sSql += "       f66.param5, f66.param6, f66.param7, f66.param8, f66.param9, f66.calform,"
            sSql += "       fn_ack_date_str(f66.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f66.regid,"
            sSql += "       NULL moddt, NULL modid, NULL modnm, NVL(f66.calview, 'A') calview, f66.caldays,"
            sSql += "       fn_ack_get_usr_name(f66.regid) regnm"
            sSql += "  FROM (rf069m f66 LEFT OUTER JOIN"
            sSql += "        (SELECT testcd, spccd, MIN(tnmd) tnmd"
            sSql += "           FROM rf060m"
            sSql += "          GROUP BY testcd, spccd"
            sSql += "        ) f60 ON (f66.testcd = f60.testcd AND f66.spccd = f60.spccd)"
            sSql += "       ) LEFT OUTER JOIN"
            sSql += "       (SELECT spccd, MIN(spcnmd) spcnmd"
            sSql += "          FROM lf030m"
            sSql += "         GROUP BY spccd"
            sSql += "       ) f30 ON (f66.spccd = f30.spccd)"
            sSql += " WHERE f66.testcd = :testcd"
            sSql += "   AND f66.spccd  = :spccd"

            al.Add(New OracleParameter("testcd", rsTestCd))
            al.Add(New OracleParameter("spccd", rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetCalcInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetCalcInfo(String, String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += " SELECT f66.testcd, f66.spccd, f60.tnmd, f30.spcnmd, f66.calrange,"
            sSql += "        NVL(f66.caltype, 'M') caltype, f66.paramcnt,"
            sSql += "        f66.param0, f66.param1, f66.param2, f66.param3, f66.param4,"
            sSql += "        f66.param5, f66.param6, f66.param7, f66.param8, f66.param9, f66.calform,"
            sSql += "        fn_ack_date_str(f66.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f66.regid,"
            sSql += "        fn_ack_date_str(f66.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f66.modid,"
            sSql += "        fn_ack_get_usr_name(f66.modid) modnm,"
            sSql += "        NVL(f66.calview, 'A') calview, f66.caldays, fn_ack_get_usr_name(f66.regid) regnm"
            sSql += "  FROM (rf069h f66 LEFT OUTER JOIN"
            sSql += "        (SELECT testcd, spccd, MIN(tnmd) tnmd"
            sSql += "           FROM rf060m"
            sSql += "          GROUP BY testcd, spccd"
            sSql += "        ) f60 ON (f66.testcd = f60.testcd AND f66.spccd = f60.spccd)"
            sSql += "       ) LEFT OUTER JOIN"
            sSql += "       (SELECT spccd, MIN(spcnmd) spcnmd"
            sSql += "          FROM lf030m"
            sSql += "         GROUP BY spccd"
            sSql += "       ) f30 ON (f66.spccd = f30.spccd)"
            sSql += "  WHERE f66.testcd = :testcd"
            sSql += "    AND f66.spccd  = :spccd"
            sSql += "    AND f66.moddt  = :moddt"
            sSql += "    AND f66.modid  = :modid"

            al.Add(New OracleParameter("testcd", rsTestCd))
            al.Add(New OracleParameter("spccd", rsSpcCd))
            al.Add(New OracleParameter("moddt", rsModDt))          '                                 
            al.Add(New OracleParameter("modid", rsModId))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentCalcInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentCalcInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT testcd, spccd"
            sSql += "  FROM rf069m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransCalcInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsTestCd As String, ByVal rsSpcCd As String) As Boolean
        Dim sFn As String = "Public Function TransCalcInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf069M : 계산식 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf069m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf069H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf069h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf069m f"
                        sSql += " WHERE testcd = :testcd"
                        sSql += "   AND spccd  = :spccd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf069m"
                        sSql += " WHERE testcd = :testcd"
                        sSql += "   AND spccd  = :spccd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf069m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransCalcInfo_UE(ByVal rsTestCd As String, ByVal rsSpcCd As String) As Boolean
        Dim sFn As String = "Public Function TransCalcInfo_UE(String, String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf069M : 계산식 마스터
            '   rf069H Insert
            sSql = ""
            sSql += "INSERT INTO rf069h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf069m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf069M Delete
            sSql = ""
            sSql += "DELETE rf069m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
End Class

Public Class APP_F_CMT
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : DA01.APP_F_CMT" & vbTab

    Public Function GetCmtInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetCmtInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT cmtcd, cmtcont, slipnmd slipnmd_01"
                sSql += "  FROM ("
                sSql += "        SELECT f8.cmtcd, f8.cmtcont, '[' || f8.partcd || f8.slipcd || '] ' ||  NVL(f2.slipnmd, CASE WHEN f8.partcd || f8.slipcd = '00' THEN '공통' ELSE NVL(f2.slipnmd, '') END ) slipnmd"
                sSql += "          FROM rf080m f8 LEFT OUTER JOIN rf021m f2 ON (f8.partcd = f2.partcd AND f8.slipcd = f2.slipcd)"
                sSql += "       ) a"
                sSql += " ORDER BY cmtcd"
            ElseIf riMode = 1 Then
                sSql += "SELECT cmtcd, cmtcont, slipnmd slipnmd_01, diffday, moddt, modid"
                sSql += "  FROM ("
                sSql += "        SELECT f8.cmtcd, f8.cmtcont, '[' || f8.partcd || f8.slipcd || '] ' ||  NVL(f2.slipnmd, CASE WHEN f8.partcd || f8.slipcd = '00' THEN '공통' ELSE NVL(f2.slipnmd, '') END) slipnmd,"
                sSql += "               NULL diffday, NULL moddt, NULL modid"
                sSql += "          FROM rf080m f8 LEFT OUTER JOIN rf021m f2 ON (f8.partcd = f2.partcd AND f8.slipcd = f2.slipcd)"
                sSql += "         UNION ALL"
                sSql += "        SELECT f8.cmtcd, f8.cmtcont, '[' || f8.partcd || f8.slipcd || '] ' ||  NVL(f2.slipnmd, CASE WHEN f8.partcd || f8.slipcd = '00' THEN '공통' ELSE NVL(f2.slipnmd, '') END) slipnmd,"
                sSql += "               -1 diffday,"
                sSql += "               fn_ack_date_str(f8.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f8.modid"
                sSql += "          FROM rf080h f8 LEFT OUTER JOIN rf021m f2 ON (f8.partcd = f2.partcd AND f8.slipcd = f2.slipcd)"
                sSql += "       ) a"
                sSql += " ORDER BY cmtcd, moddt, modid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetCmtInfo(ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetCmtInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT cmtcd, cmtcont, slipnmd slipnmd_01, regdt, regid, fn_ack_get_usr_name(regid) regnm"
            sSql += "  FROM ("
            sSql += "        SELECT f8.cmtcd, f8.cmtcont, '[' || f8.partcd || f8.slipcd || '] ' ||  NVL(f2.slipnmd, CASE WHEN f8.partcd || f8.slipcd = '00' THEN '공통' ELSE NVL(f2.slipnmd, '') END) slipnmd,"
            sSql += "               fn_ack_date_str(f8.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f8.regid"
            sSql += "          FROM rf080m f8 LEFT OUTER JOIN rf021m f2 ON (f8.partcd = f2.partcd AND f8.slipcd = f2.slipcd)"
            sSql += "       ) a"
            sSql += " WHERE cmtcd = :cmtcd"

            alParm.Add(New oracleParameter("cmtcd", rsCmtCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetCmtInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetCmtInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT cmtcd, cmtcont, slipnmd slipnmd_01, regdt, regid, moddt, modid, fn_ack_get_usr_name(regid) regnm, fn_ack_get_usr_name(modid) modnm"
            sSql += "  FROM ("
            sSql += "        SELECT f8.cmtcd, f8.cmtcont, '[' || f8.partcd || f8.slipcd || '] ' ||  NVL(f2.slipnmd, CASE WHEN f8.partcd || f8.slipcd = '00' THEN '공통' ELSE NVL(f2.slipnmd, '') END) slipnmd,"
            sSql += "               fn_ack_date_str(f8.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f8.regid,"
            sSql += "               fn_ack_date_str(f8.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f8.modid"
            sSql += "          FROM rf080h f8 LEFT OUTER JOIN rf021m f2 ON (f8.partcd = f2.partcd AND f8.slipcd = f2.slipcd)"
            sSql += "       ) a"
            sSql += " WHERE a.moddt = :moddt"
            sSql += "   AND a.modid = :modid"
            sSql += "   AND a.cmtcd = :cmtcd"

            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModId))
            alParm.Add(New OracleParameter("cmtcd",  OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function GetRecentCmtInfo(ByVal rsCmtCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentCmtInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT cmtcd FROM rf080m"
            sSql += " WHERE cmtcd = :cmtcd"

            alParm.Add(New OracleParameter("cmtcd",  OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransCmtInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsCmtCd As String) As Boolean
        Dim sFn As String = "Public Function TransCmtInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf080M : 소견 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf080m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf080H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf080h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf080m f"
                        sSql += " WHERE cmtcd = :cmtcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf080m"
                        sSql += " WHERE cmtcd = :cmtcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf080m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransCmtInfo_UE(ByVal rsCmtCd As String) As Boolean
        Dim sFn As String = "Public Function TransCmtInfo_UE(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf080M : 소견 마스터
            '   rf080H Insert
            sSql = ""
            sSql += "INSERT INTO rf080h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf080m f"
            sSql += " WHERE cmtcd = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf080M Delete
            sSql = ""
            sSql += "DELETE rf080m"
            sSql += " WHERE cmtcd = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTestInfo_Dispseql(ByVal rsCmtCd As String, ByVal rsDispSeq As String) As Boolean
        Dim sFn As String = "Public Function TransTestInfo_Dispseql(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "UPDATE rf080m SET dispseq = :dispseq"
            sSql += " WHERE cmtcd = :cmtcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("dispseq",  OracleDbType.Varchar2).Value = rsDispSeq
            dbCmd.Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_EQ
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : RISAPP.APP_F_EQ" & vbTab

    Public Overloads Function GetEqInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Overloads Function GetEqInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT eqcd, eqnms, CASE WHEN DELFLG='0' THEN 'Y'WHEN DELFLG='1' THEN 'N' END AS USEYN, NULL diffday"
                sSql += "  FROM rf070m"
                sSql += " ORDER BY eqcd"
            ElseIf riMode = 1 Then
                sSql += "SELECT eqcd, eqnms, CASE WHEN DELFLG='0' THEN 'Y'WHEN DELFLG='1' THEN 'N' END AS USEYN,"
                sSql += "       TO_NUMBER(NVL(delflg, '0')) * -1 diffday"
                sSql += "  FROM rf070m"
                sSql += "  ORDER BY eqcd"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetEqInfo(ByVal rsEqCd As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetEqInfo(Integer, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT eqcd, eqnm, eqnms,"
            sSql += "       delflg, fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid,"
            sSql += "       fn_ack_get_usr_name(regid) regnm,"
            sSql += "       CASE WHEN eqgbn = '1' THEN '[1] 미생물' ELSE '[0] 일반' END eqgbn_01"
            sSql += "  FROM rf070m"
            sSql += " WHERE eqcd = :eqcd"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("eqcd",  OracleDbType.Varchar2, rsEqCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsEqCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentEqInfo(ByVal rsEqCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentEqInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT eqnm"
            sSql += "  FROM rf070m"
            sSql += " WHERE eqcd = :eqcd"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("eqcd",  OracleDbType.Varchar2, rsEqCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsEqCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransEqInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsEqCd As String) As Boolean
        Dim sFn As String = "Public Function TransEqInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf070M : 장비 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf070m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정 
                    With ro_Tcol1
                        'rf070H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf070h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf070m f"
                        sSql += " WHERE eqcd = :eqcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("eqcd",  OracleDbType.Varchar2).Value = rsEqCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "EQCD", "USDT"

                                    Case Else
                                        sFields += sField + " = :" + sField + ","

                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sSql = ""
                            sSql += "UPDATE rf070m SET " + sFields
                            sSql += " WHERE eqcd = :eqcd"

                            dbCmd.Parameters.Add("eqcd",  OracleDbType.Varchar2).Value = rsEqCd

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransEqInfo_UE(ByVal rsEqCd As String) As Boolean
        Dim sFn As String = "Public Function TransEqInfo_UE(String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf070M : 장비 마스터 
            '   rf070H Insert 
            sSql = ""
            sSql += "INSERT INTO rf070h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf070m f"
            sSql += " WHERE eqcd = :eqcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("eqcd",  OracleDbType.Varchar2).Value = rsEqCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf070M Update
            sSql = ""
            sSql += "UPDATE rf070m SET delflg = '1', regdt = fn_ack_sysdate, regid = regid"
            sSql += " WHERE eqcd = :eqcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("eqcd",  OracleDbType.Varchar2).Value = rsEqCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransEqInfo_DEL(ByVal rsEqCd As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            '   lf030h Insert
            sSql = ""
            sSql += "INSERT INTO rf070h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf070m f"
            sSql += " WHERE eqcd = :eqcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("eqcd",  OracleDbType.Varchar2).Value = rsEqCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf070M Delete
            sSql = ""
            sSql += "DELETE rf070m"
            sSql += " WHERE eqcd = :eqcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("eqcd",  OracleDbType.Varchar2).Value = rsEqCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_EXLAB
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_EXLAB" + vbTab

    Public Overloads Function GetExLabInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Overloads Function GetExLabInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT exlabcd, exlabnmd, delflg,"
                sSql += "  CASE WHEN DELFLG='0' THEN 'Y'WHEN DELFLG='1' THEN 'N' END AS USEYN"
                sSql += "  FROM rf050m"
                sSql += " ORDER BY exlabcd"
            ElseIf riMode = 1 Then
                sSql += "SELECT exlabcd, exlabnmd, TO_NUMBER(NVL(delflg, '0')) * - 1 diffday, CASE WHEN DELFLG='0' THEN 'Y'WHEN DELFLG='1' THEN 'N' END AS USEYN"
                sSql += "  FROM rf050m"
                sSql += " ORDER BY exlabcd"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetExLabInfo(ByVal rsExLabCd As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetExLabInfo(String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT delflg, exlabcd, exlabnm, exlabnms, exlabnmd, exlabnmp, exlabnmbp,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid,"
            sSql += "       fn_ack_get_usr_name(regid) regnm"
            sSql += "  FROM rf050m"
            sSql += " WHERE exlabcd = :exlabcd"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("exlabcd",  OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransExLabInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsExLabCd As String) As Boolean
        Dim sFn As String = "Public Function TransExLabInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf050M : 위탁기관마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf050m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf050H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf050h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf050m f"
                        sSql += " WHERE exlabcd = :exlabcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("exlabcd",  OracleDbType.Varchar2).Value = rsExLabCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "EXLABCD"

                                    Case Else
                                        sFields += sField + " = :" + sField + ","

                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue

                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sSql = ""
                            sSql += "UPDATE rf050m set " + sFields
                            sSql += " WHERE exlabcd = :exlabcd"

                            dbCmd.Parameters.Add("exlabcd",  OracleDbType.Varchar2).Value = rsExLabCd

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransExLabInfo_DEL(ByVal rsExLabCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = " Public Function TransTestInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf050M : 위탁기관 마스터
            '   rf050H Insert
            sSql = ""
            sSql += "INSERT INTO rf050h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf050m f"
            sSql += " WHERE exlabcd = :exlabcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("exlabcd",  OracleDbType.Varchar2).Value = rsExLabCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf050M Delete
            sSql = ""
            sSql += "DELETE rf050m"
            sSql += " WHERE exlabcd = :exlabcd"


            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("exlabcd",  OracleDbType.Varchar2).Value = rsExLabCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_OSLIP
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : DA01.APP_F_OSLIP" & vbTab

    Public Function GetRecentOSlipInfo(ByVal rsOSlipCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetRecentOSlipInfo() As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT tordslip"
            sSql += "  FROM lf100m"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            alParm.Add(New OracleParameter("tordslip",  OracleDbType.Varchar2, rsOSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOSlipCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetOSlipInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetOSlipInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT tordslip, tordslipnm, usdt, uedt, null diffday"
                sSql += "  FROM lf100m"
                sSql += " WHERE uedt > fn_ack_sysdate"
                sSql += " ORDER BY tordslip"
            ElseIf riMode = 1 Then
                sSql += "SELECT f68.tordslip, f68.tordslipnm, usdt, uedt,"
                sSql += "       CASE WHEN TO_DATE(uedt, 'yyyymmddhh24miss') - SYSDATE < 0 THEN -1 ELSE 0 END diffday,"
                sSql += "       NULL moddt, NULL modid"
                sSql += "  FROM lf100m f68"
                sSql += " ORDER BY tordslip, moddt, modid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function GetOSlipInfo(ByVal rsOrdSlip As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetOSlipInfo(String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT f68.tordslip, f68.tordslipnm, f68.dispseq,"
            sSql += "       fn_ack_date_str(f68.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
            sSql += "       fn_ack_get_usr_name(f68.regid) regnm,"
            sSql += "       null moddt, null modid, doctorid1, doctorid2,"
            sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
            sSql += "       fn_ack_date_str(uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
            sSql += "       fn_ack_get_usr_name(doctorid1) docnm1,"
            sSql += "       fn_ack_get_usr_name(doctorid2) docnm2"
            sSql += "  FROM lf100m f68"
            sSql += " WHERE f68.tordslip = :tordslip"
            sSql += "   AND usdt         = :usdt"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("tordslip",  OracleDbType.Varchar2, rsOrdSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdSlip))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetOSlipInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsOSlipCd As String) As DataTable
        Dim sFn As String = "Public Function GetOSlipInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT tordslip, tordslipnm, dispseq,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
            sSql += "       fn_ack_get_usr_name(regid) regnm,"
            sSql += "       doctorid1, doctorid2,"
            sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
            sSql += "       fn_ack_date_str(uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
            sSql += "       fn_ack_date_str(moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f68.modid,"
            sSql += "       fn_usr_name(doctorid1) docnm1,"
            sSql += "       fn_usr_name(doctorid2) docnm2"
            sSql += "  FROM lf100h"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND moddt    = :moddt"
            sSql += "   AND modid    = :modid"

            alParm.Add(New OracleParameter("tordslip",  OracleDbType.Varchar2, rsOSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOSlipCd))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModId))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransOSlipInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                     ByVal ro_Tcol2 As ItemTableCollection, ByVal riType2 As Integer, _
                                        ByVal rsOrdSlip As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = "Public Function TransOSlipInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'lf100m : 처방슬립 마스터
            Select Case riType1
                Case 0      '----- 신규
                    'UPDATE uedt of previous record
                    sSql = ""
                    sSql += "UPDATE lf100m SET uedt = :usdt"
                    sSql += " WHERE (tordslip, usdt) IN"
                    sSql += "       (SELECT a.tordslip, a.usdt"
                    sSql += "          FROM (SELECT tordslip, usdt"
                    sSql += " 		 		   FROM lf100m"
                    sSql += " 				  WHERE tordslip = :tordslip"
                    sSql += " 				    AND usdt     < :usdt"
                    sSql += " 				    AND uedt     > :usdt"
                    sSql += "                 ORDER BY usdt DESC"
                    sSql += "               ) a"
                    sSql += "         WHERE ROWNUM = 1"
                    sSql += " 	    )"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                    dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
                    dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                    dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUsDt

                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO lf100m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'lf100h Backup
                        sSql = ""
                        sSql += "INSERT INTO lf100h "
                        sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf100m f"
                        sSql += " WHERE tordslip = :tordslip"
                        sSql += "   AND usdt     = :usdt"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "TORDSLIP"

                                    Case Else
                                        sFields += sField + " = :" + sField + ","

                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sSql = ""
                            sSql += "UPDATE lf100m SET " + sFields
                            sSql += " WHERE tordslip = :tordslip"
                            sSql += "   AND usdt     = :usdt"

                            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
                            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransOSlipInfo_DEL(ByVal rsOrdSlip As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = " Public Function TransOSlipInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'lf100m : 검사슬립 마스터
            '   lf100h Insert
            sSql = ""
            sSql += "INSERT INTO lf100h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf100m f"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf100m Update
            sSql = ""
            sSql += "DELETE lf100m"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransOSlipInfo_UE(ByVal rsOrdSlip As String, ByVal rsUsDt As String, ByVal rsUeDt As String) As Boolean
        Dim sFn As String = " Public Function TransOSlipInfo_UE() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim sMsg As String = ""

            'lf100m : 검사처방슬립코드 마스터
            '   lf100h Insert
            sSql = ""
            sSql += "INSERT INTO lf100h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf100m f"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf100m UPDATE
            sSql = ""
            sSql += "UPDATE lf100m SET uedt = :uedt, regdt = fn_ack_sysdate, regid = :regid"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDt
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransOSlipInfo_UPD_UE(ByVal rsOrdSlip As String, ByVal rsUsDt As String, ByVal rsUeDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransBcclsInfo_UPD_UE() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf010M : 검체분류 마스터
            '   rf010H Insert
            sSql = ""
            sSql += "INSERT INTO lf100h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf100m f"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf010M Update
            sSql = ""
            sSql += "UPDATE lf100m SET"
            sSql += "       uedt  = :uedt,"
            sSql += "       regdt = fn_ack_sysdate,"
            sSql += "       regid = :regid"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransOSlipInfo_UPD_US(ByVal rsOrdSlip As String, ByVal rsUsDt As String, ByVal rsUsDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransBcclsInfo_UPD_US() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf010M : 검체 마스터 
            '   rf010H Insert 
            sSql = ""
            sSql += "INSERT INTO lf100h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf100m f"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf010M Update 
            sSql = ""
            sSql += "UPDATE lf100m SET"
            sSql += "       usdt  = :usdtchg,"
            sSql += "       regdt = fn_ack_sysdate,"
            sSql += "       regid = :regid"
            sSql += " WHERE tordslip = :tordslip"
            sSql += "   AND usdt     = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usdtchg",  OracleDbType.Varchar2).Value = rsUsDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("tordslip",  OracleDbType.Varchar2).Value = rsOrdSlip
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            If iRet > 0 Then
                dbTran.Commit()
                Return True
            Else
                dbTran.Rollback()
                Return False
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Public Class APP_F_RSTCD
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_RSTCD" + vbTab

    Public Function GetRstCdInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetRstCdInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT testcd, tnmd"
                sSql += "  FROM ("
                sSql += "        SELECT f64.testcd, MIN(CASE WHEN f60.tcdgbn = 'C' THEN '-- ' || f60.tnmd ELSE f60.tnmd END) tnmd"
                sSql += "          FROM rf083m f64, rf060m f60"
                sSql += "         WHERE f64.testcd = f60.testcd"
                sSql += "           AND f60.usdt <= fn_ack_sysdate"
                sSql += "           AND f60.uedt >  fn_ack_sysdate"
                sSql += "         GROUP BY f64.testcd"
                sSql += "       ) a"
            ElseIf riMode = 1 Then
                sSql += "SELECT testcd, tnmd, diffday"
                sSql += "  FROM ("
                sSql += "        SELECT f64.testcd, MIN(CASE WHEN f60.tcdgbn = 'C' THEN '-- ' || f60.tnmd ELSE f60.tnmd END) tnmd,"
                sSql += "               NULL diffday, NULL moddt, NULL modid"
                sSql += "          FROM rf083m f64, rf060m f60"
                sSql += "         WHERE f64.testcd = f60.testcd"
                sSql += "           AND f60.usdt <= fn_ack_sysdate"
                sSql += "           AND f60.uedt >  fn_ack_sysdate"
                sSql += "         GROUP BY f64.testcd"
                sSql += "         UNION ALL"
                sSql += "        SELECT f64.testcd, MIN(CASE WHEN f60.tcdgbn = 'C' THEN '-- ' || f60.tnmd ELSE f60.tnmd END) tnmd,"
                sSql += "               -1 diffday, fn_ack_date_str(f64.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f64.modid modid"
                sSql += "          FROM rf083h f64, rf060m f60"
                sSql += "         WHERE f64.testcd = f60.testcd"
                sSql += "           AND f60.usdt <= f64.moddt"
                sSql += "           AND f60.uedt >  f64.moddt"
                sSql += "         GROUP BY f64.testcd, f64.moddt, f64.modid"
                sSql += "       ) a"
                sSql += " ORDER BY testcd, moddt, modid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRstCdInfo(ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetRstCdInfo(String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT f8.testcd, f8.rstcdseq, f6.tnmd, f8.keypad, f8.rstcont,"
            sSql += "       fn_ack_date_str(f8.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f8.regid,"
            sSql += "       fn_ack_get_usr_name(f8.regid) regnm,"
            sSql += "       NULL moddt, NULL modid, NULL modnm, f8.grade, f8.rstlvl"
            sSql += "  FROM (SELECT f8.testcd, MIN(CASE WHEN f6.tcdgbn = 'C' THEN '-- ' || f6.tnmd ELSE f6.tnmd END) tnmd"
            sSql += "          FROM rf083m f8, rf060m f6"
            sSql += " 		  WHERE f8.testcd = f6.testcd"
            sSql += " 		    AND f6.usdt  <= fn_ack_sysdate"
            sSql += " 		    AND f6.uedt  >  fn_ack_sysdate"
            sSql += " 		    AND f8.testcd = :testcd"
            sSql += " 		  GROUP BY f8.testcd"
            sSql += "        ) f6, rf083m f8"
            sSql += " WHERE f8.testcd   = f6.testcd"
            sSql += " ORDER BY f8.testcd, f8.rstcdseq"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRstCdInfo(ByVal rsModDT As String, ByVal rsModID As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetRstCdInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT f8.testcd, f8.rstcdseq, f6.tnmd, f8.keypad, f8.rstcont,"
            sSql += "       fn_get_sting(f8.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f8.regid,"
            sSql += "       fn_ack_get_usr_name(f8.regid) regnm,"
            sSql += " 	    fn_get_sting(f8.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f8.modid,"
            sSql += "       fn_ack_get_usr_name(f8.modid) modnm, f8.grade, f8.rstlvl"
            sSql += "  FROM (SELECT f6.testcd, MIN(CASE WHEN f6.tcdgbn = 'C' THEN '-- ' || f6.tnmd ELSE f6.tnmd END) tnmd"
            sSql += "          FROM rf060m f6"
            sSql += "         WHERE f6.testcd = :testcd"
            sSql += " 	        AND f6.usdt  <= :moddt"
            sSql += " 	        AND f6.uedt   > :moddt"
            sSql += " 	      GROUP BY f6.testcd"
            sSql += "       ) f6, rf083h f8"
            sSql += " WHERE f8.testcd = f6.testcd"
            sSql += "   AND f8.moddt  = :moddt"
            sSql += "   AND f8.modid  = :modid"
            sSql += " ORDER BY f8.testcd, f8.rstcdseq"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDT))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDT))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDT))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentRstCdInfo(ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentRstCdInfo(ByVal asTClsCd As String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT testcd FROM rf083m"
            sSql += " WHERE testcd = :testcd"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransRstCdInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsTestCd As String) As Boolean
        Dim sFn As String = "Public Function TransRstCdInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf083M : 결과코드 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf083m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf083H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf083h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf083m f"
                        sSql += " WHERE testcd = :testcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()


                        sSql = ""
                        sSql += "DELETE rf083m"
                        sSql += " WHERE testcd = :testcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf083m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransRstCdInfo_UE(ByVal rsTestCd As String) As Boolean
        Dim sFn As String = "Public Function TransRstCdInfo_UE(String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf083M : 결과코드 마스터 
            '   rf083H Insert 
            sSql = ""
            sSql += "INSERT INTO rf083h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf083m f"
            sSql += " WHERE testcd = :testcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf083M Delete 
            sSql = ""
            sSql += "DELETE rf083m"
            sSql += " WHERE testcd = :testcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
End Class

Public Class APP_F_BCCLS
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_BCCLS" + vbTab

    Private Function ifExistMoreSameBccls(ByVal rsBcclsCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = "Private Function ifExistMoreSameBccls(String, String) As Boolean"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT bcclscd"
            sSql += "  FROM vw_ack_tot_bccls_info"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND uedt   >= fn_ack_sysdate"
            sSql += "   AND bcclscd NOT IN (SELECT bcclscd FROM vw_ack_tot_bccls_info"
            sSql += "                        WHERE bcclscd = :bcclscd"
            sSql += "                          AND uedt   >= :usdt"
            sSql += "                          AND usdt    = :usdt"
            sSql += "                      )"

            alParm.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
            alParm.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim objDT As DataTable = DbExecuteQuery(sSql, alParm)

            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetSameBC(ByVal rsBcclsCd As String, ByVal rsUsDt As String, ByVal rsBcclsnmbp As String, Optional ByVal riRegType As Integer = 0) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT bcclscd, partgbn"
            sSql += "  FROM vw_ack_tot_bccls_info "
            sSql += " WHERE uedt >= fn_ack_sysdate"
            sSql += "   AND bcclsnmbp = :bcclsnmbp"
            sSql += "   AND bcclscd  <> :bcclscd"

            alParm.Add(New OracleParameter("bcclsnmbp",  OracleDbType.Varchar2, rsBcclsnmbp.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsnmbp))
            alParm.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))

            If riRegType = 0 Then
                '신규
                sSql += "   OR (bcclscd = :bcclscd AND usdt >= :usdt AND PARTGBN = '[핵의학검사실]')"
                sSql += "   OR (bcclscd = :bcclscd AND PARTGBN = '[진단검사실]')"

                alParm.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                alParm.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))

            End If

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


        End Try
    End Function


    Public Function GetRecentBcclsInfo(ByVal rsBcclsCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetRecentBcclsInfo() As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT usdt, partgbn"
            sSql += "  FROM vw_ack_tot_bccls_info"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND ((usdt   >= :usdt AND PARTGBN = '[진단검사실]') OR PARTGBN = '[핵의학검사실]') "
            sSql += "   AND ROWNUM  = 1"
            sSql += " ORDER BY usdt DESC"

            alParm.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetBcclsInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetBcclsInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT bcclscd, bcclsnmd, bcclsnmbp, usdt,"
                sSql += "       CASE WHEN TO_DATE(uedt, 'yyyymmddhh24miss') - SYSDATE < 0 THEN -1 ELSE 0 END diffday"
                sSql += "  FROM rf010m"
                sSql += " WHERE uedt >= fn_ack_sysdate"
                sSql += " ORDER BY bcclscd, bcclsnmbp"
            ElseIf riMode = 1 Then
                sSql += "SELECT bcclscd, bcclsnmd, bcclsnmbp, usdt,"
                sSql += "       CASE WHEN TO_DATE(uedt, 'yyyymmddhh24miss') - SYSDATE < 0 THEN -1 ELSE 0 END diffday,"
                sSql += "       '' modid, '' moddt"
                sSql += "  FROM rf010m"
                sSql += " ORDER BY bcclscd, bcclsnmbp"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function


    Public Overloads Function GetBcclsInfo(ByVal rsBcclsCd As String, ByVal rsUsDt As String, ByVal rsUeDt As String) As DataTable
        Dim sFn As String = "Public Function GetBcclsInfo(IString, String, String) As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT bcclscd, bcclsnm, bcclsnms, bcclsnmd, bcclsnmp, bcclsnmbp,"
            sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
            sSql += "       fn_ack_date_str(uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid,"
            sSql += "       fn_ack_get_usr_name(regid) regnm,"
            sSql += "       CASE WHEN colorgbn = '0' THEN '[0] 흰색'   WHEN colorgbn = '1' THEN '[1] 노랑색'"
            sSql += "            WHEN colorgbn = '2' THEN '[2] 보라색' WHEN colorgbn = '3' THEN '[3] 주황색'"
            sSql += "       END colorgbn_01,"
            sSql += "       CASE WHEN bcclsgbn = '0' THEN '[0]'          WHEN bcclsgbn = '1' THEN '[1] 종합검증'"
            sSql += "            WHEN bcclsgbn = '2' THEN '[2] 미생물'   WHEN bcclsgbn = '3' THEN '[3] 혈액은행'"
            sSql += "            WHEN bcclsgbn = '6' THEN '[6] 위탁검체'"
            sSql += "            WHEN bcclsgbn = '7' THEN '[7] 성분제제'"
            sSql += "            WHEN bcclsgbn = '8' THEN '[8] 핵의학'"
            sSql += "            WHEN bcclsgbn = '9' THEN '[9] 병리과'"
            sSql += "       END bcclsgbn_01"
            sSql += "  FROM rf010m"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"

            alParm.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            If rsUeDt <> "" Then
                sSql += "   AND uedt   = :uedt"

                alParm.Add(New OracleParameter("uedt",  OracleDbType.Varchar2, rsUeDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUeDt))
            End If
            sSql += " ORDER BY bcclscd"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function TransBcclsInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsBcclsCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = "Public Function TransBcclsInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf010M : 계 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        'UPDATE uedt of previous record
                        sSql = ""
                        sSql += "UPDATE rf010m SET uedt = :usdt"
                        sSql += " WHERE (bcclscd, usdt) IN"
                        sSql += "       (SELECT a.bcclscd, a.usdt"
                        sSql += "          FROM (SELECT bcclscd, usdt"
                        sSql += " 			 	   FROM rf010m"
                        sSql += " 				  WHERE bcclscd = :bcclscd"
                        sSql += " 				    AND usdt    < :usdt"
                        sSql += " 				    AND uedt    > :usdt"
                        sSql += "                 ORDER BY usdt DESC"
                        sSql += "              ) a"
                        sSql += "        WHERE ROWNUM = 1"
                        sSql += " 	    )"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = " INSERT INTO rf010m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf010H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf010h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf010m f"
                        sSql += " WHERE (bcclscd, usdt) IN (SELECT a.bcclscd, a.usdt"
                        sSql += "                              FROM (SELECT bcclscd, usdt"
                        sSql += " 							           FROM rf010m"
                        sSql += " 							          WHERE bcclscd = :bcclscd"
                        sSql += " 							          ORDER BY usdt DESC"
                        sSql += "                                   ) a"
                        sSql += " 							  WHERE ROWNUM = 1"
                        sSql += " 							)"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()

                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "BCCLSCD", "USDT"

                                    Case Else
                                        sFields += sField + " = :" + sField + ","
                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sSql = ""
                            sSql += "UPDATE rf010m SET " + sFields
                            sSql += " WHERE bcclscd = :bcclscd"
                            sSql += "   AND usdt   <= :usdt"
                            sSql += "   AND uedt    > :usdt"

                            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
                            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function


    Public Function TransBcclsInfo_DEL(ByVal rsBcclscd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = " Public Function TransBcclsInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            '   rf010H Insert
            sSql = ""
            sSql += "INSERT INTO rf010h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf010m f"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclscd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf010M Update
            sSql = ""
            sSql += "DELETE rf010m"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"


            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclscd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransBcclsInfo_UE(ByVal rsBcclsCd As String, ByVal rsUsDt As String, ByVal rsUeDt As String) As Boolean
        Dim sFn As String = " Public Function TransBcclsInfo_UE() As Boolean"

        Dim dbCn As New OracleConnection
        Dim dbTran As OracleTransaction
        Dim dbCmd As New OracleCommand

        Try
            Dim sMsg As String = ""

            sMsg = ifExistOtherUsableData("rf011", "BCCLSCD", rsBcclsCd, rsUsDt)

            If IsNothing(sMsg) Then
                MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
                Return False
            End If

            If Not sMsg = "" Then
                MsgBox(sMsg, MsgBoxStyle.Critical)
                Return False
            End If

            'rf010M : 검사분류 마스터
            If ifExistMoreSameBccls(rsBcclsCd, rsUsDt) Then Return False

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            dbCn = GetDbConnection()
            dbTran = dbCn.BeginTransaction()


            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            '   rf010H Insert
            sSql = ""
            sSql += "INSERT INTO rf010h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf010m f"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf010M Update
            sSql = ""
            sSql += "UPDATE rf010m SET uedt = :uedt, regdt = fn_ack_sysdate, regid = :regid"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDt
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()
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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransBcclsInfo_UPD_UE(ByVal rsBcclsCd As String, ByVal rsUsDt As String, ByVal rsUeDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransBcclsInfo_UPD_UE() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf010M : 검체분류 마스터
            '   rf010H Insert
            sSql = ""
            sSql += "INSERT INTO rf010h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf010m f"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"
            sSql += "   AND uedt    = ("
            sSql += "                  SELECT uedt FROM rf010m"
            sSql += "                   WHERE bcclscd = :bcclscd"
            sSql += "                     AND usdt    = :usdt"
            sSql += "                 )"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf010M Update
            sSql = ""
            sSql += "UPDATE rf010m SET"
            sSql += "       uedt  = :uedt,"
            sSql += "       regdt = fn_ack_sysdate,"
            sSql += "       regid = :regid"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"
            sSql += "   AND uedt    = ("
            sSql += "                  SELECT uedt FROM rf010m"
            sSql += "                   WHERE bcclscd = :bcclscd"
            sSql += "                     AND usdt    = :usdt"
            sSql += "                 )"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransBcclsInfo_UPD_US(ByVal rsBcclsCd As String, ByVal rsUsDt As String, ByVal rsUsDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransBcclsInfo_UPD_US() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf010M : 검체 마스터 
            '   rf010H Insert 
            sSql = ""
            sSql += "INSERT INTO rf010h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf010m f"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf010M Update 
            sSql = ""
            sSql += "UPDATE rf010m SET"
            sSql += "       usdt  = :usdtchg,"
            sSql += "       regdt = fn_ack_sysdate,"
            sSql += "       regid = :regid"
            sSql += " WHERE bcclscd = :bcclscd"
            sSql += "   AND usdt    = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usdtchg",  OracleDbType.Varchar2).Value = rsUsDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclsCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_SLIP
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : RISAPP.APP_F_SLIP" + vbTab

    Private Function ifExistPart(ByVal rsPartCd As String, ByVal rsUsDt As String, _
                                 ByVal r_DbTrans As oracleTransaction, ByVal r_DbCn As oracleConnection) As Boolean
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT partcd"
            sSql += "  FROM vw_ack_tot_partslip_info"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = '-'"
            sSql += "   AND usdt   = :usdt"
            sSql += "   AND uedt  >  :usdt"
            sSql += "   AND uedt  >= fn_ack_sysdate"

            Dim dbCmd As New oracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            With dbCmd
                .Connection = r_DbCn
                .Transaction = r_DbTrans
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
                .Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                .Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            End With

            dbDa = New OracleDataAdapter(dbCmd)
            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Private Function ifExistMoreSameSlip(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String, _
                                         ByVal r_DbTrans As OracleTransaction, ByVal r_DbCn As OracleConnection) As Boolean
        Dim sFn As String = "Private Function ifExistMoreSameSlip(ByVal asPartCd As String, ByVal asSlipCd As String, ByVal asUSDT As String) As Boolean"

        Try
            Dim dbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter

            With dbCmd
                .Connection = r_DbCn
                .Transaction = r_DbTrans
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""

            sSql += "SELECT f21.partcd, f21.slipcd"
            sSql += "  FROM vw_ack_tot_partslip_info f20, vw_ack_tot_partslip_info f21"
            sSql += " WHERE f20.partcd  = f21.partcd"
            sSql += "   AND f20.slipcd  = '-'"
            sSql += "   AND f21.slipcd <> '-'"
            sSql += "   AND f21.partcd  = :partcd"
            sSql += "   AND f20.uedt >= fn_ack_sysdate"
            sSql += "   AND f21.uedt >= fn_ack_sysdate"
            sSql += "   AND (f21.partcd, f21.slipcd) NOT IN ("
            sSql += "       (SELECT f21.partcd, f21.slipcd"
            sSql += "          FROM vw_ack_tot_partslip_info f20, vw_ack_tot_partslip_info f21"
            sSql += "         WHERE f20.partcd  = f21.partcd"
            sSql += "           AND f20.slipcd  = '-'"
            sSql += "           AND f21.slipcd <> '-'"
            sSql += "           AND f21.partcd  = :partcd"
            sSql += "           AND f21.slipcd  = :slipcd"
            sSql += "           AND f20.uedt   >= :usdt"
            sSql += "           AND f21.usdt    = :usdt"
            sSql += "       )"

            dbCmd.CommandText = sSql

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbDa = New OracleDataAdapter(dbCmd)
            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetOnlyPartInfo(ByVal rsPartCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList


            sSql += "SELECT partnm, partnm, partnms, partnmd, partnmp,"
            sSql += "       take2yn, telno,"
            sSql += "       CASE WHEN partgbn = '0' THEN '[0]'            WHEN partgbn = '1' THEN '[1] 종합검증'"
            sSql += "            WHEN partgbn = '2' THEN '[2] 미생물'     WHEN partgbn = '3' THEN '[3] 혈액은행'"
            sSql += "            WHEN partgbn = '4' THEN '[4] 핵의학체외'"
            sSql += "       END partgbn_01"
            sSql += "  FROM rf020m"
            sSql += " WHERE partcd  = :partcd"
            sSql += "   AND uedt   >  :usdt"
            sSql += "   AND uedt   >= fn_ack_sysdate"

            alParm.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
             Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetRecentSlipInfo(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetRecentSlipInfo() As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT usdt, partgbn"
            sSql += "  FROM (SELECT usdt, partgbn"
            sSql += "          FROM vw_ack_tot_partslip_info"
            sSql += "         WHERE partcd = :partcd"
            sSql += "           AND slipcd = :slipcd"
            sSql += "           AND ((usdt   >= :usdt AND PARTGBN = '[핵의학검사실]') OR PARTGBN = '[진단검사실]')"
            sSql += "         ORDER BY usdt DESC"
            sSql += "       ) a"
            sSql += " WHERE ROWNUM = 1"

            alParm.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
            alParm.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
             Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Overloads Function GetSlipInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Overloads Function GetSlipInfo(ByVal iMode As Integer, ByVal Serch As String) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT DISTINCT slipcd, slipnmd, partnmd, usdt, uedt, dispseq"
                sSql += "  FROM ("
                sSql += "        SELECT f21.partcd || f21.slipcd slipcd, f21.slipnmd, f20.partnmd, f21.usdt, f21.uedt, f21.dispseq"
                sSql += "          FROM rf020m f20, rf021m f21"
                sSql += "         WHERE f20.partcd = f21.partcd"
                sSql += "           AND f20.uedt  >= fn_ack_sysdate"
                sSql += "           AND f21.uedt  >= fn_ack_sysdate"
                sSql += "       ) a"
                sSql += " ORDER BY dispseq, slipcd"
            ElseIf riMode = 1 Then
                sSql += "SELECT DISTINCT slipcd, slipnmd, partnmd, usdt, uedt, dispseq, diffday"
                sSql += "  FROM ("
                sSql += "        SELECT f21.partcd || f21.slipcd slipcd, f21.slipnmd, f20.partnmd, f21.usdt, f21.uedt,"
                sSql += "               CASE WHEN TO_DATE(f21.uedt, 'yyyymmddhh24miss') - SYSDATE < 0 THEN -1 ELSE 0 END  diffday,"
                sSql += "               f21.dispseq"
                sSql += "          FROM rf020m f20, rf021m f21"
                sSql += "         WHERE f20.partcd = f21.partcd"
                sSql += "       ) a"
                sSql += " ORDER BY dispseq, slipcd"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
             Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Overloads Function GetSlipInfo(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetSlipInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT f21.partcd, f21.slipcd, f20.partnm, f20.partnms, f20.partnmd, f20.partnmp,"
            sSql += "       f21.slipnm, f21.slipnms, f21.slipnmd, f21.slipnmp,"
            sSql += "       fn_ack_date_str(f21.usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
            sSql += "       fn_ack_date_str(f21.uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
            sSql += "       fn_ack_date_str(f21.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
            sSql += "       f21.regid, f21.dispseq, f20.take2yn, fn_ack_get_usr_name(f21.regid) regnm,"
            sSql += "       CASE WHEN f20.partgbn = '0' THEN '[0]'            WHEN f20.partgbn = '1' THEN '[1] 종합검증'"
            sSql += "            WHEN f20.partgbn = '2' THEN '[2] 미생물'     WHEN f20.partgbn = '3' THEN '[3] 혈액은행'"
            sSql += "            WHEN f20.partgbn = '4' THEN '[4] 핵의학체외' "
            sSql += "       END partgbn_01, f20.telno"
            sSql += "  FROM rf020m f20, rf021m f21"
            sSql += " WHERE f20.partcd = f21.partcd"
            sSql += "   AND f21.partcd = :partcd"
            sSql += "   AND f21.slipcd = :slipcd"
            sSql += "   AND f21.usdt   = :usdt"

            alParm.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
            alParm.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
             Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransSlipInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                  ByVal ro_Tcol2 As ItemTableCollection, ByVal riType2 As Integer, _
                                  ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = "Public Function TransSlipInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf020M : 분야 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        If Not ifExistPart(rsPartCd, rsUsDt, dbTran, dbCn) Then
                            For i As Integer = 1 To .ItemTableRowCount
                                sField = "" : sFields = "" : sValue = "" : sValues = ""

                                dbCmd.Parameters.Clear()
                                For j As Integer = 1 To .ItemTableColCount
                                    sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                    sFields += sField + ","

                                    sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                    sValues += ":" + sField + ","

                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                Next

                                'insert new record
                                sFields = sFields.Substring(0, sFields.Length - 1)
                                sValues = sValues.Substring(0, sValues.Length - 1)

                                sSql = "INSERT INTO rf020m (" + sFields + ") VALUES (" + sValues + ")"

                                dbCmd.CommandText = sSql
                                iRet += dbCmd.ExecuteNonQuery()

                            Next
                        End If
                    End With
                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf020H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf020h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf020m f"
                        sSql += " WHERE partcd = :partcd"
                        sSql += "   AND usdt   = :usdt"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "PARTCD", "USDT"

                                    Case Else
                                        sFields += sField + "= :" + sField + ","

                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)

                            sSql = ""
                            sSql += "UPDATE rf020m SET " + sFields
                            sSql += " WHERE partcd = :partcd"
                            sSql += "   AND usdt   = :usdt"

                            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
                            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With
            End Select

            'rf021M : 슬립 마스터
            Select Case riType2
                Case 0      '----- 신규
                    With ro_Tcol2
                        'UPDATE uedt of previous record
                        sSql = ""
                        sSql += "UPDATE rf021m SET uedt = :usdt"
                        sSql += " WHERE (partcd, slipcd, usdt) IN"
                        sSql += "       (SELECT a.partcd, a.slipcd, a.usdt"
                        sSql += "          FROM (SELECT partcd, slipcd, usdt"
                        sSql += " 				   FROM rf021m"
                        sSql += " 				  WHERE partcd = :partcd"
                        sSql += "                   AND slipcd = :slipcd"
                        sSql += " 				    AND usdt  <= :usdt"
                        sSql += " 				    AND uedt  >  :usdt"
                        sSql += "                 ORDER BY usdt DESC"
                        sSql += "              ) a"
                        sSql += "        WHERE ROWNUM = 1"
                        sSql += " 	    )"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
                        dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf021m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol2
                        'rf021H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf021h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf021m f"
                        sSql += " WHERE partcd = :partcd"
                        sSql += "   AND slipcd = :slipcd"
                        sSql += "   AND usdt   = :usdt"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
                        dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "PARTCD", "SLIPCD", "USDT"

                                    Case Else
                                        sFields += sField + " = :" + sField + ","

                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sSql = ""
                            sSql += "UPDATE rf021m SET " + sFields
                            sSql += " WHERE partcd = :partcd"
                            sSql += "   AND slipcd = :slipcd"
                            sSql += "   AND usdt   = :usdt"

                            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
                            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
                            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransSlipInfo_DEL(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = " Public Function TransSlipInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf021M : 슬립 마스터
            '   rf021H Insert 
            sSql = ""
            sSql += "INSERT INTO rf021h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf021m f"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf021M Delete
            sSql = ""
            sSql += "DELETE rf021m"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf020M : 분야 마스터 
            '   rf020H Insert 
            sSql = ""
            sSql += "INSERT INTO rf020h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf020m f"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND usdt   = :usdt"
            sSql += "   AND partcd NOT IN (SELECT partcd FROM rf021m WHERE partcd = :partcd AND slipcd <> :slipcd AND usdt = :usdt)"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()
            '   rf020M Update
            sSql = ""
            sSql += "DELETE rf020m"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND usdt   = :usdt"
            sSql += "   AND partcd NOT IN (SELECT partcd FROM rf021m WHERE partcd = :partcd AND slipcd <> :slipcd AND usdt = :usdt)"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransSlipInfo_UPD_US(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String, ByVal rsUsDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransSlipInfo_UPD_US() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf020M : 분야 마스터 
            '   rf020H Insert 
            sSql = ""
            sSql += "INSERT INTO rf020h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf020m f"
            sSql += " WHERE partcd   = :partcd"
            sSql += "   AND usdt     = :usdt"
            sSql += "   AND partcd NOT IN (SELECT partcd FROM rf021m WHERE partcd = :partcd AND slipcd <> :slipcd AND usdt = :usdt)"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf020M Update
            sSql = ""
            sSql += "UPDATE rf020m SET"
            sSql += "       usdt   = :usdtchg,"
            sSql += "       regdt  = fn_ack_sysdate,"
            sSql += "       regid  = :regid"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND usdt   = :usdt"
            sSql += "   AND partcd NOT IN (SELECT partcd FROM rf021m WHERE partcd = :partcd AND slipcd <> :slipcd AND usdt = :usdt)"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usdtchg",  OracleDbType.Varchar2).Value = rsUsDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf021M : 슬립 마스터 
            '   rf021H Insert 
            sSql = ""
            sSql += "INSERT INTO rf021h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf021m f"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf021M Update 
            sSql = ""
            sSql += "UPDATE rf021m SET"
            sSql += "       usdt   = :usdtchg,"
            sSql += "       regdt  = fn_ack_sysdate,"
            sSql += "       regid  = :regid"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usdtchg",  OracleDbType.Varchar2).Value = rsUsDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransSlipInfo_UPD_UE(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String, ByVal rsUeDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransSlipInfo_UPD_UE() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf020M : 분야 마스터 
            '   rf020H Insert 
            sSql = ""
            sSql += "INSERT INTO rf020h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf020m f"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND usdt   = :usdt"
            sSql += "   AND partcd NOT IN (SELECT partcd FROM rf021m WHERE partcd = :partcd AND slipcd <> :slipcd AND usdt = :usdt)"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf020M Update
            sSql = ""
            sSql += "UPDATE rf020m SET"
            sSql += "       uedt   = :uedt,"
            sSql += "       regdt  = fn_ack_sysdate,"
            sSql += "       regid  = :regid"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND usdt   = :usdt"
            sSql += "   AND partcd NOT IN (SELECT partcd FROM rf021m WHERE partcd = :partcd AND slipcd <> :slipcd AND usdt = :usdt)"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf021M : 슬립 마스터
            '   rf021H Insert
            sSql = ""
            sSql += "INSERT INTO rf021h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf021m f"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf021M Update
            sSql = ""
            sSql += "UPDATE rf021m SET"
            sSql += "       uedt  = :uedt,"
            sSql += "       regdt = fn_ack_sysdate,"
            sSql += "       regid = :regid"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransSlipInfo_UE(ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsUsDt As String, ByVal rsUeDt As String) As Boolean
        Dim sFn As String = " Public Function TransSlipInfo_UE() As Boolean"

        Dim dbCn As New OracleConnection
        Dim dbTran As OracleTransaction
        Dim dbCmd As New OracleCommand

        Try

            Dim sMsg As String = ""
            Dim alTest As New ArrayList

            sMsg = ifExistOtherUsableData("rf021M", "PARTCD", "SLIPCD", rsPartCd, rsSlipCd, rsUsDt)

            If IsNothing(sMsg) Then
                MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
                Exit Function
            End If

            If Not sMsg = "" Then
                MsgBox(sMsg, MsgBoxStyle.Critical)
                Exit Function
            End If

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            dbCn = GetDbConnection()
            dbTran = dbCn.BeginTransaction()

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf021M : 슬립 마스터
            '   rf021H Insert
            sSql = ""
            sSql += "INSERT INTO rf021h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf021m f"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf021M Update
            sSql = ""
            sSql += "UPDATE rf021m SET uedt = :uedt, regdt = fn_ack_sysdate, regid = :regid"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt   = :usdt"
            alTest.Add(sSql)

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDt
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf020H Insert
            sSql = ""
            sSql += "INSERT INTO rf020h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf020m f"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND usdt   = :usdt"
            sSql += "   AND partcd NOT IN (SELECT partcd FROM rf021m WHERE partcd = :partcd AND slipcd <> :slipcd AND usdt = :usdt)"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf020M Update
            sSql = ""
            sSql += "UPDATE rf020m SET uedt = :uedt, regdt = fn_ack_sysdate, regid = :regid"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND usdt   = :usdt"
            sSql += "   AND partcd NOT IN (SELECT partcd FROM rf021m WHERE partcd = :partcd AND slipcd <> :slipcd AND usdt = :usdt)"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDt
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsPartCd
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
End Class

Public Class APP_F_SPC
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : DA01.APP_F_SPC" + vbTab

    Public Function GetUsUeDupl_Spc(ByVal rsCd As String, ByVal rsUsDt As String, ByVal rsUseTag As String, ByVal rsCompDt As String) As DataTable
        Dim sFn As String = "Public Function GetUsUeDupl_Spc(String, String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += "SELECT a.*"
            sSql += "  FROM ("
            sSql += "        SELECT spccd, spcnmd, usdt, uedt"
            sSql += "          FROM lf030m"
            sSql += "         WHERE spccd = :spccd"
            sSql += "           AND usdt <" + IIf(rsUseTag = "USDT", "=", "").ToString + " :compdt"
            sSql += "           AND uedt >" + IIf(rsUseTag = "USDT", "", "=").ToString + " :compdt"
            sSql += "       ) a LEFT OUTER JOIN"
            sSql += "       ("
            sSql += "        SELECT spccd, spcnmd, usdt, uedt"
            sSql += "          FROM lf030m"
            sSql += "         WHERE spccd = :spccd"
            sSql += "           AND usdt  = :usdt"
            sSql += "       ) b ON (a.spccd = b.spccd AND a.usdt = b.usdt)"
            sSql += " WHERE NVL(b.spccd, ' ') = ' '"

            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("compdt",  OracleDbType.Varchar2, rsCompDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCompDt))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, al)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


        End Try
    End Function

    Public Overloads Function GetSpcInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Overloads Function GetSpcInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT spccd, spcnmd, spcifcd, spcwncd, CASE WHEN reqcmt = '1' THEN 'Y' ELSE '' END reqcmt, usdt, uedt, NULL diffday"
                sSql += "  FROM lf030m"
                sSql += " WHERE uedt >= fn_ack_sysdate"
                sSql += " ORDER BY spccd"
            ElseIf riMode = 1 Then
                sSql += "SELECT spccd, spcnmd, spcifcd, spcwncd, CASE WHEN reqcmt = '1' THEN 'Y' ELSE '' END reqcmt, usdt, uedt,"
                sSql += "       CASE WHEN TO_DATE(uedt, 'yyyymmddhh24miss') - SYSDATE < 0 THEN -1 ELSE 0 END diffday"
                sSql += "  FROM lf030m"
                sSql += " ORDER BY spccd"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetSpcInfo(ByVal rsSpcCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetSpcInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT spccd, spcnm, spcnms, spcnmd, spcnmp, spcnmbp,"
            sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
            sSql += "       fn_ack_date_str(uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid,"
            sSql += "       fn_ack_get_usr_name(regid) regnm,"
            sSql += "       mbspcyn, spcifcd, spcwncd, reqcmt, bldgbn"
            sSql += "  FROM lf030m"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetSpcOrdSlipInfo(ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetSpcOrdSlipInfo(ByVal asSpcCd As String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT CASE WHEN NVL(f33.chk, '') = '' THEN '0' ELSE '1' END chk,"
            sSql += "       '[' || f68.tordslip || '] ' ||  f68.tordslipnm tordslip,"
            sSql += "        f33.dispseq, f33.useflg"
            sSql += "  FROM lf100m f68 LEFT OUTER JOIN"
            sSql += "       (SELECT '1' chk, f68.tordslip, f33.dispseq, f33.useflg"
            sSql += "          FROM lf100m f68, lf033m f33"
            sSql += "         WHERE f68.tordslip = f33.tordslip"
            sSql += "           AND f33.spccd = :spccd"
            sSql += "       ) f33 ON (f68.tordslip = f33.tordslip)"
            sSql += " ORDER BY tordslip"

            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentSpcInfo(ByVal rsSpcCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetRecentSpcInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT usdt"
            sSql += "  FROM (SELECT usdt"
            sSql += "          FROM lf030m"
            sSql += "         WHERE spccd = :spccd"
            sSql += "           AND usdt >= :usdt"
            sSql += "         ORDER BY usdt DESC"
            sSql += "       ) a"
            sSql += " WHERE ROWNUM = 1"

            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransSpcInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                 ByVal ro_Tcol2 As ItemTableCollection, ByVal riType2 As Integer, _
                                 ByVal rsSpcCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = "Public Function TransSpcInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            Dim dt As New DataTable

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'lf030M : 검체마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        'UPDATE uedt of previous record
                        sSql = ""
                        sSql += "UPDATE lf030m SET uedt = :usdt"
                        sSql += " WHERE (spccd, usdt) IN"
                        sSql += "       (SELECT a.spccd, a.usdt"
                        sSql += "          FROM (SELECT spccd, usdt"
                        sSql += " 				   FROM lf030m"
                        sSql += " 				  WHERE spccd = :spccd"
                        sSql += " 				    AND usdt  < :usdt"
                        sSql += " 				    AND uedt  > :usdt"
                        sSql += "                 ORDER BY usdt DESC"
                        sSql += "              ) a"
                        sSql += "        WHERE ROWNUM = 1"
                        sSql += " 	    )"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO lf030m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'lf030h Backup
                        sSql = ""
                        sSql += "INSERT INTO lf030h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf030m f"
                        sSql += " WHERE spccd = :spccd"
                        sSql += "   AND usdt  = :usdt"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "SPCCD", "USDT"

                                    Case Else
                                        sFields += sField + " = :" + sField + ","

                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)

                            sSql = ""
                            sSql += "UPDATE lf030m SET " + sFields
                            sSql += " WHERE spccd = :spccd"
                            sSql += "   AND usdt  = :usdt"

                            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()


                        Next
                    End With
            End Select

            'rf033M : 처방슬립별 검체 마스터
            Select Case riType2
                Case 0      '----- 신규
                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO lf033m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    'rf033H Backup
                    sSql = ""
                    sSql += "INSERT INTO lf033h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf033m f"
                    sSql += " WHERE spccd = :spccd"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE lf033m"
                    sSql += " WHERE spccd = :spccd"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO lf033m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

            '-- OCS 관련 수정
            With dbCmd
                .CommandType = CommandType.StoredProcedure
                .CommandText = "pro_ack_exe_ocs_spc"

                .Parameters.Clear()
                .Parameters.Add(New OracleParameter("rs_iud", "I"))
                .Parameters.Add(New OracleParameter("rs_spccd", rsSpcCd))
                .Parameters.Add(New OracleParameter("rs_editid", USER_INFO.USRID))
                .Parameters.Add(New OracleParameter("rs_editip", USER_INFO.LOCALIP))

                .Parameters.Add("rs_errmsg",  OracleDbType.Varchar2, 4000)
                .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                .Parameters("rs_errmsg").Value = ""

                .ExecuteNonQuery()

                Dim sRetVal As String = .Parameters(4).Value.ToString

                If sRetVal <> "00" Then
                    dbTran.Rollback()
                    Throw (New Exception(sRetVal.Substring(2)))
                End If
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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransSpcInfo_DEL(ByVal rsSpcCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_DEL(string, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            Dim dt As New DataTable

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            '   lf030h Insert
            sSql = ""
            sSql += "INSERT INTO lf030h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf030m f"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf030M Delete
            sSql = ""
            sSql += "DELETE lf030m"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf030h Insert
            sSql = ""
            sSql += "INSERT INTO lf033h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf033m f"
            sSql += " WHERE spccd = :spccd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf030M Delete
            sSql = ""
            sSql += "DELETE lf033m"
            sSql += " WHERE spccd = :spccd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function


    Public Function TransSpcInfo_UE(ByVal rsSpcCd As String, ByVal rsUsDt As String, ByVal rsUeDt As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_UE(String, String, String) As Boolean"

        Dim dbCn As New OracleConnection
        Dim dbTran As OracleTransaction
        Dim dbCmd As New OracleCommand

        Try

            Dim sMsg As String = ifExistOtherUsableData("lf030M", "SPCCD", rsSpcCd, rsUsDt)

            If IsNothing(sMsg) Then
                MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
                Exit Function
            End If

            If Not sMsg = "" Then
                MsgBox(sMsg, MsgBoxStyle.Critical)
                Exit Function
            End If

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            dbCn = GetDbConnection()
            dbTran = dbCn.BeginTransaction()

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim dt As New DataTable

            '   lf030h Insert
            sSql = ""
            sSql += "INSERT INTO lf030h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf030m f"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf030M Update
            sSql = ""
            sSql += "UPDATE lf030m SET uedt = :uedt, regdt = fn_ack_sysdate, regid = :regid"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDt
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransSpcInfo_UPD_UE(ByVal rsSpcCd As String, ByVal rsUsDt As String, ByVal rsUeDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_UPD_UE(String, String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim dt As New DataTable

            '   lf030H Insert
            sSql = ""
            sSql += "INSERT INTO lf030h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf030m f"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf030M Update
            sSql = ""
            sSql += "UPDATE lf030m SET"
            sSql += "       uedt  = :uedt,"
            sSql += "       regdt = fn_ack_sysdate,"
            sSql += "       regid = :regid"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransSpcInfo_UPD_US(ByVal rsSpcCd As String, ByVal rsUsDt As String, ByVal rsUsDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransSpcInfo_UPD_US() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim dt As New DataTable

            '   lf030h Insert
            sSql = ""
            sSql += "INSERT INTO lf030h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf030m f"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf030M Update
            sSql = ""
            sSql += "UPDATE lf030m SET"
            sSql += "       usdt  = :usdtchg,"
            sSql += "       regdt = fn_ack_sysdate,"
            sSql += "       regid = :regid"
            sSql += " WHERE spccd = :spccd"
            sSql += "   AND usdt  = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usdtchg",  OracleDbType.Varchar2).Value = rsUsDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_TEST
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : RISAPP.APP_F_TEST" & vbTab

    Public Function GetTestCdInfo_tmp(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetTestCdInfo_tmp() As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += " SELECT *"
            sSql += "   FROM rf060m_tmp"
            sSql += "  WHERE testcd = :testcd"
            sSql += "    AND spccd  = :spccd"

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransTestInfo_tmp(ByVal ro_Tcol1 As ItemTableCollection) As Boolean
        Dim sFn As String = "Public Function TransTestInfo_tmp(ItemTableCollection) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf060m : 검사마스터
            With ro_Tcol1
                For i As Integer = 1 To .ItemTableRowCount
                    sField = "" : sFields = "" : sValue = "" : sValues = ""

                    dbCmd.Parameters.Clear()
                    For j As Integer = 1 To .ItemTableColCount
                        sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                        sFields += sField + ","

                        sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                        sValues += ":" + sField + ","

                        If sValue = "" Then
                            dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = DBNull.Value
                        Else
                            dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                        End If
                    Next

                    'insert new record
                    sFields = sFields.Substring(0, sFields.Length - 1)
                    sValues = sValues.Substring(0, sValues.Length - 1)
                    sSql = "INSERT INTO rf060m_tmp (" + sFields + ") VALUES (" + sValues + ")"

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()
                Next
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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function GetTestCdInfo_xls(ByVal rsTableNm As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String, ByVal rsTclsCd As String, ByVal rsTSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetTestCdInfo() As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            If rsTableNm.ToLower = "rf062m" Then
                sSql += " SELECT *"
                sSql += "   FROM " + rsTableNm + ""
                sSql += "  WHERE testcd = :testcd"
                sSql += "    AND spccd  = :spccd"
                sSql += "    AND tclscd = :tclscd"
                sSql += "    AND tspccd = :tspccd"

                alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                alParm.Add(New OracleParameter("tclscd",  OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))
                alParm.Add(New OracleParameter("tspccd",  OracleDbType.Varchar2, rsTSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTSpcCd))
            Else
                sSql += " SELECT *"
                sSql += "   FROM " + rsTableNm + ""
                sSql += "  WHERE testcd = :testcd"
                sSql += "    AND spccd  = :spccd"
                sSql += "    AND usdt   = :usdt"

                alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            End If

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function fnGetTnm(ByVal rsTnm As String) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql = ""
            sSql += "SELECT DISTINCT testcd, tnm, uedt"
            sSql += "  FROM ("
            sSql += "        SELECT testcd, tnm, uedt, '1' seq"
            sSql += "          FROM rf060m"
            sSql += "         WHERE UPPER(tnm) LIKE :tnmd || '%'"
            sSql += "           AND uedt > fn_ack_sysdate"
            sSql += "         UNION  "
            sSql += "        SELECT testcd, tnm, uedt, '2' seq"
            sSql += "          FROM rf060m"
            sSql += "         WHERE UPPER(tnm) LIKE like '%' || :tnmd ||'%'"
            sSql += "           AND uedt > fn_ack_sysdate"
            sSql += "       )"
            sSql += " ORDER BY seq, testcd, uedt DESC "

            al.Add(New OracleParameter("tnm",  OracleDbType.Varchar2, rsTnm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnm))
            al.Add(New OracleParameter("tnm",  OracleDbType.Varchar2, rsTnm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnm))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function


    Public Function fnGetHelpInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String, _
                                  ByVal rsUsDt As String, ByRef r_dt_Ref As DataTable, _
                                  ByRef r_dt_DTest As DataTable, ByRef r_dt_RTest As DataTable) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList
            Dim dt As New DataTable

            sSql = ""
            sSql += "SELECT f6.tnm, f6.tnms, f6.tnmp, f6.tnmd, f6.tnmbp, f3.spcnm,"
            sSql += "       CASE WHEN f6.tcdgbn = 'P' THEN '[P] Parent' WHEN f6.tcdgbn = 'S' THEN '[S] Single'"
            sSql += "            WHEN f6.tcdgbn = 'C' THEN '[C] Child'  WHEN f6.tcdgbn = 'G' TH'EN [G] Group'"
            sSql += "            WHEN f6.tcdgbn = 'B' THEN '[B] Battery' "
            sSql += "       END tcdgbn,"
            sSql += "       f6.tordcd, f6.sugacd, f6.exlabyn, f6.exlabcd,"
            sSql += "       CASE WHEN SUBSTR(f6.exeday, 1, 1) = 'Y' THEN '1' ELSE '0' END exeday1,"
            sSql += "       CASE WHEN SUBSTR(f6.exeday, 2, 1) = 'Y' THEN '1' ELSE '0' END exeday2,"
            sSql += "       CASE WHEN SUBSTR(f6.exeday, 3, 1) = 'Y' THEN '1' ELSE '0' END exeday3,"
            sSql += "       CASE WHEN SUBSTR(f6.exeday, 4, 1) = 'Y' THEN '1' ELSE '0' END exeday4,"
            sSql += "       CASE WHEN SUBSTR(f6.exeday, 5, 1) = 'Y' THEN '1' ELSE '0' END exeday5,"
            sSql += "       CASE WHEN SUBSTR(f6.exeday, 6, 1) = 'Y' THEN '1' ELSE '0' END exeday6,"
            sSql += "       CASE WHEN SUBSTR(f6.exeday, 7, 1) = 'Y' THEN '1' ELSE '0' END exeday7,"
            sSql += "       f6.titleyn, f6.seqtyn, f6.seqtmi, f6.dispseqo, f6.dispseql, f6.rptyn, f6.tatyn, f6.prptmi, f6.frptmi,"
            sSql += "       '[' || f4.tubecd + '] ' ||  f4.tubenmd tubenm, '[' || f1.bcclscd || '] ' ||  f1.bcclsnmd bcclsnmd,"
            sSql += "       f6.cwarning, f6.owarning, f6.emergbn, f6.ctgbn, f6.poctyn, f6.ptgbn,"
            sSql += "       CASE WHEN NVL(f6.iogbn, '0') = '0' THEN '1'"
            sSql += "            ELSE CASE WHEN NVL(f6.iogbn, '0') = '1' THEN '1' ELSE '0' END"
            sSql += "       END iogbn0,"
            sSql += "       CASE WHEN NVL(f6.iogbn, '0') = '0' THEN '1'"
            sSql += "            ELSE CASE WHEN NVL(f6.iogbn, '0') = '2' THEN '1' ELSE '0' END"
            sSql += "       END iogbn1,"
            sSql += "       '[' || CASE WHEN NVL(f6.bccnt, '0') = 'A' THEN '2' WHEN NVL(f6.bccnt, '0') = 'B' THEN '2'"
            sSql += "                   ELSE f6.bccnt"
            sSql += "              END || ']' bccnt, f6.dspccd1, '[' || f6.dspccd1 || ']' dspcnm1_01,"
            sSql += "       f6.tordslip, '[' || f6.tordslip || ']' tordslip_01, f6.srecvlt, f6.rrptst, f6.fixrptusr, f6.fixrptyn,"
            sSql += "       '[' || f6.partcd || f6.slipcd || ']' slipcd2, f6.rptyn,"
            sSql += "       CASE WHEN NVL(f6.rsttype, '0') = '0' THEN '문자 + 숫자 혼합' ELSE '숫자만 허용' END rsttype,"
            sSql += "       f6.rstunit, CASE WHEN f6.rstllen = '-1' THEN '' ELSE rstllen END rstllen,"
            sSql += "       CASE WHEN f6.rstulen = '-1' THEN '' ELSE rstulen END rstulen,"
            sSql += "       CASE WHEN f6.refgbn = '0' THEN '없음' WHEN f6.refgbn = '1' THEN '문자' WHEN f6.refgbn = '2' THEN '숫자' END refgbn, f6.ordhide,"
            sSql += "       CASE WHEN f6.cutopt = '0' THEN '' WHEN f6.cutopt =  '1' THEN '올림' WHEN f6.cutopt = '2' THEN '반올림' WHEN f6.cutopt = '3' THEN '내림' END cutopt,"
            sSql += "       f6.descref, '[' || f6.panicgbn || ']' panicgbn, f6.panicl, f6.panich, '[' || f6.deltagbn || ']' deltagbn, f6.deltal, f6.deltah,"
            sSql += "       '[' || f6.criticalgbn || ']' criticalgbn, f6.criticall, f6.criticalh, '[' || f6.alertgbn || ']' alertgbn,"
            sSql += "       f6.alertl, f6.alerth, f6.deltaday, CASE WHEN NVL(f6.judgtype, '0') = '0' THEN '1' ELSE '0' END judgtype0,"
            sSql += "       CASE WHEN NVL(f6.judgtype, '0') = '1' THEN '1' ELSE '0' END judgtype1,"
            sSql += "       CASE WHEN LENGTH(NVL(f6.judgtype, '0')) = 6 THEN '1' ELSE '0' END judgtype2,"
            sSql += "       CASE WHEN LENGTH(NVL(f6.judgtype, '0')) = 9 THEN '1' ELSE '0' END judgtype3,"
            sSql += "       f6.ujudglt1, f6.ujudglt2, f6.ujudglt3,"
            sSql += "       CASE WHEN SUBSTR(f6.judgtype, 2, 1) = '1' THEN '[' ||SUBSTR(f6.judgtype, 3, 1) || ']' ELSE '[]' END judgtype11_01,"
            sSql += "       CASE WHEN SUBSTR(f6.judgtype, 5, 1) = '2' THEN '[' ||SUBSTR(f6.judgtype, 6, 1) || ']' ELSE '[]' END judgtype12_01,"
            sSql += " 	    CASE WHEN SUBSTR(f6.judgtype, 8, 1) = '3' THEN '[' ||SUBSTR(f6.judgtype, 9, 1) || ']' ELSE '[]' END judgtype13_01,"
            sSql += "       '[' || f6.alimitgbn || ']' alimitgbn, f6.alimitl, f6.alimith, '[' || f6.alimitls || ']' alimitls , '[' || f6.alimiths || ']' alimiths"
            sSql += "  FROM rf060m f6, lf030m f3, lf040m f4, rf020m f2, rf010m f1"
            sSql += " WHERE f6.testcd = :testcd"
            sSql += "   AND f6.spccd  = :spccd"
            sSql += "   AND f6.usdt   = :usdt"
            sSql += "   AND f6.spccd  = f3.spccd"
            sSql += "   AND f6.usdt  >= f3.usdt"
            sSql += "   AND f6.tubecd = f4.tubecd"
            sSql += "   AND f6.usdt  >= f4.usdt"
            sSql += "   AND f6.bcclscd = f1.bcclscd"
            sSql += "   AND f6.partcd  = f2.partcd"

            al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            dt = DbExecuteQuery(sSql, al)

            If dt.Rows.Count > 0 Then
                al = New ArrayList

                sSql = ""
                sSql += "SELECT refseq,"
                sSql += "       CASE WHEN ageymd = 'D' THEN 'day' WHEN ageymd = 'M' THEN 'month' WHEN ageymd = 'Y' THEN 'year' END ageymd, sage,"
                sSql += "       CASE WHEN sages  = '0' THEN '<=' WHEN sages  = '1' THEN '<' END sages, eage,"
                sSql += "       CASE WHEN eages  = '0' THEN '<=' WHEN eages  = '1' THEN '<' END eages, reflm, refhm,"
                sSql += "       CASE WHEN reflms = '0' THEN '<=' WHEN reflms = '1' THEN '<' END reflms,"
                sSql += "       CASE WHEN refhms = '0' THEN '<=' WHEN refhms = '1' THEN '<' END refhms, refrf, refhf,"
                sSql += "       CASE WHEN refrfs = '0' THEN '<=' WHEN refrfs = '1' THEN '<' END refrfs,"
                sSql += "       DECODE(refhfs,'0','<=','1','<') refhfs, reflt"
                sSql += "  FROM rf061m"
                sSql += " WHERE testcd = :testcd"
                sSql += "   AND spccd  = :spccd"
                sSql += "   AND usdt   = :usdt"
                sSql += " ORDER BY refseq"

                al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

                DbCommand()
                r_dt_Ref = DbExecuteQuery(sSql, al)


                al = New ArrayList

                sSql = ""
                sSql += "SELECT f62.testcd, f62.spccd, f60.tnmd, f60.uedt, dispseql"
                sSql += "  FROM rf062m f62, rf060m f60"
                sSql += " WHERE f62.tclscd = :testcd"
                sSql += "   AND f62.spccd  = :spccd"
                sSql += "   AND f62.testcd = f60.testcd"
                sSql += "   AND f62.spccd  = f60.spccd"
                sSql += "   AND f60.usdt   = :usdt"
                sSql += " ORDER BY dispseql"

                al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

                DbCommand()
                r_dt_DTest = DbExecuteQuery(sSql, al)


                al = New ArrayList

                sSql = ""
                sSql += "SELECT f67.reftestcd testcd, f67.refspccd spccd, f60.tnmd, f60.uedt"
                sSql += "  FROM rf063m f67, rf060m f60"
                sSql += " WHERE f67.testcd    = :testcd"
                sSql += "   AND f67.spccd     = :spccd"
                sSql += "   AND f67.reftestcd = f60.testcd"
                sSql += "   AND f67.refspccd  = f60.spccd"
                sSql += "   AND f60.usdt      = :usdt"

                al.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                al.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                al.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

                DbCommand()
                r_dt_RTest = DbExecuteQuery(sSql, al)
            End If

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function

    Public Function TransTestInfo_DEL(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = " Public Function TransTestInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim dt As New DataTable

            'rf060M : 검사마스터
            '   rf060H Insert
            sSql = ""
            sSql += "INSERT INTO rf060h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf060m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf060M Delete
            sSql = ""
            sSql += "DELETE rf060m"
            sSql += "  WHERE testcd = :testcd"
            sSql += "    AND spccd  = :spccd"
            sSql += "    AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf061M : 참고치 마스터
            '   rf061H Insert
            sSql = ""
            sSql += "INSERT INTO rf061h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf061m f"
            sSql += "  WHERE testcd = :testcd"
            sSql += "    AND spccd  = :spccd"
            sSql += "    AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf061M Delete
            sSql = ""
            sSql += "DELETE rf061m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf062M : 세부검사마스터
            '   rf062H Insert
            sSql = ""
            sSql += "INSERT INTO rf062h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf062m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND (testcd, spccd) NOT IN"
            sSql += "       (SELECT testcd, spccd FROM rf060m"
            sSql += "         WHERE testcd = :testcd"
            sSql += "           AND spccd  = :spccd"
            sSql += "       )"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf062M Delete
            sSql = ""
            sSql += "DELETE rf062m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND (testcd, spccd) NOT IN"
            sSql += "       (SELECT testcd, spccd FROM rf060m"
            sSql += "         WHERE testcd = :testcd"
            sSql += "           AND spccd  = :spccd"
            sSql += "       )"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf063M : 참조검사마스터
            '   rf063H Insert
            sSql = ""
            sSql += "INSERT INTO rf063h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf063m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND (testcd, spccd) NOT IN"
            sSql += "       (SELECT testcd, spccd FROM rf060m"
            sSql += "         WHERE testcd = :testcd"
            sSql += "           AND spccd  = :spccd"
            sSql += "       )"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf063M Delete
            sSql = ""
            sSql += "DELETE rf063m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND (testcd, spccd) NOT IN"
            sSql += "       (SELECT testcd, spccd FROM rf060m"
            sSql += "         WHERE testcd = :testcd"
            sSql += "           AND spccd  = :spccd"
            sSql += "       )"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTestInfo_UPD_UE(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String, ByVal rsUeDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransTestInfo_UPD_UE() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim dt As New DataTable

            'rf060M : 검사 마스터
            '   rf060H Insert
            sSql = ""
            sSql += "INSERT INTO rf060h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf060m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf060M Update
            sSql = ""
            sSql += "UPDATE rf060m SET"
            sSql += "       uedt   = :uedt,"
            sSql += "       regdt  = fn_ack_sysdate,"
            sSql += "       regid  = :regid"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTestInfo_UPD_US(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String, ByVal rsUsDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransTestInfo_UPD_US() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim bITF_yn As Boolean = False
            Dim dt As New DataTable

            'rf060M : 검사 마스터
            '   rf060H Insert
            sSql = ""
            sSql += "INSERT INTO rf060h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf060m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf060M Update
            sSql = ""
            sSql += "UPDATE rf060m SET"
            sSql += "       usdt   = :usdtchg,"
            sSql += "       regdt  = fn_ack_sysdate,"
            sSql += "       regid  = :regid"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usdtchg",  OracleDbType.Varchar2).Value = rsUsDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()


            'rf061M : 참고치 마스터
            '   rf061H Insert
            sSql = ""
            sSql += "INSERT INTO rf061h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf061m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf061M Update
            sSql = ""
            sSql += "UPDATE rf061m SET"
            sSql += "       usdt   = :usdtchg,"
            sSql += "       regdt  = fn_ack_sysdate,"
            sSql += "       regid  = :regid"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usdtchg",  OracleDbType.Varchar2).Value = rsUsDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function GetAgeRefInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetAgeRefInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT CASE WHEN ageymd = 'D' THEN '0' WHEN ageymd = 'M' THEN '1' WHEN ageymd = 'Y' THEN '2' END ageymd,"
            sSql += "       sage, sages, eages, eage, reflm, reflms, refhms, refhm, reflf, reflfs, refhfs, refhf, reflt"
            sSql += "  FROM rf061m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"
            sSql += " ORDER BY refseq"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTestInfo_detail(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetTestInfo_detail(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT a.testcd, a.spccd, a.tnmd, a.grprstyn, a.sort_key"
            sSql += "  FROM ("
            sSql += "        SELECT t.testcd, t.spccd, t.tnmd tnmd, d.grprstyn, t.usdt, t.uedt, t.dispseql sort_key"
            sSql += "          FROM rf062m d, rf060m t"
            sSql += "         WHERE d.testcd = t.testcd"
            sSql += "           AND d.spccd  = t.spccd"
            sSql += "           AND d.tclscd = :testcd"
            sSql += "           AND d.tspccd = :spccd"
            sSql += "       ) a,"
            sSql += "       ("
            sSql += "        SELECT t.testcd, t.spccd, MAX(t.usdt) usdt"
            sSql += "          FROM rf062m d, rf060m t"
            sSql += "         WHERE d.testcd = t.testcd"
            sSql += "           AND d.spccd  = t.spccd"
            sSql += "           AND d.tclscd = :testcd"
            sSql += "           AND d.tspccd = :spccd"
            sSql += "         GROUP BY t.testcd, t.spccd"
            sSql += "       ) b"
            sSql += " WHERE a.testcd = b.testcd"
            sSql += "   AND a.spccd  = b.spccd"
            sSql += "   AND a.usdt   = b.usdt"
            sSql += " ORDER BY sort_key"

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTestInfo_ref(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetTestInfo_ref(String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT DISTINCT reftestcd testcd, refspccd spccd, t.tnmd tnmd"
            sSql += "  FROM rf063m r, rf060m t"
            sSql += " WHERE r.reftestcd = t.testcd"
            sSql += "   AND r.refspccd  = t.spccd"
            sSql += "   AND r.testcd    = :testcd"
            sSql += "   AND r.spccd     = :spccd"
            sSql += " ORDER BY r.reftestcd, r.refspccd"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTestInfo_info(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
        Dim sFn As String = "Public Function GetTestInfo_ref(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT DISTINCT"
            sSql += "       infogbn, testinfo,"
            sSql += "       CASE WHEN spccd = '----' THEN 1 ELSE 2 END sort1"
            sSql += "  FROM rf064m"
            sSql += " WHERE testcd  = :testcd"
            sSql += "   AND spccd  IN ('----', :spccd)"
            sSql += " ORDER BY infogbn, sort1"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentTestInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetRecentTestInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT usdt,partgbn"
            sSql += "  FROM (SELECT usdt,partgbn"
            sSql += "          FROM vw_ack_tot_test_info"
            sSql += "         WHERE testcd = :testcd"
            sSql += "           AND spccd  = :spccd"
            sSql += "           AND ((usdt   >= :usdt AND PARTGBN = '[핵의학검사실]') OR PARTGBN = '[진단검사실]')"
            sSql += "         ORDER BY usdt DESC"
            sSql += "       ) a"
            sSql += " WHERE ROWNUM = 1"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentTOrdCdInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsTOrdCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetRecentTOrdCdInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT testspc"
            sSql += "  FROM (SELECT testcd || spccd testspc"
            sSql += "          FROM rf060m"
            sSql += "         WHERE tordcd  = :tordcd"
            sSql += "           AND testcd <> :testcd"
            sSql += "           AND uedt   >= :usdt"
            sSql += "         ORDER BY testspc DESC"
            sSql += "       ) a"
            sSql += " WHERE ROWNUM = 1"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("tordcd",  OracleDbType.Varchar2, rsTOrdCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTOrdCd))
            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTestInfo(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetTestInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT testcd, spccd, '['|| spccd || ']' spcnmd_01, tnm, tnms, tnmd, tnmp, tnmbp,"
            sSql += "       '[' || tcdgbn || ']' tcdgbn_01, titleyn, tordcd, tliscd, insugbn, sugacd, edicd,"
            sSql += "       CASE WHEN NVL(rptyn, '0') = '1' THEN '0' ELSE '1' END rptyn, dispseqo, dispseql,"
            sSql += "       SUBSTR(exeday, 1, 1) exeday1,"
            sSql += "       SUBSTR(exeday, 2, 1) exeday2,"
            sSql += "       SUBSTR(exeday, 3, 1) exeday3,"
            sSql += "       SUBSTR(exeday, 4, 1) exeday4,"
            sSql += "       SUBSTR(exeday, 5, 1) exeday5,"
            sSql += "       SUBSTR(exeday, 6, 1) exeday6,"
            sSql += "       SUBSTR(exeday, 7, 1) exeday7,"
            sSql += "       tatyn, prptmi, ':M' prptmi_01, frptmi, ':M' frptmi_01, rrptst, srecvlt, cwarning,"
            sSql += "       tubecd, '[' || tubecd || ']' tubenmd_01, tubevol, tubeunit, minspcvol, exlabyn, exlabcd,"
            sSql += "       '[' || exlabcd || ']' exlabnmd_01, seqtyn, seqtmi, ctgbn, poctyn,"
            sSql += "       bcclscd, '[' || bcclscd || ']' bcclsnmd_01, slipcd2, '[' || slipcd2 || ']' slipnmd_01,"
            sSql += "       samecd, '[' || mbttype || ']' mbttype_01, '[' || bbttype || ']' bbttype_01, '[' || mgttype || ']' mgttype_01,"
            sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt, fn_ack_date_str(uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid, fn_ack_get_usr_name(regid) regnm,"
            sSql += "       CASE WHEN NVL(rsttype, '0') = '0' THEN '1' ELSE '0' END rsttype0,"
            sSql += "       CASE WHEN NVL(rsttype, '0') = '1' THEN '1' ELSE '0' END rsttype1,"
            sSql += "       CASE WHEN NVL(cutopt, '0')  = '0' THEN '0' ELSE '1' END rstlen,"
            sSql += "       NVL(rstulen, -1) rstulen_01, NVL(rstllen, -1) rstllen_01,"
            sSql += "       CASE WHEN cutopt = '1' THEN '1' ELSE '0' END cutopt1,"
            sSql += "       CASE WHEN cutopt = '2' THEN '1' ELSE '0' END cutopt2,"
            sSql += "       CASE WHEN cutopt = '3' THEN '1' ELSE '0' END cutopt3,"
            sSql += "       CASE WHEN NVL(refgbn, '0') = '0' THEN '1' ELSE '0' END refgbn0,"
            sSql += "       CASE WHEN NVL(refgbn, '0') = '1' THEN '1' ELSE '0' END refgbn1,"
            sSql += "       CASE WHEN NVL(refgbn, '0') = '2' THEN '1' ELSE '0' END refgbn2,"
            sSql += "       rstunit, descref, CASE WHEN NVL(judgtype, '0') = '0' THEN '1' ELSE '0' END judgtype0,"
            sSql += "       CASE WHEN NVL(judgtype, '0') = '1' THEN '1' ELSE '0' END judgtype1,"
            sSql += "       CASE WHEN LENGTH(NVL(judgtype, '0')) = 6 THEN '1' ELSE '0' END judgtype2,"
            sSql += "       CASE WHEN LENGTH(NVL(judgtype, '0')) = 9 THEN '1' ELSE '0' END judgtype3,"
            sSql += "       ujudglt1, ujudglt2, ujudglt3,"
            sSql += "       CASE WHEN SUBSTR(judgtype, 2, 1) = '1' THEN '[' || SUBSTR(judgtype, 3, 1) || ']' ELSE '[]' END judgtype11_01,"
            sSql += "       CASE WHEN SUBSTR(judgtype, 5, 1) = '2' THEN '[' || SUBSTR(judgtype, 6, 1) || ']' ELSE '[]' END judgtype12_01,"
            sSql += " 	    CASE WHEN SUBSTR(judgtype, 8, 1) = '3' THEN '[' || SUBSTR(judgtype, 9, 1) || ']' ELSE '[]' END judgtype13_01,"
            sSql += "       '[' || panicgbn || ']' panicgbn_01, '[' ||criticalgbn || ']' criticalgbn_01,"
            sSql += " 	    '[' || alertgbn || ']' alertgbn_01, '[' ||deltagbn || ']' deltagbn_01,"
            sSql += "       panicl, panich, criticall, criticalh, alertl, alerth, deltaday, deltal, deltah,"
            sSql += "       '[' || alimitgbn || ']' alimitgbn_01, alimitl, '[' || alimitls || ']' alimitls_01, alimith, '[' || alimiths || ']' alimiths_01,"
            sSql += "       reqsub, tordslip, '[' || tordslip || ']' tordslip_01, ptgbn,"
            sSql += "       CASE WHEN NVL(iogbn, '0') = '0' THEN '1' ELSE CASE WHEN NVL(iogbn, '0') = '1' THEN '1' ELSE '0' END END iogbn0,"
            sSql += "       CASE WHEN NVL(iogbn, '0') = '0' THEN '1' ELSE CASE WHEN NVL(iogbn, '0') = '2' THEN '1' ELSE '0' END END iogbn1,"
            sSql += "       CASE WHEN emergbn IN ('1', '3') THEN '1' ELSE '' END ergbn1,"
            sSql += "       CASE WHEN emergbn IN ('2', '3') THEN '1' ELSE '' END ergbn2,"
            sSql += "       fixrptyn, fixrptusr, dspccd1, '[' || dspccd1 || ']' dspcnm1_01, dspccd2, '[' || dspccd2 || ']' dspcnm2_01,"
            sSql += "       ordhide, '[' || owarninggbn || ']' owarninggbn_01, owarning,"
            sSql += "       SUBSTR(oreqitem, 1, 1) oreqitem1,"
            sSql += "       SUBSTR(oreqitem, 2, 1) oreqitem2,"
            sSql += "       SUBSTR(oreqitem, 3, 1) oreqitem3,"
            sSql += "       SUBSTR(oreqitem, 4, 1) oreqitem4,"
            sSql += "       viwsub, '[' || bccnt || ']' bccnt_01, bconeyn, grprstyn,"
            sSql += "       CASE WHEN cprtgbn = '0' THEN '[0] 없음' WHEN cprtgbn = '1' THEN '[1] 수혈의뢰서'"
            sSql += "            WHEN cprtgbn = '2' THEN '[2] 유전자동의서'"
            sSql += "       END cprtgbn_01"
            sSql += "  FROM vw_ack_ris_test_info"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTestInfo(ByVal riMode As Integer, ByVal rsSerch As String) As DataTable
        Dim sFn As String = "Public Function GetTestInfo(Integer, String) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT DISTINCT"
                sSql += "       RPAD(testcd, 8, ' ') || spccd tcd, tnmd, spcnmd, '[' || tubecd || '] ' || tubenmd tubenmd,"
                sSql += "       tcdgbn, '[' || tordslip || '] ' || tordslipnm || '(' || LPAD(NVL(dispseqo, '0'), 3, '0') || ')' tordslipnm,"
                sSql += "       CASE WHEN ordhide = '1' THEN 'X' ELSE '' END ordhide, tordcd, sugacd, dspccd1, tliscd,"
                sSql += "       '[' || bcclscd || '] ' || bcclsnmd bcclsnmd, '[' || slipcd2 || '] ' || slipnmd slipnmd,"
                sSql += "       CASE WHEN NVL(exlabyn, '0') = '0' THEN '' ELSE '[' || exlabcd || '] '|| exlabnmd END exlabnmd, titleyn,"
                sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt, uedt, bcclscd, tordslip, slipcd, testcd, spccd,"
                sSql += "       dispseql, dispseqo, 0 diffday"
                sSql += "  FROM vw_ack_ris_test_info"
                sSql += " WHERE uedt >= fn_ack_sysdate"
                If rsSerch <> "" Then
                    sSql += "   AND  " + rsSerch + ""
                End If

                sSql += " ORDER BY testcd, spccd, usdt"
            ElseIf riMode = 1 Then
                sSql += "SELECT DISTINCT"
                sSql += "       RPAD(testcd, 8, ' ') || spccd tcd, tnmd, spcnmd, '[' || tubecd || '] ' ||  tubenmd tubenmd,"
                sSql += "       tcdgbn, '[' || tordslip || '] ' ||  tordslipnm || '(' || LPAD(NVL(dispseqo, '0'), 3, '0') || ')' tordslipnm,"
                sSql += "       CASE WHEN ordhide = '1' THEN 'X' ELSE '' END ordhide, tordcd, sugacd, dspccd1, tliscd,"
                sSql += "       '[' || bcclscd || '] ' ||  bcclsnmd bcclsnmd, '[' || slipcd2 || '] ' ||  slipnmd slipnmd,"
                sSql += "       CASE WHEN NVL(exlabyn, '0') = '0' THEN '' ELSE '[' || exlabcd || '] '|| exlabnmd END exlabnmd, titleyn,"
                sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt, uedt, bcclscd, tordslip, slipcd, testcd, spccd,"
                sSql += "       dispseql, dispseqo,"
                sSql += "       CASE WHEN TO_DATE(uedt, 'yyyymmddhh24miss') - SYSDATE < 0 THEN -1 ELSE 0 END diffday"
                sSql += "  FROM vw_ack_ris_test_info "
                If rsSerch <> "" Then
                    sSql += " WHERE  " + rsSerch + ""
                End If

                sSql += " ORDER BY testcd, spccd, usdt"

            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTestInfo_NotSpc(ByVal riMode As Integer, ByVal rsSerch As String) As DataTable
        Dim sFn As String = "Public Function GetTestInfo_Exact(ByVal iMode As Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT DISTINCT"
                sSql += "       RPAD(testcd, 8, ' ')  tcd, tnmd, '' spcnmd, '' tubenmd,"
                sSql += "       tcdgbn, '[' || tordslip || '] ' || tordslipnm || '(' || LPAD(NVL(dispseqo, '0'), 3, '0') || ')' tordslipnm,"
                sSql += "       CASE WHEN ordhide = '1' THEN 'X' ELSE '' END ordhide, tordcd, sugacd, '' dspccd1, tliscd,"
                sSql += "       '[' || bcclscd || '] ' || bcclsnmd bcclsnmd, '[' || slipcd2 || '] ' || slipnmd slipnmd,"
                sSql += "       '' exlabnmd, '' titleyn,"
                sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt, uedt, bcclscd, tordslip, slipcd, testcd, '' spccd,"
                sSql += "       dispseql, dispseqo, 0 diffday"
                sSql += "  FROM vw_ack_ris_test_info "
                sSql += " WHERE uedt >= fn_ack_sysdate"
                If rsSerch <> "" Then
                    sSql += "   AND  " + rsSerch + ""
                End If

                sSql += " ORDER BY testcd, usdt"
            ElseIf riMode = 1 Then
                sSql += "SELECT DISTINCT "
                sSql += "       RPAD(testcd, 8, ' ')  tcd, tnmd, '' spcnmd, '' tubenmd,"
                sSql += "       tcdgbn, '[' || tordslip || '] ' ||  tordslipnm + '(' || LPAD(NVL(dispseqo, '0'), 3, '0') || ')' tordslipnm,"
                sSql += "       CASE WHEN ordhide = '1' THEN 'X' ELSE '' END ordhide, tordcd, sugacd, '' dspccd1, tliscd,"
                sSql += "       '['|| bcclscd || '] ' || bcclsnmd bcclsnmd, '[' || slipcd2 || '] ' || slipnmd slipnmd,"
                sSql += "       '' exlabnmd, '' titleyn,"
                sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt, uedt, bcclscd, tordslip, slipcd, testcd, '' spccd,"
                sSql += "       dispseql, dispseqo,"
                sSql += "       CASE WHEN TO_DATE(uedt, 'yyyymmddhh24miss') - SYSDATE < 0 THEN -1 ELSE 0 END diffday"
                sSql += "  FROM vw_ack_ris_test_info "
                If rsSerch <> "" Then
                    sSql += " WHERE  " + rsSerch + ""
                End If

                sSql += " ORDER BY testcd, usdt"

            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransTestInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                  ByVal ro_Tcol2 As ItemTableCollection, ByVal riType2 As Integer, _
                                  ByVal ro_Tcol3 As ItemTableCollection, ByVal riType3 As Integer, _
                                  ByVal ro_Tcol4 As ItemTableCollection, ByVal riType4 As Integer, _
                                  ByVal ro_Tcol5 As ItemTableCollection, _
                                  ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String, _
                                  Optional ByVal rbExcelMode As Boolean = False) As Boolean
        Dim sFn As String = "Public Function TransTestInfo(ByVal ro_Tcol_060m As ItemTableCollection, ByVal ro_Tcol_061m As ItemTableCollection, ByVal ro_Tcol_062m As ItemTableCollection) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""
            Dim dt As New DataTable

            'rf060M : 검사마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        'UPDATE uedt of previous record
                        sSql = ""
                        sSql += "UPDATE rf060m SET uedt = :usdt"
                        sSql += " WHERE (testcd, spccd, usdt) IN"
                        sSql += "       (SELECT a.testcd, a.spccd, a.usdt"
                        sSql += "          FROM (SELECT testcd, spccd, usdt"
                        sSql += " 				   FROM rf060m"
                        sSql += " 				  WHERE testcd = :testcd"
                        sSql += " 					AND spccd  = :spccd"
                        sSql += " 					AND usdt   < :usdt"
                        sSql += " 					AND uedt   > :usdt"
                        sSql += " 		          ORDER BY usdt DESC"
                        sSql += "               ) a"
                        sSql += "         WHERE ROWNUM = 1"
                        sSql += " 		)"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                If sValue = "" Then
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = DBNull.Value
                                Else
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End If
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf060m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf060H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf060h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf060m f"
                        sSql += " WHERE testcd = :testcd"
                        sSql += "   AND spccd  = :spccd"
                        sSql += "   AND usdt   = :usdt"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "TESTCD", "SPCCD", "USDT"

                                    Case Else
                                        sFields += sField + " =  :" + sField + ","

                                        If sValue = "" Then
                                            dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = DBNull.Value
                                        Else
                                            dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                        End If
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)

                            sSql = ""
                            sSql += "UPDATE rf060m SET " + sFields
                            sSql += " WHERE testcd = :testcd"
                            sSql += "   AND spccd  = :spccd"
                            sSql += "   AND usdt   = :usdt"

                            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With
            End Select

            'rf061M : 참고치 마스터
            Select Case riType2
                Case 0      '----- 신규
                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                If sValue = "" Then
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = DBNull.Value
                                Else
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End If
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf061m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    'rf061H Backup
                    sSql = ""
                    sSql += "INSERT INTO rf061h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf061m f"
                    sSql += " WHERE testcd = :testcd"
                    sSql += "   AND spccd  = :spccd"
                    sSql += "   AND usdt   = :usdt"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                    dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                    dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rf061m"
                    sSql += " WHERE testcd = :testcd"
                    sSql += "   AND spccd  = :spccd"
                    sSql += "   AND usdt   = :usdt"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                    dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
                    dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""
                            Dim sTmp As String = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                sTmp += "'" + sValue + "',"

                                If sValue = "" Then
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = DBNull.Value
                                Else
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End If
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf061m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With
            End Select

            '< add freety 2006/07/26 : 신규시 기존 세부검사 존재할 경우의 오류 방지, 유의할점은 Battery 세부검사는 가장 최근것으로 적용됨
            'rf062M : 세부검사마스터
            Select Case riType3
                Case 0, 1      '----- 0 : 신규, 1 : 수정

                    If rbExcelMode = False Then
                        'rf062H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf062h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf062m f"
                        sSql += " WHERE tclscd = :testcd"
                        sSql += "   AND Tspccd = :spccd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf062m"
                        sSql += " WHERE tclscd = :testcd"
                        sSql += "   AND tspccd = :spccd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                    End If

                    With ro_Tcol3
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf062m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With
            End Select
            '>

            '< add freety 2006/07/26 : 신규시 기존 참조검사 존재할 경우의 오류 방지, 유의할점은 Battery 세부검사는 가장 최근것으로 적용됨
            'rf063M : 참조검사마스터
            Select Case riType4
                Case 0, 1     '----- 0 : 신규, 1 : 수정

                    If rbExcelMode = False Then
                        'rf063H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf063h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf063m f"
                        sSql += " WHERE testcd = :testcd"
                        sSql += "   AND spccd  = :spccd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf063m"
                        sSql += " WHERE testcd = :testcd"
                        sSql += "   AND spccd  = :spccd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
                        dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                    End If

                    With ro_Tcol4
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf063m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select
            '>

            If ro_Tcol5.ItemTableRowCount > 0 Then
                'rf063H Backup
                sSql = ""
                sSql += "INSERT INTO rf064h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf064m f"
                sSql += " WHERE testcd = :testcd"

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

                dbCmd.CommandText = sSql
                iRet += dbCmd.ExecuteNonQuery()

                sSql = ""
                sSql += "DELETE rf064m"
                sSql += " WHERE testcd = :testcd"

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

                dbCmd.CommandText = sSql
                iRet += dbCmd.ExecuteNonQuery()

                With ro_Tcol5
                    For i As Integer = 1 To .ItemTableRowCount
                        sField = "" : sFields = "" : sValue = "" : sValues = ""

                        dbCmd.Parameters.Clear()
                        For j As Integer = 1 To .ItemTableColCount
                            sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                            sFields += sField + ","

                            sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                            sValues += ":" + sField + ","

                            dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                        Next

                        sFields = sFields.Substring(0, sFields.Length - 1)
                        sValues = sValues.Substring(0, sValues.Length - 1)
                        sSql = "INSERT INTO rf064m (" + sFields + ") VALUES (" + sValues + ")"

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()
                    Next
                End With

            End If


            If iRet > 0 Then
                dbTran.Commit()

                '-- OCS 관련 수정
                With dbCmd
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_ocs_test"

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("rs_testcd", rsTestCd))
                    .Parameters.Add(New OracleParameter("rs_spccd", rsSpcCd))
                    .Parameters.Add(New OracleParameter("rs_editid", USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("rs_editip", USER_INFO.LOCALIP))

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = ""

                    .ExecuteNonQuery()

                    Dim sRetVal As String = .Parameters(4).Value.ToString

                    If sRetVal <> "00" Then
                        Throw (New Exception(sRetVal.Substring(2)))
                    End If
                End With


                Return True
            Else
                dbTran.Rollback()
                Return False
            End If

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

    Public Function TransTestInfo_UE(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String, ByVal rsUeDt As String) As Boolean
        Dim sFn As String = "Public Function TransTestInfo_UE(String, String, String, String) As Boolean"

        Dim dbCn As New OracleConnection
        Dim dbTran As OracleTransaction
        Dim dbCmd As New OracleCommand

        Try
            Dim sMsg As String = ""
            Dim arrRTblNm(0) As String

            arrRTblNm(0) = "rf060"

            sMsg = ifExistOtherUsableData("rf060M", "TESTCD", "SPCCD", rsTestCd, rsSpcCd, rsUsDt)

            If IsNothing(sMsg) Then
                MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
                Exit Function
            End If

            If Not sMsg = "" Then
                MsgBox(sMsg, MsgBoxStyle.Critical)
                Exit Function
            End If

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            dbCn = GetDbConnection()
            dbTran = dbCn.BeginTransaction()

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim bITF_yn As Boolean = False
            Dim dt As New DataTable

            'rf060M : 검사마스터
            '   rf060H Insert
            'rf060H Backup
            sSql = ""
            sSql += "INSERT INTO rf060h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf060m f"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf060M Update
            sSql = ""
            sSql += "UPDATE rf060m SET uedt = :uedt, regid = :regid"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = :spccd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDt
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd
            dbCmd.Parameters.Add("spccd",  OracleDbType.Varchar2).Value = rsSpcCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTestInfo_DispseqlL(ByVal rsTestCd As String, ByVal rsDispSeq As String) As Boolean
        Dim sFn As String = "Public Function TransTestInfo_DispseqlL(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "UPDATE rf060m SET dispseql = :dispseql"
            sSql += " WHERE testcd = :testcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("dispseql",  OracleDbType.Varchar2).Value = rsDispSeq
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTestInfo_DispseqlO(ByVal rsTestCd As String, ByVal rsDispSeq As String) As Boolean
        Dim sFn As String = "Public Function TransTestInfo_DispseqlL(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "UPDATE rf060m SET dispseqo = :dispseqo"
            sSql += " WHERE testcd = :testcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("dispseqo",  OracleDbType.Varchar2).Value = rsDispSeq
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
End Class

Public Class APP_F_TGRP
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_TGRP" + vbTab

    Public Function GetTGrpInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql = ""
                sSql += "SELECT tgrpcd, tgrpnmd"
                sSql += "  FROM rf065m "
                sSql += " GROUP BY tgrpcd, tgrpnmd"

            ElseIf riMode = 1 Then
                sSql = ""
                sSql += "SELECT tgrpcd, tgrpnmd, NULL diffday, NULL moddt, NULL modid"
                sSql += "  FROM rf065m"
                sSql += " GROUP BY tgrpcd, tgrpnmd"
                sSql += " UNION ALL "
                sSql += "SELECT tgrpcd, tgrpnmd, -1 diffday,"
                sSql += "       fn_ack_date_str(moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, modid"
                sSql += "  FROM rf065h"
                sSql += " GROUP BY tgrpcd, tgrpnmd, moddt, modid"
                sSql += " ORDER BY tgrpcd, moddt, modid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTGrpInfo(ByVal rsTGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo(String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       tgrpcd, tgrpnm, tgrpnms, tgrpnmd, tgrpnmbp,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid,"
            sSql += "       fn_ack_get_usr_name(regid) regnm,"
            sSql += "       NULL moddt, NULL modid "
            sSql += "  FROM rf065m"
            sSql += " WHERE tgrpcd = :tgrpcd"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("tgrpcd",  OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alparm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTGrpInfo(ByVal rsModDT As String, ByVal rsModID As String, ByVal rsTGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       tgrpcd, tgrpnm, tgrpnms, tgrpnmd, tgrpnmbp,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid, fn_ack_get_usr_name(regid) regnm,"
            sSql += "	    fn_ack_date_str(moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, modid, fn_ack_get_usr_name(modid) modnm"
            sSql += "  FROM rf065h"
            sSql += " WHERE tgrpcd = :tgrpcd"
            sSql += "   AND moddt  = :moddt"
            sSql += "   AND modid  = :modid"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("tgrpcd",  OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDT))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTGrpInfo_Test(ByVal rsTGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo_Test(String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT a.testcd, a.spccd, b.tnmd, b.partcd || b.slipcd partslip"
            sSql += "  FROM rf065m a,"
            sSql += "       ("
            sSql += "        SELECT testcd, spccd, tnmd, partcd, slipcd"
            sSql += "          FROM rf060m"
            sSql += "         WHERE usdt <= fn_ack_sysdate"
            sSql += "           AND uedt >  fn_ack_sysdate"
            sSql += "           AND tcdgbn <> 'G'"
            sSql += "       ) b"
            sSql += " WHERE a.testcd = b.testcd"
            sSql += "   AND a.spccd  = b.spccd"
            sSql += "   AND a.tgrpcd = :tgrpcd"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("tgrpcd",  OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetTGrpInfo_Test(ByVal rsModDT As String, ByVal rsModID As String, ByVal rsTGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo_Test(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT a.testcd, a.spccd, b.tnmd, b.partcd || b.slipcd partslip"
            sSql += "  FROM rf065h a,"
            sSql += "       ("
            sSql += "        SELECT testcd, spccd, tnmd, partcd, slipcd"
            sSql += "          FROM rf060m"
            sSql += "         WHERE usdt <= fn_ack_sysdate"
            sSql += "           AND uedt >  fn_ack_sysdate"
            sSql += "           AND tcdgbn <> 'G'"
            sSql += "       ) b"
            sSql += " WHERE a.testcd = b.testcd"
            sSql += "   AND a.spccd  = b.spccd"
            sSql += "   AND a.tgrpcd = :tgrpcd"
            sSql += "   AND a.moddt  = :moddt"
            sSql += "   AND a.modid  = :modid"

            Dim alParm As New ArrayList
            alParm.Add(New OracleParameter("tgrpcd",  OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDT))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentTGrpInfo(ByVal rsTGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentTGrpInfo(String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT tgrpcd"
            sSql += "  FROM rf065m"
            sSql += " WHERE tgrpcd = :tgrpcd"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("tgrpcd",  OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransTGrpInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsTGrpCd As String) As Boolean
        Dim sFn As String = "Public Function TransTGrpInfo(ItemTableCollection, Integer, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf065M : 검사그룹 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf065m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf065H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf065h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf065m f"
                        sSql += " WHERE tgrpcd = :tgrpcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("tgrpcd",  OracleDbType.Varchar2).Value = rsTGrpCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf065m"
                        sSql += " WHERE tgrpcd = :tgrpcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("tgrpcd",  OracleDbType.Varchar2).Value = rsTGrpCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf065m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTGrpInfo_UE(ByVal rsTGrpCd As String) As Boolean
        Dim sFn As String = "Public Function TransTGrpInfo_UE(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf065M : 검사그룹 마스터
            '   rf065H Insert
            sSql = ""
            sSql += "INSERT INTO rf065h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf065m f"
            sSql += " WHERE tgrpcd = :tgrpcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tgrpcd",  OracleDbType.Varchar2).Value = rsTGrpCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf065M Delete
            sSql = ""
            sSql += "DELETE rf065m"
            sSql += " WHERE tgrpcd = :tgrpcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("tgrpcd",  OracleDbType.Varchar2).Value = rsTGrpCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
End Class

Public Class APP_F_TUBE
    Inherits APP_F

    Private Const msFile As String = "File : CGRISAPP_F.vb, Class : RISAPP.APP_F_TUBE" + vbTab
    Public Overloads Function GetTubeInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Overloads Function GetTubeInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT tubecd, tubenmd, tubevol, tubeunit, tubeifcd, usdt, uedt"
                sSql += "  FROM lf040m"
                sSql += " WHERE uedt >= fn_ack_sysdate"
                sSql += " ORDER BY tubecd"
            ElseIf riMode = 1 Then
                sSql += "SELECT tubecd, tubenmd, tubevol, tubeunit, tubeifcd, usdt, uedt,"
                sSql += "       CASE WHEN TO_DATE(uedt, 'yyyymmddhh24miss') - SYSDATE < 0 THEN -1 ELSE 0 END diffday"
                sSql += "  FROM lf040m"
                sSql += " ORDER BY tubecd"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetTubeInfo(ByVal rsTubeCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetTubeInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT tubecd, tubenm, tubenms, tubenmd, tubenmp, tubenmbp, tubevol, tubeunit, tubeifcd,"
            sSql += "       fn_ack_date_str(usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
            sSql += "       fn_ack_date_str(uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid,"
            sSql += "       fn_ack_get_usr_name(regid) regnm"
            sSql += "  FROM lf040m"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"

            alParm.Add(New OracleParameter("tubecd",  OracleDbType.Varchar2, rsTubeCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTubeCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetTubeInfo_img(ByVal rsTubeCd As String) As Byte()
        Dim sFn As String = "Public Shared Function GetTubeInfo_img(String) As Byte()"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            With dbCmd
                .Connection = dbCn
            End With

            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT filelen, filebin"
            sSql += "  FROM lf041m"
            sSql += " WHERE tubecd = :tubecd"

            With dbCmd

                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()

                .Parameters.Clear()
                .Parameters.Add("tubecd",  OracleDbType.Varchar2, rsTubeCd.Length).Value = rsTubeCd

            End With

            Dim a_btReturn() As Byte

            Dim dbDr As oracleDataReader = dbCmd.ExecuteReader(CommandBehavior.SequentialAccess)

            Do While dbDr.Read()

                Dim iStartIndex As Integer = 0
                Dim lngReturn As Long = 0

                Dim iBufferSize As Integer = 0

                iBufferSize = Convert.ToInt32(dbDr.GetValue(0).ToString)

                Dim a_btBuffer(iBufferSize - 1) As Byte
                ReDim a_btBuffer(iBufferSize - 1)

                iStartIndex = 0
                lngReturn = dbDr.GetBytes(1, iStartIndex, a_btBuffer, 0, iBufferSize)

                Do While lngReturn = iBufferSize
                    fnCopyToBytes(a_btBuffer, a_btReturn)


                    ReDim a_btBuffer(iBufferSize - 1)

                    iStartIndex += iBufferSize
                    lngReturn = dbDr.GetBytes(1, iStartIndex, a_btReturn, 0, iBufferSize)
                Loop
            Loop

            dbDr.Close()

            Return a_btReturn

        Catch ex As Exception
            Fn.Log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing
        End Try
    End Function

    Private Shared Function fnCopyToBytes(ByVal r_a_btFrom As Byte(), ByRef r_a_btTo As Byte()) As Boolean
        Dim sFn As String = "Private Shared Function fnCopyToBytes(Byte(), ByRef Byte()) As Boolean"

        Try
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
        Catch ex As Exception
            Fn.Log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try

    End Function

    Public Function GetRecentTubeInfo(ByVal rsTubeCd As String, ByVal rsUsDt As String) As DataTable
        Dim sFn As String = "Public Function GetRecentTubeInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT usdt"
            sSql += "  FROM (SELECT usdt"
            sSql += "          FROM lf040m"
            sSql += "         WHERE tubecd = :tubecd"
            sSql += "           AND usdt  >= :usdt"
            sSql += "         ORDER BY usdt DESC"
            sSql += "       ) a"
            sSql += " WHERE ROWNUM = 1"

            alParm.Add(New OracleParameter("tubecd",  OracleDbType.Varchar2, rsTubeCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTubeCd))
            alParm.Add(New OracleParameter("usdt",  OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransTubeInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsTubeCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = "Public Function TransTubeInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'lf040m : 용기마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        'UPDATE uedt of previous record
                        sSql = ""
                        sSql += "UPDATE lf040m SET uedt = :usdt"
                        sSql += " WHERE (tubecd, usdt) IN"
                        sSql += "       (SELECT a.tubecd, a.usdt"
                        sSql += "          FROM (SELECT tubecd, usdt"
                        sSql += "				   FROM lf040m"
                        sSql += "				  WHERE tubecd = :tubecd"
                        sSql += "					AND usdt   < :usdt"
                        sSql += "					AND uedt   > :usdt"
                        sSql += "		          ORDER BY usdt DESC"
                        sSql += "               ) a"
                        sSql += "         WHERE ROWNUM = 1"
                        sSql += " 		)"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO lf040m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'lf040h Backup
                        sSql = ""
                        sSql += "INSERT INTO lf040h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf040m f"
                        sSql += " WHERE tubecd = :tubecd"
                        sSql += "   AND usdt   = :usdt"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
                        dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "TUBECD", "USDT"

                                    Case Else
                                        sFields += sField + " = :" + sField + ","
                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sSql = ""
                            sSql += "UPDATE lf040m SET " + sFields
                            sSql += " WHERE tubecd = :tubecd"
                            sSql += "   AND usdt   = :usdt"

                            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
                            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTubeInfo_DEL(ByVal rsTubeCd As String, ByVal rsUsDt As String) As Boolean
        Dim sFn As String = " Public Function TransTestInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'lf040m : 용기 마스터
            '   lf040h Insert
            sSql = ""
            sSql += "INSERT INTO lf040h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf040m f"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf040m Delete
            sSql = ""
            sSql += "DELETE lf040m"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTubeInfo_UE(ByVal rsTubeCd As String, ByVal rsUsDt As String, ByVal rsUeDt As String) As Boolean
        Dim sFn As String = "Public Function TransTubeInfo_UE(String, String) As Boolean"

        Dim dbCn As New OracleConnection
        Dim dbTran As OracleTransaction
        Dim dbCmd As New OracleCommand

        Try
            Dim sMsg As String = ""

            sMsg = ifExistOtherUsableData("lf040", "TUBECD", rsTubeCd, rsUsDt)

            If IsNothing(sMsg) Then
                MsgBox("쿼리문의 오류가 있습니다!!", MsgBoxStyle.Exclamation)
                Exit Function
            End If

            If Not sMsg = "" Then
                MsgBox(sMsg, MsgBoxStyle.Critical)
                Exit Function
            End If

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            dbCn = GetDbConnection()
            dbTran = dbCn.BeginTransaction()

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'lf040m : 용기 마스터
            '   lf040h Insert
            sSql = ""
            sSql += "INSERT INTO lf040h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf040m f"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf040m Update
            sSql = ""
            sSql = "UPDATE lf040m SET uedt = :uedt, regdt = fn_ack_sysdate, regid = :regid"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDt
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function


    Public Function TransTubeInfo_UPD_UE(ByVal rsTubeCd As String, ByVal rsUsDt As String, ByVal rsUeDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransTestInfo_UPD_UE() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            '   lf040h Insert
            sSql = ""
            sSql += "INSERT INTO lf040h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf040m f"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf040m Update
            sSql = ""
            sSql += "UPDATE lf040m SET"
            sSql += "       uedt   = :uedt,"
            sSql += "       regdt  = fn_ack_sysdate,"
            sSql += "       regid  = :regid"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("uedt",  OracleDbType.Varchar2).Value = rsUeDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTubeInfo_UPD_US(ByVal rsTubeCd As String, ByVal rsUsDt As String, ByVal rsUsDtNew As String) As Boolean
        Dim sFn As String = " Public Function TransTestInfo_UPD_US() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'lf040m : 성분제제 마스터
            '   lf040h Insert
            sSql = ""
            sSql += "INSERT INTO lf040h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf040m f"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   lf040m Update
            sSql = ""
            sSql += "UPDATE lf040m SET"
            sSql += "       usdt   = :usdtchg,"
            sSql += "       regdt  = fn_ack_sysdate,"
            sSql += "       regid  = :regid"
            sSql += " WHERE tubecd = :tubecd"
            sSql += "   AND usdt   = :usdt"


            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usdtchg",  OracleDbType.Varchar2).Value = rsUsDtNew
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("tubecd",  OracleDbType.Varchar2).Value = rsTubeCd
            dbCmd.Parameters.Add("usdt",  OracleDbType.Varchar2).Value = rsUsDt

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransTubeInfo_Img(ByVal rsTubeCd As String, ByVal r_btFile As Byte()) As Boolean
        Dim sFn As String = "Public Function TransTubeInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "UPDATE lf041m SET filelen = :filelen"
            sSql += " WHERE tubecd = :tubecd"


            With dbCmd
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("filelen", OracleDbType.Int64).Value = r_btFile.Length
                .Parameters.Add("tubecd",  OracleDbType.Varchar2, rsTubeCd.Length).Value = rsTubeCd

                iRet = .ExecuteNonQuery()
            End With

            If iRet < 1 Then
                sSql = ""
                sSql += " INSERT INTO lf041m (  tubecd,  filelen )"
                sSql += "             VALUES ( :tubecd, :filelen )"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("tubecd",  OracleDbType.Varchar2, rsTubeCd.Length).Value = rsTubeCd
                    .Parameters.Add("filelen", OracleDbType.Int64).Value = r_btFile.Length

                    iRet += .ExecuteNonQuery()
                End With

            End If

            sSql = ""
            sSql += "UPDATE lf041m SET filebin = :filebin"
            sSql += " WHERE tubecd = :tubecd"

            With dbCmd
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("filebin", OracleDbType.LongRaw, r_btFile.Length).Value = r_btFile
                .Parameters.Add("tubecd",  OracleDbType.Varchar2, rsTubeCd.Length).Value = rsTubeCd

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_USR
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP.vb, Class : RISAPP.APP_F_USR" + vbTab

    Public Overloads Function GetUsrInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetUsrInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT usrid, usrnm,"
                sSql += "       CASE WHEN usrlvl = 'S' THEN '관리자' "
                sSql += "            ELSE CASE WHEN drspyn = '1' THEN '전문의'"
                sSql += "                      WHEN drspyn = '0' THEN '일반'"
                sSql += "                 END"
                sSql += "       END usrlvl, "
                sSql += "       CASE WHEN DELFLG='0' THEN 'Y'WHEN DELFLG='1' THEN 'N' END AS USEYN"
                sSql += "  FROM rf090m"
                sSql += " ORDER by usrid"
            ElseIf riMode = 1 Then
                sSql += "SELECT usrid, usrnm,"
                sSql += "       CASE WHEN usrlvl = 'S' THEN '관리자' "
                sSql += "            ELSE CASE WHEN drspyn = '1' THEN '전문의'"
                sSql += "                      WHEN drspyn = '0' THEN '일반'"
                sSql += "                 END"
                sSql += "       END usrlvl, CASE WHEN delflg = '1' THEN '사용종료' ELSE '' END delflg_v, delflg, "
                sSql += "       CASE WHEN DELFLG='0' THEN 'Y'WHEN DELFLG='1' THEN 'N' END AS USEYN"
                sSql += "  FROM rf090m"
                sSql += " ORDER by usrid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetUsrInfo(ByVal rsUsrID As String) As DataTable
        Dim sFn As String = "Public Function GetUsrInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT usrid, usrnm, usrpwd, '********************' usrpwd_vw,"
            sSql += "       '[' || usrlvl || ']' usrlvl_01, medino, drspyn, other, drspyn, medino,"
            sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid,"
            sSql += "       fn_ack_get_usr_name(regid) regnm,"
            sSql += "       fn_ack_get_usr_telno(usrid) telno,"
            sSql += "       delflg"
            sSql += "  FROM rf090m"
            sSql += " WHERE usrid = :usrid"

            alParm.Add(New OracleParameter("usrid",  OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetUsrMnuInfo(ByVal rsUsrID As String) As DataTable
        Dim sFn As String = "Public Function GetUsrMnuInfo(ByVal asUsrID As String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT NVL(f91.chk, '0') chk, f92.mnuid, f92.isparent, f92.mnulvl, f92.parentid,"
            sSql += "       LPAD('-', f92.mnulvl*4, '-') || mnunm mnunm,"
            sSql += "       CASE WHEN ISPARENT = '1' THEN f92.mnuid ELSE f92.parentid END sort1"
            sSql += "  FROM rf092m f92 LEFT OUTER JOIN"
            sSql += "       (SELECT '1' chk, f92.mnuid "
            sSql += "		   FROM rf092m f92, rf091m f91"
            sSql += "		  WHERE f92.mnuid = f91.mnuid"
            sSql += "		    AND f92.mnugbn in ('1', '9')"
            sSql += " 		    AND f91.usrid = :usrid"
            sSql += "       ) f91 ON (f92.mnuid = f91.mnuid)"
            sSql += " WHERE f92.mnugbn in ('1', '9')"
            sSql += " ORDER BY sort1, f92.vieworder, f92.mnuid"

            alParm.Add(New OracleParameter("usrid",  OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetUsrSkillInfo(ByVal rsUsrID As String) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT NVL(f93.chk, '0') chk, f94.sklgrp, f94.sklcd, f94.skldesc"
            sSql += "  FROM rf094m f94 LEFT OUTER JOIN"
            sSql += "       (SELECT '1' chk, f94.sklgrp, f94.sklcd"
            sSql += "  		   FROM rf094m f94, rf093m f93"
            sSql += "		  WHERE f94.sklgrp = f93.sklgrp"
            sSql += " 		    AND f94.sklcd  = f93.sklcd"
            sSql += " 		    AND f93.usrid  = :usrid"
            sSql += "       ) f93 ON (f94.sklgrp = f93.sklgrp AND f94.sklcd = f93.sklcd)"
            sSql += " WHERE f94.sklflg  = '1'"
            sSql += "   AND f94.sklgrp <> '000'"
            sSql += " ORDER BY dispseq, f94.sklgrp, f94.sklcd"

            alParm.Add(New OracleParameter("usrid",  OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentUsrInfo(ByVal rsUsrID As String) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT usrid"
            sSql += "  FROM rf090m"
            sSql += " WHERE usrid = :usrid"

            alParm.Add(New OracleParameter("usrid",  OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransUsrInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, _
                                 ByVal ro_Tcol2 As ItemTableCollection, ByVal riType2 As Integer, _
                                 ByVal ro_Tcol3 As ItemTableCollection, ByVal riType3 As Integer, _
                                 ByVal ro_Tcol4 As ItemTableCollection, ByVal riType4 As Integer, _
                                 ByVal rsUsrID As String) As Boolean
        Dim sFn As String = "Public Function TransUsrInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""
            Dim alTest As New ArrayList

            'rf090M : 사용자마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""
                            dbCmd.Parameters.Clear()

                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf090m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf090H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf090h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf090m f"
                        sSql += " WHERE usrid = :usrid"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrID

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value

                                Select Case sField.ToUpper
                                    Case "USRID"

                                    Case Else
                                        sFields += sField + " = :" + sField + ","

                                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End Select
                            Next

                            'UPDATE record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sSql = ""
                            sSql += "UPDATE rf090m SET " + sFields
                            sSql += " WHERE usrid = :usrid"

                            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrID
                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

            'rf091M : 사용자별 메뉴 마스터
            Select Case riType2
                Case 0      '----- 신규
                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            Dim sTmp As String = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                sTmp += sValue + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf091m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    'rf091H Backup
                    sSql = ""
                    sSql += "INSERT INTO rf091h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf091m f"
                    sSql += " WHERE usrid = :usrid"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrID

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rf091m"
                    sSql += " WHERE usrid = :usrid"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrID

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    With ro_Tcol2
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf091m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

            'rf093M : 사용자별 기능 마스터
            Select Case riType3
                Case 0      '----- 신규
                    With ro_Tcol3
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf093m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    'rf093H Backup
                    sSql = ""
                    sSql += "INSERT INTO rf093h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf093m f"
                    sSql += " WHERE usrid = :usrid"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrID

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rf093m"
                    sSql += " WHERE usrid = :usrid"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrID

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    With ro_Tcol3
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf093m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

            'rf097M : 연락처 등록
            Select Case riType4
                Case 0      '----- 신규
                    With ro_Tcol4
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf097m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    'rf093H Backup
                    sSql = ""
                    sSql += "INSERT INTO rf097h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf097m f"
                    sSql += " WHERE usrid  = :usrid"
                    sSql += "   AND fldgbn = '1'"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrID

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE rf097m"
                    sSql += " WHERE usrid  = :usrid"
                    sSql += "   AND fldgbn = '1'"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrID

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    With ro_Tcol4
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf097m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransUsrInfo_DEL(ByVal rsUsrId As String) As Boolean
        Dim sFn As String = " Public Function TransBcclsInfo_DEL() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "INSERT INTO rf090h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf090m f"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf090m"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "INSERT INTO rf091h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf091m f"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf091m"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "INSERT INTO rf093h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf093m f"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf093m"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransUsrInfo_UE(ByVal rsUsrId As String) As Boolean
        Dim sFn As String = "Public Function TransUsrInfo_UE(String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            '   rf090H Insert
            sSql = ""
            sSql += "INSERT INTO rf090h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf090m f"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf090M Update
            sSql = ""
            sSql += "UPDATE rf090m SET delflg = '1', regdt = fn_ack_sysdate, regid = :regid"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf091H Insert
            sSql = ""
            sSql += "INSERT INTO rf091h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf091m f"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf091M Delete
            sSql = ""
            sSql += "DELETE rf091m"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf093H Insert
            sSql = ""
            sSql += "INSERT INTO rf093h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf093m f"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf093M Delete
            sSql = ""
            sSql += "DELETE rf093m"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            If iRet > 0 Then
                dbTran.Commit()
                Return True
            Else
                dbTran.Rollback()
                Return False
            End If

            Return True
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
End Class

Public Class APP_F_WKGRP
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : RISAPP.APP_F_WKGRP" & vbTab

    '-- 검사코드 리스트
    Public Shared Function fnGet_TestInfo(ByVal rsWGrpCd As String) As DataTable
        Dim sFn As String = "Function fnGet_TestInfo(String) As DataTable"
        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       '' chk, f6.tnmd, f6.tnmp tnmp, f6.testcd, f6.spccd, RPAD(f6.testcd, 8, ' ') || f6.spccd testspc,"
            sSql += "       f6.tordcd, f6.tordslip, f6.tcdgbn, f6.partcd || f6.slipcd partslip, NVL(f6.titleyn, '0') titleyn,"
            sSql += "       NVL(f6.mbttype, '0') mbttype, NVL(f6.poctyn, '0') poctyn,"
            sSql += "       f3.spcnmd, NVL(f2.dispseq, 999) sort1, NVL(f6.dispseql, 999) sort2"
            sSql += "  FROM rf060m f6, rf021m f2, lf030m f3"
            sSql += " WHERE f6.usdt <= fn_ack_sysdate"
            sSql += "   AND f6.uedt >  fn_ack_sysdate"
            sSql += "   AND (f6.testcd, f6.spccd) NOT IN (SELECT testcd, spccd FROM rf066m WHERE wkgrpcd <> '" + rsWGrpCD + "')"
            sSql += "   AND f6.partcd = f2.partcd"
            sSql += "   AND f6.slipcd = f2.slipcd"
            sSql += "   AND f2.usdt  <= fn_ack_sysdate"
            sSql += "   AND f2.uedt   > fn_ack_sysdate"
            sSql += "   AND f6.spccd  = f3.spccd"
            sSql += "   AND f3.usdt  <= fn_ack_sysdate"
            sSql += "   AND f3.uedt   > fn_ack_sysdate"
            sSql += " ORDER BY sort1, sort2, testspc"

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try

    End Function


    Public Function fnGet_TestInfo_spc(ByVal rsSlipCd As String, ByVal rsSpcCds As String, ByVal rsWGrpCd As String) As DataTable
        Dim sFn As String = "Public Function fnGet_TestInfo_spc(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT testcd, spccd, tnmd, partcd || slipcd slipcd, dispseql dispseql"
            sSql += "  FROM rf060m"
            sSql += " WHERE usdt   <= fn_ack_sysdate"
            sSql += "   AND uedt   >  fn_ack_sysdate"
            sSql += "   AND partcd  = :partcd"
            sSql += "   AND slipcd  = :slipcd"
            sSql += "   AND spccd  IN ('" + rsSpcCds.Replace(",", "','").ToString + "')"
            sSql += "   AND tcdgbn IN ('S', 'P')"
            sSql += "   AND (testcd, spccd) NOT IN (SELECT testcd, spccd FROM rf066m WHERE wkgrpcd <> '" + rsWGrpCd + "')"
            sSql += " ORDER BY dispseql"

            al.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
            al.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


        End Try
    End Function

    Public Function GetWGrpInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql = ""
                sSql += "SELECT wkgrpcd, wkgrpnmd"
                sSql += "  FROM rf066m"
                sSql += " GROUP BY wkgrpcd, wkgrpnmd"

            ElseIf riMode = 1 Then
                sSql = ""
                sSql += "SELECT wkgrpcd, wkgrpnmd, '1' diffday, '' modid, '' moddt"
                sSql += "  FROM rf066m"
                sSql += " GROUP BY wkgrpcd, wkgrpnmd"
                sSql += " UNION ALL "
                sSql += "SELECT wkgrpcd, wkgrpnmd, '-1' diffday, modid, fn_ack_date_str(moddt, 'yyyy-mm-dd hh24:mi:ss') moddt"
                sSql += "  FROM rf066h"
                sSql += "  GROUP BY wkgrpcd, wkgrpnmd, modid, moddt"
                sSql += "  ORDER BY wkgrpcd, moddt, modid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetWGrpInfo(ByVal rsWGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo( String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       f64.wkgrpcd, f64.wkgrpnm, f64.wkgrpnms, f64.wkgrpnmd, f64.wkgrpnmbp,"
            sSql += "       '[' || f21.partcd || f21.slipcd || '] ' ||  f21.slipnmd tgrptype_01,"
            sSql += "       CASE WHEN f64.wkgrpgbn = '1' THEN '[1] 일' WHEN f64.wkgrpgbn = '2' THEN '[2] 월' WHEN f64.wkgrpgbn = '3' THEN '[3] 년' END wkgrpgbn_01,"
            sSql += "       fn_ack_date_str(f64.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f64.regid,"
            sSql += "       fn_ack_get_usr_name(f64.regid) regnm,"
            sSql += "       NULL moddt, NULL modid, NULL modnm"
            sSql += "  FROM rf066m f64 LEFT OUTER JOIN"
            sSql += "       rf021m f21 ON (f64.partcd = f21.partcd AND f64.slipcd = f21.slipcd)"
            sSql += " WHERE f64.wkgrpcd = :wgrpcd"

            alParm.Add(New OracleParameter("wgrpcd",  OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetWGrpInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsWGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       f64.wkgrpcd, f64.wkgrpnm, f64.wkgrpnms, f64.wkgrpnmd, f64.wkgrpnmbp,"
            sSql += "       '[' || f21.partcd || f21.slipcd || '] ' ||  f21.slipnmd tgrptype_01,"
            sSql += "       CASE WHEN f64.wkgrpgbn = '1' THEN '[1] 일' WHEN f64.wkgrpgbn = '2' THEN '[2] 월' WHEN f64.wkgrpgbn = '3' THEN '[3] 년' END wkgrpgbn_01,"
            sSql += "       fn_ack_date_str(f64.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f64.regid,"
            sSql += "       fn_ack_get_usr_name(f64.regid) regnm, "
            sSql += "       fn_ack_date_str(f64.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, modid,"
            sSql += "       fn_ack_get_usr_name(modid) modnm"
            sSql += "  FROM rf066h f64 LEFT OUTER JOIN"
            sSql += "       rf021m f21 ON (f64.partcd = f21.partcd AND f64.slipcd = f21.slipcd)"
            sSql += " WHERE f64.wkgrpcd = :wgrpcd"
            sSql += "   AND f64.moddt   = :moddt"
            sSql += "   AND f64.modid   = :modid"

            alParm.Add(New OracleParameter("wgrpcd",  OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDT.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDT))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetWGrpInfo_Test(ByVal rsWGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo_Test(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT a.testcd, a.spccd, b.tnmd, a.partcd || a.slipcd partslip"
            sSql += "  FROM rf066m a LEFT OUTER JOIN"
            sSql += "       ("
            sSql += "        SELECT testcd, spccd, tnmd"
            sSql += "          FROM rf060m b"
            sSql += "         WHERE b.usdt <= fn_ack_sysdate"
            sSql += "           AND b.uedt >  fn_ack_sysdate"
            sSql += "           AND b.tcdgbn IN ('B', 'S', 'P')"
            sSql += "       ) b ON (a.testcd = b.testcd AND a.spccd = b.spccd)"
            sSql += " WHERE a.wkgrpcd = :wgrpcd"

            alParm.Add(New OracleParameter("wgrpcd",  OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetWGrpInfo_Test(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsWGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetTGrpInfo_Test(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT a.testcd, a.spccd, b.tnmd, a.partcd || a.slipcd partslip"
            sSql += "  FROM rf066h a LEFT OUTER JOIN"
            sSql += "       ("
            sSql += "        SELECT testcd, spccd, tnmd"
            sSql += "          FROM rf060m b"
            sSql += "         WHERE b.usdt <= fn_ack_sysdate"
            sSql += "           AND b.uedt >  fn_ack_sysdate"
            sSql += "           AND b.tcdgbn IN ('B', 'S', 'P')"
            sSql += "       ) b ON (a.testcd = b.testcd AND a.spccd = b.spccd)"
            sSql += " WHERE a.wkgrpcd = :wgrpcd"
            sSql += "   AND a.moddt   = :moddt"
            sSql += "   AND a.modid   = :modid"

            alParm.Add(New OracleParameter("wgrpcd",  OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModID))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function GetRecentWGrpInfo(ByVal rsWGrpCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentTGrpInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT wkgrpcd"
            sSql += "  FROM rf066m"
            sSql += " WHERE wkgrpcd = :wgrpcd"

            alParm.Add(New OracleParameter("wgrpcd",  OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransWGrpInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsWGrpCd As String) As Boolean
        Dim sFn As String = "Public Function TransTGrpInfo(ItemTableCollection, Integer, String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf065M : 검사그룹 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf066m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf065H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf066h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf066m f"
                        sSql += " WHERE wkgrpcd = :wgrpcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("wgrpcd",  OracleDbType.Varchar2).Value = rsWGrpCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf066m"
                        sSql += " WHERE wkgrpcd = :wgrpcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("wgrpcd",  OracleDbType.Varchar2).Value = rsWGrpCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf066m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransWGrpInfo_UE(ByVal rsWGrpCd As String) As Boolean
        Dim sFn As String = "Public Function TransTGrpInfo_UE(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf066M : 작업그룹 마스터
            '   rf066H Insert
            sSql = ""
            sSql += "INSERT INTO rf066h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf066m f"
            sSql += " WHERE Wkgrpcd = :wgrpcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("wgrpcd",  OracleDbType.Varchar2).Value = rsWGrpCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf066M Delete
            sSql = ""
            sSql += "DELETE rf066m"
            sSql += " WHERE wkgrpcd = :wgrpcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("wgrpcd",  OracleDbType.Varchar2).Value = rsWGrpCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing


            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function
End Class

Public Class APP_F_DCOMCD
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : DA01.APP_F_DCOMCD" & vbTab

    Public Function TransDcomCdInfo_UE(ByVal rsSlipCd As String) As Boolean
        Dim sFn As String = " Public Function TransKSRackInfo_UE(string) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            'rf430H Backup
            sSql = ""
            sSql += "INSERT INTO rf430h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf430m f"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsSlipCd.Substring(0, 1)
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd.Substring(1, 1)

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf430m"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsSlipCd.Substring(0, 1)
            dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd.Substring(1, 1)

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransDcomCdInfo(ByVal roTcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsSlipCd As String) As Boolean
        Dim sFn As String = "Public Function TransDcomCdInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf430M : 성분제제 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With roTcol1
                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf430m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next
                    End With

                Case 1      '----- 수정
                    With roTcol1
                        'rf430H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf430h "
                        sSql += "SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf430m f"
                        sSql += " WHERE partcd = :partcd"
                        sSql += "   AND slipcd = :slipcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsSlipCd.Substring(0, 1)
                        dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd.Substring(1, 1)

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE rf430m"
                        sSql += " WHERE partcd = :partcd"
                        sSql += "   AND slipcd = :slipcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("partcd",  OracleDbType.Varchar2).Value = rsSlipCd.Substring(0, 1)
                        dbCmd.Parameters.Add("slipcd",  OracleDbType.Varchar2).Value = rsSlipCd.Substring(1, 1)

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        For i As Integer = 1 To .ItemTableRowCount
                            sField = "" : sFields = "" : sValue = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                            Next

                            'insert new record
                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)
                            sSql = "INSERT INTO rf430m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()
                        Next

                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function GetDcomCdInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetDcomCdInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT slipcd, slipnmd "
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT"
                sSql += "               f4.partcd || f4.slipcd slipcd, CASE WHEN NVL(f2.slipnmd, ' ') = ' ' THEN '공통' ELSE f2.slipnmd END slipnmd"
                sSql += "          FROM rf430m f4 LEFT OUTER JOIN"
                sSql += "               rf021m f2 ON (f4.partcd = f2.partcd AND f4.slipcd = f2.slipcd)"
                sSql += "       ) a"

            ElseIf riMode = 1 Then
                sSql += "SELECT slipcd, slipnmd, diffday, moddt, modid"
                sSql += "  FROM ( "
                sSql += "        SELECT DISTINCT f4.partcd || f4.slipcd slipcd, CASE WHEN NVL(f2.slipnmd, ' ') = ' ' THEN '공통' ELSE f2.slipnmd END slipnmd,"
                sSql += "               NULL diffday, NULL moddt, NULL modid"
                sSql += "          FROM rf430m f4 LEFT OUTER JOIN"
                sSql += "               rf021M f2 ON (f4.partcd = f2.partcd AND f4.slipcd = f2.slipcd)"
                sSql += "         UNION ALL"
                sSql += "        SELECT DISTINCT f4.partcd || f4.slipcd slipcd, CASE WHEN NVL(f2.slipnmd, ' ') = ' ' THEN '공통' ELSE f2.slipnmd END slipnmd,"
                sSql += "               -1 diffday, fn_ack_date_str(f4.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f4.modid"
                sSql += "          FROM rf430h f4 LEFT OUTER JOIN"
                sSql += "               rf021m f2 ON (f4.partcd = f2.partcd AND f4.slipcd = f2.slipcd)"
                sSql += "       ) a"
                sSql += " ORDER BY slipcd, moddt, modid"

            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetDcomCdInfo(ByVal rsSlipCd As String, Optional ByVal rsAdd As String = "") As DataTable
        Dim sFn As String = "Public Function GetDcomCdInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim sTmp As String = ""

            If rsSlipCd.Length > 0 Then
                sTmp = "( '" + rsSlipCd + "'" ' )"
            End If

            If rsAdd <> "" Then
                If sTmp.Length > 0 Then sTmp += ","

                sTmp += "'00'"
            End If

            sTmp += ")"


            sSql += "SELECT DISTINCT"
            sSql += "       a.drugcomcd, a.drugcomnm, fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
            sSql += "       NULL moddt, NULL modid, NULL modnm, fn_ack_get_usr_name(a.regid) regnm,"
            sSql += "       '[' || a.partcd || a.slipcd || '] ' ||  b.slipnmd slipnmd_01"
            sSql += "  FROM rf430m a LEFT OUTER JOIN"
            sSql += "       rf021m b ON (a.partcd = b.partcd AND a.slipcd = b.slipcd)"
            sSql += " WHERE a.partcd || a.slipcd IN " + sTmp

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetDcomCdInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsSlipCd As String) As DataTable
        Dim sFn As String = "Public Function GetDcomCdInfo(string, string, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       a.drugcomcd, a.drugcomnm, fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, a.regid,"
            sSql += "       a.moddt, a.modid,"
            sSql += "       '[' || a.partcd || a.slipcd || '] ' ||  CASE WHEN a.partcd + a.slipcd = '00' THEN '공통' ELSE b.slipnmd END slipnmd_01,"
            sSql += "       fn_ack_get_usr_name(a.modid) modnm,"
            sSql += "       fn_ack_get_usr_name(a.regid) regnm"
            sSql += "  FROM rf430h a LEFT OUTER JOIN"
            sSql += "       rf021m b ON (a.partcd = b.partcd AND a.slipcd = b.slipcd)"
            sSql += " WHERE a.moddt  = :moddt"
            sSql += "   AND a.modid  = :modid"
            sSql += "   AND a.partcd = :partcd"
            sSql += "   AND a.slipcd = :slipcd"

            al.Add(New OracleParameter("moddt", rsModDt))
            al.Add(New OracleParameter("modid", rsModId))
            al.Add(New oracleParameter("partcd", rsSlipCd.Substring(0, 1)))
            al.Add(New oracleParameter("slipcd", rsSlipCd.Substring(1, 1)))


            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Function GetSangBunInfo() As DataTable
        Dim sFn As String = "Public Function GetSangBunInfo() As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT DISTINCT"
            sSql += "       '' sungbun_code, igrdname sungbun_name"
            sSql += "  FROM oram1.mdordrtc a "
            sSql += " WHERE a.appldate <= SYSDATE"
            sSql += "   AND a.enddate  >= SYSDATE"
            sSql += " ORDER BY a.igrdname"

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function
End Class

Public Class APP_F_SPTEST
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : DA01.APP_F_SPTEST" & vbTab

    Public Function GetSpTestInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Function GetSpTestInfo(ByVal iMode As Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT testcd, tnmd tnmd_01 "
                sSql += "  FROM ("
                sSql += "        SELECT f31.testcd, MIN(f60.tnmd) tnmd"
                sSql += "          FROM rf310m f31, rf060m f60"
                sSql += "         WHERE f31.testcd = f60.testcd"
                sSql += "           AND f60.usdt  <= fn_ack_sysdate"
                sSql += "           AND f60.uedt   > fn_ack_sysdate"
                sSql += "         GROUP BY f31.testcd"
                sSql += "       ) a"
                sSql += " ORDER BY testcd"

            ElseIf riMode = 1 Then
                sSql += "SELECT testcd, tnmd tnmd_01, diffday, moddt, modid"
                sSql += "  FROM ("
                sSql += "        SELECT f31.testcd, MIN(f60.tnmd) tnmd,"
                sSql += "               null diffday, null moddt, null modid"
                sSql += "          FROM rf310m f31, rf060m f60"
                sSql += "         WHERE f31.testcd = f60.testcd"
                sSql += "           AND f60.usdt  <= fn_ack_sysdate"
                sSql += "           AND f60.uedt  >  fn_ack_sysdate"
                sSql += "         GROUP BY f31.testcd"
                sSql += "         UNION ALL "
                sSql += "         SELECT f31.testcd, MIN(f60.tnmd) tnmd,"
                sSql += "                -1 diffday, fn_ack_date_str(f31.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f31.modid modid"
                sSql += "           FROM rf310h f31, rf060m f60"
                sSql += "          WHERE f31.testcd = f60.testcd"
                sSql += "            AND f60.usdt  <= f31.moddt"
                sSql += "            AND f60.uedt  >  f31.moddt"
                sSql += "          GROUP BY f31.testcd, f31.moddt, f31.modid"
                sSql += "        ) a"
                sSql += " ORDER BY testcd, moddt, modid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetSpTestInfo(ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetSpTestInfo(String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT f31.testcd, f31.stsubseq, f60.tnmd, f31.strsttxtr, f31.strsttxtm, f31.strsttxtf,"
            sSql += "       f31.stsubcnt, f31.stsubnm, f31.stsubtype, f31.imgtype, f31.imgsizew, f31.imgsizeh,"
            sSql += "       f31.stsubrtf, f31.stsubexprg, f31.stsubfirst,"
            sSql += "       fn_ack_date_str(f31.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f31.regid,"
            sSql += "       fn_ack_get_usr_name(f31.regid) regnm, null modnm "
            sSql += "  FROM (SELECT f31.testcd, MIN(f60.tnmd) tnmd"
            sSql += "          FROM rf310m f31, rf060m f60"
            sSql += " 		  WHERE f31.testcd = f60.testcd"
            sSql += "		    AND f60.usdt  <= fn_ack_sysdate"
            sSql += "		    AND f60.uedt  >  fn_ack_sysdate"
            sSql += " 		    AND f31.testcd = :testcd"
            sSql += " 		  GROUP BY f31.testcd"
            sSql += "       ) f60,"
            sSql += " 	    rf310m f31"
            sSql += " WHERE f31.testcd = f60.testcd"
            sSql += " ORDER BY f31.testcd, f31.stsubseq"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
           Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetSpTestInfo(ByVal rsModDt As String, ByVal rsModId As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetSpTestInfo(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT f31.testcd, f31.stsubseq, f60.tnmd, f31.strsttxtr, f31.strsttxtm, f31.strsttxtf,"
            sSql += "       f31.stsubcnt, f31.stsubnm, f31.stsubtype, f31.imgtype, f31.imgsizew, f31.imgsizeh,"
            sSql += "       f31.stsubrtf, f31.stsubexprg, f31.stsubfirst,"
            sSql += "       fn_ack_date_str(f31.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f31.regid,"
            sSql += "       fn_ack_get_usr_name(f31.regid) regnm, null modnm "
            sSql += "  FROM (SELECT f31.testcd, MIN(f60.tnmd) tnmd"
            sSql += "          FROM rf310h f31, rf060m f60"
            sSql += " 		  WHERE f31.testcd = f60.testcd"
            sSql += "		    AND f60.usdt  <= fn_ack_sysdate"
            sSql += "		    AND f60.uedt  >  fn_ack_sysdate"
            sSql += " 		    AND f31.testcd = :testcd"
            sSql += " 		  GROUP BY f31.testcd"
            sSql += "       ) f60,"
            sSql += " 	    rf310h f31"
            sSql += " WHERE f31.testcd = f60.testcd"
            sSql += "   AND f31.moddt  = :moddt"
            sSql += "   AND f31.modid  = :modid"
            sSql += " ORDER BY f31.testcd, f31.stsubseq"

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            alParm.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModId))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
           Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function GetRecentSpTestInfo(ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetRecentSpTestInfo(ByVal asTClsCd As String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql += "SELECT testcd"
            sSql += "  FROM rf310m"
            sSql += " WHERE testcd = :testcd"

            Dim alParm As New ArrayList

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
           Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Overloads Function GetTClsCdInfo(ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public Function GetTClsCdInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT MIN(tnmd) tnmd, MIN(tcdgbn) tcdgbn_min, MAX(tcdgbn) tcdgbn_max"
            sSql += "  FROM rf060m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND usdt  <= fn_ack_sysdate"
            sSql += "   AND uedt  >  fn_ack_sysdate"

            alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
           Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

    Public Function TransSpTestInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsTestCd As String) As Boolean
        Dim sFn As String = "Public Function TransSpTestInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Dim sSql As String = ""
        Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

        Dim iRet As Integer = 0

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1
                        'rf310M Insert
                        For i As Integer = 1 To .ItemTableRowCount
                            sFields = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                If sValue = "" Then
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = DBNull.Value
                                Else
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End If
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf310m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf310H Backup
                        sSql = ""
                        sSql += "INSERT INTO rf310h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf310m f"
                        sSql += " WHERE testcd = :testcd"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        'rf310M Delete
                        sSql = "DELETE rf310m WHERE testcd = :testcd"

                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

                        iRet += dbCmd.ExecuteNonQuery()

                        'rf310M Insert
                        For i As Integer = 1 To .ItemTableRowCount
                            sFields = "" : sValues = ""

                            dbCmd.Parameters.Clear()
                            For j As Integer = 1 To .ItemTableColCount
                                sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                sFields += sField + ","

                                sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                sValues += ":" + sField + ","

                                If sValue = "" Then
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = DBNull.Value
                                Else
                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                End If
                            Next

                            sFields = sFields.Substring(0, sFields.Length - 1)
                            sValues = sValues.Substring(0, sValues.Length - 1)

                            sSql = "INSERT INTO rf310m (" + sFields + ") VALUES (" + sValues + ")"

                            dbCmd.CommandText = sSql
                            iRet += dbCmd.ExecuteNonQuery()

                        Next
                    End With
            End Select

            If iRet > 0 Then
                dbTran.Commit()
            Else
                dbTran.Rollback()
            End If

            Return True

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

    Public Function TransSpTestInfo_UE(ByVal rsTestCd As String) As Boolean
        Dim sFn As String = "Public Function TransSpTestInfo_UE(String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Dim sSql As String = ""
        Dim iRet As Integer = 0

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            'rf310H Backup
            sSql = ""
            sSql += "INSERT INTO lf030h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM lf030m f"
            sSql += " WHERE testcd = :testcd"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            'rf310M Delete
            sSql = "DELETE rf310m WHERE testcd = :testcd"

            dbCmd.CommandText = sSql

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("testcd",  OracleDbType.Varchar2).Value = rsTestCd

            iRet += dbCmd.ExecuteNonQuery()

            If iRet > 0 Then
                dbTran.Commit()
            Else
                dbTran.Rollback()
            End If

            Return True

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
End Class

Public Class APP_F_KSRACK
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : LISAPP.APP_F_KSRACK" + vbTab

    Public Function GetKSSpcInfo(ByVal rsBcclscd As String, ByVal rsRackId As String) As DataTable
        Dim sFn As String = "Public Function GetKSSpcInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT f16.chk, f30.spccd, f30.spcnmd"
            sSql += "  FROM (SELECT spccd, spcnmd"
            sSql += "          FROM lf030m"
            sSql += "		  WHERE uedt > fn_ack_sysdate"
            sSql += "         UNION "
            sSql += "        SELECT '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "' spccd, '검체구분 없음' spcnmd FROM DUAL"
            sSql += "       ) f30 LEFT OUTER JOIN"
            sSql += "       (SELECT DISTINCT '1' chk, f16.spccd"
            sSql += " 		   FROM lf030m f03, rf160m f16"
            sSql += " 		  WHERE f03.spccd   = f16.spccd"
            sSql += " 		    AND f03.usdt   <= fn_ack_sysdate"
            sSql += " 		    AND f03.uedt   >  fn_ack_sysdate"
            sSql += "           AND f16.bcclscd = :bcclscd"
            sSql += " 		    AND f16.rackid  = :rackcd"
            sSql += "       ) f16 ON (f30.spccd = f16.spccd)"
            sSql += " ORDER BY f30.spccd"

            al.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclscd))
            al.Add(New OracleParameter("rackcd",  OracleDbType.Varchar2, rsRackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRackId))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetKSRackInfo(ByVal riMode As Integer) As DataTable
        Dim sFn As String = "Public Overloads Function GetKSRackInfo(Integer) As DataTable"

        Try
            Dim sSql As String = ""

            If riMode = 0 Then
                sSql += "SELECT bcclscd, rackid, bcclsnmd, alarmterm, maxcol, maxrow, regid, regdt"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT"
                sSql += "               f16.bcclscd, f16.rackid, f1.bcclsnmd, f16.alarmterm, f16.maxcol, f16.maxrow,"
                sSql += "               f16.regid, fn_ack_date_str(f16.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt"
                sSql += "          FROM rf160m f16 LEFT OUTER JOIN"
                sSql += "               rf010m f1  ON f16.bcclscd  = f1.bcclscd"
                sSql += "       ) a"
                sSql += " ORDER BY bcclscd, rackid"

            ElseIf riMode = 1 Then
                sSql += "SELECT bcclscd, rackid, bcclsnmd, alarmterm, maxcol, maxrow, regid, regdt, diffday, moddt, modid"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT"
                sSql += "               f16.bcclscd, f16.rackid, f1.bcclsnmd, f16.alarmterm, f16.maxcol, f16.maxrow,"
                sSql += "               f16.regid, fn_ack_date_str(f16.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, NULL diffday, NULL modid, NULL moddt"
                sSql += "          FROM rf160m f16 LEFT OUTER JOIN"
                sSql += "               rf010m f1  ON f16.bcclscd  = f1.bcclscd"
                sSql += "         UNION ALL"
                sSql += "        SELECT DISTINCT"
                sSql += "               f16.bcclscd, f16.rackid, f1.bcclsnmd, f16.alarmterm, f16.maxcol, f16.maxrow,"
                sSql += "               f16.regid, fn_ack_date_str(f16.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, -1 diffday,"
                sSql += "               f16.modid, fn_ack_date_str(f16.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt"
                sSql += "          FROM rf160h f16 LEFT OUTER JOIN"
                sSql += "               rf010m f1  ON f16.bcclscd  = f1.bcclscd"
                sSql += "       ) a"
                sSql += " ORDER BY bcclscd, rackid"
            End If

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetKSRackInfo(ByVal rsBcclscd As String, ByVal rsRackId As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetKSRackInfo(ByVal iMode As Integer, ByVal asSpcCd As String, ByVal asUSDT As String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       f16.bcclscd, f16.rackid, f16.alarmterm, f16.maxcol, f16.maxrow,"
            sSql += "       '[' || f16.bcclscd || ']' bcclsnmd_01,"
            sSql += "       fn_ack_date_str(f16.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f16.regid,"
            sSql += "       NULL modid, NULL modid,"
            sSql += "       fn_ack_get_usr_name(f16.regid) regnm"
            sSql += "  FROM rf160M f16"
            sSql += " WHERE f16.bcclscd = :bcclscd"
            sSql += "   AND f16.rackid  = :rackid"

            al.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclscd))
            al.Add(New OracleParameter("rackid",  OracleDbType.Varchar2, rsRackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRackId))


            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Overloads Function GetKSRackInfo(ByVal rsBcclscd As String, ByVal rsRackId As String, ByVal rsModDt As String, ByVal rsModId As String) As DataTable
        Dim sFn As String = "Public Overloads Function GetKSRackInfo(ByVal iMode As Integer, ByVal asSpcCd As String, ByVal asUSDT As String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim al As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       f16.bcclscd, f16.rackid, f16.alarmterm, f16.maxcol, f16.maxrow,"
            sSql += "       '[' || f16.bcclscd || ']' bcclsnmd_01,"
            sSql += "       fn_ack_date_str(f16.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, f16.regid,"
            sSql += "       fn_ack_date_str(f16.moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, f16.modid,"
            sSql += "       fn_ack_get_usr_name(f16.regid) regnm"
            sSql += "  FROM rf160h f16"
            sSql += " WHERE f16.bcclscd = :bcclscd"
            sSql += "   AND f16.rackid  = :rackid"
            sSql += "   AND f16.moddt   = :modid"
            sSql += "   AND f16.modid   = :modip"

            al.Add(New OracleParameter("bcclscd",  OracleDbType.Varchar2, rsBcclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclscd))
            al.Add(New OracleParameter("rackid",  OracleDbType.Varchar2, rsRackId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRackId))
            al.Add(New OracleParameter("moddt",  OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))
            al.Add(New OracleParameter("modid",  OracleDbType.Varchar2, rsModId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModId))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function TransKSRackInfo(ByVal ro_Tcol1 As ItemTableCollection, ByVal riType1 As Integer, ByVal rsBcclscd As String, ByVal rsRackId As String) As Boolean
        Dim sFn As String = "Public Function TransKSRackInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            'rf160M : 보관검체 마스터
            Select Case riType1
                Case 0      '----- 신규
                    With ro_Tcol1

                        With ro_Tcol1
                            For i As Integer = 1 To .ItemTableRowCount
                                sField = "" : sFields = "" : sValue = "" : sValues = ""

                                dbCmd.Parameters.Clear()
                                For j As Integer = 1 To .ItemTableColCount
                                    sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                    sFields += sField + ","

                                    sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                    sValues += ":" + sField + ","

                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                Next

                                'insert new record
                                sFields = sFields.Substring(0, sFields.Length - 1)
                                sValues = sValues.Substring(0, sValues.Length - 1)
                                sSql = "INSERT INTO rf160m (" + sFields + ") VALUES (" + sValues + ")"

                                dbCmd.CommandText = sSql
                                iRet += dbCmd.ExecuteNonQuery()
                            Next
                        End With

                    End With

                Case 1      '----- 수정
                    With ro_Tcol1
                        'rf160H Backup
                        sSql = ""
                        sSql += " INSERT INTO rf160H"
                        sSql += " SELECT fn_ack_sysdate, :modid, :modip, f16.* FROM rf160M f16"
                        sSql += "  WHERE bcclscd = :bcclscd"
                        sSql += "    AND rackid  = :rackid"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclscd
                        dbCmd.Parameters.Add("rackid",  OracleDbType.Varchar2).Value = rsRackId

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        sSql = ""
                        sSql += " DELETE FROM rf160M"
                        sSql += "  WHERE bcclscd = :bcclscd"
                        sSql += "    AND rackid  = :rackid"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclscd
                        dbCmd.Parameters.Add("rackid",  OracleDbType.Varchar2).Value = rsRackId

                        dbCmd.CommandText = sSql
                        iRet += dbCmd.ExecuteNonQuery()

                        With ro_Tcol1
                            For i As Integer = 1 To .ItemTableRowCount
                                sField = "" : sFields = "" : sValue = "" : sValues = ""

                                dbCmd.Parameters.Clear()
                                For j As Integer = 1 To .ItemTableColCount
                                    sField = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Field
                                    sFields += sField + ","

                                    sValue = CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).Value
                                    sValues += ":" + sField + ","

                                    dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (i - 1) + j), ItemTable).DbType).Value = sValue
                                Next

                                'insert new record
                                sFields = sFields.Substring(0, sFields.Length - 1)
                                sValues = sValues.Substring(0, sValues.Length - 1)
                                sSql = "INSERT INTO rf160m (" + sFields + ") VALUES (" + sValues + ")"

                                dbCmd.CommandText = sSql
                                iRet += dbCmd.ExecuteNonQuery()
                            Next
                        End With

                    End With
            End Select

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function TransKSRackInfo_UE(ByVal rsBcclscd As String, ByVal rsRackId As String) As Boolean
        Dim sFn As String = "Public Function TransKSRackInfo_UE(String, String, String) As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            '   rf160H Insert 
            sSql = ""
            sSql += "INSERT INTO rf160h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf160m f"
            sSql += "  WHERE bcclscd = :bcclscd"
            sSql += "    AND rackid  = :rackid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclscd
            dbCmd.Parameters.Add("rackid",  OracleDbType.Varchar2).Value = rsRackId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            '   rf160M Delete 
            sSql = ""
            sSql += "DELETE rf160m"
            sSql += "  WHERE bcclscd = :bcclscd"
            sSql += "    AND rackid  = :rackid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("bcclscd",  OracleDbType.Varchar2).Value = rsBcclscd
            dbCmd.Parameters.Add("rackid",  OracleDbType.Varchar2).Value = rsRackId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class APP_F_USR_HOT
    Inherits APP_F

    Private Const msFile As String = "File : RISAPP_F.vb, Class : DA01.APP_F_USR_HOT" & vbTab

    Public Function fnGet_UsrMenuInfo(ByVal rsUsrID As String) As DataTable
        Dim sFn As String = "Public Function fnGet_UsrMenuInfo(String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim aParam As New ArrayList

            sSql += "SELECT '' chk, f92.mnuid, f92.isparent, f92.mnulvl, f92.parentid,"
            sSql += "       LPAD('-', f92.mnulvl*4, '-') || mnunm mnunm"
            sSql += "  FROM (SELECT '1' chk, f92.mnuid "
            sSql += " 		   FROM rf092m f92, rf091m f91"
            sSql += "         WHERE f92.mnuid   = f91.mnuid"
            sSql += "           AND f92.mnugbn IN ('1', '9')"
            sSql += " 		    AND f91.usrid   = :usrid"
            sSql += "        ) f91 LEFT OUTER JOIN "
            sSql += "        rf092m f92 ON (f92.mnuid = f91.mnuid)"
            sSql += " WHERE f92.mnugbn IN ('1', '9')"
            sSql += "   AND (f92.mnulvl = 0 or f91.chk = '1')"
            sSql += "   AND mnunm  <> '-'"
            sSql += " ORDER BY f92.mnuid"

            aParam.Add(New OracleParameter("usrid",  OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

            DbCommand()
            Return DbExecuteQuery(sSql, aParam)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Function

    Public Function fnGet_UsrHotListInfo(ByVal rsUsrID As String) As DataTable
        Dim sFn As String = ""

        Try
            Dim sSql As String = ""
            Dim aParam As New ArrayList

            sSql += "SELECT f92.mnuid, f92.mnunm, f95.dispseq, f95.icongbn"
            sSql += "  FROM rf092m f92, rf095m f95"
            sSql += " WHERE f95.usrid = :usrid"
            sSql += "   AND f95.mnuid = f92.mnuid"
            sSql += "   AND f92.mnugbn in ('1', '9')"
            sSql += " ORDER BY f95.dispseq"

            aParam.Add(New OracleParameter("usrid",  OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

            DbCommand()
            Return DbExecuteQuery(sSql, aParam)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))


        End Try
    End Function

    Public Function TransUsrInfo(ByVal ro_Tcol As ItemTableCollection, ByVal rsUsrId As String) As Boolean
        Dim sFn As String = "Public Function TransUsrInfo() As Boolean"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
            End With

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim sField As String = "", sFields As String = "", sValue As String = "", sValues As String = ""

            sSql = ""
            sSql += "INSERT INTO rf095h SELECT fn_ack_sysdate, :modid, :modip, f.* FROM rf095m f"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
            dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "DELETE rf095m"
            sSql += " WHERE usrid = :usrid"

            dbCmd.Parameters.Clear()
            dbCmd.Parameters.Add("usrid",  OracleDbType.Varchar2).Value = rsUsrId

            dbCmd.CommandText = sSql
            iRet += dbCmd.ExecuteNonQuery()

            With ro_Tcol
                For ix1 As Integer = 0 To .ItemTableRowCount - 1
                    sField = "" : sFields = "" : sValue = "" : sValues = ""

                    dbCmd.Parameters.Clear()
                    For ix2 As Integer = 1 To .ItemTableColCount
                        sField = CType(.ItemTables.Item(.ItemTableColCount * (ix1) + ix2), ItemTable).Field
                        sFields += sField + ","

                        sValue = CType(.ItemTables.Item(.ItemTableColCount * (ix1) + ix2), ItemTable).Value
                        sValues += ":" + sField + ","

                        dbCmd.Parameters.Add(sField, CType(.ItemTables.Item(.ItemTableColCount * (ix1) + ix2), ItemTable).DbType).Value = sValue
                    Next

                    'insert new record
                    sFields = sFields.Substring(0, sFields.Length - 1)
                    sValues = sValues.Substring(0, sValues.Length - 1)
                    sSql = "INSERT INTO rf095m (" + sFields + ") VALUES (" + sValues + ")"

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()
                Next
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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

End Class

Public Class ItemTable
    Public Field As String
    Public Value As String
    Public DbType As OracleDbType
    Public Col As Integer
    Public Row As Integer
End Class

Public Class ItemTableCollection
    Public ItemTables As Collection
    Private RowCount As Integer = 0
    Private ColCount As Integer = 0

    Public ReadOnly Property ItemTableRowCount() As Integer
        Get
            Return RowCount
        End Get
    End Property

    Public ReadOnly Property ItemTableColCount() As Integer
        Get
            Return ColCount
        End Get
    End Property

    Public Sub New()
        ItemTables = New Collection
    End Sub

    Public Sub SetItemTable(ByVal rsField As String, ByVal riCol As Integer, ByVal riRow As Integer, ByVal rsValue As String, ByVal r_db_Type As OracleDbType)
        Dim it As New ItemTable

        With it
            .Field = rsField
            .Col = riCol
            .Row = riRow
            .Value = rsValue
            .DbType = r_db_Type
        End With

        ItemTables.Add(it)

        If riRow > RowCount Then
            RowCount = riRow
        End If

        If riCol > ColCount Then
            ColCount = riCol
        End If
    End Sub
End Class

