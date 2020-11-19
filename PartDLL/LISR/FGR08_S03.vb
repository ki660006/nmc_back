Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing


Public Class FGR08_S03

    Private m_s_Bcno As String = ""
    Private m_s_Rstno As String = ""
    Private m_s_Patnm As String = ""
    Private m_s_FileNme As String = ""
    Private m_s_Testcd As String = ""

    Public Sub sbDisplay_Data()

        Try
            Dim sFileNm As String = ""
            Dim sDir As String = Application.StartupPath + "\image"
            Me.cboImgItem.Items.Clear()

            If m_s_FileNme = "" Then

                If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

                Dim regrst As New LISAPP.APP_R.AxRstFn

                Dim dt As DataTable = regrst.fnGet_File_Image_count(m_s_Bcno, m_s_Testcd)

                If dt.Rows.Count > 0 Then
                    For ix As Integer = 1 To dt.Rows.Count

                        Dim bcn As Boolean = True

                        Dim sRstno As String = dt.Rows(ix - 1).Item(2).ToString.Trim

                        sFileNm = sDir + "\" + m_s_Bcno + "_" + m_s_Testcd + "_" + m_s_Patnm + "_" + ix.ToString + ".jpg"

                        Me.cboImgItem.Items.Add(m_s_Bcno + "_" + m_s_Testcd + "_" + m_s_Patnm + "_" + ix.ToString)

                        If ix = dt.Rows.Count Then
                            bcn = False
                        End If

                        Dim a_btBuf As Byte() = regrst.fnGet_File_Image(m_s_Bcno, sRstno, bcn, m_s_Testcd)

                        'If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

                        Dim fs As IO.FileStream

                        If a_btBuf IsNot Nothing Then

                            If IO.File.Exists(sFileNm) Then
                                Try
                                    Threading.Thread.Sleep(100)
                                    IO.File.Delete(sFileNm)
                                Catch ex As Exception
                                    'Dim bmpTmp As Bitmap = New Bitmap(sFileNm)

                                    'Me.picFileImg.Image = CType(bmpTmp, Image)
                                    Return
                                End Try
                            End If

                            fs = New IO.FileStream(sFileNm, IO.FileMode.Create, FileAccess.Write)

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

                    Next

                Else

                    MsgBox("전송된 이미지 없습니다. 이미지검증을 해주시기바랍니다")
                    Return

                End If

            Else
                sFileNm = m_s_FileNme
            End If

            Dim bmpTmp As Bitmap = New Bitmap(sDir + "\" + m_s_Bcno + "_" + m_s_Testcd + "_" + m_s_Patnm + "_1.jpg")
            Me.picFileImg.Image = CType(bmpTmp, Image)

            Me.cboImgItem.SelectedIndex = 0
            ' Try
            '   Process.Start(sDir + "\" + sFileNm)
            'Catch ex As Exception
            'End Try

            'Me.Close()
            Me.ShowDialog()


        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
            Return
        End Try
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsBcno As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        m_s_Bcno = rsBcno

    End Sub

    Public Sub New(ByVal rsBcno As String, ByVal rsPatnm As String, ByVal rsTestcd As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        m_s_Bcno = rsBcno
        m_s_Patnm = rsPatnm
        m_s_Testcd = rsTestcd

    End Sub


    Private Sub FGR03_S03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'sbDisplay_Data()

        ' Me.WindowState = FormWindowState.Maximized


    End Sub

    Private Sub cboImgItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboImgItem.SelectedIndexChanged
        Try

            Dim sDir As String = Application.StartupPath + "\image"
            Dim bmpTmp As Bitmap = New Bitmap(sDir + "\" + Me.cboImgItem.SelectedItem.ToString + ".jpg")
            Me.picFileImg.Image = CType(bmpTmp, Image)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
            Return
        End Try
    End Sub

    Private Sub FGR08_S03_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Refresh()
        Me.picFileImg.Image = Nothing
        Me.picFileImg.Dispose()
        Me.Close()
    End Sub
End Class