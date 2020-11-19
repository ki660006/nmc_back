Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Namespace APP_T

    Public Class SrhFn
        Private Const msFile As String = "File : CGRISAPP_T.vb, Class : RISAPP.APP_T.SrhFn" + vbTab

        '-- 검사통계
        Public Function fnGet_Test_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, ByVal rsADN As String, _
                                              ByVal rsTM1 As String, ByVal rsTM2 As String, ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsAbRst As String, _
                                              ByVal rsPart As String, ByVal rsSlip As String, ByVal rsBcclsCd As String, ByVal rsWkGrp As String, _
                                              ByVal rsTestCds As String, ByVal rsSame As String, ByVal rsSpc As String, ByVal rsMinusExLab As String, ByVal rsTCdGbn As String, _
                                              ByVal rsTGrpCd As String, ByVal rbIoGbn_NotC As Boolean) As DataTable
            Dim sFn As String = "fnGet_Test_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                '이상자구분 변수 -> 컬럼명과 일치하도록 변경
                If rsAbRst.Length > 0 Then rsAbRst = "ab" + rsAbRst

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                If rsSpc = "" Then
                    sSql += "SELECT CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END testcd, '' spccd,"
                    sSql += "       MIN(b.tnmd) tnm, '' spcnm,"
                Else
                    sSql += "SELECT CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END testcd, a.spccd spccd,"
                    sSql += "       MIN(b.tnmd) tnm, c.spcnmd spcnm,"
                End If
                sSql += "       SUM(a.stcnt" + rsAbRst + ") ctotal,"

                For i As Integer = 1 To ra_sDMY.Length
                    Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                        Case 8
                            '일별 - 일자
                            sSql += "       SUM(CASE WHEN a.styymmdd = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

                        Case 10
                            '일별 - 순차시간, 일별 - 시간대
                            sSql += "       SUM(CASE WHEN a.styymmdd + a.sthh = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

                        Case 6
                            '월별
                            sSql += "       SUM(CASE WHEN a.styymm = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

                        Case 4
                            '연별
                            sSql += "       SUM(CASE WHEN a.styy = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

                    End Select

                    If i = ra_sDMY.Length Then
                        sSql += ""
                    Else
                        sSql += ","
                    End If
                Next

                sSql += "  FROM " + IIf(bIO Or rbIoGbn_NotC, "rt011m", "rt010m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "       ("
                sSql += "        SELECT testcd, MIN(tnmd) tnmd, NVL(MIN(samecd), testcd) samecd"
                sSql += "          FROM rf060m"
                sSql += "         WHERE usdt <= fn_ack_sysdate"

                If rsPart.Length > 0 Then
                    sSql += "           AND partcd = :partcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPart.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPart))
                End If

                If rsSlip.Length > 0 Then
                    sSql += "           AND partcd = :partcd"
                    sSql += "           AND slipcd = :slipcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlip.Substring(0, 1)))
                End If

                If rsBcclsCd.Length > 0 Then
                    sSql += "           AND bcclscd = :bcclscd"
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                If rsMinusExLab.Length > 0 Then
                    sSql += "           AND NVL(exlabyn, '0') = '0'"
                End If

                If rsWkGrp.Length > 0 Then
                    sSql += "           AND SUBSTR(testcd, 1, 5), spccd IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM rf066m WHERE wkgrpcd = :wkgrp)"
                    alParm.Add(New OracleParameter("wkgrp", OracleDbType.Varchar2, rsWkGrp.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrp))
                End If

                If rsTCdGbn.IndexOf(",") > 0 Then
                    sSql += "           AND tcdgbn IN (" + rsTCdGbn + ")"
                ElseIf rsTCdGbn <> "" Then
                    sSql += "           AND tcdgbn = :tcdgbn"
                    alParm.Add(New oracleParameter("tcdgbn", rsTCdGbn))
                End If

                sSql += "         GROUP BY testcd"
                sSql += "       ) b ON a.testcd = b.testcd"
                If rsSpc = "Y" Then
                    sSql += "       INNER JOIN"
                    sSql += "       ("
                    sSql += "        SELECT spccd, min(spcnmd) spcnmd"
                    sSql += "          FROM lf030m"
                    sSql += "         GROUP BY spccd"
                    sSql += "       ) c ON a.spccd = c.spccd"
                End If

                Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                    Case 8
                        '일별 - 일자
                        sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' and a.styymmdd <= '" + rsDT2.Replace("-", "") + "'"

                    Case 10
                        '일별 - 순차시간, 일별 - 시간대
                        If rsTM1.Length = 0 And rsTM2.Length = 0 Then
                            '순차시간
                            sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' AND a.styymmdd <= '" + rsDT2.Replace("-", "") + "'"
                            sSql += "   AND a.styymmdd + a.sthh >= '" + ra_sDMY(0).Replace("-", "").Replace(" ", "") + "'"
                            sSql += "   AND a.styymmdd + a.sthh <= '" + ra_sDMY(ra_sDMY.Length - 1).Replace("-", "").Replace(" ", "") + "'"
                        Else
                            '시간대
                            sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' AND a.styymmdd <= '" + rsDT2.Replace("-", "") + "'"
                            sSql += "   AND a.sthh >= '" + rsTM1 + "' and a.sthh <= '" + rsTM2 + "'"
                        End If

                    Case 6
                        '월별
                        sSql += " WHERE a.styymm >= '" + rsDT1.Replace("-", "") + "' AND a.styymm <= '" + rsDT2.Replace("-", "") + "'"

                    Case 4
                        '연별
                        sSql += " WHERE a.styy >= '" + rsDT1.Replace("-", "") + "' AND a.styy <= '" + rsDT2.Replace("-", "") + "'"

                End Select

                sSql += "   AND a.sttype = :sttype"
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                Select Case rsADN
                    Case "D" : sSql += "   AND a.sthh >= '08' AND a.sthh <= '16'"
                    Case "N" : sSql += "   AND a.sthh >= '00' AND a.sthh <= '07' AND a.sthh >= '17' AND a.sthh <= '23'"
                End Select

                If rsIO <> "" Then
                    If rsIO = "O" Then
                        sSql += "   AND a.stioflg in ('O','S','E') "
                    Else
                        sSql += "   AND a.stioflg = '" + rsIO + "'"

                    End If
                End If

                If rsDept <> "" Then
                    sSql += "   AND a.stdeptcd = '" + rsDept + "'"
                End If

                If rsWard <> "" Then
                    sSql += "   AND a.stdeptcd = '" + rsWard + "'"
                End If

                If rsTGrpCd.Length > 0 Then
                    sSql += "   AND SUBSTR(a.testcd, 1, 5), a.spccd IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                If rsTestCds.Length > 0 Then
                    sSql += "   AND a.testcd IN (" + rsTestCds + ")"
                End If

                If rbIoGbn_NotC Then sSql += "   AND a.stioflg <> 'C'"

                If rsSpc = "Y" Then
                    sSql += " GROUP BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, a.spccd, c.spcnmd"
                Else
                    sSql += " GROUP BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END"
                End If
                sSql += " ORDER BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, spccd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 검사통계 작업 리스트
        Public Function fnGet_Test_AnalysisInfo(ByVal rsDayB As String, ByVal rsDayE As String, ByVal rsType As String) As DataTable
            Dim sFn As String = "fnGet_Test_AnalysisInfo(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT t.styymmdd, t.sttype, fn_ack_date_str(t.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "       t.regid, fn_ack_get_usr_name(t.regid) regnm"
                sSql += "  FROM rt001m t"
                sSql += " WHERE t.styymmdd >= :dates"
                sSql += "   AND t.styymmdd <= :datee"
                sSql += "   AND t.sttype    = :sttype"

                Dim al As New ArrayList

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayB.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayB))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                al.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al, True)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '--TAT 시간대별 통계
        Public Function fnGet_TatTime_Statistics(ByVal rsQryGbn As String, ByVal rsRstflg As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartSlip As String, _
                                                 ByVal rsDeptCd As String, ByVal rsWardNo As String, ByVal rsIOGbn As String, _
                                                 ByVal rsEmerYn As String, ByVal raTests As ArrayList, _
                                                 ByVal rbVerity As Boolean, ByVal rbNotPDCA As Boolean) As DataTable
            Dim sFn As String = "Public Function fnGet_TatTime_Statistics(String, String, String, String, String, String, String, String, ArrayList)"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rsQryGbn = "" Then
                    '결과단위 TAT
                    sSql = ""
                    sSql += "SELECT f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,"
                    If rsRstflg = "2" Then
                        sSql += "       f6.prptmi tmi, fn_ack_date_diff(r.tkdt, r.mwdt, '3') tat_mi,"
                    Else
                        sSql += "       f6.frptmi tmi, fn_ack_date_diff(r.tkdt, r.fndt, '3') tat_mi,"
                    End If
                    sSql += "       count(*) totcnt"
                    sSql += "  FROM rf060m f6,"
                    sSql += "       ("
                    sSql += "        SELECT bcno, testcd, spccd, tkdt, NVL(mwdt, fndt) mwdt, fndt"
                    sSql += "          FROM rr010m r"
                    sSql += "         WHERE tkdt >= :dates"
                    sSql += "           AND tkdt <= :datee || '235959'"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                    If rbNotPDCA Then
                        sSql += "           AND NVL(panicmark, '-') = '-' AND NVL(deltamark, '-') = '-' AND NVL(crticalmark, '-') = '-' AND NVL(alertmark, '-') = '-'"
                    End If

                    If raTests.Count > 0 Then
                        sSql += "           AND testcd IN ("
                        For ix As Integer = 0 To raTests.Count - 1
                            If ix > 0 Then
                                sSql += ", "
                            End If
                            sSql += ":testcd" + ix.ToString

                            alParm.Add(New OracleParameter("testcd" + ix.ToString, raTests.Item(ix).ToString))
                        Next
                        sSql += ")"
                    End If

                    If rsRstflg = "2" Then
                        sSql += "           AND NVL(mwdt, '-') <> '-'"
                    Else
                        sSql += "           AND NVL(fndt, '-') <> '-'"
                    End If

                    If rbVerity Then
                        sSql += "           AND bcno NOT IN (SELECT bcno FROM rr051m"
                        sSql += "                             WHERE bcno   = r.bcno"
                        sSql += "                               AND testcd = r.testcd"
                        sSql += "                           )"

                    End If

                    sSql += "       ) r,"
                    sSql += "       rj010m j,"
                    sSql += "       lf030m f3"

                Else
                    '처방단위 TAT
                    sSql = ""
                    sSql += "SELECT f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,"
                    If rsRstflg = "2" Then
                        sSql += "       f6.prptmi tmi, fn_ack_date_diff(r.tkdt, r.mwdt, '3') tat_mi,"
                    Else
                        sSql += "       f6.frptmi tmi, fn_ack_date_diff(r.tkdt, r.fndt, '3') tat_mi,"
                    End If
                    sSql += "       count(*) totcnt"
                    sSql += "  FROM rf060m f6,"
                    sSql += "       ("
                    sSql += "        SELECT j1.bcno, j1.tclscd testcd, j1.spccd, MIN(r.tkdt) tkdt, MAX(NVL(r.mwdt, r.fndt)) mwdt, MAX(r.fndt) fndt"
                    sSql += "          FROM rr010m r, rj011m j1"
                    sSql += "         WHERE r.tkdt >= :dates"
                    sSql += "           AND r.tkdt <= :datee || '235959'"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                    If rbNotPDCA Then
                        sSql += "           AND NVL(r.panicmark, '') = '' AND NVL(r.deltamark, '') = '' AND NVL(r.crticalmark, '') = '' AND NVL(r.alertmark, '') = ''"
                    End If
                    If raTests.Count > 0 Then
                        sSql += "           AND r.testcd IN ("
                        For ix As Integer = 0 To raTests.Count - 1
                            If ix > 0 Then
                                sSql += ", "
                            End If
                            sSql += ":testcd" + ix.ToString

                            alParm.Add(New OracleParameter("testcd" + ix.ToString, raTests.Item(ix).ToString))
                        Next
                        sSql += ")"
                    End If

                    If rbVerity Then
                        sSql += "           AND r.bcno NOT IN (SELECT bcno FROM rr051m"
                        sSql += "                               WHERE bcno    = r.bcno"
                        sSql += "                                 AND testcd  = r.testcd"
                        sSql += "                           )"

                    End If

                    sSql += "           AND j1.bcno   = r.bcno"
                    sSql += "           AND j1.tclscd = r.tclscd"
                    sSql += "           AND j1.spccd  = r.spccd"
                    sSql += "           AND (NVL(r.mwdt, '-') <> '-' OR NVL(r.fndt, '-') <> '-')"
                    sSql += "         GROUP BY j1.bcno, j1.tclscd, j1.spccd"

                    sSql += "       ) r,"
                    sSql += "       rj010M j,"
                    sSql += "       lf030m f3"

                End If
                sSql += " WHERE f6.testcd  = r.testcd"
                sSql += "   AND f6.spccd   = r.spccd"
                sSql += "   AND f6.usdt   <= r.tkdt"
                sSql += "   AND f6.uedt   >  r.tkdt"
                sSql += "   AND f6.tcdgbn IN ('B', 'S', 'P')"
                sSql += "   AND r.bcno     = j.bcno"
                sSql += "   AND j.spcflg   = '4'"
                sSql += "   AND f6.spccd   = f3.spccd"
                sSql += "   AND f3.usdt   <= r.tkdt"
                sSql += "   AND f3.uedt   >  r.tkdt"

                If rsPartSlip <> "" Then
                    sSql += "   AND f6.partcd = :partcd"
                    sSql += "   AND f6.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If
                If rsIOGbn <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIOGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIOGbn))
                End If
                If rsWardNo <> "" Then
                    sSql += "   AND j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardNo))
                End If

                If rsEmerYn = "B" Then
                    sSql += "   AND j.statgbn = 'B'"
                ElseIf rsEmerYn = "Y" Then
                    sSql += "   AND j.statgbn = 'Y'"
                ElseIf rsEmerYn = "N" Then
                    sSql += "   AND NVL(j.statgbn, '-') = '-'"
                End If


                sSql += " GROUP BY f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,"
                If rsRstflg = "2" Then
                    sSql += "        f6.prptmi, r.tkdt, r.mwdt"
                Else
                    sSql += "        f6.frptmi, r.tkdt, r.fndt"
                End If


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 최종보고 통계
        Public Function fnGet_Final_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                               ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String) As DataTable

            Dim sFn As String = "fnGet_Final_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT f.dispseq, f.slipcd, f.slipnmd,"
                sSql += "       a.days, a.stcnt cnt1, a1.stcnt cnt2, TO_CHAR(CASE WHEN a.stcnt = 0 THEN 0 ELSE (a1.stcnt/a.stcnt) * 100.00 END) cnt3"
                If bIO Then
                    sSql += "  FROM (SELECT f.partcd || f.slipcd slipcd,"
                    If rsDMYGbn = "D" Then
                        sSql += " t.styymmdd days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM rt011m t, rf060m f"
                        sSql += "         WHERE t.styymmdd >= :dates AND t.styymmdd <= :datee"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += " t.styymm days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM rt011m t, rf060m f"
                        sSql += "         WHERE t.styymm >= :dates AND t.styymm <= :datee"
                    Else
                        sSql += " t.styy days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM rt011m t, rf060m f"
                        sSql += "         WHERE t.styy >= :dates AND t.styy <= :datee"
                    End If

                    sSql += "           AND t.sttype  = :sttype"
                    sSql += "           AND t.stioflg = :iogbn"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                    alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))

                    If rsIO = "I" Then
                        If rsWard.Length > 0 Then
                            sSql += "           AND t.stwardcd = :wardno"
                            alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                        End If
                    Else
                        If rsDept.Length > 0 Then
                            sSql += "           AND t.stdeptcd = :deptcd"
                            alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                        End If

                    End If

                    sSql += "           AND t.testcd = f.testcd"
                    sSql += "           AND t.spccd  = f.spccd"
                    sSql += "           AND f.usdt  <= fn_ack_sysdate"
                    sSql += "           AND f.uedt  >  fn_ack_sysdate"
                    If rsDMYGbn = "D" Then
                        sSql += "         GROUP BY f.partcd || f.slipcd, t.styymmdd"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "         GROUP BY f.partcd || f.slipcd, t.styymm"
                    Else
                        sSql += "         GROUP BY f.partcd || f.slipcd, t.styy"
                    End If
                    sSql += "        ) a"
                    If rsDMYGbn = "D" Then
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd slipcd, t.styymmdd days, SUM(t.stcnt) stcnt"
                        sSql += "                          FROM rt021m t, rf060m f"
                        sSql += "                         WHERE t.styymmdd >= :dates AND t.styymmdd <= :datee"
                        sSql += "                           AND t.sttype    = :sttype"
                        sSql += "                           AND t.stioflg   = :iogbn"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))
                        alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))

                        If rsIO = "I" Then
                            If rsWard.Length > 0 Then
                                sSql += "                           AND t.stwardcd = :wardno"
                                alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                            End If
                        Else
                            If rsDept.Length > 0 Then
                                sSql += "                           AND t.stdeptcd = :deptcd"
                                alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                            End If
                        End If
                        sSql += "                           AND t.testcd = f.testcd"
                        sSql += "                           AND t.spccd  = f.spccd"
                        sSql += "                           AND f.usdt  <= fn_ack_sysdate"
                        sSql += "                           AND f.uedt  >  fn_ack_sysdate"
                        sSql += "                         GROUP BY f.partcd || f.slipcd, t.styymmdd"
                        sSql += "                       ) a1 ON a.slipcd = a1.slipcd AND a.days = a1.days"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd slipcd, t.styymm days, SUM(t.stcnt) stcnt"
                        sSql += "                          FROM rt021m t, rf060m f"
                        sSql += "                         WHERE t.styymm >= :dates AND t.styymm <= :datee"
                        sSql += "                           AND t.sttype  = :sttype"
                        sSql += "                           AND t.stioflg = :iogbn"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))
                        alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))

                        If rsIO = "I" Then
                            If rsWard.Length > 0 Then
                                sSql += "                           AND t.stwardcd = :wardno"
                                alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                            End If
                        Else
                            If rsDept.Length > 0 Then
                                sSql += "                           AND t.stdeptcd = :deptcd"
                                alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                            End If
                        End If
                        sSql += "                           AND t.testcd = f.testcd"
                        sSql += "                           AND t.spccd  = f.spccd"
                        sSql += "                           AND f.usdt  <= fn_ack_sysdate"
                        sSql += "                           AND f.uedt  >  fn_ack_sysdate"
                        sSql += "                           GROUP BY f.partcd || f.slipcd, t.styymm"
                        sSql += "                       ) a1 ON a.slipcd = a1.slipcd AND a.days = a1.days"
                    Else
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd, t.styy days, SUM(t.stcnt) stcnt FROM rt021m t, rf060m f"
                        sSql += "                         WHERE t.styy   >= :dates AND t.styy <= :datee"
                        sSql += "                           AND t.sttype  = :sttype"
                        sSql += "                           AND t.stioflg = :iogbn"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))
                        alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))

                        If rsIO = "I" Then
                            If rsWard.Length > 0 Then
                                sSql += "                           AND t.stwardcd = :wardno"
                                alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                            End If
                        Else
                            If rsDept.Length > 0 Then
                                sSql += "                           AND t.stdeptcd = :deptcd"
                                alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                            End If
                        End If

                        sSql += "                      AND t.testcd = f.testcd"
                        sSql += "                      AND t.spccd  = f.spccd"
                        sSql += "                      AND f.usdt  <= fn_ack_sysdate"
                        sSql += "                      AND f.uedt  >  fn_ack_sysdate"
                        sSql += "                    GROUP BY f.partcd || f.slipcd, t.styy"
                        sSql += "                  ) a1 ON a.bcclscd = a1.bcclscd AND a.days = a1.days"
                    End If
                Else
                    sSql += "  FROM (SELECT f.partcd || f.slipcd slipcd,"
                    If rsDMYGbn = "D" Then
                        sSql += " t.styymmdd days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM rt010m t, rf060m f"
                        sSql += "         WHERE t.styymmdd >= :dates AND t.styymmdd <= :datee"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += " t.styymm days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM rt010m t, rf060m f"
                        sSql += "         WHERE t.styymm >= :dates AND t.styymm <= :datee"
                    Else
                        sSql += " t.styy days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM rt010m t, rf060m f"
                        sSql += "         WHERE t.styy >= :dates AND t.styy <= :datee"
                    End If

                    sSql += "           AND t.sttype = :sttype"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                    alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                    sSql += "           AND t.testcd = f.testcd"
                    sSql += "           AND t.spccd  = f.spccd"
                    sSql += "           AND f.usdt  <= fn_ack_sysdate"
                    sSql += "           AND f.uedt  >  fn_ack_sysdate"
                    If rsDMYGbn = "D" Then
                        sSql += "         GROUP BY f.partcd || f.slipcd, t.styymmdd"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "         GROUP BY f.partcd || f.slipcd, t.styymm"
                    Else
                        sSql += "         GROUP BY f.partcd || f.slipcd, t.styy"
                    End If
                    sSql += "        ) a"

                    If rsDMYGbn = "D" Then
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd slipcd, t.styymmdd days, SUM(t.stcnt) stcnt"
                        sSql += "                          FROM rt020m t, rf060m f"
                        sSql += "                         WHERE t.styymmdd >= :dates AND t.styymmdd <= :datee"
                        sSql += "                           AND t.sttype    = :sttype"
                        sSql += "                           AND t.testcd    = f.testcd"
                        sSql += "                           AND t.spccd     = f.spccd"
                        sSql += "                           AND f.usdt     <= fn_ack_sysdate"
                        sSql += "                           AND f.uedt     >  fn_ack_sysdate"
                        sSql += "                         GROUP BY f.partcd || f.slipcd, t.styymmdd"
                        sSql += "                       ) a1 ON a.slipcd = a1.slipcd AND a.days = a1.days"

                    ElseIf rsDMYGbn = "M" Then
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd slipcd, t.styymm days, SUM(t.stcnt) stcnt"
                        sSql += "                          FROM rt020m t, rf060m f"
                        sSql += "                         WHERE t.styymm >= :dates AND t.styymm <= :datee"
                        sSql += "                           AND t.sttype  = :sttype"
                        sSql += "                           AND t.testcd = f.testcd"
                        sSql += "                           AND t.spccd  = f.spccd"
                        sSql += "                           AND f.usdt  <= fn_ack_sysdate"
                        sSql += "                           AND f.uedt  >  fn_ack_sysdate"
                        sSql += "                         GROUP BY f.partcd || f.slipcd, t.styymm"
                        sSql += "                       ) a1 ON a.slipcd = a1.slipcd AND a.days = a1.days"
                    Else
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd, t.styy days, SUM(t.stcnt) stcnt"
                        sSql += "                          FROM rt020m t, rf060m f"
                        sSql += "                         WHERE t.styy  >= :dates AND t.styy <= :datee"
                        sSql += "                           AND t.sttype = :sttype"
                        sSql += "                           AND t.testcd = f.testcd"
                        sSql += "                           AND t.spccd  = f.spccd"
                        sSql += "                           AND f.usdt  <= fn_sydate()"
                        sSql += "                           AND f.uedt  >  fn_sydate()"
                        sSql += "                         GROUP BY f.partcd || f.slipcd, t.styy"
                        sSql += "                       ) a1 ON a.slipcd = a1.slipcd AND a.days = a1.days"
                    End If
                End If

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                sSql += "       INNER JOIN (SELECT partcd || slipcd slipcd, MIN(dispseq) dispseq, MAX(slipnmd) slipnmd"
                sSql += "                     FROM rf021m"
                sSql += "                    GROUP BY partcd || slipcd"
                sSql += "                  ) f ON a.slipcd= f.slipcd "

                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 최종보고 작업 리스트
        Public Function fnGet_Final_AnalysisInfo(ByVal rsDayB As String, ByVal rsDayE As String, ByVal rsType As String) As DataTable
            Dim sFn As String = "fnGet_Final_AnalysisInfo(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += " SELECT t.styymmdd, t.sttype, fn_ack_date_str(t.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "        t.regid, fn_ack_get_usr_name(t.regid) regnm"
                sSql += "   FROM rt002m t"
                sSql += "  WHERE t.styymmdd >= :dates"
                sSql += "    AND t.styymmdd <= :datee"
                sSql += "    AND t.sttype    = :sttype"

                Dim al As New ArrayList

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayB.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayB))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                al.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                DbCommand()

                Dim dt As DataTable = DbExecuteQuery(sSql, al, True)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 채혈통계
        Public Function fnGet_Coll_Statistics(ByVal rsDayGbn As String, ByVal rsDate As String, ByVal rsCollId As String) As DataTable
            Dim sFn As String = "fnGet_Coll_Statistics(String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT iogbn, SUM(h06) h06,"
                sSql += "              SUM(h07) h07, SUM(h08) h08, SUM(h09) h09, SUM(h10) h10, SUM(h11) h11, SUM(h12) h12, SUM(h13) h13,"
                sSql += "              SUM(h14) h14, SUM(h15) h15, SUM(h16) h16, SUM(h17) h17, SUM(h18) h18, SUM(h19) h19, SUM(tot) tot"
                sSql += "  FROM ("
                sSql += "        SELECT s.iogbn,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '06' THEN COUNT(*) END h06,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '07' THEN COUNT(*) END h07,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '08' THEN COUNT(*) END h08,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '09' THEN COUNT(*) END h09,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '10' THEN COUNT(*) END h10,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '11' THEN COUNT(*) END h11,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '12' THEN COUNT(*) END h12,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '13' THEN COUNT(*) END h13,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '14' THEN COUNT(*) END h14,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '15' THEN COUNT(*) END h15,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '16' THEN COUNT(*) END h16,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '17' THEN COUNT(*) END h17,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '18' THEN COUNT(*) END h18,"
                sSql += "               CASE WHEN SUBSTR(s.colldt, 9, 2) = '19' THEN COUNT(*) END h19,"
                sSql += "               COUNT(*) TOT"
                sSql += "          FROM ("
                sSql += "                SELECT a.iogbn, a.regno, SUBSTR(b.colldt, 1, 10) colldt"
                sSql += "                  FROM rj011m b, rj010m a"
                sSql += "                 WHERE b.colldt >= :dates"
                sSql += "                   AND b.colldt <  :datee"
                sSql += "                   AND a.owngbn <> 'H'"

                If rsDayGbn = "D" Then
                    Dim sDateE As String = ""
                    sDateE = Format(DateAdd(DateInterval.Day, 1, CDate(rsDate)), "yyyyMMdd")

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                ElseIf rsDayGbn = "M" Then
                    Dim sDateE As String = ""
                    sDateE = Format(DateAdd(DateInterval.Month, 1, CDate(rsDate + "-01")), "yyyyMMdd")

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-", "") + "01"))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                Else
                    Dim sDateE As String = ""
                    sDateE = Format(DateAdd(DateInterval.Year, 1, CDate(rsDate + "-01-01")), "yyyyMMdd")

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-", "") + "0101"))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                End If

                sSql += "                   AND SUBSTR(b.colldt, 9, 2) >= '06'"
                sSql += "                   AND SUBSTR(b.colldt, 9, 2) <= '19'"

                If rsCollId <> "" Then
                    sSql += "                   AND b.collid IN (" + rsCollId + ")"
                End If

                sSql += "                   AND a.bcno = b.bcno"
                sSql += "                 GROUP BY a.iogbn, a.regno, SUBSTR(b.colldt, 1, 10)"
                sSql += "               ) s"
                sSql += "         GROUP BY iogbn, colldt"
                sSql += "       ) a"
                sSql += " GROUP BY iogbn"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- TAT 관리 통계
        Public Function fnGet_TatTest_Statistics(ByVal rsTkDtS As String, ByVal rsTkDtE As String, ByVal rsIoGbn As String, ByVal rsDeptCd As String, ByVal rsWard As String, _
                                                 ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsEmerYN As String, _
                                                 ByVal rbVerity As Boolean, ByVal rbNotPDCA As Boolean, ByVal rbIoGbn_noC As Boolean) As DataTable
            Dim sFn As String = "fnGet_TatTest_Statistics(String...) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT fn_ack_date_str(r.tkdt, 'hh24:mi') tk_tm, count(r.testcd) cnt, fn_ack_date_diff(r.tkdt, r.rstdt, '3') rst_tm"
                sSql += "  FROM rr010m r, rj010m j"
                sSql += " WHERE r.tkdt >= :dates || '0000'"
                sSql += "   AND r.tkdt <= :datee || '5959'"
                sSql += "   AND SUBSTR(r.tkdt, 9, 6) >= :hhs || '0000'"
                sSql += "   AND SUBSTR(r.tkdt, 9, 6) <= :hhe || '5959'"
                sSql += "   AND r.testcd = :testcd"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                al.Add(New OracleParameter("hhs", OracleDbType.Varchar2, 2, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS.Substring(8, 2)))
                al.Add(New OracleParameter("hhe", OracleDbType.Varchar2, 2, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE.Substring(8, 2)))
                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                If rsSpcCd <> "" Then
                    sSql += "   AND r.spccd = :spccd"

                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += "   AND r.rstflg > '1'"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"

                If rsIoGbn <> "" Then
                    If rsIoGbn = "O" Then
                        sSql += "   AND j.iogbn <> 'I'"
                    Else

                        sSql += "   AND j.iogbn = :iogbn"
                        al.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))
                    End If
                End If

                If rbIoGbn_noC Then
                    sSql += "   AND j.iogbn <> 'C'"
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    al.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If

                If rsWard <> "" Then
                    sSql += "   and j.wardno = :wardno"
                    al.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                If rbNotPDCA Then
                    sSql += "   AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.criticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '"
                End If

                If rbVerity Then
                    sSql += "   AND r.bcno NOT IN (SELECT a.bcno FROM rr010m b, rr051m a"
                    sSql += "                       WHERE b.tkdt  >= :dates || '0000'"
                    sSql += "                         AND b.tkdt  <= :datee || '5959'"
                    sSql += "                         AND b.testcd = :testcd"
                    sSql += "                         AND a.bcno   = b.bcno"
                    sSql += "                         AND a.testcd = b.testcd"

                    al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                    al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                    If rsSpcCd <> "" Then
                        sSql += "                         AND b.spccd = :spccd"

                        al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                    End If

                    sSql += "                     )"

                End If

                If rsEmerYN = "N" Then
                    sSql += "   AND (NVL(j.statgbn, ' ') = ' ' OR j.statgbn IS NULL)"
                ElseIf rsEmerYN = "Y" Then
                    sSql += "   AND NVL(j.statgbn, ' ') = 'Y'"
                End If

                sSql += " GROUP BY fn_ack_date_str(r.tkdt, 'hh24:mi'), fn_ack_date_diff(r.tkdt, r.rstdt, '3')"
                sSql += " ORDER BY tk_tm"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 처방의사별 검사통계
        Public Function fnGet_Test_Statistics_dr(ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "fnGet_Test_Statistics_dr(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT fn_ack_get_dr_name(a.drcd) drnm, a.*"
                sSql += "  FROM ("
                sSql += "        SELECT orgdoctorcd drcd, count(*) total,"
                For ix As Integer = 1 To ra_sDMY.Length
                    Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                        Case 8
                            '일별 - 일자
                            sSql += "CASE WHEN SUBSTR(tkdt, 1, 8) = '" + ra_sDMY(ix - 1).Replace("-", "").Replace(" ", "") + "' THEN count(*) ELSE 0 END c" + ix.ToString
                        Case 6
                            '월별
                            sSql += "CASE WHEN SUBSTR(tkdt, 1, 6) = '" + ra_sDMY(ix - 1).Replace("-", "").Replace(" ", "") + "' THEN count(*) ELSE 0 END c" + ix.ToString

                        Case 4
                            '연별
                            sSql += "CASE WHEN SUBSTR(tkdt, 1, 4) = '" + ra_sDMY(ix - 1).Replace("-", "").Replace(" ", "") + "' THEN count(*) ELSE 0 END c" + ix.ToString

                    End Select

                    If ix = ra_sDMY.Length Then
                        sSql += ""
                    Else
                        sSql += ","
                    End If
                Next

                sSql += "          FROM rj011m j"
                sSql += "         WHERE j.tclscd = :testcd"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                If rsSpcCd <> "" Then
                    sSql += "           AND j.spccd = :spccd"
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                    Case 8
                        '일별 - 일자
                        sSql += "           AND j.tkdt >= '" + rsDT1.Replace("-", "") + "' AND j.tkdt <= '" + rsDT2.Replace("-", "") + "235959'"
                    Case 6
                        '월별
                        sSql += "           AND j.tkdt >= '" + rsDT1.Replace("-", "") + "01' AND j.tkdt <= '" + rsDT2.Replace("-", "") + "31235959'"

                    Case 4
                        '연별
                        sSql += "           AND j.tkdt >= '" + rsDT1.Replace("-", "") + "0101' AND j.tkdt <= '" + rsDT2.Replace("-", "") + "1231235959'"
                End Select

                sSql += "           AND j.owngbn <> 'H'"
                sSql += "           AND j.spcflg  = '4'"
                sSql += "         GROUP BY orgdoctorcd, tkdt"
                sSql += "       ) a"

                sSql += " ORDER BY drnm"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 검사통계(진료과별)
        Public Function fnGet_Test_Statistics_dept(ByVal rsStType As String, ByVal rsPartSlip As String, ByVal rsDate As String, _
                                                   ByVal rsIoGbn As String, ByVal rsSame As String, ByVal rsSpc As String, ByVal rsTCdGbn As String) As DataTable

            Dim sFn As String = "fnGet_Test_Statistics_dept(String, ... , String) As DataTable"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                If rsSpc = "" Then
                    sSql += "SELECT b.dispseq, CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END testcd, '' spccd,"
                    sSql += "       MIN(b.tnmd) tnm, '' spcnm, a.stdeptcd, fn_ack_get_dept_abbr(a.stioflg, a.stdeptcd) deptnm,"
                Else
                    sSql += "SELECT b.dispseq, CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END testcd, a.spccd,"
                    sSql += "       MIN(b.tnmd) tnm, c.spcnmd spcnm, a.stdeptcd, fn_ack_get_dept_abbr(a.stioflg, a.stdeptcd) deptnm,"
                End If
                sSql += "           SUM(a.stcnt) cnt"
                sSql += "  FROM rt011m a"
                sSql += "       INNER JOIN"
                sSql += "       ("
                sSql += "        SELECT testcd, MIN(tnmd) tnmd, NVL(MIN(samecd), testcd) samecd, MIN(dispseql) dispseq"
                sSql += "          FROM rf060m"
                sSql += "         WHERE usdt <= fn_ack_sysdate"

                If rsPartSlip.Length = 1 Then
                    sSql += "           AND partcd = :partcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                End If

                If rsPartSlip.Length = 2 Then
                    sSql += "           AND partcd = :partcd"
                    sSql += "           AND slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsTCdGbn.IndexOf(",") > 0 Then
                    sSql += "           AND tcdgbn IN (" + rsTCdGbn + ")"
                ElseIf rsTCdGbn <> "" Then
                    sSql += "           AND tcdgbn = :tcdgbn"
                    alParm.Add(New OracleParameter("tcdgbn", OracleDbType.Varchar2, rsTCdGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTCdGbn))
                End If

                sSql += "         GROUP BY testcd"
                sSql += "       ) b ON a.testcd = b.testcd"

                If rsSpc = "Y" Then
                    sSql += "       INNER JOIN"
                    sSql += "       ("
                    sSql += "        SELECT spccd, min(spcnmd) spcnmd"
                    sSql += "          FROM lf030m"
                    sSql += "         GROUP BY spccd"
                    sSql += "       ) c ON a.spccd = c.spccd"
                End If

                If rsDate.Length = 6 Then
                    sSql += " WHERE a.styymm = :styymm"
                Else
                    sSql += " WHERE a.styyyy = :styymm"
                End If
                alParm.Add(New OracleParameter("styymm", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                sSql += "   AND a.sttype = :sttype"
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsStType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsStType))

                If rsIoGbn = "I" Then
                    sSql += "   AND a.stioflg = 'I'"
                ElseIf rsIoGbn = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                End If

                If rsSpc = "Y" Then
                    sSql += " GROUP BY b.dispseq, CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, a.spccd, c.spcnmd, a.stdeptcd, fn_ack_get_dept_abbr(a.stioflg, a.stdeptcd)"
                Else
                    sSql += " GROUP BY b.dispseq, CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, a.stdeptcd, fn_ack_get_dept_abbr(a.stioflg, a.stdeptcd)"
                End If
                sSql += " ORDER BY b.dispseq, CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, spccd, a.stdeptcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
    End Class

    Public Class ExecFn
        Private Const msFile As String = "File : CGRISAPP_T.vb, Class : RISAPP.APP_T.ExecFn" + vbTab

        '-- 검사통계 작업
        Public Function fnExe_Tset_Statistics(ByVal rsStDate As String) As String
            Dim sFn As String = "fnExe_Tset_Statistics(String) As Date"

            Dim dbCn As oracleConnection = GetDbConnection()
            Dim sRetVal As String = ""

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New oracleCommand

                With dbCmd
                    .Connection = dbCn
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_sta_test_ris"

                    .Parameters.Clear()
                    .Parameters.Add("rs_date", OracleDbType.Varchar2).Value = rsStDate
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_usrip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 1000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sRetVal

                    .ExecuteNonQuery()

                    sRetVal = .Parameters(2).Value.ToString

                End With

                If sRetVal = "OK" Then
                    sRetVal = Format(Now, "yyyy-MM-dd HH:mm:ss").ToString
                Else
                    sRetVal = ""
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

            Return sRetVal

        End Function

        '-- 최종보고 수정 통계 작업
        Public Function fnExe_Final_Statistics(ByVal rsStDate As String) As String
            Dim sFn As String = "fnExe_Final_Statistics(String) As Date"

            Dim dbCn As oracleConnection = GetDbConnection()
            Dim sRetVal As String = ""

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sSql As String = ""

                Dim dbCmd As New oracleCommand

                With dbCmd
                    .Connection = dbCn
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_sta_final_ris"

                    .Parameters.Clear()
                    .Parameters.Add("rs_date", OracleDbType.Varchar2).Value = rsStDate
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_usrip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 1000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sRetVal

                    .ExecuteNonQuery()

                    sRetVal = .Parameters(2).Value.ToString

                End With

                If sRetVal = "OK" Then
                    sRetVal = Format(Now, "yyyy-MM-dd HH:mm:ss").ToString
                Else
                    sRetVal = ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

            Return sRetVal

        End Function

    End Class

End Namespace