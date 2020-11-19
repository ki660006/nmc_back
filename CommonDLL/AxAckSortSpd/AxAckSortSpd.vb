Imports System.Windows.Forms

Public Class AxAckSortSpd
    Inherits System.Windows.Forms.UserControl

    Private Const mc_iGap1 As Integer = 1
    Private Const mc_iGap2 As Integer = 2

    Private miColNum As Integer = 1
    Private miRowNum As Integer = 1

    Private miWh_pnl As Integer = 0         'Width of pnlOpt_1_1
    Private miHt_pnl As Integer = 0         'Height of pnlOpt_1_1

    Private miWh_cbo As Integer = 0         'Width of cboOpt_1_1
    Private miHt_cbo As Integer = 0         'Height of cboOpt_1_1

    Private miWh_btn As Integer = 0         'Width of btnSort
    Private miHt_btn As Integer = 0         'Height of btnSort

    Private mbUseSortButton As Boolean = True

    Private mbInitialized As Boolean = False

    Private m_al_col As ArrayList = Nothing
    Private m_al_col_init As ArrayList = Nothing

    Private m_spdSort As AxFPSpreadADO.AxfpSpread

    Public WriteOnly Property Columns() As ArrayList

        Set(ByVal Value As ArrayList)

            If m_al_col Is Nothing Then
                m_al_col_init = New ArrayList
                For i As Integer = 0 To Value.Count - 1
                    Dim si As New SortingInfo
                    si.ColumnDesc = CType(Value.Item(i), SortingInfo).ColumnDesc
                    si.ColumnId = CType(Value.Item(i), SortingInfo).ColumnId
                    si.ColumnName = CType(Value.Item(i), SortingInfo).ColumnName
                    si.ColumnNo = CType(Value.Item(i), SortingInfo).ColumnNo
                    m_al_col_init.Add(si)
                Next
            End If

            m_al_col = Value

            Dim iComcount As Integer = 0
            Dim iBtnCount As Integer = 0

            For i As Integer = 1 To miRowNum
                For j As Integer = 1 To miColNum
                    For k As Integer = 0 To Me.Controls.Count - 1
                        For m As Integer = 0 To Me.Controls(k).Controls.Count - 1

                            If Me.Controls(k).Controls(m).Name = "cboOpt_" + i.ToString + "_" + j.ToString Then
                                For n As Integer = 0 To m_al_col.Count - 1
                                    CType(Me.Controls(k).Controls(m), ComboBox).Items.Add(m_al_col.Item(n).ColumnName)
                                Next
                                CType(Me.Controls(k).Controls(m), ComboBox).SelectedIndex = iComcount
                                iComcount += 1

                            ElseIf Me.Controls(k).Controls(m).Name = "btnOpt_" + i.ToString + "_" + j.ToString Then
                                If m_al_col.Item(iBtnCount).ColumnDesc = False Then
                                    CType(Me.Controls(k).Controls(m), Button).ImageIndex = 0
                                ElseIf m_al_col.Item(iBtnCount).ColumnDesc = True Then
                                    CType(Me.Controls(k).Controls(m), Button).ImageIndex = 1
                                End If
                                iBtnCount += 1
                            End If
                            If iBtnCount >= m_al_col.Count Then Return
                        Next
                    Next
                Next
            Next

        End Set
    End Property

    Public Property ColWidth() As Integer
        Get
            Return miWh_cbo
        End Get
        Set(ByVal Value As Integer)
            If miWh_cbo = Value Then
                sbInitialize(False)
            Else
                If miWh_cbo = Value Then Return

                miWh_cbo = Value

                sbInitialize(True)

                set_ColumnRowSize(miColNum, miRowNum)
            End If
        End Set
    End Property

    Public Property ColNumber() As Integer
        Get
            Return miColNum
        End Get
        Set(ByVal Value As Integer)
            If Value < 1 Then
                MsgBox("ColNumber는 0보다 큰 숫자이어야 합니다!!", MsgBoxStyle.Critical)

                Return
            End If

            If miColNum = Value Then Return

            miColNum = Value

            set_ColumnRowSize(miColNum, miRowNum)
        End Set
    End Property

    Public Property RowNumber() As Integer
        Get
            Return miRowNum
        End Get
        Set(ByVal Value As Integer)
            If Value < 1 Then
                MsgBox("RowNumber는 0보다 큰 숫자이어야 합니다!!", MsgBoxStyle.Critical)

                Return
            End If

            If miRowNum = Value Then Return

            miRowNum = Value

            set_ColumnRowSize(miColNum, miRowNum)
        End Set
    End Property

    Public Property Spread6ToSort() As AxFPSpreadADO.AxfpSpread
        Get
            Return m_spdSort
        End Get

        Set(ByVal Value As AxFPSpreadADO.AxfpSpread)
            m_spdSort = Value
        End Set
    End Property

    Public Property UseSortButton() As Boolean
        Get
            Return mbUseSortButton
        End Get
        Set(ByVal Value As Boolean)
            If mbUseSortButton.ToString() = Value.ToString() Then Return

            mbUseSortButton = Value

            set_ColumnRowSize(miColNum, miRowNum)
        End Set
    End Property

    Public Sub set_ColumnRowSize(ByVal ColNumb As Integer, ByVal RowNumb As Integer)
        Dim sFn As String = "Sub set_ColumnRowSize(" + ColNumber.ToString() + ", " + RowNumber.ToString() + ")"

        Try
            If ColNumb < 1 Then
                MsgBox("ColNumber는 0보다 큰 숫자이어야 합니다!!", MsgBoxStyle.Critical)

                Return
            End If

            If RowNumb < 1 Then
                MsgBox("RowNumber는 0보다 큰 숫자이어야 합니다!!", MsgBoxStyle.Critical)

                Return
            End If

            miColNum = ColNumb
            miRowNum = RowNumb

            'set Location of btnSort
            Me.btnSort.Left = mc_iGap2 + (miWh_pnl * miColNum) + mc_iGap2

            'set Size of btnSort
            If mbUseSortButton Then
                Me.btnSort.Width = miWh_btn
            Else
                Me.btnSort.Width = 0
            End If

            AxAckSortSpd_SizeChanged(Nothing, Nothing)

            sbControl_Clear()

            For ir As Integer = 1 To miRowNum
                For ic As Integer = 1 To miColNum
                    sbControl_Add(ic, ir)
                Next
            Next

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub

    Public Sub Sort()
        sbSort()
    End Sub

    Private Sub sb_btnOpt_Row_Col_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFn As String = "Sub sb_btnOpt_Row_Col_Click(ByVal sender As Object, ByVal e As System.EventArgs)"

        Try
            CType(sender, Button).ImageIndex = (CType(sender, Button).ImageIndex + 1) Mod 2

            For Each ctrl As Control In sender.Parent.Controls
                If TypeOf ctrl Is ComboBox Then
                    ctrl.Select()

                    Exit For
                End If
            Next

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub sbControl_Add(ByVal riCol As Integer, ByVal riRow As Integer)
        Dim sFn As String = "Sub sbControl_Add(" + riCol.ToString() + ", " + riRow.ToString() + ")"

        If riCol = 1 And riRow = 1 Then Return

        Dim pnl As New Panel
        Dim cbo As New ComboBox
        Dim btn As New Button

        Try
            'pnl
            pnl.BackColor = System.Drawing.Color.Transparent
            pnl.Controls.Add(cbo)
            pnl.Controls.Add(btn)
            pnl.Location = New System.Drawing.Point(mc_iGap2 + (riCol - 1) * miWh_pnl, mc_iGap2 + (riRow - 1) * miHt_pnl)

            pnl.Name = "pnlOpt" + "_" + riRow.ToString() + "_" + riCol.ToString()
            pnl.Size = New System.Drawing.Size(miWh_pnl, miHt_pnl)
            pnl.TabIndex = (riRow - 1) * miColNum + riCol

            'cbo
            cbo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            cbo.Location = New System.Drawing.Point(mc_iGap1, mc_iGap1)
            cbo.Name = "cboOpt" + "_" + riRow.ToString() + "_" + riCol.ToString()
            cbo.Size = New System.Drawing.Size(miWh_cbo, miHt_cbo)
            cbo.DropDownStyle = ComboBoxStyle.DropDownList
            cbo.DropDownWidth = cbo.Width + 30
            cbo.TabIndex = 1

            'btn
            btn.BackColor = System.Drawing.Color.Transparent
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
            btn.ImageIndex = 0
            btn.ImageList = Me.imgLst01
            btn.Location = New System.Drawing.Point(mc_iGap1 + cbo.Width, mc_iGap1)
            btn.Name = "btnOpt" + "_" + riRow.ToString() + "_" + riCol.ToString()
            btn.Size = New System.Drawing.Size(20, 20)
            btn.TabIndex = 2

            AddHandler btn.Click, AddressOf sb_btnOpt_Row_Col_Click

            Me.Controls.Add(pnl)

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub sbControl_Clear()
        Dim sFn As String = "Sub sbControl_Clear()"

        Try
            For i As Integer = Me.Controls.Count To 1 Step -1
                Dim sCtrlNm As String = Me.Controls(i - 1).Name

                If sCtrlNm.EndsWith("_1_1") Or sCtrlNm.EndsWith("btnSort") Or sCtrlNm.EndsWith("AxAckSortSpd") Then
                Else
                    Me.Controls.RemoveAt(i - 1)
                End If
            Next

            Me.cboOpt_1_1.Items.Clear()

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub sbInitialize()
        Dim sFn As String = "Sub sbInitialize()"

        Try
            miWh_btn = Me.btnSort.Width
            miHt_btn = Me.btnSort.Height

            miWh_pnl = Me.pnlOpt_1_1.Width
            miHt_pnl = Me.pnlOpt_1_1.Height

            miWh_cbo = Me.cboOpt_1_1.Width
            miHt_cbo = Me.cboOpt_1_1.Height

            Me.cboOpt_1_1.DropDownStyle = ComboBoxStyle.DropDownList
            Me.cboOpt_1_1.DropDownWidth = Me.cboOpt_1_1.Width + 30

            AddHandler btnOpt_1_1.Click, AddressOf sb_btnOpt_Row_Col_Click

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub sbInitialize(ByVal rbChangedColWidth As Boolean)
        If rbChangedColWidth = False Then Return

        Me.cboOpt_1_1.Width = miWh_cbo
        Me.btnOpt_1_1.Left = Me.cboOpt_1_1.Left + Me.cboOpt_1_1.Width

        Me.pnlOpt_1_1.Width = mc_iGap1 + Me.cboOpt_1_1.Width + Me.btnOpt_1_1.Width + mc_iGap1

        miWh_pnl = Me.pnlOpt_1_1.Width
    End Sub

    Private Sub sbSort()
        Dim sFn As String = "Sub sbSort()"
        Dim iCboCount As Integer = 0
        Dim iBtnCount As Integer = 0
        Dim iSortKey As Integer = 0

        Try
            With Me.Spread6ToSort

                .ReDraw = False
                .SortBy = FPSpreadADO.SortByConstants.SortByRow

                For i As Integer = 1 To miRowNum
                    For j As Integer = 1 To miColNum
                        For k As Integer = 0 To Me.Controls.Count - 1
                            For m As Integer = 0 To Me.Controls(k).Controls.Count - 1
                                If Me.Controls(k).Controls(m).Name = "cboOpt_" + i.ToString + "_" + j.ToString Then
                                    iCboCount += 1

                                    If iCboCount > m_al_col.Count Then Exit For

                                    For n As Integer = 0 To m_al_col.Count - 1
                                        Dim iCol As Integer = 0

                                        If CType(m_al_col(n), SortingInfo).ColumnName = CType(Me.Controls(k).Controls(m), ComboBox).SelectedItem.ToString Then
                                            If CType(m_al_col(n), SortingInfo).ColumnNo = 0 Then
                                                If CType(m_al_col(n), SortingInfo).ColumnId = "" Then
                                                    MsgBox("ColumnNo and ColumnId is all empty!!", MsgBoxStyle.Information)

                                                    Return
                                                Else
                                                    iCol = .GetColFromID(CType(m_al_col(n), SortingInfo).ColumnId)

                                                    If iCol > 0 Then
                                                        .set_SortKey(Convert.ToInt16(iCboCount), iCol)
                                                    Else
                                                        MsgBox("ColumnId is not matched!!", MsgBoxStyle.Information)

                                                        Return
                                                    End If
                                                End If
                                            Else
                                                .set_SortKey(Convert.ToInt16(iCboCount), CType(m_al_col.Item(n), SortingInfo).ColumnNo)
                                            End If

                                            Exit For
                                        End If
                                    Next

                                ElseIf Me.Controls(k).Controls(m).Name = "btnOpt_" + i.ToString + "_" + j.ToString Then
                                    iBtnCount += 1
                                    If iBtnCount > m_al_col.Count Then Exit For

                                    If CType(Me.Controls(k).Controls(m), Button).ImageIndex = 0 Then
                                        .set_SortKeyOrder(Convert.ToInt16(iBtnCount), FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                                        m_al_col.Item(iBtnCount - 1).ColumnDesc = False

                                    ElseIf CType(Me.Controls(k).Controls(m), Button).ImageIndex = 1 Then
                                        .set_SortKeyOrder(Convert.ToInt16(iBtnCount), FPSpreadADO.SortKeyOrderConstants.SortKeyOrderDescending)
                                        m_al_col.Item(iBtnCount - 1).ColumnDesc = True
                                    End If
                                End If
                            Next
                        Next
                    Next
                Next

                .Col = 1
                .Col2 = .MaxCols
                .Row = 1
                .Row2 = .MaxRows

                .Action = FPSpreadADO.ActionConstants.ActionSort

                .ReDraw = True

                .Refresh()
            End With
        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message, MsgBoxStyle.Critical)

        End Try

    End Sub

    Private Sub sbSort_Init()
        Dim sFn As String = "sbSort_Init()"
        Dim iCboCount As Integer = 0
        Dim iBtnCount As Integer = 0

        Try
            With Me.Spread6ToSort

                .ReDraw = False

                .SortBy = FPSpreadADO.SortByConstants.SortByRow

                For i As Integer = 1 To miRowNum
                    For j As Integer = 1 To miColNum
                        For k As Integer = 0 To Me.Controls.Count - 1
                            For m As Integer = 0 To Me.Controls(k).Controls.Count - 1
                                If Me.Controls(k).Controls(m).Name = "cboOpt_" + i.ToString + "_" + j.ToString Then
                                    iCboCount += 1
                                    If iCboCount > m_al_col_init.Count Then Exit For
                                    CType(Me.Controls(k).Controls(m), ComboBox).SelectedIndex = iCboCount - 1
                                    .set_SortKey(Convert.ToInt16(iCboCount), CType(Me.Controls(k).Controls(m), ComboBox).SelectedIndex + 1)

                                ElseIf Me.Controls(k).Controls(m).Name = "btnOpt_" + i.ToString + "_" + j.ToString Then
                                    iBtnCount += 1

                                    If iBtnCount > m_al_col_init.Count Then Exit For
                                    If m_al_col_init.Item(iBtnCount - 1).ColumnDesc = False Then
                                        CType(Me.Controls(k).Controls(m), Button).ImageIndex = 0
                                    ElseIf m_al_col_init.Item(iBtnCount - 1).ColumnDesc = True Then
                                        CType(Me.Controls(k).Controls(m), Button).ImageIndex = 1
                                    End If

                                    If CType(Me.Controls(k).Controls(m), Button).ImageIndex = 0 Then
                                        .set_SortKeyOrder(Convert.ToInt16(iBtnCount), FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                                    ElseIf CType(Me.Controls(k).Controls(m), Button).ImageIndex = 1 Then
                                        .set_SortKeyOrder(Convert.ToInt16(iBtnCount), FPSpreadADO.SortKeyOrderConstants.SortKeyOrderDescending)
                                    End If
                                End If
                            Next
                        Next
                    Next
                Next

                .Col = 1
                .Col2 = .MaxCols
                .Row = 1
                .Row2 = .MaxRows

                .Action = FPSpreadADO.ActionConstants.ActionSort

                .ReDraw = True

                .Refresh()
            End With
        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbInitialize()

        mbInitialized = True
    End Sub

    'UserControl1은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
    Friend WithEvents imgLst01 As System.Windows.Forms.ImageList
    Friend WithEvents btnSort As System.Windows.Forms.Button
    Friend WithEvents pnlOpt_1_1 As System.Windows.Forms.Panel
    Friend WithEvents btnOpt_1_1 As System.Windows.Forms.Button
    Friend WithEvents cboOpt_1_1 As System.Windows.Forms.ComboBox
    Friend WithEvents ctmnu01 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuitemSort As System.Windows.Forms.MenuItem
    Friend WithEvents mnuitemInit As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(AxAckSortSpd))
        Me.imgLst01 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnSort = New System.Windows.Forms.Button
        Me.pnlOpt_1_1 = New System.Windows.Forms.Panel
        Me.btnOpt_1_1 = New System.Windows.Forms.Button
        Me.cboOpt_1_1 = New System.Windows.Forms.ComboBox
        Me.ctmnu01 = New System.Windows.Forms.ContextMenu
        Me.mnuitemSort = New System.Windows.Forms.MenuItem
        Me.mnuitemInit = New System.Windows.Forms.MenuItem
        Me.pnlOpt_1_1.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgLst01
        '
        Me.imgLst01.ImageSize = New System.Drawing.Size(20, 20)
        Me.imgLst01.ImageStream = CType(resources.GetObject("imgLst01.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgLst01.TransparentColor = System.Drawing.Color.Transparent
        '
        'btnSort
        '
        Me.btnSort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSort.Location = New System.Drawing.Point(96, 2)
        Me.btnSort.Name = "btnSort"
        Me.btnSort.Size = New System.Drawing.Size(37, 23)
        Me.btnSort.TabIndex = 0
        Me.btnSort.Text = "정렬"
        '
        'pnlOpt_1_1
        '
        Me.pnlOpt_1_1.BackColor = System.Drawing.Color.Transparent
        Me.pnlOpt_1_1.Controls.Add(Me.btnOpt_1_1)
        Me.pnlOpt_1_1.Controls.Add(Me.cboOpt_1_1)
        Me.pnlOpt_1_1.Location = New System.Drawing.Point(2, 2)
        Me.pnlOpt_1_1.Name = "pnlOpt_1_1"
        Me.pnlOpt_1_1.Size = New System.Drawing.Size(92, 23)
        Me.pnlOpt_1_1.TabIndex = 1
        '
        'btnOpt_1_1
        '
        Me.btnOpt_1_1.BackColor = System.Drawing.Color.Transparent
        Me.btnOpt_1_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOpt_1_1.ImageIndex = 0
        Me.btnOpt_1_1.ImageList = Me.imgLst01
        Me.btnOpt_1_1.Location = New System.Drawing.Point(71, 1)
        Me.btnOpt_1_1.Name = "btnOpt_1_1"
        Me.btnOpt_1_1.Size = New System.Drawing.Size(20, 20)
        Me.btnOpt_1_1.TabIndex = 2
        '
        'cboOpt_1_1
        '
        Me.cboOpt_1_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOpt_1_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOpt_1_1.Location = New System.Drawing.Point(1, 1)
        Me.cboOpt_1_1.Name = "cboOpt_1_1"
        Me.cboOpt_1_1.Size = New System.Drawing.Size(70, 21)
        Me.cboOpt_1_1.TabIndex = 1
        '
        'ctmnu01
        '
        Me.ctmnu01.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuitemSort, Me.mnuitemInit})
        '
        'mnuitemSort
        '
        Me.mnuitemSort.Index = 0
        Me.mnuitemSort.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mnuitemSort.Text = "정렬(&S)"
        '
        'mnuitemInit
        '
        Me.mnuitemInit.Index = 1
        Me.mnuitemInit.Text = "원상태로 정렬"
        '
        'AxAckSortSpd
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ContextMenu = Me.ctmnu01
        Me.Controls.Add(Me.pnlOpt_1_1)
        Me.Controls.Add(Me.btnSort)
        Me.Name = "AxAckSortSpd"
        Me.Size = New System.Drawing.Size(135, 27)
        Me.pnlOpt_1_1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub AxAckSortSpd_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Dim sFn As String = "Sub AxAckSortSpd_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged"

        Try
            If mbInitialized = False Then Return

            Me.btnSort.Left = mc_iGap2 + (miWh_pnl * miColNum) + mc_iGap2
            Me.btnSort.Height = miHt_pnl * miRowNum
            Me.Width = Me.btnSort.Left + Me.btnSort.Width + mc_iGap2
            Me.Height = mc_iGap2 + Me.btnSort.Height + mc_iGap2

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub mnuitemSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuitemSort.Click
        sbSort()
    End Sub

    Private Sub mnuitemInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuitemInit.Click
        sbSort_Init()
    End Sub

    Private Sub btnSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSort.Click
        sbSort()
    End Sub

End Class

Public Class SortingInfo
    Public ColumnNo As Integer = 0
    Public ColumnId As String = ""
    Public ColumnName As String = ""
    Public ColumnDesc As Boolean = False
End Class