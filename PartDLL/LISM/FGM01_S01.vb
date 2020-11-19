Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommFN
Imports COMMON.CommConst
Imports COMMON.SVar


Public Class FGM01_S01

    Private msFormText As String = ""
    Private msReturn As String = ""
    Private msSearchid As String = ""
    Private msSearchnm As String = ""
    Private msMIC As String = ""
    Private msDisk As String = ""



    Public WriteOnly Property FormText() As String
        Set(ByVal value As String)
            msFormText = value
        End Set
    End Property

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        '추가처방 MIC, Disk 선택 기능 추가 20150427 허용석
        If chb_MIC.Checked And chb_Disk.Checked Then
            msMIC = "MIC"
            msDisk = "Disk"
        ElseIf chb_MIC.Checked Then
            msMIC = "MIC"
            msDisk = ""
        ElseIf chb_Disk.Checked Then
            msMIC = ""
            msDisk = "Disk"
        Else
            MsgBox("처방을 선택해주세요.")
            Return
        End If

        If (fnGet_PrcpDrid_info()) Then
            msReturn = msSearchid + "/" + msSearchnm + "^" + msMIC + "/" + msDisk
        Else
            msReturn = ""
        End If

        Me.Close()
        '20150427 END
    End Sub

    Public Function Display_Form() As String

        Me.ShowDialog()
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Me.Hide()

        Return msReturn

    End Function

    Public Function fnGet_PrcpDrid_info() As Boolean

        If (Me.txtDrid.Text.Trim.Length > 0) Then
            Dim dt As DataTable = LISAPP.APP_G.CommFn.fnGet_PrcpDrid_info(Me.txtDrid.Text.Trim)

            If dt.Rows.Count > 0 Then
                msSearchid = dt.Rows(0).Item("usrid").ToString
                msSearchnm = dt.Rows(0).Item("usrnm").ToString
                Return True
            Else
                MsgBox("입력하신 ID는 의사 아이디가 아닙니다. 처방을 낼수 없습니다.")
                Return False
            End If
        Else
            MsgBox("처방의ID를 입력하세요")
        End If

    End Function


    Private Sub txtDrid_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDrid.KeyDown

        If e.KeyCode = Windows.Forms.Keys.Enter Then
            '추가처방 MIC, Disk 선택 기능 추가 20150427 허용석
            If chb_MIC.Checked And chb_Disk.Checked Then
                msMIC = "MIC"
                msDisk = "Disk"
            ElseIf chb_MIC.Checked Then
                msMIC = "MIC"
                msDisk = ""
            ElseIf chb_Disk.Checked Then
                msMIC = ""
                msDisk = "Disk"
            Else
                MsgBox("처방을 선택해주세요.")
                Return
            End If

            If (fnGet_PrcpDrid_info()) Then
                msReturn = msSearchid + "/" + msSearchnm + "^" + msMIC + "/" + msDisk
            Else
                msReturn = ""
            End If

            Me.Close()
            '20150427 END

            'If (fnGet_PrcpDrid_info()) Then
            '    msReturn = msSearchid + "/" + msSearchnm + "^" + msMIC + "/" + msDisk
            'Else
            '    msReturn = ""
            'End If
            'Me.Close()
        End If

    End Sub

    'Private Sub FGM01_S01_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    If msReturn = "" Then
    '        msReturn = ""
    '    End If
    'End Sub

    Private Sub btncnl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncnl.Click
        msReturn = ""
        Close()
    End Sub

    Private Sub FGM01_S01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtDrid.Text = "217002"
    End Sub
End Class