Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN


Public Class POPUP_TAT_OVER
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : POPUP_TAT_OVER.vb, Class : POPUP_TAT_OVER" & vbTab

    Private msDataTable As New DataTable()

    Public Sub New(ByVal rsDataTable As DataTable)

        ' 디자이너에서 이 호출이 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하세요.
        msDataTable = rsDataTable

    End Sub

    Public Sub sbInit()
        Me.spdList.MaxRows = 0
    End Sub

    Public Sub sbDisplay_Data()
        Try
            With spdList

                For Each spcinfo As DataRow In msDataTable.Rows
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("bcno") : .Text = spcinfo.Item("bcno").ToString().Trim()
                    .Col = .GetColFromID("regno") : .Text = spcinfo.Item("regno").ToString().Trim()
                    .Col = .GetColFromID("patnm") : .Text = spcinfo.Item("patnm").ToString().Trim()
                    .Col = .GetColFromID("testcd") : .Text = spcinfo.Item("testcd").ToString().Trim()
                    .Col = .GetColFromID("spccd") : .Text = spcinfo.Item("spccd").ToString().Trim()
                    .Col = .GetColFromID("tnmd") : .Text = spcinfo.Item("tnmd").ToString().Trim()

                    .Col = .GetColFromID("rstflg")
                    Select Case spcinfo.Item("rstflg").ToString().Trim
                        Case "3"    ' 최종결과 표시
                            .ForeColor = Drawing.Color.DarkGreen
                            .Text = "◆"

                        Case "2"    ' 중간보고 표시
                            .Text = "○"

                        Case "1"
                            .Text = "△"
                        Case Else
                            .Text = ""

                    End Select

                    .Col = .GetColFromID("tkdt") : .Text = spcinfo.Item("tkdt").ToString().Trim()
                    .Col = .GetColFromID("tatoverdt") : .Text = spcinfo.Item("tatoverdt").ToString().Trim()
                    .Col = .GetColFromID("remainingtime") : .Text = spcinfo.Item("remainingtime").ToString().Trim() 'TAT 임박 남은 시간(mm)
                    If Convert.ToInt16(.Text) < 0 Then
                        .BackColor = Color.Red
                        .ForeColor = Color.White
                    End If

                    .Col = .GetColFromID("regdt") : .Text = spcinfo.Item("regdt").ToString().Trim()
                    .Col = .GetColFromID("mwdt") : .Text = spcinfo.Item("mwdt").ToString().Trim()

                Next

            End With
        Catch ex As Exception
            Throw (New Exception(ex.Message))
        End Try
    End Sub

    Private Sub POPUP_TAT_OVER_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        sbInit()
        sbDisplay_Data()
    End Sub
End Class