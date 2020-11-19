'*****************************************************************************************/
'/*                                                                                      */
'/* Project Name : 원자력병원 Laboratory Information System(KMC_LIS)                     */
'/*                                                                                      */
'/*                                                                                      */
'/* FileName     : CGDA_EXLAB.vb                                                         */
'/* PartName     : 위탁검사에 사용되는 공유 Data Access                                  */
'/* Description  : 위탁검사 공유 Data Access Class                                       */
'/* Design       :                                                                       */
'/* Coded        : 2007-10-23 hyde                                                       */
'/* Modified     :                                                                       */
'/*                                                                                      */
'/*                                                                                      */
'/*                                                                                      */
'/****************************************************************************************/
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports System.Drawing
Imports System.IO

Public Class APP_EXLAB

    Private Const msFile As String = "File : CGLISAPP_EXLAB.vb, Class : LISAPP.APP_EXLAB" + vbTab

    Public Shared Function fnExe_Get_Data_SML_Image(ByRef ribufferSize As Integer, ByRef rsFilePath As String) As Byte()
        Dim sFn As String = "Public Shared Function fnExe_Get_Data_SML_Image() As String"
        Dim oleDbCn As OleDb.OleDbConnection
        Dim oleDbTrans As OleDb.OleDbTransaction
        Dim oleDbCmd As New OleDb.OleDbCommand

        oleDbCn = GetOleDbConnection()
        oleDbTrans = oleDbCn.BeginTransaction()

        Try
            Dim sSql As String = ""
            Dim dt As New DataTable

            sSql = ""
            sSql += " SELECT FILENM, FILELEN, FILEBIN " + vbCrLf
            sSql += "  FROM  ACK_OCS" + vbCrLf
            sSql += " WHERE cucd = '45436' " + vbCrLf '병원구분코드(거래처코드)
            sSql += "   AND usab = 'SML'" + vbCrLf '결과입력자(SML = 삼광)
            sSql += "   AND isok = 'Y' " + vbCrLf '결과여부 (Y = 결과완료) 
            sSql += "   ORDER BY jsdt,kseq,hgcd" + vbCrLf

            With oleDbCmd
                .Connection = oleDbCn
                .Transaction = oleDbTrans
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()

                '.Parameters.Add("DATEF", OleDb.OleDbType.VarChar).Value = sSdate
                '.Parameters.Add("DATEE", OleDb.OleDbType.VarChar).Value = sEdate
                '.Parameters.Add("SELECTFLAG", OleDb.OleDbType.VarChar).Value = sDategbn

                Dim a_btReturn() As Byte

                Dim dbDr As OleDb.OleDbDataReader = oleDbCmd.ExecuteReader(CommandBehavior.SequentialAccess)

                Do While dbDr.Read()

                    Dim iStartIndex As Integer = 0
                    Dim lngReturn As Long = 0


                    Dim iBufferSize As Integer
                    Dim sFilePath As String

                    iBufferSize = Convert.ToInt32(dbDr.GetValue(0).ToString)


                    ribufferSize = iBufferSize

                    Dim a_btBuffer(iBufferSize - 1) As Byte
                    ReDim a_btBuffer(iBufferSize - 1)

                    iStartIndex = 0
                    lngReturn = dbDr.GetBytes(1, iStartIndex, a_btBuffer, 0, iBufferSize)

                    Do While lngReturn = iBufferSize
                        fnCopyToBytes(a_btBuffer, a_btReturn)


                        ReDim a_btBuffer(iBufferSize - 1)

                        iStartIndex += iBufferSize
                        lngReturn = dbDr.GetBytes(1, iStartIndex, a_btReturn, 0, iBufferSize)
                    Loop

                    sFilePath = dbDr.GetValue(3).ToString
                    rsFilePath = sFilePath
                Loop

                dbDr.Close()

                Return a_btReturn

            End With

            oleDbTrans.Commit()


        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            oleDbTrans.Rollback()
            Return Nothing
        End Try
    End Function

    Private Shared Function fnCopyToBytes(ByVal r_a_btFrom As Byte(), ByRef r_a_btTo As Byte()) As Boolean

        Try
            Dim iIndexDest As Integer = 0
            Dim iLength As Integer = 0

            If r_a_btTo Is Nothing Then
                iIndexDest = 0
            Else
                iIndexDest = r_a_btTo.Length
            End If

            iLength = r_a_btFrom.Length

            ReDim Preserve r_a_btTo(iIndexDest + iLength - 1)

            Array.Copy(r_a_btFrom, 0, r_a_btTo, iIndexDest, iLength)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Function

    Public Shared Function fnExe_Get_Data_SML(ByVal RsDateGbn As String, ByVal RsDateS As String, ByVal RsDateE As String) As DataTable
        Dim sFn As String = "Public Shared Function fnExe_UpLoad_SML() As String"
        Dim oleDbCn As OleDb.OleDbConnection
        Dim oleDbTrans As OleDb.OleDbTransaction
        Dim oleDbCmd As New OleDb.OleDbCommand


        oleDbCn = GetOleDbConnection()
        oleDbTrans = oleDbCn.BeginTransaction()
        Try
            Dim sSql As String = ""
            Dim iRet As Integer = 0
            Dim dt As New DataTable

            sSql = ""
            sSql += " SELECT  CUCD,  JSDT, KSEQ, HGCD, HGNM, KCCD, KCNM, CHNO, PTNM, JNID, " + vbCrLf
            sSql += "         SEXX , AGEE, MENM, WARD, JKNM, REDT, CHRT, RIMG, RMKK, LOHI, " + vbCrLf
            sSql += "         CHAM, IDAT, ITIM, ISOK, UDAT, UTIM, FILENM, FILELEN, LONGRESULT" + vbCrLf
            sSql += "  FROM  ACK_OCS" + vbCrLf
            sSql += " WHERE cucd = '45436' " + vbCrLf '병원구분코드(거래처코드)
            sSql += "   AND usab = 'SML'" + vbCrLf '결과입력자(SML = 삼광)
            sSql += "   AND isok = 'Y' " + vbCrLf '결과여부 (Y = 결과완료)    
            'sSql += "   AND isnull(filenm, '') = '' " + vbCrLf
            ' sSql += "   AND isnull(filelen, 0) = 0 " + vbCrLf
            If RsDateGbn = "1" Then '접수일자
                sSql += " AND idat between '" + RsDateS + "' and '" + RsDateE + "'"
            ElseIf RsDateGbn = "2" Then '보고일자
                sSql += " AND UDAT between '" + RsDateS + "' and '" + RsDateE + "'"
            End If
            sSql += "   ORDER BY jsdt,kseq,hgcd" + vbCrLf

            '*접수일자, 결과일자 구간 조건 추가해야 함.

            With oleDbCmd
                .Connection = oleDbCn
                .Transaction = oleDbTrans
                .CommandType = CommandType.Text
                .CommandText = sSql

                Dim lisdbDa As New OleDb.OleDbDataAdapter(oleDbCmd)

                dt.Reset()
                lisdbDa.Fill(dt)

                oleDbTrans.Commit()

            End With

            Return dt

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            oleDbTrans.Rollback()
            Return Nothing
        End Try
    End Function

    Public Shared Function fnExe_UpLoad_SML(ByVal arrRst As ArrayList) As String
        Dim sFn As String = "Public Shared Function fnExe_UpLoad_SML() As String"
        Dim oleDbCn As OleDb.OleDbConnection
        Dim oleDbTrans As OleDb.OleDbTransaction
        Dim oleDbCmd As New OleDb.OleDbCommand


        Dim ddate As Date = DateTime.Now

        oleDbCn = GetOleDbConnection()
        oleDbTrans = oleDbCn.BeginTransaction()
        Try
            Dim sSql As String = ""
            Dim iRet As Integer = 0
            For i As Integer = 0 To arrRst.Count - 1


                sSql = ""
                sSql += " INSERT INTO ACK_OCS ( CUCD, JSDT , KSEQ, HGCD, HGNM,  " + vbCrLf
                sSql += "                       KCCD, KCNM, CHNO, PTNM, JNID, " + vbCrLf
                sSql += "                       SEXX, AGEE, MENM, WARD, JKNM, " + vbCrLf
                sSql += "                       PIDT, IDAT, ITIM, ISAB) " + vbCrLf
                sSql += "             VALUES (  ? ,    ?,    ?,    ? ,   ?, " + vbCrLf
                sSql += "                       ?,     ?,    ?,    ?,    ?, " + vbCrLf
                sSql += "                       ?,     ?,    ?,    ?,    ?, " + vbCrLf
                sSql += "                       ?,     ?,    ?,    ? )" + vbCrLf

                With oleDbCmd
                    .Connection = oleDbCn
                    .Transaction = oleDbTrans
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("CUCD", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sCUCD
                    .Parameters.Add("JSDT", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sJSDT
                    .Parameters.Add("KSEQ", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sKSEQ
                    .Parameters.Add("HGCD", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sHGCD
                    .Parameters.Add("HGNM", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sHGNM
                    .Parameters.Add("KCCD", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sKCCD
                    .Parameters.Add("KCNM", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sKCNM
                    .Parameters.Add("CHNO", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sCHNO
                    .Parameters.Add("PTNM", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sPTNM
                    .Parameters.Add("JNID", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sJNID
                    .Parameters.Add("SEXX", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sSEXX
                    .Parameters.Add("AGEE", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sAGEE
                    .Parameters.Add("MENM", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sMENM
                    .Parameters.Add("WARD", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sWARD
                    .Parameters.Add("JKNM", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sJKNM
                    .Parameters.Add("PIDT", OleDb.OleDbType.VarChar).Value = CType(arrRst(i), SML_Data).sPIDT
                    .Parameters.Add("IDAT", OleDb.OleDbType.VarChar).Value = CStr(ddate.Date).Replace("-", "")
                    .Parameters.Add("ITIM", OleDb.OleDbType.VarChar).Value = CStr(ddate.Hour) + CStr(ddate.Minute) + CStr(ddate.Second)
                    .Parameters.Add("ISAB", OleDb.OleDbType.VarChar).Value = "ACK"



                    iRet = .ExecuteNonQuery()

                End With
            Next



            oleDbTrans.Commit()
            Return ""

        Catch ex As Exception
            oleDbTrans.Rollback()
            Fn.log(msFile & sFn, Err)
            Return ex.Message
        End Try
    End Function

    '-- JJH 녹십자 결과 불러오기
    Public Shared Function fnExe_Get_Data_GCL(ByVal RsDateGbn As String, ByVal RsDateS As String, ByVal RsDateE As String) As DataTable
        Dim sFn As String = "Public Shared Function fnExe_Get_Data_GCL() As String"
        
        Dim OraDbcn As OracleConnection
        Dim OraDbTrans As OracleTransaction
        Dim OraDbCmd As New OracleCommand
        Dim dbDa As New OracleDataAdapter
        Dim dt As New DataTable

        OraDbcn = GetDbConnection_GCRL() '녹십자 DB연결
        OraDbTrans = OraDbcn.BeginTransaction()

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += " SELECT A.REQNO, "  ' 녹십자 key
            sSql += "        A.SAMPLENO AS BCNO, A.CSTITEMCD AS TESTCD, A.SAMPLECD AS SPCCD, A.HOSNO AS REGNO, "
            sSql += "        A.PATNM, A.ITEMCD, A.CSTITEMNM AS TNMD, B.LABRES AS RST, A.REQDTE "
            sSql += "   FROM GCRL.UPLOADMST A, GCRL.VIEW_RESULT_NMC B "
            sSql += "  WHERE A.REQNO    = B.REQNO "
            sSql += "    AND A.ITEMCD   = B.ITEMCD "
            sSql += "    AND A.CSTCD    = '41666'"
            sSql += "    AND B.IMAGE_YN = 'N' "  '이미지결과 제외

            If RsDateGbn = "1" Then '접수일자
                sSql += " AND A.REQDTE between '" + RsDateS + "' and '" + RsDateE + "'"
            ElseIf RsDateGbn = "2" Then '보고일자
                sSql += " AND B.INPDTE between '" + RsDateS + "' and '" + RsDateE + "'"
            End If



            With OraDbCmd
                .Connection = OraDbcn
                .Transaction = OraDbTrans
                .CommandType = CommandType.Text
                .CommandText = sSql

                dbDa = New OracleDataAdapter(OraDbCmd)

                dt.Reset()
                dbDa.Fill(dt)

            End With

            Return dt

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            OraDbTrans.Rollback()
            Return New DataTable
        Finally
            OraDbTrans.Dispose() : OraDbTrans = Nothing
            If OraDbcn.State = ConnectionState.Open Then OraDbcn.Close()
            OraDbcn.Dispose() : OraDbcn = Nothing
        End Try
    End Function


    Public Shared Function fnExe_UpLoad_GCRL(ByVal arrRst As ArrayList) As String
        Dim sFn As String = "Public Shared Function fnExe_UpLoad_GCRL() As String"
        
        Dim OraDbcn As OracleConnection
        Dim OraDbTrans As OracleTransaction
        Dim OraDbCmd As New OracleCommand
        Dim dbDa As New OracleDataAdapter
        Dim dt As New DataTable

        Dim ddate As Date = DateTime.Now

        OraDbcn = GetDbConnection_GCRL()
        OraDbTrans = OraDbcn.BeginTransaction()

        Try
            Dim sSql As String = ""
            Dim iRet As Integer = 0
            For i As Integer = 0 To arrRst.Count - 1

                'sSql = ""
                'sSql += " SELECT * "
                'sSql += "   FROM GCRL.UPLOADMST"
                'sSql += "  WHERE REQDTE >= '20200601'"
                'sSql += "    AND cstcd = '40792'"
                'sSql += "    AND ROWNUM <= 10"

                sSql = ""
                sSql += " INSERT INTO GCRL.UPLOADMST "
                sSql += "            ( REQDTE,  CSTCD,  SAMPLENO,  CSTITEMCD,  CSTITEMNM,  HOSNO,  PATNM,  SAMPLECD,  SAMPLENM,  BIRDTE,  SEX,  HOSLOC,  HOSPLC,  SAMDTE,  NO,  SEQ,  DOCNM,  HOSPI_DOWN_YN )"
                sSql += "    VALUES  (:REQDTE, :CSTCD, :SAMPLENO, :CSTITEMCD, :CSTITEMNM, :HOSNO, :PATNM, :SAMPLECD, :SAMPLENM, :BIRDTE, :SEX, :HOSLOC, :HOSPLC, :SAMDTE, :NO, :SEQ, :DOCNM, :HOSPI_DOWN_YN )"
                '                      의뢰일  병원코드 검체번호    검사코드    검사명   등록번호 환자명   검체코드    검체명   주민번호  성별   병동    진료과   채취일  순번  순번  의사명  다운로드여부(IMG) 
                With OraDbCmd
                    .Connection = OraDbcn
                    .Transaction = OraDbTrans
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    'pk   REQDTE, CSTCD, SAMPLENO, CSTITEMCD, CSTITEMNM, HOSNO, PATNM, BIRDTE, SEX, SEQ
                    .Parameters.Add("REQDTE", OracleDbType.Char).Value = CStr(ddate.Date).Replace("-", "")
                    .Parameters.Add("CSTCD", OracleDbType.Char).Value = CType(arrRst(i), GCLAB_Data).sCSTCD
                    .Parameters.Add("SAMPLENO", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sSAMPLENO
                    .Parameters.Add("CSTITEMCD", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sCSTITEMCD
                    .Parameters.Add("CSTITEMNM", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sCSTITEMNM
                    .Parameters.Add("HOSNO", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sHOSNO
                    .Parameters.Add("PATNM", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sPATNM
                    .Parameters.Add("SAMPLECD", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sSAMPLECD
                    .Parameters.Add("SAMPLENM", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sSAMPLENM
                    .Parameters.Add("BIRDTE", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sBIRDTE
                    .Parameters.Add("SEX", OracleDbType.Char).Value = CType(arrRst(i), GCLAB_Data).sSEX
                    .Parameters.Add("HOSLOC", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sHOSLOC
                    .Parameters.Add("HOSPLC", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sHOSPLC
                    .Parameters.Add("SAMDTE", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sSAMDTE
                    .Parameters.Add("NO", OracleDbType.Varchar2).Value = "00"
                    .Parameters.Add("SEQ", OracleDbType.Varchar2).Value = "00"
                    .Parameters.Add("DOCNM", OracleDbType.Varchar2).Value = CType(arrRst(i), GCLAB_Data).sDOCNM
                    .Parameters.Add("HOSPI_DOWN_YN", OracleDbType.Char).Value = "N"
                    '.Parameters.Add("SAMTME", OracleDbType.Char).Value = CType(arrRst(i), GCLAB_Data).sSAMTME

                    iRet = .ExecuteNonQuery()

                End With


            Next

            OraDbTrans.Commit()

            Return ""

        Catch ex As Exception
            OraDbTrans.Rollback()
            Fn.log(msFile & sFn, Err)
            Return ex.Message
        Finally
            OraDbTrans.Dispose() : OraDbTrans = Nothing
            If OraDbcn.State = ConnectionState.Open Then OraDbcn.Close()
            OraDbcn.Dispose() : OraDbcn = Nothing
        End Try
    End Function

    Public Shared Function fnExe_UpLoad(ByVal rsExLabCd As String, ByVal rsFileNm As String, ByVal rsUsrId As String, ByVal rsCmtCont As String, ByVal raData As ArrayList) As String
        Dim sFn As String = "Function fnExe_UpLoad(arraylist) As DataTable"

        Dim dbCn As OracleConnection
        Dim dbTran As OracleTransaction
        Dim dbCmd As New OracleCommand

        dbCn = GetDbConnection()
        dbTran = dbCn.BeginTransaction()
        COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

        Try
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "UPDATE lre10m SET regid = :regid, regdt = fn_ack_sysdate"
            sSql += " WHERE exlabcd = :exlabcd"
            sSql += "   AND filenm  = :filenm"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm

                iRet = .ExecuteNonQuery()
            End With

            If iRet = 0 Then
                sSql = ""
                sSql += "INSERT INTO lre10m( exlabcd, filenm, fregdt, regdt, regid)"
                sSql += "    VALUES( :exlabcd, :filenm, fn_ack_sysdate, fn_ack_sysdate, :regid)"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm
                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId

                    iRet = .ExecuteNonQuery()
                End With

                If iRet = 0 Then
                    dbTran.Rollback()
                    Return "[오류] LRE10M 데이블에 입력하지 못 했습니다.!!"
                End If
            End If

            '<<<20180402 SCL 위탁접수시 2번씩 올리는데 삭제하면 조회가 안되서 수정 
            'sSql = ""
            'sSql += "DELETE lre11m"
            'sSql += " WHERE exlabcd = :exlabcd"
            'sSql += "   AND filenm  = :filenm"

            'With dbCmd
            '    .CommandType = CommandType.Text
            '    .CommandText = sSql

            '    .Parameters.Clear()
            '    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
            '    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm

            '    iRet = .ExecuteNonQuery()
            'End With

            'sSql = ""
            'sSql += "DELETE lre12m"
            'sSql += " WHERE exlabcd = :exlabcd"
            'sSql += "   AND filenm  = :filenm"

            'With dbCmd
            '    .CommandType = CommandType.Text
            '    .CommandText = sSql

            '    .Parameters.Clear()
            '    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
            '    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm

            '    iRet = .ExecuteNonQuery()
            'End With

            For intIdx As Integer = 0 To raData.Count - 1
                Dim strBcNo As String = raData.Item(intIdx).ToString.Split("|"c)(0)
                Dim strTclsCd As String = raData.Item(intIdx).ToString.Split("|"c)(1)
                Dim strSpcCd As String = raData.Item(intIdx).ToString.Split("|"c)(2)
                Dim strRemark As String = raData.Item(intIdx).ToString.Split("|"c)(3)

                sSql = ""
                sSql += "INSERT INTO lre11m( exlabcd, filenm, bcno, testcd, spccd, remark, regdt)"
                sSql += "    VALUES( :exlabcd, :filenm, :bcno, :testcd, :spccd, :remark, fn_ack_sysdate)"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = strBcNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = strTclsCd
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = strSpcCd
                    .Parameters.Add("remark", OracleDbType.Varchar2).Value = strRemark

                    iRet = .ExecuteNonQuery()
                End With
            Next

            If rsCmtCont <> "" Then
                sSql = ""
                sSql += "INSERT INTO lre12m( exlabcd, filenm, cmtcont, regdt )"
                sSql += "    VALUES( :exlabcd, :filenm, :cmtcont, fn_ack_sysdate )"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = rsFileNm
                    .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = rsCmtCont

                    iRet = .ExecuteNonQuery()
                End With
            End If

            dbTran.Commit()
            Return ""

        Catch ex As Exception
            dbTran.Rollback()
            Return ex.Message
        Finally
            dbTran.Dispose() : dbTran = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Shared Function fnExe_UpLoad_Del(ByVal rsExLabCd As String, ByVal rsUsrId As String, ByVal rsCmtCont As String, ByVal raData As ArrayList) As String
        Dim sFn As String = "Function fnExe_UpLoad(arraylist) As DataTable"

        Dim dbcn As OracleConnection
        Dim dbTran As OracleTransaction
        Dim dbCmd As New OracleCommand
        Dim dbDa As OracleDataAdapter

        dbcn = GetDbConnection()
        dbTran = dbcn.BeginTransaction()
        COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

        Try
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim dt As New DataTable
            Dim strDate As String = ""
            Dim arlFileNms As New ArrayList


            sSql = ""
            sSql += "SELECT fn_ack_sysdate srvdate FROM DUAL"

            dbCmd.Connection = dbcn
            dbCmd.Transaction = dbTran
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                strDate = dt.Rows(0).Item("srvdate").ToString()
            Else
                strDate = Format(Now, "yyyyMMddHHmmss").ToString
            End If

            For intIdx As Integer = 0 To raData.Count - 1
                Dim strBcNo As String = raData.Item(intIdx).ToString.Split("|"c)(0)
                Dim strTclsCd As String = raData.Item(intIdx).ToString.Split("|"c)(1)
                Dim strSpcCd As String = raData.Item(intIdx).ToString.Split("|"c)(2)
                Dim strRemark As String = raData.Item(intIdx).ToString.Split("|"c)(3)
                Dim strFileNm As String = raData.Item(intIdx).ToString.Split("|"c)(4)

                sSql = ""
                sSql += "DELETE lre11m"
                sSql += " WHERE exlabcd = :exlabcd"
                sSql += "   AND filenm  = :filenm"
                sSql += "   AND bcno    = :bcno"
                sSql += "   AND testcd  = :testcd"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = strFileNm
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = strBcNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = strTclsCd

                    iRet = .ExecuteNonQuery()
                End With

                If arlFileNms.Contains(strFileNm) = False Then arlFileNms.Add(strFileNm)
            Next

            For intIdx As Integer = 0 To arlFileNms.Count - 1
                sSql = ""
                sSql += "DELETE lre12m"
                sSql += " WHERE exlabcd = :exlabcd"
                sSql += "   AND filenm  = :filenm"
                sSql += "   AND (SELECT count(*) FROM lre11m WHERE exlabcd = :exlabcd AND filenm = :filenm) = 0"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = arlFileNms.Item(intIdx).ToString
                    .Parameters.Add("exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = arlFileNms.Item(intIdx).ToString

                    iRet = .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += "DELETE lre10m"
                sSql += " WHERE exlabcd = :exlabcd"
                sSql += "   AND filenm  = :filenm"
                sSql += "   AND (SELECT count(*) FROM lre11m WHERE exlabcd = :exlabcd AND filenm = :filenm) = 0"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(":exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = arlFileNms.Item(intIdx).ToString
                    .Parameters.Add(":exlabcd", OracleDbType.Varchar2).Value = rsExLabCd
                    .Parameters.Add("filenm", OracleDbType.Varchar2).Value = arlFileNms.Item(intIdx).ToString

                    iRet = .ExecuteNonQuery()
                End With
            Next
            dbTran.Commit()
            Return ""

        Catch ex As Exception
            dbTran.Rollback()
            Return ex.Message
        Finally
            dbTran.Dispose() : dbTran = Nothing
            If dbcn.State = ConnectionState.Open Then dbcn.Close()
            dbcn.Dispose() : dbcn = Nothing

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Shared Function fnGet_UpLoad_FileList(ByVal rsDateS As String, ByVal rsDateE As String) As DataTable
        Dim sFn As String = "Function fnGet_UpLoad_List(string, string, string) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql = ""
            sSql += "SELECT a.exlabcd, a.filenm,"
            sSql += "       fn_ack_get_usr_name(a.regid) regnm,"
            sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd') regdt,"
            sSql += "       (SELECT exlabnmd FROM lf050m WHERE exlabcd = a.exlabcd) exlabnmd"
            sSql += "  FROM lre10m a"
            sSql += " WHERE a.regdt >= :dates"
            sSql += "   AND a.regdt <= :datee || '5959'"
            sSql += " ORDER BY a.regdt"

            alParm.Add(New OracleParameter("dates", rsDateS))
            alParm.Add(New OracleParameter("datee", rsDateE))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_UpLoad_List(ByVal rsExLabCd As String, ByVal rsFileNm As String) As DataTable
        Dim sFn As String = "Function fnGet_UpLoad_List(string, string, string) As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql = ""
            sSql += "SELECT e11.bcno, e11.testcd, e11.spccd, f6.tnmd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e11.remark, e11.filenm, e12.cmtcont, r.rstflg"
            sSql += "  FROM lj010m j, lj011m j1, lr010m r,"
            sSql += "       lf060m f6, lf030m f3,"
            sSql += "       lre11m e11, lre12m e12"
            sSql += " WHERE e11.exlabcd = :exlabcd"
            sSql += "   AND e11.filenm  = :filenm"
            sSql += "   AND e11.bcno    = r.bcno"
            sSql += "   AND e11.testcd  = r.testcd"
            sSql += "   AND e11.spccd   = r.spccd"
            sSql += "   AND j.bcno      = j1.bcno"
            sSql += "   AND j1.bcno     = r.bcno"
            sSql += "   AND j1.tclscd   = r.tclscd"
            sSql += "   AND r.testcd    = f6.testcd"
            sSql += "   AND r.spccd     = f6.spccd"
            sSql += "   AND f6.usdt    <= r.tkdt"
            sSql += "   AND f6.uedt    >  r.tkdt"
            sSql += "   AND r.spccd     = f3.spccd"
            sSql += "   AND f3.usdt    <= r.tkdt"
            sSql += "   AND f3.uedt    > r.tkdt"
            sSql += "   AND e11.exlabcd = e12.exlabcd (+)"
            sSql += "   AND e11.filenm  = e12.filenm (+)"

            alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            alParm.Add(New OracleParameter("filenm", OracleDbType.Varchar2, rsFileNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFileNm))

            sSql += " UNION "
            sSql += "SELECT e11.bcno, e11.testcd, e11.spccd, f6.tnmd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e11.remark, e11.filenm, e12.cmtcont, r.rstflg"
            sSql += "  FROM lj010m j, lj011m j1, lm010m r,"
            sSql += "       lf060m f6, lf030m f3,"
            sSql += "       lre11m e11, lre12m e12"
            sSql += " WHERE e11.exlabcd = :exlabcd"
            sSql += "   AND e11.filenm  = :filenm"
            sSql += "   AND e11.bcno    = r.bcno"
            sSql += "   AND e11.testcd  = r.testcd"
            sSql += "   AND e11.spccd   = r.spccd"
            sSql += "   AND j.bcno      = j1.bcno"
            sSql += "   AND j1.bcno     = r.bcno"
            sSql += "   AND j1.tclscd   = r.tclscd"
            sSql += "   AND r.testcd    = f6.testcd"
            sSql += "   AND r.spccd     = f6.spccd"
            sSql += "   AND f6.usdt    <= r.tkdt"
            sSql += "   AND f6.uedt    >  r.tkdt"
            sSql += "   AND r.spccd     = f3.spccd"
            sSql += "   AND f3.usdt    <= r.tkdt"
            sSql += "   AND f3.uedt    > r.tkdt"
            sSql += "   AND e11.exlabcd = e12.exlabcd (+)"
            sSql += "   AND e11.filenm  = e12.filenm (+)"

            alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            alParm.Add(New OracleParameter("filenm", OracleDbType.Varchar2, rsFileNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFileNm))

            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_UpLoad_List(ByVal rsExLabCd As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsRegNo As String) As DataTable
        Dim sFn As String = "Function fnGet_UpLoad_List(string, string, string) As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT e1.bcno, e1.testcd, e1.spccd, f6.tnmd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.remark, e12.cmtcont, r.rstflg,"
            sSql += "       (SELECT MAX(regdt) FROM lre11m WHERE bcno = r.bcno AND testcd = r.testcd) regdt"
            sSql += "       , case nvl(f6.cprtcd, ' ') when ' '   then '' "
            sSql += "                                   else 'Y' "
            sSql += "         end as imgyn    "
            sSql += "  FROM lj010m j,  lj011m j1, lr010m r,"
            sSql += "       lf060m f6, lf030m f3,"
            sSql += "       lre11m e1, lre10m e, lre12m e12"
            sSql += " WHERE e.regdt  >= :dates"
            sSql += "   AND e.regdt  <= :datee || '5959'"
            sSql += "   AND e.exlabcd = e1.exlabcd"
            sSql += "   AND e.filenm  = e1.filenm"
            sSql += "   AND e1.bcno   = r.bcno"
            sSql += "   AND e1.testcd = r.testcd"
            sSql += "   AND e1.spccd  = r.spccd"
            sSql += "   AND j.bcno    = j1.bcno"
            sSql += "   AND j1.bcno   = r.bcno"
            sSql += "   AND j1.tclscd = r.tclscd"
            sSql += "   AND r.testcd  = f6.testcd"
            sSql += "   AND r.spccd   = f6.spccd"
            sSql += "   AND f6.usdt  <= r.tkdt"
            sSql += "   AND f6.uedt  >  r.tkdt"
            sSql += "   AND r.spccd   = f3.spccd"
            sSql += "   AND f3.usdt  <= r.tkdt"
            sSql += "   AND f3.uedt  >  r.tkdt"
            sSql += "   AND e.exlabcd = e12.exlabcd (+) "
            sSql += "   AND e.filenm  = e12.filenm (+)"

            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

            If rsExLabCd <> "" Then
                sSql += "   AND e.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            End If

            If rsRegNo <> "" Then
                sSql += "   and j.regno = :regno"
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            End If

            sSql += " UNION "
            sSql += "SELECT e1.bcno, e1.testcd, e1.spccd, f6.tnmd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.remark, e12.cmtcont, r.rstflg,"
            sSql += "       (SELECT MAX(regdt) FROM lre11m WHERE bcno = r.bcno AND testcd = r.testcd)regdt"
            sSql += "       , case nvl(f6.cprtcd, ' ') when ' '   then '' "
            sSql += "                                   else 'Y' "
            sSql += "         end as imgyn    "
            sSql += "  FROM lj010m j,  lj011m j1, lm010m r,"
            sSql += "       lf060m f6, lf030m f3,"
            sSql += "       lre11m e1,"
            sSql += "       lre10m e,"
            sSql += "       lre12m e12"
            sSql += " WHERE e.regdt  >= :dates"
            sSql += "   AND e.regdt  <= :datee || '5959'"
            sSql += "   AND e.exlabcd = e1.exlabcd"
            sSql += "   AND e.filenm  = e1.filenm"
            sSql += "   AND e1.bcno   = r.bcno"
            sSql += "   AND e1.testcd = r.testcd"
            sSql += "   AND e1.spccd  = r.spccd"
            sSql += "   AND j.bcno    = j1.bcno"
            sSql += "   AND j1.bcno   = r.bcno"
            sSql += "   AND j1.tclscd = r.tclscd"
            sSql += "   AND r.testcd  = f6.testcd"
            sSql += "   AND r.spccd   = f6.spccd"
            sSql += "   AND f6.usdt  <= r.tkdt"
            sSql += "   AND f6.uedt  >  r.tkdt"
            sSql += "   AND r.spccd   = f3.spccd"
            sSql += "   AND f3.usdt  <= r.tkdt"
            sSql += "   AND f3.uedt  >  r.tkdt"
            sSql += "   AND e.exlabcd = e12.exlabcd (+)"
            sSql += "   AND e.filenm  = e12.filenm (+)"

            alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

            If rsExLabCd <> "" Then
                sSql += "   AND e.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            End If
            If rsRegNo <> "" Then
                sSql += "   and j.regno = :regno"
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            End If

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_SpcInfo_ExLab(ByVal rsExLabCd As String, ByVal rsBcclsCd As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, ByVal rbFlagAll As Boolean) As DataTable

        Dim sFn As String = "Function fnGet_SpcInfo_ExLab(string, string, string, string, boolean) As DataTable"
        Try
            Dim sSql As String
            Dim alParm As New ArrayList

            rsExLabCd = rsExLabCd.Replace("000", "")

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       j.bcno, r.testcd, f6.tnmd, r.spccd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.filenm, e1.remark,"
            sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
            sSql += "       NVL(f6.dispseqO, 999) sort2, SUBSTR(r.tkdt,1,8) tkdt,"
            sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm"
            sSql += "       ,fn_ack_exlab_imgyn(r.testcd, r.spccd, r.tkdt) as imgyn , j.age "
            'sSql += "       , case nvl(f6.cprtcd, ' ') when ' '   then '' "
            'sSql += "                                   else 'Y' "
            'sSql += "         end as imgyn    "
            sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf030m f3,"
            If rbFlagAll Then
                sSql += "       lr010m r,"
            Else
                sSql += "       (SELECT bcno, tclscd, testcd, spccd, tkdt, rstflg FROM lr010m"
                sSql += "          where tkdt >= :dates"
                sSql += "            AND tkdt <= :datee || '5959'"
                sSql += "            AND (bcno, testcd) NOT IN"
                sSql += "                (SELECT r.bcno, r.testcd FROM lr010m r, lre11m e"
                sSql += "                  WHERE r.tkdt  >= :dates"
                sSql += "                    AND r.tkdt  <= :datee || '5959'"
                sSql += "                    AND r.bcno   = e.bcno"
                sSql += "                    AND r.testcd = e.testcd"
                sSql += "                )"
                sSql += "       ) r,"

                alParm.Add(New OracleParameter("dates", rsTkDtS))
                alParm.Add(New OracleParameter("datee", rsTkDtE))

                alParm.Add(New OracleParameter("dates", rsTkDtS))
                alParm.Add(New OracleParameter("datee", rsTkDtE))
            End If

            sSql += "       lre11m e1"
            sSql += " WHERE r.tkdt    >= :dates"
            sSql += "   AND r.tkdt    <= :datee || '5959'"
            sSql += "   AND j.bcno     = j1.bcno"
            sSql += "   AND j1.bcno    = r.bcno"
            sSql += "   AND j1.tclscd  = r.tclscd"
            sSql += "   AND r.testcd   = f6.testcd"
            sSql += "   AND r.spccd    = f6.spccd"
            sSql += "   AND r.tkdt    >= f6.usdt"
            sSql += "   AND r.tkdt    <  f6.uedt"
            sSql += "   AND f6.exlabyn = '1'"
            sSql += "   AND f6.tcdgbn <> 'C'"
            sSql += "   AND r.spccd    = f3.spccd"
            sSql += "   AND r.tkdt    >= f3.usdt"
            sSql += "   AND r.tkdt    <  f3.uedt"
            sSql += "   AND NVL(r.rstflg, '0') IN ('', '0')"
            sSql += "   AND j.spcflg = '4'"
            sSql += "   AND j1.spcflg = '4'"
            sSql += "   AND r.bcno    = e1.bcno (+)"
            sSql += "   AND r.testcd  = e1.testcd (+)"
            '<삼광 테스트


            alParm.Add(New OracleParameter("dates", rsTkDtS))
            alParm.Add(New OracleParameter("datee", rsTkDtE))

            If rsBcclsCd <> "" Then
                sSql += "   and f6.bcclscd = :bcclscd"
                alParm.Add(New OracleParameter("bcclscd", rsBcclsCd))
            End If

            If rsExLabCd <> "" Then
                sSql += "   and f6.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", rsExLabCd))
            End If

            sSql += " UNION "
            sSql += "SELECT DISTINCT"
            sSql += "       j.bcno, r.testcd, f6.tnmd, r.spccd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.filenm, e1.remark,"
            sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
            sSql += "       NVL (f6.dispseqO, 999) sort2, SUBSTR(r.tkdt,1,8) tkdt,"
            sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm"
            sSql += "       ,fn_ack_exlab_imgyn(r.testcd, r.spccd, r.tkdt) as imgyn, j.age "
            'sSql += "       , case nvl(f6.cprtcd, ' ') when ' '   then '' "
            'sSql += "                                   else 'Y' "
            'sSql += "         end as imgyn    "
            sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf030m f3,"
            If rbFlagAll Then
                sSql += "       lm010m r,"
            Else
                sSql += "       (SELECT bcno, tclscd, testcd, spccd, tkdt, rstflg FROM lm010m"
                sSql += "          where tkdt >= :dates"
                sSql += "            AND tkdt <= :datee || '5959'"
                sSql += "            AND (bcno, testcd) NOT IN"
                sSql += "                (SELECT r.bcno, r.testcd FROM lm010m r, lre11m e"
                sSql += "                  WHERE r.tkdt  >= :dates"
                sSql += "                    AND r.tkdt  <= :datee || '5959'"
                sSql += "                    AND r.bcno   = e.bcno"
                sSql += "                    AND r.testcd = e.testcd"
                sSql += "                )"
                sSql += "       ) r,"

                alParm.Add(New OracleParameter("dates", rsTkDtS))
                alParm.Add(New OracleParameter("datee", rsTkDtE))

                alParm.Add(New OracleParameter("dates", rsTkDtS))
                alParm.Add(New OracleParameter("datee", rsTkDtE))
            End If

            sSql += "       lre11m e1"
            sSql += " WHERE r.tkdt    >= :dates"
            sSql += "   AND r.tkdt    <= :datee || '5959'"
            sSql += "   AND j.bcno     = j1.bcno"
            sSql += "   AND j1.bcno    = r.bcno"
            sSql += "   AND j1.tclscd  = r.tclscd"
            sSql += "   AND r.testcd   = f6.testcd"
            sSql += "   AND r.spccd    = f6.spccd"
            sSql += "   AND r.tkdt    >= f6.usdt"
            sSql += "   AND r.tkdt    <  f6.uedt"
            sSql += "   AND f6.exlabyn = '1'"
            sSql += "   AND f6.tcdgbn <> 'C'"
            sSql += "   AND r.spccd    = f3.spccd"
            sSql += "   AND r.tkdt    >= f3.usdt"
            sSql += "   AND r.tkdt    <  f3.uedt"
            sSql += "   AND NVL(r.rstflg, '0') IN ('', '0')"
            sSql += "   AND j.spcflg  = '4'"
            sSql += "   AND j1.spcflg = '4'"
            sSql += "   AND r.bcno    = e1.bcno (+)"
            sSql += "   AND r.testcd  = e1.testcd (+)"
            '<삼광 테스트


            alParm.Add(New OracleParameter("dates", rsTkDtS))
            alParm.Add(New OracleParameter("datee", rsTkDtE))

            If rsBcclsCd <> "" Then
                sSql += "   and f6.bcclscd = :bcclscd"
                alParm.Add(New OracleParameter("bcclscd", rsBcclsCd))
            End If

            If rsExLabCd <> "" Then
                sSql += "   and f6.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", rsExLabCd))
            End If

            sSql += " order by tkdt, bcno, sort1, sort2, testcd"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_SpcInfo_ExLab(ByVal rsExLabCd As String, ByVal rsBcclsCd As String, ByVal rsBcNo As String, ByVal rbFlagAll As Boolean) As DataTable

        Dim sFn As String = "Function fnGet_SpcInfo_ExLab(string, string, string, boolean) As DataTable"
        Try
            Dim sSql As String
            Dim alParm As New ArrayList

            rsExLabCd = rsExLabCd.Replace("000", "")

            sSql = ""
            sSql += "SELECT DISTINCT"
            sSql += "       j.bcno, r.testcd, f6.tnmd, r.spccd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.filenm, e1.remark,"
            'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.slipcd, r.tkdt) sort1,"
            sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
            sSql += "       NVL(f6.dispseqO, 999) sort2"
            sSql += "       ,fn_ack_get_dr_name(j.doctorcd) doctornm"
            sSql += "       , case nvl(f6.cprtcd, ' ') when ' '   then '' "
            sSql += "                                   else 'Y' "
            sSql += "         end as imgyn , j.age   "
            sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf030m f3,"
            If rbFlagAll Then
                sSql += "       lr010m r,"
            Else
                sSql += "       (SELECT bcno, tclscd, testcd, spccd, tkdt, rstflg FROM lr010m"
                sSql += "          where bcno = :bcno"
                sSql += "            AND (bcno + testcd) NOT IN"
                sSql += "                (SELECT r.bcno + r.testcd FROM lr010m r, lre11m e"
                sSql += "                  WHERE r.bcno   = :bcno"
                sSql += "                    AND r.bcno   = e.bcno"
                sSql += "                    AND r.testcd = e.testcd"
                sSql += "                )"
                sSql += "       ) r,"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            End If

            sSql += "       lre11m e1"
            sSql += " WHERE r.bcno     = :bcno"
            sSql += "   AND j.bcno     = j1.bcno"
            sSql += "   AND j1.bcno    = r.bcno"
            sSql += "   AND j1.tclscd  = r.tclscd"
            sSql += "   and r.testcd   = f6.testcd"
            sSql += "   and r.spccd    = f6.spccd"
            sSql += "   and r.tkdt    >= f6.usdt"
            sSql += "   and r.tkdt    <  f6.uedt"
            sSql += "   and f6.exlabyn = '1'"
            sSql += "   aND f6.tcdgbn <> 'C'"
            sSql += "   and r.spccd    = f3.spccd"
            sSql += "   and r.tkdt    >= f3.usdt"
            sSql += "   and r.tkdt    <  f3.uedt"
            sSql += "   and NVL(r.rstflg, '0') IN ('', '0')"
            sSql += "   and j.spcflg  = '4'"
            sSql += "   and j1.spcflg = '4'"
            sSql += "   AND r.bcno    = e1.bcno (+)"
            sSql += "   AND r.testcd  = e1.testcd (+)"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            If rsBcclsCd <> "" Then
                sSql += "   and f6.bcclscd = :bcclscd"
                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
            End If

            If rsExLabCd <> "" Then
                sSql += "   and f6.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            End If

            sSql += " UNION "
            sSql += "SELECT DISTINCT"
            sSql += "       j.bcno, r.testcd, f6.tnmd, r.spccd, f3.spcnmd, j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
            sSql += "       j.wardno, j.deptcd, SUBSTR(j1.colldt, 1, 8) colldt, e1.filenm, e1.remark,"
            'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.slipcd, r.tkdt) sort1,"
            sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
            sSql += "       NVL(f6.dispseqO, 999) sort2"
            sSql += "       ,fn_ack_get_dr_name(j.doctorcd) doctornm"
            sSql += "       , case nvl(f6.cprtcd, ' ') when ' '   then '' "
            sSql += "                                   else 'Y' "
            sSql += "         end as imgyn , j.age   "
            sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf030m f3,"
            If rbFlagAll Then
                sSql += "       lm010m r,"
            Else
                sSql += "       (SELECT bcno, tclscd, testcd, spccd, tkdt, rstflg FROM lm010m"
                sSql += "          where bcno = :bcno"
                sSql += "            AND (bcno + testcd) NOT IN"
                sSql += "                (SELECT r.bcno + r.testcd FROM lm010m r, lre11m e"
                sSql += "                  WHERE r.bcno   = :bcno"
                sSql += "                    AND r.bcno   = e.bcno"
                sSql += "                    AND r.testcd = e.testcd"
                sSql += "                )"
                sSql += "       ) r,"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            End If

            sSql += "       lre11m e1"
            sSql += " WHERE r.bcno     = :bcno"
            sSql += "   AND j.bcno     = j1.bcno"
            sSql += "   AND j1.bcno    = r.bcno"
            sSql += "   AND j1.tclscd  = r.tclscd"
            sSql += "   and r.testcd   = f6.testcd"
            sSql += "   and r.spccd    = f6.spccd"
            sSql += "   and r.tkdt    >= f6.usdt"
            sSql += "   and r.tkdt    <  f6.uedt"
            sSql += "   and f6.exlabyn = '1'"
            sSql += "   aND f6.tcdgbn <> 'C'"
            sSql += "   and r.spccd    = f3.spccd"
            sSql += "   and r.tkdt    >= f3.usdt"
            sSql += "   and r.tkdt    <  f3.uedt"
            sSql += "   and NVL(r.rstflg, '0') IN ('', '0')"
            sSql += "   and j.spcflg = '4'"
            sSql += "   and j1.spcflg = '4'"
            sSql += "   AND r.bcno    = e1.bcno (+)"
            sSql += "   AND r.testcd  = e1.testcd (+)"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            If rsBcclsCd <> "" Then
                sSql += "   and f6.bcclscd = :bcclscd"
                alParm.Add(New OracleParameter("bcclscd", OracleDbType.Varchar2, rsBcclsCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcclsCd))
            End If

            If rsExLabCd <> "" Then
                sSql += "   and f6.exlabcd = :exlabcd"
                alParm.Add(New OracleParameter("exlabcd", OracleDbType.Varchar2, rsExLabCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsExLabCd))
            End If

            sSql += " order by bcno, sort1, sort2, testcd"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_SpcInfo(ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "Public fnGet_SpcInfo(String, String) As DataTable"

        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim sTableNm As String = "lr010m"

            If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

            sSql += "SELECT r.bcno, r.spccd, r.rstflg, r.orgrst, r.regno, j.spcflg, f.tnmd, NVL(f.titleyn, '0') titleyn, f.tcdgbn,"
            sSql += "       f.partcd || f.slipcd partslip"
            sSql += "  FROM " + sTableNm + " r, lj010m j, lf060m f"
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd = :testcd"
            sSql += "   AND r.bcno   = j.bcno"
            sSql += "   AND r.testcd = f.testcd"
            sSql += "   AND r.spccd  = f.spccd"
            sSql += "   AND r.tkdt  >= f.usdt"
            sSql += "   AND r.tkdt  <  f.uedt"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
            alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Shared Function fnGet_ExLab_ImgYn(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsTkdt As String) As DataTable
        Dim sFn As String = "Public Shared Function fnGet_ExLab_ImgYn() As DataTable"
        Dim sSql As String = ""
        Dim alParm As New ArrayList
        Try
            sSql = ""
            sSql += "  select NVL2(f.testcd , 'Y','N')  imgyn    "
            sSql += "    from lf060m f , lf310m s"
            sSql += "  where f.testcd = :testcd"
            sSql += "    and f.spccd = :spccd"
            sSql += "    and f.usdt <= :tkdt         "
            sSql += "    and f.uedt >= :tkdt"
            sSql += "    and NVL(f.exlabcd, ' ') <> ' '"
            sSql += "    and nvl(f.cprtcd , ' ') <> ' '"
            sSql += "    and f.ctgbn = '1'"
            sSql += "    and s.testcd = f.testcd "
            sSql += "    and stsubseq = '1'  "

            alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
            alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
            alParm.Add(New OracleParameter("tkdt", OracleDbType.Varchar2, rsTkdt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkdt + "000000"))
            alParm.Add(New OracleParameter("tkdt", OracleDbType.Varchar2, rsTkdt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkdt + "000000"))

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function




    Public Shared Function fnGet_PatInfo_IdNo(ByVal rsBcNo As String) As String

        Dim sFn As String = "Function fnGet_PatInfo_IdNo(string, string) As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT fn_ack_get_pat_info(regno, '', '') patinfo"
            sSql += "  FROM lj010m "
            sSql += " WHERE bcno = :bcno"

            alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

            DbCommand()
            Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

            If dt.Rows.Count > 0 Then
                Dim sPatInfo() = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

                Return sPatInfo(6) + sPatInfo(7)
            End If

            Return ""

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function


    Public Shared Function fnGet_PartSlip_ExLab() As DataTable
        Dim sFn As String = "Function fnGet_PartSlip_ExLab(string, string) As DataTable"
        Try
            Dim sSql As String = ""

            sSql += "SELECT f2.partcd || f2.slipcd partslip, f2.slipnmd"
            sSql += "  FROM lf021m f2, lf060m f6"
            sSql += " WHERE f2.partcd  = f6.partcd"
            sSql += "   AND f2.slipcd  = f6.slipcd"
            sSql += "   AND f2.usdt   <= fn_ack_sysdate"
            sSql += "   AND f2.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.exlabyn = '1'"

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function

End Class

