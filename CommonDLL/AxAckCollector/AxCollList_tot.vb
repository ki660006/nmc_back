'>>> AxCollList

Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Imports LISAPP.APP_C
Imports OCSAPP.OcsLink.Ord
Imports WEBSERVER
Imports LISAPP.APP_BT
Imports CDHELP.FGCDHELPFN


Public Class AxCollList_tot
    Inherits System.Windows.Forms.UserControl

    Private moForm As Windows.Forms.Form

    Private msIoGbn As String = ""
    Private msDeptOrWard As String = ""
    Private msSpcFlg1 As String = ""
    Private msSpcFlg2 As String = ""
    Private msPartGbn As String = ""    '-- L:진단검체, R:핵의학검체, 

    Private mbSearchMode As Boolean = False
    Private mbCollBatch As Boolean = False
    Private msCollUsrId As String = ""

    Private m_enumCallForm As enumCollectCallForm = enumCollectCallForm.CollectIn
    Private msLisCmts As String = ""

    Private m_prtparams As AxAckPrinterSetting.PrinterParams

    Private miPints As Integer = 0
    Private m_objAxCollBcNos As New AxCollBcNos
    Private m_so As AxAckCollector.OrdList_SearchOption
    Private m_dt_ord As DataTable
    Private m_cpi As STU_PatInfo
    Private m_al_HiddenCols As ArrayList
    Private mbSkip As Boolean = False
    Private mbMoveMode As Boolean = False
    Private mbAllCheck As Boolean = True
    Private miAutoCheckDay As Integer = 0

    Private mbMergeMode As Boolean
    Private mbCheckMode As Boolean = False

    Public Event ChangedRow(ByVal cpi As STU_PatInfo)
    Public Event MsgPopup(ByVal rs_Msg As String)

    Public WriteOnly Property Form() As Windows.Forms.Form
        Set(ByVal value As Windows.Forms.Form)
            moForm = value
        End Set
    End Property

    Public Property CallForm() As enumCollectCallForm
        Get
            Return m_enumCallForm
        End Get

        Set(ByVal value As enumCollectCallForm)
            m_enumCallForm = value
            sbDisplay_Spread_Inti()

        End Set

    End Property

    Public WriteOnly Property CollUsrId() As String

        Set(ByVal value As String)
            msCollUsrId = value
            sbGet_Data_LisCmt()
        End Set

    End Property

    Public WriteOnly Property CollBatch() As Boolean

        Set(ByVal value As Boolean)
            mbCollBatch = value

            sbDisplay_Spread_Inti()
        End Set

    End Property

    Public Property BcPrinterParams() As AxAckPrinterSetting.PrinterParams
        Get
            Return m_prtparams
        End Get

        Set(ByVal value As AxAckPrinterSetting.PrinterParams)
            m_prtparams = value
        End Set
    End Property

    Public WriteOnly Property SearchMode() As Boolean

        Set(ByVal value As Boolean)
            mbSearchMode = value
            sbDisplay_Spread_Search(value)
        End Set

    End Property

    Public WriteOnly Property CollMoveMode() As Boolean
        Set(ByVal value As Boolean)
            mbMoveMode = value

            Me.spdOrdList.AllowColMove = mbMoveMode

        End Set
    End Property

    Public WriteOnly Property AllCheckMode() As Boolean
        Set(ByVal value As Boolean)
            mbAllCheck = value
        End Set
    End Property

    Public WriteOnly Property AutoCheckDay() As Integer
        Set(ByVal value As Integer)
            miAutoCheckDay = value
        End Set
    End Property

    Public Property PatInfo() As STU_PatInfo
        Get
            Return fnGet_PatList(spdOrdList.ActiveRow)
        End Get

        Set(ByVal value As STU_PatInfo)
            m_cpi = value
        End Set

    End Property

    Public Function fnGet_Checked_BcNos() As ArrayList

        Try
            Dim arlBcNos As New ArrayList

            With spdOrdList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chkbc") : Dim sChk As String = .Text
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "").Trim

                    If sChk = "1" And sBcNo <> "" Then
                        If arlBcNos.Contains(sBcNo) = False Then
                            arlBcNos.Add(sBcNo)
                        End If
                    End If
                Next
            End With

            Return arlBcNos
        Catch ex As Exception

            Return New ArrayList
        End Try

    End Function

    Private Sub sbDisplay_Spread_Inti()
        With spdOrdList
            Dim iCol As Integer = 0

            .ReDraw = False

            .Col = .GetColFromID("bcno") : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            .BlockMode = True
            .ColHidden = True
            .BlockMode = False

            iCol = .GetColFromID("deptcd") : If iCol > 0 Then .Col = iCol : .ColHidden = True
            iCol = .GetColFromID("wardno") : If iCol > 0 Then .Col = iCol : .ColHidden = True
            iCol = .GetColFromID("wardnm") : If iCol > 0 Then .Col = iCol : .ColHidden = True
            iCol = .GetColFromID("regno") : If iCol > 0 Then .Col = iCol : .ColHidden = True

            If mbCollBatch Then
                iCol = .GetColFromID("patnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
            End If

            Select Case m_enumCallForm
                Case enumCollectCallForm.CollectIn     '-- 입원
                    'iCol = .GetColFromID("wardno") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("wardnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("entdt") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("roomno") : If iCol > 0 Then .Col = iCol : .ColHidden = False

                    iCol = .GetColFromID("iogbn") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                    iCol = .GetColFromID("hopeday") : If iCol > 0 Then .Col = iCol : .ColHidden = True

                    If mbCollBatch Then
                        iCol = .GetColFromID("regno") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                        iCol = .GetColFromID("patnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    End If

                Case enumCollectCallForm.CollectOut     '-- 외래
                    iCol = .GetColFromID("entdt") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                    iCol = .GetColFromID("roomno") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                    iCol = .GetColFromID("iogbn") : If iCol > 0 Then .Col = iCol : .ColHidden = True

                    iCol = .GetColFromID("hopeday") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    'iCol = .GetColFromID("deptcd") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("deptnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("rsvdate") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("opencard") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("roomno") : Dim blnFlg = .ColHidden


                Case enumCollectCallForm.CollectAll   '-- 통합
                    iCol = .GetColFromID("entdt") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    'iCol = .GetColFromID("wardno") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("wardnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("roomno") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("iogbn") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("hopeday") : If iCol > 0 Then .Col = iCol : .ColHidden = False

                Case enumCollectCallForm.CollectCust  '-- 수탁
                    iCol = .GetColFromID("entdt") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                    'iCol = .GetColFromID("wardno") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                    iCol = .GetColFromID("wardnm") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                    iCol = .GetColFromID("roomnm") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                    iCol = .GetColFromID("iogbn") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                    iCol = .GetColFromID("hopeday") : If iCol > 0 Then .Col = iCol : .ColHidden = True

                    iCol = .GetColFromID("regno") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                    iCol = .GetColFromID("patnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
            End Select

            .ReDraw = True

        End With
    End Sub

    Private Sub sbDisplay_Spread_Search(ByVal rbSearchMode As Boolean)
        Dim iCol As Integer = 0

        With spdOrdList
            .ReDraw = False
            If rbSearchMode Then
                iCol = .GetColFromID("spcflg") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                iCol = .GetColFromID("rstflg") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                iCol = .GetColFromID("bcno") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                iCol = .GetColFromID("colldt") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                iCol = .GetColFromID("collnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                'iCol = .GetColFromID("passdt") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                'iCol = .GetColFromID("passnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                iCol = .GetColFromID("tkdt") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                iCol = .GetColFromID("tknm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
            Else
                iCol = .GetColFromID("spcflg") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                iCol = .GetColFromID("rstflg") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                iCol = .GetColFromID("bcno") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                iCol = .GetColFromID("colldt") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                iCol = .GetColFromID("collnm") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                'iCol = .GetColFromID("passdt") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                'iCol = .GetColFromID("passnm") : If iCol > 0 Then .Col = iCol : .ColHidden = False
                iCol = .GetColFromID("tkdt") : If iCol > 0 Then .Col = iCol : .ColHidden = True
                iCol = .GetColFromID("tknm") : If iCol > 0 Then .Col = iCol : .ColHidden = True

            End If

            .ReDraw = True
        End With

    End Sub

    Public Sub sbDisplay_Spread_HiddenYn(ByVal rbFlag As Boolean)
        Dim iCol As Integer = 0

        With spdOrdList
            .ReDraw = False

            If rbFlag Then
                sbDisplay_Spread_Inti()
                sbDisplay_Spread_Search(mbSearchMode)
            Else
                For ix As Integer = 1 To .MaxCols
                    .Col = ix : .ColHidden = rbFlag
                Next
            End If
            .ReDraw = True
        End With

    End Sub

    Public Sub Clear()
        Me.spdOrdList.MaxRows = 0
        Me.lblMsg.Visible = False
        Me.lstMsg.Items.Clear()
    End Sub

    Private Sub sbGet_Data_LisCmt()
        Dim sFn As String = "Private Sub sbGet_Data_LisCmt"
        Try
            Dim dt As DataTable = (New LISAPP.APP_F_COLLTKCD).fnGet_CollTK_Cancel_ContInfo("3")

            If dt.Rows.Count > 0 Then
                Dim sCmt As String = "".PadLeft(6, " "c) + Chr(9)
                For iCnt As Integer = 0 To dt.Rows.Count - 1
                    sCmt += dt.Rows(iCnt).Item("cmtcont").ToString().Trim() + Chr(9)
                Next

                msLisCmts = sCmt
            End If

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Color_bccls()
        Dim sFn As String = "Private Sub sbGet_Data_LisCmt"
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_bccls_color
            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(ix).Item("colorgbn").ToString
                        Case "1"
                            lblBcclsNm1.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor1.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor1.ForeColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                        Case "2"
                            lblBcclsNm2.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor2.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor2.ForeColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                        Case "3"
                            lblBcclsNm3.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor3.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor3.ForeColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                    End Select
                Next
            End If

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Fkocs_Select(ByVal rsChkGbn As String, ByVal rsFkOcs As String, ByVal riRow As Integer)
        '-- Group 처방인 경우 처리
        For ix As Integer = 1 To spdOrdList.MaxRows
            Dim sChkGbn As String = Ctrl.Get_Code(spdOrdList, "chk", ix, False)
            Dim sFkocs As String = Ctrl.Get_Code(spdOrdList, "fkocs", ix, False)

            Dim iRowB As Integer = 0

            Dim bDuplicated As Boolean = False
            Dim bDuplicated_IncludeOrder As Boolean = False '< Panel 에 포함된 중복 처방 체크 

            If riRow <> ix And sChkGbn <> rsChkGbn And sFkocs = rsFkOcs Then
                With spdOrdList
                    .Col = .GetColFromID("chk")

                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                        sChkGbn = Ctrl.Get_Code(spdOrdList, "chk", ix, False)

                        Dim iSelGrp As Integer = spdOrdList.GetRowItemData(ix)
                        Dim iRowE As Integer = fnFind_Row_End_With_Same_GrpNo(iSelGrp, iRowB)

                        If rsChkGbn = "1" Then
                            bDuplicated = fnFind_Duplicated_Order(ix, iRowB)

                            If bDuplicated Then
                                .SetText(.GetColFromID("chk"), ix, "")

                                sbLog_Msg("중복", "중복처방은 동시에 선택할 수 없습니다!!")
                            Else

                                .SetText(.GetColFromID("chk"), ix, rsChkGbn)
                            End If

                            '< yjlee 2009-02-12 
                            ' # Panel 또는 Group에 포함된 단일 검사코드의 중복 체크
                            If Not bDuplicated Then
                                bDuplicated_IncludeOrder = fnFind_Duplicated_IncludeOrder(ix, iRowB)

                                If bDuplicated_IncludeOrder Then
                                    .SetText(.GetColFromID("chk"), ix, "")

                                    sbLog_Msg("중복", "중복처방은 동시에 선택할 수 없습니다!!")
                                Else
                                    .SetText(.GetColFromID("chk"), ix, rsChkGbn)
                                End If
                            End If
                            '> yjlee 2009-02-12

                        Else
                            .SetText(.GetColFromID("chk"), ix, rsChkGbn)

                        End If

                        iSelGrp = .SearchCol(.GetColFromID("chk"), iRowB - 1, iRowE, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                        If iSelGrp < iRowB Then
                            .SetText(.GetColFromID("chkbc"), iRowB, "")
                        Else
                            .SetText(.GetColFromID("chkbc"), iRowB, "1")
                        End If

                    End If

                End With
            End If
        Next

    End Sub

    Public Function CollectAndTakeSelOrder() As ArrayList
        Dim sFn As String = "Public Function CollectAndTakeSelOrder() As ArrayList"

        Dim iCnt As Integer = 0

        Dim diagData As STU_DiagInfo

        Dim al_collData As New ArrayList
        Dim al_diagData As New ArrayList

        Dim sOwnGbn As String = ""

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim al_return As ArrayList

        Try
            If msCollUsrId = "" Then
                MsgBox("채혈자 아이디가 존재하지 않습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)

                Return Nothing
            End If

            If Fn.SpdColSearch(spd, "1", spd.GetColFromID("chk")) = 0 Then
                If mbCollBatch = False Then
                    sbLog_Msg("", sFn + " : " + "채혈을 위해 선택된 검사항목이 없습니다. 확인하여 주십시요!!")
                End If

                Return Nothing
            End If

            ''< 데이터 변경 여부 조사
            'Dim bChange As Boolean = fnFind_Exist_Change(m_dt_ord, fnGet_OrderData(m_so))

            'If bChange Then
            '    If mbCollBatch = False Then
            '        MsgBox(Label_RegNo + " : " + m_so.RegNo + "의 Order 정보가 변경되었습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)
            '    Else
            '        sbLog_Msg("변경", Label_RegNo + " : " + m_so.RegNo + "의 Order 정보가 변경되었습니다.")
            '    End If

            '    Return Nothing
            'End If
            ''>

            Dim dtSysDt As Date = (New LISAPP.APP_DB.ServerDateTime).GetDateTime()

            Dim al_BcInfo As New ArrayList

            Dim iMaxGrpNo As Integer = spd.GetRowItemData(spd.MaxRows)

            With spd
                Dim iRowE As Integer = 0

                Dim listCollData_pre As New List(Of STU_CollectInfo)

                For g As Integer = 1 To iMaxGrpNo
                    Dim iRowB As Integer = 0

                    Dim listCollData As New List(Of STU_CollectInfo)

                    iRowB = iRowE + 1

                    miPints = 0

                    For i As Integer = iRowB To .MaxRows
                        Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)

                        Dim collData As New STU_CollectInfo

                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            If sChk = "1" Then
                                collData = fnFind_collData(i, dtSysDt)

                                If collData IsNot Nothing Then
                                    listCollData.Add(collData)
                                End If
                            End If
                        Else
                            Exit For
                        End If

                        collData = Nothing
                    Next

                    '> 연속검사 샘플 -> True, 아니면 False
                    Dim bSeries As Boolean = False

                    If listCollData.Count > 0 Then
                        If listCollData_pre.Count > 0 Then
                            If listCollData.Item(0).BCKEY3 = listCollData_pre.Item(0).BCKEY3 Then
                                If listCollData.Item(0).SEQTMI <> listCollData_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo.Add(listCollData)

                    End If

                    If listCollData.Count > 0 Then
                        listCollData_pre = listCollData
                    End If

                    listCollData = Nothing
                Next

                If m_cpi.DIAG_K <> "" Or m_cpi.DIAG_E <> "" Then
                    diagData = New STU_DiagInfo

                    With diagData
                        .DIAGNM = m_cpi.DIAG_K
                        .DIAGNM_ENG = m_cpi.DIAG_E
                    End With

                    al_diagData.Add(diagData)
                End If

            End With

            If al_BcInfo.Count < 1 Then Return Nothing

            With (New LISAPP.APP_C.CollReg)
                al_return = .ExecuteDo(al_BcInfo, al_diagData, True)
            End With

            Return al_return

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

            Return Nothing

        End Try
    End Function


    Public Function LebelPrint() As ArrayList
        Dim sFn As String = "Public Function LebelPrint() As ArrayList"

        Dim al_collData As New ArrayList
        Dim sIBDay As String = ""

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Try

            If Fn.SpdColSearch(spd, "1", spd.GetColFromID("chk")) = 0 Then
                sbLog_Msg("", sFn + " : " + "출력을 위해 선택된 검사항목이 없습니다. 확인하여 주십시요!!")

                Return Nothing
            End If

            Dim dtSysDt As Date = (New LISAPP.APP_DB.ServerDateTime).GetDateTime()

            Dim al_BcInfo As New ArrayList

            Dim iMaxGrpNo As Integer = spd.GetRowItemData(spd.MaxRows)

            With spd
                Dim iRowE As Integer = 0

                Dim listCollData_pre As New List(Of STU_CollectInfo)

                For g As Integer = 1 To iMaxGrpNo
                    Dim iRowB As Integer = 0

                    Dim listCollData As New List(Of STU_CollectInfo)

                    iRowB = iRowE + 1

                    miPints = 0

                    For i As Integer = iRowB To .MaxRows
                        Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)

                        Dim collData As New STU_CollectInfo

                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            If sChk = "1" Then
                                collData = fnFind_collData(i, dtSysDt, True)

                                If collData IsNot Nothing Then

                                    listCollData.Add(collData)
                                End If
                            End If
                        Else
                            Exit For
                        End If

                        collData = Nothing
                    Next

                    '> 연속검사 샘플 -> True, 아니면 False
                    Dim bSeries As Boolean = False

                    If listCollData.Count > 0 Then
                        If listCollData_pre.Count > 0 Then
                            If listCollData.Item(0).BCKEY3 = listCollData_pre.Item(0).BCKEY3 Then
                                If listCollData.Item(0).SEQTMI <> listCollData_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo.Add(listCollData)

                    End If

                    If listCollData.Count > 0 Then
                        listCollData_pre = listCollData
                    End If

                    listCollData = Nothing
                Next

            End With

            Return al_BcInfo

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

            Return Nothing

        End Try
    End Function

    Public Sub Print_barcode(ByVal rsFormNm As String)
        Dim sFn As String = "Public Function Print_barcode() As ArrayList"

        Try

            If Fn.SpdColSearch(Me.spdOrdList, "1", Me.spdOrdList.GetColFromID("chkbc")) = 0 Then
                sbLog_Msg("", sFn + " : " + "출력을 위해 선택된 검체가 없습니다. 확인하여 주십시요!!")
                Return
            End If

            Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(rsFormNm)
            Dim alBcNo As New ArrayList

            With Me.spdOrdList
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chkbc") : Dim sChk As String = .Text
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")

                    If sChk = "1" And sBcNo <> "" And alBcNo.Contains(sBcNo) = False Then alBcNo.Add(sBcNo)
                Next

                If alBcNo.Count > 0 Then
                    objBCPrt.PrintDo(alBcNo, "1")
                End If

                alBcNo.Clear()
            End With

            alBcNo = Nothing
        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)
        End Try
    End Sub

    Public Function CollectSelCancel(ByVal r_frm As Windows.Forms.Form) As Boolean
        Dim sFn As String = "Public Function CollectSelCancel() As ArrayList"

        Dim alBcNos As ArrayList = fnGet_Checked_BcNos()

        'If arlBcNos.Count < 1 Then
        '    MsgBox("채혈취소할 검체를 선택하지 않았습니다." + vbCrLf + "확인하세요.!!", MsgBoxStyle.OkOnly, "채혈취소")
        '    Return False
        'End If

        Try
            Dim sIoGbn As String = "O"
            Dim frm As New FGCancel_BC

            If m_enumCallForm = enumCollectCallForm.CollectIn Then sIoGbn = "I"

            Return frm.Display_Result(r_frm, sIoGbn, alBcNos)

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)
            Return False
        End Try

    End Function

    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "등록번호" : .WIDTH = "120" : .FIELD = "regno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "환자명" : .WIDTH = "140" : .FIELD = "patnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "병동" : .WIDTH = "20" : .FIELD = "wardnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "병실" : .WIDTH = "20" : .FIELD = "roomno"
        End With
        alItems.Add(stu_item)

        Return alItems

    End Function

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String, ByVal rbPreView As Boolean)
        Dim sFn As String = "Sub sbPrint_Data()"

        Try
            Dim arlPrint As New ArrayList

            With spdOrdList
                Dim alItem As New ArrayList
                Dim sTnmbps As String = ""
                Dim sGrpNo As String = ""
                Dim sGrpNo_Key As String = ""
                Dim sTnms_tmp As String = ""

                Dim objPat As New CGPRT_PATINFO

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow

                    .Col = .GetColFromID("grpno") : Dim sGrpNo_t As String = .Text.Trim

                    If sGrpNo_t <> "" Then sGrpNo = sGrpNo_t

                    If sGrpNo_Key <> sGrpNo Then

                        If sGrpNo_Key <> "" Then
                            objPat = New CGPRT_PATINFO

                            With objPat
                                .alItem = alItem
                                .CmtCont = sTnmbps + "^" + "검사항목" + "^" + "1000" + "^"
                            End With

                            arlPrint.Add(objPat)

                            sTnmbps = "" : sTnms_tmp = ""
                            alItem = New ArrayList
                        End If

                        Dim sBuf() As String = rsTitle_Item.Split("|"c)
                        For intIdx As Integer = 0 To sBuf.Length - 1

                            If sBuf(intIdx) = "" Then Exit For

                            Dim intCol As Integer = .GetColFromID(sBuf(intIdx).Split("^"c)(1))

                            If intCol > 0 Then

                                Dim sTitle As String = sBuf(intIdx).Split("^"c)(0)
                                Dim sField As String = sBuf(intIdx).Split("^"c)(1)
                                Dim sWidth As String = sBuf(intIdx).Split("^"c)(2)

                                .Row = intRow
                                .Col = .GetColFromID(sField) : Dim strVal As String = .Text

                                alItem.Add(strVal + "^" + sTitle + "^" + sWidth + "^")
                            End If
                        Next
                    End If
                    sGrpNo_Key = sGrpNo

                    .Col = .GetColFromID("tnmbp") : Dim sTnmbp As String = .Text

                    If sTnms_tmp.Length > 50 Then
                        sTnmbps += vbCrLf : sTnms_tmp = ""
                    End If

                    sTnmbps += sTnmbp + " "
                    sTnms_tmp += sTnmbp + " "

                Next

                objPat = New CGPRT_PATINFO

                With objPat
                    .alItem = alItem
                    .CmtCont = sTnmbps.Substring(0, sTnmbps.Length - 1) + "^" + "검사항목" + "^" + "1000" + "^"
                End With

                arlPrint.Add(objPat)

            End With

            If arlPrint.Count > 0 Then
                Dim prt As New CGPRT_COLLLIST
                prt.mbLandscape = False  '-- false : 세로, true : 가로
                prt.msTitle = "채혈 대상자 리스트"
                prt.msJobGbn = ""
                prt.maPrtData = arlPrint

                If rbPreView Then
                    prt.sbPrint_Preview()
                Else
                    prt.sbPrint()
                End If
            End If
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Sub Print_CollList(ByVal rbPreView As Boolean)
        Dim sFn As String = "Print_CollList"

        Try

            Dim sRetUrn As String = ""

            sRetUrn += "등록번호" + "^" + "regno" + "^" + "80" + "^" + "|"
            sRetUrn += "성명" + "^" + "patnm" + "^" + "120" + "^" + "|"
            sRetUrn += "병동" + "^" + "wardnm" + "^" + "40" + "^" + "|"
            sRetUrn += "병실" + "^" + "roomno" + "^" + "40" + "^" + "|"

            sbPrint_Data(sRetUrn, rbPreView)

        Catch ex As Exception
            Fn.log("AxCollector :" + sFn, Err)
        End Try

    End Sub

    Public Sub Print_Document()

        Dim al_prtPatData As New ArrayList
        Dim alCPrtGbn As New ArrayList

        With spdOrdList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("cprtgbn") : Dim sCprtGbn As String = .Text

                If sChk = "1" And sCprtGbn > "0" Then
                    If alCPrtGbn.Contains(sCprtGbn) = False Then
                        Dim objData As STU_CollectInfo = fnFind_collData(ix, Now)
                        al_prtPatData.Add(objData)
                    End If
                End If
            Next

            If al_prtPatData.Count > 0 Then
                sbPrint_Document(al_prtPatData)
            Else
                MsgBox("인쇄할 서식 없습니다.!! 확인하세요.", MsgBoxStyle.Information, "서식인쇄")
            End If

        End With

    End Sub


    Private Sub sbPrint_Document(ByVal raPrtData As ArrayList)

        For ix As Integer = 0 To raPrtData.Count - 1
            Dim prt As New CGPRT_DOCUMENT
            Dim collData As STU_CollectInfo = CType(raPrtData(ix), STU_CollectInfo)
            Dim cpi As New STU_PatInfo

            With cpi
                .REGNO = collData.REGNO
                .PATNM = collData.PATNM
                .SEX = collData.SEX
                .AGE = collData.AGE
                .DEPTNM = collData.DEPTNM
                .DOCTORNM = collData.DOCTORNM
                .WARD = collData.WARDNO
                .ROOMNO = collData.ROOMNO
            End With

            prt.cpi = cpi
            prt.sbPrint(collData.CPRTGBN)
        Next

    End Sub


    Public Function CollectSelOrder(ByRef noSunabList As ArrayList, _
                                    ByVal rsFormName As String, _
                                    ByVal rsRegNo As String, _
                                    ByVal rsIoGbn As String, _
                                    ByVal rsDeptOrWard As String, _
                                    ByVal rsPartGbn As String, _
                                    ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                    ByVal rbToColl As Boolean, ByVal rbAutoTk As Boolean, _
                                    ByVal rbNotBcPrt As Boolean) As ArrayList
        Dim sFn As String = "Public Function CollectSelOrder() As ArrayList"

        Dim iCnt As Integer = 0

        Dim diagData As STU_DiagInfo

        Dim al_diagData As New ArrayList
        Dim al_prtPatData As New ArrayList
        Dim alCPrtGbn As New ArrayList

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim al_return As ArrayList

        Try
            If msCollUsrId = "" Then
                MsgBox("채혈자 아이디가 존재하지 않습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)

                Return Nothing
            End If

            If Fn.SpdColSearch(spd, "1", spd.GetColFromID("chk")) = 0 Then
                If mbCollBatch = False Then
                    sbLog_Msg("", sFn + " : " + "채혈을 위해 선택된 검사항목이 없습니다. 확인하여 주십시요!!")
                End If

                Return Nothing
            End If

            Dim stu As New STU_COLLINFO

            stu.REGNO = rsRegNo
            stu.IOGBN = rsIoGbn
            If rsIoGbn = "I" Then
                stu.WARDCD = rsDeptOrWard
            Else
                stu.DEPTCD = rsDeptOrWard
            End If

            stu.ORDDT1 = rsOrdDtS.Replace("-", "")
            stu.ORDDT2 = rsOrdDtE.Replace("-", "")
            stu.SPCFLG1 = "0"
            stu.SPCFLG2 = "0"
            stu.PARTGBN = rsPartGbn

            '< 데이터 변경 여부 조사
            Dim bChange As Boolean = fnFind_Exist_Change(m_dt_ord, fnGet_OrderData(stu, False))

            If bChange Then
                If mbCollBatch = False Then
                    MsgBox("등록번호 : " + m_so.RegNo + "의 Order 정보가 변경되었습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)
                Else
                    sbLog_Msg("변경", "등록번호 : " + m_so.RegNo + "의 Order 정보가 변경되었습니다.")
                End If

                Return Nothing
            End If
            '>

            Dim dtSysDt As Date = (New LISAPP.APP_DB.ServerDateTime).GetDateTime()

            Dim al_BcInfo As New ArrayList
            Dim al_BcInfo_NoSunab As New ArrayList

            Dim iMaxGrpNo As Integer = spd.GetRowItemData(spd.MaxRows)

            With spd
                Dim iRowE As Integer = 0

                Dim listCollData_pre As New List(Of STU_CollectInfo)
                Dim listCollData_NoSunab_pre As New List(Of STU_CollectInfo)

                For g As Integer = 1 To iMaxGrpNo
                    Dim iRowB As Integer = 0

                    Dim listCollData As New List(Of STU_CollectInfo)
                    Dim listCollData_NoSunab As New List(Of STU_CollectInfo)

                    iRowB = iRowE + 1

                    miPints = 0

                    For i As Integer = iRowB To .MaxRows
                        Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)
                        Dim sSuNabYn As String = Ctrl.Get_Code(spd, "sunab_yn", i)
                        Dim sDeptCd As String = Ctrl.Get_Code(spd, "deptcd", i)

                        Dim collData As New STU_CollectInfo

                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            If sChk = "1" And (sSuNabYn = "Y" Or PRG_CONST.DEPT_NOSUNAB.IndexOf(sDeptCd + ",") >= 0) Then
                                collData = fnFind_collData(i, dtSysDt)

                                If collData IsNot Nothing Then
                                    listCollData.Add(collData)

                                    '-- 관련서식 추가
                                    If collData.CPRTGBN > "0" Then
                                        If alCPrtGbn.Contains(collData.CPRTGBN) = False Then
                                            al_prtPatData.Add(collData)
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Exit For
                        End If

                        collData = Nothing
                    Next

                    '> 연속검사 샘플 -> True, 아니면 False
                    Dim bSeries As Boolean = False

                    If listCollData.Count > 0 Then
                        If listCollData_pre.Count > 0 Then
                            If listCollData.Item(0).BCKEY3 = listCollData_pre.Item(0).BCKEY3 Then
                                If listCollData.Item(0).SEQTMI <> listCollData_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo.Add(listCollData)

                    End If

                    If listCollData.Count > 0 Then
                        listCollData_pre = listCollData
                    End If

                    listCollData = Nothing

                    '< add yjlee 2009-05-29 
                    If listCollData_NoSunab.Count > 0 Then
                        If listCollData_NoSunab_pre.Count > 0 Then
                            If listCollData_NoSunab.Item(0).BCKEY3 = listCollData_NoSunab_pre.Item(0).BCKEY3 Then
                                If listCollData_NoSunab.Item(0).SEQTMI <> listCollData_NoSunab_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData_NoSunab
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo_NoSunab.Add(listCollData_NoSunab)
                    End If

                    If listCollData_NoSunab.Count > 0 Then
                        listCollData_NoSunab_pre = listCollData_NoSunab
                    End If

                    listCollData_NoSunab = Nothing
                    '> 
                Next

                If m_cpi.DIAG_K <> "" Or m_cpi.DIAG_E <> "" Then
                    diagData = New STU_DiagInfo

                    With diagData
                        .DIAGNM = m_cpi.DIAG_K
                        .DIAGNM_ENG = m_cpi.DIAG_E
                    End With

                    al_diagData.Add(diagData)
                End If

            End With


            If al_BcInfo_NoSunab.Count > 0 Then
                '수납 안된 환자에 대한 처리 
                Dim al As New ArrayList

                For i As Integer = 0 To al_BcInfo_NoSunab.Count - 1
                    Dim listcollData As List(Of STU_CollectInfo) = CType(al_BcInfo_NoSunab(i), List(Of STU_CollectInfo))
                    al.Add(listcollData)
                Next

                noSunabList = al
            End If

            If al_BcInfo.Count < 1 Then Return Nothing

            With (New LISAPP.APP_C.CollReg)
                al_return = .ExecuteDo(al_BcInfo, al_diagData, rsFormName, "", rbToColl, rbAutoTk, True)
            End With

            If al_return.Count > 0 Then sbPrint_Document(al_prtPatData)

            Return al_return

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)
            Return Nothing

        End Try
    End Function

    Public Function CollectSelOrder_Web(ByRef noSunabList As ArrayList,
                                        ByVal rsFormName As String,
                                        ByVal rsRegNo As String,
                                        ByVal rsIoGbn As String,
                                        ByVal rsDeptOrWard As String,
                                        ByVal rsPartGbn As String,
                                        ByVal rsOrdDtS As String, ByVal rsOrdDtE As String,
                                        ByVal rbToColl As Boolean, ByVal rbAutoTk As Boolean,
                                        ByVal rbNotBcPrt As Boolean,
                                        Optional ByVal rsBolPrntNum As Boolean = False, Optional ByVal rsPrntNum As String = "0") As ArrayList
        Dim sFn As String = "Public Function CollectSelOrder() As ArrayList"

        Dim iCnt As Integer = 0

        Dim stu_diagData As New STU_DiagInfo

        Dim al_prtPatData As New ArrayList
        Dim alCPrtGbn As New ArrayList

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim al_return As ArrayList

        Try
            If msCollUsrId = "" Then
                MsgBox("채혈자 아이디가 존재하지 않습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)

                Return Nothing
            End If

            If Fn.SpdColSearch(spd, "1", spd.GetColFromID("chk")) = 0 Then
                If mbCollBatch = False Then
                    sbLog_Msg("", sFn + " : " + "채혈을 위해 선택된 검사항목이 없습니다. 확인하여 주십시요!!")
                End If

                Return Nothing
            End If

            Dim stu As New STU_COLLINFO

            stu.REGNO = rsRegNo
            stu.IOGBN = rsIoGbn
            If rsIoGbn = "I" Then
                stu.WARDCD = rsDeptOrWard
            Else
                stu.DEPTCD = rsDeptOrWard
            End If

            stu.ORDDT1 = rsOrdDtS.Replace("-", "")
            stu.ORDDT2 = rsOrdDtE.Replace("-", "")
            stu.SPCFLG1 = "0"
            stu.SPCFLG2 = "0"
            stu.PARTGBN = rsPartGbn

            '< 데이터 변경 여부 조사
            Dim bChange As Boolean = fnFind_Exist_Change(m_dt_ord, fnGet_OrderData(stu, False))

            If bChange Then
                If mbCollBatch = False Then
                    MsgBox("등록번호 : " + m_so.RegNo + "의 Order 정보가 변경되었습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)
                Else
                    sbLog_Msg("변경", "등록번호 : " + m_so.RegNo + "의 Order 정보가 변경되었습니다.")
                End If

                Return Nothing
            End If
            '>

            Dim dtSysDt As Date = (New LISAPP.APP_DB.ServerDateTime).GetDateTime()

            Dim al_BcInfo As New ArrayList
            Dim al_BcInfo_NoSunab As New ArrayList

            Dim iMaxGrpNo As Integer = spd.GetRowItemData(spd.MaxRows)

            With spd
                Dim iRowE As Integer = 0

                Dim listCollData_pre As New List(Of STU_CollectInfo)
                Dim listCollData_NoSunab_pre As New List(Of STU_CollectInfo)

                For g As Integer = 1 To iMaxGrpNo
                    Dim iRowB As Integer = 0

                    Dim listCollData As New List(Of STU_CollectInfo)
                    Dim listCollData_NoSunab As New List(Of STU_CollectInfo)

                    iRowB = iRowE + 1

                    miPints = 0

                    For i As Integer = iRowB To .MaxRows
                        Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)
                        Dim sSuNabYn As String = Ctrl.Get_Code(spd, "sunab_yn", i)
                        Dim sDeptCd As String = Ctrl.Get_Code(spd, "deptcd", i)
                        Dim sOpenCard As String = Ctrl.Get_Code(spd, "opencard", i)

                        Dim collData As New STU_CollectInfo

                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            If sChk = "1" And (sSuNabYn = "Y" Or PRG_CONST.DEPT_NOSUNAB.IndexOf(sDeptCd + ",") >= 0) Then
                                'collData = fnFind_collData(i, dtSysDt) 
                                collData = fnFind_collData(i, dtSysDt, False, rsBolPrntNum, rsPrntNum) ' 20211104 jhs 프린터 추가

                                If collData IsNot Nothing Then
                                    listCollData.Add(collData)

                                    '-- 관련서식 추가
                                    If collData.CPRTGBN > "0" Then
                                        If alCPrtGbn.Contains(collData.CPRTGBN) = False Then
                                            al_prtPatData.Add(collData)
                                        End If
                                    End If
                                End If
                            ElseIf sChk = "1" And (sSuNabYn = "N" Or PRG_CONST.DEPT_NOSUNAB.IndexOf(sDeptCd + ",") >= 0) And sOpenCard = "Y" Then ' 20150909 오픈카드 관련 수정
                                'collData = fnFind_collData(i, dtSysDt) 
                                collData = fnFind_collData(i, dtSysDt, False, rsBolPrntNum, rsPrntNum) ' 20211104 jhs 프린터 추가

                                If collData IsNot Nothing Then
                                    listCollData.Add(collData)

                                    '-- 관련서식 추가
                                    If collData.CPRTGBN > "0" Then
                                        If alCPrtGbn.Contains(collData.CPRTGBN) = False Then
                                            al_prtPatData.Add(collData)
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            Exit For
                        End If

                        collData = Nothing
                    Next

                    '> StatGbn 처리
                    Dim sStatGbn As String = ""
                    For Each collData As STU_CollectInfo In listCollData
                        If collData.STATGBN <> "" Then
                            sStatGbn = collData.STATGBN
                            Exit For
                        End If
                    Next

                    If sStatGbn <> "" Then
                        For Each collData As STU_CollectInfo In listCollData
                            collData.STATGBN = sStatGbn
                        Next
                    End If

                    '> 연속검사 샘플 -> True, 아니면 False
                    Dim bSeries As Boolean = False

                    If listCollData.Count > 0 Then
                        If listCollData_pre.Count > 0 Then
                            If listCollData.Item(0).BCKEY3 = listCollData_pre.Item(0).BCKEY3 Then
                                If listCollData.Item(0).SEQTMI <> listCollData_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo.Add(listCollData)
                    End If

                    If listCollData.Count > 0 Then
                        listCollData_pre = listCollData
                    End If

                    listCollData = Nothing

                    '< add yjlee 2009-05-29 
                    If listCollData_NoSunab.Count > 0 Then
                        If listCollData_NoSunab_pre.Count > 0 Then
                            If listCollData_NoSunab.Item(0).BCKEY3 = listCollData_NoSunab_pre.Item(0).BCKEY3 Then
                                If listCollData_NoSunab.Item(0).SEQTMI <> listCollData_NoSunab_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData_NoSunab
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo_NoSunab.Add(listCollData_NoSunab)
                    End If

                    If listCollData_NoSunab.Count > 0 Then
                        listCollData_NoSunab_pre = listCollData_NoSunab
                    End If

                    listCollData_NoSunab = Nothing
                    '> 
                Next

                If m_cpi.DIAG_K <> "" Or m_cpi.DIAG_E <> "" Then
                    stu_diagData = New STU_DiagInfo

                    With stu_diagData
                        .DIAGNM = m_cpi.DIAG_K
                        .DIAGNM_ENG = m_cpi.DIAG_E
                    End With

                End If

            End With


            If al_BcInfo_NoSunab.Count > 0 Then
                '수납 안된 환자에 대한 처리 
                Dim al As New ArrayList

                For i As Integer = 0 To al_BcInfo_NoSunab.Count - 1
                    Dim listcollData As List(Of STU_CollectInfo) = CType(al_BcInfo_NoSunab(i), List(Of STU_CollectInfo))
                    al.Add(listcollData)
                Next

                noSunabList = al
            End If

            If al_BcInfo.Count < 1 Then Return Nothing
            '20220121 jhs 중복처방 존재하는지 확인 로직 구현
            'Dim msgContent As String = CollectSelOrder_chk_Dpl_Ord(al_BcInfo)

            'If fn_PopConfirm(moForm, "E"c, "중복검사 " + msgContent + "가 존재합니다." + vbCrLf + "계속 진행 하시겠습니까?") = False Then Return Nothing
            '--------------------------------------

            With (New LISAPP.APP_C.CollReg_Web)
                al_return = .ExecuteDo(al_BcInfo, stu_diagData, rsFormName, "", rbToColl, rbAutoTk, True)
            End With

            'If al_return.Count > 0 Then sbPrint_Document(al_prtPatData)

            Return al_return

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)
            Return Nothing
        End Try
    End Function
    '20220121 jhs 중복처방 존재하는지 확인 로직 구현l
    Public Function CollectSelOrder_chk_Dpl_Ord(ByVal rsCollList As ArrayList) As String
        Dim sFn As String = "Public Function CollectSelOrder_chk_Dpl_Ord() As ArrayList"
        Dim sTestCdsStr As String = ""
        Dim rsTestCd As String = ""
        Dim rsSpcCd As String = ""
        Dim totalDuplTestCd As String = ""
        Dim alTestCds As New List(Of String)
        Dim alTempTestCds As List(Of String)

        Try

            For i = 0 To rsCollList.Count - 1
                Dim tempObj As List(Of STU_CollectInfo) = CType(rsCollList(i), List(Of STU_CollectInfo))
                rsTestCd = tempObj(0).TORDCD
                rsSpcCd = tempObj(0).SPCCD
                Dim tempStr As String = LISAPP.APP_C.Collfn.Fn_Chk_testcd(rsTestCd, rsSpcCd)
                sTestCdsStr += "," + tempStr
                alTestCds.Insert(i, tempStr)
            Next

            For i = 0 To alTestCds.Count - 1
                alTempTestCds = alTestCds.ToList
                alTempTestCds.RemoveAt(i)
                Dim sEcptTestCds As String = ""

                For z = 0 To alTempTestCds.Count - 1
                    sEcptTestCds += "," + alTempTestCds(z)
                Next

                Dim slEcptTestCds() As String = Mid(sEcptTestCds.Replace(",,", ","), 2, sEcptTestCds.Length).Split(","c)
                Dim slSelTestCds() As String = alTestCds(i).ToString.Replace(",,", ",").Split(","c)

                For x = 0 To slSelTestCds.Count - 1
                    For j = 0 To slEcptTestCds.Count - 1
                        If slSelTestCds(x) = slEcptTestCds(j) Then
                            totalDuplTestCd += "," + slSelTestCds(x)
                        End If
                    Next
                Next
            Next

            totalDuplTestCd = LISAPP.APP_C.Collfn.Fn_Combine_TestCd(totalDuplTestCd)

            If totalDuplTestCd.Length > 0 Then
                Return Mid(totalDuplTestCd, 1, totalDuplTestCd.Length - 1)
            Else
                Return ""
            End If

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)
        End Try
    End Function
    '---------------------------------------------------------

    Public Function CollectSelOrder_Batch(ByVal rsFormName As String, ByVal rsIoGbn As String, ByVal rsDptOrWard As String, _
                                          ByVal rbToColl As Boolean, ByVal rbToTk As Boolean, ByVal rbNotBcPrt As Boolean) As ArrayList
        Dim sFn As String = "Public Function CollectSelOrder() As ArrayList"

        Dim iCnt As Integer = 0

        Dim dt As New DataTable

        Dim stu_diagData As New STU_DiagInfo

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim al_return As ArrayList
        Dim al_Treturn As New ArrayList

        Dim dtSysDt As Date = (New LISAPP.APP_DB.ServerDateTime).GetDateTime()

        Try
            If msCollUsrId = "" Then
                MsgBox("채혈자 아이디가 존재하지 않습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)

                Return Nothing
            End If

            If Fn.SpdColSearch(spd, "1", spd.GetColFromID("chk")) = 0 Then
                If mbCollBatch = False Then
                    sbLog_Msg("", sFn + " : " + "채혈을 위해 선택된 검사항목이 없습니다. 확인하여 주십시요!!")
                End If

                Return Nothing
            End If

            '''< 데이터 변경 여부 조사
            ''Dim bChange As Boolean = fnFind_Exist_Change(m_dt_ord, fnGet_OrderData(m_so))

            ''If bChange Then
            ''    If mbCollBatch = False Then
            ''        MsgBox(Label_RegNo + " : " + m_so.RegNo + "의 Order 정보가 변경되었습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)
            ''    Else
            ''        sbLog_Msg("변경", Label_RegNo + " : " + m_so.RegNo + "의 Order 정보가 변경되었습니다.")
            ''    End If

            ''    Return Nothing
            ''End If
            '''>

            Dim al_BcInfo As New ArrayList

            With spd
                Dim iRowE As Integer = 0

                Dim listCollData_pre As New List(Of STU_CollectInfo)
                Dim iMaxGrpNo As Integer = spd.GetRowItemData(spd.MaxRows)

                For g As Integer = 1 To iMaxGrpNo
                    dtSysDt = (New LISAPP.APP_DB.ServerDateTime).GetDateTime()

                    al_BcInfo = New ArrayList
                    al_return = New ArrayList

                    Dim iRowB As Integer = 0

                    Dim listCollData As New List(Of STU_CollectInfo)
                    Dim al_prtPatData As New ArrayList

                    iRowB = iRowE + 1

                    miPints = 0

                    For i As Integer = iRowB To .MaxRows
                        Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)
                        Dim collData As New STU_CollectInfo
                        Dim sSuNabYn As String = Ctrl.Get_Code(spd, "sunab_yn", i)
                        Dim sDeptCd As String = Ctrl.Get_Code(spd, "deptcd", i)

                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            If sChk = "1" And (sSuNabYn = "Y" Or PRG_CONST.DEPT_NOSUNAB.IndexOf(sDeptCd + ",") >= 0) Then
                                .Row = i
                                If i = iRowB Or m_cpi Is Nothing Then
                                    m_cpi = fnGet_PatList(i)
                                End If

                                collData = fnFind_collData(i, dtSysDt)

                                If collData IsNot Nothing Then
                                    listCollData.Add(collData)
                                End If
                            End If
                        Else
                            Exit For
                        End If

                        collData = Nothing
                    Next

                    '> 연속검사 샘플 -> True, 아니면 False
                    Dim bSeries As Boolean = False

                    If listCollData.Count > 0 Then
                        If listCollData_pre.Count > 0 Then
                            If listCollData.Item(0).BCKEY3 = listCollData_pre.Item(0).BCKEY3 Then
                                If listCollData.Item(0).SEQTMI <> listCollData_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo.Add(listCollData)

                    End If

                    If listCollData.Count > 0 Then
                        listCollData_pre = listCollData
                    End If

                    listCollData = Nothing

                    Dim al_diagData As New ArrayList

                    If al_BcInfo.Count > 0 Then
                        If m_cpi.DIAG_K <> "" Or m_cpi.DIAG_E <> "" Then
                            stu_diagData = New STU_DiagInfo

                            With stu_diagData
                                .DIAGNM = m_cpi.DIAG_K
                                .DIAGNM_ENG = m_cpi.DIAG_E
                            End With

                            al_diagData.Add(stu_diagData)
                        End If

                        Dim dtSysDtE As Date = (New LISAPP.APP_DB.ServerDateTime).GetDateTime()

                        Fn.logFile(Format(Now, "yyyy-MM-dd hh:mm:ss") & " 시작 ", "채혈테스트")

                        'With (New LISAPP.APP_C.CollReg)
                        '    al_return = .ExecuteDo(al_BcInfo, al_diagData, rsFormName, "", rbToColl, rbToTk, True)

                        '    If al_return.Count > 0 Then
                        '        sbPrint_Document(al_prtPatData)

                        '        For intIdx As Integer = 0 To al_return.Count - 1
                        '            al_Treturn.Add(al_return.Item(intIdx))
                        '        Next
                        '    End If
                        'End With

                        With (New LISAPP.APP_C.CollReg_Web)
                            al_return = .ExecuteDo(al_BcInfo, stu_diagData, rsFormName, "", rbToColl, False, True)

                            If al_return.Count > 0 Then
                                For intIdx As Integer = 0 To al_return.Count - 1
                                    al_Treturn.Add(al_return.Item(intIdx))
                                Next
                            End If

                        End With


                        Fn.logFile(vbCrLf, "채혈테스트")
                    End If

                    al_BcInfo = Nothing
                    al_return = Nothing
                Next
            End With

            If al_Treturn.Count < 1 Then Return Nothing

            Return al_Treturn

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

            Return al_Treturn

        End Try
    End Function

    Public Function CollectSelOrder_Doner(ByRef noSunabList As ArrayList, ByVal rbBcPrt As Boolean) As ArrayList
        Dim sFn As String = "Public Function CollectSelOrder() As ArrayList"

        Dim iCnt As Integer = 0

        Dim diagData As STU_DiagInfo

        Dim al_collData As New ArrayList
        Dim al_diagData As New ArrayList

        Dim sIBDay As String = ""

        Dim sOwnGbn As String = ""

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim al_return As ArrayList

        Try
            If msCollUsrId = "" Then
                MsgBox("채혈자 아이디가 존재하지 않습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)

                Return Nothing
            End If

            If Fn.SpdColSearch(spd, "1", spd.GetColFromID("chk")) = 0 Then
                If mbCollBatch = False Then
                    sbLog_Msg("", sFn + " : " + "채혈을 위해 선택된 검사항목이 없습니다. 확인하여 주십시요!!")
                End If

                Return Nothing
            End If

            '''< 데이터 변경 여부 조사
            ''Dim bChange As Boolean = fnFind_Exist_Change(m_dt_ord, fnGet_OrderData(m_so))

            ''If bChange Then
            ''    If mbCollBatch = False Then
            ''        MsgBox(Label_RegNo + " : " + m_so.RegNo + "의 Order 정보가 변경되었습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)
            ''    Else
            ''        sbLog_Msg("변경", Label_RegNo + " : " + m_so.RegNo + "의 Order 정보가 변경되었습니다.")
            ''    End If

            ''    Return Nothing
            ''End If
            '''>

            Dim dtSysDt As Date = (New LISAPP.APP_DB.ServerDateTime).GetDateTime()

            Dim al_BcInfo As New ArrayList
            Dim al_BcInfo_NoSunab As New ArrayList

            Dim iMaxGrpNo As Integer = spd.GetRowItemData(spd.MaxRows)

            With spd
                Dim iRowE As Integer = 0

                Dim listCollData_pre As New List(Of STU_CollectInfo)
                Dim listCollData_NoSunab_pre As New List(Of STU_CollectInfo)

                For g As Integer = 1 To iMaxGrpNo
                    Dim iRowB As Integer = 0

                    Dim listCollData As New List(Of STU_CollectInfo)
                    Dim listCollData_NoSunab As New List(Of STU_CollectInfo)

                    iRowB = iRowE + 1

                    miPints = 0

                    For i As Integer = iRowB To .MaxRows
                        Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)

                        Dim collData As New STU_CollectInfo

                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            If sChk = "1" Then
                                collData = fnFind_collData(i, dtSysDt)

                                If collData IsNot Nothing Then
                                    If collData.SUNABYN = "N" Then
                                        listCollData_NoSunab.Add(collData)
                                    ElseIf collData.SUNABYN = "Y" Then
                                        listCollData.Add(collData)
                                    End If
                                End If
                            End If
                        Else
                            Exit For
                        End If

                        collData = Nothing
                    Next

                    '> 연속검사 샘플 -> True, 아니면 False
                    Dim bSeries As Boolean = False

                    If listCollData.Count > 0 Then
                        If listCollData_pre.Count > 0 Then
                            If listCollData.Item(0).BCKEY3 = listCollData_pre.Item(0).BCKEY3 Then
                                If listCollData.Item(0).SEQTMI <> listCollData_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo.Add(listCollData)

                    End If

                    If listCollData.Count > 0 Then
                        listCollData_pre = listCollData
                    End If

                    listCollData = Nothing

                    '< add yjlee 2009-05-29 
                    If listCollData_NoSunab.Count > 0 Then
                        If listCollData_NoSunab_pre.Count > 0 Then
                            If listCollData_NoSunab.Item(0).BCKEY3 = listCollData_NoSunab_pre.Item(0).BCKEY3 Then
                                If listCollData_NoSunab.Item(0).SEQTMI <> listCollData_NoSunab_pre.Item(0).SEQTMI Then
                                    bSeries = True
                                Else
                                    bSeries = False
                                End If
                            Else
                                bSeries = False
                            End If

                            If bSeries Then
                                For Each collData As STU_CollectInfo In listCollData_NoSunab
                                    collData.SERIES = bSeries
                                Next
                            End If
                        End If

                        al_BcInfo_NoSunab.Add(listCollData_NoSunab)
                    End If

                    If listCollData_NoSunab.Count > 0 Then
                        listCollData_NoSunab_pre = listCollData_NoSunab
                    End If

                    listCollData_NoSunab = Nothing
                    '> 
                Next

                If m_cpi.DIAG_K <> "" Or m_cpi.DIAG_E <> "" Then
                    diagData = New STU_DiagInfo

                    With diagData
                        .DIAGNM = m_cpi.DIAG_K
                        .DIAGNM_ENG = m_cpi.DIAG_E
                    End With

                    al_diagData.Add(diagData)
                End If

            End With


            If al_BcInfo_NoSunab.Count > 0 Then
                '수납 안된 환자에 대한 처리 
                Dim al As New ArrayList

                For i As Integer = 0 To al_BcInfo_NoSunab.Count - 1
                    Dim listcollData As List(Of STU_CollectInfo) = CType(al_BcInfo_NoSunab(i), List(Of STU_CollectInfo))
                    al.Add(listcollData)
                Next

                noSunabList = al
            End If

            If al_BcInfo.Count < 1 Then Return Nothing

            With (New LISAPP.APP_C.CollReg)
                al_return = .ExecuteDo(al_BcInfo, al_diagData, "", "", True, True, rbBcPrt)
            End With

            Return al_return

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

            Return Nothing

        End Try
    End Function

    Public Function CommentSelOrder() As Boolean
        Dim sFn As String = "Public Function CommentSelOrder() As ArrayList"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim bReturn As Boolean = False

        Dim sIBDay As String = ""

        Try
            If msCollUsrId = "" Then
                MsgBox("채혈자 아이디가 존재하지 않습니다. 확인하여 주십시요!!", MsgBoxStyle.Information, Me.Text)

                Return Nothing
            End If

            Dim listCollData As New List(Of STU_CollectInfo)

            With spd
                For i As Integer = 1 To .MaxRows
                    Dim sLabCmt As String = Ctrl.Get_Code(spd, "liscmt", i)

                    Dim collData As STU_CollectInfo

                    If sLabCmt.Length > 0 Then
                        collData = New STU_CollectInfo

                        collData = fnFind_collData(i, Now)

                        listCollData.Add(collData)

                        collData = Nothing
                    End If
                Next
            End With

            If listCollData.Count < 1 Then Return False

            With (New LISAPP.APP_C.CollReg)
                bReturn = .ExecuteDo_Comment(listCollData)
            End With

            Return bReturn

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

            Return False

        End Try
    End Function

    Public Sub sbDisplay_NoOrder(ByVal rsRegNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String)
        Me.spdOrdList.MaxRows = 0

        Dim sMsg As String = ""

        sMsg += "등록번호 : " + rsRegNo
        sMsg += ", " + "처방일 : " + IIf(rsOrdDtS = rsOrdDtE, rsOrdDtE, rsOrdDtS + " ~ " + rsOrdDtE).ToString
        sMsg += vbCrLf + "의 처방내역이 존재하지 않습니다!!"


        Me.lblMsg.Text = sMsg
        Me.lblMsg.Visible = True
        Me.Refresh()

    End Sub


    Public Sub DisplayOrder(ByVal r_stu As STU_COLLINFO, ByVal rbHopeday As Boolean)

        Dim sFn As String = "Public Sub DisplayOrder()"

        Try
            mbMergeMode = False

            Clear()
            Me.Refresh()

            Dim dt As DataTable = fnGet_OrderData(r_stu, rbHopeday)

            If dt.Rows.Count < 1 Then
                sbDisplay_NoOrder(r_stu.REGNO, r_stu.ORDDT1, r_stu.ORDDT2)
                Return
            End If

            '> DataTable 사본 저장
            m_dt_ord = dt.Copy()

            If mbSearchMode Then
                sbDisplayOrder_Detail(dt, True)
            Else
                sbDisplayOrder_Detail(dt)
            End If

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

        End Try
    End Sub

    Private Function fnGet_OrderData(ByVal r_stu As STU_COLLINFO, ByVal rbHopeday As Boolean) As DataTable
        Dim sFn As String = "Private Function fnGet_OrderData() As DataTable"

        Try
            Dim dt As DataTable = (New CGWEB_C).fnGet_OrdList(r_stu, mbSearchMode, rbHopeday)

            Return dt

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

            Return (New DataTable)

        End Try
    End Function

    Private Function fnGet_Order_AboRhYn(ByVal rsRegNo As String, ByVal rsOrdDt As String) As String
        Dim sFn As String = "Private Function fnGet_OrderData() As DataTable"

        Try
            rsOrdDt = rsOrdDt.Replace("-", "").Replace(":", "").Replace(" ", "").Substring(0, 8)
            Return Collfn.FindOrder_AboRh(rsRegNo, rsOrdDt)

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

            Return ""

        End Try
    End Function

    Protected Sub sbDisplayOrder_Detail(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub sbDisplayOrder_Detail(DataTable)"

        Try
            mbSkip = True

            Dim sRoomC As String = ""
            Dim sPatNameC As String = ""
            Dim sRoomP As String = ""
            Dim sPatNameP As String = ""

            Dim sBcKeyC As String = ""
            Dim sBcKeyP As String = ""
            Dim sRegNoC As String = ""
            Dim sRegNoP As String = ""

            '< yjlee  
            Dim sTclsCdC As String = ""
            Dim sTclsCdP As String = ""

            Dim sBuf As String = ""
            '> 

            Dim bGrpCheck As Boolean = False

            Dim iGrpNo As Integer = 0

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

            With spd
                .ReDraw = False
                Clear()

                .MaxRows = r_dt.Rows.Count


                Dim sFirstHopeDay As String = ""

                If r_dt.Rows(0).Item("hopeday").ToString <> r_dt.Rows(0).Item("ordday").ToString Then
                    sFirstHopeDay = r_dt.Rows(0).Item("ordday").ToString
                Else
                    sFirstHopeDay = r_dt.Rows(0).Item("hopeday").ToString
                End If

                'Dim sSeqtYn As String = "0"
                'Dim iSeqtMi As Integer = -1

                For i As Integer = 1 To r_dt.Rows.Count
                    '-----------------------20170714 jjh2 ----------------------------- 식사관련 채혈 주의

                    Dim Stestcd As String = r_dt.Rows(i - 1).Item("testcd").ToString().Trim
                    Dim Sspccd As String = r_dt.Rows(i - 1).Item("spccd").ToString().Trim

                    Dim dt As DataTable = LISAPP.APP_C.Collfn.Fn_FoodWaring_C(Stestcd, Sspccd)

                    If dt.Rows.Count > 0 Then '20170718 yhj
                        If dt.Rows(0).Item("fwgbn").ToString = "1" Then
                            .Col = .GetColFromID("bcclscd") : .Row = i : .BackColor = Color.Yellow

                        End If
                    End If

                    '-----------------------------------------------------------------


                    '-----------------------20170920 jjh2 -----------------------------  채혈 주의 사항 관련 팝업 자료 가져오기.

                    Dim dt2 As DataTable = LISAPP.APP_C.Collfn.Fn_CWaring_C(Stestcd, Sspccd)

                    If dt2.Rows.Count > 0 Then '20170920 jjh
                        If dt2.Rows(0).Item("cwgbn").ToString = "1" Then
                            ' MsgBox("검사명 : " + dt2.Rows(0).Item("tnmd").ToString + " 확인 바랍니다. ", MsgBoxStyle.OkOnly, "채혈시 주의사항 확인요망.")
                            MsgBox("검사명 : " + dt2.Rows(0).Item("tnmd").ToString + vbCrLf + dt2.Rows(0).Item("cwarning").ToString + "확인 바랍니다. ", MsgBoxStyle.OkOnly, "채혈시 주의사항 확인요망.")
                        End If
                    End If

                    '-----------------------------------------------------------------


                    For ix2 As Integer = 0 To r_dt.Columns.Count - 1
                        If r_dt.Rows(i - 1).Item(ix2).ToString = "-" Then r_dt.Rows(i - 1).Item(ix2) = ""
                    Next

                    'BcKey : hopeday, exlabcd, bcclscd, spccd, tubecd, seqtmi, ordday, [regno]

                    Dim sOrdDt As String = r_dt.Rows(i - 1).Item("orddt").ToString.Substring(0, 10)

                    'If r_dt.Rows(i - 1).Item("seqtyn").ToString = "1" Then
                    '    If sSeqtYn <> "0" Then iSeqtMi += 1
                    'Else
                    '    iSeqtMi = 0
                    'End If

                    'sSeqtYn = r_dt.Rows(i - 1).Item("seqtyn").ToString()

                    sBcKeyC = ""

                    sBcKeyC += r_dt.Rows(i - 1).Item("iogbn").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("deptcd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("wardno").ToString.Trim + "/"

                    If mbCollBatch Then
                        sBcKeyC += r_dt.Rows(i - 1).Item("regno").ToString.Trim + "/"

                        sRegNoC = r_dt.Rows(i - 1).Item("regno").ToString.Trim
                    End If

                    'sBcKeyC += r_dt.Rows(i - 1).Item("hopeday").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("exlabcd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("bcclscd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("spccd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("tubecd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("poctyn").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("bconeyn").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("seqtmi").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("ordday").ToString.Trim

                    'BcKey2 : hopeday, exlabcd, sectcd, tsectcd, spccd, tubecd, seqtmi, [regno] -> 처방일시가 다르지만 합쳐질 수 있는 경우
                    'BcKey2 : hopeday, exlabcd, sectcd, tsectcd, spccd, tubecd, seqtmi, [regno] -> 처방일시, 진료과,처방의가 다르지만 합쳐질 수 있는 경우(국립의료원)
                    Dim sBcKey2 As String = ""

                    sBcKey2 = ""

                    sBcKey2 += r_dt.Rows(i - 1).Item("iogbn").ToString.Trim + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("ordday").ToString.Trim + "/"
                    'sBcKey2 += r_dt.Rows(i - 1).Item("deptcd").ToString.Trim + "/"
                    'sBcKey2 += r_dt.Rows(i - 1).Item("wardno").ToString.Trim + "/"

                    If mbCollBatch Then
                        sBcKey2 += r_dt.Rows(i - 1).Item("regno").ToString.Trim + "/"
                    End If

                    sBcKey2 += r_dt.Rows(i - 1).Item("exlabcd").ToString.Trim + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("bcclscd").ToString.Trim + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("spccd").ToString.Trim + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("tubecd").ToString.Trim + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("poctyn").ToString.Trim + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("bconeyn").ToString.Trim
                    sBcKey2 += r_dt.Rows(i - 1).Item("seqtmi").ToString + "/"

                    'BcKey3 : hopeday, exlabcd, sectcd, tsectcd, spccd, tubecd -> 동일조건의 연속검사 샘플 판별용
                    Dim sBcKey3 As String = ""

                    sBcKey3 = ""
                    sBcKey3 += r_dt.Rows(i - 1).Item("iogbn").ToString.Trim + "/"
                    If mbCollBatch Then
                        sBcKey3 += r_dt.Rows(i - 1).Item("regno").ToString.Trim + "/"
                    End If
                    sBcKey3 += r_dt.Rows(i - 1).Item("exlabcd").ToString.Trim + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("bcclscd").ToString.Trim + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("spccd").ToString.Trim + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("tubecd").ToString.Trim + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("poctyn").ToString.Trim + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("bconeyn").ToString.Trim
                    'sBcKey3 += r_dt.Rows(i - 1).Item("deptcd").ToString.Trim + "/"
                    'sBcKey3 += r_dt.Rows(i - 1).Item("wardno").ToString.Trim + "/"


                    '< yjlee 
                    sTclsCdC = r_dt.Rows(i - 1).Item("dtestcd").ToString.Trim

                    .Row = i
                    .Col = .GetColFromID("grpno") : .Text = ""

                    .Col = .GetColFromID("regno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("regno").ToString().Trim : .ForeColor = Color.White ': sRegNoC = .Text
                    .Col = .GetColFromID("patnm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("patinfo").ToString().Split("|"c)(0).Trim : .ForeColor = Color.White
                    .Col = .GetColFromID("patinfo") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("patinfo").ToString().Trim : .ForeColor = Color.White
                    .Col = .GetColFromID("roomno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("roomno").ToString().Trim : .ForeColor = Color.White ': sRoomC = .Text

                    .Col = .GetColFromID("orddt") : If .Col > -1 Then .ForeColor = Color.White : .Text = r_dt.Rows(i - 1).Item("orddt").ToString.Trim
                    .Col = .GetColFromID("hopeday") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("hopeday").ToString.Trim
                    .Col = .GetColFromID("deptcd") : If .Col > -1 Then .ForeColor = Color.White : .Text = r_dt.Rows(i - 1).Item("deptcd").ToString.Trim
                    .Col = .GetColFromID("deptnm") : If .Col > -1 Then .ForeColor = Color.White : .Text = r_dt.Rows(i - 1).Item("deptnm").ToString.Trim
                    .Col = .GetColFromID("doctorcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("doctorcd").ToString().Trim
                    .Col = .GetColFromID("doctornm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("doctornm").ToString().Trim
                    .Col = .GetColFromID("testcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("testcd").ToString().Trim
                    .Col = .GetColFromID("entdt") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("entdt").ToString().Trim
                    .Col = .GetColFromID("sunab_date") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sunab_date").ToString().Trim
                    .Col = .GetColFromID("cprtgbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("cprtgbn").ToString.Trim
                    .Col = .GetColFromID("tnmd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tnmd").ToString.Trim

                    Select Case r_dt.Rows(i - 1).Item("bccolor").ToString.Trim
                        Case "1"
                            .BackColor = Me.lblBcColor1.BackColor
                            .ForeColor = Me.lblBcColor1.ForeColor
                        Case "2"
                            .BackColor = Me.lblBcColor2.BackColor
                            .ForeColor = Me.lblBcColor2.ForeColor
                        Case "3"
                            .BackColor = Me.lblBcColor3.BackColor
                            .ForeColor = Me.lblBcColor3.ForeColor
                        Case Else
                            .BackColor = Me.lblBcColor0.BackColor
                            .ForeColor = Me.lblBcColor0.ForeColor
                    End Select

                    .Col = .GetColFromID("spccd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("spccd").ToString().Trim
                    .Col = .GetColFromID("bcclscd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bcclscd").ToString().Trim
                    .Col = .GetColFromID("remark")
                    If .Col > -1 Then
                        If r_dt.Rows(i - 1).Item("remark").ToString().Trim() <> "" Then
                            .Text = r_dt.Rows(i - 1).Item("remark").ToString().Trim().Replace(vbCrLf, "")
                        End If
                    End If

                    .Col = .GetColFromID("remark_nrs")
                    If .Col > -1 Then
                        If r_dt.Rows(i - 1).Item("remark_nrs").ToString().Trim() <> "" Then
                            .Text = r_dt.Rows(i - 1).Item("remark_nrs").ToString().Trim()
                        End If
                    End If

                    .Col = .GetColFromID("minspcvol") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("minspcvol").ToString().Trim

                    .Col = .GetColFromID("erflg")
                    If r_dt.Rows(i - 1).Item("erflg").ToString().Trim = PRG_CONST.Flg_ER Then
                        .Text = Me.lblErFlgE.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblErFlgE.BackColor
                        .ForeColor = Me.lblErFlgE.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("erflg").ToString().Trim = PRG_CONST.Flg_BF Then
                        .Text = Me.lblErFlgB.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblErFlgB.BackColor
                        .ForeColor = Me.lblErFlgB.ForeColor
                    Else
                        .Text = ""
                    End If

                    .Col = .GetColFromID("exlabcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("exlabcd").ToString().Trim
                    .Col = .GetColFromID("bconeyn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bconeyn").ToString().Trim
                    .Col = .GetColFromID("seqtyn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("seqtyn").ToString().Trim
                    .Col = .GetColFromID("seqtmi") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("seqtmi").ToString().Trim
                    .Col = .GetColFromID("poctyn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("poctyn").ToString.Trim
                    .Col = .GetColFromID("iogbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("iogbn").ToString().Trim
                    .Col = .GetColFromID("fkocs") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("fkocs").ToString().Trim
                    .Col = .GetColFromID("cwarning")
                    If .Col > -1 Then
                        .Text = r_dt.Rows(i - 1).Item("cwarning").ToString().Trim
                        .ForeColor = Color.Red
                    End If

                    .Col = .GetColFromID("height") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("height").ToString().Trim
                    .Col = .GetColFromID("weight") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("weight").ToString().Trim
                    .Col = .GetColFromID("tubecd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tubecd").ToString().Trim

                    Dim sTubeColor As String = Collfn.FindOrder_TUBECOLOR(r_dt.Rows(i - 1).Item("tubecd").ToString().Trim)

                    .Col = .GetColFromID("tubecolor")
                    If sTubeColor = "빨강색" Then
                        .BackColor = Color.Red
                    ElseIf sTubeColor = "주황색" Then
                        .BackColor = Color.Orange
                    ElseIf sTubeColor = "노랑색" Then
                        .BackColor = Color.Yellow
                    ElseIf sTubeColor = "초록색" Then
                        .BackColor = Color.Green
                    ElseIf sTubeColor = "하늘색" Then
                        .BackColor = Color.SkyBlue
                    ElseIf sTubeColor = "파랑색" Then
                        .BackColor = Color.Blue
                    ElseIf sTubeColor = "남색" Then
                        .BackColor = Color.DarkBlue
                    ElseIf sTubeColor = "보라색" Then
                        .BackColor = Color.Purple
                    ElseIf sTubeColor = "흰색" Then
                        .BackColor = Color.WhiteSmoke
                    ElseIf sTubeColor = "검은색" Then
                        .BackColor = Color.Black
                    End If

                    .Col = .GetColFromID("owngbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("owngbn").ToString().Trim

                    .Col = .GetColFromID("liscmt")
                    .TypeComboBoxList = msLisCmts
                    .Text = r_dt.Rows(i - 1).Item("liscmt").ToString().Trim

                    .Col = .GetColFromID("ordcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("ordcd").ToString().Trim

                    .Col = .GetColFromID("append_yn")
                    If r_dt.Rows(i - 1).Item("append_yn").ToString().Trim = PRG_CONST.Flg_Regular Then
                        .Text = ""
                    ElseIf r_dt.Rows(i - 1).Item("append_yn").ToString().Trim = PRG_CONST.Flg_Add Then
                        .Text = r_dt.Rows(i - 1).Item("append_yn").ToString().Trim
                    Else
                        .Text = ""
                    End If

                    .Col = .GetColFromID("bccnt") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bccnt").ToString().Trim
                    .Col = .GetColFromID("spcflg")
                    If r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Ord Then
                        .Text = ""

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_BcPrt Then
                        .Text = Me.lblOrdFlgB.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgB.BackColor
                        .ForeColor = Me.lblOrdFlgB.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Coll Then
                        .Text = Me.lblOrdFlgC.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgC.BackColor
                        .ForeColor = Me.lblOrdFlgC.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Pass Then
                        .Text = Me.lblOrdFlgP.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgP.BackColor
                        .ForeColor = Me.lblOrdFlgP.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Tk Then
                        .Text = Me.lblOrdFlgT.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgT.BackColor
                        .ForeColor = Me.lblOrdFlgT.ForeColor

                    End If

                    .Col = .GetColFromID("rstflg")
                    If r_dt.Rows(i - 1).Item("rstflg").ToString().Trim = PRG_CONST.Flg_NoRst Then
                        .Text = ""
                    ElseIf r_dt.Rows(i - 1).Item("rstflg").ToString().Trim = PRG_CONST.Flg_Rst Then
                        .Text = Me.lblRstFlgR_img.Text.Trim
                        .BackColor = Me.lblRstFlgR_img.BackColor
                        .ForeColor = Me.lblRstFlgR_img.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("rstflg").ToString().Trim = PRG_CONST.Flg_Mw Then
                        .Text = Me.lblRstFlgM_img.Text.Trim
                        .BackColor = Me.lblRstFlgM_img.BackColor
                        .ForeColor = Me.lblRstFlgM_img.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("rstflg").ToString().Trim = PRG_CONST.Flg_Fn Then
                        .Text = Me.lblRstFlgF_img.Text.Trim
                        .BackColor = Me.lblRstFlgF_img.BackColor
                        .ForeColor = Me.lblRstFlgF_img.ForeColor
                    End If

                    .Col = .GetColFromID("spcnmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("spcnmbp").ToString().Trim
                    .Col = .GetColFromID("tcdgbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tcdgbn").ToString().Trim
                    .Col = .GetColFromID("tnmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tnmbp").ToString().Trim
                    .Col = .GetColFromID("tubenmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tubenmbp").ToString().Trim
                    .Col = .GetColFromID("dc_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("dc_yn").ToString().Trim

                    Dim sSpcInfo() As String = m_dt_ord.Rows(i - 1).Item("spcinfo").ToString.Split("|"c)

                    If sSpcInfo.Length > 1 Then
                        .Col = .GetColFromID("bcno") : If .Col > -1 Then .Text = sSpcInfo(0)
                        .Col = .GetColFromID("prtbcno") : If .Col > -1 Then .Text = sSpcInfo(1)
                    End If

                    .Col = .GetColFromID("sortkey") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sortslip").ToString().Trim + "/" + r_dt.Rows(i - 1).Item("sortl").ToString().Trim
                    .Col = .GetColFromID("wardno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("wardno").ToString().Trim
                    .Col = .GetColFromID("wardnm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("wardnm").ToString().Trim
                    .Col = .GetColFromID("partgbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("partgbn").ToString().Trim
                    .Col = .GetColFromID("ordslip") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("ordslip").ToString().Trim
                    .Col = .GetColFromID("rsvdate") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("rsvdate").ToString().Trim
                    .Col = .GetColFromID("wardabbr") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("wardabbr").ToString().Trim
                    .Col = .GetColFromID("deptabbr") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("deptabbr").ToString().Trim

                    .Col = .GetColFromID("tgrpnm")
                    If sBuf.ToUpper().Trim().IndexOf(r_dt.Rows(i - 1).Item("tgrpnm").ToString.Trim.ToUpper().Trim()) = -1 Then
                        sBuf += r_dt.Rows(i - 1).Item("tgrpnm").ToString.Trim
                    End If
                    .Text = sBuf
                    sBuf = ""

                    .Col = .GetColFromID("dtestcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("dtestcd").ToString().Trim
                    .Col = .GetColFromID("sunab_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sunab_yn").ToString().Trim

                    '오픈카드관련 
                    If .Text = "N" Then

                        '오픈카드관련 추가 20150909
                        .Col = .GetColFromID("opencard") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("opencard").ToString().Trim
                        If .Text = "Y" Then '임시로 오픈
                            .Col = .GetColFromID("sunab_yn") : .Col2 = .GetColFromID("sunab_yn")
                            .Row = i : .Row2 = i
                            .BlockMode = True
                            .FontBold = True
                            .BackColor = Color.LightGreen
                            .ForeColor = Color.Crimson
                            .BlockMode = False
                        Else
                            .Col = .GetColFromID("sunab_yn") : .Col2 = .GetColFromID("sunab_yn")
                            .Row = i : .Row2 = i
                            .BlockMode = True
                            .FontBold = True
                            .BackColor = Color.Gainsboro
                            .ForeColor = Color.Crimson
                            .BlockMode = False
                        End If

                        '.Col = .GetColFromID("sunab_yn") : .Col2 = .GetColFromID("sunab_yn")
                        '.Row = i : .Row2 = i
                        '.BlockMode = True
                        '.FontBold = True
                        '.BackColor = Color.Gainsboro
                        '.ForeColor = Color.Crimson
                        '.BlockMode = False
                    Else
                        .Col = .GetColFromID("opencard") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("opencard").ToString().Trim
                    End If


                    .Col = .GetColFromID("hold_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("hold_yn").ToString().Trim

                    If .Text = "Y" Then
                        .Col = .GetColFromID("hold_yn") : .Col2 = .GetColFromID("hold_yn")
                        .Row = i : .Row2 = i
                        .BlockMode = True
                        .FontBold = True
                        .BackColor = Color.Gainsboro
                        .ForeColor = Color.DarkMagenta
                        .BlockMode = False
                    End If

                    If mbCollBatch Then
                        If sRegNoC <> sRegNoP Then
                            'Line 그리기
                            If i > 1 Then Fn.DrawBorderLineTop(spd, i)
                        End If
                    End If

                    '''< yjlee 
                    If sTclsCdP <> "" Then
                        If sBcKeyC = sBcKeyP Then
                            If r_dt.Rows(i - 1).Item("bcclscd").ToString = PRG_CONST.BCCLS_BldCrossMatch Then
                                bGrpCheck = True
                            ElseIf CheckDuplicated_Order(sTclsCdC.Split(",".ToCharArray()(0)), sTclsCdP.Split(",".ToCharArray()(0))) Then
                                bGrpCheck = False
                                sTclsCdC = ""
                            Else
                                bGrpCheck = True
                            End If
                        Else
                            bGrpCheck = False
                        End If
                    End If

                    If sBcKeyC = sBcKeyP And r_dt.Rows(i - 1).Item("bconeyn").ToString().Trim = "0" Then
                        .SetRowItemData(i, iGrpNo)

                        .SetCellBorder(-1, i, -1, i, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexTop, _
                                   Convert.ToUInt32(Microsoft.VisualBasic.RGB(192, 192, 192)), _
                                   FPSpreadADO.CellBorderStyleConstants.CellBorderStyleDot)

                    Else
                        sTclsCdP = ""
                        bGrpCheck = True

                        iGrpNo += 1

                        .SetRowItemData(i, iGrpNo)

                        'Line 그리기
                        If i > 1 Then Fn.DrawBorderLineTop(spd, i)

                        .Row = i

                        'grpno
                        .SetText(.GetColFromID("grpno"), i, iGrpNo.ToString)

                        'spcnmd
                        .SetText(.GetColFromID("spcnmd"), i, r_dt.Rows(i - 1).Item("spcnmd").ToString.Trim)

                        'tubenmd
                        .SetText(.GetColFromID("tubenmd"), i, r_dt.Rows(i - 1).Item("tubenmd").ToString.Trim)

                        'dptcd  
                        .Col = .GetColFromID("deptcd") : .Row = i : .ForeColor = Color.Black

                        'dptnm  
                        .Col = .GetColFromID("deptnm") : .Row = i : .ForeColor = Color.Black

                        'regno
                        .Col = .GetColFromID("regno") : .Row = i : .ForeColor = Color.Black

                        'patinfo
                        .Col = .GetColFromID("patnm") : .Row = i : .ForeColor = Color.Black

                        'roomno
                        .Col = .GetColFromID("roomno") : .Row = i : .ForeColor = Color.Black

                        'orddt  
                        .Col = .GetColFromID("orddt") : .Row = i : .ForeColor = Color.Black

                        ''hopeday  
                        '.Col = .GetColFromID("hopeday") : .Row = i : .ForeColor = Color.Black

                        'docname  
                        .Col = .GetColFromID("doctornm") : .Row = i : .ForeColor = Color.Black

                        'deptnm  
                        .Col = .GetColFromID("deptnm") : .Row = i : .ForeColor = Color.Black
                        '> 
                    End If

                    'chk 
                    If r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Ord Then
                        If r_dt.Rows(i - 1).Item("testcd").ToString().Trim = "" Then
                            .Col = .GetColFromID("chk") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                        Else

                            '< yjlee 2009-01-29
                            If Not bGrpCheck Then

                            Else
                                '< yjlee 2009-04-09 보류,수납안된것들 디폴트 체크 해지 
                                Dim sSuNabGbn As String = "", sHoldGbn As String = ""
                                .Row = i
                                .Col = .GetColFromID("sunab_yn") : sSuNabGbn = .Text.Trim()
                                .Col = .GetColFromID("hold_yn") : sHoldGbn = .Text.Trim()

                                If sSuNabGbn = "Y" And sHoldGbn = "N" Then
                                    .SetText(.GetColFromID("chk"), i, "1")
                                End If
                                '> yjlee 2009-04-09 

                                '-- X-Matching 검사인 경우 혈액형 오더가 없으면 채크 안함.
                                If r_dt.Rows(i - 1).Item("bccnt").ToString = "B" Then
                                    .Row = i
                                    .Col = .GetColFromID("chk") : .Text = fnGet_Order_AboRhYn(r_dt.Rows(i - 1).Item("regno").ToString().Trim, r_dt.Rows(i - 1).Item("orddt").ToString().Trim)

                                    If .Text = "" Then RaiseEvent MsgPopup("혈액형 검사 오더가 없습니다.!!")
                                End If
                            End If

                        End If
                    Else
                        .Col = .GetColFromID("chk") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                    End If

                    'bckey
                    .SetText(.GetColFromID("bckey"), i, sBcKeyC)

                    'bckey2
                    .SetText(.GetColFromID("bckey2"), i, sBcKey2)

                    'bckey3
                    .SetText(.GetColFromID("bckey3"), i, sBcKey3)

                    '< yjlee 
                    '부천순천향 Battery에 포함된 중복 검사항목에 대하여 바코드 나누기 위해서 
                    .SetText(.GetColFromID("dtestcd"), i, r_dt.Rows(i - 1).Item("dtestcd").ToString.Trim)
                    .SetText(.GetColFromID("bckeytemp"), i, sBcKeyC + r_dt.Rows(i - 1).Item("dtestcd").ToString.Trim)

                    Dim bDuplicated As Boolean = False

                    If r_dt.Rows(i - 1).Item("bconeyn").ToString().Trim = "0" And r_dt.Rows(i - 1).Item("spcflg").ToString = PRG_CONST.Flg_Ord Then
                        bDuplicated = fnFind_Duplicated_Order(i, sBcKeyC, r_dt.Rows(i - 1).Item("testcd").ToString.Trim)
                    End If

                    If bDuplicated Then
                        .SetText(.GetColFromID("chk"), i, "")
                        '< yjlee 2009-02-11 
                        .Col = -1 : .Col2 = -1
                        .Row = i : .Row2 = i
                        .BlockMode = True
                        .Font = New Drawing.Font("굴림체", 10, FontStyle.Italic)
                        .BlockMode = False
                        '> yjlee 2009-02-11
                    End If

                    '-- 검사희망일자와 처방일자가 틀린경우
                    If r_dt.Rows(i - 1).Item("hopeday").ToString.Trim <> r_dt.Rows(i - 1).Item("ordday").ToString.Trim And r_dt.Rows(i - 1).Item("hopeday").ToString.Trim > Format(Now, "yyyy-MM-dd") Then
                        .SetText(.GetColFromID("chk"), i, "")
                        .Col = -1 : .Col2 = -1
                        .Row = i : .Row2 = i
                        .BlockMode = True
                        .Font = New Drawing.Font("굴림체", 9, FontStyle.Italic)
                        .BlockMode = False
                    End If

                    sBcKeyP = sBcKeyC
                    sRegNoP = sRegNoC

                    '< yjlee 2009-01-29 
                    If Not sTclsCdC = "" Then
                        sTclsCdP += sTclsCdC + ","
                    End If
                    '> yjlee 2009-01-29


                Next

                '< yjlee 부천순천향  
                .Col = .GetColFromID("bckeytemp") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                'chkbc
                Dim iRowE As Integer = 0

                For g As Integer = 1 To iGrpNo
                    Dim iChkRow As Integer = 0
                    Dim iRowB As Integer = 0

                    iRowB = iRowE + 1

                    For i As Integer = iRowB To .MaxRows
                        Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)

                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            If sChk = "1" Then
                                iChkRow = i
                            End If
                        Else
                            Exit For
                        End If
                    Next

                    For i As Integer = iRowB To iRowE
                        If i = iRowB Then
                            If iChkRow = 0 Then
                                Dim sSuNabGbn As String = "", sHoldGbn As String = ""
                                .Row = i
                                .Col = .GetColFromID("sunab_yn") : sSuNabGbn = .Text.Trim()
                                .Col = .GetColFromID("hold_yn") : sHoldGbn = .Text.Trim()

                                If sSuNabGbn = "Y" And sHoldGbn = "N" Then
                                    '.Col = .GetColFromID("chkbc") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                                    .Col = .GetColFromID("chkbc") : .Row = i : .Text = ""
                                End If
                            Else
                                .SetText(.GetColFromID("chkbc"), i, "1")
                            End If
                        Else
                            .Col = .GetColFromID("chkbc") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""

                        End If
                    Next
                Next

                If mbAllCheck = False Then
                    '< 처음표시 날짜와 비교 해서 처음표시 날짜 보다 검사희망일이 작으면 체크 해지
                    For ix As Integer = 1 To .MaxRows
                        .Row = ix
                        .Col = .GetColFromID("hopeday") : Dim sHopeDay As String = .Text
                        .Col = .GetColFromID("orddt") : Dim sOrdDay As String = .Text.Substring(0, 10)
                        .Col = .GetColFromID("chk") : Dim sChk As String = .Text

                        'If sChk = "" Then sHopeDay = sOrdDay

                        If DateAdd(DateInterval.Day, miAutoCheckDay, CDate(sHopeDay)) < CDate(sFirstHopeDay) Then
                            .SetText(.GetColFromID("chkbc"), ix, "")
                            .SetText(.GetColFromID("chk"), ix, "")
                        End If
                    Next
                End If

                If mbCollBatch And mbSearchMode = False Then
                    MergeOrder(False)
                    MergeBatch()
                End If

                .ReDraw = True
            End With

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

        Finally
            mbSkip = False
            Me.spdOrdList.ReDraw = True

        End Try
    End Sub

    Protected Sub sbDisplayOrder_Detail_bcsum(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub sbDisplayOrder_Detail_bcsum(DataTable)"

        Try
            mbSkip = True

            Dim sRoomC As String = ""
            Dim sPatNameC As String = ""
            Dim sRoomP As String = ""
            Dim sPatNameP As String = ""

            Dim sBcKeyC As String = ""
            Dim sBcKeyP As String = ""
            Dim sRegNoC As String = ""
            Dim sRegNoP As String = ""

            '< yjlee  
            Dim sTclsCdC As String = ""
            Dim sTclsCdP As String = ""

            Dim sBuf As String = ""
            '> 

            Dim bGrpCheck As Boolean = False

            Dim iGrpNo As Integer = 0

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

            With spd
                .ReDraw = False
                Clear()

                .MaxRows = r_dt.Rows.Count


                Dim sFirstHopeDay As String = ""
                sFirstHopeDay = r_dt.Rows(0).Item("hopeday").ToString

                For i As Integer = 1 To r_dt.Rows.Count
                    'BcKey : hopeday, exlabcd, bcclscd, spccd, tubecd, seqtmi, ordday, [regno]

                    Dim sOrdDt As String = r_dt.Rows(i - 1).Item("orddt").ToString.Substring(0, 10)

                    sBcKeyC = ""
                    sBcKeyC += r_dt.Rows(i - 1).Item("exlabcd").ToString + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("bcclscd").ToString + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("spccd").ToString + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("tubecd").ToString + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("bconeyn").ToString + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("seqtmi").ToString + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("iogbn").ToString

                    If mbCollBatch Then
                        sBcKeyC += "/" + r_dt.Rows(i - 1).Item("regno").ToString

                        sRegNoC = r_dt.Rows(i - 1).Item("regno").ToString
                    End If

                    'BcKey2 : hopeday, exlabcd, sectcd, tsectcd, spccd, tubecd, seqtmi, [regno] -> 처방일시가 다르지만 합쳐질 수 있는 경우
                    Dim sBcKey2 As String = ""

                    sBcKey2 = ""
                    sBcKey2 += r_dt.Rows(i - 1).Item("exlabcd").ToString + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("bcclscd").ToString + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("spccd").ToString + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("tubecd").ToString + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("bconeyn").ToString + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("seqtmi").ToString + "/"
                    sBcKey2 += r_dt.Rows(i - 1).Item("iogbn").ToString

                    If mbCollBatch Then
                        sBcKey2 += "/" + r_dt.Rows(i - 1).Item("regno").ToString
                    End If

                    'BcKey3 : hopeday, exlabcd, sectcd, tsectcd, spccd, tubecd -> 동일조건의 연속검사 샘플 판별용
                    Dim sBcKey3 As String = ""

                    sBcKey3 = ""
                    sBcKey3 += r_dt.Rows(i - 1).Item("exlabcd").ToString + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("bcclscd").ToString + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("spccd").ToString + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("tubecd").ToString + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("bconeyn").ToString + "/"
                    sBcKey3 += r_dt.Rows(i - 1).Item("iogbn").ToString

                    '< yjlee 
                    sTclsCdC = r_dt.Rows(i - 1).Item("dtestcd").ToString

                    If mbCollBatch Then
                        sBcKey3 += "/" + r_dt.Rows(i - 1).Item("regno").ToString
                    End If

                    .Row = i
                    .Col = .GetColFromID("grpno") : .Text = ""

                    .Col = .GetColFromID("regno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("regno").ToString() : .ForeColor = Color.White ': sRegNoC = .Text
                    .Col = .GetColFromID("patnm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("patinfo").ToString().Split("|"c)(0) : .ForeColor = Color.White
                    .Col = .GetColFromID("roomno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("roomno").ToString() : .ForeColor = Color.White ': sRoomC = .Text

                    .Col = .GetColFromID("orddt") : If .Col > -1 Then .ForeColor = Color.White : .Text = r_dt.Rows(i - 1).Item("orddt").ToString
                    .Col = .GetColFromID("deptcd") : If .Col > -1 Then .ForeColor = Color.White : .Text = r_dt.Rows(i - 1).Item("deptcd").ToString
                    .Col = .GetColFromID("deptnm") : If .Col > -1 Then .ForeColor = Color.White : .Text = r_dt.Rows(i - 1).Item("deptnm").ToString
                    .Col = .GetColFromID("doctorcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("doctorcd").ToString()
                    .Col = .GetColFromID("testcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("testcd").ToString()
                    .Col = .GetColFromID("entdt") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("entdt").ToString()
                    .Col = .GetColFromID("sunab_date") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sunab_date").ToString()
                    .Col = .GetColFromID("tnmd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tnmd").ToString

                    Select Case r_dt.Rows(i - 1).Item("bccolor").ToString
                        Case "1"
                            .BackColor = Me.lblBcColor1.BackColor
                            .ForeColor = Me.lblBcColor1.ForeColor
                        Case "2"
                            .BackColor = Me.lblBcColor2.BackColor
                            .ForeColor = Me.lblBcColor2.ForeColor
                        Case "3"
                            .BackColor = Me.lblBcColor3.BackColor
                            .ForeColor = Me.lblBcColor3.ForeColor
                        Case Else
                            .BackColor = Me.lblBcColor0.BackColor
                            .ForeColor = Me.lblBcColor0.ForeColor
                    End Select

                    .Col = .GetColFromID("spccd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("spccd").ToString()
                    .Col = .GetColFromID("bcclscd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bcclscd").ToString()
                    .Col = .GetColFromID("remark")
                    If .Col > -1 Then
                        If r_dt.Rows(i - 1).Item("remark").ToString().Trim() <> "" Then
                            .Text = r_dt.Rows(i - 1).Item("remark").ToString().Trim().Replace(vbCrLf, "")
                        End If
                    End If

                    .Col = .GetColFromID("remark_nrs")
                    If .Col > -1 Then
                        If r_dt.Rows(i - 1).Item("remark_nrs").ToString().Trim() <> "" Then
                            .Text = r_dt.Rows(i - 1).Item("remark_nrs").ToString().Trim()
                        End If
                    End If

                    .Col = .GetColFromID("minspcvol") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("minspcvol").ToString()

                    .Col = .GetColFromID("erflg")
                    If r_dt.Rows(i - 1).Item("erflg").ToString() = PRG_CONST.Flg_ER Then
                        .Text = Me.lblErFlgE.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblErFlgE.BackColor
                        .ForeColor = Me.lblErFlgE.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("erflg").ToString() = PRG_CONST.Flg_BF Then
                        .Text = Me.lblErFlgB.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblErFlgB.BackColor
                        .ForeColor = Me.lblErFlgB.ForeColor
                    Else
                        .Text = ""
                    End If

                    .Col = .GetColFromID("bconeyn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bconeyn").ToString()
                    .Col = .GetColFromID("exlabcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("exlabcd").ToString()
                    .Col = .GetColFromID("seqtyn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("seqtyn").ToString()
                    .Col = .GetColFromID("seqtmi") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("seqtmi").ToString()
                    .Col = .GetColFromID("iogbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("iogbn").ToString()
                    .Col = .GetColFromID("fkocs") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("fkocs").ToString()
                    .Col = .GetColFromID("cwarning")
                    If .Col > -1 Then
                        .Text = r_dt.Rows(i - 1).Item("cwarning").ToString()
                        .ForeColor = Color.Red
                    End If

                    .Col = .GetColFromID("height") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("height").ToString()
                    .Col = .GetColFromID("weight") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("weight").ToString()
                    .Col = .GetColFromID("tubecd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tubecd").ToString()
                    .Col = .GetColFromID("owngbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("owngbn").ToString()

                    .Col = .GetColFromID("liscmt")
                    .TypeComboBoxList = msLisCmts
                    .Text = r_dt.Rows(i - 1).Item("liscmt").ToString()

                    .Col = .GetColFromID("ordcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("ordcd").ToString()

                    .Col = .GetColFromID("append_yn")
                    If r_dt.Rows(i - 1).Item("append_yn").ToString() = PRG_CONST.Flg_Regular Then
                        .Text = ""
                    ElseIf r_dt.Rows(i - 1).Item("append_yn").ToString() = PRG_CONST.Flg_Add Then
                        .Text = r_dt.Rows(i - 1).Item("append_yn").ToString()
                    Else
                        .Text = ""
                    End If

                    .Col = .GetColFromID("bccnt") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bccnt").ToString()
                    .Col = .GetColFromID("spcflg")
                    If r_dt.Rows(i - 1).Item("spcflg").ToString() = PRG_CONST.Flg_Ord Then
                        .Text = ""

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString() = PRG_CONST.Flg_BcPrt Then
                        .Text = Me.lblOrdFlgB.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgB.BackColor
                        .ForeColor = Me.lblOrdFlgB.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString() = PRG_CONST.Flg_Coll Then
                        .Text = Me.lblOrdFlgC.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgC.BackColor
                        .ForeColor = Me.lblOrdFlgC.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Pass Then
                        .Text = Me.lblOrdFlgP.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgP.BackColor
                        .ForeColor = Me.lblOrdFlgP.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString() = PRG_CONST.Flg_Tk Then
                        .Text = Me.lblOrdFlgT.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgT.BackColor
                        .ForeColor = Me.lblOrdFlgT.ForeColor

                    End If

                    .Col = .GetColFromID("rstflg")
                    If r_dt.Rows(i - 1).Item("rstflg").ToString() = PRG_CONST.Flg_NoRst Or r_dt.Rows(i - 1).Item("rstflg").ToString() = PRG_CONST.Flg_Rst Then
                        .Text = ""

                    ElseIf r_dt.Rows(i - 1).Item("rstflg").ToString() = PRG_CONST.Flg_Mw Then
                        .Text = Me.lblRstFlgM_img.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblRstFlgM_img.BackColor
                        .ForeColor = Me.lblRstFlgM_img.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("rstflg").ToString() = PRG_CONST.Flg_Fn Then
                        .Text = Me.lblRstFlgF_img.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblRstFlgF_img.BackColor
                        .ForeColor = Me.lblRstFlgF_img.ForeColor
                    End If

                    .Col = .GetColFromID("spcnmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("spcnmbp").ToString()
                    .Col = .GetColFromID("tcdgbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tcdgbn").ToString()
                    .Col = .GetColFromID("tnmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tnmbp").ToString()
                    .Col = .GetColFromID("tubenmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tubenmbp").ToString()
                    .Col = .GetColFromID("dc_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("dc_yn").ToString()
                    .Col = .GetColFromID("bcno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bcno").ToString()
                    .Col = .GetColFromID("sortkey") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sortslip").ToString() + "/" + r_dt.Rows(i - 1).Item("sortl").ToString()
                    .Col = .GetColFromID("wardno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("wardno").ToString()
                    .Col = .GetColFromID("wardnm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("wardnm").ToString()

                    .Col = .GetColFromID("tgrpnm")
                    If sBuf.ToUpper().Trim().IndexOf(r_dt.Rows(i - 1).Item("tgrpnm").ToString.ToUpper().Trim()) = -1 Then
                        sBuf += r_dt.Rows(i - 1).Item("tgrpnm").ToString
                    End If
                    .Text = sBuf
                    sBuf = ""

                    .Col = .GetColFromID("dtestcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("dtestcd").ToString()
                    .Col = .GetColFromID("sunab_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sunab_yn").ToString()

                    If .Text = "N" Then
                        .Col = .GetColFromID("sunab_yn") : .Col2 = .GetColFromID("sunab_yn")
                        .Row = i : .Row2 = i
                        .BlockMode = True
                        .FontBold = True
                        .BackColor = Color.Gainsboro
                        .ForeColor = Color.Crimson
                        .BlockMode = False
                    End If

                    .Col = .GetColFromID("hold_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("hold_yn").ToString()

                    If .Text = "Y" Then
                        .Col = .GetColFromID("hold_yn") : .Col2 = .GetColFromID("hold_yn")
                        .Row = i : .Row2 = i
                        .BlockMode = True
                        .FontBold = True
                        .BackColor = Color.Gainsboro
                        .ForeColor = Color.DarkMagenta
                        .BlockMode = False
                    End If

                    If mbCollBatch Then
                        If sRegNoC <> sRegNoP Then
                            'Line 그리기
                            If i > 1 Then Fn.DrawBorderLineTop(spd, i)
                        End If
                    End If

                    '''< yjlee 
                    If sTclsCdP <> "" Then
                        If sBcKeyC = sBcKeyP Then
                            If r_dt.Rows(i - 1).Item("bcclscd").ToString = PRG_CONST.BCCLS_BldCrossMatch Then
                                bGrpCheck = True
                            ElseIf CheckDuplicated_Order(sTclsCdC.Split(",".ToCharArray()(0)), sTclsCdP.Split(",".ToCharArray()(0))) Then
                                bGrpCheck = False
                                sTclsCdC = ""
                            Else
                                bGrpCheck = True
                            End If
                        Else
                            bGrpCheck = False
                        End If
                    End If


                    If sBcKeyC = sBcKeyP Then
                        .SetRowItemData(i, iGrpNo)

                        .SetCellBorder(-1, i, -1, i, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexTop, _
                                   Convert.ToUInt32(Microsoft.VisualBasic.RGB(192, 192, 192)), _
                                   FPSpreadADO.CellBorderStyleConstants.CellBorderStyleDot)

                    Else
                        sTclsCdP = ""
                        bGrpCheck = True

                        iGrpNo += 1

                        .SetRowItemData(i, iGrpNo)

                        'Line 그리기
                        If i > 1 Then Fn.DrawBorderLineTop(spd, i)

                        .Row = i

                        'grpno
                        .SetText(.GetColFromID("grpno"), i, iGrpNo.ToString)

                        'spcnmd
                        .SetText(.GetColFromID("spcnmd"), i, r_dt.Rows(i - 1).Item("spcnmd").ToString)

                        'tubenmd
                        .SetText(.GetColFromID("tubenmd"), i, r_dt.Rows(i - 1).Item("tubenmd").ToString)

                        'dptcd  
                        .Col = .GetColFromID("deptcd") : .Row = i : .ForeColor = Color.Black

                        'dptnm  
                        .Col = .GetColFromID("deptnm") : .Row = i : .ForeColor = Color.Black

                        'regno
                        .Col = .GetColFromID("regno") : .Row = i : .ForeColor = Color.Black

                        'patinfo
                        .Col = .GetColFromID("patnm") : .Row = i : .ForeColor = Color.Black

                        'roomno
                        .Col = .GetColFromID("roomno") : .Row = i : .ForeColor = Color.Black

                        'orddt  
                        .Col = .GetColFromID("orddt") : .Row = i : .ForeColor = Color.Black

                        'hopeday  
                        .SetText(.GetColFromID("hopeday"), i, r_dt.Rows(i - 1).Item("hopeday").ToString)

                        'docname  
                        .SetText(.GetColFromID("doctornm"), i, r_dt.Rows(i - 1).Item("doctornm").ToString)
                        '> 
                    End If

                    'chk 
                    If r_dt.Rows(i - 1).Item("spcflg").ToString() = PRG_CONST.Flg_Ord Then
                        If r_dt.Rows(i - 1).Item("testcd").ToString() = "" Then
                            .Col = .GetColFromID("chk") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                        Else

                            '< yjlee 2009-01-29
                            If Not bGrpCheck Then

                            Else
                                '< yjlee 2009-04-09 보류,수납안된것들 디폴트 체크 해지 
                                Dim sSuNabGbn As String = "", sHoldGbn As String = ""
                                .Row = i
                                .Col = .GetColFromID("sunab_yn")
                                sSuNabGbn = .Text.Trim()
                                .Col = .GetColFromID("hold_yn")
                                sHoldGbn = .Text.Trim()

                                If sSuNabGbn = "Y" And sHoldGbn = "N" Then
                                    .SetText(.GetColFromID("chk"), i, "1")
                                End If
                                '> yjlee 2009-04-09 
                            End If

                        End If
                    Else
                        .Col = .GetColFromID("chk") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                    End If

                    'bckey
                    .SetText(.GetColFromID("bckey"), i, sBcKeyC)

                    'bckey2
                    .SetText(.GetColFromID("bckey2"), i, sBcKey2)

                    'bckey3
                    .SetText(.GetColFromID("bckey3"), i, sBcKey3)

                    '< yjlee 
                    '부천순천향 Battery에 포함된 중복 검사항목에 대하여 바코드 나누기 위해서 
                    .SetText(.GetColFromID("dtestcd"), i, r_dt.Rows(i - 1).Item("dtestcd").ToString)
                    .SetText(.GetColFromID("bckeytemp"), i, sBcKeyC + r_dt.Rows(i - 1).Item("dtestcd").ToString)

                    Dim bDuplicated As Boolean = False

                    If r_dt.Rows(i - 1).Item("spcflg").ToString = PRG_CONST.Flg_Ord Then
                        bDuplicated = fnFind_Duplicated_Order(i, sBcKeyC, r_dt.Rows(i - 1).Item("testcd").ToString)
                    End If

                    If bDuplicated Then
                        .SetText(.GetColFromID("chk"), i, "")
                        '< yjlee 2009-02-11 
                        .Col = -1 : .Col2 = -1
                        .Row = i : .Row2 = i
                        .BlockMode = True
                        .Font = New Drawing.Font("굴림체", 10, FontStyle.Italic)
                        .BlockMode = False
                        '> yjlee 2009-02-11
                    End If

                    sBcKeyP = sBcKeyC
                    sRegNoP = sRegNoC

                    '< yjlee 2009-01-29 
                    If Not sTclsCdC = "" Then
                        sTclsCdP += sTclsCdC + ","
                    End If
                    '> yjlee 2009-01-29
                Next

                '< yjlee 부천순천향  
                .Col = .GetColFromID("bckeytemp") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                'chkbc
                Dim iRowE As Integer = 0

                For g As Integer = 1 To iGrpNo
                    Dim iChkRow As Integer = 0
                    Dim iRowB As Integer = 0

                    iRowB = iRowE + 1

                    For i As Integer = iRowB To .MaxRows
                        Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)

                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            If sChk = "1" Then
                                iChkRow = i
                            End If
                        Else
                            Exit For
                        End If
                    Next

                    For i As Integer = iRowB To iRowE
                        If i = iRowB Then
                            If iChkRow = 0 Then
                                Dim sSuNabGbn As String = "", sHoldGbn As String = ""
                                .Row = i
                                .Col = .GetColFromID("sunab_yn")
                                sSuNabGbn = .Text.Trim()
                                .Col = .GetColFromID("hold_yn")
                                sHoldGbn = .Text.Trim()

                                If sSuNabGbn = "Y" And sHoldGbn = "N" Then
                                    .Col = .GetColFromID("chkbc") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                                End If
                            Else
                                .SetText(.GetColFromID("chkbc"), i, "1")
                            End If
                        Else
                            .Col = .GetColFromID("chkbc") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                        End If
                    Next
                Next

                If mbAllCheck Then
                    '< 현재 날짜와 비교 해서 현재 날짜 보다 검사희망일이 작으면 체크 해지 -부천순천향 요구사항  
                    For intCnt As Integer = 1 To .MaxRows
                        Dim sHopeDay As String = r_dt.Rows(intCnt - 1).Item("hopeday").ToString

                        If CDate(sHopeDay) <> CDate(sFirstHopeDay) Then
                            .SetText(.GetColFromID("chkbc"), intCnt, "")
                            .SetText(.GetColFromID("chk"), intCnt, "")
                        End If
                    Next
                End If

                .ReDraw = True
            End With

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

        Finally
            mbSkip = False
            Me.spdOrdList.ReDraw = True

        End Try
    End Sub

    '< yjlee 2008-12-29 
    Private Function CheckDuplicated_Order(ByVal r_al_D As String(), ByVal r_al_O As String()) As Boolean
        Dim sFn As String = ""

        Try

            For i As Integer = 0 To r_al_D.Length
                For ii As Integer = 0 To r_al_O.Length - 1
                    If r_al_D(i).ToString().Trim() <> "" And r_al_D(i).ToString().Trim() = r_al_O(ii).ToString().Trim() Then
                        Return True
                    End If
                Next
            Next

            Return False

        Catch ex As Exception

        End Try
    End Function
    '> yjlee 2008-12-29

    Protected Sub sbDisplayOrder_Detail(ByVal r_dt As DataTable, ByVal rbSearch As Boolean)
        Dim sFn As String = "Protected Sub DisplayOrder_Detail(DataTable, Boolean)"

        Try
            mbSkip = True

            If rbSearch = False Then
                sbDisplayOrder_Detail(r_dt)
                Return
            End If

            Dim sBcKeyC As String = ""
            Dim sBcKeyP As String = ""

            Dim iGrpNo As Integer = 0

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

            With spd
                .MaxRows = 0

                .ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For i As Integer = 1 To r_dt.Rows.Count


                    '-----------------------20170714 jjh2 ----------------------------- 식사관련 채혈 주의

                    Dim Stestcd As String = r_dt.Rows(i - 1).Item("testcd").ToString().Trim
                    Dim Sspccd As String = r_dt.Rows(i - 1).Item("spccd").ToString().Trim

                    Dim dt As DataTable = LISAPP.APP_C.Collfn.Fn_FoodWaring_C(Stestcd, Sspccd)

                    If dt.Rows.Count > 0 Then '20170718 yhj
                        If dt.Rows(0).Item("fwgbn").ToString = "1" Then
                            .Col = .GetColFromID("bcclscd") : .Row = i : .BackColor = Color.Yellow

                        End If
                    End If

                    '----------------------------------------------------------

                    '-----------------------20170920 jjh2 -----------------------------  채혈 주의 사항 관련 팝업 자료 가져오기.

                    Dim dt2 As DataTable = LISAPP.APP_C.Collfn.Fn_CWaring_C(Stestcd, Sspccd)

                    If dt2.Rows.Count > 0 Then '20170920 jjh
                        If dt2.Rows(0).Item("cwgbn").ToString = "1" Then
                            MsgBox("검사명 : " + dt2.Rows(0).Item("tnmd").ToString + vbCrLf + dt2.Rows(0).Item("cwarning").ToString + "확인 바랍니다. ", MsgBoxStyle.OkOnly, "채혈시 주의사항 확인요망.")

                        End If
                    End If

                    '-----------------------------------------------------------------


                    For ix2 As Integer = 0 To r_dt.Columns.Count - 1
                        If r_dt.Rows(i - 1).Item(ix2).ToString = "-" Then r_dt.Rows(i - 1).Item(ix2) = ""
                    Next

                    Dim sSpcInfo() As String = r_dt.Rows(i - 1).Item("spcinfo").ToString.Split("|"c)
                    Dim sBcNo As String = ""

                    If sSpcInfo.Length > 1 Then sBcNo = sSpcInfo(0)

                    'BcKey : ordday, dptcd, docno, exlabcd, sectcd, tsectcd, spccd, tubecd
                    sBcKeyC = ""
                    sBcKeyC += r_dt.Rows(i - 1).Item("iogbn").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("deptcd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("wardno").ToString.Trim + "/"
                    'sBcKeyC += r_dt.Rows(i - 1).Item("hopeday").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("exlabcd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("bcclscd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("spccd").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("tubecd").ToString.Trim + "/"
                    sBcKeyC += sBcNo + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("poctyn").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("bconeyn").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("seqtmi").ToString.Trim + "/"
                    sBcKeyC += r_dt.Rows(i - 1).Item("ordday").ToString.Trim

                    .Row = i
                    .Col = .GetColFromID("regno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("regno").ToString().Trim
                    .Col = .GetColFromID("patnm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("patinfo").ToString().Split("|"c)(0).Trim
                    .Col = .GetColFromID("patinfo") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("patinfo").ToString().Trim
                    .Col = .GetColFromID("roomno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("roomno").ToString().Trim
                    .Col = .GetColFromID("orddt") : If .Col > -1 Then .ForeColor = Color.White : .Text = r_dt.Rows(i - 1).Item("orddt").ToString.Trim
                    .Col = .GetColFromID("hopeday") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("hopeday").ToString.Trim
                    .Col = .GetColFromID("deptcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("deptcd").ToString.Trim
                    .Col = .GetColFromID("doctorcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("doctorcd").ToString().Trim
                    .Col = .GetColFromID("testcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("testcd").ToString().Trim
                    .Col = .GetColFromID("entdt") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("entdt").ToString().Trim
                    .Col = .GetColFromID("sunab_date") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sunab_date").ToString().Trim

                    .Col = .GetColFromID("cprtgbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("cprtgbn").ToString.Trim

                    .Col = .GetColFromID("tnmd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tnmd").ToString.Trim

                    Select Case r_dt.Rows(i - 1).Item("bccolor").ToString.Trim
                        Case "1"
                            .BackColor = Me.lblBcColor1.BackColor
                            .ForeColor = Me.lblBcColor1.ForeColor
                        Case "2"
                            .BackColor = Me.lblBcColor2.BackColor
                            .ForeColor = Me.lblBcColor2.ForeColor
                        Case "3"
                            .BackColor = Me.lblBcColor3.BackColor
                            .ForeColor = Me.lblBcColor3.ForeColor
                        Case Else
                            .BackColor = Me.lblBcColor0.BackColor
                            .ForeColor = Me.lblBcColor0.ForeColor
                    End Select

                    .Col = .GetColFromID("spccd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("spccd").ToString().Trim
                    .Col = .GetColFromID("bcclscd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bcclscd").ToString().Trim
                    .Col = .GetColFromID("remark") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("remark").ToString().Replace(vbCrLf, "").Trim
                    .Col = .GetColFromID("remark_nrs")
                    If .Col > -1 Then
                        If r_dt.Rows(i - 1).Item("remark_nrs").ToString().Trim() <> "" Then
                            .Text = r_dt.Rows(i - 1).Item("remark_nrs").ToString().Trim()
                        End If
                    End If
                    .Col = .GetColFromID("minspcvol") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("minspcvol").ToString().Trim

                    .Col = .GetColFromID("erflg")
                    If r_dt.Rows(i - 1).Item("erflg").ToString() = PRG_CONST.Flg_ER Then
                        .Text = Me.lblErFlgE.Text.Trim
                        .BackColor = Me.lblErFlgE.BackColor
                        .ForeColor = Me.lblErFlgE.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("erflg").ToString() = PRG_CONST.Flg_BF Then
                        .Text = Me.lblErFlgB.Text
                        .BackColor = Me.lblErFlgB.BackColor
                        .ForeColor = Me.lblErFlgB.ForeColor
                    Else
                        .Text = ""
                    End If

                    .Col = .GetColFromID("exlabcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("exlabcd").ToString().Trim
                    .Col = .GetColFromID("poctyn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("poctyn").ToString().Trim
                    .Col = .GetColFromID("bconeyn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bconeyn").ToString().Trim
                    .Col = .GetColFromID("seqtyn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("seqtyn").ToString().Trim
                    .Col = .GetColFromID("seqtmi") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("seqtmi").ToString().Trim
                    .Col = .GetColFromID("iogbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("iogbn").ToString().Trim
                    .Col = .GetColFromID("fkocs") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("fkocs").ToString().Trim
                    .Col = .GetColFromID("cwarning") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("cwarning").ToString().Trim
                    .Col = .GetColFromID("height") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("height").ToString().Trim
                    .Col = .GetColFromID("weight") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("weight").ToString().Trim
                    .Col = .GetColFromID("tubecd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tubecd").ToString().Trim

                    Dim sTubeColor As String = Collfn.FindOrder_TUBECOLOR(r_dt.Rows(i - 1).Item("tubecd").ToString().Trim)
                    .Col = .GetColFromID("tubecolor")
                    If sTubeColor = "빨강색" Then
                        .BackColor = Color.Red
                    ElseIf sTubeColor = "주황색" Then
                        .BackColor = Color.Orange
                    ElseIf sTubeColor = "노랑색" Then
                        .BackColor = Color.Yellow
                    ElseIf sTubeColor = "초록색" Then
                        .BackColor = Color.Green
                    ElseIf sTubeColor = "하늘색" Then
                        .BackColor = Color.SkyBlue
                    ElseIf sTubeColor = "파랑색" Then
                        .BackColor = Color.Blue
                    ElseIf sTubeColor = "남색" Then
                        .BackColor = Color.DarkBlue
                    ElseIf sTubeColor = "보라색" Then
                        .BackColor = Color.Purple
                    ElseIf sTubeColor = "흰색" Then
                        .BackColor = Color.WhiteSmoke
                    ElseIf sTubeColor = "검은색" Then
                        .BackColor = Color.Black
                    End If

                    .Col = .GetColFromID("owngbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("owngbn").ToString().Trim
                    .Col = .GetColFromID("liscmt") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("liscmt").ToString().Trim
                    .Col = .GetColFromID("cprtgbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("cprtgbn").ToString.Trim
                    .Col = .GetColFromID("ordcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("ordcd").ToString().Trim

                    .Col = .GetColFromID("append_yn")
                    If r_dt.Rows(i - 1).Item("append_yn").ToString().Trim = PRG_CONST.Flg_Regular Then
                        .Text = ""
                    ElseIf r_dt.Rows(i - 1).Item("append_yn").ToString() = PRG_CONST.Flg_Add Then
                        .Text = r_dt.Rows(i - 1).Item("append_yn").ToString().Trim
                    Else
                        .Text = ""
                    End If

                    .Col = .GetColFromID("bccnt") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("bccnt").ToString().Trim

                    .Col = .GetColFromID("spcflg")
                    If r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Ord Then
                        .Text = ""

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_BcPrt Then
                        .Text = Me.lblOrdFlgB.Text.Trim
                        .BackColor = Me.lblOrdFlgB.BackColor
                        .ForeColor = Me.lblOrdFlgB.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Coll Then
                        .Text = Me.lblOrdFlgC.Text.Trim
                        .BackColor = Me.lblOrdFlgC.BackColor
                        .ForeColor = Me.lblOrdFlgC.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Pass Then
                        .Text = Me.lblOrdFlgP.Text.Trim.Substring(0, 1)
                        .BackColor = Me.lblOrdFlgP.BackColor
                        .ForeColor = Me.lblOrdFlgP.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("spcflg").ToString().Trim = PRG_CONST.Flg_Tk Then
                        .Text = Me.lblOrdFlgT.Text.Trim
                        .BackColor = Me.lblOrdFlgT.BackColor
                        .ForeColor = Me.lblOrdFlgT.ForeColor

                    End If

                    .Col = .GetColFromID("rstflg")
                    If r_dt.Rows(i - 1).Item("rstflg").ToString().Trim = PRG_CONST.Flg_NoRst Then
                        .Text = ""
                    ElseIf r_dt.Rows(i - 1).Item("rstflg").ToString().Trim = PRG_CONST.Flg_Rst Then
                        .Text = Me.lblRstFlgR_img.Text.Trim
                        .BackColor = Me.lblRstFlgR_img.BackColor
                        .ForeColor = Me.lblRstFlgR_img.ForeColor
                    ElseIf r_dt.Rows(i - 1).Item("rstflg").ToString().Trim = PRG_CONST.Flg_Mw Then
                        .Text = Me.lblRstFlgM_img.Text.Trim
                        .BackColor = Me.lblRstFlgM_img.BackColor
                        .ForeColor = Me.lblRstFlgM_img.ForeColor

                    ElseIf r_dt.Rows(i - 1).Item("rstflg").ToString().Trim = PRG_CONST.Flg_Fn Then
                        .Text = Me.lblRstFlgF_img.Text.Trim
                        .BackColor = Me.lblRstFlgF_img.BackColor
                        .ForeColor = Me.lblRstFlgF_img.ForeColor
                    End If

                    .Col = .GetColFromID("spcnmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("spcnmbp").ToString().Trim
                    .Col = .GetColFromID("tcdgbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tcdgbn").ToString().Trim
                    .Col = .GetColFromID("tnmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tnmbp").ToString().Trim
                    .Col = .GetColFromID("tubenmbp") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("tubenmbp").ToString().Trim
                    .Col = .GetColFromID("dc_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("dc_yn").ToString.Trim

                    If sSpcInfo.Length > 1 Then

                        '<< JJH 자체응급일때 Y표시
                        Dim ERYN As String = LISAPP.COMM.RstFn.fnGet_ERYN(sSpcInfo(0).Replace("-", "").Replace(" ", ""))
                        If ERYN = "Y" Then
                            .Col = .GetColFromID("erprtyn") : If .Col > -1 Then .Text = "Y"
                        End If
                        '>>

                        .Col = .GetColFromID("bcno") : If .Col > -1 Then .Text = sSpcInfo(0)
                        .Col = .GetColFromID("prtbcno") : If .Col > -1 Then .Text = sSpcInfo(1)
                        .Col = .GetColFromID("colldt") : If .Col > -1 Then .Text = sSpcInfo(3)
                        .Col = .GetColFromID("collnm") : If .Col > -1 Then .Text = sSpcInfo(2)
                        '.Col = .GetColFromID("passdt") : If .Col > -1 Then .Text = sSpcInfo(4)
                        '.Col = .GetColFromID("passnm") : If .Col > -1 Then .Text = sSpcInfo(5)
                        .Col = .GetColFromID("tkdt") : If .Col > -1 Then .Text = sSpcInfo(7)
                        .Col = .GetColFromID("tknm") : If .Col > -1 Then .Text = sSpcInfo(6)
                        .Col = .GetColFromID("rstdt") : If .Col > -1 Then .Text = sSpcInfo(8)

                    End If

                    .Col = .GetColFromID("sortkey") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sortslip").ToString().Trim + "/" + r_dt.Rows(i - 1).Item("sortl").ToString().Trim
                    .Col = .GetColFromID("wardno") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("wardno").ToString().Trim
                    .Col = .GetColFromID("wardnm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("wardnm").ToString().Trim
                    .Col = .GetColFromID("partgbn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("partgbn").ToString().Trim
                    .Col = .GetColFromID("ordslip") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("ordslip").ToString().Trim
                    .Col = .GetColFromID("rsvdate") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("rsvdate").ToString().Trim
                    .Col = .GetColFromID("wardabbr") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("wardabbr").ToString().Trim
                    .Col = .GetColFromID("deptabbr") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("deptabbr").ToString().Trim

                    .Col = .GetColFromID("tgrpnm")
                    Dim sBuf As String = ""

                    If sBuf.ToUpper().Trim().IndexOf(r_dt.Rows(i - 1).Item("tgrpnm").ToString.ToUpper().Trim()) = -1 Then
                        sBuf += r_dt.Rows(i - 1).Item("tgrpnm").ToString.Trim
                    End If
                    .Text = sBuf
                    sBuf = ""

                    .Col = .GetColFromID("deptnm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("deptnm").ToString().Trim
                    .Col = .GetColFromID("doctornm") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("doctornm").ToString().Trim
                    .Col = .GetColFromID("dtestcd") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("dtestcd").ToString().Trim
                    .Col = .GetColFromID("sunab_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("sunab_yn").ToString().Trim


                    If .Text = "N" Then

                        .Col = .GetColFromID("opencard") '오픈카드관련 추가 20150909
                        If .Text = "Y" Then
                            .Col = .GetColFromID("sunab_yn") : .Col2 = .GetColFromID("sunab_yn")
                            .Row = i : .Row2 = i
                            .BlockMode = True
                            .FontBold = True
                            .BackColor = Color.LightGreen
                            .ForeColor = Color.Crimson
                            .BlockMode = False
                        Else
                            .Col = .GetColFromID("sunab_yn") : .Col2 = .GetColFromID("sunab_yn")
                            .Row = i : .Row2 = i
                            .BlockMode = True
                            .FontBold = True
                            .BackColor = Color.Gainsboro
                            .ForeColor = Color.Crimson
                            .BlockMode = False
                        End If

                    End If
                    .Col = .GetColFromID("opencard") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("opencard").ToString().Trim
                    .Col = .GetColFromID("hold_yn") : If .Col > -1 Then .Text = r_dt.Rows(i - 1).Item("hold_yn").ToString().Trim

                    If .Text = "Y" Then
                        .Col = .GetColFromID("hold_yn") : .Col2 = .GetColFromID("hold_yn")
                        .Row = i : .Row2 = i
                        .BlockMode = True
                        .FontBold = True
                        .BackColor = Color.Gainsboro
                        .ForeColor = Color.DarkMagenta
                        .BlockMode = False
                    End If

                    If sBcKeyC = sBcKeyP And r_dt.Rows(i - 1).Item("bconeyn").ToString().Trim = "0" Then
                        .SetRowItemData(i, iGrpNo)

                        .SetCellBorder(-1, i, -1, i, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexTop, _
                                   Convert.ToUInt32(Microsoft.VisualBasic.RGB(192, 192, 192)), _
                                   FPSpreadADO.CellBorderStyleConstants.CellBorderStyleDot)
                    Else
                        iGrpNo += 1

                        .SetRowItemData(i, iGrpNo)

                        'Line 그리기
                        If i > 1 Then Fn.DrawBorderLineTop(spd, i)

                        .Row = i

                        'grpno
                        .SetText(.GetColFromID("grpno"), i, iGrpNo.ToString)

                        'spcnmd
                        .SetText(.GetColFromID("spcnmd"), i, r_dt.Rows(i - 1).Item("spcnmd").ToString.Trim)

                        'tubenmd
                        .SetText(.GetColFromID("tubenmd"), i, r_dt.Rows(i - 1).Item("tubenmd").ToString.Trim)

                        'orddt
                        .Col = .GetColFromID("orddt") : .Row = i : .ForeColor = Color.Black

                        ''hopeday
                        '.Col = .GetColFromID("hopeday") : .Row = i : .ForeColor = Color.Black
                    End If

                    If r_dt.Rows(i - 1).Item("spcinfo").ToString = "" Then
                        'chk
                        .Col = .GetColFromID("chk") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""

                        'chkbc
                        .Col = .GetColFromID("chkbc") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                    End If

                    'bckey
                    .SetText(.GetColFromID("bckey"), i, sBcKeyC)

                    sBcKeyP = sBcKeyC
                Next

                .Col = .GetColFromID("bckey") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                '-- 2010/07/13 YEJ 추가
                'chkbc
                Dim iRowE As Integer = 0

                For g As Integer = 1 To iGrpNo
                    Dim iChkRow As Integer = 0
                    Dim iRowB As Integer = 0

                    iRowB = iRowE + 1

                    For i As Integer = iRowB To .MaxRows
                        If .GetRowItemData(i) = g Then
                            iRowE = i

                            .Row = i : .Col = .GetColFromID("chk")
                            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                                iChkRow = i
                            End If
                        Else
                            Exit For
                        End If
                    Next

                    For i As Integer = iRowB To iRowE
                        If i = iRowB Then
                            If iChkRow = 0 Then
                                .Col = .GetColFromID("chkbc") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                            End If
                        Else
                            .Col = .GetColFromID("chkbc") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                        End If
                    Next
                Next
                '-- 2010/07/13 YEJ 추가

                .ReDraw = True
            End With

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)
        Finally
            mbSkip = False
            Me.spdOrdList.ReDraw = True
        End Try
    End Sub

    Public Function FindEnabledMerge() As Boolean
        Dim sFn As String = "Public Function FindEnabledMerge() As Boolean"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim bFind As Boolean = False

        Try
            With spd
                Dim iMaxGrpNo As Integer = .GetRowItemData(.MaxRows)

                '_c : current, _m : merge
                For g As Integer = 1 To iMaxGrpNo
                    Dim iRowB_c As Integer = 0
                    Dim iRowE_c As Integer = fnFind_Row_End_With_Same_GrpNo(g, iRowB_c)

                    Dim sBcKey2_c As String = Ctrl.Get_Code(spd, "bckey2", iRowB_c, False)
                    Dim sTSect_c As String = Ctrl.Get_Code(spd, "bcclscd", iRowB_c, False)


                    Dim al_TClsCds_c As New ArrayList

                    For i As Integer = iRowB_c To iRowE_c
                        Dim sChk_c As String = Ctrl.Get_Code(spd, "chk", i, False)
                        '< yjlee 2009-08-01 처방일시는 다르나 동일 검체바코드로 가능한 검사가 존재 
                        ' 여부 판단시 검사코드가 아닌 포함검사 코드로 체크 
                        Dim sTClsCd_c() As String = Ctrl.Get_Code(spd, "dtestcd", i, False).Split(","c)

                        If sChk_c = "1" Then
                            For ii As Integer = 0 To sTClsCd_c.Length - 1
                                If al_TClsCds_c.Contains(sTClsCd_c(ii)) = False Then
                                    al_TClsCds_c.Add(sTClsCd_c(ii))
                                End If
                            Next
                        End If
                        '>
                    Next

                    If al_TClsCds_c.Count < 1 Then Continue For

                    Dim iRowB_m As Integer = .SearchCol(.GetColFromID("bckey2"), iRowE_c, .MaxRows, sBcKey2_c, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRowB_m < 0 Then Continue For

                    Dim iRowE_m As Integer = fnFind_Row_End_With_Same_GrpNo(.GetRowItemData(iRowB_m), iRowB_m)
                    Dim sBcKey2_m As String = Ctrl.Get_Code(spd, "bckey2", iRowB_m, False)
                    Dim sTSect_m As String = Ctrl.Get_Code(spd, "bcclscd", iRowB_m, False)

                    If sTSect_m <> sTSect_c Then Continue For

                    Dim al_TClsCds_m As New ArrayList

                    For i As Integer = iRowB_m To iRowE_m
                        Dim sChk_m As String = Ctrl.Get_Code(spd, "chk", i, False)
                        '< yjlee 2009-08-01 처방일시는 다르나 동일 검체바코드로 가능한 검사가 존재 
                        ' 여부 판단시 검사코드가 아닌 포함검사 코드로 체크 
                        Dim sTClsCd_m() As String = Ctrl.Get_Code(spd, "dtestcd", i, False).Split(","c)

                        If sChk_m = "1" Then
                            For ii As Integer = 0 To sTClsCd_m.Length - 1
                                If al_TClsCds_m.Contains(sTClsCd_m(ii)) = False Then
                                    al_TClsCds_m.Add(sTClsCd_m(ii))
                                End If
                            Next
                        End If
                        '> 
                    Next

                    If sTSect_m = PRG_CONST.BCCLS_BldCrossMatch Then
                        For i As Integer = 1 To al_TClsCds_m.Count
                            If al_TClsCds_c.Contains(al_TClsCds_m(i - 1)) Then
                                bFind = True
                            Else
                                bFind = False

                                Exit For
                            End If
                        Next
                    Else
                        For i As Integer = 1 To al_TClsCds_m.Count
                            If al_TClsCds_c.Contains(al_TClsCds_m(i - 1)) Then
                                bFind = False

                                Exit For
                            Else
                                bFind = True
                            End If
                        Next
                    End If

                    If bFind = False Then Continue For
                    If bFind Then Exit For
                Next
            End With

            Return bFind

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

        End Try
    End Function

    Public Sub MergeOrder(Optional ByVal rbReDraw As Boolean = True)
        Dim sFn As String = "Public Sub MergeOrder()"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList
        Dim bFind As Boolean = False

        Try
            mbMergeMode = True

            With spd
                .ReDraw = False

                For i As Integer = 1 To .MaxRows
                    .SetText(.GetColFromID("sortkey"), i, i)
                Next

                Dim iMaxGrpNo As Integer = .GetRowItemData(.MaxRows)

                '_c : current, _m : merge
                For g As Integer = 1 To iMaxGrpNo
                    Dim iRowB_c As Integer = 0
                    Dim iRowE_c As Integer = fnFind_Row_End_With_Same_GrpNo(g, iRowB_c)

                    Dim sBcKey2_c As String = Ctrl.Get_Code(spd, "bckey2", iRowB_c, False)
                    Dim sTSect_c As String = Ctrl.Get_Code(spd, "bcclscd", iRowB_c, False)


                    'If sTSect_c = Const_TSect_BldCrossMatch Then Continue For

                    Dim al_TClsCds_c As New ArrayList

                    For i As Integer = iRowB_c To iRowE_c
                        Dim sChk_c As String = Ctrl.Get_Code(spd, "chk", i, False)
                        Dim sTClsCd_c As String = Ctrl.Get_Code(spd, "testcd", i, False)

                        If sChk_c = "1" Then
                            If al_TClsCds_c.Contains(sTClsCd_c) = False Then
                                al_TClsCds_c.Add(sTClsCd_c)
                            End If
                        End If
                    Next

                    If al_TClsCds_c.Count < 1 Then Continue For

                    Dim iRowB_m As Integer = .SearchCol(.GetColFromID("bckey2"), iRowE_c, .MaxRows, sBcKey2_c, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRowB_m < 0 Then Continue For

                    Dim iRowE_m As Integer = fnFind_Row_End_With_Same_GrpNo(.GetRowItemData(iRowB_m), iRowB_m)
                    Dim sBcKey2_m As String = Ctrl.Get_Code(spd, "bckey2", iRowB_m, False)
                    Dim sTSect_m As String = Ctrl.Get_Code(spd, "bcclscd", iRowB_m, False)

                    'If sTSect_m = Const_TSect_BldCrossMatch Then Continue For
                    If sTSect_m <> sTSect_c Then Continue For

                    Dim al_TClsCds_m As New ArrayList

                    For i As Integer = iRowB_m To iRowE_m
                        Dim sChk_m As String = Ctrl.Get_Code(spd, "chk", i, False)
                        Dim sTClsCd_m As String = Ctrl.Get_Code(spd, "testcd", i, False)

                        If sChk_m = "1" Then
                            If al_TClsCds_m.Contains(sTClsCd_m) = False Then
                                al_TClsCds_m.Add(sTClsCd_m)
                            End If
                        End If
                    Next

                    If sTSect_m = PRG_CONST.BCCLS_BldCrossMatch Then
                        For i As Integer = 1 To al_TClsCds_m.Count
                            If al_TClsCds_c.Contains(al_TClsCds_m(i - 1)) Then
                                bFind = True
                            Else
                                bFind = False

                                Exit For
                            End If
                        Next
                    Else
                        For i As Integer = 1 To al_TClsCds_m.Count
                            If al_TClsCds_c.Contains(al_TClsCds_m(i - 1)) Then
                                bFind = False

                                Exit For
                            Else
                                bFind = True
                            End If
                        Next
                    End If

                    If bFind = False Then Continue For

                    If bFind Then
                        For i As Integer = iRowB_m To iRowE_m
                            Dim sChk_m As String = Ctrl.Get_Code(spd, "chk", i, False)

                            If sChk_m = "1" Then
                                .SetRowItemData(i, g)
                                .SetText(.GetColFromID("grpno"), i, "")
                                .SetText(.GetColFromID("sortkey"), i, iRowE_c.ToString + "." + i.ToString("D4"))

                                .Col = .GetColFromID("chkbc") : .Row = i

                                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                    .Text = ""
                                End If

                                If i = iRowB_m Then
                                    .Col = 1 : .Col2 = .MaxCols
                                    '128, 128, 128
                                    .SetCellBorder(1, i, .MaxCols, i, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexTop, Convert.ToUInt32(Microsoft.VisualBasic.RGB(255, 0, 0)), FPSpreadADO.CellBorderStyleConstants.CellBorderStyleFineDot)
                                End If
                            End If
                        Next
                    End If
                Next

                For i As Integer = 1 To .MaxRows
                    .Col = .GetColFromID("grpno") : .Row = i
                    .CellTag = .GetRowItemData(i).ToString
                Next

                .SortBy = FPSpreadADO.SortByConstants.SortByRow
                .set_SortKey(1, .GetColFromID("sortkey"))
                .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .Action = FPSpreadADO.ActionConstants.ActionSort

                For i As Integer = 1 To .MaxRows
                    .Col = .GetColFromID("grpno") : .Row = i
                    .SetRowItemData(i, Convert.ToInt32(Val(.CellTag)))
                Next

                '-- 중복데이타 채크
                For ix1 As Integer = 1 To .MaxRows
                    .Row = ix1
                    .Col = .GetColFromID("chkbc")
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox And .Text = "1" Then
                        .Row = ix1
                        .Col = .GetColFromID("chkbc") : Dim sChkBc As String = .Text
                        .Col = .GetColFromID("grpno") : Dim iSelGrp As Integer = CInt(.Text)

                        Dim iRowB As Integer = 0
                        Dim iRowE As Integer = fnFind_Row_End_With_Same_GrpNo(iSelGrp, iRowB)

                        For ix2 As Integer = ix1 To iRowE
                            If fnFind_Duplicated_Order(ix2, iRowB) Then
                                .Row = ix2
                                .Col = .GetColFromID("chk") : .Text = ""
                            Else
                                If fnFind_Duplicated_IncludeOrder(ix2, 1) Then
                                    .SetText(.GetColFromID("chk"), ix2, "")
                                End If
                            End If
                        Next
                    End If
                Next

                .ReDraw = rbReDraw
            End With

            Return

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

        End Try
    End Sub

    Public Sub MergeBatch()
        'With spdOrdList
        '    ' 다중 Sort를 위한 설정
        '    .Col = 1 : .Col2 = .MaxCols
        '    .Row = 1 : .Row2 = .MaxRows
        '    .BlockMode = True
        '    .SortBy = FPSpreadADO.SortByConstants.SortByRow
        '    .set_SortKey(1, .GetColFromID("bckey2"))
        '    .set_SortKey(2, .GetColFromID("chkbc"))
        '    .set_SortKey(3, .GetColFromID("chk"))
        '    .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
        '    .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderDescending)
        '    .set_SortKeyOrder(3, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderDescending)
        '    .Action = FPSpreadADO.ActionConstants.ActionSort
        '    .BlockMode = False


        '    For ix As Integer = 1 To .MaxRows
        '        .Row = ix
        '        .Col = .GetColFromID("chk") : Dim sChk As String = .Text

        '        If sChk <> "1" Then
        '            .Col = .GetColFromID("chkbc")
        '            If .CellType <> FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
        '                .MaxRows += 1
        '                .Row = ix
        '                .Action = FPSpreadADO.ActionConstants.ActionInsertRow

        '                For ix2 As Integer = 1 To .MaxCols
        '                    .Row = ix + 1
        '                    .Col = ix2 : Dim sTmp As String = .Text

        '                    .Row = ix
        '                    .Col = ix2 : .Text = sTmp
        '                Next

        '                .Row = ix + 1
        '                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
        '                .MaxRows -= 1

        '                Exit For
        '            End If
        '        End If
        '    Next

        '    Dim iGrpNo As Integer = 0

        '    For ix As Integer = 1 To .MaxRows
        '        .Row = ix
        '        .Col = .GetColFromID("chkbc")
        '        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
        '            iGrpNo += 1

        '            .SetRowItemData(ix, iGrpNo)

        '            'Line 그리기
        '            If ix > 1 Then Fn.DrawBorderLineTop(spdOrdList, ix)

        '            .Col = .GetColFromID("grpno") : .Text = iGrpNo.ToString
        '            '.Col = .GetColFromID("chkbc") : If .Text <> "1" Then .Text = "1"
        '        Else
        '            .SetRowItemData(ix, iGrpNo)
        '        End If
        '    Next

        'End With

    End Sub

    Private Function fnFind_collData(ByVal riRow As Integer, ByVal rdtSysDt As Date, Optional ByVal rbLabel As Boolean = False,
                                     Optional ByVal rsBolPrntNum As Boolean = False, Optional ByVal rsPrntNum As String = "0") As STU_CollectInfo
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim collData As New STU_CollectInfo

        collData.REGNO = m_cpi.REGNO
        collData.PATNM = m_cpi.PATNM
        collData.SEX = m_cpi.SEX
        collData.AGE = m_cpi.AGE           ' 나이
        collData.IDNOL = m_cpi.IDNOL       ' 주민등록번호 왼쪽
        collData.IDNOR = m_cpi.IDNOR       ' 주민등록번호 오른쪽
        collData.BIRTHDAY = m_cpi.BIRTHDAY ' 생일
        collData.TEL1 = m_cpi.TEL1         ' 연락처1
        collData.TEL2 = m_cpi.TEL2         ' 연락처2

        '< 일 환산 나이
        If IsDate(m_cpi.BIRTHDAY) Then
            m_cpi.DAGE = CType(DateDiff(DateInterval.Day, CDate(m_cpi.BIRTHDAY), rdtSysDt), String)
        Else
            m_cpi.DAGE = ""
        End If

        collData.DAGE = m_cpi.DAGE
        '>  
        collData.DEPTCD = Ctrl.Get_Code(spd, "deptcd", riRow, False)     ' 진료과코드
        collData.DEPTNM = Ctrl.Get_Code(spd, "deptnm", riRow, False)
        collData.DOCTORCD = Ctrl.Get_Code(spd, "doctorcd", riRow, False)     ' 의사코드
        collData.DOCTORNM = Ctrl.Get_Code(spd, "doctornm", riRow, False)     ' 

        collData.WARDNO = Ctrl.Get_Code(spd, "wardno", riRow, False)    ' 병동코드
        collData.WARDNM = Ctrl.Get_Code(spd, "wardnm", riRow, False)    ' 병실명
        collData.ROOMNO = Ctrl.Get_Code(spd, "roomno", riRow, False)    ' 병실코드
        collData.BEDNO = ""                                             ' 병상코드

        collData.ORDDT = Ctrl.Get_Code(spd, "orddt", riRow, False).Replace("-", "").Replace(":", "").Replace(" ", "")
        If collData.ORDDT.Length = 12 Then collData.ORDDT = collData.ORDDT + "00"

        collData.JUBSUGBN = "0"
        collData.REMARK = Ctrl.Get_Code(spd, "remark", riRow, False)
        collData.REMARK_NRS = Ctrl.Get_Code(spd, "remark_nrs", riRow, False)
        collData.IOGBN = Ctrl.Get_Code(spd, "iogbn", riRow, False)
        collData.FKOCS = Ctrl.Get_Code(spd, "fkocs", riRow, False)
        collData.HEIGHT = Ctrl.Get_Code(spd, "height", riRow, False)
        collData.WEIGHT = Ctrl.Get_Code(spd, "weight", riRow, False)
        collData.STATGBN = Ctrl.Get_Code(spd, "erflg", riRow, False)
        collData.TCLSCD = Ctrl.Get_Code(spd, "testcd", riRow, False) '검사코드 
        collData.SPCCD = Ctrl.Get_Code(spd, "spccd", riRow, False)
        collData.OWNGBN = Ctrl.Get_Code(spd, "owngbn", riRow, False)

        '< yjlee 2009-01-05 부천순천향병원 
        collData.TGRPNM = Ctrl.Get_Code(spd, "tgrpnm", riRow, False)
        collData.TORDCD = Ctrl.Get_Code(spd, "ordcd", riRow, False)
        collData.SEQTMI = Convert.ToInt32(Val(Ctrl.Get_Code(spd, "seqtmi", riRow, False)))
        '> 

        collData.BCCLSCD = Ctrl.Get_Code(spd, "bcclscd", riRow, False)

        If collData.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Then
            miPints += 1
            collData.TCLSCD = "L" + PRG_CONST.BCCLS_BldCrossMatch + miPints.ToString("D2")
        End If

        collData.EXLABCD = Ctrl.Get_Code(spd, "exlabcd", riRow, False)
        collData.POCTYN = Ctrl.Get_Code(spd, "poctyn", riRow, False)
        collData.BCONEYN = Ctrl.Get_Code(spd, "bconeyn", riRow, False)
        collData.TUBECD = Ctrl.Get_Code(spd, "tubecd", riRow, False)
        collData.COMMENT = Ctrl.Get_Code(spd, "liscmt", riRow, False)

        collData.BCKEY = Ctrl.Get_Code(spd, "bckey", riRow, False)
        collData.BCKEY2 = Ctrl.Get_Code(spd, "bckey2", riRow, False)
        collData.BCKEY3 = Ctrl.Get_Code(spd, "bckey3", riRow, False)

        collData.COLLDT = rdtSysDt.ToString("yyyyMMddHHmmss")
        collData.COLLID = msCollUsrId

        collData.TCDGBN = Ctrl.Get_Code(spd, "tcdgbn", riRow, False)
        collData.TNMBP = Ctrl.Get_Code(spd, "tnmbp", riRow, False)
        collData.SPCNMBP = Ctrl.Get_Code(spd, "spcnmbp", riRow, False)

        If collData.BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Then
            collData.SPCNMBP = PRG_CONST.BCPRTNM_Transfusion
        End If

        collData.TUBENMBP = Ctrl.Get_Code(spd, "tubenmbp", riRow, False)

        collData.HREGNO = m_cpi.WHOSPID
        collData.TKDT = ""
        '외래채혈 화면에서 바코드 출력 시 감염정보 G로 표기 요청
        If moForm.Text = "FGC31ː외래채혈" Then
            If OCSAPP.OcsLink.Pat.fnGet_Pat_Infection(m_cpi.REGNO, True) <> "" Then
                collData.INFINFO = "G"
            End If
        Else
            collData.INFINFO = OCSAPP.OcsLink.Pat.fnGet_Pat_Infection(m_cpi.REGNO, True)
        End If


        collData.ENTDT = Ctrl.Get_Code(spd, "entdt", riRow, False).Replace("-", "").Replace(":", "").Replace(" ", "")
        collData.BCCNT = Ctrl.Get_Code(spd, "bccnt", riRow, False)
        '<혈액형 결과 없고 크로스매칭 검사 진행시 팝업 추가 2019-04-26
        Dim dt As DataTable = New DataTable
        dt = CGDA_BT.fnGet_ABORh(m_cpi.REGNO)

        If dt.Rows.Count <= 0 And (collData.TCLSCD = "LB141" Or collData.TCLSCD = "LB142") Then

            If MsgBox("혈액형 결과가 없는 초진 환자입니다. 그래도 크로스매칭 검사를 진행하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return Nothing
            End If

        End If

        collData.SUNABYN = Ctrl.Get_Code(spd, "sunab_yn", riRow, False)
        collData.CPRTGBN = Ctrl.Get_Code(spd, "cprtgbn", riRow, False)

        collData.OPDT = Ctrl.Get_Code(spd, "opdt", riRow, False).Replace("-", "").Replace(":", "").Replace(" ", "")
        collData.RESDT = Ctrl.Get_Code(spd, "resdt", riRow, False).Replace("-", "").Replace(":", "").Replace(" ", "")
        collData.PARTGBN = Ctrl.Get_Code(spd, "partgbn", riRow, False)
        collData.ORDSLIP = Ctrl.Get_Code(spd, "ordslip", riRow, False)

        collData.DEPTABBR = Ctrl.Get_Code(spd, "deptabbr", riRow, False)
        collData.WARDABBR = Ctrl.Get_Code(spd, "wardabbr", riRow, False)

        If rbLabel Then
            collData.BCNO = Ctrl.Get_Code(spd, "bcno", riRow, False)
            collData.PRTBCNO = Ctrl.Get_Code(spd, "prtbcno", riRow, False)
        End If

        collData.ERPRTYN = Ctrl.Get_Code(spd, "erprtyn", riRow, False) '<<<20180801 응급바코드 추가

        '20211104 jhs 바코드 장수 더하기 위해 추가
        If rsBolPrntNum Then
            collData.CHKADDPRNT = rsBolPrntNum
            collData.PRNTNUM = rsPrntNum
        End If
        '---------------------------

        Return collData
    End Function

    Private Function fnFind_Duplicated_Order(ByVal riRow As Integer, ByVal rsBcKey As String, ByVal rsTClsCd As String) As Boolean
        If riRow = 1 Then Return False
        If rsTClsCd = "" Then Return False

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        With spd
            For i As Integer = riRow - 1 To 1 Step -1
                Dim sBcKey As String = Ctrl.Get_Code(spd, "bckey", i, False)
                Dim sTClsCd As String = Ctrl.Get_Code(spd, "testcd", i, False)
                Dim sBcclsCd As String = Ctrl.Get_Code(spd, "bcclscd", i, False)

                If sBcKey <> rsBcKey Then Return False
                If sBcKey = rsBcKey And sBcclsCd = PRG_CONST.BCCLS_BldCrossMatch Then Return False

                If sBcKey = rsBcKey And sTClsCd.Length > 0 And sTClsCd = rsTClsCd Then
                    Return True
                End If
            Next
        End With

        Return False
    End Function

    Private Function fnFind_Duplicated_Order(ByVal riRow As Integer, ByVal riRowB As Integer) As Boolean
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        With spd
            Dim sBcKeyC As String = Ctrl.Get_Code(spd, "bckey", riRow, False)
            Dim sTClsCdC As String = Ctrl.Get_Code(spd, "testcd", riRow, False)

            For i As Integer = riRow - 1 To riRowB Step -1
                Dim sChkP As String = Ctrl.Get_Code(spd, "chk", i, False)
                Dim sBcKeyP As String = Ctrl.Get_Code(spd, "bckey", i, False)
                Dim sTClsCdP As String = Ctrl.Get_Code(spd, "testcd", i, False)
                Dim sTSectGbnP As String = Ctrl.Get_Code(spd, "bcclscd", i, False)

                If sChkP = "1" Then
                    If sBcKeyP <> sBcKeyC Then Return False
                    If sBcKeyP = sBcKeyC And sTSectGbnP = PRG_CONST.BCCLS_BldCrossMatch Then Return False

                    If sBcKeyP = sBcKeyC And sTClsCdC.Length > 0 And sTClsCdP = sTClsCdC Then
                        Return True
                    End If
                End If
            Next
        End With

        Return False
    End Function

    '-- rsKeyGbn : 
    Private Function fnFind_Duplicated_IncludeOrder(ByVal riRow As Integer, ByVal riRowB As Integer) As Boolean
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        With spd
            Dim sBcKeyC As String = Ctrl.Get_Code(spd, "bckey", riRow, False)
            If mbMergeMode Then
                sBcKeyC = Ctrl.Get_Code(spd, "bckey2", riRow, False)
            End If
            Dim sTClsCdC As String = Ctrl.Get_Code(spd, "dtestcd", riRow, False)

            For i As Integer = riRow - 1 To riRowB Step -1
                Dim sChkP As String = Ctrl.Get_Code(spd, "chk", i, False)
                Dim sBcKeyP As String = Ctrl.Get_Code(spd, "bckey", i, False)
                If mbMergeMode Then
                    sBcKeyP = Ctrl.Get_Code(spd, "bckey2", i, False)
                End If

                Dim sTClsCdP As String = Ctrl.Get_Code(spd, "dtestcd", i, False)
                Dim sTSectGbnP As String = Ctrl.Get_Code(spd, "bcclscd", i, False)

                If sChkP = "1" Then
                    If sBcKeyP <> sBcKeyC Then Return False

                    If sBcKeyP = sBcKeyC And sTSectGbnP = PRG_CONST.BCCLS_BldCrossMatch Then Return False
                    If sBcKeyP = sBcKeyC _
                            And CheckDuplicated_Order(sTClsCdC.Split(",".ToCharArray()), sTClsCdP.Split(",".ToCharArray())) Then
                        Return True
                    End If
                End If
            Next
        End With

        Return False
    End Function

    Private Function fnFind_Exist_Change(ByVal r_dt_pre As DataTable, ByVal r_dt_cur As DataTable) As Boolean
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        With Me.spdOrdList
            For i As Integer = 1 To .MaxRows
                Dim sChk As String = Ctrl.Get_Code(spd, "chk", i, False)
                Dim sFkOcs As String = Ctrl.Get_Code(spd, "fkocs", i, False)
                Dim sTOrdCd As String = Ctrl.Get_Code(spd, "ordcd", i, False)

                Dim a_dr_pre As DataRow()
                Dim a_dr_cur As DataRow()

                If sChk = "1" Then
                    a_dr_pre = r_dt_pre.Select("fkocs = '" + sFkOcs + "'")
                    a_dr_cur = r_dt_cur.Select("fkocs = '" + sFkOcs + "'")

                    If a_dr_pre.Length = a_dr_cur.Length Then 'If a_dr_pre.Length = 1 And a_dr_cur.Length = 1 Then
                        For c As Integer = 1 To a_dr_pre(0).Table.Columns.Count
                            If a_dr_pre(0).Item(c - 1).ToString <> a_dr_cur(0).Item(c - 1).ToString Then
                                Return True
                            End If
                        Next
                    Else
                        Return True
                    End If
                End If
            Next
        End With

        Return False
    End Function

    Private Function fnFind_Row_End_With_Same_GrpNo(ByVal riGrpNo As Integer, ByRef riRowB As Integer) As Integer
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim iRowB As Integer = 0

        iRowB = spd.SearchCol(spd.GetColFromID("grpno"), 0, spd.MaxRows, riGrpNo.ToString, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

        If iRowB < 1 Then
            riRowB = 0

            Return 0
        End If

        riRowB = iRowB

        Dim iRowE As Integer = 0

        '< add freety 2007/11/26 : iMaxGrpNo < riGrpNo 이거나 riGrpNo보다 큰 GrpNo가 수정되어 작은경우 오류 처리
        For r As Integer = iRowB + 1 To spd.MaxRows
            Dim iGrpNoCur As Integer = spd.GetRowItemData(r)

            iRowE = r - 1

            If riGrpNo <> iGrpNoCur And iGrpNoCur > 0 Then
                Return iRowE
            End If
        Next
        '>

        Return spd.MaxRows
    End Function

    Private Function fnGet_PatList(ByVal riRow As Integer) As STU_PatInfo

        Dim sFn As String = "Private Function fnGet_PatList() As STU_PatInfo"

        Dim cpi As New STU_PatInfo
        If riRow < 1 Then riRow = 1

        If Me.spdOrdList.MaxRows = 0 Then Return cpi

        'If spdOrdList.ActiveRow > 0 Then iRow = spdOrdList.ActiveRow

        Try
            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim iCol As Integer = 0

            With Me.spdOrdList
                .Row = riRow

                Dim a_sPatInfo(100) As String
                iCol = .GetColFromID("patinfo") : If iCol > 0 Then .Col = iCol : a_sPatInfo = .Text.Split(Chr(124))

                '< 나이계산
                Dim dtBirthDay As Date = CDate(a_sPatInfo(2).Trim)
                Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                '>

                iCol = .GetColFromID("roomno") : If iCol > 0 Then .Col = iCol : cpi.ROOMNO = .Text
                iCol = .GetColFromID("regno") : If iCol > 0 Then .Col = iCol : cpi.REGNO = .Text
                iCol = .GetColFromID("patnm") : If iCol > 0 Then .Col = iCol : cpi.PATNM = .Text

                cpi.SEX = a_sPatInfo(1).Trim
                cpi.AGE = iAge.ToString
                cpi.IDNOL = a_sPatInfo(6).Trim
                cpi.IDNOR = a_sPatInfo(7).Trim
                cpi.BIRTHDAY = IIf(a_sPatInfo(2).Trim.Length = 10, a_sPatInfo(2), Fn.Format_Day8ToDay10(a_sPatInfo(2).Trim)).ToString
                If cpi.IDNOR <> "" Then
                    cpi.IDNO = cpi.IDNOL + "-" + cpi.IDNOR.Substring(0, 1) + "******"
                Else
                    cpi.IDNO = ""
                End If
                cpi.TEL1 = a_sPatInfo(4).Trim
                cpi.TEL2 = a_sPatInfo(5).Trim

                iCol = .GetColFromID("wardno") : If iCol > 0 Then .Col = iCol : cpi.WARD = .Text
                iCol = .GetColFromID("wardnm") : If iCol > 0 Then .Col = iCol : cpi.WARDNM = .Text
                iCol = .GetColFromID("deptcd") : If iCol > 0 Then .Col = iCol : cpi.DEPTCD = .Text
                iCol = .GetColFromID("deptnm") : If iCol > 0 Then .Col = iCol : cpi.DEPTNM = .Text
                iCol = .GetColFromID("doctorcd") : If iCol > 0 Then .Col = iCol : cpi.DOCTORCD = .Text
                iCol = .GetColFromID("doctornm") : If iCol > 0 Then .Col = iCol : cpi.DOCTORNM = .Text
                iCol = .GetColFromID("entdt") : If iCol > 0 Then .Col = iCol : cpi.ENTDT = .Text
                iCol = .GetColFromID("owngbn") : If iCol > 0 Then .Col = iCol : cpi.OWNGBN = .Text
                iCol = .GetColFromID("orddt") : If iCol > 0 Then .Col = iCol : cpi.ORDDT = .Text
                iCol = .GetColFromID("erflg") : If iCol > 0 Then .Col = iCol : cpi.ERFLG = .Text

                Dim sFkocs As String = ""
                iCol = .GetColFromID("fkocs") : If iCol > 0 Then .Col = iCol : sFkocs = .Text

                cpi.RESDT = OCSAPP.OcsLink.Ord.fnGet_Ord_ResdtInfo_fkocs(sFkocs)    '-- 예약일자

                Dim sDiagNm As String = ""
                Dim a_sDiagNm As String()
                Dim sWHospId As String = ""

                Dim sInfInfo As String = OCSAPP.OcsLink.Pat.fnGet_Pat_Infection(cpi.REGNO, True)

                sDiagNm = OCSAPP.OcsLink.Pat.fnGet_DiagNm(cpi.REGNO, cpi.ORDDT.Substring(0, 10), cpi.ORDDT.Substring(0, 10), cpi.OWNGBN)
                a_sDiagNm = sDiagNm.Split(Convert.ToChar(124))
                sWHospId = cpi.REGNO

                '<
                Dim LeukemiaChk As Boolean = False
                Dim LeukemiaDt As DataTable = OCSAPP.OcsLink.Pat.fnGet_Diag_Leukemia()

                If LeukemiaDt.Rows.Count > 0 Then

                    If a_sDiagNm(0) = "" Then '-- 진단명(한글) 없고 진단명(영문) 있을때
                        If a_sDiagNm(1) <> "" Then
                            For i As Integer = 0 To LeukemiaDt.Rows.Count - 1
                                If a_sDiagNm(1) = LeukemiaDt.Rows(i).Item("DIAG_ENG").ToString Then
                                    LeukemiaChk = True
                                    Exit For
                                End If
                            Next
                        End If
                    Else '-- 진단명(한글) 있을때
                        For i As Integer = 0 To LeukemiaDt.Rows.Count - 1
                            If a_sDiagNm(0) = LeukemiaDt.Rows(i).Item("DIAG_HNG").ToString Then
                                LeukemiaChk = True
                                Exit For
                            End If
                        Next
                    End If
                End If

                '-- 혈액종양질환 진단명 구분
                cpi.DiagLeukemia = LeukemiaChk

                '>

                cpi.DIAG_K = a_sDiagNm(0)
                cpi.DIAG_E = a_sDiagNm(1)
                cpi.WHOSPID = sWHospId
                cpi.INFINFO = sInfInfo

                cpi.SPCOMMENT = Collfn.fnGet_Comment_pat(msIoGbn, cpi.REGNO)

                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("height") : Dim sHeight As String = .Text
                    .Col = .GetColFromID("weight") : Dim sWeight As String = .Text

                    If sHeight.Length + sWeight.Length > 0 Then
                        cpi.HEIGHT = sHeight
                        cpi.WEIGHT = sWeight
                        Exit For
                    End If
                Next

                Dim dt As New DataTable

                dt = CGDA_BT.fnGet_ABORh(cpi.REGNO)
                If dt.Rows.Count > 0 Then
                    cpi.ABORh = dt.Rows(0).Item("aborh").ToString.Trim
                End If
            End With


            Return cpi

        Catch ex As Exception
            Fn.log(sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return Nothing
        End Try

    End Function


    Private Sub sbLog_Msg(ByVal rsType As String, ByVal rsMsg As String)
        If rsType.Length > 0 Then
            rsMsg = "[" + rsType + "] " + rsMsg
        End If

        Me.lstMsg.Items.Insert(0, rsMsg)
    End Sub

    '<--- Control Event --->
    Private Sub AxCollList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Control.CheckForIllegalCrossThreadCalls = False

        spdOrdList.AddCellSpan(14, 0, 15, 1)
        Clear()

        'sbDisp_Cols()
    End Sub

    Private Sub spdOrdList_BlockSelected(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BlockSelectedEvent) Handles spdOrdList.BlockSelected
        Dim sFn As String = "Handles spdOrdList.BlockSelected"

        If e.blockCol < 1 Then Return
        If e.blockCol2 < 1 Then Return

        If e.blockRow > 0 Then Return
        If e.blockRow2 > 0 Then Return

        If e.blockCol <> e.blockCol2 Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Try
            mbSkip = True

            Dim iRowChk As Integer = 0
            Dim iRowGrp As Integer = 0
            Dim iMaxGrpNo As Integer = 0
            Dim bDuplicated As Boolean = False
            Dim bDuplicated_IncludeOrder As Boolean = False

            With spd
                If e.blockCol <> .GetColFromID("chk") Then Return

                iRowChk = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                iMaxGrpNo = .GetRowItemData(.MaxRows)

                If iRowChk > 0 Then
                    .Col = .GetColFromID("chk") : .Col2 = .GetColFromID("chk")
                    .Row = 1 : .Row2 = .MaxRows
                    .BlockMode = True
                    .Text = ""
                    .BlockMode = False
                Else
                    For i As Integer = 1 To .MaxRows
                        .Col = .GetColFromID("chk")
                        .Row = i

                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            bDuplicated = fnFind_Duplicated_Order(i, 1)

                            If bDuplicated Then
                                .SetText(.GetColFromID("chk"), i, "")

                                sbLog_Msg("중복", "중복처방은 동시에 선택할 수 없습니다!!")
                            Else
                                .SetText(.GetColFromID("chk"), i, "1")
                            End If

                            '< yjlee 2009-02-12 
                            ' # Panel 또는 Group에 포함된 단일 검사코드의 중복 체크
                            If Not bDuplicated Then
                                bDuplicated_IncludeOrder = fnFind_Duplicated_IncludeOrder(i, 1)

                                If bDuplicated_IncludeOrder Then
                                    .SetText(.GetColFromID("chk"), i, "")

                                    sbLog_Msg("중복", "중복처방은 동시에 선택할 수 없습니다!!")
                                Else
                                    .SetText(.GetColFromID("chk"), i, "1")
                                End If
                                '> yjlee 2009-02-12
                            End If
                        End If
                    Next
                End If

                For g As Integer = 1 To iMaxGrpNo
                    iRowGrp = .SearchCol(.GetColFromID("grpno"), iRowGrp, .MaxRows, g.ToString, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRowGrp > 0 Then
                        .Col = .GetColFromID("chkbc") : .Row = iRowGrp

                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            If iRowChk > 0 Then
                                .SetText(.GetColFromID("chkbc"), iRowGrp, "")
                            Else
                                .SetText(.GetColFromID("chkbc"), iRowGrp, "1")
                            End If
                        End If
                    End If
                Next

                .ClearSelection()
            End With

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

        Finally
            spd.ClearSelection()

            mbSkip = False

        End Try
    End Sub

    Private Sub spdOrdList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdOrdList.ButtonClicked
        Dim sFn As String = "Handles spdOrdList.ButtonClicked"

        If mbSkip Then Return
        If e.row < 0 Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        Dim sChkGbn As String = ""
        Dim sFkOcs As String = ""
        Dim iSelGrp As Integer = 0

        Dim iRowB As Integer = 0
        Dim iRowE As Integer = 0

        Dim bDuplicated As Boolean = False
        Dim bDuplicated_IncludeOrder As Boolean = False '< Panel 에 포함된 중복 처방 체크 

        Try
            mbSkip = True

            With spd
                .ReDraw = False

                .SetActiveCell(e.col + 1, e.row)

                If e.col = .GetColFromID("chkbc") Then
                    .Col = e.col : .Row = e.row

                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                        sFkOcs = Ctrl.Get_Code(spd, "fkocs", e.row, False)
                        sChkGbn = Ctrl.Get_Code(spd, "chkbc", e.row, False)
                        iSelGrp = CInt(Ctrl.Get_Code(spd, "grpno", e.row, False))

                        iRowE = fnFind_Row_End_With_Same_GrpNo(iSelGrp, iRowB)

                        For i As Integer = e.row To iRowE
                            If sChkGbn = "1" Then
                                bDuplicated = fnFind_Duplicated_Order(i, iRowB)

                                If bDuplicated Then
                                    .SetText(.GetColFromID("chk"), i, "")

                                    sbLog_Msg("중복", "중복처방은 동시에 선택할 수 없습니다!!")
                                Else
                                    .SetText(.GetColFromID("chk"), i, sChkGbn)

                                End If

                                '< yjlee 2009-03-13 
                                ' # Panel 또는 Group에 포함된 단일 검사코드의 중복 체크
                                If Not bDuplicated Then
                                    bDuplicated_IncludeOrder = fnFind_Duplicated_IncludeOrder(i, 1)

                                    If bDuplicated_IncludeOrder Then
                                        .SetText(.GetColFromID("chk"), i, "")

                                        sbLog_Msg("중복", "중복처방은 동시에 선택할 수 없습니다!!")
                                    Else
                                        .SetText(.GetColFromID("chk"), i, sChkGbn)
                                    End If

                                End If
                                '> yjlee 2009-03-13 
                            Else
                                .SetText(.GetColFromID("chk"), i, sChkGbn)
                            End If

                            If bDuplicated_IncludeOrder Or bDuplicated Then
                            Else
                                '-- 2010/04/13 group 처방 추가
                                .Col = .GetColFromID("chk") : .Row = i
                                sbDisplay_Fkocs_Select(.Text, sFkOcs, i)
                            End If

                        Next
                    End If

                ElseIf e.col = .GetColFromID("chk") Then
                    .Col = e.col : .Row = e.row

                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                        sFkOcs = Ctrl.Get_Code(spd, "fkocs", e.row, False)
                        sChkGbn = Ctrl.Get_Code(spd, "chk", e.row, False)
                        iSelGrp = spd.GetRowItemData(e.row)

                        iRowE = fnFind_Row_End_With_Same_GrpNo(iSelGrp, iRowB)

                        If sChkGbn = "1" Then
                            bDuplicated = fnFind_Duplicated_Order(e.row, iRowB)

                            If bDuplicated Then
                                .SetText(.GetColFromID("chk"), e.row, "")

                                sbLog_Msg("중복", "중복처방은 동시에 선택할 수 없습니다!!")
                            Else
                                .SetText(.GetColFromID("chk"), e.row, sChkGbn)
                            End If


                            '< yjlee 2009-02-12 
                            ' # Panel 또는 Group에 포함된 단일 검사코드의 중복 체크
                            If Not bDuplicated Then
                                bDuplicated_IncludeOrder = fnFind_Duplicated_IncludeOrder(e.row, iRowB)

                                If bDuplicated_IncludeOrder Then
                                    .SetText(.GetColFromID("chk"), e.row, "")

                                    sbLog_Msg("중복", "중복처방은 동시에 선택할 수 없습니다!!")
                                Else
                                    .SetText(.GetColFromID("chk"), e.row, sChkGbn)
                                End If
                            End If
                            '> yjlee 2009-02-12

                        Else
                            .SetText(.GetColFromID("chk"), e.row, sChkGbn)

                        End If

                        iSelGrp = .SearchCol(.GetColFromID("chk"), iRowB - 1, iRowE, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                        If mbCheckMode = False Then
                            If iSelGrp < iRowB Then
                                .SetText(.GetColFromID("chkbc"), iRowB, "")
                            Else
                                .SetText(.GetColFromID("chkbc"), iRowB, "1")
                            End If
                        End If

                    End If

                    sbDisplay_Fkocs_Select(sChkGbn, sFkOcs, e.row)
                End If

                .ReDraw = True
            End With

        Catch ex As Exception
            sbLog_Msg("오류", sFn + " : " + ex.Message)

        Finally
            mbSkip = False
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub spdOrdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdOrdList.ClickEvent

        ImeModeBase = Windows.Forms.ImeMode.Hangul

        RaiseEvent ChangedRow(fnGet_PatList(e.row))

    End Sub

    Private Sub spdOrdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdOrdList.DblClick
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdOrdList

        With spd
            Dim dblColWidth_design As Double = .get_ColWidth(e.col)
            Dim dblColWidth_text As Double = .get_MaxTextColWidth(e.col)

            Dim sMsg As String = ""

            If dblColWidth_text > dblColWidth_design Then
                sMsg += "이 필드의 전체 내용 ->" + vbCrLf + vbCrLf
                sMsg += Ctrl.Get_Code(spd, e.col, e.row, False)

                MsgBox(sMsg, MsgBoxStyle.Information, "필드 전체 내용 보기")
            End If
        End With
    End Sub

    Private Sub spdOrdList_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdOrdList.KeyDownEvent
        If e.shift = PRG_CONST.Key_spd_Ctrl + PRG_CONST.Key_spd_Shift And e.keyCode = Keys.F1 Then
            COMMON.CommFN.Ctrl.Excel_Column_Info(Me, Me.spdOrdList)
        End If
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub spdOrdList_RightClick(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdOrdList.RightClick
        Dim sFn As String = "spdOrdList_RightClick"

        Dim sChkGbn As String = ""

        Try
            With spdOrdList
                For iRow As Integer = spdOrdList.SelBlockRow To spdOrdList.SelBlockRow2
                    sChkGbn = Ctrl.Get_Code(spdOrdList, "chkbc", iRow, False)

                    If sChkGbn = "" Then
                        sChkGbn = "1"
                    Else
                        sChkGbn = "0"
                    End If

                    .SetText(.GetColFromID("chkbc"), iRow, "1")
                Next
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSel.Click

        With spdOrdList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chkbc")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    mbCheckMode = True
                    .Text = IIf(chkSel.Checked, "1", "").ToString
                    mbCheckMode = False
                End If
            Next
        End With
    End Sub

    Private Sub mnuTestInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuTestInfo.Click

        Dim sTestCd As String = ""
        Dim sTestGbn As String = "T"

        If USER_INFO.N_IOGBN <> "" Then sTestGbn = "O"

        With spdOrdList
            .Row = .ActiveRow
            If sTestGbn = "T" Then
                .Col = .GetColFromID("testcd") : sTestCd = .Text
            Else
                .Col = .GetColFromID("ordcd") : sTestCd = .Text
            End If
        End With

        'Dim frm As Windows.Forms.Form = New CDHELP.FGCDHELP_TEST(sTestGbn, sTestCd)

        Dim frm As Windows.Forms.Form = New CDHELP.FGCDHELP_TEST_NEW(sTestGbn, sTestCd)

        moForm.AddOwnedForm(frm)
        frm.WindowState = FormWindowState.Normal
        frm.Activate()
        frm.Show()

    End Sub

End Class

