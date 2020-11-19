'>>> 종합검증

Imports COMMON.CommFN
Imports common.commlogin.login
Imports COMMON.CommConst

Public Class FGR09
    Inherits FGR08
    Private Const msFile As String = "File : FGR09.vb, Class : FGR09" & vbTab

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal riUseMode As Integer, ByVal rsCd As String)
        MyBase.New(riUseMode, rsCd)

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
    End Sub

    Protected Overrides Sub sbDisplay_Search(ByVal rsOpt As String)
        Dim sFn As String = "sbDisplay_Search"

        Dim dt As DataTable

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            Dim sSlipCdCd As String = Ctrl.Get_Code(Me.cboSlip)

            If Ctrl.Get_Code(Me.cboSlip).Length < 2 Then
                MsgBox("검사분야 코드가 없습니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sTestCds As String = ""

            With Me.spdSpTest
                For i As Integer = 1 To .MaxRows
                    Dim sChk As String = Ctrl.Get_Code(Me.spdSpTest, "chk", i)
                    Dim sTCd As String = Ctrl.Get_Code(Me.spdSpTest, "testcd", i)

                    If sChk = "1" Then
                        If sTestCds.Length = 0 Then
                            sTestCds += sTCd
                        Else
                            sTestCds += "," + sTCd
                        End If
                    End If
                Next
            End With

            If sTestCds.Length = 0 Then
                MsgBox("선택한 검사코드가 없습니다. 확인하여 주십시요!!")
                Return
            End If

            sTestCds = "'" + sTestCds.Replace(",", "','") + "'"

            Dim iUsrOpt As Integer = 0

            If Me.rdoUserMe.Checked Then iUsrOpt = 1

            dt = LISAPP.APP_G.CommFn.Get_SpcList_Test_User(Ctrl.Get_Code(Me.cboSlip), Me.dtpTkS.Text.Replace("-", ""), Me.dtpTkE.Text.Replace("-", ""), _
                                                 rsOpt, sTestCds, iUsrOpt, USER_INFO.USRID)

            '접수일시에 정렬 표시
            spd.set_ColUserSortIndicator(spd.GetColFromID("tkdt"), FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)

            Ctrl.DisplayAfterSelect(spd, dt)

            spd.SetActiveCell(0, 0)

            sbDisplay_Search_Color(rsOpt)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub btnTkCc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTkCc.Click
        Dim frm As Windows.Forms.Form

        frm = Ctrl.CheckFormObject(Me, Me.btnTkCc.Tag.ToString())

        If frm Is Nothing Then frm = New LISR.FGR10

        'frm.MdiParent = Me
        'frm.WindowState = Windows.Forms.FormWindowState.Maximized
        frm.Text = Me.btnTkCc.Tag.ToString()
        frm.Activate()
        frm.ShowDialog()
    End Sub

    Private Sub FGR09_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = Windows.Forms.FormWindowState.Maximized
        btnReg_FnAll.Visible = False
        btnReg_FnBcno.Visible = False

        btnPrint_All.Visible = True
        btnCancel.Visible = True

    End Sub

    Private Sub FGR_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub btnAdd_Test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd_Test.Click
        Try
            If Me.lblRstFlg.Text <> FixedVariable.gsRstFlagF Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "최종보고된 자료만 소견추가 할 수 있습니다.!!")
                Return
            End If

            Dim bOk As Boolean = (New LISAPP.APP_G.RegFn).fnExe_Test_Add(Me.txtBcno.Text)

            If bOk Then
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "추가소견 발생에 실패 했습니다.!!")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
End Class