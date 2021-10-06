'>>> 검사
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FGF11
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGF11.vb, Class : FGF11" + vbTab

    Private Const mcDevFrmBaseWidth As Integer = 1280
    Private Const mcDevFrmBaseHeight As Integer = 1024
    Private Const mcDevFrmMinWidth As Integer = 200
    Private Const mcDevMainPanelHeight As Integer = 40 '58 

    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2
    Private mbActivated As Boolean = False

    Private mo_DAF As New LISAPP.APP_F_TEST

    Public giClearKey As Integer = 0
    Friend WithEvents btnGetExcel As System.Windows.Forms.Button
    Friend WithEvents cboDSpcNm2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblDSpcNm2 As System.Windows.Forms.Label
    Friend WithEvents txtDSpcCd2 As System.Windows.Forms.TextBox
    Friend WithEvents lblLine4 As System.Windows.Forms.Label
    Friend WithEvents lblLine6 As System.Windows.Forms.Label
    Friend WithEvents chkViwSub As System.Windows.Forms.CheckBox
    Friend WithEvents lblLine3 As System.Windows.Forms.Label
    Friend WithEvents chkReqSub As System.Windows.Forms.CheckBox
    Friend WithEvents chkFixRptYN As System.Windows.Forms.CheckBox
    Friend WithEvents cboFRptMi As System.Windows.Forms.ComboBox
    Friend WithEvents txtFRptMI As System.Windows.Forms.TextBox
    Friend WithEvents cboPRptMi As System.Windows.Forms.ComboBox
    Friend WithEvents chkRptYN As System.Windows.Forms.CheckBox
    Friend WithEvents chkTatYN As System.Windows.Forms.CheckBox
    Friend WithEvents txtPRptMi As System.Windows.Forms.TextBox
    Friend WithEvents chkTitleYN As System.Windows.Forms.CheckBox
    Friend WithEvents lblPRptMi As System.Windows.Forms.Label
    Friend WithEvents lblFRptMi As System.Windows.Forms.Label
    Friend WithEvents lblBpGbn As System.Windows.Forms.Label
    Friend WithEvents cboBpGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblLine7 As System.Windows.Forms.Label
    Friend WithEvents txtTubeUnit As System.Windows.Forms.TextBox
    Friend WithEvents txtTubeVol As System.Windows.Forms.TextBox
    Friend WithEvents cboMGTType As System.Windows.Forms.ComboBox
    Friend WithEvents cboBBTType As System.Windows.Forms.ComboBox
    Friend WithEvents cboMBTType As System.Windows.Forms.ComboBox
    Friend WithEvents txtSameCd As System.Windows.Forms.TextBox
    Friend WithEvents lblSameCd As System.Windows.Forms.Label
    Friend WithEvents lblMGTType As System.Windows.Forms.Label
    Friend WithEvents lblBBTType As System.Windows.Forms.Label
    Friend WithEvents lblMBTType As System.Windows.Forms.Label
    Friend WithEvents txtSeqTMi As System.Windows.Forms.TextBox
    Friend WithEvents lblSeqTMi As System.Windows.Forms.Label
    Friend WithEvents chkSeqTYN As System.Windows.Forms.CheckBox
    Friend WithEvents cboExLabNmD As System.Windows.Forms.ComboBox
    Friend WithEvents txtExLabCd As System.Windows.Forms.TextBox
    Friend WithEvents chkExLabYN As System.Windows.Forms.CheckBox
    Friend WithEvents lblExLabCd As System.Windows.Forms.Label
    Friend WithEvents txtMinSpcVol As System.Windows.Forms.TextBox
    Friend WithEvents lblMinSpcVol As System.Windows.Forms.Label
    Friend WithEvents lblTube As System.Windows.Forms.Label
    Friend WithEvents lblTubeVol As System.Windows.Forms.Label
    Friend WithEvents cboBcclsNmd As System.Windows.Forms.ComboBox
    Friend WithEvents txtBcclsCd As System.Windows.Forms.TextBox
    Friend WithEvents lblBcclsCd As System.Windows.Forms.Label
    Friend WithEvents txtDispSeqL As System.Windows.Forms.TextBox
    Friend WithEvents lblDispSeqL As System.Windows.Forms.Label
    Friend WithEvents lblLine2 As System.Windows.Forms.Label
    Friend WithEvents txtCWarning As System.Windows.Forms.TextBox
    Friend WithEvents cboOWarningGbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtOWarning As System.Windows.Forms.TextBox
    Friend WithEvents lblOWarning As System.Windows.Forms.Label
    Friend WithEvents cboTubeNmD As System.Windows.Forms.ComboBox
    Friend WithEvents txtTubeCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTubeCd As System.Windows.Forms.Label
    Friend WithEvents lblIOGbn As System.Windows.Forms.Label
    Friend WithEvents chkIOGbnI As System.Windows.Forms.CheckBox
    Friend WithEvents chkIOGbnO As System.Windows.Forms.CheckBox
    Friend WithEvents txtCprtGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblCprtGbn As System.Windows.Forms.Label
    Friend WithEvents txtEdiCd As System.Windows.Forms.TextBox
    Friend WithEvents lblEdiCd As System.Windows.Forms.Label
    Friend WithEvents txtSugaCd As System.Windows.Forms.TextBox
    Friend WithEvents lblSugaCd As System.Windows.Forms.Label
    Friend WithEvents txtInsuGbn As System.Windows.Forms.TextBox
    Friend WithEvents lblInsuGbn As System.Windows.Forms.Label
    Friend WithEvents txtTOrdCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTOrdCd As System.Windows.Forms.Label
    Friend WithEvents txtBconeYN As System.Windows.Forms.CheckBox
    Friend WithEvents chkOReqItem4 As System.Windows.Forms.CheckBox
    Friend WithEvents lblOReqItem As System.Windows.Forms.Label
    Friend WithEvents lblTOrdgbn As System.Windows.Forms.Label
    Friend WithEvents chkOReqItem3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkOReqItem1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkOReqItem2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkPtGbn As System.Windows.Forms.CheckBox
    Friend WithEvents chkPoctYN As System.Windows.Forms.CheckBox
    Friend WithEvents chkCtGbn As System.Windows.Forms.CheckBox
    Friend WithEvents btnExeDay As System.Windows.Forms.Button
    Friend WithEvents chkExeDay7 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay1 As System.Windows.Forms.CheckBox
    Friend WithEvents lblExeDay As System.Windows.Forms.Label
    Friend WithEvents cboSlipNmD As System.Windows.Forms.ComboBox
    Friend WithEvents txtSlipCd As System.Windows.Forms.TextBox
    Friend WithEvents lblSlipCd As System.Windows.Forms.Label
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents btnReg_dispseql As System.Windows.Forms.Button
    Friend WithEvents chkSpcGbn As System.Windows.Forms.CheckBox
    Friend WithEvents btnCdHelp_spc As System.Windows.Forms.Button
    Friend WithEvents txtSelSpc As System.Windows.Forms.TextBox
    Friend WithEvents btnClear_spc As System.Windows.Forms.Button
    Friend WithEvents txtTLisCd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnReg_dispseqO As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTestInfo3 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTestInfo2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTestInfo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblAddModeInfo As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents lblGuide2 As System.Windows.Forms.Label
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents pnlBotton As System.Windows.Forms.Panel
    Friend WithEvents rdoWorkOpt2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoWorkOpt1 As System.Windows.Forms.RadioButton
    Friend WithEvents btnChgUseDt As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFieldVal As System.Windows.Forms.TextBox
    Friend WithEvents lblFieldNm As System.Windows.Forms.Label
    Friend WithEvents cboTordSlip_q As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboFilter As System.Windows.Forms.ComboBox
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents cboPartSlip As System.Windows.Forms.ComboBox
    Friend WithEvents cboPSGbn As System.Windows.Forms.ComboBox
    Friend WithEvents cboBccls_q As System.Windows.Forms.ComboBox
    Friend WithEvents chkNotSpc As System.Windows.Forms.CheckBox
    Friend WithEvents rdoSOpt1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSOpt0 As System.Windows.Forms.RadioButton
    Friend WithEvents spdCdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents cboOps As System.Windows.Forms.ComboBox

    Private Const mcAgeRefMaxRow As Integer = 40
    Friend WithEvents spdList_spc As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkGrpRstYn As System.Windows.Forms.CheckBox
    Friend WithEvents lblCWarning As System.Windows.Forms.Label
    Friend WithEvents pnlOrdCont As System.Windows.Forms.Panel
    Friend WithEvents spdOrdCont As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnOrdContExit As System.Windows.Forms.Button
    Friend WithEvents btnOrdContAdd As System.Windows.Forms.Button
    Friend WithEvents btnOrdContDel As System.Windows.Forms.Button
    Friend WithEvents btnOrdContView As System.Windows.Forms.Button
    Friend WithEvents chkErGbn2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkOrder As System.Windows.Forms.CheckBox
    Friend WithEvents rdoSort_ocs As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSort_lis As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSort_spc As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents rdoSort_test As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtDefrst As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboBldGbn As System.Windows.Forms.ComboBox
    Friend WithEvents chkSignRptYn As System.Windows.Forms.CheckBox
    Friend WithEvents cboFixRptusr As System.Windows.Forms.ComboBox
    Friend WithEvents lblFErRptMi As System.Windows.Forms.Label
    Friend WithEvents lblPErRptMi As System.Windows.Forms.Label
    Friend WithEvents cboFErRptMi As System.Windows.Forms.ComboBox
    Friend WithEvents txtFErRptMI As System.Windows.Forms.TextBox
    Friend WithEvents cboPErRptMi As System.Windows.Forms.ComboBox
    Friend WithEvents txtPErRptMi As System.Windows.Forms.TextBox
    Friend WithEvents txtCprtcd As System.Windows.Forms.TextBox
    Friend WithEvents chkCtGbn_q As System.Windows.Forms.CheckBox
    Friend WithEvents btnRefExcel As CButtonLib.CButton
    Friend WithEvents spdRef As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkFwgbn As System.Windows.Forms.CheckBox
    Friend WithEvents chkCWarning As System.Windows.Forms.CheckBox
    Friend WithEvents txtTestInfo4 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboErAlramMi As System.Windows.Forms.ComboBox
    Friend WithEvents cboAlramMi As System.Windows.Forms.ComboBox
    Friend WithEvents txtAlramTEr As System.Windows.Forms.TextBox
    Friend WithEvents txtAlramT As System.Windows.Forms.TextBox
    Friend WithEvents cboRPTITEMER As System.Windows.Forms.ComboBox
    Friend WithEvents cboRPTITEM As System.Windows.Forms.ComboBox
    Friend WithEvents lblErRptTime As System.Windows.Forms.Label
    Friend WithEvents lblRptTIME As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents BtnTestChg As System.Windows.Forms.Button
    Friend WithEvents CboRequest As System.Windows.Forms.ComboBox
    Friend WithEvents cboEnforcement As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTestInfo5 As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkReq2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkReq1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkReq0 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnf3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnf2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnf1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnf0 As System.Windows.Forms.CheckBox
    Private miLeaveRow As Integer = 0

    Public Sub sbMinimize()
        Me.WindowState = Windows.Forms.FormWindowState.Minimized
    End Sub


    Private Sub sbColHidden_spdcdlist()

        With Me.spdCdList

            If Me.chkNotSpc.Checked Then
                .set_ColWidth(.GetColFromID("tcd"), 6)
                .Col = .GetColFromID("spcnmd") : .ColHidden = True
                .Col = .GetColFromID("tubenmd") : .ColHidden = True
                .Col = .GetColFromID("dspccd1") : .ColHidden = True
                .Col = .GetColFromID("exlabnmd") : .ColHidden = True
            Else

                .set_ColWidth(.GetColFromID("tcd"), 9.75)
                .Col = .GetColFromID("spcnmd") : .ColHidden = False : .set_ColWidth(.GetColFromID("spcnmd"), 10)
                .Col = .GetColFromID("tubenmd") : .ColHidden = False : .set_ColWidth(.GetColFromID("tubenmd"), 20)
                .Col = .GetColFromID("dspccd1") : .ColHidden = False : .set_ColWidth(.GetColFromID("tubenmd"), 5)
                .Col = .GetColFromID("exlabnmd") : .ColHidden = False : .set_ColWidth(.GetColFromID("exlabnmd"), 14)
            End If
        End With
    End Sub

    Private Sub sbDisplayChgUseDt(ByVal riCurRow As Integer)
        Dim sFn As String = "Sub sbDisplayChgUseDt"

        If riCurRow < 1 Then Return

        Try
            '> 전체자료 조회 시에 관리자에 한해서 사용(시작 또는 종료)일시 변경가능하도록 함
            If USER_INFO.USRLVL = "S" Then
                If rdoSOpt1.Checked Then
                    With Me.spdCdList
                        If .GetColFromID("usdt") + .GetColFromID("uedt") > 0 Then
                            .Col = 1 : .Row = riCurRow

                            'if 사용종료 then 사용종료일시 변경 else 사용시작일시 변경
                            If .ForeColor = Drawing.Color.Red Then
                                Me.btnChgUseDt.Text = Me.btnChgUseDt.Text.Replace("사용", "종료").Replace("시작", "종료")
                                Me.btnChgUseDt.Tag = "UEDT"
                            Else
                                Me.btnChgUseDt.Text = Me.btnChgUseDt.Text.Replace("사용", "시작").Replace("종료", "시작")
                                Me.btnChgUseDt.Tag = "USDT"
                            End If

                            Me.btnChgUseDt.Visible = True
                        Else
                            Me.btnChgUseDt.Visible = False
                        End If
                    End With
                Else
                    Me.btnChgUseDt.Visible = False
                End If
            End If

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally

        End Try
    End Sub

    Private Sub sbUpdateCdList_Test()
        Dim sFn As String = "Private Sub sbUpdateCdList_Test()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            Dim iRow As Integer = 0


            With spdCdList
                For ix As Integer = 1 To .MaxRows

                    .Row = ix
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                    .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text

                    If Me.txtTestCd.Text = sTestCd And Me.txtSpcCd.Text = sSpcCd Then
                        iRow = ix
                        Exit For
                    End If
                Next

                If iRow < 1 Then Return

                .Row = iRow

                .Col = .GetColFromID("tnmd") : .Text = Me.txtTNmD.Text
                .Col = .GetColFromID("spcnmd") : .Text = Ctrl.Get_Name(Me.cboSpcNmD)
                .Col = .GetColFromID("tubenmd") : .Text = Me.cboTubeNmD.SelectedItem.ToString
                .Col = .GetColFromID("tcdgbn") : .Text = Ctrl.Get_Code(Me.cboTCdGbn)

                .Col = .GetColFromID("tordslipnm")
                Dim sBuf As String = ""
                sBuf += Me.cboTOrdSlip.SelectedItem.ToString
                sBuf += "(" + IIf(Me.txtDispSeqO.Text.Trim = "", "0", Me.txtDispSeqO.Text.Trim).ToString.PadLeft(3, "0"c) + ")"
                .Text = sBuf
                Call .SetText(.Col, .Row, .Text)

                .Col = .GetColFromID("ordhide") : .Text = IIf(Me.chkOrdHIde.Checked, "X", "").ToString
                .Col = .GetColFromID("tordcd") : .Text = Me.txtTOrdCd.Text
                .Col = .GetColFromID("sugacd") : .Text = Me.txtSugaCd.Text
                .Col = .GetColFromID("dspccd1") : .Text = Me.txtDSpcCdO.Text
                .Col = .GetColFromID("tliscd") : .Text = Me.txtTLisCd.Text

                .Col = .GetColFromID("bcclsnmd") : .Text = Me.cboBcclsNmd.SelectedItem.ToString
                .Col = .GetColFromID("slipnmd") : .Text = Me.cboSlipNmD.SelectedItem.ToString
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbAddTest(ByVal iRow As Integer)
        Dim sFn As String = ""

        Try
            Dim sTestCd As String = "", sSpcCd As String = "", sTNmD As String = "", sCdGbn As String = "", sTitleYN As String = ""

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iRow : sTestCD = .Text
                .Col = .GetColFromID("spccd") : .Row = iRow : sSpccd = .Text
                .Col = .GetColFromID("tnmd") : .Row = iRow : sTNmD = .Text

                If Me.chkNotSpc.Checked Then
                    If spdList_spc.ActiveRow < 1 Then Return
                    spdList_spc.Row = spdList_spc.ActiveRow
                    spdList_spc.Col = spdList_spc.GetColFromID("spccd") : sSpcCd = spdList_spc.Text
                End If

                If Me.chkAddModeD.Checked Then
                    .Col = .GetColFromID("tcdgbn") : .Row = iRow : sCdGbn = .Text

                    Select Case fnGetTCdGbn()
                        Case "B"
                            If sCdGbn = "G" Or sCdGbn = "B" Or sCdGbn = "C" Then
                                Dim sTmp As String = CType(IIf(sCdGbn = "G", "Group Code", IIf(sCdGbn = "B", "Battery Code", IIf(sCdGbn = "C", "Child Of Sub. Code", ""))), String)

                                MsgBox("Single Code와 Parent Of Sub. Code 이외에는 세부검사로 추가할 수 없습니다!!" _
                                        + vbCrLf + vbCrLf + sTNmD + "(" + sTestCd.PadRight(8, " "c) + sSpcCd + ")는 " _
                                        + sTmp + "입니다.", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                        Case "G"
                            If sCdGbn = "G" Or sCdGbn = "C" Then
                                Dim sTmp As String = CType(IIf(sCdGbn = "G", "Group Code", IIf(sCdGbn = "B", "Battery Code", IIf(sCdGbn = "C", "Child Of Sub. Code", ""))), String)

                                MsgBox("Battery Code, Single Code, Parent Of Sub. Code 이외에는 세부검사로 추가할 수 없습니다!!" _
                                        + vbCrLf + vbCrLf + sTNmD + "(" + sTestCd.PadRight(8, " "c) + sSpcCd + ")는 " _
                                        + sTmp + "입니다.", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                        Case Else
                            Exit Sub
                    End Select
                ElseIf Me.chkAddModeR.Checked Then
                    .Col = .GetColFromID("titleyn") : .Row = iRow : sTitleYN = .Text
                    .Col = .GetColFromID("tcdgbn") : .Row = iRow : sCdGbn = .Text

                    If sTitleYN = "1" And sCdGbn <> "B" Then
                        MsgBox("결과 입력이 불가능한 TITLE로 설정된 검사를 추가할 수 없습니다!!", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                End If

                sbAddTest_spdTest(sTestCd, sSpcCd, sTNmD)
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbJudgeAddTest(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            Dim sTestCd As String = "", sSpcCd As String, sTNm As String = "", sSpcNm As String = "", sUsDt As String = "", sUeDt As String = ""
            Dim iCopyMode As Integer = 0
            Dim sMsg As String = ""


            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : sTestCd = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : sSpcCd = .Text
                .Col = .GetColFromID("tnmd") : .Row = iCurRow : sTNm = .Text
                .Col = .GetColFromID("spcnmd") : .Row = iCurRow : sSpcNm = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : sUsDt = .Text.Replace("-", "").Replace(":", "").Replace(" ", "")
                .Col = .GetColFromID("uedt") : .Row = iCurRow : sUeDt = .Text

                If rdoWorkOpt2.Checked Then
                    '### 신규
                    If Me.tclTest.SelectedTab.Name.EndsWith("2") Then
                        sMsg = ""
                        sMsg += "검사코드 : " + sTestCd + vbCrLf
                        sMsg += "검체코드 : " + sSpcCd + vbCrLf
                        sMsg += "검사명   : " + sTNm + vbCrLf
                        sMsg += "검체명   : " + sSpcNm + vbCrLf
                        sMsg += vbCrLf
                        sMsg += "이 검사의 [결과관련정보]를 현재 화면으로 복사하시겠습니까?"

                        If MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                        sbJudgeAddTest_Ref(iCurRow)

                        Exit Sub
                    ElseIf Me.tclTest.SelectedTab.Name.EndsWith("3") Then
                        sMsg = ""
                        sMsg += "검사코드 : " + sTestCd + vbCrLf
                        sMsg += "검체코드 : " + sSpcCd + vbCrLf
                        sMsg += "검사명   : " + sTNm + vbCrLf
                        sMsg += "검체명   : " + sSpcNm + vbCrLf
                        sMsg += vbCrLf
                        sMsg += "이 검사의 [세부/참조검사정보]를 현재 화면으로 복사하시겠습니까?"

                        If MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                        If Me.chkAddModeD.Checked Then
                            sbJudgeAddTest_DTest(iCurRow)
                        Else
                            sbJudgeAddTest_RTest(iCurRow)
                        End If

                        Exit Sub
                    End If

                    sMsg = ""
                    sMsg += "검사코드 : " + sTestCd + vbCrLf
                    sMsg += "검체코드 : " + sSpcCd + vbCrLf
                    sMsg += "검사명   : " + sTNm + vbCrLf
                    sMsg += "검체명   : " + sSpcNm + vbCrLf
                    sMsg += vbCrLf
                    sMsg += "이 검사의 [검사기본정보]를 신규 등록 화면으로 복사하시겠습니까?"

                    '신규 등록 화면과 선택한 검사의 검사코드가 다른 경우
                    If Not Me.txtTestCd.Text.Trim = sTestCd Then iCopyMode = 0

                    'New 신규
                    If Me.txtTestCd.Text.Trim = "" Then iCopyMode = 1

                    Select Case iCopyMode
                        Case 0
                            sMsg = ""
                            sMsg += "검사코드 : " + sTestCd + vbCrLf
                            sMsg += "검체코드 : " + sSpcCd + vbCrLf
                            sMsg += "검사명   : " + sTNm + vbCrLf
                            sMsg += "검체명   : " + sSpcNm + vbCrLf
                            sMsg += vbCrLf
                            sMsg += "이 검사의 정보와 신규 등록 화면에서 입력하신 검사코드가 다릅니다." + vbCrLf
                            sMsg += "[검사기본정보]의 부분내용만 복사하시겠습니까?"

                            Dim msgboxRst As MsgBoxResult = MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2)

                            If Not msgboxRst = MsgBoxResult.Yes Then Exit Sub

                            Me.sbDisplayCdDetail(sTestCd, sSpcCd, sUsDt, , 1)

                        Case 1
                            sMsg = ""
                            sMsg += "검사코드 : " + sTestCd + vbCrLf
                            sMsg += "검체코드 : " + sSpcCd + vbCrLf
                            sMsg += "검사명   : " + sTNm + vbCrLf
                            sMsg += "검체명   : " + sSpcNm + vbCrLf
                            sMsg += vbCrLf
                            sMsg += "이 검사의 [검사기본정보]를 신규 등록 화면으로 복사하시겠습니까?"

                            Dim msgboxRst As MsgBoxResult = MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2)

                            If Not msgboxRst = MsgBoxResult.Yes Then Exit Sub

                            Dim lngBuf As Long = DateDiff(DateInterval.Second, CDate(sUsDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")), _
                                                            CDate(Me.txtUSDay.Text + " " + _
                                                            Format(Me.dtpUSTime.Value, "HH:mm:ss")))

                            Select Case lngBuf
                                Case Is < 0
                                    sMsg = ""
                                    sMsg += "선택하신 검사의 시작일시가 더 최근이라 복사할 수 없습니다." + vbCrLf
                                    sMsg += vbCrLf
                                    sMsg += "[검사기본정보]의 부분내용만 복사하시겠습니까?"

                                    msgboxRst = MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2)

                                    If Not msgboxRst = MsgBoxResult.Yes Then Exit Sub

                                    Me.sbDisplayCdDetail(sTestCd, sSpcCd, sUsDt, , 1)

                                Case 0
                                    sMsg = ""
                                    sMsg += "선택하신 검사의 시작일시가 서로 일치합니다." + vbCrLf
                                    sMsg += vbCrLf
                                    sMsg += "이런 경우는 관리자 작업 선택 ▶▶▶ [수정]에서 작업하십시요!!"

                                    MsgBox(sMsg, MsgBoxStyle.Information)

                                    Me.btnUE.Visible = False
                                    giClearKey = 1
                                    sbInitialize()
                                    giClearKey = 0

                                    Exit Sub

                                Case Is > 0
                                    Me.sbDisplayCdDetail(sTestCd, sSpcCd, sUsDt, sUeDt, 0)

                            End Select
                    End Select

                ElseIf rdoWorkOpt1.Checked Then
                    '### 수정
                    If Me.tclTest.SelectedTab.Name.EndsWith("2") Then
                        sMsg = ""
                        sMsg += "검사코드 : " + sTestCd + vbCrLf
                        sMsg += "검체코드 : " + sSpcCd + vbCrLf
                        sMsg += "검사명   : " + sTNm + vbCrLf
                        sMsg += "검체명   : " + sSpcNm + vbCrLf
                        sMsg += vbCrLf
                        sMsg += "이 검사의 [결과관련정보]를 현재 화면으로 복사하시겠습니까?"

                        If MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                        sbJudgeAddTest_Ref(iCurRow)

                        Exit Sub
                    ElseIf Me.tclTest.SelectedTab.Name.EndsWith("3") Then
                        sMsg = ""
                        sMsg += "검사코드 : " + sTestCd + vbCrLf
                        sMsg += "검체코드 : " + sSpcCd + vbCrLf
                        sMsg += "검사명   : " + sTNm + vbCrLf
                        sMsg += "검체명   : " + sSpcNm + vbCrLf
                        sMsg += vbCrLf
                        sMsg += "이 검사의 [세부/참조검사정보]를 현재 화면으로 복사하시겠습니까?"

                        If MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                        If Me.chkAddModeD.Checked Then
                            sbJudgeAddTest_DTest(iCurRow)
                        Else
                            sbJudgeAddTest_RTest(iCurRow)
                        End If

                        Exit Sub
                    End If
                End If
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbJudgeAddTest_DTest(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            Dim sTestCd As String = "", sSpcCd As String, sUsDt As String

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : sTestCd = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : sSpcCd = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : sUsDt = .Text.Replace("-", "").Replace(":", "").Replace(" ", "")

                sbDisplayCdDetail_Add_Dtest(sTestCd, sSpcCd, sUsDt)
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbJudgeAddTest_RTest(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            Dim sTestCd As String = "", sSpcCd As String, sTNm As String = "", sSpcNm As String = "", sUSDT As String = "", sUEDT As String = ""
            Dim sMsg As String = ""

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : sTestCd = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : sSpcCd = .Text
                .Col = .GetColFromID("tnmd") : .Row = iCurRow : sTNm = .Text
                .Col = .GetColFromID("spcnmd") : .Row = iCurRow : sSpcNm = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : sUSDT = .Text.Replace("-", "").Replace(":", "").Replace(" ", "")
                .Col = .GetColFromID("uedt") : .Row = iCurRow : sUEDT = .Text

                sbDisplayCdDetail_Add_Rtest(sTestCd, sSpcCd, sUSDT)
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbJudgeAddTest_Ref(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            Dim sTestCd As String = "", sSpcCd As String, sUsDt As String

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : sTestCd = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : sSpcCd = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : sUsDt = .Text

                sbDisplayCdDetail_Test_Partial(sTestCd, sSpcCd, sUsDt, 1)
                sbDisplayCdDetail_Test_AgeRef(sTestCd, sSpcCd, sUsDt)
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Test(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : Dim sSpcCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text.Replace("-", "").Replace(":", "").Replace(" ", "")
                .Col = .GetColFromID("uedt") : .Row = iCurRow : Dim sUeDt As String = .Text

                If Me.chkNotSpc.Checked Then
                    sbDisplay_spc(sTestCd, sUsDt)
                Else
                    sbDisplayCdDetail(sTestCd, sSpcCd, sUsDt, sUeDt)
                End If
            End With

            '조회 또는 수정의 경우
            If rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayColumnNm(ByVal riCol As Integer)
        Dim sColNm As String = ""

        With Me.spdCdList
            .Col = riCol : .Row = 0 : sColNm = .Text
        End With

        Me.lblFieldNm.Text = sColNm
        Me.lblFieldNm.Tag = riCol
    End Sub

    Private Sub sbFindList(ByVal rsBuf As String)
        Dim sFn As String = "Sub sbFindList"

        Try
            If Me.lblFieldNm.Tag Is Nothing Then Return
            If IsNumeric(Me.lblFieldNm.Tag) = False Then Return

            Dim iCol As Integer = Convert.ToInt32(Val(Me.lblFieldNm.Tag))

            With Me.spdCdList
                'If rsBuf = "" Then Return

                Dim iFindRow As Integer = .SearchCol(iCol, 1, .MaxRows, rsBuf, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)

                Do
                    Dim sCd As String = Ctrl.Get_Code(Me.spdCdList, iCol, iFindRow)

                    If sCd.StartsWith(rsBuf) Then
                        Exit Do
                    Else
                        iFindRow = .SearchCol(iCol, iFindRow, .MaxRows, rsBuf, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)
                    End If
                Loop While iFindRow > 0

                If iFindRow < 0 Then iFindRow = 0

                If iFindRow < 1 Then Return

                If iCol = 1 Then
                    Me.spdCdList.Col = iCol
                Else
                    Me.spdCdList.Col = iCol - 1
                End If

                Me.spdCdList.Row = iFindRow
                Me.spdCdList.Action = FPSpreadADO.ActionConstants.ActionGotoCell
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally

        End Try
    End Sub

    Private Sub sbDisplay_Test()
        Dim sFn As String = "sbDisplay_Test"

        Try
            Dim dt As New DataTable
            Dim iCol As Integer = 0

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim sWhere As String = ""

            If Ctrl.Get_Code(Me.cboBccls_q) <> "" Then sWhere += " AND bcclscd = '" + Ctrl.Get_Code(Me.cboBccls_q) + "'"
            If Ctrl.Get_Code(Me.cboTordSlip_q) <> "" Then sWhere += " AND tordslip = '" + Ctrl.Get_Code(Me.cboTordSlip_q) + "'"
            If Ctrl.Get_Code(Me.cboPartSlip) <> "" Then

                If Me.cboPSGbn.Text = "부서" Then
                    sWhere += " AND partcd = '" + Ctrl.Get_Code(Me.cboPartSlip.Text) + "'"
                Else
                    sWhere += " AND partcd = '" + Ctrl.Get_Code(Me.cboPartSlip.Text).Substring(0, 1) + "'"
                    sWhere += " AND slipcd = '" + Ctrl.Get_Code(Me.cboPartSlip.Text).Substring(1, 1) + "'"
                End If
            End If

            If Me.chkOrder.Checked Then
                sWhere += " AND tcdgbn IN ('G', 'B', 'S', 'P')"
                sWhere += " AND NVL(ordhide, '0') = '0'"
            End If

            If Me.chkCtGbn_q.Checked Then sWhere += " AND NVL(ctgbn, '0') = '1'"

            If Me.txtFilter.Text <> "" Then
                Select Case Me.cboFilter.Text.Replace(" ", "")
                    Case "검사코드" : sWhere += " AND testcd"
                    Case "검체코드" : sWhere += " AND spccd"
                    Case "처방코드" : sWhere += " AND tordcd"
                    Case "결과코드" : sWhere += " AND tliscd"
                    Case "검사구분" : sWhere += " AND tcdgbn"
                    Case "검사명" : sWhere += " AND tnmd"
                    Case "위탁기관명" : sWhere += " AND exlabnmd"
                End Select

                Select Case Me.cboOps.Text
                    Case "LIKE *" : sWhere += " LIKE '" + Me.txtFilter.Text + "%'"
                    Case "* LIKE" : sWhere += " LIKE '%" + Me.txtFilter.Text + "'"
                    Case "LIKE *" : sWhere += " LIKE '" + Me.txtFilter.Text + "%'"
                    Case "* LIKE *" : sWhere += " LIKE '%" + Me.txtFilter.Text + "%'"
                    Case "IN" : sWhere += " " + Me.cboOps.Text + " ('" + Me.txtFilter.Text.Replace(",", "','") + "')"
                    Case Else : sWhere += " " + Me.cboOps.Text + " '" + Me.txtFilter.Text + "'"
                End Select
            End If

            If sWhere <> "" Then sWhere = sWhere.Substring(4).Trim

            If Me.chkNotSpc.Checked Then
                dt = mo_DAF.GetTestInfo_NotSpc(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), sWhere)
            Else
                dt = mo_DAF.GetTestInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), sWhere)
            End If

            If Me.rdoSort_spc.Checked Then
                Dim a_dr As DataRow() = dt.Select("", "spccd, testcd")
                dt = Fn.ChangeToDataTable(a_dr)
            ElseIf Me.rdoSort_lis.Checked Then
                Dim a_dr As DataRow() = dt.Select("", "slipnmd, dispseql, testcd, spccd")
                dt = Fn.ChangeToDataTable(a_dr)
            ElseIf Me.rdoSort_ocs.Checked Then
                Dim a_dr As DataRow() = dt.Select("", "tordslipnm, dispseqo, testcd, spccd")
                dt = Fn.ChangeToDataTable(a_dr)
            End If

            If dt.Rows.Count < 0 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                        .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 14
                        .BlockMode = True : .ForeColor = System.Drawing.Color.Red : .BlockMode = False
                    Else
                        .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 14
                        .BlockMode = True : .ForeColor = System.Drawing.Color.Black : .BlockMode = False
                    End If

                    If i > 50 Then
                        .ReDraw = True
                    End If
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub sbUSDT_New()
        If Me.dtpUSDay.Enabled Then Exit Sub

        Me.txtUSDay.ReadOnly = False : Me.dtpUSDay.Enabled = True : Me.dtpUSTime.Enabled = True
        Me.txtTestCd.ReadOnly = False
        Me.txtSpcCd.ReadOnly = False : Me.cboSpcNmD.Enabled = True : Me.cboSpcNmD.Items.Clear()
        Me.btnUE.Visible = False
        Me.chkSpcGbn.Visible = True
        Me.sbSetNewUSDT()

    End Sub

    Private Sub sbUSDT_Disable()

        Me.txtUSDay.ReadOnly = True : Me.txtUSDay.BackColor = Drawing.Color.White : Me.dtpUSDay.Enabled = False : Me.dtpUSTime.Enabled = False
        Me.txtTestCd.ReadOnly = True : Me.txtTestCd.BackColor = Drawing.Color.White
        Me.txtSpcCd.ReadOnly = True : Me.txtSpcCd.BackColor = Drawing.Color.White : Me.cboSpcNmD.Enabled = False

        If Me.rdoSOpt0.Checked = True Then
            Me.btnUE.Visible = True
        Else
            Me.btnUE.Visible = False
        End If

    End Sub

    Private Sub sbDisplay_bccls()
        Dim sFn As String = "Sub sbDisplay_bccls"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Bccls_List()

            Me.cboBccls_q.Items.Clear()
            Me.cboBccls_q.Items.Add("[  ] 전체")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboBccls_q.Items.Add("[" + dt.Rows(ix).Item("bcclscd").ToString.Trim + "] " + dt.Rows(ix).Item("bcclsnmd").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_tordslip()
        Dim sFn As String = "Sub sbDisplay_tordslip"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TOrdSlip()

            Me.cboTordSlip_q.Items.Clear()
            Me.cboTordSlip_q.Items.Add("[  ] 전체")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboTordSlip_q.Items.Add(dt.Rows(ix).Item("tordslipnm").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_fixrptusr()
        Dim sFn As String = "Sub sbDisplay_fixrptusr"

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_RptDr_List()

            Me.cboFixRptusr.Items.Clear()
            Me.cboFixRptusr.Items.Add("")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboFixRptusr.Items.Add("[" + dt.Rows(ix).Item("usrid").ToString + "] " + dt.Rows(ix).Item("usrnm").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_part()
        Dim sFn As String = "Sub sbDisplay_part"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Part_List()

            Me.cboPartSlip.Items.Clear()
            Me.cboPartSlip.Items.Add("[ ] 전체")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_slip()
        Dim sFn As String = "Sub sbDisplay_slip"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()

            Me.cboPartSlip.Items.Clear()
            Me.cboPartSlip.Items.Add("[  ] 전체")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub


    Private Sub sbDisplay_spc(ByVal rsTestcd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Sub sbDisplay_spc"

        Try
            Me.spdList_spc.MaxRows = 0

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TestWithSpc_List(rsTestcd, rsUsDt)

            If dt.Rows.Count < 1 Then Return

            With spdList_spc
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString.Trim
                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString.Trim
                    .Col = .GetColFromID("usdt") : .Text = dt.Rows(ix).Item("usdt").ToString.Trim
                    .Col = .GetColFromID("uedt") : .Text = dt.Rows(ix).Item("uedt").ToString.Trim
                Next
            End With

            Me.spdList_spc_ClickEvent(spdList_spc, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1))

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbGetExcel_f60()
        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim dt As New DataTable
        Dim iLine As Integer = 1

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open("c:\as\검사코드_화학.xlsx")

            xlsWkS = CType(xlsWkB.Sheets("화학"), Excel.Worksheet)

            Do While True
                iLine += 1
                If xlsWkS.Range("A" + CStr(iLine)).Value Is Nothing Then Exit Do

                Dim sTestCd As String = xlsWkS.Range("A" + CStr(iLine)).Value.ToString
                Dim sSpcCd As String = xlsWkS.Range("B" + CStr(iLine)).Value.ToString
                'sSpcCd = sSpcCd.PadLeft(4, "0"c)

                If sTestCd.Length > 7 Then sTestCd = sTestCd.Substring(0, 7)

                Dim sUsdt As String = "20110901000000" 'xlsWkS.Range("C" + CStr(iLine)).Value.ToString
                Dim sTnm As String = xlsWkS.Range("D" + CStr(iLine)).Value.ToString
                Dim sTnms As String = xlsWkS.Range("D" + CStr(iLine)).Value.ToString
                Dim sTnmd As String = xlsWkS.Range("D" + CStr(iLine)).Value.ToString
                Dim sTnmp As String = xlsWkS.Range("D" + CStr(iLine)).Value.ToString
                Dim sTnmbp As String = ""
                If xlsWkS.Range("G" + CStr(iLine)).Value IsNot Nothing Then
                    sTnmbp = xlsWkS.Range("G" + CStr(iLine)).Value.ToString
                End If

                If sTnmbp.Length > 5 Then sTnmbp = sTnmbp.Substring(0, 5)
                If sTnm.Length > 70 Then sTnm = sTnm.Substring(0, 70)
                If sTnms.Length > 70 Then sTnmd = sTnm.Substring(0, 70)
                If sTnmd.Length > 70 Then sTnmd = sTnm.Substring(0, 70)
                If sTnmp.Length > 70 Then sTnmp = sTnm.Substring(0, 70)

                Dim sLisCd As String = xlsWkS.Range("C" + CStr(iLine)).Value.ToString
                Dim sTordCd As String = xlsWkS.Range("C" + CStr(iLine)).Value.ToString

                Dim sExLabCd As String = "" ' xlsWkS.Range("L" + CStr(iLine)).Value.ToString
                Dim sExLabYn As String = "0" 'xlsWkS.Range("K" + CStr(iLine)).Value.ToString
                If sExLabYn = "1" Then
                    sExLabCd = sExLabCd.PadLeft(3, "0"c)
                Else
                    sExLabCd = ""
                End If

                Dim sExeDay As String = xlsWkS.Range("M" + CStr(iLine)).Value.ToString.Replace("Y", "1").Replace("N", "0").PadLeft(7, "0"c)
                Dim sTitleYn As String = "0" 'xlsWkS.Range("N" + CStr(iLine)).Value.ToString
                Dim sSeqtyn As String = xlsWkS.Range("N" + CStr(iLine)).Value.ToString
                Dim sSeqtmi As String = "" 'xlsWkS.Range("P" + CStr(iLine)).Value.ToString
                Dim sMbtType As String = "0" 'xlsWkS.Range("Q" + CStr(iLine)).Value.ToString
                Dim sTubeCd As String = ""
                If xlsWkS.Range("Q" + CStr(iLine)).Value IsNot Nothing Then
                    sTubeCd = xlsWkS.Range("R" + CStr(iLine)).Value.ToString
                End If
                If sTubeCd = "" Then sTubeCd = "01"
                sTubeCd = sTubeCd.PadLeft(2, "0"c)

                Dim sSpcvol As String = ""

                If xlsWkS.Range("R" + CStr(iLine)).Value IsNot Nothing Then
                    sSpcvol = xlsWkS.Range("R" + CStr(iLine)).Value.ToString
                End If
                If sSpcvol = "0" Then sSpcvol = ""

                Dim sBcclsCd As String = Ctrl.Get_Code(xlsWkS.Range("I" + CStr(iLine)).Value.ToString)
                If sBcclsCd.Length = 1 Then sBcclsCd = "C" + sBcclsCd

                Dim sPartCd As String = ""
                If xlsWkS.Range("J" + CStr(iLine)).Value IsNot Nothing Then
                    sPartCd = Ctrl.Get_Code(xlsWkS.Range("J" + CStr(iLine)).Value.ToString)
                End If
                If sPartCd = "" Then sPartCd = "C"
                If sPartCd.Length = 2 Then sPartCd = sPartCd.Substring(0, 1)

                Dim sSlipCd As String = ""
                If xlsWkS.Range("J" + CStr(iLine)).Value IsNot Nothing Then
                    sSlipCd = Ctrl.Get_Code(xlsWkS.Range("J" + CStr(iLine)).Value.ToString)
                End If
                If sSlipCd = "" Then sSlipCd = "1"
                If sSlipCd.Length = 2 Then sSlipCd = sSlipCd.Substring(1, 1)

                Dim sRstType As String = xlsWkS.Range("T" + CStr(iLine)).Value.ToString
                Dim sRstUnit As String = xlsWkS.Range("X" + CStr(iLine)).Value.ToString
                If xlsWkS.Range("U" + CStr(iLine)).Value IsNot Nothing Then
                    sRstUnit = xlsWkS.Range("U" + CStr(iLine)).Value.ToString
                End If
                If sRstUnit = "NULL" Then sRstUnit = ""

                Dim sRefGbn As String = xlsWkS.Range("V" + CStr(iLine)).Value.ToString
                Dim sJudgType As String = ""
                If xlsWkS.Range("W" + CStr(iLine)).Value IsNot Nothing Then
                    sJudgType = xlsWkS.Range("W" + CStr(iLine)).Value.ToString
                End If

                Dim sPanicGbn As String = xlsWkS.Range("X" + CStr(iLine)).Value.ToString
                Dim sPanicL As String = ""
                If xlsWkS.Range("Y" + CStr(iLine)).Value IsNot Nothing Then
                    sPanicL = xlsWkS.Range("Y" + CStr(iLine)).Value.ToString
                End If
                Dim sPanicH As String = ""
                If xlsWkS.Range("Z" + CStr(iLine)).Value IsNot Nothing Then
                    sPanicH = xlsWkS.Range("Z" + CStr(iLine)).Value.ToString
                End If
                Dim sDeltaGbn As String = ""
                If xlsWkS.Range("AA" + CStr(iLine)).Value IsNot Nothing Then
                    xlsWkS.Range("AA" + CStr(iLine)).Value.ToString()
                End If

                Dim sDeltaL As String = ""
                If xlsWkS.Range("AB" + CStr(iLine)).Value IsNot Nothing Then
                    sDeltaL = xlsWkS.Range("AB" + CStr(iLine)).Value.ToString
                End If

                Dim sDeltaH As String = ""
                If xlsWkS.Range("AC" + CStr(iLine)).Value IsNot Nothing Then
                    sDeltaH = xlsWkS.Range("AC" + CStr(iLine)).Value.ToString
                End If

                Dim sDeltaDay As String = ""
                If xlsWkS.Range("AD" + CStr(iLine)).Value IsNot Nothing Then
                    sDeltaDay = xlsWkS.Range("AD" + CStr(iLine)).Value.ToString
                End If

                Dim sTOrdSlip As String = Ctrl.Get_Code(xlsWkS.Range("AE" + CStr(iLine)).Value.ToString)
                Dim sTCdGbn As String = ""
                If xlsWkS.Range("H" + CStr(iLine)).Value IsNot Nothing Then
                    sTCdGbn = xlsWkS.Range("H" + CStr(iLine)).Value.ToString
                End If

                If sTestCd.Length > 6 Then sTCdGbn = "C"


                dt = mo_DAF.GetTestCdInfo_xls("lf060m", sTestCd, sSpcCd, sUsdt, "", "")
                'If iLine = 325 Then MsgBox("A")

                If dt.Rows.Count < 1 Then

                    Dim it60 As New LISAPP.ItemTableCollection
                    Dim it61 As New LISAPP.ItemTableCollection
                    Dim it62 As New LISAPP.ItemTableCollection
                    Dim it63 As New LISAPP.ItemTableCollection
                    Dim it64 As New LISAPP.ItemTableCollection

                    With it60
                        .SetItemTable("TESTCD", 1, 1, sTestCd)
                        .SetItemTable("SPCCD", 2, 1, sSpcCd)
                        .SetItemTable("USDT", 3, 1, sUsdt)

                        .SetItemTable("UEDT", 4, 1, "30000101000000")

                        .SetItemTable("REGDT", 5, 1, Format(Now, "yyyyMMddHHmmss").ToString)
                        .SetItemTable("REGID", 6, 1, USER_INFO.USRID)
                        .SetItemTable("REGIP", 7, 1, USER_INFO.LOCALIP)

                        .SetItemTable("TNM", 8, 1, sTnm)
                        .SetItemTable("TNMS", 9, 1, sTnms)
                        .SetItemTable("TNMD", 10, 1, sTnmd)
                        .SetItemTable("TNMP", 11, 1, sTnmp)
                        .SetItemTable("TNMBP", 12, 1, sTnmbp)
                        .SetItemTable("TCDGBN", 13, 1, sTCdGbn)
                        .SetItemTable("TORDCD", 14, 1, sTordCd)
                        .SetItemTable("EXLABYN", 15, 1, sExLabYn)
                        .SetItemTable("EXLABCD", 16, 1, sExLabCd)
                        .SetItemTable("EXEDAY", 17, 1, sExeDay)
                        .SetItemTable("SEQTYN", 18, 1, sSeqtyn)
                        .SetItemTable("SEQTMI", 19, 1, sSeqtmi)
                        .SetItemTable("MBTTYPE", 20, 1, sMbtType)

                        .SetItemTable("DISPSEQL", 21, 1, "999")
                        .SetItemTable("DISPSEQO", 22, 1, "999")

                        .SetItemTable("TUBECD", 23, 1, sTubeCd.Substring(0, 2))
                        .SetItemTable("MINSPCVOL", 24, 1, sSpcvol)
                        .SetItemTable("BCCLSCD", 25, 1, sBcclsCd)
                        .SetItemTable("PARTCD", 26, 1, sPartCd)
                        .SetItemTable("SLIPCD", 27, 1, sSlipCd)
                        .SetItemTable("RSTTYPE", 28, 1, sRstType)
                        .SetItemTable("RSTUNIT", 29, 1, sRstUnit)
                        .SetItemTable("REFGBN", 30, 1, sRefGbn)
                        .SetItemTable("JUDGTYPE", 31, 1, sJudgType)

                        .SetItemTable("PANICGBN", 32, 1, sPanicGbn)
                        .SetItemTable("PANICL", 33, 1, sPanicL)
                        .SetItemTable("PANICH", 34, 1, sPanicH)
                        .SetItemTable("DELTAGBN", 35, 1, sDeltaGbn)
                        .SetItemTable("DELTAL", 36, 1, sDeltaL)
                        .SetItemTable("DELTAH", 37, 1, sDeltaH)
                        .SetItemTable("DELTADAY", 38, 1, sDeltaDay)
                        .SetItemTable("TORDSLIP", 39, 1, sTOrdSlip)
                        .SetItemTable("BCCNT", 40, 1, "1")
                        .SetItemTable("TLISCD", 41, 1, sLisCd)
                        .SetItemTable("DSPCCD1", 42, 1, sSpcCd)

                        If mo_DAF.TransTestInfo(it60, 0, it61, 2, it62, 2, it63, 2, it64, sTestCd, sSpcCd, sUsdt, True) Then
                        Else
                            MsgBox("등록오류(" + iLine.ToString + ")")
                        End If
                    End With
                    'Exit Do
                End If
            Loop

        Catch ex As Exception
            MsgBox(ex.Message + "(" + iLine.ToString + ")")

        Finally
            MsgBox("완료")
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub

    Private Sub sbGetExcel_f61()
        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim dt As New DataTable
        Dim iLine As Integer = 0

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open("c:\as\검사참고치.xls")

            xlsWkS = CType(xlsWkB.Sheets("sheet1"), Excel.Worksheet)
            iLine = 0

            Do While True
                iLine += 1

                If xlsWkS.Range("A" + CStr(iLine)).Value Is Nothing Then Exit Do

                Dim sTestCd As String = xlsWkS.Range("A" + CStr(iLine)).Value.ToString.Trim
                Dim sSpcCd As String = xlsWkS.Range("B" + CStr(iLine)).Value.ToString.Trim
                'sSpcCd = sSpcCd.PadLeft(4, "0"c)
                Dim sUsdt As String = xlsWkS.Range("C" + CStr(iLine)).Value.ToString.Trim
                Dim sRefSeq As String = xlsWkS.Range("D" + CStr(iLine)).Value.ToString.Trim
                Dim sAgeYmd As String = xlsWkS.Range("E" + CStr(iLine)).Value.ToString.Trim
                Dim sSage As String = xlsWkS.Range("F" + CStr(iLine)).Value.ToString.Trim
                Dim sSageS As String = xlsWkS.Range("G" + CStr(iLine)).Value.ToString.Trim
                Dim sSageC As String = xlsWkS.Range("H" + CStr(iLine)).Value.ToString.Trim
                Dim sEage As String = xlsWkS.Range("I" + CStr(iLine)).Value.ToString.Trim
                Dim sEages As String = xlsWkS.Range("J" + CStr(iLine)).Value.ToString.Trim
                Dim sEageC As String = xlsWkS.Range("K" + CStr(iLine)).Value.ToString.Trim
                If sSage = "NULL" Then sSage = ""
                If sSpcCd.Length > 4 Then sSpcCd = sSpcCd.Substring(0, 4)
                Dim sReflm As String = ""
                If xlsWkS.Range("L" + CStr(iLine)).Value IsNot Nothing Then
                    sReflm = xlsWkS.Range("L" + CStr(iLine)).Value.ToString
                End If

                Dim sReflms As String = ""
                If xlsWkS.Range("M" + CStr(iLine)).Value IsNot Nothing Then
                    sReflms = xlsWkS.Range("M" + CStr(iLine)).Value.ToString
                End If

                Dim sRefHm As String = ""
                If xlsWkS.Range("N" + CStr(iLine)).Value IsNot Nothing Then
                    sRefHm = xlsWkS.Range("N" + CStr(iLine)).Value.ToString
                End If

                Dim sRefHms As String = ""
                If xlsWkS.Range("O" + CStr(iLine)).Value IsNot Nothing Then
                    sRefHms = xlsWkS.Range("O" + CStr(iLine)).Value.ToString
                End If
                Dim sReflf As String = ""
                If xlsWkS.Range("P" + CStr(iLine)).Value IsNot Nothing Then
                    sReflf = xlsWkS.Range("P" + CStr(iLine)).Value.ToString
                End If
                Dim sReflfs As String = ""
                If xlsWkS.Range("Q" + CStr(iLine)).Value IsNot Nothing Then
                    sReflfs = xlsWkS.Range("Q" + CStr(iLine)).Value.ToString
                End If
                Dim sRefHf As String = ""
                If xlsWkS.Range("R" + CStr(iLine)).Value IsNot Nothing Then
                    sRefHf = xlsWkS.Range("R" + CStr(iLine)).Value.ToString
                End If

                Dim sRefHfs As String = ""
                If xlsWkS.Range("S" + CStr(iLine)).Value IsNot Nothing Then
                    sRefHfs = xlsWkS.Range("S" + CStr(iLine)).Value.ToString
                End If

                Dim sRefLt As String = ""
                If xlsWkS.Range("T" + CStr(iLine)).Value IsNot Nothing Then
                    sRefLt = xlsWkS.Range("T" + CStr(iLine)).Value.ToString
                End If

                dt = mo_DAF.GetTestCdInfo_xls("lf061m", sTestCd, sSpcCd, sUsdt, "", "")
                'If iLine = 325 Then MsgBox("A")

                If dt.Rows.Count < 1 And sSage <> "" And sTestCd.Length < 8 And sSpcCd.Length < 5 Then

                    Dim it60 As New LISAPP.ItemTableCollection
                    Dim it61 As New LISAPP.ItemTableCollection
                    Dim it62 As New LISAPP.ItemTableCollection
                    Dim it63 As New LISAPP.ItemTableCollection
                    Dim it64 As New LISAPP.ItemTableCollection

                    it61.SetItemTable("TESTCD", 1, 1, sTestCd)
                    it61.SetItemTable("SPCCD", 2, 1, sSpcCd)
                    it61.SetItemTable("USDT", 3, 1, sUsdt)
                    it61.SetItemTable("REFSEQ", 4, 1, sRefSeq)
                    it61.SetItemTable("REGDT", 5, 1, Format(Now, "yyyyMMddHHmmss").ToString)
                    it61.SetItemTable("REGID", 6, 1, USER_INFO.USRID)

                    it61.SetItemTable("AGEYMD", 7, 1, sAgeYmd)
                    it61.SetItemTable("SAGE", 8, 1, sSage)
                    it61.SetItemTable("SAGES", 9, 1, sSageS)
                    it61.SetItemTable("SAGEC", 10, 1, sSageC)
                    it61.SetItemTable("EAGE", 11, 1, sEage)
                    it61.SetItemTable("EAGES", 12, 1, sEages)
                    it61.SetItemTable("EAGEC", 13, 1, sEageC)
                    it61.SetItemTable("REFLM", 14, 1, sReflm)
                    it61.SetItemTable("REFLMS", 15, 1, sReflms)
                    it61.SetItemTable("REFHM", 16, 1, sRefHm)
                    it61.SetItemTable("REFHMS", 17, 1, sRefHms)
                    it61.SetItemTable("REFLF", 18, 1, sReflf)
                    it61.SetItemTable("REFLFS", 19, 1, sReflfs)
                    it61.SetItemTable("REFHF", 20, 1, sRefHf)
                    it61.SetItemTable("REFHFS", 21, 1, sRefHfs)
                    it61.SetItemTable("REFLT", 22, 1, sRefLt)

                    If mo_DAF.TransTestInfo(it60, 2, it61, 0, it62, 2, it63, 2, it64, sTestCd, sSpcCd, sUsdt, True) Then
                    Else
                        MsgBox("등록오류(" + iLine.ToString + ")")
                    End If

                End If
            Loop

        Catch ex As Exception
            MsgBox(ex.Message + "(" + iLine.ToString + ")")

        Finally
            MsgBox("완료")
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub

    Private Sub sbGetExcel_f62()
        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim dt As New DataTable
        Dim iLine As Integer = 0

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open("c:\as\참조검사.xls")

            xlsWkS = CType(xlsWkB.Sheets("sheet1"), Excel.Worksheet)

            Do While True
                iLine += 1

                If xlsWkS.Range("A" + CStr(iLine)).Value Is Nothing Then Exit Do

                Dim sTclsCd As String = xlsWkS.Range("A" + CStr(iLine)).Value.ToString : sTclsCd = sTclsCd.Substring(0, 5)
                Dim sTSpcCd As String = xlsWkS.Range("B" + CStr(iLine)).Value.ToString ' : sTSpcCd = sTSpcCd.PadLeft(4, "0"c)
                Dim sTestCd As String = xlsWkS.Range("C" + CStr(iLine)).Value.ToString : sTestCd = sTestCd.Substring(0, 5)
                Dim sSpcCd As String = xlsWkS.Range("D" + CStr(iLine)).Value.ToString ': sSpcCd = sSpcCd.PadLeft(4, "0"c)

                dt = mo_DAF.GetTestCdInfo_xls("lf062m", sTestCd, sSpcCd, "", sTclsCd, sTSpcCd)
                'If iLine = 325 Then MsgBox("A")

                If dt.Rows.Count < 1 And sTclsCd.Length < 6 And sTestCd.Length < 6 And sTSpcCd.Length < 5 And sSpcCd.Length < 5 Then

                    Dim it60 As New LISAPP.ItemTableCollection
                    Dim it61 As New LISAPP.ItemTableCollection
                    Dim it62 As New LISAPP.ItemTableCollection
                    Dim it63 As New LISAPP.ItemTableCollection
                    Dim it64 As New LISAPP.ItemTableCollection

                    it62.SetItemTable("TCLSCD", 1, 1, sTclsCd)
                    it62.SetItemTable("TSPCCD", 2, 1, sTSpcCd)
                    it62.SetItemTable("TESTCD", 3, 1, sTestCd)
                    it62.SetItemTable("SPCCD", 4, 1, sSpcCd)
                    it62.SetItemTable("REGDT", 5, 1, Format(Now, "yyyyMMddHHmmss").ToString)
                    it62.SetItemTable("REGID", 6, 1, USER_INFO.USRID)

                    If mo_DAF.TransTestInfo(it60, 2, it61, 2, it62, 0, it63, 2, it64, _
                                             sTestCd, sSpcCd, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, True) Then
                    Else
                        MsgBox("등록오류(" + iLine.ToString + ")")
                    End If

                End If
            Loop

        Catch ex As Exception
            MsgBox(ex.Message + "(" + iLine.ToString + ")")

        Finally
            MsgBox("완료")
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub

    Public Sub sbEditUseDt(ByVal rsUseTag As String)
        Dim sFn As String = "Public Sub sbEditUseDt"

        Try
            Dim fgf02 As New FGF03

            With fgf02
                .txtCd.Text = Me.txtTestCd.Text + Me.txtSpcCd.Text
                .txtNm.Text = Me.txtTNm.Text

                .lblUseDt.Text = IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString
                .lblUseDtA.Text = IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString
                .btnEditUseDt.Text = .btnEditUseDt.Text.Replace("사용일시", IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString)
                .txtUseDt.Text = IIf(rsUseTag.ToUpper = "USDT", Me.txtUSDT.Text, Me.txtUEDT.Text).ToString

                .Owner = Me
                .StartPosition = Windows.Forms.FormStartPosition.CenterParent
                .ShowDialog()
            End With

            If IsDate(Me.AccessibleName) Then
                If CDate(Me.AccessibleName) = Date.MinValue Then
                    'Delete
                    sbEditUseDt_Del()
                Else
                    'Edit
                    sbEditUseDt_Edit(rsUseTag, Me.AccessibleName)
                End If

            Else
                Return

            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.AccessibleName = ""

        End Try
    End Sub

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            dt = mo_DAF.GetUsUeCd_Test(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mo_DAF.TransTestInfo_DEL(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

            If bReturn Then
                MsgBox("해당 코드가 삭제되었습니다!!", MsgBoxStyle.Information)

                btnQuery_Click(Nothing, Nothing)
            Else
                MsgBox("해당 코드 삭제에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbEditUseDt_Edit(ByVal rsUseTag As String, ByVal rsUseDt As String)
        Dim sFn As String = "Sub sbEditUseDt_Edit"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            rsUseDt = rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", "")

            '> 사용중복 조사
            dt = mo_DAF.GetUsUeDupl_Test(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mo_DAF.TransTestInfo_UPD_US(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mo_DAF.TransTestInfo_UPD_UE(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
            End If

            If bReturn Then
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + "가 수정되었습니다!!", MsgBoxStyle.Information)

                btnQuery_Click(Nothing, Nothing)
            Else
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + " 수정에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Function fnCollectItemTable_60(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_60() As LISAPP.ItemTableCollection"

        Try
            Dim it60 As New LISAPP.ItemTableCollection
            Dim iCnt As Integer = 1

            If Me.txtSelSpc.Text <> "" Then iCnt = Me.txtSelSpc.Tag.ToString.Split("|"c).Length

            With it60

                For ix As Integer = 1 To iCnt
                    .SetItemTable("TESTCD", 1, ix, Me.txtTestCd.Text)

                    If Me.chkSpcGbn.Checked And Me.txtSelSpc.Text <> "" Then
                        .SetItemTable("SPCCD", 2, ix, Me.txtSelSpc.Tag.ToString.Split("|"c)(ix - 1))
                    Else
                        .SetItemTable("SPCCD", 2, ix, Me.txtSpcCd.Text)
                    End If
                    .SetItemTable("USDT", 3, ix, Me.txtUSDay.Text.Replace("-", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString)

                    If Me.txtUEDT.Text = "" Then
                        .SetItemTable("UEDT", 4, ix, msUEDT)
                    Else
                        .SetItemTable("UEDT", 4, ix, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                    End If

                    .SetItemTable("REGDT", 5, ix, rsRegDT)
                    .SetItemTable("REGID", 6, ix, USER_INFO.USRID)
                    .SetItemTable("REGIP", 7, ix, USER_INFO.LOCALIP)
                    .SetItemTable("TNM", 8, ix, Me.txtTNm.Text)
                    .SetItemTable("TNMS", 9, ix, Me.txtTNmS.Text)
                    .SetItemTable("TNMD", 10, ix, Me.txtTNmD.Text)
                    .SetItemTable("TNMP", 11, ix, Me.txtTNmP.Text)
                    .SetItemTable("TNMBP", 12, ix, Me.txtTNmBP.Text)
                    .SetItemTable("TCDGBN", 13, ix, CType(Me.cboTCdGbn.SelectedItem, String).Substring(1, 1))
                    .SetItemTable("TORDCD", 14, ix, Me.txtTOrdCd.Text)
                    .SetItemTable("SUGACD", 15, ix, Me.txtSugaCd.Text)
                    .SetItemTable("INSUGBN", 16, ix, Me.txtInsuGbn.Text)
                    .SetItemTable("SAMECD", 17, ix, Me.txtSameCd.Text)
                    .SetItemTable("EDICD", 18, ix, Me.txtEdiCd.Text)
                    .SetItemTable("EXLABYN", 19, ix, CType(IIf(Me.chkExLabYN.Checked, "1", "0"), String))
                    .SetItemTable("EXLABCD", 20, ix, Me.txtExLabCd.Text)
                    .SetItemTable("EXEDAY", 21, ix, CType(IIf(Me.chkExeDay1.Checked, "1", "0"), String) _
                                                  + CType(IIf(Me.chkExeDay2.Checked, "1", "0"), String) _
                                                  + CType(IIf(Me.chkExeDay3.Checked, "1", "0"), String) _
                                                  + CType(IIf(Me.chkExeDay4.Checked, "1", "0"), String) _
                                                  + CType(IIf(Me.chkExeDay5.Checked, "1", "0"), String) _
                                                  + CType(IIf(Me.chkExeDay6.Checked, "1", "0"), String) _
                                                  + CType(IIf(Me.chkExeDay7.Checked, "1", "0"), String))
                    .SetItemTable("TITLEYN", 22, ix, CType(IIf(Me.chkTitleYN.Checked, "1", "0"), String))
                    .SetItemTable("SEQTYN", 23, ix, CType(IIf(Me.chkSeqTYN.Checked, "1", "0"), String))
                    .SetItemTable("SEQTMI", 24, ix, IIf(Me.txtSeqTMi.Text = "", "0", Me.txtSeqTMi.Text).ToString)
                    .SetItemTable("CTGBN", 25, ix, CType(IIf(Me.chkCtGbn.Checked, "1", "0"), String))
                    .SetItemTable("POCTYN", 26, ix, CType(IIf(Me.chkPoctYN.Checked, "1", "0"), String))
                    .SetItemTable("MBTTYPE", 27, ix, CType(IIf(Me.cboMBTType.SelectedIndex = -1, "", Me.cboMBTType.SelectedIndex), String))

                    Select Case cboBBTType.SelectedIndex
                        Case -1
                            .SetItemTable("BBTTYPE", 28, ix, "")
                        Case Else
                            .SetItemTable("BBTTYPE", 28, ix, Me.cboBBTType.SelectedItem.ToString.Substring(1, 1))
                    End Select

                    .SetItemTable("MGTTYPE", 29, ix, CType(IIf(Me.cboMGTType.SelectedIndex = -1, "", Me.cboMGTType.SelectedIndex), String))
                    .SetItemTable("DISPSEQL", 30, ix, IIf(Me.txtDispSeqL.Text = "", "999", Me.txtDispSeqL.Text).ToString)
                    .SetItemTable("DISPSEQO", 31, ix, IIf(Me.txtDispSeqO.Text = "", "999", Me.txtDispSeqO.Text).ToString)
                    .SetItemTable("RPTYN", 32, ix, CType(IIf(Me.chkRptYN.Checked, "0", "1"), String))
                    .SetItemTable("TATYN", 33, ix, CType(IIf(Me.chkTatYN.Checked, "1", "0"), String))

                    Select Case cboPRptMi.SelectedIndex
                        Case -1, 0
                            .SetItemTable("PRPTMI", 34, ix, Me.txtPRptMi.Text)
                        Case 1
                            .SetItemTable("PRPTMI", 34, ix, CType(CInt(Me.txtPRptMi.Text) * 60, String))
                        Case 2
                            .SetItemTable("PRPTMI", 34, ix, CType(CInt(Me.txtPRptMi.Text) * 60 * 24, String))
                    End Select

                    Select Case cboFRptMi.SelectedIndex
                        Case -1, 0
                            .SetItemTable("FRPTMI", 35, ix, Me.txtFRptMI.Text)
                        Case 1
                            .SetItemTable("FRPTMI", 35, ix, CType(CInt(Me.txtFRptMI.Text) * 60, String))
                        Case 2
                            .SetItemTable("FRPTMI", 35, ix, CType(CInt(Me.txtFRptMI.Text) * 60 * 24, String))
                    End Select

                    .SetItemTable("TUBECD", 36, ix, Me.txtTubeCd.Text)
                    .SetItemTable("MINSPCVOL", 37, ix, Me.txtMinSpcVol.Text)
                    .SetItemTable("SRECVLT", 38, ix, Me.txtSRecvLT.Text)
                    .SetItemTable("RRPTST", 39, ix, Me.txtRRptST.Text)
                    .SetItemTable("CWARNING", 40, ix, Me.txtCWarning.Text)
                    .SetItemTable("BCCLSCD", 41, ix, Me.txtBcclsCd.Text)
                    If txtCprtGbn.SelectedIndex = -1 Then
                        .SetItemTable("CPRTGBN", 42, ix, "")
                    Else
                        .SetItemTable("CPRTGBN", 42, ix, CType(Me.txtCprtGbn.SelectedItem, String).Substring(1, CType(Me.txtCprtGbn.SelectedItem, String).IndexOf("]") - 1))
                    End If
                    .SetItemTable("PARTCD", 43, ix, Me.txtSlipCd.Text.Substring(0, 1))
                    .SetItemTable("SLIPCD", 44, ix, Me.txtSlipCd.Text.Substring(1, 1))
                    .SetItemTable("RSTTYPE", 45, ix, CType(IIf(Me.rdoRstType0.Checked, "0", "1"), String))
                    .SetItemTable("RSTULEN", 46, ix, CType(IIf(Me.chkRstLen.Checked, Me.cboRstULen.SelectedIndex.ToString, ""), String))
                    .SetItemTable("RSTLLEN", 47, ix, CType(IIf(Me.chkRstLen.Checked, Me.cboRstLLen.SelectedIndex.ToString, ""), String))
                    .SetItemTable("CUTOPT", 48, ix, CType(IIf(Me.rdoCutOpt1.Checked, "1", IIf(Me.rdoCutOpt2.Checked, "2", IIf(rdoCutOpt3.Checked, "3", "0"))), String))

                    '<-- JJH 결과단위(rstunit) 실제 byte수 체크 추가(20 byte이하일때만 등록되도록..  한글=2byte) - 일딴 주석처리
                    'Dim chkRstunit As Integer = 0
                    'For i = 1 To Len(Me.txtRstUnit.Text)
                    '    If Asc(Mid(Me.txtRstUnit.Text, i, 1)) > 0 Then
                    '        chkRstunit += 1
                    '    Else
                    '        chkRstunit += 2
                    '    End If
                    'Next

                    'Dim maxlenRstunit As Integer = 0
                    'maxlenRstunit = txtRstUnit.MaxLength

                    'If chkRstunit > maxlenRstunit Then'
                    '    MsgBox("결과단위 글자수 입력 초과입니다 글자수를 줄여주세요!!" + vbCrLf + "제한 수 : " + Str(maxlenRstunit) + Space(2) + "입력 수 : " + Str(chkRstunit) + vbCrLf + "한글은 2로 인식")
                    '    it60 = Nothing
                    '    Exit Function
                    'End If
                    '-->

                    .SetItemTable("RSTUNIT", 49, ix, Me.txtRstUnit.Text)
                    .SetItemTable("REFGBN", 50, ix, CType(IIf(Me.rdoRefGbn0.Checked, "0", IIf(Me.rdoRefGbn1.Checked, "1", "2")), String))
                    .SetItemTable("DESCREF", 51, ix, Me.txtDescRef.Text)

                    Dim sJudgType As String = "0"

                    sJudgType = IIf(Me.rdoJudgType0.Checked, "0", IIf(Me.rdoJudgType1.Checked, "1", IIf(Me.rdoJudgType2.Checked, "2", "3"))).ToString

                    If sJudgType = "2" Then
                        sJudgType = "21" + Me.cboJudgType1.SelectedIndex.ToString
                        sJudgType += "22" + Me.cboJudgType2.SelectedIndex.ToString
                    ElseIf sJudgType = "3" Then
                        sJudgType = "31" + Me.cboJudgType1.SelectedIndex.ToString
                        sJudgType += "32" + Me.cboJudgType2.SelectedIndex.ToString
                        sJudgType += "33" + Me.cboJudgType3.SelectedIndex.ToString
                    End If

                    .SetItemTable("JUDGTYPE", 52, ix, sJudgType)
                    .SetItemTable("UJUDGLT1", 53, ix, Me.txtUJudgLT1.Text)
                    .SetItemTable("UJUDGLT2", 54, ix, Me.txtUJudgLT2.Text)
                    .SetItemTable("UJUDGLT3", 55, ix, Me.txtUJudgLT3.Text)

                    .SetItemTable("PANICGBN", 56, ix, CType(IIf(Me.cboPanicGbn.SelectedIndex = -1, "", Me.cboPanicGbn.SelectedIndex), String))
                    .SetItemTable("PANICL", 57, ix, Me.txtPanicL.Text)
                    .SetItemTable("PANICH", 58, ix, Me.txtPanicH.Text)
                    .SetItemTable("DELTAGBN", 59, ix, CType(IIf(Me.cboDeltaGbn.SelectedIndex = -1, "", Me.cboDeltaGbn.SelectedIndex), String))
                    .SetItemTable("DELTAL", 60, ix, Me.txtDeltaL.Text)
                    .SetItemTable("DELTAH", 61, ix, Me.txtDeltaH.Text)
                    .SetItemTable("DELTADAY", 62, ix, Me.txtDeltaDay.Text)
                    .SetItemTable("CRITICALGBN", 63, ix, CType(IIf(Me.cboCriticalGbn.SelectedIndex = -1, "", Me.cboCriticalGbn.SelectedIndex), String))
                    .SetItemTable("CRITICALL", 64, ix, Me.txtCriticalL.Text)
                    .SetItemTable("CRITICALH", 65, ix, Me.txtCriticalH.Text)
                    .SetItemTable("ALERTGBN", 66, ix, Ctrl.Get_Code(Me.cboAlertGbn)) ' CType(IIf(cboAlertGbn.SelectedIndex = -1, "", cboAlertGbn.SelectedIndex), String))
                    .SetItemTable("ALERTL", 67, ix, Me.txtAlertL.Text)
                    .SetItemTable("ALERTH", 68, ix, Me.txtAlertH.Text)
                    .SetItemTable("ALIMITGBN", 69, ix, CType(IIf(Me.cboALimitGbn.SelectedIndex = -1, "", Me.cboALimitGbn.SelectedIndex), String))
                    .SetItemTable("ALIMITL", 70, ix, txtALimitL.Text)
                    .SetItemTable("ALIMITLS", 71, ix, CType(IIf(Me.cboALimitLS.SelectedIndex = -1, "", Me.cboALimitLS.SelectedIndex), String))
                    .SetItemTable("ALIMITH", 72, ix, Me.txtALimitH.Text)
                    .SetItemTable("ALIMITHS", 73, ix, CType(IIf(Me.cboALimitHS.SelectedIndex = -1, "", Me.cboALimitHS.SelectedIndex), String))
                    .SetItemTable("REQSUB", 74, ix, CType(IIf(Me.chkReqSub.Checked, "1", "0"), String))
                    .SetItemTable("TORDSLIP", 75, ix, CType(Me.cboTOrdSlip.SelectedItem, String).Substring(1, CType(Me.cboTOrdSlip.SelectedItem, String).IndexOf("]") - 1))
                    .SetItemTable("PTGBN", 76, ix, CType(IIf(Me.chkPtGbn.Checked, "1", "0"), String))
                    .SetItemTable("IOGBN", 77, ix, CType(IIf(Me.chkIOGbnO.Checked And Me.chkIOGbnI.Checked, "0", IIf(Me.chkIOGbnO.Checked, "1", "2")), String))

                    Dim sErGbn As String = ""
                    If Me.chkErGbn1.Checked And Me.chkErGbn2.Checked Then
                        sErGbn = "3"
                    ElseIf Me.chkErGbn1.Checked Then
                        sErGbn = "1"
                    ElseIf Me.chkErGbn2.Checked Then
                        sErGbn = "2"
                    End If

                    .SetItemTable("EMERGBN", 78, ix, sErGbn)
                    .SetItemTable("FIXRPTYN", 79, ix, CType(IIf(Me.chkFixRptYN.Checked, "1", "0"), String))
                    .SetItemTable("FIXRPTUSR", 80, ix, Ctrl.Get_Code(Me.cboFixRptusr))
                    .SetItemTable("DSPCCD1", 81, ix, Me.txtDSpcCdO.Text)
                    .SetItemTable("DSPCCD2", 82, ix, Me.txtDSpcCd2.Text)
                    .SetItemTable("ORDHIDE", 83, ix, CType(IIf(Me.chkOrdHIde.Checked, "1", "0"), String))
                    .SetItemTable("OWARNINGGBN", 84, ix, CType(IIf(Me.cboOWarningGbn.SelectedIndex = -1, "", Me.cboOWarningGbn.SelectedIndex), String))
                    .SetItemTable("OWARNING", 85, ix, Me.txtOWarning.Text)
                    .SetItemTable("VIWSUB", 86, ix, CType(IIf(Me.chkViwSub.Checked, "1", "0"), String))
                    .SetItemTable("BCCNT", 87, ix, Ctrl.Get_Code(Me.cboBpGbn))

                    .SetItemTable("OREQITEM", 88, ix, CType(IIf(chkOReqItem1.Checked, "1", "0"), String) _
                                                    + CType(IIf(chkOReqItem2.Checked, "1", "0"), String) _
                                                    + CType(IIf(chkOReqItem3.Checked, "1", "0"), String) _
                                                    + CType(IIf(chkOReqItem4.Checked, "1", "0"), String))

                    .SetItemTable("BCONEYN", 89, ix, CType(IIf(Me.txtBconeYN.Checked, "1", "0"), String))
                    .SetItemTable("GRPRSTYN", 90, ix, CType(IIf(Me.chkGrpRstYn.Checked, "1", "0"), String))
                    .SetItemTable("TLISCD", 91, ix, Me.txtTLisCd.Text)
                    .SetItemTable("DEFRST", 92, ix, Me.txtDefrst.Text)
                    .SetItemTable("SIGNRPTYN", 93, ix, CType(IIf(chkSignRptYn.Checked, "1", "0"), String))
                    .SetItemTable("CPRTCD", 94, ix, Me.txtCprtcd.Text)
                    .SetItemTable("FWGBN", 95, ix, CType(IIf(Me.chkFwgbn.Checked, "1", "0"), String)) ' 20170713 전재휘 추가
                    .SetItemTable("CWGBN", 96, ix, CType(IIf(Me.chkCWarning.Checked, "1", "0"), String)) ' 20170713 전재휘 추가

                    '.SetItemTable("ENFORCEMENT", 97, ix, CType(IIf(Me.cboEnforcement.SelectedIndex = -1, "", Me.cboEnforcement.SelectedIndex), String)) ' 20191216 JJH 시행처 구분 추가
                    '.SetItemTable("REQUEST", 98, ix, CType(IIf(Me.CboRequest.SelectedIndex = -1, "", Me.CboRequest.SelectedIndex), String)) ' 20191216 JJH 검사의뢰서/동의서 구분 추가
                    .SetItemTable("COWARNING", 97, ix, Me.txtTestInfo5.Text) ' 20191216 JJH 검체채취 및 의뢰시 주의사항 추가


                    Select Case cboPErRptMi.SelectedIndex
                        Case -1, 0
                            .SetItemTable("PERRPTMI", 98, ix, Me.txtPErRptMi.Text)
                        Case 1
                            .SetItemTable("PERRPTMI", 98, ix, CType(CInt(Me.txtPErRptMi.Text) * 60, String))
                        Case 2
                            .SetItemTable("PERRPTMI", 98, ix, CType(CInt(Me.txtPErRptMi.Text) * 60 * 24, String))
                    End Select

                    Select Case cboFErRptMi.SelectedIndex
                        Case -1, 0
                            .SetItemTable("FERRPTMI", 99, ix, Me.txtFErRptMI.Text)
                        Case 1
                            .SetItemTable("FERRPTMI", 99, ix, CType(CInt(Me.txtFErRptMI.Text) * 60, String))
                        Case 2
                            .SetItemTable("FERRPTMI", 99, ix, CType(CInt(Me.txtFErRptMI.Text) * 60 * 24, String))
                    End Select

                    Select Case cboAlramMi.SelectedIndex
                        Case -1, 0
                            .SetItemTable("ALARMT", 100, ix, Me.txtAlramT.Text)
                        Case 1
                            .SetItemTable("ALARMT", 100, ix, CType(CInt(Me.txtAlramT.Text) * 60, String))
                        Case 2
                            .SetItemTable("ALARMT", 100, ix, CType(CInt(Me.txtAlramT.Text) * 60 * 24, String))
                    End Select

                    Select Case cboErAlramMi.SelectedIndex
                        Case -1, 0
                            .SetItemTable("ALARMTER", 101, ix, Me.txtAlramTEr.Text)
                        Case 1
                            .SetItemTable("ALARMTER", 101, ix, CType(CInt(Me.txtAlramTEr.Text) * 60, String))
                        Case 2
                            .SetItemTable("ALARMTER", 101, ix, CType(CInt(Me.txtAlramTEr.Text) * 60 * 24, String))
                    End Select

                    .SetItemTable("ALARMTYPE", 102, ix, CType(Me.cboRPTITEM.SelectedItem, String))

                    .SetItemTable("ALARMTYPEER", 103, ix, CType(Me.cboRPTITEMER.SelectedItem, String))
                    '2019-12-06 검사의뢰지침 시행처 추가
                    'Select Case cboEnforcement.SelectedIndex
                    '    Case -1
                    '        .SetItemTable("ENFORCEMENT", 104, ix, "")
                    '    Case Else
                    '        .SetItemTable("ENFORCEMENT", 104, ix, Me.cboEnforcement.SelectedItem.ToString.Substring(1, 1))
                    'End Select

                    .SetItemTable("ENFORCEMENT", 104, ix, CType(IIf(Me.chkEnf0.Checked, "1", "0"), String) _
                                                         + CType(IIf(Me.chkEnf1.Checked, "1", "0"), String) _
                                                         + CType(IIf(Me.chkEnf2.Checked, "1", "0"), String) _
                                                         + CType(IIf(Me.chkEnf3.Checked, "1", "0"), String))



                    '<<검사의뢰서/동의서
                    'Select Case CboRequest.SelectedIndex
                    '    Case -1
                    '        .SetItemTable("REQUEST", 105, ix, "")
                    '    Case Else
                    '        .SetItemTable("REQUEST", 105, ix, Me.CboRequest.SelectedItem.ToString.Substring(1, 1))
                    'End Select

                    .SetItemTable("REQUEST", 105, ix, CType(IIf(Me.chkReq0.Checked, "1", "0"), String) _
                                                         + CType(IIf(Me.chkReq1.Checked, "1", "0"), String) _
                                                         + CType(IIf(Me.chkReq2.Checked, "1", "0"), String))


                    '<< JJH 검체단위 추가
                    .SetItemTable("spcunit", 106, ix, Me.txtSpcUnit.Text)

                    If Me.cboBldGbn.SelectedIndex >= 0 Then
                        .SetItemTable("DBLTSEQ", 107, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(0, 1))
                        .SetItemTable("DBLTORD", 108, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(1, 1))
                        .SetItemTable("PLGBN", 109, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(1, 1))
                    End If

                    'If Me.cboBldGbn.SelectedIndex >= 0 Then
                    '    .SetItemTable("DBLTSEQ", 106, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(0, 1))
                    '    .SetItemTable("DBLTORD", 107, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(1, 1))
                    '    .SetItemTable("PLGBN", 108, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(1, 1))
                    'End If


                    'Select Case cboPErRptMi.SelectedIndex
                    '    Case -1, 0
                    '        .SetItemTable("PERRPTMI", 97, ix, Me.txtPErRptMi.Text)
                    '    Case 1
                    '        .SetItemTable("PERRPTMI", 97, ix, CType(CInt(Me.txtPErRptMi.Text) * 60, String))
                    '    Case 2
                    '        .SetItemTable("PERRPTMI", 97, ix, CType(CInt(Me.txtPErRptMi.Text) * 60 * 24, String))
                    'End Select

                    'Select Case cboFErRptMi.SelectedIndex
                    '    Case -1, 0
                    '        .SetItemTable("FERRPTMI", 98, ix, Me.txtFErRptMI.Text)
                    '    Case 1
                    '        .SetItemTable("FERRPTMI", 98, ix, CType(CInt(Me.txtFErRptMI.Text) * 60, String))
                    '    Case 2
                    '        .SetItemTable("FERRPTMI", 98, ix, CType(CInt(Me.txtFErRptMI.Text) * 60 * 24, String))
                    'End Select

                    'Select Case cboAlramMi.SelectedIndex
                    '    Case -1, 0
                    '        .SetItemTable("ALARMT", 99, ix, Me.txtAlramT.Text)
                    '    Case 1
                    '        .SetItemTable("ALARMT", 99, ix, CType(CInt(Me.txtAlramT.Text) * 60, String))
                    '    Case 2
                    '        .SetItemTable("ALARMT", 99, ix, CType(CInt(Me.txtAlramT.Text) * 60 * 24, String))
                    'End Select

                    'Select Case cboErAlramMi.SelectedIndex
                    '    Case -1, 0
                    '        .SetItemTable("ALARMTER", 100, ix, Me.txtAlramTEr.Text)
                    '    Case 1
                    '        .SetItemTable("ALARMTER", 100, ix, CType(CInt(Me.txtAlramTEr.Text) * 60, String))
                    '    Case 2
                    '        .SetItemTable("ALARMTER", 100, ix, CType(CInt(Me.txtAlramTEr.Text) * 60 * 24, String))
                    'End Select

                    '.SetItemTable("ALARMTYPE", 101, ix, CType(Me.cboRPTITEM.SelectedItem, String))

                    '.SetItemTable("ALARMTYPEER", 102, ix, CType(Me.cboRPTITEMER.SelectedItem, String))
                    ''2019-12-06 검사의뢰지침 시행처 추가
                    'Select Case cboEnforcement.SelectedIndex
                    '    Case -1
                    '        .SetItemTable("ENFORCEMENT", 103, ix, "")
                    '    Case Else
                    '        .SetItemTable("ENFORCEMENT", 103, ix, Me.cboEnforcement.SelectedItem.ToString.Substring(1, 1))
                    'End Select

                    'Select Case CboRequest.SelectedIndex
                    '    Case -1
                    '        .SetItemTable("REQUEST", 104, ix, "")
                    '    Case Else
                    '        .SetItemTable("REQUEST", 104, ix, Me.CboRequest.SelectedItem.ToString.Substring(1, 1))
                    'End Select

                    'If Me.cboBldGbn.SelectedIndex >= 0 Then
                    '    .SetItemTable("DBLTSEQ", 105, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(0, 1))
                    '    .SetItemTable("DBLTORD", 106, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(1, 1))
                    '    .SetItemTable("PLGBN", 107, ix, Ctrl.Get_Code(Me.cboBldGbn).Substring(1, 1))
                    'End If
                    '


                   
                Next

            End With

            fnCollectItemTable_60 = it60

        Catch ex As Exception
            fnCollectItemTable_60 = Nothing
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnCollectItemTable_61(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_61() As LISAPP.ItemTableCollection"

        Try
            Dim it61 As New LISAPP.ItemTableCollection
            Dim iAgeYMD As Integer = 0, sAge1 As String = "", iAge1S As Integer = -1, iAge2S As Integer = -1, sAge2 As String = "", sRefLM As String = "", iRefLMS As Integer = -1, sRefHM As String = "", iRefHMS As Integer = -1
            Dim sRefLF As String = "", iRefLFS As Integer = -1, sRefHF As String = "", iRefHFS As Integer = -1, sRefLT As String = ""
            Dim iSpcCnt As Integer = 0, iCnt As Integer = 0

            If Me.txtSelSpc.Text <> "" Then iSpcCnt = Me.txtSelSpc.Tag.ToString.Split("|"c).Length - 1

            For ix1 As Integer = 0 To iSpcCnt
                With spdAgeRef
                    For i As Integer = 1 To .MaxRows
                        iCnt += 1

                        .Col = 1 : .Row = i : iAgeYMD = .TypeComboBoxCurSel
                        .Col = 2 : .Row = i : sAge1 = .Text
                        .Col = 3 : .Row = i : iAge1S = .TypeComboBoxCurSel
                        .Col = 4 : .Row = i : iAge2S = .TypeComboBoxCurSel
                        .Col = 5 : .Row = i : sAge2 = .Text
                        .Col = 6 : .Row = i : sRefLM = .Text
                        .Col = 7 : .Row = i : iRefLMS = .TypeComboBoxCurSel
                        .Col = 8 : .Row = i : iRefHMS = .TypeComboBoxCurSel
                        .Col = 9 : .Row = i : sRefHM = .Text
                        .Col = 10 : .Row = i : sRefLF = .Text
                        .Col = 11 : .Row = i : iRefLFS = .TypeComboBoxCurSel
                        .Col = 12 : .Row = i : iRefHFS = .TypeComboBoxCurSel
                        .Col = 13 : .Row = i : sRefHF = .Text
                        .Col = 14 : .Row = i : sRefLT = .Text

                        If iRefLMS = -1 Then iRefLMS = 0
                        If iRefHMS = -1 Then iRefHMS = 0
                        If iRefLFS = -1 Then iRefLFS = 0
                        If iRefHFS = -1 Then iRefHFS = 0

                        If sAge1 = "" Then Exit For
                        'If sAge1 = "" Or sAge2 = "" Or iAgeYMD = -1 Or iAge1S = -1 Or iAge2S = -1 Then
                        '    If i = 1 And Ctrl.Get_Code(cboTCdGbn) <> "G" Then
                        '        it61.SetItemTable("TESTCD", 1, iCnt, Me.txtTestCd.Text)
                        '        If Me.txtSelSpc.Text <> "" Then
                        '            it61.SetItemTable("SPCCD", 2, iCnt, Me.txtSelSpc.Tag.ToString.Split("|"c)(ix1))
                        '        Else
                        '            it61.SetItemTable("SPCCD", 2, iCnt, Me.txtSpcCd.Text)
                        '        End If
                        '        it61.SetItemTable("USDT", 3, iCnt, Me.txtUSDay.Text.Replace("-", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString)
                        '        it61.SetItemTable("REFSEQ", 4, iCnt, i.ToString)
                        '        it61.SetItemTable("REGDT", 5, iCnt, rsRegDT)
                        '        it61.SetItemTable("REGID", 6, iCnt, msUserID)
                        '        it61.SetItemTable("AGEYMD", 7, iCnt, "Y")
                        '        it61.SetItemTable("SAGE", 8, iCnt, "0")
                        '        it61.SetItemTable("SAGES", 9, iCnt, "0")
                        '        it61.SetItemTable("SAGEC", 10, iCnt, "0")
                        '        it61.SetItemTable("EAGE", 11, iCnt, "200")
                        '        it61.SetItemTable("EAGES", 12, iCnt, "0")
                        '        it61.SetItemTable("EAGEC", 13, iCnt, "200")
                        '        it61.SetItemTable("REFLM", 14, iCnt, "")
                        '        it61.SetItemTable("REFLMS", 15, iCnt, "")
                        '        it61.SetItemTable("REFHM", 16, iCnt, "")
                        '        it61.SetItemTable("REFHMS", 17, iCnt, "")
                        '        it61.SetItemTable("REFLF", 18, iCnt, "")
                        '        it61.SetItemTable("REFLFS", 19, iCnt, "")
                        '        it61.SetItemTable("REFHF", 20, iCnt, "")
                        '        it61.SetItemTable("REFHFS", 21, iCnt, "")
                        '        it61.SetItemTable("REFLT", 22, iCnt, "")
                        '    End If

                        '    Exit For
                        'End If

                        it61.SetItemTable("TESTCD", 1, iCnt, Me.txtTestCd.Text)
                        it61.SetItemTable("SPCCD", 2, iCnt, Me.txtSpcCd.Text)
                        it61.SetItemTable("USDT", 3, iCnt, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)
                        it61.SetItemTable("REFSEQ", 4, iCnt, i.ToString)
                        it61.SetItemTable("REGDT", 5, iCnt, rsRegDT)
                        it61.SetItemTable("REGID", 6, iCnt, USER_INFO.USRID)
                        it61.SetItemTable("AGEYMD", 7, iCnt, IIf(iAgeYMD = 0, "D", IIf(iAgeYMD = 1, "M", "Y")).ToString)
                        it61.SetItemTable("SAGE", 8, iCnt, sAge1)
                        it61.SetItemTable("SAGES", 9, iCnt, iAge1S.ToString)
                        it61.SetItemTable("SAGEC", 10, iCnt, Format(IIf(iAgeYMD = 2, CDbl(sAge1), IIf(iAgeYMD = 1, CDbl(sAge1) / 12, CDbl(sAge1) / 365)), "000.000").ToString)
                        it61.SetItemTable("EAGE", 11, iCnt, sAge2)
                        it61.SetItemTable("EAGES", 12, iCnt, iAge2S.ToString)
                        it61.SetItemTable("EAGEC", 13, iCnt, Format(IIf(iAgeYMD = 2, CDbl(sAge2), IIf(iAgeYMD = 1, CDbl(sAge2) / 12, CDbl(sAge2) / 365)), "000.000").ToString)
                        it61.SetItemTable("REFLM", 14, iCnt, sRefLM)
                        it61.SetItemTable("REFLMS", 15, iCnt, IIf(sRefLM = "", "", iRefLMS).ToString)
                        it61.SetItemTable("REFHM", 16, iCnt, sRefHM)
                        it61.SetItemTable("REFHMS", 17, iCnt, IIf(sRefHM = "", "", iRefHMS).ToString)
                        it61.SetItemTable("REFLF", 18, iCnt, sRefLF)
                        it61.SetItemTable("REFLFS", 19, iCnt, IIf(sRefLF = "", "", iRefLFS).ToString)
                        it61.SetItemTable("REFHF", 20, iCnt, sRefHF)
                        it61.SetItemTable("REFHFS", 21, iCnt, IIf(sRefHF = "", "", iRefHFS).ToString)
                        it61.SetItemTable("REFLT", 22, iCnt, sRefLT)
                    Next
                End With
            Next

            fnCollectItemTable_61 = it61

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            fnCollectItemTable_61 = New LISAPP.ItemTableCollection

        End Try
    End Function

    Private Function fnCollectItemTable_62(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_62() As LISAPP.ItemTableCollection"

        Try
            Dim it62 As New LISAPP.ItemTableCollection

            With spdDTest
                For ix As Integer = 1 To .MaxRows

                    .Row = ix
                    .Col = .GetColFromID("testcd") : Dim sTCd As String = .Text
                    .Col = .GetColFromID("spccd") : Dim sSCd As String = .Text
                    .Col = .GetColFromID("grprstyn") : Dim sGRstYn As String = .Text

                    it62.SetItemTable("TCLSCD", 1, ix, Me.txtTestCd.Text)
                    it62.SetItemTable("TSPCCD", 2, ix, Me.txtSpcCd.Text)
                    it62.SetItemTable("TESTCD", 3, ix, sTCd)
                    it62.SetItemTable("SPCCD", 4, ix, sSCd)
                    it62.SetItemTable("GRPRSTYN", 5, ix, sGRstYn)
                    it62.SetItemTable("REGDT", 6, ix, rsRegDT)
                    it62.SetItemTable("REGID", 7, ix, USER_INFO.USRID)
                Next
            End With

            fnCollectItemTable_62 = it62
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            fnCollectItemTable_62 = New LISAPP.ItemTableCollection

        End Try
    End Function

    Private Function fnCollectItemTable_63(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_63() As LISAPP.ItemTableCollection"

        Try
            Dim it63 As New LISAPP.ItemTableCollection

            With spdRTest
                For i As Integer = 1 To .MaxRows
                    it63.SetItemTable("TESTCD", 1, i, Me.txtTestCd.Text)
                    it63.SetItemTable("SPCCD", 2, i, Me.txtSpcCd.Text)
                    .Col = 2 : .Row = i : it63.SetItemTable("REFTESTCD", 3, i, .Text)
                    .Col = 3 : .Row = i : it63.SetItemTable("REFSPCCD", 4, i, .Text)
                    it63.SetItemTable("REGDT", 5, i, rsRegDT)
                    it63.SetItemTable("REGID", 6, i, USER_INFO.USRID)
                Next
            End With

            Return it63
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New LISAPP.ItemTableCollection
        End Try

    End Function

    Private Function fnCollectItemTable_64(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_64() As LISAPP.ItemTableCollection"

        Try
            Dim it64 As New LISAPP.ItemTableCollection
            Dim iCnt As Integer = 0

            'If Me.txtTestInfo1.Text <> "" Then
            iCnt += 1

            it64.SetItemTable("INFOGBN", 1, iCnt, "1") '검사법
            it64.SetItemTable("TESTCD", 2, iCnt, Me.txtTestCd.Text)
            it64.SetItemTable("SPCCD", 3, iCnt, Me.txtSpcCd.Text)
            it64.SetItemTable("TESTINFO", 4, iCnt, Me.txtTestInfo1.Text)
            it64.SetItemTable("REGDT", 5, iCnt, rsRegDT)
            it64.SetItemTable("REGID", 6, iCnt, USER_INFO.USRID)
            'End If

            'If Me.txtTestInfo2.Text <> "" Then
            iCnt += 1

            'it64.SetItemTable("INFOGBN", 1, iCnt, "1")
            it64.SetItemTable("INFOGBN", 1, iCnt, "2") '20140128 정선영 수정, 주의내용
            it64.SetItemTable("TESTCD", 2, iCnt, Me.txtTestCd.Text)
            it64.SetItemTable("SPCCD", 3, iCnt, Me.txtSpcCd.Text)
            it64.SetItemTable("TESTINFO", 4, iCnt, Me.txtTestInfo2.Text)
            it64.SetItemTable("REGDT", 5, iCnt, rsRegDT)
            it64.SetItemTable("REGID", 6, iCnt, USER_INFO.USRID)
            'End If

            'If Me.txtTestInfo3.Text <> "" Then
            iCnt += 1

            'it64.SetItemTable("INFOGBN", 1, iCnt, "1")
            it64.SetItemTable("INFOGBN", 1, iCnt, "3") '20140128 정선영 수정, 임상적의의
            it64.SetItemTable("TESTCD", 2, iCnt, Me.txtTestCd.Text)
            it64.SetItemTable("SPCCD", 3, iCnt, Me.txtSpcCd.Text)
            it64.SetItemTable("TESTINFO", 4, iCnt, Me.txtTestInfo3.Text)
            it64.SetItemTable("REGDT", 5, iCnt, rsRegDT)
            it64.SetItemTable("REGID", 6, iCnt, USER_INFO.USRID)
            'End If

            If Me.chkOReqItem4.Checked Then
                Dim sTestInfo As String = ""

                With Me.spdOrdCont
                    For ix As Integer = 1 To Me.spdOrdCont.MaxRows
                        .Row = ix
                        .Col = 1 : Dim sInfo As String = .Text

                        If sInfo <> "" Then
                            sTestInfo += sInfo + "|"
                        End If
                    Next
                End With

                If sTestInfo <> "" Then
                    iCnt += 1

                    it64.SetItemTable("INFOGBN", 1, iCnt, "4")
                    it64.SetItemTable("TESTCD", 2, iCnt, Me.txtTestCd.Text)
                    it64.SetItemTable("SPCCD", 3, iCnt, Me.txtSpcCd.Text)
                    it64.SetItemTable("TESTINFO", 4, iCnt, sTestInfo)
                    it64.SetItemTable("REGDT", 5, iCnt, rsRegDT)
                    it64.SetItemTable("REGID", 6, iCnt, USER_INFO.USRID)

                End If
            End If

            Return it64
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New LISAPP.ItemTableCollection
        End Try

    End Function

    Private Function fnFindChildControl(ByVal actrlCol As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control
        Dim sFn As String = "Private Function fnFindChildControl(ByVal actrlCol As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control"

        Try
            Dim ctrl As System.Windows.Forms.Control

            For Each ctrl In actrlCol
                If ctrl.Controls.Count > 0 Then
                    fnFindChildControl(ctrl.Controls)
                ElseIf ctrl.Controls.Count = 0 Then
                    If CType(ctrl.Tag, String) <> "" Then
                        mchildctrlcol.Add(ctrl)
                    End If
                End If
            Next
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mo_DAF.GetRecentTestInfo(rsTestCd, rsSpcCd, rsUsDt)

            If dt.Rows.Count > 0 Then
                Return "시작일시가 " + dt.Rows(0).Item(0).ToString + "인 동일 " + dt.Rows(0).Item(1).ToString + " 검사+검체 코드가 존재합니다." + vbCrLf + vbCrLf + _
                       "검사코드, 검체코드 또는 시작일시를 재조정 하십시요!!"
            Else
                Return ""
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindTOrdCd(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsTOrdCd As String, ByVal rsUsDt As String) As String
        Dim sFn As String = ""

        Try
            If rsTOrdCd = "" Then Return ""

            Dim dt As DataTable = mo_DAF.GetRecentTOrdCdInfo(rsTestCd.Trim, rsSpcCd.Trim, rsTOrdCd, rsUsDt)

            If dt.Rows.Count > 0 Then
                Return "검사코드 " + dt.Rows(0).Item(0).ToString + "에 동일 처방코드가 존재합니다." + vbCrLf + vbCrLf + _
                       "처방코드를 재조정 하십시요!!"
            Else
                Return ""
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function


    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim dt As DataTable = mo_DAF.GetNewRegDT

            If dt.Rows.Count > 0 Then
                fnGetSystemDT = dt.Rows(0).Item(0).ToString
            Else
                MsgBox("시스템의 날짜를 초기화하지 못했습니다. 관리자에게 문의하시기 바랍니다!!", MsgBoxStyle.Information)
                Return Format(Now, "yyyyMMddHHmmss")
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")
        End Try
    End Function

    Public Function fnGetTCdGbn() As String
        Dim sFn As String = "Public Function fnGetTCdGbn() As String"

        Try
            If cboTCdGbn.SelectedIndex = -1 Then
                Return ""
            Else
                Return cboTCdGbn.SelectedItem.ToString.Substring(1, 1)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = ""

        Try
            Dim it60 As New LISAPP.ItemTableCollection
            Dim it61 As New LISAPP.ItemTableCollection
            Dim it62 As New LISAPP.ItemTableCollection
            Dim it63 As New LISAPP.ItemTableCollection
            Dim it64 As New LISAPP.ItemTableCollection
            Dim iRegType60 As Integer = 0, iRegType61 As Integer = 0, iRegType62 As Integer = 0, iRegType63 As Integer = 0
            Dim sRegDT As String

            iRegType60 = CType(IIf(Me.rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType61 = CType(IIf(Me.rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType62 = CType(IIf(Me.rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType63 = CType(IIf(Me.rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it60 = fnCollectItemTable_60(sRegDT)
            If IsNothing(it60) = True Then
                fnReg = False
                Exit Function
            End If

            it61 = fnCollectItemTable_61(sRegDT)

            If spdDTest.MaxRows > 0 Then
                it62 = fnCollectItemTable_62(sRegDT)
            End If

            If spdRTest.MaxRows > 0 Then
                it63 = fnCollectItemTable_63(sRegDT)
            End If

            it64 = fnCollectItemTable_64(sRegDT)

            If mo_DAF.TransTestInfo(it60, iRegType60, it61, iRegType61, it62, iRegType62, it63, iRegType63, it64, _
                                     Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            fnReg = False
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Me.chkAddModeD.Checked Or Me.chkAddModeR.Checked Then
                MsgBox("검사 추가모드를 중지하시고 등록(수정)하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTestCd.Text = "" Then
                MsgBox("검사코드를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTestCd.Text.Length <> 5 And Me.txtTestCd.Text.Length <> 7 Then
                MsgBox("검사코드 자리수가 5자리나 7자리인지 확인하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If (Me.txtSpcCd.Text = "" And Me.chkSpcGbn.Checked = False) Or (Me.chkSpcGbn.Checked And Me.txtSelSpc.Text = "") Then
                MsgBox("검체코드를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(Me.txtTestCd.Text.Trim) < 5 Then
                MsgBox("검사코드를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.cboTCdGbn.SelectedIndex = -1 Then
                MsgBox("검사코드 구분을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTestCd.Text.Length > 5 And Me.cboTCdGbn.Text.Substring(1, 1) <> "C" Then
                MsgBox("검사코드 자리수가 5가 넘으면 검사코드구분을 'Child Of Sub'로 선택하세요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTestCd.Text.Length = 5 And Me.cboTCdGbn.Text.Substring(1, 1) = "C" Then
                MsgBox("검사코드 자리수가 5이면 검사코드구분을 'Child Of Sub'로 선택할 수 없습니다!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.chkSpcGbn.Checked = False And txtSpcCd.Text.Contains(" ") Then 'Len(Me.txtSpcCd.Text.Trim) < PRG_CONST.Len_SpcCd Then
                MsgBox("검체코드를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsDate(Me.txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If Me.rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If

                    sBuf = fnFindTOrdCd(Me.txtTestCd.Text, Me.txtSpcCd.Text, Me.txtTOrdCd.Text, Me.txtUSDay.Text.Replace("-", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString)
                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If txtTNm.Text.Trim = "" Then
                MsgBox("검사명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtTNmS.Text.Trim = "" Then
                MsgBox("검사명(약어)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtTNmD.Text.Trim = "" Then
                MsgBox("검사명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtTNmP.Text.Trim = "" Then
                MsgBox("검사명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If cboTCdGbn.SelectedIndex = -1 Then
                MsgBox("검사코드구분을 선택해 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If cboTOrdSlip.SelectedIndex = -1 Then
                MsgBox("검사처방분야를 선택해 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            'Group과 Child를 제외하고 나머지 경우에는 검사처방코드를 입력해야 한다.
            If Not (CType(cboTCdGbn.SelectedItem, String).StartsWith("[S]") Or CType(cboTCdGbn.SelectedItem, String).StartsWith("[C]") Or CType(cboTCdGbn.SelectedItem, String).StartsWith("[P]")) Then
                If txtTOrdCd.Text.Trim = "" Then
                    MsgBox("검사처방코드를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If
            End If

            '검사처방 사용하지 않는 경우를 제외하고 검사처방순번을 입력해야 한다.
            If Not chkOrdHIde.Checked Then
                If IsNumeric(txtDispSeqO.Text) = False Then
                    MsgBox("검사처방순번을 숫자로 입력하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If
            End If

            '검사처방조건 중 외래, 병동을 적어도 하나 선택해야 한다.
            If Not (chkIOGbnO.Checked Or chkIOGbnI.Checked) Then
                MsgBox("검사처방조건을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            '기본처방검체를 입력해야 한다.
            If txtDSpcCdO.Text.Trim = "" And Me.txtTestCd.Text.Length = 5 Then
                MsgBox("기본처방검체를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            '용기코드를 입력해야 한다.
            If txtTubeCd.Text.Trim = "" Then
                MsgBox("용기코드를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            '검사계(부서)를 입력해야 한다.
            If txtBcclsCd.Text.Trim = "" Then
                MsgBox("검사계(부서)를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            '검사슬립을 입력해야 한다.
            If txtSlipCd.Text.Trim = "" Then
                MsgBox("검사분야를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If chkExLabYN.Checked And txtExLabCd.Text = "" Then
                MsgBox("위탁기관을 입력하거나 위탁검사 설정을 풀어 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            '-- 2004-08-27 JJH Remark & 수정 판정유형의 데이타 Validate 기능추가 ---------------------------------------------------
            'sJudgType = IIf(rbnJudgType0.Checked, "0", IIf(rbnJudgType1.Checked, "1", IIf(rbnJudgType2.Checked, "2", "3"))).ToString

            'If sJudgType = "2" Then
            '    sJudgType = "21" + cboJudgType1.SelectedIndex.ToString
            '    sJudgType += "22" + cboJudgType2.SelectedIndex.ToString
            'ElseIf sJudgType = "3" Then
            '    sJudgType = "31" + cboJudgType1.SelectedIndex.ToString
            '    sJudgType += "32" + cboJudgType2.SelectedIndex.ToString
            '    sJudgType += "33" + cboJudgType3.SelectedIndex.ToString
            'End If

            If rdoJudgType2.Checked Or rdoJudgType3.Checked Then
                If txtUJudgLT1.Text.Trim = "" Then
                    MsgBox("판정문자 1을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If

                If cboJudgType1.SelectedIndex < 0 Then
                    MsgBox("판정문자 1의 결과처리방법을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If

                If txtUJudgLT2.Text.Trim = "" Then
                    MsgBox("판정문자 2를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If

                If cboJudgType1.SelectedIndex < 0 Then
                    MsgBox("판정문자 2의 결과처리방법을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If

                If rdoJudgType3.Checked Then
                    If txtUJudgLT3.Text.Trim = "" Then
                        MsgBox("판정문자 3을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                        Exit Function
                    End If

                    If cboJudgType3.SelectedIndex < 0 Then
                        MsgBox("판정문자 3의 결과처리방법을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Not errpd.GetError(Me.btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(Me.btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbAddTest_spdTest(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsTnmd As String)
        Dim sFn As String = "Private Sub sbAddTest_spdTest(ByVal asTCd As String)"

        Try
            Dim sTClsCd As String = "", sSpcCd As String = ""
            Dim iExist As Integer = 0

            If miAddModeKey = 1 Then
                With spdDTest
                    For i As Integer = 1 To .MaxRows
                        .Col = 2 : .Row = i : sTClsCd = .Text.Trim
                        .Col = 3 : .Row = i : sSpcCd = .Text.Trim

                        If sTClsCd + sSpcCd = rsTestCd + rsSpcCd Then
                            iExist = i
                            MsgBox("이미 추가된 검사입니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
                            Exit For
                        End If
                    Next

                    If iExist = 0 Then
                        .MaxRows += 1
                        .Col = 2 : .Row = .MaxRows : .Text = rsTestCd
                        .Col = 3 : .Row = .MaxRows : .Text = rsSpcCd
                        .Col = 4 : .Row = .MaxRows : .Text = rsTnmd
                    End If
                End With
            ElseIf miAddModeKey = 2 Then
                With spdRTest
                    For i As Integer = 1 To .MaxRows
                        .Col = 2 : .Row = i : sTClsCd = .Text
                        .Col = 3 : .Row = i : sSpcCd = .Text

                        If sTClsCd + sSpcCd = rsTestCd + rsSpcCd Then
                            iExist = i
                            MsgBox("이미 추가된 검사입니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
                            Exit For
                        End If
                    Next

                    If iExist = 0 Then
                        .MaxRows += 1
                        .Col = 2 : .Row = .MaxRows : .Text = rsTestCd
                        .Col = 3 : .Row = .MaxRows : .Text = rsSpcCd
                        .Col = 4 : .Row = .MaxRows : .Text = rsTnmd
                    End If
                End With
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    'Private Sub sbBlockSpreadClickedRow(ByVal aspd As AxFPSpreadADO.AxfpSpread, ByVal aiCol As Integer, ByVal aiRow As Integer)
    '    Dim sFn As String = "Private Sub sbBlockSpreadClickedRow(ByVal aspd As AxFPSpreadADO.AxfpSpread, ByVal aiCol As Integer, ByVal aiRow As Integer)"

    '    Try
    '        With aspd
    '            .ReDraw = False
    '            .Col = 1 : .Col2 = .MaxCols : .Row = aiRow : .Row2 = aiRow
    '            .BlockMode = True
    '            .Action = FPSpreadADO.ActionConstants.ActionSelectBlock
    '            .BlockMode = False

    '            .SetActiveCell(aiCol, aiRow)
    '            .ReDraw = True
    '        End With
    '    Catch ex As Exception
    '        Fn.log(msFile + sFn, Err)
    '        MsgBox(msFile + sFn + vbCrLf + ex.Message)
    '    Finally
    '        aspd.ReDraw = True
    '    End Try
    'End Sub

    Private Sub sbDelCheckedRow(ByVal aspd As AxFPSpreadADO.AxfpSpread, ByVal aiChkCol As Integer)
        Dim sFn As String = "Private Sub sbDelCheckedRow(ByVal aspd As AxFPSpreadADO.AxfpSpread, ByVal aiChkCol As Integer)"

        Try
            Dim sChk As String = ""

            With aspd
                For i As Integer = 1 To .MaxRows
                    For j As Integer = i To .MaxRows
                        .Col = aiChkCol : .Row = j : sChk = .Text

                        If sChk = "1" Then
                            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                            .MaxRows -= 1
                            i = j - 1

                            Exit For
                        End If
                    Next

                    If i > .MaxRows Then
                        Exit For
                    End If
                Next
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdDetail(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String, Optional ByVal rsUeDt As String = "30000101", Optional ByVal riMode As Integer = 0)
        Dim sFn As String = ""

        Try
            miSelectKey = 1
            sbInitialize(riMode)

            If Not Me.rdoWorkOpt2.Checked Then
                sbDisplayCdList_Ref(rsUsDt, rsUeDt)
            End If

            If riMode = 0 Then
                sbDisplayCdDetail_Test(rsTestCd, rsSpcCd, rsUsDt)
            ElseIf riMode = 1 Then
                sbDisplayCdDetail_Test_Partial(rsTestCd, rsSpcCd, rsUsDt)
            End If

            sbDisplayCdDetail_Test_AgeRef(rsTestCd, rsSpcCd, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
            sbDisplayCdDetail_Test_DTest(rsTestCd, rsSpcCd)
            sbDisplayCdDetail_Test_RTest(rsTestCd, rsSpcCd) '<세부검사조회
            sbDisplayCdDetail_Test_Info(rsTestCd, rsSpcCd)
            sbDisplayCdDetail_Test_Info_new(rsTestCd, rsSpcCd, rsUsDt) '<검사의뢰지침

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0

            If Not IsNothing(Me.Owner) Then
                If Me.rdoWorkOpt2.Checked Then
                    Me.txtSpcCd_Validating(Me.txtSpcCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtSlipCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtBcclsCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtTubeCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtDSpcCdO, Nothing)
                    Me.txtSpcCd_Validating(Me.txtDSpcCd2, Nothing)
                End If
            End If
            If Ctrl.Get_Code(cboTCdGbn) = "B" And chkGrpRstYn.Enabled = False Then chkGrpRstYn.Enabled = True

        End Try
    End Sub

    Public Sub sbDisplayCdDetail_spc(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String, Optional ByVal rsUeDt As String = "30000101", Optional ByVal riMode As Integer = 0)
        Dim sFn As String = ""

        Try
            miSelectKey = 1
            sbInitialize(riMode)

            If Not Me.rdoWorkOpt2.Checked Then
                sbDisplayCdList_Ref(rsUsDt, rsUeDt)
            End If

            If riMode = 0 Then
                sbDisplayCdDetail_Test(rsTestCd, rsSpcCd, rsUsDt)
            ElseIf riMode = 1 Then
                sbDisplayCdDetail_Test_Partial(rsTestCd, rsSpcCd, rsUsDt)
            End If

            sbDisplayCdDetail_Test_AgeRef(rsTestCd, rsSpcCd, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
            sbDisplayCdDetail_Test_DTest(rsTestCd, rsSpcCd)
            sbDisplayCdDetail_Test_RTest(rsTestCd, rsSpcCd)
            sbDisplayCdDetail_Test_Info(rsTestCd, rsSpcCd)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0

            If Not IsNothing(Me.Owner) Then
                If Me.rdoWorkOpt2.Checked Then
                    Me.txtSpcCd_Validating(Me.txtSpcCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtSlipCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtBcclsCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtTubeCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtDSpcCdO, Nothing)
                    Me.txtSpcCd_Validating(Me.txtDSpcCd2, Nothing)
                End If
            End If
            If Ctrl.Get_Code(Me.cboTCdGbn) = "B" And Me.chkGrpRstYn.Enabled = False Then Me.chkGrpRstYn.Enabled = True

        End Try
    End Sub


    Private Sub sbDisplayCdDetail_Test(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail(ByVal asBuf As String, ByVal asTCd As String)"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mo_DAF.GetTestInfo(rsTestCd, rsSpcCd, rsUsDt)

            sbInitialize()
            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()
            sbInitialize_CtrlCollection()

            fnFindChildControl(Me.Controls)

            If dt.Rows.Count < 1 Then Return

            For i As Integer = 0 To dt.Rows.Count - 1
                For Each cctrl In mchildctrlcol
                    For j As Integer = 0 To dt.Columns.Count - 1


                        If cctrl.Tag.ToString.ToUpper = dt.Columns(j).ColumnName().ToUpper Then
                            mchildctrlcol.Remove(1)

                            If TypeOf (cctrl) Is System.Windows.Forms.ComboBox Then
                                If cctrl.Tag.ToString.EndsWith("_01") = True Then
                                    iCurIndex = -1

                                    For k As Integer = 0 To CType(cctrl, System.Windows.Forms.ComboBox).Items.Count - 1
                                        If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.EndsWith(dt.Rows(i).Item(j).ToString) = True Then
                                            iCurIndex = k

                                            Exit For
                                        End If

                                        If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.StartsWith(dt.Rows(i).Item(j).ToString) = True Then
                                            iCurIndex = k

                                            Exit For
                                        End If

                                    Next

                                    CType(cctrl, Windows.Forms.ComboBox).SelectedIndex = iCurIndex
                                End If

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.TextBox Then
                                cctrl.Text = dt.Rows(i).Item(j).ToString.Trim

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.CheckBox Then


                                CType(cctrl, System.Windows.Forms.CheckBox).Checked = CType(IIf(dt.Rows(i).Item(j).ToString.Trim = "1", True, False), Boolean)

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.RadioButton Then
                                CType(cctrl, System.Windows.Forms.RadioButton).Checked = CType(IIf(dt.Rows(i).Item(j).ToString.Trim = "1", True, False), Boolean)

                            End If

                            Exit For
                        End If
                    Next
                Next
            Next

            If Not IsNothing(Me.Owner) Then
                If Not Me.rdoWorkOpt2.Checked Then
                    Me.txtUSDay.Text = rsUsDt.Insert(4, "-").Insert(7, "-").Substring(0, 10)
                    Me.dtpUSTime.Value = CDate(rsUsDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":"))
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdDetail_Test_Partial(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String, Optional ByVal iMode As Integer = 0)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Test_Partial(ByVal asTCd As String, ByVal asUSDT As String)"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mo_DAF.GetTestInfo(rsTestCd, rsSpcCd, rsUsDt)

            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            '''    sbInitialize()

            ''초기화할 것은 Query라벨
            'sbInitialize_Test_QueryLabel()

            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()

            sbInitialize_CtrlCollection()

            fnFindChildControl(Me.Controls)

            If dt.Rows.Count < 1 Then Return

            For i As Integer = 0 To dt.Rows.Count - 1
                For Each cctrl In mchildctrlcol
                    For j As Integer = 0 To dt.Columns.Count - 1
                        If cctrl.Tag.ToString.ToUpper = dt.Columns(j).ColumnName().ToUpper Then
                            mchildctrlcol.Remove(1)

                            If iMode = 0 Then
                                Select Case cctrl.Tag.ToString.ToUpper
                                    Case "TESTCD", "SPCCD", "SPCNMD_01", "USDT", "UEDT", "REGDT", "REGID", _
                                          "RSTTYPE0", "RSTTYPE1", "RSTLEN", "RSTULEN_01", "RSTLLEN_01", _
                                          "CUTOPT1", "CUTOPT2", "CUTOPT3", "REFGBN2", "REFGBN1", "REFGBN0", _
                                          "DESCREF", "RSTUNIT", "JUDGTYPE0", "JUDGTYPE1", "JUDGTYPE2", "JUDGTYPE3", _
                                          "UJUDGLT1", "UJUDGLT2", "UJUDGLT3", "JUDGTYPE11_01", "JUDGTYPE12_01", "JUDGTYPE13_01", _
                                          "PANICGBN_01", "PANICL", "PANICH", "CRITICALGBN_01", "CRITICALL", "CRITICALH", _
                                          "ALERTGBN_01", "ALERTL", "ALERTH", "DELTAGBN_01", "DELTADAY", "DELTAL", "DELTAH", _
                                          "ALIMITGBN_01", "ALIMITL", "ALIMITLS_01", "ALIMITH", "ALIMITHS_01"

                                        Exit For

                                    Case Else

                                End Select
                            ElseIf iMode = 1 Then
                                Select Case cctrl.Tag.ToString.ToUpper
                                    Case "RSTTYPE0", "RSTTYPE1", "RSTLEN", "RSTULEN_01", "RSTLLEN_01", _
                                          "CUTOPT1", "CUTOPT2", "CUTOPT3", "REFGBN2", "REFGBN1", "REFGBN0", _
                                          "DESCREF", "RSTUNIT", "JUDGTYPE0", "JUDGTYPE1", "JUDGTYPE2", "JUDGTYPE3", _
                                          "UJUDGLT1", "UJUDGLT2", "UJUDGLT3", "JUDGTYPE11_01", "JUDGTYPE12_01", "JUDGTYPE13_01", _
                                          "PANICGBN_01", "PANICL", "PANICH", "CRITICALGBN_01", "CRITICALL", "CRITICALH", _
                                          "ALERTGBN_01", "ALERTL", "ALERTH", "DELTAGBN_01", "DELTADAY", "DELTAL", "DELTAH", _
                                          "ALIMITGBN_01", "ALIMITL", "ALIMITLS_01", "ALIMITH", "ALIMITHS_01"

                                    Case Else
                                        Exit For

                                End Select
                            End If

                            If TypeOf (cctrl) Is System.Windows.Forms.ComboBox Then
                                If cctrl.Tag.ToString.EndsWith("_01") = True And CType(cctrl, Windows.Forms.ComboBox).SelectedIndex = -1 Then
                                    iCurIndex = -1

                                    For k As Integer = 0 To CType(cctrl, System.Windows.Forms.ComboBox).Items.Count - 1
                                        If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.EndsWith(dt.Rows(i).Item(j).ToString) = True Then
                                            iCurIndex = k

                                            Exit For
                                        End If

                                        If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.StartsWith(dt.Rows(i).Item(j).ToString) = True Then
                                            iCurIndex = k

                                            Exit For
                                        End If
                                    Next

                                    CType(cctrl, Windows.Forms.ComboBox).SelectedIndex = iCurIndex
                                End If

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.TextBox Then
                                If cctrl.Text.Trim = "" Then
                                    cctrl.Text = dt.Rows(i).Item(j).ToString.Trim
                                End If

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.CheckBox Then
                                If CType(cctrl, Windows.Forms.CheckBox).Visible Then
                                    CType(cctrl, System.Windows.Forms.CheckBox).Checked = CType(IIf(dt.Rows(i).Item(j).ToString = "1", True, False), Boolean)
                                End If

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.RadioButton Then
                                CType(cctrl, System.Windows.Forms.RadioButton).Checked = CType(IIf(dt.Rows(i).Item(j).ToString = "1", True, False), Boolean)

                            End If

                            Exit For
                        End If
                    Next
                Next
            Next

            If Not IsNothing(Me.Owner) Then
                If Not Me.rdoWorkOpt2.Checked And iMode = 0 Then
                    Me.txtUSDay.Text = rsUsDt.Insert(4, "-").Insert(7, "-").Substring(0, 10)
                    Me.dtpUSTime.Value = CDate(rsUsDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":"))
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub


    Public Sub sbDisplayCdDetail_Test_AgeRef(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Test_AgeRef(String, String, String)"

        Try

            Dim iCol As Integer = 0
            Dim dt As DataTable = mo_DAF.GetAgeRefInfo(rsTestCd, rsSpcCd, rsUsDt)

            ''LF061M이 조회되었음을 나타냄
            'lblQuery2.Text = "1"

            '스프레드 초기화
            sbInitialize_spdAgeRef()

            If dt.Rows.Count < 1 Then Return

            With spdAgeRef
                .ReDraw = False

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1

                            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeComboBox Then
                                If IsNumeric(dt.Rows(i).Item(j)) Then
                                    .TypeComboBoxCurSel = CType(dt.Rows(i).Item(j), Short)
                                Else
                                    .TypeComboBoxCurSel = -1
                                End If
                            ElseIf .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then
                                .Text = dt.Rows(i).Item(j).ToString.Trim
                            End If
                        End If
                    Next
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Test_DTest(ByVal rsTestCd As String, ByVal rsSpcCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Test_DTest(String, String)"

        Try

            Dim iCol As Integer = 0
            Dim dt As DataTable = mo_DAF.GetTestInfo_detail(rsTestCd, rsSpcCd)

            ''LF062M이 조회되었음을 나타냄
            'lblQuery3.Text = "1"

            '스프레드 초기화
            sbInitialize_spdDTest()

            If dt.Rows.Count < 1 Then Return

            With spdDTest
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdDetail_Add_Dtest(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Add_Dtest(String, String)"

        Try

            Dim iCol As Integer = 0
            Dim dt As DataTable = mo_DAF.GetTestInfo(rsTestCd, rsSpcCd, rsUsDt)

            If dt.Rows.Count < 1 Then Return

            With spdDTest
                .ReDraw = False
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Dim iRow As Integer = 0

                    For ix2 As Integer = 1 To .MaxRows
                        .Row = ix2
                        .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpccd As String = .Text

                        If dt.Rows(ix).Item("testcd").ToString.Trim + dt.Rows(ix).Item("spccd").ToString.Trim = sTestCd + sSpccd Then
                            iRow = ix2
                            Exit For
                        End If
                    Next

                    If iRow = 0 Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                        .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString
                        .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdDetail_Add_Rtest(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Add_Rtest(String, String)"

        Try

            Dim iCol As Integer = 0
            Dim dt As DataTable = mo_DAF.GetTestInfo(rsTestCd, rsSpcCd, rsUsDt)

            If dt.Rows.Count < 1 Then Return

            With spdRTest
                .ReDraw = False
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Dim iRow As Integer = 0

                    For ix2 As Integer = 1 To .MaxRows
                        .Row = ix2
                        .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpccd As String = .Text

                        If dt.Rows(ix).Item("testcd").ToString.Trim + dt.Rows(ix).Item("spccd").ToString.Trim = sTestCd + sSpccd Then
                            iRow = ix2
                            Exit For
                        End If
                    Next

                    If iRow = 0 Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                        .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString
                        .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdDetail_Test_RTest(ByVal rsTestCd As String, ByVal rsSpcCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Test_RTest(String, String, String)"

        Try

            Dim iCol As Integer = 0
            Dim dt As DataTable = mo_DAF.GetTestInfo_ref(rsTestCd, rsSpcCd)

            '스프레드 초기화
            sbInitialize_spdRTest()

            If dt.Rows.Count < 1 Then Return

            With spdRTest
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString
                        End If
                    Next
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdDetail_Test_Info(ByVal rsTestCd As String, ByVal rsSpcCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Test_Info(String)"

        Try

            Dim iCol As Integer = 0
            Dim dt As DataTable = mo_DAF.GetTestInfo_info(rsTestCd, rsSpcCd)

            Me.txtTestInfo1.Text = ""
            Me.txtTestInfo2.Text = ""
            Me.txtTestInfo3.Text = ""

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Select Case dt.Rows(ix).Item("infogbn").ToString.Trim
                    Case "1" : Me.txtTestInfo1.Text = dt.Rows(ix).Item("testinfo").ToString.Trim
                    Case "2" : Me.txtTestInfo2.Text = dt.Rows(ix).Item("testinfo").ToString.Trim
                    Case "3" : Me.txtTestInfo3.Text = dt.Rows(ix).Item("testinfo").ToString.Trim
                    Case "4"
                        Dim sBuf() As String = dt.Rows(ix).Item("testinfo").ToString.Trim.Split("|"c)
                        For ix2 As Integer = 0 To sBuf.Length - 1
                            With Me.spdOrdCont
                                .MaxRows = sBuf.Length
                                .Row = ix2 + 1
                                .Col = 1 : .Text = sBuf(ix2)
                            End With
                        Next
                End Select
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdDetail_Test_Info_new(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsdt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Test_Info(String)"

        Try

            Dim iCol As Integer = 0
            Dim dt As DataTable = mo_DAF.GetTestInfo_info_new(rsTestCd, rsSpcCd, rsUsdt)


            Me.txtTestInfo5.Text = ""

            If dt.Rows.Count < 1 Then Return

            Me.txtTestInfo5.Text = dt.Rows(0).Item("COWARNING").ToString.Trim                    '<검체 채취 및 의뢰시 주의사항
            'Me.cboEnforcement.SelectedIndex = CInt(IIf(dt.Rows(0).Item("ENFORCEMENT").ToString.Trim = "", -1, dt.Rows(0).Item("ENFORCEMENT").ToString.Trim)) '<시행처 구분
            'Me.CboRequest.SelectedIndex = CInt(IIf(dt.Rows(0).Item("REQUEST").ToString.Trim = "", -1, dt.Rows(0).Item("REQUEST").ToString.Trim))         '<검사의뢰서/동의서
            Me.txtSpcUnit.Text = dt.Rows(0).Item("spcunit").ToString.Trim

            '<< 시행처
            For ix As Integer = 0 To dt.Rows(0).Item("ENFORCEMENT").ToString.Trim.Length - 1
                If dt.Rows(0).Item("ENFORCEMENT").ToString.Trim.Substring(ix, 1) = "1" Then
                    Select Case ix
                        Case 0 : Me.chkEnf0.Checked = True
                        Case 1 : Me.chkEnf1.Checked = True
                        Case 2 : Me.chkEnf2.Checked = True
                        Case 3 : Me.chkEnf3.Checked = True
                    End Select
                Else
                    Select Case ix
                        Case 0 : Me.chkEnf0.Checked = False
                        Case 1 : Me.chkEnf1.Checked = False
                        Case 2 : Me.chkEnf2.Checked = False
                        Case 3 : Me.chkEnf3.Checked = False
                    End Select
                End If
            Next

            '<< 검사의뢰서/동의서
            For ix As Integer = 0 To dt.Rows(0).Item("REQUEST").ToString.Trim.Length - 1
                If dt.Rows(0).Item("REQUEST").ToString.Trim.Substring(ix, 1) = "1" Then
                    Select Case ix
                        Case 0 : Me.chkReq0.Checked = True
                        Case 1 : Me.chkReq1.Checked = True
                        Case 2 : Me.chkReq2.Checked = True
                    End Select
                Else
                    Select Case ix
                        Case 0 : Me.chkReq0.Checked = False
                        Case 1 : Me.chkReq1.Checked = False
                        Case 2 : Me.chkReq2.Checked = False
                    End Select
                End If
            Next


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref(ByVal rsUsDt As String, Optional ByVal rsUeDt As String = "30000101")
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref(ByVal asUSDT As String, Optional ByVal asUEDT As String)"

        Try
            miSelectKey = 1

            sbDisplayCdList_Ref_SpcNmD(cboSpcNmD, cboDSpcNmO, cboDSpcNm2, rsUsDt)
            sbDisplayCdList_Ref_TOrdSlip(cboTOrdSlip, rsUsDt)
            sbDisplayCdList_Ref_TubeNmD(cboTubeNmD, rsUsDt)
            sbDisplayCdList_Ref_ExLabNmD(cboExLabNmD, rsUsDt)
            sbDisplayCdList_Ref_BCCLSNMD(cboBcclsNmd, rsUsDt)
            sbDisplayCdList_Ref_SlipNmD(cboSlipNmD, rsUsDt)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0

            If Not IsNothing(Me.Owner) Then
                If Me.rdoWorkOpt2.Checked Then
                    Me.txtSpcCd_Validating(Me.txtSpcCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtDSpcCdO, Nothing)
                    Me.txtSpcCd_Validating(Me.txtDSpcCd2, Nothing)
                    Me.lblTOrdSlip_TextChanged(Nothing, Nothing)
                    Me.txtSpcCd_Validating(Me.txtTubeCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtExLabCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtSlipCd, Nothing)
                    Me.txtSpcCd_Validating(Me.txtBcclsCd, Nothing)
                End If
            End If
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_ExLabNmD(ByVal actrl As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_ExLabNmD(ByVal actrl As System.Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable = mo_DAF.GetExLabInfo()

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("exlabnmd"))
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_SlipNmD(ByVal ro_cbo As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_SlipNmD(ByVal actrl As System.Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List(rsUsDt)

            ro_cbo.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                ro_cbo.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_SpcNmD(ByVal actrl1 As System.Windows.Forms.ComboBox, _
                                            ByVal actrl2 As System.Windows.Forms.ComboBox, _
                                             ByVal actrl3 As System.Windows.Forms.ComboBox, _
                                              ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_SpcNmD(a1, a2, a3, a4, a5)"

        Try
            Dim dt As DataTable = mo_DAF.GetSpcInfo(rsUsDt)

            actrl1.Items.Clear()
            actrl2.Items.Clear()
            actrl3.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            For i As Integer = 0 To dt.Rows.Count - 1
                actrl1.Items.Add(dt.Rows(i).Item("spcnmd"))
                actrl2.Items.Add(dt.Rows(i).Item("spcnmd"))
                actrl3.Items.Add(dt.Rows(i).Item("spcnmd"))
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_TOrdSlip(ByVal actrl As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_TOrdSlip(ByVal actrl As System.Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable = mo_DAF.GetTOrdSlipInfo(rsUsDt)

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("tordslipnmd"))
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_BCCLSNMD(ByVal actrl As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_TSectNmD(ByVal actrl As System.Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable = mo_DAF.GetBcclsInfo(rsUsDt)

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("bcclsnmd"))
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Ref_TubeNmD(ByVal actrl As System.Windows.Forms.ComboBox, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdList_Ref_TubeNmD(ByVal actrl As System.Windows.Forms.ComboBox)"

        Try
            Dim dt As DataTable = mo_DAF.GetTubeInfo(rsUsDt)

            actrl.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            With actrl
                For i As Integer = 0 To dt.Rows.Count - 1
                    actrl.Items.Add(dt.Rows(i).Item("TUBENMD"))
                Next
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDeleteCdList(ByVal testcd As String, ByVal spccd As String)
        Dim sFn As String = "Public Sub sbDeleteCdList()"

        Try
            Dim iRow As Integer = 0

            With spdCdList
                For ix As Integer = 1 To .MaxRows

                    .Row = ix
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                    .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text

                    If testcd = sTestCd And spccd = sSpcCd Then
                        iRow = ix
                        Exit For
                    End If
                Next

                .Row = iRow
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbInitialize(Optional ByVal riMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

            miSelectKey = 1

            If USER_INFO.USRID = "ACK" Then btnGetExcel.Visible = True
            Me.txtSpcCd.MaxLength = PRG_CONST.Len_SpcCd
            Me.txtSpcCd0.MaxLength = PRG_CONST.Len_SpcCd
            Me.txtDSpcCdO.MaxLength = PRG_CONST.Len_SpcCd

            Me.btnUE.Enabled = True

            If riMode <> 1 Then Me.chkSpcGbn.Checked = False

            sbDisplay_fixrptusr()

            sbInitialize_ErrProvider()
            sbInitialize_Control(riMode)

            If Ctrl.Get_Code(Me.cboBccls_q) <> "" Then
                Me.txtBcclsCd.Text = Ctrl.Get_Code(Me.cboBccls_q)
                Me.cboBcclsNmd.Text = Me.cboBccls_q.Text
            End If

            If Ctrl.Get_Code(Me.cboTordSlip_q) <> "" Then
                Me.cboTOrdSlip.Text = Me.cboTordSlip_q.Text
            End If

            If Me.cboPSGbn.Text = "분야" And Ctrl.Get_Code(Me.cboPartSlip) <> "" Then
                Me.txtSlipCd.Text = Ctrl.Get_Code(Me.cboPartSlip)
                Me.cboSlipNmD.Text = Me.cboPartSlip.Text
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn As String = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_spdAgeRef()
        Dim sFn As String = "Private Sub sbInitializeControl_spdAgeRef()"

        Try
            With spdAgeRef
                .ReDraw = False : .MaxRows = 0 : .MaxRows = mcAgeRefMaxRow : .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_spdDTest()
        Dim sFn As String = "Private Sub sbInitializeControl_spdDTest()"

        Try
            With spdDTest
                .ReDraw = False : .MaxRows = 0 : .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_spdRTest()
        Dim sFn As String = "Private Sub sbInitializeControl_spdRTest()"

        Try
            With spdRTest
                .ReDraw = False : .MaxRows = 0 : .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_Control(Optional ByVal riMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If riMode = 0 Then
                'tpgTest1 초기화

                txtCprtGbn.SelectedIndex = -1 : txtBconeYN.Checked = False

                txtTestCd.Text = "" : btnUE.Visible = False
                txtSelSpc.Text = "" : txtSpcCd.Text = "" : cboSpcNmD.SelectedIndex = -1

                txtTNm.Text = "" : txtTNmD.Text = "" : txtTNmP.Text = "" : txtTNmBP.Text = ""

                cboTCdGbn.SelectedIndex = -1 : chkTitleYN.Checked = False : chkReqSub.Checked = False : chkReqSub.Visible = False
                chkViwSub.Checked = False : chkViwSub.Visible = False

                txtTNmS.Text = "" : cboTOrdSlip.SelectedIndex = -1 : lblTOrdSlip.Text = ""
                txtDSpcCdO.Text = "" : cboDSpcNmO.SelectedIndex = -1
                txtDSpcCd2.Text = "" : cboDSpcNm2.SelectedIndex = -1

                txtTOrdCd.Text = "" : txtInsuGbn.Text = "" : txtSugaCd.Text = "" : txtEdiCd.Text = "" : chkRptYN.Checked = False
                txtTLisCd.Text = ""

                txtDispSeqO.Text = "" : txtDispSeqL.Text = ""

                chkOrdHIde.Checked = False : chkOReqItem1.Checked = False : chkOReqItem2.Checked = False : chkOReqItem3.Checked = False : chkOReqItem4.Checked = False
                Me.pnlOrdCont.Visible = False : Me.spdOrdCont.MaxRows = 0 : Me.btnOrdContView.Visible = False

                cboOWarningGbn.SelectedIndex = -1 : txtOWarning.Text = ""
                cboOWarningGbn_SelectedIndexChanged(Nothing, Nothing)

                chkIOGbnO.Checked = False : chkIOGbnI.Checked = False

                chkExeDay1.Checked = False : chkExeDay2.Checked = False : chkExeDay3.Checked = False : chkExeDay4.Checked = False
                chkExeDay5.Checked = False : chkExeDay6.Checked = False : chkExeDay7.Checked = False

                chkPtGbn.Checked = False : chkFixRptYN.Checked = False : Me.cboFixRptusr.SelectedIndex = -1
                chkFixRptYN_CheckedChanged(Nothing, Nothing)

                chkTatYN.Checked = False : txtPRptMi.Text = "" : cboPRptMi.SelectedIndex = -1 : txtFRptMI.Text = "" : cboFRptMi.SelectedIndex = -1
                txtRRptST.Text = ""
                chkTATYN_CheckedChanged(Nothing, Nothing)

                txtSRecvLT.Text = "" : txtCWarning.Text = ""

                txtTubeCd.Text = "" : cboTubeNmD.SelectedIndex = -1 : txtTubeVol.Text = "" : txtTubeUnit.Text = "" : txtMinSpcVol.Text = ""

                chkExLabYN.Checked = False : txtExLabCd.Text = "" : cboExLabNmD.SelectedIndex = -1
                chkExLabYN_CheckedChanged(Nothing, Nothing)

                chkSeqTYN.Checked = False : txtSeqTMi.Text = "" : chkCtGbn.Checked = False : chkPoctYN.Checked = False
                chkSeqTYN_CheckedChanged(Nothing, Nothing)

                txtBcclsCd.Text = "" : cboBcclsNmd.SelectedIndex = -1 : txtSlipCd.Text = "" : cboSlipNmD.SelectedIndex = -1 : txtSameCd.Text = ""

                cboMBTType.SelectedIndex = -1 : cboBBTType.SelectedIndex = -1 : cboMGTType.SelectedIndex = -1 : cboBpGbn.SelectedIndex = -1

                txtTClsCd0.Text = "" : txtSpcCd0.Text = "" : txtUSDT.Text = "" : txtUEDT.Text = "" : txtRegDT.Text = "" : txtRegID.Text = ""

                'tpgTest2 초기화
                rdoRstType0.Checked = True : chkRstLen.Checked = False : cboRstULen.SelectedIndex = -1 : cboRstLLen.SelectedIndex = -1
                rdoCutOpt1.Checked = False : rdoCutOpt2.Checked = False : rdoCutOpt3.Checked = False
                chkRstLen_CheckedChanged(Nothing, Nothing)

                rdoRefGbn0.Checked = True : txtRstUnit.Text = ""
                lblDescRef.Text = "" : txtDescRef.Text = "" : txtDescRef.Visible = False : btnDescRefExit.Visible = False

                sbInitialize_spdAgeRef()

                rdoJudgType0.Checked = True

                txtUJudgLT1.Text = "" : cboJudgType1.SelectedIndex = -1
                txtUJudgLT2.Text = "" : cboJudgType2.SelectedIndex = -1
                txtUJudgLT3.Text = "" : cboJudgType3.SelectedIndex = -1

                cboPanicGbn.SelectedIndex = -1 : txtPanicL.Text = "" : txtPanicH.Text = ""
                cboPanicGbn_SelectedIndexChanged(Nothing, Nothing)

                cboCriticalGbn.SelectedIndex = -1 : txtCriticalL.Text = "" : txtCriticalH.Text = ""
                cboCriticalGbn_SelectedIndexChanged(Nothing, Nothing)

                cboAlertGbn.SelectedIndex = -1 : txtAlertL.Text = "" : txtAlertH.Text = ""
                cboAlertGbn_SelectedIndexChanged(Nothing, Nothing)

                cboDeltaGbn.SelectedIndex = -1 : txtDeltaDay.Text = "" : txtDeltaL.Text = "" : txtDeltaH.Text = ""
                cboDeltaGbn_SelectedIndexChanged(Nothing, Nothing)

                cboALimitGbn.SelectedIndex = -1
                txtALimitL.Text = "" : cboALimitLS.SelectedIndex = -1
                txtALimitH.Text = "" : cboALimitHS.SelectedIndex = -1
                cboALimitGbn_SelectedIndexChanged(Nothing, Nothing)

                'tpgTest3 초기화
                spdDTest.MaxRows = 0 : spdRTest.MaxRows = 0
                chkAddModeD.Checked = False : chkAddModeR.Checked = False : chkGrpRstYn.Checked = False

                'txtMinDTCnt.Text = ""
                txtRegNm.Text = ""
                'lblAddModeInfo 초기화
                lblAddModeInfo.Visible = False
                chkFwgbn.Checked = False
                chkCWarning.Checked = False

                'jjh 검사의뢰지침 초기화
                CboRequest.SelectedIndex = -1      '<검사의뢰서/동의서
                cboEnforcement.SelectedIndex = -1  '<시행처구분
                txtTestInfo5.Text = ""             '<검체 채취 및 의뢰시 주의사항

                chkEnf0.Checked = False : chkEnf1.Checked = False : chkEnf2.Checked = False : chkEnf3.Checked = False
                chkReq0.Checked = False : chkReq1.Checked = False : chkReq2.Checked = False


            ElseIf riMode = 1 Then


            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_CtrlCollection()
        mchildctrlcol = Nothing

        mchildctrlcol = New Collection
    End Sub

    'Private Sub sbInitialize_Test_QueryLabel()
    '    lblQuery1.Text = "" : lblQuery2.Text = "" : lblQuery3.Text = ""
    'End Sub

    Public Sub sbSetNewUSDT()
        Dim sFn As String = ""

        Try
            Dim sDate As String = fnGetSystemDT()
            sDate = sDate.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")

#If DEBUG Then
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 0, CType(sDate, Date)), "yyyy-MM-dd 00:00:00")
#Else
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 1, CType(sDate, Date)), "yyyy-MM-dd 00:00:00")
#End If
            miSelectKey = 1

            Me.txtUSDay.Text = sSysDT.Substring(0, 10)
            Me.dtpUSDay.Value = CType(sSysDT, Date)
            Me.dtpUSTime.Value = CType(sSysDT, Date)

            '신규 시작일시에 맞는 CdList를 불러옴
            sbDisplayCdList_Ref(sSysDT.Replace("-", "").Replace(":", ""))
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbInitialize()
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
    Friend WithEvents lblLine5 As System.Windows.Forms.Label
    Friend WithEvents lblLine1 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents pnlRstGbn As System.Windows.Forms.Panel
    Friend WithEvents lblLine10 As System.Windows.Forms.Label
    Friend WithEvents lblLine8 As System.Windows.Forms.Label
    Friend WithEvents lblLine9 As System.Windows.Forms.Label
    Friend WithEvents lblLine11 As System.Windows.Forms.Label
    Friend WithEvents lblText1 As System.Windows.Forms.Label
    Friend WithEvents lbltext2 As System.Windows.Forms.Label
    Friend WithEvents spdAgeRef As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdDTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTCdGbn As System.Windows.Forms.Label
    Friend WithEvents lblTNmBP As System.Windows.Forms.Label
    Friend WithEvents txtTNmBP As System.Windows.Forms.TextBox
    Friend WithEvents lblTNmP As System.Windows.Forms.Label
    Friend WithEvents txtTNmP As System.Windows.Forms.TextBox
    Friend WithEvents lblTNmD As System.Windows.Forms.Label
    Friend WithEvents txtTNmD As System.Windows.Forms.TextBox
    Friend WithEvents lblTNmS As System.Windows.Forms.Label
    Friend WithEvents txtTNmS As System.Windows.Forms.TextBox
    Friend WithEvents lblTNm As System.Windows.Forms.Label
    Friend WithEvents txtTNm As System.Windows.Forms.TextBox
    Friend WithEvents tclTest As System.Windows.Forms.TabControl
    Friend WithEvents grpTInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpTestCd As System.Windows.Forms.GroupBox
    Friend WithEvents grpTInfo2 As System.Windows.Forms.GroupBox
    Friend WithEvents grpDTest As System.Windows.Forms.GroupBox
    Friend WithEvents grpRTest As System.Windows.Forms.GroupBox
    Friend WithEvents txtDispSeqO As System.Windows.Forms.TextBox
    Friend WithEvents lblDispSeqO As System.Windows.Forms.Label
    Friend WithEvents txtSRecvLT As System.Windows.Forms.TextBox
    Friend WithEvents lblRRptST As System.Windows.Forms.Label
    Friend WithEvents txtRRptST As System.Windows.Forms.TextBox
    Friend WithEvents lblSRecvLT As System.Windows.Forms.Label
    Friend WithEvents cboSpcNmD As System.Windows.Forms.ComboBox
    Friend WithEvents lblRstType As System.Windows.Forms.Label
    Friend WithEvents rdoJudgType3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoJudgType2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoJudgType1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoJudgType0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRefGbn1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRefGbn2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRefGbn0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCutOpt3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCutOpt2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCutOpt1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRstType1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRstType0 As System.Windows.Forms.RadioButton
    Friend WithEvents lblJudgType3 As System.Windows.Forms.Label
    Friend WithEvents lblJudgType2 As System.Windows.Forms.Label
    Friend WithEvents lblJudgType1 As System.Windows.Forms.Label
    Friend WithEvents cboJudgType3 As System.Windows.Forms.ComboBox
    Friend WithEvents cboJudgType2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboJudgType1 As System.Windows.Forms.ComboBox
    Friend WithEvents txtUJudgLT3 As System.Windows.Forms.TextBox
    Friend WithEvents lblUJudgLT3 As System.Windows.Forms.Label
    Friend WithEvents txtUJudgLT2 As System.Windows.Forms.TextBox
    Friend WithEvents lblUJudgLT2 As System.Windows.Forms.Label
    Friend WithEvents txtUJudgLT1 As System.Windows.Forms.TextBox
    Friend WithEvents lblUJudgLT1 As System.Windows.Forms.Label
    Friend WithEvents lblJudgType As System.Windows.Forms.Label
    Friend WithEvents btnDescRef As System.Windows.Forms.Button
    Friend WithEvents txtRstUnit As System.Windows.Forms.TextBox
    Friend WithEvents lblRstUnit As System.Windows.Forms.Label
    Friend WithEvents lblCutOpt As System.Windows.Forms.Label
    Friend WithEvents chkRstLen As System.Windows.Forms.CheckBox
    Friend WithEvents cboRstLLen As System.Windows.Forms.ComboBox
    Friend WithEvents lblRstLLen As System.Windows.Forms.Label
    Friend WithEvents cboRstULen As System.Windows.Forms.ComboBox
    Friend WithEvents lblRefGbn As System.Windows.Forms.Label
    Friend WithEvents lblRstULen As System.Windows.Forms.Label
    Friend WithEvents cboALimitHS As System.Windows.Forms.ComboBox
    Friend WithEvents cboALimitLS As System.Windows.Forms.ComboBox
    Friend WithEvents lblALimitHS As System.Windows.Forms.Label
    Friend WithEvents lblALimitLS As System.Windows.Forms.Label
    Friend WithEvents lblALimitH As System.Windows.Forms.Label
    Friend WithEvents lblALimitL As System.Windows.Forms.Label
    Friend WithEvents txtALimitH As System.Windows.Forms.TextBox
    Friend WithEvents cboALimitGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblALimitGbn As System.Windows.Forms.Label
    Friend WithEvents lblDeltaDay As System.Windows.Forms.Label
    Friend WithEvents txtDeltaDay As System.Windows.Forms.TextBox
    Friend WithEvents lblDeltaH As System.Windows.Forms.Label
    Friend WithEvents lblDeltaL As System.Windows.Forms.Label
    Friend WithEvents txtDeltaH As System.Windows.Forms.TextBox
    Friend WithEvents txtDeltaL As System.Windows.Forms.TextBox
    Friend WithEvents cboDeltaGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblDeltaGbn As System.Windows.Forms.Label
    Friend WithEvents lblAlertH As System.Windows.Forms.Label
    Friend WithEvents txtAlertH As System.Windows.Forms.TextBox
    Friend WithEvents cboAlertGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblAlertGbn As System.Windows.Forms.Label
    Friend WithEvents lblCriticalH As System.Windows.Forms.Label
    Friend WithEvents lblCriticalL As System.Windows.Forms.Label
    Friend WithEvents txtCriticalH As System.Windows.Forms.TextBox
    Friend WithEvents txtCriticalL As System.Windows.Forms.TextBox
    Friend WithEvents cboCriticalGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblCriticalGbn As System.Windows.Forms.Label
    Friend WithEvents lblPanicH As System.Windows.Forms.Label
    Friend WithEvents lblPanicL As System.Windows.Forms.Label
    Friend WithEvents txtPanicH As System.Windows.Forms.TextBox
    Friend WithEvents txtPanicL As System.Windows.Forms.TextBox
    Friend WithEvents cboPanicGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblPanicGbn As System.Windows.Forms.Label
    Friend WithEvents btnDTDel As System.Windows.Forms.Button
    Friend WithEvents btnRTDel As System.Windows.Forms.Button
    Friend WithEvents tpgTest1 As System.Windows.Forms.TabPage
    Friend WithEvents tpgTest2 As System.Windows.Forms.TabPage
    Friend WithEvents tpgTest3 As System.Windows.Forms.TabPage
    Friend WithEvents cboTCdGbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtAlertL As System.Windows.Forms.TextBox
    Friend WithEvents txtALimitL As System.Windows.Forms.TextBox
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents txtTClsCd0 As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcCd0 As System.Windows.Forms.TextBox
    Friend WithEvents spdRTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkAddModeD As System.Windows.Forms.CheckBox
    Friend WithEvents chkAddModeR As System.Windows.Forms.CheckBox
    Friend WithEvents lblDescRef As System.Windows.Forms.Label
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents chkErGbn1 As System.Windows.Forms.CheckBox
    Friend WithEvents lblOrdSlip As System.Windows.Forms.Label
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents cboTOrdSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblSpccdO As System.Windows.Forms.Label
    Friend WithEvents txtDSpcCdO As System.Windows.Forms.TextBox
    Friend WithEvents cboDSpcNmO As System.Windows.Forms.ComboBox
    Friend WithEvents lblAlertL As System.Windows.Forms.Label
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents lblTOrdSlip As System.Windows.Forms.Label
    Friend WithEvents chkOrdHIde As System.Windows.Forms.CheckBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents txtDescRef As System.Windows.Forms.TextBox
    Friend WithEvents btnDescRefExit As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGF11))
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
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdoSort_spc = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.rdoSort_test = New System.Windows.Forms.RadioButton()
        Me.rdoSort_lis = New System.Windows.Forms.RadioButton()
        Me.rdoSort_ocs = New System.Windows.Forms.RadioButton()
        Me.chkOrder = New System.Windows.Forms.CheckBox()
        Me.cboOps = New System.Windows.Forms.ComboBox()
        Me.btnQuery = New CButtonLib.CButton()
        Me.rdoSOpt1 = New System.Windows.Forms.RadioButton()
        Me.rdoSOpt0 = New System.Windows.Forms.RadioButton()
        Me.chkNotSpc = New System.Windows.Forms.CheckBox()
        Me.cboTordSlip_q = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboFilter = New System.Windows.Forms.ComboBox()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.cboPartSlip = New System.Windows.Forms.ComboBox()
        Me.cboPSGbn = New System.Windows.Forms.ComboBox()
        Me.cboBccls_q = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkCtGbn_q = New System.Windows.Forms.CheckBox()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnRefExcel = New CButtonLib.CButton()
        Me.txtFieldVal = New System.Windows.Forms.TextBox()
        Me.lblFieldNm = New System.Windows.Forms.Label()
        Me.lblGuide2 = New System.Windows.Forms.Label()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.pnlBotton = New System.Windows.Forms.Panel()
        Me.rdoWorkOpt2 = New System.Windows.Forms.RadioButton()
        Me.rdoWorkOpt1 = New System.Windows.Forms.RadioButton()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnChgUseDt = New CButtonLib.CButton()
        Me.lblAddModeInfo = New System.Windows.Forms.Label()
        Me.tclTest = New System.Windows.Forms.TabControl()
        Me.tpgTest1 = New System.Windows.Forms.TabPage()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.txtUEDT = New System.Windows.Forms.TextBox()
        Me.lblUEDT = New System.Windows.Forms.Label()
        Me.txtRegDT = New System.Windows.Forms.TextBox()
        Me.txtUSDT = New System.Windows.Forms.TextBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblRegDT = New System.Windows.Forms.Label()
        Me.lblUSDT = New System.Windows.Forms.Label()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.grpTInfo1 = New System.Windows.Forms.GroupBox()
        Me.chkReq2 = New System.Windows.Forms.CheckBox()
        Me.chkReq1 = New System.Windows.Forms.CheckBox()
        Me.chkReq0 = New System.Windows.Forms.CheckBox()
        Me.chkEnf3 = New System.Windows.Forms.CheckBox()
        Me.chkEnf2 = New System.Windows.Forms.CheckBox()
        Me.chkEnf1 = New System.Windows.Forms.CheckBox()
        Me.chkEnf0 = New System.Windows.Forms.CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtSpcUnit = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CboRequest = New System.Windows.Forms.ComboBox()
        Me.cboEnforcement = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboErAlramMi = New System.Windows.Forms.ComboBox()
        Me.cboAlramMi = New System.Windows.Forms.ComboBox()
        Me.txtAlramTEr = New System.Windows.Forms.TextBox()
        Me.txtAlramT = New System.Windows.Forms.TextBox()
        Me.cboRPTITEMER = New System.Windows.Forms.ComboBox()
        Me.cboRPTITEM = New System.Windows.Forms.ComboBox()
        Me.lblErRptTime = New System.Windows.Forms.Label()
        Me.lblRptTIME = New System.Windows.Forms.Label()
        Me.cboOWarningGbn = New System.Windows.Forms.ComboBox()
        Me.chkCWarning = New System.Windows.Forms.CheckBox()
        Me.chkFwgbn = New System.Windows.Forms.CheckBox()
        Me.txtCprtcd = New System.Windows.Forms.TextBox()
        Me.lblFErRptMi = New System.Windows.Forms.Label()
        Me.lblPErRptMi = New System.Windows.Forms.Label()
        Me.cboFErRptMi = New System.Windows.Forms.ComboBox()
        Me.txtFErRptMI = New System.Windows.Forms.TextBox()
        Me.cboPErRptMi = New System.Windows.Forms.ComboBox()
        Me.txtPErRptMi = New System.Windows.Forms.TextBox()
        Me.cboFixRptusr = New System.Windows.Forms.ComboBox()
        Me.chkSignRptYn = New System.Windows.Forms.CheckBox()
        Me.cboBldGbn = New System.Windows.Forms.ComboBox()
        Me.txtDefrst = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkErGbn2 = New System.Windows.Forms.CheckBox()
        Me.btnOrdContView = New System.Windows.Forms.Button()
        Me.lblCWarning = New System.Windows.Forms.Label()
        Me.pnlOrdCont = New System.Windows.Forms.Panel()
        Me.btnOrdContAdd = New System.Windows.Forms.Button()
        Me.btnOrdContDel = New System.Windows.Forms.Button()
        Me.spdOrdCont = New AxFPSpreadADO.AxfpSpread()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnOrdContExit = New System.Windows.Forms.Button()
        Me.btnReg_dispseqO = New System.Windows.Forms.Button()
        Me.txtTLisCd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnReg_dispseql = New System.Windows.Forms.Button()
        Me.cboSlipNmD = New System.Windows.Forms.ComboBox()
        Me.txtSlipCd = New System.Windows.Forms.TextBox()
        Me.lblSlipCd = New System.Windows.Forms.Label()
        Me.txtBconeYN = New System.Windows.Forms.CheckBox()
        Me.chkOReqItem4 = New System.Windows.Forms.CheckBox()
        Me.lblOReqItem = New System.Windows.Forms.Label()
        Me.lblTOrdgbn = New System.Windows.Forms.Label()
        Me.chkOReqItem3 = New System.Windows.Forms.CheckBox()
        Me.chkOReqItem1 = New System.Windows.Forms.CheckBox()
        Me.chkOReqItem2 = New System.Windows.Forms.CheckBox()
        Me.chkPtGbn = New System.Windows.Forms.CheckBox()
        Me.chkPoctYN = New System.Windows.Forms.CheckBox()
        Me.chkCtGbn = New System.Windows.Forms.CheckBox()
        Me.btnExeDay = New System.Windows.Forms.Button()
        Me.chkExeDay7 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay6 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay5 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay4 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay3 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay2 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay1 = New System.Windows.Forms.CheckBox()
        Me.lblExeDay = New System.Windows.Forms.Label()
        Me.txtCWarning = New System.Windows.Forms.TextBox()
        Me.txtCprtGbn = New System.Windows.Forms.ComboBox()
        Me.lblCprtGbn = New System.Windows.Forms.Label()
        Me.txtEdiCd = New System.Windows.Forms.TextBox()
        Me.lblEdiCd = New System.Windows.Forms.Label()
        Me.txtSugaCd = New System.Windows.Forms.TextBox()
        Me.lblSugaCd = New System.Windows.Forms.Label()
        Me.txtInsuGbn = New System.Windows.Forms.TextBox()
        Me.lblInsuGbn = New System.Windows.Forms.Label()
        Me.txtTOrdCd = New System.Windows.Forms.TextBox()
        Me.lblTOrdCd = New System.Windows.Forms.Label()
        Me.lblIOGbn = New System.Windows.Forms.Label()
        Me.chkIOGbnI = New System.Windows.Forms.CheckBox()
        Me.chkIOGbnO = New System.Windows.Forms.CheckBox()
        Me.cboTubeNmD = New System.Windows.Forms.ComboBox()
        Me.txtTubeCd = New System.Windows.Forms.TextBox()
        Me.lblTubeCd = New System.Windows.Forms.Label()
        Me.txtDSpcCd2 = New System.Windows.Forms.TextBox()
        Me.cboDSpcNm2 = New System.Windows.Forms.ComboBox()
        Me.lblDSpcNm2 = New System.Windows.Forms.Label()
        Me.txtOWarning = New System.Windows.Forms.TextBox()
        Me.lblOWarning = New System.Windows.Forms.Label()
        Me.lblLine2 = New System.Windows.Forms.Label()
        Me.lblBpGbn = New System.Windows.Forms.Label()
        Me.cboBpGbn = New System.Windows.Forms.ComboBox()
        Me.lblLine7 = New System.Windows.Forms.Label()
        Me.txtTubeUnit = New System.Windows.Forms.TextBox()
        Me.txtTubeVol = New System.Windows.Forms.TextBox()
        Me.cboMGTType = New System.Windows.Forms.ComboBox()
        Me.cboBBTType = New System.Windows.Forms.ComboBox()
        Me.cboMBTType = New System.Windows.Forms.ComboBox()
        Me.txtSameCd = New System.Windows.Forms.TextBox()
        Me.lblSameCd = New System.Windows.Forms.Label()
        Me.lblMGTType = New System.Windows.Forms.Label()
        Me.lblBBTType = New System.Windows.Forms.Label()
        Me.lblMBTType = New System.Windows.Forms.Label()
        Me.txtSeqTMi = New System.Windows.Forms.TextBox()
        Me.lblSeqTMi = New System.Windows.Forms.Label()
        Me.chkSeqTYN = New System.Windows.Forms.CheckBox()
        Me.cboExLabNmD = New System.Windows.Forms.ComboBox()
        Me.txtExLabCd = New System.Windows.Forms.TextBox()
        Me.chkExLabYN = New System.Windows.Forms.CheckBox()
        Me.lblExLabCd = New System.Windows.Forms.Label()
        Me.txtMinSpcVol = New System.Windows.Forms.TextBox()
        Me.lblMinSpcVol = New System.Windows.Forms.Label()
        Me.lblTube = New System.Windows.Forms.Label()
        Me.lblTubeVol = New System.Windows.Forms.Label()
        Me.cboBcclsNmd = New System.Windows.Forms.ComboBox()
        Me.txtBcclsCd = New System.Windows.Forms.TextBox()
        Me.lblBcclsCd = New System.Windows.Forms.Label()
        Me.txtDispSeqL = New System.Windows.Forms.TextBox()
        Me.lblDispSeqL = New System.Windows.Forms.Label()
        Me.lblFRptMi = New System.Windows.Forms.Label()
        Me.lblPRptMi = New System.Windows.Forms.Label()
        Me.chkViwSub = New System.Windows.Forms.CheckBox()
        Me.lblLine3 = New System.Windows.Forms.Label()
        Me.chkReqSub = New System.Windows.Forms.CheckBox()
        Me.chkFixRptYN = New System.Windows.Forms.CheckBox()
        Me.cboFRptMi = New System.Windows.Forms.ComboBox()
        Me.txtFRptMI = New System.Windows.Forms.TextBox()
        Me.cboPRptMi = New System.Windows.Forms.ComboBox()
        Me.chkRptYN = New System.Windows.Forms.CheckBox()
        Me.chkTatYN = New System.Windows.Forms.CheckBox()
        Me.txtPRptMi = New System.Windows.Forms.TextBox()
        Me.lblLine6 = New System.Windows.Forms.Label()
        Me.lblLine4 = New System.Windows.Forms.Label()
        Me.chkOrdHIde = New System.Windows.Forms.CheckBox()
        Me.cboDSpcNmO = New System.Windows.Forms.ComboBox()
        Me.lblSpccdO = New System.Windows.Forms.Label()
        Me.txtDSpcCdO = New System.Windows.Forms.TextBox()
        Me.cboTOrdSlip = New System.Windows.Forms.ComboBox()
        Me.lblOrdSlip = New System.Windows.Forms.Label()
        Me.chkErGbn1 = New System.Windows.Forms.CheckBox()
        Me.txtSRecvLT = New System.Windows.Forms.TextBox()
        Me.lblRRptST = New System.Windows.Forms.Label()
        Me.txtRRptST = New System.Windows.Forms.TextBox()
        Me.lblSRecvLT = New System.Windows.Forms.Label()
        Me.txtDispSeqO = New System.Windows.Forms.TextBox()
        Me.lblDispSeqO = New System.Windows.Forms.Label()
        Me.lblLine5 = New System.Windows.Forms.Label()
        Me.lblLine1 = New System.Windows.Forms.Label()
        Me.cboTCdGbn = New System.Windows.Forms.ComboBox()
        Me.lblTCdGbn = New System.Windows.Forms.Label()
        Me.lblTNmBP = New System.Windows.Forms.Label()
        Me.txtTNmBP = New System.Windows.Forms.TextBox()
        Me.lblTNmP = New System.Windows.Forms.Label()
        Me.txtTNmP = New System.Windows.Forms.TextBox()
        Me.lblTNmD = New System.Windows.Forms.Label()
        Me.txtTNmD = New System.Windows.Forms.TextBox()
        Me.lblTNmS = New System.Windows.Forms.Label()
        Me.txtTNmS = New System.Windows.Forms.TextBox()
        Me.lblTNm = New System.Windows.Forms.Label()
        Me.txtTNm = New System.Windows.Forms.TextBox()
        Me.lblTOrdSlip = New System.Windows.Forms.Label()
        Me.chkTitleYN = New System.Windows.Forms.CheckBox()
        Me.spdRef = New AxFPSpreadADO.AxfpSpread()
        Me.grpTestCd = New System.Windows.Forms.GroupBox()
        Me.btnClear_spc = New System.Windows.Forms.Button()
        Me.txtSelSpc = New System.Windows.Forms.TextBox()
        Me.btnCdHelp_spc = New System.Windows.Forms.Button()
        Me.chkSpcGbn = New System.Windows.Forms.CheckBox()
        Me.txtTestCd = New System.Windows.Forms.TextBox()
        Me.btnGetExcel = New System.Windows.Forms.Button()
        Me.btnUE = New System.Windows.Forms.Button()
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker()
        Me.txtUSDay = New System.Windows.Forms.TextBox()
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker()
        Me.lblUSDayTime = New System.Windows.Forms.Label()
        Me.cboSpcNmD = New System.Windows.Forms.ComboBox()
        Me.lblSpcCd = New System.Windows.Forms.Label()
        Me.txtSpcCd = New System.Windows.Forms.TextBox()
        Me.txtTClsCd0 = New System.Windows.Forms.TextBox()
        Me.lblTestCd = New System.Windows.Forms.Label()
        Me.txtSpcCd0 = New System.Windows.Forms.TextBox()
        Me.tpgTest2 = New System.Windows.Forms.TabPage()
        Me.grpTInfo2 = New System.Windows.Forms.GroupBox()
        Me.btnDescRefExit = New System.Windows.Forms.Button()
        Me.txtDescRef = New System.Windows.Forms.TextBox()
        Me.lblDescRef = New System.Windows.Forms.Label()
        Me.spdAgeRef = New AxFPSpreadADO.AxfpSpread()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.rdoJudgType3 = New System.Windows.Forms.RadioButton()
        Me.rdoJudgType2 = New System.Windows.Forms.RadioButton()
        Me.rdoJudgType1 = New System.Windows.Forms.RadioButton()
        Me.rdoJudgType0 = New System.Windows.Forms.RadioButton()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rdoRefGbn1 = New System.Windows.Forms.RadioButton()
        Me.rdoRefGbn2 = New System.Windows.Forms.RadioButton()
        Me.rdoRefGbn0 = New System.Windows.Forms.RadioButton()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdoCutOpt3 = New System.Windows.Forms.RadioButton()
        Me.rdoCutOpt2 = New System.Windows.Forms.RadioButton()
        Me.rdoCutOpt1 = New System.Windows.Forms.RadioButton()
        Me.pnlRstGbn = New System.Windows.Forms.Panel()
        Me.rdoRstType1 = New System.Windows.Forms.RadioButton()
        Me.rdoRstType0 = New System.Windows.Forms.RadioButton()
        Me.cboALimitHS = New System.Windows.Forms.ComboBox()
        Me.cboALimitLS = New System.Windows.Forms.ComboBox()
        Me.lblALimitHS = New System.Windows.Forms.Label()
        Me.lblALimitLS = New System.Windows.Forms.Label()
        Me.lblJudgType3 = New System.Windows.Forms.Label()
        Me.lblJudgType2 = New System.Windows.Forms.Label()
        Me.lblJudgType1 = New System.Windows.Forms.Label()
        Me.lblALimitH = New System.Windows.Forms.Label()
        Me.lblALimitL = New System.Windows.Forms.Label()
        Me.txtALimitH = New System.Windows.Forms.TextBox()
        Me.txtALimitL = New System.Windows.Forms.TextBox()
        Me.cboALimitGbn = New System.Windows.Forms.ComboBox()
        Me.lblALimitGbn = New System.Windows.Forms.Label()
        Me.lblDeltaDay = New System.Windows.Forms.Label()
        Me.txtDeltaDay = New System.Windows.Forms.TextBox()
        Me.lblLine10 = New System.Windows.Forms.Label()
        Me.lblDeltaH = New System.Windows.Forms.Label()
        Me.lblDeltaL = New System.Windows.Forms.Label()
        Me.txtDeltaH = New System.Windows.Forms.TextBox()
        Me.txtDeltaL = New System.Windows.Forms.TextBox()
        Me.cboDeltaGbn = New System.Windows.Forms.ComboBox()
        Me.lblDeltaGbn = New System.Windows.Forms.Label()
        Me.lblAlertH = New System.Windows.Forms.Label()
        Me.lblAlertL = New System.Windows.Forms.Label()
        Me.txtAlertH = New System.Windows.Forms.TextBox()
        Me.txtAlertL = New System.Windows.Forms.TextBox()
        Me.cboAlertGbn = New System.Windows.Forms.ComboBox()
        Me.lblAlertGbn = New System.Windows.Forms.Label()
        Me.lblCriticalH = New System.Windows.Forms.Label()
        Me.lblCriticalL = New System.Windows.Forms.Label()
        Me.txtCriticalH = New System.Windows.Forms.TextBox()
        Me.txtCriticalL = New System.Windows.Forms.TextBox()
        Me.cboCriticalGbn = New System.Windows.Forms.ComboBox()
        Me.lblCriticalGbn = New System.Windows.Forms.Label()
        Me.lblPanicH = New System.Windows.Forms.Label()
        Me.lblPanicL = New System.Windows.Forms.Label()
        Me.txtPanicH = New System.Windows.Forms.TextBox()
        Me.txtPanicL = New System.Windows.Forms.TextBox()
        Me.cboPanicGbn = New System.Windows.Forms.ComboBox()
        Me.lblPanicGbn = New System.Windows.Forms.Label()
        Me.cboJudgType3 = New System.Windows.Forms.ComboBox()
        Me.cboJudgType2 = New System.Windows.Forms.ComboBox()
        Me.cboJudgType1 = New System.Windows.Forms.ComboBox()
        Me.txtUJudgLT3 = New System.Windows.Forms.TextBox()
        Me.lblUJudgLT3 = New System.Windows.Forms.Label()
        Me.txtUJudgLT2 = New System.Windows.Forms.TextBox()
        Me.lblUJudgLT2 = New System.Windows.Forms.Label()
        Me.txtUJudgLT1 = New System.Windows.Forms.TextBox()
        Me.lblUJudgLT1 = New System.Windows.Forms.Label()
        Me.lblJudgType = New System.Windows.Forms.Label()
        Me.btnDescRef = New System.Windows.Forms.Button()
        Me.txtRstUnit = New System.Windows.Forms.TextBox()
        Me.lblRstUnit = New System.Windows.Forms.Label()
        Me.lblCutOpt = New System.Windows.Forms.Label()
        Me.chkRstLen = New System.Windows.Forms.CheckBox()
        Me.cboRstLLen = New System.Windows.Forms.ComboBox()
        Me.lblRstLLen = New System.Windows.Forms.Label()
        Me.cboRstULen = New System.Windows.Forms.ComboBox()
        Me.lblRstType = New System.Windows.Forms.Label()
        Me.lblLine8 = New System.Windows.Forms.Label()
        Me.lblLine9 = New System.Windows.Forms.Label()
        Me.lblLine11 = New System.Windows.Forms.Label()
        Me.lblRefGbn = New System.Windows.Forms.Label()
        Me.lblRstULen = New System.Windows.Forms.Label()
        Me.tpgTest3 = New System.Windows.Forms.TabPage()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTestInfo5 = New System.Windows.Forms.TextBox()
        Me.txtTestInfo4 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTestInfo3 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTestInfo2 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTestInfo1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpDTest = New System.Windows.Forms.GroupBox()
        Me.BtnTestChg = New System.Windows.Forms.Button()
        Me.btnDTDel = New System.Windows.Forms.Button()
        Me.chkGrpRstYn = New System.Windows.Forms.CheckBox()
        Me.chkAddModeD = New System.Windows.Forms.CheckBox()
        Me.spdDTest = New AxFPSpreadADO.AxfpSpread()
        Me.lblText1 = New System.Windows.Forms.Label()
        Me.grpRTest = New System.Windows.Forms.GroupBox()
        Me.btnRTDel = New System.Windows.Forms.Button()
        Me.chkAddModeR = New System.Windows.Forms.CheckBox()
        Me.spdRTest = New AxFPSpreadADO.AxfpSpread()
        Me.lbltext2 = New System.Windows.Forms.Label()
        Me.spdList_spc = New AxFPSpreadADO.AxfpSpread()
        Me.spdCdList = New AxFPSpreadADO.AxfpSpread()
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlBotton.SuspendLayout()
        Me.tclTest.SuspendLayout()
        Me.tpgTest1.SuspendLayout()
        Me.grpTInfo1.SuspendLayout()
        Me.pnlOrdCont.SuspendLayout()
        CType(Me.spdOrdCont, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdRef, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTestCd.SuspendLayout()
        Me.tpgTest2.SuspendLayout()
        Me.grpTInfo2.SuspendLayout()
        CType(Me.spdAgeRef, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlRstGbn.SuspendLayout()
        Me.tpgTest3.SuspendLayout()
        Me.grpDTest.SuspendLayout()
        CType(Me.spdDTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRTest.SuspendLayout()
        CType(Me.spdRTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdList_spc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.Controls.Add(Me.GroupBox1)
        Me.pnlTop.Controls.Add(Me.pnlBottom)
        Me.pnlTop.Controls.Add(Me.lblAddModeInfo)
        Me.pnlTop.Controls.Add(Me.tclTest)
        Me.pnlTop.Controls.Add(Me.spdList_spc)
        Me.pnlTop.Controls.Add(Me.spdCdList)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1205, 925)
        Me.pnlTop.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.chkOrder)
        Me.GroupBox1.Controls.Add(Me.cboOps)
        Me.GroupBox1.Controls.Add(Me.btnQuery)
        Me.GroupBox1.Controls.Add(Me.rdoSOpt1)
        Me.GroupBox1.Controls.Add(Me.rdoSOpt0)
        Me.GroupBox1.Controls.Add(Me.chkNotSpc)
        Me.GroupBox1.Controls.Add(Me.cboTordSlip_q)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cboFilter)
        Me.GroupBox1.Controls.Add(Me.txtFilter)
        Me.GroupBox1.Controls.Add(Me.cboPartSlip)
        Me.GroupBox1.Controls.Add(Me.cboPSGbn)
        Me.GroupBox1.Controls.Add(Me.cboBccls_q)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.chkCtGbn_q)
        Me.GroupBox1.Location = New System.Drawing.Point(2, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(414, 149)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.rdoSort_spc)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.rdoSort_test)
        Me.Panel1.Controls.Add(Me.rdoSort_lis)
        Me.Panel1.Controls.Add(Me.rdoSort_ocs)
        Me.Panel1.Location = New System.Drawing.Point(6, 124)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(263, 21)
        Me.Panel1.TabIndex = 214
        '
        'rdoSort_spc
        '
        Me.rdoSort_spc.AutoSize = True
        Me.rdoSort_spc.Location = New System.Drawing.Point(109, 2)
        Me.rdoSort_spc.Name = "rdoSort_spc"
        Me.rdoSort_spc.Size = New System.Drawing.Size(47, 16)
        Me.rdoSort_spc.TabIndex = 210
        Me.rdoSort_spc.Text = "검체"
        Me.rdoSort_spc.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(0, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(57, 12)
        Me.Label10.TabIndex = 209
        Me.Label10.Text = " 표시방법"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdoSort_test
        '
        Me.rdoSort_test.AutoSize = True
        Me.rdoSort_test.Location = New System.Drawing.Point(58, 2)
        Me.rdoSort_test.Name = "rdoSort_test"
        Me.rdoSort_test.Size = New System.Drawing.Size(47, 16)
        Me.rdoSort_test.TabIndex = 213
        Me.rdoSort_test.Text = "검사"
        Me.rdoSort_test.UseVisualStyleBackColor = True
        '
        'rdoSort_lis
        '
        Me.rdoSort_lis.AutoSize = True
        Me.rdoSort_lis.Location = New System.Drawing.Point(160, 2)
        Me.rdoSort_lis.Name = "rdoSort_lis"
        Me.rdoSort_lis.Size = New System.Drawing.Size(41, 16)
        Me.rdoSort_lis.TabIndex = 211
        Me.rdoSort_lis.Text = "LIS"
        Me.rdoSort_lis.UseVisualStyleBackColor = True
        '
        'rdoSort_ocs
        '
        Me.rdoSort_ocs.AutoSize = True
        Me.rdoSort_ocs.Location = New System.Drawing.Point(207, 2)
        Me.rdoSort_ocs.Name = "rdoSort_ocs"
        Me.rdoSort_ocs.Size = New System.Drawing.Size(49, 16)
        Me.rdoSort_ocs.TabIndex = 212
        Me.rdoSort_ocs.Text = "OCS"
        Me.rdoSort_ocs.UseVisualStyleBackColor = True
        '
        'chkOrder
        '
        Me.chkOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkOrder.AutoSize = True
        Me.chkOrder.Location = New System.Drawing.Point(303, 104)
        Me.chkOrder.Name = "chkOrder"
        Me.chkOrder.Size = New System.Drawing.Size(112, 16)
        Me.chkOrder.TabIndex = 208
        Me.chkOrder.Text = "처방가능 항목만"
        Me.chkOrder.UseVisualStyleBackColor = True
        '
        'cboOps
        '
        Me.cboOps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOps.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboOps.FormattingEnabled = True
        Me.cboOps.Items.AddRange(New Object() {"=", ">", "<", ">=", "<=", "LIKE *", "* LIKE *", "* LIKE"})
        Me.cboOps.Location = New System.Drawing.Point(129, 79)
        Me.cboOps.Name = "cboOps"
        Me.cboOps.Size = New System.Drawing.Size(83, 20)
        Me.cboOps.TabIndex = 207
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems1
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 1.0!
        Me.btnQuery.FocalPoints.CenterPtY = 1.0!
        Me.btnQuery.FocalPoints.FocusPtX = 0!
        Me.btnQuery.FocalPoints.FocusPtY = 0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(274, 123)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(137, 21)
        Me.btnQuery.TabIndex = 206
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'rdoSOpt1
        '
        Me.rdoSOpt1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.rdoSOpt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSOpt1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoSOpt1.ForeColor = System.Drawing.Color.White
        Me.rdoSOpt1.Location = New System.Drawing.Point(82, 101)
        Me.rdoSOpt1.Name = "rdoSOpt1"
        Me.rdoSOpt1.Size = New System.Drawing.Size(52, 21)
        Me.rdoSOpt1.TabIndex = 205
        Me.rdoSOpt1.Text = "전체"
        Me.rdoSOpt1.UseVisualStyleBackColor = False
        '
        'rdoSOpt0
        '
        Me.rdoSOpt0.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rdoSOpt0.Checked = True
        Me.rdoSOpt0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSOpt0.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoSOpt0.ForeColor = System.Drawing.Color.Black
        Me.rdoSOpt0.Location = New System.Drawing.Point(6, 101)
        Me.rdoSOpt0.Name = "rdoSOpt0"
        Me.rdoSOpt0.Size = New System.Drawing.Size(76, 21)
        Me.rdoSOpt0.TabIndex = 204
        Me.rdoSOpt0.TabStop = True
        Me.rdoSOpt0.Text = "사용가능"
        Me.rdoSOpt0.UseVisualStyleBackColor = False
        '
        'chkNotSpc
        '
        Me.chkNotSpc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkNotSpc.AutoSize = True
        Me.chkNotSpc.Location = New System.Drawing.Point(201, 104)
        Me.chkNotSpc.Name = "chkNotSpc"
        Me.chkNotSpc.Size = New System.Drawing.Size(100, 16)
        Me.chkNotSpc.TabIndex = 202
        Me.chkNotSpc.Text = "검체코드 제외"
        Me.chkNotSpc.UseVisualStyleBackColor = True
        '
        'cboTordSlip_q
        '
        Me.cboTordSlip_q.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTordSlip_q.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTordSlip_q.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTordSlip_q.FormattingEnabled = True
        Me.cboTordSlip_q.Location = New System.Drawing.Point(77, 35)
        Me.cboTordSlip_q.Name = "cboTordSlip_q"
        Me.cboTordSlip_q.Size = New System.Drawing.Size(332, 20)
        Me.cboTordSlip_q.TabIndex = 199
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(6, 35)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 21)
        Me.Label8.TabIndex = 200
        Me.Label8.Text = " 처방슬립"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboFilter
        '
        Me.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilter.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboFilter.FormattingEnabled = True
        Me.cboFilter.Items.AddRange(New Object() {"검사코드", "검체코드", "처방코드", "결과코드", "검사구분", "검사명", "위탁기관명"})
        Me.cboFilter.Location = New System.Drawing.Point(7, 79)
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(120, 20)
        Me.cboFilter.TabIndex = 198
        '
        'txtFilter
        '
        Me.txtFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFilter.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtFilter.Location = New System.Drawing.Point(215, 79)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(196, 21)
        Me.txtFilter.TabIndex = 197
        '
        'cboPartSlip
        '
        Me.cboPartSlip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPartSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPartSlip.FormattingEnabled = True
        Me.cboPartSlip.Location = New System.Drawing.Point(150, 57)
        Me.cboPartSlip.Name = "cboPartSlip"
        Me.cboPartSlip.Size = New System.Drawing.Size(259, 20)
        Me.cboPartSlip.TabIndex = 196
        '
        'cboPSGbn
        '
        Me.cboPSGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPSGbn.FormattingEnabled = True
        Me.cboPSGbn.Items.AddRange(New Object() {"부서", "분야"})
        Me.cboPSGbn.Location = New System.Drawing.Point(77, 57)
        Me.cboPSGbn.Name = "cboPSGbn"
        Me.cboPSGbn.Size = New System.Drawing.Size(71, 20)
        Me.cboPSGbn.TabIndex = 195
        '
        'cboBccls_q
        '
        Me.cboBccls_q.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboBccls_q.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBccls_q.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBccls_q.FormattingEnabled = True
        Me.cboBccls_q.Location = New System.Drawing.Point(77, 13)
        Me.cboBccls_q.Name = "cboBccls_q"
        Me.cboBccls_q.Size = New System.Drawing.Size(332, 20)
        Me.cboBccls_q.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(6, 57)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 21)
        Me.Label7.TabIndex = 191
        Me.Label7.Text = " 부서/분야"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(6, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 21)
        Me.Label6.TabIndex = 190
        Me.Label6.Text = " 검체분류"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkCtGbn_q
        '
        Me.chkCtGbn_q.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkCtGbn_q.AutoSize = True
        Me.chkCtGbn_q.Location = New System.Drawing.Point(153, 104)
        Me.chkCtGbn_q.Name = "chkCtGbn_q"
        Me.chkCtGbn_q.Size = New System.Drawing.Size(48, 16)
        Me.chkCtGbn_q.TabIndex = 215
        Me.chkCtGbn_q.Text = "특수"
        Me.chkCtGbn_q.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnRefExcel)
        Me.pnlBottom.Controls.Add(Me.txtFieldVal)
        Me.pnlBottom.Controls.Add(Me.lblFieldNm)
        Me.pnlBottom.Controls.Add(Me.lblGuide2)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.pnlBotton)
        Me.pnlBottom.Controls.Add(Me.btnReg)
        Me.pnlBottom.Controls.Add(Me.btnChgUseDt)
        Me.pnlBottom.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 889)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1201, 32)
        Me.pnlBottom.TabIndex = 4
        '
        'btnRefExcel
        '
        Me.btnRefExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnRefExcel.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnRefExcel.ColorFillBlend = CBlendItems2
        Me.btnRefExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnRefExcel.Corners.All = CType(6, Short)
        Me.btnRefExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnRefExcel.Corners.LowerRight = CType(6, Short)
        Me.btnRefExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnRefExcel.Corners.UpperRight = CType(6, Short)
        Me.btnRefExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnRefExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnRefExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnRefExcel.FocalPoints.CenterPtY = 0!
        Me.btnRefExcel.FocalPoints.FocusPtX = 0!
        Me.btnRefExcel.FocalPoints.FocusPtY = 0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnRefExcel.FocusPtTracker = DesignerRectTracker4
        Me.btnRefExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnRefExcel.ForeColor = System.Drawing.Color.White
        Me.btnRefExcel.Image = Nothing
        Me.btnRefExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnRefExcel.ImageIndex = 0
        Me.btnRefExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnRefExcel.Location = New System.Drawing.Point(753, 3)
        Me.btnRefExcel.Name = "btnRefExcel"
        Me.btnRefExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnRefExcel.SideImage = Nothing
        Me.btnRefExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnRefExcel.Size = New System.Drawing.Size(89, 25)
        Me.btnRefExcel.TabIndex = 201
        Me.btnRefExcel.Text = "참고치 Excel "
        Me.btnRefExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnRefExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'txtFieldVal
        '
        Me.txtFieldVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFieldVal.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtFieldVal.Location = New System.Drawing.Point(69, 2)
        Me.txtFieldVal.Name = "txtFieldVal"
        Me.txtFieldVal.Size = New System.Drawing.Size(154, 21)
        Me.txtFieldVal.TabIndex = 200
        Me.txtFieldVal.Text = "코드명"
        Me.txtFieldVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblFieldNm
        '
        Me.lblFieldNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFieldNm.BackColor = System.Drawing.SystemColors.Desktop
        Me.lblFieldNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFieldNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFieldNm.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFieldNm.Location = New System.Drawing.Point(3, 2)
        Me.lblFieldNm.Name = "lblFieldNm"
        Me.lblFieldNm.Size = New System.Drawing.Size(64, 24)
        Me.lblFieldNm.TabIndex = 199
        Me.lblFieldNm.Tag = "0"
        Me.lblFieldNm.Text = "코드"
        Me.lblFieldNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGuide2
        '
        Me.lblGuide2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGuide2.BackColor = System.Drawing.Color.AliceBlue
        Me.lblGuide2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGuide2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGuide2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblGuide2.Location = New System.Drawing.Point(418, 3)
        Me.lblGuide2.Name = "lblGuide2"
        Me.lblGuide2.Size = New System.Drawing.Size(172, 24)
        Me.lblGuide2.TabIndex = 6
        Me.lblGuide2.Text = "관리자 작업 선택  ▶▶▶"
        Me.lblGuide2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems3
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0!
        Me.btnExit.FocalPoints.FocusPtX = 0!
        Me.btnExit.FocalPoints.FocusPtY = 0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker6
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1117, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(79, 25)
        Me.btnExit.TabIndex = 196
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems4
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0!
        Me.btnExcel.FocalPoints.FocusPtX = 0!
        Me.btnExcel.FocalPoints.FocusPtY = 0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker8
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(318, 3)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(97, 25)
        Me.btnExcel.TabIndex = 194
        Me.btnExcel.Text = "Excel 출력(F5)"
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems5
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.5!
        Me.btnClear.FocalPoints.CenterPtY = 0!
        Me.btnClear.FocalPoints.FocusPtX = 0!
        Me.btnClear.FocalPoints.FocusPtY = 0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker10
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1027, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(89, 25)
        Me.btnClear.TabIndex = 197
        Me.btnClear.Text = "화면정리(F6)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'pnlBotton
        '
        Me.pnlBotton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBotton.Controls.Add(Me.rdoWorkOpt2)
        Me.pnlBotton.Controls.Add(Me.rdoWorkOpt1)
        Me.pnlBotton.Location = New System.Drawing.Point(593, 3)
        Me.pnlBotton.Name = "pnlBotton"
        Me.pnlBotton.Size = New System.Drawing.Size(181, 24)
        Me.pnlBotton.TabIndex = 10
        '
        'rdoWorkOpt2
        '
        Me.rdoWorkOpt2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rdoWorkOpt2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWorkOpt2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoWorkOpt2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.rdoWorkOpt2.Location = New System.Drawing.Point(98, 2)
        Me.rdoWorkOpt2.Name = "rdoWorkOpt2"
        Me.rdoWorkOpt2.Size = New System.Drawing.Size(70, 21)
        Me.rdoWorkOpt2.TabIndex = 9
        Me.rdoWorkOpt2.Text = " 신규"
        Me.rdoWorkOpt2.UseVisualStyleBackColor = False
        '
        'rdoWorkOpt1
        '
        Me.rdoWorkOpt1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rdoWorkOpt1.Checked = True
        Me.rdoWorkOpt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWorkOpt1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoWorkOpt1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.rdoWorkOpt1.Location = New System.Drawing.Point(1, 2)
        Me.rdoWorkOpt1.Name = "rdoWorkOpt1"
        Me.rdoWorkOpt1.Size = New System.Drawing.Size(97, 21)
        Me.rdoWorkOpt1.TabIndex = 8
        Me.rdoWorkOpt1.TabStop = True
        Me.rdoWorkOpt1.Text = " 조회, 수정"
        Me.rdoWorkOpt1.UseVisualStyleBackColor = False
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems6
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.5!
        Me.btnReg.FocalPoints.CenterPtY = 0!
        Me.btnReg.FocalPoints.FocusPtX = 0.02061856!
        Me.btnReg.FocalPoints.FocusPtY = 0.16!
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker12
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(941, 3)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(85, 25)
        Me.btnReg.TabIndex = 195
        Me.btnReg.Text = "수정(F2)"
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnChgUseDt
        '
        Me.btnChgUseDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker13.IsActive = False
        DesignerRectTracker13.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker13.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnChgUseDt.CenterPtTracker = DesignerRectTracker13
        CBlendItems7.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems7.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnChgUseDt.ColorFillBlend = CBlendItems7
        Me.btnChgUseDt.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnChgUseDt.Corners.All = CType(6, Short)
        Me.btnChgUseDt.Corners.LowerLeft = CType(6, Short)
        Me.btnChgUseDt.Corners.LowerRight = CType(6, Short)
        Me.btnChgUseDt.Corners.UpperLeft = CType(6, Short)
        Me.btnChgUseDt.Corners.UpperRight = CType(6, Short)
        Me.btnChgUseDt.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnChgUseDt.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnChgUseDt.FocalPoints.CenterPtX = 0.4639175!
        Me.btnChgUseDt.FocalPoints.CenterPtY = 0.16!
        Me.btnChgUseDt.FocalPoints.FocusPtX = 0.02061856!
        Me.btnChgUseDt.FocalPoints.FocusPtY = 0.16!
        DesignerRectTracker14.IsActive = False
        DesignerRectTracker14.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker14.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnChgUseDt.FocusPtTracker = DesignerRectTracker14
        Me.btnChgUseDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnChgUseDt.ForeColor = System.Drawing.Color.White
        Me.btnChgUseDt.Image = Nothing
        Me.btnChgUseDt.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnChgUseDt.ImageIndex = 0
        Me.btnChgUseDt.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnChgUseDt.Location = New System.Drawing.Point(843, 3)
        Me.btnChgUseDt.Name = "btnChgUseDt"
        Me.btnChgUseDt.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnChgUseDt.SideImage = Nothing
        Me.btnChgUseDt.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnChgUseDt.Size = New System.Drawing.Size(97, 25)
        Me.btnChgUseDt.TabIndex = 198
        Me.btnChgUseDt.Text = "사용일시 수정"
        Me.btnChgUseDt.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnChgUseDt.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lblAddModeInfo
        '
        Me.lblAddModeInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAddModeInfo.BackColor = System.Drawing.Color.Maroon
        Me.lblAddModeInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAddModeInfo.ForeColor = System.Drawing.Color.White
        Me.lblAddModeInfo.Location = New System.Drawing.Point(430, 859)
        Me.lblAddModeInfo.Name = "lblAddModeInfo"
        Me.lblAddModeInfo.Size = New System.Drawing.Size(764, 20)
        Me.lblAddModeInfo.TabIndex = 3
        Me.lblAddModeInfo.Text = "현재 검사에서 검사추가 모드가 작동 중 입니다.  다른 검사의 작업을 하시려면 검사추가 모드를 중지해야 합니다!!"
        Me.lblAddModeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tclTest
        '
        Me.tclTest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tclTest.Controls.Add(Me.tpgTest1)
        Me.tclTest.Controls.Add(Me.tpgTest2)
        Me.tclTest.Controls.Add(Me.tpgTest3)
        Me.tclTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tclTest.Location = New System.Drawing.Point(419, 0)
        Me.tclTest.Name = "tclTest"
        Me.tclTest.SelectedIndex = 0
        Me.tclTest.Size = New System.Drawing.Size(781, 889)
        Me.tclTest.TabIndex = 0
        Me.tclTest.TabStop = False
        '
        'tpgTest1
        '
        Me.tpgTest1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgTest1.Controls.Add(Me.txtRegNm)
        Me.tpgTest1.Controls.Add(Me.txtUEDT)
        Me.tpgTest1.Controls.Add(Me.lblUEDT)
        Me.tpgTest1.Controls.Add(Me.txtRegDT)
        Me.tpgTest1.Controls.Add(Me.txtUSDT)
        Me.tpgTest1.Controls.Add(Me.lblUserNm)
        Me.tpgTest1.Controls.Add(Me.lblRegDT)
        Me.tpgTest1.Controls.Add(Me.lblUSDT)
        Me.tpgTest1.Controls.Add(Me.txtRegID)
        Me.tpgTest1.Controls.Add(Me.grpTInfo1)
        Me.tpgTest1.Controls.Add(Me.grpTestCd)
        Me.tpgTest1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpgTest1.Location = New System.Drawing.Point(4, 22)
        Me.tpgTest1.Name = "tpgTest1"
        Me.tpgTest1.Size = New System.Drawing.Size(773, 863)
        Me.tpgTest1.TabIndex = 0
        Me.tpgTest1.Text = "검사기본정보"
        Me.tpgTest1.UseVisualStyleBackColor = True
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Location = New System.Drawing.Point(696, 830)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 3
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Location = New System.Drawing.Point(309, 830)
        Me.txtUEDT.Name = "txtUEDT"
        Me.txtUEDT.ReadOnly = True
        Me.txtUEDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUEDT.TabIndex = 0
        Me.txtUEDT.TabStop = False
        Me.txtUEDT.Tag = "UEDT"
        '
        'lblUEDT
        '
        Me.lblUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUEDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Location = New System.Drawing.Point(211, 830)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUEDT.TabIndex = 0
        Me.lblUEDT.Tag = ""
        Me.lblUEDT.Text = "종료일시(선택)"
        Me.lblUEDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Location = New System.Drawing.Point(502, 830)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 0
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'txtUSDT
        '
        Me.txtUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUSDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSDT.Location = New System.Drawing.Point(102, 830)
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUSDT.TabIndex = 0
        Me.txtUSDT.TabStop = False
        Me.txtUSDT.Tag = "USDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(611, 830)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 21)
        Me.lblUserNm.TabIndex = 0
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(417, 830)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUSDT
        '
        Me.lblUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDT.ForeColor = System.Drawing.Color.Black
        Me.lblUSDT.Location = New System.Drawing.Point(4, 830)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUSDT.TabIndex = 0
        Me.lblUSDT.Text = "시작일시(선택)"
        Me.lblUSDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Location = New System.Drawing.Point(696, 830)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'grpTInfo1
        '
        Me.grpTInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpTInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTInfo1.Controls.Add(Me.chkReq2)
        Me.grpTInfo1.Controls.Add(Me.chkReq1)
        Me.grpTInfo1.Controls.Add(Me.chkReq0)
        Me.grpTInfo1.Controls.Add(Me.chkEnf3)
        Me.grpTInfo1.Controls.Add(Me.chkEnf2)
        Me.grpTInfo1.Controls.Add(Me.chkEnf1)
        Me.grpTInfo1.Controls.Add(Me.chkEnf0)
        Me.grpTInfo1.Controls.Add(Me.Label20)
        Me.grpTInfo1.Controls.Add(Me.Label19)
        Me.grpTInfo1.Controls.Add(Me.txtSpcUnit)
        Me.grpTInfo1.Controls.Add(Me.Label18)
        Me.grpTInfo1.Controls.Add(Me.CboRequest)
        Me.grpTInfo1.Controls.Add(Me.cboEnforcement)
        Me.grpTInfo1.Controls.Add(Me.Label16)
        Me.grpTInfo1.Controls.Add(Me.Label15)
        Me.grpTInfo1.Controls.Add(Me.Label13)
        Me.grpTInfo1.Controls.Add(Me.Label12)
        Me.grpTInfo1.Controls.Add(Me.cboErAlramMi)
        Me.grpTInfo1.Controls.Add(Me.cboAlramMi)
        Me.grpTInfo1.Controls.Add(Me.txtAlramTEr)
        Me.grpTInfo1.Controls.Add(Me.txtAlramT)
        Me.grpTInfo1.Controls.Add(Me.cboRPTITEMER)
        Me.grpTInfo1.Controls.Add(Me.cboRPTITEM)
        Me.grpTInfo1.Controls.Add(Me.lblErRptTime)
        Me.grpTInfo1.Controls.Add(Me.lblRptTIME)
        Me.grpTInfo1.Controls.Add(Me.cboOWarningGbn)
        Me.grpTInfo1.Controls.Add(Me.chkCWarning)
        Me.grpTInfo1.Controls.Add(Me.chkFwgbn)
        Me.grpTInfo1.Controls.Add(Me.txtCprtcd)
        Me.grpTInfo1.Controls.Add(Me.lblFErRptMi)
        Me.grpTInfo1.Controls.Add(Me.lblPErRptMi)
        Me.grpTInfo1.Controls.Add(Me.cboFErRptMi)
        Me.grpTInfo1.Controls.Add(Me.txtFErRptMI)
        Me.grpTInfo1.Controls.Add(Me.cboPErRptMi)
        Me.grpTInfo1.Controls.Add(Me.txtPErRptMi)
        Me.grpTInfo1.Controls.Add(Me.cboFixRptusr)
        Me.grpTInfo1.Controls.Add(Me.chkSignRptYn)
        Me.grpTInfo1.Controls.Add(Me.cboBldGbn)
        Me.grpTInfo1.Controls.Add(Me.txtDefrst)
        Me.grpTInfo1.Controls.Add(Me.Label11)
        Me.grpTInfo1.Controls.Add(Me.chkErGbn2)
        Me.grpTInfo1.Controls.Add(Me.btnOrdContView)
        Me.grpTInfo1.Controls.Add(Me.lblCWarning)
        Me.grpTInfo1.Controls.Add(Me.pnlOrdCont)
        Me.grpTInfo1.Controls.Add(Me.btnReg_dispseqO)
        Me.grpTInfo1.Controls.Add(Me.txtTLisCd)
        Me.grpTInfo1.Controls.Add(Me.Label1)
        Me.grpTInfo1.Controls.Add(Me.btnReg_dispseql)
        Me.grpTInfo1.Controls.Add(Me.cboSlipNmD)
        Me.grpTInfo1.Controls.Add(Me.txtSlipCd)
        Me.grpTInfo1.Controls.Add(Me.lblSlipCd)
        Me.grpTInfo1.Controls.Add(Me.txtBconeYN)
        Me.grpTInfo1.Controls.Add(Me.chkOReqItem4)
        Me.grpTInfo1.Controls.Add(Me.lblOReqItem)
        Me.grpTInfo1.Controls.Add(Me.lblTOrdgbn)
        Me.grpTInfo1.Controls.Add(Me.chkOReqItem3)
        Me.grpTInfo1.Controls.Add(Me.chkOReqItem1)
        Me.grpTInfo1.Controls.Add(Me.chkOReqItem2)
        Me.grpTInfo1.Controls.Add(Me.chkPtGbn)
        Me.grpTInfo1.Controls.Add(Me.chkPoctYN)
        Me.grpTInfo1.Controls.Add(Me.chkCtGbn)
        Me.grpTInfo1.Controls.Add(Me.btnExeDay)
        Me.grpTInfo1.Controls.Add(Me.chkExeDay7)
        Me.grpTInfo1.Controls.Add(Me.chkExeDay6)
        Me.grpTInfo1.Controls.Add(Me.chkExeDay5)
        Me.grpTInfo1.Controls.Add(Me.chkExeDay4)
        Me.grpTInfo1.Controls.Add(Me.chkExeDay3)
        Me.grpTInfo1.Controls.Add(Me.chkExeDay2)
        Me.grpTInfo1.Controls.Add(Me.chkExeDay1)
        Me.grpTInfo1.Controls.Add(Me.lblExeDay)
        Me.grpTInfo1.Controls.Add(Me.txtCWarning)
        Me.grpTInfo1.Controls.Add(Me.txtCprtGbn)
        Me.grpTInfo1.Controls.Add(Me.lblCprtGbn)
        Me.grpTInfo1.Controls.Add(Me.txtEdiCd)
        Me.grpTInfo1.Controls.Add(Me.lblEdiCd)
        Me.grpTInfo1.Controls.Add(Me.txtSugaCd)
        Me.grpTInfo1.Controls.Add(Me.lblSugaCd)
        Me.grpTInfo1.Controls.Add(Me.txtInsuGbn)
        Me.grpTInfo1.Controls.Add(Me.lblInsuGbn)
        Me.grpTInfo1.Controls.Add(Me.txtTOrdCd)
        Me.grpTInfo1.Controls.Add(Me.lblTOrdCd)
        Me.grpTInfo1.Controls.Add(Me.lblIOGbn)
        Me.grpTInfo1.Controls.Add(Me.chkIOGbnI)
        Me.grpTInfo1.Controls.Add(Me.chkIOGbnO)
        Me.grpTInfo1.Controls.Add(Me.cboTubeNmD)
        Me.grpTInfo1.Controls.Add(Me.txtTubeCd)
        Me.grpTInfo1.Controls.Add(Me.lblTubeCd)
        Me.grpTInfo1.Controls.Add(Me.txtDSpcCd2)
        Me.grpTInfo1.Controls.Add(Me.cboDSpcNm2)
        Me.grpTInfo1.Controls.Add(Me.lblDSpcNm2)
        Me.grpTInfo1.Controls.Add(Me.txtOWarning)
        Me.grpTInfo1.Controls.Add(Me.lblOWarning)
        Me.grpTInfo1.Controls.Add(Me.lblLine2)
        Me.grpTInfo1.Controls.Add(Me.lblBpGbn)
        Me.grpTInfo1.Controls.Add(Me.cboBpGbn)
        Me.grpTInfo1.Controls.Add(Me.lblLine7)
        Me.grpTInfo1.Controls.Add(Me.txtTubeUnit)
        Me.grpTInfo1.Controls.Add(Me.txtTubeVol)
        Me.grpTInfo1.Controls.Add(Me.cboMGTType)
        Me.grpTInfo1.Controls.Add(Me.cboBBTType)
        Me.grpTInfo1.Controls.Add(Me.cboMBTType)
        Me.grpTInfo1.Controls.Add(Me.txtSameCd)
        Me.grpTInfo1.Controls.Add(Me.lblSameCd)
        Me.grpTInfo1.Controls.Add(Me.lblMGTType)
        Me.grpTInfo1.Controls.Add(Me.lblBBTType)
        Me.grpTInfo1.Controls.Add(Me.lblMBTType)
        Me.grpTInfo1.Controls.Add(Me.txtSeqTMi)
        Me.grpTInfo1.Controls.Add(Me.lblSeqTMi)
        Me.grpTInfo1.Controls.Add(Me.chkSeqTYN)
        Me.grpTInfo1.Controls.Add(Me.cboExLabNmD)
        Me.grpTInfo1.Controls.Add(Me.txtExLabCd)
        Me.grpTInfo1.Controls.Add(Me.chkExLabYN)
        Me.grpTInfo1.Controls.Add(Me.lblExLabCd)
        Me.grpTInfo1.Controls.Add(Me.txtMinSpcVol)
        Me.grpTInfo1.Controls.Add(Me.lblMinSpcVol)
        Me.grpTInfo1.Controls.Add(Me.lblTube)
        Me.grpTInfo1.Controls.Add(Me.lblTubeVol)
        Me.grpTInfo1.Controls.Add(Me.cboBcclsNmd)
        Me.grpTInfo1.Controls.Add(Me.txtBcclsCd)
        Me.grpTInfo1.Controls.Add(Me.lblBcclsCd)
        Me.grpTInfo1.Controls.Add(Me.txtDispSeqL)
        Me.grpTInfo1.Controls.Add(Me.lblDispSeqL)
        Me.grpTInfo1.Controls.Add(Me.lblFRptMi)
        Me.grpTInfo1.Controls.Add(Me.lblPRptMi)
        Me.grpTInfo1.Controls.Add(Me.chkViwSub)
        Me.grpTInfo1.Controls.Add(Me.lblLine3)
        Me.grpTInfo1.Controls.Add(Me.chkReqSub)
        Me.grpTInfo1.Controls.Add(Me.chkFixRptYN)
        Me.grpTInfo1.Controls.Add(Me.cboFRptMi)
        Me.grpTInfo1.Controls.Add(Me.txtFRptMI)
        Me.grpTInfo1.Controls.Add(Me.cboPRptMi)
        Me.grpTInfo1.Controls.Add(Me.chkRptYN)
        Me.grpTInfo1.Controls.Add(Me.chkTatYN)
        Me.grpTInfo1.Controls.Add(Me.txtPRptMi)
        Me.grpTInfo1.Controls.Add(Me.lblLine6)
        Me.grpTInfo1.Controls.Add(Me.lblLine4)
        Me.grpTInfo1.Controls.Add(Me.chkOrdHIde)
        Me.grpTInfo1.Controls.Add(Me.cboDSpcNmO)
        Me.grpTInfo1.Controls.Add(Me.lblSpccdO)
        Me.grpTInfo1.Controls.Add(Me.txtDSpcCdO)
        Me.grpTInfo1.Controls.Add(Me.cboTOrdSlip)
        Me.grpTInfo1.Controls.Add(Me.lblOrdSlip)
        Me.grpTInfo1.Controls.Add(Me.chkErGbn1)
        Me.grpTInfo1.Controls.Add(Me.txtSRecvLT)
        Me.grpTInfo1.Controls.Add(Me.lblRRptST)
        Me.grpTInfo1.Controls.Add(Me.txtRRptST)
        Me.grpTInfo1.Controls.Add(Me.lblSRecvLT)
        Me.grpTInfo1.Controls.Add(Me.txtDispSeqO)
        Me.grpTInfo1.Controls.Add(Me.lblDispSeqO)
        Me.grpTInfo1.Controls.Add(Me.lblLine5)
        Me.grpTInfo1.Controls.Add(Me.lblLine1)
        Me.grpTInfo1.Controls.Add(Me.cboTCdGbn)
        Me.grpTInfo1.Controls.Add(Me.lblTCdGbn)
        Me.grpTInfo1.Controls.Add(Me.lblTNmBP)
        Me.grpTInfo1.Controls.Add(Me.txtTNmBP)
        Me.grpTInfo1.Controls.Add(Me.lblTNmP)
        Me.grpTInfo1.Controls.Add(Me.txtTNmP)
        Me.grpTInfo1.Controls.Add(Me.lblTNmD)
        Me.grpTInfo1.Controls.Add(Me.txtTNmD)
        Me.grpTInfo1.Controls.Add(Me.lblTNmS)
        Me.grpTInfo1.Controls.Add(Me.txtTNmS)
        Me.grpTInfo1.Controls.Add(Me.lblTNm)
        Me.grpTInfo1.Controls.Add(Me.txtTNm)
        Me.grpTInfo1.Controls.Add(Me.lblTOrdSlip)
        Me.grpTInfo1.Controls.Add(Me.chkTitleYN)
        Me.grpTInfo1.Controls.Add(Me.spdRef)
        Me.grpTInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpTInfo1.Location = New System.Drawing.Point(4, 79)
        Me.grpTInfo1.Name = "grpTInfo1"
        Me.grpTInfo1.Size = New System.Drawing.Size(764, 737)
        Me.grpTInfo1.TabIndex = 2
        Me.grpTInfo1.TabStop = False
        Me.grpTInfo1.Text = "검사정보"
        '
        'chkReq2
        '
        Me.chkReq2.AutoSize = True
        Me.chkReq2.Location = New System.Drawing.Point(265, 674)
        Me.chkReq2.Name = "chkReq2"
        Me.chkReq2.Size = New System.Drawing.Size(60, 16)
        Me.chkReq2.TabIndex = 287
        Me.chkReq2.Text = "동의서"
        Me.chkReq2.UseVisualStyleBackColor = True
        '
        'chkReq1
        '
        Me.chkReq1.AutoSize = True
        Me.chkReq1.Location = New System.Drawing.Point(197, 674)
        Me.chkReq1.Name = "chkReq1"
        Me.chkReq1.Size = New System.Drawing.Size(60, 16)
        Me.chkReq1.TabIndex = 286
        Me.chkReq1.Text = "의뢰서"
        Me.chkReq1.UseVisualStyleBackColor = True
        '
        'chkReq0
        '
        Me.chkReq0.AutoSize = True
        Me.chkReq0.Location = New System.Drawing.Point(120, 674)
        Me.chkReq0.Name = "chkReq0"
        Me.chkReq0.Size = New System.Drawing.Size(72, 16)
        Me.chkReq0.TabIndex = 285
        Me.chkReq0.Text = "해당없음"
        Me.chkReq0.UseVisualStyleBackColor = True
        '
        'chkEnf3
        '
        Me.chkEnf3.AutoSize = True
        Me.chkEnf3.Location = New System.Drawing.Point(398, 650)
        Me.chkEnf3.Name = "chkEnf3"
        Me.chkEnf3.Size = New System.Drawing.Size(150, 16)
        Me.chkEnf3.TabIndex = 284
        Me.chkEnf3.Text = "국가기관 질병관리본부"
        Me.chkEnf3.UseVisualStyleBackColor = True
        '
        'chkEnf2
        '
        Me.chkEnf2.AutoSize = True
        Me.chkEnf2.Location = New System.Drawing.Point(230, 650)
        Me.chkEnf2.Name = "chkEnf2"
        Me.chkEnf2.Size = New System.Drawing.Size(162, 16)
        Me.chkEnf2.TabIndex = 283
        Me.chkEnf2.Text = "국가기관 보건환경연구원"
        Me.chkEnf2.UseVisualStyleBackColor = True
        '
        'chkEnf1
        '
        Me.chkEnf1.AutoSize = True
        Me.chkEnf1.Location = New System.Drawing.Point(175, 650)
        Me.chkEnf1.Name = "chkEnf1"
        Me.chkEnf1.Size = New System.Drawing.Size(48, 16)
        Me.chkEnf1.TabIndex = 282
        Me.chkEnf1.Text = "원외"
        Me.chkEnf1.UseVisualStyleBackColor = True
        '
        'chkEnf0
        '
        Me.chkEnf0.AutoSize = True
        Me.chkEnf0.Location = New System.Drawing.Point(121, 650)
        Me.chkEnf0.Name = "chkEnf0"
        Me.chkEnf0.Size = New System.Drawing.Size(48, 16)
        Me.chkEnf0.TabIndex = 281
        Me.chkEnf0.Text = "원내"
        Me.chkEnf0.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label20.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(6, 670)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(111, 23)
        Me.Label20.TabIndex = 280
        Me.Label20.Text = "검사의뢰서/동의서"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label19.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(6, 647)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(111, 21)
        Me.Label19.TabIndex = 279
        Me.Label19.Text = "시행처"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcUnit
        '
        Me.txtSpcUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcUnit.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSpcUnit.Location = New System.Drawing.Point(518, 216)
        Me.txtSpcUnit.MaxLength = 40
        Me.txtSpcUnit.Name = "txtSpcUnit"
        Me.txtSpcUnit.Size = New System.Drawing.Size(60, 21)
        Me.txtSpcUnit.TabIndex = 278
        Me.txtSpcUnit.Tag = ""
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(444, 217)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(73, 21)
        Me.Label18.TabIndex = 277
        Me.Label18.Text = "검체단위"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CboRequest
        '
        Me.CboRequest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboRequest.Items.AddRange(New Object() {"[0] 해당없음", "[1] 의뢰서", "[2] 동의서"})
        Me.CboRequest.Location = New System.Drawing.Point(578, 554)
        Me.CboRequest.MaxDropDownItems = 10
        Me.CboRequest.Name = "CboRequest"
        Me.CboRequest.Size = New System.Drawing.Size(174, 20)
        Me.CboRequest.TabIndex = 276
        Me.CboRequest.TabStop = False
        Me.CboRequest.Tag = "REQUEST_01"
        Me.CboRequest.Visible = False
        '
        'cboEnforcement
        '
        Me.cboEnforcement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEnforcement.Items.AddRange(New Object() {"[0] 원내", "[1] 원외", "[2] 국가기관 보건환경연구원", "[3] 국가기관 질병관리본부"})
        Me.cboEnforcement.Location = New System.Drawing.Point(578, 531)
        Me.cboEnforcement.MaxDropDownItems = 10
        Me.cboEnforcement.Name = "cboEnforcement"
        Me.cboEnforcement.Size = New System.Drawing.Size(173, 20)
        Me.cboEnforcement.TabIndex = 275
        Me.cboEnforcement.TabStop = False
        Me.cboEnforcement.Tag = "ENFORCEMENT_01"
        Me.cboEnforcement.Visible = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(453, 552)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(124, 23)
        Me.Label16.TabIndex = 274
        Me.Label16.Text = "검사의뢰서/동의서"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label16.Visible = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label15.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(453, 530)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(124, 21)
        Me.Label15.TabIndex = 273
        Me.Label15.Text = "시행처"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label15.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(343, 473)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(17, 12)
        Me.Label13.TabIndex = 272
        Me.Label13.Text = "전"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(343, 455)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 12)
        Me.Label12.TabIndex = 271
        Me.Label12.Text = "전"
        '
        'cboErAlramMi
        '
        Me.cboErAlramMi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboErAlramMi.Items.AddRange(New Object() {"분:M", "시간:H", "일:D"})
        Me.cboErAlramMi.Location = New System.Drawing.Point(277, 470)
        Me.cboErAlramMi.Name = "cboErAlramMi"
        Me.cboErAlramMi.Size = New System.Drawing.Size(64, 20)
        Me.cboErAlramMi.TabIndex = 270
        Me.cboErAlramMi.Tag = "ALARMTER_01"
        '
        'cboAlramMi
        '
        Me.cboAlramMi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAlramMi.Items.AddRange(New Object() {"분:M", "시간:H", "일:D"})
        Me.cboAlramMi.Location = New System.Drawing.Point(277, 449)
        Me.cboAlramMi.Name = "cboAlramMi"
        Me.cboAlramMi.Size = New System.Drawing.Size(64, 20)
        Me.cboAlramMi.TabIndex = 269
        Me.cboAlramMi.Tag = "ALARMT_01"
        '
        'txtAlramTEr
        '
        Me.txtAlramTEr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlramTEr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAlramTEr.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAlramTEr.Location = New System.Drawing.Point(235, 469)
        Me.txtAlramTEr.MaxLength = 5
        Me.txtAlramTEr.Name = "txtAlramTEr"
        Me.txtAlramTEr.Size = New System.Drawing.Size(40, 21)
        Me.txtAlramTEr.TabIndex = 268
        Me.txtAlramTEr.Tag = "ALARMTER"
        '
        'txtAlramT
        '
        Me.txtAlramT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlramT.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAlramT.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAlramT.Location = New System.Drawing.Point(235, 447)
        Me.txtAlramT.MaxLength = 5
        Me.txtAlramT.Name = "txtAlramT"
        Me.txtAlramT.Size = New System.Drawing.Size(40, 21)
        Me.txtAlramT.TabIndex = 267
        Me.txtAlramT.Tag = "ALARMT"
        '
        'cboRPTITEMER
        '
        Me.cboRPTITEMER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRPTITEMER.Items.AddRange(New Object() {"중간보고", "최종보고"})
        Me.cboRPTITEMER.Location = New System.Drawing.Point(446, 469)
        Me.cboRPTITEMER.Name = "cboRPTITEMER"
        Me.cboRPTITEMER.Size = New System.Drawing.Size(84, 20)
        Me.cboRPTITEMER.TabIndex = 266
        Me.cboRPTITEMER.Tag = "ALARMTYPEER_01"
        Me.cboRPTITEMER.Visible = False
        '
        'cboRPTITEM
        '
        Me.cboRPTITEM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRPTITEM.Items.AddRange(New Object() {"중간보고", "최종보고"})
        Me.cboRPTITEM.Location = New System.Drawing.Point(446, 446)
        Me.cboRPTITEM.Name = "cboRPTITEM"
        Me.cboRPTITEM.Size = New System.Drawing.Size(84, 20)
        Me.cboRPTITEM.TabIndex = 265
        Me.cboRPTITEM.Tag = "ALARMTYPE_01"
        Me.cboRPTITEM.Visible = False
        '
        'lblErRptTime
        '
        Me.lblErRptTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblErRptTime.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblErRptTime.ForeColor = System.Drawing.Color.Black
        Me.lblErRptTime.Location = New System.Drawing.Point(119, 469)
        Me.lblErRptTime.Name = "lblErRptTime"
        Me.lblErRptTime.Size = New System.Drawing.Size(114, 21)
        Me.lblErRptTime.TabIndex = 264
        Me.lblErRptTime.Text = "TAT알람시간(응급)"
        Me.lblErRptTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRptTIME
        '
        Me.lblRptTIME.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRptTIME.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRptTIME.ForeColor = System.Drawing.Color.Black
        Me.lblRptTIME.Location = New System.Drawing.Point(119, 447)
        Me.lblRptTIME.Name = "lblRptTIME"
        Me.lblRptTIME.Size = New System.Drawing.Size(114, 21)
        Me.lblRptTIME.TabIndex = 263
        Me.lblRptTIME.Text = "TAT알람시간"
        Me.lblRptTIME.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboOWarningGbn
        '
        Me.cboOWarningGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOWarningGbn.Items.AddRange(New Object() {"[0] : 없음", "[1] : 일반", "[2] : 팝업"})
        Me.cboOWarningGbn.Location = New System.Drawing.Point(384, 250)
        Me.cboOWarningGbn.Name = "cboOWarningGbn"
        Me.cboOWarningGbn.Size = New System.Drawing.Size(89, 20)
        Me.cboOWarningGbn.TabIndex = 193
        Me.cboOWarningGbn.Tag = "OWARNINGGBN_01"
        '
        'chkCWarning
        '
        Me.chkCWarning.BackColor = System.Drawing.Color.Transparent
        Me.chkCWarning.Location = New System.Drawing.Point(478, 250)
        Me.chkCWarning.Name = "chkCWarning"
        Me.chkCWarning.Size = New System.Drawing.Size(120, 20)
        Me.chkCWarning.TabIndex = 262
        Me.chkCWarning.Tag = "CWGBN"
        Me.chkCWarning.Text = "채혈주의사항팝업"
        Me.chkCWarning.UseVisualStyleBackColor = False
        '
        'chkFwgbn
        '
        Me.chkFwgbn.BackColor = System.Drawing.Color.Transparent
        Me.chkFwgbn.Location = New System.Drawing.Point(560, 145)
        Me.chkFwgbn.Name = "chkFwgbn"
        Me.chkFwgbn.Size = New System.Drawing.Size(131, 17)
        Me.chkFwgbn.TabIndex = 261
        Me.chkFwgbn.Tag = "FWGBN"
        Me.chkFwgbn.Text = "식사관련 채혈주의"
        Me.chkFwgbn.UseVisualStyleBackColor = False
        '
        'txtCprtcd
        '
        Me.txtCprtcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCprtcd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCprtcd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCprtcd.Location = New System.Drawing.Point(267, 336)
        Me.txtCprtcd.MaxLength = 20
        Me.txtCprtcd.Name = "txtCprtcd"
        Me.txtCprtcd.Size = New System.Drawing.Size(89, 21)
        Me.txtCprtcd.TabIndex = 259
        Me.txtCprtcd.Tag = "CPRTCD"
        '
        'lblFErRptMi
        '
        Me.lblFErRptMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblFErRptMi.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFErRptMi.ForeColor = System.Drawing.Color.Black
        Me.lblFErRptMi.Location = New System.Drawing.Point(348, 425)
        Me.lblFErRptMi.Name = "lblFErRptMi"
        Me.lblFErRptMi.Size = New System.Drawing.Size(123, 21)
        Me.lblFErRptMi.TabIndex = 253
        Me.lblFErRptMi.Text = "최종보고기간(응급)"
        Me.lblFErRptMi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPErRptMi
        '
        Me.lblPErRptMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblPErRptMi.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPErRptMi.ForeColor = System.Drawing.Color.Black
        Me.lblPErRptMi.Location = New System.Drawing.Point(119, 425)
        Me.lblPErRptMi.Name = "lblPErRptMi"
        Me.lblPErRptMi.Size = New System.Drawing.Size(114, 21)
        Me.lblPErRptMi.TabIndex = 254
        Me.lblPErRptMi.Text = "중간보고기간(응급)"
        Me.lblPErRptMi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboFErRptMi
        '
        Me.cboFErRptMi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFErRptMi.Items.AddRange(New Object() {"분:M", "시간:H", "일:D"})
        Me.cboFErRptMi.Location = New System.Drawing.Point(513, 425)
        Me.cboFErRptMi.Name = "cboFErRptMi"
        Me.cboFErRptMi.Size = New System.Drawing.Size(64, 20)
        Me.cboFErRptMi.TabIndex = 258
        Me.cboFErRptMi.Tag = "FERRPTMI_01"
        '
        'txtFErRptMI
        '
        Me.txtFErRptMI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFErRptMI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFErRptMI.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtFErRptMI.Location = New System.Drawing.Point(472, 426)
        Me.txtFErRptMI.MaxLength = 5
        Me.txtFErRptMI.Name = "txtFErRptMI"
        Me.txtFErRptMI.Size = New System.Drawing.Size(40, 21)
        Me.txtFErRptMI.TabIndex = 257
        Me.txtFErRptMI.Tag = "FERRPTMI"
        '
        'cboPErRptMi
        '
        Me.cboPErRptMi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPErRptMi.Items.AddRange(New Object() {"분:M", "시간:H", "일:D"})
        Me.cboPErRptMi.Location = New System.Drawing.Point(276, 426)
        Me.cboPErRptMi.Name = "cboPErRptMi"
        Me.cboPErRptMi.Size = New System.Drawing.Size(64, 20)
        Me.cboPErRptMi.TabIndex = 256
        Me.cboPErRptMi.Tag = "PERRPTMI_01"
        '
        'txtPErRptMi
        '
        Me.txtPErRptMi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPErRptMi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPErRptMi.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPErRptMi.Location = New System.Drawing.Point(235, 425)
        Me.txtPErRptMi.MaxLength = 5
        Me.txtPErRptMi.Name = "txtPErRptMi"
        Me.txtPErRptMi.Size = New System.Drawing.Size(40, 21)
        Me.txtPErRptMi.TabIndex = 255
        Me.txtPErRptMi.Tag = "PERRPTMI"
        '
        'cboFixRptusr
        '
        Me.cboFixRptusr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFixRptusr.Location = New System.Drawing.Point(443, 195)
        Me.cboFixRptusr.MaxDropDownItems = 10
        Me.cboFixRptusr.Name = "cboFixRptusr"
        Me.cboFixRptusr.Size = New System.Drawing.Size(135, 20)
        Me.cboFixRptusr.TabIndex = 252
        Me.cboFixRptusr.TabStop = False
        Me.cboFixRptusr.Tag = "FIXRPTUSR_01"
        '
        'chkSignRptYn
        '
        Me.chkSignRptYn.BackColor = System.Drawing.Color.Transparent
        Me.chkSignRptYn.Location = New System.Drawing.Point(425, 490)
        Me.chkSignRptYn.Name = "chkSignRptYn"
        Me.chkSignRptYn.Size = New System.Drawing.Size(121, 20)
        Me.chkSignRptYn.TabIndex = 251
        Me.chkSignRptYn.Tag = "SIGNRPTYN"
        Me.chkSignRptYn.Text = "서식변환 여부"
        Me.chkSignRptYn.UseVisualStyleBackColor = False
        '
        'cboBldGbn
        '
        Me.cboBldGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBldGbn.Items.AddRange(New Object() {"[00] 없음", "[11] ABO 1차", "[12] ABO 2차", "[21] Rh 1차", "[22] Rh 2차"})
        Me.cboBldGbn.Location = New System.Drawing.Point(635, 337)
        Me.cboBldGbn.MaxDropDownItems = 10
        Me.cboBldGbn.Name = "cboBldGbn"
        Me.cboBldGbn.Size = New System.Drawing.Size(116, 20)
        Me.cboBldGbn.TabIndex = 250
        Me.cboBldGbn.Tag = "BLDGBN_01"
        '
        'txtDefrst
        '
        Me.txtDefrst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefrst.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDefrst.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDefrst.Location = New System.Drawing.Point(672, 425)
        Me.txtDefrst.MaxLength = 0
        Me.txtDefrst.Name = "txtDefrst"
        Me.txtDefrst.Size = New System.Drawing.Size(83, 21)
        Me.txtDefrst.TabIndex = 249
        Me.txtDefrst.Tag = "DEFRST"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(594, 425)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 21)
        Me.Label11.TabIndex = 248
        Me.Label11.Text = "기본 결과값"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkErGbn2
        '
        Me.chkErGbn2.BackColor = System.Drawing.Color.Transparent
        Me.chkErGbn2.Location = New System.Drawing.Point(680, 600)
        Me.chkErGbn2.Name = "chkErGbn2"
        Me.chkErGbn2.Size = New System.Drawing.Size(72, 15)
        Me.chkErGbn2.TabIndex = 247
        Me.chkErGbn2.Tag = "ERGBN2"
        Me.chkErGbn2.Text = "당일검사"
        Me.chkErGbn2.UseVisualStyleBackColor = False
        '
        'btnOrdContView
        '
        Me.btnOrdContView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrdContView.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOrdContView.Location = New System.Drawing.Point(311, 250)
        Me.btnOrdContView.Name = "btnOrdContView"
        Me.btnOrdContView.Size = New System.Drawing.Size(54, 21)
        Me.btnOrdContView.TabIndex = 246
        Me.btnOrdContView.Text = "View"
        Me.btnOrdContView.Visible = False
        '
        'lblCWarning
        '
        Me.lblCWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblCWarning.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCWarning.ForeColor = System.Drawing.Color.Black
        Me.lblCWarning.Location = New System.Drawing.Point(478, 271)
        Me.lblCWarning.Name = "lblCWarning"
        Me.lblCWarning.Size = New System.Drawing.Size(90, 32)
        Me.lblCWarning.TabIndex = 245
        Me.lblCWarning.Text = "채혈주의사항"
        Me.lblCWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlOrdCont
        '
        Me.pnlOrdCont.Controls.Add(Me.btnOrdContAdd)
        Me.pnlOrdCont.Controls.Add(Me.btnOrdContDel)
        Me.pnlOrdCont.Controls.Add(Me.spdOrdCont)
        Me.pnlOrdCont.Controls.Add(Me.Label9)
        Me.pnlOrdCont.Controls.Add(Me.btnOrdContExit)
        Me.pnlOrdCont.Location = New System.Drawing.Point(138, 271)
        Me.pnlOrdCont.Name = "pnlOrdCont"
        Me.pnlOrdCont.Size = New System.Drawing.Size(334, 128)
        Me.pnlOrdCont.TabIndex = 244
        Me.pnlOrdCont.Visible = False
        '
        'btnOrdContAdd
        '
        Me.btnOrdContAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrdContAdd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOrdContAdd.Location = New System.Drawing.Point(296, 23)
        Me.btnOrdContAdd.Name = "btnOrdContAdd"
        Me.btnOrdContAdd.Size = New System.Drawing.Size(19, 20)
        Me.btnOrdContAdd.TabIndex = 81
        Me.btnOrdContAdd.TabStop = False
        Me.btnOrdContAdd.Text = "＋"
        '
        'btnOrdContDel
        '
        Me.btnOrdContDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrdContDel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOrdContDel.Location = New System.Drawing.Point(315, 23)
        Me.btnOrdContDel.Name = "btnOrdContDel"
        Me.btnOrdContDel.Size = New System.Drawing.Size(19, 20)
        Me.btnOrdContDel.TabIndex = 82
        Me.btnOrdContDel.TabStop = False
        Me.btnOrdContDel.Text = "－"
        '
        'spdOrdCont
        '
        Me.spdOrdCont.DataSource = Nothing
        Me.spdOrdCont.Location = New System.Drawing.Point(0, 22)
        Me.spdOrdCont.Name = "spdOrdCont"
        Me.spdOrdCont.OcxState = CType(resources.GetObject("spdOrdCont.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrdCont.Size = New System.Drawing.Size(334, 106)
        Me.spdOrdCont.TabIndex = 80
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.DimGray
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(1, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(313, 20)
        Me.Label9.TabIndex = 79
        Me.Label9.Text = "사유코드 등록"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnOrdContExit
        '
        Me.btnOrdContExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOrdContExit.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOrdContExit.Location = New System.Drawing.Point(316, 0)
        Me.btnOrdContExit.Name = "btnOrdContExit"
        Me.btnOrdContExit.Size = New System.Drawing.Size(18, 20)
        Me.btnOrdContExit.TabIndex = 1
        Me.btnOrdContExit.Text = "×"
        '
        'btnReg_dispseqO
        '
        Me.btnReg_dispseqO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReg_dispseqO.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnReg_dispseqO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReg_dispseqO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg_dispseqO.ForeColor = System.Drawing.Color.Black
        Me.btnReg_dispseqO.Location = New System.Drawing.Point(636, 710)
        Me.btnReg_dispseqO.Margin = New System.Windows.Forms.Padding(1)
        Me.btnReg_dispseqO.Name = "btnReg_dispseqO"
        Me.btnReg_dispseqO.Size = New System.Drawing.Size(115, 21)
        Me.btnReg_dispseqO.TabIndex = 243
        Me.btnReg_dispseqO.Text = "처방 정렬 순서"
        Me.btnReg_dispseqO.UseVisualStyleBackColor = False
        '
        'txtTLisCd
        '
        Me.txtTLisCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTLisCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTLisCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTLisCd.Location = New System.Drawing.Point(480, 122)
        Me.txtTLisCd.MaxLength = 20
        Me.txtTLisCd.Name = "txtTLisCd"
        Me.txtTLisCd.Size = New System.Drawing.Size(68, 21)
        Me.txtTLisCd.TabIndex = 242
        Me.txtTLisCd.Tag = "TLISCD"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(396, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 21)
        Me.Label1.TabIndex = 241
        Me.Label1.Text = "결과코드"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnReg_dispseql
        '
        Me.btnReg_dispseql.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReg_dispseql.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnReg_dispseql.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReg_dispseql.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg_dispseql.ForeColor = System.Drawing.Color.Black
        Me.btnReg_dispseql.Location = New System.Drawing.Point(367, 710)
        Me.btnReg_dispseql.Margin = New System.Windows.Forms.Padding(1)
        Me.btnReg_dispseql.Name = "btnReg_dispseql"
        Me.btnReg_dispseql.Size = New System.Drawing.Size(103, 21)
        Me.btnReg_dispseql.TabIndex = 76
        Me.btnReg_dispseql.Text = "LIS 정렬 순서"
        Me.btnReg_dispseql.UseVisualStyleBackColor = False
        '
        'cboSlipNmD
        '
        Me.cboSlipNmD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlipNmD.Location = New System.Drawing.Point(123, 195)
        Me.cboSlipNmD.MaxDropDownItems = 10
        Me.cboSlipNmD.Name = "cboSlipNmD"
        Me.cboSlipNmD.Size = New System.Drawing.Size(176, 20)
        Me.cboSlipNmD.TabIndex = 240
        Me.cboSlipNmD.TabStop = False
        Me.cboSlipNmD.Tag = "SLIPNMD_01"
        '
        'txtSlipCd
        '
        Me.txtSlipCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSlipCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSlipCd.Location = New System.Drawing.Point(98, 195)
        Me.txtSlipCd.MaxLength = 2
        Me.txtSlipCd.Name = "txtSlipCd"
        Me.txtSlipCd.Size = New System.Drawing.Size(24, 21)
        Me.txtSlipCd.TabIndex = 239
        Me.txtSlipCd.Tag = "SLIPCD2"
        '
        'lblSlipCd
        '
        Me.lblSlipCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlipCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlipCd.ForeColor = System.Drawing.Color.White
        Me.lblSlipCd.Location = New System.Drawing.Point(7, 195)
        Me.lblSlipCd.Name = "lblSlipCd"
        Me.lblSlipCd.Size = New System.Drawing.Size(90, 21)
        Me.lblSlipCd.TabIndex = 238
        Me.lblSlipCd.Text = "검사분야"
        Me.lblSlipCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBconeYN
        '
        Me.txtBconeYN.BackColor = System.Drawing.Color.Transparent
        Me.txtBconeYN.Location = New System.Drawing.Point(103, 600)
        Me.txtBconeYN.Name = "txtBconeYN"
        Me.txtBconeYN.Size = New System.Drawing.Size(114, 15)
        Me.txtBconeYN.TabIndex = 234
        Me.txtBconeYN.Tag = "BCONEYN"
        Me.txtBconeYN.Text = "단독검체로 설정"
        Me.txtBconeYN.UseVisualStyleBackColor = False
        '
        'chkOReqItem4
        '
        Me.chkOReqItem4.BackColor = System.Drawing.Color.Transparent
        Me.chkOReqItem4.Location = New System.Drawing.Point(261, 251)
        Me.chkOReqItem4.Name = "chkOReqItem4"
        Me.chkOReqItem4.Size = New System.Drawing.Size(50, 20)
        Me.chkOReqItem4.TabIndex = 233
        Me.chkOReqItem4.Tag = "OREQITEM4"
        Me.chkOReqItem4.Text = "사유"
        Me.chkOReqItem4.UseVisualStyleBackColor = False
        '
        'lblOReqItem
        '
        Me.lblOReqItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblOReqItem.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOReqItem.ForeColor = System.Drawing.Color.Black
        Me.lblOReqItem.Location = New System.Drawing.Point(5, 250)
        Me.lblOReqItem.Name = "lblOReqItem"
        Me.lblOReqItem.Size = New System.Drawing.Size(90, 21)
        Me.lblOReqItem.TabIndex = 232
        Me.lblOReqItem.Text = "처방입력사항"
        Me.lblOReqItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTOrdgbn
        '
        Me.lblTOrdgbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTOrdgbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTOrdgbn.ForeColor = System.Drawing.Color.Black
        Me.lblTOrdgbn.Location = New System.Drawing.Point(6, 599)
        Me.lblTOrdgbn.Name = "lblTOrdgbn"
        Me.lblTOrdgbn.Size = New System.Drawing.Size(93, 21)
        Me.lblTOrdgbn.TabIndex = 231
        Me.lblTOrdgbn.Text = "검사처방설정"
        Me.lblTOrdgbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkOReqItem3
        '
        Me.chkOReqItem3.BackColor = System.Drawing.Color.Transparent
        Me.chkOReqItem3.Location = New System.Drawing.Point(210, 251)
        Me.chkOReqItem3.Name = "chkOReqItem3"
        Me.chkOReqItem3.Size = New System.Drawing.Size(50, 20)
        Me.chkOReqItem3.TabIndex = 230
        Me.chkOReqItem3.Tag = "OREQITEM3"
        Me.chkOReqItem3.Text = "체중"
        Me.chkOReqItem3.UseVisualStyleBackColor = False
        '
        'chkOReqItem1
        '
        Me.chkOReqItem1.BackColor = System.Drawing.Color.Transparent
        Me.chkOReqItem1.Location = New System.Drawing.Point(102, 251)
        Me.chkOReqItem1.Name = "chkOReqItem1"
        Me.chkOReqItem1.Size = New System.Drawing.Size(50, 20)
        Me.chkOReqItem1.TabIndex = 228
        Me.chkOReqItem1.Tag = "OREQITEM1"
        Me.chkOReqItem1.Text = "수량"
        Me.chkOReqItem1.UseVisualStyleBackColor = False
        '
        'chkOReqItem2
        '
        Me.chkOReqItem2.BackColor = System.Drawing.Color.Transparent
        Me.chkOReqItem2.Location = New System.Drawing.Point(156, 251)
        Me.chkOReqItem2.Name = "chkOReqItem2"
        Me.chkOReqItem2.Size = New System.Drawing.Size(50, 20)
        Me.chkOReqItem2.TabIndex = 229
        Me.chkOReqItem2.Tag = "OREQITEM2"
        Me.chkOReqItem2.Text = "신장"
        Me.chkOReqItem2.UseVisualStyleBackColor = False
        '
        'chkPtGbn
        '
        Me.chkPtGbn.BackColor = System.Drawing.Color.Transparent
        Me.chkPtGbn.Location = New System.Drawing.Point(474, 600)
        Me.chkPtGbn.Name = "chkPtGbn"
        Me.chkPtGbn.Size = New System.Drawing.Size(122, 15)
        Me.chkPtGbn.TabIndex = 219
        Me.chkPtGbn.Tag = "PTGBN"
        Me.chkPtGbn.Text = "소아검사로 설정"
        Me.chkPtGbn.UseVisualStyleBackColor = False
        '
        'chkPoctYN
        '
        Me.chkPoctYN.BackColor = System.Drawing.Color.Transparent
        Me.chkPoctYN.Location = New System.Drawing.Point(226, 600)
        Me.chkPoctYN.Name = "chkPoctYN"
        Me.chkPoctYN.Size = New System.Drawing.Size(116, 15)
        Me.chkPoctYN.TabIndex = 217
        Me.chkPoctYN.Tag = "POCTYN"
        Me.chkPoctYN.Text = "현장검사로 설정"
        Me.chkPoctYN.UseVisualStyleBackColor = False
        '
        'chkCtGbn
        '
        Me.chkCtGbn.BackColor = System.Drawing.Color.Transparent
        Me.chkCtGbn.Location = New System.Drawing.Point(350, 600)
        Me.chkCtGbn.Name = "chkCtGbn"
        Me.chkCtGbn.Size = New System.Drawing.Size(115, 15)
        Me.chkCtGbn.TabIndex = 218
        Me.chkCtGbn.Tag = "CTGBN"
        Me.chkCtGbn.Text = "특수검사로 설정"
        Me.chkCtGbn.UseVisualStyleBackColor = False
        '
        'btnExeDay
        '
        Me.btnExeDay.BackColor = System.Drawing.SystemColors.Control
        Me.btnExeDay.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExeDay.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExeDay.Location = New System.Drawing.Point(102, 621)
        Me.btnExeDay.Name = "btnExeDay"
        Me.btnExeDay.Size = New System.Drawing.Size(48, 22)
        Me.btnExeDay.TabIndex = 220
        Me.btnExeDay.TabStop = False
        Me.btnExeDay.Text = "매일"
        Me.btnExeDay.UseVisualStyleBackColor = False
        '
        'chkExeDay7
        '
        Me.chkExeDay7.Location = New System.Drawing.Point(393, 622)
        Me.chkExeDay7.Name = "chkExeDay7"
        Me.chkExeDay7.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay7.TabIndex = 227
        Me.chkExeDay7.Tag = "EXEDAY7"
        Me.chkExeDay7.Text = "일"
        '
        'chkExeDay6
        '
        Me.chkExeDay6.Location = New System.Drawing.Point(352, 622)
        Me.chkExeDay6.Name = "chkExeDay6"
        Me.chkExeDay6.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay6.TabIndex = 226
        Me.chkExeDay6.Tag = "EXEDAY6"
        Me.chkExeDay6.Text = "토"
        '
        'chkExeDay5
        '
        Me.chkExeDay5.Location = New System.Drawing.Point(315, 622)
        Me.chkExeDay5.Name = "chkExeDay5"
        Me.chkExeDay5.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay5.TabIndex = 225
        Me.chkExeDay5.Tag = "EXEDAY5"
        Me.chkExeDay5.Text = "금"
        '
        'chkExeDay4
        '
        Me.chkExeDay4.Location = New System.Drawing.Point(274, 622)
        Me.chkExeDay4.Name = "chkExeDay4"
        Me.chkExeDay4.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay4.TabIndex = 224
        Me.chkExeDay4.Tag = "EXEDAY4"
        Me.chkExeDay4.Text = "목"
        '
        'chkExeDay3
        '
        Me.chkExeDay3.Location = New System.Drawing.Point(237, 622)
        Me.chkExeDay3.Name = "chkExeDay3"
        Me.chkExeDay3.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay3.TabIndex = 223
        Me.chkExeDay3.Tag = "EXEDAY3"
        Me.chkExeDay3.Text = "수"
        '
        'chkExeDay2
        '
        Me.chkExeDay2.Location = New System.Drawing.Point(196, 622)
        Me.chkExeDay2.Name = "chkExeDay2"
        Me.chkExeDay2.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay2.TabIndex = 222
        Me.chkExeDay2.Tag = "EXEDAY2"
        Me.chkExeDay2.Text = "화"
        '
        'chkExeDay1
        '
        Me.chkExeDay1.Location = New System.Drawing.Point(159, 622)
        Me.chkExeDay1.Name = "chkExeDay1"
        Me.chkExeDay1.Size = New System.Drawing.Size(31, 20)
        Me.chkExeDay1.TabIndex = 221
        Me.chkExeDay1.Tag = "EXEDAY1"
        Me.chkExeDay1.Text = "월"
        '
        'lblExeDay
        '
        Me.lblExeDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblExeDay.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExeDay.ForeColor = System.Drawing.Color.Black
        Me.lblExeDay.Location = New System.Drawing.Point(6, 621)
        Me.lblExeDay.Name = "lblExeDay"
        Me.lblExeDay.Size = New System.Drawing.Size(93, 21)
        Me.lblExeDay.TabIndex = 216
        Me.lblExeDay.Text = "실시요일"
        Me.lblExeDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCWarning
        '
        Me.txtCWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCWarning.Location = New System.Drawing.Point(569, 271)
        Me.txtCWarning.MaxLength = 200
        Me.txtCWarning.Multiline = True
        Me.txtCWarning.Name = "txtCWarning"
        Me.txtCWarning.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCWarning.Size = New System.Drawing.Size(184, 31)
        Me.txtCWarning.TabIndex = 195
        Me.txtCWarning.Tag = "CWARNING"
        '
        'txtCprtGbn
        '
        Me.txtCprtGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txtCprtGbn.Items.AddRange(New Object() {"[  ]", "[01] 골수검사의뢰서", "[02] 분자생물학부의뢰서 ", "[03] 산전검사의뢰서", "[04] 세포유전학검사의뢰서", "[05] HLA 의뢰서", "[06] TDM의뢰서"})
        Me.txtCprtGbn.Location = New System.Drawing.Point(97, 337)
        Me.txtCprtGbn.Name = "txtCprtGbn"
        Me.txtCprtGbn.Size = New System.Drawing.Size(169, 20)
        Me.txtCprtGbn.TabIndex = 215
        Me.txtCprtGbn.Tag = "cprtgbn_01"
        '
        'lblCprtGbn
        '
        Me.lblCprtGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblCprtGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCprtGbn.ForeColor = System.Drawing.Color.Black
        Me.lblCprtGbn.Location = New System.Drawing.Point(6, 336)
        Me.lblCprtGbn.Name = "lblCprtGbn"
        Me.lblCprtGbn.Size = New System.Drawing.Size(90, 21)
        Me.lblCprtGbn.TabIndex = 214
        Me.lblCprtGbn.Text = "의뢰지/서식"
        Me.lblCprtGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEdiCd
        '
        Me.txtEdiCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEdiCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEdiCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtEdiCd.Location = New System.Drawing.Point(679, 195)
        Me.txtEdiCd.MaxLength = 10
        Me.txtEdiCd.Name = "txtEdiCd"
        Me.txtEdiCd.Size = New System.Drawing.Size(73, 21)
        Me.txtEdiCd.TabIndex = 213
        Me.txtEdiCd.Tag = "EDICD"
        '
        'lblEdiCd
        '
        Me.lblEdiCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblEdiCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblEdiCd.ForeColor = System.Drawing.Color.Black
        Me.lblEdiCd.Location = New System.Drawing.Point(588, 195)
        Me.lblEdiCd.Name = "lblEdiCd"
        Me.lblEdiCd.Size = New System.Drawing.Size(90, 21)
        Me.lblEdiCd.TabIndex = 206
        Me.lblEdiCd.Text = "청구코드"
        Me.lblEdiCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSugaCd
        '
        Me.txtSugaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSugaCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSugaCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSugaCd.Location = New System.Drawing.Point(679, 173)
        Me.txtSugaCd.MaxLength = 20
        Me.txtSugaCd.Name = "txtSugaCd"
        Me.txtSugaCd.Size = New System.Drawing.Size(73, 21)
        Me.txtSugaCd.TabIndex = 212
        Me.txtSugaCd.Tag = "SUGACD"
        '
        'lblSugaCd
        '
        Me.lblSugaCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSugaCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSugaCd.ForeColor = System.Drawing.Color.Black
        Me.lblSugaCd.Location = New System.Drawing.Point(588, 173)
        Me.lblSugaCd.Name = "lblSugaCd"
        Me.lblSugaCd.Size = New System.Drawing.Size(90, 21)
        Me.lblSugaCd.TabIndex = 207
        Me.lblSugaCd.Text = "수가코드"
        Me.lblSugaCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInsuGbn
        '
        Me.txtInsuGbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInsuGbn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInsuGbn.Location = New System.Drawing.Point(679, 217)
        Me.txtInsuGbn.MaxLength = 10
        Me.txtInsuGbn.Name = "txtInsuGbn"
        Me.txtInsuGbn.Size = New System.Drawing.Size(73, 21)
        Me.txtInsuGbn.TabIndex = 211
        Me.txtInsuGbn.Tag = "INSUGBN"
        '
        'lblInsuGbn
        '
        Me.lblInsuGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblInsuGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblInsuGbn.ForeColor = System.Drawing.Color.Black
        Me.lblInsuGbn.Location = New System.Drawing.Point(588, 218)
        Me.lblInsuGbn.Name = "lblInsuGbn"
        Me.lblInsuGbn.Size = New System.Drawing.Size(90, 21)
        Me.lblInsuGbn.TabIndex = 208
        Me.lblInsuGbn.Text = "보험분류"
        Me.lblInsuGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTOrdCd
        '
        Me.txtTOrdCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTOrdCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTOrdCd.Location = New System.Drawing.Point(480, 100)
        Me.txtTOrdCd.MaxLength = 20
        Me.txtTOrdCd.Name = "txtTOrdCd"
        Me.txtTOrdCd.Size = New System.Drawing.Size(68, 21)
        Me.txtTOrdCd.TabIndex = 210
        Me.txtTOrdCd.Tag = "TORDCD"
        '
        'lblTOrdCd
        '
        Me.lblTOrdCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTOrdCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTOrdCd.ForeColor = System.Drawing.Color.Black
        Me.lblTOrdCd.Location = New System.Drawing.Point(396, 100)
        Me.lblTOrdCd.Name = "lblTOrdCd"
        Me.lblTOrdCd.Size = New System.Drawing.Size(83, 21)
        Me.lblTOrdCd.TabIndex = 209
        Me.lblTOrdCd.Text = "검사처방코드"
        Me.lblTOrdCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIOGbn
        '
        Me.lblIOGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIOGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIOGbn.ForeColor = System.Drawing.Color.White
        Me.lblIOGbn.Location = New System.Drawing.Point(559, 100)
        Me.lblIOGbn.Name = "lblIOGbn"
        Me.lblIOGbn.Size = New System.Drawing.Size(83, 21)
        Me.lblIOGbn.TabIndex = 205
        Me.lblIOGbn.Text = "검사처방조건"
        Me.lblIOGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkIOGbnI
        '
        Me.chkIOGbnI.BackColor = System.Drawing.Color.Transparent
        Me.chkIOGbnI.ForeColor = System.Drawing.Color.Black
        Me.chkIOGbnI.Location = New System.Drawing.Point(700, 101)
        Me.chkIOGbnI.Name = "chkIOGbnI"
        Me.chkIOGbnI.Size = New System.Drawing.Size(53, 21)
        Me.chkIOGbnI.TabIndex = 204
        Me.chkIOGbnI.Tag = "IOGBN1"
        Me.chkIOGbnI.Text = "병동"
        Me.chkIOGbnI.UseVisualStyleBackColor = False
        '
        'chkIOGbnO
        '
        Me.chkIOGbnO.BackColor = System.Drawing.Color.Transparent
        Me.chkIOGbnO.ForeColor = System.Drawing.Color.Black
        Me.chkIOGbnO.Location = New System.Drawing.Point(645, 101)
        Me.chkIOGbnO.Name = "chkIOGbnO"
        Me.chkIOGbnO.Size = New System.Drawing.Size(54, 21)
        Me.chkIOGbnO.TabIndex = 203
        Me.chkIOGbnO.Tag = "IOGBN0"
        Me.chkIOGbnO.Text = "외래"
        Me.chkIOGbnO.UseVisualStyleBackColor = False
        '
        'cboTubeNmD
        '
        Me.cboTubeNmD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTubeNmD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTubeNmD.Location = New System.Drawing.Point(123, 173)
        Me.cboTubeNmD.MaxDropDownItems = 10
        Me.cboTubeNmD.Name = "cboTubeNmD"
        Me.cboTubeNmD.Size = New System.Drawing.Size(176, 20)
        Me.cboTubeNmD.TabIndex = 202
        Me.cboTubeNmD.TabStop = False
        Me.cboTubeNmD.Tag = "TUBENMD_01"
        '
        'txtTubeCd
        '
        Me.txtTubeCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTubeCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTubeCd.Location = New System.Drawing.Point(98, 173)
        Me.txtTubeCd.MaxLength = 2
        Me.txtTubeCd.Name = "txtTubeCd"
        Me.txtTubeCd.Size = New System.Drawing.Size(24, 21)
        Me.txtTubeCd.TabIndex = 201
        Me.txtTubeCd.Tag = "TUBECD"
        '
        'lblTubeCd
        '
        Me.lblTubeCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTubeCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTubeCd.ForeColor = System.Drawing.Color.White
        Me.lblTubeCd.Location = New System.Drawing.Point(7, 173)
        Me.lblTubeCd.Name = "lblTubeCd"
        Me.lblTubeCd.Size = New System.Drawing.Size(90, 21)
        Me.lblTubeCd.TabIndex = 200
        Me.lblTubeCd.Text = "용기코드"
        Me.lblTubeCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDSpcCd2
        '
        Me.txtDSpcCd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSpcCd2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDSpcCd2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDSpcCd2.Location = New System.Drawing.Point(299, 317)
        Me.txtDSpcCd2.MaxLength = 3
        Me.txtDSpcCd2.Name = "txtDSpcCd2"
        Me.txtDSpcCd2.Size = New System.Drawing.Size(28, 21)
        Me.txtDSpcCd2.TabIndex = 79
        Me.txtDSpcCd2.Tag = "DSPCCD2"
        Me.txtDSpcCd2.Visible = False
        '
        'cboDSpcNm2
        '
        Me.cboDSpcNm2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDSpcNm2.Location = New System.Drawing.Point(645, 394)
        Me.cboDSpcNm2.MaxDropDownItems = 10
        Me.cboDSpcNm2.Name = "cboDSpcNm2"
        Me.cboDSpcNm2.Size = New System.Drawing.Size(101, 20)
        Me.cboDSpcNm2.TabIndex = 80
        Me.cboDSpcNm2.TabStop = False
        Me.cboDSpcNm2.Tag = "DSPCNM2_01"
        Me.cboDSpcNm2.Visible = False
        '
        'lblDSpcNm2
        '
        Me.lblDSpcNm2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblDSpcNm2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDSpcNm2.ForeColor = System.Drawing.Color.Black
        Me.lblDSpcNm2.Location = New System.Drawing.Point(326, 509)
        Me.lblDSpcNm2.Name = "lblDSpcNm2"
        Me.lblDSpcNm2.Size = New System.Drawing.Size(84, 20)
        Me.lblDSpcNm2.TabIndex = 78
        Me.lblDSpcNm2.Text = "부가처방검체"
        Me.lblDSpcNm2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDSpcNm2.Visible = False
        '
        'txtOWarning
        '
        Me.txtOWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOWarning.Location = New System.Drawing.Point(97, 273)
        Me.txtOWarning.MaxLength = 100
        Me.txtOWarning.Multiline = True
        Me.txtOWarning.Name = "txtOWarning"
        Me.txtOWarning.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOWarning.Size = New System.Drawing.Size(375, 29)
        Me.txtOWarning.TabIndex = 192
        Me.txtOWarning.Tag = "OWARNING"
        '
        'lblOWarning
        '
        Me.lblOWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblOWarning.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOWarning.ForeColor = System.Drawing.Color.Black
        Me.lblOWarning.Location = New System.Drawing.Point(5, 273)
        Me.lblOWarning.Name = "lblOWarning"
        Me.lblOWarning.Size = New System.Drawing.Size(91, 29)
        Me.lblOWarning.TabIndex = 191
        Me.lblOWarning.Text = "처방주의사항"
        Me.lblOWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLine2
        '
        Me.lblLine2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine2.Location = New System.Drawing.Point(4, 245)
        Me.lblLine2.Name = "lblLine2"
        Me.lblLine2.Size = New System.Drawing.Size(756, 2)
        Me.lblLine2.TabIndex = 190
        '
        'lblBpGbn
        '
        Me.lblBpGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblBpGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBpGbn.ForeColor = System.Drawing.Color.Black
        Me.lblBpGbn.Location = New System.Drawing.Point(6, 314)
        Me.lblBpGbn.Name = "lblBpGbn"
        Me.lblBpGbn.Size = New System.Drawing.Size(90, 21)
        Me.lblBpGbn.TabIndex = 189
        Me.lblBpGbn.Text = "바코드 구분"
        Me.lblBpGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboBpGbn
        '
        Me.cboBpGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBpGbn.FormattingEnabled = True
        Me.cboBpGbn.Items.AddRange(New Object() {"[ ] ", "[A] 바코드 추가", "[B] CrossMatch 검체", "[J] 접수시 출력", "[J2] 검사부서접수시 출력", "[3] 3장", "[4] 4장", "[5] 5장", "[6] 6장", "[7] 7장", "[8] 8장", "[9] 9장", "[AJ] 바코드 추가, 접수시 출력", "[A2] 바코드 추가,  검사부서시 출력"})
        Me.cboBpGbn.Location = New System.Drawing.Point(97, 315)
        Me.cboBpGbn.Name = "cboBpGbn"
        Me.cboBpGbn.Size = New System.Drawing.Size(169, 20)
        Me.cboBpGbn.TabIndex = 188
        Me.cboBpGbn.Tag = "BCCNT_01"
        '
        'lblLine7
        '
        Me.lblLine7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine7.Location = New System.Drawing.Point(3, 585)
        Me.lblLine7.Name = "lblLine7"
        Me.lblLine7.Size = New System.Drawing.Size(756, 2)
        Me.lblLine7.TabIndex = 186
        '
        'txtTubeUnit
        '
        Me.txtTubeUnit.BackColor = System.Drawing.Color.LightGray
        Me.txtTubeUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeUnit.Location = New System.Drawing.Point(517, 173)
        Me.txtTubeUnit.Name = "txtTubeUnit"
        Me.txtTubeUnit.ReadOnly = True
        Me.txtTubeUnit.Size = New System.Drawing.Size(60, 21)
        Me.txtTubeUnit.TabIndex = 153
        Me.txtTubeUnit.TabStop = False
        Me.txtTubeUnit.Tag = "TUBEUNIT"
        '
        'txtTubeVol
        '
        Me.txtTubeVol.BackColor = System.Drawing.Color.LightGray
        Me.txtTubeVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeVol.Location = New System.Drawing.Point(386, 173)
        Me.txtTubeVol.Name = "txtTubeVol"
        Me.txtTubeVol.ReadOnly = True
        Me.txtTubeVol.Size = New System.Drawing.Size(57, 21)
        Me.txtTubeVol.TabIndex = 146
        Me.txtTubeVol.TabStop = False
        Me.txtTubeVol.Tag = "TUBEVOL"
        '
        'cboMGTType
        '
        Me.cboMGTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMGTType.Items.AddRange(New Object() {"[0] : 없음", "[1] : HLA 검사"})
        Me.cboMGTType.Location = New System.Drawing.Point(497, 359)
        Me.cboMGTType.MaxDropDownItems = 10
        Me.cboMGTType.Name = "cboMGTType"
        Me.cboMGTType.Size = New System.Drawing.Size(254, 20)
        Me.cboMGTType.TabIndex = 184
        Me.cboMGTType.Tag = "MGTTYPE_01"
        '
        'cboBBTType
        '
        Me.cboBBTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBBTType.Items.AddRange(New Object() {"[0] 없음", "[1] Cross 검사", "[2] Transfusion(수혈)", "[3] Preparation(수혈)", "[4] Emergency(수혈)", "[5] 지정헌혈 사전검사", "[6] 지정헌혈", "[7] 성분헌혈 사전검사", "[8] 성분헌혈", "[A] 자가헌혈 1차", "[B] 자가헌혈 2차", "[C] 자가헌혈 3차", "[D] 자가헌혈 4차", "[P] Plebotomy", "[T] : Therapeutic"})
        Me.cboBBTType.Location = New System.Drawing.Point(497, 337)
        Me.cboBBTType.MaxDropDownItems = 10
        Me.cboBBTType.Name = "cboBBTType"
        Me.cboBBTType.Size = New System.Drawing.Size(137, 20)
        Me.cboBBTType.TabIndex = 183
        Me.cboBBTType.Tag = "BBTTYPE_01"
        '
        'cboMBTType
        '
        Me.cboMBTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMBTType.Items.AddRange(New Object() {"[0] : 없음", "[1] : 현미경(도말염색)검사", "[2] : 배양(동정)검사", "[3] : 객담검사"})
        Me.cboMBTType.Location = New System.Drawing.Point(497, 315)
        Me.cboMBTType.MaxDropDownItems = 10
        Me.cboMBTType.Name = "cboMBTType"
        Me.cboMBTType.Size = New System.Drawing.Size(254, 20)
        Me.cboMBTType.TabIndex = 182
        Me.cboMBTType.Tag = "MBTTYPE_01"
        '
        'txtSameCd
        '
        Me.txtSameCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSameCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSameCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSameCd.Location = New System.Drawing.Point(97, 358)
        Me.txtSameCd.MaxLength = 6
        Me.txtSameCd.Name = "txtSameCd"
        Me.txtSameCd.Size = New System.Drawing.Size(77, 21)
        Me.txtSameCd.TabIndex = 181
        Me.txtSameCd.Tag = "SAMECD"
        '
        'lblSameCd
        '
        Me.lblSameCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSameCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSameCd.ForeColor = System.Drawing.Color.Black
        Me.lblSameCd.Location = New System.Drawing.Point(6, 358)
        Me.lblSameCd.Name = "lblSameCd"
        Me.lblSameCd.Size = New System.Drawing.Size(90, 21)
        Me.lblSameCd.TabIndex = 138
        Me.lblSameCd.Text = "대표코드"
        Me.lblSameCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMGTType
        '
        Me.lblMGTType.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblMGTType.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMGTType.ForeColor = System.Drawing.Color.Black
        Me.lblMGTType.Location = New System.Drawing.Point(383, 359)
        Me.lblMGTType.Name = "lblMGTType"
        Me.lblMGTType.Size = New System.Drawing.Size(109, 21)
        Me.lblMGTType.TabIndex = 141
        Me.lblMGTType.Text = "분자유전검사유형"
        Me.lblMGTType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBBTType
        '
        Me.lblBBTType.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblBBTType.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBBTType.ForeColor = System.Drawing.Color.Black
        Me.lblBBTType.Location = New System.Drawing.Point(383, 337)
        Me.lblBBTType.Name = "lblBBTType"
        Me.lblBBTType.Size = New System.Drawing.Size(109, 21)
        Me.lblBBTType.TabIndex = 144
        Me.lblBBTType.Text = "혈액은행검사유형"
        Me.lblBBTType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMBTType
        '
        Me.lblMBTType.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblMBTType.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMBTType.ForeColor = System.Drawing.Color.Black
        Me.lblMBTType.Location = New System.Drawing.Point(383, 315)
        Me.lblMBTType.Name = "lblMBTType"
        Me.lblMBTType.Size = New System.Drawing.Size(109, 21)
        Me.lblMBTType.TabIndex = 145
        Me.lblMBTType.Text = "미생물검사유형"
        Me.lblMBTType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSeqTMi
        '
        Me.txtSeqTMi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeqTMi.Location = New System.Drawing.Point(259, 553)
        Me.txtSeqTMi.MaxLength = 3
        Me.txtSeqTMi.Name = "txtSeqTMi"
        Me.txtSeqTMi.Size = New System.Drawing.Size(28, 21)
        Me.txtSeqTMi.TabIndex = 175
        Me.txtSeqTMi.Tag = "SEQTMI"
        '
        'lblSeqTMi
        '
        Me.lblSeqTMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSeqTMi.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSeqTMi.ForeColor = System.Drawing.Color.Black
        Me.lblSeqTMi.Location = New System.Drawing.Point(134, 553)
        Me.lblSeqTMi.Name = "lblSeqTMi"
        Me.lblSeqTMi.Size = New System.Drawing.Size(124, 21)
        Me.lblSeqTMi.TabIndex = 143
        Me.lblSeqTMi.Text = "연속검사시간(분:M)"
        Me.lblSeqTMi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkSeqTYN
        '
        Me.chkSeqTYN.Location = New System.Drawing.Point(7, 554)
        Me.chkSeqTYN.Name = "chkSeqTYN"
        Me.chkSeqTYN.Size = New System.Drawing.Size(123, 21)
        Me.chkSeqTYN.TabIndex = 174
        Me.chkSeqTYN.Tag = "SEQTYN"
        Me.chkSeqTYN.Text = "연속검사로 설정"
        '
        'cboExLabNmD
        '
        Me.cboExLabNmD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExLabNmD.Location = New System.Drawing.Point(288, 532)
        Me.cboExLabNmD.MaxDropDownItems = 10
        Me.cboExLabNmD.Name = "cboExLabNmD"
        Me.cboExLabNmD.Size = New System.Drawing.Size(159, 20)
        Me.cboExLabNmD.TabIndex = 172
        Me.cboExLabNmD.TabStop = False
        Me.cboExLabNmD.Tag = "EXLABNMD_01"
        '
        'txtExLabCd
        '
        Me.txtExLabCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExLabCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtExLabCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtExLabCd.Location = New System.Drawing.Point(259, 531)
        Me.txtExLabCd.MaxLength = 3
        Me.txtExLabCd.Name = "txtExLabCd"
        Me.txtExLabCd.Size = New System.Drawing.Size(28, 21)
        Me.txtExLabCd.TabIndex = 171
        Me.txtExLabCd.Tag = "EXLABCD"
        '
        'chkExLabYN
        '
        Me.chkExLabYN.Location = New System.Drawing.Point(7, 533)
        Me.chkExLabYN.Name = "chkExLabYN"
        Me.chkExLabYN.Size = New System.Drawing.Size(122, 21)
        Me.chkExLabYN.TabIndex = 170
        Me.chkExLabYN.Tag = "EXLABYN"
        Me.chkExLabYN.Text = "위탁검사로 설정"
        '
        'lblExLabCd
        '
        Me.lblExLabCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblExLabCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblExLabCd.ForeColor = System.Drawing.Color.Black
        Me.lblExLabCd.Location = New System.Drawing.Point(134, 531)
        Me.lblExLabCd.Name = "lblExLabCd"
        Me.lblExLabCd.Size = New System.Drawing.Size(124, 21)
        Me.lblExLabCd.TabIndex = 150
        Me.lblExLabCd.Text = "위탁기관코드"
        Me.lblExLabCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMinSpcVol
        '
        Me.txtMinSpcVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMinSpcVol.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtMinSpcVol.Location = New System.Drawing.Point(386, 217)
        Me.txtMinSpcVol.MaxLength = 10
        Me.txtMinSpcVol.Name = "txtMinSpcVol"
        Me.txtMinSpcVol.Size = New System.Drawing.Size(56, 21)
        Me.txtMinSpcVol.TabIndex = 169
        Me.txtMinSpcVol.Tag = "MINSPCVOL"
        '
        'lblMinSpcVol
        '
        Me.lblMinSpcVol.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblMinSpcVol.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMinSpcVol.ForeColor = System.Drawing.Color.Black
        Me.lblMinSpcVol.Location = New System.Drawing.Point(313, 217)
        Me.lblMinSpcVol.Name = "lblMinSpcVol"
        Me.lblMinSpcVol.Size = New System.Drawing.Size(72, 21)
        Me.lblMinSpcVol.TabIndex = 140
        Me.lblMinSpcVol.Text = "최소검체량"
        Me.lblMinSpcVol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTube
        '
        Me.lblTube.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTube.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTube.ForeColor = System.Drawing.Color.Black
        Me.lblTube.Location = New System.Drawing.Point(444, 173)
        Me.lblTube.Name = "lblTube"
        Me.lblTube.Size = New System.Drawing.Size(72, 21)
        Me.lblTube.TabIndex = 139
        Me.lblTube.Text = "용기단위"
        Me.lblTube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTubeVol
        '
        Me.lblTubeVol.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTubeVol.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTubeVol.ForeColor = System.Drawing.Color.Black
        Me.lblTubeVol.Location = New System.Drawing.Point(313, 173)
        Me.lblTubeVol.Name = "lblTubeVol"
        Me.lblTubeVol.Size = New System.Drawing.Size(72, 21)
        Me.lblTubeVol.TabIndex = 152
        Me.lblTubeVol.Text = "용기용량"
        Me.lblTubeVol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboBcclsNmd
        '
        Me.cboBcclsNmd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBcclsNmd.Location = New System.Drawing.Point(123, 217)
        Me.cboBcclsNmd.MaxDropDownItems = 10
        Me.cboBcclsNmd.Name = "cboBcclsNmd"
        Me.cboBcclsNmd.Size = New System.Drawing.Size(176, 20)
        Me.cboBcclsNmd.TabIndex = 178
        Me.cboBcclsNmd.TabStop = False
        Me.cboBcclsNmd.Tag = "BCCLSNMD_01"
        '
        'txtBcclsCd
        '
        Me.txtBcclsCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcclsCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBcclsCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtBcclsCd.Location = New System.Drawing.Point(98, 217)
        Me.txtBcclsCd.MaxLength = 2
        Me.txtBcclsCd.Name = "txtBcclsCd"
        Me.txtBcclsCd.Size = New System.Drawing.Size(24, 21)
        Me.txtBcclsCd.TabIndex = 177
        Me.txtBcclsCd.Tag = "BCCLSCD"
        '
        'lblBcclsCd
        '
        Me.lblBcclsCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblBcclsCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcclsCd.ForeColor = System.Drawing.Color.White
        Me.lblBcclsCd.Location = New System.Drawing.Point(7, 217)
        Me.lblBcclsCd.Name = "lblBcclsCd"
        Me.lblBcclsCd.Size = New System.Drawing.Size(90, 21)
        Me.lblBcclsCd.TabIndex = 148
        Me.lblBcclsCd.Text = "바코드분류"
        Me.lblBcclsCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDispSeqL
        '
        Me.txtDispSeqL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDispSeqL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispSeqL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDispSeqL.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDispSeqL.Location = New System.Drawing.Point(328, 710)
        Me.txtDispSeqL.MaxLength = 5
        Me.txtDispSeqL.Name = "txtDispSeqL"
        Me.txtDispSeqL.Size = New System.Drawing.Size(37, 21)
        Me.txtDispSeqL.TabIndex = 176
        Me.txtDispSeqL.Tag = "DISPSEQL"
        '
        'lblDispSeqL
        '
        Me.lblDispSeqL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDispSeqL.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDispSeqL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDispSeqL.ForeColor = System.Drawing.Color.Black
        Me.lblDispSeqL.Location = New System.Drawing.Point(234, 710)
        Me.lblDispSeqL.Name = "lblDispSeqL"
        Me.lblDispSeqL.Size = New System.Drawing.Size(93, 21)
        Me.lblDispSeqL.TabIndex = 151
        Me.lblDispSeqL.Text = "정렬순서 LIS"
        Me.lblDispSeqL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFRptMi
        '
        Me.lblFRptMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblFRptMi.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFRptMi.ForeColor = System.Drawing.Color.Black
        Me.lblFRptMi.Location = New System.Drawing.Point(348, 402)
        Me.lblFRptMi.Name = "lblFRptMi"
        Me.lblFRptMi.Size = New System.Drawing.Size(123, 21)
        Me.lblFRptMi.TabIndex = 96
        Me.lblFRptMi.Text = "최종보고기간"
        Me.lblFRptMi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPRptMi
        '
        Me.lblPRptMi.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblPRptMi.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPRptMi.ForeColor = System.Drawing.Color.Black
        Me.lblPRptMi.Location = New System.Drawing.Point(119, 402)
        Me.lblPRptMi.Name = "lblPRptMi"
        Me.lblPRptMi.Size = New System.Drawing.Size(114, 21)
        Me.lblPRptMi.TabIndex = 98
        Me.lblPRptMi.Text = "중간보고기간"
        Me.lblPRptMi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkViwSub
        '
        Me.chkViwSub.BackColor = System.Drawing.Color.Transparent
        Me.chkViwSub.Location = New System.Drawing.Point(551, 490)
        Me.chkViwSub.Name = "chkViwSub"
        Me.chkViwSub.Size = New System.Drawing.Size(170, 20)
        Me.chkViwSub.TabIndex = 136
        Me.chkViwSub.Tag = "VIWSUB"
        Me.chkViwSub.Text = "결과입력시 화면표시 여부"
        Me.chkViwSub.UseVisualStyleBackColor = False
        Me.chkViwSub.Visible = False
        '
        'lblLine3
        '
        Me.lblLine3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine3.Location = New System.Drawing.Point(3, 519)
        Me.lblLine3.Name = "lblLine3"
        Me.lblLine3.Size = New System.Drawing.Size(756, 2)
        Me.lblLine3.TabIndex = 134
        '
        'chkReqSub
        '
        Me.chkReqSub.BackColor = System.Drawing.Color.Transparent
        Me.chkReqSub.Location = New System.Drawing.Point(7, 490)
        Me.chkReqSub.Name = "chkReqSub"
        Me.chkReqSub.Size = New System.Drawing.Size(253, 20)
        Me.chkReqSub.TabIndex = 106
        Me.chkReqSub.Tag = "REQSUB"
        Me.chkReqSub.Text = "결과입력 필수 Child Of Sub.로 설정"
        Me.chkReqSub.UseVisualStyleBackColor = False
        '
        'chkFixRptYN
        '
        Me.chkFixRptYN.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkFixRptYN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkFixRptYN.ForeColor = System.Drawing.Color.Black
        Me.chkFixRptYN.Location = New System.Drawing.Point(313, 195)
        Me.chkFixRptYN.Margin = New System.Windows.Forms.Padding(0)
        Me.chkFixRptYN.Name = "chkFixRptYN"
        Me.chkFixRptYN.Size = New System.Drawing.Size(129, 21)
        Me.chkFixRptYN.TabIndex = 108
        Me.chkFixRptYN.Tag = "FIXRPTYN"
        Me.chkFixRptYN.Text = "고정 보고자 사용"
        Me.chkFixRptYN.UseVisualStyleBackColor = False
        '
        'cboFRptMi
        '
        Me.cboFRptMi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFRptMi.Items.AddRange(New Object() {"분:M", "시간:H", "일:D"})
        Me.cboFRptMi.Location = New System.Drawing.Point(513, 402)
        Me.cboFRptMi.Name = "cboFRptMi"
        Me.cboFRptMi.Size = New System.Drawing.Size(64, 20)
        Me.cboFRptMi.TabIndex = 114
        Me.cboFRptMi.Tag = "FRPTMI_01"
        '
        'txtFRptMI
        '
        Me.txtFRptMI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFRptMI.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFRptMI.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtFRptMI.Location = New System.Drawing.Point(472, 402)
        Me.txtFRptMI.MaxLength = 5
        Me.txtFRptMI.Name = "txtFRptMI"
        Me.txtFRptMI.Size = New System.Drawing.Size(40, 21)
        Me.txtFRptMI.TabIndex = 113
        Me.txtFRptMI.Tag = "FRPTMI"
        '
        'cboPRptMi
        '
        Me.cboPRptMi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPRptMi.Items.AddRange(New Object() {"분:M", "시간:H", "일:D"})
        Me.cboPRptMi.Location = New System.Drawing.Point(276, 403)
        Me.cboPRptMi.Name = "cboPRptMi"
        Me.cboPRptMi.Size = New System.Drawing.Size(64, 20)
        Me.cboPRptMi.TabIndex = 112
        Me.cboPRptMi.Tag = "PRPTMI_01"
        '
        'chkRptYN
        '
        Me.chkRptYN.BackColor = System.Drawing.Color.Transparent
        Me.chkRptYN.Location = New System.Drawing.Point(263, 490)
        Me.chkRptYN.Name = "chkRptYN"
        Me.chkRptYN.Size = New System.Drawing.Size(155, 20)
        Me.chkRptYN.TabIndex = 107
        Me.chkRptYN.Tag = "RPTYN"
        Me.chkRptYN.Text = "진료부에 결과 미보고"
        Me.chkRptYN.UseVisualStyleBackColor = False
        '
        'chkTatYN
        '
        Me.chkTatYN.BackColor = System.Drawing.Color.Transparent
        Me.chkTatYN.Location = New System.Drawing.Point(7, 402)
        Me.chkTatYN.Name = "chkTatYN"
        Me.chkTatYN.Size = New System.Drawing.Size(111, 20)
        Me.chkTatYN.TabIndex = 110
        Me.chkTatYN.Tag = "TATYN"
        Me.chkTatYN.Text = "TAT 체크 적용"
        Me.chkTatYN.UseVisualStyleBackColor = False
        '
        'txtPRptMi
        '
        Me.txtPRptMi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPRptMi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPRptMi.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPRptMi.Location = New System.Drawing.Point(235, 402)
        Me.txtPRptMi.MaxLength = 5
        Me.txtPRptMi.Name = "txtPRptMi"
        Me.txtPRptMi.Size = New System.Drawing.Size(40, 21)
        Me.txtPRptMi.TabIndex = 111
        Me.txtPRptMi.Tag = "PRPTMI"
        '
        'lblLine6
        '
        Me.lblLine6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine6.Location = New System.Drawing.Point(3, 307)
        Me.lblLine6.Name = "lblLine6"
        Me.lblLine6.Size = New System.Drawing.Size(756, 2)
        Me.lblLine6.TabIndex = 85
        '
        'lblLine4
        '
        Me.lblLine4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine4.Location = New System.Drawing.Point(4, 169)
        Me.lblLine4.Name = "lblLine4"
        Me.lblLine4.Size = New System.Drawing.Size(756, 2)
        Me.lblLine4.TabIndex = 84
        '
        'chkOrdHIde
        '
        Me.chkOrdHIde.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkOrdHIde.Location = New System.Drawing.Point(560, 124)
        Me.chkOrdHIde.Name = "chkOrdHIde"
        Me.chkOrdHIde.Size = New System.Drawing.Size(122, 20)
        Me.chkOrdHIde.TabIndex = 61
        Me.chkOrdHIde.Tag = "ORDHIDE"
        Me.chkOrdHIde.Text = "검사처방 미사용"
        Me.chkOrdHIde.UseVisualStyleBackColor = False
        '
        'cboDSpcNmO
        '
        Me.cboDSpcNmO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDSpcNmO.Location = New System.Drawing.Point(138, 123)
        Me.cboDSpcNmO.MaxDropDownItems = 10
        Me.cboDSpcNmO.Name = "cboDSpcNmO"
        Me.cboDSpcNmO.Size = New System.Drawing.Size(245, 20)
        Me.cboDSpcNmO.TabIndex = 21
        Me.cboDSpcNmO.TabStop = False
        Me.cboDSpcNmO.Tag = "DSPCNM1_01"
        '
        'lblSpccdO
        '
        Me.lblSpccdO.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpccdO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpccdO.ForeColor = System.Drawing.Color.White
        Me.lblSpccdO.Location = New System.Drawing.Point(7, 122)
        Me.lblSpccdO.Name = "lblSpccdO"
        Me.lblSpccdO.Size = New System.Drawing.Size(89, 21)
        Me.lblSpccdO.TabIndex = 0
        Me.lblSpccdO.Text = "기본처방검체"
        Me.lblSpccdO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDSpcCdO
        '
        Me.txtDSpcCdO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSpcCdO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDSpcCdO.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDSpcCdO.Location = New System.Drawing.Point(97, 122)
        Me.txtDSpcCdO.MaxLength = 4
        Me.txtDSpcCdO.Name = "txtDSpcCdO"
        Me.txtDSpcCdO.Size = New System.Drawing.Size(40, 21)
        Me.txtDSpcCdO.TabIndex = 20
        Me.txtDSpcCdO.Tag = "DSPCCD1"
        '
        'cboTOrdSlip
        '
        Me.cboTOrdSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTOrdSlip.Location = New System.Drawing.Point(98, 145)
        Me.cboTOrdSlip.MaxDropDownItems = 10
        Me.cboTOrdSlip.Name = "cboTOrdSlip"
        Me.cboTOrdSlip.Size = New System.Drawing.Size(285, 20)
        Me.cboTOrdSlip.TabIndex = 8
        Me.cboTOrdSlip.Tag = "TORDSLIP_01"
        '
        'lblOrdSlip
        '
        Me.lblOrdSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOrdSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdSlip.ForeColor = System.Drawing.Color.White
        Me.lblOrdSlip.Location = New System.Drawing.Point(7, 144)
        Me.lblOrdSlip.Name = "lblOrdSlip"
        Me.lblOrdSlip.Size = New System.Drawing.Size(89, 21)
        Me.lblOrdSlip.TabIndex = 55
        Me.lblOrdSlip.Text = "검사처방슬립"
        Me.lblOrdSlip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkErGbn1
        '
        Me.chkErGbn1.BackColor = System.Drawing.Color.Transparent
        Me.chkErGbn1.Location = New System.Drawing.Point(599, 600)
        Me.chkErGbn1.Name = "chkErGbn1"
        Me.chkErGbn1.Size = New System.Drawing.Size(72, 15)
        Me.chkErGbn1.TabIndex = 16
        Me.chkErGbn1.Tag = "ERGBN1"
        Me.chkErGbn1.Text = "응급검사"
        Me.chkErGbn1.UseVisualStyleBackColor = False
        '
        'txtSRecvLT
        '
        Me.txtSRecvLT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSRecvLT.Location = New System.Drawing.Point(268, 359)
        Me.txtSRecvLT.MaxLength = 100
        Me.txtSRecvLT.Multiline = True
        Me.txtSRecvLT.Name = "txtSRecvLT"
        Me.txtSRecvLT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSRecvLT.Size = New System.Drawing.Size(89, 21)
        Me.txtSRecvLT.TabIndex = 41
        Me.txtSRecvLT.Tag = "SRECVLT"
        Me.txtSRecvLT.Visible = False
        '
        'lblRRptST
        '
        Me.lblRRptST.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRRptST.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRRptST.ForeColor = System.Drawing.Color.Black
        Me.lblRRptST.Location = New System.Drawing.Point(441, 621)
        Me.lblRRptST.Name = "lblRRptST"
        Me.lblRRptST.Size = New System.Drawing.Size(90, 21)
        Me.lblRRptST.TabIndex = 0
        Me.lblRRptST.Text = "결과 소요일"
        Me.lblRRptST.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRRptST
        '
        Me.txtRRptST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRRptST.Location = New System.Drawing.Point(533, 621)
        Me.txtRRptST.MaxLength = 20
        Me.txtRRptST.Multiline = True
        Me.txtRRptST.Name = "txtRRptST"
        Me.txtRRptST.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRRptST.Size = New System.Drawing.Size(80, 21)
        Me.txtRRptST.TabIndex = 40
        Me.txtRRptST.Tag = "RRPTST"
        '
        'lblSRecvLT
        '
        Me.lblSRecvLT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSRecvLT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSRecvLT.ForeColor = System.Drawing.Color.Black
        Me.lblSRecvLT.Location = New System.Drawing.Point(180, 360)
        Me.lblSRecvLT.Name = "lblSRecvLT"
        Me.lblSRecvLT.Size = New System.Drawing.Size(90, 21)
        Me.lblSRecvLT.TabIndex = 0
        Me.lblSRecvLT.Text = "접수마감시간"
        Me.lblSRecvLT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSRecvLT.Visible = False
        '
        'txtDispSeqO
        '
        Me.txtDispSeqO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDispSeqO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispSeqO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDispSeqO.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDispSeqO.Location = New System.Drawing.Point(597, 710)
        Me.txtDispSeqO.MaxLength = 5
        Me.txtDispSeqO.Name = "txtDispSeqO"
        Me.txtDispSeqO.Size = New System.Drawing.Size(37, 21)
        Me.txtDispSeqO.TabIndex = 13
        Me.txtDispSeqO.Tag = "DISPSEQO"
        '
        'lblDispSeqO
        '
        Me.lblDispSeqO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDispSeqO.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDispSeqO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDispSeqO.ForeColor = System.Drawing.Color.Black
        Me.lblDispSeqO.Location = New System.Drawing.Point(513, 710)
        Me.lblDispSeqO.Name = "lblDispSeqO"
        Me.lblDispSeqO.Size = New System.Drawing.Size(83, 21)
        Me.lblDispSeqO.TabIndex = 0
        Me.lblDispSeqO.Text = "검사처방순번"
        Me.lblDispSeqO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLine5
        '
        Me.lblLine5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine5.Location = New System.Drawing.Point(3, 389)
        Me.lblLine5.Name = "lblLine5"
        Me.lblLine5.Size = New System.Drawing.Size(756, 2)
        Me.lblLine5.TabIndex = 0
        '
        'lblLine1
        '
        Me.lblLine1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine1.Location = New System.Drawing.Point(4, 91)
        Me.lblLine1.Name = "lblLine1"
        Me.lblLine1.Size = New System.Drawing.Size(756, 2)
        Me.lblLine1.TabIndex = 0
        '
        'cboTCdGbn
        '
        Me.cboTCdGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTCdGbn.Items.AddRange(New Object() {"[S] Single", "[P] Parent Of Sub.", "[C] Child Of Sub.", "[B] Battery", "[G] Group"})
        Me.cboTCdGbn.Location = New System.Drawing.Point(97, 100)
        Me.cboTCdGbn.Name = "cboTCdGbn"
        Me.cboTCdGbn.Size = New System.Drawing.Size(164, 20)
        Me.cboTCdGbn.TabIndex = 5
        Me.cboTCdGbn.Tag = "TCDGBN_01"
        '
        'lblTCdGbn
        '
        Me.lblTCdGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTCdGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTCdGbn.ForeColor = System.Drawing.Color.White
        Me.lblTCdGbn.Location = New System.Drawing.Point(7, 100)
        Me.lblTCdGbn.Name = "lblTCdGbn"
        Me.lblTCdGbn.Size = New System.Drawing.Size(89, 21)
        Me.lblTCdGbn.TabIndex = 0
        Me.lblTCdGbn.Text = "검사코드구분"
        Me.lblTCdGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTNmBP
        '
        Me.lblTNmBP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTNmBP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTNmBP.ForeColor = System.Drawing.Color.White
        Me.lblTNmBP.Location = New System.Drawing.Point(8, 60)
        Me.lblTNmBP.Name = "lblTNmBP"
        Me.lblTNmBP.Size = New System.Drawing.Size(97, 21)
        Me.lblTNmBP.TabIndex = 0
        Me.lblTNmBP.Text = "검사명(바코드)"
        Me.lblTNmBP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTNmBP
        '
        Me.txtTNmBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTNmBP.Location = New System.Drawing.Point(106, 60)
        Me.txtTNmBP.MaxLength = 12
        Me.txtTNmBP.Name = "txtTNmBP"
        Me.txtTNmBP.Size = New System.Drawing.Size(257, 21)
        Me.txtTNmBP.TabIndex = 4
        Me.txtTNmBP.Tag = "TNMBP"
        '
        'lblTNmP
        '
        Me.lblTNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTNmP.ForeColor = System.Drawing.Color.White
        Me.lblTNmP.Location = New System.Drawing.Point(8, 38)
        Me.lblTNmP.Name = "lblTNmP"
        Me.lblTNmP.Size = New System.Drawing.Size(97, 21)
        Me.lblTNmP.TabIndex = 0
        Me.lblTNmP.Text = "검사명(출력)"
        Me.lblTNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTNmP
        '
        Me.txtTNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTNmP.Location = New System.Drawing.Point(106, 38)
        Me.txtTNmP.MaxLength = 80
        Me.txtTNmP.Multiline = True
        Me.txtTNmP.Name = "txtTNmP"
        Me.txtTNmP.Size = New System.Drawing.Size(257, 21)
        Me.txtTNmP.TabIndex = 3
        Me.txtTNmP.Tag = "TNMP"
        '
        'lblTNmD
        '
        Me.lblTNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTNmD.ForeColor = System.Drawing.Color.White
        Me.lblTNmD.Location = New System.Drawing.Point(387, 16)
        Me.lblTNmD.Name = "lblTNmD"
        Me.lblTNmD.Size = New System.Drawing.Size(84, 21)
        Me.lblTNmD.TabIndex = 0
        Me.lblTNmD.Text = "검사명(화면)"
        Me.lblTNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTNmD
        '
        Me.txtTNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTNmD.Location = New System.Drawing.Point(472, 16)
        Me.txtTNmD.MaxLength = 80
        Me.txtTNmD.Multiline = True
        Me.txtTNmD.Name = "txtTNmD"
        Me.txtTNmD.Size = New System.Drawing.Size(280, 21)
        Me.txtTNmD.TabIndex = 2
        Me.txtTNmD.Tag = "TNMD"
        '
        'lblTNmS
        '
        Me.lblTNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTNmS.ForeColor = System.Drawing.Color.White
        Me.lblTNmS.Location = New System.Drawing.Point(387, 38)
        Me.lblTNmS.Name = "lblTNmS"
        Me.lblTNmS.Size = New System.Drawing.Size(84, 21)
        Me.lblTNmS.TabIndex = 0
        Me.lblTNmS.Text = "검사명(처방)"
        Me.lblTNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTNmS
        '
        Me.txtTNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTNmS.Location = New System.Drawing.Point(472, 38)
        Me.txtTNmS.MaxLength = 80
        Me.txtTNmS.Multiline = True
        Me.txtTNmS.Name = "txtTNmS"
        Me.txtTNmS.Size = New System.Drawing.Size(280, 21)
        Me.txtTNmS.TabIndex = 7
        Me.txtTNmS.Tag = "TNMS"
        '
        'lblTNm
        '
        Me.lblTNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTNm.ForeColor = System.Drawing.Color.White
        Me.lblTNm.Location = New System.Drawing.Point(8, 16)
        Me.lblTNm.Name = "lblTNm"
        Me.lblTNm.Size = New System.Drawing.Size(97, 21)
        Me.lblTNm.TabIndex = 0
        Me.lblTNm.Text = "검사명"
        Me.lblTNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTNm
        '
        Me.txtTNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTNm.Location = New System.Drawing.Point(106, 16)
        Me.txtTNm.MaxLength = 80
        Me.txtTNm.Multiline = True
        Me.txtTNm.Name = "txtTNm"
        Me.txtTNm.Size = New System.Drawing.Size(257, 21)
        Me.txtTNm.TabIndex = 1
        Me.txtTNm.Tag = "TNM"
        '
        'lblTOrdSlip
        '
        Me.lblTOrdSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblTOrdSlip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTOrdSlip.Location = New System.Drawing.Point(97, 145)
        Me.lblTOrdSlip.Name = "lblTOrdSlip"
        Me.lblTOrdSlip.Size = New System.Drawing.Size(40, 20)
        Me.lblTOrdSlip.TabIndex = 0
        Me.lblTOrdSlip.Tag = "TORDSLIP"
        Me.lblTOrdSlip.Visible = False
        '
        'chkTitleYN
        '
        Me.chkTitleYN.BackColor = System.Drawing.Color.Transparent
        Me.chkTitleYN.Location = New System.Drawing.Point(7, 490)
        Me.chkTitleYN.Name = "chkTitleYN"
        Me.chkTitleYN.Size = New System.Drawing.Size(236, 20)
        Me.chkTitleYN.TabIndex = 105
        Me.chkTitleYN.Tag = "TITLEYN"
        Me.chkTitleYN.Text = "결과입력 불가능한 TITLE로 설정"
        Me.chkTitleYN.UseVisualStyleBackColor = False
        '
        'spdRef
        '
        Me.spdRef.DataSource = Nothing
        Me.spdRef.Location = New System.Drawing.Point(525, 645)
        Me.spdRef.Name = "spdRef"
        Me.spdRef.OcxState = CType(resources.GetObject("spdRef.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRef.Size = New System.Drawing.Size(227, 75)
        Me.spdRef.TabIndex = 260
        Me.spdRef.Visible = False
        '
        'grpTestCd
        '
        Me.grpTestCd.Controls.Add(Me.btnClear_spc)
        Me.grpTestCd.Controls.Add(Me.txtSelSpc)
        Me.grpTestCd.Controls.Add(Me.btnCdHelp_spc)
        Me.grpTestCd.Controls.Add(Me.chkSpcGbn)
        Me.grpTestCd.Controls.Add(Me.txtTestCd)
        Me.grpTestCd.Controls.Add(Me.btnGetExcel)
        Me.grpTestCd.Controls.Add(Me.btnUE)
        Me.grpTestCd.Controls.Add(Me.dtpUSTime)
        Me.grpTestCd.Controls.Add(Me.txtUSDay)
        Me.grpTestCd.Controls.Add(Me.dtpUSDay)
        Me.grpTestCd.Controls.Add(Me.lblUSDayTime)
        Me.grpTestCd.Controls.Add(Me.cboSpcNmD)
        Me.grpTestCd.Controls.Add(Me.lblSpcCd)
        Me.grpTestCd.Controls.Add(Me.txtSpcCd)
        Me.grpTestCd.Controls.Add(Me.txtTClsCd0)
        Me.grpTestCd.Controls.Add(Me.lblTestCd)
        Me.grpTestCd.Controls.Add(Me.txtSpcCd0)
        Me.grpTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpTestCd.Location = New System.Drawing.Point(6, 7)
        Me.grpTestCd.Name = "grpTestCd"
        Me.grpTestCd.Size = New System.Drawing.Size(762, 67)
        Me.grpTestCd.TabIndex = 1
        Me.grpTestCd.TabStop = False
        Me.grpTestCd.Text = "검사코드"
        '
        'btnClear_spc
        '
        Me.btnClear_spc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_spc.Location = New System.Drawing.Point(700, 39)
        Me.btnClear_spc.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_spc.Name = "btnClear_spc"
        Me.btnClear_spc.Size = New System.Drawing.Size(49, 21)
        Me.btnClear_spc.TabIndex = 200
        Me.btnClear_spc.Text = "Clear"
        Me.btnClear_spc.UseVisualStyleBackColor = True
        Me.btnClear_spc.Visible = False
        '
        'txtSelSpc
        '
        Me.txtSelSpc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelSpc.BackColor = System.Drawing.Color.Thistle
        Me.txtSelSpc.ForeColor = System.Drawing.Color.Brown
        Me.txtSelSpc.Location = New System.Drawing.Point(114, 39)
        Me.txtSelSpc.Multiline = True
        Me.txtSelSpc.Name = "txtSelSpc"
        Me.txtSelSpc.ReadOnly = True
        Me.txtSelSpc.Size = New System.Drawing.Size(583, 21)
        Me.txtSelSpc.TabIndex = 199
        Me.txtSelSpc.Visible = False
        '
        'btnCdHelp_spc
        '
        Me.btnCdHelp_spc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_spc.Image = CType(resources.GetObject("btnCdHelp_spc.Image"), System.Drawing.Image)
        Me.btnCdHelp_spc.Location = New System.Drawing.Point(93, 39)
        Me.btnCdHelp_spc.Name = "btnCdHelp_spc"
        Me.btnCdHelp_spc.Size = New System.Drawing.Size(21, 21)
        Me.btnCdHelp_spc.TabIndex = 198
        Me.btnCdHelp_spc.UseVisualStyleBackColor = True
        Me.btnCdHelp_spc.Visible = False
        '
        'chkSpcGbn
        '
        Me.chkSpcGbn.AutoSize = True
        Me.chkSpcGbn.Location = New System.Drawing.Point(401, 19)
        Me.chkSpcGbn.Name = "chkSpcGbn"
        Me.chkSpcGbn.Size = New System.Drawing.Size(108, 16)
        Me.chkSpcGbn.TabIndex = 76
        Me.chkSpcGbn.Text = "다중 검체 작업"
        Me.chkSpcGbn.UseVisualStyleBackColor = True
        Me.chkSpcGbn.Visible = False
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestCd.Location = New System.Drawing.Point(333, 17)
        Me.txtTestCd.MaxLength = 7
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(61, 21)
        Me.txtTestCd.TabIndex = 4
        Me.txtTestCd.Tag = "TESTCD"
        Me.txtTestCd.Text = "88888888"
        '
        'btnGetExcel
        '
        Me.btnGetExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGetExcel.Location = New System.Drawing.Point(614, 11)
        Me.btnGetExcel.Margin = New System.Windows.Forms.Padding(1)
        Me.btnGetExcel.Name = "btnGetExcel"
        Me.btnGetExcel.Size = New System.Drawing.Size(62, 25)
        Me.btnGetExcel.TabIndex = 75
        Me.btnGetExcel.TabStop = False
        Me.btnGetExcel.Text = "Excel"
        Me.btnGetExcel.UseVisualStyleBackColor = True
        Me.btnGetExcel.Visible = False
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(677, 11)
        Me.btnUE.Margin = New System.Windows.Forms.Padding(1)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 7
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(191, 17)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(80, 21)
        Me.dtpUSTime.TabIndex = 3
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(93, 17)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(76, 21)
        Me.txtUSDay.TabIndex = 1
        Me.txtUSDay.Text = "2010-09-17"
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(170, 17)
        Me.dtpUSDay.Name = "dtpUSDay"
        Me.dtpUSDay.Size = New System.Drawing.Size(20, 21)
        Me.dtpUSDay.TabIndex = 2
        Me.dtpUSDay.TabStop = False
        Me.dtpUSDay.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'lblUSDayTime
        '
        Me.lblUSDayTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Location = New System.Drawing.Point(5, 17)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(90, 21)
        Me.lblUSDayTime.TabIndex = 0
        Me.lblUSDayTime.Text = "시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboSpcNmD
        '
        Me.cboSpcNmD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpcNmD.Location = New System.Drawing.Point(131, 39)
        Me.cboSpcNmD.MaxDropDownItems = 10
        Me.cboSpcNmD.Name = "cboSpcNmD"
        Me.cboSpcNmD.Size = New System.Drawing.Size(263, 20)
        Me.cboSpcNmD.TabIndex = 6
        Me.cboSpcNmD.TabStop = False
        Me.cboSpcNmD.Tag = "SPCNMD_01"
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(5, 39)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(87, 21)
        Me.lblSpcCd.TabIndex = 0
        Me.lblSpcCd.Text = "검체코드"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcCd
        '
        Me.txtSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSpcCd.Location = New System.Drawing.Point(93, 39)
        Me.txtSpcCd.MaxLength = 4
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.Size = New System.Drawing.Size(37, 21)
        Me.txtSpcCd.TabIndex = 5
        Me.txtSpcCd.Tag = "SPCCD"
        '
        'txtTClsCd0
        '
        Me.txtTClsCd0.BackColor = System.Drawing.Color.LightGray
        Me.txtTClsCd0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTClsCd0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTClsCd0.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTClsCd0.Location = New System.Drawing.Point(342, 17)
        Me.txtTClsCd0.Name = "txtTClsCd0"
        Me.txtTClsCd0.ReadOnly = True
        Me.txtTClsCd0.Size = New System.Drawing.Size(52, 21)
        Me.txtTClsCd0.TabIndex = 0
        Me.txtTClsCd0.TabStop = False
        Me.txtTClsCd0.Tag = "TESTCD"
        Me.txtTClsCd0.Visible = False
        '
        'lblTestCd
        '
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(273, 17)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(59, 21)
        Me.lblTestCd.TabIndex = 0
        Me.lblTestCd.Text = "검사코드"
        Me.lblTestCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcCd0
        '
        Me.txtSpcCd0.BackColor = System.Drawing.Color.LightGray
        Me.txtSpcCd0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd0.Location = New System.Drawing.Point(95, 39)
        Me.txtSpcCd0.Name = "txtSpcCd0"
        Me.txtSpcCd0.ReadOnly = True
        Me.txtSpcCd0.Size = New System.Drawing.Size(28, 21)
        Me.txtSpcCd0.TabIndex = 0
        Me.txtSpcCd0.TabStop = False
        Me.txtSpcCd0.Tag = "SPCCD"
        Me.txtSpcCd0.Visible = False
        '
        'tpgTest2
        '
        Me.tpgTest2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgTest2.Controls.Add(Me.grpTInfo2)
        Me.tpgTest2.Location = New System.Drawing.Point(4, 22)
        Me.tpgTest2.Name = "tpgTest2"
        Me.tpgTest2.Size = New System.Drawing.Size(773, 863)
        Me.tpgTest2.TabIndex = 1
        Me.tpgTest2.Text = "결과관련정보"
        Me.tpgTest2.UseVisualStyleBackColor = True
        Me.tpgTest2.Visible = False
        '
        'grpTInfo2
        '
        Me.grpTInfo2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTInfo2.Controls.Add(Me.btnDescRefExit)
        Me.grpTInfo2.Controls.Add(Me.txtDescRef)
        Me.grpTInfo2.Controls.Add(Me.lblDescRef)
        Me.grpTInfo2.Controls.Add(Me.spdAgeRef)
        Me.grpTInfo2.Controls.Add(Me.Panel7)
        Me.grpTInfo2.Controls.Add(Me.Panel6)
        Me.grpTInfo2.Controls.Add(Me.Panel5)
        Me.grpTInfo2.Controls.Add(Me.pnlRstGbn)
        Me.grpTInfo2.Controls.Add(Me.cboALimitHS)
        Me.grpTInfo2.Controls.Add(Me.cboALimitLS)
        Me.grpTInfo2.Controls.Add(Me.lblALimitHS)
        Me.grpTInfo2.Controls.Add(Me.lblALimitLS)
        Me.grpTInfo2.Controls.Add(Me.lblJudgType3)
        Me.grpTInfo2.Controls.Add(Me.lblJudgType2)
        Me.grpTInfo2.Controls.Add(Me.lblJudgType1)
        Me.grpTInfo2.Controls.Add(Me.lblALimitH)
        Me.grpTInfo2.Controls.Add(Me.lblALimitL)
        Me.grpTInfo2.Controls.Add(Me.txtALimitH)
        Me.grpTInfo2.Controls.Add(Me.txtALimitL)
        Me.grpTInfo2.Controls.Add(Me.cboALimitGbn)
        Me.grpTInfo2.Controls.Add(Me.lblALimitGbn)
        Me.grpTInfo2.Controls.Add(Me.lblDeltaDay)
        Me.grpTInfo2.Controls.Add(Me.txtDeltaDay)
        Me.grpTInfo2.Controls.Add(Me.lblLine10)
        Me.grpTInfo2.Controls.Add(Me.lblDeltaH)
        Me.grpTInfo2.Controls.Add(Me.lblDeltaL)
        Me.grpTInfo2.Controls.Add(Me.txtDeltaH)
        Me.grpTInfo2.Controls.Add(Me.txtDeltaL)
        Me.grpTInfo2.Controls.Add(Me.cboDeltaGbn)
        Me.grpTInfo2.Controls.Add(Me.lblDeltaGbn)
        Me.grpTInfo2.Controls.Add(Me.lblAlertH)
        Me.grpTInfo2.Controls.Add(Me.lblAlertL)
        Me.grpTInfo2.Controls.Add(Me.txtAlertH)
        Me.grpTInfo2.Controls.Add(Me.txtAlertL)
        Me.grpTInfo2.Controls.Add(Me.cboAlertGbn)
        Me.grpTInfo2.Controls.Add(Me.lblAlertGbn)
        Me.grpTInfo2.Controls.Add(Me.lblCriticalH)
        Me.grpTInfo2.Controls.Add(Me.lblCriticalL)
        Me.grpTInfo2.Controls.Add(Me.txtCriticalH)
        Me.grpTInfo2.Controls.Add(Me.txtCriticalL)
        Me.grpTInfo2.Controls.Add(Me.cboCriticalGbn)
        Me.grpTInfo2.Controls.Add(Me.lblCriticalGbn)
        Me.grpTInfo2.Controls.Add(Me.lblPanicH)
        Me.grpTInfo2.Controls.Add(Me.lblPanicL)
        Me.grpTInfo2.Controls.Add(Me.txtPanicH)
        Me.grpTInfo2.Controls.Add(Me.txtPanicL)
        Me.grpTInfo2.Controls.Add(Me.cboPanicGbn)
        Me.grpTInfo2.Controls.Add(Me.lblPanicGbn)
        Me.grpTInfo2.Controls.Add(Me.cboJudgType3)
        Me.grpTInfo2.Controls.Add(Me.cboJudgType2)
        Me.grpTInfo2.Controls.Add(Me.cboJudgType1)
        Me.grpTInfo2.Controls.Add(Me.txtUJudgLT3)
        Me.grpTInfo2.Controls.Add(Me.lblUJudgLT3)
        Me.grpTInfo2.Controls.Add(Me.txtUJudgLT2)
        Me.grpTInfo2.Controls.Add(Me.lblUJudgLT2)
        Me.grpTInfo2.Controls.Add(Me.txtUJudgLT1)
        Me.grpTInfo2.Controls.Add(Me.lblUJudgLT1)
        Me.grpTInfo2.Controls.Add(Me.lblJudgType)
        Me.grpTInfo2.Controls.Add(Me.btnDescRef)
        Me.grpTInfo2.Controls.Add(Me.txtRstUnit)
        Me.grpTInfo2.Controls.Add(Me.lblRstUnit)
        Me.grpTInfo2.Controls.Add(Me.lblCutOpt)
        Me.grpTInfo2.Controls.Add(Me.chkRstLen)
        Me.grpTInfo2.Controls.Add(Me.cboRstLLen)
        Me.grpTInfo2.Controls.Add(Me.lblRstLLen)
        Me.grpTInfo2.Controls.Add(Me.cboRstULen)
        Me.grpTInfo2.Controls.Add(Me.lblRstType)
        Me.grpTInfo2.Controls.Add(Me.lblLine8)
        Me.grpTInfo2.Controls.Add(Me.lblLine9)
        Me.grpTInfo2.Controls.Add(Me.lblLine11)
        Me.grpTInfo2.Controls.Add(Me.lblRefGbn)
        Me.grpTInfo2.Controls.Add(Me.lblRstULen)
        Me.grpTInfo2.Location = New System.Drawing.Point(8, 4)
        Me.grpTInfo2.Name = "grpTInfo2"
        Me.grpTInfo2.Size = New System.Drawing.Size(764, 536)
        Me.grpTInfo2.TabIndex = 0
        Me.grpTInfo2.TabStop = False
        Me.grpTInfo2.Text = "검사정보"
        '
        'btnDescRefExit
        '
        Me.btnDescRefExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDescRefExit.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDescRefExit.Location = New System.Drawing.Point(546, 97)
        Me.btnDescRefExit.Name = "btnDescRefExit"
        Me.btnDescRefExit.Size = New System.Drawing.Size(18, 20)
        Me.btnDescRefExit.TabIndex = 0
        Me.btnDescRefExit.Text = "×"
        '
        'txtDescRef
        '
        Me.txtDescRef.Location = New System.Drawing.Point(272, 96)
        Me.txtDescRef.MaxLength = 1000
        Me.txtDescRef.Multiline = True
        Me.txtDescRef.Name = "txtDescRef"
        Me.txtDescRef.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescRef.Size = New System.Drawing.Size(292, 108)
        Me.txtDescRef.TabIndex = 0
        Me.txtDescRef.Tag = "DESCREF"
        '
        'lblDescRef
        '
        Me.lblDescRef.BackColor = System.Drawing.Color.Black
        Me.lblDescRef.ForeColor = System.Drawing.Color.White
        Me.lblDescRef.Location = New System.Drawing.Point(272, 96)
        Me.lblDescRef.Name = "lblDescRef"
        Me.lblDescRef.Size = New System.Drawing.Size(192, 16)
        Me.lblDescRef.TabIndex = 177
        Me.lblDescRef.Tag = "DESCREF"
        Me.lblDescRef.Visible = False
        '
        'spdAgeRef
        '
        Me.spdAgeRef.DataSource = Nothing
        Me.spdAgeRef.Location = New System.Drawing.Point(8, 102)
        Me.spdAgeRef.Name = "spdAgeRef"
        Me.spdAgeRef.OcxState = CType(resources.GetObject("spdAgeRef.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdAgeRef.Size = New System.Drawing.Size(747, 102)
        Me.spdAgeRef.TabIndex = 9
        Me.spdAgeRef.TabStop = False
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.rdoJudgType3)
        Me.Panel7.Controls.Add(Me.rdoJudgType2)
        Me.Panel7.Controls.Add(Me.rdoJudgType1)
        Me.Panel7.Controls.Add(Me.rdoJudgType0)
        Me.Panel7.Location = New System.Drawing.Point(96, 220)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(396, 20)
        Me.Panel7.TabIndex = 10
        '
        'rdoJudgType3
        '
        Me.rdoJudgType3.BackColor = System.Drawing.Color.Transparent
        Me.rdoJudgType3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoJudgType3.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoJudgType3.Location = New System.Drawing.Point(268, 0)
        Me.rdoJudgType3.Name = "rdoJudgType3"
        Me.rdoJudgType3.Size = New System.Drawing.Size(124, 20)
        Me.rdoJudgType3.TabIndex = 4
        Me.rdoJudgType3.TabStop = True
        Me.rdoJudgType3.Tag = "JUDGTYPE3"
        Me.rdoJudgType3.Text = "사용자정의 3단계"
        Me.rdoJudgType3.UseVisualStyleBackColor = False
        '
        'rdoJudgType2
        '
        Me.rdoJudgType2.BackColor = System.Drawing.Color.Transparent
        Me.rdoJudgType2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoJudgType2.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoJudgType2.Location = New System.Drawing.Point(140, 0)
        Me.rdoJudgType2.Name = "rdoJudgType2"
        Me.rdoJudgType2.Size = New System.Drawing.Size(124, 20)
        Me.rdoJudgType2.TabIndex = 3
        Me.rdoJudgType2.TabStop = True
        Me.rdoJudgType2.Tag = "JUDGTYPE2"
        Me.rdoJudgType2.Text = "사용자정의 2단계"
        Me.rdoJudgType2.UseVisualStyleBackColor = False
        '
        'rdoJudgType1
        '
        Me.rdoJudgType1.BackColor = System.Drawing.Color.Transparent
        Me.rdoJudgType1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoJudgType1.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoJudgType1.Location = New System.Drawing.Point(72, 0)
        Me.rdoJudgType1.Name = "rdoJudgType1"
        Me.rdoJudgType1.Size = New System.Drawing.Size(64, 20)
        Me.rdoJudgType1.TabIndex = 2
        Me.rdoJudgType1.TabStop = True
        Me.rdoJudgType1.Tag = "JUDGTYPE1"
        Me.rdoJudgType1.Text = "L/H"
        Me.rdoJudgType1.UseVisualStyleBackColor = False
        '
        'rdoJudgType0
        '
        Me.rdoJudgType0.BackColor = System.Drawing.Color.Transparent
        Me.rdoJudgType0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoJudgType0.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoJudgType0.Location = New System.Drawing.Point(4, 0)
        Me.rdoJudgType0.Name = "rdoJudgType0"
        Me.rdoJudgType0.Size = New System.Drawing.Size(64, 20)
        Me.rdoJudgType0.TabIndex = 1
        Me.rdoJudgType0.TabStop = True
        Me.rdoJudgType0.Tag = "JUDGTYPE0"
        Me.rdoJudgType0.Text = "미사용"
        Me.rdoJudgType0.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.rdoRefGbn1)
        Me.Panel6.Controls.Add(Me.rdoRefGbn2)
        Me.Panel6.Controls.Add(Me.rdoRefGbn0)
        Me.Panel6.Location = New System.Drawing.Point(96, 76)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(172, 20)
        Me.Panel6.TabIndex = 6
        '
        'rdoRefGbn1
        '
        Me.rdoRefGbn1.BackColor = System.Drawing.Color.Transparent
        Me.rdoRefGbn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoRefGbn1.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoRefGbn1.Location = New System.Drawing.Point(60, 0)
        Me.rdoRefGbn1.Name = "rdoRefGbn1"
        Me.rdoRefGbn1.Size = New System.Drawing.Size(52, 20)
        Me.rdoRefGbn1.TabIndex = 2
        Me.rdoRefGbn1.TabStop = True
        Me.rdoRefGbn1.Tag = "REFGBN1"
        Me.rdoRefGbn1.Text = "문자"
        Me.rdoRefGbn1.UseVisualStyleBackColor = False
        '
        'rdoRefGbn2
        '
        Me.rdoRefGbn2.BackColor = System.Drawing.Color.Transparent
        Me.rdoRefGbn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoRefGbn2.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoRefGbn2.Location = New System.Drawing.Point(4, 0)
        Me.rdoRefGbn2.Name = "rdoRefGbn2"
        Me.rdoRefGbn2.Size = New System.Drawing.Size(52, 20)
        Me.rdoRefGbn2.TabIndex = 1
        Me.rdoRefGbn2.TabStop = True
        Me.rdoRefGbn2.Tag = "REFGBN2"
        Me.rdoRefGbn2.Text = "숫자"
        Me.rdoRefGbn2.UseVisualStyleBackColor = False
        '
        'rdoRefGbn0
        '
        Me.rdoRefGbn0.BackColor = System.Drawing.Color.Transparent
        Me.rdoRefGbn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoRefGbn0.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoRefGbn0.Location = New System.Drawing.Point(116, 0)
        Me.rdoRefGbn0.Name = "rdoRefGbn0"
        Me.rdoRefGbn0.Size = New System.Drawing.Size(52, 20)
        Me.rdoRefGbn0.TabIndex = 3
        Me.rdoRefGbn0.TabStop = True
        Me.rdoRefGbn0.Tag = "REFGBN0"
        Me.rdoRefGbn0.Text = "없음"
        Me.rdoRefGbn0.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.rdoCutOpt3)
        Me.Panel5.Controls.Add(Me.rdoCutOpt2)
        Me.Panel5.Controls.Add(Me.rdoCutOpt1)
        Me.Panel5.Location = New System.Drawing.Point(556, 40)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(196, 20)
        Me.Panel5.TabIndex = 5
        '
        'rdoCutOpt3
        '
        Me.rdoCutOpt3.BackColor = System.Drawing.Color.Transparent
        Me.rdoCutOpt3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoCutOpt3.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.rdoCutOpt3.Location = New System.Drawing.Point(132, 0)
        Me.rdoCutOpt3.Name = "rdoCutOpt3"
        Me.rdoCutOpt3.Size = New System.Drawing.Size(60, 20)
        Me.rdoCutOpt3.TabIndex = 8
        Me.rdoCutOpt3.TabStop = True
        Me.rdoCutOpt3.Tag = "CUTOPT3"
        Me.rdoCutOpt3.Text = "내림"
        Me.rdoCutOpt3.UseVisualStyleBackColor = False
        '
        'rdoCutOpt2
        '
        Me.rdoCutOpt2.BackColor = System.Drawing.Color.Transparent
        Me.rdoCutOpt2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoCutOpt2.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.rdoCutOpt2.Location = New System.Drawing.Point(68, 0)
        Me.rdoCutOpt2.Name = "rdoCutOpt2"
        Me.rdoCutOpt2.Size = New System.Drawing.Size(60, 20)
        Me.rdoCutOpt2.TabIndex = 7
        Me.rdoCutOpt2.TabStop = True
        Me.rdoCutOpt2.Tag = "CUTOPT2"
        Me.rdoCutOpt2.Text = "반올림"
        Me.rdoCutOpt2.UseVisualStyleBackColor = False
        '
        'rdoCutOpt1
        '
        Me.rdoCutOpt1.BackColor = System.Drawing.Color.Transparent
        Me.rdoCutOpt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoCutOpt1.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.rdoCutOpt1.Location = New System.Drawing.Point(4, 0)
        Me.rdoCutOpt1.Name = "rdoCutOpt1"
        Me.rdoCutOpt1.Size = New System.Drawing.Size(60, 20)
        Me.rdoCutOpt1.TabIndex = 6
        Me.rdoCutOpt1.TabStop = True
        Me.rdoCutOpt1.Tag = "CUTOPT1"
        Me.rdoCutOpt1.Text = "올림"
        Me.rdoCutOpt1.UseVisualStyleBackColor = False
        '
        'pnlRstGbn
        '
        Me.pnlRstGbn.Controls.Add(Me.rdoRstType1)
        Me.pnlRstGbn.Controls.Add(Me.rdoRstType0)
        Me.pnlRstGbn.Location = New System.Drawing.Point(96, 16)
        Me.pnlRstGbn.Name = "pnlRstGbn"
        Me.pnlRstGbn.Size = New System.Drawing.Size(236, 20)
        Me.pnlRstGbn.TabIndex = 1
        '
        'rdoRstType1
        '
        Me.rdoRstType1.BackColor = System.Drawing.Color.Transparent
        Me.rdoRstType1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoRstType1.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoRstType1.Location = New System.Drawing.Point(136, 0)
        Me.rdoRstType1.Name = "rdoRstType1"
        Me.rdoRstType1.Size = New System.Drawing.Size(96, 20)
        Me.rdoRstType1.TabIndex = 2
        Me.rdoRstType1.Tag = "RSTTYPE1"
        Me.rdoRstType1.Text = "숫자만 허용"
        Me.rdoRstType1.UseVisualStyleBackColor = False
        '
        'rdoRstType0
        '
        Me.rdoRstType0.BackColor = System.Drawing.Color.Transparent
        Me.rdoRstType0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoRstType0.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.rdoRstType0.Location = New System.Drawing.Point(4, 0)
        Me.rdoRstType0.Name = "rdoRstType0"
        Me.rdoRstType0.Size = New System.Drawing.Size(128, 20)
        Me.rdoRstType0.TabIndex = 1
        Me.rdoRstType0.Tag = "RSTTYPE0"
        Me.rdoRstType0.Text = "문자 + 숫자 혼합"
        Me.rdoRstType0.UseVisualStyleBackColor = False
        '
        'cboALimitHS
        '
        Me.cboALimitHS.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboALimitHS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboALimitHS.Items.AddRange(New Object() {"[0] 없음", "[1] 결과를 '허용상한수치'로        : 예) 2000", "[2] 결과를 '> 허용상한수치'로      : 예) > 2000", "[3] 결과를 '허용하한수치 이상'으로 : 예) 2000 이상", "[4] 결과를 '허용하한수치 초과'로   : 예) 2000 초과", "[5] 결과를 '> 허용상한수치'로      : 예) >= 2000"})
        Me.cboALimitHS.Location = New System.Drawing.Point(396, 507)
        Me.cboALimitHS.MaxDropDownItems = 10
        Me.cboALimitHS.Name = "cboALimitHS"
        Me.cboALimitHS.Size = New System.Drawing.Size(358, 20)
        Me.cboALimitHS.TabIndex = 34
        Me.cboALimitHS.Tag = "ALIMITHS_01"
        '
        'cboALimitLS
        '
        Me.cboALimitLS.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboALimitLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboALimitLS.Items.AddRange(New Object() {"[0] 없음", "[1] 결과를 '허용하한수치'로        : 예) 20", "[2] 결과를 '< 허용하한수치'로      : 예) < 20", "[3] 결과를 '허용하한수치 이하'로   : 예) 20 이하", "[4] 결과를 '허용하한수치 미만'으로 : 예) 20 미만", "[5] 결과를 '<= 허용하한수치'로      : 예) <= 20"})
        Me.cboALimitLS.Location = New System.Drawing.Point(396, 485)
        Me.cboALimitLS.MaxDropDownItems = 10
        Me.cboALimitLS.Name = "cboALimitLS"
        Me.cboALimitLS.Size = New System.Drawing.Size(358, 20)
        Me.cboALimitLS.TabIndex = 32
        Me.cboALimitLS.Tag = "ALIMITLS_01"
        '
        'lblALimitHS
        '
        Me.lblALimitHS.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblALimitHS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblALimitHS.ForeColor = System.Drawing.Color.Black
        Me.lblALimitHS.Location = New System.Drawing.Point(307, 506)
        Me.lblALimitHS.Name = "lblALimitHS"
        Me.lblALimitHS.Size = New System.Drawing.Size(88, 21)
        Me.lblALimitHS.TabIndex = 0
        Me.lblALimitHS.Text = "결과처리방법"
        Me.lblALimitHS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblALimitLS
        '
        Me.lblALimitLS.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblALimitLS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblALimitLS.ForeColor = System.Drawing.Color.Black
        Me.lblALimitLS.Location = New System.Drawing.Point(307, 484)
        Me.lblALimitLS.Name = "lblALimitLS"
        Me.lblALimitLS.Size = New System.Drawing.Size(88, 21)
        Me.lblALimitLS.TabIndex = 0
        Me.lblALimitLS.Text = "결과처리방법"
        Me.lblALimitLS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblJudgType3
        '
        Me.lblJudgType3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblJudgType3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJudgType3.ForeColor = System.Drawing.Color.Black
        Me.lblJudgType3.Location = New System.Drawing.Point(307, 289)
        Me.lblJudgType3.Name = "lblJudgType3"
        Me.lblJudgType3.Size = New System.Drawing.Size(88, 21)
        Me.lblJudgType3.TabIndex = 0
        Me.lblJudgType3.Text = "결과처리방법"
        Me.lblJudgType3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblJudgType2
        '
        Me.lblJudgType2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblJudgType2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJudgType2.ForeColor = System.Drawing.Color.Black
        Me.lblJudgType2.Location = New System.Drawing.Point(307, 267)
        Me.lblJudgType2.Name = "lblJudgType2"
        Me.lblJudgType2.Size = New System.Drawing.Size(88, 21)
        Me.lblJudgType2.TabIndex = 0
        Me.lblJudgType2.Text = "결과처리방법"
        Me.lblJudgType2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblJudgType1
        '
        Me.lblJudgType1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblJudgType1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJudgType1.ForeColor = System.Drawing.Color.Black
        Me.lblJudgType1.Location = New System.Drawing.Point(307, 244)
        Me.lblJudgType1.Name = "lblJudgType1"
        Me.lblJudgType1.Size = New System.Drawing.Size(88, 21)
        Me.lblJudgType1.TabIndex = 0
        Me.lblJudgType1.Text = "결과처리방법"
        Me.lblJudgType1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblALimitH
        '
        Me.lblALimitH.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblALimitH.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblALimitH.ForeColor = System.Drawing.Color.Black
        Me.lblALimitH.Location = New System.Drawing.Point(111, 506)
        Me.lblALimitH.Name = "lblALimitH"
        Me.lblALimitH.Size = New System.Drawing.Size(60, 21)
        Me.lblALimitH.TabIndex = 0
        Me.lblALimitH.Text = "허용상한"
        Me.lblALimitH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblALimitL
        '
        Me.lblALimitL.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblALimitL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblALimitL.ForeColor = System.Drawing.Color.Black
        Me.lblALimitL.Location = New System.Drawing.Point(111, 484)
        Me.lblALimitL.Name = "lblALimitL"
        Me.lblALimitL.Size = New System.Drawing.Size(60, 21)
        Me.lblALimitL.TabIndex = 0
        Me.lblALimitL.Text = "허용하한"
        Me.lblALimitL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtALimitH
        '
        Me.txtALimitH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtALimitH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtALimitH.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtALimitH.Location = New System.Drawing.Point(172, 506)
        Me.txtALimitH.MaxLength = 20
        Me.txtALimitH.Name = "txtALimitH"
        Me.txtALimitH.Size = New System.Drawing.Size(128, 21)
        Me.txtALimitH.TabIndex = 33
        Me.txtALimitH.Tag = "ALIMITH"
        Me.txtALimitH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtALimitL
        '
        Me.txtALimitL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtALimitL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtALimitL.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtALimitL.Location = New System.Drawing.Point(172, 484)
        Me.txtALimitL.MaxLength = 20
        Me.txtALimitL.Name = "txtALimitL"
        Me.txtALimitL.Size = New System.Drawing.Size(128, 21)
        Me.txtALimitL.TabIndex = 31
        Me.txtALimitL.Tag = "ALIMITL"
        Me.txtALimitL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboALimitGbn
        '
        Me.cboALimitGbn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboALimitGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboALimitGbn.Items.AddRange(New Object() {"[0] 사용 안함", "[1] 허용하한만 사용", "[2] 허용상한만 사용", "[3] 모두 사용"})
        Me.cboALimitGbn.Location = New System.Drawing.Point(96, 460)
        Me.cboALimitGbn.MaxDropDownItems = 10
        Me.cboALimitGbn.Name = "cboALimitGbn"
        Me.cboALimitGbn.Size = New System.Drawing.Size(144, 20)
        Me.cboALimitGbn.TabIndex = 30
        Me.cboALimitGbn.Tag = "ALIMITGBN_01"
        '
        'lblALimitGbn
        '
        Me.lblALimitGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblALimitGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblALimitGbn.ForeColor = System.Drawing.Color.White
        Me.lblALimitGbn.Location = New System.Drawing.Point(8, 460)
        Me.lblALimitGbn.Name = "lblALimitGbn"
        Me.lblALimitGbn.Size = New System.Drawing.Size(87, 21)
        Me.lblALimitGbn.TabIndex = 0
        Me.lblALimitGbn.Text = "허용치구분"
        Me.lblALimitGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDeltaDay
        '
        Me.lblDeltaDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDeltaDay.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeltaDay.ForeColor = System.Drawing.Color.Black
        Me.lblDeltaDay.Location = New System.Drawing.Point(143, 424)
        Me.lblDeltaDay.Name = "lblDeltaDay"
        Me.lblDeltaDay.Size = New System.Drawing.Size(128, 21)
        Me.lblDeltaDay.TabIndex = 0
        Me.lblDeltaDay.Text = "Delta기간 (일:Day)"
        Me.lblDeltaDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDeltaDay
        '
        Me.txtDeltaDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeltaDay.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDeltaDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDeltaDay.Location = New System.Drawing.Point(272, 424)
        Me.txtDeltaDay.MaxLength = 3
        Me.txtDeltaDay.Name = "txtDeltaDay"
        Me.txtDeltaDay.Size = New System.Drawing.Size(28, 21)
        Me.txtDeltaDay.TabIndex = 27
        Me.txtDeltaDay.Tag = "DELTADAY"
        Me.txtDeltaDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblLine10
        '
        Me.lblLine10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine10.Location = New System.Drawing.Point(4, 212)
        Me.lblLine10.Name = "lblLine10"
        Me.lblLine10.Size = New System.Drawing.Size(756, 2)
        Me.lblLine10.TabIndex = 0
        '
        'lblDeltaH
        '
        Me.lblDeltaH.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDeltaH.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeltaH.ForeColor = System.Drawing.Color.Black
        Me.lblDeltaH.Location = New System.Drawing.Point(536, 424)
        Me.lblDeltaH.Name = "lblDeltaH"
        Me.lblDeltaH.Size = New System.Drawing.Size(88, 21)
        Me.lblDeltaH.TabIndex = 0
        Me.lblDeltaH.Text = "Delta상한"
        Me.lblDeltaH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDeltaL
        '
        Me.lblDeltaL.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDeltaL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeltaL.ForeColor = System.Drawing.Color.Black
        Me.lblDeltaL.Location = New System.Drawing.Point(309, 424)
        Me.lblDeltaL.Name = "lblDeltaL"
        Me.lblDeltaL.Size = New System.Drawing.Size(88, 21)
        Me.lblDeltaL.TabIndex = 0
        Me.lblDeltaL.Text = "Delta하한"
        Me.lblDeltaL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDeltaH
        '
        Me.txtDeltaH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeltaH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDeltaH.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDeltaH.Location = New System.Drawing.Point(626, 424)
        Me.txtDeltaH.MaxLength = 20
        Me.txtDeltaH.Name = "txtDeltaH"
        Me.txtDeltaH.Size = New System.Drawing.Size(129, 21)
        Me.txtDeltaH.TabIndex = 29
        Me.txtDeltaH.Tag = "DELTAH"
        Me.txtDeltaH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDeltaL
        '
        Me.txtDeltaL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeltaL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDeltaL.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtDeltaL.Location = New System.Drawing.Point(398, 424)
        Me.txtDeltaL.MaxLength = 20
        Me.txtDeltaL.Name = "txtDeltaL"
        Me.txtDeltaL.Size = New System.Drawing.Size(130, 21)
        Me.txtDeltaL.TabIndex = 28
        Me.txtDeltaL.Tag = "DELTAL"
        Me.txtDeltaL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboDeltaGbn
        '
        Me.cboDeltaGbn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDeltaGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDeltaGbn.Items.AddRange(New Object() {"[0] 사용 안함", "[1] 변화차 = 현재결과 - 이전결과", "[2] 변화비율 = 변화차/이전결과  * 100", "[3] 기간당 변화차 = 변화차/기간", "[4] 기간당 변화비율 = 변화비율/기간", "[5] Grade Delta = |현재Grade - 이전Grade|"})
        Me.cboDeltaGbn.Location = New System.Drawing.Point(96, 395)
        Me.cboDeltaGbn.MaxDropDownItems = 10
        Me.cboDeltaGbn.Name = "cboDeltaGbn"
        Me.cboDeltaGbn.Size = New System.Drawing.Size(304, 20)
        Me.cboDeltaGbn.TabIndex = 26
        Me.cboDeltaGbn.Tag = "DELTAGBN_01"
        '
        'lblDeltaGbn
        '
        Me.lblDeltaGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDeltaGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeltaGbn.ForeColor = System.Drawing.Color.White
        Me.lblDeltaGbn.Location = New System.Drawing.Point(7, 394)
        Me.lblDeltaGbn.Name = "lblDeltaGbn"
        Me.lblDeltaGbn.Size = New System.Drawing.Size(87, 21)
        Me.lblDeltaGbn.TabIndex = 0
        Me.lblDeltaGbn.Text = "Delta구분"
        Me.lblDeltaGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAlertH
        '
        Me.lblAlertH.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblAlertH.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAlertH.ForeColor = System.Drawing.Color.Black
        Me.lblAlertH.Location = New System.Drawing.Point(535, 372)
        Me.lblAlertH.Name = "lblAlertH"
        Me.lblAlertH.Size = New System.Drawing.Size(88, 21)
        Me.lblAlertH.TabIndex = 0
        Me.lblAlertH.Text = "Alert상한"
        Me.lblAlertH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAlertL
        '
        Me.lblAlertL.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblAlertL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAlertL.ForeColor = System.Drawing.Color.Black
        Me.lblAlertL.Location = New System.Drawing.Point(307, 372)
        Me.lblAlertL.Name = "lblAlertL"
        Me.lblAlertL.Size = New System.Drawing.Size(88, 21)
        Me.lblAlertL.TabIndex = 0
        Me.lblAlertL.Text = "Alert하한"
        Me.lblAlertL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAlertH
        '
        Me.txtAlertH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlertH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAlertH.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAlertH.Location = New System.Drawing.Point(624, 372)
        Me.txtAlertH.MaxLength = 20
        Me.txtAlertH.Name = "txtAlertH"
        Me.txtAlertH.Size = New System.Drawing.Size(132, 21)
        Me.txtAlertH.TabIndex = 25
        Me.txtAlertH.Tag = "ALERTH"
        Me.txtAlertH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAlertL
        '
        Me.txtAlertL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlertL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAlertL.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAlertL.Location = New System.Drawing.Point(396, 372)
        Me.txtAlertL.MaxLength = 20
        Me.txtAlertL.Name = "txtAlertL"
        Me.txtAlertL.Size = New System.Drawing.Size(132, 21)
        Me.txtAlertL.TabIndex = 24
        Me.txtAlertL.Tag = "ALERTL"
        Me.txtAlertL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboAlertGbn
        '
        Me.cboAlertGbn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAlertGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAlertGbn.Items.AddRange(New Object() {"[0] 사용 안함", "[1] Alert하한만 사용", "[2] Alert상한만 사용", "[3] 모두 사용", "[4] 문자값 비교", "[5] Alert Rule 적용", "[7] 결과코드 적용", "[A] Alert하한, Alert Rule 적용", "[B] Alert상한, Alert Rule 적용", "[C] 모두, Alert Rule 적용"})
        Me.cboAlertGbn.Location = New System.Drawing.Point(96, 373)
        Me.cboAlertGbn.MaxDropDownItems = 10
        Me.cboAlertGbn.Name = "cboAlertGbn"
        Me.cboAlertGbn.Size = New System.Drawing.Size(204, 20)
        Me.cboAlertGbn.TabIndex = 23
        Me.cboAlertGbn.Tag = "ALERTGBN_01"
        '
        'lblAlertGbn
        '
        Me.lblAlertGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblAlertGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAlertGbn.ForeColor = System.Drawing.Color.White
        Me.lblAlertGbn.Location = New System.Drawing.Point(7, 372)
        Me.lblAlertGbn.Name = "lblAlertGbn"
        Me.lblAlertGbn.Size = New System.Drawing.Size(87, 21)
        Me.lblAlertGbn.TabIndex = 0
        Me.lblAlertGbn.Text = "Alert구분"
        Me.lblAlertGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCriticalH
        '
        Me.lblCriticalH.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblCriticalH.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCriticalH.ForeColor = System.Drawing.Color.Black
        Me.lblCriticalH.Location = New System.Drawing.Point(535, 350)
        Me.lblCriticalH.Name = "lblCriticalH"
        Me.lblCriticalH.Size = New System.Drawing.Size(88, 21)
        Me.lblCriticalH.TabIndex = 0
        Me.lblCriticalH.Text = "Critical상한"
        Me.lblCriticalH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCriticalL
        '
        Me.lblCriticalL.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblCriticalL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCriticalL.ForeColor = System.Drawing.Color.Black
        Me.lblCriticalL.Location = New System.Drawing.Point(307, 350)
        Me.lblCriticalL.Name = "lblCriticalL"
        Me.lblCriticalL.Size = New System.Drawing.Size(88, 21)
        Me.lblCriticalL.TabIndex = 0
        Me.lblCriticalL.Text = "Critical하한"
        Me.lblCriticalL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCriticalH
        '
        Me.txtCriticalH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCriticalH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCriticalH.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCriticalH.Location = New System.Drawing.Point(624, 350)
        Me.txtCriticalH.MaxLength = 20
        Me.txtCriticalH.Name = "txtCriticalH"
        Me.txtCriticalH.Size = New System.Drawing.Size(132, 21)
        Me.txtCriticalH.TabIndex = 22
        Me.txtCriticalH.Tag = "CRITICALH"
        Me.txtCriticalH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCriticalL
        '
        Me.txtCriticalL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCriticalL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCriticalL.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCriticalL.Location = New System.Drawing.Point(396, 350)
        Me.txtCriticalL.MaxLength = 20
        Me.txtCriticalL.Name = "txtCriticalL"
        Me.txtCriticalL.Size = New System.Drawing.Size(132, 21)
        Me.txtCriticalL.TabIndex = 21
        Me.txtCriticalL.Tag = "CRITICALL"
        Me.txtCriticalL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboCriticalGbn
        '
        Me.cboCriticalGbn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCriticalGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCriticalGbn.Items.AddRange(New Object() {"[0] 사용 안함", "[1] Critical하한만 사용", "[2] Critical상한만 사용", "[3] 모두 사용", "[4] x", "[5] x", "[6] x", "[7] 문자결과(결과코드 설정)"})
        Me.cboCriticalGbn.Location = New System.Drawing.Point(96, 351)
        Me.cboCriticalGbn.MaxDropDownItems = 10
        Me.cboCriticalGbn.Name = "cboCriticalGbn"
        Me.cboCriticalGbn.Size = New System.Drawing.Size(204, 20)
        Me.cboCriticalGbn.TabIndex = 20
        Me.cboCriticalGbn.Tag = "CRITICALGBN_01"
        '
        'lblCriticalGbn
        '
        Me.lblCriticalGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCriticalGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCriticalGbn.ForeColor = System.Drawing.Color.White
        Me.lblCriticalGbn.Location = New System.Drawing.Point(7, 350)
        Me.lblCriticalGbn.Name = "lblCriticalGbn"
        Me.lblCriticalGbn.Size = New System.Drawing.Size(87, 21)
        Me.lblCriticalGbn.TabIndex = 0
        Me.lblCriticalGbn.Text = "Critical구분"
        Me.lblCriticalGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPanicH
        '
        Me.lblPanicH.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblPanicH.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPanicH.ForeColor = System.Drawing.Color.Black
        Me.lblPanicH.Location = New System.Drawing.Point(535, 328)
        Me.lblPanicH.Name = "lblPanicH"
        Me.lblPanicH.Size = New System.Drawing.Size(88, 21)
        Me.lblPanicH.TabIndex = 0
        Me.lblPanicH.Text = "Panic상한"
        Me.lblPanicH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPanicL
        '
        Me.lblPanicL.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblPanicL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPanicL.ForeColor = System.Drawing.Color.Black
        Me.lblPanicL.Location = New System.Drawing.Point(307, 328)
        Me.lblPanicL.Name = "lblPanicL"
        Me.lblPanicL.Size = New System.Drawing.Size(88, 21)
        Me.lblPanicL.TabIndex = 0
        Me.lblPanicL.Text = "Panic하한"
        Me.lblPanicL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPanicH
        '
        Me.txtPanicH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPanicH.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPanicH.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPanicH.Location = New System.Drawing.Point(624, 328)
        Me.txtPanicH.MaxLength = 20
        Me.txtPanicH.Name = "txtPanicH"
        Me.txtPanicH.Size = New System.Drawing.Size(132, 21)
        Me.txtPanicH.TabIndex = 19
        Me.txtPanicH.Tag = "PANICH"
        Me.txtPanicH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPanicL
        '
        Me.txtPanicL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPanicL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPanicL.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPanicL.Location = New System.Drawing.Point(396, 328)
        Me.txtPanicL.MaxLength = 20
        Me.txtPanicL.Name = "txtPanicL"
        Me.txtPanicL.Size = New System.Drawing.Size(132, 21)
        Me.txtPanicL.TabIndex = 18
        Me.txtPanicL.Tag = "PANICL"
        Me.txtPanicL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboPanicGbn
        '
        Me.cboPanicGbn.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPanicGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPanicGbn.Items.AddRange(New Object() {"[0] 사용 안함", "[1] Panic하한만 사용", "[2] Panic상한만 사용", "[3] 모두 사용", "[4] Panic하한만 사용(Grade)", "[5] Panic상한만 사용(Grade)", "[6] 모두 사용(Grade)"})
        Me.cboPanicGbn.Location = New System.Drawing.Point(96, 329)
        Me.cboPanicGbn.MaxDropDownItems = 10
        Me.cboPanicGbn.Name = "cboPanicGbn"
        Me.cboPanicGbn.Size = New System.Drawing.Size(203, 20)
        Me.cboPanicGbn.TabIndex = 17
        Me.cboPanicGbn.Tag = "PANICGBN_01"
        '
        'lblPanicGbn
        '
        Me.lblPanicGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPanicGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPanicGbn.ForeColor = System.Drawing.Color.White
        Me.lblPanicGbn.Location = New System.Drawing.Point(7, 328)
        Me.lblPanicGbn.Name = "lblPanicGbn"
        Me.lblPanicGbn.Size = New System.Drawing.Size(87, 21)
        Me.lblPanicGbn.TabIndex = 0
        Me.lblPanicGbn.Text = "Panic구분"
        Me.lblPanicGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboJudgType3
        '
        Me.cboJudgType3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboJudgType3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboJudgType3.Items.AddRange(New Object() {"[0] 판정문자는 판정으로만  : 결과 → 300.1", "[1] 판정문자를 결과에 대체 : 결과 → Positive", "[2] 판정문자를 결과에 포함 : 결과 → Positive(300.1)", "[3] 판정문자를 결과에 포함 : 결과 → Positive 300.1", "[4] 판정문자를 결과에 포함 : 결과 → 300.1 Positive"})
        Me.cboJudgType3.Location = New System.Drawing.Point(396, 289)
        Me.cboJudgType3.MaxDropDownItems = 10
        Me.cboJudgType3.Name = "cboJudgType3"
        Me.cboJudgType3.Size = New System.Drawing.Size(360, 20)
        Me.cboJudgType3.TabIndex = 16
        Me.cboJudgType3.Tag = "JUDGTYPE13_01"
        '
        'cboJudgType2
        '
        Me.cboJudgType2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboJudgType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboJudgType2.Items.AddRange(New Object() {"[0] 판정문자는 판정으로만  : 결과 → 200.1", "[1] 판정문자를 결과에 대체 : 결과 → Trace", "[2] 판정문자를 결과에 포함 : 결과 → Trace(200.1)", "[3] 판정문자를 결과에 포함 : 결과 → Trace 200.1", "[4] 판정문자를 결과에 포함 : 결과 → 200.1 Trace"})
        Me.cboJudgType2.Location = New System.Drawing.Point(396, 267)
        Me.cboJudgType2.MaxDropDownItems = 10
        Me.cboJudgType2.Name = "cboJudgType2"
        Me.cboJudgType2.Size = New System.Drawing.Size(360, 20)
        Me.cboJudgType2.TabIndex = 14
        Me.cboJudgType2.Tag = "JUDGTYPE12_01"
        '
        'cboJudgType1
        '
        Me.cboJudgType1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboJudgType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboJudgType1.Items.AddRange(New Object() {"[0] 판정문자는 판정으로만  : 결과 → 100.1", "[1] 판정문자를 결과에 대체 : 결과 → Negative", "[2] 판정문자를 결과에 포함 : 결과 → Negative(100.1)", "[3] 판정문자를 결과에 포함 : 결과 → Negative 100.1", "[4] 판정문자를 결과에 포함 : 결과 → 100.1 Negative"})
        Me.cboJudgType1.Location = New System.Drawing.Point(396, 244)
        Me.cboJudgType1.MaxDropDownItems = 10
        Me.cboJudgType1.Name = "cboJudgType1"
        Me.cboJudgType1.Size = New System.Drawing.Size(360, 20)
        Me.cboJudgType1.TabIndex = 12
        Me.cboJudgType1.Tag = "JUDGTYPE11_01"
        '
        'txtUJudgLT3
        '
        Me.txtUJudgLT3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUJudgLT3.Location = New System.Drawing.Point(172, 290)
        Me.txtUJudgLT3.MaxLength = 20
        Me.txtUJudgLT3.Name = "txtUJudgLT3"
        Me.txtUJudgLT3.Size = New System.Drawing.Size(128, 21)
        Me.txtUJudgLT3.TabIndex = 15
        Me.txtUJudgLT3.Tag = "UJUDGLT3"
        '
        'lblUJudgLT3
        '
        Me.lblUJudgLT3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUJudgLT3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUJudgLT3.ForeColor = System.Drawing.Color.Black
        Me.lblUJudgLT3.Location = New System.Drawing.Point(99, 290)
        Me.lblUJudgLT3.Name = "lblUJudgLT3"
        Me.lblUJudgLT3.Size = New System.Drawing.Size(72, 21)
        Me.lblUJudgLT3.TabIndex = 0
        Me.lblUJudgLT3.Text = "판정문자 3"
        Me.lblUJudgLT3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUJudgLT2
        '
        Me.txtUJudgLT2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUJudgLT2.Location = New System.Drawing.Point(172, 267)
        Me.txtUJudgLT2.MaxLength = 20
        Me.txtUJudgLT2.Name = "txtUJudgLT2"
        Me.txtUJudgLT2.Size = New System.Drawing.Size(128, 21)
        Me.txtUJudgLT2.TabIndex = 13
        Me.txtUJudgLT2.Tag = "UJUDGLT2"
        '
        'lblUJudgLT2
        '
        Me.lblUJudgLT2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUJudgLT2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUJudgLT2.ForeColor = System.Drawing.Color.Black
        Me.lblUJudgLT2.Location = New System.Drawing.Point(99, 267)
        Me.lblUJudgLT2.Name = "lblUJudgLT2"
        Me.lblUJudgLT2.Size = New System.Drawing.Size(72, 21)
        Me.lblUJudgLT2.TabIndex = 0
        Me.lblUJudgLT2.Text = "판정문자 2"
        Me.lblUJudgLT2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUJudgLT1
        '
        Me.txtUJudgLT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUJudgLT1.Location = New System.Drawing.Point(172, 244)
        Me.txtUJudgLT1.MaxLength = 20
        Me.txtUJudgLT1.Name = "txtUJudgLT1"
        Me.txtUJudgLT1.Size = New System.Drawing.Size(128, 21)
        Me.txtUJudgLT1.TabIndex = 11
        Me.txtUJudgLT1.Tag = "UJUDGLT1"
        '
        'lblUJudgLT1
        '
        Me.lblUJudgLT1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUJudgLT1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUJudgLT1.ForeColor = System.Drawing.Color.Black
        Me.lblUJudgLT1.Location = New System.Drawing.Point(99, 244)
        Me.lblUJudgLT1.Name = "lblUJudgLT1"
        Me.lblUJudgLT1.Size = New System.Drawing.Size(72, 21)
        Me.lblUJudgLT1.TabIndex = 0
        Me.lblUJudgLT1.Text = "판정문자 1"
        Me.lblUJudgLT1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblJudgType
        '
        Me.lblJudgType.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblJudgType.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJudgType.ForeColor = System.Drawing.Color.White
        Me.lblJudgType.Location = New System.Drawing.Point(8, 220)
        Me.lblJudgType.Name = "lblJudgType"
        Me.lblJudgType.Size = New System.Drawing.Size(88, 21)
        Me.lblJudgType.TabIndex = 0
        Me.lblJudgType.Text = "판정유형"
        Me.lblJudgType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnDescRef
        '
        Me.btnDescRef.BackColor = System.Drawing.SystemColors.Control
        Me.btnDescRef.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDescRef.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDescRef.Location = New System.Drawing.Point(272, 74)
        Me.btnDescRef.Name = "btnDescRef"
        Me.btnDescRef.Size = New System.Drawing.Size(192, 24)
        Me.btnDescRef.TabIndex = 7
        Me.btnDescRef.TabStop = False
        Me.btnDescRef.Text = "서술형(텍스트형) 참고치 설정"
        Me.btnDescRef.UseVisualStyleBackColor = False
        '
        'txtRstUnit
        '
        Me.txtRstUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRstUnit.Location = New System.Drawing.Point(508, 76)
        Me.txtRstUnit.MaxLength = 20
        Me.txtRstUnit.Name = "txtRstUnit"
        Me.txtRstUnit.Size = New System.Drawing.Size(128, 21)
        Me.txtRstUnit.TabIndex = 8
        Me.txtRstUnit.Tag = "RSTUNIT"
        '
        'lblRstUnit
        '
        Me.lblRstUnit.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstUnit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstUnit.ForeColor = System.Drawing.Color.White
        Me.lblRstUnit.Location = New System.Drawing.Point(471, 76)
        Me.lblRstUnit.Name = "lblRstUnit"
        Me.lblRstUnit.Size = New System.Drawing.Size(36, 21)
        Me.lblRstUnit.TabIndex = 0
        Me.lblRstUnit.Text = "단위"
        Me.lblRstUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCutOpt
        '
        Me.lblCutOpt.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCutOpt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCutOpt.ForeColor = System.Drawing.Color.White
        Me.lblCutOpt.Location = New System.Drawing.Point(483, 40)
        Me.lblCutOpt.Name = "lblCutOpt"
        Me.lblCutOpt.Size = New System.Drawing.Size(72, 21)
        Me.lblCutOpt.TabIndex = 0
        Me.lblCutOpt.Text = "반올림옵션"
        Me.lblCutOpt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkRstLen
        '
        Me.chkRstLen.BackColor = System.Drawing.Color.Transparent
        Me.chkRstLen.Location = New System.Drawing.Point(100, 40)
        Me.chkRstLen.Name = "chkRstLen"
        Me.chkRstLen.Size = New System.Drawing.Size(168, 20)
        Me.chkRstLen.TabIndex = 2
        Me.chkRstLen.Tag = "RSTLEN"
        Me.chkRstLen.Text = "숫자결과 크기 제한 적용"
        Me.chkRstLen.UseVisualStyleBackColor = False
        '
        'cboRstLLen
        '
        Me.cboRstLLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRstLLen.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"})
        Me.cboRstLLen.Location = New System.Drawing.Point(440, 40)
        Me.cboRstLLen.MaxDropDownItems = 9
        Me.cboRstLLen.Name = "cboRstLLen"
        Me.cboRstLLen.Size = New System.Drawing.Size(36, 20)
        Me.cboRstLLen.TabIndex = 4
        Me.cboRstLLen.Tag = "RSTLLEN_01"
        '
        'lblRstLLen
        '
        Me.lblRstLLen.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstLLen.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstLLen.ForeColor = System.Drawing.Color.White
        Me.lblRstLLen.Location = New System.Drawing.Point(379, 40)
        Me.lblRstLLen.Name = "lblRstLLen"
        Me.lblRstLLen.Size = New System.Drawing.Size(60, 21)
        Me.lblRstLLen.TabIndex = 0
        Me.lblRstLLen.Text = "소수크기"
        Me.lblRstLLen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboRstULen
        '
        Me.cboRstULen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRstULen.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"})
        Me.cboRstULen.Location = New System.Drawing.Point(336, 40)
        Me.cboRstULen.MaxDropDownItems = 9
        Me.cboRstULen.Name = "cboRstULen"
        Me.cboRstULen.Size = New System.Drawing.Size(36, 20)
        Me.cboRstULen.TabIndex = 3
        Me.cboRstULen.Tag = "RSTULEN_01"
        '
        'lblRstType
        '
        Me.lblRstType.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstType.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstType.ForeColor = System.Drawing.Color.White
        Me.lblRstType.Location = New System.Drawing.Point(8, 16)
        Me.lblRstType.Name = "lblRstType"
        Me.lblRstType.Size = New System.Drawing.Size(88, 21)
        Me.lblRstType.TabIndex = 0
        Me.lblRstType.Text = "결과유형"
        Me.lblRstType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLine8
        '
        Me.lblLine8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine8.Location = New System.Drawing.Point(4, 320)
        Me.lblLine8.Name = "lblLine8"
        Me.lblLine8.Size = New System.Drawing.Size(756, 2)
        Me.lblLine8.TabIndex = 0
        '
        'lblLine9
        '
        Me.lblLine9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine9.Location = New System.Drawing.Point(8, 452)
        Me.lblLine9.Name = "lblLine9"
        Me.lblLine9.Size = New System.Drawing.Size(756, 2)
        Me.lblLine9.TabIndex = 0
        '
        'lblLine11
        '
        Me.lblLine11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine11.Location = New System.Drawing.Point(4, 68)
        Me.lblLine11.Name = "lblLine11"
        Me.lblLine11.Size = New System.Drawing.Size(756, 2)
        Me.lblLine11.TabIndex = 0
        '
        'lblRefGbn
        '
        Me.lblRefGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRefGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRefGbn.ForeColor = System.Drawing.Color.White
        Me.lblRefGbn.Location = New System.Drawing.Point(8, 76)
        Me.lblRefGbn.Name = "lblRefGbn"
        Me.lblRefGbn.Size = New System.Drawing.Size(88, 21)
        Me.lblRefGbn.TabIndex = 0
        Me.lblRefGbn.Text = "참고치유형"
        Me.lblRefGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRstULen
        '
        Me.lblRstULen.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstULen.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstULen.ForeColor = System.Drawing.Color.White
        Me.lblRstULen.Location = New System.Drawing.Point(275, 40)
        Me.lblRstULen.Name = "lblRstULen"
        Me.lblRstULen.Size = New System.Drawing.Size(60, 21)
        Me.lblRstULen.TabIndex = 0
        Me.lblRstULen.Text = "정수크기"
        Me.lblRstULen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tpgTest3
        '
        Me.tpgTest3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgTest3.Controls.Add(Me.Label17)
        Me.tpgTest3.Controls.Add(Me.txtTestInfo5)
        Me.tpgTest3.Controls.Add(Me.txtTestInfo4)
        Me.tpgTest3.Controls.Add(Me.Label14)
        Me.tpgTest3.Controls.Add(Me.Label5)
        Me.tpgTest3.Controls.Add(Me.txtTestInfo3)
        Me.tpgTest3.Controls.Add(Me.Label4)
        Me.tpgTest3.Controls.Add(Me.txtTestInfo2)
        Me.tpgTest3.Controls.Add(Me.Label3)
        Me.tpgTest3.Controls.Add(Me.txtTestInfo1)
        Me.tpgTest3.Controls.Add(Me.Label2)
        Me.tpgTest3.Controls.Add(Me.grpDTest)
        Me.tpgTest3.Controls.Add(Me.grpRTest)
        Me.tpgTest3.Location = New System.Drawing.Point(4, 22)
        Me.tpgTest3.Name = "tpgTest3"
        Me.tpgTest3.Size = New System.Drawing.Size(773, 863)
        Me.tpgTest3.TabIndex = 2
        Me.tpgTest3.Text = "세부/참조/검사정보"
        Me.tpgTest3.UseVisualStyleBackColor = True
        Me.tpgTest3.Visible = False
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label17.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(9, 648)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(97, 119)
        Me.Label17.TabIndex = 224
        Me.Label17.Text = "검체채취 및" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "의뢰시 주의사항"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestInfo5
        '
        Me.txtTestInfo5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestInfo5.Location = New System.Drawing.Point(108, 648)
        Me.txtTestInfo5.MaxLength = 4000
        Me.txtTestInfo5.Multiline = True
        Me.txtTestInfo5.Name = "txtTestInfo5"
        Me.txtTestInfo5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTestInfo5.Size = New System.Drawing.Size(344, 119)
        Me.txtTestInfo5.TabIndex = 225
        Me.txtTestInfo5.Tag = "TESTINFO2"
        '
        'txtTestInfo4
        '
        Me.txtTestInfo4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestInfo4.Location = New System.Drawing.Point(428, 525)
        Me.txtTestInfo4.MaxLength = 2000
        Me.txtTestInfo4.Multiline = True
        Me.txtTestInfo4.Name = "txtTestInfo4"
        Me.txtTestInfo4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTestInfo4.Size = New System.Drawing.Size(344, 119)
        Me.txtTestInfo4.TabIndex = 223
        Me.txtTestInfo4.Tag = "TESTINFO3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(399, 525)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(27, 119)
        Me.Label14.TabIndex = 222
        Me.Label14.Text = " 검  사  의  뢰  정  보"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(399, 402)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 119)
        Me.Label5.TabIndex = 220
        Me.Label5.Text = " 임  상  적  의  의"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestInfo3
        '
        Me.txtTestInfo3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestInfo3.Location = New System.Drawing.Point(428, 402)
        Me.txtTestInfo3.MaxLength = 2000
        Me.txtTestInfo3.Multiline = True
        Me.txtTestInfo3.Name = "txtTestInfo3"
        Me.txtTestInfo3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTestInfo3.Size = New System.Drawing.Size(344, 119)
        Me.txtTestInfo3.TabIndex = 221
        Me.txtTestInfo3.Tag = "TESTINFO3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(9, 525)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 119)
        Me.Label4.TabIndex = 218
        Me.Label4.Text = " 주  의  내  용"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestInfo2
        '
        Me.txtTestInfo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestInfo2.Location = New System.Drawing.Point(38, 525)
        Me.txtTestInfo2.MaxLength = 2000
        Me.txtTestInfo2.Multiline = True
        Me.txtTestInfo2.Name = "txtTestInfo2"
        Me.txtTestInfo2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTestInfo2.Size = New System.Drawing.Size(344, 119)
        Me.txtTestInfo2.TabIndex = 219
        Me.txtTestInfo2.Tag = "TESTINFO2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(9, 402)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 119)
        Me.Label3.TabIndex = 216
        Me.Label3.Text = " 검  사  법"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestInfo1
        '
        Me.txtTestInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestInfo1.Location = New System.Drawing.Point(38, 402)
        Me.txtTestInfo1.MaxLength = 2000
        Me.txtTestInfo1.Multiline = True
        Me.txtTestInfo1.Name = "txtTestInfo1"
        Me.txtTestInfo1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTestInfo1.Size = New System.Drawing.Size(344, 119)
        Me.txtTestInfo1.TabIndex = 217
        Me.txtTestInfo1.Tag = "TESTINFO2"
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(4, 384)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(775, 9)
        Me.Label2.TabIndex = 215
        Me.Label2.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" &
    "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'grpDTest
        '
        Me.grpDTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpDTest.Controls.Add(Me.BtnTestChg)
        Me.grpDTest.Controls.Add(Me.btnDTDel)
        Me.grpDTest.Controls.Add(Me.chkGrpRstYn)
        Me.grpDTest.Controls.Add(Me.chkAddModeD)
        Me.grpDTest.Controls.Add(Me.spdDTest)
        Me.grpDTest.Controls.Add(Me.lblText1)
        Me.grpDTest.Location = New System.Drawing.Point(8, 4)
        Me.grpDTest.Name = "grpDTest"
        Me.grpDTest.Size = New System.Drawing.Size(376, 378)
        Me.grpDTest.TabIndex = 9
        Me.grpDTest.TabStop = False
        Me.grpDTest.Text = "세부검사정보"
        '
        'BtnTestChg
        '
        Me.BtnTestChg.Location = New System.Drawing.Point(10, 11)
        Me.BtnTestChg.Name = "BtnTestChg"
        Me.BtnTestChg.Size = New System.Drawing.Size(167, 24)
        Me.BtnTestChg.TabIndex = 201
        Me.BtnTestChg.Text = "검사의뢰지침 검사항목설정"
        '
        'btnDTDel
        '
        Me.btnDTDel.Location = New System.Drawing.Point(177, 11)
        Me.btnDTDel.Name = "btnDTDel"
        Me.btnDTDel.Size = New System.Drawing.Size(188, 24)
        Me.btnDTDel.TabIndex = 112
        Me.btnDTDel.Text = "선택한 검사를 화면에서 제거"
        '
        'chkGrpRstYn
        '
        Me.chkGrpRstYn.AutoSize = True
        Me.chkGrpRstYn.Location = New System.Drawing.Point(244, 38)
        Me.chkGrpRstYn.Name = "chkGrpRstYn"
        Me.chkGrpRstYn.Size = New System.Drawing.Size(120, 16)
        Me.chkGrpRstYn.TabIndex = 200
        Me.chkGrpRstYn.Tag = "GRPRSTYN"
        Me.chkGrpRstYn.Text = "그룹결과보고여부"
        Me.chkGrpRstYn.UseVisualStyleBackColor = True
        '
        'chkAddModeD
        '
        Me.chkAddModeD.Location = New System.Drawing.Point(10, 36)
        Me.chkAddModeD.Name = "chkAddModeD"
        Me.chkAddModeD.Size = New System.Drawing.Size(104, 20)
        Me.chkAddModeD.TabIndex = 115
        Me.chkAddModeD.Text = "검사추가 모드"
        '
        'spdDTest
        '
        Me.spdDTest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdDTest.DataSource = Nothing
        Me.spdDTest.Location = New System.Drawing.Point(10, 57)
        Me.spdDTest.Name = "spdDTest"
        Me.spdDTest.OcxState = CType(resources.GetObject("spdDTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDTest.Size = New System.Drawing.Size(356, 281)
        Me.spdDTest.TabIndex = 114
        '
        'lblText1
        '
        Me.lblText1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblText1.Location = New System.Drawing.Point(13, 346)
        Me.lblText1.Name = "lblText1"
        Me.lblText1.Size = New System.Drawing.Size(352, 12)
        Me.lblText1.TabIndex = 113
        Me.lblText1.Text = "Group Code 또는 Battery Code의 세부검사를 설정합니다."
        '
        'grpRTest
        '
        Me.grpRTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpRTest.Controls.Add(Me.btnRTDel)
        Me.grpRTest.Controls.Add(Me.chkAddModeR)
        Me.grpRTest.Controls.Add(Me.spdRTest)
        Me.grpRTest.Controls.Add(Me.lbltext2)
        Me.grpRTest.Location = New System.Drawing.Point(396, 4)
        Me.grpRTest.Name = "grpRTest"
        Me.grpRTest.Size = New System.Drawing.Size(376, 378)
        Me.grpRTest.TabIndex = 114
        Me.grpRTest.TabStop = False
        Me.grpRTest.Text = "참조검사정보"
        '
        'btnRTDel
        '
        Me.btnRTDel.Location = New System.Drawing.Point(178, 30)
        Me.btnRTDel.Name = "btnRTDel"
        Me.btnRTDel.Size = New System.Drawing.Size(188, 24)
        Me.btnRTDel.TabIndex = 112
        Me.btnRTDel.Text = "선택한 검사를 화면에서 제거"
        '
        'chkAddModeR
        '
        Me.chkAddModeR.Location = New System.Drawing.Point(10, 36)
        Me.chkAddModeR.Name = "chkAddModeR"
        Me.chkAddModeR.Size = New System.Drawing.Size(104, 20)
        Me.chkAddModeR.TabIndex = 116
        Me.chkAddModeR.Text = "검사추가 모드"
        '
        'spdRTest
        '
        Me.spdRTest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdRTest.DataSource = Nothing
        Me.spdRTest.Location = New System.Drawing.Point(10, 57)
        Me.spdRTest.Name = "spdRTest"
        Me.spdRTest.OcxState = CType(resources.GetObject("spdRTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRTest.Size = New System.Drawing.Size(356, 281)
        Me.spdRTest.TabIndex = 115
        '
        'lbltext2
        '
        Me.lbltext2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltext2.Location = New System.Drawing.Point(13, 346)
        Me.lbltext2.Name = "lbltext2"
        Me.lbltext2.Size = New System.Drawing.Size(352, 26)
        Me.lbltext2.TabIndex = 113
        Me.lbltext2.Text = "해당검사에서 결과를 참조할 검사를 설정합니다. 특수검사 등에서 해당환자의 참조결과를 표시하는데 사용됩니다."
        '
        'spdList_spc
        '
        Me.spdList_spc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList_spc.DataSource = Nothing
        Me.spdList_spc.Location = New System.Drawing.Point(215, 153)
        Me.spdList_spc.Name = "spdList_spc"
        Me.spdList_spc.OcxState = CType(resources.GetObject("spdList_spc.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList_spc.Size = New System.Drawing.Size(203, 674)
        Me.spdList_spc.TabIndex = 7
        Me.spdList_spc.Visible = False
        '
        'spdCdList
        '
        Me.spdCdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdCdList.DataSource = Nothing
        Me.spdCdList.Location = New System.Drawing.Point(2, 153)
        Me.spdCdList.Name = "spdCdList"
        Me.spdCdList.OcxState = CType(resources.GetObject("spdCdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCdList.Size = New System.Drawing.Size(416, 733)
        Me.spdCdList.TabIndex = 6
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FGF11
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1205, 925)
        Me.Controls.Add(Me.pnlTop)
        Me.KeyPreview = True
        Me.Name = "FGF11"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "[08] 검사"
        Me.pnlTop.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.pnlBotton.ResumeLayout(False)
        Me.tclTest.ResumeLayout(False)
        Me.tpgTest1.ResumeLayout(False)
        Me.tpgTest1.PerformLayout()
        Me.grpTInfo1.ResumeLayout(False)
        Me.grpTInfo1.PerformLayout()
        Me.pnlOrdCont.ResumeLayout(False)
        CType(Me.spdOrdCont, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdRef, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTestCd.ResumeLayout(False)
        Me.grpTestCd.PerformLayout()
        Me.tpgTest2.ResumeLayout(False)
        Me.grpTInfo2.ResumeLayout(False)
        Me.grpTInfo2.PerformLayout()
        CType(Me.spdAgeRef, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlRstGbn.ResumeLayout(False)
        Me.tpgTest3.ResumeLayout(False)
        Me.tpgTest3.PerformLayout()
        Me.grpDTest.ResumeLayout(False)
        Me.grpDTest.PerformLayout()
        CType(Me.spdDTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRTest.ResumeLayout(False)
        CType(Me.spdRTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdList_spc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnDescRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDescRef.Click
        If txtDescRef.Text = "" Then
            txtDescRef.Text = lblDescRef.Text
        End If

        txtDescRef.Visible = True
        btnDescRefExit.Visible = True
        txtDescRef.Focus()
    End Sub

    Private Sub btnDescRefExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDescRefExit.Click
        txtDescRef.Visible = False
        btnDescRefExit.Visible = False
    End Sub

    Private Sub btnDTDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDTDel.Click, btnRTDel.Click
        Dim sFn As String = "Private Sub btnDTDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDTDel.Click, btnRTDel.Click"

        Try
            If CType(sender, Windows.Forms.Button).Name.StartsWith("btnD") Then
                sbDelCheckedRow(spdDTest, 1)
            ElseIf CType(sender, Windows.Forms.Button).Name.StartsWith("btnR") Then
                sbDelCheckedRow(spdRTest, 1)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnExeDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExeDay.Click
        chkExeDay1.Checked = True
        chkExeDay2.Checked = True
        chkExeDay3.Checked = True
        chkExeDay4.Checked = True
        chkExeDay5.Checked = True
        chkExeDay6.Checked = True
        chkExeDay7.Checked = True
    End Sub

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String
        Dim TestCd As String
        Dim SpcCd As String

        If Me.txtTestCd.Text = "" Then Return
        If Me.txtSpcCd.Text = "" Then Return

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "검사코드 : " & txtTestCd.Text & ", "
            sMsg &= "검체코드 : " & txtSpcCd.Text & Space(10) & vbCrLf
            sMsg &= "검사명   : " & txtTNm.Text & vbCrLf & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            TestCd = txtTestCd.Text
            SpcCd = txtSpcCd.Text

            'If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Return
            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mo_DAF.TransTestInfo_UE(txtTestCd.Text, txtSpcCd.Text, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, sUeDate + sUeTime, USER_INFO.USRID) Then
                MsgBox("해당 검사정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

                sbInitialize()
                sbDeleteCdList(TestCd, SpcCd)
            Else
                MsgBox("사용종료에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub cboAlertGbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAlertGbn.SelectedIndexChanged
        Select Case cboAlertGbn.SelectedIndex
            Case -1, 0, 5
                lblAlertL.Enabled = False
                txtAlertL.Text = ""
                txtAlertL.Enabled = False
                lblAlertH.Enabled = False
                txtAlertH.Text = ""
                txtAlertH.Enabled = False
            Case 1, 4, 6
                lblAlertL.Enabled = True
                txtAlertL.Enabled = True
                lblAlertH.Enabled = False
                txtAlertH.Text = ""
                txtAlertH.Enabled = False
            Case 2, 7
                lblAlertL.Enabled = False
                txtAlertL.Text = ""
                txtAlertL.Enabled = False
                lblAlertH.Enabled = True
                txtAlertH.Enabled = True
            Case 3, 8
                lblAlertL.Enabled = True
                txtAlertL.Enabled = True
                lblAlertH.Enabled = True
                txtAlertH.Enabled = True

        End Select
    End Sub

    Private Sub cboALimitGbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboALimitGbn.SelectedIndexChanged
        Select Case cboALimitGbn.SelectedIndex
            Case -1, 0
                lblALimitL.Enabled = False
                txtALimitL.Text = ""
                txtALimitL.Enabled = False
                cboALimitLS.SelectedIndex = -1
                cboALimitLS.Enabled = False
                lblALimitH.Enabled = False
                txtALimitH.Text = ""
                txtALimitH.Enabled = False
                cboALimitHS.SelectedIndex = -1
                cboALimitHS.Enabled = False
            Case 1
                lblALimitL.Enabled = True
                txtALimitL.Enabled = True
                cboALimitLS.Enabled = True
                lblALimitH.Enabled = False
                txtALimitH.Text = ""
                txtALimitH.Enabled = False
                cboALimitHS.SelectedIndex = -1
                cboALimitHS.Enabled = False
            Case 2
                lblALimitL.Enabled = False
                txtALimitL.Text = ""
                txtALimitL.Enabled = False
                cboALimitLS.SelectedIndex = -1
                cboALimitLS.Enabled = False
                lblALimitH.Enabled = True
                txtALimitH.Enabled = True
                cboALimitHS.Enabled = True
            Case 3
                lblALimitL.Enabled = True
                txtALimitL.Enabled = True
                cboALimitLS.Enabled = True
                lblALimitH.Enabled = True
                txtALimitH.Enabled = True
                cboALimitHS.Enabled = True
        End Select
    End Sub

    Private Sub cboCriticalGbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCriticalGbn.SelectedIndexChanged
        Select Case cboCriticalGbn.SelectedIndex
            Case -1, 0
                lblCriticalL.Enabled = False
                txtCriticalL.Text = ""
                txtCriticalL.Enabled = False
                lblCriticalH.Enabled = False
                txtCriticalH.Text = ""
                txtCriticalH.Enabled = False
            Case 1, 4
                lblCriticalL.Enabled = True
                txtCriticalL.Enabled = True
                lblCriticalH.Enabled = False
                txtCriticalH.Text = ""
                txtCriticalH.Enabled = False
            Case 2, 5
                lblCriticalL.Enabled = False
                txtCriticalL.Text = ""
                txtCriticalL.Enabled = False
                lblCriticalH.Enabled = True
                txtCriticalH.Enabled = True
            Case 3, 6
                lblCriticalL.Enabled = True
                txtCriticalL.Enabled = True
                lblCriticalH.Enabled = True
                txtCriticalH.Enabled = True
        End Select
    End Sub

    Private Sub cboDeltaGbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDeltaGbn.SelectedIndexChanged
        Select Case cboDeltaGbn.SelectedIndex
            Case -1, 0
                lblDeltaDay.Enabled = False
                txtDeltaDay.Text = ""
                txtDeltaDay.Enabled = False
                lblDeltaL.Enabled = False
                txtDeltaL.Text = ""
                txtDeltaL.Enabled = False
                lblDeltaH.Enabled = False
                txtDeltaH.Text = ""
                txtDeltaH.Enabled = False
            Case 5
                lblDeltaDay.Enabled = True
                txtDeltaDay.Enabled = True
                lblDeltaL.Enabled = False
                txtDeltaL.Enabled = False
                lblDeltaH.Enabled = True
                txtDeltaH.Enabled = True
            Case Else
                lblDeltaDay.Enabled = True
                txtDeltaDay.Enabled = True
                lblDeltaL.Enabled = True
                txtDeltaL.Enabled = True
                lblDeltaH.Enabled = True
                txtDeltaH.Enabled = True
        End Select
    End Sub

    Private Sub cboOWarningGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOWarningGbn.SelectedIndexChanged
        Select Case cboOWarningGbn.SelectedIndex
            Case -1, 0
                txtOWarning.Enabled = False
            Case Else
                txtOWarning.Enabled = True
        End Select
    End Sub

    Private Sub cboPanicGbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPanicGbn.SelectedIndexChanged
        Select Case cboPanicGbn.SelectedIndex
            Case -1, 0
                lblPanicL.Enabled = False
                txtPanicL.Text = ""
                txtPanicL.Enabled = False
                lblPanicH.Enabled = False
                txtPanicH.Text = ""
                txtPanicH.Enabled = False
            Case 1, 4
                lblPanicL.Enabled = True
                txtPanicL.Enabled = True
                lblPanicH.Enabled = False
                txtPanicH.Text = ""
                txtPanicH.Enabled = False
            Case 2, 5
                lblPanicL.Enabled = False
                txtPanicL.Text = ""
                txtPanicL.Enabled = False
                lblPanicH.Enabled = True
                txtPanicH.Enabled = True
            Case 3, 6
                lblPanicL.Enabled = True
                txtPanicL.Enabled = True
                lblPanicH.Enabled = True
                txtPanicH.Enabled = True
        End Select
    End Sub

    Private Sub cboSpcNmD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSpcNmD.SelectedIndexChanged, cboExLabNmD.SelectedIndexChanged, cboSlipNmD.SelectedIndexChanged, cboBcclsNmd.SelectedIndexChanged, cboTubeNmD.SelectedIndexChanged, cboDSpcNmO.SelectedIndexChanged, cboDSpcNm2.SelectedIndexChanged

        Dim sFn As String = "Private Sub cboSpcNmD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSpcNmD.SelectedIndexChanged, _" + _
                    "cboExLabNmD.SelectedIndexChanged, cboSlipNmD.SelectedIndexChanged, cboTSectNmD.SelectedIndexChanged, cboTubeNmD.SelectedIndexChanged, _" + _
                    "cboDSpcNm1.SelectedIndexChanged, cboDSpcNm2.SelectedIndexChanged"

        If miSelectKey = 1 Then Return

        Try
            Dim ctrl As Windows.Forms.Control
            Dim cbo As Windows.Forms.ComboBox

            cbo = CType(sender, Windows.Forms.ComboBox)

            ctrl = cbo.Parent.GetNextControl(cbo, False)

            miSelectKey = 1
            If cbo.SelectedIndex > -1 Then
                ctrl.Text = cbo.SelectedItem.ToString.Substring(1, cbo.SelectedItem.ToString.IndexOf("]") - 1)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub cboTCdGbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTCdGbn.SelectedIndexChanged
        Dim sFn As String = ""


        If cboTCdGbn.SelectedIndex = -1 Then
            chkReqSub.Visible = False
            chkReqSub.Checked = False
            chkTitleYN.Visible = True
        Else
            If CType(cboTCdGbn.SelectedItem, String).StartsWith("[C]") Then
                chkViwSub.Visible = True
                chkGrpRstYn.Checked = False
                chkReqSub.Visible = True
                chkTitleYN.Visible = False
                chkTitleYN.Checked = False
                chkReqSub.Location = chkTitleYN.Location
                chkGrpRstYn.Enabled = False
            ElseIf CType(cboTCdGbn.SelectedItem, String).StartsWith("[B]") Then
                chkGrpRstYn.Enabled = True
            Else
                chkViwSub.Visible = False
                chkReqSub.Visible = False
                chkReqSub.Checked = False
                chkTitleYN.Visible = True
                chkGrpRstYn.Checked = False
                chkGrpRstYn.Enabled = False
            End If
        End If

        If miSelectKey = 1 Then Return

        Try
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub cboTOrdSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTOrdSlip.SelectedIndexChanged
        Dim sFn As String = ""

        Try
            If cboTOrdSlip.SelectedIndex > -1 Then
                lblTOrdSlip.Text = cboTOrdSlip.SelectedItem.ToString.Substring(1, cboTOrdSlip.SelectedItem.ToString.IndexOf("]") - 1)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub chkAddModeD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAddModeD.CheckedChanged
        Dim sFn As String = "Private Sub chkAddModeD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAddModeD.CheckedChanged"

        If miSelectKey = 1 Then Return

        Try
            If chkAddModeD.Checked Then
                miAddModeKey = 1

                chkAddModeR.Checked = False
                lblAddModeInfo.BringToFront()
                lblAddModeInfo.Visible = True
            Else
                If miAddModeKey = 2 Then Return

                miAddModeKey = 0

                lblAddModeInfo.SendToBack()
                lblAddModeInfo.Visible = False
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            miAddModeKey = 0

            lblAddModeInfo.SendToBack()
            lblAddModeInfo.Visible = False
        End Try
    End Sub

    Private Sub chkAddModeR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAddModeR.CheckedChanged
        Dim sFn As String = "Private Sub chkAddModeR_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAddModeR.CheckedChanged"

        If miSelectKey = 1 Then Return

        Try
            If chkAddModeR.Checked Then
                miAddModeKey = 2

                chkAddModeD.Checked = False
                lblAddModeInfo.BringToFront()
                lblAddModeInfo.Visible = True
            Else
                If miAddModeKey = 1 Then Return

                miAddModeKey = 0

                lblAddModeInfo.SendToBack()
                lblAddModeInfo.Visible = False
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            miAddModeKey = 0

            lblAddModeInfo.SendToBack()
            lblAddModeInfo.Visible = False
        End Try
    End Sub

    Private Sub chkExLabYN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExLabYN.CheckedChanged
        If chkExLabYN.Checked Then
            lblExLabCd.Enabled = True
            txtExLabCd.Enabled = True
            cboExLabNmD.Enabled = True

        Else
            lblExLabCd.Enabled = False
            txtExLabCd.Text = ""
            txtExLabCd.Enabled = False
            cboExLabNmD.SelectedIndex = -1
            cboExLabNmD.Enabled = False

        End If
    End Sub

    Private Sub chkFixRptYN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFixRptYN.CheckedChanged
        If chkFixRptYN.Checked Then
            Me.cboFixRptusr.Enabled = True
        Else
            Me.cboFixRptusr.SelectedIndex = -1
            Me.cboFixRptusr.Enabled = False
        End If
    End Sub

    Private Sub chkRstLen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRstLen.CheckedChanged
        If chkRstLen.Checked Then
            lblRstULen.Enabled = True
            cboRstULen.Enabled = True
            lblRstLLen.Enabled = True
            cboRstLLen.Enabled = True
            lblCutOpt.Enabled = True
            rdoCutOpt1.Enabled = True
            rdoCutOpt2.Enabled = True
            rdoCutOpt3.Enabled = True
        Else
            lblRstULen.Enabled = False
            cboRstULen.SelectedIndex = -1
            cboRstULen.Enabled = False
            lblRstLLen.Enabled = False
            cboRstLLen.SelectedIndex = -1
            cboRstLLen.Enabled = False
            lblCutOpt.Enabled = False
            rdoCutOpt1.Checked = False
            rdoCutOpt1.Enabled = False
            rdoCutOpt2.Checked = False
            rdoCutOpt2.Enabled = False
            rdoCutOpt3.Checked = False
            rdoCutOpt3.Enabled = False
        End If
    End Sub

    Private Sub chkSeqTYN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeqTYN.CheckedChanged
        If chkSeqTYN.Checked Then
            lblSeqTMi.Enabled = True
            txtSeqTMi.Enabled = True
        Else
            lblSeqTMi.Enabled = False
            txtSeqTMi.Text = ""
            txtSeqTMi.Enabled = False
        End If
    End Sub

    Private Sub chkTATYN_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTatYN.CheckedChanged
        If chkTatYN.Checked Then
            lblPRptMi.Enabled = True
            txtPRptMi.Enabled = True
            cboPRptMi.Enabled = True
            lblFRptMi.Enabled = True
            txtFRptMI.Enabled = True
            cboFRptMi.Enabled = True

            txtPErRptMi.Enabled = True
            cboPErRptMi.Enabled = True
            txtFErRptMI.Enabled = True
            cboFErRptMi.Enabled = True

            lblErRptTime.Enabled = True
            lblRptTIME.Enabled = True
            cboRPTITEM.Enabled = True
            cboRPTITEMER.Enabled = True
            txtAlramT.Enabled = True
            txtAlramTEr.Enabled = True
            cboAlramMi.Enabled = True
            cboErAlramMi.Enabled = True
        Else
            lblPRptMi.Enabled = False
            txtPRptMi.Text = ""
            txtPRptMi.Enabled = False
            cboPRptMi.SelectedIndex = -1
            cboPRptMi.Enabled = False
            lblFRptMi.Enabled = False
            txtFRptMI.Text = ""
            txtFRptMI.Enabled = False
            cboFRptMi.SelectedIndex = -1
            cboFRptMi.Enabled = False

            txtPErRptMi.Enabled = False
            cboPErRptMi.Enabled = False
            txtFErRptMI.Enabled = False
            cboFErRptMi.Enabled = False
            cboPErRptMi.SelectedIndex = -1
            cboFErRptMi.SelectedIndex = -1

            lblErRptTime.Enabled = False
            lblRptTIME.Enabled = False
            cboRPTITEM.Enabled = False
            cboRPTITEMER.Enabled = False
            txtAlramT.Enabled = False
            txtAlramTEr.Enabled = False
            cboAlramMi.Enabled = False
            cboErAlramMi.Enabled = False
            cboErAlramMi.SelectedIndex = -1
            cboAlramMi.SelectedIndex = -1
            cboRPTITEMER.SelectedIndex = -1
            cboRPTITEM.SelectedIndex = -1
        End If
    End Sub

    Private Sub dtpUSDay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUSDay.ValueChanged
        If miSelectKey = 1 Then Return
        If Me.txtUSDay.Text.Trim = "" Then Return

        Me.txtUSDay.Text = Format(dtpUSDay.Value, "yyyy-MM-dd").Substring(0, 10)

        If IsNothing(Me.Owner) Then Return

        If Me.rdoWorkOpt2.Checked Then
            sbDisplayCdList_Ref(Me.txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""))
        End If
    End Sub

    Private Sub FGF11_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim sFn As String = "Private Sub FGF01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated"

        Try
            If mbActivated Then Return

            Me.spdRef.MaxRows = 1

            Dim iWtO As Integer = Me.Owner.ClientSize.Width
            Dim iHtO As Integer = Me.Owner.ClientSize.Height

            Dim iWt As Integer = Me.Width
            Dim iHt As Integer = Me.Height

            Dim iWtGap As Integer = iWtO - mcDevFrmBaseWidth
            Dim iHtGap As Integer = iHtO - mcDevFrmBaseHeight

            If iWtO - iWt > 0 Then
                Me.Width = Me.Width + iWtGap
            End If

            If iHtO - iHt > 0 Then
                Me.Height = Me.Height + iHtGap + 15
            End If

            Me.Location = New System.Drawing.Point(Me.Owner.Location.X, Me.Owner.Location.Y + 110)

            Return

        Catch ex As Exception

        Finally
            mbActivated = True

        End Try
    End Sub

    Private Sub FGF11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode

            Case Windows.Forms.Keys.F2

                If btnReg.Visible And btnReg.Enabled Then btnReg_Click(Nothing, Nothing)

            Case Windows.Forms.Keys.F6
                btnClear_Click(Nothing, Nothing)

                '< add freety 2007/05/03 : 검색기능 추가
            Case Windows.Forms.Keys.Delete
                Me.txtFieldVal.Text = ""
                '>
            Case Windows.Forms.Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub lblTOrdSlip_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTOrdSlip.TextChanged
        Dim sFn As String = ""

        If miSelectKey = 1 Then Return
        If lblTOrdSlip.Text = "" Then Return

        Try
            Dim iCurIndex As Integer = -1

            For i As Integer = 0 To cboTOrdSlip.Items.Count - 1
                If cboTOrdSlip.Items.Item(i).ToString.StartsWith("[" + lblTOrdSlip.Text + "]") = True Then
                    iCurIndex = i

                    Exit For
                End If
            Next

            miSelectKey = 1
            cboTOrdSlip.SelectedIndex = iCurIndex

            If iCurIndex = -1 Then
                errpd.SetIconAlignment(cboTOrdSlip, Windows.Forms.ErrorIconAlignment.TopLeft)
                errpd.SetError(cboTOrdSlip, "존재하지 않는 코드입니다. 확인하여 주십시요!!")
                errpd.SetError(Me.btnReg, "존재하지 않는 코드입니다. 확인하여 주십시요!!")
            Else
                errpd.SetError(cboTOrdSlip, "")
                errpd.SetError(Me.btnReg, "")
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub rbnJudgType0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoJudgType0.CheckedChanged, rdoJudgType1.CheckedChanged, rdoJudgType2.CheckedChanged, rdoJudgType3.CheckedChanged
        If rdoJudgType0.Checked Or rdoJudgType1.Checked Then
            lblUJudgLT1.Enabled = False
            txtUJudgLT1.Text = ""
            txtUJudgLT1.Enabled = False
            lblJudgType1.Enabled = False
            cboJudgType1.SelectedIndex = -1
            cboJudgType1.Enabled = False

            lblUJudgLT2.Enabled = False
            txtUJudgLT2.Text = ""
            txtUJudgLT2.Enabled = False
            lblJudgType2.Enabled = False
            cboJudgType2.SelectedIndex = -1
            cboJudgType2.Enabled = False

            lblUJudgLT3.Enabled = False
            txtUJudgLT3.Text = ""
            txtUJudgLT3.Enabled = False
            lblJudgType3.Enabled = False
            cboJudgType3.SelectedIndex = -1
            cboJudgType3.Enabled = False
        ElseIf rdoJudgType2.Checked Then
            lblUJudgLT1.Enabled = True
            txtUJudgLT1.Enabled = True
            lblJudgType1.Enabled = True
            cboJudgType1.Enabled = True

            lblUJudgLT2.Enabled = True
            txtUJudgLT2.Enabled = True
            lblJudgType2.Enabled = True
            cboJudgType2.Enabled = True

            lblUJudgLT3.Enabled = False
            txtUJudgLT3.Text = ""
            txtUJudgLT3.Enabled = False
            lblJudgType3.Enabled = False
            cboJudgType3.SelectedIndex = -1
            cboJudgType3.Enabled = False
        ElseIf rdoJudgType3.Checked Then
            lblUJudgLT1.Enabled = True
            txtUJudgLT1.Enabled = True
            lblJudgType1.Enabled = True
            cboJudgType1.Enabled = True

            lblUJudgLT2.Enabled = True
            txtUJudgLT2.Enabled = True
            lblJudgType2.Enabled = True
            cboJudgType2.Enabled = True

            lblUJudgLT3.Enabled = True
            txtUJudgLT3.Enabled = True
            lblJudgType3.Enabled = True
            cboJudgType3.Enabled = True
        End If
    End Sub

    Private Sub rbnRefGbn0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoRefGbn0.CheckedChanged, rdoRefGbn1.CheckedChanged, rdoRefGbn2.CheckedChanged
        If rdoRefGbn0.Checked Then
            With spdAgeRef
                .MaxRows = 0
                .MaxRows = mcAgeRefMaxRow
                .Enabled = False
            End With
        Else
            spdAgeRef.Enabled = True
        End If
    End Sub

    Private Sub txtSpcCd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSpcCd.Validating, txtSlipCd.Validating, txtBcclsCd.Validating, txtTubeCd.Validating, txtDSpcCdO.Validating, txtDSpcCd2.Validating, txtExLabCd.Validating
        Dim sFn As String = "Private Sub txtSpcCd_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSpcCd.Validating, _" + _
                    "txtSlipCd.Validating, txtTSectCd.Validating, txtTubeCd.Validating, txtDSpcCd1.Validating, txtDSpcCd2.Validating"

        If miSelectKey = 1 Then Return

        Try
            Dim ctrl As Windows.Forms.TextBox
            Dim cbo As Windows.Forms.ComboBox
            Dim iCurIndex As Integer = -1

            ctrl = CType(sender, Windows.Forms.TextBox)

            If ctrl.Text = "" Then Return

            cbo = CType(ctrl.Parent.GetNextControl(ctrl, True), Windows.Forms.ComboBox)

            For i As Integer = 0 To cbo.Items.Count - 1
                If cbo.Items.Item(i).ToString.StartsWith("[" + ctrl.Text + "]") = True Then
                    iCurIndex = i

                    Exit For
                End If
            Next

            miSelectKey = 1
            cbo.SelectedIndex = iCurIndex

            If iCurIndex = -1 Then
                errpd.SetIconAlignment(ctrl, Windows.Forms.ErrorIconAlignment.TopRight)
                errpd.SetError(ctrl, "존재하지 않는 코드입니다. 확인하여 주십시요!!")
                errpd.SetError(Me.btnReg, "존재하지 않는 코드입니다. 확인하여 주십시요!!")
                e.Cancel = True
            Else
                errpd.SetError(ctrl, "")
                errpd.SetError(Me.btnReg, "")
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub txtTNm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTNm.TextChanged
        Dim sFn As String = ""

        If miSelectKey = 1 Then Return

        Try
            If txtTNm.Text.Trim = "" Then
                errpd.SetIconAlignment(txtTNm, Windows.Forms.ErrorIconAlignment.TopLeft)
                errpd.SetError(txtTNm, "필수항목입니다. 입력하여 주십시요!!")
                errpd.SetError(Me.btnReg, "필수항목입니다. 입력하여 주십시요!!")
            Else
                errpd.SetError(txtTNm, "")
                errpd.SetError(Me.btnReg, "")
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub txtTNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTNm.Validating
        Dim sFn As String = ""

        If miSelectKey = 1 Then Return

        Try
            If txtTNmS.Text.Trim = "" Then
                If txtTNm.Text.Length > txtTNmS.MaxLength Then
                    txtTNmS.Text = txtTNm.Text.Substring(0, txtTNmS.MaxLength)
                Else
                    txtTNmS.Text = txtTNm.Text
                End If
            End If

            If txtTNmD.Text.Trim = "" Then
                If txtTNm.Text.Length > txtTNmD.MaxLength Then
                    txtTNmD.Text = txtTNm.Text.Substring(0, txtTNmD.MaxLength)
                Else
                    txtTNmD.Text = txtTNm.Text
                End If
            End If

            If txtTNmP.Text.Trim = "" Then
                If txtTNm.Text.Length > txtTNmP.MaxLength Then
                    txtTNmP.Text = txtTNm.Text.Substring(0, txtTNmP.MaxLength)
                Else
                    txtTNmP.Text = txtTNm.Text
                End If
            End If

            If Not errpd.GetError(txtTNm).ToString = "" Then
                e.Cancel = True
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub txtUSDay_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUSDay.TextChanged
        If miSelectKey = 1 Then Return
        If txtUSDay.Text.Trim = "" Then Return
        If Not IsDate(txtUSDay.Text) Then Return
        If IsNothing(Me.Owner) Then Return
        If Not txtUSDay.Text.Length = txtUSDay.MaxLength Then Return

        If Me.rdoWorkOpt2.Checked Then
            sbDisplayCdList_Ref(txtUSDay.Text.Replace("-", "") & Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""))

            If Not txtUSDT.Text.Trim = "" Then
                If DateDiff(DateInterval.Second, CDate(txtUSDT.Text), CDate(txtUSDay.Text & " " & Format(dtpUSTime.Value, "HH:mm:ss"))) <= 0 Then
                    Dim sMsg As String = "시작일시가 시작일시(선택)보다 같거나 이전입니다. 이런 경우에는 신규로 등록하실 수 없습니다!!" & vbCrLf
                    sMsg &= "시작일시를 다시 설정하십시요!!"

                    MsgBox(sMsg)

                    sbSetNewUSDT()
                End If
            End If
        End If
    End Sub

    Private Sub btnGetExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetExcel.Click

        If MsgBox("검사코드 입력?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Excel") = MsgBoxResult.Yes Then sbGetExcel_f60()
        If MsgBox("참고치 입력?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Excel") = MsgBoxResult.Yes Then sbGetExcel_f61()
        If MsgBox("세부검사 입력?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Excel") = MsgBoxResult.Yes Then sbGetExcel_f62()

    End Sub

    Private Sub spdAgeRef_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdAgeRef.Change

        If e.col = spdAgeRef.GetColFromID("AGEYMD") And (rdoRefGbn1.Checked Or rdoRefGbn2.Checked) Then
            With spdAgeRef
                .Row = e.row
                .Col = .GetColFromID("AGEYMD") : Dim strValue As String = .Text

                If strValue.ToLower = "year" And e.row = 1 Then
                    .Col = .GetColFromID("SAGE") : .Text = "0"
                    .Col = .GetColFromID("EAGE") : .Text = "200"
                    .Col = .GetColFromID("SAGES") : .TypeComboBoxCurSel = 0
                    .Col = .GetColFromID("EAGES") : .TypeComboBoxCurSel = 0
                End If

                If rdoRefGbn2.Checked Then
                    .Col = .GetColFromID("REFLMS") : .TypeComboBoxCurSel = 0
                    .Col = .GetColFromID("REFHMS") : .TypeComboBoxCurSel = 0
                    .Col = .GetColFromID("REFLFS") : .TypeComboBoxCurSel = 0
                    .Col = .GetColFromID("REFHFS") : .TypeComboBoxCurSel = 0
                End If

            End With
        End If

    End Sub

    '< yjlee 2009-01-09
    Private Sub spdAgeRef_DblClick(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdAgeRef.DblClick
        Dim sFn As String = "spdAgeRef_DblClick"

        Try
            If e.col > 0 Then Exit Sub
            If e.row < 1 Then Exit Sub

            With spdAgeRef
                If MsgBox(e.row & "행의 정보를 초기화 하시겠습니까?", MsgBoxStyle.OkOnly Or MsgBoxStyle.OkCancel, "초기화") = MsgBoxResult.Ok Then
                    .DeleteRows(e.row, 1)
                    .MaxRows -= 1

                    .MaxRows += 1
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '> yjlee 2009-01-09 

    Private Sub btnReg_dispseql_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg_dispseql.Click, btnReg_dispseqO.Click
        Dim sFn As String = "Handles btnReg_dispseql.ButtonClick"

        Dim frmChild As Windows.Forms.Form
        Dim sDispSeqGbn As String = "L"


        If CType(sender, Windows.Forms.Button).Name = "btnReg_dispseqO" Then sDispSeqGbn = "O"

        frmChild = New FGF11_S01(sDispSeqGbn)

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = System.Windows.Forms.FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()

    End Sub

    Private Sub btnClear_spc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_spc.Click
        Me.txtSelSpc.Text = ""
        Me.txtSelSpc.Tag = ""
    End Sub

    Private Sub btnCdHelp_spc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_spc.Click
        Dim sFn As String = "Handles btnCdHelp_spc.Click"
        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List(Me.dtpUSDay.Text.Replace("-", "") + Me.dtpUSTime.Text.Replace(":", ""), "", "", "", "", "", "")

            objHelp.FormText = "검체정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("spcnmd", "검체명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(Me.btnCdHelp_spc)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_spc.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnCdHelp_spc.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sSpcCds As String = "", sSpcNmds As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sSpccd As String = aryList.Item(ix).ToString.Split("|"c)(1)
                    Dim sSpcnmd As String = aryList.Item(ix).ToString.Split("|"c)(0)

                    If ix > 0 Then
                        sSpcCds += "|" : sSpcNmds += "|"
                    End If

                    sSpcCds += sSpccd : sSpcNmds += sSpcnmd
                Next

                Me.txtSelSpc.Text = sSpcNmds.Replace("|", ",")
                Me.txtSelSpc.Tag = sSpcCds
            Else
                Me.txtSelSpc.Text = ""
                Me.txtSelSpc.Tag = ""
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub chkSpcGbn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSpcGbn.CheckedChanged
        Me.txtSelSpc.Text = ""
        Me.txtSelSpc.Tag = ""

        If chkSpcGbn.Checked Then
            Me.btnCdHelp_spc.Visible = True
            Me.btnClear_spc.Visible = True
            Me.txtSelSpc.Visible = True

            Me.txtSpcCd.Visible = False
            Me.cboSpcNmD.Visible = False
        Else
            Me.btnCdHelp_spc.Visible = False
            Me.btnClear_spc.Visible = False
            Me.txtSelSpc.Visible = False

            Me.txtSpcCd.Visible = True
            Me.cboSpcNmD.Visible = True
        End If
    End Sub

    Private Sub FGF11_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DS_FormDesige.sbInti(Me)

        sbDisplayColumnNm(1)
        sbDisplay_bccls()
        sbDisplay_tordslip()

        Me.cboPSGbn.SelectedIndex = 1

        If Not USER_SKILL.Authority("F01", 9) Then
            Me.rdoWorkOpt1.Checked = False : Me.rdoWorkOpt2.Checked = False
            Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
            Me.btnReg.Enabled = False
        End If

        If USER_INFO.USRLVL <> "S" Then
            Me.btnChgUseDt.Visible = False
        End If


        Me.cboFilter.SelectedIndex = 0
        Me.cboOps.SelectedIndex = 5

    End Sub

    Private Sub cboPSGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPSGbn.SelectedIndexChanged

        If Me.cboPSGbn.Text = "부서" Then
            sbDisplay_part()
        Else
            sbDisplay_slip()
        End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        sbDisplay_Test()

    End Sub

    Private Sub rbnSOpt0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSOpt0.Click, rdoSOpt1.Click

        If rdoSOpt1.Checked = True Then
            Me.btnReg.Text = "수정(F2)"
            Me.rdoWorkOpt2.Enabled = False

            If Me.rdoWorkOpt2.Checked Then Me.rdoWorkOpt1.Checked = True

            Me.btnChgUseDt.Text = "종료일시 수정"
        Else
            Me.btnChgUseDt.Text = "사용일시 수정"
            If Me.rdoWorkOpt2.Enabled = False Then Me.rdoWorkOpt2.Enabled = True
        End If

        If Me.rdoWorkOpt2.Checked = False Then sbUSDT_Disable()
        btnQuery_Click(Nothing, Nothing)

    End Sub

    Private Sub rbnWorkOpt0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoWorkOpt1.CheckedChanged, rdoWorkOpt2.CheckedChanged

        Me.btnReg.Enabled = True

        If Me.rdoWorkOpt1.Checked Then

            Me.btnChgUseDt.Enabled = True
            Me.btnReg.Text = "수정(F2)"
            Me.rdoSOpt1.Enabled = True
            sbUSDT_Disable()

        Else
            Me.btnChgUseDt.Enabled = False
            Me.btnUE.Visible = False
            Me.giClearKey = 1
            Me.sbInitialize()
            Me.giClearKey = 0

            Me.btnReg.Text = "등록(F2)"      '등록시에 sbUSDT_New()를 통해 컨트롤을 Enable시킴
            sbUSDT_New()
            'sbDisplayCdList(msMstGbn)
        End If

    End Sub

    Private Sub spdCdList_BeforeUserSort(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BeforeUserSortEvent) Handles spdCdList.BeforeUserSort
        sbDisplayColumnNm(e.col)
    End Sub

    Private Sub spdCdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdCdList.ClickEvent
        Dim sFn As String = "Private Sub spdCdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdCdList.ClickEvent"

        If e.row = 0 Then
            sbDisplayColumnNm(e.col)
            Return
        End If

        If Me.chkAddModeD.Checked Or Me.chkAddModeR.Checked Then Return
        If e.row < 1 Then Return

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            sbDisplayCdCurRow(e.row)
            sbDisplayChgUseDt(e.row)


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub spdCdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdCdList.DblClick
        Dim sFn As String = "Private Sub spdCdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdCdList.DblClick"

        Try
            If Me.chkNotSpc.Checked Then Return

            If Me.chkAddModeD.Checked Or Me.chkAddModeR.Checked Then
                sbAddTest(e.row)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub spdCdList_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles spdCdList.LeaveRow
        Dim sFn As String = "Private Sub spdCdList_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles spdCdList.LeaveRow"

        If Me.chkAddModeD.Checked Or Me.chkAddModeR.Checked Then Return
        If e.newRow < 1 Then Return
        If e.newRow = e.row Then Return

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            miLeaveRow = 1

            sbDisplayCdCurRow(e.newRow)
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub spdCdList_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdCdList.RightClick
        With spdCdList
            If .MaxRows < 1 Then Return
            If e.row < 1 Then Return
            If Me.chkNotSpc.Checked Then Return

            sbJudgeAddTest(e.row)

        End With
    End Sub

    Private Sub txtFieldVal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFieldVal.GotFocus
        Dim sFn As String = ""

        Try
            If Me.lblFieldNm.Text.Trim().EndsWith("검사코드") Then
                Me.txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Upper
            Else
                Me.txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Normal
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtFieldVal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFieldVal.TextChanged
        Try
            If Me.spdCdList.MaxRows < 1 Then Return

            sbFindList(Me.txtFieldVal.Text)

        Catch ex As Exception

        End Try

    End Sub

    '< yjlee 2010-06-15
    Private Sub txtFieldVal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFieldVal.Click
        Dim sFn As String = ""

        Try
            If Me.lblFieldNm.Text.Trim().EndsWith("검사코드") Then
                Me.txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Upper
            Else
                Me.txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Normal
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "sbReg_"

        Try
            If fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사코드 : " + Me.txtTestCd.Text + ", "
            sMsg += "검체코드 : " + Me.txtSpcCd.Text + Space(10) + vbCrLf
            sMsg += "검사명   : " + Me.txtTNm.Text + vbCrLf + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf Me.rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Test()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        btnQuery_Click(Nothing, Nothing)
                        'sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Critical)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Critical)
                    End If
                End If
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.btnUE.Visible = False
        Me.giClearKey = 1
        Me.sbInitialize()
        Me.giClearKey = 0
        Me.spdRef.MaxRows = 0
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sFn As String = " Handles btnExcel.Click"
        Try
            With spdCdList
                .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

                If .ExportToExcel("code.xls", "code list", "") Then
                    Process.Start("code.xls")
                End If
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub btnChgUseDt_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChgUseDt.Click
        Dim sFn As String = "Handles btnChgUseDt.Click"

        Try

            Dim a_objArgs(0) As Object

            a_objArgs(0) = Me.btnChgUseDt.Tag

            CallByName(Me, "sbEditUseDt", CallType.Method, a_objArgs)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub chkNotSpc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNotSpc.CheckedChanged

        If chkNotSpc.Checked Then
            Me.rdoSort_spc.Checked = False
            Me.rdoSort_spc.Enabled = False

            Me.spdCdList.Width = Me.tclTest.Left - Me.spdList_spc.Width - 5
            Me.spdList_spc.Height = Me.spdCdList.Height
            Me.spdList_spc.Visible = True

        Else
            Me.spdCdList.Width = Me.tclTest.Left - 5
            Me.spdList_spc.Visible = False

            Me.rdoSort_spc.Enabled = True
        End If

        sbColHidden_spdcdlist()
        btnQuery_Click(Nothing, Nothing)

    End Sub

    Private Sub spdList_spc_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList_spc.ClickEvent

        If e.row < 1 Then Return

        With spdList_spc
            .Row = e.row
            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
            .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
            .Col = .GetColFromID("usdt") : Dim sUsDt As String = .Text
            .Col = .GetColFromID("uedt") : Dim sUeDt As String = .Text

            sbDisplayCdDetail_spc(sTestCd, sSpcCd, sUsDt, sUeDt)

        End With

    End Sub

    Private Sub chkGrpRstYn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGrpRstYn.CheckedChanged

        With Me.spdDTest
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("grprstyn") : .Text = IIf(chkGrpRstYn.Checked, "1", "").ToString
            Next
        End With

    End Sub

    Private Sub btnOrdContExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrdContExit.Click
        Me.pnlOrdCont.Visible = False
    End Sub

    Private Sub chkOReqItem4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOReqItem4.CheckedChanged
        'If Me.chkOReqItem4.Checked Then
        '    Me.btnOrdContView.Visible = True
        '    Me.pnlOrdCont.Visible = True
        '    Me.spdOrdCont.MaxRows = 1

        'Else
        '    Me.btnOrdContView.Visible = False
        '    Me.pnlOrdCont.Visible = False
        '    Me.spdOrdCont.MaxRows = 0
        'End If
    End Sub

    Private Sub btnOrdContAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrdContAdd.Click

        With Me.spdOrdCont
            .MaxRows += 1
        End With

    End Sub

    Private Sub btnOrdContDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrdContDel.Click
        With Me.spdOrdCont
            If .MaxRows < 1 Then Return
            .MaxRows -= 1
        End With
    End Sub

    Private Sub btnOrdContView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrdContView.Click
        Me.pnlOrdCont.Visible = True
    End Sub

    Private Sub spdOrdCont_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdOrdCont.KeyDownEvent
        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        With Me.spdOrdCont
            .MaxRows += 1
        End With

    End Sub

    Private Sub txtFilter_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilter.GotFocus
        Dim sFn As String = ""

        Try
            If Me.cboFilter.Text.EndsWith("코드") Then
                Me.txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Upper
            Else
                Me.txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Normal
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Me.btnQuery_Click(Nothing, Nothing)

    End Sub

    Private Sub cboFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFilter.SelectedIndexChanged
        If Me.cboFilter.Text.EndsWith("코드") Then
            Me.txtFilter.CharacterCasing = Windows.Forms.CharacterCasing.Upper
        Else
            Me.txtFilter.CharacterCasing = Windows.Forms.CharacterCasing.Normal
        End If
    End Sub

    Private Sub spdAgeRef_KeyDownEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdAgeRef.KeyDownEvent
        If e.keyCode = Windows.Forms.Keys.F2.GetHashCode Then
            btnReg.Focus()
            If btnReg.Visible And btnReg.Enabled Then btnReg_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub cboJudgType1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboJudgType1.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboJudgType2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboJudgType2.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboJudgType3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboJudgType3.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboPanicGbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboPanicGbn.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboCriticalGbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboCriticalGbn.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboAlertGbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboAlertGbn.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboDeltaGbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboDeltaGbn.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboALimitGbn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboALimitGbn.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboALimitLS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboALimitLS.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub cboALimitHS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboALimitHS.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")
    End Sub

    
    Private Sub btnRefExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefExcel.Click

        Try

        
            Dim iNum As Integer = 0

            With spdCdList

                spdRef.MaxRows = spdCdList.MaxRows

                For ix As Integer = 1 To .MaxRows

                    Dim rsTestcd As String = Ctrl.Get_Code(spdCdList, "testcd", ix)
                    Dim rsSpccd As String = Ctrl.Get_Code(spdCdList, "spccd", ix)


                    Dim dt As DataTable = mo_DAF.GetRefExcel(rsTestcd, rsSpccd)

                    If dt.Rows.Count <> 0 Then



                        With spdRef
                            '.MaxRows = dt.Rows.Count

                            For ix2 As Integer = 0 To dt.Rows.Count - 1
                                If iNum = 0 Then
                                    .Row = 0
                                End If
                                .Row += 1
                                .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix2).Item("testcd").ToString
                                .Col = .GetColFromID("tnm") : .Text = dt.Rows(ix2).Item("tnm").ToString
                                .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix2).Item("spccd").ToString
                                .Col = .GetColFromID("spcnm") : .Text = dt.Rows(ix2).Item("spcnm").ToString
                                .Col = .GetColFromID("ageymd") : .Text = dt.Rows(ix2).Item("ageymd").ToString
                                .Col = .GetColFromID("years") : .Text = dt.Rows(ix2).Item("years").ToString
                                .Col = .GetColFromID("man") : .Text = dt.Rows(ix2).Item("man").ToString
                                .Col = .GetColFromID("woman") : .Text = dt.Rows(ix2).Item("woman").ToString
                                .Col = .GetColFromID("reflt") : .Text = dt.Rows(ix2).Item("reflt").ToString

                                .MaxRows += 1

                                iNum += 1
                            Next

                        End With
                    End If

                Next

                

            End With

            Dim sBuf As String = ""

            With spdRef
                .ReDraw = False

                .MaxRows = .MaxRows + 1
                .InsertRows(1, 1)

                For i As Integer = 1 To .MaxCols
                    .Col = i : .Row = 0 : sBuf = .Text
                    .Col = i : .Row = 1 : .Text = sBuf
                Next

                If .ExportToExcel("참고치리스트.xls", "참고치리스트", "") Then
                    Process.Start("참고치리스트.xls")
                End If

                .DeleteRows(1, 1)
                .MaxRows -= 1

                .ReDraw = True
            End With


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

        '  End With
    End Sub

    Private Sub BtnTestChg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTestChg.Click
        Dim sFn As String = "Handles btnReg_dispseql.ButtonClick"

        If txtTestCd.Text = "" Then Return

        Dim frmChild As Windows.Forms.Form
        Dim sDispSeqGbn As String = "L"

        If CType(sender, Windows.Forms.Button).Name = "btnReg_dispseqO" Then sDispSeqGbn = "O"


        'frmChild = New FGF11_S02(txtTestCd.Text, txtSpcCd.Text)
        frmChild = New FGF11_S04(txtTestCd.Text, txtSpcCd.Text)

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = System.Windows.Forms.FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()
    End Sub

End Class
