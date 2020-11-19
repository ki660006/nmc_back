Imports COMMON.CommFN
Imports COMMON.CommConst

Public Class FPOPUP_HOA
    Inherits System.Windows.Forms.Form

    Public BcNo As String = ""
    Public WkNo As String = ""
    Public RegNo As String = ""
    Public PatNm As String = ""
    Friend WithEvents spdAntiList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdBacList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblTclsCd As System.Windows.Forms.Label

    Private msFile As String = "File : FPOPUP_HOA.vb, Class : FPOPUP_HOA" + vbTab

    Private Sub sbDisplay_Micro_History_Date()
        Dim sFn As String = "sbDisplay_Micro_History_Date"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBacList

        Try
            Dim dt As DataTable = LISAPP.APP_M.CommFn.fnGet_Micro_Bac_Rst_History(BcNo)

            If dt.Rows.Count < 1 Then
                MsgBox("��ȸ�� ������ �����ϴ�!!", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            Me.txtBcNo.Text = dt.Rows(0).Item("bcno").ToString()

            With spd
                .ReDraw = False
                .MaxRows = dt.Rows.Count

                For intRow As Integer = 1 To dt.Rows.Count

                    For intIx1 As Integer = 1 To dt.Columns.Count
                        Dim intCol As Integer = 0

                        intCol = .GetColFromID(dt.Columns(intIx1 - 1).ColumnName.ToLower())

                        If intCol > 0 Then
                            .Row = intRow
                            .Col = intCol

                            .Text = dt.Rows(intRow - 1).Item(intIx1 - 1).ToString()
                            If intCol = .GetColFromID("rstflag") Then
                                Select Case dt.Rows(intRow - 1).Item("rstflag").ToString()
                                    Case "3"
                                        .Text = FixedVariable.gsRstFlagF
                                        .ForeColor = FixedVariable.g_color_FN
                                    Case "2"
                                        .Text = FixedVariable.gsRstFlagM
                                    Case "1"
                                        .Text = FixedVariable.gsRstFlagR
                                End Select
                            End If
                        End If
                    Next

                Next
                .ReDraw = True

                If .MaxRows > 0 Then
                    spdBacList_ClickEvent(spdBacList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1))
                End If
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplay_Micro_History_Anti_Rst(ByVal rsTclsCd As String, ByVal rsDelDt As String)
        Dim sFn As String = "sbDisplay_Micro_History_Anti_Rst"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAntiList

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Try
            '�ʱ�ȭ
            spd.MaxRows = 0

            Dim dt As DataTable = LISAPP.APP_M.CommFn.fnGet_Micro_Anti_Rst_History(BcNo, rsTclsCd, rsDelDt)

            If dt.Rows.Count < 1 Then
                Return
            End If

            sbdDisplay_Anti(dt)


        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally
            spd.ReDraw = True
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_SampleInfo()
        Dim sFn As String = "sbDisplay_SampleInfo"

        Try
            Me.txtWkNo.Text = WkNo
            Me.txtRegNo.Text = RegNo
            Me.txtPatNm.Text = PatNm

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Sub

    Private Sub sbDisplay_Init()
        Dim sFn As String = "sbDisplay_Init"

        Try
            Me.txtBcNo.Text = ""
            Me.txtBcNo.AccessibleName = ""
            Me.txtWkNo.Text = ""
            Me.txtRegNo.Text = ""
            Me.txtPatNm.Text = ""

            With Me.spdBacList
                .MaxRows = 0
            End With

            With Me.spdAntiList
                .MaxRows = 0
            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        End Try
    End Sub

    Private Sub sbdDisplay_Anti(ByVal r_dt As DataTable)

        Dim arlAnti As New ArrayList

        For intIdx As Integer = 0 To r_dt.Rows.Count - 1
            With spdAntiList
                Dim strAnti As String = r_dt.Rows(intIdx).Item("anticd").ToString

                If arlAnti.Contains(strAnti) = False Then
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("antinmd") : .Text = r_dt.Rows(intIdx).Item("antinmd").ToString
                    .Col = .GetColFromID("anticd") : .Text = r_dt.Rows(intIdx).Item("anticd").ToString

                    arlAnti.Add(strAnti)
                End If

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("anticd")
                    If .Text = strAnti Then
                        .Col = .GetColFromID(r_dt.Rows(intIdx).Item("seq").ToString)
                        .Text = r_dt.Rows(intIdx).Item("antirst").ToString + IIf(r_dt.Rows(intIdx).Item("antirst").ToString = "", "", " / ").ToString + r_dt.Rows(intIdx).Item("decrst").ToString
                        Exit For
                    End If
                Next

            End With
        Next

    End Sub

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.

    End Sub

    'Form�� Dispose�� �������Ͽ� ���� ��� ����� �����մϴ�.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form �����̳ʿ� �ʿ��մϴ�.
    Private components As System.ComponentModel.IContainer

    '����: ���� ���ν����� Windows Form �����̳ʿ� �ʿ��մϴ�.
    'Windows Form �����̳ʸ� ����Ͽ� ������ �� �ֽ��ϴ�.  
    '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
    Friend WithEvents txtWkNo As System.Windows.Forms.TextBox
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents lblBcNo As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FPOPUP_HOA))
        Me.txtWkNo = New System.Windows.Forms.TextBox
        Me.txtBcNo = New System.Windows.Forms.TextBox
        Me.lblBcNo = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPatNm = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.spdAntiList = New AxFPSpreadADO.AxfpSpread
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblTclsCd = New System.Windows.Forms.Label
        Me.spdBacList = New AxFPSpreadADO.AxfpSpread
        Me.Panel6.SuspendLayout()
        CType(Me.spdAntiList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.spdBacList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtWkNo
        '
        Me.txtWkNo.BackColor = System.Drawing.Color.White
        Me.txtWkNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWkNo.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkNo.Location = New System.Drawing.Point(273, 8)
        Me.txtWkNo.MaxLength = 16
        Me.txtWkNo.Name = "txtWkNo"
        Me.txtWkNo.ReadOnly = True
        Me.txtWkNo.Size = New System.Drawing.Size(117, 21)
        Me.txtWkNo.TabIndex = 156
        Me.txtWkNo.Text = "20050301-M0-0001-0"
        Me.txtWkNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBcNo
        '
        Me.txtBcNo.BackColor = System.Drawing.Color.White
        Me.txtBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBcNo.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcNo.Location = New System.Drawing.Point(77, 8)
        Me.txtBcNo.MaxLength = 18
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.ReadOnly = True
        Me.txtBcNo.Size = New System.Drawing.Size(117, 21)
        Me.txtBcNo.TabIndex = 155
        Me.txtBcNo.Text = "20050301-M0-0001-0"
        Me.txtBcNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblBcNo
        '
        Me.lblBcNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblBcNo.Font = New System.Drawing.Font("����", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcNo.ForeColor = System.Drawing.Color.White
        Me.lblBcNo.Location = New System.Drawing.Point(6, 8)
        Me.lblBcNo.Name = "lblBcNo"
        Me.lblBcNo.Size = New System.Drawing.Size(70, 21)
        Me.lblBcNo.TabIndex = 154
        Me.lblBcNo.Text = "��ü��ȣ"
        Me.lblBcNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(202, 8)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(70, 21)
        Me.Label30.TabIndex = 153
        Me.Label30.Text = "�۾���ȣ"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRegNo
        '
        Me.txtRegNo.BackColor = System.Drawing.Color.White
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegNo.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNo.Location = New System.Drawing.Point(470, 8)
        Me.txtRegNo.MaxLength = 16
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.ReadOnly = True
        Me.txtRegNo.Size = New System.Drawing.Size(117, 21)
        Me.txtRegNo.TabIndex = 158
        Me.txtRegNo.Text = "20050301-M0-0001-0"
        Me.txtRegNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(399, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 21)
        Me.Label1.TabIndex = 157
        Me.Label1.Text = "��Ϲ�ȣ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPatNm
        '
        Me.txtPatNm.BackColor = System.Drawing.Color.White
        Me.txtPatNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatNm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPatNm.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtPatNm.Location = New System.Drawing.Point(668, 8)
        Me.txtPatNm.MaxLength = 16
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.ReadOnly = True
        Me.txtPatNm.Size = New System.Drawing.Size(117, 21)
        Me.txtPatNm.TabIndex = 160
        Me.txtPatNm.Text = "20050301-M0-0001-0"
        Me.txtPatNm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(597, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 21)
        Me.Label2.TabIndex = 159
        Me.Label2.Text = "����"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel6.Controls.Add(Me.spdAntiList)
        Me.Panel6.Location = New System.Drawing.Point(4, 150)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(784, 420)
        Me.Panel6.TabIndex = 162
        '
        'spdAntiList
        '
        Me.spdAntiList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdAntiList.Location = New System.Drawing.Point(0, 0)
        Me.spdAntiList.Name = "spdAntiList"
        Me.spdAntiList.OcxState = CType(resources.GetObject("spdAntiList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdAntiList.Size = New System.Drawing.Size(779, 415)
        Me.spdAntiList.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblTclsCd)
        Me.Panel1.Controls.Add(Me.spdBacList)
        Me.Panel1.Location = New System.Drawing.Point(4, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(784, 110)
        Me.Panel1.TabIndex = 163
        '
        'lblTclsCd
        '
        Me.lblTclsCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTclsCd.Location = New System.Drawing.Point(623, 0)
        Me.lblTclsCd.Name = "lblTclsCd"
        Me.lblTclsCd.Size = New System.Drawing.Size(37, 19)
        Me.lblTclsCd.TabIndex = 164
        Me.lblTclsCd.Visible = False
        '
        'spdBacList
        '
        Me.spdBacList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdBacList.Location = New System.Drawing.Point(0, 0)
        Me.spdBacList.Name = "spdBacList"
        Me.spdBacList.OcxState = CType(resources.GetObject("spdBacList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdBacList.Size = New System.Drawing.Size(779, 103)
        Me.spdBacList.TabIndex = 0
        '
        'FPOPUP_HOA
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.txtPatNm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRegNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtWkNo)
        Me.Controls.Add(Me.txtBcNo)
        Me.Controls.Add(Me.lblBcNo)
        Me.Controls.Add(Me.Label30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FPOPUP_HOA"
        Me.ShowInTaskbar = False
        Me.Text = "���� �� �ױ��� ��� History"
        Me.Panel6.ResumeLayout(False)
        CType(Me.spdAntiList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdBacList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Sub FPOPUPMM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sbDisplay_Init()

        sbDisplay_SampleInfo()

        sbDisplay_Micro_History_Date()
    End Sub

    Private Sub spdBacList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdBacList.ClickEvent
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        With Me.spdBacList
            .Row = e.row
            .Col = .GetColFromID("tclscd") : Dim strTclsCd As String = .Text
            .Col = .GetColFromID("deldt") : Dim strDeldt As String = .Text

            sbDisplay_Micro_History_Anti_Rst(strTclsCd, strDeldt)
        End With
    End Sub

End Class
