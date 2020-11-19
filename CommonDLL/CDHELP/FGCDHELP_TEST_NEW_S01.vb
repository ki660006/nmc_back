Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing

Public Class FGCDHELP_TEST_NEW_S01
    Public Sub New()

        ' 이 호출은 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Private Sub FGCDHELP_TEST_NEW_S01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sbDisplay_Data()

        Me.WindowState = Windows.Forms.FormWindowState.Maximized

    End Sub

    Private Sub sbDisplay_Data()

        Try
            Dim sFileNm As String = ""
            Dim sDir As String = Application.StartupPath + "\image"

            'If m_s_FileNme = "" Then

            If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)


            Dim a_btBuf As Byte() = (New CDHELP.DA_CDHELP_TEST).fnGet_File_Image(sFileNm)

            If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

            Dim fs As IO.FileStream

            If a_btBuf IsNot Nothing Then

                If IO.File.Exists(sFileNm) Then
                    Try
                        Threading.Thread.Sleep(100)
                        IO.File.Delete(sFileNm)
                    Catch ex As Exception
                        Dim bmpTmp As Bitmap = New Bitmap(sFileNm)

                        Me.picFileImg.Image = CType(bmpTmp, Image)
                        Return
                    End Try
                End If

                fs = New IO.FileStream(sDir + "\" + sFileNm, IO.FileMode.Create, FileAccess.Write)

            Else
                Me.picFileImg.Image = Nothing

                Return
            End If

            Dim bw As IO.BinaryWriter = New IO.BinaryWriter(fs)

            bw.Write(a_btBuf)
            bw.Flush()

            bw.Close()
            fs.Close()
            fs = Nothing

            'Else
            '    sFileNm = m_s_FileNme
            'End If

            Try
                Process.Start(sDir + "\" + sFileNm)
            Catch ex As Exception
            End Try

            Me.Close()


        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub
End Class