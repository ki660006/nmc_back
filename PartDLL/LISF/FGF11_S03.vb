Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst
Public Class FGF11_S03
    Public msTestcd As String = ""
    Public msSpcCd As String = ""
    Public msDataTable As DataTable = New DataTable

    Private mo_DAF As New LISAPP.APP_F_TEST

    Private Const msFile As String = "File : FGF11_S03.vb, Class : FGF11_S03" + vbTab

    Public Sub New(ByVal rsTestcd As String, ByVal rsSpccd As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        msTestcd = rsTestcd
        msSpcCd = rsSpccd
    End Sub

    Private Sub FGF11_S03_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sbDisplay_bccls()
        sbDisplay_tordslip()
        sInitial_BackCOlor()
        Me.cboPSGbn.SelectedIndex = 1
        Me.cboFilter.SelectedIndex = 0
        Me.cboOps.SelectedIndex = 5
    End Sub
    Private Sub sInitial_BackCOlor()
        msDataTable = LISAPP.COMM.CdFn.fnGet_DTestList(msTestcd, msSpcCd)

    End Sub
    Private Sub sbDisplay_tordslip()
        Dim sFn As String = "Sub sbDisplay_tordslip"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TOrdSlip()

            Me.cboTordSlip_q.Items.Clear()
            Me.cboTordSlip_q.Items.Add("[  ] 전체")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboTordSlip_q.Items.Add(dt.Rows(ix).Item("tordslipnm").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub
    Private Sub sbDisplay_bccls()
        Dim sFn As String = "Sub sbDisplay_bccls"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Bccls_List()

            Me.cboBccls_q.Items.Clear()
            Me.cboBccls_q.Items.Add("[  ] 전체")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboBccls_q.Items.Add("[" + dt.Rows(ix).Item("bcclscd").ToString.Trim + "] " + dt.Rows(ix).Item("bcclsnmd").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub
    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        sbDisplay_Test()

    End Sub
    Private Sub cboPSGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPSGbn.SelectedIndexChanged

        If Me.cboPSGbn.Text = "부서" Then
            sbDisplay_part()
        Else
            sbDisplay_slip()
        End If
    End Sub
    Private Sub sbDisplay_slip()
        Dim sFn As String = "Sub sbDisplay_slip"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()

            Me.cboPartSlip.Items.Clear()
            Me.cboPartSlip.Items.Add("[  ] 전체")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub
    Private Sub sbDisplay_part()
        Dim sFn As String = "Sub sbDisplay_part"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Part_List()

            Me.cboPartSlip.Items.Clear()
            Me.cboPartSlip.Items.Add("[ ] 전체")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub
    Private Sub txtFilter_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Me.btnQuery_Click(Nothing, Nothing)

    End Sub
    Private Sub cboFilter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFilter.SelectedIndexChanged
        If Me.cboFilter.Text.EndsWith("코드") Then
            Me.txtFilter.CharacterCasing = Windows.Forms.CharacterCasing.Upper
        Else
            Me.txtFilter.CharacterCasing = Windows.Forms.CharacterCasing.Normal
        End If
    End Sub
    Private Sub sbDisplay_Test()
        Dim sFn As String = "sbDisplay_Test"

        Try
            Me.spdCdList.MaxRows = 0
            Dim dt As New DataTable
            Dim iCol As Integer = 0

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim sWhere As String = ""

            If Ctrl.Get_Code(Me.cboBccls_q) <> "" Then sWhere += " AND bcclscd = '" + Ctrl.Get_Code(Me.cboBccls_q) + "'"
            If Ctrl.Get_Code(Me.cboTordSlip_q) <> "" Then sWhere += " AND tordslip = '" + Ctrl.Get_Code(Me.cboTordSlip_q) + "'"
            If Ctrl.Get_Code(Me.cboPartSlip) <> "" Then

                If Me.cboPSGbn.Text = "부서" Then
                    sWhere += " AND partcd = '" + Ctrl.Get_Code(Me.cboPartSlip.Text) + "'"
                Else
                    sWhere += " AND partcd = '" + Ctrl.Get_Code(Me.cboPartSlip.Text).Substring(0, 1) + "'"
                    sWhere += " AND slipcd = '" + Ctrl.Get_Code(Me.cboPartSlip.Text).Substring(1, 1) + "'"
                End If
            End If

            If Me.chkOrder.Checked Then
                sWhere += " AND tcdgbn IN ('G', 'B', 'S', 'P')"
                sWhere += " AND NVL(ordhide, '0') = '0'"
            End If

            If Me.chkCtGbn_q.Checked Then sWhere += " AND NVL(ctgbn, '0') = '1'"

            If Me.txtFilter.Text <> "" Then
                Select Case Me.cboFilter.Text.Replace(" ", "")
                    Case "검사코드" : sWhere += " AND testcd"
                    Case "검체코드" : sWhere += " AND spccd"
                    Case "처방코드" : sWhere += " AND tordcd"
                    Case "결과코드" : sWhere += " AND tliscd"
                    Case "검사구분" : sWhere += " AND tcdgbn"
                    Case "검사명" : sWhere += " AND tnmd"
                    Case "위탁기관명" : sWhere += " AND exlabnmd"
                End Select

                Select Case Me.cboOps.Text
                    Case "LIKE *" : sWhere += " LIKE '" + Me.txtFilter.Text + "%'"
                    Case "* LIKE" : sWhere += " LIKE '%" + Me.txtFilter.Text + "'"
                    Case "LIKE *" : sWhere += " LIKE '" + Me.txtFilter.Text + "%'"
                    Case "* LIKE *" : sWhere += " LIKE '%" + Me.txtFilter.Text + "%'"
                    Case "IN" : sWhere += " " + Me.cboOps.Text + " ('" + Me.txtFilter.Text.Replace(",", "','") + "')"
                    Case Else : sWhere += " " + Me.cboOps.Text + " '" + Me.txtFilter.Text + "'"
                End Select
            End If

            If sWhere <> "" Then sWhere = sWhere.Substring(4).Trim

            dt = mo_DAF.GetTestInfo(0, sWhere)

            If Me.rdoSort_spc.Checked Then
                Dim a_dr As DataRow() = dt.Select("", "spccd, testcd")
                dt = Fn.ChangeToDataTable(a_dr)
            ElseIf Me.rdoSort_lis.Checked Then
                Dim a_dr As DataRow() = dt.Select("", "slipnmd, dispseql, testcd, spccd")
                dt = Fn.ChangeToDataTable(a_dr)
            ElseIf Me.rdoSort_ocs.Checked Then
                Dim a_dr As DataRow() = dt.Select("", "tordslipnm, dispseqo, testcd, spccd")
                dt = Fn.ChangeToDataTable(a_dr)
            End If

            If dt.Rows.Count < 0 Then Return

            With spdCdList
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    .Row = i + 1
                    .Col = .GetColFromID("tcdgbn") : .Text = dt.Rows(i).Item("tcdgbn").ToString
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(i).Item("testcd").ToString
                    If dt.Rows(i).Item("testcd").ToString = msTestcd Then
                        .BlockMode = True
                        .Row = i + 1 : .Row2 = i + 1
                        .Col = 2 : .Col2 = .MaxCols
                        .BackColor = Color.Purple
                        .BlockMode = False

                    End If
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(i).Item("tnmd").ToString
                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(i).Item("spccd").ToString
                    .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(i).Item("spcnmd").ToString
                    .Col = .GetColFromID("tubenmd") : .Text = dt.Rows(i).Item("tubenmd").ToString
                    .Col = .GetColFromID("tordslipnm") : .Text = dt.Rows(i).Item("tordslipnm").ToString
                    .Col = .GetColFromID("ordhide") : .Text = dt.Rows(i).Item("ordhide").ToString
                    .Col = .GetColFromID("sugacd") : .Text = dt.Rows(i).Item("sugacd").ToString
                    .Col = .GetColFromID("dspccd1") : .Text = dt.Rows(i).Item("dspccd1").ToString
                    .Col = .GetColFromID("bcclsnmd") : .Text = dt.Rows(i).Item("bcclsnmd").ToString
                    .Col = .GetColFromID("slipnmd") : .Text = dt.Rows(i).Item("slipnmd").ToString
                    .Col = .GetColFromID("exlabnmd") : .Text = dt.Rows(i).Item("exlabnmd").ToString
                    .Col = .GetColFromID("exlabnmd") : .Text = dt.Rows(i).Item("exlabnmd").ToString
                    .Col = .GetColFromID("usdt") : .Text = dt.Rows(i).Item("usdt").ToString

                    For j As Integer = 0 To msDataTable.Rows.Count - 1
                        If dt.Rows(i).Item("testcd").ToString + dt.Rows(i).Item("spccd").ToString = msDataTable.Rows(j).Item("testcd").ToString + msDataTable.Rows(j).Item("spccd").ToString Then
                            .BlockMode = True
                            .Row = i + 1 : .Row2 = i + 1
                            .Col = 2 : .Col2 = .MaxCols
                            .BackColor = Color.LightBlue
                            .BlockMode = False
                        End If
                    Next

                    If i > 50 Then
                        .ReDraw = True
                    End If
                Next

                'For i As Integer = 0 To dt.Rows.Count - 1
                '    For j As Integer = 0 To dt.Columns.Count - 1
                '        iCol = 0
                '        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                '        If iCol > 0 Then
                '            .Col = iCol
                '            .Row = i + 1
                '            .Text = dt.Rows(i).Item(j).ToString.Trim
                '        End If
                '    Next

                '    'If CType(dt.Rows(i).Item("diffday"), Double) < 0 Then
                '    '    .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 14
                '    '    .BlockMode = True : .ForeColor = System.Drawing.Color.Red : .BlockMode = False
                '    'Else
                '    '    .Row = i + 1 : .Row2 = i + 1 : .Col = 1 : .Col2 = 14
                '    '    .BlockMode = True : .ForeColor = System.Drawing.Color.Black : .BlockMode = False
                '    'End If

                '    If i > 50 Then
                '        .ReDraw = True
                '    End If
                'Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Public Sub btnINSERT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnINSERT.Click
        Dim sFn As String = "Private Sub btnINSERT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnINSERT.Click"
        Try
            Dim arrTList As New ArrayList
            Dim CTESTLIST As New DTESTLIST : Dim sOverlabList As String = ""
            With spdCdList
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk")
                    If .Text = "1" Then
                        CTESTLIST = New DTESTLIST
                        .Col = .GetColFromID("testcd") : CTESTLIST.TESTCD = .Text
                        .Col = .GetColFromID("spccd") : CTESTLIST.SPCCCD = .Text
                        .Col = .GetColFromID("usdt") : CTESTLIST.USDT = .Text



                        arrTList.Add(CTESTLIST)

                    End If

                Next
                If mo_DAF.INSERTDTEST(arrTList, msTestcd, msSpcCd, sOverlabList) = True Then
                    MsgBox("세부검사항목이 정상등록되었습니다.!!", MsgBoxStyle.Information)

                End If

                If sOverlabList <> "" Then
                    MsgBox(sOverlabList + " 은 이미 등록되어 있기 때문에 제외되었습니다.!!", MsgBoxStyle.Critical)
                End If

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub FGF11_S03_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
       

        ' frmChild.Show()

    End Sub
End Class

