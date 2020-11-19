Public NotInheritable Class ABOUT

    Private Sub ABOUT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sAppTitle As String = ""

        If My.Application.Info.Title <> "" Then
            sAppTitle = My.Application.Info.Title
        Else
            sAppTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If

        Me.lblProductName.Text += " :  " + My.Application.Info.ProductName
        Me.lblVersion.Text += " :  " + My.Application.Info.Version.ToString
        Me.lblCopyright.Text += " :  " + My.Application.Info.Copyright
        Me.lblDescription.Text += " :  " + My.Application.Info.Description

        Me.lblPCNm.Text += " :  " + My.Computer.Name
        Me.lblIPAddr.Text += " :  " + COMMON.CommFN.Fn.GetIPAddress("")
        Me.lblOs.Text += " :  " + My.Computer.Info.OSFullName

        Dim ulngMemAll As ULong = My.Computer.Info.TotalPhysicalMemory
        Dim sMemAll As String = ""

        If ulngMemAll / 1024 / 1024 > 512 Then
            sMemAll = (ulngMemAll / 1024 / 1024 / 1024).ToString("0.00") + " GB"
        Else
            sMemAll = (ulngMemAll / 1024 / 1024).ToString("0") + " MB"
        End If

        Me.lblMemory.Text += " :  " + sMemAll
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub lblAckAs_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblAckAs.LinkClicked
        Process.Start("http://www.ack.co.kr")
    End Sub

    Private Sub lblHosp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblHosp.LinkClicked
        Process.Start("http://www.nmc.or.kr")
    End Sub

End Class
