Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.CommLogin.LOGIN

Imports LISAPP.APP_BT


Public Class AxTnsPatinfo
    Private Const mcFile As String = "File : AxAckPatientInfo.dll, Class : AxTnsPatinfo" + vbTab

    Public ReadOnly Property AboRh() As String
        Get
            AboRh = Me.lblAbo.Text
        End Get
    End Property

    '//JJH
    Public ReadOnly Property Regno() As String
        Get
            Return Me.lblRegno.Text
        End Get
    End Property

    Public ReadOnly Property Ab_Screen() As String
        Get
            With Me.spdPatInfo
                For iCol As Integer = 1 To .MaxCols
                    .Col = iCol
                    .Row = 0 : Dim sTnmd As String = .Text
                    .Row = 1
                    If .BackColor = Color.Red Then
                        Return sTnmd + " 결과가 [" + .Text + "] 입니다." + vbCrLf + vbCrLf + "확인하세요.!!"
                    End If

                Next
            End With
           
            Return ""
        End Get
    End Property

    Private Sub AxTnsPatinfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sb_ClearLbl()
        DS_SpreadDesige.sbInti(spdPatInfo)
    End Sub

    Public Sub sb_ClearLbl()
        lblRegno.Text = ""
        lblPatNm.Text = ""
        lblAbo.Text = ""
        lblSexAge.Text = ""
        lblNation.Text = ""
        lblInfInfo.Text = ""
        lblHeight.Text = ""
        lblWeight.Text = ""
        lblEmer.Text = ""
        lblJumino.Text = ""
        lblPhone.Text = ""
        lblRmk.Text = ""
        lblOrdDate.Text = ""
        lblDeptCd.Text = ""
        lblDoctor.Text = ""
        lblIdate.Text = ""
        lblSr.Text = ""
        lblWd.Text = ""
        lblOdate.Text = ""
        lblDiagNm.Text = ""
        txtRmk.Text = ""
        txtSRmk.Text = ""

        With spdPatInfo
            .ReDraw = False
            .MaxRows = 0
            .MaxRows = 2
            .ReDraw = True

            For i As Integer = 0 To .MaxCols
                .Row = 0
                .Col = i : .Text = ""
            Next

        End With

    End Sub

    Public Sub sb_setPatinfo(ByVal rsRegno As String, ByVal rsOrddate As String, ByVal rsTnsNo As String)
        Dim sFn As String = "Sub sb_setPatinfo(ByVal rsRegno As String)"
        Dim dt As DataTable
        Dim sBBGBN As String = ""

        Dim sRh As String = ""
        Dim dtSysDate As Date = Fn.GetServerDateTime()

        Try
            sb_ClearLbl()

            ' 환자정보 디스플레이
            dt = OCSAPP.OcsLink.SData.fnGet_BldPatInfo(rsRegno, rsOrddate, rsTnsNo)

            If dt.Rows.Count < 1 Then Return

            Dim sPatInfo() As String = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

            '< 나이계산
            Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
            Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

            If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
            '>

            Me.lblRegno.Text = dt.Rows(0).Item("bunho").ToString.Trim
            Me.lblPatNm.Text = sPatInfo(0).Trim
            Me.lblSexAge.Text = sPatInfo(1).Trim + "/" + iAge.ToString
            Me.lblInfInfo.Text = dt.Rows(0).Item("infection").ToString.Trim
            Me.lblHeight.Text = dt.Rows(0).Item("height").ToString.Trim
            Me.lblWeight.Text = dt.Rows(0).Item("weight").ToString.Trim
            Me.lblEmer.Text = dt.Rows(0).Item("ernm").ToString.Trim
            Me.lblOrdDate.Text = dt.Rows(0).Item("order_date").ToString.Trim
            Me.lblDeptCd.Text = dt.Rows(0).Item("deptnm").ToString.Trim
            Me.lblDoctor.Text = dt.Rows(0).Item("doctornm").ToString.Trim
            Me.lblIdate.Text = dt.Rows(0).Item("ipwon_date").ToString.Trim
            Me.lblSr.Text = dt.Rows(0).Item("wardno").ToString.Trim
            Me.lblWd.Text = dt.Rows(0).Item("roomno").ToString.Trim
            Me.lblOdate.Text = dt.Rows(0).Item("opdt").ToString.Trim
            Me.lblDiagNm.Text = dt.Rows(0).Item("dignm").ToString.Trim
            Me.txtRmk.Text = dt.Rows(0).Item("drmk").ToString.Trim
            Me.txtSRmk.Text = dt.Rows(0).Item("sprmk").ToString.Trim

            Me.lblRmk.Text = sPatInfo(9).Trim
            Me.lblNation.Text = sPatInfo(8).Trim
            Me.lblJumino.Text = sPatInfo(3).Trim
            Me.lblPhone.Text = sPatInfo(4).Trim

            Dim sJubsuDt As String = dt.Rows(0).Item("jubsudt").ToString.Trim
            If sJubsuDt = "" Then sJubsuDt = Format(dtSysDate, "yyyyMMdd").ToString

            dt = CGDA_BT.fnGet_ABORh(rsRegno)

            If dt.Rows.Count > 0 Then
                Me.lblAbo.Text = dt.Rows(0).Item("aborh").ToString.Trim
                If dt.Rows(0).Item("aborh").ToString.Trim.Length > 0 Then
                    sRh = dt.Rows(0).Item("aborh").ToString.Substring(dt.Rows(0).Item("aborh").ToString.Length - 1)
                End If
            End If


            If Me.lblAbo.Text <> "" Then
                Me.lblAbo.ForeColor = fnGet_BloodColor(Me.lblAbo.Text.Replace(sRh, ""))
            End If

            If sRh = "-"c Then
                Me.lblAbo.BackColor = Color.Red
                If Me.lblAbo.ForeColor = Color.Red Then Me.lblAbo.ForeColor = Color.Black
            Else
                Me.lblAbo.BackColor = Color.White
            End If

            ' 최근 검사결과 세팅
            dt = CGDA_BT.fn_GetLatelyTestList(rsRegno)

            With Me.spdPatInfo
                .ReDraw = False
                .Col = 1 : .Col2 = .MaxCols
                .Row = 0 : .Row2 = .MaxRows
                .BlockMode = True
                .Action = FPSpreadADO.ActionConstants.ActionClearText
                .BlockMode = False

                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True
                .BackColor = Color.White
                .BlockMode = False

                .MaxCols = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    sBBGBN = dt.Rows(i).Item("bbgbn").ToString.Trim

                    .Col = i + 1
                    .set_ColWidth(.Col, 11)
                    .Row = 0 : .Text = dt.Rows(i).Item("tnmd").ToString.Trim
                    '20210420 jhs 접수일시 기준으로 해달라고 하여 변경
                    '.Row = 2 : .Text = dt.Rows(i).Item("fndt").ToString.Trim
                    .Row = 2 : .Text = dt.Rows(i).Item("tkdt").ToString.Trim
                    '-----------------------------------------------------------
                    .Row = 1 : .Text = dt.Rows(i).Item("viewrst").ToString.Trim : .ForeColor = Drawing.Color.Navy : .FontBold = True : .set_RowHeight(1, 11.9)
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                    If sBBGBN = CStr(enumBloodTest.Ab_SCR) Then
                        If dt.Rows(i).Item("months_between").ToString.Trim > "3" Then
                            .ForeColor = Drawing.Color.Silver
                        End If
                        If dt.Rows(i).Item("viewrst").ToString.Trim = "+" Or dt.Rows(i).Item("viewrst").ToString.Trim.ToLower.StartsWith("pos") Then
                            .BackColor = Color.Red
                        Else
                            .BackColor = Color.White
                        End If
                    End If
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(mcFile & sFn, Err)
        Finally
            spdPatInfo.ReDraw = True
        End Try

    End Sub

    Public Sub sb_setPatinfo(ByVal rsRegno As String, ByVal rsFkOcs As String)
        Dim sFn As String = "Sub sb_setPatinfo(String, String, String)"
        Dim dt As DataTable
        Dim sBBGBN As String = ""

        Dim sRh As String = ""
        Dim dtSysDate As Date = Fn.GetServerDateTime()

        Try
            sb_ClearLbl()

            ' 환자정보 디스플레이
            dt = OCSAPP.OcsLink.SData.fnGet_DonPatInfo(rsRegno, rsFkOcs)

            If dt.Rows.Count < 1 Then Return

            Dim sPatInfo() As String = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

            '< 나이계산
            Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
            Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

            If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
            '>

            Me.lblRegno.Text = dt.Rows(0).Item("bunho").ToString.Trim
            Me.lblPatNm.Text = sPatInfo(0).Trim
            Me.lblSexAge.Text = sPatInfo(1).Trim + "/" + iAge.ToString
            Me.lblInfInfo.Text = dt.Rows(0).Item("infection").ToString.Trim
            Me.lblHeight.Text = dt.Rows(0).Item("height").ToString.Trim
            Me.lblWeight.Text = dt.Rows(0).Item("weight").ToString.Trim
            Me.lblEmer.Text = dt.Rows(0).Item("ernm").ToString.Trim
            Me.lblOrdDate.Text = dt.Rows(0).Item("order_date").ToString.Trim
            Me.lblDeptCd.Text = dt.Rows(0).Item("deptcd").ToString.Trim
            Me.lblDoctor.Text = dt.Rows(0).Item("doctornm").ToString.Trim
            Me.lblIdate.Text = dt.Rows(0).Item("ipwon_date").ToString.Trim
            Me.lblSr.Text = dt.Rows(0).Item("wardno").ToString.Trim
            Me.lblWd.Text = dt.Rows(0).Item("roomno").ToString.Trim
            Me.lblOdate.Text = dt.Rows(0).Item("opdt").ToString.Trim
            Me.lblDiagNm.Text = dt.Rows(0).Item("dignm").ToString.Trim
            Me.txtRmk.Text = dt.Rows(0).Item("drmk").ToString.Trim
            Me.txtSRmk.Text = dt.Rows(0).Item("sprmk").ToString.Trim

            Me.lblRmk.Text = sPatInfo(9).Trim
            Me.lblNation.Text = sPatInfo(8).Trim
            Me.lblJumino.Text = sPatInfo(3).Trim
            Me.lblPhone.Text = sPatInfo(4).Trim

            Dim sJubsuDt As String = dt.Rows(0).Item("jubsudt").ToString.Trim
            If sJubsuDt = "" Then sJubsuDt = Format(dtSysDate, "yyyyMMdd").ToString

            dt = CGDA_BT.fnGet_ABORh(rsRegno)
            Me.lblAbo.Text = dt.Rows(0).Item("aborh").ToString.Trim

            If dt.Rows(0).Item("aborh").ToString.Trim.Length > 0 Then
                sRh = dt.Rows(0).Item("aborh").ToString.Substring(dt.Rows(0).Item("aborh").ToString.Length - 1)
            End If

            If Me.lblAbo.Text <> "" Then
                Me.lblAbo.ForeColor = fnGet_BloodColor(Me.lblAbo.Text.Replace(sRh, ""))
            End If

            If sRh = "-"c Then
                Me.lblAbo.BackColor = Color.Red
            Else
                Me.lblAbo.BackColor = Color.White
            End If

        Catch ex As Exception
            Fn.log(mcFile & sFn, Err)
        Finally
            spdPatInfo.ReDraw = True
        End Try

    End Sub

    Private Sub btnSebu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSebu.Click
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_Current(lblRegno.Text.Trim())
            If dt.Rows.Count = 0 Then
                MsgBox("OCS에서 환자정보를 찾을 수 없습니다!!", MsgBoxStyle.Information)

                Return
            End If

            Dim iLeft As Integer = Ctrl.FindControlLeft(btnSebu)
            Dim iTop As Integer = Ctrl.menuHeight + Ctrl.FindControlTop(btnSebu) + btnSebu.Height

            Dim patinfo As New AxAckResultViewer.PATINFO

            With patinfo
                .Left = iLeft
                .Top = iTop

                .RegNo = dt.Rows(0).Item("regno").ToString()
                .PatNm = dt.Rows(0).Item("patnm").ToString()
                .SexAge = dt.Rows(0).Item("sexage").ToString()
                .IdNo = dt.Rows(0).Item("idno").ToString()

                .OrdDt = dt.Rows(0).Item("orddt").ToString()
                .DeptNm = dt.Rows(0).Item("deptnm").ToString()
                .DoctorNm = dt.Rows(0).Item("doctornm").ToString()
                .WardRoom = dt.Rows(0).Item("wardroom").ToString()

                .Tel = (dt.Rows(0).Item("tel1").ToString() + " / " + dt.Rows(0).Item("tel2").ToString()).Trim
                If .Tel.StartsWith("/") Then .Tel = .Tel.Substring(1)
                If .Tel.EndsWith("/") Then .Tel = .Tel.Substring(0, .Tel.Length - 1)

                .Addr1 = dt.Rows(0).Item("addr1").ToString().Trim
                .Addr2 = dt.Rows(0).Item("addr2").ToString().Trim

                .Display_PatInfo()

                .ShowDialog()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mnuCopy_regno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCopy_regno.Click
        Clipboard.Clear()
        Clipboard.SetText(Me.lblRegno.Text)
    End Sub
End Class
