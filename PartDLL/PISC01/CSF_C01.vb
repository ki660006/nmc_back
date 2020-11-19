Imports COMMON.CommFN

Public Class SF_COLL
    Private Const msFile As String = "File : CSF_C01.vb, Class : SF_COLL" & vbTab

    Public Shared Function fnAutoUpDate(ByVal rsPrgId As String, ByVal rsAppPath As String, ByVal rsPrgNm As String) As Boolean
        Dim sFileVer As String = Diagnostics.FileVersionInfo.GetVersionInfo("C01.DLL").FileVersion

        Dim sDepDt As String = DA01.DA_SF.Find_DepFile_NewVersion(rsPrgId, "C01.DLL", sFileVer)

        If IsDate(sDepDt) = False Then
            Return False
        End If

        If MsgBox("확인을 누르시면 최신 버전으로 업그레이드 됩니다.", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.OkCancel, "업그레이드") = MsgBoxResult.Cancel Then
            Return False
        End If

        sbAutoUpdateLIS(rsAppPath, rsPrgNm)

        Return True

    End Function

    Private Shared Sub sbAutoUpdateLIS(ByVal rsAppPath As String, ByVal rsPrgNm As String)
        Dim sFn As String = "Private Sub sbAutoUpdateLIS()"

        Try
            Dim sArgs As String = ""

            sArgs += "D" + " "
            sArgs += Convert.ToChar(34) + rsPrgNm + Convert.ToChar(34) + " "
            sArgs += Convert.ToChar(34) + "ACK" + Convert.ToChar(34) + " "
            sArgs += Convert.ToChar(34) + My.Computer.Name + "," + Fn.GetIPAddress("") + Convert.ToChar(34)

            Process.Start(rsAppPath + "\DEP\LIS_DEPs.exe", sArgs)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, "C01.DLL" + "-" + sFn)

        End Try
    End Sub
End Class
