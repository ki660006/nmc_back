'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : ������������� Laboratory Information System(KMC_LIS)                  */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : FGB07_S01.vb                                                           */
'/* PartName     : ��������-Cross Matching ���(�����):�Է���ȸ                          */
'/* Description  :                                                                        */
'/* Design       : 2003-06-24 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     : 2004-07-26 Jin Hwa Ji ������                                         */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System.Windows.Forms
Imports COMMON.CommFN

Public Class FGB07_S01
    Inherits System.Windows.Forms.Form
    Private Const sFile As String = "File : FGB07_S01.vb, Class : B01" & vbTab
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnQuery As System.Windows.Forms.Button

    Public msTNSJUBSUNO As String = ""

    'Private mComInfo As clsComInfo

    'Public ReadOnly Property SelectCom() As clsComInfo
    '    Get
    '        SelectCom = mComInfo
    '    End Get
    'End Property

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.
        'fnFormInitialize()

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblComNm As System.Windows.Forms.Label
    Friend WithEvents txtComCd As System.Windows.Forms.TextBox
    Friend WithEvents txtBldNo As System.Windows.Forms.TextBox
    Friend WithEvents btnComHlp As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnQuery = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblComNm = New System.Windows.Forms.Label
        Me.btnComHlp = New System.Windows.Forms.Button
        Me.txtComCd = New System.Windows.Forms.TextBox
        Me.txtBldNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSearch = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnQuery)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblComNm)
        Me.GroupBox1.Controls.Add(Me.btnComHlp)
        Me.GroupBox1.Controls.Add(Me.txtComCd)
        Me.GroupBox1.Controls.Add(Me.txtBldNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblSearch)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(310, 134)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(204, 100)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 26)
        Me.btnExit.TabIndex = 111
        Me.btnExit.Text = "��  ��(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuery.Location = New System.Drawing.Point(108, 100)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(96, 26)
        Me.btnQuery.TabIndex = 110
        Me.btnQuery.Text = "��  ��"
        Me.btnQuery.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Location = New System.Drawing.Point(7, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(296, 2)
        Me.Label3.TabIndex = 109
        '
        'lblComNm
        '
        Me.lblComNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblComNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblComNm.Location = New System.Drawing.Point(92, 68)
        Me.lblComNm.Name = "lblComNm"
        Me.lblComNm.Size = New System.Drawing.Size(208, 21)
        Me.lblComNm.TabIndex = 108
        Me.lblComNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnComHlp
        '
        Me.btnComHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnComHlp.Location = New System.Drawing.Point(164, 44)
        Me.btnComHlp.Name = "btnComHlp"
        Me.btnComHlp.Size = New System.Drawing.Size(28, 22)
        Me.btnComHlp.TabIndex = 2
        Me.btnComHlp.Text = "..."
        '
        'txtComCd
        '
        Me.txtComCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComCd.Location = New System.Drawing.Point(92, 44)
        Me.txtComCd.Name = "txtComCd"
        Me.txtComCd.Size = New System.Drawing.Size(72, 21)
        Me.txtComCd.TabIndex = 1
        '
        'txtBldNo
        '
        Me.txtBldNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldNo.Location = New System.Drawing.Point(92, 16)
        Me.txtBldNo.MaxLength = 10
        Me.txtBldNo.Name = "txtBldNo"
        Me.txtBldNo.Size = New System.Drawing.Size(100, 21)
        Me.txtBldNo.TabIndex = 0
        Me.txtBldNo.Text = "2002123456"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SlateGray
        Me.Label1.Font = New System.Drawing.Font("����", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(8, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 22)
        Me.Label1.TabIndex = 104
        Me.Label1.Text = "��������"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.SlateGray
        Me.lblSearch.Font = New System.Drawing.Font("����", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSearch.Location = New System.Drawing.Point(8, 16)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(84, 22)
        Me.lblSearch.TabIndex = 103
        Me.lblSearch.Text = "���׹�ȣ"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGB07_S01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(310, 131)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGB07_S01"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "�Է���ȸ"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " ���� ��ư ó�� "
    '' Function Key����
    'Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.ButtonClick
    '    Me.Close()
    'End Sub

    'Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.ButtonClick
    '    mComInfo = New clsComInfo
    '    Me.Close()
    'End Sub

#End Region

#Region " Form���� �Լ� "
    ' Form�ʱ�ȭ
    Private Sub fnFormInitialize()

        'txtBldNo.Text = ""
        'txtComCd.Text = ""
        'lblComNm.Text = ""

    End Sub

#End Region

#Region " Control Event ó�� "
    'Private Sub txtComCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComCd.Validated
    '    Dim sFn As String = "Private Sub txtcomcd_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComCd.LostFocus"
    '    Dim objDTable As DataTable
    '    Dim objQryData As New DA01.CommCDHelp.PopWin_O01

    '    Try
    '        If txtComCd.Modified = True Then
    '            If Not txtComCd.Text.Equals("") Then
    '                objDTable = objQryData.ComForRegnoCdHlp(txtBldNo.Text.Replace("-", ""), txtComCd.Text)

    '                If objDTable.Rows.Count > 0 Then
    '                    txtComCd.Text = objDTable.Rows(0).Item(0).ToString
    '                    lblComNm.Text = objDTable.Rows(0).Item(1).ToString
    '                Else
    '                    MsgBox("�ش��ڵ尡 �������� �ʽ��ϴ�.")
    '                    lblComNm.Text = ""
    '                    txtComCd.Focus()
    '                    txtComCd.SelectAll()
    '                End If
    '            Else
    '                lblComNm.Text = ""
    '            End If

    '            txtComCd.Modified = False
    '        End If

    '    Catch ex As Exception
    '        Fn.log(sFile & sFn, Err)
    '        Fn.ExclamationErrMsg(Err, Me.Text)

    '    End Try
    'End Sub

    'Private Sub fnTxt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBldNo.KeyPress, txtComCd.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
    '        e.Handled = True : SendKeys.Send("{TAB}")
    '    End If
    'End Sub

#End Region

#Region " CodeHelp��ư ó��"

    Private Sub btnComHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComHlp.Click
        'Dim sFn As String = "Private Sub btnComHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComHlp.Click"
        'Dim CdHelp As New CDHELP01.CodeHelp.PopWin
        'Dim objDTable As New DataTable
        'Dim objQryData As New DA01.CommCDHelp.PopWin_O01

        'Try
        '    objDTable = objQryData.ComForRegnoCdHlp(txtBldNo.Text.Replace("-", ""))
        '    With CdHelp
        '        .SetFormText = "��������"
        '        .SetFieldAdd("�ڵ�", 8, )
        '        .SetFieldAdd("��������", 10)
        '        .SetFieldAdd("����", 4, enumHAlign.Center)
        '        .SetFieldAdd("���׹�ȣ", 10)
        '        .SetFieldAdd("ABO/Rh", 6, enumHAlign.Center)
        '        .SetFieldAdd("�����Ͻ�", 8)
        '        .SetFieldAdd("�԰��Ͻ�", 8)
        '        .SetFieldAdd("��ȿ�Ͻ�", 8)

        '        '-- Hidden �ʵ�
        '        .SetFieldAdd("ABO", , , True)
        '        .SetFieldAdd("RH", , , True)
        '        .SetFieldAdd("��Ϲ�ȣ", , , True)
        '        .SetFieldAdd("����������", , , True)
        '        .SetFieldAdd("�����ڵ�", , , True)

        '        .SetViewData = objDTable
        '        .ShowCdHelp(Me, CType(txtComCd, Control), enumCodeHelpFrm.Normal)
        '        If .SelDataCnt > 0 Then
        '            txtComCd.Text = .SelData(0)
        '            lblComNm.Text = .SelData(1)

        '            mComInfo = New clsComInfo
        '            mComInfo.COMCD = .SelData(0)
        '            mComInfo.COMNM = .SelData(1)
        '            mComInfo.DONGBN = .SelData(2)
        '            mComInfo.BLDNO = .SelData(3)
        '            mComInfo.DONDT = .SelData(5)
        '            mComInfo.INDT = .SelData(6)
        '            mComInfo.AVAILDT = .SelData(7)
        '            mComInfo.ABO = .SelData(8)
        '            mComInfo.RH = .SelData(9)
        '            mComInfo.REGNO = .SelData(10)
        '            mComInfo.COMVAL = .SelData(11)

        '        End If
        '    End With

        'Catch ex As Exception
        '    Fn.log(sFile & sFn, Err)
        '    Fn.ExclamationErrMsg(Err, Me.Text)

        'End Try

    End Sub

#End Region

    Private Sub FGB07_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtBldNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBldNo.Click
        txtBldNo.SelectAll()
    End Sub
End Class
