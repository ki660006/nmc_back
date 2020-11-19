Imports COMMON.CommFN
Imports COMMON.commlogin.login


Public Class FGF01
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGF01.vb, Class : FGF01" + vbTab

    '< add freety 2007/07/27 : Active Size 조정
    Private Const mcDevFrmBaseWidth As Integer = 1024
    Private Const mcDevFrmBaseHeight As Integer = 768
    Private Const mcDevFrmMinWidth As Integer = 112
    Private Const mcDevMainPanelHeight As Integer = 58 '40

    Private m_dt_CdList As DataTable
    Private m_dr_CdList As DataRow()
    Private m_fpopup_f As FPOPUPFT

    '일반검사
    Private Const mcFDF00 As String = "00"
    Private Const mcFDF01 As String = "01"
    Private Const mcFDF02 As String = "02"
    Private Const mcFDF03 As String = "03"
    Private Const mcFDF04 As String = "04"
    Private Const mcFDF05 As String = "05"
    Private Const mcFDF06 As String = "06"
    Private Const mcFDF07 As String = "07"
    'Private Const mcFDF08 As String = "08"
    Private Const mcFDF09 As String = "09"
    Private Const mcFDF10 As String = "10"
    Private Const mcFDF11 As String = "11"
    Private Const mcFDF12 As String = "12"
    Private Const mcFDF13 As String = "13"
    'Private Const mcFDF14 As String = "14"
    Private Const mcFDF40 As String = "40"
    Private Const mcFDF41 As String = "41"
    Private Const mcFDF43 As String = "43"
    Private Const mcFDF44 As String = "44"

    '미생물
    Private Const mcFDF15 As String = "15"
    Private Const mcFDF16 As String = "16"
    Private Const mcFDF17 As String = "17"
    Private Const mcFDF18 As String = "18"
    Private Const mcFDF52 As String = "52"
    Private Const mcFDF19 As String = "19"

    '특수검사
    Private Const mcFDF20 As String = "20"
    Private Const mcFDF21 As String = "21"

    '혈액은행
    Private Const mcFDF30 As String = "30"
    Private Const mcFDF31 As String = "31"
    Private Const mcFDF32 As String = "32"
    Private Const mcFDF33 As String = "33"
    Private Const mcFDF34 As String = "34"
    Private Const mcFDF35 As String = "35"

    '채혈/접수 취소
    Private Const mcFDF42 As String = "42"
    Private Const mcFDF47 As String = "47"

    'KEYPAD 설정 
    Private Const mcFDF45 As String = "45"

    '성분명 설정 
    Private Const mcFDF46 As String = "46"

    '종합검증 소견 등록
    Private Const mcFDF48 As String = "48"
    Private Const mcFDF49 As String = "49"
    Private Const mcFDF50 As String = "50"

    'Aleart Rule
    Private Const mcFDF51 As String = "51"

    '병원체검사 <<< 20170601 수정 
    Private Const mcFDF53 As String = "53"

    '검사의뢰지침서 관리 추가
    Private Const mcFDF54 As String = "54"

    '### 기능정의상수
    Private Const mc_Add_Or_Edit_Of_User As Integer = 0 + 1
    Private Const mc_Add_Or_Edit_Of_Sect As Integer = 1 + 1
    Private Const mc_Add_Or_Edit_Of_Slip As Integer = 2 + 1
    Private Const mc_Add_Or_Edit_Of_Spc As Integer = 3 + 1
    Private Const mc_Add_Or_Edit_Of_SpcGrp As Integer = 4 + 1
    Private Const mc_Add_Or_Edit_Of_WkGrp As Integer = 5 + 1
    Private Const mc_Add_Or_Edit_Of_Tube As Integer = 6 + 1
    Private Const mc_Add_Or_Edit_Of_ExLab As Integer = 7 + 1
    Private Const mc_Add_Or_Edit_Of_Test As Integer = 8 + 1
    Private Const mc_Add_Or_Edit_Of_TGrp As Integer = 9 + 1
    Private Const mc_Add_Or_Edit_Of_RstCd As Integer = 10 + 1
    Private Const mc_Add_Or_Edit_Of_Cmt As Integer = 11 + 1
    Private Const mc_Add_Or_Edit_Of_Calc As Integer = 12 + 1
    Private Const mc_Add_Or_Edit_Of_Eq As Integer = 13 + 1
    Private Const mc_Add_Or_Edit_Of_Tla As Integer = 14 + 1
    Private Const mc_Add_Or_Edit_Of_OSlip As Integer = 40 + 1
    Private Const mc_Add_Or_Edit_Of_KSRack As Integer = 41 + 1

    Private Const mc_Add_Or_Edit_Of_SpTest As Integer = 20 + 1
    Private Const mc_Add_Or_Edit_Of_SpWord As Integer = 21 + 1

    Private Const mc_Add_Or_Edit_Of_Bacgen As Integer = 15 + 1
    Private Const mc_Add_Or_Edit_Of_Bac As Integer = 16 + 1
    Private Const mc_Add_Or_Edit_Of_Anti As Integer = 17 + 1
    Private Const mc_Add_Or_Edit_Of_BacgenAnti As Integer = 18 + 1
    Private Const mc_Add_Or_Edit_Of_Cult As Integer = 52 + 1
    Private Const mc_Add_Or_Edit_Of_BacRst As Integer = 19 + 1

    Private Const mc_Add_Or_Edit_Of_ComCd As Integer = 30 + 1
    Private Const mc_Add_Or_Edit_Of_FtCd As Integer = 31 + 1
    Private Const mc_Add_Or_Edit_Of_JobCd As Integer = 32 + 1
    Private Const mc_Add_Or_Edit_Of_DisCd As Integer = 33 + 1
    Private Const mc_Add_Or_Edit_Of_RtnCd As Integer = 34 + 1
    Private Const mc_Add_Or_Edit_Of_BDTest As Integer = 35 + 1

    Private Const mc_Add_Or_Edit_Of_CollTk As Integer = 42 + 1
    Private Const mc_Add_Or_Edit_Of_CalcRst As Integer = 43 + 1
    Private Const mc_Add_Or_Edit_Of_CalcCmt As Integer = 44 + 1
    Private Const mc_Add_Or_Edit_Of_KeyPad As Integer = 45 + 1
    Private Const mc_Add_Or_Edit_Of_DComCd As Integer = 46 + 1
    Private Const mc_Add_Or_Edit_Of_AbnRstCd As Integer = 47 + 1

    Private Const mc_Add_Or_Edit_Of_VCmt As Integer = 48 + 1
    Private Const mc_Add_Or_Edit_Of_VCmt_Tcls As Integer = 49 + 1
    Private Const mc_Add_Or_Edit_Of_VCmt_Doctor As Integer = 50 + 1

    Private Const mc_Add_Or_Edit_Of_Alert_Rule As Integer = 51 + 1

    Private msMstGbn As String = ""
    Private msNewUSDT As String = ""
    Private msUserID As String = USER_INFO.USRID
    Private miWidth As Integer = 0
    Private mfrmCur As Windows.Forms.Form

    Private miFirstWidth_pnlLeft As Integer = Nothing
    Private miParentGapX As Integer = Nothing
    Private miParentGapY As Integer = Nothing

    Private miMDIChild As Integer = 0           'OwnedForm = 0, MDIChildForm = 1
    Private miLeaveRow As Integer = 0
    Private miCurRow As Integer = 0           '왼쪽 스프레드에서 현재 선택된(클릭) 로우

    Private mbActivated As Boolean = False
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnChgUseDt As CButtonLib.CButton
    Friend WithEvents txtFieldVal As System.Windows.Forms.TextBox
    Friend WithEvents lblGuide3 As System.Windows.Forms.Label
    Friend WithEvents lblFieldNm As System.Windows.Forms.Label
    Friend WithEvents btnFilter As System.Windows.Forms.Button
    Friend WithEvents lblFil As System.Windows.Forms.Label

    Public giAddModeKey As Integer = 0        'giAddModeKey = 0, 1, 2

    Private Sub sbDisplayInit_Filter()
        Dim sFn As String = "sbDisplayInit_Filter"

        Try
            Me.lblFilter.Text = "" 

            m_dt_CdList = Nothing
            m_dr_CdList = Nothing

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Return_Filter(ByVal rsCont As String, ByVal rsSyntax As String)
    
        Me.lblFilter.Text = rsSyntax
        'Me.lblFilter.Text = rsCont
        'Me.lblFilter.AccessibleName = rsSyntax
    End Sub

    Private Sub sbLoad_Popup_Filter()
        Dim sFn As String = "sbLoad_Popup_Filter"

        Try
            Dim al_columns As New ArrayList

            'al_columns.Add("파트".PadRight(100, " ") + "[SECTCD]")
            'al_columns.Add("검사파트".PadRight(100, " ") + "[TSECTCD]")
            'al_columns.Add("처방슬립".PadRight(100, " ") + "[TORDSLIP]")
            'al_columns.Add("검사SLIP".PadRight(100, " ") + "[SLIPCD]")
            'al_columns.Add("검사명".PadRight(100, " ") + "[TNMD]")
            'al_columns.Add("처방코드".PadRight(100, " ") + "[TORDCD]")

            With spdCdList
                For intCol As Integer = 1 To spdCdList.MaxRows
                    Dim strTitle As String = ""
                    Dim strField As String = ""

                    .Row = 0
                    .Col = intCol : strTitle = .Text
                    .Col = intCol : strField = .ColID

                    If .ColHidden = False Then
                        al_columns.Add(strTitle.PadRight(100, " "c) + "[" + strField + "]")
                    End If
                Next
            End With

            If Not m_fpopup_f Is Nothing Then
                m_fpopup_f.Close()
                RemoveHandler m_fpopup_f.ReturnPopupFilter, AddressOf sbDisplay_Return_Filter
            End If

            m_fpopup_f = New FPOPUPFT

            With m_fpopup_f
                .Columns = al_columns
                .DisplayInit()
            End With

            m_fpopup_f.TopMost = True
            m_fpopup_f.Hide()

            AddHandler m_fpopup_f.ReturnPopupFilter, AddressOf sbDisplay_Return_Filter

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
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

    Public Sub sbRefreshCdList()
        Dim sFn As String = "Public Sub sbRefreshCdList"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            sbDisplayCdList(msMstGbn)

            Me.Cursor = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            sbDisplayColumnNm(1)

        End Try
    End Sub

    Private Sub sbChgUseDt()
        Dim sFn As String = "Sub sbChgUseDt()"

        Try
            If IsNothing(mfrmCur) Then Return

            Dim a_objArgs(0) As Object

            a_objArgs(0) = Me.btnChgUseDt.Tag

            CallByName(mfrmCur, "sbEditUseDt", CallType.Method, a_objArgs)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbBlockSpreadClickedRow(ByVal aspd As AxFPSpreadADO.AxfpSpread, ByVal aiCol As Integer, ByVal aiRow As Integer)
        Dim sFn As String = "Private Sub sbBlockSpreadClickedRow(ByVal aspd As AxFPSpreadADO.AxfpSpread, ByVal aiCol As Integer, ByVal aiRow As Integer)"

        Try
            With aspd
                .Col = 0 : .Col2 = .MaxCols : .Row = aiRow : .Row2 = aiRow
                .BlockMode = True
                .Action = FPSpreadADO.ActionConstants.ActionSelectBlock
                .BlockMode = False

                .SetActiveCell(aiCol, aiRow)
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
        End Try
    End Sub

    Public Sub sbDeleteCdList()
        Dim sFn As String = "Public Sub sbDeleteCdList()"

        Try
            With spdCdList
                .Row = miCurRow
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Filter_Query()
        Dim sFn As String = "Private Sub sbDisplay_Filter_Query()"

        Dim strSort As String = ""
        Dim sWhere As String = Me.lblFilter.AccessibleName

        m_dr_CdList = m_dt_CdList.Select(sWhere, strSort)

        If m_dr_CdList.Length < 1 Then
            MsgBox("해당 필터 조건에(" + Me.lblFilter.Text + ") 해당하는 검색 자료가 없습니다!!")
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Return
        End If

        Dim dt As DataTable = Fn.ChangeToDataTable(m_dr_CdList)

        Try
            Select Case msMstGbn
                Case mcFDF00
                    sbDisplayCdList_Usr(dt)
                Case mcFDF01
                    sbDisplayCdList_Bccls(dt)
                Case mcFDF02
                    sbDisplayCdList_Slip(dt)
                Case mcFDF03
                    sbDisplayCdList_Spc(dt)
                Case mcFDF04
                    sbDisplayCdList_SpcGrp(dt)
                Case mcFDF05
                    sbDisplayCdList_WkGrp(dt)
                Case mcFDF06
                    sbDisplayCdList_Tube(dt)
                Case mcFDF07
                    sbDisplayCdList_ExLab(dt)
                Case mcFDF09
                    sbDisplayCdList_TGrp(dt)
                Case mcFDF10
                    sbDisplayCdList_RstCd(dt)
                Case mcFDF11
                    sbDisplayCdList_Cmt(dt)
                Case mcFDF12
                    sbDisplayCdList_Calc(dt)
                Case mcFDF13
                    sbDisplayCdList_Eq(dt)

                Case mcFDF40
                    sbDisplayCdList_OSlip(dt)
                Case mcFDF41
                    sbDisplayCdList_KSRack(dt)
                Case mcFDF20
                    sbDisplayCdList_SpTest(dt)

                Case mcFDF15
                    sbDisplayCdList_Bacgen(dt)
                Case mcFDF16
                    sbDisplayCdList_Bac(dt)
                Case mcFDF17
                    sbDisplayCdList_Anti(dt)
                Case mcFDF18
                    sbDisplayCdList_BacgenAnti(dt)
                Case mcFDF19
                    sbDisplayCdList_BacRst(dt)

                Case mcFDF52
                    sbDisplayCdList_Cult(dt)

                Case mcFDF30
                    sbDisplayCdList_ComCd(dt)
                Case mcFDF31
                    sbDisplayCdList_FtCd(dt)
                Case mcFDF32
                    sbDisplayCdList_JobCd(dt)
                Case mcFDF33
                    sbDisplayCdList_DisCd(dt)
                Case mcFDF34
                    sbDisplayCdList_RtnCd(dt)
                Case mcFDF35
                    sbDisplayCdList_BldRef(dt)

                Case mcFDF42
                    sbDisplayCdList_CollTkCd(dt)

                Case mcFDF43
                    sbDisplayCdList_Cvt_RST(dt)

                Case mcFDF44
                    sbDisplayCdList_Cvt_CMT(dt)

                Case mcFDF45
                    sbDisplayCdList_KeyPad(dt)

                Case mcFDF46
                    sbDisplayCdList_DComCd(dt)

                Case mcFDF47
                    sbDisplayCdList_AbnRstCd(dt)

                Case mcFDF48
                    sbDisplayCdList_VCmt("CMT", dt)
                Case mcFDF49
                    sbDisplayCdList_vcmt_tcls(dt)

                Case mcFDF50
                    sbDisplayCdList_VCmt_Doctor(dt)

                Case mcFDF51
                    sbDisplayCdList_Alert_Rule(dt)
            End Select

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

#Region " sbDisplayCdCurRow 선언"
    Private Sub sbDisplayCdCurRow(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDiplayCdCurRow(ByVal iCurRow As Integer)"

        Try
            Select Case msMstGbn
                Case mcFDF00
                    sbDisplayCdCurRow_Usr(iCurRow)
                Case mcFDF01
                    sbDisplayCdCurRow_Bccls(iCurRow)
                Case mcFDF02
                    sbDisplayCdCurRow_Slip(iCurRow)
                Case mcFDF03
                    sbDisplayCdCurRow_Spc(iCurRow)
                Case mcFDF04
                    sbDisplayCdCurRow_SpcGrp(iCurRow)
                Case mcFDF05
                    sbDisplayCdCurRow_WkGrp(iCurRow)
                Case mcFDF06
                    sbDisplayCdCurRow_Tube(iCurRow)
                Case mcFDF07
                    sbDisplayCdCurRow_ExLab(iCurRow)
                Case mcFDF09
                    sbDisplayCdCurRow_TGrp(iCurRow)
                Case mcFDF10
                    sbDisplayCdCurRow_RstCd(iCurRow)
                Case mcFDF11
                    sbDisplayCdCurRow_Cmt(iCurRow)
                Case mcFDF12
                    sbDisplayCdCurRow_Calc(iCurRow)
                Case mcFDF13
                    sbDisplayCdCurRow_Eq(iCurRow)
                Case mcFDF40
                    sbDisplayCdCurRow_OSlip(iCurRow)
                Case mcFDF41
                    sbDisplayCdCurRow_KSRack(iCurRow)
                Case mcFDF20
                    sbDisplayCdCurRow_SpTest(iCurRow)

                Case mcFDF21
                    sbDisplayCdCurRow_SpTest_Cmt(iCurRow)

                Case mcFDF15
                    sbDisplayCdCurRow_Bacgen(iCurRow)
                Case mcFDF16
                    sbDisplayCdCurRow_Bac(iCurRow)
                Case mcFDF17
                    sbDisplayCdCurRow_Anti(iCurRow)
                Case mcFDF18
                    sbDisplayCdCurRow_BacgenAnti(iCurRow)
                Case mcFDF52
                    sbDisplayCdCurRow_Cult(iCurRow)
                Case mcFDF19
                    sbDisplayCdCurRow_Bac_Rst(iCurRow)

                Case mcFDF30
                    sbDisplayCdCurRow_ComCd(iCurRow)
                Case mcFDF31
                    sbDisplayCdCurRow_FtCd(iCurRow)
                Case mcFDF32
                    sbDisplayCdCurRow_JobCd(iCurRow)
                Case mcFDF33
                    sbDisplayCdCurRow_DisCd(iCurRow)
                Case mcFDF34
                    sbDisplayCdCurRow_RtnCd(iCurRow)
                Case mcFDF35
                    sbDisplayCdCurRow_BldRef(iCurRow)

                Case mcFDF42
                    sbDisplayCdCurRow_CollTkCd(iCurRow)

                Case mcFDF43
                    sbDisplayCdCurRow_Cvt_Rst(iCurRow)

                Case mcFDF44
                    sbDisplayCdCurRow_Cvt_Cmt(iCurRow)

                Case mcFDF45
                    sbDisplayCdCurRow_KeyPad(iCurRow)

                Case mcFDF46
                    sbDisplayCdCurRow_DComCd(iCurRow)

                Case mcFDF47
                    sbDisplayCdCurRow_AbnRstCd(iCurRow)

                Case mcFDF48
                    sbDisplayCdCurRow_VCmt(iCurRow, "CMT")
                Case mcFDF49
                    sbDisplayCdCurRow_vcmt_tcls(iCurRow)
                Case mcFDF50
                    sbDisplayCdCurRow_vcmt_Doctor(iCurRow)

                Case mcFDF51
                    sbDisplayCdCurRow_Alert_Rule(iCurRow)

                Case mcFDF53
                    sbDisplayCdCurRow_Ref(iCurRow)
                Case mcFDF54
                    sbDisplayCdCurRow_TestDoc(iCurRow)
            End Select

            sbDisplayChgUseDt(iCurRow)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub
#End Region

#Region " sbDisplayCdCurRow_% 일반검사, 공통 "

    Private Sub sbDisplayCdCurRow_DComCd(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("slipcd") : .Row = iCurRow : Dim strCd As String = .Text
                .Col = .GetColFromID("slipnmd") : .Row = iCurRow : Dim strNmd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim strModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim strModID As String = .Text

                CType(mfrmCur, FDF46).gsModDT = strModDT
                CType(mfrmCur, FDF46).gsModID = strModID

                If strCd <> "" Then
                    CType(mfrmCur, FDF46).sbDisplayCdDetail(strCd)
                End If
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_KeyPad(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : Dim sSpcCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDt As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModId As String = .Text

                CType(mfrmCur, FDF45).gsModDT = sModDt
                CType(mfrmCur, FDF45).gsModID = sModId

                If sTestCd <> "" Then
                    CType(mfrmCur, FDF45).sbDisplayCdDetail(sTestCd, sSpcCd)
                End If
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Cvt_Rst(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : Dim sSpcCd As String = .Text
                .Col = .GetColFromID("rstcdseq") : .Row = iCurRow : Dim sRstCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF43).gsModDT = sModDT
                CType(mfrmCur, FDF43).gsModID = sModID

                CType(mfrmCur, FDF43).sbDisplayCdDetail(sTestCd, sSpcCd, sRstCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Cvt_Cmt(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("cmtcd") : .Row = iCurRow : Dim sCmtpCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF44).gsModDT = sModDT
                CType(mfrmCur, FDF44).gsModID = sModID

                CType(mfrmCur, FDF44).sbDisplayCdDetail(sCmtpCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_CollTkCd()
        Dim sFn As String = "Private Sub sbSetColumnInfo_CollTkCd()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "구분" : .ColID = "cmtgbn_01" : .set_ColWidth(.GetColFromID("cmtgbn_01"), 4 / 5 * 24 + 1)
                .Col = 2 : .Text = "사유코드" : .ColID = "cmtcd" : .set_ColWidth(.GetColFromID("cmtcd"), 4 / 5 * 10 + 1)
                .Col = 3 : .Text = "사유명" : .ColID = "cmtcont" : .set_ColWidth(.GetColFromID("cmtcont"), 4 / 5 * 60 + 1)
                .Col = 4 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 4 / 5 * 12 + 1)
                .Col = 5 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 4 / 5 * 12 + 1)
                .Col = 6 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True
                .Col = 7 : .Text = "사용여부" : .ColID = "useyn" : .set_ColWidth(.GetColFromID("useyn"), 20)

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSetColumnInfo_AbnRstCd()
        Dim sFn As String = "Private Sub sbSetColumnInfo_AbnRstCd()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "구분" : .ColID = "cmtgbn_01" : .set_ColWidth(.GetColFromID("cmtgbn_01"), 4 / 5 * 24 + 1)
                .Col = 2 : .Text = "코드" : .ColID = "cmtcd" : .set_ColWidth(.GetColFromID("cmtcd"), 4 / 5 * 10 + 1)
                .Col = 3 : .Text = "내용" : .ColID = "cmtcont" : .set_ColWidth(.GetColFromID("cmtcont"), 4 / 5 * 60 + 1)
                .Col = 4 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 4 / 5 * 12 + 1)
                .Col = 5 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 4 / 5 * 12 + 1)
                .Col = 6 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True
                .Col = 7 : .Text = "사용여부" : .ColID = "useyn" : .set_ColWidth(.GetColFromID("useyn"), 20)

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_KSRack(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("BCCLSCD") : .Row = iCurRow : Dim sBcclscd As String = .Text
                .Col = .GetColFromID("RACKID") : .Row = iCurRow : Dim sRACKID As String = .Text
                .Col = .GetColFromID("SPCCD") : .Row = iCurRow : Dim sSPCCD As String = .Text

                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                If sModDT <> "" Then CType(mfrmCur, FDF41).gsModDT = sModDT
                If sModID <> "" Then CType(mfrmCur, FDF41).gsModID = sModID

                If sBcclscd = "" Then
                Else
                    CType(mfrmCur, FDF41).sbDisplayCdDetail(sBcclscd, sRACKID, sSPCCD)
                End If
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Calc(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : Dim sSpcCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF12).gsModDT = sModDT
                CType(mfrmCur, FDF12).gsModID = sModID

                CType(mfrmCur, FDF12).sbDisplayCdDetail(sTestCd, sSpcCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Cmt(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("cmtcd") : .Row = iCurRow : Dim sCmtCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF11).gsModDT = sModDT
                CType(mfrmCur, FDF11).gsModID = sModID

                CType(mfrmCur, FDF11).sbDisplayCdDetail(sCmtCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Eq(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("eqcd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUSDT As String = .Text
                .Col = .GetColFromID("uedt") : .Row = iCurRow : Dim sUEDT As String = .Text

                CType(mfrmCur, FDF13).sbDisplayCdDetail(sCd, sUSDT, sUEDT)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_ExLab(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("exlabcd") : .Row = iCurRow : Dim sCd As String = .Text

                CType(mfrmCur, FDF07).sbDisplayCdDetail(sCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_OSlip(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("tordslip") : .Row = iCurRow : Dim sTOSlipCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text

                CType(mfrmCur, FDF40).gsModDT = sModDT
                CType(mfrmCur, FDF40).gsModID = sModID


                CType(mfrmCur, FDF40).sbDisplayCdDetail(sTOSlipCd, sUsDt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_RstCd(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modi") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF10).gsModDT = sModDT
                CType(mfrmCur, FDF10).gsModID = sModID

                CType(mfrmCur, FDF10).sbDisplayCdDetail(sTestCd, CType(IIf(rdoSOpt0.Checked, 0, 1), Integer))
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then

                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Bccls(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Bccls(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("bcclscd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text
                .Col = .GetColFromID("uedt") : .Row = iCurRow : Dim sUeDt As String = .Text

                If sUsDt = "" Then sUsDt = Format(Now, "yyyyMMdd") + "000000"

                CType(mfrmCur, FDF01).sbDisplayCdDetail(sCd.Substring(0, 2), sUsDt, sUeDt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Slip(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Slip(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("slipcd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text
                .Col = .GetColFromID("uedt") : .Row = iCurRow : Dim sUeDt As String = .Text

                If sUsDt = "" Then sUsDt = Format(Now, "yyyyMMdd") + "000000"

                CType(mfrmCur, FDF02).sbDisplayCdDetail(sCd.Substring(0, 1), sCd.Substring(1, 1), sUsDt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Spc(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Spc(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("spccd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUSDT As String = .Text
                .Col = .GetColFromID("uedt") : .Row = iCurRow : Dim sUEDT As String = .Text

                CType(mfrmCur, FDF03).sbDisplayCdDetail(sCd, sUSDT, sUEDT)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_SpcGrp(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_SpcGrp(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("SPCGRPCD") : .Row = iCurRow : Dim sCd As String = .Text
                '.Col = .GetColFromID("USDT") : .Row = iCurRow : Dim sUSDT As String = Format(CType(.Text, Date), "yyyyMMddHHmmss")
                '.Col = .GetColFromID("UEDT") : .Row = iCurRow : Dim sUEDT As String = Format(CType(.Text, Date), "yyyyMMddHHmmss")

                CType(mfrmCur, FDF04).sbDisplayCdDetail(sCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_TGrp(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("tgrpcd") : .Row = iCurRow : Dim sTGrpCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF09).gsModDT = sModDT
                CType(mfrmCur, FDF09).gsModID = sModID

                CType(mfrmCur, FDF09).sbDisplayCdDetail(sTGrpCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Tube(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("tubecd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text
                .Col = .GetColFromID("uedt") : .Row = iCurRow : Dim sUeDt As String = .Text

                CType(mfrmCur, FDF06).sbDisplayCdDetail(sCd, sUsDt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Usr(ByVal riCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Usr(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("usrid") : .Row = riCurRow : Dim sCd As String = .Text

                CType(mfrmCur, FDF00).sbDisplayCdDetail(sCd)
                miCurRow = riCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_WkGrp(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_WkGrp(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("wkgrpcd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF05).gsModDT = sModDT
                CType(mfrmCur, FDF05).gsModID = sModID

                CType(mfrmCur, FDF05).sbDisplayCdDetail(sCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub


    Private Sub sbDisplayCdCurRow_VCmt_Doctor(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_VCmt_Doctor(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList

                .Col = .GetColFromID("doctorcd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text
                .Col = .GetColFromID("uedt") : .Row = iCurRow : Dim sUeDt As String = .Text

                CType(mfrmCur, FDF50).sbDisplayCdDetail(sCd, sUSDT, sUEDT)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Alert_Rule(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Alert_Rule(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                Dim sUSDT As String = ""
                Dim sUEDT As String = ""

                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text
                .Col = .GetColFromID("regid") : .Row = iCurRow : Dim sREGID As String = .Text
                .Col = .GetColFromID("regdt") : .Row = iCurRow : Dim sREGDT As String = .Text

                CType(mfrmCur, FDF51).gsModDT = sModDT
                CType(mfrmCur, FDF51).gsModID = sModID
                CType(mfrmCur, FDF51).gsREGID = sREGID
                CType(mfrmCur, FDF51).gsREGDT = sREGDT

                CType(mfrmCur, FDF51).sbDisplayCdDetail(sCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Ref(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Alert_Rule(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                Dim sUSDT As String = ""
                Dim sUEDT As String = ""

                .Col = .GetColFromID("refcd") : .Row = iCurRow : Dim sCd As String = .Text


                CType(mfrmCur, FDF53).sbDisplayCdDetail(sCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_TestDoc(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Alert_Rule(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                Dim sUSDT As String = ""
                Dim sUEDT As String = ""

                .Col = .GetColFromID("nmd") : .Row = iCurRow : Dim sCd As String = .Text


                CType(mfrmCur, FDF54).sbDisplayCdDetail(sCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region " sbDisplayCdCurRow_% 미생물 "
    Private Sub sbDisplayCdCurRow_Anti(ByVal iCurRow As Integer)
        Dim sFn As String = "Sub sbDisplayCdCurRow_Anti(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList

                .Col = .GetColFromID("anticd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text
                .Col = .GetColFromID("uedt") : .Row = iCurRow : Dim sUeDt As String = .Text


                CType(mfrmCur, FDF17).sbDisplayCdDetail(sCd, sUsDt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Bac(ByVal iCurRow As Integer)
        Dim sFn As String = "Sub sbDisplayCdCurRow_Bac(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                Dim sUSDT As String = ""
                Dim sUEDT As String = ""

                .Col = .GetColFromID("baccd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : sUSDT = .Text
                .Col = .GetColFromID("uedt") : .Row = iCurRow : sUEDT = .Text

                CType(mfrmCur, FDF16).sbDisplayCdDetail(sCd, sUSDT)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Bac_Rst(ByVal iCurRow As Integer)
        Dim sFn As String = "Sub sbDisplayCdCurRow_Bac_Rst(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : Dim sSpcCd As String = .Text
                .Col = .GetColFromID("incrstcd") : .Row = iCurRow : Dim sIncRstCd As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModid As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModdt As String = .Text

                CType(mfrmCur, FDF19).sbDisplayCdDetail(sTestCd, sSpcCd, sIncRstCd, sModid, sModdt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Bacgen(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Bacgen(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList

                .Col = .GetColFromID("bacgencd") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModId As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDt As String = .Text

                CType(mfrmCur, FDF15).sbDisplayCdDetail(sCd, sModId, sModDt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_BacgenAnti(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Bacgen(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                Dim sUSDT As String = "", sUEDT As String = ""

                .Col = .GetColFromID("bacgencd") : .Row = iCurRow : Dim sBacGen As String = .Text
                .Col = .GetColFromID("testmtd") : .Row = iCurRow : Dim sTmts As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModId As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDt As String = .Text

                CType(mfrmCur, FDF18).sbDisplayCdDetail(sBacGen, sTmts, sModId, sModDt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Cult(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Bacgen(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList

                .Col = .GetColFromID("cultnm") : .Row = iCurRow : Dim sCultNm As String = .Text
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : Dim sSpcCd As String = .Text
                .Col = .GetColFromID("usedays") : .Row = iCurRow : Dim sUssDays As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModId As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDt As String = .Text

                CType(mfrmCur, FDF52).sbDisplayCdDetail(sCultNm, sTestCd, sSpcCd, sUssDays, sModId, sModDt)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbDisplayCdCurRow_% 특수검사"
    Private Sub sbDisplayCdCurRow_SpTest(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDt As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModId As String = .Text

                CType(mfrmCur, FDF20).gsModDT = sModDt
                CType(mfrmCur, FDF20).gsModID = sModId

                CType(mfrmCur, FDF20).sbDisplayCdDetail(sTestCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbDisplayCdCurRow_% 특수검사 소견"
    Private Sub sbDisplayCdCurRow_SpTest_Cmt(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("testcd") : .Row = iCurRow : Dim sTestCd As String = .Text
                .Col = .GetColFromID("cmtseq") : .Row = iCurRow : Dim sCmtseq As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDt As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModId As String = .Text

                CType(mfrmCur, FDF21).gsModDT = sModDt
                CType(mfrmCur, FDF21).gsModID = sModId

                CType(mfrmCur, FDF21).sbDisplayCdDetail(sTestCd, sCmtseq)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbDisplayCdCurRow_% 혈액은행 "
    Private Sub sbDisplayCdCurRow_BldRef(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            ' 신규를 클릭한 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF35).gsModDT = sModDT
                CType(mfrmCur, FDF35).gsModID = sModID

                CType(mfrmCur, FDF35).sbDisplayCdDetail()
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_ComCd(ByVal iCurRow As Integer)
        Dim sFn As String = "Sub sbDisplayCdCurRow_ComCd(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("comcd") : .Row = iCurRow : Dim sComCd As String = .Text
                .Col = .GetColFromID("spccd") : .Row = iCurRow : Dim sSpcCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text

                CType(mfrmCur, FDF30).sbDisplayCdDetail(sComCd, sSpcCd, sUSDT)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_FtCd(ByVal iCurRow As Integer)
        Dim sFn As String = "Sub sbDisplayCdCurRow_FtCd(ByVal iCurRow As Integer)"

        Dim FtCd As String = ""

        Try
            '신규의 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("ftcd") : .Row = iCurRow : Dim sFtCd As String = .Text
                .Col = .GetColFromID("usdt") : .Row = iCurRow : Dim sUsDt As String = .Text

                CType(mfrmCur, FDF31).sbDisplayCdDetail(sFtCd, sUsDt)
                miCurRow = iCurRow
            End With

            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then      ' 조회, 수정 상태인 경우는 사용 시작일시 선택 불가능하다.
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdCurRow_JobCd(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            ' 신규를 클릭한 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("jobcd") : .Row = iCurRow : Dim sJobCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDt As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModId As String = .Text

                CType(mfrmCur, FDF32).gsModDT = sModDt
                CType(mfrmCur, FDF32).gsModID = sModId

                CType(mfrmCur, FDF32).sbDisplayCdDetail(sJobCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_DisCd(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            ' 신규를 클릭한 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("discd") : .Row = iCurRow : Dim sDisCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF33).gsModDT = sModDT
                CType(mfrmCur, FDF33).gsModID = sModID

                CType(mfrmCur, FDF33).sbDisplayCdDetail(sDisCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_RtnCd(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            ' 신규를 클릭한 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                Dim sCmtGbnCd As String = Ctrl.Get_Code(Me.spdCdList, "cmtgbn_01", iCurRow, True)
                .Col = .GetColFromID("cmtcd") : .Row = iCurRow : Dim sRtnCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF34).gsModDT = sModDT
                CType(mfrmCur, FDF34).gsModID = sModID

                CType(mfrmCur, FDF34).sbDisplayCdDetail(sCmtGbnCd, sRtnCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_CollTkCd(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            ' 신규를 클릭한 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                Dim sCmtGbnCd As String = Ctrl.Get_Code(Me.spdCdList, "cmtgbn_01", iCurRow, True)
                .Col = .GetColFromID("cmtcd") : .Row = iCurRow : Dim sRtnCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF42).gsModDT = sModDT
                CType(mfrmCur, FDF42).gsModID = sModID

                CType(mfrmCur, FDF42).sbDisplayCdDetail(sCmtGbnCd, sRtnCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_AbnRstCd(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            ' 신규를 클릭한 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                Dim sCmtGbnCd As String = Ctrl.Get_Code(Me.spdCdList, "cmtgbn_01", iCurRow, True)
                .Col = .GetColFromID("cmtcd") : .Row = iCurRow : Dim sRtnCd As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF47).gsModDT = sModDT
                CType(mfrmCur, FDF47).gsModID = sModID

                CType(mfrmCur, FDF47).sbDisplayCdDetail(sCmtGbnCd, sRtnCd)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_VCmt(ByVal iCurRow As Integer, ByVal rsCdSep As String)
        Dim sFn As String = ""

        Try
            ' 신규를 클릭한 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList

                .Col = .GetColFromID("cdseq") : .Row = iCurRow : Dim sCdSeq As String = .Text
                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF48).gsModDT = sModDT
                CType(mfrmCur, FDF48).gsModID = sModID

                CType(mfrmCur, FDF48).sbDisplayCdDetail(rsCdSep, sCdSeq)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_VCmt_Tcls(ByVal iCurRow As Integer)
        Dim sFn As String = ""

        Try
            ' 신규를 클릭한 경우
            If rdoWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList

                .Col = .GetColFromID("moddt") : .Row = iCurRow : Dim sModDT As String = .Text
                .Col = .GetColFromID("modid") : .Row = iCurRow : Dim sModID As String = .Text

                CType(mfrmCur, FDF49).gsModDT = sModDT
                CType(mfrmCur, FDF49).gsModID = sModID

                CType(mfrmCur, FDF49).sbDisplayCdDetail()
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rdoWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

#End Region


#Region " sbDisplayCdList 선언"
    Private Sub sbDisplayCdList(ByVal rsBuf As String)
        Dim sFn As String = "Private Sub sbDisplayCdList(ByVal asBuf As String)"

        Try
            '전체자료 조회 시에는 신규, 수정할 수 없도록 Disable
            Select Case rsBuf
                Case mcFDF00
                    sbDisplayCdList_Usr()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF01
                    sbDisplayCdList_Bccls()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF02
                    sbDisplayCdList_Slip()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF03
                    sbDisplayCdList_Spc()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF04
                    sbDisplayCdList_SpcGrp()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF05
                    sbDisplayCdList_WkGrp()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF06
                    sbDisplayCdList_Tube()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF07
                    sbDisplayCdList_ExLab()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF09
                    sbDisplayCdList_TGrp()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF10
                    sbDisplayCdList_RstCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF11
                    sbDisplayCdList_Cmt()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF12
                    sbDisplayCdList_Calc()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF13
                    sbDisplayCdList_Eq()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF40
                    sbDisplayCdList_OSlip()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF41
                    sbDisplayCdList_KSRack()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF15
                    sbDisplayCdList_Bacgen()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF16
                    sbDisplayCdList_Bac()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF17
                    sbDisplayCdList_Anti()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF18
                    sbDisplayCdList_BacgenAnti()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF52
                    sbDisplayCdList_Cult()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF19
                    sbDisplayCdList_Bac_Rst()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF20
                    sbDisplayCdList_SpTest()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF21
                    sbDisplayCdList_SpTest_cmt()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF30
                    sbDisplayCdList_ComCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF31
                    sbDisplayCdList_FtCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF32
                    sbDisplayCdList_JobCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF33
                    sbDisplayCdList_DisCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF34
                    sbDisplayCdList_RtnCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF35
                    sbDisplayCdList_BldRef()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF42
                    sbDisplayCdList_CollTkCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF43
                    sbDisplayCdList_Cvt_RST()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF44
                    sbDisplayCdList_Cvt_CMT()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF45
                    sbDisplayCdList_KeyPad()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF46
                    sbDisplayCdList_DComCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF47
                    sbDisplayCdList_AbnRstCd()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF48
                    sbDisplayCdList_VCmt("CMT")
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF49
                    sbDisplayCdList_VCmt_Tcls()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDF50
                    sbDisplayCdList_VCmt_Doctor()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF51
                    sbDisplayCdList_Alert_Rule()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF53
                    sbDisplayCdList_Ref()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)

                Case mcFDF54
                    sbDisplayCdList_TestDoc()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case Else

            End Select

            sbLoad_Popup_Filter()


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbDisplayCdList_% 일반검사, 공통 "

    Private Sub sbDisplayCdList_DComCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_DComCd()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_DCOMCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_DComCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_DCOMCD

            If r_dt Is Nothing Then
                dt = objDAF.GetDcomCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 0 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim

                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 2
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_KeyPad(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_KeyPad()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_KEYPAD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_KeyPad) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_KEYPAD

            If r_dt Is Nothing Then
                dt = objDAF.GetKeyPadInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 5
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_Cvt_RST(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Cvt_RST()"

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_CVT_RST
            Dim iCol As Integer = 0
            Dim Serch As String = lblFilter.Text


            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_CalcRst) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_CVT_RST

            If r_dt Is Nothing Then
                dt = objDAF.GetCvtRstInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 10
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_Cvt_CMT(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Cvt_CMT()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_CVT_CMT
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_CalcRst) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_CVT_CMT

            If r_dt Is Nothing Then
                dt = objDAF.GetCvtCmtInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 7
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_KSRack(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_KSRack()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_KSRACK
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_KSRack) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_KSRACK

            If r_dt Is Nothing Then
                dt = objDAF.GetKSRackInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToUpper())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 9
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Calc(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Calc()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_CALC
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Calc) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_CALC

            If r_dt Is Nothing Then
                dt = objDAF.GetCalcInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 7
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Cmt(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Cmt()"
        Dim sSerch As String = lblFilter.Text

        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_CMT
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Cmt) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_CMT

            If r_dt Is Nothing Then
                dt = objDAF.GetCmtInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), sSerch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 6
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Eq(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Eq()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_EQ
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Eq) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_EQ

            If r_dt Is Nothing Then
                dt = objDAF.GetEqInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 2
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_ExLab(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_ExLab()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_EXLAB
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_ExLab) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_EXLAB

            If r_dt Is Nothing Then
                dt = objDAF.GetExLabInfo(CType(IIf(Me.rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                        .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 2

                        If rdoSOpt1.Checked Then
                            .BlockMode = True : .ForeColor = System.Drawing.Color.Red : .BlockMode = False
                        End If
                    End If

                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_OSlip(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_OSlip()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_OSLIP
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_OSlip) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_OSLIP

            If r_dt Is Nothing Then
                dt = objDAF.GetOSlipInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = System.Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_RstCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_RstCd()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_RSTCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_RstCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_RSTCD

            If r_dt Is Nothing Then
                dt = objDAF.GetRstCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Bccls(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_bccls()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_BCCLS
            Dim iCol As Integer = 0
            Dim asGbn As String = ""
            Dim asSerch As String = ""


            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Sect) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If
            objDAF = New LISAPP.APP_F_BCCLS

            If r_dt Is Nothing Then
                dt = objDAF.GetBcclsInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 3

                            .BlockMode = True

                            .ForeColor = System.Drawing.Color.Red
                            .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Slip(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Slip()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_SLIP
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Slip) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_SLIP

            If r_dt Is Nothing Then
                dt = objDAF.GetSlipInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 3
                            .BlockMode = True : .ForeColor = System.Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Spc(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Spc()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_SPC
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Spc) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_SPC

            If r_dt Is Nothing Then
                dt = objDAF.GetSpcInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 5

                            .BlockMode = True : .ForeColor = System.Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_SpcGrp(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_SpcGrp()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_SPCGRP
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_SpcGrp) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_SPCGRP

            If r_dt Is Nothing Then
                dt = objDAF.GetSpcGrpInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch, "")
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 1
                            .BlockMode = True : .BackColor = System.Drawing.Color.FromArgb(255, 220, 220) : .BlockMode = False
                        End If
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_TGrp(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_TGrp()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_TGRP
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_TGrp) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_TGRP

            If r_dt Is Nothing Then
                dt = objDAF.GetTGrpInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Tube(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Tube()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_TUBE
            Dim iCol As Integer = 0
            Dim asGbn As String = ""
            Dim asSerch As String = ""

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Tube) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_TUBE

            If r_dt Is Nothing Then
                dt = objDAF.GetTubeInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 6
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Usr(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Usr()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_USR
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_User) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_USR

            If r_dt Is Nothing Then
                dt = objDAF.GetUsrInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count > 0 Then
                With spdCdList
                    .ReDraw = False

                    .MaxRows = dt.Rows.Count

                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = dt.Rows(i).Item(j).ToString.Trim
                            End If
                        Next

                        If rdoSOpt1.Checked Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True

                                .ForeColor = System.Drawing.Color.Red
                                .BlockMode = False
                            End If
                        End If
                    Next

                    'Autosize
                    For j As Integer = 1 To .MaxCols
                        .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                    Next

                    .ReDraw = True
                End With
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_WkGrp(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_WkGrp()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_WKGRP
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_WkGrp) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_WKGRP

            If r_dt Is Nothing Then
                dt = objDAF.GetWGrpInfo(CType(IIf(Me.rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 5
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbDisplayCdList_% 미생물 "


    Private Sub sbDisplayCdList_Anti(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Sect()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_ANTI
            Dim iCol As Integer = 0


            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Anti) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_ANTI

            If r_dt Is Nothing Then
                dt = objDAF.GetAntiInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 5
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_Bac(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_Bac()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_BAC
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Bac) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_BAC

            If r_dt Is Nothing Then
                dt = objDAF.GetBacInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 5
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_Bac_Rst(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_Bac_Rst()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_BAC_RST
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_BacRst) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_BAC_RST

            If r_dt Is Nothing Then
                dt = objDAF.GetBacRstInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch, "")
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 2
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_Bacgen(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Becgen()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_BACGEN
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Bacgen) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_BACGEN

            If r_dt Is Nothing Then
                dt = objDAF.GetBacgenInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 2
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_BacgenAnti(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_BacgenAnti()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_BACGEN_ANTI
            Dim iCol As Integer = 0


            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_BacgenAnti) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_BACGEN_ANTI

            If r_dt Is Nothing Then
                dt = objDAF.GetBacgenAntiInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 3
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_Cult(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_Cult()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_CULT
            Dim iCol As Integer = 0


            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_BacgenAnti) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_CULT

            If r_dt Is Nothing Then
                dt = objDAF.GetCultiInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_BacRst(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_BacgenAnti()"

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_BAC_RST
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_BacgenAnti) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_BAC_RST

            If r_dt Is Nothing Then
                dt = objDAF.GetBacRstInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), "", "")
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 1
                            .BlockMode = True : .BackColor = System.Drawing.Color.FromArgb(255, 220, 220) : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

#End Region

#Region " sbDisplayCdList_% 특수검사"
    Private Sub sbDisplayCdList_SpTest(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_SpTest()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_SPTEST
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_SpTest) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_SPTEST

            If r_dt Is Nothing Then
                dt = objDAF.GetSpTestInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbDisplayCdList_% 특수검사 소견"
    Private Sub sbDisplayCdList_SpTest_cmt(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_SpTest_cmt()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_SPTEST_CMT
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_SpTest) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_SPTEST_CMT

            If r_dt Is Nothing Then
                dt = objDAF.GetSpCmtTestInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbDisplayCdList_% 혈액은행 "

    Private Sub sbDisplayCdList_BldRef(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_BldRef()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_BLD_REF
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_RtnCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_BLD_REF

            If r_dt Is Nothing Then
                dt = objDAF.GetBldRefInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 1
                                .BlockMode = True : .BackColor = System.Drawing.Color.FromArgb(255, 220, 220) : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_ComCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub spDisplayCdList_ComCdList()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_COMCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_ComCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_COMCD

            If r_dt Is Nothing Then
                dt = objDAF.GetComCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_FtCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_FtCd()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As New DataTable
            Dim objDAF As LISAPP.APP_F_FTCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_FtCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_FTCD

            If r_dt Is Nothing Then
                dt = objDAF.GetFtCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 2
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayCdList_JobCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_JobCd()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_JOBCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_JobCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_JOBCD

            If r_dt Is Nothing Then
                dt = objDAF.GetJobCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_DisCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_DisCd()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_DISCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_DisCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_DISCD

            If r_dt Is Nothing Then
                dt = objDAF.GetDisCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_RtnCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_RtnCd()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_RTNCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_RtnCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_RTNCD

            If r_dt Is Nothing Then
                dt = objDAF.GetRtnCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 6
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_CollTkCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_CollTkCd()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_COLLTKCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_CollTk) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_COLLTKCD

            If r_dt Is Nothing Then
                dt = objDAF.GetCollTkCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), "", Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next


                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 5

                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If

                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_AbnRstCd(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_AbnRstCd()"
        Dim Serch As String = lblFilter.Text
        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_COLLTKCD
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_AbnRstCd) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_COLLTKCD

            If r_dt Is Nothing Then
                dt = objDAF.GetCollTkCdInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), "ETC", Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 5
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_VCmt(ByVal rsCdSep As String, Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_VCmt()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_VCMT
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_VCmt) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_VCMT

            If r_dt Is Nothing Then
                dt = objDAF.GetVCmtInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), rsCdSep, Serch, "")
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 3
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_VCmt_Tcls(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Sub sbDisplayCdList_VCmt_Tcls()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_VCMT_TCLS
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_VCmt_Tcls) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_VCMT_TCLS

            If r_dt Is Nothing Then
                dt = objDAF.GetVCmtTclsInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 2
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayCdList_VCmt_Doctor(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_VCmt_Doctor()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_VCMT_DOCTOR
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_VCmt_Doctor) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_VCMT_DOCTOR

            If r_dt Is Nothing Then
                dt = objDAF.GetVCmtDoctorInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                            .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 3
                            .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbDisplayCdList_Alert_Rule(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Alert_Rule()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_ALERT_RULE
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Alert_Rule) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_ALERT_RULE

            If r_dt Is Nothing Then
                dt = objDAF.GetAlertRuleInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    '<<<20170605 병원체코드 디스플레이 추가 

    Public Sub sbDisplayCdList_Ref(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Alert_Rule()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_REF
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Alert_Rule) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_REF

            If r_dt Is Nothing Then
                dt = objDAF.GetRefInfo(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    '<<<20191210 검사의뢰지침서 관리 추가

    Public Sub sbDisplayCdList_TestDoc(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Alert_Rule()"
        Dim Serch As String = lblFilter.Text

        Try
            Dim dt As DataTable
            Dim objDAF As LISAPP.APP_F_BLD_REF
            Dim iCol As Integer = 0

            If Not USER_SKILL.Authority("F01", mc_Add_Or_Edit_Of_Alert_Rule) Then
                Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
                Me.btnReg.Enabled = False
            End If

            objDAF = New LISAPP.APP_F_BLD_REF

            If r_dt Is Nothing Then
                dt = objDAF.GeTTestDoc(CType(IIf(rdoSOpt0.Checked, 0, 1), Integer), Serch)
                m_dt_CdList = dt
                m_dr_CdList = dt.Select()
            Else
                dt = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If dt.Rows.Count < 1 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next

                    If rdoSOpt1.Checked Then
                        If IsNumeric(dt.Rows(i).Item("diffday")) Then
                            If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                                .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 4
                                .BlockMode = True : .ForeColor = Drawing.Color.Red : .BlockMode = False
                            End If
                        End If
                    End If
                Next

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub
#End Region


#Region " sbDisplayClear 선언"
    Private Sub sbDisplayClear()
        If Not IsNothing(mfrmCur) Then
            Select Case msMstGbn
                Case mcFDF00
                    CType(mfrmCur, FDF00).btnDel.Visible = False
                    CType(mfrmCur, FDF00).sbInitialize()
                Case mcFDF01
                    CType(mfrmCur, FDF01).btnUE.Visible = False
                    CType(mfrmCur, FDF01).sbInitialize()
                Case mcFDF02
                    CType(mfrmCur, FDF02).btnUE.Visible = False
                    CType(mfrmCur, FDF02).giClearKey = 1
                    CType(mfrmCur, FDF02).sbInitialize()
                    CType(mfrmCur, FDF02).giClearKey = 0
                Case mcFDF03
                    CType(mfrmCur, FDF03).btnUE.Visible = False
                    CType(mfrmCur, FDF03).sbInitialize()
                Case mcFDF04
                    CType(mfrmCur, FDF04).btnUE.Visible = False
                    CType(mfrmCur, FDF04).sbInitialize()
                Case mcFDF05
                    CType(mfrmCur, FDF05).btnUE.Visible = False
                    CType(mfrmCur, FDF05).sbInitialize()
                Case mcFDF06
                    CType(mfrmCur, FDF06).btnUE.Visible = False
                    CType(mfrmCur, FDF06).sbInitialize()
                Case mcFDF07
                    CType(mfrmCur, FDF07).btnDel.Visible = False
                    CType(mfrmCur, FDF07).sbInitialize()
                Case mcFDF09
                    CType(mfrmCur, FDF09).btnUE.Visible = False
                    CType(mfrmCur, FDF09).sbInitialize()
                Case mcFDF10
                    CType(mfrmCur, FDF10).btnUE.Visible = False
                    CType(mfrmCur, FDF10).sbInitialize()
                Case mcFDF11
                    CType(mfrmCur, FDF11).btnUE.Visible = False
                    CType(mfrmCur, FDF11).sbInitialize()
                Case mcFDF12
                    CType(mfrmCur, FDF12).btnUE.Visible = False
                    CType(mfrmCur, FDF12).sbInitialize()
                Case mcFDF13
                    CType(mfrmCur, FDF13).btnDel.Visible = False
                    CType(mfrmCur, FDF13).sbInitialize()
                Case mcFDF40
                    CType(mfrmCur, FDF40).btnUE.Visible = False
                    CType(mfrmCur, FDF40).sbInitialize()

                Case mcFDF41
                    CType(mfrmCur, FDF41).btnUE.Visible = False
                    CType(mfrmCur, FDF41).sbInitialize()

                Case mcFDF15
                    CType(mfrmCur, FDF15).sbInitialize()
                Case mcFDF16
                    CType(mfrmCur, FDF16).btnUE.Visible = False
                    CType(mfrmCur, FDF16).sbInitialize()
                Case mcFDF17
                    CType(mfrmCur, FDF17).btnUE.Visible = False
                    CType(mfrmCur, FDF17).sbInitialize()
                Case mcFDF18
                    CType(mfrmCur, FDF18).sbInitialize()
                Case mcFDF19
                    CType(mfrmCur, FDF19).btnUE.Visible = False
                    CType(mfrmCur, FDF19).sbInitialize()

                Case mcFDF20
                    CType(mfrmCur, FDF20).btnUE.Visible = False
                    CType(mfrmCur, FDF20).sbInitialize()

                Case mcFDF21
                    CType(mfrmCur, FDF21).btnUE.Visible = False
                    CType(mfrmCur, FDF21).sbInitialize()

                    'Case mcFDF21
                    '    CType(mfrmCur, FDF21).giClearKey = 1
                    '    CType(mfrmCur, FDF21).sbInitialize()
                    '    CType(mfrmCur, FDF21).giClearKey = 0

                Case mcFDF30
                    CType(mfrmCur, FDF30).btnUE.Visible = False
                    CType(mfrmCur, FDF30).sbInitialize()
                Case mcFDF31
                    CType(mfrmCur, FDF31).btnUE.Visible = False
                    CType(mfrmCur, FDF31).sbInitialize()
                Case mcFDF32
                    CType(mfrmCur, FDF32).btnUE.Visible = False
                    CType(mfrmCur, FDF32).sbInitialize()
                Case mcFDF33
                    CType(mfrmCur, FDF33).btnUE.Visible = False
                    CType(mfrmCur, FDF33).sbInitialize()
                Case mcFDF34
                    CType(mfrmCur, FDF34).btnUE.Visible = False
                    CType(mfrmCur, FDF34).sbInitialize()
                Case mcFDF35
                    CType(mfrmCur, FDF35).sbInitialize()

                Case mcFDF42
                    CType(mfrmCur, FDF42).btnDel.Visible = False
                    CType(mfrmCur, FDF42).sbInitialize()

                Case mcFDF43
                    CType(mfrmCur, FDF43).btnUE.Visible = False
                    CType(mfrmCur, FDF43).sbInitialize()

                Case mcFDF44
                    CType(mfrmCur, FDF44).btnUE.Visible = False
                    CType(mfrmCur, FDF44).sbInitialize()

                Case mcFDF45
                    CType(mfrmCur, FDF45).btnUE.Visible = False
                    CType(mfrmCur, FDF45).sbInitialize()

                Case mcFDF46
                    CType(mfrmCur, FDF46).btnUE.Visible = False
                    CType(mfrmCur, FDF46).sbInitialize()

                Case mcFDF47
                    CType(mfrmCur, FDF47).btnDel.Visible = False
                    CType(mfrmCur, FDF47).sbInitialize()

                Case mcFDF48
                    CType(mfrmCur, FDF48).btnUE.Visible = False
                    CType(mfrmCur, FDF48).sbInitialize()
                Case mcFDF49
                    CType(mfrmCur, FDF49).btnUE.Visible = False
                    CType(mfrmCur, FDF49).sbInitialize()
                Case mcFDF50
                    CType(mfrmCur, FDF50).btnUE.Visible = False
                    CType(mfrmCur, FDF50).sbInitialize()
                Case mcFDF51
                    CType(mfrmCur, FDF51).btnUE.Visible = False
                    CType(mfrmCur, FDF51).sbInitialize()
                Case mcFDF52
                    CType(mfrmCur, FDF52).btnUE.Visible = False
                    CType(mfrmCur, FDF52).sbInitialize()

                Case mcFDF53
                    CType(mfrmCur, FDF53).btnUE.Visible = False
                    CType(mfrmCur, FDF53).sbInitialize()


            End Select
        End If
    End Sub
#End Region

    Private Sub sbDisplayColumnNm(ByVal riCol As Integer)
        Dim sColNm As String = ""

        With Me.spdCdList
            .Col = riCol : .Row = 0 : sColNm = .Text
        End With

        Me.lblFieldNm.Text = sColNm
        Me.lblFieldNm.Tag = riCol
    End Sub

    Private Sub sbInitialize(ByVal asBuf As String)
        Dim sFn As String = "Private Sub sbInitialize(ByVal asBuf As String))"

        Try
            '< add freety 2007/05/03 : 검색기능 추가
            Me.lblFieldNm.Text = ""
            Me.txtFieldVal.Text = ""
            '>

            If asBuf = "" Then
                Exit Sub
            End If

            sbInitialize_spdCdList(asBuf)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

#Region " sbInitialize_spdCdList 선언"
    Private Sub sbInitialize_spdCdList(ByVal asBuf As String)
        Dim sFn As String = "Private Sub sbInitialize_spdCdList(ByVal asBuf As String)"

        Try
            If asBuf = "" Then
                Exit Sub
            End If

            With spdCdList
                .MaxRows = 0

                Select Case asBuf
                    Case mcFDF00
                        sbSetColumnInfo_Usr()
                    Case mcFDF01
                        sbSetColumnInfo_Bccls()
                    Case mcFDF02
                        sbSetColumnInfo_Slip()
                    Case mcFDF03
                        sbSetColumnInfo_Spc()
                    Case mcFDF04
                        sbSetColumnInfo_SpcGrp()
                    Case mcFDF05
                        sbSetColumnInfo_WkGrp()
                    Case mcFDF06
                        sbSetColumnInfo_Tube()
                    Case mcFDF07
                        sbSetColumnInfo_ExLab()
                    Case mcFDF09
                        sbSetColumnInfo_TGrp()
                    Case mcFDF10
                        sbSetColumnInfo_RstCd()
                    Case mcFDF11
                        sbSetColumnInfo_Cmt()
                    Case mcFDF12
                        sbSetColumnInfo_Calc()
                    Case mcFDF13
                        sbSetColumnInfo_Eq()
                    Case mcFDF40
                        sbSetColumnInfo_OSlip()
                    Case mcFDF41
                        sbSetColumnInfo_KSRack()

                    Case mcFDF20
                        sbSetColumnInfo_SpTest()

                        'jjh 특수보고서 소견 추가
                    Case mcFDF21
                        sbSetColumnInfo_SpTest_Cmt()



                    Case mcFDF15
                        sbSetColumnInfo_Bacgen()
                    Case mcFDF16
                        sbSetColumnInfo_Bac()
                    Case mcFDF17
                        sbSetColumnInfo_Anti()
                    Case mcFDF18
                        sbSetColumnInfo_BacgenAnti()
                    Case mcFDF52
                        sbSetColumnInfo_Cult()
                    Case mcFDF19
                        sbSetColumnInfo_Bac_Rst()

                    Case mcFDF30
                        sbSetColumnInfo_ComCd()
                    Case mcFDF31
                        sbSetColumnInfo_FtCd()
                    Case mcFDF32
                        sbSetColumnInfo_JobCd()
                    Case mcFDF33
                        sbSetColumnInfo_DisCd()
                    Case mcFDF34
                        sbSetColumnInfo_RtnCd()
                    Case mcFDF35
                        sbSetColumnInfo_BldRef() 'ㄱ반납/폐기 사유
                    Case mcFDF42
                        sbSetColumnInfo_CollTkCd()

                    Case mcFDF43
                        sbSetColumnInfo_Cvt_RST()

                    Case mcFDF44
                        sbSetColumnInfo_Cvt_CMT()

                    Case mcFDF45
                        sbSetColumnInfo_KeyPad()

                    Case mcFDF46
                        sbSetColumnInfo_DComCd()

                    Case mcFDF47
                        sbSetColumnInfo_AbnRstCd()

                    Case mcFDF48
                        sbSetColumnInfo_VCmt()

                    Case mcFDF49
                        sbSetColumnInfo_VCmt_Tcls()

                    Case mcFDF50
                        sbSetColumnInfo_VCmt_Doctor()

                    Case mcFDF51
                        sbSetColumnInfo_Alert_Rule()

                    Case mcFDF53
                        sbSetColumnInfo_refcode()
                    Case mcFDF54
                        sbSetColumnInfo_TestDoc()
                    Case Else
                End Select
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

    Public Sub sbMinimize()
        Me.WindowState = Windows.Forms.FormWindowState.Minimized

        If Not IsNothing(mfrmCur) Then
            mfrmCur.Hide()
        End If
    End Sub

    Private Sub sbNew()
        Dim sFn As String = "sbNew"

        Try
            rbnWorkOpt0.Checked = True
            miFirstWidth_pnlLeft = Me.pnlLeft.Width

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbPreviousFormClose(ByVal asBuf As String)
        Dim sFn As String = "sbPreviousFormClose(ByVal asBuf As String)"

        Try
            If asBuf = "" Then Exit Sub

            If Not IsNothing(mfrmCur) Then
                mfrmCur.Dispose()
                mfrmCur = Nothing
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

#Region " sbReg 선언"
    Private Sub sbReg()
        Select Case msMstGbn
            Case mcFDF00        'USER
                sbReg_Usr()
            Case mcFDF01        'SECT/TSECT
                sbReg_Bccls()
            Case mcFDF02        'PART/SLIP
                sbReg_Slip()
            Case mcFDF03        'SPC
                sbReg_Spc()
            Case mcFDF04        'SPCGRP
                sbReg_SpcGrp()
            Case mcFDF05        'WKGRP
                sbReg_WkGrp()
            Case mcFDF06        'TUBE
                sbReg_Tube()
            Case mcFDF07        'EXLAB
                sbReg_ExLab()
            Case mcFDF09        'TGRP
                sbReg_TGrp()
            Case mcFDF10        'RSTCD
                sbReg_RstCd()
            Case mcFDF11        'CMT
                sbReg_Cmt()
            Case mcFDF12        'CALC
                sbReg_Calc()
            Case mcFDF13        'EQ
                sbReg_Eq()
            Case mcFDF40        'OSLIP
                sbReg_OSlip()
            Case mcFDF41        'KSRACK
                sbReg_KSRack()

            Case mcFDF15        'BACGEN
                sbReg_Bacgen()
            Case mcFDF16        'BAC
                sbReg_Bac()
            Case mcFDF17        'ANTI
                sbReg_Anti()
            Case mcFDF18        'BACGEN-ANTI
                sbReg_BacgenAntiList()
            Case mcFDF52       'BACGEN-ANTI
                sbReg_Cult()
            Case mcFDF19       'BACGEN-ANTI
                sbReg_Bac_Rst()

            Case mcFDF20        'SPTEST
                sbReg_SpTest()
            Case mcFDF21        'SPTEST_cmt
                sbReg_SpCmtTest()

            Case mcFDF30        '성분제제 마스터
                sbReg_ComCd()
            Case mcFDF31        '필터 마스터
                sbReg_FtCd()
            Case mcFDF32        '직업 마스터
                sbReg_JobCd()
            Case mcFDF33        '부적격사유 마스터
                sbReg_DisCd()
            Case mcFDF34        '반납폐기사유 마스터
                sbReg_RtnCd()
            Case mcFDF35        'KSRACK
                sbReg_BldRef()

            Case mcFDF42
                sbReg_CollTkCd()

            Case mcFDF43
                sbReg_Cvt_RST()

            Case mcFDF44
                sbReg_Cvt_CMT()

            Case mcFDF45
                sbReg_KeyPad()

            Case mcFDF46
                sbReg_DComCd()

            Case mcFDF47
                sbReg_AbnRstCd()

            Case mcFDF48
                sbReg_VCmt()

            Case mcFDF49
                sbReg_VCmt_Tcls()

            Case mcFDF50
                sbReg_VCmt_Doctor()

            Case mcFDF51
                sbReg_Alert_Rule()

            Case mcFDF53        '병원체코드 
                sbReg_Ref()
            Case mcFDF54        '검사의뢰지침서 관리
                sbReg_TestDoc()
            Case Else
        End Select
    End Sub
#End Region

#Region " sbReg_% 일반검사, 공통 "

    Private Sub sbReg_DComCd()
        Dim sFn As String = "Sub sbReg_DComCd()"
        Dim sMsg As String = ""

        Try
            sMsg += "검사분야 : " + CType(mfrmCur, FDF46).cboSlip.Text
            sMsg += "의 성분제제" + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF46).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF46).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사분야에 성분제제 설정 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_DComCd()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사분야에 성분제제 설정 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_KeyPad()
        Dim sFn As String = "Sub sbReg_KeyPad()"
        Dim sMsg As String = ""

        Try
            sMsg += "검사코드 : " + CType(mfrmCur, FDF45).txtTestCd.Text + ", "
            sMsg += "검체코드 : " + CType(mfrmCur, FDF45).txtSpcCd.Text + vbCrLf + vbCrLf
            sMsg += "의 세부항목" + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF45).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF45).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 KEYPAD 설정 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_KeyPad()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 KEYPAD 설정 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_Cvt_RST()
        Dim sFn As String = "Sub sbReg_Cvt_RST()"
        Dim sMsg As String = ""

        Try
            sMsg += "검사코드 : " + CType(mfrmCur, FDF43).txtTestCd.Text + ", "
            sMsg += "검체코드 : " + CType(mfrmCur, FDF43).txtSpcCd.Text + vbCrLf + vbCrLf
            sMsg += "의 계산식내용" + vbCrLf + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf Me.rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF43).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF43).fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 결과값 자동변환 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Cvt_RST()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 결과값 자동변환 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_Cvt_CMT()
        Dim sFn As String = "Sub sbReg_Cvt_CMT()"
        Dim sMsg As String = ""

        Try
            sMsg += "소견코드 : " + CType(mfrmCur, FDF44).txtCmtCd.Text + ", "
            sMsg += "의 계산식내용" + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF44).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF44).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 결과값 자동변환 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Cvt_CMT()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 결과값 자동변환 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub sbReg_CollTkCd()
        Dim sFn As String = "Sub sbReg_CollTkCd()"
        Dim sMsg As String = ""

        Try
            sMsg = ""
            sMsg += "채혈접수 취소" + "사유구분 : " + Ctrl.Get_Item(CType(mfrmCur, FDF42).cboCmtGbn) + ", " + vbCrLf
            sMsg += "사유코드 : " + CType(mfrmCur, FDF42).txtCmtCd.Text + ", "
            sMsg += "사유명 : " + CType(mfrmCur, FDF42).txtCmtCont.Text

            If rdoWorkOpt1.Checked Then
                sMsg += " 을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += " 을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF42).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF42).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 채혈접수 취소사유 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_CollTkCd()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 채혈접수 취소사유 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_AbnRstCd()
        Dim sFn As String = "Sub sbReg_AbnRstCd()"
        Dim sMsg As String = ""

        Try
            sMsg = "구분 : " + CType(mfrmCur, FDF47).cboCmtGbn.Text + ", "
            sMsg += "코드 : " + CType(mfrmCur, FDF47).txtCmtCd.Text + ", "
            sMsg += "특이결과 : " + CType(mfrmCur, FDF47).txtCmtCont.Text

            If rdoWorkOpt1.Checked Then
                sMsg += " 을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += " 을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF47).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF47).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 기타코드 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_AbnRstCd()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 기타코드 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_KSRack()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF41).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사계 : " + CType(mfrmCur, FDF41).txtBcclsCd.Text + ", "
            sMsg += "보관검체 Rack ID : " + CType(mfrmCur, FDF41).txtRackId.Text + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "보관검체 정보을 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "보관검체 정보를 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF41).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 보관검체 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        'sbUpdateCdList_KSRack()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 보관검체 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If


                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Calc()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF12).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사코드 : " + CType(mfrmCur, FDF12).txtTestCd.Text + ", "
            sMsg += "검체코드 : " + CType(mfrmCur, FDF12).txtSpcCd.Text + vbCrLf + vbCrLf
            sMsg += "의 계산식내용" + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF12).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 계산식정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Calc()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 계산식정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Cmt()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF11).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "소견코드 : " + CType(mfrmCur, FDF11).txtCmtCd.Text + ", "
            sMsg += "소견내용 : " + CType(mfrmCur, FDF11).txtCmtCont.Text + vbCrLf + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf Me.rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF11).fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 소견정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Cmt()
                    ElseIf Me.rdoWorkOpt2.Checked Then
                        MsgBox("해당 소견정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Critical)
                    ElseIf Me.rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Critical)
                    End If
                End If
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbReg_Eq()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF13).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "장비코드 : " + CType(mfrmCur, FDF13).txtEqCd.Text + ", "
            sMsg += "장비명   : " + CType(mfrmCur, FDF13).txtEqNm.Text + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF13).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 장비정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Eq()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 장비정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If


                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_ExLab()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF07).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "위탁기관코드 : " + CType(mfrmCur, FDF07).txtExLabCd.Text + ", "
            sMsg += "위탁기관명   : " + CType(mfrmCur, FDF07).txtExLabNm.Text + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF07).fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 위탁기관정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_ExLab()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 위탁기관정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
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

    Private Sub sbReg_OSlip()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF40).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사처방슬립코드 : " + CType(mfrmCur, FDF40).txtTOSlipCd.Text + ", "
            sMsg += "검사처방슬립명 : " + CType(mfrmCur, FDF40).txtTOSlipNm.Text + vbCrLf + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "검사처방슬립정보을 수정하시겠습니까?"
            ElseIf Me.rdoWorkOpt2.Checked Then
                sMsg += "검사처방슬립정보를 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF40).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사처방슬립정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_OSlip()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사처방슬립정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
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

    Private Sub sbReg_RstCd()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF10).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사코드 : " + CType(mfrmCur, FDF10).txtTestCd.Text + ", "
            sMsg += "검사명 : " + CType(mfrmCur, FDF10).txtTNmD.Text + vbCrLf + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "결과코드를 수정하시겠습니까?"
            ElseIf Me.rdoWorkOpt2.Checked Then
                sMsg += "결과코드를 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF10).fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 결과정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_RstCd()
                    ElseIf Me.rdoWorkOpt2.Checked Then
                        MsgBox("해당 결과정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
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
    Private Sub sbReg_Ref()
        Dim sFn As String = "sbReg_Ref"

        Try
            If CType(mfrmCur, FDF53).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "병원체코드     : " + CType(mfrmCur, FDF53).txtRefcd.Text + ", "
            sMsg += "감염병명 : " + CType(mfrmCur, FDF53).txtRefnm.Text + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF53).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사분류정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Bccls()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사분류정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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
    Private Sub sbReg_TestDoc()
        Dim sFn As String = "sbReg_Ref"

        Try
            If CType(mfrmCur, FDF54).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "병원체코드     : " + CType(mfrmCur, FDF54).txtRefcd.Text + ", "
            sMsg += "감염병명 : " + CType(mfrmCur, FDF54).txtRefnm.Text + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF54).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사분류정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Bccls()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사분류정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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
    Private Sub sbReg_Bccls()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF01).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사분류코드     : " + CType(mfrmCur, FDF01).txtBcclsCd.Text + ", "
            sMsg += "검사분류명 : " + CType(mfrmCur, FDF01).txtBcclsNm.Text + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF01).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사분류정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Bccls()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사분류정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Slip()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF02).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "부서코드 : " + CType(mfrmCur, FDF02).txtPartCd.Text + ", "
            sMsg += "분야코드 : " + CType(mfrmCur, FDF02).txtSlipCd.Text + vbCrLf
            sMsg += "부서명   : " + CType(mfrmCur, FDF02).txtPartNm.Text + vbCrLf
            sMsg += "분야명   : " + CType(mfrmCur, FDF02).txtSlipNm.Text + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF02).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 (부서)분야정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Slip()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 (부서)분야정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Spc()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF03).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검체코드 : " + CType(mfrmCur, FDF03).txtSpcCd.Text + ", "
            sMsg += "검체명   : " + CType(mfrmCur, FDF03).txtSpcNm.Text + vbCrLf + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF03).fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 검체정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Spc()
                    ElseIf Me.rdoWorkOpt2.Checked Then
                        MsgBox("해당 검체정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
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

    Private Sub sbReg_SpcGrp()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF04).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검체그룹코드 : " + CType(mfrmCur, FDF04).txtSpcGrpCd.Text + ", "
            sMsg += "검체그룹명 : " + CType(mfrmCur, FDF04).txtSpcGrpNm.Text + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF04).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검체그룹정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_SpcGrp()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검체그룹정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If


                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Tube()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF06).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "용기코드     : " + CType(mfrmCur, FDF06).txtTubeCd.Text + ", "
            sMsg += "용기명       : " + CType(mfrmCur, FDF06).txtTubeNm.Text + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF06).fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 용기정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Tube()
                    ElseIf Me.rdoWorkOpt2.Checked Then
                        MsgBox("해당 용기정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
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

    Private Sub sbReg_TGrp()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF09).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사그룹코드 : " + CType(mfrmCur, FDF09).txtTGrpCd.Text + ", "
            sMsg += "검사그룹명 : " + CType(mfrmCur, FDF09).txtTGrpNm.Text + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF09).fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사그룹정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_TGrp()
                    ElseIf Me.rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사그룹정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                    'sbUpdateCdList_TGrp()
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

    Private Sub sbReg_Tla()
        'Dim sFn As String = "sbReg_"

        'Try
        '    If CType(mfrmCur, FDF14).fnValidate() = False Then Exit Sub

        '    Dim sMsg As String = "자동화라인코드 : " + Ctrl.Get_Code(CType(mfrmCur, FDF14).cboTlaCd) + ", "
        '    sMsg += "자동화라인부코드 : " + CType(mfrmCur, FDF14).txtEqTlaCd.Text + vbCrLf + vbCrLf

        '    If rbnWorkOpt1.Checked Then
        '        sMsg += "을(를) 수정하시겠습니까?"
        '    ElseIf rbnWorkOpt2.Checked Then
        '        sMsg += "을(를) 등록하시겠습니까?"
        '    End If

        '    If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
        '        If CType(mfrmCur, FDF14).fnReg() Then
        '            If rbnWorkOpt1.Checked Then
        '                MsgBox("해당 자동화라인정보가 수정되었습니다!!", MsgBoxStyle.Information)
        '            ElseIf rbnWorkOpt2.Checked Then
        '                MsgBox("해당 자동화라인정보가 등록되었습니다!!", MsgBoxStyle.Information)
        '            End If

        '            sbUpdateCdList_Tla()
        '        Else
        '            If rbnWorkOpt1.Checked Then
        '                MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Critical)
        '            ElseIf rbnWorkOpt2.Checked Then
        '                MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Critical)
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    Fn.log(msFile + sFn, Err)
        '    MsgBox(msFile + sFn + vbCrLf + ex.Message)
        'End Try
    End Sub

    Private Sub sbReg_Usr()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF00).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "사용자ID : " + CType(mfrmCur, FDF00).txtUsrID.Text + ", "
            sMsg += "사용자명 : " + CType(mfrmCur, FDF00).txtUsrNm.Text + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF00).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 사용자정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Usr()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 사용자정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_WkGrp()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF05).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "작업그룹코드 : " + CType(mfrmCur, FDF05).txtWkGrpCd.Text + ", "
            sMsg += "작업그룹명 : " + CType(mfrmCur, FDF05).txtWkGrpNm.Text + vbCrLf + vbCrLf

            If Me.rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF05).fnReg() Then
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("해당 작업그룹정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_WkGrp()
                    ElseIf Me.rdoWorkOpt2.Checked Then
                        MsgBox("해당 작업그룹정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If Me.rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Critical)
                    ElseIf Me.rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Critical)
                    End If
                End If
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbReg_VCmt()
        Dim sFn As String = "Sub sbReg_VCmt()"
        Dim sMsg As String = ""

        Try
            sMsg = "코드 : " + CType(mfrmCur, FDF48).txtCdSeq.Text + ", "

            If rdoWorkOpt1.Checked Then
                sMsg += " 을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += " 을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF48).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF48).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 기타코드 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_VCmt()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 기타코드 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_VCmt_Tcls()
        Dim sFn As String = "Sub sbReg_VCmt_Tcls()"
        Dim sMsg As String = ""

        Try
            sMsg = "종합정보 검사항목 소견설정"

            If rdoWorkOpt1.Checked Then
                sMsg += " 을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += " 을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF49).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF49).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 기타코드 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_VCmt_Tcls()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 기타코드 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_VCmt_Doctor()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF50).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "의사코드 : " + CType(mfrmCur, FDF50).txtDoctorCd.Text + ", "
            sMsg += "의사명   : " + CType(mfrmCur, FDF50).txtDoctorNm.Text + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF50).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검체정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_VCmt_Doctor()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검체정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Alert_Rule()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF51).fnValidate() = False Then Exit Sub

            Dim strCode As String = Ctrl.Get_Code(CType(mfrmCur, FDF51).cboTestCd)
            Dim strName As String = CType(mfrmCur, FDF51).cboTestCd.Text

            strName = strName.Substring(strName.IndexOf("]") + 1).Trim


            Dim sMsg As String = "검사코드: " + strCode + "," + vbCrLf + "검 사 명: " + strName + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF51).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사의 Alert Rule 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Alert_Rule()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사의 Alert Rule 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
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

#End Region

#Region " sbReg_% 미생물 "
    Private Sub sbReg_Anti()
        Dim sFn As String = "Private Sub sbReg_Anti()"
        Dim sMsg As String = ""

        Try
            sMsg = "항균제코드 : " + CType(mfrmCur, FDF17).txtAntiCd.Text + ", "
            sMsg += "항균제명 : " + CType(mfrmCur, FDF17).txtAntiNm.Text

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF17).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF17).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 항균제정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Anti()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 항균제정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Bac()
        Dim sFn As String = "Private Sub sbReg_Bac()"
        Dim sMsg As String = ""

        Try
            sMsg = "배양균코드 : " + CType(mfrmCur, FDF16).txtBacCd.Text + ", "
            sMsg += "배양균명 : " + CType(mfrmCur, FDF16).txtBacNm.Text

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF16).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF16).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 배양균정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Bac()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 배양균정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Bac_Rst()
        Dim sFn As String = "Private sub sbReg_Bac_Rst()"
        Dim sMsg As String = ""

        Try
            sMsg = "균 결과 코드 : " + CType(mfrmCur, FDF19).txtIncCd.Text + ", "
            sMsg += "균 결과 내용 : " + CType(mfrmCur, FDF19).txtIncNm.Text

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF19).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF19).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 배양균정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Bac_Rst()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 배양균정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Bacgen()
        Dim sFn As String = "Private Sub sbReg_Bacgen()"
        Dim sMsg As String = ""

        Try
            sMsg = "배양균속코드 : " + CType(mfrmCur, FDF15).txtBacgenCd.Text + ", "
            sMsg += "배양균속명 : " + CType(mfrmCur, FDF15).txtBacgenNm.Text

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF15).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF15).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 배양균속정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_Bacgen()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 배양균속정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_Cult()
        Dim sFn As String = "Private Sub sbReg_BacgenAntiList()"
        Dim sMsg As String = ""

        Try
            sMsg = "배지명 : " + CType(mfrmCur, FDF52).txtCultNm.Text + ", "
            sMsg += "검사코드 : " + CType(mfrmCur, FDF52).txtTestCd.Text + " "
            'sMsg += "검체코드 : " + CType(mfrmCur, FDF52).txtSelSpc.Text

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF52).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF52).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 배지정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        'sbUpdateCdList_BacgenAnti()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 배지정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_BacgenAntiList()
        Dim sFn As String = "Private Sub sbReg_BacgenAntiList()"
        Dim sMsg As String = ""

        Try
            sMsg = "균속코드 : " + Ctrl.Get_Code(CType(mfrmCur, FDF18).cboBacgen) + ", "
            sMsg += "항균제검사방법 : " + Ctrl.Get_Item(CType(mfrmCur, FDF18).cboTestMtd)

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF18).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF18).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 배양균속별 항균제정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_BacgenAnti()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 배양균속별 항균제정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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
#End Region

#Region " sbReg_% 특수검사"
    Private Sub sbReg_SpTest()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF20).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사코드 : " + CType(mfrmCur, FDF20).txtTestCd.Text + ", "
            sMsg += "검사명 : " + CType(mfrmCur, FDF20).txtTNmD.Text + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "특수검사 보고서를 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "특수검사 보고서를 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF20).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 특수검사 보고서가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_SpTest()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 특수검사 보고서가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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
#End Region

#Region " sbReg_% 특수검사 소견"
    Private Sub sbReg_SpCmtTest()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDF21).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "검사코드 : " + CType(mfrmCur, FDF21).txtTestcd.Text + ", "
            sMsg += "소견코드 : " + CType(mfrmCur, FDF21).txtCmtseq.Text + vbCrLf + vbCrLf
            'sMsg += "검사명 : " + CType(mfrmCur, FDF21).txtTNmD.Text + vbCrLf + vbCrLf

            If rdoWorkOpt1.Checked Then
                sMsg += "특수검사 소견을 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "특수검사 소견을 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF21).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 특수검사 소견이 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_SpTest_Cmt()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 특수검사 소견이 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
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
#End Region

#Region " sbReg_% 혈액은행 "
    Private Sub sbReg_ComCd()
        Dim sFn As String = "Sub sbReg_ComCd()"
        Dim sMsg As String = ""

        Try
            sMsg = "성분제제코드 : " + CType(mfrmCur, FDF30).txtComCd.Text + ", "
            sMsg += "성분제제구분 : " + Ctrl.Get_Code(CType(mfrmCur, FDF30).cboSpcCd) + ", "
            sMsg += "성분제제명 : " + CType(mfrmCur, FDF30).txtComNm.Text

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF30).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF30).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 성분제제 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_ComCd()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 성분제제 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
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

    Private Sub sbReg_FtCd()
        Dim sFn As String = "Sub sbReg_FtCd()"
        Dim sMsg As String = ""

        Try
            sMsg = "필터코드 : " + CType(mfrmCur, FDF31).txtFTCd.Text + ", "
            sMsg += "필터명 : " + CType(mfrmCur, FDF31).txtFTNm.Text

            If rdoWorkOpt1.Checked Then
                sMsg += "을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += "을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF31).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF31).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 필터정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_FtCd()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 필터정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbReg_JobCd()
        Dim sFn As String = "Sub sbReg_DisCd()"
        Dim sMsg As String = ""

        Try
            sMsg = "직업코드 : " + CType(mfrmCur, FDF32).txtJobCd.Text + ", "
            sMsg += "직업명 : " + CType(mfrmCur, FDF32).txtJobNm.Text

            If rdoWorkOpt1.Checked Then
                sMsg += " 을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += " 을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF32).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF32).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 직업(헌혈) 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_JobCd()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 직업(헌혈) 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_DisCd()
        Dim sFn As String = "Sub sbReg_DisCd()"
        Dim sMsg As String = ""

        Try
            sMsg = "부적격코드 : " + CType(mfrmCur, FDF33).txtDisCd.Text + ", "
            sMsg += "부적격사유 : " + CType(mfrmCur, FDF33).txtDisRsn.Text

            If rdoWorkOpt1.Checked Then
                sMsg += " 을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += " 을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF33).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF33).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 부적격사유(헌혈) 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_DisCd()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 부적격사유(헌혈) 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_RtnCd()
        Dim sFn As String = "Sub sbReg_RtnCd()"
        Dim sMsg As String = ""

        Try
            sMsg = ""
            sMsg += "반납폐기 " + "사유구분 : " + Ctrl.Get_Item(CType(mfrmCur, FDF34).cboCmtGbn) + ", " + vbCrLf
            sMsg += "사유코드 : " + CType(mfrmCur, FDF34).txtCmtCd.Text + ", "
            sMsg += "사유명 : " + CType(mfrmCur, FDF34).txtCmtCont.Text

            If rdoWorkOpt1.Checked Then
                sMsg += " 을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += " 을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF34).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF34).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 반납폐기사유(수혈) 정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_RtnCd()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 반납폐기사유(수혈) 정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If

                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbReg_BldRef()
        Dim sFn As String = "Sub sbReg_BldRef()"
        Dim sMsg As String = ""

        Try
            sMsg = ""
            sMsg += "혈액은행 관련검사"

            If rdoWorkOpt1.Checked Then
                sMsg += " 을(를) 수정하시겠습니까?"
            ElseIf rdoWorkOpt2.Checked Then
                sMsg += " 을(를) 등록하시겠습니까?"
            End If

            If CType(mfrmCur, FDF35).fnValidate() = False Then
                Exit Sub
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDF35).fnReg() Then
                    If rdoWorkOpt1.Checked Then
                        MsgBox("해당 검사구분별 검사정보가 수정되었습니다!!", MsgBoxStyle.Information)
                        sbUpdateCdList_BldRef()
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("해당 검사구분별 검사정보가 등록되었습니다!!", MsgBoxStyle.Information)
                        sbDisplayCdList(msMstGbn)
                    End If
                Else
                    If rdoWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Information)
                    ElseIf rdoWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Information)
                    End If
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

#End Region

#Region " sbReloadRightArea 선언 "
    Private Sub sbReloadRightArea(ByVal asBuf As String)
        Dim sFn As String = "Private Sub sbReloadRightArea(ByVal asBuf As String)"

        Try
            Select Case asBuf
                Case mcFDF00            'USR
                    mfrmCur = New FDF00
                Case mcFDF01            'SECTION/TSECTION
                    mfrmCur = New FDF01
                Case mcFDF02            'PART/SLIP
                    mfrmCur = New FDF02
                Case mcFDF03            'SPC
                    mfrmCur = New FDF03
                Case mcFDF04            'SPCGRP
                    mfrmCur = New FDF04
                Case mcFDF05            'WKGRP
                    mfrmCur = New FDF05
                Case mcFDF06            'TUBE
                    mfrmCur = New FDF06
                Case mcFDF07            'EXLAB
                    mfrmCur = New FDF07
                Case mcFDF09            'TGRP
                    mfrmCur = New FDF09
                Case mcFDF10            'RSTCD
                    mfrmCur = New FDF10
                Case mcFDF11            'CMT
                    mfrmCur = New FDF11
                Case mcFDF12            'CALC
                    mfrmCur = New FDF12
                Case mcFDF13            'EQ
                    mfrmCur = New FDF13
                Case mcFDF40            'OSLIP
                    mfrmCur = New FDF40
                Case mcFDF41            'KSRACK
                    mfrmCur = New FDF41

                Case mcFDF15            'BACGEN
                    mfrmCur = New FDF15
                Case mcFDF16            'BAC
                    mfrmCur = New FDF16
                Case mcFDF17            'ANTI
                    mfrmCur = New FDF17
                Case mcFDF18            'BACGEN_ANTI
                    mfrmCur = New FDF18
                Case mcFDF19            'BAC_RST
                    mfrmCur = New FDF19

                Case mcFDF20            'SPTEST
                    mfrmCur = New FDF20

                Case mcFDF21            '특수보고서 소견
                    mfrmCur = New FDF21

                Case mcFDF30            'COM
                    mfrmCur = New FDF30
                Case mcFDF31            'FILTER
                    mfrmCur = New FDF31
                Case mcFDF32            'JOB
                    mfrmCur = New FDF32
                Case mcFDF33            'DISCD
                    mfrmCur = New FDF33
                Case mcFDF34            'RTNCD
                    mfrmCur = New FDF34
                Case mcFDF35            'BDTEST
                    mfrmCur = New FDF35
                Case mcFDF42
                    mfrmCur = New FDF42

                Case mcFDF43
                    mfrmCur = New FDF43

                Case mcFDF44
                    mfrmCur = New FDF44

                Case mcFDF45
                    mfrmCur = New FDF45

                Case mcFDF46
                    mfrmCur = New FDF46

                Case mcFDF47
                    mfrmCur = New FDF47

                Case mcFDF48
                    mfrmCur = New FDF48("CMT")

                Case mcFDF49
                    mfrmCur = New FDF49

                Case mcFDF50
                    mfrmCur = New FDF50

                Case mcFDF51
                    mfrmCur = New FDF51

                Case mcFDF52
                    mfrmCur = New FDF52

                Case mcFDF53
                    mfrmCur = New FDF53

                Case mcFDF54
                    mfrmCur = New FDF54
                Case Else

            End Select

            If IsNothing(mfrmCur) Then Exit Sub

            mfrmCur.ShowInTaskbar = False
            mfrmCur.StartPosition = Windows.Forms.FormStartPosition.Manual
            mfrmCur.FormBorderStyle = Windows.Forms.FormBorderStyle.None

            sbResizeRightArea()

            Me.AddOwnedForm(mfrmCur)

            mfrmCur.Show()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

    Private Sub sbRelocation()
        Dim sFn As String = "sbRelocation"

        Try
            If (Me.ParentForm.DesktopLocation.X + Me.DesktopLocation.X + Me.Size.Width) > _
               (Me.ParentForm.DesktopLocation.X + Me.ParentForm.Size.Width - miParentGapX) Then
                Me.Location = New System.Drawing.Point(Me.Location.X - _
                                                       ((Me.ParentForm.DesktopLocation.X + Me.DesktopLocation.X + Me.Size.Width) - _
                                                        (Me.ParentForm.DesktopLocation.X + Me.ParentForm.Size.Width - miParentGapX)), _
                                                       Me.Location.Y)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbResizeLeftArea(ByVal iFrmWidth As Integer)
        Dim sFn As String = "sbResizeLeftArea"

        Try
            miWidth = iFrmWidth - 1024

            If miWidth < 0 Then miWidth = 0

            pnlLeft.Size = New System.Drawing.Size(miFirstWidth_pnlLeft + miWidth, pnlLeft.Size.Height)
            btnBack.Location = New System.Drawing.Point(miFirstWidth_pnlLeft + 1 + miWidth, btnBack.Location.Y)
            splSpl.MinSize = miFirstWidth_pnlLeft + miWidth
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbResizeRightArea()
        Dim sFn As String = "sbResizeRightArea"

        Try
            'Window 와 Control 사이의 시각적인 Gap
            Dim iGap As Integer = Convert.ToInt32((Me.Size.Width - Me.ClientSize.Width) / 2)

            'Window Title Bar Height
            Dim iWndTitleHeight As Integer = Me.Size.Height - Me.ClientSize.Height - iGap

            If Not IsNothing(mfrmCur) Then
                If miMDIChild = 0 Then
                    mfrmCur.Location = New System.Drawing.Point(Me.DesktopLocation.X + iGap + _
                                                            pnlRight.Location.X, _
                                                            Me.DesktopLocation.Y + iWndTitleHeight + _
                                                            pnlRight.Location.Y)
                Else
                    mfrmCur.Location = New System.Drawing.Point(Me.ParentForm.DesktopLocation.X + _
                                                            Me.DesktopLocation.X + iGap + _
                                                            pnlRight.Location.X + miParentGapX, _
                                                            Me.ParentForm.DesktopLocation.Y + _
                                                            Me.DesktopLocation.Y + iWndTitleHeight + _
                                                            pnlRight.Location.Y + miParentGapY)
                End If

                mfrmCur.Size = New System.Drawing.Size(Me.Size.Width - pnlLeft.Size.Width - btnBack.Size.Width - miParentGapX, pnlRight.Size.Height)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbRestore()
        If Not IsNothing(mfrmCur) Then
            mfrmCur.Show()
        End If
    End Sub

#Region " sbSetColumnInfo_% 일반검사, 공통 "

    Private Sub sbSetColumnInfo_DComCd()
        Dim sFn As String = "Private Sub sbSetColumnInfo_DComCd()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "slipcd" : .set_ColWidth(.GetColFromID("slipcd"), 7 / 5 * 10 + 1)
                .Col = 2 : .Text = "검사분야명" : .ColID = "slipnmd" : .set_ColWidth(.GetColFromID("slipnmd"), 25 / 5 * 10 + 1)
                .Col = 3 : .Text = "등록자 ID" : .ColID = "regid" : .ColHidden = True : .set_ColWidth(.GetColFromID("regid"), 4 / 5 * 12 + 1)
                .Col = 4 : .Text = "등록일" : .ColID = "regdt" : .ColHidden = True
                .Col = 5 : .Text = "modid" : .ColID = "modid" : .ColHidden = True
                .Col = 6 : .Text = "moddt" : .ColID = "moddt" : .ColHidden = True
                .Col = 7 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_KeyPad()
        Dim sFn As String = "Private Sub sbSetColumnInfo_KeyPad()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "testspc" : .set_ColWidth(.GetColFromID("testspc"), 7 / 5 * 10 + 1)
                .Col = 2 : .Text = "검사명" : .ColID = "tnmd" : .set_ColWidth(.GetColFromID("tnmd"), 25 / 5 * 10 + 1)
                .Col = 3 : .Text = "Keypad 폼" : .ColID = "formgbn" : .set_ColWidth(.GetColFromID("formgbn"), 7 / 5 * 10 + 1)
                .Col = 4 : .Text = "testcd" : .ColID = "testcd" : .ColHidden = True
                .Col = 5 : .Text = "spccd" : .ColID = "spccd" : .ColHidden = True
                .Col = 6 : .Text = "moddt" : .ColID = "moddt" : .ColHidden = True
                .Col = 7 : .Text = "modid" : .ColID = "modid" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_Cvt_RST()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Cvt_RST()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 12
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "검사코드" : .ColID = "testspc" : .set_ColWidth(.GetColFromID("testspc"), 7 / 5 * 10 + 1)
                .Col = 2 : .Text = "검사명" : .ColID = "tnmd" : .set_ColWidth(.GetColFromID("TNMD"), 25 / 5 * 10 + 1)
                .Col = 3 : .Text = "단축키" : .ColID = "keypad" : .set_ColWidth(.GetColFromID("KEYPAD"), 7 / 5 * 10 + 1)
                .Col = 4 : .Text = "계산식" : .ColID = "cvtform" : .set_ColWidth(.GetColFromID("cvtform"), 4 / 5 * 12 + 1)
                .Col = 5 : .Text = "결과구분" : .ColID = "cvtfldgbn" : .set_ColWidth(.GetColFromID("cvtfldgbn"), 4 / 5 * 12 + 1)
                .Col = 6 : .Text = "적용구분" : .ColID = "cvtrange" : .set_ColWidth(.GetColFromID("cvtrange"), 4 / 5 * 12 + 1)
                .Col = 7 : .Text = "결과내용" : .ColID = "rstcont" : .set_ColWidth(.GetColFromID("rstcont"), 4 / 5 * 12 + 1)
                .Col = 8 : .Text = "TESTCD" : .ColID = "testcd" : .ColHidden = True
                .Col = 9 : .Text = "SPCCD" : .ColID = "spccd" : .ColHidden = True
                .Col = 10 : .Text = "RSTCD" : .ColID = "rstcdseq" : .ColHidden = True
                .Col = 11 : .Text = "변경삭제일시" : .ColID = "moddt" : .ColHidden = True
                .Col = 12 : .Text = "변경삭제자ID" : .ColID = "modid" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_Cvt_CMT()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Cvt_CMT()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "소견코드" : .ColID = "cmtcd" : .set_ColWidth(.GetColFromID("cmtcd"), 7 / 5 * 10 + 1)
                .Col = 2 : .Text = "계산식" : .ColID = "cvtform" : .set_ColWidth(.GetColFromID("cvtform"), 4 / 5 * 12 + 1)
                .Col = 3 : .Text = "소견내용" : .ColID = "cmtcont" : .set_ColWidth(.GetColFromID("cmtcont"), 4 / 5 * 12 + 1)
                .Col = 4 : .Text = "moddt" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 0) : .ColHidden = True
                .Col = 5 : .Text = "modid" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 0) : .ColHidden = True


                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_KSRack()
        Dim sFn As String = "Private Sub sbSetColumnInfo_KSRack()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 10
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "검체분류 코드" : .ColID = "BCCLSCD" : .set_ColWidth(.GetColFromID("BCCLSCD"), 7 / 5 * 10 + 1)
                .Col = 2 : .Text = "Rack ID" : .ColID = "RACKID" : .set_ColWidth(.GetColFromID("RACKID"), 25 / 5 * 10 + 1)
                .Col = 3 : .Text = "검체분류 명" : .ColID = "BCCLSNMD" : .set_ColWidth(.GetColFromID("BCCLSNMD"), 7 / 5 * 10 + 1)
                .Col = 4 : .Text = "경고기간" : .ColID = "ALARMTERM" : .set_ColWidth(.GetColFromID("ALARMTERM"), 4 / 5 * 12 + 1)
                .Col = 5 : .Text = "Max Col" : .ColID = "MAXCOL" : .set_ColWidth(.GetColFromID("MAXCOL"), 4 / 5 * 12 + 1)
                .Col = 6 : .Text = "Max Row" : .ColID = "MAXROW" : .set_ColWidth(.GetColFromID("MAXROW"), 4 / 5 * 12 + 1)
                .Col = 7 : .Text = "등록자 ID" : .ColID = "REGID" : .set_ColWidth(.GetColFromID("REGID"), 4 / 5 * 12 + 1)
                .Col = 8 : .Text = "등록일" : .ColID = "REGDT" : .set_ColWidth(.GetColFromID("REGDT"), 4 / 5 * 12 + 1)
                .Col = 9 : .Text = "변경삭제일시" : .ColHidden = True : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 0)
                .Col = 10 : .Text = "변경삭제자ID" : .ColHidden = True : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 0)

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_Calc()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Calc()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "검사코드" : .ColID = "testcd" : .set_ColWidth(.GetColFromID("testcd"), 4 / 5 * 10 + 1)
                .Col = 2 : .Text = "검체코드" : .ColID = "spccd" : .set_ColWidth(.GetColFromID("spccd"), 4 / 5 * 10 + 1)
                .Col = 3 : .Text = "검사명" : .ColID = "tnmd" : .set_ColWidth(.GetColFromID("tnmd"), 4 / 5 * 50 + 1)
                .Col = 4 : .Text = "검체명" : .ColID = "spcnmd" : .set_ColWidth(.GetColFromID("spcnmd"), 4 / 5 * 20 + 1)
                .Col = 5 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 4 / 5 * 12 + 1)
                .Col = 6 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 4 / 5 * 12 + 1)
                .Col = 7 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSetColumnInfo_Cmt()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Cmt()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 6
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "소견코드" : .ColID = "cmtcd" : .set_ColWidth(.GetColFromID("cmtcd"), 4 / 5 * 10 + 1)
                .Col = 2 : .Text = "소견내용" : .ColID = "cmtcont" : .set_ColWidth(.GetColFromID("cmtcont"), 4 / 5 * 50 + 1)
                .Col = 3 : .Text = "검사분야" : .ColID = "slipnmd_01" : .set_ColWidth(.GetColFromID("slipnmd_01"), 4 / 5 * 20 + 1)
                .Col = 4 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 4 / 5 * 12 + 1)
                .Col = 5 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 4 / 5 * 12 + 1)
                .Col = 6 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSetColumnInfo_Eq()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Eq()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 4
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "장비코드" : .ColID = "eqcd" : .set_ColWidth(.GetColFromID("eqcd"), 10)
                .Col = 2 : .Text = "장비명" : .ColID = "eqnms" : .set_ColWidth(.GetColFromID("eqnms"), 30)
                .Col = 3 : .Text = "DELFLG" : .ColID = "delflg" : .ColHidden = True
                .Col = 4 : .Text = "사용여부" : .ColID = "useyn" : .set_ColWidth(.GetColFromID("useyn"), 20)

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_ExLab()
        Dim sFn As String = "Private Sub sbSetColumnInfo_ExLab()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 4
                .MaxRows = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "exlabcd" : .set_ColWidth(.GetColFromID("exlabcd"), 10)
                .Col = 2 : .Text = "위탁기관명" : .ColID = "exlabnmd" : .set_ColWidth(.GetColFromID("exlabnmd"), 30)
                .Col = 3 : .Text = "DELFLG" : .ColID = "delflg" : .ColHidden = True
                .Col = 4 : .Text = "사용여부" : .ColID = "useyn" : .set_ColWidth(.GetColFromID("useyn"), 20)

                .ReDraw = True


            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_OSlip()
        Dim sFn As String = "Private Sub sbSetColumnInfo_OSlip()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 6
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "tordslip" : .set_ColWidth(.GetColFromID("tordslip"), 10)
                .Col = 2 : .Text = "검사처방슬립명" : .ColID = "tordslipnm" : .set_ColWidth(.GetColFromID("tordslipnm"), 30)
                .Col = 3 : .Text = "변경삭제일시" : .ColID = "moddt" : .ColHidden = True
                .Col = 4 : .Text = "변경삭제자ID" : .ColID = "modid" : .ColHidden = True
                .Col = 5 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True
                .Col = 6 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True : .set_ColWidth(.GetColFromID("usdt"), 16)


                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_RstCd()
        Dim sFn As String = "Private Sub sbSetColumnInfo_RstCd()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "검사코드" : .ColID = "testcd" : .set_ColWidth(.GetColFromID("TESTCD"), 10)
                .Col = 2 : .Text = "검사명" : .ColID = "tnmd" : .set_ColWidth(.GetColFromID("tnmd"), 30)
                .Col = 3 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True
                .Col = 4 : .Text = "moddt" : .ColID = "moddt" : .ColHidden = True
                .Col = 5 : .Text = "modid" : .ColID = "modid" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_Bccls()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Bccls()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "bcclscd" : .set_ColWidth(.GetColFromID("bcclscd"), 8)
                .Col = 2 : .Text = "검체분류명" : .ColID = "bcclsnmd" : .set_ColWidth(.GetColFromID("bcclsnmd"), 20)
                .Col = 3 : .Text = "B" : .ColID = "bcclsnmbp" : .set_ColWidth(.GetColFromID("bcclsnmbp"), 3)
                .Col = 4 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 5 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_Slip()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Slip()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "" : .ColID = "slipcd" : .set_ColWidth(.GetColFromID("slipcd"), 4 / 5 * 2.5)
                .Col = 2 : .Text = "분야명" : .ColID = "slipnmd" : .set_ColWidth(.GetColFromID("slipnmd"), 4 / 5 * 10)
                .Col = 3 : .Text = "부서명" : .ColID = "partnmd" : .set_ColWidth(.GetColFromID("partnmd"), 4 / 5 * 10)
                .Col = 4 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 5 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_Spc()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Spc()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "spccd" : .set_ColWidth(.GetColFromID("spccd"), 10)
                .Col = 2 : .Text = "검체명" : .ColID = "spcnmd" : .set_ColWidth(.GetColFromID("spcnmd"), 30)
                .Col = 3 : .Text = "IF" : .ColID = "spcifcd" : .set_ColWidth(.GetColFromID("spcifcd"), 10)
                .Col = 4 : .Text = "whonet" : .ColID = "whonet" : .set_ColWidth(.GetColFromID("whonet"), 10)
                .Col = 5 : .Text = "O" : .ColID = "reqcmt" : .ColHidden = True : .set_ColWidth(.GetColFromID("reqcmt"), 3)
                .Col = 6 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 7 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_SpcGrp()
        Dim sFn As String = "Private Sub sbSetColumnInfo_SpcGrp()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 2
                .MaxRows = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "SPCGRPCD" : .set_ColWidth(.GetColFromID("SPCGRPCD"), 4 / 5 * 5)
                .Col = 2 : .Text = "검체그룹명" : .ColID = "SPCGRPNMD" : .set_ColWidth(.GetColFromID("SPCGRPNMD"), 4 / 5 * 20)
                '.Col = 3 : .Text = "USDT" : .ColID = "USDT" : .ColHidden = True
                '.Col = 4 : .Text = "UEDT" : .ColID = "UEDT" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_TGrp()
        Dim sFn As String = "Private Sub sbSetColumnInfo_TGrp()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "tgrpcd" : .set_ColWidth(.GetColFromID("tgrpcd"), 10)
                .Col = 2 : .Text = "검사그룹명" : .ColID = "tgrpnmd" : .set_ColWidth(.GetColFromID("tgrpnmd"), 20)
                .Col = 3 : .Text = "변경일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 18)
                .Col = 4 : .Text = "변경자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 10)
                .Col = 5 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_Tube()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Tube()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "tubecd" : .set_ColWidth(.GetColFromID("tubecd"), 6)
                .Col = 2 : .Text = "용기명" : .ColID = "tubenmd" : .set_ColWidth(.GetColFromID("tubenmd"), 15)
                .Col = 3 : .Text = "VOL" : .ColID = "tubevol" : .set_ColWidth(.GetColFromID("tubevol"), 10)
                .Col = 4 : .Text = "UNIT" : .ColID = "tubeunit" : .set_ColWidth(.GetColFromID("tubeunit"), 10)
                .Col = 5 : .Text = "IF" : .ColID = "tubeifcd" : .set_ColWidth(.GetColFromID("tubeifcd"), 8)
                .Col = 6 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 7 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_Usr()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Usr()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "사용자ID" : .ColID = "usrid" : .set_ColWidth(.GetColFromID("usrid"), 4 / 5 * 10)
                .Col = 2 : .Text = "사용자명" : .ColID = "usrnm" : .set_ColWidth(.GetColFromID("usrnm"), 4 / 5 * 20)
                .Col = 3 : .Text = "레벨" : .ColID = "usrlvl" : .set_ColWidth(.GetColFromID("usrlvl"), 4 / 5 * 10)
                .Col = 4 : .Text = "상태" : .ColID = "delflg_v" : .ColHidden = True : .set_ColWidth(.GetColFromID("delflg_v"), 4 / 5 * 20)
                .Col = 5 : .Text = "사용여부" : .ColID = "useyn" : .set_ColWidth(.GetColFromID("useyn"), 20)

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_WkGrp()
        Dim sFn As String = "Private Sub sbSetColumnInfo_WkGrp()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 4
                .MaxRows = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "wkgrpcd" : .set_ColWidth(.GetColFromID("wkgrpcd"), 10)
                .Col = 2 : .Text = "작업그룹명" : .ColID = "wkgrpnmd" : .set_ColWidth(.GetColFromID("wkgrpnmd"), 30)
                .Col = 3 : .Text = "modid" : .ColHidden = True : .ColID = "modid"
                .Col = 4 : .Text = "moddt" : .ColHidden = True : .ColID = "moddt"

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_VCmt()
        Dim sFn As String = "Private Sub sbSetColumnInfo_VCmt()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 6
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "cdseq" : .set_ColWidth(.GetColFromID("cdseq"), 4 / 5 * 24 + 1)
                .Col = 2 : .Text = "타이틀" : .ColID = "cdtitle" : .set_ColWidth(.GetColFromID("cdtitle"), 4 / 5 * 10 + 1)
                .Col = 3 : .Text = "내용" : .ColID = "cdcont" : .set_ColWidth(.GetColFromID("cdcont"), 4 / 5 * 60 + 1)
                .Col = 4 : .Text = "변경삭제일시" : .ColID = "moddt" : .ColHidden = True : .set_ColWidth(.GetColFromID("moddt"), 0)
                .Col = 5 : .Text = "변경삭제자ID" : .ColID = "modid" : .ColHidden = True : .set_ColWidth(.GetColFromID("modid"), 0)
                .Col = 6 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSetColumnInfo_VCmt_Tcls()
        Dim sFn As String = "Private Sub sbSetColumnInfo_VCmt_Tcls()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "구분" : .ColID = "cdnm" : .set_ColWidth(.GetColFromID("cdnm"), 4 / 5 * 24 + 1)
                .Col = 2 : .Text = "등록일시" : .ColID = "regdt" : .set_ColWidth(.GetColFromID("regdt"), 4 / 5 * 10 + 1)
                .Col = 3 : .Text = "변경삭제일시" : .ColID = "moddt" : .ColHidden = True : .set_ColWidth(.GetColFromID("moddt"), 0)
                .Col = 4 : .Text = "변경삭제자ID" : .ColID = "modid" : .ColHidden = True : .set_ColWidth(.GetColFromID("modid"), 0)
                .Col = 5 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSetColumnInfo_VCmt_Doctor()
        Dim sFn As String = "Private Sub sbSetColumnInfo_VCmt_Doctor()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "doctorcd" : .set_ColWidth(.GetColFromID("doctorcd"), 4 / 5 * 10)
                .Col = 2 : .Text = "의사명" : .ColID = "doctornm" : .set_ColWidth(.GetColFromID("doctornm"), 4 / 5 * 30)
                .Col = 3 : .Text = "면허번호" : .ColID = "medino" : .set_ColWidth(.GetColFromID("medino"), 4 / 5 * 10)
                .Col = 4 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 5 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    '병원체 코드 마스터 추가 20170601
    Private Sub sbSetColumnInfo_refcode()
        Dim sFn As String = "Private Sub sbSetColumnInfo_refcode()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 6
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "병원체코드" : .ColID = "refcd" : .set_ColWidth(.GetColFromID("testcd"), 4 / 5 * 10)
                .Col = 2 : .Text = "감염병명" : .ColID = "refnm" : .set_ColWidth(.GetColFromID("refnm"), 4 / 5 * 30)
                .Col = 3 : .Text = "세세부명" : .ColID = "refnmd" : .set_ColWidth(.GetColFromID("refnmd"), 4 / 5 * 12 + 1)
                .Col = 4 : .Text = "그룹" : .ColID = "groupcd" : .set_ColWidth(.GetColFromID("groupcd"), 4 / 5 * 12 + 1)
                .Col = 5 : .Text = "순번" : .ColID = "seq" : .set_ColWidth(.GetColFromID("seq"), 4 / 5 * 12 + 1)
                .Col = 6 : .Text = "REGDT" : .ColID = "regdt" : .set_ColWidth(.GetColFromID("regdt"), 0) : .ColHidden = True
                .Col = 7 : .Text = "REGID" : .ColID = "regid" : .set_ColWidth(.GetColFromID("regid"), 0) : .ColHidden = True

                .ReDraw = True

            End With


        Catch ex As Exception

        End Try

    End Sub
    '검사의뢰지침서 관리 추가 20191210
    Private Sub sbSetColumnInfo_TestDoc()
        Dim sFn As String = "Private Sub sbSetColumnInfo_BldRef()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 2
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "내용" : .ColID = "nmd" : .set_ColWidth(.GetColFromID("nmd"), 4 / 5 * 10 + 1)
                .Col = 2 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub sbSetColumnInfo_Alert_Rule()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Alert_Rule()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "코드" : .ColID = "testcd" : .set_ColWidth(.GetColFromID("testcd"), 4 / 5 * 10)
                .Col = 2 : .Text = "검사명" : .ColID = "tnmd" : .set_ColWidth(.GetColFromID("tnmd"), 4 / 5 * 30)
                .Col = 3 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 4 / 5 * 12 + 1)
                .Col = 4 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 4 / 5 * 12 + 1)
                .Col = 5 : .Text = "REGDT" : .ColID = "regdt" : .set_ColWidth(.GetColFromID("regdt"), 0) : .ColHidden = True
                .Col = 6 : .Text = "REGID" : .ColID = "regid" : .set_ColWidth(.GetColFromID("regid"), 0) : .ColHidden = True
                .Col = 7 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region " sbSetColumnInfo_% 미생물 "
    Private Sub sbSetColumnInfo_Anti()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Anti()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Row = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Col = 5 : .Col2 = 5 : .Row = -1 : .Row2 = -1
                .BlockMode = True
                .CellType = FPSpreadADO.CellTypeConstants.CellTypeNumber
                .TypeNumberDecPlaces = 0
                .TypeNumberMin = 0
                .TypeNumberMax = 999
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "항균제코드" : .ColID = "anticd" : .set_ColWidth(.GetColFromID("anticd"), 4 / 5 * 6 + 1)
                .Col = 2 : .Text = "항균제명" : .ColID = "antinmd" : .set_ColWidth(.GetColFromID("antinmd"), 4 / 5 * 8 + 1)
                .Col = 3 : .Text = "IF" : .ColID = "antiifcd" : .set_ColWidth(.GetColFromID("antiifcd"), 4 / 5 * 2 + 1)
                .Col = 4 : .Text = "WHONET" : .ColID = "antiwncd" : .set_ColWidth(.GetColFromID("antiwncd"), 4 / 5 * 6 + 1)
                .Col = 5 : .Text = "정렬순서" : .ColID = "dispseq" : .set_ColWidth(.GetColFromID("dispseq"), 4 / 5 * 8 + 1)
                .Col = 6 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 7 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ReDraw = True

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_Bac()
        Dim sFn As String = "Sub sbSetColumnInfo_Bac()"

        Try
            With spdCdList
                .ReDraw = False

                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Row = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "균코드" : .ColID = "baccd" : .set_ColWidth(.GetColFromID("baccd"), 4 / 5 * 6 + 1)
                .Col = 2 : .Text = "균명" : .ColID = "bacnmd" : .set_ColWidth(.GetColFromID("bacnmd"), 4 / 5 * 4 + 1)
                .Col = 3 : .Text = "균속" : .ColID = "bacgencd" : .set_ColWidth(.GetColFromID("bacgencd"), 4 / 5 * 4 + 1)
                .Col = 4 : .Text = "IF" : .ColID = "bacifcd" : .set_ColWidth(.GetColFromID("bacifcd"), 4 / 5 * 2 + 1)
                .Col = 5 : .Text = "WHONET" : .ColID = "bacwncd" : .set_ColWidth(.GetColFromID("bacwncd"), 4 / 5 * 6 + 1)
                .Col = 6 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 7 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ColsFrozen = 2

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_Bac_Rst()
        Dim sFn As String = "Sub sbSetColumnInfo_Bac_Rst()"

        Try
            With spdCdList
                .ReDraw = False

                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Row = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "검사코드" : .ColID = "testcd" : .set_ColWidth(.GetColFromID("testcd"), 8)
                .Col = 2 : .Text = "검체코드" : .ColID = "spccd" : .set_ColWidth(.GetColFromID("spccd"), 8)
                .Col = 3 : .Text = "검사명" : .ColID = "tnmd" : .set_ColWidth(.GetColFromID("tnmd"), 20)
                .Col = 4 : .Text = "코드" : .ColID = "incrstcd" : .set_ColWidth(.GetColFromID("incrstcd"), 6)
                .Col = 5 : .Text = "내용" : .ColID = "incrstnm" : .set_ColWidth(.GetColFromID("incrstnm"), 40)
                .Col = 6 : .Text = "modid" : .ColID = "modid" : .ColHidden = True
                .Col = 7 : .Text = "moddt" : .ColID = "moddt" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_Bacgen()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Bacgen()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 6
                .MaxRows = 1000

                .Row = 0

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "균속코드" : .ColID = "bacgencd" : .set_ColWidth(.GetColFromID("bacgencd"), 10)
                .Col = 2 : .Text = "균속명" : .ColID = "bacgennmd" : .set_ColWidth(.GetColFromID("bacgennmd"), 40)
                .Col = 5 : .Text = "modid" : .ColID = "modid" : .ColHidden = True
                .Col = 6 : .Text = "moddt" : .ColID = "moddt" : .ColHidden = True

                .ReDraw = True

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_BacgenAnti()
        Dim sFn As String = "Sub sbSetColumnInfo_BacgenAnti()"

        Try
            With spdCdList
                .ReDraw = False

                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Col = 1 : .Col = .MaxCols : .Row = 1 : .Row = .MaxRows
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "균속코드" : .ColID = "bacgencd" : .set_ColWidth(.GetColFromID("bacgencd"), 10)
                .Col = 2 : .Text = "균속명" : .ColID = "bacgennmd" : .set_ColWidth(.GetColFromID("bacgennmd"), 30)
                .Col = 3 : .Text = "검사방법" : .ColID = "testmtd" : .set_ColWidth(.GetColFromID("testmtd"), 8)
                .Col = 4 : .Text = "modid" : .ColID = "modid" : .ColHidden = True
                .Col = 5 : .Text = "moddt" : .ColID = "moddt" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_Cult()
        Dim sFn As String = "Sub sbSetColumnInfo_Cult()"

        Try
            With spdCdList
                .ReDraw = False

                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col = .MaxCols : .Row = 1 : .Row = .MaxRows
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "배지명" : .ColID = "cultnm" : .set_ColWidth(.GetColFromID("cultnm"), 10)
                .Col = 2 : .Text = "검사명" : .ColID = "tnmd" : .set_ColWidth(.GetColFromID("tnmd"), 20)
                .Col = 3 : .Text = "검사코드" : .ColID = "testcd" : .set_ColWidth(.GetColFromID("testcd"), 8)
                .Col = 4 : .Text = "검체코드" : .ColID = "spccd" : .set_ColWidth(.GetColFromID("spccd"), 8)
                .Col = 5 : .Text = "시작월일" : .ColID = "usedays" : .set_ColWidth(.GetColFromID("usedays"), 8)
                .Col = 6 : .Text = "종료월일" : .ColID = "usedaye" : .set_ColWidth(.GetColFromID("usedaye"), 8)
                .Col = 7 : .Text = "modid" : .ColID = "modid" : .ColHidden = True
                .Col = 8 : .Text = "moddt" : .ColID = "moddt" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub
#End Region

#Region " sbSetColumnInfo_% 특수검사"
    Private Sub sbSetColumnInfo_SpTest()
        Dim sFn As String = "Private Sub sbSetColumnInfo_SpTest()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "검사코드" : .ColID = "testcd" : .set_ColWidth(.GetColFromID("testcd"), 10)
                .Col = 2 : .Text = "검사명" : .ColID = "tnmd_01" : .set_ColWidth(.GetColFromID("tnmd_01"), 20)
                .Col = 3 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 18)
                .Col = 4 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 10)
                .Col = 5 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbSetColumnInfo_% 특수검사 소견"
    Private Sub sbSetColumnInfo_SpTest_Cmt()
        Dim sFn As String = "Private Sub sbSetColumnInfo_SpTest_Cmt()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 6
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "검사코드" : .ColID = "testcd" : .set_ColWidth(.GetColFromID("testcd"), 10)
                .Col = 2 : .Text = "SEQ" : .ColID = "cmtseq" : .set_ColWidth(.GetColFromID("cmtseq"), 5)
                .Col = 3 : .Text = "소견" : .ColID = "cmtcont" : .set_ColWidth(.GetColFromID("cmtcont"), 30)
                .Col = 4 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 18)
                .Col = 5 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 10)
                .Col = 6 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbSetColumnInfo_% 혈액은행 "
    Private Sub sbSetColumnInfo_ComCd()
        Dim sFn As String = "Sub sbSetColumnInfo_ComCd()"

        Try
            With spdCdList
                .ReDraw = False

                .MaxCols = 0
                .MaxCols = 6
                .MaxRows = 1000

                .Row = 0

                .Col = 1 : .Text = "성분제제코드" : .ColID = "comcd" : .set_ColWidth(.GetColFromID("comcd"), 4 / 5 * 10)
                .Col = 2 : .Text = "검체코드" : .ColID = "spccd" : .set_ColWidth(.GetColFromID("spccd"), 4 / 5 * 3)
                .Col = 3 : .Text = "성분제제명" : .ColID = "comnmd" : .set_ColWidth(.GetColFromID("comnmd"), 4 / 5 * 20)
                .Col = 4 : .Text = "처방코드" : .ColID = "comordcd" : .set_ColWidth(.GetColFromID("comordcd"), 4 / 5 * 20)
                .Col = 5 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 6 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_FtCd()
        Dim sFn As String = "Sub sbSetColumnInfo_FtCd()"

        Try
            With spdCdList
                .ReDraw = False

                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Row = 0

                .Col = 1 : .Text = "필터코드" : .ColID = "ftcd" : .set_ColWidth(.GetColFromID("ftcd"), 10)
                .Col = 2 : .Text = "필터명" : .ColID = "ftnms" : .set_ColWidth(.GetColFromID("ftnms"), 30)
                .Col = 3 : .Text = "처방코드" : .ColID = "fordcd" : .set_ColWidth(.GetColFromID("fordcd"), 10)
                .Col = 4 : .Text = "USDT" : .ColID = "usdt" : .ColHidden = True
                .Col = 5 : .Text = "UEDT" : .ColID = "uedt" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbSetColumnInfo_JobCd()
        Dim sFn As String = "Private Sub sbSetColumnInfo_DisCd()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "직업코드" : .ColID = "jobcd" : .set_ColWidth(.GetColFromID("jobcd"), 4 / 5 * 10 + 1)
                .Col = 2 : .Text = "직업명" : .ColID = "jobnm" : .set_ColWidth(.GetColFromID("jobnm"), 4 / 5 * 10 + 1)
                .Col = 3 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 4 / 5 * 12 + 1)
                .Col = 4 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 4 / 5 * 12 + 1)
                .Col = 5 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSetColumnInfo_DisCd()
        Dim sFn As String = "Private Sub sbSetColumnInfo_DisCd()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 5
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "부적격사유코드" : .ColID = "discd" : .set_ColWidth(.GetColFromID("DISCD"), 10)
                .Col = 2 : .Text = "부적격사유명" : .ColID = "disrsn" : .set_ColWidth(.GetColFromID("DISRSN"), 40)
                .Col = 3 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 14)
                .Col = 4 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 10)
                .Col = 5 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSetColumnInfo_RtnCd()
        Dim sFn As String = "Private Sub sbSetColumnInfo_RtnCd()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 7
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "반납폐기사유구분" : .ColID = "cmtgbn_01" : .set_ColWidth(.GetColFromID("cmtgbn_01"), 4 / 5 * 24 + 1)
                .Col = 2 : .Text = "사유코드" : .ColID = "cmtcd" : .set_ColWidth(.GetColFromID("cmtcd"), 4 / 5 * 10 + 1)
                .Col = 3 : .Text = "사유명" : .ColID = "cmtcont" : .set_ColWidth(.GetColFromID("cmtcont"), 4 / 5 * 60 + 1)
                .Col = 4 : .Text = "수혈중단" : .ColID = "stopgbn" : .set_ColWidth(.GetColFromID("stopgbn"), 4 / 5 * 10 + 1)
                .Col = 5 : .Text = "변경삭제일시" : .ColID = "moddt" : .set_ColWidth(.GetColFromID("moddt"), 4 / 5 * 12 + 1)
                .Col = 6 : .Text = "변경삭제자ID" : .ColID = "modid" : .set_ColWidth(.GetColFromID("modid"), 4 / 5 * 12 + 1)
                .Col = 7 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbSetColumnInfo_BldRef()
        Dim sFn As String = "Private Sub sbSetColumnInfo_BldRef()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 2
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "내용" : .ColID = "nmd" : .set_ColWidth(.GetColFromID("nmd"), 4 / 5 * 10 + 1)
                .Col = 2 : .Text = "diffday" : .ColID = "diffday" : .ColHidden = True

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

#End Region


#Region " sbUpdateCdList_% 일반검사, 공통 "

    Private Sub sbUpdateCdList_DComCd()
        Dim sFn As String = "Private Sub sbUpdateCdList_DComCd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub


    Private Sub sbUpdateCdList_KeyPad()
        Dim sFn As String = "Private Sub sbUpdateCdList_KeyPad()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("formgbn") : .Text = CType(mfrmCur, FDF45).cboFormGbn.Text

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Cvt_RST()
        Dim sFn As String = "Private Sub sbUpdateCdList_Cvt_RST()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("cvtform") : .Text = CType(mfrmCur, FDF43).txtCvtForm.Text
                .Col = .GetColFromID("cvtfldgbn") : .Text = IIf(CType(mfrmCur, FDF43).rdoFldGbnR.Checked, "R", "C").ToString
                .Col = .GetColFromID("cvtrange") : .Text = IIf(CType(mfrmCur, FDF43).rdoCvtBcNo.Checked, "B", "R").ToString

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Cvt_CMT()
        Dim sFn As String = "Private Sub sbUpdateCdList_Cvt_CMT()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow
                .Col = .GetColFromID("cvtform") : .Text = CType(mfrmCur, FDF44).txtCvtForm.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Calc()
        Dim sFn As String = "Private Sub sbUpdateCdList_Calc()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Cmt()
        Dim sFn As String = "Private Sub sbUpdateCdList_Cmt()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("cmtcont") : .Text = CType(mfrmCur, FDF11).txtCmtCont.Text
                .Col = .GetColFromID("slipnmd_01") : .Text = CType(mfrmCur, FDF11).cboSlip.SelectedItem.ToString()
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Eq()
        Dim sFn As String = "Private Sub sbUpdateCdList_Eq()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow
                .Col = .GetColFromID("eqnms")
                .Text = CType(mfrmCur, FDF13).txtEqNmS.Text

                .Row = miCurRow
                .Col = .GetColFromID("useyn")
                .Text = CStr((IIf(CType(mfrmCur, FDF13).chkDelflg.CheckState.ToString = "Unchecked", "Y", "N")))

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_ExLab()
        Dim sFn As String = "Private Sub sbUpdateCdList_ExLab()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow
                .Col = .GetColFromID("exlabnmd")
                .Text = CType(mfrmCur, FDF07).txtExLabNmD.Text

                .Row = miCurRow
                .Col = .GetColFromID("useyn")
                .Text = CStr((IIf(CType(mfrmCur, FDF07).chkDelflg.CheckState.ToString = "Unchecked", "Y", "N")))

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_OSlip()
        Dim sFn As String = "Private Sub sbUpdateCdList_OSlip()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("tordslipnm")
                .Text = CType(mfrmCur, FDF40).txtTOSlipNm.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_RstCd()
        Dim sFn As String = "Private Sub sbUpdateCdList_RstCd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("tnmd")
                .Text = CType(mfrmCur, FDF10).txtTNmD.Text

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Bccls()
        Dim sFn As String = "Private Sub sbUpdateCdList_Bccls()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("bcclscd") : .Text = CType(mfrmCur, FDF01).txtBcclsCd.Text
                .Col = .GetColFromID("bcclsnmd") : .Text = CType(mfrmCur, FDF01).txtBcclsNmD.Text
                .Col = .GetColFromID("bcclsnmbp") : .Text = CType(mfrmCur, FDF01).txtTBcclsNmBP.Text

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Slip()
        Dim sFn As String = "Private Sub sbUpdateCdList_Slip()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("slipnmd") : .Text = CType(mfrmCur, FDF02).txtSlipNmD.Text
                .Col = .GetColFromID("partnmd") : .Text = CType(mfrmCur, FDF02).txtPartNmD.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Spc()
        Dim sFn As String = "Private Sub sbUpdateCdList_Spc()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("spcnmd") : .Text = CType(mfrmCur, FDF03).txtSpcNmD.Text
                .Col = .GetColFromID("spcifcd") : .Text = CType(mfrmCur, FDF03).txtIFCd.Text
                .Col = .GetColFromID("whonet") : .Text = CType(mfrmCur, FDF03).txtWNCd.Text
                .Col = .GetColFromID("reqcmt") : .Text = IIf(CType(mfrmCur, FDF03).chkReqCmt.Checked, "Y", "").ToString
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_SpcGrp()
        Dim sFn As String = "Private Sub sbUpdateCdList_SpcGrp()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("SPCGRPNMD")
                .Text = CType(mfrmCur, FDF04).txtSpcGrpNmD.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_TGrp()
        Dim sFn As String = "Private Sub sbUpdateCdList_TGrp()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("tgrpnmd") : .Text = CType(mfrmCur, FDF09).txtTGrpNmD.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Tla()
        'Dim sFn As String = "Private Sub sbUpdateCdList_Tla()"

        'If Not rbnWorkOpt1.Checked Then Exit Sub

        'Try
        '    With spdCdList
        '        .Row = miCurRow

        '        .Col = .GetColFromID("EQTLAVOL")
        '        .Text = CType(mfrmCur, FDF14).txtEqTlaVol.Text

        '        .Col = .GetColFromID("EQTLAPOS")
        '        .Text = CType(mfrmCur, FDF14).txtEqTlaPos.Text
        '    End With
        'Catch ex As Exception
        '    Fn.log(msFile + sFn, Err)
        '    MsgBox(msFile + sFn + vbCrLf + ex.Message)
        'End Try
    End Sub

    Private Sub sbUpdateCdList_Tube()
        Dim sFn As String = "Private Sub sbUpdateCdList_Tube()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("tubenmd") : .Text = CType(mfrmCur, FDF06).txtTubeNmD.Text
                .Col = .GetColFromID("tubevol") : .Text = CType(mfrmCur, FDF06).txtVol.Text
                .Col = .GetColFromID("tubeunit") : .Text = CType(mfrmCur, FDF06).txtUnit.Text
                .Col = .GetColFromID("tubeifcd") : .Text = CType(mfrmCur, FDF06).txtIFCd.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Usr()
        Dim sFn As String = "Private Sub sbUpdateCdList_Usr()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow
                .Col = .GetColFromID("usrnm") : .Text = CType(mfrmCur, FDF00).txtUsrNm.Text

                If Ctrl.Get_Code(CType(mfrmCur, FDF00).cboUsrLvl) = "S" Then
                    .Col = .GetColFromID("usrlvl") : .Text = "관리자"
                Else
                    If CType(mfrmCur, FDF00).chkDrSpYN.Checked Then
                        .Col = .GetColFromID("usrlvl") : .Text = "전문의"
                    Else
                        .Col = .GetColFromID("usrlvl") : .Text = "일반"
                    End If

                End If

                .Row = miCurRow
                .Col = .GetColFromID("useyn")
                .Text = CStr((IIf(CType(mfrmCur, FDF00).chkDelFlg.CheckState.ToString = "Unchecked", "Y", "N")))

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_WkGrp()
        Dim sFn As String = "Private Sub sbUpdateCdList_WkGrp()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("wkgrpnmd") : .Text = CType(mfrmCur, FDF05).txtWkGrpNmD.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_VCmt()
        Dim sFn As String = "Private Sub sbUpdateCdList_VCmt()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("cdseq")
                .Text = CType(mfrmCur, FDF48).txtCdSeq.Text
                .Col = .GetColFromID("cdtitle")
                .Text = CType(mfrmCur, FDF48).txtCdTitle.Text
                .Col = .GetColFromID("cdcont")
                .Text = CType(mfrmCur, FDF48).txtCdCont.Text
              
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_VCmt_Tcls()
        Dim sFn As String = "Private Sub sbUpdateCdList_VCmt_Tcls()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            'With spdCdList
            '    .Row = miCurRow

            '    .Col = .GetColFromID("TCLSCD")
            '    .Text = CType(mfrmCur, FDF49).txttclscd.Text
            'End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_VCmt_Doctor()
        Dim sFn As String = "Private Sub sbUpdateCdList_VCmt_Doctor()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("doctornm") : .Text = CType(mfrmCur, FDF50).txtDoctorNm.Text
                .Col = .GetColFromID("medino") : .Text = CType(mfrmCur, FDF50).txtMediNo.Text

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Alert_Rule()

    End Sub

#End Region

#Region " sbUpdateCdList_% 특수검사"
    Private Sub sbUpdateCdList_SpTest()
        Dim sFn As String = "Private Sub sbUpdateCdList_SpTest()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbUpdateCdList_% 특수검사 소견"
    Private Sub sbUpdateCdList_SpTest_Cmt()
        Dim sFn As String = "Private Sub sbUpdateCdList_SpTest_Cmt()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("testcd") : .Text = CType(mfrmCur, FDF21).txtTestcd.Text
                .Col = .GetColFromID("cmtseq") : .Text = CType(mfrmCur, FDF21).txtCmtseq.Text
                .Col = .GetColFromID("cmtcont") : .Text = CType(mfrmCur, FDF21).txtCmtCont.Text

            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbUpdateCdList_% 미생물 "
    Private Sub sbUpdateCdList_Anti()    '항균제 마스터
        Dim sFn As String = "Private Sub sbUpdateCdList_Anti()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("anticd") : .Text = CType(mfrmCur, FDF17).txtAntiCd.Text
                .Col = .GetColFromID("antinmd") : .Text = CType(mfrmCur, FDF17).txtAntiNmD.Text
                .Col = .GetColFromID("antiifcd") : .Text = CType(mfrmCur, FDF17).txtIFCd.Text
                .Col = .GetColFromID("antiwncd") : .Text = CType(mfrmCur, FDF17).txtWNCd.Text
                .Col = .GetColFromID("dispseq") : .Text = CType(mfrmCur, FDF17).txtDispSeq.Text
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_Bac()            '배양균 마스터
        Dim sFn As String = "Sub sbUpdateCdList_Bac()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("bacnmd") : .Text = CType(mfrmCur, FDF16).txtBacNmD.Text
                .Col = .GetColFromID("bacgencd") : .Text = Ctrl.Get_Code(CType(mfrmCur, FDF16).cboBacgen)
                .Col = .GetColFromID("bacifcd") : .Text = CType(mfrmCur, FDF16).txtIFCd.Text
                .Col = .GetColFromID("bacwncd") : .Text = CType(mfrmCur, FDF16).txtWNCd.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbUpdateCdList_Bac_Rst()            '균 결과 마스터
        Dim sFn As String = "Sub sbUpdateCdList_Bac_Rst()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("incrstcd") : .Text = CType(mfrmCur, FDF19).txtIncCd.Text
                .Col = .GetColFromID("incrstnm") : .Text = CType(mfrmCur, FDF19).txtIncNm.Text
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbUpdateCdList_Bacgen()
        Dim sFn As String = "Private Sub sbUpdateCdList_Bacgen()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("bacgennmd")
                .Text = CType(mfrmCur, FDF15).txtBacgenNmD.Text

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_BacgenAnti()
        Dim sFn As String = "Private Sub sbUpdateCdList_BacgenAnti()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbUpdateCdList_% 혈액은행 "
    Private Sub sbUpdateCdList_ComCd()           ' 성분제제 마스터
        Dim sFn As String = "Sub sbUpdateCdList_ComCd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("comnmd") : .Text = CType(mfrmCur, FDF30).txtComNmD.Text
                .Col = .GetColFromID("comordcd") : .Text = CType(mfrmCur, FDF30).txtTOrdCd.Text
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbUpdateCdList_FtCd()           ' 필터 마스터
        Dim sFn As String = "Sub sbUpdateCdList_FTcd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("ftnms") : .Text = CType(mfrmCur, FDF31).txtFTNmS.Text
                .Col = .GetColFromID("fordcd") : .Text = CType(mfrmCur, FDF31).txtFOrdCd.Text
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbUpdateCdList_JobCd()
        Dim sFn As String = "Private Sub sbUpdateCdList_JobCd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("jobnm") : .Text = CType(mfrmCur, FDF32).txtJobNm.Text
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbUpdateCdList_DisCd()
        Dim sFn As String = "Private Sub sbUpdateCdList_DisCd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("disrsn")
                .Text = CType(mfrmCur, FDF33).txtDisRsn.Text
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbUpdateCdList_RtnCd()
        Dim sFn As String = "Private Sub sbUpdateCdList_RtnCd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("cmtcont") : .Text = CType(mfrmCur, FDF34).txtCmtCont.Text
                .Col = .GetColFromID("stopgbn") : .Text = IIf(CType(mfrmCur, FDF34).chkStopGbn.Checked, "1", "").ToString()
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbUpdateCdList_BldRef()
        Dim sFn As String = "Private Sub sbUpdateCdList_BldRef()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbUpdateCdList_CollTkCd()
        Dim sFn As String = "Private Sub sbUpdateCdList_CollTkCd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow
                .Col = .GetColFromID("cmtcont") : .Text = CType(mfrmCur, FDF42).txtCmtCont.Text

                .Row = miCurRow
                .Col = .GetColFromID("useyn")
                .Text = CStr((IIf(CType(mfrmCur, FDF42).chkDelflg.CheckState.ToString = "Unchecked", "Y", "N")))

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbUpdateCdList_AbnRstCd()
        Dim sFn As String = "Private Sub sbUpdateCdList_AbnRstCd()"

        If Not rdoWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("cmtcont") : .Text = CType(mfrmCur, FDF47).txtCmtCont.Text

                .Row = miCurRow
                .Col = .GetColFromID("useyn")
                .Text = CStr((IIf(CType(mfrmCur, FDF47).chkDelflg.CheckState.ToString = "Unchecked", "Y", "N")))

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

#End Region


#Region " sbUSDT_Disable 선언"
    Private Sub sbUSDT_Disable()
        Dim sFn As String = ""

        If IsNothing(mfrmCur) Then Exit Sub

        Try
            Select Case msMstGbn
                Case mcFDF00
                    sbUSDT_Disable_Usr()
                Case mcFDF01
                    sbUSDT_Disable_Bccls()
                Case mcFDF02
                    sbUSDT_Disable_Slip()
                Case mcFDF03
                    sbUSDT_Disable_Spc()
                Case mcFDF04
                    sbUSDT_Disable_SpcGrp()
                Case mcFDF05
                    sbUSDT_Disable_WkGrp()
                Case mcFDF06
                    sbUSDT_Disable_Tube()
                Case mcFDF07
                    sbUSDT_Disable_ExLab()
                Case mcFDF09
                    sbUSDT_Disable_TGrp()
                Case mcFDF10
                    sbUSDT_Disable_RstCd()
                Case mcFDF11
                    sbUSDT_Disable_Cmt()
                Case mcFDF12
                    sbUSDT_Disable_Calc()
                Case mcFDF13
                    sbUSDT_Disable_Eq()
                Case mcFDF40
                    sbUSDT_Disable_OSlip()
                Case mcFDF41
                    sbUSDT_Disable_KSRack()

                Case mcFDF15
                    sbUSDT_Disable_Bacgen()
                Case mcFDF16
                    sbUSDT_Disable_Bac()
                Case mcFDF17
                    sbUSDT_Disable_Anti()
                Case mcFDF18
                    sbUSDT_Disable_BacgenAnti()
                Case mcFDF52
                    sbUSDT_Disable_Cult()
                Case mcFDF19
                    sbUSDT_Disable_Bac_RST()

                Case mcFDF20
                    sbUSDT_Disable_SpTest()
                Case mcFDF21
                    sbUSDT_Disable_SpCmtTest()

                Case mcFDF30
                    sbUSDT_Disable_ComCd()
                Case mcFDF31
                    sbUSDT_Disable_FtCd()
                Case mcFDF32
                    sbUSDT_Disable_JobCd()
                Case mcFDF33
                    sbUSDT_Disable_DisCd()
                Case mcFDF34
                    sbUSDT_Disable_RtnCd()
                Case mcFDF35
                    sbUSDT_Disable_bldref()

                Case mcFDF42
                    sbUSDT_Disable_CollTkCd()

                Case mcFDF43
                    sbUSDT_Disable_Cvt_RST()

                Case mcFDF44
                    sbUSDT_Disable_Cvt_CMT()

                Case mcFDF45
                    sbUSDT_Disable_KeyPad()

                Case mcFDF46
                    sbUSDT_Disable_DComCd()

                Case mcFDF47
                    sbUSDT_Disable_AbnRstCd()

                Case mcFDF48
                    sbUSDT_Disable_VCmt()

                Case mcFDF49
                    sbUSDT_Disable_VCmt_Tcls()

                Case mcFDF50
                    sbUSDT_Disable_VCMT_DOCTOR()

                Case mcFDF51
                    sbUSDT_Disable_aLERT_RULE()

            End Select
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbUSDT_Disable_% 일반검사, 공통 "

    Private Sub sbUSDT_Disable_DComCd()
        With CType(mfrmCur, FDF46)
            '사용일시 미사용
            .cboSlip.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_KeyPad()
        With CType(mfrmCur, FDF45)
            '사용일시 미사용
            .txtTestCd.ReadOnly = True : .txtTestCd.BackColor = Drawing.Color.White
            .txtSpcCd.ReadOnly = True : .txtSpcCd.BackColor = Drawing.Color.White

            .btnCdHelp_test.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Cvt_RST()
        With CType(mfrmCur, FDF43)
            '사용일시 미사용
            .txtTestCd.ReadOnly = True : .txtTestCd.BackColor = Drawing.Color.White
            .txtSpcCd.ReadOnly = True : .txtSpcCd.BackColor = Drawing.Color.White

            .btnCdHelp.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Cvt_CMT()
        With CType(mfrmCur, FDF44)
            '사용일시 미사용
            .txtCmtCd.ReadOnly = True : .txtCmtCd.BackColor = Drawing.Color.White

            .btnSelCmt.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_CollTkCd()
        With CType(mfrmCur, FDF42)
            '사용일시 미사용
            .cboCmtGbn.Enabled = False

            .txtCmtCd.ReadOnly = True : .txtCmtCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnDel.Visible = False
            Else
                .btnDel.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_AbnRstCd()
        With CType(mfrmCur, FDF47)
            '사용일시 미사용
            .cboCmtGbn.Enabled = False

            .txtCmtCd.ReadOnly = True : .txtCmtCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnDel.Visible = False
            Else
                .btnDel.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_KSRack()
        With CType(mfrmCur, FDF41)
            '사용일시 미사용
            .txtBcclsCd.ReadOnly = True : .txtBcclsCd.BackColor = Drawing.Color.White : .cboBcclsNmD.Enabled = False
            .txtRackId.ReadOnly = True : .txtRackId.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Calc()
        With CType(mfrmCur, FDF12)
            '사용일시 미사용
            .txtTestCd.ReadOnly = True : .txtTestCd.BackColor = Drawing.Color.White
            .txtSpcCd.ReadOnly = True : .txtSpcCd.BackColor = Drawing.Color.White

            .btnSelSpc.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Cmt()
        With CType(mfrmCur, FDF11)
            '사용일시 미사용
            .txtCmtCd.ReadOnly = True : .txtCmtCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Eq()
        With CType(mfrmCur, FDF13)
            .txtEqCd.ReadOnly = True : .txtEqCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnDel.Visible = False
            Else
                .btnDel.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_ExLab()
        With CType(mfrmCur, FDF07)
            .txtExLabCd.ReadOnly = True : .txtExLabCd.BackColor = Drawing.Color.White

            If rdoSOpt0.Checked = True Then
                .btnDel.Visible = True
            Else
                .btnDel.Visible = False
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_OSlip()
        With CType(mfrmCur, FDF40)
            '사용일시 미사용
            .txtTOSlipCd.ReadOnly = True : .txtTOSlipCd.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White

            If Me.rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_RstCd()
        With CType(mfrmCur, FDF10)
            '사용일시 미사용
            .txtTestCd.ReadOnly = True : .txtTestCd.BackColor = Drawing.Color.White
            .btnCdHelp_test.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Bccls()
        With CType(mfrmCur, FDF01)

            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_Slip()
        With CType(mfrmCur, FDF02)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtPartCd.ReadOnly = True : .txtPartCd.BackColor = Drawing.Color.White
            .txtSlipCd.ReadOnly = True : .txtSlipCd.BackColor = Drawing.Color.White

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_Spc()
        With CType(mfrmCur, FDF03)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtSpcCd.ReadOnly = True : .txtSpcCd.BackColor = Drawing.Color.White

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_SpcGrp()
        With CType(mfrmCur, FDF04)
            '사용일시 미사용
            .txtSpcGrpCd.ReadOnly = True : .txtSpcGrpCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_TGrp()
        With CType(mfrmCur, FDF09)
            '사용일시 미사용
            .txtTGrpCd.ReadOnly = True : .txtTGrpCd.BackColor = Drawing.Color.White

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_Tube()
        With CType(mfrmCur, FDF06)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtTubeCd.ReadOnly = True : .txtTubeCd.BackColor = Drawing.Color.White
            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_Usr()
        With CType(mfrmCur, FDF00)
            '사용일시 미사용
            .txtUsrID.ReadOnly = True : .txtUsrID.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnDel.Visible = True
            Else
                .btnDel.Visible = False
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_WkGrp()
        With CType(mfrmCur, FDF05)
            '사용일시 미사용
            .txtWkGrpCd.ReadOnly = True : .txtWkGrpCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_VCmt()
        With CType(mfrmCur, FDF48)
            '사용일시 미사용
            .txtCdSeq.ReadOnly = True : .txtCdSeq.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_VCmt_Tcls()
        With CType(mfrmCur, FDF49)
            '사용일시 미사용
            .txtTestCd.ReadOnly = True : .txtTestCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_VCmt_Doctor()
        With CType(mfrmCur, FDF50)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtDoctorCd.ReadOnly = True : .txtDoctorCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Alert_Rule()
        With CType(mfrmCur, FDF51)
            .cboTestCd.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
                '.btnUE.Enabled = True
            End If
        End With
    End Sub

#End Region

#Region " sbUSDT_Disable_% 특수검사"
    Private Sub sbUSDT_Disable_SpTest()
        With CType(mfrmCur, FDF20)
            '사용일시 미사용
            .txtTestCd.ReadOnly = True : .txtTestCd.BackColor = Drawing.Color.White
            .btnSelTest.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If

        End With
    End Sub
#End Region

#Region " sbUSDT_Disable_% 특수검사 소견"
    Private Sub sbUSDT_Disable_SpCmtTest()
        With CType(mfrmCur, FDF21)
            '사용일시 미사용
            .txtTestcd.ReadOnly = True : .txtTestcd.BackColor = Drawing.Color.White
            '.btnSelTest.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If

        End With
    End Sub
#End Region

#Region " sbUSDT_Disable_% 미생물 "
    Private Sub sbUSDT_Disable_Anti()
        With CType(mfrmCur, FDF17)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtAntiCd.ReadOnly = True : .txtAntiCd.BackColor = Drawing.Color.White

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_Bac()
        With CType(mfrmCur, FDF16)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtBacCd.ReadOnly = True : .txtBacCd.BackColor = Drawing.Color.White

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Bac_Rst()
        With CType(mfrmCur, FDF19)
            .txtTestCd.ReadOnly = True : .txtTestCd.BackColor = Drawing.Color.White
            .txtSpcCd.ReadOnly = True : .txtSpcCd.BackColor = Drawing.Color.White
            .txtSpcNmd.Tag = "SPCNMD"
            .btnCdHelp_test.Enabled = False
            .btnCdHelp_spc.Enabled = False
            .btnClear_spc.Visible = True
            .chkSpcGbn.Visible = False : .chkSpcGbn.Checked = False

            .txtIncCd.ReadOnly = True : .txtIncCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Bacgen()
        With CType(mfrmCur, FDF15)
            '.txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtBacgenCd.ReadOnly = True : .txtBacgenCd.BackColor = Drawing.Color.White

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_BacgenAnti()
        With CType(mfrmCur, FDF18)
            '.txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .cboBacgen.Enabled = False : .cboTestMtd.Enabled = False

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If

        End With
    End Sub
    Private Sub sbUSDT_Disable_Cult()
        With CType(mfrmCur, FDF52)

            .btnHelp_test.Enabled = False : .txtTestCd.ReadOnly = True : .txtTnmd.ReadOnly = True : .txtSelSpc.ReadOnly = True : .txtSelSpc.ReadOnly = True
            .txtCultNm.ReadOnly = True : .txtUseDayS.ReadOnly = True
            .btnHelp_spc.Visible = False : .txtSelSpc.Width = 613 : .txtSelSpc.Left = 146 : .txtSpccd.Visible = True

            If rdoSOpt0.Checked = True Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If

        End With
    End Sub
#End Region

#Region " sbUSDT_Disable_% 혈액은행 "
    Private Sub sbUSDT_Disable_ComCd()
        With CType(mfrmCur, FDF30)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtComCd.ReadOnly = True : .txtComCd.BackColor = Drawing.Color.White
            .cboSpcCd.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_FtCd()
        With CType(mfrmCur, FDF31)
            .txtFTCd.ReadOnly = True
            .txtUSDay.ReadOnly = True
            .txtUSDay.BackColor = Drawing.Color.White
            .dtpUSDay.Enabled = False
            .dtpUSTime.Enabled = False

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If

        End With
    End Sub

    Private Sub sbUSDT_Disable_JobCd()
        With CType(mfrmCur, FDF32)
            '사용일시 미사용
            .txtJobCd.ReadOnly = True : .txtJobCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_DisCd()
        With CType(mfrmCur, FDF33)
            '사용일시 미사용
            .txtDisCd.ReadOnly = True : .txtDisCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_RtnCd()
        With CType(mfrmCur, FDF34)
            '사용일시 미사용
            .cboCmtGbn.Enabled = False

            .txtCmtCd.ReadOnly = True : .txtCmtCd.BackColor = Drawing.Color.White

            If rdoSOpt1.Checked Then
                .btnUE.Visible = False
            Else
                .btnUE.Visible = True
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_BldRef()
        With CType(mfrmCur, FDF35)

        End With
    End Sub

#End Region


#Region " sbUSDT_New 선언"
    Private Sub sbUSDT_New()
        Dim sFn As String = ""

        If IsNothing(mfrmCur) Then Exit Sub

        Try
            Select Case msMstGbn
                Case mcFDF00
                    sbUSDT_New_Usr()
                Case mcFDF01
                    sbUSDT_New_Bccls()
                Case mcFDF02
                    sbUSDT_New_Slip()
                Case mcFDF03
                    sbUSDT_New_Spc()
                Case mcFDF04
                    sbUSDT_New_SpcGrp()
                Case mcFDF05
                    sbUSDT_New_WkGrp()
                Case mcFDF06
                    sbUSDT_New_Tube()
                Case mcFDF07
                    sbUSDT_New_ExLab()
                Case mcFDF09
                    sbUSDT_New_TGrp()
                Case mcFDF10
                    sbUSDT_New_RstCd()
                Case mcFDF11
                    sbUSDT_New_Cmt()
                Case mcFDF12
                    sbUSDT_New_Calc()
                Case mcFDF13
                    sbUSDT_New_Eq()
                Case mcFDF40
                    sbUSDT_New_OSlip()
                Case mcFDF41
                    sbUSDT_New_KSRack()

                Case mcFDF15
                    sbUSDT_New_Bacgen()
                Case mcFDF16
                    sbUSDT_New_Bac()
                Case mcFDF17
                    sbUSDT_New_Anti()
                Case mcFDF18
                    sbUSDT_New_BacgenAnti()
                Case mcFDF52
                    sbUSDT_New_Cult()
                Case mcFDF19
                    sbUSDT_New_Bac_Rst()

                Case mcFDF20
                    sbUSDT_New_SpTest()
                Case mcFDF21
                    sbUSDT_New_SpCmtTest()

                Case mcFDF30
                    sbUSDT_New_ComCd()
                Case mcFDF31
                    sbUSDT_New_FtCd()
                Case mcFDF32
                    sbUSDT_New_JobCd()
                Case mcFDF33
                    sbUSDT_New_DisCd()
                Case mcFDF34
                    sbUSDT_New_RtnCd()
                Case mcFDF35
                    sbUSDT_New_BldRef()

                Case mcFDF42
                    sbUSDT_New_CollTkCd()
                Case mcFDF43
                    sbUSDT_New_Cvt_RST()
                Case mcFDF44
                    sbUSDT_New_Cvt_CMT()
                Case mcFDF45
                    sbUSDT_New_KeyPad()

                Case mcFDF46
                    sbUSDT_New_DComCd()

                Case mcFDF47
                    sbUSDT_New_AbnRstCd()

                Case mcFDF48
                    sbUSDT_New_VCmt()

                Case mcFDF49
                    sbUSDT_New_VCmt_Tcls()

                Case mcFDF50
                    sbUSDT_New_vcmt_doctor()

                Case mcFDF51
                    sbUSDT_New_Alert_Rule()
                Case mcFDF53
                    sbUSDT_New_Ref()

            End Select
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbUSDT_New_% 일반검사, 공통 "

    Private Sub sbUSDT_New_DComCd()
        With CType(mfrmCur, FDF46)
            '시작일시 미사용
            .cboSlip.Enabled = True
        End With
    End Sub

    Private Sub sbUSDT_New_KeyPad()
        With CType(mfrmCur, FDF45)
            '시작일시 미사용
            .txtTestCd.ReadOnly = False : .txtSpcCd.ReadOnly = False
            .btnCdHelp_test.Enabled = True
        End With
    End Sub

    Private Sub sbUSDT_New_Cvt_RST()
        With CType(mfrmCur, FDF43)
            '시작일시 미사용
            .txtTestCd.ReadOnly = False : .txtSpcCd.ReadOnly = False
            .btnCdHelp.Enabled = True
            .txtRstCd.ReadOnly = False
            .btnCdHelp_rst.Enabled = True
        End With
    End Sub

    Private Sub sbUSDT_New_Cvt_CMT()
        With CType(mfrmCur, FDF44)
            '시작일시 미사용
            .txtCmtCd.ReadOnly = False
            .btnSelCmt.Enabled = True
        End With
    End Sub

    Private Sub sbUSDT_New_CollTkCd()
        With CType(mfrmCur, FDF42)
            '시작일시 미사용
            .cboCmtGbn.Enabled = True
            .txtCmtCd.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_AbnRstCd()
        With CType(mfrmCur, FDF47)
            '시작일시 미사용
            .cboCmtGbn.Enabled = True
            .txtCmtCd.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_KSRack()
        With CType(mfrmCur, FDF41)
            '시작일시 미사용
            .txtBcclsCd.ReadOnly = False : .cboBcclsNmD.Enabled = True
            .txtRackId.ReadOnly = False : .txtRackId.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_Calc()
        With CType(mfrmCur, FDF12)
            '시작일시 미사용
            .txtTestCd.ReadOnly = False : .txtSpcCd.ReadOnly = False
            .btnSelSpc.Enabled = True
        End With
    End Sub

    Private Sub sbUSDT_New_Cmt()
        With CType(mfrmCur, FDF11)
            '시작일시 미사용
            .txtCmtCd.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_Eq()
        With CType(mfrmCur, FDF13)

            '시작일시 미사용
            .txtEqCd.ReadOnly = False
            .btnDel.Visible = False

        End With
    End Sub

    Private Sub sbUSDT_New_ExLab()
        With CType(mfrmCur, FDF07)
            .txtExLabCd.ReadOnly = False
            .btnDel.Visible = False
        End With
    End Sub

    Private Sub sbUSDT_New_OSlip()
        With CType(mfrmCur, FDF40)
            .txtUSDay.ReadOnly = False
            .txtTOSlipCd.ReadOnly = False : .txtTOSlipCd.ReadOnly = False : .dtpUSTime.Enabled = True : .dtpUSDay.Enabled = True
            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_RstCd()
        With CType(mfrmCur, FDF10)
            '시작일시 미사용
            .txtTestCd.ReadOnly = False : .txtTestCd.ReadOnly = False
            .btnCdHelp_test.Enabled = True
        End With
    End Sub

    Private Sub sbUSDT_New_Bccls()
        With CType(mfrmCur, FDF01)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .btnUE.Visible = False
            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_Slip()
        With CType(mfrmCur, FDF02)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .txtPartCd.ReadOnly = False
            .txtSlipCd.ReadOnly = False
            .btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_Spc()
        With CType(mfrmCur, FDF03)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .txtSpcCd.ReadOnly = False
            .btnUE.Visible = False
            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_SpcGrp()
        With CType(mfrmCur, FDF04)
            '시작일시 미사용
            .txtSpcGrpCd.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_TGrp()
        With CType(mfrmCur, FDF09)
            '시작일시 미사용
            .txtTGrpCd.ReadOnly = False

        End With
    End Sub

    Private Sub sbUSDT_New_Tube()
        With CType(mfrmCur, FDF06)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .txtTubeCd.ReadOnly = False
            .btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_Usr()
        With CType(mfrmCur, FDF00)
            '시작일시 미사용
            .txtUsrID.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_WkGrp()
        With CType(mfrmCur, FDF05)
            '시작일시 미사용
            .txtWkGrpCd.ReadOnly = False

        End With
    End Sub

    Private Sub sbUSDT_New_VCmt()
        With CType(mfrmCur, FDF48)
            '시작일시 미사용
            .txtCdSeq.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_VCmt_Tcls()
        With CType(mfrmCur, FDF49)
            '시작일시 미사용
            .txtTestCd.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_VCmt_Doctor()
        With CType(mfrmCur, FDF50)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .txtDoctorCd.ReadOnly = False
            .btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_Alert_Rule()
        With CType(mfrmCur, FDF51)
            .cboTestCd.Enabled = True
        End With
    End Sub
    '신규일때 enable 해주는 부분 
    Private Sub sbUSDT_New_Ref()
        With CType(mfrmCur, FDF53)

        End With
    End Sub

#End Region

#Region " sbUSDT_New_% 특수검사"
    Private Sub sbUSDT_New_SpTest()
        With CType(mfrmCur, FDF20)
            '시작일시 미사용
            .txtTestCd.ReadOnly = False : .txtTestCd.ReadOnly = False
            .btnSelTest.Enabled = True
        End With
    End Sub
#End Region

#Region " sbUSDT_New_% 특수검사 소견"
    Private Sub sbUSDT_New_SpCmtTest()
        With CType(mfrmCur, FDF21)
            '시작일시 미사용
            .txtTestcd.ReadOnly = False : .txtTestcd.ReadOnly = False
            '.btnSelTest.Enabled = True
        End With
    End Sub
#End Region

#Region " sbUSDT_New_% 미생물 "
    Private Sub sbUSDT_New_Anti()
        With CType(mfrmCur, FDF17)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .txtAntiCd.ReadOnly = False
            .btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_Bac()
        With CType(mfrmCur, FDF16)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .txtBacCd.ReadOnly = False
            .btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_Bac_Rst()
        With CType(mfrmCur, FDF19)
            .txtTestCd.ReadOnly = False : .txtSpcCd.ReadOnly = False
            .btnCdHelp_test.Enabled = True : .btnCdHelp_spc.Enabled = True : .btnClear_spc.Visible = True
            .chkSpcGbn.Visible = True

            .txtIncCd.ReadOnly = False
            .btnUE.Visible = False
        End With
    End Sub

    Private Sub sbUSDT_New_Bacgen()
        With CType(mfrmCur, FDF15)
            .txtBacgenCd.ReadOnly = False
            .btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_BacgenAnti()
        With CType(mfrmCur, FDF18)

            '.txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .cboBacgen.Enabled = True : .cboTestMtd.Enabled = True
            '.btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub
    Private Sub sbUSDT_New_Cult()
        With CType(mfrmCur, FDF52)

            .btnHelp_test.Enabled = True : .txtTestCd.ReadOnly = False : .txtTnmd.ReadOnly = True : .txtSelSpc.ReadOnly = False : .txtSelSpc.ReadOnly = True
            .txtCultNm.ReadOnly = False : .txtUseDayS.ReadOnly = False
            .btnHelp_spc.Visible = True : .txtSelSpc.Width = 692 : .txtSelSpc.Left = 100 : .txtSpccd.Visible = False

            .btnUE.Visible = False
            '.sbSetNewUSDT()
        End With
    End Sub
#End Region

#Region " sbUSDT_New_% 혈액은행 "
    Private Sub sbUSDT_New_ComCd()
        With CType(mfrmCur, FDF30)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .txtComCd.ReadOnly = False : .cboSpcCd.Enabled = True
            .btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_FtCd()
        With CType(mfrmCur, FDF31)
            If .dtpUSDay.Enabled Then Exit Sub '조건 강화의 의미로만 보여짐

            .txtUSDay.ReadOnly = False
            .dtpUSDay.Enabled = True
            .dtpUSTime.Enabled = True
            .btnUE.Visible = False
            .txtFTCd.ReadOnly = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_JobCd()
        With CType(mfrmCur, FDF32)
            '시작일시 미사용
            .txtJobCd.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_DisCd()
        With CType(mfrmCur, FDF33)
            '시작일시 미사용
            .txtDisCd.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_RtnCd()
        With CType(mfrmCur, FDF34)
            '시작일시 미사용
            .cboCmtGbn.Enabled = True
            .txtCmtCd.ReadOnly = False
        End With
    End Sub

    Private Sub sbUSDT_New_BldRef()
        With CType(mfrmCur, FDF35)

        End With
    End Sub

#End Region

    '<------- Control 관련 ------->

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbNew()
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
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents lblCdList As System.Windows.Forms.Label
    Friend WithEvents spdCdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblMstList As System.Windows.Forms.Label
    Friend WithEvents lstMstList As System.Windows.Forms.ListBox
    Friend WithEvents splSpl As System.Windows.Forms.Splitter
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblGuide2 As System.Windows.Forms.Label
    Friend WithEvents rbnWorkOpt0 As System.Windows.Forms.RadioButton
    Friend WithEvents pnlBotton As System.Windows.Forms.Panel
    Friend WithEvents rdoWorkOpt2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoWorkOpt1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSOpt1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSOpt0 As System.Windows.Forms.RadioButton
    Friend WithEvents lblGuide1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGF01))
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
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.lblFil = New System.Windows.Forms.Label()
        Me.btnQuery = New System.Windows.Forms.Button()
        Me.lblGuide2 = New System.Windows.Forms.Label()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.pnlBotton = New System.Windows.Forms.Panel()
        Me.rdoWorkOpt2 = New System.Windows.Forms.RadioButton()
        Me.rdoWorkOpt1 = New System.Windows.Forms.RadioButton()
        Me.rbnWorkOpt0 = New System.Windows.Forms.RadioButton()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnChgUseDt = New CButtonLib.CButton()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.txtFieldVal = New System.Windows.Forms.TextBox()
        Me.lblGuide3 = New System.Windows.Forms.Label()
        Me.lblFieldNm = New System.Windows.Forms.Label()
        Me.rdoSOpt1 = New System.Windows.Forms.RadioButton()
        Me.rdoSOpt0 = New System.Windows.Forms.RadioButton()
        Me.lblCdList = New System.Windows.Forms.Label()
        Me.spdCdList = New AxFPSpreadADO.AxfpSpread()
        Me.lblMstList = New System.Windows.Forms.Label()
        Me.lstMstList = New System.Windows.Forms.ListBox()
        Me.splSpl = New System.Windows.Forms.Splitter()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.lblGuide1 = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.pnlBottom.SuspendLayout()
        Me.pnlBotton.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnFilter)
        Me.pnlBottom.Controls.Add(Me.lblFil)
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Controls.Add(Me.lblGuide2)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.lblFilter)
        Me.pnlBottom.Controls.Add(Me.pnlBotton)
        Me.pnlBottom.Controls.Add(Me.btnReg)
        Me.pnlBottom.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 600)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(988, 32)
        Me.pnlBottom.TabIndex = 3
        '
        'btnFilter
        '
        Me.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFilter.Image = CType(resources.GetObject("btnFilter.Image"), System.Drawing.Image)
        Me.btnFilter.Location = New System.Drawing.Point(69, 3)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(26, 24)
        Me.btnFilter.TabIndex = 183
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'lblFil
        '
        Me.lblFil.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFil.BackColor = System.Drawing.SystemColors.Desktop
        Me.lblFil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFil.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFil.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFil.Location = New System.Drawing.Point(3, 3)
        Me.lblFil.Name = "lblFil"
        Me.lblFil.Size = New System.Drawing.Size(65, 24)
        Me.lblFil.TabIndex = 73
        Me.lblFil.Tag = "0"
        Me.lblFil.Text = "필터"
        Me.lblFil.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuery.BackColor = System.Drawing.Color.White
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuery.Location = New System.Drawing.Point(185, 3)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(40, 24)
        Me.btnQuery.TabIndex = 19
        Me.btnQuery.Text = "조회"
        Me.btnQuery.UseVisualStyleBackColor = False
        '
        'lblGuide2
        '
        Me.lblGuide2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGuide2.BackColor = System.Drawing.Color.AliceBlue
        Me.lblGuide2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGuide2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGuide2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblGuide2.Location = New System.Drawing.Point(228, 3)
        Me.lblGuide2.Name = "lblGuide2"
        Me.lblGuide2.Size = New System.Drawing.Size(172, 24)
        Me.lblGuide2.TabIndex = 6
        Me.lblGuide2.Text = "관리자 작업 선택  ▶▶▶"
        Me.lblGuide2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems1
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker2
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(882, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(97, 25)
        Me.btnExit.TabIndex = 196
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
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
        Me.btnExcel.Location = New System.Drawing.Point(684, 3)
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
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.5!
        Me.btnClear.FocalPoints.CenterPtY = 0.0!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(783, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(97, 25)
        Me.btnClear.TabIndex = 197
        Me.btnClear.Text = "화면정리(F6)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lblFilter
        '
        Me.lblFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilter.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilter.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFilter.ForeColor = System.Drawing.Color.White
        Me.lblFilter.Location = New System.Drawing.Point(96, 3)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(88, 24)
        Me.lblFilter.TabIndex = 66
        Me.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBotton
        '
        Me.pnlBotton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBotton.Controls.Add(Me.rdoWorkOpt2)
        Me.pnlBotton.Controls.Add(Me.rdoWorkOpt1)
        Me.pnlBotton.Controls.Add(Me.rbnWorkOpt0)
        Me.pnlBotton.Location = New System.Drawing.Point(403, 3)
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
        Me.rdoWorkOpt2.Location = New System.Drawing.Point(110, 2)
        Me.rdoWorkOpt2.Name = "rdoWorkOpt2"
        Me.rdoWorkOpt2.Size = New System.Drawing.Size(70, 21)
        Me.rdoWorkOpt2.TabIndex = 9
        Me.rdoWorkOpt2.Text = " 신규"
        Me.rdoWorkOpt2.UseVisualStyleBackColor = False
        '
        'rdoWorkOpt1
        '
        Me.rdoWorkOpt1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rdoWorkOpt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWorkOpt1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoWorkOpt1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.rdoWorkOpt1.Location = New System.Drawing.Point(1, 2)
        Me.rdoWorkOpt1.Name = "rdoWorkOpt1"
        Me.rdoWorkOpt1.Size = New System.Drawing.Size(109, 21)
        Me.rdoWorkOpt1.TabIndex = 8
        Me.rdoWorkOpt1.Text = " 조회, 수정"
        Me.rdoWorkOpt1.UseVisualStyleBackColor = False
        '
        'rbnWorkOpt0
        '
        Me.rbnWorkOpt0.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rbnWorkOpt0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbnWorkOpt0.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rbnWorkOpt0.ForeColor = System.Drawing.Color.MidnightBlue
        Me.rbnWorkOpt0.Location = New System.Drawing.Point(1, 2)
        Me.rbnWorkOpt0.Name = "rbnWorkOpt0"
        Me.rbnWorkOpt0.Size = New System.Drawing.Size(70, 21)
        Me.rbnWorkOpt0.TabIndex = 7
        Me.rbnWorkOpt0.Text = " 조회"
        Me.rbnWorkOpt0.UseVisualStyleBackColor = False
        Me.rbnWorkOpt0.Visible = False
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems4
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
        Me.btnReg.FocalPoints.FocusPtX = 0.02061856!
        Me.btnReg.FocalPoints.FocusPtY = 0.16!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker8
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(585, 3)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(97, 25)
        Me.btnReg.TabIndex = 195
        Me.btnReg.Text = "등록(F2)"
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnChgUseDt
        '
        Me.btnChgUseDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnChgUseDt.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnChgUseDt.ColorFillBlend = CBlendItems5
        Me.btnChgUseDt.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnChgUseDt.Corners.All = CType(6, Short)
        Me.btnChgUseDt.Corners.LowerLeft = CType(6, Short)
        Me.btnChgUseDt.Corners.LowerRight = CType(6, Short)
        Me.btnChgUseDt.Corners.UpperLeft = CType(6, Short)
        Me.btnChgUseDt.Corners.UpperRight = CType(6, Short)
        Me.btnChgUseDt.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnChgUseDt.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnChgUseDt.FocalPoints.CenterPtX = 0.4639175!
        Me.btnChgUseDt.FocalPoints.CenterPtY = 0.32!
        Me.btnChgUseDt.FocalPoints.FocusPtX = 0.02061856!
        Me.btnChgUseDt.FocalPoints.FocusPtY = 0.16!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnChgUseDt.FocusPtTracker = DesignerRectTracker10
        Me.btnChgUseDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnChgUseDt.ForeColor = System.Drawing.Color.White
        Me.btnChgUseDt.Image = Nothing
        Me.btnChgUseDt.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnChgUseDt.ImageIndex = 0
        Me.btnChgUseDt.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnChgUseDt.Location = New System.Drawing.Point(382, 569)
        Me.btnChgUseDt.Name = "btnChgUseDt"
        Me.btnChgUseDt.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnChgUseDt.SideImage = Nothing
        Me.btnChgUseDt.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnChgUseDt.Size = New System.Drawing.Size(97, 25)
        Me.btnChgUseDt.TabIndex = 198
        Me.btnChgUseDt.Text = "사용일시 수정"
        Me.btnChgUseDt.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnChgUseDt.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnChgUseDt.Visible = False
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlLeft.Controls.Add(Me.txtFieldVal)
        Me.pnlLeft.Controls.Add(Me.lblGuide3)
        Me.pnlLeft.Controls.Add(Me.lblFieldNm)
        Me.pnlLeft.Controls.Add(Me.rdoSOpt1)
        Me.pnlLeft.Controls.Add(Me.rdoSOpt0)
        Me.pnlLeft.Controls.Add(Me.lblCdList)
        Me.pnlLeft.Controls.Add(Me.spdCdList)
        Me.pnlLeft.Controls.Add(Me.lblMstList)
        Me.pnlLeft.Controls.Add(Me.lstMstList)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(224, 600)
        Me.pnlLeft.TabIndex = 4
        '
        'txtFieldVal
        '
        Me.txtFieldVal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFieldVal.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtFieldVal.Location = New System.Drawing.Point(107, 575)
        Me.txtFieldVal.Name = "txtFieldVal"
        Me.txtFieldVal.Size = New System.Drawing.Size(113, 21)
        Me.txtFieldVal.TabIndex = 72
        Me.txtFieldVal.Text = "코드명"
        Me.txtFieldVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblGuide3
        '
        Me.lblGuide3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblGuide3.BackColor = System.Drawing.Color.White
        Me.lblGuide3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGuide3.Location = New System.Drawing.Point(68, 575)
        Me.lblGuide3.Name = "lblGuide3"
        Me.lblGuide3.Size = New System.Drawing.Size(38, 21)
        Me.lblGuide3.TabIndex = 71
        Me.lblGuide3.Text = "검색"
        Me.lblGuide3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFieldNm
        '
        Me.lblFieldNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFieldNm.BackColor = System.Drawing.SystemColors.Desktop
        Me.lblFieldNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFieldNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFieldNm.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFieldNm.Location = New System.Drawing.Point(3, 575)
        Me.lblFieldNm.Name = "lblFieldNm"
        Me.lblFieldNm.Size = New System.Drawing.Size(64, 21)
        Me.lblFieldNm.TabIndex = 70
        Me.lblFieldNm.Tag = "0"
        Me.lblFieldNm.Text = "코드"
        Me.lblFieldNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rdoSOpt1
        '
        Me.rdoSOpt1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.rdoSOpt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSOpt1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoSOpt1.ForeColor = System.Drawing.Color.White
        Me.rdoSOpt1.Location = New System.Drawing.Point(115, 181)
        Me.rdoSOpt1.Name = "rdoSOpt1"
        Me.rdoSOpt1.Size = New System.Drawing.Size(105, 20)
        Me.rdoSOpt1.TabIndex = 5
        Me.rdoSOpt1.Text = "전체 자료"
        Me.rdoSOpt1.UseVisualStyleBackColor = False
        '
        'rdoSOpt0
        '
        Me.rdoSOpt0.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rdoSOpt0.Checked = True
        Me.rdoSOpt0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSOpt0.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoSOpt0.ForeColor = System.Drawing.Color.Black
        Me.rdoSOpt0.Location = New System.Drawing.Point(4, 181)
        Me.rdoSOpt0.Name = "rdoSOpt0"
        Me.rdoSOpt0.Size = New System.Drawing.Size(107, 20)
        Me.rdoSOpt0.TabIndex = 4
        Me.rdoSOpt0.TabStop = True
        Me.rdoSOpt0.Text = "사용가능 자료"
        Me.rdoSOpt0.UseVisualStyleBackColor = False
        '
        'lblCdList
        '
        Me.lblCdList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCdList.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCdList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCdList.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCdList.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblCdList.Location = New System.Drawing.Point(4, 156)
        Me.lblCdList.Name = "lblCdList"
        Me.lblCdList.Size = New System.Drawing.Size(216, 20)
        Me.lblCdList.TabIndex = 2
        Me.lblCdList.Text = "기초자료별 코드 리스트"
        Me.lblCdList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'spdCdList
        '
        Me.spdCdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdCdList.DataSource = Nothing
        Me.spdCdList.Location = New System.Drawing.Point(4, 206)
        Me.spdCdList.Name = "spdCdList"
        Me.spdCdList.OcxState = CType(resources.GetObject("spdCdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCdList.Size = New System.Drawing.Size(216, 366)
        Me.spdCdList.TabIndex = 3
        Me.spdCdList.TabStop = False
        '
        'lblMstList
        '
        Me.lblMstList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMstList.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblMstList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMstList.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMstList.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblMstList.Location = New System.Drawing.Point(4, 4)
        Me.lblMstList.Name = "lblMstList"
        Me.lblMstList.Size = New System.Drawing.Size(216, 20)
        Me.lblMstList.TabIndex = 0
        Me.lblMstList.Text = "기초자료 목록"
        Me.lblMstList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstMstList
        '
        Me.lstMstList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstMstList.BackColor = System.Drawing.SystemColors.Window
        Me.lstMstList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstMstList.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lstMstList.ItemHeight = 12
        Me.lstMstList.Items.AddRange(New Object() {"▷ 1 - [00] 사용자", "▷ 2 - [01] 바코드분류", "▷ 3 - [02] 부서/분야", "▷ 4 - [40] 검사처방슬립", "▷ 5 - [03] 검체", "▷ 6 - [06] 용기", "▷ 7 - [07] 위탁기관", "▷ 9 - [09] 검사그룹", "▷10 - [05] 작업그룹", "▷11 - [45] KEYPAD 설정", "▷12 - [10] 결과코드", "▷13 - [43] 결과값 자동변환", "▷14 - [11] 소견", "▷15 - [44] 소견 자동변환", "▷16 - [12] 계산식", "▷17 - [51] Alert Rule", "▷18 - [13] 장비", "▷19 - [41] 보관검체 RACK", "------------------------------", "▷20 - [20] 특수검사 보고서", "------------------------------", "▷21 - [15] 배양균속", "▷22 - [16] 배양균", "▷23 - [17] 항균제", "▷24 - [52] 배지", "▷25 - [18] 배양균속별 항균제", "▷26 - [19] 균 결과", "------------------------------", "▷27 - [30] 성분제제", "▷28 - [31] 필터", "▷29 - [32] 직업(헌혈)", "▷30 - [33] 부적격사유(헌혈)", "▷31 - [34] 반납폐기사유(수혈)", "▷32 - [35] 혈액은행 관련검사 설정", "------------------------------", "▷33 - [42] 채혈/접수(부적합검체) 취소사유", "------------------------------", "▷34 - [47] 기타 사유", "------------------------------", "▷35 - [46] 투약 성분제제 설정", "------------------------------", "▷36 - [48] 종합검증 소견 등록", "▷37 - [49] 종합검증 검사항목 소견 설정", "▷38 - [50] 종합검증 의사 면허번호 등록", "------------------------------", "▷39 - [53] 병원체검사 코드 ", "▷40 - [54] 검사의뢰지침서 관리", "▷41 - [21] 특수검사 소견 관리"})
        Me.lstMstList.Location = New System.Drawing.Point(4, 28)
        Me.lstMstList.Name = "lstMstList"
        Me.lstMstList.Size = New System.Drawing.Size(216, 122)
        Me.lstMstList.TabIndex = 1
        Me.lstMstList.TabStop = False
        '
        'splSpl
        '
        Me.splSpl.BackColor = System.Drawing.SystemColors.Control
        Me.splSpl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.splSpl.Location = New System.Drawing.Point(224, 0)
        Me.splSpl.MinSize = 224
        Me.splSpl.Name = "splSpl"
        Me.splSpl.Size = New System.Drawing.Size(5, 600)
        Me.splSpl.TabIndex = 5
        Me.splSpl.TabStop = False
        '
        'pnlRight
        '
        Me.pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlRight.Controls.Add(Me.lblGuide1)
        Me.pnlRight.Controls.Add(Me.btnChgUseDt)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(229, 0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(759, 600)
        Me.pnlRight.TabIndex = 6
        '
        'lblGuide1
        '
        Me.lblGuide1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblGuide1.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGuide1.Location = New System.Drawing.Point(112, 268)
        Me.lblGuide1.Name = "lblGuide1"
        Me.lblGuide1.Size = New System.Drawing.Size(572, 24)
        Me.lblGuide1.TabIndex = 0
        Me.lblGuide1.Text = "[관리자 작업 선택 ▶▶▶]에서 원하는 작업을 선택하고 기초자료 목록을 클릭하십시요!!"
        Me.lblGuide1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnBack.Location = New System.Drawing.Point(224, 268)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(6, 72)
        Me.btnBack.TabIndex = 8
        Me.btnBack.Text = "◀"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'FGF01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(988, 632)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.splSpl)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlBottom)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FGF01"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "기초마스터 관리"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBotton.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeft.PerformLayout()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRight.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim sFn As String = "Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click"

        Try
            spdCdList.Focus()
            pnlLeft.Width = splSpl.MinSize
            btnBack.Location = New System.Drawing.Point(splSpl.Location.X + 1, btnBack.Location.Y)
            sbResizeRightArea()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnBack_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.MouseEnter
        btnBack.BackColor = System.Drawing.Color.LightSteelBlue
    End Sub

    Private Sub btnBack_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.MouseLeave
        btnBack.BackColor = System.Drawing.Color.FromArgb(234, 234, 234)
    End Sub

    Public Sub btnClear_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = "Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.ButtonClick"

        Try
            sbDisplayClear()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If msMstGbn = "" Then Exit Sub

        With spdCdList
            .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

            If .ExportToExcel("code.xls", "code list", "") Then
                Process.Start("code.xls")
            End If
        End With
    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Public Sub btnReg_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = ""

        If Not btnReg.Enabled Then Exit Sub
        If IsNothing(mfrmCur) Then Exit Sub

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            sbReg()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub lstMstList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMstList.SelectedIndexChanged
        Dim sFn As String = "Private Sub lstMstList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMstList.SelectedIndexChanged"
        Dim sPMstGbn As String = ""

        sPMstGbn = msMstGbn

        Try
            msMstGbn = CType(lstMstList.SelectedItem, String)
            '< mod freety 2007/07/27 : Master List 변경
            'msMstGbn = msMstGbn.Substring(3, 2)
            msMstGbn = Ctrl.Get_Code(msMstGbn)
            '>

            If sPMstGbn = msMstGbn Then Exit Sub

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            sbPreviousFormClose(sPMstGbn)
            sbReloadRightArea(msMstGbn) '<<<
            sbInitialize(msMstGbn) '<<<

            sbDisplayInit_Filter()

            System.Windows.Forms.Application.DoEvents()
            Me.rdoWorkOpt1.Checked = True
            sbDisplayCdList(msMstGbn)

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            Me.Focus()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            '< add freety 2007/05/03 : 검색기능 추가
            sbDisplayColumnNm(1)
            '>

        End Try
    End Sub

    '< add freety 2007/07/27 : Owner Size에 맞게 Resize
    Private Sub FGF01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Dim sFn As String = "Private Sub FGF01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated"

        Try
            If mbActivated Then Return

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

            sbResizeLeftArea(Me.Width)
            Me.Location = New System.Drawing.Point(Me.Owner.Location.X, Me.Owner.Location.Y + 110)
            ' Me.CenterToParent()

            If miMDIChild = 0 Then
                miParentGapX = Me.Owner.Width - Me.Owner.ClientSize.Width
                miParentGapY = Me.Owner.Size.Height - Me.Owner.ClientSize.Height + mcDevMainPanelHeight
            Else
                miParentGapX = Me.ParentForm.Width - Me.ParentForm.ClientSize.Width
                miParentGapY = Me.ParentForm.Size.Height - Me.ParentForm.ClientSize.Height + mcDevMainPanelHeight
            End If

            Return

        Catch ex As Exception

        Finally
            mbActivated = True

        End Try
    End Sub

    '< add freety 2007/05/03 : Close 후 메인 활성화
    Private Sub FGF01_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Owner.Activate()
    End Sub

    Private Sub FGF01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2

                If btnReg.Visible Then btnReg_ButtonClick(Nothing, Nothing)

            Case Windows.Forms.Keys.F6
                btnClear_ButtonClick(Nothing, Nothing)

                '< add freety 2007/05/03 : 검색기능 추가
            Case Windows.Forms.Keys.Delete
                Me.txtFieldVal.Text = ""
                '>
            Case Windows.Forms.Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGF01_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim sFn As String = "Private Sub FGF01_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize"

        Try
            If Me.WindowState = Windows.Forms.FormWindowState.Minimized Then
                Exit Sub
            End If

            If Me.Size.Width < pnlLeft.Size.Width + btnBack.Size.Width + miParentGapX + mcDevFrmMinWidth Then
                Me.Size = New System.Drawing.Size(pnlLeft.Size.Width + btnBack.Size.Width + miParentGapX + mcDevFrmMinWidth, Me.Size.Height)
                Exit Sub
            End If

            If IsNothing(mfrmCur) Then Exit Sub

            mfrmCur.Hide()
            sbResizeLeftArea(Me.Size.Width)
            sbResizeRightArea()
            mfrmCur.Show()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FGF01_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If miMDIChild = 1 Then
            sbRelocation()
        End If

        sbResizeRightArea()
    End Sub

    Private Sub rbnSOpt0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSOpt0.Click, rdoSOpt1.Click
        If msMstGbn = "" Then Exit Sub

        If rdoSOpt1.Checked Then
            Me.btnChgUseDt.Visible = True
            Me.btnReg.Visible = False

            Me.rdoWorkOpt1.Enabled = False : Me.rdoWorkOpt2.Enabled = False
            If rdoWorkOpt1.Checked = False Then rdoWorkOpt1.Checked = True

            sbUSDT_Disable()
        Else
            Me.btnReg.Visible = True
            Me.btnChgUseDt.Visible = False
            Me.rdoWorkOpt1.Enabled = True : Me.rdoWorkOpt2.Enabled = True

            sbUSDT_Disable()
        End If

        sbDisplayCdList(msMstGbn)
    End Sub

    Private Sub rbnWorkOpt0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnWorkOpt0.CheckedChanged, rdoWorkOpt1.CheckedChanged

        If rbnWorkOpt0.Checked Then

            Me.btnReg.Enabled = False
            Me.rdoSOpt1.Enabled = True
            sbUSDT_Disable()
        Else
            Me.btnReg.Enabled = True

            If Me.rdoWorkOpt1.Checked Then

                sbDisplayCdList(msMstGbn)
                Me.btnReg.Text = "수정(F2)"
                Me.rdoSOpt1.Enabled = True
                sbUSDT_Disable()

            Else
                sbDisplayClear()
                Me.btnReg.Text = "등록(F2)"      '등록시에 sbUSDT_New()를 통해 컨트롤을 Enable시킴
                sbUSDT_New()
                'sbDisplayCdList(msMstGbn)
            End If
        End If
    End Sub

    '< add freety 2007/05/03 : 검색기능 추가
    Private Sub spdCdList_BeforeUserSort(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BeforeUserSortEvent) Handles spdCdList.BeforeUserSort
        '< add freety 2007/05/03 : 검색기능 추가
        sbDisplayColumnNm(e.col)
        '>
    End Sub

    Private Sub spdCdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdCdList.ClickEvent
        Dim sFn As String = "Private Sub spdCdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdCdList.ClickEvent"

        If e.row = 0 Then
             sbDisplayColumnNm(e.col)
        End If

        If giAddModeKey > 0 Then Return
        If e.row < 1 Then Return
        If IsNothing(mfrmCur) Then Return

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If miLeaveRow = 1 Then Return

            sbDisplayCdCurRow(e.row)
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            miLeaveRow = 0
        End Try
    End Sub

    Private Sub spdCdList_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles spdCdList.LeaveRow
        Dim sFn As String = "Private Sub spdCdList_LeaveRow(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles spdCdList.LeaveRow"

        If giAddModeKey > 0 Then Exit Sub
        If e.newRow < 1 Then Exit Sub
        If e.newRow = e.row Then Exit Sub

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

    Private Sub split1_SplitterMoving(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles splSpl.SplitterMoving
        Dim sFn As String = "Private Sub split1_SplitterMoving(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles split1.SplitterMoving"

        Try
            btnBack.SendToBack()
            btnBack.Location = New System.Drawing.Point(e.SplitX + 1, btnBack.Location.Y)
            btnBack.Hide()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub split1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles splSpl.SplitterMoved
        Dim sFn As String = "Private Sub split1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles split1.SplitterMoved"

        Try
            btnBack.BringToFront()
            btnBack.Show()
            sbResizeRightArea()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnChgUseDt_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChgUseDt.Click
        Dim sFn As String = ""

        If IsNothing(mfrmCur) Then Exit Sub

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            sbChgUseDt()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        If m_dt_CdList Is Nothing Then
            MsgBox("조회를 한 이후에 필터할 수 있습니다!!")
            Return
        End If

        'Top --> btnFilter의 아래쪽에 맞춰지도록 설정
        Dim iTop As Integer = Ctrl.FindControlTop(Me.btnFilter) - m_fpopup_f.Height '+ Me.btnFilter.Height + Ctrl.menuHeight

        'Left --> btnFilter와 같이 설정
        Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnFilter)

        With m_fpopup_f
            .TopPoint = iTop
            .LeftPoint = iLeft
            .Display()
        End With

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        If m_dt_CdList Is Nothing Then
            MsgBox("조회를 한 이후에 필터할 수 있습니다!!")
            Return
        End If

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        sbDisplayCdList(msMstGbn)

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFilter.Text = ""
        'Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        'sbDisplay_Filter_Query()

        'Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub


    Private Sub txtFieldVal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFieldVal.GotFocus
        Dim sFn As String = ""

        Try
            If lblFieldNm.Text.Trim().EndsWith("검사코드") Then
                txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Upper
            Else
                txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Normal
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
            If lblFieldNm.Text.Trim().EndsWith("검사코드") Then
                txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Upper
            Else
                txtFieldVal.CharacterCasing = Windows.Forms.CharacterCasing.Normal
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sbFindList(ByVal rsBuf As String)
        Dim sFn As String = "Sub sbFindList"

        Try
            If Me.lblFieldNm.Tag Is Nothing Then Return
            If IsNumeric(Me.lblFieldNm.Tag) = False Then Return

            Dim iCol As Integer = Convert.ToInt32(Val(Me.lblFieldNm.Tag))

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCdList

            With spd
                'If rsBuf = "" Then Return

                Dim iFindRow As Integer = .SearchCol(iCol, 1, .MaxRows, rsBuf, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)

                Do
                    Dim sCd As String = Ctrl.Get_Code(spd, iCol, iFindRow)

                    If sCd.StartsWith(rsBuf) Then
                        Exit Do
                    Else
                        iFindRow = .SearchCol(iCol, iFindRow, .MaxRows, rsBuf, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)
                    End If
                Loop While iFindRow > 0

                If iFindRow < 0 Then iFindRow = 0

                If iFindRow < 1 Then Return

                If iCol = 1 Then
                    spd.Col = iCol
                Else
                    spd.Col = iCol - 1
                End If

                spd.Row = iFindRow
                spd.Action = FPSpreadADO.ActionConstants.ActionGotoCell
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally

        End Try
    End Sub
    '> yjlee 2010-06-15

    Private Sub FGF01_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'MdiTabControl.sbTabPageMove(Me)
    End Sub

End Class
