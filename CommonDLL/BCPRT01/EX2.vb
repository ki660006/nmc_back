Imports COMMON.SVar
Imports COMMON.CommFN
Imports COMMON.CommFN.Fn
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommPrint

Public Class EX2
    Private Const mcFile$ = "File : EX2.vb, Class : EX2" + vbTab

    Public Overridable Function BarCodePrtOut(ByVal ra_PrtData As ArrayList, _
                                              ByVal rsPrintPort As String, ByVal rsSocketIP As String, ByVal rbFirst As Boolean, _
                                              Optional ByVal riLeftPos As Integer = 0, _
                                              Optional ByVal riTopPos As Integer = 0, _
                                              Optional ByVal rsBarType As String = "CODABAR") As Boolean
        Dim sFn As String = "BarCodePrtOut"
        Dim bReturn As Boolean = False
        Dim iFileNo As Integer = 0

        Try
            For ix1 As Integer = 0 To ra_PrtData.Count - 1
                If CType(ra_PrtData(ix1), STU_BCPRTINFO).REGNO <> "" Then
                    Dim sPrtMsg = fnMakePrtMsg(CType(ra_PrtData(ix1), STU_BCPRTINFO), rbFirst, riLeftPos, riTopPos, rsBarType)
                    Dim iPrtCnt As Integer = 1

                    If CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "A" Then
                        iPrtCnt = 2
                    ElseIf CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "B" Then
                        '< CrossMatching 검체
                        iPrtCnt = 3
                    ElseIf IsNumeric(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT) Then
                        iPrtCnt = Convert.ToInt32(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT)
                    End If

                    For ix2 As Integer = 1 To iPrtCnt
                        If rsPrintPort.Trim() = "" Then
                            Dim objSkt As New TCP01.SendSocket

                            objSkt.sbConnectCliSocketToSvrSocket(rsSocketIP, 13734)

                            If objSkt.fnSendMsgOneConn("ITM", sPrtMsg) Then
                                bReturn = True
                            End If

                            objSkt.sbDispose()

                            Dim strFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                            iFileNo = FreeFile()
                            Try
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)

                            Catch ex As Exception
                                strFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)
                            End Try

                            Print(iFileNo, sPrtMsg)
                            FileClose(iFileNo)


                        Else
                            Dim strFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                            iFileNo = FreeFile()
                            Try
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)

                            Catch ex As Exception
                                strFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)
                            End Try

                            Print(iFileNo, sPrtMsg)
                            FileClose(iFileNo)

                            Process.Start("cmd.exe", "/C TYPE " + strFileNm + " > " + rsPrintPort.Trim())
                        End If

                        Threading.Thread.Sleep(CInt(sPrtMsg.Length * 1.5))
                    Next
                End If
            Next

            Return True
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
            Return False
        Finally
            FileClose(iFileNo)

        End Try
    End Function

    Public Overridable Function BarCodePrtOut_BLD(ByVal roSndMsg As ArrayList, ByVal riPrtCnt As Integer, _
                                        Optional ByVal rsIP As String = "127.0.0.1", _
                                            Optional ByVal riPort As Integer = 9100, _
                                              Optional ByVal rsOUTPUT As String = "", _
                                                   Optional ByVal rsLeftPos As String = "0", Optional ByVal rsTopPos As String = "0", _
                                                    Optional ByVal rbFirst As Boolean = False) As Boolean
        Dim sFn As String = "BarCodePrtOut_BLD"
        Dim bReturn As Boolean = False
        Dim iFileNo As Integer = 0

        Try

            If roSndMsg Is Nothing Then
            Else
                Dim sPrtMsg As String = fnMakePrtMsg_BLD(CType(roSndMsg(0), STU_BLDLABEL))

                If rsOUTPUT.Trim() = "" Then
                    Dim objSkt As New TCP01.SendSocket

                    objSkt.sbConnectCliSocketToSvrSocket(rsIP, 13734)

                    If objSkt.fnSendMsgOneConn("ITM", sPrtMsg) Then
                        bReturn = True
                    End If

                    objSkt.sbDispose()
                Else
                    Dim strFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                    iFileNo = FreeFile()
                    Try
                        FileOpen(iFileNo, strFileNm, OpenMode.Output)

                    Catch ex As Exception
                        strFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                        FileOpen(iFileNo, strFileNm, OpenMode.Output)
                    End Try

                    Print(iFileNo, sPrtMsg)
                    FileClose(iFileNo)

                    Process.Start("cmd.exe", "/C TYPE " + strFileNm + " > " + rsOUTPUT.Trim())
                End If

                Threading.Thread.Sleep(1000)
            End If


            Return True
        Catch ioex As System.IO.IOException
            ''파일은 다른 프로세스에서 사용 중이므로 프로세스에서 액세스할 수 없습니다.
            'If Err.Number = 75 Then
            '    'Recursive Call
            '    BarCodePrtOut(asSndMsg, aiPrtCnt, asIP, aiPort)
            'End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
            Return False
        Finally
            FileClose(iFileNo)

        End Try
    End Function

    Public Overridable Function BarCodePrtOut_PIS(ByVal ra_PrtData As ArrayList, _
                                              ByVal rsPrintPort As String, ByVal rsSocketIP As String, ByVal rbFirst As Boolean, _
                                              Optional ByVal riLeftPos As Integer = 0, _
                                              Optional ByVal riTopPos As Integer = 0, _
                                              Optional ByVal rsBarType As String = "CODABAR") As Boolean
        Dim sFn As String = "BarCodePrtOut"
        Dim bReturn As Boolean = False
        Dim iFileNo As Integer = 0

        Try
            For ix1 As Integer = 0 To ra_PrtData.Count - 1
                If CType(ra_PrtData(ix1), STU_BCPRTINFO).REGNO <> "" Then

                    Dim iPrtCnt As Integer = Convert.ToInt32(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT)

                    For ix2 As Integer = 1 To iPrtCnt

                        '-- 검체갯수(병리인 경우)
                        CType(ra_PrtData(ix1), STU_BCPRTINFO).REMARK = ix2.ToString + "/" + CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT

                        Dim sPrtMsg = fnMakePrtMsg_PIS(CType(ra_PrtData(ix1), STU_BCPRTINFO), rbFirst, riLeftPos, riTopPos, rsBarType)

                        If rsPrintPort.Trim() = "" Then
                            Dim objSkt As New TCP01.SendSocket

                            objSkt.sbConnectCliSocketToSvrSocket(rsSocketIP, 13734)

                            If objSkt.fnSendMsgOneConn("ITM", sPrtMsg) Then
                                bReturn = True
                            End If

                            objSkt.sbDispose()

                            Dim strFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                            iFileNo = FreeFile()
                            Try
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)

                            Catch ex As Exception
                                strFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)
                            End Try

                            Print(iFileNo, sPrtMsg)
                            FileClose(iFileNo)


                        Else
                            Dim strFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                            iFileNo = FreeFile()
                            Try
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)

                            Catch ex As Exception
                                strFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)
                            End Try

                            Print(iFileNo, sPrtMsg)
                            FileClose(iFileNo)

                            Process.Start("cmd.exe", "/C TYPE " + strFileNm + " > " + rsPrintPort.Trim())
                        End If

                        Threading.Thread.Sleep(CInt(sPrtMsg.Length * 1.5))
                    Next
                End If
            Next

            Return True
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
            Return False
        Finally
            FileClose(iFileNo)

        End Try
    End Function

    Protected Overridable Function fnMakePrtMsg(ByVal ro_Data As STU_BCPRTINFO, _
                                                ByVal rbFirst As Boolean, _
                                                ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                                ByVal rsBarType As String) As String
        Dim sFn As String = "fnMakePrtMsg"

        Try

            Dim a_sInfInfo As String() = ro_Data.INFINFO.Split("/"c)
            Dim sTestNms As String = ro_Data.TESTNMS

            If ro_Data.TESTNMS.Length > 35 Then
                sTestNms = ro_Data.TESTNMS.Substring(0, 25) + "..."
            End If

            If sTestNms.IndexOf("...") > -1 Then
                If sTestNms.Substring(0, sTestNms.IndexOf("...")).Length > 35 Then
                    sTestNms = sTestNms.Substring(0, 35) + "..."
                End If
            End If

            If ro_Data.PATNM.Length > 4 Then ro_Data.PATNM = ro_Data.PATNM.Substring(0, 4)

            Dim sPrtBuf As String = ""
            Dim iHanCnt As Integer = 0

            sPrtBuf = ""
            sPrtBuf += Chr(2) + "qA" + vbCrLf       '-- Clear Memory
            sPrtBuf += Chr(2) + "XA" + vbCrLf       '-- Fonts
            sPrtBuf += Chr(2) + "m" + vbCrLf        '-- Metric Mode 
            sPrtBuf += Chr(2) + "f680" + vbCrLf     '-- Position(Backfedd)


            If ro_Data.BCNOPRT = "" Then
                Dim sTmp As String = "미채혈바코드"

                For ix As Integer = 0 To sTmp.Length - 1
                    sPrtBuf += Chr(2) + "ICF " + "IMG" + (ix + 1).ToString.PadLeft(2, "0"c) + Chr(13) + fnGet_FontImage(sTmp.Substring(ix, 1), New Font("굴림", 18, FontStyle.Bold), 32, 32)
                Next
            End If

            If ro_Data.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Or ro_Data.BCCNT = "B" Or PRG_CONST.BCCLS_BloodBank.StartsWith(ro_Data.BCCLSCD.Substring(0, 1)) Then
                Dim sTmp As String = "채혈자"

                For ix As Integer = 0 To sTmp.Length - 1
                    sPrtBuf += Chr(2) + "ICF " + "IMG" + (ix + 11).ToString.PadLeft(2, "0"c) + Chr(13) + fnGet_FontImage(sTmp.Substring(ix, 1), New Font("굴림", 16, FontStyle.Regular), 30, 30)
                Next

                sTmp = "확인자"

                For ix As Integer = 0 To sTmp.Length - 1
                    sPrtBuf += Chr(2) + "ICF " + "IMG" + (ix + 21).ToString.PadLeft(2, "0"c) + Chr(13) + fnGet_FontImage(sTmp.Substring(ix, 1), New Font("굴림", 16, FontStyle.Regular), 30, 30)
                Next

            End If

            For ix As Integer = 0 To ro_Data.PATNM.Length - 1
                iHanCnt += 1
                sPrtBuf += Chr(2) + "ICF " + "IMG" + (ix + 31).ToString.PadLeft(2, "0"c) + Chr(13) + fnGet_FontImage(ro_Data.PATNM.Substring(ix, 1), New Font("굴림", 18, FontStyle.Bold), 32, 32)
            Next

            sPrtBuf += Chr(2) + "L" + vbCrLf

            sPrtBuf += "D11" + vbCrLf               '-- Set Dot Size
            sPrtBuf += "H19" + vbCrLf               '-- Header Setting
            sPrtBuf += "R0000" + vbCrLf             '-- Set Row Offset Amount

            '< 검체번호 
            sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNO + vbCrLf

            ''< 바코드 발행 일시  233
            If rbFirst Then
                sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (610 + riLeftPos).ToString.PadLeft(4, "0"c) + Fn.GetServerDateTime.ToString("yyyy-MM-dd HH:mm") + vbCrLf
            Else
                sPrtBuf += "A5" + vbCrLf
                sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (610 + riLeftPos).ToString.PadLeft(4, "0"c) + Fn.GetServerDateTime.ToString("yyyy-MM-dd HH:mm") + vbCrLf
                sPrtBuf += "A1" + vbCrLf
            End If

            '< 감염정보  

            For iCnt As Integer = 0 To a_sInfInfo.Length - 1
                If iCnt > 1 Then Exit For
                sPrtBuf += "1" + "9" + "2" + "2" + "001" + (190 - (iCnt * 35)).ToString.PadLeft(4, "0"c) + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + a_sInfInfo(iCnt).ToString() + vbCrLf
            Next

            '< 바코드  
            If ro_Data.BCNOPRT <> "" Then
                ' CODAR BAR 
                'sPrtBuf += "1" + "I" + "3" + "2" + "120" + "0115" + (350 + riLeftPos).ToString.PadLeft(4, "0"c) + "A" + ro_Data.BCNOPRT + "A" + vbCrLf
                sPrtBuf += "1" + "I" + "4" + "2" + "120" + "0120" + (370 + riLeftPos).ToString.PadLeft(4, "0"c) + "A" + ro_Data.BCNOPRT + "A" + vbCrLf

                ' code128
                'sPrtBuf += "1" + "e" + "1" + "2" + "125" + "0135" + (340 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNOPRT + vbCrLf

                '< 바코드 번호
                'sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0110" + (470 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNOPRT + vbCrLf
            Else
                '< 미수납 바코드 
                For ix As Integer = 0 To "미채혈바코드".Length - 1
                    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0180" + (400 + (ix * 32) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 1).ToString.PadLeft(2, "0"c) + vbCrLf
                Next
                '>  
            End If

            '< 등록번호 sPID
            If ro_Data.BCCLSCD.Substring(0, 1).Trim() = "R" Then
                sPrtBuf += "A5" + vbCrLf
                sPrtBuf += "1" + "9" + "1" + "2" + "003" + "0075" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.REGNO + vbCrLf
                sPrtBuf += "A1" + vbCrLf
            Else
                sPrtBuf += "1" + "9" + "1" + "2" + "002" + "0075" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.REGNO + vbCrLf
            End If

            ''< 진료과/병동/병실
            'sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0090" + (650 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.DEPTWARD + vbCrLf
            sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0090" + (600 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.DEPTWARD + vbCrLf

            '< 성별/나이 
            sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0078" + (700 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.SEXAGE + vbCrLf

            ''< 환자명 
            For ix As Integer = 0 To ro_Data.PATNM.Length - 1
                sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0072" + (460 + (ix * 32) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 31).ToString.PadLeft(2, "0"c) + vbCrLf
            Next

            '< sRemark
            sPrtBuf += "1" + "9" + "2" + "2" + "001" + "0200" + (735 + riLeftPos).ToString.PadLeft(4, "0"c) + IIf(ro_Data.REMARK <> "", "C", "").ToString + vbCrLf

            '< 검체명
            sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0040" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.SPCNM + vbCrLf


            If ro_Data.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Or ro_Data.BCCNT = "B" Then
                '< 용기명 
                sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0040" + (400 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.TUBENM + vbCrLf
                '< 채혈자
                For ix As Integer = 0 To "채혈자:".Length - 1
                    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0040" + (600 + (ix * 28) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 11).ToString.PadLeft(2, "0"c) + vbCrLf
                Next
                '< 확인자
                For ix As Integer = 0 To "확인자:".Length - 1
                    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0010" + (600 + (ix * 28) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 21).ToString.PadLeft(2, "0"c) + vbCrLf
                Next
                '< 음영
                sPrtBuf += "A5" + vbCrLf
                sPrtBuf += "1" + "9" + "1" + "1" + "003" + "0000" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + "  X-Matching  " + vbCrLf
                sPrtBuf += "A1" + vbCrLf

            ElseIf PRG_CONST.BCCLS_BloodBank.StartsWith(ro_Data.BCCLSCD.Substring(0, 1)) Then

                '< 용기명 
                sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0040" + (400 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.TUBENM + vbCrLf
                '< 채혈자
                For ix As Integer = 0 To "채혈자".Length - 1
                    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0040" + (570 + (ix * 28) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 11).ToString.PadLeft(2, "0"c) + vbCrLf
                Next
                '< 확인자
                For ix As Integer = 0 To "확인자".Length - 1
                    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0010" + (570 + (ix * 28) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 21).ToString.PadLeft(2, "0"c) + vbCrLf
                Next

                '< 검사항목(음영)
                If sTestNms.Length > 12 Then sTestNms = sTestNms.Substring(0, 12)
                sPrtBuf += "A5" + vbCrLf
                sPrtBuf += "1" + "9" + "1" + "1" + "003" + "0000" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + " " + sTestNms + " " + vbCrLf
                sPrtBuf += "A1" + vbCrLf
            Else
                If ro_Data.BCTYPE = "M" Then
                    '< 검사그룹 sComment2
                    sPrtBuf += "1" + "9" + "1" + "1" + "003" + "0040" + (570 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.TGRPNM + vbCrLf

                    '< 미생물 검체번호
                    sPrtBuf += "1" + "9" + "1" + "1" + "003" + "0000" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNO_MB + vbCrLf
                Else
                    '< 검사항목명 
                    sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0000" + (300 + riLeftPos).ToString.PadLeft(4, "0"c) + sTestNms + vbCrLf

                    '< 용기명 
                    sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0040" + (400 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.TUBENM + vbCrLf

                    '< 검사그룹 sComment2
                    sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0045" + (600 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.TGRPNM + vbCrLf

                    '< 응급 sEmer 
                    sPrtBuf += "A5" + vbCrLf
                    sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0130" + (740 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.EMER + vbCrLf
                    sPrtBuf += "A1" + vbCrLf

                    '< 계 sKind
                    sPrtBuf += "1" + "9" + "1" + "1" + "003" + "0000" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCCLSCD + vbCrLf
                End If
            End If

            '< 라인 마지막 
            sPrtBuf += "Q0001" + vbCrLf
            sPrtBuf += "E"

            Return sPrtBuf

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return ""
        Finally

        End Try

    End Function

    Protected Overridable Function fnMakePrtMsg_BLD(ByVal ro_Data As STU_BLDLABEL) As String
        Dim sFn As String = "fnMakePrtMsg"

        Try
            Dim sPrtBuf As String = ""
            Dim sCrLf As String = Chr(13) + Chr(10)

            Dim iMaxLen As Integer = 33

            '< 기본설정 
            sPrtBuf = ""
            'sPrtBuf = sPrtBuf + "I8,1,001" + vbCrLf
            'sPrtBuf = sPrtBuf + "D" + vbCrLf        '-- 감열 = OD, 리본 = D
            'sPrtBuf = sPrtBuf + "Q464,24" + vbCrLf  '-- Label Length, Gap Length
            'sPrtBuf = sPrtBuf + "q696" + vbCrLf     '-- Label(Width)
            'sPrtBuf = sPrtBuf + "S4" + vbCrLf       '-- speed
            'sPrtBuf = sPrtBuf + "D8" + vbCrLf       '-- 농도
            'sPrtBuf = sPrtBuf + "ZT" + vbCrLf
            'sPrtBuf = sPrtBuf + "JF" + vbCrLf       '-- FB
            sPrtBuf = sPrtBuf & "N" + vbCrLf

            '< 등록번호
            sPrtBuf = sPrtBuf + "A170,20,0,3,1,2,N," + Chr(34) + ro_Data.REGNO + Chr(34) + vbCrLf

            '< 환자명  
            sPrtBuf = sPrtBuf + "A500,25,0,8,2,1,N," + Chr(34) + ro_Data.PATNM + Chr(34) + vbCrLf

            '< 진료과/병동/병실  
            sPrtBuf = sPrtBuf + "A180,68,0,3,1,1,N," + Chr(34) + ro_Data.DEPTWARD + Chr(34) + vbCrLf

            '< 성별/나이 
            sPrtBuf = sPrtBuf + "A530,68,0,3,1,1,N," + Chr(34) + ro_Data.SEXAGE + Chr(34) + vbCrLf

            '< 환자 혈액형 
            sPrtBuf = sPrtBuf + "A220,95,0,3,1,2,N," + Chr(34) + ro_Data.PAT_ABORH + Chr(34) + vbCrLf

            '< 출고 혈액형 
            sPrtBuf = sPrtBuf + "A550,95,0,3,1,2,N," + Chr(34) + ro_Data.BLD_ABORH + Chr(34) + vbCrLf

            ''< 혈액종류 
            Dim bHangul As Boolean = False
            For iLen As Integer = 0 To ro_Data.COMNM.Length - 1
                If Char.GetUnicodeCategory(ro_Data.COMNM.Substring(iLen, 1)) = Globalization.UnicodeCategory.OtherLetter Then
                    bHangul = True
                    Exit For
                End If
            Next

            If bHangul Then
                sPrtBuf = sPrtBuf + "A170,143,0,8,1,1,N," + Chr(34) + ro_Data.COMNM + Chr(34) + vbCrLf
            Else
                sPrtBuf = sPrtBuf + "A170,143,0,2,1,1,N," + Chr(34) + ro_Data.COMNM + Chr(34) + vbCrLf
            End If


            '< 적합부적합
            If ro_Data.XMATCH1 = "-" Then ro_Data.XMATCH1 = "적합"
            If ro_Data.XMATCH1 = "+" Then ro_Data.XMATCH1 = "부적합"

            If ro_Data.XMATCH2 = "-" Then ro_Data.XMATCH2 = "적합"
            If ro_Data.XMATCH2 = "+" Then ro_Data.XMATCH2 = "부적합"

            'sPrtBuf = sPrtBuf + "A190,185,0,8,1,1,N," + Chr(34) + sRst1 + Chr(34) + vbCrLf
            sPrtBuf = sPrtBuf + "A190,215,0,8,1,1,N," + Chr(34) + ro_Data.XMATCH2 + Chr(34) + vbCrLf

            '-- 혈액번호
            If ro_Data.BLDNO.Count = 1 Then
                sPrtBuf = sPrtBuf + "A500,180,0,3,1,1,N," + Chr(34) + ro_Data.BLDNO(0) + Chr(34) + vbCrLf
            Else
                For ix As Integer = 0 To ro_Data.BLDNO.Count - 1
                    sPrtBuf = sPrtBuf + "A500," + (150 + (ix * 30)).ToString + ",0,3,1,1,N," + Chr(34) + ro_Data.BLDNO(ix) + Chr(34) + vbCrLf
                Next
            End If

            '< IR
            'sIR = "1" : sFilter_in = "1"
            sPrtBuf = sPrtBuf + "A190,263,0,3,1,1,N," + Chr(34) + IIf(ro_Data.IR = "1", "Y", "").ToString().PadLeft(1, " "c) + "/" + IIf(ro_Data.FITER = "1", "F", "").ToString + Chr(34) + vbCrLf
            '>

            '< 확인
            sPrtBuf = sPrtBuf + "A450,263,0,3,1,1,N," + Chr(34) + "OK" + Chr(34) + vbCrLf
            '>


            '< 검사자
            sPrtBuf = sPrtBuf + "A180,303,0,8,1,1,N," + Chr(34) + ro_Data.BEFOUTNM + Chr(34) + vbCrLf

            '< 검사일자
            sPrtBuf = sPrtBuf + "A500,303,0,2,1,1,N," + Chr(34) + ro_Data.BEFOUTDT + Chr(34) + vbCrLf

            '< 출고자
            sPrtBuf = sPrtBuf + "A180,343,0,8,1,1,N," + Chr(34) + ro_Data.OUTNM + Chr(34) + vbCrLf

            '< 출고일자
            sPrtBuf = sPrtBuf + "A500,343,0,2,1,1,N," + Chr(34) + ro_Data.OUTDT + Chr(34) + vbCrLf

            '< 수령자
            sPrtBuf = sPrtBuf + "A180,383,0,8,1,1,N," + Chr(34) + ro_Data.RECNM + Chr(34) + vbCrLf


            '< 라인 마지막 
            sPrtBuf = sPrtBuf & "P1" + vbCrLf
            '>  

            Return sPrtBuf

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return ""

        End Try
    End Function

    Protected Overridable Function fnMakePrtMsg_PIS(ByVal ro_Data As STU_BCPRTINFO, _
                                                    ByVal rbFirst As Boolean, _
                                                    ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                                    ByVal rsBarType As String) As String
        Dim sFn As String = "fnMakePrtMsg_PIS"

        Try

            Dim a_sInfInfo As String() = ro_Data.INFINFO.Split("/"c)
            Dim sTestNms As String = ro_Data.TESTNMS


            If ro_Data.TESTNMS.Length > 20 Then
                sTestNms = ro_Data.TESTNMS.Substring(0, 25) + "..."
            End If

            If sTestNms.IndexOf("...") > -1 Then
                If sTestNms.Substring(0, sTestNms.IndexOf("...")).Length > 25 Then
                    sTestNms = sTestNms.Substring(0, 25) + "..."
                End If
            End If

            If ro_Data.PATNM.Length > 4 Then ro_Data.PATNM = ro_Data.PATNM.Substring(0, 4)
            If ro_Data.TGRPNM.Length > 20 Then ro_Data.TGRPNM = ro_Data.TGRPNM.Substring(0, 20)

            Dim sPrtBuf As String = ""

            sPrtBuf = ""
            sPrtBuf += Chr(2) + "qA" + vbCrLf       '-- Clear Memory
            sPrtBuf += Chr(2) + "XA" + vbCrLf       '-- Fonts
            sPrtBuf += Chr(2) + "m" + vbCrLf        '-- Metric Mode 
            sPrtBuf += Chr(2) + "f680" + vbCrLf     '-- Position(Backfedd)


            If ro_Data.BCNOPRT = "" Then
                Dim sTmp As String = "미채혈바코드"

                For ix As Integer = 0 To sTmp.Length - 1
                    sPrtBuf += Chr(2) + "ICF " + "IMG" + (ix + 1).ToString.PadLeft(2, "0"c) + Chr(13) + fnGet_FontImage(sTmp.Substring(ix, 1), New Font("굴림", 18, FontStyle.Bold), 32, 32)
                Next
            End If

            For ix As Integer = 0 To ro_Data.PATNM.Length - 1
                sPrtBuf += Chr(2) + "ICF " + "IMG" + (ix + 21).ToString.PadLeft(2, "0"c) + Chr(13) + fnGet_FontImage(ro_Data.PATNM.Substring(ix, 1), New Font("굴림", 18, FontStyle.Bold), 32, 32)
            Next

            ''-- 검사명
            'For ix As Integer = 0 To sTestNms.Length - 1
            '    sPrtBuf += Chr(2) + "ICF " + "IM" + (ix + 101).ToString.PadLeft(3, "0"c) + Chr(13) + fnGet_FontImage(sTestNms.Substring(ix, 1), New Font("굴림", 15, FontStyle.Regular), 30, 30)
            'Next

            ''-- 추가처방인 경우(원처방명)
            'For ix As Integer = 0 To ro_Data.TGRPNM.Length - 1
            '    sPrtBuf += Chr(2) + "ICF " + "IM" + (ix + 201).ToString.PadLeft(3, "0"c) + Chr(13) + fnGet_FontImage(ro_Data.TGRPNM.Substring(ix, 1), New Font("굴림", 15, FontStyle.Regular), 30, 30)
            'Next

            sPrtBuf += Chr(2) + "L" + vbCrLf

            sPrtBuf += "D11" + vbCrLf               '-- Set Dot Size
            sPrtBuf += "H19" + vbCrLf               '-- Header Setting
            sPrtBuf += "R0000" + vbCrLf             '-- Set Row Offset Amount

            '< 검체번호 
            sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNO + vbCrLf

            ''< 바코드 발행 일시  233
            If rbFirst Then
                sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (610 + riLeftPos).ToString.PadLeft(4, "0"c) + Fn.GetServerDateTime.ToString("yyyy-MM-dd HH:mm") + vbCrLf
            Else
                sPrtBuf += "A5" + vbCrLf
                sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (610 + riLeftPos).ToString.PadLeft(4, "0"c) + Fn.GetServerDateTime.ToString("yyyy-MM-dd HH:mm") + vbCrLf
                sPrtBuf += "A1" + vbCrLf
            End If

            '< 감염정보  

            For iCnt As Integer = 0 To a_sInfInfo.Length - 1
                If iCnt > 1 Then Exit For
                sPrtBuf += "1" + "9" + "2" + "2" + "001" + (190 - (iCnt * 35)).ToString.PadLeft(4, "0"c) + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + a_sInfInfo(iCnt).ToString() + vbCrLf
            Next

            '< 바코드  
            If ro_Data.BCNOPRT <> "" Then
                ' CODAR BAR 
                'sPrtBuf += "1" + "I" + "3" + "2" + "120" + "0115" + (350 + riLeftPos).ToString.PadLeft(4, "0"c) + "A" + ro_Data.BCNOPRT + "A" + vbCrLf
                sPrtBuf += "1" + "I" + "4" + "2" + "100" + "0140" + (370 + riLeftPos).ToString.PadLeft(4, "0"c) + "A" + ro_Data.BCNOPRT + "A" + vbCrLf

                ' code128
                'sPrtBuf += "1" + "e" + "1" + "2" + "120" + "0115" + (340 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNOPRT + vbCrLf

                '< 바코드 번호
                'sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0110" + (470 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNOPRT + vbCrLf
            Else
                '< 미수납 바코드 
                For ix As Integer = 0 To "미채혈바코드".Length - 1
                    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0180" + (400 + (ix * 32) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 1).ToString.PadLeft(2, "0"c) + vbCrLf
                Next
                '>  
            End If

            '< 등록번호 sPID
            If ro_Data.BCCLSCD.Substring(0, 1).Trim() = "R" Then
                sPrtBuf += "A5" + vbCrLf
                sPrtBuf += "1" + "9" + "1" + "2" + "003" + "0095" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.REGNO + vbCrLf
                sPrtBuf += "A1" + vbCrLf
            Else
                sPrtBuf += "1" + "9" + "1" + "2" + "002" + "0095" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.REGNO + vbCrLf
            End If

            ''< 진료과/병동/병실
            sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0110" + (600 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.DEPTWARD + vbCrLf

            '< 성별/나이 
            sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0090" + (700 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.SEXAGE + vbCrLf

            ''< 환자명 
            For ix As Integer = 0 To ro_Data.PATNM.Length - 1
                sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0092" + (450 + (ix * 32) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 21).ToString.PadLeft(2, "0"c) + vbCrLf
            Next

            '< 검체순번
            sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0060" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.REMARK + vbCrLf

            '< 검체명
            sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0065" + (430 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.SPCNM + vbCrLf

            '< 검사명
            sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0010" + (300 + riLeftPos).ToString.PadLeft(4, "0"c) + sTestNms + vbCrLf
            'For ix As Integer = 0 To sTestNms.Length - 1
            '    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0020" + (250 + (ix * 25) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IM" + (ix + 101).ToString.PadLeft(3, "0"c) + vbCrLf
            'Next

            '< 추가처방명
            'sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0005" + (300 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.TGRPNM + vbCrLf
            'For ix As Integer = 0 To ro_Data.TGRPNM.Length - 1
            '    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0055" + (250 + (ix * 25) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IM" + (ix + 201).ToString.PadLeft(3, "0"c) + vbCrLf
            'Next

            sPrtBuf += "1" + "9" + "2" + "2" + "002" + "0005" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + "P" + vbCrLf

            '< 라인 마지막 
            sPrtBuf += "Q0001" + vbCrLf
            sPrtBuf += "E"

            Return sPrtBuf

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return ""
        Finally

        End Try

    End Function

    Private Function fnGet_Bin2Hex(ByVal sBin As String) As String
        Dim sHex As String
        Dim lenBin As Integer
        Dim BHi%, BHj%

        sHex = "&H"
        lenBin = Len(sBin)
        If lenBin > 0 Then
            Dim splitLen As Integer
            Dim splitBin As String
            Dim jBin As Integer
            Dim iBin As Integer

            If lenBin Mod 4 <> 0 Then
                For BHi = 1 To 4 - (lenBin Mod 4)
                    sBin = "0" & sBin
                Next
            End If
            lenBin = Len(sBin)
            splitLen = lenBin / 4
            For BHi = 1 To splitLen
                splitBin = Mid(sBin, ((BHi - 1) * 4) + 1, 4)

                jBin = 8
                iBin = 0
                For BHj = 1 To 4
                    If Mid(splitBin, BHj, 1) = "1" Then
                        iBin = iBin + jBin
                    End If
                    jBin = jBin / 2
                Next
                sHex = sHex & Hex(iBin)
            Next
        End If

        Return sHex
    End Function

    Public Function fnGet_FontImage(ByVal rsValue As String, ByVal r_Font As Font, ByVal riWidth As Integer, ByVal riHeight As Integer) As String
        Dim sFilePath As String = Application.StartupPath + "\Image\"

        Try

            '0) 이미지 및 그래픽 개체 생성
            Dim bmpEP As New System.Drawing.Bitmap(riWidth, riHeight)

            Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(bmpEP)
            Dim sf_c As New Drawing.StringFormat
            Dim rect As New Drawing.RectangleF

            sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
            rect = New Drawing.RectangleF(1, 5, riWidth - 1, riHeight - 5)

            g.Clear(Drawing.Color.White)
            g.DrawString(rsValue, r_Font, Drawing.Brushes.Black, rect, sf_c)

            Dim sBin As String = ""
            Dim sImgData As String = ""

            For iy As Integer = bmpEP.Height - 1 To 0 Step -1
                For ix As Integer = 0 To bmpEP.Width - 1

                    If bmpEP.GetPixel(ix, iy).Name = "ffffffff" Then
                        sBin += "0"
                    Else
                        sBin += "1"
                    End If

                    If (ix + 1) Mod 8 = 0 Then
                        sImgData += fnGet_Bin2Hex(sBin).Substring(2)
                        sBin = ""
                    End If
                Next
            Next

            Dim sRet As String = ""

            For ix As Integer = 0 To (sImgData.Length - 9) Step 8

                sRet += "8004" + sImgData.Substring(ix, 8) + vbCr

            Next

            Return sRet + "FFFF"

        Catch ex As Exception

            Return ""
            MsgBox(ex.Message)
        End Try

    End Function

End Class

