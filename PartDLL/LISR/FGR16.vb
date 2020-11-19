'>> 등록번호 변경
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommFN
Imports COMMON.CommConst
Imports SYSIF01
Imports LISAPP

Public Class FGR16
    Private Const msFile As String = "File : FGR16.vb, Class : FGR16" & vbTab

    Private Sub sbReg()

        Dim sFn As String = "sbChange()"
        Dim bRet As Boolean = False

        Try

            Dim sTableNM As String() = {"lj010h", "lj010m", _
                                        "lj011h", "lj011m", _
                                        "lm010h", "lm010m", _
                                        "lr010h", "lr010m", _
                                        "lb040m", "lb043m", "lb043h", _
                                        "slxboutt", "mdresult"}

            If Me.txtRegno.Text = "" Then
                MsgBox("등록번호를 입력하세요.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If Me.txtRegNo_chg.Text = "" Then
                MsgBox("변경할 등록번호를 입력하세요.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If MsgBox("변경하시겠습니까?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim sBcNos As String = ""
            Dim sDateS As String = dtpDateS.Text.Replace("-", "")
            Dim sDateE As String = dtpDateE.Text.Replace("-", "")

            With spdOrdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                    .Col = .GetColFromID("jobgbn") : Dim sJobGbn As String = .Text

                    If sChk = "1" Then
                        If sBcNos <> "" Then sBcNos += ","
                        sBcNos += sBcNo
                    End If
                Next

                bRet = APP_R.ChangePatFn.fnExe_Change_Regnoe(Me.txtRegno.Text, Me.txtRegNo_chg.Text, sTableNM, sBcNos, _
                                                             Me.txtPatnm_chg.Text, Me.txtIdNoL_chg.Text, Me.txtIdNoR_chg.Text, USER_INFO.USRID, sDateS, sDateE)

                If bRet = False Then
                    MsgBox("정보변경에 실패했습니다.!!", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("정보변경에 성공했습니다.!!", MsgBoxStyle.Information, Me.Text)
                End If

            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    Private Sub sbDisplay_Date(ByVal rsRegno As String)
        Dim sFn As String = "sbDisplay_Date(ByVal rsRegno As String)"

        Dim sDateS As String = ""
        Dim sDateE As String = ""

        Dim dt As New DataTable

        Try
            sDateS = Me.dtpDateS.Text.Replace("-", "")
            sDateE = Me.dtpDateE.Text.Replace("-", "")

            dt = APP_R.ChangePatFn.fnGet_Change_PatInfo(sDateS, sDateE, rsRegno)

            Ctrl.DisplayAfterSelect(spdOrdList, dt)

            With spdOrdList
                For i As Integer = 0 To dt.Rows.Count - 1
                    .Row = i + 1
                    .Col = .GetColFromID("rstflag")
                    If .Text = "3" Then
                        .ForeColor = FixedVariable.g_color_FN
                        .Text = "◆"
                    ElseIf .Text = "2" Then
                        .Text = "○"
                    ElseIf .Text = "1" Then
                        .Text = "△"
                    Else
                        .Text = ""
                    End If

                Next
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_ChangeList()
        Dim sFn As String = "sbDisplay_ChangeList"

        Dim sDateS As String = Me.dtpDateS_qry.Text.Replace("-", "")
        Dim sDateE As String = Me.dtpDateE_qry.Text.Replace("-", "")
        Dim dt As New DataTable

        Try

            dt = APP_R.ChangePatFn.fnGet_Change_PatList(sDateS, sDateE, Me.txtRegNo_qry.Text)
            Ctrl.DisplayAfterSelect(Me.spdUpList, dt)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Sub btnChg_regno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChg_regno.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If txtRegNo_chg.Text.Length <> PRG_CONST.Len_RegNo Then
            MsgBox("등록번호가 틀립니다.  확인하세요.!!")
            txtRegNo_chg.Focus()
            Return
        End If

        sbReg()
        Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub


    Private Sub txtRegno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown, txtRegNo_chg.KeyDown
        Dim sFn As String = "txtRegno_KeyDown"

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Try
            Dim sRegno As String = Trim(CType(sender, System.Windows.Forms.TextBox).Text)

            If sRegno = "" Then
                MsgBox("등록번호를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                CType(sender, System.Windows.Forms.TextBox).Focus()
                Exit Sub
            End If

            If IsNumeric(sRegno.Substring(0, 1)) Then
                sRegno = sRegno.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            Else
                sRegno = sRegno.Substring(0, 1).ToUpper + sRegno.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
            End If
            CType(sender, System.Windows.Forms.TextBox).Text = sRegno

            Dim dt As New DataTable
            dt = APP_R.ChangePatFn.fnGet_PatInfo(sRegno)

            If dt.Rows.Count > 0 Then
                If CType(sender, System.Windows.Forms.TextBox).Name.ToLower = "txtregno" Then
                    Me.lblPatnm.Text = dt.Rows(0).Item("patnm").ToString
                Else
                    Me.txtPatnm_chg.Text = dt.Rows(0).Item("patnm").ToString
                End If

            Else
                dt = OCSAPP.OcsLink.Pat.fnGet_Patinfo(sRegno, "")
                If dt.Rows.Count > 0 Then
                    If CType(sender, System.Windows.Forms.TextBox).Name.ToLower = "txtregno" Then
                        Me.lblPatnm.Text = dt.Rows(0).Item("suname").ToString
                    Else
                        Me.txtPatnm_chg.Text = dt.Rows(0).Item("suname").ToString
                    End If

                Else

                    If CType(sender, System.Windows.Forms.TextBox).Name.ToLower = "txtregno" Then
                        Me.lblPatnm.Text = ""
                        Me.lblIdnoL.Text = ""
                        Me.lblIdNoR.Text = ""
                    End If

                    Me.txtPatnm_chg.Text = ""
                    Me.txtIdNoL_chg.Text = ""
                    Me.txtIdNoR_chg.Text = ""


                    MsgBox("등록된 환자가 없습니다..", MsgBoxStyle.Information, Me.Text)
                End If
            End If

            CType(sender, System.Windows.Forms.TextBox).SelectAll()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnQuery_regno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery_regno.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        sbDisplay_Date(Me.txtRegno.Text)

        Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub FRG16_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpDateS.Value = Now
        dtpDateE.Value = Now

        dtpDateS_qry.Value = Now
        dtpDateE_qry.Value = Now

    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click, btnQueryC.Click

        sbDisplay_ChangeList()

    End Sub

    Private Sub txtRegNo_qry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo_qry.KeyDown

        If CType(sender, System.Windows.Forms.TextBox).Text = "" Then Return

        If e.KeyCode = Windows.Forms.Keys.Enter Then
            Dim sRegno As String = Trim(CType(sender, System.Windows.Forms.TextBox).Text)

            If sRegno = "" Then
                MsgBox("등록번호를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                CType(sender, System.Windows.Forms.TextBox).Focus()
                Exit Sub
            End If

            If IsNumeric(sRegno.Substring(0, 1)) Then
                sRegno = sRegno.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            Else
                sRegno = sRegno.Substring(0, 1).ToUpper + sRegno.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
            End If

            btnQuery_Click(Nothing, Nothing)

            Me.txtRegNo_qry.SelectAll()
        End If
    End Sub

    Private Sub chkSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSel.Click

        With spdOrdList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : .Text = IIf(chkSel.Checked, "1", "").ToString
            Next
        End With

    End Sub

    Private Sub FGR_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub
End Class