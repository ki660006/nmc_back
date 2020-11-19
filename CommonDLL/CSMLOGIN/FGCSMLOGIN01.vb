Imports System.Windows.Forms

Imports COMMON.CommLogin.LOGIN

Public Class FGCSMLOGIN01

    Private m_frm As Windows.Forms.Form
    Private m_DbCn As OleDb.OleDbConnection
    Private ms_UsrId As String = ""
    Private mbSave As Boolean = False

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_DbCn As OleDb.OleDbConnection, ByVal rsUsrId As String) As String
        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm
        m_DbCn = r_DbCn
        ms_UsrId = rsUsrId

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            Me.ShowDialog(r_frm)

            Dim sReturn As String = ""

            If mbSave Then
                sReturn = Me.txtCsmDn.Text
            End If

            Return sReturn

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
            Return ""
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try

    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            '================================================
            '접속
            'BOOL CSMConnect();
            'return: true or false (boolean)
            '================================================
            Dim bRet As Boolean = CSMConnect(PRG_CONST.CSM_SERVER_IP, CLng(PRG_CONST.CSM_SERVER_port))
            If bRet = False Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "CSM 서버 접속 실패!!" + vbCrLf + vbCrLf + "[" & CSMGetErrorCode & "]-" & CSMGetErrorMsg())
                Return
            End If

            '================================================
            '사용자 PC 내의 비밀번호 확인
            'BSTR CSMVerifyPassword(BSTR dn, BSTR password);
            'return: user cert DN (string data)
            'parameter:user cert DN, cert password
            '================================================
            Dim sDn As String = CSMVerifyPassword(Me.txtCsmDn.Text, Me.txtCsmPw.Text)
            If sDn = "" Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "인증서 암호가 올바르지 않습니다.!!" + vbCrLf + vbCrLf & "[" & CSMGetErrorCode & "]-" & CSMGetErrorMsg())
                Return
            End If

            '================================================
            '접속 종료
            'BOOL CSMDisconnect();
            'return: true or false (boolean)
            '================================================
            bRet = CSMDisconnect()
            If bRet = False Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "CSM 서버 접속 종료 실패!!" + vbCrLf + vbCrLf + "[" & CSMGetErrorCode & "]-" & CSMGetErrorMsg())
                Return
            End If

            mbSave = True
            Me.Close()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtUsrId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsrId.KeyDown, txtCsmPw.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        SendKeys.Send("{TAB}")

    End Sub


    Private Sub txtUsrId_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsrId.Validated

        Try

            Me.txtUsr_csm.Text = DA_CSM.fnGet_CSM_UsrId(Me.txtUsrId.Text, m_DbCn)
            If Me.txtUsr_csm.Text = "" Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "인증서ID가 존재하지 않습니다.!!" + vbCrLf + vbCrLf + " 확인해 주세요.")
                Return
            End If

            '================================================
            '접속
            'BOOL CSMConnect();
            'return: true or false (boolean)
            '================================================
            Dim bRet As Boolean = CSMConnect(PRG_CONST.CSM_SERVER_IP, CLng(PRG_CONST.CSM_SERVER_PORT))
            If bRet = False Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "CSM 서버 접속 실패!!" + vbCrLf + vbCrLf + "[" & CSMGetErrorCode & "]-" & CSMGetErrorMsg())
                Return
            End If

            '=====================================================================================================================
            '인증서 다운로드
            '사용자가 인증서를 다운로드 한 뒤 사용이 끝났다면, 로그아웃 시점에서 CSMLocalDelCert() 함수를 호출 하여 사용자 PC의
            '인증서를 삭제 해주시는것이 일관성있는 인증서 관리에 좋습니다.
            'BSTR CSMGetCert(BSTR id);
            'return: user cert DN (string data)
            'parameter:user id
            '=====================================================================================================================
            Dim sDn As String = CSMGetCert(Me.txtUsr_csm.Text)

            If sDn <> "" Then
                Me.txtCsmDn.Text = sDn
            Else
                Me.txtCsmDn.Text = ""
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "서버의 사용자 인증서 가져오기 실패!!" + vbCrLf + vbCrLf + "[" & CSMGetErrorCode & "]-" & CSMGetErrorMsg())
            End If

            '================================================
            '접속 종료
            'BOOL CSMDisconnect();
            'return: true or false (boolean)
            '================================================
            bRet = CSMDisconnect()
            If bRet = False Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "CSM 서버 접속 종료 실패!!" + vbCrLf + vbCrLf + "[" & CSMGetErrorCode & "]-" & CSMGetErrorMsg())
                Return
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGCSMLOGIN01_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Me.txtUsrId.Text = ms_UsrId

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        mbSave = False
        Me.Close()
    End Sub
End Class

Public Class DA_CSM

    Public Shared Function fnGet_CSM_UsrId(ByVal rsUsrId As String, ByVal r_DbCn As OleDb.OleDbConnection) As String

        Dim oledbcn As OleDb.OleDbConnection = r_DbCn
        Dim oledbda As OleDb.OleDbDataAdapter
        Dim oledbcmd As New OleDb.OleDbCommand

        Dim dt As New DataTable

        Dim sSql As String = ""
        Try
            sSql = ""
            sSql += "SELECT empid FROM ccusermt WHERE userid = ?"

            oledbcmd.Connection = oledbcn
            oledbcmd.CommandType = CommandType.Text
            oledbcmd.CommandText = sSql

            oledbda = New OleDb.OleDbDataAdapter(oledbcmd)

            With oledbda
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("empid", OleDb.OleDbType.VarChar).Value = rsUsrId
            End With

            dt.Reset()
            oledbda.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        End Try

    End Function

End Class