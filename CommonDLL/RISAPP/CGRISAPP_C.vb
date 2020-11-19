Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Namespace APP_C
    Public Class CollFn
        Private Const msFile As String = "File : CGRISAPP_C.vb, Class : RISAPP.APP_C.CommFn" + vbTab

        Public Sub New()
            MyBase.New()
        End Sub

        '< add yjlee 
        Public Shared Function FindAboRhInfo(ByVal rsRegNo As String) As String
            Dim sFn As String = "FindAboRh"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT abo || rh aborh FROM lr070m WHERE regno = :regno"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count < 1 Then
                    Return ""
                Else
                    Return dt.Rows(0).Item("aborh").ToString
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function FindInfectionInfoD(ByVal rsRegNo As String) As String
            Dim sFn As String = "FindInfectionInfoD"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT fn_ack_get_infection(:regno) FROM DUAL"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()

                dt = DbExecuteQuery(sSql, al)

                Dim sTmpInfection As String = ""

                For iCnt As Integer = 0 To dt.Rows.Count - 1
                    If sTmpInfection.Length > 0 Then sTmpInfection += "/"

                    sTmpInfection += dt.Rows(iCnt).Item(0).ToString().Trim()
                Next

                Return sTmpInfection

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function FindInfectionInfoP(ByVal rsRegNo As String) As String
            Dim sFn As String = "FindInfectionInfoP"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT itemnmp"
                sSql += "  FROM lr060m"
                sSql += " WHERE regno = :regno"
                sSql += "   AND deldt is null "
                sSql += "   AND delid is null "
                sSql += " ORDER BY regdt "

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()

                dt = DbExecuteQuery(sSql, al)

                Dim sTmpInfection As String = ""

                For iCnt As Integer = 0 To dt.Rows.Count - 1
                    If sTmpInfection.Length > 0 Then sTmpInfection += "/"

                    sTmpInfection += dt.Rows(iCnt).Item("itemnmp").ToString().Trim()
                Next

                Return sTmpInfection

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try
        End Function

        '-- 채혈 정보
        Public Shared Function fnGet_CollectInfo(ByVal rsBcNo As String, ByVal rbTakeYn As Boolean) As DataTable
            Dim sFn As String = "Public fnGet_CollectInfo(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.spcflg,"
                'sSql += "       fn_ack_get_test_name_list(j0.bcno) testnms,"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "          FROM rj011m a, rf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) testnms,"
                sSql += "       CASE WHEN (SELECT MAX(NVL(rstflg, '0')) rstflg FROM rr010m WHERE bcno = j.bcno) > '0' OR"
                sSql += "                 (SELECT MAX(NVL(rstflg, '0')) rstflg FROM lm010m WHERE bcno = j.bcno) > '0' THEN '1' ELSE '0'"
                sSql += "       END rstflg,"
                'sSql += "       fn_ack_get_bcno_fkocs(j0.bcno) bcno_fkocs,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(a, ',' || a.bcno)).extract('//text()'), 2)"
                sSql += "          FROM rj011m a"
                sSql += "         WHERE a.fkocs  IN (SELECT fkocs FROM rj011m WHERE bcno = j.bcno AND fkocs <> '0') "
                sSql += "           AND a.spcflg IN ('1', '2', '3', '4')"
                sSql += "       ) bcno_fkocs,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo"
                sSql += "  FROM rj010m j, rj011m j1"
                sSql += " WHERE j.bcno = :bcno"
                sSql += "   AND j.bcno = j1.bcno"

                If rbTakeYn Then
                    sSql += "   AND j1.spcflg IN ('1', '2', '4')"
                Else
                    sSql += "   AND j1.spcflg IN ('1', '2')"
                End If
                sSql += " ORDER BY bcno"

                alParm.Clear()
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 환자특이사항 조회
        Public Shared Function fnGet_Comment_pat(ByVal rsIoGbn As String, ByVal rsRegNo As String) As String
            Dim sFn As String = "Public fnGet_Comment_pat(String) As DataTable"

            Try
                Dim sqlDoc As String = ""
                Dim alParm As New ArrayList

                sqlDoc += "SELECT DISTINCT remark  FROM lj040m"
                sqlDoc += " WHERE regno = :regno"

                alParm.Clear()
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sqlDoc, alParm)

                If dt.Rows.Count < 1 Then Return ""

                Dim sCmt As String = ""
                For ix As Integer = 0 To dt.Rows.Count - 1
                    If ix > 0 Then sCmt += vbCrLf
                    sCmt += dt.Rows(ix).Item("remark").ToString
                Next

                Return sCmt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

    '-- 환자특이사항 등록
    Public Class SpCmtReg
        Private Const msFile As String = "File : CGLISAPP_C.vb, Class : LISAPP.APP_C.SpCmtReg" + vbTab

        Public Function Reg_SpecalComment(ByVal rsRegNo As String, ByVal rsSpComment As String, ByVal rsIOGBN As String, _
                                          ByVal riCmtGbn As Integer, ByVal rsUsrId As String, _
                                          Optional ByVal rbDel As Boolean = False) As Integer
            Dim sFn As String = "Reg_SpecalComment"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction

            Dim dbCmd As New OracleCommand

            Try

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim sSql As String = ""
                Dim iRow As Integer = 0

                With dbCmd
                    .Connection = dbCn

                    If dbTran IsNot Nothing Then
                        If dbTran.Connection IsNot Nothing Then
                            .Transaction = dbTran
                        End If
                    End If

                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "SELECT regno  FROM lj040m"
                    sSql += " WHERE regno = :regno "

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo

                    Dim dt As New DataTable
                    Dim objDAdapter As New OracleDataAdapter(dbCmd)
                    objDAdapter.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        sSql = ""
                        sSql += "INSERT INTO lj040h("
                        sSql += "       moddt, modid, modip, regno ,iogbn, remark, regid, regdt"
                        sSql += "     ) "
                        sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*  FROM lj040m a"
                        sSql += " WHERE a.regno = :regno"

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("modid", OracleDbType.Varchar2).Value = rsUsrId
                        .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo

                        iRow = .ExecuteNonQuery()

                        sSql = ""
                        sSql += "DELETE FROM lj040m"
                        sSql += " WHERE regno = :regno"

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo

                        iRow = .ExecuteNonQuery()

                    End If

                    If Not rbDel Then
                        sSql = ""
                        sSql += "INSERT INTO lj040m("
                        sSql += "             regno,  iogbn,  remark,  regid, regdt)"
                        sSql += "    VALUES( :regno, :Iogbn, :remark, :regid, fn_ack_sysdate)"

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo
                        .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = rsIOGBN
                        .Parameters.Add("remark", OracleDbType.Varchar2).Value = rsSpComment
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId

                        iRow = .ExecuteNonQuery()
                    End If
                End With

                If iRow > 0 Then
                    dbTran.Commit()
                End If

                Return iRow

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

End Namespace
