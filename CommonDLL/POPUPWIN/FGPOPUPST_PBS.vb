Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports DBORA.DbProvider

Public Class FGPOPUPST_PBS
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_PBS.vb, Class : FGPOPUPST_PBS" & vbTab

    Private Const mc_iXmargin_right As Integer = 5
    Private Const mc_iYmargin_bottom As Integer = 20

    Private m_frm As Windows.Forms.Form
    Private m_dbCn As OracleConnection
    Private msBcNo As String = ""
    Private msTestCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

    Private msCrLf As String = Convert.ToChar(13) + Convert.ToChar(10)

    Private msResult As String = ""
    Private msAutoCmt As String = ""

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

    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcNo As String, ByVal rsTestCd As String)

        Try
            Dim dt_r As DataTable = DA_ST_PBS.fnGet_Rst_RefInfo(rsBcNo, rsTestCd, m_dbCn)
            Dim dt_c As DataTable = DA_ST_PBS.fnGet_EtcInfo(rsTestCd, m_dbCn)

            Dim alRst_flag As New ArrayList

            msAutoCmt = ""

            '-- spread 에 결과값 표시
            If dt_r.Rows.Count > 0 Then
                With spdResult
                    For ix As Integer = 0 To dt_r.Rows.Count - 1
                        Dim iRow As Integer = .SearchCol(2, 1, .MaxRows, "Z" + dt_r.Rows(ix).Item("testcd").ToString.Trim + "", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                        If iRow > 0 Then
                            .Row = iRow
                            .Col = 2 : .Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim
                        Else
                            iRow = .SearchCol(8, 1, .MaxRows, "Z" + dt_r.Rows(ix).Item("testcd").ToString.Trim + "", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                            If iRow > 0 Then
                                .Row = iRow
                                .Col = 8 : .Text = dt_r.Rows(ix).Item("viewrst").ToString.Trim
                            End If
                        End If

                        If dt_r.Rows(ix).Item("eqflag").ToString.Trim <> "" Then
                            alRst_flag.Add(dt_r.Rows(ix).Item("eqflag").ToString.Trim)
                        End If
                    Next
                End With

            End If

            If dt_c.Rows.Count < 1 Then Return

            Dim sBuf() As String = dt_c.Rows(0).Item("etcinfo").ToString.Split("|"c)

            If alRst_flag.Count < 1 Then
                For ix As Integer = 0 To sBuf.Length - 1
                    If sBuf(ix).StartsWith("W-CMT") Then
                        msAutoCmt = sBuf(ix).Split("^"c)(2)
                        Return
                    End If
                Next

                Return
            End If

            Dim bWbcYN As Boolean = False
            Dim sWbcCmt As String = ""

            For ix As Integer = 0 To sBuf.Length - 1
                If alRst_flag.Contains(sBuf(ix).Split("^"c)(1)) Then
                    msAutoCmt += sBuf(ix).Split("^"c)(2) + vbCrLf

                    If sBuf(ix).Split("^"c)(0) = "WBC" Then bWbcYN = True
                End If

                If sBuf(ix).Split("^"c)(0) = "W-CMT" Then sWbcCmt = sBuf(ix).Split("^"c)(2)
            Next

            If bWbcYN = False Then msAutoCmt += sWbcCmt + vbCrLf

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
            With Me.spdResult
                For iRow As Integer = 1 To .MaxRows
                    Dim sTnm1 As String = "", sTnm2 As String = ""
                    Dim sRst1 As String = "", sRst2 As String = ""
                    Dim sSpace As String = ""

                    .Row = iRow
                    .Col = 1 : sTnm1 = .Text
                    .Col = 6 : sSpace = .Text

                    .Col = 2
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then
                        sRst1 = .Text
                        .Col = 3 : sRst1 += " " + .Text
                    Else
                        For intCol As Integer = 2 To 5
                            .Col = intCol
                            If .CellType <> FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then Exit For

                            If .Text = "1" Then
                                sRst1 = "(" + .TypeCheckText + ")"
                                Exit For
                            End If
                        Next
                    End If

                    .Col = 7 : sTnm2 = .Text
                    .Col = 8
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then
                        sRst2 = .Text
                        .Col = 9 : sRst2 += " " + .Text
                    Else
                        For intCol As Integer = 8 To 10
                            .Col = intCol
                            If .CellType <> FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then Exit For

                            If .Text = "1" Then
                                sRst2 = "(" + .TypeCheckText + ")"
                                Exit For
                            End If
                        Next
                    End If


                    If iRow <> 1 And sTnm1.Substring(0, 1) <> " " Then
                        sValues += vbCrLf
                    End If

                    If sTnm1.Substring(0, 1) <> " " Then
                        sValues += sTnm1 + vbCrLf
                    Else
                        Dim sLine As String = ""

                        sLine = sTnm1.PadRight(Convert.ToInt16(sSpace.Split("/"c)(0)), " "c)
                        sLine += sRst1

                        If sTnm2 <> "" Then
                            sLine = sLine.PadRight(40, " "c)

                            sLine += sTnm2.PadRight(Convert.ToInt16(sSpace.Split("/"c)(1)), " "c)
                            sLine += sRst2
                        End If

                        sValues += sLine + vbCrLf
                    End If
                Next
            End With

        Catch ex As Exception

        Finally
            fnGet_Report = sValues + vbCrLf + "Commen" + vbCrLf + msAutoCmt
        End Try

    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        msResult = ""
        mbSave = False
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        msResult = fnGet_Report()
        mbSave = True
        Me.Close()
    End Sub

    Private Sub spdResult_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdResult.ButtonClicked

        With spdResult
            .Row = e.row
            .Col = e.col : Dim strChk As String = .Text

            If .Text = "1" Then
                If e.col > 7 Then
                    For intCol As Integer = 8 To 10
                        .Row = e.row
                        .Col = intCol
                        If e.col <> intCol And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            .Text = ""
                        End If
                    Next
                Else
                    For intCol As Integer = 2 To 5
                        .Row = e.row
                        .Col = intCol
                        If e.col <> intCol And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            : .Text = ""
                        End If
                    Next
                End If
            End If
        End With

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

    Private Sub btnFlag_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFlag.Click
        Dim sFn As String = "Handles btnFlag.Click"

        Dim frm As New FGPOPUPST_PBS_S01(msTestCd, m_dbCn)
        frm.ShowDialog()

        sbDisplay_Data(msBcNo, msTestCd)

    End Sub

    Private Sub spdResult_Advance(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_AdvanceEvent) Handles spdResult.Advance

    End Sub
End Class

Public Class DA_ST_PBS

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
            sSql += "          FROM lf063m a, lf062m b"
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

    Public Shared Function fnExec_Insert(ByVal rsTestCd As String, ByVal rsValue As String, ByVal r_dbCn As OracleConnection) As Boolean
        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql = ""
            sSql += "DELETE lf311m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = '" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd

                .ExecuteNonQuery()

                sSql = ""
                sSql += "INSERT INTO LF311M(  testcd, spccd, regdt, regid, etcinfo )"
                sSql += "            VALUES( :testcd, '" + "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "', fn_ack_sysdate, :regid, :etcinfo )"

                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                .Parameters.Add("usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                .Parameters.Add("etcinfo", OracleDbType.Varchar2).Value = rsValue

                iRet = .ExecuteNonQuery()
            End With

            If iRet < 1 Then
                dbTran.Rollback()
                Return False
            End If

            dbTran.Commit()
            Return True

        Catch ex As Exception
            dbTran.Rollback()
            Return False
            MsgBox(ex.Message)
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