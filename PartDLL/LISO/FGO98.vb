Imports COMMON.CommFN
Imports COMMON.FVar.AppCfg

Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports spire

Imports DP01.DataProvider

Public Class FGO98

    Private Shared msFile As String = ""
    Private Sub btnToJpg_Click(sender As Object, e As EventArgs) Handles btnToJpg.Click

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyValue = Keys.Enter Then
            Dim sbcno As String = TextBox1.Text.Trim
            Dim sTclscd As String = ""
            Dim dt_getSpInfo As DataTable = (New DA01.DA_O_TEST).GetBcnoInfo(sbcno)

            If dt_getSpInfo.Rows.Count > 0 Then
                sTclscd = dt_getSpInfo.Rows(0).Item("tclscd")
            End If

            Dim dt As DataTable = DA01.DA_SF.Get_Rst_Special(sbcno, sTclsCd)

            If dt.Rows.Count > 0 Then
                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    Dim intStRst As Integer = 0

                    Me.rtbStRst.set_SelRTF(dt.Rows(intIdx).Item("rstrtf").ToString, True)
                    '  Me.rtbStRst.print_Data()
                    Dim sStr As String = fnSpecialTest_Compress(sbcno, sTclscd, Me.rtbStRst.get_SelRTF(True).Trim, "")
                    Dim sJPG As String = fnSpecialTest_Compress(sbcno, sTclscd, sStr)
                    ' fnSpecialTest_Compress(rsBcNo, rsTclsCd, Me.rtbStRst.get_SelRTF(True).Trim, "")
                Next
            End If

        End If
    End Sub
    Private Shared Function fnSpecialTest_Compress(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsFileNm As String) As String

        Dim sFn As String = "fnSpecialTest_Compress(string)"
        Dim strDir As String = System.Windows.Forms.Application.StartupPath + "\image"
        Dim m_zipProc As COMMON.ZipProc = New COMMON.ZipProc

        Try
            If m_zipProc Is Nothing Then m_zipProc = New COMMON.ZipProc

            Dim strZipFile As String = ""
            Dim strCurFile As String = ""

            strZipFile = rsBcNo + "_" + rsTclsCd

            If IO.File.Exists(strDir + "\" + strZipFile + ".jpg") Then
                IO.File.Delete(strDir + "\" + strZipFile + ".jpg")
            End If

            IO.File.Copy(rsFileNm, strDir + "\" + strZipFile + ".jpg")

            If File.Exists(strDir + "\" + strZipFile + ".jpg") Then

                m_zipProc.ArchiveNew(strDir + "\" + strZipFile + ".gzip", COMMON.ZipConstants.GZIP)
                m_zipProc.ArchiveAdd(strDir + "\" + strZipFile + ".jpg")
                m_zipProc.ArchiveClose()
            End If

            Return strDir + "\" + strZipFile + ".gzip"

        Catch ex As Exception

            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return False

        End Try
    End Function

    Private Shared Function fnSpecialTest_Compress(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal r_rtf As String, ByVal rsdd As String) As String
        Dim sFn As String = "fnSpecialTest_Compress"
        Dim sFile As String
        Dim sJPGFile As String
        Dim sDir As String
        Dim m_zipProc As COMMON.ZipProc = New COMMON.ZipProc

        Try
            If m_zipProc Is Nothing Then m_zipProc = New COMMON.ZipProc
            Dim fileNM As String = ""

            sDir = System.Windows.Forms.Application.StartupPath + "\image"
            sJPGFile = rsBcNo.Replace("-", "") + "_" + rsTclsCd + ".jpg"

            sDir = System.Windows.Forms.Application.StartupPath & "\SpecialTest"
            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = rsBcNo.Replace("-", "") & "_" & rsTclsCd.ToString & ".rtf"
            If IO.File.Exists(sDir + "\" + sFile) Then
                IO.File.Delete(sDir + "\" + sFile)
            End If


            Dim sw As New StreamWriter(sDir & "\" & sFile, True, System.Text.Encoding.UTF8)
            sw.Write(r_rtf)
            sw.Close()

            fileNM = sFile.Substring(0, sFile.LastIndexOf(".")) + ".gzip"

            m_zipProc.ArchiveNew(sDir + "\" + fileNM, COMMON.ZipConstants.GZIP)
            m_zipProc.ArchiveAdd(sDir & "\" & sFile)
            'm_zipProc.ArchiveAdd(sDir + "\" + sJPGFile)
            m_zipProc.ArchiveClose()

            '  Return sDir + "\" + fileNM
            Return fileNM
        Catch ex As Exception

            Fn.log(sFn, Err)
            MsgBox(sFn + vbCrLf + ex.Message)

            Return False

        End Try
    End Function

    'Private Shared Sub Main(ByVal args() As String)
    '    'Load RTF Document
    '    Dim document As New Document()
    '    document.LoadFromFile("E:\Work\Documents\WordDocuments\Blues Introduction.rtf", FileFormat.Rtf)

    '    'Save to Image
    '    Dim image As Image = document.SaveToImages(0, ImageType.Bitmap)
    '    image.Save("RTF2Image.jpg", ImageFormat.Jpeg)
    '    System.Diagnostics.Process.Start("RTF2Image.jpg")
    'End Sub

    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click

        Dim sDCDir As String = "C:\Program Files\GWH_LIS\image\" 'DefultCopyDirectory
        Dim sSCDir As String = "" 'SecondCopyDirectory
        Dim sNFilenm As String = Me.TextBox2.Text.Trim

        Dim sfileyn() As String = IO.Directory.GetFiles(sDCDir, "*.jpg")

        If sfileyn.Length > 0 Then
            For ix As Integer = 0 To sfileyn.Length - 1
                Dim sTemp() As String = sfileyn(ix).Split("\"c)
                Dim sfilenm As String = sTemp(sTemp.Length)

                If sfilenm.IndexOf("@") > -1 Then
                    Dim sArg() As String = sfilenm.Split("@"c)
                End If
            Next
            MsgBox("파일있음")
        Else
            MsgBox("파일없음")
        End If



    End Sub

    Private Sub btnEMR_Click(sender As System.Object, e As System.EventArgs) Handles btnEMR.Click
        Dim obj As Object = New O01.FGO97

        obj.show()
    End Sub

    Private Sub btnTestDataClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestDataClear.Click
        Dim sFn As String = "Convert_OCS_TO_LIS"
        Dim dt_ocs As New DataTable
        Dim dt As New DataTable
        Dim al As New ArrayList
        Dim iRet As Integer = 0

        Dim sSql As String = ""

        sSql = ""
        sSql += " SELECT fkocs "
        sSql += "   FROM mts0001_ocs"
        sSql += "  WHERE order_date <= to_date('20171112', 'yyyy/mm/dd')  "

        LisDbCommand()
        Dim dt2 As DataTable = LisDbExecuteQuery(sSql, al)

        For i As Integer = 0 To dt2.Rows.Count - 1



        Next



    End Sub
End Class