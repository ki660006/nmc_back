Imports COMMON.CommFN
Imports System.Drawing

Public Class SPCLIST03
    Inherits System.Windows.Forms.UserControl

    Public RegNo As String = ""
    Public SearchDayS As String = ""
    Public SearchDayE As String = ""

    Public Event ChangeSelectedRow(ByVal r_al_bcno As ArrayList, ByVal r_al_TOrdSlip As ArrayList) 
    Public Event DoubleClickRow(ByVal riCol As Integer, ByVal riRow As Integer)

    Private Const mc_sState_Final As String = "최종보고"

    Private mbUseDebug As Boolean = False
    Private mbUseTempRstState As Boolean = False

    Private miUseMode As Integer = 0 

    '< yjlee
    Private mbChkUseMode As Boolean = False

    Private m_dt_bcno As DataTable

    Public ReadOnly Property CurrentBcno() As String
        Get
            Return Me.txtBcNoBuf.Text.Substring(0, txtBcNoBuf.Text.IndexOf(":"))
        End Get
    End Property

    Public ReadOnly Property CurrentRow() As Integer
        Get
            Return Me.spdList.ActiveRow
        End Get
    End Property

    Public ReadOnly Property CurrentState() As String
        Get
            Return Ctrl.Get_Code(Me.spdList, "state", Me.spdList.ActiveRow)
        End Get
    End Property

    Public ReadOnly Property RowCount() As Integer
        Get
            Return Me.spdList.MaxRows
        End Get
    End Property

    Public ReadOnly Property SelectedBcNoList() As ArrayList
        Get
            Return Ctrl.FindCheckedItem(Me.spdList, Me.spdList.GetColFromID("chk"), Me.spdList.GetColFromID("bcno"))
        End Get
    End Property

    Public ReadOnly Property SelectedBcNoList_Order() As ArrayList
        Get
            Return fnGet_BcNo_Order_Select()
        End Get
    End Property

    Public Property UseDebug() As Boolean
        Get
            Return mbUseDebug
        End Get
        Set(ByVal Value As Boolean)
            mbUseDebug = Value

            sbDisplayInit()
        End Set
    End Property

    Public Property UseMode() As Integer
        Get
            Return miUseMode
        End Get
        Set(ByVal Value As Integer)
            miUseMode = Value

            sbDisplayInit()
        End Set
    End Property

    Public Property UseTempRstState() As Boolean
        Get
            Return mbUseTempRstState
        End Get
        Set(ByVal Value As Boolean)
            mbUseTempRstState = Value
        End Set
    End Property

    Public Property CheckUseMode() As Boolean
        Get
            Return mbChkUseMode
        End Get
        Set(ByVal Value As Boolean)
            mbChkUseMode = Value

            sbDisplayInit()
        End Set
    End Property

    Public WriteOnly Property UseSPrst() As Boolean
        Set(ByVal Value As Boolean)
            sbDisplayInit(Value)
        End Set
    End Property

    Public Sub Clear()
        Dim sFn As String = "Sub Clear()"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With spd
                .ReDraw = False

                .LeftCol = .GetColFromID("chk")

                .MaxRows = 0
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        Finally
            spd.Hide()
            spd.Show()
            spd.ReDraw = True

        End Try
    End Sub

    Public Sub Display_OrderList()
        Dim sFn As String = "Sub Display_OrderList()"

        Try
            If RegNo = "" Then Return
            If SearchDayS = "" Then Return
            If SearchDayE = "" Then Return

            Me.ParentForm.Cursor = Windows.Forms.Cursors.WaitCursor

            Dim dt As New DataTable
            Dim dt_bcno As New DataTable

            If miUseMode = 0 Then
                '-- 처방일자별
                dt = LISAPP.APP_V.CommFn.fnGet_List_RegNo_Order(RegNo, SearchDayS, SearchDayE, dt_bcno, mbUseTempRstState)
            ElseIf miUseMode = 1 Then
                '-- 결과일자별
                dt = LISAPP.APP_V.CommFn.fnGet_List_RegNo_Result_WithSlipName(RegNo, SearchDayS, SearchDayE, mbUseTempRstState)
            ElseIf miUseMode = 2 Then
                dt = LISAPP.APP_V.CommFn.fnGet_List_RegNo_ResultOnly(RegNo, SearchDayS, SearchDayE, mbUseTempRstState)
            End If

            If dt.Rows.Count = 0 Then Return

            Dim sSort As String = ""

            If miUseMode = 0 Then
                sSort = "orddt desc, tkdt desc, tordslip, deptcd, doctornm, state, rstdt"
            ElseIf miUseMode = 1 Then
                sSort = "rstdt desc, orddt desc, deptnm, doctornm, bcno"
            ElseIf miUseMode = 2 Then
                sSort = "rstdt desc, orddt desc, tkdt desc, deptnm, doctornm, bcno"
            End If

            Dim a_dr() As DataRow = dt.Select("", sSort)

            If a_dr.Length = 0 Then Return

            sbDisplay_List(a_dr)

            If dt_bcno Is Nothing Then dt_bcno = New DataTable

            m_dt_bcno = dt_bcno.Copy()

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        Finally
            Me.ParentForm.Cursor = Windows.Forms.Cursors.Default

            sbDisplayInit_btnChk()

            Me.txtBcNoBuf.Text = ""

        End Try
    End Sub

    Public Sub Display_OrderList(ByVal rsRegNo As String, ByVal rsDayS As String, ByVal rsDayE As String)
        Dim sFn As String = "Sub Display_OrderList()"

        Try
            RegNo = rsRegNo
            SearchDayS = rsDayS
            SearchDayE = rsDayE

            Display_OrderList()

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub ReDraw()
        Dim sFn As String = "Sub ReDraw()"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With spd
                .ReDraw = False
                .LeftCol = .GetColFromID("chk")
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        Finally
            spd.Hide()
            spd.Show()
            spd.ReDraw = True

        End Try
    End Sub

    Public Function fnGet_BcNo_Order_Select() As ArrayList

        Dim sFn As String = "fnGet_BcNo_Order_Select"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList
        Try
            Dim al_bcno As New ArrayList

            For iRow As Integer = 1 To spd.MaxRows
                Dim sChk As String = Ctrl.Get_Code(spd, "chk", iRow)
                Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", iRow)
                Dim sRegNo As String = Ctrl.Get_Code(spd, "regno", iRow)
                Dim sOrdDt As String = Ctrl.Get_Code(spd, "orddt", iRow)
                Dim sTOrdSlip As String = Ctrl.Get_Code(spd, "tordslip", iRow)
                Dim sDeptNm As String = Ctrl.Get_Code(spd, "deptnm", iRow)
                Dim sDoctorNm As String = Ctrl.Get_Code(spd, "doctornm", iRow)

                Dim sFilter As String = ""
                Dim a_dr() As DataRow
                Dim bTran As Boolean = False

                If sChk = "1" Then
                    sFilter = ""
                    sFilter += "regno = '" + sRegNo + "'"
                    sFilter += " and orddt = '" + sOrdDt + "'"
                    sFilter += " and tordslip = '" + sTOrdSlip + "'"
                    'sFilter += " and deptnm = '" + sDeptNm + "'"

                    If sDeptNm = "" Then
                        sFilter += " and deptnm is null"
                    Else
                        sFilter += " and deptnm = '" + sDeptNm + "'"
                    End If
                    '-- 2009/06/18 yej 수정(마이그레이션 자료에는 의사명이 없음.)
                    If sDoctorNm = "" Then
                        sFilter += " and doctornm is null"
                    Else
                        sFilter += " and doctornm= '" + sDoctorNm + "'"
                    End If

                    a_dr = m_dt_bcno.Select(sFilter, "bcno asc")

                    If a_dr.Length > 0 Then
                        For i As Integer = 1 To a_dr.Length
                            If Not al_bcno.Contains(a_dr(i - 1).Item("bcno").ToString()) Then
                                al_bcno.Add(a_dr(i - 1).Item("bcno").ToString())
                            End If
                        Next
                    End If
                End If
            Next

            Return al_bcno

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        End Try
    End Function

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbDisplayInit()
    End Sub

    'UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Public WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnChk As System.Windows.Forms.Button
    Friend WithEvents txtBcNoBuf As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SPCLIST03))
        Me.pnl = New System.Windows.Forms.Panel
        Me.txtBcNoBuf = New System.Windows.Forms.TextBox
        Me.btnChk = New System.Windows.Forms.Button
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.pnl.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnl
        '
        Me.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl.Controls.Add(Me.txtBcNoBuf)
        Me.pnl.Controls.Add(Me.btnChk)
        Me.pnl.Controls.Add(Me.spdList)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Location = New System.Drawing.Point(0, 0)
        Me.pnl.Name = "pnl"
        Me.pnl.Size = New System.Drawing.Size(645, 576)
        Me.pnl.TabIndex = 0
        '
        'txtBcNoBuf
        '
        Me.txtBcNoBuf.Location = New System.Drawing.Point(16, 348)
        Me.txtBcNoBuf.Name = "txtBcNoBuf"
        Me.txtBcNoBuf.Size = New System.Drawing.Size(308, 21)
        Me.txtBcNoBuf.TabIndex = 2
        Me.txtBcNoBuf.Visible = False
        '
        'btnChk
        '
        Me.btnChk.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnChk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChk.Font = New System.Drawing.Font("새굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnChk.Location = New System.Drawing.Point(-1, -1)
        Me.btnChk.Name = "btnChk"
        Me.btnChk.Size = New System.Drawing.Size(18, 30)
        Me.btnChk.TabIndex = 0
        Me.btnChk.TabStop = False
        Me.btnChk.Tag = ""
        Me.btnChk.UseVisualStyleBackColor = False
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(644, 575)
        Me.spdList.TabIndex = 1
        '
        'SPCLIST03
        '
        Me.Controls.Add(Me.pnl)
        Me.Name = "SPCLIST03"
        Me.Size = New System.Drawing.Size(645, 576)
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbDisplay_List(ByVal ra_dr() As DataRow)
        Dim sFn As String = "sbDisplay_List"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With spd
                Ctrl.DisplayAfterSelect(spd, ra_dr, True, False)

                .ReDraw = False

                If miUseMode = 0 Then

                ElseIf miUseMode = 1 Then
                    sbDisplay_List_ChkFinal()
                ElseIf miUseMode = 2 Then

                End If

                .SetActiveCell(0, 0)
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplay_List_ChkFinal()
        Dim sFn As String = "sbDisplay_List_ChkFinal"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Dim sState As String = ""

        Try
            With spd
                For i As Integer = 1 To .MaxRows
                    sState = Ctrl.Get_Code(spd, "state", i)

                    '아이콘 지움 --> Cell을 StaticText로
                    If Not sState = mc_sState_Final Then
                        .Col = .GetColFromID("chk") : .Row = i : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    End If
                Next
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplayInit(Optional ByVal rbSpFlg As Boolean = False)
        Dim sFn As String = "sbDisplayInit"

        Try
            With Me.spdList
                .Font = New Font("굴림체", 9, FontStyle.Regular)

                .ShadowColor = Drawing.Color.FromArgb(165, 186, 222)
                .ShadowDark = Color.DimGray
                .ShadowText = SystemColors.ControlText

                .GrayAreaBackColor = Drawing.Color.FromArgb(236, 242, 255)

                Select Case miUseMode
                    Case 0
                        .RowHeadersShow = True
                    Case 1
                        .RowHeadersShow = False
                    Case 2
                        .RowHeadersShow = True
                End Select

                For j As Integer = .MaxCols To 1 Step -1
                    .Col = j

                    If j = .GetColFromID("regno") Then
                        If mbUseDebug Then
                            .ColHidden = False
                        Else
                            .ColHidden = True
                        End If
                    End If

                    If j = .GetColFromID("bcno") Then
                        If mbUseDebug Then
                            .ColHidden = False
                        Else
                            .ColHidden = True
                        End If
                    End If

                    If j = .GetColFromID("tordslip") Then
                        If mbUseDebug Then
                            .ColHidden = False
                        Else
                            .ColHidden = True
                        End If
                    End If

                    If j = .GetColFromID("chk") Then
                        If mbChkUseMode Then
                            'MsgBox("ok")
                            .ColHidden = False
                        Else
                            .ColHidden = True
                        End If
                    End If

                    If j = .GetColFromID("rstdt") Then
                        Select Case miUseMode
                            Case 0
                                .ColHidden = True
                            Case 1
                                .ColHidden = False
                            Case 2
                                .ColHidden = False
                        End Select
                    End If

                    If j = .GetColFromID("tordslipnm") Then
                        Select Case miUseMode
                            Case 0
                                .ColHidden = False
                                If rbSpFlg Then .set_ColWidth(.GetColFromID("tordslipnm"), 14)
                            Case 1
                                .ColHidden = False
                                If rbSpFlg Then .set_ColWidth(.GetColFromID("tordslipnm"), 14)
                            Case 2
                                .ColHidden = True
                        End Select
                    End If

                    If j = .GetColFromID("spcnmd") Then
                        Select Case miUseMode
                            Case 0
                                .ColHidden = True
                            Case 1
                                .ColHidden = True
                            Case 2
                                .ColHidden = False
                        End Select
                    End If


                    If j = .GetColFromID("orddt") Then
                        .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
                    End If

                    If j = .GetColFromID("rstdt") Then
                        .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
                    End If

                    If j = .GetColFromID("deptnm") Then
                        .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
                        If rbSpFlg Then .set_ColWidth(.GetColFromID("deptnm"), 12)
                    End If

                    If j = .GetColFromID("doctornm") Then
                        .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
                        If rbSpFlg Then .set_ColWidth(.GetColFromID("doctornm"), 8)
                    End If
                Next

                .LeftCol = .GetColFromID("chk")
                .ColsFrozen = .LeftCol
            End With

            sbDisplayInit_btnChk()

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisplayInit_btnChk()
        Dim sFn As String = "sbDisplayInit_btnChk"

        Try
            Select Case miUseMode
                Case 0
                    Me.btnChk.Visible = False

                Case 1
                    Me.btnChk.Visible = True
                    Me.btnChk.Image = GetImgList.getChkBox(enumChkBox.UnCheck)
                    Me.btnChk.Tag = ""

                Case 2
                    Me.btnChk.Visible = False

            End Select

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Public Sub sbRaiseEvent_ChangeSelectedRow(ByVal riRow As Integer)
        Dim sFn As String = "sbRaiseEvent_ChangeSelectedRow"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", riRow)
            Dim sRegNo As String = Ctrl.Get_Code(spd, "regno", riRow)
            Dim sOrdDt As String = Ctrl.Get_Code(spd, "orddt", riRow)
            Dim sTOrdSlip As String = Ctrl.Get_Code(spd, "tordslip", riRow)
            Dim sDeptNm As String = Ctrl.Get_Code(spd, "deptnm", riRow)
            Dim sDoctorNm As String = Ctrl.Get_Code(spd, "doctornm", riRow)

            Dim sFilter As String = ""
            Dim a_dr() As DataRow, a_dr_T() As DataRow
            Dim bTran As Boolean = False
            Dim al_bcno As New ArrayList, al_TordSlip As New ArrayList

            If miUseMode = 0 Then
                sFilter = ""
                sFilter += "regno = '" + sRegNo + "'"
                sFilter += " and orddt = '" + sOrdDt + "'"
                sFilter += " and tordslip = '" + sTOrdSlip + "'"

                If sDeptNm = "" Then
                    sFilter += " and deptnm is null"
                Else
                    sFilter += " and deptnm = '" + sDeptNm + "'"
                End If

                '-- 2009/06/18 yej 수정(마이그레이션 자료에는 의사명이 없음.)
                If sDoctorNm = "" Then
                    sFilter += " and doctornm is null"
                Else
                    sFilter += " and doctornm= '" + sDoctorNm + "'"
                End If

                a_dr = m_dt_bcno.Select(sFilter, "bcno asc")

                If a_dr.Length = 0 Then
                    al_bcno.Clear()
                    RaiseEvent ChangeSelectedRow(al_bcno, al_TordSlip)
                    Return
                End If

                al_bcno.Clear()

                For i As Integer = 1 To a_dr.Length

                    'If a_dr(i - 1).Item("sectcd").ToString().Trim() = "B" Then
                    '    ' 혈액은행
                    '    Dim dtT As DataTable = LISAPP.DA_SF.GetTranRst(a_dr(i - 1).Item("regno").ToString().Trim(), a_dr(i - 1).Item("fkocs").ToString().Trim(), a_dr(i - 1).Item("iogbn").ToString().Trim())

                    '    If dtT.Rows.Count > 0 Then
                    '        bTran = True

                    '        a_dr_T = m_dt_bcno.Select("fkocs = '" + dtT.Rows(0).Item(0).ToString() + "'", "bcno asc")

                    '        For j As Integer = 1 To a_dr_T.Length
                    '            If Not al_bcno.Contains(a_dr(i - 1).Item("bcno").ToString()) Then
                    '                al_bcno.Add(a_dr_T(j - 1).Item("bcno").ToString())
                    '                al_TordSlip.Add(LISAPP.DA_SF.fnGetTordSlip(a_dr_T(j - 1).Item("bcno").ToString()))
                    '            End If
                    '        Next
                    '    Else
                    '        If Not al_bcno.Contains(a_dr(i - 1).Item("bcno").ToString()) Then
                    '            al_bcno.Add(a_dr(i - 1).Item("bcno").ToString())
                    '            al_TordSlip.Add(a_dr(i - 1).Item("tordslip").ToString())
                    '        End If

                    '    End If
                    'Else
                    If Not al_bcno.Contains(a_dr(i - 1).Item("bcno").ToString()) Then
                        al_bcno.Add(a_dr(i - 1).Item("bcno").ToString())
                        al_TordSlip.Add(a_dr(i - 1).Item("tordslip").ToString())
                    End If

                    'End If
                    '> mod yjlee 2009-07-17
                Next
            ElseIf miUseMode = 1 Then
                'If sBcNo = "" Then Return

                al_bcno.Clear()

                If sBcNo <> "" Then al_bcno.Add(sBcNo)
            ElseIf miUseMode = 2 Then
                'If sBcNo = "" Then Return

                al_bcno.Clear()

                If sBcNo <> "" Then al_bcno.Add(sBcNo)
            End If

            Dim sBcNoBuf As String = ""

            If al_TordSlip.Count = 0 Then
                al_TordSlip.Add(sTOrdSlip)
            End If

            For i As Integer = 1 To al_bcno.Count
                If sBcNoBuf.Length > 0 Then sBcNoBuf += ","

                sBcNoBuf += al_bcno(i - 1).ToString() + ":" + al_TordSlip(i - 1).ToString()
            Next

            '이전 조회 조건과 같으면 Return
            If Me.txtBcNoBuf.Text = sBcNoBuf Then Return

            Me.txtBcNoBuf.Text = sBcNoBuf

            RaiseEvent ChangeSelectedRow(al_bcno, al_TordSlip)

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    '<----- Control Event ----->
    Private Sub btnChk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChk.Click
        Dim sFn As String = ""

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With spd
                If .MaxRows < 1 Then Return

                If Ctrl.Get_Code_Tag(Me.btnChk) = "" Then
                    Ctrl.CheckYesAll(spd, .GetColFromID("chk"), True)

                    Me.btnChk.Image = GetImgList.getChkBox(enumChkBox.Check)

                    Me.btnChk.Tag = "1"
                Else
                    Ctrl.CheckNoAll(spd, .GetColFromID("chk"), True)

                    Me.btnChk.Image = GetImgList.getChkBox(enumChkBox.UnCheck)

                    Me.btnChk.Tag = ""
                End If
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)
        Finally
            Me.spdList.Focus()

        End Try

    End Sub

    Public Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        If e.row < 1 Then Return
        If e.col = Me.spdList.GetColFromID("chk") Then
            Me.spdList.Col = e.col
            Me.spdList.Row = e.row

            If Me.spdList.Text = "1" Then
                Me.spdList.Text = ""
            Else
                Me.spdList.Text = "1"
            End If
            Return
        End If


        sbRaiseEvent_ChangeSelectedRow(e.row)
    End Sub

    Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick
        RaiseEvent DoubleClickRow(e.col, e.row)
    End Sub

End Class
