﻿'< New LIS 
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Imports COMMON.CommFN
Imports COMMON.SVar


Public Class AxPatientInfo

    Private Const mc_iHeight As Integer = 72

    Private mbSearch As Boolean = False
    Private mbDoner As Boolean = False
    Private moForm As Windows.Forms.Form
    Private mbShowAllCols As Boolean = False

    Private m_enumUseMode As enumUseMode

    Private m_al_HiddenCols As ArrayList

    Public msInfInfo As String

    Private m_color_def As Drawing.Color = Color.FromArgb(224, 224, 224)
    Private m_color_inf As Drawing.Color = Color.Crimson

    Private m_dt_ShareCmt_bcno As DataTable '검사자간 공유사항 추가

    Public ReadOnly Property PatInfo() As STU_PatInfo
        Get
            'If mbSearch = False Then Return Nothing

            Dim cpi As New STU_PatInfo

            cpi.REGNO = lblRegNo.Text.Trim()
            cpi.PATNM = lblPatNm.Text.Trim()

            If lblSexAge.Text.Trim().Length > 0 Then
                cpi.SEX = lblSexAge.Text.Trim().Split(CChar("/"))(0)
                cpi.AGE = lblSexAge.Text.Trim().Split(CChar("/"))(1)
            End If
            If lblIdNo.Text.Length > 0 Then
                cpi.IDNOL = lblIdNo.Text.Trim().Split(CChar("-"))(0)
                cpi.IDNOR = lblIdNo.Text.Trim().Split(CChar("-"))(1)
            End If
            cpi.TEL1 = lblTel.Text.Trim()
            cpi.DEPTCD = lblDeptCd.Text.Trim()
            cpi.DOCTORNM = lblDoctorNm.Text.Trim()
            cpi.WARD = lblWardCd.Text.Trim()
            cpi.WARDNM = lblWardCd.Text.Trim()
            cpi.ROOMNO = lblRoomNo.Text.Trim()
            cpi.BEDNO = lblRoomNo.Text.Trim()
            cpi.ENTDT = lblEntDt.Text.Trim()

            cpi.DIAG_K = lblDiagNm.Text.Trim()
            cpi.DIAG_E = lblDiagNme.Text.Trim()
            cpi.INFINFO = OCSAPP.OcsLink.Pat.fnGet_Pat_Infection(cpi.REGNO, False)

            cpi.INJONG = lblInjong.Text.Trim()
            cpi.GUBUN = lblGubun.Text
            cpi.SOGAE = lblSogae.Text
            cpi.VIP = lblVip.Text

            cpi.ABORh = OCSAPP.OcsLink.Pat.fnGet_Pat_AboRh(cpi.REGNO)

            cpi.BIRTHDAY = lblBirthDay.Text

            Return cpi
        End Get
    End Property

    Public Property DonerYn() As Boolean
        Set(ByVal value As Boolean)
            mbDoner = value

            If value Then
                Me.lblResDtail.Height = Me.lblOpDt.Height : Me.txtResDtail.Height = Me.lblOpDt.Height
                Me.lblOpDt.Visible = True
                Me.lblOpDt_Label.Visible = True
            Else
                Me.lblResDtail.Height = 54 : Me.txtResDtail.Height = 54

                Me.lblOpDt.Visible = False
                Me.lblOpDt_Label.Visible = False
            End If
        End Set
        Get
            Return mbDoner
        End Get
    End Property

    Public ReadOnly Property RegNo() As String
        Get
            Dim sRegNo As String = ""

            sRegNo = lblRegNo.Text

            Return sRegNo
        End Get
    End Property
      
    Public ReadOnly Property PatNm() As String
        Get
            Dim sPatNm As String = ""

            sPatNm = lblPatNm.Text

            Return sPatNm
        End Get
    End Property

    Public ReadOnly Property SEX() As String
        Get
            Dim sSEX As String = ""

            sSEX = lblSexAge.Text.Substring(0, lblSexAge.Text.IndexOf("/") - 1)

            Return sSEX
        End Get
    End Property

    Public ReadOnly Property AGE() As String
        Get
            Dim sAGE As String = ""

            sAGE = lblSexAge.Text.Substring(lblSexAge.Text.IndexOf("/") + 1)

            Return sAGE
        End Get
    End Property

    Public ReadOnly Property DeptCd() As String
        Get
            Return Me.lblDeptCd.Text
        End Get
    End Property

    Public ReadOnly Property OrdDt() As String
        Get
            Return Me.lblOrdDt.Text
        End Get
    End Property

    Public ReadOnly Property Ward() As String
        Get
            Return Me.lblWardCd.Text
        End Get
    End Property

    Public WriteOnly Property IsInfected() As Boolean
        Set(ByVal value As Boolean)

        End Set
    End Property
    Public WriteOnly Property Form() As Windows.Forms.Form
        Set(ByVal value As Windows.Forms.Form)
            moForm = value
        End Set
    End Property

    Public Sub Clear()  
        Me.lblAbo.Text = ""
        Me.lblDeptCd.Text = ""
        Me.lblGubun.Text = ""
        Me.lblHeight.Text = ""
        Me.lblInfInfo.Text = ""
        Me.lblPatNm.Text = ""
        Me.lblRegNo.Text = ""
        Me.txtRemark.Text = ""
        Me.lblDiagNm.Text = ""
        Me.lblSexAge.Text = ""
        Me.lblSogae.Text = ""
        Me.lblVip.Text = ""
        Me.lblWeight.Text = ""
        Me.lblEntDt.Text = ""
        Me.lblWardCd.Text = ""
        Me.lblRoomNo.Text = ""
        Me.lblDoctorNm.Text = ""
        Me.lblOrdDt.Text = ""
        Me.lblIdNo.Text = ""
        Me.lblTel.Text = ""
        Me.lblEmer.Text = ""
        Me.lblInjong.Text = ""
        Me.txtSpecialCmt.Text = ""

        Me.txtResDtail.Text = ""

        Me.lblDiagNme.Text = ""
        Me.lblBirthDay.Text = ""

    End Sub

    Public Sub DisplayPatInfo(ByVal r_cpi As COMMON.SVar.STU_PatInfo)
         
        'lblDeptCd.Text = r_cpi.DEPTCD 

        If r_cpi.INFINFO.Length > 7 Then
            Me.lblInfInfo.Text = r_cpi.INFINFO.Substring(0, 7) & ".."
        Else
            '외래채혈일 때 감염정보 G로 표기 요청 (2019-10-29)
            If moForm.Text = "FGC31ː외래채혈" Then
                If r_cpi.INFINFO <> "" Then
                    Me.lblInfInfo.Text = "G"
                End If
            Else
                Me.lblInfInfo.Text = r_cpi.INFINFO
            End If

        End If

        Me.lblPatNm.Text = r_cpi.PATNM
        Me.lblRegNo.Text = r_cpi.REGNO
        Me.lblDiagNm.Text = r_cpi.DRUG
        Me.lblSexAge.Text = r_cpi.SEX + "/" + r_cpi.AGE
        Me.lblBirthDay.Text = r_cpi.BIRTHDAY

        Me.lblHeight.Text = r_cpi.HEIGHT
        Me.lblWeight.Text = r_cpi.WEIGHT

        Me.lblDeptCd.Text = r_cpi.DEPTNM
        Me.lblEntDt.Text = r_cpi.ENTDT
        Me.lblWardCd.Text = r_cpi.WARDNM
        Me.lblRoomNo.Text = r_cpi.ROOMNO
        Me.lblDoctorNm.Text = r_cpi.DOCTORNM
        Me.lblOrdDt.Text = r_cpi.ORDDT

        Me.lblIdNo.Text = r_cpi.IDNO
        Me.lblTel.Text = r_cpi.TEL1
        Me.lblInjong.Text = r_cpi.INJONG
        Me.txtSpecialCmt.Text = r_cpi.SPCOMMENT

        '<<<20180104 DUR 특이사항 추가 --20190627 페스트의심환자 체크
        'Dim sTripInfo As String = fnGetTripInfo(r_cpi.PATNM, r_cpi.IDNOL, r_cpi.IDNOR, r_cpi.ORDDT)
        'If sTripInfo.Trim <> "" Then

        '    If sTripInfo.Substring(0, 9) = "0000000000" Then
        '        Me.txtSpecialCmt.Text += vbCrLf + sTripInfo
        '    End If

        'End If
        '>>>
        Me.lblGubun.Text = r_cpi.GUBUN
        Me.lblSogae.Text = r_cpi.SOGAE
        Me.lblVip.Text = r_cpi.VIP



        Me.lblDiagNm.Text = IIf(r_cpi.DIAG_K = "", r_cpi.DIAG_E, r_cpi.DIAG_K).ToString

        '< 혈액종양 진단명
        If r_cpi.DiagLeukemia Then
            Me.lblDiagNm.BackColor = Color.LightPink
        Else
            Me.lblDiagNm.BackColor = Color.White
        End If
        '>

        Me.lblDiagNme.Text = r_cpi.DIAG_E

        msInfInfo = r_cpi.INFINFO

        Me.lblAbo.Text = r_cpi.ABORh

        If r_cpi.ERFLG = "Y" Then
            Me.lblEmer.Text = r_cpi.ERFLG
        ElseIf r_cpi.ERFLG = "D" Then
            Me.lblEmer.Text = r_cpi.ERFLG
        Else
            Me.lblEmer.Text = r_cpi.ERFLG
        End If

        Me.txtResDtail.Text = r_cpi.RESDT
        '<<<20180208 항응고재 내역 추가 및  수정 
        Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_AntiDurg_Info(r_cpi.REGNO)

        If dt.Rows.Count > 0 Then
            Me.txtSpecialCmt.Text += vbCrLf
            Dim sRows As String = ""

            For ix As Integer = 0 To dt.Rows.Count - 1
                If ix > 0 Then
                    sRows += vbCrLf
                End If
                sRows += dt.Rows(ix).Item("grupnm").ToString + " | " + vbCrLf
                sRows += dt.Rows(ix).Item("prcpdd").ToString.Substring(0, 4) + "-" + dt.Rows(ix).Item("prcpdd").ToString.Substring(4, 2) + "-" + dt.Rows(ix).Item("prcpdd").ToString.Substring(6, 2) + " | "
                sRows += dt.Rows(ix).Item("prcpdayno").ToString + "일(" + dt.Rows(ix).Item("endprcp").ToString + ")| " '<<<20180515 항응고제 날짜 추가 
                sRows += vbCrLf
                sRows += dt.Rows(ix).Item("comdesc").ToString + "  "
            Next

            ' Me.txtSpecialCmt.Text += "[항응고제 처방 이력 있음]" + vbCrLf
            If Me.txtSpecialCmt.Text.Trim = "" Then
                Me.txtSpecialCmt.Text = sRows
            Else
                Me.txtSpecialCmt.Text += sRows
            End If

        
        End If
        '>>>20180208



    End Sub

    Public Function fnGetTripInfo(ByVal rsPatnm As String, ByVal rsIDnoL As String, ByVal rsIDnoR As String, ByVal rsOrddt As String) As String

        Try
            If rsOrddt <> "" Then
                rsOrddt = rsOrddt.Replace("-", "").Trim.Substring(0, 8)
            End If


            Dim sRetv As String = ""
            Dim pJuminNo As String = rsIDnoL + rsIDnoR          ' //수진자 주민등록번호
            Dim pPatNm As String = rsPatnm                       ' //수진자 이름
            Dim pMprscIssueAdmin As String = "11101318"              '  //요양기관 코드
            Dim pPrscAdminName As String = "국립중앙의료원"          ' //요양기관 명칭
            Dim pPrscPresDt As String = rsOrddt ' //기준일자(점검일자(처방일자))
            Dim pAppIssueAdmin As String = "11101318"                ' //청구SW업체코드 (요양기관코드)
            Dim pAppIssueCode As String = "D09278512011202412083065720112" ' //청구SW인증코드 (병원문의 DUR 개발서버 전송시 : 요양기관번호 + '0000000000000000000000')

            If pJuminNo = "" Then Return ""
            If pPatNm = "" Then Return ""


            sRetv = PDCheck(pJuminNo, pPatNm, pMprscIssueAdmin, pPrscAdminName, pPrscPresDt, pAppIssueAdmin, pAppIssueCode)

            Return sRetv

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    <DllImport("eirspdc.dll", SetLastError:=True, _
   CharSet:=CharSet.Ansi, ExactSpelling:=True, _
   CallingConvention:=CallingConvention.StdCall)> _
   Public Shared Function PDCheck(ByVal JuminNo As String, ByVal PatNm As String, ByVal MprscIssueAdmin As String, ByVal PrscAdminName As String, ByVal PrscPresDt As String, ByVal AppIssueAdmin As String, ByVal AppIssueCode As String) As String

    End Function

    Public Sub DisplayPatInfoDetail(ByVal r_cti As COMMON.SVar.STU_TestItemInfo)

        lblEntDt.Text = r_cti.ENTDT
        lblWardCd.Text = r_cti.WARDCD
        lblRoomNo.Text = r_cti.ROOMNO
        lblDeptCd.Text = r_cti.DEPTCD
        lblHeight.Text = r_cti.HEIGHT
        lblWeight.Text = r_cti.WEIGHT
        lblOrdDt.Text = r_cti.ORDDT
        txtRemark.Text = r_cti.REMARK
        lblDoctorNm.Text = r_cti.DOCTORNM


    End Sub

    Private Sub AxPatInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Clear()
    End Sub

    Private Sub btnDetailPatInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetailPatInfo.Click
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_Current(lblRegNo.Text.Trim())
            If dt.Rows.Count = 0 Then
                MsgBox("OCS에서 환자정보를 찾을 수 없습니다!!", MsgBoxStyle.Information)

                Return
            End If

            Dim iLeft As Integer = Ctrl.FindControlLeft(btnDetailPatInfo)
            Dim iTop As Integer = Ctrl.menuHeight + Ctrl.FindControlTop(btnDetailPatInfo) + btnDetailPatInfo.Height

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
                '.InWonDate = dt.Rows(0).Item("entdt").ToString + "/" + dt.Rows(0).Item("entdt_to").ToString

                .Tel = (dt.Rows(0).Item("tel1").ToString() + " / " + dt.Rows(0).Item("tel2").ToString()).Trim
                If .Tel.StartsWith("/") Then .Tel = .Tel.Substring(1)
                If .Tel.EndsWith("/") Then .Tel = .Tel.Substring(0, .Tel.Length - 1)

                .Addr1 = dt.Rows(0).Item("addr1").ToString().Trim
                .Addr2 = dt.Rows(0).Item("addr2").ToString().Trim

                .Display_PatInfo()

                '20210408 JHS 검사자간 공유사항 추가
                dt = LISAPP.COMM.RstFn.fnGet_Rst_ShareComment_slip(dt.Rows(0).Item("regno").ToString())
                m_dt_ShareCmt_bcno = dt
                sbDisplay_ShareCmt(dt.Rows(0).Item("regno").ToString())
                '------------------------------------------


                .ShowDialog()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mnuCopy_regno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuCopy_regno.Click
        Clipboard.Clear()
        Clipboard.SetText(Me.lblRegNo.Text)

    End Sub




    '20210408 jhs 검사자간 공유사항 추가
    Private Sub txtShareCmtCont_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)


        Dim ci As New ShareCMT_INFO

        With ci
            .CmtCont = Me.txtShareCmtCont.Text
        End With

        sbSet_ShareCmt_BcNo_Edit(ci)

    End Sub

    Private Sub sbSet_ShareCmt_BcNo_Edit(ByVal r_ci As ShareCMT_INFO)
        Dim sFn As String = "sbSet_Cmt_BcNo_Edit"

        Try
            With m_dt_ShareCmt_bcno
                Dim iRow As Integer = -1

                For ix As Integer = 0 To .Rows.Count - 1
                    If .Rows(ix).Item("bcno").ToString = r_ci.BcNo And .Rows(ix).Item("partslip").ToString = r_ci.PartSlip Then
                        iRow = ix
                        Exit For
                    End If
                Next

                If iRow < 0 Then
                    sbSet_ShareCmt_BcNo_Add(r_ci)
                Else
                    Dim a_fieldinfo() As System.Reflection.FieldInfo = r_ci.GetType().GetFields()
                    Dim sStatus As String = "S"

                    For ix As Integer = 0 To a_fieldinfo.Length - 1
                        Dim sFieldName As String = a_fieldinfo(ix).Name.ToLower
                        Dim sFieldValue As String = a_fieldinfo(ix).GetValue(r_ci).ToString()

                        '수정된 부분이 있는 지 조사하고 있으면 변경
                        If Not .Rows(iRow).Item(sFieldName).ToString() = sFieldValue Then
                            .Rows(iRow).Item(sFieldName) = sFieldValue
                            sStatus = "U"
                        End If
                    Next

                    'status
                    If .Rows(iRow).Item("status").ToString() = "S" Then
                        .Rows(iRow).Item("status") = sStatus
                    End If

                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub sbSet_ShareCmt_BcNo_Add(ByVal r_ci As ShareCMT_INFO)
        Dim sFn As String = "sbSet_Cmt_BcNo_Add"

        Try
            With m_dt_ShareCmt_bcno
                'Row 추가
                Dim dr As DataRow = .NewRow()

                Dim a_fieldinfo() As System.Reflection.FieldInfo = r_ci.GetType().GetFields()

                For j As Integer = 1 To a_fieldinfo.Length
                    Dim sFieldName As String = a_fieldinfo(j - 1).Name.ToLower
                    Dim sFieldValue As String = a_fieldinfo(j - 1).GetValue(r_ci).ToString()

                    If Not sFieldValue = "" Then
                        dr.Item(sFieldName) = sFieldValue
                    End If
                Next

                'status
                dr.Item("status") = "I"

                .Rows.Add(dr)
            End With

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub
    Private Sub sbDisplay_ShareCmt(ByVal rsBcno As String)
        Dim sFn As String = "sbDisplay_Cmt_One_slipcd"

        Try
            Me.txtShareCmtCont.Text = ""

            Dim a_dr As DataRow()
            Dim a_dt As DataTable = New DataTable

            'If rsSlipCd = "" Then
            a_dr = m_dt_ShareCmt_bcno.Select("bcno = '" + rsBcNo + "'", "partslip")
            'Else
            'a_dr = m_dt_ShareCmt_bcno.Select("bcno = '" + rsBcNo + "' AND partslip = '" + rsSlipCd + "'")
            'End If

            'If rsSlipCd = "" Then
            '    For ix As Integer = 0 To a_dr.Length - 1
            '        Me.txtShareCmtCont.Text += "[" + a_dr(ix).Item("slipnmd").ToString.Trim + "]" + vbCrLf
            '        Me.txtShareCmtCont.Text += a_dr(ix).Item("cmtcont").ToString + vbCrLf
            '    Next
            'Else
            If a_dr.Length > 0 Then
                Me.txtShareCmtCont.Text = a_dr(0).Item("cmtcont").ToString
            End If
            'End If

            'If rsSlipCd = "" Then
            '    Me.txtShareCmtCont.ReadOnly = True
            'Else
            '    Me.txtShareCmtCont.ReadOnly = False
            'End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnShareCmtDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShareCmtDel.Click
        Dim alShareCmt As New ArrayList
        Dim a_dr As DataRow()
        Dim chkbool As Boolean = False
        Try

            If lblRegNo.Text = "" Then
                MsgBox("검체 조회를 먼저 진행해주세요.")
                Return
            End If

            txtShareCmtCont_LostFocus(Nothing, Nothing)
            a_dr = m_dt_ShareCmt_bcno.Select() '--"status <> 'S'")

            For ix As Integer = 0 To a_dr.Length - 1
                Dim arlBuf() As String

                arlBuf = a_dr(ix).Item("cmtcont").ToString.Replace(Chr(10), "").Split(Chr(13))

                For ix2 As Integer = 0 To arlBuf.Length - 1
                    Dim objBR As New ResultInfo_ShareCmt
                    'objBR.BcNo = a_dr(ix).Item("bcno").ToString
                    objBR.PartSlip = a_dr(ix).Item("partslip").ToString
                    'objBR.TestCd = ""
                    objBR.Regno = ""

                    objBR.RstSeq = Convert.ToString(ix2).PadLeft(2, "0"c)
                    objBR.Cmt = arlBuf(ix2)
                    objBR.SaveFlg = "2" '삭제플래그

                    alShareCmt.Add(objBR)
                Next
            Next

            Dim objRst As New LISAPP.APP_R.AxRstFn

            chkbool = objRst.fnReg_shareCmt(alShareCmt)

            If chkbool Then
            ElseIf chkbool = False Then
                MsgBox("검사자간 공유사항 저장 오류")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnShareCmtAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShareCmtAdd.Click

        Dim alShareCmt As New ArrayList
        Dim a_dr As DataRow()
        Dim chkbool As Boolean = False
        Try

            If lblRegNo.Text = "" Then
                MsgBox("검체 조회를 먼저 진행해주세요.")
                Return
            End If

            txtShareCmtCont_LostFocus(Nothing, Nothing)
            a_dr = m_dt_ShareCmt_bcno.Select() '--"status <> 'S'")

            For ix As Integer = 0 To a_dr.Length - 1
                Dim arlBuf() As String

                arlBuf = a_dr(ix).Item("cmtcont").ToString.Replace(Chr(10), "").Split(Chr(13))

                For ix2 As Integer = 0 To arlBuf.Length - 1
                    Dim objBR As New ResultInfo_ShareCmt
                    'objBR.BcNo = a_dr(ix).Item("bcno").ToString
                    'objBR.TestCd = ""

                    objBR.PartSlip = a_dr(ix).Item("partslip").ToString
                    objBR.Regno = ""

                    objBR.RstSeq = Convert.ToString(ix2).PadLeft(2, "0"c)
                    objBR.Cmt = arlBuf(ix2)
                    objBR.SaveFlg = "1" '추가 플래그

                    alShareCmt.Add(objBR)
                Next
            Next

            Dim objRst As New LISAPP.APP_R.AxRstFn

            chkbool = objRst.fnReg_shareCmt(alShareCmt)

            If chkbool Then
            ElseIf chkbool = False Then
                MsgBox("검사자간 공유사항 저장 오류")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '-----------------------------------------------
End Class

'20210303 jhs 공유자간 정보 객체클래스 추가
Public Class ShareCMT_INFO
    Public BcNo As String = ""
    Public PartSlip As String = ""
    Public CmtCont As String = ""
End Class
'----------------------------------
