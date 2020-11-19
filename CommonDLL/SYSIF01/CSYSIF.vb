'// 2006-04-05 by freety : SYSTEM간 상호 데이터교환을 위한 IF 클래스

Imports System.Net
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Public Class SYSIF
    Private Const msFile As String = "File : CSYSIF.vb, Class : SYSIF" & vbTab
    Private Const msErrMsg As String = "§ERR§"

    Private m_ts_ord As TimeSpan = New TimeSpan(0, 10, 0)
    Private mdt_sys As Date = Now.Date
    Private mdt_ord As Date = Now.Date
    Private Shared m_dbCn As OracleConnection
    Private m_dbTran As OracleTransaction = Nothing

    Public UserID As String = ""
    Public UseLIS As Boolean = False
    Public LOCALIP As String = ""

    Public DbProvider As String = ""
    Public DbDatasource As String = ""
    Public DbUsername As String = ""
    Public DbPassword As String = ""
    Public DbCategory As String = ""

    Public OrdInfo As OrderInfo
    Public DiagInfos As ArrayList

    Public Sub New()
        'UserID = "S01"
        'DbProvider = "OraOLEDB.Oracle"
        'DbDatasource = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 14.35.234.249)(PORT = 1521)) ) (CONNECT_DATA =(SERVICE_NAME = NMC)))"
        'DbUsername = "oras1"
        'DbPassword = "oras1"
        UserID = "S01"
        DbProvider = "OraOLEDB.Oracle"
        DbDatasource = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.95.21.143)(PORT = 1521))(ADDRESS = (PROTOCOL = TCP)(HOST = 10.95.21.144)(PORT = 1521))(LOAD_BALANCE = NO) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = EMRDB) (FAILOVER_MODE = (TYPE = SELECT)(METHOD = BASIC)(RETRIES = 180)(DELAY = 5))))"
        DbUsername = "lisif"
        DbPassword = "lisif"
    End Sub

    Public Sub New(ByVal rsUserID As String, ByVal rsLocakIP As String)
        UseLIS = True
        UserID = rsUserID
        LOCALIP = rsLocakIP

        If rsLocakIP = "" Then
            Dim objIpEntry As IPHostEntry = Dns.GetHostByName(Dns.GetHostName())
            Dim objIpAdrees As IPAddress() = objIpEntry.AddressList

            LOCALIP = objIpAdrees(0).ToString()
        End If

    End Sub

    Public Sub New(ByVal rbUseLIS As Boolean, ByVal rsUserID As String, ByVal rsLocakIP As String)

        UseLIS = rbUseLIS
        UserID = rsUserID

        If rsLocakIP = "" Then
            Dim objIpEntry As IPHostEntry = Dns.GetHostByName(Dns.GetHostName())
            Dim objIpAdrees As IPAddress() = objIpEntry.AddressList

            LOCALIP = objIpAdrees(0).ToString()
        End If

        If UseLIS = False Then
            DbProvider = "OraOLEDB.Oracle"
            DbDatasource = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 14.35.234.249)(PORT = 1521)) ) (CONNECT_DATA =(SERVICE_NAME = NMC)))"
            DbUsername = "oras1"
            DbPassword = "oras1"
        End If
    End Sub

    Public Function fnExe_OrderOnly(ByRef rsErrMsg As String) As ArrayList
        Dim sFn As String = "Function exeProcOrderOnly"

        rsErrMsg = ""

        Try
            Dim sErrMsg As String = ""

            If fnGet_OleDbConnection() = False Then
                rsErrMsg = "DB 연결에 실패하였습니다."
                Return Nothing
            End If

            mdt_ord = fnGetServerDateTime(OrdInfo.OrderDay)
            mdt_sys = fnGetServerDateTime()

            sbInitial_Order_Collect_Take()

            m_dbTran = m_dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim al_OcsKey As ArrayList = fnExe_Order_lis(sErrMsg, True)

            If sErrMsg.Length > 0 Then
                m_dbTran.Rollback()

                rsErrMsg = sErrMsg

                Return Nothing
            End If

            Dim iOK As Integer = 0

            For i As Integer = 1 To al_OcsKey.Count
                If al_OcsKey(i - 1).ToString().StartsWith(msErrMsg) = False Then
                    iOK += 1
                Else
                    If sErrMsg.Length > 0 Then sErrMsg += vbCrLf

                    sErrMsg += al_OcsKey(i - 1).ToString()
                End If
            Next

            If iOK = 0 Then
                m_dbTran.Rollback()

                rsErrMsg = sErrMsg

                Return Nothing
            End If

            m_dbTran.Commit()

            Return al_OcsKey

        Catch ex As Exception
            m_dbTran.Rollback()

            Fn.log(msFile + sFn, Err)
            rsErrMsg = sFn + " - " + ex.Message
            Return Nothing
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function fnExe_CollectToTake(ByRef rsErrMsg As String) As ArrayList
        Dim sFn As String = "Function fnExe_CollectToTake"

        Try
            Dim sErrMsg As String = ""

            If fnGet_OleDbConnection() = False Then
                rsErrMsg = "DB 연결에 실패하였습니다."
                Return Nothing
            End If

            mdt_ord = fnGetServerDateTime(OrdInfo.OrderDay)
            mdt_sys = fnGetServerDateTime()

            sbInitial_Order_Collect_Take()

            m_dbTran = m_dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim al_OcsKey As New ArrayList

            'If OrdInfo.OwnGbn = "L" Then
            '    al_OcsKey = fnExe_Order_lis(sErrMsg, True)
            'Else
            '    al_OcsKey = fnExe_Order_ocs(sErrMsg, True)
            'End If

            al_OcsKey = fnExe_Order_lis(sErrMsg, True)

            If sErrMsg.Length > 0 Then
                m_dbTran.Rollback()

                rsErrMsg = sErrMsg

                Return Nothing
            End If

            Dim iOK As Integer = 0

            For i As Integer = 1 To al_OcsKey.Count
                If al_OcsKey(i - 1).ToString().StartsWith(msErrMsg) = False Then
                    iOK += 1
                Else
                    If sErrMsg.Length > 0 Then sErrMsg += vbCrLf

                    sErrMsg += al_OcsKey(i - 1).ToString()
                End If
            Next

            If iOK = 0 Then
                m_dbTran.Rollback()

                rsErrMsg = sErrMsg

                Return Nothing
            End If

            sErrMsg = ""

            Dim al_BcNo As ArrayList = fnExe_Collect(sErrMsg)

            If al_BcNo Is Nothing Then
                m_dbTran.Rollback()

                rsErrMsg = sErrMsg

                Return Nothing
            Else
                If al_BcNo.Count < 1 Then
                    m_dbTran.Rollback()

                    rsErrMsg = sErrMsg

                    Return Nothing

                End If
            End If

            m_dbTran.Commit()

            rsErrMsg = sErrMsg
            Return al_BcNo

        Catch ex As Exception
            m_dbTran.Rollback()

            Fn.log(msFile + sFn, Err)

            rsErrMsg = sFn + " - " + ex.Message

            Return Nothing
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function fnExe_CollectToTake(ByVal rsRegNo As String, ByVal rsOrdDt As String, ByVal rsFkOcs As String) As String
        Dim sFn As String = "Function fnExe_CollectToTake"

        Dim strErrMsg As String = ""

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            If fnGet_OleDbConnection() = False Then
                Return "DB 연결에 실패하였습니다."
            End If

            Dim dbCmd As New OracleCommand
            Dim strErrVal As String = ""

            With dbCmd
                .Connection = m_dbCn
                .Transaction = m_dbTran
                .CommandType = CommandType.StoredProcedure
                .CommandText = "pro_ack_exe_collect_take"

                .Parameters.Clear()
                .Parameters.Add("rs_regno", OracleDbType.Varchar2).Value = rsRegNo
                .Parameters.Add("rs_orddt", OracleDbType.Varchar2).Value = rsOrdDt
                .Parameters.Add("rs_fkocs", OracleDbType.Varchar2).Value = rsFkOcs
                .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = UserID
                .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                .Parameters("rs_retval").Value = strErrVal

                .ExecuteNonQuery()

                strErrVal = .Parameters(5).Value.ToString
            End With

            Return strErrVal

        Catch ex As Exception
            m_dbTran.Rollback()
            Fn.log(msFile + sFn, Err)

            Return sFn + " - " + ex.Message
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Function fnExe_TakeBcNo(ByVal rsBcNo As String, ByRef rsWkNo As String, ByRef rsErrMsg As String) As String
        Dim sFn As String = "Function fnExe_TakeBcNo"

        Dim sSEP As String = Convert.ToChar(10)

        '초기화
        rsWkNo = ""
        rsErrMsg = ""

        If fnGet_OleDbConnection() = False Then
            rsErrMsg = sSEP + sSEP + "DB 연결에 실패하였습니다."
            Return "-1"
        End If

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim bErr As Boolean = False
            Dim iErrNo As Integer = 0
            Dim sErrMsg As String = ""

            If fnGetConvBcNo(rsBcNo) = False Then
                rsErrMsg = sSEP + sSEP + "검체번호가 올바르지 않습니다."

                Return "-1"
            End If

            rsWkNo = fnTakeBcNo(rsBcNo, UserID, bErr, iErrNo, sErrMsg)

            If bErr Then
                Select Case iErrNo
                    Case 1
                        '접수된 검체번호 오류
                        rsErrMsg = sSEP + fnGetPreWorkNo(rsBcNo) + sSEP + "기접수한 검체입니다." + sSEP

                    Case 2
                        '검사항목 조회 오류
                        rsErrMsg = sSEP + sSEP + "해당 검체번호의 검사항목 오류로 실패하였습니다." + sSEP

                    Case Else
                        '기타 오류
                        rsErrMsg = sSEP + sSEP + sErrMsg

                End Select

                Return "-1"
            Else
                If rsWkNo.Length = 0 Then
                    rsWkNo = "0"
                    rsErrMsg = "자동접수는 되었으나 작업번호는 생성되지 않았습니다."
                Else
                    rsWkNo = Fn.WKNO_View(rsWkNo)
                End If

                Return "1"
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            rsErrMsg = sSEP + sSEP + ex.Message

            Return "-1"
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function fnGet_Test_Collect(ByVal rsSystemSeq As String, ByVal rsCollDayS As String, ByVal rsCollDayE As String, ByVal rsTclsCd As String, _
                                          ByRef rsRegNo As String, ByRef rsPatNm As String, _
                                               ByRef rsSex As String, ByRef rsAge As String, _
                                                 ByRef rsBcNo As String, ByRef rsWkDt As String, _
                                                   ByRef rsDept As String, ByRef rsWard As String, _
                                                     ByRef rsInfect As String, ByRef rsToday As String, _
                                                       ByRef rsEmer As String, ByRef rsExamCd As String, _
                                                         ByRef rsSlip As String, ByRef rsReRun As String, _
                                                          ByRef rsSpcCd As String) As String

        Dim sFn As String = "Function fnGet_Test_Collect"

        'rsSystemSeq는 개별적으로 n개(n = 1 ~ 9)의 System에 채혈정보를 보내야하는 경우에 사용
        If IsNumeric(rsSystemSeq) = False Then
            Return "-1"
        End If

        If Convert.ToInt32(rsSystemSeq) < 0 Or Convert.ToInt32(rsSystemSeq) > 9 Then
            Return "-1"
        End If

        Dim sTblNm As String = "lnc" + rsSystemSeq + "0m"

        If fnGet_OleDbConnection() = False Then
            Return "-1"
        End If

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT j.regno, j.patnm, j.sex, j.age, fn_ack_get_bcno_prt(j.bcno) bcno, '' wkdt,"
            sSql += "       j.deptcd dept, j.wardno || j.roomno ward,"
            sSql += " 	     '' infect, '' today, j.statgbn emer, f.partcd || f.slipcd slip,"
            sSql += "       f6.tclscd tclscd, f6.tnmd, f6.testcd examcd,"
            sSql += "       ci.rerun, j.spccd"
            sSql += "  FROM lj010m j, lj011m j1,"
            sSql += "       (SELECT bcno, '' rerun"
            sSql += "          FROM lj010m"
            sSql += "         WHERE bcno  >= :bcnos"
            sSql += "           AND bcno  <= :bcnoe || 'ZZZZZZZ'"
            sSql += " 		    AND NVL(spcflg, '0') > '2'"
            sSql += "           AND bcno NOT IN (SELECT bcno"
            sSql += "                              FROM " + sTblNm + ""
            sSql += "                             WHERE bcno >= :bcnos"
            sSql += "                               AND bcno <= :bcnoe || 'ZZZZZZZ'"
            sSql += "                           )"
            sSql += "         UNION "
            sSql += "        SELECT bcno, rerunflg rerun"
            sSql += "          FROM lr010m "
            sSql += "         WHERE bcno >= :bcnos"
            sSql += "           AND bcno <= :bcnoe || 'ZZZZZZZ'"
            sSql += "           AND NVL(orgrst,   ' ')  = ' '"
            sSql += "           AND NVL(rerunflg, ' ') <> ' '"
            sSql += " 	    ) ci,"
            sSql += "       (SELECT testcd tclscd, spccd tspccd, testcd, spccd, usdt, uedt"
            sSql += "          FROM lf060m f6"
            sSql += "         WHERE testcd IN (" + rsTclsCd + ")"
            sSql += "         UNION "
            sSql += "        SELECT b.tclscd, b.tspccd, a.testcd, a.spccd, a.usdt, a.uedt"
            sSql += "          FROM lf060m a, lf062m b"
            sSql += "         WHERE a.testcd LIKE b.testcd || '%'"
            sSql += "           AND a.spccd  = b.spccd"
            sSql += "           AND a.testcd IN (" + rsTclsCd + ")"
            sSql += "       ) f6"
            sSql += " WHERE j.bcno     = ci.bcno"
            sSql += "   AND j.bcno     = j1.bcno"
            sSql += "   AND j1.tclscd  = f6.tclscd"
            sSql += "   AND j1.spccd   = f6.tspccd"
            sSql += "   AND j1.colldt >= f6.usdt"
            sSql += "   AND j1.colldt <  f6.uedt"
            sSql += "   AND NVL(j.spcflg,  '0') >  '2'"
            sSql += "   AND NVL(j1.spcflg, '0') >  '2'"
            sSql += "   AND NVL(j1.collid, ' ') <> ' '"

            Dim dbCmd As OracleCommand = New OracleCommand
            Dim dbDa As OracleDataAdapter = New OracleDataAdapter(dbCmd)
            Dim dt As DataTable = New DataTable

            dbCmd.Connection = m_dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcnos", OracleDbType.Varchar2).Value = rsCollDayS
                .SelectCommand.Parameters.Add("bcnoe", OracleDbType.Varchar2).Value = rsCollDayE
                .SelectCommand.Parameters.Add("bcnos", OracleDbType.Varchar2).Value = rsCollDayS
                .SelectCommand.Parameters.Add("bcnoe", OracleDbType.Varchar2).Value = rsCollDayE
                .SelectCommand.Parameters.Add("bcnos", OracleDbType.Varchar2).Value = rsCollDayS
                .SelectCommand.Parameters.Add("bcnoe", OracleDbType.Varchar2).Value = rsCollDayE
            End With

            dt.Reset()
            dbDa.Fill(dt)

            Dim iCnt As Integer = dt.Rows.Count

            Dim sSEP As String = Convert.ToChar(1)

            If iCnt > 0 Then
                '초기화
                rsRegNo = "" : rsPatNm = ""
                rsSex = "" : rsAge = ""
                rsBcNo = "" : rsWkDt = ""
                rsDept = "" : rsWard = ""
                rsInfect = "" : rsToday = ""
                rsEmer = "" : rsExamCd = "" : rsSlip = "" : rsReRun = "" : rsSpcCd = ""

                For i As Integer = 1 To iCnt
                    If i > 1 Then
                        rsRegNo += sSEP
                        rsPatNm += sSEP
                        rsSex += sSEP
                        rsAge += sSEP
                        rsBcNo += sSEP
                        rsWkDt += sSEP
                        rsDept += sSEP
                        rsWard += sSEP
                        rsInfect += sSEP
                        rsToday += sSEP
                        rsEmer += sSEP
                        rsExamCd += sSEP
                        rsSlip += sSEP
                        rsReRun += sSEP
                        rsSpcCd += sSEP
                    End If

                    rsRegNo += dt.Rows(i - 1).Item("regno").ToString().Trim
                    rsPatNm += dt.Rows(i - 1).Item("patnm").ToString().Trim
                    rsSex += dt.Rows(i - 1).Item("sex").ToString().Trim
                    rsAge += dt.Rows(i - 1).Item("age").ToString().Trim
                    rsBcNo += dt.Rows(i - 1).Item("bcno").ToString().Trim
                    rsWkDt += dt.Rows(i - 1).Item("wkdt").ToString().Trim
                    rsDept += dt.Rows(i - 1).Item("dept").ToString().Trim
                    rsWard += dt.Rows(i - 1).Item("ward").ToString().Trim
                    rsInfect += dt.Rows(i - 1).Item("infect").ToString().Trim
                    rsToday += dt.Rows(i - 1).Item("today").ToString().Trim
                    rsEmer += dt.Rows(i - 1).Item("emer").ToString().Trim
                    rsExamCd += dt.Rows(i - 1).Item("examcd").ToString().Trim
                    rsSlip += dt.Rows(i - 1).Item("slip").ToString().Trim
                    rsReRun += dt.Rows(i - 1).Item("rerun").ToString().Trim
                    rsSpcCd += dt.Rows(i - 1).Item("spccd").ToString().Trim
                Next
            Else
                Return "0"
            End If

            Return "1"

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return "-1"
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function fnGet_Test_psm(ByVal rsBcNo As String, _
                                     ByRef rsRegNo As String, ByRef rsPatNm As String, _
                                       ByRef rsTestCd As String, ByRef rsTNm As String, _
                                         ByRef rsRefL As String, ByRef rsRefU As String, _
                                           ByRef rsPanicL As String, ByRef rsPanicU As String, _
                                             ByRef rsPsmCd As String) As String

        Dim sFn As String = "Function fnGet_Test_psm"

        If fnGet_OleDbConnection() = False Then
            Return "-1"
        End If

        If fnGetConvBcNo(rsBcNo) = False Then
            Return "-1"
        End If

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT j.regno, j.patnm, r.testcd, f6.tnmd tnm,"
            sSql += "        f61.refl, f61.refu, f6.panicl, f6.panich panicu, p.psmcd"
            sSql += "  FROM lj010m j, lr010m r,lf060m f6,"
            sSql += "       psmvm p,"
            sSql += "       (SELECT f6.testcd, f61.spccd,"
            sSql += " 	            CASE WHEN f6.refgbn + j.sex = '2M' THEN f61.reflm WHEN f6.refgbn + j.sex = '2F' THEN f61.reflf ELSE '' END refl,"
            sSql += " 		        CASE WHEN f6.refgbn = j.sex = '2M' THEN f61.refhm WHEN f6.refgbn + j.sex = '2F' THEN f61.refhf ELSE '' END refu"
            sSql += "          FROM lj010m j, lr010m r, lf060m f6, lf061m f61, "
            sSql += "         WHERE j.bcno    = :bcno"
            sSql += "           AND j.bcno    = r.bcno"
            sSql += "           AND r.testcd  = f6.testcd"
            sSql += "  	        AND r.spccd   = f6.spccd"
            sSql += "      	    AND r.tkdt   >= f6.usdt"
            sSql += "  	        AND r.tkdt   <  f6.uedt"
            sSql += "      	    AND f6.testcd = f61.testcd"
            sSql += "     	    AND f6.spccd  = f61.spccd"
            sSql += "     	    AND f6.usdt   = f61.usdt"
            sSql += "     	    AND ROUND(f61.sagec * 365) + f61.sages * 0.1 <= j.dage"
            sSql += "     	    AND j.dage <= ROUND(f61.eagec * 365) - f61.eages * 0.1"
            sSql += "     	) f61"
            sSql += " WHERE j.bcno     = :bcno"
            sSql += "   AND j.bcno     = r.bcno"
            sSql += "   AND r.testcd   = f6.testcd"
            sSql += "   AND r.spccd    = f.spccd"
            sSql += "   AND r.tkdt    >= f6.usdt"
            sSql += "   AND r.tkdt    <  f6.uedt"
            sSql += "   AND f6.tcdgbn IN ('S', 'P', 'C')"
            sSql += "   AND r.testcd   = p.testcd (+)"
            sSql += "   AND r.spccd    = p.spccd (+)"
            sSql += "   AND r.testcd   = f61.testcd (+)"
            sSql += "   AND r.spccd    = f61.spccd (+)"

            Dim dbCmd As OracleCommand = New OracleCommand
            Dim dbDa As OracleDataAdapter = New OracleDataAdapter(dbCmd)
            Dim dt As DataTable = New DataTable

            dbCmd.Connection = m_dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            dt.Reset()
            dbDa.Fill(dt)

            Dim iCnt As Integer = dt.Rows.Count

            Dim sSEP As String = Convert.ToChar(1)

            If iCnt > 0 Then
                '초기화
                rsRegNo = "" : rsPatNm = ""
                rsTestCd = "" : rsTNm = ""
                rsRefL = "" : rsRefU = ""
                rsPanicL = "" : rsPanicU = ""
                rsPsmCd = ""

                For i As Integer = 1 To iCnt
                    If i > 1 Then
                        rsRegNo += sSEP
                        rsPatNm += sSEP
                        rsTestCd += sSEP
                        rsTNm += sSEP
                        rsRefL += sSEP
                        rsRefU += sSEP
                        rsPanicL += sSEP
                        rsPanicU += sSEP
                        rsPsmCd += sSEP
                    End If

                    rsRegNo += dt.Rows(i - 1).Item("regno").ToString()
                    rsPatNm += dt.Rows(i - 1).Item("patnm").ToString()
                    rsTestCd += dt.Rows(i - 1).Item("testcd").ToString()
                    rsTNm += dt.Rows(i - 1).Item("tnm").ToString()
                    rsRefL += dt.Rows(i - 1).Item("refl").ToString()
                    rsRefU += dt.Rows(i - 1).Item("refu").ToString()
                    rsPanicL += dt.Rows(i - 1).Item("panicl").ToString()
                    rsPanicU += dt.Rows(i - 1).Item("panicu").ToString()
                    rsPsmCd += dt.Rows(i - 1).Item("psmcd").ToString()
                Next
            End If

            Return iCnt.ToString()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return "-1"
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function fnExe_EqCd_Info(ByVal rsEqCd As String, ByVal rsBcNo As String, ByVal rsRack As String, ByVal rsPos As String, ByVal rsSvrCd As String, ByRef rsErrMsg As String) As String
        Dim sFn As String = "Function fnExe_EqCd_Info"

        '초기화
        rsErrMsg = ""

        'rsSystemSeq는 개별적으로 n개(n = 1 ~ 9)의 System에 채혈정보를 보내야하는 경우에 사용
        If rsEqCd = "" Then
            rsErrMsg = "I/F 장비코드가 잘못되었습니다."
            Return "-1"
        End If

        If fnGet_OleDbConnection() = False Then
            rsErrMsg = "DB 연결에 실패하였습니다."
            Return "-1"
        End If

        If fnGetConvBcNo(rsBcNo) = False Then
            rsErrMsg = "검체번호가 올바르지 않습니다."

            Return "-1"
        End If

        Dim dbTran As OracleTransaction = m_dbCn.BeginTransaction()

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim dbCmd As OracleCommand = New OracleCommand

            Dim sTblNm As String
            Dim sSql As String = ""
            Dim iRow As Integer = 0

            If COMMON.CommLogin.LOGIN.PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                sTblNm = "lm010m"
            Else
                sTblNm = "lr010m"
            End If
            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = dbTran
            dbCmd.CommandType = CommandType.Text

            Dim sBuf() As String = rsSvrCd.Split("|"c)
            Dim dteRstDt As Date = fnGetServerDateTime()

            For ix As Integer = 0 To sBuf.Length - 1
                sSql = ""
                sSql += "UPDATE " + sTblNm + " SET eqcd = :eqcd, rstdt = :rstdt, eqrack = :eqrack, eqpos = :eqpos"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = rsEqCd
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dteRstDt
                    .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = rsRack
                    .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = rsPos

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = sBuf(ix)
                End With

                iRow += dbCmd.ExecuteNonQuery()
            Next

            If iRow > 0 Then
                dbTran.Commit()

                Return "1"
            End If

            dbTran.Rollback()

            rsErrMsg = "Occur Error : Update " + sTblNm

            Return "0"

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            rsErrMsg = ex.Message

            dbTran.Rollback()

            Return "-1"
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Public Function fnExe_RcvFlgCollectedItem(ByVal rsSystemSeq As String, ByVal rsBcNo As String, ByRef rsErrMsg As String) As String
        Dim sFn As String = "Function fnExe_RcvFlgCollectedItem"

        '초기화
        rsErrMsg = ""

        'rsSystemSeq는 개별적으로 n개(n = 1 ~ 9)의 System에 채혈정보를 보내야하는 경우에 사용
        If IsNumeric(rsSystemSeq) = False Then
            rsErrMsg = "I/F SeqNo가 잘못되었습니다."
            Return "-1"
        End If

        If Convert.ToInt32(rsSystemSeq) < 0 Or Convert.ToInt32(rsSystemSeq) > 9 Then
            rsErrMsg = "I/F SeqNo가 잘못되었습니다."
            Return "-1"
        End If

        Dim sTblNm As String = "lnc" + rsSystemSeq + "0m"

        If fnGet_OleDbConnection() = False Then
            rsErrMsg = "DB 연결에 실패하였습니다."
            Return "-1"
        End If

        If fnGetConvBcNo(rsBcNo) = False Then
            rsErrMsg = "검체번호가 올바르지 않습니다."

            Return "-1"
        End If

        m_dbTran = m_dbCn.BeginTransaction()

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim dbCmd As OracleCommand = New OracleCommand

            Dim sSql As String = ""

            Dim iRow As Integer = 0

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.Text

            sSql = ""
            sSql += "DELETE " + sTblNm + " WHERE bcno = :bcno"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            iRow = dbCmd.ExecuteNonQuery()

            sSql = ""
            sSql += "INSERT INTO " + sTblNm + " ( bcno ) "
            sSql += "     VALUES ( :bcno )"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            iRow = dbCmd.ExecuteNonQuery()

            If iRow = 1 Then
                m_dbTran.Commit()

                Return "1"
            End If

            m_dbTran.Rollback()

            rsErrMsg = "Occur Error : Insert " + sTblNm

            Return "0"

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            rsErrMsg = ex.Message

            m_dbTran.Rollback()

            Return "-1"
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Function

    Private Function fnExe_Collect(ByRef rsErrMsg As String) As ArrayList
        Dim sFn As String = "fnExe_Collect(*String) As ArrayList"
        Dim al_return As New ArrayList

        Try
            Dim dbCmd As New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran

            Dim sRet As String = ""
            rsErrMsg = ""

            For ix As Integer = 1 To OrdInfo.FKOCSs.Count

                With dbCmd
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_collect_take"

                    .Parameters.Clear()
                    .Parameters.Add("rs_regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo
                    .Parameters.Add("rs_orddt", OracleDbType.Varchar2).Value = OrdInfo.OrderDay.Replace("-", "")
                    .Parameters.Add("rs_fkocs", OracleDbType.Varchar2).Value = OrdInfo.FKOCSs(ix - 1)
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = UserID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = LOCALIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 1000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = ""

                    .ExecuteNonQuery()

                    sRet = .Parameters(5).Value.ToString

                    If sRet.StartsWith("00") Then
                        al_return.Add(sRet.Substring(2))
                    Else
                        rsErrMsg += sRet.Substring(2)
                    End If
                End With

            Next

            Return al_return

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            rsErrMsg = sFn + " - " + ex.Message

            Return al_return

        End Try
    End Function

    Private Function fnCollect_LJ010M(ByVal rsBcNo As String, ByVal rsSpcCd As String, ByVal rsStatGbn As String) As Integer
        Dim sFn As String = "fnCollect_LJ010M(String, String, String) As Integer"

        '검체 정보
        Dim iReturn As Integer = 0

        Try
            Dim dbCmd As OracleCommand
            Dim dbDa As New OracleDataAdapter

            dbCmd = New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.Text

            Dim iRow As Integer = 0

            Dim sSql As String = ""

            sSql = ""
            sSql += "INSERT INTO lj010m"
            sSql += "          (  bcno,    spccd,    regno,    patnm,   sex,    age,    dage,     owngbn,  iogbn,     orddt,"
            sSql += "             deptcd,  doctorcd, wardno,   roomno,  bedno,  entdt,  statgbn,  opdt,    jubsugbn,  bcclscd,"
            sSql += "             spcflg,  bcprtdt,  bcprtid"
            sSql += "          )"
            sSql += "   VALUES ( :bcno,   :spccd,   :regno,   :patnm,  :sex,   :age,   :dage,    :owngbn, :iogbn,    :orddt,"
            sSql += "            :deptcd, :drcd,    :wardno,  :roomno, :bedno, :entdt, :statgbn, :opdt,   :jubsugbn, :bcclscd,"
            sSql += "            '2',     :bcprtdt, :bcprtid"
            sSql += "          )"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("spccd", OracleDbType.Varchar2).Value = rsSpcCd
                .Parameters.Add("regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo
                .Parameters.Add("patnm", OracleDbType.Varchar2).Value = OrdInfo.PatNm
                .Parameters.Add("sex", OracleDbType.Varchar2).Value = OrdInfo.Sex
                .Parameters.Add("age", OracleDbType.Varchar2).Value = OrdInfo.Age
                .Parameters.Add("dage", OracleDbType.Varchar2).Value = OrdInfo.DAge
                .Parameters.Add("owngbn", OracleDbType.Varchar2).Value = OrdInfo.OwnGbn
                .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = OrdInfo.IOGbn
                .Parameters.Add("orddt", OracleDbType.Varchar2).Value = mdt_ord

                .Parameters.Add("deptcd", OracleDbType.Varchar2).Value = OrdInfo.DeptCd
                .Parameters.Add("drcd", OracleDbType.Varchar2).Value = OrdInfo.DoctorCd
                .Parameters.Add("wardno", OracleDbType.Varchar2).Value = OrdInfo.WardNo
                .Parameters.Add("roomno", OracleDbType.Varchar2).Value = OrdInfo.RoomNo
                .Parameters.Add("bedno", OracleDbType.Varchar2).Value = OrdInfo.BedNo

                If IsDate(OrdInfo.EntDt) Then
                    .Parameters.Add("entdt", OracleDbType.Varchar2).Value = OrdInfo.EntDt.Replace("-", "")
                Else
                    .Parameters.Add("entdt", OracleDbType.Varchar2).Value = DBNull.Value
                End If

                .Parameters.Add("statgbn", OracleDbType.Varchar2).Value = rsStatGbn

                If IsDate(OrdInfo.OpDt) Then
                    .Parameters.Add("opdt", OracleDbType.Varchar2).Value = OrdInfo.OpDt
                Else
                    .Parameters.Add("opdt", OracleDbType.Varchar2).Value = DBNull.Value
                End If

                .Parameters.Add("jubsugbn", OracleDbType.Varchar2).Value = OrdInfo.JubsuGbn
                .Parameters.Add("bcclscd", OracleDbType.Varchar2).Value = DBNull.Value

                .Parameters.Add("bcprtdt", OracleDbType.Varchar2).Value = mdt_sys
                .Parameters.Add("bcprtid", OracleDbType.Varchar2).Value = UserID
            End With

            iRow = dbCmd.ExecuteNonQuery()

            iReturn += iRow

            Return iReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Return iReturn

        End Try
    End Function

    Private Function fnCollect_LJ011M(ByVal rsBcNo As String, ByVal rsSugaCd As String, ByVal riIndex As Integer) As Integer
        Dim sFn As String = "fnCollect_LJ011M(String, String, Integer) As Integer"

        '검체 정보
        Dim iReturn As Integer = 0

        Try
            Dim dbCmd As OracleCommand

            dbCmd = New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.Text

            Dim iRow As Integer = 0

            Dim sSql As String = ""

            sSql = ""
            sSql += "INSERT INTO lj011m"
            sSql += "          (  bcno,       tclscd,   spccd,   regno,     collid,     colldt,       owngbn,  iogbn,  fkocs,  orddt,"
            sSql += "             doctorrmk,  collvol,  spcflg,  orgorddt,  orgdeptcd,  orgdoctorcd,  sysdt"
            sSql += "          )"
            sSql += "    VALUES( :bcno,      :testcd,  :spccd,  :regno,    :collid,    :colldt,      :owngbn, :iogbn, :fkcos, :orddt,"
            sSql += "            :drcd,      :colval,  '2',     :orgorddt, :orgdeptcd, :orgdrcd,     fn_ack_sysdate"
            sSql += "            )"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = OrdInfo.TestCds(riIndex)
                .Parameters.Add("spccd", OracleDbType.Varchar2).Value = OrdInfo.SpcCds(riIndex)
                .Parameters.Add("regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo
                .Parameters.Add("collid", OracleDbType.Varchar2).Value = UserID

                .Parameters.Add("colldt", OracleDbType.Varchar2).Value = mdt_sys
                .Parameters.Add("owngbn", OracleDbType.Varchar2).Value = OrdInfo.OwnGbn
                .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = OrdInfo.IOGbn
                .Parameters.Add("fkocs", OracleDbType.Int64).Value = OrdInfo.FKOCSs(riIndex)
                .Parameters.Add("orddt", OracleDbType.Varchar2).Value = mdt_ord

                .Parameters.Add("docrmk", OracleDbType.Varchar2).Value = OrdInfo.TRemarks(riIndex)

                .Parameters.Add("collvol", OracleDbType.Varchar2).Value = DBNull.Value
                .Parameters.Add("orgorddt", OracleDbType.Varchar2).Value = mdt_ord
                .Parameters.Add("orgdept", OracleDbType.Varchar2).Value = OrdInfo.DeptCd
                .Parameters.Add("orgdrcd", OracleDbType.Varchar2).Value = OrdInfo.DoctorCd

            End With

            iRow = dbCmd.ExecuteNonQuery()

            iReturn += iRow

            Return iReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Return iReturn

        End Try
    End Function

    Private Function fnCollect_LJ012M(ByVal rsBcNo As String) As Integer
        Dim sFn As String = "fnCollect_LJ012M(String) As Integer"

        '신장/체중 정보
        Dim iReturn As Integer = 0

        Try
            Dim dbCmd As OracleCommand

            dbCmd = New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.Text

            Dim iRow As Integer = 0

            Dim sSql As String = ""

            sSql = ""
            sSql += " insert into lj012m (  bcno,  height,  weight )"
            sSql += "             values ( :bcno, :height, :weight )"
            sSql += " )"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()

                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                If OrdInfo.Height = 0 Then
                    .Parameters.Add("height", OracleDbType.Varchar2).Value = "0"
                Else
                    .Parameters.Add("height", OracleDbType.Varchar2).Value = OrdInfo.Height.ToString()
                End If

                If OrdInfo.Weight = 0 Then
                    .Parameters.Add("weight", OracleDbType.Varchar2).Value = "0"
                Else
                    .Parameters.Add("weight", OracleDbType.Varchar2).Value = OrdInfo.Weight.ToString()
                End If
            End With

            iRow = dbCmd.ExecuteNonQuery()

            iReturn += iRow

            Return iReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return iReturn

        End Try
    End Function

    Private Function fnCollect_LJ013M(ByVal rsBcNo As String) As Integer
        Dim sFn As String = "fnCollect_LJ013M(String) As Integer"

        '진단/상병 정보
        Dim iReturn As Integer = 0

        If DiagInfos Is Nothing Then Return 1

        Dim sDiagNm As String = ""
        Dim sDiagNmE As String = ""

        For i As Integer = 1 To DiagInfos.Count
            If sDiagNm.Length > 0 Then sDiagNm += ", "
            If sDiagNmE.Length > 0 Then sDiagNmE += ", "

            sDiagNm += CType(DiagInfos(i - 1), DiagnosticsInfo).DiagNm
            sDiagNmE += CType(DiagInfos(i - 1), DiagnosticsInfo).DiagNmE
        Next

        Try
            Dim dbCmd As OracleCommand

            dbCmd = New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.Text

            Dim iRow As Integer = 0

            Dim sSql As String = ""

            sSql = ""
            sSql += " insert into lj013m (  bcno,  diagnm,  diagnm_eng )"
            sSql += "             values ( :bcno, :diagnm, :diagnm_eng )"
            sSql += " )"

            dbCmd.CommandText = sSql

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("diagnm", OracleDbType.Varchar2).Value = sDiagNm
                .Parameters.Add("diagnm_eng", OracleDbType.Varchar2).Value = sDiagNmE
            End With

            iRow = dbCmd.ExecuteNonQuery()

            iReturn += iRow

            Return iReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return iReturn

        End Try
    End Function

    Private Function fnGetConvBcNo(ByRef rsBcNo As String) As Boolean
        Dim sFn As String = "fnGetConvBcNo"

        Try
            '모검체는 OK
            If rsBcNo.Length >= 15 Then
                rsBcNo = rsBcNo.Substring(0, 15)

                Return True
            Else
                If rsBcNo.Length >= 11 Then
                    rsBcNo = rsBcNo.Substring(0, 11)
                Else
                    Return False
                End If
            End If

            Dim sSql As String = ""

            sSql = ""
            'sSql += "SELECT fn_get_bcno_from_prt(:bcno) FROM DUAL"
            sSql += "SELECT fn_ack_get_bcno_normal(:bcno) FROM DUAL"

            Dim dbCmd As OracleCommand = New OracleCommand
            Dim dbDa As OracleDataAdapter = New OracleDataAdapter(dbCmd)
            Dim dt As DataTable = New DataTable

            dbCmd.Connection = m_dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                rsBcNo = dt.Rows(0).Item(0).ToString()

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return False
        End Try
    End Function

    Private Function fnGet_OleDbConnection() As Boolean
        Dim sFn As String = "fnGet_OleDbConnection"

        Try
            If UseLIS Then
                m_dbCn = GetDbConnection()
            Else
                m_dbCn = GetDbConnection(DbProvider, DbDatasource, DbUsername, DbPassword, "")
            End If

            Return True

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return False

        End Try
    End Function

    Private Function fnGetPreWorkNo(ByVal rsBcNo As String) As String
        Dim sFn As String = "fnGetPreWorkNo"

        Try
            Dim sReturn As String = ""

            Dim sSql As String = ""
            sSql = ""
            sSql += "SELECT wkymd || NVL(wkgrpcd, '') || NVL(wkno) workno"
            sSql += "  FROM lr010m"
            sSql += " WHERE bcno = :bcno"
            sSql += " UNION "
            sSql += "SELECT wkymd || NVL(wkgrpcd, '') || NVL(wkno) workno"
            sSql += "  FROM lm010m"
            sSql += " WHERE bcno = :bcno"

            Dim dbCmd As OracleCommand = New OracleCommand
            Dim dbDa As OracleDataAdapter = New OracleDataAdapter(dbCmd)
            Dim dt As DataTable = New DataTable

            dbCmd.Connection = m_dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                sReturn = Fn.WKNO_View(dt.Rows(0).Item(0).ToString())
            End If

            Return sReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return ""
        End Try
    End Function

    Private Function fnGetServerDateTime() As Date
        Dim sFn As String = "fnGetServerDateTime() As Date"

        Dim dtReturn As Date = Now.Date

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT fn_ack_date_str(fn_ack_sysdate, 'yyyy-mm-dd hh24:mi:ss') FROM DUAL"

            Dim dbCmd As OracleCommand = New OracleCommand
            Dim dbDa As OracleDataAdapter = New OracleDataAdapter(dbCmd)
            Dim dt As DataTable = New DataTable

            dbCmd.Connection = m_dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            With dbDa
                .SelectCommand.Parameters.Clear()
            End With

            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                dtReturn = CType(dt.Rows(0).Item(0), Date)
            End If

            Return dtReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return dtReturn
        End Try
    End Function

    Private Function fnGetServerDateTime(ByVal rsOrderDay As String) As Date
        Dim sFn As String = "fnGetServerDateTime(String) As Date"

        Dim dtReturn As Date = Now.Date

        Try
            Dim a_obj(7) As Object
            Dim sOrderDay As String = rsOrderDay.Replace("-", "").Replace("/", "")
            sOrderDay.ToCharArray().CopyTo(a_obj, 0)

            If IsNumeric(sOrderDay) And sOrderDay.Length = 8 Then
                sOrderDay = String.Format("{0}{1}{2}{3}-{4}{5}-{6}{7}", a_obj)
            Else
                sOrderDay = Now.Date.ToString("yyyy-MM-dd")
            End If

            Dim sSql As String = ""

            sSql = ""
            sSql += " select fn_ack_date_str(:orddt, 'yyyy-mm-dd') + ' ' + fn_ack_date_str(fn_ack_sysdate, 'hh24:mi:ss') FROM DUAL"

            Dim dbCmd As OracleCommand = New OracleCommand
            Dim dbDa As OracleDataAdapter = New OracleDataAdapter(dbCmd)
            Dim dt As DataTable = New DataTable

            dbCmd.Connection = m_dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("orddt", OracleDbType.Varchar2).Value = sOrderDay
            End With

            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                dtReturn = CType(dt.Rows(0).Item(0), Date)
            End If

            Return dtReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return dtReturn
        End Try
    End Function

    Private Function fnExe_Order_ocs(ByRef rsErrMsg As String, ByVal rbOrderOnly As Boolean) As ArrayList
        Dim sFn As String = "fnExe_Order_ocs(*String, Boolean) As ArrayList"
        Dim al_return As New ArrayList

        Try
            Dim dbCmd As OracleCommand

            dbCmd = New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.StoredProcedure

            Dim sSql As String = ""
            Dim sOcsKey As String = ""

            '1) Order 검사처방
            With OrdInfo
                For ix As Integer = 1 To .TestCds.Count
                    '1-0) 초기화
                    Dim iRow As Integer = 0
                    Dim sErrMsg As String = ""

                    sSql = "up_itf_lis_coe_ptnt_ord_i"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("poctyn", OracleDbType.Varchar2).Value = "N"                                      '0
                        .Parameters.Add("poctdeptcd", OracleDbType.Varchar2).Value = PRG_CONST.DEPT_LAB                   '1

                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo                             '2
                        .Parameters.Add("orddt", OracleDbType.Varchar2).Value = Format(mdt_ord, "yyyyMMdd").ToString      '3
                        .Parameters.Add("deptcd", OracleDbType.Varchar2).Value = OrdInfo.DeptCd                           '4
                        .Parameters.Add("drcd", OracleDbType.Varchar2).Value = OrdInfo.DoctorCd                           '5
                        .Parameters.Add("ordcd", OracleDbType.Varchar2).Value = OrdInfo.TestCds(ix - 1)                   '6
                        .Parameters.Add("chosno", OracleDbType.Varchar2).Value = OrdInfo.Chos_No                          '7
                        .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = OrdInfo.IOGbn                             '8
                        .Parameters.Add("orddiv", OracleDbType.Varchar2).Value = "L"                                      '9
                        .Parameters.Add("emeryn", OracleDbType.Varchar2).Value = OrdInfo.EmerYNs(ix - 1)                  '10
                        .Parameters.Add("kubgb", OracleDbType.Varchar2).Value = OrdInfo.EdiGbns(ix - 1)                    '11
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = OrdInfo.SpcCds(ix - 1)                    '12
                        .Parameters.Add("orddt_org", OracleDbType.Varchar2).Value = OrdInfo.OrdDt_org.Substring(0, 8)     '13
                        .Parameters.Add("fkocs_org", OracleDbType.Varchar2).Value = OrdInfo.FkOcs_org.Split("/"c)(0)      '14
                        .Parameters.Add("iogbn_org", OracleDbType.Varchar2).Value = OrdInfo.IoGbn_org                     '15
                        .Parameters.Add("ord_rmk", OracleDbType.Varchar2).Value = OrdInfo.TRemarks(ix - 1)                '16

                        .Parameters.Add("ord_stus", OracleDbType.Varchar2).Value = "10"                                   '17

                        .Parameters.Add("ord_qtn", OracleDbType.Int32).Value = "0"                                     '18
                        .Parameters.Add("ord_ht", OracleDbType.Int32).Value = "0"                                      '19
                        .Parameters.Add("ord_wt", OracleDbType.Int32).Value = "0"                                      '20
                        .Parameters.Add("ord_resn1", OracleDbType.Varchar2).Value = ""                                    '21
                        .Parameters.Add("ord_resn2", OracleDbType.Varchar2).Value = ""                                    '22
                        .Parameters.Add("ord_resn3", OracleDbType.Varchar2).Value = ""                                    '23
                        .Parameters.Add("ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP                            '24

                        .Parameters.Add("retcd", OracleDbType.Varchar2, 2)                                                   '25
                        .Parameters("retcd").Direction = ParameterDirection.InputOutput
                        .Parameters("retcd").Value = ""

                        .Parameters.Add("retmsg", OracleDbType.Varchar2, 1000)                                                  '26
                        .Parameters("retmsg").Direction = ParameterDirection.InputOutput
                        .Parameters("retmsg").Value = ""

                        .ExecuteNonQuery()

                        Dim sRetCd As String = .Parameters(25).Value.ToString

                        If sRetCd = "00" Then
                            OrdInfo.FKOCSs(ix - 1) = .Parameters(26).Value.ToString
                            al_return.Add(OrdInfo.FKOCSs(ix - 1))
                        Else
                            al_return.Add(msErrMsg + "처방발생 오류")
                        End If

                    End With

                Next
            End With

            rsErrMsg = ""

            Return al_return

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            rsErrMsg = sFn + " - " + ex.Message

            Return al_return

        End Try
    End Function

    Private Function fnExe_Order_lis(ByRef rsErrMsg As String, ByVal rbOrderOnly As Boolean) As ArrayList
        Dim sFn As String = "fnExe_Order_lis(*String, Boolean) As ArrayList"
        Dim al_return As New ArrayList

        Try
            Dim dbCmd As OracleCommand

            dbCmd = New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.Text

            Dim sSql As String = ""

            '1) Order 검사처방
            With OrdInfo
                For ix As Integer = 1 To .TestCds.Count
                    '1-0) 초기화
                    Dim iRow As Integer = 0
                    Dim sErrMsg As String = ""

                    '1-1) OcsKey 생성
                    Dim sOcsKey As String = fnTrans_KeyNo(sErrMsg, OrdInfo.OrderDay.Replace("-", "").Replace("/", ""), "OR")

                    If IsNumeric(sOcsKey) Then
                        OrdInfo.FKOCSs(ix - 1) = OrdInfo.OrderDay.Replace("-", "").Replace("/", "") + sOcsKey
                    Else
                        OrdInfo.FKOCSs(ix - 1) = sErrMsg
                    End If

                    If IsNumeric(sOcsKey) Then
                        sSql = ""
                        sSql += "INSERT INTO mts0001_lis("
                        sSql += "            in_out_gubun, fkocs,        bunho,      gwa,        ipwon_date,"
                        sSql += "            resident,     doctor,       ho_dong,      ho_code,    ho_bed,"
                        sSql += "            order_date,   order_time,   hangmog_code, slip_gubun, specimen_code,"
                        sSql += "            suryang,      hope_date,    hope_time,    dc_yn,      append_yn,"
                        sSql += "            sunab_date,   emergency,    remark,       height,     weight,"
                        sSql += "            opdt,         sys_date,     user_id,      upd_date,   seq"
                        sSql += "          ) "
                        sSql += "SELECT :iogbn,    :fkocs,         :regno,   :deptcd,        :indate,"
                        sSql += "       :resident, :drcd,          :wardno,  :roomno,        :bedno,"
                        sSql += "       :orddt,    :ordtm,         f.tordcd, :slipgbn,       f.spccd,"
                        sSql += "       :suryang,  :hopdt,         :hopetm,  :dcyn,          :appendyn,"
                        sSql += "       :sunabdt,  :eryn,          :reamrk,  :height,        :weight,"
                        sSql += "       :opdt,     fn_ack_sysdate, :usrid,   fn_ack_sysdate, sq_mts0001_lis.nextval"
                        sSql += "  FROM lf060m f"
                        sSql += " WHERE testcd = :testcd"
                        sSql += "   AND spccd  = :spccd"
                        sSql += "   AND usdt  <= :usdt"
                        sSql += "   AND uedt   > :usdt"
                        sSql += "   AND ROWNUM = 1"
                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("iogbn", OracleDbType.Varchar2).Value = OrdInfo.IOGbn
                            .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = OrdInfo.FKOCSs(ix - 1)
                            .Parameters.Add("regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo
                            .Parameters.Add("deptcd", OracleDbType.Varchar2).Value = OrdInfo.DeptCd

                            If OrdInfo.IOGbn = "I" Then
                                .Parameters.Add("indate", OracleDbType.Varchar2).Value = OrdInfo.EntDt.Replace("-", "")
                            Else
                                .Parameters.Add("indate", OracleDbType.Varchar2).Value = DBNull.Value
                            End If

                            .Parameters.Add("resident", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("drcd", OracleDbType.Varchar2).Value = OrdInfo.DoctorCd

                            If OrdInfo.IOGbn = "I" Then
                                .Parameters.Add("wardno", OracleDbType.Varchar2).Value = OrdInfo.WardNo
                                .Parameters.Add("roomno", OracleDbType.Varchar2).Value = OrdInfo.RoomNo
                            Else
                                .Parameters.Add("wardno", OracleDbType.Varchar2).Value = DBNull.Value
                                .Parameters.Add("roomno", OracleDbType.Varchar2).Value = DBNull.Value
                            End If

                            .Parameters.Add("bedno", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("orddt", OracleDbType.Varchar2).Value = mdt_ord.ToString("yyyyMMdd")
                            .Parameters.Add("ordtm", OracleDbType.Varchar2).Value = mdt_ord.ToString("HHmm")
                            .Parameters.Add("slipgbn", OracleDbType.Varchar2).Value = "B"

                            .Parameters.Add("suryang", OracleDbType.Int32).Value = 1
                            .Parameters.Add("hopedt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("hopetm", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("dcyn", OracleDbType.Varchar2).Value = "N"
                            .Parameters.Add("appendyn", OracleDbType.Varchar2).Value = DBNull.Value

                            .Parameters.Add("sunabdt", OracleDbType.Varchar2).Value = Format(mdt_ord, "yyyyMMdd").ToString
                            .Parameters.Add("eryn", OracleDbType.Varchar2).Value = OrdInfo.EmerYNs(ix - 1)
                            .Parameters.Add("remark", OracleDbType.Varchar2).Value = OrdInfo.TRemarks(ix - 1)

                            .Parameters.Add("height", OracleDbType.Double).Value = OrdInfo.Height
                            .Parameters.Add("weight", OracleDbType.Double).Value = OrdInfo.Weight

                            If IsDate(OrdInfo.OpDt) Then
                                .Parameters.Add("opdt", OracleDbType.Varchar2).Value = OrdInfo.OpDt
                            Else
                                .Parameters.Add("opdt", OracleDbType.Varchar2).Value = DBNull.Value
                            End If

                            .Parameters.Add("usrid", OracleDbType.Varchar2).Value = UserID

                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = OrdInfo.TestCds(ix - 1)
                            .Parameters.Add("spccd", OracleDbType.Varchar2).Value = OrdInfo.SpcCds(ix - 1)
                            .Parameters.Add("usdt", OracleDbType.Varchar2).Value = mdt_ord.ToString("yyyyMMddHHmmss")
                            .Parameters.Add("usdt", OracleDbType.Varchar2).Value = mdt_ord.ToString("yyyyMMddHHmmss")
                        End With

                        iRow = dbCmd.ExecuteNonQuery()

                        If iRow > 0 Then
                            al_return.Add(OrdInfo.FKOCSs(ix - 1))
                        Else
                            al_return.Add(msErrMsg + "insert into mts0001_lis")
                        End If
                    Else
                        al_return.Add(OrdInfo.FKOCSs(ix - 1))
                    End If
                Next
            End With

            '2) DIAG 진단(상병)
            fnOrder_Diag()

            '4) PAT 환자정보
            fnOrder_Pat()

            rsErrMsg = ""

            Return al_return

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            rsErrMsg = sFn + " - " + ex.Message

            Return al_return

        End Try
    End Function

    Private Function fnOrder_Diag() As Integer
        Dim sFn As String = "fnOrder_Diag() As Integer"
        Dim iReturn As Integer = 0

        If DiagInfos Is Nothing Then Return iReturn

        Try
            Dim dbCmd As OracleCommand

            dbCmd = New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.Text

            Dim sSql As String = ""

            '2) DIAG 진단(상병)
            With DiagInfos
                For i As Integer = 1 To .Count
                    Dim iRow As Integer = 0

                    sSql = ""
                    sSql += " insert into mts0101_lis ("
                    sSql += "   seq,        order_date, bunho,     sang_code, sang_ename,"
                    sSql += "   sang_hname, send_date,  recv_date, iud,       flag"
                    sSql += " ) values ("
                    sSql += "   sq_mts0101_lis.nextval, :orddt, :regno, :diagcd, :diagenm_e,"
                    sSql += "   :diagnm_h, :senddt, :recvdt, :iud, :flag"
                    sSql += " ) "

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        '.Parameters.Add("seq", OracleDbType.Number).Value = 1
                        .Parameters.Add("orddt", OracleDbType.Varchar2).Value = Convert.ToDateTime(mdt_ord.ToShortDateString())
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo
                        .Parameters.Add("sang_code", OracleDbType.Varchar2).Value = CType(DiagInfos(i - 1), DiagnosticsInfo).DiagCd
                        .Parameters.Add("diagcd", OracleDbType.Varchar2).Value = CType(DiagInfos(i - 1), DiagnosticsInfo).DiagNmE

                        .Parameters.Add("diagnm_e", OracleDbType.Varchar2).Value = CType(DiagInfos(i - 1), DiagnosticsInfo).DiagNm
                        .Parameters.Add("diagnm_h", OracleDbType.Varchar2).Value = CType(DiagInfos(i - 1), DiagnosticsInfo).DiagNm
                        .Parameters.Add("senddt", OracleDbType.Varchar2).Value = DBNull.Value
                        .Parameters.Add("recvdt", OracleDbType.Varchar2).Value = DBNull.Value
                        .Parameters.Add("iud", OracleDbType.Varchar2).Value = "I"
                        .Parameters.Add("flag", OracleDbType.Varchar2).Value = DBNull.Value
                    End With

                    iRow = dbCmd.ExecuteNonQuery()

                    iReturn += iRow
                Next
            End With

            Return iReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return iReturn

        End Try
    End Function

    Private Function fnOrder_Pat() As Integer
        Dim sFn As String = "fnOrder_Pat() As Integer"
        Dim iReturn As Integer = 0

        Try
            Dim dbCmd As OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            dbCmd = New OracleCommand

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran
            dbCmd.CommandType = CommandType.Text

            Dim sSql As String = ""

            '4) PAT 환자정보

            sSql = ""
            sSql += " select bunho"
            sSql += "   from mts0002_lis"
            sSql += "  where bunho = :regno"

            dbCmd.CommandText = sSql

            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo

                .Fill(dt)
            End With

            If dt.Rows.Count > 0 Then
                ' 존재하면 수정
                sSql = ""
                sSql += " update mts0002_lis"
                sSql += "    set suname    = :patnm"
                sSql += "      , birth     = :birthday"
                sSql += "      , sujumin1  = :idnol"
                sSql += "      , sujumin2  = :idnor"
                sSql += "      , tel1      = :tel1"
                sSql += "      , tel2      = :tel2"
                sSql += "      , sex       = :sex"
                sSql += " where bunho = :regno"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("patnm", OracleDbType.Varchar2).Value = OrdInfo.PatNm
                    .Parameters.Add("birthday", OracleDbType.Varchar2).Value = OrdInfo.BirthDay.Replace("-", "").Replace("/", "").Substring(0, 8)
                    .Parameters.Add("idnol", OracleDbType.Varchar2).Value = OrdInfo.IdNoL
                    .Parameters.Add("idnor", OracleDbType.Varchar2).Value = OrdInfo.IdNoR
                    .Parameters.Add("tel1", OracleDbType.Varchar2).Value = OrdInfo.TEL1
                    .Parameters.Add("tel2", OracleDbType.Varchar2).Value = OrdInfo.TEL2
                    .Parameters.Add("sex", OracleDbType.Varchar2).Value = OrdInfo.Sex

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo
                End With

                iReturn = dbCmd.ExecuteNonQuery()
            Else
                sSql = ""
                sSql += " insert into mts0002_lis ("
                sSql += "   seq,      bunho, suname, birth, sujumin1,"
                sSql += "   sujumin2, tel1,  tel2,   sex"
                sSql += " ) values ("
                sSql += "   sq_mts0002_lis.nextval, :regno, :patnm, :birthday, :idnol,"
                sSql += "   :idnor, :tel1, :tel2, :sex"
                sSql += " )"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = OrdInfo.RegNo
                    .Parameters.Add("patnm", OracleDbType.Varchar2).Value = OrdInfo.PatNm
                    .Parameters.Add("birthday", OracleDbType.Varchar2).Value = OrdInfo.BirthDay.Replace("-", "").Replace("/", "").Substring(0, 8)
                    .Parameters.Add("idnol", OracleDbType.Varchar2).Value = OrdInfo.IdNoL

                    .Parameters.Add("idnor", OracleDbType.Varchar2).Value = OrdInfo.IdNoR
                    .Parameters.Add("tel1", OracleDbType.Varchar2).Value = OrdInfo.TEL1
                    .Parameters.Add("tel2", OracleDbType.Varchar2).Value = OrdInfo.TEL2
                    .Parameters.Add("sex", OracleDbType.Varchar2).Value = OrdInfo.Sex
                End With

                iReturn = dbCmd.ExecuteNonQuery()
            End If

            Return iReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return iReturn

        End Try
    End Function

    Private Function fnTakeBcNo(ByVal rsBcNo As String, ByVal rsUserID As String, _
                                    ByRef rbErr As Boolean, ByRef riErrNo As Integer, ByRef rsErrMsg As String) As String

        Dim sFn As String = "fnTakeBcNo"

        Dim dbTran As OracleTransaction = m_dbCn.BeginTransaction()

        Try
            Dim dbCmd As OracleCommand = New OracleCommand

            Dim objIpEntry As IPHostEntry = Dns.GetHostByName(Dns.GetHostName())
            Dim objIpAdrees As IPAddress() = objIpEntry.AddressList

            Dim sSql As String = "pro_ack_exe_take"

            Dim iRow As Integer = 0

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = dbTran
            dbCmd.CommandType = CommandType.StoredProcedure
            dbCmd.CommandText = sSql

            Dim sReturn As String = ""

            With dbCmd
                .Parameters.Clear()
                .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("rs_wknoyn", OracleDbType.Varchar2).Value = ""
                .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = rsUserID
                .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = objIpAdrees(0).ToString

                Dim oledbparam As OracleParameter = New OracleParameter

                With oledbparam
                    .Direction = ParameterDirection.InputOutput
                    .OracleDbType = OracleDbType.Varchar2
                    .ParameterName = "rs_retval"
                    .Size = 4000
                    .Value = sReturn
                End With

                .Parameters.Add(oledbparam)
            End With

            iRow = dbCmd.ExecuteNonQuery()

            sReturn = dbCmd.Parameters(4).Value.ToString()

            Dim sWkNo As String = ""

            If IsNumeric(sReturn.Substring(0, 2)) Then
                riErrNo = Convert.ToInt32(sReturn.Substring(0, 2))
                rsErrMsg = sReturn.Substring(2)

                If riErrNo = 0 Then
                    rbErr = False

                    '작업번호
                    sWkNo = rsErrMsg

                    dbTran.Commit()
                Else
                    rbErr = True

                    dbTran.Rollback()
                End If
            Else
                rbErr = True
                riErrNo = 99
                rsErrMsg = ""

                dbTran.Rollback()
            End If

            Return sWkNo

        Catch ex As Exception
            dbTran.Rollback()
            Return ""
        End Try
    End Function

    Private Function fnTrans_KeyNo(ByRef rsErrMsg As String, ByVal rsDate As String, ByVal rsGbn As String) As String
        Dim sFn As String = "Public Function fnGet_FKOCS(String, String) As String"
        Dim dbCmd As New OracleCommand
        Dim dbDa As OracleDataAdapter
        Dim dt As New DataTable

        Dim strErrVal As String = ""

        Try

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran

            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim sValue As String = ""

            sSql = ""
            sSql += "UPDATE ln010m SET seqno = seqno + 1"
            sSql += " WHERE SEQYMD = :seqymd"
            sSql += "   AND SEQgbn = :seqgbn"
            sSql += "   AND JOBGBN = :jobgbn"

            With dbCmd
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("seqymd", OracleDbType.Varchar2).Value = rsDate
                .Parameters.Add("seqgbn", OracleDbType.Varchar2).Value = rsGbn
                .Parameters.Add("jobgbn", OracleDbType.Varchar2).Value = "9"

                iRet = .ExecuteNonQuery()

            End With

            If iRet = 0 Then
                sSql = ""
                sSql += "INSERT INTO ln010m (seqymd, seqgbn, seqno, jobgbn) VALUES (:seqymd, :seqgbn, 1, :jobgbn)"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("seqymd", OracleDbType.Varchar2).Value = rsDate
                    .Parameters.Add("seqgbn", OracleDbType.Varchar2).Value = rsGbn
                    .Parameters.Add("jobgbn", OracleDbType.Varchar2).Value = "9"
                    .ExecuteNonQuery()

                End With

                sValue = "1"
            Else
                sSql = ""
                sSql += "SELECT seqno FROM ln010m"
                sSql += " WHERE seqymd = :seqymd"
                sSql += "   AND seqgbn = :seqgbn"
                sSql += "   AND jobgbn = :jobgbn"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("seqymd", OracleDbType.Varchar2).Value = rsDate
                    .SelectCommand.Parameters.Add("seqgbn", OracleDbType.Varchar2).Value = rsGbn
                    .SelectCommand.Parameters.Add("jobgbn", OracleDbType.Varchar2).Value = "9"
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    sValue = dt.Rows(0).Item("seqno").ToString
                Else
                    sValue = ""
                End If
            End If

            Return sValue

        Catch ex As Exception
            rsErrMsg = ex.Message
            Return ""
        End Try
    End Function

    Private Sub sbInitial_Order_Collect_Take()
        Dim sFn As String = "sbInitial_Order_Collect_Take"

        Try
            Dim al_trmk As New ArrayList
            Dim al_emer As New ArrayList
            Dim al_fkocs As New ArrayList
            Dim al_coll As New ArrayList
            Dim al_take As New ArrayList

            If OrdInfo.TestCds Is Nothing Then
                MsgBox("검사코드 설정 내역이 없습니다. 확인하여 주십시요!!")

                Return
            End If

            If OrdInfo.SpcCds Is Nothing Then
                MsgBox("검체코드 설정 내역이 없습니다. 확인하여 주십시요!!")

                Return
            End If

            For i As Integer = 1 To OrdInfo.TestCds.Count
                al_trmk.Add("")
                al_emer.Add("")
                al_fkocs.Add("")
                al_coll.Add("")
                al_take.Add("")
            Next

            If OrdInfo.TRemarks Is Nothing Then
                OrdInfo.TRemarks = al_trmk
            End If

            If OrdInfo.EmerYNs Is Nothing Then
                OrdInfo.EmerYNs = al_emer
            End If

            If OrdInfo.FKOCSs Is Nothing Then
                OrdInfo.FKOCSs = al_fkocs
            End If

            If OrdInfo.Collects Is Nothing Then
                OrdInfo.Collects = al_coll
            End If

            If OrdInfo.Takes Is Nothing Then
                OrdInfo.Takes = al_take
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

        End Try
    End Sub
End Class

Public Class OrderInfo
    Public OrderDay As String = ""
    Public RegNo As String = ""
    Public PatNm As String = ""
    Public Sex As String = ""
    Public Age As String = ""
    Public DAge As String = ""
    Public IdNoL As String = ""
    Public IdNoR As String = ""
    Public BirthDay As String = ""
    Public TEL1 As String = ""
    Public TEL2 As String = ""
    Public DoctorCd As String = ""
    Public DoctorNm As String = ""
    Public DeptCd As String = ""
    Public DeptNm As String = ""
    Public WardNo As String = ""
    Public RoomNo As String = ""
    Public BedNo As String = ""
    Public EntDt As String = ""
    Public OpDt As String = ""
    Public JubsuGbn As String = "0"
    Public OwnGbn As String = ""
    Public IOGbn As String = ""
    Public Height As Double = 0
    Public Weight As Double = 0

    Public Chos_No As String = ""       '-- 내원번호
    Public OrdDt_org As String = ""     '-- 원처방일자
    Public FkOcs_org As String = ""     '-- 원처방 fkocs
    Public IoGbn_org As String = ""     '-- 원 입외구분

    Public TestCds As ArrayList         '-- 검사코드
    Public SpcCds As ArrayList          '-- 검체코드
    Public EdiGbns As ArrayList         '-- 급여구분
    Public TRemarks As ArrayList        '-- 처방비고
    Public EmerYNs As ArrayList         '-- 응급여부
    Public FKOCSs As ArrayList
    Public Collects As ArrayList
    Public Takes As ArrayList
End Class

Public Class DiagnosticsInfo
    Public OrderDay As String = ""
    Public RegNo As String = ""
    Public DiagCd As String = ""
    Public DiagNm As String = ""
    Public DiagNmE As String = ""
End Class

Public Class DrugInfo
    Public OrderDay As String = ""
    Public RegNo As String = ""
    Public DrugCd As String = ""
    Public DrugNm As String = ""
    Public Quantity As Integer
    Public Between As Integer
End Class

