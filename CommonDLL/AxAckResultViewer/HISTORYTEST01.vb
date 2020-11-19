Imports System.Drawing

Imports COMMON.CommFN

Public Class HISTORYTEST01
    Inherits System.Windows.Forms.UserControl

    Dim mbUseResultDateMode As Boolean

    Public Property UseResultDateMode() As Boolean
        Get
            Return mbUseResultDateMode
        End Get
        Set(ByVal Value As Boolean)
            mbUseResultDateMode = Value

            With spdRst
                If .GetColFromID("orddt") > 0 Then
                    .Col = .GetColFromID("orddt")
                    .ColHidden = False
                    .set_ColWidth(.GetColFromID("orddt"), 12.5)
                End If

                If .GetColFromID("rstdt") > 0 Then
                    .Col = .GetColFromID("rstdt")
                    If mbUseResultDateMode Then
                        .ColHidden = False
                        .set_ColWidth(.GetColFromID("rstdt"), 12.5)
                    Else
                        .ColHidden = True
                    End If
                End If

            End With
        End Set
    End Property

    Public Sub Display_HistoryTest(ByVal rsRegno As String, ByVal rsTestCd As String, ByVal rsTnm As String, ByVal rsSDate As String, _
                                   ByVal rsEDate As String, ByVal rsBcNo As String)


        Try
            Me.lblTnm.Text = rsTnm
            Me.spdRst.ReDraw = False
            Me.spdRst.MaxRows = 0

            rsSDate += ""
            rsEDate += ""

            Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_hsitory_rst_test_rv(rsRegno, rsTestCd, rsSDate.Replace("-", ""), rsEDate.Replace("-", ""), mbUseResultDateMode, rsBcNo)
            For ix As Integer = 0 To dt.Rows.Count - 1
                With spdRst
                    .MaxRows = .MaxRows + 1

                    .Row = .MaxRows
                    .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString
                    .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString
                    .Col = .GetColFromID("rstdt") : .Text = dt.Rows(ix).Item("rstdt").ToString
                    .Col = .GetColFromID("viewrst") : .Text = dt.Rows(ix).Item("rstval").ToString
                    .Col = .GetColFromID("reftxt") : .Text = dt.Rows(ix).Item("reftxt").ToString
                    .Col = .GetColFromID("rstunit") : .Text = dt.Rows(ix).Item("rstunit").ToString
                    .Col = .GetColFromID("spcnm") : .Text = dt.Rows(ix).Item("spcnmd").ToString

                    If dt.Rows(ix).Item("pnmark").ToString = "P" Then
                        .Col = .GetColFromID("viewrst")
                        .Row = .MaxRows
                        .BackColor = Color.FromArgb(150, 150, 255)
                        .ForeColor = Color.FromArgb(0, 0, 255)
                    Else
                        If dt.Rows(ix).Item("hlmark").ToString = "L" Then
                            .Col = .GetColFromID("viewrst")
                            .Row = .MaxRows
                            .BackColor = Color.FromArgb(221, 240, 255)
                            .ForeColor = Color.FromArgb(0, 0, 255)
                        ElseIf dt.Rows(ix).Item("hlmark").ToString = "H" Then
                            .Col = .GetColFromID("viewrst")
                            .Row = .MaxRows
                            .BackColor = Color.FromArgb(255, 230, 231)
                            .ForeColor = Color.FromArgb(255, 0, 0)
                        End If
                    End If
                End With
            Next
        Catch ex As Exception
        Finally
            Me.spdRst.ReDraw = True
        End Try
        

    End Sub

    Public Function Display_HistoryTest(ByVal rsRegno As String, ByVal rsTestCd As String, ByVal rsTnm As String, ByVal rsEDate As String, ByVal rsBcNo As String) As ArrayList

        Try
            Dim alData As New ArrayList

            Me.lblTnm.Text = rsTnm
            Me.spdRst.ReDraw = False
            Me.spdRst.MaxRows = 0

            alData.Clear()

            rsEDate += "235959"

            Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_hsitory_rst_test_rv(rsRegno, rsTestCd, "", rsEDate, mbUseResultDateMode, rsBcNo)
            For ix As Integer = 0 To dt.Rows.Count - 1
                With spdRst
                    .MaxRows = .MaxRows + 1

                    .Row = .MaxRows
                    .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString
                    .Col = .GetColFromID("rstdt") : .Text = dt.Rows(ix).Item("rstdt").ToString
                    .Col = .GetColFromID("viewrst") : .Text = dt.Rows(ix).Item("rstval").ToString
                    .Col = .GetColFromID("reftxt") : .Text = dt.Rows(ix).Item("reftxt").ToString
                    .Col = .GetColFromID("rstunit") : .Text = dt.Rows(ix).Item("rstunit").ToString
                    .Col = .GetColFromID("spcnm") : .Text = dt.Rows(ix).Item("spcnmd").ToString

                    Dim clsChart As New ChartInfo
                    With clsChart
                        .sRstDte = dt.Rows(ix).Item("rstdt").ToString
                        .sRstVal = dt.Rows(ix).Item("rstval").ToString
                    End With

                    alData.Add(clsChart)

                    If dt.Rows(ix).Item("pnmark").ToString = "P" Then
                        .Col = .GetColFromID("viewrst")
                        .Row = .MaxRows
                        .BackColor = Color.FromArgb(150, 150, 255)
                        .ForeColor = Color.FromArgb(0, 0, 255)
                    Else
                        If dt.Rows(ix).Item("hlmark").ToString = "L" Then
                            .Col = .GetColFromID("viewrst")
                            .Row = .MaxRows
                            .BackColor = Color.FromArgb(221, 240, 255)
                            .ForeColor = Color.FromArgb(0, 0, 255)
                        ElseIf dt.Rows(ix).Item("hlmark").ToString = "H" Then
                            .Col = .GetColFromID("viewrst")
                            .Row = .MaxRows
                            .BackColor = Color.FromArgb(255, 230, 231)
                            .ForeColor = Color.FromArgb(255, 0, 0)
                        End If
                    End If
                End With
            Next

            alData.TrimToSize()
            Display_HistoryTest = alData
        Catch ex As Exception
        Finally
            Me.spdRst.ReDraw = True
        End Try



    End Function

    Public Sub Clear()
        Dim sFn As String = "Sub Clear()"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdRst

        Try
            lblTnm.Text = ""
            With spd
                .ReDraw = False
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

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        With spdRst
            .Font = New Font("굴림체", 9, FontStyle.Regular)

            .SelBackColor = Drawing.Color.FromArgb(213, 215, 255)
            .SelForeColor = SystemColors.InactiveBorder

            .ShadowColor = Drawing.Color.FromArgb(165, 186, 222)
            .ShadowDark = Color.DimGray
            .ShadowText = SystemColors.ControlText

            .GrayAreaBackColor = Drawing.Color.FromArgb(236, 242, 255)
        End With

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
    Friend WithEvents spdRst As AxFPSpreadADO.AxfpSpread

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents lblTnm As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HISTORYTEST01))
        Me.lblTnm = New System.Windows.Forms.Label
        Me.spdRst = New AxFPSpreadADO.AxfpSpread
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTnm
        '
        Me.lblTnm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTnm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTnm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTnm.ForeColor = System.Drawing.Color.White
        Me.lblTnm.Location = New System.Drawing.Point(0, 0)
        Me.lblTnm.Name = "lblTnm"
        Me.lblTnm.Size = New System.Drawing.Size(296, 24)
        Me.lblTnm.TabIndex = 3
        Me.lblTnm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdRst
        '
        Me.spdRst.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdRst.Location = New System.Drawing.Point(0, 24)
        Me.spdRst.Name = "spdRst"
        Me.spdRst.OcxState = CType(resources.GetObject("spdRst.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRst.Size = New System.Drawing.Size(296, 168)
        Me.spdRst.TabIndex = 4
        '
        'HISTORYTEST01
        '
        Me.Controls.Add(Me.spdRst)
        Me.Controls.Add(Me.lblTnm)
        Me.Name = "HISTORYTEST01"
        Me.Size = New System.Drawing.Size(296, 192)
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
