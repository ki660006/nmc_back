'>>> 혈액형 2차 결과 입력

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.APP_BT
Imports LISAPP.APP_BT.CGDA_BT

Public Class FGB04
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGB04.vb, Class : B01" & vbTab

    Private Const msXMLDir As String = "\XML"
    Private msWkGrpFile As String = Application.StartupPath & msXMLDir & "\FGB04_WKGRP.XML"
    Private msTgrpFile As String = Application.StartupPath & msXMLDir & "\FGB04_TGRP.XML"
    Private msSlipFile As String = Application.StartupPath & msXMLDir & "\FGB04_SLIP.XML"
    Private msQryFile As String = Application.StartupPath & msXMLDir & "\FGB04_Qry.XML"

    Private msABOCode As String = ""
    Private msRhCode As String = ""

#Region " Form내부 함수 "
    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검체번호" : .WIDTH = "140" : .FIELD = "bcno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "작업번호" : .WIDTH = "120" : .FIELD = "workno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "등록번호" : .WIDTH = "80" : .FIELD = "regno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "성명" : .WIDTH = "80" : .FIELD = "patnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "성별/나이" : .WIDTH = "70" : .FIELD = "sexage"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "의뢰의사" : .WIDTH = "60" : .FIELD = "doctornm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "진료과/병동" : .WIDTH = "120" : .FIELD = "dept"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "검체명" : .WIDTH = "80" : .FIELD = "spcnmd"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "ABO(1차)" : .WIDTH = "60" : .FIELD = msABOCode
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "Rh(1차)" : .WIDTH = "60" : .FIELD = msRhCode
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "검사자(1차)" : .WIDTH = "80" : .FIELD = "fnnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "결과일시(1차)" : .WIDTH = "120" : .FIELD = "fndt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "ABO(2차)" : .WIDTH = "60" : .FIELD = "abo"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "Rh(2차)" : .WIDTH = "60" : .FIELD = "rh"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "검사자(2차)" : .WIDTH = "80" : .FIELD = "rstnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "결과일시(2차)" : .WIDTH = "120" : .FIELD = "rstdt"
        End With
        alItems.Add(stu_item)


        Return alItems

    End Function

    Private Sub sbReg()
        Try
            With spdList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                    Dim arlRst As New ArrayList

                    If strChk = "1" Then
                        Dim sTestCd As String = msABOCode
                        .Row = ix : .Col = .GetColFromID("bcno") : Dim sBcno As String = .Text.Replace("-", "")
                        .Row = ix : .Col = .GetColFromID("abo") : Dim sRst As String = .Text

                        If sRst <> "" Then
                            arlRst.Add(sBcno + "|" + sTestCd + "|" + sRst + "|")
                        End If

                        sTestCd = msRhCode
                        .Col = .GetColFromID("rh") : sRst = .Text

                        If sRst <> "" Then
                            arlRst.Add(sBcno + "|" + sTestCd + "|" + sRst + "|")
                        End If
                    End If

                    If arlRst.Count > 0 Then
                        Dim sMsg As String = (New RegAboRh).fnExe_Reg_Rst(arlRst, USER_INFO.USRID)
                        If sMsg <> "" Then MsgBox(sMsg)
                    End If
                Next

                btnQuery_Click(Nothing, Nothing)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub sbForm_Initialize()
        Dim sFn As String = "Private Sub sbForm_Initialize()"

        Try
            Dim strTmp As String

            '-- 서버날짜로 설정
            strTmp = (New LISAPP.APP_DB.ServerDateTime).GetDate("-") + " 00:00:00"

            Me.dtpDateS.Value = CDate(strTmp)
            Me.dtpDateE.Value = Me.dtpDateS.Value

            Me.txtBcNo.Text = ""

            sbDisplay_Slip()
            sbDisplay_TGrp()

            sbGet_ABOandRH_Code(msABOCode, msRhCode)

            With Me.spdList
                .MaxRows = 0
                .Col = .GetColFromID("abo") - 4 : .ColID = msABOCode
                .Col = .GetColFromID("rh") - 4 : .ColID = msRhCode
            End With

            Dim sTgrp As String = "", sWkGrp As String = "", sJob As String = ""

            sTgrp = COMMON.CommXML.getOneElementXML(msXMLDir, msTgrpFile, "TGRP")
            sWkGrp = COMMON.CommXML.getOneElementXML(msXMLDir, msWkGrpFile, "WKGRP")
            sJob = COMMON.CommXML.getOneElementXML(msXMLDir, msQryFile, "JOB")

            If cboTGrp.Items.Count > 0 Then
                If sTgrp = "" Or Val(sTgrp) > cboTGrp.Items.Count Then
                    Me.cboTGrp.SelectedIndex = 0
                Else
                    Me.cboTGrp.SelectedIndex = Convert.ToInt16(sTgrp)
                End If
            End If

            If cboWkGrp.Items.Count > 0 Then
                If sWkGrp = "" Or Val(sWkGrp) > cboWkGrp.Items.Count Then
                    Me.cboWkGrp.SelectedIndex = 0
                Else
                    Me.cboWkGrp.SelectedIndex = Convert.ToInt16(sWkGrp)
                End If
            End If

            If sJob = "" Or Val(sJob) > cboQrygbn.Items.Count Then
                Me.cboQrygbn.SelectedIndex = 0
            Else
                Me.cboQrygbn.SelectedIndex = Convert.ToInt16(sJob)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub sbDisplay_Date_Setting()

        If cboQrygbn.Text = "검사그룹" Then
            Me.lblTitleDt.Text = "접수일자"

            Me.dtpDateE.Visible = True
            Me.lblDate.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

            Me.txtWkNoS.Visible = False : txtWkNoE.Visible = False
            Me.cboTGrp.Visible = True : Me.cboWkGrp.Visible = False

        Else
            Me.lblTitleDt.Text = "작업일자"

            Me.dtpDateE.Visible = False
            Me.lblDate.Visible = False

            Me.txtWkNoS.Visible = True : Me.txtWkNoE.Visible = True
            Me.lblWk.Visible = True

            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = True

            Dim sWkNoGbn As String = cboWkGrp.Text.Split("|"c)(1)

            Select Case sWkNoGbn
                Case "1"
                    Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
                Case "2"
                    Me.dtpDateS.CustomFormat = "yyyy-MM"
                Case "3"
                    Me.dtpDateS.CustomFormat = "yyyy"
                Case Else
                    Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            End Select
        End If
    End Sub

    Private Sub sbDisplay_Slip()

        Dim sFn As String = "Sub sbDisplay_Slip()"

        Try
            Dim sTmp As String = ""

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List(Me.dtpDateS.Text, , True)

            Me.cboPartSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            sTmp = COMMON.CommXML.getOneElementXML(msXMLDir, msSlipFile, "SLIP")

            If sTmp = "" Or Val(sTmp) > Me.cboPartSlip.Items.Count Then
                Me.cboPartSlip.SelectedIndex = 0
            Else
                Me.cboPartSlip.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_TGrp() ''' 검사그룹조회 

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_List(False, False, True)

        Me.cboTGrp.Items.Clear()
        Me.cboTGrp.Items.Add("[  ] 전체")

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboTGrp.Items.Add("[" + dt.Rows(ix).Item("tgrpcd").ToString + "] " + dt.Rows(ix).Item("tgrpnmd").ToString)
        Next
        cboTGrp.SelectedIndex = 0
    End Sub

    Private Sub sbDisplay_WkGrp()

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_WKGrp_List(Ctrl.Get_Code(Me.cboPartSlip))

        Me.cboWkGrp.Items.Clear()

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboWkGrp.Items.Add("[" + dt.Rows(ix).Item("wkgrpcd").ToString + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString)
        Next

        Me.cboWkGrp.SelectedIndex = 0
    End Sub

    Private Sub sbDisplay_Data(Optional ByVal rsBcNo As String = "")
        Dim sFn As String = "Private Sub sbDisplay_Data()"

        Try

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim dt As New DataTable

            Dim sTGrpCd As String = ""
            Dim sWkYmd As String = "", sWGrpCd As String = "", sWkNoS As String = "", sWkNoE As String = ""
            Dim sRstFlg As String = ""

            If Me.chkRstNull.Checked And Me.chkRstReg.Checked Then
                sRstFlg = ""
            ElseIf Me.chkRstNull.Checked Then
                sRstFlg = "1"
            ElseIf Me.chkRstReg.Checked Then
                sRstFlg = "2"
            End If

            If Me.cboQrygbn.Text = "작업그룹" And Ctrl.Get_Code(Me.cboWkGrp) <> "" Then
                sWkYmd = Me.dtpDateS.Text.PadRight(8, "0"c)
                sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
                sWkNoS = Me.txtWkNoS.Text.PadRight(4, "0"c)

                If Me.txtWkNoE.Text = "" Then
                    sWkNoE = "9999"
                Else
                    sWkNoE = Me.txtWkNoS.Text.PadRight(4, "0"c)
                End If


                dt = fnGet_ABOandRh_Result_WGrp("", sWkYmd, sWGrpCd, sWkNoS, sWkNoE, sRstFlg)

            Else
                sTGrpCd = Ctrl.Get_Code(cboTGrp)

                dt = fnGet_ABOandRH_Result_TGrp(sTGrpCd, Me.dtpDateS.Text.Replace("-", "").Replace(" ", ""), Me.dtpDateE.Text.Replace("-", "").Replace(" ", ""), sRstFlg)

            End If

            sbDisplay_Data_View(dt)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View(ByVal r_dt As DataTable, Optional ByVal rbAdd As Boolean = False)
        Dim sFn As String = "Protected Sub sbDisplay_Data_View(DataTable)"

        Try
            Dim strBcNo As String = ""

            With Me.spdList
                If Not rbAdd Then .MaxRows = 0
                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    ' 새로운 바코드 구분
                    If strBcNo <> r_dt.Rows(ix).Item("bcno").ToString Then
                        .MaxRows += 1
                    End If

                    .Row = .MaxRows
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("workno") : .Text = r_dt.Rows(ix).Item("workno").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("dept") : .Text = r_dt.Rows(ix).Item("dept").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("diagnm") : .Text = r_dt.Rows(ix).Item("diagnm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("docrmk") : .Text = r_dt.Rows(ix).Item("doctorrmk").ToString.Trim
                    .Col = .GetColFromID("rstnm") : .Text = r_dt.Rows(ix).Item("rstnm").ToString.Trim
                    .Col = .GetColFromID("rstdt") : .Text = r_dt.Rows(ix).Item("rstdt").ToString.Trim
                    .Col = .GetColFromID("fnnm") : .Text = r_dt.Rows(ix).Item("fnnm").ToString.Trim
                    .Col = .GetColFromID("fndt") : .Text = r_dt.Rows(ix).Item("fndt").ToString.Trim

                    strBcNo = r_dt.Rows(ix).Item("bcno").ToString

                    Dim iCol As Integer = .GetColFromID(r_dt.Rows(ix).Item("testcd").ToString.Trim)
                    If iCol > 0 Then
                        .Col = iCol : .Text = r_dt.Rows(ix).Item("viewrst").ToString
                        .Col = iCol + 4 : .Text = r_dt.Rows(ix).Item("rstval").ToString
                    End If
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub


    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)
        Dim sFn As String = "Sub sbPrint_Data()"

        Try
            Dim alPrint As New ArrayList

            With spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    Dim sBuf() As String = rsTitle_Item.Split("|"c)
                    Dim arlItem As New ArrayList

                    For ix As Integer = 0 To sBuf.Length - 1

                        If sBuf(ix) = "" Then Exit For

                        Dim intCol As Integer = .GetColFromID(sBuf(ix).Split("^"c)(1))

                        If intCol > 0 Then

                            Dim sTitle As String = sBuf(ix).Split("^"c)(0)
                            Dim sField As String = sBuf(ix).Split("^"c)(1)
                            Dim sWidth As String = sBuf(ix).Split("^"c)(2)

                            .Row = iRow
                            .Col = .GetColFromID(sField) : Dim sVal As String = .Text

                            arlItem.Add(sVal + "^" + sTitle + "^" + sWidth + "^")
                        End If
                    Next

                    Dim objPat As New FGB00_PATINFO

                    With objPat
                        .alItem = arlItem
                    End With

                    alPrint.Add(objPat)
                Next
            End With

            If alPrint.Count > 0 Then
                Dim prt As New FGB00_PRINT

                prt.msTitle = "혈액형 검사결과 리스트"

                If lblTitleDt.Text = "접수일자" Then
                    prt.msTitle_sub_left_1 = lblTitleDt.Text + ": " + dtpDateS.Text + " ~ " + dtpDateE.Text
                Else
                    Dim sWkNo As String = ""
                    If txtWkNoS.Text + txtWkNoE.Text = "" Then
                    Else
                        sWkNo = Space(5) + "접수번호: " + IIf(txtWkNoS.Text = "", "0001", txtWkNoS.Text).ToString + " ~ " + IIf(txtWkNoE.Text = "", "9999", txtWkNoE.Text).ToString
                    End If
                    prt.msTitle_sub_left_1 = lblTitleDt.Text + ": " + dtpDateS.Text + sWkNo
                End If

                prt.maPrtData = alPrint

                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

#End Region

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        sbReg()
    End Sub

    Private Sub FGB04_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim sFn As String = ""

        Try
            txtBcNo.Text = ""
            txtBcNo.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FGB04_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB04_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                btnReg_Click(Nothing, Nothing)
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        With spdList
            .MaxRows = 0
            .MaxCols = .GetColFromID("spcnmd")
            .MaxCols += 6
        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Private Sub FGB04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        Me.WindowState = FormWindowState.Maximized

        sbForm_Initialize()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim sFn As String = "Handles btnPrint.Click"

        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\B01.dll", "B01.FGB00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sFn As String = "Sub btnExcel_ButtonClick()"
        Dim sBuf As String = ""

        Try
            With spdList
                .ReDraw = False

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text <> "1" And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .RowHidden = True
                Next

                .Col = .GetColFromID("chk") : .ColHidden = True

                .MaxRows = .MaxRows + 1
                .InsertRows(1, 1)

                For i As Integer = 1 To .MaxCols
                    .Col = i : .Row = 0 : sBuf = .Text
                    .Col = i : .Row = 1 : .Text = sBuf
                Next

                If .ExportToExcel("혈액형결과_" + Now.ToShortDateString() + ".xls", "Worklist", "") Then
                    Process.Start("혈액형결과_" + Now.ToShortDateString() + ".xls")
                End If


                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text <> "1" And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .RowHidden = False
                Next

                .Col = .GetColFromID("chk") : .ColHidden = False

                .DeleteRows(1, 1)
                .MaxRows -= 1

                .ReDraw = True

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub chkSelChk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelChk.Click

        With spdList
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Text = IIf(chkSelChk.Checked, "1", "").ToString
                End If
            Next
        End With

    End Sub

    Private Sub txtBcno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBcNo.Click
        Dim sFn As String = ""

        Try
            Me.txtBcNo.SelectAll()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub txtBcno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim sFn As String = ""
        If e.KeyCode <> Keys.Enter Then Return

        Try

            Dim sBcNo As String = Me.txtBcNo.Text.Trim().Replace("-", "")
            Dim dt As New DataTable

            If sBcNo.Length = 14 Then sBcNo = sBcNo + "0"
            If sBcNo.Length < 13 Then
                sBcNo = LISAPP.COMM.BcnoFn.fnFind_BcNo(sBcNo)
            End If

            With spdList

                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("bcno") : Dim strTmp As String = .Text.Replace("-", "")

                    If strTmp = sBcNo Then
                        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "리스트에 존재하는 검체입니다.!!")
                        Return
                    End If
                Next
            End With

            dt = fnGet_ABOandRh_Result_WGrp(sBcNo, "", "", "", "", "")

            If dt.Rows.Count > 0 Then
                With Me.spdList
                    .ReDraw = False

                    sbDisplay_Data_View(dt, True)

                    .ReDraw = True
                End With
                Me.txtBcNo.Text = ""
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "검체를 확인하세요.!!")
            End If

            Me.txtBcNo.SelectAll()
            Me.txtBcNo.Focus()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub spdList_BlockSelected(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BlockSelectedEvent) Handles spdList.BlockSelected
        Dim sFn As String = ""

        Try
            spdList.ClearSelection()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        sbDisplay_Data()
    End Sub

    Private Sub spdList_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdList.Change

        With spdList
            If e.col = .GetColFromID("spcnmd") + 3 Then
                .Row = e.row
                .Col = .GetColFromID("spcnmd") + 1 : Dim sOAbo As String = .Text
                .Col = .GetColFromID("spcnmd") + 3 : Dim sCAbo As String = .Text.ToUpper : .Text = sCAbo

                If sOAbo <> sCAbo Then
                    .Col = e.col : .BackColor = Color.Coral
                Else
                    .Col = e.col : .BackColor = Color.White
                End If

            ElseIf e.col = .GetColFromID("spcnmd") + 4 Then
                .Row = e.row
                .Col = .GetColFromID("spcnmd") + 2 : Dim sORh As String = .Text
                .Col = .GetColFromID("spcnmd") + 4 : Dim sCRh As String = .Text

                If sORh <> sCRh Then
                    .Col = e.col : .BackColor = Color.Coral
                Else
                    .Col = e.col : .BackColor = Color.White
                End If
            End If

            If e.col = .GetColFromID("spcnmd") + 3 Or e.col = .GetColFromID("spcnmd") + 4 Then
                .Row = e.row
                .Col = .GetColFromID("chk") : .Text = "1"
            End If
        End With
    End Sub

    Private Sub spdList_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdList.KeyDownEvent
        If e.keyCode <> Keys.Enter Then Return

        With spdList
            If .ActiveCol = .GetColFromID("spcnmd") + 4 Then
                .Row = .ActiveRow + 1 : .Col = .GetColFromID("spcnmd") + 2
                .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            End If
        End With
    End Sub

    Private Sub Panel2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlBottom.DoubleClick
        With spdList
            .Col = .GetColFromID("spcnmd") + 1 : .ColHidden = CType(IIf(.ColHidden, False, True), Boolean)
            .Col = .GetColFromID("spcnmd") + 2 : .ColHidden = CType(IIf(.ColHidden, False, True), Boolean)
        End With
    End Sub

    Private Sub cboQrygbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQrygbn.SelectedIndexChanged
        sbDisplay_Date_Setting()

        COMMON.CommXML.setOneElementXML(msXMLDir, msQryFile, "JOB", cboQrygbn.SelectedIndex.ToString)
    End Sub

    Private Sub cboPartSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged

        Me.spdList.MaxRows = 0
        sbDisplay_WkGrp()
        sbDisplay_Date_Setting()

        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", Me.cboPartSlip.SelectedIndex.ToString)

    End Sub

    Private Sub cboTGrp_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        Me.spdList.MaxRows = 0
        COMMON.CommXML.setOneElementXML(msXMLDir, msTgrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged

        Me.spdList.MaxRows = 0
        sbDisplay_Date_Setting()

        If cboWkGrp.SelectedIndex >= 0 Then
            COMMON.CommXML.setOneElementXML(msXMLDir, msWkGrpFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)
        End If

    End Sub

    'Private Sub chkRstReg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRstReg.CheckedChanged
    '    'With Me.spdList
    '    '    If chkRstReg.Checked Then
    '    '        .Col = .GetColFromID("abo") - 4 : .ColHidden = False
    '    '        .Col = .GetColFromID("rh") - 4 : .ColHidden = False
    '    '    Else
    '    '        .Col = .GetColFromID("abo") - 4 : .ColHidden = True
    '    '        .Col = .GetColFromID("rh") - 4 : .ColHidden = True
    '    '    End If

    '    'End With
    'End Sub
End Class

Public Class FGB04_PATINFO
    Public sRegNo As String = ""
    Public sPatNm As String = ""
    Public sSexAge As String = ""
    Public sDeptWard As String = ""
    Public sDoctorNm As String = ""
    Public sDocRmk As String = ""
    Public sSpcNmd As String = ""
    Public sBcNo As String = ""
    Public sWorkNo As String = ""
    Public sDiagNm As String = ""

    Public sPrtBcNo As String = ""

    Public sAbo_1 As String = ""
    Public sRh_1 As String = ""
    Public sAbo_2 As String = ""
    Public sRh_2 As String = ""

End Class

Public Class FGB04_PRINT
    Private Const msFile As String = "File : FGB04.vb, Class : B01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0
    'Private miCCol As Integer = 1

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msTitle As String
    Public maPrtData As ArrayList
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")
    Public miTotExmCnt As Integer = 0

    Public Sub sbPrint_Preview(ByVal rbFixed As Boolean)
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = False
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub sbPrint(ByVal rbFixed As Boolean)
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = True
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport
                prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miCIdx = 0
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim fnt_BarCd As New Font("Code39(2:3)", 18, FontStyle.Regular)
        Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far


        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF

        If miCIdx = 0 Then miPageNo = 0

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
            Dim strWkNo As String = CType(maPrtData.Item(intIdx), FGB04_PATINFO).sWorkNo.Substring(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sWorkNo.Length - 4)

            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 검체번호
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 0, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sBcNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 작업번호
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sWorkNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 등록번호
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 성명
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * 0, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 성별/나이
            rect = New Drawing.RectangleF(msgPosX(5), sngPosY, msgPosX(6) - msgPosX(5), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sSexAge, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 진료과/병동
            rect = New Drawing.RectangleF(msgPosX(6), sngPosY, msgPosX(7) - msgPosX(6), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sDeptWard, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- ABO
            rect = New Drawing.RectangleF(msgPosX(7), sngPosY, msgPosX(8) - msgPosX(7), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sAbo_2, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- Rh
            rect = New Drawing.RectangleF(msgPosX(8), sngPosY, msgPosX(9) - msgPosX(8), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB04_PATINFO).sRh_2, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            sngPosY += sngPrtH                              '데이터로우간 간격 조절
            If msgHeight - sngPrtH * 4 < sngPosY Then miCIdx += 1 : Exit For ' 전체크기에서 로우의높이를 나눈것보다 크면 다음페이지

            miCIdx += 1

            If (intIdx + 1) Mod 5 = 0 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY, msgWidth, sngPosY)
            End If

        Next

        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

        If miCIdx < maPrtData.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1

        Dim sgPosX(0 To 9) As Single

        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(0) + 40
        sgPosX(2) = sgPosX(1) + 140
        sgPosX(3) = sgPosX(2) + 140
        sgPosX(4) = sgPosX(3) + 80
        sgPosX(5) = sgPosX(4) + 85
        sgPosX(6) = sgPosX(5) + 80
        sgPosX(7) = sgPosX(6) + 85
        sgPosX(8) = sgPosX(7) + 70

        sgPosX(sgPosX.Length - 1) = msgWidth

        msgPosX = sgPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = Convert.ToSingle(fnt_Title.GetHeight(e.Graphics) * 1.5)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = fnt_Head.GetHeight(e.Graphics)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sngPosY, msgWidth - sgPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sngPosY, msgWidth - msgLeft - 25, sngPrt), sf_r)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrt), sf_c)
        e.Graphics.DrawString("검체번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(2) - sgPosX(1), sngPrt), sf_c)
        e.Graphics.DrawString("작업번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(2), sngPosY, sgPosX(3) - sgPosX(2), sngPrt), sf_c)
        e.Graphics.DrawString("등록번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(3), sngPosY, sgPosX(4) - sgPosX(3), sngPrt), sf_c)
        e.Graphics.DrawString("성명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(4), sngPosY, sgPosX(5) - sgPosX(4), sngPrt), sf_c)
        e.Graphics.DrawString("성별/나이", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(5), sngPosY, sgPosX(6) - sgPosX(5), sngPrt), sf_c)
        e.Graphics.DrawString("진료과/병동", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(6), sngPosY, sgPosX(7) - sgPosX(6), sngPrt), sf_c)
        e.Graphics.DrawString("ABO", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(7), sngPosY, sgPosX(8) - sgPosX(7), sngPrt), sf_c)
        e.Graphics.DrawString("Rh", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(8), sngPosY, sgPosX(9) - sgPosX(8), sngPrt), sf_c)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt + sngPrt / 4, msgWidth, sngPosY + sngPrt + sngPrt / 4)

        msgPosX = sgPosX

        Return sngPosY + sngPrt + sngPrt / 2

    End Function



End Class