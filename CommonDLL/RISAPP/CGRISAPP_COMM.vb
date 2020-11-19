Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommFN
Imports COMMON.SVar

Namespace COMM
    Public Class CdFn
        Private Const msFile As String = "File : CGRISAPP_COM, Class : RISAPP.COMM.CdFn" + vbTab

        ' 검체분류
        Public Shared Function fnGet_Bccls_List(Optional ByVal rbAll As Boolean = True, Optional ByVal rbBloodBank As Boolean = False, Optional ByVal rbMicroBio As Boolean = False) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Bccls_List() As DataTable"

            Try
                Dim sSql As String = ""

                sSql += "SELECT bcclscd, bcclsnmd, colorgbn"
                sSql += "  FROM rf010m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"

                If rbMicroBio Then
                    sSql += "   AND bcclscd = '2'"
                ElseIf rbBloodBank Then
                    sSql += "   AND bcclscd IN ('3', '7')"
                ElseIf rbAll = False Then
                    sSql += "   AND bcclscd NOT IN ('2', 3', '7', '8', '9')"
                End If

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_bccls_color() As DataTable
            Dim sFn As String = "Function fnGet_bccls_color() As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT bcclscd, REPLACE(bcclsnmd, '검체', '') bcclsnmd, colorgbn, bcclsgbn"
                sSql += "  FROM rf010m "
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND NVL(colorgbn, '0') > 0"
                sSql += "   and bcclsgbn <> '9'"
                sSql += " ORDER BY colorgbn"

                DbCommand()
                Return DbExecuteQuery(sSql)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 검체분류
        Public Shared Function fnGet_Bccls_ExLab_List(ByVal rsExLabCd As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Bccls_ExLab_List() As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT bcclscd, bcclsnmd, colorgbn"
                sSql += "  FROM rf010m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                If rsExLabCd <> "" Then
                    sSql += "   AND bcclscd IN (SELECT bcclscd FROM rf060m WHERE NVL(exlabyn, '0') = '1' AND exlabcd = :exlabcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                    alParm.Add(New OracleParameter("exlabcd", rsExLabCd))
                Else
                    sSql += "   AND bcclscd IN (SELECT bcclscd FROM rf060m WHERE NVL(exlabyn, '0') = '1' AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 검사코드 리스트(병원체검체검사 전용)
        Public Shared Function fnGet_test_list_req(ByVal rsSlipCd As String, ByVal rsTGrpCd As String, ByVal rsWGrpCd As String, _
                                               Optional ByVal rsTestCd As String = "", Optional ByVal rsSpcCd As String = "", _
                                               Optional ByVal rsBcclsCd As String = "", Optional ByVal rsTordSlip As String = "", _
                                               Optional ByVal rsFilter As String = "") As DataTable
            Dim sFn As String = "Function fnGet_test_list(String, String, String, [String], [String], [String], [String]) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       '' chk, MAX(tnmd) tnmd, MAX(tnmp) tnmp, testcd, tcdgbn," + vbCrLf
                'sSql += "       NVL(titleyn, '0') titleyn, NVL(mbttype, '0') mbttype," + vbCrLf
                'sSql += "       NVL(ordhide, '0') ordhide, NVL(poctyn, '0') poctyn," + vbCrLf
                'sSql += "       fn_ack_get_slip_dispseq(partcd, slipcd, fn_ack_sysdate) sort1,"+ vbCrLf
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = a.partcd AND slipcd = a.slipcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1," + vbCrLf
                sSql += "       MIN(dispseql) sort2," + vbCrLf
                sSql += "       (SELECT MIN(NVL(dispseq, 999)) FROM lf100m WHERE tordslip = a.tordslip AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort_tslip," + vbCrLf
                sSql += "       MIN(dispseqo) sort_ord" + vbCrLf
                sSql += "  FROM rf060m a" + vbCrLf
                sSql += " WHERE usdt <= fn_ack_sysdate" + vbCrLf
                sSql += "   AND uedt >  fn_ack_sysdate" + vbCrLf
                sSql += "   AND (tcdgbn = 'P' or nvl(titleyn,'0') = '0') "
                If rsTestCd <> "" Then
                    sSql += "   AND testcd = :testcd" + vbCrLf
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND spccd = :spccd" + vbCrLf
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND (SUBSTR(testcd, 1, 5), spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf065m WHERE tgrpcd = :tgrpcd) " + vbCrLf

                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))

                ElseIf rsSlipCd <> "" Then
                    sSql += "   AND partcd = :partcd" + vbCrLf
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))

                    If rsSlipCd.Length = 2 Then
                        sSql += "   AND slipcd = :slipcd" + vbCrLf
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                    End If
                End If

                If rsBcclsCd <> "" Then
                    sSql += "   AND bcclscd = :bcclscd" + vbCrLf
                    al.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                If rsWGrpCd <> "" Then
                    sSql += "   AND (SUBSTR(testcd, 1, 5), spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf066m WHERE wkgrpcd = :wgrpcd)" + vbCrLf
                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                End If

                If rsTordSlip <> "" Then
                    sSql += "   AND tordslip = :tordslip" + vbCrLf
                    al.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsTordSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTordSlip))
                End If

                If rsFilter <> "" Then
                    sSql += "   AND " + rsFilter
                End If

                sSql += " GROUP BY testcd, tcdgbn, titleyn, mbttype, partcd, slipcd, ordhide, tordslip, poctyn" + vbCrLf
                sSql += " ORDER BY sort1, sort2, testcd" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        ' 검사분야 조회
        Public Shared Function fnGet_Slip_List(Optional ByVal rsUsDt As String = "", Optional ByVal rbCtTest As Boolean = False) As DataTable
            Dim sFn As String = "Function fnGet_Slip_List([String], [Boolean]) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                rsUsDt = rsUsDt.Replace("-", "").Replace(":", "").Replace(" ", "")
                If rsUsDt.Length = 8 Then rsUsDt += "000000"

                sSql += "SELECT DISTINCT"
                sSql += "       partcd || slipcd slipcd, slipnmd, dispseq"
                sSql += "  FROM rf021m"

                If rsUsDt = "" Then
                    sSql += " WHERE usdt <= fn_ack_sysdate"
                    sSql += "   AND uedt >  fn_ack_sysdate"
                Else
                    sSql += " WHERE usdt <= :dates"
                    sSql += "   AND uedt >  :datee"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                End If

                If rbCtTest Then
                    sSql += "   AND (partcd, slipcd) IN (SELECT partcd, slipcd FROM rf060m  WHERE NVL(ctgbn, '0') = '1')"
                End If

                sSql += "   AND partcd <> 'Z'"

                sSql += " ORDER BY dispseq, slipcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 분야 목록 조회
        Public Shared Function fnGet_Part_List(Optional ByVal rbTake2Yn As Boolean = False) As DataTable
            Dim sFn As String = "Function fnGet_Part_List() As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT a.partcd, a.partnmd, MAX(b.dispseq) dispseq"
                sSql += "  FROM rf020m a, rf021m b"
                sSql += " WHERE a.partcd = b.partcd"
                sSql += "   AND a.usdt  <= fn_ack_sysdate"
                sSql += "   AND a.uedt  >  fn_ack_sysdate"
                sSql += "   AND b.usdt  <= fn_ack_sysdate"
                sSql += "   AND b.uedt  >  fn_ack_sysdate"

                If rbTake2Yn Then
                    sSql += "   AND NVL(a.take2yn, '0') = '1'"
                End If

                sSql += " GROUP BY a.partcd, a.partnmd"
                sSql += " ORDER BY dispseq, a.partcd"

                DbCommand()
                Return DbExecuteQuery(sSql)

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

                al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


        '-- 검사코드 리스트
        Public Shared Function fnGet_test_wllist(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String) As DataTable
            Dim sFn As String = "Function fnGet_test_wllist(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, MAX(tnmd) tnmd, MAX(tnmp) tnmp, b.testcd, tcdgbn,"
                sSql += "       NVL(titleyn, '0') titleyn, NVL(mbttype, '0') mbttype,"
                sSql += "       NVL(ordhide, '0') ordhide, NVL(poctyn, '0') poctyn,"
                'sSql += "       fn_ack_get_slip_dispseq(partcd, slipcd, fn_ack_sysdate) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = b.partcd AND slipcd = b.slipcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1,"
                sSql += "       MIN(dispseql) sort2,"
                sSql += "       (SELECT MIN(NVL(dispseq, 999)) FROM lf100m WHERE tordslip = b.tordslip AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort_tslip,"
                sSql += "       MIN(dispseqo) sort_ord"
                sSql += "  FROM rrw11m w, rf060m b"
                sSql += " WHERE w.wluid   = :wluid"
                sSql += "   AND w.wlymd   = :wlymd"
                sSql += "   AND w.wltitle = :wltitle"
                sSql += "   AND w.testcd  = b.testcd"
                sSql += "   AND b.usdt   <= fn_ack_sysdate"
                sSql += "   AND b.uedt   > fn_ack_sysdate"

                al.Add(New OracleParameter("WLUid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                al.Add(New OracleParameter("WLYmd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                al.Add(New OracleParameter("WLTitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle.Substring(0, rsWLTitle.Length - 10)))


                sSql += " GROUP BY b.testcd, tcdgbn, titleyn, mbttype, partcd, slipcd, ordhide, tordslip, poctyn"
                sSql += " ORDER BY sort1, sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 검사코드 리스트
        Public Shared Function fnGet_test_list(ByVal rsSlipCd As String, ByVal rsTGrpCd As String, ByVal rsWGrpCd As String, _
                                       Optional ByVal rsTestCd As String = "", Optional ByVal rsSpcCd As String = "", _
                                       Optional ByVal rsBcclsCd As String = "", Optional ByVal rsTordSlip As String = "", _
                                       Optional ByVal rsFilter As String = "") As DataTable
            Dim sFn As String = "Function fnGet_test_list(String, String, String, [String], [String], [String], [String]) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, MAX(tnmd) tnmd, MAX(tnmp) tnmp, testcd, tcdgbn,"
                sSql += "       NVL(titleyn, '0') titleyn, NVL(mbttype, '0') mbttype,"
                sSql += "       NVL(ordhide, '0') ordhide, NVL(poctyn, '0') poctyn,"
                'sSql += "       fn_ack_get_slip_dispseq(partcd, slipcd, fn_ack_sysdate) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = a.partcd AND slipcd = a.slipcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1,"
                sSql += "       MIN(dispseql) sort2,"
                sSql += "       (SELECT MIN(NVL(dispseq, 999)) FROM lf100m WHERE tordslip = a.tordslip AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort_tslip,"
                sSql += "       MIN(dispseqo) sort_ord"
                sSql += "  FROM rf060m a"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND ordhide = '0' "

                If rsTestCd <> "" Then
                    sSql += "   AND testcd = :testcd"
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND spccd = :spccd"
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND (SUBSTR(testcd, 1, 5), spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM rf065m WHERE tgrpcd = :tgrpcd) "

                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))

                ElseIf rsSlipCd <> "" Then
                    sSql += "   AND partcd = :partcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))

                    If rsSlipCd.Length = 2 Then
                        sSql += "   AND slipcd = :slipcd"
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                    End If
                End If

                If rsBcclsCd <> "" Then
                    sSql += "   AND bcclscd = :bcclscd"
                    al.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                If rsWGrpCd <> "" Then
                    sSql += "   AND (SUBSTR(testcd, 1, 5), spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM rf066m WHERE wkgrpcd = :wgrpcd)"

                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                End If

                If rsTordSlip <> "" Then
                    sSql += "   AND tordslip = :tordslip"
                    al.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsTordSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTordSlip))
                End If

                If rsFilter <> "" Then
                    sSql += "   AND " + rsFilter
                End If

                sSql += " GROUP BY testcd, tcdgbn, titleyn, mbttype, partcd, slipcd, ordhide, tordslip, poctyn"
                sSql += " ORDER BY sort1, sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 검사코드 리스트
        Public Shared Function fnGet_testspc_list(ByVal rsSlipCd As String, ByVal rsTGrpCd As String, _
                                                  Optional ByVal rsTestCd As String = "", _
                                                  Optional ByVal rsSpcCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_testspc_list(String, String) As DataTable"
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
                    sSql += "   AND f6.testcd LIKE :testcd || '%'"

                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND f6.spccd = :spccd"

                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsSlipCd <> "" Then
                    sSql += "   AND f6.partcd = :partcd"
                    sSql += "   AND f6.slipcd = :slipcd"

                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND (f6.testcd, f6.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"

                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

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

        '-- 검사코드 리스트(Battery, Group 제외)
        Public Shared Function fnGet_testspc_BatteryParentSingle(ByVal rsSlipCd As String, Optional ByVal rsTestCd As String = "", Optional ByVal rsSpcCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_testspc_BatteryParentSingle(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, f6.testcd, f6.spccd, f6.tnmd, f3.spcnmd, NVL(f6.dispseql, 999) sort2,"
                sSql += "       tcdgbn, NVL(titleyn, '0') titleyn, NVL(mbttype, '0') mbttype"
                sSql += "  FROM rf060m f6, lf030m f3"
                sSql += " WHERE f6.tcdgbn IN ('B', 'P', 'S')"
                sSql += "   AND f6.usdt <= fn_ack_sysdate"
                sSql += "   AND f6.uedt >  fn_ack_sysdate"
                sSql += "   AND f6.spccd = f3.spccd"
                sSql += "   AND f3.usdt <= fn_ack_sysdate"
                sSql += "   AND f3.uedt >  fn_ack_sysdate"

                If rsTestCd <> "" Then
                    sSql += "   AND f6.testcd = :testcd"
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND f6.spccd = :spccd"
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsSlipCd <> "" Then
                    sSql += "   AND f6.partcd = :partcd"
                    sSql += "   AND f6.slipcd = :slipcd"

                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If

                sSql += " ORDER BY sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 일반 검사항목 조회
        Public Shared Function fnGet_test_WithParent(ByVal rsTestCd As String, Optional ByVal rsSpcCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_tcls_WithParent(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT '' chk, '.. ' || MAX(tnms) tnms, '.. ' || MAX(tnmD) tnmd, testcd, tordcd, tordslip, tcdgbn,"
                sSql += "       NVL(viwsub, '0') viwsub, MIN(dispseql) sort2"
                sSql += "  FROM rf060m"
                sSql += " WHERE testcd LIKE :testcd || '%'"

                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                If rsSpcCd <> "" Then
                    sSql += "    AND spccd = :spccd"
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += "   AND tcdgbn = 'C'"
                sSql += "   AND usdt  <= fn_ack_sysdate"
                sSql += "   AND uedt   > fn_ack_sysdate"
                sSql += " GROUP BY testcd, tordcd, tordslip, tcdgbn, viwsub"
                sSql += " ORDER BY sort2, testcd"


                DbCommand()
                Return DbExecuteQuery(sSql, al)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 일반 검사항목 조회
        Public Shared Function fnGet_test_WithReference(ByVal rsTestCd As String, Optional ByVal rsSpcCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_tcls_WithParent(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT '' chk, MAX(f6.tnms) tnms, MAX(f6.tnmD) tnmd, f6.testcd, f6.tordcd, f6.tordslip, f6.tcdgbn, MIN(NVL(f6.dispseqL, 999)) sort2"
                sSql += "  FROM rf063m f67, rf060m f6"
                sSql += " WHERE f67.testcd = :testcd"

                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                If rsSpcCd <> "" Then
                    sSql += "   AND f67.spccd  = :spccd"
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += "   AND f67.reftestcd = f6.testcd"
                sSql += "   AND f67.refspccd  = f6.spccd"
                sSql += "   AND f6.usdt      <= fn_ack_sysdate"
                sSql += "   AND f6.uedt      >  fn_ack_sysdate"
                sSql += " GROUP BY f6.testcd, f6.tordcd, f6.tordslip, f6.tcdgbn"
                sSql += " ORDER BY sort2, f6.testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 검사코드 리스트(Battery, Group 제외)
        Public Shared Function fnGet_test_ParentSingle(ByVal rsSlipCd As String, ByVal rsTGrpCd As String, Optional ByVal rsTestCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_tcls_ParentSingle(String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, MAX(f6.tnmd) tnmd, MAX(f6.tnmp) tnmp, f6.testcd, f6.tordcd, f6.tordslip, f6.tcdgbn,"
                sSql += "       MIN(NVL(f6.dispseql, 999)) sort2,"
                sSql += "       MAX(NVL(f6.ctgbn, '0')) ctgbn,"
                sSql += "       f5.exlabnmd"
                sSql += "  FROM rf060m f6 LEFT OUTER JOIN"
                sSql += "       rf050m f5 ON (f6.exlabcd = f5.exlabcd AND NVL(f5.delflg, ' ') <> '1')"
                sSql += " WHERE f6.tcdgbn IN ('P', 'S')"
                sSql += "   AND f6.usdt   <= fn_ack_sysdate"
                sSql += "   AND f6.uedt   >  fn_ack_sysdate"


                If rsTestCd <> "" Then
                    sSql += "   AND f6.testcd LIKE :testcd || '%'"
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSlipCd <> "" Then
                    sSql += "   AND f6.partcd = :partcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))

                    If rsSlipCd.Length = 2 Then
                        sSql += "   AND f6.slipcd = :slipcd"
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                    End If
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND (f6.testcd, f6.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"

                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                sSql += " GROUP BY testcd, tordcd, tordslip, tcdgbn, exlabnmd"
                sSql += " ORDER BY sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_TestWithSpc_List(ByVal rsTestCd As String, ByVal rsUsDt As String) As DataTable
            Dim sFn As String = "fnGet_TestWithSpc_List([String], [String], [String], [String])"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       f6.testcd, f6.spccd, f6.usdt, f6.uedt, f3.spcnmd"
                sSql += "  FROM rf060m f6, lf030m f3"
                sSql += " WHERE f6.testcd = :testcd"
                sSql += "   AND f6.spccd  = f3.spccd"
                sSql += "   AND f6.usdt  <= :usdt"
                sSql += "   AND f6.uedt  >  :usdt"
                sSql += "   AND f3.usdt  <= :usdt"
                sSql += "   AND f3.uedt  >  :usdt"
                sSql += " ORDER BY spcnmd"

                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                al.Add(New OracleParameter("uedt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                al.Add(New OracleParameter("uedt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_Spc_List(ByVal rsUsDt As String, ByVal rsPartCd As String, ByVal rsSlipCd As String, _
                              ByVal rsTGrpCd As String, ByVal rsWGrpCd As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "fnGet_Spc_List([String], [String], [String], [String])"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                rsUsDt = rsUsDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                If rsUsDt.Length = 8 Then rsUsDt += "000000"

                sSql += "SELECT DISTINCT spccd, spcnmd"
                sSql += "  FROM lf030m"

                If rsUsDt <> "" Then
                    sSql += " WHERE usdt <= :usdt"
                    sSql += "   AND uedt >  :usdt"

                    al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                    al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                Else
                    sSql += " WHERE usdt <= fn_ack_sysdate"
                    sSql += "   AND uedt >  fn_ack_sysdate"
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND spccd = :spccd"
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsTestCd.Length > 0 Then
                    sSql += "   AND spccd IN (SELECT spccd FROM rf060m  where testcd = :testcd)"
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsPartCd.Length + rsSlipCd.Length = 2 Then
                    sSql += "   AND spccd IN (SELECT spccd FROM rf060m  WHERE partcd = :partcd AND slipcd = :slipcd)"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                ElseIf rsPartCd.Length > 0 Then
                    sSql += "   AND spccd IN (SELECT spccd FROM rf060m  WHERE partcd = :partcd)"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND spccd IN (SELECT spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                If rsWGrpCd <> "" Then
                    sSql += "   AND spccd IN (SELECT spccd FROM rf066m WHERE wkgrpcd = :wgrpcd)"
                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                End If

                sSql += " ORDER BY spccd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try
        End Function

        '-- 용기리스트
        Public Shared Function fnGet_Tube_List() As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Tube_List(object) As DataTable"

            Try
                Dim sSql As String = ""

                sSql += "SELECT tubecd, tubenmd FROM lf040m"
                sSql += " WHERE usdt   <= fn_ack_sysdate"
                sSql += "   AND uedt   >  fn_ack_sysdate"
                sSql += "   and tubecd >  '00'"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        ' 검사그룹 조회
        Public Shared Function fnGet_TGrp_List() As DataTable
            Dim sFn As String = "Function fnGet_TGrp_List() As DataTable"
            Try
                Dim sSql As String = ""
                Dim arlParm As New ArrayList

                sSql += "SELECT DISTINCT f63.tgrpcd, f63.tgrpnmd"
                sSql += "  FROM rf065m f63, rf060m f6"
                sSql += " WHERE f63.testcd = f6.testcd"
                sSql += "   AND f63.spccd  = f6.spccd"
                sSql += "   AND f6.usdt   <= fn_ack_sysdate"
                sSql += "   AND f6.uedt   >  fn_ack_sysdate"
                sSql += " ORDER BY F63.tgrpcd"

                DbCommand()
                Return DbExecuteQuery(sSql, arlParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 검사그룹 검사항목 조회
        Public Shared Function fnGet_TGrp_Test_List(ByVal rsTGrpCd As String) As DataTable
            Dim sFn As String = "Function fnGet_TGrp_Test_List(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       RPAD(testcd, 7, ' ') || spccd testcd"
                sSql += "  FROM rf065m"

                If rsTGrpCd.IndexOf(",") > 0 Then
                    sSql += " WHERE tgrpcd IN ('" + rsTGrpCd.Replace(",", "','") + "')"
                Else
                    sSql += " WHERE tgrpcd = :tgrpcd"
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_TGrp_Test_List(ByVal rsTGrpCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "fnGet_TGrp_Test_List(String, String)"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList


                sSql += "SELECT f6.testcd, MAX(f6.tnmd) tnmd,"
                sSql += "       MAX(f2.dispseq) sort1,"
                sSql += "       MAX(f6.dispseql) sort2"
                sSql += "  FROM rf065m f63, rf060m f6, rf021m f2"
                sSql += " WHERE f63.tgrpcd = :tgrpcd"

                alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))

                If rsSpcCd <> "" Then
                    sSql += "   AND f63.spccd = :spccd"
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += "   AND f63.testcd = f6.testcd"
                sSql += "   AND f63.spccd  = f6.spccd"
                sSql += "   AND f6.usdt   <= fn_ack_sysdate"
                sSql += "   AND f6.uedt   >  fn_ack_sysdate"
                sSql += "   AND f6.partcd  = f2.partcd"
                sSql += "   AND f6.slipcd  = f2.slipcd"
                sSql += "   AND f2.usdt   <= fn_ack_sysdate"
                sSql += "   AND f2.uedt   >  fn_ack_sysdate"
                sSql += " GROUP BY f6.testcd"
                sSql += " ORDER BY sort1, sort2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 작업그룹 조회
        Public Shared Function fnGet_WKGrp_List(ByVal rsSlipCd As String) As DataTable
            Dim sFn As String = "Function fnGet_WKGrp_List(String, string) As DataTable"
            Try
                Dim sqlDoc As String = ""
                Dim arlParm As New ArrayList

                sqlDoc += "SELECT DISTINCT wkgrpcd, wkgrpnmd, wkgrpgbn"
                sqlDoc += "  FROM rf066m"

                If rsSlipCd.Length = 1 Then
                    sqlDoc += " WHERE partcd = :partcd"
                    arlParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                ElseIf rsSlipCd.Length = 2 Then
                    sqlDoc += " WHERE partcd = :partcd"
                    sqlDoc += "   AND slipcd = :slipcd"
                    arlParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    arlParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If
                sqlDoc += " ORDER BY wkgrpcd"

                DbCommand()
                Return DbExecuteQuery(sqlDoc, arlParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 외주업체 조회
        Public Shared Function fnGet_ExLab_List() As DataTable
            Dim sFn As String = "fnGet_Com_ListfnGet_ExLab_List As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT '' chk, exlabcd, exlabnmd"
                sSql += "  FROM rf050m"
                sSql += " WHERE NVL(delflg, '0') = '0'"
                sSql += " ORDER BY exlabcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 장비 리스트 조회
        Public Shared Function fnGet_Eq_List(ByVal rsEqGbn As String) As DataTable
            Dim sFn As String = "fnGet_Eq_List() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT '' chk, eqcd, eqnm"
                sSql += "  FROM lf070m"
                sSql += " WHERE NVL(delflg, '0') = '0'"
                sSql += "   AND eqgbn = '" + rsEqGbn + "'"
                sSql += " ORDER BY eqcd"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 처방슬립 가져오기
        Public Shared Function fnGet_OrdSlip_List() As DataTable
            Dim sFn As String = "Function getOrdSlip() As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT tordslip, tordslipnm, dispseq"
                sSql += "  FROM lf100m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += " ORDER BY dispseq, tordslip"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 
        Public Shared Function fnGet_cmtcont_slip(ByVal rsSlipCd As String, Optional ByVal rsCmtCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_cmtcont_slip(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT '' chk, cmtcd, cmtcont, slipnmd, dispseq"
                sSql += "  FROM ("
                sSql += "        SELECT f8.cmtcd, f8.cmtcont,"
                sSql += "               '[' || f8.partcd || f8.slipcd || '] ' ||  NVL(f2.slipnmd, CASE WHEN f8.partcd || f8.slipcd =  '00' THEN '공통' ELSE NVL(f2.slipnmd, '') END) slipnmd,"
                sSql += "               NVL(f8.dispseq, 999) dispseq"
                sSql += "          FROM rf080m f8  LEFT OUTER JOIN rf021m f2  ON (f8.partcd = f2.partcd AND f8.slipcd = f2.slipcd)"

                If rsSlipCd <> "" Then
                    sSql += "         WHERE f8.partcd || f8.slipcd IN ('00', :slipcd)"
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If
                sSql += "       ) a"

                If rsCmtCd <> "" Then
                    sSql += " WHERE cmtcd = :cmtcd"
                    alParm.Add(New OracleParameter("cmtcd", OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))
                End If
                sSql += " ORDER BY dispseq, cmtcd"


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 기타코드 조회
        Public Shared Function fnGet_Etc_CdLists(ByVal rsCmtGbn As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Etc_CdLists(string) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT cmtcd, cmtcont FROM lf410m"
                sSql += " WHERE cmtgbn = :cmtgbn"
                sSql += "   AND usdt  <= fn_ack_sysdate"
                sSql += "   AND uedt  >  fn_ack_sysdate"

                alParm.Add(New OracleParameter("cmtgbn", OracleDbType.Varchar2, rsCmtGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtGbn))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_Usr_List(Optional ByVal rbUseFlg As Boolean = False, Optional ByVal rsUsrId As String = "") As DataTable
            Dim sFn As String = "fnGet_UsrInfo"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT '' chk, usrid, usrnm,"
                sSql += "       CASE WHEN NVL(delflg, '0') = '0' THEN '사용중' ELSE '삭제' END delflg"
                sSql += "  FROM rf090m "
                If rbUseFlg Then
                    sSql += " WHERE NVL(delflg, '0') = '0'"
                End If

                If rsUsrId <> "" Then
                    sSql += IIf(sSql.IndexOf(" WHERE ") < 0, " WHERE ", "   AND ").ToString + "usrid = :usrid"
                    alParm.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrId))
                End If
                sSql += " ORDER BY delflg"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 검사항목별 결과코드
        Public Shared Function fnGet_TestRst_list(ByVal rsTestCd As String) As DataTable

            Dim sFn As String = "fnGet_TestRst_list() As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT keypad, grade, rstcont, rstcdseq"
                sSql += "  FROM rf083m"
                sSql += " WHERE testcd = :testcd"
                sSql += "   AND spccd  = '" + "".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"
                sSql += " ORDER BY rstcdseq"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_cmtcont_etc(ByVal rsCmtGbn As String, ByVal rbCmtGbnAdd As Boolean, Optional ByVal rsCmtCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_cmtcont_etc(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rbCmtGbnAdd Then
                    sSql += "SELECT cmtgbn || cmtcd cmtcd, cmtcont  FROM rf410m"
                Else
                    sSql += "SELECT cmtcd cmtcd, cmtcont  FROM rf410m"
                End If
                sSql += " WHERE cmtgbn = :cmtgbn"

                alParm.Add(New OracleParameter("cmtgbn", OracleDbType.Varchar2, rsCmtGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtGbn))

                If rsCmtCd <> "" Then
                    sSql += "   AND cmtcd = :cmtcd"
                    alParm.Add(New OracleParameter("cmtcd", OracleDbType.Varchar2, rsCmtCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCd))
                End If

                sSql += " ORDER BY cmtcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

    Public Class RstFn
        Private Const msFile As String = "File : CGLISAPP_COMM.vb, Class : LISAPP.COMM.RstFn" + vbTab

        '-- 특수결과 존재 여부
        Public Shared Function fnGet_SpRst_yn(ByVal rsBcNo As String, ByVal rsTestCd As String) As String
            Dim sFn As String = "Function fnGet_SpRst_yn(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT r.testcd"
                sSql += "  FROM rr010m r, rf060m f6, rf310m f31"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND r.testcd = :testcd"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.testcd = f31.testcd"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return "Y"
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 해당검체에 최종보고 수정내용 조회
        Public Shared Function fnGet_FN_Modify_Cmt(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_FN_Modify_Cmt(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT seq, cmtcd, cmtcont,"
                sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi') regdt,"
                sSql += "       fn_ack_get_usr_name(regid) regnm"
                sSql += "  FROM rr052m"
                sSql += " WHERE bcno = :bcno"
                sSql += " ORDER BY regdt, seq"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '-- 검사항목 결과코드 조회
        Public Shared Function fnGet_Test_RstCdList(ByVal rsTestCds As String, ByVal rsWkGrpCd As String) As DataTable
            Dim sFn As String = "Function fnGet_Test_RstCdList(String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT testcd, keypad, rstcont, grade"
                sSql += "  FROM rf083m"

                If rsTestCds = "" Then
                    sSql += " WHERE testcd IN (SELECT testcd FROM rf066m WHERE wkgrpcd = :wgprcd)"
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                Else
                    sSql += " WHERE testcd IN (" + rsTestCds + ")"
                End If

                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 검사명 가져오기
        Public Shared Function fnGet_ManualDiff_Tnmd(ByVal rsTestCd As String, ByVal rsSpcCd As String) As String
            Dim sFn As String = "Function getManualDiffName(string, string) As string"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim dt As New DataTable

                sSql += "SELECT tnmd FROM rf060m"
                sSql += " WHERE testcd = :testcd"
                sSql += "   AND spccd  = :spccd"
                sSql += "   AND usdt  <= fn_ack_sysdate"
                sSql += "   AND uedt  >  fn_ack_sysdate"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    fnGet_ManualDiff_Tnmd = dt.Rows(0).Item("tnmd").ToString
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 메뉴얼 Diff History
        Public Shared Function fnGet_ManualDiff_History(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As String
            Dim sFn As String = "Function fnGet_ManualDiff_History(string, string) As string"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim dt As New DataTable

                sSql += "SELECT a.testcd, a.viewrst"
                sSql += "  FROM rr010m a,"
                sSql += "       (SELECT bcno"
                sSql += "          FROM (SELECT rstdt, bcno FROM rr010m"
                sSql += "                 WHERE regno  = (SELECT regno FROM rj010m WHERE bcno = :bcno)"
                sSql += "                   AND testcd = :testcd"
                sSql += "                   AND spccd  = :spccd"
                sSql += "                   AND tkdt   < (SELECT tkdt FROM rr010m WHERE bcno = :bcno AND testcd = :testcd AND spccd = :spccd)"
                sSql += "                   AND rstflg = '3'"
                sSql += "                 ORDER BY rstdt DESC"
                sSql += "               )"
                sSql += "         WHERE ROWNUM = 1"
                sSql += "       ) h"
                sSql += " WHERE a.bcno   = h.bcno"
                sSql += "   AND a.testcd like :testcd || '%'"
                sSql += "   AND NVL(a.orgrst, ' ') <> ' '"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                Dim strBuf As String = ""

                If dt.Rows.Count > 0 Then
                    For intIdx As Integer = 0 To dt.Rows.Count - 1
                        strBuf += dt.Rows(intIdx).Item("testcd").ToString + "^" + dt.Rows(intIdx).Item("viewrst").ToString + "|"
                    Next
                End If

                Return strBuf

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- Keypad 항목 가져오기
        Public Shared Function fnGet_ManualDiff(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "Function getManualDiff(string, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT b.testcd, b.tnmd, b.reqsub"
                sSql += "  FROM rf420m a, rf060m b"
                sSql += " WHERE a.testcd    LIKE :testcd || '%'"
                sSql += "   AND a.spccd     = :spccd"
                sSql += "   AND a.cnttestcd = b.testcd"
                sSql += "   AND a.spccd     = b.spccd"
                sSql += "   AND b.usdt     <= fn_ack_sysdate"
                sSql += "   AND b.uedt     >  fn_ack_sysdate"
                sSql += " ORDER BY testcd"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' Manual Diff 폼 구분 가져오기
        Public Shared Function fnGet_ManualDiff_FormGbn(ByVal rsTestCd As String, ByVal rsSpcCd As String) As String
            Dim sFn As String = "Function GetManualDiff_FormGbn(string, string) As String"
            Try
                Dim dt As New DataTable
                Dim sSql As String
                Dim alParm As New ArrayList

                sSql = "SELECT formgbn FROM rf420m WHERE testcd = :testcd AND spccd = :spccd AND ROWNUM = 1"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' Manual Diff의 WBC 코드 가져오기
        Public Shared Function fnGet_ManualDiff_WBC_TestCd(ByVal rsTestCd As String, ByVal rsSpcCd As String) As String
            Dim sFn As String = "Function fnGet_ManualDiff_WBC_TestCd(string, string) As String"
            Try
                Dim dt As New DataTable
                Dim sSql As String
                Dim alParm As New ArrayList

                sSql = "SELECT wbctestcd FROM rf420m WHERE testcd = :testcd AND spccd = :spccd"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' Manual Diff의 WBC 결과값 가져오기
        Public Shared Function fnGet_ManualDiff_WBC_Rst(ByVal rsBcNo As String, ByVal rsTestCd As String) As String
            Dim sFn As String = "Function fnGet_ManualDiff_WBC_Rst(string, string) As String"
            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT orgrst FROM rr010m"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"
                sSql += "   AND rstflg = '3'"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' Manual Diff % 코드 가져오기
        Public Shared Function fnGet_ManualDiff_Percent_TclsCd(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsCTestCd As String) As String
            Dim sFn As String = "Function GetManualDiff_FormGbn(string, string) As String"
            Try
                Dim dt As New DataTable
                Dim sSql As String
                Dim alParm As New ArrayList

                sSql = "SELECT pertestcd FROM rf420m WHERE testcd = :testcd AND spccd = :spccd AND cnttestcd = :cnttestcd"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                alParm.Add(New OracleParameter("cnttestcd", OracleDbType.Varchar2, rsCTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCTestCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 해당검체에 검체명 가져오기
        Public Shared Function fnGet_SpcNmInfo(ByVal rsBcNo As String) As String
            Dim sFn As String = "Function fnGet_SpcNmInfo(string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT b.spcnmd FROM rj010m a, lf030m b"
                sSql += " WHERE a.bcno  = :bcno"
                sSql += "   AND a.spccd = b.spccd"
                sSql += "   AND b.usdt <= a.bcprtdt"
                sSql += "   AND b.uedt >  a.bcprtdt"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("spcnmd").ToString
                Else
                    Return ""
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 계산식 결과 리턴
        Public Shared Function fnGet_Calc_DBQuery(ByVal rsCalcForm As String, _
                                            Optional ByVal ro_DbCn As oracleConnection = Nothing, _
                                            Optional ByVal ro_DbTrans As oracleTransaction = Nothing) As String
            Dim sFn As String = "Public Shared Function fnGet_Calc_DBQuery(String, [oracleConnection], [oracleTransaction]) As String"
            Try

                If ro_DbCn Is Nothing Then ro_DbCn = GetDbConnection()

                Dim dbCmd As New OracleCommand
                Dim objDAdapter As OracleDataAdapter
                Dim dt As New DataTable

                dbCmd.Connection = ro_DbCn
                If ro_DbTrans IsNot Nothing Then dbCmd.Transaction = ro_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = "SELECT '1' FROM DUAL WHERE " + rsCalcForm

                objDAdapter = New OracleDataAdapter(dbCmd)

                dt.Reset()
                objDAdapter.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '검사별 소견 결과 조회
        Public Shared Function fnGet_Rst_Comment_test(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Rst_Comment_test(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT r.bcno, r.partslip, r.slipnmd, fn_ack_get_bcno_comment_test(r.bcno, r.partslip) cmtcont, 'S' status"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT r.bcno, f.partcd || f.slipcd partslip, f2.slipnmd"
                sSql += "          FROM rr010m r, rr030m r3, rf060m f, rf021m f2"
                sSql += "         WHERE r.bcno   = :bcno"
                sSql += "           AND r.bcno   = r3.bcno"
                sSql += "           AND r.testcd = r3.testcd"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "           AND r.tkdt  >= f.usdt"
                sSql += "           AND r.tkdt  <  f.uedt"
                sSql += "           AND f.partcd = f2.partcd"
                sSql += "           AND f.slipcd = f2.slipcd"
                sSql += "           AND r.tkdt  >= f2.usdt"
                sSql += "           AND r.tkdt  <  f2.uedt"
                sSql += "       ) r"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        ' 검사항목별 결과코드 조회
        Public Shared Function fnGet_test_rstinfo(ByVal rsBcNo As String, Optional ByVal r_DbCn As OracleConnection = Nothing) As DataTable
            Dim sFn As String = "Function fnGet_test_rstinfo(String, [oleDbConnection]) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT b.testcd, b.keypad, b.rstcont, b.grade, b.rstcdseq, b.rstlvl"
                sSql += "  FROM rr010m a, rf083m b"
                sSql += " WHERE a.bcno like :bcno || '%'"
                sSql += "   AND a.testcd = b.testcd"
                sSql += "   AND (a.spccd = b.spccd or b.spccd = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "')"
                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 검사항목별 결과코드 조회
        Public Shared Function fnGet_test_rstinfo_wl(ByVal rsWLuid As String, ByVal rsWLymd As String, ByVal rsWLtitle As String) As DataTable
            Dim sFn As String = "Function fnGet_test_rstinfo(String, string, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       b.testcd, b.keypad, b.rstcont, b.grade, b.rstcdseq, b.rstlvl"
                sSql += "  FROM rrw11m a, rf083m b"
                sSql += " WHERE a.wluid   = :wluid"
                sSql += "   AND a.wlymd   = :wlymd"
                sSql += "   AND a.wltitle = :wltitle"
                sSql += "   AND a.testcd  = b.testcd"
                sSql += "   AND (a.spccd = b.spccd or b.spccd = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "')"
                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"


                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLuid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLuid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLymd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLymd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLtitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLtitle))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 검사항목별 결과코드 조회
        Public Shared Function fnGet_test_rstinfo_wgrp(ByVal rsWkYmd As String, ByVal rsWkCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String) As DataTable
            Dim sFn As String = "Function fnGet_test_rstinfo(String, string, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       b.testcd, b.keypad, b.rstcont, b.grade, b.rstcdseq, b.rstlvl"
                sSql += "  FROM rr010m a, rf083m b"
                sSql += " WHERE a.wkymd    = :wkymd"
                sSql += "   AND a.wkgrpcd  = :wgrpcd"
                sSql += "   AND a.wkno    >= :wknos"
                sSql += "   AND a.wkno    <= :wknoe"
                sSql += "   AND a.testcd  = b.testcd"
                sSql += "   AND (a.spccd = b.spccd or b.spccd = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "')"
                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"


                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkCd))
                alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 검사그룹별 결과코드 조회
        Public Shared Function fnGet_test_rstinfo_tgrp(ByVal rsTGrpCd As String, ByVal rsTkDts As String, ByVal rsTkDtE As String) As DataTable
            Dim sFn As String = "Function fnGet_test_rstinfo_tgrp(String,..) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       b.testcd, b.keypad, b.rstcont, b.grade, b.rstcdseq, b.rstlvl"
                sSql += "  FROM rr010m a, rf083m b"
                sSql += " WHERE a.tkdt    >= :dates"
                sSql += "   AND a.tkdt    <= :datee || '5959'"
                sSql += "   AND (SUBSTR(a.testcd, 1, 5), a.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                sSql += "   AND a.testcd  = b.testcd"
                sSql += "   AND (a.spccd = b.spccd OR b.spccd = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "')"
                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"


                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDts.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDts))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 해당검체번호에 포함된 부서/분야 정보 조회
        Public Shared Function fnGet_SlipInfo_bcno(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "Function fnGet_SlipInfo_bcno"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT f2.partcd || f2.slipcd slipcd, f2.slipnmd, NVL(f2.dispseq, 999) sortkey"
                sSql += "  FROM rr010m r, rf060m f6, rf021m f2"
                sSql += " WHERE r.bcno     = :bcno"
                sSql += "   AND r.testcd   = f6.testcd"
                sSql += "   AND r.spccd    = f6.spccd"
                sSql += "   AND r.tkdt    >= f6.usdt"
                sSql += "   AND r.tkdt    <  f6.uedt"
                sSql += "   AND f6.partcd  = f2.partcd"
                sSql += "   AND f6.slipcd  = f2.slipcd"
                sSql += "   AND r.tkdt    >= f2.usdt"
                sSql += "   AND r.tkdt    < f2.uedt"
                sSql += " ORDER BY sortkey"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand(False)
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '소견결과 조회
        Public Shared Function fnGet_Rst_Comment_slip(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Rst_Comment_slip(String) As DataTablev"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT r.bcno, r.partslip, r.slipnmd, fn_ack_get_bcno_comment_slip(r.bcno, r.partslip) cmtcont, 'S' status"
                sSql += "  FROM (SELECT DISTINCT a.bcno, a.partcd || a.slipcd partslip, b.slipnmd"
                sSql += "          FROM rr040m a, rf021m b"
                sSql += "         WHERE a.bcno   = :bcno"
                sSql += "           AND a.partcd = b.partcd"
                sSql += "           AND a.slipcd = b.slipcd"
                sSql += "           AND a.regdt >= b.usdt"
                sSql += "           AND a.regdt <  b.uedt"
                sSql += "       ) r"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '<<< lhj 20151119 접수 / WL 생성 및 조회 화면에서 검사항목 클릭 시, 이전 결과, 이전 결과일, 처방 일시 뿌려주기.
        Public Shared Function getOrddt(ByVal bcno As String, ByVal regno As String) As DataTable
            Dim sFn As String = "Public Shared Function getOrddt(ByVal bcno As String, ByVal regno As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Try
                sSql += "select  to_date(orddt,'yyyy-mm-dd HH24:mi:ss') as orddt" + vbCrLf
                sSql += " from rj011m " + vbCrLf
                sSql += " where bcno = :bcno" + vbCrLf
                sSql += " and regno = :regno " + vbCrLf

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, bcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, bcno))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, regno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, regno))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 검체의 결과조회
        Public Shared Function fnGet_Result_bcno(ByVal rsBcNo As String, ByVal rsSlipCd As String, ByVal rbBcNoAll As Boolean, ByVal rsTestCds As String, _
                                         ByVal rsWkGrpCd As String, ByVal rsEqCd As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_bcno"

            Try
                Dim sSql As String = ""

                sSql = "pkg_ack_rst.pkg_get_result_bcno_r"

                Dim o_Parm As New DBORA.DbParrameter

                With o_Parm
                    .AddItem("rs_bcno", OracleDbType.Varchar2, ParameterDirection.Input, rsBcNo.Substring(0, 14))
                    .AddItem("rs_slipcd", OracleDbType.Varchar2, ParameterDirection.Input, rsSlipCd)
                End With

                DbCommand(False)

                Dim dt As DataTable = DbExecuteQuery(sSql, o_Parm, False)

                Dim a_dr As DataRow()

                If rsEqCd <> "" Then
                    a_dr = dt.Select("eqcd = '" + rsEqCd + "'", "sort1, sort2, tclscd, sort3, testcd")
                ElseIf rbBcNoAll = False Then
                    sSql = ""

                    If rsTestCds <> "" Then
                        sSql += " testspc IN ('" + rsTestCds.Replace(",", "','") + "')"
                    ElseIf rsSlipCd <> "" Then
                        sSql = "slipcd = '" + rsSlipCd + "'"
                        If rsWkGrpCd <> "" Then sSql += " AND wkgrpcd = '" + rsWkGrpCd + "'"
                    End If

                    a_dr = dt.Select(sSql, "sort1, sort2, tclscd, sort3, testcd")
                Else
                    a_dr = dt.Select("", "sort1, sort2, tclscd, sort3, testcd")
                End If

                dt = Fn.ChangeToDataTable(a_dr)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- w/l별 결과 결과조회
        Public Shared Function fnGet_Result_wl(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsRstNullReg As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_wl"

            Try
                Dim sSql As String = ""
                Dim o_Parm As New DBORA.DbParrameter

                With o_Parm
                    .AddItem("rs_wluid", OracleDbType.Varchar2, ParameterDirection.Input, rsWLUid)
                    .AddItem("rs_wlymd", OracleDbType.Varchar2, ParameterDirection.Input, rsWLYmd)
                    .AddItem("rs_wltitle", OracleDbType.Varchar2, ParameterDirection.Input, rsWLTitle)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery("pkg_ack_rst.pkg_get_result_wl_r", o_Parm, False)

                Dim a_dr As DataRow()

                Select Case rsRstNullReg
                    Case "000"

                    Case "001"
                        sSql += "rstflg = '3'"
                    Case "010"
                        sSql += "rstflg < '3'"
                    Case "011"
                        sSql += "rstflg > '0'"
                    Case "100"
                        sSql += "rstflg = '0'"
                    Case "101"
                        sSql += "(rstflg = '0' OR rstflg = '3')"
                    Case "110"
                        sSql += "(rstflg = '0' OR rstflg < '3')"
                    Case "111"

                End Select

                a_dr = dt.Select(sSql, "workno, sort1, sort2, tclscd, sort3, testcd")
                dt = Fn.ChangeToDataTable(a_dr)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 작업번호별 결과 결과조회
        Public Shared Function fnGet_Result_wgrp(ByVal rsWkYmd As String, ByVal rsWkCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                                 ByVal rsTestCds As String, ByVal rsRstNullReg As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_wgrp"

            Try
                Dim sSql As String = ""
                Dim o_Parm As New DBORA.DbParrameter

                With o_Parm
                    .AddItem("rs_wkymd", OracleDbType.Varchar2, ParameterDirection.Input, rsWkYmd)
                    .AddItem("rs_wkgrp", OracleDbType.Varchar2, ParameterDirection.Input, rsWkCd)
                    .AddItem("rs_wknos", OracleDbType.Varchar2, ParameterDirection.Input, rsWkNoS)
                    .AddItem("rs_wknoe", OracleDbType.Varchar2, ParameterDirection.Input, rsWkNoE)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery("pkg_ack_rst.pkg_get_result_wgrp_r", o_Parm, False)

                Dim a_dr As DataRow()

                Select Case rsRstNullReg
                    Case "000"

                    Case "001"
                        sSql += "rstflg = '3'"
                    Case "010"
                        sSql += "rstflg < '3'"
                    Case "011"
                        sSql += "rstflg > '0'"
                    Case "100"
                        sSql += "rstflg = '0'"
                    Case "101"
                        sSql += "(rstflg = '0' OR rstflg = '3')"
                    Case "110"
                        sSql += "(rstflg = '0' OR rstflg < '3')"
                    Case "111"

                End Select

                If rsTestCds <> "" Then sSql += IIf(sSql = "", "", " AND ").ToString + "testcd IN ('" + rsTestCds.Replace(",", "','") + "')"

                a_dr = dt.Select(sSql, "workno, sort1, sort2, tclscd, sort3, testcd")
                dt = Fn.ChangeToDataTable(a_dr)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 검사그룹별 
        Public Shared Function fnGet_Result_tgrp(ByVal rsTGrpCd As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, _
                                                 ByVal rsTestCds As String, ByVal rsRstNullReg As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_tgrp"

            Try
                Dim sSql As String = ""
                Dim o_Parm As New DBORA.DbParrameter

                With o_Parm
                    .AddItem("rs_tkdts", OracleDbType.Varchar2, ParameterDirection.Input, rsTkDtS)
                    .AddItem("rs_tkdte", OracleDbType.Varchar2, ParameterDirection.Input, rsTkDtE)
                    .AddItem("rs_tgrpcd", OracleDbType.Varchar2, ParameterDirection.Input, rsTGrpCd)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery("pkg_ack_rst.pkg_get_result_tgrp_r", o_Parm, False)

                Dim a_dr As DataRow()

                Select Case rsRstNullReg
                    Case "000"

                    Case "001"
                        sSql += "rstflg = '3'"
                    Case "010"
                        sSql += "rstflg < '3'"
                    Case "011"
                        sSql += "rstflg > '0'"
                    Case "100"
                        sSql += "rstflg = '0'"
                    Case "101"
                        sSql += "(rstflg = '0' OR rstflg = '3')"
                    Case "110"
                        sSql += "(rstflg = '0' OR rstflg < '3')"
                    Case "111"

                End Select

                If rsTestCds <> "" Then sSql += IIf(sSql = "", "", " AND ").ToString + "testcd IN ('" + rsTestCds.Replace(",", "','") + "')"

                a_dr = dt.Select(sSql, "workno, sort1, sort2, tclscd, sort3, testcd")
                dt = Fn.ChangeToDataTable(a_dr)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 환자정보 조회(AxRstPatInfo)
        Public Shared Function fnGet_PatInfo(ByVal rsBcNo As String, ByVal rsSlipCd As String) As DataTable
            Dim sFn As String = "Public Shared Function FindDiagNm(String, String) As String"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.regno, j.sex, j.age,"
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm, j.deptcd,"
                sSql += "       j.iogbn, fn_ack_get_ward_abbr(j.wardno) wardno, j.roomno,"
                sSql += "       fn_ack_date_str(j.entdt, 'yyyy-mm-dd') entdt,"
                sSql += "       CASE WHEN j.statgbn = '1' THEN 'Y' ELSE j.statgbn END statgbn,"
                sSql += "       j2.height, j2.weight,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi:ss') colldt,"
                sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       fn_ack_date_str(j1.rstdt, 'yyyy-mm-dd hh24:mi:ss') rstdt,"
                sSql += "       f3.spcnmd, j3.diagnm,"
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM rj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       (SELECT abo || rh FROM lr070m WHERE regno = j.regno) aborh,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       CASE WHEN LENGTH(r.workno) = 8 THEN '' ELSE fn_ack_get_bcno_full(r.workno) END workno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       r.tat_mi"
                sSql += "  FROM rj010m j, rj011m j1, rj012m j2, rj013m j3, lf030m f3,"
                sSql += "       (SELECT bcno, MAX(wkymd || NVL(wkgrpcd, '') || NVL(wkno, '')) workno,"
                sSql += "               fn_ack_date_diff(MIN(NVL(wkdt, tkdt)), MIN(NVL(rstdt, fn_ack_sysdate)), '0') tat_mi"
                sSql += "          FROM rr010m"
                sSql += "         WHERE bcno LIKE :bcno || '%'"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                If rsSlipCd <> "" Then
                    sSql += "           AND (testcd, spccd) IN (SELECT testcd, spccd FROM rf060m WHERE partcd = :partcd AND slipcd = :slipcd)"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If

                sSql += "         GROUP BY bcno"
                sSql += "       ) r"
                sSql += " WHERE j.bcno     = :bcno"
                sSql += "   AND j.bcno     = j1.bcno"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j1.colldt >= f3.usdt"
                sSql += "   AND j1.colldt <  f3.uedt"
                sSql += "   AND j.spcflg   = '4'"
                sSql += "   AND j.bcno     = j2.bcno (+)"
                sSql += "   AND j.bcno     = j3.bcno (+)"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

    End Class

    '-- 계산식
    Public Class CalcFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.CalcFn" + vbTab

        Public Shared Function fnGet_CFCompute(ByVal rsCalForm As String) As String
            Dim sFn As String = "Function fnGet_CFCompute(string) As string"
            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT " + rsCalForm + " FROM DUAL"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql)
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    fnGet_CFCompute = ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_CalcTests(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable

            Dim sFn As String = "Function fnGet_CalcTests(string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "rr010m"

                If COMMON.CommLogin.LOGIN.PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       a.testcd, a.spccd, a.calrange, b.tnmd, c.spcnmd,"
                sSql += "       a.paramcnt, a.param0, a.param1, a.param2, a.param3,"
                sSql += "       a.param4, a.param5, a.param6, a.param7, a.param8,"
                sSql += "       a.param9, a.calform"
                sSql += "  FROM rf069m a, rf060m b, lf030m c"
                sSql += " WHERE a.testcd   = :testcd"
                sSql += "   AND a.spccd    = :spccd"
                sSql += "   AND a.calrange = 'B'"
                sSql += "   AND a.testcd   = b.testcd"
                sSql += "   AND a.spccd    = b.spccd"
                sSql += "   AND b.usdt    <= (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd)"
                sSql += "   AND b.uedt    >  (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd)"
                sSql += "   AND b.spccd    = c.spccd"
                sSql += "   AND c.usdt    <= (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd)"
                sSql += "   AND c.uedt    >  (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd)"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

    '-- 소견자동변환
    Public Class CvtCmt
        Private Const msFile As String = "File : CGLISAPP_COMM.vb, Class : LISAPP.COMM.CvtCmt" + vbTab

        Public Shared Function fnCvtCmtInfo(ByVal rsBcNo As String, ByVal ra_Rst As ArrayList, ByVal rsSlipCd As String, Optional ByVal rbLisMode As Boolean = False, _
                                    Optional ByVal ro_DbCn As OracleConnection = Nothing, _
                                    Optional ByVal ro_DbTran As OracleTransaction = Nothing) As ArrayList
            Dim sFn As String = "Public Shared Function fnCvtCmtInfo(String, ArrayList, String, [Boolean], [oracleConnection], [oracleTransaction]) As ArrayList"

            Dim dbCn As OracleConnection = ro_DbCn
            Dim dbTran As OracleTransaction = ro_DbTran

            Try
                If ro_DbCn Is Nothing Then dbCn = GetDbConnection()

                Dim alRet As New ArrayList
                Dim alCvtInfo As ArrayList = fnGet_CvtCmtInfo(rsBcNo, rsSlipCd, dbCn, dbTran)
                If alCvtInfo.Count < 1 Then Return New ArrayList

                For ix As Integer = 0 To alCvtInfo.Count - 1
                    Dim alCvtInfo_Item As ArrayList = fnGet_CvtCmtInfo_Items(rsBcNo, CType(alCvtInfo(ix), STU_CvtCmtInfo).CmtCd, dbCn, dbTran)
                    Dim iConvCnt As Integer = 0

                    If alCvtInfo_Item.Count > 0 Then

                        For ix1 As Integer = 0 To alCvtInfo_Item.Count - 1
                            For ix2 As Integer = 0 To ra_Rst.Count - 1
                                If CType(ra_Rst(ix2), STU_CvtCmtInfo).TestCd = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).TestCd Then
                                    CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).OrgRst = CType(ra_Rst(ix2), STU_CvtCmtInfo).OrgRst
                                    CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).ViewRst = CType(ra_Rst(ix2), STU_CvtCmtInfo).ViewRst
                                    CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).EqFlag = CType(ra_Rst(ix2), STU_CvtCmtInfo).EqFlag
                                    Exit For
                                End If
                            Next
                        Next

                        For ix1 As Integer = 0 To alCvtInfo_Item.Count - 1
                            If CType(alCvtInfo(ix), STU_CvtCmtInfo).CmtCd = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CmtCd And CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).OrgRst <> "" Then
                                CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp.Replace("[ro]", CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).OrgRst)
                                CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp.Replace("[rv]", CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).ViewRst)

                                CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp.Replace("{ro}", "'" + CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).OrgRst + "'")
                                CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp.Replace("{rv}", "'" + CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).ViewRst + "'")
                                CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp.Replace("{rj}", "'" + CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).HlMark + "'")
                                CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp.Replace("{re}", "'" + CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).EqFlag + "'")

                                CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm = CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm.Replace("[" + CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CvtParam + "]", CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).CondiExp)

                                iConvCnt += 1
                            End If
                        Next

                        CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm += " "

                        '< 2010-03-23 yjlee mod 
                        '< 설정식의 or 조건이 검체가 다를 경우 파라미터 문자를 대체할수 없어서 에러남. 수정
                        For ii As Integer = 1 To 26
                            Dim sAcs As Char = Convert.ToChar(64 + ii)

                            If CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm.IndexOf(sAcs) > -1 Then
                                CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm = CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm.Replace("[" + sAcs + "]", "'1' <> '1'")
                            End If
                        Next
                        '> 2010-03-23 yjlee mod 

                        CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm = CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm.Replace("$$", "AND").Replace("||", "OR")

                        Dim sSql As String = ""

                        If CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm <> "" Then
                            Try
                                Dim dt As DataTable = DbExecuteQuery("SELECT CASE WHEN " + CType(alCvtInfo(ix), STU_CvtCmtInfo).CvtForm + " THEN '1' ELSE '0' END rst FROM DUAL", dbCn, dbTran)
                                If dt.Rows.Count > 0 Then
                                    If dt.Rows(0).Item("rst").ToString = "1" Then
                                        Dim objRet As New STU_CvtCmtInfo

                                        objRet.BcNo = CType(alCvtInfo(ix), STU_CvtCmtInfo).BcNo
                                        objRet.CmtCd = CType(alCvtInfo(ix), STU_CvtCmtInfo).CmtCd
                                        objRet.CmtCont = CType(alCvtInfo(ix), STU_CvtCmtInfo).CmtCont
                                        objRet.SlipCd = CType(alCvtInfo(ix), STU_CvtCmtInfo).SlipCd
                                        objRet.CmtCont_Base = ""

                                        alRet.Add(objRet)
                                    End If
                                ElseIf rbLisMode = False Then
                                    Dim objRet As New STU_CvtCmtInfo

                                    objRet.BcNo = CType(alCvtInfo(ix), STU_CvtCmtInfo).BcNo
                                    objRet.CmtCd = CType(alCvtInfo(ix), STU_CvtCmtInfo).CmtCd
                                    objRet.CmtCont = ""
                                    objRet.CmtCont_Base = CType(alCvtInfo(ix), STU_CvtCmtInfo).CmtCont
                                    objRet.SlipCd = CType(alCvtInfo(ix), STU_CvtCmtInfo).SlipCd

                                    alRet.Add(objRet)
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                    End If
                Next

                Return alRet

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If ro_DbCn Is Nothing Then
                    If dbTran IsNot Nothing Then dbTran.Dispose() : dbTran = Nothing

                    If dbCn.State = ConnectionState.Open Then dbCn.Close()
                    dbCn.Dispose() : dbCn = Nothing
                End If
            End Try

        End Function

        Private Shared Function fnGet_CvtCmtInfo(ByVal rsBcNo As String, ByVal rsSlipCd As String, ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnGet_CvtCmtInfo(String) As Boolean"

            Try
                Dim dt As DataTable = RISAPP.COMM.CvtCmt.fnGet_CvtCmt_State_BcNo(rsBcNo, rsSlipCd, ro_DbCn, ro_DbTrans)
                Dim aryList As New ArrayList

                Dim bExist As Boolean = False

                If dt.Rows.Count < 1 Then Return New ArrayList

                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(intIdx).Item("minrstflg").ToString.Trim > "2" Then
                    Else
                        Dim objCvt As New STU_CvtCmtInfo

                        objCvt.BcNo = dt.Rows(intIdx).Item("bcno").ToString.Trim
                        objCvt.CmtCd = dt.Rows(intIdx).Item("cmtcd").ToString.Trim
                        objCvt.CvtForm = dt.Rows(intIdx).Item("cvtform").ToString.Trim
                        objCvt.CmtCont = dt.Rows(intIdx).Item("cmtcont").ToString.Trim
                        objCvt.SlipCd = dt.Rows(intIdx).Item("slipcd").ToString.Trim

                        aryList.Add(objCvt)
                    End If
                Next

                Return aryList

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Private Shared Function fnGet_CvtCmtInfo_Items(ByVal rsBcNo As String, ByVal rsCmtCd As String, ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnGet_CvtCmtInfo_Items(string, string, string) As ArrayList"

            Try
                Dim dt As DataTable
                dt = RISAPP.COMM.CvtCmt.fnGet_CvtCmtInfo_BcNo(rsBcNo, rsCmtCd, ro_DbCn, ro_DbTrans)

                Dim aryList As New ArrayList

                If dt.Rows.Count < 1 Then Return New ArrayList

                For ix As Integer = 0 To dt.Rows.Count - 1
                    Dim objCvt As New STU_CvtCmtInfo

                    objCvt.CmtCd = dt.Rows(ix).Item("cmtcd").ToString.Trim
                    objCvt.CvtParam = dt.Rows(ix).Item("cvtparam").ToString.Trim
                    objCvt.TestCd = dt.Rows(ix).Item("testcd").ToString.Trim
                    objCvt.OrgRst = dt.Rows(ix).Item("orgrst").ToString.Trim
                    objCvt.ViewRst = dt.Rows(ix).Item("viewrst").ToString.Trim
                    objCvt.EqFlag = dt.Rows(ix).Item("eqflag").ToString.Trim
                    objCvt.HlMark = dt.Rows(ix).Item("hlmark").ToString.Trim
                    objCvt.BcNo = dt.Rows(ix).Item("bcno").ToString.Trim

                    Dim strCalcL As String = ""
                    Dim strCalcH As String = ""
                    Dim strCalcC As String = ""

                    If dt.Rows(ix).Item("refl").ToString.Trim <> "" Then
                        Select Case dt.Rows(ix).Item("reflgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(ix).Item("refls").ToString
                                    Case "0" : strCalcL = "[ro] > " + dt.Rows(ix).Item("refl").ToString.Trim
                                    Case "1" : strCalcL = "[ro] >= " + dt.Rows(ix).Item("refl").ToString.Trim
                                End Select
                            Case "2"
                                Select Case dt.Rows(ix).Item("refls").ToString.Trim
                                    Case "0" : strCalcL = "[rv] > " + dt.Rows(ix).Item("refl").ToString.Trim
                                    Case "1" : strCalcL = "[rv] >= " + dt.Rows(ix).Item("refl").ToString.Trim
                                End Select
                        End Select
                    End If

                    If dt.Rows(ix).Item("refh").ToString.Trim <> "" Then
                        Select Case dt.Rows(ix).Item("refhgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(ix).Item("refhs").ToString
                                    Case "0" : strCalcH = "[ro] < " + dt.Rows(ix).Item("refh").ToString.Trim
                                    Case "1" : strCalcH = "[ro] <= " + dt.Rows(ix).Item("refh").ToString.Trim
                                    Case "2"
                                        strCalcH = "[ro] = " + dt.Rows(ix).Item("refh").ToString.Trim
                                        strCalcL = ""
                                End Select
                            Case "2"
                                Select Case dt.Rows(ix).Item("refhs").ToString.Trim
                                    Case "0" : strCalcH = "[rv] < " + dt.Rows(ix).Item("refh").ToString.Trim
                                    Case "1" : strCalcH = "[rv] <= " + dt.Rows(ix).Item("refh").ToString.Trim
                                    Case "2"
                                        strCalcH = "[rv] = " + dt.Rows(ix).Item("refh").ToString.Trim
                                        strCalcL = ""
                                End Select
                        End Select
                    End If

                    If dt.Rows(ix).Item("reflt").ToString.Trim <> "" Then
                        strCalcL = "" : strCalcH = ""
                        Select Case dt.Rows(ix).Item("refhgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(ix).Item("reflts").ToString.Trim
                                    Case "0" : strCalcC = "{ro} = '" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                    Case "1" : strCalcC = "{ro} like '" + dt.Rows(ix).Item("reflt").ToString.Trim + "%'"
                                    Case "2" : strCalcC = "{ro} like '%" + dt.Rows(ix).Item("reflt").ToString.Trim + "%'"
                                    Case "3" : strCalcC = "{ro} like '%" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                    Case "4" : strCalcC = "{ro} <> '" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                End Select
                            Case "2"
                                Select Case dt.Rows(ix).Item("reflts").ToString
                                    Case "0" : strCalcC = "{rv} = '" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                    Case "1" : strCalcC = "{rv} like '" + dt.Rows(ix).Item("reflt").ToString.Trim + "%'"
                                    Case "2" : strCalcC = "{rv} like '%" + dt.Rows(ix).Item("reflt").ToString.Trim + "%'"
                                    Case "3" : strCalcC = "{rv} like '%" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                    Case "4" : strCalcC = "{rv} <> '" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                End Select
                            Case "3"
                                strCalcH = "{rj} = '" + dt.Rows(ix).Item("refh").ToString.Trim + "'"
                                strCalcL = ""

                            Case "4"
                                Select Case dt.Rows(ix).Item("reflts").ToString
                                    Case "0" : strCalcC = "{re} = '" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                    Case "1" : strCalcC = "{re} like '" + dt.Rows(ix).Item("reflt").ToString.Trim + "%'"
                                    Case "2" : strCalcC = "{re} like '%" + dt.Rows(ix).Item("reflt").ToString.Trim + "%'"
                                    Case "3" : strCalcC = "{re} like '%" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                    Case "4" : strCalcC = "{re} <> '" + dt.Rows(ix).Item("reflt").ToString.Trim + "'"
                                End Select
                        End Select
                    End If

                    objCvt.CondiExp = ""
                    If strCalcC <> "" Then
                        objCvt.CondiExp = strCalcC
                    Else
                        If strCalcL <> "" And strCalcH <> "" Then
                            objCvt.CondiExp = "(" + strCalcL + " and " + strCalcH + ")"
                        ElseIf strCalcL <> "" Then
                            objCvt.CondiExp = strCalcL
                        ElseIf strCalcH <> "" Then
                            objCvt.CondiExp = strCalcH
                        End If
                    End If

                    aryList.Add(objCvt)
                Next

                Return aryList

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function


        Public Shared Function fnGet_CvtCmt_State_BcNo(ByVal rsBcNo As String, ByVal rsSlipCd As String, ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction, Optional ByVal rbAuto As Boolean = False) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtCmt_State_BcNo(String, (Boolean), (Object)) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT r.bcno, c.cmtcd, b.cvtform, d.partcd || d.slipcd slipcd, d.cmtcont, MIN(NVL(r.rstflg, '0')) minrstflg"
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lm010m r, lf081m b, lf082m c, lf080m d"
                Else
                    sSql += "  FROM rr010m r, rf081m b, rf082m c, rf080m d"
                End If
                sSql += " WHERE r.bcno  = :bcno"
                sSql += "   AND r.testcd = c.testcd"
                sSql += "   AND r.spccd  = c.spccd"
                sSql += "   AND b.cmtcd  = c.cmtcd"
                sSql += "   AND c.cmtcd  = d.cmtcd"

                If rsSlipCd <> "" Then
                    sSql += "   AND d.partcd || d.slipcd = :partslip"
                End If
                sSql += " GROUP BY r.bcno, c.cmtcd, b.cvtform, d.partcd, d.slipcd, d.cmtcont"

                Dim dbCmd As New OracleCommand
                Dim objDAdapter As OracleDataAdapter
                Dim dt As New DataTable

                dbCmd.Connection = ro_DbCn
                If ro_DbTrans IsNot Nothing Then dbCmd.Transaction = ro_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                objDAdapter = New OracleDataAdapter(dbCmd)

                objDAdapter.SelectCommand.Parameters.Clear()
                objDAdapter.SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                If rsSlipCd <> "" Then
                    objDAdapter.SelectCommand.Parameters.Add("partslip", OracleDbType.Varchar2).Value = rsSlipCd
                End If
                dt.Reset()
                objDAdapter.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_CvtCmtInfo_BcNo(ByVal rsBcNo As String, ByVal rsCmtCd As String, _
                                                     ByVal ro_DbCn As OracleConnection, _
                                                     ByVal ro_DbTrans As OracleTransaction) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtCmtInfo_BcNo(String, (Object)) As DataTable"

            Try
                Dim sSql As String

                sSql = ""
                sSql += "SELECT r.bcno, c.cmtcd, c.cvtparam, c.testcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "       f.tnmd, r.orgrst, r.viewrst, r.hlmark, r.rstflg, r.eqflag"
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lm010m r, rf082m c, rf060m f"
                Else
                    sSql += "  FROM rr010m r, rf082m c, rf060m f"
                End If
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND c.cmtcd  = :cmtcd"
                sSql += "   AND r.testcd = c.testcd"
                sSql += "   AND r.spccd  = c.spccd"
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd"
                sSql += "   AND r.tkdt  >= f.usdt"
                sSql += "   AND r.tkdt  <  f.uedt"

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                dbCmd.Connection = ro_DbCn
                If ro_DbTrans IsNot Nothing Then dbCmd.Transaction = ro_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .SelectCommand.Parameters.Add("cmtcd", OracleDbType.Varchar2).Value = rsCmtCd
                End With

                dt.Reset()
                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

    End Class

    '-- 결과값 자동변환
    Public Class CvtRst
        Private Const msFile As String = "File : CGLISAPP_COMM.vb, Class : LISAPP.COMM.CvtRst" + vbTab

        Public Shared Function fnCvtRstInfo(ByVal rsBcNo As String, ByVal raRst As ArrayList, Optional ByVal rbIFGbn As Boolean = False, _
                                    Optional ByVal ro_DbCn As OracleConnection = Nothing, _
                                    Optional ByVal ro_DbTran As OracleTransaction = Nothing) As ArrayList
            Dim sFn As String = "Public Shared Function fnCvtRstInfo(String, ArrayList, [Boolean], [oracleConnection], [oracleTransaction]) As ArrayList"

            Dim dbCn As OracleConnection = ro_DbCn
            Dim dbTran As OracleTransaction = ro_DbTran

            Try

                If ro_DbCn Is Nothing Then dbCn = GetDbConnection()

                Dim alReturn As New ArrayList

                Dim alCvtInfo As ArrayList = fnGet_CvtRstInfo(rsBcNo, rbIFGbn, dbCn, dbTran)
                If alCvtInfo.Count < 1 Then Return New ArrayList

                For ix As Integer = 0 To alCvtInfo.Count - 1
                    Dim alCvtInfo_Item As ArrayList = fnGet_CvtRstInfo_Items(CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtRange, rsBcNo, CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd, CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd, CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCdSeq, CType(alCvtInfo(ix), STU_RstInfo_cvt).OrgRst, dbCn, dbTran)
                    If alCvtInfo_Item.Count > 0 Then

                        For ix1 As Integer = 0 To alCvtInfo_Item.Count - 1
                            For ix2 As Integer = 0 To raRst.Count - 1
                                If (CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CTestCd) = CType(raRst(ix2), STU_RstInfo_cvt).TestCd Then
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst = CType(raRst(ix2), STU_RstInfo_cvt).OrgRst
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).ViewRst = CType(raRst(ix2), STU_RstInfo_cvt).ViewRst
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).HlMark = CType(raRst(ix2), STU_RstInfo_cvt).HlMark

                                    Exit For
                                End If
                            Next
                        Next

                        Dim bAction As Boolean = False

                        For ix1 As Integer = 0 To alCvtInfo_Item.Count - 1
                            If CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).TestCd And _
                               CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).SpcCd Then
                                If CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst <> "" Or CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtRange = "R" Then
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("[ro]", CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst)
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("[rv]", CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).ViewRst)

                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{ro}", "'" + CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst + "'")
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{rv}", "'" + CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).ViewRst + "'")
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{rj}", "'" + CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).HlMark + "'")

                                    CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm.Replace("[" + CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CvtParam + "]", CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp)

                                    bAction = True
                                End If
                            End If
                        Next

                        If bAction = False Then Return alReturn

                        For ix1 = 65 To 90
                            CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm.Replace("[" + Chr(ix1) + "]", "2 = 1")
                        Next

                        CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm.Replace("$$", "AND").Replace("||", "OR")

                        Dim sSql As String = ""
                        Dim dt As New DataTable
                        Try
                            dt = DbExecuteQuery("SELECT CASE WHEN " + CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm + " THEN '1' ELSE '0' END rst FROM DUAL", dbCn, dbTran)
                            If dt.Rows.Count > 0 Then
                                If dt.Rows(0).Item("rst").ToString = "1" Then
                                    Dim objRet As New STU_RstInfo_cvt

                                    objRet.TestCd = CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd
                                    objRet.SpcCd = CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd
                                    objRet.BcNo = CType(alCvtInfo(ix), STU_RstInfo_cvt).BcNo
                                    objRet.CvtFldGbn = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtFldGbn
                                    objRet.RstFlg = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstFlg

                                    If CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtRange = "R" Then
                                        objRet.OrgRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCont
                                    End If

                                    If CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtFldGbn = "R" Then
                                        objRet.ViewRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCont
                                    Else
                                        objRet.RstCmt = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCont
                                    End If

                                    alReturn.Add(objRet)
                                Else
                                    If Not rbIFGbn Then
                                        Dim objRet As New STU_RstInfo_cvt

                                        objRet.TestCd = CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd
                                        objRet.SpcCd = CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd
                                        objRet.BcNo = CType(alCvtInfo(ix), STU_RstInfo_cvt).BcNo
                                        objRet.CvtFldGbn = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtFldGbn
                                        objRet.RstFlg = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstFlg
                                        objRet.OrgRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).OrgRst

                                        If CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtFldGbn = "R" Then
                                            objRet.ViewRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).ViewRst
                                        Else
                                            objRet.RstCmt = ""
                                        End If

                                        alReturn.Add(objRet)

                                    End If
                                End If
                            End If
                        Catch ex As Exception
                        End Try

                    End If
                Next

                Return alReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                If ro_DbCn Is Nothing Then
                    If dbTran IsNot Nothing Then dbTran.Dispose() : dbTran = Nothing

                    If dbCn.State = ConnectionState.Open Then dbCn.Close()
                    dbCn.Dispose() : dbCn = Nothing
                End If
            End Try

        End Function

        Public Shared Function fnCvtRstInfo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal raRst As ArrayList, Optional ByVal rbIFGbn As Boolean = False, _
                                    Optional ByVal ro_DbCn As OracleConnection = Nothing, _
                                    Optional ByVal ro_DbTran As OracleTransaction = Nothing) As ArrayList
            Dim sFn As String = "Public Shared Function fnCvtRstInfo(String, ArrayList, [Boolean], [oracleConnection], [oracleTransaction]) As ArrayList"

            Dim dbCn As OracleConnection = ro_DbCn
            Dim dbTran As OracleTransaction = ro_DbTran

            Try

                If dbCn Is Nothing Then dbCn = GetDbConnection()

                Dim alReturn As New ArrayList

                Dim alCvtInfo As ArrayList = fnGet_CvtRstInfo(rsBcNo, rsTestCd, rbIFGbn, dbCn, dbTran)
                If alCvtInfo.Count < 1 Then Return New ArrayList

                For ix1 As Integer = 0 To alCvtInfo.Count - 1
                    For ix2 As Integer = 0 To raRst.Count - 1
                        If CType(alCvtInfo(ix1), STU_RstInfo_cvt).TestCd = CType(raRst(ix2), STU_RstInfo_cvt).TestCd Then
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).OrgRst = CType(raRst(ix2), STU_RstInfo_cvt).OrgRst
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).ViewRst = CType(raRst(ix2), STU_RstInfo_cvt).ViewRst
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).HlMark = CType(raRst(ix2), STU_RstInfo_cvt).HlMark
                            Exit For
                        End If
                    Next
                Next

                For ix As Integer = 0 To alCvtInfo.Count - 1
                    Dim alCvtInfo_Item As ArrayList = fnGet_CvtRstInfo_Items(CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtRange, rsBcNo, CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd, CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd, CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCdSeq, CType(alCvtInfo(ix), STU_RstInfo_cvt).OrgRst, dbCn, dbTran)
                    If alCvtInfo_Item.Count > 0 Then

                        Dim bAction As Boolean = False

                        For ix1 As Integer = 0 To alCvtInfo_Item.Count - 1
                            If CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).TestCd And _
                               CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).SpcCd Then
                                If CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst <> "" Or CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtRange = "R" Then
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("[ro]", CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst)
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("[rv]", CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).ViewRst)

                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{ro}", "'" + CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst + "'")
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{rv}", "'" + CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).ViewRst + "'")
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{rj}", "'" + CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).HlMark + "'")

                                    CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm.Replace("[" + CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CvtParam + "]", CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CondiExp)

                                    bAction = True
                                End If
                            End If
                        Next

                        If bAction = False Then Return alReturn

                        For ix1 = 65 To 90
                            CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm.Replace("[" + Chr(ix1) + "]", "2 = 1")
                        Next

                        CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm.Replace("$$", "AND").Replace("||", "OR")

                        Dim sSql As String = ""
                        Dim dt As New DataTable
                        Try
                            dt = DbExecuteQuery("SELECT CASE WHEN " + CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtForm + " THEN '1' ELSE '0' END rst FROM DUAL", dbCn, dbTran)
                            If dt.Rows.Count > 0 Then
                                If dt.Rows(0).Item("rst").ToString = "1" Then
                                    Dim objRet As New STU_RstInfo_cvt

                                    objRet.TestCd = CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd
                                    objRet.SpcCd = CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd
                                    objRet.BcNo = CType(alCvtInfo(ix), STU_RstInfo_cvt).BcNo
                                    objRet.CvtFldGbn = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtFldGbn
                                    objRet.RstFlg = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstFlg

                                    If CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtRange = "R" Then
                                        objRet.OrgRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCont
                                    Else
                                        objRet.OrgRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).OrgRst
                                    End If

                                    If CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtFldGbn = "R" Then
                                        objRet.ViewRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCont
                                    Else
                                        objRet.RstCmt = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCont
                                    End If

                                    alReturn.Add(objRet)
                                    Exit For
                                Else
                                    If Not rbIFGbn Then
                                        Dim objRet As New STU_RstInfo_cvt

                                        objRet.TestCd = CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd
                                        objRet.SpcCd = CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd
                                        objRet.BcNo = CType(alCvtInfo(ix), STU_RstInfo_cvt).BcNo
                                        objRet.CvtFldGbn = CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtFldGbn
                                        objRet.RstFlg = CType(alCvtInfo(ix), STU_RstInfo_cvt).RstFlg
                                        objRet.OrgRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).OrgRst

                                        If CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtFldGbn = "R" Then
                                            objRet.ViewRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).ViewRst
                                        Else
                                            objRet.RstCmt = ""
                                        End If

                                        alReturn.Add(objRet)

                                    End If
                                End If
                            End If
                        Catch ex As Exception
                        End Try

                    End If
                Next

                Return alReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                If ro_DbCn Is Nothing Then
                    If dbTran IsNot Nothing Then dbTran.Dispose() : dbTran = Nothing

                    If dbCn.State = ConnectionState.Open Then dbCn.Close()
                    dbCn.Dispose() : dbCn = Nothing
                End If
            End Try

        End Function

        Private Shared Function fnGet_CvtRstInfo(ByVal rsBcNo As String, ByVal rbIFGbn As Boolean, _
                                                 ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnCvtRstInfo_State(String) As Boolean"

            Try
                Dim dt As DataTable = RISAPP.COMM.CvtRst.fnGet_CvtRst_State_BcNo(rsBcNo, rbIFGbn, ro_DbCn, ro_DbTrans)
                Dim aryList As New ArrayList

                Dim bExist As Boolean = False

                If dt.Rows.Count < 1 Then Return New ArrayList

                Dim bFinal As Boolean = False

                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(0).Item("minrstflg").ToString.Trim > "2" Then
                    Else
                        Dim objCvt As New STU_RstInfo_cvt

                        objCvt.BcNo = dt.Rows(intIdx).Item("bcno").ToString.Trim
                        objCvt.TestCd = dt.Rows(intIdx).Item("testcd").ToString.Trim
                        objCvt.SpcCd = dt.Rows(intIdx).Item("spccd").ToString.Trim
                        objCvt.OrgRst = dt.Rows(intIdx).Item("orgrst").ToString.Trim
                        objCvt.RstCmt = dt.Rows(intIdx).Item("rstcmt").ToString.Trim
                        objCvt.RstCdSeq = dt.Rows(intIdx).Item("rstcdseq").ToString.Trim
                        objCvt.CvtForm = dt.Rows(intIdx).Item("cvtform").ToString.Trim
                        objCvt.CvtFldGbn = dt.Rows(intIdx).Item("cvtfldgbn").ToString.Trim
                        objCvt.CvtRange = dt.Rows(intIdx).Item("cvtrange").ToString.Trim
                        objCvt.RstCont = dt.Rows(intIdx).Item("rstcont").ToString.Trim

                        aryList.Add(objCvt)

                    End If
                Next

                Return aryList

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Private Shared Function fnGet_CvtRstInfo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rbIFGbn As Boolean, _
                                                 ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnCvtRstInfo_State(String) As Boolean"

            Try
                Dim dt As DataTable = RISAPP.COMM.CvtRst.fnGet_CvtRst_State_BcNo(rsBcNo, rsTestCd, rbIFGbn, ro_DbCn, ro_DbTrans)
                Dim aryList As New ArrayList

                Dim bExist As Boolean = False

                If dt.Rows.Count < 1 Then Return New ArrayList

                Dim bFinal As Boolean = False

                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(0).Item("minrstflg").ToString.Trim > "2" Then
                    Else
                        Dim objCvt As New STU_RstInfo_cvt

                        objCvt.BcNo = dt.Rows(intIdx).Item("bcno").ToString.Trim
                        objCvt.TestCd = dt.Rows(intIdx).Item("testcd").ToString.Trim
                        objCvt.SpcCd = dt.Rows(intIdx).Item("spccd").ToString.Trim
                        objCvt.OrgRst = dt.Rows(intIdx).Item("orgrst").ToString.Trim
                        objCvt.RstCmt = dt.Rows(intIdx).Item("rstcmt").ToString.Trim
                        objCvt.RstCdSeq = dt.Rows(intIdx).Item("rstcdseq").ToString.Trim
                        objCvt.CvtForm = dt.Rows(intIdx).Item("cvtform").ToString.Trim
                        objCvt.CvtFldGbn = dt.Rows(intIdx).Item("cvtfldgbn").ToString.Trim
                        objCvt.CvtRange = dt.Rows(intIdx).Item("cvtrange").ToString.Trim
                        objCvt.RstCont = dt.Rows(intIdx).Item("rstcont").ToString.Trim

                        aryList.Add(objCvt)

                    End If
                Next

                Return aryList

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Private Shared Function fnGet_CvtRstInfo_Items(ByVal rsRange As String, ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsSpcCd As String, ByVal rsRstSeq As String, ByVal rsOrgRst As String, _
                                                       ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnGet_CvtRstInfo_Items(string, string, string, String) As ArrayList"

            Try
                Dim dt As DataTable = RISAPP.COMM.CvtRst.fnGet_CvtRstInfo_BcNo(rsBcNo, rsTclsCd, rsSpcCd, rsRstSeq, ro_DbCn, ro_DbTrans)
                Dim alList As New ArrayList

                If dt.Rows.Count < 1 Then Return New ArrayList

                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    Dim objCvt As New STU_RstInfo_cvt

                    objCvt.TestCd = dt.Rows(intIdx).Item("testcd").ToString.Trim
                    objCvt.SpcCd = dt.Rows(intIdx).Item("spccd").ToString.Trim
                    objCvt.RstFlg = dt.Rows(intIdx).Item("rstflg").ToString.Trim
                    objCvt.CvtParam = dt.Rows(intIdx).Item("cvtparam").ToString.Trim
                    objCvt.CTestCd = dt.Rows(intIdx).Item("ctestcd").ToString.Trim
                    objCvt.OrgRst = IIf(rsOrgRst = "", dt.Rows(intIdx).Item("orgrst").ToString.Trim, rsOrgRst).ToString
                    objCvt.ViewRst = dt.Rows(intIdx).Item("viewrst").ToString.Trim
                    objCvt.HlMark = dt.Rows(intIdx).Item("hlmark").ToString.Trim
                    objCvt.BcNo = dt.Rows(intIdx).Item("bcno").ToString.Trim

                    Dim sCalcL As String = ""
                    Dim sCalcH As String = ""
                    Dim sCalcC As String = ""

                    If dt.Rows(intIdx).Item("refl").ToString.Trim <> "" Then
                        Select Case dt.Rows(intIdx).Item("reflgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(intIdx).Item("refls").ToString.Trim
                                    Case "0" : sCalcL = "[ro] > " + dt.Rows(intIdx).Item("refl").ToString.Trim
                                    Case "1" : sCalcL = "[ro] >= " + dt.Rows(intIdx).Item("refl").ToString.Trim
                                End Select
                            Case "2"
                                Select Case dt.Rows(intIdx).Item("refls").ToString.Trim
                                    Case "0" : sCalcL = "[rv] > " + dt.Rows(intIdx).Item("refl").ToString.Trim
                                    Case "1" : sCalcL = "[rv] >= " + dt.Rows(intIdx).Item("refl").ToString.Trim
                                End Select
                        End Select
                    End If

                    If dt.Rows(intIdx).Item("refh").ToString.Trim <> "" Then
                        Select Case dt.Rows(intIdx).Item("refhgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(intIdx).Item("refhs").ToString.Trim
                                    Case "0" : sCalcH = "[ro] < " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                    Case "1" : sCalcH = "[ro] <= " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                    Case "2"
                                        sCalcH = "[ro] = " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                        sCalcL = ""
                                End Select
                            Case "2"
                                Select Case dt.Rows(intIdx).Item("refhs").ToString.Trim
                                    Case "0" : sCalcH = "[rv] < " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                    Case "1" : sCalcH = "[rv] <= " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                    Case "2"
                                        sCalcH = "[rv] = " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                        sCalcL = ""
                                End Select
                            Case "3"
                                sCalcH = "{rj} = '" + dt.Rows(intIdx).Item("refh").ToString.Trim + "'"
                                sCalcL = ""
                        End Select
                    End If

                    If dt.Rows(intIdx).Item("reflt").ToString.Trim <> "" Then
                        sCalcL = "" : sCalcH = ""
                        Select Case dt.Rows(intIdx).Item("refhgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(intIdx).Item("reflts").ToString.Trim
                                    Case "0" : sCalcC = "{ro} = '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "1" : sCalcC = "{ro} <> '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "2" : sCalcC = "{ro} like '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%'"
                                    Case "3" : sCalcC = "{ro} like '%" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%'"
                                    Case "4" : sCalcC = "{ro} like '%" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "5" : sCalcC = "{ro} <> '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                End Select
                            Case "2"
                                Select Case dt.Rows(intIdx).Item("reflts").ToString.Trim
                                    Case "0" : sCalcC = "{rv} = '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "1" : sCalcC = "{rv} <> '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "2" : sCalcC = "{rv} like '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%'"
                                    Case "3" : sCalcC = "{rv} like '%" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%'"
                                    Case "4" : sCalcC = "{rv} like '%" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "5" : sCalcC = "{rv} <> '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                End Select
                            Case "3"
                                sCalcC = "{rj} = '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                        End Select
                    End If

                    objCvt.CondiExp = ""
                    If sCalcC <> "" Then
                        objCvt.CondiExp = sCalcC
                    Else
                        If sCalcL <> "" And sCalcH <> "" Then
                            objCvt.CondiExp = "(" + sCalcL + " and " + sCalcH + ")"
                        ElseIf sCalcL <> "" Then
                            objCvt.CondiExp = sCalcL
                        ElseIf sCalcH <> "" Then
                            objCvt.CondiExp = sCalcH
                        End If
                    End If

                    alList.Add(objCvt)
                Next

                Return alList

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_CvtRst_State_BcNo(ByVal rsBcNo As String, ByVal rbAuto As Boolean, _
                                                       ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtRst_State_BcNo(String, Boolean, oracleConnection, oracleTransaction) As DataTable"

            Try

                Dim sSql As String

                sSql = ""
                sSql += "SELECT r.bcno, r.testcd, r.spccd, r.orgrst, r.rstcmt,"
                sSql += "       c.rstcdseq, c.cvtrange, c.cvtform, c.cvtfldgbn, d.rstcont, MIN(NVL(r.rstflg, '0')) minrstflg"
                sSql += "  FROM rr010m r, rf084m c, rf083m d"
                sSql += " WHERE r.bcno     = :bcno"
                sSql += "   AND r.testcd   = c.testcd"
                sSql += "   AND r.spccd    = c.spccd"
                sSql += "   AND c.testcd   = d.testcd"
                sSql += "   AND c.rstcdseq = d.rstcdseq"
                sSql += "   AND NVL(r.rstflg, '0') IN ('0', '1', '2', '3')"

                If rbAuto Then
                    sSql += "   AND NVL(c.cvttype, 'M') = 'A'"
                End If

                sSql += " GROUP BY r.bcno, r.testcd, r.spccd, r.orgrst, r.rstcmt, c.rstcdseq, c.cvtrange, c.cvtform, c.cvtfldgbn, d.rstcont"


                Dim dbCmd As New OracleCommand
                Dim objDAdapter As OracleDataAdapter
                Dim dt As New DataTable

                dbCmd.Connection = ro_DbCn
                If ro_DbTrans IsNot Nothing Then dbCmd.Transaction = ro_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                objDAdapter = New OracleDataAdapter(dbCmd)

                objDAdapter.SelectCommand.Parameters.Clear()
                objDAdapter.SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                dt.Reset()
                objDAdapter.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_CvtRst_State_BcNo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rbAuto As Boolean, _
                                                       ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtRst_State_BcNo(String, Boolean, oracleConnection, oracleTransaction) As DataTable"

            Try

                Dim sSql As String

                sSql = ""
                sSql += "SELECT r.bcno, r.testcd, r.spccd, r.orgrst, r.rstcmt,"
                sSql += "       c.rstcdseq, c.cvtrange, c.cvtform, c.cvtfldgbn, d.rstcont, MIN(NVL(r.rstflg, '0')) minrstflg"
                sSql += "  FROM rr010m r, rf084m c, rf083m d"
                sSql += " WHERE r.bcno     = :bcno"

                If rsTestCd <> "" Then
                    sSql += "   AND r.testcd   = :testcd"

                End If
                sSql += "   AND r.testcd   = c.testcd"
                sSql += "   AND r.spccd    = c.spccd"
                sSql += "   AND c.testcd   = d.testcd"
                sSql += "   AND c.rstcdseq = d.rstcdseq"
                sSql += "   AND NVL(r.rstflg, '0') IN ('0', '1', '2', '3')"

                If rbAuto Then
                    sSql += "   AND NVL(c.cvttype, 'M') = 'A'"
                End If

                sSql += " GROUP BY r.bcno, r.testcd, r.spccd, r.orgrst, r.rstcmt, c.rstcdseq, c.cvtrange, c.cvtform, c.cvtfldgbn, d.rstcont"


                Dim dbCmd As New OracleCommand
                Dim objDAdapter As OracleDataAdapter
                Dim dt As New DataTable

                dbCmd.Connection = ro_DbCn
                If ro_DbTrans IsNot Nothing Then dbCmd.Transaction = ro_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                objDAdapter = New OracleDataAdapter(dbCmd)

                objDAdapter.SelectCommand.Parameters.Clear()
                objDAdapter.SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                If rsTestCd <> "" Then
                    objDAdapter.SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                End If

                dt.Reset()
                objDAdapter.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_CvtRstInfo_BcNo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String, _
                                                     ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtRstInfo_BcNo(String, (Object)) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT r.bcno, c.testcd, c.spccd, c.cvtparam, c.ctestcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "       f.tnmd, r.orgrst, r.viewrst, r.hlmark, MIN(NVL(r.rstflg, '0')) rstflg"
                sSql += "  FROM rr010m r, rf085m c, rf060m f"
                sSql += " WHERE r.bcno     = :bcno"
                sSql += "   AND c.testcd   = :testcd"
                sSql += "   AND c.spccd    = :spccd"
                sSql += "   AND c.rstcdseq = :rstcdseq"
                sSql += "   AND r.testcd   = c.ctestcd"
                sSql += "   AND r.spccd    = c.cspccd"
                sSql += "   AND r.testcd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND r.tkdt    >= f.usdt"
                sSql += "   AND r.tkdt    <  f.uedt"
                sSql += "   AND NVL(r.rstflg, '0') IN ('0', '1', '2', '3')"
                sSql += " GROUP BY r.bcno, c.testcd, c.spccd, c.cvtparam, c.ctestcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "          f.tnmd, r.orgrst, r.viewrst, r.hlmark"

                Dim dbCmd As New OracleCommand
                Dim objDAdapter As OracleDataAdapter
                Dim dt As New DataTable

                dbCmd.Connection = ro_DbCn
                If ro_DbTrans IsNot Nothing Then dbCmd.Transaction = ro_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                objDAdapter = New OracleDataAdapter(dbCmd)

                objDAdapter.SelectCommand.Parameters.Clear()
                objDAdapter.SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                objDAdapter.SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                objDAdapter.SelectCommand.Parameters.Add("spccd", OracleDbType.Varchar2).Value = rsSpcCd
                objDAdapter.SelectCommand.Parameters.Add("rstcdseq", OracleDbType.Varchar2).Value = rsRstCd

                dt.Reset()
                objDAdapter.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_CvtRstInfo_RegNo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String, _
                                                      ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtRstInfo_RegNo(String, string, string, string, [Object]) As DataTable"

            Try
                Dim sSql As String

                sSql = ""
                sSql += "SELECT r.bcno, c.testcd, c.spccd, c.cvtparam, c.ctestcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "       f.tnmd, r.orgrst, r.viewrst, r.hlmark, MIN(NVL(r.rstflg, '0')) rstflg"

                sSql += "  FROM rj010m j, rr010m r, rf085m c, rf060m f,"
                sSql += "       (SELECT regno, orddt FROM rj010m WHERE bcno = :bcno) t"
                sSql += " WHERE j.regno    = t.regno"
                sSql += "   AND j.orddt    = t.orddt"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND r.testcd   = c.testcd"
                sSql += "   AND r.spccd    = c.spccd"
                sSql += "   AND c.testcd   = :testcd"
                sSql += "   AND c.spccd    = :spccd"
                sSql += "   AND c.rstcdseq = :rstcdseq"
                sSql += "   AND r.testcd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND r.tkdt    >= f.usdt"
                sSql += "   AND r.tkdt    <  f.uedt"
                sSql += "   AND NVL(r.rstflg, '0') IN ('0', '1', '2', '3')"
                sSql += " GROUP BY r.bcno, c.testcd, c.spccd, c.cvtparam, c.ctestcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "          f.tnmd, r.orgrst, r.viewrst, r.hlmark"


                Dim dbCmd As New OracleCommand
                Dim objDAdapter As OracleDataAdapter
                Dim dt As New DataTable

                dbCmd.Connection = ro_DbCn
                If ro_DbTrans IsNot Nothing Then dbCmd.Transaction = ro_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                objDAdapter = New OracleDataAdapter(dbCmd)

                objDAdapter.SelectCommand.Parameters.Clear()
                objDAdapter.SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                objDAdapter.SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                objDAdapter.SelectCommand.Parameters.Add("spccd", OracleDbType.Varchar2).Value = rsSpcCd
                objDAdapter.SelectCommand.Parameters.Add("rstcdseq", OracleDbType.Varchar2).Value = rsRstCd

                dt.Reset()
                objDAdapter.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

    End Class

    Public Class BcnoFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.COMM.BcNoFn" + vbTab

        '# PrtBcNo --> BcNo
        Public Shared Function fnFind_BcNo(ByVal rsNo As String) As String
            Dim sFn As String = "Function fnFind_BcNo"

            Try
                Dim sBcNo As String = ""
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""

                '검체번호바코드(일반)   : 11
                '작업번호바코드(미생물) : <> 11
                Select Case rsNo.Length
                    Case 11
                        sSql = ""
                        sSql += "SELECT fn_ack_get_bcno_normal(:bcno) FROM DUAL"

                        al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsNo))

                    Case Else '10

                        sSql = ""
                        sSql += "SELECT bcno FROM rr010m"
                        sSql += " WHERE wkymd   = :wkymd"
                        sSql += "   AND wkgrpcd = :wgrpcd"
                        sSql += "   AND wkno    = :wkno"

                        rsNo = fnFind_WkNo_From_PrtWkNo(rsNo)

                        al.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, 8, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsNo.Substring(0, 8)))
                        al.Add(New OracleParameter("wkgrp", OracleDbType.Varchar2, 2, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsNo.Substring(8, 2)))
                        al.Add(New OracleParameter("wkno", OracleDbType.Varchar2, 4, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsNo.Substring(10, 4)))

                End Select

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    sBcNo = dt.Rows(0).Item(0).ToString()
                Else
                    sBcNo = ""
                End If

                Return sBcNo

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '# PrtWkNo(yyMMdd__1234) --> WkNo(yyyyMMdd__1234)
        Public Shared Function fnFind_WkNo_From_PrtWkNo(ByVal rsNo As String) As String
            Dim sReturn As String = ""

            '2100년에 2001년 바코드 사용하는 경우
            If Now.Year < Convert.ToInt32(Now.ToShortDateString().Substring(0, 2) + rsNo.Substring(0, 2)) Then
                sReturn = (Convert.ToInt32(Now.ToShortDateString().Substring(0, 2)) - 1).ToString() + rsNo
            Else
                sReturn = Now.ToShortDateString().Substring(0, 2) + rsNo
            End If

            Return sReturn
        End Function
    End Class

End Namespace
