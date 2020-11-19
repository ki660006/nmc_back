'>>> 특이결과 조회

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports LISAPP.APP_S.RstSrh

Public Class FGS10
    Inherits System.Windows.Forms.Form

    Private Const msXML As String = "\XML"
    Private msSlipFile As String = Application.StartupPath + msXML + "\FGS10_SLIP.XML"
    Private msCfmId As String = ""

#Region " Form내부 함수 "
    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1"
            .TITLE = "통보일시"
            .WIDTH = "120"
            .FIELD = "regdt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "통보자"
            .WIDTH = "60"
            .FIELD = "regnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1"
            .TITLE = "검체번호"
            .WIDTH = "140"
            .FIELD = "bcno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1"
            .TITLE = "등록번호"
            .WIDTH = "70"
            .FIELD = "regno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1"
            .TITLE = "성명"
            .WIDTH = "60"
            .FIELD = "patnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1"
            .TITLE = "성별/나이"
            .WIDTH = "40"
            .FIELD = "sexage"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "처방일자"
            .WIDTH = "120"
            .FIELD = "orddt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "의뢰의사"
            .WIDTH = "60"
            .FIELD = "doctornm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "진료과/병동"
            .WIDTH = "120"
            .FIELD = "deptward"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "검체명"
            .WIDTH = "100"
            .FIELD = "spcnmd"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "확인자"
            .WIDTH = "100"
            .FIELD = "cfnnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "확인일시"
            .WIDTH = "120"
            .FIELD = "cfndt"
        End With
        alItems.Add(stu_item)


        'stu_item = New STU_PrtItemInfo
        'With stu_item
        '    .CHECK = "1"
        '    .TITLE = "조치사항"
        '    .WIDTH = "300"
        '    .FIELD = "cmtcont"
        'End With
        'alItems.Add(stu_item)

        Return alItems

    End Function

    ' 화면 정리
    Private Sub sbClear_Form()
        Me.spdList.MaxRows = 0
    End Sub

    Private Sub sbDisp_Init()

        Try
            Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

            Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
            Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")

            sbDisplay_Slip()    '-- 검사분야 
            sbDisplay_dept()
            sbDisplay_Ward()

            Me.dtpDateS.Focus()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Cust()

        Try
            Dim dt As DataTable = (New LISAPP.LISAPP_O_CUST_ORD).fnGet_CustList()

            If dt.Rows.Count < 1 Then Return

            Me.cboDeptCd.Items.Clear()
            Me.cboDeptCd.Items.Add("[ ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDeptCd.Items.Add(dt.Rows(ix).Item("cust").ToString)
            Next

            If Me.cboDeptCd.Items.Count > 0 Then Me.cboDeptCd.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_CmtCont()

        Try

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Etc_CdLists("A")

            Me.cboCmtCont.Items.Clear()
            Me.cboCmtCont.Items.Add("")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboCmtCont.Items.Add(dt.Rows(ix).Item("cmtcont").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Dr(ByVal rsDeptCd As String)

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptDoctorList(rsDeptCd, "")

            Me.cboDr.Items.Clear()
            Me.cboDr.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim sTmp As String = "[" + dt.Rows(ix).Item("doctorcd").ToString + "] " + dt.Rows(ix).Item("doctornm").ToString.Trim
                If Me.cboDr.Items.Contains(sTmp) Then
                Else
                    Me.cboDr.Items.Add(sTmp)
                End If
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Slip()

         Dim dt As DataTable

        Try
            Dim sTmp As String = ""

            dt = LISAPP.COMM.cdfn.fnGet_Slip_List(, True)

            Me.cboPartSlip.Items.Clear()
            Me.cboPartSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            sTmp = COMMON.CommXML.getOneElementXML(msXML, msSlipFile, "SLIP")

            If sTmp = "" Or Val(sTmp) > cboPartSlip.Items.Count Then
                Me.cboPartSlip.SelectedIndex = 0
            Else
                Me.cboPartSlip.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Ward()

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList()

            Me.cboWard.Items.Clear()
            Me.cboWard.Items.Add("" + Space(200) + "|")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboWard.Items.Add(dt.Rows(ix).Item("wardnm").ToString.Trim + Space(200) + "|" + dt.Rows(ix).Item("wardno").ToString)
            Next

            If Me.cboWard.Items.Count > 0 Then Me.cboWard.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_dept()

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList()

            Me.cboDeptCd.Items.Clear()
            Me.cboDeptCd.Items.Add("" + Space(200) + "|")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDeptCd.Items.Add(dt.Rows(ix).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(ix).Item("deptcd").ToString)
            Next

            If Me.cboDeptCd.Items.Count > 0 Then Me.cboDeptCd.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Data()

        Try

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim sSlipCd As String = ""
            Dim sDrCd As String = ""
            Dim sCfmFlg As String = "ALL"
            Dim sWardNo As String = ""
            Dim sDeptCd As String = ""

            If Me.cboDeptCd.Text.IndexOf("|") >= 0 Then sDeptCd = Me.cboDeptCd.Text.Split("|"c)(1)
            If Me.cboWard.Text.IndexOf("|") >= 0 Then sWardNo = Me.cboWard.Text.Split("|"c)(1)

            If Me.rdoCfmNo.Checked Then
                sCfmFlg = "NO"
            ElseIf Me.rdoCfmOk.Checked Then
                sCfmFlg = "OK"
            End If

            If Me.lblDr.Visible Then sDrCd = Ctrl.Get_Code(Me.cboDr)
            If Me.lblPartSlip.Visible Then sSlipCd = Ctrl.Get_Code(Me.cboPartSlip)

            Dim dt As DataTable = fnGet_Abnormal_ActionInfo(Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), Ctrl.Get_Code(Me.cboPartSlip), _
                                                            Me.txtRegNo.Text, Me.txtPatnm.Text, sDeptCd, sWardNo, Me.cboCmtCont.Text, _
                                                            sCfmFlg, sDrCd)

            sbDisplay_Data_View(dt)


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View(ByVal r_dt As DataTable)

        Try
            With Me.spdList
                .MaxRows = r_dt.Rows.Count
                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    For ix2 As Integer = 0 To r_dt.Columns.Count - 1
                        Dim iCol As Integer = .GetColFromID(r_dt.Columns(ix2).ColumnName.ToLower)
                        If iCol > 0 Then
                            .Row = ix + 1
                            .Col = iCol : .Text = r_dt.Rows(ix).Item(ix2).ToString.Trim
                        End If
                    Next

                    .set_RowHeight(ix + 1, .get_MaxTextRowHeight(ix + 1))
                Next

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View_Fix(ByVal r_dt As DataTable)

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList
            Dim strBcNo As String = "", strDocRmk As String = ""
            Dim intBcNo_Start_Row As Integer = 0
            Dim intGrpNo As Integer = 0
            Dim objBColor As System.Drawing.Color
            Dim intCol As Integer = 0

            With spd
                .MaxRows = 0

                .ReDraw = False

                For intRow As Integer = 0 To r_dt.Rows.Count - 1
                    ' 새로운 바코드 구분
                    If strBcNo <> r_dt.Rows(intRow).Item("bcno").ToString Then

                        If intBcNo_Start_Row > 0 Then
                            For intIx1 As Integer = intBcNo_Start_Row To .MaxRows
                                .Row = intIx1
                                .Col = .GetColFromID("docrmk") : .Text = strDocRmk
                            Next
                        End If

                        intGrpNo += 1
                        If intGrpNo Mod 2 = 1 Then
                            objBColor = System.Drawing.Color.White
                        Else
                            objBColor = System.Drawing.Color.FromArgb(255, 251, 244)
                        End If

                        .MaxRows += 1
                        .Row = .MaxRows

                        ' Line 그리기
                        If intRow > 1 Then Fn.DrawBorderLineTop(spdList, intRow)

                        '배경색 설정
                        .Row = .MaxRows : .Col = -1
                        .BackColor = objBColor

                        intBcNo_Start_Row = .MaxRows
                        strDocRmk = ""
                        intCol = .GetColFromID("spcnmd")
                    End If

                    If r_dt.Rows(intRow).Item("docrmk").ToString <> "" Then
                        strDocRmk += IIf(strDocRmk = "", "", ",").ToString + r_dt.Rows(intRow).Item("docrmk").ToString
                    End If
                    strBcNo = r_dt.Rows(intRow).Item("bcno").ToString

                    .Row = .MaxRows
                    .Col = 0 : .Text = intGrpNo.ToString
                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(intRow).Item("bcno").ToString
                    .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(intRow).Item("orddt").ToString
                    .Col = .GetColFromID("colldt") : .Text = r_dt.Rows(intRow).Item("colldt").ToString
                    .Col = .GetColFromID("collnm") : .Text = r_dt.Rows(intRow).Item("collnm").ToString
                    .Col = .GetColFromID("passdt") : .Text = r_dt.Rows(intRow).Item("passdt").ToString
                    .Col = .GetColFromID("passnm") : .Text = r_dt.Rows(intRow).Item("passnm").ToString
                    .Col = .GetColFromID("recdt") : .Text = r_dt.Rows(intRow).Item("recdt").ToString
                    .Col = .GetColFromID("recnm") : .Text = r_dt.Rows(intRow).Item("recnm").ToString
                    .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(intRow).Item("tkdt").ToString
                    .Col = .GetColFromID("tknm") : .Text = r_dt.Rows(intRow).Item("tknm").ToString
                    .Col = .GetColFromID("tkdt2") : .Text = r_dt.Rows(intRow).Item("tkdt2").ToString
                    .Col = .GetColFromID("tknm2") : .Text = r_dt.Rows(intRow).Item("tknm2").ToString

                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(intRow).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(intRow).Item("patnm").ToString
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(intRow).Item("sexage").ToString
                    .Col = .GetColFromID("dept") : .Text = r_dt.Rows(intRow).Item("dept").ToString
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(intRow).Item("doctornm").ToString
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(intRow).Item("spcnmd").ToString

                    intCol = .GetColFromID(r_dt.Rows(intRow).Item("tclscd").ToString)
                    If intCol > 0 Then
                        .Col = intCol
                        .Text = "▷"
                    End If
                Next

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim arlPrint As New ArrayList

            With spdList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow

                    Dim strBuf() As String = rsTitle_Item.Split("|"c)
                    Dim arlItem As New ArrayList

                    For intIdx As Integer = 0 To strBuf.Length - 1

                        If strBuf(intIdx) = "" Then Exit For

                        Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

                        If intCol > 0 Then

                            Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                            Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                            Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                            .Row = intRow
                            .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                            arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                        End If
                    Next

                    .Col = .GetColFromID("cmtcont") : Dim sCmtCont As String = .Text

                    Dim objPat As New FGS00_PATINFO

                    With objPat
                        .alItem = arlItem
                        .CmtCont = sCmtCont + "^" + "조치내용" + "^" + "1000" + "^"
                    End With

                    arlPrint.Add(objPat)
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGS00_PRINT
                prt.mbLandscape = True  '-- false : 세로, true : 가로
                prt.msTitle = "특이결과 리스트"
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
                prt.msJobGbn = ""
                prt.maPrtData = arlPrint
                prt.m_o_BodyFont = New Font("굴림체", 9, FontStyle.Regular)

                If Me.chkPreview.Checked Then
                    prt.sbPrint_Preview("FGS10")
                Else
                    prt.sbPrint("FGS10")
                End If
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

#End Region


    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        sbClear_Form()
        sbDisplay_Data()
    End Sub

    Private Sub FGS10_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbClear_Form()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsIoGbn As String, ByVal rsDeptCd As String, ByVal rsDrCd As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        sbDisplay_Dr(rsDeptCd)

        msCfmId = rsDrCd

        Me.rdoCfmNo.Checked = True
        Me.lblPartSlip.Visible = False : Me.cboPartSlip.Visible = False
        Me.lblDr.Visible = True : Me.cboDr.Visible = True
        Me.btnOK.Visible = True

        If rsIoGbn = "I" Then
            Me.rdoIogbnI.Checked = True
            Me.lblDeptWard.Text = "병  동"
            Me.cboDeptCd.Visible = False
            Me.cboWard.Visible = True
        Else
            Me.rdoIogbnO.Checked = True
            Me.lblDeptWard.Text = "진료과"
            Me.cboDeptCd.Visible = True
            Me.cboWard.Visible = False
        End If

    End Sub

    Private Sub FGS10_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo

        sbDisp_Init()

        '<<< 20170124 기본로드시에는 분야로 선택하도록 
        Me.cboPart.SelectedIndex = 1
        sbDisplay_Slip()
        '>>>

    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try
            Dim invas_buf As New InvAs
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)
                'sbPrint_Data()
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub


    Private Sub btnExcel_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try
            With spdList
                .ReDraw = False

                .MaxRows += 1
                .Row = 1
                .Action = FPSpreadADO.ActionConstants.ActionInsertRow

                For iCol As Integer = 1 To .MaxCols
                    .Row = 0
                    .Col = iCol : Dim strTmp As String = .Text
                    .Row = 1 : .Col = iCol : .Text = strTmp
                Next

                .Col = .GetColFromID("chk") : .ColHidden = True

                If .ExportToExcel("특이결과_" + Now.ToShortDateString() + ".xls", "특이결과", "") Then
                    Process.Start("특이결과_" + Now.ToShortDateString() + ".xls")
                End If


                .Row = 1
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1

                .ReDraw = True

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub txtPatnm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatnm.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtPatnm.Text = "" Then Return

        Try
            e.Handled = True

            Dim dt As New DataTable
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "환자정보"

            dt = OCSAPP.OcsLink.Pat.fnGet_Patinfo("", Me.txtPatnm.Text)

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("bunho", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("suname", "성명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("sex", "성별", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("idno", "주민번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtPatnm)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + Me.txtPatnm.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtRegNo.Text = alList.Item(0).ToString.Split("|"c)(0)
            Else
                MsgBox("해당하는 환자가 없습니다.", MsgBoxStyle.Information, Me.Text)
            End If

            Me.txtPatnm.Text = ""

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged
        sbClear_Form()

        COMMON.CommXML.setOneElementXML(msXML, msSlipFile, "SLIP", cboPartSlip.SelectedIndex.ToString)
    End Sub

    Private Sub txtRegNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.Click
        Me.txtRegNo.SelectionStart = 0
        Me.txtRegNo.SelectAll()

    End Sub

    Private Sub txtRegNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.GotFocus
        Me.txtRegNo.SelectionStart = 0
        Me.txtRegNo.SelectAll()

    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        Dim sRegNo As String = txtRegNo.Text.Trim

        If IsNumeric(sRegNo.Substring(0, 1)) Then
            Me.txtRegNo.Text = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
        Else
            Me.txtRegNo.Text = sRegNo.Substring(0, 1).ToUpper + sRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
        End If

    End Sub

    Private Sub rdoIogbnA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoIogbnA.Click, rdoIogbnI.Click, rdoIogbnO.Click
        If CType(sender, Windows.Forms.RadioButton).Checked Then
            Select Case CType(sender, Windows.Forms.RadioButton).Text
                Case "입원"
                    Me.lblDeptWard.Text = "병  동"
                    Me.cboDeptCd.Visible = False
                    Me.cboWard.Visible = True
                Case Else
                    Me.lblDeptWard.Text = "진료과"
                    Me.cboDeptCd.Visible = True
                    Me.cboWard.Visible = False
            End Select

        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Try
            Dim alCfmInfo As New ArrayList

            With Me.spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("regdt") : Dim sRegDt As String = .Text.Replace("-", "").Replace(":", "").Replace(" ", "")
                    .Col = .GetColFromID("regid") : Dim sRegid As String = .Text.Trim
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text

                    If sChk = "1" Then alCfmInfo.Add(sRegDt + "|" + sRegid)
                Next
            End With

            If alCfmInfo.Count > 0 Then
                Dim frmChild As Windows.Forms.Form
                frmChild = New FGS10_S01(msCfmId, alCfmInfo)

                frmChild.WindowState = FormWindowState.Normal
                frmChild.Activate()
                frmChild.ShowDialog()
            Else
                MsgBox("확인할 자료가 없습니다.!!", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub cboPart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPart.SelectedIndexChanged

        If Me.cboPart.SelectedIndex = 0 Then
            sbDisplay_part()
        Else
            sbDisplay_Slip()
        End If

        'COMMON.CommXML.setOneElementXML(msXML, msPartFile, "PART", cboPart.SelectedIndex.ToString)

        If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0
    End Sub

    Private Sub sbDisplay_part()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Part_List()

            Me.cboPartSlip.Items.Clear()
            Me.cboPartSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

            'Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msPartFile, "PART")

            'If sTmp = "" Or Val(sTmp) > cboSlip.Items.Count Then
            Me.cboPartSlip.SelectedIndex = 0
            'Else
            '    Me.cboSlip.SelectedIndex = Convert.ToInt16(sTmp)
            'End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

End Class

