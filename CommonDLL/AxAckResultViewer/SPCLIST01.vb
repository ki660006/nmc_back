Imports COMMON.CommFN
Imports System.Drawing

Public Class SPCLIST01
    Inherits System.Windows.Forms.UserControl

    Public RegNo As String = ""
    Public SearchDayS As String = ""
    Public SearchDayE As String = ""

    Public Event ChangeSelectedRow(ByVal r_al_bcno As ArrayList, ByVal rsTOrdSlip As String)
    Public Shadows Event DoubleClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent)

    Private Const mc_sState_Final As String = "최종보고"

    Private mbUseDebug As Boolean = False
    Private mbUseTempRstState As Boolean = False

    Private miUseMode As Integer = 0

    Private m_dt_bcno As DataTable

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

    Public Sub Clear()
        Dim sFn As String = "Sub Clear()"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With spd
                .ReDraw = False

                Select Case miUseMode
                    Case 0
                        .LeftCol = .GetColFromID("orddt")
                    Case 1
                        .LeftCol = .GetColFromID("chk")
                    Case 2
                        .LeftCol = .GetColFromID("rstdt")
                End Select

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

            Dim dt As DataTable
            Dim dt_bcno As DataTable

            If miUseMode = 0 Then
                dt = DA01.DA_SF.Get_List_RegNo_Order(RegNo, SearchDayS, SearchDayE, dt_bcno, mbUseTempRstState)
            ElseIf miUseMode = 1 Then
                dt = DA01.DA_SF.Get_List_RegNo_Result(RegNo, SearchDayS, SearchDayE, mbUseTempRstState)
            ElseIf miUseMode = 2 Then
                dt = DA01.DA_SF.Get_List_RegNo_ResultOnly(RegNo, SearchDayS, SearchDayE, mbUseTempRstState)
            End If

            If dt.Rows.Count = 0 Then Return

            Dim sSort As String = ""

            If miUseMode = 0 Then
                sSort = "orddt desc, tordslip, deptnm, doctornm"
            ElseIf miUseMode = 1 Then
                sSort = "rstdt desc, orddt desc, deptnm, doctornm, bcno"
            ElseIf miUseMode = 2 Then
                sSort = "rstdt desc, orddt desc, deptnm, doctornm, bcno"
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

                Select Case miUseMode
                    Case 0
                        .LeftCol = .GetColFromID("orddt")
                    Case 1
                        .LeftCol = .GetColFromID("chk")
                    Case 2
                        .LeftCol = .GetColFromID("rstdt")
                End Select
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        Finally
            spd.Hide()
            spd.Show()
            spd.ReDraw = True

        End Try
    End Sub

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
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnChk As System.Windows.Forms.Button
    Friend WithEvents txtBcNoBuf As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SPCLIST01))
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
        Me.pnl.Size = New System.Drawing.Size(356, 576)
        Me.pnl.TabIndex = 0
        '
        'txtBcNoBuf
        '
        Me.txtBcNoBuf.Location = New System.Drawing.Point(16, 348)
        Me.txtBcNoBuf.Name = "txtBcNoBuf"
        Me.txtBcNoBuf.Size = New System.Drawing.Size(308, 21)
        Me.txtBcNoBuf.TabIndex = 2
        Me.txtBcNoBuf.Text = ""
        Me.txtBcNoBuf.Visible = False
        '
        'btnChk
        '
        Me.btnChk.BackColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.btnChk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChk.Font = New System.Drawing.Font("새굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnChk.Location = New System.Drawing.Point(-1, -1)
        Me.btnChk.Name = "btnChk"
        Me.btnChk.Size = New System.Drawing.Size(18, 30)
        Me.btnChk.TabIndex = 0
        Me.btnChk.TabStop = False
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.ContainingControl = Me
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(355, 575)
        Me.spdList.TabIndex = 1
        '
        'SPCLIST01
        '
        Me.Controls.Add(Me.pnl)
        Me.Name = "SPCLIST01"
        Me.Size = New System.Drawing.Size(356, 576)
        Me.pnl.ResumeLayout(False)
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

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            With Me.spdList
                .Font = New Font("굴림체", 9, FontStyle.Regular)

                .SelBackColor = Drawing.Color.FromArgb(213, 215, 255)
                .SelForeColor = SystemColors.InactiveBorder

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
                        Select Case miUseMode
                            Case 0
                                .ColHidden = True
                            Case 1
                                .ColHidden = False
                            Case 2
                                .ColHidden = True
                        End Select
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
                            Case 1
                                .ColHidden = True
                            Case 2
                                .ColHidden = True
                        End Select
                    End If

                    If j = .GetColFromID("spcnmd") Then
                        Select Case miUseMode
                            Case 0
                                .ColHidden = True
                            Case 1
                                .ColHidden = False
                            Case 2
                                .ColHidden = False
                        End Select
                    End If

                    If j = .GetColFromID("orddt") Then
                        .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
                    End If

                    If j = .GetColFromID("deptnm") Then
                        .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
                    End If

                    If j = .GetColFromID("doctornm") Then
                        .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
                    End If
                Next

                Select Case miUseMode
                    Case 0
                        .LeftCol = .GetColFromID("orddt")
                    Case 1
                        .LeftCol = .GetColFromID("chk")
                        .ColsFrozen = .LeftCol
                    Case 2
                        .LeftCol = .GetColFromID("rstdt")
                End Select
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
                    Me.btnChk.Image = GetImgList.getChkBox(enumChkBox.Check)
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
            Dim a_dr() As DataRow
            Dim al_bcno As New ArrayList

            If miUseMode = 0 Then
                sFilter = ""
                sFilter += "regno = '" + sRegNo + "'"
                sFilter += " and orddt = '" + sOrdDt + "'"
                sFilter += " and tordslip = '" + sTOrdSlip + "'"
                sFilter += " and deptnm = '" + sDeptNm + "'"
                sFilter += " and doctornm = '" + sDoctorNm + "'"

                a_dr = m_dt_bcno.Select(sFilter, "bcno asc")

                If a_dr.Length = 0 Then Return

                al_bcno.Clear()

                For i As Integer = 1 To a_dr.Length
                    If al_bcno.Contains(a_dr(i - 1).Item("bcno").ToString()) = False Then
                        al_bcno.Add(a_dr(i - 1).Item("bcno").ToString())
                    End If
                Next
            ElseIf miUseMode = 1 Then
                If sBcNo = "" Then Return

                al_bcno.Clear()

                al_bcno.Add(sBcNo)
            ElseIf miUseMode = 2 Then
                If sBcNo = "" Then Return

                al_bcno.Clear()

                al_bcno.Add(sBcNo)
            End If

            Dim sBcNoBuf As String = ""

            For i As Integer = 1 To al_bcno.Count
                If sBcNoBuf.Length > 0 Then sBcNoBuf += ","
                sBcNoBuf += al_bcno(i - 1).ToString() + ":" + sTOrdSlip
            Next

            '이전 조회 조건과 같으면 Return
            If Me.txtBcNoBuf.Text = sBcNoBuf Then Return

            Me.txtBcNoBuf.Text = sBcNoBuf

            RaiseEvent ChangeSelectedRow(al_bcno, sTOrdSlip)

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

                    Me.btnChk.Image = GetImgList.getChkBox(enumChkBox.UnCheck)

                    Me.btnChk.Tag = "1"
                Else
                    Ctrl.CheckNoAll(spd, .GetColFromID("chk"), True)

                    Me.btnChk.Image = GetImgList.getChkBox(enumChkBox.Check)

                    Me.btnChk.Tag = ""
                End If
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        Finally
            Me.spdList.Focus()

        End Try
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        If e.row < 1 Then Return
        If e.col = Me.spdList.GetColFromID("chk") Then Return

        sbRaiseEvent_ChangeSelectedRow(e.row)
    End Sub

    Private Sub spdList_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdList.LeaveCell
        If e.newRow < 1 Then Return
        If e.newCol = Me.spdList.GetColFromID("chk") Then Return

        sbRaiseEvent_ChangeSelectedRow(e.newRow)
    End Sub

    Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick
        RaiseEvent DoubleClick(sender, e)
    End Sub

End Class
