Imports COMMON.SVar
Imports COMMON.CommFN
Imports COMMON.CommFN.Fn
Imports COMMON.CommPrint
Imports COMMON.CommLogin.LOGIN

Imports System.Drawing
Imports System.Drawing.Printing

Public Class PPRINT
    Private Const msFile As String = "File : TMPPRINT.vb, Class : TMPPRINT" + vbTab

    Private ma_PrtData As New ArrayList
    Private miLeftPos As Integer = 0
    Private miTopPos As Integer = 0
    Private mi_Copy As Integer = 1
    Private mb_First As Boolean = False

    Public Overridable Function BarCodePrtOut(ByVal ra_PrtData As ArrayList, _
                                              ByVal rsPrintPort As String, ByVal rsSocketIP As String, ByVal rbFirst As Boolean, _
                                              Optional ByVal riLeftPos As Integer = 0, _
                                              Optional ByVal riTopPos As Integer = 0, _
                                              Optional ByVal rsBarType As String = "CODABAR") As Boolean
        Dim sFn As String = "BarCodePrtOut"

        Try
            ma_PrtData = ra_PrtData
            miLeftPos = riLeftPos
            miTopPos = riTopPos
            mb_First = rbFirst

            Dim prtR As New PrintDocument

            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtR.DocumentName = "BARPRINT"
            prtR.PrinterSettings.PrinterName = rsPrintPort

            AddHandler prtR.PrintPage, AddressOf sbPrintPage
            AddHandler prtR.BeginPrint, AddressOf sbPrintData
            AddHandler prtR.EndPrint, AddressOf sbReport

            prtR.Print()

            Return True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    Public Overridable Function BarCodePrtOut_BLD(ByVal ra_PrtData As ArrayList, ByVal riCopy As Integer, _
                                                  ByVal rsPrintPort As String, ByVal rsSocketIP As String, _
                                                  Optional ByVal riLeftPos As Integer = 0, _
                                                  Optional ByVal riTopPos As Integer = 0) As Boolean
        Dim sFn As String = "BarCodePrtOut_BLD"

        Try
            ma_PrtData = ra_PrtData
            miLeftPos = riLeftPos
            miTopPos = riTopPos
            mi_Copy = riCopy

            Dim prtR As New PrintDocument

            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtR.DocumentName = "BLDPRINT"
            prtR.PrinterSettings.PrinterName = rsPrintPort

            AddHandler prtR.PrintPage, AddressOf sbPrintPage_blood
            AddHandler prtR.BeginPrint, AddressOf sbPrintData
            AddHandler prtR.EndPrint, AddressOf sbReport

            prtR.Print()

            Return True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return False
        End Try

    End Function


    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim sFn As String = "sbPrintPage"

        Try
            e.Graphics.PageUnit = GraphicsUnit.Millimeter

            Dim fnt_1 As New Font("굴림체", 6, FontStyle.Regular)
            Dim fnt_2 As New Font("굴림체", 7, FontStyle.Regular)
            Dim fnt_3 As New Font("굴림체", 8, FontStyle.Regular)
            Dim fnt_4 As New Font("굴림체", 10, FontStyle.Bold)
            Dim fnt_5 As New Font("굴림체", 11, FontStyle.Bold)
            Dim fnt_6 As New Font("굴림체", 13, FontStyle.Bold)

            Dim fnt_b3 As New Font("굴림체", 9, FontStyle.Bold)
            Dim fnt_b5 As New Font("굴림체", 11, FontStyle.Bold)

            Dim fnt_1_u As New Font("굴림체", 6, FontStyle.Underline)
            Dim fnt_5_u As New Font("굴림체", 11, FontStyle.Underline)
            Dim fnt_6_u As New Font("굴림체", 13, FontStyle.Underline)

            'Dim fnt_BarCd As New Font("Code39(2:3)", 24, FontStyle.Regular)
            'Dim fnt_BarCd As New Font("Code39One", 16, FontStyle.Regular)
            Dim fnt_BarCd As New Font("Free 3 of 9", 24, FontStyle.Regular)

            Dim sf_c As New Drawing.StringFormat
            Dim sf_l As New Drawing.StringFormat
            Dim sf_r As New Drawing.StringFormat

            sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
            sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
            sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far


            Dim intCnt As Integer = 0

            For ix1 As Integer = 0 To ma_PrtData.Count - 1
                If CType(ma_PrtData(ix1), STU_BCPRTINFO).REGNO <> "" Then
                    Dim iPrtCnt As Integer = 1

                    If CType(ma_PrtData(ix1), STU_BCPRTINFO).BCCNT = "A" Then
                        iPrtCnt = 2
                    ElseIf CType(ma_PrtData(ix1), STU_BCPRTINFO).BCCNT = "B" Then
                        '< CrossMatching 검체
                        iPrtCnt = 3
                    ElseIf IsNumeric(CType(ma_PrtData(ix1), STU_BCPRTINFO).BCCNT) Then
                        iPrtCnt = Convert.ToInt32(CType(ma_PrtData(ix1), STU_BCPRTINFO).BCCNT)
                    End If

                    For ix2 As Integer = 1 To iPrtCnt
                        Dim bpi As STU_BCPRTINFO = CType(ma_PrtData(ix1), STU_BCPRTINFO)

                        intCnt += 1
                        Dim intPosX As Integer = ((intCnt - 1) Mod 3)
                        Dim intPosY As Integer = intCnt / 3

                        If (intCnt * intPosY) + ((intCnt - 1) Mod 3) <> intCnt Then intPosY -= 1
                        If intPosY < 0 Then intPosY = 0

                        Dim sgLeft As Single = miLeftPos + 2 + 60 * intPosX
                        Dim sgTop As Single = miTopPos + 1 + 35 * intPosY

                        Dim rect As New Drawing.RectangleF
                        Dim sTmp As String = ""

                        rect = New Drawing.RectangleF(miLeftPos, miTopPos, 50, 35)
                        e.Graphics.DrawRectangle(Drawing.Pens.Black, miLeftPos, miTopPos, 50, 35)

                        '-- 검체번호
                        rect = New Drawing.RectangleF(sgLeft, sgTop, 25, 2)
                        e.Graphics.DrawString(bpi.BCNO, fnt_1, Drawing.Brushes.Black, rect, sf_l)

                        '-- 발행일시
                        rect = New Drawing.RectangleF(sgLeft + 33, sgTop, 20, 2)
                        If mb_First Then
                            e.Graphics.DrawString(Fn.GetServerDateTime.ToString("MM-dd HH:mm"), fnt_1, Drawing.Brushes.Black, rect, sf_l)
                        Else
                            e.Graphics.DrawString(Fn.GetServerDateTime.ToString("MM-dd HH:mm"), fnt_1_u, Drawing.Brushes.Black, rect, sf_l)
                        End If

                        '< 감염정보  
                        'bpi.INFINFO = "A/MRAS"
                        rect = New Drawing.RectangleF(sgLeft + 32, sgTop + 30, 15, 4)
                        e.Graphics.DrawString(bpi.INFINFO, fnt_b5, Drawing.Brushes.Black, rect, sf_l)

                        'Dim a_sInfInfo As String() = bpi.INFINFO.Split("/"c)

                        'For iCnt As Integer = 0 To a_sInfInfo.Length - 1
                        '    If iCnt > 1 Then Exit For

                        '    rect = New Drawing.RectangleF(sgLeft + 35, sgTop + 5 + 4 * iCnt, 5, 4)
                        '    e.Graphics.DrawString(a_sInfInfo(iCnt).ToString().Trim, fnt_b5, Drawing.Brushes.Black, rect, sf_l)
                        'Next

                        Dim sTestNm As String = bpi.TESTNMS.Trim

                        If sTestNm.Length > 20 Then
                            sTestNm = sTestNm.Substring(0, 19) & "..."
                        End If

                        If sTestNm.IndexOf("...") > -1 Then
                            If sTestNm.Substring(0, sTestNm.IndexOf("...")).Length > 20 Then
                                sTestNm = sTestNm.Substring(0, 19) & "..."
                            End If
                        End If

                        '< 바코드  
                        If bpi.BCNOPRT <> "" Then
                            rect = New Drawing.RectangleF(sgLeft, sgTop + 0, 500, 16)
                            e.Graphics.DrawString("*" + bpi.BCNOPRT.Substring(0, 10) + "*", fnt_BarCd, Drawing.Brushes.Black, rect, sf_l)

                            rect = New Drawing.RectangleF(sgLeft + 10, sgTop + 16, 25, 2)
                            e.Graphics.DrawString(bpi.BCNOPRT, fnt_2, Drawing.Brushes.Black, rect, sf_c)
                        Else
                            rect = New Drawing.RectangleF(sgLeft + 10, sgTop + 3, 35, 13)
                            e.Graphics.DrawString("미채혈바코드", fnt_6, Drawing.Brushes.Black, rect, sf_c)

                            rect = New Drawing.RectangleF(sgLeft, sgTop + 13, 500, 16)
                            e.Graphics.DrawString("*" + bpi.REGNO + "*", fnt_BarCd, Drawing.Brushes.Black, rect, sf_l)

                        End If

                        ''< 등록번호 sPID
                        rect = New Drawing.RectangleF(sgLeft, sgTop + 19, 22, 5)
                        If PRG_CONST.BCCLS_ExLab.Contains(bpi.BCNO) Then
                            e.Graphics.DrawString(bpi.REGNO, fnt_6_u, Drawing.Brushes.Black, rect, sf_l)
                        Else
                            e.Graphics.DrawString(bpi.REGNO, fnt_6, Drawing.Brushes.Black, rect, sf_l)
                        End If

                        ''< 진료과/병동/병실  
                        rect = New Drawing.RectangleF(sgLeft + 38, sgTop + 23, 20, 2)
                        e.Graphics.DrawString(bpi.DEPTWARD, fnt_2, Drawing.Brushes.Black, rect, sf_l)

                        ''< 성별/나이 
                        rect = New Drawing.RectangleF(sgLeft + 40, sgTop + 19, 10, 1.5)
                        e.Graphics.DrawString(bpi.SEXAGE, fnt_1, Drawing.Brushes.Black, rect, sf_l)

                        ''< 환자명 
                        rect = New Drawing.RectangleF(sgLeft + 23, sgTop + 18, 15, 4)
                        e.Graphics.DrawString(bpi.PATNM, fnt_5, Drawing.Brushes.Black, rect, sf_l)

                        ''< sRemark
                        ''sRemark = "C"
                        rect = New Drawing.RectangleF(sgLeft + 48, sgTop + 13, 10, 2)
                        e.Graphics.DrawString(IIf(bpi.REMARK = "", "", "C").ToString, fnt_2, Drawing.Brushes.Black, rect, sf_l)


                        If bpi.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Or bpi.BCCNT = "B" Then
                            '< 용기명 
                            rect = New Drawing.RectangleF(sgLeft, sgTop + 25, 20, 4)
                            e.Graphics.DrawString(bpi.TUBENM, fnt_4, Drawing.Brushes.Black, rect, sf_l)

                            '< 채혈자
                            rect = New Drawing.RectangleF(sgLeft + 25, sgTop + 25, 10, 4)
                            e.Graphics.DrawString("채혈자:", fnt_4, Drawing.Brushes.Black, rect, sf_l)
                            '< 확인자
                            rect = New Drawing.RectangleF(sgLeft + 25, sgTop + 29, 10, 4)
                            e.Graphics.DrawString("확인자:", fnt_4, Drawing.Brushes.Black, rect, sf_l)
                            '< 음영
                            e.Graphics.DrawRectangle(Pens.Black, New Drawing.Rectangle(sgLeft, sgTop + 29, 20, 4))
                            rect = New Drawing.RectangleF(sgLeft, sgTop + 29, 20, 4)
                            e.Graphics.DrawString("X-Matching", fnt_4, Drawing.Brushes.Black, rect, sf_l)

                            'ElseIf bpi.BCCLSCD = PRG_CONST.BCCLS_BloodBank Then
                            '    '< 용기명 
                            '    rect = New Drawing.RectangleF(sgLeft, sgTop + 25, 20, 4)
                            '    e.Graphics.DrawString(bpi.TUBENM, fnt_4, Drawing.Brushes.Black, rect, sf_l)
                            '    '< 채혈자
                            '    rect = New Drawing.RectangleF(sgLeft + 25, sgTop + 25, 10, 4)
                            '    e.Graphics.DrawString("채혈자:", fnt_4, Drawing.Brushes.Black, rect, sf_l)
                            '    '< 확인자
                            '    rect = New Drawing.RectangleF(sgLeft + 25, sgTop + 29, 10, 4)
                            '    e.Graphics.DrawString("확인자:", fnt_4, Drawing.Brushes.Black, rect, sf_l)
                            '    '< 검사항목(음영)
                            '    If sTestNm.Length > 12 Then sTestNm = sTestNm.Substring(0, 12)
                            '    rect = New Drawing.RectangleF(sgLeft, sgTop + 29, 10, 4)
                            '    e.Graphics.DrawString(sTestNm, fnt_3, Drawing.Brushes.Black, rect, sf_l)

                        Else
                            If bpi.BCTYPE = "M" Then
                                '< 검사그룹 sComment2
                                rect = New Drawing.RectangleF(sgLeft + 30, sgTop + 27, 20, 4)
                                e.Graphics.DrawString(bpi.TGRPNM, fnt_b3, Drawing.Brushes.Black, rect, sf_l)

                                '< 미생물 검체번호
                                rect = New Drawing.RectangleF(sgLeft, sgTop + 28, 40, 4)
                                e.Graphics.DrawString(bpi.BCNO_MB, fnt_4, Drawing.Brushes.Black, rect, sf_l)
                            Else
                                '< 검사항목명 
                                rect = New Drawing.RectangleF(sgLeft + 5, sgTop + 30, 40, 4)
                                e.Graphics.DrawString(sTestNm, fnt_3, Drawing.Brushes.Black, rect, sf_l)

                                '< 용기명 
                                rect = New Drawing.RectangleF(sgLeft + 23, sgTop + 25, 10, 4)
                                e.Graphics.DrawString(bpi.TUBENM, fnt_4, Drawing.Brushes.Black, rect, sf_l)

                                '< 검사그룹 sComment2
                                'bpi.TGRPNM = "H C E"
                                rect = New Drawing.RectangleF(sgLeft + 30, sgTop + 25, 10, 4)
                                e.Graphics.DrawString(bpi.TGRPNM, fnt_b3, Drawing.Brushes.Black, rect, sf_l)

                                '< 응급 sEmer 
                                'bpi.EMER = "E"
                                rect = New Drawing.RectangleF(sgLeft + 45, sgTop + 8, 5, 2)
                                e.Graphics.DrawString(bpi.EMER, fnt_2, Drawing.Brushes.Black, rect, sf_l)

                                '< 계 sKind
                                rect = New Drawing.RectangleF(sgLeft, sgTop + 28, 5, 4)
                                e.Graphics.DrawString(bpi.BCCLSCD, fnt_b3, Drawing.Brushes.Black, rect, sf_l)
                            End If
                        End If

                        ''< 검체명
                        rect = New Drawing.RectangleF(sgLeft, sgTop + 25, 10, 4)
                        e.Graphics.DrawString(bpi.SPCNM, fnt_4, Drawing.Brushes.Black, rect, sf_l)
                    Next
                End If
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Public Overridable Sub sbPrintPage_blood(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim sFn As String = "sbPrintPage_blood"

        Try
            e.Graphics.PageUnit = GraphicsUnit.Millimeter

            Dim fnt_1 As New Font("굴림체", 6, FontStyle.Regular)
            Dim fnt_2 As New Font("굴림체", 7, FontStyle.Regular)
            Dim fnt_3 As New Font("굴림체", 8, FontStyle.Regular)
            Dim fnt_4 As New Font("굴림체", 10, FontStyle.Bold)
            Dim fnt_5 As New Font("굴림체", 12, FontStyle.Bold)
            Dim fnt_6 As New Font("굴림체", 15, FontStyle.Bold)

            Dim fnt_b2 As New Font("굴림체", 9, FontStyle.Bold)
            Dim fnt_b3 As New Font("굴림체", 10, FontStyle.Bold)
            Dim fnt_b5 As New Font("굴림체", 14, FontStyle.Bold)

            Dim fnt_BarCd As New Font("Code39(2:3)", 10, FontStyle.Regular)

            Dim sf_c As New Drawing.StringFormat
            Dim sf_l As New Drawing.StringFormat
            Dim sf_r As New Drawing.StringFormat

            sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
            sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
            sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

            Dim sgPosX(0 To 4) As Single
            Dim sgPosY(0 To 8) As Single

            For ix1 As Integer = 0 To ma_PrtData.Count - 1

                For ix2 As Integer = 1 To mi_Copy

                    Dim sgLeft As Single = 5 + miLeftPos
                    Dim sgTop As Single = 5 + miTopPos

                    sgPosX(0) = sgLeft
                    sgPosX(1) = sgPosX(0) + 18
                    sgPosX(2) = sgPosX(1) + 22
                    sgPosX(3) = sgPosX(2) + 18
                    sgPosX(4) = sgPosX(3) + 22

                    sgPosY(0) = sgTop
                    sgPosY(1) = sgPosY(0) + 3

                    For ix As Integer = 1 To sgPosY.Length - 2
                        sgPosY(ix) = sgPosY(ix - 1) + 8
                    Next
                    sgPosY(sgPosY.Length - 1) = sgPosY(sgPosY.Length - 2) + 5

                    Dim sgPrtH As Single = 61

                    For ix3 As Integer = 0 To 2
                        ''-- 세로
                        'For ix = 0 To sgPosX.Length - 1
                        '    If ix = 0 Or ix = sgPosX.Length - 1 Then
                        '        e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(ix), sgPosY(1) + (ix3 * sgPrtH), sgPosX(ix), sgPosY(sgPosY.Length - 1) + (ix3 * sgPrtH))
                        '    Else
                        '        e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(ix), sgPosY(1) + (ix3 * sgPrtH), sgPosX(ix), sgPosY(sgPosY.Length - 2) + (ix3 * sgPrtH))
                        '    End If
                        'Next

                        ''-- 가로
                        'For ix = 1 To sgPosY.Length - 1
                        '    e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(0), sgPosY(ix) + (ix3 * sgPrtH), sgPosX(sgPosX.Length - 1), sgPosY(ix) + (ix3 * sgPrtH))
                        'Next

                        Dim bpi As STU_BLDLABEL = CType(ma_PrtData(ix1), STU_BLDLABEL)

                        Dim rect As New Drawing.RectangleF

                        ''-- 환자혈액형
                        'rect = New Drawing.RectangleF(sgPosX(0), sgPosY(1) + (ix3 * sgPrtH), sgPosX(1) - sgPosX(0), sgPosY(2) - sgPosY(1))
                        'e.Graphics.DrawString("환자혈액형", fnt_1, Drawing.Brushes.Black, rect, sf_c)
                        ''-- 적합혈액
                        'rect = New Drawing.RectangleF(sgPosX(2), sgPosY(1) + (ix3 * sgPrtH), sgPosX(3) - sgPosX(2), sgPosY(2) - sgPosY(1))
                        'e.Graphics.DrawString("적합혈액", fnt_1, Drawing.Brushes.Black, rect, sf_c)

                        ''-- 등록번호
                        'rect = New Drawing.RectangleF(sgPosX(0), sgPosY(2) + (ix3 * sgPrtH), sgPosX(1) - sgPosX(0), sgPosY(3) - sgPosY(2))
                        'e.Graphics.DrawString("등록번호", fnt_1, Drawing.Brushes.Black, rect, sf_c)
                        ''-- 성    명
                        'rect = New Drawing.RectangleF(sgPosX(2), sgPosY(2) + (ix3 * sgPrtH), sgPosX(3) - sgPosX(2), sgPosY(3) - sgPosY(2))
                        'e.Graphics.DrawString("성    명", fnt_1, Drawing.Brushes.Black, rect, sf_c)

                        ''-- 성별/나이
                        'rect = New Drawing.RectangleF(sgPosX(0), sgPosY(3) + (ix3 * sgPrtH), sgPosX(1) - sgPosX(0), sgPosY(4) - sgPosY(3))
                        'e.Graphics.DrawString("성별/나이", fnt_1, Drawing.Brushes.Black, rect, sf_c)
                        ''-- 과/병동
                        'rect = New Drawing.RectangleF(sgPosX(2), sgPosY(3) + (ix3 * sgPrtH), sgPosX(3) - sgPosX(2), sgPosY(4) - sgPosY(3))
                        'e.Graphics.DrawString("과/병동", fnt_1, Drawing.Brushes.Black, rect, sf_c)

                        ''-- 혈액제제명
                        'rect = New Drawing.RectangleF(sgPosX(0), sgPosY(4) + (ix3 * sgPrtH), sgPosX(1) - sgPosX(0), sgPosY(5) - sgPosY(4))
                        'e.Graphics.DrawString("혈액제제명", fnt_1, Drawing.Brushes.Black, rect, sf_c)
                        ''-- 혈액번호
                        'rect = New Drawing.RectangleF(sgPosX(2), sgPosY(4) + (ix3 * sgPrtH), sgPosX(3) - sgPosX(2), sgPosY(5) - sgPosY(4))
                        'e.Graphics.DrawString("혈액번호", fnt_1, Drawing.Brushes.Black, rect, sf_c)

                        ''-- 검사일시
                        'rect = New Drawing.RectangleF(sgPosX(0), sgPosY(5) + (ix3 * sgPrtH), sgPosX(1) - sgPosX(0), sgPosY(6) - sgPosY(5))
                        'e.Graphics.DrawString("검사일시", fnt_1, Drawing.Brushes.Black, rect, sf_c)
                        ''-- 검사자
                        'rect = New Drawing.RectangleF(sgPosX(2), sgPosY(5) + (ix3 * sgPrtH), sgPosX(3) - sgPosX(2), sgPosY(6) - sgPosY(5))
                        'e.Graphics.DrawString("검 사 자", fnt_1, Drawing.Brushes.Black, rect, sf_c)

                        ''-- 출고일시
                        'rect = New Drawing.RectangleF(sgPosX(0), sgPosY(6) + (ix3 * sgPrtH), sgPosX(1) - sgPosX(0), sgPosY(7) - sgPosY(6))
                        'e.Graphics.DrawString("수령일시", fnt_1, Drawing.Brushes.Black, rect, sf_c)
                        ''-- 출고자
                        'rect = New Drawing.RectangleF(sgPosX(2), sgPosY(6) + (ix3 * sgPrtH), sgPosX(3) - sgPosX(2), sgPosY(7) - sgPosY(6))
                        'e.Graphics.DrawString("출 고 자", fnt_1, Drawing.Brushes.Black, rect, sf_c)



                        '-- 환자혈액형
                        rect = New Drawing.RectangleF(sgPosX(1), sgPosY(1) + (ix3 * sgPrtH), sgPosX(2) - sgPosX(1), sgPosY(2) - sgPosY(1))
                        e.Graphics.DrawString(bpi.PAT_ABORH, fnt_b5, Drawing.Brushes.Black, rect, sf_c)
                        '-- 출고혈액형
                        rect = New Drawing.RectangleF(sgPosX(3), sgPosY(1) + (ix3 * sgPrtH), sgPosX(4) - sgPosX(3), sgPosY(2) - sgPosY(1))
                        e.Graphics.DrawString(bpi.BLD_ABORH, fnt_b5, Drawing.Brushes.Black, rect, sf_c)

                        '-- 등록번호
                        rect = New Drawing.RectangleF(sgPosX(1), sgPosY(2) + (ix3 * sgPrtH), sgPosX(2) - sgPosX(1), sgPosY(3) - sgPosY(2))
                        e.Graphics.DrawString(bpi.REGNO, fnt_b3, Drawing.Brushes.Black, rect, sf_c)
                        '-- 성명
                        rect = New Drawing.RectangleF(sgPosX(3), sgPosY(2) + (ix3 * sgPrtH), sgPosX(4) - sgPosX(3), sgPosY(3) - sgPosY(2))
                        e.Graphics.DrawString(bpi.PATNM, fnt_b3, Drawing.Brushes.Black, rect, sf_c)

                        '-- 성별/나이
                        rect = New Drawing.RectangleF(sgPosX(1), sgPosY(3) + (ix3 * sgPrtH), sgPosX(2) - sgPosX(1), sgPosY(4) - sgPosY(3))
                        e.Graphics.DrawString(bpi.SEXAGE, fnt_4, Drawing.Brushes.Black, rect, sf_c)
                        '-- 진료과
                        rect = New Drawing.RectangleF(sgPosX(3), sgPosY(3) + (ix3 * sgPrtH), sgPosX(4) - sgPosX(3), sgPosY(4) - sgPosY(3))
                        e.Graphics.DrawString(bpi.DEPTWARD, fnt_4, Drawing.Brushes.Black, rect, sf_c)

                        '-- 혈액제제명
                        rect = New Drawing.RectangleF(sgPosX(1), sgPosY(4) + (ix3 * sgPrtH), sgPosX(2) - sgPosX(1), sgPosY(5) - sgPosY(4))
                        e.Graphics.DrawString(bpi.COMNM, fnt_b3, Drawing.Brushes.Black, rect, sf_c)
                        '-- 혈액번호
                        rect = New Drawing.RectangleF(sgPosX(3), sgPosY(4) + (ix3 * sgPrtH), sgPosX(4) - sgPosX(3), sgPosY(5) - sgPosY(4))
                        e.Graphics.DrawString(bpi.BLDNO(0), fnt_b2, Drawing.Brushes.Black, rect, sf_c)

                        '-- 검사일시
                        rect = New Drawing.RectangleF(sgPosX(1), sgPosY(5) + (ix3 * sgPrtH), sgPosX(2) - sgPosX(1), sgPosY(6) - sgPosY(5))
                        e.Graphics.DrawString(bpi.TESTDT, fnt_3, Drawing.Brushes.Black, rect, sf_c)
                        '-- 검사자
                        rect = New Drawing.RectangleF(sgPosX(3), sgPosY(5) + (ix3 * sgPrtH), sgPosX(2) - sgPosX(1), sgPosY(6) - sgPosY(5))
                        e.Graphics.DrawString(bpi.TESTNM, fnt_4, Drawing.Brushes.Black, rect, sf_c)

                        '-- 출고일시
                        rect = New Drawing.RectangleF(sgPosX(1), sgPosY(6) + (ix3 * sgPrtH), sgPosX(2) - sgPosX(1), sgPosY(7) - sgPosY(6))
                        e.Graphics.DrawString(bpi.OUTDT, fnt_3, Drawing.Brushes.Black, rect, sf_c)
                        '-- 출고자
                        rect = New Drawing.RectangleF(sgPosX(3), sgPosY(6) + (ix3 * sgPrtH), sgPosX(4) - sgPosX(3), sgPosY(7) - sgPosY(6))
                        e.Graphics.DrawString(bpi.OUTNM, fnt_4, Drawing.Brushes.Black, rect, sf_c)


                        ''-- 병원명
                        'rect = New Drawing.RectangleF(sgPosX(0), sgPosY(7) + (ix3 * sgPrtH), sgPosX(sgPosX.Length - 1) - sgPosX(0), sgPosY(8) - sgPosY(7))
                        'e.Graphics.DrawString(PRG_CONST.Tail_WorkList + " 혈액은행", fnt_b3, Drawing.Brushes.Black, rect, sf_c)

                    Next
                    
                Next
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

End Class
