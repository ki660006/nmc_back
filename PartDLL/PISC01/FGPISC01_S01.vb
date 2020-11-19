Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.SVar.Login
Imports DA01.DataAccess.C01

Imports System.Windows.Forms

Public Class FGPISC01_S01
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGC01_S01.vb, Class : FGC01_S01" & vbTab

    Private Sub sbDisplay_List(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_List"

        Try
            Dim dt As DataTable = fnGet_CollectInfo(rsBcNo, Me.chkAutoTk.Checked)

            With spdList
                If dt.Rows.Count < 1 Then Return

                .ReDraw = False
                .MaxRows += 1

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = .MaxRows
                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patinfo").ToString.Split("|"c)(0)
                    .Col = .GetColFromID("testnms") : .Text = dt.Rows(ix).Item("testnms").ToString
                Next
                .ReDraw = True

            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try

    End Sub
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtBcNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcNo.Click
        Me.txtBcNo.Focus()
        Me.txtBcNo.SelectAll()
    End Sub

    Private Sub txtBcNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcNo.GotFocus
        Me.txtBcNo.SelectAll()
    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown
        Dim sFn As String = "txtBcNo_KeyDown"

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtBcNo.Text = "" Then Return

        Try
            Dim sBcNo As String = Me.txtBcNo.Text.Replace("-", "")

            If Len(sBcNo) = 11 Or Len(sBcNo) = 12 Then
                sBcNo = (New DA01.CommDBFN.DBSql).GetBCPrtToView(Mid(sBcNo, 1, 11))
            End If

            With (New DA01.DataAccess.DB_Collect)
                Dim blnRet As Boolean = .ExecuteDo_CollDt(sBcNo, USER_INFO.USRID, Me.chkAutoTk.Checked)

                If blnRet = False Then
                    MsgBox("채혈일시 등록에 실패했습니다.!!", MsgBoxStyle.Critical, "채혈일시 등록")
                Else
                    sbDisplay_List(sBcNo)
                End If
            End With

            txtBcNo_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
        End Try

    End Sub

    Private Sub FGC01_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub FGC01_S01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        Me.spdList.MaxRows = 0
        Me.txtBcNo.Focus()
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rbTakeYn As Boolean)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        Me.chkAutoTk.Visible = rbTakeYn
        Me.chkAutoTk.Checked = rbTakeYn

    End Sub

End Class