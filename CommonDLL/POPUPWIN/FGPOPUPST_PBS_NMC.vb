Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports DBORA.DbProvider

Public Class FGPOPUPST_PBS_NMC
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_PBS.vb, Class : FGPOPUPST_PBS" & vbTab

    Private Const mc_iXmargin_right As Integer = 5
    Private Const mc_iYmargin_bottom As Integer = 20

    Private m_dbcn As OracleConnection
    Private m_frm As Windows.Forms.Form
    Private msBcNo As String = ""
    Private msTestCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

    Private msCrLf As String = Convert.ToChar(13) + Convert.ToChar(10)

    Private msResult As String = ""

    Private mbSave As Boolean = False
    Private mbActivated As Boolean = False

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

        Me.txtRbc.Text = "" : Me.txtOther_r.Text = "" : Me.txtNrc.Text = ""
        Me.txtWbc.Text = "" : Me.txtOther_w.Text = ""
        Me.txtPlt.Text = "" : Me.txtOther_p.Text = ""
        Me.txtOpinion.Text = ""

    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcNo As String, ByVal rsTestCd As String)

        Try
            Dim dt_r As DataTable = DA_ST_PBS_NMC.fnGet_Rst_SubInfo(rsBcNo, rsTestCd, m_dbCn)

            Dim alRst_flag As New ArrayList

            sbDisplay_Init()

            For ix As Integer = 0 To dt_r.Rows.Count - 1
                Select Case dt_r.Rows(ix).Item("testcd").ToString.Trim
                    Case Me.txtRbc.Tag.ToString

                        Me.txtRbc.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtWbc.Tag.ToString

                        Me.txtWbc.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtPlt.Tag.ToString

                        Me.txtPlt.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtNrc.Tag.ToString

                        Me.txtRbc.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtOther_r.Tag.ToString

                        Me.txtRbc.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtOther_w.Tag.ToString

                        Me.txtRbc.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtOther_p.Tag.ToString

                        Me.txtRbc.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtOpinion.Tag.ToString

                        Me.txtRbc.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim
                End Select

            Next

        Catch ex As Exception

        End Try
    End Sub

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_m_dbCn As OracleConnection, _
                                    ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm

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

            sRstInfo += Me.txtRbc.Tag.ToString + "^" + Me.txtRbc.Text + "|"
            sRstInfo += Me.txtOther_r.Tag.ToString + "^" + Me.txtOther_r.Text + "|"
            sRstInfo += Me.txtNrc.Tag.ToString + "^" + Me.txtNrc.Text + "|"
            sRstInfo += Me.txtWbc.Tag.ToString + "^" + Me.txtWbc.Text + "|"
            sRstInfo += Me.txtOther_w.Tag.ToString + "^" + Me.txtOther_w.Text + "|"
            sRstInfo += Me.txtPlt.Tag.ToString + "^" + Me.txtPlt.Text + "|"
            sRstInfo += Me.txtOther_p.Tag.ToString + "^" + Me.txtOther_p.Text + "|"
            sRstInfo += Me.txtOpinion.Tag.ToString + "^" + Me.txtOpinion.Text

            'Dim bRet As Boolean = DA_ST_PBS_NMC.fnExe_Insert(msBcNo, sRstInfo)

            'If bRet = False Then
            '    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "데이타시 저장시 오류가 발생했습니다.!!")
            '    Return ""
            'End If

            sValues += Space(5) + "□ RED BLOOD CELLS" + vbCrLf
            sValues += Space(15) + Me.txtRbc.Text + vbCrLf + vbCrLf
            sValues += Space(8) + "Other : " + Me.txtOther_r.Text + vbCrLf

            If Me.txtNrc.Text <> "" Then
                sValues += Space(8) + "Nucleated red cell " + Me.txtNrc.Text + " /100 WBC" + vbCrLf
            End If

            sValues += vbCrLf

            sValues += Space(5) + "□ WHITE BLOOD CELLS" + vbCrLf
            sValues += Space(15) + Me.txtWbc.Text + vbCrLf + vbCrLf
            sValues += Space(8) + "Other : " + Me.txtOther_w.Text + vbCrLf

            sValues += vbCrLf

            sValues += Space(5) + "□ PLATELETS " + vbCrLf
            sValues += Space(15) + Me.txtPlt.Text + vbCrLf + vbCrLf
            sValues += Space(8) + "Other : " + Me.txtOther_p.Text + vbCrLf

            sValues += vbCrLf

            sValues += Space(5) + "□ OPINION " + vbCrLf
            sValues += Space(8) + Me.txtOpinion.Text

        Catch ex As Exception

        Finally
            fnGet_Report = sValues + vbCrLf
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

    Private Sub btnHelp_r_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_p.Click, btnHelp_w.Click, btnHelp_r.Click, btnHelp_or.Click, btnHelp_op.Click, btnHelp_ow.Click
        Try
            Dim objBtn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)
            Dim objTxt As Windows.Forms.TextBox

            Dim sTestCd As String = ""

            Select Case objBtn.Name.ToLower
                Case "btnhelp_r" : sTestCd = Me.txtRbc.Tag.ToString : objTxt = Me.txtRbc
                Case "btnhelp_w" : sTestCd = Me.txtWbc.Tag.ToString : objTxt = Me.txtWbc
                Case "btnhelp_p" : sTestCd = Me.txtPlt.Tag.ToString : objTxt = Me.txtPlt
                Case "btnhelp_or" : sTestCd = Me.txtOther_r.Tag.ToString : objTxt = Me.txtOther_r
                Case "btnhelp_ow" : sTestCd = Me.txtOther_w.Tag.ToString : objTxt = Me.txtOther_w
                Case "btnhelp_op" : sTestCd = Me.txtOther_p.Tag.ToString : objTxt = Me.txtOther_p
            End Select

            Dim iHeight As Integer = Convert.ToInt32(objBtn.Height)
            Dim iWidth As Integer = Convert.ToInt32(objBtn.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Me.Top + objBtn.Top + Ctrl.menuHeight - 50

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Me.Left + objBtn.Left
            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - objBtn.Width)

            Dim dt As DataTable = DA_ST_PBS_NMC.fnGet_RstCd_Info(sTestCd, m_dbcn)

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

    Private Sub btnHelp_opin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_opin.Click

        Dim invas_buf As New InvAs

        Try
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\POPUPWIN.dll", "POPUPWIN.FGPOPUPST_VSPT")
                .SetProperty("UserID", USER_INFO.USRID)

                Dim a_objParam() As Object
                ReDim a_objParam(4)

                a_objParam(0) = Me
                a_objParam(1) = GetDbConnection()
                a_objParam(2) = msBcNo
                a_objParam(3) = msTestCd
                a_objParam(4) = msTNm

                Dim al_return As ArrayList = CType(.InvokeMember("Display_Result", a_objParam), ArrayList)

                If al_return Is Nothing Then Return
                If al_return.Count < 1 Then Return

                For i As Integer = 1 To al_return.Count
                    Dim objData As Object = CType(al_return(i - 1), STU_StDataInfo).Data
                    Dim objData2 As Object = CType(al_return(i - 1), STU_StDataInfo).Data2
                    Dim iAlign As Integer = CType(al_return(i - 1), STU_StDataInfo).Alignment

                    Select Case objData.GetType.Name.ToLower()
                        Case "string"
                            Me.txtOpinion.Text = objData.ToString
                    End Select
                Next
            End With

        Catch ex As Exception
            MsgBox(msFile + ex.Message)
        Finally
            invas_buf = Nothing
        End Try

    End Sub

End Class

Public Class DA_ST_PBS_NMC

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
            sSql += "SELECT r.testcd, r.viewrst, r.eqflag"
            sSql += "  FROM lr010m r"
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd LIKE :testcd || '%'"

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

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
                .CommandText = "delete lrs11m where bcno = :bcno and testcd = :testcd"

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd

                .ExecuteNonQuery()

                sSql = ""
                sSql += "insert into lrs11m(  bcno,  testcd,  rsttxt, rstdt )"
                sSql += "            values( :bcno, :testcd, :rsttxt, fn_ack_sysdate)"

                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                .Parameters.Add("rsttxt", OracleDbType.Varchar2).Value = rsRst

                iRet = .ExecuteNonQuery()
            End With

            If iRet = 0 Then
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