'>>> 특수검사 결과저장 및 보고
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Imports COMMON
Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports COMMON.CommConst

Public Class FGR08
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGR08.vb, Class : FGR08" & vbTab

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
    Private Const mc_sComment As String = "V"
    Private Const mc_sMediNo As String = "W"
    Private Const mc_sTestCd As String = "Z"
    Private Const mc_sRefTestCd As String = "Y"
    Private Const mc_sGendr As String = "X"

    Private Const mc_iSklCd_ChgRst As Integer = 1        '결과 수정기능
    Private Const mc_iSklCd_ChgFn As Integer = 6         '최종보고 수정기능
    Private Const mc_iRptCd_Mw As Integer = 20           '이미 중간보고
    Private Const mc_iRptCd_Fn As Integer = 30           '이미 최종보고

    Private Const msXmlDir As String = "\XML"
    Private msSlipXml As String = Application.StartupPath & msXmlDir & "\FGR08_SLIP.XML"

    Private mbActivated As Boolean = False

    Public msBcNo As String = ""
    Public msTestCd As String = ""
    Public msUse_PartCd As String = ""

    Private msSTX As String = Convert.ToChar(2)
    Private msETX As String = Convert.ToChar(3)

    Private m_tooltip As New Windows.Forms.ToolTip

    Private m_dt_SpTest As DataTable

    Private msDisableMsg As String = ""

    Private miStSubSeq As Integer = 0

    Private m_al_StSub As New ArrayList

    Private msOrigin_RstRTF As String = ""
    Private mbAddFileGbn As Boolean = True

    Protected piUseMode As Integer = 0
    Protected psCd_Include_Exclude As String = ""

    Protected piProcessing As Integer = 0
    Protected piSkip As Integer = 0

    Private spTestTF As Boolean = True
    Private msSpSubExPrg As String = ""                '-- 2008/02/29 YEJ Add(IMG Popup인 경우 처리때문에...)
    Private msEmrPrintName As String = ""

    Private msBfRst As Boolean = False   '-- JJH 이전결과 등록 


    Private Function fnSaveImage() As Boolean

        If msEmrPrintName = "" Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "[메뉴->MEDI@CK->이미지 프린트 설정] 에서 이미지 프린트 설정해 주세요.!!")
            Return False
        End If

        Dim sFn As String = "Handles btnPrint.Click"
        Dim sFileNm As String = Me.txtBcno.Text.Replace("-", "") + " " + Me.lblTestCd.Text + " " + Me.AxPatInfo.PatNm.Replace(" ", "") '20130821 정선영 수정

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
            Dim rPrtImg As Boolean = Me.rtbStRst.print_image(sFileNm, msEmrPrintName)

            If rPrtImg = False Then
                Return False
            End If

            Dim iImgCnt As Integer = 0
            Dim dt As DataTable = (New LISAPP.APP_F_SPTEST).GetSpTestInfo(Me.lblTestCd.Text)

            iImgCnt = dt.Rows.Count

            System.Threading.Thread.Sleep(3000 * iImgCnt)

            Dim iLoop As Integer = 0

            Do While True

                If iLoop > 1000 Then Exit Do

                If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_1.jpg") Then Exit Do

                System.Threading.Thread.Sleep(1500)
                iLoop += 1
            Loop

            'Dim a_proc As Process() = Diagnostics.Process.GetProcesses()
            'For ix As Integer = 0 To a_proc.Length - 1
            '    If a_proc(ix).ProcessName.ToLower.StartsWith("ImageServerInAIBorker2005") Then

            '    End If
            'Next

            Dim sFileNms As String = ""
            Dim al_FileNm As New ArrayList

            For ix As Integer = 1 To 20
                If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString() + ".jpg") Then
                    al_FileNm.Add("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString() + ".jpg")
                End If
            Next

            If al_FileNm.Count > 0 Then
                System.Threading.Thread.Sleep(1000)

                Return (New LISAPP.APP_R.AxRstFn).fnReg_IMAGE(Me.txtBcno.Text, Me.lblTestCd.Text, al_FileNm)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "이미지 버튼을 눌러 주세요.!!")
                Return False
            End If

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return False
        End Try
    End Function
    Private Function fnSaveImage(ByVal rsBcno As String, ByVal rsTestcd As String, ByVal rsPatnm As String) As Boolean

        If msEmrPrintName = "" Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "[메뉴->MEDI@CK->이미지 프린트 설정] 에서 이미지 프린트 설정해 주세요.!!")
            Return False
        End If

        Dim sFn As String = "Handles btnPrint.Click"
        Dim sFileNm As String = rsBcno.Replace("-", "") + " " + rsTestcd + " " + rsPatnm.Replace(" ", "") '20130821 정선영 수정

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
            Dim rPrtImg As Boolean = Me.rtbStRst.print_image(sFileNm, msEmrPrintName)

            If rPrtImg = False Then
                Return False
            End If

            Dim iImgCnt As Integer = 0
            Dim dt As DataTable = (New LISAPP.APP_F_SPTEST).GetSpTestInfo(rsTestcd)

            iImgCnt = dt.Rows.Count

            System.Threading.Thread.Sleep(3000 * iImgCnt)

            Dim iLoop As Integer = 0

            Do While True

                If iLoop > 1000 Then Exit Do

                If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_1.jpg") Then Exit Do

                System.Threading.Thread.Sleep(1500)
                iLoop += 1
            Loop

            'Dim a_proc As Process() = Diagnostics.Process.GetProcesses()
            'For ix As Integer = 0 To a_proc.Length - 1
            '    If a_proc(ix).ProcessName.ToLower.StartsWith("ImageServerInAIBorker2005") Then

            '    End If
            'Next

            Dim sFileNms As String = ""
            Dim al_FileNm As New ArrayList

            For ix As Integer = 1 To 20
                If IO.File.Exists("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString() + ".jpg") Then
                    al_FileNm.Add("C:\ACK\LIS\" + sFileNm.Replace(" ", "_") + "_" + ix.ToString() + ".jpg")
                End If
            Next

            If al_FileNm.Count > 0 Then
                System.Threading.Thread.Sleep(1000)


                Return (New LISAPP.APP_R.AxRstFn).fnReg_IMAGE(rsBcno.Replace("-", ""), rsTestcd, al_FileNm)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "이미지 버튼을 눌러 주세요.!!")
                Return False
            End If

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return False
        End Try
    End Function
    Private Function fnGet_CfmSign() As String

        Dim invas_buf As New InvAs

        Try


            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\CSMLOGIN.dll", "CSMLOGIN.FGCSMLOGIN01")

                Dim a_objParam() As Object
                ReDim a_objParam(2)

                a_objParam(0) = Me
                a_objParam(1) = LISAPP.APP_DB.DbFn.fnGet_DbConnect()
                a_objParam(2) = USER_INFO.USRID

                Dim sReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If sReturn Is Nothing Then Return ""
                Return sReturn
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            invas_buf = Nothing

        End Try

    End Function

    Private Sub sbDisplay_Link_Data()
        Dim sFn As String = "sbDisplay_Link_Data"
        Try
            Dim dt As DataTable = LISAPP.APP_SP.fnGet_SpcList_Sp_bcno(msBcNo, msTestCd)

            '접수일시에 정렬 표시
            spdList.set_ColUserSortIndicator(spdList.GetColFromID("tkdt"), FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)

            Ctrl.DisplayAfterSelect(spdList, dt)

            spdList.SetActiveCell(0, 0)

            sbDisplay_Search_Color("A")

            If spdList.MaxRows = 0 Then Return

            spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(spdList.GetColFromID("bcno"), 1))

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
        End Try
    End Sub

    Private Function fnFind_Disable_Msg(ByVal riDisable As Integer) As String
        Dim sFn As String = "fnFind_Disable_Msg"

        Try
            Dim sReturn As String = ""

            Select Case riDisable
                Case 0
                    sReturn = "은(는) 변경된 내용이 없습니다. 확인하여 주십시요!!"

                Case mc_iSklCd_ChgRst, mc_iSklCd_ChgFn
                    USER_SKILL.Authority("R01", riDisable, sReturn)
                    sReturn += "의 권한이 없습니다. 확인하여 주십시요!!"

                Case mc_iRptCd_Mw
                    sReturn = "은(는) 이미 중간보고된 상태입니다. 확인하여 주십시요!!"

                Case mc_iRptCd_Fn
                    sReturn = "은(는) 이미 최종보고된 상태입니다. 확인하여 주십시요!!"

            End Select

            Return sReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Return msFile + sFn + vbCrLf + ex.Message

        End Try
    End Function

    Private Function fnFind_Enable_Reg(ByVal riRstFlg As Integer, ByVal riRegStep As Integer, ByVal riChange As Integer) As Integer
        Dim sFn As String = "fnFind_Enable_Reg"

        Try
            Dim iReturn As Integer = -1

            Select Case riRstFlg
                Case 0
                    '없음
                    Select Case riRegStep
                        Case 1
                            '@없음 --> 결과저장
                            If riChange > 0 Then
                                iReturn = -1
                            Else
                                iReturn = 0
                            End If

                        Case 2, 3
                            '@없음 --> 중간보고 또는 최종보고
                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                iReturn = 0
                            End If

                    End Select

                Case 1
                    '결과저장
                    Select Case riRegStep
                        Case 1
                            '@결과저장 --> 결과저장
                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                iReturn = 0
                            End If

                        Case 2, 3
                            '@결과저장 --> 중간보고 또는 최종보고
                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                'RstFlg만 1 -> 2 또는 3
                                iReturn = -1
                            End If

                    End Select

                Case 2
                    '중간보고
                    Select Case riRegStep
                        Case 1
                            '@중간보고 --> 결과저장
                            iReturn = mc_iRptCd_Mw

                        Case 2, 22
                            '@중간보고 --> 중간보고
                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                iReturn = 0
                            End If

                        Case 3
                            '@중간보고 --> 최종보고
                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                'RstFlg만 2 -> 3
                                iReturn = -1
                            End If

                    End Select

                Case 3
                    '최종보고
                    Select Case riRegStep
                        Case 1
                            '@최종보고 --> 결과저장
                            iReturn = mc_iRptCd_Fn

                        Case 2
                            '@최종보고 --> 중간보고
                            iReturn = mc_iRptCd_Fn

                        Case 3
                            '@최종보고 --> 최종보고
                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgFn) = False Then iReturn = mc_iSklCd_ChgFn
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                iReturn = 0
                            End If

                    End Select
            End Select

            Return iReturn

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return 0
        End Try
    End Function

    Private Function fnGet_Change_Rst(ByVal riRegStep As Integer, ByRef riDisable As Integer, ByRef rsCmtCont As String, Optional ByVal rsCfSign As String = "") As ArrayList
        Dim sFn As String = "fnGet_Change_Rst"

        Dim al As New ArrayList
        Dim ri As STU_RstInfo

        If riRegStep = 22 Then riRegStep = 2

        Try
            '변경여부 조사 --> 변경된 결과를 ArrayList에 담기
            Dim iChange As Integer = 0
            Dim iRstFlg As Integer = 0


            If msSpSubExPrg = "IMG" Then
                Dim sFileNm As String = ""

                sFileNm = System.Windows.Forms.Application.StartupPath + "\image\" + Me.txtBcno.Text.Replace("-", "") + "_" + Me.lblTestCd.Text + ".jpg"

                If IO.File.Exists(sFileNm) Then
                    riDisable = -1
                Else
                    riDisable = 0
                End If

                riDisable = fnFind_Enable_Reg(iRstFlg, riRegStep, 1)
            Else
                '등록가능여부 조사
                If msOrigin_RstRTF.Length = 0 Then
                    iRstFlg = 0

                    If Me.rtbStRst.get_SelRTF(True).Trim.Length > 0 Then
                        iChange += 1
                    End If
                Else
                    '결과저장 --> 중간보고 --> 최종보고 처럼 RegStep 변경여부 판단
                    If Me.lblRstFlg.Text = FixedVariable.gsRstFlagR Then iRstFlg = 1
                    If Me.lblRstFlg.Text = FixedVariable.gsRstFlagM Then iRstFlg = 2
                    If Me.lblRstFlg.Text = FixedVariable.gsRstFlagF Then iRstFlg = 3

                    If iRstFlg > 0 And iRstFlg < riRegStep Then iChange += 1

                    '결과변경여부
                    If Not Me.rtbStRst.get_SelRTF(True).Trim.Equals(msOrigin_RstRTF) Then
                        iChange += 1
                    End If
                End If
                riDisable = fnFind_Enable_Reg(iRstFlg, riRegStep, iChange)

            End If

            'Disable시 iDisable >= 0
            If riDisable >= 0 Then
                msDisableMsg = Me.lblTNm.Text + " ( " + Me.lblTestCd.Text + " ) " + fnFind_Disable_Msg(riDisable)
                Return Nothing
            Else
                If Me.lblRstFlg.Text = FixedVariable.gsRstFlagF And riRegStep = 3 Then rsCmtCont = "최종보고 수정"
            End If

            ri = New STU_RstInfo

            ri.TestCd = Me.lblTestCd.Text

            '일반검사 결과
            Select Case riRegStep
                Case 1
                    ri.OrgRst = "{null}"
                    ri.ChageRst = Me.txtStRstTxtR.Text

                Case 2
                    ri.OrgRst = "{null}"
                    ri.ChageRst = Me.txtStRstTxtM.Text

                Case 3
                    ri.OrgRst = "{null}"
                    ri.ChageRst = Me.txtStRstTxtF.Text
                    ri.CfmSign = rsCfSign
            End Select

            ri.RstCmt = ""
            ri.EqFlag = ""

            '특수검사 결과
            If msSpSubExPrg = "IMG" Then
                ri.RstRTF = ""
                ri.RstTXT = ""
                al.Add(ri)
            Else
                'ri.RstRTF = Me.rtbStRst.get_SelRTF(True).Trim
                ri.RstRTF = Me.rtbStRst.get_SelRTF(True).Trim
                ri.RstTXT = Fn.SubstringH(Me.rtbStRst.get_SelText(True).Trim, 0, 4000)

                mbAddFileGbn = False
                If cboAddFile.Items.Count > 0 Then
                    cboAddFile.SelectedIndex = 0
                    ri.AddFileNm1 = cboAddFile.Text
                End If
                mbAddFileGbn = True

                al.Add(ri)
            End If

            ri = Nothing

            '-- 2008/02/21 YEJ Add(서브항목이 있는 경우도 처리)
            Dim dt As New DataTable
            dt = LISAPP.APP_SP.fnGet_Rst_SpTest_Sub(Me.txtBcno.Text.Replace("-", ""), Me.lblTestCd.Text, riRegStep.ToString)

            If dt.Rows.Count > 0 Then
                For intIx1 As Integer = 0 To dt.Rows.Count - 1

                    If dt.Rows(intIx1).Item("testcd").ToString.Length > 5 Then
                        ri = New STU_RstInfo
                        ri.TestCd = dt.Rows(intIx1).Item("testcd").ToString
                        ri.OrgRst = dt.Rows(intIx1).Item("orgrst").ToString
                        ri.RstCmt = dt.Rows(intIx1).Item("rstcmt").ToString
                        ri.EqFlag = dt.Rows(intIx1).Item("eqflag").ToString

                        al.Add(ri)

                        ri = Nothing
                    End If
                Next
            End If

            Return al

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New ArrayList

        Finally
            al = Nothing

        End Try
    End Function

    Private Sub sbDisplay_BcNo(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_BcNo"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            sbDisplay_BcNo_PatInfo(rsBcNo)
            sbDisplay_BcNo_Rst(rsBcNo, Me.lblTestCd.Text)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            Me.spdList.Focus()
        End Try
    End Sub

    Private Sub sbDisplay_BcNo_PatInfo(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_BcNo_PatInfo"

        Try

            Me.txtBcno.Text = rsBcNo

            AxPatInfo.sbDisplay_Init()
            AxPatInfo.BcNo = rsBcNo
            AxPatInfo.fnDisplay_Data()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_BcNo_Rst(ByVal rsBcNo As String, ByVal rsTestCd As String)
        Dim sFn As String = "sbDisplay_BcNo_Rst"

        Try

            sbDisplayInit_lblRstInfo()
            sbDisplayInit_SpTest()

            '< 2010-03-09 yjlee add 
            Me.rtbStRst.set_BcNo(Me.txtBcno.Text.Replace("-", ""))
            '> 2010-03-09 yjlee add 


            If Me.spTestTF Then sbDisplayInit_btnReg()

            '원본결과RTF 초기화
            msOrigin_RstRTF = ""

            '< jjh 이전결과 체크
            Dim bfChk As DataTable = LISAPP.COMM.RstFn.fnGet_BfRst_Testcd()
            If bfChk.Rows.Count > 0 Then
                Dim bfTestcd As String() = bfChk.Rows(0).Item("clsval").ToString.Split("/"c)

                For i As Integer = 0 To bfTestcd.Count - 1
                    If bfTestcd(i) = rsTestCd Then
                        msBfRst = True
                        Me.btnRstHis.Visible = True
                        Exit For
                    End If
                Next
            End If
            '>

            Dim dt As DataTable
            Dim a_dr As DataRow()

            '-- 특수검사 OCS 전달 REMARK 표시
            Me.txtTestCont.Text = LISAPP.APP_SP.fnGet_Dr_TestCont_Sp(Me.txtBcno.Text.Replace("-", ""), Me.lblTestCd.Text)

            dt = LISAPP.APP_SP.fnGet_Rst_SpTest_MULTI(rsBcNo, rsTestCd)
            'dt = LISAPP.APP_SP.fnGet_Rst_SpTest(rsBcNo, rsTestCd)

            a_dr = dt.Select("rstflg > '0'")

            sbDisplay_BcNo_RstInfo(dt)

            Me.PictureBox1.Dispose()
            Me.PictureBox1.Image = Nothing
            Me.PictureBox1.Refresh()

            Me.PictureBox2.Dispose()
            Me.PictureBox2.Image = Nothing
            Me.PictureBox2.Refresh()

            If a_dr.Length = 0 Then
                '일반검사결과의 상태, 결과에 따른 표시

                msSpSubExPrg = dt.Rows(0).Item("stsubexprg").ToString

                '특수검사 마스터에 대한 내용 표시
                sbDisplay_BcNo_SpTest()

            Else

                '특수검사 결과의 상태, 결과에 따른 표시
                sbDisplay_BcNo_SpRst_RstDtUsr(dt)

                Me.pnlRst.Visible = True

                For i As Integer = 0 To a_dr.Length - 1
                    Me.rtbStRst.set_SelRTF(a_dr(i).Item("rstrtf").ToString)
                Next

                'Me.rtbStRst.set_SelRTF(a_dr(0).Item("rstrtf").ToString)

                msOrigin_RstRTF = Me.rtbStRst.get_SelRTF(True).Trim


                Me.txtStRstTxtR.Text = a_dr(0).Item("strsttxtr").ToString()
                Me.txtStRstTxtM.Text = a_dr(0).Item("strsttxtm").ToString()
                Me.txtStRstTxtF.Text = a_dr(0).Item("strsttxtf").ToString()

            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            'bmp.Dispose()
        End Try
    End Sub

    Private Sub sbDisplay_BcNo_Rst()
        Dim sFn As String = "sbDisplay_BcNo_Rst"

        Try
            Dim sBcNo As String = Me.txtBcno.Text.Replace("-", "")
            Dim sTestCd As String = Me.lblTestCd.Text

            Dim dt As DataTable
            Dim a_dr As DataRow()

            dt = LISAPP.APP_SP.fnGet_Rst_SpTest(sBcNo, sTestCd)

            a_dr = dt.Select("rstflg > '0'")

            If a_dr.Length = 0 Then

            Else
                sbDisplayInit_lblRstInfo()
                sbDisplayInit_SpTest()
                sbDisplayInit_btnReg()

                '원본결과RTF 초기화
                msOrigin_RstRTF = ""

                '특수검사 결과의 상태, 결과에 따른 표시
                sbDisplay_BcNo_SpRst_RstDtUsr(dt)

                Me.pnlRst.Visible = True
                Me.rtbStRst.set_SelRTF(a_dr(0).Item("rstrtf").ToString)


                '원본결과RTF 저장
                msOrigin_RstRTF = Me.rtbStRst.get_SelRTF(True).Trim

                Me.txtStRstTxtR.Text = a_dr(0).Item("strsttxtr").ToString()
                Me.txtStRstTxtM.Text = a_dr(0).Item("strsttxtm").ToString()
                Me.txtStRstTxtF.Text = a_dr(0).Item("strsttxtf").ToString()

            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub


    Protected Overridable Sub sbDisplay_BcNo_RstInfo(ByVal r_dt As DataTable)
        Dim sFn As String = "sbDisplay_BcNo_RstInfo"

        Try
            If r_dt.Rows.Count = 0 Then Return

            Select Case r_dt.Rows(0).Item("rstflg").ToString()
                Case "3"
                    Me.btnReg_m.Enabled = False
                    Me.btnReg_r.Enabled = False

                Case "2"
                    Me.btnReg_m.Enabled = True
                    Me.btnReg_r.Enabled = False

                Case "1"
                    Me.btnReg_m.Enabled = True
                    Me.btnReg_r.Enabled = True

                Case "0"
                    Me.btnReg_m.Enabled = True
                    Me.btnReg_r.Enabled = True

            End Select

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_BcNo_SpRst_RstDtUsr(ByVal r_dt As DataTable)
        Dim sFn As String = "sbDisplay_BcNo_SpRst_RstDtUsr"

        Try
            Dim sID As String = ""
            Dim sNM As String = ""
            Dim sDT As String = ""

            '초기화
            sbDisplayInit_lblRstInfo()

            Dim a_dr As DataRow()

            a_dr = r_dt.Select("rstflg >= '1'", "regdt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("regid").ToString()
                sNM = a_dr(i - 1).Item("regnm").ToString()
                sDT = a_dr(i - 1).Item("regdt").ToString()

                If Not sID + sNM + sDT = "" Then
                    Me.lblRstFlg.Text = FixedVariable.gsRstFlagR
                    Me.lblRstDtUsr.Text = sDT + " " + sNM

                    Exit For
                End If
            Next

            a_dr = r_dt.Select("rstflg >= '2'", "mwdt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("mwid").ToString()
                sNM = a_dr(i - 1).Item("mwnm").ToString()
                sDT = a_dr(i - 1).Item("mwdt").ToString()

                If Not sID + sNM + sDT = "" Then
                    Me.lblRstFlg.Text = FixedVariable.gsRstFlagM
                    Me.lblRstDtUsr.Text = sDT + " " + sNM

                    Exit For
                End If
            Next

            a_dr = r_dt.Select("rstflg = '3'", "fndt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("fnid").ToString()
                sNM = a_dr(i - 1).Item("fnnm").ToString()
                sDT = a_dr(i - 1).Item("fndt").ToString()

                If Not sID + sNM + sDT = "" Then
                    Me.lblRstFlg.Text = FixedVariable.gsRstFlagF
                    Me.lblRstDtUsr.Text = sDT + " " + sNM

                    Exit For
                End If
            Next

            If Me.lblRstFlg.Text = FixedVariable.gsRstFlagF Then
                Me.lblRstFlg.ForeColor = FixedVariable.g_color_FN
            Else
                Me.lblRstFlg.ForeColor = Drawing.Color.Black
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_BcNo_OcsRemark_Exam()

    End Sub

    Private Sub sbDisplay_BcNo_SpTest()
        Dim sFn As String = "sbDisplay_BcNo_SpTest"

        Try

            'TabControl의 SelectedIndexChanged 이벤트 Skip 하도록 설정(TabPages 제거시, SelectedTab 변화시 ...)
            piSkip = 1

            Dim dt As DataTable = (New LISAPP.APP_F_SPTEST).GetSpTestInfo(Me.lblTestCd.Text)

            m_dt_SpTest = dt.Copy()

            'm_al_StSub 초기화
            m_al_StSub.Clear()

            For i As Integer = 1 To m_dt_SpTest.Rows.Count
                Dim si As New StSubInfo

                m_al_StSub.Add(si)

                si = Nothing
            Next

            m_al_StSub.TrimToSize()

            Me.tbcStSubSeq.TabPages.Clear()

            With m_dt_SpTest
                For i As Integer = 1 To .Rows.Count
                    Me.tbcStSubSeq.TabPages.Add(New Windows.Forms.TabPage(i.ToString()))
                Next

                For i As Integer = 1 To .Rows.Count
                    Dim si As New StSubInfo

                    si.Name = .Rows(i - 1).Item("stsubnm").ToString()
                    si.Type = .Rows(i - 1).Item("stsubtype").ToString()
                    si.ImgType = .Rows(i - 1).Item("imgtype").ToString()
                    si.ImgSizeW = .Rows(i - 1).Item("imgsizew").ToString()
                    si.ImgSizeH = .Rows(i - 1).Item("imgsizeh").ToString()
                    si.RTF = .Rows(i - 1).Item("stsubrtf").ToString()
                    si.ExPrg = .Rows(i - 1).Item("stsubexprg").ToString()

                    m_al_StSub(i - 1) = si

                    Me.tbcStSubSeq.TabPages(i - 1).Text = si.Name

                    si = Nothing
                Next

                Me.txtStRstTxtR.Text = .Rows(0).Item("strsttxtr").ToString()
                Me.txtStRstTxtM.Text = .Rows(0).Item("strsttxtm").ToString()
                Me.txtStRstTxtF.Text = .Rows(0).Item("strsttxtf").ToString()

                '초기화
                miStSubSeq = 0

                If .Rows(0).Item("stsubfirst").ToString() = "" Then
                    Me.tbcStSubSeq.SelectedIndex = 1 - 1
                    sbStSub_Set(1)
                Else
                    Me.tbcStSubSeq.SelectedIndex = Convert.ToInt32(.Rows(0).Item("stsubfirst").ToString()) - 1
                    sbStSub_Set(Convert.ToInt32(.Rows(0).Item("stsubfirst").ToString()))
                End If

                '버튼(검사결과 전체보기) 보이기/숨기기
                If .Rows(0).Item("stsubcnt").ToString() = "1" Then
                    Me.btnRstAll.Visible = False
                Else
                    Me.btnRstAll.Visible = True
                End If
            End With


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            piSkip = 0

        End Try
    End Sub

    Public Sub sbDisplay_Clear()
        Dim sFn As String = "sbDisplay_Clear"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            piProcessing = 1

            Me.txtTestCont.Text = ""

            Me.nCovRst.Text = "" 'JJH 코로나 바이러스 결과값

            sbDisplayInit_grpPatInfo()

            sbDisplayInit_lblTestInfo()

            sbDisplayInit_lblRstInfo()

            sbDisplayInit_SpTest()

            sbDisplayInit_btnReg()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            piProcessing = 0

            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_Reset_Rst()
        Dim sFn As String = "sbDisplay_Reset_Rst"

        Try
            If MsgBox("결과를 특수검사 설정에 따라 초기화합니다. 계속하시겠습니까?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return

            sbDisplayInit_SpTest()

            sbDisplayInit_btnReg()

            '원본결과RTF 초기화
            msOrigin_RstRTF = ""

            Dim dt As DataTable

            dt = LISAPP.APP_SP.fnGet_Rst_SpTest(Me.txtBcno.Text.Replace("-", ""), Me.lblTestCd.Text)

            '일반검사결과의 상태, 결과에 따른 표시
            sbDisplay_BcNo_RstInfo(dt)

            '< jjh 이전결과 체크
            Dim bfChk As DataTable = LISAPP.COMM.RstFn.fnGet_BfRst_Testcd()
            If bfChk.Rows.Count > 0 Then
                Dim bfTestcd As String() = bfChk.Rows(0).Item("clsval").ToString.Split("/"c)

                For i As Integer = 0 To bfTestcd.Count - 1
                    If bfTestcd(i) = Me.lblTestCd.Text Then
                        msBfRst = True
                    End If
                Next
            End If
            '>

            '특수검사 마스터에 대한 내용 표시
            sbDisplay_BcNo_SpTest()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Protected Overridable Sub sbDisplay_Search(ByVal rsOpt As String)
        Dim sFn As String = "sbDisplay_Search"

        Dim dt As DataTable

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim sSlipCd As String = Ctrl.Get_Code(Me.cboSlip)
            Dim sTestCds As String = ""

            With Me.spdSpTest
                For i As Integer = 1 To .MaxRows
                    Dim sChk As String = Ctrl.Get_Code(Me.spdSpTest, "chk", i)
                    Dim sTCd As String = Ctrl.Get_Code(Me.spdSpTest, "testcd", i)

                    If sChk = "1" Then
                        If sTestCds.Length = 0 Then
                            sTestCds += sTCd
                        Else
                            sTestCds += "," + sTCd
                        End If
                    End If
                Next
            End With

            If sTestCds.Length = 0 Then
                MsgBox("선택한 검사코드가 없습니다. 확인하여 주십시요!!")
                Return
            End If

            sTestCds = "'" + sTestCds.Replace(",", "','") + "'"

            If PRG_CONST.SLIP_ExLab.Contains(Ctrl.Get_Code(cboSlip)) And Me.chkFilter.Checked = True Then
                dt = LISAPP.APP_SP.fnGet_SpcList_Sp_RstDt(sSlipCd, Me.dtpTkS.Value.ToShortDateString.Replace("-", ""), Me.dtpTkE.Value.ToShortDateString.Replace("-", ""), rsOpt, sTestCds)
            Else
                dt = LISAPP.APP_SP.fnGet_SpcList_Sp_Tk(sSlipCd, Me.dtpTkS.Value.ToShortDateString.Replace("-", ""), Me.dtpTkE.Value.ToShortDateString.Replace("-", ""), rsOpt, sTestCds, CInt(lblDate.Tag))
            End If


            '접수일시에 정렬 표시
            Me.spdList.set_ColUserSortIndicator(Me.spdList.GetColFromID("tkdt"), FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)
            sbDisplay_Data(dt)

            'Ctrl.DisplayAfterSelect(Me.spdList, dt)

            Me.spdList.SetActiveCell(0, 0)

            'sbDisplay_Search_Color(rsOpt)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_Data(ByVal r_dt As DataTable)

        Me.spdList.MaxRows = 0
        Me.spdList.MaxRows = r_dt.Rows.Count
        If r_dt.Rows.Count < 1 Then Return

        With Me.spdList
            For ix As Integer = 0 To r_dt.Rows.Count - 1
                .Row = ix + 1
                .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString
                .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString
                .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString
                .Col = .GetColFromID("workno") : .Text = r_dt.Rows(ix).Item("workno").ToString
                .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix).Item("testcd").ToString
                .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix).Item("tnmd").ToString
                .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString
                .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString
                .Col = .GetColFromID("rstflg") : .Text = r_dt.Rows(ix).Item("rstflg").ToString
                .Col = .GetColFromID("partslip") : .Text = r_dt.Rows(ix).Item("partslip").ToString

                '미완 --> BackColor 변경
                If r_dt.Rows(ix).Item("rstflg").ToString <> "N" Then
                    .Col = -1
                    .BackColor = Ctrl.color_LightRed
                End If
            Next
        End With
    End Sub

    Private Sub Label14_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblDate.DoubleClick
        Dim sFn As String = ""

        Try
            With lblDate
                If .Tag.ToString = "0" Then
                    .Text = "2차접수일자"
                    .Tag = 1
                    .BackColor = Color.SteelBlue
                Else
                    .Text = "접수일자"
                    .Tag = 0
                    .BackColor = Color.Navy
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Protected Overridable Sub sbDisplay_Search_Color(ByVal rsOpt As String)
        Dim sFn As String = "sbDisplay_Search_Color"

        '전체인 경우에만 완/미완 색상
        If Not rsOpt.Substring(0, 1) = "A" Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With spd
                .ReDraw = False

                For i As Integer = 1 To .MaxRows
                    Dim sFlag As String = Ctrl.Get_Code(spd, "rstflg", i)

                    '미완 --> BackColor 변경
                    If sFlag <> "Y" Then
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = i : .Row2 = i
                        .BlockMode = True : .BackColor = Ctrl.color_LightRed : .BlockMode = False
                    End If
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplay_SpTest()
        Dim sFn As String = "sbDisplay_SpTest"

        Try
            Dim dt As New DataTable
            Dim sSlipCd As String = Ctrl.Get_Code(Me.cboSlip)

            'piUseMode = 0 --> 일반, piUseMode = 1 --> psCd_Include_Exclude만 포함, piUseMode = 2 --> psCd_Include_Exclude를 제외
            Select Case piUseMode
                Case 0, 1
                    dt = LISAPP.APP_SP.fnGet_TestList_sp(sSlipCd, Me.dtpTkS.Text.Replace("-", ""), Me.dtpTkE.Text.Replace("-", ""))

                Case 2
                    dt = LISAPP.APP_SP.fnGet_TestList_sp(sSlipCd, Me.dtpTkS.Text.Replace("-", ""), Me.dtpTkE.Text.Replace("-", ""), _
                                                           piUseMode, psCd_Include_Exclude)
                Case 2

            End Select

            Ctrl.DisplayAfterSelect(Me.spdSpTest, dt, True)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Slip()
        Dim sFn As String = "sbDisplay_Slip"

        Try
            Dim dt As DataTable

            Dim sPartSlip_Pre As String = Ctrl.Get_Item(Me.cboSlip)

            Me.cboSlip.Items.Clear()

            If msUse_PartCd = "" Then
                Me.cboSlip.Items.Add("[  ] 전체")
                dt = LISAPP.COMM.CdFn.fnGet_Slip_List(Me.dtpTkS.Value.ToShortDateString.Replace("-", ""), True)
            Else
                dt = LISAPP.COMM.CdFn.fnGet_Slip_List(Me.dtpTkS.Value.ToShortDateString.Replace("-", ""), , , , True)
            End If


            For i As Integer = 1 To dt.Rows.Count
                Me.cboSlip.Items.Add("[" + dt.Rows(i - 1).Item("slipcd").ToString() + "] " + dt.Rows(i - 1).Item("slipnmd").ToString())
            Next

            If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0

            If sPartSlip_Pre = "" Then Return

            '이전검사계명(sTSectNm_Pre)과 일치하는 Index 선택
            If Me.cboSlip.Items.Count > 0 Then
                For i As Integer = 1 To Me.cboSlip.Items.Count
                    If Me.cboSlip.Items.Item(i - 1).ToString() = sPartSlip_Pre Then
                        Me.cboSlip.SelectedIndex = i - 1

                        Exit For
                    Else
                        Me.cboSlip.SelectedIndex = -1
                    End If
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_txtNo_KeyDown()
        Dim sFn As String = "sbDisplay_txtNo_KeyDown"

        Dim dt As DataTable

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim sSlipCd As String = Ctrl.Get_Code(Me.cboSlip)

            If Me.lblSearch.Text = "검체번호" Then
                Dim sBcNo As String = Me.txtNo.Text.Trim.Replace("-", "")

                If sBcNo = "" Then
                    MsgBox("검체번호를 입력해 주십시요!!")
                    Return
                End If

                '검체번호               : 14 Or 15
                '검체번호바코드(일반)   : 11 Or 12
                '작업번호바코드(미생물) : 10
                If sBcNo.Length = 14 Then sBcNo = sBcNo + "0"
                If sBcNo.Length = 12 Then sBcNo = sBcNo.Substring(0, 11)

                If sBcNo.Length = 11 Or sBcNo.Length = 10 Then
                    sBcNo = LISAPP.COMM.BcnoFn.fnFind_BcNo(sBcNo)
                End If

                If Not sBcNo.Length = 15 Then
                    MsgBox("검체번호에 오류가 발견되었습니다. 확인하여 주십시요!!")

                    Me.txtNo.SelectAll()

                    Return
                End If

                dt = LISAPP.APP_SP.fnGet_SpcList_Sp_bcno(sBcNo)

            Else
                '등록번호 입력시 처리
                Dim sRegNo As String = Me.txtNo.Text.Trim()

                If sRegNo = "" Then
                    MsgBox("등록번호를 입력해 주십시요!!")
                    Return
                End If

                If IsNumeric(sRegNo.Substring(0, 1)) Then
                    sRegNo = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                Else
                    sRegNo = sRegNo.Substring(0, 1).ToUpper + sRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If

                Dim sSectCd As String = ""
                Dim sTSectCd As String = ""

                If Me.chkFilter.Checked Then
                    If sSlipCd.Length < 2 Then
                        MsgBox("검사분야 코드가 없습니다. 확인하여 주십시요!!")
                        Return
                    End If

                    Dim sTestCds As String = ""

                    With Me.spdSpTest
                        For i As Integer = 1 To .MaxRows
                            Dim sChk As String = Ctrl.Get_Code(Me.spdSpTest, "chk", i)
                            Dim sTCd As String = Ctrl.Get_Code(Me.spdSpTest, "testcd", i)

                            If sChk = "1" Then
                                If sTestCds.Length = 0 Then
                                    sTestCds += sTCd
                                Else
                                    sTestCds += "," + sTCd
                                End If
                            End If
                        Next
                    End With

                    If sTestCds.Length = 0 Then
                        MsgBox("선택한 검사코드가 없습니다. 확인하여 주십시요!!")
                        Return
                    End If

                    sTestCds = "'" + sTestCds.Replace(",", "','") + "'"

                    dt = LISAPP.APP_SP.fnGet_SpcList_Sp_Regno(sRegNo, sSlipCd, Me.dtpTkS.Value.ToShortDateString.Replace("-", ""), Me.dtpTkE.Value.ToShortDateString.Replace("-", ""), sTestCds)
                Else
                    dt = LISAPP.APP_SP.fnGet_SpcList_Sp_Regno(sRegNo, "", "", "", "")
                End If
            End If

            '접수일시에 정렬 표시
            Me.spdList.set_ColUserSortIndicator(Me.spdList.GetColFromID("tkdt"), FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)

            Ctrl.DisplayAfterSelect(Me.spdList, dt)

            Me.spdList.SetActiveCell(0, 0)

            sbDisplay_Search_Color("A")

            '조회 후 화면 처리
            If Me.spdList.MaxRows = 0 Then
                MsgBox("해당하는 내역이 없습니다!!")
                Me.txtNo.SelectAll()
            Else
                Me.txtNo.Text = ""

                If Me.spdList.MaxRows = 1 Then
                    Me.spdList_ClickEvent(Me.spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1))
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Protected Overridable Sub sbCheckColHidden(ByVal checkTF As Boolean)
        Dim sFn As String = "sbCheckColHidden"

        Try

            Me.spdList.Col = Me.spdList.GetColFromID("check")
            Me.spdList.ColHidden = checkTF

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try

            If spTestTF Then
                btnCancel.Visible = False
                btnReg_FnAll.Visible = False

                '-- 2008/03/03 YEJ add(완료버튼 추가:폐기능에서 Popup IMG인 경우 사용)
                btnReg_FnBcno.Visible = True
                'Me.spdList.Col = Me.spdList.GetColFromID("check")
                'Me.spdList.ColHidden = True
            Else
                '-- 2008/03/03 YEJ add(완료버튼 추가:폐기능에서 Popup IMG인 경우 사용)
                btnReg_FnBcno.Visible = False
            End If

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            piProcessing = 1

            sbDisplayInit_grpPatInfo()

            sbDisplayInit_grpSearchOpt()

            sbDisplayInit_spdSpTest()

            sbDisplayInit_spdList()

            sbDisplayInit_lblTestInfo()

            sbDisplayInit_lblRstInfo()

            sbDisplayInit_SpTest()

            sbDisplay_Slip()

            sbDisplay_SpTest()

            'If (STU_AUTHORITY.UsrID = "EMR" Or STU_AUTHORITY.UsrID = "ACK") Then
            '    Me.btnRstHis.Visible = True
            'Else
            '    Me.btnRstHis.Visible = False
            'End If

            Me.btnRstHis.Visible = False


#If DEBUG Then
            Me.txtStRstTxtF.Visible = True
            Me.txtStRstTxtM.Visible = True
            Me.txtStRstTxtR.Visible = True
#Else
            Me.txtStRstTxtF.Visible = False
            Me.txtStRstTxtM.Visible = False
            Me.txtStRstTxtR.Visible = False
#End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            piProcessing = 0

            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplayInit_btnReg()
        Dim sFn As String = "sbDisplayInit_btnReg"

        Try
            Me.btnReg_r.Enabled = True
            Me.btnReg_m.Enabled = True
            Me.btnReg_F.Enabled = True

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_grpPatInfo()
        Dim sFn As String = "sbDisplayInit_grpPatInfo"

        Try
            AxPatInfo.sbDisplay_Init()
            Me.txtBcno.Text = ""
            Me.txtPartSlip.Text = ""

            m_tooltip.RemoveAll()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_grpSearchOpt()
        Dim sFn As String = "sbDisplayInit_grpSearchOpt"

        Try
            Me.txtNo.Text = ""
            Me.chkFilter.Enabled = False

            Dim dtNow As Date = New LISAPP.APP_DB.ServerDateTime().GetDateTime

            Me.dtpTkE.Value = dtNow
            Me.dtpTkS.Value = dtNow

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Public Sub sbDisplayInit_JH()
        Dim sFn As String = "sbDisplayInit_JH"

        Try
            Me.spdList.MaxRows = 0
            sbDisplayInit_grpPatInfo()
            sbDisplayInit_lblTestInfo()
            sbDisplayInit_lblRstInfo()
            sbDisplayInit_SpTest()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_lblRstInfo()
        Dim sFn As String = "sbDisplayInit_lblRstInfo"

        Try
            '결과상태, RstDt+RstUsr
            Me.lblRstFlg.Text = ""
            Me.lblRstDtUsr.Text = ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub sbDisplayInit_lblTestInfo()
        '검사코드, 검사명
        Me.lblTestCd.Text = ""
        Me.lblTNm.Text = ""
        Me.nCovRst.Text = ""

        msSpSubExPrg = ""
    End Sub

    Private Sub sbDisplayInit_spdList()
        Dim sFn As String = "sbDisplayInit_spdList"
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            spd.ReDraw = False

            With spd
                .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort

                For i As Integer = 1 To .MaxCols
                    .set_ColUserSortIndicator(i, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorNone)
                Next

                .MaxRows = 0

                .Col = .GetColFromID("check") : .ColHidden = True
                .Col = .GetColFromID("rstflg") : .ColHidden = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplayInit_spdSpTest()
        Dim sFn As String = "sbDisplayInit_spdSpTest"
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdSpTest

        Try
            spd.ReDraw = False

            With spd
                .MaxRows = 0
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplayInit_SpTest()
        Dim sFn As String = "sbDisplayInit_SpTest"

        Try
            'TabPages 제거시 SelectedIndexChanged 이벤트 발생됨
            piSkip = 1
            Me.tbcStSubSeq.TabPages.Clear()
            piSkip = 0

            sbDisplayInit_SpTest_panel()

            Me.btnRstAll.Visible = False
            Me.btnRstHis.Visible = False

            Me.txtStRstTxtR.Text = ""
            Me.txtStRstTxtM.Text = ""
            Me.txtStRstTxtF.Text = ""

            Me.cboAddFile.Items.Clear()
            mbAddFileGbn = True

            msBfRst = False
            Me.btnRstHis.Visible = False
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisplayInit_SpTest_panel()
        Dim sFn As String = "sbDisplayInit_SpTest_panel"

        Try
            'Me.pnlSpTest, Me.pnlIMG, Me.pnlRst 초기화
            Me.pnlRTF.Location = New Drawing.Point(-2, 52)
            Me.pnlIMG.Location = Me.pnlRTF.Location
            Me.pnlRst.Location = Me.pnlRTF.Location

            Me.pnlRTF.Size = New Drawing.Size(734, Me.pnlRTF.Size.Height)
            Me.pnlIMG.Size = Me.pnlRTF.Size
            Me.pnlRst.Size = Me.pnlRTF.Size

            Me.pnlRTF.Visible = False
            Me.pnlIMG.Visible = False
            Me.pnlRst.Visible = False

            Me.rtbSt.set_SelRTF("", True)

            Me.picBuf.Image = Nothing
            Me.rtbImg.set_SelRTF("", True)
            Me.rtbImg.set_Lock(True)

            Me.rtbStRst.set_SelRTF("", True)

            '검사결과 전체보기 상태 해제
            Dim sText As String = Me.btnRstAll.Text

            If sText.IndexOf("전체") < 0 Then
                Me.tbcStSubSeq.Visible = True

                Me.pnlRst.Visible = False
                Me.pnlRst.SendToBack()

                Me.btnRstAll.Text = Me.btnRstAll.AccessibleName
                Me.btnRstAll.AccessibleName = sText
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbImg_Open()
        Dim sFn As String = "sbImg_Open"

        Try
            '임시그림파일 삭제
            If IO.File.Exists(Windows.Forms.Application.StartupPath + "\tmpfile.jpg") Then
                IO.File.Delete(Windows.Forms.Application.StartupPath + "\tmpfile.jpg")
            End If

            Dim filedlg As New Windows.Forms.OpenFileDialog

            filedlg.Multiselect = False
            filedlg.Title = "그림 파일 불러오기"
            filedlg.Filter = "그림파일(*.bmp;*jpg;*.gif)|*.bmp;*.jpg;*.gif|모든파일(*.*)|*.*"

            If filedlg.ShowDialog() = DialogResult.OK Then
                Dim bmp As Drawing.Bitmap = New Drawing.Bitmap(filedlg.FileName)

                Select Case CType(m_al_StSub(Me.tbcStSubSeq.SelectedIndex), StSubInfo).ImgType
                    Case "0"
                        '자동
                        Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.AutoSize

                    Case "1"
                        '고정
                        Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.StretchImage

                        Me.picBuf.Width = Convert.ToInt32(CType(m_al_StSub(Me.tbcStSubSeq.SelectedIndex), StSubInfo).ImgSizeW)
                        Me.picBuf.Height = Convert.ToInt32(CType(m_al_StSub(Me.tbcStSubSeq.SelectedIndex), StSubInfo).ImgSizeH)



                End Select

                Me.picBuf.Image = bmp

                Me.picBuf.Refresh()
                Me.picBuf.Image.Save(Windows.Forms.Application.StartupPath + "\image\" + Me.txtBcno.Text.Replace("-", "") + "_" + lblTestCd.Text + ".jpg", Drawing.Imaging.ImageFormat.Jpeg)
                '현Image --> 그림소스로 저장
                Dim imgTot As Drawing.Image = Me.picBuf.Image

                Select Case CType(m_al_StSub(Me.tbcStSubSeq.SelectedIndex), StSubInfo).ImgType
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
                        Dim iAreaW As Integer = Convert.ToInt32(CType(m_al_StSub(Me.tbcStSubSeq.SelectedIndex), StSubInfo).ImgSizeW)
                        Dim iAreaH As Integer = Convert.ToInt32(CType(m_al_StSub(Me.tbcStSubSeq.SelectedIndex), StSubInfo).ImgSizeH)

                        Dim bmpArea As Drawing.Bitmap = New Drawing.Bitmap(iAreaW, iAreaH)

                        Me.picBuf.Image = bmpArea

                        Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(Me.picBuf.Image)

                        Me.picBuf.Width = iAreaW
                        Me.picBuf.Height = iAreaH

                        g.DrawImage(imgTot, -iAreaX, -iAreaY, iTotalW, iTotalH)

                        g.Dispose()

                        Me.picBuf.Image.Save(Windows.Forms.Application.StartupPath + "\tmpfile.jpg", Drawing.Imaging.ImageFormat.Jpeg)

                End Select

                Me.picBuf.Image.Dispose()
                Me.picBuf.Image = Nothing

                Me.rtbImg.set_Lock(False)
                Me.rtbImg.set_SelRTF("", True)
                Me.rtbImg.set_Image(Windows.Forms.Application.StartupPath + "\tmpfile.jpg", False)
                Me.rtbImg.set_Lock(True)


                Dim stream As New FileStream(Windows.Forms.Application.StartupPath + "\image\" & Me.txtBcno.Text.Replace("-", "") & "_" & Me.lblTestCd.Text & ".jpg", FileMode.Open)
                PictureBox1.Image = New System.Drawing.Bitmap(stream)
                stream.Close()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbImg_Remove()
        Dim sFn As String = "sbImg_Remove"

        Try
            Me.rtbImg.set_SelRTF("", True)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbReg_Rst(ByVal riRegStep As Integer, Optional ByVal rsCfnSign As String = "")
        Dim sFn As String = "sbReg_Rst"
        Dim sBcno As String = Me.txtBcno.Text.Trim
        Dim sTestCd As String = Me.lblTestCd.Text.Trim
        Dim bOK As Boolean = False

        Try
            If Me.txtBcno.Text.Trim.Length = 0 Then Return
            If Me.lblTestCd.Text.Trim.Length = 0 Then Return

            '전체보기 후 등록함
            If msSpSubExPrg = "IMG" Then

            Else
                sbStSub_View(True, riRegStep)
            End If

            Dim iDisable As Integer = 0
            Dim sCmt_Final As String = ""

            Dim al_ChgRst As ArrayList = fnGet_Change_Rst(riRegStep, iDisable, sCmt_Final)

            '오류 발생 시
            If al_ChgRst Is Nothing Then
                MsgBox(msDisableMsg)
                Return
            End If

            Dim sMsg As String = Me.lblTNm.Text + " ( " + Me.lblTestCd.Text + " ) : "

            Select Case riRegStep.ToString.Substring(0, 1)
                Case "1"
                    sMsg += "결과저장 하시겠습니까?"

                Case "2"
                    sMsg += "중간보고 하시겠습니까?"

                Case "3"
                    sMsg += "최종보고 하시겠습니까?"

            End Select

            If MsgBox(sMsg, MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return
            End If

            'If sCmt_Final <> "" Then
            '    Dim frm As New AxAckResult.FGFINAL_CMT

            '    frm.msBcNo = Me.txtBcno.Text.Replace("-", "")
            '    frm.msPartSlip = Me.txtPartSlip.Text
            '    frm.msCmt = Me.lblTNm.Text + "{" + "특수결과" + "/" + "특수결과" + "}|"

            '    Dim sRet As String = frm.Display_Result()

            '    If sRet <> "OK" Then Return
            'End If


            Dim si As New STU_SampleInfo

            si.RegStep = riRegStep.ToString()
            si.BCNo = Me.txtBcno.Text.Replace("-", "")
            si.EqCd = ""
            si.UsrID = USER_INFO.USRID
            si.UsrIP = USER_INFO.LOCALIP
            si.IntSeqNo = ""
            si.Rack = ""
            si.Pos = ""
            si.EqBCNo = ""
            si.SenderID = Me.Name
            si.BfRst = Me.nCovRst.Text 'jjh 결과값

            Dim al_ri As New ArrayList
            Dim al_return As New ArrayList

            For i As Integer = 1 To al_ChgRst.Count
                al_ri.Add(al_ChgRst(i - 1))
            Next

            Dim iReturn As Integer
            If LOGIN.PRG_CONST.BCCLS_MicorBio.Contains(Me.txtBcno.Text.Replace("-", "").Substring(8, 2)) Then
                Dim regrst As New LISAPP.APP_M.RegFn

                iReturn = regrst.RegServer(al_ri, si, al_return, True)
            Else
                Dim regrst As New LISAPP.APP_R.RegFn

                iReturn = regrst.RegServer(al_ri, si, al_return, True, msBfRst)

            End If

            If iReturn > 0 Then

                '<< 이미지 일괄등록으로 뺌 (진검 요청)
                'If riRegStep = 3 And (New LISAPP.APP_R.RstFn).fnGet_CSM_TEST_YES(Me.txtBcno.Text, Me.lblTestCd.Text) Then
                '    If fnSaveImage() Then
                '        sbDisplay_Clear()
                '        bOK = True
                '    End If
                'Else
                '    sbDisplay_Clear()
                '    bOK = True
                'End If

                sbDisplay_Clear()
                bOK = True

            Else
                MsgBox(sMsg.Replace("하시겠습니까?", "중 오류가 발생하였습니다!!"), MsgBoxStyle.Critical)
            End If

            si = Nothing

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            If msBcNo <> "" And msTestCd <> "" And bOK Then Me.Close()
        End Try
    End Sub

    Private Sub sbStSub_Get(ByVal aiSeq As Integer)
        If aiSeq < 1 Then Return

        '0 : 일반, 1 : 텍스트만, 2 : 이미지만
        Select Case CType(m_al_StSub(aiSeq - 1), StSubInfo).Type
            Case "1"
                CType(m_al_StSub(aiSeq - 1), StSubInfo).RTF = Me.rtbSt.get_SelRTF(True).Trim

            Case "2"
                CType(m_al_StSub(aiSeq - 1), StSubInfo).RTF = Me.rtbImg.get_SelRTF(True).Trim

            Case Else
                CType(m_al_StSub(aiSeq - 1), StSubInfo).RTF = Me.rtbSt.get_SelRTF(True).Trim

        End Select
    End Sub

    Private Sub sbStSub_Set(ByVal aiSeq As Integer)
        If aiSeq < 1 Then Return

        sbDisplayInit_SpTest_panel()

        '0 : 일반, 1 : 텍스트만, 2 : 이미지만
        Select Case CType(m_al_StSub(aiSeq - 1), StSubInfo).Type
            Case "1"
                Me.pnlRTF.Visible = True
                Me.pnlIMG.Visible = False
                Me.pnlRst.Visible = False

                Me.rtbSt.set_SelRTF(CType(m_al_StSub(aiSeq - 1), StSubInfo).RTF, True)

                Me.lblModeRTF.Text = "텍스트만"

                'DB Field 보이기
                sbStSub_View_DbField(False, 0)

            Case "2"
                Me.pnlRTF.Visible = False
                Me.pnlIMG.Visible = True
                Me.pnlRst.Visible = False

                If CType(m_al_StSub(aiSeq - 1), StSubInfo).ImgType = "0" Then
                    Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.AutoSize
                Else
                    Me.picBuf.SizeMode = Windows.Forms.PictureBoxSizeMode.StretchImage
                End If

            Case Else
                Me.pnlRTF.Visible = True
                Me.pnlIMG.Visible = False
                Me.pnlRst.Visible = False

                Me.rtbSt.set_SelRTF(CType(m_al_StSub(aiSeq - 1), StSubInfo).RTF, True)

                Me.lblModeRTF.Text = "일반"

                'DB Field 보이기
                sbStSub_View_DbField(False, 0)

        End Select

        If CType(m_al_StSub(aiSeq - 1), StSubInfo).ExPrg.Trim.Length > 0 Then
            If CType(m_al_StSub(aiSeq - 1), StSubInfo).UsrCfm = "Y" Then
                If MsgBox("이미 저장된 상태입니다. 연동 프로그램을 실행하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    '외부프로그램 호출
                    sbStSub_Set_ExPrg(aiSeq, CType(m_al_StSub(aiSeq - 1), StSubInfo).ExPrg.Trim)
                End If
            Else
                '외부프로그램 호출
                sbStSub_Set_ExPrg(aiSeq, CType(m_al_StSub(aiSeq - 1), StSubInfo).ExPrg.Trim)
            End If

            If CType(m_al_StSub(aiSeq - 1), StSubInfo).ExPrg = "TYPE2" Or CType(m_al_StSub(aiSeq - 1), StSubInfo).ExPrg = "TYPE3" Then
                sbDisplay_BcNo_Rst()
            End If
            sbStSub_View_DbField(False, 0)
        End If

        '이전 StSubSeq 할당
        miStSubSeq = aiSeq
    End Sub

    Private Sub sbStSub_Set_ExPrg(ByVal aiSeq As Integer, ByVal rsExPrg As String)
        Dim sFn As String = "sbStSub_Set_ExPrg"

        If rsExPrg.Trim.Length = 0 Then Return

        Dim invas_buf As New InvAs

        Try
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\POPUPWIN.dll", "POPUPWIN.FGPOPUPST_" + rsExPrg)

                .SetProperty("UserID", USER_INFO.USRID)

                Dim a_objParam() As Object
                ReDim a_objParam(4)

                a_objParam(0) = Me
                a_objParam(1) = LISAPP.APP_DB.DbFn.fnGet_DbConnect()

                a_objParam(2) = Me.txtBcno.Text.Replace("-", "")
                a_objParam(3) = Me.lblTestCd.Text
                a_objParam(4) = Me.lblTNm.Text

                Dim al_return As ArrayList = CType(.InvokeMember("Display_Result", a_objParam), ArrayList)

                If al_return Is Nothing Then Return
                If al_return.Count < 1 Then Return

                Dim rtb As AxAckRichTextBox.AxAckRichTextBox

                '0 : 일반, 1 : 텍스트만, 2 : 이미지만
                Select Case CType(m_al_StSub(aiSeq - 1), StSubInfo).Type
                    Case "1"
                        rtb = Me.rtbSt

                    Case "2"
                        rtb = Me.rtbImg

                        rtb.set_Lock(False)

                    Case Else
                        rtb = Me.rtbSt

                End Select

                If Not CType(.GetProperty("Append"), Boolean) Then
                    '초기화
                    rtb.set_SelRTF("", True)
                End If

                For i As Integer = 1 To al_return.Count

                    '<JJH

                    Dim objData As Object = Nothing
                    Dim objData2 As Object = Nothing
                    Dim iAlign As Integer = Nothing

                    If msBfRst Then

                        objData = CType(al_return(i - 1), STU_StDataInfo_NCOV).Data
                        objData2 = CType(al_return(i - 1), STU_StDataInfo_NCOV).Data2
                        iAlign = CType(al_return(i - 1), STU_StDataInfo_NCOV).Alignment
                        Me.nCovRst.Text = CType(al_return(i - 1), STU_StDataInfo_NCOV).sResult '판정값

                    Else

                        objData = CType(al_return(i - 1), STU_StDataInfo).Data
                        objData2 = CType(al_return(i - 1), STU_StDataInfo).Data2
                        iAlign = CType(al_return(i - 1), STU_StDataInfo).Alignment

                    End If
                    '>
                    'Dim objData As Object = CType(al_return(i - 1), STU_StDataInfo).Data
                    'Dim objData2 As Object = CType(al_return(i - 1), STU_StDataInfo).Data2
                    'Dim iAlign As Integer = CType(al_return(i - 1), STU_StDataInfo).Alignment


                    If rsExPrg = "VCMT" Then
                        rtb.set_SelRTF("", True)

                    End If

                    Select Case objData.GetType.Name.ToLower()
                        Case "bitmap"
                            rtb.set_Image(objData.ToString, iAlign)

                        Case "string"
                            If objData.ToString.Trim.StartsWith("{\rtf") And objData.ToString.Trim.EndsWith("}") Then
                                rtb.set_SelRTF(objData.ToString.Trim, True)
                                'rtb.set_SelRTF(objData.ToString.Trim.Substring(1, objData.ToString.Trim.Length - 2), False)
                            Else
                                If rsExPrg = "VCMT" Then
                                    rtb.set_SelText(objData.ToString)

                                Else
                                    rtb.set_SelText(objData.ToString, iAlign)
                                End If

                            End If

                            rtb.Font = New System.Drawing.Font("굴림", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))

                    End Select

                    If rsExPrg = "VCMT" Then
                        Select Case objData2.GetType.Name.ToLower()
                            Case "bitmap"
                                rtb.set_Image(objData2.ToString, iAlign)

                            Case "string"
                                If objData2.ToString.Trim.StartsWith("{\rtf") And objData2.ToString.Trim.EndsWith("}") Then
                                    rtb.set_SelRTF(objData2.ToString.Trim, True)
                                    'rtb.set_SelRTF(objData.ToString.Trim.Substring(1, objData.ToString.Trim.Length - 2), False)
                                Else
                                    rtb.set_SelText(objData2.ToString)

                                End If

                        End Select

                        rtb.Font = New System.Drawing.Font("굴림", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
                    End If

                Next

                If rtb.Name = "rtbImg" Then
                    rtb.set_Lock(True)
                End If

                '사용자확인 --> Y
                CType(m_al_StSub(aiSeq - 1), StSubInfo).UsrCfm = "Y"

                sbStSub_Get(aiSeq)
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            invas_buf = Nothing

        End Try
    End Sub

    Private Sub sbStSub_View(ByVal rbReg As Boolean, ByVal riRegStep As Integer)
        Dim sRTF As String = "", sRTF_All As String = ""

        Dim iCnt As Integer = Me.tbcStSubSeq.TabPages.Count

        If iCnt > 0 Then
            '현재 탭의 StSubInfo 저장
            sbStSub_Get(Convert.ToInt32(Me.tbcStSubSeq.SelectedIndex + 1))

            For i As Integer = 1 To iCnt
                sRTF = CType(m_al_StSub(i - 1), StSubInfo).RTF.Trim

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

            If rbReg = False Then
                Me.pnlRst.BringToFront()
                Me.pnlRst.Visible = True
            End If

            Me.rtbStRst.set_SelRTF(sRTF_All, True)
        End If

        sbStSub_View_DbField(True, riRegStep)
    End Sub

    Private Sub sbStSub_View_DbField(ByVal rbAll As Boolean, ByVal riRegStep As Integer)
        Dim rtb As AxAckRichTextBox.AxAckRichTextBox

        If rbAll Then
            '전체
            rtb = Me.rtbStRst
        Else
            '개별
            rtb = Me.rtbSt
        End If
        rtb.set_BcNo(txtBcno.Text.Replace("-", ""))

        rtb.set_DbField_Value(Convert.ToChar(2), mc_sOrdDt, Convert.ToChar(3), Me.AxPatInfo.OrdDt)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sRegNo, Convert.ToChar(3), Me.AxPatInfo.RegNo)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sPatNm, Convert.ToChar(3), Me.AxPatInfo.PatNm)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sSexAge, Convert.ToChar(3), Me.AxPatInfo.SexAge)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sIdNo, Convert.ToChar(3), Me.AxPatInfo.IdNo)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sDrNm, Convert.ToChar(3), Me.AxPatInfo.DocName)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sDeptNm, Convert.ToChar(3), Me.AxPatInfo.DeptName)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sWardNm, Convert.ToChar(3), Me.AxPatInfo.WardRoom)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sEntDay, Convert.ToChar(3), Me.AxPatInfo.EntDt)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sBcNo, Convert.ToChar(3), Me.txtBcno.Text)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sSpcNm, Convert.ToChar(3), Me.AxPatInfo.SpcNmd)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sDiagNm, Convert.ToChar(3), Me.AxPatInfo.DiagNm)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sDrugNm, Convert.ToChar(3), Me.AxPatInfo.DrugNm)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sDrRmk, Convert.ToChar(3), Me.AxPatInfo.Remark)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sCollDt, Convert.ToChar(3), Me.AxPatInfo.CollDt)
        rtb.set_DbField_Value(Convert.ToChar(2), mc_sTkDt, Convert.ToChar(3), Me.AxPatInfo.TkDt)

        Dim sGenDr As String = Me.AxPatInfo.GenDr

        If sGenDr.IndexOf("/"c) >= 0 Then
            Dim sBuf() As String = sGenDr.Split("/"c)
            sGenDr = sBuf(0)
            If sGenDr = "" Then sGenDr = sBuf(1)
            If sGenDr = "" Then sGenDr = Me.AxPatInfo.DocName
        End If

        rtb.set_DbField_Value(Convert.ToChar(2), mc_sGendr, Convert.ToChar(3), sGenDr) '-- 주치의

        If Ctrl.Get_Code(Me.spdSpTest, "testcd", 1) = PRG_CONST.TEST_GV Then
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sFnDt, Convert.ToChar(3), New LISAPP.APP_DB.ServerDateTime().GetDateTime.ToString("yyyy-MM-dd"))
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sFnUsr, Convert.ToChar(3), USER_INFO.USRNM)
        End If

        If riRegStep = 3 Then
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sFnDt, Convert.ToChar(3), New LISAPP.APP_DB.ServerDateTime().GetDateTime.ToString("yyyy-MM-dd HH:mm"))
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sFnUsr, Convert.ToChar(3), USER_INFO.USRNM)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sDoctNo, Convert.ToChar(3), USER_INFO.N_WARDorDEPT)
        End If

        If riRegStep.ToString.Substring(0, 1) = "2" Then
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sMwDt, Convert.ToChar(3), New LISAPP.APP_DB.ServerDateTime().GetDateTime.ToString("yyyy-MM-dd HH:mm"))
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sMwUsr, Convert.ToChar(3), USER_INFO.USRNM)
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sDoctNo, Convert.ToChar(3), USER_INFO.N_WARDorDEPT)
        End If

        Dim dt As New DataTable

        '-- 의뢰의사 면허번호
        dt = LISAPP.APP_SP.fnGet_MediNoInfo_Sp(Me.txtBcno.Text.Replace("-", ""))
        If dt.Rows.Count > 0 Then
            rtb.set_DbField_Value(Convert.ToChar(2), mc_sMediNo, Convert.ToChar(3), dt.Rows(0).Item("medino").ToString)
        End If

        dt = Nothing

        '-- 2008-02-13 YOOEJ Add(관련검사 표시)
        Dim sRegNo As String = ""
        Dim sSpcCd As String = ""
        Dim sTkDt As String = ""

        dt = LISAPP.APP_SP.fnGet_SpcInfo_TkSpcRegno(txtBcno.Text.Replace("-", ""), Me.lblTestCd.Text)
        If dt.Rows.Count > 0 Then
            sRegNo = dt.Rows(0).Item("regno").ToString
            sTkDt = dt.Rows(0).Item("tkdt").ToString + "235959"
            sSpcCd = dt.Rows(0).Item("spccd").ToString
        Else
            Exit Sub
        End If

        dt = Nothing

        dt = LISAPP.APP_SP.fnGet_Rst_SpTest_Sub(Me.txtBcno.Text.Replace("-", ""), Me.lblTestCd.Text, "")
        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(ix).Item("testcd").ToString <> lblTestCd.Text Then
                    Dim sTestCd As String = "Z" + dt.Rows(ix).Item("testcd").ToString
                    Dim sRst As String = dt.Rows(ix).Item("viewrst").ToString.Trim

                    rtb.set_DbField_Value(Convert.ToChar(2), sTestCd, Convert.ToChar(3), sRst)
                End If
            Next
        End If

        dt = Nothing

        dt = LISAPP.APP_SP.fnGet_Rst_SpTest_Ref(sRegNo, Me.lblTestCd.Text, sSpcCd, sTkDt)
        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim sTestCd As String = "Y" + dt.Rows(ix).Item("testcd").ToString.PadRight(7) + dt.Rows(ix).Item("spccd").ToString
                Dim sRst As String = dt.Rows(ix).Item("viewrst").ToString.Trim

                rtb.set_DbField_Value(Convert.ToChar(2), sTestCd, Convert.ToChar(3), sRst)
            Next
        End If

    End Sub

    '<----- Control Event ----->
    Private Sub FGR08_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Return
        If Me.Name = "FGR08" Then spTestTF = True Else spTestTF = False

        sbDisplayInit()

        Dim strTmp As String = COMMON.CommXML.getOneElementXML(msXmlDir, msSlipXml, "SLIP")
        If strTmp <> "" Then
            If Val(strTmp) < cboSlip.Items.Count Then cboSlip.SelectedIndex = Convert.ToInt16(Val(strTmp))
        End If

        mbActivated = True

        If msBcNo <> "" And msTestCd <> "" Then
            If spdSpTest.MaxRows = 0 Then
                MsgBox("검사항목[" + msTestCd + "]는 특수보고서 형식으로 설정하지 않았습니다.!!")
                Me.Close() : Return
            End If
            btnReg_FnAll.Enabled = False
            sbDisplay_Link_Data()
        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Clear()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMove.Click
        If Me.btnMove.Text = "◀" Then
            Me.btnMove.Left = 3
            Me.AxPatInfo.Left = Me.btnMove.Left + Me.btnMove.Width + 1
            Me.pnlSpTest.Left = Me.AxPatInfo.Left
            Me.btnMove.Text = "▶"
        Else
            Me.btnMove.Left = 303
            Me.AxPatInfo.Left = 312
            Me.pnlSpTest.Left = Me.AxPatInfo.Left
            Me.btnMove.Text = "◀"
        End If
    End Sub

    Private Sub btnImgOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenImg.Click
        sbImg_Open()
    End Sub

    Private Sub btnImgRmv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRmvImg.Click
        sbImg_Remove()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim sFn As String = "Handles btnPrint.Click"


        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Try


            Dim sDir As String = System.Windows.Forms.Application.StartupPath & "\SpecialTestUncompress"
            Dim sFile As String = Me.txtBcno.Text.Replace("-", "") + "_" + Me.lblTestCd.Text
            Dim strRTF As String = Me.rtbStRst.get_SelRTF(True)

            If msSpSubExPrg = "IMG" And File.Exists(sDir + "\" + sFile + "\" + sFile + ".jpg") Then

                PrintDocument1.Print()

            Else

                Dim intPos As Integer = strRTF.IndexOf("[PAGE SKIP]")
                Dim intCnt As Integer = 1

                Dim strRTF_p As String = strRTF
                Dim strRTF_t As String = ""
                Dim strFont As String = ""
                Dim intfnt1 As Integer = -1

                Do While intPos >= 0

                    If intCnt = 1 Then
                        Me.rtbStRst.set_SelRTF(strRTF_p.Substring(0, intPos) + "}", True)
                        Me.rtbStRst.print_data()
                    Else
                        Me.rtbStRst.set_SelRTF("", True)
                        strRTF_t = Me.rtbStRst.get_SelRTF(True)

                        Me.rtbStRst.set_SelRTF(strRTF_t.Substring(0, strRTF_t.Length - 3) + strRTF_p.Substring(0, intPos) + "}", True)
                        Me.rtbStRst.print_data()
                    End If


                    strRTF_p = strRTF_p.Substring(intPos + 11)
                    intPos = strRTF_p.IndexOf("[PAGE SKIP]")
                    intCnt += 1
                Loop

                If intCnt = 1 Then
                    Me.rtbStRst.print_data()
                Else
                    Me.rtbStRst.set_SelRTF("", True)
                    strRTF_t = Me.rtbStRst.get_SelRTF(True)

                    Me.rtbStRst.set_SelRTF(strRTF_t.Substring(0, strRTF_t.Length - 3) + strRTF_p, True)
                    Me.rtbStRst.print_data()
                End If
                'Me.rtbStRst.set_SelRTF(strRTF, True)

                'Me.rtbStRst.print_Data()

            End If

            mbAddFileGbn = False
            If cboAddFile.Items.Count > 0 Then
                For intIdx As Integer = 0 To cboAddFile.Items.Count - 1
                    cboAddFile.SelectedIndex = intIdx
                    Me.rtbStRst.set_SelRTF("", True)
                    Me.rtbStRst.set_Image(cboAddFile.Text, True)
                    Me.rtbStRst.print_data()
                Next
            End If
            mbAddFileGbn = True

            Me.rtbStRst.set_SelRTF(strRTF, True)

            Me.Cursor = Windows.Forms.Cursors.Default

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnReg_F_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg_F.Click

        Try
            Dim sCfmSign As String = ""

            If STU_AUTHORITY.FNReg <> "1" Then
                MsgBox("결과검증 권한이 없습니다.!!  확인하세요.")
                Return
            End If

            sbReg_Rst(3, sCfmSign)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnReg_M_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg_m.Click
        sbReg_Rst(22)
    End Sub

    Private Sub btnReg_R_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg_r.Click
        sbReg_Rst(1)
    End Sub

    Private Sub btnResetRst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResetRst.Click
        sbDisplay_Reset_Rst()
    End Sub

    Private Sub btnRstAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRstAll.Click
        Dim sText As String = Me.btnRstAll.Text

        If sText.IndexOf("전체") >= 0 Then
            Me.tbcStSubSeq.Visible = False

            sbStSub_View(False, 0)
        Else
            Me.tbcStSubSeq.Visible = True

            Me.pnlRst.Visible = False
            Me.pnlRst.SendToBack()
        End If

        Me.btnRstAll.Text = Me.btnRstAll.AccessibleName
        Me.btnRstAll.AccessibleName = sText
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchNR.Click, btnSearchNF.Click, btnSearchF.Click, btnSearchA.Click
        Dim btn As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)

        sbDisplay_Search(btn.Name.ToUpper.Replace("BTNSEARCH", ""))
    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Dim CommFn As New COMMON.CommFN.Fn
        CommFn.SearchToggle(Me.lblSearch, Me.btnToggle, enumToggle.BcnoToRegno, Me.txtNo)

        If Me.lblSearch.Text = "검체번호" Then
            Dim sSlipCd As String = Ctrl.Get_Code(Me.cboSlip)

            If PRG_CONST.SLIP_ExLab.Contains(Ctrl.Get_Code(Me.cboSlip)) Then
                Me.chkFilter.Checked = True
                Me.chkFilter.Enabled = True

                Me.chkFilter.Text = "결과일자/검사분야/검사코드 조건 적용"
                Me.lblDate.Text = "결과일자"
            Else
                Me.chkFilter.Checked = False
                Me.chkFilter.Enabled = False

                Me.chkFilter.Text = "접수일자/검사분야/검사코드 조건 적용"
                Me.lblDate.Text = "접수일자"
            End If
        Else
            Me.chkFilter.Checked = True
            Me.chkFilter.Enabled = True

            Me.chkFilter.Text = "접수일자/검사분야/검사코드 조건 적용"
            Me.lblDate.Text = "접수일자"
        End If

        Me.txtNo.Focus()
    End Sub

    Private Sub btnUpDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpDown.Click
        If Me.btnUpDown.Text = "▲" Then
            Me.pnlSpTest.Location = New Drawing.Point(Me.pnlSpTest.Location.X, 6)
            Me.pnlSpTest.Height += 129 - 6
            Me.btnUpDown.Text = "▼"
            Me.txtTestCont.Visible = False
        Else
            Me.pnlSpTest.Location = New Drawing.Point(Me.pnlSpTest.Location.X, 129)
            Me.pnlSpTest.Height -= 129 - 6
            Me.btnUpDown.Text = "▲"
            Me.txtTestCont.Visible = True
        End If
    End Sub

    Private Sub cboTSect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        If piProcessing = 1 Then Return
        If piSkip = 1 Then Return

        COMMON.CommXML.setOneElementXML(msXmlDir, msSlipXml, "SLIP", cboSlip.SelectedIndex.ToString)

        If PRG_CONST.SLIP_ExLab.Contains(Ctrl.Get_Code(cboSlip)) Then
            If Me.lblSearch.Text = "검체번호" Then
                Me.chkFilter.Text = "결과일자/담당계/검사코드 조건 적용"
                Me.lblDate.Text = "결과일자"
            Else
                Me.chkFilter.Text = "접수일자/담당계/검사코드 조건 적용"
                Me.lblDate.Text = "접수일자"
            End If
            Me.chkFilter.Checked = True
            Me.chkFilter.Enabled = True
        Else
            Me.chkFilter.Text = "접수일자/담당계/검사코드 조건 적용"
            Me.lblDate.Text = "접수일자"

            If Me.lblSearch.Text = "검체번호" Then
                Me.chkFilter.Checked = False
                Me.chkFilter.Enabled = False
            Else
                Me.chkFilter.Checked = True
                Me.chkFilter.Enabled = True
            End If
        End If
        sbDisplay_SpTest()
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        Dim sBcNo As String = Ctrl.Get_Code(Me.spdList, "bcno", e.row).Replace("-", "")
        Dim sTestCd As String = Ctrl.Get_Code(Me.spdList, "testcd", e.row)

        If sBcNo.Replace("-", "") = Me.txtBcno.Text.Replace("-", "") And sTestCd = Me.lblTestCd.Text Then Return

        sbDisplayInit_lblTestInfo()

        Me.AxPatInfo.BcNo = sBcNo
        Me.AxPatInfo.fnDisplay_Data()
        Me.AxPatInfo.sbDisplay_rst_info(sBcNo, sTestCd)

        Me.lblTestCd.Text = Ctrl.Get_Code(Me.spdList, "testcd", e.row)
        Me.lblTNm.Text = Ctrl.Get_Code(Me.spdList, "tnmd", e.row)
        Me.txtPartSlip.Text = Ctrl.Get_Code(Me.spdList, "partslip", e.row)

        sbDisplay_BcNo(sBcNo)

        STU_RVInfo.msRegNo = AxPatInfo.RegNo

    End Sub

    Private Sub spdList_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdList.KeyDownEvent
        Select Case Convert.ToInt32(e.keyCode)
            Case Keys.PageUp
                e.keyCode = 0

            Case Keys.PageDown
                e.keyCode = 0
        End Select
    End Sub

    Private Sub spdList_LeaveCell(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdList.LeaveCell
        If e.newCol < 1 Then Return
        If e.newRow < 1 Then Return
        If e.row = e.newRow Then Return

        spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(e.newCol, e.newRow))
    End Sub

    Private Sub tbcStSubSeq_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcStSubSeq.SelectedIndexChanged
        If piProcessing = 1 Then Return
        If piSkip = 1 Then Return

        If CType(sender, Windows.Forms.TabControl).TabPages.Count = 0 Then Return

        Dim iStSubSeq As Integer = CType(sender, Windows.Forms.TabControl).SelectedIndex + 1

        Try
            '이전 StSubSeq의 내용 저장
            If miStSubSeq > 0 Then
                sbStSub_Get(miStSubSeq)
            End If

            sbStSub_Set(iStSubSeq)

        Catch ex As Exception

        Finally
            miStSubSeq = iStSubSeq

        End Try
    End Sub

    Private Sub txtNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.Click
        Me.txtNo.SelectAll()
    End Sub

    Private Sub txtNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNo.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            sbDisplay_txtNo_KeyDown()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        sbCancel()
    End Sub

    ' 취소 
    Private Sub sbCancel()
        Dim sFn As String = "Private Sub sbCancel()"
        Dim alOrdList As New ArrayList
        Dim row As Integer = Me.spdList.ActiveRow

        Dim rstflg As String = Ctrl.Get_Code(Me.spdList, "rstflg", row)


        Try

            If fnValidation() = False Then Exit Sub

            fnSelOrdList(alOrdList)

            If alOrdList.Count > 0 Then

                If MsgBox("취소 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    Exit Sub
                End If

                With (New LISAPP.APP_J.Cancel)
                    .CancelTItem = alOrdList
                    .CancelCmt = "종합검증취소"

                    Dim sRet As String = ""
                    If rstflg < "1" Then
                        sRet = .ExecuteDo(enumCANCEL.채혈접수취소, USER_INFO.USRID)
                    Else
                        sRet = .ExecuteDo(enumCANCEL.REJECT, USER_INFO.USRID)
                    End If

                    If sRet <> "" Then
                        Throw (New Exception(sRet))

                    Else
                        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 종합검증 취소되었습니다.")
                        sbDisplay_Clear()
                    End If
                End With

            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "취고할 자료를 선택해 주십시오.")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 데이타 유효성 체크
    Private Function fnValidation() As Boolean
        Dim sFn As String = "Private Function fnValidation() As Boolean"
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Dim row As Integer = spd.Row
        fnValidation = False
        Try
            Dim sRstflg As String = Ctrl.Get_Code(spd, "rstflg", row)

            ' 기능사용 유무 
            Dim strDESC As String = ""
            If sRstflg < "1" Then
                ' 접수취소 
                If Not USER_SKILL.Authority("J01", 2, strDESC) Then
                    MsgBox("[" & strDESC & "] 권한이 없어 처리할 수 없습니다", MsgBoxStyle.Information, Me.Text)
                    Exit Function
                End If
            Else
                If Not USER_SKILL.Authority("J01", 3, strDESC) Then
                    MsgBox("[" & strDESC & "] 권한이 없어 처리할 수 없습니다", MsgBoxStyle.Information, Me.Text)
                    Exit Function
                End If
            End If

            fnValidation = True

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    ' 취소할 항목 ArrayList에 Add
    Private Function fnSelOrdList(ByRef aoOrdList As ArrayList) As Boolean
        Dim sFn As String = "Private Sub fnSelOrdList(ByRef aoOrdList As ArrayList)"
        Dim objOrdList As STU_CANCELINFO
        Dim strRstStat As String = ""
        Dim strOrdNm As String = ""

        Try
            fnSelOrdList = False

            objOrdList = New STU_CANCELINFO
            objOrdList.BCNO = Me.txtBcno.Text.Replace("-", "")
            objOrdList.REGNO = Me.AxPatInfo.RegNo
            objOrdList.TCLSCD = Me.lblTestCd.Text
            objOrdList.SPCCD = PRG_CONST.SPC_GV
            objOrdList.OWNGBN = "L"
            objOrdList.BCCLSCD = ""
            objOrdList.CANCELCMT = Me.AxPatInfo.Remark
            strRstStat = Ctrl.Get_Code(Me.spdList, "rstflg", Me.spdList.ActiveRow)
            strOrdNm = Me.lblTNm.Text

            aoOrdList.Add(objOrdList)
            fnSelOrdList = True
            aoOrdList.TrimToSize()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    Private Sub FGR08_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        STU_RVInfo.msRegNo = ""
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGR08_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.PageUp
                btnListUpDown_Click(btnUp, Nothing)
            Case Keys.PageDown
                btnListUpDown_Click(btnDown, Nothing)
            Case Keys.F2
                If Me.lblSearch.Text = "검체번호" Then
                    btnToggle_Click(Nothing, Nothing)
                End If
                Me.txtNo.Focus()

            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_Click(Nothing, Nothing)
            Case Keys.F9
                btnReg_R_Click(Nothing, Nothing)
            Case Keys.F11
                btnReg_M_Click(Nothing, Nothing)
            Case Keys.F12
                btnReg_F_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGR08_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim sInitDir As String = System.Windows.Forms.Application.StartupPath

        Try
            Me.WindowState = FormWindowState.Maximized

            DS_FormDesige.sbInti(Me)
            Me.spdList.ColsFrozen = spdList.GetColFromID("check")

            msEmrPrintName = (New COMMON.CommPrint.PRT_Printer("EMRIMG")).GetInfo.PRTNM

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnReg_FnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg_FnAll.Click
        Dim sFn As String = "Handles btnReg_FnAll_Click.Click"
        Dim sBcNo As String = ""
        Dim sTestCd As String = ""
        Dim sSpcNmd As String = ""
        Dim sPartSlip As String = ""
        Try

            With Me.spdList
                If MsgBox("일괄보고하시겠습니까?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Return
                End If

                Me.rtbStRst.Visible = False
                For i As Integer = 0 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("check")

                    If .Text = "1" Then
                        .Col = .GetColFromID("rstflg")

                        If .Text = "2" Then
                            .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                            .Col = .GetColFromID("testcd") : sTestCd = .Text
                            .Col = .GetColFromID("spcnmd") : sSpcNmd = .Text
                            .Col = .GetColFromID("partcd") : sPartSlip = .Text

                            Me.txtBcno.Text = sBcNo
                            Me.lblTestCd.Text = sTestCd
                            Me.txtPartSlip.Text = sPartSlip

                            sbAllReg_Rst(3)

                            Me.rtbStRst.set_SelRTF("", True)


                        End If
                    End If
                Next
                Me.rtbStRst.Visible = True
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Private Sub sbAllReg_Rst(ByVal riRegStep As Integer)
        Dim sFn As String = "sbAllReg_Rst"
        Dim sBcno As String = Me.txtBcno.Text.Trim
        Dim sTestCd As String = Me.lblTestCd.Text.Trim

        Try

            If Me.txtBcno.Text.Trim.Length = 0 Then Return
            If Me.lblTestCd.Text.Trim.Length = 0 Then Return

            '전체보기 후 등록함
            sbStSub_View(True, riRegStep)

            Dim iDisable As Integer = 0

            Dim al_ChgRst As ArrayList = fnGet_Change_AllRst(riRegStep, iDisable)

            '오류 발생 시
            If al_ChgRst Is Nothing Then
                MsgBox(msDisableMsg)
                Return
            End If

            Dim sMsg As String = Me.lblTNm.Text + " ( " + Me.lblTestCd.Text + " ) : "

            Dim si As New STU_SampleInfo

            si.RegStep = riRegStep.ToString()
            si.BCNo = Me.txtBcno.Text.Replace("-", "")
            si.EqCd = ""
            si.UsrID = USER_INFO.USRID
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
                sbDisplay_Clear()
            Else
                MsgBox(sMsg.Replace("하시겠습니까?", "중 오류가 발생하였습니다!!"), MsgBoxStyle.Critical)
            End If


            si = Nothing

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            'System.Threading.Thread.Sleep(3000)
            'Timer1.Enabled = True

            'Dim sFile As String = sBcno.Replace("-", "") & "_" & sTestCd & ".rtf"
            'Dim sDir As String = System.Windows.Forms.Application.StartupPath & "\SpecialTest"

            'If My.Computer.FileSystem.FileExists(sDir & "\" & sFile) Then
            '    My.Computer.FileSystem.DeleteFile(sDir & "\" & sFile)
            'End If

            'If My.Computer.FileSystem.FileExists(sDir & "\" & sFile.Substring(0, sFile.IndexOf(".")) + ".gzip") Then
            '    My.Computer.FileSystem.DeleteFile(sDir & "\" & sFile.Substring(0, sFile.IndexOf(".")) + ".gzip")
            'End If
        End Try
    End Sub

    Private Function fnGet_Change_AllRst(ByVal riRegStep As Integer, ByRef riDisable As Integer) As ArrayList
        Dim sFn As String = "fnGet_Change_AllRst"

        Dim al As New ArrayList
        Dim ri As STU_RstInfo

        Try

            ri = New STU_RstInfo

            ri.TestCd = Me.lblTestCd.Text

            '일반검사 결과
            Select Case riRegStep.ToString.Substring(0, 1)
                Case "1"
                    If Me.txtStRstTxtR.Text.Trim.Length = 0 Then
                        ri.OrgRst = "{null}"
                    Else
                        ri.OrgRst = Me.txtStRstTxtR.Text
                    End If

                Case "2"
                    If Me.txtStRstTxtM.Text.Trim.Length = 0 Then
                        ri.OrgRst = "{null}"
                    Else
                        ri.OrgRst = Me.txtStRstTxtM.Text
                    End If

                Case "3"
                    If Me.txtStRstTxtF.Text.Trim.Length = 0 Then
                        ri.OrgRst = "{null}"
                    Else
                        ri.OrgRst = Me.txtStRstTxtF.Text
                    End If

            End Select

            ri.RstCmt = ""

            ' ri.RstRTF = Me.rtbStRst.get_SelRTF(True).Trim
            ri.RstRTF = Me.rtbStRst.get_SelRTF(True).Trim
            ri.RstTXT = Fn.SubstringH(Me.rtbStRst.get_SelText(True).Trim, 0, 4000)
            'ri.GzipNAME = fnSpecialTest_Compress(Me.rtbStRst.get_SelRTF(True).Trim)
            al.Add(ri)

            ri = Nothing

            '-- 2008/02/21 YEJ Add(서브항목이 있는 경우도 처리)
            Dim dt As DataTable = LISAPP.APP_SP.fnGet_Rst_SpTest_Sub(Me.txtBcno.Text.Replace("-", ""), Me.lblTestCd.Text, riRegStep.ToString)
            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1

                    If dt.Rows(ix).Item("testcd").ToString.Length > 5 Then
                        ri = New STU_RstInfo
                        ri.TestCd = dt.Rows(ix).Item("testcd").ToString
                        ri.OrgRst = dt.Rows(ix).Item("orgrst").ToString
                        ri.RstCmt = dt.Rows(ix).Item("rstcmt").ToString
                        ri.EqFlag = dt.Rows(ix).Item("eqflag").ToString

                        al.Add(ri)

                        ri = Nothing
                    End If
                Next
            End If

            Return al

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New ArrayList
        Finally
            al = Nothing

        End Try
    End Function

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim sFn As String = "PrintDocument1_PrintPage"
        Try


            With e

                .Graphics.DrawImage(Me.PictureBox1.Image, 0, 0)

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub btnReg_FnBcNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_FnBcno.Click

        Dim sFn As String = "Handles btnReg_FnBcno.Click"
        Dim sBcno As String = Me.txtBcno.Text.Trim
        Dim sTestCd As String = Me.lblTestCd.Text.Trim

        Try
            If Me.txtBcno.Text.Trim.Length = 0 Then Return
            If Me.lblTestCd.Text.Trim.Length = 0 Then Return

            Dim iDisable As Integer = 0
            Dim sMsg As String = "검체번호 (" + Me.txtBcno.Text + ": "

            Dim ri As STU_RstInfo
            Dim al_ChgRst As New ArrayList

            Dim objDTable As New DataTable

            objDTable = LISAPP.APP_SP.fnGet_Rst_SpTest_img(Me.txtBcno.Text.Replace("-", ""))

            If objDTable.Rows.Count > 0 Then
                For intIx1 As Integer = 0 To objDTable.Rows.Count - 1

                    ri = New STU_RstInfo
                    ri.TestCd = objDTable.Rows(intIx1).Item("testcd").ToString
                    ri.OrgRst = "{null}"
                    ri.RstCmt = ""
                    ri.EqFlag = ""

                    al_ChgRst.Add(ri)

                    ri = Nothing

                    If intIx1 <> 0 Then sMsg += ", "
                    sMsg += objDTable.Rows(intIx1).Item("testcd").ToString
                Next
            Else
                MsgBox("완료할 자료가 없습니다.")
                Return
            End If

            sMsg += ") 최종보고 하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return
            End If

            Dim si As New STU_SampleInfo


            si.RegStep = "3"
            si.BCNo = Me.txtBcno.Text.Replace("-", "")
            si.EqCd = ""
            si.UsrID = USER_INFO.USRID
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
                sbDisplay_Clear()
            Else
                MsgBox(sMsg.Replace("하시겠습니까?", "중 오류가 발생하였습니다!!"), MsgBoxStyle.Critical)
            End If

            si = Nothing

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try

    End Sub

    Private Sub btnCmdHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCmdHelp.Click

        Dim sFn As String = "sbStSub_Set_ExPrg"

        Dim invas_buf As New InvAs

        Try
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\POPUPWIN.dll", "POPUPWIN.FGPOPUPST_VSPT")

                .SetProperty("UserID", USER_INFO.USRID)

                Dim a_objParam() As Object
                ReDim a_objParam(4)

                a_objParam(0) = Me
                a_objParam(1) = LISAPP.APP_DB.DbFn.fnGet_DbConnect()
                a_objParam(2) = Me.txtBcno.Text.Replace("-", "")
                a_objParam(3) = Me.lblTestCd.Text
                a_objParam(4) = Me.lblTNm.Text

                Dim al_return As ArrayList = CType(.InvokeMember("Display_Result", a_objParam), ArrayList)

                If al_return Is Nothing Then Return
                If al_return.Count < 1 Then Return

                Dim rtb As AxAckRichTextBox.AxAckRichTextBox

                If Me.rtbSt.Visible Then
                    rtb = Me.rtbSt
                Else
                    rtb = Me.rtbStRst
                End If

                If Not CType(.GetProperty("Append"), Boolean) Then
                    '초기화
                    rtb.set_SelRTF("", True)
                End If

                For i As Integer = 1 To al_return.Count
                    Dim objData As Object = CType(al_return(i - 1), STU_StDataInfo).Data
                    Dim objData2 As Object = CType(al_return(i - 1), STU_StDataInfo).Data2
                    Dim iAlign As Integer = CType(al_return(i - 1), STU_StDataInfo).Alignment

                    Select Case objData.GetType.Name.ToLower()
                        Case "string"
                            rtb.set_DbField_Value(Convert.ToChar(2), mc_sComment, Convert.ToChar(3), objData.ToString)
                    End Select
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            invas_buf = Nothing

        End Try

    End Sub

    Private Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelect.CheckedChanged

        With spdSpTest
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = 1
                If chkSelect.Checked Then
                    If .Text = "" Then .Text = "1"
                Else
                    If .Text = "1" Then .Text = ""
                End If
            Next
        End With

    End Sub

    Private Sub btnListUpDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click, btnDown.Click
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        If spd.MaxRows = 0 Then Return

        Dim iNext As Integer = 0

        If CType(sender, Windows.Forms.Button).Name.ToLower.EndsWith("up") Then
            If spd.ActiveRow < 1 Then Return

            iNext -= 1
        Else
            If spd.ActiveRow = spd.MaxRows Then Return

            iNext += 1
        End If

        Me.spdList_LeaveCell(spd, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(1, spd.ActiveRow, 1, spd.ActiveRow + iNext, False))

        With spd
            .ReDraw = False
            .SetActiveCell(1, .ActiveRow + iNext)
            '   .Action = FPSpreadADO.ActionConstants.ActionGotoCell
            .ReDraw = True
        End With
    End Sub

    Private Sub btnAddFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddFile.Click

        Dim sFn As String = "btnAddFile_Click"

        Dim invas_buf As New InvAs

        Try
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\POPUPWIN.dll", "POPUPWIN.FGPOPUPST_AFILE")

                .SetProperty("UserID", USER_INFO.USRID)

                Dim a_objParam() As Object
                ReDim a_objParam(5)

                a_objParam(0) = Me
                a_objParam(1) = LISAPP.APP_DB.DbFn.fnGet_DbConnect()
                a_objParam(2) = Me.txtBcno.Text.Replace("-", "")
                a_objParam(3) = Me.lblTestCd.Text
                a_objParam(4) = Me.lblTNm.Text
                a_objParam(5) = ""

                Dim strFileName As String = .InvokeMember("Display_Result", a_objParam).ToString

                cboAddFile.Items.Clear()

                If strFileName Is Nothing Then Return
                If strFileName <> "" Then
                    cboAddFile.Items.Add(strFileName)
                End If

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            invas_buf = Nothing

        End Try

    End Sub

    Private Sub cboAddFile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAddFile.SelectedIndexChanged
        If mbAddFileGbn = False Then Return

        Dim sFn As String = "btnAddFile_Click"

        Dim invas_buf As New InvAs

        Try
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\POPUPWIN.dll", "POPUPWIN.FGPOPUPST_AFILE")

                .SetProperty("UserID", USER_INFO.USRID)

                Dim a_objParam() As Object
                ReDim a_objParam(5)

                a_objParam(0) = Me
                a_objParam(1) = LISAPP.APP_DB.DbFn.fnGet_DbConnect()
                a_objParam(2) = Me.txtBcno.Text.Replace("-", "")
                a_objParam(3) = Me.lblTestCd.Text
                a_objParam(4) = Me.lblTNm.Text
                a_objParam(5) = Me.cboAddFile.Text

                Dim strFileName As String = .InvokeMember("Display_Result", a_objParam).ToString

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            invas_buf = Nothing

        End Try

    End Sub

    Private Sub mnuQry_orddt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuQry_orddt.Click

        Dim sRegNo As String = AxPatInfo.RegNo
        Dim sDayS As String = Format(Me.dtpTkS.Value, "yyyy-MM-dd").Replace("-", "")
        Dim sDayE As String = Format(Me.dtpTkE.Value, "yyyy-MM-dd").Replace("-", "")
        Dim objForm As New LISV.FGRV01(sRegNo, sDayS, sDayE, True, False, True)
        objForm.ShowDialog()

    End Sub

    Private Sub mnuQry_rstdt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuQry_rstdt.Click


        Dim sRegNo As String = AxPatInfo.RegNo
        Dim sDayS As String = Format(Me.dtpTkS.Value, "yyyy-MM-dd").Replace("-", "")
        Dim sDayE As String = Format(Me.dtpTkE.Value, "yyyy-MM-dd").Replace("-", "")
        Dim objForm As New LISV.FGRV01(sRegNo, sDayS, sDayE, True, True, True)
        objForm.ShowDialog()

    End Sub

    Private Sub mnuQry_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuQry_test.Click

        Dim sRegNo As String = AxPatInfo.RegNo
        Dim sDayS As String = Format(Me.dtpTkS.Value, "yyyy-MM-dd").Replace("-", "")
        Dim sDayE As String = Format(Me.dtpTkE.Value, "yyyy-MM-dd").Replace("-", "")
        Dim objForm As New LISV.FGRV12(sRegNo, sDayS, sDayE, , True)
        objForm.ShowDialog()

    End Sub

    Private Sub btnPrint_All_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_All.Click
        Try

            With spdList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                    Dim dt As DataTable = LISAPP.APP_SP.fnGet_Rst_SpTest(sBcNo, sTestCd)

                    If dt.Rows.Count > 0 Then
                        For intIdx As Integer = 0 To dt.Rows.Count - 1
                            Dim intStRst As Integer = 0

                            Me.rtbStRst.set_SelRTF(dt.Rows(intIdx).Item("rstrtf").ToString, True)
                            Me.rtbStRst.print_data()
                        Next
                    End If
                Next
            End With

        Catch ex As Exception
            MsgBox(ex.Message, , "보고서인쇄")
        End Try

    End Sub

    Private Sub chkFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFilter.Click
        If chkFilter.Checked Then
            lblDate.Text = chkFilter.Text.Substring(0, 4)
        Else
            lblDate.Text = "접수일자"
        End If

    End Sub

    Private Sub dtpTkS_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpTkS.CloseUp, dtpTkE.CloseUp
        If piProcessing = 1 Then Return
        If piSkip = 1 Then Return

        sbDisplay_Slip()
        sbDisplay_SpTest()

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal riUseMode As Integer, ByVal rsTestCd As String)
        MyBase.New()

        piProcessing = 1

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        piProcessing = 0

        'piUseMode = 0 --> 일반, piUseMode = 1 --> psCd_Include_Exclude만 포함, piUseMode = 2 --> psCd_Include_Exclude를 제외
        piUseMode = riUseMode
        psCd_Include_Exclude = rsTestCd
    End Sub

    Public Sub New(ByVal riUseMode As Integer, ByVal rsTestCd As String, ByVal rsBcNo As String)
        MyBase.New()

        piProcessing = 1

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        piProcessing = 0

        'piUseMode = 0 --> 일반, piUseMode = 1 --> psCd_Include_Exclude만 포함, piUseMode = 2 --> psCd_Include_Exclude를 제외
        piUseMode = riUseMode
        psCd_Include_Exclude = rsTestCd

        msBcNo = rsBcNo
        msTestCd = rsTestCd

    End Sub

    Private Sub btnQuery_rst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery_rst.Click
        Dim sFn As String = "btnQuery_rst_Click"

        Dim invas_buf As New InvAs

        Try
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\POPUPWIN.dll", "POPUPWIN.FGPOPUPST_VRST3")

                .SetProperty("UserID", USER_INFO.USRID)

                Dim a_objParam() As Object
                ReDim a_objParam(4)

                a_objParam(0) = Me
                a_objParam(1) = LISAPP.APP_DB.DbFn.fnGet_DbConnect()

                a_objParam(2) = Me.txtBcno.Text.Replace("-", "")
                a_objParam(3) = Me.lblTestCd.Text
                a_objParam(4) = Me.lblTNm.Text

                Dim al_return As ArrayList = CType(.InvokeMember("Display_Result", a_objParam), ArrayList)
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            invas_buf = Nothing
        End Try
    End Sub

    Private Sub btnQuery_pat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery_pat.Click

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_Current(Me.AxPatInfo.RegNo) '.Text.Trim())

            If dt.Rows.Count = 0 Then
                MsgBox("OCS에서 환자정보를 찾을 수 없습니다!!", MsgBoxStyle.Information)

                Return
            End If

            Dim iLeft As Integer = Ctrl.FindControlLeft(btnQuery_pat)
            Dim iTop As Integer = Ctrl.menuHeight + Ctrl.FindControlTop(btnQuery_pat) + btnQuery_pat.Height

            Dim patallinfo As New OCSAPP.FGOCS01

            With patallinfo
                .Left = iLeft
                .Top = iTop

                .gsRegNo = dt.Rows(0).Item("regno").ToString()
                .gsPatNm = dt.Rows(0).Item("patnm").ToString()
                .gsSexAge = dt.Rows(0).Item("sexage").ToString()
                .gsIdNo = dt.Rows(0).Item("idno").ToString()

                .gsOrdDt = dt.Rows(0).Item("orddt").ToString()
                .gsDeptNm = dt.Rows(0).Item("deptnm").ToString()
                .gsDoctorNm = dt.Rows(0).Item("doctornm").ToString()
                .gsWardRoom = dt.Rows(0).Item("wardroom").ToString()
                '.InWonDate = dt.Rows(0).Item("entdt").ToString + "/" + dt.Rows(0).Item("entdt_to").ToString
                .gsNowDate = Format(Now, "yyyyMMdd").ToString

                .gsTel = (dt.Rows(0).Item("tel1").ToString() + " / " + dt.Rows(0).Item("tel2").ToString()).Trim
                If .gsTel.StartsWith("/") Then .gsTel = .gsTel.Substring(1)
                If .gsTel.EndsWith("/") Then .gsTel = .gsTel.Substring(0, .gsTel.Length - 1)

                .gsAddr1 = dt.Rows(0).Item("addr1").ToString().Trim
                .gsAddr2 = dt.Rows(0).Item("addr2").ToString().Trim

                .sbDisplay_PatInfo() '환자 기본정보 출력

                .sbDisplay_SujinInfo() '환자 수진내역 출력

                .spdOrdDt.MaxRows = 0

                .spdOrdInfo.MaxRows = 0

                .ShowDialog()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImage.Click
        Try

            fnSaveImage()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnEMRImage_Click(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles btnEMRImage.Click

        With Me.AxPatInfo

            Dim sRow As String = ""
            Dim scol As String = ""
            Dim sBcno As String = ""
            Dim sPatnm As String = ""
            Dim sTestcd As String = ""

            sBcno = Replace(.BcNo, "-", "")
            sPatnm = .PatNm
            sTestcd = lblTestCd.Text

            Dim objFrm As New LISR.FGR08_S03(sBcno, sPatnm, sTestcd)

            objFrm.sbDisplay_Data()


        End With


    End Sub


    Private Sub CButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRstHis.Click
        Try

            'With Me.spdList

            '    Dim ispdRows As Integer = .MaxRows
            '    Dim sBcno As String
            '    Dim sTestcd As String
            '    Dim sPatnm As String
            '    Dim sTnmd As String
            '    Dim sPartslip As String

            '    If ispdRows < 0 Then Return

            '    For icnt As Integer = 0 To ispdRows - 1
            '        .Row = icnt + 1
            '        .Col = .GetColFromID("bcno") : sBcno = .Text
            '        .Col = .GetColFromID("testcd") : sTestcd = .Text
            '        .Col = .GetColFromID("patnm") : sPatnm = .Text
            '        .Col = .GetColFromID("tnmd") : sTnmd = .Text
            '        .Col = .GetColFromID("partslip") : sPartslip = .Text

            '        '스프레드클릭 이벤트

            '        If sBcno.Replace("-", "") = Me.txtBcno.Text.Replace("-", "") And sTestcd = Me.lblTestCd.Text Then Return

            '        sbDisplayInit_lblTestInfo()

            '        Me.AxPatInfo.BcNo = sBcNo
            '        Me.AxPatInfo.fnDisplay_Data()
            '        Me.AxPatInfo.sbDisplay_rst_info(sBcNo, sTestCd)

            '        Me.lblTestCd.Text = sTestcd
            '        Me.lblTNm.Text = sTnmd
            '        Me.txtPartSlip.Text = sPartslip

            '        sbDisplay_BcNo(sBcno.Replace("-", ""))

            '        STU_RVInfo.msRegNo = AxPatInfo.RegNo

            '        '이미지 전송
            '        fnSaveImage(sBcno, sTestcd, sPatnm)

            '    Next

            'End With

            Dim regno As String = AxPatInfo.RegNo

            Dim obj As New LISR.FGR08_S05
            obj.Display_Data(regno)
            obj.ShowDialog()



        Catch ex As Exception

        End Try


    End Sub

End Class

Public Class StSubInfo
    Public Name As String = ""
    Public Type As String = ""

    Public ImgType As String = ""
    Public ImgSizeW As String = ""
    Public ImgSizeH As String = ""
    Public RTF As String = ""
    Public ExPrg As String = ""
    Public UsrCfm As String = ""
End Class