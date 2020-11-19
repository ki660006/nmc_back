Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.SVar
Public Class FGSMSSEND
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGSMSSEND.vb, Class : FGSMSSEND" + vbTab

    Private mbLoad As Boolean = False
    Private msBcNo As String = ""
    Private msDoctorCd As String = ""
    Private msRegno As String = ""
    Private mbSave As Boolean = False
    Private msLisseq As String = ""
    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal rsBcNo As String, ByVal rsSMSCont As String, Optional ByVal rslisseq As String = "0") As Boolean
        Dim sFn As String = "Function Display_Result"

        msBcNo = rsBcNo
        Me.txtBcno.Text = msBcNo
        Me.txtSmsCont.Text = rsSMSCont
        Me.txtLisseq.Text = rslisseq
        msLisseq = rslisseq
        Try

            sbDisplay_dept()
            sbDisplay_Data()
            sbDisplay_drInfo() '<20150120 

            mbLoad = True

            Me.ShowDialog(r_frm)

            Dim bOk As Boolean = False

            If mbSave Then
                bOk = True
            End If

            Return bOk

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function


    Private Sub sbDisplay_Data()

        With Me.spdDrInfo
            .Row = 1
            .Col = .GetColFromID("chk") : .Text = "1"
        End With

        Try
            Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SMS_DrInof(msBcNo)

            If dt.Rows.Count < 1 Then Return

            Me.txtUsrNm.Text = Login.USER_INFO.USRNM
            'Me.txtTelno.Text = dt.Rows(0).Item("telno").ToString.Trim

            msDoctorCd = dt.Rows(0).Item("doctorcd").ToString.Trim
            msRegno = dt.Rows(0).Item("regno").ToString.Trim

            For ix As Integer = 0 To Me.cboDept.Items.Count - 1
                Me.cboDept.SelectedIndex = ix
                If Me.cboDept.Text.IndexOf(dt.Rows(0).Item("deptcd").ToString.Trim) >= 0 Then
                    Exit For
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sbDisplay_dept()
        Dim sFn As String = "sbDisplay_dept"

        Try
            Dim dt As DataTable

            dt = OCSAPP.OcsLink.SData.fnGet_DeptList

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboDept.Items.Add("[" + dt.Rows(i).Item("deptcd").ToString.Trim + "]" + " " + dt.Rows(i).Item("deptnm").ToString.Trim)
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_drInfo()
        Dim sFn As String = "sbDisplay_drinfo"

        Try
            Me.spdDrInfo.MaxRows = 0

            'Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DoctorList(Ctrl.Get_Code(Me.cboDept), msDoctorCd)
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DoctorList("", msDoctorCd)

            If dt.Rows.Count < 1 Then
                Me.spdDrInfo.MaxRows = 1
                Return
            End If

            With Me.spdDrInfo
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("drcd") : .Text = dt.Rows(ix).Item("doctorcd").ToString.Trim
                    .Col = .GetColFromID("drnm") : .Text = dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("drtel") : .Text = dt.Rows(ix).Item("doctortel").ToString.Trim

                    If dt.Rows(ix).Item("doctorcd").ToString.Trim = msDoctorCd Then
                        .Col = .GetColFromID("chk") : .Text = "1"
                    End If
                Next

            End With


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub cboDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDept.SelectedIndexChanged

        If mbLoad = False Then Return

        sbDisplay_drInfo()

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mbSave = True
        Me.Close()
    End Sub

    Private Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click

        Try
            Dim alSendInfo As New ArrayList

            With spdDrInfo
                For ix As Integer = 1 To .MaxRows

                    .Row = ix
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                    .Col = .GetColFromID("drtel") : Dim sDrTel As String = .Text.Replace("-", "")
                    .Col = .GetColFromID("drnm") : Dim sDrNm As String = .Text

                    If sChk = "1" And sDrTel <> "" Then
                        alSendInfo.Add(sDrTel + "/" + sDrNm)
                    End If
                Next
            End With

            If DA_SMS_SEND.fnGet_SMS_Send(Me.txtTelno.Text, Me.txtSmsCont.Text, alSendInfo, msRegno, msBcNo, msLisseq) Then
                mbSave = True
                MsgBox("전송이 완료되었습니다.")
                Me.Close()
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
            mbSave = False
        End Try

    End Sub

    Private Sub FGSMSSEND_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
    End Sub
    Private Sub spdDrInfo_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdDrInfo.ClickEvent '20130826 정선영 추가, 스프레드 의사명 입력 한글 설정
        If spdDrInfo.ActiveCol = spdDrInfo.GetColFromID("drnm") Then
            ImeModeBase = Windows.Forms.ImeMode.Hangul
        End If
    End Sub

    Private Sub spdDrInfo_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdDrInfo.KeyDownEvent

        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        With Me.spdDrInfo

            If .ActiveCol = .GetColFromID("drnm") Then
                .Row = .ActiveRow
                .Col = .GetColFromID("drnm") : Dim sUsrNm As String = .Text

                Dim iTop As Integer = Me.Top
                Dim iLeft As Integer = Me.Left

                Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DoctorList("", "", sUsrNm)

                Dim objHelp As New CDHELP.FGCDHELP01
                Dim alList As New ArrayList

                objHelp.FormText = "의사정보"

                objHelp.Distinct = True
                objHelp.MaxRows = 15
                objHelp.OnRowReturnYN = True

                objHelp.AddField("doctorcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("doctornm", "의사명", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("doctortel", "전화번호", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

                alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

                If alList.Count > 0 Then
                    .Row = .ActiveRow
                    .Col = .GetColFromID("drnm") : .Text = alList.Item(0).ToString.Split("|"c)(1)
                    .Col = .GetColFromID("drcd") : .Text = alList.Item(0).ToString.Split("|"c)(0)
                    .Col = .GetColFromID("drtel") : .Text = alList.Item(0).ToString.Split("|"c)(2)
                End If
            Else
                If .ActiveRow = .MaxRows And .ActiveCol = .GetColFromID("drtel") Then
                    .MaxRows += 1
                End If
            End If
        End With
    End Sub

End Class

Public Class DA_SMS_SEND

    'Public Shared Function fnGet_SMS_Send(ByVal rsUsrTelNo As String, ByVal rsSMSCont As String, ByVal ra_SendInfo As ArrayList) As Boolean

    '    Dim dbCn As New OracleConnection
    '    Dim dbTrans As OracleTransaction
    '    Dim dbCmd As New OracleCommand

    '    Dim sSql As String = ""
    '    Try
    '        'dbCn.ConnectionString = LOGIN.PRG_CONST.SMS_CONNECTSTR
    '        'dbCn.Open()

    '        dbTrans = dbCn.BeginTransaction()

    '        dbCmd.Connection = dbCn
    '        dbCmd.Transaction = dbTrans

    '        For ix As Integer = 0 To ra_SendInfo.Count - 1
    '            With dbCmd
    '                sSql = ""
    '                sSql += "INSERT INTO sm_data(  dest,  callback, msg_flag,  msg_text, reservation_time )"
    '                sSql += "             VALUES( :dest, :callback,      '1', :msg_text, SYSDATE)"

    '                .CommandType = CommandType.Text
    '                .CommandText = sSql

    '                .Parameters.Clear()
    '                .Parameters.Add("dest", OracleDbType.Varchar2).Value = ra_SendInfo.Item(ix).ToString
    '                .Parameters.Add("callback", OracleDbType.Varchar2).Value = rsUsrTelNo
    '                .Parameters.Add("msg_text", OracleDbType.Varchar2).Value = rsSMSCont

    '                .ExecuteNonQuery()

    '            End With
    '        Next

    '        dbTrans.Commit()
    '        Return True

    '    Catch ex As Exception
    '        dbTrans.Rollback()
    '        If dbCn.State <> 0 Then
    '            dbCn.Close()
    '        End If

    '        Throw (New Exception(ex.Message, ex))
    '    End Try

    'End Function
    '<<
    Public Shared Function fnGet_SMS_Send(ByVal rsUsrTelNo As String, ByVal rsSMSCont As String, ByVal ra_SendInfo As ArrayList, ByVal rsRegNo As String, Optional ByVal rsBcno As String = "", Optional ByVal rslisseq As String = "") As Boolean
        Dim dbCn As New OracleConnection

        Dim dbTran As OracleTransaction
        Dim dbCmd As New OracleCommand
        Dim dbDa As New OracleDataAdapter

        Dim sSql As String = ""
        Try
            Dim sSeqNo As String = ""

            'dbCn.ConnectionString = LOGIN.PRG_CONST.SMS_CONNECTSTR
            'dbCn.Open()

            Dim sDeptInf As String = LISAPP.APP_G.CommFn.fnGet_Usr_Dept_info(LOGIN.USER_INFO.USRID)

            dbCn = DBORA.DbProvider.GetDbConnection
            dbTran = dbCn.BeginTransaction()

            dbCmd.Connection = dbCn
            dbCmd.Transaction = dbTran

            For ix As Integer = 0 To ra_SendInfo.Count - 1
                With dbCmd

                    sSql = ""
                    sSql += "SELECT TO_CHAR(INF.SMS_SEQ.NEXTVAL) AS seqno FROM DUAL"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    dbDa = New OracleDataAdapter(dbCmd)

                    Dim dt As New DataTable

                    dbDa.Fill(dt)

                    If dt.Rows.Count < 1 Then Return False

                    sSeqNo = dt.Rows(0).Item("seqno").ToString

                    sSql = ""
                    sSql += "INSERT INTO inf.sm_data(  seq,  dest,  callback, msg_flag,   msg_text, reservation_time )"
                    sSql += "                 VALUES( :seq, :dest, :callback,       '1', :msg_text, SYSDATE)"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("seq", OracleDbType.Int64).Value = sSeqNo
                    .Parameters.Add("dest", OracleDbType.Varchar2).Value = ra_SendInfo.Item(ix).ToString.Split("/"c)(0)
                    .Parameters.Add("callback", OracleDbType.Varchar2).Value = rsUsrTelNo
                    .Parameters.Add("msg_text", OracleDbType.Varchar2).Value = rsSMSCont

                    .ExecuteNonQuery()

                    sSql = ""
                    sSql += "INSERT INTO com.zmshmsgt("
                    sSql += "            msgkey,      msgfrmtid,  instcd,       recvrnm,        recvrtelno, replytelno, msgcnts, rsrvyn, emergencyyn, trsmreqdt,"
                    sSql += "            systemcd,    deptcd,     deptnm,       fstrgstrid,     fstrgstrnm, bizabbr,    macaddr, pid,    fstrgstdt,   msgstate,"
                    sSql += "            lastupdtrid, lastupdtdt, trsmresultcd, smstrsmstartdt, mtstrsmdt,  smstrsmenddt"
                    sSql += "          )"
                    sSql += "    VALUES( :seq,       :msgfrmtid,    :instcd,    :recvrnm, :recvrtelno, :replytelno, :msgcnts,  0,                0, SYSDATE,"
                    sSql += "            :systemcd,  :deptcd,       :deptnm,    :userid,  :usernm,      'LIS',      :macaddr, :regno, SYSTIMESTAMP, 'W',"
                    sSql += "            :userid,     SYSTIMESTAMP,  NULL,       NULL,     NULL,        NULL"
                    sSql += "          )"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("seq", OracleDbType.Int64).Value = sSeqNo
                    .Parameters.Add("msgfrmtid", OracleDbType.Varchar2).Value = "ACK@LIS"
                    .Parameters.Add("instcd", OracleDbType.Varchar2).Value = LOGIN.PRG_CONST.SITECD
                    .Parameters.Add("recvrnm", OracleDbType.Varchar2).Value = ra_SendInfo.Item(ix).ToString.Split("/"c)(1)
                    .Parameters.Add("recvrnm", OracleDbType.Varchar2).Value = ra_SendInfo.Item(ix).ToString.Split("/"c)(0)
                    .Parameters.Add("replytelno", OracleDbType.Varchar2).Value = rsUsrTelNo
                    .Parameters.Add("msgcnts", OracleDbType.Varchar2).Value = rsSMSCont
                    .Parameters.Add("systemcd", OracleDbType.Varchar2).Value = "-"

                    If sDeptInf.IndexOf("/") >= 0 Then
                        .Parameters.Add("deptcd", OracleDbType.Varchar2).Value = IIf(sDeptInf.Split("/"c)(0) = "", "2200000000", sDeptInf.Split("/"c)(0)).ToString
                        .Parameters.Add("deptnm", OracleDbType.Varchar2).Value = IIf(sDeptInf.Split("/"c)(1) = "", "진단검사의학과", sDeptInf.Split("/"c)(1)).ToString
                    Else
                        .Parameters.Add("deptcd", OracleDbType.Varchar2).Value = "2200000000"
                        .Parameters.Add("deptnm", OracleDbType.Varchar2).Value = "진단검사의학과"
                    End If

                    .Parameters.Add("userid", OracleDbType.Varchar2).Value = LOGIN.USER_INFO.USRID
                    .Parameters.Add("usernm", OracleDbType.Varchar2).Value = LOGIN.USER_INFO.USRNM
                    .Parameters.Add("macaddr", OracleDbType.Varchar2).Value = LOGIN.USER_INFO.LOCALIP
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo
                    .Parameters.Add("userid", OracleDbType.Varchar2).Value = LOGIN.USER_INFO.USRID

                    .ExecuteNonQuery()
                    '<<<20170523 LIS 테이블에 sms 전송기록 저장 
                    If rsBcno <> "" Then
                        sSql = ""
                        sSql += " INSERT INTO LR054m (BCNO  ,  MSGKEY ,LISSEQ,  RECVRNM  , RECVRTELNO  , REPLYTELNO  , MSGCNTS )  " + vbCrLf
                        sSql += "             values (:bcno , :msgkey , :lisseq , :recvrnm , :recvrtelno , :replytelno , :msgcnts )" + vbCrLf
                        sSql += "   " + vbCrLf
                        sSql += "   " + vbCrLf

                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                        .Parameters.Add("msgkey", OracleDbType.Varchar2).Value = sSeqNo
                        .Parameters.Add("lisseq", OracleDbType.Varchar2).Value = rslisseq
                        .Parameters.Add("recvrnm", OracleDbType.Varchar2).Value = ra_SendInfo.Item(ix).ToString.Split("/"c)(1)
                        .Parameters.Add("recvrnm", OracleDbType.Varchar2).Value = ra_SendInfo.Item(ix).ToString.Split("/"c)(0)
                        .Parameters.Add("replytelno", OracleDbType.Varchar2).Value = rsUsrTelNo
                        .Parameters.Add("msgcnts", OracleDbType.Varchar2).Value = rsSMSCont

                        .ExecuteNonQuery()

                    End If

                End With
            Next

            dbTran.Commit()
            Return True

        Catch ex As Exception
            dbTran.Rollback()
            If dbCn.State <> 0 Then
                dbCn.Close() : dbCn.Dispose()
            End If

            Throw (New Exception(ex.Message, ex))
        End Try

    End Function

End Class
