'>> 등록번호 변경
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommFN
Imports COMMON.CommConst
Imports SYSIF01
Imports LISAPP

Public Class FGR08_S04
    Private Const msFile As String = "File : FGR08_04.vb, Class : FGR08_04" & vbTab
    Private msEmrPrintName As String = ""

    Private Function sbReg() As Boolean

        msEmrPrintName = (New COMMON.CommPrint.PRT_Printer("EMRIMG")).GetInfo.PRTNM
        If sbCreateImg() = False Then
            Return False
        End If

        For ix As Integer = 0 To 60
            Dim a_proc As Process() = Diagnostics.Process.GetProcessesByName("ImageServerInAIBorker2005")
            If a_proc.Length < 1 Then
                Exit For
            Else
                System.Threading.Thread.Sleep(1000)
            End If
        Next

        With spdList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("bcno") : Dim sBcno As String = .Text
                .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text
                .Col = .GetColFromID("patnm") : Dim sPatnm As String = .Text

                Dim sFileNm As String = sBcno.Replace("-", "") + " " + sTestcd + " " + sPatnm.Replace(" ", "")
                Dim sFileNms As String = ""
                Dim al_Filenm As New ArrayList

                For ix2 As Integer = 1 To 20
                    If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix2.ToString() + ".jpg") Then
                        al_Filenm.Add("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix2.ToString() + ".jpg")
                    End If
                Next

                '<< JJH 코로나검사 이미지등록시 최종보고시간 update
                'Dim bfChk As DataTable = LISAPP.COMM.RstFn.fnGet_BfRst_Testcd()
                'If bfChk.Rows.Count > 0 Then
                '    Dim bfTestcd As String() = bfChk.Rows(0).Item("clsval").ToString.Split("/"c)

                '    For i As Integer = 0 To bfTestcd.Count - 1
                '        If bfTestcd(i) = sTestcd Then


                '            Exit For
                '        End If
                '    Next

                'End If

                If (New LISAPP.APP_R.AxRstFn).fnReg_IMAGE(sBcno.Replace("-", ""), sTestcd, al_Filenm) = False Then
                    MsgBox(sFileNm + " 오류 발생")
                    Return False
                End If

                For ix2 As Integer = 1 To 20
                    If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix2.ToString() + ".jpg") Then
                        IO.File.Delete("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix2.ToString() + ".jpg")
                    End If
                Next

            Next
        End With

        sbDisplay_Search()

        Return True

    End Function

    Public Function sbCreateImg() As Boolean
        Dim sFn As String = "sbCreateImg()"

        Try

            With spdList
                If .MaxRows < 0 Then Return False

                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                    .Col = .GetColFromID("imgyn") : Dim sImgChk As String = .Text

                    If sChk = "1" Then
                        If sImgChk = "X" Or (rboAll.Checked) Then
                            .Col = .GetColFromID("bcno") : Dim sBcno As String = .Text
                            .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text
                            .Col = .GetColFromID("patnm") : Dim sPatnm As String = .Text
                            If fnSaveImage(sBcno, sTestcd, sPatnm) = False Then
                                Return False
                            End If
                        End If
                    End If
                Next
            End With

            Return True

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Function

    Private Function fnSaveImage(ByVal rsBcno As String, ByVal rsTestcd As String, ByVal rsPatnm As String) As Boolean

        If msEmrPrintName = "" Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "[메뉴->MEDI@CK->이미지 프린트 설정]에서 이미지 프린트 설정해 주세요.!!")
            Return False
        End If

        Dim sFn As String = "Handles btnPrint.Click"
        Dim sFileNm As String = rsBcno.Replace("-", "") + " " + rsTestcd + " " + rsPatnm.Replace(" ", "")

        Try
            For ix As Integer = 1 To 10
                If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString + ".jpg") Then
                    IO.File.Delete("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString + ".jpg")
                End If

            Next


        Catch ex As Exception

        End Try

        System.Threading.Thread.Sleep(1000)

        Try

            Dim dt As DataTable
            Dim a_dr As DataRow()
            dt = LISAPP.APP_SP.fnGet_Rst_SpTest(rsBcno.Replace("-", ""), rsTestcd)

            a_dr = dt.Select("rstflg > '0'")
            Me.rtbStRst.set_SelRTF("", True)

            If a_dr.Length > 0 Then
                Me.rtbStRst.set_SelRTF(a_dr(0).Item("rstrtf").ToString)
            Else
                Return False
            End If

            Dim rPrtImg As Boolean = Me.rtbStRst.print_image(sFileNm, msEmrPrintName)

            If rPrtImg = False Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Sub FGR08_S04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        sbDisplayInit()

    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Try

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            sbDisplay_Search()

        Catch ex As Exception
            Fn.log(msFile, Err)
            MsgBox(msFile + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub sbDisplay_Search()
        Dim sFn As String = "sbDisplay_Search()"

        Try

            Dim sDateS As String = Me.dtpDateS.Value.ToShortDateString.ToString.Replace("-", "")
            Dim sDateE As String = Me.dtpDateE.Value.ToShortDateString.ToString.Replace("-", "")

            Dim regrst As New LISAPP.APP_R.AxRstFn
            Dim dt As DataTable = regrst.fnGet_ImgFile_List(sDateS, sDateE, Me.rboTkdt.Checked)
            Dim a_dr As DataRow()
            If Me.rboNoSend.Checked Then
                a_dr = dt.Select("imgchk = 'X'", "bcno")
                dt = Fn.ChangeToDataTable(a_dr)
            End If

            sbDisplay_Data(dt)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Public Sub sbDisplay_Data(ByVal dt As DataTable)
        Dim sFn As String = "sbDisplay_Data(DataTable)"

        Try

            With Me.spdList
                .MaxRows = 0

                If dt.Rows.Count < 1 Then Return

                .ReDraw = False

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows
                    Dim sBcno As String = dt.Rows(ix).Item("bcno").ToString
                    Dim spatnm As String = dt.Rows(ix).Item("patnm").ToString
                    Dim stestcd As String = dt.Rows(ix).Item("testcd").ToString
                    Dim sImgChk As String = dt.Rows(ix).Item("imgchk").ToString

                    .Col = .GetColFromID("bcno") : .Text = sBcno
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                    .Col = .GetColFromID("patnm") : .Text = spatnm
                    .Col = .GetColFromID("testcd") : .Text = stestcd
                    .Col = .GetColFromID("imgchk") : .Text = sImgChk

                    If sImgChk <> "O" Then
                        .BackColor = Drawing.Color.FromArgb(255, 230, 231)
                        .ForeColor = Drawing.Color.FromArgb(255, 0, 0)
                        .SetText(.GetColFromID("chk"), .Row, "1")

                        Dim sFileNm As String = sBcno.Replace("-", "") + " " + stestcd + " " + spatnm.Replace(" ", "")
                        .Col = .GetColFromID("imgyn")

                        If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_1.jpg") Then
                            .Text = "O"
                            .BackColor = Drawing.Color.FromArgb(225, 245, 243)
                            .ForeColor = Drawing.Color.Black
                        Else
                            .Text = "X"
                            .BackColor = Drawing.Color.FromArgb(255, 230, 231)
                            .ForeColor = Drawing.Color.FromArgb(255, 0, 0)
                        End If

                    Else
                        .BackColor = Drawing.Color.FromArgb(225, 245, 243)
                        .ForeColor = Drawing.Color.Black
                    End If


                Next


            End With


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try


    End Sub

    Public Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit()"


        Try

            Dim Now As Date = New LISAPP.APP_DB.ServerDateTime().GetDateTime

            Me.dtpDateS.Value = Now.AddDays(-1)
            Me.dtpDateE.Value = Now

            Me.spdList.MaxRows = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub


    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim sFn As String = "btnUpload_click()"

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            sbReg()

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub FGR08_S04_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        MdiTabControl.sbTabPageMove(Me)

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        sbDisplayInit()

    End Sub

    Private Sub chkall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkall.Click

        Try

            With spdList
                For ix As Integer = 1 To .MaxRows

                    .Row = ix
                    .Col = .GetColFromID("chk")



                    If chkall.Checked Then
                        .Text = "1"
                    Else
                        .Text = "0"
                    End If

                Next

            End With

        Catch ex As Exception

        End Try

    End Sub
End Class