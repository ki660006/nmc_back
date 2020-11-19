'>>> 접수대장

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.APP_S.CollTkFn

Public Class FGS03
    Inherits System.Windows.Forms.Form

    Private Const msXML As String = "\XML"
    Private msTestFile As String = Application.StartupPath + msXML + "\FGS13_TEST.XML"
    Private msWkGrpFile As String = Application.StartupPath + msXML + "\FGS13_WKGRP.XML"
    Private msTgrpFile As String = Application.StartupPath + msXML + "\FGS13_TGRP.XML"
    Private msPartFile As String = Application.StartupPath + msXML + "\FGS13_PART.XML" '<<<20161124 부서 추가 
    Private msSlipFile As String = Application.StartupPath + msXML + "\FGS13_SLIP.XML"
    Private msSpcFile As String = Application.StartupPath + msXML + "\FGS13_SPC.XML"
    Private msQryFile As String = Application.StartupPath + msXML + "\FGS13_Qry.XML"
    Private msTermFile As String = Application.StartupPath + msXML + "\FGS13_Term.XML"

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
            .TITLE = "채혈일시"
            .WIDTH = "120"
            .FIELD = "colldt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "채혈자"
            .WIDTH = "80"
            .FIELD = "collnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "전달일시"
            .WIDTH = "120"
            .FIELD = "passdt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "전달자"
            .WIDTH = "80"
            .FIELD = "passnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "접수일시"
            .WIDTH = "120"
            .FIELD = "tkdt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = ""
            .TITLE = "접수자"
            .WIDTH = "80"
            .FIELD = "tknm"
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
            .CHECK = "1"
            .TITLE = "처방일자"
            .WIDTH = "100"
            .FIELD = "regno"
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
            '2018-06-21 yjh 진료과 안보여 수정함 
            '.FIELD = "deptinfo"
            .FIELD = "dept"
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
            .TITLE = "검사항목"
            .WIDTH = "300"
            .FIELD = "testnms"
        End With
        alItems.Add(stu_item)


        Return alItems

    End Function

    ' 화면 정리
    Private Sub sbClear_Form()
        Me.spdList.MaxRows = 0
        Me.lblCnt_coll.Text = ""
        Me.lblCnt_pass.Text = ""
        Me.lblCnt_Tk.Text = ""
    End Sub

    Private Sub sbDisp_Init()

        Try

            If PRG_CONST.S01_PASS_VIEW = "" Then
                With spdList
                    .Col = .GetColFromID("passdt") : .ColHidden = True
                    .Col = .GetColFromID("passnm") : .ColHidden = True
                End With

                Me.cboAction.Items.Clear()
                Me.cboAction.Items.Add("[1] 미접수바코드")
                Me.cboAction.Items.Add("[2] 채혈")
                Me.cboAction.Items.Add("[3] 검체전달")
                Me.cboAction.Items.Add("[4] 접수")



                Me.lblPass.Visible = False : Me.lblCnt_pass.Visible = False
                Me.lblTk.Top = 47 : Me.lblCnt_Tk.Top = 47
                Me.lblColl.Top = Me.lblTk.Top - (Me.lblTk.Height + 2) : Me.lblCnt_coll.Top = Me.lblTk.Top - (Me.lblTk.Height + 2)
                Me.lblTk2.Top = Me.lblTk.Top + Me.lblTk.Height + 2 : Me.lblCnt_tk2.Top = Me.lblTk.Top + Me.lblTk.Height + 2

            End If

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

            Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
            Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")

            sbDisplay_Slip()    '-- 검사분야 
            sbDisplay_TGrp()    '-- 검사그룹

            Dim sTgrp As String = "", sWkGrp As String = "", sJob As String = "", sTerm As String = "", sTestCds As String = "", sSpc As String = "", sPart = ""

            sTgrp = COMMON.CommXML.getOneElementXML(msXML, msTgrpFile, "TGRP")
            sWkGrp = COMMON.CommXML.getOneElementXML(msXML, msWkGrpFile, "WKGRP")
            sJob = COMMON.CommXML.getOneElementXML(msXML, msQryFile, "JOB")
            sTerm = COMMON.CommXML.getOneElementXML(msXML, msTermFile, "TERM")
            sTestCds = COMMON.CommXML.getOneElementXML(msXML, msTestFile, "TEST")
            sSpc = COMMON.CommXML.getOneElementXML(msXML, msSpcFile, "SPC")
            sPart = COMMON.CommXML.getOneElementXML(msXML, msPartFile, "PART")

            If Me.cboPart.Items.Count > 0 Then
                If sPart = "" Or Val(sPart) > Me.cboPart.Items.Count Then
                    Me.cboPart.SelectedIndex = 0
                Else
                    Me.cboPart.SelectedIndex = Convert.ToInt16(sPart)
                End If
            End If

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

            If Me.cboSpcCd.Items.Count > 0 Then
                If sSpc = "" Or Val(sSpc) > Me.cboSpcCd.Items.Count Then
                    Me.cboSpcCd.SelectedIndex = 0
                Else
                    Me.cboSpcCd.SelectedIndex = Convert.ToInt16(sSpc)
                End If
            End If
            If sJob = "" Or Val(sJob) > Me.cboQryGbn.Items.Count Then
                Me.cboQryGbn.SelectedIndex = 0
            Else
                Me.cboQryGbn.SelectedIndex = Convert.ToInt16(sJob)
            End If

            If sTestCds <> "" Then
                Me.txtSelTest.Text = sTestCds.Split("^"c)(1).Replace("|", ",")
                Me.txtSelTest.Tag = sTestCds
            End If

            sbDisplay_Spc()
            sbDisplay_dept()
            sbDisplay_Ward()

            Me.dtpDateS.Focus()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub


    Private Sub sbDisplay_Date_Setting()

        If Me.cboQryGbn.Text = "검사그룹" Then
            sbDisplay_TGrp()
            Me.lblTitleDt.Text = "접수일자"

            Me.dtpDateE.Visible = True
            Me.lblDate.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

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

    Private Sub sbDisplay_part()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Part_List()

            Me.cboSlip.Items.Clear()
            Me.cboSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msPartFile, "PART")

            If sTmp = "" Or Val(sTmp) > cboSlip.Items.Count Then
                Me.cboSlip.SelectedIndex = 0
            Else
                Me.cboSlip.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Slip()

        Try

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List(, True)

            Me.cboSlip.Items.Clear()
            Me.cboSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msSlipFile, "SLIP")

            If sTmp = "" Or Val(sTmp) > cboSlip.Items.Count Then
                Me.cboSlip.SelectedIndex = 0
            Else
                Me.cboSlip.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_TGrp()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_List()

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

    Private Sub sbDisplay_WGrp()

        Try
            Me.cboWkGrp.Items.Clear()
            If Ctrl.Get_Code(Me.cboSlip) = "" Then Return

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_WKGrp_List(Ctrl.Get_Code(Me.cboSlip))

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

            If Me.cboQryGbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(Me.cboTGrp)
                If sTGrpCd = "" And Ctrl.Get_Code(cboSlip) <> "" Then
                    If Ctrl.Get_Code(cboSlip).Length = 1 Then
                        sPartCd = Ctrl.Get_Code(Me.cboSlip) '<<<20161124 부서조회 시 추가 
                    Else
                        sPartCd = Ctrl.Get_Code(Me.cboSlip).Substring(0, 1)
                        sSlipCd = Ctrl.Get_Code(Me.cboSlip).Substring(1, 1)
                    End If
                    'sPartCd = Ctrl.Get_Code(Me.cboSlip).Substring(0, 1)
                    'sSlipCd = Ctrl.Get_Code(Me.cboSlip).Substring(1, 1)
                End If
            Else
                If Ctrl.Get_Code(Me.cboSlip) <> "" Then
                    If Ctrl.Get_Code(cboSlip).Length = 1 Then
                        sPartCd = Ctrl.Get_Code(Me.cboSlip) '<<<20161124 부서조회 시 추가 
                    Else
                        sPartCd = Ctrl.Get_Code(Me.cboSlip).Substring(0, 1)
                        sSlipCd = Ctrl.Get_Code(Me.cboSlip).Substring(1, 1)
                    End If

                End If
                If Me.cboQryGbn.Text = "검사그룹" Then sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
            End If

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List("", sPartCd, sSlipCd, sTGrpCd, sWGrpCd, "", "")

            Me.cboSpcCd.Items.Clear()
            Me.cboSpcCd.Items.Add("[   ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSpcCd.Items.Add("[" + dt.Rows(ix).Item("spccd").ToString.Trim + "] " + dt.Rows(ix).Item("spcnmd").ToString.Trim)
            Next


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Ward()


        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList()

            Me.cboWard.Items.Clear()
            Me.cboWard.Items.Add("" + Space(200) + "|")
            For intIdx As Integer = 0 To dt.Rows.Count - 1
                Me.cboWard.Items.Add(dt.Rows(intIdx).Item("wardnm").ToString.Trim + Space(200) + "|" + dt.Rows(intIdx).Item("wardno").ToString.Trim)
            Next

            If Me.cboWard.Items.Count > 0 Then cboWard.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_dept()

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList()

            Me.cboDeptCd.Items.Clear()
            Me.cboDeptCd.Items.Add("전체" + Space(200) + "|")
            For intIdx As Integer = 0 To dt.Rows.Count - 1
                Me.cboDeptCd.Items.Add(dt.Rows(intIdx).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(intIdx).Item("deptcd").ToString)
            Next

            If Me.cboDeptCd.Items.Count > 0 Then Me.cboDeptCd.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Data()

        Try

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim sSlipCd As String = Ctrl.Get_Code(cboSlip)
            Dim sTGrpCd As String = "", sWGrpCd As String = "", sRstFlg As String = ""
            Dim sTestCds As String = ""
            Dim sIoGbn As String = ""

            If Me.rdoIogbnI.Checked Then sIoGbn = "I"
            If Me.rdoIogbnO.Checked Then sIoGbn = "O"

            If Me.chkRstNull.Checked Then sRstFlg = "0"
            If Me.chkRstReg.Checked Then sRstFlg += IIf(sRstFlg = "", "", ",").ToString + "1"
            If Me.chkRstMw.Checked Then sRstFlg += IIf(sRstFlg = "", "", ",").ToString + "2"
            If Me.chkRstFn.Checked Then sRstFlg += IIf(sRstFlg = "", "", ",").ToString + "3"

            If Me.txtSelTest.Text <> "" Then
                'sTestCds = Me.txtSelTest.Tag.ToString.Replace("|", ",")
                sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")

            End If

            If Me.cboQryGbn.Text = "작업그룹" Then
                sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
            Else
                sTGrpCd = Ctrl.Get_Code(Me.cboTGrp)
                If sTGrpCd <> "" Then sSlipCd = ""
            End If

            Dim sDeptCd As String = ""
            Dim sWardNo As String = ""

            If Me.cboDeptCd.Text.IndexOf("|") >= 0 Then sDeptCd = Me.cboDeptCd.Text.Split("|"c)(1)
            If Me.cboWard.Text.IndexOf("|") >= 0 Then sWardNo = Me.cboWard.Text.Split("|"c)(1)

            'ori
            'Dim dt As DataTable = fnGet_CollTk_List(Ctrl.Get_Code(Me.cboAction), Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
            '                                        sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, Me.txtRegNo.Text, Me.txtPatnm.Text, _
            '                                        sIoGbn, sWardNo, sDeptCd, Me.chkNoTk2.Checked)

            Dim dt As DataTable = fnGet_CollTk_List2(Ctrl.Get_Code(Me.cboAction), Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                                    sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, Me.txtRegNo.Text, Me.txtPatnm.Text, _
                                                    sIoGbn, sWardNo, sDeptCd, Me.chkNoTk2.Checked)

            sbDisplay_Data_View(dt)

            Select Case Ctrl.Get_Code(cboAction)
                Case "1"

                    Me.lblCnt_coll.Text = Me.spdList.MaxRows.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("3", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_pass.Text = dt.Rows.Count.ToString + " 건"

                    '20161020 허용석 미접수 바코드만 조회 조건 추가
                    dt = fnGet_CollTk_Statistics("4", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_Tk.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("5", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_tk2.Text = dt.Rows.Count.ToString + " 건"
                Case "2"

                    Me.lblCnt_coll.Text = Me.spdList.MaxRows.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("3", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_pass.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("4", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_Tk.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("5", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_tk2.Text = dt.Rows.Count.ToString + " 건"

                Case "3"
                    Me.lblCnt_pass.Text = Me.spdList.MaxRows.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("2", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_coll.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("4", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                              sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                              Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_Tk.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("5", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                               sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                               Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_tk2.Text = dt.Rows.Count.ToString + " 건"


                Case "4"

                    If Me.chkNoTk2.Checked Then
                        dt = fnGet_CollTk_Statistics("4", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                                   sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                                   Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                        Me.lblCnt_Tk.Text = dt.Rows.Count.ToString + " 건"
                    Else
                        Me.lblCnt_Tk.Text = Me.spdList.MaxRows.ToString + " 건"
                    End If

                    dt = fnGet_CollTk_Statistics("2", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_coll.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("3", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                        sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_pass.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("5", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                              sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                              Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_tk2.Text = dt.Rows.Count.ToString + " 건"

                Case "5"

                    Me.lblCnt_tk2.Text = Me.spdList.MaxRows.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("2", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_coll.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("3", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                       sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                       Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_pass.Text = dt.Rows.Count.ToString + " 건"

                    dt = fnGet_CollTk_Statistics("4", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), _
                                              sSlipCd, sWGrpCd, sTGrpCd, Ctrl.Get_Code(cboSpcCd), sTestCds, sRstFlg, _
                                              Me.txtRegNo.Text, Me.txtPatnm.Text, sIoGbn, sWardNo, sDeptCd)
                    Me.lblCnt_Tk.Text = dt.Rows.Count.ToString + " 건"

            End Select


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
                            .Col = iCol

                            '<< JJH 바코드폰트로 표시
                            If r_dt.Columns(ix2).ColumnName.ToLower = "prtimg" Then
                                Dim fnt_BarCd As New Font("Code39(2:3)", 9)
                                .Font = fnt_BarCd
                            End If

                            .Text = r_dt.Rows(ix).Item(ix2).ToString.Trim
                        End If
                    Next

                    .Col = .GetColFromID("chk") : .Text = "1"
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

                        Dim objPat As New FGS00_PATINFO

                        With objPat
                            .alItem = arlItem
                        End With

                        arlPrint.Add(objPat)
                    End If
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGS00_PRINT
                prt.mbLandscape = True  '-- false : 세로, true : 가로
                prt.msTitle = "채혈 및 접수대장"
                prt.msJobGbn = Me.cboAction.Text
                prt.maPrtData = arlPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                If Me.chkPreview.Checked Then
                    prt.sbPrint_Preview()
                Else
                    prt.sbPrint()
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

    Private Sub FGS03_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS03_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FGS03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo

        sbDisp_Init()
        Me.cboAction.SelectedIndex = Me.cboAction.Items.Count - 1

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


    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try
            With spdList
                .ReDraw = False

                .MaxRows += 1
                .Row = 1
                .Action = FPSpreadADO.ActionConstants.ActionInsertRow

                For intCol As Integer = 1 To .MaxCols
                    .Row = 0
                    .Col = intCol : Dim strTmp As String = .Text
                    .Row = 1 : .Col = intCol : .Text = strTmp
                Next


                For intRow As Integer = 2 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text <> "1" Then .RowHidden = True
                Next

                .Col = .GetColFromID("chk") : .ColHidden = True

                '<< JJH
                If chkBarcode.Checked Then

                    .Col = .GetColFromID("prtimg") : .ColHidden = False
                    .set_ColWidth(2, 36)
                    For ix As Integer = 1 To .MaxRows
                        .set_RowHeight(ix, 44)

                        .Row = ix
                        .Col = .GetColFromID("prtimg") : .FontSize = 36
                    Next

                End If
                '>>

                If .ExportToExcel("WorkList_" + Now.ToShortDateString() + ".xls", "Worklist", "") Then
                    Process.Start("WorkList_" + Now.ToShortDateString() + ".xls")
                End If


                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text <> "1" Then .RowHidden = True
                Next

                .Col = .GetColFromID("chk") : .ColHidden = False

                '<< JJH
                If chkBarcode.Checked Then

                    .Col = .GetColFromID("prtimg") : .ColHidden = True
                    For ix As Integer = 1 To .MaxRows
                        .set_RowHeight(ix, 10.9)

                        .Row = ix
                        .Col = .GetColFromID("prtimg") : .FontSize = 9
                    Next

                End If
                '>>



                .Row = 1
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1

                .ReDraw = True

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub chkSelChk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelChk.Click

        With Me.spdList
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                    .Text = IIf(chkSelChk.Checked, "1", "").ToString
                End If
            Next
        End With

    End Sub

    Private Sub chkTclsFix_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        sbDisplay_Data()

    End Sub

    Private Sub chkColMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkColMove.Click
        If Me.chkColMove.Checked Then
            Me.spdList.AllowColMove = True
        Else
            Me.spdList.AllowColMove = False
        End If
    End Sub

    Private Sub cboTerm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        COMMON.CommXML.setOneElementXML(msXML, msTermFile, "TERM", Me.cboTGrp.SelectedIndex.ToString)
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

    Private Sub cboAction_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAction.SelectedIndexChanged
        Select Case Ctrl.Get_Code(cboAction)
            Case "1"    '-- 미접수바코드
                Me.lblTitleDt.Text = "채혈일자"
                Me.cboQryGbn.SelectedIndex = 0
                Me.cboQryGbn.Enabled = False
                Me.chkNoTk2.Enabled = False
            Case "2"    '-- 채혈
                Me.lblTitleDt.Text = "채혈일자"
                Me.cboQryGbn.SelectedIndex = 0
                Me.cboQryGbn.Enabled = False
                Me.chkNoTk2.Enabled = False

            Case "3"    '-- 검체전달
                Me.lblTitleDt.Text = "전달일시"
                Me.cboQryGbn.SelectedIndex = 0
                Me.cboQryGbn.Enabled = False
                Me.chkNoTk2.Enabled = False
            Case "4"    '-- 접수
                Me.lblTitleDt.Text = "접수일자"
                Me.cboQryGbn.Enabled = True
                Me.chkNoTk2.Enabled = True
            Case "5"
                Me.lblTitleDt.Text = "접수일자"
                Me.cboQryGbn.Enabled = True
                Me.chkNoTk2.Enabled = False

        End Select
    End Sub

    Private Sub cboQryGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQryGbn.SelectedIndexChanged

        If Me.cboQryGbn.Text = "검사그룹" Then
            Me.cboTGrp.Visible = True : Me.cboWkGrp.Visible = False
        Else
            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = True
        End If

        sbDisplay_Date_Setting()
        COMMON.CommXML.setOneElementXML(msXML, msQryFile, "JOB", cboQryGbn.SelectedIndex.ToString)
    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbClear_Form()

        sbDisplay_WGrp()
        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        COMMON.CommXML.setOneElementXML(msXML, msSlipFile, "SLIP", cboSlip.SelectedIndex.ToString)
    End Sub

    Private Sub cboSpcCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSpcCd.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXML, msSpcFile, "SPC", cboSpcCd.SelectedIndex.ToString)
    End Sub

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbDisplay_Spc()
        COMMON.CommXML.setOneElementXML(msXML, msTgrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbClear_Form()

        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        If cboWkGrp.SelectedIndex >= 0 Then
            COMMON.CommXML.setOneElementXML(msXML, msWkGrpFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)
        End If

    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Try

            Dim sWGrpCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sPartSlip As String = ""

            If Me.cboQryGbn.Text = "검사그룹" Then
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
            Dim a_dr As DataRow() = dt.Select("tcdgbn IN ('B', 'C', 'P','S')", "") '20160128 전재휘 SINGLE 코드 추가.

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

            COMMON.CommXML.setOneElementXML(msXML, msTestFile, "TEST", Me.txtSelTest.Tag.ToString)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnClear_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_test.Click
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""
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


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub rdoIogbnI_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoIogbnI.CheckedChanged, rdoIogbnO.CheckedChanged
        If CType(sender, Windows.Forms.RadioButton).Checked Then
            Select Case CType(sender, Windows.Forms.RadioButton).Text
                Case "입원"
                    Me.lblDeptWard.Text = "병  동"
                    Me.cboDeptCd.Visible = False
                    Me.cboWard.Visible = True

                    sbDisplay_Ward()
                Case Else
                    Me.lblDeptWard.Text = "진료과"
                    Me.cboDeptCd.Visible = True
                    Me.cboWard.Visible = False

                    sbDisplay_dept()
            End Select
        End If

    End Sub

    Private Sub lblslipnm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblslipnm.Click

        If Me.lblslipnm.Text = "검사분야" Then
            Me.lblslipnm.Text = "검사부서"
            sbDisplay_part()
        ElseIf Me.lblslipnm.Text = "검사부서" Then
            Me.lblslipnm.Text = "검사분야"
            sbDisplay_Slip()
        End If

    End Sub

    Private Sub cboPart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPart.SelectedIndexChanged
        If Me.cboPart.SelectedIndex = 0 Then
            sbDisplay_part()
        Else
            sbDisplay_Slip()
        End If

        COMMON.CommXML.setOneElementXML(msXML, msPartFile, "PART", cboPart.SelectedIndex.ToString)

        If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0
    End Sub
End Class

Public Class FGS03_PATINFO
    Public aItem As New ArrayList
    Public sRegNo As String = ""
    Public sPatNm As String = ""
    Public sSexAge As String = ""
    Public sDeptWard As String = ""
    Public sDoctorNm As String = ""
    Public sDocRmk As String = ""
    Public sSpcNmd As String = ""
    Public sBcNo As String = ""
    Public sOrdDt As String = ""

    Public sCollDt As String = ""
    Public sCollNm As String = ""
    Public sTkDt As String = ""
    Public sTkNm As String = ""

    Public sTNms As String = ""
    Public sTCds As String = ""
    Public sRsts As String = ""
End Class


Public Class FGS03_PRINT
    Private Const msFile As String = "File : FGS03.vb, Class : S01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0
    Private miTitle_ExmCnt As Integer = 0
    Private miCCol As Integer = 1

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msgExmWidth As Single
    Public msTitle As String
    Public maPrtData As ArrayList
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd hh:mm")
    Public miTotExmCnt As Integer = 0

    Public Sub sbPrint_Preview(ByVal rbFixed As Boolean)
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtRView = New PrintPreviewDialog

                prtR.DocumentName = "ACK_" + msTitle

                If rbFixed Then
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage_Fixed
                Else
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage
                End If
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
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

                If rbFixed Then
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage_Fixed
                Else
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage
                End If
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport
                prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
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

        'Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
        'Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

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

        sngPrtH = fnt_Body.GetHeight(e.Graphics)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * 0, msgPosX(1) - msgPosX(0), sngPrtH * 3)
            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 처방일시
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 0, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sOrdDt, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            '-- 등록번호
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * 0, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 성명
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * 0, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 진료과/병동
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * 0, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sDeptWard, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 채혈일시
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 1, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sCollDt, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            '-- 채혈자
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * 1, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sCollNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 성별/나이
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * 1, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sSexAge, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 검체명
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * 1, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sSpcNmd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 접수일시
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 2, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sTkDt, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            '-- 접수자
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * 2, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sTkNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 의사 Remark
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * 2, msgPosX(5) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sDocRmk, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGS03_PATINFO).sTNms.Split("|"c)

            Dim intCol As Integer = 0
            For intIx1 As Integer = 0 To strTnm.Length - 2
                intCol += 1
                If intCol > miTitle_ExmCnt Then
                    intCol = 1
                    sngPosY += sngPrtH * 3 + sngPrtH / 2

                    e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(5), sngPosY - sngPrtH / 2, msgWidth, sngPosY - sngPrtH / 2)
                End If

                If msgHeight - sngPrtH * 6 < sngPosY Then Exit For

                '-- 검사명
                rect = New Drawing.RectangleF(msgPosX(4 + intCol), sngPosY + sngPrtH * 0, msgPosX(5 + intCol) - msgPosX(4 + intCol), sngPrtH)
                e.Graphics.DrawString(strTnm(intIx1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            Next

            sngPosY += sngPrtH * 3 + sngPrtH / 2
            If msgHeight - sngPrtH * 6 < sngPosY Then Exit For

            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrtH / 2, msgWidth, sngPosY - sngPrtH / 2)

            miCIdx += 1
        Next

        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

        If miCIdx < maPrtData.Count - 1 Then
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

        sngTmp = msgWidth - msgLeft - 540
        intCnt = Convert.ToInt16(sngTmp / msgExmWidth)
        If intCnt * msgExmWidth > sngTmp Then intCnt -= 1
        miTitle_ExmCnt = intCnt

        Dim sngPosX(0 To intCnt + 6) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40
        sngPosX(2) = sngPosX(1) + 160
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

        fnPrtTitle = sngPosY + sngPrt * 3 + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY + sngPrt * 0, sngPosX(1) - sngPosX(0), sngPrt), sf_c)

        e.Graphics.DrawString("처방일시", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY + sngPrt * 0, sngPosX(2) - sngPosX(1), sngPrt), sf_c)
        e.Graphics.DrawString("등록번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 0, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("성명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 0, sngPosX(4) - sngPosX(3), sngPrt), sf_l)
        e.Graphics.DrawString("진료과/병동", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY + sngPrt * 0, sngPosX(5) - sngPosX(4), sngPrt), sf_l)

        e.Graphics.DrawString("채혈일시", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY + sngPrt * 1, sngPosX(2) - sngPosX(1), sngPrt), sf_c)
        e.Graphics.DrawString("채혈자", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 1, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("성별/나이", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 1, sngPosX(4) - sngPosX(3), sngPrt), sf_l)
        e.Graphics.DrawString("검체", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY + sngPrt * 1, sngPosX(5) - sngPosX(4), sngPrt), sf_l)

        e.Graphics.DrawString("접수일시", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY + sngPrt * 2, sngPosX(2) - sngPosX(1), sngPrt), sf_c)
        e.Graphics.DrawString("접수자", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 2, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("의사 Remark", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 2, sngPosX(5) - sngPosX(3), sngPrt), sf_l)

        e.Graphics.DrawString("검사항목", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5), sngPosY + sngPrt * 0, msgWidth - sngPosX(5), sngPrt), sf_c)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt * 3, msgWidth, sngPosY + sngPrt * 3)

        msgPosX = sngPosX

    End Function

    Public Overridable Sub sbPrintPage_Fixed(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim sFn As String = "Public Overridable Sub sbPrintPage_Fixed(Object, PrintPageEventArgs)"
        Try
            Dim intPage As Integer = 0
            Dim sngPosY As Single = 0
            Dim sngPrtH As Single = 0

            Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
            Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
            Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

            'Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
            'Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

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

            sngPrtH = fnt_Body.GetHeight(e.Graphics)

            Dim rect As New Drawing.RectangleF

            Dim sngTmp As Single = 0
            Dim intCnt As Integer = 0

            sngTmp = msgWidth - msgLeft - 540
            intCnt = Convert.ToInt16(sngTmp / msgExmWidth)
            If intCnt * msgExmWidth > sngTmp Then intCnt -= 1
            miTitle_ExmCnt = intCnt

            If miCIdx = 0 Then miPageNo = 0

            Dim intCol As Integer = miCCol
            For intCol = miCCol To miTotExmCnt Step miTitle_ExmCnt

                For intIdx As Integer = miCIdx To maPrtData.Count - 1
                    If sngPosY = 0 Then
                        sngPosY = fnPrtTitle_Fixed(e, CType(maPrtData.Item(intIdx), FGS03_PATINFO).sTNms.Split("|"c), miCCol)
                    End If

                    '-- 번호
                    rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * 0, msgPosX(1) - msgPosX(0), sngPrtH * 3)
                    e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

                    '-- 처방일시
                    rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 0, msgPosX(2) - msgPosX(1), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sOrdDt, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                    '-- 등록번호
                    rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * 0, msgPosX(3) - msgPosX(2), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                    '-- 성명
                    rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * 0, msgPosX(4) - msgPosX(3), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                    '-- 진료과/병동
                    rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * 0, msgPosX(5) - msgPosX(4), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sDeptWard, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                    '-- 채혈일시
                    rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 1, msgPosX(2) - msgPosX(1), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sCollDt, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                    '-- 작업번호
                    rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * 1, msgPosX(3) - msgPosX(2), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sCollNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                    '-- 성별/나이
                    rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * 1, msgPosX(4) - msgPosX(3), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sSexAge, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                    '-- 검체명
                    rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * 1, msgPosX(5) - msgPosX(4), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sSpcNmd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                    '-- 접수일시
                    rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 2, msgPosX(2) - msgPosX(1), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sTkDt, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                    '-- 접수자
                    rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * 2, msgPosX(3) - msgPosX(2), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sTkNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                    '-- 의사 Remark
                    rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * 2, msgPosX(5) - msgPosX(3), sngPrtH)
                    e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGS03_PATINFO).sDocRmk, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                    Dim strTnm() As String = CType(maPrtData.Item(intIdx), FGS03_PATINFO).sTNms.Split("|"c)
                    Dim strRst() As String = CType(maPrtData.Item(intIdx), FGS03_PATINFO).sRsts.Split("|"c)

                    intCnt = 0
                    For intIx1 As Integer = miCCol To miCCol + miTitle_ExmCnt
                        intCnt += 1
                        If intCnt > miTitle_ExmCnt Or intIx1 > miTotExmCnt Then
                            Exit For
                        End If

                        ''-- 검사명
                        'rect = New Drawing.RectangleF(msgPosX(4 + intCnt), sngPosY + sngPrtH * 0, msgPosX(5 + intCnt) - msgPosX(4 + intCnt), sngPrtH)
                        'e.Graphics.DrawString(strTnm(intIx1 - 1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                        '-- 이전검사결과
                        rect = New Drawing.RectangleF(msgPosX(4 + intCnt), sngPosY + sngPrtH * 0, msgPosX(5 + intCnt) - msgPosX(4 + intCnt), sngPrtH)
                        e.Graphics.DrawString(strRst(intIx1 - 1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                    Next

                    sngPosY += sngPrtH * 3 + sngPrtH / 2
                    If msgHeight - sngPrtH * 6 < sngPosY Then Exit For

                    e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrtH / 2, msgWidth, sngPosY - sngPrtH / 2)

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
            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)

            e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
            e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

            If miCIdx < maPrtData.Count - 1 Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Sub

    Public Overridable Function fnPrtTitle_Fixed(ByVal e As PrintPageEventArgs, ByRef rsExmNm() As String, ByVal riColS As Integer) As Single
        Dim sFn As String = "Public Overridable Function fnPrtTitle_Fixed(PrintPageEventArgs, ByRef String(), Integer) As Single"
        Try
            Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
            Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
            Dim sngPrt As Single = 0
            Dim sngPosY As Single = 0
            Dim intCnt As Integer = 1

            Dim sngPosX(0 To miTitle_ExmCnt + 6) As Single

            sngPosX(0) = msgLeft
            sngPosX(1) = sngPosX(0) + 40
            sngPosX(2) = sngPosX(1) + 160
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

            fnPrtTitle_Fixed = sngPosY + sngPrt * 3 + sngPrt / 2

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

            intCnt = 0

            For intIdx As Integer = riColS To riColS + miTitle_ExmCnt - 1
                If intIdx > miTotExmCnt Then Exit For

                e.Graphics.DrawString(rsExmNm(intIdx - 1), fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5 + intCnt), sngPosY + sngPrt * 0, sngPosX(6 + intCnt) - sngPosX(5 + +intCnt), sngPrt * 3), sf_l)
                intCnt += 1
            Next

            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt * 3, msgWidth, sngPosY + sngPrt * 3)

            msgPosX = sngPosX

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Function

End Class