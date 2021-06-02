Imports COMMON.CommFN
Imports common.commlogin.login

Imports System.Windows.Forms

Public Class AxRstPatInfo

    Private msRegNo As String = ""
    Private msBcNo As String = ""
    Private msSlipCd As String = ""
    Private msUsrLevel As String = ""

    Private m_tooltip As New Windows.Forms.ToolTip

    Public Property SlipCd() As String
        Get
            SlipCd = msSlipCd
        End Get
        Set(ByVal value As String)
            msSlipCd = value
        End Set
    End Property

    Public Property BcNo() As String
        Get
            BcNo = msBcNo
        End Get
        Set(ByVal value As String)
            msBcNo = value
        End Set
    End Property

    Public Property RegNo() As String
        Get
            RegNo = Me.lblRegNo.Text
        End Get
        Set(ByVal value As String)
            msRegNo = value
        End Set
    End Property

    Public ReadOnly Property ABORh() As String
        Get
            ABORh = Me.lblAbo.Text
        End Get
    End Property

    Public ReadOnly Property GenDr() As String
        Get
            GenDr = Me.lblGenDr.Text
        End Get
    End Property

    Public WriteOnly Property UsrLevel() As String
        Set(ByVal value As String)
            msUsrLevel = value

            If USER_SKILL.Authority("R01", 7) Then
                Me.btnPatInfo.Enabled = True
            Else
                Me.btnPatInfo.Enabled = False
            End If
        End Set
    End Property

    Public ReadOnly Property WkNO() As String
        Get
            WkNO = Me.txtWkNo.Text
        End Get
    End Property

    Public ReadOnly Property FnDt() As String
        Get
            FnDt = Me.lblFnDt.Text
        End Get
    End Property

    Public ReadOnly Property PatNm() As String
        Get
            PatNm = Me.lblPatNm.Text
        End Get
    End Property

    Public ReadOnly Property SexAge() As String
        Get
            SexAge = Me.lblSexAge.Text
        End Get
    End Property

    Public ReadOnly Property DiagNm() As String
        Get
            DiagNm = Me.txtDiagNm.Text
        End Get
    End Property

    Public ReadOnly Property DrugNm() As String
        Get
            DrugNm = ""
        End Get
    End Property

    Public ReadOnly Property Remark() As String
        Get
            Remark = Me.txtRemark.Text
        End Get
    End Property

    Public ReadOnly Property CollDt() As String
        Get
            If Me.lblCollDt.Tag Is Nothing Then Me.lblCollDt.Tag = ""
            CollDt = Me.lblCollDt.Tag.ToString
        End Get
    End Property

    Public ReadOnly Property TkDt() As String
        Get
            TkDt = Me.lblTkDt.Text.ToString
        End Get
    End Property

    Public ReadOnly Property OrdDt() As String
        Get
            OrdDt = Me.lblOrdDt.Text
        End Get
    End Property

    Public ReadOnly Property IdNo() As String
        Get
            IdNo = Me.lblIdNo.Text
        End Get
    End Property

    Public ReadOnly Property DocName() As String
        Get
            DocName = Me.lblDoctorNm.Text
        End Get
    End Property

    Public ReadOnly Property DeptName() As String
        Get
            If Me.lblDeptCd.Tag Is Nothing Then Me.lblDeptCd.Tag = ""
            DeptName = Me.lblDeptCd.Tag.ToString
        End Get
    End Property

    Public ReadOnly Property WardRoom() As String
        Get
            WardRoom = Me.lblWardCd.Text + "/" + Me.lblRoomNo.Text
        End Get
    End Property

    Public ReadOnly Property EntDt() As String
        Get
            EntDt = Me.lblEntDt.Text
        End Get
    End Property

    Public ReadOnly Property SpcNmd() As String
        Get
            SpcNmd = Me.lblSpcNmd.Text
        End Get
    End Property

    Public Sub sbDisplay_Init()

        Me.lblRegNo.Text = ""
        Me.lblPatNm.Text = ""
        Me.lblSexAge.Text = ""
        Me.lblHeight.Text = ""
        Me.lblWeight.Text = ""
        Me.lblInjong.Text = ""
        Me.lblAbo.Text = ""
        Me.lblIdNo.Text = ""

        Me.lblOrdDt.Text = ""
        Me.lblDeptCd.Text = ""
        Me.lblDoctorNm.Text = ""
        Me.lblGenDr.Text = ""
        Me.lblEntDt.Text = ""
        Me.lblWardCd.Text = ""
        Me.lblRoomNo.Text = ""
        Me.txtDiagNm.Text = ""
        Me.lblER.Text = ""
        Me.lblInf.Text = ""
        Me.lblDonor.Text = ""
        Me.txtRemark.Text = ""

        Me.lblCollDt.Text = "" : Me.lblCollDt.Tag = ""
        Me.lblTkDt.Text = "" : Me.lblTkDt.Tag = ""
        Me.lblFnDt.Text = "" : Me.lblFnDt.Tag = ""

        Me.txtBcNo.Text = ""
        Me.txtWkNo.Text = ""
        Me.txtPrtBcNo.Text = ""

        Me.lblSpcNmd.Text = ""

    End Sub

    Public Sub sbDisplay_rst_info(ByVal rsBcNo As String, ByVal rsTestCd As String)
        Try
            If rsBcNo = "" Then Return

            Dim dt As DataTable = (New LISAPP.APP_R.RstFn).fnGet_rstInfo_test(rsBcNo, rsTestCd)

            If dt.Rows.Count < 1 Then Return

            Me.txtWkNo.Text = dt.Rows(0).Item("workno").ToString
            Me.lblFnDt.Text = dt.Rows(0).Item("rstdt").ToString

        Catch ex As Exception

        End Try
    End Sub


    Public Function fnDisplay_Data() As Boolean
        ' 정은생성 
        Dim sFn As String = "Public Sub sbDisplay_Data()"

        Try
            sbDisplay_Init()

            ' 결과화면 공통 AxRstPatInfo_new 환자정보조회 
            Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_PatInfo(msBcNo, msSlipCd)

            If dt.Rows.Count < 1 Then
                Return False
            Else
                Dim sPatInfo() = dt.Rows(0).Item("patinfo").ToString.Split("|"c)  ' 환자정보들 

                msRegNo = dt.Rows(0).Item("regno").ToString.Trim

                Me.lblRegNo.Text = dt.Rows(0).Item("regno").ToString().Trim      ' 등록번호 
                Me.lblPatNm.Text = sPatInfo(0).Trim      ' 환자명 
                Me.lblSexAge.Text = sPatInfo(1).Trim.Trim + "/" + dt.Rows(0).Item("age").ToString().Trim   ' 성별/나이 
                Me.lblHeight.Text = dt.Rows(0).Item("height").ToString().Trim    ' 키
                Me.lblWeight.Text = dt.Rows(0).Item("weight").ToString().Trim    ' 몸무게 
                Me.lblInjong.Text = sPatInfo(8).Trim                             ' 인종 
                Me.lblAbo.Text = dt.Rows(0).Item("aborh").ToString().Trim        ' 혈액형
                Me.lblIdNo.Text = sPatInfo(3).Trim    ' 주민등록번호 


                Me.lblOrdDt.Text = dt.Rows(0).Item("orddt").ToString().Trim          ' 처방일시 
                Me.lblDeptCd.Text = dt.Rows(0).Item("deptcd").ToString().Trim        ' 진료과코드 
                Me.lblDeptCd.Tag = dt.Rows(0).Item("deptnm").ToString().Trim         ' 진료과명

                Me.lblDoctorNm.Text = dt.Rows(0).Item("doctornm").ToString().Trim       ' 의사명 
                Me.lblGenDr.Text = OCSAPP.OcsLink.Ord.fnGet_GenDr_Name(msBcNo, msRegNo) ' 주치의/담당의
                '<20130704 정선영 수정
                If lblGenDr.Text = "" Then
                    Me.lblGenDr.Text = dt.Rows(0).Item("doctornm").ToString().Trim
                End If
                '>
                Me.lblEntDt.Text = dt.Rows(0).Item("entdt").ToString().Trim          ' 입원일 
                Me.lblWardCd.Text = dt.Rows(0).Item("wardno").ToString().Trim        ' 병동 
                Me.lblRoomNo.Text = dt.Rows(0).Item("roomno").ToString().Trim        ' 병실 

                '<< 혈액종양 진단명
                'Dim LeukemiaChk As Boolean = False
                'Dim LeukemiaDt As DataTable = OCSAPP.OcsLink.Pat.fnGet_Diag_Leukemia()

                'If LeukemiaDt.Rows.Count > 0 Then
                '    If dt.Rows(0).Item("diagnm").ToString().Trim <> "" Then
                '        For i As Integer = 0 To LeukemiaDt.Rows.Count - 1
                '            If dt.Rows(0).Item("diagnm").ToString().Trim = LeukemiaDt.Rows(i).Item("DIAG_HNG").ToString Then
                '                LeukemiaChk = True
                '                Exit For
                '            ElseIf dt.Rows(0).Item("diagnm").ToString().Trim = LeukemiaDt.Rows(i).Item("DIAG_ENG").ToString Then
                '                LeukemiaChk = True
                '                Exit For
                '            End If
                '        Next
                '    End If
                'End If

                Me.txtDiagNm.Text = dt.Rows(0).Item("diagnm").ToString().Trim   ' 진단명 

                '< JJH 혈액 종양 진단 판단 받았는지 체크
                Dim LeukemiaYn As String = OCSAPP.OcsLink.Pat.fnGet_Diag_Leukemia_Chk(msRegNo)
                If LeukemiaYn = "Y" Then
                    Me.txtDiagNm.BackColor = Drawing.Color.Red
                    Me.txtDiagNm.ForeColor = Drawing.Color.White
                Else
                    Me.txtDiagNm.BackColor = Drawing.Color.White
                    Me.txtDiagNm.ForeColor = Drawing.Color.Black
                End If
                '>


                If dt.Rows(0).Item("colldt").ToString().Trim <> "" Then Me.lblCollDt.Text = dt.Rows(0).Item("colldt").ToString().Substring(0, 16) : Me.lblCollDt.Tag = dt.Rows(0).Item("colldt").ToString() '채혈일시 
                If dt.Rows(0).Item("tkdt").ToString().Trim <> "" Then Me.lblTkDt.Text = dt.Rows(0).Item("tkdt").ToString().Substring(0, 16) : Me.lblTkDt.Tag = dt.Rows(0).Item("tkdt").ToString() ' 접수일시
                If dt.Rows(0).Item("rstdt").ToString().Trim <> "" Then Me.lblFnDt.Text = dt.Rows(0).Item("rstdt").ToString().Substring(0, 16) : Me.lblFnDt.Tag = dt.Rows(0).Item("rstdt").ToString() ' 보고일시 

                Me.txtBcNo.Text = dt.Rows(0).Item("bcno").ToString().Trim        ' 검체번호 
                Me.txtWkNo.Text = dt.Rows(0).Item("workno").ToString().Trim      ' 작업번호
                Me.txtPrtBcNo.Text = dt.Rows(0).Item("prtbcno").ToString().Trim  ' 바코드번호 
                Me.lblSpcNmd.Text = dt.Rows(0).Item("spcnmd").ToString().Trim    ' 검체명

                '-- TAT
                Me.lblTat.Text = dt.Rows(0).Item("tat_mi").ToString.Trim

                '<< JJH 자체응급일때 Y표시
                Dim ERYN As String = LISAPP.COMM.RstFn.fnGet_ERYN(msBcNo)
                If ERYN = "Y" Then
                    Me.lblER.BackColor = Drawing.Color.Purple
                Else
                    Me.lblER.BackColor = Drawing.Color.FromArgb(254, 226, 235)
                End If
                '>>

                If dt.Rows(0).Item("iogbn").ToString = "E" Then
                    '-- 응급실인 경우는 뇌졸증 여부 표시

                    Me.lblER.Text = OCSAPP.OcsLink.Pat.fnGet_Pat_Type(dt.Rows(0).Item("regno").ToString().Trim, dt.Rows(0).Item("orddt").ToString().Trim)
                End If

                If Me.lblER.Text = "" Then
                    '-- 응급
                    If (dt.Rows(0).Item("statgbn").ToString().Trim <> "" Or ERYN = "Y") Then
                        Me.lblER.Text = "응급"
                    Else
                        Me.lblER.Text = ""
                    End If
                End If

                '-- 감염정보
                Dim sinf As String = LISAPP.APP_C.Collfn.FindInfectionInfoD(msRegNo)
                If sinf = "" Then
                    Me.lblInf.Text = sinf
                    Me.lblInf.BackColor = System.Drawing.Color.White
                Else
                    Me.lblInf.BackColor = System.Drawing.Color.Red
                    Me.lblInf.Text = sinf
                End If

                'Me.lblInf.Text = LISAPP.APP_C.Collfn.FindInfectionInfoD(msRegNo)

                '-- 공여자정보 (국립의료원인 경우 예약일자)
                Me.lblDonor.Text = dt.Rows(0).Item("resdt").ToString()

                Me.txtRemark.Text = dt.Rows(0).Item("doctorrmk").ToString().Trim
                'Ctrl.Set_ToolTip(Me.txtRemark, Me.txtRemark.Text, m_tooltip)

                Return True
            End If
        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message)
        End Try

    End Function

    Public Function fnDisplay_Data(ByVal rsRegNo As String, ByVal rsOrdDt As String) As Boolean
        ' 정은생성 
        Dim sFn As String = "Public Sub sbDisplay_Data()"

        Try
            Dim dtSysDate As Date = Fn.GetServerDateTime()

            ' 결과화면 공통 AxRstPatInfo_new 환자정보조회 
            Dim dt As DataTable = OCSAPP.OcsLink.Ord.fnGet_Coll_PatList_RegNo(rsRegNo, rsOrdDt, rsOrdDt, "", "4", "")

            If dt.Rows.Count < 1 Then
                Return False
            Else
                Dim sPatInfo() = dt.Rows(0).Item("patinfo").ToString.Split("|"c)  ' 환자정보들 
                '< 나이계산
                Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                '>

                msRegNo = dt.Rows(0).Item("regno").ToString.Trim

                Me.lblRegNo.Text = dt.Rows(0).Item("regno").ToString().Trim      ' 등록번호 
                Me.lblPatNm.Text = sPatInfo(0).Trim      ' 환자명 
                Me.lblSexAge.Text = sPatInfo(1).Trim.Trim + "/" + iAge.ToString    ' 성별/나이 
                Me.lblHeight.Text = ""   ' 키
                Me.lblWeight.Text = ""   ' 몸무게 
                Me.lblInjong.Text = sPatInfo(8).Trim                             ' 인종 
                Me.lblAbo.Text = LISAPP.APP_C.Collfn.FindAboRhInfo(msRegNo)       ' 혈액형
                Me.lblIdNo.Text = sPatInfo(3).Trim    ' 주민등록번호 


                Me.lblOrdDt.Text = rsOrdDt         ' 처방일시 
                Me.lblDeptCd.Text = dt.Rows(0).Item("deptcd").ToString().Trim        ' 진료과코드 
                Me.lblDeptCd.Tag = dt.Rows(0).Item("deptnm").ToString().Trim         ' 진료과명
                Me.lblDoctorNm.Text = dt.Rows(0).Item("doctornm").ToString().Trim    ' 의사명 
                Me.lblGenDr.Text = ""
                Me.lblEntDt.Text = dt.Rows(0).Item("ibday").ToString().Trim          ' 입원일 
                Me.lblWardCd.Text = dt.Rows(0).Item("wardno").ToString().Trim        ' 병동 
                Me.lblRoomNo.Text = dt.Rows(0).Item("roomno").ToString().Trim        ' 병실 
                Me.txtDiagNm.Text = ""        ' 진단명 


                Me.txtBcNo.Text = ""     ' 검체번호 
                Me.txtWkNo.Text = ""     ' 작업번호
                Me.txtPrtBcNo.Text = ""  ' 바코드번호 
                Me.lblSpcNmd.Text = ""   ' 검체명

                '-- 응급 
                Me.lblER.Text = ""

                '-- 감염정보
                Me.lblInf.Text = LISAPP.APP_C.Collfn.FindInfectionInfoD(msRegNo)

                '-- 공여자정보 
                Me.lblDonor.Text = ""

                Me.txtRemark.Text = ""
                Ctrl.Set_ToolTip(Me.txtRemark, Me.txtRemark.Text, m_tooltip)

                Return True
            End If
        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message)
        End Try

    End Function

    Private Sub btnDrug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrug.Click
        Dim frmDrug As New FGDRUG
        Dim sOrdDt As String = ""

        '''frmDrug.RegNo = Ctrl.Get_Code(spdPatInfo, "regno", 1, False)
        frmDrug.RegNo = lblRegNo.Text
        If msBcNo <> "" Then  ''' 바코드 없을때 안타게 

            frmDrug.SLIPCD = msSlipCd
            sOrdDt = Format(Now, "yyyy-MM-dd").ToString
            frmDrug.OrdDtS = Format(DateAdd(DateInterval.Year, -1, CDate(sOrdDt)), "yyyyMMdd").ToString
            frmDrug.OrdDtE = sOrdDt.Replace("-", "")

            frmDrug.sbDisplay_Data()
        End If

    End Sub


    Private Sub btnPatInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatInfo.Click
        Try
            '환자정보 조회 기능 권한
            If USER_SKILL.Authority("R01", 7) = False Then Return
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_Current(msRegNo)
            If dt.Rows.Count = 0 Then
                MsgBox("OCS에서 환자정보를 찾을 수 없습니다!!", MsgBoxStyle.Information)

                Return
            End If

            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnPatInfo)
            Dim iTop As Integer = Ctrl.menuHeight + Ctrl.FindControlTop(Me.btnPatInfo) + Me.btnPatInfo.Height

            Dim patinfo As New PATINFO

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
                .InWonDate = dt.Rows(0).Item("entdt").ToString + "/" + dt.Rows(0).Item("entdt_to").ToString

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
        Clipboard.SetText(Me.lblRegNo.Text)
    End Sub

    Private Sub txtRemark_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRemark.KeyDown
        Return
    End Sub

  
End Class
