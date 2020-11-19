'>> CVS 입고
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT
Imports LISAPP.APP_BT.CGDA_BT

Public Class FGB05_S01

    Private m_aI_BldInfo As New ArrayList
    Private msUserId As String = ""
    Private mbSave As Boolean = False

    Public Function Display_Result(ByVal rsFileNm As String, ByVal rsUsrId As String) As ArrayList
        Dim sFn As String = "Sub Display_Data(string)"

        msUserId = rsUsrId

        Dim strLine As String = ""
        Dim sr As New System.IO.StreamReader(rsFileNm, System.Text.Encoding.GetEncoding("euc-kr"))

        Try
            spdList.MaxRows = 0

            Do
                strLine = sr.ReadLine()

                If strLine Is Nothing Then Exit Do

                strLine = strLine.Replace(",""", Chr(3)).Replace(""",""", Chr(3)).Replace(""",", Chr(3)).Replace("""", "")

                If strLine.ToLower.StartsWith("no") Then ' first line  ->  필요없음
                    strLine = ""
                Else
                    If Not strLine.Equals("") Then

                        Dim blnInYn As Boolean = True

                        Dim strComCd As String = "", strComNmd As String = ""
                        Dim strDonQnt As String = ""
                        Dim intAvailMi As Integer  ' 성분제제별 유효기간
                        Dim dtAvailMi As Date
                         Dim dtcreatlMi As Date '제제일적용 유효기간

                        Dim strBuf() As String = Split(strLine, Chr(3))
                        Dim strBldInfo() As String
                        If strBuf.Length >= 14 Then '2019/02 yjy수정 CSV파일 컬럼 수가 14를 넘어가는 경우가 있으므로 부등호 추가
                            strBldInfo = strBuf
                        Else
                            strBldInfo = Split(strBuf(0), ","c)
                        End If

                        With spdList

                            If InStr(strBldInfo(8), ":") = 2 Then strBldInfo(8) = "0" + strBldInfo(8)
                            strBldInfo(4) = strBldInfo(4).PadLeft(5, "0"c)  '-- 혈액코드

                            Dim iAboRh As Integer = InStr(1, strBldInfo(11), "(")
                            Dim strAbo As String = strBldInfo(11).Substring(0, iAboRh - 1).Trim
                            Dim strRh As String = strBldInfo(11).Substring(iAboRh, 1)
                            Dim strDonDt As String = strBldInfo(7) + " " + strBldInfo(8) + ":00"
                            Dim strCreatDt As String = strBldInfo(9) + " " + strBldInfo(10) + ":00"

                            Dim strEtc As String
                            If strBuf.Length = 14 Then
                                strEtc = "공급날짜 : " + strBldInfo(1) + " " + strBldInfo(2) & vbCrLf + _
                                                       "단가 : " + strBldInfo(12) + vbCrLf + _
                                                       "출고인 : " + strBldInfo(13)

                            Else
                                strEtc = "공급날짜 : " + strBldInfo(1) + " " + strBldInfo(2) & vbCrLf + _
                                                       "단가 : " + strBuf(1) + vbCrLf + _
                                                       "출고인 : " + strBuf(2)

                            End If

                            .MaxRows += 1

                            .Row = .MaxRows
                            .Col = .GetColFromID("bldcd") : .Text = strBldInfo(4)  '-- 혈액코드
                            .Col = .GetColFromID("dondt") : .Text = strDonDt        '-- 채혈일
                            .Col = .GetColFromID("bldnm") : .Text = strBldInfo(6) '-- 혈액번호
                            .Col = .GetColFromID("comnmd") : .Text = strBldInfo(3)
                            .Col = .GetColFromID("abo") : .Text = strAbo
                            .Col = .GetColFromID("rh") : .Text = strRh
                            .Col = .GetColFromID("cmt") : .Text = strEtc
                            .Col = .GetColFromID("donqnt") : .Text = strBldInfo(5)
                            .Col = .GetColFromID("createdt") : .Text = strBldInfo(7)

                            ' 이미 입고된 혈액원 파일인지 알아보기!!!
                            Dim objDTable As DataTable = CGDA_BT.fnGet_BldProv(strBldInfo(6).Replace("-", ""), strBldInfo(4))

                            If objDTable.Rows.Count > 0 Then ' 기존에 입고된 혈액임!
                                'MsgBox("이미 입고된 혈액입니다. 다른 파일을 선택하세요", MsgBoxStyle.Information, Me.Text)
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            End If

                            If IsArray(strBldInfo) Then
                                Dim dt As DataTable = CGDA_BT.fnGet_ComCd(strBldInfo(4))      ' 혈액코드
                                If dt.Rows.Count > 0 Then
                                    strComCd = dt.Rows(0).Item("COMCD").ToString()     ' 성분제제코드
                                    strComNmd = dt.Rows(0).Item("COMNMD").ToString()   ' 성분제제명

                                    intAvailMi = CType(dt.Rows(0).Item("AVAILMI").ToString(), Integer)    ' 유효기간
                                    dtAvailMi = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Minute, intAvailMi, CType(strDonDt, Date))) ' 유효기간
                                    'dtAvailMi = DateAdd(DateInterval.Minute, intAvailMi, CType(strDonDt, Date)) ' 유효기간
                                    dtcreatlMi = DateAdd(DateInterval.Minute, intAvailMi, CType(strCreatDt, Date)) ' 제제일 혈소판 유효기간

                                    .Col = .GetColFromID("comcd") : .Text = strComCd
                                    .Col = .GetColFromID("comnmd") : .Text = strComNmd

                                    If dt.Rows(0).Item("platyn").ToString = "Y" Then '<20130410 ymg 혈소판 제제일시기준 유효기간 추가 
                                        .Col = .GetColFromID("availdt") : .Text = Format(dtcreatlMi, "yyyy-MM-dd HH:mm:ss").ToString
                                    Else
                                        .Col = .GetColFromID("availdt") : .Text = Format(dtAvailMi, "yyyy-MM-dd").ToString + " 23:59:59"
                                    End If

                                    '.Col = .GetColFromID("availdt") : .Text = Format(dtAvailMi, "yyyy-MM-dd").ToString + " 23:59:59"

                                Else
                                    fn_PopMsg(Me, "I"c, "혈액코드[" + strBldInfo(4) + "] 는 존재하지 않습니다.")
                                    .Col = .GetColFromID("chk")
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                End If
                            Else
                                .Col = .GetColFromID("chk")
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                                fn_PopMsg(Me, "I"c, "값이 제대로 입력되지 못했습니다.")
                                Exit Do
                            End If
                        End With
                    End If
                End If
            Loop Until strLine Is Nothing
            sr.Close()  ' sream reader와 원본 스트림을 닫고 reader와 관련된 모든 시스템 리소스를 해제

            Me.ShowDialog()

            If mbSave Then
                Return m_aI_BldInfo
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Function

    Private Sub txtBType_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBldNm.GotFocus, txtBldQnt.GotFocus
        CType(sender, Windows.Forms.TextBox).SelectAll()
    End Sub

    Private Sub txtBldQnt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBldQnt.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim objDTable As DataTable = BldIn.fnGet_BldCdToComcd(txtBldQnt.Text)
            If objDTable.Rows.Count > 0 Then
                lblComNmd.Text = objDTable.Rows(0).Item("donqnt").ToString

                With spdList
                    For intRow As Integer = 1 To .MaxRows
                        .Row = intRow
                        .Col = .GetColFromID("comcd") : Dim strComCd As String = .Text
                        .Col = .GetColFromID("bldnm") : Dim strbldnm As String = .Text.Replace("-", "")

                        If strComCd = objDTable.Rows(0).Item("comcd").ToString And strbldnm = txtBldNm.Text Then
                            .Row = intRow
                            .Col = .GetColFromID("chk")
                            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .Text = "1"
                        End If
                    Next
                End With
            End If

            txtBldNm.Focus()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGB05_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)

        End Select
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sFn As String = "Sub Display_Data(string)"

        Try
            m_aI_BldInfo.Clear()

            With Me.spdList
                For iRow As Integer = 1 To .MaxRows
                    Dim stuBld As New STU_BldInfo

                    .Row = iRow
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                    .Col = .GetColFromID("bldnm") : stuBld.Bldno_Full = .Text
                    .Col = .GetColFromID("bldnm") : stuBld.BldNo = .Text.Replace("-", "")
                    .Col = .GetColFromID("comcd") : stuBld.ComCd = .Text
                    .Col = .GetColFromID("abo") : stuBld.Abo = .Text
                    .Col = .GetColFromID("rh") : stuBld.Rh = .Text
                    .Col = .GetColFromID("availdt") : stuBld.AvailDt = .Text
                    .Col = .GetColFromID("dondt") : stuBld.DonDt = .Text
                    .Col = .GetColFromID("donqnt") : stuBld.DonQnt = IIf(.Text = "320", "1", "0").ToString
                    .Col = .GetColFromID("comnmd") : stuBld.ComNmd = .Text
                    .Col = .GetColFromID("cmt") : stuBld.Cmt = .Text

                    stuBld.RegNo = ""
                    stuBld.DonGbn = "0"
                    stuBld.InDt = ""
                    stuBld.InPlace = "0"

                    If sChk = "1" And stuBld.BldNo.Length = 10 Then
                        m_aI_BldInfo.Add(stuBld)
                    ElseIf stuBld.BldNo.Length <> 10 Then
                        fn_PopMsg(Me, "I"c, "혈액번호[" + stuBld.BldNo + "]는 잘못된 혈액번호 입니다.")
                    End If
                Next

                If BldIn.fnExe_BldIn(m_aI_BldInfo) = True Then
                    fn_PopMsg(Me, "I"c, "정상적으로 입고되었습니다")
                    mbSave = True
                    Me.Close()
                Else
                    fn_PopMsg(Me, "I"c, "입고되지 못했습니다. 다시 시도하세요")
                    m_aI_BldInfo.Clear()
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtBldNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBldNm.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If txtBldNm.Text = "" Then
            txtBldNm.SelectAll()
            txtBldNm.Focus()
        Else
            txtBldQnt.Focus()
        End If
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        If e.col = 1 And e.row = 0 And msUserId = "ACK" Then
            With spdList
                .Row = 1 : .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                        .Text = IIf(strChk = "1", "", "1").ToString
                    End If
                Next
            End With
        End If
    End Sub

    Private Sub txtBldNm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBldNm.Click
        txtBldNm.SelectAll()
    End Sub

    Private Sub FGB05_S01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_SpreadDesige.sbInti(spdList)
    End Sub

    Private Sub spdList_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdList.KeyDownEvent
        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        With spdList
            If .ActiveCol <> .GetColFromID("cmt") Then Return

            Dim iRow As Integer = .ActiveRow
            Dim iCol As Integer = .ActiveCol

            .Row = iRow : .Col = iCol : Dim sValue As String = .Text

            If IsNumeric(sValue) = False Then
                MsgBox("수치값만 입력 가능합니다.!!")
                .Row = iRow : .Col = iCol : .Text = ""
                Return
            End If

            If MsgBox("일괄 적용하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "혈액비고") = MsgBoxResult.No Then Return

            Dim iSeq As Integer = Convert.ToInt16(sValue)

            For ix As Integer = iRow + 1 To .MaxRows

                iSeq += 1

                .Row = ix
                .Col = .GetColFromID("cmt") : .Text = iSeq.ToString + "," + .Text
            Next

        End With
    End Sub

    '<20151026  전체체크기능 추가 
    Private Sub chkall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkall.CheckedChanged
        If Me.chkall.Checked = True Then
            For icnt As Integer = 1 To Me.spdList.MaxRows

                With Me.spdList
                    .Row = icnt
                    .Col = .GetColFromID("chk")
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .Text = "1"
                End With
            Next
        Else
            For icnt As Integer = 1 To Me.spdList.MaxRows
                With Me.spdList
                    .Row = icnt
                    .Col = .GetColFromID("chk")
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .Text = "0"
                End With
            Next
        End If
    End Sub
End Class