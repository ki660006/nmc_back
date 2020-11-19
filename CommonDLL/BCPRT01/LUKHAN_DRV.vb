Imports COMMON.SVar
Imports COMMON.CommFN
Imports COMMON.CommFN.Fn
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommPrint

Public Class LUKHAN_DRV

    ' Line Pen Type
    Public Const PS_SOLID As Integer = 0
    Public Const PS_DASH As Integer = 1
    Public Const PS_DOT As Integer = 2
    Public Const PS_DASHDOT As Integer = 3
    Public Const PS_DASHDOTDOT As Integer = 4


    Public Declare Function LK_OpenPrinter Lib "LKBSDK.dll" (ByVal PrinterName As String) As Integer
    Public Declare Function LK_ClosePrinter Lib "LKBSDK.dll" () As Integer
    Public Declare Function LK_StartPage Lib "LKBSDK.dll" () As Integer
    Public Declare Function LK_EndPage Lib "LKBSDK.dll" () As Integer
    Public Declare Function LK_SetupPrinter Lib "LKBSDK.dll" (ByVal LabelWidth As String, ByVal LabelLength As String, ByVal MediaType As Integer, ByVal GapHeight As String, ByVal Offset As String, ByVal Density As Integer, ByVal Speed As Integer, ByVal Copies As Integer) As Integer
    Public Declare Function LK_PrintWindowsFont Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Degree As Integer, ByVal Height As Integer, ByVal Weight As Integer, ByVal Italic As Integer, ByVal Underline As Integer, ByVal TypeFace As String, ByVal Data As String) As Integer
    Public Declare Function LK_PrintBMP Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal FileName As String) As Integer
    Public Declare Function LK_PrintPCX Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal FileName As String) As Integer
    Public Declare Function LK_PrintDeviceFont Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rotation As Integer, ByVal FontNumber As Integer, ByVal HorExpand As Integer, ByVal VerExpand As Integer, ByVal Reverse As Integer, ByVal Data As String) As Integer
    Public Declare Function LK_PrintBarCode Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Rotation As Integer, ByVal BarCode As String, ByVal NarrowWidth As Integer, ByVal WideWidth As Integer, ByVal BarHeight As Integer, ByVal Readable As Integer, ByVal Data As String) As Integer
    Public Declare Function LK_PrintLine Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal HoriSize As Integer, ByVal VertSize As Integer) As Integer
    Public Declare Function LK_PrintDiagonalLine Lib "LKBSDK.dll" (ByVal StartX As Integer, ByVal StartY As Integer, ByVal EndX As Integer, ByVal EndY As Integer, ByVal Thick As Integer) As Integer
    Public Declare Function LK_PrintBox Lib "LKBSDK.dll" (ByVal StartX As Integer, ByVal StartY As Integer, ByVal EndX As Integer, ByVal EndY As Integer, ByVal Thick As Integer) As Integer
    Public Declare Function LK_PrintDate Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Degree As Integer, ByVal Height As Integer, ByVal Weight As Integer, ByVal Italic As Integer, ByVal Underline As Integer, ByVal TypeFace As String, ByVal DateFormat As Integer) As Integer
    Public Declare Function LK_PrintTime Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Degree As Integer, ByVal Height As Integer, ByVal Weight As Integer, ByVal Italic As Integer, ByVal Underline As Integer, ByVal TypeFace As String, ByVal TimeFormat As Integer) As Integer
    Public Declare Function LK_SetupPrinterCutter Lib "LKBSDK.dll" (ByVal LabelWidth As String, ByVal LabelLength As String, ByVal MediaType As Integer, ByVal GapHeight As String, ByVal Offset As String, ByVal Density As Integer, ByVal Speed As Integer, ByVal Copies As Integer, ByVal Rotation As Integer, ByVal Cutting As Integer, ByVal CutMethod As Integer, ByVal CutPageInterval As Integer, ByVal FeedAfterCut As String) As Integer
    Public Declare Function LK_DrawLine Lib "LKBSDK.dll" (ByVal LineType As Integer, ByVal sx As Integer, ByVal sy As Integer, ByVal ex As Integer, ByVal ey As Integer, ByVal Thick As Integer) As Integer
    Public Declare Function LK_Rectangle Lib "LKBSDK.dll" (ByVal LineType As Integer, ByVal sx As Integer, ByVal sy As Integer, ByVal ex As Integer, ByVal ey As Integer, ByVal Thick As Integer) As Integer
    Public Declare Function LK_Ellipse Lib "LKBSDK.dll" (ByVal LineType As Integer, ByVal sx As Integer, ByVal sy As Integer, ByVal ex As Integer, ByVal ey As Integer, ByVal Thick As Integer) As Integer
    Public Declare Function LK_PrintWindowsFontAlign Lib "LKBSDK.dll" (ByVal Alignment As Integer, ByVal PosY As Integer, ByVal Degree As Integer, ByVal Height As Integer, ByVal Weight As Integer, ByVal Italic As Integer, ByVal Underline As Integer, ByVal TypeFace As String, ByVal Data As String) As Integer
    Public Declare Function LK_PrintWindowsFontPitch Lib "LKBSDK.dll" (ByVal PosX As Integer, ByVal PosY As Integer, ByVal Degree As Integer, ByVal Height As Integer, ByVal Width As Integer, ByVal Weight As Integer, ByVal Italic As Integer, ByVal Underline As Integer, ByVal TypeFace As String, ByVal Data As String) As Integer

    Private Const msFile As String = "File : LUKHAN_DRV.vb, Class : LUKHAN_DRV" + vbTab


    Public Overridable Function BarCodePrtOut(ByVal ra_PrtData As ArrayList, _
                                              ByVal rsPrintPort As String, ByVal rsSocketIP As String, ByVal rbFirst As Boolean, _
                                              Optional ByVal riLeftPos As Integer = 0, _
                                              Optional ByVal riTopPos As Integer = 0, _
                                              Optional ByVal rsBarType As String = "CODABAR") As Boolean
        Dim sFn As String = "BarCodePrtOut"
        Dim bReturn As Boolean = False
        Dim iFileNo As Integer = 0

        Try
            'If LK_OpenPrinter(rsPrintPort) Then
            '    LK_SetupPrinter("55.0", "30.0", 0, "4.0", "0", 8, 6, 1)
            '    LK_ClosePrinter()
            'End If

            For ix1 As Integer = 0 To ra_PrtData.Count - 1
                If CType(ra_PrtData(ix1), STU_BCPRTINFO).REGNO <> "" Then
                    Dim iPrtCnt As Integer = 1

                    If CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "A" Then
                        iPrtCnt = 2
                    ElseIf CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "B" Then
                        '< CrossMatching ��ü
                        iPrtCnt = 3
                    ElseIf IsNumeric(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT) Then
                        iPrtCnt = Convert.ToInt32(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT)
                    End If

                    For ix2 As Integer = 1 To iPrtCnt

                        If LK_OpenPrinter(rsPrintPort) = 0 Then

                            Dim sPrtMsg = fnMakePrtMsg(CType(ra_PrtData(ix1), STU_BCPRTINFO), rbFirst, riLeftPos, riTopPos, rsBarType)

                            LK_ClosePrinter()

                        End If

                        Threading.Thread.Sleep(1000)
                    Next
                End If
            Next

            Return True
        Catch ex As Exception
            LK_ClosePrinter()
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return False
        Finally
            FileClose(iFileNo)

        End Try
    End Function

    Public Overridable Function BarCodePrtOut_BLD(ByVal roSndMsg As ArrayList, ByVal riPrtCnt As Integer, _
                                                  Optional ByVal rsIP As String = "127.0.0.1", _
                                                  Optional ByVal rsPrintPort As String = "9100", _
                                                  Optional ByVal rsOUTPUT As String = "", _
                                                  Optional ByVal riLeftPos As Integer = 0, Optional ByVal riTopPos As Integer = 0, _
                                                  Optional ByVal rbFirst As Boolean = False) As Boolean
        Dim sFn As String = "BarCodePrtOut_BLD"
        Dim bReturn As Boolean = False
        Dim iFileNo As Integer = 0

        Try

            If roSndMsg Is Nothing Then
            Else
                'riPrtCnt = 10
                For ix1 As Integer = 0 To roSndMsg.Count - 1

                    ' For ix2 As Integer = 1 To riPrtCnt '<<<20180125 ��ĭ�� ��� �տ��� �̸� ��� ����ϼ� roSndMsg �� ������ŭ ��ƿ� 
                    If LK_OpenPrinter(rsPrintPort) = 0 Then
                        Dim sPrtMsg = fnMakePrtMsg_BLD(CType(roSndMsg(ix1), STU_BLDLABEL), riLeftPos, riTopPos)
                        LK_ClosePrinter()
                    End If
                    ' Next'

                    Threading.Thread.Sleep(1000)
                Next
            End If


            Return True
        Catch ioex As System.IO.IOException
            ''������ �ٸ� ���μ������� ��� ���̹Ƿ� ���μ������� �׼����� �� �����ϴ�.
            'If Err.Number = 75 Then
            '    'Recursive Call
            '    BarCodePrtOut(asSndMsg, aiPrtCnt, asIP, aiPort)
            'End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
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
            'If LK_OpenPrinter(rsPrintPort) Then
            '    LK_SetupPrinter("55.0", "30.0", 0, "4.0", "0", 8, 6, 1)
            '    LK_ClosePrinter()
            'End If

            For ix1 As Integer = 0 To ra_PrtData.Count - 1
                If CType(ra_PrtData(ix1), STU_BCPRTINFO).REGNO <> "" Then
                    Dim iPrtCnt As Integer = Convert.ToInt32(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT)

                    For ix2 As Integer = 1 To iPrtCnt
                        '-- ��ü����(������ ���)
                        CType(ra_PrtData(ix1), STU_BCPRTINFO).REMARK = ix2.ToString + "/" + CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT

                        If LK_OpenPrinter(rsPrintPort) = 0 Then
                            Dim sPrtMsg = fnMakePrtMsg_PIS(CType(ra_PrtData(ix1), STU_BCPRTINFO), rbFirst, riLeftPos, riTopPos, rsBarType)
                            LK_ClosePrinter()
                        End If

                        Threading.Thread.Sleep(1000)
                    Next
                End If
            Next

            Return True
        Catch ex As Exception
            LK_ClosePrinter()
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
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
            'If LK_OpenPrinter(rsPrintPort) Then
            '    LK_SetupPrinter("55.0", "30.0", 0, "4.0", "0", 8, 6, 1)
            '    LK_ClosePrinter()
            'End If

            For ix1 As Integer = 0 To ra_PrtData.Count - 1
                If CType(ra_PrtData(ix1), STU_BCPRTINFO).REGNO <> "" Then
                    Dim iPrtCnt As Integer = 1

                    If CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "A" Then
                        iPrtCnt = 2
                    ElseIf CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT = "B" Then
                        '< CrossMatching ��ü
                        iPrtCnt = 3
                    ElseIf IsNumeric(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT) Then
                        iPrtCnt = Convert.ToInt32(CType(ra_PrtData(ix1), STU_BCPRTINFO).BCCNT)
                    End If

                    For ix2 As Integer = 1 To iPrtCnt

                        If LK_OpenPrinter(rsPrintPort) = 0 Then
                            Dim sPrtMsg = fnMakePrtMsg_RIS(CType(ra_PrtData(ix1), STU_BCPRTINFO), rbFirst, riLeftPos, riTopPos, rsBarType)
                            LK_ClosePrinter()
                        End If

                        Threading.Thread.Sleep(1000)
                    Next
                End If
            Next

            Return True
        Catch ex As Exception
            LK_ClosePrinter()
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return False
        Finally
            FileClose(iFileNo)

        End Try
    End Function
    Protected Overridable Function fnMakePrtMsg_new(ByVal ro_Data As STU_BCPRTINFO, _
                                              ByVal rbFirst As Boolean, _
                                              ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                              ByVal rsBarType As String) As String
        Dim sFn As String = "fnMakePrtMsg"

        Try

            Dim sTestNms As String = ro_Data.TESTNMS
            Dim iTop As Integer = riTopPos - 5

            riLeftPos += 20

            If ro_Data.TESTNMS.Length > 20 Then
                sTestNms = sTestNms.Substring(0, 19) & "..."
            End If

            If sTestNms.IndexOf("...") > -1 Then
                If sTestNms.Substring(0, sTestNms.IndexOf("...")).Length > 20 Then
                    sTestNms = sTestNms.Substring(0, 19) & "..."
                End If
            End If

            Dim lgRtn As Integer = 0

            lgRtn = LK_SetupPrinter("53.0", "35.0", 0, "3.0", "0", 8, 6, 1)
            If lgRtn <> 0 Then Return ""

            '< ���� ����  
            LK_StartPage()

            Dim sSpcNm As String = ro_Data.SPCNM
            Dim sTubeNm As String = ro_Data.TUBENM
            Dim sTGrpNm As String = ro_Data.TGRPNM

            If ro_Data.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Or ro_Data.BCCNT = "B" Then
                '< ��ü��
                'LK_PrintWindowsFont(12 + riLeftPos, 210 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)
                LK_PrintWindowsFont(12 + riLeftPos, 210 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)
                '< ���� 
                LK_PrintWindowsFont(90 + riLeftPos, 210 + iTop, 0, 20, 0, 0, 0, "����", sTubeNm)
                '< ä����
                LK_PrintWindowsFont(240 + riLeftPos, 215 + iTop, 0, 25, 0, 0, 0, "����", "ä����: ")
                ''< Ȯ����
                'LK_PrintWindowsFont(260 + riLeftPos, 240 + iTop, 0, 25, 0, 0, 0, "����", "Ȯ����: ")
                '< ����
                LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 4, 1, 1, 1, " X-Matching ")

            ElseIf PRG_CONST.BCCLS_BloodBank.StartsWith(ro_Data.BCCLSCD.Substring(0, 1)) Then
                '< ��ü��
                LK_PrintWindowsFont(12 + riLeftPos, 215 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)

                '< ���� 
                LK_PrintWindowsFont(90 + riLeftPos, 215 + iTop, 0, 20, 0, 0, 0, "����", sTubeNm)
                '< ä����
                LK_PrintWindowsFont(240 + riLeftPos, 225 + iTop, 0, 25, 0, 0, 0, "����", "ä����: ")
                ''< Ȯ����
                'LK_PrintWindowsFont(260 + riLeftPos, 240 + iTop, 0, 25, 0, 0, 0, "����", "Ȯ����: ")
                '< �˻��׸�(����)
                If sTestNms.Length > 12 Then sTestNms = sTestNms.Substring(0, 12)
                LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 4, 1, 1, 1, " " + sTestNms + " ")
            Else
                '< ��ü��*
                'LK_PrintWindowsFont(12 + riLeftPos, 220 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)
                LK_PrintWindowsFont(150 + riLeftPos, 220 + iTop, 0, 20, 0, 0, 0, "����", "ABCDEABCDE")


                If ro_Data.BCTYPE = "M" Then

                    '< �˻�׷� sComment2
                    LK_PrintWindowsFont(220 + riLeftPos, 220 + iTop, 0, 20, 0, 0, 0, "����", sTGrpNm)

                    '< �̻��� ��ü��ȣ
                    LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 4, 1, 1, 0, ro_Data.BCNO_MB)
                Else

                    '< ���� *
                    LK_PrintWindowsFont(12 + riLeftPos, 220 + iTop, 0, 20, 0, 0, 0, "����", "ABCDEABCDE")
                    'LK_PrintWindowsFont(90 + riLeftPos, 250 + iTop, 0, 20, 0, 0, 0, "����", ro_Data.TUBENM)

                    '< �˻�׷� sComment2
                    LK_PrintDeviceFont(290 + riLeftPos, 220 + iTop, 0, 8, 1, 1, 0, ro_Data.TGRPNM)

                    '< ���� sEmer 
                    LK_PrintDeviceFont(355 + riLeftPos, 130 + iTop, 0, 2, 1, 1, 1, ro_Data.EMER)

                    '< �˻��׸�� *
                    'LK_PrintDeviceFont(55 + riLeftPos, 260 + iTop, 0, 1, 1, 1, 0, sTestNms)
                    '<<<20170912
                    LK_PrintWindowsFont(55 + riLeftPos, 260 + iTop, 0, 20, 0, 0, 0, "����", "ABCDEABCDEABCDEABCDE")
                    'LK_PrintWindowsFont 20170911
                    '< �� sKind
                    LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 6, 2, 2, 0, ro_Data.BCCLSCD)
                End If
            End If

            '< ��ü��ȣ 
            'LK_PrintDeviceFont(6 + riLeftPos, 45 + iTop, 0, 15, 1, 0, 0, ro_Data.BCNO)
            LK_PrintWindowsFont(6 + riLeftPos, 45 + iTop, 0, 15, 1, 0, 0, "����", ro_Data.BCNO)

            '<��ȣ �ּ� 20150602
            ''< ���ڵ� ���� �Ͻ�  233
            'If rbFirst Then
            '    LK_PrintDeviceFont(260 + riLeftPos, 45 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("MM-dd HH:mm"))
            'Else
            '    LK_PrintDeviceFont(260 + riLeftPos, 45 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("HH:mm"))
            'End If
            '>

            '<��ȣ ����� 20150602
            '< ���ڵ� ���� �Ͻ�  233
            If rbFirst Then
                LK_PrintDeviceFont(260 + riLeftPos, 260 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("MM-dd HH:mm"))
            Else
                LK_PrintDeviceFont(260 + riLeftPos, 260 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("HH:mm"))
            End If
            '>

            '< ��������  
            Dim a_sInfInfo As String() = ro_Data.INFINFO.Split("/"c)

            ''<��ȣ �ּ� 20150602
            'For iCnt As Integer = 0 To a_sInfInfo.Length - 1
            '    If iCnt > 1 Then Exit For
            '    'LK_PrintDeviceFont(175 + riLeftPos, 45 + iTop, 0, 2, 1, 1, 0, a_sInfInfo(iCnt).ToString())
            '    LK_PrintWindowsFont(175 + riLeftPos, 45 + iTop, 1, 20, 1, 0, 0, "�ü�", a_sInfInfo(iCnt).ToString())
            'Next
            ''>

            ''<��ȣ ���� 20150602
            Dim InfInfoTmp As String = ro_Data.INFINFO

            If InfInfoTmp <> "" Then 'Null���� Replace ó�� �� ��� "��ü ������ ��ü �ν��Ͻ��� ���� ���� �ʾҴ�"�� ������ ���Ƿ� ���� ó��, ��ȣ 
                InfInfoTmp = Replace(InfInfoTmp, "/", ".")
                InfInfoTmp = Replace(InfInfoTmp, ",", ".")
            End If

            LK_PrintWindowsFont(220 + riLeftPos, 40 + iTop, 0, 25, 1, 0, 0, "�ü�", InfInfoTmp.ToString())

            '< ���ڵ�  
            If ro_Data.BCNOPRT <> "" Then
                ' Codabar                 
                LK_PrintBarCode(80 + riLeftPos, 65 + iTop, 0, "K", 2, 4, 90, 0, "A" + ro_Data.BCNOPRT + "A")

                '' code 128
                'LK_PrintBarCode(75 + riLeftPos, 61, 0, "1A", 2, 4, 90, 0, ro_Data.BCNOPRT)

                '< ���ڵ� ��ȣ
                Dim sBcnoPrt As String = ro_Data.BCNOPRT
                LK_PrintWindowsFont(160 + riLeftPos, 157 + iTop, 0, 15, 1, 0, 0, "����", sBcnoPrt)
            Else
                '< �̼��� ���ڵ� 
                LK_PrintWindowsFont(70 + riLeftPos, 100 + iTop, 0, 35, 1, 0, 0, "����", "��ä�����ڵ�")
                '>  
            End If

            '< ��Ϲ�ȣ sPID
            LK_PrintDeviceFont(9 + riLeftPos, 165 + iTop, 0, 3, 1, 2, CType(IIf(ro_Data.BCCLSCD.Substring(0, 1).Trim() = "R", "1", "0"), Integer), ro_Data.REGNO)

            '< �����/����/����  
            Dim sDptWard As String = ro_Data.DEPTWARD
            LK_PrintWindowsFont(270 + riLeftPos, 195 + iTop, 0, 20, 1, 0, 0, "����", sDptWard)

            '< ����/���� 
            Dim sSexAge As String = ro_Data.SEXAGE
            LK_PrintWindowsFont(320 + riLeftPos, 170 + iTop, 0, 18, 0, 0, 0, "����", sSexAge)

            '< ȯ�ڸ� 
            Dim sPatNm As String = ro_Data.PATNM
            LK_PrintWindowsFont(150 + riLeftPos, 180 + iTop, 0, 30, 0, 0, 0, "����", sPatNm)

            '< sRemark
            LK_PrintWindowsFont(350 + riLeftPos, 80 + iTop, 0, 25, 3, 0, 0, "����", IIf(ro_Data.REMARK <> "", "C", "").ToString)

            '< ���� ������ 
            LK_EndPage()

            Return ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return ""

        End Try

    End Function
    Protected Overridable Function fnMakePrtMsg(ByVal ro_Data As STU_BCPRTINFO, _
                                                ByVal rbFirst As Boolean, _
                                                ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                                ByVal rsBarType As String) As String
        Dim sFn As String = "fnMakePrtMsg"

        Try

            Dim sTestNms As String = ro_Data.TESTNMS
            Dim iTop As Integer = riTopPos - 5

            riLeftPos += 20

            If ro_Data.TESTNMS.Length > 20 Then
                sTestNms = sTestNms.Substring(0, 19) & "..."
            End If

            If sTestNms.IndexOf("...") > -1 Then
                If sTestNms.Substring(0, sTestNms.IndexOf("...")).Length > 20 Then
                    sTestNms = sTestNms.Substring(0, 19) & "..."
                End If
            End If

            Dim lgRtn As Integer = 0

            lgRtn = LK_SetupPrinter("53.0", "35.0", 0, "3.0", "0", 8, 6, 1)
            If lgRtn <> 0 Then Return ""

            '< ���� ����  
            LK_StartPage()

            Dim sSpcNm As String = ro_Data.SPCNM
            Dim sTubeNm As String = ro_Data.TUBENM
            Dim sTGrpNm As String = ro_Data.TGRPNM
            Dim Abochk As String = ro_Data.ABOCHK

            '���������� �׽�Ʈ
            LK_PrintWindowsFont(10 + riLeftPos, 85 + iTop, 0, 50, 0, 0, 0, "����", Abochk)

            If ro_Data.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Or ro_Data.BCCNT = "B" Then
                '< ��ü��
                'LK_PrintWindowsFont(12 + riLeftPos, 210 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)
                LK_PrintWindowsFont(12 + riLeftPos, 210 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)
                '< ���� 
                LK_PrintWindowsFont(90 + riLeftPos, 210 + iTop, 0, 20, 0, 0, 0, "����", sTubeNm)
                '< ä����
                LK_PrintWindowsFont(240 + riLeftPos, 215 + iTop, 0, 25, 0, 0, 0, "����", "ä����: ")
                LK_PrintBox(240 + riLeftPos, 220 + iTop, 430 + riLeftPos, 255 + iTop, 1)
                ''< Ȯ����
                'LK_PrintWindowsFont(260 + riLeftPos, 240 + iTop, 0, 25, 0, 0, 0, "����", "Ȯ����: ")
                '< ����
                LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 4, 1, 1, 1, " X-Matching ")

            ElseIf PRG_CONST.BCCLS_BloodBank.StartsWith(ro_Data.BCCLSCD.Substring(0, 1)) Then
                '< ��ü��
                LK_PrintWindowsFont(12 + riLeftPos, 215 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)

                '< ���� 
                LK_PrintWindowsFont(90 + riLeftPos, 215 + iTop, 0, 20, 0, 0, 0, "����", sTubeNm) 'ORI

                '< ä����
                LK_PrintWindowsFont(240 + riLeftPos, 225 + iTop, 0, 25, 0, 0, 0, "����", "ä����: ")
                LK_PrintBox(240 + riLeftPos, 220 + iTop, 430 + riLeftPos, 255 + iTop, 1)
                ''< Ȯ����
                'LK_PrintWindowsFont(260 + riLeftPos, 240 + iTop, 0, 25, 0, 0, 0, "����", "Ȯ����: ")
                '< �˻��׸�(����)
                If sTestNms.Length > 12 Then sTestNms = sTestNms.Substring(0, 12)
                LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 4, 1, 1, 1, " " + sTestNms + " ")
            Else
                '< ��ü��*
                'LK_PrintWindowsFont(12 + riLeftPos, 220 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)
                LK_PrintWindowsFont(90 + riLeftPos, 220 + iTop, 0, 20, 0, 0, 0, "����", sSpcNm)

                If ro_Data.BCTYPE = "M" Then

                    '< �˻�׷� sComment21123
                    LK_PrintWindowsFont(220 + riLeftPos, 220 + iTop, 0, 20, 0, 0, 0, "����", sTGrpNm)

                    '< �̻��� ��ü��ȣ
                    LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 4, 1, 1, 0, ro_Data.BCNO_MB)
                Else

                    '< ���� *
                    LK_PrintWindowsFont(12 + riLeftPos, 220 + iTop, 0, 20, 0, 0, 0, "����", ro_Data.TUBENM)
                    'LK_PrintWindowsFont(90 + riLeftPos, 250 + iTop, 0, 20, 0, 0, 0, "����", ro_Data.TUBENM)

                    '< �˻�׷� sComment2
                    LK_PrintDeviceFont(290 + riLeftPos, 220 + iTop, 0, 8, 1, 1, 0, ro_Data.TGRPNM)

                    '< ���� sEmer 
                    'LK_PrintDeviceFont(355 + riLeftPos, 80 + iTop, 0, 2, 1, 1, 1, ro_Data.EMER) '����
                    LK_PrintDeviceFont(355 + riLeftPos, 80 + iTop, 0, 5, 1, 1, 1, ro_Data.EMER) '����� ��û���� ũ�� ����

                    '< �˻��׸�� *
                    'LK_PrintDeviceFont(55 + riLeftPos, 260 + iTop, 0, 1, 1, 1, 0, sTestNms)
                    '<<<20170912
                    LK_PrintWindowsFont(55 + riLeftPos, 260 + iTop, 0, 20, 0, 0, 0, "����", sTestNms)
                    'LK_PrintWindowsFont 20170911
                    '< �� sKind
                    'LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 6, 1, 1, 0, ro_Data.BCCLSCD) 'ori
                    LK_PrintDeviceFont(15 + riLeftPos, 250 + iTop, 0, 3, 1, 1, 0, ro_Data.BCCLSCD) ' jjh ����

                    '< ��ü���� sErprt
                    If ro_Data.ERPRTYN <> "" Then
                        'LK_PrintDeviceFont(355 + riLeftPos, 130 + iTop, 0, 3, 1, 1, 1, "E")
                        LK_PrintDeviceFont(355 + riLeftPos, 130 + iTop, 0, 8, 1, 1, 1, "R")
                    End If

                End If
            End If

            '< ��ü��ȣ 
            'LK_PrintDeviceFont(6 + riLeftPos, 45 + iTop, 0, 15, 1, 0, 0, ro_Data.BCNO)
            LK_PrintWindowsFont(6 + riLeftPos, 45 + iTop, 0, 15, 1, 0, 0, "����", ro_Data.BCNO)

            '<��ȣ �ּ� 20150602
            ''< ���ڵ� ���� �Ͻ�  233
            'If rbFirst Then
            '    LK_PrintDeviceFont(260 + riLeftPos, 45 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("MM-dd HH:mm"))
            'Else
            '    LK_PrintDeviceFont(260 + riLeftPos, 45 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("HH:mm"))
            'End If
            '>

            '<��ȣ ����� 20150602
            '< ���ڵ� ���� �Ͻ�  233
            If rbFirst Then
                LK_PrintDeviceFont(260 + riLeftPos, 260 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("MM-dd HH:mm"))
            Else
                LK_PrintDeviceFont(260 + riLeftPos, 260 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("HH:mm"))
            End If
            '>

            '< ��������  
            Dim a_sInfInfo As String() = ro_Data.INFINFO.Split("/"c)

            ''<��ȣ �ּ� 20150602
            'For iCnt As Integer = 0 To a_sInfInfo.Length - 1
            '    If iCnt > 1 Then Exit For
            '    'LK_PrintDeviceFont(175 + riLeftPos, 45 + iTop, 0, 2, 1, 1, 0, a_sInfInfo(iCnt).ToString())
            '    LK_PrintWindowsFont(175 + riLeftPos, 45 + iTop, 1, 20, 1, 0, 0, "�ü�", a_sInfInfo(iCnt).ToString())
            'Next
            ''>

            ''<��ȣ ���� 20150602
            Dim InfInfoTmp As String = ro_Data.INFINFO

            If InfInfoTmp <> "" Then 'Null���� Replace ó�� �� ��� "��ü ������ ��ü �ν��Ͻ��� ���� ���� �ʾҴ�"�� ������ ���Ƿ� ���� ó��, ��ȣ 
                InfInfoTmp = Replace(InfInfoTmp, "/", ".")
                InfInfoTmp = Replace(InfInfoTmp, ",", ".")
            End If

            LK_PrintWindowsFont(220 + riLeftPos, 40 + iTop, 0, 25, 1, 0, 0, "�ü�", InfInfoTmp.ToString())

            '< ���ڵ�  
            If ro_Data.BCNOPRT <> "" Then
                ' Codabar                 
                LK_PrintBarCode(80 + riLeftPos, 65 + iTop, 0, "K", 2, 4, 90, 0, "A" + ro_Data.BCNOPRT + "A")

                '' code 128
                'LK_PrintBarCode(75 + riLeftPos, 61, 0, "1A", 2, 4, 90, 0, ro_Data.BCNOPRT)

                '< ���ڵ� ��ȣ
                Dim sBcnoPrt As String = ro_Data.BCNOPRT
                LK_PrintWindowsFont(160 + riLeftPos, 157 + iTop, 0, 15, 1, 0, 0, "����", sBcnoPrt)
            Else
                '< �̼��� ���ڵ� 
                'LK_PrintWindowsFont(70 + riLeftPos, 100 + iTop, 0, 35, 1, 0, 0, "����", "��ä�����ڵ�")
                LK_PrintWindowsFont(9 + riLeftPos, 20 + iTop, 0, 35, 1, 0, 0, "����", "��ä�����ڵ�")

                '<< JJH  ��ä�� ��Ϲ�ȣ ���ڵ�(��ġ�����ʿ�)
                LK_PrintBarCode(80 + riLeftPos, 65 + iTop, 0, "K", 2, 4, 90, 0, "A" + ro_Data.REGNO + "A")

            End If

            '< ��Ϲ�ȣ sPID
            LK_PrintDeviceFont(9 + riLeftPos, 165 + iTop, 0, 3, 1, 2, CType(IIf(ro_Data.BCCLSCD.Substring(0, 1).Trim() = "R", "1", "0"), Integer), ro_Data.REGNO)

            '< �����/����/����  
            Dim sDptWard As String = ro_Data.DEPTWARD
            LK_PrintWindowsFont(270 + riLeftPos, 195 + iTop, 0, 20, 1, 0, 0, "����", sDptWard)

            '< ����/���� 
            Dim sSexAge As String = ro_Data.SEXAGE
            LK_PrintWindowsFont(320 + riLeftPos, 170 + iTop, 0, 18, 0, 0, 0, "����", sSexAge)

            '< ȯ�ڸ� 
            Dim sPatNm As String = ro_Data.PATNM
            LK_PrintWindowsFont(150 + riLeftPos, 180 + iTop, 0, 30, 0, 0, 0, "����", sPatNm)

            '< sRemark
            LK_PrintWindowsFont(350 + riLeftPos, 80 + iTop, 0, 25, 3, 0, 0, "����", IIf(ro_Data.REMARK <> "", "CM", "").ToString)

            '< ���� ������ 
            LK_EndPage()

            Return ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return ""

        End Try

    End Function

    Protected Overridable Function fnMakePrtMsg_BLD(ByVal ro_Data As STU_BLDLABEL, ByVal riLeftPos As Integer, ByVal riTopPos As Integer) As String
        Dim sFn As String = "fnMakePrtMsg_BLD"

        Try

            Dim lgRtn As Integer = 0
            Dim iTop As Integer = riTopPos - 2

            riLeftPos += 30

            lgRtn = LK_SetupPrinter("90.0", "73.0", 0, "3.0", "0", 8, 6, 1)
            If lgRtn <> 0 Then Return ""

            '< ���� ����  
            LK_StartPage()

            '< 1. ��Ϲ�ȣ                       '20
            LK_PrintDeviceFont(165 + riLeftPos, 8 + iTop, 0, 2, 1, 2, 0, ro_Data.REGNO)

            '< 2. �����/����/����         '75
            LK_PrintDeviceFont(165 + riLeftPos, 55 + iTop, 0, 2, 1, 2, 0, ro_Data.DEPTWARD)

            '< 3. ȯ��������                      115
            'If ro_Data.PAT_ABORH = "" Then ro_Data.PAT_ABORH = ro_Data.BLD_ABORH
            LK_PrintDeviceFont(165 + riLeftPos, 110 + iTop, 0, 4, 2, 2, 0, ro_Data.PAT_ABORH)

            '< 4. ����������
            Dim sComNm As String = ro_Data.COMNM
            LK_PrintWindowsFont(165 + riLeftPos, 190 + iTop, 0, 25, 2, 0, 0, "����", sComNm)

            '< 6. ���պ�����
            Dim sXMatcd As String = ro_Data.XMATCH1 '<<< 20180205 2���� 1�� ���� 
            If ro_Data.XMATCH1 = "-" Then ro_Data.XMATCH1 = "����"
            If ro_Data.XMATCH1 = "+" Then ro_Data.XMATCH1 = "������"

            If ro_Data.XMATCH2 = "-" Then ro_Data.XMATCH2 = "����"
            If ro_Data.XMATCH2 = "+" Then ro_Data.XMATCH2 = "������"

            If ro_Data.XMATCH3 = "-" Then ro_Data.XMATCH3 = "����"
            If ro_Data.XMATCH3 = "+" Then ro_Data.XMATCH3 = "������"

            If ro_Data.XMATCH4 = "-" Then ro_Data.XMATCH4 = "����"
            If ro_Data.XMATCH4 = "+" Then ro_Data.XMATCH4 = "������"

            If ro_Data.XMATCH1 = "������" Then
                sXmatcd = "������"
            ElseIf ro_Data.XMATCH1 = "����" Then
                sXmatcd = "����"
                If ro_Data.XMATCH2 = "" Then

                ElseIf ro_Data.XMATCH2 = "������" Then
                    sXmatcd = "������"
                ElseIf ro_Data.XMATCH2 = "����" Then
                    sXmatcd = "����"
                    If ro_Data.XMATCH3 = "" Then

                    ElseIf ro_Data.XMATCH3 = "������" Then
                        sXmatcd = "������"
                    ElseIf ro_Data.XMATCH3 = "����" Then
                        sXmatcd = "����"
                        If ro_Data.XMATCH4 = "" Then

                        ElseIf ro_Data.XMATCH4 = "������" Then
                            sXmatcd = "������"
                        ElseIf ro_Data.XMATCH4 = "����" Then
                            sXmatcd = "����"
                        End If
                    End If
                End If
            End If

            LK_PrintWindowsFont(165 + riLeftPos, 245 + iTop, 0, 25, 1, 0, 0, "����", sXMatcd)

            '< 7. Filter/IR
            Dim sFiterIr As String = ro_Data.FITER + "/" + ro_Data.IR
            LK_PrintDeviceFont(165 + riLeftPos, 280 + iTop, 0, 2, 1, 2, 0, sFiterIr)

            '< 8. �˻���
            Dim sTestNm As String = ro_Data.TESTNM
            LK_PrintWindowsFont(165 + riLeftPos, 347 + iTop, 0, 25, 1, 0, 0, "����", sTestNm)

            '< 9. �����
            Dim sOutNm As String = ro_Data.OUTNM
            LK_PrintWindowsFont(165 + riLeftPos, 397 + iTop, 0, 25, 1, 0, 0, "����", sOutNm)

            '< 10.������ 
            Dim sRecNm As String = ro_Data.RECNM
            LK_PrintWindowsFont(165 + riLeftPos, 453 + iTop, 0, 25, 1, 0, 0, "����", sRecNm)


            '< 1. ȯ�ڸ�
            Dim sPatNm As String = ro_Data.PATNM
            LK_PrintWindowsFont(500 + riLeftPos, 22 + iTop, 0, 30, 1, 0, 0, "����", sPatNm)

            '< 2. ����/����
            LK_PrintDeviceFont(500 + riLeftPos, 60 + iTop, 0, 2, 1, 2, 0, ro_Data.SEXAGE)

            '< 3. ���������
            LK_PrintDeviceFont(500 + riLeftPos, 110 + iTop, 0, 4, 2, 2, 0, ro_Data.BLD_ABORH)

            '< 4. ���׹�ȣ
            If ro_Data.BLDNO.Count = 1 Then
                LK_PrintDeviceFont(500 + riLeftPos, 210 + iTop, 0, 2, 1, 2, 0, ro_Data.BLDNO(0))
            Else
                For ix As Integer = 0 To ro_Data.BLDNO.Count - 1
                    LK_PrintDeviceFont(500 + riLeftPos, 152 + iTop + 30 * ix, 0, 2, 1, 2, 0, ro_Data.BLDNO(ix))
                Next
            End If

            '< 7. ���׹�Ȯ��
            LK_PrintDeviceFont(500 + riLeftPos, 285 + iTop, 0, 2, 2, 2, 0, "OK")

            '< 8. �˻��Ͻ�
            LK_PrintDeviceFont(500 + riLeftPos, 340 + iTop, 0, 1, 1, 2, 0, ro_Data.TESTDT)

            '< 9. ����Ͻ�
            LK_PrintDeviceFont(500 + riLeftPos, 390 + iTop, 0, 1, 1, 2, 0, ro_Data.OUTDT)


            '< ���� ������ 
            LK_EndPage()

            Return ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return ""

        End Try
    End Function

    Protected Overridable Function fnMakePrtMsg_RIS(ByVal ro_Data As STU_BCPRTINFO, _
                                             ByVal rbFirst As Boolean, _
                                             ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                             ByVal rsBarType As String) As String
        Dim sFn As String = "fnMakePrtMsg"

        Try

            Dim sTestNms As String = ro_Data.TESTNMS
            Dim iTop As Integer = riTopPos - 5

            riLeftPos += 20

            If ro_Data.TESTNMS.Length > 20 Then
                sTestNms = sTestNms.Substring(0, 19) & "..."
            End If

            If sTestNms.IndexOf("...") > -1 Then
                If sTestNms.Substring(0, sTestNms.IndexOf("...")).Length > 20 Then
                    sTestNms = sTestNms.Substring(0, 19) & "..."
                End If
            End If

            Dim lgRtn As Integer = 0

            lgRtn = LK_SetupPrinter("53.0", "35.0", 0, "3.0", "0", 8, 6, 1)
            If lgRtn <> 0 Then Return ""

            '< ���� ����  
            LK_StartPage()

            Dim sSpcNm As String = ro_Data.SPCNM
            Dim sTubeNm As String = ro_Data.TUBENM
            Dim sTGrpNm As String = ro_Data.TGRPNM

            '< ���ڵ�  
            ' Codabar 
            LK_PrintBarCode(80 + riLeftPos, 30 + iTop, 0, "K", 2, 4, 75, 0, "A" + ro_Data.BCNOPRT + "A")

            '' code 128
            'LK_PrintBarCode(75 + riLeftPos, 61, 0, "1A", 2, 4, 90, 0, ro_Data.BCNOPRT)

            '< ���ڵ� ��ȣ
            Dim sBcnoPrt As String = ro_Data.BCNOPRT
            LK_PrintWindowsFont(160 + riLeftPos, 107 + iTop, 0, 15, 1, 0, 0, "����", sBcnoPrt)

            '< ��Ϲ�ȣ sPID
            LK_PrintDeviceFont(12 + riLeftPos, 130 + iTop, 0, 3, 1, 2, CType(IIf(ro_Data.BCCLSCD.Substring(0, 1).Trim() = "R", "1", "0"), Integer), ro_Data.REGNO)

            '< �����/����/����  
            Dim sDptWard As String = ro_Data.DEPTWARD
            LK_PrintWindowsFont(270 + riLeftPos, 170 + iTop, 0, 20, 1, 0, 0, "����", sDptWard)

            '< ����/���� 
            Dim sSexAge As String = ro_Data.SEXAGE
            LK_PrintWindowsFont(320 + riLeftPos, 140 + iTop, 0, 18, 0, 0, 0, "����", sSexAge)

            '< ȯ�ڸ� 
            Dim sPatNm As String = ro_Data.PATNM
            LK_PrintWindowsFont(150 + riLeftPos, 135 + iTop, 0, 35, 1, 0, 0, "����", sPatNm)

            '< ��ü��
            LK_PrintWindowsFont(12 + riLeftPos, 180 + iTop, 0, 25, 1, 0, 0, "����", sSpcNm)

            '< ���� 
            LK_PrintWindowsFont(12 + riLeftPos, 215 + iTop, 0, 25, 1, 0, 0, "����", sTubeNm.Substring(0, 8) + "-")

            '< ���� 
            LK_PrintDeviceFont(145 + riLeftPos, 205 + iTop, 0, 2, 2, 2, 0, sTubeNm.Substring(8))


            '< �˻��׸�� 
            LK_PrintDeviceFont(12 + riLeftPos, 250 + iTop, 0, 2, 1, 1, 0, sTestNms)

            '< ���� ������ 
            LK_EndPage()

            Return ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return ""

        End Try

    End Function

    Protected Overridable Function fnMakePrtMsg_BLD_(ByVal ro_Data As STU_BLDLABEL, ByVal riLeftPos As Integer, ByVal riTopPos As Integer) As String
        Dim sFn As String = "fnMakePrtMsg"

        Try
            Dim sPrtBuf As String = ""
            Dim sCrLf As String = Chr(13) + Chr(10)

            Dim iMaxLen As Integer = 33

            '< �⺻���� 
            sPrtBuf = ""
            'sPrtBuf = sPrtBuf + "I8,1,001" + vbCrLf
            'sPrtBuf = sPrtBuf + "D" + vbCrLf        '-- ���� = OD, ���� = D
            'sPrtBuf = sPrtBuf + "Q464,24" + vbCrLf  '-- Label Length, Gap Length
            'sPrtBuf = sPrtBuf + "q696" + vbCrLf     '-- Label(Width)
            'sPrtBuf = sPrtBuf + "S4" + vbCrLf       '-- speed
            'sPrtBuf = sPrtBuf + "D8" + vbCrLf       '-- ��
            'sPrtBuf = sPrtBuf + "ZT" + vbCrLf
            'sPrtBuf = sPrtBuf + "JF" + vbCrLf       '-- FB
            sPrtBuf = sPrtBuf & "N" + vbCrLf

            '< ��Ϲ�ȣ
            sPrtBuf = sPrtBuf + "A170,20,0,3,1,2,N," + Chr(34) + ro_Data.REGNO + Chr(34) + vbCrLf

            '< ȯ�ڸ�  
            sPrtBuf = sPrtBuf + "A500,25,0,8,2,1,N," + Chr(34) + ro_Data.PATNM + Chr(34) + vbCrLf

            '< �����/����/����  
            sPrtBuf = sPrtBuf + "A180,68,0,3,1,1,N," + Chr(34) + ro_Data.DEPTWARD + Chr(34) + vbCrLf

            '< ����/���� 
            sPrtBuf = sPrtBuf + "A530,68,0,3,1,1,N," + Chr(34) + ro_Data.SEXAGE + Chr(34) + vbCrLf

            '< ȯ�� ������ 
            sPrtBuf = sPrtBuf + "A220,95,0,3,1,2,N," + Chr(34) + ro_Data.PAT_ABORH + Chr(34) + vbCrLf

            '< ��� ������ 
            sPrtBuf = sPrtBuf + "A550,95,0,3,1,2,N," + Chr(34) + ro_Data.BLD_ABORH + Chr(34) + vbCrLf

            ''< �������� 
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


            '< ���պ�����
            If ro_Data.XMATCH1 = "-" Then ro_Data.XMATCH1 = "����"
            If ro_Data.XMATCH1 = "+" Then ro_Data.XMATCH1 = "������"

            If ro_Data.XMATCH2 = "-" Then ro_Data.XMATCH2 = "����"
            If ro_Data.XMATCH2 = "+" Then ro_Data.XMATCH2 = "������"

            'sPrtBuf = sPrtBuf + "A190,185,0,8,1,1,N," + Chr(34) + sRst1 + Chr(34) + vbCrLf
            sPrtBuf = sPrtBuf + "A190,215,0,8,1,1,N," + Chr(34) + ro_Data.XMATCH2 + Chr(34) + vbCrLf

            '-- ���׹�ȣ
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

            '< Ȯ��
            sPrtBuf = sPrtBuf + "A450,263,0,3,1,1,N," + Chr(34) + "OK" + Chr(34) + vbCrLf
            '>


            '< �˻���
            sPrtBuf = sPrtBuf + "A180,303,0,8,1,1,N," + Chr(34) + ro_Data.BEFOUTNM + Chr(34) + vbCrLf

            '< �˻�����
            sPrtBuf = sPrtBuf + "A500,303,0,2,1,1,N," + Chr(34) + ro_Data.BEFOUTDT + Chr(34) + vbCrLf

            '< �����
            sPrtBuf = sPrtBuf + "A180,343,0,8,1,1,N," + Chr(34) + ro_Data.OUTNM + Chr(34) + vbCrLf

            '< �������
            sPrtBuf = sPrtBuf + "A500,343,0,2,1,1,N," + Chr(34) + ro_Data.OUTDT + Chr(34) + vbCrLf

            '< ������
            sPrtBuf = sPrtBuf + "A180,383,0,8,1,1,N," + Chr(34) + ro_Data.RECNM + Chr(34) + vbCrLf


            '< ���� ������ 
            sPrtBuf = sPrtBuf & "P1" + vbCrLf
            '>  

            Return sPrtBuf

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return ""

        End Try
    End Function

    Protected Overridable Function fnMakePrtMsg_PIS(ByVal ro_Data As STU_BCPRTINFO, _
                                                    ByVal rbFirst As Boolean, _
                                                    ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                                    ByVal rsBarType As String) As String
        Dim sFn As String = "fnMakePrtMsgfnMakePrtMsg_PIS"

        Try

            Dim sTestNms As String = ro_Data.TESTNMS
            Dim iTop As Integer = riTopPos - 30

            If ro_Data.TESTNMS.Length > 20 Then
                sTestNms = sTestNms.Substring(0, 19) & "..."
            End If

            If sTestNms.IndexOf("...") > -1 Then
                If sTestNms.Substring(0, sTestNms.IndexOf("...")).Length > 20 Then
                    sTestNms = sTestNms.Substring(0, 19) & "..."
                End If
            End If

            Dim lgRtn As Integer = 0

            lgRtn = LK_SetupPrinter("55.0", "30.0", 0, "3.1", "0", 8, 6, 1)
            If lgRtn <> 0 Then Return ""

            '< ���� ����  
            LK_StartPage()

            '< sRemark
            LK_PrintWindowsFont(15 + riLeftPos, 200 + iTop, 0, 25, 3, 0, 0, "����", ro_Data.REMARK)

            '< ��ü��
            Dim sSpcNm As String = ro_Data.SPCNM
            LK_PrintWindowsFont(110 + riLeftPos, 200 + iTop, 0, 20, 1, 0, 0, "����", sSpcNm)

            '< �˻��׸�� 
            LK_PrintWindowsFont(45 + riLeftPos, 250 + iTop, 0, 20, 1, 0, 0, "����", sTestNms)

            ''< ��ó���
            'LK_PrintDeviceFont(65 + riLeftPos, 230 + iTop, 0, 8, 1, 1, 0, ro_Data.TGRPNM)

            '< �� sKind
            LK_PrintDeviceFont(15 + riLeftPos, 230 + iTop, 0, 3, 2, 2, 0, "P")

            '< ��ü��ȣ 
            LK_PrintDeviceFont(6 + riLeftPos, 45 + iTop, 0, 2, 1, 1, 0, ro_Data.BCNO)

            '< ���ڵ� ���� �Ͻ�  233
            LK_PrintDeviceFont(260 + riLeftPos, 45 + iTop, 0, 1, 1, 1, CType(IIf(rbFirst, "0", "1"), Integer), Fn.GetServerDateTime.ToString("yyyy-MM-dd HH:mm"))

            '< ��������  
            Dim a_sInfInfo As String() = ro_Data.INFINFO.Split("/"c)
            For iCnt As Integer = 0 To a_sInfInfo.Length - 1
                If iCnt > 1 Then Exit For
                LK_PrintDeviceFont(6 + riLeftPos, 80 + (iCnt * 30) + iTop, 0, 3, 1, 1, 0, a_sInfInfo(iCnt).ToString())
            Next

            '< ���ڵ�  
            If ro_Data.BCNOPRT <> "" Then
                ' Codabar 
                LK_PrintBarCode(100 + riLeftPos, 65 + iTop, 0, "K", 2, 4, 80, 0, "A" + ro_Data.BCNOPRT + "A")

                '' code 128
                'LK_PrintBarCode(75 + riLeftPos, 61, 0, "1A", 2, 4, 90, 0, ro_Data.BCNOPRT)

                '< ���ڵ� ��ȣ
                Dim sBcnoPrt As String = ro_Data.BCNOPRT
                LK_PrintWindowsFont(170 + riLeftPos, 147 + iTop, 0, 15, 1, 0, 0, "����", sBcnoPrt)
            Else
                '< �̼��� ���ڵ� 
                LK_PrintWindowsFont(90 + riLeftPos, 100 + iTop, 0, 35, 1, 0, 0, "����", "��ä�����ڵ�")
                '>  
            End If

            '< ��Ϲ�ȣ sPID
            LK_PrintDeviceFont(9 + riLeftPos, 145 + iTop, 0, 3, 1, 2, CType(IIf(ro_Data.BCCLSCD.Substring(0, 1).Trim() = "R", "1", "0"), Integer), ro_Data.REGNO)

            '< �����/����/����  
            Dim sDptWard As String = ro_Data.DEPTWARD
            LK_PrintWindowsFont(310 + riLeftPos, 165 + iTop, 0, 20, 1, 0, 0, "����", sDptWard)

            '< ����/���� 
            Dim sSexAge As String = ro_Data.SEXAGE
            LK_PrintWindowsFont(380 + riLeftPos, 175 + iTop, 0, 15, 0, 0, 0, "����", sSexAge)

            '< ȯ�ڸ�
            Dim sPatNm As String = ro_Data.PATNM
            LK_PrintWindowsFont(160 + riLeftPos, 165 + iTop, 0, 25, 0, 0, 0, "����", sPatNm)

            '< ���� ������ 
            LK_EndPage()

            Return ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return ""

        End Try

    End Function

End Class

