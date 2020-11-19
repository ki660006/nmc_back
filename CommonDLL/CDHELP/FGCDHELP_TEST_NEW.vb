Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports Oracle.DataAccess.Client

Imports System.Drawing.Drawing2D
Imports Microsoft.Office.Interop
Imports Microsoft.Office

Imports System
Imports System.Xml

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Public Class FGCDHELP_TEST_NEW
    Private Const msFile As String = "File : CDHELP.vb, Class : FGCDHELP_TEST_NEW" + vbTab
    Private moCtrlcol As Collection
    Private msTestFile As String = Application.StartupPath + msXML + "\FGCDHELP_TEST_NEW.XML"

    Private Stack As New ArrayList
    Private Stackseq As Integer = 0
    Private Stackgbn As Boolean = False

    Private msFrmGbn As String = ""
    Private msTestcd As String = ""

    Private mTestcd As String = ""
    Private mTordcd As String = ""


    Private Const msXML As String = "\XML"

    '<< JJH 프로세스 ID search
    Private Declare Auto Function GetWindowThreadProcessId Lib "user32" (ByVal hWnd As IntPtr, ByRef lpdwProcessid As Integer) As Long

    Private Sub sbPrint_Data()
        Dim sFn As String = "Sub sbPrint_Data()"

        Try
            Dim o_PrtInfo As New PRT_INFO_NEW
            Dim prt As New TESTINFO_PRINT_NEW

            With o_PrtInfo
                If Me.txtTCode.Tag.ToString.IndexOf("^"c) >= 0 Then
                    .TestCd = Me.txtTCode.Tag.ToString.Split("^"c)(0).Trim '검사코드
                    .OrdCd = Me.txtTCode.Tag.ToString.Split("^"c)(1).Trim  '처방코드
                Else
                    If Me.lblTest.Text.Trim = "검사코드" Then
                        .TestCd = Me.txtTCode.Text.Trim '검사코드
                    Else
                        .OrdCd = Me.txtTCode.Text.Trim '처방코드
                    End If
                End If
                .TestNm = Me.txtTnmd.Text.Trim '검사명
                .Spcnm = Me.cboSpc.Text.Split("|"c)(0).Trim '검체명
                .TubeNm = Me.txtTubeNmd.Text.Trim '용기명'
                .OrdSlip = Me.txtOrdSlip.Text.Trim '처방슬립 (사용안함)
                .PartSlip = Me.txtSlipNmd.Text.Trim ' 검사분야

                .UsDate = Me.txtUsDt.Text.Trim '시작일시(사용안함)
                .ExLab = Me.txtExLabYn.Text.Trim '위탁기관(사용안함)
                .Rrptst = Me.txtRrptst.Text '??(사용안함)

                '실시요일
                .ExeDay = ""
                .ExeDay += IIf(Me.chkExeDay1.Checked, "1", "0").ToString '월
                .ExeDay += IIf(Me.chkExeDay2.Checked, "1", "0").ToString '화
                .ExeDay += IIf(Me.chkExeDay3.Checked, "1", "0").ToString '수
                .ExeDay += IIf(Me.chkExeDay4.Checked, "1", "0").ToString '목
                .ExeDay += IIf(Me.chkExeDay5.Checked, "1", "0").ToString '금
                .ExeDay += IIf(Me.chkExeDay6.Checked, "1", "0").ToString '토
                .ExeDay += IIf(Me.chkExeDay7.Checked, "1", "0").ToString '일

                .ErGbn = ""
                .ErGbn += IIf(Me.chkErGbn2.Checked, "1", "0").ToString '응급여부 (사용안함
                .ErGbn += IIf(Me.chkErGbn1.Checked, "1", "0").ToString

                '-- 세부검사코드
                Dim sTestInfo As String = ""

                With Me.spdTestInfo
                    For ix As Integer = 1 To .MaxRows
                        For iCol As Integer = 1 To .MaxCols
                            .Row = ix
                            .Col = iCol : sTestInfo += .Text.Trim + "^"
                        Next
                        sTestInfo += "|"
                    Next
                End With

                .TestLIst = sTestInfo

                .RefTxt = Me.txtRef.Text '참고치
                .CWarning = Me.txtCWarning.Text '채혈시 주의사항(사용안함)

                .Info1 = Me.txtInfo1.Text '검사법
                .Info2 = Me.txtInfo2.Text ' ??(사용안함)
                .Info3 = Me.txtInfo3.Text '??(사용안함)

                '<2019-12-11 추가되는 부분
                .PartCd = Me.txtpartnmd.Text '부서(추가해야함)
                '시행처
                .Execution = ""
                .Execution += IIf(Me.CheckBox3.Checked, "1", "0").ToString '원내
                .Execution += IIf(Me.CheckBox2.Checked, "1", "0").ToString '원외
                .Execution += IIf(Me.CheckBox1.Checked, "1", "0").ToString '국가기관 보건환경연구원
                .Execution += IIf(Me.CheckBox4.Checked, "1", "0").ToString '국가기관 질병관리본부
                '검사의뢰서/동의서
                .AgreeMent = ""
                .AgreeMent += IIf(Me.CheckBox7.Checked, "1", "0").ToString '의뢰서
                .AgreeMent += IIf(Me.CheckBox6.Checked, "1", "0").ToString '동의서
                .AgreeMent += IIf(Me.CheckBox5.Checked, "1", "0").ToString '해당없음

                .TelNo = Me.txtTelNo.Text '내선번호
                '검사소요시간
                .TATME = Me.txtERPTAT.Text '응급중간보고
                .TATM = Me.txtPTAT.Text '일반중간보고
                .TATFE = Me.txtERFTAT.Text '응급최종보고
                .TATF = Me.txtFTAT.Text '일반최종보고

                .Vol = Me.txtVol.Text '검체량
                .ETC = Me.txtCWarning.Text '검사체취및 의뢰시 주의사항 

            End With

            prt.mbLandscape = False  '-- false : 세로, true : 가로
            prt.m_PrtInfo = o_PrtInfo
            prt.sbPrint_Preview()


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Function fnFindControl(ByVal ra_FrmCtrl As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control
        Dim sFn As String = "Private Function fnFindChildControl(System.Windows.Forms.Control.ControlCollection, Collection) "

        Try
            Dim ctrl As System.Windows.Forms.Control

            For Each ctrl In ra_FrmCtrl
                If ctrl.Controls.Count > 0 Then
                    fnFindControl(ctrl.Controls)
                ElseIf ctrl.Controls.Count = 0 Then
                    moCtrlcol.Add(ctrl)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Sub sbInti(ByVal ro_frm As System.Windows.Forms.Form)


        Dim objCtrl As System.Windows.Forms.Control

        moCtrlcol = New Collection

        fnFindControl(ro_frm.Controls)

        For Each objCtrl In moCtrlcol
            If TypeOf (objCtrl) Is AxFPSpreadADO.AxfpSpread Then

                With CType(objCtrl, AxFPSpreadADO.AxfpSpread)
                    .Font = New Font("굴림체", 9, FontStyle.Regular)

                    .SelBackColor = Drawing.Color.FromArgb(213, 215, 255)
                    .SelForeColor = SystemColors.InactiveBorder

                    .ShadowColor = Drawing.Color.FromArgb(165, 186, 222)
                    .ShadowDark = Color.DimGray
                    .ShadowText = SystemColors.ControlText

                    .GrayAreaBackColor = Drawing.Color.FromArgb(236, 242, 255)
                End With
            Else
                'objCtrl.Font = New Font("굴림체", 9, FontStyle.Regular)
            End If
        Next
    End Sub

    Private Sub sbClear_Form()
        Dim sFn As String = "Private Sub sbClear_Form()"

        Try

            Me.cboSpc.Items.Clear()
            Me.txtUsDt.Text = ""
            Me.txtSlipNmd.Text = ""
            Me.txtOrdSlip.Text = ""

            Me.txtTnmd.Text = ""
            Me.txtTubeNmd.Text = ""
            Me.txtExLabYn.Text = ""
            Me.txtRrptst.Text = ""
            Me.txtTelNo.Text = ""

            Me.chkExeDay1.Checked = False : Me.chkExeDay2.Checked = False : Me.chkExeDay3.Checked = False
            Me.chkExeDay4.Checked = False : Me.chkExeDay5.Checked = False : Me.chkExeDay6.Checked = False
            Me.chkExeDay7.Checked = False

            Me.chkErGbn1.Checked = False : Me.chkErGbn2.Checked = False
            Me.txtERPTAT.Text = ""

            Me.spdTestInfo.MaxRows = 0

            Me.txtRef.Text = ""
            Me.txtInfo1.Text = ""
            Me.txtInfo2.Text = ""
            Me.txtCWarning.Text = ""
            Me.txtspcunit.Text = ""

            Me.picTube.Image = Nothing

            Me.txtInfo3.Text = ""
            Me.txtVol.Text = "" 'JJH

            Me.CheckBox3.Checked = False
            Me.CheckBox2.Checked = False
            Me.CheckBox1.Checked = False
            Me.CheckBox4.Checked = False
            Me.CheckBox7.Checked = False
            Me.CheckBox6.Checked = False
            Me.CheckBox5.Checked = False
            txtERPTAT.Text = "" : txtPTAT.Text = "" : txtERFTAT.Text = "" : txtFTAT.Text = ""
            txtVol.Text = "" : txtTubeNmd.Text = "" : txtpartnmd.Text = ""


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Test(ByVal rsTestCd As String)
        Dim sFn As String = "Private Sub sbDisplay_Test()"
        Try

            sbClear_Form()

            Dim dt As New DataTable

            dt = (New CDHELP.DA_CDHELP_TEST_NEW).fnGet_spc_info(rsTestCd)

            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Me.cboSpc.Items.Add(dt.Rows(ix).Item("spcnmd").ToString + Space(100) + "|" + dt.Rows(ix).Item("spccd").ToString.Trim)
                Next
            End If


            If Me.cboSpc.Items.Count > 0 Then Me.cboSpc.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_TestSpc(ByVal rsTestCd As String, ByVal rsSpcCd As String)
        Dim sFn As String = "Private Sub sbDisplay_TestSpc()"
        Dim arry As New ArrayList

        Try
            Dim sTubeCd As String = ""

            Dim dt As New DataTable

            dt = (New DA_CDHELP_TEST_NEW).fnGet_test_info(rsTestCd, rsSpcCd)

            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    If ix = 0 Then Me.txtTnmd.Text = dt.Rows(ix).Item("tnmd").ToString.Trim

                    Select Case dt.Rows(ix).Item("infogbn").ToString.Trim
                        Case "1" : Me.txtInfo1.Text = dt.Rows(ix).Item("testinfo").ToString
                        Case "2" : Me.txtInfo2.Text = dt.Rows(ix).Item("testinfo").ToString
                        Case "3" : Me.txtInfo3.Text = dt.Rows(ix).Item("testinfo").ToString
                    End Select
                Next
            End If

            dt = (New DA_CDHELP_TEST_NEW).fnGet_testspc_info(rsTestCd, rsSpcCd)

            If dt.Rows.Count > 0 Then
                sTubeCd = dt.Rows(0).Item("tubecd").ToString.Trim

                Me.txtCWarning.Text = dt.Rows(0).Item("cowarning").ToString.Trim

                Me.txtUsDt.Text = dt.Rows(0).Item("usdt").ToString.Trim
                Me.txtTnmd.Text = dt.Rows(0).Item("tnmd").ToString.Trim
                Me.txtTubeNmd.Text = dt.Rows(0).Item("tubenmd").ToString.Trim
                Me.txtSlipNmd.Text = dt.Rows(0).Item("slipnmd").ToString.Trim
                Me.txtpartnmd.Text = dt.Rows(0).Item("partnmd").ToString.Trim
                Me.txtOrdSlip.Text = dt.Rows(0).Item("tordslipnm").ToString.Trim
                Me.txtRrptst.Text = dt.Rows(0).Item("rrptst").ToString.Trim
                Me.txtTelNo.Text = dt.Rows(0).Item("telno").ToString.Trim
                Me.txtExLabYn.Text = dt.Rows(0).Item("exlabyn").ToString.Trim 'IIf(dt.Rows(0).Item("exlabyn").ToString = "1", "Y", "N").ToString

                Me.txtRef.Text = dt.Rows(0).Item("descref").ToString
                Me.txtVol.Text = dt.Rows(0).Item("minspcvol").ToString

                Me.txtrstunit.Text = dt.Rows(0).Item("rstunit").ToString '참고치단위
                Me.txtspcunit.Text = dt.Rows(0).Item("spcunit").ToString '검체단위

                '<< 시행처
                For ix As Integer = 0 To dt.Rows(0).Item("enforcement").ToString.Trim.Length - 1
                    If dt.Rows(0).Item("enforcement").ToString.Trim.Substring(ix, 1) = "1" Then
                        Select Case ix
                            Case 0 : Me.CheckBox3.Checked = True
                            Case 1 : Me.CheckBox2.Checked = True
                            Case 2 : Me.CheckBox1.Checked = True
                            Case 3 : Me.CheckBox4.Checked = True
                        End Select
                    Else
                        Select Case ix
                            Case 0 : Me.CheckBox3.Checked = False
                            Case 1 : Me.CheckBox2.Checked = False
                            Case 2 : Me.CheckBox1.Checked = False
                            Case 3 : Me.CheckBox4.Checked = False
                        End Select
                    End If
                Next

                '<< 검사의뢰서/동의서
                For ix As Integer = 0 To dt.Rows(0).Item("request").ToString.Trim.Length - 1
                    If dt.Rows(0).Item("request").ToString.Trim.Substring(ix, 1) = "1" Then
                        Select Case ix
                            Case 0 : Me.CheckBox5.Checked = True
                            Case 1 : Me.CheckBox7.Checked = True
                            Case 2 : Me.CheckBox6.Checked = True
                        End Select
                    Else
                        Select Case ix
                            Case 0 : Me.CheckBox5.Checked = False
                            Case 1 : Me.CheckBox7.Checked = False
                            Case 2 : Me.CheckBox6.Checked = False
                        End Select
                    End If
                Next

                ''<< 시행처
                'If dt.Rows(0).Item("enforcement").ToString = "0" Then '원내
                '    CheckBox3.Checked = True
                'ElseIf dt.Rows(0).Item("enforcement").ToString = "1" Then '원외
                '    CheckBox2.Checked = True
                'ElseIf dt.Rows(0).Item("enforcement").ToString = "2" Then '국가기관 보건환경연구원
                '    CheckBox1.Checked = True
                'ElseIf dt.Rows(0).Item("enforcement").ToString = "3" Then '국가기관 질병관리본부
                '    CheckBox4.Checked = True
                'End If

                ''<< 검사동의서/의뢰서
                'If dt.Rows(0).Item("request").ToString = "0" Then '해당없음
                '    CheckBox5.Checked = True
                'ElseIf dt.Rows(0).Item("request").ToString = "1" Then '의뢰서
                '    CheckBox7.Checked = True
                'ElseIf dt.Rows(0).Item("request").ToString = "2" Then '동의서
                '    CheckBox6.Checked = True
                'End If


                If dt.Rows(0).Item("tatyn").ToString = "1" Then
                    Dim sTmp As String = ""
                    Dim iTmpD As Integer = 0 'dt.Rows("prptmi").ToString
                    Dim iTmpH As Integer = 0
                    Dim iTmpM As Integer = 0
                    Dim iTmp As Integer = 0
                    'prptmi -> 일반중간보고TAT ,frptmi ->일반최종보고TAT

                    '일반중간보고TAT
                    If dt.Rows(0).Item("prptmi").ToString <> "" Then
                        iTmp = Convert.ToInt32(dt.Rows(0).Item("prptmi").ToString) / 60
                        If iTmp > 24 Then
                            iTmpD = iTmp / 24

                            If iTmp <> iTmpD * 24 Then
                                iTmp = iTmp - (iTmpD * 24)
                            Else
                                iTmp = 0
                            End If
                        Else
                            iTmp = Convert.ToInt32(dt.Rows(0).Item("prptmi").ToString)
                        End If

                        iTmpH = iTmp / 60
                        If iTmpH > 0 Then
                            If iTmp <> iTmpH * 60 Then
                                iTmp = iTmp - (iTmpH * 60)
                            Else
                                iTmp = 0
                            End If
                        End If

                        iTmpM = iTmp

                        sTmp = ""
                        sTmp += IIf(iTmpD > 0, iTmpD.ToString + "일 ", "").ToString
                        sTmp += IIf(iTmpH > 0, iTmpH.ToString("D2") + ":", "00:").ToString + iTmpM.ToString("D2") + ":00"

                        'Me.txtPTAT.Text += sTmp + Space(10)
                        Me.txtPTAT.Text = sTmp
                    End If
                    '일반최종보고TAT
                    If dt.Rows(0).Item("frptmi").ToString <> "" Then
                        iTmp = Convert.ToInt32(dt.Rows(0).Item("frptmi").ToString) / 60
                        If iTmp > 24 Then
                            iTmpD = iTmp / 24

                            If iTmp <> iTmpD * 24 Then
                                iTmp = iTmp - (iTmpD * 24)
                            Else
                                iTmp = 0
                            End If
                        Else
                            iTmp = Convert.ToInt32(dt.Rows(0).Item("frptmi").ToString)
                        End If

                        iTmpH = iTmp / 60
                        If iTmpH > 0 Then
                            If iTmp <> iTmpH * 60 Then
                                iTmp = iTmp - (iTmpH * 60)
                            Else
                                iTmp = 0
                            End If
                        End If

                        iTmpM = iTmp

                        sTmp = ""
                        sTmp += IIf(iTmpD > 0, iTmpD.ToString + "일 ", "").ToString
                        sTmp += IIf(iTmpH > 0, iTmpH.ToString("D2") + ":", "00:").ToString + iTmpM.ToString("D2") + ":00"

                        'Me.txtFTAT.Text += sTmp + Space(10)
                        Me.txtFTAT.Text = sTmp
                    End If
                    '응급최종보고TAT
                    If dt.Rows(0).Item("ferrptmi").ToString <> "" Then
                        iTmp = Convert.ToInt32(dt.Rows(0).Item("ferrptmi").ToString) / 60
                        If iTmp > 24 Then
                            iTmpD = iTmp / 24

                            If iTmp <> iTmpD * 24 Then
                                iTmp = iTmp - (iTmpD * 24)
                            Else
                                iTmp = 0
                            End If
                        Else
                            iTmp = Convert.ToInt32(dt.Rows(0).Item("ferrptmi").ToString)
                        End If

                        iTmpH = iTmp / 60
                        If iTmpH > 0 Then
                            If iTmp <> iTmpH * 60 Then
                                iTmp = iTmp - (iTmpH * 60)
                            Else
                                iTmp = 0
                            End If
                        End If

                        iTmpM = iTmp

                        sTmp = ""
                        sTmp += IIf(iTmpD > 0, iTmpD.ToString + "일 ", "").ToString
                        sTmp += IIf(iTmpH > 0, iTmpH.ToString("D2") + ":", "00:").ToString + iTmpM.ToString("D2") + ":00"

                        'Me.txtERFTAT.Text += sTmp + Space(10)
                        Me.txtERFTAT.Text = sTmp
                    End If
                    '응급중간보고 TAT
                    If dt.Rows(0).Item("perrptmi").ToString <> "" Then
                        iTmp = Convert.ToInt32(dt.Rows(0).Item("perrptmi").ToString) / 60
                        If iTmp > 24 Then
                            iTmpD = iTmp / 24

                            If iTmp <> iTmpD * 24 Then
                                iTmp = iTmp - (iTmpD * 24)
                            Else
                                iTmp = 0
                            End If
                        Else
                            iTmp = Convert.ToInt32(dt.Rows(0).Item("perrptmi").ToString)
                        End If

                        iTmpH = iTmp / 60
                        If iTmpH > 0 Then
                            If iTmp <> iTmpH * 60 Then
                                iTmp = iTmp - (iTmpH * 60)
                            Else
                                iTmp = 0
                            End If
                        End If

                        iTmpM = iTmp

                        sTmp = ""
                        sTmp += IIf(iTmpD > 0, iTmpD.ToString + "일 ", "").ToString
                        sTmp += IIf(iTmpH > 0, iTmpH.ToString("D2") + ":", "00:").ToString + iTmpM.ToString("D2") + ":00"

                        'Me.txtERPTAT.Text += sTmp + Space(10)
                        Me.txtERPTAT.Text = sTmp
                    End If

                    'If dt.Rows(0).Item("erptmi").ToString <> "" Then
                    '    iTmp = Convert.ToInt32(dt.Rows(0).Item("frptmi").ToString) / 60
                    '    If iTmp > 24 Then
                    '        iTmpD = iTmp / 24

                    '        If iTmp <> iTmpD * 24 Then
                    '            iTmp = iTmp - (iTmpD * 24)
                    '        Else
                    '            iTmp = 0
                    '        End If
                    '    Else
                    '        iTmp = Convert.ToInt32(dt.Rows(0).Item("frptmi").ToString)
                    '    End If

                    '    iTmpH = iTmp / 60
                    '    If iTmpH > 0 Then
                    '        If iTmp <> iTmpH * 60 Then
                    '            iTmp = iTmp - (iTmpH * 60)
                    '        Else
                    '            iTmp = 0
                    '        End If
                    '    End If

                    '    iTmpM = iTmp

                    '    sTmp = ""
                    '    sTmp += IIf(iTmpD > 0, iTmpD.ToString + "일 ", "").ToString
                    '    sTmp += IIf(iTmpH > 0, iTmpH.ToString("D2") + ":", "00:").ToString + iTmpM.ToString("D2") + ":00"

                    '    Me.txtERPTAT.Text += "응급TAT: " + sTmp + " 분"
                    'End If
                End If

                For ix As Integer = 0 To dt.Rows(0).Item("exeday").ToString.Trim.Length - 1
                    If dt.Rows(0).Item("exeday").ToString.Trim.Substring(ix, 1) = "1" Then
                        Select Case ix
                            Case 0 : Me.chkExeDay1.Checked = True
                            Case 1 : Me.chkExeDay2.Checked = True
                            Case 2 : Me.chkExeDay3.Checked = True
                            Case 3 : Me.chkExeDay4.Checked = True
                            Case 4 : Me.chkExeDay5.Checked = True
                            Case 5 : Me.chkExeDay6.Checked = True
                            Case 6 : Me.chkExeDay7.Checked = True
                        End Select
                    Else
                        Select Case ix
                            Case 0 : Me.chkExeDay1.Checked = False
                            Case 1 : Me.chkExeDay2.Checked = False
                            Case 2 : Me.chkExeDay3.Checked = False
                            Case 3 : Me.chkExeDay4.Checked = False
                            Case 4 : Me.chkExeDay5.Checked = False
                            Case 5 : Me.chkExeDay6.Checked = False
                            Case 6 : Me.chkExeDay7.Checked = False
                        End Select

                    End If
                Next

                If dt.Rows(0).Item("ergbn1").ToString = "1" Then Me.chkErGbn1.Checked = True
                If dt.Rows(0).Item("ergbn2").ToString = "1" Then Me.chkErGbn2.Checked = True

                'If Me.txtInfo1.Text <> "" Then Me.txtInfo1.Text += vbCrLf
                'Me.txtInfo1.Text += dt.Rows(0).Item("reftest").ToString.Trim


            End If

            If Me.txtRef.Text.Replace(vbCrLf, "").Trim = "" Then
                dt = (New DA_CDHELP_TEST_NEW).fnGet_test_ref(rsTestCd, rsSpcCd, Me.txtUsDt.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

                If dt.Rows.Count > 0 Then
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        If ix = 0 Then Me.txtTnmd.Text = dt.Rows(ix).Item("tnmd").ToString.Trim
                        If ix > 0 Then Me.txtRef.Text += vbCrLf

                        Dim sAgeYmd As String = ""
                        Select Case dt.Rows(ix).Item("ageymd").ToString
                            Case "Y" : sAgeYmd = " 살"
                            Case "M" : sAgeYmd = " 달"
                            Case "D" : sAgeYmd = " 일"

                        End Select

                        If dt.Rows(ix).Item("descref").ToString.Trim <> "" Then
                            Me.txtRef.Text += dt.Rows(ix).Item("descref").ToString.Trim
                            Exit For
                        ElseIf dt.Rows(ix).Item("refgbn").ToString.Trim <> "0" Then
                            If dt.Rows(ix).Item("eage").ToString.Trim = "999" Then
                                Me.txtRef.Text += "* 전 연령 " + vbCrLf
                            Else

                                Me.txtRef.Text += dt.Rows(ix).Item("sage").ToString.Trim + sAgeYmd
                                Me.txtRef.Text += " ~ "
                                Me.txtRef.Text += dt.Rows(ix).Item("eage").ToString.Trim + sAgeYmd + ": " + vbCrLf
                                'Me.txtRef.Text += dt.Rows(ix).Item("sage").ToString.Trim + sAgeYmd + IIf(dt.Rows(ix).Item("sages").ToString.Trim = "0", " <= ", " < ").ToString
                                'Me.txtRef.Text += "환자" + IIf(dt.Rows(ix).Item("eages").ToString.Trim = "0", " <= ", " < ").ToString
                                'Me.txtRef.Text += dt.Rows(ix).Item("eage").ToString.Trim + sAgeYmd + ": " + vbCrLf
                            End If


                            If dt.Rows(ix).Item("refgbn").ToString = "1" Then
                                Me.txtRef.Text += Space(4) + dt.Rows(ix).Item("reflt").ToString.Trim
                            Else
                                'Me.txtRef.Text += Space(4) + "(남) " + dt.Rows(ix).Item("reflm").ToString.Trim + IIf(dt.Rows(ix).Item("reflms").ToString.Trim = "0", " <= ", " < ").ToString + "결과"
                                'Me.txtRef.Text += IIf(dt.Rows(ix).Item("refhms").ToString.Trim = "0", " <= ", " < ").ToString
                                'Me.txtRef.Text += dt.Rows(ix).Item("refhm").ToString.Trim + ", " + vbCrLf

                                'Me.txtRef.Text += Space(4) + "(여) " + dt.Rows(ix).Item("reflf").ToString.Trim + IIf(dt.Rows(ix).Item("reflfs").ToString.Trim = "0", " <= ", " < ").ToString + "결과"
                                'Me.txtRef.Text += IIf(dt.Rows(ix).Item("refhfs").ToString.Trim = "0", " <= ", " < ").ToString
                                'Me.txtRef.Text += dt.Rows(ix).Item("refhf").ToString.Trim

                                Me.txtRef.Text += Space(4) + "(남) " + dt.Rows(ix).Item("reflm").ToString.Trim
                                Me.txtRef.Text += " ~ "
                                Me.txtRef.Text += dt.Rows(ix).Item("refhm").ToString.Trim + ", " + vbCrLf

                                Me.txtRef.Text += Space(4) + "(여) " + dt.Rows(ix).Item("reflf").ToString.Trim
                                Me.txtRef.Text += " ~ "
                                Me.txtRef.Text += dt.Rows(ix).Item("refhf").ToString.Trim
                            End If
                        End If
                    Next
                End If

            End If

            dt = (New DA_CDHELP_TEST_NEW).fnGet_testspc_ref(rsTestCd, rsSpcCd, Me.txtUsDt.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt.Rows.Count > 0 Then
                With spdTestInfo
                    .MaxRows = dt.Rows.Count
                    Dim dt_Ref As DataTable = New DataTable

                    mTestcd = dt.Rows(0).Item("testcd").ToString
                    mTordcd = dt.Rows(0).Item("tordcd").ToString

                    For ix As Integer = 0 To dt.Rows.Count - 1
                        dt_Ref = (New DA_CDHELP_TEST_NEW).fnGet_test_ref(dt.Rows(ix).Item("testcd").ToString.Trim, rsSpcCd, Me.txtUsDt.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

                        .Row = ix + 1
                        .Col = .GetColFromID("tcdgbn") : .Text = dt.Rows(ix).Item("tcdgbn").ToString.Trim
                        .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString.Trim
                        .Col = .GetColFromID("tordcd") : .Text = dt.Rows(ix).Item("tordcd").ToString.Trim
                        .Col = .GetColFromID("sugacd") : .Text = dt.Rows(ix).Item("sugacd").ToString.Trim
                        .Col = .GetColFromID("edicd") : .Text = dt.Rows(ix).Item("edicd").ToString.Trim
                        .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString.Trim
                        .Col = .GetColFromID("unit") : .Text = dt.Rows(ix).Item("rstunit").ToString.Trim
                        For ix2 As Integer = 0 To dt_Ref.Rows.Count - 1
                            .Row = ix + 1
                            .Col = .GetColFromID("reftxt")
                            If ix2 > 0 Then .Text += Chr(13) & Chr(10)

                            Dim sAgeYmd As String = ""
                            Select Case dt_Ref.Rows(ix2).Item("ageymd").ToString
                                Case "Y" : sAgeYmd = " 살"
                                Case "M" : sAgeYmd = " 달"
                                Case "D" : sAgeYmd = " 일"

                            End Select

                            If dt_Ref.Rows(ix2).Item("descref").ToString.Trim <> "" Then
                                .Text += dt_Ref.Rows(ix2).Item("descref").ToString.Trim
                                Exit For
                            ElseIf dt_Ref.Rows(ix2).Item("refgbn").ToString.Trim <> "0" Then
                                If dt_Ref.Rows(ix2).Item("eage").ToString.Trim = "999" Then
                                    .Text = "* 전 연령 " + vbCrLf

                                Else
                                    .Text += dt_Ref.Rows(ix2).Item("sage").ToString.Trim + sAgeYmd
                                    .Text += " ~ "
                                    .Text += dt_Ref.Rows(ix2).Item("eage").ToString.Trim + sAgeYmd + ": " + vbCrLf
                                End If


                                If dt_Ref.Rows(ix2).Item("refgbn").ToString = "1" Then
                                    .Text += Space(4) + dt_Ref.Rows(ix2).Item("reflt").ToString.Trim
                                Else
                                    .Text += Space(4) + "(남) " + dt_Ref.Rows(ix2).Item("reflm").ToString.Trim
                                    .Text += " ~ "
                                    .Text += dt_Ref.Rows(ix2).Item("refhm").ToString.Trim + ", " + vbCrLf

                                    .Text += Space(4) + "(여) " + dt_Ref.Rows(ix2).Item("reflf").ToString.Trim
                                    .Text += " ~ "
                                    .Text += dt_Ref.Rows(ix2).Item("refhf").ToString.Trim
                                End If
                            End If
                        Next
                    Next
                End With
            End If

            Dim a_btBuf As Byte() = (New DA_CDHELP_TEST_NEW).fnGet_tube_img(sTubeCd)

            If a_btBuf Is Nothing Then Return

            Dim sDir As String = "c:\ack\Image"
            Dim sFileNm = sDir + "\Tube_" + sTubeCd + ".jpg"

            If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

            Dim fs As IO.FileStream

            If a_btBuf IsNot Nothing Then

                If IO.File.Exists(sFileNm) Then
                    Try
                        Threading.Thread.Sleep(1000)
                        IO.File.Delete(sFileNm)
                    Catch ex As Exception

                        Dim bmpTmp As Bitmap = New Bitmap(sFileNm)

                        Me.picTube.Image = CType(bmpTmp, Image)
                        Return
                    End Try
                End If

                fs = New IO.FileStream(sFileNm, IO.FileMode.Create, FileAccess.Write)

            Else
                Me.picTube.Image = Nothing

                Return
            End If

            Dim bw As IO.BinaryWriter = New IO.BinaryWriter(fs)

            bw.Write(a_btBuf)
            bw.Flush()

            bw.Close()
            fs.Close()

            Dim bmpBuf As Bitmap = New Bitmap(sFileNm)

            'Me.picTube.Width = 138
            'Me.picTube.Height = 138

            Me.picTube.Width = 138
            Me.picTube.Height = 138

            Me.picTube.Image = CType(bmpBuf, Image)

            bmpBuf = Nothing
            fs = Nothing

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        Me.txtTCode.Text = ""

    End Sub

    Public Sub New(ByVal rsQryGbn As String, ByVal rsTestCd As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        If rsQryGbn = "O" Then
            Me.lblTest.Text = "처방코드"
        Else
            Me.lblTest.Text = "검사코드"
            rsTestCd = rsTestCd.Substring(0, 5)
        End If
        Me.txtTCode.Text = rsTestCd

    End Sub

    Private Sub FGCDHELP_TEST_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub FGCDHELP_TEST_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            sbInti(Me)
            sbClear_Form()

            MdiMain.Frm = Me
            MdiMain.FrmMenu = Me.Menu

            Dim strArg As [String]() = System.Environment.GetCommandLineArgs()

            For intIdx As Integer = 0 To strArg.Length - 1
                Select Case intIdx
                    Case 1  ' 구분
                        msFrmGbn = strArg(intIdx)
                    Case 2  ' 처방/검사코드
                        msTestcd = strArg(intIdx)
                End Select
            Next

            If msFrmGbn = "O" Then
                Me.lblTest.Text = "처방코드"
            Else
                If msTestcd <> "" Then
                    msTestcd = msTestcd.Substring(0, 5)
                End If

            End If

            If msTestcd <> "" Then
                Me.txtTCode.Text = msTestcd
            End If


            If Me.txtTCode.Text <> "" Then btnCdHelp_test_Click(Me.btnCdHelp_test, Nothing)

            Me.Left = MdiMain.Frm.Location.X + (MdiMain.Frm.Width - Me.Width) / 2
            Me.Top = MdiMain.Frm.Location.Y + (MdiMain.Frm.Height - Me.Height) / 2

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click, btnCdHelp_Tnm.Click
        Dim sFn As String = "Handles btnCdHelp_test.Click"
        Try
            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As New DataTable

            If CType(sender, Windows.Forms.Button).Name.ToLower = "btncdhelp_test" Then
                dt = (New DA_CDHELP_TEST_NEW).fnGet_Help_Info("")
            Else
                dt = (New DA_CDHELP_TEST_NEW).fnGet_Help_Info(Me.txtTnmd.Text)
            End If

            If CType(sender, Windows.Forms.Button).Name.ToUpper = "BTNCDHELP_TNM" Then
            Else
                If Me.txtTCode.Text <> "" Then
                    Dim a_dr As DataRow()
                    If Me.lblTest.Text.Trim = "처방코드" Then
                        a_dr = dt.Select("tordcd = '" + Me.txtTCode.Text + "'", "")
                    Else
                        a_dr = dt.Select("testcd = '" + Me.txtTCode.Text + "'", "")
                    End If

                    dt = Fn.ChangeToDataTable(a_dr)

                End If
            End If

            objHelp.FormText = "검사목록"

            objHelp.MaxRows = 15
            objHelp.OnRowReturnYN = True

            objHelp.AddField("tnmd", "검사명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tordcd", "처방코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tordslipnm", "처방슬립", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            'objHelp.AddField("slipnmd", "검사분류", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtTestCd.Text = alList.Item(0).ToString.Split("|"c)(1)
                If Me.txtTCode.Text = "" Then
                    If Me.lblTest.Text.Trim = "검사코드" Then
                        Me.txtTCode.Text = alList.Item(0).ToString.Split("|"c)(1)
                    Else
                        Me.txtTCode.Text = alList.Item(0).ToString.Split("|"c)(2)
                    End If
                End If
                Me.txtTCode.Tag = alList.Item(0).ToString.Split("|"c)(1) + "^" + alList.Item(0).ToString.Split("|"c)(2)

                '<JJH 새로운 항목 조회시
                If Stack.Count <> Stackseq And Stack.Count > Stackseq Then

                    Dim ix As Integer = Stack.Count - Stackseq

                    For i = 1 To ix
                        Stack.RemoveAt(Stackseq)
                    Next

                End If

                sbDisplay_Test(Me.txtTestCd.Text)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub cboSpc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSpc.SelectedIndexChanged

        If Me.cboSpc.Text = "" Then Return

        sbDisplay_TestSpc(Me.txtTestCd.Text, Me.cboSpc.Text.Split("|"c)(1))


        Dim TestInfo As String = Me.txtTestCd.Text + "|" + Me.cboSpc.Text.Split("|"c)(1)
        'Stack1.Push(stck1)

        If Stackseq = 0 Then
            Stack.Add(TestInfo)
            Stackseq += 1
        Else
            If Stack.Count = Stackseq Then
                If TestInfo <> Stack.Item(Stackseq - 1).ToString Then
                    Stack.Add(TestInfo)
                    Stackseq += 1
                End If
            End If
        End If


        'Dim test As String = Me.txtTestCd.Text + "|" + Me.cboSpc.Text.Split("|"c)(1)

        'If Stack1.Contains(test) = False Then
        '    Stack1.Push(test)
        'End If


    End Sub

    Private Sub txtTCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTCode.KeyDown

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Me.txtTCode.Text = Me.txtTCode.Text.ToUpper

        Me.txtTCode.Tag = "" : Me.txtTnmd.Text = ""
        sbClear_Form()

        btnCdHelp_test_Click(Me.btnCdHelp_test, Nothing)

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        If Me.lblTest.Text.Trim = "검사코드" Then
            Me.lblTest.Text = "처방코드"
            Me.txtTCode.Text = mTordcd
        Else
            Me.lblTest.Text = "검사코드"
            Me.txtTCode.Text = mTestcd
        End If

    End Sub

    Private Sub txtTnmd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTnmd.Click
        Me.txtTnmd.SelectionStart = 0
        Me.txtTnmd.SelectAll()
    End Sub

    Private Sub txtTnmd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTnmd.KeyDown

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Me.txtTCode.Tag = "" : Me.txtTCode.Text = ""
        sbClear_Form()

        btnCdHelp_test_Click(Nothing, Nothing)

    End Sub


    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        If Me.txtTnmd.Text = "" Or Me.txtTCode.Text = "" Then
            MsgBox("조회된 검사항목이 없습니다 확인하여 주십시요!!")
        End If

        sbPrint()

        'sbPrint_Data()



    End Sub

    Private Sub spdTestInfo_DblClick(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdTestInfo.DblClick
        Dim sTestcd As String = "" : Dim sTordcd As String = ""
        Dim ev = New System.Windows.Forms.KeyEventArgs(Keys.Enter)

        Try

            '<JJH 새로운 항목 조회시
            If Stack.Count <> Stackseq And Stack.Count > Stackseq Then
                Dim ix As Integer = Stack.Count - Stackseq
                For i = 1 To ix
                    Stack.RemoveAt(Stackseq)
                Next
            End If

            With spdTestInfo
                .Row = e.row
                If Me.lblTest.Text.Trim = "처방코드" Then
                    .Col = .GetColFromID("tordcd")
                    sTordcd = .Text
                    If Me.txtTCode.Text <> sTordcd And sTordcd <> "" Then
                        COMMON.CommXML.setOneElementXML(msXML, msTestFile, "TEST", Me.txtTCode.Text + "^" + Me.lblTest.Text.Trim)
                        '  Me.btnUp.Visible = True
                        Me.txtTCode.Text = sTordcd
                        txtTCode_KeyDown(sender, ev)
                    End If

                ElseIf Me.lblTest.Text.Trim = "검사코드" Then
                    .Col = .GetColFromID("testcd")
                    sTestcd = .Text
                    If Me.txtTCode.Text <> sTestcd And sTestcd <> "" Then
                        COMMON.CommXML.setOneElementXML(msXML, msTestFile, "TEST", Me.txtTCode.Text + "^" + Me.lblTest.Text.Trim)
                        ' Me.btnUp.Visible = True
                        Me.txtTCode.Text = sTestcd
                        txtTCode_KeyDown(sender, ev)
                    End If

                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        'Dim ev = New System.Windows.Forms.KeyEventArgs(Keys.Enter)
        'Dim sCodes As String = COMMON.CommXML.getOneElementXML(msXML, msTestFile, "TEST")

        'Dim sTestcd As String = sCodes.Split("^"c)(0)
        'Dim sGbn As String = sCodes.Split("^"c)(1)

        'If sGbn = "검사코드" Then
        '    COMMON.CommXML.setOneElementXML(msXML, msTestFile, "TEST", Me.txtTCode.Text + "^" + Me.lblTest.Text.Trim)
        '    Me.lblTest.Text = "검사코드"
        '    ' Me.btnDown.Visible = True
        '    Me.txtTCode.Text = sTestcd
        '    txtTCode_KeyDown(sender, ev)
        'ElseIf sGbn = "처방코드" Then
        '    COMMON.CommXML.setOneElementXML(msXML, msTestFile, "TEST", Me.txtTCode.Text + "^" + Me.lblTest.Text.Trim)
        '    Me.lblTest.Text = "처방코드"
        '    ' Me.btnDown.Visible = True
        '    Me.txtTCode.Text = sTestcd
        '    txtTCode_KeyDown(sender, ev)
        'End If

        Try
            If Stack.Count > 0 Then

                If Stackseq = 1 Then MsgBox("첫 페이지 입니다.!!") : Return

                Stackseq -= 1

                Dim Info As String = Stack.Item(Stackseq - 1).ToString

                Dim sTestcd As String = Split(Info, "|")(0)
                Dim sSpccd As String = Split(Info, "|")(1)

                sbDisplay_TestSpc(sTestcd, sSpccd)
                Me.txtTestCd.Text = sTestcd
                Me.txtTCode.Text = sTestcd
                sbDisplay_Test(sTestcd)


                'Stackseq -= 1

                'Stackseq += 1
                'MsgBox("첫 페이지 입니다.!!")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Sub

    Private Sub btnOpenDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenDoc.Click
        Dim frm As New FGCDHELP_TEST_NEW_S01
        frm.ShowDialog()
    End Sub



    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click

        'Dim test As String

        'If Stack2.Count > 0 Then
        '    test = Stack2.Peek()
        '    Stack2.Pop()

        '    Dim sTestcd As String = Split(test, "|")(0)
        '    Dim sSpccd As String = Split(test, "|")(1)

        '    Dim test1 As String = Me.txtTestCd.Text + "|" + Me.cboSpc.Text.Split("|"c)(1)

        '    sbDisplay_TestSpc(sTestcd, sSpccd)
        '    Me.txtTestCd.Text = sTestcd
        '    sbDisplay_Test(sTestcd)

        '    Stack1.Push(test1)
        'Else
        '    MsgBox("마지막 !!")
        'End If

        Try
            If Stack.Count > 0 Then

                If Stack.Count = Stackseq Then MsgBox("마지막 페이지 입니다.!!") : Return

                Stackseq += 1

                Dim Info As String = Stack.Item(Stackseq - 1).ToString

                Dim sTestcd As String = Split(Info, "|")(0)
                Dim sSpccd As String = Split(Info, "|")(1)

                sbDisplay_TestSpc(sTestcd, sSpccd)
                Me.txtTestCd.Text = sTestcd
                Me.txtTCode.Text = sTestcd
                sbDisplay_Test(sTestcd)




            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Me.txtTnmd.Text = "" Or Me.txtTCode.Text = "" Then
            MsgBox("조회된 검사항목이 없습니다 확인하여 주십시요!!")
        End If

        sbPrint()

    End Sub

    Public Sub sbPrint(Optional ByVal ExcelYN As Boolean = False)

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(Windows.Forms.Application.StartupPath + "\sample.xls") '경로에 해당파일이 있어야함
            xlsWkS = CType(xlsWkB.ActiveSheet, Excel.Worksheet)

            Dim iCnt As Integer = 0


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

            '-- 1 검사명
            xlsWkS.Range("B" + (1 + 13 * (iCnt - 1)).ToString).Value = Me.txtTnmd.Text

            '-- 2 처방코드 시행처
            xlsWkS.Range("A" + (2 + 13 * (iCnt - 1)).ToString).Value = Me.lblTest.Text
            xlsWkS.Range("B" + (2 + 13 * (iCnt - 1)).ToString).Value = Me.txtTCode.Text

            If Me.CheckBox3.Checked = "1" Then

            End If

            '-- 시행처
            Dim Enforcement As String = ""
            Enforcement = "[" + IIf(CheckBox3.Checked = "1", "√", "  ") + "]원내" + Space(1) + "[" + IIf(CheckBox2.Checked = "1", "√", "  ") + "]원외" + Space(1) _
                + "[" + IIf(CheckBox1.Checked = "1", "√", "  ") + "]국가기관 보건환경연구원" + Space(1) + "[" + IIf(CheckBox4.Checked = "1", "√", "  ") + "]국가기관 질병관리본부"

            xlsWkS.Range("F" + (2 + 13 * (iCnt - 1)).ToString).Value = Enforcement


            '-- 3 의뢰서 동의서 해당없음 내선번호 부서명
            If CheckBox7.Checked = "1" Then '의뢰서
                xlsWkS.Range("B" + (3 + 13 * (iCnt - 1)).ToString).Value = "[√]의뢰서"
            Else
                xlsWkS.Range("B" + (3 + 13 * (iCnt - 1)).ToString).Value = "[  ]의뢰서"
            End If

            If CheckBox6.Checked = "1" Then '동의서
                xlsWkS.Range("C" + (3 + 13 * (iCnt - 1)).ToString).Value = "[√]동의서"
            Else
                xlsWkS.Range("C" + (3 + 13 * (iCnt - 1)).ToString).Value = "[  ]동의서"
            End If

            If CheckBox5.Checked = "1" Then ' 해당없음
                xlsWkS.Range("D" + (3 + 13 * (iCnt - 1)).ToString).Value = "[√]해당없음"
            Else
                xlsWkS.Range("D" + (3 + 13 * (iCnt - 1)).ToString).Value = "[  ]해당없음"
            End If

            xlsWkS.Range("F" + (3 + 13 * (iCnt - 1)).ToString).Value = Me.txtTelNo.Text
            xlsWkS.Range("H" + (3 + 13 * (iCnt - 1)).ToString).Value = Me.txtpartnmd.Text


            '-- 4 검사법 실시요일
            xlsWkS.Range("B" + (4 + 13 * (iCnt - 1)).ToString).Value = Me.txtInfo1.Text

            Dim ExeDayt As String = ""

            ExeDayt = Space(2) + "[" + IIf(chkExeDay1.Checked = "1", "√", "  ") + "]월" + Space(2) + "[" + IIf(chkExeDay2.Checked = "1", "√", "  ") + "]화" + Space(2) _
                + "[" + IIf(chkExeDay3.Checked = "1", "√", "  ") + "]수" + Space(2) + "[" + IIf(chkExeDay4.Checked = "1", "√", "  ") + "]목" + Space(2) + "[" + IIf(chkExeDay5.Checked = "1", "√", "  ") + "]금" + Space(2) _
                + "[" + IIf(chkExeDay6.Checked = "1", "√", "  ") + "]토" + Space(2) + "[" + IIf(chkExeDay7.Checked = "1", "√", "  ") + "]일"

            xlsWkS.Range("F" + (4 + 13 * (iCnt - 1)).ToString).Value = ExeDayt


            '-- 5 검사분야
            xlsWkS.Range("B" + (5 + 13 * (iCnt - 1)).ToString).Value = Me.txtSlipNmd.Text


            '-- 6 참고치 단위 응급중간보고 일반중간보고 응급최종보고 일반최종보고
            xlsWkS.Range("B" + (6 + 13 * (iCnt - 1)).ToString).Value = Me.txtRef.Text
            xlsWkS.Range("D" + (6 + 13 * (iCnt - 1)).ToString).Value = Me.txtrstunit.Text '참고치단위
            xlsWkS.Range("F" + (6 + 13 * (iCnt - 1)).ToString).Value = Me.txtERPTAT.Text '"응급중간보고"
            xlsWkS.Range("G" + (6 + 13 * (iCnt - 1)).ToString).Value = Me.txtPTAT.Text '"일반중간보고"
            xlsWkS.Range("H" + (6 + 13 * (iCnt - 1)).ToString).Value = Me.txtERFTAT.Text '"응급최종보고"
            xlsWkS.Range("I" + (6 + 13 * (iCnt - 1)).ToString).Value = Me.txtFTAT.Text '"일반최종보고"


            '-- 7 검체종류 검체용기명
            xlsWkS.Range("B" + (7 + 13 * (iCnt - 1)).ToString).Value = Split(Me.cboSpc.Text, "|")(0)
            xlsWkS.Range("F" + (7 + 13 * (iCnt - 1)).ToString).Value = Me.txtTubeNmd.Text


            '-- 8
            '<JJH 이미지 엑셀로 넣어주는 부분
            Dim file As String = Windows.Forms.Application.StartupPath + "\tube.png" '경로에 해당파일이 있어야함
            Dim bmp As New Bitmap(Me.picTube.Width, Me.picTube.Height)

            Me.picTube.DrawToBitmap(bmp, Me.picTube.DisplayRectangle)


            bmp.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg)
            xlsWkS.Shapes.AddPicture(file, 0, 1, 420, 180, 174, 130)
            'xlsWkS.Shapes.AddPicture(file, 0, 1, 420, 180, 130, 130)
            '>

            '-- 9 검체량 검체단위
            xlsWkS.Range("B" + (9 + 13 * (iCnt - 1)).ToString).Value = Me.txtVol.Text
            xlsWkS.Range("D" + (9 + 13 * (iCnt - 1)).ToString).Value = Me.txtspcunit.Text


            '< 세부검사목록
            Dim Spdrow As Integer = spdTestInfo.MaxRows '5

            With spdTestInfo
                If Spdrow <= 1 Then
                    .Row = 1

                    .Col = .GetColFromID("tnmd") : Dim Tnmd As String = .Text
                    .Col = .GetColFromID("reftxt") : Dim Reftxt As String = .Text
                    .Col = .GetColFromID("unit") : Dim Unit As String = .Text

                    xlsWkS.Range("A" + (11 + 13 * (iCnt - 1)).ToString).RowHeight = IIf(Reftxt = "", 33, 50)

                    xlsWkS.Range("B" + (11 + 13 * (iCnt - 1)).ToString).Value = Tnmd '검사명
                    xlsWkS.Range("F" + (11 + 13 * (iCnt - 1)).ToString).Value = Reftxt '참고치
                    xlsWkS.Range("I" + (11 + 13 * (iCnt - 1)).ToString).Value = Unit '단위


                    '-- 12 검체 채취 및 의뢰시 주의사항

                    xlsWkS.Range("B" + (13 + 13 * (iCnt - 1)).ToString).RowHeight = 175
                    xlsWkS.Range("B" + (13 + 13 * (iCnt - 1)).ToString).Value = Me.txtCWarning.Text
                Else
                    For i As Integer = 1 To Spdrow
                        'Set_WksBorder(xlsWkS, CStr(11 + i)) '세부검사 row(spread)만큼  row(excel) insert 작업

                        '--검사명
                        .Row = i
                        .Col = .GetColFromID("tnmd") : Dim Tnmd As String = .Text
                        .Col = .GetColFromID("reftxt") : Dim Reftxt As String = .Text
                        .Col = .GetColFromID("unit") : Dim Unit As String = .Text

                        Set_WksBorder(xlsWkS, CStr(11 + i), Reftxt) '세부검사 row(spread)만큼  row(excel) insert 작업

                        xlsWkS.Range("B" + ((10 + i) + 13 * (iCnt - 1)).ToString).WrapText = True
                        xlsWkS.Range("F" + ((10 + i) + 13 * (iCnt - 1)).ToString).WrapText = True
                        xlsWkS.Range("I" + ((10 + i) + 13 * (iCnt - 1)).ToString).WrapText = True

                        xlsWkS.Range("B" + ((10 + i) + 13 * (iCnt - 1)).ToString).Value = Tnmd '검사명
                        xlsWkS.Range("F" + ((10 + i) + 13 * (iCnt - 1)).ToString).Value = Reftxt '참고치
                        xlsWkS.Range("I" + ((10 + i) + 13 * (iCnt - 1)).ToString).Value = Unit '단위


                    Next

                    xlsWkS.Range("B" + ((11 + Spdrow) + 13 * (iCnt - 1)).ToString).EntireRow.Hidden = True
                    xlsWkS.Range("B" + ((12 + Spdrow) + 13 * (iCnt - 1)).ToString).EntireRow.Hidden = True

                    '-- 12 검체 채취 및 의뢰시 주의사항
                    xlsWkS.Range("B" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString, "I" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString).Merge()
                    xlsWkS.Range("B" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString, "I" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                    xlsWkS.Range("B" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString, "I" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString).Borders.Weight = Excel.XlBorderWeight.xlThin
                    xlsWkS.Range("B" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString, "I" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)

                    xlsWkS.Range("B" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString).RowHeight = 175
                    xlsWkS.Range("B" + ((13 + Spdrow) + 13 * (iCnt - 1)).ToString).Value = Me.txtCWarning.Text
                End If
            End With
            '>


            If ExcelYN Then

                Dim sDir As String = Windows.Forms.Application.StartupPath + "\Excel"
                If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

                'xlsWkSs = xlsWkS
                Dim Dir As String = Windows.Forms.Application.StartupPath + "\Excel\" + Me.txtTnmd.Text + ".xlsx"

                xlsWkS.SaveAs(Dir)


                Process.Start(Dir)

                'Process.Start("EXCEL.EXE", Dir)

            Else

                xlsWkS.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, , , , , , , True)

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'Try

            If ExcelYN = False Then 'pdf 출력

                Try

                    '<< JJH 프로세스 종료(Excel)
                    Dim hWnd As Integer = xlsApp.Hwnd
                    Dim processID As Integer

                    GetWindowThreadProcessId(hWnd, processID) '해당 프로세스 ID찾기
                    Process.GetProcessById(processID).Kill()  '해당 프로세스 kill

                    xlsWkB = Nothing : xlsApp = Nothing : xlsWkS = Nothing

                Catch ex As Exception

                End Try

            Else 'Excel 출력

                If Not xlsWkS Is Nothing Then xlsWkS = Nothing
                '    If Not xlsWkSs Is Nothing Then xlsWkSs = Nothing
                If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing 'xlsWkB = Nothing : xlsWkB.Close(False)
                If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing

            End If


        End Try

    End Sub

    '<< JJH 줄 추가후 테두리 병합, 테두리 작업
    Public Sub Set_WksBorder(ByVal xlsWkS As Excel.Worksheet, ByVal Range As String, Optional ByVal Reftxt As String = "")

        Try

            With xlsWkS
                '-- 셀 추가
                .Range("A" + Range).Rows.Insert()

                '.Range("A" + Range).RowHeight = 33
                .Range("A" + Range).RowHeight = IIf(Reftxt = "", 33, 50)

                '3줄 50

                '--세부검사목록 병합
                .Range("A11", "A" + Range).Merge()
                If Range = "12" Then '-- 첫줄 추가시 병합 해제
                    .Range("B13").MergeArea.UnMerge()
                    .Range("B13:I13").UnMerge()
                End If

                '--병합
                .Range("B" + Range + ":E" + Range).Merge()
                .Range("F" + Range + ":H" + Range).Merge()

                '------- 테두리
                '--검사명
                .Range("B" + Range, "E" + Range).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .Range("B" + Range, "E" + Range).Borders.Weight = Excel.XlBorderWeight.xlThin
                .Range("B" + Range, "E" + Range).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)

                '--참고치
                .Range("F" + Range, "H" + Range).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .Range("F" + Range, "H" + Range).Borders.Weight = Excel.XlBorderWeight.xlThin
                .Range("F" + Range, "H" + Range).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)

                '--단위
                .Range("I" + Range).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                .Range("I" + Range).Borders.Weight = Excel.XlBorderWeight.xlThin
                .Range("I" + Range).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)


                If Range = "13" Then .Range("A12").RowHeight = IIf(Reftxt = "", 33, 50) : .Range("B12", "I12").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter 'HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        If Me.txtTnmd.Text = "" Or Me.txtTCode.Text = "" Then
            MsgBox("조회된 검사항목이 없습니다 확인하여 주십시요!!")
        End If

        sbPrint(True)

    End Sub

End Class

Public Class DA_CDHELP_TEST_NEW
    Private Const msFile As String = "File : FGCDHELP_TEST.vb, Class : DA_CDHELP_TEST" + vbTab

    Public Function fnGet_Help_Info(Optional ByVal rsTestNm As String = "") As DataTable
        Dim sFn As String = " fnGet_Help_Info([String]) As DataTable"


        Dim dbCn As OracleConnection = GetDbConnection()

        Try
            Dim dbDa As OracleDataAdapter
            Dim dbCmd As New OracleCommand

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += "SELECT f6.testcd, MAX(f6.tnmd) tnmd, f6.partcd || f6.slipcd partslip, f6.tordslip,"
            sSql += "       f6.tordcd, f2.slipnmd, f10.tordslipnm"
            sSql += "  FROM lf060m f6, lf021m f2, lf100m f10"
            sSql += " WHERE f6.partcd   = f2.partcd"
            sSql += "   AND f6.slipcd   = f2.slipcd"
            sSql += "   AND f6.tordslip = f10.tordslip"
            sSql += "   AND f6.usdt    <= fn_ack_sysdate"
            sSql += "   AND f6.uedt    >  fn_ack_sysdate"
            sSql += "   AND f2.usdt    <= fn_ack_sysdate"
            sSql += "   AND f2.uedt    >  fn_ack_sysdate"
            sSql += "   AND f10.usdt   <= fn_ack_sysdate"
            sSql += "   AND f10.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.tcdgbn  <> 'C'"
            'sSql += "   AND f6.ordhide <> '1' "

            If rsTestNm <> "" Then
                sSql += "   AND f6.tnmd LIKE '%" + rsTestNm + "%'"
            End If

            sSql += " GROUP BY f6.testcd, f6.partcd, f6.slipcd, f6.tordslip,"
            sSql += "       f6.tordcd, f2.slipnmd, f10.tordslipnm"

            sSql += " UNION "
            sSql += "SELECT f6.testcd, MAX(f6.tnmd) tnmd, f6.partcd || f6.slipcd partslip, f6.tordslip,"
            sSql += "       f6.tordcd, f2.slipnmd, f10.tordslipnm"
            sSql += "  FROM rf060m f6, rf021m f2, lf100m f10"
            sSql += " WHERE f6.partcd   = f2.partcd"
            sSql += "   AND f6.slipcd   = f2.slipcd"
            sSql += "   AND f6.tordslip = f10.tordslip"
            sSql += "   AND f6.usdt    <= fn_ack_sysdate"
            sSql += "   AND f6.uedt    >  fn_ack_sysdate"
            sSql += "   AND f2.usdt    <= fn_ack_sysdate"
            sSql += "   AND f2.uedt    >  fn_ack_sysdate"
            sSql += "   AND f10.usdt   <= fn_ack_sysdate"
            sSql += "   AND f10.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.tcdgbn  <> 'C'"

            If rsTestNm <> "" Then
                sSql += "   AND f6.tnmd LIKE '%" + rsTestNm + "%'"
            End If

            sSql += " GROUP BY f6.testcd, f6.partcd, f6.slipcd, f6.tordslip,"
            sSql += "       f6.tordcd, f2.slipnmd, f10.tordslipnm"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If

            dbCn = Nothing
        End Try

    End Function

    Public Function fnGet_testcd_tord(ByVal rsTOrdCd As String) As String

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbDa As OracleDataAdapter
        Dim dbCmd As New OracleCommand

        Try

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += "SELECT testcd"
            sSql += "  FROM lf060m"
            sSql += " WHERE tordcd  = :tordcd"
            sSql += "   AND usdt   <= fn_ack_sysdate"
            sSql += "   AND uedt   >  fn_ack_sysdate"
            sSql += "   AND tcdgbn <> 'C'"
            sSql += " UNION "
            sSql += "SELECT testcd"
            sSql += "  FROM rf060m"
            sSql += " WHERE tordcd  = :tordcd"
            sSql += "   AND usdt   <= fn_ack_sysdate"
            sSql += "   AND uedt   >  fn_ack_sysdate"
            sSql += "   AND tcdgbn <> 'C'"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("tordcd", rsTOrdCd)
                dbCmd.Parameters.Add("tordcd", rsTOrdCd)

            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("testcd").ToString.Trim
            Else
                Return ""
            End If
        Catch ex As Exception

            Return ""
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If
            dbCn = Nothing
        End Try

    End Function

    Public Function fnGet_spc_info(ByVal rsTestCd As String) As DataTable

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbDa As OracleDataAdapter
        Dim dbCmd As New OracleCommand

        Try

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += "SELECT DISTINCT"
            sSql += "       f6.spccd, f3.spcnmd,"
            sSql += "       CASE WHEN f6.spccd = f6.dspccd1 THEN 1"
            sSql += "            WHEN f6.spccd = f6.dspccd2 THEN 2"
            sSql += "            WHEN f6.spccd = f6.dspccd3 THEN 3"
            sSql += "            ELSE  3"
            sSql += "       END sort1"
            sSql += "  FROM lf060m f6, lf030m f3"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.spccd   = f3.spccd"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.tcdgbn <> 'C'"
            sSql += "   AND f3.usdt   <= fn_ack_sysdate"
            sSql += "   AND f3.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.ordhide <> '1' "
            sSql += " UNION "
            sSql += "SELECT DISTINCT"
            sSql += "       f6.spccd, f3.spcnmd,"
            sSql += "       CASE WHEN f6.spccd = f6.dspccd1 THEN 1"
            sSql += "            WHEN f6.spccd = f6.dspccd2 THEN 2"
            sSql += "            WHEN f6.spccd = f6.dspccd3 THEN 3"
            sSql += "            ELSE  3"
            sSql += "       END sort1"
            sSql += "  FROM rf060m f6, lf030m f3"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.spccd   = f3.spccd"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.tcdgbn <> 'C'"
            sSql += "   AND f3.usdt   <= fn_ack_sysdate"
            sSql += "   AND f3.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.ordhide <> '1' "

            sSql += " ORDER BY sort1, spccd"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("testcd", rsTestCd)

            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception

            Return New DataTable

        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If
            dbCn = Nothing
        End Try


    End Function

    Public Function fnGet_testspc_info(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbDa As OracleDataAdapter
        Dim dbCmd As New OracleCommand

        Try

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += "SELECT DISTINCT"
            sSql += "       fn_ack_date_str(f6.usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
            sSql += "       f6.tnmd, f6.tubecd, f4.tubenmd, f21.slipnmd,"
            sSql += "       (SELECT exlabnmd FROM lf050m WHERE exlabcd = f6.exlabcd) exlabyn,"
            sSql += "       f6.rrptst, f2.telno,"
            sSql += "       f6.exeday, f6.cwarning,"
            sSql += "       CASE WHEN f6.emergbn IN ('1', '3') THEN '1' ELSE '' END ergbn1,"
            sSql += "       CASE WHEN f6.emergbn IN ('2', '3') THEN '1' ELSE '' END ergbn2,"
            sSql += "       f10.tordslipnm, f6.descref,"
            sSql += "       fn_ack_get_ref_nmbp_list(f6.testcd, f6.spccd) reftest,"
            sSql += "       f6.tatyn, f6.prptmi, f6.frptmi, f6.erptmi, f6.perrptmi,f6.ferrptmi, minspcvol,f2.partnmd , f6.enforcement , f6.request , f6.cowarning"
            sSql += "       ,f6.rstunit ,f6.spcunit"
            sSql += "  FROM lf060m f6, lf020m f2, lf021m f21,"
            sSql += "       lf040m f4, lf100m f10"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.spccd   = :spccd"
            sSql += "   AND f6.partcd  = f2.partcd"
            sSql += "   AND f6.partcd  = f21.partcd"
            sSql += "   AND f6.slipcd  = f21.slipcd"
            sSql += "   AND f6.tubecd  = f4.tubecd"
            sSql += "   AND f6.tordslip = f10.tordslip"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.tcdgbn <> 'C'"
            sSql += "   AND f2.usdt   <= fn_ack_sysdate"
            sSql += "   AND f2.uedt   >  fn_ack_sysdate"
            sSql += "   AND f21.usdt  <= fn_ack_sysdate"
            sSql += "   AND f21.uedt  >  fn_ack_sysdate"
            sSql += "   AND f4.usdt   <= fn_ack_sysdate"
            sSql += "   AND f4.uedt   >  fn_ack_sysdate"
            sSql += "   AND f10.usdt  <= fn_ack_sysdate"
            sSql += "   AND f10.uedt  >  fn_ack_sysdate"
            sSql += " UNION  "
            sSql += "SELECT DISTINCT"
            sSql += "       fn_ack_date_str(f6.usdt, 'yyyy-mm-dd hh24:mi:ss') usdt,"
            sSql += "       f6.tnmd, f6.tubecd, f4.tubenmd, f21.slipnmd,"
            sSql += "       (SELECT exlabnmd FROM lf050m WHERE exlabcd = f6.exlabcd) exlabyn,"
            sSql += "       f6.rrptst, f2.telno,"
            sSql += "       f6.exeday, f6.cwarning,"
            sSql += "       CASE WHEN f6.emergbn IN ('1', '3') THEN '1' ELSE '' END ergbn1,"
            sSql += "       CASE WHEN f6.emergbn IN ('2', '3') THEN '1' ELSE '' END ergbn2,"
            sSql += "       f10.tordslipnm, f6.descref,"
            sSql += "       fn_ack_get_ref_nmbp_list(f6.testcd, f6.spccd) reftest,"
            sSql += "       f6.tatyn, f6.prptmi, f6.frptmi, f6.erptmi, f6.perrptmi,f6.ferrptmi,minspcvol,f2.partnmd , '' , '' , ''"
            sSql += "       ,'',''"
            sSql += "  FROM rf060m f6, rf020m f2, rf021m f21,"
            sSql += "       lf040m f4, lf100m f10"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.spccd   = :spccd"
            sSql += "   AND f6.partcd  = f2.partcd"
            sSql += "   AND f6.partcd  = f21.partcd"
            sSql += "   AND f6.slipcd  = f21.slipcd"
            sSql += "   AND f6.tubecd  = f4.tubecd"
            sSql += "   AND f6.tordslip = f10.tordslip"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += "   AND f6.tcdgbn <> 'C'"
            sSql += "   AND f2.usdt   <= fn_ack_sysdate"
            sSql += "   AND f2.uedt   >  fn_ack_sysdate"
            sSql += "   AND f21.usdt  <= fn_ack_sysdate"
            sSql += "   AND f21.uedt  >  fn_ack_sysdate"
            sSql += "   AND f4.usdt   <= fn_ack_sysdate"
            sSql += "   AND f4.uedt   >  fn_ack_sysdate"
            sSql += "   AND f10.usdt  <= fn_ack_sysdate"
            sSql += "   AND f10.uedt  >  fn_ack_sysdate"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)
                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)

            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception

            Return New DataTable
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If
        End Try


    End Function

    Public Function fnGet_testspc_ref(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String) As DataTable

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbDa As OracleDataAdapter
        Dim dbCmd As New OracleCommand

        Try

            Dim dt As New DataTable
            Dim sSql As String = ""

            'sSql += "SELECT DISTINCT"
            'sSql += "       f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f6.tnmd, 1 sort1, f6.dispseqo sort2, f6.rstunit"
            'sSql += "  FROM lf060m f6"
            ''sSql += " WHERE f6.testcd  LIKE :testcd || '%'"
            'sSql += " WHERE f6.testcd  = :testcd"
            'sSql += "   AND f6.spccd   = :spccd"
            'sSql += "   AND f6.usdt    = :usdt"
            'sSql += " UNION "
            'sSql += "SELECT f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f6.tnmd, 2 sort2, f6.dispseqo sort2, f6.rstunit"
            'sSql += "  FROM lf067m f62, lf060m f6"
            'sSql += " WHERE f62.tclscd = :testcd"
            'sSql += "   AND f62.tspccd = :spccd"
            ''sSql += "   AND f6.testcd  LIKE f62.testcd || '%'"
            'sSql += "   AND f6.testcd  = f62.testcd "
            'sSql += "   AND f6.spccd  = f62.spccd"
            'sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            'sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            'sSql += " UNION "
            'sSql += "SELECT DISTINCT"
            'sSql += "       f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f6.tnmd, 1 sort1, f6.dispseqo sort2, f6.rstunit"
            'sSql += "  FROM rf060m f6"
            'sSql += " WHERE f6.testcd  LIKE :testcd || '%'"
            'sSql += "   AND f6.spccd   = :spccd"
            'sSql += "   AND f6.usdt    = :usdt"
            'sSql += " UNION "
            'sSql += "SELECT f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f6.tnmd, 2 sort2, f6.dispseqo sort2, f6.rstunit"
            'sSql += "  FROM rf062m f62, rf060m f6"
            'sSql += " WHERE f62.tclscd = :testcd"
            'sSql += "   AND f62.tspccd = :spccd"
            'sSql += "   AND f6.testcd  LIKE f62.testcd || '%'"
            'sSql += "   AND f6.spccd  = f62.spccd"
            'sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            'sSql += "   AND f6.uedt   >  fn_ack_sysdate"

            sSql += " SELECT DISTINCT "
            sSql += "        f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd,  f6.edicd, f6.tnmd, 1 sort1, f6.dispseqo sort2, 0 sort3, f6.rstunit"
            sSql += "   FROM lf060m f6 "
            sSql += "  WHERE f6.testcd = :testcd "
            sSql += "    AND f6.spccd  = :spccd  "
            sSql += "    AND f6.uedt  >= fn_ack_sysdate() "
            sSql += " UNION "
            sSql += " SELECT f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f62.tnmd, 2 sort2, f6.dispseqo sort2, f62.seq sort3, f6.rstunit "
            sSql += "   FROM lf068m f62 "
            sSql += "        LEFT OUTER JOIN lf060m f6 ON f6.testcd LIKE f62.testcd || '%'"
            sSql += "                                 AND f6.testcd  = f62.testcd "
            sSql += "                                 AND f6.spccd   = f62.spccd "
            sSql += "                                 AND f6.usdt   <= fn_ack_sysdate() "
            sSql += "                                 AND f6.uedt   >= fn_ack_sysdate() "
            sSql += "  WHERE f62.tclscd = :tclscd "
            sSql += "    AND f62.tspccd = :tspccd "

            sSql += " ORDER BY sort1, sort2, testcd, sort3"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)
                'dbCmd.Parameters.Add("usdt", rsUsDt)

                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)

                'dbCmd.Parameters.Add("testcd", rsTestCd)
                'dbCmd.Parameters.Add("spccd", rsSpcCd)
                'dbCmd.Parameters.Add("usdt", rsUsDt)

                'dbCmd.Parameters.Add("testcd", rsTestCd)
                'dbCmd.Parameters.Add("spccd", rsSpcCd)
            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception

            Return New DataTable
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If
        End Try


    End Function

    Public Function fnGet_test_info(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbDa As OracleDataAdapter
        Dim dbCmd As New OracleCommand

        Try

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += "SELECT DISTINCT"
            sSql += "       f64.infogbn, f64.testinfo, MAX(f6.tnmd) tnmd,"
            sSql += "       CASE WHEN f64.spccd = '----' THEN 1 ELSE 2 END sort1"
            sSql += "  FROM lf060m f6"
            sSql += "       LEFT OUTER JOIN"
            sSql += "            lf064m f64 ON (f6.testcd = f64.testcd AND f64.spccd IN ('" + "".PadLeft(PRG_CONST.Len_SpcCd, "-") + "', :spccd))"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += " GROUP BY f64.infogbn, f64.testinfo, f64.spccd"
            sSql += " UNION "
            sSql += "SELECT DISTINCT"
            sSql += "       f64.infogbn, f64.testinfo, MAX(f6.tnmd) tnmd,"
            sSql += "       CASE WHEN f64.spccd = '" + "".PadLeft(PRG_CONST.Len_SpcCd, "-") + "' THEN 1 ELSE 2 END sort1"
            sSql += "  FROM rf060m f6"
            sSql += "       LEFT OUTER JOIN"
            sSql += "            rf064m f64 ON (f6.testcd = f64.testcd AND f64.spccd IN ('" + "".PadLeft(PRG_CONST.Len_SpcCd, "-") + "', :spccd))"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += " GROUP BY f64.infogbn, f64.testinfo, f64.spccd"

            sSql += " ORDER BY infogbn, sort1"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("spccd", rsSpcCd)
                dbCmd.Parameters.Add("testcd", rsTestCd)

                dbCmd.Parameters.Add("spccd", rsSpcCd)
                dbCmd.Parameters.Add("testcd", rsTestCd)

            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception

            Return New DataTable
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If
        End Try


    End Function

    Public Function fnGet_test_ref(ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsUsDt As String) As DataTable

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbDa As OracleDataAdapter
        Dim dbCmd As New OracleCommand

        Try

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += "SELECT DISTINCT"
            sSql += "       f61.ageymd, f61.sage, f61.sages, f61.eage, f61.eages,"
            sSql += "       f61.reflm, f61.reflms, f61.refhm, f61.refhms,"
            sSql += "       f61.reflf, f61.reflfs, f61.refhf, f61.refhfs,"
            sSql += "       f61.reflt, f6.refgbn, f6.descref, f61.refseq, f6.tnmd"
            sSql += "  FROM lf060m f6, lf061m f61"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.spccd   = :spccd"
            sSql += "   AND f6.usdt    = :usdt"
            sSql += "   AND f6.testcd  = f61.testcd"
            sSql += "   AND f6.spccd   = f61.spccd"
            sSql += "   AND f6.usdt    = f61.usdt"
            sSql += " UNION "
            sSql += "SELECT DISTINCT"
            sSql += "       f61.ageymd, f61.sage, f61.sages, f61.eage, f61.eages,"
            sSql += "       f61.reflm, f61.reflms, f61.refhm, f61.refhms,"
            sSql += "       f61.reflf, f61.reflfs, f61.refhf, f61.refhfs,"
            sSql += "       f61.reflt, f6.refgbn, f6.descref, f61.refseq, f6.tnmd"
            sSql += "  FROM rf060m f6, rf061m f61"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.spccd   = :spccd"
            sSql += "   AND f6.usdt    = :usdt"
            sSql += "   AND f6.testcd  = f61.testcd"
            sSql += "   AND f6.spccd   = f61.spccd"
            sSql += "   AND f6.usdt    = f61.usdt"

            sSql += " ORDER BY  refseq"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()

                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)
                'dbCmd.Parameters.Add("usdt", rsUsDt)

                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)
                'dbCmd.Parameters.Add("usdt", rsUsDt)
            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception

            Return New DataTable
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If
        End Try


    End Function

    Public Function fnGet_tube_img(ByVal rsTubeCd) As Byte()

        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbDa As OracleDataAdapter
        Dim dbCmd As New OracleCommand

        Try

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += "SELECT filelen, filebin"
            sSql += "  FROM lf041m"
            sSql += " WHERE tubecd = :tubecd"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                .Parameters.Clear()

                .Parameters.Clear()
                .Parameters.Add("tubecd", OracleDbType.Varchar2, rsTubeCd.Length).Value = rsTubeCd

            End With

            Dim a_btReturn() As Byte
            dbCmd.InitialLONGFetchSize = -1
            Dim dbDr As OracleDataReader = dbCmd.ExecuteReader(CommandBehavior.SequentialAccess)

            Do While dbDr.Read()

                Dim iStartIndex As Integer = 0
                Dim lngReturn As Long = 0

                Dim iBufferSize As Integer = 0

                iBufferSize = Convert.ToInt32(dbDr.GetValue(0).ToString)

                Dim a_btBuffer(iBufferSize - 1) As Byte
                ReDim a_btBuffer(iBufferSize - 1)

                iStartIndex = 0
                lngReturn = dbDr.GetBytes(1, iStartIndex, a_btBuffer, 0, iBufferSize)

                Do While lngReturn = iBufferSize
                    fnCopyToBytes(a_btBuffer, a_btReturn)


                    ReDim a_btBuffer(iBufferSize - 1)

                    iStartIndex += iBufferSize
                    lngReturn = dbDr.GetBytes(1, iStartIndex, a_btReturn, 0, iBufferSize)
                Loop
            Loop

            dbDr.Close()

            Return a_btReturn

        Catch ex As Exception

            Return Nothing
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If
        End Try


    End Function

    Private Shared Function fnCopyToBytes(ByVal r_a_btFrom As Byte(), ByRef r_a_btTo As Byte()) As Boolean

        Try
            Dim iIndexDest As Integer = 0
            Dim iLength As Integer = 0

            If r_a_btTo Is Nothing Then
                iIndexDest = 0
            Else
                iIndexDest = r_a_btTo.Length
            End If

            iLength = r_a_btFrom.Length

            ReDim Preserve r_a_btTo(iIndexDest + iLength - 1)

            Array.ConstrainedCopy(r_a_btFrom, 0, r_a_btTo, iIndexDest, iLength)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Function
End Class

Public Class PRT_INFO_NEW
    Public PrtGbn As String = ""  'A=검사기본정보(1), B=검사기본정보(2), C=검사기본정보(3), D=세부검사목록, E,F,G,H,I:검사상세정보, J:임사적의릐
    Public OrdCd As String = ""
    Public TestCd As String = ""
    Public TestNm As String = ""
    Public Spcnm As String = ""
    Public TubeNm As String = ""
    Public UsDate As String = ""
    Public ExLab As String = ""
    Public PartSlip As String = ""
    Public OrdSlip As String = ""
    Public Rrptst As String = ""
    Public TelNo As String = ""
    Public ExeDay As String = ""
    Public ErGbn As String = ""

    '-- 세부검사 목록
    Public TestLIst As String = ""

    '-- 검사상세 정보
    Public RefTxt As String = ""
    Public Info1 As String = ""       '-- 검사법
    Public Info2 As String = ""       '-- 주의내용
    Public Info3 As String = ""       '-- 임상적의의
    Public CWarning As String = ""    '-- 채혈시 주의사항

    '2019-12-11
    Public PartCd As String = "" '부서명
    Public AgreeMent As String = "" '검사의뢰서/동의서
    Public Execution As String = "" '시행처
    Public TATME As String = "" '응급중간보고
    Public TATM As String = "" '일반중간보고
    Public TATFE As String = "" '응급최종보고
    Public TATF As String = "" '일반최종보고
    Public Vol As String = "" '검체량
    Public ETC As String = "" '검사의뢰및 채취시 주의사항

End Class

Public Class TESTINFO_PRINT_NEW
    Private Const msFile As String = "File : FGCDHELP_TEST.vb, Class : CDHELP" + vbTab

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Public mbLandscape As Boolean = False
    Public m_PrtInfo As PRT_INFO_NEW

    Public Sub sbPrint_Preview()
        Dim sFn As String = "Sub sbPrint_Preview()"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = mbLandscape
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_검사정보"

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

    Public Sub sbPrint()
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = mbLandscape

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog


            If prtBPress = DialogResult.OK Then

                prtR.DocumentName = "ACK_검사정보"

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

    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        msgWidth = e.PageBounds.Width - 60
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        Dim sgPosY As Single = msgTop

        sgPosY = fnPrt_Body_1(e, sgPosY)
        sgPosY = fnPrt_Body_2(e, sgPosY)
        sgPosY = fnPrt_Body_3(e, sgPosY)

        sbPrt_Body_4(e, sgPosY)


        e.HasMorePages = False

    End Sub

    Public Overridable Function fnPrt_Body_1(ByVal e As PrintPageEventArgs, ByVal r_sgPosY As Single) As Single

        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim rect As Drawing.RectangleF
        Dim sTmp As String = ""

        Dim sgBoxX(0 To 9) As Single
        Dim sgBoxY(0 To 5) As Single
        Dim sgPrt As Single = fnt_Body.GetHeight(e.Graphics) * 1.7

        sgBoxX(0) = msgLeft
        sgBoxX(1) = sgBoxX(0) + 80
        sgBoxX(2) = sgBoxX(1) + 90
        sgBoxX(3) = sgBoxX(2) + 80
        sgBoxX(4) = sgBoxX(3) + 90
        sgBoxX(5) = sgBoxX(4) + 80
        sgBoxX(6) = sgBoxX(5) + 0
        sgBoxX(7) = sgBoxX(6) + 140
        sgBoxX(8) = sgBoxX(7) + 80
        sgBoxX(9) = msgWidth - 5

        sgBoxY(0) = r_sgPosY + sgPrt + 5
        sgBoxY(1) = sgBoxY(0) + sgPrt
        sgBoxY(2) = sgBoxY(1) + sgPrt
        sgBoxY(3) = sgBoxY(2) + sgPrt
        sgBoxY(4) = sgBoxY(3) + sgPrt
        sgBoxY(5) = sgBoxY(4) + sgPrt

        '-- 세로
        For ix As Integer = 0 To 9

            Select Case ix
                Case 0, 1, 9
                    e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix), sgBoxY(0), sgBoxX(ix), sgBoxY(5))
                    If ix <> 1 Then e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix) + 1, sgBoxY(0), sgBoxX(ix) + 1, sgBoxY(5)) '두줄씩 긋는 코드
                Case 2, 3
                    e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix), sgBoxY(0), sgBoxX(ix), sgBoxY(1))
                Case 4
                    e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix), sgBoxY(0), sgBoxX(ix), sgBoxY(4))
                Case 5, 7, 8
                    e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix), sgBoxY(2), sgBoxX(ix), sgBoxY(4))
                Case 6
                    e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix), sgBoxY(0), sgBoxX(ix), sgBoxY(2))
            End Select
        Next

        '-- box
        For ix As Integer = 0 To 5
            e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(0), sgBoxY(ix), sgBoxX(9), sgBoxY(ix))

            If ix = 0 Or ix = 5 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(0), sgBoxY(ix) + 1, sgBoxX(9), sgBoxY(ix) + 1) '두줄씩 긋는 코드
            End If
        Next

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        rect = New Drawing.RectangleF(msgLeft - 5, r_sgPosY, sgBoxX(9) - sgBoxX(0), sgPrt)
        e.Graphics.DrawString(">> 검사 기본정보", fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft - 5, msgTop + sgPrt + 0, msgWidth, r_sgPosY + sgPrt)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft - 5, msgTop + sgPrt + 2, msgWidth, r_sgPosY + sgPrt + 2)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(0), sgBoxX(1) - sgBoxX(0), sgPrt)
        e.Graphics.DrawString("처방코드", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(2), sgBoxY(0), sgBoxX(3) - sgBoxX(2), sgPrt)
        e.Graphics.DrawString("검사코드", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(1), sgBoxX(1) - sgBoxX(0), sgPrt)
        e.Graphics.DrawString("검 체 명", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(2), sgBoxX(1) - sgBoxX(0), sgPrt)
        e.Graphics.DrawString("처방슬립", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(3), sgBoxX(1) - sgBoxX(0), sgPrt)
        e.Graphics.DrawString("검사분류", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(4), sgBoxX(1) - sgBoxX(0), sgPrt)
        e.Graphics.DrawString("실시요일", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(4), sgBoxY(0), sgBoxX(6) - sgBoxX(4), sgPrt)
        e.Graphics.DrawString("검 사 명", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(4), sgBoxY(1), sgBoxX(6) - sgBoxX(4), sgPrt)
        e.Graphics.DrawString("용 기 명", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(4), sgBoxY(2), sgBoxX(5) - sgBoxX(4), sgPrt)
        e.Graphics.DrawString("적용일자", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(4), sgBoxY(3), sgBoxX(5) - sgBoxX(4), sgPrt)
        e.Graphics.DrawString("소 요 일", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(7), sgBoxY(2), sgBoxX(8) - sgBoxX(7), sgPrt)
        e.Graphics.DrawString("위탁기관", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(7), sgBoxY(3), sgBoxX(8) - sgBoxX(7), sgPrt)
        e.Graphics.DrawString("내선번호", fnt_Body, Drawing.Brushes.Black, rect, sf_c)


        '-- data
        rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(0), sgBoxX(2) - sgBoxX(1), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.OrdCd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(3), sgBoxY(0), sgBoxX(4) - sgBoxX(3), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.TestCd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(1), sgBoxX(4) - sgBoxX(1), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.Spcnm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(2), sgBoxX(4) - sgBoxX(1), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.OrdSlip, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(3), sgBoxX(4) - sgBoxX(1), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.PartSlip, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        sTmp = ""
        For ix As Integer = 0 To m_PrtInfo.ExeDay.Length - 1
            If m_PrtInfo.ExeDay.Substring(ix, 1) = "1" Then
                Select Case ix
                    Case 0 : sTmp += "[√]월 "
                    Case 1 : sTmp += "[√]화 "
                    Case 2 : sTmp += "[√]수 "
                    Case 3 : sTmp += "[√]목 "
                    Case 4 : sTmp += "[√]금 "
                    Case 5 : sTmp += "[√]토 "
                    Case 6 : sTmp += "[√]일 "
                End Select
            Else '
                Select Case ix
                    Case 0 : sTmp += "[ ]월 "
                    Case 1 : sTmp += "[ ]화 "
                    Case 2 : sTmp += "[ ]수 "
                    Case 3 : sTmp += "[ ]목 "
                    Case 4 : sTmp += "[ ]금 "
                    Case 5 : sTmp += "[ ]토 "
                    Case 6 : sTmp += "[ ]일 "
                End Select
            End If
        Next

        If m_PrtInfo.ErGbn.Substring(0, 1) = "1" Then
            sTmp += Space(6) + "[√]당일검사 "
        Else
            sTmp += Space(6) + "[ ]당일검사 "
        End If

        If m_PrtInfo.ErGbn.Substring(1, 1) = "1" Then
            sTmp += Space(2) + "[√]응급검사 "
        Else
            sTmp += Space(2) + "[ ]응급검사 "
        End If

        rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(4), sgBoxX(9) - sgBoxX(1), sgPrt)
        e.Graphics.DrawString(" " + sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)


        rect = New Drawing.RectangleF(sgBoxX(6), sgBoxY(0), sgBoxX(9) - sgBoxX(6), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.TestNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(6), sgBoxY(1), sgBoxX(9) - sgBoxX(6), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.TubeNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(5), sgBoxY(2), sgBoxX(7) - sgBoxX(5), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.UsDate.Substring(0, 16), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(5), sgBoxY(3), sgBoxX(7) - sgBoxX(5), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.Rrptst, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(8), sgBoxY(2), sgBoxX(9) - sgBoxX(8), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.ExLab, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(8), sgBoxY(3), sgBoxX(9) - sgBoxX(8), sgPrt)
        e.Graphics.DrawString(" " + m_PrtInfo.TelNo, fnt_Body, Drawing.Brushes.Black, rect, sf_l)


        Return sgBoxY(5) + sgPrt

    End Function

    Public Overridable Function fnPrt_Body_2(ByVal e As PrintPageEventArgs, ByVal r_sgPosY As Single) As Single

        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim rect As Drawing.RectangleF

        Dim sgBoxX(0 To 7) As Single
        Dim sgBoxY() As Single
        Dim sgPrt As Single = fnt_Body.GetHeight(e.Graphics) * 1.7

        Dim sBuf() As String = m_PrtInfo.TestLIst.Split("|"c)

        sgBoxX(0) = msgLeft
        sgBoxX(1) = sgBoxX(0) + 50
        sgBoxX(2) = sgBoxX(1) + 50
        sgBoxX(3) = sgBoxX(2) + 80
        sgBoxX(4) = sgBoxX(3) + 80
        sgBoxX(5) = sgBoxX(4) + 80
        sgBoxX(6) = sgBoxX(5) + 80
        sgBoxX(7) = msgWidth - 5

        ReDim sgBoxY(0 To sBuf.Length)
        sgBoxY(0) = r_sgPosY + sgPrt + 5

        For ix As Integer = 1 To sgBoxY.Length - 1
            sgBoxY(ix) = sgBoxY(ix - 1) + sgPrt
        Next

        '-- 세로
        For ix As Integer = 0 To sgBoxX.Length - 1

            e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix), sgBoxY(0), sgBoxX(ix), sgBoxY(sgBoxY.Length - 1))
            If ix = 0 Or ix = sgBoxX.Length - 1 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix) + 1, sgBoxY(0), sgBoxX(ix) + 1, sgBoxY(sgBoxY.Length - 1))
            End If
        Next

        '-- box
        For ix As Integer = 0 To sgBoxY.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(0), sgBoxY(ix), sgBoxX(7), sgBoxY(ix))

            If ix = 0 Or ix = sgBoxY.Length - 1 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(0), sgBoxY(ix) + 1, sgBoxX(7), sgBoxY(ix) + 1)
            End If
        Next

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        rect = New Drawing.RectangleF(msgLeft - 5, r_sgPosY, sgBoxX(6) - sgBoxX(0), sgPrt)
        e.Graphics.DrawString(">> 세부검사 목록", fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft - 5, r_sgPosY + sgPrt + 0, msgWidth, r_sgPosY + sgPrt)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft - 5, r_sgPosY + sgPrt + 2, msgWidth, r_sgPosY + sgPrt + 2)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(0), sgBoxX(1) - sgBoxX(0), sgPrt)
        e.Graphics.DrawString("No.", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(0), sgBoxX(2) - sgBoxX(1), sgPrt)
        e.Graphics.DrawString("구분", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(2), sgBoxY(0), sgBoxX(3) - sgBoxX(2), sgPrt)
        e.Graphics.DrawString("처방코드", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(3), sgBoxY(0), sgBoxX(4) - sgBoxX(3), sgPrt)
        e.Graphics.DrawString("검사코드", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(4), sgBoxY(0), sgBoxX(5) - sgBoxX(4), sgPrt)
        e.Graphics.DrawString("수가코드", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(5), sgBoxY(0), sgBoxX(6) - sgBoxX(5), sgPrt)
        e.Graphics.DrawString("보험코드", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgBoxX(6), sgBoxY(0), sgBoxX(7) - sgBoxX(6), sgPrt)
        e.Graphics.DrawString("검사명", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        For ix1 As Integer = 1 To sBuf.Length - 1
            rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(ix1), sgBoxX(1) - sgBoxX(0), sgPrt)
            e.Graphics.DrawString(ix1.ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            For ix2 As Integer = 1 To sBuf(ix1 - 1).Split("^"c).Length - 1
                rect = New Drawing.RectangleF(sgBoxX(ix2), sgBoxY(ix1), sgBoxX(ix2 + 1) - sgBoxX(ix2), sgPrt)

                If ix2 = 1 Then
                    e.Graphics.DrawString(sBuf(ix1 - 1).Split("^"c)(ix2 - 1), fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                Else
                    e.Graphics.DrawString(" " + sBuf(ix1 - 1).Split("^"c)(ix2 - 1), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
                End If
            Next
        Next

        Return sgBoxY(sgBoxY.Length - 1) + sgPrt

    End Function

    Public Overridable Function fnPrt_Body_3(ByVal e As PrintPageEventArgs, ByVal r_sgPosY As Single) As Single

        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim rect As Drawing.RectangleF

        Dim sgBoxX(0 To 2) As Single
        Dim sgBoxY(0 To 4) As Single
        Dim sgPrt As Single = fnt_Body.GetHeight(e.Graphics) * 1.7

        Dim sBuf() As String

        sgBoxX(0) = msgLeft
        sgBoxX(1) = sgBoxX(0) + 160
        sgBoxX(2) = msgWidth - 5

        sgBoxY(0) = r_sgPosY + sgPrt + 5

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        rect = New Drawing.RectangleF(msgLeft - 5, r_sgPosY, msgWidth, sgPrt)
        e.Graphics.DrawString(">> 검사 상세정보", fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft - 5, r_sgPosY + sgPrt + 0, msgWidth, r_sgPosY + sgPrt)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft - 5, r_sgPosY + sgPrt + 3, msgWidth, r_sgPosY + sgPrt + 3)

        sgPrt = fnt_Body.GetHeight(e.Graphics) * 1.3

        '-- 참고치
        sBuf = m_PrtInfo.RefTxt.Split(vbCr)
        If sBuf.Length > 0 Then
            For ix As Integer = 0 To sBuf.Length - 1
                rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(0) + 10 + sgPrt * ix, sgBoxX(2) - sgBoxX(1), sgPrt)
                e.Graphics.DrawString(sBuf(ix).Replace(vbLf, ""), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next
            If sBuf.Length < 3 Then
                sgBoxY(1) = sgBoxY(0) + sgPrt * 3 + 10
            Else
                sgBoxY(1) = sgBoxY(0) + sgPrt * sBuf.Length + 10
            End If
        Else
            sgBoxY(1) = sgBoxY(0) + sgPrt * 3
        End If

        '-- 검사법
        sBuf = m_PrtInfo.Info1.Split(vbCr)
        If sBuf.Length > 0 Then
            For ix As Integer = 0 To sBuf.Length - 1
                rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(1) + 10 + sgPrt * ix, sgBoxX(2) - sgBoxX(1), sgPrt)
                e.Graphics.DrawString(sBuf(ix).Replace(vbLf, ""), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next
            If sBuf.Length < 2 Then
                sgBoxY(2) = sgBoxY(1) + sgPrt * 2 + 10
            Else
                sgBoxY(2) = sgBoxY(1) + sgPrt * sBuf.Length + 10
            End If
        Else
            sgBoxY(2) = sgBoxY(1) + sgPrt * 2
        End If

        '-- 주의내용
        sBuf = m_PrtInfo.Info2.Split(vbCr)
        If sBuf.Length > 0 Then
            For ix As Integer = 0 To sBuf.Length - 1
                rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(2) + 10 + sgPrt * ix, sgBoxX(2) - sgBoxX(1), sgPrt)
                e.Graphics.DrawString(sBuf(ix).Replace(vbLf, ""), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next
            If sBuf.Length < 2 Then
                sgBoxY(3) = sgBoxY(2) + sgPrt * 2 + 10
            Else
                sgBoxY(3) = sgBoxY(2) + sgPrt * sBuf.Length + 10
            End If
        Else
            sgBoxY(3) = sgBoxY(2) + sgPrt * 2
        End If

        '-- 검체 체취시 주의사항
        sBuf = m_PrtInfo.CWarning.Split(vbCr)
        If sBuf.Length > 0 Then
            For ix As Integer = 0 To sBuf.Length - 1
                rect = New Drawing.RectangleF(sgBoxX(1), sgBoxY(3) + 10 + sgPrt * ix, sgBoxX(2) - sgBoxX(1), sgPrt)
                e.Graphics.DrawString(sBuf(ix).Replace(vbLf, ""), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next
            If sBuf.Length < 2 Then
                sgBoxY(4) = sgBoxY(3) + sgPrt * 2 + 10
            Else
                sgBoxY(4) = sgBoxY(3) + sgPrt * sBuf.Length + 10
            End If
        Else
            sgBoxY(4) = sgBoxY(3) + sgPrt * 2
        End If

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(0), sgBoxX(1) - sgBoxX(0), sgBoxY(1) - sgBoxY(0))
        e.Graphics.DrawString(" 참 고 치", fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(1), sgBoxX(1) - sgBoxX(0), sgBoxY(2) - sgBoxY(1))
        e.Graphics.DrawString(" 검사법/" + vbCrLf + " 참조검사", fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(2), sgBoxX(1) - sgBoxX(0), sgBoxY(3) - sgBoxY(2))
        e.Graphics.DrawString(" 주의내용", fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgBoxX(0), sgBoxY(3), sgBoxX(1) - sgBoxX(0), sgBoxY(4) - sgBoxY(3))
        e.Graphics.DrawString(" 검체 채취시" + vbCrLf + " 주의사항   ", fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 세로
        For ix As Integer = 0 To sgBoxX.Length - 1

            e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix), sgBoxY(0), sgBoxX(ix), sgBoxY(sgBoxY.Length - 1))
            If ix = 0 Or ix = sgBoxX.Length - 1 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix) + 1, sgBoxY(0), sgBoxX(ix) + 1, sgBoxY(4))
            End If
        Next

        '-- box
        For ix As Integer = 0 To sgBoxY.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(0), sgBoxY(ix), sgBoxX(2), sgBoxY(ix))

            If ix = 0 Or ix = sgBoxY.Length - 1 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(0), sgBoxY(ix) + 1, sgBoxX(2), sgBoxY(ix) + 1)
            End If
        Next

        Return sgBoxY(4) + sgPrt

    End Function

    Public Overridable Sub sbPrt_Body_4(ByVal e As PrintPageEventArgs, ByVal r_sgPosY As Single)

        If m_PrtInfo.Info3.Replace(vbCrLf, "").Trim = "" Then Return

        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim rect As Drawing.RectangleF

        Dim sgPrt As Single = fnt_Body.GetHeight(e.Graphics) * 1.7
        Dim sgTop As Single = r_sgPosY + sgPrt + 10

        Dim sBuf() As String

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        rect = New Drawing.RectangleF(msgLeft - 5, r_sgPosY, msgWidth, sgPrt)
        e.Graphics.DrawString(">> 임상적의의", fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft - 5, r_sgPosY + sgPrt + 0, msgWidth, r_sgPosY + sgPrt)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft - 5, r_sgPosY + sgPrt + 2, msgWidth, r_sgPosY + sgPrt + 2)

        sgPrt = fnt_Body.GetHeight(e.Graphics) * 1.3

        '-- 임상적의의
        sBuf = m_PrtInfo.Info3.Split(vbCr)
        For ix As Integer = 0 To sBuf.Length - 1
            rect = New Drawing.RectangleF(msgLeft, sgTop + sgPrt * ix, msgWidth, sgPrt)
            e.Graphics.DrawString(sBuf(ix).Replace(vbLf, ""), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
        Next


    End Sub

End Class