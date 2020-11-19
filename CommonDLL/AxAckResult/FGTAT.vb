'>>> TAT 사유 등록
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN

Public Class FGTAT
    Private Const msFile As String = "File : FGTAT.vb, Class : AxAckResult.FGTAT" + vbTab

    Private msBcNo As String = ""
    Private msPartSlip As String = ""
    Private m_al_TestCd As ArrayList

    Private Sub sbDisplay_CmtCont()
        Dim sFn As String = "Private Sub sbDisplay_CmtCont()"

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_cmtcont_etc("C", False)

            Me.cboCmtCont.Items.Clear()
            Me.cboCmtCont.Items.Add("")

            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Me.cboCmtCont.Items.Add("[" + dt.Rows(ix).Item("cmtcd").ToString + "] " + dt.Rows(ix).Item("cmtcont").ToString)
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 검사항목 표시 
    Private Sub sbDisplay_DataView(ByVal rsBcNo As String, ByVal rsPartSlip As String)
        Dim sFn As String = "Private Sub sbDisplay_DataView(String)"

        Try
            Me.spdList.MaxRows = 0

            If rsBcNo = "" Then Return

            Dim dt As DataTable = LISAPP.APP_R.TatFn.fnGet_TatInfo_bcno(rsBcNo, rsPartSlip)
            If dt.Rows.Count < 1 Then Return
            With spdList
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1

                    .Row = ix + 1

                    If m_al_TestCd.Contains(dt.Rows(ix).Item("testcd").ToString) Then
                        .Col = .GetColFromID("chk") : .Text = "1"
                    End If

                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString.Trim
                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString.Trim
                    .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString.Trim
                    .Col = .GetColFromID("tat1") : .Text = dt.Rows(ix).Item("tat1").ToString.Trim
                    .Col = .GetColFromID("tat2") : .Text = dt.Rows(ix).Item("tat2").ToString.Trim
                    .Col = .GetColFromID("mwdt") : .Text = dt.Rows(ix).Item("mwdt").ToString.Trim
                    .Col = .GetColFromID("fndt") : .Text = dt.Rows(ix).Item("fndt").ToString.Trim

                    .Col = .GetColFromID("tnmd")
                    If dt.Rows(ix).Item("tcdgbn").ToString().Trim = "C" Then
                        If dt.Rows(ix).Item("tclscd").ToString.Trim = dt.Rows(ix).Item("testcd").ToString.Substring(1, 5).Trim Then
                            .Text = "... " + dt.Rows(ix).Item("tnmd").ToString().Trim
                        Else
                            .Text = ".... " + dt.Rows(ix).Item("tnmd").ToString().Trim
                        End If
                    ElseIf dt.Rows(ix).Item("tcdgbn").ToString.Trim = "B" Or _
                           dt.Rows(ix).Item("testcd").ToString.Trim = dt.Rows(ix).Item("tclscd").ToString.Trim Then
                        .Text = dt.Rows(ix).Item("tnmd").ToString().Trim
                    Else
                        .Text = ". " + dt.Rows(ix).Item("tnmd").ToString().Trim
                    End If

                    Dim sgTat_m As Single = CSng(IIf(dt.Rows(ix).Item("tat_m").ToString.Trim = "", 0, dt.Rows(ix).Item("tat_m").ToString.Trim))
                    Dim sgTat_f As Single = CSng(IIf(dt.Rows(ix).Item("tat_f").ToString.Trim = "", 0, dt.Rows(ix).Item("tat_f").ToString.Trim))
                    Dim sgPrptmi As Single = CSng(IIf(dt.Rows(ix).Item("prptmi").ToString.Trim = "", 0, dt.Rows(ix).Item("prptmi").ToString.Trim))
                    Dim sgFrptmi As Single = CSng(IIf(dt.Rows(ix).Item("frptmi").ToString.Trim = "", 0, dt.Rows(ix).Item("frptmi").ToString.Trim))

                    If sgTat_m > sgPrptmi And sgPrptmi > 0 Then
                        .Col = .GetColFromID("tat1")
                        .ForeColor = System.Drawing.Color.Red
                        .BackColor = System.Drawing.Color.FromArgb(255, 202, 202)

                    End If

                    If sgTat_f > sgFrptmi And sgFrptmi > 0 Then
                        .Col = .GetColFromID("tat1")
                        .ForeColor = System.Drawing.Color.Red
                        .BackColor = System.Drawing.Color.FromArgb(255, 202, 202)
                    End If

                    If ix = 0 Then

                        Me.lblBCNO.Text = Fn.BCNO_View(dt.Rows(ix).Item("bcno").ToString, True)
                        Me.lblOrdDt.Text = dt.Rows(ix).Item("orddt").ToString
                        Me.lblRegNo.Text = dt.Rows(ix).Item("regno").ToString
                        Me.lblPatNm.Text = dt.Rows(ix).Item("patnm").ToString
                        Me.lblSexAge.Text = dt.Rows(ix).Item("sexage").ToString

                        Dim sPatInfo() As String = dt.Rows(ix).Item("patinfo").ToString.Split("|"c) ''' 정은수정 
                        Me.lblIdNo.Text = sPatInfo(3) ''' 정은 수정 
                        Me.lblDoctor.Text = dt.Rows(ix).Item("doctornm").ToString
                        Me.lblDeptNm.Text = dt.Rows(ix).Item("deptnm").ToString
                        Me.lblWard_SR.Text = dt.Rows(ix).Item("wardroom").ToString
                        Me.lblSpcNm.Text = dt.Rows(ix).Item("spcnmd").ToString

                        Me.lblCollectDt.Text = dt.Rows(ix).Item("colldt").ToString
                        Me.lblCollectID.Text = dt.Rows(ix).Item("collnm").ToString
                        Me.lblTkDt.Text = dt.Rows(ix).Item("tkdt").ToString
                        Me.lblTkID.Text = dt.Rows(ix).Item("tknm").ToString

                    End If

                    If dt.Rows(ix).Item("cmtcont").ToString.Trim <> "" Then
                        Me.txtTatCont.Text += dt.Rows(ix).Item("tnmd").ToString().Trim.PadRight(30, " "c) + ": " + dt.Rows(ix).Item("cmtcont").ToString.Trim + vbCrLf
                    End If
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message + " @" + msFile + sFn)

        End Try

    End Sub

    ' 데이타 유효성 체크
    Private Function fnValidation() As Boolean
        Dim sFn As String = "Private Function fnValidation() As Boolean"

        Try

            With Me.spdList
                Dim iRow As Integer = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If Me.spdList.MaxRows < 1 Or iRow < 1 Then
                    MsgBox(btnReg.Text + "할 검사항목을 선택해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Return False
                End If

            End With

            If Me.txtCmtCont.Text = "" Then
                MsgBox(btnReg.Text.Replace("(F5)", "") + " 사유를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                Me.txtCmtCont.Focus()
                Exit Function
            End If

            Return True

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsBcNo As String, ByVal rsPartSlip As String, ByVal r_al_Test As ArrayList)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        msBcNo = rsBcNo
        msPartSlip = rsPartSlip
        m_al_TestCd = r_al_Test

    End Sub

    Private Sub FGTAT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnReg_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub FGTAT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        sbDisplay_CmtCont()
        sbDisplay_DataView(msBcNo, msPartSlip)

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "Handles btnReg.Click"
        Try

            If fnValidation() = False Then Exit Sub
            Dim sTestCds As String = ""

            With Me.spdList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                    If sChk = "1" Then
                        sTestCds += IIf(sTestCds = "", "", "|").ToString + sTestCd
                    End If
                Next
            End With

            If LISAPP.APP_R.TatFn.fnExe_Tat_Reg(msBcNo, sTestCds, Me.txtCmtCd.Text.Trim, Me.txtCmtCont.Text) Then
                Me.Close()
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "TAT 등록시 오류가 발생했습니다.!!" + msFile + sFn)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message + " @" + msFile + sFn)

        End Try

    End Sub

    Private Sub cboCmtCont_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboCmtCont.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub cboCmtCont_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCmtCont.SelectedValueChanged
        If Me.cboCmtCont.Text <> "" Then
            Me.txtCmtCont.Text += Ctrl.Get_Name(Me.cboCmtCont)
            Me.txtCmtCd.Text = Ctrl.Get_Code(cboCmtCont)
        End If

        If Me.txtCmtCont.Text = "" Then
            Me.txtCmtCont.Focus()
        Else
            Me.btnReg.Focus()
        End If
    End Sub

    Private Sub txtCmtCd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCmtCd.GotFocus
        Me.txtCmtCd.SelectionStart = 0
        Me.txtCmtCd.SelectAll()
    End Sub

    Private Sub txtCmtCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown
        Dim sFn As String = "Handles txtCmtCd.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtCmtCd.Text = "" Then Return

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_cmtcont_etc("A", False, Me.txtCmtCd.Text)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "조치사항"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("cmtcont", "내용", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("cmtcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtCmtCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + Me.txtCmtCd.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtCmtCont.Text += alList.Item(0).ToString.Split("|"c)(0) + vbCrLf
                Me.txtCmtCd.Text = alList.Item(0).ToString.Split("|"c)(1)

                For ix As Integer = 0 To cboCmtCont.Items.Count - 1
                    Me.cboCmtCont.SelectedIndex = ix
                    If Me.cboCmtCont.Text = "[" + Me.txtCmtCd.Text + "] " + Me.txtCmtCont.Text Then
                        Me.cboCmtCont.SelectedIndex = ix
                        Exit For
                    End If
                Next

                SendKeys.Send("{TAB}")
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
End Class