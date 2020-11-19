Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports Oracle.DataAccess.Client

Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports DBORA.DbProvider

Public Class FGPOPUPST_TDM
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_TDM.vb, Class : FGPOPUPST_TDM" & vbTab

    Private Const mc_iXmargin_right As Integer = 5
    Private Const mc_iYmargin_bottom As Integer = 20

    Private m_frm As Windows.Forms.Form
    Private m_dbCn As OracleConnection
    Private msBcNo As String = ""
    Private msTClsCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

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

    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal r_DbCn As oracleConnection)

        Try

            Dim strTmp As String = DA_ST_TDM.fnGet_Rst_TDM(rsBcNo, rsTclsCd, r_DbCn)
            If strTmp = "" Then Return

            Dim strBuf() As String = strTmp.Split("|"c)

            Me.txtOpRst.Text = strBuf(0) : Me.txtSpRst.Text = strBuf(1)
            Me.txtDrug1.Text = strBuf(2)
            Me.txtDrug2.Text = strBuf(3)

            Me.dtpCollDt1.Text = strBuf(4) : Me.txtCollRst1.Text = strBuf(5)
            Me.dtpCollDt2.Text = strBuf(6) : Me.txtCollRst2.Text = strBuf(7)
            Me.dtpCollDt3.Text = strBuf(8) : Me.txtCollRst3.Text = strBuf(9)
            Me.dtpCollDt4.Text = strBuf(10) : Me.txtCollRst4.Text = strBuf(11)

            Me.dtpRstDt.Text = strBuf(12) : Me.txtTnmd.Text = strBuf(13) : Me.txtViewRst.Text = strBuf(14)
            Me.txtVdRst.Text = strBuf(15) : Me.txtClRst.Text = strBuf(16) : Me.txtT2Rst.Text = strBuf(17)

            Me.txtInterRst.Text = strBuf(18)
            Me.txtRecomRst.Text = strBuf(19)

        Catch ex As Exception

        End Try
    End Sub

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_dbCn As OracleConnection, _
                                    ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm
        m_dbCn = r_dbCn
        msBcNo = rsBcNo
        msTClsCd = rsTClsCd
        msTNm = rsTNm

        Try
            sbDisplay_Data(rsBcNo, rsTClsCd, r_dbCn)

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

        Dim strVal As String = ""
        Dim strBuf() As String

        Try
            Dim strTmp As String = ""

            strVal += "[자문목적]" + vbCrLf
            strBuf = txtOpRst.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                strVal += Space(2) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
            Next

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            strVal += "[특이사항]" + vbCrLf
            strBuf = txtSpRst.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                strVal += Space(2) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
            Next

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            strVal += "[Drug administration]" + vbCrLf
            strVal += "  현투약력: "
            strBuf = txtDrug1.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                If intIdx = 0 Then
                    strVal += strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                Else
                    strVal += Space(12) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                End If
            Next

            strVal += vbCrLf

            strVal += "  병용약력: "
            strBuf = txtDrug2.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                If intIdx = 0 Then
                    strVal += strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                Else
                    strVal += Space(12) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                End If
            Next

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            strVal += "[Serum Drug concentration / Lab]" + vbCrLf
            strVal += "  채혈시각: " + dtpCollDt1.Text + Space(2) + "농도: " + (txtCollRst1.Text + " ug/ml").PadLeft(12, " "c) + Space(2)
            strVal += "  채혈시각: " + dtpCollDt2.Text + Space(2) + "농도: " + (txtCollRst2.Text + " ug/ml").PadLeft(12, " "c) + vbCrLf

            strVal += "  채혈시각: " + dtpCollDt3.Text + Space(2) + "농도: " + (txtCollRst3.Text + " ug/ml").PadLeft(12, " "c) + Space(2)
            strVal += "  채혈시각: " + dtpCollDt4.Text + Space(2) + "농도: " + (txtCollRst4.Text + " ug/ml").PadLeft(12, " "c) + vbCrLf

            strVal += vbCrLf

            strVal += "  검 사 일: " + dtpRstDt.Text + Space(5) + "검사항목:   " + txtTnmd.Text + Space(5) + "검사결과:   " + txtT2Rst.Text + vbCrLf

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            strVal += "[Result]" + vbCrLf
            strVal += "  Vd(L/kg):   " + txtVdRst.Text + Space(6) + "Cl(L/cr):   " + txtClRst.Text + Space(6) + "t2/1(hr):   " + txtT2Rst.Text + vbCrLf

            strVal += vbCrLf

            strVal += "  Interetation:" + vbCrLf
            strBuf = txtInterRst.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                strVal += Space(4) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
            Next

            strVal += vbCrLf

            strVal += "  Rcommendation:" + vbCrLf
            strBuf = txtRecomRst.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length
                strVal += Space(4) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
            Next

        Catch ex As Exception

        Finally
            fnGet_Report = strVal
        End Try

    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        msResult = ""
        mbSave = False
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim strRst As String = ""

        strRst += Me.txtOpRst.Text + "|" + Me.txtSpRst.Text + "|"
        strRst += Me.txtDrug1.Text + "|" + Me.txtDrug2.Text + "|"

        strRst += Me.dtpCollDt1.Text + "|" + Me.txtCollRst1.Text + "|"
        strRst += Me.dtpCollDt2.Text + "|" + Me.txtCollRst2.Text + "|"
        strRst += Me.dtpCollDt3.Text + "|" + Me.txtCollRst3.Text + "|"
        strRst += Me.dtpCollDt4.Text + "|" + Me.txtCollRst4.Text + "|"


        strRst += Me.dtpRstDt.Text + "|" + Me.txtTnmd.Text + "|" + Me.txtViewRst.Text + "|"
        strRst += Me.txtVdRst.Text + "|" + Me.txtClRst.Text + "|" + Me.txtT2Rst.Text + "|"

        strRst += Me.txtInterRst.Text + "|"
        strRst += Me.txtRecomRst.Text + "|"

        If DA_ST_BM.fnExe_LRS11M(msBcNo, msTClsCd, strRst, m_dbCn) Then
            msResult = fnGet_Report()
            mbSave = True
            Me.Close()
        Else
            mbSave = False
        End If

    End Sub

    Private Sub txtFocus_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOpRst.GotFocus, txtSpRst.GotFocus, txtDrug1.GotFocus, txtDrug2.GotFocus, txtCollRst1.GotFocus, txtCollRst2.GotFocus, txtCollRst3.GotFocus, txtCollRst3.GotFocus, txtCollRst4.GotFocus, txtTnmd.GotFocus, txtViewRst.GotFocus, txtVdRst.GotFocus, txtClRst.GotFocus, txtT2Rst.GotFocus, txtInterRst.GotFocus, txtRecomRst.GotFocus

        CType(sender, Windows.Forms.TextBox).SelectAll()

    End Sub

    Private Sub txtKeyDn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCollRst1.KeyDown, txtCollRst2.KeyDown, txtCollRst3.KeyDown, txtCollRst3.KeyDown, txtCollRst3.KeyDown, txtCollRst4.KeyDown, txtTnmd.KeyDown, txtViewRst.KeyDown, txtVdRst.KeyDown, txtClRst.KeyDown, txtT2Rst.KeyDown, txtTnmd.KeyDown, txtViewRst.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Select Case CType(sender, Windows.Forms.TextBox).Name
            Case "txtCollRst3"
                dtpCollDt4.Focus()
            Case "txtViewRst"
                txtVdRst.Focus()
            Case Else
                SendKeys.Send("{TAB}")
        End Select

    End Sub

    Private Sub txtKeyDnColl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpCollDt1.KeyDown, dtpCollDt2.KeyDown, dtpCollDt3.KeyDown, dtpCollDt4.KeyDown, dtpRstDt.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Select Case CType(sender, Windows.Forms.DateTimePicker).Name
            Case "txtCollRst3"
                dtpCollDt1.Focus()
            Case "dtpRstDt"
                txtTnmd.Focus()
            Case Else
                SendKeys.Send("{TAB}")
        End Select

    End Sub

End Class


Public Class DA_ST_TDM

    Public Shared Function fnGet_Rst_WBCHbPLT(ByVal rsBcNo As String, ByVal r_DbCn As OracleConnection) As String

        Dim dbCn As OracleConnection = r_DbCn
        If r_DbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try

            Dim sSql As String = ""

            sSql = ""
            sSql += "select fn_get_refrst_bm(:bcno) from dual"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return "|||"
            End If

        Catch ex As Exception

            Return "|||"
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_DbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Shared Function fnGet_Rst_TDM(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal r_DbCn As OracleConnection) As String

        Dim dbCn As OracleConnection = r_DbCn
        If r_DbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = ""
            Try
                sSql = ""
                sSql += "select rsttxt from lrs11m where bcno = :bcno and testcd = :testcd"

                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                Dim dbDa As OracleDataAdapter
                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                End With

                Dim dt As New DataTable
                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Return ""
            End Try

        Catch ex As Exception
            Return ""
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_DbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Shared Function fnExe_LRS11M(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsRst As String, ByVal r_DbCn As OracleConnection) As Boolean

        Dim dbCn As OracleConnection = r_DbCn
        If r_DbCn Is Nothing Then dbCn = GetDbConnection()
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
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd

                .ExecuteNonQuery()

                sSql = ""
                sSql += "insert into lrs11m(  bcno,  testcd,  rsttxt,  rstid, rstdt )"
                sSql += "            values( :bcno, :testcd, :rsttxt, :rstid, fn_ack_sysdate)"

                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                .Parameters.Add("rsttxt", OracleDbType.Varchar2).Value = rsRst
                .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

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
            If r_DbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function
End Class

