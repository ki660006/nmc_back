Imports Oracle.DataAccess.Client
Imports DBORA.DbProvider
Imports COMMON.CommLogin.LOGIN

Public Class DA_V
    Private msFile As String = "File : CGDA_V.vb, Class : DA_V" + vbTab
    Private msCrLf As String = Convert.ToChar(13) + Convert.ToChar(10)

    Public Function Del_CdList(ByVal r_dbCn As OracleConnection, ByVal rsCdSep As String, ByVal rsCdSeq As String, ByVal rsUsrID As String) As Boolean
        Dim sFn As String = "Del_CdList"

        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()

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
            sSql += " insert into lf320h"
            sSql += " select sysdate, :modid, :modip, cdsep, cdseq, regdt, regid, cdtitle, cdcont"
            sSql += "   from lf320m"
            sSql += "  where cdsep = :cdsep"
            sSql += "    and cdseq = :cdseq"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("modid", OracleDbType.Varchar2).Value = rsUsrID
                .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                .Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep
                .Parameters.Add("cdseq", OracleDbType.Varchar2).Value = rsCdSeq

                iRow = .ExecuteNonQuery()
            End With

            If iRow < 1 Then
                dbTran.Rollback()

                Return False
            End If

            sSql = ""
            sSql += " delete from lf320m"
            sSql += "  where cdsep = :cdsep"
            sSql += "    and cdseq = :cdseq"

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Function Get_CdList(ByVal r_dbCn As OracleConnection, ByVal rbAll As Boolean, ByVal rsCdSep As String, _
                                    ByVal rsField As String, ByVal rsValue As String, ByVal rsUsrID As String) As DataTable
        Dim sFn As String = "Get_CdList"
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()

        Dim dbCmd As New OracleCommand
        Dim dbDa As New OracleDataAdapter

        Dim dt As New DataTable
        Dim sSql As String = ""

        Try
            sSql = ""
            sSql += " select cdseq, cdtitle, cdcont,"
            sSql += "        regid, to_char(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
            sSql += "        null diffday, null moddt, null modid"
            sSql += "   from lf320m"
            sSql += "  where cdsep = :cdsep"

            If rsField.Length * rsValue.Length > 0 Then
                sSql += " and " + rsField + " like :value || '%'"
            End If

            If rbAll Then
                sSql += "  union all"
                sSql += " select cdseq, cdtitle, cdcont,"
                sSql += "        regid, to_char(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "        moddt - sysdate diffday, to_char(moddt, 'yyyy-mm-dd hh24:mi:ss') moddt, modid modid"
                sSql += "   from lf320h"
                sSql += "  where cdsep = :cdsep"

                If rsField.Length * rsValue.Length > 0 Then
                    sSql += " and " + rsField + " like :value || '%'"
                End If
            End If

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep

                If rsField.Length * rsValue.Length > 0 Then
                    .SelectCommand.Parameters.Add("value", OracleDbType.Varchar2).Value = rsValue
                End If

                If rbAll Then
                    .SelectCommand.Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep

                    If rsField.Length * rsValue.Length > 0 Then
                        .SelectCommand.Parameters.Add("value", OracleDbType.Varchar2).Value = rsValue
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
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        Finally
            dbDa.Dispose() : dbDa = Nothing
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Function Get_EntInfo(ByVal r_dbCn As OracleConnection, ByVal rsBcNo As String) As DataTable
        Dim sFn As String = "Get_EntInfo"
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()

        Dim dbCmd As New OracleCommand

        Dim sSql As String = ""
        Try
            sSql = ""
            sSql += "SELECT j.regno,"
            sSql += "       fn_ack_date_str(j.entdt, 'yyyy-mm-dd') entday,"
            sSql += "       fn_ack_date_str(MIN(j1.rstdt), 'yyyy-mm-dd') rstdays,"
            sSql += "       fn_ack_date_str(MAX(j1.rstdt), 'yyyy-mm-dd') rstdaye"
            sSql += "  FROM lj010m j, lj011m j1"
            sSql += " WHERE j.regno   = (SELECT regno FROM lj010m WHERE bcno = :bcno)"
            sSql += "   AND j.bcno    = j1.bcno"
            sSql += "   AND j.entdt   = (SELECT entdt FROM lj010m WHERE bcno = :bcno)"
            sSql += "   AND j.spcflg  = '4'"
            sSql += "   AND j1.rstflg = '3'"
            sSql += " GROUP BY j.regno, j.entdt"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            Dim dt As New DataTable

            dt.Reset()
            dbDa.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Function Set_CdList(ByVal r_dbCn As OracleConnection, ByVal rsCdSep As String, ByVal rsCdSeq As String, _
                                ByVal rsCdTitle As String, ByVal rsCdCont As String, ByVal rsUsrID As String) As Boolean
        Dim sFn As String = "Set_CdList"

        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()

        Dim dbCmd As New OracleCommand

        Try
            Dim dt As New DataTable

            Dim sSql As String = ""

            sSql = ""
            sSql += " select cdseq, cdtitle, cdcont, regid"
            sSql += "   from lf320m"
            sSql += "  where cdsep = :cdsep"
            sSql += "    and cdseq = :cdseq"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep
                .SelectCommand.Parameters.Add("cdseq", OracleDbType.Varchar2).Value = rsCdSeq
            End With

            dt.Reset()
            dbDa.Fill(dt)

            Dim sMsg As String = "코드 : " + rsCdSeq + ", 제목 : " + rsCdTitle + msCrLf + msCrLf

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
                Return Set_CdList_Insert(dbCn, rsCdSep, rsCdSeq, rsCdTitle, rsCdCont, rsUsrID)
            Else
                'backup lf320h --> update lf320m
                Return Set_CdList_Update(dbCn, rsCdSep, rsCdSeq, rsCdTitle, rsCdCont, rsUsrID)
            End If
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If

        End Try

    End Function


    Private Function Set_CdList_Insert(ByVal r_dbCn As OracleConnection, ByVal rsCdSep As String, ByVal rsCdSeq As String, _
                                        ByVal rsCdTitle As String, ByVal rsCdCont As String, ByVal rsUsrID As String) As Boolean
        Dim sFn As String = "Set_CdList_Insert"
        Dim dbTran As OracleTransaction = r_dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            dbCmd.Connection = r_dbCn
            dbCmd.Transaction = dbTran
            dbCmd.CommandType = CommandType.Text

            Dim sSql As String = ""

            Dim iRow As Integer = 0

            'insert lf320m
            sSql = ""
            sSql += " insert into lf320m (  cdsep,  cdseq,  regdt,           regid,  cdtitle,  cdcont )"
            sSql += "             values ( :cdsep, :cdseq,  fn_ack_sysdate, :regid, :cdtitle, :cdcont )"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep
                .Parameters.Add("cdseq", OracleDbType.Varchar2).Value = rsCdSeq
                .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrID
                .Parameters.Add("cdtitle", OracleDbType.Varchar2).Value = rsCdTitle
                .Parameters.Add("cdcont", OracleDbType.Varchar2).Value = rsCdCont

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Private Function Set_CdList_Update(ByVal r_dbCn As OracleConnection, ByVal rsCdSep As String, ByVal rsCdSeq As String, _
                                        ByVal rsCdTitle As String, ByVal rsCdCont As String, ByVal rsUsrID As String) As Boolean

        Dim sFn As String = "Set_CdList_Update"

        Dim dbTran As OracleTransaction = r_dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            dbCmd.Connection = r_dbCn
            dbCmd.Transaction = dbTran
            dbCmd.CommandType = CommandType.Text

            Dim sSql As String = ""

            Dim iRow As Integer = 0

            'insert lf320h
            sSql = ""
            sSql += " insert into lf320h"
            sSql += " select sysdate, :modid, :modip, cdsep, cdseq, regdt, regid, cdtitle, cdcont"
            sSql += "   from lf320m"
            sSql += "  where cdsep = :cdsep"
            sSql += "    and cdseq = :cdseq"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("modid", OracleDbType.Varchar2).Value = rsUsrID
                .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                .Parameters.Add("cdsep", OracleDbType.Varchar2).Value = rsCdSep
                .Parameters.Add("cdseq", OracleDbType.Varchar2).Value = rsCdSeq

                iRow = .ExecuteNonQuery()
            End With

            If iRow < 1 Then
                dbTran.Rollback()

                Return False
            End If

            'update lf320m
            sSql = ""
            sSql += " update lf320m set"
            sSql += "        regdt   = fn_ack_sysdate"
            sSql += "      , regid   = :regid"
            sSql += "      , cdtitle = :cdtitle"
            sSql += "      , cdcont  = :cdcont"
            sSql += "  where cdsep   = :cdseq"
            sSql += "    and cdseq   = :cdseq"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrID
                .Parameters.Add("cdtitle", OracleDbType.Varchar2).Value = rsCdTitle
                .Parameters.Add("cdcont", OracleDbType.Varchar2).Value = rsCdCont

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
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function
End Class
