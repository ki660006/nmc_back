'>>> WorkList 조회

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.APP_S.WkFn

Public Class FGS13
    Inherits System.Windows.Forms.Form

    Private Const msXMLDir As String = "\XML"
    Private msTestFile As String = Application.StartupPath & msXMLDir & "\FGS13_TEST.XML"
    Private msWkGrpFile As String = Application.StartupPath & msXMLDir & "\FGS13_WKGRP.XML"
    Private msTgrpFile As String = Application.StartupPath & msXMLDir & "\FGS13_TGRP.XML"
    Private msSlipFile As String = Application.StartupPath & msXMLDir & "\FGS13_SLIP.XML"
    Private msSpcFile As String = Application.StartupPath & msXMLDir & "\FGS13_SPC.XML"
    Private msQryFile As String = Application.StartupPath & msXMLDir & "\FGS13_Qry.XML"
    Private msTermFile As String = Application.StartupPath & msXMLDir & "\FGS13_Term.XML"

    Private m_al_SheetList As New ArrayList
    Private mbMicroBioYn As Boolean = False
    Private mbM As Boolean = False


#Region " Form내부 함수 "
    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        stu_item = New STU_PrtItemInfo


        If mbMicroBioYn = False Then
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
                .WIDTH = "65"
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
                .WIDTH = "65"
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
                .TITLE = "접수일시"
                .WIDTH = "80"
                .FIELD = "tkdt"
            End With
            alItems.Add(stu_item)

            stu_item = New STU_PrtItemInfo
            With stu_item
                .CHECK = ""
                .TITLE = "검체명"
                .WIDTH = "100"
                .FIELD = "spcnmd"
            End With
        Else '< 20121012 미생물로 출력시 출력 컬럼변경 
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
                .TITLE = "작업번호"
                .WIDTH = "120"
                .FIELD = "workno"
            End With
            alItems.Add(stu_item)

            stu_item = New STU_PrtItemInfo
            With stu_item
                .CHECK = "1"
                .TITLE = "등록번호"
                .WIDTH = "65"
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
                .CHECK = ""
                .TITLE = "성별/나이"
                .WIDTH = "65"
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
                .TITLE = "접수일시"
                .WIDTH = "80"
                .FIELD = "tkdt"
            End With
            alItems.Add(stu_item)

            stu_item = New STU_PrtItemInfo
            With stu_item
                .CHECK = "1"
                .TITLE = "검체명"
                .WIDTH = "100"
                .FIELD = "spcnmd"
            End With

        End If
        alItems.Add(stu_item)

        Return alItems

    End Function

    '-- WK Sheet 정의 가져오기
    Private Sub sbDisplay_sheet()

        Try

            Dim oFso As New Scripting.FileSystemObject
            Dim oFolder As Scripting.Folder

            If Dir(Application.StartupPath + "\ssf", FileAttribute.Directory) <> "" Then
                oFolder = oFso.GetFolder(Application.StartupPath + "\ssf")
                Dim objFile As Scripting.File

                For Each objFile In oFolder.Files
                    If objFile.Name.ToLower.StartsWith("ws_") Or objFile.Name.ToLower.StartsWith("ts_") Then
                        m_al_SheetList.Add(objFile.Name.Substring(0, objFile.Name.Length - 4).ToLower)
                    End If
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )

        End Try
    End Sub


    ' 화면 정리
    Private Sub sbClear_Form()
        Me.spdList.MaxRows = 0

    End Sub

    Private Sub sbDisp_Init()

        Try
            If mbMicroBioYn = False Then Me.chkPrtWL.Checked = True
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
                    Me.cboTGrp.SelectedIndex = 0
                Else
                    Me.cboTGrp.SelectedIndex = Convert.ToInt16(sTgrp)
                End If
            End If

            If Me.cboWkGrp.Items.Count > 0 Then
                If sWkGrp = "" Or Val(sWkGrp) > Me.cboWkGrp.Items.Count Then
                    Me.cboWkGrp.SelectedIndex = 0
                Else
                    Me.cboWkGrp.SelectedIndex = Convert.ToInt16(sWkGrp)
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

            sbDisplay_BarCdPrt()
            sbDisplay_sheet()

            Me.dtpDateS.Focus()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Date_Setting()

        If Me.cboQrygbn.Text = "검사그룹" Then
            Me.lblTitleDt.Text = "접수일자"

            Me.dtpDateE.Visible = True
            Me.lblDate.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

            Me.txtWkNoS.Visible = False : Me.txtWkNoE.Visible = False
            Me.cboTGrp.Visible = True : Me.cboWkGrp.Visible = False

        Else 'If Me.cboWkGrp.Text <> "" Or cboWkGrp.Text = "" Then
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

    Private Sub sbDisplay_Data(Optional ByVal rsBcNo As String = "")

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
            Dim sSpcCds As String = ""

            If Me.txtSelTest.Text <> "" Then
                sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")
            End If

            If Me.txtSelSpc.Text <> "" Then
                sSpcCds = Me.txtSelSpc.Tag.ToString.Split("^"c)(0).Replace("|", ",")
            End If

            Dim sRstFlg As String = "0000"

            If Me.chkRstNull.Checked Then sRstFlg = "1000"
            If Me.chkRstReg.Checked Then sRstFlg = sRstFlg.Substring(0, 1) + "100"
            If Me.chkRstMw.Checked Then sRstFlg = sRstFlg.Substring(0, 2) + "10"
            If Me.chkRstFn.Checked Then sRstFlg = sRstFlg.Substring(0, 3) + "1"


            If Me.cboQrygbn.Text = "작업그룹" Then
                sPartSlip = Ctrl.Get_Code(Me.cboSlip)
                sWkYmd = Me.dtpDateS.Text.Replace("-", "").PadRight(8, "0"c)

                sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
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
                sTGrpCd = Ctrl.Get_Code(Me.cboTGrp)
                If sTGrpCd = "" Then sPartSlip = Ctrl.Get_Code(Me.cboSlip)

                sDateS = dtpDateS.Text.Replace("-", "").Replace(" ", "")
                sDateE = dtpDateE.Text.Replace("-", "").Replace(" ", "")
            End If

            Dim dt As New DataTable
            If sWkYmd <> "" Then
                dt = fnGet_WorkList_WGrp(sWkYmd, sWGrpCd, sWkNoS, sWkNoE, sSpcCds, sTestCds, sRstFlg, rsBcNo, Me.chkMbtType.Checked, mbMicroBioYn, Not Me.chkSpcSelect.Checked)
            Else
                dt = fnGet_WorkList_TGrp(sPartSlip, sTGrpCd, sDateS, sDateE, sSpcCds, sTestCds, sRstFlg, rsBcNo, Me.chkMbtType.Checked, mbMicroBioYn, Not Me.chkSpcSelect.Checked)
            End If

            If Me.cboQrygbn.Text <> "검사그룹" Or sTGrpCd = "" Then
                If Me.txtSelTest.Text = "" Then Me.chkTestsFix.Checked = False
            End If

            If Me.chkTestsFix.Checked Then
                If (rsBcNo = "" Or Me.spdList.MaxRows = 0) Then
                    sbDisplay_Test()

                End If


                If rsBcNo = "" Then
                    sbDisplay_Data_View_Fix(dt)
                Else
                    sbDisplay_Data_View_Fix(dt, True)
                End If
            Else

                With Me.spdList
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

            '<20130801 정선영 추가, 조회된 내용 없을 때 메세지 박스 나오도록
            If Me.spdList.MaxRows = 0 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조회자료가 없습니다.!!")
            End If
            '>

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View(ByVal r_dt As DataTable, Optional ByVal rbAdd As Boolean = False)

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
                    .Col = .GetColFromID("workno") : .Text = r_dt.Rows(ix).Item("workno").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptinfo") : .Text = r_dt.Rows(ix).Item("deptinfo").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("diagnm") : .Text = r_dt.Rows(ix).Item("diagnm").ToString.Trim
                    .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(ix).Item("orddt").ToString.Trim '20140128(20140526) 정선영 추가, 처방일
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("doctorrmk") : .Text = r_dt.Rows(ix).Item("doctorrmk").ToString.Trim
                    .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString.Trim
                   



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
                    .Col = .GetColFromID("workno") : .Text = r_dt.Rows(ix).Item("workno").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptinfo") : .Text = r_dt.Rows(ix).Item("deptinfo").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("diagnm") : .Text = r_dt.Rows(ix).Item("diagnm").ToString.Trim
                    .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(ix).Item("orddt").ToString.Trim '20140128 정선영 추가, 처방일
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("doctorrmk") : .Text = r_dt.Rows(ix).Item("doctorrmk").ToString.Trim
                    .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString.Trim
                    

                    iCol += 1
                    If iCol > .MaxCols Then
                        .MaxCols += 1
                        If Me.cboTerm.Text <> "" Then .set_ColWidth(.MaxCols, Convert.ToInt32(Me.cboTerm.Text))


                        .Row = 0 : .Col = iCol : .Text = (iCol - .GetColFromID("spcnmd")).ToString.Trim
                    

                    End If

                    .Row = iRow
                    .Col = iCol : .Text = r_dt.Rows(ix).Item("tnmd").ToString.Trim
                    .Row = iRow + 1
                    .Col = iCol : .Text = r_dt.Rows(ix).Item("bfviewrst").ToString.Trim
                    If .Text = "" Then .Text = "▷"

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
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList
            Dim strBcNo As String = "", strDocRmk As String = ""
            Dim intBcNo_Start_Row As Integer = 0
            Dim intGrpNo As Integer = 0
            Dim objBColor As System.Drawing.Color
            Dim intCol As Integer = 0

            With spd
                If Not rbAdd Then
                    .MaxRows = 0
                End If
                .ReDraw = False

                For intRow As Integer = 0 To r_dt.Rows.Count - 1
                    ' 새로운 바코드 구분
                    If strBcNo <> r_dt.Rows(intRow).Item("bcno").ToString.Trim Then

                        If intBcNo_Start_Row > 0 Then
                            For intIx1 As Integer = intBcNo_Start_Row To .MaxRows
                                .Row = intIx1
                                .Col = .GetColFromID("doctorrmk") : .Text = strDocRmk
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

                    If r_dt.Rows(intRow).Item("doctorrmk").ToString <> "" Then
                        strDocRmk += IIf(strDocRmk = "", "", ",").ToString + r_dt.Rows(intRow).Item("doctorrmk").ToString.Trim
                    End If
                    strBcNo = r_dt.Rows(intRow).Item("bcno").ToString.Trim

                    .Row = .MaxRows
                    'If cboJobGbn.Text = "작업그룹" Then
                    '    .Col = 0 : .Text = r_dt.Rows(intRow).Item("workno").ToString.Substring(r_dt.Rows(intRow).Item("workno").ToString.Length - 4) 'intGrpNo.ToString
                    'Else
                    .Col = 0 : .Text = intGrpNo.ToString
                    'End If

                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("prtbcno") : .Text = r_dt.Rows(intRow).Item("prtbcno").ToString.Trim
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(intRow).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("workno") : .Text = r_dt.Rows(intRow).Item("workno").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(intRow).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(intRow).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(intRow).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptinfo") : .Text = r_dt.Rows(intRow).Item("deptinfo").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(intRow).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(intRow).Item("orddt").ToString.Trim '20140128 정선영 추가, 처방일
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(intRow).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("diagnm") : .Text = r_dt.Rows(intRow).Item("diagnm").ToString.Trim
                    .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(intRow).Item("tkdt").ToString.Trim


                    intCol = .GetColFromID(r_dt.Rows(intRow).Item("testcd").ToString.Trim)
                    If intCol > 0 Then
                        .Col = intCol
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
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    Private Sub sbDisplay_Test()

        If Me.txtSelTest.Text = "" And Ctrl.Get_Code(Me.cboTGrp) <> "" Then
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_Test_List(Ctrl.Get_Code(Me.cboTGrp))

            For ix As Integer = 0 To dt.Rows.Count - 1
                With spdList
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

            Me.spdList.MaxCols = spdList.GetColFromID("spcnmd") + 1

            For ix As Integer = 0 To strBuf_Cd.Length - 1
                With spdList
                    .Row = 0
                    If .GetColFromID("spcnmd") + ix + 1 > .MaxCols Then
                        .MaxCols += 1
                    End If

                    .Col = .GetColFromID("spcnmd") + ix + 1 : .Text = strBuf_Nm(ix) : .ColID = strBuf_Cd(ix)
                    If Me.cboTerm.Text <> "" Then .set_ColWidth(.GetColFromID("spcnmd") + ix + 1, Convert.ToInt32(Me.cboTerm.Text))
                    '2018-07-23 yjh 검사명 안보여서 주석처리.
                    '.GetColFromID("spcnmd") : .ColHidden = True
                End With
            Next
        End If

    End Sub
    Private Sub sbDisplay_Test_MB()

        spdList.GetColFromID("tkdt") : spdList.ColHidden = True

        If Me.txtSelTest.Text = "" And Ctrl.Get_Code(Me.cboTGrp) <> "" Then
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_Test_List(Ctrl.Get_Code(Me.cboTGrp))

            For ix As Integer = 0 To dt.Rows.Count - 1
                With spdList
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

            Me.spdList.MaxCols = spdList.GetColFromID("spcnmd") + 1

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

    Private Sub sbPrint_wl(ByVal rsTitle_Item As String)

        Try
            Dim arlPrint As New ArrayList
            Dim arlPrint_typ As New ArrayList

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
                        '<jjh
                        If chkTyping.Checked Then

                            Dim objPat As New FGS13_PATINFO
                            Dim objPat2 As New FGS13_PATINFO

                            With objPat
                                .alItem = arlItem

                                .sTNms = strTnms + "Front Typing" + vbCrLf + "채혈자"
                                .sRsts = strRsts + "|"
                            End With

                            arlPrint.Add(objPat)

                            With objPat2
                                .alItem = arlItem

                                .sTNms = strTnms + "Back Typing"
                                .sRsts = strRsts + "|"
                            End With

                            arlPrint_typ.Add(objPat2)

                        Else
                            Dim objPat As New FGS13_PATINFO

                            With objPat
                                .alItem = arlItem

                                .sTNms = strTnms
                                .sRsts = strRsts
                            End With

                            arlPrint.Add(objPat)
                        End If
                        '>
                        'Dim objPat As New FGS13_PATINFO

                        'With objPat
                        '    .alItem = arlItem

                        '    .sTNms = strTnms
                        '    .sRsts = strRsts
                        'End With

                        'arlPrint.Add(objPat)
                    End If
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGS13_PRINT
                If mbMicroBioYn = False Then '< 20121012 미생물출력시 세로로 셋팅 
                    prt.mbLandscape = True  '-- false : 세로, true : 가로
                    '  prt.mbLandscape = False  '-- false : 세로, true : 가로
                Else
                    prt.mbLandscape = False   '-- false : 세로, true : 가로
                End If

                prt.msTitle = "Work List"
                prt.msJobGbn = cboQrygbn.Text
                prt.maPrtData = arlPrint
                prt.miTotExmCnt = spdList.MaxCols - spdList.GetColFromID("spcnmd")
                prt.mbUseBarNo = Me.chkBar_view.Checked
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

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

            '<jjh ABO, RH TYPING 
            If arlPrint_typ.Count > 0 Then
                Dim prt As New FGS13_PRINT
                If mbMicroBioYn = False Then '< 20121012 미생물출력시 세로로 셋팅 
                    prt.mbLandscape = True  '-- false : 세로, true : 가로
                    '  prt.mbLandscape = False  '-- false : 세로, true : 가로
                Else
                    prt.mbLandscape = False   '-- false : 세로, true : 가로
                End If

                prt.msTitle = "Work List"
                prt.msJobGbn = cboQrygbn.Text
                prt.maPrtData = arlPrint_typ
                prt.miTotExmCnt = spdList.MaxCols - spdList.GetColFromID("spcnmd")
                prt.mbUseBarNo = Me.chkBar_view.Checked
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

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
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbPrint_ws()

        Try

            If chkMbtType.Checked Then
                sbPrint_ws_microbio()
            Else
                If m_al_SheetList.Contains("ws_" + Ctrl.Get_Code(Me.cboWkGrp).ToLower) Then
                    sbPrint_ws_wgrp(Ctrl.Get_Code(Me.cboWkGrp))
                ElseIf m_al_SheetList.Contains("ts_" + Ctrl.Get_Code(Me.cboTGrp).ToLower) Then
                    sbPrint_ws_tgrp(Ctrl.Get_Code(Me.cboTGrp))
                End If
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbPrint_ws_wgrp(ByVal rsWgrpCd As String)


        Select Case rsWgrpCd
            Case "BM" : sbPrint_ws_wgrp_pb()
            Case "PB" : sbPrint_ws_wgrp_pb()
            Case "CY" : sbPrint_ws_wgrp_cy()
            Case "GS" : sbPrint_ws_wgrp_gram("ws")
        End Select
    End Sub

    Private Sub sbPrint_ws_tgrp(ByVal rsTgrpCd As String)
        Dim sTestcdTag As String = Me.txtSelTest.Tag.ToString

        Dim sTestArr As String() = sTestcdTag.Split("^"c)

        Select Case rsTgrpCd
            Case "GS" : sbPrint_ws_wgrp_gram("ws")
            Case "H4"
                For i = 0 To sTestArr.Length - 1
                    If sTestArr(i) = "LH411" Then 'Lymphocyte Subset 일경우 이 시트지 출력
                        sbPrint_ws_wgrp_lym("ts")
                    End If
                Next
        End Select
    End Sub

    Private Sub sbPrint_ws_microbio()

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(Windows.Forms.Application.StartupPath + "\SSF\ws_micro.xls")
            xlsWkS = CType(xlsWkB.ActiveSheet, Excel.Worksheet)

            Dim iCnt As Integer = 0

            With Me.spdList
                If .ActiveCol = .GetColFromID("chk") Then
                    .SetActiveCell(.GetColFromID("chk") + 1, .ActiveRow)
                End If

                Dim iCntChk As Integer = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iCntChk < 1 Then
                    MsgBox("선택한 자료가 없습니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
                    Return
                End If

                For iRow As Integer = 1 To .MaxRows
                    If Ctrl.Get_Code(Me.spdList, "chk", iRow) = "1" Then
                        iCnt += 1

                        If iCnt Mod 5 = 1 Then
                            For ix As Integer = 1 To 5
                                xlsWkS.Range("B" + (1 + 13 * (ix - 1)).ToString).Value = ""
                                xlsWkS.Range("F" + (1 + 13 * (ix - 1)).ToString).Value = ""
                                xlsWkS.Range("H" + (1 + 13 * (ix - 1)).ToString).Value = ""
                                xlsWkS.Range("K" + (1 + 13 * (ix - 1)).ToString).Value = ""
                                xlsWkS.Range("N" + (1 + 13 * (ix - 1)).ToString).Value = ""

                                xlsWkS.Range("B" + (2 + 13 * (ix - 1)).ToString).Value = ""
                                xlsWkS.Range("F" + (2 + 13 * (ix - 1)).ToString).Value = ""
                                xlsWkS.Range("H" + (2 + 13 * (ix - 1)).ToString).Value = ""
                                xlsWkS.Range("L" + (2 + 13 * (ix - 1)).ToString).Value = ""

                                xlsWkS.Range("B" + (3 + 13 * (ix - 1)).ToString).Value = ""
                                xlsWkS.Range("F" + (3 + 13 * (ix - 1)).ToString).Value = ""

                                xlsWkS.Range("B" + (4 + 13 * (ix - 1)).ToString).Value = ""
                            Next
                        End If

                        Dim sPrtBcNo As String = Ctrl.Get_Code(Me.spdList, "prtbcno", iRow)
                        Dim sWorkNo As String = Ctrl.Get_Code(Me.spdList, "workno", iRow)
                        Dim sRegNo As String = Ctrl.Get_Code(Me.spdList, "regno", iRow) '등록번호 
                        Dim sPatNm As String = Ctrl.Get_Code(Me.spdList, "patnm", iRow)
                        Dim sSexAge As String = Ctrl.Get_Code(Me.spdList, "sexage", iRow)
                        Dim sDptInfo As String = Ctrl.Get_Code(Me.spdList, "deptinfo", iRow)
                        Dim sDoctor As String = Ctrl.Get_Code(Me.spdList, "doctornm", iRow)
                        Dim sSpcNm As String = Ctrl.Get_Code(Me.spdList, "spcnmd", iRow)  '검체병 
                        Dim sDiagnm As String = Ctrl.Get_Code(Me.spdList, "diagnm", iRow) '상병명 
                        Dim sDocRmk As String = Ctrl.Get_Code(Me.spdList, "doctorrmk", iRow) '의사 Remark 

                        Dim sBcNo As String = Ctrl.Get_Code(Me.spdList, "bcno", iRow)

                        xlsWkS.Range("B" + (1 + 13 * (iCnt - 1)).ToString).Value = sWorkNo
                        xlsWkS.Range("F" + (1 + 13 * (iCnt - 1)).ToString).Value = sRegNo
                        xlsWkS.Range("H" + (1 + 13 * (iCnt - 1)).ToString).Value = sPatNm
                        xlsWkS.Range("K" + (1 + 13 * (iCnt - 1)).ToString).Value = sSexAge
                        xlsWkS.Range("N" + (1 + 13 * (iCnt - 1)).ToString).Value = sPrtBcNo

                        xlsWkS.Range("B" + (2 + 13 * (iCnt - 1)).ToString).Value = sBcNo
                        xlsWkS.Range("F" + (2 + 13 * (iCnt - 1)).ToString).Value = sDptInfo
                        xlsWkS.Range("H" + (2 + 13 * (iCnt - 1)).ToString).Value = sDoctor
                        xlsWkS.Range("L" + (2 + 13 * (iCnt - 1)).ToString).Value = "*" + sPrtBcNo + "*"


                        xlsWkS.Range("B" + (3 + 13 * (iCnt - 1)).ToString).Value = sSpcNm
                        xlsWkS.Range("F" + (3 + 13 * (iCnt - 1)).ToString).Value = sDiagnm

                        xlsWkS.Range("B" + (4 + 13 * (iCnt - 1)).ToString).Value = sDocRmk

                        If iCnt Mod 5 = 0 Then
                            iCnt = 0
                            xlsWkS.PrintOut()
                        End If

                    End If
                Next

                If iCnt Mod 5 > 0 Then xlsWkS.PrintOut()

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub
    '20210415 jhs 체액검사 worksheet 출력
    Private Sub sbPrint_ws_BFTest(ByVal rsExcelChk As Boolean)

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(Windows.Forms.Application.StartupPath + "\SSF\ws_BFTest.xlsx")
            xlsWkS = CType(xlsWkB.ActiveSheet, Excel.Worksheet)

            '파일열기 전 초기화
            xlsWkS.Range("H4:H9").Value = ""
            xlsWkS.Range("C13:C14").Value = ""
            xlsWkS.Range("B25:B30").Value = ""
            xlsWkS.Range("D15:D31").Value = ""
            xlsWkS.Range("C25:C34").Value = ""
            xlsWkS.Range("D35:D37").Value = ""
            xlsWkS.Range("D41:F51").Value = ""
            xlsWkS.Range("H41:H51").Value = ""
            xlsWkS.Range("B56:D56").Value = ""
            xlsWkS.Range("B54").Value = ""
            xlsWkS.Range("B58").Value = ""
            xlsWkS.Range("B60").Value = ""
            xlsWkS.Range("D15").Value = ""
            xlsWkS.Range("H58:I58").Value = ""
            xlsWkS.Range("H60:I60").Value = ""


            Dim iCnt As Integer = 0

            With Me.spdList
                If .ActiveCol = .GetColFromID("chk") Then
                    .SetActiveCell(.GetColFromID("chk") + 1, .ActiveRow)
                End If

                Dim iCntChk As Integer = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iCntChk < 1 Then
                    MsgBox("선택한 자료가 없습니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
                    Return
                End If

                For iRow As Integer = 1 To .MaxRows
                    If Ctrl.Get_Code(Me.spdList, "chk", iRow) = "1" Then
                        iCnt += 1
                        Dim dt As DataTable
                        Dim sBcno As String = Ctrl.Get_Code(Me.spdList, "bcno", iRow).Replace("-", "") '의뢰처
                        dt = LISAPP.COMM.RstFn.fnGet_PatInfo(sBcno, "")

                        Dim sWkNo As String = Ctrl.Get_Code(Me.spdList, "workno", iRow)   '작업번호
                        Dim sOrddt As String = dt.Rows(0).Item("ORDDT").ToString
                        Dim sPatnm As String = Ctrl.Get_Code(Me.spdList, "patnm", iRow) '환자성명
                        Dim sRegNo As String = Ctrl.Get_Code(Me.spdList, "regno", iRow)   '등록번호
                        Dim sSexAge As String = Ctrl.Get_Code(Me.spdList, "sexage", iRow) '성별나이
                        Dim sDept As String = Ctrl.Get_Code(Me.spdList, "deptinfo", iRow) '의뢰처

                        Dim diagnm As String = Ctrl.Get_Code(Me.spdList, "diagnm", iRow)

                        xlsWkS.Range("H4").Value = sBcno
                        xlsWkS.Range("H5").Value = sOrddt
                        xlsWkS.Range("H6").Value = sPatnm
                        xlsWkS.Range("H7").Value = sRegNo
                        xlsWkS.Range("H8").Value = sSexAge
                        xlsWkS.Range("H9").Value = sDept

                        'Dim msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest(sBcno, "")
                        xlsWkS.Range("C13").Value = dt.Rows(0).Item("diagnm").ToString
                        xlsWkS.Range("C32").Value = dt.Rows(0).Item("tkdt").ToString
                        xlsWkS.Range("B25").Value = dt.Rows(0).Item("spcnm").ToString
                        xlsWkS.Range("C25").Value = "O"

                        'cytospin
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest(sBcno, "L1778")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("D15").Value = dt.Rows(0).Item("fndt").ToString
                        End If

                        '검체종류 중 LDH체액검사 체액
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest(sBcno, "LH524")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("D26").Value = "LDH[체액]  " + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                        Else
                            xlsWkS.Range("D26").Value = "LDH[체액]  " + "x"
                        End If

                        '검체종류 중 LDH체액검사 Blood
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest_spc(sRegNo, "LH105", "S01")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("D27").Value = "LDH[Blood]  " + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                        Else
                            xlsWkS.Range("D27").Value = "LDH[Blood]  " + "x"
                        End If

                        '핵의학과 내용
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest_rr(sBcno, "'LR305', 'LR303'")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("D28").Value = dt.Rows(0).Item("tnm").ToString + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                        End If

                        'D29 내용 조합하여 한번에 출력 
                        Dim tempcont As String = ""
                        'CEA
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest_spc(sRegNo, "LC511", "S01")
                        If dt.Rows.Count > 0 Then
                            tempcont = dt.Rows(0).Item("tnmd").ToString + "-" + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                        End If
                        'CA19-9
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest_spc(sRegNo, "LC512", "S01")
                        If dt.Rows.Count > 0 Then
                            If tempcont <> "" Then
                                tempcont += ", " + dt.Rows(0).Item("tnmd").ToString + "-" + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                            Else
                                tempcont = dt.Rows(0).Item("tnmd").ToString + "-" + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                            End If
                        End If
                        'CA125
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest_spc(sRegNo, "LC513", "S01")
                        If dt.Rows.Count > 0 Then
                            If tempcont <> "" Then
                                tempcont += ", " + dt.Rows(0).Item("tnmd").ToString + "-" + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                            Else
                                tempcont = dt.Rows(0).Item("tnmd").ToString + "-" + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                            End If
                        End If
                        'PAS(Blood)
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest_spc(sRegNo, "LH375", "S01")
                        If dt.Rows.Count > 0 Then
                            If tempcont <> "" Then
                                tempcont += ", " + dt.Rows(0).Item("tnmd").ToString + "-" + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                            Else
                                tempcont = dt.Rows(0).Item("tnmd").ToString + "-" + dt.Rows(0).Item("viewrst").ToString + " / " + dt.Rows(0).Item("rstunit").ToString
                            End If
                        End If
                        xlsWkS.Range("D29").Value = tempcont




                        'RBC count
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest(sBcno, "LH51115")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("C33").Value = dt.Rows(0).Item("viewrst").ToString
                        End If

                        'WBC count
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest(sBcno, "LH51116")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("C34").Value = dt.Rows(0).Item("viewrst").ToString
                        End If

                        'NewutroPhils
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest(sBcno, "LH51117")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("D35").Value = dt.Rows(0).Item("viewrst").ToString + "%"
                        End If

                        'Lymphocytes
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest(sBcno, "LH51118")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("D36").Value = dt.Rows(0).Item("viewrst").ToString + "%"
                        End If

                        'Other cells
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_BFtest(sBcno, "LH51119")
                        If dt.Rows.Count > 0 Then
                            xlsWkS.Range("D37").Value = dt.Rows(0).Item("viewrst").ToString + "%"
                        End If


                        '20210714 jhs toexcel 기능 만들기
                        If rsExcelChk Then
                            '폴더 있는지 확인후 없으면 만들기 
                            Dim filePath = "C:\ACK\CYTOSPIN\" + Replace(DateTime.Now.ToString("yyyy-MM-dd"), "-", "") + "_Excel"
                            If Directory.Exists(filePath) = False Then
                                MkDir(filePath)
                            End If

                            '기존에 파일 있는지 확인 후 삭제 
                            Dim filename As String = filePath + "\" + sBcno + ".xlsx"
                            If File.Exists(filename) = True Then
                                File.Delete(filename)
                            End If
                            xlsWkS.SaveAs(filename)

                            MsgBox("파일생성 완료 : " + vbCrLf + filename)
                        Else
                            xlsWkS.PrintOut()
                        End If
                        '---------------------------------------------
                    End If
                Next
            End With
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub
    '------------------------------------------------------------------------
    Private Sub sbPrint_ws_wgrp_pb()

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(Windows.Forms.Application.StartupPath + "\SSF\ws_pb.xls")
            xlsWkS = CType(xlsWkB.ActiveSheet, Excel.Worksheet)

            '파일열기 전 초기화
            xlsWkS.Range("C3").Value = "" : xlsWkS.Range("E3").Value = "" : xlsWkS.Range("H3").Value = ""
            xlsWkS.Range("J3").Value = "" : xlsWkS.Range("C4").Value = "" : xlsWkS.Range("H4").Value = ""
            xlsWkS.Range("H6").Value = "" : xlsWkS.Range("J7").Value = ""
            xlsWkS.Range("H7").Value = "" : xlsWkS.Range("B9").Value = ""
            xlsWkS.Range("D34").Value = "" : xlsWkS.Range("F34").Value = "" : xlsWkS.Range("B19").Value = ""
            xlsWkS.Range("B20").Value = "" : xlsWkS.Range("B21").Value = "" : xlsWkS.Range("B22").Value = ""
            xlsWkS.Range("B23").Value = "" : xlsWkS.Range("B24").Value = "" : xlsWkS.Range("B25").Value = ""
            xlsWkS.Range("B26").Value = "" : xlsWkS.Range("B27").Value = "" : xlsWkS.Range("B28").Value = ""
            xlsWkS.Range("B29").Value = "" : xlsWkS.Range("H20").Value = "" : xlsWkS.Range("H21").Value = ""
            xlsWkS.Range("H22").Value = "" : xlsWkS.Range("H23").Value = "" : xlsWkS.Range("H24").Value = ""
            xlsWkS.Range("H26").Value = "" : xlsWkS.Range("H28").Value = "" : xlsWkS.Range("H29").Value = ""
            xlsWkS.Range("C36").Value = "" : xlsWkS.Range("C38").Value = "" : xlsWkS.Range("C40").Value = ""
            xlsWkS.Range("C42").Value = "" : xlsWkS.Range("E36").Value = "" : xlsWkS.Range("E38").Value = ""
            xlsWkS.Range("E40").Value = "" : xlsWkS.Range("E42").Value = "" : xlsWkS.Range("H36").Value = ""
            xlsWkS.Range("J36").Value = "" : xlsWkS.Range("L36").Value = "" : xlsWkS.Range("I38").Value = ""
            xlsWkS.Range("L38").Value = "" : xlsWkS.Range("I40").Value = "" : xlsWkS.Range("L40").Value = ""
            xlsWkS.Range("C44").Value = "" : xlsWkS.Range("H4").Value = "" : xlsWkS.Range("D6").Value = "(        ,        ):"
            xlsWkS.Range("D7").Value = "(        ,        ):"
            xlsWkS.Range("E19").Value = "(       )" : xlsWkS.Range("E21").Value = "(       kg/mon)" : xlsWkS.Range("E26").Value = "(       )"
            xlsWkS.Range("K26").Value = "(       )" : xlsWkS.Range("K27").Value = "(       )" : xlsWkS.Range("K28").Value = "(       )"
            xlsWkS.Range("K29").Value = "(       )" : xlsWkS.Range("H32").Value = "(       )" : xlsWkS.Range("K32").Value = "(       )"
            xlsWkS.Range("D34").Value = "" : xlsWkS.Range("F34").Value = "" : xlsWkS.Range("H41").Value = "" : xlsWkS.Range("C43").Value = ""

            Dim iCnt As Integer = 0

            With Me.spdList
                If .ActiveCol = .GetColFromID("chk") Then
                    .SetActiveCell(.GetColFromID("chk") + 1, .ActiveRow)
                End If

                Dim iCntChk As Integer = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iCntChk < 1 Then
                    MsgBox("선택한 자료가 없습니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
                    Return
                End If

                For iRow As Integer = 1 To .MaxRows
                    If Ctrl.Get_Code(Me.spdList, "chk", iRow) = "1" Then
                        iCnt += 1

                        Dim sRegNo As String = Ctrl.Get_Code(Me.spdList, "regno", iRow) '등록번호
                        Dim sBcNo As String = Ctrl.Get_Code(Me.spdList, "bcno", iRow).Replace("-", "")
                        Dim sWkNo As String = Ctrl.Get_Code(Me.spdList, "workno", iRow)

                        Dim msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

                        Dim dt As New DataTable
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_PbBm(sBcNo)

                        Dim sPrtBcNo As String = Ctrl.Get_Code(Me.spdList, "prtbcno", iRow)
                        Dim sWorkNo As String = Ctrl.Get_Code(Me.spdList, "workno", iRow)
                        Dim sPatNm As String = Ctrl.Get_Code(Me.spdList, "patnm", iRow)
                        Dim sSexAge As String = Ctrl.Get_Code(Me.spdList, "sexage", iRow) '20140128 정선영 추가, 성별, 나이 스프레드에서 받아올 수 있도록.
                        Dim sDptInfo As String = Ctrl.Get_Code(Me.spdList, "deptinfo", iRow)
                        Dim sDoctor As String = Ctrl.Get_Code(Me.spdList, "doctornm", iRow)
                        Dim sSpcNm As String = Ctrl.Get_Code(Me.spdList, "spcnmd", iRow)  '검체명
                        Dim sDiagnm As String = Ctrl.Get_Code(Me.spdList, "diagnm", iRow) '상병명 
                        Dim sDocRmk As String = Ctrl.Get_Code(Me.spdList, "doctorrmk", iRow) '의사 Remark
                        Dim sDate As String = Ctrl.Get_Code(Me.spdList, "orddt", iRow) '20140128 정선영 추가, 의뢰일자(처방일)
                        ' Dim sDate As String = ""

                        If dt.Rows.Count = 0 Then

                            xlsWkS.Range("H4").Value = ""
                            xlsWkS.Range("D6").Value = "(        ,        ):"
                            xlsWkS.Range("D7").Value = "(        ,        ):" : xlsWkS.Range("B9").Value = ""
                            xlsWkS.Range("E19").Value = "(       )" : xlsWkS.Range("E21").Value = "(       kg/mon)" : xlsWkS.Range("E26").Value = "(       )"
                            xlsWkS.Range("K26").Value = "(       )" : xlsWkS.Range("K27").Value = "(       )" : xlsWkS.Range("K28").Value = "(       )"
                            xlsWkS.Range("K29").Value = "(       )" : xlsWkS.Range("H32").Value = "(       )" : xlsWkS.Range("K32").Value = "(       )"
                            xlsWkS.Range("D34").Value = "" : xlsWkS.Range("F34").Value = "" : xlsWkS.Range("B19").Value = " □ "
                            xlsWkS.Range("B20").Value = " □ " : xlsWkS.Range("B21").Value = " □ " : xlsWkS.Range("B22").Value = " □ "
                            xlsWkS.Range("B23").Value = " □ " : xlsWkS.Range("B24").Value = " □ " : xlsWkS.Range("B25").Value = " □ "
                            xlsWkS.Range("B26").Value = " □ " : xlsWkS.Range("B27").Value = " □ " : xlsWkS.Range("B28").Value = " □ "
                            xlsWkS.Range("B29").Value = " □ " : xlsWkS.Range("H20").Value = " □ " : xlsWkS.Range("H21").Value = " □ "
                            xlsWkS.Range("H22").Value = " □ " : xlsWkS.Range("H23").Value = " □ " : xlsWkS.Range("H24").Value = " □ "
                            xlsWkS.Range("H26").Value = " □ " : xlsWkS.Range("H28").Value = " □ " : xlsWkS.Range("H29").Value = " □ "

                        Else
                            'Dim sDage As Integer = CInt(dt.Rows(0).Item("dage"))

                            'If sDage / 365 < 1 Then
                            '    If sDage Mod 365 < 31 Then
                            '        xlsWkS.Range("H3").Value = dt.Rows(0).Item("sex").ToString + "/" + (sDage Mod 365).ToString + "d"
                            '    Else
                            '        xlsWkS.Range("H3").Value = dt.Rows(0).Item("sex").ToString + "/" + CInt(sDage / 30).ToString + "m"
                            '    End If
                            'Else
                            '    xlsWkS.Range("H3").Value = dt.Rows(0).Item("sex").ToString + "/" + dt.Rows(0).Item("age").ToString
                            'End If

                            'sDate = dt.Rows(0).Item("orddt").ToString.Substring(0, 8)
                            'xlsWkS.Range("H4").Value = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2)

                            If dt.Rows(0).Item("lymphosize").ToString <> "" Then
                                xlsWkS.Range("K26").Value = "( " + dt.Rows(0).Item("lymphosize").ToString + " )"
                            Else

                            End If

                            If dt.Rows(0).Item("lymphosite").ToString <> "" Then
                                xlsWkS.Range("K27").Value = "( " + dt.Rows(0).Item("lymphosite").ToString + " )"
                            Else

                            End If

                            If dt.Rows(0).Item("hepatosize").ToString <> "" Then
                                xlsWkS.Range("K28").Value = "( " + dt.Rows(0).Item("hepatosize").ToString + " )"
                            Else

                            End If

                            If dt.Rows(0).Item("splenosize").ToString <> "" Then
                                xlsWkS.Range("K29").Value = "( " + dt.Rows(0).Item("splenosize").ToString + " )"
                            Else

                            End If

                            If dt.Rows(0).Item("drug").ToString <> "" Then
                                xlsWkS.Range("H32").Value = "( " + dt.Rows(0).Item("drug").ToString + " )"
                            Else

                            End If

                            If dt.Rows(0).Item("duration").ToString <> "" Then
                                xlsWkS.Range("K32").Value = "( " + dt.Rows(0).Item("duration").ToString + " )"
                            Else

                            End If

                            If dt.Rows(0).Item("fevertxt").ToString <> "" Then
                                xlsWkS.Range("E19").Value = "( " + dt.Rows(0).Item("fevertxt").ToString + " )"
                            Else

                            End If

                            If dt.Rows(0).Item("weighttxt").ToString <> "" Then
                                xlsWkS.Range("E21").Value = "( " + dt.Rows(0).Item("weighttxt").ToString + " kg/mon)"
                            Else

                            End If

                            If dt.Rows(0).Item("abdosize").ToString <> "" Then
                                xlsWkS.Range("E26").Value = "( " + dt.Rows(0).Item("abdosize").ToString + " )"
                            Else

                            End If

                            If dt.Rows(0).Item("PBR").ToString = "1" Then
                                xlsWkS.Range("D6").Value = "(    +    ,        ):"
                            ElseIf dt.Rows(0).Item("PBR").ToString = "2" Then
                                xlsWkS.Range("D6").Value = "(         ,    -   ):"
                            Else

                            End If

                            If dt.Rows(0).Item("BMR").ToString = "1" Then
                                xlsWkS.Range("D7").Value = "(    +    ,        ):"
                            ElseIf dt.Rows(0).Item("BMR").ToString = "2" Then
                                xlsWkS.Range("D7").Value = "(         ,    -   ):"
                            Else

                            End If

                            xlsWkS.Range("H6").Value = dt.Rows(0).Item("pbsdate").ToString
                            xlsWkS.Range("H7").Value = dt.Rows(0).Item("bmsdate").ToString
                            xlsWkS.Range("J7").Value = dt.Rows(0).Item("slideno").ToString
                            xlsWkS.Range("B9").Value = dt.Rows(0).Item("diagname").ToString

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(0, 1) = "Y" Then
                                xlsWkS.Range("B19").Value = " ■ "
                            Else
                                xlsWkS.Range("B19").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(1, 1) = "Y" Then
                                xlsWkS.Range("B20").Value = " ■ "
                            Else
                                xlsWkS.Range("B20").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(2, 1) = "Y" Then
                                xlsWkS.Range("B21").Value = " ■ "
                            Else
                                xlsWkS.Range("B21").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(3, 1) = "Y" Then
                                xlsWkS.Range("B22").Value = " ■ "
                            Else
                                xlsWkS.Range("B22").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(4, 1) = "Y" Then
                                xlsWkS.Range("B23").Value = " ■ "
                            Else
                                xlsWkS.Range("B23").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(5, 1) = "Y" Then
                                xlsWkS.Range("B24").Value = " ■ "
                            Else
                                xlsWkS.Range("B24").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(6, 1) = "Y" Then
                                xlsWkS.Range("B25").Value = " ■ "
                            Else
                                xlsWkS.Range("B25").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(7, 1) = "Y" Then
                                xlsWkS.Range("B26").Value = " ■ "
                            Else
                                xlsWkS.Range("B26").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(8, 1) = "Y" Then
                                xlsWkS.Range("B27").Value = " ■ "
                            Else
                                xlsWkS.Range("B27").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(9, 1) = "Y" Then
                                xlsWkS.Range("B28").Value = " ■ "
                            Else
                                xlsWkS.Range("B28").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(10, 1) = "Y" Then
                                xlsWkS.Range("B29").Value = " ■ "
                            Else
                                xlsWkS.Range("B29").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(11, 1) = "Y" Then
                                xlsWkS.Range("H20").Value = " ■ "
                            Else
                                xlsWkS.Range("H20").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(12, 1) = "Y" Then
                                xlsWkS.Range("H21").Value = " ■ "
                            Else
                                xlsWkS.Range("H21").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(13, 1) = "Y" Then
                                xlsWkS.Range("H22").Value = " ■ "
                            Else
                                xlsWkS.Range("H22").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(14, 1) = "Y" Then
                                xlsWkS.Range("H23").Value = " ■ "
                            Else
                                xlsWkS.Range("H23").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(15, 1) = "Y" Then
                                xlsWkS.Range("H24").Value = " ■ "
                            Else
                                xlsWkS.Range("H24").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(16, 1) = "Y" Then
                                xlsWkS.Range("H26").Value = " ■ "
                            Else
                                xlsWkS.Range("H26").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(17, 1) = "Y" Then
                                xlsWkS.Range("H28").Value = " ■ "
                            Else
                                xlsWkS.Range("H28").Value = " □ "
                            End If

                            If dt.Rows(0).Item("ITEMIDX").ToString.Substring(18, 1) = "Y" Then
                                xlsWkS.Range("H29").Value = " ■ "
                            Else
                                xlsWkS.Range("H29").Value = " □ "
                            End If
                        End If

                        Dim dt2 As New DataTable
                        dt2 = LISAPP.APP_S.WkFn.fnget_worklist_pastrst(sBcNo)

                        xlsWkS.Range("E3").Value = sPatNm
                        xlsWkS.Range("C3").Value = sRegNo
                        xlsWkS.Range("C4").Value = sDptInfo
                        xlsWkS.Range("H3").Value = sSexAge '20140128 정선영 추가. 성별, 나이
                        xlsWkS.Range("H4").Value = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2) '20140128 정선영 추가, 의뢰일자
                        xlsWkS.Range("J3").Value = sDoctor

                        If dt2.Rows.Count = 0 Then

                            xlsWkS.Range("C36").Value = ""
                            xlsWkS.Range("E36").Value = ""
                            xlsWkS.Range("H36").Value = ""
                            xlsWkS.Range("J36").Value = ""
                            xlsWkS.Range("L36").Value = ""
                            xlsWkS.Range("C38").Value = ""
                            xlsWkS.Range("E38").Value = ""
                            xlsWkS.Range("I38").Value = ""
                            xlsWkS.Range("L38").Value = ""
                            xlsWkS.Range("C40").Value = ""
                            xlsWkS.Range("E40").Value = ""
                            xlsWkS.Range("I40").Value = ""
                            xlsWkS.Range("C42").Value = ""
                            xlsWkS.Range("C43").Value = ""
                            xlsWkS.Range("E42").Value = ""
                            xlsWkS.Range("C44").Value = ""
                            xlsWkS.Range("F34").Value = ""
                            xlsWkS.Range("L40").Value = ""
                            xlsWkS.Range("H41").Value = ""
                        Else
                            Dim sFe As String = ""
                            Dim sTibc As String = ""
                            Dim sProtein As String = ""
                            Dim sAlbumin As String = ""
                            Dim sProteinBfrst As String = ""
                            Dim sAlbuminBfrst As String = ""
                            Dim sRetiBfrst As String = ""
                            Dim SFedt As String = ""
                            Dim sTibcdt As String = ""

                            For i = 0 To dt2.Rows.Count - 1

                                Select Case dt2.Rows(i).Item("testcd").ToString
                                    Case "LH102"
                                        xlsWkS.Range("C36").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LH105"
                                        xlsWkS.Range("E36").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LH101"
                                        xlsWkS.Range("H36").Value = dt2.Rows(i).Item("viewrst").ToString
                                        Dim sBffndt As String = dt2.Rows(i).Item("bffndt").ToString
                                        xlsWkS.Range("F34").Value = sBffndt.Substring(0, 4) + "-" + sBffndt.Substring(4, 2) + "-" + sBffndt.Substring(6, 2)
                                    Case "LH109"
                                        xlsWkS.Range("J36").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LC124"
                                        sFe = dt2.Rows(i).Item("viewrst").ToString
                                        SFedt = dt2.Rows(i).Item("bffndt").ToString
                                    Case "LC125"
                                        sTibc = dt2.Rows(i).Item("viewrst").ToString
                                        sTibcdt = dt2.Rows(i).Item("bffndt").ToString
                                    Case "LH103"
                                        xlsWkS.Range("C38").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LH106"
                                        xlsWkS.Range("E38").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LH121"
                                        xlsWkS.Range("K41").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "L1299"
                                        xlsWkS.Range("L38").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LH104"
                                        xlsWkS.Range("C40").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LH107"
                                        xlsWkS.Range("E40").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LC118"
                                        sProtein = dt2.Rows(i).Item("viewrst").ToString
                                        sProteinBfrst = dt2.Rows(i).Item("bffndt").ToString
                                        sProteinBfrst = sProteinBfrst.Substring(0, 4) + "-" + sProteinBfrst.Substring(4, 2) + "-" + sProteinBfrst.Substring(6, 2)
                                    Case "LC119"
                                        sAlbumin = dt2.Rows(i).Item("viewrst").ToString
                                        sAlbuminBfrst = dt2.Rows(i).Item("bffndt").ToString
                                        sAlbuminBfrst = sAlbuminBfrst.Substring(0, 4) + "-" + sAlbuminBfrst.Substring(4, 2) + "-" + sAlbuminBfrst.Substring(6, 2)
                                    Case "LH123"
                                        xlsWkS.Range("C42").Value = dt2.Rows(i).Item("viewrst").ToString
                                        sRetiBfrst = dt2.Rows(i).Item("bffndt").ToString
                                        sRetiBfrst = sRetiBfrst.Substring(0, 4) + "-" + sRetiBfrst.Substring(4, 2) + "-" + sRetiBfrst.Substring(6, 2)
                                        xlsWkS.Range("C43").Value = "'" + sRetiBfrst '+ "'"

                                    Case "LH108"
                                        xlsWkS.Range("E42").Value = dt2.Rows(i).Item("viewrst").ToString
                                    Case "LH124"
                                        xlsWkS.Range("C44").Value = dt2.Rows(i).Item("viewrst").ToString
                                        
                                    Case "LH378"

                                        '   If dt2.Rows(i).Item("viewrst").ToString <> "" Then
                                        xlsWkS.Range("I43").Value = dt2.Rows(i).Item("viewrst").ToString
                                        xlsWkS.Range("H44").Value = dt2.Rows(i).Item("bffndt").ToString
                                        '   End If
                                End Select

                                'Select Case dt2.Rows(i).Item("testcd").ToString
                                '    Case "LH102"
                                '        xlsWkS.Range("C36").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LH105"
                                '        xlsWkS.Range("E36").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LH101"
                                '        xlsWkS.Range("H36").Value = dt2.Rows(i).Item("viewrst").ToString
                                '        Dim sBffndt As String = dt2.Rows(i).Item("bffndt").ToString
                                '        xlsWkS.Range("F34").Value = sBffndt.Substring(0, 4) + "-" + sBffndt.Substring(4, 2) + "-" + sBffndt.Substring(6, 2)
                                '    Case "LH109"
                                '        xlsWkS.Range("J36").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LC124"
                                '        sFe = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LC125"
                                '        sTibc = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LH103"
                                '        xlsWkS.Range("C38").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LH106"
                                '        xlsWkS.Range("E38").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LH121"
                                '        xlsWkS.Range("I38").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "L1299"
                                '        xlsWkS.Range("L38").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LH104"
                                '        xlsWkS.Range("C40").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LH107"
                                '        xlsWkS.Range("E40").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LC118"
                                '        sProtein = dt2.Rows(i).Item("viewrst").ToString
                                '        sProteinBfrst = dt2.Rows(i).Item("bffndt").ToString
                                '        sProteinBfrst = sProteinBfrst.Substring(0, 4) + "-" + sProteinBfrst.Substring(4, 2) + "-" + sProteinBfrst.Substring(6, 2)
                                '    Case "LC119"
                                '        sAlbumin = dt2.Rows(i).Item("viewrst").ToString
                                '        sAlbuminBfrst = dt2.Rows(i).Item("bffndt").ToString
                                '        sAlbuminBfrst = sAlbuminBfrst.Substring(0, 4) + "-" + sAlbuminBfrst.Substring(4, 2) + "-" + sAlbuminBfrst.Substring(6, 2)
                                '    Case "LH123"
                                '        xlsWkS.Range("C42").Value = dt2.Rows(i).Item("viewrst").ToString
                                '        sRetiBfrst = dt2.Rows(i).Item("bffndt").ToString
                                '        sRetiBfrst = sRetiBfrst.Substring(0, 4) + "-" + sRetiBfrst.Substring(4, 2) + "-" + sRetiBfrst.Substring(6, 2)
                                '        xlsWkS.Range("C43").Value = "'" + sRetiBfrst '+ "'"

                                '    Case "LH108"
                                '        xlsWkS.Range("E42").Value = dt2.Rows(i).Item("viewrst").ToString
                                '    Case "LH124"
                                '        xlsWkS.Range("C44").Value = dt2.Rows(i).Item("viewrst").ToString
                                'End Select

                            Next

                            'xlsWkS.Range("L36").Value = sFe + "  /  " + sTibc
                            'xlsWkS.Range("K37").Value = SFedt + "  /  " + sTibcdt
                            'xlsWkS.Range("I40").Value = sProtein + "  /  " + sAlbumin
                            ''xlsWkS.Range("H41").Value = sProteinBfrst
                            'xlsWkS.Range("H41").Value = "" + "( " + sProteinBfrst + " / " + sAlbuminBfrst + " )" + ""
                            'xlsWkS.Range("J46").Value = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
                            'xlsWkS.Range("J47").Value = "출력시간: " + msTitle_Time

                            xlsWkS.Range("L36").Value = sFe + "  /  " + sTibc
                            xlsWkS.Range("K37").Value = SFedt + "  /  " + sTibcdt
                            xlsWkS.Range("I38").Value = sProtein + "  /  " + sAlbumin
                            'xlsWkS.Range("H41").Value = sProteinBfrst
                            xlsWkS.Range("I39").Value = "" + "( " + sProteinBfrst + " / " + sAlbuminBfrst + " )" + ""
                            xlsWkS.Range("J46").Value = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
                            xlsWkS.Range("J47").Value = "출력시간: " + msTitle_Time

                        End If

                        xlsWkS.PrintOut()

                        '출력 후 초기화
                        xlsWkS.Range("C3").Value = "" : xlsWkS.Range("E3").Value = "" : xlsWkS.Range("H3").Value = ""
                        xlsWkS.Range("J3").Value = "" : xlsWkS.Range("C4").Value = "" : xlsWkS.Range("H4").Value = ""
                        xlsWkS.Range("H6").Value = "" : xlsWkS.Range("J7").Value = ""
                        xlsWkS.Range("H7").Value = "" : xlsWkS.Range("B9").Value = ""
                        xlsWkS.Range("D34").Value = "" : xlsWkS.Range("F34").Value = "" : xlsWkS.Range("B19").Value = ""
                        xlsWkS.Range("B20").Value = "" : xlsWkS.Range("B21").Value = "" : xlsWkS.Range("B22").Value = ""
                        xlsWkS.Range("B23").Value = "" : xlsWkS.Range("B24").Value = "" : xlsWkS.Range("B25").Value = ""
                        xlsWkS.Range("B26").Value = "" : xlsWkS.Range("B27").Value = "" : xlsWkS.Range("B28").Value = ""
                        xlsWkS.Range("B29").Value = "" : xlsWkS.Range("H20").Value = "" : xlsWkS.Range("H21").Value = ""
                        xlsWkS.Range("H22").Value = "" : xlsWkS.Range("H23").Value = "" : xlsWkS.Range("H24").Value = ""
                        xlsWkS.Range("H26").Value = "" : xlsWkS.Range("H28").Value = "" : xlsWkS.Range("H29").Value = ""
                        xlsWkS.Range("C36").Value = "" : xlsWkS.Range("C38").Value = "" : xlsWkS.Range("C40").Value = ""
                        xlsWkS.Range("C42").Value = "" : xlsWkS.Range("E36").Value = "" : xlsWkS.Range("E38").Value = ""
                        xlsWkS.Range("E40").Value = "" : xlsWkS.Range("E42").Value = "" : xlsWkS.Range("H36").Value = ""
                        xlsWkS.Range("J36").Value = "" : xlsWkS.Range("L36").Value = "" : xlsWkS.Range("I38").Value = ""
                        xlsWkS.Range("L38").Value = "" : xlsWkS.Range("I40").Value = "" : xlsWkS.Range("L40").Value = ""
                        xlsWkS.Range("C44").Value = "" : xlsWkS.Range("H4").Value = "" : xlsWkS.Range("D6").Value = "(        ,        ):"
                        xlsWkS.Range("D7").Value = "(        ,        ):"
                        xlsWkS.Range("E19").Value = "(       )" : xlsWkS.Range("E21").Value = "(       kg/mon)" : xlsWkS.Range("E26").Value = "(       )"
                        xlsWkS.Range("K26").Value = "(       )" : xlsWkS.Range("K27").Value = "(       )" : xlsWkS.Range("K28").Value = "(       )"
                        xlsWkS.Range("K29").Value = "(       )" : xlsWkS.Range("H32").Value = "(       )" : xlsWkS.Range("K32").Value = "(       )"
                        xlsWkS.Range("D34").Value = "" : xlsWkS.Range("F34").Value = ""
                        xlsWkS.Range("H41").Value = "" : xlsWkS.Range("C43").Value = "" : xlsWkS.Range("I43").Value = "" : xlsWkS.Range("H44").Value = ""
                        xlsWkS.Range("E42").Value = "" : xlsWkS.Range("C43").Value = ""
                        xlsWkS.Range("C44").Value = "" : xlsWkS.Range("C42").Value = ""
                    End If

                Next

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub

    Private Sub sbPrint_ws_wgrp_cy()

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(Windows.Forms.Application.StartupPath + "\SSF\ws_cy.xls")
            xlsWkS = CType(xlsWkB.ActiveSheet, Excel.Worksheet)


            Dim iCnt As Integer = 0

            With Me.spdList
                If .ActiveCol = .GetColFromID("chk") Then
                    .SetActiveCell(.GetColFromID("chk") + 1, .ActiveRow)
                End If

                Dim iCntChk As Integer = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iCntChk < 1 Then
                    MsgBox("선택한 자료가 없습니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
                    Return
                End If

                For iRow As Integer = 1 To .MaxRows
                    If Ctrl.Get_Code(Me.spdList, "chk", iRow) = "1" Then
                        iCnt += 1

                        .Row = iRow
                        .Col = .GetColFromID("bcno") : Dim sBcno As String = .Text.Replace("-", "")
                        .Col = .GetColFromID("regno") : Dim sRegNo As String = .Text
                        .Col = .GetColFromID("orddt") : Dim sOrdDt As String = .Text
                        .Col = .GetColFromID("patnm") : Dim sPatNm As String = .Text
                        .Col = .GetColFromID("workno") : Dim sWkNo As String = .Text
                        .Col = .GetColFromID("deptinfo") : Dim sDeptNm As String = .Text
                        .Col = .GetColFromID("diagnm") : Dim sDiagnm As String = .Text
                        .Col = .GetColFromID("spcnmd") : Dim sSpcNm As String = .Text
                        .Col = .GetColFromID("sexage") : Dim sSexAge As String = .Text


                        '초기화
                        xlsWkS.Range("F4").Value = ""
                        xlsWkS.Range("F5").Value = ""
                        xlsWkS.Range("F6").Value = ""
                        xlsWkS.Range("F7").Value = ""
                        xlsWkS.Range("F8").Value = ""
                        xlsWkS.Range("F9").Value = ""


                        xlsWkS.Range("D15").Value = ""

                        xlsWkS.Range("C13").Value = ""
                        xlsWkS.Range("C14").Value = ""

                        xlsWkS.Range("C19").Value = ""
                        xlsWkS.Range("C20").Value = ""
                        xlsWkS.Range("C21").Value = ""
                        xlsWkS.Range("C22").Value = ""
                        xlsWkS.Range("C23").Value = ""
                        xlsWkS.Range("C24").Value = ""

                        xlsWkS.Range("A55").Value = "출력정보: "
                        xlsWkS.Range("F55").Value = "출력시간: "

                        xlsWkS.Range("F4").Value = sWkNo
                        xlsWkS.Range("F5").Value = sOrdDt
                        xlsWkS.Range("F6").Value = sPatNm
                        xlsWkS.Range("F7").Value = sRegNo
                        xlsWkS.Range("F8").Value = sSexAge
                        xlsWkS.Range("F9").Value = sDeptNm

                        Select Case sSpcNm.ToLower
                            Case "csf" : xlsWkS.Range("C19").Value = "V"
                            Case "ascites" : xlsWkS.Range("C20").Value = "V"
                            Case "pleural fluid" : xlsWkS.Range("C21").Value = "V"
                            Case "pericardial fluid" : xlsWkS.Range("C22").Value = "V"
                            Case "joint fluid" : xlsWkS.Range("C23").Value = "V"
                            Case Else : xlsWkS.Range("C24").Value = sSpcNm
                        End Select


                        Dim msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

                        Dim dt As New DataTable
                        dt = LISAPP.APP_S.WkFn.fnGet_WorkList_cs(sBcno)

                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item("diaginfo").ToString.IndexOf("|"c) >= 0 Then
                                xlsWkS.Range("C13").Value = dt.Rows(0).Item("diaginfo").ToString.Split("|"c)(1)              '-- 진단명
                                xlsWkS.Range("C14").Value = dt.Rows(0).Item("diaginfo").ToString.Split("|"c)(0)              '-- 진단명
                            End If
                        End If

                        xlsWkS.Range("A55").Value = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
                        xlsWkS.Range("F55").Value = "출력시간: " + msTitle_Time

                        xlsWkS.PrintOut()

                    End If

                Next

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub

    Private Sub sbPrint_ws_wgrp_gram(ByVal rsSheetGbn As String)

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Dim msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(Windows.Forms.Application.StartupPath + "\SSF\" + rsSheetGbn + "_GS.xls")
            xlsWkS = CType(xlsWkB.ActiveSheet, Excel.Worksheet)

            Dim iCnt As Integer

            xlsWkS.Range("M61").Value = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
            xlsWkS.Range("M62").Value = "출력시간: " + msTitle_Time

            For iCnt = 1 To 15
                xlsWkS.Range("B" + (1 + 4 * (iCnt - 1)).ToString).Value = ""
                xlsWkS.Range("B" + (2 + 4 * (iCnt - 1)).ToString).Value = ""
                xlsWkS.Range("D" + (2 + 4 * (iCnt - 1)).ToString).Value = ""
                xlsWkS.Range("B" + (3 + 4 * (iCnt - 1)).ToString).Value = ""
                xlsWkS.Range("B" + (4 + 4 * (iCnt - 1)).ToString).Value = ""

                xlsWkS.Range("I" + (1 + 4 * (iCnt - 1)).ToString).Value = ""
                xlsWkS.Range("I" + (2 + 4 * (iCnt - 1)).ToString).Value = ""
                xlsWkS.Range("K" + (2 + 4 * (iCnt - 1)).ToString).Value = ""
                xlsWkS.Range("I" + (3 + 4 * (iCnt - 1)).ToString).Value = ""
                xlsWkS.Range("I" + (4 + 4 * (iCnt - 1)).ToString).Value = ""
            Next

            iCnt = 0

            With Me.spdList
                If .ActiveCol = .GetColFromID("chk") Then
                    .SetActiveCell(.GetColFromID("chk") + 1, .ActiveRow)
                End If

                Dim iCntChk As Integer = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iCntChk < 1 Then
                    MsgBox("선택한 자료가 없습니다. 확인하여 주십시요!!", MsgBoxStyle.Information)
                    Return
                End If

                For iRow As Integer = 1 To .MaxRows
                    If Ctrl.Get_Code(Me.spdList, "chk", iRow) = "1" Then
                        iCnt += 1

                        Dim sWorkNo As String = Ctrl.Get_Code(Me.spdList, "workno", iRow)
                        Dim sRegNo As String = Ctrl.Get_Code(Me.spdList, "regno", iRow) '등록번호
                        Dim sPatNm As String = Ctrl.Get_Code(Me.spdList, "patnm", iRow)
                        Dim sBcNo As String = Ctrl.Get_Code(Me.spdList, "bcno", iRow)
                        Dim sSpcNm As String = Ctrl.Get_Code(Me.spdList, "spcnmd", iRow)  '검체명
                        Dim sPrtBcNo As String = Ctrl.Get_Code(Me.spdList, "prtbcno", iRow)

                        If iCnt < 16 Then   '왼쪽부터 찍고 오른쪽으로 넘어감.

                            xlsWkS.Range("B" + (1 + 4 * (iCnt - 1)).ToString).Value = sWorkNo
                            xlsWkS.Range("B" + (2 + 4 * (iCnt - 1)).ToString).Value = sRegNo
                            xlsWkS.Range("D" + (2 + 4 * (iCnt - 1)).ToString).Value = sPatNm
                            '<20130704 정선영 수정
                            xlsWkS.Range("B" + (3 + 4 * (iCnt - 1)).ToString).Value = sSpcNm
                            'xlsWkS.Range("B" + (4 + 4 * (iCnt - 1)).ToString).Value = sSpcNm
                            '>

                        Else

                            xlsWkS.Range("I" + (1 + 4 * (iCnt - 16)).ToString).Value = sWorkNo
                            xlsWkS.Range("I" + (2 + 4 * (iCnt - 16)).ToString).Value = sRegNo
                            xlsWkS.Range("K" + (2 + 4 * (iCnt - 16)).ToString).Value = sPatNm
                            '<20130704 정선영 수정
                            xlsWkS.Range("I" + (3 + 4 * (iCnt - 16)).ToString).Value = sSpcNm
                            'xlsWkS.Range("I" + (4 + 4 * (iCnt - 16)).ToString).Value = sSpcNm
                            '>

                        End If

                        If iCnt Mod 30 = 0 Then  '다 채워졌을 때 출력하고 초기화.

                            xlsWkS.PrintOut()

                            For iCnt = 1 To 15
                                xlsWkS.Range("B" + (1 + 4 * (iCnt - 1)).ToString).Value = ""
                                xlsWkS.Range("B" + (2 + 4 * (iCnt - 1)).ToString).Value = ""
                                xlsWkS.Range("D" + (2 + 4 * (iCnt - 1)).ToString).Value = ""
                                xlsWkS.Range("B" + (3 + 4 * (iCnt - 1)).ToString).Value = ""
                                xlsWkS.Range("B" + (4 + 4 * (iCnt - 1)).ToString).Value = ""

                                xlsWkS.Range("I" + (1 + 4 * (iCnt - 1)).ToString).Value = ""
                                xlsWkS.Range("I" + (2 + 4 * (iCnt - 1)).ToString).Value = ""
                                xlsWkS.Range("K" + (2 + 4 * (iCnt - 1)).ToString).Value = ""
                                xlsWkS.Range("I" + (3 + 4 * (iCnt - 1)).ToString).Value = ""
                                xlsWkS.Range("I" + (4 + 4 * (iCnt - 1)).ToString).Value = ""
                            Next

                            iCnt = 0

                        End If

                    End If
                Next

                If iCnt Mod 30 > 0 Then xlsWkS.PrintOut()

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub
    '> 20140403 유민규 추가 (H4) 세포면역 wbc ,lym(%) 결과출력지로 표시 
    Private Sub sbPrint_ws_wgrp_lym(ByVal rsSheetGbn As String)

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing
        Dim dt As DataTable

        Dim msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(Windows.Forms.Application.StartupPath + "\SSF\" + rsSheetGbn + "_H4.xls")
            xlsWkS = CType(xlsWkB.ActiveSheet, Excel.Worksheet)

            Dim iCnt As Integer

            'xlsWkS.Range("M6").Value = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
            xlsWkS.Range("K1").Value = "출력시간: " + msTitle_Time

            iCnt = 0

            With Me.spdList
                If .ActiveCol = .GetColFromID("chk") Then
                    .SetActiveCell(.GetColFromID("chk") + 1, .ActiveRow)
                End If

                Dim iCntChk As Integer = .SearchCol(.GetColFromID("chk"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iCntChk < 1 Then
                    MsgBox("선택한 자료가 없습니다. 확인해 주십시오!!", MsgBoxStyle.Information)
                    Return
                End If

                For iRow As Integer = 1 To .MaxRows
                    If Ctrl.Get_Code(Me.spdList, "chk", iRow) = "1" Then


                        Dim sWorkNo As String = Ctrl.Get_Code(Me.spdList, "workno", iRow)
                        Dim sRegNo As String = Ctrl.Get_Code(Me.spdList, "regno", iRow)
                        Dim sPatNm As String = Ctrl.Get_Code(Me.spdList, "patnm", iRow)
                        Dim sBcNo As String = Ctrl.Get_Code(Me.spdList, "bcno", iRow)
                        Dim sSpcNm As String = Ctrl.Get_Code(Me.spdList, "spcnmd", iRow)
                        Dim sPrtBcNo As String = Ctrl.Get_Code(Me.spdList, "prtbcno", iRow)
                        Dim sSexage As String = Ctrl.Get_Code(Me.spdList, "sexage", iRow)
                        Dim sDoctornm As String = Ctrl.Get_Code(Me.spdList, "doctornm", iRow)
                        Dim sdeptinfo As String = Ctrl.Get_Code(Me.spdList, "deptinfo", iRow)


                        .Col = .GetColFromID("spcnmd") + 1
                        .Row = iRow
                        Dim slymrst As String = .Text

                        xlsWkS.Range("A" + (3 + (iRow - 1)).ToString).Value = iRow
                        xlsWkS.Range("B" + (3 + (iRow - 1)).ToString).Value = sBcNo
                        xlsWkS.Range("C" + (3 + (iRow - 1)).ToString).Value = sWorkNo
                        xlsWkS.Range("D" + (3 + (iRow - 1)).ToString).Value = sRegNo
                        xlsWkS.Range("E" + (3 + (iRow - 1)).ToString).Value = sPatNm
                        xlsWkS.Range("H" + (3 + (iRow - 1)).ToString).Value = sSexage
                        xlsWkS.Range("I" + (3 + (iRow - 1)).ToString).Value = sDoctornm
                        xlsWkS.Range("J" + (3 + (iRow - 1)).ToString).Value = sdeptinfo
                        xlsWkS.Range("K" + (3 + (iRow - 1)).ToString).Value = sSpcNm
                        xlsWkS.Range("L" + (3 + (iRow - 1)).ToString).Value = slymrst

                        dt = LISAPP.APP_S.WkFn.fnget_worklist_pastrst_lym(sBcNo) 'wbc , lym(%) 최근결과 조회

                        If dt.Rows.Count = 0 Then
                            xlsWkS.Range("K" + (3 + (iRow - 1)).ToString).Value = ""
                            xlsWkS.Range("L" + (3 + (iRow - 1)).ToString).Value = ""

                        Else
                            For i = 0 To dt.Rows.Count - 1
                                Select Case dt.Rows(i).Item("testcd").ToString
                                    Case "LH101"
                                        xlsWkS.Range("F" + (3 + (iRow - 1)).ToString).Value = dt.Rows(i).Item("viewrst").ToString
                                    Case "LH12103"
                                        xlsWkS.Range("g" + (3 + (iRow - 1)).ToString).Value = dt.Rows(i).Item("viewrst").ToString
                                End Select

                            Next

                        End If

                    End If
                Next

                'If iCnt Mod 30 > 0 Then xlsWkS.PrintOut()

                xlsWkS.PrintOut()

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try
    End Sub

    Private Sub sbDisplay_BarCdPrt()

        Try
            ' 기본 바코드프린터 설정
            Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
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
                If Ctrl.Get_Code(Me.cboSlip) = "H4" And Me.chkBar_cult.Checked Then '세포면역 배지바코드 출력
                    objBCPrt.PrintDo_Mic_Barcode(alBcNo, Me.txtPrtCnt.Text)
                ElseIf Me.chkBar_cult.Checked Then
                    'objBCPrt.PrintDo_Micro(alBcNo, Me.txtPrtCnt.Text)
                    objBCPrt.PrintDo_Micro(alBcNo, Me.txtPrtCnt.Text, Me.chkMultiBc.Checked)
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
        sbDisplay_Data()
    End Sub

    Private Sub FGS13_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.txtBcNo.Text = ""
        Me.txtBcNo.Focus()
    End Sub

    Private Sub FGS13_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS13_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            msTestFile = Application.StartupPath & msXMLDir & "\FGS13_M_TEST.XML"
            msWkGrpFile = Application.StartupPath & msXMLDir & "\FGS13_M_WKGRP.XML"
            msTgrpFile = Application.StartupPath & msXMLDir & "\FGS13_M_TGRP.XML"
            msSlipFile = Application.StartupPath & msXMLDir & "\FGS13_M_SLIP.XML"
            msSpcFile = Application.StartupPath & msXMLDir & "\FGS13_M_SPC.XML"
            msQryFile = Application.StartupPath & msXMLDir & "\FGS13_M_Qry.XML"
            msTermFile = Application.StartupPath & msXMLDir & "\FGS13_M_Term.XML"

            Me.Text = Me.Text + "(미생물)"
            Me.chkMbtType.Visible = True
            Me.chkMbtType.Checked = True
            Me.chkBar_cult.Checked = True
            Me.chkPrtWL.Checked = False

            Me.chkTyping.Visible = False 'jjh
            Me.chkTyping.Checked = False
        End If

    End Sub

    Private Sub FGS13_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        sbDisp_Init()

        Me.axItemSave.FORMID = IIf(mbMicroBioYn, "M", "R").ToString
        Me.axItemSave.USRID = USER_INFO.USRID
        Me.axItemSave.ITEMGBN = ""
        Me.axItemSave.SPCGBN = "NONE"
        Me.axItemSave.MicroBioYn = mbMicroBioYn
        Me.axItemSave.BloodBankYn = False
        Me.axItemSave.AllPartYn = True
        Me.axItemSave.sbDisplay_ItemList()

    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        If Me.chkPrtBar.Checked Or chkBar_cult.Checked Then sbPrint_BarCode()

        Try
            If Me.chkPrtWL.Checked Then

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


            ElseIf chkPrtWS.Checked And chkBFtest.Checked = False Then
                sbPrint_ws()
                '20210406 jhs 
            ElseIf chkPrtWS.Checked And chkBFtest.Checked Then
                sbPrint_ws_BFTest(False)
                '------------------------
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnSelBCPRT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelBCPRT.Click
        Dim frm As New POPUPPRT.FGPOUP_PRTBC(Me.Name, Me.chkBarInit.Checked)

        frm.ShowDialog()
        frm.Dispose()
        frm = Nothing

        sbDisplay_BarCdPrt()
    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sBuf As String = ""

        Try
            '20210714 jhs 체액검사 엑셀 출력 추가
            If Me.chkBFtestToExcel.Checked = False Then '체액검사 toexcel 로 체크 되어있을때 
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
            Else
                sbPrint_ws_BFTest(Me.chkBFtestToExcel.Checked)
            End If
            '-----------------------
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

    Private Sub spdList_BlockSelected(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BlockSelectedEvent) Handles spdList.BlockSelected
        Me.spdList.ClearSelection()
    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_test.Click
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
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_list(sPartSlip, sTGrpCd, sWGrpCd)
            Dim a_dr As DataRow() = dt.Select("(tcdgbn in ('P', 'B') OR titleyn = '0')", "sort1, sort2, testcd")

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

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnHelp_test.Height + 80, dt)

            If alList.Count > 0 Then

                Dim sTestCds As String = "", sTestNmds As String = ""

                For ix As Integer = 0 To alList.Count - 1
                    Dim sTestCd As String = alList.Item(ix).ToString.Split("|"c)(2)
                    Dim sTnmd As String = alList.Item(ix).ToString.Split("|"c)(1)

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
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub cboQrygbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQrygbn.SelectedIndexChanged
        sbDisplay_Date_Setting()
        COMMON.CommXML.setOneElementXML(msXMLDir, msQryFile, "JOB", cboQrygbn.SelectedIndex.ToString)
    End Sub

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        COMMON.CommXML.setOneElementXML(msXMLDir, msTgrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged

        Try
            Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

            sbClear_Form()

            sbDisplay_Date_Setting()

            If Me.cboWkGrp.SelectedIndex >= 0 Then
                COMMON.CommXML.setOneElementXML(msXMLDir, msWkGrpFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub axItemSave_ListDblClick(ByVal rsItemCds As String, ByVal rsItemNms As String)
        If rsItemCds <> "" Then
            Me.txtSelTest.Tag = rsItemCds + "^" + rsItemNms
            Me.txtSelTest.Text = rsItemNms.Replace("|", ",")

            cboQrygbn.SelectedIndex = 0

            sbDisplay_Test()
        End If
    End Sub

    Private Sub btnClear_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_test.Click, btnClear_spc.Click

        If CType(sender, Windows.Forms.Button).Name = "btnClear_test" Then
            Me.txtSelTest.Text = ""
            Me.txtSelTest.Tag = ""
        Else
            Me.txtSelSpc.Text = ""
            Me.txtSelSpc.Tag = ""
        End If
    End Sub

    Private Sub chkMbtType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMbtType.CheckedChanged
        If Me.chkMbtType.Checked Then
            Me.chkBar_cult.Enabled = True
        Else
            Me.chkBar_cult.Enabled = False : Me.chkBar_cult.Checked = False
        End If

    End Sub

    Private Sub chkPrttWL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrtWL.CheckedChanged, chkPrtWS.CheckedChanged
        If CType(sender, Windows.Forms.CheckBox).Checked Then
            If CType(sender, Windows.Forms.CheckBox).Name.ToLower = "chkprtwl" Then
                Me.chkPrtWS.Checked = False
                Me.chkBar_view.Enabled = True
            Else
                Me.chkPrtWL.Checked = False
                Me.chkBar_view.Enabled = False : Me.chkBar_view.Checked = False

            End If
        End If
    End Sub

    Private Sub chkPrtBar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPrtBar.CheckedChanged, chkBar_cult.CheckedChanged
        If CType(sender, Windows.Forms.CheckBox).Checked Then
            If CType(sender, Windows.Forms.CheckBox).Name.ToLower = "chkprtbar" Then
                Me.chkBar_cult.Checked = False
                Me.chkMultiBc.Checked = False
            Else
                Me.chkPrtBar.Checked = False
            End If
        End If

        If Me.chkPrtBar.Checked Then
            Me.chkMultiBc.Checked = False
        ElseIf Me.chkBar_cult.Checked = False Then
            Me.chkMultiBc.Checked = False
        End If
    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbClear_Form()

        sbDisplay_WkGrp()
        sbDisplay_Date_Setting()

        '<< 세포면역 배지바코드 활성화
        If Ctrl.Get_Code(Me.cboSlip) = "H4" Or mbMicroBioYn Then
            Me.chkBar_cult.Enabled = True
        Else
            Me.chkBar_cult.Enabled = False
        End If

        '20210415 jhs 체액검사일시 출력 될 수 있도록 구현 
        If Ctrl.Get_Code(Me.cboSlip) = "H5" Then
            Me.chkBFtest.Visible = True
            Me.chkBFtest.Checked = True
            Me.chkBFtestToExcel.Visible = True
            Me.chkBFtestToExcel.Checked = True
        Else
            Me.chkBFtest.Visible = False
            Me.chkBFtest.Checked = False
            Me.chkBFtestToExcel.Visible = False
            Me.chkBFtestToExcel.Checked = False
        End If
        '-------------------------------------------------

        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", cboSlip.SelectedIndex.ToString)
    End Sub

    Private Sub btnHelp_spc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_spc.Click
        Try

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim sPartCd As String = ""
            Dim sSlipCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sWGrpCd As String = ""

            If Me.cboQrygbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(Me.cboTGrp)
            Else
                If Ctrl.Get_Code(Me.cboSlip) <> "" Then
                    sPartCd = Ctrl.Get_Code(cboSlip).Substring(0, 1)
                    sSlipCd = Ctrl.Get_Code(cboSlip).Substring(1, 1)
                End If
                If Me.cboQrygbn.Text = "검사그룹" Then sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
            End If

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List("", sPartCd, sSlipCd, sTGrpCd, sWGrpCd, "", "")

            objHelp.FormText = "검체코드"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("spcnmd", "검체명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnHelp_test.Height + 80, dt)

            If alList.Count > 0 Then

                Dim sSpcCds As String = "", sSpcNmds As String = ""

                For ix As Integer = 0 To alList.Count - 1
                    Dim sSpcCd As String = alList.Item(ix).ToString.Split("|"c)(1)
                    Dim sSpcNmd As String = alList.Item(ix).ToString.Split("|"c)(0)

                    If ix > 0 Then
                        sSpcCds += "|" : sSpcNmds += "|"
                    End If

                    sSpcCds += sSpcCd : sSpcNmds += sSpcNmd
                Next

                Me.txtSelSpc.Text = sSpcNmds.Replace("|", ",")
                Me.txtSelSpc.Tag = sSpcCds + "^" + sSpcNmds
            Else
                Me.txtSelSpc.Text = ""
                Me.txtSelSpc.Tag = ""
            End If


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub axItemSave_ListDblClick1(ByVal rsItemCds As String, ByVal rsItemNms As String) Handles axItemSave.ListDblClick
        If rsItemCds <> "" Then
            Me.txtSelTest.Tag = rsItemCds + "^" + rsItemNms
            Me.txtSelTest.Text = rsItemNms.Replace("|", ",")

            sbDisplay_Test()
        End If
    End Sub

  
    Private Sub chkMultiBc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMultiBc.CheckedChanged
        If Me.chkBar_cult.Checked = False Then
            Me.chkMultiBc.Checked = False
        Else

        End If
    End Sub

    Private Sub chkBFtest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBFtest.CheckedChanged
        If chkBFtest.Checked = True Then
            chkPrtWS.Checked = True
            chkPrtWL.Checked = False
        ElseIf chkBFtest.Checked = False Then
            chkPrtWS.Checked = False
            chkPrtWL.Checked = True
        End If
    End Sub
End Class

Public Class FGS13_PATINFO
    Public sPrtBcNo As String = ""

    Public alItem As New ArrayList

    Public sTNms As String = ""
    Public sTCds As String = ""
    Public sRsts As String = ""
End Class

Public Class FGS13_PRINT
    Private Const msFile As String = "File : FGS13.vb, Class : S01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0
    Private miTitle_ExmCnt As Integer = 0
    Private miCCol As Integer = 0
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
        miCCol = 0
    End Sub

Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 9, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
        Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        ' msgLeft = 5
        msgLeft = 10
        'msgTop = 40
        msgTop = 60

        Dim sngTmp As Single = 0
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGS13_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGS13_PATINFO).alItem(ix).ToString.Split("^"c)(2))
        Next

        If sngTmp + 40 > msgWidth Then Return

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 2)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            For ix As Integer = 1 To CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem.Count
                rect = New Drawing.RectangleF(msgPosX(ix), sngPosY, msgPosX(ix + 1) - msgPosX(ix), sngPrtH)
                Dim strTmp As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(ix - 1).ToString.Split("^"c)(0)

                e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).sTNms.Split("|"c)
            Dim strRst() As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).sRsts.Split("|"c)

            Dim intCol As Integer = -1
            Dim bLineSkip As Boolean = True
            For intIx1 As Integer = miCCol To strTnm.Length - 2
                intCol += 1
                If intCol > miTitle_ExmCnt Then
                    intCol = 0
                    sngPosY += sngPrtH
                    e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(miTitleCnt), sngPosY, msgWidth, sngPosY)
                End If

                If msgHeight < sngPosY + sngPrtH * 3 Then
                    bLineSkip = False
                    miCCol = intIx1 + 1
                    Exit For
                End If

                miCCol = 0
                '-- 검사명
                rect = New Drawing.RectangleF(msgPosX(miTitleCnt + intCol), sngPosY + sngPrtH * 0, msgPosX(miTitleCnt + intCol + 1) - msgPosX(miTitleCnt + intCol), sngPrtH)
                e.Graphics.DrawString(strTnm(intIx1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            sngPosY += sngPrtH
            If msgHeight < sngPosY + sngPrtH * 4 Then If bLineSkip Then miCIdx += 1 : Exit For

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

        miTitleCnt = CType(maPrtData.Item(0), FGS13_PATINFO).alItem.Count + 1

        Dim sngPosX(0 To 1) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40

        For ix As Integer = 1 To miTitleCnt - 1
            ReDim Preserve sngPosX(ix + 1)

            sngPosX(ix + 1) = sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), FGS13_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))
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

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = Convert.ToSingle(fnt_Title.GetHeight(e.Graphics) * 1.5)

        '-- 출력정보
        If msTitle_sub_right_1.Length > msTitle_Time.Length + 6 Then
            msTitle_Time = msTitle_Time.PadRight(msTitle_sub_right_1.Length - 6)
        Else
            msTitle_sub_right_1 = msTitle_sub_right_1.PadRight(msTitle_Time.Length + 6)
        End If

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * msTitle_sub_right_1.Length, sngPosY - 20, msgWidth - msgLeft - 25, sngPrt), sf_l)
        End If

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6), sngPosY, msgWidth - 8 * (msTitle_Time.Length + 6), sngPrt), sf_l)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt), sf_l)

        For ix As Integer = 1 To CType(maPrtData.Item(0), FGS13_PATINFO).alItem.Count

            Dim strTmp As String = CType(maPrtData.Item(0), FGS13_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

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
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGS13_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGS13_PATINFO).alItem(ix).ToString.Split("^"c)(2))
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
                    sngPosY = fnPrtTitle_Fixed(e, CType(maPrtData.Item(intIdx), FGS13_PATINFO).sTNms.Split("|"c), miCCol)
                End If

                '-- 번호
                rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * intLine, msgPosX(1) - msgPosX(0), sngPrtH)
                e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

                For ix As Integer = 1 To CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem.Count
                    rect = New Drawing.RectangleF(msgPosX(ix), sngPosY + sngPrtH * intLine, msgPosX(ix + 1) - msgPosX(ix), sngPrtH)
                    Dim strTmp As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(ix - 1).ToString.Split("^"c)(0)

                    e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                Next

                Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).sTNms.Split("|"c)
                Dim strRst() As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).sRsts.Split("|"c)

                intCnt = 0 : Dim intTitleCnt As Integer = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem.Count + 1

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
                    e.Graphics.DrawString(strRst(intIx1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
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

        miTitleCnt = CType(maPrtData.Item(0), FGS13_PATINFO).alItem.Count + 1

        Dim sngPosX(0 To 1) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40

        For ix As Integer = 1 To miTitleCnt - 1
            ReDim Preserve sngPosX(ix + 1)

            sngPosX(ix + 1) = sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), FGS13_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))
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

        For ix As Integer = 1 To CType(maPrtData.Item(0), FGS13_PATINFO).alItem.Count

            Dim strTmp As String = CType(maPrtData.Item(0), FGS13_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(ix), sngPosY, sngPosX(ix + 1) - sngPosX(ix), sngPrt * 2), sf_l)
        Next

        intCnt = 0

        For intIdx As Integer = riColS To riColS + miTitle_ExmCnt
            If intIdx > miTotExmCnt Then Exit For

            If intIdx > rsExmNm.Length Then
                Exit For
            End If
            e.Graphics.DrawString(rsExmNm(intIdx), fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(miTitleCnt + intCnt), sngPosY, sngPosX(miTitleCnt + 1 + intCnt) - sngPosX(miTitleCnt + intCnt), sngPrt * 2), sf_l)
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

            Dim sBcNo As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(0).ToString.Split("^"c)(0)
            Dim sWorkNo As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(1).ToString.Split("^"c)(0)
            Dim sRegNo As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(2).ToString.Split("^"c)(0)
            Dim sPatNm As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(3).ToString.Split("^"c)(0)
            Dim sSexAge As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(4).ToString.Split("^"c)(0)
            Dim sDeptWard As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(6).ToString.Split("^"c)(0)
            Dim sDoctorRmk As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(7).ToString.Split("^"c)(0)
            Dim sDiagNm As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(8).ToString.Split("^"c)(0)
            Dim sSpcNmd As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(9).ToString.Split("^"c)(0)
            Dim sPrtBcNo As String = CType(maPrtData.Item(ix), FGS13_PATINFO).alItem(10).ToString.Split("^"c)(0)

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

            Dim strTnm() As String = CType(maPrtData.Item(ix), FGS13_PATINFO).sTNms.Split("|"c)
            Dim strRst() As String = CType(maPrtData.Item(ix), FGS13_PATINFO).sRsts.Split("|"c)

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
                Dim sBcNo As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(0).ToString.Split("^"c)(0)
                Dim sWorkNo As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(1).ToString.Split("^"c)(0)
                Dim sRegNo As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(2).ToString.Split("^"c)(0)
                Dim sPatNm As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(3).ToString.Split("^"c)(0)
                Dim sSexAge As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(4).ToString.Split("^"c)(0)
                Dim sDeptWard As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(6).ToString.Split("^"c)(0)
                Dim sDoctorRmk As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(7).ToString.Split("^"c)(0)
                Dim sDiagNm As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(8).ToString.Split("^"c)(0)
                Dim sSpcNmd As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(9).ToString.Split("^"c)(0)
                Dim sPrtBcNo As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).alItem(10).ToString.Split("^"c)(0)

                If sgPosY = 0 Then
                    sgPosY = fnPrtTitle_Fixed_barno(e, CType(maPrtData.Item(intIdx), FGS13_PATINFO).sTNms.Split("|"c), miCCol)
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

                Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).sTNms.Split("|"c)
                Dim strRst() As String = CType(maPrtData.Item(intIdx), FGS13_PATINFO).sRsts.Split("|"c)

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

