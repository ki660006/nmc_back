Imports COMMON.CommFN
Imports COMMON.CommConst

Public Class FGOCS01_S02
    Inherits System.Windows.Forms.Form

    Public BcNo As String = ""
    Public WkNo As String = ""
    Public RegNo As String = ""
    Public PatNm As String = ""
    Friend WithEvents lblUsrNm As System.Windows.Forms.Label
    Friend WithEvents txtUsrNm As System.Windows.Forms.TextBox
    Friend WithEvents pnlSetRst As System.Windows.Forms.Panel
    Friend WithEvents spdList_SetRst As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtRslt As System.Windows.Forms.TextBox

    Private msFile As String = "File : FPOPUP_HOA.vb, Class : FPOPUP_HOA" + vbTab

    Public Sub sbDisplay_SetRstInfo(ByVal sRstGbn As String, ByVal sRegNo As String, ByVal sOrdDt As String, ByVal sOrdSeqNo As String, ByVal sOrdNm As String)
        Try
            Dim dt7 As DataTable = OCSAPP.OcsLink.NMC.fnGet_SetRstInfo(sRegNo, sOrdDt, sOrdSeqNo)

            If dt7.Rows.Count = 0 Then
                spdList_SetRst.MaxRows = 0
                Return
            End If

            With spdList_SetRst
                .ReDraw = False
                .MaxRows = dt7.Rows.Count
                For ix As Integer = 0 To dt7.Rows.Count - 1

                    .Row = ix + 1

                    txtOrdNm.Text = sOrdNm
                    txtExecDate.Text = dt7.Rows(ix).Item("execdate").ToString
                    txtRsltDate.Text = dt7.Rows(ix).Item("rsltdate").ToString
                    txtUsrNm.Text = dt7.Rows(ix).Item("usrnm").ToString

                    If sRstGbn = "1" Then
                        txtRslt.Visible = False
                        lblUsrNm.Visible = False
                        txtUsrNm.Visible = False
                        .Col = .GetColFromID("tnm") : .Text = dt7.Rows(ix).Item("tnm").ToString
                        .Col = .GetColFromID("rst") : .Text = dt7.Rows(ix).Item("rslt1").ToString
                        .Col = .GetColFromID("rstunit") : .Text = dt7.Rows(ix).Item("rsltunit").ToString
                        .Col = .GetColFromID("rstmax") : .Text = dt7.Rows(ix).Item("rsltupp").ToString
                        .Col = .GetColFromID("rstmin") : .Text = dt7.Rows(ix).Item("rsltlow").ToString
                    Else
                        txtRslt.Visible = True
                        spdList_SetRst.Visible = False
                        txtRslt.Text = dt7.Rows(ix).Item("rslt2").ToString
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            'Fn.log(msFile & sFn, Err)
            'MsgBox(ex.Message)
        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

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
    Friend WithEvents txtOrdNm As System.Windows.Forms.TextBox
    Friend WithEvents lblOrdNm As System.Windows.Forms.Label
    Friend WithEvents lblExecDate As System.Windows.Forms.Label
    Friend WithEvents lblRsltDate As System.Windows.Forms.Label
    Friend WithEvents txtExecDate As System.Windows.Forms.TextBox
    Friend WithEvents txtRsltDate As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGOCS01_S02))
        Me.txtOrdNm = New System.Windows.Forms.TextBox
        Me.lblOrdNm = New System.Windows.Forms.Label
        Me.txtExecDate = New System.Windows.Forms.TextBox
        Me.lblExecDate = New System.Windows.Forms.Label
        Me.txtRsltDate = New System.Windows.Forms.TextBox
        Me.lblRsltDate = New System.Windows.Forms.Label
        Me.lblUsrNm = New System.Windows.Forms.Label
        Me.txtUsrNm = New System.Windows.Forms.TextBox
        Me.pnlSetRst = New System.Windows.Forms.Panel
        Me.spdList_SetRst = New AxFPSpreadADO.AxfpSpread
        Me.txtRslt = New System.Windows.Forms.TextBox
        Me.pnlSetRst.SuspendLayout()
        CType(Me.spdList_SetRst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtOrdNm
        '
        Me.txtOrdNm.BackColor = System.Drawing.Color.White
        Me.txtOrdNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrdNm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOrdNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtOrdNm.Location = New System.Drawing.Point(84, 13)
        Me.txtOrdNm.MaxLength = 16
        Me.txtOrdNm.Name = "txtOrdNm"
        Me.txtOrdNm.ReadOnly = True
        Me.txtOrdNm.Size = New System.Drawing.Size(495, 21)
        Me.txtOrdNm.TabIndex = 156
        Me.txtOrdNm.Text = "20050301-M0-0001-0"
        '
        'lblOrdNm
        '
        Me.lblOrdNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblOrdNm.ForeColor = System.Drawing.Color.Black
        Me.lblOrdNm.Location = New System.Drawing.Point(13, 13)
        Me.lblOrdNm.Name = "lblOrdNm"
        Me.lblOrdNm.Size = New System.Drawing.Size(70, 21)
        Me.lblOrdNm.TabIndex = 153
        Me.lblOrdNm.Text = "처방명"
        Me.lblOrdNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtExecDate
        '
        Me.txtExecDate.BackColor = System.Drawing.Color.White
        Me.txtExecDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExecDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtExecDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtExecDate.Location = New System.Drawing.Point(84, 35)
        Me.txtExecDate.MaxLength = 16
        Me.txtExecDate.Name = "txtExecDate"
        Me.txtExecDate.ReadOnly = True
        Me.txtExecDate.Size = New System.Drawing.Size(117, 21)
        Me.txtExecDate.TabIndex = 158
        Me.txtExecDate.Text = "20050301-M0-0001-0"
        Me.txtExecDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblExecDate
        '
        Me.lblExecDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblExecDate.ForeColor = System.Drawing.Color.Black
        Me.lblExecDate.Location = New System.Drawing.Point(13, 35)
        Me.lblExecDate.Name = "lblExecDate"
        Me.lblExecDate.Size = New System.Drawing.Size(70, 21)
        Me.lblExecDate.TabIndex = 157
        Me.lblExecDate.Text = "검사일시"
        Me.lblExecDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRsltDate
        '
        Me.txtRsltDate.BackColor = System.Drawing.Color.White
        Me.txtRsltDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRsltDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRsltDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRsltDate.Location = New System.Drawing.Point(273, 35)
        Me.txtRsltDate.MaxLength = 16
        Me.txtRsltDate.Name = "txtRsltDate"
        Me.txtRsltDate.ReadOnly = True
        Me.txtRsltDate.Size = New System.Drawing.Size(117, 21)
        Me.txtRsltDate.TabIndex = 160
        Me.txtRsltDate.Text = "20050301-M0-0001-0"
        Me.txtRsltDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblRsltDate
        '
        Me.lblRsltDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRsltDate.ForeColor = System.Drawing.Color.Black
        Me.lblRsltDate.Location = New System.Drawing.Point(202, 35)
        Me.lblRsltDate.Name = "lblRsltDate"
        Me.lblRsltDate.Size = New System.Drawing.Size(70, 21)
        Me.lblRsltDate.TabIndex = 159
        Me.lblRsltDate.Text = "보고일시"
        Me.lblRsltDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUsrNm
        '
        Me.lblUsrNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUsrNm.ForeColor = System.Drawing.Color.Black
        Me.lblUsrNm.Location = New System.Drawing.Point(391, 35)
        Me.lblUsrNm.Name = "lblUsrNm"
        Me.lblUsrNm.Size = New System.Drawing.Size(70, 21)
        Me.lblUsrNm.TabIndex = 161
        Me.lblUsrNm.Text = "판독"
        Me.lblUsrNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUsrNm
        '
        Me.txtUsrNm.BackColor = System.Drawing.Color.White
        Me.txtUsrNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrNm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUsrNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUsrNm.Location = New System.Drawing.Point(462, 35)
        Me.txtUsrNm.MaxLength = 16
        Me.txtUsrNm.Name = "txtUsrNm"
        Me.txtUsrNm.ReadOnly = True
        Me.txtUsrNm.Size = New System.Drawing.Size(117, 21)
        Me.txtUsrNm.TabIndex = 162
        Me.txtUsrNm.Text = "20050301-M0-0001-0"
        Me.txtUsrNm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pnlSetRst
        '
        Me.pnlSetRst.Controls.Add(Me.spdList_SetRst)
        Me.pnlSetRst.Controls.Add(Me.txtRslt)
        Me.pnlSetRst.Location = New System.Drawing.Point(4, 67)
        Me.pnlSetRst.Name = "pnlSetRst"
        Me.pnlSetRst.Size = New System.Drawing.Size(585, 382)
        Me.pnlSetRst.TabIndex = 163
        '
        'spdList_SetRst
        '
        Me.spdList_SetRst.DataSource = Nothing
        Me.spdList_SetRst.Location = New System.Drawing.Point(8, 3)
        Me.spdList_SetRst.Name = "spdList_SetRst"
        Me.spdList_SetRst.OcxState = CType(resources.GetObject("spdList_SetRst.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList_SetRst.Size = New System.Drawing.Size(567, 373)
        Me.spdList_SetRst.TabIndex = 0
        '
        'txtRslt
        '
        Me.txtRslt.Location = New System.Drawing.Point(8, 3)
        Me.txtRslt.Multiline = True
        Me.txtRslt.Name = "txtRslt"
        Me.txtRslt.Size = New System.Drawing.Size(567, 373)
        Me.txtRslt.TabIndex = 1
        Me.txtRslt.Visible = False
        '
        'FGOCS01_S01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(595, 456)
        Me.Controls.Add(Me.pnlSetRst)
        Me.Controls.Add(Me.txtUsrNm)
        Me.Controls.Add(Me.lblUsrNm)
        Me.Controls.Add(Me.txtRsltDate)
        Me.Controls.Add(Me.lblRsltDate)
        Me.Controls.Add(Me.txtExecDate)
        Me.Controls.Add(Me.lblExecDate)
        Me.Controls.Add(Me.txtOrdNm)
        Me.Controls.Add(Me.lblOrdNm)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGOCS01_S01"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SET검사결과"
        Me.pnlSetRst.ResumeLayout(False)
        Me.pnlSetRst.PerformLayout()
        CType(Me.spdList_SetRst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

End Class
