Imports Oracle.DataAccess.Client
Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar


Namespace COMM
    '-- 기초코드 공통
    Public Class CdFn
        Private Const msFile As String = "File : CGLISAPP_COMM, Class : LISAPP.COMM.CdFn" + vbCrLf

      
        Public Shared Function fnGet_CmtList_GV() As DataTable
            Dim sFn As String = "fnGet_CmtList_GV As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT '' chk, cdseq, cdtitle, cdcont"
                sSql += "  FROM lf320m"
                sSql += " WHERE cdsep = 'CMT'"
                sSql += " ORDER BY cdseq"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

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
                sSql += "  FROM lf050m"
                sSql += " WHERE NVL(delflg, '0') = '0'"
                sSql += " ORDER BY exlabcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)


      
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                If rsEqGbn <> "" Then
                    sSql += "   AND (eqgbn LIKE '" + rsEqGbn + "%' OR eqgbn = '0')"
                End If
                sSql += " ORDER BY eqcd"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_Com_List(ByVal rsComGbn As String, ByVal rsComCd As String) As DataTable
            Dim sFn As String = "fnGet_Com_List() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT '' chk, f.comcd, f.comnmd, f.comordcd, f.spccd, f.donqnt, f.comordcd || f.spccd ordkey,"
                sSql += "       CASE WHEN f.comgbn = '1' THEN 'Prep'"
                sSql += "            WHEN f.comgbn = '2' THEN 'Tran'"
                sSql += "            WHEN f.comgbn = '3' THEN 'Emer'"
                sSql += "            WHEN f.comgbn = '4' THEN 'Irra.'"
                sSql += "       END tnsgbn,"
                sSql += "       CASE WHEN NVL(f.ftcd, '000') = '000' THEN '' ELSE '○' END filter,"
                sSql += "       NVL(f.dispseqo, 999) sort_key"
                sSql += "  FROM lf120m f, lf030m f3"
                sSql += " WHERE f.usdt  <= fn_ack_sysdate"
                sSql += "   AND f.uedt  >  fn_ack_sysdate"
                sSql += "   AND f.spccd  = f3.spccd"
                sSql += "   AND f3.usdt <= fn_ack_sysdate"
                sSql += "   AND f3.uedt >  fn_ack_sysdate"

                If rsComGbn <> "" Then
                    sSql += "   AND f.comgbn = :comgbn"
                    al.Add(New OracleParameter("comgbn", OracleDbType.Varchar2, rsComGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComGbn))
                End If

                If rsComCd <> "" Then
                    sSql += "   AND f.comcd = :comcd"
                    al.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComCd))
                End If

                sSql += " ORDER BY sort_key"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 헌혈 부적격 사유
        Public Shared Function fnGet_dis_list() As DataTable
            Dim sFn As String = "Function fnGet_dis_list() As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       discd, disrsn"
                sSql += "  FROM lf111m"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_TOrdSlip() As DataTable
            Dim sFn As String = "fnGet_TOrdSlip() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT  '[' || tordslip || '] ' || tordslipnm tordslipnm"
                sSql += "  FROM lf100m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += " ORDER BY dispseq, tordslip"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        Public Shared Function fnGet_DTestList(ByVal rsTestcd As String, ByVal rsSpccd As String) As DataTable
            Dim sFn As String = "fnGet_TOrdSlip() As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList


                sSql = ""
                sSql += " SELECT tclscd , tspccd , testcd , spccd "
                sSql += "  FROM lf067m "
                sSql += " WHERE tclscd = :tclscd "
                sSql += "   AND tspccd = :tspccd "

                al.Add(New OracleParameter("tclscd", OracleDbType.Varchar2, rsTestcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestcd))
                al.Add(New OracleParameter("tspccd", OracleDbType.Varchar2, rsSpccd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 검사항목별 결과코드
        Public Shared Function fnGet_TestRst_list(ByVal rsTestCd As String) As DataTable

            Dim sFn As String = "fnGet_TestRst_list() As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT keypad, grade, rstcont, rstcdseq, NVL(rstlvl, 'N') rstlvl"
                sSql += "  FROM lf083m"
                sSql += " WHERE testcd = :testcd"
                sSql += "   AND spccd  = '" + "".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"
                sSql += " ORDER BY rstcdseq"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 장비코드
        Public Shared Function fngGet_Eq_list() As DataTable
            Dim sFn As String = "fngGet_Eq_list() As DataTable"
            Try
                Dim sSql As String = ""


                sSql = ""
                sSql += "SELECT portno, eqcd, eqnm"
                sSql += "  FROM lf070m"
                sSql += " WHERE uedt > fn_ack_sysdate"
                sSql += " ORDER BY portno, eqcd"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 배양균
        Public Shared Function fnGet_Bac_List(ByVal rsBacGenCd As String, ByVal rbSameCd As Boolean, ByVal rsUsDt As String) As DataTable
            Dim sFn As String = "fnGet_Bac_List(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rbSameCd Then
                    sSql = ""
                    sSql += "SELECT a.baccd, a.bacnmd, a.bacgencd"
                    sSql += "  FROM lf210m a, lf210m b"
                    sSql += " WHERE a.baccd = NVL(b.samecd, b.baccd)"
                    sSql += "   AND a.usdt  = b.usdt"

                    If rsUsDt = "" Then
                        sSql += "   AND a.usdt <= fn_ack_sysdate"
                        sSql += "   AND a.uedt >  fn_ack_sysdate"

                    Else
                        sSql += "   AND a.usdt <= :usdt || '000000'"
                        sSql += "   AND a.uedt >  :usdt || '000000'"

                        alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                        alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                    End If
                Else
                    sSql = ""
                    sSql += "SELECT a.baccd, a.bacnmd, a.bacgencd"
                    sSql += "  FROM lf210m a"

                    If rsUsDt = "" Then
                        sSql += " WHERE a.usdt <= fn_ack_sysdate"
                        sSql += "   AND a.uedt >  fn_ack_sysdate"

                    Else
                        sSql += " WHERE a.usdt <= :usdt || '000000'"
                        sSql += "   AND a.uedt >  :usdt || '000000'"

                        alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                        alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                    End If
                End If

                If rsBacGenCd <> "" Then
                    sSql += "   AND a.bacgencd = :bacgencd"
                    alParm.Add(New OracleParameter("bacgencd", OracleDbType.Varchar2, rsBacGenCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBacGenCd))
                End If
                sSql += " ORDER BY baccd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 2008/03/21 YEJ add(배양균속)
        Public Shared Function fnGet_BacGen_List() As DataTable
            Dim sFn As String = "fnGet_BacGen_List"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT bacgencd, bacgennmd"
                sSql += "  FROM lf220m"
                sSql += " ORDER BY bacgencd"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_TestWithSpc_List(ByVal rsTestCd As String, ByVal rsUsDt As String) As DataTable
            Dim sFn As String = "fnGet_TestWithSpc_List([String], [String], [String], [String])"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       f6.testcd, f6.spccd, f6.usdt, f6.uedt, f3.spcnmd"
                sSql += "  FROM lf060m f6, lf030m f3"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                    sSql += "   AND spccd IN (SELECT spccd FROM lf060m  where testcd = :testcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsPartCd.Length + rsSlipCd.Length = 2 Then
                    sSql += "   AND spccd IN (SELECT spccd FROM lf060m  WHERE partcd = :partcd AND slipcd = :slipcd)"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                ElseIf rsPartCd.Length > 0 Then
                    sSql += "   AND spccd IN (SELECT spccd FROM lf060m  WHERE partcd = :partcd)"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND spccd IN (SELECT spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                If rsWGrpCd <> "" Then
                    sSql += "   AND spccd IN (SELECT spccd FROM lf066m WHERE wkgrpcd = :wgrpcd)"
                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                End If

                sSql += " ORDER BY spccd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

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

        Public Shared Function fnGet_Usr_List(Optional ByVal rbUseFlg As Boolean = False, Optional ByVal rsUsrId As String = "") As DataTable
            Dim sFn As String = "fnGet_UsrInfo"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT '' chk, usrid, usrnm,"
                sSql += "       CASE WHEN NVL(delflg, '0') = '0' THEN '사용중' ELSE '삭제' END delflg"
                sSql += "  FROM lf090m "
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_bccls_color() As DataTable
            Dim sFn As String = "Function fnGet_bccls_color() As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT bcclscd, REPLACE(bcclsnmd, '검체', '') bcclsnmd, colorgbn, bcclsgbn"
                sSql += "  FROM lf010m "
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND NVL(colorgbn, '0') > 0"
                sSql += "   and bcclsgbn <> '9'"
                sSql += " ORDER BY colorgbn"

                DbCommand()
                Return DbExecuteQuery(sSql)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_cmtcont_etc(ByVal rsCmtGbn As String, ByVal rbCmtGbnAdd As Boolean, Optional ByVal rsCmtCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_cmtcont_etc(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rbCmtGbnAdd Then
                    sSql += "SELECT cmtgbn || cmtcd cmtcd, cmtcont  FROM lf410m"
                Else
                    sSql += "SELECT cmtcd cmtcd, cmtcont  FROM lf410m"
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
                sSql += "          FROM lf080m f8  LEFT OUTER JOIN lf021m f2  ON (f8.partcd = f2.partcd AND f8.slipcd = f2.slipcd)"

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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                sSql += "  FROM lf060m"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        ' 일반 검사항목 조회
        Public Shared Function fnGet_test_WithReference(ByVal rsTestCd As String, Optional ByVal rsSpcCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_tcls_WithParent(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT '' chk, MAX(f6.tnms) tnms, MAX(f6.tnmD) tnmd, f6.testcd, f6.tordcd, f6.tordslip, f6.tcdgbn, MIN(NVL(f6.dispseqL, 999)) sort2"
                sSql += "  FROM lf063m f67, lf060m f6"
                sSql += " WHERE f67.testcd = :testcd"

                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                If rsSpcCd <> "" Then
                    sSql += "   AND f67.spccd  = :spccd"
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += "   AND f67.reftestcd = f6.testcd"
                sSql += "   AND f67.refspccd  = f6.spccd"
                sSql += "   AND f6.usdt <= fn_ack_sysdate"
                sSql += "   AND f6.uedt  > fn_ack_sysdate"
                sSql += " GROUP BY f6.testcd, f6.tordcd, f6.tordslip, f6.tcdgbn"
                sSql += " ORDER BY sort2, f6.testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try
        End Function

        '-- 검사코드 리스트(Battery, Group 제외)
        Public Shared Function fnGet_test_ParentSingle(ByVal rsSlipCd As String, ByVal rsTGrpCd As String, _
                                                       Optional ByVal rsTestCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_tcls_ParentSingle(String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, MAX(f6.tnmd) tnmd, MAX(f6.tnmp) tnmp, f6.testcd, f6.tordcd, f6.tordslip, f6.tcdgbn,"
                sSql += "       MIN(NVL(f6.dispseql, 999)) sort2,"
                sSql += "       MAX(NVL(f6.ctgbn, '0')) ctgbn,"
                sSql += "       f5.exlabnmd"
                sSql += "  FROM lf060m f6 LEFT OUTER JOIN"
                sSql += "       lf050m f5 ON (f6.exlabcd = f5.exlabcd AND NVL(f5.delflg, ' ') <> '1')"
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
                    sSql += "   AND (f6.testcd, f6.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"

                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                sSql += " GROUP BY testcd, tordcd, tordslip, tcdgbn, exlabnmd"
                sSql += " ORDER BY sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '-- 검사코드 리스트(Group 제외)
        Public Shared Function fnGet_test_BatteryParentSingle(ByVal rsBcclsCd As String, ByVal rsPartCd As String, ByVal rsSlipCd As String, ByVal rsTGrpCd As String) As DataTable
            Dim sFn As String = "Function fnGet_tcls_ParentSingle(String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       MAX(tnmd) tnmd, testcd, NVL(ordhide, '0') ordhide, MIN(NVL(dispseql, 999)) sort2"
                sSql += "  FROM lf060m"
                sSql += " WHERE tcdgbn IN ('B', 'P', 'S')"
                sSql += "   AND usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"

                If rsBcclsCd <> "" Then
                    sSql += "   AND bcclscd = :bcclscd"
                    al.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                If rsPartCd <> "" Then
                    sSql += "   AND partcd = :partcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartCd))
                End If

                If rsSlipCd <> "" Then
                    sSql += "   AND slipcd = :slipcd"
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND (tsectcd, spccd) IN (SELECT tsectcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"

                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                sSql += " GROUP BY testcd, ordhide"
                sSql += " ORDER BY sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                sSql += "  FROM lf060m f6, lf030m f3"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_TestCd(ByVal rsCd As String, Optional ByVal riMode As Integer = 0) As DataTable
            Dim sFn As String = "fnGet_TestCd"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT f6.testcd, f6.tordcd, f6.tnm, f3.spcnm, f3.spccd, f6.tnmd, f3.spcnmd,"
                sSql += "       CASE WHEN f6.tcdgbn = 'P' THEN '[P] Parent'"
                sSql += "            WHEN f6.tcdgbn = 'S' THEN '[S] Single'"
                sSql += "            WHEN f6.tcdgbn = 'C' THEN '[C] Child'"
                sSql += "            WHEN f6.tcdgbn = 'G' THEN '[G] Group'"
                sSql += "            WHEN f6.tcdgbn = 'B' THEN '[B] Battery'"
                sSql += "       END tcdgbn,"
                sSql += "       fn_ack_date_str(f6.usdt,'yyyy-mm-dd hh24:mi:ss') usdt,"
                sSql += "       fn_ack_date_str(f6.uedt, 'yyyy-mm-dd hh24:mi:ss') uedt,"
                sSql += "       fn_ack_date_str(f6.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "       fn_ack_get_usr_name(f6.regid) regid"
                sSql += "  FROM lf060m f6, lf030m f3"
                sSql += " WHERE f6.spccd = f3.spccd "
                If riMode = 0 Then
                    '검사코드
                    sSql += "   AND f6.testcd = :tcd"
                Else
                    '처방코드
                    sSql += "   AND f6.tordcd = :tcd"
                End If
                sSql += " ORDER BY testcd, uedt DESC"

                al.Add(New OracleParameter("tcd", OracleDbType.Varchar2, rsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCd))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                sSql += "  FROM lf060m a" + vbCrLf
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
        '-- 검사코드 리스트()
        Public Shared Function fnGet_test_ref_list(ByVal rsSlipCd As String, _
                                               Optional ByVal rsTestCd As String = "", _
                                               Optional ByVal rsFilter As String = "") As DataTable
            Dim sFn As String = "Function fnGet_test_list(String, String, String, [String], [String], [String], [String]) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       '' chk, MAX(tnmd) tnmd, MAX(tnmp) tnmp, testcd, tcdgbn," + vbCrLf
                sSql += "       NVL(titleyn, '0') titleyn, NVL(mbttype, '0') mbttype," + vbCrLf
                sSql += "       NVL(ordhide, '0') ordhide, NVL(poctyn, '0') poctyn," + vbCrLf
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = a.partcd AND slipcd = a.slipcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1," + vbCrLf
                sSql += "       MIN(dispseql) sort2," + vbCrLf
                sSql += "       (SELECT MIN(NVL(dispseq, 999)) FROM lf100m WHERE tordslip = a.tordslip AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort_tslip," + vbCrLf
                sSql += "       MIN(dispseqo) sort_ord" + vbCrLf
                sSql += "  FROM lf060m a" + vbCrLf
                sSql += " WHERE usdt <= fn_ack_sysdate" + vbCrLf
                sSql += "   AND uedt >  fn_ack_sysdate" + vbCrLf

                If rsTestCd <> "" Then
                    sSql += "   AND testcd = :testcd" + vbCrLf
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSlipCd <> "" Then
                    sSql += "   AND partcd = :partcd" + vbCrLf
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))

                    If rsSlipCd.Length = 2 Then
                        sSql += "   AND slipcd = :slipcd" + vbCrLf
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                    End If
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

        '-- 검사코드 리스트()
        Public Shared Function fnGet_test_list(ByVal rsSlipCd As String, ByVal rsTGrpCd As String, ByVal rsWGrpCd As String, _
                                               Optional ByVal rsTestCd As String = "", Optional ByVal rsSpcCd As String = "", _
                                               Optional ByVal rsBcclsCd As String = "", Optional ByVal rsTordSlip As String = "", _
                                               Optional ByVal rsFilter As String = "") As DataTable
            Dim sFn As String = "Function fnGet_test_list(String, String, String, [String], [String], [String], [String]) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       '' chk, MAX(tnmd) tnmd, MAX(tnmp) tnmp, testcd, tcdgbn," + vbCrLf
                sSql += "       NVL(titleyn, '0') titleyn, NVL(mbttype, '0') mbttype," + vbCrLf
                sSql += "       NVL(ordhide, '0') ordhide, NVL(poctyn, '0') poctyn," + vbCrLf
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = a.partcd AND slipcd = a.slipcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1," + vbCrLf
                sSql += "       MIN(dispseql) sort2," + vbCrLf
                sSql += "       (SELECT MIN(NVL(dispseq, 999)) FROM lf100m WHERE tordslip = a.tordslip AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort_tslip," + vbCrLf
                sSql += "       MIN(dispseqo) sort_ord" + vbCrLf
                sSql += "  FROM lf060m a" + vbCrLf
                sSql += " WHERE usdt <= fn_ack_sysdate" + vbCrLf
                sSql += "   AND uedt >  fn_ack_sysdate" + vbCrLf

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


        '-- 검사코드 리스트()
        Public Shared Function fnGet_test_poct() As DataTable
            Dim sFn As String = "Function fnGet_test_poct() As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, MAX(tnmd) tnmd, MAX(tnmp) tnmp, testcd,"
                sSql += "       NVL(poctyn, '0') poctyn,"
                sSql += "       (SELECT MIN(NVL(dispseq, 999)) FROM lf100m WHERE tordslip = a.tordslip AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1,"
                sSql += "       MIN(dispseqo) sort2"
                sSql += "  FROM lf060m a"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND NVL(ordhide, '0') = '0'"
                sSql += "   AND NVL(poctyn, '0') = '1'"
                sSql += "   AND tcdgbn <> 'C'"

                sSql += " GROUP BY testcd, tcdgbn, tordslip, poctyn"
                sSql += " ORDER BY sort1, sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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

                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       '' chk, f6.tnmd, f6.tnmp tnmp, f6.testcd, f6.spccd, RPAD(f6.testcd, 8, ' ') || f6.spccd testspc," + vbCrLf
                sSql += "       f6.tordcd, f6.tordslip, f6.tcdgbn, f6.partcd || f6.slipcd partslip, NVL(f6.titleyn, '0') titleyn," + vbCrLf
                sSql += "       NVL(f6.mbttype, '0') mbttype, NVL(f6.poctyn, '0') poctyn," + vbCrLf
                sSql += "       f3.spcnmd, NVL(f2.dispseq, 999) sort1, NVL(f6.dispseql, 999) sort2" + vbCrLf
                sSql += "  FROM lf060m f6, lf021m f2, lf030m f3" + vbCrLf
                sSql += " WHERE f6.usdt <= fn_ack_sysdate" + vbCrLf
                sSql += "   AND f6.uedt >  fn_ack_sysdate" + vbCrLf

                If rsTestCd <> "" Then
                    sSql += "   AND f6.testcd LIKE :testcd || '%'" + vbCrLf

                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND f6.spccd = :spccd" + vbCrLf

                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsSlipCd <> "" Then
                    sSql += "   AND f6.partcd = :partcd" + vbCrLf
                    sSql += "   AND f6.slipcd = :slipcd" + vbCrLf

                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND (f6.testcd, f6.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)" + vbCrLf

                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                sSql += "   AND f6.partcd = f2.partcd" + vbCrLf
                sSql += "   AND f6.slipcd = f2.slipcd" + vbCrLf
                sSql += "   AND f2.usdt  <= fn_ack_sysdate" + vbCrLf
                sSql += "   AND f2.uedt   > fn_ack_sysdate" + vbCrLf
                sSql += "   AND f6.spccd  = f3.spccd" + vbCrLf
                sSql += "   AND f3.usdt  <= fn_ack_sysdate" + vbCrLf
                sSql += "   AND f3.uedt   > fn_ack_sysdate" + vbCrLf
                sSql += " ORDER BY sort1, sort2, testspc" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '-- 검사코드 리스트
        Public Shared Function fnGet_testspc_list_m(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rbSpcGbn As Boolean) As DataTable
            Dim sFn As String = "Function fnGet_testspc_list(String, String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                If rbSpcGbn Then
                    sSql += "SELECT DISTINCT"
                    sSql += "       f6.testcd, '' spccd, MIN(f6.tnmd) tnmd, '' spcnmd,"
                    sSql += "       MIN(NVL(f2.dispseq, 999)) sort1, MIN(NVL(f6.dispseql, 999)) sort2"
                    sSql += "  FROM lf060m f6, lf021m f2"
                    sSql += " WHERE f6.usdt   <= fn_ack_sysdate"
                    sSql += "   AND f6.uedt   >  fn_ack_sysdate"
                    sSql += "   AND f6.partcd  = f2.partcd"
                    sSql += "   AND f6.slipcd  = f2.slipcd"
                    sSql += "   AND f2.usdt   <= fn_ack_sysdate"
                    sSql += "   AND f2.uedt   >  fn_ack_sysdate"
                    sSql += "   AND f6.mbttype = '2'"

                    If rsTestCd <> "" Then
                        sSql += "   AND f6.testcd = :testcd"
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                    End If
                    sSql += " GROUP BY f6.testcd"
                Else
                    sSql += "SELECT DISTINCT"
                    sSql += "       f6.testcd, f6.spccd, f6.tnmd, f3.spcnmd,"
                    sSql += "       NVL(f2.dispseq, 999) sort1, NVL(f6.dispseql, 999) sort2"
                    sSql += "  FROM lf060m f6, lf030m f3, lf021m f2"
                    sSql += " WHERE f6.usdt   <= fn_ack_sysdate"
                    sSql += "   AND f6.uedt   >  fn_ack_sysdate"
                    sSql += "   AND f6.spccd   = f3.spccd"
                    sSql += "   AND f3.usdt   <= fn_ack_sysdate"
                    sSql += "   AND f3.uedt   >  fn_ack_sysdate"
                    sSql += "   AND f6.partcd  = f2.partcd"
                    sSql += "   AND f6.slipcd  = f2.slipcd"
                    sSql += "   AND f2.usdt   <= fn_ack_sysdate"
                    sSql += "   AND f2.uedt   >  fn_ack_sysdate"
                    sSql += "   AND f6.mbttype = '2'"
                    If rsTestCd <> "" Then
                        sSql += "   AND f6.testcd = :testcd"
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "   AND f6.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                    End If
                End If

                sSql += " ORDER BY sort1, sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_testspc_list_ord(ByVal rsOrdSlip As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "Function fnGet_testspc_list_ord(String, String, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT'' chk, f6.tnmd, f6.tnmp tnmp, f6.testcd, f6.spccd, RPAD(f6.testcd, 8, ' ') || f6.spccd testspc,"
                sSql += "       f6.tordcd, f6.tordslip, f6.tcdgbn, f6.partcd || f6.slipcd partslip, NVL(f6.titleyn, '0') titleyn,"
                sSql += "       f6.sugacd, f6.insugbn, f6.minspcvol, f6.bcclscd, f3.spcnmd,"
                sSql += "       (SELECT MIN(NVL(dispseq, 999)) FROM lf100m WHERE tordslip = f6.tordslip AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1,"
                sSql += "       NVL(f6.dispseqo, 999) sort2,"
                sSql += "       CASE WHEN f6.spccd = dspccd1 THEN 0 ELSE 1 END sort3"
                sSql += "  FROM lf060m f6, lf030m f3"
                sSql += " WHERE f6.usdt    <= fn_ack_sysdate"
                sSql += "   AND f6.uedt    >  fn_ack_sysdate"
                sSql += "   AND f6.ordhide  = '0'"
                sSql += "   AND NVL(f6.tordcd, ' ') <> ' '"
                sSql += "   AND f6.tcdgbn  IN ('G', 'B', 'S', 'P')"

                If rsOrdSlip <> "" Then
                    sSql += "   AND f6.tordslip = :tordslip"

                    al.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsOrdSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdSlip))
                End If

                If rsTestCd <> "" Then
                    sSql += "   AND f6.testcd = :testcd"

                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND f6.spccd = :spccd"

                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += "   AND f6.spccd = f3.spccd"
                sSql += "   AND f3.usdt <= fn_ack_sysdate"
                sSql += "   AND f3.uedt >  fn_ack_sysdate"

                '-- 핵의학체외 검사
                sSql += " UNION "
                sSql += "SELECT'' chk, f6.tnmd, f6.tnmp tnmp, f6.testcd, f6.spccd, RPAD(f6.testcd, 8, ' ') || f6.spccd testspc,"
                sSql += "       f6.tordcd, f6.tordslip, f6.tcdgbn, f6.partcd || f6.slipcd partslip, NVL(f6.titleyn, '0') titleyn,"
                sSql += "       f6.sugacd, f6.insugbn, f6.minspcvol, f6.bcclscd, f3.spcnmd,"
                sSql += "       (SELECT MIN(NVL(dispseq, 999)) FROM lf100m WHERE tordslip = f6.tordslip AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1,"
                sSql += "       NVL(f6.dispseqo, 999) sort2,"
                sSql += "       CASE WHEN f6.spccd = dspccd1 THEN 0 ELSE 1 END sort3"
                sSql += "  FROM rf060m f6, lf030m f3"
                sSql += " WHERE f6.usdt    <= fn_ack_sysdate"
                sSql += "   AND f6.uedt    >  fn_ack_sysdate"
                sSql += "   AND f6.ordhide  = '0'"
                sSql += "   AND NVL(f6.tordcd, ' ') <> ' '"
                sSql += "   AND f6.tcdgbn  IN ('G', 'B', 'S', 'P')"

                If rsOrdSlip <> "" Then
                    sSql += "   AND f6.tordslip = :tordslip"

                    al.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsOrdSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdSlip))
                End If

                If rsTestCd <> "" Then
                    sSql += "   AND f6.testcd = :testcd"
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND f6.spccd = :spccd"

                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += "   AND f6.spccd = f3.spccd"
                sSql += "   AND f3.usdt <= fn_ack_sysdate"
                sSql += "   AND f3.uedt >  fn_ack_sysdate"

                sSql += " ORDER BY sort1, sort2, testcd, sort3, spccd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '-- 검체코드 리스트
        Public Shared Function fnGet_spc_list_m(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rbSpcGbn As Boolean) As DataTable
            Dim sFn As String = "Function fnGet_spc_list_m(String, String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       '' chk, f6.spccd, f3.spcnmd"
                sSql += "  FROM lf060m f6, lf030m f3"
                sSql += " WHERE f6.usdt  <= fn_ack_sysdate"
                sSql += "   AND f6.uedt  >  fn_ack_sysdate"
                sSql += "   AND f6.spccd  = f3.spccd"
                sSql += "   AND f3.usdt  <= fn_ack_sysdate"
                sSql += "   AND f3.uedt  >  fn_ack_sysdate"
                sSql += "   AND f6.mbttype = '2'"

                If rsTestCd <> "" Then
                    sSql += "   AND f6.testcd = :testcd"
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND f6.spccd = :spccd"
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += " ORDER BY f6.spccd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        ' 배터리, 그룹 조회
        Public Shared Function fnGet_BatteyGroup(ByVal rsWkGrpCd As String) As DataTable
            Dim sFn As String = "Function fnGet_BatteyGroup(ByVal sOrdSlip As String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT '' chk, tnmd, tnms, testcd, tordcd, tordslip, tcdgbn, NVL(dispseql, 999) sort2"
                sSql += "  FROM lf060m"
                sSql += " WHERE tcdgbn IN ('B', 'G')"
                sSql += "   AND usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate "
                If rsWkGrpCd <> "" Then
                    sSql += "   AND (tsectcd, spccd) IN (SELECT tsectcd, spccd FROM lf066m WHERE wkgrpcd = wgrpcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                End If
                sSql += " ORDER BY sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        ' 검체분류
        Public Shared Function fnGet_Bccls_List(Optional ByVal rbAll As Boolean = True, Optional ByVal rbBloodBank As Boolean = False, Optional ByVal rbMicroBio As Boolean = False) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Bccls_List() As DataTable"

            Try
                Dim sSql As String = ""

                sSql += "SELECT bcclscd, bcclsnmd, colorgbn"
                sSql += "  FROM lf010m"
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
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        ' 검체분류
        Public Shared Function fnGet_Bccls_ExLab_List(ByVal rsExLabCd As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Bccls_ExLab_List() As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT bcclscd, bcclsnmd, colorgbn"
                sSql += "  FROM lf010m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                If rsExLabCd <> "" Then
                    sSql += "   AND bcclscd IN (SELECT bcclscd FROM lf060m WHERE NVL(exlabyn, '0') = '1' AND exlabcd = :exlabcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                    alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
                Else
                    sSql += "   AND bcclscd IN (SELECT bcclscd FROM lf060m WHERE NVL(exlabyn, '0') = '1' AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '검사예문 조회 
        Public Shared Function fnGet_RstEx_List() As DataTable
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       refcd rstcd , refnm rstex "
                sSql += "  FROM lf510m "
                sSql += " WHERE gbn = 'R'"
                sSql += " ORDER BY refcd "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Function
        ' 검사분야 조회
        Public Shared Function fnGet_PartSlip_List() As DataTable
            Dim sFn As String = "Function fnGet_PartSlip_List() As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       partcd || slipcd slipcd, slipnmd "
                sSql += "  FROM lf021m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += " ORDER by slipcd "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        ' 검사분야 조회
        Public Shared Function fnGet_Slip_List(Optional ByVal rsUsDt As String = "", Optional ByVal rbAll As Boolean = True, _
                                               Optional ByVal rbBloodBank As Boolean = False, _
                                               Optional ByVal rbMicroBioYn As Boolean = False, _
                                               Optional ByVal rbGeneralVerifyYn As Boolean = False, _
                                               Optional ByVal rbCtTest As Boolean = False) As DataTable
            Dim sFn As String = "Function fnGet_Slip_List() As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                rsUsDt = rsUsDt.Replace("-", "").Replace(":", "").Replace(" ", "")
                If rsUsDt.Length = 8 Then rsUsDt += "000000"

                sSql += "SELECT DISTINCT"
                sSql += "       partcd || slipcd slipcd, slipnmd, dispseq"
                sSql += "  FROM lf021m"

                If rsUsDt = "" Then
                    sSql += " WHERE usdt <= fn_ack_sysdate"
                    sSql += "   AND uedt >  fn_ack_sysdate"
                Else
                    sSql += " WHERE usdt <= :usdt"
                    sSql += "   AND uedt >  :usdt"

                    alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                    alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                End If

                If rbAll And rbBloodBank Then
                    sSql += "   AND partcd IN (SELECT partcd FROM lf020m WHERE partgbn IN ('0', '1', '3'))"
                ElseIf rbAll = False And rbBloodBank = False Then
                    sSql += "   AND partcd IN (SELECT partcd FROM lf020m WHERE partgbn = '0')"
                ElseIf rbBloodBank Then
                    sSql += "   AND partcd IN (SELECT partcd FROM lf020m WHERE partgbn = '3')"
                ElseIf rbMicroBioYn Then
                    sSql += "   AND partcd IN (SELECT f6.partcd FROM lf060m f6, lf010m f1"
                    sSql += "                   WHERE f6.bcclscd  = f1.bcclscd"
                    sSql += "                     AND f1.bcclsgbn = '2'"
                    If rsUsDt = "" Then
                        sSql += "                     AND f6.usdt <= fn_ack_sysdate"
                        sSql += "                     AND f6.uedt >  fn_ack_sysdate"
                        sSql += "                     AND f1.usdt <= fn_ack_sysdate"
                        sSql += "                     AND f1.uedt >  fn_ack_sysdate"
                    Else
                        sSql += "                     AND f6.usdt <= :usdt"
                        sSql += "                     AND f6.uedt >  :usdt"
                        sSql += "                     AND f1.usdt <= :usdt"
                        sSql += "                     AND f1.uedt >  :usdt"

                        alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                        alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                        alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                        alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                    End If

                    sSql += "                 )"
                ElseIf rbGeneralVerifyYn Then
                    sSql += "   AND partcd IN (SELECT partcd FROM lf020m  WHERE partgbn = '1')"
                End If

                If rbCtTest Then
                    sSql += "   AND (partcd, slipcd) IN (SELECT partcd, slipcd FROM lf060m  WHERE NVL(ctgbn, '0') = '1')"
                End If

                'sSql += "   AND partcd <> 'Z'"

                sSql += " ORDER BY dispseq, slipcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        ' 검사그룹 조회
        Public Shared Function fnGet_TGrp_List(Optional ByVal rbAll As Boolean = True, Optional ByVal rbMicro As Boolean = False, _
                                               Optional ByVal rbBlood As Boolean = False) As DataTable
            Dim sFn As String = "Function fnGet_TGrp_List(String, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim sWhere As String = ""

                sSql += "SELECT DISTINCT f63.tgrpcd, f63.tgrpnmd"
                sSql += "  FROM lf065m f63, lf060m f6"
                sSql += " WHERE f63.testcd = f6.testcd"
                sSql += "   AND f63.spccd  = f6.spccd"
                sSql += "   AND f6.usdt   <= fn_ack_sysdate"
                sSql += "   AND f6.uedt   >  fn_ack_sysdate"


                If rbMicro Then
                    sSql += "   AND f6.bcclscd IN (SELECT bcclscd FROM lf010m WHERE NVL(bcclsgbn, '0') = '2' AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                ElseIf rbBlood Then
                    sSql += "   AND f6.bcclscd IN (SELECT bcclscd FROM lf010m WHERE NVL(bcclsgbn, '0') = '3' AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                ElseIf rbAll = False Then
                    sWhere = "'2', '3',"
                End If

                If sWhere <> "" Then
                    sSql += "   AND f6.bcclscd IN (SELECT bcclscd FROM lf010m WHERE NVL(bcclsgbn, '0') NOT IN (" + sWhere + "'7', '8', '9') AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
                End If
                sSql += " ORDER BY F63.tgrpcd"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        ' 검사그룹 조회
        Public Shared Function fnGet_TGrp_List(ByVal rsPartSlip As String) As DataTable
            Dim sFn As String = "Function fnGet_TGrp_List(String, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT f65.tgrpcd, f65.tgrpnmd"
                sSql += "  FROM lf065m f65, lf060m f6"
                sSql += " WHERE f65.testcd = f6.testcd"
                sSql += "   AND f65.spccd  = f6.spccd"
                sSql += "   AND f6.usdt   <= fn_ack_sysdate"
                sSql += "   AND f6.uedt   >  fn_ack_sysdate"

                If rsPartSlip.Length = 1 Then
                    sSql += "   AND f6.partcd = :partcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                ElseIf rsPartSlip.Length = 2 Then
                    sSql += "   AND f6.partcd = :partcd"
                    sSql += "   AND f6.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))

                End If

                sSql += " ORDER BY F65.tgrpcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        ' 검사그룹 검사항목 조회
        Public Shared Function fnGet_TGrp_Test_List(ByVal rsTGrpCd As String) As DataTable
            Dim sFn As String = "Function fnGet_TGrp_Test_List(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       RPAD(b.testcd, 7, ' ') || b.spccd testcd ,b.tnmd "
                sSql += "  FROM lf065m a, lf060m b"

                If rsTGrpCd.IndexOf(",") > 0 Then
                    sSql += " WHERE a.tgrpcd IN ('" + rsTGrpCd.Replace(",", "','") + "')"
                Else
                    sSql += " WHERE a.tgrpcd = :tgrpcd"
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                sSql += "   AND b.testcd LIKE a.testcd || '%'"
                sSql += "   AND b.spccd  =  a.spccd"
                sSql += "   AND b.usdt   <= fn_ack_sysdate"
                sSql += "   AND b.uedt   >  fn_ack_sysdate"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_TGrp_Test_List(ByVal rsTGrpCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "fnGet_TGrp_Items"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT f6.testcd, MAX(f6.tnmd) tnmd,"
                sSql += "       MAX(f2.dispseq) sort1,"
                sSql += "       MAX(f6.dispseql) sort2"
                sSql += "  FROM lf065m f63, lf060m f6, lf021m f2"
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
                Throw (New Exception(ex.Message, ex))
            End Try

        End Function

        ' 작업그룹 조회
        Public Shared Function fnGet_WKGrp_List(ByVal rsSlipCd As String) As DataTable
            Dim sFn As String = "Function fnGet_WKGrp_List(String, string) As DataTable"
            Try
                Dim sqlDoc As String = ""
                Dim arlParm As New ArrayList

                sqlDoc += "SELECT DISTINCT wkgrpcd, wkgrpnmd, wkgrpgbn"
                sqlDoc += "  FROM lf066m"

                If rsSlipCd.Length = 1 Then
                    sqlDoc += " WHERE partcd = :partcd"
                    arlParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
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
                Throw (New Exception(ex.Message, ex))
            End Try
        End Function

        ' 의사USR
        Public Shared Function fnGet_RptDr_List() As DataTable
            Dim sFn As String = "Function fnGet_RptDr_List() As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT usrid, usrnm"
                sSql += "  FROM lf090m"
                sSql += " WHERE NVL(drspyn, '0') = '1'"
                sSql += "   AND NVL(delflg, '0') = '0'"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function


        ' 분야 목록 조회
        Public Shared Function fnGet_Part_List(Optional ByVal rbTake2Yn As Boolean = False, Optional ByVal rsViewGbn As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Part_List() As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT a.partcd, a.partnmd, MAX(b.dispseq) dispseq"
                sSql += "  FROM lf020m a, lf021m b"
                sSql += " WHERE a.partcd = b.partcd"
                sSql += "   AND a.usdt  <= fn_ack_sysdate"
                sSql += "   AND a.uedt  >  fn_ack_sysdate"
                sSql += "   AND b.usdt  <= fn_ack_sysdate"
                sSql += "   AND b.uedt  >  fn_ack_sysdate"

                If rsViewGbn <> "" Then
                    sSql += "   AND NVL(a.partgbn, '0') = '" + rsViewGbn + "'"
                End If

                If rbTake2Yn Then
                    sSql += "   AND NVL(b.take2yn, '0') = '1'"
                End If

                sSql += " GROUP BY a.partcd, a.partnmd"
                sSql += " ORDER BY dispseq, a.partcd"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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

    End Class

    '-- 결과관련 
    Public Class RstFn
        Private Const msFile As String = "File : CGLISAPP_COMM.vb, Class : LISAPP.COMM.RstFn" + vbTab

        '-- 특수결과 존재 여부
        Public Shared Function fnGet_SpRst_yn(ByVal rsBcNo As String, ByVal rsTestCd As String) As String
            Dim sFn As String = "Function fnGet_SpRst_yn(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim sTableNm As String = "lr010m"
                Dim al As New ArrayList

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql += "SELECT DISTINCT r.testcd"
                sSql += "  FROM " + sTableNm + " r,lf060m f6, lf310m f31"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                sSql += "  FROM lr052m"
                sSql += " WHERE bcno = :bcno"
                sSql += " ORDER BY regdt, seq"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '<< JJH 자체응급 여부
        Public Shared Function fnGet_ERYN(ByVal rsBcNo As String) As String
            Dim sFn As String = "Function fnGet_FN_Modify_Cmt(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList
                Dim dt As New DataTable

                sSql += "SELECT BCNO "
                sSql += "  FROM LJ015M "
                sSql += " WHERE BCNO = :BCNO "

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()

                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return "Y"
                Else
                    Return "N"
                End If

            Catch ex As Exception
                Return "N"
            End Try
        End Function

        '-- 검사항목 결과코드 조회
        Public Shared Function fnGet_Test_RstCdList(ByVal rsTestCds As String, ByVal rsWkGrpCd As String) As DataTable
            Dim sFn As String = "Function fnGet_Test_RstCdList(String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT testcd, keypad, rstcont, grade"
                sSql += "  FROM lf083m"

                If rsTestCds = "" Then
                    sSql += " WHERE testcd IN (SELECT testcd FROM lf066m WHERE wkgrpcd = :wgrpcd)"
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                Else
                    sSql += " WHERE testcd IN (" + rsTestCds + ")"
                End If

                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 검사명 가져오기
        Public Shared Function fnGet_ManualDiff_Tnmd(ByVal rsTclsCd As String, ByVal rsSpCd As String) As String
            Dim sFn As String = "Function getManualDiffName(string, string) As string"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim dt As New DataTable

                sSql += "SELECT tnmd FROM lf060m"
                sSql += " WHERE testcd = :testcd"
                sSql += "   AND spccd  = :spccd"
                sSql += "   AND usdt  <= fn_ack_sysdate"
                sSql += "   AND uedt  >  fn_ack_sysdate"

                alParm.Add(New OracleParameter("tclscd", OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("tnmd").ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 메뉴얼 Diff History
        Public Shared Function fnGet_ManualDiff_History(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As String
            Dim sFn As String = "Function fnGet_ManualDiff_History(string, string) As string"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim dt As New DataTable
                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql += "SELECT a.testcd, a.viewrst"
                sSql += "  FROM " + sTableNm + " a,"
                sSql += "       (SELECT bcno"
                sSql += "          FROM (SELECT rstdt, bcno FROM lr010m"
                sSql += "                 WHERE regno  = (SELECT regno FROM lj010m WHERE bcno = :bcno)"
                sSql += "                   AND testcd = :testcd"
                sSql += "                   AND spccd  = :spccd"
                sSql += "                   AND tkdt < (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd AND spccd = :spccd)"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- Keypad 항목 가져오기
        Public Shared Function fnGet_ManualDiff(ByVal rsTestCd As String, ByVal rsSpCd As String) As DataTable
            Dim sFn As String = "Function getManualDiff(string, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT b.testcd, b.tnmd, b.reqsub"
                sSql += "  FROM lf420m a, lf060m b"
                sSql += " WHERE a.testcd    LIKE :testcd || '%'"
                sSql += "   AND a.spccd     = :spccd"
                sSql += "   AND a.cnttestcd = b.testcd"
                sSql += "   AND a.spccd     = b.spccd"
                sSql += "   AND b.usdt     <= fn_ack_sysdate"
                sSql += "   AND b.uedt     >  fn_ack_sysdate"
                sSql += " ORDER BY testcd"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        ' Manual Diff 폼 구분 가져오기
        Public Shared Function fnGet_ManualDiff_FormGbn(ByVal rsTestCd As String, ByVal rsSpcCd As String) As String
            Dim sFn As String = "Function GetManualDiff_FormGbn(string, string) As String"
            Try
                Dim dt As New DataTable
                Dim sSql As String
                Dim alParm As New ArrayList

                sSql = "SELECT formgbn FROM lf420m WHERE testcd = :testcd AND spccd = :spccd AND ROWNUM = 1"

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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
    

        ' Manual Diff의 WBC 코드 가져오기
        Public Shared Function fnGet_ManualDiff_WBC_TestCd(ByVal rsTestCd As String, ByVal rsSpcCd As String) As String
            Dim sFn As String = "Function fnGet_ManualDiff_WBC_TestCd(string, string) As String"
            Try
                Dim dt As New DataTable
                Dim sSql As String
                Dim alParm As New ArrayList

                sSql = "SELECT wbctestcd FROM lf420m WHERE testcd = :testcd AND spccd = :spccd"

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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        ' Manual Diff의 WBC 결과값 가져오기
        Public Shared Function fnGet_ManualDiff_WBC_Rst(ByVal rsBcNo As String, ByVal rsTestCd As String) As String
            Dim sFn As String = "Function fnGet_ManualDiff_WBC_Rst(string, string) As String"
            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010"

                sSql += "SELECT orgrst FROM " + sTableNm
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try
        End Function

        ' Manual Diff % 코드 가져오기
        Public Shared Function fnGet_ManualDiff_Percent_TclsCd(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsCTestCd As String) As String
            Dim sFn As String = "Function GetManualDiff_FormGbn(string, string) As String"
            Try
                Dim dt As New DataTable
                Dim sSql As String
                Dim alParm As New ArrayList

                sSql = "SELECT pertestcd FROM lf420m WHERE testcd = :testcd AND spccd = :spccd AND cnttestcd = :ctestcd"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                alParm.Add(New OracleParameter("ctestcd", OracleDbType.Varchar2, rsCTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCTestCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try
        End Function

        '-- 해당검체에 검체명 가져오기
        Public Shared Function fnGet_SpcNmInfo(ByVal rsBcNo As String) As String
            Dim sFn As String = "Function fnGet_SpcNmInfo(string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT b.spcnmd FROM lj010m a, lf030m b"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '-- 계산식 결과 리턴
        Public Shared Function fnGet_Calc_DBQuery(ByVal rsCalcForm As String, _
                                            Optional ByVal ro_DbCn As OracleConnection = Nothing, _
                                            Optional ByVal ro_DbTrans As OracleTransaction = Nothing) As String
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
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '검사별 소견 결과 조회
        Public Shared Function fnGet_Rst_Comment_test(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Rst_Comment_test(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim sTableNm As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql += "SELECT r.bcno, r.partslip, r.slipnmd, fn_ack_get_bcno_comment_test(r.bcno, r.partslip) cmtcont, 'S' status"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT r.bcno, f.partcd || f.slipcd partslip, f2.slipnmd"
                sSql += "          FROM " + sTableNm + " r, lr030m r3,"
                sSql += "               lf060m f, lf021m f2"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try


        End Function

        ' 검사항목별 결과코드 조회
        Public Shared Function fnGet_test_rstinfo(ByVal rsBcNo As String, Optional ByVal r_DbCn As OracleConnection = Nothing) As DataTable
            Dim sFn As String = "Function fnGet_test_rstinfo(String, [oleDbConnection]) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT b.testcd, b.keypad, b.rstcont, b.grade, b.rstcdseq, b.rstlvl ,b.crtval" '<<<20180802 추가
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lm010m a, lf083m b"
                Else
                    sSql += "  FROM lr010m a, lf083m b"
                End If
                sSql += " WHERE a.bcno like :bcno || '%'"
                sSql += "   AND a.testcd = b.testcd"
                sSql += "   AND (a.spccd = b.spccd or b.spccd = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "')"
                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"


                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                sSql += "  FROM lrw11m a, lf083m b"
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
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                sSql += "  FROM lr010m a, lf083m b"
                sSql += " WHERE a.wkymd    = :wkymd"
                sSql += "   AND a.wkgrpcd  = :wgrpcd"
                sSql += "   AND a.wkno    >= :wknos"
                sSql += "   AND a.wkno    <= :wknoe"
                sSql += "   AND a.testcd   = b.testcd"
                sSql += "   AND (a.spccd   = b.spccd OR b.spccd = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "')"
                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"


                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkCd))
                alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                sSql += "  FROM lr010m a, lf083m b"
                sSql += " WHERE a.tkdt    >= :dates"
                sSql += "   AND a.tkdt    <= :datee || '5959'"
                sSql += "   AND (SUBSTR(a.testcd, 1, 5), a.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                sSql += "   AND a.testcd  = b.testcd"
                sSql += "   AND (a.spccd  = b.spccd OR b.spccd = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "')"
                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"


                alParm.Add(New OracleParameter("tkdts", OracleDbType.Varchar2, rsTkDts.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDts))
                alParm.Add(New OracleParameter("tkdte", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        ' 해당검체번호에 포함된 부서/분야 정보 조회
        Public Shared Function fnGet_SlipInfo_bcno(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "Function fnGet_SlipInfo_bcno"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If COMMON.CommLogin.LOGIN.PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql += "SELECT DISTINCT f2.partcd || f2.slipcd slipcd, f2.slipnmd, NVL(f2.dispseq, 999) sortkey"
                sSql += "  FROM " + sTableNm + " r, lf060m f6, lf021m f2"
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        '특정 검사결과 조회
        Public Shared Function fnGet_Pat_Recent_Rst(ByVal rsRegNo As String) As DataTable
            Dim sFN As String = "Public Shared Function fnGet_Pat_Recent_Rst() As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Try

                sSql += "SELECT y.* , TO_CHAR(TO_DATE(y.rstdt, 'yyyy-mm-dd hh24:miss'), 'YYYY-MM-DD') rstdtd, "
                sSql += "       f6.testcd, f6.rstunit "
                sSql += "  FROM ( "
                sSql += "        SELECT ROW_NUMBER() OVER(PARTITION BY x.testcd || x.spccd ORDER BY x.tkdt desc) num, x.* "
                sSql += "          FROM ("
                sSql += "                SELECT testcd, spccd, orgrst, viewrst, MAX(tkdt) tkdt, MAX(rstdt) rstdt "
                sSql += "                  FROM lr010m "
                sSql += "                 WHERE regno = :regno"
                sSql += "                   AND testcd IN ('LH101', 'LH12103') "
                sSql += "                   AND spccd = 'S01' "
                sSql += "                   AND rstflg IN ('2', '3') "
                sSql += "                GROUP BY testcd, spccd, orgrst, viewrst "
                sSql += "                ) x"
                sSql += "        ) y "
                sSql += "       , lf060m f6 "
                sSql += "  WHERE y.num = 1 "
                sSql += "    AND y.testcd = f6.testcd "
                sSql += "    AND y.spccd = f6.spccd "
                sSql += "    AND f6.usdt <= TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS')"
                sSql += "    AND f6.uedt > TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS')"
                sSql += "    ORDER BY f6.testcd"


                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand(False)
                Return DbExecuteQuery(sSql, alParm)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFN, ex))
            End Try
        End Function
        'Public Shared Function fnGet_Xpert_Comment(ByVal rsBcno As String) As DataTable
        '    '해당 환자의 Xpert PCR 검사 최근 1주일 검사결과 가져오는 쿼리 함수
        '    Dim sFn As String = "Public Shared Function fnGet_Xpert_Comment(ByVal rsBcno As String) As DataTable"
        '    Dim sSql As String = ""
        '    Dim alParm As New ArrayList
        '    Try

        '        sSql = ""
        '        sSql += "SELECT r.regno ,r.testcd , r.spccd ,  r.tkdt , r.bcno , TO_CHAR(TO_DATE(r.fndt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') fndt, r.viewrst"
        '        sSql += "  FROM lr010m r,"
        '        sSql += "  ("
        '        sSql += "   SELECT regno , tclscd , tkdt , bcno "
        '        sSql += "     FROM lj011m"
        '        sSql += "    WHERE bcno = :bcno"
        '        sSql += "      AND tclscd = 'LG104'"
        '        sSql += "  ) x"
        '        sSql += " WHERE r.regno = x.regno"
        '        sSql += "   AND r.testcd = x.tclscd"
        '        sSql += "   AND r.tkdt BETWEEN TO_CHAR(TO_DATE(x.tkdt , 'yyyy-mm-dd hh24:mi:ss') - 7, 'yyyymmddhh24miss') AND x.tkdt"
        '        sSql += "   AND r.rstflg = '3'"
        '        sSql += "   AND r.bcno NOT IN (:bcno)  "
        '        sSql += "   ORDER BY r.tkdt"

        '        alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
        '        alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

        '        DbCommand()
        '        Return DbExecuteQuery(sSql, alParm)
        '    Catch ex As Exception
        '        Throw (New Exception(ex.Message + " @" + sFn, ex))
        '    End Try
        'End Function
        '소견결과 조회
        Public Shared Function fnGet_Rst_Comment_slip(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Rst_Comment_slip(String) As DataTablev"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT r.bcno, r.partslip, r.slipnmd, fn_ack_get_bcno_comment_slip(r.bcno, r.partslip) cmtcont, 'S' status"
                sSql += "  FROM (SELECT DISTINCT a.bcno, a.partcd || a.slipcd partslip, b.slipnmd"
                sSql += "          FROM lr040m a, lf021m b"
                sSql += "         WHERE a.bcno   = :bcno"
                sSql += "           AND a.partcd = b.partcd"
                sSql += "           AND a.slipcd = b.slipcd"
                sSql += "           AND a.regdt >= b.usdt"
                sSql += "           AND a.regdt <  b.uedt"
                sSql += "       ) r"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        '20210303 jhs 사용자간 공유사항 코멘트 추가
        Public Shared Function fnGet_Rst_ShareComment_slip(ByVal rsRegno As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Rst_Comment_slip(String) As DataTablev"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "  SELECT DISTINCT a.regno, fn_ack_get_bcno_ShareCmt_slip(a.regno) cmtcont , 'S' status" + vbCrLf
                sSql += "          FROM lrc40m a" + vbCrLf
                sSql += "         WHERE a.regno   = :regno" + vbCrLf

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        '-------------------------------------

        Public Shared Function fnGet_GraedValue_C(ByVal rsTclsCd As String, ByVal rsRstVal As String) As String
            Dim sFn As String = "Private Function fnGet_GraedValue(String, String) As String"

            Try

                Dim dt As New DataTable
                Dim sSql As String = ""
                Dim sXpertRst As String = ""
                Dim alParm As New ArrayList
                Dim sValue As String = ""

                If rsTclsCd = "LG104" Then
                    If rsRstVal.IndexOf(Chr(13)) > 0 Then
                        sXpertRst = rsRstVal.Substring(0, rsRstVal.IndexOf(Chr(13)))
                    Else
                        sXpertRst = rsRstVal
                    End If
                End If

                sSql = ""
                sSql += "SELECT crtval FROM lf083m"
                sSql += " WHERE testcd  = :testcd"
                sSql += "   AND spccd   = '" + "".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"
                If rsTclsCd = "LG104" Then
                    'sSql += "   AND lower(rstcont) like '" + sXpertRst.ToLower + "%'"
                    sSql += "   AND lower(replace(rstcont, ' ', '')) LIKE lower(replace('" + sXpertRst + "' , ' ', ''))||'%'"
                Else
                    sSql += "   AND rstcont = :rstcont"
                End If

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))
                If rsTclsCd <> "LG104" Then alParm.Add(New OracleParameter("rstcont", OracleDbType.Varchar2, rsRstVal.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstVal))


                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then sValue = dt.Rows(0).Item(0).ToString().Trim

                Return sValue

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '20210810 jhs alter 결과코드 추가 
        Public Shared Function fnGet_GraedValue_A(ByVal rsTclsCd As String, ByVal rsRstVal As String) As String
            Dim sFn As String = "Private Function fnGet_GraedValue(String, String) As String"

            Try

                Dim dt As New DataTable
                Dim sSql As String = ""
                Dim sXpertRst As String = ""
                Dim alParm As New ArrayList
                Dim sValue As String = ""

                sSql = ""
                sSql += "SELECT altval FROM lf083m"
                sSql += " WHERE testcd  = :testcd"
                sSql += "   AND spccd   = '" + "".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"
                sSql += "   AND rstcont = :rstcont"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))
                alParm.Add(New OracleParameter("rstcont", OracleDbType.Varchar2, rsRstVal.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstVal))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then sValue = dt.Rows(0).Item(0).ToString().Trim

                Return sValue

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '------------------------------------------------

        Public Shared Function fnGet_Xpert_Comment(ByVal rsBcno As String, Optional ByVal RsCriticalGbn As Boolean = False) As DataTable
            '해당 환자의 Xpert PCR 검사 최근 1주일 검사결과 가져오는 쿼리 함수
            Dim sFn As String = "Public Shared Function fnGet_Xpert_Comment(ByVal rsBcno As String) As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList
          
            Try
                sSql = ""
                sSql += "SELECT r.regno ,r.testcd , r.spccd ,  r.tkdt , r.bcno , TO_CHAR(TO_DATE(r.fndt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') fndt, r.viewrst"
                sSql += "  FROM lr010m r,"
                sSql += "  ("
                sSql += "   SELECT regno , tclscd , tkdt , bcno "
                sSql += "     FROM lj011m"
                sSql += "    WHERE bcno = :bcno"
                sSql += "      AND tclscd = 'LG104'"
                sSql += "  ) x"
                sSql += " WHERE r.regno = x.regno"
                sSql += "   AND r.testcd = x.tclscd"
                sSql += "   AND r.tkdt BETWEEN TO_CHAR(TO_DATE(x.tkdt , 'yyyy-mm-dd hh24:mi:ss') - 7, 'yyyymmddhh24miss') AND x.tkdt"
                sSql += "   AND r.rstflg = '3'"
                sSql += "   AND r.bcno NOT IN (:bcno)  "
                If RsCriticalGbn = True Then
                    '   sSql += " AND LOWER(r.orgrst) LIKE '%m. tuberculosis : detected%'"
                    sSql += "AND lower(replace(r.orgrst, ' ', ''))  LIKE '%m.tuberculosis:detected%'"
                End If
                sSql += "   ORDER BY r.tkdt"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_AFB_Comment(ByVal rsBcno As String, Optional ByVal RsCriticalGbn As Boolean = False) As DataTable
            '해당 환자의 Xpert PCR 검사 최근 1주일 검사결과 가져오는 쿼리 함수
            Dim sFn As String = "Public Shared Function fnGet_AFB_Comment(ByVal rsBcno As String) As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList

            Try
                sSql = ""
                sSql += "SELECT r.regno ,r.testcd , r.spccd ,f3.spcnms,  r.fndt , r.bcno , TO_CHAR(TO_DATE(r.fndt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') fndt2, r.viewrst," + vbCrLf
                sSql += "       MAX(length(f3.spcnms)) spclen" + vbCrLf
                sSql += "  FROM lm010m r," + vbCrLf
                sSql += "  (" + vbCrLf
                sSql += "   SELECT regno , testcd , tkdt , bcno " + vbCrLf
                sSql += "     FROM lm010m" + vbCrLf
                sSql += "    WHERE bcno = :bcno" + vbCrLf
                sSql += "      AND testcd = 'LM205'" + vbCrLf
                sSql += "  ) x, lf083m f8 , lf030m f3" + vbCrLf
                sSql += " WHERE r.regno = x.regno" + vbCrLf
                sSql += "   AND r.testcd = x.testcd" + vbCrLf
                sSql += "   AND r.tkdt BETWEEN TO_CHAR(TO_DATE(x.tkdt , 'yyyy-mm-dd hh24:mi:ss') - 7, 'yyyymmddhh24miss') AND x.tkdt" + vbCrLf
                sSql += "   AND r.rstflg = '3'" + vbCrLf
                sSql += "   AND r.bcno NOT IN (:bcno)  " + vbCrLf
                sSql += "   AND r.testcd = f8.testcd" + vbCrLf
                sSql += "   AND f8.spccd = '00000'" + vbCrLf
                If RsCriticalGbn = True Then
                    sSql += "   AND f8.rstlvl = 'P'" + vbCrLf
                    sSql += "   AND f8.crtval = 'C' " + vbCrLf
                End If
                sSql += "   AND r.viewrst = f8.rstcont" + vbCrLf
                sSql += "   AND r.spccd = f3.spccd" + vbCrLf
                sSql += "   AND f3.usdt <= r.tkdt" + vbCrLf
                sSql += "   ANd f3.uedt > r.tkdt " + vbCrLf
                sSql += "GROUP BY r.regno," + vbCrLf
                sSql += "         r.testcd," + vbCrLf
                sSql += "         r.spccd," + vbCrLf
                sSql += "         f3.spcnms," + vbCrLf
                sSql += "         r.fndt," + vbCrLf
                sSql += "         r.bcno," + vbCrLf
                sSql += "         TO_CHAR (TO_DATE (r.fndt, 'yyyy-mm-dd hh24:mi:ss'), 'yyyy-mm-dd')," + vbCrLf
                sSql += "         r.viewrst" + vbCrLf
                sSql += "   ORDER BY r.fndt" + vbCrLf


                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        '20210712 NTM, MTB 검사 Critical 5년안에 ntm, 
        Public Shared Function fnGet_AFB_NTM_Comment(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_AFB_NTM_Comment(ByVal rsBcno As String) As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList

            Try
                sSql = ""
                sSql += "  selecT  CASE WHEN SUM(CASE WHEN SUBSTR (B.ORGRST, 1, 3) = 'Myc' THEN 1 ELSE 0 END) > 0" + vbCrLf
                sSql += "               THEN 'Y'" + vbCrLf
                sSql += "               ELSE 'N'" + vbCrLf
                sSql += "               END AS MTB," + vbCrLf
                sSql += "          CASE WHEN SUM(CASE WHEN ( (B.TESTCD = 'LM20101' OR B.TESTCD = 'LM20102') AND SUBSTR (B.ORGRST, 1, 3) = 'AFB')" + vbCrLf
                sSql += "                               OR ( (B.TESTCD = 'LM20302' OR B.TESTCD = 'LM20303') AND SUBSTR (B.ORGRST, 1, 3) = 'Liq')" + vbCrLf
                sSql += "                             THEN   1" + vbCrLf
                sSql += "                             ELSE   0 END) > 0" + vbCrLf
                sSql += "               THEN   'Y'" + vbCrLf
                sSql += "               ELSE   'N'" + vbCrLf
                sSql += "                END   AS NTM" + vbCrLf
                sSql += "   FROM (SELECT   MAX (TKDT) TKDT, REGNO" + vbCrLf
                sSql += "           FROM(LM010M)" + vbCrLf
                sSql += "           WHERE   BCNO = :bcno" + vbCrLf
                sSql += "           GROUP BY   REGNO) A" + vbCrLf
                sSql += "   , LM010M B" + vbCrLf
                sSql += "   , lf083m f83" + vbCrLf
                sSql += "   WHERE A.REGNO = B.REGNO" + vbCrLf
                sSql += "     AND B.TKDT BETWEEN TO_CHAR (ADD_MONTHS (TO_DATE (fn_ack_sysdate,'YYYY-MM-DD HH24:MI:SS'), - 60 ),'YYYYMMDDHH24MISS') AND  fn_ack_sysdate" + vbCrLf ' 모든 진행준 검사 포함하여 5년 안에 
                sSql += "     AND B.RSTFLG = '3'" + vbCrLf
                'sSql += "     and b.testcd in (selecT clsval from lf000m where clsgbn = 'NTM')"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        '---------------------------------------------

        Public Shared Function Fnget_Fkocs(ByVal rsBcno As String, ByVal tclscd As String) As DataTable
            '검사항목에 대한 처방키 가져오기
            Dim sFn As String = "Public Shared Function Fnget_Fkocs(ByVal rsBcno As String, ByVal tclscd as String) As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList

            Try
                sSql = ""
                sSql += "SELECT OCS_KEY, ORDDT "
                sSql += "  FROM LJ011M "
                sSql += " WHERE BCNO   = :bcno "
                sSql += "   AND TCLSCD = :tclscd "

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                alParm.Add(New OracleParameter("tclscd", OracleDbType.Varchar2, tclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, tclscd))

                DbCommand()

                Return DbExecuteQuery(sSql, alParm)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function Fnget_tnmd(ByVal rsBcno As String, ByVal tclscd As String) As DataTable
            '검사항목에 대한 처방키 가져오기
            Dim sFn As String = "Public Shared Function Fnget_Fkocs(ByVal rsBcno As String, ByVal tclscd as String) As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList

            Try
                sSql = " "
                sSql += "selecT m1.bcno, m1.testcd, m1.spccd , f6.tnmd from lm010m m1 " + vbCrLf
                sSql += " inner join lf060m f6 " + vbCrLf
                sSql += "    on  m1.testcd = f6.testcd and  m1.spccd = f6.spccd " + vbCrLf
                sSql += " where m1.bcno = :bcno " + vbCrLf
                sSql += "   and m1.tkdt >=  f6.usdt " + vbCrLf
                sSql += "   and m1.tkdt <= f6.uedt " + vbCrLf
                sSql += "   and m1.testcd = substr(:tclscd,1,5) " + vbCrLf


                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                alParm.Add(New OracleParameter("tclscd", OracleDbType.Varchar2, tclscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, tclscd))

                DbCommand()

                Return DbExecuteQuery(sSql, alParm)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        '-- 검체의 결과조회
        Public Shared Function fnGet_Result_bcno(ByVal rsBcNo As String, ByVal rsSlipCd As String, ByVal rbBcNoAll As Boolean, ByVal rsTestCds As String, _
                                         ByVal rsWkGrpCd As String, ByVal rsEqCd As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_bcno"

            Try
                Dim sSql As String = ""

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then  ''' 미생물 
                    sSql = "pkg_ack_rst.pkg_get_result_bcno_m"
                Else
                    sSql = "pkg_ack_rst.pkg_get_result_bcno"
                End If

                Dim oParm As New DBORA.DbParrameter

                With oParm
                    .AddItem("rs_bcno", OracleDbType.Varchar2, ParameterDirection.Input, rsBcNo.Substring(0, 14))
                    .AddItem("rs_slipcd", OracleDbType.Varchar2, ParameterDirection.Input, rsSlipCd)
                End With

                DbCommand(False)

                Dim dt As DataTable = DbExecuteQuery(sSql, oParm, False)

                Dim a_dr As DataRow()

                If rsEqCd <> "" Then
                    a_dr = dt.Select("(eqcd = '" + rsEqCd + "' OR (tcdgbn = 'P' AND titleyn = '0'))", "sort1, sort2, tclscd, sort3, testcd")
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- w/l별 결과 결과조회
        Public Shared Function fnGet_Result_wl(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsRstNullReg As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_wl"

            Try
                Dim sSql As String = ""
                Dim oleParm As New DBORA.DbParrameter

                With oleParm
                    .AddItem("rs_wluid", OracleDbType.Varchar2, ParameterDirection.Input, rsWLUid)
                    .AddItem("rs_wlymd", OracleDbType.Varchar2, ParameterDirection.Input, rsWLYmd)
                    .AddItem("rs_wltitle", OracleDbType.Varchar2, ParameterDirection.Input, rsWLTitle)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery("pkg_ack_rst.pkg_get_result_wl", oleParm, False)

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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 작업번호별 결과 결과조회
        Public Shared Function fnGet_Result_wgrp(ByVal rsWkYmd As String, ByVal rsWkCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                                 ByVal rsTestCds As String, ByVal rsRstNullReg As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_wgrp"

            Try
                Dim sSql As String = ""
                Dim oleParm As New DBORA.DbParrameter

                With oleParm
                    .AddItem("rs_wkymd", OracleDbType.Varchar2, ParameterDirection.Input, rsWkYmd)
                    .AddItem("rs_wkgrp", OracleDbType.Varchar2, ParameterDirection.Input, rsWkCd)
                    .AddItem("rs_wknos", OracleDbType.Varchar2, ParameterDirection.Input, rsWkNoS)
                    .AddItem("rs_wknoe", OracleDbType.Varchar2, ParameterDirection.Input, rsWkNoE)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery("pkg_ack_rst.pkg_get_result_wgrp", oleParm, False)

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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 검사그룹별 
        Public Shared Function fnGet_Result_tgrp(ByVal rsTGrpCd As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, _
                                                 ByVal rsTestCds As String, ByVal rsRstNullReg As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_tgrp"

            Try
                Dim sSql As String = ""
                Dim oleParm As New DBORA.DbParrameter

                With oleParm
                    .AddItem("rs_tkdts", OracleDbType.Varchar2, ParameterDirection.Input, rsTkDtS)
                    .AddItem("rs_tkdte", OracleDbType.Varchar2, ParameterDirection.Input, rsTkDtE)
                    .AddItem("rs_tgrpcd", OracleDbType.Varchar2, ParameterDirection.Input, rsTGrpCd)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery("pkg_ack_rst.pkg_get_result_tgrp", oleParm, False)

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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '-- 환자정보 조회(AxRstPatInfo)
        Public Shared Function fnGet_PatInfo(ByVal rsBcNo As String, ByVal rsSlipCd As String) As DataTable
            Dim sFn As String = "Public Shared Function FindDiagNm(String, String) As String"
            Try
                Dim sTableNm As String = "lr010m"
                Dim sSql As String = ""
                Dim al As New ArrayList

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.regno, j.sex," + vbCrLf
                sSql += "       CASE WHEN j.dage <= 31  THEN TO_CHAR(j.dage) || 'd'" + vbCrLf
                sSql += "            WHEN j.dage >  365 THEN TO_CHAR(j.age) ELSE TO_CHAR(TRUNC(j.dage/30)) || 'm' END age," + vbCrLf
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo," + vbCrLf
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm," + vbCrLf
                sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm," + vbCrLf
                sSql += "       fn_ack_get_dept_code(j.iogbn, j.deptcd) deptcd," + vbCrLf
                sSql += "       j.iogbn, fn_ack_get_ward_abbr(j.wardno) wardno, j.roomno," + vbCrLf
                sSql += "       fn_ack_date_str(j.entdt, 'yyyy-mm-dd') entdt," + vbCrLf
                sSql += "       CASE WHEN j.statgbn = '1' THEN 'Y' ELSE j.statgbn END statgbn," + vbCrLf
                sSql += "       j2.height, j2.weight," + vbCrLf
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi:ss') colldt," + vbCrLf
                sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt," + vbCrLf
                sSql += "       CASE WHEN j1.rstflg = '3' THEN fn_ack_date_str(j1.rstdt, 'yyyy-mm-dd hh24:mi:ss') ELSE '' END rstdt," + vbCrLf
                sSql += "       f3.spcnmd, j3.diagnm," + vbCrLf
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)" + vbCrLf
                sSql += "          FROM lj011m ff" + vbCrLf
                sSql += "         WHERE bcno    = j.bcno" + vbCrLf
                sSql += "           AND spcflg IN ('1', '2', '3', '4')" + vbCrLf
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '" + vbCrLf
                sSql += "       ) doctorrmk," + vbCrLf
                sSql += "       (SELECT abo || rh FROM lr070m WHERE regno = j.regno) aborh," + vbCrLf
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno," + vbCrLf
                sSql += "       CASE WHEN LENGTH(r.workno) = 8 THEN '' ELSE fn_ack_get_bcno_full(r.workno) END workno," + vbCrLf
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno," + vbCrLf
                sSql += "       j.resdt /*fn_ack_get_ocs_resdt_bcno(j.bcno)*/ resdt," + vbCrLf
                sSql += "       r.tat_mi" + vbCrLf
                sSql += "  FROM lj011m j1, lf030m f3," + vbCrLf
                sSql += "       (SELECT bcno, MAX(wkymd || NVL(wkgrpcd, '') || NVL(wkno, '')) workno," + vbCrLf
                sSql += "               fn_ack_date_diff(MIN(NVL(wkdt, tkdt)), MIN(NVL(rstdt, fn_ack_sysdate)), '0') tat_mi" + vbCrLf
                sSql += "          FROM " + sTableNm + "" + vbCrLf
                sSql += "         WHERE bcno LIKE :bcno || '%'" + vbCrLf

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                If rsSlipCd.Length = 2 Then
                    sSql += "           AND (testcd, spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd)" + vbCrLf
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                ElseIf rsSlipCd.Length = 1 Then
                    sSql += "           AND (testcd, spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd)" + vbCrLf
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If

                sSql += "         GROUP BY bcno" + vbCrLf
                sSql += "       ) r, lj010m j, lj012m j2, lj013m j3" + vbCrLf
                sSql += " WHERE j.bcno     = :bcno" + vbCrLf
                sSql += "   AND j.bcno     = j1.bcno" + vbCrLf
                sSql += "   AND j.bcno     = r.bcno" + vbCrLf
                sSql += "   AND j.spccd    = f3.spccd" + vbCrLf
                sSql += "   AND j1.colldt >= f3.usdt" + vbCrLf
                sSql += "   AND j1.colldt <  f3.uedt" + vbCrLf
                sSql += "   AND j.spcflg   = '4'" + vbCrLf
                sSql += "   AND j.bcno     = j2.bcno (+)" + vbCrLf
                sSql += "   AND j.bcno     = j3.bcno (+)" + vbCrLf

                al.Add(New OracleParameter("bcno", rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))

            End Try
        End Function

        'JJH 특수보고서 이전결과 검사코드
        Public Shared Function fnGet_BfRst_Testcd() As DataTable
            Dim sFn As String = "Public Shared Function fnGet_BfRst_Testcd() As DataTable"
            Try

                Dim sSql As String = ""

                sSql += "SELECT CLSVAL  "
                sSql += "  FROM lf000m  "
                sSql += " WHERE clsgbn = 'NCOV' "
                sSql += "   AND clscd  = 'CHK'  "

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
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
                sSql += "SELECT TO_CHAR(" + rsCalForm.Replace("^", "POWER").Replace("↓", "least").Replace("↑", "greatest") + ") FROM DUAL"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql)
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_CalcTests(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsSpcCd As String) As DataTable

            Dim sFn As String = "Function fnGet_CalcTests(string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If COMMON.CommLogin.LOGIN.PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       a.testcd, a.spccd, a.calrange, b.tnmd, c.spcnmd,"
                sSql += "       a.paramcnt, a.param0, a.param1, a.param2, a.param3,"
                sSql += "       a.param4, a.param5, a.param6, a.param7, a.param8,"
                sSql += "       a.param9, a.calform"
                sSql += "  FROM lf069m a, lf060m b, lf030m c"
                sSql += " WHERE a.testcd   = :testcd"
                sSql += "   AND a.spccd    = :spccd"
                'sSql += "   AND a.calrange = 'B'"
                sSql += "   AND a.testcd   = b.testcd"
                sSql += "   AND a.spccd    = b.spccd"
                sSql += "   AND b.usdt    <= (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd)"
                sSql += "   AND b.uedt    >  (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd)"
                sSql += "   AND b.spccd    = c.spccd"
                sSql += "   AND c.usdt    <= (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd)"
                sSql += "   AND c.uedt    >  (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND testcd = :testcd)"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTclsCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

    End Class

    '-- 소견자동변환
    Public Class CvtCmt
        Private Const msFile As String = "File : CGLISAPP_COMM.vb, Class : LISAPP.COMM.CvtCmt" + vbTab

        Public Shared Function fnCvtCmtInfo(ByVal rsBcNo As String, ByVal r_al_Rst As ArrayList, ByVal rsSlipCd As String, Optional ByVal rbLisMode As Boolean = False, _
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
                            For ix2 As Integer = 0 To r_al_Rst.Count - 1
                                If CType(r_al_Rst(ix2), STU_CvtCmtInfo).TestCd = CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).TestCd Then
                                    CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).OrgRst = CType(r_al_Rst(ix2), STU_CvtCmtInfo).OrgRst
                                    CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).ViewRst = CType(r_al_Rst(ix2), STU_CvtCmtInfo).ViewRst
                                    CType(alCvtInfo_Item(ix1), STU_CvtCmtInfo).EqFlag = CType(r_al_Rst(ix2), STU_CvtCmtInfo).EqFlag
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
                                        objRet.CmtCont += vbNewLine
                                        objRet.CmtCont += vbNewLine
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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally

                If ro_DbCn Is Nothing Then
                    If dbTran IsNot Nothing Then dbTran.Dispose() : dbTran = Nothing

                    If dbCn.State = ConnectionState.Open Then dbCn.Close()
                    dbCn.Dispose() : dbCn = Nothing
                End If
            End Try

        End Function

        Private Shared Function fnGet_CvtCmtInfo(ByVal rsBcNo As String, ByVal rsSlipCd As String, ByVal ro_DbCn As OracleConnection, ByVal ro_DbTran As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnGet_CvtCmtInfo(String) As Boolean"

            Try

                Dim dt As DataTable = LISAPP.COMM.CvtCmt.fnGet_CvtCmt_State_BcNo(rsBcNo, rsSlipCd, ro_DbCn, ro_DbTran)
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

        Private Shared Function fnGet_CvtCmtInfo_Items(ByVal rsBcNo As String, ByVal rsCmtCd As String, ByVal ro_DbCn As OracleConnection, ByVal ro_DbTran As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnGet_CvtCmtInfo_Items(string, string, string) As ArrayList"

            Try

                Dim dt As DataTable = LISAPP.COMM.CvtCmt.fnGet_CvtCmtInfo_BcNo(rsBcNo, rsCmtCd, ro_DbCn, ro_DbTran)

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

            If rsBcNo = "" Then Return New DataTable

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT r.bcno, c.cmtcd, b.cvtform, d.partcd || d.slipcd slipcd, d.cmtcont, MIN(NVL(r.rstflg, '0')) minrstflg"
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lm010m r, lf081m b, lf082m c, lf080m d"
                Else
                    sSql += "  FROM lr010m r, lf081m b, lf082m c, lf080m d"
                End If
                sSql += " WHERE r.bcno   = :bcno"
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
                    sSql += "  FROM lm010m r, lf082m c, lf060m f"
                Else
                    sSql += "  FROM lr010m r, lf082m c, lf060M f"
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

        Public Shared Function fnCvtRstInfo(ByVal rsBcNo As String, ByVal r_al_Rst As ArrayList, Optional ByVal rbIFGbn As Boolean = False, _
                                    Optional ByVal ro_DbCn As OracleConnection = Nothing, _
                                    Optional ByVal ro_DbTran As OracleTransaction = Nothing) As ArrayList
            Dim sFn As String = "Public Shared Function fnCvtRstInfo(String, ArrayList, [Boolean], [oracleConnection], [oracleTransaction]) As ArrayList"

            Dim dbCn As OracleConnection = ro_DbCn
            Dim dbTran As OracleTransaction = ro_DbTran

            Try
                If ro_DbCn Is Nothing Then dbCn = GetDbConnection()

                Dim alReturn As New ArrayList

                Dim alCvtInfo As ArrayList = fnGet_CvtRstInfo(rsBcNo, "", rbIFGbn, dbCn, dbTran)
                If alCvtInfo.Count < 1 Then Return New ArrayList

                For ix1 As Integer = 0 To alCvtInfo.Count - 1
                    For ix2 As Integer = 0 To r_al_Rst.Count - 1
                        If CType(alCvtInfo(ix1), STU_RstInfo_cvt).TestCd = CType(r_al_Rst(ix2), STU_RstInfo_cvt).TestCd Then
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).OrgRst = CType(r_al_Rst(ix2), STU_RstInfo_cvt).OrgRst
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).ViewRst = CType(r_al_Rst(ix2), STU_RstInfo_cvt).ViewRst
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).HlMark = CType(r_al_Rst(ix2), STU_RstInfo_cvt).HlMark
                            Exit For
                        End If
                    Next
                Next

                For ix As Integer = 0 To alCvtInfo.Count - 1
                    Dim alCvtInfo_Item As ArrayList = fnGet_CvtRstInfo_Items(CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtRange, rsBcNo, CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd, CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd, CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCdSeq, CType(alCvtInfo(ix), STU_RstInfo_cvt).OrgRst, dbCn, dbTran)
                    If alCvtInfo_Item.Count > 0 Then

                        For ix1 As Integer = 0 To alCvtInfo_Item.Count - 1
                            For ix2 As Integer = 0 To r_al_Rst.Count - 1
                                If CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CTestCd = CType(r_al_Rst(ix2), STU_RstInfo_cvt).TestCd Then
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst = CType(r_al_Rst(ix2), STU_RstInfo_cvt).OrgRst
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).ViewRst = CType(r_al_Rst(ix2), STU_RstInfo_cvt).ViewRst
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).HlMark = CType(r_al_Rst(ix2), STU_RstInfo_cvt).HlMark
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
                                    Else
                                        objRet.OrgRst = CType(alCvtInfo(ix), STU_RstInfo_cvt).OrgRst
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

        Public Shared Function fnCvtRstInfo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal r_al_Rst As ArrayList, Optional ByVal rbIFGbn As Boolean = False, _
                                    Optional ByVal ro_DbCn As OracleConnection = Nothing, _
                                    Optional ByVal ro_DbTran As OracleTransaction = Nothing) As ArrayList
            Dim sFn As String = "Public Shared Function fnCvtRstInfo(String, ArrayList, [Boolean], [oracleConnection], [oracleTransaction]) As ArrayList"

            Dim dbCn As OracleConnection = ro_DbCn
            Dim dbTran As OracleTransaction = ro_DbTran

            Try
                If ro_DbCn Is Nothing Then dbCn = GetDbConnection()

                Dim alReturn As New ArrayList

                Dim alCvtInfo As ArrayList = fnGet_CvtRstInfo(rsBcNo, rsTestCd, rbIFGbn, dbCn, dbTran)
                If alCvtInfo.Count < 1 Then Return New ArrayList

                For ix1 As Integer = 0 To alCvtInfo.Count - 1
                    For ix2 As Integer = 0 To r_al_Rst.Count - 1
                        If CType(alCvtInfo(ix1), STU_RstInfo_cvt).TestCd = CType(r_al_Rst(ix2), STU_RstInfo_cvt).TestCd Then
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).OrgRst = CType(r_al_Rst(ix2), STU_RstInfo_cvt).OrgRst
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).ViewRst = CType(r_al_Rst(ix2), STU_RstInfo_cvt).ViewRst
                            CType(alCvtInfo(ix1), STU_RstInfo_cvt).HlMark = CType(r_al_Rst(ix2), STU_RstInfo_cvt).HlMark
                            Exit For
                        End If
                    Next
                Next

                For ix As Integer = 0 To alCvtInfo.Count - 1
                    Dim alCvtInfo_Item As ArrayList = fnGet_CvtRstInfo_Items(CType(alCvtInfo(ix), STU_RstInfo_cvt).CvtRange, rsBcNo, CType(alCvtInfo(ix), STU_RstInfo_cvt).TestCd, CType(alCvtInfo(ix), STU_RstInfo_cvt).SpcCd, CType(alCvtInfo(ix), STU_RstInfo_cvt).RstCdSeq, CType(alCvtInfo(ix), STU_RstInfo_cvt).OrgRst, dbCn, dbTran)
                    If alCvtInfo_Item.Count > 0 Then

                        For ix1 As Integer = 0 To alCvtInfo_Item.Count - 1
                            For ix2 As Integer = 0 To r_al_Rst.Count - 1
                                If CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).CTestCd = CType(r_al_Rst(ix2), STU_RstInfo_cvt).TestCd Then
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).OrgRst = CType(r_al_Rst(ix2), STU_RstInfo_cvt).OrgRst
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).ViewRst = CType(r_al_Rst(ix2), STU_RstInfo_cvt).ViewRst
                                    CType(alCvtInfo_Item(ix1), STU_RstInfo_cvt).HlMark = CType(r_al_Rst(ix2), STU_RstInfo_cvt).HlMark
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
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            Finally
                If ro_DbCn Is Nothing Then
                    If dbTran IsNot Nothing Then dbTran.Dispose() : dbTran = Nothing
                    If dbCn.State = ConnectionState.Open Then dbCn.Close()
                    dbCn.Dispose() : dbCn = Nothing
                End If

            End Try

        End Function

        Private Shared Function fnGet_CvtRstInfo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rbIFGbn As Boolean, _
                                                 ByVal ro_DbCn As OracleConnection, ByVal ro_DbTran As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnCvtRstInfo_State(String) As Boolean"

            Try

                Dim dt As DataTable = LISAPP.COMM.CvtRst.fnGet_CvtRst_State_BcNo(rsBcNo, rsTestCd, rbIFGbn, ro_DbCn, ro_DbTran)
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
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Private Shared Function fnGet_CvtRstInfo_Items(ByVal rsRange As String, ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsSpcCd As String, ByVal rsRstSeq As String, ByVal rsOrgRst As String, _
                                                       ByVal ro_DbCn As OracleConnection, ByVal ro_DbTrans As OracleTransaction) As ArrayList
            Dim sFn As String = "Private Function fnGet_CvtRstInfo_Items(string, string, string, String) As ArrayList"

            Try
                Dim dt As DataTable = LISAPP.COMM.CvtRst.fnGet_CvtRstInfo_BcNo(rsBcNo, rsTclsCd, rsSpcCd, rsRstSeq, ro_DbCn, ro_DbTrans)
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
                Fn.log(msFile + sFn, Err)
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
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lm010m r, lf084m c, lf083m d"
                Else
                    sSql += "  FROM lr010m r, lf084m c, lf083m d"
                End If

                If rsTestCd <> "" Then
                    sSql += ", lf085m e"
                End If

                sSql += " WHERE r.bcno     = :bcno"

                If rsTestCd <> "" Then
                    sSql += "   AND e.ctestcd   = :testcd"
                    sSql += "   AND c.testcd    = e.testcd"
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
                Fn.log(msFile + sFn, Err)
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
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lm010m r, lf085m c, lf060m f"
                Else
                    sSql += "  FROM lr010m r, lf085m c, lf060m f"
                End If
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
                Fn.log(msFile + sFn, Err)
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

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lj010m j, lm010m r, lf085m c, lf060m f,"
                Else
                    sSql += "  FROM lj010m j, lr010m r, lf085m c, lf060m f,"
                End If
                sSql += "       (SELECT regno, orddt FROM lj010m WHERE bcno = :bcno') t"
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

    '-- 검체번호 관련
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

                        rsNo = fnFind_WkNo_From_PrtWkNo(rsNo)

                        sSql = ""
                        sSql += "SELECT bcno FROM lr010m"
                        sSql += " WHERE wkymd   = :wkymd"
                        sSql += "   AND wkgrpcd = :wgrpcd"
                        sSql += "   AND wkno    = :wkno"
                        sSql += " UNION "
                        sSql += "SELECT bcno FROM lm010m"
                        sSql += " WHERE wkymd   = :wkymd"
                        sSql += "   AND wkgrpcd = :wgrpcd"
                        sSql += "   AND wkno    = :wkno"

                        al.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, 8, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsNo.Substring(0, 8)))
                        al.Add(New OracleParameter("wkgrp", OracleDbType.Varchar2, 2, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsNo.Substring(8, 2)))
                        al.Add(New OracleParameter("wkno", OracleDbType.Varchar2, 4, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsNo.Substring(10, 4)))

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
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '# PrtWkNo(yyMMdd__1234) --> WkNo(yyyyMMdd__1234)
        Public Shared Function fnFind_WkNo_From_PrtWkNo(ByVal rsNo As String) As String
            Dim sFn As String = "Public Shared Function fnFind_WkNo_From_PrtWkNo(String) As String"
            Try
                Dim sReturn As String = ""

                '2100년에 2001년 바코드 사용하는 경우
                If Now.Year < Convert.ToInt32(Now.ToShortDateString().Substring(0, 2) + rsNo.Substring(0, 2)) Then
                    sReturn = (Convert.ToInt32(Now.ToShortDateString().Substring(0, 2)) - 1).ToString() + rsNo
                Else
                    sReturn = Now.ToShortDateString().Substring(0, 2) + rsNo
                End If

                Return sReturn

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
    End Class


End Namespace