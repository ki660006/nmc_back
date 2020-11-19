Imports COMMON.CommFN
Imports common.commlogin.login

Public Class FGO91
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGO91.vb, Class : FGO91" + vbTab

    '< add freety 2007/07/27 : Active Size 조정
    Private Const mcDevFrmBaseWidth As Integer = 1024
    Private Const mcDevFrmBaseHeight As Integer = 768
    Private Const mcDevFrmMinWidth As Integer = 112
    Private Const mcDevMainPanelHeight As Integer = 40

    Private m_dt_CdList As DataTable
    Private m_dr_CdList As DataRow()
    Private m_fpopup_f As FPOPUPFT

    Private Const mcFDO01 As String = "01"
    Private Const mcFDO02 As String = "02"

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
    Friend WithEvents btnFilter As System.Windows.Forms.Button
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFieldVal As System.Windows.Forms.TextBox
    Friend WithEvents btnChgUseDt As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton

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
        Me.lblFilter.Text = rsCont
        Me.lblFilter.AccessibleName = rsSyntax
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
                        al_columns.Add(strTitle.PadRight(100) + "[" + strField + "]")
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
                If rbnSOpt1.Checked Then
                    With Me.spdCdList
                        If .GetColFromID("USDT") + .GetColFromID("UEDT") > 0 Then
                            .Col = 1 : .Row = riCurRow

                            'if 사용종료 then 사용종료일시 변경 else 사용시작일시 변경
                            If .BackColor = Drawing.Color.FromArgb(255, 220, 220) Then
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
                Case mcFDO01
                    sbDisplayCdList_UCost(dt)
                Case mcFDO02
                    sbDisplayCdList_Cust(dt)

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
                Case mcFDO01
                    sbDisplayCdCurRow_UCost(iCurRow)
                Case mcFDO02
                    sbDisplayCdCurRow_Cust(iCurRow)
            End Select

            sbDisplayChgUseDt(iCurRow)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub
#End Region

#Region " sbDisplayCdCurRow_% 일반검사, 공통 "

    Private Sub sbDisplayCdCurRow_UCost(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_UCost(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rbnWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("USDTD") : .Row = iCurRow : Dim sUSDTd As String = "" : sUSDTd = .Text
                .Col = .GetColFromID("USDT") : .Row = iCurRow : Dim sUSDT As String = "" : If .Text <> "" Then sUSDT = Format(CType(.Text, Date), "yyyyMMddHHmmss")
                .Col = .GetColFromID("UEDT") : .Row = iCurRow : Dim sUEDT As String = "" : If .Text <> "" Then sUEDT = Format(CType(.Text, Date), "yyyyMMddHHmmss")

                If sUSDT = "" Then sUSDT = Format(Now, "yyyyMMdd") + "000000"
                CType(mfrmCur, FDO01).sbDisplayCdDetail(sUSDTd, sUSDT, sUEDT)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rbnWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdCurRow_Cust(ByVal iCurRow As Integer)
        Dim sFn As String = "Private Sub sbDisplayCdCurRow_Cust(ByVal iCurRow As Integer)"

        Try
            '신규의 경우
            If rbnWorkOpt2.Checked Then
                sbUSDT_New()
                Exit Sub
            End If

            With spdCdList
                .Col = .GetColFromID("CUSTCD") : .Row = iCurRow : Dim sCd As String = .Text
                .Col = .GetColFromID("USDT") : .Row = iCurRow : Dim sUSDT As String = "" : If .Text <> "" Then sUSDT = Format(CType(.Text, Date), "yyyyMMddHHmmss")
                .Col = .GetColFromID("UEDT") : .Row = iCurRow : Dim sUEDT As String = "" : If .Text <> "" Then sUEDT = Format(CType(.Text, Date), "yyyyMMddHHmmss")

                If sUSDT = "" Then sUSDT = Format(Now, "yyyyMMdd") + "000000"
                CType(mfrmCur, FDO02).sbDisplayCdDetail(sCd, sUSDT, sUEDT)
                miCurRow = iCurRow
            End With

            '조회 또는 수정의 경우
            If rbnWorkOpt0.Checked Or rbnWorkOpt1.Checked Then
                sbUSDT_Disable()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region " sbDisplayCdList 선언"
    Private Sub sbDisplayCdList(ByVal asBuf As String)
        Dim sFn As String = "Private Sub sbDisplayCdList(ByVal asBuf As String)"

        Try
            '전체자료 조회 시에는 신규, 수정할 수 없도록 Disable
            If rbnSOpt1.Checked Then
                rbnWorkOpt1.Enabled = False : rbnWorkOpt2.Enabled = False
            Else
                rbnWorkOpt1.Enabled = True : rbnWorkOpt2.Enabled = True
            End If

            Select Case asBuf
                Case mcFDO01
                    sbDisplayCdList_UCost()
                    If spdCdList.MaxRows > 0 Then sbDisplayCdCurRow(1)
                Case mcFDO02
                    sbDisplayCdList_Cust()
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

#Region " sbisplayCdList_% 수탁검사 "

    Private Sub sbDisplayCdList_UCost(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_UCost()"

        Try
            Dim DTable As DataTable
            Dim objDAF As LISAPP.LISAPP_O_UCOST
            Dim iCol As Integer = 0

            objDAF = New LISAPP.LISAPP_O_UCOST

            If r_dt Is Nothing Then
                DTable = objDAF.GetUCostInfo(CType(IIf(rbnSOpt0.Checked, 0, 1), Integer))
                m_dt_CdList = DTable
                m_dr_CdList = DTable.Select()
            Else
                DTable = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If DTable.Rows.Count > 0 Then
                With spdCdList
                    .ReDraw = False

                    .MaxRows = DTable.Rows.Count

                    For i As Integer = 0 To DTable.Rows.Count - 1
                        For j As Integer = 0 To DTable.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(DTable.Columns(j).ColumnName)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = DTable.Rows(i).Item(j).ToString
                            End If
                        Next

                        If rbnSOpt1.Checked Then
                            If CType(DTable.Rows(i).Item("DIFFDAY"), Double) < 0 Then
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
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdList_Cust(Optional ByVal r_dt As DataTable = Nothing)
        Dim sFn As String = "Private Sub sbDisplayCdList_Cust()"

        Try
            Dim DTable As DataTable
            Dim objDAF As LISAPP.LISAP_O_CUST
            Dim iCol As Integer = 0

            objDAF = New LISAPP.LISAP_O_CUST

            If r_dt Is Nothing Then
                DTable = objDAF.GetCustInfo(CType(IIf(rbnSOpt0.Checked, 0, 1), Integer))
                m_dt_CdList = DTable
                m_dr_CdList = DTable.Select()
            Else
                DTable = r_dt
            End If

            sbInitialize_spdCdList(msMstGbn)

            If DTable.Rows.Count > 0 Then
                With spdCdList
                    .ReDraw = False

                    .MaxRows = DTable.Rows.Count

                    For i As Integer = 0 To DTable.Rows.Count - 1
                        For j As Integer = 0 To DTable.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(DTable.Columns(j).ColumnName)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = DTable.Rows(i).Item(j).ToString
                            End If
                        Next

                        If rbnSOpt1.Checked Then
                            If CType(DTable.Rows(i).Item("DIFFDAY"), Double) < 0 Then
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
            End If

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
                Case mcFDO01
                    CType(mfrmCur, FDO01).giClearKey = 1
                    CType(mfrmCur, FDO01).sbInitialize()
                    CType(mfrmCur, FDO01).giClearKey = 0
                Case mcFDO02
                    CType(mfrmCur, FDO02).giClearKey = 1
                    CType(mfrmCur, FDO02).sbInitialize()
                    CType(mfrmCur, FDO02).giClearKey = 0

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

    Private Sub sbFindList(ByVal rsBuf As String)
        Dim sFn As String = "Sub sbFindList"

        Try
            If Me.lblFieldNm.Tag Is Nothing Then Return
            If IsNumeric(Me.lblFieldNm.Tag) = False Then Return

            Dim iCol As Integer = Convert.ToInt16(Me.lblFieldNm.Tag)

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
                    Case mcFDO01
                        sbSetColumnInfo_UCost()
                    Case mcFDO02
                        sbSetColumnInfo_Cust()

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
            Case mcFDO01        '검사항목별 단가
                sbReg_UCost()
            Case mcFDO02        '거래처
                sbReg_Cust()

        End Select
    End Sub
#End Region

#Region " sbReg_% 수탁검사 "

    Private Sub sbReg_UCost()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDO01).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "시작일시 : " + CType(mfrmCur, FDO01).txtUSDay.Text + "의 검사항목별 단가" + vbCrLf + vbCrLf

            If rbnWorkOpt1.Checked Then
                sMsg &= "을(를) 수정하시겠습니까?"
            ElseIf rbnWorkOpt2.Checked Then
                sMsg &= "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDO01).fnReg() Then
                    If rbnWorkOpt1.Checked Then
                        MsgBox("해당 검사항목별 단가가 수정되었습니다!!", MsgBoxStyle.Information)
                    ElseIf rbnWorkOpt2.Checked Then
                        MsgBox("해당 검사항목별 단가가 등록되었습니다!!", MsgBoxStyle.Information)
                    End If

                    sbUpdateCdList_UCost()
                Else
                    If rbnWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Critical)
                    ElseIf rbnWorkOpt2.Checked Then
                        MsgBox("등록에 실패하였습니다!!", MsgBoxStyle.Critical)
                    End If
                End If
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbReg_Cust()
        Dim sFn As String = "sbReg_"

        Try
            If CType(mfrmCur, FDO02).fnValidate() = False Then Exit Sub

            Dim sMsg As String = "거래처코드 : " & CType(mfrmCur, FDO02).txtCustCd.Text & ", "
            sMsg &= "거래처명   : " & CType(mfrmCur, FDO02).txtCustNm.Text & vbCrLf & vbCrLf

            If rbnWorkOpt1.Checked Then
                sMsg &= "을(를) 수정하시겠습니까?"
            ElseIf rbnWorkOpt2.Checked Then
                sMsg &= "을(를) 등록하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                If CType(mfrmCur, FDO02).fnReg() Then
                    If rbnWorkOpt1.Checked Then
                        MsgBox("해당 거래처정보가 수정되었습니다!!", MsgBoxStyle.Information)
                    ElseIf rbnWorkOpt2.Checked Then
                        MsgBox("해당 거래처정보가 등록되었습니다!!", MsgBoxStyle.Information)
                    End If

                    sbUpdateCdList_Cust()
                Else
                    If rbnWorkOpt1.Checked Then
                        MsgBox("수정에 실패하였습니다!!", MsgBoxStyle.Critical)
                    ElseIf rbnWorkOpt2.Checked Then
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

#Region " sbReloadRightArea 선언 "
    Private Sub sbReloadRightArea(ByVal asBuf As String)
        Dim sFn As String = "Private Sub sbReloadRightArea(ByVal asBuf As String)"

        Try
            Select Case asBuf
                Case mcFDO01            '검사항목별 단가
                    mfrmCur = New FDO01
                Case mcFDO02            '거래처
                    mfrmCur = New FDO02

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
            split1.MinSize = miFirstWidth_pnlLeft + miWidth
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

#Region " sbSetColumnInfo_% 수탁검사 "

    Private Sub sbSetColumnInfo_UCost()
        Dim sFn As String = "Private Sub sbSetColumnInfo_UCost()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 3
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "시작일시" : .ColID = "USDTD" : .set_ColWidth(.GetColFromID("USDTD"), 16)
                .Col = 2 : .Text = "USDT" : .ColID = "USDT" : .ColHidden = True
                .Col = 3 : .Text = "UEDT" : .ColID = "UEDT" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbSetColumnInfo_Cust()
        Dim sFn As String = "Private Sub sbSetColumnInfo_Cust()"

        Try
            With spdCdList
                .ReDraw = False
                .MaxCols = 0
                .MaxCols = 4
                .MaxRows = 1000

                .Col = 1 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False

                .Row = 0

                .Col = 1 : .Text = "거래처코드" : .ColID = "CUSTCD" : .set_ColWidth(.GetColFromID("CUSTCD"), 10)
                .Col = 2 : .Text = "거래처명" : .ColID = "CUSTNM" : .set_ColWidth(.GetColFromID("CUSTNM"), 50)
                .Col = 3 : .Text = "USDT" : .ColID = "USDT" : .ColHidden = True
                .Col = 4 : .Text = "UEDT" : .ColID = "UEDT" : .ColHidden = True

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region " sbUpdateCdList_% 수탁검사 "

    Private Sub sbUpdateCdList_UCost()
        'Dim sFn As String = "Private Sub sbUpdateCdList_UCost()"

        'If Not rbnWorkOpt1.Checked Then Exit Sub

        'Try
        '    With spdCdList
        '        .Row = miCurRow

        '        .Col = .GetColFromID("TCLSCD")
        '        .Text = CType(mfrmCur, FDO01).txtSpcNmD.Text

        '        .Col = .GetColFromID("SPCCD")
        '        .Text = CType(mfrmCur, FDO01).txtIFCd.Text

        '        .Col = .GetColFromID("UCOST")
        '        .Text = CType(mfrmCur, FDO01).txtWNCd.Text

        '        .Col = .GetColFromID("REQCMT")
        '        .Text = IIf(CType(mfrmCur, FDO01).chkReqCmt.Checked, "Y", "").ToString
        '    End With
        'Catch ex As Exception
        '    Fn.log(msFile + sFn, Err)
        '    MsgBox(msFile + sFn + vbCrLf + ex.Message)
        'End Try
    End Sub

    Private Sub sbUpdateCdList_Cust()
        Dim sFn As String = "Private Sub sbUpdateCdList_Cust()"

        If Not rbnWorkOpt1.Checked Then Exit Sub

        Try
            With spdCdList
                .Row = miCurRow

                .Col = .GetColFromID("CUSTCD")
                .Text = CType(mfrmCur, FDO02).txtCustCd.Text

                .Col = .GetColFromID("CUSTNM")
                .Text = CType(mfrmCur, FDO02).txtCustNm.Text

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
                Case mcFDO01
                    sbUSDT_Disable_UCost()
                Case mcFDO02
                    sbUSDT_Disable_Cust()

            End Select
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbUSDT_Disable_% 수탁검사 "

    Private Sub sbUSDT_Disable_UCost()
        With CType(mfrmCur, FDO01)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False

            If rbnWorkOpt1.Checked Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If
        End With
    End Sub

    Private Sub sbUSDT_Disable_Cust()
        With CType(mfrmCur, FDO02)
            .txtUSDay.ReadOnly = True : .txtUSDay.BackColor = Drawing.Color.White : .dtpUSDay.Enabled = False : .dtpUSTime.Enabled = False
            .txtCustCd.ReadOnly = True : .txtCustCd.BackColor = Drawing.Color.White

            If rbnWorkOpt1.Checked Then
                .btnUE.Visible = True
            Else
                .btnUE.Visible = False
            End If
        End With
    End Sub

#End Region

#Region " sbUSDT_New 선언"
    Private Sub sbUSDT_New()
        Dim sFn As String = ""

        If IsNothing(mfrmCur) Then Exit Sub

        Try
            Select Case msMstGbn
                Case mcFDO01
                    sbUSDT_New_UCost()
                Case mcFDO02
                    sbUSDT_New_Cust()
            End Select
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
#End Region

#Region " sbUSDT_New_% 수탁검사 "

    Private Sub sbUSDT_New_UCost()
        With CType(mfrmCur, FDO01)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .btnUE.Visible = False

            .sbSetNewUSDT()
        End With
    End Sub

    Private Sub sbUSDT_New_Cust()
        With CType(mfrmCur, FDO02)
            If .dtpUSDay.Enabled Then Exit Sub

            .txtUSDay.ReadOnly = False : .dtpUSDay.Enabled = True : .dtpUSTime.Enabled = True
            .txtCustCd.ReadOnly = False
            .btnUE.Visible = False

            .sbSetNewUSDT()
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
    Friend WithEvents split1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rbnWorkOpt0 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbnWorkOpt2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbnWorkOpt1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSOpt1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSOpt0 As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblFieldNm As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGO91))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnChgUseDt = New CButtonLib.CButton
        Me.btnReg = New CButtonLib.CButton
        Me.btnExcel = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.txtFieldVal = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblFieldNm = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rbnWorkOpt2 = New System.Windows.Forms.RadioButton
        Me.rbnWorkOpt0 = New System.Windows.Forms.RadioButton
        Me.rbnWorkOpt1 = New System.Windows.Forms.RadioButton
        Me.btnQuery = New System.Windows.Forms.Button
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.lblFilter = New System.Windows.Forms.Label
        Me.btnFilter = New System.Windows.Forms.Button
        Me.rbnSOpt1 = New System.Windows.Forms.RadioButton
        Me.rbnSOpt0 = New System.Windows.Forms.RadioButton
        Me.lblCdList = New System.Windows.Forms.Label
        Me.spdCdList = New AxFPSpreadADO.AxfpSpread
        Me.lblMstList = New System.Windows.Forms.Label
        Me.lstMstList = New System.Windows.Forms.ListBox
        Me.split1 = New System.Windows.Forms.Splitter
        Me.pnlRight = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnBack = New System.Windows.Forms.Button
        Me.pnlBottom.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnChgUseDt)
        Me.pnlBottom.Controls.Add(Me.btnReg)
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.txtFieldVal)
        Me.pnlBottom.Controls.Add(Me.Label3)
        Me.pnlBottom.Controls.Add(Me.lblFieldNm)
        Me.pnlBottom.Controls.Add(Me.Label1)
        Me.pnlBottom.Controls.Add(Me.Panel1)
        Me.pnlBottom.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 628)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1016, 32)
        Me.pnlBottom.TabIndex = 3
        '
        'btnChgUseDt
        '
        Me.btnChgUseDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnChgUseDt.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnChgUseDt.ColorFillBlend = CBlendItems1
        Me.btnChgUseDt.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnChgUseDt.Corners.All = CType(6, Short)
        Me.btnChgUseDt.Corners.LowerLeft = CType(6, Short)
        Me.btnChgUseDt.Corners.LowerRight = CType(6, Short)
        Me.btnChgUseDt.Corners.UpperLeft = CType(6, Short)
        Me.btnChgUseDt.Corners.UpperRight = CType(6, Short)
        Me.btnChgUseDt.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnChgUseDt.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnChgUseDt.FocalPoints.CenterPtX = 0.4672897!
        Me.btnChgUseDt.FocalPoints.CenterPtY = 0.12!
        Me.btnChgUseDt.FocalPoints.FocusPtX = 0.0!
        Me.btnChgUseDt.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnChgUseDt.FocusPtTracker = DesignerRectTracker2
        Me.btnChgUseDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnChgUseDt.ForeColor = System.Drawing.Color.White
        Me.btnChgUseDt.Image = Nothing
        Me.btnChgUseDt.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnChgUseDt.ImageIndex = 0
        Me.btnChgUseDt.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnChgUseDt.Location = New System.Drawing.Point(579, 2)
        Me.btnChgUseDt.Name = "btnChgUseDt"
        Me.btnChgUseDt.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnChgUseDt.SideImage = Nothing
        Me.btnChgUseDt.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnChgUseDt.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnChgUseDt.Size = New System.Drawing.Size(107, 25)
        Me.btnChgUseDt.TabIndex = 201
        Me.btnChgUseDt.Text = "사용일시 수정"
        Me.btnChgUseDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnChgUseDt.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnChgUseDt.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems2
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.4766355!
        Me.btnReg.FocalPoints.CenterPtY = 0.12!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker4
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(579, 3)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(107, 25)
        Me.btnReg.TabIndex = 200
        Me.btnReg.Text = "등록(F2)"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems3
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.4766355!
        Me.btnExcel.FocalPoints.CenterPtY = 0.12!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker6
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(687, 3)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(107, 25)
        Me.btnExcel.TabIndex = 199
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.4672897!
        Me.btnExit.FocalPoints.CenterPtY = 0.4!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(903, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(107, 25)
        Me.btnExit.TabIndex = 198
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems5
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4766355!
        Me.btnClear.FocalPoints.CenterPtY = 0.12!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker10
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(795, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 197
        Me.btnClear.Text = "화면정리 (F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'txtFieldVal
        '
        Me.txtFieldVal.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtFieldVal.Location = New System.Drawing.Point(109, 5)
        Me.txtFieldVal.Name = "txtFieldVal"
        Me.txtFieldVal.Size = New System.Drawing.Size(113, 21)
        Me.txtFieldVal.TabIndex = 20
        Me.txtFieldVal.Text = "코드명"
        Me.txtFieldVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.LavenderBlush
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(4, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 21)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "검색"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFieldNm
        '
        Me.lblFieldNm.BackColor = System.Drawing.SystemColors.Desktop
        Me.lblFieldNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFieldNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFieldNm.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblFieldNm.Location = New System.Drawing.Point(44, 5)
        Me.lblFieldNm.Name = "lblFieldNm"
        Me.lblFieldNm.Size = New System.Drawing.Size(64, 21)
        Me.lblFieldNm.TabIndex = 15
        Me.lblFieldNm.Tag = "0"
        Me.lblFieldNm.Text = "코드"
        Me.lblFieldNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.AliceBlue
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Crimson
        Me.Label1.Location = New System.Drawing.Point(228, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 24)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "관리자 작업 선택  ▶▶▶"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rbnWorkOpt2)
        Me.Panel1.Controls.Add(Me.rbnWorkOpt0)
        Me.Panel1.Controls.Add(Me.rbnWorkOpt1)
        Me.Panel1.Location = New System.Drawing.Point(404, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 24)
        Me.Panel1.TabIndex = 10
        '
        'rbnWorkOpt2
        '
        Me.rbnWorkOpt2.BackColor = System.Drawing.Color.Honeydew
        Me.rbnWorkOpt2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbnWorkOpt2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rbnWorkOpt2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.rbnWorkOpt2.Location = New System.Drawing.Point(136, 2)
        Me.rbnWorkOpt2.Name = "rbnWorkOpt2"
        Me.rbnWorkOpt2.Size = New System.Drawing.Size(56, 21)
        Me.rbnWorkOpt2.TabIndex = 9
        Me.rbnWorkOpt2.Text = " 신규"
        Me.rbnWorkOpt2.UseVisualStyleBackColor = False
        '
        'rbnWorkOpt0
        '
        Me.rbnWorkOpt0.BackColor = System.Drawing.Color.Ivory
        Me.rbnWorkOpt0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbnWorkOpt0.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rbnWorkOpt0.ForeColor = System.Drawing.Color.MidnightBlue
        Me.rbnWorkOpt0.Location = New System.Drawing.Point(8, 2)
        Me.rbnWorkOpt0.Name = "rbnWorkOpt0"
        Me.rbnWorkOpt0.Size = New System.Drawing.Size(56, 21)
        Me.rbnWorkOpt0.TabIndex = 7
        Me.rbnWorkOpt0.Text = " 조회"
        Me.rbnWorkOpt0.UseVisualStyleBackColor = False
        '
        'rbnWorkOpt1
        '
        Me.rbnWorkOpt1.BackColor = System.Drawing.Color.Beige
        Me.rbnWorkOpt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbnWorkOpt1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rbnWorkOpt1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.rbnWorkOpt1.Location = New System.Drawing.Point(72, 2)
        Me.rbnWorkOpt1.Name = "rbnWorkOpt1"
        Me.rbnWorkOpt1.Size = New System.Drawing.Size(56, 21)
        Me.rbnWorkOpt1.TabIndex = 8
        Me.rbnWorkOpt1.Text = " 수정"
        Me.rbnWorkOpt1.UseVisualStyleBackColor = False
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuery.BackColor = System.Drawing.SystemColors.Control
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuery.Location = New System.Drawing.Point(177, 600)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(40, 24)
        Me.btnQuery.TabIndex = 19
        Me.btnQuery.Text = "조회"
        Me.btnQuery.UseVisualStyleBackColor = False
        '
        'pnlLeft
        '
        Me.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlLeft.Controls.Add(Me.btnQuery)
        Me.pnlLeft.Controls.Add(Me.lblFilter)
        Me.pnlLeft.Controls.Add(Me.btnFilter)
        Me.pnlLeft.Controls.Add(Me.rbnSOpt1)
        Me.pnlLeft.Controls.Add(Me.rbnSOpt0)
        Me.pnlLeft.Controls.Add(Me.lblCdList)
        Me.pnlLeft.Controls.Add(Me.spdCdList)
        Me.pnlLeft.Controls.Add(Me.lblMstList)
        Me.pnlLeft.Controls.Add(Me.lstMstList)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(224, 628)
        Me.pnlLeft.TabIndex = 4
        '
        'lblFilter
        '
        Me.lblFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilter.BackColor = System.Drawing.Color.Thistle
        Me.lblFilter.ForeColor = System.Drawing.Color.Brown
        Me.lblFilter.Location = New System.Drawing.Point(69, 600)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(106, 24)
        Me.lblFilter.TabIndex = 66
        Me.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFilter
        '
        Me.btnFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFilter.Location = New System.Drawing.Point(3, 600)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(65, 24)
        Me.btnFilter.TabIndex = 6
        Me.btnFilter.Text = "필터선택"
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'rbnSOpt1
        '
        Me.rbnSOpt1.BackColor = System.Drawing.Color.PaleVioletRed
        Me.rbnSOpt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbnSOpt1.ForeColor = System.Drawing.Color.Black
        Me.rbnSOpt1.Location = New System.Drawing.Point(117, 180)
        Me.rbnSOpt1.Name = "rbnSOpt1"
        Me.rbnSOpt1.Size = New System.Drawing.Size(101, 20)
        Me.rbnSOpt1.TabIndex = 5
        Me.rbnSOpt1.Text = "전체 자료"
        Me.rbnSOpt1.UseVisualStyleBackColor = False
        '
        'rbnSOpt0
        '
        Me.rbnSOpt0.BackColor = System.Drawing.Color.PowderBlue
        Me.rbnSOpt0.Checked = True
        Me.rbnSOpt0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbnSOpt0.Location = New System.Drawing.Point(5, 180)
        Me.rbnSOpt0.Name = "rbnSOpt0"
        Me.rbnSOpt0.Size = New System.Drawing.Size(100, 20)
        Me.rbnSOpt0.TabIndex = 4
        Me.rbnSOpt0.TabStop = True
        Me.rbnSOpt0.Text = "사용가능 자료"
        Me.rbnSOpt0.UseVisualStyleBackColor = False
        '
        'lblCdList
        '
        Me.lblCdList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCdList.BackColor = System.Drawing.SystemColors.Desktop
        Me.lblCdList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
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
        Me.spdCdList.Location = New System.Drawing.Point(4, 206)
        Me.spdCdList.Name = "spdCdList"
        Me.spdCdList.OcxState = CType(resources.GetObject("spdCdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCdList.Size = New System.Drawing.Size(216, 391)
        Me.spdCdList.TabIndex = 3
        Me.spdCdList.TabStop = False
        '
        'lblMstList
        '
        Me.lblMstList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMstList.BackColor = System.Drawing.SystemColors.Desktop
        Me.lblMstList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
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
        Me.lstMstList.Items.AddRange(New Object() {"▷ 1 - [01] 검사항목별 단가", "▷ 2 - [02] 거래처"})
        Me.lstMstList.Location = New System.Drawing.Point(4, 28)
        Me.lstMstList.Name = "lstMstList"
        Me.lstMstList.Size = New System.Drawing.Size(216, 122)
        Me.lstMstList.TabIndex = 1
        Me.lstMstList.TabStop = False
        '
        'split1
        '
        Me.split1.BackColor = System.Drawing.SystemColors.Control
        Me.split1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.split1.Location = New System.Drawing.Point(224, 0)
        Me.split1.MinSize = 224
        Me.split1.Name = "split1"
        Me.split1.Size = New System.Drawing.Size(6, 628)
        Me.split1.TabIndex = 5
        Me.split1.TabStop = False
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.Label2)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(230, 0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(786, 628)
        Me.pnlRight.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Lavender
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(112, 268)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(572, 24)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "[관리자 작업 선택 ▶▶▶]에서 원하는 작업을 선택하고 기초자료 목록을 클릭하십시요!!"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'FGO91
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 660)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.split1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlBottom)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FGO91"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "기초마스터 관리"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRight.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim sFn As String = "Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click"

        Try
            spdCdList.Focus()
            pnlLeft.Width = split1.MinSize
            btnBack.Location = New System.Drawing.Point(split1.Location.X + 1, btnBack.Location.Y)
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

    Public Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = "Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.ButtonClick"

        Try
            sbDisplayClear()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
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

    Public Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
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
            sbReloadRightArea(msMstGbn)
            sbInitialize(msMstGbn)

            sbDisplayInit_Filter()

            System.Windows.Forms.Application.DoEvents()

            rbnWorkOpt0.Checked = True
            sbDisplayCdList(msMstGbn)

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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
    Private Sub FGO91_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Dim sFn As String = "Private Sub FGO91_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated"

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

            Me.CenterToParent()

            If miMDIChild = 0 Then
                miParentGapX = Me.Owner.Width - Me.Owner.ClientSize.Width
                miParentGapY = Me.Owner.Size.Height - Me.Owner.ClientSize.Height + mcDevMainPanelHeight
            Else
                miParentGapX = Me.ParentForm.Width - Me.ParentForm.ClientSize.Width
                miParentGapY = Me.ParentForm.Size.Height - Me.ParentForm.ClientSize.Height + mcDevMainPanelHeight
            End If

            Return

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            mbActivated = True

        End Try
    End Sub

    '< add freety 2007/05/03 : Close 후 메인 활성화
    Private Sub FGO91_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Me.Owner.Activate()
    End Sub

    Private Sub FGO91_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                btnReg_Click(Nothing, Nothing)

            Case Windows.Forms.Keys.F6
                btnClear_Click(Nothing, Nothing)

                '< add freety 2007/05/03 : 검색기능 추가
            Case Windows.Forms.Keys.Delete
                Me.txtFieldVal.Text = ""
                '>
            Case Windows.Forms.Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGO91_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Dim sFn As String = "Private Sub FGO91_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize"

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

    Private Sub FGO91_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Move
        If miMDIChild = 1 Then
            sbRelocation()
        End If

        sbResizeRightArea()
    End Sub

    Private Sub rbnSOpt0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnSOpt0.Click, rbnSOpt1.Click
        If msMstGbn = "" Then Exit Sub

        sbDisplayCdList(msMstGbn)
    End Sub

    Private Sub rbnWorkOpt0_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnWorkOpt0.CheckedChanged, rbnWorkOpt1.CheckedChanged
        sbDisplayClear()

        If rbnWorkOpt0.Checked Then
            btnReg.Enabled = False
            rbnSOpt1.Enabled = True
        Else
            btnReg.Enabled = True

            If rbnWorkOpt1.Checked Then
                btnReg.Text = "수정(F2)"

                rbnSOpt0.Checked = True
                rbnSOpt1.Enabled = False
            Else
                btnReg.Text = "등록(F2)"      '등록시에 sbUSDT_New()를 통해 컨트롤을 Enable시킴
                sbUSDT_New()
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

        '-- 2009/02/09 YEJ 수정 : row가 0인 경우
        ''< add freety 2007/05/03 : 검색기능 추가
        'sbDisplayColumnNm(e.col)
        ''>

        If e.row = 0 Then
            '< add freety 2007/05/03 : 검색기능 추가
            sbDisplayColumnNm(e.col)
            '>
        End If
        '-- 2009/02/09

        If giAddModeKey > 0 Then Exit Sub
        If e.row < 1 Then Exit Sub
        If IsNothing(mfrmCur) Then Exit Sub

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If miLeaveRow = 1 Then Exit Sub

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

    Private Sub split1_SplitterMoving(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles split1.SplitterMoving
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

    Private Sub split1_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles split1.SplitterMoved
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

    Private Sub btnChgUseDt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChgUseDt.Click
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

        sbDisplay_Filter_Query()

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub txtFieldVal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFieldVal.TextChanged
        Try
            If Me.spdCdList.MaxRows < 1 Then Return

            sbFindList(Me.txtFieldVal.Text)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub FGO91_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

    End Sub
End Class