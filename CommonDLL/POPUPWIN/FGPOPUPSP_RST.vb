'>> 특이결과 등록 및 조회
Imports System.Windows.Forms
Imports System.Drawing

Imports DA01
Imports COMMON.SVar.Login
Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON

Imports DA01.OcsLink.SData
Imports DA01.OcsLink.Pat
Imports DA01.OcsLink.Ord

Public Class FGPOPUPSP_RST
    Private Const msFile As String = "File : FGR15.vb, Class : FGR15" & vbTab
    '---
    Private mbBloodBankYN As Boolean

    Private msTClsDir As String = "\XML"
    Private msTClsFile As String = Application.StartupPath + msTClsDir + "\FGR02_TSECTIONCD.XML"
    Public msLocation As String = "0"   '1:병동, 2:외래
    Public msJobGbn As String = ""      '1:부적합검체 취소, A:특이결과, 0:부적합검체조회
    Private mbCheckMode As Boolean = False

    Private Sub sbDisplay_bccls()

        Dim sFn As String = "Sub sbDisplay_bccls()"
        Dim dt As DataTable

        Try
            Dim strTmp As String
            Dim strSect As String = "", arlSect As New ArrayList

            'End If

            dt = DA01.CommQry.DA_LF.fnGet_Bccls_List()

            cboBccls.Items.Clear()
            For intIdx As Integer = 0 To dt.Rows.Count - 1
                strTmp = "[" + dt.Rows(intIdx).Item("bcclscd").ToString + "] "
                cboBccls.Items.Add(strTmp + dt.Rows(intIdx).Item("bcclsnmd").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_CmtCont()
        Dim sFn As String = "Sub sbDisplay_CmtCont()"
        Dim dt As DataTable

        Try

            dt = DA01.CommQry.DA_LF.fnGet_Etc_CdLists(Me.msJobGbn)

            cboCmt.Items.Clear()
            For intIdx As Integer = 0 To dt.Rows.Count - 1
                cboCmt.Items.Add(dt.Rows(intIdx).Item("cmtcont").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbClear()

        Me.lblno.Text = ""
        Me.lblPatnm.Text = ""
        Me.lblDeptnm.Text = ""
        Me.lblWardroom.Text = ""
        Me.txtCmtCont.Text = ""

        Me.spdRsltList.MaxRows = 0
    End Sub

    Private Sub sbDisplayPatInfo(ByVal rsBcno As String)
        Dim sFn As String = "sbDisplayPatInfo(ByVal rsBcno As String)"

        Try

            Dim dt As DataTable = New DataTable

            dt = DA_R.fnGet_Abnormal_RstInfo(rsBcno, "")

            If dt.Rows.Count > 0 Then
                Me.lblno.Text = dt.Rows(0).Item("regno").ToString
                Me.lblPatnm.Text = dt.Rows(0).Item("patnm").ToString
                Me.lblDeptnm.Text = dt.Rows(0).Item("deptnm").ToString
                Me.lblWardroom.Text = dt.Rows(0).Item("wardroom").ToString

            Else
                Me.lblno.Text = ""
                Me.lblPatnm.Text = ""
                Me.lblDeptnm.Text = ""
                Me.lblWardroom.Text = ""
                Me.txtCmtCont.Text = ""
            End If

            Ctrl.DisplayAfterSelect(Me.spdRsltList, dt)
            If dt.Rows.Count > 0 Then sbDisplayChangeSpd(dt)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Sub sbDisplayChangeSpd(ByVal r_dt As DataTable)
        Dim sFn As String = "sbDisplayChangeSpd()"
        Dim sTestNm As Object
        Try

            With Me.spdRsltList


                For i As Integer = 0 To r_dt.Rows.Count - 1
                    .Row = i + 1

                    Select Case r_dt.Rows(i).Item("tcdgbn").ToString
                        Case "P"
                        Case "C"
                            .GetText(.GetColFromID("tnmd"), i + 1, sTestNm)
                            .SetText(.GetColFromID("tnmd"), i + 1, "... " & Convert.ToString(sTestNm))
                            sTestNm = Nothing
                        Case "S"
                            If r_dt.Rows(i).Item("ptclscd").ToString <> "" Then
                                If r_dt.Rows(i).Item("ptclscd").ToString <> r_dt.Rows(i).Item("tclscd").ToString Then
                                    .GetText(.GetColFromID("tnmd"), i + 1, sTestNm)
                                    .SetText(.GetColFromID("tnmd"), i + 1, "    " & Convert.ToString(sTestNm))
                                    sTestNm = Nothing

                                End If
                            End If
                    End Select


                    .Col = .GetColFromID("n")
                    Select Case r_dt.Rows(i).Item("n").ToString
                        Case "L"

                            .BackColor = FixedVariable.g_color_LM_Bg
                            .ForeColor = FixedVariable.g_color_LM_Fg
                        Case "H"

                            .BackColor = FixedVariable.g_color_HM_Bg
                            .ForeColor = FixedVariable.g_color_HM_Fg
                    End Select


                    If r_dt.Rows(i).Item("p").ToString = "P" Then
                        .Col = .GetColFromID("p")

                        .BackColor = FixedVariable.g_color_PM_Bg
                        .ForeColor = FixedVariable.g_color_PM_Fg
                    End If

                    If r_dt.Rows(i).Item("d").ToString = "D" Then
                        .Col = .GetColFromID("d")

                        .BackColor = FixedVariable.g_color_DM_Bg
                        .ForeColor = FixedVariable.g_color_DM_Fg
                    End If

                    If r_dt.Rows(i).Item("c").ToString = "C" Then
                        .Col = .GetColFromID("c")

                        .BackColor = FixedVariable.g_color_CM_Bg
                        .ForeColor = FixedVariable.g_color_CM_Fg
                    End If

                    If r_dt.Rows(i).Item("a").ToString = "A" Then
                        .Col = .GetColFromID("a")

                        .BackColor = FixedVariable.g_color_AM_Bg
                        .ForeColor = FixedVariable.g_color_AM_Fg
                    End If

                Next
            End With
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Sub FGR15_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)

        End Select
    End Sub

    Private Sub FGR15_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        dtpStart.Value = Now
        dtpEnd.Value = Now

        Me.spdRsltList.MaxRows = 0
        Me.spdSpcList.MaxRows = 0

        Me.spdSpcList.TypeEditMultiLine = True
        Dim sTmp As String = ""
        'Dim alRmSect As New ArrayList

        'alRmSect.Add(Const_Sect_BloodBank)
        'alRmSect.Add(Const_Sect_MicroBio)

        sbDisplay_bccls()
        sbDisplay_CmtCont()

        sTmp = DP_Common.getOneElementXML(msTClsDir, msTClsFile, "TSECTCD")

        cboBccls.SelectedIndex = CInt(IIf(sTmp = "", -1, sTmp))

        Me.rdoA.Checked = True
        Me.cboBccls.SelectedIndex = -1
        cboBccls.Enabled = False

        Dim dtWord As New DataTable

        dtWord = fnGet_WardList()

        If dtWord.Rows.Count > 0 Then
            For i As Integer = 1 To dtWord.Rows.Count
                Me.cboWard.Items.Add(dtWord.Rows(i - 1).Item("wardno").ToString + "/" + dtWord.Rows(i - 1).Item("wardnm").ToString)
            Next
        End If

        Me.spdSpcList.Col = Me.spdSpcList.GetColFromID("tnmd")
        Me.spdSpcList.ColHidden = True

        Me.mnuDel_sp.Enabled = False

        If Me.Text = "특이결과 등록 및 조회" Then
            Me.mnuDel_sp.Enabled = True
        End If

    End Sub

    Private Sub FGR15_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.txtSearch.Focus()
    End Sub


    Private Sub btnJCanCel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim bUnChecked As Boolean = False

        With spdRsltList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                If sChk <> "1" Then
                    bUnChecked = True
                    Exit For
                End If
            Next
        End With

        If bUnChecked Then
            MsgBox("접수취소는 검사항목 단위로 취소할 수 없습니다.!!" + vbCrLf + "모든 검사항목을 선택해 주세요.", , "접수취소")
        Else
            sbCanCel("J")
        End If

    End Sub

    Private Sub btnRCanCel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sbCanCel("R")
    End Sub



    Private Sub sbCanCel(ByVal rsCancelGbn As String)

        Dim sFn As String = "sbCanCel(ByVal rCancelGbn As String))"
        Dim alOrdList As New ArrayList
        Try
            If Me.txtCmtCont.Text = "" Then
                MsgBox("조치사항을 입력하세요", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            If MsgBox("취소 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                Exit Sub
            End If

            fnSelOrdList(alOrdList, rsCancelGbn)

            If alOrdList.Count > 0 Then
                With (New DA01.DataAccess.DB_CJ_Cancel)
                    .CancelTItem = alOrdList
                    .CancelRMK = Me.txtCmtCont.Text
                    .CancelCd = ""
                    If txtCmt.Text <> "" Then .CancelCd = "1" + txtCmtCd.Text

                    If rsCancelGbn = "J" = True Then
                        .ExecuteDo(enumCANCEL.접수취소, USER_INFO.USRID)
                    Else
                        .ExecuteDo(enumCANCEL.REJECT, USER_INFO.USRID)
                    End If

                    If .ErrFlag Then
                        Throw (New Exception(.ErrMsg))

                    Else

                        Dim objList As New DataAccess.J01.clsCancelTItem

                        sbRegSPcIns(CType(alOrdList(0), DataAccess.J01.clsCancelTItem).BCNO, CType(alOrdList(0), DataAccess.J01.clsCancelTItem).SPCCD, rsCancelGbn)

                        Me.lblno.Text = ""
                        Me.lblPatnm.Text = ""
                        Me.lblDeptnm.Text = ""
                        Me.lblWardroom.Text = ""
                        Me.txtCmtCont.Text = ""

                        MsgBox("정상적으로 취소되었습니다.", MsgBoxStyle.Information, Me.Text)
                        sbClear()
                        btnExit_ButtonClick(Nothing, Nothing)

                    End If
                End With

            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Function fnSelOrdList(ByRef aoOrdList As ArrayList, ByVal rsCancelGbn As String) As Boolean

        Dim sFn As String = "Private Sub fnSelOrdList(ByRef aoOrdList As ArrayList)"

        Dim intCnt As Integer
        Dim objOrdList As DA01.DataAccess.J01.clsCancelTItem
        Dim strRstStat As String = ""
        Dim strOrdNm As String = ""
        Dim objDTable As DataTable

        Try
            fnSelOrdList = False

            If rsCancelGbn = "J" Then
                Dim sBcNo As String = txtSearch.Text.Replace("-", "")
                objDTable = DA01.DataAccess.J01.FGJ02_GetOrderList(sBCNO)

                If objDTable.Rows.Count > 0 Then

                    With objDTable
                        For intCnt = 0 To objDTable.Rows.Count - 1

                            objOrdList = New DataAccess.J01.clsCancelTItem
                            objOrdList.BCNO = objDTable.Rows(intCnt).Item("bcno").ToString
                            objOrdList.REGNO = objDTable.Rows(intCnt).Item("regno").ToString
                            objOrdList.TCLSCD = objDTable.Rows(intCnt).Item("tclscd").ToString
                            objOrdList.SPCCD = objDTable.Rows(intCnt).Item("spccd").ToString
                            objOrdList.TCDGBN = objDTable.Rows(intCnt).Item("tcdgbn").ToString
                            objOrdList.IOGBN = objDTable.Rows(intCnt).Item("iogbn").ToString
                            objOrdList.FKOCS = objDTable.Rows(intCnt).Item("fkocs").ToString
                            objOrdList.OWNGBN = objDTable.Rows(intCnt).Item("owngbn").ToString
                            objOrdList.BCCLSCD = objDTable.Rows(intCnt).Item("sectcd").ToString
                            objOrdList.TORDCD = objDTable.Rows(intCnt).Item("tordcd").ToString
                            objOrdList.SPCFLG = "2"
                            objOrdList.CANCELRMK = Me.txtCmtCont.Text
                            aoOrdList.Add(objOrdList)
                            fnSelOrdList = True
                        Next
                    End With
                End If
            Else
                With spdRsltList
                    Dim arlTCd As New ArrayList

                    For ix As Integer = 1 To .MaxRows
                        .Row = ix
                        .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                        .Col = .GetColFromID("regno") : Dim sregno As String = .Text
                        .Col = .GetColFromID("ptclscd") : Dim sTclsCd As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
                        .Col = .GetColFromID("tcdgbn") : Dim sTCdGbn As String = .Text
                        .Col = .GetColFromID("iogbn") : Dim sIoGbn As String = .Text
                        .Col = .GetColFromID("fkocs") : Dim sFkOcs As String = .Text
                        .Col = .GetColFromID("owngbn") : Dim sOwnGbn As String = .Text
                        .Col = .GetColFromID("sectcd") : Dim sSectCd As String = .Text
                        .Col = .GetColFromID("tordcd") : Dim sTOrdCd As String = .Text

                        If sChk = "1" And arlTCd.Contains(sTclsCd) = False Then
                            objOrdList = New DataAccess.J01.clsCancelTItem
                            objOrdList.BCNO = sBcNo
                            objOrdList.REGNO = sregno
                            objOrdList.TCLSCD = sTclsCd
                            objOrdList.SPCCD = sSpcCd
                            objOrdList.TCDGBN = sTCdGbn
                            objOrdList.IOGBN = sIoGbn
                            objOrdList.FKOCS = sFkOcs
                            objOrdList.OWNGBN = sOwnGbn
                            objOrdList.BCCLSCD = sSectCd
                            objOrdList.TORDCD = sTOrdCd
                            objOrdList.SPCFLG = "2"

                            objOrdList.CANCELRMK = Me.txtCmtCont.Text

                            aoOrdList.Add(objOrdList)
                            fnSelOrdList = True

                            arlTCd.Add(sTclsCd)
                        End If
                    Next
                End With
            End If

            aoOrdList.TrimToSize()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    Private Sub sbRegSPcIns(ByVal rsBcNo As String, ByVal rsSpcCd As String, ByVal rsCancelGbn As String)

        Dim sFn As String = "sbBJHSpcIns(ByVal dt As DataTable)"

        Try
            DA_R.fnExe_Special_Reg(rsBcNo, rsCancelGbn, rsSpcCd, "1", USER_INFO.USRID, Me.txtCmtCont.Text, Me.txtCmtCd.Text)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Sub sbRegSPcIns(ByVal cancelDT As DataTable, Optional ByVal rsCancelGbn As String = "")

        Dim sFn As String = "sbRegSPcIns(ByVal dt As DataTable)"
        Dim sBcno As String = ""
        Dim sTclscd As String = ""
        Dim sSpccd As String = ""
        Dim rsgTF As Boolean = False


        Try
            If msJobGbn = "1" Then
                DA_R.fnExe_Special_Reg(cancelDT.Rows(0).Item("bcno").ToString, rsCancelGbn, cancelDT.Rows(0).Item("spccd").ToString, "1", USER_INFO.USRID, Me.txtCmtCont.Text, Me.txtCmtCd.Text)

                'MsgBox("등록되었습니다.", MsgBoxStyle.Information, Me.Text)
            Else
                With Me.spdRsltList
                    For i As Integer = 1 To .MaxRows
                        .Row = i
                        .Col = .GetColFromID("chk")

                        If .Text = "1" Then

                            .Col = .GetColFromID("bcno")
                            sBcno = .Text

                            .Col = .GetColFromID("tclscd")
                            sTclscd = .Text

                            .Col = .GetColFromID("spccd")
                            sSpccd = .Text
                            DA_R.fnExe_Special_Reg(sBcno, sTclscd, sSpccd, "2", USER_INFO.USRID, Me.txtCmtCont.Text, Me.txtCmtCd.Text)
                            rsgTF = True
                        End If

                    Next
                    If rsgTF Then MsgBox("등록되었습니다.", MsgBoxStyle.Information, Me.Text)
                End With

            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Sub sbDisplaySpcInfo(ByVal rsBcno As String)
        Me.txtSearch.Text = rsBcno
        txtSearch_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged

        If TabControl1.SelectedIndex = 1 Then
            If msJobGbn = "A" Or msJobGbn = "1" Then txtSearch_KeyDown(txtSearch, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
        End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Dim sFn As String = "btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRef.Click"
        Dim sStartDt As String
        Dim sEndDt As String
        Dim dt2 As New DataTable

        Try
            sStartDt = Format(dtpStart.Value, "yyyy-MM-dd")
            sEndDt = Format(dtpEnd.Value, "yyyy-MM-dd")

            If Me.msJobGbn = "1" Then
                Dim sWardCd As String = Ctrl.Get_Code(cboWard)
                Dim sSRCd As String = cboSR.Text.Replace("전체", "")

                dt2 = DA_R.getDelSPcListInfo(Ctrl.Get_Code(cboBccls), "", sStartDt, sEndDt, "1", sWardCd, sSRCd, cboCmt.Text)
                Ctrl.DisplayAfterSelect(Me.spdSpcList, dt2)

                For i As Integer = 1 To Me.spdSpcList.MaxRows

                    Me.spdSpcList.set_RowHeight(i, Me.spdSpcList.get_MaxTextRowHeight(i))

                Next
            Else
                dt2 = DA_R.fnGet_Abnormal_ActionInfo(Ctrl.Get_Code(cboBccls), "", sStartDt, sEndDt, "2", , , cboCmt.Text)
                Ctrl.DisplayAfterSelect(Me.spdSpcList, dt2)

                For i As Integer = 1 To Me.spdSpcList.MaxRows

                    Me.spdSpcList.set_RowHeight(i, Me.spdSpcList.get_MaxTextRowHeight(i))

                Next

                dt2 = DA_R.getDelPercent(Ctrl.Get_Code(cboBccls), "", sStartDt, sEndDt, "2")

                If dt2.Rows.Count > 0 Then
                    Me.Label7.Text = dt2.Rows(0).Item("acnt").ToString
                    Me.Label8.Text = dt2.Rows(0).Item("bcnt").ToString
                    Me.Label11.Text = dt2.Rows(0).Item("ccnt").ToString
                Else
                    Me.Label7.Text = "0"
                    Me.Label8.Text = "0"
                    Me.Label11.Text = "0"
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            Dim sBCNO As String
            sBCNO = Trim(txtSearch.Text)

            If sBCNO = "" Then
                'MsgBox("검체번호를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            sBCNO = sBCNO.Replace("-", "")

            If Len(sBCNO) = 11 Or Len(sBCNO) = 12 Then
                sBCNO = (New DA01.CommDBFN.DBSql).GetBCPrtToView(Mid(sBCNO, 1, 11))
            End If

            If sBCNO = "" Then
                txtSearch.SelectAll()
                Exit Sub
            End If

            sbDisplayPatInfo(sBCNO)

            txtSearch.SelectAll()
        End If
    End Sub

    Private Sub btnCReg_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCReg.ButtonClick
        Dim sFn As String = "btnCReg_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCReg.ButtonClick"
        Dim alOrdList As New ArrayList
        Try
            If Me.txtCmtCont.Text = "" Then
                MsgBox("조치사항을 입력하세요", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            If MsgBox("등록 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                Exit Sub
            End If

            If Me.txtCmt.Text = "" Then Me.txtCmtCd.Text = ""

            sbRegSPcIns(Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoA.CheckedChanged, rdoB.CheckedChanged

        Dim sFn As String = "RadioButton1_CheckedChanged"


        Try
            '전체
            If rdoA.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOA" Then
                cboBccls.SelectedIndex = -1 : Me.cboBccls.Enabled = False

            ElseIf rdoB.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOB" Then

                cboBccls.SelectedIndex = 0 : cboBccls.Enabled = True
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub btnPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.ButtonClick
        Dim obj As New rsltPrint

        obj.mspdWorklist = Me.spdSpcList

        obj.sfrmFlag = IIf(msJobGbn = "A", "2", msJobGbn).ToString

        If msJobGbn = "1" Then
            obj.sfrmGbn = "부적합 검체"
        Else
            obj.sfrmGbn = "특이결과"
        End If

        obj.msStartDt = Me.dtpStart.Value.ToShortDateString 'dtpB.Value.ToShortDateString + " " + nudB.Value.ToString
        obj.msEndDt = Me.dtpEnd.Value.ToShortDateString 'dtpE.Value.ToShortDateString + " " + nudE.Value.ToString

        obj.PrintPreview()

    End Sub

    Private Sub cboWard_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWard.SelectedIndexChanged
        Dim sFn As String = "Private Sub cboWard_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWard.SelectedIndexChanged"

        Try
            '병실 리스트
            Me.cboSR.Items.Clear()
            Me.spdSpcList.MaxRows = 0

            Me.cboSR.Items.Add("전체")

            Dim dt As DataTable = fnGet_RoomList(Ctrl.Get_Code(cboWard))

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        Me.cboSR.Items.Add(.Item("roomno").ToString)
                    End With
                Next
            End If

            Me.cboSR.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub btnCdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp.Click
        Dim sFn As String = "Sub btnCdHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp.Click"

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim strCds As String = txtCmtCd.Text
            If strCds.IndexOf("?") < 0 Then strCds += "%"

            If msJobGbn = "A" Then
                objHelp.FormText = "특이결과 내용"
                objHelp.TableNm = "LF410M"
                objHelp.Where = "CMTGBN = 'A'" + _
                                IIf(txtCmtCd.Text = "", "", " and (CMTCD like '" + strCds.Replace("?", "%") + "' or CMTCONT like '" + strCds.Replace("?", "%") + "')").ToString

            Else
                objHelp.FormText = "취소 사유 내용"
                objHelp.TableNm = "LF410M"
                objHelp.Where = "CMTGBN = '1'" + _
                                IIf(txtCmtCd.Text = "", "", " and (CMTCD like '" + strCds.Replace("?", "%") + "' or CMTCONT like '" + strCds.Replace("?", "%") + "')").ToString
            End If
            objHelp.GroupBy = ""
            objHelp.OrderBy = "CMTCONT"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("CMTCONT", "내용", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("CMTCD", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(btnCdHelp)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - btnCdHelp.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp.Height + 80)

            If aryList.Count > 0 Then
                txtCmtCont.Text += aryList.Item(0).ToString.Split("|"c)(0) + vbCrLf
                txtCmt.Text = aryList.Item(0).ToString.Split("|"c)(0)
                txtCmtCd.Text = aryList.Item(0).ToString.Split("|"c)(1)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub spdRsltList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdRsltList.ButtonClicked

        If msJobGbn = "1" Then
            If mbCheckMode Then Return

            '-- 부적합검체인 경우
            With spdRsltList
                .Row = e.row
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("fkocs") : Dim sFkOcs As String = .Text

                mbCheckMode = True
                For Ix As Integer = 1 To .MaxRows
                    If Ix <> e.row Then
                        .Row = Ix : .Col = .GetColFromID("fkocs")
                        If .Text = sFkOcs Then
                            .Col = .GetColFromID("chk") : .Text = sChk
                        End If
                    End If
                Next
                mbCheckMode = False
            End With

        Else
            '-- 특이결과인 경우
            With spdRsltList
                Dim strChk As String = ""

                .Row = e.row
                .Col = e.col : strChk = .Text

                .Row = e.row
                .Col = .GetColFromID("tnmd") : Dim strTnm As String = .Text
                .Col = .GetColFromID("viewrst") : Dim strViewRst As String = .Text

                Dim strTmp As String = strTnm + ": " + strViewRst + vbCrLf

                If strChk = "1" Then
                    If txtCmtCont.Text.IndexOf(strTmp) < 0 Then txtCmtCont.Text += strTmp
                Else
                    If txtCmtCont.Text.IndexOf(strTmp) < 0 Then txtCmtCont.Text = txtCmtCont.Text.Replace(strTmp, "")
                End If
            End With
        End If
    End Sub

    Private Sub txtCmtCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown
        Dim sFn As String = "Sub txtCmtCd_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCmtCd.LostFocus"

        If e.KeyCode <> Keys.Enter Then Return

        Try

            If txtCmtCd.Text = "" Then Exit Sub

            btnCdHelp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.ButtonClick, btnClose.ButtonClick
        Me.Close()
    End Sub 

    Private Sub mnuDel_sp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDel_sp.Click

        If msJobGbn <> "A" Then Return
        If lblDelInfo.Text = "" Then
            MsgBox("특이결과에서 삭제할 검체를 선택하세요.")
            Return
        End If
        If MsgBox("특이결과로 입력된 검체번호 : " + lblDelInfo.Text.Split("|"c)(1) + " 를 삭제하시겠습니까?", MsgBoxStyle.YesNo, "특이결과 삭제여부") = MsgBoxResult.No Then Return

        Dim strRegDt As String = lblDelInfo.Text.Split("|"c)(0).Replace("-", "").Replace(":", "").Replace(" ", "")
        Dim strBcNo As String = lblDelInfo.Text.Split("|"c)(1).Replace("-", "")

        If DA_R.fnExe_Special_Del(strRegDt, strBcNo, "2") Then

            btnQuery_Click(Nothing, Nothing)
            lblDelInfo.Text = ""
        Else
            MsgBox("삭제시 오류가 발생했습니다.!!")
        End If
    End Sub

    Private Sub spdSpcList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdSpcList.ClickEvent
        With spdSpcList
            .Row = .ActiveRow
            .Col = .GetColFromID("regdt") : lblDelInfo.Text = .Text + "|"
            .Col = .GetColFromID("bcno") : lblDelInfo.Text += .Text
        End With

    End Sub

    Private Sub spdRsltList_Advance(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_AdvanceEvent) Handles spdRsltList.Advance

    End Sub
End Class

Public Class rsltPrint
    Inherits PrinterBase
    Implements IPrintableObject

    Private miPos As Integer = 1

    Public msSectionName As String
    Public msStartDt As String
    Public msEndDt As String

    Public sSearchMTD As String

    Public iTCm As Single

    Public mspdWorklist As AxFPSpreadADO.AxfpSpread
    Public sfrmGbn As String
    Public sfrmFlag As String

    Public Sub Print() Implements IPrintableObject.Print
        Dim p As New ObjectPrinter
        p.mbLandscape = True
        p.Print(Me)
    End Sub

    Public Sub PrintPreview() Implements IPrintableObject.PrintPreview
        Dim p As New ObjectPrinter
        p.mbLandscape = True
        p.PrintPreview(Me)
    End Sub

    Public Sub RenderPage(ByVal sender As Object, ByVal ev As System.Drawing.Printing.PrintPageEventArgs) Implements IPrintableObject.RenderPage

        Dim PrintFontTitle As New Font("굴림체", 16, FontStyle.Underline Or FontStyle.Bold)

        ev.Graphics.PageUnit = GraphicsUnit.Point

        Dim PrintFont As New Font("굴림체", 7)
        Dim LineHeight As Single = PrintFont.GetHeight(ev.Graphics)
        Dim sPageWidth As Single = CType(((ev.MarginBounds.Width / 100) + 2) * msPoint, Single) - CmToPoint(3) 'CType((3 / ms1InCm) * msPoint, Single)

        Dim LeftMargin As Single = CmToPoint(1.5) ' CType((1.5 / ms1InCm) * msPoint, Single)

        Dim yPos As Single = CmToPoint(0.5) ' CType((1.5 / ms1InCm) * msPoint, Single)


        Dim iLineCnt As Integer = 0
        Do
            iLineCnt += 1
            If sPageWidth <= ev.Graphics().MeasureString((New String(Chr(Asc("-")), iLineCnt)), PrintFont).Width Then
                Exit Do
            End If
        Loop

        Dim linesPerPage As Single
        linesPerPage = ev.MarginBounds.Height / PrintFont.GetHeight(ev.Graphics)

        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center

        Dim layoutSize As New RectangleF(LeftMargin, yPos, sPageWidth, PrintFontTitle.GetHeight(ev.Graphics))

        Dim iPosNo As Single
        Dim iPosRefDt As Single
        Dim iPosBCNO As Single
        Dim iPosTnmd As Single = 0
        Dim iPosRegNo As Single
        Dim iPosPatNm As Single
        Dim iPosDeptnm As Single
        Dim iPosWardNM As Single
        Dim iPosSpcnmd As Single
        Dim iPosRegNM As Single
        Dim iPosCmt As Single

        If sfrmFlag = "1" Then
            iPosNo = CmToPoint(0.2)
            iPosRefDt = CmToPoint(1.1)
            iPosBCNO = CmToPoint(3.5)
            iPosTnmd = CmToPoint(6.0)
            iPosRegNo = CmToPoint(8.5)
            iPosPatNm = CmToPoint(10.0)
            iPosDeptnm = CmToPoint(11.5)
            iPosWardNM = CmToPoint(12.5)
            iPosRegNM = CmToPoint(14.0)
            iPosSpcnmd = CmToPoint(15.5)
            iPosCmt = CmToPoint(18.0)

        Else
            iPosNo = CmToPoint(0.2)
            iPosRefDt = CmToPoint(1.1)
            iPosBCNO = CmToPoint(3.5)
            iPosRegNo = CmToPoint(6.0)
            iPosPatNm = CmToPoint(7.5)
            iPosDeptnm = CmToPoint(9.0)
            iPosWardNM = CmToPoint(10.0)
            iPosRegNM = CmToPoint(11.5)
            iPosSpcnmd = CmToPoint(13.0)
            iPosCmt = CmToPoint(15.5)
        End If

        '-- 결재란인
        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat
        Dim rect As New Drawing.RectangleF
        Dim fnt_Body As New Font("굴림체", 7, FontStyle.Regular)

        Dim sgLeft As Single = LeftMargin + sPageWidth

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        ev.Graphics.DrawLine(Pens.Black, sgLeft - CmToPoint(6.5), yPos, sgLeft, yPos)
        ev.Graphics.DrawLine(Pens.Black, sgLeft - CmToPoint(6.0), yPos + CmToPoint(0.5), sgLeft, yPos + CmToPoint(0.5))
        ev.Graphics.DrawLine(Pens.Black, sgLeft - CmToPoint(6.5), yPos + CmToPoint(1.8), sgLeft, yPos + CmToPoint(1.8))

        ev.Graphics.DrawLine(Pens.Black, sgLeft - CmToPoint(6.5), yPos, sgLeft - CmToPoint(6.5), yPos + CmToPoint(1.8))
        ev.Graphics.DrawLine(Pens.Black, sgLeft - CmToPoint(6.0), yPos, sgLeft - CmToPoint(6.0), yPos + CmToPoint(1.8))
        ev.Graphics.DrawLine(Pens.Black, sgLeft - CmToPoint(4.0), yPos, sgLeft - CmToPoint(4.0), yPos + CmToPoint(1.8))
        ev.Graphics.DrawLine(Pens.Black, sgLeft - CmToPoint(2.0), yPos, sgLeft - CmToPoint(2.0), yPos + CmToPoint(1.8))
        ev.Graphics.DrawLine(Pens.Black, sgLeft - CmToPoint(0), yPos, sgLeft - CmToPoint(0), yPos + CmToPoint(1.8))

        rect = New Drawing.RectangleF(sgLeft - CmToPoint(6.5), yPos, CmToPoint(0.5), CmToPoint(1.9))
        ev.Graphics.DrawString("확        인", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgLeft - CmToPoint(6.0), yPos, CmToPoint(2.0), CmToPoint(0.5))
        ev.Graphics.DrawString("담당자", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgLeft - CmToPoint(4.0), yPos, CmToPoint(2.0), CmToPoint(0.5))
        ev.Graphics.DrawString("실  장", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgLeft - CmToPoint(2.0), yPos, CmToPoint(2.0), CmToPoint(0.5))
        ev.Graphics.DrawString("과  장", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        '-- 결재란인

        ev.Graphics.DrawString(sfrmGbn, PrintFontTitle, Brushes.Black, layoutSize, sf)

        yPos += LineHeight * 4

        ev.Graphics.DrawString("출력일시 :" & Format(Now, "yyyy-MM-dd HH:mm"), PrintFont, Brushes.Black, LeftMargin, yPos, New StringFormat)
        yPos += LineHeight
        If msSectionName <> "" Then
            ev.Graphics.DrawString("담당계 :" & msSectionName, PrintFont, Brushes.Black, LeftMargin + iPosNo, yPos, New StringFormat)
        End If

        ev.Graphics.DrawString("출력조건 :" & msStartDt & "일 ~ " & msEndDt & "일", PrintFont, Brushes.Black, LeftMargin, yPos, New StringFormat)

        yPos += LineHeight
        ev.Graphics.DrawString((New String(Chr(Asc("-")), iLineCnt)), PrintFont, Brushes.Black, LeftMargin, yPos, New StringFormat)

        Dim iPosPlus As Integer = 0
        Dim yPosLine As Single = 0
        yPos += LineHeight

        If sfrmFlag = "1" Then
            ev.Graphics.DrawString("No", PrintFont, Brushes.Black, LeftMargin + iPosNo, yPos, New StringFormat)
            ev.Graphics.DrawString("등록일시", PrintFont, Brushes.Black, LeftMargin + iPosRefDt, yPos, New StringFormat)
            ev.Graphics.DrawString("검체번호", PrintFont, Brushes.Black, LeftMargin + iPosBCNO, yPos, New StringFormat)
            ev.Graphics.DrawString("검사명", PrintFont, Brushes.Black, LeftMargin + iPosTnmd, yPos, New StringFormat)
            ev.Graphics.DrawString("등록번호", PrintFont, Brushes.Black, LeftMargin + iPosRegNo, yPos, New StringFormat)
            ev.Graphics.DrawString("환자명", PrintFont, Brushes.Black, LeftMargin + iPosPatNm, yPos, New StringFormat)
            ev.Graphics.DrawString("진료과", PrintFont, Brushes.Black, LeftMargin + iPosDeptnm, yPos, New StringFormat)
            ev.Graphics.DrawString("병동", PrintFont, Brushes.Black, LeftMargin + iPosWardNM, yPos, New StringFormat)
            ev.Graphics.DrawString("검체명", PrintFont, Brushes.Black, LeftMargin + iPosSpcnmd, yPos, New StringFormat)
            ev.Graphics.DrawString("통보자", PrintFont, Brushes.Black, LeftMargin + iPosRegNM, yPos, New StringFormat)
            ev.Graphics.DrawString("조치사항", PrintFont, Brushes.Black, LeftMargin + iPosCmt, yPos, New StringFormat)
        Else
            ev.Graphics.DrawString("No", PrintFont, Brushes.Black, LeftMargin + iPosNo, yPos, New StringFormat)
            ev.Graphics.DrawString("등록일시", PrintFont, Brushes.Black, LeftMargin + iPosRefDt, yPos, New StringFormat)
            ev.Graphics.DrawString("검체번호", PrintFont, Brushes.Black, LeftMargin + iPosBCNO, yPos, New StringFormat)
            ev.Graphics.DrawString("등록번호", PrintFont, Brushes.Black, LeftMargin + iPosRegNo, yPos, New StringFormat)
            ev.Graphics.DrawString("환자명", PrintFont, Brushes.Black, LeftMargin + iPosPatNm, yPos, New StringFormat)
            ev.Graphics.DrawString("진료과", PrintFont, Brushes.Black, LeftMargin + iPosDeptnm, yPos, New StringFormat)
            ev.Graphics.DrawString("병동", PrintFont, Brushes.Black, LeftMargin + iPosWardNM, yPos, New StringFormat)
            ev.Graphics.DrawString("검체명", PrintFont, Brushes.Black, LeftMargin + iPosSpcnmd, yPos, New StringFormat)
            ev.Graphics.DrawString("통보자", PrintFont, Brushes.Black, LeftMargin + iPosRegNM, yPos, New StringFormat)
            ev.Graphics.DrawString("조치사항", PrintFont, Brushes.Black, LeftMargin + iPosCmt, yPos, New StringFormat)

        End If
      

        yPos += LineHeight
        ev.Graphics.DrawString((New String(Chr(Asc("-")), iLineCnt)), PrintFont, Brushes.Black, LeftMargin, yPos, New StringFormat)

        'test
        'Exit Sub


        With mspdWorklist
            For iRow As Integer = miPos To .MaxRows

                yPos += LineHeight
                Dim yPosTmp As Single
                yPosTmp = yPos
                iPosPlus = 0


                If (ev.PageSettings.PaperSize.Width / 100) * msPoint - CmToPoint(1.5) < yPosTmp Then
                    ev.Graphics.DrawString((New String(Chr(Asc("-")), iLineCnt)), PrintFont, Brushes.Black, LeftMargin, yPos, New StringFormat)
                    ev.HasMorePages = True
                    miPos = iRow
                    Exit Sub
                End If
                .Row = iRow

                If sfrmFlag = "1" Then
                    ev.Graphics.DrawString(Str(iRow), PrintFont, Brushes.Black, LeftMargin + iPosNo, yPos, New StringFormat)
                    .Col = .GetColFromID("regdt")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosRefDt, yPos, New StringFormat)
                    .Col = .GetColFromID("bcno")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosBCNO, yPos, New StringFormat)
                    .Col = .GetColFromID("tnmd")
                    If .Text.Length > 12 Then
                        ev.Graphics.DrawString(.Text.Substring(0, 12), PrintFont, Brushes.Black, LeftMargin + iPosTnmd, yPos, New StringFormat)
                    Else
                        ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosTnmd, yPos, New StringFormat)
                    End If
                    .Col = .GetColFromID("regno")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosRegNo, yPos, New StringFormat)
                    .Col = .GetColFromID("patnm")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosPatNm, yPos, New StringFormat)
                    .Col = .GetColFromID("deptnm")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosDeptnm, yPos, New StringFormat)
                    .Col = .GetColFromID("wardroom")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosWardNM, yPos, New StringFormat)
                    .Col = .GetColFromID("spcnmd")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosSpcnmd, yPos, New StringFormat)
                    .Col = .GetColFromID("regnm")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosRegNM, yPos, New StringFormat)
                    .Col = .GetColFromID("cmtcont")
                Else
                    ev.Graphics.DrawString(Str(iRow), PrintFont, Brushes.Black, LeftMargin + iPosNo, yPos, New StringFormat)
                    .Col = .GetColFromID("regdt")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosRefDt, yPos, New StringFormat)
                    .Col = .GetColFromID("bcno")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosBCNO, yPos, New StringFormat)
                    .Col = .GetColFromID("regno")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosRegNo, yPos, New StringFormat)
                    .Col = .GetColFromID("patnm")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosPatNm, yPos, New StringFormat)
                    .Col = .GetColFromID("deptnm")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosDeptnm, yPos, New StringFormat)
                    .Col = .GetColFromID("wardroom")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosWardNM, yPos, New StringFormat)
                    .Col = .GetColFromID("spcnmd")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosSpcnmd, yPos, New StringFormat)
                    .Col = .GetColFromID("regnm")
                    ev.Graphics.DrawString(.Text, PrintFont, Brushes.Black, LeftMargin + iPosRegNM, yPos, New StringFormat)
                    .Col = .GetColFromID("cmtcont")
                End If

                Dim sCmt As String = .Text

                Dim sCmt2 As String = ""

                For xx As Integer = 0 To sCmt.Length - 1
                    Dim SMidCmt As String = sCmt.Substring(xx, 1)
                    sCmt2 = sCmt2 + SMidCmt
                    If xx = sCmt.Length - 1 Then
                        ev.Graphics.DrawString(sCmt2, PrintFont, Brushes.Black, LeftMargin + iPosCmt, yPos, New StringFormat)
                    End If
                    If SMidCmt = Convert.ToChar(10) Then
                        ev.Graphics.DrawString(sCmt2, PrintFont, Brushes.Black, LeftMargin + iPosCmt, yPos, New StringFormat)
                        sCmt2 = ""
                        yPos += LineHeight
                    End If
                Next

                yPos += CmToPoint(0.2)

            Next
        End With

        yPos += LineHeight
        ev.Graphics.DrawString((New String(Chr(Asc("-")), iLineCnt)), PrintFont, Brushes.Black, LeftMargin, yPos, New StringFormat)

        miPos = 1
    End Sub
End Class