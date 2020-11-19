Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.CommLogin.LOGIN

Imports LISAPP.APP_DB
Imports LISAPP.APP_BT
Imports CDHELP.FGCDHELPFN

Public Class FGB05_S02

    Private m_aI_BldInfo As New ArrayList
    Private msUserId As String = ""
    Private mbSave As Boolean = False

    Private moComn As New ServerDateTime

    Public Function Display_Result(ByVal rsUsrId As String) As ArrayList
        Dim sFn As String = "Sub Display_Data(string)"

        msUserId = rsUsrId

        Try
            Me.spdList.MaxRows = 0

            Me.cboBType.SelectedIndex = 0
            Me.cboRH.SelectedIndex = 0

            Me.cboBldGbn.SelectedIndex = 0

            Me.dtpDonDt.Value = Now
            Me.dtpIndt.Value = Now

            Me.lblBType.Text = Me.cboBType.Text + Me.cboRH.Text
            Me.lblBType.ForeColor = fnGet_BloodColor(Me.cboBType.Text)
            If Me.cboRH.Text = "-" Then
                Me.lblBType.BackColor = Color.Red
            Else
                Me.lblBType.BackColor = Color.White
            End If

            Me.ShowDialog()

            If mbSave Then
                Return m_aI_BldInfo
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Function fnGet_DateCheck(ByVal rdPassDate As Date, ByVal rsRef As String) As Boolean
        Dim dtePressTime As Date
        Dim dtePassTime As Date

        dtePressTime = moComn.GetDateTime     ' 현재 시간
        'dtePassTime = CDate(Format(rdPassDate, "yyyy-MM-dd") & " " & "00:00:00")
        dtePassTime = CDate(Format(rdPassDate, "yyyy-MM-dd HH:mm"))

        If dtePassTime > dtePressTime Then   ' 현재보다 큰 미래의 일자는 선택 불가능

            MsgBox(rsRef + "가 현재 시간보다 크므로 선택할 수 없습니다", MsgBoxStyle.Information, Me.Text)

            If rsRef = "헌혈일자" Then
                dtpDonDt.Value = dtePressTime
            Else
                dtpIndt.Value = dtePressTime
            End If

            Return False
        Else
            Return True
        End If

    End Function

    Private Sub txtBldNm_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBldNm.GotFocus, txtBldQnt.GotFocus, txtBType.GotFocus, txtRegNo.GotFocus

        CType(sender, Windows.Forms.TextBox).SelectAll()

    End Sub


    Private Sub txtBldQnt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBldQnt.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim dt As DataTable = CGDA_BT.fnGet_ComCd(txtBldQnt.Text)      ' 혈액코드
            If dt.Rows.Count > 0 Then
                Me.lblComCd.Text = dt.Rows(0).Item("comcd").ToString()     ' 성분제제코드
                Me.lblComNmd.Text = dt.Rows(0).Item("comnmd").ToString()   ' 성분제제명
                Me.lblDonQnt.Text = dt.Rows(0).Item("donqnt").ToString
                Dim intAvailMi As Integer = CType(dt.Rows(0).Item("availmi").ToString(), Integer)   ' 유효기간
                'Dim dtAvailMi As Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Minute, intAvailMi, dtpDonDt.Value))   ' 유효기간
                Dim dtAvailMi As Date

                If dt.Rows(0).Item("platyn").ToString = "Y" Then
                    dtAvailMi = DateAdd(DateInterval.Minute, intAvailMi, dtpDonDt.Value)   ' 유효기간
                Else
                    dtAvailMi = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Minute, intAvailMi, dtpDonDt.Value))   ' 유효기간
                End If

                Dim sTime As String = Me.dtpDonDt.Text.Substring(11, 5) + ":00"

                If dt.Rows(0).Item("platyn").ToString = "Y" Then '<20130410 ymg 유효기간 삽입
                    Me.lblAvailDt.Text = Format(dtAvailMi, "yyyy-MM-dd").ToString + " " + sTime
                Else
                    Me.lblAvailDt.Text = Format(dtAvailMi, "yyyy-MM-dd").ToString + " 23:59:59"
                End If

                'Me.lblAvailDt.Text = Format(dtAvailMi, "yyyy-MM-dd").ToString + " 23:59:59"

                If Me.cboBType.Text = "" Or Me.cboRH.Text = "" Then
                    fn_PopMsg(Me, "I"c, "혈액형이 잘못 입력 되었습니다.  확인하세요.!!")
                    Me.txtBType.Focus()
                    Return
                End If

                ' 혈액번호 체크
                If Len(Me.txtBldNm.Text.Trim) < 10 Then
                    fn_PopMsg(Me, "I"c, "잘못된 혈액번호 입니다. 다시 확인해 주세요.")
                    Me.txtBldNm.Text = ""
                    Me.txtBldNm.Focus()

                    Return
                End If

                dt = BldIn.fnGet_BldNo_Info(Me.txtBldNm.Text.Trim, Me.lblComCd.Text) ' 혈액번호를 이용하여 입고된 혈액정보 가져와 화면에 뿌려줌

                If dt.Rows.Count > 0 Then
                    '입력 혈액번호
                    fn_PopMsg(Me, "I"c, "입고된 혈액입니다..")
                    Me.txtBldNm.Text = ""
                    Me.txtBldNm.Focus()
                    Return

                Else
                    '신규 혈액번호
                    If Me.txtBldNm.Text.Trim.Substring(0, 2) = COMMON.CommLogin.LOGIN.PRG_CONST.Bank_DonorBldNo Then
                        fn_PopMsg(Me, "I"c, "헌혈 혈액은 개별로 입고하세요.")
                        Me.txtBldNm.Text = ""
                        Me.txtBldNm.Focus()
                        Return
                    End If

                End If

                If Me.cboBldGbn.SelectedIndex >= 2 And (Me.txtRegNo.Text = "" Or Me.lblPatNm.Text = "") Then
                    fn_PopMsg(Me, "I"c, cboBldGbn.Text + "는 등록번호가 필요합니다.")
                    Me.txtRegNo.Focus()
                    Return
                End If

                If fnGet_DateCheck(dtpDonDt.Value, "헌혈일자") Then
                    With spdList
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("chk") : .Text = "1"
                        .Col = .GetColFromID("bldno") : .Text = Me.txtBldNm.Text.Substring(0, 2) + "-" + txtBldNm.Text.Substring(2, 2) + "-" + txtBldNm.Text.Substring(4)
                        .Col = .GetColFromID("comcd") : .Text = Me.lblComCd.Text
                        .Col = .GetColFromID("comnmd") : .Text = Me.lblComNmd.Text
                        .Col = .GetColFromID("donqnt") : .Text = Me.lblDonQnt.Text
                        .Col = .GetColFromID("abo") : .Text = Me.cboBType.Text
                        .Col = .GetColFromID("rh") : .Text = Me.cboRH.Text
                        '.Col = .GetColFromID("dondt") : .Text = Format(Me.dtpDonDt.Value, "yyyy-MM-dd")
                        .Col = .GetColFromID("dondt") : .Text = Format(Me.dtpDonDt.Value, "yyyy-MM-dd HH:mm:00")
                        .Col = .GetColFromID("availdt") : .Text = Me.lblAvailDt.Text
                        .Col = .GetColFromID("dongbn") : .Text = "[" + Me.cboBldGbn.SelectedIndex.ToString + "] " + Me.cboBldGbn.Text
                        .Col = .GetColFromID("regno") : .Text = Me.txtRegNo.Text
                        .Col = .GetColFromID("patnm") : .Text = Me.lblPatNm.Text
                    End With
                End If

                Me.txtRegNo.Text = "" : Me.lblPatNm.Text = ""
                Me.txtBType.Text = "" : Me.cboBType.SelectedIndex = -1 : Me.cboRH.SelectedIndex = -1 : Me.lblBType.Text = ""
                Me.txtBldQnt.Text = "" : Me.lblComNmd.Text = "" : Me.lblDonQnt.Text = ""
                Me.txtBldNm.Text = ""
                Me.txtBType.Focus()
            Else
                Me.lblComCd.Text = ""
                Me.lblComNmd.Text = ""
                Me.lblDonQnt.Text = ""
                Me.lblAvailDt.Text = ""

                fn_PopMsg(Me, "I"c, "혈액코드[" + txtBldQnt.Text + "] 는 존재하지 않습니다.")
                Me.txtBldQnt.Focus()
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try


    End Sub

    Private Sub txtBldNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBldNm.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            ' 혈액번호 체크
            If Me.txtBldNm.Text.Equals("") Then

            ElseIf Len(Me.txtBldNm.Text.Replace("-", "").Trim) < 10 Then
                fn_PopMsg(Me, "I"c, "잘못된 혈액번호 입니다. 다시 확인해 주세요.")
                Me.txtBldNm.Text = ""
                Me.txtBldNm.Focus()

                Return
            End If


            Me.txtBldQnt.Focus()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub DateTimePicker2_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDonDt.CloseUp
        fnGet_DateCheck(dtpDonDt.Value, "헌혈일자")
        txtBType.Focus()

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sFn As String = "Sub Display_Data(string)"

        Try
            m_aI_BldInfo.Clear()

            With spdList
                For iRow As Integer = 1 To .MaxRows
                    Dim stuBldIn As New STU_BldInfo

                    .Row = iRow
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                    .Col = .GetColFromID("bldno") : stuBldIn.Bldno_Full = .Text
                    .Col = .GetColFromID("bldno") : stuBldIn.BldNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("comcd") : stuBldIn.ComCd = .Text
                    .Col = .GetColFromID("abo") : stuBldIn.Abo = .Text
                    .Col = .GetColFromID("rh") : stuBldIn.Rh = .Text
                    .Col = .GetColFromID("availdt") : stuBldIn.AvailDt = .Text
                    '.Col = .GetColFromID("dondt") : stuBldIn.DonDt = .Text + " 00:00:00"
                    .Col = .GetColFromID("dondt") : stuBldIn.DonDt = .Text
                    .Col = .GetColFromID("donqnt") : stuBldIn.DonQnt = IIf(.Text = "320", "1", "0").ToString
                    .Col = .GetColFromID("comnmd") : stuBldIn.ComNmd = .Text
                    .Col = .GetColFromID("regno") : stuBldIn.RegNo = .Text
                    .Col = .GetColFromID("dongbn") : stuBldIn.DonGbn = Ctrl.Get_Code(.Text)
                    .Col = .GetColFromID("cmt") : stuBldIn.Cmt = .Text
                    .Col = .GetColFromID("patnm") : Dim sPatNm As String = .Text

                    stuBldIn.InDt = Format(Me.dtpIndt.Value, "yyyy-MM-dd HH:mm:ss").ToString
                    stuBldIn.InPlace = "0"

                    Dim bFlag As Boolean = True

                    If stuBldIn.BldNo.Length <> 10 Then
                        fn_PopMsg(Me, "I"c, "혈액번호[" + stuBldIn.BldNo + "]는 잘못된 혈액번호 입니다.")
                        bFlag = False
                    End If

                    ' 헌혈일자, 입고일자 확인하기!!
                    If fnGet_DateCheck(CDate(stuBldIn.DonDt), "헌혈일자") = False Or fnGet_DateCheck(dtpIndt.Value, "입고일자") = False Then
                        bFlag = False
                    End If

                    ' 헌혈일자 > 입고일자인 경우는 말도 안돼!!!
                    If CDate(stuBldIn.DonDt) > CDate(dtpIndt.Value) Then
                        fn_PopMsg(Me, "I"c, "헌혈일자가 입고일자보다 크므로 입고 불가능 합니다")
                        bFlag = False
                    End If

                    If stuBldIn.DonGbn = "2" Or stuBldIn.DonGbn = "3" Or stuBldIn.DonGbn = "4" Then
                        If stuBldIn.RegNo = "" Then
                            fn_PopMsg(Me, "I"c, "지정, 성분, 자가 혈액인 경우는 등록번호를 입력해야 합니다.")
                            Exit Sub
                        End If
                        If sPatNm = "" Then
                            sPatNm = BldIn.fnGet_PatName(stuBldIn.RegNo)
                            If sPatNm = "" Then
                                If MsgBox("등록번호가 존재하지 않습니다.  그래도 입고 하시겠습니까?", MsgBoxStyle.OkCancel, Me.Text) = MsgBoxResult.Cancel Then
                                    bFlag = False
                                End If
                            End If
                        End If
                    End If

                    If bFlag And sChk = "1" Then m_aI_BldInfo.Add(stuBldIn)

                Next

                If BldIn.fnExe_BldIn(m_aI_BldInfo) = True Then
                    fn_PopMsg(Me, "I"c, "정상적으로 입고되었습니다")
                    mbSave = True
                    Me.Close()
                Else
                    fn_PopMsg(Me, "I"c, "입고되지 못했습니다. 다시 시도하세요")
                    m_aI_BldInfo.Clear()
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGB05_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)

        End Select
    End Sub

    Private Sub txtBType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBType.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim dt As DataTable = BldIn.fnGet_BldCdToBType(Me.txtBType.Text)

            If dt.Rows.Count < 1 Then
                Me.lblBType.Text = "" : Me.cboBType.Text = "" : Me.cboRH.Text = ""
                Me.txtBType.SelectAll()
                Me.txtBType.Focus()
                Return
            End If

            Me.lblBType.Text = dt.Rows(0).Item("infofld1").ToString : Me.cboBType.Text = dt.Rows(0).Item("infofld1").ToString
            Me.lblBType.Text += dt.Rows(0).Item("infofld2").ToString : Me.cboRH.Text = dt.Rows(0).Item("infofld2").ToString

            If Me.cboRH.Text = "-" Then
                Me.lblBType.BackColor = Color.Red
            Else
                Me.lblBType.BackColor = Color.White
            End If
            Me.lblBType.ForeColor = Fn.GetBldFrColor(Me.cboBType.Text)

            If Me.cboBType.Text = "" Or Me.cboRH.Text = "" Then
                fn_PopMsg(Me, "I"c, "혈액형이 잘못 입력 되었습니다.  확인하세요.!!")
                Me.txtBType.Text = ""
                Me.txtBType.Focus()
            Else
                Me.txtBldNm.Focus()
            End If
        Catch ex As Exception
            Me.txtBType.SelectAll()
            Me.txtBType.Focus()
        End Try

    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Me.txtRegNo.Text = Me.txtRegNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
        Me.lblPatNm.Text = BldIn.fnGet_PatName(Me.txtRegNo.Text)

        If Me.txtRegNo.Text <> "" And Me.lblPatNm.Text <> "" Then
            Me.txtBldNm_KeyDown(Me.txtBldNm, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
        End If

    End Sub

    Private Sub cboBldGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBldGbn.SelectedIndexChanged
        Select Case Me.cboBldGbn.SelectedIndex
            Case 2, 3, 4
                Me.txtRegNo.ReadOnly = False
            Case Else
                Me.txtRegNo.ReadOnly = True
        End Select

    End Sub

    Private Sub dtpDonDt_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDonDt.ValueChanged
        Me.txtBldNm.Focus()
    End Sub

    Private Sub FGB05_S02_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_SpreadDesige.sbInti(spdList)
    End Sub

    Private Sub spdList_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdList.KeyDownEvent
        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        With spdList
            If .ActiveCol <> .GetColFromID("cmt") Then Return

            Dim iRow As Integer = .ActiveRow
            Dim iCol As Integer = .ActiveCol

            .Row = iRow : .Col = iCol : Dim sValue As String = .Text

            If IsNumeric(sValue) = False Then
                MsgBox("수치값만 입력 가능합니다.!!")
                .Row = iRow : .Col = iCol : .Text = ""
                Return
            End If

            If MsgBox("일괄 적용하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "혈액비고") = MsgBoxResult.No Then Return

            Dim iSeq As Integer = Convert.ToInt16(sValue)

            For ix As Integer = iRow + 1 To .MaxRows

                iSeq += 1

                .Row = ix
                .Col = .GetColFromID("cmt") : .Text = iSeq.ToString
            Next

        End With
    End Sub
End Class