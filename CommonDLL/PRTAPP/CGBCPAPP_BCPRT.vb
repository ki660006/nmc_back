Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Namespace APP_BC
#Region " 바코드 출력 : Class BCPrinter "
    Public Class BCPrinter
        Private Const msFile As String = "File : CGBCPAPP_C.vb, Class : PRTAPP.APP_BC.BCPrinter" & vbTab
        Private mlPRTInfo As New ArrayList

        Private msXmlDir As String = System.Windows.Forms.Application.StartupPath + "\XML"
        Private msXmlFile As String = ""

        Private miSelPRTID As Integer = 0  ' 선택된프린터

        Private msMsg As String
        Private miCnt As Integer

        Private msFldSep As String = CStr(Chr(32))
        Private miMaxLenCmt As Integer = 34
        Private msSymbolMore As String = "..."

        Public Sub New(ByVal rsLoadFrm As String)
            MyBase.New()

            msXmlFile = msXmlDir + "\" + rsLoadFrm & "_BCPrinterINFO.XML"

            ' 생성시 바코드프린터정보 읽기
            sbReadPrtInfo()
        End Sub

        ' 바코드 프린터정보 읽기( Client 기준 ) 
        Private Sub sbReadPrtInfo()
            Dim sFn As String = ""

            Try
                If Dir(msXmlDir, FileAttribute.Directory) = "" Then MkDir(msXmlDir)

                If Dir(msXmlFile) > "" Then
                    Dim XmlRead As Xml.XmlTextReader

                    XmlRead = New Xml.XmlTextReader(msXmlFile)
                    While XmlRead.Read

                        XmlRead.ReadStartElement("ROOT")
                        Do While (True)
                            XmlRead.ReadStartElement("PRTINFO")
                            Dim PRTInfo As New clsPRTInfo
                            With PRTInfo
                                .PRTID = XmlRead.ReadElementString("PRTID")
                                .PRTNM = XmlRead.ReadElementString("PRTNM")
                                .OUTIP = XmlRead.ReadElementString("OUTIP")
                                .OUTPORT = XmlRead.ReadElementString("OUTPORT")
                                .SUPPORTIP = XmlRead.ReadElementString("SUPPORTIP")
                                .SELECTED = XmlRead.ReadElementString("SELECTED")
                                .IOPORT = XmlRead.ReadElementString("IOPORT")
                                .LEFTMARGIN = XmlRead.ReadElementString("LEFTMARGIN")
                                .TOPMARGIN = XmlRead.ReadElementString("TOPMARGIN")
                                .PRTTYPE = XmlRead.ReadElementString("PRTTYPE")
                                ' 선택된 프린터 설정
                                If .SELECTED = "1" Then miSelPRTID = CInt(.PRTID)
                            End With
                            mlPRTInfo.Add(PRTInfo)
                            XmlRead.ReadEndElement()
                            XmlRead.Read()

                            If XmlRead.Name <> "PRTINFO" Then Exit Do
                        Loop
                        XmlRead.Close()
                    End While

                Else
                    Dim moBCPRT As New BCPRT01.BCPRT
                    For intCnt As Integer = 0 To moBCPRT.BCPRINTERS.Count - 1
                        Dim PRTInfo As New clsPRTInfo
                        With PRTInfo
                            .PRTID = CType(moBCPRT.BCPRINTERS(intCnt), BCPRT01.BCPRINTER_CFG).PrinterID.ToString
                            .PRTNM = CType(moBCPRT.BCPRINTERS(intCnt), BCPRT01.BCPRINTER_CFG).PrinterName.ToString
                            .SUPPORTIP = IIf(CType(moBCPRT.BCPRINTERS(intCnt), BCPRT01.BCPRINTER_CFG).SupportTCPIP = True, "1", "").ToString
                            .OUTIP = ""
                            .OUTPORT = ""
                            If .SUPPORTIP = "1" Then
                                .OUTIP = "127.0.0.1"
                                .OUTPORT = CType(moBCPRT.BCPRINTERS(intCnt), BCPRT01.BCPRINTER_CFG).PortNo.ToString
                            End If
                            .SELECTED = ""
                            .SELECTED = ""
                            .IOPORT = ""
                            .LEFTMARGIN = ""
                            .TOPMARGIN = ""
                        End With
                        mlPRTInfo.Add(PRTInfo)
                    Next

                    For intCnt As Integer = 0 To 2

                    Next

                    WritePrtInfo()
                End If

                mlPRTInfo.TrimToSize()

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)

            End Try

        End Sub

        ' 수정된 바코드 프린터정보 쓰기
        Public Sub WritePrtInfo()
            Dim sFn As String = ""

            Try
                If mlPRTInfo.Count > 0 Then
                    If Dir(msXmlDir, FileAttribute.Directory) = "" Then MkDir(msXmlDir)

                    Dim XmlWrite As Xml.XmlTextWriter = Nothing
                    XmlWrite = New Xml.XmlTextWriter(msXmlFile, System.Text.Encoding.GetEncoding("utf-8"))
                    With XmlWrite
                        .Formatting = Xml.Formatting.Indented
                        .Indentation = 4
                        .IndentChar = " "c
                        .WriteStartDocument(False)

                        .WriteStartElement("ROOT")
                        For intRow As Integer = 0 To mlPRTInfo.Count - 1
                            .WriteStartElement("PRTINFO")
                            .WriteElementString("PRTID", CType(mlPRTInfo(intRow), clsPRTInfo).PRTID)
                            .WriteElementString("PRTNM", CType(mlPRTInfo(intRow), clsPRTInfo).PRTNM)
                            .WriteElementString("OUTIP", CType(mlPRTInfo(intRow), clsPRTInfo).OUTIP)
                            .WriteElementString("OUTPORT", CType(mlPRTInfo(intRow), clsPRTInfo).OUTPORT)
                            .WriteElementString("SUPPORTIP", CType(mlPRTInfo(intRow), clsPRTInfo).SUPPORTIP)
                            .WriteElementString("SELECTED", CType(mlPRTInfo(intRow), clsPRTInfo).SELECTED)
                            .WriteElementString("IOPORT", CType(mlPRTInfo(intRow), clsPRTInfo).IOPORT)
                            .WriteElementString("LEFTMARGIN", CType(mlPRTInfo(intRow), clsPRTInfo).LEFTMARGIN)
                            .WriteElementString("TOPMARGIN", CType(mlPRTInfo(intRow), clsPRTInfo).TOPMARGIN)
                            .WriteElementString("PRTTYPE", CType(mlPRTInfo(intRow), clsPRTInfo).PRTTYPE)
                            .WriteEndElement()
                        Next
                        .WriteEndElement()
                        .Close()
                    End With

                Else
                    If Dir(msXmlFile) <> "" Then Kill(msXmlFile)
                End If

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)

            End Try

        End Sub

        ' PRTID가 없으면 선택된 프린터
        Public ReadOnly Property GetInfo(Optional ByVal aiPRTID As Integer = -1) As clsPRTInfo
            Get
                If aiPRTID < 0 Then
                    GetInfo = CType(mlPRTInfo(miSelPRTID), clsPRTInfo)
                Else
                    GetInfo = CType(mlPRTInfo(aiPRTID), clsPRTInfo)
                End If
            End Get
        End Property

        ' 선택가능 프린터 수
        Public ReadOnly Property GetCnt() As Integer
            Get
                GetCnt = mlPRTInfo.Count
            End Get
        End Property

        ' 출력프린터 설정
        Public Property PrtID() As Integer
            Get
                PrtID = miSelPRTID
            End Get
            Set(ByVal Value As Integer)
                miSelPRTID = Value

                For intCnt As Integer = 0 To mlPRTInfo.Count - 1
                    CType(mlPRTInfo(intCnt), clsPRTInfo).SELECTED = ""
                    If miSelPRTID = intCnt Then
                        CType(mlPRTInfo(intCnt), clsPRTInfo).SELECTED = "1"
                    End If
                Next
            End Set
        End Property


        ' 선택한 프린터 IP설정
        Public WriteOnly Property SetOutIP(Optional ByVal aiPRTID As Integer = -1) As String
            Set(ByVal Value As String)
                If aiPRTID = -1 Then
                    CType(mlPRTInfo(miSelPRTID), clsPRTInfo).OUTIP = Value
                Else
                    CType(mlPRTInfo(aiPRTID), clsPRTInfo).OUTIP = Value
                End If
            End Set
        End Property

        '-- 2007-10-16 YOOEJ ADD
        ' 선택한 프린터 포트설정
        Public WriteOnly Property SetIOPort(Optional ByVal aiPRTID As Integer = -1) As String
            Set(ByVal Value As String)
                If aiPRTID = -1 Then
                    CType(mlPRTInfo(miSelPRTID), clsPRTInfo).IOPORT = Value
                Else
                    CType(mlPRTInfo(aiPRTID), clsPRTInfo).IOPORT = Value
                End If
            End Set
        End Property

        '-- 2007-10-16 YOOEJ ADD
        ' 선택한 인쇄 마진 설정
        Public WriteOnly Property SetLeftMargin(Optional ByVal aiPRTID As Integer = -1) As String
            Set(ByVal Value As String)
                If aiPRTID = -1 Then
                    CType(mlPRTInfo(miSelPRTID), clsPRTInfo).LEFTMARGIN = Value
                Else
                    CType(mlPRTInfo(aiPRTID), clsPRTInfo).LEFTMARGIN = Value
                End If
            End Set
        End Property

        '-- 2007-10-19 YOOEJ ADD
        ' 선택한 인쇄 마진 설정
        Public WriteOnly Property SetTopMargin(Optional ByVal aiPRTID As Integer = -1) As String
            Set(ByVal Value As String)
                If aiPRTID = -1 Then
                    CType(mlPRTInfo(miSelPRTID), clsPRTInfo).TOPMARGIN = Value
                Else
                    CType(mlPRTInfo(aiPRTID), clsPRTInfo).TOPMARGIN = Value
                End If
            End Set
        End Property


        '-- 2008-12-23 yjlee
        ' 프린트타입 설정
        Public WriteOnly Property SetPrtType(Optional ByVal aiPRTID As Integer = -1) As String
            Set(ByVal Value As String)
                If aiPRTID = -1 Then
                    CType(mlPRTInfo(miSelPRTID), clsPRTInfo).PRTTYPE = Value
                Else
                    CType(mlPRTInfo(aiPRTID), clsPRTInfo).PRTTYPE = Value
                End If
            End Set
        End Property

        Private maPrtData As New ArrayList
        Private mbFirst As Boolean
        Private mTrd As System.Threading.Thread

        Public Sub PrintDo(ByVal ra_PrtData As ArrayList, ByVal rbFirst As Boolean, ByVal rsPrinterName As String)
            Dim sFn As String = "Public Sub PrintDo(String,  Boolean, String)"

            Try
                maPrtData = ra_PrtData
                mbFirst = rbFirst

                sbPrint(rsPrinterName)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)

            End Try

        End Sub

        Public Sub PrintDo_pis(ByVal ra_PrtData As ArrayList, ByVal rbFirst As Boolean, ByVal rsPrinterName As String)
            Dim sFn As String = "Public Sub PrintDo_pis(String, Boolean, String)"

            Try
                maPrtData = ra_PrtData
                mbFirst = rbFirst

                sbPrint_pis(rsPrinterName)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)

            End Try

        End Sub


        ' 바코드 출력하기
        Public Sub PrintDo(ByVal ra_PrtData As ArrayList, ByVal rbFirst As Boolean)
            Dim sFn As String = "Public Sub PrintDo(ArrayList,  Boolean)"

            Try
                maPrtData = ra_PrtData
                mbFirst = rbFirst

                sbPrint()

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)

            End Try

        End Sub

        Public Sub PrintDo_ris(ByVal ra_PrtData As ArrayList, ByVal rbFirst As Boolean)
            Dim sFn As String = "Public Sub PrintDo(ArrayList,  Boolean)"

            Try
                maPrtData = ra_PrtData
                mbFirst = rbFirst

                sbPrint_ris()

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)

            End Try

        End Sub

        '< add yjlee 2009-04-24
        ' 바코드 출력하기
        Public Sub PrintDo_Micro(ByVal ra_BcData As ArrayList)
            Dim sFn As String = "Public Sub PrintDo_Micro(ArrayList)"

            Try
                maPrtData = ra_BcData

                sbPrint_Micro()

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)

            End Try

        End Sub
        '> add yjlee 2009-04-24 

        Public Sub PrintDo_Blood(ByVal ra_BcData As ArrayList, ByVal riCopy As Integer)

            Dim sFn As String = "Public Sub PrintDo_Micro(ArrayList)"

            Try
                maPrtData = ra_BcData

                sbPrint_blood(riCopy)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)

            End Try
        End Sub


        Public Sub PrintDoBarcode(ByVal r_al_BcNos As ArrayList, ByVal riCount As Integer, _
                                     Optional ByVal roForm As String = "", Optional ByVal rbFirst As Boolean = False)
            Dim sFN As String = ""

            Try
                Dim sBcNos As String = ""
                Dim arlBcData As New ArrayList

                For ix As Integer = 0 To r_al_BcNos.Count - 1
                    Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_BcNos(ix), List(Of STU_CollectInfo))
                    Dim bpi As STU_BCPRTINFO = fnFind_BcPrtItem(listcollData)

                    arlBcData.Add(bpi)

                    If sBcNos.Length > 0 Then sBcNos += ", "
                    sBcNos += bpi.BCNO.Replace("-", "").Trim()
                Next

                Dim bReturn As Boolean = False

                Call (New BCPrinter(roForm)).PrintDo(arlBcData, rbFirst)

            Catch ex As Exception

            End Try
        End Sub

        Public Sub PrintDoBarcode(ByVal r_al_BcNos As ArrayList, ByVal riCount As Integer, _
                                     ByVal roForm As String, ByVal rbFirst As Boolean, ByVal rsPrinterName As String)
            Dim sFN As String = ""

            Try
                Dim sBcNos As String = ""
                Dim alBcData As New ArrayList

                For i As Integer = 1 To r_al_BcNos.Count
                    Dim sPrtMsgOne As String = ""
                    Dim sPrtCntOne As String = ""

                    Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_BcNos(i - 1), List(Of STU_CollectInfo))

                    Dim bpi As STU_BCPRTINFO = fnFind_BcPrtItem(listcollData)

                    alBcData.Add(bpi)

                    If sBcNos.Length > 0 Then sBcNos += ","
                    sBcNos += bpi.BCNO.Replace("-", "").Trim()

                Next

                Dim bReturn As Boolean = False

                Call (New BCPrinter(roForm)).PrintDo(alBcData, rbFirst)

            Catch ex As Exception

            End Try
        End Sub

        Private Function fnFind_BcPrtItem(ByVal r_listcollData As List(Of STU_CollectInfo)) As STU_BCPRTINFO
            Dim sFn As String = "Private Function fnFind_BcPrtItem(List(Of STU_CollectInfo)) As String"

            Try
                Dim bpi As New STU_BCPRTINFO

                With bpi
                    '<---- 2019-04-19 환자 혈액형 여부 표시 (있을때 공란, 없을때 *표시)
                    Dim ABOCHK As String = OCSAPP.OcsLink.SData.fnget_ABO(r_listcollData.Item(0).REGNO)
                    .ABOCHK = ABOCHK
                    '----->

                    .BCNOPRT = r_listcollData.Item(0).PRTBCNO
                    .REGNO = r_listcollData.Item(0).REGNO
                    '<2019-12-03 E4 인간유전자 검사 바코드에 환자명 중간정보 "O"으로 표기
                    If r_listcollData.Item(0).BCCLSCD = "E4" Then
                        .PATNM = r_listcollData.Item(0).PATNM
                    Else
                        .PATNM = r_listcollData.Item(0).PATNM
                    End If
                    .SEXAGE = r_listcollData.Item(0).SEX + "/" + r_listcollData.Item(0).AGE
                    .BCCLSCD = r_listcollData.Item(0).BCCLSCD
                    If r_listcollData.Item(0).IOGBN <> "I" Then
                        .DEPTWARD = r_listcollData.Item(0).DEPTCD
                    Else
                        .DEPTWARD = r_listcollData.Item(0).WARDNO + "/" + r_listcollData.Item(0).ROOMNO
                    End If
                    .IOGBN = r_listcollData.Item(0).IOGBN
                    .BCNO = Fn.BCNO_View(r_listcollData.Item(0).BCNO, True)
                    .HREGNO = r_listcollData.Item(0).HREGNO
                    .TUBENM = r_listcollData.Item(0).TUBENMBP

                    Dim sTNmBP As String = ""
                    Dim sTmpTgrpnm As String = ""

                    If .BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Then
                        sTNmBP = r_listcollData.Item(0).TNMBP + msFldSep + r_listcollData.Count.ToString + "unit(s)"
                    Else
                        For r As Integer = 1 To r_listcollData.Count
                            Dim collData As STU_CollectInfo = CType(r_listcollData(r - 1), STU_CollectInfo)

                            Dim sTNmOne As String = collData.TNMBP.Trim

                            If sTNmOne.IndexOf(">") >= 0 Then
                                sTNmOne = sTNmOne.Substring(0, sTNmOne.IndexOf(">")).Trim
                            End If

                            If sTNmOne.IndexOf("<") >= 0 Then
                                sTNmOne = sTNmOne.Substring(sTNmOne.IndexOf("<") + 1).Trim
                            End If

                            If sTNmBP.Length > 0 Then sTNmBP += msFldSep

                            If Fn.LengthH(sTNmBP + sTNmOne) > miMaxLenCmt - msSymbolMore.Length Then
                                If r = r_listcollData.Count Then
                                    If Fn.LengthH(sTNmBP + sTNmOne) > miMaxLenCmt Then
                                        sTNmBP = sTNmBP.Trim + msSymbolMore
                                    Else
                                        sTNmBP += sTNmOne
                                    End If
                                Else
                                    sTNmBP = sTNmBP.Trim + msSymbolMore
                                End If

                                'Exit For
                            Else
                                sTNmBP += sTNmOne
                            End If

                            If collData.TGRPNM <> "" Then
                                If sTmpTgrpnm.IndexOf(collData.TGRPNM) < 0 Then
                                    sTmpTgrpnm += collData.TGRPNM + " "
                                End If
                            End If
                        Next
                    End If

                    sTNmBP = sTNmBP.Replace("&amp;", "&") '바코드 출력시 &표기 수정 20131127

                    .TESTNMS = Fn.PadRightH(sTNmBP, 50)

                    Dim sStat As String = ""

                    For r As Integer = 1 To r_listcollData.Count
                        sStat = r_listcollData.Item(r - 1).STATGBN

                        If sStat <> "" Then Exit For
                    Next

                    '기타1 -> 응급(1) + 병실(9)
                    .EMER = sStat

                    '기타2 -> 검체명(10)
                    .SPCNM = r_listcollData.Item(0).SPCNMBP

                    '기타3 -> 감염정보(10)
                    .INFINFO = r_listcollData.Item(0).INFINFO

                    '기타4 -> 검사그룹(12)

                    .TGRPNM = sTmpTgrpnm

                    .ERPRTYN = r_listcollData.Item(0).ERPRTYN  '<<<<20180802 응급프린트

                    '20210611 jhs 추가 바코드 설정 리스트 맨 처음으로 변경
                    'For ix As Integer = 0 To r_listcollData.Count - 1 '맨 마지막 바코드 출력 갯수로 초기화 됨 수정 필요 
                    '    If r_listcollData.Item(ix).BCCNT <> "" Then
                    '        .BCCNT = Fn.PadRightH(r_listcollData.Item(ix).BCCNT, 1) 'Fn.PadRightH(fnFind_BcCrossMatchingCheck(r_listcollData.Item(0).BCNO.Trim().Replace("-", "")), 4)
                    '    End If
                    'Next
                    .BCCNT = Fn.PadRightH(r_listcollData.Item(0).BCCNT, 1)
                    '-------------------------------------------------

                    Dim sRemark As String = OCSAPP.OcsLink.SData.fnGet_Remark(r_listcollData.Item(0).BCNO)

                    If sRemark = "" Then
                        .REMARK = OCSAPP.OcsLink.SData.fnGet_LisCmt(r_listcollData.Item(0).FKOCS)
                    Else
                        .REMARK = sRemark
                    End If
                    '20210429 jhs 음영 표시 위해 검사 코드추가
                    .TESTCD = r_listcollData.Item(0).TCLSCD
                    '------------------------------------
                End With

                Return bpi

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
                Return Nothing

            End Try
        End Function

        '< add freety 2005/08/16 
        Public Sub PrintDo(ByVal ra_Bcno As ArrayList, ByVal rsBarCnt As String)
            Dim sFn As String = "PrintDo"
            Try
                Dim alBcData As New ArrayList

                For ix As Integer = 0 To ra_Bcno.Count - 1
                    Dim sSql As String = ""
                    Dim alParm As New ArrayList

                    If PRG_CONST.PART_RIA.StartsWith(ra_Bcno(ix).ToString.Substring(8, 1)) Then
                        sSql = ""
                        sSql += "SELECT DISTINCT"
                        sSql += "       j.regno,   j.patnm, j.sex || '/' || j.age sexage, j.bcclscd,"
                        sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                        sSql += "       j.iogbn, f3.spcnmbp, f4.tubenmbp || ' ' || f6.minspcvol tubenmbp, j.statgbn,"
                        sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                        sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                        sSql += "       (SELECT doctorrmk"
                        sSql += "          FROM rj011m"
                        sSql += "         WHERE bcno    = :bcno"
                        sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                        sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                        sSql += "           AND ROWNUM= 1"
                        sSql += "       ) doctorrmk,"
                        sSql += "       REPLACE(fn_ack_get_infection_prt(j.bcno), ',', '/') infinfo,"
                        'sSql += "       fn_ack_get_test_nmbp_list(j.bcno) testnms,"
                        sSql += "       (SELECT listagg(b.tnmbp,',') within group (order by b.dispseql)"
                        sSql += "          FROM rj011m a, rf060m b"
                        sSql += "         WHERE a.bcno   = j.bcno"
                        sSql += "           AND a.tclscd = b.testcd  AND a.spccd  = b.spccd"
                        sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt   > j.bcprtdt"
                        sSql += "       ) testnms,"
                        sSql += "       fn_ack_get_tgrp_nmbp_list(j.bcno) tgrpnm,"
                        sSql += "       CASE WHEN f6.bccnt = 'B' THEN f6.bccnt ELSE '1' END bccnt"
                        '20201201 jhs 자체처방 자체응급로직이 추가 되지 않아 일단 공백으로 입력
                        sSql += "       , '' ERPRTYN"
                        sSql += "       , f6.testcd"
                        '-------------------------------------------------------------------
                        sSql += "  FROM rj010m j,  rj011m j1, rf060m f6,"
                        sSql += "       lf030m f3, lf040m f4"
                        sSql += " WHERE j.bcno     = :bcno"
                        sSql += "   AND j.bcno     = j1.bcno"
                        sSql += "   AND j1.spccd   = f3.spccd"
                        sSql += "   AND j1.colldt >= f3.usdt"
                        sSql += "   AND j1.colldt <  f3.uedt"
                        sSql += "   AND j1.tclscd  = f6.testcd"
                        sSql += "   AND j1.spccd   = f6.spccd"
                        sSql += "   AND j1.colldt >= f6.usdt"
                        sSql += "   AND j1.colldt <  f6.uedt"
                        sSql += "   AND f6.tubecd  = f4.tubecd"
                        sSql += "   AND j1.colldt >= f4.usdt"
                        sSql += "   AND j1.colldt <  f4.uedt"
                    Else
                        sSql = ""
                        sSql += "SELECT DISTINCT"
                        sSql += "       j.regno,   j.patnm, j.sex || '/' || j.age sexage, j.bcclscd,"
                        sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                        sSql += "       j.iogbn, f3.spcnmbp, f4.tubenmbp || ' ' || f6.minspcvol tubenmbp, j.statgbn,"
                        sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                        sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                        sSql += "       (SELECT doctorrmk"
                        sSql += "          FROM lj011m"
                        sSql += "         WHERE bcno    = j.bcno"
                        sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                        sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                        sSql += "           AND ROWNUM= 1"
                        sSql += "       ) doctorrmk,"
                        sSql += "       REPLACE(fn_ack_get_infection_prt(j.regno), ',', '/') infinfo,"
                        'sSql += "       fn_ack_get_test_nmbp_list(j.bcno) testnms,"
                        sSql += "       (SELECT listagg(b.tnmbp,',') within group (order by b.dispseql)"
                        sSql += "          FROM lj011m a, lf060m b"
                        sSql += "         WHERE a.bcno   = j.bcno"
                        sSql += "           AND a.tclscd = b.testcd  AND a.spccd  = b.spccd"
                        sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt   > j.bcprtdt"
                        sSql += "       ) testnms,"
                        sSql += "       fn_ack_get_tgrp_nmbp_list(j.bcno) tgrpnm,"
                        sSql += "       CASE WHEN f6.bccnt = 'B' THEN f6.bccnt ELSE '1' END bccnt"
                        'sSql += "       ,fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno "  '<< JJH 세포면역[H4] 작업번호 '
                        sSql += "       ,(SELECT CASE WHEN COUNT(*) > 0 THEN 'R' ELSE '' END YN "
                        sSql += "           FROM LJ015M "
                        sSql += "          WHERE BCNO = j.bcno) ERPRTYN"
                        '20210429 jhs 검사코드 추가
                        sSql += "       , f6.testcd"
                        '--------------------------------------------
                        sSql += "  FROM lj010m j,  lj011m j1, lf060m f6,"
                        '<< JJH 세포면역검사[H4] 작업번호 바코드에 출력되도록
                        'sSql += "  FROM lj010m j,  lr010m r, lj011m j1, lf060m f6,"
                        '>>
                        sSql += "       lf030m f3, lf040m f4"
                        sSql += " WHERE j.bcno     = :bcno"
                        sSql += "   AND j.bcno     = j1.bcno"
                        sSql += "   AND j1.spccd   = f3.spccd"
                        sSql += "   AND j1.colldt >= f3.usdt"
                        sSql += "   AND j1.colldt <  f3.uedt"
                        sSql += "   AND j1.tclscd  = f6.testcd"
                        sSql += "   AND j1.spccd   = f6.spccd"
                        sSql += "   AND j1.colldt >= f6.usdt"
                        sSql += "   AND j1.colldt <  f6.uedt"
                        sSql += "   AND f6.tubecd  = f4.tubecd"
                        sSql += "   AND j1.colldt >= f4.usdt"
                        sSql += "   AND j1.colldt <  f4.uedt"

                        '<< JJH 세포면역검사[H4] 작업번호 바코드에 출력되도록
                        'sSql += "   AND j.bcno     = r.bcno"
                        'sSql += "   AND r.testcd   = f6.testcd "
                        'sSql += "   AND r.spccd    = f6.spccd  "
                        'sSql += "   AND r.tkdt    >= f6.usdt "
                        'sSql += "   AND r.tkdt    <= f6.uedt "


                    End If

                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, ra_Bcno(ix).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_Bcno(ix).ToString()))

                    DbCommand()
                    Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt.Rows.Count > 0 Then
                        'For ix2 As Integer = 0 To dt.Rows.Count - 1
                        Dim ix2 As Integer = 0

                        Dim objBcInfo As New STU_BCPRTINFO
                        With objBcInfo
                            .BCCLSCD = dt.Rows(ix2).Item("bcclscd").ToString
                            .BCCNT = IIf(dt.Rows(ix2).Item("bccnt").ToString = "B", "B", rsBarCnt).ToString
                            .BCNO = dt.Rows(ix2).Item("bcno").ToString
                            .BCNO_MB = ""
                            .BCNOPRT = dt.Rows(ix2).Item("prtbcno").ToString
                            .BCTYPE = ""
                            .DEPTWARD = dt.Rows(ix2).Item("deptinfo").ToString
                            .EMER = dt.Rows(ix2).Item("statgbn").ToString
                            .HREGNO = ""
                            .INFINFO = dt.Rows(ix2).Item("infinfo").ToString
                            .IOGBN = dt.Rows(ix2).Item("iogbn").ToString
                            .PATNM = dt.Rows(ix2).Item("patnm").ToString
                            .REGNO = dt.Rows(ix2).Item("regno").ToString
                            .REMARK = dt.Rows(ix2).Item("doctorrmk").ToString
                            .SEXAGE = dt.Rows(ix2).Item("sexage").ToString
                            .SPCNM = dt.Rows(ix2).Item("spcnmbp").ToString
                            .TGRPNM = dt.Rows(ix2).Item("tgrpnm").ToString
                            .TESTNMS = dt.Rows(ix2).Item("testnms").ToString
                            .TUBENM = dt.Rows(ix2).Item("tubenmbp").ToString
                            .XMATCH = ""
                            .TESTCD = dt.Rows(ix2).Item("testcd").ToString

                            '<< JJH 자체응급
                            .ERPRTYN = dt.Rows(ix2).Item("ERPRTYN").ToString

                            ' 혈액형 여부 표시 (있을때 공란, 없을때 * 표시)
                            Dim ABOCHK As String = OCSAPP.OcsLink.SData.fnget_ABO(dt.Rows(ix2).Item("regno").ToString)
                            .ABOCHK = ABOCHK

                        End With

                        alBcData.Add(objBcInfo)
                        'Next
                    End If
                Next

                If alBcData.Count > 0 Then PrintDo(alBcData, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Public Sub PrintDo_Mic_Barcode(ByVal ra_Bcno As ArrayList, ByVal rsBarCnt As String)
            Dim sFn As String = "Public Sub PrintDo_Mic_Barcode(ByVal ra_Bcno As ArrayList, ByVal rsBarCnt As String)"

            Try
                Dim alBcData As New ArrayList

                For ix As Integer = 0 To ra_Bcno.Count - 1
                    Dim sSql As String = ""
                    Dim alParm As New ArrayList

                    
                    sSql = ""
                    sSql += "SELECT DISTINCT"
                    sSql += "       j.regno,   j.patnm, j.sex || '/' || j.age sexage, j.bcclscd,"
                    sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                    sSql += "       j.iogbn, f3.spcnmbp, f4.tubenmbp || ' ' || f6.minspcvol tubenmbp, j.statgbn,"
                    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                    sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                    sSql += "       (SELECT doctorrmk"
                    sSql += "          FROM lj011m"
                    sSql += "         WHERE bcno    = j.bcno"
                    sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                    sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                    sSql += "           AND ROWNUM= 1"
                    sSql += "       ) doctorrmk,"
                    sSql += "       REPLACE(fn_ack_get_infection_prt(j.regno), ',', '/') infinfo,"
                    'sSql += "       fn_ack_get_test_nmbp_list(j.bcno) testnms,"
                    sSql += "       (SELECT listagg(b.tnmbp,',') within group (order by b.dispseql)"
                    sSql += "          FROM lj011m a, lf060m b"
                    sSql += "         WHERE a.bcno   = j.bcno"
                    sSql += "           AND a.tclscd = b.testcd  AND a.spccd  = b.spccd"
                    sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt   > j.bcprtdt"
                    sSql += "       ) testnms,"
                    sSql += "       fn_ack_get_tgrp_nmbp_list(j.bcno) tgrpnm,"
                    sSql += "       CASE WHEN f6.bccnt = 'B' THEN f6.bccnt ELSE '1' END bccnt"
                    sSql += "       ,fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno "  '<< JJH 세포면역[H4] 작업번호 '
                    'sSql += "  FROM lj010m j,  lj011m j1, lf060m f6,"

                    '<< JJH 세포면역검사[H4] 작업번호 바코드에 출력되도록
                    sSql += "  FROM lj010m j,  lr010m r, lj011m j1, lf060m f6,"
                    '>>
                    sSql += "       lf030m f3, lf040m f4"
                    sSql += " WHERE j.bcno     = :bcno"
                    sSql += "   AND j.bcno     = j1.bcno"
                    sSql += "   AND j1.spccd   = f3.spccd"
                    sSql += "   AND j1.colldt >= f3.usdt"
                    sSql += "   AND j1.colldt <  f3.uedt"
                    sSql += "   AND j1.tclscd  = f6.testcd"
                    sSql += "   AND j1.spccd   = f6.spccd"
                    sSql += "   AND j1.colldt >= f6.usdt"
                    sSql += "   AND j1.colldt <  f6.uedt"
                    sSql += "   AND f6.tubecd  = f4.tubecd"
                    sSql += "   AND j1.colldt >= f4.usdt"
                    sSql += "   AND j1.colldt <  f4.uedt"

                    '<< JJH 세포면역검사[H4] 작업번호 바코드에 출력되도록
                    sSql += "   AND j.bcno     = r.bcno"
                    sSql += "   AND r.testcd   = f6.testcd "
                    sSql += "   AND r.spccd    = f6.spccd  "
                    sSql += "   AND r.tkdt    >= f6.usdt "
                    sSql += "   AND r.tkdt    <= f6.uedt "

                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, ra_Bcno(ix).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_Bcno(ix).ToString()))

                    DbCommand()
                    Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt.Rows.Count > 0 Then
                        'For ix2 As Integer = 0 To dt.Rows.Count - 1
                        Dim ix2 As Integer = 0

                        Dim objBcInfo As New STU_BCPRTINFO
                        With objBcInfo
                            .BCCLSCD = dt.Rows(ix2).Item("bcclscd").ToString
                            .BCCNT = IIf(dt.Rows(ix2).Item("bccnt").ToString = "B", "B", rsBarCnt).ToString
                            .BCNO = dt.Rows(ix2).Item("bcno").ToString
                            '.BCNO_MB = ""
                            '<< JJH 세포면역검사[H4]일때 작업번호 바코드 표기되도록
                            .BCNO_MB = IIf(.BCCLSCD = "H4", dt.Rows(ix2).Item("workno").ToString, "").ToString

                            .BCNOPRT = dt.Rows(ix2).Item("prtbcno").ToString
                            '.BCTYPE = ""

                            '<< JJH 세포면역검사[H4]일때 작업번호 바코드 표기되도록
                            .BCTYPE = IIf(.BCCLSCD = "H4", "M", "").ToString

                            .DEPTWARD = dt.Rows(ix2).Item("deptinfo").ToString
                            .EMER = dt.Rows(ix2).Item("statgbn").ToString
                            .HREGNO = ""
                            .INFINFO = dt.Rows(ix2).Item("infinfo").ToString
                            .IOGBN = dt.Rows(ix2).Item("iogbn").ToString
                            .PATNM = dt.Rows(ix2).Item("patnm").ToString
                            .REGNO = dt.Rows(ix2).Item("regno").ToString
                            .REMARK = dt.Rows(ix2).Item("doctorrmk").ToString
                            .SEXAGE = dt.Rows(ix2).Item("sexage").ToString
                            .SPCNM = dt.Rows(ix2).Item("spcnmbp").ToString
                            .TGRPNM = dt.Rows(ix2).Item("tgrpnm").ToString
                            .TESTNMS = dt.Rows(ix2).Item("testnms").ToString
                            .TUBENM = dt.Rows(ix2).Item("tubenmbp").ToString
                            .XMATCH = ""

                            ' 혈액형 여부 표시 (있을때 공란, 없을때 * 표시)
                            Dim ABOCHK As String = OCSAPP.OcsLink.SData.fnget_ABO(dt.Rows(ix2).Item("regno").ToString)
                            .ABOCHK = ABOCHK

                        End With

                        alBcData.Add(objBcInfo)
                        'Next
                    End If
                Next

                If alBcData.Count > 0 Then PrintDo(alBcData, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Public Sub PrintDo_ris(ByVal ra_Bcno As ArrayList, ByVal rsBarCnt As String)
            Dim sFn As String = "PrintDo_ris"
            Try
                Dim alBcData As New ArrayList

                For ix As Integer = 0 To ra_Bcno.Count - 1
                    Dim sSql As String = ""
                    Dim alParm As New ArrayList
                    Dim sTableNm As String = "rr010m"

                    sSql = ""
                    sSql += "SELECT DISTINCT"
                    sSql += "       j.regno,   j.patnm, j.sex || '/' || j.age sexage, j.bcclscd,"
                    sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                    sSql += "       j.iogbn, f3.spcnmbp, f4.tubenmbp || ' ' || f6.minspcvol tubenmbp, j.statgbn,"
                    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                    sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                    sSql += "       LISTAGG(j1.doctorrmk, ',') WITHIN GROUP (ORDER BY f6.dispseql) doctorrmk,"
                    sSql += "       REPLACE(fn_ack_get_infection_prt(j.bcno), ',', '/') infinfo,"
                    sSql += "       LISTAGG(f6.tnmbp, ',')  WITHIN GROUP (ORDER BY f6.dispseql) testnms,"
                    sSql += "       LISTAGG(f65.tgrpnmbp, ',')  WITHIN GROUP (ORDER BY f6.dispseql) tgrpnm,"
                    sSql += "       r.wkymd || r.wkgrpcd || r.wkno as wkno"
                    sSql += "  FROM rj010m j , rj011m j1, rr010m r, rf060m f6,"
                    sSql += "       lf030m f3, lf040m f4,"
                    sSql += "       (SELECT f65.testcd, f65.spccd, f65.tgrpnmbp"
                    sSql += "          FROM rj011m j, rf065m f65"
                    sSql += "         WHERE j.bcno   = :bcno"
                    sSql += "           AND j.tclscd = f65.testcd"
                    sSql += "           AND j.spccd  = f65.spccd"
                    sSql += "         UNION"
                    sSql += "        SELECT f65.testcd, f65.spccd, f65.tgrpnmbp"
                    sSql += "          FROM rj011m j, rf062m f62, rf065m f65"
                    sSql += "         WHERE j.bcno     = :bcno"
                    sSql += "           AND j.tclscd   = f62.tclscd"
                    sSql += "           AND j.spccd    = f62.tspccd"
                    sSql += "           AND f62.testcd = f65.testcd"
                    sSql += "           AND f62.spccd  = f65.spccd"
                    sSql += "       ) f65"
                    sSql += " WHERE j.bcno     = :bcno"
                    sSql += "   AND j.bcno     = j1.bcno"
                    sSql += "   AND j1.bcno    = r.bcno"
                    sSql += "   AND j1.tclscd  = r.tclscd"
                    sSql += "   AND r.spccd    = f3.spccd"
                    sSql += "   AND r.tkdt    >= f3.usdt"
                    sSql += "   AND r.tkdt    <  f3.uedt"
                    sSql += "   AND r.tclscd   = f6.testcd"
                    sSql += "   AND r.spccd    = f6.spccd"
                    sSql += "   AND r.tkdt    >= f6.usdt"
                    sSql += "   AND r.tkdt    <  f6.uedt"
                    sSql += "   AND f6.tubecd  = f4.tubecd"
                    sSql += "   AND r.tkdt    >= f4.usdt"
                    sSql += "   AND r.tkdt    <  f4.uedt"
                    sSql += "   AND r.testcd   = f65.testcd (+)"
                    sSql += "   AND r.spccd    = f65.spccd (+)"
                    sSql += " GROUP BY j.bcno, j.regno, j.patnm, j.sex, j.age , j.bcclscd, j.wardno, j.roomno, j.iogbn, j.deptcd,"
                    sSql += "       f3.spcnmbp, j.statgbn, r.wkymd, r.wkgrpcd, r.wkno, f4.tubenmbp || ' ' || f6.minspcvol"

                    alParm.Add(New OracleParameter("bcno", ra_Bcno(ix).ToString()))
                    alParm.Add(New OracleParameter("bcno", ra_Bcno(ix).ToString()))
                    alParm.Add(New OracleParameter("bcno", ra_Bcno(ix).ToString()))

                    DbCommand()
                    Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt.Rows.Count > 0 Then
                        For ix2 As Integer = 0 To dt.Rows.Count - 1

                            Dim alTnmbp As New ArrayList
                            Dim alTgrpNm As New ArrayList
                            Dim sTnmBp() As String = dt.Rows(ix2).Item("testnms").ToString.Split(","c)
                            Dim sTgrpNm() As String = dt.Rows(ix2).Item("tgrpnm").ToString.Split(","c)

                            For ix3 As Integer = 0 To sTnmBp.Length - 1
                                If alTnmbp.Contains(sTnmBp(ix3)) Then
                                Else
                                    alTnmbp.Add(sTnmBp(ix3))
                                End If
                            Next

                            For ix3 As Integer = 0 To sTgrpNm.Length - 1
                                If alTgrpNm.Contains(sTgrpNm(ix3)) Then
                                Else
                                    alTgrpNm.Add(sTgrpNm(ix3))
                                End If
                            Next


                            Dim objBcInfo As New STU_BCPRTINFO
                            With objBcInfo
                                .BCCLSCD = dt.Rows(ix2).Item("bcclscd").ToString
                                .BCCNT = rsBarCnt
                                .BCNO = dt.Rows(ix2).Item("bcno").ToString
                                .BCNO_MB = ""
                                .BCNOPRT = dt.Rows(ix2).Item("prtbcno").ToString
                                .BCTYPE = ""
                                .DEPTWARD = dt.Rows(ix2).Item("deptinfo").ToString
                                .EMER = dt.Rows(ix2).Item("statgbn").ToString
                                .HREGNO = ""
                                .INFINFO = dt.Rows(ix2).Item("infinfo").ToString
                                .IOGBN = dt.Rows(ix2).Item("iogbn").ToString
                                .PATNM = dt.Rows(ix2).Item("patnm").ToString
                                .REGNO = dt.Rows(ix2).Item("regno").ToString
                                .REMARK = dt.Rows(ix2).Item("doctorrmk").ToString
                                .SEXAGE = dt.Rows(ix2).Item("sexage").ToString
                                .SPCNM = dt.Rows(ix2).Item("spcnmbp").ToString

                                For ix3 As Integer = 0 To alTgrpNm.Count - 1
                                    .TGRPNM += alTgrpNm(ix3).ToString + ","
                                Next

                                For ix3 As Integer = 0 To alTnmbp.Count - 1
                                    .TESTNMS += alTnmbp(ix3).ToString + ","
                                Next

                                .TUBENM = dt.Rows(ix2).Item("wkno").ToString
                                .XMATCH = ""
                            End With

                            alBcData.Add(objBcInfo)
                        Next
                    End If
                Next

                If alBcData.Count > 0 Then PrintDo_ris(alBcData, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Public Sub PrintDo_Micro(ByVal ra_Bcno As ArrayList, ByVal rsBarCnt As String, Optional ByVal rsBcBoolean As Boolean = False)
            Dim sFn As String = "PrintDo_Micro"

            Try
                Dim alBcData As New ArrayList

                For ix As Integer = 0 To ra_Bcno.Count - 1
                    Dim sSql As String = ""
                    Dim alParm As New ArrayList

                    sSql = ""
                    sSql += "SELECT DISTINCT" + vbCrLf
                    sSql += "       j.regno,   j.patnm, j.sex || '/' || j.age sexage, j.bcclscd, g.cultnm, g.bccnt," + vbCrLf
                    sSql += "       FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || CASE WHEN j.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(j.wardno) ELSE '' END deptinfo," + vbCrLf
                    sSql += "       j.iogbn, f3.spcnmbp, f4.tubenmbp || ' ' || f6.minspcvol tubenmbp, f6.testcd, f6.tnmbp, j.statgbn," + vbCrLf
                    sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno," + vbCrLf
                    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno," + vbCrLf
                    sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno," + vbCrLf
                    sSql += "       (SELECT doctorrmk" + vbCrLf
                    sSql += "          FROM lj011m" + vbCrLf
                    sSql += "         WHERE bcno    = j.bcno" + vbCrLf
                    sSql += "           AND spcflg IN ('1', '2', '3', '4')" + vbCrLf
                    sSql += "           AND NVL(doctorrmk, ' ') <> ' '" + vbCrLf
                    sSql += "           AND ROWNUM= 1" + vbCrLf
                    sSql += "       ) doctorrmk," + vbCrLf
                    sSql += "       REPLACE(fn_ack_get_infection_prt(j.bcno), ',', '/') infinfo" + vbCrLf
                    sSql += "  FROM lj010m j,  lm010m r , lf060m f6," + vbCrLf
                    sSql += "       lf030m f3, lf040m f4, lf250m g " + vbCrLf
                    sSql += " WHERE j.bcno    = :bcno" + vbCrLf
                    sSql += "   AND j.bcno    = r.bcno" + vbCrLf
                    sSql += "   AND r.spccd   = f3.spccd" + vbCrLf
                    sSql += "   AND r.tkdt   >= f3.usdt" + vbCrLf
                    sSql += "   AND r.tkdt   <  f3.uedt" + vbCrLf
                    sSql += "   AND r.testcd  = f6.testcd" + vbCrLf
                    sSql += "   AND r.spccd   = f6.spccd" + vbCrLf
                    sSql += "   AND r.tkdt   >= f6.usdt" + vbCrLf
                    sSql += "   AND r.tkdt   <  f6.uedt" + vbCrLf
                    sSql += "   AND f6.tubecd = f4.tubecd" + vbCrLf
                    sSql += "   AND r.tkdt   >= f4.usdt" + vbCrLf
                    sSql += "   AND r.tkdt   <  f4.uedt" + vbCrLf
                    sSql += "   AND r.testcd  = g.testcd" + vbCrLf
                    sSql += "   AND r.spccd   = g.spccd" + vbCrLf
                    sSql += "   AND SUBSTR(r.tkdt, 5, 4) >= g.usedays" + vbCrLf
                    sSql += "   AND SUBSTR(r.tkdt, 5, 4) <= g.usedaye" + vbCrLf
                    sSql += " ORDER BY g.cultnm" + vbCrLf

                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, ra_Bcno(ix).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_Bcno(ix).ToString()))

                    DbCommand()
                    Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt.Rows.Count > 0 Then
                        For ix2 As Integer = 0 To dt.Rows.Count - 1

                            Dim objBcInfo As New STU_BCPRTINFO
                            With objBcInfo
                                .BCCLSCD = dt.Rows(ix2).Item("bcclscd").ToString
                                '20210218 jhs 바코드 출력장 수로 출력 할 수 잇도록 구현
                                If rsBcBoolean Then
                                    .BCCNT = rsBarCnt
                                Else
                                    .BCCNT = dt.Rows(ix2).Item("bccnt").ToString : If .BCCNT = "" Then .BCCNT = rsBarCnt
                                End If
                                '--------------------------------------
                                .BCNO = dt.Rows(ix2).Item("bcno").ToString
                                .BCNO_MB = dt.Rows(ix2).Item("workno").ToString
                                .BCNOPRT = dt.Rows(ix2).Item("prtbcno").ToString
                                .BCTYPE = "M"
                                .DEPTWARD = dt.Rows(ix2).Item("deptinfo").ToString
                                .EMER = dt.Rows(ix2).Item("statgbn").ToString
                                .HREGNO = ""
                                .INFINFO = dt.Rows(ix2).Item("infinfo").ToString
                                .IOGBN = dt.Rows(ix2).Item("iogbn").ToString
                                .PATNM = dt.Rows(ix2).Item("patnm").ToString
                                .REGNO = dt.Rows(ix2).Item("regno").ToString
                                .REMARK = dt.Rows(ix2).Item("doctorrmk").ToString
                                .SEXAGE = dt.Rows(ix2).Item("sexage").ToString
                                .SPCNM = dt.Rows(ix2).Item("spcnmbp").ToString
                                .TGRPNM = dt.Rows(ix2).Item("cultnm").ToString
                                .TESTNMS = ""
                                .TUBENM = dt.Rows(ix2).Item("tubenmbp").ToString
                                .XMATCH = ""

                                ' 혈액형 여부 표시 (있을때 공란, 없을때 * 표시)
                                Dim ABOCHK As String = OCSAPP.OcsLink.SData.fnget_ABO(dt.Rows(ix2).Item("regno").ToString)
                                .ABOCHK = ABOCHK

                            End With

                            alBcData.Add(objBcInfo)
                        Next
                    End If
                Next

                If alBcData.Count > 0 Then PrintDo(alBcData, True)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Public Sub PrintDo_ris(ByVal ra_Bcno As ArrayList, ByVal rsBarCnt As String, ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String)
            Dim sFn As String = "PrintDo_ris"

            Try
                Dim alBcData As New ArrayList

                For ix As Integer = 0 To ra_Bcno.Count - 1
                    Dim sSql As String = ""
                    Dim alParm As New ArrayList

                    sSql = ""
                    sSql += "SELECT DISTINCT"
                    sSql += "       j.regno,   j.patnm, j.sex || '/' || j.age sexage, j.bcclscd, rw.title,"
                    sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                    sSql += "       j.iogbn, f3.spcnmbp, f4.tubenmbp || ' ' || f6.minspcvol tubenmbp, f6.testcd, f6.tnmbp, j.statgbn,"
                    sSql += "       rw.wlymd || '-' || rw.slseq workno,"
                    sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                    sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                    sSql += "       (SELECT doctorrmk"
                    sSql += "          FROM rj011m"
                    sSql += "         WHERE bcno    = j.bcno"
                    sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                    sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                    sSql += "           AND ROWNUM= 1"
                    sSql += "       ) doctorrmk,"
                    sSql += "       REPLACE(fn_ack_get_infection_prt(j.bcno), ',', '/') infinfo"
                    sSql += "  FROM rj010m j,  rr010m r , rf060m f6,"
                    sSql += "       lf030m f3, lf040m f4, rrw11m rw "
                    sSql += " WHERE j.bcno     = :bcno"
                    sSql += "   AND j.bcno     = r.bcno"
                    sSql += "   AND r.spccd    = f3.spccd"
                    sSql += "   AND r.tkdt    >= f3.usdt"
                    sSql += "   AND r.tkdt    <  f3.uedt"
                    sSql += "   AND r.testcd   = f6.testcd"
                    sSql += "   AND r.spccd    = f6.spccd"
                    sSql += "   AND r.tkdt    >= f6.usdt"
                    sSql += "   AND r.tkdt    <  f6.uedt"
                    sSql += "   AND f6.tubecd  = f4.tubecd"
                    sSql += "   AND r.tkdt    >= f4.usdt"
                    sSql += "   AND r.tkdt    <  f4.uedt"
                    sSql += "   AND r.bcno     = rw.bcno"
                    sSql += "   AND r.testcd   = rw.testcd"
                    sSql += "   AND rw.wluid   = :wluid"
                    sSql += "   AND rw.wlymd   = :wlymd"
                    sSql += "   AND rw.wltitle = :wltitle"
                    sSql += " ORDER BY g.cultnm"

                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, ra_Bcno(ix).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_Bcno(ix).ToString()))
                    alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                    alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                    alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))

                    DbCommand()
                    Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                    If dt.Rows.Count > 0 Then
                        For ix2 As Integer = 0 To dt.Rows.Count - 1

                            Dim objBcInfo As New STU_BCPRTINFO
                            With objBcInfo
                                .BCCLSCD = dt.Rows(ix2).Item("bcclscd").ToString
                                .BCCNT = rsBarCnt
                                .BCNO = dt.Rows(ix2).Item("bcno").ToString
                                .BCNO_MB = dt.Rows(ix2).Item("workno").ToString
                                .BCNOPRT = dt.Rows(ix2).Item("prtbcno").ToString
                                .BCTYPE = "M"
                                .DEPTWARD = dt.Rows(ix2).Item("deptinfo").ToString
                                .EMER = dt.Rows(ix2).Item("statgbn").ToString
                                .HREGNO = ""
                                .INFINFO = dt.Rows(ix2).Item("infinfo").ToString
                                .IOGBN = dt.Rows(ix2).Item("iogbn").ToString
                                .PATNM = dt.Rows(ix2).Item("patnm").ToString
                                .REGNO = dt.Rows(ix2).Item("regno").ToString
                                .REMARK = dt.Rows(ix2).Item("doctorrmk").ToString
                                .SEXAGE = dt.Rows(ix2).Item("sexage").ToString
                                .SPCNM = dt.Rows(ix2).Item("spcnmbp").ToString
                                .TGRPNM = dt.Rows(ix2).Item("cultnm").ToString
                                .TESTNMS = ""
                                .TUBENM = dt.Rows(ix2).Item("tubenmbp").ToString
                                .XMATCH = ""
                            End With

                            alBcData.Add(objBcInfo)
                        Next
                    End If
                Next

                If alBcData.Count > 0 Then PrintDo(alBcData, True)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Private Sub sbPrint()
            Dim sFn As String = "Private Sub sbPrint()"

            Dim objBCPrt As New BCPRT01.BCPRT
            Dim blnRetVal As Boolean
            Dim strWardYN As String

            Try
                strWardYN = USER_INFO.USRLVL

                Debug.WriteLine(GetInfo.PRTID & ", " & GetInfo.OUTIP & ", " & GetInfo.PRTNM & ", " & GetInfo.OUTPORT)

                blnRetVal = objBCPrt.BarCodePrtOut(maPrtData, CInt(GetInfo.PRTID), GetInfo.IOPORT, _
                                                   GetInfo.OUTIP, mbFirst, _
                                                   CInt(Val(GetInfo.LEFTMARGIN)), CInt(Val(GetInfo.TOPMARGIN)), GetInfo.PRTTYPE)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Sub

        Private Sub sbPrint_ris()
            Dim sFn As String = "Private Sub sbPrint_ris()"

            Dim objBCPrt As New BCPRT01.BCPRT
            Dim blnRetVal As Boolean
            Dim strWardYN As String

            Try
                strWardYN = USER_INFO.USRLVL

                Debug.WriteLine(GetInfo.PRTID & ", " & GetInfo.OUTIP & ", " & GetInfo.PRTNM & ", " & GetInfo.OUTPORT)

                blnRetVal = objBCPrt.BarCodePrtOut_ris(maPrtData, CInt(GetInfo.PRTID), GetInfo.IOPORT, _
                                                   GetInfo.OUTIP, mbFirst, _
                                                   CInt(Val(GetInfo.LEFTMARGIN)), CInt(Val(GetInfo.TOPMARGIN)), GetInfo.PRTTYPE)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Sub

        Private Sub sbPrint(ByVal rsPrinterName As String)
            Dim sFn As String = "Private Sub fnPrint(String)"

            Dim objBCPrt As New BCPRT01.BCPRT
            Dim bRetVal As Boolean = False

            Try
                Debug.WriteLine(GetInfo.PRTID & ", " & GetInfo.OUTIP & ", " & GetInfo.PRTNM & ", " & rsPrinterName)

                bRetVal = objBCPrt.BarCodePrtOut(maPrtData, CInt(GetInfo.PRTID), GetInfo.IOPORT, GetInfo.OUTIP, _
                                                   mbFirst, CInt(Val(GetInfo.LEFTMARGIN)), CInt(Val(GetInfo.TOPMARGIN)), _
                                                   GetInfo.PRTTYPE)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Private Sub sbPrint_pis(ByVal rsPrinterName As String)
            Dim sFn As String = "Private Sub fnPrint(String)"

            Dim objBCPrt As New BCPRT01.BCPRT

            Try
                Debug.WriteLine(GetInfo.PRTID & ", " & GetInfo.OUTIP & ", " & GetInfo.PRTNM & ", " & rsPrinterName)

                Dim bRetVal As Boolean = (New BCPRT01.BCPRT).BarCodePrtOut_PIS(maPrtData, CInt(GetInfo.PRTID), GetInfo.IOPORT, GetInfo.OUTIP, _
                                                   mbFirst, CInt(Val(GetInfo.LEFTMARGIN)), CInt(Val(GetInfo.TOPMARGIN)), _
                                                   GetInfo.PRTTYPE)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Sub

        '< add yjlee 2009-04-24 
        Private Sub sbPrint_Micro()
            Dim sFn As String = "Private Sub sbPrint_Micro()"

            Dim objBCPrt As New BCPRT01.BCPRT
            Dim bRetVal As Boolean = False
            Dim sWardYN As String

            Try
                sWardYN = USER_INFO.USRLVL

                Debug.WriteLine(GetInfo.PRTID & ", " & GetInfo.OUTIP & ", " & GetInfo.PRTNM & ", " & GetInfo.OUTPORT)


                bRetVal = objBCPrt.BarCodePrtOut_Micro(maPrtData, CInt(GetInfo.PRTID), GetInfo.OUTPORT, GetInfo.OUTIP, _
                                                         CInt(Val(GetInfo.LEFTMARGIN)), CInt(Val(GetInfo.TOPMARGIN)), GetInfo.PRTTYPE)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub
        '> add yjlee 2009-04-24 

        Private Sub sbPrint_blood(ByVal riCopy As Integer)
            Dim sFn As String = "Private Sub sbPrint_blood()"

            Dim objBCPrt As New BCPRT01.BCPRT
            Dim bRetVal As Boolean = False

            Try

                Debug.WriteLine(GetInfo.PRTID + ", " + GetInfo.OUTIP + ", " + GetInfo.PRTNM + ", " + GetInfo.OUTPORT)

                bRetVal = objBCPrt.BarCodePrtOut_Blood(maPrtData, riCopy, CInt(GetInfo.PRTID), GetInfo.IOPORT, GetInfo.OUTIP, _
                                                       CInt(Val(GetInfo.LEFTMARGIN)), CInt(Val(GetInfo.TOPMARGIN)))

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

#Region " clsAutoLabelerDF "
        Public Class clsAutoLabelerDF
            Private STX As String = Chr(2)
            Public BARCODE As String = ""       ' 11: 바코드( 년월일(4)+구분(2)+SEQ1(4)+SEQ2(1) )
            Public REGNO As String = ""         ' 08: 등록번호                                   
            Public PATNM As String = ""         ' 20: 환자명
            Public SA As String = ""            ' 05: 성별(M/F)/나이
            Public TSECTGBN As String = ""      ' 02: 계+검사계
            Public DW As String = ""            ' 08: 진료과병동
            Public DATESTR As String = ""       ' 08: YYYYMMDD
            Public IOGBN As String = ""         ' 01: I:입원, O:외래 구분
            Public BCNO As String = ""          ' 18: 20030923-A0-0001-0 (FULL)
            Public ROOMCD As String = ""        ' 10: 검사실 코드
            Public ST_SPCNM As String = ""      ' 14: 검체명(5)+'/'
            Public ST_TUBENM As String = ""     '    +용기명(8)
            Public TUBECD As String = ""        ' 02: 용기코드
            Public TUBENM As String = ""        ' 10: 용기명
            Public COLLVOL As String = ""       ' 02: 체혈량
            Private mCMT As String = ""         ' 50: 진료측 COMMENT
            Public ETC1 As String = ""          ' 10: 예비1 (응급여부)
            Public ETC2 As String = ""          ' 10: 예비2 (감염여부)
            Public ETC3 As String = ""          ' 10: 예비3 (추가인쇄여부)
            Public mETC4 As String = ""        ' 50: 예비4 (검사그룹)
            Public ETC5 As String = ""
            Public ETC6 As String = ""     '
            'Public ENDCD As String = ""         ' 02: END CODE( 01 or 02 ) 

            Public Property CMT() As String
                Get
                    CMT = mCMT
                End Get
                Set(ByVal Value As String)
                    Dim arrTmp As String()
                    Dim intLength As Integer = 0
                    Dim intCurLength As Integer

                    mCMT = ""
                    If Fn.LengthH(Value) > 34 Then
                        arrTmp = Split(Value, " ")
                        For intCnt As Integer = 0 To arrTmp.Length - 1
                            intCurLength = Fn.LengthH(arrTmp(intCnt))
                            If intLength + intCurLength + 1 > 30 Then
                                Exit For
                            Else
                                If mCMT = "" Then
                                    intLength += intCurLength
                                    mCMT = arrTmp(intCnt)
                                Else
                                    intLength += intCurLength + 1
                                    mCMT &= " " & arrTmp(intCnt)
                                End If
                            End If
                        Next
                        mCMT &= " ..."
                    Else
                        mCMT = Value
                    End If
                End Set
            End Property

            Public Property ETC4() As String
                Get
                    ETC4 = mETC4
                End Get
                Set(ByVal Value As String)
                    Dim arrTmp As String()
                    Dim intLength As Integer = 0
                    Dim intCurLength As Integer

                    mETC4 = ""
                    If Fn.LengthH(Value) > 34 Then
                        arrTmp = Split(Value, " ")
                        For intCnt As Integer = 0 To arrTmp.Length - 1
                            intCurLength = Fn.LengthH(arrTmp(intCnt))
                            If intLength + intCurLength + 1 > 30 Then
                                Exit For
                            Else
                                If mETC4 = "" Then
                                    intLength += intCurLength
                                    mETC4 = arrTmp(intCnt)
                                Else
                                    intLength += intCurLength + 1
                                    mETC4 &= " " & arrTmp(intCnt)
                                End If
                            End If
                        Next
                        mETC4 &= " ..."
                    Else
                        mETC4 = Value
                    End If
                End Set
            End Property

            Public ReadOnly Property GetMessage() As String
                Get
                    Dim strMsg As String

                    strMsg = STX & Space(1)
                    strMsg &= Fn.PadRightH(BARCODE, 11) & Space(1)
                    strMsg &= Fn.PadRightH(REGNO, 9) & Space(1)
                    strMsg &= Fn.PadRightH(PATNM, 20) & Space(1)
                    strMsg &= Fn.PadRightH(SA, 5) & Space(1)
                    strMsg &= Fn.PadRightH(TSECTGBN, 2) & Space(1)
                    strMsg &= Fn.PadRightH(DW, 20) & Space(1)
                    strMsg &= Fn.PadRightH(DATESTR, 8) & Space(1)
                    strMsg &= Fn.PadRightH(IOGBN, 1) & Space(1)
                    strMsg &= Fn.PadRightH(BCNO, 18) & Space(1)
                    strMsg &= Fn.PadRightH(" ", 10) & Space(1)
                    strMsg &= Fn.PadRightH(ST_SPCNM, 5) '& Space(1) 
                    strMsg &= Fn.PadRightH(ST_TUBENM, 8) & Space(1)
                    strMsg &= Fn.PadRightH(" ", 2) & Space(1)
                    strMsg &= Fn.PadRightH(ST_TUBENM, 10) & Space(1)
                    strMsg &= Fn.PadRightH(" ", 2) & Space(1)
                    strMsg &= Fn.PadRightH(mCMT, 50) & Space(1)
                    strMsg &= Fn.PadRightH(" ", 10) & Space(1)
                    strMsg &= Fn.PadRightH(ETC1, 10) & Space(1)
                    strMsg &= Fn.PadRightH(ETC3, 10) & Space(1)
                    strMsg &= Fn.PadRightH(ETC4, 12) & Space(1)
                    strMsg &= Fn.PadRightH(ETC5, 4) & Space(1)
                    strMsg &= Fn.PadRightH(ETC6, 16) & Space(1)
                    strMsg &= "Y" & Space(1)

                    GetMessage = strMsg
                End Get
            End Property

            Public Sub New()
                MyBase.New()
            End Sub

            Public Sub Clear()
                BARCODE = ""       ' 11: 바코드( 년월일(4)+구분(2)+SEQ1(4)+SEQ2(1) )
                REGNO = ""         ' 08: 등록번호                                   
                PATNM = ""         ' 20: 환자명
                SA = ""            ' 05: 성별(M/F)/나이
                TSECTGBN = ""      ' 02: 계+검사계
                DW = ""            ' 08: 진료과병동
                DATESTR = ""       ' 08: YYYYMMDD
                IOGBN = ""         ' 01: I:입원, O:외래 구분
                BCNO = ""          ' 18: 20030923-A0-0001-0 (FULL)
                ROOMCD = ""        ' 10: 검사실 코드
                ST_SPCNM = ""      ' 14: 검체명(5)+'/'
                ST_TUBENM = ""     '    +용기명(8)
                TUBECD = ""        ' 02: 용기코드
                TUBENM = ""        ' 10: 용기명
                COLLVOL = ""       ' 02: 체혈량
                mCMT = ""          ' 50: 진료측 COMMENT
                ETC1 = ""          ' 10: 예비1
                ETC2 = ""          ' 10: 예비2
                ETC3 = ""          ' 10: 예비3
                mETC4 = ""         ' 50: 예비4
                ETC5 = ""
            End Sub

        End Class
#End Region

#Region " clsPRTInfo "
        Public Class clsPRTInfo
            Public PRTID As String = ""
            Public PRTNM As String = ""
            Public OUTIP As String = ""
            Public OUTPORT As String = ""
            Public SUPPORTIP As String = ""
            Public SELECTED As String = ""
            Public IOPORT As String = ""
            Public LEFTMARGIN As String = ""
            Public TOPMARGIN As String = ""

            '< yjlee
            Public PRTTYPE As String = ""

            Public Sub New()
                MyBase.new()
            End Sub
        End Class
#End Region

    End Class
#End Region


End Namespace
