Imports DBORA.DbProvider
Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Public Class DB_CALC
    Private Const msFile As String = "File : CGCALCRST_CALC.vb, Class : DB_CALC" + vbTab

    Public Shared Function fnFind_Calculated_Result(ByVal rsCalForm As String, Optional ByVal r_objLisDbTran As Object = Nothing) As String
        Dim sFn As String = "Function Find_Calculated_Result"

        Dim sReturn As String = ""

        Try
            Dim dbCmd As New OracleCommand
            Dim dt As New DataTable
            Dim sSql As String

            sSql = ""
            sSql += "SELECT TO_CHAR(" + rsCalForm.Replace("^", "POWER") + ") FROM DUAL"


            DbCommand()
            dt = DbExecuteQuery(sSql)

            If dt.Rows.Count > 0 Then
                sReturn = dt.Rows(0).Item(0).ToString
            End If

            Return sReturn

        Catch ex As Exception


            Return ""

        End Try
    End Function

    Public Shared Function fnGet_CalcUrVolInfo_BcNo(ByVal rsBcNo As String, ByVal rsTGrpUv As String) As DataTable
        Dim sFn As String = "Public Shared Function Get_CalcUrVolInfo_BcNo(String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT r.bcno, r.testcd, r.spccd"
            If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "  FROM rr010m r, rf065m t"
            ElseIf PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "  FROM lm010m r, lf065m t"
            Else
                sSql += "  FROM lr010m r, lf065m t"
            End If
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd = t.testcd"
            sSql += "   AND r.spccd  = t.spccd"
            sSql += "   AND t.tgrpcd IN (" + rsTGrpUv + ")"

            Dim al As New ArrayList

            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            DbCommand()
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception

            COMMON.CommFN.Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + Err.Description)
            Return New DataTable
        End Try
    End Function

    Public Shared Function fnGet_CalcRstInfo_Pat(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, _
                                               ByVal rsCalDays As String, ByVal rsCalRange As String, Optional ByVal r_objLisDbTran As Object = Nothing) As DataTable
        Dim sFn As String = "Public Shared Function Get_CalcRstInfo_Pat(String, String, String) As DataTable"

        Try
            Dim sSql As String = ""

            sSql = ""

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "pkg_ack_rst.pkg_get_pat_calc_rstinfo_m"
            ElseIf PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "pkg_ack_rst.pkg_get_pat_calc_rstinfo_r"
            Else
                sSql += "pkg_ack_rst.pkg_get_pat_calc_rstinfo"
            End If

            Dim al As New ArrayList

            al.Add(New OracleParameter("rs_bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            al.Add(New OracleParameter("rs_testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            al.Add(New OracleParameter("rs_spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            al.Add(New OracleParameter("ri_caldays", OracleDbType.Int32, rsCalDays.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, Convert.ToInt16(rsCalDays)))

            DbCommand(r_objLisDbTran)

            Dim dt As DataTable = DbExecuteQuery(sSql, al, False)

            Return dt

        Catch ex As Exception
            COMMON.CommFN.Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + Err.Description)

            Return New DataTable

        End Try
    End Function

    Public Shared Function fnGet_CalcRstInfo_BcNo(ByVal rsBcNo As String, Optional ByVal rbAuto As Boolean = False, Optional ByVal r_objLisdbTran As Object = Nothing) As DataTable
        Dim sFn As String = "Public Shared Function fnGet_CalcRstInfo_BcNo(String, (Boolean), (Object)) As DataTable"

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT b.* "
            If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "  FROM lm010m a,"
            ElseIf PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "  FROM rr010m a,"
            Else
                sSql += "  FROM lr010m a,"
            End If

            sSql += "       ("
            sSql += "        SELECT 1 seq, c.calform, r.bcno, c.testcd ctestcd, r.testcd, f.tnmd, r.orgrst, r.rstflg rstflag,"
            sSql += "               c.param0 || '/' || NVL(c.param1, '') || '/' ||"
            sSql += "               NVL(c.param2, '') || '/' || NVL(c.param3, '') || '/' ||"
            sSql += "               NVL(c.param4, '') || '/' || NVL(c.param5, '') || '/' ||"
            sSql += "               NVL(c.param6, '') || '/' || NVL(c.param7, '') || '/' ||"
            sSql += "               NVL(c.param8, '') || '/' || NVL(c.param9, '') calitems,"
            sSql += "               f.dispseql sortpkey, 0 sortskey, c.caldays, c.calrange"

            If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "          FROM lr010m r, lf069m c, lf060m f"
            ElseIf PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "          FROM rr010m r, rf069m c, rf060m f"
            Else
                sSql += "          FROM lr010m r, lf069m c, lf060m f"
            End If

            sSql += "         WHERE r.bcno   = :bcno"
            sSql += "           AND r.testcd = c.testcd"
            sSql += "           AND r.spccd  = c.spccd"
            sSql += "           AND r.testcd = f.testcd"
            sSql += "           AND r.spccd  = f.spccd"
            sSql += "           AND r.tkdt  >= f.usdt"
            sSql += "           AND r.tkdt  <  f.uedt"

            If rbAuto Then
                sSql += "           AND NVL(c.caltype, 'M') = 'A'"
            End If

            sSql += "         UNION ALL "
            sSql += "        SELECT 2 seq,"
            sSql += "               CASE WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param0) THEN 'A'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param1) then 'B'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param2) then 'C'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param3) then 'D'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param4) then 'E'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param5) then 'F'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param6) then 'G'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param7) then 'H'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param8) then 'I'"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param9) then 'J'"
            sSql += "                    ELSE '-'"
            sSql += "               END calform,"
            sSql += "               r.bcno, c.testcd ctestcd, r.testcd, f.tnmd, r.orgrst, r.rstflg rstflag,"
            sSql += "               '' calitems, f.dispseql sortpkey,"
            sSql += "               CASE WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param0) THEN 10"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param1) then 11"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param2) then 12"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param3) then 13"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param4) then 14"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param5) then 15"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param6) then 16"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param7) then 17"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param8) then 18"
            sSql += "                    WHEN RPAD(r.testcd, 7, ' ') || r.spccd = TRIM(c.param9) then 19"
            sSql += "                    ELSE 20"
            sSql += "               END sortskey, c.caldays , c.calrange"
            If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "          FROM lr010m r, lf069m c, lf060m f"
            ElseIf PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "          FROM rr010m r, rf069m c, rf060m f"
            Else
                sSql += "          FROM lr010m r, lf069m c, lf060m f"
            End If

            sSql += "         WHERE r.bcno = :bcno"
            sSql += "           AND RPAD(r.testcd, 7, ' ') || r.spccd IN"
            sSql += "               ("
            sSql += "                TRIM(c.param0), TRIM(c.param1), TRIM(c.param2), TRIM(c.param3), TRIM(c.param4),"
            sSql += "                TRIM(c.param5), TRIM(c.param6), TRIM(c.param7), TRIM(c.param8), TRIM(c.param9)"
            sSql += "               )"
            sSql += "           AND r.testcd = f.testcd"
            sSql += "           AND r.spccd  = f.spccd"
            sSql += "           AND r.tkdt  >= f.usdt"
            sSql += "           AND r.tkdt   < f.uedt"

            If rbAuto Then
                sSql += "           AND NVL(c.caltype, 'M') = 'A'"
            End If

            sSql += "       ) b"
            sSql += " WHERE a.bcno   = :bcno"
            sSql += "   AND a.testcd = b.ctestcd"
            sSql += " ORDER BY ctestcd, seq, calform, sortpkey, b.testcd, sortskey"

            Dim al As New ArrayList

            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            DbCommand(r_objLisdbTran)
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            COMMON.CommFN.Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + Err.Description)
            Return New DataTable
        End Try
    End Function

    Public Shared Function fnGet_CalcState_BcNo(ByVal rsBcNo As String, Optional ByVal rbAuto As Boolean = False, Optional ByVal r_objLisdbTran As Object = Nothing) As DataTable
        Dim sFn As String = "Public Shared Function Get_CalcState_BcNo(String, (Boolean), (Object)) As DataTable"

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += " SELECT r.bcno, MIN(NVL(r.rstflg, '0')) minrstflg, NVL(c.calview, 'A') calview"
            If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "   FROM rr010m r, lf069m c"
            ElseIf PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                sSql += "  FROM lm010m r, rf069m c"
            Else
                sSql += "  FROM lr010m r, lf069m c"
            End If
            sSql += "  where r.bcno   = :bcno"
            sSql += "    and r.testcd = c.testcd"
            sSql += "    and r.spccd  = c.spccd"

            If rbAuto Then
                sSql += "    and NVL(c.caltype, 'M') = 'A'"
            End If
            sSql += "  group by r.bcno, calview"

            Dim al As New ArrayList

            al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            DbCommand(r_objLisdbTran)
            Return DbExecuteQuery(sSql, al)

        Catch ex As Exception
            COMMON.CommFN.Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + Err.Description)
            Return New DataTable
        End Try
    End Function

End Class





