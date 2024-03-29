﻿' 혈액이력조회

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB13

    Private Sub FGB13_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB13_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB13_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        ' 화면 오픈시 초기화
        Me.spdComList.MaxRows = 0
        Me.spdBHisList.MaxRows = 0

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdComList)
        DS_SpreadDesige.sbInti(spdBHisList)
    End Sub

    Private Sub FGB13_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.txtSBldno.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdComList.MaxRows = 0
        Me.spdBHisList.MaxRows = 0

        Me.lblInid.Text = ""
        Me.lblState.Text = ""
        Me.lblDondt.Text = ""
        Me.lblAbo.Text = ""

        Me.txtSBldno.Text = ""
        Me.txtSBldno.Focus()
        Me.AxTnsPatinfo1.sb_ClearLbl()
    End Sub

    Private Sub txtSBldno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSBldno.Click
        Me.txtSBldno.SelectAll()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        Dim sBldno As String = ""

        Me.spdComList.MaxRows = 0
        Me.spdBHisList.MaxRows = 0
        Me.AxTnsPatinfo1.sb_ClearLbl()

        sBldno = Me.txtSBldno.Text

        If sBldno.Length() = 10 Then

        ElseIf sBldno.Length() = 12 Then
            sBldno = sBldno.Replace("-"c, "")
        Else
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "혈액번호를 정확히 입력하시기 바랍니다.")
            Me.txtSBldno.Focus()
            Me.txtSBldno.SelectAll()
            Return
        End If

        Try
            ' 조회
            dt = CGDA_BT.fn_GetBldInfo(sBldno)

            sb_DisplayData(dt)

            dt = CGDA_BT.fn_GetComcdList(sBldno)

            sb_DisplayComcdList(dt)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub txtSBldno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSBldno.KeyDown
        Dim sBldno As String = ""

        If e.KeyCode = Keys.Enter Then
            sBldno = Me.txtSBldno.Text.Replace("-", "")

            If sBldno.Length() = 10 Then

            ElseIf sBldno.Length() = 12 Then
                sBldno = sBldno.Replace("-"c, "")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "혈액번호를 정확히 입력하시기 바랍니다.")
                Me.txtSBldno.Focus()
                Me.txtSBldno.SelectAll()
                Return
            End If

            btnSearch_Click(Nothing, Nothing)

        End If
    End Sub

    Private Sub sb_DisplayData(ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then
            Me.lblInid.Text = ""
            Me.lblState.Text = ""
            Me.lblDondt.Text = ""
            Me.lblAbo.Text = ""

            Me.txtSBldno.Text = ""
            Me.txtSBldno.Focus()

            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "혈액이력을 조회 할 수 없습니다.")
            Return
        Else
            Dim alRtnItem As New ArrayList

            alRtnItem = fn_GetSelectItem(r_dt, 11)

            ' 0: 혈액번호 1: 혈액번호(포멧) 2: 용량 3: 입고자 4: 상태 5: 입고일자 
            ' 6: abo 7: rh 8: aborh 9: 등록번호 10: 처방일자

            If alRtnItem(2).ToString = "0"c Then
                Me.rdo400.Checked = True
            ElseIf alRtnItem(2).ToString = "1"c Then
                Me.rdo320.Checked = True
            Else
                Me.rdoNone.Checked = True
            End If

            Me.lblInid.Text = alRtnItem(3).ToString
            Me.lblState.Text = alRtnItem(4).ToString
            Me.lblDondt.Text = alRtnItem(5).ToString
            Me.lblAbo.Text = alRtnItem(8).ToString

            Me.lblAbo.ForeColor = fnGet_BloodColor(alRtnItem(6).ToString)
            Me.txtSBldno.Text = alRtnItem(1).ToString
            Me.txtSBldno.Focus()
        End If

    End Sub

    ' 성분제제리스트
    Private Sub sb_DisplayComcdList(ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then Return

        Dim sBldno As String = ""
        Dim sComcd As String = ""

        Try
            With Me.spdComList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False
                For i As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(i).Item("vbldno").ToString
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(i).Item("comcd").ToString
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(i).Item("comnmd").ToString
                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(i).Item("indt").ToString
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(i).Item("availdt").ToString
                Next
            End With

            With spdComList
                .Row = 1
                .Col = .GetColFromID("vbldno") : sBldno = .Text.Replace("-"c, "")
                .Col = .GetColFromID("comcd") : sComcd = .Text
            End With

            Dim dt As DataTable = CGDA_BT.fn_GetBldHisList(sBldno, sComcd)

            sb_DisplayBldHisList(dt)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdComList.ReDraw = True
        End Try
    End Sub

    Private Sub sb_DisplayBldHisList(ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then Return

        Try
            With Me.spdBHisList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("state") : .Text = r_dt.Rows(ix).Item("state").ToString
                    .Col = .GetColFromID("workdt") : .Text = r_dt.Rows(ix).Item("workdt").ToString
                    .Col = .GetColFromID("worknm") : .Text = r_dt.Rows(ix).Item("worknm").ToString
                    .Col = .GetColFromID("recid") : .Text = r_dt.Rows(ix).Item("recid").ToString
                    .Col = .GetColFromID("recnm") : .Text = r_dt.Rows(ix).Item("recnm").ToString
                    .Col = .GetColFromID("tnsgbn") : .Text = r_dt.Rows(ix).Item("tnsgbn").ToString
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString
                    .Col = .GetColFromID("vtnsjubsuno") : .Text = r_dt.Rows(ix).Item("vtnsjubsuno").ToString
                    .Col = .GetColFromID("abo") : .Text = r_dt.Rows(ix).Item("abo").ToString
                    .Col = .GetColFromID("rh") : .Text = r_dt.Rows(ix).Item("rh").ToString
                    .Col = .GetColFromID("rtnrsncmt") : .Text = r_dt.Rows(ix).Item("rtnrsncmt").ToString
                Next
            End With

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdComList.ReDraw = True
        End Try
    End Sub

    Private Sub spdComList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdComList.ClickEvent
        If Me.spdComList.MaxRows < 1 Then Return

        Dim sbldno As String = ""
        Dim sComcd As String = ""


        With Me.spdComList
            .Row = e.row '<20141020 수정
            .Col = .GetColFromID("vbldno") : sbldno = .Text.Replace("-"c, "")
            .Col = .GetColFromID("comcd") : sComcd = .Text
        End With

        Dim dt As DataTable = CGDA_BT.fn_GetBldHisList(sbldno, sComcd)

        sb_DisplayBldHisList(dt)
    End Sub
End Class