Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports DBORA.DbProvider

Public Class FGFINAL_CMT2
    Public msBcNo As String = ""
    Public msPartSlip As String = ""
    Public msCmt As String = ""
    Private msRetValue As String = ""

    Public Function Display_Result() As String

        sbDisplay_Cmt()
        sbDisplay_Data(msBcNo, msCmt)

        Me.ShowDialog()

        Return msRetValue

    End Function

    Private Sub sbDisplay_Cmt()

        Try
            Dim dt As New DataTable

            dt = (New LISAPP.APP_F_COLLTKCD).fnGet_CollTK_Cancel_ContInfo("B")

            Me.cboCmtCd.Items.Clear()
            Me.cboCmtCd.Items.Add("")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboCmtCd.Items.Add("[" + dt.Rows(ix).Item("cmtcd").ToString + "] " + dt.Rows(ix).Item("cmtcont").ToString)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcNo As String, ByVal rsCmt As String)
        Me.lblBcNo.Text = rsBcNo.Substring(0, 8) + "-" + rsBcNo.Substring(8, 2) + "-" + rsBcNo.Substring(10, 4) + "-" + rsBcNo.Substring(14, 1)

        'Dim sTmp As String = rsCmt.Substring(rsCmt.IndexOf("[") + 1).Trim

        Dim sTmp As String = rsCmt.Split("@"c)(1)
        sTmp = sTmp.Substring(0, sTmp.Length - 1)

        Dim sBuf() As String = sTmp.Split("|"c)

        For ix As Integer = 0 To sBuf.Length - 1

            Dim sTnmd As String = sBuf(ix).Substring(0, sBuf(ix).IndexOf("{")).Trim
            sTmp = sBuf(ix).Substring(sBuf(ix).IndexOf("{") + 1).Trim
            Dim sOrgRst As String = sTmp.Split("/"c)(0).Trim
            Dim sViewRst As String = sTmp.Split("/"c)(1).Trim

            With spdList
                If ix = 0 Then .MaxRows = sBuf.Length
                .Row = ix + 1
                .Col = 1 : .Text = sTnmd
                .Col = 2 : .Text = sOrgRst
                .Col = 3 : .Text = sViewRst.Substring(0, sViewRst.Length - 1)
            End With
        Next

    End Sub

    Private Sub cboCmtCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCmtCd.SelectedIndexChanged

        If Me.cboCmtCd.Text <> "" Then
            'Me.txtCmtCont.Text = cboCmtCd.Text.Substring(cboCmtCd.Text.IndexOf("]") + 1).Trim + vbCrLf
            Me.txtCmtCont.Text = cboCmtCd.Text.Substring(cboCmtCd.Text.IndexOf("]") + 1).Trim + vbCrLf + "[통보자:  ,피통보자: ]"
        End If

    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = ""

        Try
            If Me.txtCmtCont.Text = "" Then
                MsgBox("최종보고 수정 사유가 입력되지 않았습니다." + vbCrLf + "최종보고 수정 사유를 입력하세요.", MsgBoxStyle.Information)
                cboCmtCd.Focus()
                Return
            End If

            Dim sCmtCd As String = ""

            If Me.cboCmtCd.Text.Trim = "" Then
            Else
                sCmtCd = Ctrl.Get_Code(cboCmtCd)
            End If

            If DA_FINAL_CMT.ExecuteDo(msBcNo, sCmtCd, Me.txtCmtCont.Text) Then
                msRetValue = "OK" + "|" + Me.txtCmtCont.Text
                Me.Close()
            End If

        Catch ex As Exception
            MsgBox(sFn + " " + ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        msRetValue = ""
        Me.Close()
    End Sub

    Private Sub FGDCMT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                btnReg_Click(Nothing, Nothing)
            Case Windows.Forms.Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

End Class

Public Class DA_FINAL_CMT2
    Private Const msFile As String = "File : FGFINAL_CMT2.vb, Class : DA_FINAL_CMT2" & vbTab

    Public Shared Function ExecuteDo(ByVal rsBcNo As String, ByVal rsCmtCd As String, ByVal rsCmtCont As String) As Boolean
        Dim sFn As String = " Public Shared Function ExecuteDo(String, String, String)"
        Dim oleDbCn As OracleConnection = GetDbConnection()
        Dim oleDbTrans As oracleTransaction = oleDbCn.BeginTransaction()

        Dim dt As New DataTable

        Dim sSql As String = ""
        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            sSql += "INSERT INTO lr052m(  bcno,  cmtcd,  cmtcont, regdt,           regid, seq )"
            sSql += "            VALUES( :bcno, :cmtcd, :cmtcont, fn_ack_sysdate, :regid, sq_lr052m.nextval)"

            Dim oleDbCmd As New oracleCommand

            With oleDbCmd
                .Connection = oleDbCn
                .Transaction = oleDbTrans
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()

                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("cmtcd", OracleDbType.Varchar2).Value = rsCmtCd
                .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = rsCmtCont
                .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID

                .ExecuteNonQuery()
            End With

            oleDbTrans.Commit()

            Return True

        Catch ex As Exception
            oleDbTrans.Rollback()
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return False
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function
End Class
