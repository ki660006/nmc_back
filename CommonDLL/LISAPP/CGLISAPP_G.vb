'*****************************************************************************************/
'/*                                                                                      */
'/* Project Name : 관동대명지병원 Laboratory Information System(KMC_LIS)                 */
'/*                                                                                      */
'/*                                                                                      */
'/* FileName     : CGDA_V.vb                                                         */
'/* PartName     : 종합검증에 사용되는 공유 Data Access                        */
'/* Description  : 종합검증 공유 Data Access Class                                       */
'/* Design       :                                                                       */
'/* Coded        : 2006-08-01 freety                                                     */
'/* Modified     :                                                                       */
'/*                                                                                      */
'/*                                                                                      */
'/*                                                                                      */
'/****************************************************************************************/
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Namespace APP_G
    Public Class RegFn

        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.RegFn" + vbTab

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Public Sub New()
            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Public Function fnExe_Test_Add(ByVal rsBcNo As String) As Boolean
            Dim sFn As String = "Public Function fnExe_Test_Add( String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                sSql += "INSERT INTO lr010m("
                sSql += "            bcno, tclscd, testcd, spccd, regno, donflg, tkid, tkdt, wkymd, wkgrpcd, wkno,"
                sSql += "            wkdt, wkid, partcd, slipcd, editdt, editid, editip"
                sSql += "          ) "
                sSql += "SELECT bcno, tclscd, :testcd, spccd, regno, donflg, tkid, tkdt, wkymd, wkgrpcd, wkno,"
                sSql += "       wkdt, wkid, partcd, slipcd, fn_ack_sysdate(), :editid, :editip"
                sSql += "  FROM lr010m"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"
                sSql += "   AND rstflg = '3'"


                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()


                    .Parameters.Add(New OracleParameter("testcd", OracleDbType.Varchar2, PRG_CONST.TEST_GV_ADD.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.TEST_GV_ADD))
                    .Parameters.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("editip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))

                    .Parameters.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                    .Parameters.Add(New OracleParameter("testcd", OracleDbType.Varchar2, PRG_CONST.TEST_GV.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.TEST_GV))

                    iRet = .ExecuteNonQuery()
                End With

                If iRet < 1 Then
                    m_dbTran.Rollback()
                    Return False
                Else
                    m_dbTran.Commit()
                    Return True
                End If


            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

    End Class

    Public Class CommFn
        Private Const msFile As String = "File : CGLISAPP_G.vb, Class : LISAPP.APP_G.CommFn" + vbTab

        Public Shared Function fnGet_ENT_OUT_YN(ByVal rsRegNo As String) As String
            Dim sFn As String = "fnGet_Usr_Dept_info"

            Try
                Dim sSql As String = "SELECT FN_ACK_GET_ENT_OUT_YN(:regno) FROM DUAL"
                Dim al As New ArrayList

                DbCommand()

                al.Add(New OracleParameter("regno", rsRegNo))
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return "Y"
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_Usr_Dept_info(ByVal rsUsrId As String) As String
            Dim sFn As String = "fnGet_Usr_Dept_info"

            Try
                Dim sSql As String = "SELECT FN_ACK_GET_USR_DEPTINFO(:usrid) FROM DUAL"
                Dim al As New ArrayList

                DbCommand()

                al.Add(New OracleParameter("usrid", rsUsrId))
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return "/"
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_PrcpDrid_info(ByVal rsUsrId As String) As DataTable
            Dim sFn As String = "fnGet_Usr_Dept_info"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT DISTINCT usrid, usrnm"
                sSql += "  FROM lisif.lf090m "
                sSql += " WHERE usrid = :usrid"
                sSql += "   AND DRSPYN = '1' "

                Dim al As New ArrayList

                DbCommand()

                al.Add(New OracleParameter("usrid", rsUsrId))
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        Public Shared Function Get_SpcList_Test_User(ByVal rsSlipCd As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, _
                                                       ByVal rsOpt As String, ByVal rsTestCds As String, _
                                                         ByVal riUsrOpt As Integer, ByVal rsUsrId As String) As DataTable
            Dim sFn As String = ""

            Try
                Dim sTableNm As String = "lr010m"
                Dim sSql As String = ""

                'lm010m은 나중에 적용여부 결정
                If rsSlipCd.Substring(0, 1) = PRG_CONST.PART_MicroBio Then sTableNm = "lm010m"

                Dim al As New ArrayList

                'A : 전체, F : 완료, NF : 미완료, NR : 미검사
                If rsOpt = "NR" Then
                    sSql = ""
                    sSql += "SELECT DISTINCT"
                    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm,"
                    sSql += "       fn_ack_get_bcno_full(r.workno) workno,"
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, r.tknm, r.testcd, f.tnmd,"
                    sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg"
                    sSql += "  FROM ("
                    sSql += "  		 SELECT bcno, NVL(rstflg, '0') rstflg, tkdt, testcd, spccd,"
                    sSql += "               wkymd || NVL(wkgrpcd, '') || NVL(wkno, '') workno, fn_ack_get_usr_name(tkid) tknm"
                    sSql += " 		   FROM " + sTableNm
                    sSql += "         WHERE tkdt   >= :dates"
                    sSql += " 		    AND tkdt   <= :datee || '235959'"
                    sSql += "           AND testcd IN (" + rsTestCds + ")"
                    sSql += "           AND NVL(rstflg, '0') = '0'"

                    If riUsrOpt = 1 Then
                        sSql += "           AND NVL(tkid, '{null}') = :usrid"
                    Else
                        sSql += "           AND NVL(tkid, '{null}') <> :userid"
                    End If

                    sSql += " 		) r, lj010m j, lf060m f"
                    sSql += " WHERE j.bcno = r.bcno"
                    sSql += "   AND f.testcd = r.testcd"
                    sSql += "   AND f.spccd  = r.spccd"
                    sSql += "   AND f.usdt  <= r.tkdt"
                    sSql += "   AND f.uedt  >  r.tkdt"
                    sSql += "   AND j.spcflg = '4'"

                    al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                    al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                    al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrId))

                    If rsSlipCd.Length > 0 Then
                        sSql += "   AND f.partcd || f.slipcd = :slipcd"
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                    End If

                    sSql += " ORDER BY tkdt, workno, bcno"
                Else
                    sSql = ""
                    sSql += "SELECT DISTINCT"
                    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm,"
                    sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, fn_ack_get_usr_name(tkid) tknm, r.testcd, f.tnmd,"
                    sSql += "       CASE WHEN r.rstflg = '3' THEN 'Y' ELSE 'N' END rstflg"
                    sSql += "  FROM " + sTableNm + " r, lj010m j, lf060m f"
                    sSql += " WHERE r.tkdt   >= :dates"
                    sSql += "   AND r.tkdt   <= :datee || '235959'"
                    sSql += "   AND r.testcd IN (" + rsTestCds + ")"
                    sSql += "   AND j.bcno    = r.bcno"
                    sSql += "   AND f.testcd  = r.testcd"
                    sSql += "   AND f.spccd   = r.spccd"
                    sSql += "   AND f.usdt   <= r.tkdt"
                    sSql += "   AND f.uedt   >  r.tkdt"
                    sSql += "   AND j.spcflg  = '4'"

                    If riUsrOpt = 1 Then
                        sSql += "   AND NVL(r.tkid, '{null}') = :usrid"
                    Else
                        sSql += "   AND NVL(r.tkid, '{null}') <> :usrid"
                    End If

                    al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                    al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                    al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrId))

                    Select Case rsOpt.Substring(0, 1)
                        Case "N"
                            sSql += "   AND (r.rstflg <> '1' OR NVL(r.rstflg, ' ') = ' ')"

                        Case "F"
                            sSql += "   AND r.rstflg = '3'"

                        Case Else

                    End Select

                    If rsSlipCd.Length > 0 Then
                        sSql += "   AND f.partcd || f.slipcd = :slipcd"
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                    End If

                    sSql += " ORDER BY tkdt, workno, bcno"

                End If

                DbCommand()

                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        Public Shared Function Get_CdList(ByVal r_dbCn As OracleConnection, ByVal rbAll As Boolean, ByVal rsCdSep As String, _
                                          ByVal rsField As String, ByVal rsValue As String, ByVal rsUsrID As String) As DataTable

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbDa As OracleDataAdapter
            Dim dbCmd As New OracleCommand

            Dim dt As New DataTable

            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT cdseq, cdtitle, cdcont,"
            sSql += "       regid, fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
            sSql += "       NULL diffday, NULL moddt, NULL modid"
            sSql += "  FROM lf320m"
            sSql += " WHERE cdsep = :cdsep"

            If rsField.Length * rsValue.Length > 0 Then
                sSql += "   AND " + rsField + " LIKE :fdval || '%'"
            End If

            If rbAll Then
                sSql += " UNION ALL "
                sSql += "SELECT cdseq, cdtitle, cdcont,"
                sSql += "       regid, fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "       -1 diffday, fn_ack_date_str(moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, modid"
                sSql += "  FROM lf320h"
                sSql += " WHERE cdsep = :cdsep"

                If rsField.Length * rsValue.Length > 0 Then
                    sSql += " and " + rsField + " LIKE :fdval || '%'"
                End If
            End If

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            dbDa = New OracleDataAdapter(dbCmd)
            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add(New OracleParameter("cdsep", OracleDbType.Varchar2, rsCdSep.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdSep))

                If rsField.Length * rsValue.Length > 0 Then
                    .SelectCommand.Parameters.Add("fdval", OracleDbType.Varchar2).Value = rsValue
                End If

                If rbAll Then
                    .SelectCommand.Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep

                    If rsField.Length * rsValue.Length > 0 Then
                        .SelectCommand.Parameters.Add("fdval", OracleDbType.Varchar2).Value = rsValue
                    End If
                End If
            End With

            dt.Reset()
            dbDa.Fill(dt)

            If rsUsrID.Length > 0 Then
                dt = COMMON.CommFN.Fn.ChangeToDataTable(dt.Select("regid = '" + rsUsrID + "'", "cdseq, regdt, moddt"))
            Else
                dt = COMMON.CommFN.Fn.ChangeToDataTable(dt.Select("", "cdseq, regdt, moddt"))
            End If

            Return dt
        End Function

        Public Shared Function Set_CdList(ByVal r_dbCn As OracleConnection, ByVal rsCdSep As String, ByVal rsCdSeq As String, _
                                    ByVal rsCdTitle As String, ByVal rsCdCont As String, ByVal rsUsrID As String) As Boolean

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbCmd As New OracleCommand

            Dim dt As New DataTable

            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT cdseq, cdtitle, cdcont, regid"
            sSql += "  FROM lf320m"
            sSql += " WHERE cdsep = :cdsep"
            sSql += "   AND cdseq = :cdseq"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As New OracleDataAdapter

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep
                .Parameters.Add("cdseq", OracleDbType.Varchar2).Value = rsCdSeq
            End With
            dbDa = New OracleDataAdapter(dbCmd)
            dt.Reset()
            dbDa.Fill(dt)

            Dim sMsg As String = "코드 : " + rsCdSeq + ", 제목 : " + rsCdTitle + vbCrLf + vbCrLf

            Dim bIns As Boolean = False

            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("cdtitle").ToString() = rsCdTitle And dt.Rows(0).Item("cdcont").ToString() = rsCdCont Then
                    sMsg += "변경된 내용이 없습니다. 확인하여 주십시요!!"

                    MsgBox(sMsg, MsgBoxStyle.Information)

                    Return False
                Else
                    If dt.Rows(0).Item("regid").ToString() <> rsUsrID Then
                        sMsg += "다른 사용자가 등록한 항목입니다. 등록(수정) 하시겠습니까?"

                        If MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                            Return False
                        End If
                    Else
                        sMsg += "등록(수정)하시겠습니까?"

                        If MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                            Return False
                        End If
                    End If
                End If
            Else
                bIns = True

                sMsg += "등록(신규) 하시겠습니까?"

                If MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                    Return False
                End If
            End If

            Dim iRow As Integer = 0

            If bIns Then
                'insert lf320m
                Return Set_CdList_Insert(r_dbCn, rsCdSep, rsCdSeq, rsCdTitle, rsCdCont, rsUsrID)
            Else
                'backup lf320h --> update lf320m
                Return Set_CdList_Update(r_dbCn, rsCdSep, rsCdSeq, rsCdTitle, rsCdCont, rsUsrID)
            End If
        End Function

        Public Shared Function Set_CdList_Insert(ByVal r_dbCn As OracleConnection, ByVal rsCdSep As String, ByVal rsCdSeq As String, _
                                        ByVal rsCdTitle As String, ByVal rsCdCont As String, ByVal rsUsrID As String) As Boolean
            Dim sFn As String = "Set_CdList_Insert"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New OracleCommand

            Try

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dbcmd.Connection = dbCn
                dbCmd.Transaction = dbTran
                Dbcmd.CommandType = CommandType.Text

                Dim sSql As String = ""

                Dim iRow As Integer = 0

                'insert lf320m
                sSql = ""
                sSql += "INSERT INTO lf320m ( cdsep, cdseq, regdt,   regid, cdtitle, cdcont )"
                sSql += "    VALUES( :cdsep, :cdseq, fn_ack_sysdate, :regid, :cdtitle, :cdcont)"

                Dbcmd.CommandText = sSql

                With Dbcmd
                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("cdsep", OracleDbType.Varchar2, rsCdSep.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdSep))
                    .Parameters.Add(New OracleParameter("cdseq", OracleDbType.Varchar2, rsCdSeq.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdSeq))
                    .Parameters.Add(New OracleParameter("regid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))
                    .Parameters.Add(New OracleParameter("cdtitle", OracleDbType.Varchar2, rsCdTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdTitle))
                    .Parameters.Add(New OracleParameter("cdcont", OracleDbType.Varchar2, rsCdCont.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdCont))

                    iRow = .ExecuteNonQuery()
                End With

                If iRow < 1 Then
                    dbTran.Rollback()
                    Return False
                End If

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

        Public Shared Function Set_CdList_Update(ByVal r_dbCn As OracleConnection, ByVal rsCdSep As String, ByVal rsCdSeq As String, _
                                            ByVal rsCdTitle As String, ByVal rsCdCont As String, ByVal rsUsrID As String) As Boolean
            Dim sFn As String = "Set_CdList_Update"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New OracleCommand

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTran
                dbCmd.CommandType = CommandType.Text

                Dim sSql As String = ""

                Dim iRow As Integer = 0

                'insert lf320h
                sSql = ""
                sSql += "INSERT INTO lf320h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                sSql += "  FROM lf320m a"
                sSql += " WHERE cdsep = :cdsep"
                sSql += "   AND cdseq = :cdseq"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("cdsep", OracleDbType.Varchar2, rsCdSep.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdSep))
                    .Parameters.Add(New OracleParameter("cdseq", OracleDbType.Varchar2, rsCdSeq.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdSeq))
                End With


                iRow = dbCmd.ExecuteNonQuery()


                If iRow < 1 Then
                    dbTran.Rollback()

                    Return False
                End If

                'update lf320m
                sSql = ""
                sSql += "UPDATE lf320m SET"
                sSql += "       regdt   = fn_ack_sysdate,"
                sSql += "       regid   = :regid,"
                sSql += "       cdtitle = :cdtitle,"
                sSql += "       cdcont  = :cdcont"
                sSql += " WHERE cdsep   = :cdsep"
                sSql += "   AND cdseq   = :cdseq"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("regid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))
                    .Parameters.Add(New OracleParameter("cdtitle", OracleDbType.Varchar2, rsCdTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdTitle))
                    .Parameters.Add(New OracleParameter("cdcont", OracleDbType.Varchar2, rsCdCont.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdCont))

                    .Parameters.Add(New OracleParameter("cdsep", OracleDbType.Varchar2, rsCdSep.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdSep))
                    .Parameters.Add(New OracleParameter("cdseq", OracleDbType.Varchar2, rsCdSeq.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCdSeq))

                    iRow = .ExecuteNonQuery()
                End With

                If iRow < 1 Then
                    dbTran.Rollback()

                    Return False
                End If

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

        Public Shared Function Del_CdList(ByVal r_dbCn As OracleConnection, ByVal rsCdSep As String, ByVal rsCdSeq As String, ByVal rsUsrID As String) As Boolean

            Dim sFn As String = "Del_CdList"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New OracleCommand

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim iRow As Integer = 0

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTran
                dbCmd.CommandType = CommandType.Text

                Dim sSql As String = ""

                sSql = ""
                sSql += "INSERT INTO lf320h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip , cdsep, cdseq, cdtitle, cdcont, regdt, regid, :regip "
                sSql += "  FROM lf320m"
                sSql += " WHERE cdsep = :cdsep"
                sSql += "   AND cdseq = :cdseq"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = rsUsrID
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("regip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep
                    .Parameters.Add("cdseq", OracleDbType.Varchar2).Value = rsCdSeq


                    iRow = .ExecuteNonQuery()
                End With

                If iRow < 1 Then
                    dbTran.Rollback()

                    Return False
                End If

                sSql = ""
                sSql += "DELETE lf320m"
                sSql += " WHERE cdsep = :cdsep"
                sSql += "   AND cdseq = :cdseq"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep
                    .Parameters.Add("cdseq", OracleDbType.Varchar2).Value = rsCdSeq

                    iRow = .ExecuteNonQuery()
                End With

                If iRow < 1 Then
                    dbTran.Rollback()

                    Return False
                End If

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

        Public Shared Function fnGet_Board(ByVal rsTkDtS As String, ByVal rsTkDtE As String, ByVal riMode As Integer, ByVal rsUsrID As String) As DataTable
            Dim sFn As String = "Function fnGet_Board"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                If riMode > 2 Then riMode = 0
                If riMode = 0 Then rsUsrID = "{null}"

                sSql = ""
                sSql += "SELECT fn_ack_date_str(a.tkday, 'yyyy-mm-dd') tkday,"
                sSql += "       NVL(tkcnt, 0) tkcnt, NVL(fncnt, 0) fncnt,"
                sSql += "       RPAD(' ', NVL(tkcnt, 0), ' ') tksp,"
                sSql += "       RPAD(' ', NVL(fncnt, 0), ' ') fnsp"
                sSql += "  FROM ("
                'sSql += "        SELECT DISTINCT"
                sSql += "        SELECT "
                sSql += "               SUBSTR(orddt, 1, 8) tkday"
                sSql += "          FROM lj010m"
                sSql += "         WHERE orddt BETWEEN :dates AND :datee || '235959'"
                sSql += "         group by SUBSTR (orddt, 1, 8) "
                sSql += "       ) a LEFT OUTER JOIN"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

                sSql += "       ("
                sSql += "        SELECT fn_ack_date_str(tkdt, 'yyyymmdd') tkday, COUNT(*) tkcnt"
                sSql += "          FROM lr010m"
                sSql += "         WHERE tkdt   >= :dates"
                sSql += "           AND tkdt   <= :datee || '235959'"
                sSql += "           AND testcd  = '" + PRG_CONST.TEST_GV + "'"
                sSql += "           AND SUBSTR(bcno, 9, 2) = '" + PRG_CONST.BCCLS_GeneralVerify + "'"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

                If riMode = 1 Then
                    sSql += "           AND NVL(tkid, '{null}') = :usrid"
                Else
                    sSql += "           AND NVL(tkid, '{null}') <> :usrid"
                End If

                al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

                sSql += "          GROUP BY fn_ack_date_str(tkdt, 'yyyymmdd')"
                sSql += "       ) b ON (a.tkday = b.tkday) LEFT OUTER JOIN"
                sSql += "       ("
                sSql += "        SELECT fn_ack_date_str(tkdt, 'yyyymmdd') tkday, COUNT(*) fncnt"
                sSql += "          FROM lr010m"
                sSql += "         WHERE tkdt  >= :dates"
                sSql += "           AND tkdt  <= :datee || '235959'"
                sSql += "           AND testcd = '" + PRG_CONST.TEST_GV + "'"
                sSql += "           AND SUBSTR(bcno, 9, 2) = '" + PRG_CONST.BCCLS_GeneralVerify + "'"

                If riMode = 1 Then
                    sSql += "           AND NVL(tkid, '{null}') = :usrid"
                Else
                    sSql += "           AND NVL(tkid, '{null}') <> :usrid"
                End If

                sSql += "           AND rstflg = '3'"
                sSql += "         GROUP BY fn_ack_date_str(tkdt, 'yyyymmdd')"
                sSql += "       ) c ON (a.tkday = c.tkday)"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function Get_List_ToTk(ByVal rsEntDtB As String, ByVal rsEntDtE As String) As DataTable
            Dim sFn As String = "Function Get_List_ToTk(String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = "pkg_ack_gv.pkg_get_gv_list"

                al.Add(New OracleParameter("rs_entdt1", rsEntDtB))
                al.Add(New OracleParameter("rs_entdt2", rsEntDtE))

                DbCommand()
                Return DbExecuteQuery(sSql, al, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        Public Shared Function Get_List_ToTk_RegNo(ByVal rsRegNo As String, ByVal rsEntDay As String) As DataTable
            Dim sFn As String = "Function Get_List_ToTk_RegNo(String, String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = "pkg_ack_gv.pkg_get_totake_regno"

                DbCommand()

                Dim al As New ArrayList

                al.Add(New OracleParameter("rs_regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("rs_entdt", OracleDbType.Varchar2, rsEntDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsEntDay))

                Return DbExecuteQuery(sSql, al, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        Public Shared Function Get_List_ToTk_RegNo(ByVal rsRegNo As String, ByVal rsOrdDt As String, ByVal rsFkOcs As String) As DataTable
            Dim sFn As String = "Function Get_List_ToTk_RegNo(String, String, String) As DataTable"

            Try
                Dim sSql As String = "pkg_ack_gv.pkg_get_totake_fkocs"

                Dim al As New ArrayList

                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_orddt", rsOrdDt))

                If rsFkOcs.IndexOf("/") >= 0 Then
                    al.Add(New OracleParameter("rs_fkocs", rsFkOcs.Split("/"c)(3)))
                    al.Add(New OracleParameter("rs_ioflag", rsFkOcs.Split("/"c)(0)))
                Else
                    al.Add(New OracleParameter("rs_fkocs", rsFkOcs))
                    al.Add(New OracleParameter("rs_ioflag", ""))
                End If

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al, False)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function
    End Class

End Namespace
