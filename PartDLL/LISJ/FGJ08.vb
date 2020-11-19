'>>> W/L 생성 및 조회

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.APP_WL
Imports LISAPP.APP_WL.Qry

Public Class FGJ08
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGJ08.vb, Class : LISJ.FGJ08" + vbTab

    Private Const msXMLDir As String = "\XML"
    Private msTestFile As String = Application.StartupPath & msXMLDir & "\FGJ08_TEST.XML"
    Private msWkGrpFile As String = Application.StartupPath & msXMLDir & "\FGJ08_WKGRP.XML"
    Private msTgrpFile As String = Application.StartupPath & msXMLDir & "\FGJ08_TGRP.XML"
    Private msSlipFile As String = Application.StartupPath & msXMLDir & "\FGJ08_SLIP.XML"
    Private msSpcFile As String = Application.StartupPath & msXMLDir & "\FGJ08_SPC.XML"
    Private msQryFile As String = Application.StartupPath & msXMLDir & "\FGJ08_Qry.XML"
    Private msTermFile As String = Application.StartupPath & msXMLDir & "\FGJ08_Term.XML"

    Private m_al_SheetList As New ArrayList
    Private mbMicroBioYn As Boolean = False
    Private mbM As Boolean = False


#Region " Form내부 함수 "
    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

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
            .CHECK = ""
            .TITLE = "작업번호"
            .WIDTH = "120"
            .FIELD = "workno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1"
            .TITLE = "등록번호"
            .WIDTH = "85"
            .FIELD = "regno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "성명"
            .WIDTH = "80"
            .FIELD = "patnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1"
            .TITLE = "성별/나이"
            .WIDTH = "70"
            .FIELD = "sexage"
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
            .FIELD = "deptinfo"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "의사 Remark"
            .WIDTH = "200"
            .FIELD = "doctorrmk"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "진단명"
            .WIDTH = "120"
            .FIELD = "diagnm"
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

        Return alItems

    End Function

    '-- WK Sheet 정의 가져오기
    Private Sub sbDisplay_sheet()
        Dim sFn As String = "Sub sbDisplay_sheet"

        Try

            Dim oFso As New Scripting.FileSystemObject
            Dim oFolder As Scripting.Folder

            If Dir(Application.StartupPath + "\ssf", FileAttribute.Directory) <> "" Then
                oFolder = oFso.GetFolder(Application.StartupPath + "\ssf")
                Dim objFile As Scripting.File

                For Each objFile In oFolder.Files
                    If objFile.Name.ToLower.StartsWith("ws_") Then
                        m_al_SheetList.Add(objFile.Name.Substring(0, objFile.Name.Length - 4).ToLower)
                    End If
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    ' 화면 정리
    Private Sub sbClear_Form()
        Dim sFn As String = "Sub sbClear_Form()"

        Try
            Me.spdList.MaxRows = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub sbDisp_Init()
        Dim sFn As String = "Sub sbDisp_Init()"

        Try
            Me.chkPrtWL.Checked = True
            Me.chkBar_view.Enabled = True

            Me.txtBcNo.Text = ""
            With spdList
                .Col = .GetColFromID("prtbcno") : .ColHidden = True
                .MaxRows = 0
            End With

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

            Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
            Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")
            Me.dtpWkLDt.Value = Now

            sbDisplay_Slip()    '-- 검사분야 
            sbDisplay_TGrp()    '-- 검사그룹

            Dim sTgrp As String = "", sWkGrp As String = "", sJob As String = "", sTerm As String = "", sTestCds As String = "", sSpc As String = ""

            sTgrp = COMMON.CommXML.getOneElementXML(msXMLDir, msTgrpFile, "TGRP")
            sWkGrp = COMMON.CommXML.getOneElementXML(msXMLDir, msWkGrpFile, "WKGRP")
            sJob = COMMON.CommXML.getOneElementXML(msXMLDir, msQryFile, "JOB")
            sTerm = COMMON.CommXML.getOneElementXML(msXMLDir, msTermFile, "TERM")
            sTestCds = COMMON.CommXML.getOneElementXML(msXMLDir, msTestFile, "TEST")
            sSpc = COMMON.CommXML.getOneElementXML(msXMLDir, msSpcFile, "SPC")

            If Me.cboTGrp.Items.Count > 0 Then
                If sTgrp = "" Or Val(sTgrp) > Me.cboTGrp.Items.Count Then
                    cboTGrp.SelectedIndex = 0
                Else
                    cboTGrp.SelectedIndex = Convert.ToInt16(sTgrp)
                End If
            End If

            If Me.cboWkGrp.Items.Count > 0 Then
                If sWkGrp = "" Or Val(sWkGrp) > Me.cboWkGrp.Items.Count Then
                    Me.cboWkGrp.SelectedIndex = 0
                Else
                    Me.cboWkGrp.SelectedIndex = Convert.ToInt16(sWkGrp)
                End If
            End If

            If Me.cboSpcCd.Items.Count > 0 Then
                If sSpc = "" Or Val(sSpc) > Me.cboSpcCd.Items.Count Then
                    Me.cboSpcCd.SelectedIndex = 0
                Else
                    Me.cboSpcCd.SelectedIndex = Convert.ToInt16(sSpc)
                End If
            End If
            If sJob = "" Or Val(sJob) > Me.cboQrygbn.Items.Count Then
                Me.cboQrygbn.SelectedIndex = 0
            Else
                Me.cboQrygbn.SelectedIndex = Convert.ToInt16(sJob)
            End If

            If sTestCds <> "" Then
                Me.txtSelTest.Text = sTestCds.Split("^"c)(1).Replace("|", ",")
                Me.txtSelTest.Tag = sTestCds

                sbDisplay_Test()
            End If

            Me.cboTerm.Text = sTerm

            sbDisplay_Spc()
            sbDisplay_BarCdPrt()
            sbDisplay_sheet()

            Me.dtpDateS.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Date_Setting()

        If cboQrygbn.Text = "검사그룹" Then
            Me.lblTitleDt.Text = "접수일자"

            Me.dtpDateE.Visible = True
            Me.lblDate.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

            Me.txtWkNoS.Visible = False : Me.txtWkNoE.Visible = False
            Me.cboTGrp.Visible = True : Me.cboWkGrp.Visible = False

        ElseIf Me.cboWkGrp.Text <> "" Then
            Me.lblTitleDt.Text = "작업일자"

            Me.dtpDateE.Visible = False
            Me.lblDate.Visible = False

            Me.txtWkNoS.Visible = True : Me.txtWkNoE.Visible = True
            Me.lblWk.Visible = True

            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = True

            Dim sWkNoGbn As String = Me.cboWkGrp.Text.Split("|"c)(1)

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
        Dim dt As DataTable

        Try
            Dim sTmp As String = ""

            If mbMicroBioYn Then
                dt = LISAPP.COMM.CdFn.fnGet_Slip_List(, True, False, mbMicroBioYn, False)
            Else

                dt = LISAPP.COMM.CdFn.fnGet_Slip_List(, False, False, mbMicroBioYn, False)
            End If

            Me.cboSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            sTmp = COMMON.CommXML.getOneElementXML(msXMLDir, msSlipFile, "SLIP")

            If sTmp = "" Or Val(sTmp) > cboSlip.Items.Count Then
                Me.cboSlip.SelectedIndex = 0
            Else
                Me.cboSlip.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_WkGrp()

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_WKGrp_List(Ctrl.Get_Code(Me.cboSlip))

        Me.cboWkGrp.Items.Clear()

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboWkGrp.Items.Add("[" + dt.Rows(ix).Item("wkgrpcd").ToString + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString)
        Next

        If Me.cboWkGrp.Items.Count > 0 Then Me.cboWkGrp.SelectedIndex = 0
    End Sub

    Private Sub sbDisplay_TGrp() ''' 검사그룹조회 

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_List(False, mbMicroBioYn)

        Me.cboTGrp.Items.Clear()

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboTGrp.Items.Add("[" + dt.Rows(ix).Item("tgrpcd").ToString + "] " + dt.Rows(ix).Item("tgrpnmd").ToString)
        Next
        If Me.cboTGrp.Items.Count > 0 Then Me.cboTGrp.SelectedIndex = 0
    End Sub

    Private Sub sbDisplay_Spc()
        Dim sFn As String = "Sub sbDisplay_Spc()"

        Try
            Dim sPartCd As String = ""
            Dim sSlipCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sWGrpCd As String = ""

            If Me.cboQrygbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(cboTGrp)
            Else
                If Ctrl.Get_Code(Me.cboSlip) <> "" Then
                    sPartCd = Ctrl.Get_Code(cboSlip).Substring(0, 1)
                    sSlipCd = Ctrl.Get_Code(cboSlip).Substring(0, 1)
                End If
                If Me.cboQrygbn.Text = "검사그룹" Then sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
            End If

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List("", sPartCd, sSlipCd, sTGrpCd, sWGrpCd, "", "")

            Me.cboSpcCd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSpcCd.Items.Add("[" + dt.Rows(ix).Item("spccd").ToString.Trim + "] " + dt.Rows(ix).Item("spcnmd").ToString.Trim)
            Next


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_wl()

        Dim sFn As String = "Sub sbDisplay_wl()"

        Try

            Dim dt As DataTable = Qry.fnGet_wl_title(Ctrl.Get_Code(Me.cboSlip), "--", Me.dtpWkLDt.Text.Replace("-", ""), Me.dtpWkLDt.Text.Replace("-", ""), "")

            Me.cboWL.Items.Clear()
            Me.cboWL.Items.Add("새로운 W/L 생성")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim sTmp As String = ""
                sTmp += dt.Rows(ix).Item("wltitle").ToString.Trim + Space(200) + "|"
                sTmp += dt.Rows(ix).Item("wlymd").ToString.Trim + "|"
                sTmp += dt.Rows(ix).Item("wluid").ToString.Trim + "|"
                sTmp += dt.Rows(ix).Item("wltype").ToString.Trim + "|"

                Me.cboWL.Items.Add(sTmp)
            Next

            Me.cboWL.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Data_wl(ByVal rsWLUId As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String)
        Dim sFn As String = "Sub sbDisplay_wl()"

        Try
            Dim sRstFlg As String = "0000"

            If chkRstNull.Checked Then sRstFlg = "1000"
            If chkRstReg.Checked Then sRstFlg = sRstFlg.Substring(0, 1) + "100"
            If chkRstMw.Checked Then sRstFlg = sRstFlg.Substring(0, 2) + "10"
            If chkRstFn.Checked Then sRstFlg = sRstFlg.Substring(0, 3) + "1"

            Me.spdList.MaxRows = 0
            sbDisplay_Test_wl(rsWLUId, rsWLYmd, rsWLTitle)

            Dim dt As DataTable = fnGet_wl_List(rsWLUId, rsWLYmd, rsWLTitle, sRstFlg, mbMicroBioYn)

            sbDisplay_Data_View_Fix(dt)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try


    End Sub

    Private Sub sbDisplay_Data(Optional ByVal rsBcNo As String = "")
        Dim sFn As String = "Private Sub sbDisplay_Data()"

        Try

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim sPartSlip As String = ""
            Dim sTGrpCd As String = ""
            Dim sWGrpCd As String = ""
            Dim sWkYmd As String = ""
            Dim sWkNoS As String = ""
            Dim sWkNoE As String = ""
            Dim sDateS As String = ""
            Dim sDateE As String = ""
            Dim sTestCds As String = ""

            If Me.txtSelTest.Text <> "" Then
                sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")
            End If

            Dim sRstFlg As String = "0000"

            If chkRstNull.Checked Then sRstFlg = "1000"
            If chkRstReg.Checked Then sRstFlg = sRstFlg.Substring(0, 1) + "100"
            If chkRstMw.Checked Then sRstFlg = sRstFlg.Substring(0, 2) + "10"
            If chkRstFn.Checked Then sRstFlg = sRstFlg.Substring(0, 3) + "1"


            If cboQrygbn.Text = "작업그룹" Then
                sPartSlip = Ctrl.Get_Code(cboSlip)
                sWkYmd = dtpDateS.Text.Replace("-", "").PadRight(8, "0"c)

                sWGrpCd = Ctrl.Get_Code(cboWkGrp)
                sWkNoS = Me.txtWkNoS.Text.PadLeft(4, "0"c)
                sWkNoE = Me.txtWkNoE.Text.PadLeft(4, "0"c)

                If sWkNoS <> "" Then
                    If IsNumeric(sWkNoS) = False Then
                        MsgBox("작업번호에 숫자를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                        Return
                    End If
                Else
                    sWkNoS = "0000"
                End If

                If sWkNoE <> "" Then
                    If IsNumeric(sWkNoE) = False Then
                        MsgBox("작업번호에 숫자를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                        Return
                    End If
                Else
                    sWkNoE = "9999"
                End If
            Else
                sTGrpCd = Ctrl.Get_Code(cboTGrp)
                If sTGrpCd = "" Then sPartSlip = Ctrl.Get_Code(cboSlip)

                sDateS = dtpDateS.Text.Replace("-", "").Replace(" ", "")
                sDateE = dtpDateE.Text.Replace("-", "").Replace(" ", "")
            End If

            Dim dt As New DataTable
            If sWkYmd <> "" Then
                dt = LISAPP.APP_S.WkFn.fnGet_WorkList_WGrp(sWkYmd, sWGrpCd, sWkNoS, sWkNoE, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, rsBcNo, Me.chkMbtType.Checked, mbMicroBioYn)
            Else
                dt = LISAPP.APP_S.WkFn.fnGet_WorkList_TGrp(sPartSlip, sTGrpCd, sDateS, sDateE, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, rsBcNo, Me.chkMbtType.Checked, mbMicroBioYn)
            End If

            'If cboQrygbn.Text <> "검사그룹" Or sTGrpCd = "" Then
            '    If Me.txtSelTest.Text = "" Then chkTestsFix.Checked = False
            'End If

            If chkTestsFix.Checked Then
                If rsBcNo = "" Or Me.spdList.MaxRows = 0 Then sbDisplay_Test()

                If rsBcNo = "" Then
                    sbDisplay_Data_View_Fix(dt)
                Else
                    sbDisplay_Data_View_Fix(dt, True)
                End If
            Else

                With spdList
                    If rsBcNo = "" Or .MaxRows = 0 Then
                        .ReDraw = False
                        .MaxCols = 17

                        For intCol As Integer = .GetColFromID("spcnmd") + 1 To .MaxCols
                            .Row = 0
                            .Col = intCol : .Text = Convert.ToString(intCol - .GetColFromID("spcnmd"))
                            .ColID = Convert.ToString(intCol - .GetColFromID("spcnmd"))
                        Next
                        .ReDraw = True
                    End If
                End With

                If rsBcNo = "" Then
                    sbDisplay_Data_View(dt)
                Else
                    sbDisplay_Data_View(dt, True)
                End If
            End If


        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View(ByVal r_dt As DataTable, Optional ByVal rbAdd As Boolean = False)
        Dim sFn As String = "Protected Sub sbDisplay_Data_View(DataTable)"

        Try
            Dim objBColor As System.Drawing.Color
            Dim sBcNo As String = ""
            Dim iBcNo_Start_Row As Integer = 0
            Dim iGrpNo As Integer = 0
            Dim iCol As Integer = 0
            Dim iRow As Integer = -1

            If Me.cboTerm.Text = "" Then Me.cboTerm.Text = "5"

            With Me.spdList
                If Not rbAdd Then
                    .MaxRows = 0
                Else
                    iRow = .MaxRows - 1
                End If
                .ReDraw = False

                For ix As Integer = 1 To 5
                    .set_ColWidth(ix + .GetColFromID("spcnmd"), Convert.ToInt32(Me.cboTerm.Text))
                Next

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    ' 새로운 바코드 구분
                    If sBcNo <> r_dt.Rows(ix).Item("bcno").ToString.Trim Then
                        iGrpNo += 1
                        If iGrpNo Mod 2 = 1 Then
                            objBColor = System.Drawing.Color.White
                        Else
                            objBColor = System.Drawing.Color.FromArgb(255, 251, 244)
                        End If

                        .MaxRows += 2
                        iRow += 2

                        .Row = iRow

                        ' Line 그리기
                        If iRow > 1 Then Fn.DrawBorderLineTop(spdList, iRow)

                        '배경색 설정
                        .Row = iRow : .Col = -1
                        .BackColor = objBColor

                        .Row = iRow + 1 : .Col = -1
                        .BackColor = objBColor

                        iBcNo_Start_Row = .MaxRows
                        iCol = .GetColFromID("spcnmd")
                    End If
                    sBcNo = r_dt.Rows(ix).Item("bcno").ToString.Trim

                    .Row = iRow
                    .Col = 0 : .Text = iGrpNo.ToString
                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("prtbcno") : .Text = r_dt.Rows(ix).Item("prtbcno").ToString.Trim
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("wlseq") : .Text = r_dt.Rows(ix).Item("wlseq").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptinfo") : .Text = r_dt.Rows(ix).Item("deptinfo").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("diagnm") : .Text = r_dt.Rows(ix).Item("diagnm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(ix).Item("spccd").ToString.Trim
                    .Col = .GetColFromID("doctorrmk") : .Text = r_dt.Rows(ix).Item("doctorrmk").ToString.Trim

                    .Row = iRow + 1 : .Row2 = iRow + 1
                    .Col = 1 : .Col2 = .GetColFromID("spcnmd")
                    .BlockMode = True
                    .ForeColor = Color.White
                    .BlockMode = False

                    .Row = iRow + 1 : .Row2 = iRow + 1
                    .Col = .GetColFromID("spcnmd") + 1 : .Col2 = .MaxCols
                    .BlockMode = True
                    .ForeColor = Color.Blue
                    .BlockMode = False

                    .Row = iRow + 1
                    .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    .Col = .GetColFromID("prtbcno") : .Text = r_dt.Rows(ix).Item("prtbcno").ToString.Trim
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("wlseq") : .Text = r_dt.Rows(ix).Item("wlseq").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptinfo") : .Text = r_dt.Rows(ix).Item("deptinfo").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("diagnm") : .Text = r_dt.Rows(ix).Item("diagnm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("doctorrmk") : .Text = r_dt.Rows(ix).Item("doctorrmk").ToString.Trim

                    iCol += 1
                    If iCol > .MaxCols Then
                        .MaxCols += 1
                        If Me.cboTerm.Text <> "" Then .set_ColWidth(.MaxCols, Convert.ToInt32(Me.cboTerm.Text))

                        .Row = 0 : .Col = iCol : .Text = (iCol - .GetColFromID("spcnmd")).ToString
                    End If

                    .Row = iRow
                    .Col = iCol : .Text = r_dt.Rows(ix).Item("tnmd").ToString.Trim : .Tag = r_dt.Rows(ix).Item("testcd").ToString.Trim
                    .Row = iRow + 1
                    .Col = iCol : .Text = r_dt.Rows(ix).Item("bfviewrst").ToString.Trim
                    If .Text = "" Then .Text = "▷"

                Next

            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View_Fix(ByVal r_dt As DataTable, Optional ByVal rbAdd As Boolean = False)
        Dim sFn As String = "Protected Sub sbDisplay_Data_View(DataTable)"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList
            Dim sBcNo As String = "", sDoctorRmk As String = ""
            Dim iBcNo_StartRow As Integer = 0
            Dim iGrpNo As Integer = 0
            Dim objBColor As System.Drawing.Color
            Dim iCol As Integer = 0

            With spd
                If Not rbAdd Then
                    .MaxRows = 0
                End If
                .ReDraw = False

                For intRow As Integer = 0 To r_dt.Rows.Count - 1
                    ' 새로운 바코드 구분
                    If sBcNo <> r_dt.Rows(intRow).Item("bcno").ToString.Trim Then

                        If iBcNo_StartRow > 0 Then
                            For intIx1 As Integer = iBcNo_StartRow To .MaxRows
                                .Row = intIx1
                                .Col = .GetColFromID("doctorrmk") : .Text = sDoctorRmk
                            Next
                        End If

                        iGrpNo += 1
                        If iGrpNo Mod 2 = 1 Then
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

                        iBcNo_StartRow = .MaxRows
                        sDoctorRmk = ""
                        iCol = .GetColFromID("spcnmd")
                    End If

                    If r_dt.Rows(intRow).Item("doctorrmk").ToString.Trim <> "" Then
                        sDoctorRmk += IIf(sDoctorRmk = "", "", ",").ToString.Trim + r_dt.Rows(intRow).Item("doctorrmk").ToString.Trim
                    End If
                    sBcNo = r_dt.Rows(intRow).Item("bcno").ToString.Trim

                    .Row = .MaxRows
                    'If cboJobGbn.Text = "작업그룹" Then
                    '    .Col = 0 : .Text = r_dt.Rows(intRow).Item("workno").ToString.Substring(r_dt.Rows(intRow).Item("workno").ToString.Length - 4) 'iGrpNo.ToString
                    'Else
                    .Col = 0 : .Text = iGrpNo.ToString
                    'End If

                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("prtbcno") : .Text = r_dt.Rows(intRow).Item("prtbcno").ToString.Trim
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(intRow).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("wlseq") : .Text = r_dt.Rows(intRow).Item("wlseq").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(intRow).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(intRow).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(intRow).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptinfo") : .Text = r_dt.Rows(intRow).Item("deptinfo").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(intRow).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(intRow).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(intRow).Item("spccd").ToString.Trim
                    .Col = .GetColFromID("diagnm") : .Text = r_dt.Rows(intRow).Item("diagnm").ToString.Trim

                    iCol = .GetColFromID(r_dt.Rows(intRow).Item("testcd").ToString.Trim)
                    If iCol > 0 Then
                        .Col = iCol
                        '.Text = r_dt.Rows(intRow).Item("befview").ToString
                        'If .Text = "" Then
                        '    If r_dt.Rows(intRow).Item("exist").ToString = "1" Then
                        .Text = "▷"
                        'End If
                        '    End If

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

    Private Sub sbDisplay_Test()
        If Me.txtSelTest.Text = "" And Ctrl.Get_Code(Me.cboTGrp) <> "" Then
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_Test_List(Ctrl.Get_Code(Me.cboTGrp), Ctrl.Get_Code(Me.cboSpcCd))

            For ix As Integer = 0 To dt.Rows.Count - 1
                With Me.spdList
                    .Row = 0
                    If .GetColFromID("spcnmd") + ix + 1 > .MaxCols Then
                        .MaxCols += 1
                    End If

                    .Col = .GetColFromID("spcnmd") + ix + 1 : .Text = dt.Rows(ix).Item("tnmd").ToString.Trim
                    .Col = .GetColFromID("spcnmd") + ix + 1 : .ColID = dt.Rows(ix).Item("testcd").ToString.Trim

                End With
            Next
        ElseIf Me.txtSelTest.Text <> "" Then
            Dim strBuf_Cd() As String = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Split("|"c)
            Dim strBuf_Nm() As String = Me.txtSelTest.Tag.ToString.Split("^"c)(1).Split("|"c)

            Me.spdList.MaxCols = Me.spdList.GetColFromID("spcnmd") + 1

            For ix As Integer = 0 To strBuf_Cd.Length - 1
                With spdList
                    .Row = 0
                    If .GetColFromID("spcnmd") + ix + 1 > .MaxCols Then
                        .MaxCols += 1
                    End If

                    .Col = .GetColFromID("spcnmd") + ix + 1 : .Text = strBuf_Nm(ix) : .ColID = strBuf_Cd(ix)
                    If Me.cboTerm.Text <> "" Then .set_ColWidth(.GetColFromID("spcnmd") + ix + 1, Convert.ToInt32(Me.cboTerm.Text))
                End With
            Next
        End If

    End Sub

    Private Sub sbDisplay_Test_wl(ByVal rsWLUId As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String)

        Try
            Dim sTestCds As String = "", sTestNmds As String = ""
            Dim dt As DataTable = fnGet_wl_test(rsWLUId, rsWLYmd, rsWLTitle)

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                With spdList
                    .Row = 0
                    If .GetColFromID("spcnmd") + ix + 1 > .MaxCols Then
                        .MaxCols += 1
                    End If

                    .Col = .GetColFromID("spcnmd") + ix + 1 : .Text = dt.Rows(ix).Item("tnmd").ToString.Trim
                    .Col = .GetColFromID("spcnmd") + ix + 1 : .ColID = dt.Rows(ix).Item("testcd").ToString.Trim

                    Dim sTestCd As String = dt.Rows(ix).Item("testcd").ToString.Trim
                    Dim sTnmd As String = dt.Rows(ix).Item("tnmd").ToString.Trim

                    If ix > 0 Then
                        sTestCds += "|" : sTestNmds += "|"
                    End If

                    sTestCds += sTestCd : sTestNmds += sTnmd

                End With
            Next

            Me.txtSelTest.Text = sTestNmds.Replace("|", ",")
            Me.txtSelTest.Tag = sTestCds + "^" + sTestNmds

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sbPrint_wl(ByVal rsTitle_Item As String)
        Dim sFn As String = "Sub sbPrint_wl()"

        Try
            Dim arlPrint As New ArrayList

            With spdList
                For intRow As Integer = 1 To .MaxRows Step Convert.ToInt16(IIf(chkTestsFix.Checked, 1, 2))
                    .Row = intRow
                    .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                    If strChk = "1" Then

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

                        Dim strTnms As String = "", strRsts As String = ""

                        If chkTestsFix.Checked Then
                            For intCol As Integer = .GetColFromID("spcnmd") + 1 To .MaxCols
                                Dim strTnm As String = "", strRst As String = ""
                                .Row = 0 : .Col = intCol : strTnm = .Text
                                .Row = intRow : .Col = intCol : strRst = .Text

                                If strTnm = "" Then Exit For

                                strTnms += strTnm + "|"
                                strRsts += strRst + "|"
                            Next
                        Else
                            For intCol As Integer = .GetColFromID("spcnmd") + 1 To .MaxCols
                                Dim strTnm As String = "", strRst As String = ""
                                .Row = intRow : .Col = intCol : strTnm = .Text
                                .Row = intRow + 1 : .Col = intCol : strRst = .Text

                                If strTnm <> "" Then
                                    strTnms += strTnm + "|"
                                    strRsts += strRst.Replace("▷", "") + "|"
                                End If
                            Next
                        End If


                        Dim objPat As New FGJ08_PATINFO

                        With objPat
                            .alItem = arlItem

                            .sTNms = strTnms
                            .sRsts = strRsts
                        End With

                        arlPrint.Add(objPat)
                    End If
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGJ08_PRINT
                prt.mbLandscape = True  '-- false : 세로, true : 가로
                prt.msTitle = "W/L 목록"
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
                prt.msJobGbn = cboQrygbn.Text
                prt.maPrtData = arlPrint
                prt.miTotExmCnt = spdList.MaxCols - spdList.GetColFromID("spcnmd")
                prt.mbUseBarNo = Me.chkBar_view.Checked


                If cboTerm.Text = "" Then
                    prt.msgExmWidth = 60.0
                Else
                    prt.msgExmWidth = Convert.ToSingle(cboTerm.Text) * 10 + 2
                End If

                If chkPreview.Checked Then
                    prt.sbPrint_Preview(chkTestsFix.Checked)
                Else
                    prt.sbPrint(chkTestsFix.Checked)
                End If
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbPrint_ws()
        Dim sFn As String = "sbPrint_WorkSheetsbPrint_ws"

        Try

            If chkMbtType.Checked Then
                sbPrint_ws_microbio()
            Else
                If m_al_SheetList.Contains("ws_" + Ctrl.Get_Code(cboTGrp).ToLower) Then
                    'sbPrint_WorkSheet_gwh(Ctrl.Get_Code(cboTGrp))
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub sbPrint_ws_microbio()
        Dim sFn As String = "sbPrint_ws_microbio"

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Dim iCnt As Integer = 0

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(Windows.Forms.Application.StartupPath + "\SSF\ws_micro.ss6")
            xlsWkS = CType(xlsWkB.ActiveSheet, Excel.Worksheet)

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            With spd
                If .ActiveCol = .GetColFromID("chk") Then
                    .SetActiveCell(.GetColFromID("chk") + 1, .ActiveRow)
                End If

                Dim iCntChk As Integer = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iCntChk < 1 Then
                    MsgBox("선택한 자료가 없습니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
                    Return
                End If

                For i As Integer = 1 To .MaxRows
                    If Ctrl.Get_Code(spd, "chk", i) = "1" Then

                        If iCnt = 0 Then
                            For ix2 As Integer = 0 To 3
                                xlsWkS.Range("A" + (2 + 12 * ix2).ToString).Value = ""
                                xlsWkS.Range("H" + (2 + 12 * ix2).ToString).Value = "작업번호: "
                                xlsWkS.Range("O" + (2 + 12 * ix2).ToString).Value = ""
                                xlsWkS.Range("Q" + (2 + 12 * ix2).ToString).Value = Space(3) + "등록번호: " + Space(11) + "성   명: " + Space(10) + " /00"

                                xlsWkS.Range("A" + (3 + 12 * ix2).ToString).Value = " 검체번호 : " '
                                xlsWkS.Range("J" + (3 + 12 * ix2).ToString).Value = "과 /병동: "
                                xlsWkS.Range("T" + (3 + 12 * ix2).ToString).Value = "의뢰의사: "

                                xlsWkS.Range("A" + (4 + 12 * ix2).ToString).Value = " BarCode N: "
                                xlsWkS.Range("J" + (4 + 12 * ix2).ToString).Value = "검 체 명: "

                                xlsWkS.Range("A" + (5 + 12 * ix2).ToString).Value = " 진 단 명 : "
                                xlsWkS.Range("A" + (6 + 12 * ix2).ToString).Value = " Remark   : "
                            Next
                        End If

                        iCnt += 1

                        Dim sPrtBcNo As String = Ctrl.Get_Code(spd, "prtbcno", i)
                        Dim sWkNo1 As String = ""
                        Dim sWkNo2 As String = ""

                        Dim sBuf() As String = Ctrl.Get_Code(spd, "workno", i).Split("-"c)
                        If sBuf.Length = 3 Then
                            sWkNo1 = sBuf(0) + "-" + sBuf(1) + "-" : sWkNo2 = sBuf(2)
                        Else
                            sWkNo1 = Ctrl.Get_Code(spd, "workno", i)
                        End If

                        Dim sRegNo As String = Ctrl.Get_Code(spd, "regno", i) '등록번호 
                        Dim sPatNm As String = Ctrl.Get_Code(spd, "patnm", i)
                        Dim sSexAge As String = Ctrl.Get_Code(spd, "sexage", i)
                        Dim sDept As String = Ctrl.Get_Code(spd, "deptinfo", i)
                        Dim sDoctor As String = Ctrl.Get_Code(spd, "doctornm", i)
                        Dim sSpcNm As String = Ctrl.Get_Code(spd, "spcnmd", i)  '검체병 
                        Dim sDiagnm As String = Ctrl.Get_Code(spd, "diagnm", i) '상병명 
                        Dim sDocRmk As String = Ctrl.Get_Code(spd, "doctorrmk", i) '의사 Remark 

                        Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i)


                        xlsWkS.Range("A" + (2 + 12 * (iCnt - 1)).ToString).Value = "*" + sPrtBcNo + "*"
                        xlsWkS.Range("H" + (2 + 12 * (iCnt - 1)).ToString).Value = "작업번호: " + sWkNo1
                        xlsWkS.Range("O" + (2 + 12 * (iCnt - 1)).ToString).Value = sWkNo2
                        xlsWkS.Range("Q" + (2 + 12 * (iCnt - 1)).ToString).Value = Space(3) + "등록번호: " + sRegNo + Space(3) + "성   명: " + sPatNm + Space(4) + sSexAge

                        xlsWkS.Range("A" + (3 + 12 * (iCnt - 1)).ToString).Value = " 검체번호 : " + sBcNo
                        xlsWkS.Range("J" + (3 + 12 * (iCnt - 1)).ToString).Value = "과 /병동: " + sDept
                        xlsWkS.Range("T" + (3 + 12 * (iCnt - 1)).ToString).Value = "의뢰의사: " + sDoctor

                        xlsWkS.Range("A" + (4 + 12 * (iCnt - 1)).ToString).Value = " BarCode N: " + sPrtBcNo
                        xlsWkS.Range("J" + (4 + 12 * (iCnt - 1)).ToString).Value = "검 체 명: " + sSpcNm

                        xlsWkS.Range("A" + (5 + 12 * (iCnt - 1)).ToString).Value = " 진 단 명 : " + sDiagnm
                        xlsWkS.Range("A" + (6 + 12 * (iCnt - 1)).ToString).Value = " Remark   : " + sDocRmk

                        If iCnt Mod 4 = 0 Then
                            iCnt = 0
                            xlsWkS.PrintOut()
                        End If

                    End If
                Next

                If iCnt Mod 4 > 0 Then xlsWkS.PrintOut()

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub

    Private Sub sbDisplay_BarCdPrt()
        Dim sFn As String = "sbDisplay_BarCdPrt"

        Try
            ' 기본 바코드프린터 설정
            Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbPrint_BarCode()
        ' Barcode 출력
        If IsNumeric(Me.txtPrtCnt.Text) = False Then
            MsgBox("바코드 출력장수를 숫자로 입력해 주십시요!!", MsgBoxStyle.Information, Me.Text)
            Return
        End If


        Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(Me.Name)
        Dim alBcNo As New ArrayList

        With spdList
            For i As Integer = 1 To .MaxRows
                .Row = i
                .Col = .GetColFromID("chk") : Dim strChk As String = .Text
                .Col = .GetColFromID("bcno") : Dim strBcNo As String = .Text.Replace("-", "")

                If strChk = "1" Then alBcNo.Add(strBcNo)
            Next

            If alBcNo.Count > 0 Then
                If Me.chkBar_cult.Checked Then
                    objBCPrt.PrintDo_Micro(alBcNo, Me.txtPrtCnt.Text)
                Else
                    objBCPrt.PrintDo(alBcNo, Me.txtPrtCnt.Text)
                End If
            End If

            alBcNo.Clear()
        End With

        alBcNo = Nothing

    End Sub

#End Region

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Me.spdList.MaxRows = 0
        Me.spdList.MaxCols = Me.spdList.GetColFromID("spcnmd")

        sbDisplay_Data()
    End Sub

    Private Sub FGJ08_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim sFn As String = ""

        Try
            Me.txtBcNo.Text = ""
            Me.txtBcNo.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FGJ08_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGJ08_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Public Sub New(ByVal rbMicroBioYn As Boolean)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        mbMicroBioYn = rbMicroBioYn

        If mbMicroBioYn Then
            msTestFile = Application.StartupPath & msXMLDir & "\FGJ08_M_TEST.XML"
            msWkGrpFile = Application.StartupPath & msXMLDir & "\FGJ08_M_WKGRP.XML"
            msTgrpFile = Application.StartupPath & msXMLDir & "\FGJ08_M_TGRP.XML"
            msSlipFile = Application.StartupPath & msXMLDir & "\FGJ08_M_SLIP.XML"
            msSpcFile = Application.StartupPath & msXMLDir & "\FGJ08_M_SPC.XML"
            msQryFile = Application.StartupPath & msXMLDir & "\FGJ08_M_Qry.XML"
            msTermFile = Application.StartupPath & msXMLDir & "\FGJ08_M_Term.XML"

            Me.Text = Me.Text + "(미생물)"
            Me.chkMbtType.Visible = True
        End If

    End Sub

    Private Sub FGJ08_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        sbDisp_Init()

        Me.axItemSave.FORMID = IIf(mbMicroBioYn, "M", "R").ToString
        Me.axItemSave.USRID = USER_INFO.USRID
        Me.axItemSave.ITEMGBN = ""
        Me.axItemSave.SPCGBN = "NONE"
        Me.axItemSave.MicroBioYn = mbMicroBioYn
        Me.axItemSave.AllPartYn = False
        Me.axItemSave.sbDisplay_ItemList()

    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        If chkPrtBar.Checked Then sbPrint_BarCode()

        If chkPrtWL.Checked Then
            Dim sFn As String = "Handles btnPrint.Click"

            Try
                Dim sReturn As String = ""

                If Me.chkBar_view.Checked Then
                    Dim alPrtItem = fnGet_prt_iteminfo()

                    For ix As Integer = 0 To alPrtItem.Count - 1
                        If ix > 0 Then sReturn += "|"

                        sReturn += CType(alPrtItem(ix), STU_PrtItemInfo).TITLE + "^" + CType(alPrtItem(ix), STU_PrtItemInfo).FIELD + "^" + CType(alPrtItem(ix), STU_PrtItemInfo).WIDTH
                    Next

                    sReturn += "|prtbcno^prtbcno^10"
                Else
                    Dim invas_buf As New InvAs
                    With invas_buf
                        .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                        .SetProperty("UserID", "")

                        Dim a_objParam() As Object
                        ReDim a_objParam(1)

                        a_objParam(0) = Me
                        a_objParam(1) = fnGet_prt_iteminfo()

                        sReturn = CType(.InvokeMember("Display_Result", a_objParam), String)

                        If sReturn Is Nothing Then Return
                        If sReturn.Length < 1 Then Return

                    End With
                End If

                sbPrint_wl(sReturn)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                MsgBox(msFile + sFn + vbCrLf + ex.Message)
            End Try
        ElseIf chkPrtWS.Checked Then
            sbPrint_ws()
        End If

    End Sub

    Private Sub btnSelBCPRT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelBCPRT.Click
        Dim frm As New POPUPPRT.FGPOUP_PRTBC(Me.Name, Me.chkBarInit.Checked)

        frm.ShowDialog()
        frm.Dispose()
        frm = Nothing

        sbDisplay_BarCdPrt()
    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
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

                If .ExportToExcel("WorkList_" + Now.ToShortDateString() + ".xls", "Worklist", "") Then
                    Process.Start("WorkList_" + Now.ToShortDateString() + ".xls")
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

    Private Sub chkTclsFix_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTestsFix.Click

        sbDisplay_Data()

    End Sub

    Private Sub chkColMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkColMove.Click
        If chkColMove.Checked Then
            spdList.AllowColMove = True
        Else
            spdList.AllowColMove = False
        End If
    End Sub

    Private Sub cboTerm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTerm.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXMLDir, msTermFile, "TERM", cboTerm.Text)
    End Sub

    Private Sub txtBcno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBcNo.Click
        Me.txtBcNo.SelectAll()
    End Sub

    Private Sub txtBcno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown
        Dim sFn As String = "Handles txtBcNo.KeyDown"
        If e.KeyCode <> Keys.Enter Then Return

        Try

            Me.txtBcNo.Text = Me.txtBcNo.Text.Trim().Replace("-", "").Trim

            If Me.txtBcNo.Text.Length = 14 Then Me.txtBcNo.Text += "0"
            If Me.txtBcNo.Text.Length = 12 Or Me.txtBcNo.Text.Length = 11 Then
                Me.txtBcNo.Text = LISAPP.COMM.BcnoFn.fnFind_BcNo(Me.txtBcNo.Text)
            End If

            sbDisplay_Data(Me.txtBcNo.Text)

            Me.txtBcNo.SelectionStart = 0
            Me.txtBcNo.SelectAll()
            Me.txtBcNo.Focus()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Dim sFn As String = "Handles btnCdHelp_test.Click"
        Try

            Dim sWGrpCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sPartSlip As String = ""

            If Me.cboQrygbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(Me.cboTGrp)
                If sTGrpCd = "" Then sPartSlip = Ctrl.Get_Code(Me.cboSlip)
            Else
                sPartSlip = Ctrl.Get_Code(Me.cboSlip)
                sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
            End If

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_list(sPartSlip, sTGrpCd, sWGrpCd, , Ctrl.Get_Code(cboSpcCd))
            Dim a_dr As DataRow() = dt.Select("((tcdgbn in('P', 'B') AND titleyn = '1') OR titleyn = '0')", "")

            dt = Fn.ChangeToDataTable(a_dr)
            objHelp.FormText = "검사목록"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            If Me.txtSelTest.Text <> "" Then objHelp.KeyCodes = Me.txtSelTest.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmp", "출력명", 0, , , True)
            objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("tcdgbn", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("titleyn", "titleyn", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sTestCds As String = "", sTestNmds As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sTestCd As String = aryList.Item(ix).ToString.Split("|"c)(2)
                    Dim sTnmd As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sTestCds += "|" : sTestNmds += "|"
                    End If

                    sTestCds += sTestCd : sTestNmds += sTnmd
                Next

                Me.txtSelTest.Text = sTestNmds.Replace("|", ",")
                Me.txtSelTest.Tag = sTestCds + "^" + sTestNmds
            Else
                Me.txtSelTest.Text = ""
                Me.txtSelTest.Tag = ""
            End If

            sbDisplay_Test()

            COMMON.CommXML.setOneElementXML(msXMLDir, msTestFile, "TEST", Me.txtSelTest.Tag.ToString)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub cboQrygbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQrygbn.SelectedIndexChanged
        sbDisplay_Date_Setting()
        COMMON.CommXML.setOneElementXML(msXMLDir, msQryFile, "JOB", cboQrygbn.SelectedIndex.ToString)
    End Sub

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbDisplay_Spc()
        COMMON.CommXML.setOneElementXML(msXMLDir, msTgrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged

        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbClear_Form()

        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        If cboWkGrp.SelectedIndex >= 0 Then
            COMMON.CommXML.setOneElementXML(msXMLDir, msWkGrpFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)
        End If
    End Sub

    Private Sub axItemSave_ListDblClick(ByVal rsItemCds As String, ByVal rsItemNms As String)
        If rsItemCds <> "" Then
            Me.txtSelTest.Tag = rsItemCds + "^" + rsItemNms
            Me.txtSelTest.Text = rsItemNms.Replace("|", ",")

            sbDisplay_Test()
        End If
    End Sub


    Private Sub btnClear_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_test.Click
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""
    End Sub

    Private Sub chkMbtType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMbtType.CheckedChanged
        If chkMbtType.Checked Then
            Me.chkBar_cult.Enabled = True
        Else
            Me.chkBar_cult.Enabled = False : Me.chkBar_cult.Checked = False
        End If

    End Sub

    Private Sub chkPrttWL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrtWL.CheckedChanged, chkPrtWS.CheckedChanged
        If CType(sender, Windows.Forms.CheckBox).Checked Then
            If CType(sender, Windows.Forms.CheckBox).Name.ToLower = "chkprtwl" Then
                chkPrtWS.Checked = False
                Me.chkBar_view.Enabled = True
            Else
                chkPrtWL.Checked = False
                Me.chkBar_view.Enabled = False : Me.chkBar_view.Checked = False

            End If
        End If
    End Sub

    Private Sub chkPrtBar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrtBar.CheckedChanged, chkBar_cult.CheckedChanged
        If CType(sender, Windows.Forms.CheckBox).Checked Then
            If CType(sender, Windows.Forms.CheckBox).Name.ToLower = "chkprtbar" Then
                chkBar_cult.Checked = False
            Else
                chkPrtBar.Checked = False
            End If
        End If
    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbClear_Form()

        sbDisplay_WkGrp()
        sbDisplay_Date_Setting()
        sbDisplay_Spc()
        sbDisplay_wl()

        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", cboSlip.SelectedIndex.ToString)
    End Sub

    Private Sub cboSpcCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSpcCd.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXMLDir, msSpcFile, "SPC", cboSpcCd.SelectedIndex.ToString)
    End Sub

    Private Sub dtpWkLDt_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpWkLDt.CloseUp
        sbDisplay_wl()
    End Sub

    Private Sub cboWkList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWL.SelectedIndexChanged

        If Me.cboWL.SelectedIndex = 0 Then Return

        Dim sBuf() As String = Me.cboWL.Text.Split("|"c)

        If sBuf.Length > 3 Then
            Dim sWLtitle As String = sBuf(0).Trim
            Dim sWLYmd As String = sBuf(1).Trim
            Dim sWLUId As String = sBuf(2).Trim

            Me.dtpWkLDt.Value = CDate(sWLYmd.Insert(4, "-").Insert(7, "-"))

            sbDisplay_Data_wl(sWLUId, sWLYmd, sWLtitle)
        End If

    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click

        Try
            Dim sWLTitle As String = ""
            Dim sWLUId As String = ""
            Dim sWLYmd As String = ""

            If Me.cboWL.SelectedIndex = 0 Then
                sWLTitle = Me.txtSelTest.Text
                If sWLTitle = "" Then
                    If Me.cboQrygbn.Text = "작업그룹" Then
                        sWLTitle = Ctrl.Get_Name(Me.cboWkGrp)
                    Else
                        sWLTitle = Ctrl.Get_Name(Me.cboTGrp)
                    End If
                End If

                Dim objFrm As Windows.Forms.Form

                objFrm = New FGJ08_S01
                CType(objFrm, FGJ08_S01).WLTITLE = sWLTitle
                sWLTitle = ""
                objFrm.ShowDialog()

                If CType(objFrm, FGJ08_S01).ACTION.ToString <> "YES" Then Exit Sub
                sWLTitle = CType(objFrm, FGJ08_S01).txtWLTitle.Text
                sWLYmd = Me.dtpWkLDt.Text.Replace("-", "")
                sWLUId = "--"

            Else
                sWLTitle = Me.cboWL.Text.Split("|"c)(0)
                sWLYmd = Me.cboWL.Text.Split("|"c)(1)
                sWLUId = Me.cboWL.Text.Split("|"c)(2)
            End If

            If sWLTitle = "" Then
                MsgBox("W/L 제목을 입력하세요.!!")
                Return
            End If

            Dim alData As New ArrayList
            Dim sWLType As String = "L"

            If mbMicroBioYn Then sWLType = "M"

            With Me.spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                    .Col = .GetColFromID("spccd") : Dim sSpccd As String = .Text

                    Dim sTestCds As String = ""

                    For iCol As Integer = .GetColFromID("spcnmd") + 1 To .MaxCols
                        .Col = iCol : Dim sValue As String = .Text
                        .Col = iCol : Dim sTestCd As String = .ColID

                        If sValue <> "" Then
                            sTestCds += sTestCd + "^ "
                        End If
                    Next

                    alData.Add(sBcNo + "|" + sSpccd + "|" + sTestCds + "|")

                Next
            End With

            Dim bRet As Boolean = (New LISAPP.APP_WL.Reg).ExecuteDo(sWLUId, sWLYmd, sWLTitle, sWLType, alData)
            If bRet = False Then
                MsgBox("W/L 생성을 하지 못 했습니다.!!")
            Else
                sbDisplay_wl()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim sWLTitle As String = ""
            Dim sWLUId As String = ""
            Dim sWLYmd As String = ""

            If Me.cboWL.SelectedIndex < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "삭제할 W/L를 선택하세요.!!")
                Return
            Else
                sWLTitle = Me.cboWL.Text.Split("|"c)(0).Trim()
                sWLYmd = Me.cboWL.Text.Split("|"c)(1).Trim()
                sWLUId = Me.cboWL.Text.Split("|"c)(2).Trim()
            End If

            If sWLTitle = "" Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "W/L 제목을 입력하세요.!!")
                Return
            End If

            Dim sWLType As String = "L"
            If mbMicroBioYn Then sWLType = "M"

            Dim bRet As Boolean = (New LISAPP.APP_WL.Reg).DeleteDo(sWLUId, sWLYmd, sWLTitle, sWLType)
            If bRet = False Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "W/L 삭제를 하지 못 했습니다.!!")
            Else
                sbDisplay_wl()
                Me.spdList.MaxRows = 0
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick
        With Me.spdList
            If e.col = .GetColFromID("wlseq") Then Return

            .Row = e.row
            .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text

            If MsgBox("검체번호[" + sBcNo + "]를 화면에서 삭제 하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Me.Text) = MsgBoxResult.No Then Return

            .Row = e.row
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

        End With
    End Sub

    Private Sub spdList_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdList.KeyDownEvent
        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        With spdList
            If .ActiveCol <> .GetColFromID("wlseq") Then Return

            Dim iRow As Integer = .ActiveRow
            Dim iCol As Integer = .ActiveCol

            .Row = iRow : .Col = iCol : Dim sValue As String = .Text

            If IsNumeric(sValue) = False Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "수치값만 입력 가능합니다.!!")
                .Row = iRow : .Col = iCol : .Text = ""
                Return
            End If

            If MsgBox("일괄 적용하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "정렬순서") = MsgBoxResult.No Then Return

            Dim iSeq As Integer = Convert.ToInt16(sValue)

            For ix As Integer = iRow + 1 To .MaxRows

                iSeq += 1

                .Row = ix
                .Col = .GetColFromID("wlseq") : .Text = iSeq.ToString
            Next

        End With

    End Sub

    Private Sub axItemSave_ListDblClick1(ByVal rsItemCds As String, ByVal rsItemNms As String) Handles axItemSave.ListDblClick
        If rsItemCds <> "" Then
            Me.txtSelTest.Tag = rsItemCds + "^" + rsItemNms
            Me.txtSelTest.Text = rsItemNms.Replace("|", ",")

            sbDisplay_Test()
        End If
    End Sub
End Class

Public Class FGJ08_PATINFO
    Public sPrtBcNo As String = ""

    Public alItem As New ArrayList

    Public sTNms As String = ""
    Public sTCds As String = ""
    Public sRsts As String = ""
End Class

Public Class FGJ08_PRINT
    Private Const msFile As String = "File : FGJ08.vb, Class : LISJ.FGJ08_PRINT" + vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0
    Private miTitle_ExmCnt As Integer = 0
    Private miCCol As Integer = 1
    Public mbLandscape As Boolean = False

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msgExmWidth As Single
    Public msTitle As String
    Public maPrtData As ArrayList
    Public msJobGbn As String = ""
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")
    Public miTotExmCnt As Integer = 0
    Public miTitleCnt As Integer = 0
    Public mbUseBarNo As Boolean = False
    Public msTitle_sub_right_1 As String = ""

    Public Sub sbPrint_Preview(ByVal rbFixed As Boolean)
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = mbLandscape
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                If mbUseBarNo Then
                    If rbFixed Then
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_Fixed_barno
                    Else
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_barno
                    End If
                    AddHandler prtR.BeginPrint, AddressOf sbPrintData
                    AddHandler prtR.EndPrint, AddressOf sbReport
                Else
                    If rbFixed Then
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_Fixed
                    Else
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage
                    End If
                    AddHandler prtR.BeginPrint, AddressOf sbPrintData
                    AddHandler prtR.EndPrint, AddressOf sbReport
                End If

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

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then


                prtR.DocumentName = "ACK_" + msTitle
                If mbUseBarNo Then
                    If rbFixed Then
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_Fixed_barno
                    Else
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_barno
                    End If
                Else
                    If rbFixed Then
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_Fixed
                    Else
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage
                    End If
                End If
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
        miCCol = 1
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
        Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        Dim sngTmp As Single = 0
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGJ08_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGJ08_PATINFO).alItem(ix).ToString.Split("^"c)(2))
        Next

        If sngTmp + 40 > msgWidth Then Return

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.2)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            For ix As Integer = 1 To CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem.Count
                rect = New Drawing.RectangleF(msgPosX(ix), sngPosY, msgPosX(ix + 1) - msgPosX(ix), sngPrtH)
                Dim strTmp As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(ix - 1).ToString.Split("^"c)(0)

                e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).sTNms.Split("|"c)
            Dim strRst() As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).sRsts.Split("|"c)

            Dim intCol As Integer = 0
            For intIx1 As Integer = 0 To strTnm.Length - 2
                intCol += 1
                If intCol > miTitle_ExmCnt Then
                    intCol = 1
                    sngPosY += sngPrtH

                    e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(miTitleCnt), sngPosY, msgWidth, sngPosY)
                End If

                If msgHeight < sngPosY + sngPrtH * 3 Then Exit For

                '-- 검사명
                rect = New Drawing.RectangleF(msgPosX(miTitleCnt + intCol - 1), sngPosY + sngPrtH * 0, msgPosX(miTitleCnt + intCol) - msgPosX(miTitleCnt + intCol - 1), sngPrtH)
                e.Graphics.DrawString(strTnm(intIx1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            sngPosY += sngPrtH
            If msgHeight < sngPosY + sngPrtH * 3 Then miCIdx += 1 : Exit For

            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY, msgWidth, sngPosY)

            miCIdx += 1
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
        Dim sngTmp As Single

        miTitleCnt = CType(maPrtData.Item(0), FGJ08_PATINFO).alItem.Count + 1

        Dim sngPosX(0 To 1) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40

        For ix As Integer = 1 To miTitleCnt - 1
            ReDim Preserve sngPosX(ix + 1)

            sngPosX(ix + 1) = sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), FGJ08_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))
        Next

        sngTmp = msgWidth - msgLeft - sngPosX(sngPosX.Length - 1)
        intCnt = Convert.ToInt16(sngTmp / msgExmWidth)
        If intCnt * msgExmWidth > sngTmp Then intCnt -= 1
        miTitle_ExmCnt = intCnt

        For ix As Integer = 1 To intCnt + 1
            ReDim Preserve sngPosX(miTitleCnt + ix)
            sngPosX(miTitleCnt + ix) = sngPosX(miTitleCnt + ix - 1) + msgExmWidth
        Next

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * msTitle_sub_right_1.Length, sngPosY + 20, msgWidth - 8 * msTitle_sub_right_1.Length, sngPrt), sf_l)
        End If

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = Convert.ToSingle(fnt_Title.GetHeight(e.Graphics) * 1.5)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6), sngPosY, msgWidth - 8 * (msTitle_Time.Length + 6), sngPrt), sf_l)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt), sf_l)

        For ix As Integer = 1 To CType(maPrtData.Item(0), FGJ08_PATINFO).alItem.Count

            Dim strTmp As String = CType(maPrtData.Item(0), FGJ08_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(ix), sngPosY + sngPrt * 0, sngPosX(ix + 1) - sngPosX(ix), sngPrt), sf_l)
        Next

        e.Graphics.DrawString("검사항목", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(miTitleCnt), sngPosY + sngPrt * 0, msgWidth - sngPosX(miTitleCnt), sngPrt), sf_c)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY, msgWidth, sngPosY)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt, msgWidth, sngPosY + sngPrt)

        msgPosX = sngPosX
        Return sngPosY + sngPrt

    End Function

    Public Overridable Sub sbPrintPage_Fixed(ByVal sender As Object, ByVal e As PrintPageEventArgs)

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

        Dim sngTmp As Single = 0
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGJ08_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGJ08_PATINFO).alItem(ix).ToString.Split("^"c)(2))
        Next

        If sngTmp + 40 > msgWidth Then Return

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF
        Dim intCnt As Integer = 0

        If miCIdx = 0 Then miPageNo = 0

        Dim intCol As Integer = miCCol
        Dim intLine As Integer = 0

        For intCol = miCCol To miTotExmCnt Step miTitle_ExmCnt

            For intIdx As Integer = miCIdx To maPrtData.Count - 1
                If sngPosY = 0 Then
                    sngPosY = fnPrtTitle_Fixed(e, CType(maPrtData.Item(intIdx), FGJ08_PATINFO).sTNms.Split("|"c), miCCol)
                End If

                '-- 번호
                rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * intLine, msgPosX(1) - msgPosX(0), sngPrtH)
                e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

                For ix As Integer = 1 To CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem.Count
                    rect = New Drawing.RectangleF(msgPosX(ix), sngPosY + sngPrtH * intLine, msgPosX(ix + 1) - msgPosX(ix), sngPrtH)
                    Dim strTmp As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(ix - 1).ToString.Split("^"c)(0)

                    e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                Next

                Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).sTNms.Split("|"c)
                Dim strRst() As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).sRsts.Split("|"c)

                intCnt = 0 : Dim intTitleCnt As Integer = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem.Count + 1

                For intIx1 As Integer = miCCol To miCCol + miTitle_ExmCnt
                    intCnt += 1
                    If intCnt > miTitle_ExmCnt + 1 Or intIx1 > miTotExmCnt Then
                        Exit For
                    End If

                    If intIx1 > strRst.Length Then
                        Exit For
                    End If

                    '-- 이전검사결과
                    rect = New Drawing.RectangleF(msgPosX(intTitleCnt + intCnt - 1), sngPosY + sngPrtH * intLine, msgPosX(intTitleCnt + intCnt) - msgPosX(intTitleCnt + intCnt - 1), sngPrtH)
                    e.Graphics.DrawString(strRst(intIx1 - 1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                Next

                'sngPosY += sngPrtH
                intLine += 1
                If msgHeight - sngPrtH * 3 < sngPosY + sngPrtH * intLine Then miCIdx += 1 : Exit For
                e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)

                miCIdx += 1

            Next

            If miCIdx >= maPrtData.Count Then
                miCCol += miTitle_ExmCnt + 1
                If miCCol < miTotExmCnt Then miCIdx = 0
            End If

            Exit For

        Next
        miPageNo += 1

        '-- 세로라인
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sngPosY, msgPosX(ix), msgHeight - sngPrtH * 2)
        Next

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2, msgWidth, msgHeight - sngPrtH * 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

        If miCIdx < maPrtData.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle_Fixed(ByVal e As PrintPageEventArgs, ByRef rsExmNm() As String, ByVal riColS As Integer) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1
        Dim sngTmp As Single = 0

        miTitleCnt = CType(maPrtData.Item(0), FGJ08_PATINFO).alItem.Count + 1

        Dim sngPosX(0 To 1) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40

        For ix As Integer = 1 To miTitleCnt - 1
            ReDim Preserve sngPosX(ix + 1)

            sngPosX(ix + 1) = sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), FGJ08_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))
        Next

        sngTmp = msgWidth - msgLeft - sngPosX(sngPosX.Length - 1)

        intCnt = Convert.ToInt16(sngTmp / msgExmWidth)
        If intCnt * msgExmWidth > sngTmp Then intCnt -= 1
        miTitle_ExmCnt = intCnt

        'MsgBox(sngPosX.Length.ToString)

        For ix As Integer = 1 To miTitle_ExmCnt + 1
            ReDim Preserve sngPosX(miTitleCnt + ix)
            sngPosX(miTitleCnt + ix) = sngPosX(miTitleCnt + ix - 1) + msgExmWidth
        Next

        sngPosX(sngPosX.Length - 1) = msgWidth

        msgPosX = sngPosX

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
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력정보
        If msTitle_sub_right_1.Length > msTitle_Time.Length + 6 Then
            msTitle_Time = msTitle_Time.PadRight(msTitle_sub_right_1.Length - 6)
        Else
            msTitle_sub_right_1 = msTitle_sub_right_1.PadRight(msTitle_Time.Length + 6)
        End If

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * msTitle_sub_right_1.Length, sngPosY - 20, msgWidth - msgLeft - 25, sngPrt), sf_l)
        End If

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6), sngPosY, msgWidth - 8 * (msTitle_Time.Length + 6), sngPrt), sf_l)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt * 2), sf_l)

        For ix As Integer = 1 To CType(maPrtData.Item(0), FGJ08_PATINFO).alItem.Count

            Dim strTmp As String = CType(maPrtData.Item(0), FGJ08_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(ix), sngPosY, sngPosX(ix + 1) - sngPosX(ix), sngPrt * 2), sf_l)
        Next

        intCnt = 0

        For intIdx As Integer = riColS To riColS + miTitle_ExmCnt
            If intIdx > miTotExmCnt Then Exit For

            If intIdx > rsExmNm.Length Then
                Exit For
            End If
            e.Graphics.DrawString(rsExmNm(intIdx - 1), fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(miTitleCnt + intCnt), sngPosY, sngPosX(miTitleCnt + 1 + intCnt) - sngPosX(miTitleCnt + intCnt), sngPrt * 2), sf_l)
            intCnt += 1
        Next

        '-- 세로라인
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sngPosY - sngPrt / 2, msgPosX(ix), sngPosY + sngPrt * 2)
        Next

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt * 2, msgWidth, sngPosY + sngPrt * 2)

        msgPosX = sngPosX

        Return sngPosY + sngPrt * 2


    End Function


    Public Overridable Function fnPrtTitle_Barno(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1
        Dim sngTmp As Single

        sngTmp = msgWidth - msgLeft - 540
        intCnt = Convert.ToInt16(sngTmp / msgExmWidth)
        If intCnt * msgExmWidth > sngTmp Then intCnt -= 1
        miTitle_ExmCnt = intCnt

        Dim sngPosX(0 To intCnt + 6) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40
        sngPosX(2) = sngPosX(1) + 195
        sngPosX(3) = sngPosX(2) + 140
        sngPosX(4) = sngPosX(3) + 80
        sngPosX(5) = sngPosX(4) + 120
        For intIdx As Integer = 6 To intCnt + 5
            sngPosX(intIdx) = sngPosX(intIdx - 1) + msgExmWidth
        Next
        sngPosX(sngPosX.Length - 1) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = fnt_Head.GetHeight(e.Graphics)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - msgLeft - 25, sngPrt), sf_r)

        sngPosY += sngPrt * 2

        fnPrtTitle_Barno = sngPosY + sngPrt * 4 + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY + sngPrt * 0, sngPosX(1) - sngPosX(0), sngPrt), sf_c)
        e.Graphics.DrawString("BarCode", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY + sngPrt * 0, sngPosX(2) - sngPosX(1), sngPrt), sf_c)

        e.Graphics.DrawString("등록번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 0, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("성명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 0, sngPosX(4) - sngPosX(3), sngPrt), sf_l)
        e.Graphics.DrawString("진료과/병동", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY + sngPrt * 0, sngPosX(5) - sngPosX(4), sngPrt), sf_l)

        e.Graphics.DrawString("작업번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 1, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("성별/나이", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 1, sngPosX(4) - sngPosX(3), sngPrt), sf_l)
        e.Graphics.DrawString("검체", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY + sngPrt * 1, sngPosX(5) - sngPosX(4), sngPrt), sf_l)

        e.Graphics.DrawString("검체번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 2, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("의사 Remark", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 2, sngPosX(5) - sngPosX(3), sngPrt), sf_l)

        e.Graphics.DrawString("검사항목", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5), sngPosY + sngPrt * 0, msgWidth - sngPosX(5), sngPrt), sf_c)

        e.Graphics.DrawString("진단명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 3, sngPosX(5) - sngPosX(2), sngPrt), sf_l)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt * 4, msgWidth, sngPosY + sngPrt * 4)

        msgPosX = sngPosX

    End Function

    Public Overridable Sub sbPrintPage_barno(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim iPage As Integer = 0
        Dim sgPosY As Single = 0
        Dim sgPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
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

        sgPrtH = fnt_Body.GetHeight(e.Graphics)

        Dim rect As New Drawing.RectangleF

        For ix As Integer = miCIdx To maPrtData.Count - 1
            If sgPosY = 0 Then
                sgPosY = fnPrtTitle_Barno(e)
            End If

            Dim sBcNo As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(0).ToString.Split("^"c)(0)
            Dim sWorkNo As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(1).ToString.Split("^"c)(0)
            Dim sRegNo As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(2).ToString.Split("^"c)(0)
            Dim sPatNm As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(3).ToString.Split("^"c)(0)
            Dim sSexAge As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(4).ToString.Split("^"c)(0)
            Dim sDeptWard As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(6).ToString.Split("^"c)(0)
            Dim sDoctorRmk As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(7).ToString.Split("^"c)(0)
            Dim sDiagNm As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(8).ToString.Split("^"c)(0)
            Dim sSpcNmd As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(9).ToString.Split("^"c)(0)
            Dim sPrtBcNo As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).alItem(10).ToString.Split("^"c)(0)

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sgPosY + sgPrtH * 0, msgPosX(1) - msgPosX(0), sgPrtH * 3)
            'Dim strWkNo As String = sWorkNo.Substring(sWorkNo.Length - 4)

            'If msJobGbn = "작업그룹" Then
            '    e.Graphics.DrawString(strWkNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            'Else
            e.Graphics.DrawString((ix + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            'End If
            '-- 바코드
            rect = New Drawing.RectangleF(msgPosX(1), sgPosY + sgPrtH * 0, msgPosX(2) - msgPosX(1), sgPrtH * 3 - sgPrtH / 2)
            e.Graphics.DrawString("*" + sPrtBcNo + "*", fnt_BarCd, Drawing.Brushes.Black, rect, sf_c)

            '-- 바코드-문자
            rect = New Drawing.RectangleF(msgPosX(1), sgPosY + sgPrtH * 2 + sgPrtH / 2, msgPosX(2) - msgPosX(1), sgPrtH / 2)
            e.Graphics.DrawString(sPrtBcNo, fnt_BarCd_Str, Drawing.Brushes.Black, rect, sf_c)

            '-- 등록번호
            rect = New Drawing.RectangleF(msgPosX(2), sgPosY + sgPrtH * 0, msgPosX(3) - msgPosX(2), sgPrtH)
            e.Graphics.DrawString(sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 성명
            rect = New Drawing.RectangleF(msgPosX(3), sgPosY + sgPrtH * 0, msgPosX(4) - msgPosX(3), sgPrtH)
            e.Graphics.DrawString(sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 진료과/병동
            rect = New Drawing.RectangleF(msgPosX(4), sgPosY + sgPrtH * 0, msgPosX(5) - msgPosX(4), sgPrtH)
            e.Graphics.DrawString(sDeptWard, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 작업번호
            rect = New Drawing.RectangleF(msgPosX(2), sgPosY + sgPrtH * 1, msgPosX(3) - msgPosX(2), sgPrtH)
            e.Graphics.DrawString(sWorkNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 성별/나이
            rect = New Drawing.RectangleF(msgPosX(3), sgPosY + sgPrtH * 1, msgPosX(4) - msgPosX(3), sgPrtH)
            e.Graphics.DrawString(sSexAge, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 검체명
            rect = New Drawing.RectangleF(msgPosX(4), sgPosY + sgPrtH * 1, msgPosX(5) - msgPosX(4), sgPrtH)
            e.Graphics.DrawString(sSpcNmd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 검체번호
            rect = New Drawing.RectangleF(msgPosX(2), sgPosY + sgPrtH * 2, msgPosX(3) - msgPosX(2), sgPrtH)
            e.Graphics.DrawString(sBcNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 의사 Remark
            rect = New Drawing.RectangleF(msgPosX(3), sgPosY + sgPrtH * 2, msgPosX(5) - msgPosX(3), sgPrtH)
            e.Graphics.DrawString(sDoctorRmk, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 진단명
            rect = New Drawing.RectangleF(msgPosX(2), sgPosY + sgPrtH * 3, msgPosX(5) - msgPosX(2), sgPrtH)
            e.Graphics.DrawString(sDiagNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            Dim strTnm() As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).sTNms.Split("|"c)
            Dim strRst() As String = CType(maPrtData.Item(ix), FGJ08_PATINFO).sRsts.Split("|"c)

            Dim iCol As Integer = 0
            For ix2 As Integer = 0 To strTnm.Length - 2
                iCol += 1
                If iCol > miTitle_ExmCnt Then
                    iCol = 1
                    sgPosY += sgPrtH * 3 + sgPrtH / 2

                    e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(5), sgPosY - sgPrtH / 2, msgWidth, sgPosY - sgPrtH / 2)
                End If

                If msgHeight - sgPrtH * 7 < sgPosY Then Exit For

                '-- 검사명
                rect = New Drawing.RectangleF(msgPosX(4 + iCol), sgPosY + sgPrtH * 0, msgPosX(5 + iCol) - msgPosX(4 + iCol), sgPrtH)
                e.Graphics.DrawString(strTnm(ix2), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                '-- 이전검사결과
                rect = New Drawing.RectangleF(msgPosX(4 + iCol), sgPosY + sgPrtH * 1, msgPosX(5 + iCol) - msgPosX(4 + iCol), sgPrtH)
                e.Graphics.DrawString(strRst(ix2), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            Next

            sgPosY += sgPrtH * 4 + sgPrtH / 2
            If msgHeight - sgPrtH * 7 < sgPosY Then miCIdx += 1 : Exit For

            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY - sgPrtH / 2, msgWidth, sgPosY - sgPrtH / 2)

            miCIdx += 1
        Next


        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sgPrtH * 2 - sgPrtH / 2, msgWidth, msgHeight - sgPrtH * 2 - sgPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sgPrtH * 2, msgWidth - msgLeft - 25, sgPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sgPrtH * 2, msgWidth - msgLeft - 25, sgPrtH), sf_c)

        If miCIdx < maPrtData.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle_Fixed_barno(ByVal e As PrintPageEventArgs, ByRef rsExmNm() As String, ByVal riColS As Integer) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1

        Dim sngPosX(0 To miTitle_ExmCnt + 6) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40
        sngPosX(2) = sngPosX(1) + 195
        sngPosX(3) = sngPosX(2) + 140
        sngPosX(4) = sngPosX(3) + 80
        sngPosX(5) = sngPosX(4) + 120
        For intIdx As Integer = 6 To miTitle_ExmCnt + 5
            sngPosX(intIdx) = sngPosX(intIdx - 1) + msgExmWidth
        Next
        sngPosX(sngPosX.Length - 1) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = fnt_Head.GetHeight(e.Graphics)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - msgLeft - 25, sngPrt), sf_r)

        sngPosY += sngPrt * 2

        fnPrtTitle_Fixed_barno = sngPosY + sngPrt * 4 + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY + sngPrt * 0, sngPosX(1) - sngPosX(0), sngPrt), sf_c)
        e.Graphics.DrawString("BarCode", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY + sngPrt * 0, sngPosX(2) - sngPosX(1), sngPrt), sf_c)

        e.Graphics.DrawString("등록번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 0, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("성명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 0, sngPosX(4) - sngPosX(3), sngPrt), sf_l)
        e.Graphics.DrawString("진료과/병동", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY + sngPrt * 0, sngPosX(5) - sngPosX(4), sngPrt), sf_l)

        e.Graphics.DrawString("작업번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 1, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("성별/나이", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 1, sngPosX(4) - sngPosX(3), sngPrt), sf_l)
        e.Graphics.DrawString("검체", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY + sngPrt * 1, sngPosX(5) - sngPosX(4), sngPrt), sf_l)

        e.Graphics.DrawString("검체번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 2, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("의사 Remark", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 2, sngPosX(5) - sngPosX(3), sngPrt), sf_l)

        e.Graphics.DrawString("진단명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 3, sngPosX(5) - sngPosX(2), sngPrt), sf_l)

        intCnt = 0

        For intIdx As Integer = riColS To riColS + miTitle_ExmCnt - 1
            If intIdx > miTotExmCnt Then Exit For

            If intIdx > rsExmNm.Length Then
                Exit For
            End If
            e.Graphics.DrawString(rsExmNm(intIdx - 1), fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5 + intCnt), sngPosY + sngPrt * 0, sngPosX(6 + intCnt) - sngPosX(5 + +intCnt), sngPrt * 3), sf_l)
            intCnt += 1
        Next

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt * 4, msgWidth, sngPosY + sngPrt * 4)

        msgPosX = sngPosX

    End Function


    Public Overridable Sub sbPrintPage_Fixed_barno(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim iPage As Integer = 0
        Dim sgPosY As Single = 0
        Dim sgPrtH As Single = 0

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

        sgPrtH = fnt_Body.GetHeight(e.Graphics)

        Dim rect As New Drawing.RectangleF

        Dim sgTmp As Single = 0
        Dim iCnt As Integer = 0

        sgTmp = msgWidth - msgLeft - 540
        iCnt = Convert.ToInt16(sgTmp / msgExmWidth)
        If iCnt * msgExmWidth > sgTmp Then iCnt -= 1
        miTitle_ExmCnt = iCnt

        If miCIdx = 0 Then miPageNo = 0

        Dim iCol As Integer = miCCol
        For iCol = miCCol To miTotExmCnt Step miTitle_ExmCnt

            For intIdx As Integer = miCIdx To maPrtData.Count - 1
                Dim sBcNo As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(0).ToString.Split("^"c)(0)
                Dim sWorkNo As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(1).ToString.Split("^"c)(0)
                Dim sRegNo As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(2).ToString.Split("^"c)(0)
                Dim sPatNm As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(3).ToString.Split("^"c)(0)
                Dim sSexAge As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(4).ToString.Split("^"c)(0)
                Dim sDeptWard As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(6).ToString.Split("^"c)(0)
                Dim sDoctorRmk As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(7).ToString.Split("^"c)(0)
                Dim sDiagNm As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(8).ToString.Split("^"c)(0)
                Dim sSpcNmd As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(9).ToString.Split("^"c)(0)
                Dim sPrtBcNo As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).alItem(10).ToString.Split("^"c)(0)

                If sgPosY = 0 Then
                    sgPosY = fnPrtTitle_Fixed_barno(e, CType(maPrtData.Item(intIdx), FGJ08_PATINFO).sTNms.Split("|"c), miCCol)
                End If

                '-- 번호
                rect = New Drawing.RectangleF(msgPosX(0), sgPosY + sgPrtH * 0, msgPosX(1) - msgPosX(0), sgPrtH * 3)

                Dim strWkNo As String = ""
                If sWorkNo.Length > 0 Then
                    strWkNo = sWorkNo.Substring(sWorkNo.Length - 4)
                End If


                'If msJobGbn = "작업그룹" Then
                '    e.Graphics.DrawString(strWkNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                'Else
                e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                'End If

                '-- 바코드
                rect = New Drawing.RectangleF(msgPosX(1), sgPosY + sgPrtH * 0, msgPosX(2) - msgPosX(1), sgPrtH * 3)
                e.Graphics.DrawString("*" & sPrtBcNo & "*", fnt_BarCd, Drawing.Brushes.Black, rect, sf_c)

                '-- 바코드-문자
                rect = New Drawing.RectangleF(msgPosX(1), sgPosY + sgPrtH * 2 + sgPrtH / 2, msgPosX(2) - msgPosX(1), sgPrtH / 2)
                e.Graphics.DrawString(sPrtBcNo, fnt_BarCd_Str, Drawing.Brushes.Black, rect, sf_c)

                '-- 등록번호
                rect = New Drawing.RectangleF(msgPosX(2), sgPosY + sgPrtH * 0, msgPosX(3) - msgPosX(2), sgPrtH)
                e.Graphics.DrawString(sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 성명
                rect = New Drawing.RectangleF(msgPosX(3), sgPosY + sgPrtH * 0, msgPosX(4) - msgPosX(3), sgPrtH)
                e.Graphics.DrawString(sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 진료과/병동
                rect = New Drawing.RectangleF(msgPosX(4), sgPosY + sgPrtH * 0, msgPosX(5) - msgPosX(4), sgPrtH)
                e.Graphics.DrawString(sDeptWard, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                '-- 작업번호
                rect = New Drawing.RectangleF(msgPosX(2), sgPosY + sgPrtH * 1, msgPosX(3) - msgPosX(2), sgPrtH)
                e.Graphics.DrawString(sWorkNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 성별/나이
                rect = New Drawing.RectangleF(msgPosX(3), sgPosY + sgPrtH * 1, msgPosX(4) - msgPosX(3), sgPrtH)
                e.Graphics.DrawString(sSexAge, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 검체명
                rect = New Drawing.RectangleF(msgPosX(4), sgPosY + sgPrtH * 1, msgPosX(5) - msgPosX(4), sgPrtH)
                e.Graphics.DrawString(sSpcNmd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                '-- 검체번호
                rect = New Drawing.RectangleF(msgPosX(2), sgPosY + sgPrtH * 2, msgPosX(3) - msgPosX(2), sgPrtH)
                e.Graphics.DrawString(sBcNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                '-- 의사 Remark
                rect = New Drawing.RectangleF(msgPosX(3), sgPosY + sgPrtH * 2, msgPosX(5) - msgPosX(3), sgPrtH)
                e.Graphics.DrawString(sDoctorRmk, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                '-- 진단명
                rect = New Drawing.RectangleF(msgPosX(2), sgPosY + sgPrtH * 3, msgPosX(5) - msgPosX(2), sgPrtH)
                e.Graphics.DrawString(sDiagNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).sTNms.Split("|"c)
                Dim strRst() As String = CType(maPrtData.Item(intIdx), FGJ08_PATINFO).sRsts.Split("|"c)

                iCnt = 0
                For ix As Integer = miCCol To miCCol + miTitle_ExmCnt
                    iCnt += 1
                    If iCnt > miTitle_ExmCnt Or ix > miTotExmCnt Then
                        Exit For
                    End If

                    If ix > strRst.Length Then
                        Exit For
                    End If
                    ''-- 검사명
                    'rect = New Drawing.RectangleF(msgPosX(4 + iCnt), sgPosY + sgPrtH * 0, msgPosX(5 + iCnt) - msgPosX(4 + iCnt), sgPrtH)
                    'e.Graphics.DrawString(strTnm(ix - 1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                    '-- 이전검사결과
                    rect = New Drawing.RectangleF(msgPosX(4 + iCnt), sgPosY + sgPrtH * 0, msgPosX(5 + iCnt) - msgPosX(4 + iCnt), sgPrtH)
                    e.Graphics.DrawString(strRst(ix - 1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                Next

                sgPosY += sgPrtH * 4 + sgPrtH / 2
                If msgHeight - sgPrtH * 6 < sgPosY Then miCIdx += 1 : Exit For

                e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY - sgPrtH / 2, msgWidth, sgPosY - sgPrtH / 2)

                miCIdx += 1
            Next

            If miCIdx >= maPrtData.Count Then
                miCCol += miTitle_ExmCnt
                If miCCol < miTotExmCnt Then miCIdx = 0

            End If

            Exit For

        Next
        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sgPrtH * 2 - sgPrtH / 2, msgWidth, msgHeight - sgPrtH * 2 - sgPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sgPrtH * 2, msgWidth - msgLeft - 25, sgPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sgPrtH * 2, msgWidth - msgLeft - 25, sgPrtH), sf_c)

        If miCIdx < maPrtData.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub
End Class