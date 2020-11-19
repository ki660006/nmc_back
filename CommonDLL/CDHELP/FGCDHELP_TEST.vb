Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.IO
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Public Class FGCDHELP_TEST
    Private Const msFile As String = "File : CDHELP.vb, Class : FGCDHELP_TEST" + vbTab
    Private moCtrlcol As Collection

    Private Sub sbPrint_Data()
        Dim sFn As String = "Sub sbPrint_Data()"

        Try
            Dim o_PrtInfo As New PRT_INFO
            Dim prt As New TESTINFO_PRINT

            With o_PrtInfo
                If Me.txtTCode.Tag.ToString.IndexOf("^"c) >= 0 Then
                    .TestCd = Me.txtTCode.Tag.ToString.Split("^"c)(0).Trim
                    .OrdCd = Me.txtTCode.Tag.ToString.Split("^"c)(1).Trim
                Else
                    If Me.lblTest.Text.Trim = "검사코드" Then
                        .TestCd = Me.txtTCode.Text.Trim
                    Else
                        .OrdCd = Me.txtTCode.Text.Trim
                    End If
                End If
                .TestNm = Me.txtTnmd.Text.Trim
                .Spcnm = Me.cboSpc.Text.Split("|"c)(0).Trim
                .TubeNm = Me.txtTubeNmd.Text.Trim
                .OrdSlip = Me.txtOrdSlip.Text.Trim
                .PartSlip = Me.txtSlipNmd.Text.Trim
                .UsDate = Me.txtUsDt.Text.Trim
                .ExLab = Me.txtExLabYn.Text.Trim
                .Rrptst = Me.txtRrptst.Text

                .ExeDay = ""
                .ExeDay += IIf(Me.chkExeDay1.Checked, "1", "0").ToString
                .ExeDay += IIf(Me.chkExeDay2.Checked, "1", "0").ToString
                .ExeDay += IIf(Me.chkExeDay3.Checked, "1", "0").ToString
                .ExeDay += IIf(Me.chkExeDay4.Checked, "1", "0").ToString
                .ExeDay += IIf(Me.chkExeDay5.Checked, "1", "0").ToString
                .ExeDay += IIf(Me.chkExeDay6.Checked, "1", "0").ToString
                .ExeDay += IIf(Me.chkExeDay7.Checked, "1", "0").ToString

                .ErGbn = ""
                .ErGbn += IIf(Me.chkErGbn2.Checked, "1", "0").ToString
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

                .RefTxt = Me.txtRef.Text
                .CWarning = Me.txtCWarning.Text

                .Info1 = Me.txtInfo1.Text
                .Info2 = Me.txtInfo2.Text
                .Info3 = Me.txtInfo3.Text

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
            Me.txtTAT.Text = ""

            Me.spdTestInfo.MaxRows = 0

            Me.txtRef.Text = ""
            Me.txtInfo1.Text = ""
            Me.txtInfo2.Text = ""
            Me.txtCWarning.Text = ""

            Me.picTube.Image = Nothing

            Me.txtInfo3.Text = ""

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

            dt = (New CDHELP.DA_CDHELP_TEST).fnGet_spc_info(rsTestCd)

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
        Try
            Dim sTubeCd As String = ""

            Dim dt As New DataTable

            dt = (New CDHELP.DA_CDHELP_TEST).fnGet_test_info(rsTestCd, rsSpcCd)
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

            dt = (New CDHELP.DA_CDHELP_TEST).fnGet_testspc_info(rsTestCd, rsSpcCd)
            If dt.Rows.Count > 0 Then
                sTubeCd = dt.Rows(0).Item("tubecd").ToString.Trim

                Me.txtUsDt.Text = dt.Rows(0).Item("usdt").ToString.Trim
                Me.txtTnmd.Text = dt.Rows(0).Item("tnmd").ToString.Trim
                Me.txtTubeNmd.Text = dt.Rows(0).Item("tubenmd").ToString.Trim
                Me.txtSlipNmd.Text = dt.Rows(0).Item("slipnmd").ToString.Trim
                Me.txtOrdSlip.Text = dt.Rows(0).Item("tordslipnm").ToString.Trim
                Me.txtRrptst.Text = dt.Rows(0).Item("rrptst").ToString.Trim
                Me.txtTelNo.Text = dt.Rows(0).Item("telno").ToString.Trim
                Me.txtExLabYn.Text = dt.Rows(0).Item("exlabyn").ToString.Trim 'IIf(dt.Rows(0).Item("exlabyn").ToString = "1", "Y", "N").ToString

                Me.txtRef.Text = dt.Rows(0).Item("descref").ToString


                If dt.Rows(0).Item("tatyn").ToString = "1" Then
                    Dim sTmp As String = ""
                    Dim iTmpD As Integer = 0 'dt.Rows("prptmi").ToString
                    Dim iTmpH As Integer = 0
                    Dim iTmpM As Integer = 0
                    Dim iTmp As Integer = 0

                    If dt.Rows(0).Item("prptmi").ToString <> "" Then
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

                        Me.txtTAT.Text += "중간TAT: " + sTmp + Space(10)
                    End If
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

                        Me.txtTAT.Text += "최종TAT: " + sTmp + Space(10)
                    End If
                    If dt.Rows(0).Item("erptmi").ToString <> "" Then
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

                        Me.txtTAT.Text += "응급TAT: " + sTmp + " 분"
                    End If
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
                    End If
                Next

                If dt.Rows(0).Item("ergbn1").ToString = "1" Then Me.chkErGbn1.Checked = True
                If dt.Rows(0).Item("ergbn2").ToString = "1" Then Me.chkErGbn2.Checked = True

                If Me.txtInfo1.Text <> "" Then Me.txtInfo1.Text += vbCrLf
                Me.txtInfo1.Text += dt.Rows(0).Item("reftest").ToString.Trim


            End If

            If Me.txtRef.Text.Replace(vbCrLf, "").Trim = "" Then
                dt = (New CDHELP.DA_CDHELP_TEST).fnGet_test_ref(rsTestCd, rsSpcCd, Me.txtUsDt.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

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

                            Me.txtRef.Text += dt.Rows(ix).Item("sage").ToString.Trim + sAgeYmd + IIf(dt.Rows(ix).Item("sages").ToString.Trim = "0", " <= ", " < ").ToString
                            Me.txtRef.Text += "환자" + IIf(dt.Rows(ix).Item("eages").ToString.Trim = "0", " <= ", " < ").ToString
                            Me.txtRef.Text += dt.Rows(ix).Item("eage").ToString.Trim + sAgeYmd + ": " + vbCrLf

                            If dt.Rows(ix).Item("refgbn").ToString = "1" Then
                                Me.txtRef.Text += Space(4) + dt.Rows(ix).Item("reflt").ToString.Trim
                            Else
                                Me.txtRef.Text += Space(4) + "(남) " + dt.Rows(ix).Item("reflm").ToString.Trim + IIf(dt.Rows(ix).Item("reflms").ToString.Trim = "0", " <= ", " < ").ToString + "결과"
                                Me.txtRef.Text += IIf(dt.Rows(ix).Item("refhms").ToString.Trim = "0", " <= ", " < ").ToString
                                Me.txtRef.Text += dt.Rows(ix).Item("refhm").ToString.Trim + ", " + vbCrLf

                                Me.txtRef.Text += Space(4) + "(여) " + dt.Rows(ix).Item("reflf").ToString.Trim + IIf(dt.Rows(ix).Item("reflfs").ToString.Trim = "0", " <= ", " < ").ToString + "결과"
                                Me.txtRef.Text += IIf(dt.Rows(ix).Item("refhfs").ToString.Trim = "0", " <= ", " < ").ToString
                                Me.txtRef.Text += dt.Rows(ix).Item("refhf").ToString.Trim
                            End If
                        End If
                    Next
                End If
            End If

            dt = (New CDHELP.DA_CDHELP_TEST).fnGet_testspc_ref(rsTestCd, rsSpcCd, Me.txtUsDt.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
            If dt.Rows.Count > 0 Then
                With spdTestInfo
                    .MaxRows = dt.Rows.Count

                    For ix As Integer = 0 To dt.Rows.Count - 1

                        .Row = ix + 1
                        .Col = .GetColFromID("tcdgbn") : .Text = dt.Rows(ix).Item("tcdgbn").ToString.Trim
                        .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString.Trim
                        .Col = .GetColFromID("tordcd") : .Text = dt.Rows(ix).Item("tordcd").ToString.Trim
                        .Col = .GetColFromID("sugacd") : .Text = dt.Rows(ix).Item("sugacd").ToString.Trim
                        .Col = .GetColFromID("edicd") : .Text = dt.Rows(ix).Item("edicd").ToString.Trim
                        .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString.Trim
                    Next
                End With
            End If

            Dim a_btBuf As Byte() = (New CDHELP.DA_CDHELP_TEST).fnGet_tube_img(sTubeCd)

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
            Me.lblTest.Text = " 처방코드"
        Else
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
        sbInti(Me)
        sbClear_Form()

        If Me.txtTCode.Text <> "" Then btnCdHelp_test_Click(Me.btnCdHelp_test, Nothing)

        Me.Left = MdiMain.Frm.Location.X + (MdiMain.Frm.Width - Me.Width) / 2
        Me.Top = MdiMain.Frm.Location.Y + (MdiMain.Frm.Height - Me.Height) / 2

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
                dt = (New CDHELP.DA_CDHELP_TEST).fnGet_Help_Info("")
            Else
                dt = (New CDHELP.DA_CDHELP_TEST).fnGet_Help_Info(Me.txtTnmd.Text)
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
            objHelp.AddField("slipnmd", "검사분류", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

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
                sbDisplay_Test(Me.txtTestCd.Text)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub cboSpc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSpc.SelectedIndexChanged

        If Me.cboSpc.Text = "" Then Return

        sbDisplay_TestSpc(Me.txttestcd.text, Me.cboSpc.Text.Split("|"c)(1))

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
            Me.lblTest.Text = " 처방코드"
        Else
            Me.lblTest.Text = " 검사코드"
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

        sbPrint_Data()

    End Sub
End Class

Public Class DA_CDHELP_TEST
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
            sSql += "       f6.tatyn, f6.prptmi, f6.frptmi, f6.erptmi, f6.perrptmi,f6.ferrptmi, minspcvol,f2.partnmd , f6.enforcement , f6.request , f6.cowarning "
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
            sSql += "       f6.tatyn, f6.prptmi, f6.frptmi, f6.erptmi , f6.perrptmi,f6.ferrptmi,minspcvol,f2.partnmd , '' , '' , ''"
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

            sSql += "SELECT DISTINCT"
            sSql += "       f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f6.tnmd, 1 sort1, f6.dispseqo sort2, f6.rstunit"
            sSql += "  FROM lf060m f6"
            'sSql += " WHERE f6.testcd  LIKE :testcd || '%'"
            sSql += " WHERE f6.testcd  = :testcd"
            sSql += "   AND f6.spccd   = :spccd"
            sSql += "   AND f6.usdt    = :usdt"
            sSql += " UNION "
            sSql += "SELECT f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f6.tnmd, 2 sort2, f6.dispseqo sort2, f6.rstunit"
            sSql += "  FROM lf067m f62, lf060m f6"
            sSql += " WHERE f62.tclscd = :testcd"
            sSql += "   AND f62.tspccd = :spccd"
            'sSql += "   AND f6.testcd  LIKE f62.testcd || '%'"
            sSql += "   AND f6.testcd  = f62.testcd "
            sSql += "   AND f6.spccd  = f62.spccd"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"
            sSql += " UNION "
            sSql += "SELECT DISTINCT"
            sSql += "       f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f6.tnmd, 1 sort1, f6.dispseqo sort2, f6.rstunit"
            sSql += "  FROM rf060m f6"
            sSql += " WHERE f6.testcd  LIKE :testcd || '%'"
            sSql += "   AND f6.spccd   = :spccd"
            sSql += "   AND f6.usdt    = :usdt"
            sSql += " UNION "
            sSql += "SELECT f6.tcdgbn, f6.testcd, f6.tordcd, f6.sugacd, f6.edicd, f6.tnmd, 2 sort2, f6.dispseqo sort2, f6.rstunit"
            sSql += "  FROM rf062m f62, rf060m f6"
            sSql += " WHERE f62.tclscd = :testcd"
            sSql += "   AND f62.tspccd = :spccd"
            sSql += "   AND f6.testcd  LIKE f62.testcd || '%'"
            sSql += "   AND f6.spccd  = f62.spccd"
            sSql += "   AND f6.usdt   <= fn_ack_sysdate"
            sSql += "   AND f6.uedt   >  fn_ack_sysdate"

            sSql += " ORDER BY sort1, sort2, testcd"

            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)
                dbCmd.Parameters.Add("usdt", rsUsDt)

                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)

                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)
                dbCmd.Parameters.Add("usdt", rsUsDt)

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

    Public Shared Function fnGet_File_Image(ByRef rsFileNm As String) As Byte()


        Dim dbCn As OracleConnection = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            With dbCmd
                .Connection = dbCn
            End With
            Dim sSql As String = ""
            sSql = ""
            sSql += "SELECT FILELEN, FILEBIN, filenm"
            sSql += "  FROM lrs15m"
            sSql += " WHERE seq = (select MAX(seq) from lrs15m )"
            sSql += " ORDER BY seq desc "

            With dbCmd
                .Connection = dbCn
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()

                .Parameters.Clear()
                '.Parameters.Add("fileseq", SqlDbType.Int, rsFileSeq.Length).Value = rsFileSeq
                '.Parameters.Add("lineseq", SqlDbType.Int, rsLineSeq.Length).Value = rsLineSeq

            End With

            Dim a_btReturn() As Byte

            'Dim dbDr As SqlDataReader = dbCmd.ExecuteReader(CommandBehavior.SequentialAccess)
            Dim dbDr As OracleDataReader = dbCmd.ExecuteReader(CommandBehavior.SequentialAccess)

            Do While dbDr.Read()

                Dim iStartIndex As Integer = 0
                Dim lngReturn As Long = 0

                Dim iBufferSize As Integer = 0

                Dim sFileSize As String = dbDr.GetValue(0).ToString


                iBufferSize = Convert.ToInt32(sFileSize)

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
                rsFileNm = dbDr.GetValue(2).ToString()

            Loop

            dbDr.Close()

            Return a_btReturn

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))


        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If dbCn.State = ConnectionState.Open Then dbCn.Close()
            dbCn.Dispose() : dbCn = Nothing

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
                dbCmd.Parameters.Add("usdt", rsUsDt)

                dbCmd.Parameters.Add("testcd", rsTestCd)
                dbCmd.Parameters.Add("spccd", rsSpcCd)
                dbCmd.Parameters.Add("usdt", rsUsDt)
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

Public Class PRT_INFO
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

End Class

Public Class TESTINFO_PRINT
    Private Const msFile As String = "File : FGCDHELP_TEST.vb, Class : CDHELP" + vbTab

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Public mbLandscape As Boolean = False
    Public m_PrtInfo As PRT_INFO

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
                    If ix <> 1 Then e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(ix) + 1, sgBoxY(0), sgBoxX(ix) + 1, sgBoxY(5))
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
                e.Graphics.DrawLine(Drawing.Pens.Black, sgBoxX(0), sgBoxY(ix) + 1, sgBoxX(9), sgBoxY(ix) + 1)
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