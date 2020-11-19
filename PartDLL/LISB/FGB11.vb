' 혈액자체폐기

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB11

    Private Sub FGB11_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.txtSBldno.Focus()
    End Sub

    Private Sub FGB11_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F7
                btnExecute_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB11_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        ' 화면 오픈시 초기화
        Me.spdList.MaxRows = 0

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdList)
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdList.MaxRows = 0
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        If Me.spdList.MaxRows < 1 Then Return

        Try
            Dim objHelp As New FGB10_S01
            Dim iChkCnt As Integer = 0
            Dim bContinue As Boolean = False
            Dim bOk As Boolean = False
            Dim sChk As String
            Dim alRtnValue As New ArrayList
            Dim alArg As New ArrayList
            Dim sMs As String = ""

            If Me.rdoAbn.Checked = True Then
                sMs = " '폐기' "
            ElseIf Me.rdoChg.Checked = True Then
                sMs = " '교환' "
            End If

            With Me.spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1"c Then
                        iChkCnt += 1
                    End If
                Next

                If iChkCnt < 1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "선택한 항목이 없습니다.")
                    Return
                End If

                bContinue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "선택된 혈액을" + sMs + "처리 하시겠습니까?")

                If bContinue = False Then Return

                Dim sRtnReqId As String = ""
                Dim sRtnReqNm As String = ""
                Dim sRtnCode As String = ""
                Dim sRtnCmt As String = ""

                If Me.rdoAbn.Checked = True Then
                    alRtnValue = objHelp.fn_DisplayPop(Me, 1)       ' 자체페기
                Else
                    alRtnValue = objHelp.fn_DisplayPop(Me, 1, "2"c) ' 혈액교환
                End If

                If alRtnValue.Count < 1 Then
                    Return
                Else
                    sRtnReqId = alRtnValue(0).ToString
                    sRtnReqNm = alRtnValue(1).ToString
                    sRtnCode = alRtnValue(2).ToString
                    sRtnCmt = alRtnValue(3).ToString
                End If

                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1"c Then
                        Dim stuRtn As New STU_TnsJubsu

                        stuRtn.TNSJUBSUNO = ""
                        .Col = .GetColFromID("comcd") : stuRtn.COMCD = .Text
                        .Col = .GetColFromID("bldno") : stuRtn.BLDNO = .Text

                        stuRtn.COMCD_OUT = stuRtn.COMCD
                        stuRtn.COMORDCD = ""
                        stuRtn.IOGBN = ""
                        stuRtn.OWNGBN = ""
                        stuRtn.FKOCS = ""
                        stuRtn.REGNO = ""
                        stuRtn.FILTER = ""
                        stuRtn.RTNREQID = sRtnReqId
                        stuRtn.RTNREQNM = sRtnReqNm
                        stuRtn.RTNRSNCD = sRtnCode
                        stuRtn.RTNRSNCMT = sRtnCmt
                        stuRtn.ORDDATE = ""
                        stuRtn.SPCCD = ""
                        stuRtn.TEMP01 = ""

                        alArg.Add(stuRtn)
                    End If
                Next

                If Me.rdoAbn.Checked = True Then
                    bOk = (New Rtn).fnExe_SelfAbn(alArg, "A"c)
                Else
                    bOk = (New Rtn).fnExe_SelfAbn(alArg, "C"c)
                End If

            End With

            If bOk = True Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 되었습니다.")
                Me.spdList.MaxRows = 0
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, sMs + " 작업중 오류가 발생 하였습니다.")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub rdoAbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAbn.Click
        btnExecute.Text = "폐  기(F7)"
        txtSBldno.Focus()
    End Sub

    Private Sub rdoChg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoChg.Click
        btnExecute.Text = "교  환(F7)"
        Me.txtSBldno.Focus()
    End Sub

    Private Sub chkAbo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAbo.CheckedChanged
        Dim sFn As String = "Private Sub chkAbo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAbo.CheckedChanged"

        If Me.chkAbo.Checked = True Then
            Try
                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

                Dim dt As DataTable = CGDA_BT.fn_OverAvailyBloodList()

                sb_DisplayDataList(dt)

                sb_SetStBarSearchCnt(dt.Rows.Count)

            Catch ex As Exception
                Me.spdList.ReDraw = True
                fn_PopMsg(Me, "E"c, ex.Message)
            Finally
                Me.spdList.ReDraw = True
                Cursor.Current = System.Windows.Forms.Cursors.Default
            End Try

        Else
            Me.spdList.MaxRows = 0
        End If

    End Sub

    Private Sub sb_DisplayDataList(ByVal rDt As DataTable)

        Try
            With Me.spdList
                .MaxRows = 0
                If rDt.Rows.Count < 1 Then Return

                .ReDraw = False

                For i As Integer = 0 To rDt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("chk") : .Text = "1"c
                    .Col = .GetColFromID("vbldno") : .Text = rDt.Rows(i).Item("vbldno").ToString
                    .Col = .GetColFromID("comnmd") : .Text = rDt.Rows(i).Item("comnmd").ToString
                    .Col = .GetColFromID("state") : .Text = rDt.Rows(i).Item("state").ToString
                    .Col = .GetColFromID("aborh") : .Text = rDt.Rows(i).Item("aborh").ToString

                    Dim sAbo As String = rDt.Rows(i).Item("aborh").ToString.Replace("+"c, "").Replace("-"c, "")
                    .ForeColor = fnGet_BloodColor(sAbo)

                    .Col = .GetColFromID("indt") : .Text = rDt.Rows(i).Item("indt").ToString
                    .Col = .GetColFromID("dondt") : .Text = rDt.Rows(i).Item("dondt").ToString
                    .Col = .GetColFromID("availdt") : .Text = rDt.Rows(i).Item("availdt").ToString
                    .Col = .GetColFromID("bldno") : .Text = rDt.Rows(i).Item("bldno").ToString
                    .Col = .GetColFromID("comcd") : .Text = rDt.Rows(i).Item("comcd").ToString
                Next

            End With
        Catch ex As Exception
            Me.spdList.ReDraw = True
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdList.ReDraw = True
        End Try

    End Sub

    Private Sub txtSBldno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSBldno.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim dt As DataTable
            Dim sBldno As String = ""
            Dim iFRow As Integer = 0

            sBldno = Me.txtSBldno.Text.Replace("-"c, "")

            If sBldno.Length() < 10 Then
                fn_PopMsg(Me, "I"c, "잘못된 혈액 번호 입니다.")
                Me.txtSBldno.Focus()
                Me.txtSBldno.SelectAll()
                Return
            ElseIf sBldno.Length() < 1 Then
                Return
            Else
                iFRow = Fn.SpdColSearch(spdList, sBldno, spdList.GetColFromID("bldno"))

                If iFRow > 0 Then
                    fn_PopMsg(Me, "I"c, "중복된 혈액입니다.")
                    Return
                End If

                dt = CGDA_BT.fn_AbnBloodSearch(sBldno)

                sb_DisplayDataAdd(dt)
            End If

            Me.txtSBldno.Text = ""
            Me.txtSBldno.Focus()
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sb_DisplayDataAdd(ByVal r_dt As DataTable)
        Try
            With Me.spdList
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("chk") : .Text = r_dt.Rows(ix).Item("chkgbn").ToString.Trim
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(ix).Item("vbldno").ToString.Trim
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("comnmd").ToString.Trim
                    .Col = .GetColFromID("state") : .Text = r_dt.Rows(ix).Item("state").ToString.Trim
                    .Col = .GetColFromID("aborh") : .Text = r_dt.Rows(ix).Item("aborh").ToString.Trim

                    Dim sAbo As String = r_dt.Rows(ix).Item("aborh").ToString.Replace("+"c, "").Replace("-"c, "")
                    .ForeColor = fnGet_BloodColor(sAbo)

                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(ix).Item("indt").ToString.Trim
                    .Col = .GetColFromID("dondt") : .Text = r_dt.Rows(ix).Item("dondt").ToString.Trim
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(ix).Item("availdt").ToString.Trim
                    .Col = .GetColFromID("bldno") : .Text = r_dt.Rows(ix).Item("bldno").ToString.Trim
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(ix).Item("comcd").ToString.Trim
                Next

            End With
        Catch ex As Exception
            Me.spdList.ReDraw = True
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdList.ReDraw = True

        End Try
    End Sub

    Private Sub txtSBldno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSBldno.Click
        Me.txtSBldno.SelectAll()
    End Sub
End Class