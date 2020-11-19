'>>> 수탁 처방입력

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login

Imports LISAPP.APP_DB
Imports LISAPP.APP_O
Imports LISAPP.APP_O.O01

Public Class FGO03
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGO03.vb, Class : O01" & vbTab
    Private Const msBasDate As String = "2010-01-01"

    Private msXML As String = "\XML\SaveAs_FGO03"
    Private moDB As New LISAPP.LISAPP_O_CUST_ORD

    Friend WithEvents btnSaveAs As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lstSaveList As System.Windows.Forms.ListBox
    Friend WithEvents sfdSCd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cboCustCd As System.Windows.Forms.ComboBox
    Friend WithEvents txtJubsuNo As System.Windows.Forms.TextBox
    Friend WithEvents dtpBirthDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoSex0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSex1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents chkForeignYN As System.Windows.Forms.CheckBox
    Friend WithEvents tbcPatInfo As System.Windows.Forms.TabControl
    Friend WithEvents tbpPatInfo0 As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCDoctorNm As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtTel2 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtTel1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCDeptNm As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnSelBCPRT As System.Windows.Forms.Button
    Friend WithEvents lblBarPrinter As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCRegNo As System.Windows.Forms.TextBox
    Friend WithEvents lblDayNo As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cboComGbn As System.Windows.Forms.ComboBox
    Friend WithEvents btnComCdHlp As System.Windows.Forms.Button
    Friend WithEvents lblComCd As System.Windows.Forms.Label
    Friend WithEvents txtComCd As System.Windows.Forms.TextBox
    Friend WithEvents spdComList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents cboTOrdSlip As System.Windows.Forms.ComboBox
    Friend WithEvents btnHRegNo As System.Windows.Forms.Button
    Friend WithEvents btnReg_DC As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents txtZipno As System.Windows.Forms.TextBox

#Region " 폼내부 함수 "

    Private Sub sbDisplay_Age()
        Dim intAGE As Integer = CType(DateDiff(DateInterval.Year, dtpBirthDay.Value, dtpOrdDt.Value), Integer)
        If (dtpBirthDay.Value.Month.ToString("MM") + dtpBirthDay.Value.Day.ToString("dd")) > (dtpOrdDt.Value.Month.ToString("MM") + dtpOrdDt.Value.Day.ToString("dd")) Then
            intAGE -= 1
        End If
        lblAge.Text = intAGE.ToString
        lblDAge.Text = CType(DateDiff(DateInterval.Day, dtpBirthDay.Value, dtpOrdDt.Value), String)

    End Sub

    Private Sub sbDisplay_PatInfo(ByVal rsGbn As String, ByVal r_dt As DataTable)
        Dim sFn As String = "Private Sub sbDisplay_PatInfo()"
        Try

            fnFormClear("")
            If r_dt.Rows.Count < 1 Then Return

            txtCRegNo.Text = r_dt.Rows(0).Item("custregno").ToString
            txtPatNm.Text = r_dt.Rows(0).Item("patnm").ToString
            dtpBirthDay.Value = CDate(r_dt.Rows(0).Item("birth").ToString)
            txtIdnoL.Text = r_dt.Rows(0).Item("idnol").ToString
            txtIdnoR.Text = r_dt.Rows(0).Item("idnor").ToString

            If r_dt.Rows(0).Item("sex").ToString = "M" Then
                rdoSex1.Checked = True
            Else
                rdoSex0.Checked = True
            End If

            txtTel1.Text = r_dt.Rows(0).Item("tel1").ToString
            txtTel2.Text = r_dt.Rows(0).Item("tel2").ToString
            txtZipno.Text = r_dt.Rows(0).Item("zipno1").ToString
            txtAddress.Text = r_dt.Rows(0).Item("address1").ToString

            Dim sBuf() As String = r_dt.Rows(0).Item("remark").ToString.Split("|"c)

            If sBuf.Length >= 3 Then
                txtCDeptNm.Text = sBuf(1)
                txtCDoctorNm.Text = sBuf(2)
            End If

            If r_dt.Rows(0).Item("foreginyn").ToString = "Y" Then chkForeignYN.Checked = True

            If rsGbn.StartsWith("신상") Then Return

            For ix As Integer = 0 To r_dt.Rows.Count - 1
                If r_dt.Rows(ix).Item("slip_gubun").ToString = "B" Then
                    With spdComList
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(ix).Item("tclscd").ToString
                        .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("tnmd").ToString
                        .Col = .GetColFromID("trngbn") : .Text = r_dt.Rows(ix).Item("comgbn").ToString
                        .Col = .GetColFromID("filter") : .Text = r_dt.Rows(ix).Item("filter").ToString
                        .Col = .GetColFromID("qnt") : .Text = "1"
                        .Col = .GetColFromID("comcdo") : .Text = r_dt.Rows(ix).Item("tordcd").ToString
                        .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix).Item("spccd").ToString
                        .Col = .GetColFromID("ordkey") : .Text = r_dt.Rows(ix).Item("tordcd").ToString + r_dt.Rows(ix).Item("spccd").ToString
                        .Col = .GetColFromID("fkocs") : .Text = r_dt.Rows(ix).Item("fkocs").ToString
                    End With
                Else
                    With spdOrderList
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix).Item("tnmd").ToString
                        .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString
                        .Col = .GetColFromID("tclscd") : .Text = r_dt.Rows(ix).Item("tclscd").ToString
                        .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix).Item("spccd").ToString
                        .Col = .GetColFromID("tordcd") : .Text = r_dt.Rows(ix).Item("tordcd").ToString
                        .Col = .GetColFromID("sugacd") : .Text = r_dt.Rows(ix).Item("sugacd").ToString
                        .Col = .GetColFromID("insugbn") : .Text = r_dt.Rows(ix).Item("insugbn").ToString
                        .Col = .GetColFromID("tcdgbn") : .Text = r_dt.Rows(ix).Item("tcdgbn").ToString
                        .Col = .GetColFromID("minspcvol") : .Text = r_dt.Rows(ix).Item("minspcvol").ToString
                        .Col = .GetColFromID("tsectcd") : .Text = r_dt.Rows(ix).Item("tsectcd").ToString
                        .Col = .GetColFromID("tcd") : .Text = r_dt.Rows(ix).Item("tcd").ToString
                        .Col = .GetColFromID("fkocs") : .Text = r_dt.Rows(ix).Item("fkocs").ToString
                    End With
                End If
            Next

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub


    ' 폼 초기설정
    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"
        Dim CommFN As New Fn
        Dim ServerDT As New ServerDateTime

        Try
            Me.Tag = "Load"
            Me.txtJubsuNo.MaxLength = PRG_CONST.Len_RegNo - 5

            ' 서버날짜로 설정
            Me.dtpOrdDt.Value = CDate(ServerDT.GetDate("-"))

            fnFormClear("ALL")

            ' 로그인정보 설정
            Me.lblUserId.Text = USER_INFO.USRID
            Me.lblUserNm.Text = USER_INFO.USRNM

            fnSpreadColHidden(True)

            sbDisplaySaveList()

            If USER_INFO.USRID = "ACK" Then
                btnHRegNo.Visible = True
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub sbDisplay_Cust()
        Dim sFn As String = "Private Sub sbDisplay_Cust()"

        Try
            Dim dt As DataTable = moDB.fnGet_CustList()

            If dt.Rows.Count < 1 Then Return

            cboCustCd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboCustCd.Items.Add(dt.Rows(ix).Item("cust").ToString)
            Next

            If cboCustCd.Items.Count > 0 Then cboCustCd.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub sbDisplay_TOrdSlip()
        Dim sFn As String = "Private Sub sbDisplay_TOrdSlip()"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TOrdSlip()

            If dt.Rows.Count < 1 Then Return

            cboTOrdSlip.Items.Clear()
            cboTOrdSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboTOrdSlip.Items.Add(dt.Rows(ix).Item("tordslipnm").ToString.Trim)
            Next

            If cboTOrdSlip.Items.Count > 0 Then cboTOrdSlip.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub
    ' 화면정리
    Private Sub fnFormClear(ByVal rsFlag As String)
        Dim sFn As String = "Private Sub fnFormClear()"

        Try
            If rsFlag = "ALL" Then
                txtJubsuNo.Text = ""
            End If

            txtCRegNo.Text = ""
            txtPatNm.Text = "" : chkForeignYN.Checked = False
            txtIdnoL.Text = "" : txtIdnoR.Text = ""

            lblSex.Text = IIf(rdoSex0.Checked, "여", "남").ToString
            lblAge.Text = ""
            lblDAge.Text = ""

            txtCDeptNm.Text = ""
            txtCDoctorNm.Text = ""

            txtTel1.Text = ""
            txtTel2.Text = ""

            txtTestCd.Text = ""
            txtSpcCd.Text = ""
            lblSpcNm.Text = ""

            spdOrderList.MaxRows = 0
            spdComList.MaxRows = 0
            txtComCd.Text = ""

            txtPrtBCNo.Text = ""
            lblPrtBCNOcnt.Text = ""
            lblDayNo.Text = DateDiff(DateInterval.Day, CDate(msBasDate), dtpOrdDt.Value).ToString.PadLeft(4, "0"c)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try


    End Sub

    ' 칼럼 Hidden 유무
    Private Sub fnSpreadColHidden(ByVal abFlag As Boolean)
        Dim sFn As String = "Private Sub fnSpreadColHidden(ByVal abFlag As Boolean)"

        Try

            With spdOrderList
                .Col = .GetColFromID("remark") : .ColHidden = abFlag
                .Col = .GetColFromID("spccd") : .ColHidden = abFlag
                .Col = .GetColFromID("sugacd") : .ColHidden = abFlag
                .Col = .GetColFromID("insugbn") : .ColHidden = abFlag
                .Col = .GetColFromID("tsectcd") : .ColHidden = abFlag
                .Col = .GetColFromID("minspcvol") : .ColHidden = abFlag
                .Col = .GetColFromID("tordcd") : .ColHidden = abFlag
                .Col = .GetColFromID("tcdgbn") : .ColHidden = abFlag
                .Col = .GetColFromID("tcd") : .ColHidden = abFlag
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 데이타 유효성 체크
    Private Function fnValidation() As Boolean
        Dim sFn As String = "Private Function fnValidation() As Boolean"

        fnValidation = False
        Try

            If txtJubsuNo.Text.Equals("") Then
                MsgBox("접수번호를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                txtPatNm.Focus()
                Exit Function
            End If

            If txtPatNm.Text.Equals("") Then
                MsgBox("이름을 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                txtPatNm.Focus()
                Exit Function
            End If

            If txtCRegNo.Text.Equals("") Then
                MsgBox("등록번호를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                txtCRegNo.Focus()
                Exit Function
            End If

            ' Only처방 주민등록번호, 진료과, 의뢰의사 필수
            If txtIdnoL.Text.Equals("") Then
                MsgBox("주민등록번호(좌측)를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                txtIdnoL.Focus()
                Exit Function
            End If

            If txtIdnoR.Text.Equals("") Then
                MsgBox("주민등록번호(우측)를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                txtIdnoR.Focus()
                Exit Function
            End If

            If spdOrderList.MaxRows = 0 Then
                MsgBox("검사항목을 선택해 주십시오.", MsgBoxStyle.Information, Me.Text)
                txtTestCd.Focus()
                Exit Function
            End If

            If spdComList.MaxRows > 0 Then
                With spdComList
                    For intRow As Integer = 1 To .MaxRows
                        ' 의뢰량 체크

                        .Row = intRow
                        .Col = .GetColFromID("qnt")
                        If .Text.ToString = "" Or Not IsNumeric(.Text.ToString) Then .Text = "0"

                        Dim intReqQnt As Integer = CInt(.Text.ToString)
                        If intReqQnt < 1 Then
                            MsgBox("의뢰량을 입력해 주십시오", MsgBoxStyle.Information, Me.Text)
                            .Focus()
                            .Row = intRow
                            .Col = .GetColFromID("qnt")
                            .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                            Exit Function
                        End If
                    Next
                End With
            End If

            fnValidation = True

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    ' 주민번호 왼쪽 Validated시 실행함수
    Private Sub sbIdNoLeft()
        Dim sFn As String = "Private Sub fnIdNoLeft()"
        Dim strIDYear As String
        Dim strIDMonth As String
        Dim strIDDay As String
        Dim dtBirthday As Date
        Dim intAGE As Integer

        Try
            ' 기입력의 경우
            If txtIdnoL.Text.Length.Equals(6) Then
                strIDYear = txtIdnoL.Text.Substring(0, 2)
                strIDMonth = txtIdnoL.Text.Substring(2, 2)
                strIDDay = txtIdnoL.Text.Substring(4, 2)

                If IsDate(strIDYear + "-" + strIDMonth + "-" + strIDDay) = False Then
                    MsgBox("주민등록번호를 확인해주세요", MsgBoxStyle.Information, Me.Text)
                    txtIdnoL.Focus()
                    Exit Sub
                Else
                    dtpBirthDay.Value = CDate(strIDYear & "-" & strIDMonth & "-" & strIDDay)
                    dtBirthday = dtpBirthDay.Value
                End If

                intAGE = CType(DateDiff(DateInterval.Year, dtBirthday, dtpOrdDt.Value), Integer)
                If Format(dtBirthday, "MMdd").ToString > Format(dtpOrdDt.Value, "MMdd").ToString Then intAGE -= 1
                lblAge.Text = intAGE.ToString
                lblDAge.Text = CType(DateDiff(DateInterval.Day, dtBirthday, dtpOrdDt.Value), String)

            ElseIf txtIdnoL.Text.Length < 4 Then
                lblAge.Text = txtIdnoL.Text
                lblDAge.Text = CStr(Val(txtIdnoL.Text) * 365)

            Else
                '    MsgBox("나이를 확인해주세요", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try


    End Sub

    ' 주민번호 오른쪽 Validated시 실행함수
    Private Sub sbIdNoRight()
        Dim sFn As String = "Private Sub sbIdNoRight()"
        Dim strIDYear As String
        Dim strIDMonth As String
        Dim strIDDay As String
        Dim dtBirthday As Date
        Dim strSex As String
        Dim intAGE As Integer

        Try
            If txtIdnoL.Text.Length.Equals(6) AndAlso txtIdnoR.Text.Length.Equals(7) Then
                ' 주민번호로 나이계산

                If txtIdnoL.Text.Length.Equals(6) Then
                    strIDYear = txtIdnoL.Text.Substring(0, 2)
                    strIDMonth = txtIdnoL.Text.Substring(2, 2)
                    strIDDay = txtIdnoL.Text.Substring(4, 2)

                    If IsDate(strIDYear & "-" & strIDMonth & "-" & strIDDay) = False Then
                        MsgBox("주민등록번호를 확인해주세요", MsgBoxStyle.Information, Me.Text)
                        txtIdnoL.Focus()

                        Exit Sub
                    Else
                        '< add freety 2006/12/15 : 주민등록번호 체크 루틴 수정
                        Select Case txtIdnoR.Text.Substring(0, 1)
                            Case "1", "2"
                                dtpBirthDay.Value = CDate("19" & strIDYear & "-" & strIDMonth & "-" & strIDDay)

                            Case "3", "4"
                                dtpBirthDay.Value = CDate("20" & strIDYear & "-" & strIDMonth & "-" & strIDDay)

                            Case "5", "6"   '외국인등록번호
                                dtpBirthDay.Value = CDate("19" & strIDYear & "-" & strIDMonth & "-" & strIDDay)

                            Case "7", "8"   '외국인등록번호
                                dtpBirthDay.Value = CDate("20" & strIDYear & "-" & strIDMonth & "-" & strIDDay)

                            Case "9", "0"
                                dtpBirthDay.Value = CDate("18" & strIDYear & "-" & strIDMonth & "-" & strIDDay)

                        End Select
                        '>

                        dtBirthday = dtpBirthDay.Value
                    End If

                    intAGE = CType(DateDiff(DateInterval.Year, dtBirthday, dtpOrdDt.Value), Integer)
                    If (dtBirthday.Month.ToString("MM") & dtBirthday.Day.ToString("dd")) > (dtpOrdDt.Value.Month.ToString("MM") & dtpOrdDt.Value.Day.ToString("dd")) Then
                        intAGE -= 1
                    End If
                    lblAge.Text = intAGE.ToString
                    lblDAge.Text = CType(DateDiff(DateInterval.Day, dtBirthday, dtpOrdDt.Value), String)

                    ' 성별 판정
                    strSex = txtIdnoR.Text.Substring(0, 1)
                    If Val(strSex) Mod 2 = 1 Then
                        lblSex.Text = "남"
                    ElseIf Val(strSex) Mod 2 = 0 Then
                        lblSex.Text = "여"
                    End If

                    Exit Sub
                End If

            End If

            '입력된 숫자가 주민번호가 아닐때 성별 판정
            If txtIdnoR.Text.Trim.Length > 0 Then
                strSex = txtIdnoR.Text.Substring(0, 1)

                If txtIdnoL.Text.Length.Equals(6) Then
                    strIDYear = txtIdnoL.Text.Substring(0, 2)
                    strIDMonth = txtIdnoL.Text.Substring(2, 2)
                    strIDDay = txtIdnoL.Text.Substring(4, 2)

                    If Val(txtIdnoR.Text.Substring(0, 1)) < 3 Then
                        dtpBirthDay.Value = CDate("19" & strIDYear & "-" & strIDMonth & "-" & strIDDay)
                    Else
                        dtpBirthDay.Value = CDate("20" & strIDYear & "-" & strIDMonth & "-" & strIDDay)
                    End If
                    dtBirthday = dtpBirthDay.Value

                    intAGE = CType(DateDiff(DateInterval.Year, dtBirthday, dtpOrdDt.Value), Integer)
                    If (dtBirthday.Month.ToString & dtBirthday.Day.ToString) > (dtpOrdDt.Value.Month.ToString & dtpOrdDt.Value.Day.ToString) Then
                        intAGE -= 1
                    End If
                    lblAge.Text = intAGE.ToString
                    lblDAge.Text = CType(DateDiff(DateInterval.Day, dtBirthday, dtpOrdDt.Value), String)
                End If

                If Val(strSex) Mod 2 = 1 Then
                    lblSex.Text = "남"
                ElseIf Val(strSex) Mod 2 = 0 Then
                    lblSex.Text = "여"
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    Private Sub sbReg_DC()
        Dim sFn As String = "Private Sub sbReg_DC()"

        Dim alOCSOrder As New ArrayList

        Try

            ' 검사항목 수집
            For intRow As Integer = 1 To spdOrderList.MaxRows
                With spdOrderList
                    .Row = intRow
                    .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                    If strChk = "1" Then
                        Dim OCSOrder As DB_MTS_Order.clsMTS0001 = New DB_MTS_Order.clsMTS0001

                        With OCSOrder
                            .SEQ = ""                               ' 순번
                            .IN_OUT_GUBUN = "C"                     ' O:외래, I:입원구분, C:수탁
                            .FKOCS = ""
                            .BUNHO = Ctrl.Get_Code(cboCustCd) + lblDayNo.Text + txtJubsuNo.Text         ' 등록번호
                            .GWA = Ctrl.Get_Code(cboCustCd)             ' 진료과
                            .IPWON_DATE = ""                            ' 입원일자
                            .RESIDENT = ""                              ' 주치의 
                            .DOCTOR = ""                                ' 의사코드
                            .HO_DONG = ""                               ' 병동
                            .HO_CODE = ""                               ' 병실
                            .HO_BED = ""                                ' 병상
                            .ORDER_DATE = Format(dtpOrdDt.Value, "yyyy-MM-dd")  ' 처방일자
                            .ORDER_TIME = ""                ' 처방일시
                            .SLIP_GUBUN = "C"
                            .SURYANG = "1"                  ' 수량(default = 1)
                            .HOPE_DATE = ""                 ' 검사희망일
                            .HOPE_TIME = ""                 ' 검사희망시간
                            .DC_YN = "N"                    ' D/C 여부(default = "N")
                            .APPEND_YN = ""                 ' 추가여부
                            .SUNAB_DATE = .ORDER_DATE       ' 수납(default = "Y")
                            .SOURCE_FKOCS = ""              ' 추가검사인경우 Parent FKOCS
                            .REMARK = txtCRegNo.Text        ' 거래처 등록번호
                            .HEIGHT = ""                    ' 키
                            .WEGHT = ""                     ' 체중
                            .SEND_DATE = ""
                            .RECV_DATE = ""
                            .IUD = "I"                      ' 입력구분
                            .FLAG = ""

                            .OPDT = ""                      ' 수술예정일   
                            .REQ_REMARK = txtCRegNo.Text
                            .LISCMT = txtCDeptNm.Text + "|" + txtCDoctorNm.Text + "|"

                        End With

                        .Col = .GetColFromID("tordcd") : OCSOrder.HANGMOG_CODE = .Text.Trim ' 항목코드
                        .Col = .GetColFromID("spccd") : OCSOrder.SPECIMEN_CODE = .Text.Trim ' 검체코드
                        .Col = .GetColFromID("errflg") : OCSOrder.EMERGENCY = IIf(.Text.Trim = "1", "Y", "").ToString ' 응급구분
                        .Col = .GetColFromID("remark") : OCSOrder.REMARK = .Text.Trim ' 리마크
                        .Col = .GetColFromID("fkocs") : OCSOrder.FKOCS = .Text.Trim ' fkocs

                        alOCSOrder.Add(OCSOrder)
                    End If
                End With
            Next

            '-- 수혈처방 수집
            For intRow As Integer = 1 To spdOrderList.MaxRows
                With spdComList
                    .Row = intRow
                    .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                    If strChk = "1" Then
                        Dim OCSOrder As DB_MTS_Order.clsMTS0001 = New DB_MTS_Order.clsMTS0001

                        With OCSOrder
                            .SEQ = ""                               ' 순번
                            .IN_OUT_GUBUN = "C"                     ' O:외래, I:입원구분, C:수탁
                            .FKOCS = ""
                            .BUNHO = Ctrl.Get_Code(cboCustCd) + lblDayNo.Text + txtJubsuNo.Text         ' 등록번호
                            .GWA = Ctrl.Get_Code(cboCustCd)             ' 진료과
                            .IPWON_DATE = ""                            ' 입원일자
                            .RESIDENT = ""                              ' 주치의 
                            .DOCTOR = ""                                ' 의사코드
                            .HO_DONG = ""                               ' 병동
                            .HO_CODE = ""                               ' 병실
                            .HO_BED = ""                                ' 병상
                            .ORDER_DATE = Format(dtpOrdDt.Value, "yyyy-MM-dd")  ' 처방일자
                            .ORDER_TIME = ""                ' 처방일시
                            .SLIP_GUBUN = "B"
                            .SURYANG = "1"                  ' 수량(default = 1)
                            .HOPE_DATE = ""                 ' 검사희망일
                            .HOPE_TIME = ""                 ' 검사희망시간
                            .DC_YN = "Y"                    ' D/C 여부(default = "N")
                            .APPEND_YN = ""                 ' 추가여부
                            .SUNAB_DATE = .ORDER_DATE       ' 수납(default = "Y")
                            .SOURCE_FKOCS = ""              ' 추가검사인경우 Parent FKOCS
                            .REMARK = txtCRegNo.Text        ' 거래처 등록번호
                            .HEIGHT = ""                    ' 키
                            .WEGHT = ""                     ' 체중
                            .SEND_DATE = ""
                            .RECV_DATE = ""
                            .IUD = "I"                      ' 입력구분
                            .FLAG = ""

                            .OPDT = ""                      ' 수술예정일   
                            .REQ_REMARK = txtCRegNo.Text
                            .LISCMT = txtCDeptNm.Text + "|" + txtCDoctorNm.Text + "|"

                        End With

                        .Col = .GetColFromID("comcdo") : OCSOrder.HANGMOG_CODE = .Text.Trim ' 항목코드
                        .Col = .GetColFromID("spccd") : OCSOrder.SPECIMEN_CODE = .Text.Trim ' 검체코드
                        .Col = .GetColFromID("fkocs") : OCSOrder.FKOCS = .Text.Trim ' fkocs

                        alOCSOrder.Add(OCSOrder)
                    End If
                End With
            Next

            alOCSOrder.TrimToSize()

            ' D/C 처방(Only  처방)
            With (New DB_MTS_Order)
                Dim strErrMsg As String = .ExecuteDo_DC(alOCSOrder, lblUserId.Text)

                If strErrMsg <> "" Then
                    Throw (New Exception(strErrMsg))
                Else
                    fnFormClear("ALL")
                    MsgBox("정상적으로 처리되었습니다.", MsgBoxStyle.Information, Me.Text)
                End If
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    ' 처방 ( Only )
    Private Sub sbReg_OnlyOrder()
        Dim sFn As String = "Private Sub fnReg_New()"

        Dim alOCSOrder As New ArrayList
        Dim PatInfo As New DB_MTS_Order.clsMTS0002
        Dim alDrugList As New ArrayList
        Dim alDiagList As New ArrayList

        Try
            ' 데이타 유효성 체크
            If fnValidation() = False Then Exit Sub

            ' 환자정보 수집 
            fnPatInfo_Collect(PatInfo)

            ' 일반검사항목 수집
            fnTestItem_Collect(alOCSOrder)

            ' 수혈의뢰 내역 수집
            fnTnsItem_Collect(alOCSOrder)

            alOCSOrder.TrimToSize()

            ' 처방(Only  처방)
            With (New DB_MTS_Order)
                Dim strErrMsg As String = .ExecuteDo(alOCSOrder, PatInfo, alDrugList, alDiagList, lblUserId.Text, "Y")

                If strErrMsg <> "" Then
                    Throw (New Exception(strErrMsg))
                Else
                    fnFormClear("ALL")
                    MsgBox("정상적으로 처리되었습니다.", MsgBoxStyle.Information, Me.Text)
                End If
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 환자정보 수집
    Private Sub fnPatInfo_Collect(ByRef aoPatInfo As DB_MTS_Order.clsMTS0002)
        Dim sFn As String = "Private Sub fnPatient_Collect()"

        Try
            ' 환자정보 수집
            With aoPatInfo
                .SEQ = ""                   ' 순번
                .BUNHO = Ctrl.Get_Code(cboCustCd) + lblDayNo.Text + txtJubsuNo.Text      ' 등록번호
                .SUNAME = txtPatNm.Text     ' 성명
                .BIRTH = dtpBirthDay.Text   ' 생년월일
                .SUJUMIN1 = txtIdnoL.Text   ' 주민번호 왼쪽
                .SUJUMIN2 = txtIdnoR.Text   ' 주민번호 오른쪽
                .ZIP_CODE1 = txtZipno.Text.Replace("-", "")  ' 우편번호
                .ZIP_CODE2 = ""
                .ADDRESS1 = txtAddress.Text
                .ADDRESS2 = ""
                .TEL1 = txtTel1.Text        ' 연락처1
                .TEL2 = txtTel2.Text        ' 연락처1
                .SEND_DATE = ""
                .RECV_DATE = ""
                .IUD = "I"                  ' 입력구분
                .FLAG = ""

                .SEX = CType(IIf(lblSex.Text = "남", "M", "F"), String) '성별

                '-- 거래처
                .FOREGINYN = CType(IIf(chkForeignYN.Checked, "Y", ""), String) '-- 외국인여부
                .CUSTCD = Ctrl.Get_Code(cboCustCd)
                .CREGNO = txtCRegNo.Text    '-- 거래처 등록번호
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 일반검사항목 수집( Only 처방 )
    Private Sub fnTestItem_Collect(ByRef alOCSOrder As ArrayList)
        Dim sFn As String = "Private Sub fnTestItem_Collect() "
        Dim OCSOrder As DB_MTS_Order.clsMTS0001

        Try
            ' 검사항목 수집
            For intRow As Integer = 1 To spdOrderList.MaxRows
                With spdOrderList
                    .Row = intRow
                    OCSOrder = New DB_MTS_Order.clsMTS0001

                    With OCSOrder
                        .SEQ = ""                               ' 순번
                        .IN_OUT_GUBUN = "C"                     ' O:외래, I:입원구분, C:수탁
                        .FKOCS = ""
                        .BUNHO = Ctrl.Get_Code(cboCustCd) + lblDayNo.Text + txtJubsuNo.Text         ' 등록번호
                        .GWA = Ctrl.Get_Code(cboCustCd)             ' 진료과
                        .IPWON_DATE = ""                            ' 입원일자
                        .RESIDENT = ""                              ' 주치의 
                        .DOCTOR = ""                                ' 의사코드
                        .HO_DONG = ""                               ' 병동
                        .HO_CODE = ""                               ' 병실
                        .HO_BED = ""                                ' 병상
                        .ORDER_DATE = Format(dtpOrdDt.Value, "yyyy-MM-dd")  ' 처방일자
                        .ORDER_TIME = ""                ' 처방일시
                        .SLIP_GUBUN = "C"
                        .SURYANG = "1"                  ' 수량(default = 1)
                        .HOPE_DATE = ""                 ' 검사희망일
                        .HOPE_TIME = ""                 ' 검사희망시간
                        .DC_YN = "N"                    ' D/C 여부(default = "N")
                        .APPEND_YN = ""                 ' 추가여부
                        .SUNAB_DATE = .ORDER_DATE       ' 수납(default = "Y")
                        .SOURCE_FKOCS = ""              ' 추가검사인경우 Parent FKOCS
                        .REMARK = ""
                        .HEIGHT = ""                    ' 키
                        .WEGHT = ""                     ' 체중
                        .SEND_DATE = ""
                        .RECV_DATE = ""
                        .IUD = "I"                      ' 입력구분
                        .FLAG = ""

                        .OPDT = ""                      ' 수술예정일   
                        .REQ_REMARK = txtCRegNo.Text    ' 거래처 등록번호
                        .REMARK = txtCRegNo.Text + "|" + txtCDeptNm.Text + "|" + txtCDoctorNm.Text + "|"
                    End With

                    .Col = .GetColFromID("tordcd") : OCSOrder.HANGMOG_CODE = .Text.Trim ' 항목코드
                    .Col = .GetColFromID("spccd") : OCSOrder.SPECIMEN_CODE = .Text.Trim ' 검체코드
                    .Col = .GetColFromID("errflg") : OCSOrder.EMERGENCY = IIf(.Text.Trim = "1", "Y", "").ToString ' 응급구분
                    .Col = .GetColFromID("fkocs") : OCSOrder.FKOCS = .Text.Trim ' fkocs

                    alOCSOrder.Add(OCSOrder)
                End With
            Next
            alOCSOrder.TrimToSize()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 수혈의뢰내역 수집
    Private Sub fnTnsItem_Collect(ByRef alOCSOrder As ArrayList)
        Dim sFn As String = "Private Sub fnTestItem_Collect() "
        Dim OCSOrder As DB_MTS_Order.clsMTS0001
        Dim intSURYANG As Integer

        Try
            ' 수혈의뢰내역 수집
            For intRow As Integer = 1 To spdComList.MaxRows
                With spdComList
                    .Row = intRow

                    .Col = .GetColFromID("qnt") : intSURYANG = CInt(.Text.Trim) ' 의뢰량
                    For intCnt As Integer = 1 To intSURYANG
                        OCSOrder = New DB_MTS_Order.clsMTS0001
                        With OCSOrder
                            .SEQ = ""                               ' 순번
                            .IN_OUT_GUBUN = "C"                     ' O:외래, I:입원구분, C:수탁
                            .FKOCS = ""
                            .BUNHO = Ctrl.Get_Code(cboCustCd) + lblDayNo.Text + txtJubsuNo.Text         ' 등록번호
                            .GWA = Ctrl.Get_Code(cboCustCd)             ' 진료과
                            .IPWON_DATE = ""                            ' 입원일자
                            .RESIDENT = ""                              ' 주치의 
                            .DOCTOR = ""                                ' 의사코드
                            .HO_DONG = ""                               ' 병동
                            .HO_CODE = ""                               ' 병실
                            .HO_BED = ""                                ' 병상
                            .ORDER_DATE = Format(dtpOrdDt.Value, "yyyy-MM-dd")  ' 처방일자
                            .ORDER_TIME = ""                ' 처방일시
                            .SLIP_GUBUN = "B"
                            .SURYANG = "1"                  ' 수량(default = 1)
                            .HOPE_DATE = ""                 ' 검사희망일
                            .HOPE_TIME = ""                 ' 검사희망시간
                            .DC_YN = "N"                    ' D/C 여부(default = "N")
                            .APPEND_YN = ""                 ' 추가여부
                            .SUNAB_DATE = .ORDER_DATE       ' 수납(default = "Y")
                            .SOURCE_FKOCS = ""              ' 추가검사인경우 Parent FKOCS
                            .REMARK = ""
                            .HEIGHT = ""                    ' 키
                            .WEGHT = ""                     ' 체중
                            .SEND_DATE = ""
                            .RECV_DATE = ""
                            .IUD = "I"                      ' 입력구분
                            .FLAG = ""

                            .OPDT = ""                      ' 수술예정일   
                            .REQ_REMARK = txtCRegNo.Text    ' 거래처 등록번호
                            .REMARK = txtCRegNo.Text + "|" + txtCDeptNm.Text + "|" + txtCDoctorNm.Text + "|"
                        End With

                        .Col = .GetColFromID("comcdo") : OCSOrder.HANGMOG_CODE = .Text.Trim ' 성분제제 처방코드
                        .Col = .GetColFromID("spccd") : OCSOrder.SPECIMEN_CODE = .Text.Trim ' 성분제제구분(검체코드)

                        alOCSOrder.Add(OCSOrder)
                    Next

                End With
            Next
            alOCSOrder.TrimToSize()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

#End Region

    Private Sub sbDisplay_OrderList()

        Dim sFn As String = "Private Sub sbDisplay_OrderList()"

        Try
            Dim dt As DataTable = moDB.fnGet_OrderList(dtpOrdDt.Text, Ctrl.Get_Code(cboCustCd))

            If dt.Rows.Count < 1 Then Return

            spdList.MaxRows = dt.Rows.Count

            For ix As Integer = 0 To dt.Rows.Count - 1
                With spdList
                    .Row = ix + 1
                    Dim sPatnm As String = dt.Rows(ix).Item("patinfo").ToString.Split("|"c)(0)
                    Dim sIdno As String = dt.Rows(ix).Item("patinfo").ToString.Split("|"c)(6) + "-" + dt.Rows(ix).Item("patinfo").ToString.Split("|"c)(7)

                    .Col = .GetColFromID("custnm") : .Text = dt.Rows(ix).Item("custnm").ToString
                    .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString
                    .Col = .GetColFromID("jubsuno") : .Text = dt.Rows(ix).Item("regno").ToString.Substring(dt.Rows(ix).Item("regno").ToString.Length - 3)
                    .Col = .GetColFromID("patnm") : .Text = sPatnm
                    .Col = .GetColFromID("idno") : .Text = sIdno
                    .Col = .GetColFromID("cregno") : .Text = dt.Rows(ix).Item("cregno").ToString
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                End With
            Next

            'If cboCustCd.Items.Count > 0 Then cboCustCd.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub sbDisplaySaveList()

        lstSaveList.Items.Clear()

        If Dir(Application.StartupPath + msXML, FileAttribute.Directory) = "" Then
            MkDir(Application.StartupPath + msXML & "\")
        End If

        Dim strFile As String = Dir(Application.StartupPath + msXML + "\*.xml")

        Do While strFile <> ""
            lstSaveList.Items.Add(strFile)
            strFile = Dir()
        Loop

    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "
    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        sbFormInitialize()

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
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Friend WithEvents lblSex As System.Windows.Forms.Label
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents dtpOrdDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtIdnoR As System.Windows.Forms.TextBox
    Friend WithEvents txtIdnoL As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents spdOrderList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTestCdHlp As System.Windows.Forms.Button
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents btnSpcCdHlp As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblDAge As System.Windows.Forms.Label
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtPrtBCNo As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents lblPrtBCNOcnt As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents lblTestItem As System.Windows.Forms.Label
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGO03))
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.dtpOrdDt = New System.Windows.Forms.DateTimePicker
        Me.lblDayNo = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtIdnoR = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCRegNo = New System.Windows.Forms.TextBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.chkForeignYN = New System.Windows.Forms.CheckBox
        Me.pnlPatientGbn = New System.Windows.Forms.Panel
        Me.rdoSex0 = New System.Windows.Forms.RadioButton
        Me.rdoSex1 = New System.Windows.Forms.RadioButton
        Me.Label31 = New System.Windows.Forms.Label
        Me.dtpBirthDay = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtJubsuNo = New System.Windows.Forms.TextBox
        Me.cboCustCd = New System.Windows.Forms.ComboBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtPatNm = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtIdnoL = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblSex = New System.Windows.Forms.Label
        Me.lblDAge = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblAge = New System.Windows.Forms.Label
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblUserId = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.spdOrderList = New AxFPSpreadADO.AxfpSpread
        Me.lblTestItem = New System.Windows.Forms.Label
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnReg_DC = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnReg = New CButtonLib.CButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnSelBCPRT = New System.Windows.Forms.Button
        Me.lblBarPrinter = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cboTOrdSlip = New System.Windows.Forms.ComboBox
        Me.lblSpcNm = New System.Windows.Forms.Label
        Me.btnSpcCdHlp = New System.Windows.Forms.Button
        Me.lblSpcCd = New System.Windows.Forms.Label
        Me.txtSpcCd = New System.Windows.Forms.TextBox
        Me.btnTestCdHlp = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblPrtBCNOcnt = New System.Windows.Forms.Label
        Me.txtPrtBCNo = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.btnSaveAs = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.lstSaveList = New System.Windows.Forms.ListBox
        Me.sfdSCd = New System.Windows.Forms.SaveFileDialog
        Me.tbcPatInfo = New System.Windows.Forms.TabControl
        Me.tbpPatInfo0 = New System.Windows.Forms.TabPage
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtZipno = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCDoctorNm = New System.Windows.Forms.TextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtTel2 = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtTel1 = New System.Windows.Forms.TextBox
        Me.txtCDeptNm = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.btnHRegNo = New System.Windows.Forms.Button
        Me.btnQuery = New System.Windows.Forms.Button
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.Label17 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.spdComList = New AxFPSpreadADO.AxfpSpread
        Me.cboComGbn = New System.Windows.Forms.ComboBox
        Me.btnComCdHlp = New System.Windows.Forms.Button
        Me.lblComCd = New System.Windows.Forms.Label
        Me.txtComCd = New System.Windows.Forms.TextBox
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlPatientGbn.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.spdOrderList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.tbcPatInfo.SuspendLayout()
        Me.tbpPatInfo0.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.spdComList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Label14)
        Me.GroupBox7.Controls.Add(Me.dtpOrdDt)
        Me.GroupBox7.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(324, 40)
        Me.GroupBox7.TabIndex = 0
        Me.GroupBox7.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Navy
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(4, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 21)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "접수일자"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpOrdDt
        '
        Me.dtpOrdDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDt.Location = New System.Drawing.Point(92, 12)
        Me.dtpOrdDt.Name = "dtpOrdDt"
        Me.dtpOrdDt.Size = New System.Drawing.Size(92, 21)
        Me.dtpOrdDt.TabIndex = 0
        Me.dtpOrdDt.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'lblDayNo
        '
        Me.lblDayNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDayNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDayNo.Location = New System.Drawing.Point(92, 40)
        Me.lblDayNo.Name = "lblDayNo"
        Me.lblDayNo.Size = New System.Drawing.Size(48, 21)
        Me.lblDayNo.TabIndex = 1
        Me.lblDayNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtIdnoR)
        Me.GroupBox1.Controls.Add(Me.lblDayNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtCRegNo)
        Me.GroupBox1.Controls.Add(Me.Panel5)
        Me.GroupBox1.Controls.Add(Me.pnlPatientGbn)
        Me.GroupBox1.Controls.Add(Me.dtpBirthDay)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtJubsuNo)
        Me.GroupBox1.Controls.Add(Me.cboCustCd)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtPatNm)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label29)
        Me.GroupBox1.Controls.Add(Me.txtIdnoL)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblSex)
        Me.GroupBox1.Controls.Add(Me.lblDAge)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblAge)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(324, 198)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'txtIdnoR
        '
        Me.txtIdnoR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdnoR.Location = New System.Drawing.Point(163, 116)
        Me.txtIdnoR.MaxLength = 7
        Me.txtIdnoR.Name = "txtIdnoR"
        Me.txtIdnoR.Size = New System.Drawing.Size(48, 21)
        Me.txtIdnoR.TabIndex = 8
        Me.txtIdnoR.Text = "1234567"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Navy
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(9, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 21)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "주민등록번호"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCRegNo
        '
        Me.txtCRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCRegNo.ImeMode = System.Windows.Forms.ImeMode.Hangul
        Me.txtCRegNo.Location = New System.Drawing.Point(92, 65)
        Me.txtCRegNo.MaxLength = 10
        Me.txtCRegNo.Name = "txtCRegNo"
        Me.txtCRegNo.Size = New System.Drawing.Size(92, 21)
        Me.txtCRegNo.TabIndex = 4
        Me.txtCRegNo.Text = "지성"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkForeignYN)
        Me.Panel5.Location = New System.Drawing.Point(188, 90)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(92, 21)
        Me.Panel5.TabIndex = 161
        '
        'chkForeignYN
        '
        Me.chkForeignYN.AutoSize = True
        Me.chkForeignYN.Location = New System.Drawing.Point(3, 4)
        Me.chkForeignYN.Name = "chkForeignYN"
        Me.chkForeignYN.Size = New System.Drawing.Size(84, 16)
        Me.chkForeignYN.TabIndex = 6
        Me.chkForeignYN.Text = "외국인여부"
        Me.chkForeignYN.UseVisualStyleBackColor = True
        '
        'pnlPatientGbn
        '
        Me.pnlPatientGbn.BackColor = System.Drawing.Color.Thistle
        Me.pnlPatientGbn.Controls.Add(Me.rdoSex0)
        Me.pnlPatientGbn.Controls.Add(Me.rdoSex1)
        Me.pnlPatientGbn.Controls.Add(Me.Label31)
        Me.pnlPatientGbn.ForeColor = System.Drawing.Color.Indigo
        Me.pnlPatientGbn.Location = New System.Drawing.Point(188, 141)
        Me.pnlPatientGbn.Name = "pnlPatientGbn"
        Me.pnlPatientGbn.Size = New System.Drawing.Size(92, 22)
        Me.pnlPatientGbn.TabIndex = 160
        Me.pnlPatientGbn.TabStop = True
        '
        'rdoSex0
        '
        Me.rdoSex0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSex0.Location = New System.Drawing.Point(52, 1)
        Me.rdoSex0.Name = "rdoSex0"
        Me.rdoSex0.Size = New System.Drawing.Size(35, 18)
        Me.rdoSex0.TabIndex = 11
        Me.rdoSex0.Tag = "F"
        Me.rdoSex0.Text = "여"
        '
        'rdoSex1
        '
        Me.rdoSex1.Checked = True
        Me.rdoSex1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSex1.Location = New System.Drawing.Point(10, 1)
        Me.rdoSex1.Name = "rdoSex1"
        Me.rdoSex1.Size = New System.Drawing.Size(38, 18)
        Me.rdoSex1.TabIndex = 10
        Me.rdoSex1.TabStop = True
        Me.rdoSex1.Tag = "M"
        Me.rdoSex1.Text = "남"
        '
        'Label31
        '
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(92, 22)
        Me.Label31.TabIndex = 2
        '
        'dtpBirthDay
        '
        Me.dtpBirthDay.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpBirthDay.Location = New System.Drawing.Point(92, 141)
        Me.dtpBirthDay.Name = "dtpBirthDay"
        Me.dtpBirthDay.Size = New System.Drawing.Size(92, 21)
        Me.dtpBirthDay.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Navy
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(8, 141)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 21)
        Me.Label8.TabIndex = 158
        Me.Label8.Text = "생년월일"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtJubsuNo
        '
        Me.txtJubsuNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJubsuNo.ImeMode = System.Windows.Forms.ImeMode.Hangul
        Me.txtJubsuNo.Location = New System.Drawing.Point(140, 40)
        Me.txtJubsuNo.MaxLength = 3
        Me.txtJubsuNo.Name = "txtJubsuNo"
        Me.txtJubsuNo.Size = New System.Drawing.Size(48, 21)
        Me.txtJubsuNo.TabIndex = 3
        Me.txtJubsuNo.Text = "지성"
        '
        'cboCustCd
        '
        Me.cboCustCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustCd.FormattingEnabled = True
        Me.cboCustCd.Location = New System.Drawing.Point(92, 16)
        Me.cboCustCd.Name = "cboCustCd"
        Me.cboCustCd.Size = New System.Drawing.Size(222, 20)
        Me.cboCustCd.TabIndex = 2
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Navy
        Me.Label38.ForeColor = System.Drawing.Color.White
        Me.Label38.Location = New System.Drawing.Point(8, 16)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(84, 21)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "거래처"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Navy
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(8, 65)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(84, 22)
        Me.Label15.TabIndex = 3
        Me.Label15.Text = "등록번호(거)"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPatNm
        '
        Me.txtPatNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatNm.ImeMode = System.Windows.Forms.ImeMode.Hangul
        Me.txtPatNm.Location = New System.Drawing.Point(92, 91)
        Me.txtPatNm.MaxLength = 20
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.Size = New System.Drawing.Size(92, 21)
        Me.txtPatNm.TabIndex = 5
        Me.txtPatNm.Text = "지성"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Navy
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(8, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 21)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "성명"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Navy
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(8, 332)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 22)
        Me.Label9.TabIndex = 100
        Me.Label9.Text = "병상"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Navy
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(8, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "접수번호"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label29.Location = New System.Drawing.Point(286, 176)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(17, 12)
        Me.Label29.TabIndex = 17
        Me.Label29.Text = "일"
        '
        'txtIdnoL
        '
        Me.txtIdnoL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdnoL.Location = New System.Drawing.Point(92, 116)
        Me.txtIdnoL.MaxLength = 6
        Me.txtIdnoL.Name = "txtIdnoL"
        Me.txtIdnoL.Size = New System.Drawing.Size(48, 21)
        Me.txtIdnoL.TabIndex = 7
        Me.txtIdnoL.Text = "770405"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(146, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "~"
        '
        'lblSex
        '
        Me.lblSex.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblSex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSex.Location = New System.Drawing.Point(56, 167)
        Me.lblSex.Name = "lblSex"
        Me.lblSex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSex.Size = New System.Drawing.Size(36, 22)
        Me.lblSex.TabIndex = 12
        Me.lblSex.Text = "남"
        Me.lblSex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDAge
        '
        Me.lblDAge.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDAge.Location = New System.Drawing.Point(236, 167)
        Me.lblDAge.Name = "lblDAge"
        Me.lblDAge.Size = New System.Drawing.Size(44, 22)
        Me.lblDAge.TabIndex = 16
        Me.lblDAge.Text = "26"
        Me.lblDAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.SlateGray
        Me.Label23.ForeColor = System.Drawing.Color.White
        Me.Label23.Location = New System.Drawing.Point(188, 167)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(48, 21)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "일령"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.SlateGray
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(98, 167)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 21)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "나이"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.SlateGray
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(8, 167)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 21)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "성별"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAge
        '
        Me.lblAge.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAge.Location = New System.Drawing.Point(146, 167)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(36, 22)
        Me.lblAge.TabIndex = 14
        Me.lblAge.Text = "26"
        Me.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(364, 7)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 20)
        Me.lblUserNm.TabIndex = 155
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(454, 7)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(84, 20)
        Me.lblUserId.TabIndex = 154
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.spdOrderList)
        Me.Panel2.Location = New System.Drawing.Point(334, 80)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(348, 319)
        Me.Panel2.TabIndex = 61
        '
        'spdOrderList
        '
        Me.spdOrderList.DataSource = Nothing
        Me.spdOrderList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdOrderList.Location = New System.Drawing.Point(0, 0)
        Me.spdOrderList.Name = "spdOrderList"
        Me.spdOrderList.OcxState = CType(resources.GetObject("spdOrderList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrderList.Size = New System.Drawing.Size(344, 315)
        Me.spdOrderList.TabIndex = 1
        '
        'lblTestItem
        '
        Me.lblTestItem.BackColor = System.Drawing.Color.Navy
        Me.lblTestItem.ForeColor = System.Drawing.Color.White
        Me.lblTestItem.Location = New System.Drawing.Point(186, 42)
        Me.lblTestItem.Name = "lblTestItem"
        Me.lblTestItem.Size = New System.Drawing.Size(72, 21)
        Me.lblTestItem.TabIndex = 119
        Me.lblTestItem.Text = "검사항목"
        Me.lblTestItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtTestCd.Location = New System.Drawing.Point(258, 42)
        Me.txtTestCd.MaxLength = 5
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(52, 21)
        Me.txtTestCd.TabIndex = 21
        Me.txtTestCd.Text = "LA001"
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnReg_DC)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnReg)
        Me.pnlBottom.Controls.Add(Me.Panel1)
        Me.pnlBottom.Controls.Add(Me.lblUserId)
        Me.pnlBottom.Controls.Add(Me.lblUserNm)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 595)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1016, 34)
        Me.pnlBottom.TabIndex = 7
        '
        'btnReg_DC
        '
        Me.btnReg_DC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_DC.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg_DC.ColorFillBlend = CBlendItems5
        Me.btnReg_DC.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg_DC.Corners.All = CType(6, Short)
        Me.btnReg_DC.Corners.LowerLeft = CType(6, Short)
        Me.btnReg_DC.Corners.LowerRight = CType(6, Short)
        Me.btnReg_DC.Corners.UpperLeft = CType(6, Short)
        Me.btnReg_DC.Corners.UpperRight = CType(6, Short)
        Me.btnReg_DC.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg_DC.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg_DC.FocalPoints.CenterPtX = 0.5!
        Me.btnReg_DC.FocalPoints.CenterPtY = 0.0!
        Me.btnReg_DC.FocalPoints.FocusPtX = 0.0!
        Me.btnReg_DC.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_DC.FocusPtTracker = DesignerRectTracker10
        Me.btnReg_DC.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg_DC.ForeColor = System.Drawing.Color.White
        Me.btnReg_DC.Image = Nothing
        Me.btnReg_DC.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_DC.ImageIndex = 0
        Me.btnReg_DC.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg_DC.Location = New System.Drawing.Point(685, 3)
        Me.btnReg_DC.Name = "btnReg_DC"
        Me.btnReg_DC.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg_DC.SideImage = Nothing
        Me.btnReg_DC.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg_DC.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg_DC.Size = New System.Drawing.Size(107, 25)
        Me.btnReg_DC.TabIndex = 193
        Me.btnReg_DC.Text = "DC 처리"
        Me.btnReg_DC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_DC.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg_DC.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems1
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.4672897!
        Me.btnExit.FocalPoints.CenterPtY = 0.4!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker2
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(902, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(107, 25)
        Me.btnExit.TabIndex = 192
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems2
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4766355!
        Me.btnClear.FocalPoints.CenterPtY = 0.12!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker4
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(794, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 191
        Me.btnClear.Text = "화면정리 (F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems3
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.5!
        Me.btnReg.FocalPoints.CenterPtY = 0.0!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker6
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(576, 3)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(107, 25)
        Me.btnReg.TabIndex = 190
        Me.btnReg.Text = "처  방"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnSelBCPRT)
        Me.Panel1.Controls.Add(Me.lblBarPrinter)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Location = New System.Drawing.Point(3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(284, 24)
        Me.Panel1.TabIndex = 163
        '
        'btnSelBCPRT
        '
        Me.btnSelBCPRT.BackColor = System.Drawing.Color.Lavender
        Me.btnSelBCPRT.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSelBCPRT.ForeColor = System.Drawing.Color.Indigo
        Me.btnSelBCPRT.Location = New System.Drawing.Point(256, 1)
        Me.btnSelBCPRT.Name = "btnSelBCPRT"
        Me.btnSelBCPRT.Size = New System.Drawing.Size(28, 23)
        Me.btnSelBCPRT.TabIndex = 103
        Me.btnSelBCPRT.Text = "..."
        Me.btnSelBCPRT.UseVisualStyleBackColor = False
        '
        'lblBarPrinter
        '
        Me.lblBarPrinter.BackColor = System.Drawing.Color.Lavender
        Me.lblBarPrinter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBarPrinter.ForeColor = System.Drawing.Color.Indigo
        Me.lblBarPrinter.Location = New System.Drawing.Point(72, 1)
        Me.lblBarPrinter.Name = "lblBarPrinter"
        Me.lblBarPrinter.Size = New System.Drawing.Size(184, 23)
        Me.lblBarPrinter.TabIndex = 102
        Me.lblBarPrinter.Text = "AUTO LABELER (외래채혈실)"
        Me.lblBarPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.DarkSlateBlue
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 23)
        Me.Label13.TabIndex = 101
        Me.Label13.Text = "출력프린터"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboTOrdSlip)
        Me.GroupBox2.Controls.Add(Me.lblSpcNm)
        Me.GroupBox2.Controls.Add(Me.btnSpcCdHlp)
        Me.GroupBox2.Controls.Add(Me.lblSpcCd)
        Me.GroupBox2.Controls.Add(Me.txtSpcCd)
        Me.GroupBox2.Controls.Add(Me.btnTestCdHlp)
        Me.GroupBox2.Controls.Add(Me.lblTestItem)
        Me.GroupBox2.Controls.Add(Me.txtTestCd)
        Me.GroupBox2.Location = New System.Drawing.Point(334, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(348, 72)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'cboTOrdSlip
        '
        Me.cboTOrdSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTOrdSlip.Items.AddRange(New Object() {"[1] 혈액준비(Prep)", "[2] 혈액수혈(Tranf)", "[3] 응급수혈(Emer)", "[4] Irradiation"})
        Me.cboTOrdSlip.Location = New System.Drawing.Point(8, 42)
        Me.cboTOrdSlip.MaxDropDownItems = 10
        Me.cboTOrdSlip.Name = "cboTOrdSlip"
        Me.cboTOrdSlip.Size = New System.Drawing.Size(170, 20)
        Me.cboTOrdSlip.TabIndex = 148
        Me.cboTOrdSlip.TabStop = False
        Me.cboTOrdSlip.Tag = "COMGBN_01"
        '
        'lblSpcNm
        '
        Me.lblSpcNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSpcNm.Location = New System.Drawing.Point(162, 16)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(180, 21)
        Me.lblSpcNm.TabIndex = 144
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSpcCdHlp
        '
        Me.btnSpcCdHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSpcCdHlp.Location = New System.Drawing.Point(132, 16)
        Me.btnSpcCdHlp.Name = "btnSpcCdHlp"
        Me.btnSpcCdHlp.Size = New System.Drawing.Size(28, 21)
        Me.btnSpcCdHlp.TabIndex = 20
        Me.btnSpcCdHlp.TabStop = False
        Me.btnSpcCdHlp.Text = "..."
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.Navy
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(8, 16)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(72, 21)
        Me.lblSpcCd.TabIndex = 125
        Me.lblSpcCd.Text = "검체명"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSpcCd
        '
        Me.txtSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd.Location = New System.Drawing.Point(80, 16)
        Me.txtSpcCd.MaxLength = 4
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.Size = New System.Drawing.Size(52, 21)
        Me.txtSpcCd.TabIndex = 19
        '
        'btnTestCdHlp
        '
        Me.btnTestCdHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnTestCdHlp.Location = New System.Drawing.Point(312, 42)
        Me.btnTestCdHlp.Name = "btnTestCdHlp"
        Me.btnTestCdHlp.Size = New System.Drawing.Size(28, 21)
        Me.btnTestCdHlp.TabIndex = 22
        Me.btnTestCdHlp.TabStop = False
        Me.btnTestCdHlp.Text = "..."
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.lblPrtBCNOcnt)
        Me.GroupBox3.Controls.Add(Me.txtPrtBCNo)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Location = New System.Drawing.Point(685, 431)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(324, 156)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        '
        'lblPrtBCNOcnt
        '
        Me.lblPrtBCNOcnt.BackColor = System.Drawing.Color.Wheat
        Me.lblPrtBCNOcnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrtBCNOcnt.Location = New System.Drawing.Point(284, 12)
        Me.lblPrtBCNOcnt.Name = "lblPrtBCNOcnt"
        Me.lblPrtBCNOcnt.Size = New System.Drawing.Size(36, 25)
        Me.lblPrtBCNOcnt.TabIndex = 4
        Me.lblPrtBCNOcnt.Text = "3장"
        Me.lblPrtBCNOcnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPrtBCNo
        '
        Me.txtPrtBCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrtBCNo.Location = New System.Drawing.Point(4, 40)
        Me.txtPrtBCNo.Multiline = True
        Me.txtPrtBCNo.Name = "txtPrtBCNo"
        Me.txtPrtBCNo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPrtBCNo.Size = New System.Drawing.Size(316, 108)
        Me.txtPrtBCNo.TabIndex = 1
        Me.txtPrtBCNo.TabStop = False
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Khaki
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label24.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label24.Location = New System.Drawing.Point(4, 12)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(280, 24)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "최근 바코드 출력내역"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSaveAs
        '
        Me.btnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSaveAs.Location = New System.Drawing.Point(180, 473)
        Me.btnSaveAs.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(74, 23)
        Me.btnSaveAs.TabIndex = 126
        Me.btnSaveAs.Text = "Save As..."
        '
        'btnDelete
        '
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDelete.Location = New System.Drawing.Point(254, 473)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(74, 23)
        Me.btnDelete.TabIndex = 127
        Me.btnDelete.Text = "Delete"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(4, 473)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(170, 23)
        Me.Label12.TabIndex = 128
        Me.Label12.Text = "저장된 검사항목 리스트"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstSaveList
        '
        Me.lstSaveList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstSaveList.ItemHeight = 12
        Me.lstSaveList.Location = New System.Drawing.Point(4, 500)
        Me.lstSaveList.Name = "lstSaveList"
        Me.lstSaveList.Size = New System.Drawing.Size(324, 88)
        Me.lstSaveList.TabIndex = 129
        '
        'tbcPatInfo
        '
        Me.tbcPatInfo.Controls.Add(Me.tbpPatInfo0)
        Me.tbcPatInfo.HotTrack = True
        Me.tbcPatInfo.ItemSize = New System.Drawing.Size(48, 20)
        Me.tbcPatInfo.Location = New System.Drawing.Point(4, 244)
        Me.tbcPatInfo.Name = "tbcPatInfo"
        Me.tbcPatInfo.SelectedIndex = 0
        Me.tbcPatInfo.Size = New System.Drawing.Size(324, 223)
        Me.tbcPatInfo.TabIndex = 130
        Me.tbcPatInfo.TabStop = False
        '
        'tbpPatInfo0
        '
        Me.tbpPatInfo0.BackColor = System.Drawing.SystemColors.Control
        Me.tbpPatInfo0.Controls.Add(Me.txtAddress)
        Me.tbpPatInfo0.Controls.Add(Me.Label10)
        Me.tbpPatInfo0.Controls.Add(Me.txtZipno)
        Me.tbpPatInfo0.Controls.Add(Me.Label7)
        Me.tbpPatInfo0.Controls.Add(Me.txtCDoctorNm)
        Me.tbpPatInfo0.Controls.Add(Me.Label37)
        Me.tbpPatInfo0.Controls.Add(Me.Label28)
        Me.tbpPatInfo0.Controls.Add(Me.txtTel2)
        Me.tbpPatInfo0.Controls.Add(Me.Label27)
        Me.tbpPatInfo0.Controls.Add(Me.txtTel1)
        Me.tbpPatInfo0.Controls.Add(Me.txtCDeptNm)
        Me.tbpPatInfo0.Controls.Add(Me.Label16)
        Me.tbpPatInfo0.Controls.Add(Me.Label11)
        Me.tbpPatInfo0.Location = New System.Drawing.Point(4, 24)
        Me.tbpPatInfo0.Name = "tbpPatInfo0"
        Me.tbpPatInfo0.Size = New System.Drawing.Size(316, 195)
        Me.tbpPatInfo0.TabIndex = 0
        Me.tbpPatInfo0.Text = "일반내용"
        '
        'txtAddress
        '
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Location = New System.Drawing.Point(73, 143)
        Me.txtAddress.MaxLength = 15
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(237, 47)
        Me.txtAddress.TabIndex = 18
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.SlateGray
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(5, 143)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 47)
        Me.Label10.TabIndex = 186
        Me.Label10.Text = "주소"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtZipno
        '
        Me.txtZipno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZipno.Location = New System.Drawing.Point(73, 119)
        Me.txtZipno.MaxLength = 15
        Me.txtZipno.Name = "txtZipno"
        Me.txtZipno.Size = New System.Drawing.Size(84, 21)
        Me.txtZipno.TabIndex = 17
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.SlateGray
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(5, 119)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 21)
        Me.Label7.TabIndex = 184
        Me.Label7.Text = "우편번호"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCDoctorNm
        '
        Me.txtCDoctorNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCDoctorNm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCDoctorNm.Location = New System.Drawing.Point(73, 33)
        Me.txtCDoctorNm.MaxLength = 0
        Me.txtCDoctorNm.Name = "txtCDoctorNm"
        Me.txtCDoctorNm.Size = New System.Drawing.Size(237, 21)
        Me.txtCDoctorNm.TabIndex = 13
        '
        'Label37
        '
        Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label37.Location = New System.Drawing.Point(1, 60)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(312, 2)
        Me.Label37.TabIndex = 180
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.SlateGray
        Me.Label28.ForeColor = System.Drawing.Color.White
        Me.Label28.Location = New System.Drawing.Point(5, 95)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(68, 21)
        Me.Label28.TabIndex = 130
        Me.Label28.Text = "연락처2"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTel2
        '
        Me.txtTel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel2.Location = New System.Drawing.Point(73, 95)
        Me.txtTel2.MaxLength = 15
        Me.txtTel2.Name = "txtTel2"
        Me.txtTel2.Size = New System.Drawing.Size(84, 21)
        Me.txtTel2.TabIndex = 16
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.SlateGray
        Me.Label27.ForeColor = System.Drawing.Color.White
        Me.Label27.Location = New System.Drawing.Point(5, 71)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(68, 21)
        Me.Label27.TabIndex = 128
        Me.Label27.Text = "연락처1"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTel1
        '
        Me.txtTel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel1.Location = New System.Drawing.Point(73, 71)
        Me.txtTel1.MaxLength = 15
        Me.txtTel1.Name = "txtTel1"
        Me.txtTel1.Size = New System.Drawing.Size(84, 21)
        Me.txtTel1.TabIndex = 15
        Me.txtTel1.Text = "031-1234-1234"
        '
        'txtCDeptNm
        '
        Me.txtCDeptNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCDeptNm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCDeptNm.Location = New System.Drawing.Point(73, 8)
        Me.txtCDeptNm.MaxLength = 0
        Me.txtCDeptNm.Name = "txtCDeptNm"
        Me.txtCDeptNm.Size = New System.Drawing.Size(237, 21)
        Me.txtCDeptNm.TabIndex = 12
        Me.txtCDeptNm.Text = "D0001"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.SlateGray
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(5, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(68, 21)
        Me.Label16.TabIndex = 109
        Me.Label16.Text = "진료과"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.SlateGray
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(5, 33)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 21)
        Me.Label11.TabIndex = 102
        Me.Label11.Text = "의뢰의사"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.btnHRegNo)
        Me.GroupBox4.Controls.Add(Me.btnQuery)
        Me.GroupBox4.Controls.Add(Me.spdList)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Location = New System.Drawing.Point(685, 4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(324, 427)
        Me.GroupBox4.TabIndex = 131
        Me.GroupBox4.TabStop = False
        '
        'btnHRegNo
        '
        Me.btnHRegNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHRegNo.Location = New System.Drawing.Point(257, 9)
        Me.btnHRegNo.Name = "btnHRegNo"
        Me.btnHRegNo.Size = New System.Drawing.Size(61, 24)
        Me.btnHRegNo.TabIndex = 22
        Me.btnHRegNo.TabStop = False
        Me.btnHRegNo.Text = "등록번호"
        Me.btnHRegNo.Visible = False
        '
        'btnQuery
        '
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnQuery.Location = New System.Drawing.Point(95, 10)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(61, 24)
        Me.btnQuery.TabIndex = 21
        Me.btnQuery.TabStop = False
        Me.btnQuery.Text = "조회"
        '
        'spdList
        '
        Me.spdList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(3, 36)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(315, 390)
        Me.spdList.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Gray
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(3, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 24)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = "처방내역"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.spdComList)
        Me.GroupBox5.Controls.Add(Me.cboComGbn)
        Me.GroupBox5.Controls.Add(Me.btnComCdHlp)
        Me.GroupBox5.Controls.Add(Me.lblComCd)
        Me.GroupBox5.Controls.Add(Me.txtComCd)
        Me.GroupBox5.Location = New System.Drawing.Point(334, 395)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(345, 193)
        Me.GroupBox5.TabIndex = 132
        Me.GroupBox5.TabStop = False
        '
        'spdComList
        '
        Me.spdComList.DataSource = Nothing
        Me.spdComList.Location = New System.Drawing.Point(6, 36)
        Me.spdComList.Name = "spdComList"
        Me.spdComList.OcxState = CType(resources.GetObject("spdComList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdComList.Size = New System.Drawing.Size(334, 156)
        Me.spdComList.TabIndex = 148
        '
        'cboComGbn
        '
        Me.cboComGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComGbn.Items.AddRange(New Object() {"[1] 혈액준비(Prep)", "[2] 혈액수혈(Tranf)", "[3] 응급수혈(Emer)", "[4] Irradiation"})
        Me.cboComGbn.Location = New System.Drawing.Point(6, 12)
        Me.cboComGbn.MaxDropDownItems = 10
        Me.cboComGbn.Name = "cboComGbn"
        Me.cboComGbn.Size = New System.Drawing.Size(153, 20)
        Me.cboComGbn.TabIndex = 147
        Me.cboComGbn.TabStop = False
        Me.cboComGbn.Tag = "COMGBN_01"
        '
        'btnComCdHlp
        '
        Me.btnComCdHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnComCdHlp.Location = New System.Drawing.Point(312, 12)
        Me.btnComCdHlp.Margin = New System.Windows.Forms.Padding(1)
        Me.btnComCdHlp.Name = "btnComCdHlp"
        Me.btnComCdHlp.Size = New System.Drawing.Size(28, 22)
        Me.btnComCdHlp.TabIndex = 121
        Me.btnComCdHlp.TabStop = False
        Me.btnComCdHlp.Text = "..."
        '
        'lblComCd
        '
        Me.lblComCd.BackColor = System.Drawing.Color.Navy
        Me.lblComCd.ForeColor = System.Drawing.Color.White
        Me.lblComCd.Location = New System.Drawing.Point(163, 12)
        Me.lblComCd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblComCd.Name = "lblComCd"
        Me.lblComCd.Size = New System.Drawing.Size(94, 21)
        Me.lblComCd.TabIndex = 119
        Me.lblComCd.Text = "성분제제코드"
        Me.lblComCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtComCd
        '
        Me.txtComCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComCd.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtComCd.Location = New System.Drawing.Point(259, 12)
        Me.txtComCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtComCd.MaxLength = 5
        Me.txtComCd.Name = "txtComCd"
        Me.txtComCd.Size = New System.Drawing.Size(52, 21)
        Me.txtComCd.TabIndex = 1
        Me.txtComCd.Text = "LA001"
        '
        'FGO03
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 629)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.tbcPatInfo)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lstSaveList)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnSaveAs)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox5)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGO03"
        Me.Text = "수탁처방"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlPatientGbn.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.spdOrderList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.tbcPatInfo.ResumeLayout(False)
        Me.tbpPatInfo0.ResumeLayout(False)
        Me.tbpPatInfo0.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.spdComList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Spread 보기기/숨김 "
    Private Sub Form_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DoubleClick

        If USER_INFO.USRLVL <> "S" Then Exit Sub

#If DEBUG Then
        Static blnChk As Boolean = False

        '-- 컬럼내용모두 보기/감추기
        fnSpreadColHidden(blnChk)
        blnChk = Not blnChk
#End If
    End Sub
#End Region

#Region " 메인버튼 처리 "

    'Function Key정의()
    Private Sub FGO01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F2
                btnReg_Click(Nothing, Nothing)
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.ButtonClick"

        Try
            txtPrtBCNo.Text = ""
            lblPrtBCNOcnt.Text = ""

            txtJubsuNo.Text = txtJubsuNo.Text.PadLeft(3, "0"c)

            sbReg_OnlyOrder()       ' 처방(Only)
            'fnReg_TillCollect()     ' 채혈까지 처리 

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = "Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.ButtonClick"

        Try
            fnFormClear("ALL")

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#End Region


#Region " Control Event 처리 "
    Private Sub FGO01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If CType(Me.Tag, String) = "Load" Then
            txtCRegNo.Focus()

            Me.Tag = ""
        End If
    End Sub

    Private Sub txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatNm.KeyDown, txtCDeptNm.KeyDown, txtCDoctorNm.KeyDown, txtTel1.KeyDown, txtTel2.KeyDown, txtZipno.KeyDown, txtAddress.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True : SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txtSpcCd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSpcCd.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : txtTestCd.Focus()
        End If
    End Sub

    Private Sub dtpBirthDay_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpBirthDay.CloseUp
        If CType(dtpBirthDay.Tag, String) = Format(dtpBirthDay.Value, "yyyy-MM-dd") Then Exit Sub
        txtIdnoL.Text = dtpBirthDay.Text.Substring(2).Replace("-", "")
        sbDisplay_Age()
    End Sub

    Private Sub dtpOrdDt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpOrdDt.GotFocus, dtpBirthDay.GotFocus
        CType(sender, Windows.Forms.DateTimePicker).Tag = Format(dtpOrdDt.Value, "yyyy-MM-dd")
    End Sub

    Private Sub dtpOrdDt_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpOrdDt.DropDown, dtpBirthDay.DragDrop
        CType(sender, Windows.Forms.DateTimePicker).Tag = Format(dtpOrdDt.Value, "yyyy-MM-dd")
    End Sub

    Private Sub dtpOrdDt_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpOrdDt.CloseUp
        cboCustCd.Focus()

        If CType(dtpOrdDt.Tag, String) = Format(dtpOrdDt.Value, "yyyy-MM-dd") Then Exit Sub
        spdList.MaxRows = 0
        fnFormClear("ALL")
    End Sub

    Private Sub txtPatNm_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatNm.Validated
        Dim sFn As String = "Private Sub txtPatNm_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatNm.Validated"

        If txtPatNm.Text.Length < 2 Then Return
        If txtPatNm.Text.Substring(0, 1) <> "?" Then Return

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim strTclsCds As String = ""


            objHelp.FormText = "환자조회"

            objHelp.TableNm = "gpwrm.mts0004"
            objHelp.Where = "custcd = '" + Ctrl.Get_Code(cboCustCd) + "', AND patnm LIKE '" + txtPatNm.Text.Substring(1) + "%'"

            objHelp.GroupBy = ""
            objHelp.OrderBy = "regno"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("regno", "등록번호", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("patnm", "성명", 12, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sex", "성별", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("birth", "생일", 0, , , True)
            objHelp.AddField("idnol||'-'||idnor", "주민번호", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tel1", "연락처1", 0, , , True)
            objHelp.AddField("tel2", "연락처2", 0, , , True)
            objHelp.AddField("zipno", "우편번호", 0, , , True)
            objHelp.AddField("address", "주소", 0, , , True)
            objHelp.AddField("foreginyn", "외국인", 0, , , True)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtPatNm)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtPatNm.Height + 80)

            If aryList.Count > 0 Then
                txtCRegNo.Text = aryList.Item(0).ToString.Split("|"c)(0).Substring(0, 1)
                txtPatNm.Text = aryList.Item(0).ToString.Split("|"c)(1)
                If aryList.Item(0).ToString.Split("|"c)(2) = "M" Then
                    rdoSex1.Checked = True
                Else
                    rdoSex0.Checked = True
                End If

                dtpBirthDay.Value = CDate(aryList.Item(0).ToString.Split("|"c)(3))
                txtIdnoL.Text = aryList.Item(0).ToString.Split("|"c)(4).Split("-"c)(0) : sbIdNoLeft()
                txtIdnoR.Text = aryList.Item(0).ToString.Split("|"c)(4).Split("-"c)(1) : sbIdNoRight()
                txtTel1.Text = aryList.Item(0).ToString.Split("|"c)(5)
                txtTel2.Text = aryList.Item(0).ToString.Split("|"c)(6)
                txtZipno.Text = aryList.Item(0).ToString.Split("|"c)(7)
                txtAddress.Text = aryList.Item(0).ToString.Split("|"c)(8)
                If aryList.Item(0).ToString.Split("|"c)(9) = "Y" Then chkForeignYN.Checked = True

                txtCDeptNm.Focus()
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtSpcCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSpcCd.Validated
        Dim sFn As String = "Private Sub txtSpcCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSpcCd.Validated"

        If txtSpcCd.Text = "" Then Return

        Try
            btnSpcCdHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtTestCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestCd.Validated
        Dim sFn As String = "Private Sub txtTestCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestCd.Validated"

        If txtTestCd.Text = "" Then Return

        Try
            btnTestCdHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try


    End Sub

    Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdOrderList.DblClick
        Dim sFn As String = "Private Sub spdOrderList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdOrderList.DblClick, spdComList.DblClick, spdDrugList.DblClick"
        Dim objSpd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

        Try
            If e.row < 1 Then Exit Sub

            If MsgBox("해당항목을 리스트에서 삭제 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                With objSpd
                    .DeleteRows(e.row, 1) : .MaxRows -= 1
                End With
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

#End Region

#Region " CodeHelp버튼 처리"
    Private Sub btnTestCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestCdHlp.Click
        Dim sFn As String = "Private Sub btnTestCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestCdHlp.Click"
        Dim CommFn As New Fn

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim strTclsCds As String = ""

            With spdOrderList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("tcd") : strTclsCds += .Text + "|"
                Next
            End With

            objHelp.FormText = "검사항목 코드"

            objHelp.TableNm = "LF060M a, " + _
                              "(select SPCCD, SPCNM, SPCNMD" + _
                              "   from LF030M" + _
                              "  where USDT <= to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-MM-dd')" + _
                              "    and UEDT >  to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-MM-dd')) b"
            objHelp.Where = "NVL(a.exlabyn, '0') = '0' AND a.tcdgbn IN ('G', 'B', 'S', 'P') AND a.ordhide = '0' and a.SPCCD = b.SPCCD and " + _
                            "a.USDT <= to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-MM-dd') and " + _
                            "a.UEDT >  to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-MM-dd')" + _
                            IIf(txtSpcCd.Text <> "", " and a.SPCCD = '" + txtSpcCd.Text + "'", "").ToString + _
                            IIf(txtTestCd.Text <> "", " and a.TCLSCD = '" + txtTestCd.Text + "'", "").ToString + _
                            IIf(Ctrl.Get_Code(cboTOrdSlip).Trim <> "", " and a.tordslip = '" + Ctrl.Get_Code(cboTOrdSlip) + "'", "").ToString

            objHelp.GroupBy = ""
            objHelp.OrderBy = "TCLSCD, TNMD"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.KeyCodes = strTclsCds
            objHelp.OnRowReturnYN = True

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("a.TNMD", "검사명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("b.SPCNMD", "검체명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("a.TCLSCD", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("a.SPCCD", "검체코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("a.SUGACD", "수가코드", 1, , , True)
            objHelp.AddField("a.INSUGBN", "보험구분", , , , True)
            objHelp.AddField("a.SECTCD||A.TSECTCD TSECT", "계코드", , , , True, "TSECT")
            objHelp.AddField("a.MINSPCVOL", "최소채혈량", , , , True)
            objHelp.AddField("a.TORDCD", "처방코드", , , , True)
            objHelp.AddField("a.TCDGBN", "구분", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("a.TCLSCD||a.SPCCD TCD", "검사_검체", , , , True, "TCD", "Y")
            objHelp.AddField("nvl(a.SORT2, 999) SORTL", "순서", , , , True)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtTestCd)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtTestCd.Height + 80)

            If aryList.Count > 0 Then
                Dim arlTClsCd As New ArrayList

                For intidx As Integer = 0 To aryList.Count - 1
                    Dim strTclsCd As String = aryList.Item(intidx).ToString.Split("|"c)(2)
                    Dim strSpcCd As String = aryList.Item(intidx).ToString.Split("|"c)(3)
                    Dim strTnmd As String = aryList.Item(intidx).ToString.Split("|"c)(0)
                    Dim strSpcNmd As String = aryList.Item(intidx).ToString.Split("|"c)(1)
                    Dim strSugaCd As String = aryList.Item(intidx).ToString.Split("|"c)(4)
                    Dim strInsuGbn As String = aryList.Item(intidx).ToString.Split("|"c)(5)
                    Dim strTSectCd As String = aryList.Item(intidx).ToString.Split("|"c)(6)
                    Dim strMinSpcVol As String = aryList.Item(intidx).ToString.Split("|"c)(7)
                    Dim strTordCd As String = aryList.Item(intidx).ToString.Split("|"c)(8)
                    Dim strTcdGbn As String = aryList.Item(intidx).ToString.Split("|"c)(9)
                    Dim strTCd As String = aryList.Item(intidx).ToString.Split("|"c)(10)

                    ' 검사항목 선택 유/무 체크
                    If CommFn.SpdColSearch(spdOrderList, strTCd, spdOrderList.GetColFromID("tcd")) = 0 Then

                        With spdOrderList
                            .MaxRows += 1
                            .Row = .MaxRows
                            .Col = .GetColFromID("tnmd") : .Text = strTnmd
                            .Col = .GetColFromID("spcnmd") : .Text = strSpcNmd
                            .Col = .GetColFromID("tclscd") : .Text = strTclsCd
                            .Col = .GetColFromID("spccd") : .Text = strSpcCd
                            .Col = .GetColFromID("tordcd") : .Text = strTordCd
                            .Col = .GetColFromID("sugacd") : .Text = strSugaCd
                            .Col = .GetColFromID("insugbn") : .Text = strInsuGbn
                            .Col = .GetColFromID("tcdgbn") : .Text = strTcdGbn
                            .Col = .GetColFromID("minspcvol") : .Text = strMinSpcVol
                            .Col = .GetColFromID("tsectcd") : .Text = strTSectCd
                            .Col = .GetColFromID("tcd") : .Text = strTCd
                            .Col = .GetColFromID("fkocs") : .Text = ""
                        End With
                    Else
                        MsgBox("이미 추가된 항목 입니다.", MsgBoxStyle.Information, Me.Text)
                    End If
                Next
            End If
            txtTestCd.Text = ""
            txtTestCd.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnSpcCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpcCdHlp.Click
        Dim sFn As String = "Private Sub btnSpcCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpcCdHlp.Click"

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim strTclsCds As String = ""

            objHelp.FormText = "검체코드"
            objHelp.TableNm = "LF030M"
            objHelp.Where = "USDT <= to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-MM-dd') and UEDT > to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-MM-dd')" + _
                            IIf(txtSpcCd.Text <> "", " and SPCCD = '" + txtSpcCd.Text + "'", "").ToString

            objHelp.GroupBy = ""
            objHelp.OrderBy = "SPCNMD"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("SPCCD", "검체코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("SPCNMD", "검체명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtSpcCd)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtSpcCd.Height + 80)

            If aryList.Count > 0 Then
                txtSpcCd.Text = aryList.Item(0).ToString.Split("|"c)(0)
                lblSpcNm.Text = aryList.Item(0).ToString.Split("|"c)(1)

                txtTestCd.Focus()
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

#End Region

    Private Sub btnSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAs.Click

        If Dir(Application.StartupPath + msXML, FileAttribute.Directory) = "" Then
            MkDir(Application.StartupPath + msXML & "\")
        End If

        sfdSCd.Filter = "xml files (*.xml)|*.xml"
        sfdSCd.InitialDirectory = Application.StartupPath & msXML & "\"

        If sfdSCd.ShowDialog() = DialogResult.OK Then

            Dim xmlWriter As Xml.XmlTextWriter = Nothing

            xmlWriter = New Xml.XmlTextWriter(sfdSCd.FileName, Nothing)
            xmlWriter.Formatting = Xml.Formatting.Indented
            xmlWriter.Indentation = spdOrderList.MaxCols '4
            xmlWriter.IndentChar = Chr(32)
            xmlWriter.WriteStartDocument(False)
            xmlWriter.WriteComment(" 선택된 검사분류 코드 ")
            xmlWriter.WriteStartElement("ROOT")

            With spdOrderList
                For intRow As Integer = 1 To .MaxRows
                    xmlWriter.WriteStartElement("TCLS")
                    For intCol As Integer = 1 To .MaxCols
                        .Row = intRow
                        .Col = intCol
                        xmlWriter.WriteElementString(.ColID, .Text)
                    Next
                    xmlWriter.WriteEndElement()
                Next
            End With

            xmlWriter.WriteEndElement()
            xmlWriter.Close()
        End If

        sbDisplaySaveList()
    End Sub

    Private Sub lstSaveList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSaveList.SelectedIndexChanged
        With lstSaveList

            'spdOrderList.MaxRows = 0

            Dim strDir As String
            strDir = Application.StartupPath + msXML + "\" + .Items(.SelectedIndex).ToString

            If Dir(strDir) > "" Then
                Dim xmlReader As Xml.XmlTextReader

                xmlReader = New Xml.XmlTextReader(strDir)
                While xmlReader.Read
                    xmlReader.ReadStartElement("ROOT")

                    Do While (True)
                        xmlReader.ReadStartElement("TCLS")

                        With spdOrderList
                            .MaxRows += 1
                            For intCol As Integer = 1 To .MaxCols
                                .Row = .MaxRows
                                .Col = intCol

                                Dim strColId As String = ""
                                Dim strTmp As String = ""
                                strColId = .ColID

                                Try
                                    strTmp = xmlReader.ReadElementString(strColId)
                                Catch ex As Exception

                                    Select Case .ColID
                                        Case "tclscd" : strColId = "검사코드"
                                        Case "tnmd" : strColId = "검사명"
                                        Case "spcnmd" : strColId = "검체명"
                                        Case "spccd" : strColId = "검체코드"
                                        Case "errflg" : strColId = "응급"
                                        Case "sugacd" : strColId = "수가코드"
                                        Case "remark" : strColId = "Remark"
                                        Case "insugbn" : strColId = "보험구분"
                                        Case "tsectcd" : strColId = "계"
                                        Case "minspcvol" : strColId = "최소채혈량"
                                        Case "tordcd" : strColId = "처방코드"
                                        Case "tcdgbn" : strColId = "검사코드구분"
                                        Case "tcd" : strColId = "검사_검체코드"
                                        Case Else
                                            strColId = .ColID
                                    End Select

                                    strTmp = xmlReader.ReadElementString(strColId)

                                End Try

                                .Text = strTmp
                            Next
                        End With

                        xmlReader.ReadEndElement()
                        xmlReader.Read()
                        If xmlReader.Name <> "TCLS" Then
                            Exit Do
                        End If
                    Loop
                    xmlReader.Close()
                End While
            End If
        End With

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        With lstSaveList
            If .SelectedIndex > -1 Then
                Dim strDir As String
                strDir = Application.StartupPath + msXML + "\" + .Items(.SelectedIndex).ToString
                Kill(strDir)

                sbDisplaySaveList()
            Else
                MsgBox("삭제할 리스트를 선택하여 주세요.")
            End If
        End With
    End Sub

    Private Sub dtpOrdDt_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpOrdDt.LostFocus
        If CType(dtpOrdDt.Tag, String) = Format(dtpOrdDt.Value, "yyyy-MM-dd") Then Exit Sub
        spdList.MaxRows = 0
        fnFormClear("ALL")
    End Sub

    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJubsuNo.GotFocus, txtCRegNo.GotFocus, txtPatNm.GotFocus, txtIdnoL.GotFocus, txtIdnoR.GotFocus, txtCDeptNm.GotFocus, txtCDoctorNm.GotFocus, txtTel1.GotFocus, txtTel2.GotFocus, txtAddress.GotFocus
        CType(sender, Windows.Forms.TextBox).SelectAll()
    End Sub

    Private Sub txtJubsuNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtJubsuNo.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        txtCRegNo.Focus()

    End Sub

    Private Sub txtCRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCRegNo.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        Dim dt As DataTable = moDB.fnGet_PatInfo(Ctrl.Get_Code(cboCustCd), txtCRegNo.Text)

        sbDisplay_PatInfo("신상", dt)

        SendKeys.Send("{TAB}")
    End Sub

    Private Sub dtpBirthDay_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpBirthDay.LostFocus
        If CType(dtpBirthDay.Tag, String) = Format(dtpBirthDay.Value, "yyyy-MM-dd") Then Exit Sub
        txtIdnoL.Text = dtpBirthDay.Text.Substring(2).Replace("-", "")
        sbDisplay_Age()
    End Sub

    Private Sub txtIdnoL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtIdnoL.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        sbIdNoLeft()
        e.Handled = True : SendKeys.Send("{TAB}")

    End Sub

    Private Sub txtIdnoR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtIdnoR.KeyDown
        Dim sFn As String = "Private Sub txtIdnoR_KeyDown() Handles txtIdnoR.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim strTclsCds As String = ""


            objHelp.FormText = "환자조회"

            objHelp.TableNm = "gpwrm.vw_mts0002"
            objHelp.Where = "sujumin1 = '" + txtIdnoL.Text + "'" + _
                            IIf(txtIdnoL.Text <> "", " AND sujumin2 LIKE '" + txtIdnoR.Text + "%'", "").ToString

            objHelp.GroupBy = ""
            objHelp.OrderBy = "bunho"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("bunho", "등록번호", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("suname", "성명", 12, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sex", "성별", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("birth", "생일", 0, , , True)
            objHelp.AddField("sujumin1||'-'||sujumin2 idno", "주민번호", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tel1", "연락처1", 0, , , True)
            objHelp.AddField("tel2", "연락처2", 0, , , True)
            objHelp.AddField("zip_code1", "우편번호", 0, , , True)
            objHelp.AddField("address1", "주소", 0, , , True)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtPatNm)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtPatNm.Height + 80)

            If aryList.Count > 0 Then
                txtPatNm.Text = aryList.Item(0).ToString.Split("|"c)(1)
                If aryList.Item(0).ToString.Split("|"c)(2) = "M" Then
                    rdoSex1.Checked = True
                Else
                    rdoSex0.Checked = True
                End If

                dtpBirthDay.Value = CDate(aryList.Item(0).ToString.Split("|"c)(3))
                txtIdnoL.Text = aryList.Item(0).ToString.Split("|"c)(4).Split("-"c)(0) : sbIdNoLeft()
                txtIdnoR.Text = aryList.Item(0).ToString.Split("|"c)(4).Split("-"c)(1) : sbIdNoRight()
                txtTel1.Text = aryList.Item(0).ToString.Split("|"c)(5)
                txtTel2.Text = aryList.Item(0).ToString.Split("|"c)(6)
                txtZipno.Text = aryList.Item(0).ToString.Split("|"c)(7)
                txtAddress.Text = aryList.Item(0).ToString.Split("|"c)(8)
            End If

            e.Handled = True : SendKeys.Send("{TAB}")

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

        sbIdNoRight()

    End Sub

    Private Sub rdoSex0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSex0.CheckedChanged
        If rdoSex0.Checked Then lblSex.Text = "여"
    End Sub

    Private Sub rdoSex1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSex1.CheckedChanged
        If rdoSex1.Checked Then lblSex.Text = "남"
    End Sub

    Private Sub FGO03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DS_FormDesige.sbInti(Me)

        sbDisplay_Cust()
        sbDisplay_TOrdSlip()

    End Sub

    Private Sub spdList_DblClick1(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick

        If e.row < 1 Then Return
        Dim sRegNo As String = ""
        Dim sOrdDt As String = ""

        With spdList
            .Row = e.row
            .Col = .GetColFromID("regno") : sRegNo = .Text
            .Col = .GetColFromID("orddt") : sOrdDt = .Text

        End With

        If Ctrl.Get_Code(cboCustCd) <> sRegNo.Substring(0, 1) Then
            For ix As Integer = 0 To cboCustCd.Items.Count - 1
                cboCustCd.SelectedIndex = ix
                If Ctrl.Get_Code(cboCustCd) = sRegNo.Substring(0, 1) Then Exit For
            Next
        End If

        Dim dt As DataTable = moDB.fnGet_JubsuInfo(sOrdDt, sRegNo)

        If dt.Rows.Count > 0 Then
            dtpOrdDt.Value = CDate(sOrdDt)
            For ix As Integer = 0 To cboCustCd.Items.Count - 1
                cboCustCd.SelectedIndex = ix
                If cboCustCd.Text.StartsWith("[" + sRegNo.Substring(0, 1) + "]") Then
                    Exit For
                End If
            Next
            lblDayNo.Text = sRegNo.Substring(1, 4)
            txtJubsuNo.Text = sRegNo.Substring(5)
        End If

        sbDisplay_PatInfo("처방", dt)

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        spdList.MaxRows = 0
        sbDisplay_OrderList()

    End Sub


    Private Sub btnReg_DC_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_DC.Click
        sbReg_DC()

    End Sub

    Private Sub btnComCdHlp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComCdHlp.Click
        Dim sFn As String = "Private Sub btnComCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCdHlp.Click"

        Try
            Dim strSpcGbn As String = ""

            If cboComGbn.SelectedIndex < 0 Then cboComGbn.SelectedIndex = 0
            strSpcGbn = cboComGbn.Text.Substring(1, cboComGbn.Text.IndexOf("]") - 1)

            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim strComCds As String = ""

            With spdComList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("ordkey") : strComCds += .Text + "|"
                Next
            End With

            objHelp.FormText = "성분제제"

            objHelp.TableNm = "LF120M a, LF030M b"
            objHelp.Where = "a.SPCCD = b.SPCCD and a.COMGBN = '" + strSpcGbn + "' and " + _
                            "a.USDT <= to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-mm-dd') and " + _
                            "a.UEDT > to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-mm-dd') and " + _
                            "b.USDT <= to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-mm-dd') and " + _
                            "b.UEDT > to_date('" + Format(dtpOrdDt.Value, "yyyy-MM-dd").ToString + "', 'yyyy-mm-dd')" + _
                            IIf(txtComCd.Text <> "", " and a.COMCD = '" + txtComCd.Text + "'", "").ToString

            objHelp.GroupBy = ""
            objHelp.OrderBy = "a.SORTL, a.DONQNT, a.COMCDO"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.KeyCodes = strComCds
            objHelp.OnRowReturnYN = True

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("a.COMCD", "성분제제코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("a.COMNMD", "성분제제명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("decode(a.COMGBN, '1', 'Prep.', '2', 'Tran.', '3', 'Emer.', '4', 'Irra.') TRNGBN", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , "TRNGBN")
            objHelp.AddField("decode(nvl(TRIM(a.FTCD), '000'), '000', '', '○') FILTER", "필터", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , "FILTER")
            objHelp.AddField("a.COMCDO", "처방코드", , , , True)
            objHelp.AddField("a.SPCCD", "검체코드", , , , True)
            objHelp.AddField("a.COMCDO||a.SPCCD ORDKEY", "ORDKEY", , , , True, "ORDKEY", "Y")
            objHelp.AddField("a.SORTL", "sortl", , , , True)
            objHelp.AddField("a.DONQNT", "donqnt", , , , True)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtComCd)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtComCd.Height + 80)

            If aryList.Count > 0 Then
                Dim arlTClsCd As New ArrayList

                For intidx As Integer = 0 To aryList.Count - 1
                    Dim strComCd As String = aryList.Item(intidx).ToString.Split("|"c)(0)
                    Dim strComNmd As String = aryList.Item(intidx).ToString.Split("|"c)(1)
                    Dim strTrnGbn As String = aryList.Item(intidx).ToString.Split("|"c)(2)
                    Dim strFilter As String = aryList.Item(intidx).ToString.Split("|"c)(3)
                    Dim strComCdo As String = aryList.Item(intidx).ToString.Split("|"c)(4)
                    Dim strSpcCd As String = aryList.Item(intidx).ToString.Split("|"c)(5)
                    Dim strOrdKey As String = aryList.Item(intidx).ToString.Split("|"c)(6)

                    ' 검사항목 선택 유/무 체크
                    If Fn.SpdColSearch(spdComList, strOrdKey, spdOrderList.GetColFromID("ordkey")) = 0 Then

                        With spdComList
                            .MaxRows += 1
                            .Row = .MaxRows
                            .Col = .GetColFromID("comcd") : .Text = strComCd
                            .Col = .GetColFromID("comnmd") : .Text = strComNmd
                            .Col = .GetColFromID("trngbn") : .Text = strTrnGbn
                            .Col = .GetColFromID("filter") : .Text = strFilter
                            .Col = .GetColFromID("comcdo") : .Text = strComCdo
                            .Col = .GetColFromID("spccd") : .Text = strSpcCd
                            .Col = .GetColFromID("ordkey") : .Text = strOrdKey
                        End With
                    Else
                        MsgBox("이미 추가된 항목 입니다.", MsgBoxStyle.Information, Me.Text)
                    End If
                Next
            End If
            txtComCd.Text = ""
            txtComCd.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub txtJubsuNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtJubsuNo.Validating

        txtJubsuNo.Text = txtJubsuNo.Text.PadLeft(3, "0"c)

        Dim sRegNo As String = Ctrl.Get_Code(cboCustCd) + lblDayNo.Text + txtJubsuNo.Text
        Dim dt As DataTable = moDB.fnGet_JubsuInfo(dtpOrdDt.Text, sRegNo)

        If dt.Rows.Count > 0 Then
            If MsgBox("등록된 접수 번호 입니다.!!" + vbCrLf + "새로운 번호로 등록 하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                fnFormClear("")
                txtJubsuNo.Text = ""
                txtJubsuNo.Focus()
                Return
            End If
        End If

        sbDisplay_PatInfo("처방", dt)

    End Sub

    Private Sub btnHRegNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHRegNo.Click

        With spdList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("regno") : Dim sRegNo As String = .Text
                .Col = .GetColFromID("cregno") : Dim sHRegNo As String = .Text

                If sRegNo <> "" Then
                    Dim sRet As Integer = LISAPP.LISAP_O_CUST.fnExe_Change_HRegNo(sRegNo, sHRegNo)

                    If sRet = 0 Then
                        .Row = ix : .Col = .GetColFromID("regno") : .BackColor = Color.Red
                    End If
                End If
            Next

        End With
    End Sub

    Private Sub FGO03_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

End Class