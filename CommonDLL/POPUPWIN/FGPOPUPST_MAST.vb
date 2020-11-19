Imports Oracle.DataAccess.Client

Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports DBORA.DbProvider

Public Class FGPOPUPST_MAST
    Private Const msFile As String = "File : FGPOPUPST_MAST.vb, Class : FGPOPUPST_MAST" & vbTab

    Private m_dbCn As OracleConnection
    Private msBcNo As String = ""
    Private msTestCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

    Private Function fnHan_PadRight(ByVal rsBuf As String, ByVal riLen As Integer) As String
        Dim a_btBuf As Byte() = System.Text.Encoding.Default.GetBytes(rsBuf)
        Dim sReturn As String = ""

        If a_btBuf.Length > riLen Then
            sReturn = System.Text.Encoding.Default.GetString(a_btBuf)
        Else
            sReturn = System.Text.Encoding.Default.GetString(a_btBuf) + "".PadRight(riLen - a_btBuf.Length)
        End If

        Return sReturn
    End Function


    Private Function fnGet_Verify() As String

        Dim sValue As String = ""

        Dim dt As DataTable = (New DA_ST_MAST).fnGet_Result_Test(m_dbCn, msBcNo, msTestCd)

        If dt.Rows.Count < 1 Then Return ""


        sValue += Space(43) + "ALLERGEN ITEM" + Space(33) + "LU/ML" + Space(4) + "CLASS" + vbCrLf
        sValue += "-".PadLeft(107, "-"c) + vbCrLf

        For ix As Integer = 0 To dt.Rows.Count - 1
            sValue += Space(4) + (ix + 1).ToString.PadLeft(2, " "c) + Space(3)
            sValue += fnHan_PadRight(dt.Rows(ix).Item("tnmp").ToString, 40)
            sValue += fnHan_PadRight(dt.Rows(ix).Item("tnmd").ToString, 40)
            sValue += fnHan_PadRight(dt.Rows(ix).Item("viewrst").ToString, 10)
            sValue += fnHan_PadRight(dt.Rows(ix).Item("eqflag").ToString, 10)

            sValue += vbCrLf
        Next

        Return sValue

    End Function

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_dbCn As OracleConnection, _
                                   ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            m_dbCn = r_dbCn
            msBcNo = rsBcNo
            msTestCd = rsTClsCd
            msTNm = rsTNm

            Dim STU_StDataInfo As STU_StDataInfo
            Dim al_return As New ArrayList

            STU_StDataInfo = New STU_StDataInfo
            STU_StDataInfo.Data = fnGet_Verify()
            STU_StDataInfo.Alignment = 0
            al_return.Add(STU_StDataInfo)

            STU_StDataInfo = Nothing

            Return al_return

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New ArrayList

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Function

End Class

Public Class DA_ST_MAST
    Private Const msFile As String = "File : FGPOPUPST_MAST.vb, Class : DA_ST_MAST" & vbTab

    Public Function fnGet_Result_Test(ByVal r_dbCn As OracleConnection, ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
        Dim sFn As String = "fnGet_Result_Test"

        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim dbDa As OracleDataAdapter

            Dim dt As New DataTable

            Dim sSql As String = ""

            sSql += "SELECT r.testcd, r.viewrst, r.eqflag, f.tnmd, f.tnmp, NVL(dispseql, 999) dispseql"
            sSql += "  FROM lr010m r, lf060m f"
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd LIKE :testcd || '%'"
            sSql += "   AND r.testcd = f.testcd"
            sSql += "   AND r.spccd  = f.spccd"
            sSql += "   AND r.tkdt  >= f.usdt"
            sSql += "   AND r.tkdt  <  f.uedt"
            sSql += "   AND f.tcdgbn = 'C'"
            sSql += " ORDER BY dispseql, testcd"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
            End With

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

End Class
