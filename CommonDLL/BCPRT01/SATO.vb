Imports COMMON.SVar
Imports COMMON.CommFN
Imports COMMON.CommFN.Fn
Imports COMMON.CommPrint
Imports COMMON.CommLogin.LOGIN

Public Class SATO
    Private Const msFile As String = "File : SATO.vb, Class : SATO" + vbTab

    ''프린터 출력을 위한 정보 및 API 관련 정의
    ''http://support.microsoft.com/kb/154078 참조
    ''---------------------------------------------------------------------------------------------------------------------------------------------------------------
    'Structure DOCINFO
    '    Public pDocName As String
    '    Public pOutputFile As String
    '    Public pDataType As String
    'End Structure

    'Private Declare Function ClosePrinter Lib "winspool.drv" (ByVal hPrinter As Long) As Long
    'Private Declare Function EndDocPrinter Lib "winspool.drv" (ByVal hPrinter As Long) As Long
    'Private Declare Function EndPagePrinter Lib "winspool.drv" (ByVal hPrinter As Long) As Long
    'Private Declare Function OpenPrinter Lib "winspool.drv" Alias "OpenPrinterA" (ByVal pPrinterName As String, ByVal phPrinter As Long, ByVal pDefault As Long) As Long
    'Private Declare Function StartDocPrinter Lib "winspool.drv" Alias "StartDocPrinterA" (ByVal hPrinter As Long, ByVal Level As Long, ByVal pDocInfo As DOCINFO) As Long
    'Private Declare Function StartPagePrinter Lib "winspool.drv" (ByVal hPrinter As Long) As Long
    'Private Declare Function WritePrinter Lib "winspool.drv" (ByVal hPrinter As Long, ByVal pBuf As String, ByVal cdBuf As Long, ByRef pcWritten As Long) As Long

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
                    '<<<20170908 테스트용 임시 new로 수정 배포시 바꿔야함.
                    Dim sPrtMsg = fnMakePrtMsg(CType(ra_PrtData(ix1), STU_BCPRTINFO), rbFirst, riLeftPos, riTopPos, rsBarType)
                    '                    Dim sPrtMsg = fnMakePrtMsg(CType(ra_PrtData(ix1), STU_BCPRTINFO), rbFirst, riLeftPos, riTopPos, rsBarType)

                    Dim iPrtCnt As Integer = 1

                    If sPrtMsg <> "" Then
                        If CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT.IndexOf("A") >= 0 Then
                            iPrtCnt = 2
                        ElseIf CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "B" Then
                            '< CrossMatching 검체
                            iPrtCnt = 2 ' 루칸 카운트 비교 논의 필요 
                            '20210127 jhs 접수시 바코드 출력 로직 추가 
                        ElseIf CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "J" Or CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "J2" Then
                            iPrtCnt = 1
                            '-----------------------------------------------------
                        ElseIf IsNumeric(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT) Then
                            iPrtCnt = Convert.ToInt32(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT)
                        End If

                        If CType(ra_PrtData(ix1), STU_BCPRTINFO).CHKADDPRNT Then
                            iPrtCnt += CType(ra_PrtData(ix1), STU_BCPRTINFO).PRNTNUM
                        End If

                        For ix2 As Integer = 1 To iPrtCnt
                            If rsPrintPort.Trim() = "" Then
                                Dim objSkt As New TCP01.SendSocket

                                objSkt.sbConnectCliSocketToSvrSocket(rsSocketIP, 13734)

                                If objSkt.fnSendMsgOneConn("ITM", sPrtMsg) Then
                                    bReturn = True
                                End If

                                objSkt.sbDispose()

                                Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                                iFileNo = FreeFile()
                                Try
                                    FileOpen(iFileNo, sFileNm, OpenMode.Output)

                                Catch ex As Exception
                                    sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                    FileOpen(iFileNo, sFileNm, OpenMode.Output)
                                End Try

                                Print(iFileNo, sPrtMsg)
                                FileClose(iFileNo)
                            Else
                                If rsPrintPort.StartsWith("LPT") Or rsPrintPort.StartsWith("COM") Then
                                    Dim oBarPrt As New BarPrtParams
                                    Dim bTcpIP As Boolean = False
                                    oBarPrt = (New BCPRT01.Print_Set).fnGet_PrinterParams_Shared(rsPrintPort.Trim(), bTcpIP)

                                    If oBarPrt Is Nothing Then
                                        Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                                        iFileNo = FreeFile()
                                        Try
                                            FileOpen(iFileNo, sFileNm, OpenMode.Output)

                                        Catch ex As Exception
                                            sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                            FileOpen(iFileNo, sFileNm, OpenMode.Output)
                                        End Try

                                        Print(iFileNo, sPrtMsg)
                                        FileClose(iFileNo)

                                        'Process.Start("cmd.exe", "/C TYPE " + sFileNm + " > " + rsPrintPort.Trim())
                                        IO.File.Copy(sFileNm, rsPrintPort.Trim())
                                    Else
                                        Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                                        iFileNo = FreeFile()
                                        Try
                                            FileOpen(iFileNo, sFileNm, OpenMode.Output)

                                        Catch ex As Exception
                                            sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                            FileOpen(iFileNo, sFileNm, OpenMode.Output)
                                        End Try

                                        Print(iFileNo, sPrtMsg)
                                        FileClose(iFileNo)

                                        'Process.Start("cmd.exe", "/C TYPE " + sFileNm + " > " + oBarPrt.PrinterName)
                                        IO.File.Copy(sFileNm, oBarPrt.PrinterName)
                                        ' IO.File.Copy(sFileNm, oBarPrt.PrinterPort)
                                    End If
                                Else
                                    Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                                    iFileNo = FreeFile()
                                    Try
                                        FileOpen(iFileNo, sFileNm, OpenMode.Output)

                                    Catch ex As Exception
                                        sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                        FileOpen(iFileNo, sFileNm, OpenMode.Output)
                                    End Try

                                    Print(iFileNo, sPrtMsg)
                                    FileClose(iFileNo)

                                    PRTAPI.SendFileToPrinter(rsPrintPort, sFileNm)
                                End If

                            End If

                            Threading.Thread.Sleep(1000)
                        Next
                    End If

                End If
            Next

            Return True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Return False
        Finally
            FileClose(iFileNo)

        End Try
    End Function

    Public Overridable Function BarCodePrtOut_RIS(ByVal ra_PrtData As ArrayList, _
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
                    Dim sPrtMsg = fnMakePrtMsg_RIS(CType(ra_PrtData(ix1), STU_BCPRTINFO), rbFirst, riLeftPos, riTopPos, rsBarType)
                    Dim iPrtCnt As Integer = 1

                    If sPrtMsg <> "" Then
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

                                Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                                iFileNo = FreeFile()
                                Try
                                    FileOpen(iFileNo, sFileNm, OpenMode.Output)

                                Catch ex As Exception
                                    sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                    FileOpen(iFileNo, sFileNm, OpenMode.Output)
                                End Try

                                Print(iFileNo, sPrtMsg)
                                FileClose(iFileNo)


                            Else
                                If rsPrintPort.StartsWith("LPT") Or rsPrintPort.StartsWith("COM") Then
                                    Dim oBarPrt As New BarPrtParams
                                    Dim bTcpIP As Boolean = False
                                    oBarPrt = (New BCPRT01.Print_Set).fnGet_PrinterParams_Shared(rsPrintPort.Trim(), bTcpIP)

                                    If oBarPrt Is Nothing Then
                                        Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                                        iFileNo = FreeFile()
                                        Try
                                            FileOpen(iFileNo, sFileNm, OpenMode.Output)

                                        Catch ex As Exception
                                            sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                            FileOpen(iFileNo, sFileNm, OpenMode.Output)
                                        End Try

                                        Print(iFileNo, sPrtMsg)
                                        FileClose(iFileNo)

                                        'Process.Start("cmd.exe", "/C TYPE " + sFileNm + " > " + rsPrintPort.Trim())
                                        IO.File.Copy(sFileNm, rsPrintPort.Trim())
                                    Else
                                        Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                                        iFileNo = FreeFile()
                                        Try
                                            FileOpen(iFileNo, sFileNm, OpenMode.Output)

                                        Catch ex As Exception
                                            sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                            FileOpen(iFileNo, sFileNm, OpenMode.Output)
                                        End Try

                                        Print(iFileNo, sPrtMsg)
                                        FileClose(iFileNo)

                                        'Process.Start("cmd.exe", "/C TYPE " + sFileNm + " > " + oBarPrt.PrinterName)
                                        IO.File.Copy(sFileNm, oBarPrt.PrinterName)
                                    End If
                                Else
                                    Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                                    iFileNo = FreeFile()
                                    Try
                                        FileOpen(iFileNo, sFileNm, OpenMode.Output)

                                    Catch ex As Exception
                                        sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                        FileOpen(iFileNo, sFileNm, OpenMode.Output)
                                    End Try

                                    Print(iFileNo, sPrtMsg)
                                    FileClose(iFileNo)

                                    PRTAPI.SendFileToPrinter(rsPrintPort, sFileNm)
                                End If
                            End If

                            Threading.Thread.Sleep(1000)
                        Next
                    End If

                End If
            Next

            Return True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Return False
        Finally
            FileClose(iFileNo)

        End Try
    End Function

    Public Overridable Function BarCodePrtOut_BLD(ByVal roSndMsg As ArrayList, ByVal riPrtCnt As Integer, _
                                        Optional ByVal rsIP As String = "127.0.0.1", _
                                            Optional ByVal rsPrintPort As String = "9100", _
                                              Optional ByVal rsOUTPUT As String = "", _
                                                   Optional ByVal rsLeftPos As String = "0", Optional ByVal rsTopPos As String = "0", _
                                                    Optional ByVal rbFirst As Boolean = False) As Boolean
        Dim sFn As String = "BarCodePrtOut_BLD"
        Dim bReturn As Boolean = False
        Dim iFileNo As Integer = 0

        Try

            If roSndMsg Is Nothing Then
            Else

                For ix1 As Integer = 0 To roSndMsg.Count - 1


                    Dim sPrtMsg As String = fnMakePrtMsg_BLD(CType(roSndMsg(ix1), STU_BLDLABEL))

                    If rsIP.Trim() <> "" Then
                        Dim objSkt As New TCP01.SendSocket

                        objSkt.sbConnectCliSocketToSvrSocket(rsIP, 13734)

                        If objSkt.fnSendMsgOneConn("ITM", sPrtMsg) Then
                            bReturn = True
                        End If

                        objSkt.sbDispose()
                    Else

                        If rsPrintPort.StartsWith("LPT") Or rsPrintPort.StartsWith("COM") Then
                            Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                            iFileNo = FreeFile()
                            Try
                                FileOpen(iFileNo, sFileNm, OpenMode.Output)

                            Catch ex As Exception
                                sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                FileOpen(iFileNo, sFileNm, OpenMode.Output)
                            End Try

                            Print(iFileNo, sPrtMsg)
                            FileClose(iFileNo)

                            'Process.Start("cmd.exe", "/C TYPE " + sFileNm + " > " + rsOUTPUT.Trim())
                            IO.File.Copy(sFileNm, rsPrintPort.Trim())

                        Else
                            Dim sFileNm As String = Application.StartupPath + "\BCPRT.TXT"

                            iFileNo = FreeFile()
                            Try
                                FileOpen(iFileNo, sFileNm, OpenMode.Output)

                            Catch ex As Exception
                                sFileNm = Application.StartupPath + "\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                FileOpen(iFileNo, sFileNm, OpenMode.Output)
                            End Try

                            Print(iFileNo, sPrtMsg)
                            FileClose(iFileNo)

                            PRTAPI.SendFileToPrinter(rsPrintPort, sFileNm)
                        End If

                    End If

                    Threading.Thread.Sleep(1000)

                Next
            End If


            Return True
        Catch ioex As System.IO.IOException
            ''파일은 다른 프로세스에서 사용 중이므로 프로세스에서 액세스할 수 없습니다.
            'If Err.Number = 75 Then
            '    'Recursive Call
            '    BarCodePrtOut(asSndMsg, aiPrtCnt, asIP, aiPort)
            'End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
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
                                strFileNm = "C:\BCPRT_" + Format(Now, "yyMMddHHmmss") + ".TXT"
                                FileOpen(iFileNo, strFileNm, OpenMode.Output)
                            End Try

                            Print(iFileNo, sPrtMsg)
                            FileClose(iFileNo)

                            'Process.Start("cmd.exe", "/C TYPE " + strFileNm + " > " + rsPrintPort.Trim())
                            IO.File.Copy(strFileNm, rsPrintPort.Trim())
                        End If

                        Threading.Thread.Sleep(CInt(sPrtMsg.Length * 1.5))
                    Next
                End If
            Next

            Return True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
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
            
            'ro_Data.INFINFO = "S/MRSA"
            Dim a_sInfInfo As String() = ro_Data.INFINFO.Split("/"c)
            Dim sTestNms As String = ro_Data.TESTNMS
            Dim ABOCHK As String = ro_Data.ABOCHK

            If ro_Data.TESTNMS.Length > 35 Then
                sTestNms = ro_Data.TESTNMS.Substring(0, 25) + "..."
            End If

            If sTestNms.IndexOf("...") > -1 Then
                If sTestNms.Substring(0, sTestNms.IndexOf("...")).Length > 35 Then
                    sTestNms = sTestNms.Substring(0, 35) + "..."
                End If
            End If

            'If ro_Data.PATNM.Length > 4 Then ro_Data.PATNM = ro_Data.PATNM.Substring(0, 4)

            Dim sPrtBuf As String = ""
            Dim iHanCnt As Integer = 0

            sPrtBuf = ""
            sPrtBuf += Chr(27) + "A" + vbCrLf       '-- Data Send Start
            'sPrtBuf += Chr(27) + "A1" + (280).ToString("D4") + (400).ToString("D4") + vbCrLf      '-- Page Size: 1 mm = 8 dots, 35 mm = 280 dots, 53 mm = 424 dots
            sPrtBuf += Chr(27) + "A3H201V001" + vbCrLf

            sPrtBuf += Chr(27) + "%2" + vbCrLf '-- 회전(180)

            '< 검체번호 
            sPrtBuf += Chr(27) + "V" + "0255" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + ro_Data.BCNO + vbCrLf

           


            '<상호 주석 20150602
            '''< 바코드 발행 일시  233
            'If rbFirst Then
            '    sPrtBuf += Chr(27) + "V" + "0259" + Chr(27) + "H" + (110 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + Fn.GetServerDateTime.ToString("MM-dd HH:mm") + vbCrLf
            'Else
            '    sPrtBuf += Chr(27) + "V" + "0259" + Chr(27) + "H" + (110 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + "S" + "0" + Chr(27) + "XS" + Fn.GetServerDateTime.ToString("HH:mm") + vbCrLf
            '    sPrtBuf += Chr(27) + "V" + "0258" + Chr(27) + "H" + (115 + riLeftPos).ToString("D4") + Chr(27) + "(" + "100" + "," + "20"
            'End If
            '>

            '<상호 수정 20150602
            ''< 바코드 발행 일시  233
            If rbFirst Then
                sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (110 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + Fn.GetServerDateTime.ToString("MM-dd HH:mm") + vbCrLf
            Else
                sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (110 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + "S" + "0" + Chr(27) + "XS"  +Fn.GetServerDateTime.ToString("HH:mm") + vbCrLf
                sPrtBuf += Chr(27) + "V" + "0029" + Chr(27) + "H" + (115 + riLeftPos).ToString("D4") + Chr(27) + "(" + "100" + "," + "20"

            End If
            '>

            '< 감염정보  
            'For ix As Integer = 0 To a_sInfInfo.Length - 1
            '    If ix > 1 Then Exit For
            '    sPrtBuf += Chr(27) + "V" + (20 + (ix * 20)).ToString("D4") + Chr(27) + "H" + (0 + riLeftPos).ToString("D4") + "L0101" + Chr(27) + "M" + a_sInfInfo(ix).ToString() + vbCrLf
            'Next

            If ro_Data.INFINFO = "" Then
            Else

                '<상호 추가 20150602
                Dim InfInfoTmp As String = ro_Data.INFINFO

                If InfInfoTmp <> "" Then
                    InfInfoTmp = Replace(InfInfoTmp, "/", ".")
                    InfInfoTmp = Replace(InfInfoTmp, ",", ".")
                End If

                '>

                '<상호 수정전 20150602
                'sPrtBuf += Chr(27) + "V" + "0259" + Chr(27) + "H" + (190 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.INFINFO + vbCrLf
                '>
                sPrtBuf += Chr(27) + "V" + "0259" + Chr(27) + "H" + (170 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "OA" + InfInfoTmp + vbCrLf
                'sPrtBuf += Chr(27) + "V" + "0258" + Chr(27) + "H" + (190 + riLeftPos).ToString("D4") + Chr(27) + "(" + "35" + "," + "20" '음영부분임, 상호 주석 20150602
                'sPrtBuf += Chr(27) + "V" + "0260" + Chr(27) + "H" + (200 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "RDA00,P10,P10," + ro_Data.INFINFO + vbCrLf
            End If

            '< 바코드  
            If ro_Data.BCNOPRT <> "" Then
                ' CODAR BAR 
                sPrtBuf += Chr(27) + "V" + "0233" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "BD" + "0" + "01" + "100" + "A" + ro_Data.BCNOPRT.Trim + "A" + vbCrLf

                '< 바코드 번호
                sPrtBuf += Chr(27) + "V" + "0130" + Chr(27) + "H" + (250 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + ro_Data.BCNOPRT.Trim + vbCrLf
            Else
                '< 미수납 바코드 
                '< 20160922 미수납바코드에 환자번호 바코드 추가 
                sPrtBuf += Chr(27) + "V" + "0245" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0202" + Chr(27) + "S" + fnGet_Hangle_Font_3("미채혈바코드")
                sPrtBuf += Chr(27) + "V" + "0190" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "BD" + "0" + "01" + "070" + "A" + ro_Data.REGNO + "A" + vbCrLf

            End If

            '< 등록번호 sPID
            sPrtBuf += Chr(27) + "V" + "0110" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + ro_Data.REGNO + vbCrLf
            If PRG_CONST.BCCLS_ExLab.Contains(ro_Data.BCCLSCD) Then
                sPrtBuf += Chr(27) + "V" + "0111" + Chr(27) + "H" + (395 + riLeftPos).ToString("D4") + Chr(27) + "(" + "170" + "," + "30"
            End If

            ''< 진료과/병동/병실
            sPrtBuf += Chr(27) + "V" + "0100" + Chr(27) + "H" + (120 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "S" + ro_Data.DEPTWARD.Replace("호", "").Replace("중환자병실", "") + vbCrLf

            '< 성별/나이 
            sPrtBuf += Chr(27) + "V" + "0120" + Chr(27) + "H" + (80 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + ro_Data.SEXAGE + vbCrLf

            ''< 환자명 
            '20190607 YJY
            sPrtBuf += Chr(27) + "V" + "0120" + Chr(27) + "H" + (220 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.PATNM) '+ vbCrLf
            'sPrtBuf += Chr(27) + "V" + "0120" + Chr(27) + "H" + (220 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "M" + fnGet_Hangle_Font_3("NMC-19-06-07-01") '+ vbCrLf

            '< sRemark
            'ro_Data.REMARK = "C"
            sPrtBuf += Chr(27) + "V" + "0180" + Chr(27) + "H" + (45 + riLeftPos).ToString("D4") + Chr(27) + "L0202" + "P" + "5" + Chr(27) + "XS" + IIf(ro_Data.REMARK = "", "", "CM").ToString() + vbCrLf

            '< 혈액은행검체(크로스매칭)
            If ro_Data.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Or ro_Data.BCCNT = "B" Then
                '< 검체명
                'sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (260 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SPCNM + vbCrLf
                '< 용기명 
                sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TUBENM + vbCrLf
                ''< 채혈자
                'sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (205 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("채혈자:")
                ''< 확인자
                'sPrtBuf += Chr(27) + "V" + "0045" + Chr(27) + "H" + (205 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("확인자:")

                ''< 채혈자
                sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (205 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("채혈자:")
                sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (205 + riLeftPos).ToString("D4") + Chr(27) + "FW0303H0170V0040"

                '< 음영
                sPrtBuf += Chr(27) + "V" + "0040" + Chr(27) + "H" + (389 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + " X-Match " + vbCrLf
                sPrtBuf += Chr(27) + "V" + "0041" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "(" + "170" + "," + "34"
                '< 혈액은행검체(혈액형)
            ElseIf PRG_CONST.BCCLS_BloodBank.StartsWith(ro_Data.BCCLSCD.Substring(0, 1)) Then
                '< 검체명
                sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (260 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SPCNM + vbCrLf
                '< 용기명 
                sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TUBENM + vbCrLf
                ''< 채혈자
                'sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (185 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("채혈자:")
                ''< 확인자
                'sPrtBuf += Chr(27) + "V" + "0045" + Chr(27) + "H" + (185 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("확인자:")

                '< 채혈자
                sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (185 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("채혈자:")
                sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (185 + riLeftPos).ToString("D4") + Chr(27) + "FW0303H0170V0040"

                '< 음영
                If sTestNms.Length > 12 Then sTestNms = sTestNms.Substring(0, 12)
                sPrtBuf += Chr(27) + "V" + "0040" + Chr(27) + "H" + (389 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + sTestNms + vbCrLf
                sPrtBuf += Chr(27) + "V" + "0041" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "(" + "170" + "," + "34"
                '<일반검체
            Else
                '<<미생물
                If ro_Data.BCTYPE = "M" Then
                    '< 검체명
                    sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SPCNM + vbCrLf
                    '< 검사그룹 sComment2
                    'ro_Data.TGRPNM = "BAP"
                    'sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (170 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TGRPNM + vbCrLf
                    sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (170 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3(ro_Data.TGRPNM)

                    '< 미생물 검체번호
                    'ro_Data.BCNO_MB = "111023-52-0001"
                    sPrtBuf += Chr(27) + "V" + "0040" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "M" + ro_Data.BCNO_MB + vbCrLf
                    '<<일반검체
                Else
                    '< 검체명
                    sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (260 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SPCNM + vbCrLf
                    '< 용기명 
                    sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TUBENM + vbCrLf
                    '< 검사그룹 sComment2
                    'ro_Data.TGRPNM = "H C G"
                    sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (110 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TGRPNM + vbCrLf
                    '< 응급 sEmer
                    'ro_Data.EMER = "E"
                    If ro_Data.EMER <> "" Then
                        sPrtBuf += Chr(27) + "V" + "0210" + Chr(27) + "H" + (45 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + "P" + "3" + Chr(27) + "XS" + ro_Data.EMER + vbCrLf
                        sPrtBuf += Chr(27) + "V" + "0215" + Chr(27) + "H" + (47 + riLeftPos).ToString("D4") + Chr(27) + "(" + "25" + "," + "25"
                    End If

                    '< 검사항목명 
                    'If sTestNms = "" Then sTestNms = "EXAM"
                    '20210429 jhs 특정검사만 음영 표시
                    If PRG_CONST.shadow_test(ro_Data.TESTCD) <> "" Then
                        sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + sTestNms + vbCrLf ' 4째줄    COVID-19 E2(음영)
                        sPrtBuf += Chr(27) + "V" + "0035" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "(" + "25" + "," + "25"
                    Else
                        sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + sTestNms + vbCrLf                           ' 4째줄    COVID-19 E2
                    End If
                    '---------------------------
                    'sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + sTestNms + vbCrLf
                    '< 계 sKind
                    sPrtBuf += Chr(27) + "V" + "0040" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + ro_Data.BCCLSCD + vbCrLf

                    '< 자체응급
                    If ro_Data.ERPRTYN <> "" Then
                        ' MsgBox(ro_Data.ERPRTYN)
                        sPrtBuf += Chr(27) + "V" + "0160" + Chr(27) + "H" + (45 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + "P" + "3" + Chr(27) + "XS" + "R" + vbCrLf
                        sPrtBuf += Chr(27) + "V" + "0165" + Chr(27) + "H" + (47 + riLeftPos).ToString("D4") + Chr(27) + "(" + "25" + "," + "25"
                    End If
                End If
            End If
            '< 2019-04-19 JJH 혈액형 데이터가 없을때 * 표시
            If ABOCHK <> "" Then
                sPrtBuf += Chr(27) + "V" + "0220" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0303" + Chr(27) + "S" + ABOCHK + vbCrLf

            End If
            '< 라인 마지막 
            sPrtBuf += Chr(27) + "Q" + "1" + vbCrLf

            sPrtBuf += Chr(27) + "A3H101V001" + vbCrLf
            sPrtBuf += Chr(27) + "Z" + vbCrLf



            'riTopPos = riTopPos + 600
            ''OCS 초기화(용지크기)
            'sPrtBuf += Chr(27) + "A" + vbCrLf       '-- Data Send Start
            'sPrtBuf += Chr(27) + "A1" + (280).ToString("D4") + (riTopPos).ToString("D4") + vbCrLf      '-- Page Size: 1 mm = 8 dots, 35 mm = 280 dots, 53 mm = 424 dots
            'sPrtBuf += Chr(27) + "A3H001V001" + vbCrLf
            'sPrtBuf += Chr(27) + "Z" + vbCrLf

            Return sPrtBuf

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return ""
        Finally

        End Try

    End Function

    Protected Overridable Function fnMakePrtMsg_new(ByVal ro_Data As STU_BCPRTINFO, _
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

            Dim sPrtBuf As String = ""
            Dim iHanCnt As Integer = 0

            sPrtBuf = ""
            sPrtBuf += Chr(27) + "A" + vbCrLf       '-- Data Send Start
            sPrtBuf += Chr(27) + "A3H201V001" + vbCrLf

            sPrtBuf += Chr(27) + "%2" + vbCrLf '-- 회전(180)

            '< 검체번호 
            sPrtBuf += Chr(27) + "V" + "0255" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + ro_Data.BCNO + vbCrLf

            ''< 바코드 발행 일시  233
            If rbFirst Then
                sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (110 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + Fn.GetServerDateTime.ToString("MM-dd HH:mm") + vbCrLf
            Else
                sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (110 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + "S" + "0" + Chr(27) + "XS" + Fn.GetServerDateTime.ToString("HH:mm") + vbCrLf
                sPrtBuf += Chr(27) + "V" + "0029" + Chr(27) + "H" + (115 + riLeftPos).ToString("D4") + Chr(27) + "(" + "100" + "," + "20"

            End If
            '>

            '< 감염정보  
            'For ix As Integer = 0 To a_sInfInfo.Length - 1
            '    If ix > 1 Then Exit For
            '    sPrtBuf += Chr(27) + "V" + (20 + (ix * 20)).ToString("D4") + Chr(27) + "H" + (0 + riLeftPos).ToString("D4") + "L0101" + Chr(27) + "M" + a_sInfInfo(ix).ToString() + vbCrLf
            'Next

            If ro_Data.INFINFO = "" Then
            Else

                '<상호 추가 20150602
                Dim InfInfoTmp As String = ro_Data.INFINFO

                If InfInfoTmp <> "" Then
                    InfInfoTmp = Replace(InfInfoTmp, "/", ".")
                    InfInfoTmp = Replace(InfInfoTmp, ",", ".")
                End If

                sPrtBuf += Chr(27) + "V" + "0259" + Chr(27) + "H" + (170 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "OA" + InfInfoTmp + vbCrLf
            End If

            '< 바코드  
            If ro_Data.BCNOPRT <> "" Then
                ' CODAR BAR 
                sPrtBuf += Chr(27) + "V" + "0233" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "BD" + "0" + "01" + "100" + "A" + ro_Data.BCNOPRT.Trim + "A" + vbCrLf

                '< 바코드 번호
                sPrtBuf += Chr(27) + "V" + "0130" + Chr(27) + "H" + (250 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + ro_Data.BCNOPRT.Trim + vbCrLf
            Else
                '< 미수납 바코드 
                sPrtBuf += Chr(27) + "V" + "0245" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0202" + Chr(27) + "S" + fnGet_Hangle_Font_3("미채혈바코드")
                sPrtBuf += Chr(27) + "V" + "0190" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "BD" + "0" + "01" + "070" + "A" + ro_Data.REGNO + "A" + vbCrLf

            End If

            '< 등록번호 sPID
            sPrtBuf += Chr(27) + "V" + "0110" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + ro_Data.REGNO + vbCrLf
            If PRG_CONST.BCCLS_ExLab.Contains(ro_Data.BCCLSCD) Then
                sPrtBuf += Chr(27) + "V" + "0111" + Chr(27) + "H" + (395 + riLeftPos).ToString("D4") + Chr(27) + "(" + "170" + "," + "30"
            End If

            ''< 진료과/병동/병실
            sPrtBuf += Chr(27) + "V" + "0100" + Chr(27) + "H" + (120 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "S" + ro_Data.DEPTWARD.Replace("호", "").Replace("중환자병실", "") + vbCrLf

            '< 성별/나이 
            sPrtBuf += Chr(27) + "V" + "0120" + Chr(27) + "H" + (80 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + ro_Data.SEXAGE + vbCrLf

            ''< 환자명 
            sPrtBuf += Chr(27) + "V" + "0120" + Chr(27) + "H" + (220 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.PATNM) '+ vbCrLf

            '< sRemark
            sPrtBuf += Chr(27) + "V" + "0180" + Chr(27) + "H" + (45 + riLeftPos).ToString("D4") + Chr(27) + "L0202" + "P" + "5" + Chr(27) + "XS" + IIf(ro_Data.REMARK = "", "", "C").ToString() + vbCrLf
            '1)혈액은행 X매칭
            If ro_Data.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Or ro_Data.BCCNT = "B" Then
                '< 용기명 
                sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TUBENM + vbCrLf
                ''< 채혈자
                sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (205 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("채혈자:")
                '< 음영
                sPrtBuf += Chr(27) + "V" + "0040" + Chr(27) + "H" + (389 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + " X-Match " + vbCrLf
                sPrtBuf += Chr(27) + "V" + "0041" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "(" + "170" + "," + "34"
                '2) 혈액은행 검체 
            ElseIf PRG_CONST.BCCLS_BloodBank.StartsWith(ro_Data.BCCLSCD.Substring(0, 1)) Then
                '< 검체명
                sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (260 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SPCNM + vbCrLf
                '< 용기명 
                sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TUBENM + vbCrLf
                
                '< 채혈자
                sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (185 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("채혈자:")

                '< 음영
                If sTestNms.Length > 12 Then sTestNms = sTestNms.Substring(0, 12)
                sPrtBuf += Chr(27) + "V" + "0040" + Chr(27) + "H" + (389 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + sTestNms + vbCrLf
                sPrtBuf += Chr(27) + "V" + "0041" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "(" + "170" + "," + "34"
                '3)일반검체
            Else
                '3-1)미생물
                If ro_Data.BCTYPE = "M" Then
                    '< 검체명
                    sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SPCNM + vbCrLf
                    '< 검사그룹 sComment2
                    sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (170 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3(ro_Data.TGRPNM)
                    '< 미생물 검체번호
                    'ro_Data.BCNO_MB = "111023-52-0001"
                    sPrtBuf += Chr(27) + "V" + "0040" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "M" + ro_Data.BCNO_MB + vbCrLf
                    '3-2) 일반검체
                Else
                    ''< 검체명*
                    'sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (260 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SPCNM + vbCrLf
                    ''< 용기명*
                    'sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TUBENM + vbCrLf
                    ''< 검사항목명* 
                    'sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + sTestNms + vbCrLf
                    '영문
                    '< 검체명*
                    sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (260 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_2("ABCDEFGHK") + vbCrLf '+ ro_Data.SPCNM + vbCrLf
                    '< 용기명*
                    sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_2("ABCDEFGH") + vbCrLf 'ro_Data.TUBENM + vbCrLf
                    '< 검사항목명* 
                    sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("ABCDEFGHHKLMN") + vbCrLf ' sTestNms + vbCrLf
                    '한글()
                    ''< 검체명*
                    'sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (260 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_2("검체명검체명") + vbCrLf '+ ro_Data.SPCNM + vbCrLf
                    ''< 용기명*
                    'sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_2("용기명용기명") + vbCrLf 'ro_Data.TUBENM + vbCrLf
                    ''< 검사항목명* 
                    'sPrtBuf += Chr(27) + "V" + "0030" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + fnGet_Hangle_Font_3("검사명검사명검사명검사명") + vbCrLf ' sTestNms + vbCrLf



                    '< 검사그룹 sComment2
                    sPrtBuf += Chr(27) + "V" + "0060" + Chr(27) + "H" + (110 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TGRPNM + vbCrLf
                    '< 응급 sEmer
                    If ro_Data.EMER <> "" Then
                        sPrtBuf += Chr(27) + "V" + "0210" + Chr(27) + "H" + (45 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + "P" + "3" + Chr(27) + "XS" + ro_Data.EMER + vbCrLf
                        sPrtBuf += Chr(27) + "V" + "0215" + Chr(27) + "H" + (47 + riLeftPos).ToString("D4") + Chr(27) + "(" + "25" + "," + "25"
                    End If
                    '< 계 sKind
                    sPrtBuf += Chr(27) + "V" + "0040" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + ro_Data.BCCLSCD + vbCrLf
                End If
            End If

            '< 라인 마지막 
            sPrtBuf += Chr(27) + "Q" + "1" + vbCrLf

            sPrtBuf += Chr(27) + "A3H101V001" + vbCrLf
            sPrtBuf += Chr(27) + "Z" + vbCrLf

            Return sPrtBuf

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return ""
        Finally

        End Try

    End Function
    Protected Overridable Function fnMakePrtMsg_RIS(ByVal ro_Data As STU_BCPRTINFO, _
                                                ByVal rbFirst As Boolean, _
                                                ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                                ByVal rsBarType As String) As String
        Dim sFn As String = "fnMakePrtMsg"

        Try

            'ro_Data.INFINFO = "S/MRSA"
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
            sPrtBuf += Chr(27) + "A" + vbCrLf       '-- Data Send Start
            'sPrtBuf += Chr(27) + "A1" + (280).ToString("D4") + (400).ToString("D4") + vbCrLf      '-- Page Size: 1 mm = 8 dots, 35 mm = 280 dots, 53 mm = 424 dots
            'sPrtBuf += Chr(27) + "A3H001V001" + vbCrLf
            sPrtBuf += Chr(27) + "A3H201V001" + vbCrLf
            sPrtBuf += Chr(27) + "%2" + vbCrLf '-- 회전(180)


            '< 바코드
            '< 바코드 번호
            sPrtBuf += Chr(27) + "V" + "0185" + Chr(27) + "H" + (270 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "S" + ro_Data.BCNOPRT + vbCrLf

            ' CODAR BAR 
            sPrtBuf += Chr(27) + "V" + "0255" + Chr(27) + "H" + (350 + riLeftPos).ToString("D4") + Chr(27) + "BD" + "0" + "01" + "065" + "A" + ro_Data.BCNOPRT + "A" + vbCrLf

            '< 등록번호 sPID
            sPrtBuf += Chr(27) + "V" + "0150" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "WB" + "0" + ro_Data.REGNO + vbCrLf

            ''< 진료과/병동/병실
            'sPrtBuf += Chr(27) + "V" + "0155" + Chr(27) + "H" + (310 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + "0" + ro_Data.DEPTWARD + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0105" + Chr(27) + "H" + (140 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.DEPTWARD.Replace("호", "").Replace("중환자병실", "") + vbCrLf

            ''< 환자명 
            sPrtBuf += Chr(27) + "V" + "0160" + Chr(27) + "H" + (210 + riLeftPos).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.PATNM) '+ vbCrLf

            '< 검체명
            sPrtBuf += Chr(27) + "V" + "0105" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SPCNM + vbCrLf

            '< 작업번호
            sPrtBuf += Chr(27) + "V" + "0070" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.TUBENM.Substring(0, 8) + "-" + vbCrLf

            '< 작업번호
            sPrtBuf += Chr(27) + "V" + "0075" + Chr(27) + "H" + (250 + riLeftPos).ToString("D4") + Chr(27) + "L0202" + Chr(27) + "S" + ro_Data.TUBENM.Substring(8) + vbCrLf

            '< 검사항목명 
            'If sTestNms = "" Then sTestNms = "EXAM"
            sPrtBuf += Chr(27) + "V" + "0043" + Chr(27) + "H" + (390 + riLeftPos).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + sTestNms + vbCrLf

            '< 라인 마지막 
            sPrtBuf += Chr(27) + "Q" + "1" + vbCrLf
            sPrtBuf += Chr(27) + "A3H101V001" + vbCrLf
            sPrtBuf += Chr(27) + "Z"


            ''OCS 초기화(용지크기)
            'sPrtBuf += Chr(27) + "A" + vbCrLf       '-- Data Send Start
            'sPrtBuf += Chr(27) + "A1" + (280).ToString("D4") + (600).ToString("D4") + vbCrLf      '-- Page Size: 1 mm = 8 dots, 35 mm = 280 dots, 53 mm = 424 dots
            'sPrtBuf += Chr(27) + "A3H001V001" + vbCrLf
            'sPrtBuf += Chr(27) + "Z" + vbCrLf


            Return sPrtBuf

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

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


            sPrtBuf = ""
            sPrtBuf += Chr(27) + "A" + vbCrLf       '-- Data Send Start
            sPrtBuf += Chr(27) + "A1" + (592).ToString("D4") + (704).ToString("D4") + vbCrLf      '-- Page Size: 1 mm = 8 dots, 35 mm = 280 dots, 53 mm = 424 dots
            sPrtBuf += Chr(27) + "A3H001V001" + vbCrLf

            sPrtBuf += Chr(27) + "%2" + vbCrLf '-- 회전(180)


            ''< 기본설정 
            'sPrtBuf = ""
            ''sPrtBuf = sPrtBuf + "I8,1,001" + vbCrLf
            ''sPrtBuf = sPrtBuf + "D" + vbCrLf        '-- 감열 = OD, 리본 = D
            ''sPrtBuf = sPrtBuf + "Q464,24" + vbCrLf  '-- Label Length, Gap Length
            ''sPrtBuf = sPrtBuf + "q696" + vbCrLf     '-- Label(Width)
            ''sPrtBuf = sPrtBuf + "S4" + vbCrLf       '-- speed
            ''sPrtBuf = sPrtBuf + "D8" + vbCrLf       '-- 농도
            ''sPrtBuf = sPrtBuf + "ZT" + vbCrLf
            ''sPrtBuf = sPrtBuf + "JF" + vbCrLf       '-- FB
            'sPrtBuf = sPrtBuf & "N" + vbCrLf

            ''< 등록번호
            'sPrtBuf = sPrtBuf + "A170,20,0,3,1,2,N," + Chr(34) + ro_Data.REGNO + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0530" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.REGNO + vbCrLf

            ''< 환자명  
            'sPrtBuf = sPrtBuf + "A500,25,0,8,2,1,N," + Chr(34) + ro_Data.PATNM + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0540" + Chr(27) + "H" + (180).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.PATNM + " ") + vbCrLf

            ''< 진료과/병동/병실  
            'sPrtBuf = sPrtBuf + "A180,68,0,3,1,1,N," + Chr(34) + ro_Data.DEPTWARD + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0490" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.DEPTWARD.Replace("호", "").Replace("중환자병실", "") + vbCrLf

            ''< 성별/나이 
            'sPrtBuf = sPrtBuf + "A530,68,0,3,1,1,N," + Chr(34) + ro_Data.SEXAGE + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0490" + Chr(27) + "H" + (175).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.SEXAGE + vbCrLf

            ''< 환자 혈액형 
            'sPrtBuf = sPrtBuf + "A220,95,0,3,1,2,N," + Chr(34) + ro_Data.PAT_ABORH + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0435" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0303" + Chr(27) + "S" + ro_Data.PAT_ABORH + vbCrLf

            ''< 출고 혈액형 
            'sPrtBuf = sPrtBuf + "A550,95,0,3,1,2,N," + Chr(34) + ro_Data.BLD_ABORH + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0435" + Chr(27) + "H" + (175).ToString("D4") + Chr(27) + "L0303" + Chr(27) + "S" + ro_Data.BLD_ABORH + vbCrLf

            ''< 혈액종류 
            Dim bHangul As Boolean = False
            For iLen As Integer = 0 To ro_Data.COMNM.Length - 1
                If Char.GetUnicodeCategory(ro_Data.COMNM.Substring(iLen, 1)) = Globalization.UnicodeCategory.OtherLetter Then
                    bHangul = True
                    Exit For
                End If
            Next

            If bHangul Then
                '    sPrtBuf = sPrtBuf + "A170,143,0,8,1,1,N," + Chr(34) + ro_Data.COMNM + Chr(34) + vbCrLf
                sPrtBuf += Chr(27) + "V" + "0370" + Chr(27) + "H" + (515).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.COMNM + " ") + vbCrLf
            Else
                '    sPrtBuf = sPrtBuf + "A170,143,0,2,1,1,N," + Chr(34) + ro_Data.COMNM + Chr(34) + vbCrLf
                sPrtBuf += Chr(27) + "V" + "0370" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + ro_Data.COMNM + vbCrLf
            End If

            '< 적합부적합
            If ro_Data.XMATCH1 = "-" Then ro_Data.XMATCH1 = "적합"
            If ro_Data.XMATCH1 = "+" Then ro_Data.XMATCH1 = "부적합"

            If ro_Data.XMATCH2 = "-" Then ro_Data.XMATCH2 = "적합"
            If ro_Data.XMATCH2 = "+" Then ro_Data.XMATCH2 = "부적합"

            If ro_Data.XMATCH3 = "-" Then ro_Data.XMATCH3 = "적합"
            If ro_Data.XMATCH3 = "+" Then ro_Data.XMATCH3 = "부적합"

            If ro_Data.XMATCH4 = "-" Then ro_Data.XMATCH4 = "적합"
            If ro_Data.XMATCH4 = "+" Then ro_Data.XMATCH4 = "부적합"

            Dim sXmatcd As String = ""

            'If ro_Data.XMATCH4 = "부적합" Then
            '    sXmatcd = "부적합"
            'Else
            '    If ro_Data.XMATCH3 = "부적합" Then
            '        sXmatcd = "부적합"
            '    Else
            '        If ro_Data.XMATCH2 = "부적합" Then
            '            sXmatcd = "부적합"
            '        ElseIf ro_Data.XMATCH2 = "적합" Then
            '            sXmatcd = "적합"
            '        Else
            '            If ro_Data.XMATCH1 = "부적합" Then
            '                sXmatcd = "부적합"
            '            Else
            '                sXmatcd = "적합"
            '            End If
            '        End If
            '    End If
            'End If

            If ro_Data.XMATCH1 = "부적합" Then
                sXmatcd = "부적합"
            ElseIf ro_Data.XMATCH1 = "적합" Then
                sXmatcd = "적합"
                If ro_Data.XMATCH2 = "" Then

                ElseIf ro_Data.XMATCH2 = "부적합" Then
                    sXmatcd = "부적합"
                ElseIf ro_Data.XMATCH2 = "적합" Then
                    sXmatcd = "적합"
                    If ro_Data.XMATCH3 = "" Then

                    ElseIf ro_Data.XMATCH3 = "부적합" Then
                        sXmatcd = "부적합"
                    ElseIf ro_Data.XMATCH3 = "적합" Then
                        sXmatcd = "적합"
                        If ro_Data.XMATCH4 = "" Then

                        ElseIf ro_Data.XMATCH4 = "부적합" Then
                            sXmatcd = "부적합"
                        ElseIf ro_Data.XMATCH4 = "적합" Then
                            sXmatcd = "적합"
                        End If
                    End If
                End If
            End If

            ''sPrtBuf = sPrtBuf + "A190,185,0,8,1,1,N," + Chr(34) + sRst1 + Chr(34) + vbCrLf
            'sPrtBuf = sPrtBuf + "A190,215,0,8,1,1,N," + Chr(34) + ro_Data.XMATCH2 + Chr(34) + vbCrLf
            'sPrtBuf += Chr(27) + "V" + "0320" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.XMATCH1 + " ") + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0320" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + fnGet_Hangle_Font_3(sXmatcd + " ") + vbCrLf

            ''-- 혈액번호
            If ro_Data.BLDNO.Count = 1 Then
                'sPrtBuf = sPrtBuf + "A500,180,0,3,1,1,N," + Chr(34) + ro_Data.BLDNO(0) + Chr(34) + vbCrLf
                Dim blodno As String = ""
                blodno = ro_Data.BLDNO(0).ToString.Replace("-", "").Substring(0, 4) + "-" + ro_Data.BLDNO(0).ToString.Replace("-", "").Substring(4)
                sPrtBuf += Chr(27) + "V" + "0380" + Chr(27) + "H" + (180).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + blodno + vbCrLf
            Else
                For ix As Integer = 0 To ro_Data.BLDNO.Count - 1
                    Dim blodno As String = ""
                    blodno = ro_Data.BLDNO(ix).ToString.Replace("-", "").Substring(0, 4) + "-" + ro_Data.BLDNO(ix).ToString.Replace("-", "").Substring(4)
                    'sPrtBuf = sPrtBuf + "A500," + (150 + (ix * 30)).ToString + ",0,3,1,1,N," + Chr(34) + ro_Data.BLDNO(ix) + Chr(34) + vbCrLf
                    sPrtBuf += Chr(27) + "V" + "0" + (380 - (ix * 20)).ToString + Chr(27) + "H" + (180).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + blodno + vbCrLf
                Next
            End If

            '< IR
            'sIR = "1" : sFilter_in = "1"
            'sPrtBuf = sPrtBuf + "A190,263,0,3,1,1,N," + Chr(34) + IIf(ro_Data.IR = "1", "Y", "").ToString().PadLeft(1, " "c) + "/" + IIf(ro_Data.FITER = "1", "F", "").ToString + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0265" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + IIf(ro_Data.IR = "1", "Y", "").ToString().PadLeft(1, " "c) + "/" + IIf(ro_Data.FITER = "1", "F", "").ToString + vbCrLf
            '>

            ''< 확인
            'sPrtBuf = sPrtBuf + "A450,263,0,3,1,1,N," + Chr(34) + "OK" + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0265" + Chr(27) + "H" + (175).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "S" + "OK" + vbCrLf
            ''>

            ''< 검사자
            'sPrtBuf = sPrtBuf + "A180,303,0,8,1,1,N," + Chr(34) + ro_Data.BEFOUTNM + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0220" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.BEFOUTNM + " ") + vbCrLf

            ''< 검사일자
            'sPrtBuf = sPrtBuf + "A500,303,0,2,1,1,N," + Chr(34) + ro_Data.BEFOUTDT + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0220" + Chr(27) + "H" + (175).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "S" + ro_Data.BEFOUTDT + vbCrLf


            ''< 출고자
            'sPrtBuf = sPrtBuf + "A180,343,0,8,1,1,N," + Chr(34) + ro_Data.OUTNM + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0175" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.OUTNM + " ") + vbCrLf


            ''< 출고일자
            'sPrtBuf = sPrtBuf + "A500,343,0,2,1,1,N," + Chr(34) + ro_Data.OUTDT + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0175" + Chr(27) + "H" + (175).ToString("D4") + Chr(27) + "L0102" + Chr(27) + "S" + ro_Data.OUTDT + vbCrLf


            ''< 수령자
            'sPrtBuf = sPrtBuf + "A180,383,0,8,1,1,N," + Chr(34) + ro_Data.RECNM + Chr(34) + vbCrLf
            sPrtBuf += Chr(27) + "V" + "0110" + Chr(27) + "H" + (510).ToString("D4") + Chr(27) + "L0101" + Chr(27) + "M" + fnGet_Hangle_Font_3(ro_Data.RECNM + " ") + vbCrLf



            ''< 라인 마지막 
            'sPrtBuf = sPrtBuf & "P1" + vbCrLf
            ''>  
            sPrtBuf += Chr(27) + "Q" + "1" + vbCrLf
            sPrtBuf += Chr(27) + "Z"

            Return sPrtBuf

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)

            Return ""

        End Try
    End Function

    Protected Overridable Function fnMakePrtMsg_PIS(ByVal ro_Data As STU_BCPRTINFO, _
                                                    ByVal rbFirst As Boolean, _
                                                    ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                                    ByVal rsBarType As String) As String
        'Dim sFn As String = "fnMakePrtMsg_PIS"

        'Try

        '    Dim a_sInfInfo As String() = ro_Data.INFINFO.Split("/"c)
        '    Dim sTestNms As String = ro_Data.TESTNMS


        '    If ro_Data.TESTNMS.Length > 20 Then
        '        sTestNms = ro_Data.TESTNMS.Substring(0, 25) + "..."
        '    End If

        '    If sTestNms.IndexOf("...") > -1 Then
        '        If sTestNms.Substring(0, sTestNms.IndexOf("...")).Length > 25 Then
        '            sTestNms = sTestNms.Substring(0, 25) + "..."
        '        End If
        '    End If

        '    If ro_Data.PATNM.Length > 4 Then ro_Data.PATNM = ro_Data.PATNM.Substring(0, 4)
        '    If ro_Data.TGRPNM.Length > 20 Then ro_Data.TGRPNM = ro_Data.TGRPNM.Substring(0, 20)

        '    Dim sPrtBuf As String = ""

        '    sPrtBuf = ""
        '    sPrtBuf += Chr(2) + "qA" + vbCrLf       '-- Clear Memory
        '    sPrtBuf += Chr(2) + "XA" + vbCrLf       '-- Fonts
        '    sPrtBuf += Chr(2) + "m" + vbCrLf        '-- Metric Mode 
        '    sPrtBuf += Chr(2) + "f680" + vbCrLf     '-- Position(Backfedd)


        '    If ro_Data.BCNOPRT = "" Then
        '        Dim sTmp As String = "미채혈바코드"

        '        For ix As Integer = 0 To sTmp.Length - 1
        '            sPrtBuf += Chr(2) + "ICF " + "IMG" + (ix + 1).ToString.PadLeft(2, "0"c) + Chr(13) + fnGet_FontImage(sTmp.Substring(ix, 1), New Font("굴림", 18, FontStyle.Bold), 32, 32)
        '        Next
        '    End If

        '    For ix As Integer = 0 To ro_Data.PATNM.Length - 1
        '        sPrtBuf += Chr(2) + "ICF " + "IMG" + (ix + 21).ToString.PadLeft(2, "0"c) + Chr(13) + fnGet_FontImage(ro_Data.PATNM.Substring(ix, 1), New Font("굴림", 18, FontStyle.Bold), 32, 32)
        '    Next

        '    ''-- 검사명
        '    'For ix As Integer = 0 To sTestNms.Length - 1
        '    '    sPrtBuf += Chr(2) + "ICF " + "IM" + (ix + 101).ToString.PadLeft(3, "0"c) + Chr(13) + fnGet_FontImage(sTestNms.Substring(ix, 1), New Font("굴림", 15, FontStyle.Regular), 30, 30)
        '    'Next

        '    ''-- 추가처방인 경우(원처방명)
        '    'For ix As Integer = 0 To ro_Data.TGRPNM.Length - 1
        '    '    sPrtBuf += Chr(2) + "ICF " + "IM" + (ix + 201).ToString.PadLeft(3, "0"c) + Chr(13) + fnGet_FontImage(ro_Data.TGRPNM.Substring(ix, 1), New Font("굴림", 15, FontStyle.Regular), 30, 30)
        '    'Next

        '    sPrtBuf += Chr(2) + "L" + vbCrLf

        '    sPrtBuf += "D11" + vbCrLf               '-- Set Dot Size
        '    sPrtBuf += "H19" + vbCrLf               '-- Header Setting
        '    sPrtBuf += "R0000" + vbCrLf             '-- Set Row Offset Amount

        '    '< 검체번호 
        '    sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNO + vbCrLf

        '    ''< 바코드 발행 일시  233
        '    If rbFirst Then
        '        sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (610 + riLeftPos).ToString.PadLeft(4, "0"c) + Fn.GetServerDateTime.ToString("yyyy-MM-dd HH:mm") + vbCrLf
        '    Else
        '        sPrtBuf += "A5" + vbCrLf
        '        sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0260" + (610 + riLeftPos).ToString.PadLeft(4, "0"c) + Fn.GetServerDateTime.ToString("yyyy-MM-dd HH:mm") + vbCrLf
        '        sPrtBuf += "A1" + vbCrLf
        '    End If

        '    '< 감염정보  

        '    For iCnt As Integer = 0 To a_sInfInfo.Length - 1
        '        If iCnt > 1 Then Exit For
        '        sPrtBuf += "1" + "9" + "2" + "2" + "001" + (190 - (iCnt * 35)).ToString.PadLeft(4, "0"c) + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + a_sInfInfo(iCnt).ToString() + vbCrLf
        '    Next

        '    '< 바코드  
        '    If ro_Data.BCNOPRT <> "" Then
        '        ' CODAR BAR 
        '        'sPrtBuf += "1" + "I" + "3" + "2" + "120" + "0115" + (350 + riLeftPos).ToString.PadLeft(4, "0"c) + "A" + ro_Data.BCNOPRT + "A" + vbCrLf
        '        sPrtBuf += "1" + "I" + "4" + "2" + "100" + "0140" + (370 + riLeftPos).ToString.PadLeft(4, "0"c) + "A" + ro_Data.BCNOPRT + "A" + vbCrLf

        '        ' code128
        '        'sPrtBuf += "1" + "e" + "1" + "2" + "120" + "0115" + (340 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNOPRT + vbCrLf

        '        '< 바코드 번호
        '        'sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0110" + (470 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.BCNOPRT + vbCrLf
        '    Else
        '        '< 미수납 바코드 
        '        For ix As Integer = 0 To "미채혈바코드".Length - 1
        '            sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0180" + (400 + (ix * 32) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 1).ToString.PadLeft(2, "0"c) + vbCrLf
        '        Next
        '        '>  
        '    End If

        '    '< 등록번호 sPID
        '    If ro_Data.BCCLSCD.Substring(0, 1).Trim() = "R" Then
        '        sPrtBuf += "A5" + vbCrLf
        '        sPrtBuf += "1" + "9" + "1" + "2" + "003" + "0095" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.REGNO + vbCrLf
        '        sPrtBuf += "A1" + vbCrLf
        '    Else
        '        sPrtBuf += "1" + "9" + "1" + "2" + "002" + "0095" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.REGNO + vbCrLf
        '    End If

        '    ''< 진료과/병동/병실
        '    sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0110" + (600 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.DEPTWARD + vbCrLf

        '    '< 성별/나이 
        '    sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0090" + (700 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.SEXAGE + vbCrLf

        '    ''< 환자명 
        '    For ix As Integer = 0 To ro_Data.PATNM.Length - 1
        '        sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0092" + (450 + (ix * 32) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IMG" + (ix + 21).ToString.PadLeft(2, "0"c) + vbCrLf
        '    Next

        '    '< 검체순번
        '    sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0060" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.REMARK + vbCrLf

        '    '< 검체명
        '    sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0065" + (430 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.SPCNM + vbCrLf

        '    '< 검사명
        '    sPrtBuf += "1" + "9" + "1" + "1" + "002" + "0010" + (300 + riLeftPos).ToString.PadLeft(4, "0"c) + sTestNms + vbCrLf
        '    'For ix As Integer = 0 To sTestNms.Length - 1
        '    '    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0020" + (250 + (ix * 25) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IM" + (ix + 101).ToString.PadLeft(3, "0"c) + vbCrLf
        '    'Next

        '    '< 추가처방명
        '    'sPrtBuf += "1" + "9" + "1" + "1" + "001" + "0005" + (300 + riLeftPos).ToString.PadLeft(4, "0"c) + ro_Data.TGRPNM + vbCrLf
        '    'For ix As Integer = 0 To ro_Data.TGRPNM.Length - 1
        '    '    sPrtBuf += "1" + "Y" + "1" + "1" + "000" + "0055" + (250 + (ix * 25) + riLeftPos).ToString.PadLeft(4, "0"c) + " " + "IM" + (ix + 201).ToString.PadLeft(3, "0"c) + vbCrLf
        '    'Next

        '    sPrtBuf += "1" + "9" + "2" + "2" + "002" + "0005" + (260 + riLeftPos).ToString.PadLeft(4, "0"c) + "P" + vbCrLf

        '    '< 라인 마지막 
        '    sPrtBuf += "Q0001" + vbCrLf
        '    sPrtBuf += "E"

        '    Return sPrtBuf

        'Catch ex As Exception
        '    Fn.log(mcFile + sFn, Err)
        '    MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        '    Return ""
        'Finally

        'End Try

    End Function

    Public Function fnGet_Hangle_Font_1(ByVal rsValue As String) As String
        '한글 변환(KS-5601)
        Try
            Dim btBuf As Byte() = System.Text.Encoding.Default.GetBytes(rsValue)

            Dim sFont As String = ""
            Dim ix As Integer = 0
            Dim iPos As Integer = 0

            Do While ix < btBuf.Length - 1
                If btBuf(ix) > 128 Then
                    sFont += Chr(27) + "K2B" + Chr(btBuf(ix) - 128) + Chr(btBuf(ix + 1) - 128)
                    ix += 2
                Else
                    sFont += Chr(27) + "M" + rsValue.Substring(iPos, 1)
                    ix += 1
                End If

                iPos += 1
            Loop

            Return sFont

        Catch ex As Exception

            Return ""
            MsgBox(ex.Message)
        End Try

    End Function

    Public Function fnGet_Hangle_Font_2(ByVal rsValue As String) As String
        '한글 바탕(명조)
        Try
            Dim btBuf As Byte() = System.Text.Encoding.Default.GetBytes(rsValue)

            Dim sFont As String = ""
            Dim ix As Integer = 0
            Dim iPos As Integer = 0

            Do While ix < btBuf.Length
                If btBuf(ix) > 128 Then
                    sFont += Chr(27) + "PR" + Chr(27) + "RF010002," + rsValue.Substring(iPos, 1)
                    ix += 2
                Else
                    sFont += Chr(27) + "PS" + Chr(27) + "RF010002," + "0" + rsValue.Substring(iPos, 1)
                    ix += 1
                End If
                iPos += 1
            Loop

            Return sFont

        Catch ex As Exception
            Return ""
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function fnGet_Hangle_Font_3(ByVal rsValue As String) As String
        '한글(굴림(고딕))
        Try
            Dim btBuf As Byte() = System.Text.Encoding.Default.GetBytes(rsValue)

            Dim sFont As String = ""
            Dim ix As Integer = 0
            Dim iPos As Integer = 0

            Do While ix < btBuf.Length
                If btBuf(ix) > 128 Then
                    'sFont += Chr(27) + "K2B" + Chr(btBuf(ix) - 128) + Chr(btBuf(ix + 1) - 128)
                    'sFont += Chr(27) + "PR" + Chr(27) + "RF010002," + rsValue.Substring(iPos, 1)
                    sFont += Chr(27) + "PR" + Chr(27) + "RF020002," + rsValue.Substring(iPos, 1)
                    ix += 2
                Else
                    sFont += Chr(27) + "PS" + Chr(27) + "RF020002," + "0" + rsValue.Substring(iPos, 1)
                    ix += 1
                End If
                iPos += 1
            Loop

            Return sFont

        Catch ex As Exception
            Return ""
            MsgBox(ex.Message)
        End Try
    End Function

    Private Function fnPrint_Send(ByVal rsPrtNm As String, ByVal rsPrtMsg As String) As Boolean
        'Dim sFn As String = "fnPrint_Send"
        'Try

        '    Dim lhPrinter As Long = 0
        '    Dim lReturn As Long = 0
        '    Dim lpcWritten As Long = 0
        '    Dim lDoc As Long = 0
        '    Dim MyDocInfo As New DOCINFO
        '    Dim lLen As Long = 0

        '    lpcWritten = 0
        '    rsPrtMsg += vbFormFeed

        '    '망할!!! 이 한줄 코딩 알아 내려고 반나절 이상을 보냈다!! ㅡㅜ
        '    '버퍼의 길이를 필요 이상으로 보낼 경우 오류가 나며, 그저 바이트 숫자로 세도 문제가 된다
        '    '밑에 코드로 해결해야 한다
        '    lLen = LengthH(StrConv(rsPrtMsg, VbStrConv.Narrow))

        '    lReturn = OpenPrinter(rsPrtNm, lhPrinter, 0)
        '    If lReturn = 0 Then
        '        Throw (New Exception("프린터 설정이 잘 못 되었습니다" + vbCrLf + "프린터명을 정확히 입력하여 주십시요."))
        '        Return False
        '    End If

        '    MyDocInfo.pDocName = "barcod"
        '    MyDocInfo.pOutputFile = vbNullString
        '    MyDocInfo.pDataType = vbNullString
        '    lReturn = StartDocPrinter(lhPrinter, 1, MyDocInfo)
        '    Call StartPagePrinter(lhPrinter)

        '    lReturn = WritePrinter(lhPrinter, rsPrtMsg, lLen, lpcWritten)

        '    lReturn = EndPagePrinter(lhPrinter)
        '    lReturn = EndDocPrinter(lhPrinter)
        '    lReturn = ClosePrinter(lhPrinter)
        '    Return True

        'Catch ex As Exception
        '    Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        '    Return False
        'End Try

    End Function

    '<< ##############################################################################################
    '<< yjlee 2008-02-13 물품 바코드프린터-----------------------------------------------------------S
    '<< ##############################################################################################
    Public Overridable Function BarCodePrtOut_Goods(ByVal asSndMsg As ArrayList, ByVal aiPrtCnt As Integer, ByVal abSharedPrinter As Boolean, ByVal asPrinterPort As String, ByVal asIPAddress As String, ByVal asSharedNm As String, ByVal aiPortNo As Integer) As Boolean
        Return True
    End Function

End Class


