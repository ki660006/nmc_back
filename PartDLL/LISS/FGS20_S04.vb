Imports COMMON.CommFN
Imports COMMON.SVar
Imports LISAPP.APP_S.RstSrh

Public Class FGS20_S04

    Public Function DisplayForm(ByVal rarrRefList As ArrayList) As String
        Try


            Dim sRetval As String = ""


            Me.txtHostDoct.Text = CType(rarrRefList.Item(0), REFLIST).RHospiUsr

            Dim sPatnm As String = CType(rarrRefList.Item(0), REFLIST).SpcName
            Dim sRegno As String = CType(rarrRefList.Item(0), REFLIST).SpcRegno
            Dim sSex As String = CType(rarrRefList.Item(0), REFLIST).SpcSex
            Dim sDept As String = CType(rarrRefList.Item(0), REFLIST).SpcDept
            Dim sBirth As String = CType(rarrRefList.Item(0), REFLIST).SpcBirTh

            Me.txtRegnm.Text = CType(rarrRefList.Item(0), REFLIST).SpcName
            Me.txtRegno.Text = CType(rarrRefList.Item(0), REFLIST).SpcRegno
            Me.cboSex.SelectedIndex = CInt(IIf(CType(rarrRefList.Item(0), REFLIST).SpcSex = "남", 1, 2))
            Me.txtDept.Text = CType(rarrRefList.Item(0), REFLIST).SpcDept
            Me.txtYear.Text = CType(rarrRefList.Item(0), REFLIST).SpcBirTh.Substring(0, 4)
            Me.txtMonth.Text = CType(rarrRefList.Item(0), REFLIST).SpcBirTh.Substring(4, 2)
            Me.txtDay.Text = CType(rarrRefList.Item(0), REFLIST).SpcBirTh.Substring(6, 2)

            Dim sSpc As String = CType(rarrRefList.Item(0), REFLIST).Spc
            If sSpc = "11" Then '혈액
                Me.chkspc1.Checked = True
            ElseIf sSpc = "12" Then '대변
                Me.chkspc2.Checked = True
            ElseIf sSpc = "13" Then '인두도말
                Me.chkspc3.Checked = True
            ElseIf sSpc = "14" Then '뇌척수액
                Me.chkspc4.Checked = True
            ElseIf sSpc = "15" Then '가래
                Me.chkspc5.Checked = True
            ElseIf sSpc = "99" Then '기타
                Me.chkSpcEtc.Checked = True
            End If

            Dim sSpcTest As String = CType(rarrRefList.Item(0), REFLIST).Spcetc
            Me.txtSpcEtc.Text = sSpcTest

            Dim sTest As String = CType(rarrRefList.Item(0), REFLIST).Test
            If sTest = "01" Then '배양검사
                Me.chktest1.Checked = True
            ElseIf sTest = "02" Then '유전자 검출검사
                Me.chktest2.Checked = True
            ElseIf sTest = "03" Then '항체항원 검출검사
                Me.chktest3.Checked = True
            ElseIf sTest = "04" Then '신속진단키트
                Me.chktest4.Checked = True
            ElseIf sTest = "05" Then '현미경검사
                Me.chktest5.Checked = True
            ElseIf sTest = "99" Then '기타
                Me.chkTestEtc.Checked = True
            End If

            Dim sTestEtc As String = CType(rarrRefList.Item(0), REFLIST).Testetc
            Me.txtTestEtc.Text = sTestEtc

            If CType(rarrRefList.Item(0), REFLIST).Groupcd = "" Then
                Me.cboRef1.SelectedIndex = 0
            Else
                Me.cboRef1.SelectedIndex = CInt(CType(rarrRefList.Item(0), REFLIST).Groupcd)
            End If


            Dim sRefcd As String = CType(rarrRefList.Item(0), REFLIST).Refcd

            Me.cboRef2.SelectedIndex = Me.cboRef2.FindString("[" + sRefcd + "]")




            'Me.cboRef2.SelectedText = sRefcd
            'Me.cboRef2.SelectedValue = sRefcd
            'Me.cboRef2.Text = sRefcd

            Dim sTkdt As String = CType(rarrRefList.Item(0), REFLIST).Tkdt

            Me.txtInfTYear.Text = CType(rarrRefList.Item(0), REFLIST).Tkdt.Substring(0, 4)
            Me.txtInfTMon.Text = CType(rarrRefList.Item(0), REFLIST).Tkdt.Substring(4, 2)
            Me.txtInfTDay.Text = CType(rarrRefList.Item(0), REFLIST).Tkdt.Substring(6, 2)

            Me.txtInfRYear.Text = CType(rarrRefList.Item(0), REFLIST).fndt.Substring(0, 4)
            Me.txtInfRMon.Text = CType(rarrRefList.Item(0), REFLIST).fndt.Substring(4, 2)
            Me.txtInfRDay.Text = CType(rarrRefList.Item(0), REFLIST).fndt.Substring(6, 2)

            Dim sNow As String = CStr((New LISAPP.APP_DB.ServerDateTime).GetDate("-")).Replace("-", "")

            Me.txtInfFYear.Text = sNow.Substring(0, 4)
            Me.txtInfFMon.Text = sNow.Substring(4, 2)
            Me.txtInfFDay.Text = sNow.Substring(6, 2)

            Me.txtTestUsr.Text = CType(rarrRefList.Item(0), REFLIST).TestUsr ' 수신자
            Me.txtRptUsr.Text = CType(rarrRefList.Item(0), REFLIST).RptUsr ' 보고자(수신자)

            Me.txtBcno.Text = CType(rarrRefList.Item(0), REFLIST).Bcno

            Me.ShowDialog()

            Return sRetval
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function fnDisplayResult() As String
        Try
            Me.ShowDialog()

            Return ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Function


    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Private Sub FGS20_S04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.cboSex.SelectedIndex = 0
    End Sub

    Private Sub cboRef1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRef1.SelectedIndexChanged
        Dim st As String = Me.cboRef2.SelectedIndex.ToString

        Dim dt As DataTable = fnRefList((Me.cboRef1.SelectedIndex).ToString)

        Me.cboRef2.Items.Clear()

        'Return
        Me.cboRef2.Items.Add("선택하세요")

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboRef2.Items.Add("[" + dt.Rows(ix).Item("refcd").ToString + "] " + dt.Rows(ix).Item("refnm").ToString)
        Next

        Me.cboRef2.SelectedIndex = 0
    End Sub

    Private Sub btnClose_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ClickButtonArea
        Me.Close()
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click

        Try

            Dim objRef As New REFLIST

            Dim arrRefinfo As New ArrayList

            Dim sRHospiCd As String = "", sRHospiNm As String = "", sRHospiUsr As String = ""
            Dim sSpcName As String = "", sSpcSex As String = "", sSpcBirth As String = "", sSpcRegno As String = "", sSpcDept As String = ""
            Dim sSpcSpc As String = "", sSpcSpcetc As String = "", sTest As String = "", sTestetc As String = "", sRefcd As String = ""
            Dim sTkdt As String = ""
            Dim sfndt As String = ""
            Dim sTestUsr As String = "", sBcno As String = "", sRptUsr As String = ""

            Dim iCnt As Integer = 0

            Dim iRet As Boolean = False


            sRHospiCd = Me.txtRHospiCd.Text '1 병원기호
            sRHospiNm = Me.txtRHospiNm.Text '2  의뢰병원 명
            sRHospiUsr = Me.txtTestUsr.Text '3 의뢰의사 명
            sSpcName = Me.txtRegnm.Text '4 검체:환자명

            Dim sSex As String = ""
            If Me.cboSex.SelectedIndex = 0 Then
                MsgBox("성별을 선택해 주세요")
                Return
            ElseIf Me.cboSex.SelectedIndex = 1 Then '<<<20170906 개별등록에서 성별 잘못 가져가는 부분 수정 
                sSex = "M"
            ElseIf Me.cboSex.SelectedIndex = 2 Then
                sSex = "F"
            End If
            sSpcSex = sSex '5 검체:환자성별

            sSpcBirth = Me.txtYear.Text + Me.txtMonth.Text + Me.txtDay.Text  '6 검체:환자생일
            sSpcRegno = Me.txtRegnm.Text '7 검체:환자번호
            sSpcDept = Me.txtDept.Text '8 검체:환자 과

            Dim sSpccd As String = ""
            If Me.chkspc1.Checked Then '혈액
                sSpccd = "11"
            ElseIf Me.chkspc2.Checked Then '대변
                sSpccd = "12"
            ElseIf Me.chkspc3.Checked Then '인두도말
                sSpccd = "13"
            ElseIf Me.chkspc4.Checked Then '뇌척수액
                sSpccd = "14"
            ElseIf Me.chkspc5.Checked Then '가래
                sSpccd = "15"
            ElseIf Me.chkSpcEtc.Checked Then '기타
                sSpccd = "99"
            End If
            sSpcSpc = sSpccd '9 검체:검체명 

            If sSpcSpc = "" Then
                MsgBox("검체종류코드는 필수 입력사항입니다.")
                Return
            ElseIf IsNumeric(sSpcSpc) = False Then
                MsgBox("검체종류코드는 코드로 입력 바랍니다.")
                Return
            End If

            sSpcSpcetc = Me.txtSpcEtc.Text '10 검체기타 텍스트

            If Me.chktest1.Checked Then '11 검사방법
                sTest = "01"
            ElseIf Me.chktest2.Checked Then
                sTest = "02"
            ElseIf Me.chktest3.Checked Then
                sTest = "03"
            ElseIf Me.chktest4.Checked Then
                sTest = "04"
            ElseIf Me.chktest5.Checked Then
                sTest = "05"
            ElseIf Me.chkTestEtc.Checked Then
                sTest = "99"
            End If

            If sTest = "" Then '11 검사방법
                MsgBox("검사방법코드는 필수 입력사항입니다.")
                Return
            ElseIf IsNumeric(sTest) = False Then
                MsgBox("검체종류코드는 코드로 입력 바랍니다.")
                Return
            End If

            sTestetc = Me.txtTestEtc.Text '12 검사방법: 기타 텍스트

            If Me.cboRef2.Text <> "" Then
                Dim i As Integer = Me.cboRef2.Text.IndexOf("[")
                If Me.cboRef2.Text.IndexOf("[") > -1 Then
                    sRefcd = Ctrl.Get_Code(Me.cboRef2) '13 병원체코드 
                Else
                    sRefcd = Me.cboRef2.Text
                End If
            Else
                MsgBox("병원체코드는 필수 입력사항입니다")
                Return
            End If

            sTkdt = txtInfTYear.Text + txtInfTMon.Text + txtInfTDay.Text  '14 검체의뢰일
            sfndt = txtInfRYear.Text + txtInfRMon.Text + txtInfRDay.Text  '15 보고일자
            sTestUsr = txtTestUsr.Text '16 검사자 
            sRptUsr = txtRptUsr.Text '보고자
            sBcno = txtBcno.Text


            With objRef
                .RHospiCd = sRHospiCd
                .RHospiNm = sRHospiNm
                .RHospiUsr = sRHospiUsr
                .SpcName = sSpcName
                .SpcSex = sSpcSex
                .SpcBirTh = sSpcBirth
                .SpcRegno = sSpcRegno
                .SpcDept = sSpcDept
                .Spc = sSpcSpc
                .Spcetc = sSpcSpcetc
                .Test = sTest
                .Testetc = sTestetc
                .Refcd = sRefcd
                .TestUsr = sTestUsr
                .Tkdt = sTkdt
                .fndt = sfndt
                .RptUsr = sRptUsr

            End With

            arrRefinfo.Add(objRef)

            'sURL += "&rm_info=" '16) 비고정보
            'sURL += "&hsptl_swbser=" '17) 병원 소프트웨어 개발사 (사업자)
            'sURL += "&hsptl_swknd=" '18) 병원 소프트웨어 종류 (버전)
            'sURL += "&dplct_at=0" '19)중복여부 test시에는 0으로 보낼것 [필]
            'sURL += "&rspns_mssage_ty=0" '20) 응답 형식 0 :xml , 1:json [필]


            'URL 전송 
            Dim sRetVal As String = (New WEBSERVER.CGWEB_S).fnRegWebServer_for_KCDC(arrRefinfo)
            'URL 과 return 값을 받음 
            Dim sSaveResult As String() = sRetVal.Split(Chr(124))

            'Dim sUrl As String = sSaveResult(0) 'URL
            'Dim sRtnVal As String = sSaveResult(1) 'return 값

            ''전송 결과 처리 
            'Dim sRtn As String = fnGetResultvalue(sRtnVal)

            '<< JJH 바이트수 제한
            Dim sUrl As String = COMMON.CommFN.Fn.CHK_LENGTHB(sSaveResult(0), 2000) 'URL
            Dim sRtnVal As String = COMMON.CommFN.Fn.CHK_LENGTHB(sSaveResult(1), 2000) 'return 값

            ''전송 결과 처리 
            Dim sRtn As String = COMMON.CommFN.Fn.CHK_LENGTHB(fnGetResultvalue(sRtnVal), 1000)
            '>>

            '전송한 url과 결과 insert 
            Dim objRst As New LISAPP.APP_R.AxRstFn
            Dim iret2 As Boolean = objRst.fnIns_LR080M(sBcno, sRtn.Split("|"c)(0), sUrl, sRtnVal, CStr(IIf(sRtn.Split("|"c)(0) = "Y", "", sRtn)), sSpcSpc, sSpcSpcetc, sTest, sTestetc, sRefcd, sRptUsr)

            '전송상태 표시 

            If sRtn.Split("|"c)(0) = "Y" Then
                MsgBox("등록성공")
                Me.Close()
                'sbDisplay_Data()
            Else
                MsgBox("등록실패")

                Return
            End If

            Return

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function fnGetResultvalue(ByVal rsResult As String) As String
        Try
            Dim sRtn As String = ""

            Dim iPosMsg As Integer = rsResult.IndexOf("message")
            Dim iPosCddt As Integer = rsResult.IndexOf("code_dt")
            Dim iPosStat As Integer = rsResult.IndexOf("stat")

            Dim sMsg As String = ""
            Dim sCddt As String = ""
            Dim sStat As String = ""

            'rsResult.IndexOf("")

            sMsg = rsResult.Substring(iPosMsg).Replace("", "|").Replace("", "^")
            sCddt = rsResult.Substring(iPosCddt).Replace("", "|").Replace("", "^")
            sStat = rsResult.Substring(iPosStat).Replace("", "|").Replace("", "^")

            sMsg = sMsg.Split("^"c)(0).Split("|"c)(1)
            sCddt = sCddt.Split("^"c)(0).Split("|"c)(1)
            sStat = sStat.Split("^"c)(0).Split("|"c)(1)


            If sCddt = "2001" Then
                sRtn = "Y" + "|" + sCddt + "|" + sMsg + "|" + sStat
            Else
                sRtn = "N" + "|" + sCddt + "|" + sMsg + "|" + sStat
            End If

            Return sRtn

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
End Class