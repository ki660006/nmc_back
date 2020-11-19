'>> 현장검사
Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports common.commlogin.login

Public Class FGR05
    Private Const msFile As String = "File : FGR05.vb, Class : FGR05" + vbTab

    Private msIoGbn As String = "I"
    Private msDeptOrWard As String = ""
    Private msRegNo As String = ""
    Private msOrdDt As String = ""
    Private m_al_BcNo As ArrayList = Nothing

    Private mbOcsCall As Boolean = False

    Private Sub sbClear_Form()
        Me.AxPatInfo.sbDisplay_Init()
        Me.axResult.sbDisplay_Init("ALL")

        Me.spdList.MaxRows = 0

    End Sub

    Private Sub sbDisplay_Test()
        Dim sFn As String = "sbDisplay_Test"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_poct()

            Me.cboTest.Items.Clear()
            Me.cboTest.Items.Add("" + Space(200) + "|")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboTest.Items.Add(dt.Rows(ix).Item("tnmd").ToString + Space(100) + "|" + dt.Rows(ix).Item("testcd").ToString.Trim)
            Next

            If Me.cboTest.Items.Count > 0 Then Me.cboTest.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)

        End Try
    End Sub

    Private Sub sbDisplay_Ward()
        Dim sFn As String = "sbDisplay_Ward"

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList()

            Me.cboDptOrWard.Items.Clear()

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDptOrWard.Items.Add("[" + dt.Rows(ix).Item("wardno").ToString + "] " + dt.Rows(ix).Item("wardnm").ToString)
                If dt.Rows(ix).Item("wardno").ToString.Trim = msDeptOrWard Then Me.cboDptOrWard.SelectedIndex = ix
            Next

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)

        End Try
    End Sub

    Private Sub sbDisplay_Dept()
        Dim sFn As String = "sbDisplay_Dept"

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList()

            Me.cboDptOrWard.Items.Clear()
            Me.cboDptOrWard.Items.Add("[ ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDptOrWard.Items.Add("[" + dt.Rows(ix).Item("deptcd").ToString + "] " + dt.Rows(ix).Item("deptnm").ToString)

                If dt.Rows(ix).Item("deptcd").ToString.Trim = msDeptOrWard Then Me.cboDptOrWard.SelectedIndex = ix
            Next

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)

        End Try
    End Sub

    Private Sub sbDisplay_PatList(Optional ByVal rsRegNo As String = "")
        Dim sFn As String = "sbDisplay_PatList"

        Try
            sbClear_Form()

            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim sWhere As String = ""

            Dim dt As DataTable = New DataTable
            Dim dr As DataRow()

            dt = OCSAPP.OcsLink.Ord.fnGet_Coll_PatList_poct(Me.dtpDateS.Text, Me.dtpDateE.Text)

            sWhere += "iogbn = '" + msIoGbn + "'"

            If rsRegNo <> "" Then sWhere += " AND regno = '" + rsRegNo + "'"

            If msIoGbn = "I" Then
                sWhere += " AND wardno = '" + Ctrl.Get_Code(Me.cboDptOrWard) + "'"
            ElseIf Ctrl.Get_Code(Me.cboDptOrWard) <> "" Then
                sWhere += " AND deptcd = '" + Ctrl.Get_Code(Me.cboDptOrWard) + "'"
            End If

            Select Case Ctrl.Get_Code(Me.cboRstFlg)
                Case "3" : sWhere += " AND rstflg = '3'"
                Case "0" : sWhere += " AND spcflg = ''"
            End Select

            dr = dt.Select(sWhere, "patinfo")
            dt = Fn.ChangeToDataTable(dr)

            With spdList
                .ReDraw = False
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Dim sPatInfo() As String = dt.Rows(ix).Item("patinfo").ToString.Split("|"c)
                    '< 나이계산
                    Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                    Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                    If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                    '>
                    .Row = ix + 1
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                    .Col = .GetColFromID("sex") : .Text = sPatInfo(1).Trim
                    .Col = .GetColFromID("age") : .Text = iAge.ToString
                    .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString.Trim


                    'hidden col
                    .Col = .GetColFromID("owngbn") : .Text = dt.Rows(ix).Item("owngbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = dt.Rows(ix).Item("fkocs").ToString
                Next

                .ReDraw = True
            End With

            spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1))

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_PatList(ByVal r_al_bcno As ArrayList)
        Dim sFn As String = "sbDisplay_PatList"

        Try
            sbClear_Form()
            Me.spdList.ReDraw = False
            Me.spdList.MaxRows = 0

            Dim dtSysDate As Date = Fn.GetServerDateTime()

            For ix As Integer = 0 To r_al_bcno.Count - 1

                Dim dt As DataTable = OCSAPP.OcsLink.Ord.fnGet_Coll_PatList_bcno(r_al_bcno(ix).ToString)

                With spdList
                    .MaxRows += 1
                    Dim sPatInfo() As String = dt.Rows(0).Item("patinfo").ToString.Split("|"c)
                    '< 나이계산
                    Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                    Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                    If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                    '>
                    .Row = .MaxRows
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(0).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                    .Col = .GetColFromID("sex") : .Text = sPatInfo(1).Trim
                    .Col = .GetColFromID("age") : .Text = iAge.ToString
                    .Col = .GetColFromID("orddt") : .Text = dt.Rows(0).Item("orddt").ToString.Trim

                    'hidden col
                    .Col = .GetColFromID("owngbn") : .Text = dt.Rows(0).Item("owngbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = dt.Rows(0).Item("fkocs").ToString
                End With
            Next

            spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1))

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(ex.Message)
        Finally
            Me.spdList.ReDraw = True
        End Try

    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        If e.row < 1 Or Me.spdList.MaxRows < 1 Then Exit Sub

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Dim sOwnGbn As String = ""
        Dim sFkOcs As String = ""
        Dim sRegNo As String = ""
        Dim sOrdDt As String = ""

        With Me.spdList
            .Row = e.row
            .Col = .GetColFromID("fkocs") : sFkOcs = .Text
            .Col = .GetColFromID("owngbn") : sOwnGbn = .Text
            .Col = .GetColFromID("regno") : sRegNo = .Text
            .Col = .GetColFromID("orddt") : sOrdDt = .Text
        End With

        Me.AxPatInfo.BcNo = ""
        Me.AxPatInfo.SlipCd = Ctrl.Get_Code("")
        Me.AxPatInfo.fnDisplay_Data(sRegNo, sOrdDt)

        Me.axResult.FORMID = Me.Name  ''' 정은추가  
        Me.axResult.Form = Me
        Me.axResult.RegNo = AxPatInfo.RegNo
        Me.axResult.PatName = AxPatInfo.PatNm
        Me.axResult.SexAge = AxPatInfo.SexAge
        Me.axResult.DeptCd = AxPatInfo.DeptName
        Me.axResult.FnDt = AxPatInfo.FnDt
        Me.axResult.OwnGbn = sOwnGbn
        Me.axResult.FkOcs = sFkOcs
        Me.axResult.TestCd = Me.cboTest.Text.Split("|"c)(1)

        Me.axResult.sbDisplay_Data()

        axResult.sbFocus()
        axResult.Focus()

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub FGR_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGR05_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DS_FormDesige.sbInti(Me)

        Me.dtpDateS.Value = Now
        Me.dtpDateE.Value = Now

        sbDisplay_Test()

        If msIoGbn = "I" Then
            Me.lblDptOrWard.Text = "병    동"
            sbDisplay_Ward()
        Else
            Me.lblDptOrWard.Text = "진 료 과"
            sbDisplay_Dept()
        End If

        If (USER_INFO.N_IOGBN = "WARD" Or USER_INFO.N_IOGBN = "OUT") And USER_INFO.N_WARDorDEPT <> "" Then
            Me.cboDptOrWard.Enabled = False
        End If

        Me.AxPatInfo.UsrLevel = STU_AUTHORITY.usrid
        Me.AxPatInfo.sbDisplay_Init()

        Me.axResult.Form = Me
        Me.axResult.ColHiddenYn = True

        sbClear_Form()

        If msOrdDt <> "" Then Me.dtpDateS.Value = CDate(msOrdDt)

        If msRegNo <> "" Then
            Me.txtRegNo.Text = msRegNo
            Me.txtRegNo_KeyDown(Me.txtRegNo, New Windows.Forms.KeyEventArgs(Windows.Forms.Keys.Enter))
        ElseIf m_al_BcNo IsNot Nothing Then
            sbDisplay_PatList(m_al_BcNo)
        End If

        Me.WindowState = Windows.Forms.FormWindowState.Maximized

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsIoGbn As String, ByVal r_al_bcno As ArrayList)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        msIoGbn = rsIoGbn
        m_al_BcNo = r_al_bcno
    End Sub

    Public Sub New(ByVal rsIOGbn As String, ByVal rsDptWard As String, ByVal rsRegNo As String, ByVal rsOrdDt As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        msIoGbn = rsIOGbn
        msDeptOrWard = rsDptWard
        msRegNo = rsRegNo
        msOrdDt = rsOrdDt

    End Sub

    Private Sub cboTest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTest.SelectedIndexChanged

        Me.spdList.MaxRows = 0

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        sbDisplay_PatList()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        sbClear_Form()

    End Sub

    Private Sub btnFN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFN.Click
        Dim bRst As Boolean = False

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        bRst = axResult.fnReg()
        If bRst Then
            Me.AxPatInfo.sbDisplay_Init()
            Me.axResult.sbDisplay_Init("ALL")

            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)

            Me.txtRegNo.SelectAll()
            Me.txtRegNo.Focus()
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtRegNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.Click
        Me.txtRegNo.SelectionStart = 0
        Me.txtRegNo.SelectAll()
    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return
        If Me.txtRegNo.Text = "" Then Return

        sbDisplay_PatList(Me.txtRegNo.Text)

        Me.txtRegNo.Text = ""
        Me.txtRegNo.Focus()

    End Sub
End Class