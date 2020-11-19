Imports System.IO
Imports COMMON.CommFN

Public Class MAINTEST
    Dim m_s_gubun As String = "O"
    Dim m_s_testcd As String = ""

    Private Sub sbKill_ProcESS()
        Dim a_proc As Process() = Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)

        Dim curProc As Process = Process.GetCurrentProcess()

        For i As Integer = a_proc.Length To 1 Step -1
            If curProc.Id <> a_proc(i - 1).Id Then
                a_proc(i - 1).Kill()
            End If
        Next
    End Sub

    Private Sub MAINTEST_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim sFn As String = "Sub MAIN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load"
        Try
            Me.Hide()

            Dim sArg As [String]() = System.Environment.GetCommandLineArgs()

            Dim sTmp As String = ""
            For ix As Integer = 0 To sArg.Length - 1
                sTmp += sArg(ix) + ","
            Next

            Me.Tag = "LOAD"

            For ix As Integer = 0 To sArg.Length - 1
                Select Case ix
                    Case 1  ' 바코드번호
                        m_s_Gubun = sArg(ix)
                    Case 2  ' TLIS 코드(OCS결과코드)
                        m_s_TestCd = sArg(ix)
                End Select
            Next

            sbKill_ProcESS()

            If m_s_Gubun = "E" Then End

            Me.Text = "ACK@TEST(검사정보 Ver " + Application.ProductVersion + ")"

            MdiMain.Frm = Me
            MdiMain.FrmMenu = Me.Menu

            Dim frm As New CDHELP.FGCDHELP_TEST(m_s_gubun, m_s_testcd)

            frm.Left = 0
            frm.Top = 0

            frm.ShowDialog(Me)

        Catch ex As Exception

        End Try

        Me.Close()
    End Sub
End Class
