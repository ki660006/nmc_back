Imports Oracle.DataAccess.Client
Imports DBORA.DbProvider

Public Class APP_DEP
    Private Const msFile As String = "File : CGLISAPP_DEP.vb, Class : LISAPP.APP_DEP" + vbTab

    Public Shared Function Find_DepFile_NewVersion(ByVal rsPrgId As String, ByVal rsFileNm As String, ByVal rsFileVer As String) As String
        Dim sFn As String = "Public Shared Function Find_DepFile_NewVersion(String, String, String) As String"

        Dim dbCn As New OracleConnection
        Dim dbCmd As New OracleCommand
        Dim dbParam As OracleParameter

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT clsval FROM lf000m WHERE clsgbn = '01' AND clscd = '007'"

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql)

            If dt.Rows.Count > 0 Then COMMON.CommFN.MdiMain.Db_ConnectTimeOut = dt.Rows(0).Item("clsval").ToString

            dbCn = GetDbConnection()

            sSql = ""
            sSql += "SELECT 0 seq, fn_ack_date_str(depdt, 'yyyy-mm-dd hh24:mi:ss') depdt, filenm, filever, filepath, filereg, rowdt, rowuid"
            sSql += "   FROM ldep00"
            sSql += "  WHERE depdt < fn_ack_sysdate"
            sSql += "    AND prgid = :prgid"
            sSql += "    AND UPPER(filenm) = UPPER(:filenm)"
            sSql += "    AND filever > :filever"
            sSql += "  ORDER BY depdt DESC"

            If dbCmd Is Nothing Then dbCmd = New OracleCommand

            With dbCmd
                .Connection = dbCn
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()

                '< prgid
                dbParam = New OracleParameter
                With dbParam
                    .ParameterName = "prgid" : .DbType = DbType.String : .Value = rsPrgId
                End With
                .Parameters.Add(dbParam)
                dbParam = Nothing
                '>

                '< filenm
                dbParam = New OracleParameter
                With dbParam
                    .ParameterName = "filenm" : .DbType = DbType.String : .Value = rsFileNm
                End With
                .Parameters.Add(dbParam)
                dbParam = Nothing
                '>

                '< filever
                dbParam = New OracleParameter
                With dbParam
                    .ParameterName = "filever" : .DbType = DbType.String : .Value = rsFileVer
                End With
                .Parameters.Add(dbParam)
                dbParam = Nothing
                '>
            End With

            Dim lisdbDa As New OracleDataAdapter(dbCmd)

            dt = New DataTable

            dt.Reset()
            lisdbDa.Fill(dt)

            If dt.Rows.Count < 1 Then Return Nothing

            Return dt.Rows(0).Item("depdt").ToString()

        Catch ex As Exception
            'Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        Finally

            dbCmd.Dispose() : dbCmd = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing
        End Try
    End Function

    Public Shared Function Find_DepFile_NewVersion_DepTest(ByVal rsPrgId As String, ByVal rsFileNm As String, ByVal rsFileVer As String) As String
        Dim sFn As String = "Public Shared Function Find_DepFile_NewVersion_DepTest(String, String, String) As String"

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbCmd As New OracleCommand
        Dim dbParam As OracleParameter

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += " SELECT 0 SEQ, depdt, filenm, filever, filepath, filereg, rowdt, rowuid"
            sSql += "   FROM ldep00"
            sSql += " WHERE prgid = :prgid"
            sSql += "    AND upper(filenm) = upper(:filenm)"
            sSql += "    AND filever > :filever"
            sSql += "  ORDER BY depdt DESC"

            If dbCmd Is Nothing Then dbCmd = New OracleCommand

            With dbCmd
                .Connection = dbCn
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()

                '< prgid
                dbParam = New OracleParameter
                With dbParam
                    .ParameterName = "prgid" : .DbType = DbType.String : .Value = rsPrgId
                End With
                .Parameters.Add(dbParam)
                dbParam = Nothing
                '>

                '< filenm
                dbParam = New OracleParameter
                With dbParam
                    .ParameterName = "filenm" : .DbType = DbType.String : .Value = rsFileNm
                End With
                .Parameters.Add(dbParam)
                dbParam = Nothing
                '>

                '< filever
                dbParam = New OracleParameter
                With dbParam
                    .ParameterName = "filever" : .DbType = DbType.String : .Value = rsFileVer
                End With
                .Parameters.Add(dbParam)
                dbParam = Nothing
                '>
            End With

            Dim lisdbDa As New OracleDataAdapter(dbCmd)

            Dim dt As New DataTable

            dt.Reset()
            lisdbDa.Fill(dt)

            If dt.Rows.Count < 1 Then Return Nothing

            Return dt.Rows(0).Item("depdt").ToString()

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message, MsgBoxStyle.Exclamation)
            Return Nothing

        Finally

            dbCmd.Dispose() : dbCmd = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

        End Try
    End Function

End Class
