Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports DBORA.DbProvider

Public Class FGPOPUPST_ZIKA

    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_MERS.vb, Class : FGPOPUPST_MERS" & vbTab

    Private Const mc_iXmargin_right As Integer = 5
    Private Const mc_iYmargin_bottom As Integer = 20

    Private m_dbCn As OracleConnection
    Private m_frm As Windows.Forms.Form
    Private msBcNo As String = ""
    Private msTestCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

    Private msCrLf As String = Convert.ToChar(13) + Convert.ToChar(10)

    Private msResult As String = ""

    Private mbSave As Boolean = False
    Private mbActivated As Boolean = False
    Private mtxtTestInfo As String = ""

    Public ReadOnly Property Append() As Boolean
        Get
            Append = False
        End Get
    End Property

    Public WriteOnly Property UserID() As String
        Set(ByVal Value As String)
            msUsrID = Value
        End Set
    End Property

    Private Sub sbDisplay_Init()

        Me.txtCmt.Text = "" : Me.txtCon.Text = ""

        'Me.txtTestinfo.Text += "(1) 유전자와 검사 : MERS-CoV (Middle East Respiratory Syndrome Coronavirus) 의 " + vbCrLf
        'Me.txtTestinfo.Text += "    유전자 중 UP-E segment, ORF1a segment를 real-time RT-PCR로 검출함." + vbCrLf
        'Me.txtTestinfo.Text += "(2) 본 검사실에서 현재까지 자체산정한 코젠바이오텍 PowerChek MERS(upE & ORF1a) " + vbCrLf
        'Me.txtTestinfo.Text += "    PCR kit 시약 (질병관리본부 시행 시약과 동일함)의 민감도는 ~1-10 copies/uL임." + vbCrLf
        'Me.txtTestinfo.Text += "(3) 관련질환과 의의: 발열, 기침, 호흡곤란, 두통, 오한, 인후통, 호흡부전, 패혈성" + vbCrLf
        'Me.txtTestinfo.Text += "    쇼크, 다발성 장기부전 등의 원인인 MERS-CoV를 검출함." + vbCrLf


        Me.txtTestinfo.Text += "(1) 유전자와 검사: 혈액 또는 소변 검체에서 Zika virus의 유전자를 real-time" + vbCrLf
        Me.txtTestinfo.Text += "    RT-PCR로 검출함." + vbCrLf
        Me.txtTestinfo.Text += "(2) 관련질환과 의의: Zika virus는 주로 모기를 매개체로 하여 전파되는," + vbCrLf
        Me.txtTestinfo.Text += "    Flaviviridae과 Flavivirus속의 바이러스임. 감염시 발열, 발진, 관절통," + vbCrLf
        Me.txtTestinfo.Text += "    근육통, 결막염, 두통 등의 증상이 나타날 수 있으며, Zika virus 감염과" + vbCrLf
        Me.txtTestinfo.Text += "    신생아 소두증, 길랑-바레 증후군 등의 연관성 또한 보고되고 있음." + vbCrLf


        'Me.mtxtTestInfo += "(1) 유전자와 검사 : MERS-CoV (Middle East Respiratory Syndrome Coronavirus) 의"
        'Me.mtxtTestInfo += "    유전자 중 UP-E segment, ORF1a segment를 real-time RT-PCR로 검출함."
        'Me.mtxtTestInfo += "(2) 본 검사실에서 현재까지 자체산정한 코젠바이오텍 PowerChek MERS(upE & ORF1a)"
        'Me.mtxtTestInfo += "    PCR kit 시약 (질병관리본부 시행 시약과 동일함)의 민감도는 ~1-10 copies/uL임."
        'Me.mtxtTestInfo += "(3) 관련질환과 의의: 발열, 기침, 호흡곤란, 두통, 오한, 인후통, 호흡부전, 패혈성"
        'Me.mtxtTestInfo += "    쇼크, 다발성 장기부전 등의 원인인 MERS-CoV를 검출함."



    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcNo As String, ByVal rsTestCd As String)

        Try
            Dim dt_r As DataTable = DA_ST_CYTOSPIN.fnGet_Rst_SubInfo(rsBcNo, rsTestCd, m_dbCn)

            Dim alRst_flag As New ArrayList

            sbDisplay_Init()

            For ix As Integer = 0 To dt_r.Rows.Count - 1
                If ix = 0 Then

                End If

                Select Case dt_r.Rows(ix).Item("testcd").ToString.Trim
                    Case Me.txtCmt.Tag.ToString

                        Me.txtCmt.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtCon.Tag.ToString

                        Me.txtCon.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Else

                End Select
            Next

        Catch ex As Exception

        End Try
    End Sub

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_dbCn As OracleConnection, _
                                    ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm
        m_dbCn = r_dbCn
        msBcNo = rsBcNo
        msTestCd = rsTestCd
        msTNm = rsTNm

        Try
            sbDisplay_Data(rsBcNo, rsTestCd)

            Me.ShowDialog(r_frm)

            Dim STU_StDataInfo As STU_StDataInfo
            Dim al_return As New ArrayList

            If mbSave Then

                STU_StDataInfo = New STU_StDataInfo
                STU_StDataInfo.Data = msResult
                STU_StDataInfo.Alignment = 0
                al_return.Add(STU_StDataInfo)
                STU_StDataInfo = Nothing
            End If

            Return al_return

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function


    Private Function fnGet_Report() As String

        Dim sValues As String = ""

        Try
            Dim sRstInfo As String = ""
            Dim sSplitValue As String = ""
            Dim iStartPos As Integer = 0
            Dim iMaxLength As Integer = 0
            Dim strBuf() As String

            iMaxLength = Me.txtTestinfo.Text.Length

            sRstInfo += Me.txtCmt.Tag.ToString + "^" + Me.txtCmt.Text + "|"
            sRstInfo += Me.txtCon.Tag.ToString + "^" + Me.txtCon.Text + "|"

            sValues += vbCrLf
            sValues += Space(5) + Me.lblSpc.Text + Me.txtSpcnm.Text + vbCrLf + vbCrLf
            sValues += Space(5) + Me.lblDate.Text + Me.txtSpcDate.Text + vbCrLf + vbCrLf
            sValues += Space(5) + Me.lblMethod.Text + Me.txtTestnm.Text + vbCrLf + vbCrLf

            strBuf = Me.lblRst.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                If intIdx = 0 Then
                    sValues += Space(5) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                Else
                    sValues += Space(5) + strBuf(intIdx).Replace(vbLf, "")
                End If
            Next

            strBuf = Me.txtRst.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                If intIdx = 0 Then
                    sValues += strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                Else
                    sValues += Space(41) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                End If

            Next

            sValues += vbCrLf
            sValues += Space(5) + Me.lblCmt.Text + vbCrLf

            strBuf = Me.txtCmt.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                sValues += Space(8) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
            Next

            sValues += vbCrLf
            sValues += Space(5) + Me.lblTest.Text
            'Do Until iStartPos > Me.txtTestinfo.Text.Length
            '    sSplitValue = Me.txtTestinfo.Text.Substring(iStartPos, CInt(IIf(iMaxLength > 100, 100, iMaxLength)))

            '    sValues += Space(7) + sSplitValue + vbCrLf

            '    iStartPos += CInt(IIf(iMaxLength > 100, 100, iMaxLength))
            '    iMaxLength = CInt(IIf(iMaxLength > 100, iMaxLength - 100, iMaxLength))

            'Loop
            strBuf = txtTestinfo.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                If intIdx = 0 Then
                    sValues += strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                Else
                    sValues += Space(18) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                End If
            Next


            'sValues += Space(5) + Me.lblCon.Text + vbCrLf

            'strBuf = Me.txtCon.Text.Split(Chr(13))
            'For intIdx As Integer = 0 To strBuf.Length - 1
            '    sValues += Space(7) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
            'Next



        Catch ex As Exception

        Finally
            fnGet_Report = sValues
        End Try

    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        msResult = ""
        mbSave = False
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        msResult = fnGet_Report()
        If msResult.Trim = "" Then
            mbSave = False
        Else
            mbSave = True
            Me.Close()
        End If

    End Sub

    Private Sub FGPOPUPST_PBS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Windows.Forms.Keys.Escape Then
            btnClose_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then
            btnSave_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub FGPOPUPST_PBS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
    End Sub

    Private Sub btnHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_cmt.Click, btnHelp_con.Click, btnRst.Click, btnHelp_test.Click
        Try
            Dim objBtn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)
            Dim objTxt As Windows.Forms.TextBox

            Dim sTestCd As String = ""

            Select Case objBtn.Name.ToLower
                Case "btnrst" : sTestCd = Me.txtRst.Tag.ToString : objTxt = Me.txtRst
                Case "btnhelp_cmt" : sTestCd = Me.txtCmt.Tag.ToString : objTxt = Me.txtCmt
                Case "btnhelp_con" : sTestCd = Me.txtCon.Tag.ToString : objTxt = Me.txtCon
                Case "btnhelp_test" : sTestCd = Me.txtTestinfo.Tag.ToString : objTxt = Me.txtTestinfo
            End Select

            Dim iHeight As Integer = Convert.ToInt32(objBtn.Height)
            Dim iWidth As Integer = Convert.ToInt32(objBtn.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Me.Top + objBtn.Top + Ctrl.menuHeight - 50

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Me.Left + objBtn.Left
            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - objBtn.Width)

            Dim dt As DataTable = DA_ST_MERS.fnGet_RstCd_Info(sTestCd, m_dbCn)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "결과코드 정보"
            objHelp.MaxRows = 15

            objHelp.AddField("rstcont", "내용", 100, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then objTxt.Text += IIf(objTxt.Text = "", "", ", ").ToString + alList.Item(0).ToString.Split("|"c)(0)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnSpc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpc.Click

        Dim sTestcd As String = "LG113"

        Dim iHeight As Integer = Convert.ToInt32(btnSpc.Height)
        Dim iWidth As Integer = Convert.ToInt32(btnSpc.Width)

        'Top --> 아래쪽에 맞춰지도록 설정
        Dim iTop As Integer = Me.Top + btnSpc.Top + Ctrl.menuHeight - 50

        'Left --> 왼쪽에 맞춰지도록 설정
        Dim iLeft As Integer = Me.Left + btnSpc.Left
        'Left --> 오른쪽에 맞춰지도록 설정
        iLeft = iLeft - (iWidth - btnSpc.Width)

        Dim dt As DataTable = DA_ST_MERS.fnGet_Spc_Info(sTestcd, m_dbCn)

        Dim objHelp As New CDHELP.FGCDHELP01
        Dim alList As New ArrayList

        objHelp.FormText = "검체정보"
        objHelp.MaxRows = 15

        objHelp.AddField("spcnm", "검체명", 100, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

        alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

        If alList.Count > 0 Then txtSpcnm.Text = alList.Item(0).ToString.Split("|"c)(0)


    End Sub


End Class

Public Class DA_ST_ZIKA

    Public Shared Function fnGet_Spc_Info(ByVal rsTestCd As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT distinct f3.spcnm "
            sSql += "  FROM lf060m f6 , lf030m f3"
            sSql += " WHERE f6.testcd = :testcd"
            sSql += "   AND f6.usdt <= fn_ack_sysdate"
            sSql += "   AND f6.uedt >  fn_ack_sysdate"
            sSql += "   AND f3.spccd = f6.spccd"


            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Return New DataTable
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Shared Function fnGet_RstCd_Info(ByVal rsTestCd As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT rstcont"
            sSql += "  FROM lf083m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = '" + "".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Return New DataTable
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Shared Function fnGet_Rst_RefInfo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT r.testcd, r.viewrst, r.eqflag"
            sSql += "  FROM lr010m r,"
            sSql += "       (SELECT reftestcd testcd, refspccd spccd"
            sSql += "          FROM lf063m f"
            sSql += "         WHERE testcd = :testcd"
            sSql += "         UNION "
            sSql += "        SELECT b.testcd, b.spccd"
            sSql += "          FROM lf063m a, LF062M b"
            sSql += "         WHERE a.testcd    = :testcd"
            sSql += "           AND a.reftestcd = b.tclscd"
            sSql += "           AND a.refspccd  = b.tspccd"
            sSql += "       ) f"
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd = f.testcd"
            sSql += "   AND r.spccd  = f.spccd"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Return New DataTable
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Shared Function fnGet_Rst_SubInfo(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT r.testcd, r.viewrst, r.eqflag, f.spcnmd"
            sSql += "  FROM lr010m r, lf030m f"
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd LIKE :testcd || '%'"
            sSql += "   AND r.spccd  = f.spccd AND r.tkdt >= f.usdt AND r.tkdt < f.uedt"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Return New DataTable
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Shared Function fnGet_EtcInfo(ByVal rsTestCd As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "SELECT etcinfo  FROM lf311m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = '" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"

            With dbCmd
                .Connection = dbCn
                .CommandType = CommandType.Text
                .CommandText = sSql
            End With

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception

            Return New DataTable
            MsgBox(ex.Message)
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try
    End Function

    Public Shared Function fnExe_Insert(ByVal rsBcNo As String, ByVal rsRstInfo As String) As Boolean

        Try

            Dim sBuf() As String = rsRstInfo.Split("|"c)
            Dim stuSample As New STU_SampleInfo
            Dim arlRstInfo As New ArrayList
            Dim arlSUCC As New ArrayList

            stuSample.BCNo = rsBcNo
            stuSample.EqCd = ""
            stuSample.UsrID = USER_INFO.USRID
            stuSample.UsrIP = USER_INFO.LOCALIP
            stuSample.IntSeqNo = ""
            stuSample.Rack = ""
            stuSample.Pos = ""
            stuSample.EqBCNo = ""

            stuSample.SenderID = ""
            stuSample.RegStep = "1"

            If sBuf.Length < 1 Then Return False

            For ix As Integer = 0 To sBuf.Length - 1
                Dim stuResult As New STU_RstInfo

                stuResult.TestCd = sBuf(ix).Split("^"c)(0)
                stuResult.OrgRst = sBuf(ix).Split("^"c)(1)
                stuResult.RstCmt = ""

                arlRstInfo.Add(stuResult)
            Next

            Dim da_regrst As New LISAPP.APP_R.RegFn
            Dim iRet As Integer = da_regrst.RegServer(arlRstInfo, stuSample, arlSUCC, False)

            If iRet < 1 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try

    End Function

    Public Shared Function fnExe_LRS11M(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsRst As String, ByVal r_dbCn As OracleConnection) As Boolean
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sqlDoc As String = ""
            Dim intRet As Integer = 0

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
                .CommandText = "delete lrs11m where bcno = :bcno and testcd = :testcd"

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd

                .ExecuteNonQuery()

                sqlDoc = ""
                sqlDoc += "insert into lrs11m(  bcno,  testcd,  rsttxt, rstdt )"
                sqlDoc += "            values( :bcno, :testcd, :rsttxt, fn_ack_sysdate)"

                .CommandText = sqlDoc

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                .Parameters.Add("rsttxt", OracleDbType.Varchar2).Value = rsRst

                intRet = .ExecuteNonQuery()
            End With

            If intRet = 0 Then
                dbTran.Rollback()
            Else
                dbTran.Commit()
            End If

            Return True

        Catch ex As Exception

            dbTran.Rollback()
            MsgBox(ex.Message)
            Return False
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
End Class