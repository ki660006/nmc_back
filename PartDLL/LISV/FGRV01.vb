Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class FGRV01
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGRV01.vb, Class : FGRV01" & vbTab

    Private msRegNo As String = ""
    Private msSearchDayS As String = ""
    Private msSearchDayE As String = ""
    Private mbViewReportOnly As Boolean = False
    Private mbResultDateMode As Boolean = False

    Private miProcessing As Integer = 0
    Friend WithEvents trv1 As AxAckResultViewer.TOTRST03
    Friend WithEvents spclst1 As AxAckResultViewer.SPCLIST03

    Friend WithEvents hisTest As AxAckResultViewer.HISTORYTEST01
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdMicro As AxFPSpreadADO.AxfpSpread

    Private mbCalled As Boolean = False

    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpDayE As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDayS As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSearchR As System.Windows.Forms.Button
    Friend WithEvents btnSearchHR As System.Windows.Forms.Button
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents axPatInfo As AxAckPatientInfo.AxSpcInfo

    Private mbPatNmSearch As Boolean = False

    Public Sub sbBtnClick(ByVal rsFormGbn As String)

        If rsFormGbn = "H" Then
            Me.btnSearchHR_Click(Nothing, Nothing)
        ElseIf rsFormGbn = "R" And Me.btnSearchR.Text = "�����ȸ(���Ϻ���)" Then
            Me.btnSearchR_Click(Nothing, Nothing)
        ElseIf rsFormGbn = "O" And Me.btnSearchR.Text = "�����ȸ(ó�����ں�)" Then
            Me.btnSearchR_Click(Nothing, Nothing)
        End If

    End Sub


    Public Sub Display_List()
        Dim sFn As String = "sbDisplay_List"

        Try
            Me.btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

            Dim sw As StreamWriter
            Dim sLastFileNm As String = System.Windows.Forms.Application.StartupPath + "\emr_LastGbn.ini"

            If IO.File.Exists(sLastFileNm) Then
                sw = New StreamWriter(sLastFileNm, False, System.Text.Encoding.UTF8)

                sw.WriteLine(Me.Text)
                sw.Close()
            End If
        End Try
    End Sub

    Public Sub Display_Result(ByVal rsRegNo As String, ByVal rsDayS As String, ByVal rsDayE As String)

        Me.dtpDayS.Value = CDate(rsDayS)
        Me.dtpDayE.Value = CDate(rsDayE)
        Me.txtNo.Text = rsRegNo

        sbDisplay_Data(rsRegNo)
    End Sub

    Private Function fnGet_LastOrdDt(ByVal rsRegNo As String) As String
        Dim sFn As String = "fnGet_LastOrdDt"
        Try
            Return LISAPP.APP_V.CommFn.fnGet_OrderDate_Max(rsRegNo)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Return Format(Now, "yyyy-MM-dd").ToString
        End Try

    End Function

    Private Function fnGet_LastRstDt(ByVal rsRegNo As String) As String
        Dim sFn As String = "fnGet_LastRstDt"
        Try
            Return LISAPP.APP_V.CommFn.fnGet_RstDate_Max(rsRegNo)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Return Format(Now, "yyyy-MM-dd").ToString
        End Try

    End Function

    Private Function fnFind_RegNo(ByVal rsPatNm As String) As String
        Dim sFn As String = "fnFind_RegNo"

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_byNm(rsPatNm)

            objHelp.FormText = "ȯ������"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            objHelp.AddField("regno", "��Ϲ�ȣ", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("patnm", "����", 12, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("sex", "����", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("idno", "�ֹι�ȣ", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtNo)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.txtNo.Left, pntFrmXY.Y + pntCtlXY.Y + txtNo.Height + 80, dt)

            If aryList.Count > 0 Then
                msRegNo = aryList.Item(0).ToString.Split("|"c)(0)
                Me.txtNo.Text = aryList.Item(0).ToString.Split("|"c)(1)
                Return msRegNo
            End If

            Return ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.
        Me.WindowState = Windows.Forms.FormWindowState.Maximized

    End Sub

    Public Sub New(ByVal rbResultDateMode As Boolean, Optional ByVal rbViewReportOnly As Boolean = False)
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.
        mbResultDateMode = rbResultDateMode
        mbViewReportOnly = rbViewReportOnly

        'mbCalled = True

    End Sub

    Public Sub New(ByVal rsRegNo As String, ByVal rsDayS As String, ByVal rsDayE As String, _
                   Optional ByVal rbResultDateMode As Boolean = False, _
                   Optional ByVal rbViewReportOnly As Boolean = False, _
                   Optional ByVal rbModal As Boolean = False, Optional ByVal robjform As Object = Nothing)
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.

        If rsDayS.IndexOf("-") < 0 And rsDayS <> "" Then rsDayS = rsDayS.Substring(0, 4) + "-" + rsDayS.Substring(4, 2) + "-" + rsDayS.Substring(6, 2)
        If rsDayE.IndexOf("-") < 0 And rsDayE <> "" Then rsDayE = rsDayE.Substring(0, 4) + "-" + rsDayE.Substring(4, 2) + "-" + rsDayE.Substring(6, 2)

        msRegNo = rsRegNo
        msSearchDayS = rsDayS
        msSearchDayE = rsDayE
        mbResultDateMode = rbResultDateMode
        mbViewReportOnly = rbViewReportOnly

        If rbModal Then
            btnSearchHR.Enabled = False
            btnSearchR.Enabled = False
        Else
            mbCalled = True
        End If

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
    Friend WithEvents lblNo As System.Windows.Forms.Label
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents grpNo As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGRV01))
        Me.grpNo = New System.Windows.Forms.GroupBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpDayE = New System.Windows.Forms.DateTimePicker
        Me.lblDate = New System.Windows.Forms.Label
        Me.dtpDayS = New System.Windows.Forms.DateTimePicker
        Me.btnToggle = New System.Windows.Forms.Button
        Me.lblNo = New System.Windows.Forms.Label
        Me.txtNo = New System.Windows.Forms.TextBox
        Me.trv1 = New AxAckResultViewer.TOTRST03
        Me.spclst1 = New AxAckResultViewer.SPCLIST03
        Me.hisTest = New AxAckResultViewer.HISTORYTEST01
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdMicro = New AxFPSpreadADO.AxfpSpread
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSearchR = New System.Windows.Forms.Button
        Me.btnSearchHR = New System.Windows.Forms.Button
        Me.chkPreview = New System.Windows.Forms.CheckBox
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.axPatInfo = New AxAckPatientInfo.AxSpcInfo
        Me.grpNo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdMicro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpNo
        '
        Me.grpNo.Controls.Add(Me.btnSearch)
        Me.grpNo.Controls.Add(Me.Label2)
        Me.grpNo.Controls.Add(Me.dtpDayE)
        Me.grpNo.Controls.Add(Me.lblDate)
        Me.grpNo.Controls.Add(Me.dtpDayS)
        Me.grpNo.Controls.Add(Me.btnToggle)
        Me.grpNo.Controls.Add(Me.lblNo)
        Me.grpNo.Controls.Add(Me.txtNo)
        Me.grpNo.Location = New System.Drawing.Point(0, 0)
        Me.grpNo.Name = "grpNo"
        Me.grpNo.Size = New System.Drawing.Size(347, 61)
        Me.grpNo.TabIndex = 1
        Me.grpNo.TabStop = False
        '
        'btnSearch
        '
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Location = New System.Drawing.Point(280, 35)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(60, 23)
        Me.btnSearch.TabIndex = 10
        Me.btnSearch.Text = "��ȸ(&S)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(173, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 12)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "~"
        '
        'dtpDayE
        '
        Me.dtpDayE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDayE.Location = New System.Drawing.Point(192, 36)
        Me.dtpDayE.Name = "dtpDayE"
        Me.dtpDayE.Size = New System.Drawing.Size(86, 21)
        Me.dtpDayE.TabIndex = 9
        Me.dtpDayE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDate.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(6, 35)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(72, 22)
        Me.lblDate.TabIndex = 6
        Me.lblDate.Text = "ó������"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDayS
        '
        Me.dtpDayS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDayS.Location = New System.Drawing.Point(79, 36)
        Me.dtpDayS.Name = "dtpDayS"
        Me.dtpDayS.Size = New System.Drawing.Size(86, 21)
        Me.dtpDayS.TabIndex = 8
        Me.dtpDayS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(280, 12)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(60, 21)
        Me.btnToggle.TabIndex = 2
        Me.btnToggle.Text = "<->"
        '
        'lblNo
        '
        Me.lblNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblNo.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblNo.ForeColor = System.Drawing.Color.White
        Me.lblNo.Location = New System.Drawing.Point(6, 11)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.Size = New System.Drawing.Size(72, 22)
        Me.lblNo.TabIndex = 0
        Me.lblNo.Text = "��Ϲ�ȣ"
        Me.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNo
        '
        Me.txtNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNo.Location = New System.Drawing.Point(79, 12)
        Me.txtNo.MaxLength = 8
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(200, 21)
        Me.txtNo.TabIndex = 1
        '
        'trv1
        '
        Me.trv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trv1.FastTestDateTime = False
        Me.trv1.Location = New System.Drawing.Point(350, 119)
        Me.trv1.Name = "trv1"
        Me.trv1.Size = New System.Drawing.Size(753, 205)
        Me.trv1.TabIndex = 10
        Me.trv1.UseDblCheck = False
        Me.trv1.UseDebug = False
        Me.trv1.UseLab = False
        Me.trv1.ViewMark = False
        Me.trv1.ViewReportOnly = False
        '
        'spclst1
        '
        Me.spclst1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spclst1.CheckUseMode = True
        Me.spclst1.Location = New System.Drawing.Point(0, 117)
        Me.spclst1.Name = "spclst1"
        Me.spclst1.Size = New System.Drawing.Size(347, 256)
        Me.spclst1.TabIndex = 12
        Me.spclst1.UseDebug = False
        Me.spclst1.UseMode = 0
        Me.spclst1.UseTempRstState = False
        '
        'hisTest
        '
        Me.hisTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.hisTest.Location = New System.Drawing.Point(0, 375)
        Me.hisTest.Name = "hisTest"
        Me.hisTest.Size = New System.Drawing.Size(347, 222)
        Me.hisTest.TabIndex = 13
        Me.hisTest.UseResultDateMode = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.spdMicro)
        Me.Panel1.Location = New System.Drawing.Point(350, 323)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(753, 276)
        Me.Panel1.TabIndex = 14
        Me.Panel1.Visible = False
        '
        'spdMicro
        '
        Me.spdMicro.DataSource = Nothing
        Me.spdMicro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdMicro.Location = New System.Drawing.Point(0, 0)
        Me.spdMicro.Name = "spdMicro"
        Me.spdMicro.OcxState = CType(resources.GetObject("spdMicro.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdMicro.Size = New System.Drawing.Size(753, 276)
        Me.spdMicro.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Location = New System.Drawing.Point(270, 63)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(70, 24)
        Me.btnExit.TabIndex = 195
        Me.btnExit.Text = "ȭ��ݱ�"
        '
        'btnSearchR
        '
        Me.btnSearchR.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearchR.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnSearchR.Location = New System.Drawing.Point(8, 88)
        Me.btnSearchR.Name = "btnSearchR"
        Me.btnSearchR.Size = New System.Drawing.Size(165, 25)
        Me.btnSearchR.TabIndex = 193
        Me.btnSearchR.Text = "�����ȸ(���Ϻ���)"
        '
        'btnSearchHR
        '
        Me.btnSearchHR.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearchHR.Font = New System.Drawing.Font("����", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchHR.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnSearchHR.Location = New System.Drawing.Point(175, 88)
        Me.btnSearchHR.Name = "btnSearchHR"
        Me.btnSearchHR.Size = New System.Drawing.Size(165, 25)
        Me.btnSearchHR.TabIndex = 194
        Me.btnSearchHR.Text = "���� �˻��� ��ȸ"
        '
        'chkPreview
        '
        Me.chkPreview.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkPreview.Checked = True
        Me.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPreview.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkPreview.Location = New System.Drawing.Point(3, 67)
        Me.chkPreview.Name = "chkPreview"
        Me.chkPreview.Size = New System.Drawing.Size(113, 18)
        Me.chkPreview.TabIndex = 192
        Me.chkPreview.Text = "��� �� �̸�����"
        Me.chkPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear.Location = New System.Drawing.Point(198, 63)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(70, 24)
        Me.btnClear.TabIndex = 191
        Me.btnClear.Text = "ȭ������"
        '
        'btnPrint
        '
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrint.Location = New System.Drawing.Point(126, 63)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 24)
        Me.btnPrint.TabIndex = 190
        Me.btnPrint.Text = "���(&P)"
        '
        'axPatInfo
        '
        Me.axPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axPatInfo.Location = New System.Drawing.Point(351, 1)
        Me.axPatInfo.Name = "axPatInfo"
        Me.axPatInfo.Size = New System.Drawing.Size(756, 115)
        Me.axPatInfo.TabIndex = 198
        '
        'FGRV01
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1103, 598)
        Me.Controls.Add(Me.axPatInfo)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSearchR)
        Me.Controls.Add(Me.btnSearchHR)
        Me.Controls.Add(Me.chkPreview)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.trv1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.hisTest)
        Me.Controls.Add(Me.spclst1)
        Me.Controls.Add(Me.grpNo)
        Me.Name = "FGRV01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FGRV01"
        Me.grpNo.ResumeLayout(False)
        Me.grpNo.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdMicro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbDisplay_Clear()
        Dim sFn As String = "sbDisplay_Clear"

        Try
            Me.spclst1.Clear()
            Me.trv1.Clear()
            Me.axPatInfo.sbInit()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub sbDisplay_Abnormal_Msg(ByVal rsRegNo As String)
        Dim sFn As String = "sbDisplay_Abnormal_Msg"

        Try
            Dim dt As DataTable = LISAPP.APP_V.CommFn.fnGet_Abnormal_RegNo(rsRegNo)

            If dt.Rows.Count < 1 Then Return

            Dim frmChild As Windows.Forms.Form
            frmChild = New FGRV01_S01(dt)

            frmChild.WindowState = FormWindowState.Normal
            frmChild.Activate()
            frmChild.ShowDialog()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Public Sub sbDisplay_Data(ByVal rsRegNo As String)
        Dim sFn As String = "sbDisplay_Data"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            If rsRegNo = "" Then Return
            If IsNumeric(rsRegNo) Then
                rsRegNo = rsRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            Else
                rsRegNo = rsRegNo.Substring(0, 1).ToUpper + rsRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
            End If
            Me.txtNo.Text = rsRegNo

            sbDisplay_Clear()

            With Me.spclst1
                .Display_OrderList(rsRegNo, Me.dtpDayS.Text.Replace("-", ""), Me.dtpDayE.Text.Replace("-", ""))

                If .RowCount < 1 Then
                    'MsgBox("�ش��ϴ� ȯ�ڰ� �����ϴ�!!", MsgBoxStyle.Information)
                Else
                    spclst1_DoubleClickRow(1, 1)
                End If
            End With

            Me.hisTest.Clear()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""

            Dim sw As StreamWriter
            Dim strLastFileNm As String = System.Windows.Forms.Application.StartupPath + "\emr_LastGbn.ini"

            If IO.File.Exists(strLastFileNm) Then
                sw = New StreamWriter(strLastFileNm, False, System.Text.Encoding.UTF8)
                sw.WriteLine(Me.Text)
                sw.Close()
            End If

        End Try
    End Sub

    Private Sub sbDisplay_Result(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_Result"

        Try
            If rsBcNo.Length = 11 Or rsBcNo.Length = 12 Then
                rsBcNo = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(rsBcNo.Substring(0, 11))
            End If

            Dim al_bcno As New ArrayList

            If rsBcNo.Length = 15 Then
                al_bcno.Add(rsBcNo)
            ElseIf rsBcNo.Length = 14 Then
                For i As Integer = 0 To 9
                    al_bcno.Add(rsBcNo + i.ToString())
                Next
            Else
                Return
            End If

            With Me.trv1
                .Display_Result(al_bcno, "")
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_SpcInfo(ByVal r_si As AxAckResultViewer.SpecimenInfo)
        Dim sFn As String = "sbDisplay_SpcInfo"

        Try
            axPatInfo.sbDisplay_SpcInfo(r_si)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            If mbResultDateMode Then
                '������� ���� ��ü�� ��ȸ
                Me.Text = "�����ȸ(���Ϻ���)"
                Me.spclst1.UseMode = 1
                Me.lblDate.Text = "�������"
                Me.btnSearchR.Text = "�����ȸ(ó�����ں�)"
                Me.hisTest.UseResultDateMode = True
            Else
                'ó������ ���� ó�潽���� ��ȸ
                Me.Text = "�����ȸ(ó�����ں�)"
                Me.spclst1.UseMode = 0
                Me.lblDate.Text = " ó������"
                Me.btnSearchR.Text = "�����ȸ(���Ϻ���)"
                Me.hisTest.UseResultDateMode = False
            End If

            Me.spclst1.Clear()
            Me.trv1.Clear()
            Me.hisTest.Clear()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbPrint_Result()
        Dim sFn As String = "sbPrint_Result"

        Try
            Dim al_bcno As ArrayList

            If mbResultDateMode Then
                '������� ���� ��ü�� ��ȸ
                al_bcno = Me.spclst1.SelectedBcNoList

                If al_bcno.Count < 1 Then
                    MsgBox("����ϰ��� �ϴ� �˻����� �����Ͻ� �� �ٽ� ����Ͻñ� �ٶ��ϴ�!!", MsgBoxStyle.Information)

                    Return
                End If

                Me.trv1.Display_Result(al_bcno, "")
                Me.trv1.Print_Result(Me.chkPreview.Checked, 0)

                btnSearch_Click(Nothing, Nothing)

            Else
                'ó������ ���� ó�潽���� ��ȸ
                If Me.trv1.RowCount < 1 And Me.spclst1.CurrentRow < 1 Then
                    MsgBox("����ϰ��� �ϴ� �˻����� �����Ͻ� �� �ٽ� ����Ͻñ� �ٶ��ϴ�!!", MsgBoxStyle.Information)

                    Return
                End If
                'MsgBox("0")
                With spclst1.spdList
                    trv1.sbPrintClear()

                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("chk")

                        If .Text = "1" Then
                            spclst1.spdList_ClickEvent(Nothing, New AxFPSpreadADO._DSpreadEvents_ClickEvent(0, iRow))

                            If Me.spclst1.CurrentState = "��������" Then
                                Me.trv1.Print_Result(Me.chkPreview.Checked, 1)
                            Else
                                Me.trv1.Print_Result(Me.chkPreview.Checked, 2)
                            End If
                        End If
                    Next
                End With
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub FGRV01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim sFn As String = "FGRV01_Activated"

        Try
            Me.WindowState = Windows.Forms.FormWindowState.Maximized

            Me.txtNo.Focus()

            trv1.Height = trv1.Height + (Me.Height - (trv1.Top + trv1.Height)) - 35
            trv1.Width = trv1.Width + (Me.Width - (trv1.Left + trv1.Width)) - 8

            Dim sw As StreamWriter
            Dim strLastFileNm As String = System.Windows.Forms.Application.StartupPath + "\emr_LastGbn.ini"

            If IO.File.Exists(strLastFileNm) Then
                sw = New StreamWriter(strLastFileNm, False, System.Text.Encoding.UTF8)

                sw.WriteLine(Me.Text)
                sw.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        If Windows.Forms.Application.ExecutablePath.ToUpper.EndsWith(FixedVariable.gsExeFileName.ToUpper) Then
        Else
            If mbCalled Then
                If MsgBox("���α׷��� �����ϸ� �� ȣ��� �ε��� ������� �ֽ��ϴ�. " + vbCrLf + _
                          "�׷��� ���� �Ͻðڽ��ϱ� ?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.Yes Then
                    Windows.Forms.Application.Exit()
                Else
                    e.Cancel = True
                    Me.WindowState = Windows.Forms.FormWindowState.Minimized
                    'sbMain_Minimized()
                End If
            End If
        End If
    End Sub

    '<----- Control Event ----->
    Private Sub FGRV01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.WindowState = Windows.Forms.FormWindowState.Maximized
        Me.trv1.ViewReportOnly = mbViewReportOnly '-- 2007-10-19 YEJ ADD
        Me.trv1.Form = Me

        Me.txtNo.MaxLength = PRG_CONST.Len_RegNo

        sbDisplayInit()

        '��Ϲ�ȣ, ��ȸ����S, ��ȸ����E �� �������� ����Ʈ ��ȸ
        If msRegNo.Length > 0 Then
            If msSearchDayE = "" Then msSearchDayE = Format(Now, "yyyy-MM-dd").ToString
            If msSearchDayS = "" Then msSearchDayS = Format(DateAdd(DateInterval.Month, -3, CDate(fnGet_LastOrdDt(msRegNo))), "yyyy-MM-dd").ToCharArray

            Me.dtpDayS.Value = CDate(msSearchDayS)
            Me.dtpDayE.Value = CDate(msSearchDayE)
            Me.txtNo.Text = msRegNo

            sbDisplay_Data(Me.txtNo.Text)
        Else
            Me.dtpDayS.Value = DateAdd(DateInterval.Month, -3, Now)
            Me.dtpDayE.Value = Now
        End If

        Me.txtNo.Focus()
    End Sub

    Private Sub FGRV01_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        miProcessing = 1
    End Sub

    Private Sub FGRV01_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        If miProcessing = 1 Then Return

        Me.spclst1.ReDraw()

        trv1.Height = trv1.Height + (Me.Height - (trv1.Top + trv1.Height)) - 35
        trv1.Width = trv1.Width + (Me.Width - (trv1.Left + trv1.Width)) - 8

        If Me.WindowState = Windows.Forms.FormWindowState.Maximized Then
            Me.txtNo.Focus()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Clear()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        sbPrint_Result()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If mbViewReportOnly Then
            sbDisplay_Abnormal_Msg(Me.txtNo.Text)
        End If

        sbDisplay_Data(Me.txtNo.Text)

    End Sub

    Private Sub btnSearchHR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchHR.Click
        Dim sFn As String = "Handles btnSearchHR.Click"
        Try
            If mbCalled Then
                Dim sw As StreamWriter
                Dim strLastFileNm As String = System.Windows.Forms.Application.StartupPath + "\emr_LastGbn.ini"

                sw = New StreamWriter(strLastFileNm, False, System.Text.Encoding.UTF8)

                sw.WriteLine(Me.Text)
                sw.Close()
            End If
            Dim frm As Windows.Forms.Form

            frm = Ctrl.CheckFormObject(Me, Me.btnSearchHR.Text)

            If frm Is Nothing Then frm = New LISV.FGRV13(Me.txtNo.Text, Me.dtpDayS.Text, Me.dtpDayE.Text, mbViewReportOnly)

            frm.MdiParent = Me.MdiParent
            frm.WindowState = Windows.Forms.FormWindowState.Maximized
            If mbViewReportOnly Then
                frm.Text = Me.btnSearchHR.Text
            Else
                frm.Text = frm.Name + "��" + Me.btnSearchHR.Text
            End If
            frm.Activate()
            frm.Show()

            With CType(frm, LISV.FGRV13)
                .mbCalled = True
                .Display_Result(txtNo.Text, Me.dtpDayS.Text, Me.dtpDayE.Text)
            End With

            If mbViewReportOnly = False Then MdiTabControl.sbTabPageAdd(frm)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err())
        End Try

    End Sub

    Private Sub btnSearchR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchR.Click

        Dim sRegno As String = txtNo.Text

        If mbCalled Then
            Dim sw As StreamWriter
            Dim strLastFileNm As String = System.Windows.Forms.Application.StartupPath + "\emr_LastGbn.ini"

            sw = New StreamWriter(strLastFileNm, False, System.Text.Encoding.UTF8)
            sw.WriteLine(Me.Text)
            sw.Close()
        End If

        Dim frm As Windows.Forms.Form

        frm = Ctrl.CheckFormObject(Me, Me.btnSearchR.Text)

        Select Case Me.btnSearchR.Text
            Case "�����ȸ(ó�����ں�)"
                If frm Is Nothing Then frm = New FGRV01(False, mbViewReportOnly)

            Case "�����ȸ(���Ϻ���)"
                If frm Is Nothing Then frm = New FGRV01(True, mbViewReportOnly)

        End Select

        frm.MdiParent = Me.MdiParent
        frm.WindowState = Windows.Forms.FormWindowState.Maximized

        If mbViewReportOnly Then
            frm.Text = Me.btnSearchR.Text
        Else
            frm.Text = frm.Name + "��" + Me.btnSearchR.Text
        End If
        frm.Activate()
        frm.Show()

        With CType(frm, FGRV01)
            .Display_Result(sRegno, Me.dtpDayS.Text, Me.dtpDayE.Text)
        End With

        If mbViewReportOnly = False Then MdiTabControl.sbTabPageAdd(frm)

    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        '���ܰ˻����а� ���α׷������� ��Ϲ�ȣ -> ���� -> ��ü��ȣ -> ��Ϲ�ȣ ...
        If Windows.Forms.Application.ExecutablePath.ToUpper.EndsWith(FixedVariable.gsExeFileName.ToUpper) Then
            Fn.SearchToggle(Me.lblNo, Me.btnToggle, enumToggle.Regno_Name_Bcno, Me.txtNo)
            Me.txtNo.Focus()
        Else
            Fn.SearchToggle(Me.lblNo, Me.btnToggle, enumToggle.RegnoToName, Me.txtNo)
            Me.txtNo.Focus()
        End If
    End Sub

    Private Sub spclst1_ChangeSelectedRow(ByVal r_al_bcno As System.Collections.ArrayList, ByVal r_al_TOrdSlip As ArrayList) Handles spclst1.ChangeSelectedRow

        Me.trv1.Clear()
        Me.axPatInfo.sbInit()
        Me.hisTest.Clear()

        If r_al_bcno.Count < 1 Then Return
        Dim bMicro As Boolean = False

        With Me.trv1
            .Clear()
            .msRegNo = txtNo.Text
            .msStartDt = Format(dtpDayS.Value, "yyyy-mm-dd") + " 00:00:00"
            .msEndDt = Format(dtpDayE.Value, "yyyy-mm-dd") + " 00:00:00"
            .mbMicro = False

            .Display_Result(r_al_bcno, r_al_TOrdSlip)

            If .mbMicro Then
                spdMicro.ClearRange(1, 0, spdMicro.MaxCols, spdMicro.MaxRows, False)
                spdMicro.MaxRows = 0
                Panel1.Visible = True
                trv1.Height = trv1.Height + (Me.Height - (trv1.Top + trv1.Height)) - (35 + Panel1.Height)
                Panel1.BringToFront()

                sbDisplay_MicroRst(r_al_bcno)

            Else
                Panel1.Visible = False
                trv1.Height = trv1.Height + (Me.Height - (trv1.Top + trv1.Height)) - 35
                trv1.BringToFront()
            End If

        End With

    End Sub

    Private Sub sbDisplay_MicroRst(ByVal rsBcNo As ArrayList)
        Dim sFn As String = "sbDisplay_MicroRst"

        Try

            Dim alColId As New ArrayList
            Dim iColWidth As Double = 23.75

            Me.spdMicro.MaxCols = 3
            Me.spdMicro.ReDraw = False

            For ix1 As Integer = 0 To rsBcNo.Count - 1
                Dim dt As DataTable = LISAPP.APP_V.CommFn.fnGet_MicroRst(rsBcNo(ix1).ToString())

                With spdMicro
                    For ix2 As Integer = 0 To dt.Rows.Count - 1
                        Dim sColId As String = dt.Rows(ix2).Item("bcno").ToString.Replace("-", "") + dt.Rows(ix2).Item("testcd").ToString.Trim + dt.Rows(ix2).Item("ranking").ToString.Trim
                        If alColId.Contains(sColId) = False Then
                            .MaxCols += 1 : .set_ColWidth(.MaxCols, iColWidth)

                            .Row = FPSpreadADO.CoordConstants.SpreadHeader
                            .Col = .MaxCols : .Text = dt.Rows(ix2).Item("bcno").ToString
                            .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                            .Col = .MaxCols : .Text = dt.Rows(ix2).Item("bacnmd").ToString.Trim

                            .Col = .MaxCols : .ColID = sColId

                            alColId.Add(sColId)
                        End If

                        Dim iRow As Integer = .SearchCol(.GetColFromID("anticd"), 0, .MaxRows, dt.Rows(ix2).Item("anticd").ToString().Trim, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                        If iRow < 1 Then
                            .MaxRows += 1
                            iRow = .MaxRows

                            .Row = iRow
                            .Col = .GetColFromID("antinm") : .Text = dt.Rows(ix2).Item("antinmd").ToString().Trim
                            .Col = .GetColFromID("anticd") : .Text = dt.Rows(ix2).Item("anticd").ToString().Trim
                            .Col = .GetColFromID("sortanti") : .Text = dt.Rows(ix2).Item("sortanti").ToString.Trim
                        End If

                        .Row = iRow
                        .Col = .MaxCols
                        If dt.Rows(ix2).Item("testmtd").ToString().Trim() = "D" Then
                            .Text = Space(10) + dt.Rows(ix2).Item("decrst").ToString().Trim.PadRight(14, " "c) + Space(5)
                            '.Text = Space(10) + dt.Rows(ix2).Item("decrst").ToString()
                        Else
                            .Text = dt.Rows(ix2).Item("antirst").ToString().Trim().Trim.PadRight(14, " "c) + dt.Rows(ix2).Item("decrst").ToString().Trim.PadRight(5, " "c)
                            '.Text = dt.Rows(ix2).Item("antirst").ToString().Trim().Trim + "/" + dt.Rows(ix2).Item("decrst").ToString().Trim
                        End If
                    Next
                End With
            Next

        Catch ex As Exception
        Finally
            Me.spdMicro.ReDraw = True
        End Try
    End Sub

    Private Sub trv1_ChangedBcNo(ByVal spcinfo As AxAckResultViewer.SpecimenInfo) Handles trv1.ChangedBcNo
        sbDisplay_SpcInfo(spcinfo)
    End Sub

    Private Sub txtNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNo.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Select Case Me.lblNo.Text
            Case "��ü��ȣ"
                Return

            Case "��Ϲ�ȣ"
                '��Ϲ�ȣ, ������ ��� ó��
                If IsNumeric(Me.txtNo.Text.Substring(1)) Then
                    If IsNumeric(Me.txtNo.Text.Substring(0, 1)) Then
                        Me.txtNo.Text = Me.txtNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                    Else
                        Me.txtNo.Text = Me.txtNo.Text.Substring(0, 1) + Me.txtNo.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                    End If
                End If

            Case Else
                Me.txtNo.Text = fnFind_RegNo(Me.txtNo.Text)

                Do While True
                    btnToggle_Click(Nothing, Nothing)
                    If Me.lblNo.Text = "��Ϲ�ȣ" Then Exit Do
                Loop
        End Select

        If mbResultDateMode Then
            '������� ���� ��ü�� ��ȸ
            Me.dtpDayE.Value = CDate(fnGet_LastRstDt(Me.txtNo.Text))
        Else
            'ó������ ���� ó�潽���� ��ȸ
            Me.dtpDayE.Value = CDate(fnGet_LastOrdDt(Me.txtNo.Text))
        End If

        'Me.dtpDayE.Value = CDate(fnGet_LastOrdDt(Me.txtNo.Text))
        'Me.dtpDayS.Value = DateAdd(DateInterval.Month, -3, dtpDayE.Value)
        Me.btnSearch_Click(Nothing, Nothing)

    End Sub

    Private Sub txtNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.TextChanged
        Select Case Me.lblNo.Text
            Case "��Ϲ�ȣ"
                'If IsNumeric(Me.txtNo.Text) Then
                msRegNo = Me.txtNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                'End If
        End Select
    End Sub

    Private Sub txtNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.GotFocus
        txtNo.SelectAll()
    End Sub

    Private Sub trv1_ChangeSelectedRow(ByVal rsTCd As String, ByVal rsTnm As String) Handles trv1.ChangeSelectedRow

        Dim sSDate As String = ""
        Dim sEDate As String = ""

        sSDate = Format(dtpDayS.Value, "yyyy-MM-dd") ' + " 00:00:00"
        sEDate = Format(dtpDayE.Value, "yyyy-MM-dd") '+ " 23:59:59"

        hisTest.Display_HistoryTest(Me.axPatInfo.RegNo, rsTCd, rsTnm, sSDate, sEDate, spclst1.CurrentBcno)

    End Sub

    Private Sub spclst1_DoubleClickRow(ByVal riCol As Integer, ByVal riRow As Integer) Handles spclst1.DoubleClickRow
        spclst1.sbRaiseEvent_ChangeSelectedRow(riRow)
    End Sub

    Private Sub FGRV01_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.txtNo.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGRV01_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If mbViewReportOnly = False Then MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub dtpDayS_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDayS.CloseUp
        sbDisplay_Data(txtNo.Text)
    End Sub

    Private Sub spclst1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles spclst1.Load

    End Sub
End Class
