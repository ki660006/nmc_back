Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports COMMON.CommConst

Public Class AxRstInput_m

    Private moForm As Windows.Forms.Form

    Public Event ChangedBcNo(ByVal BcNo As String)
    Public Event ChangedTestCd(ByVal BcNo As String, ByVal TestCd As String)
    Public Event FunctionKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event Call_SpRst(ByVal BcNo As String, ByVal TestCd As String)

    Private mbBac_ClickEvent As Boolean = False

    Private msRegNo As String = ""
    Private msPartSlip As String = ""

    Private msDateS As String = ""
    Private msDateE As String = ""
    Private msPatNm As String = ""
    Private msSexAge As String = ""
    Private msDeptCd As String = ""
    Private msTkDt As String = ""
    Private msWkNo As String = ""

    Private msBcNo As String = ""
    Private msFnDt As String = ""

    Private msTestCds As String = ""
    Private msWkGrpCd As String = ""
    Private msEqCd As String = ""

    Private msRstFlg As String = ""
    Private miMbtTypeFlag As Integer = 0

    Private m_al_Slip_bcno As New ArrayList

    Private m_dt_Cmt_bcno As DataTable
    Private m_dt_Anti_BcNo As DataTable
    Private m_dt_Anti_BcNo_Bak As DataTable
    Private m_dt_Bac_BcNo As DataTable
    Private m_dt_ShareCmt_bcno As DataTable

    Private m_dt_AntiCd As DataTable
    Private m_dt_BacCd As DataTable
    Private m_dt_BacGenCd As DataTable

    Private mbBatchMode As Boolean = False

    Private msObJName As String
    Private mbQueryView As Boolean = False

    Private mbColHiddenYn As Boolean
    Private mbCodeEscKey As Boolean = False

    Public m_dt_RstUsr As DataTable
    Private m_dt_RstCdHelp As DataTable
    Private m_dt_Alert_Rule As DataTable

    Private miDelCnt_Bac As Integer = 0

    Private m_fpopup_a As FPOPUPCD
    Private m_fpopup_b As FPOPUPCD

    Private msDisableMsg As String = ""

    Private mbLostFocusGbn As Boolean = True

    Private Const mc_iSklCd_ChgRst As Integer = 1        '결과 수정기능
    Private Const mc_iSklCd_RptA As Integer = 2          'Alert 보고기능
    Private Const mc_iSklCd_RptP As Integer = 3          'Panic 보고기능
    Private Const mc_iSklCd_RptD As Integer = 4          'Delta 보고기능
    Private Const mc_iSklCd_RptC As Integer = 5          'Critical 보고기능
    Private Const mc_iSklCd_ChgFn As Integer = 6         '최종보고 수정기능

    Private Const mc_iRptCd_ReqSub As Integer = 10       '결과입력 필수 Child Of Sub. 미입력
    Private Const mc_iRptCd_Parent As Integer = 11       'Parent Of Sub. 미발견
    Private Const mc_iRptCd_Mw As Integer = 20           '이미 중간보고
    Private Const mc_iRptCd_Fn As Integer = 30           '이미 최종보고

    Private mbLeveCellGbn As Boolean = True
    Private m_dbl_RowHeightt As Double = 0

    Public Property ColHiddenYn() As Boolean
        Get
            ColHiddenYn = mbColHiddenYn
        End Get
        Set(ByVal value As Boolean)
            mbColHiddenYn = value

            Dim intCol As Integer
            With spdResult
                If mbColHiddenYn Then
                    For intCol = 1 To .MaxCols
                        If intCol = .GetColFromID("bcno") Then
                            .Col = intCol : .ColHidden = True
                        ElseIf intCol = .GetColFromID("chk") Or intCol = .GetColFromID("tnmd") Or intCol = .GetColFromID("orgrst") Or intCol = .GetColFromID("viewrst") Or _
                               intCol = .GetColFromID("history") Or intCol = .GetColFromID("reftxt") Or intCol = .GetColFromID("rstunit") Or _
                               intCol = .GetColFromID("hlmark") Or intCol = .GetColFromID("panicmark") Or intCol = .GetColFromID("deltamark") Or intCol = .GetColFromID("rstflgmark") Or _
                               intCol = .GetColFromID("alertmark") Or _
                               intCol = .GetColFromID("rstcmt") Or intCol = .GetColFromID("bfviewrst2") Or intCol = .GetColFromID("bfbcno2") Or intCol = .GetColFromID("eqnm") Or _
                               intCol = .GetColFromID("testcd") Or intCol = .GetColFromID("spccd") Or intCol = .GetColFromID("tordcd") Or _
                               intCol = .GetColFromID("reftcls") Or intCol = .GetColFromID("eqflag") Or intCol = .GetColFromID("slipcd") Or intCol = .GetColFromID("criticalmark") Or _
                               intCol = .GetColFromID("rrptst") Then
                            '20210419 jhs rrptst 추가
                        Else
                            .Col = intCol : .ColHidden = True
                        End If
                    Next
                Else
                    For intCol = 1 To .MaxCols
                        .Col = intCol : .ColHidden = False
                    Next
                End If
            End With

            With spdBac
                If mbColHiddenYn Then
                    For intCol = 1 To .MaxCols
                        If intCol = .GetColFromID("bacnmd") Or intCol = .GetColFromID("incrst") Or intCol = .GetColFromID("ranking") Or _
                           intCol = .GetColFromID("testmtd") Or intCol = .GetColFromID("baccmt") Then

                        Else
                            .Col = intCol : .ColHidden = True
                        End If

                    Next
                Else
                    For intCol = 1 To .MaxCols
                        .Col = intCol : .ColHidden = False
                    Next
                End If
            End With

            With spdAnti
                If mbColHiddenYn Then
                    For intCol = 1 To .MaxCols
                        If intCol = .GetColFromID("testmtd") Or intCol = .GetColFromID("antinmd") Or intCol = .GetColFromID("antirst") Or _
                           intCol = .GetColFromID("decrst") Or intCol = .GetColFromID("refr") Or intCol = .GetColFromID("refs") Or intCol = .GetColFromID("rptyn") Then

                        Else
                            .Col = intCol : .ColHidden = True
                        End If

                    Next
                Else
                    For intCol = 1 To .MaxCols
                        .Col = intCol : .ColHidden = False
                    Next
                End If
            End With

        End Set
    End Property

    Public WriteOnly Property Form() As Windows.Forms.Form
        Set(ByVal value As Windows.Forms.Form)
            moForm = value
        End Set
    End Property

    Public WriteOnly Property RegNo() As String
        Set(ByVal value As String)
            msRegNo = value
        End Set
    End Property

    Public WriteOnly Property PatName() As String
        Set(ByVal value As String)
            msPatNm = value
        End Set
    End Property

    Public WriteOnly Property SexAge() As String
        Set(ByVal value As String)
            msSexAge = value
        End Set
    End Property

    Public WriteOnly Property DeptCd() As String
        Set(ByVal value As String)
            msDeptCd = value
        End Set
    End Property

    Public WriteOnly Property TkDt() As String
        Set(ByVal value As String)
            msTkDt = value
        End Set
    End Property

    Public WriteOnly Property FnDt() As String
        Set(ByVal value As String)
            msFnDt = value
        End Set
    End Property

    Public WriteOnly Property TestCds() As String
        Set(ByVal value As String)
            msTestCds = value
        End Set
    End Property

    Public WriteOnly Property TgrpCds() As String
        Set(ByVal value As String)
            msTestCds = fnGet_tgrp_testspc(value)
        End Set
    End Property

    Public WriteOnly Property WKgrpCd() As String
        Set(ByVal value As String)
            msWkGrpCd = value
        End Set
    End Property

    Public WriteOnly Property EqCd() As String
        Set(ByVal value As String)
            msEqCd = value
        End Set
    End Property

    Public WriteOnly Property WkNO() As String
        Set(ByVal value As String)
            msWkNo = value
        End Set
    End Property

    Public WriteOnly Property RstFlg() As String
        Set(ByVal value As String)
            msRstFlg = value
        End Set
    End Property

    Public WriteOnly Property BatchMode() As Boolean
        Set(ByVal value As Boolean)
            mbBatchMode = value
        End Set
    End Property

    Public WriteOnly Property QueryMOde() As Boolean
        Set(ByVal value As Boolean)
            mbQueryView = value
        End Set
    End Property

    Public ReadOnly Property BCNO() As String
        Get
            If msBcNo = "" Then
                BCNO = ""
            Else
                BCNO = IIf(lblBcno.Text = "", msBcNo, lblBcno.Text).ToString
            End If
        End Get
    End Property

    Public Property BcNoAll() As Boolean
        Get
            Return Me.chkBcnoAll.Checked
        End Get

        Set(ByVal value As Boolean)
            Me.chkBcnoAll.Checked = value
        End Set
    End Property

    Private Sub sbSet_Cmt_BcNo_Add(ByVal r_ci As CMT_INFO)
        Dim sFn As String = "sbSet_Cmt_BcNo_Add"

        Try
            With m_dt_Cmt_bcno
                'Row 추가
                Dim dr As DataRow = .NewRow()

                Dim a_fieldinfo() As System.Reflection.FieldInfo = r_ci.GetType().GetFields()

                For j As Integer = 1 To a_fieldinfo.Length
                    Dim sFieldName As String = a_fieldinfo(j - 1).Name.ToLower
                    Dim sFieldValue As String = a_fieldinfo(j - 1).GetValue(r_ci).ToString()

                    If Not sFieldValue = "" Then
                        dr.Item(sFieldName) = sFieldValue
                    End If
                Next

                'status
                dr.Item("status") = "I"

                .Rows.Add(dr)
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbSet_Cmt_BcNo_Edit(ByVal r_ci As CMT_INFO)
        Dim sFn As String = "sbSet_Cmt_BcNo_Edit"

        Try
            With m_dt_Cmt_bcno
                Dim iRow As Integer = -1

                For ix As Integer = 0 To .Rows.Count - 1
                    If .Rows(ix).Item("bcno").ToString.Trim = r_ci.BcNo And .Rows(ix).Item("partslip").ToString.Trim = r_ci.PartSlip Then
                        iRow = ix
                        Exit For
                    End If
                Next

                If iRow < 0 Then
                    sbSet_Cmt_BcNo_Add(r_ci)
                Else
                    Dim a_fieldinfo() As System.Reflection.FieldInfo = r_ci.GetType().GetFields()
                    Dim sStatus As String = "S"

                    For ix As Integer = 0 To a_fieldinfo.Length - 1
                        Dim sFieldName As String = a_fieldinfo(ix).Name.ToLower
                        Dim sFieldValue As String = a_fieldinfo(ix).GetValue(r_ci).ToString()

                        '수정된 부분이 있는 지 조사하고 있으면 변경
                        If Not .Rows(iRow).Item(sFieldName).ToString() = sFieldValue Then
                            .Rows(iRow).Item(sFieldName) = sFieldValue
                            sStatus = "U"
                        End If
                    Next

                    'status
                    If .Rows(iRow).Item("status").ToString() = "S" Then
                        .Rows(iRow).Item("status") = sStatus
                    End If

                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Cmt_One_slipcd(ByVal rsSlipCd As String)
        Dim sFn As String = "sbDisplay_Cmt_One_slipcd"

        Try
            Me.txtCmtCont.Text = ""

            Dim alPartSlip As New ArrayList

            Dim a_dr As DataRow()

            If rsSlipCd = "" Then
                a_dr = m_dt_Cmt_bcno.Select("", "partslip")
            Else
                a_dr = m_dt_Cmt_bcno.Select("partslip = '" + rsSlipCd + "'")
            End If

            If a_dr.Length > 0 Then
                For ix As Integer = 0 To a_dr.Length - 1

                    If rsSlipCd = "" Then
                        If ix > 0 Then Me.txtCmtCont.Text += vbCrLf

                        If alPartSlip.Contains(a_dr(ix).Item("partslip").ToString.Trim) Then
                        Else
                            Me.txtCmtCont.Text += "[" + a_dr(ix).Item("slipnmd").ToString.Trim + "]" + vbCrLf
                            alPartSlip.Add(a_dr(ix).Item("partslip").ToString.Trim)
                        End If

                        Me.txtCmtCont.Text += a_dr(ix).Item("cmtcont").ToString

                    Else
                        If ix > 0 Then Me.txtCmtCont.Text += vbCrLf

                        Me.txtCmtCont.Text += a_dr(ix).Item("cmtcont").ToString
                    End If

                Next
            End If

            If rsSlipCd = "" Then
                Me.txtCmtCont.ReadOnly = True
            Else
                Me.txtCmtCont.ReadOnly = False
            End If

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_slip(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_slip"

        Try
            Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_SlipInfo_bcno(rsBcNo)

            cboSlip.Items.Clear()
            cboSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                If m_al_Slip_bcno.Contains(dt.Rows(ix).Item("slipcd").ToString.Trim) Then
                    cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString.Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString.Trim)
                    If dt.Rows(ix).Item("slipcd").ToString.Trim = msPartSlip Then cboSlip.SelectedIndex = cboSlip.Items.Count - 1
                End If
            Next

            If msPartSlip = "" Then cboSlip.SelectedIndex = 0

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Multi_Rst_Micro()

        If msBcNo = "" Then Return

        Dim frmMM As New FPOPUP_HOA

        With frmMM
            .BcNo = msBcNo
            .WkNo = msWkNO
            .RegNo = msRegNo
            .PatNm = msPatNm

            .StartPosition = Windows.Forms.FormStartPosition.CenterParent

            .ShowDialog(Me)
        End With
    End Sub

    Private Sub sbDisplay_BcNo_Multi_Micro(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_BcNo_Multi_Micro"

        Try
            If LISAPP.APP_M.CommFn.fnFind_Micro_Bak(rsBcNo) Then
                Me.picMultiMicro.Image = Me.imgList.Images(0)
            Else
                Me.picMultiMicro.Image = Nothing
            End If

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    '-- 배양항목에서 단축코드 입력시 해당 배양균 스프레드에 자동추가기능.
    Private Sub sbDisplay_Spd_BacCd(ByVal sBacCd As String)
        Dim sFn As String = "sbDisplay_Spd_BacCd"

        Dim bi As New BacInfo

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac
            Dim iRow As Integer = 0

            '현재 균에 대한 균 정보 수정
            Dim a_dr As DataRow() = m_dt_BacCd.Select("baccd = '" & sBacCd & "'")

            If a_dr.Length > 0 Then
                '추가
                spd.MaxRows += 1
                iRow = spd.MaxRows

                bi.OldBacCd = ""
                bi.TestCd = Me.lblTestCd.Text
                bi.BacCd = a_dr(0).Item("baccd").ToString().Trim
                bi.BacNmD = a_dr(0).Item("bacnmd").ToString().Trim
                bi.BacGenCd = a_dr(0).Item("bacgencd").ToString().Trim
                bi.BacSeq = fnFind_Bac_Next_Seq()
                bi.ranking = iRow.ToString
                bi.TestMtd = ""
                bi.IncRst = ""

                sbSet_Bac_BcNo_Add(bi)

                '증식정도
                spd.Col = spd.GetColFromID("incrst")
                spd.Row = iRow
                spd.TypeComboBoxList = fnGet_BacIncCd(Me.lblTestCd.Text, Me.lblspccd.text)

                '화면 표시
                spd.SetText(spd.GetColFromID("baccd"), iRow, bi.BacCd)
                spd.SetText(spd.GetColFromID("bacnmd"), iRow, bi.BacNmD)
                spd.SetText(spd.GetColFromID("bacgencd"), iRow, bi.BacGenCd)
                spd.SetText(spd.GetColFromID("bacseq"), iRow, bi.BacSeq)
                spd.SetText(spd.GetColFromID("ranking"), iRow, bi.ranking)
                spd.SetText(spd.GetColFromID("incrst"), iRow, bi.IncRst)
                spd.SetText(spd.GetColFromID("baccmt"), iRow, "")
                spd.SetText(spd.GetColFromID("testcd"), iRow, Me.lblTestCd.Text)

                spd_Change(spd, New AxFPSpreadADO._DSpreadEvents_ChangeEvent(spd.GetColFromID("bacnmd"), iRow))

                'ActiveCell
                spd.SetActiveCell(spd.GetColFromID("bacnmd"), iRow)
                spd.Focus()

                '항균제 반영하기
                sbDisplay_Return_BacCd_Anti(bi)

                'TestMethod 변경 spdBac에 표시
                sbDisplay_Change_Test_Method()

                '변경여부 배양(동정)검사에 표시
                sbDisplay_Change_Rst_Micro()
            Else
                MsgBox("입력한 균코드에 해당하는 균정보가 존재하지 않습니다!!")
            End If

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        Finally
            bi = Nothing

        End Try
    End Sub

    Private Sub sbSet_Anti_BcNo_Add(ByVal r_ai As AntiInfo)
        Dim sFn As String = "sbSet_Anti_BcNo_Add"

        Try
            With m_dt_Anti_BcNo
                'Row 추가
                Dim dr As DataRow = .NewRow()

                Dim a_fieldinfo() As System.Reflection.FieldInfo = r_ai.GetType().GetFields()

                For j As Integer = 1 To a_fieldinfo.Length
                    Dim sFieldName As String = a_fieldinfo(j - 1).Name.ToLower
                    Dim sFieldValue As String = a_fieldinfo(j - 1).GetValue(r_ai).ToString()

                    If Not sFieldValue = "" Then
                        dr.Item(sFieldName) = sFieldValue
                    End If
                Next

                'status
                dr.Item("status") = "I"

                .Rows.Add(dr)
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbLoad_Popup_AntiCd()
        Dim sFn As String = "sbLoad_Popup_AntiCd"

        Try
            Dim al_columns As New ArrayList

            Dim columninfo As ColumnInfo

            columninfo = New ColumnInfo
            columninfo.ColumnName = "anticd"
            columninfo.ColumnCaption = "코드"
            columninfo.ColumnSize = 6
            al_columns.Add(columninfo)
            columninfo = Nothing

            columninfo = New ColumnInfo
            columninfo.ColumnName = "antinmd"
            columninfo.ColumnCaption = "항균제명"
            columninfo.ColumnSize = 60
            al_columns.Add(columninfo)
            columninfo = Nothing

            columninfo = New ColumnInfo
            columninfo.ColumnName = "bacgencd"
            columninfo.ColumnCaption = "균속"
            columninfo.ColumnSize = 0
            al_columns.Add(columninfo)
            columninfo = Nothing

            columninfo = New ColumnInfo
            columninfo.ColumnName = "testmtd"
            columninfo.ColumnCaption = "방법"
            columninfo.ColumnSize = 0
            al_columns.Add(columninfo)
            columninfo = Nothing

            columninfo = New ColumnInfo
            columninfo.ColumnName = "refr"
            columninfo.ColumnCaption = "R"
            columninfo.ColumnSize = 0
            al_columns.Add(columninfo)
            columninfo = Nothing

            columninfo = New ColumnInfo
            columninfo.ColumnName = "refs"
            columninfo.ColumnCaption = "S"
            columninfo.ColumnSize = 0
            al_columns.Add(columninfo)
            columninfo = Nothing

            If Not m_fpopup_a Is Nothing Then
                m_fpopup_a.Close()
                RemoveHandler m_fpopup_a.ReturnPopupCd, AddressOf sbDisplay_Return_AntiCd
            End If

            m_fpopup_a = New FPOPUPCD

            Dim m_fpopup As FPOPUPCD = m_fpopup_a

            With m_fpopup
                .Title = "항균제 정보"
                .Columns = al_columns
                .MultiRowEnable = True

                .DisplayInit()
            End With

            m_fpopup.TopMost = True
            m_fpopup.Hide()

            AddHandler m_fpopup.ReturnPopupCd, AddressOf sbDisplay_Return_AntiCd

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbLoad_Popup_BacCd()
        Dim sFn As String = "sbLoad_Popup_BacCd"

        Try
            Dim al_columns As New ArrayList

            Dim columninfo As ColumnInfo

            columninfo = New ColumnInfo
            columninfo.ColumnName = "baccd"
            columninfo.ColumnCaption = "균코드"
            columninfo.ColumnSize = 6
            al_columns.Add(columninfo)
            columninfo = Nothing

            columninfo = New ColumnInfo
            columninfo.ColumnName = "bacnmd"
            columninfo.ColumnCaption = "배양/분리 균명"
            columninfo.ColumnSize = 60
            al_columns.Add(columninfo)
            columninfo = Nothing

            columninfo = New ColumnInfo
            columninfo.ColumnName = "bacgencd"
            columninfo.ColumnCaption = "균속"
            columninfo.ColumnSize = 0
            al_columns.Add(columninfo)
            columninfo = Nothing

            If Not m_fpopup_b Is Nothing Then
                m_fpopup_b.Close()
                RemoveHandler m_fpopup_b.ReturnPopupCd, AddressOf sbDisplay_Return_BacCd
            End If

            m_fpopup_b = New FPOPUPCD

            Dim m_fpopup As FPOPUPCD = m_fpopup_b

            With m_fpopup
                .Title = "배양/분리 균 정보"

                .Columns = al_columns

                .DisplayInit()
            End With

            m_fpopup.TopMost = True
            m_fpopup.Hide()

            AddHandler m_fpopup.ReturnPopupCd, AddressOf sbDisplay_Return_BacCd

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Return_AntiCd(ByVal robjSender As Object)
        Dim sFn As String = "sbDisplay_Return_AntiCd"

        Dim ai As New AntiInfo

        Try
            Dim spd_a As AxFPSpreadADO.AxfpSpread = Me.spdAnti
            Dim spd_b As AxFPSpreadADO.AxfpSpread = Me.spdBac
            Dim m_fpopup As FPOPUPCD = m_fpopup_a

            Dim iRow As Integer = 0

            With m_fpopup
                If Not .OutData Is Nothing Then
                    For i As Integer = 1 To .OutData.Rows.Count
                        ai.TestCd = Ctrl.Get_Code(spd_b, "testcd", spd_b.ActiveRow)
                        ai.BacCd = Ctrl.Get_Code(spd_b, "baccd", spd_b.ActiveRow)
                        ai.BacSeq = Ctrl.Get_Code(spd_b, "bacseq", spd_b.ActiveRow)
                        ai.AntiCd = .OutData.Rows(i - 1).Item("anticd").ToString().Trim
                        ai.AntiNmD = .OutData.Rows(i - 1).Item("antinmd").ToString().Trim
                        ai.TestMtd = .OutData.Rows(i - 1).Item("testmtd").ToString().Trim
                        ai.DecRst = ""
                        ai.AntiRst = ""
                        ai.RefR = .OutData.Rows(i - 1).Item("refr").ToString().Trim
                        ai.RefS = .OutData.Rows(i - 1).Item("refs").ToString().Trim
                        ai.RptYn = "1"

                        '추가가능여부 조사
                        iRow = spd_a.SearchCol(spd_a.GetColFromID("anticd"), 0, spd_a.MaxRows, ai.AntiCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                        If iRow > 0 Then
                            If Not Ctrl.Get_Code(spd_a, "testmtd", iRow) = ai.TestMtd Then
                                iRow = 0
                            End If
                        End If

                        If iRow > 0 Then
                            '추가 불가
                            MsgBox(ai.AntiNmD + "은(는) 이미 항균제 내역에 있습니다. 확인하여 주십시요!!")
                        Else
                            '추가 가능
                            spd_a.MaxRows += 1
                            iRow = spd_a.MaxRows

                            '화면 표시
                            spd_a.SetText(spd_a.GetColFromID("baccd"), iRow, ai.BacCd)
                            spd_a.SetText(spd_a.GetColFromID("bacseq"), iRow, ai.BacSeq)
                            spd_a.SetText(spd_a.GetColFromID("anticd"), iRow, ai.AntiCd)
                            spd_a.SetText(spd_a.GetColFromID("antinmd"), iRow, ai.AntiNmD)
                            spd_a.SetText(spd_a.GetColFromID("testmtd"), iRow, ai.TestMtd)
                            spd_a.SetText(spd_a.GetColFromID("decrst"), iRow, ai.DecRst)
                            spd_a.SetText(spd_a.GetColFromID("antirst"), iRow, ai.AntiRst)
                            spd_a.SetText(spd_a.GetColFromID("refr"), iRow, ai.RefR)
                            spd_a.SetText(spd_a.GetColFromID("refs"), iRow, ai.RefS)
                            spd_a.SetText(spd_a.GetColFromID("testcd"), iRow, ai.TestCd)
                            spd_a.SetText(spd_a.GetColFromID("rptyn"), iRow, ai.RptYn)

                            'antiinfo 설정 -> m_dt_Anti_BcNo에 반영
                            sbSet_Anti_BcNo_Add(ai)
                        End If
                    Next

                    'TestMethod 변경 spdBac에 표시
                    sbDisplay_Change_Test_Method()

                    '변경여부 배양(동정)검사에 표시
                    sbDisplay_Change_Rst_Micro()
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        Finally
            ai = Nothing

        End Try
    End Sub

    Private Sub sbSet_Bac_BcNo_Add(ByVal r_bi As BacInfo)
        Dim sFn As String = "sbSet_Bac_BcNo_Add"

        Try
            With m_dt_Bac_BcNo
                'Row 추가
                Dim dr As DataRow = .NewRow()

                Dim a_fieldinfo() As System.Reflection.FieldInfo = r_bi.GetType().GetFields()

                For j As Integer = 1 To a_fieldinfo.Length
                    Dim sFieldName As String = a_fieldinfo(j - 1).Name.ToLower
                    Dim sFieldValue As String = a_fieldinfo(j - 1).GetValue(r_bi).ToString()

                    If Not sFieldValue = "" Then
                        dr.Item(sFieldName) = sFieldValue
                    End If
                Next

                'status
                dr.Item("status") = "I"

                .Rows.Add(dr)
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub


    Private Sub sbDisplay_Return_BacCd(ByVal robjSender As Object)
        Dim sFn As String = "sbDisplay_Return_BacCd"

        Dim bi As New BacInfo

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac
            Dim m_fpopup As FPOPUPCD = m_fpopup_b

            Dim iRow As Integer = 0

            With m_fpopup
                If Not .OutData Is Nothing Then
                    If .OutData.Rows.Count < 1 Then Return

                    If CType(robjSender, CButtonLib.CButton).Name.StartsWith("btnAdd") Then
                        '추가
                        spd.MaxRows += 1
                        iRow = spd.MaxRows

                        bi.OldBacCd = ""
                        bi.TestCd = Me.lblTestCd.Text
                        bi.BacCd = .OutData.Rows(0).Item("baccd").ToString()
                        bi.BacNmD = .OutData.Rows(0).Item("bacnmd").ToString()
                        bi.BacGenCd = .OutData.Rows(0).Item("bacgencd").ToString()
                        bi.BacSeq = fnFind_Bac_Next_Seq()
                        bi.ranking = iRow.ToString
                        bi.TestMtd = ""
                        bi.IncRst = ""
                        bi.bacCmt = ""
                        bi.BcNo = ""

                        bi.OldRanking = ""
                        bi.OldIncRst = ""
                        bi.OldbacCmt = ""
                        bi.OldBacCd = ""

                        'bacinfo 설정 -> m_dt_bac_BcNo에 반영
                        sbSet_Bac_BcNo_Add(bi)

                        '증식정도
                        spd.Col = spd.GetColFromID("incrst")
                        spd.Row = iRow
                        spd.TypeComboBoxList = fnGet_BacIncCd(Me.lblTestCd.Text, Me.lblSpccd.Text)

                    ElseIf CType(robjSender, CButtonLib.CButton).Name.StartsWith("btnChg") Then
                        '수정
                        iRow = spd.ActiveRow

                        bi.OldBacCd = Ctrl.Get_Code(spd, "baccd", iRow)

                        If bi.OldBacCd = .OutData.Rows(0).Item("baccd").ToString() Then
                            MsgBox("동일한 균으로 수정할 수 없습니다. 확인하여 주십시요!!")
                            Return
                        End If

                        ' bi.OldBacCd = bi.OldBacCd
                        bi.TestCd = Me.lblTestCd.Text
                        bi.BacCd = .OutData.Rows(0).Item("baccd").ToString()
                        bi.BacNmD = .OutData.Rows(0).Item("bacnmd").ToString()
                        bi.BacGenCd = .OutData.Rows(0).Item("bacgencd").ToString()
                        bi.BacSeq = Ctrl.Get_Code(spd, "bacseq", iRow)
                        bi.ranking = Ctrl.Get_Code(spd, "ranking", iRow)

                        bi.TestMtd = Ctrl.Get_Code(spd, "testmtd", iRow)
                        '< mod freety 2006/06/12 : 증식정도는 그대로 원함
                        'bi.IncRst = ""
                        bi.IncRst = Ctrl.Get_Code(spd, "incrst", iRow)
                        bi.bacCmt = ""

                        bi.OldBacCd = Ctrl.Get_Code(spd, "oldbaccd", iRow)
                        bi.OldRanking = Ctrl.Get_Code(spd, "oldranking", iRow)
                        bi.OldIncRst = Ctrl.Get_Code(spd, "oldincrst", iRow)
                        bi.OldBacCmt = Ctrl.Get_Code(spd, "oldbaccmt", iRow)
                        '>

                        sbSet_Bac_BcNo_Edit(bi, bi.OldBacCd)
                    End If

                    '화면 표시
                    spd.SetText(spd.GetColFromID("baccd"), iRow, bi.BacCd)
                    spd.SetText(spd.GetColFromID("bacnmd"), iRow, bi.BacNmD)
                    spd.SetText(spd.GetColFromID("bacgencd"), iRow, bi.BacGenCd)
                    spd.SetText(spd.GetColFromID("bacseq"), iRow, bi.BacSeq)
                    spd.SetText(spd.GetColFromID("ranking"), iRow, bi.ranking)
                    spd.SetText(spd.GetColFromID("testmtd"), iRow, bi.TestMtd)
                    spd.SetText(spd.GetColFromID("incrst"), iRow, bi.IncRst)
                    spd.SetText(spd.GetColFromID("baccmt"), iRow, "")
                    spd.SetText(spd.GetColFromID("testcd"), iRow, Me.lblTestCd.Text)
                    spd_Change(spd, New AxFPSpreadADO._DSpreadEvents_ChangeEvent(spd.GetColFromID("bacnmd"), iRow))

                    'ActiveCell
                    spd.SetActiveCell(spd.GetColFromID("bacnmd"), iRow)
                    spd.Focus()

                    '항균제 반영하기
                    sbDisplay_Return_BacCd_Anti(bi)

                    'TestMethod 변경 spdBac에 표시
                    sbDisplay_Change_Test_Method()

                    '변경여부 배양(동정)검사에 표시
                    sbDisplay_Change_Rst_Micro()
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        Finally
            bi = Nothing

        End Try
    End Sub

    Private Sub sbDisplay_Return_BacCd_Anti(ByVal r_bi As BacInfo)
        Dim sFn As String = "sbDisplay_Return_BacCd_Anti"

        Dim ai As New AntiInfo

        Try
            Dim sTestmtd As String = "M"

            If rdoDisk.Checked Then
                sTestmtd = "D"
            ElseIf rdoETest.Checked Then
                sTestmtd = "E"
            End If

            If r_bi.OldBacCd = "" Then
                '추가

                '항균제 자동 표시
                sbDisplayInit_spdAnti()

                If r_bi.BacGenCd = FixedVariable.gsBacGenCd_Nogrowth Then Return
                If r_bi.BacGenCd = FixedVariable.gsBacGenCd_Nogen Then Return

                '균속별, TestMtd로 Filter된 항생제 DataTable
                Dim dt As DataTable = fnGet_AntiCd_By_BacGenCd_TestMtd(r_bi.BacGenCd)

                '화면 표시 + m_dt_Anti_BcNo 구성
                Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAnti

                spd.MaxRows = dt.Rows.Count

                For i As Integer = 1 To dt.Rows.Count
                    spd.SetText(spd.GetColFromID("testcd"), i, r_bi.TestCd)
                    spd.SetText(spd.GetColFromID("baccd"), i, r_bi.BacCd)
                    spd.SetText(spd.GetColFromID("bacseq"), i, r_bi.BacSeq)
                    spd.SetText(spd.GetColFromID("anticd"), i, dt.Rows(i - 1).Item("anticd").ToString().Trim)
                    spd.SetText(spd.GetColFromID("antinmd"), i, dt.Rows(i - 1).Item("antinmd").ToString().Trim)
                    'spd.SetText(spd.GetColFromID("testmtd"), i, dt.Rows(i - 1).Item("testmtd").ToString())
                    spd.SetText(spd.GetColFromID("testmtd"), i, sTestmtd)
                    spd.SetText(spd.GetColFromID("decrst"), i, "")
                    spd.SetText(spd.GetColFromID("antirst"), i, "")
                    spd.SetText(spd.GetColFromID("refr"), i, dt.Rows(i - 1).Item("refr").ToString().Trim)
                    spd.SetText(spd.GetColFromID("refs"), i, dt.Rows(i - 1).Item("refs").ToString().Trim)
                    spd.SetText(spd.GetColFromID("rptyn"), i, dt.Rows(i - 1).Item("rptyn").ToString().Trim)

                    ai.TestCd = r_bi.TestCd
                    ai.BacCd = r_bi.BacCd
                    ai.BacSeq = r_bi.BacSeq
                    ai.AntiCd = dt.Rows(i - 1).Item("anticd").ToString().Trim
                    ai.AntiNmD = dt.Rows(i - 1).Item("antinmd").ToString().Trim

                    'ai.TestMtd =  dt.Rows(i - 1).Item("testmtd").ToString()
                    ai.TestMtd = sTestmtd                                       '-- 김포우리병원인 경우

                    ai.DecRst = ""
                    ai.AntiRst = ""
                    ai.RefR = dt.Rows(i - 1).Item("refr").ToString().Trim
                    ai.RefS = dt.Rows(i - 1).Item("refs").ToString().Trim
                    ai.RptYn = dt.Rows(i - 1).Item("rptyn").ToString().Trim

                    If ai.RptYn = "" Then ai.RptYn = "1"

                    'antiinfo 설정 -> m_dt_Anti_BcNo에 반영
                    sbSet_Anti_BcNo_Add(ai)
                Next
            Else
                '수정

                '균이 변경된 경우에만 해당
                If r_bi.OldBacCd = r_bi.BacCd Then Return

                '현재 균에 대한 균 정보 수정
                Dim a_dr As DataRow() = m_dt_Anti_BcNo.Select("baccd = '" + r_bi.OldBacCd + "' and bacseq = '" + r_bi.BacSeq + "'")

                For i As Integer = 1 To a_dr.Length
                    ai.TestCd = r_bi.TestCd
                    ai.BacCd = r_bi.BacCd
                    ai.BacSeq = r_bi.BacSeq
                    ai.AntiCd = a_dr(i - 1).Item("anticd").ToString().Trim
                    ai.AntiNmD = a_dr(i - 1).Item("antinmd").ToString().Trim
                    'ai.TestMtd =  a_dr(i - 1).Item("testmtd").ToString()
                    ai.TestMtd = sTestmtd                                   '-- 김포우리병원인 경우
                    ai.DecRst = a_dr(i - 1).Item("decrst").ToString().Trim
                    ai.AntiRst = a_dr(i - 1).Item("antirst").ToString().Trim
                    ai.RefR = a_dr(i - 1).Item("refr").ToString().Trim
                    ai.RefS = a_dr(i - 1).Item("refs").ToString().Trim
                    ai.RptYn = a_dr(i - 1).Item("rptyn").ToString().Trim

                    sbSet_Anti_BcNo_Edit(ai, r_bi.OldBacCd)
                Next
            End If

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        Finally
            ai = Nothing

        End Try
    End Sub

    Private Sub sbDisplayInit_btnDebug()
        Dim sFn As String = "sbDisplayInit_btnDebug"

        Try

#If DEBUG Then
            Me.btnDebug_AntiBak.Visible = True
            Me.btnDebug_AntiCur.Visible = True
            Me.btnDebug_Bac.Visible = True
            Me.btnTest.Visible = True
            Me.lblTestCd.Visible = True

#Else
            Me.btnDebug_AntiBak.Visible = False
            Me.btnDebug_AntiCur.Visible = False
            Me.btnDebug_Bac.Visible = False
            Me.btnTest.Visible = False
            Me.lbltestcd.Visible = False
#End If

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbSet_Anti_BcNo_Edit(ByVal r_ai As AntiInfo, ByVal rsOldBacCd As String)
        Dim sFn As String = "sbSet_Anti_BcNo_Edit"

        Try
            With m_dt_Anti_BcNo
                Dim iRowIndex As Integer = -1

                If rsOldBacCd = "" Then
                    For i As Integer = 1 To .Rows.Count
                        If .Rows(i - 1).Item("testcd").ToString().Trim = r_ai.TestCd And .Rows(i - 1).Item("baccd").ToString().Trim = r_ai.BacCd And .Rows(i - 1).Item("bacseq").ToString().Trim = r_ai.BacSeq And _
                                .Rows(i - 1).Item("anticd").ToString().Trim = r_ai.AntiCd And .Rows(i - 1).Item("testmtd").ToString().Trim = r_ai.TestMtd And _
                                     .Rows(i - 1).Item("status").ToString().Trim <> "D" Then
                            iRowIndex = i - 1

                            Exit For
                        End If
                    Next
                Else
                    For i As Integer = 1 To .Rows.Count
                        If .Rows(i - 1).Item("testcd").ToString().Trim = r_ai.TestCd And .Rows(i - 1).Item("baccd").ToString().Trim = rsOldBacCd And .Rows(i - 1).Item("bacseq").ToString().Trim = r_ai.BacSeq And _
                                .Rows(i - 1).Item("anticd").ToString().Trim = r_ai.AntiCd And .Rows(i - 1).Item("testmtd").ToString().Trim = r_ai.TestMtd And _
                                    .Rows(i - 1).Item("status").ToString().Trim <> "D" Then
                            iRowIndex = i - 1

                            Exit For
                        End If
                    Next
                End If

                If iRowIndex = -1 Then Return

                Dim sStatus As String = "S"
                Dim a_fieldinfo() As System.Reflection.FieldInfo = r_ai.GetType().GetFields()

                For i As Integer = 1 To a_fieldinfo.Length
                    Dim sFieldName As String = a_fieldinfo(i - 1).Name.ToLower
                    Dim sFieldValue As String = a_fieldinfo(i - 1).GetValue(r_ai).ToString()

                    '수정된 부분이 있는 지 조사하고 있으면 변경
                    If Not .Rows(iRowIndex).Item(sFieldName).ToString().Trim = sFieldValue Then
                        .Rows(iRowIndex).Item(sFieldName) = sFieldValue
                        sStatus = "U"
                    End If
                Next

                'status
                If Not .Rows(iRowIndex).Item("status").ToString() = "I" Then
                    .Rows(iRowIndex).Item("status") = sStatus
                End If

            End With

        Catch ex As Exception
            'sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Change_Anti(ByVal riCol As Integer, ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Change_Anti"

        Dim ai As New AntiInfo

        Try
            Dim spd_a As AxFPSpreadADO.AxfpSpread = Me.spdAnti
            Dim spd_b As AxFPSpreadADO.AxfpSpread = Me.spdBac

            If spd_b.ActiveRow < 1 Then Return

            With spd_a
                'antirst, decrst change
                If riCol = .GetColFromID("antirst") Or riCol = .GetColFromID("decrst") Or riCol = .GetColFromID("rptyn") Then
                    ai.TestCd = Ctrl.Get_Code(spd_b, "testcd", spd_b.ActiveRow)
                    ai.BacCd = Ctrl.Get_Code(spd_b, "baccd", spd_b.ActiveRow)
                    ai.BacSeq = Ctrl.Get_Code(spd_b, "bacseq", spd_b.ActiveRow)
                    ai.AntiCd = Ctrl.Get_Code(spd_a, "anticd", riRow)
                    ai.AntiNmD = Ctrl.Get_Code(spd_a, "antinmd", riRow)
                    ai.TestMtd = Ctrl.Get_Code(spd_a, "testmtd", riRow)
                    ai.DecRst = Ctrl.Get_Code(spd_a, "decrst", riRow)
                    ai.AntiRst = Ctrl.Get_Code(spd_a, "antirst", riRow)
                    ai.RefR = Ctrl.Get_Code(spd_a, "refr", riRow)
                    ai.RefS = Ctrl.Get_Code(spd_a, "refs", riRow)
                    ai.RptYn = Ctrl.Get_Code(spd_a, "rptyn", riRow)

                    'antirst change의 경우 판정결과 구함 --> 판정결과 R, I, S 만 인정
                    If riCol = .GetColFromID("antirst") Then
                        Dim sDecRst As String = fnFind_DecRst(ai.RefR, ai.RefS, ai.AntiRst, ai.TestMtd)

                        If Not sDecRst = "" Then
                            ai.DecRst = sDecRst

                            '판정결과 표시
                            .SetText(.GetColFromID("decrst"), riRow, ai.DecRst)
                        End If
                    End If

                    If ai.DecRst = "I" Or ai.DecRst = "R" Then
                        .Row = riRow
                        .Col = .GetColFromID("decrst") : .ForeColor = Color.Red
                    Else
                        .Row = riRow
                        .Col = .GetColFromID("decrst") : .ForeColor = Color.Black
                    End If

                    sbSet_Anti_BcNo_Edit(ai, "")
                End If
            End With

            '변경여부 배양(동정)검사에 표시
            sbDisplay_Change_Rst_Micro()

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        Finally
            ai = Nothing

        End Try
    End Sub

    Private Sub sbSet_Bac_BcNo_Edit(ByVal r_bi As BacInfo, ByVal rsOldBacCd As String)
        Dim sFn As String = "sbSet_Bac_BcNo_Edit"

        Try
            With m_dt_Bac_BcNo
                Dim iRowIndex As Integer = -1

                For i As Integer = 1 To .Rows.Count
                    'If .Rows(i - 1).Item("testcd").ToString().Trim = r_bi.TestCd And .Rows(i - 1).Item("baccd").ToString().Trim = r_bi.BacCd And _
                    '   .Rows(i - 1).Item("bacseq").ToString().Trim = r_bi.BacSeq And .Rows(i - 1).Item("status").ToString().Trim <> "D" Then
                    '    iRowIndex = i - 1

                    '    Exit For
                    'End If
                    If .Rows(i - 1).Item("testcd").ToString().Trim = r_bi.TestCd And _
                       .Rows(i - 1).Item("bacseq").ToString().Trim = r_bi.BacSeq And .Rows(i - 1).Item("status").ToString().Trim <> "D" Then
                        iRowIndex = i - 1

                        Exit For
                    End If

                Next

                If iRowIndex = -1 Then Return

                Dim sStatus As String = "S"
                Dim a_fieldinfo() As System.Reflection.FieldInfo = r_bi.GetType().GetFields()

                For i As Integer = 1 To a_fieldinfo.Length
                    Dim sFieldName As String = a_fieldinfo(i - 1).Name.ToLower
                    Dim sFieldValue As String = a_fieldinfo(i - 1).GetValue(r_bi).ToString()

                    If sFieldValue <> "" Then
                        '수정된 부분이 있는 지 조사하고 있으면 변경
                        If Not .Rows(iRowIndex).Item(sFieldName).ToString() = sFieldValue Then
                            .Rows(iRowIndex).Item(sFieldName) = sFieldValue
                            sStatus = "U"
                        End If
                    End If
                Next

                'status
                If Not .Rows(iRowIndex).Item("status").ToString() = "I" Then
                    .Rows(iRowIndex).Item("status") = sStatus
                End If

            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Change_Bac(ByVal riCol As Integer, ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Change_Bac"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac
            Dim bi As New BacInfo

            With spd
                'antirst, decrst change
                If riCol = .GetColFromID("ranking") Or riCol = .GetColFromID("incrst") Or riCol = .GetColFromID("testmtd") Or _
                   riCol = .GetColFromID("baccmt") Or riCol = .GetColFromID("baccd") Or riCol = .GetColFromID("bacnmd") Then
                    bi.BacCd = Ctrl.Get_Code(spd, .GetColFromID("baccd"), riRow)
                    bi.BacSeq = Ctrl.Get_Code(spd, .GetColFromID("bacseq"), riRow)
                    bi.TestMtd = Ctrl.Get_Code(spd, .GetColFromID("testmtd"), riRow)
                    bi.IncRst = Ctrl.Get_Code(spd, .GetColFromID("incrst"), riRow)
                    bi.bacCmt = Ctrl.Get_Code(spd, .GetColFromID("baccmt"), riRow)
                    bi.TestCd = Ctrl.Get_Code(spd, .GetColFromID("testcd"), riRow)
                    bi.BacGenCd = Ctrl.Get_Code(spd, .GetColFromID("bacgencd"), riRow)
                    bi.BcNo = Ctrl.Get_Code(spd, .GetColFromID("bcno"), riRow)
                    bi.ranking = Ctrl.Get_Code(spd, .GetColFromID("ranking"), riRow)

                    bi.OldBacCd = Ctrl.Get_Code(spd, .GetColFromID("oldbaccd"), riRow)
                    bi.OldRanking = Ctrl.Get_Code(spd, .GetColFromID("oldranking"), riRow)
                    bi.OldIncRst = Ctrl.Get_Code(spd, .GetColFromID("oldincrst"), riRow)
                    bi.OldBacCmt = Ctrl.Get_Code(spd, .GetColFromID("oldbaccmt"), riRow)

                    sbSet_Bac_BcNo_Edit(bi, "")
                End If

            End With

            '변경여부 배양(동정)검사에 표시
            sbDisplay_Change_Rst_Micro()

            bi = Nothing

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbSet_Anti_BcNo_Del(ByVal r_ai As AntiInfo)
        Dim sFn As String = "sbSet_Anti_BcNo_Del"

        Try
            With m_dt_Anti_BcNo
                Dim iRowIndex As Integer = -1

                For i As Integer = 1 To .Rows.Count
                    If .Rows(i - 1).Item("testcd").ToString().Trim = r_ai.TestCd And .Rows(i - 1).Item("baccd").ToString().Trim = r_ai.BacCd And _
                       .Rows(i - 1).Item("bacseq").ToString().Trim = r_ai.BacSeq And .Rows(i - 1).Item("anticd").ToString().Trim = r_ai.AntiCd And .Rows(i - 1).Item("testmtd").ToString().Trim = r_ai.TestMtd Then
                        iRowIndex = i - 1

                        Exit For
                    End If
                Next

                If iRowIndex >= 0 Then
                    '신규 추가된 항균제 삭제 시 --> DataRow 자체를 삭제함
                    If .Rows(iRowIndex).Item("bcno").ToString() = "" Then
                        .Rows(iRowIndex).Delete()
                    Else
                        'status
                        .Rows(iRowIndex).Item("status") = "D"
                    End If
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub


    Private Sub sbDisplay_Change_Test_Method()
        Dim sFn As String = "sbDisplay_Change_Test_Method"

        Try
            Dim spd_b As AxFPSpreadADO.AxfpSpread = Me.spdBac
            Dim spd_a As AxFPSpreadADO.AxfpSpread = Me.spdAnti

            If spd_b.ActiveRow < 1 Then Return

            Dim sTestMtd As String = ""

            If spd_a.MaxRows > 0 Then
                sTestMtd = Ctrl.Get_Code(spd_a, "testmtd", spd_a.MaxRows)
            End If


            'spdBac의 TestMtd에 표시
            With spd_b
                .Row = .ActiveRow
                .Col = .GetColFromID("testmtd") : .Text = sTestMtd

                'bacinfo 설정 -> m_dt_bac_BcNo에 반영
                sbDisplay_Change_Bac(.GetColFromID("testmtd"), .ActiveRow)
            End With
        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Function fnFind_Change_Rst_Micro_Bac(ByRef riGrowth As Integer) As Integer
        Dim sFn As String = "fnFind_Change_Rst_Micro_Bac"

        Try
            Dim iChange As Integer = 0

            '삭제 --> 변경
            If miDelCnt_Bac > 0 Then iChange += 1

            '추가 또는 수정 --> 변경
            Dim spd_b As AxFPSpreadADO.AxfpSpread = Me.spdBac

            With spd_b
                For i As Integer = 1 To .MaxRows
                    Dim sBcNo As String = Ctrl.Get_Code(spd_b, .GetColFromID("bcno"), i)
                    Dim sBacGenCd As String = Ctrl.Get_Code(spd_b, .GetColFromID("bacgencd"), i)
                    Dim sBacCd As String = Ctrl.Get_Code(spd_b, .GetColFromID("baccd"), i)
                    Dim sBacSeq As String = Ctrl.Get_Code(spd_b, .GetColFromID("bacseq"), i)
                    Dim sRanking As String = Ctrl.Get_Code(spd_b, .GetColFromID("ranking"), i)
                    Dim sTestMtd As String = Ctrl.Get_Code(spd_b, .GetColFromID("testmtd"), i)
                    Dim sIncRst As String = Ctrl.Get_Code(spd_b, .GetColFromID("incrst"), i)
                    Dim sBacCmt As String = Ctrl.Get_Code(spd_b, .GetColFromID("baccmt"), i)

                    Dim sBacCd_Tag As String = Ctrl.Get_Code_Tag(spd_b, .GetColFromID("oldbaccd"), i)
                    Dim sRanking_Tag As String = Ctrl.Get_Code_Tag(spd_b, .GetColFromID("oldranking"), i)
                    Dim sIncRst_Tag As String = Ctrl.Get_Code_Tag(spd_b, .GetColFromID("oldincrst"), i)
                    Dim sBacCmt_Tag As String = Ctrl.Get_Code_Tag(spd_b, .GetColFromID("oldbaccmt"), i)

                    If sBacGenCd <> FixedVariable.gsBacGenCd_Nogrowth Then riGrowth += 1

                    If sBcNo <> "" And sBacGenCd <> "" And sBacCd <> "" And sBacSeq <> "" Then
                        '수정
                        If sBacCd <> sBacCd_Tag Then iChange += 1
                        If sRanking <> sRanking_Tag Then iChange += 1
                        If sIncRst <> sIncRst_Tag Then iChange += 1
                        If sBacCmt <> sBacCmt_Tag Then iChange += 1
                    Else
                        If sBcNo = "" And sBacGenCd <> "" And sBacCd <> "" And sBacSeq <> "" Then
                            '추가
                            iChange += 1
                        End If
                    End If
                Next
            End With

            Return iChange

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Function

    Private Function fnFind_Change_Rst_Micro_Anti() As Integer
        Dim sFn As String = "fnFind_Change_Rst_Micro_Anti"

        Try
            Dim iChange As Integer = 0

            '초기 상태 S인 Row Count 구하여 체크
            If m_dt_Anti_BcNo.Select("status = 'S'").Length <> m_dt_Anti_BcNo.Rows.Count Then iChange += 1

            Return iChange

        Catch ex As Exception
            sbLog_Exception(ex.Message)
            Return 0
        End Try
    End Function

    Private Sub sbDisplay_Change_Rst_Micro()
        Dim sFn As String = "sbDisplay_Change_Rst_Micro"

        Try
            Dim iGrowth As Integer = 0
            Dim iChgBac As Integer = fnFind_Change_Rst_Micro_Bac(iGrowth)
            Dim iChgAnti As Integer = fnFind_Change_Rst_Micro_Anti()

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

            With spd
                Dim iRow As Integer = 0

                For ix As Integer = 1 To .MaxRows
                    Dim sTCdGbn As String = Ctrl.Get_Code(spd, "tcdgbn", ix)
                    Dim sMbTType As String = Ctrl.Get_Code(spd, "mbttype", ix)
                    Dim sTitleYN As String = Ctrl.Get_Code(spd, "titleyn", ix)
                    Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", ix)

                    If (sTCdGbn = "S" Or sTCdGbn = "P") And sMbTType = "2" And sTestCd = Me.lblTestCd.Text Then
                        If iChgBac + iChgAnti = 0 Then
                            '변경 없음
                            If mbQueryView = False Then .SetText(.GetColFromID("chk"), ix, "")
                        Else
                            '변경 있음
                            .SetText(.GetColFromID("chk"), ix, "1")
                            .SetText(.GetColFromID("orgrst"), ix, IIf(iGrowth = 0, FixedVariable.gsRst_Nogrowth, FixedVariable.gsRst_Growth).ToString)
                            .SetText(.GetColFromID("viewrst"), ix, IIf(iGrowth = 0, FixedVariable.gsRst_Nogrowth, FixedVariable.gsRst_Growth).ToString)

                            sbAlertCheck(ix) '-- Alert 
                        End If

                        iRow = ix

                        Exit For
                    End If
                Next

            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Function fnGet_tgrp_testspc(ByVal rsTgrpCds As String) As String
        Dim sFn As String = "fnGet_tgrp_testinfo"

        Try

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TGrp_Test_List(rsTgrpCds)
            If dt.Rows.Count < 1 Then Return ""

            Dim sTestCds As String = ""

            For ix As Integer = 0 To dt.Rows.Count - 1
                If ix > 0 Then sTestCds += ","
                sTestCds += dt.Rows(ix).Item("testcd").ToString.Replace(" ", "")
            Next

            Return sTestCds

        Catch ex As Exception
            sbLog_Exception(ex.Message)
            Return ""
        End Try

    End Function

    Private Sub sbGet_Alert_Rule()
        Dim sFn As String = "sbGet_Alert_Rule"

        Try

            m_dt_Alert_Rule = LISAPP.APP_R.RstFn.fnGet_Alert_Rule()

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try

    End Sub

    Private Sub sbGet_AntiCd()
        Dim sFn As String = "sbGet_AntiCd"

        Try
            If Me.msTkDt = "" Then Return

            m_dt_AntiCd = LISAPP.APP_M.CommFn.fnGet_AntiCd(Me.msTkDt)

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbGet_BacCd()
        Dim sFn As String = "sbGet_BacCd"

        Try
            If Me.msTkDt = "" Then Return

            m_dt_BacCd = LISAPP.COMM.cdfn.fnGet_Bac_List("", False, msTkDt)

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbGet_BacGenCd()
        Dim sFn As String = "sbGet_BacGenCd"

        Try
            m_dt_BacGenCd = LISAPP.APP_M.CommFn.fnGet_BacGenCd

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub


    Private Function fnGet_BacIncCd(ByVal rsTestCd As String, ByVal rsSpcCd As String) As String
        Dim sFn As String = "fnGet_BacIncCd"

        Try
            Dim dt As DataTable = LISAPP.APP_M.CommFn.fnGet_BacIncCd(rsTestCd, rsSpcCd)
            Dim sBacIncCd_All = ""

            For ix As Integer = 0 To dt.Rows.Count - 1
                If ix = 0 Then sBacIncCd_All += Convert.ToChar(9).ToString()

                sBacIncCd_All += dt.Rows(ix).Item("incrstnm").ToString() + Chr(9)
            Next

            Return sBacIncCd_All

        Catch ex As Exception
            sbLog_Exception(ex.Message)
            Return ""
        End Try
    End Function

    Private Sub sbDisplayInit_spdBac()
        Dim sFn As String = "sbDisplayInit_spdBac"
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac

        Try
            spd.ReDraw = False

            With spd
                .ColsFrozen = .GetColFromID("bacnmd")

                .MaxRows = 0

                .Col = .GetColFromID("bcno") : .ColHidden = True
                .Col = .GetColFromID("testcd") : .ColHidden = False
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplayInit_spdAnti()
        Dim sFn As String = "sbDisplayInit_spdAnti"
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAnti

        Try
            spd.ReDraw = False

            With spd
                .Col = .GetColFromID("testmtd") : .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                .MaxRows = 0

                .Col = .GetColFromID("bcno") : .ColHidden = True
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplay_BcNo_Rst_Bac_One_testcd(ByVal rsTestCd As String)
        Dim sFn As String = "sbDisplay_BcNo_Rst_Bac_One_testcd"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac

            sbDisplayInit_spdBac()
            sbDisplayInit_spdAnti()

            Dim a_dr As DataRow()
            If rsTestCd = "" Then
                a_dr = m_dt_Bac_BcNo.Select("status <> 'D'")
            Else
                a_dr = m_dt_Bac_BcNo.Select("status <> 'D' and testcd = '" + rsTestCd + "'")
            End If

            Ctrl.DisplayAfterSelect(spd, a_dr)

            For intRow As Integer = 1 To spdBac.MaxRows
                With spdBac
                    .Row = intRow
                    '증식정도
                    .Col = spd.GetColFromID("incrst")
                    .Row = intRow
                    .TypeComboBoxList = fnGet_BacIncCd(Me.lblTestCd.Text, Me.lblSpccd.Text)
                End With
            Next
        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_BcNo_Rst_Anti_One_Bac(ByVal rsTestcd As String, ByVal rsBacCd As String, ByVal rsBacSeq As String)
        Dim sFn As String = "sbDisplay_BcNo_Rst_Anti_One_Bac"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAnti

            sbDisplayInit_spdAnti()

            Dim a_dr As DataRow() = m_dt_Anti_BcNo.Select("testcd = '" + rsTestcd + "' and baccd = '" + rsBacCd + "' and bacseq = '" + rsBacSeq + "' and status <> 'D'")

            With spd
                If a_dr Is Nothing Then
                    .MaxRows = 0

                    Return
                End If

                .MaxRows = 0

                .ReDraw = False

                .MaxRows = a_dr.Length

                For i As Integer = 1 To a_dr.Length
                    For j As Integer = 1 To a_dr(i - 1).Table.Columns.Count
                        Dim iCol As Integer = .GetColFromID(a_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i
                            .Text = a_dr(i - 1).Item(j - 1).ToString().Trim
                            .CellTag = a_dr(i - 1).Item(j - 1).ToString().Trim

                            If iCol = .GetColFromID("decrst") Then
                                If a_dr(i - 1).Item(j - 1).ToString().Trim = "R" Or a_dr(i - 1).Item(j - 1).ToString().Trim = "I" Then
                                    .ForeColor = Color.Red
                                Else
                                    .ForeColor = Color.Black
                                End If
                            End If
                        End If
                    Next
                Next
                .ReDraw = True
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        Finally

        End Try
    End Sub


    Private Sub sbDisplay_BcNo_Rst_Bac(ByVal rsBcNo As String, ByVal rsTestCds As String)
        Dim sFn As String = "sbDisplay_BcNo_Rst_Bac"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac

            Dim dt As DataTable
            dt = LISAPP.APP_M.CommFn.fnGet_Rst_Bac(rsBcNo, rsTestCds)

            '균 결과내역 DataTable로 저장하여 I,U,D에 대한 처리함
            m_dt_Bac_BcNo = dt

            Ctrl.DisplayAfterSelect(spd, dt)

            '증식정도 코드 스프레드 콤보박스에 추가
            If fnGet_BacIncCd(Me.lblTestCd.Text, Me.lblSpccd.Text) = "" Then Return

            With spd
                For i As Integer = 1 To spd.MaxRows
                    .Col = .GetColFromID("incrst")
                    .Row = i
                    .TypeComboBoxList = fnGet_BacIncCd(Me.lblTestCd.Text, Me.lblSpccd.Text)
                    
                Next
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_BcNo_Rst_Anti(ByVal rsBcNo As String, ByVal rsTestCds As String)
        Dim sFn As String = "sbDisplay_BcNo_Rst_Anti"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAnti

            'chkAntiAll, 검사방법 초기화
            sbDisplayInit_grpAntiInfo()

            Dim dt As DataTable

            dt = LISAPP.APP_M.CommFn.fnGet_Rst_Anti(rsBcNo, rsTestCds)

            '항균제 결과내역 DataTable로 저장하여 I,U,D에 대한 처리함
            m_dt_Anti_BcNo = dt
            m_dt_Anti_BcNo_Bak = dt.Copy()

            '균 화면의 첫번째 균에 대한 항균제 내역 표시
            With Me.spdBac
                If .MaxRows > 0 Then
                    Dim sTestcd As String = Ctrl.Get_Code(Me.spdBac, "testcd", 1)
                    Dim sBacCd As String = Ctrl.Get_Code(Me.spdBac, "baccd", 1)
                    Dim sBacSeq As String = Ctrl.Get_Code(Me.spdBac, "bacseq", 1)

                    sbDisplay_BcNo_Rst_Anti_One_Bac(sTestcd, sBacCd, sBacSeq)
                Else
                    sbDisplayInit_spdAnti()
                End If
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Function fnFind_Bac_Next_Seq() As String
        Dim sFn As String = "fnFind_Bac_Next_Seq"

        Try
            '현 검체번호 균 Seq + 1 을 구함
            Dim sReturn As String = ""

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac

            With spd
                If .MaxRows < 1 Then sReturn = "1" : Return sReturn

                Dim a_iSeq(.MaxRows - 1) As Integer

                For i As Integer = 1 To .MaxRows
                    Dim sSeq As String = Ctrl.Get_Code(spd, "bacseq", i)

                    If IsNumeric(sSeq) Then
                        a_iSeq(i - 1) = Convert.ToInt32(sSeq)
                    End If
                Next

                Array.Sort(a_iSeq)
                Array.Reverse(a_iSeq)

                sReturn = (a_iSeq(0) + 1).ToString()
            End With

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Function

    Private Function fnFind_BacGenCd() As String
        Dim sFn As String = "fnFind_BacGenCd"

        Try
            '현재 균속을 구함
            Dim sReturn As String = ""

            If Me.spdBac.MaxRows < 1 Then Return sReturn

            sReturn = Ctrl.Get_Code(Me.spdBac, "bacgencd", Me.spdBac.ActiveRow)

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message)

            Return ""
        End Try
    End Function

    Private Function fnFind_DecRst(ByVal sRefR As String, ByVal sRefS As String, ByVal sAntiRst As String, ByVal rsTestMtd As String) As String
        Dim sFn As String = "fnFind_DecRst"

        Try
            Dim sReturn As String = ""

            '수치결과 체크
            If IsNumeric(sAntiRst) = False Then Return sReturn

            If rsTestMtd = "M" Then
                'RefR, RefS 체크
                If IsNumeric(sRefR) Then
                    If Val(sAntiRst) >= Val(sRefR) Then
                        sReturn = "R"
                    End If
                End If

                If IsNumeric(sRefS) Then
                    If Val(sAntiRst) <= Val(sRefS) Then
                        sReturn = "S"
                        'Else
                        '    If IsNumeric(sRefR) = False Then
                        '        sReturn = "NS"
                        '    End If
                    End If
                End If

                If IsNumeric(sRefR) And IsNumeric(sRefS) Then
                    If Val(sAntiRst) < Val(sRefR) And Val(sAntiRst) > Val(sRefS) Then
                        sReturn = "I"
                    End If
                End If
            Else
                'RefR, RefS 체크
                If IsNumeric(sRefR) Then
                    If Val(sAntiRst) <= Val(sRefR) Then
                        sReturn = "R"
                    End If
                End If

                If IsNumeric(sRefS) Then
                    If Val(sAntiRst) >= Val(sRefS) Then
                        sReturn = "S"
                    Else
                        If IsNumeric(sRefR) = False Then
                            sReturn = "NS"
                        End If
                    End If
                End If

                If IsNumeric(sRefR) And IsNumeric(sRefS) Then
                    If Val(sAntiRst) > Val(sRefR) And Val(sAntiRst) < Val(sRefS) Then
                        sReturn = "I"
                    End If
                End If
            End If


            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Function

    Private Function fnGet_BacGenCds() As String

        Dim dt As New DataTable

        Try
            Dim sBacGenCds As String = ""

            dt = LISAPP.APP_M.CommFn.fnGet_AntiBacGenCds()
            For ix As Integer = 0 To dt.Rows.Count - 1
                sBacGenCds += dt.Rows(ix).Item("bacgencd").ToString + ","
            Next

            If sBacGenCds = "" Then
                sBacGenCds = "''"
            Else
                sBacGenCds = sBacGenCds.Substring(0, Len(sBacGenCds) - 1)
                sBacGenCds = "'" + sBacGenCds.Replace(",", "', '") + "'"
            End If

            Return sBacGenCds
        Catch ex As Exception
            Return "''"
        End Try

    End Function

    Private Sub sbDel_Anti(ByVal riRow As Integer)
        Dim sFn As String = "sbDel_Anti"

        Dim ai As New AntiInfo

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAnti

            Dim iRow1 As Integer = 0
            Dim iRow2 As Integer = 0

            If riRow = 0 Then
                iRow1 = 1
                iRow2 = spd.MaxRows
            Else
                iRow1 = riRow
                iRow2 = riRow
            End If

            For i As Integer = iRow2 To iRow1 Step -1
                Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", i)
                Dim sBacCd As String = Ctrl.Get_Code(spd, "baccd", i)
                Dim sBacSeq As String = Ctrl.Get_Code(spd, "bacseq", i)
                Dim sAntiCd As String = Ctrl.Get_Code(spd, "anticd", i)
                Dim sTestMtd As String = Ctrl.Get_Code(spd, "testmtd", i)

                ai.TestCd = sTestCd
                ai.BacCd = sBacCd
                ai.BacSeq = sBacSeq
                ai.AntiCd = sAntiCd
                ai.TestMtd = sTestMtd

                sbSet_Anti_BcNo_Del(ai)

                '화면 삭제
                spd.DeleteRows(i, 1)
                spd.MaxRows -= 1
            Next

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        Finally
            ai = Nothing

        End Try
    End Sub
    Private Sub sbDel_chk_Anti(ByVal riRow As Integer)
        Dim sFn As String = "sbDel_Anti"

        Dim ai As New AntiInfo

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAnti

            Dim iRow1 As Integer = 0
            Dim iRow2 As Integer = 0

            If riRow = 0 Then
                iRow1 = 1
                iRow2 = spd.MaxRows
            Else
                iRow1 = riRow
                iRow2 = riRow
            End If


            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sBacCd As String = Ctrl.Get_Code(spd, "baccd", riRow)
            Dim sBacSeq As String = Ctrl.Get_Code(spd, "bacseq", riRow)
            Dim sAntiCd As String = Ctrl.Get_Code(spd, "anticd", riRow)
            Dim sTestMtd As String = Ctrl.Get_Code(spd, "testmtd", riRow)

            ai.TestCd = sTestCd
            ai.BacCd = sBacCd
            ai.BacSeq = sBacSeq
            ai.AntiCd = sAntiCd
            ai.TestMtd = sTestMtd

            sbSet_Anti_BcNo_Del(ai)

            '화면 삭제
            spd.DeleteRows(riRow, 1)
            spd.MaxRows -= 1
        
        Catch ex As Exception
            sbLog_Exception(ex.Message)
        Finally
            ai = Nothing

        End Try
    End Sub

    Private Sub sbSet_Bac_BcNo_Del(ByVal r_bi As BacInfo)
        Dim sFn As String = "sbSet_Bac_BcNo_Del"

        Try
            With m_dt_Bac_BcNo
                Dim iRowIndex As Integer = -1

                For i As Integer = 1 To .Rows.Count
                    If .Rows(i - 1).Item("testcd").ToString().Trim = r_bi.TestCd And .Rows(i - 1).Item("baccd").ToString().Trim = r_bi.BacCd And .Rows(i - 1).Item("bacseq").ToString().Trim = r_bi.BacSeq Then
                        iRowIndex = i - 1

                        Exit For
                    End If
                Next

                If iRowIndex >= 0 Then
                    '신규 추가된 항균제 삭제 시 --> DataRow 자체를 삭제함
                    If .Rows(iRowIndex).Item("bcno").ToString().Trim = "" Then
                        .Rows(iRowIndex).Delete()
                    Else
                        'status
                        .Rows(iRowIndex).Item("status") = "D"
                    End If
                End If

            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbDel_Bac(ByVal riRow As Integer)
        Dim sFn As String = "sbDel_Bac"
        Dim bi As New BacInfo

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac

            '신규 기존 체크 --> 기존 삭제식에만 miDelCnt 증가
            If Not Ctrl.Get_Code(spd, "bcno", riRow) = "" Then
                miDelCnt_Bac += 1
            End If

            Dim sTestCd As String = Ctrl.Get_Code(spd, "testcd", riRow)
            Dim sBacCd As String = Ctrl.Get_Code(spd, "baccd", riRow)
            Dim sBacSeq As String = Ctrl.Get_Code(spd, "bacseq", riRow)

            bi.TestCd = sTestCd
            bi.BacCd = sBacCd
            bi.BacSeq = sBacSeq

            sbSet_Bac_BcNo_Del(bi)

            '화면 삭제
            spd.DeleteRows(riRow, 1)
            spd.MaxRows -= 1

            If Me.spdAnti.MaxRows > 0 Then
                sbDel_Anti(0)
            End If

            'Ranking 재조정
            With spd
                For i As Integer = riRow To .MaxRows
                    .SetText(.GetColFromID("ranking"), i, i)
                    spd_Change(spd, New AxFPSpreadADO._DSpreadEvents_ChangeEvent(.GetColFromID("ranking"), i))
                Next
            End With

            If spd.MaxRows = 0 Then Return

            '새 riRow에 해당하는 균의 항균제내역 표시
            If spd.MaxRows < riRow Then riRow = spd.MaxRows

            spdBac_LeaveCell(spd, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(1, riRow + 1, 1, riRow, False))

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Function fnGet_AntiCd_By_BacGenCd_TestMtd(ByVal rsBacGenCd As String) As DataTable
        Dim sFn As String = "fnGet_AntiCd_By_BacGenCd_TestMtd"

        Try
            Dim dt As New DataTable

            'Col 정의
            With m_dt_AntiCd
                For j As Integer = 1 To .Columns.Count
                    Dim dc As DataColumn = New DataColumn
                    dc.ColumnName = .Columns(j - 1).ColumnName
                    dc.DataType = .Columns(j - 1).DataType
                    dc.Caption = .Columns(j - 1).Caption

                    dt.Columns.Add(dc)
                Next

                Dim sTestMtd As String = "'M'"

                If Me.rdoDisk.Checked Then
                    sTestMtd = "'D'"
                ElseIf Me.rdoMIC.Checked Then
                    sTestMtd = "'M'"
                ElseIf Me.rdoETest.Checked Then
                    sTestMtd = "'E'"
                End If

                '-- 김포우리병원인 경우는 구분 없이 표시
                '''sTestMtd = "'M', 'D', 'E'"

                Dim a_dr As DataRow() = .Select(IIf(chkAntiAll.Checked, "", "bacgencd = '" + rsBacGenCd + "' and ").ToString + "testmtd IN (" + sTestMtd + ")", "sort2 asc, antinmd asc")
                Dim arlAnti As New ArrayList

                For i As Integer = 1 To a_dr.Length
                    'Row 추가
                    Dim dr As DataRow = dt.NewRow()

                    For j As Integer = 1 To .Columns.Count
                        dr.Item(j - 1) = a_dr(i - 1).Item(j - 1)
                    Next

                    dt.Rows.Add(dr)

                Next
            End With

            Return dt

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Function

    Private Sub sbDisplay_Popup_AntiCd()
        Dim sFn As String = "sbDisplay_Popup_AntiCd"

        Try
            Dim m_fpopup As FPOPUPCD = m_fpopup_a

            Dim sTkDt As String = msTkDt

            If sTkDt = "" Then Return

            Dim sBGenCd As String = fnFind_BacGenCd()
            Dim sFilter As String = ""

            If sBGenCd = "" Then Return

            If chkAntiAll.Checked Then
                sFilter += "bacgencd in (" + fnGet_BacGenCds() + ")"
            Else
                sFilter += "bacgencd = '" + sBGenCd + "'"
            End If

            Dim a_dr As DataRow() = m_dt_BacGenCd.Select(sFilter)

            If a_dr.Length < 1 Then
                m_fpopup.Hide()
                Return
            End If

            If m_dt_AntiCd.Rows.Count < 1 Then
                m_fpopup.Hide()
                Return
            End If

            '균속별, TestMtd별 항균제 DataTable 생성
            Dim dt_AntiCd As DataTable = fnGet_AntiCd_By_BacGenCd_TestMtd(sBGenCd)

            If dt_AntiCd.Rows.Count < 1 Then
                m_fpopup.Hide()
                Return
            End If

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

            'Height --> spdOrdList와 같도록 설정
            Dim iHeight As Integer = spdAnti.Height 'spd.Height 

            'Width --> spdOrdList와 같도록 설정
            Dim iWidth As Integer = spd.Width

            'Top --> btnAddA의 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnAddA) + Me.btnAddA.Height + Ctrl.menuHeight

            'Left --> btnAddA의 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnAddA) - iWidth

            With m_fpopup
                .TopPoint = iTop
                .LeftPoint = iLeft
                .HeightPoint = iHeight
                .WidthPoint = iWidth
                .FilterTitle = "균속"
                .HideSortIndicator = True
                .DisplayData(a_dr, dt_AntiCd, moForm)
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Popup_BacCd(ByVal robjSender As Object)
        Dim sFn As String = "sbDisplay_Popup_BacCd"

        Try
            Dim m_fpopup As FPOPUPCD = m_fpopup_b

            Dim sTkDt As String = msTkDt

            If sTkDt = "" Then Return

            Dim sFilter As String = fnFind_BacGenCd()

            If CType(robjSender, CButtonLib.CButton).Name.ToLower.StartsWith("btnchg") Then
                If sFilter = "" Then Return

                sFilter = "bacgencd = '" + sFilter + "'"
            Else
                sFilter = ""
            End If

            Dim a_dr As DataRow() = m_dt_BacGenCd.Select(sFilter, "dispseq, bacgencd")

            If a_dr.Length < 1 Then
                m_fpopup.Hide()
                Return
            End If

            If m_dt_BacCd.Rows.Count < 1 Then
                m_fpopup.Hide()
                Return
            End If

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

            'Height --> spdOrdList와 같도록 설정
            Dim iHeight As Integer = spd.Height + spdBac.Height

            'Width --> spdOrdList와 같도록 설정
            Dim iWidth As Integer = spd.Width

            'Top --> btnAddB의 위쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnAddB) + Ctrl.menuHeight
            iTop = iTop - iHeight

            'Left --> spdBac의 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.spdBac)

            With m_fpopup
                .TopPoint = iTop
                .LeftPoint = iLeft
                .HeightPoint = iHeight
                .WidthPoint = iWidth
                .FilterTitle = "균속"
                .objSender = robjSender
                .DisplayData(a_dr, m_dt_BacCd, moForm)
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayInit_grpAntiInfo()
        Dim sFn As String = "sbDisplayInit_grpAntiInfo"

        Try
            Me.chkAntiAll.Checked = False
            Me.rdoMIC.Checked = True

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_KeyPad(ByVal rsFormGbn As String, ByVal rsTestCd As String, ByVal rsSpcCd As String)
        Dim sFn As String = "Sub sbDisplay_KeyPad(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdOrdListR.RightClick"
        Try
            If rsFormGbn = "" Then Return

            Dim sWBCRst As String = ""
            Dim sBfViewRsts As String = ""
            Dim sBcNo As String = ""
            Dim sPartSlip As String = ""
            Dim sTnmd_p As String = ""

            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntCtlXY As Point = Fn.CtrlLocationXY(spdResult)
            Dim al_RstInfo As New ArrayList

            With spdResult
                Dim sWbcTestCd = LISAPP.COMM.RstFn.fnGet_ManualDiff_WBC_TestCd(rsTestCd, rsSpcCd)

                If sWbcTestCd <> "" Then
                    sWBCRst = LISAPP.COMM.RstFn.fnGet_ManualDiff_WBC_Rst(msBcNo, sWbcTestCd)

                    If sWBCRst = "" Then
                        For iRow As Integer = 1 To .MaxRows
                            If Ctrl.Get_Code(Me.spdResult, "testcd", iRow) = sWbcTestCd Then
                                .Row = iRow
                                .Col = .GetColFromID("orgrst") : sWBCRst = .Text
                                Exit For
                            End If
                        Next
                    End If
                End If

                For iRow As Integer = 1 To .MaxRows
                    Dim sTmp As String = Ctrl.Get_Code(Me.spdResult, "testcd", iRow)
                    If sTmp <> "" Then sTmp = sTmp.Substring(0, 5)

                    If sTmp = rsTestCd Then

                        Dim sTestCd As String = "", sBfView As String = "", sBfFnDt2 As String = ""

                        .Row = iRow
                        .Col = .GetColFromID("testcd") : sTestCd = .Text
                        .Col = .GetColFromID("bfviewrst2") : sBfView = .Text
                        .Col = .GetColFromID("bfbcno1") : sBfFnDt2 = .Text

                        If sBfView <> "" Then
                            sBfViewRsts += sTestCd + "^" + sBfView + "^" + sBfFnDt2 + "|"
                        End If
                    End If
                Next

                Dim sDiffCmt As String = ""

                Select Case rsFormGbn
                    Case "0"
                        sDiffCmt = (New FGDIFF01).Display_Result(moForm, pntFrmXY.X + Me.Width, pntFrmXY.Y + pntCtlXY.Y + spdResult.Height, rsTestCd, rsSpcCd, msRegNo, msPatNm, msSexAge, sWBCRst, sBfViewRsts, al_RstInfo)
                    Case "1"
                        sDiffCmt = (New FGDIFF02).Display_Result(moForm, pntFrmXY.X + Me.Width, pntFrmXY.Y + pntCtlXY.Y + spdResult.Height, rsTestCd, rsSpcCd, msRegNo, msPatNm, msSexAge, sWBCRst, sBfViewRsts, al_RstInfo)
                    Case Else
                        Return
                End Select

                Dim iPos As Integer = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, rsTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                If iPos > 0 Then
                    .Row = iPos
                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("bcno") : sBcNo = .Text
                    .Col = .GetColFromID("slipcd") : sPartSlip = .Text
                    .Col = .GetColFromID("tnmd") : sTnmd_p = .Text
                End If

                For iRow As Integer = 1 To al_RstInfo.Count
                    iPos = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                    If iPos > 0 Then
                        .Row = iPos

                        .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
                        .Col = .GetColFromID("orgrst") : Dim sOrgRst As String = .Text

                        If sTnmd.ToLower.IndexOf("neu#(anc)") >= 0 And IsNumeric(sOrgRst) And IsNumeric(CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst) Then
                            If Val(sOrgRst) <= 0.5 And Val(CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst) >= 0.5 Then
                                MsgBox("검사[" + sTnmd + "] 값을 확인하세요.!")
                            End If
                        End If

                        If sOrgRst <> "" And CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst = "" Then
                            CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst = "0"
                        End If

                        .Col = .GetColFromID("orgrst") : .Text = CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst
                        .Col = .GetColFromID("viewrst")

                        If CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgViewRst <> "" Then
                            .Text = CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgViewRst
                        Else
                            .Text = CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst
                        End If

                        If CType(al_RstInfo.Item(iRow - 1), ResultInfo_Test).mOrgRst <> "" Then
                            .Row = iPos
                            If .RowHidden Then .RowHidden = False
                        End If

                        sbSet_ResultView(iPos)
                        sbGet_Calc_Rst(iPos) '-- 결과 계산

                    End If
                Next

                Dim ci As New CMT_INFO
                With ci
                    .BcNo = sBcNo
                    .PartSlip = sPartslip
                    .CmtCont = "[" + sTnmd_p + "] " + sDiffCmt

                End With

                sbSet_Cmt_BcNo_Edit(ci)

                If Ctrl.Get_Code(Me.cboSlip) <> sPartslip Then
                    For ix As Integer = 0 To Me.cboSlip.Items.Count - 1
                        Me.cboSlip.SelectedIndex = ix
                        If Ctrl.Get_Code(Me.cboSlip) = sPartslip Then Exit For
                    Next
                End If
                If Me.cboSlip.SelectedIndex > 0 Then Me.txtCmtCont.Text += "[" + sTnmd_p + "] " + sDiffCmt

                Return


            End With
        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbGet_Calc_Rst(ByVal riRow As Integer)

        Dim objDTable As New DataTable

        Try
            For ix1 As Integer = 1 To spdResult.MaxRows

                If ix1 = riRow Then Continue For

                Dim sBcNo As String = ""
                Dim sTestCd As String = ""
                Dim sSpcCd As String = ""
                Dim sCalGbn As String = ""

                With spdResult
                    .Row = ix1
                    .Col = .GetColFromID("bcno") : sBcNo = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                    .Col = .GetColFromID("calcgbn") : sCalGbn = .Text
                End With

                If sCalGbn = "1" Then
                    objDTable = LISAPP.COMM.CalcFn.fnGet_CalcTests(sBcNo, sTestCd, sSpcCd)
                    If objDTable.Rows.Count < 1 Then Return

                    Dim sCalForm As String = ""
                    Dim iCalCnt As Integer = 0

                    sCalForm = objDTable.Rows(0).Item("calform").ToString.Trim
                    iCalCnt = Convert.ToInt16(objDTable.Rows(0).Item("paramcnt"))

                    For ix As Integer = 0 To iCalCnt - 1
                        Dim sChr As String = Chr(65 + ix)
                        Dim sTCd As String = objDTable.Rows(0).Item("param" + ix.ToString).ToString.Trim
                        Dim sOrgRst As String = ""

                        For intRow As Integer = 1 To spdResult.MaxRows
                            With spdResult
                                .Row = intRow
                                .Col = .GetColFromID("testcd")
                                If .Text.Trim = sTCd.Substring(0, 7).Trim Then
                                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text.Trim
                                    Exit For
                                End If
                            End With
                        Next

                        If sOrgRst <> "" Then sCalForm = sCalForm.Replace(sChr, sOrgRst)
                    Next

                    Try
                        Dim sRst As String = LISAPP.COMM.CalcFn.fnGet_CFCompute(sCalForm)
                        If sRst <> "" Then
                            sRst = fnRstTypeCheck(ix1, sRst)

                            With spdResult
                                .Row = ix1
                                .Col = .GetColFromID("orgrst") : .Text = sRst
                                .Col = .GetColFromID("viewrst") : .Text = sRst

                                sbSet_ResultView(ix1)
                            End With
                        End If
                    Catch ex As Exception

                    End Try

                End If
            Next
        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Sub

    Private Sub sbGet_Calc_Rst(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal riRow As Integer)

        Dim objDTable As New DataTable

        Try
            objDTable = LISAPP.COMM.CalcFn.fnGet_CalcTests(rsBcNo, rsTestCd, rsSpcCd)
            If objDTable.Rows.Count < 1 Then Return

            Dim sCalForm As String = ""
            Dim iCalCnt As Integer = 0

            sCalForm = objDTable.Rows(0).Item("calform").ToString
            iCalCnt = Convert.ToInt16(objDTable.Rows(0).Item("paramcnt"))

            For ix As Integer = 0 To iCalCnt - 1
                Dim sChr As String = Chr(65 + ix)
                Dim sTCd As String = objDTable.Rows(0).Item("param" + ix.ToString).ToString
                Dim sOrgRst As String = ""

                For iRow As Integer = 1 To spdResult.MaxRows
                    With spdResult
                        .Row = iRow
                        .Col = .GetColFromID("testcd")
                        If .Text = sTCd.Substring(0, 7) Then
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text.Trim
                            Exit For
                        End If
                    End With
                Next

                If sOrgRst <> "" Then sCalForm = sCalForm.Replace(sChr, sOrgRst)
            Next

            Try
                Dim sRst As String = LISAPP.COMM.CalcFn.fnGet_CFCompute(sCalForm)
                If sRst <> "" Then
                    With spdResult
                        .Row = riRow : .Col = .GetColFromID("orgrst") : .Text = sRst
                        sbSet_ResultView(riRow)
                    End With
                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try
    End Sub

    Public Sub sbFocus()
        Dim intUnLockRow As Integer = 0

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("orgrst")
                If Not .Lock And .RowHidden = False Then
                    If intUnLockRow = 0 Then intUnLockRow = intRow

                    If .Text = "" Then
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then .ForeColor = Drawing.Color.Black
                        .SetActiveCell(.GetColFromID("orgrst"), intRow)
                        .Focus()

                        .Row = intRow
                        .Col = .GetColFromID("slipcd") : Dim sSlipCd As String = .Text

                        For ix As Integer = 0 To Me.cboSlip.Items.Count - 1
                            Me.cboSlip.SelectedIndex = ix
                            If Ctrl.Get_Code(Me.cboSlip) = sSlipCd Then Exit For
                        Next

                        spdResult_ClickEvent(spdResult, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("orgrst"), intRow))

                        intUnLockRow = 0
                        Exit For
                    End If
                End If
            Next

            If intUnLockRow > 0 Then
                If .MaxRows > 0 Then
                    .SetActiveCell(.GetColFromID("orgrst") + 1, intUnLockRow)
                    .Focus()

                    .SetActiveCell(.GetColFromID("orgrst"), intUnLockRow)
                    .Focus()

                    .Row = intUnLockRow
                    .Col = .GetColFromID("slipcd") : Dim sSlipCd As String = .Text

                    For ix As Integer = 0 To Me.cboSlip.Items.Count - 1
                        Me.cboSlip.SelectedIndex = ix
                        If Ctrl.Get_Code(Me.cboSlip) = sSlipCd Then Exit For
                    Next

                    spdResult_ClickEvent(spdResult, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("orgrst"), intUnLockRow))
                End If
            End If
        End With

    End Sub

    Private Sub sbDisplayCalRst_Info(ByVal r_al As ArrayList)
        Dim sFn As String = "sbDisplayCalRst_Info"

        Try

            If r_al.Count = 0 Then Return

            For i As Integer = 1 To r_al.Count
                Dim sTestCd As String = CType(r_al(i - 1), AxAckCalcResult.CalcRstInfo).TestCd
                Dim sOrgRst As String = CType(r_al(i - 1), AxAckCalcResult.CalcRstInfo).OrgRst

                With Me.spdResult
                    Dim iRow As Integer = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, sTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRow < 1 Then
                        MsgBox("검사코드 찾기 오류 : " + sTestCd)

                        Continue For
                    End If

                    .SetText(.GetColFromID("orgrst"), iRow, sOrgRst)
                    .SetActiveCell(.GetColFromID("viewrst"), iRow)

                    Me.spdResult_KeyDownEvent(Me.spdResult, New AxFPSpreadADO._DSpreadEvents_KeyDownEvent(13, 0))
                End With
            Next

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Function fnSet_Result_Test(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsOrgRst As String) As RST_INFO

        Dim strBcNo$ = "", sTestCd$ = ""
        Dim objRst As New RST_INFO

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")
                .Col = .GetColFromID("testcd") : sTestCd = .Text

                If rsBcNo = strBcNo And rsTestCd = sTestCd Then

                    .Col = .GetColFromID("orgrst") : .Text = rsOrgRst
                    .Col = .GetColFromID("viewrst") : .Text = rsOrgRst

                    sbSet_ResultView(intRow, True)

                    .Row = intRow
                    .Col = .GetColFromID("iud") : objRst.IUD = .Text
                    .Col = .GetColFromID("rsttype") : objRst.RstType = .Text
                    .Col = .GetColFromID("rstulen") : objRst.RstULen = .Text
                    .Col = .GetColFromID("rstllen") : objRst.RstLLen = .Text
                    .Col = .GetColFromID("cutopt") : objRst.CutOpt = .Text
                    .Col = .GetColFromID("refgbn") : objRst.RefGbn = .Text
                    .Col = .GetColFromID("judgtype") : objRst.JudgType = .Text
                    .Col = .GetColFromID("refls") : objRst.RefLs = .Text
                    .Col = .GetColFromID("refl") : objRst.RefL = .Text
                    .Col = .GetColFromID("refhs") : objRst.RefHs = .Text
                    .Col = .GetColFromID("refh") : objRst.RefH = .Text
                    .Col = .GetColFromID("panicgbn") : objRst.PanicGbn = .Text
                    .Col = .GetColFromID("panicl") : objRst.PanicL = .Text
                    .Col = .GetColFromID("pnaich") : objRst.PanicH = .Text
                    .Col = .GetColFromID("spccd") : objRst.SpcCd = .Text
                    .Col = .GetColFromID("ujudglt1") : objRst.UJudglt1 = .Text
                    .Col = .GetColFromID("ujudglt2") : objRst.UJudglt2 = .Text
                    .Col = .GetColFromID("ujudglt3") : objRst.UJudglt3 = .Text
                    .Col = .GetColFromID("deltagbn") : objRst.DeltaGbn = .Text
                    .Col = .GetColFromID("deltal") : objRst.DeltaL = .Text
                    .Col = .GetColFromID("delth") : objRst.DeltaH = .Text
                    .Col = .GetColFromID("deltaday") : objRst.DeltaDay = .Text
                    .Col = .GetColFromID("criticalgbn") : objRst.CriticalGbn = .Text
                    .Col = .GetColFromID("criticall") : objRst.CriticalL = .Text
                    .Col = .GetColFromID("criticalh") : objRst.CriticalH = .Text
                    .Col = .GetColFromID("aleartgbn") : objRst.AlertGbn = .Text
                    .Col = .GetColFromID("alertl") : objRst.AlertL = .Text
                    .Col = .GetColFromID("alerth") : objRst.AlertH = .Text
                    .Col = .GetColFromID("alimitgbn") : objRst.AlimitGbn = .Text
                    .Col = .GetColFromID("alimitls") : objRst.AlimitLs = .Text
                    .Col = .GetColFromID("alimitl") : objRst.AlimitL = .Text
                    .Col = .GetColFromID("alimith") : objRst.AlimitH = .Text
                    .Col = .GetColFromID("alimiths") : objRst.AlimitHs = ""

                    .Col = .GetColFromID("orgrst") : objRst.OrgRst = .Text
                    .Col = .GetColFromID("viewrst") : objRst.ViewRst = .Text
                    .Col = .GetColFromID("bforgrst1") : objRst.BfOrgRst = .Text
                    .Col = .GetColFromID("bfviewrst1") : objRst.BfViewRst = .Text
                    .Col = .GetColFromID("bffndt1") : objRst.BfFnDt = .Text
                    .Col = .GetColFromID("rstflgmark") : objRst.RstFlg = .Text
                    .Col = .GetColFromID("reftxt") : objRst.RefTxt = .Text

                    .Col = .GetColFromID("regnm") : objRst.RegNm = .Text
                    .Col = .GetColFromID("mwnm") : objRst.MwNm = .Text
                    .Col = .GetColFromID("fnnm") : objRst.FnNm = .Text
                    .Col = .GetColFromID("rstcmt") : objRst.RstCmt = .Text

                    .Col = .GetColFromID("hlmark") : objRst.HLMark = .Text
                    .Col = .GetColFromID("panicmark") : objRst.PanicMark = .Text
                    .Col = .GetColFromID("deltamark") : objRst.DeltaMark = .Text
                    .Col = .GetColFromID("criticalmark") : objRst.CriticalMark = .Text
                    .Col = .GetColFromID("alertmark") : objRst.AlertMark = .Text

                    objRst.SpcNm = LISAPP.COMM.RstFn.fnGet_SpcNmInfo(strBcNo)

                    Return objRst

                    Exit For
                End If
            Next
        End With

        Return Nothing

    End Function

    Private Sub sbLog_Exception(ByVal rsMsg As String)
        Me.lstEx.Items.Insert(0, rsMsg)
    End Sub

    Private Sub sbSet_ResultView(ByVal riRow As Integer, Optional ByVal rbTest As Boolean = False)

        With spdResult
            sbRstTypeCheck(riRow)
            sbHLCheck(riRow)
            sbPanicCheck(riRow, m_dt_RstCdHelp)
            sbUJudgCheck(riRow)
            sbDeltaCheck(riRow, m_dt_RstCdHelp)
            sbCriticalCheck(riRow)
            sbAlertCheck(riRow)
            sbAlimitCheck(riRow)
            sbNPCheck(riRow, m_dt_RstCdHelp)

            Dim sORstC$ = "", sVRstC$ = "", sRstCmtC$ = ""
            Dim sORstO$ = "", sVRstO$ = "", sRstCmtO$ = ""

            .Row = riRow
            .Col = .GetColFromID("orgrst") : sORstC = .Text
            .Col = .GetColFromID("viewrst") : sVRstC = .Text
            .Col = .GetColFromID("rstcmt") : sRstCmtC = .Text

            .Col = .GetColFromID("corgrst") : sORstO = .Text
            .Col = .GetColFromID("cviewrst") : sVRstO = .Text
            .Col = .GetColFromID("crstcmt") : sRstCmtO = .Text

            If sORstC <> sORstO Or sVRstC <> sVRstO Or sRstCmtC <> sRstCmtO Then
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Text = "1"
                Else
                    For intRow As Integer = riRow - 1 To 1 Step -1
                        .Row = intRow
                        .Col = .GetColFromID("chk")
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            .Text = "1"
                            Exit For
                        End If
                    Next
                End If
            End If
        End With

        '-- 자동소견변환
        If rbTest = False And mbQueryView = False Then sbGet_CvtCmtInfo(riRow)

    End Sub
    ' 결과 체크
    Private Sub sbSet_JudgRst()

        With spdResult


            For iRow As Integer = 1 To .MaxRows
                .Row = iRow
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("iud") : Dim sIUD As String = .Text

                If sChk = "1" Or sIUD = "1" Then
                    If .GetColFromID("orgrst") > 0 Then
                        .Col = .GetColFromID("orgrst") : Dim sRst As String = .Text.Replace("'", "`") : .Text = sRst
                        .Col = .GetColFromID("viewrst") : .Text = sRst
                        .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                        .Col = .GetColFromID("cvtgbn") : Dim sCvtGbn As String = .Text

                        If sRst <> "" Or sCvtGbn <> "" Then
                            sbRstTypeCheck(iRow)    '-- 실제결과 -> 결과에 표시
                            sbHLCheck(iRow)
                            sbPanicCheck(iRow, m_dt_RstCdHelp)
                            sbUJudgCheck(iRow)
                            sbDeltaCheck(iRow, m_dt_RstCdHelp)
                            sbCriticalCheck(iRow)
                            sbAlertCheck(iRow)
                            sbAlimitCheck(iRow)

                            sbGet_CvtRstInfo(sBcNo, sTestCd)
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    ' 결과저장 가능 확인
    Private Function fnChecakReg(ByVal rsRstflg As String, ByRef r_al_CmtCont As ArrayList, Optional ByVal rsFlag As Boolean = False) As ArrayList

        Dim sFn As String = "Function fnChecakGeneralTestReg(String) As ArrayList"
        Try
            Dim alMsg As New ArrayList
            Dim alMsg2 As New ArrayList
            Dim sChk$ = "", sOrgRst$ = "", sViewRst$ = "", sRstCmt$ = "", sRstflg$ = ""
            Dim sOrgRst_o$ = "", sViewRst_o$ = "", sRstCmt_o$ = ""
            Dim sBcno$ = "", sSlipCd$ = "", sTestCd$ = "", strTnmd$ = "", sTcdGbn$ = "", sTitleYn$ = "", sReqSub$ = ""
            Dim sAlert$ = "", sPanic$ = "", sDelta$ = "", sCritical$ = ""
            Dim sBfViewRst As String = ""

            Dim sBcNo_OLD As String = "", sSlip_Old As String = ""
            Dim sCmtCont As String = ""

            Dim bFlag As Boolean = False

            With Me.spdResult
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("bcno") : sBcno = .Text
                    .Col = .GetColFromID("tnmd") : strTnmd = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1" And sTcdGbn = "P" Then
                        Dim iCnt% = 0
                        For ix As Integer = iRow + 1 To .MaxRows
                            .Row = ix
                            .Col = .GetColFromID("iud") : sChk = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : sReqSub = .Text

                            If (sOrgRst = "" And sReqSub = "1") Or sChk = "" Then
                                .Row = ix
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd Then
                                    .Row = ix
                                    .Col = .GetColFromID("rstflg")
                                    If .Text < rsRstflg Then
                                        .Row = iRow
                                        .Col = .GetColFromID("iud") : .Text = ""
                                        Exit For
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next

                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("bcno") : sBcno = .Text
                    .Col = .GetColFromID("slipcd") : sSlipCd = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tnmd") : strTnmd = .Text
                    .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                    .Col = .GetColFromID("titleyn") : sTitleYn = .Text
                    .Col = .GetColFromID("reqsub") : sReqSub = .Text

                    .Col = .GetColFromID("iud") : sChk = .Text
                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text
                    .Col = .GetColFromID("rstcmt") : sRstCmt = .Text
                    .Col = .GetColFromID("rstflg") : sRstflg = .Text

                    .Col = .GetColFromID("alertmark") : sAlert = .Text
                    .Col = .GetColFromID("panicmark") : sPanic = .Text
                    .Col = .GetColFromID("deltamark") : sDelta = .Text
                    .Col = .GetColFromID("criticalmark") : sCritical = .Text

                    .Col = .GetColFromID("corgrst") : sOrgRst_o = .Text
                    .Col = .GetColFromID("cviewrst") : sViewRst_o = .Text
                    .Col = .GetColFromID("crstcmt") : sRstCmt_o = .Text

                    If sChk = "1" And sOrgRst <> "" Then

                        If sBcno <> sBcNo_OLD And sSlipCd <> sSlip_Old And sCmtCont <> "" Then

                            sCmtCont = sCmtCont.Substring(0, sCmtCont.Length - 1)
                            sCmtCont = "다음과 같이 최종보고 되었던 자료 입니다." + vbCrLf + "[" + sCmtCont.Trim + "]"

                            Dim objCmt As New CMT_INFO

                            objCmt.BcNo = sBcNo_OLD
                            objCmt.PartSlip = sSlip_Old
                            objCmt.CmtCont = sCmtCont

                            r_al_CmtCont.Add(objCmt)

                            sCmtCont = ""
                        End If

                        sBcNo_OLD = sBcno : sSlip_Old = sSlipCd

                        bFlag = False

                        If rsRstflg = "3" Then
                            If sRstflg = "3" Then
                                If (sTcdGbn = "P" Or sTcdGbn = "B") And sTitleYn = "1" Then
                                Else
                                    If sOrgRst = sOrgRst_o And sViewRst = sViewRst_o And sRstCmt = sRstCmt_o Then
                                        alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 바뀐정보가 없습니다.")
                                        bFlag = True
                                    ElseIf sOrgRst <> sOrgRst_o Or sViewRst <> sViewRst_o Then
                                        sCmtCont += strTnmd + "{" + sOrgRst_o + "/" + sViewRst_o + "}|"
                                    End If
                                End If
                            End If
                        End If

                        '미생물 분야별 최종보고->중간보고 진행 안되도록..
                        If rsRstflg = "22" And rsFlag Then
                            If sRstflg = "3" Then
                                alMsg2.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 최종보고된 자료 입니다.")
                                bFlag = True
                            End If
                        End If

                        If rsRstflg = "2" Then
                            If sRstflg = "3" Then
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 최종보고된 자료 입니다.")
                                bFlag = True
                            ElseIf sRstflg = "2" Then
                                If (sTcdGbn = "P" Or sTcdGbn = "B") And sTitleYn = "1" Then
                                Else
                                    If sOrgRst = sOrgRst_o And sViewRst = sViewRst_o And sRstCmt = sRstCmt_o Then
                                        alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 바뀐정보가 없습니다.")
                                        bFlag = True
                                    End If
                                End If
                            End If
                        End If

                        If rsRstflg = "1" Then
                            If sRstflg = "3" Or sRstflg = "2" Then
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 " + IIf(sRstflg = "3", "최종보고", "중간보고").ToString + "된 자료 입니다.")
                                bFlag = True
                            Else
                                If (sTcdGbn = "P" Or sTcdGbn = "B") And sTitleYn = "1" Then
                                Else
                                    If sOrgRst = sOrgRst_o And sViewRst = sViewRst_o And sRstCmt = sRstCmt_o Then
                                        alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 바뀐정보가 없습니다.")
                                        bFlag = True
                                    End If
                                End If
                            End If
                        End If

                        If sAlert = "A" And STU_AUTHORITY.AFNReg <> "1" Then
                            bFlag = True
                            alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 Aleart에 대한 보고권한이 없습니다.")
                        End If

                        If sPanic = "P" And STU_AUTHORITY.PDFNReg <> "1" Then
                            bFlag = True
                            alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 Panic에 대한 보고권한이 없습니다.")
                        End If

                        If sDelta = "D" And STU_AUTHORITY.DFNReg <> "1" Then
                            bFlag = True
                            alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 Delta에 대한 보고권한이 없습니다.")
                        End If

                        If sCritical = "C" And STU_AUTHORITY.CFNReg <> "1" Then
                            bFlag = True
                            alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 Critical에 대한 보고권한이 없습니다.")
                        End If

                        If sRstflg = "3" Then
                            If sOrgRst <> sOrgRst_o And STU_AUTHORITY.FNUpdate <> "1" Then
                                bFlag = True
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 최종보고수정에 대한 보고권한이 없습니다.")
                            End If
                        Else
                            If sOrgRst <> sOrgRst_o And STU_AUTHORITY.RstUpdate <> "1" Then
                                bFlag = True
                                alMsg.Add("'검체번호: " + sBcno + ", 검사항목: [" + sTestCd + "] " + strTnmd + "'은 결과수정에 대한 보고권한이 없습니다.")
                            End If
                        End If

                        If bFlag Then
                            .Row = iRow
                            '.Col = .GetColFromID("chk")
                            'If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .Text = ""
                            .Col = .GetColFromID("iud") : .Text = ""
                        End If
                    End If
                Next

                If sCmtCont <> "" Then
                    sCmtCont = sCmtCont.Substring(0, sCmtCont.Length - 1)
                    sCmtCont = "다음과 같이 최종보고 되었던 자료 입니다." + vbCrLf + "[" + sCmtCont.Trim + "]"

                    Dim objCmt As New CMT_INFO

                    objCmt.BcNo = sBcNo_OLD
                    objCmt.PartSlip = sSlip_Old
                    objCmt.CmtCont = sCmtCont

                    r_al_CmtCont.Add(objCmt)

                    sCmtCont = ""
                End If

                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tcdgbn") : sTcdGbn = .Text
                    .Col = .GetColFromID("titleyn") : sTitleYn = .Text
                    .Col = .GetColFromID("rstflg") : sRstflg = .Text

                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1" And sTcdGbn = "P" And sTitleYn <> "0" Then
                        Dim iCnt% = 0
                        For ix As Integer = iRow + 1 To .MaxRows
                            .Row = ix
                            .Col = .GetColFromID("iud") : Dim sIUD As String = .Text
                            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                            .Col = .GetColFromID("reqsub") : sReqSub = .Text
                            .Col = .GetColFromID("rstflg") : Dim sSubRstFlg As String = .Text
                            If sIUD = "1" Then
                                .Row = ix

                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd Then
                                    iCnt += 1
                                Else
                                    Exit For
                                End If
                            ElseIf sReqSub = "1" And sOrgRst = "" Then
                                iCnt = 99
                                Exit For
                            ElseIf sRstflg < sSubRstFlg Then
                                iCnt = 1
                            End If
                        Next

                        If iCnt = 0 Then
                            .Row = iRow
                            .Col = .GetColFromID("chk") : .Text = ""
                            .Col = .GetColFromID("iud") : .Text = ""
                        ElseIf iCnt = 99 Then
                            .Row = iRow
                            .Col = .GetColFromID("iud") : .Text = ""
                        End If
                    End If
                Next
            End With

            If alMsg2.Count > 0 Then
                Dim strMsg As String = ""
                For intIdx As Integer = 0 To alMsg2.Count - 1
                    strMsg += alMsg2.Item(intIdx).ToString + vbCrLf
                Next

                MsgBox(strMsg + vbCrLf + "위 자료는 결과를 저장할 수 없습니다.", MsgBoxStyle.Information)
            End If


            fnChecakReg = alMsg
        Catch ex As Exception
            fnChecakReg = New ArrayList
        End Try

    End Function

    Private Function fnGet_Rst_Normal(ByVal rsRstFlg As String, ByVal rsCfmNm As String, ByVal rsCfmSign As String) As ArrayList
        Dim sFn As String = "Function fnGet_Rst_Normal(string) As ArrayList"
        Try
            Dim arlRst As New ArrayList
            Dim ri As STU_RstInfo

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("iud")
                    If .Text = "1" Then

                        .Col = .GetColFromID("mbttype") : Dim sMbtType As String = .Text

                        .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                     
                        .Col = .GetColFromID("orgrst") : Dim sOrgrst As String = .Text
                        .Col = .GetColFromID("viewrst") : Dim sViewRst As String = .Text
                        .Col = .GetColFromID("panicmark") : Dim sPanic As String = .Text
                        .Col = .GetColFromID("deltamark") : Dim sDelta As String = .Text
                        .Col = .GetColFromID("alertmark") : Dim sAlert As String = .Text
                        .Col = .GetColFromID("criticalmark") : Dim sCritical As String = .Text
                        .Col = .GetColFromID("hlmark") : Dim sHlMark As String = .Text
                        .Col = .GetColFromID("rstcmt") : Dim sRstCmt As String = .Text

                        '< yjlee 2009-01-16
                        .Col = .GetColFromID("titleyn") : Dim sTitleYn As String = .Text
                        .Col = .GetColFromID("tcdgbn") : Dim sTCdGbn As String = .Text
                        '> yjlee 2009-01-16

                        .Col = .GetColFromID("rstflg") : Dim sRstFlg As String = .Text
                        .Col = .GetColFromID("rstflg") : Dim sRstFlg_o As String = .Text
                        .Col = .GetColFromID("corgrst") : Dim sORst_o As String = .Text
                        .Col = .GetColFromID("cviewrst") : Dim sVRst_o As String = .Text
                        .Col = .GetColFromID("crstcmt") : Dim sRstCmt_o As String = .Text
                        .Col = .GetColFromID("cfmnm") : Dim sCfmNm As String = .Text


                        '전재휘 검사항목별 결과저장 문제점 으로 추측.
                        If rsRstFlg = "3" Then
                            sRstFlg = "3"
                        Else
                            If sPanic <> "" Or sDelta <> "" Or sCritical <> "" Or sAlert <> "" Then
                                sRstFlg = "2"
                            Else
                                sRstFlg = rsRstFlg.Substring(0, 1)
                            End If
                        End If


                        ri = New STU_RstInfo
                        ri.TestCd = sTestCd
                        ri.OrgRst = sOrgrst
                        ri.RstCmt = sRstCmt
                        ri.RegStep = sRstFlg
                        ri.CfmNm = ""
                        ri.CfmSign = ""
                        ri.CriticalMark = sCritical
                        ri.AlertMark = sAlert '20211012 jhs alert 추가

                        If sRstFlg = "3" Then
                            ri.CfmNm = IIf(rsCfmNm = "", sCfmNm, rsCfmNm).ToString
                            ri.CfmSign = rsCfmSign
                        End If

                        If sOrgrst <> "" And ((sORst_o <> sOrgrst Or sVRst_o <> sViewRst Or sRstCmt <> sRstCmt_o Or sRstFlg_o < sRstFlg) Or _
                           (sTitleYn = "1" And sTCdGbn = "P")) And (sMbtType <> "2" Or sTCdGbn = "C") Then

                            arlRst.Add(ri)

                        End If
                        ri = Nothing

                    End If
                Next

                Return arlRst
            End With
        Catch ex As Exception
            Return New ArrayList
        End Try

    End Function

    Private Sub sbGet_CvtRstInfo(ByVal rsBcNo As String, Optional ByVal rsTestCd As String = "", Optional ByVal rsIFGbn As Boolean = False)
        Try
            Dim alRst As New ArrayList
            Dim sBcNo As String = ""
            Dim sTestCd$ = "", sSpcCd$ = "", sViewRst$ = "", sHlMark$ = ""

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                    .Col = .GetColFromID("orgrst") : Dim sOrgRst As String = .Text
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text
                    .Col = .GetColFromID("hlmark") : sHlMark = .Text

                    If sOrgRst <> "" Then
                        Dim objRst As New STU_RstInfo_cvt

                        objRst.BcNo = sBcNo
                        objRst.TestCd = sTestCd
                        objRst.SpcCd = sSpcCd
                        objRst.OrgRst = sOrgRst
                        objRst.ViewRst = sViewRst
                        objRst.HlMark = sHlMark

                        alRst.Add(objRst)
                    End If
                Next
            End With

            Dim alCvtRst As New ArrayList

            If rsTestCd = "" Then
                alCvtRst = LISAPP.COMM.CvtRst.fnCvtRstInfo(rsBcNo, alRst, rsIFGbn)
            Else
                alCvtRst = LISAPP.COMM.CvtRst.fnCvtRstInfo(rsBcNo, rsTestCd, alRst, rsIFGbn)
            End If

            If alCvtRst.Count < 1 Then Exit Sub

            With spdResult
                For intIdx As Integer = 0 To alCvtRst.Count - 1

                    For intRow As Integer = 1 To .MaxRows
                        .Row = intRow
                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : sTestCd = .Text


                        If CType(alCvtRst(intIdx), STU_RstInfo_cvt).BcNo = sBcNo And CType(alCvtRst(intIdx), STU_RstInfo_cvt).TestCd = sTestCd Then
                            If CType(alCvtRst(intIdx), STU_RstInfo_cvt).CvtFldGbn <> "C" Then

                                If CType(alCvtRst(intIdx), STU_RstInfo_cvt).CvtRange = "B" Then
                                Else
                                    .Col = .GetColFromID("orgrst") : .Text = CType(alCvtRst(intIdx), STU_RstInfo_cvt).OrgRst
                                End If

                                .Col = .GetColFromID("viewrst") : .Text = CType(alCvtRst(intIdx), STU_RstInfo_cvt).ViewRst

                                .Col = .GetColFromID("tcdgbn")
                                If .Text = "C" Then
                                    For intIx2 As Integer = intRow - 1 To 1 Step -1

                                        .Row = intIx2
                                        .Col = .GetColFromID("tcdgbn") : Dim strTcdGbn As String = .Text
                                        .Col = .GetColFromID("testcd")

                                        If strTcdGbn = "P" And .Text = sTestCd.Substring(0, 5) Then
                                            .Col = .GetColFromID("chk") : .Text = "1"
                                            Exit For
                                        End If
                                    Next
                                End If

                                sbSet_ResultView(intRow)
                            Else
                                .Col = .GetColFromID("rstcmt") : .Text = CType(alCvtRst(intIdx), STU_RstInfo_cvt).RstCmt
                            End If
                            Exit For
                        End If
                    Next
                Next

            End With
        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbGet_CvtCmtInfo(ByVal riRow As Integer)

        Try
            Dim arlRst As New ArrayList
            Dim sBcNo As String = ""
            Dim sTestCd$ = "", sSpcCd$ = "", sOrgRst$ = "", sViewRst$ = "", sHlMark$ = "", sEqFlag As String = ""
            Dim bAfbTest As Boolean = False : Dim sCmtAfb As String = ""
            With spdResult
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text
                    .Col = .GetColFromID("hlmark") : sHlMark = .Text
                    .Col = .GetColFromID("eqflag") : sEqFlag = .Text

                    If sOrgRst <> "" Then
                        Dim objRst As New STU_CvtCmtInfo

                        objRst.BcNo = sBcNo
                        objRst.TestCd = sTestCd
                        objRst.OrgRst = sOrgRst
                        objRst.ViewRst = sViewRst
                        objRst.HlMark = sHlMark
                        objRst.EqFlag = sEqFlag
                        
                        arlRst.Add(objRst)
                        .Row = .ActiveRow
                        .Col = .GetColFromID("testcd")

                        '2019-11-21 AFB검사 여부 체크
                        'If sTestCd = "LM205" Then
                        '    bAfbTest = True
                        'End If
                    End If
                Next
                '2019-12-31 AFB STAIN 검사만 판정되도록수정
                .Row = riRow
                .Col = .GetColFromID("orgrst") : Dim Orgrst As String = .Text
                .Col = .GetColFromID("testcd")
                If Orgrst <> "" And .Text = "LM205" Then
                    bAfbTest = True
                End If
             
            End With
            If bAfbTest = True Then
                Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_AFB_Comment(msBcNo)


                If dt.Rows.Count > 0 Then
                    Dim a_dr As DataRow() = dt.Select("", "spclen desc")

                    Dim sSpcLen As String = a_dr(0).Item("spclen").ToString.Trim

                    sCmtAfb += "*상기 검체 접수일로부터 최근 1주일 이내에 의뢰된 AFB stain 검사 결과" + vbCrLf + vbCrLf
                    sCmtAfb += "검사시행날짜" + Space(8 + CInt(sSpcLen)) + "검체번호" + Space(17) + "검사결과" + vbCrLf
                    '이전 결과를 소견으로 만들어준다
                    For ix = 1 To dt.Rows.Count
                        Dim sSpcLenRow As String = ""

                        sSpcLenRow = dt.Rows(ix - 1).Item("spclen").ToString

                        sSpcLenRow = CStr((8 + CInt(sSpcLen)) - CInt(sSpcLenRow))

                        If ix <> 1 Then sCmtAfb += vbCrLf

                        If dt.Rows(ix - 1).Item("viewrst").ToString.IndexOf(Chr(13)) > 0 Then
                            Dim sViewrst1 As String = dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))(0)
                            Dim sViewrst2 As String = dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))(1)
                            sViewRst = sViewrst1.Replace(Chr(10), "") + " " + sViewrst2.Replace(Chr(10), "")
                        Else
                            sViewRst = dt.Rows(ix - 1).Item("viewrst").ToString
                        End If

                        sCmtAfb += dt.Rows(ix - 1).Item("fndt2").ToString + Space(2) + "[" + dt.Rows(ix - 1).Item("spcnms").ToString + "]" + Space(CInt(sSpcLenRow) - 2) + dt.Rows(ix - 1).Item("bcno").ToString + Space(10) + sViewRst

                        If ix = dt.Rows.Count Then sCmtAfb += vbCrLf + vbCrLf
                    Next

                ElseIf dt.Rows.Count <= 0 Then
                    sCmtAfb += "*상기 검체 접수일로부터 최근 1주일 이내에 의뢰된 AFB stain 검사 결과 : 검사이력 없음 " + vbCrLf + vbCrLf

                End If

                Dim alTmpAfb As New ArrayList
                Dim sBuf1Afb() As String = Me.txtCmtCont.Text.Replace(Chr(10), "").Split(Chr(13))
                Dim sBuf2Afb() As String = sCmtAfb.Replace(Chr(10), "").Split(Chr(13))

                For ix As Integer = 0 To sBuf1Afb.Length - 1
                    alTmpAfb.Add(sBuf1Afb(ix).Trim())
                Next

                sCmtAfb = ""
                For ix As Integer = 0 To sBuf2Afb.Length - 1
                    If alTmpAfb.Contains(sBuf2Afb(ix).Trim) = False Then
                        sCmtAfb += sBuf2Afb(ix) + vbCrLf
                    End If
                Next

                If Me.txtCmtCont.Text = "" Then
                    Me.txtCmtCont.Text = sCmtAfb
                Else
                    Me.txtCmtCont.Text = Me.txtCmtCont.Text + IIf(sCmtAfb = "", "", vbCrLf).ToString + sCmtAfb
                End If

                txtCmtCont_LostFocus(Nothing, Nothing)
            End If


            Dim alCvtCmt As ArrayList = LISAPP.COMM.CvtCmt.fnCvtCmtInfo(sBcNo, arlRst, msPartSlip)

            If alCvtCmt.Count < 1 Then Exit Sub


            Dim sCmt$ = ""

            For intIdx As Integer = 0 To alCvtCmt.Count - 1
                sCmt += CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont
            Next

            Dim alTmp As New ArrayList
            Dim sBuf1() As String = Me.txtCmtCont.Text.Replace(Chr(10), "").Split(Chr(13))
            Dim sBuf2() As String = sCmt.Replace(Chr(10), "").Split(Chr(13))

            For ix As Integer = 0 To sBuf1.Length - 1
                alTmp.Add(sBuf1(ix).Trim())
            Next

            sCmt = ""
            For ix As Integer = 0 To sBuf2.Length - 1
                If alTmp.Contains(sBuf2(ix).Trim) = False Then
                    sCmt += sBuf2(ix) + vbCrLf
                End If
            Next

            If Me.txtCmtCont.Text = "" Then
                Me.txtCmtCont.Text = sCmt
            Else
                Me.txtCmtCont.Text = Me.txtCmtCont.Text + IIf(sCmt = "", "", vbCrLf).ToString + sCmt
            End If

            txtCmtCont_LostFocus(Nothing, Nothing)

        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try
    End Sub

    Private Sub sbGet_CvtCmtInfo_BcNo(ByVal rsBcNo As String)

        Try
            Dim arlRst As New ArrayList
            Dim sBcNo As String = ""
            Dim sTestCd As String = "", sSpcCd As String = "", sOrgRst As String = "", sViewRst As String = "", sHlMark As String = "", sEqFlag As String = "", srstflg As String = ""
            Dim bAfbTest As Boolean = False : Dim sCmtAfb As String = ""
            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text
                    .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                    .Col = .GetColFromID("viewrst") : sViewRst = .Text
                    .Col = .GetColFromID("hlmark") : sHlMark = .Text
                    .Col = .GetColFromID("eqflag") : sEqFlag = .Text
                    .Col = .GetColFromID("rstflg") : srstflg = .Text.Trim

                    If sOrgRst <> "" Then
                        Dim objRst As New STU_CvtCmtInfo

                        objRst.BcNo = sBcNo
                        objRst.TestCd = sTestCd
                        objRst.OrgRst = sOrgRst
                        objRst.ViewRst = sViewRst
                        objRst.HlMark = sHlMark
                        objRst.EqFlag = sEqFlag

                        arlRst.Add(objRst)
                        '2019-12-31 AFB STAIN 검사만 판정되도록수정
                        If sTestCd = "LM205" And srstflg = "" Then
                            bAfbTest = True
                        End If

                    End If
                Next

              
            End With
            '<2019-01-08
            If bAfbTest = True Then
                Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_AFB_Comment(msBcNo)


                If dt.Rows.Count > 0 Then
                    Dim a_dr As DataRow() = dt.Select("", "spclen desc")

                    Dim sSpcLen As String = a_dr(0).Item("spclen").ToString.Trim

                    sCmtAfb += "*상기 검체 접수일로부터 최근 1주일 이내에 의뢰된 AFB stain 검사 결과" + vbCrLf + vbCrLf
                    sCmtAfb += "검사시행날짜" + Space(8 + CInt(sSpcLen)) + "검체번호" + Space(17) + "검사결과" + vbCrLf
                    '이전 결과를 소견으로 만들어준다
                    For ix = 1 To dt.Rows.Count
                        Dim sSpcLenRow As String = ""

                        sSpcLenRow = dt.Rows(ix - 1).Item("spclen").ToString

                        sSpcLenRow = CStr((8 + CInt(sSpcLen)) - CInt(sSpcLenRow))

                        If ix <> 1 Then sCmtAfb += vbCrLf

                        If dt.Rows(ix - 1).Item("viewrst").ToString.IndexOf(Chr(13)) > 0 Then
                            Dim sViewrst1 As String = dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))(0)
                            Dim sViewrst2 As String = dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))(1)
                            sViewRst = sViewrst1.Replace(Chr(10), "") + " " + sViewrst2.Replace(Chr(10), "")
                        Else
                            sViewRst = dt.Rows(ix - 1).Item("viewrst").ToString
                        End If

                        sCmtAfb += dt.Rows(ix - 1).Item("fndt2").ToString + Space(2) + "[" + dt.Rows(ix - 1).Item("spcnms").ToString + "]" + Space(CInt(sSpcLenRow) - 2) + dt.Rows(ix - 1).Item("bcno").ToString + Space(10) + sViewRst

                        If ix = dt.Rows.Count Then sCmtAfb += vbCrLf + vbCrLf
                    Next

                ElseIf dt.Rows.Count <= 0 Then
                    sCmtAfb += "*상기 검체 접수일로부터 최근 1주일 이내에 의뢰된 AFB stain 검사 결과 : 검사이력 없음 " + vbCrLf + vbCrLf

                End If

                Dim alTmpAfb As New ArrayList
                Dim sBuf1Afb() As String = Me.txtCmtCont.Text.Replace(Chr(10), "").Split(Chr(13))
                Dim sBuf2Afb() As String = sCmtAfb.Replace(Chr(10), "").Split(Chr(13))

                For ix As Integer = 0 To sBuf1Afb.Length - 1
                    alTmpAfb.Add(sBuf1Afb(ix).Trim())
                Next

                sCmtAfb = ""
                For ix As Integer = 0 To sBuf2Afb.Length - 1
                    If alTmpAfb.Contains(sBuf2Afb(ix).Trim) = False Then
                        sCmtAfb += sBuf2Afb(ix) + vbCrLf
                    End If
                Next

                If Me.txtCmtCont.Text = "" Then
                    Me.txtCmtCont.Text = sCmtAfb
                Else
                    Me.txtCmtCont.Text = Me.txtCmtCont.Text + IIf(sCmtAfb = "", "", vbCrLf).ToString + sCmtAfb
                End If

                ' txtCmtCont_LostFocus(Nothing, Nothing)
            End If
            '>
            '자동소견 
            Dim alCvtCmt As ArrayList = LISAPP.COMM.CvtCmt.fnCvtCmtInfo(rsBcNo, arlRst, msPartSlip, True)

            If alCvtCmt.Count < 1 Then Exit Sub

            Dim sCmt$ = ""

            For intIdx As Integer = 0 To alCvtCmt.Count - 1
                If msPartSlip = CType(alCvtCmt(intIdx), STU_CvtCmtInfo).SlipCd Then
                    sCmt += CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont

                    If CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont = "" Then
                        Me.txtCmtCont.Text = Me.txtCmtCont.Text.Replace(CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont_Base + vbCrLf, "")
                        Me.txtCmtCont.Text = Me.txtCmtCont.Text.Replace(CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont_Base, "")
                    End If

                Else
                    Dim ci As New CMT_INFO
                    With ci
                        .BcNo = rsBcNo
                        .PartSlip = CType(alCvtCmt(intIdx), STU_CvtCmtInfo).SlipCd

                        If CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont = "" Then
                            .CmtCont = CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont
                        Else
                            .CmtCont = CType(alCvtCmt(intIdx), STU_CvtCmtInfo).CmtCont_Base
                        End If
                    End With

                    sbSet_Cmt_BcNo_Edit(ci)

                End If
            Next

            Dim alTmp As New ArrayList
            Dim sBuf1() As String = Me.txtCmtCont.Text.Replace(Chr(10), "").Split(Chr(13))
            Dim sBuf2() As String = sCmt.Replace(Chr(10), "").Split(Chr(13))

            For ix As Integer = 0 To sBuf1.Length - 1
                alTmp.Add(sBuf1(ix).Trim())
            Next

            sCmt = ""
            For ix As Integer = 0 To sBuf2.Length - 1
                If alTmp.Contains(sBuf2(ix).Trim) = False Then
                    sCmt += sBuf2(ix) + vbCrLf
                End If
            Next

            If Me.txtCmtCont.Text = "" Then
                Me.txtCmtCont.Text = sCmt
            ElseIf sCmt <> "" Then
                Me.txtCmtCont.Text += vbCrLf + sCmt
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Private Function fnGet_Rst_ReRun(ByVal rsRerunGbn As String, ByRef rsCmtCont As String, ByRef rsErrMsg As String) As ArrayList
        Dim sFn As String = "Function fnGet_Rst_ReRun(string) As ArrayList"
        Try
            Dim aryRst As New ArrayList
            Dim strBcNo As String = "", sTestCd As String = "", sTestCd_p As String = "", strTcdGbn As String = ""
            Dim sRstflg As String = "", strTnmd As String = "", strEqCd As String = "", strOrgRst As String = "", strViewRst As String = ""
            Dim blnFlag As Boolean = False

            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text = "1" Then
                        .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : sTestCd = .Text
                        .Col = .GetColFromID("rstflg") : sRstflg = .Text
                        .Col = .GetColFromID("tcdgbn") : strTcdGbn = .Text
                        .Col = .GetColFromID("testcd") : sTestCd_p = .Text : If sTestCd_p <> "" Then sTestCd_p = sTestCd_p.Substring(0, 5)
                        .Col = .GetColFromID("tnmd") : strTnmd = .Text
                        .Col = .GetColFromID("eqcd") : strEqCd = .Text
                        .Col = .GetColFromID("orgrst") : strOrgRst = .Text
                        .Col = .GetColFromID("viewrst") : strViewRst = .Text

                        Dim objRst As New RERUN_INFO

                        blnFlag = True
                        If sRstflg = "3" Then
                            If STU_AUTHORITY.FNUpdate = "1" Then
                                sRstflg = "1"

                                rsCmtCont += strTnmd + "(" + strOrgRst + "/" + strViewRst + "),"
                            Else
                                rsErrMsg += strTnmd + "|"
                                blnFlag = False
                            End If
                        ElseIf sRstflg = "2" Or sRstflg = "1" Then
                            sRstflg = "1"
                        Else
                            sRstflg = ""
                        End If

                        If blnFlag And strEqCd <> "" Then
                            objRst.msRstFlg = sRstflg
                            objRst.msBcNo = strBcNo
                            objRst.msTestCd = sTestCd
                            objRst.msRerunGbn = rsRerunGbn

                            aryRst.Add(objRst)
                            objRst = Nothing
                        End If

                    End If
                Next

                If rsCmtCont <> "" Then
                    rsCmtCont = rsCmtCont.Substring(0, rsCmtCont.Length - 1)
                    rsCmtCont = "다음과 같이 최종보고 되었던 자료 입니다." + vbCrLf + "[" + rsCmtCont.Trim + "]"
                End If

                fnGet_Rst_ReRun = aryRst
            End With
        Catch ex As Exception
            fnGet_Rst_ReRun = New ArrayList
        End Try

    End Function

    Private Function fnGet_Rst_Erase() As ArrayList
        Dim sFn As String = "Function fnGet_Rst_Erase() As ArrayList"
        Try
            Dim aryRst As New ArrayList
            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text = "1" Then

                        Dim objRst As New ResultInfo_Test

                        .Col = .GetColFromID("bcno") : objRst.mBCNO = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : objRst.mTestCd = .Text

                        objRst.mAlertMark = ""
                        objRst.mBatchCmt = ""
                        objRst.mBatchRstChk = ""
                        objRst.mBFBCNO = ""
                        objRst.mBFFNDT = ""
                        objRst.mBFORGRST = ""
                        objRst.mBFVIEWRST = ""
                        objRst.mRstCmt = ""
                        objRst.mCriticalMark = ""
                        objRst.mDeltaMark = ""
                        objRst.mDetailYN = ""
                        objRst.mDGTestCd = ""
                        objRst.mEQBCNO = ""
                        objRst.mIntSeqNo = ""
                        objRst.mHLMark = ""
                        objRst.mOrgRst = ""
                        objRst.mPanicMark = ""
                        objRst.mPos = ""
                        objRst.mRack = ""
                        objRst.mSpcCd = ""
                        objRst.mTestNm = ""
                        objRst.mViewRst = ""
                        objRst.mUpdateYN = ""

                        aryRst.Add(objRst)

                        Dim sTestCd_p As String = ""
                        Dim strTCdGbn As String = ""

                        .Col = .GetColFromID("tcdgbn") : strTCdGbn = .Text
                        .Col = .GetColFromID("testcd") : sTestCd_p = .Text.Substring(0, 5)

                        If strTCdGbn = "P" Then
                            For intIdx = intRow + 1 To .MaxRows
                                .Row = intIdx
                                .Col = .GetColFromID("testcd")
                                If .Text.Substring(0, 5) = sTestCd_p Then

                                    objRst = New ResultInfo_Test

                                    .Col = .GetColFromID("bcno") : objRst.mBCNO = .Text.Replace("-", "")
                                    .Col = .GetColFromID("testcd") : objRst.mTestCd = .Text

                                    objRst.mAlertMark = ""
                                    objRst.mBatchCmt = ""
                                    objRst.mBatchRstChk = ""
                                    objRst.mBFBCNO = ""
                                    objRst.mBFFNDT = ""
                                    objRst.mBFORGRST = ""
                                    objRst.mBFVIEWRST = ""
                                    objRst.mRstCmt = ""
                                    objRst.mCriticalMark = ""
                                    objRst.mDeltaMark = ""
                                    objRst.mDetailYN = ""
                                    objRst.mDGTestCd = ""
                                    objRst.mEQBCNO = ""
                                    objRst.mIntSeqNo = ""
                                    objRst.mHLMark = ""
                                    objRst.mOrgRst = ""
                                    objRst.mPanicMark = ""
                                    objRst.mPos = ""
                                    objRst.mRack = ""
                                    objRst.mSpcCd = ""
                                    objRst.mTestNm = ""
                                    objRst.mViewRst = ""
                                    objRst.mUpdateYN = ""

                                    aryRst.Add(objRst)
                                Else
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Next

                fnGet_Rst_Erase = aryRst
            End With
        Catch ex As Exception
            fnGet_Rst_Erase = New ArrayList
        End Try

    End Function


    '-- 결과소거
    Public Function fnReg_Erase() As Boolean
        Dim arlRst As New ArrayList

        Try
            If STU_AUTHORITY.RstClear = "1" Then
                arlRst = fnGet_Rst_Erase()

                If arlRst.Count > 0 Then
                    Dim objRst As New LISAPP.APP_R.AxRstFn

                    Return objRst.fnRsg_RstClear(STU_AUTHORITY.UsrID, arlRst)
                End If
            End If


            Return True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return False
        End Try
    End Function

    '-- 재검 설정
    Public Function fnReRun(ByVal rsReRunGbn As String) As Boolean

        Dim arlRst As New ArrayList
        Dim arlCmt As New ArrayList

        Dim strErrMsg As String = ""

        Try
            Dim strCmtCont As String = ""

            arlRst = fnGet_Rst_ReRun(rsReRunGbn, strCmtCont, strErrMsg)
            If strErrMsg <> "" Then
                MsgBox("[검사명 : " + strErrMsg.Substring(0, strErrMsg.Length - 1) + "]는 최종보고된 자료입니다.!!" + vbCrLf + _
                       "최종보고 수정 권한이 없어 재검할 수 없습니다.")
            End If

            If strCmtCont <> "" Then
                Dim frm As New FGFINAL_CMT

                frm.msBcNo = msBcNo
                frm.msCmt = strCmtCont
                Dim strRet As String = frm.Display_Result()

                If strRet = "" Then Return True

                If Me.txtCmtCont.Text.IndexOf(strCmtCont) < 0 Then
                    If Me.txtCmtCont.Text <> "" Then
                        strCmtCont = Me.txtCmtCont.Text + vbCrLf + strCmtCont
                    End If

                    Dim arlBuf() As String

                    arlBuf = Me.txtCmtCont.Text.Replace(Chr(10), "").Split(Chr(13))

                    For intIdx As Integer = 0 To arlBuf.Length - 1
                        Dim objBR As New ResultInfo_Cmt
                        objBR.BcNo = msBcNo
                        objBR.PartSlip = msPartSlip
                        objBR.TestCd = ""

                        objBR.RstSeq = Convert.ToString(intIdx).PadLeft(2, "0"c)
                        objBR.Cmt = arlBuf(intIdx)
                        objBR.RstFlg = ""

                        arlCmt.Add(objBR)
                    Next
                End If
            End If

            If arlRst.Count > 0 Then
                Dim objRst As New LISAPP.APP_R.AxRstFn

                Return objRst.fnReg_rerun(arlRst, arlCmt)
            End If


            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return False
        End Try

    End Function

    Private Function fnFind_Enable_Reg(ByVal riRow As Integer, ByVal rsRegStep As String, ByVal riChange As Integer, _
                                           ByVal riAlert As Integer, ByVal riPanic As Integer, ByVal riDelta As Integer, ByVal riCritical As Integer) As Integer
        Dim sFn As String = "fnFind_Enable_Reg"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

            Dim sRstflg As String = Ctrl.Get_Code(spd, spd.GetColFromID("rstflg"), riRow)
            Dim iRstflg As Integer = 0

            If IsNumeric(sRstflg) Then
                iRstflg = Convert.ToInt32(sRstflg)
            Else
                iRstflg = 0
            End If

            Dim iReturn As Integer = -1

            Select Case iRstflg
                Case 0
                    '없음
                    Select Case rsRegStep
                        Case "1"
                            '@없음 --> 결과저장
                            If riChange > 0 Then
                                iReturn = -1
                            Else
                                iReturn = 0
                            End If

                        Case "2", "3"
                            '@없음 --> 중간보고 또는 최종보고
                            If riChange > 0 Then
                                If riCritical > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptC) = False Then iReturn = mc_iSklCd_RptC
                                If riDelta > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptD) = False Then iReturn = mc_iSklCd_RptD
                                If riPanic > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptP) = False Then iReturn = mc_iSklCd_RptP
                                If riAlert > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptA) = False Then iReturn = mc_iSklCd_RptA

                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                iReturn = 0
                            End If

                    End Select

                Case 1
                    '결과저장
                    Select Case rsRegStep
                        Case "1"
                            '@결과저장 --> 결과저장
                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                iReturn = 0
                            End If

                        Case "2", "3"
                            '@결과저장 --> 중간보고 또는 최종보고
                            If riCritical > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptC) = False Then iReturn = mc_iSklCd_RptC
                            If riDelta > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptD) = False Then iReturn = mc_iSklCd_RptD
                            If riPanic > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptP) = False Then iReturn = mc_iSklCd_RptP
                            If riAlert > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptA) = False Then iReturn = mc_iSklCd_RptA

                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                'Rstflg만 1 -> 2 또는 3
                                iReturn = -1
                            End If

                    End Select

                Case 2
                    '중간보고
                    Select Case rsRegStep
                        Case "1"
                            '@중간보고 --> 결과저장
                            iReturn = mc_iRptCd_Mw

                        Case "2"
                            '@중간보고 --> 중간보고
                            If riChange > 0 Then
                                If riCritical > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptC) = False Then iReturn = mc_iSklCd_RptC
                                If riDelta > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptD) = False Then iReturn = mc_iSklCd_RptD
                                If riPanic > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptP) = False Then iReturn = mc_iSklCd_RptP
                                If riAlert > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptA) = False Then iReturn = mc_iSklCd_RptA

                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                iReturn = 0
                            End If

                        Case "3"
                            '@중간보고 --> 최종보고
                            If riCritical > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptC) = False Then iReturn = mc_iSklCd_RptC
                            If riDelta > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptD) = False Then iReturn = mc_iSklCd_RptD
                            If riPanic > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptP) = False Then iReturn = mc_iSklCd_RptP
                            If riAlert > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptA) = False Then iReturn = mc_iSklCd_RptA

                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                'Rstflg만 2 -> 3
                                iReturn = -1
                            End If

                    End Select

                Case 3
                    '최종보고
                    Select Case rsRegStep
                        Case "1"
                            '@최종보고 --> 결과저장
                            iReturn = mc_iRptCd_Fn

                        Case "2"
                            '@최종보고 --> 중간보고
                            iReturn = mc_iRptCd_Fn

                        Case "3"
                            '@최종보고 --> 최종보고
                            If riChange > 0 Then
                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgFn) = False Then iReturn = mc_iSklCd_ChgFn

                                If riCritical > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptC) = False Then iReturn = mc_iSklCd_RptC
                                If riDelta > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptD) = False Then iReturn = mc_iSklCd_RptD
                                If riPanic > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptP) = False Then iReturn = mc_iSklCd_RptP
                                If riAlert > 0 Then If USER_SKILL.Authority("R01", mc_iSklCd_RptA) = False Then iReturn = mc_iSklCd_RptA

                                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst) = False Then iReturn = mc_iSklCd_ChgRst
                            Else
                                iReturn = 0
                            End If

                    End Select
            End Select

            Return iReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message)

            Return 0
        End Try
    End Function

    Private Function fnFind_Disable_Msg(ByVal riDisable As Integer) As String
        Dim sFn As String = "fnFind_Disable_Msg"

        Try
            'Private Const mc_iSklCd_ChgRst As Integer = 1        '결과 수정기능
            'Private Const mc_iSklCd_RptA As Integer = 2          'Alert 보고기능
            'Private Const mc_iSklCd_RptP As Integer = 3          'Panic 보고기능
            'Private Const mc_iSklCd_RptD As Integer = 4          'Delta 보고기능
            'Private Const mc_iSklCd_RptC As Integer = 5          'Critical 보고기능
            'Private Const mc_iSklCd_ChgFn As Integer = 6         '최종보고 수정기능

            'Private Const mc_iRptCd_ReqSub As Integer = 10       '결과입력 필수 Child Of Sub. 미입력
            'Private Const mc_iRptCd_Parent As Integer = 11       'Parent Of Sub. 미발견
            'Private Const mc_iRptCd_Mw As Integer = 20           '이미 중간보고
            'Private Const mc_iRptCd_Fn As Integer = 30           '이미 최종보고

            Dim sReturn As String = ""

            Select Case riDisable
                Case 0
                    sReturn = "은(는) 변경된 내용이 없습니다. 확인하여 주십시요!!"

                Case mc_iSklCd_ChgRst, mc_iSklCd_RptA, mc_iSklCd_RptP, mc_iSklCd_RptD, mc_iSklCd_RptC, mc_iSklCd_ChgFn
                    USER_SKILL.Authority("R01", riDisable, sReturn)
                    sReturn += "의 권한이 없습니다. 확인하여 주십시요!!"

                Case mc_iRptCd_ReqSub
                    sReturn = "의 Sub 검사 중 결과입력 필수항목이 있습니다. 확인하여 주십시요!!"

                Case mc_iRptCd_Parent
                    sReturn = "을(를) 찾을 수 없습니다. 확인하여 주십시요!!"

                Case mc_iRptCd_Mw
                    sReturn = "은(는) 이미 중간보고된 상태입니다. 확인하여 주십시요!!"

                Case mc_iRptCd_Fn
                    sReturn = "은(는) 이미 최종보고된 상태입니다. 확인하여 주십시요!!"

            End Select

            Return sReturn

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        End Try
    End Function

    Private Function fnGet_Rst_Micro_Bac(ByVal riRow As Integer, ByVal rsRegStep As String, ByRef r_al_Bac As ArrayList, ByRef riDisable As Integer, _
                                         ByVal rsCfmNm As String, ByVal rsCfmSign As String) As ArrayList
        Dim sFn As String = "fnGet_Rst_Micro_Bac"

        Dim al As New ArrayList
        Dim ri As STU_RstInfo
        Dim ri_b As ResultInfo_Bac

        Try
            '균 변경여부 조사 --> 변경된 결과를 ArrayList에 담기

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

            Dim iChange As Integer = 0

            Dim iGrowth As Integer = 0

            Dim iAlert As Integer = 0
            Dim iPanic As Integer = 0
            Dim iDelta As Integer = 0
            Dim iCritical As Integer = 0

            With spd
                '변경여부 조사
                Dim sBcNo As String = Ctrl.Get_Code(spd, .GetColFromID("bcno"), riRow)
                Dim sTestCd As String = Ctrl.Get_Code(spd, .GetColFromID("testcd"), riRow)
                Dim sSpcCd As String = Ctrl.Get_Code(spd, .GetColFromID("spccd"), riRow)
                Dim sTNmD As String = Ctrl.Get_Code(spd, .GetColFromID("tnmd"), riRow)
                Dim sTCdGbn As String = Ctrl.Get_Code(spd, .GetColFromID("tcdgbn"), riRow)
                Dim sMbTType As String = Ctrl.Get_Code(spd, .GetColFromID("mbttype"), riRow)
                Dim sTitleYN As String = Ctrl.Get_Code(spd, .GetColFromID("titleyn"), riRow)
                Dim sMultiline As String = Ctrl.Get_Code(spd, .GetColFromID("multiline"), riRow)
                Dim sRstFlg As String = Ctrl.Get_Code(spd, .GetColFromID("rstflg"), riRow)
                Dim sCfmNm As String = Ctrl.Get_Code(spd, .GetColFromID("cfmnm"), riRow)

                Dim sOrgRst As String = Ctrl.Get_Code(spd, .GetColFromID("orgrst"), riRow)
                Dim sOrgRst_Tag As String = Ctrl.Get_Code_Tag(spd, .GetColFromID("orgrst"), riRow)

                Dim sRstCmt As String = Ctrl.Get_Code(spd, .GetColFromID("rstcmt"), riRow)
                Dim sRstCmt_Tag As String = Ctrl.Get_Code_Tag(spd, .GetColFromID("rstcmt"), riRow)

                Dim sAlert As String = Ctrl.Get_Code(spd, .GetColFromID("alertmark"), riRow)
                Dim sPanic As String = Ctrl.Get_Code(spd, .GetColFromID("panicmark"), riRow)
                Dim sDelta As String = Ctrl.Get_Code(spd, .GetColFromID("deltamark"), riRow)
                Dim sCritical As String = Ctrl.Get_Code(spd, .GetColFromID("criticalmark"), riRow)

                '변경여부 판단
                If (sTCdGbn = "S" Or sTCdGbn = "P") And sMbTType = "2" And sMultiline <> "L" Then

                    '결과저장 --> 중간보고 --> 최종보고 처럼 RegStep 변경여부 판단
                    If sRstFlg <> "" And sRstFlg < rsRegStep.ToString() Then iChange += 1

                    '-- 2009/01/08 YEJ Modify
                    ''삭제 --> 변경
                    'If miDelCnt_Bac > 0 Then iChange += 1
                    '초기 상태 S인 Row Count 구하여 체크
                    If m_dt_Bac_BcNo.Select("status = 'S' and testcd = '" + sTestCd + "'").Length <> m_dt_Bac_BcNo.Select("testcd = '" + sTestCd + "'").Length Then iChange += 1
                    '-- 2009/01/08 YEJ End

                    '추가 또는 수정 --> 변경
                    Dim spd_b As AxFPSpreadADO.AxfpSpread = Me.spdBac

                    Dim dr As DataRow() = m_dt_Bac_BcNo.Select("status <> 'D' and testcd = '" + sTestCd + "'")
                    Dim dt As DataTable = Fn.ChangeToDataTable(dr)

                    For i As Integer = 0 To dt.Rows.Count - 1
                        With dt.Rows(i)
                            Dim sBacGenCd As String = .Item("bacgencd").ToString
                            Dim sBacCd As String = .Item("baccd").ToString
                            Dim sBacSeq As String = .Item("bacseq").ToString
                            Dim sTestMtd As String = .Item("testmtd").ToString
                            Dim sIncRst As String = .Item("incrst").ToString
                            Dim sBacCmt As String = .Item("baccmt").ToString
                            Dim sBac_testcd As String = .Item("testcd").ToString
                            Dim sRanking As String = .Item("ranking").ToString

                            Dim sBacCd_Tag As String = .Item("oldbaccd").ToString
                            Dim sIncRst_Tag As String = .Item("oldincrst").ToString
                            Dim sBacCmt_Tag As String = .Item("oldbaccmt").ToString
                            Dim sRanking_Tag As String = .Item("oldranking").ToString

                            If sBcNo <> "" And sBacGenCd <> "" And sBacCd <> "" And sBacSeq <> "" Then
                                '수정
                                If sBacCd <> sBacCd_Tag Then iChange += 1
                                If sRanking <> sRanking_Tag Then iChange += 1
                                If sIncRst <> sIncRst_Tag Then iChange += 1
                                If sBacCmt <> sBacCmt_Tag Then iChange += 1

                                iChange += 1
                            Else
                                If sBcNo = "" And sBacGenCd <> "" And sBacCd <> "" And sBacSeq <> "" Then
                                    '추가
                                    iChange += 1
                                End If
                            End If

                            If sBacGenCd <> "" And sBacCd <> "" And sBacSeq <> "" Then
                                ri_b = New ResultInfo_Bac
                                ri_b.TestCd = sTestCd
                                ri_b.SpcCd = sSpcCd
                                ri_b.BacGenCd = sBacGenCd
                                ri_b.BacCd = sBacCd
                                ri_b.BacSeq = sBacSeq
                                ri_b.Ranking = sRanking
                                ri_b.TestMtd = sTestMtd
                                ri_b.IncRst = sIncRst
                                ri_b.BacCmt = sBacCmt
                                r_al_Bac.Add(ri_b)
                                ri_b = Nothing

                                'Nogrowth 균속인지 체크
                                If sBacGenCd <> FixedVariable.gsBacGenCd_Nogrowth Then iGrowth += 1
                            End If
                        End With

                    Next

                    If iChange > 0 Then
                        ri = New STU_RstInfo

                        ri.TestCd = sTestCd

                        If iGrowth > 0 Then
                            ri.OrgRst = FixedVariable.gsRst_Growth
                        Else
                            ri.OrgRst = FixedVariable.gsRst_Nogrowth
                        End If

                        ri.RstCmt = ""
                        ri.EqFlag = ""
                        ri.CfmNm = IIf(rsCfmNm = "", sCfmNm, rsCfmNm).ToString
                        ri.CfmSign = rsCfmSign

                        al.Add(ri)

                        ri = Nothing
                    End If
                End If

                '등록가능여부 조사
                riDisable = fnFind_Enable_Reg(riRow, rsRegStep, iChange, iAlert, iPanic, iDelta, iCritical)

                Return al
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)
            msDisableMsg = ex.Message
        Finally
            al = Nothing

        End Try
    End Function

    Private Function fnGet_Change_Rst_Micro_Anti(ByVal riRow As Integer, ByVal riRegStep As Integer, ByRef r_al_Anti As ArrayList, ByRef riDisable As Integer, _
                                                 ByVal rsCfmNm As String, ByVal rsCfmSign As String) As ArrayList
        Dim sFn As String = "fnGet_Rst_Micro_Anti"

        Dim al As New ArrayList
        Dim ri As STU_RstInfo
        Dim ri_a As ResultInfo_Anti

        Try
            '항균제 변경여부 조사 --> 변경된 결과를 ArrayList에 담기

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

            Dim iChange As Integer = 0

            Dim iGrowth As Integer = 0

            Dim iAlert As Integer = 0
            Dim iPanic As Integer = 0
            Dim iDelta As Integer = 0
            Dim iCritical As Integer = 0

            With spd
                '변경여부 조사
                Dim sTestcd As String = Ctrl.Get_Code(spd, .GetColFromID("testcd"), riRow)
                Dim sSpcCd As String = Ctrl.Get_Code(spd, .GetColFromID("spccd"), riRow)
                Dim sTNmD As String = Ctrl.Get_Code(spd, .GetColFromID("tnmd"), riRow)
                Dim sTCdGbn As String = Ctrl.Get_Code(spd, .GetColFromID("tcdgbn"), riRow)
                Dim sMbTType As String = Ctrl.Get_Code(spd, .GetColFromID("mbttype"), riRow)
                Dim sTitleYN As String = Ctrl.Get_Code(spd, .GetColFromID("titleyn"), riRow)
                Dim sMultiline As String = Ctrl.Get_Code(spd, .GetColFromID("multiline"), riRow)
                Dim sRstflg As String = Ctrl.Get_Code(spd, .GetColFromID("rstflg"), riRow)
                Dim sCfmNm As String = Ctrl.Get_Code(spd, .GetColFromID("cfmnm"), riRow)

                Dim sOrgRst As String = Ctrl.Get_Code(spd, .GetColFromID("orgrst"), riRow)
                Dim sOrgRst_Tag As String = Ctrl.Get_Code_Tag(spd, .GetColFromID("orgrst"), riRow)

                Dim sRstCmt As String = Ctrl.Get_Code(spd, .GetColFromID("rstcmt"), riRow)
                Dim sRstCmt_Tag As String = Ctrl.Get_Code_Tag(spd, .GetColFromID("rstcmt"), riRow)

                Dim sAlert As String = Ctrl.Get_Code(spd, .GetColFromID("alertmark"), riRow)
                Dim sPanic As String = Ctrl.Get_Code(spd, .GetColFromID("panicmark"), riRow)
                Dim sDelta As String = Ctrl.Get_Code(spd, .GetColFromID("deltamark"), riRow)
                Dim sCritical As String = Ctrl.Get_Code(spd, .GetColFromID("criticalmark"), riRow)

                '변경여부 판단
                If (sTCdGbn = "S" Or sTCdGbn = "P") And sMbTType = "2" And sMultiline <> "L" Then
                    'If sOrgRst <> sOrgRst_Tag Then iChange += 1
                    'If sCmt <> sCmt_Tag Then iChange += 1

                    '결과저장 --> 중간보고 --> 최종보고 처럼 RegStep 변경여부 판단
                    If sRstflg <> "" And sRstflg < riRegStep.ToString() Then iChange += 1

                    '초기 상태 S인 Row Count 구하여 체크
                    If m_dt_Anti_BcNo.Select("status = 'S'").Length <> m_dt_Anti_BcNo.Rows.Count Then iChange += 1

                    Dim dr As DataRow() = m_dt_Anti_BcNo.Select("testcd = '" + sTestcd + "'")
                    Dim dt As DataTable = Fn.ChangeToDataTable(dr)

                    With dt
                        For i As Integer = 1 To .Rows.Count
                            Dim sBacCd As String = .Rows(i - 1).Item("baccd").ToString().Trim
                            Dim sBacSeq As String = .Rows(i - 1).Item("bacseq").ToString().Trim
                            Dim sAntiCd As String = .Rows(i - 1).Item("anticd").ToString().Trim
                            Dim sTestMtd As String = .Rows(i - 1).Item("testmtd").ToString().Trim
                            Dim sDecRst As String = .Rows(i - 1).Item("decrst").ToString().Trim
                            Dim sAntiRst As String = .Rows(i - 1).Item("antirst").ToString().Trim
                            Dim sRptYn As String = .Rows(i - 1).Item("rptyn").ToString().Trim
                            Dim sStatus As String = .Rows(i - 1).Item("status").ToString().Trim
                            Dim sAnti_Testcd As String = .Rows(i - 1).Item("testcd").ToString().Trim

                            If sBacCd <> "" And sBacSeq <> "" And sAntiCd <> "" And sTestMtd <> "" And sStatus <> "D" And sTestcd = sAnti_Testcd Then
                                ri_a = New ResultInfo_Anti
                                ri_a.TestCd = sTestcd
                                ri_a.SpcCd = sSpcCd
                                ri_a.BacCd = sBacCd
                                ri_a.BacSeq = sBacSeq
                                ri_a.AntiCd = sAntiCd
                                ri_a.TestMtd = sTestMtd
                                ri_a.DecRst = sDecRst
                                ri_a.AntiRst = sAntiRst
                                ri_a.RptYn = sRptYn

                                r_al_Anti.Add(ri_a)
                                ri_a = Nothing
                            End If
                        Next
                    End With

                    If iChange > 0 Then
                        ri = New STU_RstInfo
                        ri.TestCd = sTestcd
                        ri.OrgRst = sOrgRst
                        ri.RstCmt = ""
                        ri.EqFlag = ""
                        ri.CfmNm = IIf(rsCfmNm = "", sCfmNm, rsCfmNm).ToString
                        ri.CfmSign = rsCfmSign

                        al.Add(ri)
                        ri = Nothing
                    End If
                End If

                '등록가능여부 조사
                riDisable = fnFind_Enable_Reg(riRow, riRegStep.ToString, iChange, iAlert, iPanic, iDelta, iCritical)

                Return al
            End With

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        Finally
            al = Nothing

        End Try
    End Function

    Private Function fnGet_Rst_Micro(ByVal rsRegStep As String, ByRef r_al_Bac As ArrayList, ByRef r_al_Anti As ArrayList, _
                                     ByVal rsCfmNm As String, ByVal rsCfmSign As String) As ArrayList
        Dim sFn As String = "fnGet_Rst_Micro"

        Dim al As New ArrayList
        Dim ri As STU_RstInfo

        Try
            'Check된 검사의 변경된 일반 결과리스트를 가져옴

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

            Dim sOrgRst As String = ""

            With spd
                For i As Integer = 1 To .MaxRows
                    msDisableMsg = ""

                    If Ctrl.Get_Code(spd, .GetColFromID("chk"), i) = "1" And Ctrl.Get_Code(spd, .GetColFromID("mbttype"), i) = "2" Then

                        Dim sTestcd As String = Ctrl.Get_Code(spd, .GetColFromID("testcd"), i)
                        Dim sTNmD As String = Ctrl.Get_Code(spd, .GetColFromID("tnmd"), i)

                        Dim iDisable As Integer = 0
                        Dim iDisable_bac As Integer = 0
                        Dim iDisable_anti As Integer = 0

                        Dim al_bac As ArrayList = fnGet_Rst_Micro_Bac(i, rsRegStep, r_al_Bac, iDisable_bac, rsCfmNm, rsCfmSign)
                        Dim al_anti As ArrayList = fnGet_Change_Rst_Micro_Anti(i, Convert.ToInt16(rsRegStep), r_al_Anti, iDisable_anti, rsCfmNm, rsCfmSign)

                        '균만 변경되거나 항균제만 변경될 수 있으므로 균 + 항균제 내역을 체크해야함
                        If iDisable_bac + iDisable_anti >= 0 Then
                            If iDisable_bac > iDisable_anti Then
                                iDisable = iDisable_bac
                            Else
                                iDisable = iDisable_anti
                            End If

                            msDisableMsg += sTNmD + " ( " + sTestcd + " ) " + fnFind_Disable_Msg(iDisable)
                        Else
                            If al_bac.Count > 0 Then
                                al.Add(al_bac(0))
                            Else
                                If al_anti.Count > 0 Then
                                    al.Add(al_anti(0))
                                End If
                            End If
                        End If

                    End If
                Next

            End With

            Return al

        Catch ex As Exception
            sbLog_Exception(ex.Message)
            Return Nothing

        Finally
            al = Nothing
            ri = Nothing

        End Try
    End Function

    Private Function fnGet_Cmt(ByVal rsRegStep As String) As ArrayList
        Dim sFn As String = "fnGet_Change_Cmt"

        Dim al As New ArrayList
        Dim ci As STU_CvtCmtInfo

        Try
            Dim a_dr As DataRow()
            a_dr = m_dt_Cmt_bcno.Select("status <> 'S'")

            If a_dr.Length > 0 Then
                For ix As Integer = 0 To a_dr.Length - 1
                    Dim sBuf() As String = a_dr(ix).Item("cmtcont").ToString.Replace(Chr(10), "").Split(Chr(13))

                    For i As Integer = 0 To sBuf.Length - 1
                        ci = New STU_CvtCmtInfo
                        ci.BcNo = a_dr(ix).Item("bcno").ToString
                        ci.SlipCd = a_dr(ix).Item("partslip").ToString

                        ci.CmtCont = sBuf(i)
                        ci.RstFlg = rsRegStep

                        al.Add(ci)
                        ci = Nothing
                    Next
                Next
            End If

            Return al

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        Finally
            al = Nothing

        End Try
    End Function

    Public Function fnReg(ByVal rsRstflg As String, Optional ByVal rsCfmNm As String = "", Optional ByVal rsCfmSign As String = "") As Boolean

        Dim sFn As String = "sbReg_Rst"

        Try


            mbLeveCellGbn = False

            Dim alReturn As New ArrayList
            Dim strMsg As String = ""

            sbGet_Alert_Rule()  '-- Alert Rule

            sbDisplay_BcNo_Rst_Bac_One_testcd("")

            sbDisplay_Update()
            sbSet_JudgRst()

            If mbBatchMode = False Then
                With spdResult
                    Dim strBcNo As String = ""
                    Dim strBcNo_t As String = ""

                    For intRow As Integer = 1 To spdResult.MaxRows
                        .Row = intRow
                        .Col = .GetColFromID("bcno") : strBcNo_t = .Text.Replace("-", "")
                        If strBcNo_t <> strBcNo Then
                            If strBcNo <> "" Then
                                sbGet_CvtRstInfo(strBcNo)
                                sbGet_CvtCmtInfo_BcNo(strBcNo)
                            End If
                        End If
                        strBcNo = strBcNo_t
                    Next

                    sbGet_CvtRstInfo(strBcNo)
                    sbGet_CvtCmtInfo_BcNo(strBcNo)

                End With
            End If

            Me.txtCmtCont_LostFocus(Nothing, Nothing)

            Dim alCmtCont As New ArrayList

            alReturn = fnChecakReg(rsRstflg, alCmtCont, True)

            If alCmtCont.Count > 0 Then
                For intIdx As Integer = 0 To alCmtCont.Count - 1
                    Dim frm As New FGFINAL_CMT

                    frm.msBcNo = CType(alCmtCont.Item(intIdx), CMT_INFO).BcNo
                    frm.msPartSlip = CType(alCmtCont.Item(intIdx), CMT_INFO).PartSlip
                    frm.msCmt = CType(alCmtCont.Item(intIdx), CMT_INFO).CmtCont

                    Dim strRet As String = frm.Display_Result()

                    If strRet <> "OK" Then Return False

                    If Me.txtCmtCont.Text.IndexOf(CType(alCmtCont.Item(intIdx), CMT_INFO).CmtCont) < 0 Then
                        If Me.txtCmtCont.Text <> "" Then
                            Me.txtCmtCont.Text += vbCrLf + CType(alCmtCont.Item(intIdx), CMT_INFO).CmtCont
                        Else
                            Me.txtCmtCont.Text += CType(alCmtCont.Item(intIdx), CMT_INFO).CmtCont
                        End If

                        Dim ci As New CMT_INFO
                        With ci
                            .BcNo = CType(alCmtCont.Item(intIdx), CMT_INFO).BcNo
                            .PartSlip = CType(alCmtCont.Item(intIdx), CMT_INFO).PartSlip
                            .CmtCont = Me.txtCmtCont.Text
                        End With

                        sbSet_Cmt_BcNo_Edit(ci)

                    End If
                Next
            End If

            Dim iDisable As Integer = 0

            Dim al_ChgRstN As ArrayList = fnGet_Rst_Normal(rsRstflg, rsCfmNm, rsCfmSign)

            Dim al_ChgBac As New ArrayList
            Dim al_ChgAnti As New ArrayList

            Dim al_ChgRstM As ArrayList = fnGet_Rst_Micro(rsRstflg, al_ChgBac, al_ChgAnti, rsCfmNm, rsCfmSign)

            '배양동정 결과에서 오류 발생 시 
            If al_ChgRstM Is Nothing Then
                MsgBox(msDisableMsg)
                Return False
            End If

            Dim al_ChgCmt As ArrayList = fnGet_Cmt(rsRstflg)

            '소견에서 오류 발생 시 
            If al_ChgCmt Is Nothing Then
                MsgBox(msDisableMsg)
                Return False
            End If

            '소견은 모두 삭제되었을 경우 al_ChgCmt 까지 내용이 없기 때문에 수정된 내역이 없는 것으로 착각될 수 있어
            '       miDelCnt_Cmt와 같이 고려해야함
            If al_ChgRstN.Count = 0 And al_ChgRstM.Count = 0 And Me.txtCmtCont.Text = Me.txtCmtCont.Tag.ToString Then
                Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

                Dim iRow As Integer = 0

                With spd
                    iRow = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                End With

                If iRow > 0 Then
                    MsgBox("수정된 내역이 없습니다. 확인하여 주십시요!!")
                Else
                    MsgBox("선택된 내역이 없습니다. 확인하여 주십시요!!")
                End If
            Else

                Dim si As New STU_SampleInfo

                si.RegStep = rsRstflg
                si.BCNo = msBcNo
                si.EqCd = ""
                si.UsrID = USER_INFO.USRID
                si.UsrIP = USER_INFO.LOCALIP
                si.IntSeqNo = ""
                si.Rack = ""
                si.Pos = ""
                si.EqBCNo = ""
                si.SenderID = Me.Name

                Dim al_ri As New ArrayList
                Dim al_return As New ArrayList

                For i As Integer = 1 To al_ChgRstN.Count
                    al_ri.Add(al_ChgRstN(i - 1))
                Next

                '균 및 항균제 변경 무 --> Nothing 처리
                If al_ChgRstM.Count = 0 Then
                    al_ChgBac = Nothing
                    al_ChgAnti = Nothing
                Else
                    For i As Integer = 1 To al_ChgRstM.Count
                        al_ri.Add(al_ChgRstM(i - 1))
                    Next
                End If

                Dim regrst As New LISAPP.APP_M.RegFn

                regrst.al_Bac = al_ChgBac
                regrst.al_Anti = al_ChgAnti
                regrst.al_Cmt = al_ChgCmt

                Dim iReturn As Integer = regrst.RegServer(al_ri, si, al_return)   ''' 결과등록루틴 

                If iReturn = 0 Then
                    Return False
                Else
                    'If rsRstflg = "2" Then
                    '    Dim oForm = New FGSMSSEND()
                    '    Dim sSMSMsg As String = ""
                    '    sSMSMsg = "등록번호[" + msRegNo + "] 환자이름[" + msPatNm + "] 결과의 중간을 확인하세요.!!" + vbCrLf

                    '    oForm.Display_Result(moForm, msBcNo, sSMSMsg)
                    'End If

                    Return True
                End If
            End If


        Catch ex As Exception
            sbLog_Exception(ex.Message)

            Return False
        Finally
            mbLeveCellGbn = True
        End Try

    End Function

    Public Function fnReg(ByVal rsRstflg As String, ByVal raRstVal As ArrayList, Optional ByVal rsCfmNm As String = "", Optional ByVal rsCfmSign As String = "") As Boolean
        '-- 검사항목별 결과 저장

        Dim alReturn As New ArrayList
        Dim arySql As New ArrayList
        Dim aryTSql As New ArrayList

        Dim al_ChgRstN As New ArrayList
        Dim strMsg As String = ""

        Try
            mbLeveCellGbn = False

            sbGet_Alert_Rule()  '-- Alert Rule

            sbResult_Setting(raRstVal)

            sbDisplay_Update()
            sbSet_JudgRst()

            If mbBatchMode = False Then
                With spdResult
                    Dim strBcNo As String = ""
                    Dim strBcNo_t As String = ""

                    For intRow As Integer = 1 To spdResult.MaxRows

                        .Row = intRow
                        .Col = .GetColFromID("bcno") : strBcNo_t = .Text.Replace("-", "")
                        If strBcNo_t <> strBcNo Then
                            If strBcNo <> "" Then
                                sbGet_CvtRstInfo(strBcNo)
                                sbGet_CvtCmtInfo_BcNo(strBcNo)
                            End If
                        End If
                        strBcNo = strBcNo_t
                    Next

                    sbGet_CvtRstInfo(strBcNo)
                    sbGet_CvtCmtInfo_BcNo(strBcNo)

                End With
            End If

            Me.txtCmtCont_LostFocus(Nothing, Nothing)
            '2020-01-08 
            Dim al_ChgCmt As ArrayList = fnGet_Cmt(rsRstflg)

            Dim arlCmtCont As New ArrayList

            alReturn = fnChecakReg(rsRstflg, arlCmtCont)
            If alReturn.Count > 0 Then
                For intIdx As Integer = 0 To alReturn.Count - 1
                    strMsg += alReturn.Item(intIdx).ToString + vbCrLf
                Next

                MsgBox(strMsg + vbCrLf + "위 자료는 결과를 저장할 수 없습니다.", MsgBoxStyle.Information)
            End If

            If arlCmtCont.Count > 0 Then
                For intIdx As Integer = 0 To arlCmtCont.Count - 1
                    Dim frm As New FGFINAL_CMT

                    frm.msBcNo = CType(arlCmtCont.Item(intIdx), CMT_INFO).BcNo
                    frm.msCmt = CType(arlCmtCont.Item(intIdx), CMT_INFO).CmtCont

                    Dim strRet As String = frm.Display_Result()

                    If strRet <> "OK" Then Return False

                    If Me.txtCmtCont.Text.IndexOf(CType(arlCmtCont.Item(intIdx), CMT_INFO).CmtCont) < 0 Then
                        If Me.txtCmtCont.Text <> "" Then
                            Me.txtCmtCont.Text += vbCrLf + CType(arlCmtCont.Item(intIdx), CMT_INFO).CmtCont
                        Else
                            Me.txtCmtCont.Text += CType(arlCmtCont.Item(intIdx), CMT_INFO).CmtCont
                        End If
                    End If
                Next
            End If

            al_ChgRstN = fnGet_Rst_Normal(rsRstflg, rsCfmNm, rsCfmSign)

            If al_ChgRstN.Count > 0 Then
                Dim si As New STU_SampleInfo

                si.RegStep = rsRstflg
                si.BCNo = msBcNo
                si.EqCd = ""
                si.UsrID = USER_INFO.USRID
                si.UsrIP = USER_INFO.LOCALIP
                si.IntSeqNo = ""
                si.Rack = ""
                si.Pos = ""
                si.EqBCNo = ""
                si.SenderID = Me.Name

                Dim al_ri As New ArrayList
                Dim al_return As New ArrayList

                For i As Integer = 1 To al_ChgRstN.Count
                    al_ri.Add(al_ChgRstN(i - 1))
                Next

                Dim regrst As New LISAPP.APP_M.RegFn

                regrst.al_Bac = Nothing
                regrst.al_Anti = Nothing
                '2020-01-08 al_ChgCmt
                regrst.al_Cmt = al_ChgCmt

                Dim iReturn As Integer = regrst.RegServer(al_ri, si, al_return)

                If iReturn = 0 Then
                    Return False
                Else
                    Return True
                End If

            End If

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Information)
            fnReg = False
        Finally
            mbLeveCellGbn = True
        End Try

    End Function

    Public Sub sbDisplay_Init(ByVal rsType As String)

        Me.spdResult.TextTip = FPSpreadADO.TextTipConstants.TextTipFloating

        miMbtTypeFlag = 0
        Me.lstEx.Items.Clear()

        If rsType = "ALL" Then
            Me.spdResult.MaxRows = 0
            Me.spdBac.MaxRows = 0
            Me.spdAnti.MaxRows = 0

            Me.txtCmtCont.Text = ""
            Me.txtCmtCont.Tag = ""
            Me.lstCode.Hide()
            Me.pnlCode.Visible = False

            Me.txtCmtCont.Visible = True


            Me.lblTestCd.Text = "" : Me.lblTnmd.Text = "" : Me.lblSpccd.Text = ""
            Me.txtTestCd.Text = ""
        End If

        sbLoad_Popup_AntiCd()
        sbLoad_Popup_BacCd()

        Me.chkSelect.Checked = False
        Me.lblBcno.Text = ""

        '결과상태, 결과저장, 중간보고, 최종보고
        Me.lblSampleStatus.Text = ""
        Me.lblReg.Text = ""
        Me.lblMW.Text = ""
        Me.lblFN.Text = ""
        Me.lblCfm.Text = ""

        m_al_Slip_bcno.Clear()

    End Sub

    


    Public Function sbDisplay_Data(ByVal rsBcNo As String) As Boolean
        msBcNo = rsBcNo
        Dim dt As New DataTable
        Dim bFind As Boolean = False

        Try

            'mbQueryView = True

            sbDisplay_Init("ALL")

            sbGet_BacCd()
            sbGet_AntiCd()

            sbDisplay_RegNm(rsBcNo.Substring(0, 14)) ''' 결과상태 조회
            sbDisplay_Result(rsBcNo)  ''' 결과조회 

            sbGet_Alert_Rule()  '-- Alert Rule


            If Me.spdResult.MaxRows > 0 Then bFind = True

        Catch ex As Exception
            sbLog_Exception(ex.Message)

        Finally
            'mbQueryView = False
        End Try

        Return bFind

    End Function

    Public Sub sbDisplay_RegNm(ByVal rsBcNo As String)
        Dim sFn As String = "Sub sbDisplay_RegNm()"

        Try
            Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_RstUsrInfo(rsBcNo)

            Dim sID As String = ""
            Dim sNM As String = ""
            Dim sDT As String = ""

            sbDisplay_Init("")

            Dim a_dr As DataRow()

            a_dr = dt.Select("rstflg >= '1'", "regdt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("regid").ToString().Trim
                sNM = a_dr(i - 1).Item("regnm").ToString().Trim
                sDT = a_dr(i - 1).Item("regdt").ToString().Trim

                If Not sID + sNM + sDT = "" Then
                    Me.lblSampleStatus.Text = "결과저장"
                    '''Me.lblReg.Text = sDT + vbCrLf + sNM
                    Me.lblReg.Text = sDT + " / " + sNM
                    Exit For
                End If
            Next

            a_dr = dt.Select("rstflg >= '2'", "mwdt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("mwid").ToString().Trim
                sNM = a_dr(i - 1).Item("mwnm").ToString().Trim
                sDT = a_dr(i - 1).Item("mwdt").ToString().Trim

                If Not sID + sNM + sDT = "" Then
                    Me.lblSampleStatus.Text = "중간보고"
                    '''Me.lblMW.Text = sDT + vbCrLf + sNM
                    Me.lblMW.Text = sDT + " / " + sNM

                    Exit For
                End If
            Next

            a_dr = dt.Select("rstflg = '3'", "fndt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("fnid").ToString().Trim
                sNM = a_dr(i - 1).Item("fnnm").ToString().Trim
                sDT = a_dr(i - 1).Item("fndt").ToString().Trim

                If Not sID + sNM + sDT = "" Then
                    If a_dr(0).Item("rstflg_j").ToString.Trim <> "2" Then
                        Me.lblSampleStatus.Text = "예비보고"
                    Else
                        Me.lblSampleStatus.Text = "최종보고"
                    End If

                    '''Me.lblFN.Text = sDT + vbCrLf + sNM
                    Me.lblFN.Text = sDT + " / " + sNM
                    Me.lblCfm.Text = a_dr(i - 1).Item("cfmnm").ToString().Trim

                    Exit For
                End If
            Next

            If Me.lblSampleStatus.Text = "최종보고" Then
                Me.lblSampleStatus.BackColor = Drawing.Color.FromArgb(128, 128, 255)
                Me.lblSampleStatus.ForeColor = Drawing.Color.White
            Else
                Me.lblSampleStatus.BackColor = Drawing.Color.FromArgb(255, 192, 128)
                Me.lblSampleStatus.ForeColor = Drawing.Color.Black
            End If

            m_dt_RstUsr = dt.Copy

        Catch ex As Exception

            sbLog_Exception(sFn + ":" + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Result(ByVal rsBcNo As String)
        Dim sFn As String = "Sub sbDisplay_Result(string)"

        Dim dt As New DataTable

        Try
            Dim bRstflgNotFN As Boolean = False

            '-- 검사결과
            dt = LISAPP.COMM.RstFn.fnGet_Result_bcno(rsBcNo.Substring(0, 14), msPartSlip, Me.chkBcnoAll.Checked, msTestCds, msWkGrpCd, msEqCd)
            sbDisplay_ResultView(dt)

            dt = LISAPP.COMM.RstFn.fnGet_Rst_Comment_slip(rsBcNo)
            If dt.Rows.Count < 1 Then
                dt = LISAPP.COMM.RstFn.fnGet_Rst_Comment_test(rsBcNo)
            End If
            m_dt_Cmt_bcno = dt

          
            sbDisplay_slip(rsBcNo)

            If bRstflgNotFN Then
                sbGet_CvtCmtInfo_BcNo(rsBcNo)
            End If

            '-- 검사항목별 결과코드 help
            m_dt_RstCdHelp = LISAPP.COMM.RstFn.fnGet_test_rstinfo(rsBcNo.Substring(0, 14), Nothing)

            sbGet_Calc_Rst(0)           '-- 계산식결과 표시
            sbGet_CvtRstInfo(rsBcNo)    '-- 결과값 자동변환

            '-- 배양결과 가져와서 화면에 표시해준다.
            sbDisplay_BcNo_Rst_Bac(rsBcNo, msTestCds)

            '-- 항생제 결과가 있는 경우 화면에 표시해 준다.
            sbDisplay_BcNo_Rst_Anti(rsBcNo, msTestCds)

            sbDisplay_BcNo_Multi_Micro(rsBcNo)


            If mbBatchMode Then
                Me.axCalcRst.SEXAGE = ""
                Me.axCalcRst.BcNo = ""
            Else
                '> add freety 2008/03/28 : 계산식 결과 관련
                Me.axCalcRst.SEXAGE = msSexAge
                Me.axCalcRst.BcNo = rsBcNo
            End If

        Catch ex As Exception
            sbLog_Exception(sFn + ":" + ex.Message)
        End Try

    End Sub

    Protected Sub sbDisplay_ResultView(ByVal r_dt As DataTable, Optional ByVal rbRstflgNotFN As Boolean = False)
        Dim sFn As String = "Protected Sub Display_ResultView(DataTable)"

        Try
            Dim strCRst_abo$ = "", strCRst_rh$ = "", strORst_abo$ = "", strORst_rh$ = ""
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdResult

            With spd
                .MaxRows = 0

                .ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For ix As Integer = 1 To r_dt.Rows.Count

                    If r_dt.Rows(ix - 1).Item("bcno").ToString.Trim = msBcNo And Not m_al_Slip_bcno.Contains(r_dt.Rows(ix - 1).Item("slipcd").ToString.Trim) Then
                        m_al_Slip_bcno.Add(r_dt.Rows(ix - 1).Item("slipcd").ToString.Trim)
                    End If

                    .Row = ix
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix - 1).Item("bcno").ToString().Trim           '30
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix - 1).Item("testcd").ToString().Trim       '27
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix - 1).Item("spccd").ToString().Trim         '28
                    .Col = .GetColFromID("tclscd") : .Text = r_dt.Rows(ix - 1).Item("tclscd").ToString().Trim      '38
                    .Col = .GetColFromID("slipcd") : .Text = r_dt.Rows(ix - 1).Item("slipcd").ToString().Trim        '38
                    .Col = .GetColFromID("regnm") : .Text = r_dt.Rows(ix - 1).Item("regnm").ToString().Trim         '32
                    .Col = .GetColFromID("regid") : .Text = r_dt.Rows(ix - 1).Item("regid").ToString().Trim          '33
                    .Col = .GetColFromID("mwnm") : .Text = r_dt.Rows(ix - 1).Item("mwnm").ToString().Trim           '35
                    .Col = .GetColFromID("mwid") : .Text = r_dt.Rows(ix - 1).Item("mwid").ToString().Trim           '34
                    .Col = .GetColFromID("fnnm") : .Text = r_dt.Rows(ix - 1).Item("fnnm").ToString().Trim           '37
                    .Col = .GetColFromID("fnid") : .Text = r_dt.Rows(ix - 1).Item("fnid").ToString().Trim          '36
                    .Col = .GetColFromID("reqsub") : .Text = r_dt.Rows(ix - 1).Item("reqsub").ToString().Trim      '45
                    .Col = .GetColFromID("rsttype") : .Text = r_dt.Rows(ix - 1).Item("rsttype").ToString().Trim     '46
                    .Col = .GetColFromID("rstllen") : .Text = r_dt.Rows(ix - 1).Item("rstllen").ToString().Trim    '47
                    .Col = .GetColFromID("rstulen") : .Text = r_dt.Rows(ix - 1).Item("rstulen").ToString().Trim    '47
                    .Col = .GetColFromID("cutopt") : .Text = r_dt.Rows(ix - 1).Item("cutopt").ToString().Trim      '48
                    .Col = .GetColFromID("rerunflg") : .Text = r_dt.Rows(ix - 1).Item("rerunflg").ToString().Trim         '7
                    .Col = .GetColFromID("rstunit") : .Text = r_dt.Rows(ix - 1).Item("rstunit").ToString().Trim     '12
                    .Col = .GetColFromID("judgtype") : .Text = r_dt.Rows(ix - 1).Item("judgtype").ToString().Trim  '49
                    .Col = .GetColFromID("ujudglt1") : .Text = r_dt.Rows(ix - 1).Item("ujudglt1").ToString().Trim   '50
                    .Col = .GetColFromID("ujudglt2") : .Text = r_dt.Rows(ix - 1).Item("ujudglt2").ToString().Trim    '51
                    .Col = .GetColFromID("ujudglt3") : .Text = r_dt.Rows(ix - 1).Item("ujudglt3").ToString().Trim   '52
                    .Col = .GetColFromID("refgbn") : .Text = r_dt.Rows(ix - 1).Item("refgbn").ToString().Trim      '53
                    .Col = .GetColFromID("refls") : .Text = r_dt.Rows(ix - 1).Item("refls").ToString().Trim     '54
                    .Col = .GetColFromID("refhs") : .Text = r_dt.Rows(ix - 1).Item("refhs").ToString().Trim          '55
                    .Col = .GetColFromID("refl") : .Text = r_dt.Rows(ix - 1).Item("refl").ToString().Trim           '56
                    .Col = .GetColFromID("refh") : .Text = r_dt.Rows(ix - 1).Item("refh").ToString().Trim           '57
                    .Col = .GetColFromID("alimitgbn") : .Text = r_dt.Rows(ix - 1).Item("alimitgbn").ToString().Trim '58
                    .Col = .GetColFromID("alimitl") : .Text = r_dt.Rows(ix - 1).Item("alimitl").ToString().Trim      '59
                    .Col = .GetColFromID("alimitls") : .Text = r_dt.Rows(ix - 1).Item("alimitls").ToString().Trim   '60
                    .Col = .GetColFromID("alimith") : .Text = r_dt.Rows(ix - 1).Item("alimith").ToString().Trim    '61
                    .Col = .GetColFromID("alimiths") : .Text = r_dt.Rows(ix - 1).Item("alimiths").ToString().Trim    '62
                    .Col = .GetColFromID("panicgbn") : .Text = r_dt.Rows(ix - 1).Item("panicgbn").ToString().Trim  '63
                    .Col = .GetColFromID("panicl") : .Text = r_dt.Rows(ix - 1).Item("panicl").ToString().Trim      '64
                    .Col = .GetColFromID("panich") : .Text = r_dt.Rows(ix - 1).Item("panich").ToString().Trim      '65
                    .Col = .GetColFromID("criticalgbn") : .Text = r_dt.Rows(ix - 1).Item("criticalgbn").ToString().Trim  '66
                    .Col = .GetColFromID("criticall") : .Text = r_dt.Rows(ix - 1).Item("criticall").ToString().Trim     '67
                    .Col = .GetColFromID("criticalh") : .Text = r_dt.Rows(ix - 1).Item("criticalh").ToString().Trim      '68
                    .Col = .GetColFromID("alertgbn") : .Text = r_dt.Rows(ix - 1).Item("alertgbn").ToString().Trim      '69
                    .Col = .GetColFromID("alertl") : .Text = r_dt.Rows(ix - 1).Item("alertl").ToString().Trim           '70
                    .Col = .GetColFromID("alerth") : .Text = r_dt.Rows(ix - 1).Item("alerth").ToString().Trim           '71
                    .Col = .GetColFromID("deltagbn") : .Text = r_dt.Rows(ix - 1).Item("deltagbn").ToString().Trim       '72
                    .Col = .GetColFromID("deltal") : .Text = r_dt.Rows(ix - 1).Item("deltal").ToString().Trim          '73
                    .Col = .GetColFromID("deltah") : .Text = r_dt.Rows(ix - 1).Item("deltah").ToString().Trim          '74
                    .Col = .GetColFromID("deltaday") : .Text = r_dt.Rows(ix - 1).Item("deltaday").ToString().Trim        '75
                    .Col = .GetColFromID("bfbcno1") : .Text = r_dt.Rows(ix - 1).Item("bfbcno1").ToString().Trim       '76
                    .Col = .GetColFromID("bforgrst1") : .Text = r_dt.Rows(ix - 1).Item("bforgrst1").ToString().Trim    '77
                    .Col = .GetColFromID("bfviewrst1") : .Text = r_dt.Rows(ix - 1).Item("bfviewrst1").ToString().Trim   '78
                    .Col = .GetColFromID("bffndt1") : .Text = r_dt.Rows(ix - 1).Item("bffndt1").ToString().Trim         '79
                    .Col = .GetColFromID("bfbcno2") : .Text = r_dt.Rows(ix - 1).Item("bfbcno2").ToString().Trim        '24
                    .Col = .GetColFromID("bfviewrst2") : .Text = r_dt.Rows(ix - 1).Item("bfviewrst2").ToString().Trim  '25
                    .Col = .GetColFromID("bffndt2") : .Text = r_dt.Rows(ix - 1).Item("bffndt2").ToString().Trim          '26
                    .Col = .GetColFromID("eqcd") : .Text = r_dt.Rows(ix - 1).Item("eqcd").ToString().Trim                '21
                    .Col = .GetColFromID("eqnm") : .Text = r_dt.Rows(ix - 1).Item("eqnm").ToString().Trim             '22
                    .Col = .GetColFromID("eqbcno") : .Text = r_dt.Rows(ix - 1).Item("eqbcno").ToString().Trim          '23
                    .Col = .GetColFromID("tnmp") : .Text = r_dt.Rows(ix - 1).Item("tnmp").ToString().Trim          '80
                    .Col = .GetColFromID("tordcd") : .Text = r_dt.Rows(ix - 1).Item("tordcd").ToString().Trim          '29
                    .Col = .GetColFromID("calcgbn") : .Text = r_dt.Rows(ix - 1).Item("calcgbn").ToString().Trim         '85
                    .Col = .GetColFromID("viwsub") : .Text = r_dt.Rows(ix - 1).Item("viwsub").ToString().Trim       '86
                    .Col = .GetColFromID("rerunrst") : .Text = r_dt.Rows(ix - 1).Item("rerunrst").ToString().Trim
                    .Col = .GetColFromID("cfmnm") : .Text = r_dt.Rows(ix - 1).Item("cfmnm").ToString().Trim

                    .Col = .GetColFromID("mbttype") : .Text = r_dt.Rows(ix - 1).Item("mbttype").ToString.Trim

                    If r_dt.Rows(ix - 1).Item("mbttype").ToString = "2" Then
                        miMbtTypeFlag = Convert.ToInt32(Val(r_dt.Rows(ix - 1).Item("mbttype").ToString))
                        .Col = .GetColFromID("tnmd") : .BackColor = Drawing.Color.FromArgb(192, 255, 192)
                    End If

                    .Col = .GetColFromID("reftxt") : .Text = r_dt.Rows(ix - 1).Item("reftxt").ToString().Trim            '11
                    If r_dt.Rows(ix - 1).Item("reftxt").ToString() <> "" Then
                        .CellNoteIndicator = FPSpreadADO.CellNoteIndicatorConstants.CellNoteIndicatorShowAndFireEvent
                    End If
                    .Col = .GetColFromID("rstflg") : .Text = r_dt.Rows(ix - 1).Item("rstflg").ToString().Trim         '31
                    .Col = .GetColFromID("rstflgmark")                                                                 '18
                    Select Case r_dt.Rows(ix - 1).Item("rstflg").ToString().Trim
                        Case "3"    ' 최종결과 표시
                            .ForeColor = Drawing.Color.DarkGreen
                            .Text = "◆"

                        Case "2"    ' 중간보고 표시
                            .Text = "○"
                            rbRstflgNotFN = True
                        Case "1"
                            .Text = "△"
                            rbRstflgNotFN = True
                        Case Else
                            .Text = ""
                    End Select
                    .Col = .GetColFromID("tcdgbn") : .Text = r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim            '44

                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""

                        If r_dt.Rows(ix - 1).Item("viwsub").ToString.Trim <> "1" And _
                           r_dt.Rows(ix - 1).Item("orgrst").ToString.Trim = "" And r_dt.Rows(ix - 1).Item("bforgrst1").ToString.Trim = "" Then
                            .Row = ix
                            .RowHidden = True
                        End If
                    End If

                    .Col = .GetColFromID("titleyn") : .Text = r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim     '43
                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" And r_dt.Rows(ix - 1).Item("mbttype").ToString().Trim <> "2" Then
                        .Col = .GetColFromID("orgrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True

                        .Col = .GetColFromID("viewrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True

                        .Col = .GetColFromID("rerunrst")
                        .BackColor = Drawing.Color.LightGray
                        .ForeColor = Drawing.Color.LightGray
                        .Lock = True
                    End If

                    If r_dt.Rows(ix - 1).Item("titleyn").ToString().Trim = "1" And r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "B" Then
                        .Col = .GetColFromID("chk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Text = ""
                    End If

                    .Col = .GetColFromID("hlmark") : .Text = r_dt.Rows(ix - 1).Item("hlmark").ToString().Trim    '13
                    If r_dt.Rows(ix - 1).Item("hlmark").ToString().Trim = "L" Then
                        .BackColor = Color.FromArgb(221, 240, 255)
                        .ForeColor = Color.FromArgb(0, 0, 255)
                    ElseIf r_dt.Rows(ix - 1).Item("hlmark").ToString().Trim = "H" Then
                        .BackColor = Color.FromArgb(255, 230, 231)
                        .ForeColor = Color.FromArgb(255, 0, 0)
                    End If

                    .Col = .GetColFromID("panicmark") : .Text = r_dt.Rows(ix - 1).Item("panicmark").ToString().Trim  '14
                    If r_dt.Rows(ix - 1).Item("panicmark").ToString() = "P" Then
                        .BackColor = Color.FromArgb(150, 150, 255)
                        .ForeColor = Color.FromArgb(255, 255, 255)
                    End If

                    .Col = .GetColFromID("deltamark") : .Text = r_dt.Rows(ix - 1).Item("deltamark").ToString().Trim '15
                    If r_dt.Rows(ix - 1).Item("deltamark").ToString().Trim = "D" Then
                        .BackColor = Color.FromArgb(150, 255, 150)
                        .ForeColor = Color.FromArgb(0, 128, 64)
                    End If

                    .Col = .GetColFromID("criticalmark") : .Text = r_dt.Rows(ix - 1).Item("criticalmark").ToString().Trim   '16
                    If r_dt.Rows(ix - 1).Item("criticalmark").ToString().Trim = "C" Then
                        .BackColor = Color.FromArgb(255, 150, 255)
                        .ForeColor = Color.FromArgb(255, 255, 255)
                    End If

                    .Col = .GetColFromID("alertmark") : .Text = r_dt.Rows(ix - 1).Item("alertmark").ToString().Trim          '17
                    If r_dt.Rows(ix - 1).Item("alertmark").ToString().Trim <> "" Then
                        .BackColor = Color.FromArgb(255, 255, 150)
                        .ForeColor = Color.FromArgb(0, 0, 0)
                    End If

                    .Col = .GetColFromID("tnmd")                                                                            '3
                    If r_dt.Rows(ix - 1).Item("tcdgbn").ToString().Trim = "C" Then
                        If r_dt.Rows(ix - 1).Item("tclscd").ToString.Trim = r_dt.Rows(ix - 1).Item("testcd").ToString.Substring(1, 5) Then
                            .Text = "... " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                        Else
                            .Text = ".... " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                        End If
                    ElseIf r_dt.Rows(ix - 1).Item("tclscd").ToString.Trim = r_dt.Rows(ix - 1).Item("testcd").ToString.Trim Or _
                           r_dt.Rows(ix - 1).Item("tcdgbn").ToString.Trim = "B" Then
                        .Text = r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                    Else
                        .Text = ". " + r_dt.Rows(ix - 1).Item("tnmd").ToString().Trim
                    End If

                    .Col = .GetColFromID("cvtfldgbn") : .Text = r_dt.Rows(ix - 1).Item("cvtfldgbn").ToString().Trim         '84
                    If r_dt.Rows(ix - 1).Item("cvtfldgbn").ToString().Trim <> "" Then                                        '9
                        .Col = .GetColFromID("cvtgbn") : .Text = "C"
                    Else
                        .Col = .GetColFromID("cvtgbn") : .Text = ""
                    End If

                    .Col = .GetColFromID("reftcls")                                                                         '10
                    If r_dt.Rows(ix - 1).Item("reftcls").ToString().Trim = "1" Then
                        .Col = .GetColFromID("reftcls") : .Text = "☞"
                    Else
                        .Col = .GetColFromID("reftcls") : .Text = ""
                    End If

                    .Col = .GetColFromID("rstno") : .Text = r_dt.Rows(ix - 1).Item("rstno").ToString().Trim                  '6

                    If r_dt.Rows(ix - 1).Item("rstno").ToString().Trim > "1" Then                                            '8
                        .Col = .GetColFromID("history")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                        .TypePictStretch = False
                        .TypePictMaintainScale = False
                        .TypePictPicture = GetImgList.getMultiRst()
                    End If

                    .Col = .GetColFromID("orgrst")                                                                          '4
                    .Text = r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim
                    .Col = .GetColFromID("corgrst") : .Text = r_dt.Rows(ix - 1).Item("orgrst").ToString().Trim               '81

                    If r_dt.Rows(ix - 1).Item("bcno").ToString.Trim <> msBcNo Or (LOGIN.PRG_CONST.RST_BCNO_EXE = "0" And msPartSlip <> "" And r_dt.Rows(ix - 1).Item("slipcd").ToString <> msPartSlip) Then
                        .Col = .GetColFromID("orgrst")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .BackColor = Color.Silver
                        .ForeColor = Color.Silver

                        .Col = .GetColFromID("chk") : .Lock = True
                    End If

                    .Col = .GetColFromID("viewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim              '5
                    .Col = .GetColFromID("cviewrst") : .Text = r_dt.Rows(ix - 1).Item("viewrst").ToString().Trim            '82

                    .Col = .GetColFromID("rstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim                     '19
                    .Col = .GetColFromID("crstcmt") : .Text = r_dt.Rows(ix - 1).Item("rstcmt").ToString().Trim                    '83

                    .Col = .GetColFromID("eqflag") : .Text = r_dt.Rows(ix - 1).Item("eqflag").ToString().Trim              '20

                    .Row = ix
                    If r_dt.Rows(ix - 1).Item("rstflg").ToString.Trim = "1" Or r_dt.Rows(ix - 1).Item("rstflg").ToString.Trim = "2" Then
                        .Col = .GetColFromID("chk")
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox And .Lock = False Then
                            .Text = "1"
                        ElseIf r_dt.Rows(ix - 1).Item("tcdgbn").ToString.Trim = "C" Then
                            .Col = .GetColFromID("iud") : .Text = "1"
                        End If
                    End If

                    If r_dt.Rows(ix - 1).Item("viewrst").ToString.IndexOf(vbCr) >= 0 Then
                        If r_dt.Rows(ix - 1).Item("viewrst").ToString.IndexOf(vbCr) >= 0 Then
                            Dim sBuf() As String = r_dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))
                            .set_RowHeight(.MaxRows, m_dbl_RowHeightt * sBuf.Length)
                        End If
                    End If
                Next
                .ReDraw = True

            End With

        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)

        Finally
            Me.spdResult.ReDraw = True
        End Try
    End Sub


    Private Sub sbDisplay_CommentView(ByVal r_dt As DataTable)
        Me.txtCmtCont.Text = ""

        For intIdx As Integer = 0 To r_dt.Rows.Count - 1
            Me.txtCmtCont.Text += r_dt.Rows(intIdx).Item(11).ToString + Chr(13) + Chr(10)
        Next
        Me.txtCmtCont.Tag = Me.txtCmtCont.Text

        If txtCmtCont.Text.Replace(Chr(13), "").Replace(Chr(10), "").Trim = "" Then
            Me.txtCmtCont.Text = ""
            Me.txtCmtCont.Tag = ""
        End If

    End Sub

    Private Sub spdResult_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdResult.ButtonClicked

        If e.row < 1 Then Exit Sub

        Dim strBcNo As String = ""
        Dim sTestCd_p As String = ""

        Dim strTBcNo As String = ""
        Dim sTestCd As String = ""

        With spdResult
            If e.col = .GetColFromID("chk") Then
                .Row = e.row
                .Col = e.col
                If .Text = "1" Then
                    .Col = .GetColFromID("iud") : .Text = "1"
                    .Col = .GetColFromID("tcdgbn")
                    If .Text = "P" Then
                        .Col = .GetColFromID("testcd") : sTestCd_p = .Text : If sTestCd_p <> "" Then sTestCd_p = sTestCd_p.Substring(0, 5)
                        .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")


                        For intRow As Integer = e.row + 1 To .MaxRows
                            .Row = intRow
                            .Col = .GetColFromID("testcd") : sTestCd = .Text : If sTestCd <> "" Then sTestCd = sTestCd.Substring(0, 5)
                            .Col = .GetColFromID("bcno") : strTBcNo = .Text.Replace("-", "")

                            If sTestCd_p = sTestCd And strBcNo = strTBcNo Then
                                .Col = .GetColFromID("iud") : .Text = "1"
                            End If
                        Next
                    End If
                Else
                    .Col = .GetColFromID("iud") : .Text = ""
                    .Col = .GetColFromID("tcdgbn")
                    If .Text = "P" Then
                        .Col = .GetColFromID("testcd") : sTestCd_p = .Text : If sTestCd_p <> "" Then sTestCd_p = sTestCd_p.Substring(0, 5)
                        .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")

                        For intRow As Integer = e.row + 1 To .MaxRows
                            .Row = intRow

                            .Col = .GetColFromID("testcd") : sTestCd = .Text : If sTestCd <> "" Then sTestCd = sTestCd.Substring(0, 5)
                            .Col = .GetColFromID("bcno") : strTBcNo = .Text.Replace("-", "")

                            If sTestCd_p = sTestCd And strBcNo = strTBcNo Then
                                .Col = .GetColFromID("iud") : .Text = ""
                            End If
                        Next
                    End If
                End If
            End If

        End With
    End Sub

    Public Sub sbDisplay_RegNm_Test(ByVal rsTestCd As String)
        Dim sFn As String = "Sub sbDisplay_RegNm_Test()"

        Try
            Dim sID As String = ""
            Dim sNM As String = ""
            Dim sDT As String = ""

            '결과저장, 중간보고, 최종보고
            Me.lblReg.Text = ""
            Me.lblMW.Text = ""
            Me.lblFN.Text = ""
            Me.lblCfm.Text = ""

            Dim a_dr As DataRow()

            a_dr = m_dt_RstUsr.Select("testcd = '" + rsTestCd + "'")

            If a_dr.Length < 1 Then Return

            Dim sRstflg As String = a_dr(0).Item("rstflg").ToString().Trim

            If sRstflg = "" Then Return

            For i As Integer = 1 To Convert.ToInt32(sRstflg)
                If i = 1 Then
                    sID = a_dr(0).Item("regid").ToString().Trim
                    sNM = a_dr(0).Item("regnm").ToString().Trim
                    sDT = a_dr(0).Item("regdt").ToString().Trim

                    If Not sID + sNM + sDT = "" Then
                        Me.lblReg.Text = sDT + " / " + sNM
                    End If
                ElseIf i = 2 Then
                    sID = a_dr(0).Item("mwid").ToString().Trim
                    sNM = a_dr(0).Item("mwnm").ToString().Trim
                    sDT = a_dr(0).Item("mwdt").ToString().Trim

                    If Not sID + sNM + sDT = "" Then
                        Me.lblMW.Text = sDT + " / " + sNM
                    End If
                ElseIf i = 3 Then
                    sID = a_dr(0).Item("fnid").ToString().Trim
                    sNM = a_dr(0).Item("fnnm").ToString().Trim
                    sDT = a_dr(0).Item("fndt").ToString().Trim

                    If Not sID + sNM + sDT = "" Then
                        Me.lblFN.Text = sDT + " / " + sNM
                        Me.lblCfm.Text = a_dr(0).Item("cfmnm").ToString().Trim
                    End If
                End If
            Next

            Select Case sRstflg
                Case "1"
                    Me.lblSampleStatus.Text = "결과저장"
                Case "2"
                    Me.lblSampleStatus.Text = "중간보고"
                Case "3"
                    Me.lblSampleStatus.Text = "최종보고"
            End Select

            If Me.lblSampleStatus.Text = "최종보고" Then
                Me.lblSampleStatus.BackColor = Drawing.Color.FromArgb(128, 128, 255)
                Me.lblSampleStatus.ForeColor = Drawing.Color.White
            Else
                Me.lblSampleStatus.BackColor = Drawing.Color.FromArgb(255, 192, 128)
                Me.lblSampleStatus.ForeColor = Drawing.Color.Black
            End If

        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)
        End Try
    End Sub

    Private Sub chkSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelect.Click

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    If chkSelect.Checked Then
                        .Col = .GetColFromID("testcd") : Dim sTestCd_p As String = .Text
                        .Col = .GetColFromID("tcdgbn") : Dim strTcdGbn As String = .Text
                        .Col = .GetColFromID("rstflg") : Dim sRstflg As String = .Text : If sRstflg = "0" Then sRstflg = ""
                        .Col = .GetColFromID("orgrst")

                        If .Text = "" And strTcdGbn = "P" Then
                            For intIdx As Integer = intRow + 1 To .MaxRows
                                .Row = intIdx
                                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                                .Col = .GetColFromID("orgrst")
                                If .Text <> "" And sTestCd.StartsWith(sTestCd_p) Then
                                    .Row = intRow
                                    .Col = .GetColFromID("chk") : .Text = "1"
                                    Exit For
                                End If
                            Next
                        Else
                            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then
                                .Col = .GetColFromID("chk") : .Text = "1"
                            End If
                        End If
                    Else
                        .Col = .GetColFromID("chk") : .Text = ""
                    End If
                End If
            Next
        End With

    End Sub

    Private Sub spdResult_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdResult.ClickEvent

        Dim sBcNo As String = ""
        Dim sTnmd As String = ""
        Dim sTestCd As String = ""
        Dim sSpcCd As String = ""
        Dim sTCdGbn As String = ""

        Me.btnExmAdd.Enabled = False
        Me.mnuSpRst.Visible = False
        Me.mnuKeypad.Visible = False

        With spdResult
            .Row = e.row
            .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
            .Col = .GetColFromID("testcd") : Me.txtTestCd.Text = .Text
            .Col = .GetColFromID("spccd") : sSpcCd = .Text

            RaiseEvent ChangedTestCd(sBcNo, sTestCd)

            Dim sSpRstYn As String = LISAPP.COMM.RstFn.fnGet_SpRst_yn(msBcNo, Me.txtTestCd.Text.Substring(0, 5))
            Dim sFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(Me.txtTestCd.Text.Substring(0, 5), sSpcCd)

            If sSpRstYn <> "" Then mnuSpRst.Visible = True
            If sFormGbn <> "" Then mnuKeypad.Visible = True

            If e.col = .GetColFromID("orgrst") And e.row > 0 Then
                .Row = e.row

                .Col = .GetColFromID("tcdgbn") : sTCdGbn = .Text
                If sTCdGbn = "P" Or sTCdGbn = "C" Then
                    .Col = .GetColFromID("testcd") : sTestCd = .Text.Substring(0, 5)
                    sbDisplay_ExmAdd(sTestCd)
                End If

                .Row = e.row
                .Col = .GetColFromID("orgrst")
                If .Text.Trim = "{null}" Then
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("tnmd") : sTnmd = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text

                    Dim objStRst As New AxAckResultViewer.STRST01

                    objStRst.SpecialTestName = sTnmd
                    objStRst.BcNo = sBcNo
                    objStRst.TestCd = sTestCd

                    objStRst.Left = CType(moForm.ParentForm.Left + (moForm.ParentForm.Width - objStRst.Width) / 2, Integer)
                    objStRst.Top = moForm.ParentForm.Top + Ctrl.menuHeight

                    objStRst.ShowDialog(moForm)
                Else
                    '-- 2008-12-24 Yej Add
                    .Col = .GetColFromID("mbttype") : Dim sMbTType As String = .Text
                    .Col = .GetColFromID("titleyn") : Dim sTitleYN As String = .Text
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("tnmd") : sTnmd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text

                    'If (sTCdGbn = "S" Or sTCdGbn = "P") And sMbTType = "2" Then
                    If sMbTType = "2" Then
                        Me.lblTestCd.Text = sTestCd.Substring(0, 5)
                        Me.lblTnmd.Text = sTnmd
                        Me.lblSpccd.Text = sSpcCd

                    Else
                        Me.lblTestCd.Text = ""
                        Me.lblTnmd.Text = ""
                        Me.lblSpccd.Text = ""
                    End If

                    sbDisplay_BcNo_Rst_Bac_One_testcd(Me.lblTestCd.Text)
                    If spdBac.MaxRows > 0 Then

                        Dim sBacCd As String = Ctrl.Get_Code(Me.spdBac, "baccd", 1)
                        Dim sBacSeq As String = Ctrl.Get_Code(Me.spdBac, "bacseq", 1)

                        sTestCd = Ctrl.Get_Code(Me.spdBac, "testcd", 1)

                        sbDisplay_BcNo_Rst_Anti_One_Bac(sTestCd, sBacCd, sBacSeq)
                    End If
                    '-- 2008-12-24 End
                End If
            ElseIf e.col = .GetColFromID("history") And e.row > 0 Then

                .Row = e.row
                .Col = .GetColFromID("rstno")
                If .Text >= "1" Then
                    Dim objForm As New FGHISTORY
                    Dim aryRst As New ArrayList

                    .Row = e.row
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")

                    aryRst = objForm.Display_Data(moForm, sBcNo)

                    If aryRst.Count > 0 Then
                        For intIdx As Integer = 0 To aryRst.Count - 1
                            For intRow = 1 To .MaxRows
                                .Row = intRow
                                .Col = .GetColFromID("testcd")
                                If .Text = CType(aryRst.Item(intIdx), RST_INFO).TestCd Then
                                    .Col = .GetColFromID("orgrst") : .Text = CType(aryRst.Item(intIdx), RST_INFO).OrgRst
                                    .Col = .GetColFromID("viewrst") : .Text = CType(aryRst.Item(intIdx), RST_INFO).OrgRst
                                    sbSet_ResultView(intRow)
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If
            ElseIf e.col = .GetColFromID("cvtgbn") And e.row > 0 Then
                .Row = e.row
                .Col = .GetColFromID("cvtgbn")
                If .Text = "☞" Then

                End If
            ElseIf e.col = .GetColFromID("reftcls") And e.row > 0 Then
                .Row = e.row
                .Col = .GetColFromID("reftcls")
                If .Text = "☞" Then
                    .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("spccd") : sSpcCd = .Text

                    Dim objForm As New FGRST_REF
                    objForm.Display_Data(moForm, sBcNo, sTestCd, sSpcCd)
                End If
            End If
        End With

        sTestCd = Ctrl.Get_Code(Me.spdResult, "testcd", e.row)
        sbDisplay_RegNm_Test(sTestCd)

    End Sub

    Private Sub spdResult_ComboCloseUp(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ComboCloseUpEvent) Handles spdResult.ComboCloseUp

    End Sub

    Private Sub spdResult_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdResult.DblClick
        If e.row < 1 Or e.col <> spdResult.GetColFromID("viewrst") Then Return

        With spdResult
            Dim iRow As Integer = e.row

            .Row = iRow
            .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text
            .Col = .GetColFromID("orgrst") : Dim sOrgRst As String = .Text

            If .CellType <> FPSpreadADO.CellTypeConstants.CellTypeEdit Then Return

            Dim frm As New FGMULTILLINERST
            Dim sRetVal As String = frm.Display_Result(sTestcd, sOrgRst)

            If sRetVal <> "" Then
                .Row = iRow
                .Col = .GetColFromID("orgrst") : .Text = sRetVal

                Dim sBuf() As String = sRetVal.Split(Chr(13))
                .set_RowHeight(.ActiveRow, m_dbl_RowHeightt * sBuf.Length)

                .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                .ForeColor = Color.Black
                .Focus()
            End If
        End With
    End Sub

    Private Sub spdResult_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdResult.GotFocus
        With spdResult
            If .ActiveCol <> .GetColFromID("orgrst") Then Return

            .Row = .ActiveRow
            .Col = .GetColFromID("orgrst") : .ForeColor = Color.Black
        End With

    End Sub

    Private Sub spdResult_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdResult.KeyDownEvent
        Dim sFn As String = "Sub spdResult_KeyDownEvent(Object, AxFPSpreadADO._DSpreadEvents_KeyDownEvent)"
        Try
            Dim sRst As String = ""
            Dim sBcNo As String = ""
            Dim sTestCd As String = ""
            Dim sTCdGbn As String = ""

            Select Case Convert.ToInt32(e.keyCode)
                Case Keys.PageUp, Keys.PageDown
                    e.keyCode = 0
                Case Keys.Tab
                    With spdBac
                        If .MaxRows < 1 Then Return

                        .Row = 1
                        .Col = .GetColFromID("incrst") : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                        spdBac.Focus()
                    End With

                Case 37, 39, 229, 27 ' 화살표 키
                    lstCode.Items.Clear()
                    lstCode.Hide()
                    pnlCode.Visible = False

                Case 38 ' 방향 위키
                    If lstCode.Visible = True Then
                        If lstCode.SelectedIndex > -1 Then
                            If lstCode.SelectedIndex > 0 Then
                                lstCode.SelectedIndex -= 1
                            End If
                        Else
                            lstCode.SelectedIndex = lstCode.Items.Count - 1
                        End If
                        e.keyCode = 0
                    End If


                Case 40 ' 방향 아래키
                    If lstCode.Visible = True Then
                        If lstCode.SelectedIndex > -1 Then
                            If lstCode.Items.Count - 1 > lstCode.SelectedIndex Then
                                lstCode.SelectedIndex += 1
                            End If
                        Else
                            lstCode.SelectedIndex = 0
                        End If
                        e.keyCode = 0
                    End If

                Case 13             ' Enter키
                    With spdResult
                        Dim iRow As Integer = .ActiveRow
                        Dim sOrgRst As String = ""
                        Dim sViewRst As String = ""
                        Dim sMbtType As String = ""

                        .Row = iRow
                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "`")
                        .Col = .GetColFromID("orgrst") : sRst = .Text.Replace("'", "`") : .Text = sRst
                        .Col = .GetColFromID("testcd") : sTestCd = .Text
                        .Col = .GetColFromID("tcdgbn") : sTCdGbn = .Text
                        .Col = .GetColFromID("mbttype") : sMbtType = .Text
                        .Col = .GetColFromID("viewrst") : sViewRst = .Text

                        If sMbtType = "2" Then Me.lblTestCd.Text = sTestCd.Substring(0, 5)

                        If sMbtType = "2" And (sTCdGbn = "P" Or sTCdGbn = "S") Then
                            Me.lblTestCd.Text = sTestCd.Substring(0, 5)

                            sbDisplay_Spd_BacCd(sRst)

                            .Row = iRow
                            .Col = .GetColFromID("orgrst") : .Text = ""

                            If Me.spdBac.MaxRows > 0 Then Me.spdBac.Focus()
                        Else
                            If Me.lstCode.Visible Then
                                If lstCode.SelectedIndex >= 0 Then
                                    sRst = Me.lstCode.Text.Split(Chr(9))(1)
                                    .Col = .GetColFromID("orgrst") : .Text = sRst
                                End If
                            End If
                            .Col = .GetColFromID("viewrst") : .Text = sRst

                            sbSet_ResultView(iRow)
                            sbGet_Calc_Rst(iRow)                '-- 결과 계산
                            sbGet_CvtRstInfo(sBcNo, sTestCd)    '-- 결과값 자동변환
                        End If

                    End With

                    lstCode.Items.Clear()
                    lstCode.Hide()
                    pnlCode.Visible = False

                Case Else
                    RaiseEvent FunctionKeyDown(sender, New System.Windows.Forms.KeyEventArgs(CType(e.keyCode, System.Windows.Forms.Keys)))
            End Select
        Catch ex As Exception
            sbLog_Exception(sFn + " : " + ex.Message)
        End Try
    End Sub

    ' 결과유형 체크
    Private Function fnRstTypeCheck(ByVal riRow As Integer, ByVal rsRst As String) As String

        Dim strRstLLen As String = ""
        Dim strRstULen As String = ""
        Dim strRstType As String = ""
        Dim strCutOpt As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("rsttype") : strRstType = .Text
            .Col = .GetColFromID("rstllen") : strRstLLen = .Text
            .Col = .GetColFromID("rstulen") : strRstULen = .Text
            .Col = .GetColFromID("cutopt") : strCutOpt = .Text

            If (strRstType = "0" Or strRstType = "1") And strRstLLen <> "" And rsRst <> "" And IsNumeric(rsRst) Then
                Dim intPos As Integer
                intPos = InStr(rsRst, ".")

                If Val(strRstLLen) >= 0 Then
                    .Col = .GetColFromID("rsttype")

                    Dim strDecimal As String = "0"
                    Dim intDecimal As Integer = CInt(strRstLLen)
                    If intDecimal > 0 Then
                        strDecimal = strDecimal & "." & New String(Chr(Asc("0")), intDecimal)
                    End If

                    Select Case strCutOpt
                        Case "0", "3"   ' 0 : 반올림처리없음(입력그대로). 3 : 내림
                            If intPos > 0 Then
                                If Len(rsRst) >= intPos + intDecimal Then
                                    rsRst = Mid(rsRst, 1, intPos + intDecimal)
                                End If
                            End If
                        Case "1"    ' 1 : 올림
                            If intPos > 0 Then
                                If Len(rsRst) >= intPos + intDecimal Then
                                    Dim strRstTmp As String
                                    strRstTmp = Mid(rsRst, 1, intPos + intDecimal)
                                    If Len(rsRst) >= intPos + intDecimal + 1 Then
                                        If Mid(rsRst, intPos + intDecimal + 1, 1) > "0" Then
                                            strRstTmp += "9"
                                        End If
                                    End If
                                    rsRst = strRstTmp
                                End If
                            End If
                        Case "2"    ' 2 : 반올림
                    End Select

                    rsRst = Format(Val(rsRst), strDecimal).ToString
                End If

                If Val(strRstULen) > 0 Then
                    If CInt(strRstULen) < intPos - 1 Then
                        Dim sMsg As String = "결과정수크기" & strRstULen & " 보다 큰 값이 입력되었습니다."
                        If mbBatchMode Then
                        Else
                            MsgBox(sMsg, MsgBoxStyle.Information)
                        End If
                    End If
                End If
            End If

            If strRstType = "1" And rsRst <> "" And IsNumeric(rsRst) = False Then
                Dim sMsg As String = "숫자 결과만 입력할 수 있습니다."
                If mbBatchMode Then
                Else
                    MsgBox(sMsg, MsgBoxStyle.Information)
                End If
            End If
        End With

        fnRstTypeCheck = rsRst

    End Function

    ' 결과유형 체크
    Private Sub sbRstTypeCheck(ByVal riRow As Integer)

        Dim strRstLLen As String = ""
        Dim strRstULen As String = ""
        Dim strRstType As String = ""
        Dim strCutOpt As String = ""
        Dim strRst As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : strRst = .Text
            .Col = .GetColFromID("rsttype") : strRstType = .Text
            .Col = .GetColFromID("rstllen") : strRstLLen = .Text
            .Col = .GetColFromID("rstulen") : strRstULen = .Text
            .Col = .GetColFromID("cutopt") : strCutOpt = .Text

            If (strRstType = "0" Or strRstType = "1") And strRstLLen <> "" And strRst <> "" And IsNumeric(strRst) Then
                Dim intPos As Integer
                intPos = InStr(strRst, ".")

                If Val(strRstLLen) >= 0 Then
                    .Col = .GetColFromID("rsttype")

                    Dim strDecimal As String = "0"
                    Dim intDecimal As Integer = CInt(strRstLLen)
                    If intDecimal > 0 Then
                        strDecimal = strDecimal & "." & New String(Chr(Asc("0")), intDecimal)
                    End If

                    Select Case strCutOpt
                        Case "0", "3"   ' 0 : 반올림처리없음(입력그대로). 3 : 내림
                            If intPos > 0 Then
                                If Len(strRst) >= intPos + intDecimal Then
                                    strRst = Mid(strRst, 1, intPos + intDecimal)
                                End If
                            End If
                        Case "1"    ' 1 : 올림
                            If intPos > 0 Then
                                If Len(strRst) >= intPos + intDecimal Then
                                    Dim strRstTmp As String
                                    strRstTmp = Mid(strRst, 1, intPos + intDecimal)
                                    If Len(strRst) >= intPos + intDecimal + 1 Then
                                        If Mid(strRst, intPos + intDecimal + 1, 1) > "0" Then
                                            strRstTmp += "9"
                                        End If
                                    End If
                                    strRst = strRstTmp
                                End If
                            End If
                        Case "2"    ' 2 : 반올림

                    End Select
                    .Col = .GetColFromID("viewrst") : .Text = Format(Val(strRst), strDecimal).ToString
                End If

                If Val(strRstULen) > 0 Then
                    If CInt(strRstULen) < intPos - 1 Then
                        Dim sMsg As String = "결과정수크기" & strRstULen & " 보다 큰 값이 입력되었습니다."
                        If mbBatchMode Then
                            '.Col = .GetColFromID("rstcmt") : .Text = sMsg

                        Else
                            MsgBox(sMsg, MsgBoxStyle.Information)
                        End If

                        .Col = .GetColFromID("orgrst") : .Text = ""
                        .Col = .GetColFromID("viewrst") : .Text = ""
                    End If
                End If
            End If

            If strRstType = "1" And strRst <> "" And IsNumeric(strRst) = False Then
                Dim sMsg As String = "숫자 결과만 입력할 수 있습니다."
                If mbBatchMode Then
                    '.SetText(.GetColFromID("Comment"), iActiveRow, sMsg)
                Else
                    MsgBox(sMsg, MsgBoxStyle.Information)
                End If
            End If
        End With
    End Sub

    Private Sub sbUJudgCheck(ByVal riRow As Integer)
        Dim sRefL As String
        Dim sRefH As String
        Dim sRefHs As String
        Dim sRefLs As String

        Dim sJudgType As String = ""

        Dim sRefGbn As String = ""
        Dim sHLmark As String = ""
        Dim sRst As String = "", sViewRst As String = "", sOrgRst As String = "", sMark As String = ""
        Dim sUJRst As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("refgbn") : sRefGbn = .Text

            If sRefGbn = "2" Or sRefGbn = "1" Then
                .Col = .GetColFromID("judgtype") : sJudgType = .Text.Trim
                Select Case Len(sJudgType)
                    Case 6
                        .Col = .GetColFromID("refh") : sRefH = .Text
                        .Col = .GetColFromID("refhs") : sRefHs = .Text

                        If sRefH = "" Then
                            .Col = .GetColFromID("refl") : sRefH = .Text
                            .Col = .GetColFromID("refls") : sRefHs = .Text
                        End If

                        If sRefH = "" Then Return
                        .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                        .Col = .GetColFromID("viewrst") : sViewRst = .Text

                        If sOrgRst = "" Then Return


                        If sOrgRst.Trim.StartsWith(">=") Or sOrgRst.Trim.StartsWith("<=") Then
                            sMark = sOrgRst.Substring(0, 2)
                            sRst = sOrgRst.Substring(2).Trim
                        ElseIf sOrgRst.Trim.StartsWith(">") Or sOrgRst.Trim.StartsWith("<") Then
                            sMark = sOrgRst.Substring(0, 1)
                            sRst = sOrgRst.Substring(1).Trim
                        Else
                            sRst = sOrgRst
                        End If

                        If IsNumeric(sRst) = False Then Return

                        Select Case sRefHs
                            Case "0"
                                If Val(sRst) > Val(sRefH) Then
                                    sHLmark = "H"
                                End If
                            Case "1"
                                If Val(sRst) >= Val(sRefH) Then
                                    sHLmark = "H"
                                End If
                        End Select

                        If sHLmark = "" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt1") : sUJRst = .Text

                            Select Case Mid(sJudgType, 1, 3)
                                Case "210"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "211"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "212"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "213"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "214"
                                    .Col = .GetColFromID("viewrst") : .Text = sViewRst & " " & sUJRst & ""
                            End Select
                        End If

                        If sHLmark = "H" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt2") : sUJRst = .Text

                            Select Case Mid(sJudgType, 4, 3)
                                Case "220"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "221"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "222"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "223"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "224"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sUJRst & ""
                            End Select
                        End If

                    Case 9
                        .Col = .GetColFromID("refl") : sRefL = .Text
                        .Col = .GetColFromID("refh") : sRefH = .Text
                        .Col = .GetColFromID("orgrst") : sOrgRst = .Text
                        .Col = .GetColFromID("viewrst") : sViewRst = .Text

                        .Col = .GetColFromID("refls") : sRefLs = .Text
                        .Col = .GetColFromID("refhs") : sRefHs = .Text

                        If sOrgRst = "" Then Exit Sub
                        If sOrgRst.Trim.StartsWith(">") Or sOrgRst.Trim.StartsWith("<") Then
                            sMark = sOrgRst.Substring(0, 1)
                            sRst = sOrgRst.Substring(1).Trim
                        ElseIf sOrgRst.Trim.StartsWith(">=") Or sOrgRst.Trim.StartsWith("<=") Then
                            sMark = sOrgRst.Substring(0, 1)
                            sRst = sOrgRst.Substring(1).Trim
                        Else
                            sRst = sOrgRst
                        End If

                        If IsNumeric(sRst) = False Then Exit Sub

                        Select Case sRefLs
                            Case "0"
                                If Val(sRst) < Val(sRefL) And sRefL <> "" Then
                                    sHLmark = "L"
                                End If
                            Case "1"
                                If Val(sRst) <= Val(sRefL) And sRefL <> "" Then
                                    sHLmark = "L"
                                End If
                        End Select
                        Select Case sRefHs
                            Case "0"
                                If Val(sRst) > Val(sRefH) And sRefH <> "" Then
                                    sHLmark = "H"
                                End If
                            Case "1"
                                If Val(sRst) >= Val(sRefH) And sRefH <> "" Then
                                    sHLmark = "H"
                                End If
                        End Select

                        If sHLmark = "L" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt1") : sUJRst = .Text
                            Select Case Mid(sJudgType, 1, 3)
                                Case "310"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "311"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "312"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "313"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "314"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sUJRst & ""
                            End Select
                        End If

                        'If Val(sRst) > Val(sRefL) And Val(sRst) <= Val(sRefH) Then

                        If sRefL = "" And sRefH = "" Then
                            sHLmark = "E"
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If
                        End If

                        If sHLmark = "" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt2") : sUJRst = .Text
                            Select Case Mid(sJudgType, 4, 3)
                                Case "320"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "321"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "322"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "323"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "324"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sUJRst & ""
                            End Select
                        End If

                        If sHLmark = "H" Then
                            If .GetColFromID("hlmark") > 0 Then
                                .Col = .GetColFromID("hlmark") : .Text = ""
                            End If

                            .Col = .GetColFromID("ujudglt3") : sUJRst = .Text
                            Select Case Mid(sJudgType, 7, 3)
                                Case "330"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "331"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst
                                Case "332"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & "(" & sOrgRst & ")"
                                Case "333"
                                    .Col = .GetColFromID("viewrst") : .Text = sUJRst & " " & sOrgRst & ""
                                Case "334"
                                    .Col = .GetColFromID("viewrst") : .Text = sOrgRst & " " & sUJRst & ""
                            End Select
                        End If

                    Case Else

                End Select
            End If

        End With
    End Sub

    ' N/P check
    Private Sub sbNPCheck(ByVal riRow As Integer, ByVal rdt_RstCd As DataTable)

        Dim sMbtType As String = "" : Dim sOrgRst As String = "" : Dim sTestCd As String = "", sTCdGbn As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("mbttype") : sMbtType = .Text
            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
            .Col = .GetColFromID("testcd") : sTestCd = .Text
            .Col = .GetColFromID("tcdgbn") : sTCdGbn = .Text

            If Not (sMbtType = "2" Or sMbtType = "3") Then Return

            If sOrgRst = FixedVariable.gsRst_Nogrowth Then
                .Col = .GetColFromID("hlmark") : .Text = "N"
            ElseIf sOrgRst = FixedVariable.gsRst_Growth Then
                .Col = .GetColFromID("hlmark") : .Text = "P"
            ElseIf sOrgRst = "" Then
                .Col = .GetColFromID("hlmark") : .Text = ""
            Else
                Dim dr As DataRow() = rdt_RstCd.Select("testcd = '" + sTestCd + "'")

                Dim r As DataRow
                For Each r In dr
                    If sOrgRst = r.Item("rstcont").ToString.Trim Then
                        .Col = .GetColFromID("hlmark") : .Text = r.Item("rstlvl").ToString.Trim
                        If .Text = "" And sOrgRst <> "" Then .Text = "N"
                        Exit For
                    End If
                Next r
            End If

            .Col = .GetColFromID("hlmark")
            If .Text = "" And sOrgRst <> "" Then .Text = "N"

            If sTCdGbn <> "C" Or sMbtType = "3" Then Return

            Dim iRow_Start As Integer = 0

            For ix As Integer = riRow To 1 Step -1
                .Row = ix
                .Col = .GetColFromID("testcd") : Dim sTmp_cd As String = .Text
                If sTmp_cd = sTestCd.Substring(0, 5) Then
                    iRow_Start = ix
                    Exit For
                End If
            Next

            Dim sNP As String = ""

            For ix As Integer = iRow_Start + 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("testcd") : Dim sTmp_Cd As String = .Text

                If sTmp_Cd.Substring(0, 5) <> sTestCd.Substring(0, 5) Then Exit For
                .Col = .GetColFromID("hlmark") : If .Text.Trim <> "" Then sNP = .Text

                If sNP = "P" Then Exit For
            Next

            If sNP = "P" Then
                .Row = iRow_Start
                .Col = .GetColFromID("orgrst") : .Text = FixedVariable.gsRst_Growth
                .Col = .GetColFromID("viewrst") : .Text = FixedVariable.gsRst_Growth
                .Col = .GetColFromID("hlmark") : .Text = sNP

            ElseIf sNP = "N" Then
                .Row = iRow_Start
                .Col = .GetColFromID("orgrst") : .Text = FixedVariable.gsRst_Nogrowth
                .Col = .GetColFromID("viewrst") : .Text = FixedVariable.gsRst_Nogrowth
                .Col = .GetColFromID("hlmark") : .Text = sNP
            Else
                .Row = iRow_Start
                .Col = .GetColFromID("orgrst") : .Text = ""
                .Col = .GetColFromID("viewrst") : .Text = ""
                .Col = .GetColFromID("hlmark") : .Text = ""
            End If

        End With

    End Sub


    ' High, Low 체크
    Private Sub sbHLCheck(ByVal riRow As Integer)
        Dim strRefL As String = ""
        Dim strRefH As String = ""
        Dim strRefLS As String = ""
        Dim strRefHS As String = ""
        Dim strRst As String = ""
        Dim sHLMark As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("refgbn")
            If .Text = "2" Then
                .Col = .GetColFromID("judgtype")
                If .Text <> "1" Then
                    .BackColor = Color.White
                    .ForeColor = Color.Black
                    .SetText(.GetColFromID("hlmark"), riRow, "")
                    Return
                End If

                .Col = .GetColFromID("refl") : strRefL = .Text
                .Col = .GetColFromID("refls") : strRefLS = .Text
                .Col = .GetColFromID("refh") : strRefH = .Text
                .Col = .GetColFromID("refhs") : strRefHS = .Text
                .Col = .GetColFromID("orgrst") : strRst = .Text

                strRst = strRst.Replace(">", "").Replace("<", "").Replace("=", "")

                If IsNumeric(strRst) Then
                    Select Case strRefLS
                        Case "0"
                            If Val(strRst) < Val(strRefL) And strRefL <> "" Then
                                sHLMark = "L"
                            End If
                        Case "1"
                            If Val(strRst) <= Val(strRefL) And strRefL <> "" Then
                                sHLMark = "L"
                            End If
                    End Select

                    Select Case strRefHS
                        Case "0"
                            If Val(strRst) > Val(strRefH) And strRefH <> "" Then
                                sHLMark = "H"
                            End If
                        Case "1"
                            If Val(strRst) >= Val(strRefH) And strRefH <> "" Then
                                sHLMark = "H"
                            End If
                    End Select

                End If

                .Col = .GetColFromID("hlmark")
                Select Case sHLMark
                    Case "L"
                        .BackColor = Color.FromArgb(221, 240, 255)
                        .ForeColor = Color.FromArgb(0, 0, 255)
                        .SetText(.GetColFromID("hlmark"), riRow, sHLMark)
                    Case "H"
                        .BackColor = Color.FromArgb(255, 230, 231)
                        .ForeColor = Color.FromArgb(255, 0, 0)
                        .SetText(.GetColFromID("hlmark"), riRow, sHLMark)
                    Case Else
                        .BackColor = Color.White
                        .ForeColor = Color.Black
                        .SetText(.GetColFromID("hlmark"), riRow, "")
                End Select
            End If
        End With
    End Sub

    ' 패닉 체크
    ' 2 : 패닉 하한치, 상한치 사용
    Private Sub sbPanicCheck(ByVal riRow As Integer, Optional ByVal rdt_RstCd As DataTable = Nothing)

        Dim sOrgRst As String = ""
        Dim sPanicGbn As String = ""
        Dim sPanicL As String = ""
        Dim sPanicH As String = ""
        Dim sGrade As String = ""
        Dim sTestCd As String = ""
        Dim sPanicMark As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
            .Col = .GetColFromID("panicgbn") : sPanicGbn = .Text

            sOrgRst = sOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

            Select Case sPanicGbn
                Case "4", "5", "6"
                    .Col = .GetColFromID("testcd") : sTestCd = .Text

                    If rdt_RstCd Is Nothing Then Exit Sub

                    Dim foundRows As DataRow() = rdt_RstCd.Select("testcd = '" & sTestCd & "'")

                    Dim r As DataRow
                    For Each r In foundRows
                        If sOrgRst = r.Item("rstcont").ToString.Trim Then
                            sGrade = r.Item("grade").ToString
                            Exit For
                        End If
                    Next r
            End Select

            Select Case sPanicGbn
                Case "1"    ' 패닉하한치만 사용
                    .Col = .GetColFromID("panicl") : sPanicL = .Text

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "2"    ' 패닉상한치만 사용
                    .Col = .GetColFromID("panich") : sPanicH = .Text
                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "3"    ' 모두 사용
                    .Col = .GetColFromID("panicl") : sPanicL = .Text
                    .Col = .GetColFromID("panich") : sPanicH = .Text

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                        If Val(sOrgRst) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If
                Case "4"    ' 하한치만 사용(Grade)                        

                    .Col = .GetColFromID("panicl") : sPanicL = .Text

                    If sGrade <> "" Then
                        If Val(sGrade) < Val(sPanicL) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "5"    ' 상한치만 사용(Grade)                        

                    .Col = .GetColFromID("panich") : sPanicH = .Text

                    If sGrade <> "" Then
                        If Val(sGrade) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If

                Case "6"    ' 모두 사용(Grade)

                    .Col = .GetColFromID("panicl") : sPanicH = .Text
                    .Col = .GetColFromID("panich") : sPanicH = .Text

                    If sGrade <> "" Then
                        If Val(sGrade) < Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                        If Val(sGrade) > Val(sPanicH) Then
                            sPanicMark = "P"
                        End If
                    End If

            End Select

            If sPanicMark = "P" Then
                .Col = .GetColFromID("panicmark") : .Text = sPanicMark

                .BackColor = Color.FromArgb(150, 150, 255)
                .ForeColor = Color.FromArgb(255, 255, 255)
            Else
                .Col = .GetColFromID("panicmark") : .Text = ""

                .BackColor = Color.White
                .ForeColor = Color.Black
            End If
            'End If
        End With
    End Sub

    ' Delta 체크
    Private Sub sbDeltaCheck(ByVal riRow As Integer, Optional ByVal rdt_RstCd As DataTable = Nothing)
        Dim sFn As String = "Sub deltaCheck(Integer)"
        Try
            Dim lgDateDiff As Long = 0
            Dim sDateDiff As String = ""
            Dim dtBFFNDT As Date
            Dim sDeltaGbn As String = ""
            Dim sOrgRst As String = ""
            Dim sOrgRst_O As String = ""
            Dim sDeltaL As String = ""
            Dim sDeltaH As String = ""
            Dim sDeltaMark As String = ""

            With spdResult
                .Row = riRow
                If .GetColFromID("bffndt1") < 0 Then
                    Exit Sub
                End If
                .Col = .GetColFromID("bffndt1")
                If .Text <> "" Then
                    dtBFFNDT = CDate(.Text)
                    lgDateDiff = DateDiff(DateInterval.Day, dtBFFNDT, MainServerDateTime.mServerDateTime)
                    If lgDateDiff < 1 Then
                        sDateDiff = "1"
                    Else
                        sDateDiff = Str(lgDateDiff).Trim
                    End If
                    .Col = .GetColFromID("deltaday")
                    If Val(sDateDiff) > Val(.Text) Then Exit Sub
                Else
                    Exit Sub
                End If

                .Col = .GetColFromID("deltagbn") : sDeltaGbn = .Text
                .Col = .GetColFromID("orgrst") : sOrgRst = .Text

                sOrgRst = sOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                If sOrgRst = "" Then
                    .Col = .GetColFromID("deltamark") : .Text = ""
                    .BackColor = Color.White
                    .ForeColor = Color.Black
                    Exit Sub
                End If
                .Col = .GetColFromID("bforgrst1") : sOrgRst_O = .Text

                sOrgRst_O = sOrgRst_O.Replace(">", "").Replace("<", "").Replace("=", "")

                If sOrgRst.Trim = "" Then Exit Sub
                If sOrgRst_O.Trim = "" Then Exit Sub

                .Col = .GetColFromID("deltah") : sDeltaH = .Text
                .Col = .GetColFromID("deltal") : sDeltaL = .Text

                Select Case sDeltaGbn
                    Case "1", "2", "3", "4"
                        If IsNumeric(sOrgRst) = False Then Exit Sub
                        If IsNumeric(sOrgRst_O) = False Then Exit Sub
                End Select

                Select Case sDeltaGbn
                    Case "1"    ' 1 : 변화차 = 현재결과 - 이전결과,
                        If sDeltaH <> "" And Val(sOrgRst) - Val(sOrgRst_O) > Val(sDeltaH) Then
                            sDeltaMark = "D"
                        End If

                        If sDeltaL <> "" And Val(sOrgRst) - Val(sOrgRst_O) < Val(sDeltaL) Then
                            sDeltaMark = "D"
                        End If

                    Case "2"    ' 2: 변화비율 = 변화차/이전결과  * 100
                        If Val(sOrgRst_O) = 0 Then
                            sDeltaMark = "D"
                        Else
                            If sDeltaH <> "" And ((Val(sOrgRst) - Val(sOrgRst_O)) / Val(sOrgRst_O)) * 100 > Val(sDeltaH) Then
                                sDeltaMark = "D"
                            End If

                            If sDeltaL <> "" And ((Val(sOrgRst) - Val(sOrgRst_O)) / Val(sOrgRst_O)) * 100 < Val(sDeltaL) Then
                                sDeltaMark = "D"
                            End If
                        End If

                    Case "3"    '기간당 변화차 = 변화차/기간
                        .Col = .GetColFromID("bffndt1")
                        If .Text <> "" Then
                            If sDeltaH <> "" And (Val(sOrgRst) - Val(sOrgRst_O)) / Val(sDateDiff) > Val(sDeltaH) Then
                                sDeltaMark = "D"
                            End If

                            If sDeltaL <> "" And (Val(sOrgRst) - Val(sOrgRst_O)) / Val(sDateDiff) < Val(sDeltaL) Then
                                sDeltaMark = "D"
                            End If
                        End If

                    Case "4"    '기간당 변화비율 = 변화비율/기간
                        .Col = .GetColFromID("bffndt1")
                        If .Text <> "" Then
                            If sDeltaH <> "" And ((Val(sOrgRst) - Val(sOrgRst_O)) / Val(sOrgRst_O)) * 100 / Val(sDateDiff) > Val(sDeltaH) Then
                                sDeltaMark = "D"
                            End If

                            If sDeltaL <> "" And ((Val(sOrgRst) - Val(sOrgRst_O)) / Val(sOrgRst_O)) * 100 / Val(sDateDiff) < Val(sDeltaL) Then
                                sDeltaMark = "D"
                            End If
                        End If

                    Case "5"    'Grade Delta = 현재Grade - 이전Grade
                        Dim sTestCd As String
                        Dim sSpcCd As String

                        .Row = riRow
                        .Col = .GetColFromID("testcd") : sTestCd = .Text
                        .Col = .GetColFromID("spccd") : sSpcCd = .Text

                        Dim sGrade As String = ""
                        Dim sGrade_Old As String = ""

                        If rdt_RstCd Is Nothing Then Exit Sub

                        Dim dr As DataRow() = rdt_RstCd.Select("testcd = '" + sTestCd + "'")
                        Dim dt As DataTable = Fn.ChangeToDataTable(dr)

                        For ix As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(ix).Item("rstcont").ToString.Trim = sOrgRst Then
                                sGrade = dt.Rows(ix).Item("grade").ToString
                                Exit For
                            End If
                        Next

                        For ix As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(ix).Item("rstcont").ToString.Trim = sOrgRst_O Then
                                sGrade_Old = dt.Rows(ix).Item("grade").ToString
                                Exit For
                            End If
                        Next

                        If sGrade <> "" And sGrade_Old <> "" Then
                            If Math.Abs(Val(sGrade) - Val(sGrade_Old)) > Math.Abs(Val(sDeltaH)) Then
                                sDeltaMark = "D"
                            End If
                        End If

                End Select

                .Col = .GetColFromID("deltamark")
                If sDeltaMark = "D" Then
                    .Text = sDeltaMark
                    .BackColor = Color.FromArgb(150, 255, 150)
                    .ForeColor = Color.FromArgb(0, 128, 64)
                Else
                    .Text = ""
                    .BackColor = Color.White
                    .ForeColor = Color.Black
                End If
            End With
        Catch ex As Exception
            'sbLog_Exception(sFn + " : " + ex.Message)
        End Try

    End Sub

    ' Critical 체크
    Private Sub sbCriticalCheck(ByVal riRow As Integer)
        Dim sOrgRst As String = ""
        Dim sCriticalGbn As String = ""
        Dim sCriticalL As String = ""
        Dim sCriticalH As String = ""
        Dim sCriticalMark As String = ""
        Dim strTclscd As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
            .Col = .GetColFromID("criticalgbn") : sCriticalGbn = .Text
            .Col = .GetColFromID("criticall") : sCriticalL = .Text
            .Col = .GetColFromID("criticalh") : sCriticalH = .Text
            .Col = .GetColFromID("testcd") : strTclscd = .Text

            sOrgRst = sOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

            Select Case sCriticalGbn
                Case "1"    ' 위험하한치만 사용

                    If sCriticalL = "" Then Exit Sub
                    If IsNumeric(sCriticalL) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) < Val(sCriticalL) Then
                            sCriticalMark = "C"
                        End If
                    End If

                Case "2"    '  위험상한치만 사용
                    If sCriticalH = "" Then Exit Sub
                    If IsNumeric(sCriticalH) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) > Val(sCriticalH) Then
                            sCriticalMark = "C"
                        End If
                    End If
                Case "3"    ' 모두 사용
                    If sCriticalL = "" Then Exit Sub
                    If IsNumeric(sCriticalL) = False Then Exit Sub
                    If sCriticalH = "" Then Exit Sub
                    If IsNumeric(sCriticalH) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) < Val(sCriticalL) Then
                            sCriticalMark = "C"
                        End If
                        If Val(sOrgRst) > Val(sCriticalH) Then
                            sCriticalMark = "C"
                        End If
                    End If
                Case "7"
                    'Critical 문자값 판단 추가(검사마스터에서 Critical 구분 [7] 문자결과(결과코드 설정) 선택, 기초마스터 결과코드에 Critical 설정한 경우 )
                    Dim sTxtCritical As String = ""
                    sTxtCritical = LISAPP.COMM.RstFn.fnGet_GraedValue_C(strTclscd, sOrgRst)

                    '20200624 JHS 크리티컬 문자내용 적용 
                    ' "LM205" 
                    If PRG_CONST.AFBC_test(strTclscd) <> "" Then ' "LM205"  xpert pcr 검사가 Critical이라도 해당 환자의 1주일전 pcr검사 이력이 Deteted(Critical)일 경우 Normal결과로 판단
                        Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_AFB_Comment(msBcNo, True)

                        If dt.Rows.Count > 0 Then
                            Exit Sub
                        ElseIf dt.Rows.Count <= 0 Then
                            sCriticalMark = sTxtCritical
                            ' If sTxtCritical = "C" Then msXpertC = True Else msXpertC = False
                        End If

                        ' LM20101, LM20102, LM20301, LM20302, LM20303 액체, 고체배지
                    ElseIf PRG_CONST.AFBC_NTM_test(strTclscd) <> "" Then 'And sTxtCritical = "C" Then '20210702 jhs MTB, NTM 검사
                        Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_AFB_NTM_Comment(msBcNo) ' 5년안에 검사결과값이 있을 때 ->  20220315 jhs 3년안으로 변경
                        Dim chkOrgRst As String = Mid(sOrgRst, 1, 3).ToUpper '결과값 앞의 3자리 가져오기

                        'If chkOrgRst = "MYC" Then ' 입력결과값이 MTB 검사일 경우
                        '    If dt.Rows(0).Item("MTB").ToString.Trim = "Y" Then ' 5년안에 MTB 검사가 있을 경우
                        '        sCriticalMark = ""
                        '    Else '5년안에  MTB 검사가 없을 경우
                        '        sCriticalMark = "C"
                        '    End If
                        'Else
                        If (chkOrgRst = "LIQ" Or chkOrgRst = "AFB") Then ' 입력 결과값이 NTM검사결과 일경우
                            If dt.Rows(0).Item("NTM").ToString.Trim = "Y" Then '5년안에 NTM 검사가 있을 경우
                                sCriticalMark = ""
                            Else ' 5년안에 NTM검사가 없을 경우
                                sCriticalMark = "C"
                            End If
                        End If

                    ElseIf sTxtCritical = "C" Then
                        sCriticalMark = "C"
                    Else
                        sCriticalMark = ""
                    End If
                    '----------------------------------------------------


                    'If strTclscd = "LM205" Then 'xpert pcr 검사가 Critical이라도 해당 환자의 1주일전 pcr검사 이력이 Deteted(Critical)일 경우 Normal결과로 판단
                    '    Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_AFB_Comment(msBcNo, True)

                    '    If dt.Rows.Count > 0 Then
                    '        Exit Sub
                    '    ElseIf dt.Rows.Count <= 0 Then
                    '        sCriticalMark = sTxtCritical
                    '        ' If sTxtCritical = "C" Then msXpertC = True Else msXpertC = False
                    '    End If
                    'Else
                    '    '임시막음
                    '    'sCriticalMark = "C"
                    'End If
            End Select

            .Col = .GetColFromID("criticalmark")
            If sCriticalMark = "C" Then
                .Text = sCriticalMark
                .BackColor = Color.FromArgb(255, 150, 150)
                .ForeColor = Color.FromArgb(255, 255, 255)
            Else
                .Text = ""
                .BackColor = Color.White
                .ForeColor = Color.Black
            End If
        End With
    End Sub

    ' alert 체크
    Private Sub sbAlertCheck(ByVal riRow As Integer)
        Dim sOrgRst As String = "", sViewRst As String = "", sEqFlag As String = ""
        Dim sTestCd As String = "", sSpcCd As String = "", sTclsCd As String = "", sPanicMark As String = "", sDeltaMark As String = ""
        Dim sAlertGbn As String = ""
        Dim sAlertL As String = ""
        Dim sAlertH As String = ""
        Dim sAlertMark As String = ""

        With spdResult
            .Row = riRow
            .Col = .GetColFromID("testcd") : sTestCd = .Text
            .Col = .GetColFromID("spccd") : sSpcCd = .Text
            .Col = .GetColFromID("tclscd") : sTclsCd = .Text

            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
            .Col = .GetColFromID("viewrst") : sViewRst = .Text
            .Col = .GetColFromID("eqflag") : sEqFlag = .Text

            .Col = .GetColFromID("panicmark") : sPanicMark = .Text
            .Col = .GetColFromID("deltamark") : sDeltaMark = .Text

            .Col = .GetColFromID("alertgbn") : sAlertGbn = .Text
            .Col = .GetColFromID("alertl") : sAlertL = .Text
            .Col = .GetColFromID("alerth") : sAlertH = .Text

            Select Case sAlertGbn
                Case "1", "A"    ' 경고하한치만 사용
                    If sAlertL = "" Then Exit Sub
                    If IsNumeric(sAlertL) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) < Val(sAlertL) Then
                            sAlertMark = "A"
                        End If
                    End If

                Case "2", "B"    ' 경고상한치만 사용
                    If sAlertH = "" Then Exit Sub
                    If IsNumeric(sAlertH) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) > Val(sAlertH) Then
                            sAlertMark = "A"
                        End If
                    End If
                Case "3", "C"    ' 모두 사용
                    If sAlertL = "" Then Exit Sub
                    If IsNumeric(sAlertL) = False Then Exit Sub
                    If sAlertH = "" Then Exit Sub
                    If IsNumeric(sAlertH) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) < Val(sAlertL) Then
                            sAlertMark = "A"
                        End If
                        If Val(sOrgRst) > Val(sAlertH) Then
                            sAlertMark = "A"
                        End If
                    End If

                Case "4"    '-- 문자값 비교
                    If sAlertL = "" And sAlertH = "" Then Exit Sub
                    If sAlertL = "" Then sAlertL = sAlertH

                    If sOrgRst.ToUpper = sAlertL.ToUpper Then sAlertMark = "A"
                Case "7" '-- 결과코드 
                    '20210810 jhs 결과코드 추가 
                    'Alter 문자값 판단 추가(검사마스터에서 Alter 구분 [7] 문자결과(결과코드 설정) 선택, 기초마스터 결과코드에 Alter 설정한 경우 )
                    Dim sTxtAlter As String = ""
                    sTxtAlter = LISAPP.COMM.RstFn.fnGet_GraedValue_A(sTestCd, sOrgRst)

                    If sTxtAlter = "A" Then
                        sAlertMark = "A"
                    End If

                    ' "LM205" 
                    If PRG_CONST.AFBC_test(sTestCd) <> "" Then ' "LM205"  xpert pcr 검사가 Critical이라도 해당 환자의 1주일전 pcr검사 이력이 Deteted(Critical)일 경우 Normal결과로 판단
                        Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_AFB_Comment(msBcNo, True)

                        If dt.Rows.Count > 0 Then
                            Exit Sub
                        ElseIf dt.Rows.Count <= 0 Then
                            sAlertMark = sTxtAlter
                            ' If sTxtCritical = "C" Then msXpertC = True Else msXpertC = False
                        End If
                        '작업중..................................................
                        ' LM20101, LM20102, LM20301, LM20302, LM20303 액체, 고체배지
                    ElseIf PRG_CONST.AFBC_NTM_test(sTestCd) <> "" Then 'And sTxtAlter = "A" Then '20210702 jhs MTB, NTM 검사
                        Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_AFB_NTM_Comment(msBcNo) ' 5년안에 검사결과값이 있을 때
                        Dim chkOrgRst As String = Mid(sOrgRst, 1, 3).ToUpper '결과값 앞의 3자리 가져오기

                        If chkOrgRst = "MYC" Then ' 입력결과값이 MTB 검사일 경우
                            If dt.Rows(0).Item("MTB").ToString.Trim = "Y" Then ' 5년안에 MTB 검사가 있을 경우
                                sAlertMark = ""
                            Else '5년안에  MTB 검사가 없을 경우
                                sAlertMark = "A"
                            End If
                            'ElseIf (chkOrgRst = "LIQ" Or chkOrgRst = "AFB") Then ' 입력 결과값이 NTM검사결과 일경우
                            '    If dt.Rows(0).Item("NTM").ToString.Trim = "Y" Then '5년안에 NTM 검사가 있을 경우
                            '        sAlertMark = ""
                            '    Else ' 5년안에 NTM검사가 없을 경우
                            '        sAlertMark = "A"
                            '    End If
                        End If
                    ElseIf sAlertMark = "A" Then
                        sAlertMark = "A"
                    Else
                        sAlertMark = ""
                    End If
                    '----------------------------------------------------

            End Select

            If sAlertMark = "" And (sAlertGbn = "5" Or sAlertGbn = "A" Or sAlertGbn = "B" Or sAlertGbn = "C") Then
                Dim dr As DataRow() = m_dt_Alert_Rule.Select("testcd = '" + sTestCd + "'")

                If dr.Length > 0 Then
                    Dim iCnt As Integer = 0, iAlert As Integer = 0

                    If dr(0).Item("orgrst").ToString.Trim <> "" Then
                        iCnt += 1
                        If dr(0).Item("orgrst").ToString().IndexOf(sOrgRst + ",") >= 0 Then iAlert += 1
                    End If

                    If dr(0).Item("viewrst").ToString.Trim <> "" Then
                        iCnt += 1
                        If dr(0).Item("viewrst").ToString().IndexOf(sViewRst + ",") >= 0 Then iAlert += 1
                    End If

                    If sPanicMark <> "" Then
                        iCnt += 1
                        iAlert += 1
                    End If

                    If sDeltaMark <> "" Then
                        iCnt += 1
                        iAlert += 1
                    End If


                    If dr(0).Item("eqflag").ToString.Trim <> "" Then
                        iCnt += 1

                        If sEqFlag <> "" Then

                            If dr(0).Item("eqflag").ToString().IndexOf("^") >= 0 Then
                                Dim strBuf() As String = dr(0).Item("eqflag").ToString().Split("^"c)

                                If strBuf(1) = "" Then
                                    strBuf(0) += ","
                                    If strBuf(0).IndexOf(sEqFlag + ",") >= 0 Then iAlert += 1
                                Else
                                    If strBuf(0) = "" Then
                                        If strBuf(1).IndexOf(sTclsCd + ",") >= 0 Then iAlert += 1
                                    Else
                                        If strBuf(0).IndexOf(sEqFlag + ",") >= 0 And strBuf(1).IndexOf(sTclsCd + ",") >= 0 Then iAlert += 1
                                    End If
                                End If
                            Else
                                If dr(0).Item("eqflag").ToString().IndexOf(sEqFlag + ",") >= 0 Then iAlert += 1
                            End If
                        End If
                    End If

                    If dr(0).Item("sex").ToString.Trim <> "" Then
                        iCnt += 1
                        If msSexAge.StartsWith(dr(0).Item("sex").ToString()) Then iAlert += 1
                    End If

                    If dr(0).Item("deptcds").ToString.Trim <> "" Then
                        iCnt += 1
                        If dr(0).Item("deptcds").ToString().IndexOf(msDeptCd + ",") >= 0 Then iAlert += 1
                    End If

                    If dr(0).Item("spccds").ToString.Trim <> "" Then
                        iCnt += 1
                        If dr(0).Item("spccds").ToString().IndexOf(sSpcCd + ",") >= 0 Then iAlert += 1
                    End If

                    If dr(0).Item("baccds").ToString.Trim <> "" Then
                        iCnt += 1

                        Dim dr_bac As DataRow() = m_dt_Bac_BcNo.Select("status <> 'D' and testcd = '" + sTestCd + "'")
                        If dr_bac.Length > 0 Then

                            For ix As Integer = 0 To dr_bac.Length - 1
                                If dr(0).Item("baccds").ToString.IndexOf(dr_bac(ix).Item("baccd").ToString + ",") >= 0 Then
                                    iAlert += 1
                                    Exit For
                                End If
                            Next

                            Dim blnGrowth As Boolean = False

                            For ix As Integer = 0 To dr_bac.Length - 1
                                If dr(0).Item("baccds").ToString.IndexOf(dr_bac(ix).Item("baccd").ToString + ",") >= 0 And _
                                      dr_bac(ix).Item("bacgencd").ToString <> FixedVariable.gsBacGenCd_Nogrowth Then
                                    blnGrowth = True
                                    Exit For
                                End If
                            Next

                            If sOrgRst = "" And sViewRst = "" Then
                                For ix As Integer = 0 To dr.Length - 1
                                    If dr(ix).Item("orgrst").ToString <> "" Then
                                        If blnGrowth Then
                                            If dr(ix).Item("orgrst").ToString = FixedVariable.gsRst_Growth Then iAlert += 1
                                        Else
                                            If dr(ix).Item("orgrst").ToString = FixedVariable.gsRst_Nogrowth Then iAlert += 1
                                        End If
                                    End If

                                    If dr(ix).Item("viewrst").ToString <> "" Then
                                        If blnGrowth Then
                                            If dr(ix).Item("viewrst").ToString = FixedVariable.gsRst_Growth Then iAlert += 1
                                        Else
                                            If dr(ix).Item("viewrst").ToString = FixedVariable.gsRst_Nogrowth Then iAlert += 1
                                        End If
                                    End If

                                    Exit For
                                Next
                            End If
                        End If
                    End If

                    If dr(0).Item("antic").ToString.Trim <> "" Then
                        iCnt += 1

                        Dim dr_anti As DataRow() = m_dt_Anti_BcNo.Select("status <> 'D' and testcd = '" + sTestCd + "'")
                        If dr_anti.Length > 0 Then

                            Dim sCalcForm As String = dr(0).Item("antic").ToString

                            For ix As Integer = 0 To dr_anti.Length - 1
                                sCalcForm = sCalcForm.ToUpper.Replace("#B", "'" + dr_anti(ix).Item("baccd").ToString.ToUpper.Trim + "'")
                                sCalcForm = sCalcForm.ToUpper.Replace("[" + dr_anti(ix).Item("anticd").ToString.ToUpper.Trim + "]", "'" + dr_anti(ix).Item("decrst").ToString + "'")
                            Next

                            sCalcForm = sCalcForm.Replace("$$", "AND").Replace("||", "OR").Replace("[", "'").Replace("]", "'")

                            If LISAPP.COMM.RstFn.fnGet_Calc_DBQuery(sCalcForm) = "1" Then iAlert += 1

                        End If
                    End If

                    If iCnt > 0 And iAlert > 0 Then sAlertMark = "A"
                End If

            End If

            .Row = riRow
            .Col = .GetColFromID("alertmark")
            If sAlertMark = "A" Then
                .Text = sAlertMark
                .BackColor = Color.FromArgb(255, 255, 150)
                .ForeColor = Color.FromArgb(0, 0, 0)
            Else
                .Text = ""
                .BackColor = Color.White
                .ForeColor = Color.Black
            End If
        End With
    End Sub

    ' alert 체크
    Private Sub sbAlimitCheck(ByVal rirow As Integer)

        Dim sOrgRst As String = ""
        Dim sAlimitL As String = ""
        Dim sAlimitH As String = ""
        Dim sAlimitLs As String = ""
        Dim sAlimitHs As String = ""
        Dim sAlimitGbn As String = ""

        With spdResult
            .Row = rirow
            .Col = .GetColFromID("orgrst") : sOrgRst = .Text
            .Col = .GetColFromID("alimitgbn") : sAlimitGbn = .Text
            .Col = .GetColFromID("alimitl") : sAlimitL = .Text
            .Col = .GetColFromID("alimith") : sAlimitH = .Text
            .Col = .GetColFromID("alimitls") : sAlimitLs = .Text
            .Col = .GetColFromID("alimiths") : sAlimitHs = .Text

            sOrgRst = sOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

            Select Case sAlimitGbn
                Case "1"    ' 허용하한치만 사용
                    If sAlimitL = "" Then Exit Sub
                    If IsNumeric(sAlimitL) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) <= Val(sAlimitL) Then
                            Select Case sAlimitLs
                                Case "1"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL
                                Case "2"
                                    .Col = .GetColFromID("viewrst") : .Text = "< " + sAlimitL
                                Case "3"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL + " 이하"
                                Case "4"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL + " 미만"
                                Case "5"
                                    .Col = .GetColFromID("viewrst") : .Text = "<= " + sAlimitL
                            End Select
                            'sAlertMark = "A"
                        End If
                    End If

                Case "2"    ' 허용상한치만 사용
                    If sAlimitH = "" Then Exit Sub
                    If IsNumeric(sAlimitH) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) >= Val(sAlimitH) Then
                            Select Case sAlimitHs
                                Case "1"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH
                                Case "2"
                                    .Col = .GetColFromID("viewrst") : .Text = "> " + sAlimitH
                                Case "3"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH + " 이상"
                                Case "4"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH + " 초과"
                                Case "5"
                                    .Col = .GetColFromID("viewrst") : .Text = ">= " + sAlimitH
                            End Select
                            'sAlertMark = "A"
                        End If
                    End If
                Case "3"    ' 모두 사용

                    If sAlimitL = "" Then Exit Sub
                    If IsNumeric(sAlimitL) = False Then Exit Sub
                    If sAlimitH = "" Then Exit Sub
                    If IsNumeric(sAlimitH) = False Then Exit Sub

                    If IsNumeric(sOrgRst) Then
                        If Val(sOrgRst) <= Val(sAlimitL) Then
                            Select Case sAlimitLs
                                Case "1"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL
                                Case "2"
                                    .Col = .GetColFromID("viewrst") : .Text = "< " + sAlimitL
                                Case "3"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL + " 이하"
                                Case "4"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitL + " 미만"
                                Case "5"
                                    .Col = .GetColFromID("viewrst") : .Text = "<= " + sAlimitL
                            End Select
                            'sAlertMark = "A"
                        End If
                        If Val(sOrgRst) >= Val(sAlimitH) Then
                            Select Case sAlimitHs
                                Case "1"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH
                                Case "2"
                                    .Col = .GetColFromID("viewrst") : .Text = "> " + sAlimitH
                                Case "3"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH + " 이상"
                                Case "4"
                                    .Col = .GetColFromID("viewrst") : .Text = sAlimitH + " 초과"
                                Case "5"
                                    .Col = .GetColFromID("viewrst") : .Text = ">= " + sAlimitH
                            End Select
                        End If
                    End If
            End Select

        End With
    End Sub

    Private Function fnGetTextTipFetch(ByVal roSpd As AxFPSpreadADO.AxfpSpread, ByVal riRow As Integer) As String

        Dim sHelp As String = "" & vbCrLf
        With roSpd
            .Row = riRow
            Dim sRef As String = ""
            Dim sRefTmp As String = ""
            .Col = .GetColFromID("refl")
            sRefTmp = .Text
            If sRefTmp <> "" Then
                sRefTmp = ""
                .Col = .GetColFromID("refls")
                If .Text = "0" Then sRef = sRefTmp & " " & "<= "
                If .Text = "1" Then sRef = sRefTmp & " " & "< "
            End If
            .Col = .GetColFromID("refh")
            sRefTmp = .Text
            If sRefTmp <> "" Then
                sRefTmp = ""
                .Col = .GetColFromID("refhs")
                If .Text = "0" Then sRef &= " ~ <= " & sRefTmp
                If .Text = "1" Then sRef &= " ~ < " & sRefTmp
            End If
            .Col = .GetColFromID("refgbn")
            Dim sRefGbn As String
            sRefGbn = .Text
            .Col = .GetColFromID("judgtype")
            Dim sJudgType As String

            sJudgType = .Text
            .Col = .GetColFromID("reftxt")
            If sRef <> "" Then
                sHelp &= fnGetTipLine("참고치부등호 : " & sRef)
            End If

            If sRefGbn = "2" And Len(sJudgType) = 6 Then
                .Col = .GetColFromID("ujudglt1")
                Dim sJudg As String
                sJudg = "사용자판정문자 : " & .Text & " / "
                .Col = .GetColFromID("ujudglt2")
                sJudg &= .Text
                sHelp += fnGetTipLine(sJudg)
            End If

            If sRefGbn = "2" And Len(sJudgType) = 9 Then
                .Col = .GetColFromID("ujudglt1")
                Dim sJudg As String
                sJudg = "사용자판정문자 : " & .Text & " / "
                .Col = .GetColFromID("ujudglt2")
                sJudg &= .Text & " / "
                .Col = .GetColFromID("ujudglt3")
                sJudg &= .Text
                sHelp += fnGetTipLine(sJudg)
            End If

            If sRefGbn = "2" Then
                .Col = .GetColFromID("panicgbn")
                Dim sPanic As String = ""
                Select Case .Text
                    Case "1"    ' 패닉상한치만 사용                                    
                        .Col = .GetColFromID("panicl")
                        sPanic = "Panic 하한치 : "
                        sPanic &= .Text & "     판정기준 : 하한치"
                        sHelp += fnGetTipLine(sPanic)
                    Case "2"    ' 패닉상한치만 사용                                    
                        .Col = .GetColFromID("panich")
                        sPanic = "Panic 상한치 : "
                        sPanic &= .Text & "     판정기준 : 상한치"
                        sHelp += fnGetTipLine(sPanic)
                    Case "3"    ' 모두 사용
                        .Col = .GetColFromID("panicl")
                        sPanic = "Panic 하한치 : " & .Text & "  "
                        .Col = .GetColFromID("panich")
                        sPanic &= "상한치 : " & .Text & "   "
                        sHelp += fnGetTipLine(sPanic)
                    Case "4"    ' 패닉상한치만 사용(Grade)                                    
                        .Col = .GetColFromID("panicl")
                        sPanic = "Panic 하한치 : "
                        sPanic &= .Text & "     판정기준 : 하한치(Grade)"
                        sHelp += fnGetTipLine(sPanic)
                    Case "5"    ' 패닉상한치만 사용(Grade)                                    
                        .Col = .GetColFromID("panich")
                        sPanic = "Panic 상한치 : "
                        sPanic &= .Text & "     판정기준 : 상한치(Grade)"
                        sHelp += fnGetTipLine(sPanic)
                    Case "6"    ' 모두 사용(Grade)
                        .Col = .GetColFromID("panicl")
                        sPanic = "Panic 하한치 : " & .Text & "  "
                        .Col = .GetColFromID("panich")
                        sPanic &= " 상한치 : " & .Text & "     판정기준 : (Grade)"
                        sHelp += fnGetTipLine(sPanic)
                End Select

            End If

            If sRefGbn = "2" Then
                .Col = .GetColFromID("deltagbn")
                Dim sDelta As String = ""
                Select Case .Text
                    Case "1"    ' 델타하한치만 사용                                    
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 변화차 = 현재결과 - 이전결과"
                        sHelp += fnGetTipLine(sDelta)
                    Case "2"    ' 패닉상한치만 사용                                    
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 변화비율 = 변화차/이전결과  * 100"
                        sHelp += fnGetTipLine(sDelta)
                    Case "3"    ' 모두 사용
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 기간당 변화차 = 변화차/기간"
                        sHelp += fnGetTipLine(sDelta)
                    Case "4"    ' 패닉상한치만 사용(Grade)                                    
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 기간당 변화비율 = 변화비율/기간"
                        sHelp += fnGetTipLine(sDelta)
                    Case "5"    ' 패닉상한치만 사용(Grade)                                    
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta 하한치 : "
                        sDelta &= .Text
                        .Col = .GetColFromID("deltah")
                        sDelta &= "   상한치 : " & .Text & "     판정기준 : 절대변화비율 = 변화차/이전결과"
                        sHelp += fnGetTipLine(sDelta)
                    Case "6"    ' Grade Delta = 현재Grade - 이전Grade
                        .Col = .GetColFromID("deltal")
                        sDelta = "Delta : " '& .Text & " ~ "

                        sDelta &= .Text & "     판정기준 : Grade Delta = 현재Grade - 이전Grade"
                        sHelp += fnGetTipLine(sDelta)
                End Select

                If sDelta <> "" Then
                    .Col = .GetColFromID("deltaday")
                    sHelp += fnGetTipLine("Delta 기간일 : " & .Text)
                End If

            End If

            'If sRefGbn = "2" And sJudgType = "1" Then
            If sRefGbn = "2" Then
                .Col = .GetColFromID("criticalgbn")
                Dim sCritical As String = ""

                Select Case .Text
                    Case "1"    ' 위험하한치만 사용                                    
                        .Col = .GetColFromID("criticall")
                        sCritical = "Critical 하한치 : "
                        sCritical &= .Text & "     판정기준 : 하한치"
                        sHelp += fnGetTipLine(sCritical)
                    Case "2"    ' 위험상한치만 사용                                    
                        .Col = .GetColFromID("criticalh")
                        sCritical = "Critical 상한치 : "
                        sCritical &= .Text & "     판정기준 : 상한치"
                        sHelp += fnGetTipLine(sCritical)
                    Case "3"    ' 모두 사용
                        .Col = .GetColFromID("criticall")
                        sCritical = "Critical 하한치 : " & .Text & "  "
                        .Col = .GetColFromID("criticalh")
                        sCritical &= "상한치 : " & .Text & "   "
                        sHelp += fnGetTipLine(sCritical)
                End Select

            End If

            'If sRefGbn = "2" And sJudgType = "1" Then
            If sRefGbn = "2" Then
                .Col = .GetColFromID("alertgbn")
                Dim sAlert As String = ""

                Select Case .Text
                    Case "1", "A"    ' 경고하한치만 사용                                    
                        .Col = .GetColFromID("alertl")
                        sAlert = "Alert 하한치 : "
                        sAlert &= .Text & "     판정기준 : 하한치"
                        sHelp += fnGetTipLine(sAlert)
                    Case "2", "B"    ' 경고상한치만 사용                                    
                        .Col = .GetColFromID("alerth")
                        sAlert = "Alert 상한치 : "
                        sAlert &= .Text & "     판정기준 : 상한치"
                        sHelp += fnGetTipLine(sAlert)
                    Case "3", "C"   ' 모두 사용
                        .Col = .GetColFromID("alertl")
                        sAlert = "Alert 하한치 : " & .Text & " ~ "
                        .Col = .GetColFromID("alerth")
                        sAlert &= "상한치 : " & .Text
                        sHelp += fnGetTipLine(sAlert)
                End Select

            End If
        End With
        fnGetTextTipFetch = sHelp
    End Function

    Private Function fnGetTipLine(ByVal sStr As String) As String
        fnGetTipLine = Space(4) & sStr & Space(4) & vbCrLf
    End Function

    Private Sub sbDisplay_ExmAdd(ByVal rsTestCd As String)

        Dim strTCdGbn As String = ""

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("tcdgbn") : strTCdGbn = .Text

                .Col = .GetColFromID("testcd")
                If .Text.Substring(0, 5) = rsTestCd And strTCdGbn = "C" Then
                    .Col = .GetColFromID("viwsub")
                    If .Text = "0" Then
                        btnExmAdd.Enabled = True
                        btnExmAdd.Enabled = True
                        Exit For
                    End If
                End If
            Next
        End With

    End Sub

    Private Sub sbResult_Setting(ByVal raRst As ArrayList)

        For intIdx As Integer = 0 To raRst.Count - 1
            With spdResult
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("testcd")
                    If .Text = CType(raRst(intIdx), RST_INFO).TestCd Then
                        .Col = .GetColFromID("orgrst") : .Text = CType(raRst(intIdx), RST_INFO).OrgRst
                        .Col = .GetColFromID("viewrst") : .Text = CType(raRst(intIdx), RST_INFO).ViewRst
                        .Col = .GetColFromID("tcdgbn") : Dim sTCdGbn As String = .Text

                        .Col = .GetColFromID("chk")
                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            .Text = "1"
                        End If

                        .Col = .GetColFromID("iud") : .Text = "1"

                        If sTCdGbn = "C" Then
                            For intIx1 As Integer = intRow - 1 To 1 Step -1
                                .Row = intIx1
                                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                                .Col = .GetColFromID("tcdgbn") : Dim strPTcdgbn As String = .Text
                                If strPTcdgbn = "P" And sTestCd = CType(raRst(intIdx), RST_INFO).TestCd.Substring(0, 5) Then
                                    .Col = .GetColFromID("chk") : .Text = "1"
                                    Exit For
                                End If
                            Next
                        End If

                        Exit For

                    End If
                Next
            End With
        Next

    End Sub

    Private Sub sbDisplay_Update()

        Dim aryRst As New ArrayList
        Dim strBcNo As String = ""
        Dim sTestCd As String = ""
        Dim strOrgRst As String = ""
        Dim strViewRst As String = ""
        Dim sRstCmt As String = ""
        Dim strChk As String = ""
        Dim strIUD As String = ""

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk") : strChk = .Text
                .Col = .GetColFromID("iud") : strIUD = .Text
                .Col = .GetColFromID("orgrst") : strOrgRst = .Text
                .Col = .GetColFromID("viewrst") : strViewRst = .Text
                .Col = .GetColFromID("rstcmt") : sRstCmt = .Text
                .Col = .GetColFromID("testcd") : sTestCd = .Text
                .Col = .GetColFromID("bcno") : strBcNo = .Text

                Dim objRstInfo As New RST_INFO

                With objRstInfo
                    .BcNo = strBcNo
                    .Chk = strChk
                    .IUD = strIUD
                    .TestCd = sTestCd
                    .BcNo = strBcNo
                    .OrgRst = strOrgRst
                    .ViewRst = strViewRst
                    .RstCmt = sRstCmt
                End With

                aryRst.Add(objRstInfo)
            Next
        End With


        Dim dt As New DataTable
        dt = LISAPP.COMM.RstFn.fnGet_Result_bcno(msBcNo, msPartSlip, Me.chkBcnoAll.Checked, msTestCds, msWkGrpCd, msEqCd)

        sbDisplay_ResultView(dt)

        With spdResult
            For introw = 1 To .MaxRows
                For intidx = 0 To aryRst.Count - 1
                    .Row = introw
                    .Col = .GetColFromID("testcd") : sTestCd = .Text
                    .Col = .GetColFromID("bcno") : strBcNo = .Text

                    If strBcNo = CType(aryRst.Item(intidx), RST_INFO).BcNo And sTestCd = CType(aryRst(intidx), RST_INFO).TestCd Then
                        .Row = introw : .Col = .GetColFromID("chk") : .Text = CType(aryRst.Item(intidx), RST_INFO).Chk
                        .Row = introw : .Col = .GetColFromID("iud") : .Text = CType(aryRst.Item(intidx), RST_INFO).IUD
                        .Row = introw : .Col = .GetColFromID("orgrst") : .Text = CType(aryRst.Item(intidx), RST_INFO).OrgRst
                        .Row = introw : .Col = .GetColFromID("viewrst") : .Text = CType(aryRst.Item(intidx), RST_INFO).ViewRst
                        .Row = introw : .Col = .GetColFromID("rstcmt") : .Text = CType(aryRst.Item(intidx), RST_INFO).RstCmt

                        If CType(aryRst.Item(intidx), RST_INFO).OrgRst <> "" And .RowHidden Then
                            .RowHidden = True
                        End If
                        Exit For
                    End If
                Next

            Next

        End With
    End Sub


    Private Sub spdResult_KeyUpEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdResult.KeyUpEvent
        Dim sFn As String = "Sub spdOrdList_KeyUpEvent(Object, AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdOrdListR.KeyUpEvent"

        Select Case Convert.ToInt32(e.keyCode)
            Case 37, 38, 39, 40, 229 ' 화살표 키                
            Case 27     ' ESC
            Case Keys.F4, Keys.F9, Keys.F11, Keys.F12
            Case 13
            Case Else
                msObJName = spdResult.Name
                With Me.spdResult
                    If .ActiveCol <> .GetColFromID("orgrst") Then
                        Exit Sub
                    End If
                    .Row = .ActiveRow
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                    .Col = .GetColFromID("orgrst") : Dim sRst As String = .Text

                    DP_Common.sbDispaly_test_rstcd(m_dt_RstCdHelp, Convert.ToString(sTestCd), lstCode)  ' 검사항목별 결과코드 표시
                    DP_Common.sbFindPosition(lstCode, Convert.ToString(sRst))

                    If pnlCode.Visible = False Then
                        If lstCode.Items.Count > 0 Then
                            pnlCode.Visible = True
                        Else
                            pnlCode.Visible = False
                        End If
                    End If
                End With
        End Select

    End Sub

    Private Sub spdResult_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdResult.LeaveCell

        If mbLeveCellGbn = False Then Return

        Dim sBcNo As String = ""
        Dim sTCdGbn As String = ""
        Dim sTestcd As String = ""
        Dim sSpcCd As String = ""
        Dim sTkDt As String = ""

        With spdResult

            If e.row > 0 And e.col = .GetColFromID("orgrst") Then
                .Row = e.row
                .Col = e.col

                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then .ForeColor = Color.White
            End If

            If e.newRow > 0 And e.newCol = .GetColFromID("orgrst") Then
                btnExmAdd.Enabled = False
                btnExmAdd.Enabled = False

                .Row = e.newRow
                .Col = .GetColFromID("testcd") : sTestcd = .Text.Substring(0, 5)
                sbDisplay_ExmAdd(sTestcd)

                .Row = e.newRow
                .Col = e.newCol
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then
                    .ForeColor = Color.Black
                End If

                .Row = e.newRow
                .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                If lblBcno.Text <> sBcNo Then
                    If Me.txtCmtCont.Text.Trim = vbCrLf Then Me.txtCmtCont.Text = ""

                    Me.lblBcno.Text = sBcNo

                    RaiseEvent ChangedBcNo(lblBcno.Text)
                End If
                sbDisplay_RegNm_Test(sTestcd)

                '-- 2008-12-24 Yej Add
                .Row = e.newRow
                .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
                .Col = .GetColFromID("mbttype") : Dim sMbTType As String = .Text
                .Col = .GetColFromID("titleyn") : Dim sTitleYN As String = .Text
                .Col = .GetColFromID("tcdgbn") : sTCdGbn = .Text
                .Col = .GetColFromID("testcd") : sTestcd = .Text
                .Col = .GetColFromID("spccd") : sSpcCd = .Text

                .Col = .GetColFromID("slipcd") : Dim sSlipCd As String = .Text

                RaiseEvent ChangedTestCd(sBcNo, sTestcd)

                For ix As Integer = 0 To Me.cboSlip.Items.Count - 1
                    Me.cboSlip.SelectedIndex = ix
                    If Ctrl.Get_Code(Me.cboSlip) = sSlipCd Then Exit For
                Next

                If sMbTType = "2" Then 'and (sTCdGbn = "S" Or sTCdGbn = "P")  Then
                    Me.lblTestCd.Text = sTestcd.Substring(0, 5)
                    Me.lblTnmd.Text = sTnmd
                    Me.lblSpccd.Text = sSpcCd
                Else
                    Me.lblTestCd.Text = ""
                    Me.lblTnmd.Text = ""
                End If

                sbDisplay_BcNo_Rst_Bac_One_testcd(Me.lblTestCd.Text)
                If spdBac.MaxRows > 0 Then

                    Dim sBacCd As String = Ctrl.Get_Code(Me.spdBac, "baccd", 1)
                    Dim sBacSeq As String = Ctrl.Get_Code(Me.spdBac, "bacseq", 1)

                    sTestcd = Ctrl.Get_Code(Me.spdBac, "testcd", 1)

                    sbDisplay_BcNo_Rst_Anti_One_Bac(sTestcd, sBacCd, sBacSeq)
                End If
                '-- 2008-12-24 End
            End If
        End With
    End Sub

    Private Sub spdResult_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdResult.RightClick
        Dim sFn As String = "Sub spdOrdListR_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdOrdListR.RightClick"
        Try
            'Dim objForm As Windows.Forms.Form
            Dim iRowNo As Integer = 0
            Dim strWBCRst As String = ""
            Dim sDiffCmt As String = ""

            Dim pntFrmXY As New Point
            Dim pntCtlXY As New Point

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(spdResult)

            With spdResult
                If e.col = .GetColFromID("orgrst") And e.row > 0 Then
                    .Row = e.row
                    .Col = .GetColFromID("testcd") : Dim sTestCd_p As String = .Text : If sTestCd_p <> "" Then sTestCd_p = sTestCd_p.Substring(0, 5)
                    .Col = .GetColFromID("spccd") : Dim strSpcCd As String = .Text

                    Dim strFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(sTestCd_p, strSpcCd)

                    If strFormGbn <> "" Then sbDisplay_KeyPad(strFormGbn, sTestCd_p, strSpcCd)
                End If
            End With
        Catch ex As Exception
            sbLog_Exception(ex.Message)
        End Try

    End Sub

    Private Sub spdResult_ScriptCustomFunction(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ScriptCustomFunctionEvent) Handles spdResult.ScriptCustomFunction

    End Sub

    Private Sub spdResult_TextTipFetch(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles spdResult.TextTipFetch
        With spdResult
            Select Case e.col
                Case .GetColFromID("normal")
                    e.showTip = True
                    .Col = e.col
                    .Row = e.row
                    e.tipText = .CellNote
                Case .GetColFromID("reftxt")
                    e.showTip = True

                    Dim sHelp As String = fnGetTextTipFetch(spdResult, e.row)

                    e.tipText = sHelp

                    Dim asHelp() As String = Split(sHelp, vbCrLf)
                    Dim sMaxHelp As Single
                    Dim sHelpWidth As Single
                    For iRow As Integer = 0 To UBound(asHelp)
                        sHelpWidth = Me.CreateGraphics.MeasureString(asHelp(iRow), .Font).Width
                        If sMaxHelp < sHelpWidth Then
                            sMaxHelp = sHelpWidth
                        End If
                        '
                    Next
                    e.tipWidth = CInt(sMaxHelp) * 14
            End Select

        End With

    End Sub

    Private Sub lstCode_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCode.DoubleClick

        Dim strRstCd As String = ""
        Dim arlRstCd() As String

        Dim strRst As String = ""
        Dim sRstCmt As String = ""
        Try
            Select Case msObJName
                Case "spdResult"
                    With spdResult
                        If lstCode.SelectedIndex > -1 Then
                            For i As Integer = 0 To lstCode.SelectedIndices.Count - 1
                                arlRstCd = Split(lstCode.Items(lstCode.SelectedIndices(i)).ToString(), Chr(9))
                                strRst = arlRstCd(1)
                                If arlRstCd(2).Trim <> "" Then
                                    sRstCmt = arlRstCd(2)
                                End If
                            Next

                            .Row = .ActiveRow
                            .Col = .GetColFromID("orgrst") : .Text = strRst.Replace("'", "`")
                            If .GetColFromID("rstcmt") > 0 Then
                                .Col = .GetColFromID("rstcmt") : .Text = sRstCmt
                            End If
                        End If
                        If .GetColFromID("orgrst") > 0 Then
                            .Row = .ActiveRow
                            .Col = .GetColFromID("orgrst") : strRst = .Text.Replace("'", "`")
                            .Col = .GetColFromID("viewrst") : .Text = strRst
                        End If

                        sbSet_ResultView(spdResult.ActiveRow)

                        .Col = .GetColFromID("orgrst")
                        .Focus()
                    End With

                Case "txtComment"
                    With Me.txtCmtCont
                        Dim strTmp1 As String = ""
                        Dim strTmp2 As String = ""
                        Dim intIdxS As Integer = 0
                        Dim intIdxE As Integer = 0

                        If .SelectionLength = .Text.Length Then
                            .Text = ""
                        ElseIf .SelectionLength = 0 Then
                            intIdxS = .SelectionStart
                            intIdxE = .SelectionStart

                        Else
                            intIdxS = .SelectionStart
                            intIdxE = .SelectionStart + .SelectionLength + 1
                        End If

                        If .Text = "" Then
                        Else
                            If intIdxS = 0 Then
                                strTmp1 = ""
                                strTmp2 = Chr(13) + Chr(10) + .Text.Substring(intIdxE)
                            Else
                                strTmp1 = .Text.Substring(0, intIdxS)
                                strTmp2 = .Text.Substring(intIdxE)
                            End If
                        End If

                        .Text = strTmp1
                        For intIdx As Integer = 0 To lstCode.Items.Count - 1
                            If lstCode.GetSelected(intIdx) Then
                                arlRstCd = Split(lstCode.Items(intIdx).ToString(), Chr(9))
                                .Text += arlRstCd(1) + Chr(13) + Chr(10)
                            End If
                        Next
                        .Text = .Text.Substring(0, .Text.Length - 2)
                        .Text += strTmp2

                        .Focus()
                    End With
            End Select
            lstCode.Items.Clear()
            lstCode.Hide()
            pnlCode.Visible = False

        Catch ex As Exception

        End Try

    End Sub

    Private Sub lstCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstCode.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.Escape
                lstCode.Hide()
                pnlCode.Visible = False
            Case Windows.Forms.Keys.Enter
                lstCode_DoubleClick(lstCode, New System.EventArgs())
        End Select

    End Sub

    Private Sub btnExamAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExmAdd.Click

        Try

            With spdResult
                .Row = .ActiveRow
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text.Substring(0, 5)
                .Col = .GetColFromID("spccd") : Dim sSpccd As String = .Text

                Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_WithParent(sTestCd, sSpccd)
                Dim a_dr As DataRow() = dt.Select("viwsub = '0'", "")
                dt = Fn.ChangeToDataTable(a_dr)

                Dim objHelp As New CDHELP.FGCDHELP01
                Dim alList As New ArrayList

                objHelp.FormText = "추가항목"
                objHelp.MaxRows = 15

                objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
                objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

                alList = objHelp.Display_Result(moForm, btnExmAdd.Left, btnExmAdd.Top, dt)

                If alList.Count > 0 Then
                    For intidx As Integer = 0 To alList.Count - 1
                        For introw As Integer = 1 To .MaxRows
                            .Row = introw
                            .Col = .GetColFromID("testcd")

                            If alList.Item(intidx).ToString.Split("|"c)(1) = .Text Then
                                .Row = introw
                                .RowHidden = False
                            End If
                        Next
                    Next
                End If

            End With

        Catch ex As Exception
            sbLog_Exception("btnExamAdd_Click : " + ex.Message)
        End Try
    End Sub

    Private Sub axCalcRst_OnSelectedCalcRstInfos(ByVal r_al As System.Collections.ArrayList) Handles axCalcRst.OnSelectedCalcRstInfos
        sbDisplayCalRst_Info(r_al)
    End Sub

    Private Sub AxRstInput_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        RaiseEvent FunctionKeyDown(sender, New System.Windows.Forms.KeyEventArgs(e.KeyCode))
    End Sub

    Private Sub btnHelp_Cmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_Cmt.Click
        Dim sFn As String = "Handles btnHelp_Cmt.Click"

        If Ctrl.Get_Code(Me.cboSlip) = "" Then Return

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim arlList As New ArrayList

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_cmtcont_slip(Ctrl.Get_Code(Me.cboSlip), Me.txtCmtCd.Text)

            objHelp.FormText = "소견정보"

            objHelp.GroupBy = ""
            objHelp.OrderBy = ""
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("'' chk", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("cmtcd", "코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("cmtcont", "내용", 60, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(btnHelp_Cmt)

            arlList = objHelp.Display_Result(moForm, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + btnHelp_Cmt.Height + 80, dt)

            Dim sCmtCont As String = ""

            If arlList.Count > 0 Then

                For intIdx = 0 To arlList.Count - 1
                    If intIdx <> 0 Then sCmtCont += vbCrLf
                    sCmtCont += arlList.Item(intIdx).ToString.Split("|"c)(1)
                Next
            End If

            If sCmtCont <> "" Then
                If Me.txtCmtCont.Text = "" Then
                    Me.txtCmtCont.Text += sCmtCont
                Else
                    If Me.txtCmtCont.Text.Substring(Me.txtCmtCont.Text.Length - 1) = vbCrLf Then
                        Me.txtCmtCont.Text += sCmtCont
                    Else
                        Me.txtCmtCont.Text += vbCrLf + sCmtCont
                    End If

                End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnKeyPad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeyPad.Click

        Dim arlTcls As New ArrayList

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("testcd") : Dim sTestCd_p As String = .Text : If sTestCd_p <> "" Then sTestCd_p = sTestCd_p.Substring(0, 5)
                .Col = .GetColFromID("spccd") : Dim strSpcCd As String = .Text

                If arlTcls.Contains(sTestCd_p) Then
                Else
                    arlTcls.Add(sTestCd_p)

                    Dim strFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(sTestCd_p, strSpcCd)
                    sbDisplay_KeyPad(strFormGbn, sTestCd_p, strSpcCd)
                End If
            Next
        End With

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        sbGet_BacGenCd()    '-- 균속정보

        sbDisplayInit_btnDebug()
        spdResult.ColsFrozen = spdResult.GetColFromID("tnmd")
        m_dbl_RowHeightt = spdResult.get_RowHeight(1)

    End Sub

    Private Sub spdAnti_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdAnti.ButtonClicked
        spd_Change(spdAnti, New AxFPSpreadADO._DSpreadEvents_ChangeEvent(e.col, e.row))

    End Sub

    Private Sub spd_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdBac.Change, spdAnti.Change
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        Ctrl.ChangeColor(CType(sender, AxFPSpreadADO.AxfpSpread), e.col, e.row)

        Select Case CType(sender, AxFPSpreadADO.AxfpSpread).Name.ToLower.Substring(3)
            Case "bac"
                sbDisplay_Change_Bac(e.col, e.row)

            Case "anti"
                If mbBac_ClickEvent Then Return

                sbDisplay_Change_Anti(e.col, e.row)

        End Select
    End Sub

    Private Sub spdBac_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdBac.ClickEvent
        With spdBac
            .Row = e.row
            .Col = e.col : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            .Focus()
        End With

    End Sub

    Private Sub spdBac_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBac.GotFocus
        If Me.spdBac.MaxRows < 1 Then Return

        If Me.pnlCode.Visible Then Me.pnlCode.Visible = False

        With spdBac
            .Row = 1 : .Col = .GetColFromID("testcd") : Me.lblTestCd.Text = .Text
        End With

    End Sub

    Private Sub spdBac_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdBac.KeyDownEvent
        Select Case CType(e.keyCode, Windows.Forms.Keys)
            Case Keys.Enter
                spdBac_ClickEvent(spdBac, New AxFPSpreadADO._DSpreadEvents_ClickEvent(spdBac.ActiveCol + 1, spdBac.ActiveRow))

        End Select
    End Sub

    Private Sub spdBac_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdBac.LeaveCell
        If e.col < 1 Then Return
        If e.row < 1 Then Return
        If e.newCol < 1 Then Return
        If e.newRow < 1 Then Return
        If e.row = e.newRow Then Return

        '선택된 균에 대한 항균제 내역 표시
        Dim spd_b As AxFPSpreadADO.AxfpSpread = Me.spdBac

        Dim sTestCd As String = Ctrl.Get_Code(spd_b, "testcd", e.newRow)
        Dim sBacCd As String = Ctrl.Get_Code(spd_b, "baccd", e.newRow)
        Dim sBacSeq As String = Ctrl.Get_Code(spd_b, "bacseq", e.newRow)

        Try
            mbBac_ClickEvent = True
            sbDisplay_BcNo_Rst_Anti_One_Bac(sTestCd, sBacCd, sBacSeq)

        Catch ex As Exception
        Finally
            mbBac_ClickEvent = False
        End Try

    End Sub

    Private Sub btnAddA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddA.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        sbDisplay_Popup_AntiCd()

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnAddB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddB.Click
        '-- 2007/09/27 ssh - 배양/분리균 항목이 없을 경우 추가 불가능 하게 수정함.
        If miMbtTypeFlag = 0 Or Me.lblTestCd.Text = "" Then Exit Sub

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Me.pnlCode.Visible = False

        sbDisplay_Popup_BacCd(sender)

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnChgB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChgB.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        sbDisplay_Popup_BacCd(sender)

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnDebug_AntiBak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDebug_AntiBak.Click
        Dim dt As DataTable
        Dim fdebug As New FDEBUG

        fdebug.TopMost = True

        dt = m_dt_Anti_BcNo_Bak

        With fdebug.spd
            .MaxCols = dt.Columns.Count
            .MaxRows = dt.Rows.Count

            For j As Integer = 1 To dt.Columns.Count
                .SetText(j, 0, dt.Columns(j - 1).ColumnName)
            Next

            For i As Integer = 1 To dt.Rows.Count
                For j As Integer = 1 To dt.Columns.Count
                    .SetText(j, i, dt.Rows(i - 1).Item(j - 1).ToString())
                Next
            Next
        End With

        fdebug.Show()

    End Sub

    Private Sub btnDebug_AntiCur_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDebug_AntiCur.Click
        Dim dt As DataTable
        Dim fdebug As New FDEBUG

        fdebug.TopMost = True

        dt = m_dt_Anti_BcNo

        With fdebug.spd
            .MaxCols = dt.Columns.Count
            .MaxRows = dt.Rows.Count

            For j As Integer = 1 To dt.Columns.Count
                .SetText(j, 0, dt.Columns(j - 1).ColumnName)
            Next

            For i As Integer = 1 To dt.Rows.Count
                For j As Integer = 1 To dt.Columns.Count
                    .SetText(j, i, dt.Rows(i - 1).Item(j - 1).ToString())
                Next
            Next
        End With

        fdebug.Show()

    End Sub

    Private Sub btnDebug_Bac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDebug_Bac.Click
        Dim dt As DataTable
        Dim fdebug As New FDEBUG

        fdebug.TopMost = True

        dt = m_dt_Bac_BcNo

        With fdebug.spd
            .MaxCols = dt.Columns.Count
            .MaxRows = dt.Rows.Count

            For j As Integer = 1 To dt.Columns.Count
                .SetText(j, 0, dt.Columns(j - 1).ColumnName)
            Next

            For i As Integer = 1 To dt.Rows.Count
                For j As Integer = 1 To dt.Columns.Count
                    .SetText(j, i, dt.Rows(i - 1).Item(j - 1).ToString())
                Next
            Next
        End With

        fdebug.Show()

    End Sub

    Private Sub btnDelA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelA.Click
        If Me.spdBac.MaxRows < 1 Then Return
        If Me.spdBac.ActiveRow < 1 Then Return
        If Me.spdAnti.MaxRows < 1 Then Return
        If Me.spdAnti.ActiveRow < 1 Then Return


        With Me.spdAnti
            If Me.chkAntiAll.Checked Then
                sbDel_Anti(0)
            Else
                For iAntirow As Integer = 1 To .MaxRows '<20130116 체크삭제 기능 추가
                    .Col = .GetColFromID("rptyn")
                    .Row = iAntirow
                    Dim schk As String = .Text

                    If schk = "1" Then
                        'sbDel_Anti(.Row)
                        sbDel_chk_Anti(.Row)
                        iAntirow -= 1
                    End If

                Next

            End If
        End With
        

        'TestMethod 변경 spdBac에 표시
        sbDisplay_Change_Test_Method()

        '변경여부 배양(동정)검사에 표시
        sbDisplay_Change_Rst_Micro()

    End Sub

    Private Sub btnDelB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelB.Click
        If Me.spdBac.MaxRows < 1 Then Return
        If Me.spdBac.ActiveRow < 1 Then Return

        sbDel_Bac(Me.spdBac.ActiveRow)

        '변경여부 배양(동정)검사에 표시
        sbDisplay_Change_Rst_Micro()
    End Sub

    Private Sub btnDownB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownB.Click
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac
        With spd

            If .MaxRows = 0 Then Return

            Dim iNext As Integer = 0

            If CType(sender, Windows.Forms.Button).Name.ToLower.EndsWith("upb") Then
                If .ActiveRow = 1 Then Return

                iNext -= 1
            Else
                If .ActiveRow = .MaxRows Then Return

                iNext += 1
            End If

            Dim iActiveRow As Integer = .ActiveRow

            .SetText(.GetColFromID("ranking"), iActiveRow, (iActiveRow + iNext).ToString())
            spd_Change(spd, New AxFPSpreadADO._DSpreadEvents_ChangeEvent(.GetColFromID("ranking"), iActiveRow))

            .SetText(.GetColFromID("ranking"), iActiveRow + iNext, iActiveRow.ToString())
            spd_Change(spd, New AxFPSpreadADO._DSpreadEvents_ChangeEvent(.GetColFromID("ranking"), iActiveRow + iNext))

            .SetActiveCell(.GetColFromID("bacnmd"), iActiveRow + iNext)

            'Ranking순으로 정렬해 화면 표시
            .Col = 1
            .Col2 = .MaxCols
            .Row = 1
            .Row2 = .MaxRows

            .SortBy = FPSpreadADO.SortByConstants.SortByRow
            .set_SortKey(1, .GetColFromID("ranking"))
            .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
            .Action = FPSpreadADO.ActionConstants.ActionSort
        End With
    End Sub

    Private Sub btnUpB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpB.Click
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBac

        With spd
            If .MaxRows = 0 Then Return

            Dim iNext As Integer = 0

            If CType(sender, Windows.Forms.Button).Name.ToLower.EndsWith("upb") Then
                If .ActiveRow = 1 Then Return

                iNext -= 1
            Else
                If .ActiveRow = .MaxRows Then Return

                iNext += 1
            End If

            .SetText(.GetColFromID("ranking"), .ActiveRow, (.ActiveRow + iNext).ToString())
            spd_Change(spd, New AxFPSpreadADO._DSpreadEvents_ChangeEvent(.GetColFromID("ranking"), .ActiveRow))

            .SetText(.GetColFromID("ranking"), .ActiveRow + iNext, .ActiveRow.ToString())
            spd_Change(spd, New AxFPSpreadADO._DSpreadEvents_ChangeEvent(.GetColFromID("ranking"), .ActiveRow + iNext))

            .SetActiveCell(.GetColFromID("bacnmd"), .ActiveRow + iNext)

            'Ranking순으로 정렬해 화면 표시
            .Col = 1
            .Col2 = .MaxCols
            .Row = 1
            .Row2 = .MaxRows

            .SortBy = FPSpreadADO.SortByConstants.SortByRow
            .set_SortKey(1, .GetColFromID("ranking"))
            .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
            .Action = FPSpreadADO.ActionConstants.ActionSort
        End With
    End Sub

    Private Sub spdBac_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdBac.LostFocus

        If Me.lblTestCd.Text Is Nothing Then Return
        If Me.lblTestCd.Text = "" Then Return

        With spdResult

            For iRow As Integer = 1 To .MaxRows
                .Row = iRow
                .Col = .GetColFromID("testcd")
                If .Text = Me.lblTestCd.Text Then
                    sbAlertCheck(iRow)
                    Exit For
                End If
            Next
        End With

    End Sub

    Private Sub btnCmt_Clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCmt_Clear.Click

        Me.txtCmtCont.Text = ""
        Dim ci As New CMT_INFO

        With ci
            .BcNo = msBcNo
            .PartSlip = Ctrl.Get_Code(cboSlip)
            .CmtCont = Me.txtCmtCont.Text
        End With

        sbSet_Cmt_BcNo_Edit(ci)

    End Sub

    Private Sub picMultiMicro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picMultiMicro.Click
        If Me.picMultiMicro.Image Is Nothing Then Return

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        sbDisplay_Multi_Rst_Micro()

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub spdAnti_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdAnti.ClickEvent

        ImeModeBase = Windows.Forms.ImeMode.Alpha

        With spdAnti
            .Row = e.row
            .Col = e.col : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            .Focus()
        End With

    End Sub

    Private Sub spdAnti_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdAnti.GotFocus
        If Me.spdAnti.MaxRows < 1 Then Return

        With spdAnti
            .Row = 1 : .Col = .GetColFromID("testcd") : Me.lblTestCd.Text = .Text
        End With

    End Sub

    Private Sub spdAnti_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdAnti.KeyDownEvent

        Select Case CType(e.keyCode, System.Windows.Forms.Keys)
            Case Keys.Enter
                If spdAnti.ActiveRow = spdAnti.MaxRows Then
                    spdAnti_ClickEvent(spdAnti, New AxFPSpreadADO._DSpreadEvents_ClickEvent(spdAnti.ActiveCol + 1, spdAnti.ActiveRow))
                End If
        End Select

    End Sub

    Public WriteOnly Property PartSlip() As String
        Set(ByVal value As String)
            msPartSlip = value
        End Set
    End Property

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbDisplay_Cmt_One_slipcd(Ctrl.Get_Code(Me.cboSlip))
    End Sub

    Private Sub txtCmtCont_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCmtCont.LostFocus
        'And msPartSlip = "" 
        If Ctrl.Get_Code(cboSlip) = "" And msPartSlip = "" Then Return
        '20120-01-08
        Dim sPartslip As String = ""
        If Ctrl.Get_Code(cboSlip) <> "" Then
            sPartslip = Ctrl.Get_Code(cboSlip)
        ElseIf msPartSlip <> "" Then
            sPartslip = msPartSlip
        End If
        Dim ci As New CMT_INFO

        With ci
            .BcNo = msBcNo
            .PartSlip = sPartslip
            .CmtCont = Me.txtCmtCont.Text
        End With

        sbSet_Cmt_BcNo_Edit(ci)

    End Sub

    Private Sub txtCmtCd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCmtCd.GotFocus
        Me.txtCmtCd.SelectionStart = 0
        Me.txtCmtCd.SelectAll()
    End Sub

    Private Sub txtCmtCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        btnHelp_Cmt_Click(Nothing, Nothing)

        Me.txtCmtCd.Text = ""
    End Sub

    Private Sub btnReg_UnFit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_UnFit.Click
        Dim sFn As String = "Handles btnReg_UnFit.Click"

        Dim alTclsCds As New ArrayList

        With spdResult
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("tclscd") : Dim sTclsCd As String = .Text

                If sChk = "1" And alTclsCds.Contains(sTclsCd) = False Then
                    alTclsCds.Add(sTclsCd)
                End If
            Next

        End With


        Dim frmChild As Windows.Forms.Form
        frmChild = New FGUNFITSPC(msBcNo, alTclsCds)

        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()
    End Sub

    Private Sub btnKeyPad_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeyPad.Click
        Dim arlTcls As New ArrayList

        With spdResult
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("testcd") : Dim sTclsCd As String = .Text : If sTclsCd <> "" Then sTclsCd = sTclsCd.Substring(0, 5)
                .Col = .GetColFromID("spccd") : Dim strSpcCd As String = .Text

                If arlTcls.Contains(sTclsCd) Then
                Else
                    arlTcls.Add(sTclsCd)

                    Dim strFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(sTclsCd, strSpcCd)
                    sbDisplay_KeyPad(strFormGbn, sTclsCd, strSpcCd)
                End If
            Next
        End With
    End Sub

    Private Sub btnReg_Abn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_Abn.Click
        Dim sFn As String = "Handles btnReg_UnFit.Click"

        Dim alTclsCds As New ArrayList

        With spdResult
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                If sChk = "1" And alTclsCds.Contains(sTestCd) = False Then
                    alTclsCds.Add(sTestCd)
                End If
            Next

        End With


        Dim frmChild As Windows.Forms.Form
        frmChild = New FGABNORMAL(msBcNo, msPartSlip, False, True, alTclsCds)

        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()
    End Sub

    Private Sub spdAnti_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdAnti.LostFocus
        spdBac_LostFocus(Nothing, Nothing)

    End Sub

    Private Sub mnuSpRst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSpRst.Click
        If Me.txtTestCd.Text = "" Then Return

        Dim sSpRstYn As String = LISAPP.COMM.RstFn.fnGet_SpRst_yn(msBcNo, Me.txtTestCd.Text.Substring(0, 5))
        If sSpRstYn = "" Then Return

        RaiseEvent Call_SpRst(msBcNo, Me.txtTestCd.Text.Substring(0, 5))
    End Sub

    Private Sub mnuKeypad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuKeypad.Click

        If Me.txtTestCd.Text = "" Then
            MsgBox("검사항목이 선택되지 않았습니다.!!" + vbCrLf + "다시 클릭 후 실행하세요.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information)
            Return
        End If

        With Me.spdResult
            .Row = .ActiveRow
            .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text

            Dim sFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(Me.txtTestCd.Text.Substring(0, 5), sSpcCd)
            If sFormGbn <> "" Then sbDisplay_KeyPad(sFormGbn, Me.txtTestCd.Text.Substring(0, 5), sSpcCd)

        End With
    End Sub

    Private Sub btnSend_sms_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend_sms.Click
        Dim oForm = New FGSMSSEND()
        Dim sSMSMsg As String = ""
        Dim sTestGbn As String = ""

        With spdResult
            For ix As Integer = 1 To .MaxRows
                .Row = ix

                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                If sTestCd = "LM205" Then
                    sTestGbn = "LM205"
                    Exit For
                End If
            Next
        End With
        '< 20121009 메시지 수정요구
        If sTestGbn = "LM205" Then
            sSMSMsg = "[Critical value 통보] 등록번호[" + msRegNo + "]  이름[" + msPatNm + "] AFB stain 최종보고 결과를 확인해주세요." + vbCrLf
        Else
            sSMSMsg = "[Critical value 통보] 등록번호[" + msRegNo + "]  이름[" + msPatNm + "] 혈액배양 중간보고 결과를 확인해주세요." + vbCrLf
        End If

        'sSMSMsg = "등록번호[" + msRegNo + "] 환자이름[" + msPatNm + "]의 혈액배양 중간보고 결과를 확인 해주세요." + vbCrLf
        '> 20121009

        oForm.Display_Result(moForm, msBcNo, sSMSMsg)
    End Sub

    Private Sub btnQryFNModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQryFNModify.Click
        Dim objForm As New FGMODIFY
        objForm.Display_Data(moForm, msBcNo)
    End Sub

    Private Sub btnCVRsend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCVRsend.Click

        Try

            Dim Info_arry As New ArrayList
            With spdResult

                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("iud") : Dim chk As String = .Text

                    If chk = "1" Then

                        Dim CvrInfo As New LIS_CVR_INFO

                        .Col = .GetColFromID("tnmd") : Dim tnmd As String = .Text
                        .Col = .GetColFromID("bcno") : Dim bcno As String = .Text
                        .Col = .GetColFromID("tclscd") : Dim tclscd As String = .Text
                        .Col = .GetColFromID("testcd") : Dim testcd As String = .Text
                        .Col = .GetColFromID("viewrst") : Dim rst As String = .Text
                        .Col = .GetColFromID("rstflg") : Dim rstflg As String = .Text
                        .Col = .GetColFromID("tcdgbn") : Dim tcdgbn As String = .Text
                        .Col = .GetColFromID("titleyn") : Dim titleyn As String = .Text
                        .Col = .GetColFromID("rstunit") : Dim rstunit As String = .Text

                        If ((tcdgbn = "C" And rstflg = "") Or _
                            (tcdgbn = "B" And titleyn = "1") Or _
                            (tcdgbn = "P" And titleyn = "1")) Or rst = "" Then

                            Continue For
                        End If

                        '20210830 JHS 특정 검사는 cvr등록시에 부모검사 항목명을 가져와 등록
                        If PRG_CONST.CVR_P_testCd(testcd.Substring(0, 5)) <> "" Then
                            Dim dt_PTnmd As DataTable = LISAPP.COMM.RstFn.Fnget_tnmd(bcno, tclscd)
                            tnmd = dt_PTnmd.Rows(0).Item("tnmd").ToString + tnmd
                        End If
                        '---------------------------------------------------

                        Dim dt As DataTable = LISAPP.COMM.RstFn.Fnget_Fkocs(bcno, tclscd)
                            Dim fkocs As String = ""
                            Dim orddt As String = ""

                            If dt.Rows.Count > 0 Then
                                fkocs = dt.Rows(0).Item("ocs_key").ToString
                                orddt = dt.Rows(0).Item("orddt").ToString
                            End If

                            CvrInfo.Orddt = orddt
                            CvrInfo.Fkocs = fkocs
                            CvrInfo.Tnmd = tnmd
                            CvrInfo.Testcd = testcd
                            CvrInfo.Rst = rst
                            CvrInfo.RstUnit = rstunit

                            Dim a_dr As DataRow()
                            a_dr = m_dt_RstUsr.Select("testcd = '" + testcd + "'")

                            Dim Rstdt As String = ""
                            Dim Rstid As String = ""

                            Select Case rstflg

                                Case "1"
                                    Rstdt = a_dr(0).Item("regdt").ToString.Replace("-", "").Replace(":", "").Replace(" ", "")
                                    Rstid = a_dr(0).Item("regid").ToString
                                Case "2"
                                    Rstdt = a_dr(0).Item("mwdt").ToString.Replace("-", "").Replace(":", "").Replace(" ", "")
                                    Rstid = a_dr(0).Item("mwid").ToString
                                Case "3"
                                    Rstdt = a_dr(0).Item("fndt").ToString.Replace("-", "").Replace(":", "").Replace(" ", "")
                                    Rstid = a_dr(0).Item("fnid").ToString

                            End Select

                            CvrInfo.Rstdt = Rstdt
                            CvrInfo.Rstid = Rstid

                            Info_arry.Add(CvrInfo)

                        End If

                Next

                '<< CVR 등록
                If Info_arry.Count > 0 Then

                    With (New LISAPP.APP_R.AxRstFn)
                        If .fnExe_CVR(msBcNo, msRegNo, Info_arry, USER_INFO.USRID, USER_INFO.USRNM) = "" Then
                            MsgBox("CVR 등록되었습니다.")
                        End If
                    End With

                Else
                    MsgBox("CVR 등록할 검사항목을 선택하세요.")
                End If

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnCVRList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCVRList.Click

        Try

            Dim frm As New AxAckResult.FPOPUP_CVR
            frm.Display_Data(frm, msBcNo)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

   
End Class

Friend Class BacInfo
    Public OldBacCd As String = ""
    Public OldIncRst As String = ""
    Public OldBacCmt As String = ""
    Public OldRanking As String = ""

    Public BcNo As String = ""

    Public TestCd As String = ""
    Public BacGenCd As String = ""
    Public BacCd As String = ""
    Public BacNmD As String = ""
    Public BacSeq As String = ""
    Public TestMtd As String = ""
    Public IncRst As String = ""
    Public bacCmt As String = ""

    Public ranking As String = ""

End Class

Friend Class AntiInfo
    Public TestCd As String = ""
    Public BacCd As String = ""
    Public BacSeq As String = ""
    Public AntiCd As String = ""
    Public AntiNmD As String = ""
    Public TestMtd As String = ""
    Public DecRst As String = ""
    Public AntiRst As String = ""
    Public RefR As String = ""
    Public RefS As String = ""
    Public RptYn As String = ""
End Class


