'>>> 결과대장 조회

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.APP_S.RstSrh

Public Class FGS15
    Inherits System.Windows.Forms.Form

    Private Const msXMLDir As String = "\XML"
    Private msSlipFile As String = Application.StartupPath + msXMLDir + "\FGS15_SLIP.XML"
    Private msTGrpFile As String = Application.StartupPath + msXMLDir + "\FGS15_TGRP.XML"
    Private msWGrpFile As String = Application.StartupPath + msXMLDir + "\FGS15_WGRP.XML"
    Private msTESTFile As String = Application.StartupPath + msXMLDir + "\FGS15_TEST.XML"
    Private msSPCFile As String = Application.StartupPath + msXMLDir + "\FGS15_SPC.XML"
    Private msQryFile As String = Application.StartupPath + msXMLDir + "\FGS15_Qry.XML"
    Private msTERMFile As String = Application.StartupPath + msXMLDir + "\FGS15_TREM.XML"

    Private mbMicroBioYn As Boolean = False

#Region " Form내부 함수 "
    Private Sub sbDisp_Init()
        Try

            Me.txtBcNo.Text = ""
            With spdList
                .Col = .GetColFromID("prtbcno") : .ColHidden = True
                .MaxRows = 0
            End With

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

            Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
            Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")

            sbDisplay_Slip()    '-- 검사분야 
            sbDisplay_TGrp()    '-- 검사그룹

            Dim sTgrp As String = "", sWkGrp As String = "", sJob As String = "", sTerm As String = "", sTestCds As String = "", sSpc As String

            sTgrp = COMMON.CommXML.getOneElementXML(msXMLDir, msTGrpFile, "TGRP")
            sWkGrp = COMMON.CommXML.getOneElementXML(msXMLDir, msWGrpFile, "WKGRP")
            sJob = COMMON.CommXML.getOneElementXML(msXMLDir, msQryFile, "JOB")
            sTerm = COMMON.CommXML.getOneElementXML(msXMLDir, msTERMFile, "TERM")
            sTestCds = COMMON.CommXML.getOneElementXML(msXMLDir, msTESTFile, "TEST")
            sSpc = COMMON.CommXML.getOneElementXML(msXMLDir, msSPCFile, "SPC")

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
            Me.dtpDateS.Focus()
            '< 20121012 조회구간 결과일자로 미생물일때 명칭 수정 
            If mbMicroBioYn = True Then
                lblTitleDt.Text = "결과일자"
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    ' 화면 정리
    Private Sub sbClear_Form()
        Me.spdList.MaxRows = 0
    End Sub

    Private Sub sbDisplay_Slip()

        Try
            Dim dt As New DataTable

            If mbMicroBioYn Then
                dt = LISAPP.COMM.CdFn.fnGet_Slip_List(, True, False, mbMicroBioYn)
            Else
                dt = LISAPP.COMM.CdFn.fnGet_Slip_List(, True, True, mbMicroBioYn)
            End If

            Me.cboSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msSlipFile, "SLIP")

            If sTmp = "" Or Val(sTmp) > cboSlip.Items.Count Then
                Me.cboSlip.SelectedIndex = 0
            Else
                Me.cboSlip.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_TGrp() ''' 검사그룹조회 

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_List(False, mbMicroBioYn)

            Me.cboTGrp.Items.Clear()
            Me.cboTGrp.Items.Add("[  ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboTGrp.Items.Add("[" + dt.Rows(ix).Item("tgrpcd").ToString + "] " + dt.Rows(ix).Item("tgrpnmd").ToString)
            Next
            If Me.cboTGrp.Items.Count > 0 Then Me.cboTGrp.SelectedIndex = 0
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_WkGrp()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_WKGrp_List(Ctrl.Get_Code(cboSlip))

            Me.cboWkGrp.Items.Clear()

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboWkGrp.Items.Add("[" + dt.Rows(ix).Item("wkgrpcd").ToString + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString)
            Next

            If Me.cboWkGrp.Items.Count > 0 Then Me.cboWkGrp.SelectedIndex = 0
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Spc()

        Try
            Dim sPartCd As String = ""
            Dim sSlipCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sWGrpCd As String = ""

            If cboQrygbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(cboTGrp)
            Else
                If Ctrl.Get_Code(cboSlip) <> "" Then
                    sPartCd = Ctrl.Get_Code(cboSlip).Substring(0, 1)
                    sSlipCd = Ctrl.Get_Code(cboSlip).Substring(1, 1)
                End If
                If cboQrygbn.Text = "검사그룹" Then sWGrpCd = Ctrl.Get_Code(cboWkGrp)
            End If

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List("", sPartCd, sSlipCd, sTGrpCd, sWGrpCd, "", "")

            Me.cboSpcCd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSpcCd.Items.Add("[" + dt.Rows(ix).Item("spccd").ToString.Trim + "] " + dt.Rows(ix).Item("spcnmd").ToString.Trim)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Test()
        If Me.txtSelTest.Text = "" And Ctrl.Get_Code(Me.cboTGrp) <> "" Then
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_Test_List(Ctrl.Get_Code(Me.cboTGrp), Ctrl.Get_Code(Me.cboSpcCd))

            For ix As Integer = 0 To dt.Rows.Count - 1
                With spdList
                    .Row = 0
                    'If .GetColFromID("orddt") + ix + 1 > .MaxCols Then
                    '    .MaxCols += 1
                    'End If

                    '.Col = .GetColFromID("orddt") + ix + 1 : .Text = dt.Rows(ix).Item("tnmd").ToString
                    '.Col = .GetColFromID("orddt") + ix + 1 : .ColID = dt.Rows(ix).Item("testcd").ToString
                    '20210201 JHS 보고자 보고일시 컬럼 추가 로 인한 수정
                    'If .GetColFromID("tkdt") + ix + 1 > .MaxCols Then
                    '    .MaxCols += 1
                    'End If


                    If .GetColFromID("fndt") + ix + 1 > .MaxCols Then
                        .MaxCols += 1
                    End If

                    '.Col = .GetColFromID("tkdt") + ix + 1 : .Text = dt.Rows(ix).Item("tnmd").ToString
                    '.Col = .GetColFromID("tkdt") + ix + 1 : .ColID = dt.Rows(ix).Item("testcd").ToString


                    .Col = .GetColFromID("fndt") + ix + 1 : .Text = dt.Rows(ix).Item("tnmd").ToString
                    .Col = .GetColFromID("fndt") + ix + 1 : .ColID = dt.Rows(ix).Item("testcd").ToString

                    '----------------------------------------------





                End With
            Next
        ElseIf Me.txtSelTest.Text <> "" Then
            Dim strBuf_Cd() As String = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Split("|"c)
            Dim strBuf_Nm() As String = Me.txtSelTest.Tag.ToString.Split("^"c)(1).Split("|"c)

            'spdList.MaxCols = spdList.GetColFromID("orddt") + 1
            ' spdList.MaxCols = spdList.GetColFromID("tkdt") + 1
            '20210201 JHS 보고일자 추가로 인한 수정
            spdList.MaxCols = spdList.GetColFromID("fndt") + 1
            '----------------------------------------------

            For ix As Integer = 0 To strBuf_Cd.Length - 1
                With spdList
                    .Row = 0
                    'If .GetColFromID("orddt") + ix + 1 > .MaxCols Then
                    '    .MaxCols += 1
                    'End If

                    '.Col = .GetColFromID("orddt") + ix + 1 : .Text = strBuf_Nm(ix) : .ColID = strBuf_Cd(ix)
                    'If Me.cboTerm.Text <> "" Then .set_ColWidth(.GetColFromID("orddt") + ix + 1, Convert.ToInt32(Me.cboTerm.Text))

                    '20210201 JHS 보고일자 추가로 인한 수정
                    'If .GetColFromID("tkdt") + ix + 1 > .MaxCols Then
                    '    .MaxCols += 1
                    'End If

                    '.Col = .GetColFromID("tkdt") + ix + 1 : .Text = strBuf_Nm(ix) : .ColID = strBuf_Cd(ix)
                    'If Me.cboTerm.Text <> "" Then .set_ColWidth(.GetColFromID("tkdt") + ix + 1, Convert.ToInt32(Me.cboTerm.Text))


                    If .GetColFromID("fndt") + ix + 1 > .MaxCols Then
                        .MaxCols += 1
                    End If
                    .Col = .GetColFromID("fndt") + ix + 1 : .Text = strBuf_Nm(ix) : .ColID = strBuf_Cd(ix)
                    If Me.cboTerm.Text <> "" Then .set_ColWidth(.GetColFromID("fndt") + ix + 1, Convert.ToInt32(Me.cboTerm.Text))
                    '---------------------------------------------------

                End With
            Next
        End If

    End Sub

    Private Sub sbDisplay_Date_Setting()

        If Me.cboQrygbn.Text = "검사그룹" Then
            Me.lblTitleDt.Text = "접수일자"

            Me.dtpDateE.Visible = True
            Me.lblDate.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

            Me.txtWkNoS.Visible = False : Me.txtWkNoE.Visible = False
            Me.cboTGrp.Visible = True : Me.cboWkGrp.Visible = False

        Else 'If Me.cboWkGrp.Text <> "" Then
            Me.lblTitleDt.Text = "작업일자"

            Me.dtpDateE.Visible = False
            Me.lblDate.Visible = False

            Me.txtWkNoS.Visible = True : Me.txtWkNoE.Visible = True
            Me.lblWk.Visible = True

            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = True

            Dim sWkNoGbn As String
            If cboWkGrp.Text <> "" Then
                sWkNoGbn = Me.cboWkGrp.Text.Split("|"c)(1)
            Else
                sWkNoGbn = ""
            End If

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

    Private Sub sbDisplay_Data(Optional ByVal rsBcNo As String = "")
        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


            Dim sSlipCd As String = "", sTGrpCd As String = ""
            Dim sWkYmd As String = "", sWGrpCd As String = "", sWkNoS As String = "", sWkNoE As String = ""
            Dim sRstFlg As String = ""
            Dim sTestCds As String = ""

            If Me.txtSelTest.Text <> "" Then
                sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")
            End If

            If Me.chkRstReg.Checked Then sRstFlg = "1"
            If Me.chkRstMw.Checked Then sRstFlg += IIf(sRstFlg = "", "", ",").ToString + "2"
            If Me.chkRstFn.Checked Then sRstFlg += IIf(sRstFlg = "", "", ",").ToString + "3"
            If Me.ChkMFn.Checked Then sRstFlg = "MF"


            Dim dt As New DataTable

            If Me.cboQrygbn.Text = "작업그룹" Then
                sSlipCd = Ctrl.Get_Code(cboSlip)

                sWkYmd = dtpDateS.Text.Replace("-", "").PadRight(8, "0"c)
                sWGrpCd = Ctrl.Get_Code(cboWkGrp)
                sWkNoS = Me.txtWkNoS.Text.PadLeft(4, "0"c)
                sWkNoE = Me.txtWkNoE.Text.PadLeft(4, "0"c) : If sWkNoE = "0000" Then sWkNoE = "9999"

                dt = fnGet_RstList_WGrp(sWkYmd, sWGrpCd, sWkNoS, sWkNoE, Ctrl.Get_Code(Me.cboSpcCd), sTestCds, sRstFlg, mbMicroBioYn)

                If Me.txtSelTest.Text = "" Then Me.chkTclsFix.Checked = False

            Else
                sTGrpCd = Ctrl.Get_Code(Me.cboTGrp)
                If sTGrpCd = "" Then sSlipCd = Ctrl.Get_Code(cboSlip)

                dt = fnGet_RstList_TGrp(sSlipCd, sTGrpCd, Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), Ctrl.Get_Code(Me.cboSpcCd), sTestCds, sRstFlg, mbMicroBioYn)

                If Me.cboQrygbn.Text <> "검사그룹" Or sTGrpCd = "" Then
                    If Me.txtSelTest.Text = "" Then Me.chkTclsFix.Checked = False
                End If
            End If

            If chkTclsFix.Checked Then
                If rsBcNo = "" Or Me.spdList.MaxRows = 0 Then sbDisplay_Test()
                If rsBcNo = "" Then
                    sbDisplay_Data_View_Fix(dt)
                Else
                    sbDisplay_Data_View_Fix(dt, True)
                End If
            Else
                With spdList
                    'For iCol As Integer = .GetColFromID("orddt") + 1 To .MaxCols
                    '    .Row = 0
                    '    .Col = iCol : .Text = Convert.ToString(iCol - .GetColFromID("orddt"))
                    '    .ColID = Convert.ToString(iCol - .GetColFromID("orddt"))
                    'Next
                    '20210201 jhs 보고일시 추가 수정
                    'For iCol As Integer = .GetColFromID("tkdt") + 1 To .MaxCols
                    '    .Row = 0
                    '    .Col = iCol : .Text = Convert.ToString(iCol - .GetColFromID("tkdt"))
                    '    .ColID = Convert.ToString(iCol - .GetColFromID("tkdt"))
                    'Next

                    For iCol As Integer = .GetColFromID("fndt") + 1 To .MaxCols
                        .Row = 0
                        .Col = iCol : .Text = Convert.ToString(iCol - .GetColFromID("fndt"))
                        .ColID = Convert.ToString(iCol - .GetColFromID("fndt"))
                    Next
                    '----------------------------------------
                End With
                If rsBcNo = "" Then
                    sbDisplay_Data_View(dt)
                Else
                    sbDisplay_Data_View(dt, True)
                End If
            End If


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View(ByVal r_dt As DataTable, Optional ByVal rbAdd As Boolean = False)

        Try
            Dim sBcNo As String = ""
            Dim iBcNo_Start_Row As Integer = 0
            Dim iGrpNo As Integer = 0
            Dim oBColor As System.Drawing.Color
            Dim iCol As Integer = 0
            Dim iRow As Integer = -2

            With Me.spdList
                If Not rbAdd Then
                    .MaxRows = 0
                Else
                    iRow = .MaxRows - 2
                End If

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    ' 새로운 바코드 구분
                    If sBcNo <> r_dt.Rows(ix).Item("bcno").ToString.Trim + r_dt.Rows(ix).Item("partslip").ToString.Trim Then

                        iGrpNo += 1
                        If iGrpNo Mod 2 = 1 Then
                            oBColor = System.Drawing.Color.White
                        Else
                            oBColor = System.Drawing.Color.FromArgb(255, 251, 244)
                        End If

                        .MaxRows += 3
                        iRow += 3

                        .Row = iRow

                        ' Line 그리기
                        If iRow > 1 Then Fn.DrawBorderLineTop(spdList, iRow)

                        '배경색 설정
                        .Row = iRow + 0 : .Col = -1
                        .BackColor = oBColor

                        .Row = iRow + 1 : .Col = -1
                        .BackColor = oBColor
                        .Row = iRow + 1 : .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                        .Row = iRow + 2 : .Col = -1
                        .BackColor = oBColor
                        .Row = iRow + 2 : .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                        iBcNo_Start_Row = .MaxRows
                        'iCol = .GetColFromID("orddt")
                        'iCol = .GetColFromID("tkdt") 
                        '20210201 jhs 보고자 보고일시 수정 추가
                        iCol = .GetColFromID("fndt")
                        '-----------------------------------------
                    End If
                    sBcNo = r_dt.Rows(ix).Item("bcno").ToString.Trim + r_dt.Rows(ix).Item("partslip").ToString.Trim


                    .Row = iRow
                    .Col = 0 : .Text = iGrpNo.ToString
                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("prtbcno") : .Text = r_dt.Rows(ix).Item("prtbcno").ToString.Trim
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("workno") : .Text = r_dt.Rows(ix).Item("workno").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptinfo") : .Text = r_dt.Rows(ix).Item("deptinfo").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("partslip") : .Text = r_dt.Rows(ix).Item("partslip").ToString.Trim
                    .Col = .GetColFromID("slipcmt") : .Text = r_dt.Rows(ix).Item("slipcmt").ToString.Trim
                    .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(ix).Item("orddt").ToString.Trim '2018-11-30 처방일자 추가
                    .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString.Trim '2019-03-25 접수일자 추가
                    '20210201 jhs 보고자 보고일시 수정 추가 
                    .Col = .GetColFromID("mwid") : .Text = r_dt.Rows(ix).Item("mwid").ToString.Trim
                    .Col = .GetColFromID("mwdt") : .Text = r_dt.Rows(ix).Item("mwdt").ToString.Trim
                    .Col = .GetColFromID("fnid") : .Text = r_dt.Rows(ix).Item("fnid").ToString.Trim
                    .Col = .GetColFromID("fndt") : .Text = r_dt.Rows(ix).Item("fndt").ToString.Trim
                    '--------------------------------------

                    .Row = iRow + 1 : .Row2 = iRow + 2
                    '.Col = .GetColFromID("orddt") + 1 : .Col2 = .MaxCols
                    '.Col = .GetColFromID("tkdt") + 1 : .Col2 = .MaxCols 
                    '20210201 jhs 보고자 보고일시 수정 추가
                    .Col = .GetColFromID("fndt") + 1 : .Col2 = .MaxCols
                    '-----------------------------------------
                    .BlockMode = True
                    .ForeColor = Color.Blue
                    .BlockMode = False

                    iCol += 1
                    If iCol > .MaxCols Then
                        .MaxCols += 1
                        .Col = .MaxCols : .ColID = Convert.ToString(iCol - .GetColFromID("spcnmd"))
                    End If


                    Dim sRefVal As String = ""

                    If r_dt.Rows(ix).Item("hlmark").ToString.Trim <> "" Then
                        sRefVal += r_dt.Rows(ix).Item("hlmark").ToString.Trim

                    ElseIf r_dt.Rows(ix).Item("panicmark").ToString.Trim <> "" Then
                        sRefVal += IIf(sRefVal = "", "", ",").ToString.Trim + "P"

                    ElseIf r_dt.Rows(ix).Item("deltamark").ToString.Trim <> "" Then
                        sRefVal += IIf(sRefVal = "", "", ",").ToString.Trim + "D"
                    ElseIf r_dt.Rows(ix).Item("criticalmark").ToString.Trim <> "" Then
                        sRefVal += IIf(sRefVal = "", "", ",").ToString.Trim + "C"
                    ElseIf r_dt.Rows(ix).Item("alertmark").ToString.Trim <> "" Then
                        sRefVal += IIf(sRefVal = "", "", ",").ToString.Trim + "A"
                    End If

                    .Row = iRow + 0 : .Col = iCol : .Text = r_dt.Rows(ix).Item("tnmd").ToString
                    .Row = iRow + 1 : .Col = iCol : .Text = r_dt.Rows(ix).Item("viewrst").ToString + IIf(sRefVal = "", "", "(" + sRefVal + ")").ToString
                    .Row = iRow + 2 : .Col = iCol : .Text = r_dt.Rows(ix).Item("rstcmt").ToString

                Next

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View_Fix(ByVal r_dt As DataTable, Optional ByVal rbAdd As Boolean = False)
        Try
            Dim sKeyNo As String = ""
            Dim iBcNo_Start_Row As Integer = 0
            Dim iGrpNo As Integer = 0
            Dim oBColor As System.Drawing.Color
            Dim iCol As Integer = 0

            With Me.spdList
                If rbAdd = False Then .MaxRows = 0

                .ReDraw = False

                For iRow As Integer = 0 To r_dt.Rows.Count - 1
                    ' 새로운 바코드 구분
                    If sKeyNo <> r_dt.Rows(iRow).Item("bcno").ToString.Trim + r_dt.Rows(iRow).Item("partslip").ToString.Trim Then

                        iGrpNo += 1
                        If iGrpNo Mod 2 = 1 Then
                            oBColor = System.Drawing.Color.White
                        Else
                            oBColor = System.Drawing.Color.FromArgb(255, 251, 244)
                        End If

                        .MaxRows += 2
                        .Row = .MaxRows

                        ' Line 그리기
                        If iRow > 1 Then Fn.DrawBorderLineTop(spdList, iRow)

                        '배경색 설정
                        .Row = .MaxRows - 1 : .Col = -1
                        .BackColor = oBColor

                        .Row = .MaxRows : .Col = -1
                        .BackColor = oBColor

                        .Row = .MaxRows : .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                        iBcNo_Start_Row = .MaxRows
                        'iCol = .GetColFromID("orddt")
                        'iCol = .GetColFromID("tkdt") ' 2019-03-25 접수일자
                        '20210201 jhs 보고자 보고일시 수정 추가
                        iCol = .GetColFromID("fndt") '+ 1 : .Col2 = .MaxCols 'test
                        '-----------------------------------------
                    End If

                    sKeyNo = r_dt.Rows(iRow).Item("bcno").ToString.Trim + r_dt.Rows(iRow).Item("partslip").ToString.Trim

                    .Row = .MaxRows - 1
                    .Col = 0 : .Text = iGrpNo.ToString
                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("prtbcno") : .Text = r_dt.Rows(iRow).Item("prtbcno").ToString.Trim
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(iRow).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("workno") : .Text = r_dt.Rows(iRow).Item("workno").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(iRow).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(iRow).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(iRow).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptinfo") : .Text = r_dt.Rows(iRow).Item("deptinfo").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(iRow).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(iRow).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("partslip") : .Text = r_dt.Rows(iRow).Item("partslip").ToString.Trim
                    .Col = .GetColFromID("slipcmt") : .Text = r_dt.Rows(iRow).Item("slipcmt").ToString.Trim
                    .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(iRow).Item("orddt").ToString.Trim '2018-11-30 처방일자 추가
                    .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(iRow).Item("tkdt").ToString.Trim '2019-03-25 접수일자 추가
                    '20210201 jhs 보고자 보고일시 수정 추가 
                    .Col = .GetColFromID("mwid") : .Text = r_dt.Rows(iRow).Item("mwid").ToString.Trim
                    .Col = .GetColFromID("mwdt") : .Text = r_dt.Rows(iRow).Item("mwdt").ToString.Trim
                    .Col = .GetColFromID("fnid") : .Text = r_dt.Rows(iRow).Item("fnid").ToString.Trim
                    .Col = .GetColFromID("fndt") : .Text = r_dt.Rows(iRow).Item("fndt").ToString.Trim
                    '--------------------------------------

                    iCol = .GetColFromID(r_dt.Rows(iRow).Item("testcd").ToString.Trim)
                    If iCol > 0 Then
                        .Col = iCol
                        .Row = .MaxRows - 1 : .Text = r_dt.Rows(iRow).Item("viewrst").ToString.Trim
                        .Row = .MaxRows - 0 : .Text = r_dt.Rows(iRow).Item("rstcmt").ToString.Trim
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
                For intRow As Integer = 1 To .MaxRows Step Convert.ToInt16(IIf(chkTclsFix.Checked, 2, 3))
                    .Row = intRow
                    .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                    If strChk = "1" Then

                        Dim strBuf() As String = rsTitle_Item.Split("|"c)
                        Dim arlItem As New ArrayList

                        For ix As Integer = 0 To strBuf.Length - 1

                            If strBuf(ix) = "" Then Exit For

                            Dim iCol As Integer = .GetColFromID(strBuf(ix).Split("^"c)(1))

                            If iCol > 0 Then


                                Dim strTitle As String = strBuf(ix).Split("^"c)(0)
                                Dim strField As String = strBuf(ix).Split("^"c)(1)
                                Dim strWidth As String = strBuf(ix).Split("^"c)(2)


                                .Row = intRow
                                .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                                arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                            End If
                        Next

                        Dim sSlipCmt As String = ""
                        Dim sTnms As String = "", sRstVals As String = "", sRstCmts As String = ""

                        .Row = intRow : .Col = .GetColFromID("slipcmt") : sSlipCmt = .Text

                        If chkTclsFix.Checked Then
                            For iCnt As Integer = .GetColFromID("spcnmd") + 1 To .MaxCols
                                Dim sTnm As String = "", sRstVal As String = "", sRstCmt As String = ""
                                .Row = 0 : .Col = iCnt : sTnm = .Text
                                .Row = intRow : .Col = iCnt : sRstVal = .Text
                                .Row = intRow + 1 : .Col = iCnt : sRstCmt = .Text

                                If sTnm = "" Then Exit For

                                sTnms += sTnm + "|"
                                sRstVals += sRstVal + "|"
                                sRstCmts += sRstCmt + "|"
                            Next
                        Else
                            For iCnt As Integer = .GetColFromID("spcnmd") + 1 To .MaxCols
                                Dim strTnm As String = "", sRstVal As String = "", sRstCmt As String = ""
                                .Row = intRow : .Col = iCnt : strTnm = .Text
                                .Row = intRow + 1 : .Col = iCnt : sRstVal = .Text
                                .Row = intRow + 1 : .Col = iCnt : sRstCmt = .Text

                                If strTnm <> "" Then
                                    sTnms += strTnm + "|"
                                    sRstVals += sRstVal + "|"
                                    sRstCmts += sRstCmt + "|"
                                End If
                            Next
                        End If

                        Dim objPat As New FGS15_PATINFO

                        With objPat
                            .alItem = arlItem

                            .SlipCmt = sSlipCmt

                            .sTNms = sTnms
                            .sRsts = sRstVals

                        End With

                        arlPrint.Add(objPat)

                    End If
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGS15_PRINT
                prt.mbLandscape = True
                prt.msTitle = "결과 대장"
                prt.msJobGbn = cboQrygbn.Text
                prt.maPrtData = arlPrint
                prt.miTotExmCnt = spdList.MaxCols - spdList.GetColFromID("spcnmd")
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                If cboTerm.Text = "" Then
                    prt.msgExmWidth = 60.0
                Else
                    prt.msgExmWidth = Convert.ToSingle(cboTerm.Text) * 10
                End If

                If chkPreview.Checked Then
                    prt.sbPrint_Preview(chkTclsFix.Checked)
                Else
                    prt.sbPrint(chkTclsFix.Checked)
                End If
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

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
            .WIDTH = "95"
            .FIELD = "regno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1"
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
            .TITLE = "검체명"
            .WIDTH = "100"
            .FIELD = "spcnmd"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "검사분야"
            .WIDTH = "40"
            .FIELD = "partslip"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "소견"
            .WIDTH = "100"
            .FIELD = "slipcmt"
        End With
        alItems.Add(stu_item)

        Return alItems

    End Function
#End Region

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Me.spdList.MaxRows = 0
        sbDisplay_Data()
    End Sub

    Private Sub FGS15_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS15_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            '.MaxCols = .GetColFromID("orddt")
            '.MaxCols = .GetColFromID("tkdt")
            '20210210 jhs 최종보고 일시 추가 
            .MaxCols = .GetColFromID("fndt")
            '---------------------------------
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
            msTESTFile = Application.StartupPath + msXMLDir + "\FGS15_M_TEST.XML"
            msWGrpFile = Application.StartupPath + msXMLDir + "\FGS15_M_WGRP.XML"
            msTGrpFile = Application.StartupPath + msXMLDir + "\FGS15_M_TGRP.XML"
            msSlipFile = Application.StartupPath + msXMLDir + "\FGS15_M_SLIP.XML"
            msSPCFile = Application.StartupPath + msXMLDir + "\FGS15_M_SPC.XML"
            msQryFile = Application.StartupPath + msXMLDir + "\FGS15_M_Qry.XML"
            msTERMFile = Application.StartupPath + msXMLDir + "\FGS15_M_Term.XML"

            Me.Text = Me.Text + "(미생물)"
            Me.chkRstMw.Text = "중간보고"
        End If

    End Sub

    Private Sub FGS15_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        sbDisp_Init()

    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try
            Dim sReturn As String = ""

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


                sbPrint_Data(sReturn)
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            With spdList
                .ReDraw = False

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text <> "1" And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .RowHidden = True
                Next

                .MaxRows = .MaxRows + 1
                .InsertRows(1, 1)

                For i As Integer = 1 To .MaxCols
                    .Col = i : .Row = 0 : Dim sBuf As String = .Text
                    .Col = i : .Row = 1 : .Text = sBuf
                Next

                .Col = .GetColFromID("chk") : .ColHidden = True

                If .ExportToExcel("결과대장_" + Now.ToShortDateString() + ".xls", "결과대장", "") Then
                    Process.Start("결과대장_" + Now.ToShortDateString() + ".xls")
                End If

                .DeleteRows(1, 1)
                .MaxRows = .MaxRows - 1

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text <> "1" And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .RowHidden = False
                Next

                .Col = .GetColFromID("chk") : .ColHidden = False


                .ReDraw = True

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

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

    Private Sub chkTclsFix_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTclsFix.Click
        sbDisplay_Data()
    End Sub

    Private Sub chkColMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkColMove.Click
        If chkColMove.Checked Then
            spdList.AllowColMove = True
        Else
            spdList.AllowColMove = False
        End If
    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
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
            Dim a_dr As DataRow() = dt.Select("(tcdgbn = 'P'OR titleyn = '0')", "sort1, sort2, testcd")

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

            COMMON.CommXML.setOneElementXML(msXMLDir, msTESTFile, "TEST", Me.txtSelTest.Tag.ToString)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbClear_Form()

        sbDisplay_WkGrp()
        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", cboSlip.SelectedIndex.ToString)
    End Sub

    Private Sub btnClear_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_test.Click
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""
    End Sub

    Private Sub cboQrygbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQrygbn.SelectedIndexChanged
        sbDisplay_Date_Setting()
        COMMON.CommXML.setOneElementXML(msXMLDir, msQryFile, "JOB", cboQrygbn.SelectedIndex.ToString)
    End Sub

    Private Sub cboSpcCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSpcCd.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXMLDir, msSPCFile, "SPC", cboSpcCd.SelectedIndex.ToString)
    End Sub

    Private Sub cboTerm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTerm.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXMLDir, msTERMFile, "TERM", cboTerm.Text)
    End Sub

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbDisplay_Spc()
        COMMON.CommXML.setOneElementXML(msXMLDir, msTGrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged

        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbClear_Form()

        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        If Me.cboWkGrp.SelectedIndex >= 0 Then
            COMMON.CommXML.setOneElementXML(msXMLDir, msWGrpFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)
        End If
    End Sub

    Private Sub txtBcNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcNo.Click
        Me.txtBcNo.SelectAll()
    End Sub

    Private Sub txtBcNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcNo.GotFocus
        Me.txtSelTest.SelectionStart = 0
    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try

            Me.txtBcNo.Text = Me.txtBcNo.Text.Trim().Replace("-", "")

            If Me.txtBcNo.Text.Length = 14 Then Me.txtBcNo.Text += "0"
            If Me.txtBcNo.Text.Length = 12 Or Me.txtBcNo.Text.Length = 11 Then
                Me.txtBcNo.Text = LISAPP.COMM.BcnoFn.fnFind_BcNo(Me.txtBcNo.Text)
            End If

            sbDisplay_Data(Me.txtBcNo.Text)

            Me.txtBcNo.SelectionStart = 0
            Me.txtBcNo.SelectAll()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim lbtext As String = ""
        lbtext = lblTitleDt.Text
        If lbtext = "접수일자" Then
            lblTitleDt.Text = "결과일자"
        Else
            lblTitleDt.Text = "접수일자"
        End If
    End Sub

    Private Sub chkRstReg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRstReg.CheckedChanged, chkRstMw.CheckedChanged, chkRstFn.CheckedChanged
        If ChkMFn.Checked = True Then
            ChkMFn.Checked = False
        End If
    End Sub

    Private Sub ChkMFn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkMFn.CheckedChanged
        If chkRstReg.Checked = True Or chkRstMw.Checked = True Or chkRstFn.Checked = True Then
            chkRstReg.Checked = False : chkRstMw.Checked = False : chkRstFn.Checked = False
        End If
    End Sub
End Class

Public Class FGS15_PATINFO
    Public alItem As New ArrayList

    Public SlipCmt As String = ""

    Public sTNms As String = ""
    Public sTCds As String = ""
    Public sRsts As String = ""
    Public sCmts As String = ""
End Class


Public Class FGS15_PRINT
    Private Const msFile As String = "File : FGS15.vb, Class : LISS.FGS15_PRINT" + vbTab

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

    Public msgExmWidth As Single = 0
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
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

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
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
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
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGS15_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGS15_PATINFO).alItem(ix).ToString.Split("^"c)(2))
        Next

        If sngTmp + 40 > msgWidth Then Return

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.2)

        Dim rect As New Drawing.RectangleF

        For ix As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString((ix + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            For ix2 As Integer = 1 To CType(maPrtData.Item(ix), FGS15_PATINFO).alItem.Count
                Dim strTmp As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(ix2 - 1).ToString.Split("^"c)(0)

                rect = New Drawing.RectangleF(msgPosX(ix2), sngPosY, msgPosX(ix2 + 1) - msgPosX(ix2), sngPrtH)
                e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            sngPosY += sngPrtH * 2

            Dim sTnm() As String = CType(maPrtData.Item(ix), FGS15_PATINFO).sTNms.Split("|"c)
            Dim sRst() As String = CType(maPrtData.Item(ix), FGS15_PATINFO).sRsts.Split("|"c)

            Dim iCol As Integer = 0
            For ix2 As Integer = 0 To sTnm.Length - 2
                iCol += 1
                If iCol > miTitle_ExmCnt Then
                    iCol = 1
                    sngPosY += sngPrtH * 2

                    e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(1), sngPosY - sngPrtH, msgWidth - msgPosX(0), sngPosY - sngPrtH)
                End If

                If msgHeight < sngPosY + sngPrtH * 3 Then Exit For

                '-- 검사명
                rect = New Drawing.RectangleF(msgPosX(1) + msgExmWidth * (iCol - 1), sngPosY - sngPrtH * 1, msgExmWidth, sngPrtH)
                e.Graphics.DrawString(sTnm(ix2), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                '-- 검사결과
                rect = New Drawing.RectangleF(msgPosX(1) + msgExmWidth * (iCol - 1), sngPosY + sngPrtH * 0, msgExmWidth, sngPrtH)
                e.Graphics.DrawString(sRst(ix2), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            Dim sBuf() As String = CType(maPrtData.Item(ix), FGS15_PATINFO).SlipCmt.Replace(vbLf, "").Split(Convert.ToChar(13))

            For ix2 As Integer = 0 To sBuf.Length - 1
                If ix2 = sBuf.Length - 1 And sBuf(ix2) = "" Then Exit For

                sngPosY += sngPrtH
                If ix2 = 0 Then
                    rect = New Drawing.RectangleF(msgPosX(1), sngPosY, msgWidth - msgPosX(1), sngPrtH)
                    e.Graphics.DrawString("소견: ", fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                End If
                '-- 소견
                rect = New Drawing.RectangleF(msgPosX(1) + 40, sngPosY, msgWidth - msgPosX(1) - 40, sngPrtH)
                e.Graphics.DrawString(sBuf(ix2), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
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
        Dim sngPrtH As Single = 0
        Dim sngPosY As Single = 0
        Dim iCnt As Integer = 1

        miTitleCnt = CType(maPrtData.Item(0), FGS15_PATINFO).alItem.Count + 1

        Dim sngPosX(0 To 1) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40D

        For ix As Integer = 1 To miTitleCnt - 1
            ReDim Preserve sngPosX(ix + 1)

            sngPosX(ix + 1) = sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), FGS15_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))
        Next

        iCnt = Convert.ToInt16(msgWidth / msgExmWidth)
        If iCnt * msgExmWidth > msgWidth Then iCnt -= 1
        miTitle_ExmCnt = iCnt

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrtH)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrtH * 2
        sngPrtH = Convert.ToSingle(fnt_Title.GetHeight(e.Graphics) * 0.7)

        If msTitle_sub_right_1.Length > msTitle_Time.Length + 6 Then
            msTitle_Time = msTitle_Time.PadRight(msTitle_sub_right_1.Length - 6)
        Else
            msTitle_sub_right_1 = msTitle_sub_right_1.PadRight(msTitle_Time.Length + 6)
        End If

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * msTitle_sub_right_1.Length, sngPosY - 20, msgWidth - 8 * msTitle_sub_right_1.Length, sngPrtH), sf_l)
        End If

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrtH), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6), sngPosY, msgWidth - 8 * (msTitle_Time.Length + 6), sngPrtH), sf_l)

        sngPosY += sngPrtH + sngPrtH / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrtH), sf_l)

        For ix As Integer = 1 To CType(maPrtData.Item(0), FGS15_PATINFO).alItem.Count

            Dim strTmp As String = CType(maPrtData.Item(0), FGS15_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(ix), sngPosY + sngPrtH * 0, sngPosX(ix + 1) - sngPosX(ix), sngPrtH), sf_l)
        Next

        e.Graphics.DrawString("검사항목", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY + sngPrtH * 1, msgWidth - sngPosX(1), sngPrtH), sf_l)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY, msgWidth, sngPosY)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrtH * 2, msgWidth, sngPosY + sngPrtH * 2)

        msgPosX = sngPosX
        Return sngPosY + sngPrtH * 2

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
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGS15_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGS15_PATINFO).alItem(ix).ToString.Split("^"c)(2))
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
                    sngPosY = fnPrtTitle_Fixed(e, CType(maPrtData.Item(intIdx), FGS15_PATINFO).sTNms.Split("|"c), miCCol)
                End If

                '-- 번호
                rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * intLine, msgPosX(1) - msgPosX(0), sngPrtH)
                e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

                For ix As Integer = 1 To CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem.Count
                    rect = New Drawing.RectangleF(msgPosX(ix), sngPosY + sngPrtH * intLine, msgPosX(ix + 1) - msgPosX(ix), sngPrtH)
                    Dim strTmp As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(ix - 1).ToString.Split("^"c)(0)

                    e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                Next

                Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).sTNms.Split("|"c)
                Dim strRst() As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).sRsts.Split("|"c)

                intCnt = 0 : Dim intTitleCnt As Integer = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem.Count + 1

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

        miTitleCnt = CType(maPrtData.Item(0), FGS15_PATINFO).alItem.Count + 1

        Dim sngPosX(0 To 1) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40

        For ix As Integer = 1 To miTitleCnt - 1
            ReDim Preserve sngPosX(ix + 1)

            sngPosX(ix + 1) = sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), FGS15_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))
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

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - msgLeft - 25, sngPrt), sf_r)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt * 2), sf_l)

        For ix As Integer = 1 To CType(maPrtData.Item(0), FGS15_PATINFO).alItem.Count

            Dim strTmp As String = CType(maPrtData.Item(0), FGS15_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

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

            Dim sBcNo As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(0).ToString.Split("^"c)(0)
            Dim sWorkNo As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(1).ToString.Split("^"c)(0)
            Dim sRegNo As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(2).ToString.Split("^"c)(0)
            Dim sPatNm As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(3).ToString.Split("^"c)(0)
            Dim sSexAge As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(4).ToString.Split("^"c)(0)
            Dim sDeptWard As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(6).ToString.Split("^"c)(0)
            Dim sDoctorRmk As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(7).ToString.Split("^"c)(0)
            Dim sDiagNm As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(8).ToString.Split("^"c)(0)
            Dim sSpcNmd As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(9).ToString.Split("^"c)(0)
            Dim sPrtBcNo As String = CType(maPrtData.Item(ix), FGS15_PATINFO).alItem(10).ToString.Split("^"c)(0)

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

            Dim strTnm() As String = CType(maPrtData.Item(ix), FGS15_PATINFO).sTNms.Split("|"c)
            Dim strRst() As String = CType(maPrtData.Item(ix), FGS15_PATINFO).sRsts.Split("|"c)

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
                Dim sBcNo As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(0).ToString.Split("^"c)(0)
                Dim sWorkNo As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(1).ToString.Split("^"c)(0)
                Dim sRegNo As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(2).ToString.Split("^"c)(0)
                Dim sPatNm As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(3).ToString.Split("^"c)(0)
                Dim sSexAge As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(4).ToString.Split("^"c)(0)
                Dim sDeptWard As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(6).ToString.Split("^"c)(0)
                Dim sDoctorRmk As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(7).ToString.Split("^"c)(0)
                Dim sDiagNm As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(8).ToString.Split("^"c)(0)
                Dim sSpcNmd As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(9).ToString.Split("^"c)(0)
                Dim sPrtBcNo As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).alItem(10).ToString.Split("^"c)(0)

                If sgPosY = 0 Then
                    sgPosY = fnPrtTitle_Fixed_barno(e, CType(maPrtData.Item(intIdx), FGS15_PATINFO).sTNms.Split("|"c), miCCol)
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

                Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).sTNms.Split("|"c)
                Dim strRst() As String = CType(maPrtData.Item(intIdx), FGS15_PATINFO).sRsts.Split("|"c)

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