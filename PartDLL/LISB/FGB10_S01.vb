
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Public Class FGB10_S01
    Private Const msFile As String = "File : FGB10_NEWPOP01.vb, Class : FGB10_NEWPOP01" & vbTab
    Private mobjDAF As New LISAPP.APP_F_COMCD
    Private mal_rtnValue As New ArrayList

    Public Function fn_DisplayPop(ByVal rofrm As Windows.Forms.Form, ByVal riGbn As Integer, Optional ByVal rsSelf As String = "") As ArrayList
        If riGbn = 0 Then
            Me.txtWorkGbn.Text = " 반 납 "
            Me.chkCost.Checked = False
            Me.chkCost.Enabled = False

        ElseIf riGbn = 1 Then
            Me.txtWorkGbn.Text = " 폐 기 "
            Me.chkCost.Checked = False
            Me.chkCost.Enabled = True
        End If

        sb_SetComboDt(riGbn, rsSelf)

        Me.ShowDialog(rofrm)

        Return mal_rtnValue

    End Function

    Public Sub sb_SetComboDt(ByVal riGbn As Integer, Optional ByVal rsSelf As String = "")
        Dim sFn As String = "sb_SetComboDt"
        ' 콤보 데이터 생성
        Try
            Dim DTable As DataTable

            DTable = CDHELP.FGCDHELPFN.fn_CmtList(riGbn, rsSelf)

            cboResn.Items.Clear()
            If DTable.Rows.Count > 0 Then
                With cboResn
                    For i As Integer = 0 To DTable.Rows.Count - 1
                        .Items.Add(DTable.Rows(i).Item("cmt"))
                    Next
                End With
            Else
                Exit Sub
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnExe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExe.Click
        Dim ls_rtnreqid As String
        Dim ls_rtnreqnm As String
        Dim ls_rtnCode As String
        Dim ls_rtnCmt As String
        Dim ls_refund As String = "0"c
        Dim sFn As String = "Private Sub btnExe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExe.Click"

        Try
            ls_rtnreqid = Me.txtReqid.Text
            ls_rtnreqnm = Me.txtReqNm.Text
            ls_rtnCode = Ctrl.Get_Code(Me.cboResn)
            ls_rtnCmt = Me.txtCmt.Text

            If Me.chkCost.Checked = True Then
                ls_refund = "1"c
            Else
                ls_refund = "0"c
            End If

            If ls_rtnreqid.Length() < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "반납/폐기 의뢰자는 필수 입력 입니다.")
                txtReqid.Focus()
                Return
            End If

            If ls_rtnreqnm.Length() < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "존재 하지 않는 사용자 입니다.")
                txtReqid.Focus()
                Return
            End If

            If ls_rtnCode.Length() < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "반납/폐기 사유코드는 필수 입력 입니다.")
                cboResn.Focus()
                Return
            End If

            If ls_rtnCmt.Length() < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "Comment는 필수 입력 입니다.")
                txtCmt.Focus()
                Return
            End If

            mal_rtnValue.Add(ls_rtnreqid)
            mal_rtnValue.Add(ls_rtnreqnm)
            mal_rtnValue.Add(ls_rtnCode)
            mal_rtnValue.Add(ls_rtnCmt)
            mal_rtnValue.Add(ls_refund)

            Me.Close()
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try


    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mal_rtnValue = New ArrayList
        Me.Close()
    End Sub

    Private Sub cboResn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboResn.SelectedIndexChanged
        Dim ls_ComTxt As String
        Dim li_ChkStr As Integer

        ls_ComTxt = Ctrl.Get_Name(Me.cboResn.Text)

        li_ChkStr = ls_ComTxt.IndexOf("기타")

        If li_ChkStr > -1 Then
            txtCmt.Enabled = True
            txtCmt.Text = ""
            txtCmt.Focus()
            txtCmt.BackColor = Color.White
            txtCmt.ForeColor = Color.Black

        Else
            txtCmt.Enabled = False
            txtCmt.Text = ls_ComTxt
            txtCmt.Focus()
            txtCmt.BackColor = Color.White
            txtCmt.ForeColor = Color.Black
        End If

    End Sub

    Private Sub FGB10_NEWPOP01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnExe_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB10_NEWPOP01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtCmt.BackColor = Color.White
        txtWorkGbn.BackColor = Color.White

        txtCmt.ForeColor = Color.Black
        txtWorkGbn.ForeColor = Color.Red
    End Sub

    Private Sub txtReqid_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReqid.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim ls_RtnId As String = txtReqid.Text.Trim()
            Dim ls_RtnNm As String

            ls_RtnNm = CDHELP.FGCDHELPFN.fn_RtnDoc(ls_RtnId)

            If ls_RtnNm.Length() < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰의사 아이디가 정확하지 않습니다.")
                txtReqid.Focus()
                txtReqid.SelectAll()
                txtReqNm.Text = ""
            Else
                txtReqNm.Text = ls_RtnNm
                cboResn.Focus()
            End If

        End If

    End Sub

    Private Sub btnPop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPop.Click
        Dim sFn As String = "Private Sub btnPop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatPop.Click"
        Dim objHelp As New CDHELP.FGCDHELP99
        Dim lal_Header As New ArrayList
        Dim lal_Arg As New ArrayList
        Dim li_RtnCnt As Integer = 3
        Dim lal_Rtn As New ArrayList
        Dim ls_Id As String = txtReqid.Text

        Try
            lal_Header.Add("아이디")
            lal_Header.Add("유저명")
            lal_Header.Add("진료과")

            lal_Arg.Add(" "c)


            lal_Rtn = objHelp.fn_DisplayPop(Me, "혈액반납폐기 ", "fn_PopGetRtnReqList", lal_Arg, lal_Header, li_RtnCnt, "")

            If lal_Rtn.Count > 0 Then
                txtReqid.Text = lal_Rtn(0).ToString
                txtReqNm.Text = lal_Rtn(1).ToString

                ' 구조체로 넘겨 받았을 경우 
                'With CType(lal_Rtn(0), CDHELP.clsRtnData)
                '    txtRegno.Text = .RTNDATA0
                '    txtPatNm.Text = .RTNDATA1
                'End With
            End If
        Catch ex As Exception
            Fn.log("" & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub
End Class
