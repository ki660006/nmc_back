Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login

Imports LISAPP.APP_DB

Public Class FGO90
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGO04.vb, Class : O01" & vbTab
    Private moDB As New LISAPP.LISAPP_O_CUST_ORD

    ' 폼 초기설정
    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"
        Dim CommFN As New Fn
        Dim ServerDT As New ServerDateTime

        Try
            Me.Tag = "Load"
            ' 서버날짜로 설정
            dtpDateS.Value = CDate(ServerDT.GetDate("-").Substring(0, 8) + "01")
            dtpDateE.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, dtpDateS.Value))

            spdList.MaxRows = 0
            spdPatList.MaxRows = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub FGO04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        Me.WindowState = FormWindowState.Maximized
        sbFormInitialize()

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        spdList.MaxRows = 0
        spdPatList.MaxRows = 0

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Search()

        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Search()

        Try

            Dim dateS As String = Me.dtpDateS.Text.Replace("-", "").Replace(" ", "")
            Dim dateE As String = Me.dtpDateE.Text.Replace("-", "").Replace(" ", "")

            Dim Obj = New LISAPP.APP_R.AxRstFn
            Dim dt As DataTable = Obj.fnGet_FGO90(dateS, dateE)

            If dt.Rows.Count <= 0 Then Return

            With spdList
                For ix As Integer = 0 To dt.Rows.Count - 1
                    .MaxRows = ix + 1
                    .Row = ix + 1

                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString
                    .Col = .GetColFromID("E") : .Text = dt.Rows(ix).Item("E").ToString
                    .Col = .GetColFromID("R") : .Text = dt.Rows(ix).Item("R").ToString
                    .Col = .GetColFromID("prtno") : .Text = dt.Rows(ix).Item("prtno").ToString

                Next
            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub CButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CButton1.Click
        Try


            With spdList
                For ix As Integer = 1 To .MaxRows

                    .Row = ix
                    .Col = .GetColFromID("chk") : Dim chk As String = .Text

                    If chk = "1" Then
                        .Col = .GetColFromID("prtno") : Dim prtno As String = .Text
                        .Col = .GetColFromID("E") : Dim Egene As String = .Text
                        .Col = .GetColFromID("R") : Dim RdRP As String = .Text

                        If Egene <> "" Then
                            With (New LISAPP.APP_R.AxRstFn)
                                .RegNcov(prtno, "E", Egene, USER_INFO.USRID, "")
                            End With

                        End If

                        If RdRP <> "" Then
                            With (New LISAPP.APP_R.AxRstFn)
                                .RegNcov(prtno, "R", RdRP, USER_INFO.USRID, "")
                            End With
                        End If

                    End If


                Next

            End With

            btnQuery_Click(Nothing, Nothing)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class