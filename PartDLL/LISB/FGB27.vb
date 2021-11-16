Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO

Imports LISAPP.APP_DB
Imports LISAPP.APP_BT

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN


Public Class FGB27
    Dim m_stdt As String = ""
    Dim m_endt As String = ""
    Private Sub FGB27_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        sbDisp_Init()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        With spdTnsTranList
            .MaxRows = 0
        End With
    End Sub

    Public Sub sbDisp_Init()
        Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

        Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
        Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")
    End Sub

    Private Sub btnQuery_Click(sender As Object, e As EventArgs) Handles btnQuery.Click

        m_stdt = dtpDateS.Text.Replace("-", "").Replace(" ", "")
        m_endt = dtpDateE.Text.Replace("-", "").Replace(" ", "")

        sbDisplay_Data()

    End Sub

    Private Sub sbDisplay_Data()
        Try
            Dim dt As DataTable = CGDA_BT.fnGet_trans_mgt(m_stdt, m_endt)
            If dt.Rows.Count < 1 Then Return

            With Me.spdTnsTranList
                .MaxRows = 0

                If dt.Rows.Count < 1 Then Return

                .ReDraw = False
                .MaxRows = dt.Rows.Count

                For i As Integer = 1 To dt.Rows.Count
                    For j As Integer = 1 To dt.Columns.Count
                        Dim iCol As Integer = .GetColFromID(dt.Columns(j - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            If i >= 2 Then
                                .Row = i - 1
                                If .Text = dt.Rows(i - 1).Item(j - 1).ToString() Then
                                    .Row = i
                                    .Text = dt.Rows(i - 1).Item(j - 1).ToString()
                                    If sbDisp_column(dt.Columns(j - 1).ColumnName.ToLower()) = False Then '중복이면 안보여야하는 것  
                                        .ForeColor = Color.White
                                    End If
                                Else
                                    .Row = i
                                    .Text = dt.Rows(i - 1).Item(j - 1).ToString()
                                End If
                            Else
                                .Row = i
                                .Text = dt.Rows(i - 1).Item(j - 1).ToString()
                            End If
                        End If
                    Next
                Next
            End With
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Function sbDisp_column(ByVal rsColNm As String) As Boolean
        Try
            If rsColNm = "state" Or rsColNm = "outdt" Or rsColNm = "rtndt" Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

End Class