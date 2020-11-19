Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst



Public Class FGS20_S05
    Private mfrmCur As Windows.Forms.Form
    Private msRetVal As String = ""
    Private Sub FGS20_S05_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
        sbSearchBaccdRefcd()
        Me.spdAnti.MaxRows = 0
    End Sub

    ' 균별 병원체 코드 
    Public Sub sbSearchBaccdRefcd()
        Try
            Dim objDAF As LISAPP.APP_F_BACGEN_ANTI

            objDAF = New LISAPP.APP_F_BACGEN_ANTI

            Dim dt As DataTable


            dt = objDAF.GetBacgenAntiInfo(0, "")

            With Me.spdBacgen

                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("bacgen") : .Text = dt.Rows(ix).Item("bacgencd").ToString
                    .Col = .GetColFromID("bacgennm") : .Text = dt.Rows(ix).Item("bacgennmd").ToString
                    .Col = .GetColFromID("gbn") : .Text = dt.Rows(ix).Item("testmtd").ToString

                Next

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub spdBacgen_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdBacgen.ClickEvent

        Try


            With Me.spdBacgen
                If .MaxRows < 1 Then
                    Return
                End If

                Dim sBacgenCd As String = ""
                Dim sTestMtd As String = ""

                .Row = e.row
                .Col = .GetColFromID("bacgen") : sBacgenCd = .Text
                .Col = .GetColFromID("gbn") : sTestMtd = .Text

                Dim objDAF As LISAPP.APP_F_BACGEN_ANTI
                objDAF = New LISAPP.APP_F_BACGEN_ANTI

                Dim dt As DataTable
                dt = objDAF.GetBacgenAntiInfo(sBacgenCd, sTestMtd)

                sbDisplayAnti(dt)


            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Function fnDisplay() As String

        Me.ShowDialog()

        Return msRetVal

    End Function

    Private Sub sbDisplayAnti(ByVal rsDt As DataTable)
        Try
            With Me.spdAnti
                .MaxRows = 0

                For ix As Integer = 0 To rsDt.Rows.Count - 1
                    .Row = ix + 1
                    If rsDt.Rows(ix).Item("chk").ToString = "1" Then
                        .MaxRows += 1
                        .Col = .GetColFromID("anticd") : .Text = rsDt.Rows(ix).Item("anticd").ToString
                        .Col = .GetColFromID("antinm") : .Text = rsDt.Rows(ix).Item("antinmd").ToString
                        .Col = .GetColFromID("seq") : .Text = rsDt.Rows(ix).Item("dispseq").ToString
                    End If
                    
                    'anticd, f23.antinmd, f24.dispseq
                Next

            End With
            
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        msRetVal = Me.txtFilter.Text
        Me.Close()
    End Sub

    Private Sub btnExcute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcute.Click
        With Me.spdAnti
            Dim sTempFilter As String = ""
            Dim icnt As Integer = 0
            For ix As Integer = 0 To .MaxRows - 1
                .Row = ix + 1
                .Col = .GetColFromID("chk")
                If .Text = "1" Then
                    icnt += 1

                    'If icnt > 1 Then
                    '    MsgBox("항균제 코드를 한개만 선택해 주세요.")
                    '    Return
                    'End If
                    .Col = .GetColFromID("anticd")
                    Dim sAnticd As String = .Text
                    .Col = .GetColFromID("antinm")
                    Dim sAntinm As String = .Text
                    .Col = .GetColFromID("decrst")
                    If .Text.Trim = "" Then
                        MsgBox("판정이 선택되지 않았습니다. 확인해주세요")
                        Return
                    End If
                    Dim sDecRst As String = .Text
                    sTempFilter += sAnticd + Chr(124) + sAntinm + Chr(124) + sDecRst + Chr(3)
                End If
            Next

            Me.txtFilter.Text = sTempFilter

        End With

    End Sub
End Class