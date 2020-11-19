'>>> 위탁검사 결과 저장 및 보고

Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports System.Runtime.InteropServices

Public Class FGR07_SCL
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGR07.vb, Class : FGR07" & vbTab

    Private malSect As New ArrayList    ' 계 정보
    Private mobjDAF As New LISAPP.APP_F_EXLAB
    Private m_al_FileList As New ArrayList

    Friend WithEvents chkRst As System.Windows.Forms.CheckBox
    Friend WithEvents chkImg As System.Windows.Forms.CheckBox

    Private Const mc_sOrdDt As String = "A"
    Private Const mc_sRegNo As String = "B"
    Private Const mc_sPatNm As String = "C"
    Private Const mc_sSexAge As String = "D"
    Private Const mc_sIdNo As String = "E"
    Private Const mc_sDrNm As String = "F"
    Private Const mc_sDeptNm As String = "G"
    Private Const mc_sWardNm As String = "H"
    Private Const mc_sEntDay As String = "I"
    Private Const mc_sBcNo As String = "J"
    Private Const mc_sSpcNm As String = "K"
    Private Const mc_sDiagNm As String = "L"
    Private Const mc_sDrugNm As String = "M"
    Private Const mc_sDrRmk As String = "N"
    Private Const mc_sCollDt As String = "O"
    Private Const mc_sTkDt As String = "P"
    Private Const mc_sFnDt As String = "Q"
    Private Const mc_sFnUsr As String = "R"
    Private Const mc_sMwDt As String = "S"
    Private Const mc_sMwUsr As String = "T"
    Private Const mc_sDoctNo As String = "U"

    Private msOrigin_RstRTF As String = ""
    Private m_dt_SpTest As DataTable
    Friend WithEvents rtbSt As AxAckRichTextBox.AxAckRichTextBox
    Friend WithEvents picBuf As System.Windows.Forms.PictureBox
    Friend WithEvents cboPartSlip As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnFN As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents txtStRstTxtF As System.Windows.Forms.TextBox
    Friend WithEvents txtStRstTxtM As System.Windows.Forms.TextBox
    Friend WithEvents txtStRstTxtR As System.Windows.Forms.TextBox
    Friend WithEvents btnImage As CButtonLib.CButton
    Private m_al_StSub As New ArrayList
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboState As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboDate As System.Windows.Forms.ComboBox

    Private msEmrPrintName As String = ""

    Private Function fnSaveImage(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsRegNo As String) As Boolean

        If msEmrPrintName = "" Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "[메뉴->MEDI@CK->이미지 프린트 설정] 에서 이미지 프린트 설정해 주세요.!!")
            Return False
        End If


        Dim sFn As String = "Handles btnPrint.Click"
        Dim sFileNm As String = rsBcNo + " " + rsTestCd + " " + rsRegNo.Replace(" ", "")

        Try
            For ix As Integer = 1 To 10
                If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString + ".jpg") Then
                    IO.File.Delete("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString + ".jpg")
                End If

            Next

        Catch ex As Exception

        End Try


        Try
            Me.rtbStRst.print_image(sFileNm, msEmrPrintName)

            Dim iImgCnt As Integer = 0
            Dim dt As DataTable = (New LISAPP.APP_F_SPTEST).GetSpTestInfo(rsTestCd)

            iImgCnt = dt.Rows.Count

            System.Threading.Thread.Sleep(3000 * iImgCnt)

            Dim iLoop As Integer = 0

            Do While True

                If iLoop > 100 Then Exit Do

                If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_1.jpg") Then Exit Do

                System.Threading.Thread.Sleep(1500 * (m_al_StSub.Count - 1))
                iLoop += 1
            Loop

            Dim sFileNms As String = ""
            Dim al_FileNm As New ArrayList

            For ix As Integer = 1 To 20
                If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString() + ".jpg") Then
                    al_FileNm.Add("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString() + ".jpg")
                End If
            Next

            If al_FileNm.Count > 0 Then
                System.Threading.Thread.Sleep(2000)

                Return (New LISAPP.APP_R.AxRstFn).fnReg_IMAGE(rsBcNo, rsTestCd, al_FileNm)
            Else
                Return False
            End If

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    Private Sub sbStSub_View_DbField(ByVal rsBcNo As String, ByVal rsTestcd As String, ByVal rsRstFlag As String)
        Dim rtb As AxAckRichTextBox.AxAckRichTextBox
        Dim dt As New DataTable
        Dim sBcNo As String = Fn.BCNO_View(rsBcNo, True)

        rtb = Me.rtbStRst

        dt = LISAPP.APP_SP.fnGet_SpcInfo_bcno(rsBcNo)
        If dt.Rows.Count > 0 Then
            Dim sPatInfo() As String = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

            rtb.set_DbField_Value(Convert.ToChar(2), mc_sOrdDt, Convert.ToChar(3), dt.Rows(0).Item("orddt").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sRegNo, Convert.ToChar(3), dt.Rows(0).Item("regno").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sPatNm, Convert.ToChar(3), sPatInfo(0).Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sSexAge, Convert.ToChar(3), dt.Rows(0).Item("sexage").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sIdNo, Convert.ToChar(3), sPatInfo(3).Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sDrNm, Convert.ToChar(3), dt.Rows(0).Item("doctornm").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sDeptNm, Convert.ToChar(3), dt.Rows(0).Item("deptnm").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sWardNm, Convert.ToChar(3), dt.Rows(0).Item("wardroom").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sEntDay, Convert.ToChar(3), dt.Rows(0).Item("entdt").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sBcNo, Convert.ToChar(3), sBcNo)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sSpcNm, Convert.ToChar(3), dt.Rows(0).Item("spcnmd").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sDiagNm, Convert.ToChar(3), dt.Rows(0).Item("diagnm").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sDrugNm, Convert.ToChar(3), dt.Rows(0).Item("doctorrmk").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sDrRmk, Convert.ToChar(3), "")
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sCollDt, Convert.ToChar(3), dt.Rows(0).Item("colldt").ToString().Trim)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sTkDt, Convert.ToChar(3), dt.Rows(0).Item("tkdt").ToString().Trim)

            If rsRstFlag = "3" Then
                rtb.set_DbField_Value(Convert.ToChar(2), mc_sFnDt, Convert.ToChar(3), New LISAPP.APP_DB.ServerDateTime().GetDateTime.ToString("yyyy-MM-dd HH:mm"))
                rtb.set_DbField_Value(Convert.ToChar(2), mc_sFnUsr, Convert.ToChar(3), USER_INFO.USRNM)
                rtb.set_DbField_Value(Convert.ToChar(2), mc_sDoctNo, Convert.ToChar(3), USER_INFO.N_WARDorDEPT)
            End If

            If rsRstFlag = "2" Then
                rtb.set_DbField_Value(Convert.ToChar(2), mc_sMwDt, Convert.ToChar(3), New LISAPP.APP_DB.ServerDateTime().GetDateTime.ToString("yyyy-MM-dd HH:mm"))
                rtb.set_DbField_Value(Convert.ToChar(2), mc_sMwUsr, Convert.ToChar(3), USER_INFO.USRNM)
                rtb.set_DbField_Value(Convert.ToChar(2), mc_sDoctNo, Convert.ToChar(3), USER_INFO.N_WARDorDEPT)
            End If
        End If

        '-- 2008-02-13 YOOEJ Add(관련검사 표시)
        Dim sRegNo As String = ""
        Dim sSpcCd As String = ""
        Dim sTkDt As String = ""

        dt = LISAPP.APP_SP.fnGet_SpcInfo_TkSpcRegno(rsBcNo, rsTestcd)
        If dt.Rows.Count > 0 Then
            sRegNo = dt.Rows(0).Item("regno").ToString
            sTkDt = dt.Rows(0).Item("tkdt").ToString + " 23:59:59"
            sSpcCd = dt.Rows(0).Item("spccd").ToString
        Else
            Exit Sub
        End If

        dt = Nothing

        dt = LISAPP.APP_SP.fnGet_Rst_SpTest_Ref(sRegNo, rsTestcd, sSpcCd, sTkDt)
        If dt.Rows.Count > 0 Then
            For intIdx As Integer = 0 To dt.Rows.Count - 1
                Dim sTestcd As String = ""
                Dim sRst As String = ""

                sTestcd = "Y" + dt.Rows(intIdx).Item("testcd").ToString.PadRight(7) + dt.Rows(intIdx).Item("spccd").ToString

                sRst = dt.Rows(intIdx).Item("viewrst").ToString

                rtb.set_DbField_Value(Convert.ToChar(2), sTestcd, Convert.ToChar(3), sRst)
            Next
        End If

    End Sub

    'Private Function fnStSub_View(ByVal rsBcNo As String, ByVal rsTestcd As String, ByVal r_al_FileNm As ArrayList, ByVal rsRstFlag As String) As Integer

    '    Call sbGet_SpTest(rsTestcd)

    '    Me.picBuf.Dispose()
    '    Me.picBuf.Image = Nothing
    '    Me.picBuf.Refresh()

    '    Me.rtbSt.set_SelRTF("", True)
    '    Me.rtbStRst.set_SelRTF("", True)

    '    Dim sRTF As String = "", sRTF_All As String = ""

    '    For ix As Integer = 0 To m_al_StSub.Count - 1
    '        If ix < r_al_FileNm.Count Then
    '            If IO.File.Exists(r_al_FileNm(ix).ToString) = False Then Return -1

    '            Dim bmpBuf As Bitmap = New Bitmap(r_al_FileNm(ix).ToString)

    '            If CType(m_al_StSub(ix), StSubInfo).Type = "2" Then
    '                sRTF = CType(m_al_StSub(ix), StSubInfo).RTF.Trim
    '                Select Case CType(m_al_StSub(ix), StSubInfo).ImgType
    '                    Case "0"
    '                        '자동
    '                        Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.AutoSize

    '                    Case "1"
    '                        '고정
    '                        Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.StretchImage

    '                        Me.picBuf.Width = Convert.ToInt32(CType(m_al_StSub(ix), StSubInfo).ImgSizeW)
    '                        Me.picBuf.Height = Convert.ToInt32(CType(m_al_StSub(ix), StSubInfo).ImgSizeH)
    '                End Select

    '                Me.picBuf.Image = bmpBuf
    '                Me.picBuf.Refresh()

    '                Dim imgTot As Drawing.Image = Me.picBuf.Image

    '                Select Case CType(m_al_StSub(ix), StSubInfo).ImgType
    '                    Case "0"
    '                        '자동
    '                        Me.picBuf.Image.Save(Windows.Forms.Application.StartupPath + "\tmpfile.jpg", Drawing.Imaging.ImageFormat.Jpeg)

    '                    Case "1"
    '                        '고정

    '                        '그림소스의 너비, 높이
    '                        Dim iTotalW As Integer = Me.picBuf.Width
    '                        Dim iTotalH As Integer = Me.picBuf.Height

    '                        '자르고자하는 영역의 X, Y, 너비, 높이
    '                        Dim iAreaX As Integer = 0
    '                        Dim iAreaY As Integer = 0
    '                        Dim iAreaW As Integer = Convert.ToInt32(CType(m_al_StSub(ix), StSubInfo).ImgSizeW)
    '                        Dim iAreaH As Integer = Convert.ToInt32(CType(m_al_StSub(ix), StSubInfo).ImgSizeH)

    '                        Dim bmpArea As Drawing.Bitmap = New Drawing.Bitmap(iAreaW, iAreaH)

    '                        Me.picBuf.Image = bmpArea

    '                        Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(Me.picBuf.Image)

    '                        Me.picBuf.Width = iAreaW
    '                        Me.picBuf.Height = iAreaH

    '                        g.DrawImage(imgTot, -iAreaX, -iAreaY, iTotalW, iTotalH)

    '                        g.Dispose()

    '                        Me.picBuf.Image.Save(Windows.Forms.Application.StartupPath + "\tmpfile.jpg", Drawing.Imaging.ImageFormat.Jpeg)

    '                End Select

    '                'Me.rtbSt.set_Image(picBuf.Image, True)
    '                Me.rtbSt.set_Lock(False)
    '                Me.rtbSt.set_SelRTF("", True)
    '                Me.rtbSt.set_Image(Windows.Forms.Application.StartupPath + "\tmpfile.jpg", False)
    '                Me.rtbSt.set_Lock(True)

    '                sRTF = Me.rtbSt.get_SelRTF(True).Trim
    '            End If
    '        ElseIf CType(m_al_StSub(ix), StSubInfo).Type <> "2" Then
    '            sRTF = CType(m_al_StSub(m_al_StSub.Count - 1), StSubInfo).RTF.Trim
    '        End If

    '        If m_al_StSub.Count = 1 Then sRTF_All = sRTF : Exit For

    '        If ix = 0 Then
    '            '맨 마지막 제거
    '            sRTF_All += sRTF.Substring(0, sRTF.Length - 1)
    '        ElseIf ix = m_al_StSub.Count - 1 Then
    '            '맨 처음 제거
    '            If sRTF.Length > 1 Then sRTF_All += sRTF.Substring(1)
    '        Else
    '            '맨 처음과 맨 마지막 제거
    '            If sRTF.Length > 2 Then
    '                sRTF_All += sRTF.Substring(1, sRTF.Length - 2)
    '            End If
    '        End If
    '    Next

    '    Me.rtbStRst.set_SelRTF(sRTF_All, True)

    '    sbStSub_View_DbField(rsBcNo, rsTestcd, rsRstFlag)
    'End Function

    Private Function fnStSub_View(ByVal rsBcNo As String, ByVal rsTestcd As String, ByVal r_al_FileNm As ArrayList, ByVal rsRstFlag As String) As Integer

        Call sbGet_SpTest(rsTestcd)

        Me.picBuf.Dispose()
        Me.picBuf.Image = Nothing
        Me.picBuf.Refresh()

        Me.rtbSt.set_SelRTF("", True)
        Me.rtbStRst.set_SelRTF("", True)

        Dim sRTF As String = "", sRTF_All As String = ""

        For ix As Integer = 0 To m_al_StSub.Count - 1
            sRTF = ""
            If ix < r_al_FileNm.Count Then
                If IO.File.Exists(r_al_FileNm(ix).ToString) = False Then Return -1

                Dim bmpBuf As Bitmap = New Bitmap(r_al_FileNm(ix).ToString)

                If CType(m_al_StSub(ix), StSubInfo).Type = "2" Then
                    sRTF = CType(m_al_StSub(ix), StSubInfo).RTF.Trim
                    Select Case CType(m_al_StSub(ix), StSubInfo).ImgType
                        Case "0"
                            '자동
                            Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.AutoSize

                        Case "1"
                            '고정
                            Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.StretchImage

                            Me.picBuf.Width = Convert.ToInt32(CType(m_al_StSub(ix), StSubInfo).ImgSizeW)
                            Me.picBuf.Height = Convert.ToInt32(CType(m_al_StSub(ix), StSubInfo).ImgSizeH)
                    End Select

                    Me.picBuf.Image = bmpBuf
                    Me.picBuf.Refresh()

                    Dim imgTot As Drawing.Image = Me.picBuf.Image

                    Select Case CType(m_al_StSub(ix), StSubInfo).ImgType
                        Case "0"
                            '자동
                            Me.picBuf.Image.Save(Windows.Forms.Application.StartupPath + "\tmpfile.jpg", Drawing.Imaging.ImageFormat.Jpeg)

                        Case "1"
                            '고정

                            '그림소스의 너비, 높이
                            Dim iTotalW As Integer = Me.picBuf.Width
                            Dim iTotalH As Integer = Me.picBuf.Height

                            '자르고자하는 영역의 X, Y, 너비, 높이
                            Dim iAreaX As Integer = 0
                            Dim iAreaY As Integer = 0
                            Dim iAreaW As Integer = Convert.ToInt32(CType(m_al_StSub(ix), StSubInfo).ImgSizeW)
                            Dim iAreaH As Integer = Convert.ToInt32(CType(m_al_StSub(ix), StSubInfo).ImgSizeH)

                            Dim bmpArea As Drawing.Bitmap = New Drawing.Bitmap(iAreaW, iAreaH)

                            Me.picBuf.Image = bmpArea

                            Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(Me.picBuf.Image)

                            Me.picBuf.Width = iAreaW
                            Me.picBuf.Height = iAreaH

                            g.DrawImage(imgTot, -iAreaX, -iAreaY, iTotalW, iTotalH)

                            g.Dispose()

                            Me.picBuf.Image.Save(Windows.Forms.Application.StartupPath + "\tmpfile.jpg", Drawing.Imaging.ImageFormat.Jpeg)

                    End Select

                    'Me.rtbSt.set_Image(picBuf.Image, True)
                    Me.rtbSt.set_Lock(False)
                    Me.rtbSt.set_SelRTF("", True)
                    Me.rtbSt.set_Image(Windows.Forms.Application.StartupPath + "\tmpfile.jpg", False)
                    Me.rtbSt.set_Lock(True)

                    sRTF = Me.rtbSt.get_SelRTF(True).Trim
                End If
            ElseIf CType(m_al_StSub(ix), StSubInfo).Type <> "2" Then
                sRTF = CType(m_al_StSub(m_al_StSub.Count - 1), StSubInfo).RTF.Trim
            End If

            If m_al_StSub.Count = 1 Then sRTF_All = sRTF : Exit For

            If sRTF <> "" Then
                If ix = 0 Then
                    '맨 마지막 제거
                    sRTF_All += sRTF.Substring(0, sRTF.Length - 1)
                ElseIf ix = m_al_StSub.Count - 1 Then
                    '맨 처음 제거
                    If sRTF.Length > 1 Then sRTF_All += sRTF.Substring(1)
                Else
                    '맨 처음과 맨 마지막 제거
                    If sRTF.Length > 2 Then
                        sRTF_All += sRTF.Substring(1, sRTF.Length - 2)
                    End If
                End If
            End If

        Next

        Me.rtbStRst.set_SelRTF(sRTF_All, True)

        sbStSub_View_DbField(rsBcNo, rsTestcd, rsRstFlag)
    End Function

    Private Sub sbStSub_View(ByVal rsBcNo As String, ByVal rsTestcd As String, ByVal strFileNm As String, ByVal rsRstFlag As String)

        Call sbGet_SpTest(rsTestcd)

        Me.picBuf.Dispose()
        Me.picBuf.Image = Nothing
        Me.picBuf.Refresh()

        Me.rtbSt.set_SelRTF("", True)
        Me.rtbStRst.set_SelRTF("", True)

        Dim bmpBuf As Bitmap = New Bitmap(strFileNm)
        Dim sRTF As String = "", sRTF_All As String = ""
        Dim iCnt As Integer = Me.m_al_StSub.Count

        If iCnt > 0 Then
            For i As Integer = 1 To iCnt
                If CType(m_al_StSub(i - 1), StSubInfo).Type = "2" Then
                    sRTF = CType(m_al_StSub(i - 1), StSubInfo).RTF.Trim
                    Select Case CType(m_al_StSub(i - 1), StSubInfo).ImgType
                        Case "0"
                            '자동
                            Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.AutoSize

                        Case "1"
                            '고정
                            Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.StretchImage

                            Me.picBuf.Width = Convert.ToInt32(CType(m_al_StSub(i - 1), StSubInfo).ImgSizeW)
                            Me.picBuf.Height = Convert.ToInt32(CType(m_al_StSub(i - 1), StSubInfo).ImgSizeH)
                    End Select

                    Me.picBuf.Image = bmpBuf
                    Me.picBuf.Refresh()

                    Me.rtbSt.set_Image(picBuf.Image, True)

                    sRTF = Me.rtbSt.get_SelRTF(True).Trim
                Else
                    sRTF = CType(m_al_StSub(i - 1), StSubInfo).RTF.Trim
                End If

                If iCnt = 1 Then
                    sRTF_All = sRTF
                    Exit For
                End If

                If i = 1 Then
                    '맨 마지막 제거
                    sRTF_All += sRTF.Substring(0, sRTF.Length - 1)

                ElseIf i = iCnt Then
                    '맨 처음 제거
                    sRTF_All += sRTF.Substring(1)

                Else
                    '맨 처음과 맨 마지막 제거
                    If sRTF.Length > 2 Then
                        sRTF_All += sRTF.Substring(1, sRTF.Length - 2)
                    End If
                End If
            Next

            Me.rtbStRst.set_SelRTF(sRTF_All, True)
        End If

        sbStSub_View_DbField(rsBcNo, rsTestcd, rsRstFlag)
    End Sub

    Private Sub sbGet_SpTest(ByVal rstestcd As String)
        Dim sFn As String = "sbDisplay_BcNo_SpTest(string)"

        Try
            Dim dt As DataTable = (New LISAPP.APP_F_SPTEST).GetSpTestInfo(rstestcd)

            m_dt_SpTest = dt.Copy()

            'm_al_StSub 초기화
            m_al_StSub.Clear()

            For i As Integer = 1 To m_dt_SpTest.Rows.Count
                Dim si As New StSubInfo

                m_al_StSub.Add(si)

                si = Nothing
            Next

            m_al_StSub.TrimToSize()

            With m_dt_SpTest
                For i As Integer = 1 To .Rows.Count
                    Dim si As New StSubInfo

                    si.Name = .Rows(i - 1).Item("stsubnm").ToString().Trim
                    si.Type = .Rows(i - 1).Item("stsubtype").ToString().Trim
                    si.ImgType = .Rows(i - 1).Item("imgtype").ToString().Trim
                    si.ImgSizeW = .Rows(i - 1).Item("imgsizew").ToString().Trim
                    si.ImgSizeH = .Rows(i - 1).Item("imgsizeh").ToString().Trim
                    si.RTF = .Rows(i - 1).Item("stsubrtf").ToString().Trim
                    si.ExPrg = .Rows(i - 1).Item("stsubexprg").ToString().Trim

                    m_al_StSub(i - 1) = si
                    si = Nothing


                    If i = 1 Then
                        Me.txtStRstTxtR.Text = .Rows(i - 1).Item("strsttxtr").ToString.Trim
                        Me.txtStRstTxtM.Text = .Rows(i - 1).Item("strsttxtm").ToString.Trim
                        Me.txtStRstTxtF.Text = .Rows(i - 1).Item("strsttxtf").ToString.Trim
                    End If
                Next

            End With


        Catch ex As Exception
            MsgBox(sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbReg(ByVal riRegStep As Integer)
        Dim sFn As String = "Sub fnReg(string)"

        Dim RstInfo As New stu_RstInfo
        Dim SmpInfo As New stu_SampleInfo

        Dim arlRstInfo As New ArrayList
        Dim arlCmtInfo As New ArrayList
        Dim arlOldCmmt As New ArrayList

        Dim arlBcNotestcd As New ArrayList

        Dim sRstCmt As String = ""
        Dim sRst As String = ""
        Dim sTestCd As String = ""
        Dim sRegNo As String = ""
        Dim sCBcNo As String = ""
        Dim sOBcNo As String = ""
        Dim iCmtNo As Integer = 0

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim iDisable As Integer = 0

            Dim sMsg As String = ""

            Select Case riRegStep
                Case 1
                    sMsg += "결과저장 하시겠습니까?"
                Case 2
                    sMsg += "결과확인 하시겠습니까?"
                Case 3
                    sMsg += "결과검증 하시겠습니까?"
            End Select

            If MsgBox(sMsg, MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If

            sMsg = ""

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("rstval") : sRst = .Text
                    .Col = .GetColFromID("chk")
                    If .Text = "1" And sRst <> "별지참조" Then
                        .Row = intRow
                        .Col = .GetColFromID("bcno") : sCBcNo = .Text
                        .Col = .GetColFromID("rstval") : sRst = .Text
                        .Col = .GetColFromID("rstcmt") : sRstCmt = .Text
                        .Col = .GetColFromID("testcd") : sTestCd = .Text

                        If sOBcNo <> "" And sCBcNo <> sOBcNo Then
                            If arlRstInfo.Count > 0 Then

                                Dim arySUCC As New ArrayList
                                Dim da_regrst As New LISAPP.APP_R.RegFn
                                Dim iRet As Integer = 0

                                If PRG_CONST.BCCLS_MicorBio.Contains(sOBcNo.Substring(8, 2)) Then
                                    iRet = da_regrst.RegServer(arlRstInfo, SmpInfo, arySUCC)
                                Else
                                    iRet = da_regrst.RegServer(arlRstInfo, SmpInfo, arySUCC, False)
                                End If
                                If iRet < 1 Then
                                    If sMsg <> "" Then
                                        sMsg += ", "
                                    End If
                                    sMsg += sOBcNo

                                End If

                                fnDisplay_ResultOK(arlBcNotestcd, iRet)
                            End If

                            arlRstInfo.Clear()
                            arlCmtInfo.Clear()
                            arlOldCmmt.Clear()

                            arlBcNotestcd.Clear()

                            iCmtNo = 0

                        End If
                        sOBcNo = sCBcNo

                        .Row = intRow
                        .Col = .GetColFromID("regno") : sRegNo = .Text

                        If sRst <> "" Then
                            RstInfo = New STU_RstInfo

                            RstInfo.TestCd = sTestCd
                            RstInfo.OrgRst = sRst '+ IIf(sRstCmt = "", "", vbCrLf + vbCrLf + sRstCmt).ToString
                            RstInfo.RstCmt = sRstCmt

                            arlRstInfo.Add(RstInfo)

                            arlBcNotestcd.Add(sCBcNo + "|" + sTestCd)
                        End If

                        SmpInfo.BCNo = sCBcNo
                        SmpInfo.EqCd = ""
                        SmpInfo.UsrID = USER_INFO.USRID
                        SmpInfo.UsrIP = USER_INFO.LOCALIP
                        SmpInfo.IntSeqNo = ""
                        SmpInfo.Rack = ""
                        SmpInfo.Pos = ""
                        SmpInfo.EqBCNo = ""

                        '>
                        SmpInfo.SenderID = ""
                        SmpInfo.RegStep = riRegStep.ToString

                    End If
                Next

                If arlRstInfo.Count > 0 Then
                    Dim arySUCC As New ArrayList
                    Dim da_regrst As New LISAPP.APP_R.RegFn
                    Dim iRet As Integer = 0

                    If PRG_CONST.BCCLS_MicorBio.Contains(sOBcNo.Substring(8, 2)) Then
                        iRet = da_regrst.RegServer(arlRstInfo, SmpInfo, arySUCC)
                    Else
                        iRet = da_regrst.RegServer(arlRstInfo, SmpInfo, arySUCC, False)
                    End If

                    If iRet < 1 Then
                        If sMsg <> "" Then
                            sMsg += ", "
                        End If
                        sMsg += sOBcNo
                    End If

                    fnDisplay_ResultOK(arlBcNotestcd, iRet)
                End If
            End With

            If sMsg <> "" Then
                MsgBox("검체번호 [" + sMsg + "]를 저장하지 못 했습니다.")
            Else
                MsgBox("작업을 완료했습니다.")
            End If

            Cursor.Current = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub


    Private Sub sbReg_Img(ByVal rsRstFlg As String)
        Dim sFn As String = "sbReg_Img"

        Try

            Dim sMsgErr As String = ""

            If MsgBox("이미지 파일을 저장 하시겠습니까?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return
            End If

            With spdResult_img
                For intRow As Integer = 1 To .MaxRows

                    Dim sBcNo As String = ""
                    Dim sTestcd As String = ""
                    Dim sFileNm As String = ""
                    Dim sRegNo As String = ""
                    Dim sPatNm As String = ""

                    .Row = intRow
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text

                    If sChk = "1" Then

                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : sTestcd = .Text
                        .Col = .GetColFromID("rstval") : sFileNm = .Text
                        .Col = .GetColFromID("regno") : sRegNo = .Text
                        .Col = .GetColFromID("patnm") : sPatNm = .Text

                        Dim alFileNm As New ArrayList

                        For ix As Integer = 0 To m_al_FileList.Count - 1
                            Dim sBuf() As String = m_al_FileList(ix).ToString.Split("|"c)

                            If sBcNo + "^" + sTestcd = sBuf(0) Then alFileNm.Add(sBuf(1))
                        Next


                        Dim iDisable As Integer = 0
                        Dim al_ChgRst As ArrayList = fnGet_Change_Rst(rsRstFlg, sBcNo, sTestcd, alFileNm, iDisable)

                        '오류 발생 시
                        If al_ChgRst Is Nothing Or iDisable < 0 Then
                            .Col = .GetColFromID("state") : .Text = "실패"
                            .ForeColor = Color.Red
                        Else

                            Dim si As New STU_SampleInfo

                            si.RegStep = rsRstFlg
                            si.BCNo = sBcNo
                            si.EqCd = ""
                            si.UsrID = USER_INFO.USRID
                            si.UsrIP = USER_INFO.LOCALIP
                            si.IntSeqNo = ""
                            si.Rack = ""
                            si.Pos = ""
                            si.EqBCNo = ""
                            si.SenderID = Me.Name

                            Dim al_ri As New ArrayList
                            Dim al_return As New ArrayList

                            For i As Integer = 1 To al_ChgRst.Count
                                al_ri.Add(al_ChgRst(i - 1))
                            Next

                            Dim regrst As New LISAPP.APP_R.RegFn

                            Dim iReturn As Integer = regrst.RegServer(al_ri, si, al_return, True)

                            If iReturn > 0 Then
                                .Col = .GetColFromID("state") : .Text = "완료"

                                If rsRstFlg = "3" Then


                                    If (New LISAPP.APP_R.AxRstFn).fnReg_IMAGE(sBcNo, sTestcd, alFileNm) Then
                                        Dim bRet As Boolean = fnSend_SCLImg_Info(sBcNo, sTestcd)
                                    Else
                                        .Col = .GetColFromID("state") : .Text = "인증오류"

                                        sMsgErr = "이미지 인증 오류가 발생했습니다." + vbCrLf + "인증오류 자료만 이미지검증 버튼을 실행해 주세요.!!"
                                    End If
                                    'If fnSaveImage(sBcNo, sTestcd, sPatNm) Then
                                    'Else
                                    '    .Col = .GetColFromID("state") : .Text = "인증오류"

                                    '    sMsgErr = "이미지 인증 오류가 발생했습니다." + vbCrLf + "인증오류 자료만 이미지검증 버튼을 실행해 주세요.!!"
                                    'End If
                                End If

                            Else
                                .Col = .GetColFromID("state") : .Text = "실패"
                                .ForeColor = Color.Red
                            End If

                            si = Nothing

                            Threading.Thread.Sleep(1000)

                        End If
                    End If
                Next
            End With

            If sMsgErr <> "" Then CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, sMsgErr)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
    Private Function fnSend_SCLImg_Info(ByVal rsBcno As String, ByVal rsTestcd As String) As Boolean
        Dim sFn As String = "fnSend_SCLImg_Info() As Boolean"

        Dim OleDbCn As OleDb.OleDbConnection
        Dim OleDbTrans As OleDb.OleDbTransaction
        Dim OleDbCmd As New OleDb.OleDbCommand

        OleDbCn = DBSERVER.DbOLE.GetDbConnection()
        OleDbTrans = OleDbCn.BeginTransaction()

        Try
            Dim strSqldoc As String = ""
            Dim dt As New DataTable
            Dim arr_param As ArrayList
            Dim strErrVal As String = ""

            strSqldoc += " update OCS_NMC..SCLIMAGE HBARCODE,HITEMCODE,HSAMPCODE,IMAGESEQ,RSTIMAGE,IMAGENAME" + vbCrLf
            strSqldoc += "   SET TRANSDATE = CONVERT(CHAR(8), GETDATE(), 112) + REPLACE(CONVERT(CHAR(8), GETDATE(), 8), ':','')" + vbCrLf
            strSqldoc += "       ,UDPTDATE = CONVERT(CHAR(8), GETDATE(), 112) + REPLACE(CONVERT(CHAR(8), GETDATE(), 8), ':','') " + vbCrLf
            ' strSqldoc += "       , " + vbCrLf
            strSqldoc += " " + vbCrLf
            strSqldoc += " " + vbCrLf
            strSqldoc += "  WHERE HOSPCODE IN ('022429')" + vbCrLf
            strSqldoc += "    AND HBARCODE  = ? " + vbCrLf
            strSqldoc += "    AND HITEMCODE = ? " + vbCrLf

            With OleDbCmd
                .Connection = OleDbCn
                .Transaction = OleDbTrans
                .CommandType = CommandType.Text

                .CommandText = strSqldoc

                .Parameters.Clear()

                .Parameters.Add("HBARCODE", OleDb.OleDbType.VarChar).Value = rsBcno
                .Parameters.Add("HITEMCODE", OleDb.OleDbType.VarChar).Value = rsTestcd
                '.Parameters.Add("SELECTFLAG", OleDb.OleDbType.VarChar).Value = "1"

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Function

    Private Sub fnDisplay_ResultOK(ByVal raList As ArrayList, ByVal riCnt As Integer)
        Dim sFn As String = "Sub fnDisplay_ResultOK(ArrayList, integer)"

        Try
            If raList.Count < 1 Then Exit Try

            For intIdx As Integer = 0 To raList.Count - 1
                Dim aryBuf() As String

                aryBuf = Split(raList.Item(intIdx).ToString, "|")
                For intRow As Integer = 1 To spdResult.MaxRows
                    Dim strBcNo As String = ""
                    Dim strtestcd As String = ""

                    With spdResult
                        .Row = intRow
                        .Col = .GetColFromID("bcno") : strBcNo = .Text
                        .Col = .GetColFromID("testcd") : strtestcd = .Text

                        If strBcNo = aryBuf(0) And strtestcd = aryBuf(1) Then
                            If riCnt > 0 Then
                                .Col = .GetColFromID("state") : .Text = "완료"
                            Else
                                .Col = .GetColFromID("state") : .Text = "실패"
                                .ForeColor = Color.Red
                            End If
                            Exit For
                        End If
                    End With
                Next
            Next

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Function fnGet_Change_Rst(ByVal rsRegStep As String, ByVal rsBcNo As String, ByVal rstestcd As String, ByVal r_al_FileNm As ArrayList, ByRef riDisable As Integer) As ArrayList
        Dim sFn As String = "fnGet_Change_Rst(integer, string, string, integer)"

        Dim al As New ArrayList
        Dim ri As STU_RstInfo

        Try
            '변경여부 조사 --> 변경된 결과를 ArrayList에 담기
            Dim iChange As Integer = 0
            Dim iRstFlag As Integer = 0

            riDisable = fnStSub_View(rsBcNo, rstestcd, r_al_FileNm, rsRegStep)

            ri = New STU_RstInfo

            ri.TestCd = rstestcd
            '일반검사
            ri.OrgRst = "{null}"

            Select Case rsRegStep
                Case "1" : ri.ChageRst = Me.txtStRstTxtR.Text
                Case "2" : ri.ChageRst = Me.txtStRstTxtM.Text
                Case "3" : ri.ChageRst = Me.txtStRstTxtF.Text
            End Select

            ri.RstCmt = ""

            'ri.RstRTF = Me.rtbStRst.get_SelRTF(True).Trim
            ri.RstRTF = Me.rtbStRst.get_SelRTF(True).Trim
            ri.RstTXT = Fn.SubstringH(Me.rtbStRst.get_SelText(True).Trim, 0, 4000)
            al.Add(ri)
            ri = Nothing

            Return al

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return New ArrayList
        Finally
            al = Nothing
        End Try

    End Function

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents cboExLab As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents ofdExLab As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cbxCmtDel As System.Windows.Forms.CheckBox
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents rtbStRst As AxAckRichTextBox.AxAckRichTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdResult As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents AxvaSpread2 As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnPath As System.Windows.Forms.Button
    Friend WithEvents fbdPath As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lstImgFile As System.Windows.Forms.ListBox
    Friend WithEvents spdResult_img As AxFPSpreadADO.AxfpSpread
    Friend WithEvents tabImage As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tabExLab As System.Windows.Forms.TabControl
    Friend WithEvents tabExcel As System.Windows.Forms.TabPage
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR07_SCL))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker13 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems7 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker14 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.cboDate = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboState = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboPartSlip = New System.Windows.Forms.ComboBox()
        Me.cboExLab = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.cbxCmtDel = New System.Windows.Forms.CheckBox()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.ofdExLab = New System.Windows.Forms.OpenFileDialog()
        Me.rtbStRst = New AxAckRichTextBox.AxAckRichTextBox()
        Me.tabExLab = New System.Windows.Forms.TabControl()
        Me.tabExcel = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkRst = New System.Windows.Forms.CheckBox()
        Me.spdResult = New AxFPSpreadADO.AxfpSpread()
        Me.spdTest = New AxFPSpreadADO.AxfpSpread()
        Me.tabImage = New System.Windows.Forms.TabPage()
        Me.txtStRstTxtF = New System.Windows.Forms.TextBox()
        Me.txtStRstTxtM = New System.Windows.Forms.TextBox()
        Me.txtStRstTxtR = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstImgFile = New System.Windows.Forms.ListBox()
        Me.btnPath = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkImg = New System.Windows.Forms.CheckBox()
        Me.spdResult_img = New AxFPSpreadADO.AxfpSpread()
        Me.AxvaSpread2 = New AxFPSpreadADO.AxfpSpread()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.fbdPath = New System.Windows.Forms.FolderBrowserDialog()
        Me.rtbSt = New AxAckRichTextBox.AxAckRichTextBox()
        Me.picBuf = New System.Windows.Forms.PictureBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnImage = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnFN = New CButtonLib.CButton()
        Me.btnQuery = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.GroupBox11.SuspendLayout()
        Me.tabExLab.SuspendLayout()
        Me.tabExcel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabImage.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.spdResult_img, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxvaSpread2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBuf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.cboDate)
        Me.GroupBox11.Controls.Add(Me.Label5)
        Me.GroupBox11.Controls.Add(Me.dtpDateE)
        Me.GroupBox11.Controls.Add(Me.dtpDateS)
        Me.GroupBox11.Controls.Add(Me.Label3)
        Me.GroupBox11.Controls.Add(Me.cboState)
        Me.GroupBox11.Controls.Add(Me.Label2)
        Me.GroupBox11.Controls.Add(Me.cboPartSlip)
        Me.GroupBox11.Controls.Add(Me.cboExLab)
        Me.GroupBox11.Controls.Add(Me.Label8)
        Me.GroupBox11.Controls.Add(Me.Label39)
        Me.GroupBox11.Controls.Add(Me.cbxCmtDel)
        Me.GroupBox11.Location = New System.Drawing.Point(6, -3)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(707, 60)
        Me.GroupBox11.TabIndex = 79
        Me.GroupBox11.TabStop = False
        '
        'cboDate
        '
        Me.cboDate.AutoCompleteCustomSource.AddRange(New String() {"[0] 전체", "[1] 접수일자", "[2] 보고일자"})
        Me.cboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDate.Items.AddRange(New Object() {"[0] 전체", "[1] 접수일자", "[2] 보고일자"})
        Me.cboDate.Location = New System.Drawing.Point(331, 35)
        Me.cboDate.Name = "cboDate"
        Me.cboDate.Size = New System.Drawing.Size(164, 20)
        Me.cboDate.TabIndex = 134
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(584, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 12)
        Me.Label5.TabIndex = 133
        Me.Label5.Text = "~"
        '
        'dtpDateE
        '
        Me.dtpDateE.CustomFormat = "yyyy-MM-dd"
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateE.Location = New System.Drawing.Point(600, 35)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(84, 21)
        Me.dtpDateE.TabIndex = 132
        '
        'dtpDateS
        '
        Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateS.Location = New System.Drawing.Point(496, 35)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(85, 21)
        Me.dtpDateS.TabIndex = 131
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(258, 35)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 20)
        Me.Label3.TabIndex = 127
        Me.Label3.Text = "일자구분"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboState
        '
        Me.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboState.Items.AddRange(New Object() {"[1] 미생물 중간결과포함", "[2] 검사완료만 "})
        Me.cboState.Location = New System.Drawing.Point(78, 36)
        Me.cboState.Margin = New System.Windows.Forms.Padding(1)
        Me.cboState.Name = "cboState"
        Me.cboState.Size = New System.Drawing.Size(164, 20)
        Me.cboState.TabIndex = 126
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(5, 36)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 20)
        Me.Label2.TabIndex = 125
        Me.Label2.Text = "보고상태"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboPartSlip
        '
        Me.cboPartSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartSlip.Items.AddRange(New Object() {"녹십자", "네오딘", "랩지노믹스"})
        Me.cboPartSlip.Location = New System.Drawing.Point(78, 13)
        Me.cboPartSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.cboPartSlip.Name = "cboPartSlip"
        Me.cboPartSlip.Size = New System.Drawing.Size(164, 20)
        Me.cboPartSlip.TabIndex = 124
        '
        'cboExLab
        '
        Me.cboExLab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExLab.Items.AddRange(New Object() {"녹십자", "네오딘", "랩지노믹스"})
        Me.cboExLab.Location = New System.Drawing.Point(331, 13)
        Me.cboExLab.Name = "cboExLab"
        Me.cboExLab.Size = New System.Drawing.Size(164, 20)
        Me.cboExLab.TabIndex = 123
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(5, 13)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 20)
        Me.Label8.TabIndex = 123
        Me.Label8.Text = "검사분야"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(258, 13)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(72, 20)
        Me.Label39.TabIndex = 122
        Me.Label39.Text = "위탁기관명"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxCmtDel
        '
        Me.cbxCmtDel.Checked = True
        Me.cbxCmtDel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbxCmtDel.Enabled = False
        Me.cbxCmtDel.Location = New System.Drawing.Point(510, 13)
        Me.cbxCmtDel.Name = "cbxCmtDel"
        Me.cbxCmtDel.Size = New System.Drawing.Size(175, 21)
        Me.cbxCmtDel.TabIndex = 106
        Me.cbxCmtDel.Text = "결과등록시 기존 소견 삭제"
        Me.cbxCmtDel.Visible = False
        '
        'txtLog
        '
        Me.txtLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLog.Location = New System.Drawing.Point(8, 570)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.Size = New System.Drawing.Size(1078, 24)
        Me.txtLog.TabIndex = 104
        '
        'rtbStRst
        '
        Me.rtbStRst.Location = New System.Drawing.Point(188, 5)
        Me.rtbStRst.Name = "rtbStRst"
        Me.rtbStRst.Size = New System.Drawing.Size(177, 26)
        Me.rtbStRst.TabIndex = 110
        Me.rtbStRst.Visible = False
        '
        'tabExLab
        '
        Me.tabExLab.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabExLab.Controls.Add(Me.tabExcel)
        Me.tabExLab.Controls.Add(Me.tabImage)
        Me.tabExLab.Location = New System.Drawing.Point(7, 59)
        Me.tabExLab.Name = "tabExLab"
        Me.tabExLab.Padding = New System.Drawing.Point(4, 3)
        Me.tabExLab.SelectedIndex = 0
        Me.tabExLab.Size = New System.Drawing.Size(1079, 509)
        Me.tabExLab.TabIndex = 111
        '
        'tabExcel
        '
        Me.tabExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabExcel.Controls.Add(Me.Panel1)
        Me.tabExcel.Location = New System.Drawing.Point(4, 22)
        Me.tabExcel.Name = "tabExcel"
        Me.tabExcel.Size = New System.Drawing.Size(1071, 483)
        Me.tabExcel.TabIndex = 0
        Me.tabExcel.Text = " [ TEXT 결과저장 ] "
        Me.tabExcel.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.chkRst)
        Me.Panel1.Controls.Add(Me.spdResult)
        Me.Panel1.Controls.Add(Me.spdTest)
        Me.Panel1.Location = New System.Drawing.Point(6, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1057, 473)
        Me.Panel1.TabIndex = 102
        '
        'chkRst
        '
        Me.chkRst.AutoSize = True
        Me.chkRst.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkRst.Checked = True
        Me.chkRst.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRst.Location = New System.Drawing.Point(38, 11)
        Me.chkRst.Name = "chkRst"
        Me.chkRst.Size = New System.Drawing.Size(15, 14)
        Me.chkRst.TabIndex = 13
        Me.chkRst.UseVisualStyleBackColor = False
        '
        'spdResult
        '
        Me.spdResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdResult.DataSource = Nothing
        Me.spdResult.Location = New System.Drawing.Point(0, 0)
        Me.spdResult.Name = "spdResult"
        Me.spdResult.OcxState = CType(resources.GetObject("spdResult.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdResult.Size = New System.Drawing.Size(1053, 469)
        Me.spdResult.TabIndex = 10
        '
        'spdTest
        '
        Me.spdTest.DataSource = Nothing
        Me.spdTest.Location = New System.Drawing.Point(40, 45)
        Me.spdTest.Name = "spdTest"
        Me.spdTest.OcxState = CType(resources.GetObject("spdTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTest.Size = New System.Drawing.Size(436, 185)
        Me.spdTest.TabIndex = 12
        Me.spdTest.Visible = False
        '
        'tabImage
        '
        Me.tabImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tabImage.Controls.Add(Me.txtStRstTxtF)
        Me.tabImage.Controls.Add(Me.txtStRstTxtM)
        Me.tabImage.Controls.Add(Me.txtStRstTxtR)
        Me.tabImage.Controls.Add(Me.Label1)
        Me.tabImage.Controls.Add(Me.lstImgFile)
        Me.tabImage.Controls.Add(Me.btnPath)
        Me.tabImage.Controls.Add(Me.Panel2)
        Me.tabImage.Controls.Add(Me.txtPath)
        Me.tabImage.Location = New System.Drawing.Point(4, 22)
        Me.tabImage.Name = "tabImage"
        Me.tabImage.Size = New System.Drawing.Size(1071, 483)
        Me.tabImage.TabIndex = 1
        Me.tabImage.Text = " [ IMAGE 결과저장 ] "
        Me.tabImage.UseVisualStyleBackColor = True
        '
        'txtStRstTxtF
        '
        Me.txtStRstTxtF.Location = New System.Drawing.Point(994, 6)
        Me.txtStRstTxtF.MaxLength = 200
        Me.txtStRstTxtF.Name = "txtStRstTxtF"
        Me.txtStRstTxtF.Size = New System.Drawing.Size(68, 21)
        Me.txtStRstTxtF.TabIndex = 210
        Me.txtStRstTxtF.Visible = False
        '
        'txtStRstTxtM
        '
        Me.txtStRstTxtM.Location = New System.Drawing.Point(918, 6)
        Me.txtStRstTxtM.MaxLength = 200
        Me.txtStRstTxtM.Name = "txtStRstTxtM"
        Me.txtStRstTxtM.Size = New System.Drawing.Size(68, 21)
        Me.txtStRstTxtM.TabIndex = 209
        Me.txtStRstTxtM.Visible = False
        '
        'txtStRstTxtR
        '
        Me.txtStRstTxtR.Location = New System.Drawing.Point(842, 6)
        Me.txtStRstTxtR.MaxLength = 200
        Me.txtStRstTxtR.Name = "txtStRstTxtR"
        Me.txtStRstTxtR.Size = New System.Drawing.Size(68, 21)
        Me.txtStRstTxtR.TabIndex = 208
        Me.txtStRstTxtR.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.GhostWhite
        Me.Label1.Location = New System.Drawing.Point(9, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(314, 28)
        Me.Label1.TabIndex = 123
        Me.Label1.Text = " ** Image File Path..."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstImgFile
        '
        Me.lstImgFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstImgFile.ItemHeight = 12
        Me.lstImgFile.Location = New System.Drawing.Point(326, 6)
        Me.lstImgFile.Name = "lstImgFile"
        Me.lstImgFile.Size = New System.Drawing.Size(738, 52)
        Me.lstImgFile.TabIndex = 106
        Me.lstImgFile.Visible = False
        '
        'btnPath
        '
        Me.btnPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPath.Location = New System.Drawing.Point(290, 37)
        Me.btnPath.Name = "btnPath"
        Me.btnPath.Size = New System.Drawing.Size(33, 21)
        Me.btnPath.TabIndex = 104
        Me.btnPath.Text = "..."
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.chkImg)
        Me.Panel2.Controls.Add(Me.spdResult_img)
        Me.Panel2.Controls.Add(Me.AxvaSpread2)
        Me.Panel2.Location = New System.Drawing.Point(7, 61)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1057, 434)
        Me.Panel2.TabIndex = 103
        '
        'chkImg
        '
        Me.chkImg.AutoSize = True
        Me.chkImg.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkImg.Location = New System.Drawing.Point(36, 14)
        Me.chkImg.Name = "chkImg"
        Me.chkImg.Size = New System.Drawing.Size(15, 14)
        Me.chkImg.TabIndex = 14
        Me.chkImg.UseVisualStyleBackColor = False
        '
        'spdResult_img
        '
        Me.spdResult_img.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdResult_img.DataSource = Nothing
        Me.spdResult_img.Location = New System.Drawing.Point(0, 0)
        Me.spdResult_img.Name = "spdResult_img"
        Me.spdResult_img.OcxState = CType(resources.GetObject("spdResult_img.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdResult_img.Size = New System.Drawing.Size(1053, 430)
        Me.spdResult_img.TabIndex = 10
        '
        'AxvaSpread2
        '
        Me.AxvaSpread2.DataSource = Nothing
        Me.AxvaSpread2.Location = New System.Drawing.Point(523, 234)
        Me.AxvaSpread2.Name = "AxvaSpread2"
        Me.AxvaSpread2.OcxState = CType(resources.GetObject("AxvaSpread2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxvaSpread2.Size = New System.Drawing.Size(436, 185)
        Me.AxvaSpread2.TabIndex = 12
        Me.AxvaSpread2.Visible = False
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(9, 37)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(280, 21)
        Me.txtPath.TabIndex = 1
        Me.txtPath.Text = "C:\수탁검사\image"
        '
        'rtbSt
        '
        Me.rtbSt.Location = New System.Drawing.Point(5, 4)
        Me.rtbSt.Name = "rtbSt"
        Me.rtbSt.Size = New System.Drawing.Size(177, 26)
        Me.rtbSt.TabIndex = 158
        Me.rtbSt.Visible = False
        '
        'picBuf
        '
        Me.picBuf.BackColor = System.Drawing.Color.White
        Me.picBuf.Location = New System.Drawing.Point(383, 5)
        Me.picBuf.Name = "picBuf"
        Me.picBuf.Size = New System.Drawing.Size(92, 29)
        Me.picBuf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picBuf.TabIndex = 159
        Me.picBuf.TabStop = False
        Me.picBuf.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnImage)
        Me.Panel3.Controls.Add(Me.btnExcel)
        Me.Panel3.Controls.Add(Me.picBuf)
        Me.Panel3.Controls.Add(Me.btnReg)
        Me.Panel3.Controls.Add(Me.rtbSt)
        Me.Panel3.Controls.Add(Me.rtbStRst)
        Me.Panel3.Controls.Add(Me.btnFN)
        Me.Panel3.Controls.Add(Me.btnQuery)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.btnExit)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 595)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1094, 34)
        Me.Panel3.TabIndex = 160
        '
        'btnImage
        '
        Me.btnImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnImage.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnImage.ColorFillBlend = CBlendItems1
        Me.btnImage.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnImage.Corners.All = CType(6, Short)
        Me.btnImage.Corners.LowerLeft = CType(6, Short)
        Me.btnImage.Corners.LowerRight = CType(6, Short)
        Me.btnImage.Corners.UpperLeft = CType(6, Short)
        Me.btnImage.Corners.UpperRight = CType(6, Short)
        Me.btnImage.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnImage.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnImage.FocalPoints.CenterPtX = 0.5!
        Me.btnImage.FocalPoints.CenterPtY = 0.08!
        Me.btnImage.FocalPoints.FocusPtX = 0.0!
        Me.btnImage.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnImage.FocusPtTracker = DesignerRectTracker2
        Me.btnImage.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnImage.ForeColor = System.Drawing.Color.White
        Me.btnImage.Image = Nothing
        Me.btnImage.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnImage.ImageIndex = 0
        Me.btnImage.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnImage.Location = New System.Drawing.Point(6, 5)
        Me.btnImage.Name = "btnImage"
        Me.btnImage.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnImage.SideImage = Nothing
        Me.btnImage.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnImage.Size = New System.Drawing.Size(96, 25)
        Me.btnImage.TabIndex = 222
        Me.btnImage.Text = "이미지검증"
        Me.btnImage.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnImage.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems2
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.4672897!
        Me.btnExcel.FocalPoints.CenterPtY = 0.2!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker4
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(480, 4)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(107, 25)
        Me.btnExcel.TabIndex = 195
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems3
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.5!
        Me.btnReg.FocalPoints.CenterPtY = 0.0!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker6
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(793, 4)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(96, 25)
        Me.btnReg.TabIndex = 193
        Me.btnReg.Text = "결과저장(F9)"
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnFN
        '
        Me.btnFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnFN.ColorFillBlend = CBlendItems4
        Me.btnFN.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnFN.Corners.All = CType(6, Short)
        Me.btnFN.Corners.LowerLeft = CType(6, Short)
        Me.btnFN.Corners.LowerRight = CType(6, Short)
        Me.btnFN.Corners.UpperLeft = CType(6, Short)
        Me.btnFN.Corners.UpperRight = CType(6, Short)
        Me.btnFN.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnFN.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnFN.FocalPoints.CenterPtX = 0.5!
        Me.btnFN.FocalPoints.CenterPtY = 0.0!
        Me.btnFN.FocalPoints.FocusPtX = 0.0!
        Me.btnFN.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.FocusPtTracker = DesignerRectTracker8
        Me.btnFN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnFN.ForeColor = System.Drawing.Color.White
        Me.btnFN.Image = Nothing
        Me.btnFN.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFN.ImageIndex = 0
        Me.btnFN.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnFN.Location = New System.Drawing.Point(696, 4)
        Me.btnFN.Name = "btnFN"
        Me.btnFN.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnFN.SideImage = Nothing
        Me.btnFN.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnFN.Size = New System.Drawing.Size(96, 25)
        Me.btnFN.TabIndex = 194
        Me.btnFN.Text = "결과검증(F12)"
        Me.btnFN.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnFN.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems5
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.4672897!
        Me.btnQuery.FocalPoints.CenterPtY = 0.16!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker10
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(588, 4)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(107, 25)
        Me.btnQuery.TabIndex = 189
        Me.btnQuery.Text = "불러오기(F3)"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems6
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4672897!
        Me.btnClear.FocalPoints.CenterPtY = 0.16!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker12
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(890, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 187
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker13.IsActive = False
        DesignerRectTracker13.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker13.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker13
        CBlendItems7.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems7.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems7
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5164835!
        Me.btnExit.FocalPoints.CenterPtY = 0.8!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker14.IsActive = False
        DesignerRectTracker14.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker14.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker14
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(998, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(91, 25)
        Me.btnExit.TabIndex = 188
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGR07_SCL
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1094, 629)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.tabExLab)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.GroupBox11)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGR07_SCL"
        Me.Text = "위탁검사 결과저장 및 보고 (SCL)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.tabExLab.ResumeLayout(False)
        Me.tabExcel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabImage.ResumeLayout(False)
        Me.tabImage.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.spdResult_img, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxvaSpread2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBuf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub sbDiplay_Init()
        With spdResult
            .MaxRows = 0
        End With

        '-- 2007/10/26 ssh
        With spdResult_img
            .MaxRows = 0
        End With

        Me.txtLog.Text = ""

    End Sub

    Private Sub FGR07_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim sFn As String = ""

        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGR07_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F3
                btnQuery_Click(Nothing, Nothing)
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F9
                btnReg_Click(Nothing, Nothing)
            Case Keys.F12
                btnFN_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGR07_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sbDiplay_Init()
        Me.dtpDateS.Value = CDate(Format(DateAdd(DateInterval.Day, -1, Now), "yyyy-MM-dd").ToString)
        sbDisplay_PartSlip()
        sbDisplay_ExLab()

        If Me.cboExLab.Items.Count > 0 Then Me.cboExLab.SelectedIndex = 0

        Me.cboState.SelectedIndex = 1 '보고상태
        Me.cboDate.SelectedIndex = 2 '일자구분

        Me.rtbStRst.Visible = False

        msEmrPrintName = (New COMMON.CommPrint.PRT_Printer("EMRIMG")).GetInfo.PRTNM

    End Sub

    Private Sub sbDisplay_ExLab()
        Dim sFn As String = "Sub sbDisplay_ExLab()"

        Try

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_ExLab_List()

            Me.cboExLab.Items.Clear()
            Me.cboExLab.Items.Add("[  ] 전체")
            For ix = 0 To dt.Rows.Count - 1
                Me.cboExLab.Items.Add("[" + dt.Rows(ix).Item("exlabcd").ToString.Trim + "] " + dt.Rows(ix).Item("exlabnmd").ToString.Trim)
            Next

            If Me.cboExLab.Items.Count > 0 Then Me.cboExLab.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_PartSlip()
        Dim sFn As String = "Sub sbDisplay_PartSlip()"

        Try

            Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_PartSlip_ExLab()

            Me.cboPartSlip.Items.Clear()
            Me.cboPartSlip.Items.Add("[  ] 전체")

            For ix = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("partslip").ToString.Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString.Trim)
            Next

            If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub spdExLab_ComboSelChange(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ComboSelChangeEvent)

        Dim vTmp As New Object
        Dim sTmpData() As String
        Dim iCol As Integer
        Dim iRow As Integer

        With spdResult
            If .ActiveCol = .GetColFromID("검사항목명") Then

                iCol = .ActiveCol
                iRow = .ActiveRow

                Call .GetText(iCol, iRow, vTmp)

                If vTmp.ToString.Trim() = "" Then

                    Call .SetText(.GetColFromID("상태"), iRow, "")
                    Call .SetText(.GetColFromID("보고구분"), iRow, "")
                    Call .SetText(.GetColFromID("검사항목코드"), iRow, "")
                    Call .SetText(.GetColFromID("결과FLAG"), iRow, "")

                Else
                    sTmpData = Split(Convert.ToChar(vTmp), Chr(124))

                    Call .SetText(.GetColFromID("상태"), iRow, sTmpData(4))
                    Call .SetText(.GetColFromID("보고구분"), iRow, sTmpData(3))
                    Call .SetText(.GetColFromID("검사항목코드"), iRow, sTmpData(1))
                    Call .SetText(.GetColFromID("결과FLAG"), iRow, sTmpData(2))
                End If
            End If
        End With
    End Sub


    Private Function sbGet_SCL_Img2() As Byte()
        Dim sFn As String = "Private Sub sbGet_SCL_Img()"


        Dim OleDbCn As OleDb.OleDbConnection
        Dim OleDbTrans As OleDb.OleDbTransaction
        Dim OleDbCmd As New OleDb.OleDbCommand

        OleDbCn = DBSERVER.DbOLE.GetDbConnection()
        OleDbTrans = OleDbCn.BeginTransaction()

        Try
            Dim sSdate As String = Me.dtpDateS.Text.Replace("-", "").Replace(" ", "")
            Dim sEdate As String = Me.dtpDateE.Text.Replace("-", "").Replace(" ", "")

            Dim strSqldoc As String = ""
            Dim dt As New DataTable
            Dim arr_param As ArrayList
            Dim strErrVal As String = ""

            strSqldoc += " SELECT HBARCODE,HITEMCODE,HSAMPCODE,IMAGESEQ,RSTIMAGE,IMAGENAME" + vbCrLf
            strSqldoc += "   FROM OCS_NMC..SCLIMAGE " + vbCrLf
            strSqldoc += "  WHERE HOSPCODE IN ('022429')" + vbCrLf
            strSqldoc += "    AND ORDDATE BETWEEN ? AND ?" + vbCrLf
            strSqldoc += "    AND TRANSDATE = ''" + vbCrLf

            With OleDbCmd
                .Connection = OleDbCn
                .Transaction = OleDbTrans
                .CommandType = CommandType.Text

                .CommandText = strSqldoc

                .Parameters.Clear()

                .Parameters.Add("DATEF", OleDb.OleDbType.VarChar).Value = sSdate
                .Parameters.Add("DATEE", OleDb.OleDbType.VarChar).Value = sEdate
                '.Parameters.Add("SELECTFLAG", OleDb.OleDbType.VarChar).Value = "1"

            End With

            Dim a_btReturn() As Byte

            Dim dbDr As OleDb.OleDbDataReader = OleDbCmd.ExecuteReader(CommandBehavior.SequentialAccess)

            Do While dbDr.Read()

                Dim iStartIndex As Integer = 0
                Dim lngReturn As Long = 0

                Dim iBufferSize As Integer = 0

                iBufferSize = Convert.ToInt32(dbDr.GetValue(0).ToString)

                iBufferSize = Convert.ToInt32(dbDr.Item("RSTIMAGE").ToString)


                Dim a_btBuffer(iBufferSize - 1) As Byte
                ReDim a_btBuffer(iBufferSize - 1)

                iStartIndex = 0
                lngReturn = dbDr.GetBytes(1, iStartIndex, a_btBuffer, 0, iBufferSize)

                Do While lngReturn = iBufferSize
                    fnCopyToBytes(a_btBuffer, a_btReturn)


                    ReDim a_btBuffer(iBufferSize - 1)

                    iStartIndex += iBufferSize
                    lngReturn = dbDr.GetBytes(1, iStartIndex, a_btReturn, 0, iBufferSize)
                Loop
            Loop

            dbDr.Close()

            Return a_btReturn

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
            OleDbTrans.Rollback()

        End Try


    End Function


    Private Sub sbGet_SCL_Img()
        Dim sFn As String = "Private Sub sbGet_SCL_Img()"


        Dim OleDbCn As OleDb.OleDbConnection
        Dim OleDbTrans As OleDb.OleDbTransaction
        Dim OleDbCmd As New OleDb.OleDbCommand

        OleDbCn = DBSERVER.DbOLE.GetDbConnection()
        OleDbTrans = OleDbCn.BeginTransaction()

        Try
            Dim sSdate As String = Me.dtpDateS.Text.Replace("-", "").Replace(" ", "")
            Dim sEdate As String = Me.dtpDateE.Text.Replace("-", "").Replace(" ", "")

            Dim strSqldoc As String = ""
            Dim dt As New DataTable
            Dim arr_param As ArrayList
            Dim strErrVal As String = ""



            'strSqldoc = ""
            'strSqldoc += "select *  "
            'strSqldoc += "from sclimage" + vbCrLf
            'strSqldoc += "" + vbCrLf
            'strSqldoc += "" + vbCrLf
            strSqldoc += "SP_SCLIMAGE_SELECT" + vbCrLf

            With OleDbCmd
                .Connection = OleDbCn
                .Transaction = OleDbTrans
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSqldoc

                .Parameters.Clear()

                .Parameters.Add("DATEF", OleDb.OleDbType.VarChar).Value = sSdate
                .Parameters.Add("DATEE", OleDb.OleDbType.VarChar).Value = sEdate
                .Parameters.Add("SELECTFLAG", OleDb.OleDbType.VarChar).Value = "1"

                '.Parameters.Add("RETURN_VALUE", OleDb.OleDbType.VarChar, 4000)
                '.Parameters("RETURN_VALUE").Direction = ParameterDirection.InputOutput
                '.Parameters("RETURN_VALUE").Value = strErrVal

                '.ExecuteNonQuery()

                'strErrVal = .Parameters(3).Value.ToString

                'Dim obj As Object = .ExecuteNonQuery  '.ExecuteScalar()

                Dim objDAdapter As New OleDb.OleDbDataAdapter(OleDbCmd)
                objDAdapter.Fill(dt)

                If dt.Rows.Count > 0 Then
                    ' MsgBox("접속성공")

                    For ix As Integer = 0 To dt.Rows.Count - 1
                        'Dim a_btBuf As Byte() = dt.Rows(ix).Item("")

                        Dim obj As Object = dt.Rows(ix).Item("RSTIMAGE").GetType

                        Dim a_btReturn() As Byte = CType(obj, Byte())

                        Dim iStartIndex As Integer = 0
                        Dim lngReturn As Long = 0

                        Dim iBufferSize As Integer = 0

                        iBufferSize = dt.Rows(ix).Item("RSTIMAGE").ToString.Length

                        Dim a_btBuffer(iBufferSize - 1) As Byte
                        ReDim a_btBuffer(iBufferSize - 1)

                        iStartIndex = 0


                        '    Do While lngReturn = iBufferSize
                        '        fnCopyToBytes(a_btBuffer, a_btReturn)


                        '        ReDim a_btBuffer(iBufferSize - 1)

                        '        iStartIndex += iBufferSize
                        '        lngReturn = dbDr.GetBytes(1, iStartIndex, a_btReturn, 0, iBufferSize)
                        '    Loop
                        'Loop

                        'Dim dbDr As OleDb.OleDbDataReader = OleDbCmd.ExecuteReader(CommandBehavior.SequentialAccess)

                        'Do While dbDr.Read()

                        '    Dim iStartIndex As Integer = 0
                        '    Dim lngReturn As Long = 0

                        '    Dim iBufferSize As Integer = 0

                        '    iBufferSize = Convert.ToInt32(dbDr.GetValue(0).ToString)

                        '    Dim a_btBuffer(iBufferSize - 1) As Byte
                        '    ReDim a_btBuffer(iBufferSize - 1)

                        '    iStartIndex = 0
                        '    lngReturn = dbDr.GetBytes(1, iStartIndex, a_btBuffer, 0, iBufferSize)

                        '    Do While lngReturn = iBufferSize
                        '        fnCopyToBytes(a_btBuffer, a_btReturn)


                        '        ReDim a_btBuffer(iBufferSize - 1)

                        '        iStartIndex += iBufferSize
                        '        lngReturn = dbDr.GetBytes(1, iStartIndex, a_btReturn, 0, iBufferSize)
                        '    Loop
                        'Loop

                        'dbDr.Close()

                    Next
                End If

            End With

            If strErrVal <> "" Then
                MsgBox("접속성공")
                MsgBox(strErrVal)

            End If

            If strErrVal.StartsWith("00") Or strErrVal.IndexOf("no data") > 0 Then
                OleDbTrans.Commit()
                Return
            Else
                OleDbTrans.Rollback()
                Return
            End If

            'dt = DBSERVER.DbProvider.DbExecuteQuery(strSqldoc, arr_param, False)

            'If dt.Rows.Count > 0 Then
            '    MsgBox("접속성공")
            'Else
            '    MsgBox("접속실패")
            'End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
            OleDbTrans.Rollback()
            Return
        End Try
    End Sub

    Private Function fnGet_SCL_Img3() As Integer
        Dim sFn As String = "Private Sub sbGet_SCL_Img3()"


        Dim OleDbCn As OleDb.OleDbConnection
        Dim OleDbTrans As OleDb.OleDbTransaction
        Dim OleDbCmd As New OleDb.OleDbCommand

        OleDbCn = DBSERVER.DbOLE.GetDbConnection()
        OleDbTrans = OleDbCn.BeginTransaction()

        Try
            Dim sSdate As String = Me.dtpDateS.Text.Replace("-", "").Replace(" ", "")
            Dim sEdate As String = Me.dtpDateE.Text.Replace("-", "").Replace(" ", "")
            Dim sDategbn As String = Ctrl.Get_Code(Me.cboDate)
            Dim strSqldoc As String = ""
            Dim dt As New DataTable
            Dim arr_param As ArrayList
            Dim strErrVal As String = ""

            strSqldoc += "SP_SCLIMAGE_SELECT" + vbCrLf

            With OleDbCmd
                .Connection = OleDbCn
                .Transaction = OleDbTrans
                .CommandType = CommandType.StoredProcedure
                .CommandText = strSqldoc

                .Parameters.Clear()

                .Parameters.Add("DATEF", OleDb.OleDbType.VarChar).Value = sSdate
                .Parameters.Add("DATEE", OleDb.OleDbType.VarChar).Value = sEdate
                .Parameters.Add("SELECTFLAG", OleDb.OleDbType.VarChar).Value = sDategbn

                Dim objDAdapter As New OleDb.OleDbDataAdapter(OleDbCmd)
                objDAdapter.Fill(dt)

                Dim sDir As String = "C:\수탁검사\image\" ' Application.StartupPath + "\Image\"
                If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

                Dim sOldFiles() As String = IO.Directory.GetFiles(sDir)

                If sOldFiles.Length > 0 Then
                    For ix As Integer = 0 To sOldFiles.Length - 1
                        IO.File.Delete(sOldFiles(ix))
                    Next

                End If

                If dt.Rows.Count > 0 Then

                    For ix As Integer = 0 To dt.Rows.Count - 1

                        Dim obj As Object = dt.Rows(ix).Item("RSTIMAGE")

                        Dim a_btReturn() As Byte = CType(obj, Byte())

                        Dim sFileNm = sDir + dt.Rows(ix).Item("IMAGENAME").ToString + ".jpg"

                        Dim fs As IO.FileStream

                        If a_btReturn IsNot Nothing Then

                            If IO.File.Exists(sFileNm) Then
                                Try
                                    Threading.Thread.Sleep(100)
                                    IO.File.Delete(sFileNm)
                                Catch ex As Exception
                                    'Me.txtFileNm.Text = sFileNm

                                    Dim bmpTmp As Bitmap = New Bitmap(sFileNm)

                                    Me.picBuf.Image = CType(bmpTmp, Image)
                                    Return dt.Rows.Count
                                End Try
                            End If

                            fs = New IO.FileStream(sFileNm, IO.FileMode.Create, FileAccess.Write)

                        Else
                            Me.picBuf.Image = Nothing

                            Return dt.Rows.Count
                        End If

                        Dim bw As IO.BinaryWriter = New IO.BinaryWriter(fs)

                        bw.Write(a_btReturn)
                        bw.Flush()

                        bw.Close()
                        fs.Close()

                        'Me.txtFileNm.Text = sFileNm

                        'Dim bmpBuf As Bitmap = New Bitmap(sFileNm)

                        'Me.picBuf.Image = CType(bmpBuf, Image)

                        'bmpBuf = Nothing
                        'fs = Nothing


                    Next

                End If


            End With

            OleDbTrans.Commit()
            Return dt.Rows.Count

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
            OleDbTrans.Rollback()
            Return 0
        End Try
    End Function
    Private Shared Function fnCopyToBytes(ByVal r_a_btFrom As Byte(), ByRef r_a_btTo As Byte()) As Boolean

        Try
            Dim iIndexDest As Integer = 0
            Dim iLength As Integer = 0

            If r_a_btTo Is Nothing Then
                iIndexDest = 0
            Else
                iIndexDest = r_a_btTo.Length
            End If

            iLength = r_a_btFrom.Length

            ReDim Preserve r_a_btTo(iIndexDest + iLength - 1)

            Array.ConstrainedCopy(r_a_btFrom, 0, r_a_btTo, iIndexDest, iLength)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Function
    Private Sub sbOpenImg_SCL()

        m_al_FileList.Clear()

        'sbGet_SCL_Img()
        Dim iret As Integer = fnGet_SCL_Img3()

        If iret < 1 Then
            MsgBox("조회된 이미지 결과가 없습니다.")
        End If

        Me.txtPath.Text = "C:\수탁검사\image\" ' Application.StartupPath + "\Image\"
        'If Dir(Me.txtPath.Text.Trim, FileAttribute.Directory) = "" Then MkDir(Me.txtPath.Text.Trim)

        Dim sFiles As String()

        sFiles = Directory.GetFileSystemEntries(txtPath.Text)

        For intix1 As Integer = 0 To sFiles.Length - 1
            If sFiles(intix1).ToLower.IndexOf(".jpg") > 0 Then
                chkImg.Checked = True

                Dim sFile As String = sFiles(intix1)
                Dim sBuf() As String

                If sFile <> "" Then

                    sBuf = Split(sFile, "\")
                    Dim sTmp As String = sBuf(sBuf.Length - 1)
                    sTmp = sTmp.Replace(".jpg", "")

                    Erase sBuf

                    sBuf = Split(sTmp, "-")

                    If sBuf.Length > 3 Then
                        Dim sRegNo As String = sBuf(0)
                        Dim sPatnm As String = sBuf(1)
                        Dim sBcNo As String = sBuf(2)
                        Dim sTestCd As String = sBuf(3)

                        m_al_FileList.Add(sBcNo + "^" + sTestCd + "|" + sFile)

                        With spdResult_img
                            Dim bFind As Boolean = False
                            Dim iRow As Integer = 0

                            For ix As Integer = 1 To .MaxRows
                                Dim sTmp1 As String = ""
                                Dim sTmp2 As String = ""

                                .Row = ix
                                .Col = .GetColFromID("bcno") : sTmp1 = .Text
                                .Col = .GetColFromID("testcd") : sTmp2 = .Text

                                If sTmp1 = sBcNo And sTmp2 = sTestCd Then
                                    iRow = ix
                                    bFind = True
                                    Exit For
                                End If
                            Next

                            If bFind = True Or sBcNo.Length < 15 Then
                            Else
                                .MaxRows += 1
                                iRow = .MaxRows

                                Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_SpcInfo(sBcNo, sTestCd)

                                If dt.Rows.Count > 0 Then
                                    .Row = iRow
                                    .Col = .GetColFromID("regno") : .Text = sRegNo
                                    .Col = .GetColFromID("bcno") : .Text = sBcNo
                                    .Col = .GetColFromID("patnm") : .Text = sPatnm
                                    .Col = .GetColFromID("testcd") : .Text = sTestCd
                                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(0).Item("tnmd").ToString
                                    .Col = .GetColFromID("rstval") : .Text = sFile

                                    .Col = .GetColFromID("prtbcno") : .Text = "*" + (New LISAPP.APP_DB.DbFn).GetViewToBCPrt(sBcNo.Replace("-", "")) + "*"

                                    If dt.Rows(0).Item("regno").ToString.Trim <> sRegNo Then
                                        .Col = .GetColFromID("remark") : .Text = "자료오류"
                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    Else
                                        If dt.Rows(0).Item("spcflg").ToString.Trim = "4" Then

                                            Select Case dt.Rows(0).Item("rstflg").ToString.Trim
                                                Case "1"
                                                    .Col = .GetColFromID("rstflg") : .Text = "△"
                                                Case "2"
                                                    .Col = .GetColFromID("rstflg") : .Text = "○"

                                                    .Col = .GetColFromID("chk")
                                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                                Case "3"
                                                    .Col = .GetColFromID("rstflg") : .Text = "◆"
                                                    .ForeColor = Color.DarkGreen

                                                    .Col = .GetColFromID("chk")
                                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                                Case Else
                                                    .Col = .GetColFromID("rstflg") : .Text = ""
                                                    .Col = .GetColFromID("chk") : .Text = "1"
                                            End Select
                                        End If

                                    End If


                                    If dt.Rows(0).Item("spcflg").ToString.Trim = "2" Then
                                        .Col = .GetColFromID("remark") : .Text = "채혈"
                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    ElseIf dt.Rows(0).Item("spcflg").ToString.Trim = "R" Then
                                        .Col = .GetColFromID("remark") : .Text = "취소"
                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    ElseIf dt.Rows(0).Item("spcflg").ToString.Trim <> "4" Then
                                        .Col = .GetColFromID("remark") : .Text = "미채혈"
                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    End If
                                Else
                                    .Row = iRow
                                    .Col = .GetColFromID("regno") : .Text = sRegNo
                                    .Col = .GetColFromID("bcno") : .Text = sBcNo
                                    .Col = .GetColFromID("patnm") : .Text = sPatnm
                                    .Col = .GetColFromID("testcd") : .Text = sTestCd
                                    .Col = .GetColFromID("rstval") : .Text = sFile

                                    .Col = .GetColFromID("remark") : .Text = "자료오류"
                                    .Col = .GetColFromID("chk")
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                End If
                            End If
                        End With
                    End If

                End If

            End If
        Next

    End Sub

    Private Sub sbOpenImg()

        m_al_FileList.Clear()

        If Me.txtPath.Text = "" Then
            MsgBox("이미지 파일이 들어있는 경로명을 입력해 주십시오")
            Exit Sub
        End If

        If Dir(Me.txtPath.Text.Trim, FileAttribute.Directory) = "" Then MkDir(Me.txtPath.Text.Trim)

        Dim sFiles As String()

        sFiles = Directory.GetFileSystemEntries(txtPath.Text)

        For intix1 As Integer = 0 To sFiles.Length - 1
            If sFiles(intix1).ToLower.IndexOf(".jpg") > 0 Then
                chkImg.Checked = True

                Dim sFile As String = sFiles(intix1)
                Dim sBuf() As String

                If sFile <> "" Then

                    sBuf = Split(sFile, "\")
                    Dim sTmp As String = sBuf(sBuf.Length - 1)
                    sTmp = sTmp.Replace(".jpg", "")

                    Erase sBuf

                    sBuf = Split(sTmp, "-")

                    If sBuf.Length > 3 Then
                        Dim sRegNo As String = sBuf(0)
                        Dim sPatnm As String = sBuf(1)
                        Dim sBcNo As String = sBuf(2)
                        Dim sTestCd As String = sBuf(3)

                        m_al_FileList.Add(sBcNo + "^" + sTestCd + "|" + sFile)

                        With spdResult_img
                            Dim bFind As Boolean = False
                            Dim iRow As Integer = 0

                            For ix As Integer = 1 To .MaxRows
                                Dim sTmp1 As String = ""
                                Dim sTmp2 As String = ""

                                .Row = ix
                                .Col = .GetColFromID("bcno") : sTmp1 = .Text
                                .Col = .GetColFromID("testcd") : sTmp2 = .Text

                                If sTmp1 = sBcNo And sTmp2 = sTestCd Then
                                    iRow = ix
                                    bFind = True
                                    Exit For
                                End If
                            Next

                            If bFind = True Or sBcNo.Length < 15 Then
                            Else
                                .MaxRows += 1
                                iRow = .MaxRows

                                Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_SpcInfo(sBcNo, sTestCd)

                                If dt.Rows.Count > 0 Then
                                    .Row = iRow
                                    .Col = .GetColFromID("regno") : .Text = sRegNo
                                    .Col = .GetColFromID("bcno") : .Text = sBcNo
                                    .Col = .GetColFromID("patnm") : .Text = sPatnm
                                    .Col = .GetColFromID("testcd") : .Text = sTestCd
                                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(0).Item("tnmd").ToString
                                    .Col = .GetColFromID("rstval") : .Text = sFile

                                    .Col = .GetColFromID("prtbcno") : .Text = "*" + (New LISAPP.APP_DB.DbFn).GetViewToBCPrt(sBcNo.Replace("-", "")) + "*"

                                    If dt.Rows(0).Item("regno").ToString.Trim <> sRegNo Then
                                        .Col = .GetColFromID("remark") : .Text = "자료오류"
                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    Else
                                        If dt.Rows(0).Item("spcflg").ToString.Trim = "4" Then

                                            Select Case dt.Rows(0).Item("rstflg").ToString.Trim
                                                Case "1"
                                                    .Col = .GetColFromID("rstflg") : .Text = "△"
                                                Case "2"
                                                    .Col = .GetColFromID("rstflg") : .Text = "○"

                                                    .Col = .GetColFromID("chk")
                                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                                Case "3"
                                                    .Col = .GetColFromID("rstflg") : .Text = "◆"
                                                    .ForeColor = Color.DarkGreen

                                                    .Col = .GetColFromID("chk")
                                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                                Case Else
                                                    .Col = .GetColFromID("rstflg") : .Text = ""
                                                    .Col = .GetColFromID("chk") : .Text = "1"
                                            End Select
                                        End If

                                    End If


                                    If dt.Rows(0).Item("spcflg").ToString.Trim = "2" Then
                                        .Col = .GetColFromID("remark") : .Text = "채혈"
                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    ElseIf dt.Rows(0).Item("spcflg").ToString.Trim = "R" Then
                                        .Col = .GetColFromID("remark") : .Text = "취소"
                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    ElseIf dt.Rows(0).Item("spcflg").ToString.Trim <> "4" Then
                                        .Col = .GetColFromID("remark") : .Text = "미채혈"
                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    End If
                                Else
                                    .Row = iRow
                                    .Col = .GetColFromID("regno") : .Text = sRegNo
                                    .Col = .GetColFromID("bcno") : .Text = sBcNo
                                    .Col = .GetColFromID("patnm") : .Text = sPatnm
                                    .Col = .GetColFromID("testcd") : .Text = sTestCd
                                    .Col = .GetColFromID("rstval") : .Text = sFile

                                    .Col = .GetColFromID("remark") : .Text = "자료오류"
                                    .Col = .GetColFromID("chk")
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                End If
                            End If
                        End With
                    End If

                End If

            End If
        Next

    End Sub

    Private Sub btnPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPath.Click

        Dim sDefault As String
        Dim sFilePath As String

        sDefault = txtPath.Text

        If sDefault <> "" Then
            fbdPath.SelectedPath = sDefault
        End If

        If fbdPath.ShowDialog() = DialogResult.OK Then

            sFilePath = fbdPath.SelectedPath
            txtPath.Text = sFilePath
        End If

    End Sub

    '-- 더블클릭시에 스프레드에 이미지 파일 리스트 생성함.
    Private Sub lstImgFile_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstImgFile.DoubleClick

        Dim strValue As String = ""
        Dim strFileNm As String = ""
        Dim aryBuf() As String

        If lstImgFile.SelectedIndex >= 0 Then
            strValue = lstImgFile.Items(lstImgFile.SelectedIndex).ToString
        End If

        If strValue <> "" Then
            strFileNm = strValue

            aryBuf = Split(strValue, "\")
            Dim strTmp As String = aryBuf(aryBuf.Length - 1)
            strTmp = strTmp.Replace(".jpg", "")

            Erase aryBuf

            aryBuf = Split(strTmp, "-")

            Dim strRegNo As String
            Dim strPatnm As String
            Dim strBcNo As String
            Dim strtestcd As String

            strRegNo = aryBuf(0)
            strPatnm = aryBuf(1)
            strBcNo = aryBuf(2)
            strtestcd = aryBuf(3)

            With spdResult_img
                Dim blnFind As Boolean = False
                Dim intRow As Integer = 0

                For intIdx As Integer = 1 To .MaxRows
                    Dim strTmp1 As String = ""
                    Dim strTmp2 As String = ""

                    .Row = intIdx
                    .Col = .GetColFromID("bcno") : strTmp1 = .Text
                    .Col = .GetColFromID("testcd") : strTmp2 = .Text

                    If strTmp1 = strBcNo And strTmp2 = strtestcd Then
                        intRow = intIdx
                        blnFind = True
                        Exit For
                    End If
                Next

                If blnFind = True Then
                Else
                    .MaxRows += 1
                    intRow = .MaxRows

                    Dim objDTable As New DataTable

                    objDTable = LISAPP.APP_EXLAB.fnGet_SpcInfo(strBcNo, strtestcd)
                    If objDTable.Rows.Count > 0 Then
                        .Row = intRow
                        .Col = .GetColFromID("regno") : .Text = strRegNo
                        .Col = .GetColFromID("bcno") : .Text = strBcNo
                        .Col = .GetColFromID("patnm") : .Text = strPatnm
                        .Col = .GetColFromID("testcd") : .Text = strtestcd
                        .Col = .GetColFromID("tnmd") : .Text = objDTable.Rows(0).Item("tnmd").ToString
                        .Col = .GetColFromID("rstval") : .Text = strFileNm

                        If objDTable.Rows(0).Item("regno").ToString <> strRegNo Then
                            .Col = .GetColFromID("remark") : .Text = "자료오류"
                            .Col = .GetColFromID("chk")
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        Else
                            '''If objDTable.Rows(0).Item("spcflag").ToString = "2" Then
                            If objDTable.Rows(0).Item("spcflag").ToString = "4" Then

                                Select Case objDTable.Rows(0).Item("rstflag").ToString
                                    Case "1"
                                        .Col = .GetColFromID("rstflag") : .Text = "△"
                                    Case "2"
                                        .Col = .GetColFromID("rstflag") : .Text = "○"

                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                    Case "3"
                                        .Col = .GetColFromID("rstflag") : .Text = "◆"
                                        .ForeColor = Color.DarkGreen

                                        .Col = .GetColFromID("chk")
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                    Case Else
                                        .Col = .GetColFromID("rstflag") : .Text = ""
                                End Select
                            End If

                        End If

                        '''If objDTable.Rows(0).Item("spcflag").ToString = "1" Then
                        '''    .Col = .GetColFromID("remark") : .Text = "채혈"
                        '''    .Col = .GetColFromID("chk")
                        '''    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        '''ElseIf objDTable.Rows(0).Item("spcflag").ToString = "R" Then
                        '''    .Col = .GetColFromID("remark") : .Text = "취소"
                        '''    .Col = .GetColFromID("chk")
                        '''    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        '''ElseIf objDTable.Rows(0).Item("spcflag").ToString <> "2" Then
                        '''    .Col = .GetColFromID("remark") : .Text = "미채혈"
                        '''    .Col = .GetColFromID("chk")
                        '''    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        '''End If

                        If objDTable.Rows(0).Item("spcflag").ToString = "2" Then
                            .Col = .GetColFromID("remark") : .Text = "채혈"
                            .Col = .GetColFromID("chk")
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        ElseIf objDTable.Rows(0).Item("spcflag").ToString = "R" Then
                            .Col = .GetColFromID("remark") : .Text = "취소"
                            .Col = .GetColFromID("chk")
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        ElseIf objDTable.Rows(0).Item("spcflag").ToString <> "4" Then
                            .Col = .GetColFromID("remark") : .Text = "미채혈"
                            .Col = .GetColFromID("chk")
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        End If
                    Else
                        .Row = intRow
                        .Col = .GetColFromID("regno") : .Text = strRegNo
                        .Col = .GetColFromID("bcno") : .Text = strBcNo
                        .Col = .GetColFromID("patnm") : .Text = strPatnm
                        .Col = .GetColFromID("testcd") : .Text = strtestcd
                        .Col = .GetColFromID("rstval") : .Text = strFileNm

                        .Col = .GetColFromID("remark") : .Text = "자료오류"
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    End If

                End If
            End With
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Me.Close()

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDiplay_Init()
    End Sub

    Private Sub btnRegImg_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        sbReg_Img("1")

    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click

        Select Case Me.tabExLab.SelectedTab.Text.Trim
            Case "[ TEXT 결과저장 ]"
                sbReg(1)
            Case "[ IMAGE 결과저장 ]"
                sbReg_Img("1")
        End Select

    End Sub

    Private Sub sbToExcel()
        Dim sw As IO.StreamWriter

        Dim sfdlg As New System.Windows.Forms.SaveFileDialog
        Dim sInitDir As String = "c:\수탁검사" 'System.Windows.Forms.Application.StartupPath + "\CSV"
        Dim sFileNm As String = ""

        Try
            If IO.Directory.Exists(sInitDir) = False Then
                IO.Directory.CreateDirectory(sInitDir)
            End If

            With sfdlg
                .CheckPathExists = True

                .DefaultExt = "xls"
                .Filter = "Excel files (*.xls)|*.xls"
                .InitialDirectory = sInitDir
                .FileName = "위탁검사_일반_" + Format(Now, "yyMMdd").ToString
                .OverwritePrompt = True
                .ShowDialog()
                sFileNm = .FileName
            End With

            If sFileNm = "" Then Return

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            With spdResult
                .ReDraw = False

                .MaxRows += 3
                .InsertRows(1, 3)

                .Col = 8
                .Row = 1
                .Text = "위탁검사 일반검사"
                .FontBold = True
                .FontSize = 15
                .ForeColor = System.Drawing.Color.Red

                Dim sColHeaders As String = ""

                .Col = 1 : .Col2 = .MaxCols
                .Row = 0 : .Row2 = 0
                sColHeaders = .Clip

                .Col = 1 : .Col2 = .MaxCols
                .Row = 3 : .Row2 = 3
                .Clip = sColHeaders

                .Col = .GetColFromID("chk")
                .ColHidden = True

                .Col = .GetColFromID("prtbcno")
                .ColHidden = False
                .set_ColWidth(.GetColFromID("prtbcno"), 12)

                If spdResult.ExportToExcel(sFileNm, "위탁검사 일반검사", "") Then
                    Process.Start(sFileNm)
                End If

                .Col = .GetColFromID("chk")
                .ColHidden = False

                .Col = .GetColFromID("prtbcno")
                .ColHidden = True


                .DeleteRows(1, 3)
                .MaxRows -= 3

                .ReDraw = True
            End With

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            If sw IsNot Nothing Then
                sw.Close()

                MsgBox("파일을 생성하였습니다!!" + vbCrLf + vbCrLf + sFileNm, MsgBoxStyle.Information)
            End If
        End Try

    End Sub

    Private Sub sbToExcel_img()
        Dim sw As IO.StreamWriter

        Dim sfdlg As New System.Windows.Forms.SaveFileDialog
        Dim sInitDir As String = "c:\수탁검사" 'System.Windows.Forms.Application.StartupPath + "\CSV"
        Dim sFileNm As String = ""

        Try
            If IO.Directory.Exists(sInitDir) = False Then
                IO.Directory.CreateDirectory(sInitDir)
            End If

            With sfdlg
                .CheckPathExists = True

                .DefaultExt = "xls"
                .Filter = "Excel files (*.xls)|*.xls"
                .InitialDirectory = sInitDir
                .FileName = "위탁검사_특수_" + Format(Now, "yyMMdd").ToString
                .OverwritePrompt = True
                .ShowDialog()
                sFileNm = .FileName
            End With

            If sFileNm = "" Then Return

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            With spdResult_img
                .ReDraw = False

                .MaxRows += 3
                .InsertRows(1, 3)

                .Col = 8
                .Row = 1
                .Text = "위탁검사 특수검사"
                .FontBold = True
                .FontSize = 15
                .ForeColor = System.Drawing.Color.Red

                Dim sColHeaders As String = ""

                .Col = 1 : .Col2 = .MaxCols
                .Row = 0 : .Row2 = 0
                sColHeaders = .Clip

                .Col = 1 : .Col2 = .MaxCols
                .Row = 3 : .Row2 = 3
                .Clip = sColHeaders

                .Col = .GetColFromID("chk")
                .ColHidden = True

                .Col = .GetColFromID("prtbcno")
                .ColHidden = False
                .set_ColWidth(.GetColFromID("prtbcno"), 12)

                If spdResult_img.ExportToExcel(sFileNm, "위탁검사 특수검사", "") Then
                    Process.Start(sFileNm)
                End If

                .Col = .GetColFromID("chk")
                .ColHidden = False

                .Col = .GetColFromID("prtbcno")
                .ColHidden = True


                .DeleteRows(1, 3)
                .MaxRows -= 3

                .ReDraw = True
            End With

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            If sw IsNot Nothing Then
                sw.Close()

                MsgBox("파일을 생성하였습니다!!" + vbCrLf + vbCrLf + sFileNm, MsgBoxStyle.Information)
            End If
        End Try


    End Sub

    Private Sub chkRst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRst.Click

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Text = IIf(chkRst.Checked = True, "1", "").ToString
                End If
            Next
        End With

    End Sub

    Private Sub chkImg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkImg.Click

        With spdResult_img
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Text = IIf(chkImg.Checked = True, "1", "").ToString
                End If
            Next
        End With

    End Sub
    <DllImport("HttpDll.dll", SetLastError:=True, _
CharSet:=CharSet.Ansi, ExactSpelling:=True, _
CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function HttpResultSvr(ByVal JOBGBN As String, ByVal RTNGBN As String, ByVal OCSHEAD As String, ByVal OCSDATA As String, ByVal OCSTEMP As String) As String

    End Function

    Private Sub sbOpenFile_SCL_web()

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Try
            'MsgBox("0")
            Dim sReturn As String = ""
            Dim sOcsHead As String = "022429"
            Dim sOcsData As String = ""
            Dim sOcsTemp As String = ""
            Dim sResult() As String

            Dim sStatGbn As String = Ctrl.Get_Code(Me.cboState)
            Dim sDateGbn As String = Ctrl.Get_Code(Me.cboDate) '0:전체 1:접수일자 2: 보고일자 (디폴트)

            Dim sSdate As String = Me.dtpDateS.Text.Replace("-", "").Replace(" ", "")
            Dim sEdate As String = Me.dtpDateE.Text.Replace("-", "").Replace(" ", "")
            'MsgBox("0-1")
            Dim sDeptcd As String = ""
            Dim sReceve As String = "0"
            Dim sOcskey As String = ""
            Dim sHospiGbn As String = "0"

            sOcsHead = sOcsHead + "|" + sStatGbn + "|" + sDateGbn + "|" + sSdate + "|" + sEdate + "|" + sDeptcd + "|" + sReceve + "|" + sOcskey + "|" + sHospiGbn
            'MsgBox("DLL 호출   " + sOcsHead + "   " + sOcsData + "   " + sOcsTemp)

            'sReturn = (New WEBSERVER.CGWEB_J).fngetResult_SCL_WEb(sOcsHead, "", "")

            sReturn = HttpResultSvr("0", "0", sOcsHead, sOcsData, sOcsTemp)
            'MsgBox("1" + sReturn)
            Dim sSOH As String = Convert.ToChar(1).ToString '
            Dim sSTX As String = Convert.ToChar(2).ToString
            Dim sETX As String = Convert.ToChar(3).ToString
            Dim sEOT As String = Convert.ToChar(4).ToString
            Dim sENQ As String = Convert.ToChar(5).ToString
            Dim sACK As String = Convert.ToChar(6).ToString
            Dim sBEL As String = Convert.ToChar(7).ToString
            Dim sBS As String = Convert.ToChar(8).ToString

            Dim temp1() As String = sReturn.Split(Convert.ToChar(2))

            Dim temp2() As String = temp1(1).Split(Convert.ToChar(1))

            sResult = temp2(1).Split(Convert.ToChar(4))
            'MsgBox("2")
            If sResult.Length < 1 Then
                MsgBox("SCL 결과테이터 로드 오류 ")
                Return
            End If


            'If sReturn.Split("|"c)(0) = "00000" Then
            '    sResult = sReturn.Substring(5).Split("|"c)
            'Else
            '    MsgBox("SCL 결과테이터 로드 오류 ")
            '    Return
            'End If

            Dim alList As New ArrayList
            'MsgBox("3")
            For ix As Integer = 0 To sResult.Length - 1
                Dim sResult_dt() As String = sResult(ix).Split("|"c)

                Dim sExLabDate As String = ""

                sExLabDate = sResult_dt(0)
                If sExLabDate = "" Then Exit Sub

                Dim sRegNo As String = sResult_dt(1)
                Dim sBcNo As String = sResult_dt(2)
                Dim sPatNm As String = sResult_dt(3)
                Dim sTestCd As String = sResult_dt(4)
                Dim sOrgRst As String = sResult_dt(5)
                Dim sRstCmt As String = sResult_dt(6)
                Dim sImgyn As String = sResult_dt(9)

                Dim objExLab As New ExLabInfo
                'MsgBox("4")
                If IsNumeric(sExLabDate) And sBcNo.Length = 15 And sImgyn <> "Y" Then

                    Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_SpcInfo(sBcNo, sTestCd)
                    Dim sPartSlip As String = ""

                    If dt.Rows.Count > 0 Then sPartSlip = dt.Rows(0).Item("partslip").ToString.Trim

                    If Ctrl.Get_Code(cboPartSlip) = "" Or Ctrl.Get_Code(cboPartSlip) = sPartSlip Then
                        objExLab.ExLabDate = sExLabDate.Replace("-", "")   ' 의뢰일자
                        objExLab.RegNo = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)     ' 등록번호
                        objExLab.BcNo = sBcNo         ' 검체번호
                        objExLab.PatNm = sPatNm       ' 환자이름
                        objExLab.TestCd = sTestCd         ' 검사코드
                        If Trim(sOrgRst) = "" And Trim(sRstCmt) <> "" Then '소견만 있는 경우
                            sOrgRst = "[소견 참조]"
                        End If
                        objExLab.RstVal = sOrgRst       ' 결과값

                        objExLab.RSTCmt = sRstCmt       ' 소견

                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item("titleyn").ToString = "0" Then
                                objExLab.SpcCd = dt.Rows(0).Item("spccd").ToString.Trim
                                objExLab.RstFlg = dt.Rows(0).Item("rstflg").ToString.Trim
                                objExLab.SpcFlg = dt.Rows(0).Item("spcflg").ToString.Trim
                                objExLab.Tnmd = dt.Rows(0).Item("tnmd").ToString.Trim
                                objExLab.OldRst = dt.Rows(0).Item("orgrst").ToString.Trim
                                objExLab.CRegNo = dt.Rows(0).Item("regno").ToString.Trim
                            End If
                        End If

                        alList.Add(objExLab)
                    End If
                End If

            Next
            'MsgBox("5")
            If alList.Count > 0 Then
                chkRst.Checked = True
                spdResult.MaxRows = 0
                spdResult.MaxRows = alList.Count
                For ix As Integer = 0 To alList.Count - 1
                    With spdResult
                        .Row = ix + 1
                        .Col = .GetColFromID("regno") : .Text = CType(alList(ix), ExLabInfo).RegNo
                        .Col = .GetColFromID("bcno") : .Text = CType(alList(ix), ExLabInfo).BcNo
                        .Col = .GetColFromID("patnm") : .Text = CType(alList(ix), ExLabInfo).PatNm
                        .Col = .GetColFromID("rstval") : .Text = CType(alList(ix), ExLabInfo).RstVal
                        .Col = .GetColFromID("rstcmt") : .Text = CType(alList(ix), ExLabInfo).RSTCmt
                        .Col = .GetColFromID("testcd") : .Text = CType(alList(ix), ExLabInfo).TestCd
                        .Col = .GetColFromID("tnmd") : .Text = CType(alList(ix), ExLabInfo).Tnmd

                        .Col = .GetColFromID("prtbcno") : .Text = "*" + (New LISAPP.APP_DB.DbFn).GetViewToBCPrt(CType(alList(ix), ExLabInfo).BcNo.Replace("-", "")) + "*"

                        If CType(alList(ix), ExLabInfo).RegNo <> CType(alList(ix), ExLabInfo).CRegNo Then
                            .Col = .GetColFromID("remark") : .Text = "자료오류"
                            .Col = .GetColFromID("chk")
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        Else
                            If CType(alList(ix), ExLabInfo).SpcFlg = "4" Then

                                Select Case CType(alList(ix), ExLabInfo).RstFlg
                                    Case "1"
                                        .Col = .GetColFromID("rstflg") : .Text = "△"
                                    Case "2"
                                        .Col = .GetColFromID("rstflg") : .Text = "○"
                                    Case "3"
                                        .Col = .GetColFromID("rstflg") : .Text = "◆"
                                        .ForeColor = Color.DarkGreen
                                    Case Else
                                        .Col = .GetColFromID("rstflg") : .Text = ""
                                        If CType(alList(ix), ExLabInfo).RstVal.IndexOf("별지통보") >= 0 Then
                                        Else
                                            .Col = .GetColFromID("chk") : .Text = "1"
                                        End If
                                End Select
                            End If

                            If CType(alList(ix), ExLabInfo).SpcFlg = "2" Then
                                .Col = .GetColFromID("remark") : .Text = "채혈"
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            ElseIf CType(alList(ix), ExLabInfo).SpcFlg = "R" Then
                                .Col = .GetColFromID("remark") : .Text = "취소"
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            ElseIf CType(alList(ix), ExLabInfo).SpcFlg <> "4" Then
                                .Col = .GetColFromID("remark") : .Text = "미채혈"
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            End If
                        End If
                    End With
                Next
            End If
            'MsgBox("6")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing

        End Try

    End Sub
    Private Sub sbOpenFile_SCL()

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Try
            MsgBox("0")
            Dim sReturn As String = ""
            Dim sOcsHead As String = "022429"
            Dim sOcsData As String = ""
            Dim sOcsTemp As String = ""
            Dim sResult() As String

            Dim sStatGbn As String = Ctrl.Get_Code(Me.cboState)
            Dim sDateGbn As String = Ctrl.Get_Code(Me.cboDate) '0:전체 1:접수일자 2: 보고일자 (디폴트)

            Dim sSdate As String = Me.dtpDateS.Text.Replace("-", "").Replace(" ", "")
            Dim sEdate As String = Me.dtpDateE.Text.Replace("-", "").Replace(" ", "")
            MsgBox("0-1")
            Dim sDeptcd As String = ""
            Dim sReceve As String = "0"
            Dim sOcskey As String = ""
            Dim sHospiGbn As String = "0"

            sOcsHead = sOcsHead + "|" + sStatGbn + "|" + sDateGbn + "|" + sSdate + "|" + sEdate + "|" + sDeptcd + "|" + sReceve + "|" + sOcskey + "|" + sHospiGbn
            MsgBox("DLL 호출   " + sOcsHead + "   " + sOcsData + "   " + sOcsTemp)

            sReturn = HttpResultSvr("0", "0", sOcsHead, sOcsData, sOcsTemp)
            MsgBox("1" + sReturn)
            Dim sSOH As String = Convert.ToChar(1).ToString '
            Dim sSTX As String = Convert.ToChar(2).ToString
            Dim sETX As String = Convert.ToChar(3).ToString
            Dim sEOT As String = Convert.ToChar(4).ToString
            Dim sENQ As String = Convert.ToChar(5).ToString
            Dim sACK As String = Convert.ToChar(6).ToString
            Dim sBEL As String = Convert.ToChar(7).ToString
            Dim sBS As String = Convert.ToChar(8).ToString

            Dim temp1() As String = sReturn.Split(Convert.ToChar(2))

            Dim temp2() As String = temp1(1).Split(Convert.ToChar(1))

            sResult = temp2(1).Split(Convert.ToChar(4))
            MsgBox("2")
            If sResult.Length < 1 Then
                MsgBox("SCL 결과테이터 로드 오류 ")
                Return
            End If


            'If sReturn.Split("|"c)(0) = "00000" Then
            '    sResult = sReturn.Substring(5).Split("|"c)
            'Else
            '    MsgBox("SCL 결과테이터 로드 오류 ")
            '    Return
            'End If

            Dim alList As New ArrayList
            MsgBox("3")
            For ix As Integer = 0 To sResult.Length - 1
                Dim sResult_dt() As String = sResult(ix).Split("|"c)

                Dim sExLabDate As String = ""

                sExLabDate = sResult_dt(0)
                If sExLabDate = "" Then Exit Sub

                Dim sRegNo As String = sResult_dt(1)
                Dim sBcNo As String = sResult_dt(2)
                Dim sPatNm As String = sResult_dt(3)
                Dim sTestCd As String = sResult_dt(4)
                Dim sOrgRst As String = sResult_dt(5)
                Dim sRstCmt As String = sResult_dt(6)
                Dim sImgyn As String = sResult_dt(9)

                Dim objExLab As New ExLabInfo
                MsgBox("4")
                If IsNumeric(sExLabDate) And sBcNo.Length = 15 And sImgyn <> "Y" Then

                    Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_SpcInfo(sBcNo, sTestCd)
                    Dim sPartSlip As String = ""

                    If dt.Rows.Count > 0 Then sPartSlip = dt.Rows(0).Item("partslip").ToString.Trim

                    If Ctrl.Get_Code(cboPartSlip) = "" Or Ctrl.Get_Code(cboPartSlip) = sPartSlip Then
                        objExLab.ExLabDate = sExLabDate.Replace("-", "")   ' 의뢰일자
                        objExLab.RegNo = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)     ' 등록번호
                        objExLab.BcNo = sBcNo         ' 검체번호
                        objExLab.PatNm = sPatNm       ' 환자이름
                        objExLab.TestCd = sTestCd         ' 검사코드
                        If Trim(sOrgRst) = "" And Trim(sRstCmt) <> "" Then '소견만 있는 경우
                            sOrgRst = "[소견 참조]"
                        End If
                        objExLab.RstVal = sOrgRst       ' 결과값

                        objExLab.RSTCmt = sRstCmt       ' 소견

                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item("titleyn").ToString = "0" Then
                                objExLab.SpcCd = dt.Rows(0).Item("spccd").ToString.Trim
                                objExLab.RstFlg = dt.Rows(0).Item("rstflg").ToString.Trim
                                objExLab.SpcFlg = dt.Rows(0).Item("spcflg").ToString.Trim
                                objExLab.Tnmd = dt.Rows(0).Item("tnmd").ToString.Trim
                                objExLab.OldRst = dt.Rows(0).Item("orgrst").ToString.Trim
                                objExLab.CRegNo = dt.Rows(0).Item("regno").ToString.Trim
                            End If
                        End If

                        alList.Add(objExLab)
                    End If
                End If

            Next
            MsgBox("5")
            If alList.Count > 0 Then
                chkRst.Checked = True
                spdResult.MaxRows = 0
                spdResult.MaxRows = alList.Count
                For ix As Integer = 0 To alList.Count - 1
                    With spdResult
                        .Row = ix + 1
                        .Col = .GetColFromID("regno") : .Text = CType(alList(ix), ExLabInfo).RegNo
                        .Col = .GetColFromID("bcno") : .Text = CType(alList(ix), ExLabInfo).BcNo
                        .Col = .GetColFromID("patnm") : .Text = CType(alList(ix), ExLabInfo).PatNm
                        .Col = .GetColFromID("rstval") : .Text = CType(alList(ix), ExLabInfo).RstVal
                        .Col = .GetColFromID("rstcmt") : .Text = CType(alList(ix), ExLabInfo).RSTCmt
                        .Col = .GetColFromID("testcd") : .Text = CType(alList(ix), ExLabInfo).TestCd
                        .Col = .GetColFromID("tnmd") : .Text = CType(alList(ix), ExLabInfo).Tnmd

                        .Col = .GetColFromID("prtbcno") : .Text = "*" + (New LISAPP.APP_DB.DbFn).GetViewToBCPrt(CType(alList(ix), ExLabInfo).BcNo.Replace("-", "")) + "*"

                        If CType(alList(ix), ExLabInfo).RegNo <> CType(alList(ix), ExLabInfo).CRegNo Then
                            .Col = .GetColFromID("remark") : .Text = "자료오류"
                            .Col = .GetColFromID("chk")
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        Else
                            If CType(alList(ix), ExLabInfo).SpcFlg = "4" Then

                                Select Case CType(alList(ix), ExLabInfo).RstFlg
                                    Case "1"
                                        .Col = .GetColFromID("rstflg") : .Text = "△"
                                    Case "2"
                                        .Col = .GetColFromID("rstflg") : .Text = "○"
                                    Case "3"
                                        .Col = .GetColFromID("rstflg") : .Text = "◆"
                                        .ForeColor = Color.DarkGreen
                                    Case Else
                                        .Col = .GetColFromID("rstflg") : .Text = ""
                                        If CType(alList(ix), ExLabInfo).RstVal.IndexOf("별지통보") >= 0 Then
                                        Else
                                            .Col = .GetColFromID("chk") : .Text = "1"
                                        End If
                                End Select
                            End If

                            If CType(alList(ix), ExLabInfo).SpcFlg = "2" Then
                                .Col = .GetColFromID("remark") : .Text = "채혈"
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            ElseIf CType(alList(ix), ExLabInfo).SpcFlg = "R" Then
                                .Col = .GetColFromID("remark") : .Text = "취소"
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            ElseIf CType(alList(ix), ExLabInfo).SpcFlg <> "4" Then
                                .Col = .GetColFromID("remark") : .Text = "미채혈"
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            End If
                        End If
                    End With
                Next
            End If
            MsgBox("6")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing

        End Try

    End Sub
    Private Sub sbOpenFile()

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Try
            If Dir("c:\수탁검사\결과", FileAttribute.Directory) = "" Then MkDir("c:\수탁검사\결과")

            ofdExLab.InitialDirectory = "E:\수탁검사\결과"

            ofdExLab.Filter = "Excel files (*.xls)|*.xls"
            ofdExLab.FilterIndex = 2            ' 파일 대화 상자에서 현재 선택한 필터의 인덱스를 가져오거나 설정
            ofdExLab.RestoreDirectory = True    ' 대화상자를 닫기전 대화상자에서 현재 디렉터리를 복원할지 여부를 나타내는 값을 가져오거나 설정

            If ofdExLab.ShowDialog() = DialogResult.OK Then
                xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
                xlsWkB = xlsApp.Workbooks.Open(ofdExLab.FileName)

                xlsWkS = CType(xlsWkB.Sheets(1), Excel.Worksheet)

                Dim alList As New ArrayList
                Dim intLine As Integer = 0

                Do While True

                    intLine += 1

                    Dim sExLabDate As String = ""
                    Try

                        sExLabDate = xlsWkS.Range("A" + CStr(intLine)).Value.ToString()
                    Catch ex As Exception
                        Exit Do
                    End Try

                    Dim sRegNo As String = xlsWkS.Range("B" + CStr(intLine)).Value.ToString
                    Dim sBcNo As String = xlsWkS.Range("C" + CStr(intLine)).Value.ToString
                    Dim sPatNm As String = xlsWkS.Range("D" + CStr(intLine)).Value.ToString
                    Dim sTestCd As String = xlsWkS.Range("E" + CStr(intLine)).Value.ToString
                    Dim sOrgRst As String = xlsWkS.Range("F" + CStr(intLine)).Value.ToString
                    Dim sRstCmt As String = ""
                    If xlsWkS.Range("G" + CStr(intLine)).Value IsNot Nothing Then xlsWkS.Range("G" + CStr(intLine)).Value.ToString()

                    If sExLabDate Is Nothing Then Exit Do
                    If sRegNo Is Nothing Then sRegNo = ""
                    If sBcNo Is Nothing Then sRegNo = ""
                    If sPatNm Is Nothing Then sPatNm = ""
                    If sTestCd Is Nothing Then sTestCd = ""
                    If sOrgRst Is Nothing Then sOrgRst = ""
                    If sRstCmt Is Nothing Then sRstCmt = ""

                    Dim objExLab As New ExLabInfo

                    If IsNumeric(sExLabDate) And sBcNo.Length = 15 Then

                        Dim dt As DataTable = LISAPP.APP_EXLAB.fnGet_SpcInfo(sBcNo, sTestCd)
                        Dim sPartSlip As String = ""

                        If dt.Rows.Count > 0 Then sPartSlip = dt.Rows(0).Item("partslip").ToString.Trim

                        If Ctrl.Get_Code(cboPartSlip) = "" Or Ctrl.Get_Code(cboPartSlip) = sPartSlip Then
                            objExLab.ExLabDate = sExLabDate.Replace("-", "")   ' 의뢰일자
                            objExLab.RegNo = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)     ' 등록번호
                            objExLab.BcNo = sBcNo         ' 검체번호
                            objExLab.PatNm = sPatNm       ' 환자이름
                            objExLab.TestCd = sTestCd         ' 검사코드
                            objExLab.RstVal = sOrgRst       ' 결과값
                            objExLab.RSTCmt = sRstCmt

                            If dt.Rows.Count > 0 Then
                                If dt.Rows(0).Item("titleyn").ToString = "0" Then
                                    objExLab.SpcCd = dt.Rows(0).Item("spccd").ToString.Trim
                                    objExLab.RstFlg = dt.Rows(0).Item("rstflg").ToString.Trim
                                    objExLab.SpcFlg = dt.Rows(0).Item("spcflg").ToString.Trim
                                    objExLab.Tnmd = dt.Rows(0).Item("tnmd").ToString.Trim
                                    objExLab.OldRst = dt.Rows(0).Item("orgrst").ToString.Trim
                                    objExLab.CRegNo = dt.Rows(0).Item("regno").ToString.Trim
                                End If
                            End If

                            alList.Add(objExLab)
                        End If
                    End If
                Loop

                If alList.Count > 0 Then
                    chkRst.Checked = True
                    spdResult.MaxRows = 0
                    spdResult.MaxRows = alList.Count
                    For ix As Integer = 0 To alList.Count - 1
                        With spdResult
                            .Row = ix + 1
                            .Col = .GetColFromID("regno") : .Text = CType(alList(ix), ExLabInfo).RegNo
                            .Col = .GetColFromID("bcno") : .Text = CType(alList(ix), ExLabInfo).BcNo
                            .Col = .GetColFromID("patnm") : .Text = CType(alList(ix), ExLabInfo).PatNm
                            .Col = .GetColFromID("rstval") : .Text = CType(alList(ix), ExLabInfo).RstVal
                            .Col = .GetColFromID("rstcmt") : .Text = CType(alList(ix), ExLabInfo).RSTCmt
                            .Col = .GetColFromID("testcd") : .Text = CType(alList(ix), ExLabInfo).TestCd
                            .Col = .GetColFromID("tnmd") : .Text = CType(alList(ix), ExLabInfo).Tnmd

                            .Col = .GetColFromID("prtbcno") : .Text = "*" + (New LISAPP.APP_DB.DbFn).GetViewToBCPrt(CType(alList(ix), ExLabInfo).BcNo.Replace("-", "")) + "*"

                            If CType(alList(ix), ExLabInfo).RegNo <> CType(alList(ix), ExLabInfo).CRegNo Then
                                .Col = .GetColFromID("remark") : .Text = "자료오류"
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            Else
                                If CType(alList(ix), ExLabInfo).SpcFlg = "4" Then

                                    Select Case CType(alList(ix), ExLabInfo).RstFlg
                                        Case "1"
                                            .Col = .GetColFromID("rstflg") : .Text = "△"
                                        Case "2"
                                            .Col = .GetColFromID("rstflg") : .Text = "○"
                                        Case "3"
                                            .Col = .GetColFromID("rstflg") : .Text = "◆"
                                            .ForeColor = Color.DarkGreen
                                        Case Else
                                            .Col = .GetColFromID("rstflg") : .Text = ""
                                            If CType(alList(ix), ExLabInfo).RstVal.IndexOf("별지통보") >= 0 Then
                                            Else
                                                .Col = .GetColFromID("chk") : .Text = "1"
                                            End If
                                    End Select
                                End If

                                If CType(alList(ix), ExLabInfo).SpcFlg = "2" Then
                                    .Col = .GetColFromID("remark") : .Text = "채혈"
                                    .Col = .GetColFromID("chk")
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                ElseIf CType(alList(ix), ExLabInfo).SpcFlg = "R" Then
                                    .Col = .GetColFromID("remark") : .Text = "취소"
                                    .Col = .GetColFromID("chk")
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                ElseIf CType(alList(ix), ExLabInfo).SpcFlg <> "4" Then
                                    .Col = .GetColFromID("remark") : .Text = "미채혈"
                                    .Col = .GetColFromID("chk")
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                End If
                            End If
                        End With
                    Next
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        Finally
            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing

        End Try

    End Sub

    Private Sub btnFN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFN.Click
        Select Case Me.tabExLab.SelectedTab.Text.Trim
            Case "[ TEXT 결과저장 ]"
                sbReg(3)
            Case "[ IMAGE 결과저장 ]"
                sbReg_Img("3")
        End Select
    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Select Case Me.tabExLab.SelectedTab.Text.Trim
            Case "[ TEXT 결과저장 ]"
                sbOpenFile_SCL_web()
                'sbOpenFile_SCL()
            Case "[ IMAGE 결과저장 ]"
                sbOpenImg_SCL()
        End Select
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Select Case Me.tabExLab.SelectedTab.Text.Trim
            Case "[ TEXT 결과저장 ]"
                sbToExcel()
            Case "[ IMAGE 결과저장 ]"
                sbToExcel_img()
        End Select
    End Sub

    Private Sub btnImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImage.Click
        Try


            With Me.spdResult_img
                For ix As Integer = 1 To .MaxRows
                    .Row = ix + 1

                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text
                    .Col = .GetColFromID("rstval") : Dim sFileNm As String = .Text
                    .Col = .GetColFromID("regno") : Dim sRegNo As String = .Text
                    .Col = .GetColFromID("patnm") : Dim sPatNm As String = .Text

                    Dim alFileNm As New ArrayList

                    For ix2 As Integer = 0 To m_al_FileList.Count - 1
                        Dim sBuf() As String = m_al_FileList(ix).ToString.Split("|"c)

                        If sBcNo + "^" + sTestcd = sBuf(0) Then alFileNm.Add(sBuf(1))
                    Next


                    If sChk = "1" Then

                        Dim iDisable As Integer = 0
                        Dim al_ChgRst As ArrayList = fnGet_Change_Rst("3", sBcNo, sTestcd, alFileNm, iDisable)
                        If al_ChgRst Is Nothing Or iDisable < 0 Then
                        Else
                            fnSaveImage(sBcNo, sTestcd, sPatNm)
                        End If
                    End If

                Next
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class

Public Class ExLabInfo_SCL
    Public ExLabDate As String = ""
    Public RegNo As String = ""
    Public BcNo As String = ""
    Public PatNm As String = ""
    Public TestCd As String = ""
    Public RstVal As String = ""
    Public RSTCmt As String = ""
    Public SpcCd As String = ""
    Public SpcFlg As String = ""
    Public RstFlg As String = ""
    Public Tnmd As String = ""
    Public OldRst As String = ""
    Public CRegNo As String = ""
End Class





















