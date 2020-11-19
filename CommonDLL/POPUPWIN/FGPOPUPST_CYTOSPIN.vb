Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports DBORA.DbProvider

Public Class FGPOPUPST_CYTOSPIN
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_CYTOSPIN.vb, Class : FGPOPUPST_CYTOSPIN" & vbTab

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

        With Me.spdRst
            .Col = 2 : .Col2 = 2
            .Row = 1 : .Row2 = .MaxRows
            .BlockMode = True
            .Action = FPSpreadADO.ActionConstants.ActionClearText
            .BlockMode = False
        End With

        Me.txtCmt.Text = "" : Me.txtCon.Text = ""

    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcNo As String, ByVal rsTestCd As String)

        Try
            Dim dt_r As DataTable = DA_ST_CYTOSPIN.fnGet_Rst_SubInfo(rsBcNo, rsTestCd, m_dbCn)

            Dim alRst_flag As New ArrayList

            sbDisplay_Init()

            For ix As Integer = 0 To dt_r.Rows.Count - 1
                If ix = 0 Then
                    With Me.spdRst
                        .Row = 1 : .Col = 2 : .Text = dt_r.Rows(ix).Item("spcnmd").ToString
                    End With
                End If

                Select Case dt_r.Rows(ix).Item("testcd").ToString.Trim
                    Case Me.txtCmt.Tag.ToString

                        Me.txtCmt.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Me.txtCon.Tag.ToString

                        Me.txtCon.Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim

                    Case Else
                        With Me.spdRst
                            For iRow As Integer = 1 To .MaxRows
                                .Row = iRow : .Col = 4 : Dim sCd As String = .Text

                                If dt_r.Rows(ix).Item("testcd").ToString.Trim = sCd Then
                                    .Col = 2 : .Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim
                                    Exit For
                                End If
                            Next

                        End With
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

            With Me.spdRst
                For iRow As Integer = 1 To .MaxRows - 1
                    If iRow = 4 Or iRow = 15 Then
                    Else
                        .Row = iRow
                        .Col = 1 : Dim sTnm As String = .Text
                        .Col = 2 : Dim sRst As String = .Text
                        .Col = 3 : Dim sUnit As String = .Text
                        .Col = 4 : Dim sCde As String = .Text

                        sRstInfo += sCde + "^" + sRst + "|"

                        If iRow = 16 Then sValues += vbCrLf

                        If iRow = 5 Then
                            sValues += Space(5) + sTnm + Space(7) + sRst.PadLeft(3, " "c) + vbCrLf

                        ElseIf iRow >= 6 Then

                            If iRow = 8 Then
                                sValues += Space(5) + sTnm.PadRight(28, " "c) + sRst.PadLeft(8, " "c) + " " + sUnit + vbCrLf
                            Else
                                sValues += Space(5) + sTnm.PadRight(28, " "c) + Space(13) + sRst.PadLeft(10, " "c) + " " + sUnit + vbCrLf
                            End If

                        Else
                            sValues += Space(5) + sTnm + Space(5) + sRst.PadLeft(10, " "c) + vbCrLf
                        End If

                    End If

                Next
            End With

            sRstInfo += Me.txtCmt.Tag.ToString + "^" + Me.txtCmt.Text + "|"
            sRstInfo += Me.txtCon.Tag.ToString + "^" + Me.txtCon.Text + "|"

            sValues += vbCrLf
            sValues += Space(5) + Me.lblCmt.Text + vbCrLf + Space(7) + Me.txtCmt.Text + vbCrLf + vbCrLf
            sValues += Space(5) + Me.lblCon.Text + vbCrLf + Space(16) + Me.txtCon.Text + vbCrLf


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

    Private Sub btnHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_cmt.Click, btnHelp_con.Click
        Try
            Dim objBtn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)
            Dim objTxt As Windows.Forms.TextBox

            Dim sTestCd As String = ""

            Select Case objBtn.Name.ToLower
                Case "btnhelp_cmt" : sTestCd = Me.txtCmt.Tag.ToString : objTxt = Me.txtCmt
                Case "btnhelp_con" : sTestCd = Me.txtcon.Tag.ToString : objTxt = Me.txtcon
            End Select

            Dim iHeight As Integer = Convert.ToInt32(objBtn.Height)
            Dim iWidth As Integer = Convert.ToInt32(objBtn.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Me.Top + objBtn.Top + Ctrl.menuHeight - 50

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Me.Left + objBtn.Left
            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - objBtn.Width)

            Dim dt As DataTable = DA_ST_CYTOSPIN.fnGet_RstCd_Info(sTestCd, m_dbCn)

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


End Class

Public Class DA_ST_CYTOSPIN

    Public Shared Function fnGet_Spc_Info(ByVal rsTestCd As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT rstcont"
            sSql += "  FROM lf060m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   and usdt <= fn"


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

    Public Shared Function fnGet_Cmt_Info(ByVal rsTestCd As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT cmtcont"
            sSql += "  FROM lf312m"
            sSql += " WHERE testcd = :testcd"


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

    '<JJH 특수보고서 이전결과
    Public Shared Function fnGet_BfRst(ByVal rsBcno As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += " SELECT TO_CHAR(TO_DATE(SUBSTR(B.TKDT, 1, 8), 'YYYY-MM-DD'), 'YYYY-MM-DD') TKDT, "
            sSql += "        D.BCNO, C.SPCNMS, D.TESTCD, D.RST,                                       "
            sSql += "        CASE WHEN D.TESTCD = 'LG119' THEN '(상기도)'                             "
            sSql += "             WHEN D.TESTCD = 'LG120' THEN '(하기도)'                             "
            sSql += "        ELSE '' END GBN, LENGTH(C.SPCNMS) SPCLEN                                 "
            sSql += "   FROM ( SELECT TKDT, REGNO, BCNO                                               "
            sSql += "            FROM LJ011M                                                          "
            sSql += "           WHERE BCNO = :BCNO ) A,                                               "
            sSql += "        LJ011M B, LF030M C, LRS17M D                                             "
            sSql += "  WHERE /*B.TKDT  >= TO_CHAR(ADD_MONTHS(TO_DATE(A.TKDT, 'YYYY-MM-DD HH24:MI:SS'), -1), 'yyyymmddhh24miss')" '한달전 '사용자요청으로 한달기간 제거
            sSql += "    AND */B.TKDT  <= A.TKDT                                                        "
            sSql += "    AND A.REGNO  = B.REGNO                                                       "
            sSql += "    AND B.SPCCD  = C.SPCCD                                                       "
            sSql += "    AND A.BCNO  <> B.BCNO                                                        "
            sSql += "    AND NVL(B.RSTFLG, '0') = '3'                                                 "
            sSql += "    AND B.TKDT  >= C.USDT                                                        "
            sSql += "    AND B.TKDT  <= C.UEDT                                                        "
            sSql += "    AND B.BCNO   = D.BCNO                                                        "
            sSql += "    AND B.TCLSCD = D.TESTCD                                                      "
            sSql += "    AND B.REGNO  = D.REGNO                                                       "
            sSql += "    AND D.RST IS NOT NULL                                                        "
            sSql += "  ORDER BY TKDT, TESTCD                                                          "

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
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

    '<JJH 특수보고서 이전결과
    Public Shared Function fnGet_CtRst(ByVal rsBcno As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += " SELECT RST, GBN     " 'GBN = R --> RdRP // GBN = E  --> E gene
            sSql += "   FROM LRS18M       "
            sSql += "  WHERE BCNO = :BCNO "
            sSql += "  ORDER BY GBN "

            sSql = ""
            sSql += "  SELECT A.RST, A.GBN,"
            sSql += "         CASE WHEN NVL(B.BCNO,' ') <> ' ' THEN 'Y' ELSE 'N' END REYN"
            sSql += "    FROM LRS18M A, LRS18H B"
            sSql += "   WHERE A.BCNO   = :BCNO"
            sSql += "     AND A.BCNO   = B.BCNO(+)"
            sSql += "     AND A.TESTCD = B.TESTCD(+)"
            sSql += "     AND A.SPCCD  = B.SPCCD(+)"
            sSql += "     AND A.GBN    = B.GBN(+)"
            sSql += "   GROUP BY A.RST, A.GBN, B.BCNO"
            sSql += "   ORDER BY A.GBN "

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
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

    '<2020-02-07 JJH 코로나바이러스 정보
    Public Shared Function fnGet_nCov(ByVal rsTestCd As String, ByVal rsClscd As String, ByVal r_dbCn As OracleConnection) As DataTable
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT CLSVAL"
            sSql += "  FROM LF000M "
            sSql += " WHERE CLSGBN = 'NCOV' "
            sSql += "   AND CLSCD LIKE :clscd || '%' " 'C = 코멘트 I = 개요 S = 검체
            sSql += "   AND CLSDESC LIKE :testcd || '%'"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("clscd", OracleDbType.Varchar2).Value = rsClscd
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