Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Namespace APP_T
    Public Class SrhFn
        Private Const msFile As String = "File : CGRISAPP_T.vb, Class : RISAPP.APP_T.SrhFn" + vbTab


        '-- 미생물 결핵균 양성자 20141219
        Public Function fnGet_M_AFB_Statistics_bak(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                                 ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "fnGet_M_AFB_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False
                Dim sDays As String = ""

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "select 'LM20101' testcd , a.styymm , " + vbCrLf
                sSql += "        sum(case  when tnm = 'TOTAL' then cnt else 0 end)total " + vbCrLf
                sSql += "       ,sum(case  when tnm = 'NTM' then cnt else 0 end) NTM  " + vbCrLf
                sSql += "       ,sum(case  when tnm = 'MTB' then cnt else 0 end) MTB  " + vbCrLf
                sSql += "   from ( " + vbCrLf

                If rsType = "O" Then
                    sSql += "           select /*+ index (r,PK_LM010M) index (j,IDX_LJ010M_3)*/'TOTAL' tnm " + vbCrLf
                ElseIf rsType = "T" Then
                    sSql += "           select /*+ index (r,PK_LM010M) */'TOTAL' tnm " + vbCrLf
                Else
                    sSql += "           select /*+ index (r,PK_LM010M) */'TOTAL' tnm " + vbCrLf
                End If

                sSql += "                   , count(r.testcd) cnt, substr(j.orddt,1,6) styymm " + vbCrLf
                sSql += "             from lm010m r ,lj010m j" + vbCrLf

                If rsType = "O" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                ElseIf rsType = "T" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If

                Else
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                End If

                If rsIO = "O" Then
                    sSql += "   AND j.iogbn <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "   AND  j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += "              and j.rstflg = '2'  " + vbCrLf
                sSql += "              and r.bcno = j.bcno" + vbCrLf
                sSql += "              and r.testcd = 'LM20101'" + vbCrLf
                sSql += "              and r.partcd || r.slipcd = 'M2'" + vbCrLf
                sSql += "            group by j.orddt " + vbCrLf
                sSql += "            union all  " + vbCrLf
                sSql += "           select 'NTM' tnm, count(r.testcd) cnt, substr(j.orddt,1,6) styymm " + vbCrLf
                sSql += "             from lm010m r ,lj010m j" + vbCrLf

                If rsType = "O" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                ElseIf rsType = "T" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If

                Else
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                End If

                If rsIO = "O" Then
                    sSql += "   AND j.iogbn <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "   AND  j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += "              and j.rstflg = '2'" + vbCrLf
                sSql += "              and r.bcno = j.bcno" + vbCrLf
                sSql += "              and r.partcd || r.slipcd = 'M2'" + vbCrLf
                sSql += "              and r.testcd = 'LM20101'" + vbCrLf
                sSql += "              and r.orgrst like 'AFB%'" + vbCrLf
                sSql += "            group by j.orddt" + vbCrLf
                sSql += "            union all" + vbCrLf
                sSql += "            select 'MTB' tnm , count(r.testcd) cnt, substr(j.orddt,1,6) styymm " + vbCrLf
                sSql += "              from lm010m r , lj010m j " + vbCrLf

                If rsType = "O" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                ElseIf rsType = "T" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If

                Else
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                End If

                If rsIO = "O" Then
                    sSql += "   AND j.iogbn <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "   AND  j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += "               and j.rstflg = '2'" + vbCrLf
                sSql += "               and r.bcno = j.bcno" + vbCrLf
                sSql += "               and r.partcd || r.slipcd = 'M2'" + vbCrLf
                sSql += "               and r.testcd = 'LM20101'" + vbCrLf
                sSql += "               and r.orgrst like 'Mycobacterium%'" + vbCrLf
                sSql += "             group by j.orddt  ) a " + vbCrLf
                sSql += "   group by styymm" + vbCrLf
                sSql += "   union all " + vbCrLf
                sSql += "   select 'LM20303' testcd , a.styymm , " + vbCrLf   'LM20303
                sSql += "           sum(case  when tnm = 'TOTAL' then cnt else 0 end)total" + vbCrLf
                sSql += "          ,sum(case  when tnm = 'NTM' then cnt else 0 end) NTM" + vbCrLf
                sSql += "          ,sum(case  when tnm = 'MTB' then cnt else 0 end) MTB  " + vbCrLf
                sSql += "    from (" + vbCrLf
                sSql += "           select /*+ index (r,PK_LM010M) index (j,IDX_LJ010M_3)*/'TOTAL' tnm " + vbCrLf
                sSql += "                , count(r.testcd) cnt, substr(j.orddt,1,6) styymm " + vbCrLf
                sSql += "             from lm010m r ,lj010m j" + vbCrLf

                If rsType = "O" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                ElseIf rsType = "T" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                Else
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                End If

                If rsIO = "O" Then
                    sSql += "   AND j.iogbn <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "   AND  j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += "              and j.rstflg = '2'  " + vbCrLf
                sSql += "              and r.bcno = j.bcno" + vbCrLf
                sSql += "              and r.testcd = 'LM20303'" + vbCrLf
                sSql += "              and r.partcd || r.slipcd = 'M2'" + vbCrLf
                sSql += "            group by j.orddt " + vbCrLf
                sSql += "            union all  " + vbCrLf
                sSql += "            select 'NTM'  tnm,count(r.testcd) cnt, substr(j.orddt,1,6) styymm " + vbCrLf
                sSql += "              from lm010m r ,lj010m j" + vbCrLf

                If rsType = "O" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                ElseIf rsType = "T" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                Else
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                End If

                If rsIO = "O" Then
                    sSql += "   AND j.iogbn <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "   AND  j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += "              and j.rstflg = '2'" + vbCrLf
                sSql += "              and r.bcno = j.bcno" + vbCrLf
                sSql += "              and r.partcd || r.slipcd = 'M2'" + vbCrLf
                sSql += "              and r.testcd = 'LM20303'" + vbCrLf
                sSql += "              and r.orgrst like 'Liquid%'" + vbCrLf
                sSql += "            group by j.orddt" + vbCrLf
                sSql += "            union all" + vbCrLf
                sSql += "            select 'MTB' tnm , count(r.testcd) cnt, substr(j.orddt,1,6) styymm " + vbCrLf
                sSql += "              from lm010m r , lj010m j " + vbCrLf

                If rsType = "O" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where j.orddt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and j.orddt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                ElseIf rsType = "T" Then
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.tkdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.tkdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If

                Else
                    If rsDMYGbn = "D" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "235959'" + vbCrLf
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "01000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "31235959'" + vbCrLf
                    Else
                        sSql += "            where r.rstdt >='" + rsDT1.Replace("-", "") + "0101000000'" + vbCrLf
                        sSql += "              and r.rstdt <='" + rsDT2.Replace("-", "") + "1231235959'" + vbCrLf
                    End If
                End If

                If rsIO = "O" Then
                    sSql += "   AND j.iogbn <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "   AND  j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += "               and j.rstflg = '2'" + vbCrLf
                sSql += "               and r.bcno = j.bcno" + vbCrLf
                sSql += "               and r.partcd || r.slipcd = 'M2'" + vbCrLf
                sSql += "               and r.testcd = 'LM20303'" + vbCrLf
                sSql += "               and r.orgrst like 'Mycobacterium%'" + vbCrLf
                sSql += "             group by j.orddt  ) a " + vbCrLf
                sSql += "    group by styymm" + vbCrLf


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function
        '-- 미생물 통계
        Public Function fnGet_M_Group_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                                 ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "fnGet_M_Group_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False
                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.baccd code1, a.anticd code2, c.bacnmd name1, d.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' DAYS, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "SUM(a.stcnt) cnt1, SUM(a.stcntar) cnt2, CASE WHEN SUM(a.stcnt) = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/SUM(a.stcnt)) * 100) END CNT3"

                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT baccd, MIN(bacnmd) bacnmd"
                sSql += "                FROM lf210m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(bacnmd) = LOWER('Staphylococcus aureus')"
                sSql += "               GROUP BY baccd"
                sSql += "             ) c ON a.baccd = c.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                FROM lf230m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(antinmd) = LOWER('Vancomycin')"
                sSql += "               GROUP BY anticd"
                sSql += "             ) d ON a.anticd = d.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   AND a.sttype = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If
                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If
                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += " GROUP BY a.baccd, a.anticd, c.bacnmd, d.antinmd"
                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function
        '-- 미생물 통계 조회(VRSA)
        Public Function fnGet_M_VRSA_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                                ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "fnGet_M_VRSA_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.baccd code1, a.anticd code2, c.bacnmd name1, d.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "SUM(a.stcnt) cnt1, SUM(a.stcntar) CNT2, CASE WHEN SUM(a.STCNT) = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/SUM(a.stcnt)) * 100) END CNT3"

                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT baccd, MIN(bacnmd) bacnmd"
                sSql += "                FROM lf210m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(bacnmd) = LOWER('Staphylococcus aureus')"
                sSql += "               GROUP BY baccd"
                sSql += "             ) c ON a.baccd = c.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                FROM lf230m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(antinmd) = LOWER('Vancomycin')"
                sSql += "               GROUP BY anticd"
                sSql += "             ) d ON a.anticd = d.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   and a.STTYPE = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If
                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += " GROUP BY a.baccd, a.anticd, c.bacnmd, d.antinmd"
                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '-- 미생물 통계 조회(ESBL)
        Public Function fnGet_M_KESBL_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                                 ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "fnGet_M_KESBL_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.baccd code1, a.anticd code2, c.bacnmd name1, d.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "SUM(a.stcnt) cnt1, SUM(a.stcntar) cnt2, CASE WHEN SUM(a.stcnt) = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/SUM(a.stcnt)) * 100) END CNT3"

                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                sSql += "       INNER JOIN "
                sSql += "             ("
                sSql += "              SELECT baccd, MIN(bacnmd) banmd"
                sSql += "                FROM lf210m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(bacnmd) = LOWER('Acinetobacter baumannii (anitratus)')"
                sSql += "               GROUP BY baccd"
                sSql += "             ) c ON a.baccd = c.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                FROM lf230m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND (LOWER(antinmd) = LOWER('Imipenem') OR LOWER(antinmd) = LOWER('Meropenem'))"
                sSql += "               GROUP BY anticd"
                sSql += "             ) d ON a.anticd = d.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   and a.STTYPE = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If
                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += " GROUP BY a.baccd, a.anticd, c.bacnmd, d.antinmd"
                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '-- 미생물 통계 조회(ESBL)
        Public Function fnGet_M_EESBL_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                                 ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "fnGet_M_EESBL_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.baccd code1, a.anticd code2, c.bacnmd name1, d.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "a.stcnt cnt1, SUM(a.stcntar) cnt2, CASE WHEN a.stcnt = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/a.stcnt) * 100) END cnt3"

                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT baccd, MIN(bacnmd) bacnmd"
                sSql += "                FROM lf210m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(bacnmd) = LOWER('Acinetobacter baumannii (anitratus)')"
                sSql += "               GROUP BY baccd"
                sSql += "             ) c ON a.baccd = c.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                FROM lf230m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND (LOWER(antinmd) = LOWER('Imipenem') OR LOWER(antinmd) = LOWER('Meropenem'))"
                sSql += "               GROUP BY anticd"
                sSql += "             ) d ON a.anticd = d.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.STYYMMDD <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   AND a.sttype = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If
                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += " GROUP BY a.baccd, a.anticd, c.bacnmd, d.antinmd"
                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 미생물 통계 조회d(IRAB)
        Public Function fnGet_M_IRAB_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                                ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "fnGet_M_IRAB_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.baccd code1, a.anticd code2, c.bacnmd name1, d.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "SUM(a.stcnt) cnt1, SUM(a.stcntar) cnt2, CASE WHEN SUM(a.stcnt) = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/SUM(a.stcnt)) * 100) END cnt3"
                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT baccd, MIN(bacnmd) bacnmd"
                sSql += "                FROM lf210m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(bacnmd) = LOWER('Acinetobacter baumannii')"
                sSql += "               GROUP BY baccd"
                sSql += "             ) c ON a.baccd = c.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                FROM lf230m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND (LOWER(antinmd) = LOWER('Imipenem') or lower(ANTINMD) = lower('Meropenem'))"
                sSql += "               GROUP BY anticd"
                sSql += "             ) d ON a.anticd = d.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   AND a.sttype = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If
                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += " GROUP BY a.baccd, a.anticd, c.bacnmd, d.antinmd"
                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 미생물 통계 조회(IRPA)
        Public Function fnGet_M_IRPA_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                                ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "fnGet_M_IRPA_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.baccd code1, a.anticd code2, c.bacnmd name1, d.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "SUM(a.stcnt) cnt1, SUM(a.stcntar) cnt2, CASE WHEN SUM(a.stcnt) = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/SUM(a.stcnt)) * 100) END cnt3"
                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT baccd, MIN(bacnmd) bacnmd"
                sSql += "                FROM lf210m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(bacnmd) = LOWER('Pseudomonas aeruginosa')"
                sSql += "               GROUP BY baccd"
                sSql += "             ) c ON a.baccd = c.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                FROM lf230m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND (LOWER(antinmd) = LOWER('Imipenem') OR LOWER(ANTINMD) = LOWER('Meropenem'))"
                sSql += "               GROUP BY anticd"
                sSql += "             ) d ON a.anticd = d.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   and a.STTYPE = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If
                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += " GROUP BY a.baccd, a.anticd, c.bacnmd, d.antinmd"
                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 미생물 통계 조회(VRE)
        Public Function fnGet_M_VRE_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                              ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "fnGet_M_VRE_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.baccd code1, a.anticd code2, c.bacnmd name1, d.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "SUM(a.stcnt) cnt1, SUM(a.stcntar) CNT2, CASE WHEN SUM(a.STCNT) = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/SUM(a.stcnt)) * 100) END CNT3"

                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT baccd, MIN(bacnmd) bacnmd"
                sSql += "                FROM lf210m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND (LOWER(bacnmd) = LOWER('Enterococcus faecalis') OR LOWER(bacnmd) = LOWER('Enterococcus faecium'))"
                sSql += "               GROUP BY baccd"
                sSql += "             ) c ON a.baccd = c.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                FROM lf230m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(antinmd) = LOWER('Vancomycin')"
                sSql += "               GROUP BY anticd"
                sSql += "             ) d ON a.anticd = d.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   and a.STTYPE = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If
                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += " GROUP BY a.baccd, a.anticd, c.bacnmd, d.antinmd"
                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 미생물 통계 조회(MRSA)
        Public Function fnGet_M_MRSA_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                                ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String) As DataTable

            Dim sFn As String = "Get_M_MRSA_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.baccd code1, a.anticd code2, c.bacnmd name1, d.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "SUM(a.stcnt) cnt1, SUM(a.stcntar) CNT2, CASE WHEN SUM(a.STCNT) = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/SUM(a.stcnt)) * 100) END CNT3"

                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT baccd, MIN(bacnmd) bacnmd"
                sSql += "                FROM lf210m"
                sSql += "               WHERE usdt < fn_ack_sysdate"
                sSql += "                 AND LOWER(bacnmd) = LOWER('Staphylococcus aureus')"
                sSql += "               GROUP BY baccd"
                sSql += "             ) c ON a.baccd = c.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                FROM lf230m"
                sSql += "               WHERE USDT < fn_ack_sysdate"
                sSql += "                 AND (LOWER(antinmd) = LOWER('Cefoxitin') OR LOWER(antinmd) = LOWER('Oxacillin'))"
                sSql += "               GROUP BY anticd"
                sSql += "             ) d ON a.anticd = d.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " where a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " where a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   AND a.sttype = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsIO = "O" Then
                    sSql += "    AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "    AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += " GROUP BY a.baccd, a.anticd, c.bacnmd, d.antinmd"

                If rsDMYGbn = "D" Then
                    sSql += vbCrLf
                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 미생물 통계 조회(미생물균주 내성률)
        Public Function fnGet_M_Anti_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                             ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String, ByVal rsSpcCd As String, _
                                             ByVal rsTestCds As String, ByVal rsBacCds As String, ByVal rsAntiRst As String, ByVal rbSameCd As Boolean) As DataTable

            Dim sFn As String = "fnGet_M_Anti_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim strDays As String = ""
                If rsDMYGbn = "D" Then
                    strDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT " + IIf(rbSameCd, "c.samecd code1, e.samecd code2", "a.baccd code1, a.anticd code2").ToString + ", c.bacnmd name1, e.antinmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + strDays + "' days, "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, "
                Else
                    sSql += " a.styy days, "
                End If

                sSql += "a1.stcnt cnt1, "
                sSql += "SUM(a.stcnt)   cnt2, CASE WHEN a1.stcnt = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcnt)/a1.stcnt) * 100)   END cnt3, "
                sSql += "SUM(a.stcntar) cntr, CASE WHEN a1.stcnt = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntar)/a1.stcnt) * 100) END cntr_p, "
                sSql += "SUM(a.stcntas) cnts, CASE WHEN a1.stcnt = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntas)/a1.stcnt) * 100) END cnts_p, "
                sSql += "SUM(a.stcntai) cnti, CASE WHEN a1.stcnt = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcntai)/a1.stcnt) * 100) END cnti_p"

                sSql += "  FROM " + IIf(bIO, "lt051m", "lt050m").ToString + " a"
                If bIO Then
                    If rsDMYGbn = "D" Then
                        sSql += "       INNER JOIN (SELECT baccd, SUM(stcnt) stcnt FROM lt041m"
                        sSql += "                    WHERE styymmdd >= :dates"
                        sSql += "                      AND styymmdd <= :datee"
                        sSql += "                      AND sttype    = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If

                        If rsIO = "O" Then
                            sSql += "                      AND stioflg <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                      AND stioflg = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsDept.Length > 0 Then
                            sSql += "                      AND stdeptcd = :deptcd"
                            alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                        End If
                        If rsWard.Length > 0 Then
                            sSql += "                      AND stwardno = :wardno"
                            alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                        End If

                        sSql += "                    GROUP BY baccd) a1 ON a.baccd = a1.baccd"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "       INNER JOIN (SELECT baccd, styymm, SUM(stcnt) stcnt FROM lt041m"
                        sSql += "                    WHERE styymm >= :dates AND styymm <= :datee"
                        sSql += "                      AND sttype  = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If

                        If rsIO = "O" Then
                            sSql += "                      AND stioflg <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                      AND stioflg = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsDept.Length > 0 Then
                            sSql += "                      AND stdeptcd = :deptcd"
                            alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                        End If
                        If rsWard.Length > 0 Then
                            sSql += "                      AND stwardno = :wardno"
                            alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                        End If

                        sSql += "                    GROUP BY baccd, styymm) a1 ON a.baccd = a1.baccd AND a.styymm = a1.styymm"
                    Else
                        sSql += "       INNER JOIN (SELECT baccd, styy, SUM(stcnt) stcnt FROM lt041m"
                        sSql += "                    WHERE styy  >= :dates AND styy <= :datee"
                        sSql += "                      AND sttype = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If

                        If rsIO = "O" Then
                            sSql += "                      AND stioflg <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                      AND stioflg = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsDept.Length > 0 Then
                            sSql += "                      AND stdeptcd = :deptcd"
                            alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                        End If
                        If rsWard.Length > 0 Then
                            sSql += "                      AND stwardno = :wardno"
                            alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                        End If

                        sSql += "                    GROUP BY baccd, styy) a1 ON a.baccd = a1.baccd AND a.styy = a1.styy"

                    End If
                Else
                    If rsDMYGbn = "D" Then
                        sSql += "       INNER JOIN (SELECT baccd, SUM(stcnt) stcnt FROM lt040m"
                        sSql += "                    WHERE styymmdd >= :dates AND styymmdd <= :datee"
                        sSql += "                      AND sttype    = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If

                        sSql += "                    GROUP BY baccd) a1 ON a.baccd = a1.baccd"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += "       INNER JOIN (SELECT baccd, styymm, SUM(stcnt) stcnt FROM lt040m"
                        sSql += "                    WHERE styymm >= :dates AND styymm <= :datee"
                        sSql += "                      AND sttype  = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If

                        sSql += "                    GROUP BY baccd, styymm) a1 ON a.baccd = a1.baccd AND a.styymm = a1.styymm"
                    Else
                        sSql += "       INNER JOIN (SELECT baccd, styy, SUM(stcnt) stcnt FROM lt040m"
                        sSql += "                    WHERE styymm >= :dates AND styymm <= :datee"
                        sSql += "                      AND sttype  = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If
                        sSql += "                    GROUP BY baccd, styy) a1 ON a.baccd = a1.baccd AND a.styy = a1.styy"
                    End If
                End If
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT DISTINCT"
                sSql += "                     c1.baccd, NVL(c1.samecd, c1.baccd) samecd, c2.bacnmd"
                sSql += "                FROM lf210m c1,"
                sSql += "                     (SELECT baccd, min(bacnmd) bacnmd"
                sSql += "                        FROM lf210m"
                sSql += "                       WHERE usdt < fn_ack_sysdate"
                sSql += "                       GROUP BY baccd"
                sSql += "                     ) c2"
                sSql += "               WHERE c1.baccd  = c2.baccd"
                sSql += "                 AND c1.usdt  < fn_ack_sysdate"
                If rsBacGen <> "" Then
                    sSql += "                 AND c1.bacgencd = :bgencd"
                    alParm.Add(New OracleParameter("bgencd", OracleDbType.Varchar2, rsBacGen.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBacGen))
                End If

                If rsBacCds.Length > 0 Then
                    sSql += "                 AND c1.baccd IN (" + rsBacCds + ")"
                End If
                sSql += "             ) c on c.baccd = a.baccd"
                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT DISTINCT"
                sSql += "                     e1.anticd, NVL(e1.samecd, e1.anticd) samecd, e2.antinmd"
                sSql += "                FROM lf230m e1,"
                sSql += "                     (SELECT anticd, MIN(antinmd) antinmd"
                sSql += "                        FROM lf230m"
                sSql += "                       WHERE usdt < fn_ack_sysdate"
                sSql += "                       GROUP BY anticd"
                sSql += "                     ) e2"
                sSql += "               WHERE e1.anticd  = e2.anticd"
                sSql += "                 AND e1.usdt   < fn_ack_sysdate"
                sSql += "             ) e ON e.anticd = a.anticd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   AND a.sttype = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsSpcCd <> "" Then
                    sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                    'sSql += "   AND a.spccd = :spccd"
                    'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsIO = "O" Then
                    sSql += "    AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "    AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "    AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "    AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                sSql += "   AND a.stcnt > 0"


                sSql += " GROUP BY " + IIf(rbSameCd, "c.samecd, e.samecd", "a.baccd, a.anticd").ToString + ", c.bacnmd, e.antinmd, a1.stcnt"
                If rsDMYGbn = "D" Then
                    sSql += vbCrLf
                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 미생물 통계 조회(미생물균주 양성자률)
        Public Function fnGet_M_Bac_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                               ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsBacGen As String, ByVal rsSpcCd As String, _
                                               ByVal rsTestCds As String, ByVal rsBacCds As String, ByVal rbSameCd As Boolean) As DataTable

            Dim sFn As String = "fnGet_M_Bac_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.spccd code1, " + IIf(rbSameCd, "d.samecd", "d.baccd").ToString + " code2, c.spcnmd name1, d.bacnmd name2,"
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, a1.stcnt cnt1, SUM(a.stcnt) cnt2, CASE WHEN a1.stcnt = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcnt)/a1.stcnt) * 100) END cnt3"
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, a1.stcnt cnt1, SUM(a.stcnt) cnt2, CASE WHEN a1.stcnt = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcnt)/a1.stcnt) * 100) END cnt3"
                Else
                    sSql += " a.styy days, a1.stcnt cnt1, SUM(a.stcnt) cnt2, CASE WHEN a1.stcnt = 0 THEN '0' ELSE TO_CHAR((SUM(a.stcnt)/a1.stcnt) * 100) END cnt3"
                End If
                sSql += "  FROM " + IIf(bIO, "lt041m", "lt040m").ToString + " a"
                If bIO Then
                    If rsDMYGbn = "D" Then
                        '일별 - 일자
                        sSql += "       INNER JOIN (SELECT a.spccd, SUM(a.stcnt) stcnt FROM lt011m a, lf060m b"
                        sSql += "                    WHERE a.styymmdd >= :dates"
                        sSql += "                      AND a.styymmdd <= :datee"
                        sSql += "                      AND a.sttype    = :sttype"
                        sSql += "                      AND (a.testcd || a.spccd) IN (SELECT DISTINCT testcd || spccd FROM lf060m WHERE partcd = '" + PRG_CONST.PART_MicroBio + "')"
                        sSql += "                      AND a.testcd    = b.testcd"
                        sSql += "                      AND a.spccd     = b.spccd"
                        sSql += "                      AND b.mbttype   = '2'"
                        sSql += "                      AND b.tcdgbn   IN ('P','S')"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            ' sSql += "                      AND a.spccd = :spccd"
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            ' alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If

                        If rsIO = "O" Then
                            sSql += "                      AND a.stioflg <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                      AND a.stioflg = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsDept.Length > 0 Then
                            sSql += "                      AND a.stdeptcd = :deptcd"
                            alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                        End If

                        If rsWard.Length > 0 Then
                            sSql += "                      AND a.stwardno = :wardno"
                            alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                        End If

                        sSql += "                    GROUP BY a.spccd) a1 ON a.spccd = a1.spccd"
                    ElseIf rsDMYGbn = "M" Then
                        '월별
                        sSql += "       INNER JOIN (SELECT a.spccd, a.styymm, SUM(a.stcnt) stcnt FROM lt011m a, lf060m b"
                        sSql += "                    WHERE a.styymm >= :dates"
                        sSql += "                      AND a.styymm <= :datee"
                        sSql += "                      AND a.sttype  = :sttype"
                        sSql += "                      AND (a.testcd || a.spccd) IN (SELECT DISTINCT testcd || spccd FROM lf060m WHERE partcd = '" + PRG_CONST.PART_MicroBio + "')"
                        sSql += "                      AND a.testcd  = b.testcd"
                        sSql += "                      AND a.spccd   = b.spccd"
                        sSql += "                      AND b.mbttype = '2'"
                        sSql += "                      AND b.tcdgbn IN ('P','S')"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND a.spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If

                        If rsIO = "O" Then
                            sSql += "                      AND a.stioflg <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                      AND a.stioflg = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsDept.Length > 0 Then
                            sSql += "                      AND a.stdeptcd = :deptcd"
                            alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                        End If

                        If rsWard.Length > 0 Then
                            sSql += "                      AND a.stwardno = :wardno"
                            alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                        End If

                        sSql += "                    GROUP BY a.spccd, a.styymm) a1 ON a.spccd = a1.spccd AND a.styymm = a1.styymm"

                    Else
                        '년별
                        sSql += "       INNER JOIN (SELECT a.spccd, a.styy, SUM(a.stcnt) stcnt FROM lt011m a, lf060m b"
                        sSql += "                    WHERE a.styy   >= :dates"
                        sSql += "                      AND a.styy   <= :datee"
                        sSql += "                      AND a.sttype  = :sttype"
                        sSql += "                      AND (a.testcd || a.spccd) IN (SELECT DISTINCT testcd || spccd FROM lf060m WHERE partcd = '" + PRG_CONST.PART_MicroBio + "')"
                        sSql += "                      AND a.testcd  = b.testcd"
                        sSql += "                      AND a.spccd   = b.spccd"
                        sSql += "                      AND b.mbttype = '2'"
                        sSql += "                      AND b.tcdgbn IN ('P','S')"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND a.spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                        End If

                        If rsIO = "O" Then
                            sSql += "                      AND a.stioflg <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                      AND a.stioflg = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsDept.Length > 0 Then
                            sSql += "                      AND a.stdeptcd = :deptcd"
                            alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                        End If

                        If rsWard.Length > 0 Then
                            sSql += "                      AND a.stwardno = :wardno"
                            alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                        End If

                        sSql += "                    GROUP BY a.spccd, a.styy) a1 ON a.spccd = a1.spccd AND a.styymm = a1.styy"

                    End If
                Else
                    If rsDMYGbn = "D" Then
                        sSql += "       INNER JOIN (SELECT a.spccd,  SUM(a.stcnt) stcnt FROM lt010m a, lf060m b"
                        sSql += "                    WHERE a.styymmdd >= :dates"
                        sSql += "                      AND a.styymmdd <= :datee"
                        sSql += "                      AND a.sttype    = :sttype"
                        sSql += "                      AND (a.testcd || a.spccd) IN (SELECT DISTINCT testcd || spccd FROM lf060m WHERE partcd = '" + PRG_CONST.PART_MicroBio + "')"
                        sSql += "                      AND a.testcd    = b.testcd"
                        sSql += "                      AND a.spccd     = b.spccd"
                        sSql += "                      AND b.mbttype   = '2'"
                        sSql += "                      AND b.tcdgbn   IN ('P','S')"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND a.spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If
                        sSql += "                    GROUP BY a.spccd) a1 ON a.spccd = a1.spccd "

                    ElseIf rsDMYGbn = "M" Then
                        sSql += "       INNER JOIN (SELECT a.spccd, a.styymm, SUM(a.stcnt) stcnt FROM lt010m a, lf060m b"
                        sSql += "                    WHERE a.styymm >= :dates"
                        sSql += "                      AND a.styymm <= :datee"
                        sSql += "                      AND a.sttype  = :sttype"
                        sSql += "                      AND (a.testcd || a.spccd) IN (SELECT DISTINCT testcd || spccd FROM lf060m WHERE partcd = '" + PRG_CONST.PART_MicroBio + "')"
                        sSql += "                      AND a.testcd  = b.testcd"
                        sSql += "                      AND a.spccd   = b.spccd"
                        sSql += "                      AND b.mbttype = '2'"
                        sSql += "                      AND b.tcdgbn IN ('P','S')"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND a.spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If
                        sSql += "                    GROUP BY a.spccd, a.styymm) a1 ON a.spccd = a1.spccd and a.styymm = a1.styymm" '20131121 정선영 수정
                    Else
                        sSql += "       INNER JOIN (SELECT a.spccd, a.styy, SUM(a.stcnt) stcnt FROM lt010m a, lf060m b"
                        sSql += "                    WHERE a.styy   >= :dates"
                        sSql += "                      AND a.styy   <= :datee"
                        sSql += "                      AND a.sttype  = :sttype"
                        sSql += "                      AND (a.testcd || a.spccd) IN (SELECT DISTINCT testcd || spccd FROM lf060m WHERE partcd = '" + PRG_CONST.PART_MicroBio + "')"
                        sSql += "                      AND a.testcd  = b.testcd"
                        sSql += "                      AND a.spccd   = b.spccd"
                        sSql += "                      AND b.mbttype = '2'"
                        sSql += "                      AND b.tcdgbn IN ('P','S')"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsSpcCd <> "" Then
                            sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                            'sSql += "                      AND a.spccd = :spccd"
                            'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                        End If
                        sSql += "                    GROUP BY a.spccd, a.styy) a1 ON a.spccd = a1.spccd  and a.styy = a1.styy"
                    End If
                End If

                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT spccd, MIN(spcnmd) spcnmd"
                sSql += "                FROM lf030m"
                sSql += "               GROUP BY spccd) c ON a.spccd = c.spccd"

                sSql += "       INNER JOIN"
                sSql += "             ("
                sSql += "              SELECT DISTINCT"
                sSql += "                     d1.baccd, NVL(d1.samecd, d1.baccd) samecd, d2.bacnmd"
                sSql += "                FROM lf210m d1,"
                sSql += "                     (SELECT baccd, MIN(bacnmd) bacnmd"
                sSql += "                       FROM lf210m"
                sSql += "                      WHERE usdt < fn_ack_sysdate"
                sSql += "                      GROUP BY baccd"
                sSql += "                     ) d2"
                sSql += "               WHERE d1.baccd  = d2.baccd"
                sSql += "                 AND d1.usdt  <  fn_ack_sysdate"

                If rsBacGen <> "" Then
                    sSql += "                 AND d1.bacgencd = :bgencd"
                    alParm.Add(New OracleParameter("bgencd", OracleDbType.Varchar2, rsBacGen.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBacGen))
                End If

                If rsBacCds.Length > 0 Then
                    sSql += "                 AND d1.baccd IN (" + rsBacCds + ")"
                End If

                sSql += "             ) d ON d.baccd = a.baccd"

                If rsDMYGbn = "D" Then
                    '일별 - 일자
                    sSql += " WHERE a.styymmdd >= :dates AND a.styymmdd <= :datee"
                ElseIf rsDMYGbn = "M" Then
                    '월별
                    sSql += " WHERE a.styymm >= :dates AND a.styymm <= :datee"
                Else
                    '년별
                    sSql += " WHERE a.styy >= :dates AND a.styy <= :datee"
                End If

                sSql += "   AND a.sttype = :sttype"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                If rsSpcCd <> "" Then
                    sSql += "                      AND a.spccd IN (" + rsSpcCd + ")"
                    'sSql += "   AND a.spccd = :spccd"
                    'alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                End If

                If rsDept.Length > 0 Then
                    sSql += "   AND a.stdeptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                If rsWard.Length > 0 Then
                    sSql += "   AND a.stwardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                End If

                If rsTestCds.Length > 0 Then
                    sSql += "   AND a.testcd IN (" + rsTestCds + ")"
                End If

                sSql += " GROUP BY a.spccd, " + IIf(rbSameCd, "d.samecd", "d.baccd").ToString + ", c.spcnmd, d.bacnmd, a1.stcnt"
                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If
                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 미생물 통계 조회(미생물 결핵 양성자률) 20150123
        Public Function fnGet_M_AFB_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                               ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String) As DataTable


            Dim sFn As String = "fnGet_M_AFB_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList


                sSql += "SELECT   a.spccd code1, a.testcd code2, ''  name1, f.tnm name2,    " 'code1:MTb,NTM , code2 : 검사코드 , name2 검사명 ,CNT1 :검사건수 , CNT2 : MTb,NTM 건수
                If rsDMYGbn = "D" Then
                    sSql += "      '" + sDays + "' days, sum (a.stcnt) cnt1, SUM (a.STCNTAB1) cnt2 ,'' cnt3 "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " a.styymm days, sum (a.stcnt) cnt1, SUM (a.STCNTAB1) cnt2 ,'' cnt3"
                Else
                    sSql += " a.styy days, sum (a.stcnt) cnt1, SUM (a.STCNTAB1) cnt2 ,'' cnt3"
                End If

                sSql += " FROM " + IIf(bIO, "lt011m", "lt010m").ToString + " a  "
                sSql += "                   inner join ( select distinct tnm , testcd "
                sSql += "                                  from lf060m       "
                sSql += "                                 where testcd in ('LM20101','LM20102','LM20302','LM20303') "
                sSql += "  and usdt <= :dates"
                sSql += "  and uedt >= :datee) f on f.testcd = a.testcd "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))



                If rsDMYGbn = "D" Then
                    sSql += " where    a.styymmdd >= :dates AND a.styymmdd <= :datee AND a.sttype = :sttype "
                ElseIf rsDMYGbn = "M" Then
                    sSql += " where    a.styymm >= :dates AND a.styymm <= :datee AND a.sttype = :sttype "
                Else
                    sSql += " where    a.styy >= :dates AND a.styy <= :datee AND a.sttype = :sttype "
                End If

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = '" + rsIO + "'"
                End If

                If rsDept <> "" Then
                    sSql += "   AND a.stdeptcd = '" + rsDept + "'"
                End If

                If rsWard <> "" Then
                    sSql += "   AND a.stwardno = '" + rsWard + "'"
                End If

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                sSql += " and spccd in ('NTM','MTB')"
                sSql += " group by a.spccd,a.testcd , f.tnm"

                If rsDMYGbn = "D" Then

                ElseIf rsDMYGbn = "M" Then
                    sSql += ", a.styymm"
                Else
                    sSql += ", a.styy"
                End If

                sSql += " order by code2 , code1"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function fnGet_M_AFB_Statistics2(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                              ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String) As DataTable


            Dim sFn As String = "fnGet_M_AFB_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList


                sSql += "                SELECT 'MTB' name1,"
                sSql += "                r.testcd ,"
                sSql += "                f.tnmd,                "
                '  sSql += "                 --r.spccd ,"
                sSql += "                 SUBSTR (r.tkdt, 1, 6) styymm, "
                sSql += "                 COUNT (* ) stcnt,"
                sSql += "                 SUM (CASE WHEN NVL (r.rstflg, '0') = '0' THEN 1 ELSE 0 END)"
                sSql += "                    stcntnt,               "
                sSql += "                 SUM ("
                sSql += "                    CASE WHEN SUBSTR (r.orgrst, 1, 3) = 'Myc' THEN 1 ELSE 0 END"
                sSql += "                 )"
                sSql += "                        stcntab1"
                sSql += "          FROM   lm010m r  ,  lj010m j , lf060m f "
                sSql += "         WHERE       r.tkdt >= '20190701'"
                sSql += "                 AND r.tkdt <= '20190731' || '235959'"
                ' sSql += "                 --AND r.rstflg = '3'"
                sSql += "                 AND j.rstflg = '2'"
                sSql += "                 AND r.testcd in ('LM20101', 'LM20102', 'LM20302', 'LM20303')"
                sSql += "                 AND r.partcd || r.slipcd = 'M2'"
                sSql += "                 AND r.bcno = j.bcno"
                sSql += "                 AND j.owngbn <> 'H'"
                sSql += "                 AND r.testcd = f.testcd"
                sSql += "                 AND r.spccd = f.spccd"
                sSql += "                 AND f.usdt <= r.tkdt"
                sSql += "                 AND f.uedt > r.tkdt               "
                sSql += "      GROUP BY   r.testcd,"
                sSql += "                 SUBSTR (r.tkdt, 1, 4),"
                sSql += "                 SUBSTR (r.tkdt, 1, 6),"
                sSql += "                 f.tnmd"
                sSql += "                        union all"
                sSql += "     SELECT 'NTM' name1,"
                sSql += "                r.testcd ,  "
                sSql += "                f.tnmd,              "
                'sSql += "                 --r.spccd ,"
                sSql += "                 SUBSTR (r.tkdt, 1, 6) styymm, "
                sSql += "                 COUNT (* ) stcnt,"
                sSql += "                 SUM (CASE WHEN NVL (r.rstflg, '0') = '0' THEN 1 ELSE 0 END)"
                sSql += "                    stcntnt,               "
                sSql += "                 SUM ("
                sSql += "                    case when r.testcd in ('LM20101','LM20102') "
                sSql += "                         then case when substr(r.orgrst, 1, 3) = 'AFB'"
                sSql += "                                   then 1 "
                sSql += "                                   else 0"
                sSql += "                                End"
                sSql += "                         when r.testcd in ('LM20302','LM20303') "
                sSql += "                         then case when substr(r.orgrst, 1, 3) = 'Liq'"
                sSql += "                                   then 1 "
                sSql += "                                   else 0"
                sSql += "                                End"
                sSql += "                                End"
                sSql += "                 )"
                sSql += "                                stcntab1 "
                sSql += "          FROM   lm010m r ,  lj010m j ,lf060m f "
                sSql += "         WHERE       r.tkdt >= '20190701'"
                sSql += "                 AND r.tkdt <= '20190731' || '235959'"
                sSql += "                 AND j.rstflg = '2'"
                sSql += "                 AND r.testcd in ('LM20101', 'LM20102', 'LM20302', 'LM20303')"
                sSql += "                 AND r.partcd || r.slipcd = 'M2'"
                sSql += "                 AND r.bcno = j.bcno"
                sSql += "                 AND j.owngbn <> 'H'"
                sSql += "                 AND r.testcd = f.testcd"
                sSql += "                 AND r.spccd = f.spccd"
                sSql += "                 AND f.usdt <= r.tkdt"
                sSql += "                 AND f.uedt > r.tkdt               "
                sSql += "      GROUP BY   r.testcd,"
                sSql += "                 SUBSTR (r.tkdt, 1, 4),"
                sSql += "                 SUBSTR (r.tkdt, 1, 6),"
                sSql += "                 f.tnmd"
                sSql += "                 order by name1 , testcd"
                ' sSql += "               --  r.spccd"



                'sSql += "SELECT   a.spccd code1, a.testcd code2, ''  name1, f.tnm name2,    " 'code1:MTb,NTM , code2 : 검사코드 , name2 검사명 ,CNT1 :검사건수 , CNT2 : MTb,NTM 건수
                'If rsDMYGbn = "D" Then
                '    sSql += "      '" + sDays + "' days, sum (a.stcnt) cnt1, SUM (a.STCNTAB1) cnt2 ,'' cnt3 "
                'ElseIf rsDMYGbn = "M" Then
                '    sSql += " a.styymm days, sum (a.stcnt) cnt1, SUM (a.STCNTAB1) cnt2 ,'' cnt3"
                'Else
                '    sSql += " a.styy days, sum (a.stcnt) cnt1, SUM (a.STCNTAB1) cnt2 ,'' cnt3"
                'End If

                'sSql += " FROM " + IIf(bIO, "lt011m", "lt010m").ToString + " a  "
                'sSql += "                   inner join ( select distinct tnm , testcd "
                'sSql += "                                  from lf060m       "
                'sSql += "                                 where testcd in ('LM20101','LM20102','LM20302','LM20303') "
                'sSql += "  and usdt <= :dates"
                'sSql += "  and uedt >= :datee) f on f.testcd = a.testcd "

                'alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                'alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))



                'If rsDMYGbn = "D" Then
                '    sSql += " where    a.styymmdd >= :dates AND a.styymmdd <= :datee AND a.sttype = :sttype "
                'ElseIf rsDMYGbn = "M" Then
                '    sSql += " where    a.styymm >= :dates AND a.styymm <= :datee AND a.sttype = :sttype "
                'Else
                '    sSql += " where    a.styy >= :dates AND a.styy <= :datee AND a.sttype = :sttype "
                'End If

                'If rsIO = "O" Then
                '    sSql += "   AND a.stioflg <> 'I'"
                'ElseIf rsIO <> "" Then
                '    sSql += "   AND a.stioflg = '" + rsIO + "'"
                'End If

                'If rsDept <> "" Then
                '    sSql += "   AND a.stdeptcd = '" + rsDept + "'"
                'End If

                'If rsWard <> "" Then
                '    sSql += "   AND a.stwardno = '" + rsWard + "'"
                'End If

                'alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                'alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))
                'alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                'sSql += " and spccd in ('NTM','MTB')"
                'sSql += " group by a.spccd,a.testcd , f.tnm"

                'If rsDMYGbn = "D" Then

                'ElseIf rsDMYGbn = "M" Then
                '    sSql += ", a.styymm"
                'Else
                '    sSql += ", a.styy"
                'End If

                'sSql += " order by code2 , code1"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function fnGet_M_AFB_Statistics3(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, _
                                              ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsSpccd As String, ByVal rsTestcd As String) As DataTable


            Dim sFn As String = "fnGet_M_AFB_Statistics(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sDays As String = ""
                If rsDMYGbn = "D" Then
                    sDays = rsDT1.Substring(0, 10) + " ~ " + rsDT2.Substring(0, 10)
                End If

                '외래, 입원, 진료과, 병동 통계인지 구분
                If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim dateS As String = ""
                Dim dateE As String = ""

                Select Case rsDMYGbn
                    Case "D"
                        dateS = "000000"
                        dateE = "235959"
                    Case "M"
                        dateS = "01000000"
                        dateE = "31235959"
                    Case "Y"
                        dateS = "0101000000"
                        dateE = "1231235959"
                End Select

                sSql += "         SELECT  r.testcd ," + vbCrLf
                sSql += "                 f.tnmd,   " + vbCrLf
                sSql += "                 'MTB' name1," + vbCrLf
                sSql += "                 COUNT (*) stcnt," + vbCrLf
                sSql += "                 SUM (" + vbCrLf
                sSql += "                    CASE WHEN SUBSTR (r.orgrst, 1, 3) = 'Myc' THEN 1 ELSE 0 END" + vbCrLf
                sSql += "                 )" + vbCrLf
                sSql += "                        stcntab1" + vbCrLf

                If rsType = "O" Then
                    sSql += "          FROM lj010m j, lm010m r, lf060m f " + vbCrLf
                    sSql += "         WHERE j.orddt >= :dates || '" + dateS + "'" + vbCrLf
                    sSql += "           AND j.orddt <= :datee || '" + dateE + "'" + vbCrLf
                ElseIf rsType = "T" Then
                    sSql += "          FROM lm010m r  ,  lj010m j , lf060m f " + vbCrLf
                    sSql += "         WHERE r.tkdt  >= :dates || '" + dateS + "'" + vbCrLf
                    sSql += "           AND r.tkdt  <= :datee || '" + dateE + "'" + vbCrLf
                ElseIf rsType = "F" Then
                    sSql += "          FROM lm010m r  ,  lj010m j , lf060m f " + vbCrLf
                    sSql += "         WHERE r.rstdt >= :dates || '" + dateS + "'" + vbCrLf
                    sSql += "           AND r.rstdt <= :datee || '" + dateE + "'" + vbCrLf
                End If

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))

                sSql += "                 AND j.rstflg = '2'" + vbCrLf

                If rsTestcd <> "" Then
                    sSql += "                 AND r.testcd in (" + rsTestcd + ")" + vbCrLf
                Else
                    sSql += "                 AND r.testcd in ('LM20101', 'LM20102', 'LM20302', 'LM20303')" + vbCrLf
                End If


                If rsSpccd <> "" Then
                    sSql += "              AND r.spccd IN (" + rsSpccd + ") " + vbCrLf
                End If

                sSql += "                 AND j.bcclscd = 'M2'" + vbCrLf
                sSql += "                 AND r.bcno = j.bcno" + vbCrLf
                sSql += "                 AND j.owngbn <> 'H'" + vbCrLf

                If rsIO = "O" Then
                    sSql += "                      AND j.iogbn <> 'I'" + vbCrLf
                ElseIf rsIO <> "" Then
                    sSql += "                      AND j.iogbn = :iogbn" + vbCrLf
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))

                    If rsWard <> "" Then
                        sSql += "                  AND j.wardno = :wardno " + vbCrLf
                        alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                    End If

                End If

                If rsDept <> "" Then
                    sSql += "               AND j.deptcd = :deptcd " + vbCrLf
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                sSql += "                 AND r.testcd = f.testcd" + vbCrLf
                sSql += "                 AND r.spccd = f.spccd" + vbCrLf
                sSql += "                 AND f.usdt <= r.tkdt" + vbCrLf
                sSql += "                 AND f.uedt > r.tkdt               " + vbCrLf
                sSql += "      GROUP BY   r.testcd," + vbCrLf
                sSql += "                 f.tnmd" + vbCrLf
                sSql += "     UNION ALL" + vbCrLf
                sSql += "     SELECT      r.testcd , " + vbCrLf
                sSql += "                 f.tnmd,     " + vbCrLf
                sSql += "                 'NTM' name1," + vbCrLf
                sSql += "                 COUNT (*) stcnt," + vbCrLf
                sSql += "                 SUM (" + vbCrLf
                sSql += "                    case when r.testcd in ('LM20101','LM20102') " + vbCrLf
                sSql += "                         then case when substr(r.orgrst, 1, 3) = 'AFB'" + vbCrLf
                sSql += "                                   then 1 " + vbCrLf
                sSql += "                                   else 0" + vbCrLf
                sSql += "                                End" + vbCrLf
                sSql += "                         when r.testcd in ('LM20302','LM20303') " + vbCrLf
                sSql += "                         then case when substr(r.orgrst, 1, 3) = 'Liq'" + vbCrLf
                sSql += "                                   then 1 " + vbCrLf
                sSql += "                                   else 0" + vbCrLf
                sSql += "                                End" + vbCrLf
                sSql += "                     End" + vbCrLf
                sSql += "                 )" + vbCrLf
                sSql += "                                stcntab1 " + vbCrLf

                If rsType = "O" Then
                    sSql += "          FROM lj010m j, lm010m r, lf060m f " + vbCrLf
                    sSql += "         WHERE j.orddt >= :dates || '" + dateS + "'" + vbCrLf
                    sSql += "           AND j.orddt <= :datee || '" + dateE + "'" + vbCrLf
                ElseIf rsType = "T" Then
                    sSql += "          FROM lm010m r  ,  lj010m j , lf060m f " + vbCrLf
                    sSql += "         WHERE r.tkdt  >= :dates || '" + dateS + "'" + vbCrLf
                    sSql += "           AND r.tkdt  <= :datee || '" + dateE + "'" + vbCrLf
                ElseIf rsType = "F" Then
                    sSql += "          FROM lm010m r  ,  lj010m j , lf060m f " + vbCrLf
                    sSql += "         WHERE r.rstdt >= :dates || '" + dateS + "'" + vbCrLf
                    sSql += "           AND r.rstdt <= :datee || '" + dateE + "'" + vbCrLf
                End If

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2.Replace("-", "")))

                'sSql += "                 AND r.rstflg = '3'"
                sSql += "                 AND j.rstflg = '2'" + vbCrLf

                If rsTestcd <> "" Then
                    sSql += "                 AND r.testcd in (" + rsTestcd + ")" + vbCrLf
                Else
                    sSql += "                 AND r.testcd in ('LM20101', 'LM20102', 'LM20302', 'LM20303')" + vbCrLf
                End If

                If rsSpccd <> "" Then
                    sSql += "              AND r.spccd IN (" + rsSpccd + ") " + vbCrLf
                End If

                sSql += "                 AND j.bcclscd = 'M2'" + vbCrLf
                sSql += "                 AND r.bcno = j.bcno" + vbCrLf
                sSql += "                 AND j.owngbn <> 'H'" + vbCrLf

                If rsIO = "O" Then
                    sSql += "                      AND j.iogbn <> 'I'" + vbCrLf
                ElseIf rsIO <> "" Then
                    sSql += "                      AND j.iogbn = :iogbn" + vbCrLf
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))

                    If rsWard <> "" Then
                        sSql += "                  AND j.wardno = :wardno " + vbCrLf
                        alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                    End If
                End If

                If rsDept <> "" Then
                    sSql += "               AND j.deptcd = :deptcd " + vbCrLf
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                End If

                sSql += "                 AND r.testcd = f.testcd" + vbCrLf
                sSql += "                 AND r.spccd = f.spccd" + vbCrLf
                sSql += "                 AND f.usdt <= r.tkdt" + vbCrLf
                sSql += "                 AND f.uedt > r.tkdt               " + vbCrLf
                sSql += "      GROUP BY   r.testcd," + vbCrLf
                sSql += "                 f.tnmd" + vbCrLf
                sSql += "      ORDER BY testcd, name1"


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 미생물 통계 조회(미생물 (균/항생제) 통계 현황)
        Public Function fnGet_M_AnalysisInfo(ByVal rsDayB As String, ByVal rsDayE As String, ByVal rsType As String) As DataTable
            Dim sFn As String = "fnGet_M_AnalysisInfo(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += " select t.styymmdd, t.sttype, fn_ack_date_str(t.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "        t.regid, fn_ack_get_usr_name(t.regid) regnm"
                sSql += "   from lt003m t"
                sSql += "  where t.styymmdd >= :dates and t.styymmdd <= :datee"
                sSql += "    and t.sttype    = :sttype"

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

        '-- 검사통계 조회
        'Public Function fnGet_Test_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, ByVal rsADN As String, _
        '                                      ByVal rsTM1 As String, ByVal rsTM2 As String, ByVal rsIO As String, ByVal rsDept As String, ByVal rsWard As String, ByVal rsAbRst As String, _
        '                                      ByVal rsPart As String, ByVal rsSlip As String, ByVal rsBcclsCd As String, ByVal rsWkGrp As String, _
        '                                      ByVal rsTestCds As String, ByVal rsSame As String, ByVal rsSpc As String, ByVal rsMinusExLab As String, ByVal rsTCdGbn As String, _
        '                                      ByVal rsTGrpCd As String, ByVal rbIoGbn_NotC As Boolean) As DataTable
        '    Dim sFn As String = "fnGet_Test_Statistics(String, ... , String) As DataTable"

        '    Try
        '        Dim bIO As Boolean = False

        '        '외래, 입원, 진료과, 병동 통계인지 구분
        '        If (rsIO.Length > 0 Or rsDept.Length > 0 Or rsWard.Length > 0) Then bIO = True

        '        '이상자구분 변수 -> 컬럼명과 일치하도록 변경
        '        If rsAbRst.Length > 0 Then rsAbRst = "ab" + rsAbRst

        '        Dim sSql As String = ""
        '        Dim alParm As New ArrayList

        '        sSql = ""
        '        If rsSpc = "" Then
        '            sSql += "SELECT CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END testcd, '' spccd,"
        '            sSql += "       MIN(b.tnmd) tnm, '' spcnm,"
        '        Else
        '            sSql += "SELECT CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END testcd, a.spccd spccd,"
        '            sSql += "       MIN(b.tnmd) tnm, c.spcnmd spcnm,"
        '        End If
        '        sSql += "       SUM(a.stcnt" + rsAbRst + ") ctotal,"

        '        For i As Integer = 1 To ra_sDMY.Length
        '            Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
        '                Case 8
        '                    '일별 - 일자
        '                    sSql += "       SUM(CASE WHEN a.styymmdd = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

        '                Case 10
        '                    '일별 - 순차시간, 일별 - 시간대
        '                    sSql += "       SUM(CASE WHEN a.styymmdd || a.sthh = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

        '                Case 6
        '                    '월별
        '                    sSql += "       SUM(CASE WHEN a.styymm = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

        '                Case 4
        '                    '연별
        '                    sSql += "       SUM(CASE WHEN a.styy = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

        '            End Select

        '            If i = ra_sDMY.Length Then
        '                sSql += ""
        '            Else
        '                sSql += ","
        '            End If
        '        Next

        '        sSql += "  FROM " + IIf(bIO Or rbIoGbn_NotC, "lt011m", "lt010m").ToString + " a"
        '        sSql += "       INNER JOIN"
        '        sSql += "       ("
        '        sSql += "        SELECT testcd, spccd, MIN(tnmd) tnmd, NVL(MIN(samecd), testcd) samecd"
        '        sSql += "          FROM lf060m"
        '        sSql += "         WHERE usdt <= fn_ack_sysdate"

        '        If rsPart.Length > 0 Then
        '            sSql += "           AND partcd = :partcd"
        '            alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPart.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPart))
        '        End If

        '        If rsSlip.Length > 0 Then
        '            sSql += "           AND partcd = :partcd"
        '            sSql += "           AND slipcd = :slipcd"
        '            alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlip.Substring(0, 1)))
        '            alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlip.Substring(1, 1)))
        '        End If

        '        If rsBcclsCd.Length > 0 Then
        '            sSql += "           AND bcclscd = :bcclscd"
        '            alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
        '        End If

        '        If rsMinusExLab.Length > 0 Then
        '            sSql += "           AND NVL(exlabyn, '0') = '0'"
        '        End If

        '        If rsWkGrp.Length > 0 Then
        '            sSql += "           AND SUBSTR(testcd, 1, 5), spccd IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf066m WHERE wkgrpcd = :wgrpcd)"
        '            alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrp.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrp))
        '        End If

        '        If rsTCdGbn.IndexOf(",") > 0 Then
        '            sSql += "           AND tcdgbn IN (" + rsTCdGbn + ")"
        '        ElseIf rsTCdGbn <> "" Then
        '            sSql += "           AND tcdgbn = :tcdgbn"
        '            alParm.Add(New OracleParameter("tcdgbn", OracleDbType.Varchar2, rsTCdGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTCdGbn))
        '        End If
        '        sSql += "         GROUP BY testcd, spccd"
        '        sSql += "       ) b ON a.testcd = b.testcd"
        '        sSql += "          AND a.spccd = b.spccd "
        '        If rsSpc = "Y" Then
        '            sSql += "       INNER JOIN"
        '            sSql += "       ("
        '            sSql += "        SELECT spccd, min(spcnmd) spcnmd"
        '            sSql += "          FROM lf030m"
        '            sSql += "         GROUP BY spccd"
        '            sSql += "       ) c ON a.spccd = c.spccd"
        '        End If

        '        Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
        '            Case 8
        '                '일별 - 일자
        '                sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' and a.styymmdd <= '" + rsDT2.Replace("-", "") + "'"

        '            Case 10
        '                '일별 - 순차시간, 일별 - 시간대
        '                If rsTM1.Length = 0 And rsTM2.Length = 0 Then
        '                    '순차시간
        '                    sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' AND a.styymmdd <= '" + rsDT2.Replace("-", "") + "'"
        '                    sSql += "   AND a.styymmdd + a.sthh >= '" + ra_sDMY(0).Replace("-", "").Replace(" ", "") + "'"
        '                    sSql += "   AND a.styymmdd + a.sthh <= '" + ra_sDMY(ra_sDMY.Length - 1).Replace("-", "").Replace(" ", "") + "'"
        '                Else
        '                    '시간대
        '                    sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' AND a.styymmdd <= '" + rsDT2.Replace("-", "") + "'"
        '                    sSql += "   AND a.sthh >= '" + rsTM1 + "' and a.sthh <= '" + rsTM2 + "'"
        '                End If

        '            Case 6
        '                '월별
        '                sSql += " WHERE a.styymm >= '" + rsDT1.Replace("-", "") + "' AND a.styymm <= '" + rsDT2.Replace("-", "") + "'"

        '            Case 4
        '                '연별
        '                sSql += " WHERE a.styy >= '" + rsDT1.Replace("-", "") + "' AND a.styy <= '" + rsDT2.Replace("-", "") + "'"

        '        End Select

        '        sSql += "   AND a.sttype = :sttype"
        '        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

        '        Select Case rsADN
        '            Case "D" : sSql += "   AND a.sthh >= '08' AND a.sthh <= '16'"
        '            Case "N" : sSql += "   AND a.sthh >= '00' AND a.sthh <= '07' AND a.sthh >= '17' AND a.sthh <= '23'"
        '        End Select

        '        If rsIO = "O" Then
        '            sSql += "   AND a.stioflg <> 'I'"
        '        ElseIf rsIO <> "" Then
        '            sSql += "   AND a.stioflg = '" + rsIO + "'"
        '        End If

        '        If rsDept <> "" Then
        '            sSql += "   AND a.stdeptcd = '" + rsDept + "'"
        '        End If

        '        If rsWard <> "" Then
        '            sSql += "   AND a.stwardno = '" + rsWard + "'"
        '        End If

        '        '<20130910 정선영 추가, 바코드 분류 구분 적용
        '        If rsBcclsCd.Length > 0 Then
        '            sSql += "   AND   (a.testcd, a.spccd) IN (select testcd, spccd FROM lf060m where bcclscd = :bcclscd)"
        '        End If
        '        '>

        '        If rsTGrpCd.Length > 0 Then
        '            sSql += "   AND SUBSTR(a.testcd, 1, 5), a.spccd IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
        '            alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
        '        End If

        '        If rsTestCds.Length > 0 Then
        '            sSql += "   AND a.testcd IN (" + rsTestCds + ")"
        '        End If

        '        If rbIoGbn_NotC Then sSql += "   AND a.stioflg <> 'C'"

        '        If rsSpc = "Y" Then
        '            sSql += " GROUP BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, a.spccd, c.spcnmd"
        '        Else
        '            sSql += " GROUP BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END"
        '        End If
        '        sSql += " ORDER BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, spccd"

        '        DbCommand()
        '        Return DbExecuteQuery(sSql, alParm)

        '    Catch ex As Exception
        '        Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        '    End Try
        'End Function

        '-- 검사통계 조회
        '2018-07-11 yjh 통계조회 시 현재 검사명이 종료된 검사코드의 이름으로 나오는 건 수정 (원본 위에 주석처리)
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
                    sSql += "SELECT CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END testcd, '' spccd," + vbCrLf
                    sSql += "       MIN(b.tnmd) tnm, '' spcnm," + vbCrLf
                Else
                    sSql += "SELECT CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END testcd, a.spccd spccd," + vbCrLf
                    sSql += "       MIN(b.tnmd) tnm, c.spcnmd spcnm," + vbCrLf
                End If
                sSql += "       SUM(a.stcnt" + rsAbRst + ") ctotal," + vbCrLf

                For i As Integer = 1 To ra_sDMY.Length
                    Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                        Case 8
                            '일별 - 일자
                            sSql += "       SUM(CASE WHEN a.styymmdd = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString + vbCrLf

                        Case 10
                            '일별 - 순차시간, 일별 - 시간대
                            sSql += "       SUM(CASE WHEN a.styymmdd || a.sthh = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString + vbCrLf

                        Case 6
                            '월별
                            sSql += "       SUM(CASE WHEN a.styymm = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString + vbCrLf

                        Case 4
                            '연별
                            sSql += "       SUM(CASE WHEN a.styy = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString + vbCrLf

                    End Select

                    If i = ra_sDMY.Length Then
                        sSql += ""
                    Else
                        sSql += ","
                    End If
                Next

                sSql += "  FROM " + IIf(bIO Or rbIoGbn_NotC, "lt011m", "lt010m").ToString + " a" + vbCrLf
                sSql += "       INNER JOIN" + vbCrLf
                sSql += "       ("
                sSql += "        SELECT usdt, uedt, testcd, spccd, MIN(tnmd) tnmd, NVL(MIN(samecd), testcd) samecd" + vbCrLf '20211109 jhs 검사명 접수일시 기준으로 불러 올수 있도록 추가
                'sSql += "        SELECT testcd, spccd, MIN(tnmd) tnmd, NVL(MIN(samecd), testcd) samecd" + vbCrLf
                sSql += "          FROM lf060m" + vbCrLf
                sSql += "         WHERE usdt <= fn_ack_sysdate" + vbCrLf

                If rsPart.Length > 0 Then
                    sSql += "           AND partcd = :partcd" + vbCrLf
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPart.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPart))
                End If

                If rsSlip.Length > 0 Then
                    sSql += "           AND partcd = :partcd" + vbCrLf
                    sSql += "           AND slipcd = :slipcd" + vbCrLf
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlip.Substring(1, 1)))
                End If

                If rsBcclsCd.Length > 0 Then
                    sSql += "           AND bcclscd = :bcclscd" + vbCrLf
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                If rsMinusExLab.Length > 0 Then
                    sSql += "           AND NVL(exlabyn, '0') = '0'" + vbCrLf
                End If

                If rsWkGrp.Length > 0 Then
                    sSql += "           AND SUBSTR(testcd, 1, 5), spccd IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf066m WHERE wkgrpcd = :wgrpcd)" + vbCrLf
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrp.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrp))
                End If

                If rsTCdGbn.IndexOf(",") > 0 Then
                    sSql += "           AND tcdgbn IN (" + rsTCdGbn + ")" + vbCrLf
                ElseIf rsTCdGbn <> "" Then
                    sSql += "           AND tcdgbn = :tcdgbn" + vbCrLf
                    alParm.Add(New OracleParameter("tcdgbn", OracleDbType.Varchar2, rsTCdGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTCdGbn))
                End If
                'sSql += "         GROUP BY testcd, spccd " + vbCrLf '20211109 jhs 검사명 접수일시 기준으로 불러 올수 있도록 수정
                sSql += "         GROUP BY testcd, spccd ,  usdt, uedt" + vbCrLf
                sSql += "       ) b ON a.testcd = b.testcd" + vbCrLf
                sSql += "          AND a.spccd = b.spccd " + vbCrLf
                '20211109 jhs 검사명 접수일시 기준으로 불러 올수 있도록 추가
                sSql += "          And b.usdt <= a.styymmdd || '000000'" + vbCrLf
                sSql += "          And b.uedt >= a.styymmdd || '000000'" + vbCrLf
                '-------------------------------------
                If rsSpc = "Y" Then
                    sSql += "       INNER JOIN" + vbCrLf
                    sSql += "       (" + vbCrLf
                    sSql += "        SELECT spccd, min(spcnmd) spcnmd" + vbCrLf
                    sSql += "          FROM lf030m" + vbCrLf
                    sSql += "         GROUP BY spccd" + vbCrLf
                    sSql += "       ) c ON a.spccd = c.spccd" + vbCrLf
                End If

                Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                    Case 8
                        '일별 - 일자
                        sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' and a.styymmdd <= '" + rsDT2.Replace("-", "") + "'" + vbCrLf

                    Case 10
                        '일별 - 순차시간, 일별 - 시간대
                        If rsTM1.Length = 0 And rsTM2.Length = 0 Then
                            '순차시간
                            sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' AND a.styymmdd <= '" + rsDT2.Replace("-", "") + "'" + vbCrLf
                            sSql += "   AND a.styymmdd + a.sthh >= '" + ra_sDMY(0).Replace("-", "").Replace(" ", "") + "'" + vbCrLf
                            sSql += "   AND a.styymmdd + a.sthh <= '" + ra_sDMY(ra_sDMY.Length - 1).Replace("-", "").Replace(" ", "") + "'" + vbCrLf
                        Else
                            '시간대
                            sSql += " WHERE a.styymmdd >= '" + rsDT1.Replace("-", "") + "' AND a.styymmdd <= '" + rsDT2.Replace("-", "") + "'" + vbCrLf
                            sSql += "   AND a.sthh >= '" + rsTM1 + "' and a.sthh <= '" + rsTM2 + "'" + vbCrLf
                        End If

                    Case 6
                        '월별
                        sSql += " WHERE a.styymm >= '" + rsDT1.Replace("-", "") + "' AND a.styymm <= '" + rsDT2.Replace("-", "") + "'" + vbCrLf

                    Case 4
                        '연별
                        sSql += " WHERE a.styy >= '" + rsDT1.Replace("-", "") + "' AND a.styy <= '" + rsDT2.Replace("-", "") + "'" + vbCrLf

                End Select

                sSql += "   AND a.sttype = :sttype" + vbCrLf
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                Select Case rsADN
                    Case "D" : sSql += "   AND a.sthh >= '08' AND a.sthh <= '16'" + vbCrLf
                    Case "N" : sSql += "   AND a.sthh >= '00' AND a.sthh <= '07' AND a.sthh >= '17' AND a.sthh <= '23'" + vbCrLf
                End Select

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'" + vbCrLf
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = '" + rsIO + "'" + vbCrLf
                End If

                If rsDept <> "" Then
                    sSql += "   AND a.stdeptcd = '" + rsDept + "'" + vbCrLf
                End If

                If rsWard <> "" Then
                    sSql += "   AND a.stwardno = '" + rsWard + "'" + vbCrLf
                End If

                '<20130910 정선영 추가, 바코드 분류 구분 적용
                If rsBcclsCd.Length > 0 Then
                    sSql += "   AND   (a.testcd, a.spccd) IN (select testcd, spccd FROM lf060m where bcclscd = :bcclscd)" + vbCrLf
                End If
                '>

                If rsTGrpCd.Length > 0 Then
                    sSql += "   AND SUBSTR(a.testcd, 1, 5), a.spccd IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf065m WHERE tgrpcd = :tgrpcd)" + vbCrLf
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                If rsTestCds.Length > 0 Then
                    sSql += "   AND a.testcd IN (" + rsTestCds + ")" + vbCrLf
                End If

                If rbIoGbn_NotC Then sSql += "   AND a.stioflg <> 'C'" + vbCrLf

                If rsSpc = "Y" Then
                    sSql += " GROUP BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, a.spccd, c.spcnmd" + vbCrLf
                Else
                    sSql += " GROUP BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END" + vbCrLf
                End If
                sSql += " ORDER BY CASE WHEN 'Y' = '" + IIf(rsSame = "Y", "Y", "N").ToString + "' THEN b.samecd ELSE a.testcd END, spccd" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


        '-- 검체통계 조회
        Public Function fnGet_spc_Statistics(ByVal rsType As String, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, ByVal rsADN As String, _
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
                sSql += "SELECT '' testcd, a.spccd spccd,"
                sSql += "       '' tnm, c.spcnmd spcnm,"
                sSql += "       SUM(a.stcnt" + rsAbRst + ") ctotal,"

                For i As Integer = 1 To ra_sDMY.Length
                    Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                        Case 8
                            '일별 - 일자
                            sSql += "       SUM(CASE WHEN a.styymmdd = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

                        Case 10
                            '일별 - 순차시간, 일별 - 시간대
                            sSql += "       SUM(CASE WHEN a.styymmdd || a.sthh = '" + ra_sDMY(i - 1).Replace("-", "").Replace(" ", "") + "' THEN a.stcnt" + rsAbRst + " ELSE 0 END) c" + i.ToString

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

                sSql += "  FROM " + IIf(bIO Or rbIoGbn_NotC, "lt011m", "lt010m").ToString + " a"
                sSql += "       INNER JOIN"
                sSql += "       ("
                sSql += "        SELECT testcd, spccd, MIN(tnmd) tnmd, NVL(MIN(samecd), testcd) samecd"
                sSql += "          FROM lf060m"
                sSql += "         WHERE usdt <= fn_ack_sysdate"

                If rsPart.Length > 0 Then
                    sSql += "           AND partcd = :partcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPart.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPart))
                End If

                If rsSlip.Length > 0 Then
                    sSql += "           AND partcd = :partcd"
                    sSql += "           AND slipcd = :slipcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlip.Substring(1, 1)))
                End If

                If rsBcclsCd.Length > 0 Then
                    sSql += "           AND bcclscd = :bcclscd"
                    alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
                End If

                If rsMinusExLab.Length > 0 Then
                    sSql += "           AND NVL(exlabyn, '0') = '0'"
                End If

                If rsWkGrp.Length > 0 Then
                    sSql += "           AND SUBSTR(testcd, 1, 5), spccd IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf066m WHERE wkgrpcd = :wgrpcd)"
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrp.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrp))
                End If

                If rsTCdGbn.IndexOf(",") > 0 Then
                    sSql += "           AND tcdgbn IN (" + rsTCdGbn + ")"
                ElseIf rsTCdGbn <> "" Then
                    sSql += "           AND tcdgbn = :tcdgbn"
                    alParm.Add(New OracleParameter("tcdgbn", OracleDbType.Varchar2, rsTCdGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTCdGbn))
                End If
                sSql += "         GROUP BY testcd, spccd"
                sSql += "       ) b ON a.testcd = b.testcd"
                sSql += "          AND a.spccd = b.spccd "

                sSql += "       INNER JOIN"
                sSql += "       ("
                sSql += "        SELECT spccd, min(spcnmd) spcnmd"
                sSql += "          FROM lf030m"
                sSql += "         GROUP BY spccd"
                sSql += "       ) c ON a.spccd = c.spccd"


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

                If rsIO = "O" Then
                    sSql += "   AND a.stioflg <> 'I'"
                ElseIf rsIO <> "" Then
                    sSql += "   AND a.stioflg = '" + rsIO + "'"
                End If

                If rsDept <> "" Then
                    sSql += "   AND a.stdeptcd = '" + rsDept + "'"
                End If

                If rsWard <> "" Then
                    sSql += "   AND a.stwardno = '" + rsWard + "'"
                End If

                '<20130910 정선영 추가, 바코드 분류 구분 적용
                If rsBcclsCd.Length > 0 Then
                    sSql += "   AND   (a.testcd, a.spccd) IN (select testcd, spccd FROM lf060m where bcclscd = :bcclscd)"
                End If
                '>

                If rsTGrpCd.Length > 0 Then
                    sSql += "   AND SUBSTR(a.testcd, 1, 5), a.spccd IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                If rsTestCds.Length > 0 Then
                    sSql += "   AND a.testcd IN (" + rsTestCds + ")"
                End If

                If rbIoGbn_NotC Then sSql += "   AND a.stioflg <> 'C'"

                sSql += " GROUP BY a.spccd , c.spcnmd "
                sSql += " ORDER BY a.spccd"

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
                sSql += "  FROM lt001m t"
                sSql += " WHERE t.styymmdd >= :dates"
                sSql += "   AND t.styymmdd <= :datee"
                sSql += "   AND t.sttype    = :stype"

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

        '-- TAT 시간대 통계
        Public Function fnGet_TatTime_Statistics(ByVal rsQryGbn As String, ByVal rsRstflg As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartSlip As String, _
                                      ByVal rsDeptCd As String, ByVal rsWardNo As String, ByVal rsIOGbn As String, _
                                         ByVal rsEmerYn As String, ByVal raTests As ArrayList, _
                                           ByVal rbVerity As String, ByVal rbNotPDCA As Boolean) As DataTable
            Dim sFn As String = "Public Function fnGet_TatTime_Statistics(String, String, String, String, String, String, String, String, ArrayList)"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                If rsQryGbn = "" Then
                    '결과단위 TAT
                    sSql = ""
                    sSql += "SELECT f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd," + vbCrLf
                    If rsRstflg = "2" Then
                        '<<< 20170609 TAT 소수점 제거 
                        '<<< 20170704 TAT 응급추가 
                        'sSql += "       f6.prptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
                        sSql += "       case nvl(j.statgbn, ' ') " + vbCrLf
                        sSql += "            when ' '  then   f6.prptmi " + vbCrLf
                        sSql += "            when 'E'  then   f6.perrptmi " + vbCrLf
                        sSql += "       end     tmi " + vbCrLf
                        sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss," + vbCrLf
                    Else
                        'sSql += "       f6.frptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss,"
                        sSql += "       case nvl(j.statgbn, ' ') " + vbCrLf
                        sSql += "            when ' '  then   f6.frptmi " + vbCrLf
                        sSql += "            when 'E'  then   f6.ferrptmi " + vbCrLf
                        sSql += "       end     tmi " + vbCrLf
                        sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss,"
                    End If
                    sSql += "       count(*) totcnt" + vbCrLf
                    sSql += "  FROM lf060m f6," + vbCrLf
                    sSql += "       (" + vbCrLf
                    sSql += "        SELECT    r.bcno, r.tclscd ,r.testcd, r.spccd, NVL(wkdt, tkdt) tkdt, NVL(mwdt, fndt) mwdt, fndt" + vbCrLf
                    'sSql += "        SELECT  /*+ index( f ,PK_LF060M ) */  r.bcno, r.tclscd , r.spccd, NVL(wkdt, tkdt) tkdt, NVL(mwdt, fndt) mwdt, fndt"+vbcrlf
                    sSql += "          FROM lr010m r" + vbCrLf
                    'sSql += "          FROM lr010m r ,  lf060m f"+vbcrlf
                    sSql += "         WHERE r.tkdt >= :dates" + vbCrLf
                    sSql += "           AND r.tkdt <= :datee || '235959'" + vbCrLf

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                    'sSql += "          AND f.testcd = r.testcd "+vbcrlf
                    'sSql += "          AND f.spccd = r.spccd "+vbcrlf
                    'sSql += "          AND f.usdt <= r.tkdt "+vbcrlf
                    'sSql += "          AND f.uedt > r.tkdt "+vbcrlf
                    'sSql += "          AND f.tcdgbn IN ('B', 'S', 'P') "+vbcrlf

                    If rbNotPDCA Then
                        sSql += "           AND NVL(panicmark, ' ') = ' ' AND NVL(deltamark, ' ') = ' ' AND NVL(criticalmark, ' ') = ' ' AND NVL(alertmark, ' ') = ' '" + vbCrLf
                    End If

                    If raTests.Count > 0 Then
                        sSql += "           AND r.testcd IN (" + vbCrLf
                        For ix As Integer = 0 To raTests.Count - 1
                            If ix > 0 Then
                                sSql += ", "
                            End If
                            sSql += ":test" + ix.ToString

                            alParm.Add(New OracleParameter("test" + ix.ToString, OracleDbType.Varchar2, raTests.Item(ix).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, raTests.Item(ix).ToString))
                        Next
                        sSql += ")" + vbCrLf
                    End If

                    If rsRstflg = "2" Then
                        sSql += "           AND NVL(mwdt, ' ') <> ' '" + vbCrLf
                    Else
                        sSql += "           AND NVL(fndt, ' ') <> ' '" + vbCrLf
                    End If

                    If rbVerity = "1" Then
                        sSql += "           AND bcno NOT IN (SELECT bcno FROM lr051m" + vbCrLf
                        sSql += "                             WHERE bcno   = r.bcno" + vbCrLf
                        sSql += "                               AND testcd = r.tclscd" + vbCrLf
                        sSql += "                           )" + vbCrLf
                    ElseIf rbVerity = "2" Then
                        sSql += "           AND bcno IN (SELECT bcno FROM lr051m" + vbCrLf
                        sSql += "                             WHERE bcno   = r.bcno" + vbCrLf
                        sSql += "                               AND testcd = r.tclscd" + vbCrLf
                        sSql += "                           )" + vbCrLf
                    End If


                    sSql += "         UNION ALL" + vbCrLf
                    sSql += "        SELECT   r.bcno, r.tclscd,r.testcd, r.spccd, NVL(wkdt, tkdt) tkdt, NVL(mwdt, fndt) mwdt, fndt" + vbCrLf
                    'sSql += "        SELECT  /*+ index( f ,PK_LF060M ) */  r.bcno, r.tclscd, r.spccd, NVL(wkdt, tkdt) tkdt, NVL(mwdt, fndt) mwdt, fndt"+vbcrlf
                    sSql += "          FROM lm010m r " + vbCrLf
                    'sSql += "          FROM lm010m r , lf060m f"+vbcrlf
                    sSql += "         WHERE r.tkdt >= :dates" + vbCrLf
                    sSql += "           AND r.tkdt <= :datee || '235959'" + vbCrLf
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                    'sSql += "          AND f.testcd = r.testcd "+vbcrlf
                    'sSql += "          AND f.spccd = r.spccd "+vbcrlf
                    'sSql += "          AND f.usdt <= r.tkdt "+vbcrlf
                    'sSql += "          AND f.uedt > r.tkdt "+vbcrlf
                    'sSql += "          AND f.tcdgbn IN ('B', 'S', 'P') "+vbcrlf

                    If rbNotPDCA Then
                        sSql += "           AND NVL(panicmark, ' ') = ' ' AND NVL(deltamark, ' ') = ' ' AND NVL(criticalmark, ' ') = ' ' AND NVL(alertmark, ' ') = ' '" + vbCrLf
                    End If

                    If raTests.Count > 0 Then
                        sSql += "           AND r.testcd IN (" + vbCrLf
                        For ix As Integer = 0 To raTests.Count - 1
                            If ix > 0 Then
                                sSql += ", "
                            End If
                            sSql += ":test" + ix.ToString

                            alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
                        Next
                        sSql += ")"
                    End If


                    If rsRstflg = "2" Then
                        sSql += "           AND NVL(mwdt, ' ') <> ' '" + vbCrLf
                    Else
                        sSql += "           AND NVL(fndt, ' ') <> ' '" + vbCrLf
                    End If

                    If rbVerity = "1" Then
                        sSql += "           AND bcno NOT IN (SELECT bcno FROM lr051m" + vbCrLf
                        sSql += "                             WHERE bcno   = r.bcno" + vbCrLf
                        sSql += "                               AND testcd = r.tclscd" + vbCrLf
                        sSql += "                           )" + vbCrLf
                    ElseIf rbVerity = "2" Then
                        sSql += "           AND bcno IN (SELECT bcno FROM lr051m" + vbCrLf
                        sSql += "                             WHERE bcno   = r.bcno" + vbCrLf
                        sSql += "                               AND testcd = r.tclscd" + vbCrLf
                        sSql += "                           )" + vbCrLf

                    End If

                    sSql += "       ) r," + vbCrLf
                    sSql += "       lj010m j," + vbCrLf
                    sSql += "       lf030m f3" + vbCrLf

                Else
                    '처방단위 TAT
                    sSql = "" + vbCrLf
                    sSql += "SELECT f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd," + vbCrLf
                    If rsRstflg = "2" Then
                        '<<< 20170609 TAT 소수점 제거 
                        '<<< 20170704 TAT 응급 구분 
                        'sSql += "       f6.prptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
                        sSql += "       case nvl(j.statgbn , ' ') " + vbCrLf
                        sSql += "            when ' '  then   f6.prptmi " + vbCrLf
                        sSql += "            when 'E'  then   f6.perrptmi " + vbCrLf
                        sSql += "       end     tmi " + vbCrLf
                        sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss," + vbCrLf
                    Else
                        'sSql += "       f6.frptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss"
                        sSql += "       case nvl(j.statgbn , ' ') " + vbCrLf
                        sSql += "            when ' '  then   f6.frptmi " + vbCrLf
                        sSql += "            when 'E'  then   f6.ferrptmi " + vbCrLf
                        sSql += "       end     tmi " + vbCrLf
                        sSql += "       , trunc(fn_ack_date_+vbcrlfdiff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss," + vbCrLf
                    End If
                    sSql += "       count(*) totcnt" + vbCrLf
                    sSql += "  FROM lf060m f6," + vbCrLf
                    sSql += "       (" + vbCrLf
                    sSql += "        SELECT j1.bcno, j1.tclscd , j1.spccd, MIN(NVL(r.wkdt, r.tkdt)) tkdt, MAX(NVL(r.mwdt, r.fndt)) mwdt, MAX(r.fndt) fndt" + vbCrLf '<<20170912 조회오류수정
                    sSql += "          FROM lr010m r, lj011m j1" + vbCrLf
                    sSql += "         WHERE r.tkdt >= :dates" + vbCrLf
                    sSql += "           AND r.tkdt <= :datee || '235959'" + vbCrLf

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                    If rbNotPDCA Then
                        sSql += "           AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.crticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '" + vbCrLf
                    End If
                    If raTests.Count > 0 Then
                        '<<<20170522
                        sSql += "           AND r.tclscd IN (" + vbCrLf
                        For ix As Integer = 0 To raTests.Count - 1
                            If ix > 0 Then
                                sSql += ", "
                            End If
                            sSql += ":test" + ix.ToString

                            alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
                        Next
                        sSql += ")"
                    End If

                    If rbVerity = "1" Then
                        sSql += "           AND r.bcno NOT IN (SELECT bcno FROM lr051m" + vbCrLf
                        sSql += "                               WHERE bcno    = r.bcno" + vbCrLf
                        sSql += "                                 AND testcd  = r.testcd" + vbCrLf
                        sSql += "                           )" + vbCrLf
                    ElseIf rbVerity = "2" Then
                        sSql += "           AND bcno IN (SELECT bcno FROM lr051m" + vbCrLf
                        sSql += "                             WHERE bcno   = r.bcno" + vbCrLf
                        sSql += "                               AND testcd = r.testcd" + vbCrLf
                        sSql += "                           )" + vbCrLf

                    End If

                    sSql += "           AND j1.bcno   = r.bcno" + vbCrLf
                    sSql += "           AND j1.tclscd = r.tclscd" + vbCrLf
                    sSql += "           AND j1.spccd  = r.spccd" + vbCrLf
                    sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')" + vbCrLf
                    sSql += "         GROUP BY j1.bcno, j1.tclscd, j1.spccd" + vbCrLf
                    sSql += "         UNION ALL" + vbCrLf
                    sSql += "        SELECT j1.bcno, j1.tclscd, j1.spccd, MIN(NVL(r.wkdt, r.tkdt)) tkdt, MAX(NVL(r.mwdt, r.fndt)) mwdt, MAX(r.fndt) fndt" + vbCrLf
                    sSql += "          FROM lm010m r, lj011m j1" + vbCrLf
                    sSql += "         WHERE r.tkdt >= :dates" + vbCrLf
                    sSql += "           AND r.tkdt <= :datee || '235959'" + vbCrLf

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                    If rbNotPDCA Then
                        sSql += "           AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.crticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '" + vbCrLf
                    End If

                    If raTests.Count > 0 Then
                        '<<<20170522 testcd->tclscd로 바꿈 배터리가 조회가 안됨 
                        sSql += "           AND r.tclscd IN (" + vbCrLf
                        For ix As Integer = 0 To raTests.Count - 1
                            If ix > 0 Then
                                sSql += ", "
                            End If
                            sSql += ":test" + ix.ToString

                            alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
                        Next
                        sSql += ")"
                    End If

                    If rbVerity = "1" Then
                        sSql += "           AND r.bcno NOT IN (SELECT bcno FROM lr051m" + vbCrLf
                        sSql += "                               WHERE bcno    = r.bcno" + vbCrLf
                        sSql += "                                 AND testcd  = r.testcd" + vbCrLf
                        sSql += "                             )" + vbCrLf
                    ElseIf rbVerity = "2" Then
                        sSql += "           AND bcno IN (SELECT bcno FROM lr051m" + vbCrLf
                        sSql += "                             WHERE bcno   = r.bcno" + vbCrLf
                        sSql += "                               AND testcd = r.testcd" + vbCrLf
                        sSql += "                           )" + vbCrLf

                    End If

                    sSql += "           AND j1.BCNO   = r.BCNO" + vbCrLf
                    sSql += "           AND j1.tclscd = r.tclscd" + vbCrLf
                    sSql += "           AND j1.spccd  = r.spccd" + vbCrLf
                    sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')" + vbCrLf
                    sSql += "         GROUP BY j1.bcno, j1.tclscd, j1.spccd" + vbCrLf
                    sSql += "       ) r," + vbCrLf
                    sSql += "       lj010M j," + vbCrLf
                    sSql += "       lf030m f3" + vbCrLf

                End If '<<< 검사 / 처방단위 분기 끝 


                sSql += " WHERE f6.testcd  = r.tclscd" + vbCrLf
                sSql += "   AND f6.spccd   = r.spccd" + vbCrLf
                sSql += "   AND f6.usdt   <= r.tkdt" + vbCrLf
                sSql += "   AND f6.uedt   >  r.tkdt" + vbCrLf
                sSql += "   AND f6.tcdgbn IN ('B', 'S', 'P')" + vbCrLf
                sSql += "   AND r.bcno     = j.bcno" + vbCrLf
                sSql += "   AND j.spcflg   = '4'" + vbCrLf
                sSql += "   AND f6.spccd   = f3.spccd" + vbCrLf
                sSql += "   AND f3.usdt   <= r.tkdt" + vbCrLf
                sSql += "   AND f3.uedt   >  r.tkdt" + vbCrLf
                'sSql += "   AND r.testcd = 'LH101' " 'TEST 20160519
                If rsPartSlip <> "" Then
                    sSql += "   AND f6.partcd = :partcd" + vbCrLf
                    sSql += "   AND f6.slipcd = :slipcd" + vbCrLf

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd" + vbCrLf
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If

                If rsIOGbn = "O" Then
                    sSql += "   AND j.iogbn NOT IN ('I', 'D', 'E')" + vbCrLf
                ElseIf rsIOGbn <> "" Then
                    sSql += "   AND j.iogbn IN ('I', 'D', 'E')" + vbCrLf
                End If

                If rsWardNo <> "" Then
                    sSql += "   AND j.wardno = :wardno" + vbCrLf
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardNo))
                End If

                If rsEmerYn = "B" Then
                    sSql += "   AND j.statgbn = 'B'" + vbCrLf
                ElseIf rsEmerYn = "Y" Then
                    sSql += "   AND j.statgbn = 'E'" + vbCrLf
                ElseIf rsEmerYn = "N" Then
                    sSql += "   AND NVL(j.statgbn, ' ') = ' '" + vbCrLf
                End If

                If rsQryGbn = "" Then

                    sSql += " and r.testcd = (select testcd from lf060m where testcd = r.testcd and spccd = r.spccd  and  usdt <= r.tkdt and uedt > r.tkdt AND tcdgbn IN ('B', 'S', 'P') ) " + vbCrLf

                End If




                '<<<20170704 tat응급 맞게 추가 함 
                sSql += " GROUP BY f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,j.statgbn," + vbCrLf
                If rsRstflg = "2" Then
                    sSql += "        f6.prptmi, r.tkdt, r.mwdt ,f6.perrptmi" + vbCrLf
                Else
                    sSql += "        f6.frptmi, r.tkdt, r.fndt ,f6.ferrptmi" + vbCrLf
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '<<<20180518 이전 TAT 통계 
        ''-- TAT 시간대 통계
        'Public Function fnGet_TatTime_Statistics(ByVal rsQryGbn As String, ByVal rsRstflg As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartSlip As String, _
        '                              ByVal rsDeptCd As String, ByVal rsWardNo As String, ByVal rsIOGbn As String, _
        '                                 ByVal rsEmerYn As String, ByVal raTests As ArrayList, _
        '                                   ByVal rbVerity As String, ByVal rbNotPDCA As Boolean) As DataTable
        '    Dim sFn As String = "Public Function fnGet_TatTime_Statistics(String, String, String, String, String, String, String, String, ArrayList)"

        '    Try
        '        Dim sSql As String = ""
        '        Dim alParm As New ArrayList
        '        If rsQryGbn = "" Then
        '            '결과단위 TAT
        '            sSql = ""
        '            sSql += "SELECT f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,"
        '            If rsRstflg = "2" Then
        '                '<<< 20170609 TAT 소수점 제거 
        '                '<<< 20170704 TAT 응급추가 
        '                'sSql += "       f6.prptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
        '                sSql += "       case nvl(j.statgbn, ' ') "
        '                sSql += "            when ' '  then   f6.prptmi "
        '                sSql += "            when 'E'  then   f6.perrptmi "
        '                sSql += "       end     tmi "
        '                sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
        '            Else
        '                'sSql += "       f6.frptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss,"
        '                sSql += "       case nvl(j.statgbn, ' ') "
        '                sSql += "            when ' '  then   f6.frptmi "
        '                sSql += "            when 'E'  then   f6.ferrptmi "
        '                sSql += "       end     tmi "
        '                sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss,"
        '            End If
        '            sSql += "       count(*) totcnt"
        '            sSql += "  FROM lf060m f6,"
        '            sSql += "       ("
        '            sSql += "        SELECT bcno, tclscd, spccd, NVL(wkdt, tkdt) tkdt, NVL(mwdt, fndt) mwdt, fndt"
        '            sSql += "          FROM lr010m r"
        '            sSql += "         WHERE tkdt >= :dates"
        '            sSql += "           AND tkdt <= :datee || '235959'"

        '            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
        '            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

        '            If rbNotPDCA Then
        '                sSql += "           AND NVL(panicmark, ' ') = ' ' AND NVL(deltamark, ' ') = ' ' AND NVL(criticalmark, ' ') = ' ' AND NVL(alertmark, ' ') = ' '"
        '            End If

        '            If raTests.Count > 0 Then
        '                sSql += "           AND testcd IN ("
        '                For ix As Integer = 0 To raTests.Count - 1
        '                    If ix > 0 Then
        '                        sSql += ", "
        '                    End If
        '                    sSql += ":test" + ix.ToString

        '                    alParm.Add(New OracleParameter("test" + ix.ToString, OracleDbType.Varchar2, raTests.Item(ix).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, raTests.Item(ix).ToString))
        '                Next
        '                sSql += ")"
        '            End If

        '            If rsRstflg = "2" Then
        '                sSql += "           AND NVL(mwdt, ' ') <> ' '"
        '            Else
        '                sSql += "           AND NVL(fndt, ' ') <> ' '"
        '            End If

        '            If rbVerity = "1" Then
        '                sSql += "           AND bcno NOT IN (SELECT bcno FROM lr051m"
        '                sSql += "                             WHERE bcno   = r.bcno"
        '                sSql += "                               AND testcd = r.tclscd"
        '                sSql += "                           )"
        '            ElseIf rbVerity = "2" Then
        '                sSql += "           AND bcno IN (SELECT bcno FROM lr051m"
        '                sSql += "                             WHERE bcno   = r.bcno"
        '                sSql += "                               AND testcd = r.tclscd"
        '                sSql += "                           )"
        '            End If


        '            sSql += "         UNION ALL"
        '            sSql += "        SELECT bcno, tclscd, spccd, NVL(wkdt, tkdt) tkdt, NVL(mwdt, fndt) mwdt, fndt"
        '            sSql += "          FROM lm010m r"
        '            sSql += "         WHERE tkdt >= :dates"
        '            sSql += "           AND tkdt <= :datee || '235959'"
        '            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
        '            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

        '            If rbNotPDCA Then
        '                sSql += "           AND NVL(panicmark, ' ') = ' ' AND NVL(deltamark, ' ') = ' ' AND NVL(criticalmark, ' ') = ' ' AND NVL(alertmark, ' ') = ' '"
        '            End If

        '            If raTests.Count > 0 Then
        '                sSql += "           AND testcd IN ("
        '                For ix As Integer = 0 To raTests.Count - 1
        '                    If ix > 0 Then
        '                        sSql += ", "
        '                    End If
        '                    sSql += ":test" + ix.ToString

        '                    alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
        '                Next
        '                sSql += ")"
        '            End If


        '            If rsRstflg = "2" Then
        '                sSql += "           AND NVL(mwdt, ' ') <> ' '"
        '            Else
        '                sSql += "           AND NVL(fndt, ' ') <> ' '"
        '            End If

        '            If rbVerity = "1" Then
        '                sSql += "           AND bcno NOT IN (SELECT bcno FROM lr051m"
        '                sSql += "                             WHERE bcno   = r.bcno"
        '                sSql += "                               AND testcd = r.tclscd"
        '                sSql += "                           )"
        '            ElseIf rbVerity = "2" Then
        '                sSql += "           AND bcno IN (SELECT bcno FROM lr051m"
        '                sSql += "                             WHERE bcno   = r.bcno"
        '                sSql += "                               AND testcd = r.tclscd"
        '                sSql += "                           )"

        '            End If

        '            sSql += "       ) r,"
        '            sSql += "       lj010m j,"
        '            sSql += "       lf030m f3"

        '        Else
        '            '처방단위 TAT
        '            sSql = ""
        '            sSql += "SELECT f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,"
        '            If rsRstflg = "2" Then
        '                '<<< 20170609 TAT 소수점 제거 
        '                '<<< 20170704 TAT 응급 구분 
        '                'sSql += "       f6.prptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
        '                sSql += "       case nvl(j.statgbn , ' ') "
        '                sSql += "            when ' '  then   f6.prptmi "
        '                sSql += "            when 'E'  then   f6.perrptmi "
        '                sSql += "       end     tmi "
        '                sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
        '            Else
        '                'sSql += "       f6.frptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss"
        '                sSql += "       case nvl(j.statgbn , ' ') "
        '                sSql += "            when ' '  then   f6.frptmi "
        '                sSql += "            when 'E'  then   f6.ferrptmi "
        '                sSql += "       end     tmi "
        '                sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss,"
        '            End If
        '            sSql += "       count(*) totcnt"
        '            sSql += "  FROM lf060m f6,"
        '            sSql += "       ("
        '            sSql += "        SELECT j1.bcno, j1.tclscd testcd, j1.spccd, MIN(NVL(r.wkdt, r.tkdt)) tkdt, MAX(NVL(r.mwdt, r.fndt)) mwdt, MAX(r.fndt) fndt" '<<20170912 조회오류수정
        '            sSql += "          FROM lr010m r, lj011m j1"
        '            sSql += "         WHERE r.tkdt >= :dates"
        '            sSql += "           AND r.tkdt <= :datee || '235959'"

        '            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
        '            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

        '            If rbNotPDCA Then
        '                sSql += "           AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.crticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '"
        '            End If
        '            If raTests.Count > 0 Then
        '                '<<<20170522
        '                sSql += "           AND r.tclscd IN ("
        '                For ix As Integer = 0 To raTests.Count - 1
        '                    If ix > 0 Then
        '                        sSql += ", "
        '                    End If
        '                    sSql += ":test" + ix.ToString

        '                    alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
        '                Next
        '                sSql += ")"
        '            End If

        '            If rbVerity = "1" Then
        '                sSql += "           AND r.bcno NOT IN (SELECT bcno FROM lr051m"
        '                sSql += "                               WHERE bcno    = r.bcno"
        '                sSql += "                                 AND testcd  = r.testcd"
        '                sSql += "                           )"
        '            ElseIf rbVerity = "2" Then
        '                sSql += "           AND bcno IN (SELECT bcno FROM lr051m"
        '                sSql += "                             WHERE bcno   = r.bcno"
        '                sSql += "                               AND testcd = r.testcd"
        '                sSql += "                           )"

        '            End If

        '            sSql += "           AND j1.bcno   = r.bcno"
        '            sSql += "           AND j1.tclscd = r.tclscd"
        '            sSql += "           AND j1.spccd  = r.spccd"
        '            sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')"
        '            sSql += "         GROUP BY j1.bcno, j1.tclscd, j1.spccd"
        '            sSql += "         UNION ALL"
        '            sSql += "        SELECT j1.bcno, j1.tclscd testcd, j1.spccd, MIN(NVL(r.wkdt, r.tkdt)) tkdt, MAX(NVL(r.mwdt, r.fndt)) mwdt, MAX(r.fndt) fndt"
        '            sSql += "          FROM lm010m r, lj011m j1"
        '            sSql += "         WHERE r.tkdt >= :dates"
        '            sSql += "           AND r.tkdt <= :datee || '235959'"

        '            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
        '            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

        '            If rbNotPDCA Then
        '                sSql += "           AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.crticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '"
        '            End If

        '            If raTests.Count > 0 Then
        '                '<<<20170522 testcd->tclscd로 바꿈 배터리가 조회가 안됨 
        '                sSql += "           AND r.tcslcd IN ("
        '                For ix As Integer = 0 To raTests.Count - 1
        '                    If ix > 0 Then
        '                        sSql += ", "
        '                    End If
        '                    sSql += ":test" + ix.ToString

        '                    alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
        '                Next
        '                sSql += ")"
        '            End If

        '            If rbVerity = "1" Then
        '                sSql += "           AND r.bcno NOT IN (SELECT bcno FROM lr051m"
        '                sSql += "                               WHERE bcno    = r.bcno"
        '                sSql += "                                 AND testcd  = r.testcd"
        '                sSql += "                             )"
        '            ElseIf rbVerity = "2" Then
        '                sSql += "           AND bcno IN (SELECT bcno FROM lr051m"
        '                sSql += "                             WHERE bcno   = r.bcno"
        '                sSql += "                               AND testcd = r.testcd"
        '                sSql += "                           )"

        '            End If

        '            sSql += "           AND j1.BCNO   = r.BCNO"
        '            sSql += "           AND j1.tclscd = r.tclscd"
        '            sSql += "           AND j1.spccd  = r.spccd"
        '            sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')"
        '            sSql += "         GROUP BY j1.bcno, j1.tclscd, j1.spccd"
        '            sSql += "       ) r,"
        '            sSql += "       lj010M j,"
        '            sSql += "       lf030m f3"

        '        End If
        '        sSql += " WHERE f6.testcd  = r.tclscd"
        '        sSql += "   AND f6.spccd   = r.spccd"
        '        sSql += "   AND f6.usdt   <= r.tkdt"
        '        sSql += "   AND f6.uedt   >  r.tkdt"
        '        sSql += "   AND f6.tcdgbn IN ('B', 'S', 'P')"
        '        sSql += "   AND r.bcno     = j.bcno"
        '        sSql += "   AND j.spcflg   = '4'"
        '        sSql += "   AND f6.spccd   = f3.spccd"
        '        sSql += "   AND f3.usdt   <= r.tkdt"
        '        sSql += "   AND f3.uedt   >  r.tkdt"
        '        'sSql += "   AND r.testcd = 'LH101' " 'TEST 20160519
        '        If rsPartSlip <> "" Then
        '            sSql += "   AND f6.partcd = :partcd"
        '            sSql += "   AND f6.slipcd = :slipcd"

        '            alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
        '            alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
        '        End If

        '        If rsDeptCd <> "" Then
        '            sSql += "   AND j.deptcd = :deptcd"
        '            alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
        '        End If

        '        If rsIOGbn = "O" Then
        '            sSql += "   AND j.iogbn NOT IN ('I', 'D', 'E')"
        '        ElseIf rsIOGbn <> "" Then
        '            sSql += "   AND j.iogbn IN ('I', 'D', 'E')"
        '        End If

        '        If rsWardNo <> "" Then
        '            sSql += "   AND j.wardno = :wardno"
        '            alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardNo))
        '        End If

        '        If rsEmerYn = "B" Then
        '            sSql += "   AND j.statgbn = 'B'"
        '        ElseIf rsEmerYn = "Y" Then
        '            sSql += "   AND j.statgbn = 'E'"
        '        ElseIf rsEmerYn = "N" Then
        '            sSql += "   AND NVL(j.statgbn, ' ') = ' '"
        '        End If


        '        '<<<20170704 tat응급 맞게 추가 함 
        '        sSql += " GROUP BY f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,j.statgbn,"
        '        If rsRstflg = "2" Then
        '            sSql += "        f6.prptmi, r.tkdt, r.mwdt ,f6.perrptmi"
        '        Else
        '            sSql += "        f6.frptmi, r.tkdt, r.fndt ,f6.ferrptmi"
        '        End If

        '        '<<<20180508 임시 롤백
        '        'If rsQryGbn = "" Then
        '        '    '결과단위 TAT
        '        '    sSql = ""
        '        '    sSql += "SELECT f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,"
        '        '    If rsRstflg = "2" Then
        '        '        '<<< 20170609 TAT 소수점 제거 
        '        '        '<<< 20170704 TAT 응급추가 
        '        '        'sSql += "       f6.prptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
        '        '        sSql += "       case nvl(j.statgbn, ' ') "
        '        '        sSql += "            when ' '  then   f6.prptmi "
        '        '        sSql += "            when 'E'  then   f6.perrptmi "
        '        '        sSql += "       end     tmi "
        '        '        sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
        '        '    Else
        '        '        'sSql += "       f6.frptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss,"
        '        '        sSql += "       case nvl(j.statgbn, ' ') "
        '        '        sSql += "            when ' '  then   f6.frptmi "
        '        '        sSql += "            when 'E'  then   f6.ferrptmi "
        '        '        sSql += "       end     tmi "
        '        '        sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss,"
        '        '    End If
        '        '    sSql += "       count(*) totcnt"
        '        '    sSql += "  FROM lf060m f6,"
        '        '    sSql += "       ("
        '        '    sSql += "        SELECT bcno, r.testcd tclscd, spccd, NVL(wkdt, tkdt) tkdt, NVL(mwdt, fndt) mwdt, fndt"
        '        '    sSql += "          FROM lr010m r"
        '        '    sSql += "         WHERE tkdt >= :dates"
        '        '    sSql += "           AND tkdt <= :datee || '235959'"

        '        '    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
        '        '    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

        '        '    If rbNotPDCA Then
        '        '        sSql += "           AND NVL(panicmark, ' ') = ' ' AND NVL(deltamark, ' ') = ' ' AND NVL(criticalmark, ' ') = ' ' AND NVL(alertmark, ' ') = ' '"
        '        '    End If

        '        '    If raTests.Count > 0 Then
        '        '        sSql += "           AND testcd IN ("
        '        '        For ix As Integer = 0 To raTests.Count - 1
        '        '            If ix > 0 Then
        '        '                sSql += ", "
        '        '            End If
        '        '            sSql += ":test" + ix.ToString

        '        '            alParm.Add(New OracleParameter("test" + ix.ToString, OracleDbType.Varchar2, raTests.Item(ix).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, raTests.Item(ix).ToString))
        '        '        Next
        '        '        sSql += ")"
        '        '    End If

        '        '    If rsRstflg = "2" Then
        '        '        sSql += "           AND NVL(mwdt, ' ') <> ' '"
        '        '    Else
        '        '        sSql += "           AND NVL(fndt, ' ') <> ' '"
        '        '    End If

        '        '    If rbVerity = "1" Then
        '        '        sSql += "           AND bcno NOT IN (SELECT bcno FROM lr051m"
        '        '        sSql += "                             WHERE bcno   = r.bcno"
        '        '        sSql += "                               AND testcd = r.tclscd"
        '        '        sSql += "                           )"
        '        '    ElseIf rbVerity = "2" Then
        '        '        sSql += "           AND bcno IN (SELECT bcno FROM lr051m"
        '        '        sSql += "                             WHERE bcno   = r.bcno"
        '        '        sSql += "                               AND testcd = r.tclscd"
        '        '        sSql += "                           )"
        '        '    End If


        '        '    sSql += "         UNION ALL"
        '        '    sSql += "        SELECT bcno, r.testcd tclscd, spccd, NVL(wkdt, tkdt) tkdt, NVL(mwdt, fndt) mwdt, fndt"
        '        '    sSql += "          FROM lm010m r"
        '        '    sSql += "         WHERE tkdt >= :dates"
        '        '    sSql += "           AND tkdt <= :datee || '235959'"
        '        '    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
        '        '    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

        '        '    If rbNotPDCA Then
        '        '        sSql += "           AND NVL(panicmark, ' ') = ' ' AND NVL(deltamark, ' ') = ' ' AND NVL(criticalmark, ' ') = ' ' AND NVL(alertmark, ' ') = ' '"
        '        '    End If

        '        '    If raTests.Count > 0 Then
        '        '        sSql += "           AND testcd IN ("
        '        '        For ix As Integer = 0 To raTests.Count - 1
        '        '            If ix > 0 Then
        '        '                sSql += ", "
        '        '            End If
        '        '            sSql += ":test" + ix.ToString

        '        '            alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
        '        '        Next
        '        '        sSql += ")"
        '        '    End If


        '        '    If rsRstflg = "2" Then
        '        '        sSql += "           AND NVL(mwdt, ' ') <> ' '"
        '        '    Else
        '        '        sSql += "           AND NVL(fndt, ' ') <> ' '"
        '        '    End If

        '        '    If rbVerity = "1" Then
        '        '        sSql += "           AND bcno NOT IN (SELECT bcno FROM lr051m"
        '        '        sSql += "                             WHERE bcno   = r.bcno"
        '        '        sSql += "                               AND testcd = r.tclscd"
        '        '        sSql += "                           )"
        '        '    ElseIf rbVerity = "2" Then
        '        '        sSql += "           AND bcno IN (SELECT bcno FROM lr051m"
        '        '        sSql += "                             WHERE bcno   = r.bcno"
        '        '        sSql += "                               AND testcd = r.tclscd"
        '        '        sSql += "                           )"

        '        '    End If

        '        '    sSql += "       ) r,"
        '        '    sSql += "       lj010m j,"
        '        '    sSql += "       lf030m f3"

        '        'Else
        '        '    '처방단위 TAT
        '        '    sSql = ""
        '        '    sSql += "SELECT f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,"
        '        '    If rsRstflg = "2" Then
        '        '        '<<< 20170609 TAT 소수점 제거 
        '        '        '<<< 20170704 TAT 응급 구분 
        '        '        'sSql += "       f6.prptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
        '        '        sSql += "       case nvl(j.statgbn , ' ') "
        '        '        sSql += "            when ' '  then   f6.prptmi "
        '        '        sSql += "            when 'E'  then   f6.perrptmi "
        '        '        sSql += "       end     tmi "
        '        '        sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.mwdt, '4')) tat_ss,"
        '        '    Else
        '        '        'sSql += "       f6.frptmi tmi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss"
        '        '        sSql += "       case nvl(j.statgbn , ' ') "
        '        '        sSql += "            when ' '  then   f6.frptmi "
        '        '        sSql += "            when 'E'  then   f6.ferrptmi "
        '        '        sSql += "       end     tmi "
        '        '        sSql += "       , trunc(fn_ack_date_diff(r.tkdt, r.fndt, '3')) tat_mi, trunc(fn_ack_date_diff(r.tkdt, r.fndt, '4')) tat_ss,"
        '        '    End If
        '        '    sSql += "       count(*) totcnt"
        '        '    sSql += "  FROM lf060m f6,"
        '        '    sSql += "       ("
        '        '    sSql += "        SELECT j1.bcno, j1.tclscd , j1.spccd, MIN(NVL(r.wkdt, r.tkdt)) tkdt, MAX(NVL(r.mwdt, r.fndt)) mwdt, MAX(r.fndt) fndt" '<<20170912 조회오류수정
        '        '    sSql += "          FROM lr010m r, lj011m j1"
        '        '    sSql += "         WHERE r.tkdt >= :dates"
        '        '    sSql += "           AND r.tkdt <= :datee || '235959'"

        '        '    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
        '        '    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

        '        '    If rbNotPDCA Then
        '        '        sSql += "           AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.crticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '"
        '        '    End If
        '        '    If raTests.Count > 0 Then
        '        '        '<<<20170522 '<<<20180420
        '        '        sSql += "           AND r.tclscd IN ("
        '        '        For ix As Integer = 0 To raTests.Count - 1
        '        '            If ix > 0 Then
        '        '                sSql += ", "
        '        '            End If
        '        '            sSql += ":test" + ix.ToString

        '        '            alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
        '        '        Next
        '        '        sSql += ")"
        '        '    End If

        '        '    If rbVerity = "1" Then
        '        '        sSql += "           AND r.bcno NOT IN (SELECT bcno FROM lr051m"
        '        '        sSql += "                               WHERE bcno    = r.bcno"
        '        '        sSql += "                                 AND testcd  = r.testcd"
        '        '        sSql += "                           )"
        '        '    ElseIf rbVerity = "2" Then
        '        '        sSql += "           AND bcno IN (SELECT bcno FROM lr051m"
        '        '        sSql += "                             WHERE bcno   = r.bcno"
        '        '        sSql += "                               AND testcd = r.testcd"
        '        '        sSql += "                           )"

        '        '    End If

        '        '    sSql += "           AND j1.bcno   = r.bcno"
        '        '    sSql += "           AND j1.tclscd = r.tclscd"
        '        '    sSql += "           AND j1.spccd  = r.spccd"
        '        '    sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')"
        '        '    sSql += "         GROUP BY j1.bcno, j1.tclscd, j1.spccd"
        '        '    sSql += "         UNION ALL"
        '        '    sSql += "        SELECT j1.bcno, j1.tclscd , j1.spccd, MIN(NVL(r.wkdt, r.tkdt)) tkdt, MAX(NVL(r.mwdt, r.fndt)) mwdt, MAX(r.fndt) fndt"
        '        '    sSql += "          FROM lm010m r, lj011m j1"
        '        '    sSql += "         WHERE r.tkdt >= :dates"
        '        '    sSql += "           AND r.tkdt <= :datee || '235959'"

        '        '    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
        '        '    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

        '        '    If rbNotPDCA Then
        '        '        sSql += "           AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.crticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '"
        '        '    End If

        '        '    If raTests.Count > 0 Then
        '        '        '<<<20170522 testcd->tclscd로 바꿈 배터리가 조회가 안됨 '<<<20180420 처방단위 조회을 위해 다시변경 
        '        '        sSql += "           AND r.tclscd IN ("
        '        '        For ix As Integer = 0 To raTests.Count - 1
        '        '            If ix > 0 Then
        '        '                sSql += ", "
        '        '            End If
        '        '            sSql += ":test" + ix.ToString

        '        '            alParm.Add(New OracleParameter("test" + ix.ToString, raTests.Item(ix).ToString))
        '        '        Next
        '        '        sSql += ")"
        '        '    End If

        '        '    If rbVerity = "1" Then
        '        '        sSql += "           AND r.bcno NOT IN (SELECT bcno FROM lr051m"
        '        '        sSql += "                               WHERE bcno    = r.bcno"
        '        '        sSql += "                                 AND testcd  = r.testcd"
        '        '        sSql += "                             )"
        '        '    ElseIf rbVerity = "2" Then
        '        '        sSql += "           AND bcno IN (SELECT bcno FROM lr051m"
        '        '        sSql += "                             WHERE bcno   = r.bcno"
        '        '        sSql += "                               AND testcd = r.testcd"
        '        '        sSql += "                           )"

        '        '    End If

        '        '    sSql += "           AND j1.BCNO   = r.BCNO"
        '        '    sSql += "           AND j1.tclscd = r.tclscd"
        '        '    sSql += "           AND j1.spccd  = r.spccd"
        '        '    sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')"
        '        '    sSql += "         GROUP BY j1.bcno, j1.tclscd, j1.spccd"
        '        '    sSql += "       ) r,"
        '        '    sSql += "       lj010M j,"
        '        '    sSql += "       lf030m f3"

        '        'End If
        '        'sSql += " WHERE f6.testcd  = r.tclscd"
        '        'sSql += "   AND f6.spccd   = r.spccd"
        '        'sSql += "   AND f6.usdt   <= r.tkdt"
        '        'sSql += "   AND f6.uedt   >  r.tkdt"
        '        'sSql += "   AND f6.tcdgbn IN ('B', 'S', 'P')"
        '        'sSql += "   AND r.bcno     = j.bcno"
        '        'sSql += "   AND j.spcflg   = '4'"
        '        'sSql += "   AND f6.spccd   = f3.spccd"
        '        'sSql += "   AND f3.usdt   <= r.tkdt"
        '        'sSql += "   AND f3.uedt   >  r.tkdt"
        '        ''sSql += "   AND r.testcd = 'LH101' " 'TEST 20160519
        '        'If rsPartSlip <> "" Then
        '        '    sSql += "   AND f6.partcd = :partcd"
        '        '    sSql += "   AND f6.slipcd = :slipcd"

        '        '    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
        '        '    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
        '        'End If

        '        'If rsDeptCd <> "" Then
        '        '    sSql += "   AND j.deptcd = :deptcd"
        '        '    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
        '        'End If

        '        'If rsIOGbn = "O" Then
        '        '    sSql += "   AND j.iogbn NOT IN ('I', 'D', 'E')"
        '        'ElseIf rsIOGbn <> "" Then
        '        '    sSql += "   AND j.iogbn IN ('I', 'D', 'E')"
        '        'End If

        '        'If rsWardNo <> "" Then
        '        '    sSql += "   AND j.wardno = :wardno"
        '        '    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardNo))
        '        'End If

        '        'If rsEmerYn = "B" Then
        '        '    sSql += "   AND j.statgbn = 'B'"
        '        'ElseIf rsEmerYn = "Y" Then
        '        '    sSql += "   AND j.statgbn = 'E'"
        '        'ElseIf rsEmerYn = "N" Then
        '        '    sSql += "   AND NVL(j.statgbn, ' ') = ' '"
        '        'End If


        '        ''<<<20170704 tat응급 맞게 추가 함 
        '        'sSql += " GROUP BY f6.dispseql, f6.testcd, f6.tnmd, f6.spccd, f3.spcnmd,j.statgbn,"
        '        'If rsRstflg = "2" Then
        '        '    sSql += "        f6.prptmi, r.tkdt, r.mwdt ,f6.perrptmi"
        '        'Else
        '        '    sSql += "        f6.frptmi, r.tkdt, r.fndt ,f6.ferrptmi"
        '        'End If
        '        '>>>20180508 

        '        DbCommand()
        '        Return DbExecuteQuery(sSql, alParm)

        '    Catch ex As Exception
        '        Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        '    End Try

        'End Function
        '-- 최종보고 통계 조회
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
                        sSql += "          FROM lt011m t, lf060m f"
                        sSql += "         WHERE t.styymmdd >= :dates AND t.styymmdd <= :datee"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += " t.styymm days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM lt011m t, lf060m f"
                        sSql += "         WHERE t.styymm >= :dates AND t.styymm <= :datee"
                    Else
                        sSql += " t.styy days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM lt011m t, lf060m f"
                        sSql += "         WHERE t.styy >= :dates AND t.styy <= :datee"
                    End If

                    sSql += "           AND t.sttype  = :sttype"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                    alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                    If rsIO = "O" Then
                        sSql += "           AND t.stioflg <> 'I'"
                    ElseIf rsIO <> "" Then
                        sSql += "           AND t.stioflg = :iogbn"
                        alParm.Add(New OracleParameter("iogbn", rsIO))
                    End If

                    If rsIO = "I" Then
                        If rsWard.Length > 0 Then
                            sSql += "           AND t.stwardno = :wardno"
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
                        sSql += "                          FROM lt021m t, lf060m f"
                        sSql += "                         WHERE t.styymmdd >= :dates AND t.styymmdd <= :datee"
                        sSql += "                           AND t.sttype    = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsIO = "O" Then
                            sSql += "                           AND t.stioflg  <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                           AND t.stioflg   = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsIO = "I" Then
                            If rsWard.Length > 0 Then
                                sSql += "                           AND t.stwardno = :wardno"
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
                        sSql += "                          FROM lt021m t, lf060m f"
                        sSql += "                         WHERE t.styymm >= :dates AND t.styymm <= :datee"
                        sSql += "                           AND t.sttype  = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsIO = "O" Then
                            sSql += "                           AND t.stioflg <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                           AND t.stioflg = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsIO = "I" Then
                            If rsWard.Length > 0 Then
                                sSql += "                           AND t.stwardno = :wardno"
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
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd, t.styy days, SUM(t.stcnt) stcnt FROM lt021m t, lf060m f"
                        sSql += "                         WHERE t.styy   >= :dates AND t.styy <= :datee"
                        sSql += "                           AND t.sttype  = :sttype"

                        alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                        alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                        alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                        If rsIO = "O" Then
                            sSql += "                           AND t.stioflg <> 'I'"
                        ElseIf rsIO <> "" Then
                            sSql += "                           AND t.stioflg = :iogbn"
                            alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIO))
                        End If

                        If rsIO = "I" Then
                            If rsWard.Length > 0 Then
                                sSql += "                           AND t.stwardno = :wardno"
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
                        sSql += "          FROM lt010m t, lf060m f"
                        sSql += "         WHERE t.styymmdd >= :dates AND t.styymmdd <= :datee"
                    ElseIf rsDMYGbn = "M" Then
                        sSql += " t.styymm days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM lt010m t, lf060m f"
                        sSql += "         WHERE t.styymm >= :dates AND t.styymm <= :datee"
                    Else
                        sSql += " t.styy days, SUM(t.stcnt) stcnt"
                        sSql += "          FROM lt010m t, lf060m f"
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
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd slipcd, t.styymmdd days, SUM(t.stcnt) stcnt FROM lt020m t, lf060m f"
                        sSql += "                    WHERE t.styymmdd >= :dates AND t.styymmdd <= :datee"
                        sSql += "                      AND t.sttype    = :sttype"
                        sSql += "                      AND t.testcd = f.testcd"
                        sSql += "                      AND t.spccd  = f.spccd"
                        sSql += "                      AND f.usdt  <= fn_ack_sysdate"
                        sSql += "                      AND f.uedt  >  fn_ack_sysdate"
                        sSql += "                    GROUP BY f.partcd || f.slipcd, t.styymmdd"
                        sSql += "                  ) a1 ON a.slipcd = a1.slipcd AND a.days = a1.days"

                    ElseIf rsDMYGbn = "M" Then
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd slipcd, t.styymm days, SUM(t.stcnt) stcnt FROM lt020m t, lf060m f"
                        sSql += "                    WHERE t.styymm >= :dates AND t.styymm <= :datee"
                        sSql += "                      AND t.sttype  = :sttype"
                        sSql += "                      AND t.testcd = f.testcd"
                        sSql += "                      AND t.spccd  = f.spccd"
                        sSql += "                      AND f.usdt  <= fn_ack_sysdate"
                        sSql += "                      AND f.uedt  >  fn_ack_sysdate"
                        sSql += "                    GROUP BY f.partcd || f.slipcd, t.styymm"
                        sSql += "                  ) a1 ON a.slipcd = a1.slipcd AND a.days = a1.days"
                    Else
                        sSql += "       LEFT OUTER JOIN (SELECT f.partcd || f.slipcd slipcd, t.styy days, SUM(t.stcnt) stcnt FROM lt020m t, lf060m f"
                        sSql += "                    WHERE t.styy  >= :dates AND t.styy <= :datee"
                        sSql += "                      AND t.sttype = :sttype"
                        sSql += "                      AND t.testcd = f.testcd"
                        sSql += "                      AND t.spccd  = f.spccd"
                        sSql += "                      AND f.usdt  <= fn_ack_sysdate()"
                        sSql += "                      AND f.uedt  >  fn_ack_sysdate()"
                        sSql += "                    GROUP BY f.partcd || f.slipcd, t.styy"
                        sSql += "                  ) a1 ON a.slipcd = a1.slipcd AND a.days = a1.days"
                    End If

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDT1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT1))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDT2.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDT2))
                    alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsType))

                End If


                sSql += "       INNER JOIN (SELECT partcd || slipcd slipcd, MIN(dispseq) dispseq, MAX(slipnmd) slipnmd"
                sSql += "                     FROM lf021m"
                sSql += "                    GROUP BY partcd || slipcd"
                sSql += "                  ) f ON a.slipcd= f.slipcd "

                sSql += " ORDER BY 1, 2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 최종건수 통계 작업리스트
        Public Function fnGet_Final_AnalysisInfo(ByVal rsDayB As String, ByVal rsDayE As String, ByVal rsType As String) As DataTable
            Dim sFn As String = "fnGet_Final_AnalysisInfo(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += " select t.styymmdd, t.sttype, fn_ack_date_str(t.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "        t.regid, fn_ack_get_usr_name(t.regid) regnm"
                sSql += "   from lt002m t"
                sSql += "  where t.styymmdd >= :dates and t.styymmdd <= :datee"
                sSql += "    and t.sttype    = :sttype"

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

        '-- 채혈통계(시간대별)
        Public Function fnGet_Coll_Statistics(ByVal rsDayGbn As String, ByVal rsDate As String, ByVal rsCollId As String) As DataTable
            Dim sFn As String = "fnGet_Coll_Statistics(String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim arlParm As New ArrayList

                sSql += "SELECT CASE WHEN iogbn = 'I' THEN 'I' ELSE 'O' END iogbn,"
                sSql += "       SUM(h06) h06,"
                sSql += "       SUM(h07) h07, SUM(h08) h08, SUM(h09) h09, SUM(h10) h10, SUM(h11) h11, SUM(h12) h12, SUM(h13) h13,"
                sSql += "       SUM(h14) h14, SUM(h15) h15, SUM(h16) h16, SUM(h17) h17, SUM(h18) h18, SUM(h19) h19, SUM(tot) tot"
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
                sSql += "                  FROM lj011m b, lj010m a"
                sSql += "                 WHERE b.colldt >= :dates"
                sSql += "                   AND b.colldt <  :datee"
                sSql += "                   AND a.owngbn <> 'H'"

                If rsDayGbn = "D" Then
                    Dim sDateE As String = ""
                    sDateE = Format(DateAdd(DateInterval.Day, 1, CDate(rsDate)), "yyyyMMdd")

                    arlParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-", "")))
                    arlParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                ElseIf rsDayGbn = "M" Then
                    Dim sDateE As String = ""
                    sDateE = Format(DateAdd(DateInterval.Month, 1, CDate(rsDate + "-01")), "yyyyMMdd")

                    arlParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (rsDate.Replace("-", "") + "01").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-", "") + "01"))
                    arlParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                Else
                    Dim sDateE As String = ""
                    sDateE = Format(DateAdd(DateInterval.Year, 1, CDate(rsDate + "-01-01")), "yyyyMMdd")

                    arlParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (rsDate.Replace("-", "") + "0101").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-", "") + "0101"))
                    arlParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
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
                sSql += " GROUP BY CASE WHEN iogbn = 'I' THEN 'I' ELSE 'O' END"

                DbCommand()
                Return DbExecuteQuery(sSql, arlParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '-- TAT 관리 통계
        Public Function fnGet_TatTest_Statistics(ByVal rsTkDtS As String, ByVal rsTkDtE As String, ByVal rsIoGbn As String, ByVal rsDeptCd As String, ByVal rsWard As String, _
                                                 ByVal rsPartSlip As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsEmerYN As String, _
                                                 ByVal rbVerity As Boolean, ByVal rbNotPDCA As Boolean, ByVal rbIoGbn_noC As Boolean) As DataTable
            Dim sFn As String = "fnGet_TatTest_Statistics(String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT /*+ INDEX(R IDX_LR010M_3) */"
                sSql += "       fn_ack_date_str(NVL(r.wkdt, r.tkdt), 'hh24:mi') tk_tm,"
                sSql += "       COUNT(r.testcd) cnt,"
                sSql += "       trunc( fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.rstdt, '3')) rst_tm"
                sSql += "  FROM lr010m r, lj010m j ,lf060m f "
                sSql += " WHERE r.tkdt >= :dates || '000000'"
                sSql += "   AND r.tkdt <= :datee || '235959'"
                sSql += "   AND SUBSTR(r.tkdt, 9, 6) >= :times || '0000'"
                sSql += "   AND SUBSTR(r.tkdt, 9, 6) <= :timee || '5959'"

                al.Add(New OracleParameter("dates", rsTkDtS.Substring(0, 8)))
                al.Add(New OracleParameter("datee", rsTkDtE.Substring(0, 8)))
                al.Add(New OracleParameter("times", rsTkDtS.Substring(8, 2)))
                al.Add(New OracleParameter("timee", rsTkDtE.Substring(8, 2)))

                If rsTestCd = "" Then
                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd)"
                    al.Add(New OracleParameter("partcd", rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", rsPartSlip.Substring(1, 1)))
                Else
                    'sSql += "   AND r.testcd = :testcd"
                    'sSql += " AND r.testcd in (:testcd) "
                    'al.Add(New OracleParameter("testcd", rsTestCd))
                    '<<<20170522
                    sSql += " AND r.testcd in (" + Trim(rsTestCd) + ")" '<<<20170711 검사항목 조회가 안되서 수정함 

                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND r.spccd = :spccd"

                    al.Add(New OracleParameter("spccd", rsSpcCd))
                End If

                sSql += "   AND r.rstflg > '1'"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"

                If rsIoGbn = "O" Then
                    sSql += "   AND j.iogbn NOT IN ('I', 'D', 'E')"
                ElseIf rsIoGbn <> "" Then
                    sSql += "   AND j.iogbn IN ('I', 'D', 'E')"
                End If

                If rbIoGbn_noC Then
                    sSql += "   AND j.iogbn <> 'C'"
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    al.Add(New OracleParameter("deptcd", rsDeptCd))
                End If

                If rsWard <> "" Then
                    sSql += "   and j.wardno = :wardno"
                    al.Add(New OracleParameter("wardno", rsWard))
                End If

                If rbNotPDCA Then
                    sSql += "   AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.criticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '"
                End If

                If rbVerity Then
                    sSql += "   AND r.bcno NOT IN (SELECT a.bcno FROM lr010m b, lr051m a"
                    sSql += "                       WHERE b.tkdt  >= :dates || '0000'"
                    sSql += "                         AND b.tkdt  <= :datee || '5959'"

                    al.Add(New OracleParameter("dates", rsTkDtS))
                    al.Add(New OracleParameter("datee", rsTkDtE))

                    sSql += "                         AND a.bcno   = b.bcno"
                    sSql += "                         AND a.testcd = b.testcd"

                    If rsTestCd = "" Then
                    Else
                        ' sSql += "                         AND b.testcd = :testcd"
                        'al.Add(New OracleParameter("testcd", rsTestCd))
                        '20170522
                        sSql += " AND r.testcd in (" + Trim(rsTestCd) + ")" '<<<20170711 검사항목 조회가 안되서 수정함 
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "                         AND b.spccd = :spccd"

                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If

                    sSql += "                     )"

                End If

                If rsEmerYN = "N" Then
                    sSql += "   AND (NVL(j.statgbn, ' ') = ' ' OR j.statgbn IS NULL)"
                ElseIf rsEmerYN = "Y" Then
                    sSql += "   AND NVL(j.statgbn, ' ') = 'E'"
                End If
                sSql += "and f.testcd = r.testcd "
                sSql += "and f.spccd = r.spccd "
                sSql += "and f.usdt <= r.tkdt "
                sSql += "and f.uedt >= r.tkdt  "
                sSql += "AND ((f.tcdgbn = 'B' AND NVL(f.titleyn, '0') = '0') OR f.tcdgbn IN ('S', 'P'))"

                sSql += " GROUP BY fn_ack_date_str(NVL(r.wkdt, r.tkdt), 'hh24:mi'), fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.rstdt, '3')"
                sSql += " ORDER BY tk_tm"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGet_TatTest_Statistics_new(ByVal rsTkDtS As String, ByVal rsTkDtE As String, ByVal rsIoGbn As String, ByVal rsDeptCd As String, ByVal rsWard As String, _
                                                 ByVal rsPartSlip As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsEmerYN As String, _
                                                 ByVal rbVerity As Boolean, ByVal rbNotPDCA As Boolean, ByVal rbIoGbn_noC As Boolean, ByVal rsRstGbn As String) As DataTable
            Dim sFn As String = "fnGet_TatTest_Statistics(String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList
                Dim rst As String = ""

                If rsRstGbn = "F" Then
                    rst = "r.rstdt"
                ElseIf rsRstGbn = "M" Then
                    rst = "r.mwdt"
                End If


                sSql += "SELECT /*+ INDEX(R IDX_LR010M_3) */"
                sSql += "       fn_ack_date_str(NVL(r.wkdt, r.tkdt), 'hh24:mi') tk_tm,"
                sSql += "       COUNT(r.testcd) cnt,"
                sSql += "       trunc( fn_ack_date_diff(NVL(r.wkdt, r.tkdt), " + rst + ", '3')) rst_tm"
                sSql += "  FROM lr010m r, lj010m j ,lf060m f "
                sSql += " WHERE r.tkdt >= :dates || '000000'"
                sSql += "   AND r.tkdt <= :datee || '235959'"
                sSql += "   AND SUBSTR(r.tkdt, 9, 6) >= :times || '0000'"
                sSql += "   AND SUBSTR(r.tkdt, 9, 6) <= :timee || '5959'"

                al.Add(New OracleParameter("dates", rsTkDtS.Substring(0, 8)))
                al.Add(New OracleParameter("datee", rsTkDtE.Substring(0, 8)))
                al.Add(New OracleParameter("times", rsTkDtS.Substring(8, 2)))
                al.Add(New OracleParameter("timee", rsTkDtE.Substring(8, 2)))

                If rsTestCd = "" Then
                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd)"
                    al.Add(New OracleParameter("partcd", rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", rsPartSlip.Substring(1, 1)))
                Else
                    'sSql += "   AND r.testcd = :testcd"
                    'sSql += " AND r.testcd in (:testcd) "
                    'al.Add(New OracleParameter("testcd", rsTestCd))
                    '<<<20170522
                    sSql += " AND r.testcd in (" + Trim(rsTestCd) + ")" '<<<20170711 검사항목 조회가 안되서 수정함 

                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND r.spccd = :spccd"

                    al.Add(New OracleParameter("spccd", rsSpcCd))
                End If

                If rsRstGbn = "M" Then
                    sSql += "   AND r.rstflg > '1'"
                ElseIf rsRstGbn = "F" Then
                    sSql += "   AND r.rstflg > '2'"
                End If

                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"

                If rsIoGbn = "O" Then
                    sSql += "   AND j.iogbn NOT IN ('I', 'D', 'E')"
                ElseIf rsIoGbn <> "" Then
                    sSql += "   AND j.iogbn IN ('I', 'D', 'E')"
                End If

                If rbIoGbn_noC Then
                    sSql += "   AND j.iogbn <> 'C'"
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    al.Add(New OracleParameter("deptcd", rsDeptCd))
                End If

                If rsWard <> "" Then
                    sSql += "   and j.wardno = :wardno"
                    al.Add(New OracleParameter("wardno", rsWard))
                End If

                If rbNotPDCA Then
                    sSql += "   AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.criticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '"
                End If

                If rbVerity Then
                    sSql += "   AND r.bcno NOT IN (SELECT a.bcno FROM lr010m b, lr051m a"
                    sSql += "                       WHERE b.tkdt  >= :dates || '0000'"
                    sSql += "                         AND b.tkdt  <= :datee || '5959'"

                    al.Add(New OracleParameter("dates", rsTkDtS))
                    al.Add(New OracleParameter("datee", rsTkDtE))

                    sSql += "                         AND a.bcno   = b.bcno"
                    sSql += "                         AND a.testcd = b.testcd"

                    If rsTestCd = "" Then
                    Else
                        ' sSql += "                         AND b.testcd = :testcd"
                        'al.Add(New OracleParameter("testcd", rsTestCd))
                        '20170522
                        sSql += " AND r.testcd in (" + Trim(rsTestCd) + ")" '<<<20170711 검사항목 조회가 안되서 수정함 
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "                         AND b.spccd = :spccd"

                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If

                    sSql += "                     )"

                End If

                If rsEmerYN = "N" Then
                    sSql += "   AND (NVL(j.statgbn, ' ') = ' ' OR j.statgbn IS NULL)"
                ElseIf rsEmerYN = "Y" Then
                    sSql += "   AND NVL(j.statgbn, ' ') = 'E'"
                End If
                sSql += "and f.testcd = r.testcd "
                sSql += "and f.spccd = r.spccd "
                sSql += "and f.usdt <= r.tkdt "
                sSql += "and f.uedt >= r.tkdt  "
                sSql += "AND ((f.tcdgbn = 'B' AND NVL(f.titleyn, '0') = '0') OR f.tcdgbn IN ('S', 'P'))"

                sSql += " GROUP BY fn_ack_date_str(NVL(r.wkdt, r.tkdt), 'hh24:mi'), fn_ack_date_diff(NVL(r.wkdt, r.tkdt), " + rst + ", '3')"
                sSql += " ORDER BY tk_tm"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '<<<20180518 이전 TAT관리 
        '-- TAT 관리 통계
        'Public Function fnGet_TatTest_Statistics(ByVal rsTkDtS As String, ByVal rsTkDtE As String, ByVal rsIoGbn As String, ByVal rsDeptCd As String, ByVal rsWard As String, _
        '                                         ByVal rsPartSlip As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsEmerYN As String, _
        '                                         ByVal rbVerity As Boolean, ByVal rbNotPDCA As Boolean, ByVal rbIoGbn_noC As Boolean) As DataTable
        '    Dim sFn As String = "fnGet_TatTest_Statistics(String, String, String) As DataTable"
        '    Try
        '        Dim sSql As String = ""
        '        Dim al As New ArrayList

        '        sSql += "SELECT /*+ INDEX(R IDX_LR010M_3) */"
        '        sSql += "       fn_ack_date_str(NVL(r.wkdt, r.tkdt), 'hh24:mi') tk_tm,"
        '        sSql += "       COUNT(r.testcd) cnt,"
        '        '<<<20170515 TAT 소수점 없엠 ( 목표가 15분이면 15.023 이런것은 안걸려야 하기 때문에..
        '        sSql += "       trunc( fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.rstdt, '3')) rst_tm"
        '        sSql += "  FROM lr010m r, lj010m j"
        '        sSql += " WHERE r.tkdt >= :dates || '000000'"
        '        sSql += "   AND r.tkdt <= :datee || '235959'"
        '        sSql += "   AND SUBSTR(r.tkdt, 9, 6) >= :times || '0000'"
        '        sSql += "   AND SUBSTR(r.tkdt, 9, 6) <= :timee || '5959'"

        '        al.Add(New OracleParameter("dates", rsTkDtS.Substring(0, 8)))
        '        al.Add(New OracleParameter("datee", rsTkDtE.Substring(0, 8)))
        '        al.Add(New OracleParameter("times", rsTkDtS.Substring(8, 2)))
        '        al.Add(New OracleParameter("timee", rsTkDtE.Substring(8, 2)))

        '        If rsTestCd = "" Then
        '            sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd)"
        '            al.Add(New OracleParameter("partcd", rsPartSlip.Substring(0, 1)))
        '            al.Add(New OracleParameter("slipcd", rsPartSlip.Substring(1, 1)))
        '        Else
        '            'sSql += "   AND r.testcd = :testcd"
        '            'sSql += " AND r.testcd in (:testcd) "
        '            'al.Add(New OracleParameter("testcd", rsTestCd))
        '            '<<<20170522
        '            sSql += " AND r.testcd in (" + Trim(rsTestCd) + ")" '<<<20170711 검사항목 조회가 안되서 수정함 

        '        End If

        '        If rsSpcCd <> "" Then
        '            sSql += "   AND r.spccd = :spccd"

        '            al.Add(New OracleParameter("spccd", rsSpcCd))
        '        End If

        '        sSql += "   AND r.rstflg > '1'"
        '        sSql += "   AND j.bcno   = r.bcno"
        '        sSql += "   AND j.spcflg = '4'"

        '        If rsIoGbn = "O" Then
        '            sSql += "   AND j.iogbn NOT IN ('I', 'D', 'E')"
        '        ElseIf rsIoGbn <> "" Then
        '            sSql += "   AND j.iogbn IN ('I', 'D', 'E')"
        '        End If

        '        If rbIoGbn_noC Then
        '            sSql += "   AND j.iogbn <> 'C'"
        '        End If

        '        If rsDeptCd <> "" Then
        '            sSql += "   AND j.deptcd = :deptcd"
        '            al.Add(New OracleParameter("deptcd", rsDeptCd))
        '        End If

        '        If rsWard <> "" Then
        '            sSql += "   and j.wardno = :wardno"
        '            al.Add(New OracleParameter("wardno", rsWard))
        '        End If

        '        If rbNotPDCA Then
        '            sSql += "   AND NVL(r.panicmark, ' ') = ' ' AND NVL(r.deltamark, ' ') = ' ' AND NVL(r.criticalmark, ' ') = ' ' AND NVL(r.alertmark, ' ') = ' '"
        '        End If

        '        If rbVerity Then
        '            sSql += "   AND r.bcno NOT IN (SELECT a.bcno FROM lr010m b, lr051m a"
        '            sSql += "                       WHERE b.tkdt  >= :dates || '0000'"
        '            sSql += "                         AND b.tkdt  <= :datee || '5959'"

        '            al.Add(New OracleParameter("dates", rsTkDtS))
        '            al.Add(New OracleParameter("datee", rsTkDtE))

        '            sSql += "                         AND a.bcno   = b.bcno"
        '            sSql += "                         AND a.testcd = b.testcd"

        '            If rsTestCd = "" Then
        '            Else
        '                ' sSql += "                         AND b.testcd = :testcd"
        '                'al.Add(New OracleParameter("testcd", rsTestCd))
        '                '20170522
        '                sSql += " AND r.testcd in (" + Trim(rsTestCd) + ")" '<<<20170711 검사항목 조회가 안되서 수정함 
        '            End If

        '            If rsSpcCd <> "" Then
        '                sSql += "                         AND b.spccd = :spccd"

        '                al.Add(New OracleParameter("spccd", rsSpcCd))
        '            End If

        '            sSql += "                     )"

        '        End If

        '        If rsEmerYN = "N" Then
        '            sSql += "   AND (NVL(j.statgbn, ' ') = ' ' OR j.statgbn IS NULL)"
        '        ElseIf rsEmerYN = "Y" Then
        '            sSql += "   AND NVL(j.statgbn, ' ') = 'E'"
        '        End If

        '        sSql += " GROUP BY fn_ack_date_str(NVL(r.wkdt, r.tkdt), 'hh24:mi'), fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.rstdt, '3')"
        '        sSql += " ORDER BY tk_tm"

        '        DbCommand()
        '        Return DbExecuteQuery(sSql, al)

        '    Catch ex As Exception
        '        Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        '    End Try

        'End Function

        '-- 채혈통계(처방의 통계)
        Public Function fnGet_Test_Statistics_dr(ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "fnGet_Test_Statistics_dr(String, ... , String) As DataTable"

            Try
                Dim bIO As Boolean = False

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT fn_ack_get_dr_name(a.drcd) drnm, a.drcd, SUM(a.total) total"
                For ix As Integer = 1 To ra_sDMY.Length
                    sSql += ", SUM(c" + ix.ToString + ") c" + ix.ToString
                Next

                sSql += "  FROM ("
                sSql += "        SELECT orgdoctorcd drcd, count(*) total,"
                For ix As Integer = 1 To ra_sDMY.Length
                    Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                        Case 8
                            '일별 - 일자
                            sSql += "CASE WHEN SUBSTR(j.tkdt, 1, 8) = '" + ra_sDMY(ix - 1).Replace("-", "").Replace(" ", "") + "' THEN count(*) ELSE 0 END c" + ix.ToString
                        Case 6
                            '월별
                            sSql += "CASE WHEN SUBSTR(j.tkdt, 1, 6) = '" + ra_sDMY(ix - 1).Replace("-", "").Replace(" ", "") + "' THEN count(*) ELSE 0 END c" + ix.ToString

                        Case 4
                            '연별
                            sSql += "CASE WHEN SUBSTR(j.tkdt, 1, 4) = '" + ra_sDMY(ix - 1).Replace("-", "").Replace(" ", "") + "' THEN count(*) ELSE 0 END c" + ix.ToString

                    End Select

                    If ix = ra_sDMY.Length Then
                        sSql += ""
                    Else
                        sSql += ","
                    End If
                Next

                sSql += "          FROM lj011m j"
                sSql += "         WHERE j.tclscd = :tclscd"

                alParm.Add(New OracleParameter("tclscd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

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
                sSql += "         GROUP BY orgdoctorcd, "

                Select Case ra_sDMY(0).Replace("-", "").Replace(" ", "").Length
                    Case 8 : sSql += "SUBSTR(j.tkdt, 1, 8)"
                    Case 6 : sSql += "SUBSTR(j.tkdt, 1, 6)"
                    Case 4 : sSql += "SUBSTR(j.tkdt, 1, 4)"
                End Select
                sSql += "       ) a"

                sSql += " GROUP BY fn_ack_get_dr_name(a.drcd), a.drcd"
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
                sSql += "  FROM lt011m a"
                sSql += "       INNER JOIN"
                sSql += "       ("
                sSql += "        SELECT testcd, MIN(tnmd) tnmd, NVL(MIN(samecd), testcd) samecd, MIN(dispseql) dispseq"
                sSql += "          FROM lf060m"
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
                    sSql += " WHERE a.styymm = :stdate"
                Else
                    sSql += " WHERE a.styyyy = :stdate"
                End If
                alParm.Add(New OracleParameter("stdate", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                sSql += "   AND a.sttype = :sttype"
                alParm.Add(New OracleParameter("sttype", OracleDbType.Varchar2, rsStType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsStType))

                If rsIoGbn = "I" Then
                    sSql += "   AND a.stioflg IN ('I', 'D', 'E')"
                ElseIf rsIoGbn = "O" Then
                    sSql += "   AND a.stioflg NOT IN ('I', 'D', 'E')"
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
        Public Function fnExe_Test_Statistics(ByVal rsStDate As String) As String
            Dim sFn As String = "fnExe_Test_Statistics(String,  String) As Date"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim sRetVal As String = ""

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = dbCn
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_sta_test"

                    .Parameters.Clear()
                    .Parameters.Add("rs_date", OracleDbType.Varchar2).Value = rsStDate
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_usrip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 1000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sRetVal

                    .ExecuteNonQuery()

                    sRetVal = .Parameters(3).Value.ToString

                End With

                If sRetVal = "OK" Then
                    sRetVal = Format(Now, "yyyy-MM-dd HH:mm:ss").ToString
                Else
                    sRetVal = ""
                End If

                Return sRetVal

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        '-- 최종보고 통계 작업
        Public Function fnExe_Final_Statistics(ByVal rsStDate As String) As String
            Dim sFn As String = "fnExe_Final_Statistics(String) As Date"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim sRetVal As String = ""

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                With dbCmd
                    .Connection = dbCn
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_sta_final"

                    .Parameters.Clear()
                    .Parameters.Add("rs_date", OracleDbType.Varchar2).Value = rsStDate
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_usrip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 1000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sRetVal

                    .ExecuteNonQuery()

                    sRetVal = .Parameters(3).Value.ToString

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

        '-- 미생물(균/항생제) 통계 작업
        Public Function fnExe_Micro_Statistics(ByVal rsStDate As String) As String
            Dim sFn As String = "fnExe_Micro_Statistics(String) As Date"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim sRetVal As String = ""

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = dbCn
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_sta_micro"

                    .Parameters.Clear()
                    .Parameters.Add("rs_date", OracleDbType.Varchar2).Value = rsStDate
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_usrip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 1000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sRetVal

                    .ExecuteNonQuery()
                    sRetVal = .Parameters(3).Value.ToString

                End With

                If sRetVal = "OK" Then
                    sRetVal = Format(Now, "yyyy-MM-dd HH:mm:ss").ToString
                Else
                    sRetVal = ""
                End If

                Return sRetVal

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try


        End Function

    End Class

End Namespace
