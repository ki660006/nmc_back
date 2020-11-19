Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO

Imports LISAPP.APP_DB
Imports LISAPP.APP_BT

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Public Class FGB26

    Dim jeje As String = ""
    Dim sComcd As String = ""
    Dim sComcd2 As String = ""

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Me.dtpDateS.Value = DateAdd(DateInterval.Month, -1, dtpDateE.Value)
            Me.dtpDateE.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

            'dtpDateE.Value = New DA01.CommDbFn.ServerDateTime().GetDateTime
            'dtpDateS.Value = DateAdd(DateInterval.Month, -1, dtpDateE.Value)  ' 기본적으로 한달 전 조회

            With Me.SpdListIn
                .MaxRows = 0
                .SetText(0, 0, "")
            End With

            With Me.SpdListOut
                .MaxRows = 0
                .SetText(0, 0, "")
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub TabAbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabAbn.SelectedIndexChanged

        If TabAbn.SelectedIndex = 0 Then
            Me.lbIOgbn.Text = "입고일자"
        ElseIf TabAbn.SelectedIndex = 1 Then
            Me.lbIOgbn.Text = "출고일자"
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If lbIOgbn.Text = "입고일자" Then
            Search_bldin()
        ElseIf lbIOgbn.Text = "출고일자" Then
            Search_bldout()
        End If

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sTime As String = Format(Now, "yyyyMMdd")

        If lbIOgbn.Text = "입고일자" Then
            With SpdListIn
                .ReDraw = False

                .MaxRows += 1

                .InsertRows(1, 1)

                Dim sColHeaders As String = ""

                .Col = 1 : .Col2 = .MaxCols
                .Row = 0 : .Row2 = 0
                sColHeaders = .Clip

                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = 1
                .Clip = sColHeaders

                If .ExportToExcel("InAbnist_" + Now.ToShortDateString() + ".xls", "입고 리스트", "") Then
                    Process.Start("InAbnist_" + Now.ToShortDateString() + ".xls")
                End If

                .DeleteRows(1, 1)

                .MaxRows -= 1

                .ReDraw = True
            End With
        ElseIf lbIOgbn.Text = "출고일자" Then

            With SpdListOut
                .ReDraw = False

                .MaxRows += 1

                .InsertRows(1, 1)

                Dim sColHeaders As String = ""

                .Col = 1 : .Col2 = .MaxCols
                .Row = 0 : .Row2 = 0
                sColHeaders = .Clip

                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = 1
                .Clip = sColHeaders

                If .ExportToExcel("OutAbnist_" + Now.ToShortDateString() + ".xls", "출고/폐기 리스트", "") Then
                    Process.Start("OutAbnist_" + Now.ToShortDateString() + ".xls")
                End If

                .DeleteRows(1, 1)

                .MaxRows -= 1

                .ReDraw = True
            End With
        End If
    End Sub

    Private Sub FGB26_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB26_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Windows.Forms.Keys.F4 Then
            btnClear_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Windows.Forms.Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub Search_bldin()
        Dim sFn As String = "Sub Show_SearchIn"

        Try
            '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. 전역 변수 초기화 
            sComcd = ""
            sComcd2 = ""


            'Dim dteDateS As Date = CDate(dtpDateS.Text + " 00:00:00")

            Dim sDateS As String = dtpDateS.Text.Replace("-", "").Replace(":", "").Replace(" ", "")
            Dim sDateE As String = dtpDateE.Text.Replace("-", "").Replace(":", "").Replace(" ", "")

            '<<< lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. dt2의 CLSVAL 컬럼에 들어있는 값을 (ex. LBOOO/LBㅁㅁㅁ) 잘라 오기 위해.
            If cboBloodP.SelectedIndex <> 0 Then

                Dim dt2 As DataTable = BldInOut.Select_BloodP2(cboBloodP.SelectedItem.ToString)

                If dt2.Rows.Count > 0 Then
                    sComcd = dt2.Rows(0).Item("CLSVAL").ToString()
                    sComcd = "'" + sComcd.Replace("/", "','") + "'"
                    'sComcd2 = dt2.Rows(0).Item("CLSVAL").ToString()
                    'sComcd = Ctrl.Get_Comcd(sComcd)
                    'sComcd2 = Ctrl.Get_Comcd2(sComcd2)
                End If

            End If

           
            'Dim dt As DataTable = BldInOut.fBldIn_Search(dteDateS, Convert.ToDateTime(dtpDateE.Value.ToShortDateString() + " 23:59:59"))
            Dim dt As DataTable = BldInOut.fBldIn_Search(sDateS, sDateE, sComcd, sComcd2)
            '>

            If dt.Rows.Count > 0 Then

                With SpdListIn
                    .ReDraw = False
                    .MaxRows = dt.Rows.Count
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        .Row = ix + 1

                        .Col = .GetColFromID("bldno") : .Text = dt.Rows(ix).Item("bldno").ToString
                        .Col = .GetColFromID("dspccd2") : .Text = dt.Rows(ix).Item("dspccd2").ToString
                        .Col = .GetColFromID("comnmp") : .Text = dt.Rows(ix).Item("comnmp").ToString
                        .Col = .GetColFromID("ingbn") : .Text = dt.Rows(ix).Item("ingbn").ToString
                        .Col = .GetColFromID("indtymd") : .Text = dt.Rows(ix).Item("indtymd").ToString
                        .Col = .GetColFromID("indthm") : .Text = dt.Rows(ix).Item("indthm").ToString
                        .Col = .GetColFromID("dondt") : .Text = dt.Rows(ix).Item("dondt").ToString
                        .Col = .GetColFromID("id") : .Text = dt.Rows(ix).Item("id").ToString
                        .Col = .GetColFromID("abotype") : .Text = dt.Rows(ix).Item("abotype").ToString
                        .Col = .GetColFromID("innm") : .Text = dt.Rows(ix).Item("innm").ToString

                    Next
                    .ReDraw = True
                End With
            Else
                MsgBox("조회된 데이터가 없습니다. 조회일자를 확인하세요", MsgBoxStyle.Information, Me.Text)
                SpdListIn.MaxRows = 0
                Return
            End If

        Catch ex As Exception
            'Fn.log(mc_sFile + sFn, Err)
            'MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub Search_bldout()
        Dim sFn As String = "Sub Show_SearchOut"

        Try

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. 전역 변수 초기화 
            sComcd = ""
            sComcd2 = ""


            'Dim dteDateS As Date = CDate(dtpDateS.Text + " 00:00:00")

            Dim sDateS As String = dtpDateS.Text.Replace("-", "").Replace(":", "").Replace(" ", "")
            Dim sDateE As String = dtpDateE.Text.Replace("-", "").Replace(":", "").Replace(" ", "")

            '<<< lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. dt2의 CLSVAL 컬럼에 들어있는 값을 (ex. LBOOO/LBㅁㅁㅁ) 잘라 오기 위해.
            If cboBloodP.SelectedIndex <> 0 Then

                Dim dt2 As DataTable = BldInOut.Select_BloodP2(cboBloodP.SelectedItem.ToString)

                If dt2.Rows.Count > 0 Then
                    sComcd = dt2.Rows(0).Item("CLSVAL").ToString()

                    sComcd = "'" + sComcd.Replace("/", "','") + "'"
                    'sComcd2 = dt2.Rows(0).Item("CLSVAL").ToString()
                    'sComcd = Ctrl.Get_Comcd(sComcd)
                    'sComcd2 = Ctrl.Get_Comcd2(sComcd2)
                End If

            End If

            'Dim dt As DataTable = New DA01.AccessB01.CGDA_B01().fnGet_OutAbn_LIst(dteDateS, Convert.ToDateTime(dtpDateE.Value.ToShortDateString() + " 23:59:59"))
            Dim dt As DataTable = BldInOut.fBldOut_Search(sDateS, sDateE, sComcd, sComcd2)
            '>

            If dt.Rows.Count > 0 Then

                With SpdListOut
                    .ReDraw = False
                    .MaxRows = dt.Rows.Count
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        .Row = ix + 1

                        .Col = .GetColFromID("bldno") : .Text = dt.Rows(ix).Item("bldno").ToString
                        .Col = .GetColFromID("dspccd2") : .Text = dt.Rows(ix).Item("dspccd2").ToString
                        .Col = .GetColFromID("comnmp") : .Text = dt.Rows(ix).Item("comnmp").ToString
                        .Col = .GetColFromID("bldflg") : .Text = dt.Rows(ix).Item("bldflg").ToString
                        .Col = .GetColFromID("outdtymd") : .Text = dt.Rows(ix).Item("outdtymd").ToString
                        .Col = .GetColFromID("outdthm") : .Text = dt.Rows(ix).Item("outdthm").ToString
                        .Col = .GetColFromID("id") : .Text = dt.Rows(ix).Item("id").ToString
                        .Col = .GetColFromID("abotype") : .Text = dt.Rows(ix).Item("abotype").ToString
                        .Col = .GetColFromID("outnm") : .Text = dt.Rows(ix).Item("outnm").ToString

                        .Col = .GetColFromID("sex") : .Text = dt.Rows(ix).Item("sex1").ToString
                        .Col = .GetColFromID("birth") : .Text = dt.Rows(ix).Item("birth").ToString
                        .Col = .GetColFromID("abo") : .Text = dt.Rows(ix).Item("abo").ToString
                        .Col = .GetColFromID("info") : .Text = dt.Rows(ix).Item("info").ToString
                        .Col = .GetColFromID("deptno") : .Text = dt.Rows(ix).Item("deptno").ToString


                    Next
                    .ReDraw = True
                End With
            Else
                MsgBox("조회된 데이터가 없습니다. 조회일자를 확인하세요", MsgBoxStyle.Information, Me.Text)
                SpdListOut.MaxRows = 0
                Return
            End If

        Catch ex As Exception
            'Fn.log(mc_sFile + sFn, Err)
            'MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub FGB26_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.dtpDateS.Value = DateAdd(DateInterval.Month, -1, dtpDateE.Value)
        Me.dtpDateE.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        Me.WindowState = FormWindowState.Maximized

        Me.SpdListIn.MaxRows = 0
        Me.SpdListOut.MaxRows = 0

        '<<< 20151112 lhj
        sbDisplay_Bloodp()



    End Sub

    '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 추가. 기존 항목 및 확대 항목을 콤보박스에 채우기
    Private Sub sbDisplay_BloodP(Optional ByVal rpFormLoad As Boolean = True)

        Dim sFn As String = "sbDisplay_BloodP"
        Dim A As String = ""

        Try
            Dim dt As DataTable = BldInOut.Select_BloodP()

            If rpFormLoad Then
                Me.cboBloodP.Items.Add("[     ] 전체")

                For ix As Integer = 0 To dt.Rows.Count - 1
                    Me.cboBloodP.Items.Add(dt.Rows(ix).Item("CLSDESC").ToString)
                Next

                If Me.cboBloodP.Items.Count > 0 Then Me.cboBloodP.SelectedIndex = 0

            End If

        Catch ex As Exception

        End Try

    End Sub

End Class