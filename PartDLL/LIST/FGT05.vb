'>> 미생물 통계
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports common.commlogin.login
Imports LISAPP.APP_T

Public Class FGT05
    Inherits System.Windows.Forms.Form

    Private Const mi_Analysis_Or_Reanalysis As Integer = 1

    Private miSelectKey As Integer = 0
    Private miMaxDiffDay As Integer = 100
    Private miMaxDiffMonth As Integer = 24
    Private miMaxDiffYear As Integer = 2

    Private sDir As String = Application.StartupPath + "\XML"
    Private XmlFile As String = sDir + "\FGT05_Setting.Xml"

    Friend WithEvents rdoYear As System.Windows.Forms.RadioButton
    Friend WithEvents pnlSlip As System.Windows.Forms.Panel
    Friend WithEvents rdoBacGenS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBacGenA As System.Windows.Forms.RadioButton
    Friend WithEvents spdBac As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rdoBacS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBacA As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents pnlAntiRst As System.Windows.Forms.Panel
    Friend WithEvents chkAntiR As System.Windows.Forms.CheckBox
    Friend WithEvents chkAntiI As System.Windows.Forms.CheckBox
    Friend WithEvents chkAntiS As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents split1 As System.Windows.Forms.Splitter
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents spdStatistics As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblColor As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents rdoIOC As System.Windows.Forms.RadioButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnSearch As CButtonLib.CButton
    Friend WithEvents btnAnalysis As CButtonLib.CButton
    Friend WithEvents chkSameCd As System.Windows.Forms.CheckBox
    Friend WithEvents rdoOptDT2 As System.Windows.Forms.RadioButton
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdspc As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnSave As CButtonLib.CButton
    Private m_fgt05_anal As New FGT05_ANALSVR
    Private Sub sbDisplay_List(ByVal rsStGbn As String, ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then Return

        Dim sKey As String = ""
        Dim lngCnt1 As Long = 0
        Dim lngCnt2 As Long = 0
        Dim iCol As Integer = 0

        Dim alKeys As New ArrayList

        With Me.spdStatistics
            .ReDraw = False

            For i As Integer = 0 To r_dt.Rows.Count - 1

                sKey = r_dt.Rows(i).Item("code1").ToString + "/" + r_dt.Rows(i).Item("code2").ToString
                If alKeys.Contains(sKey) = False Then
                    alKeys.Add(sKey)

                    If .MaxRows > 0 Then
                        iCol = .GetColFromID("total")
                        If iCol > 0 Then
                            .Row = .MaxRows
                            .Col = iCol + 0 : .Text = lngCnt1.ToString
                            .Col = iCol + 1 : .Text = lngCnt2.ToString
                            .Col = iCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.00")
                        End If
                    End If

                    .MaxRows += 1

                    .Row = .MaxRows
                    .Col = .GetColFromID("code1".ToLower) : .Text = r_dt.Rows(i).Item("code1").ToString
                    .Col = .GetColFromID("name1".ToLower) : .Text = r_dt.Rows(i).Item("name1").ToString
                    .Col = .GetColFromID("code2".ToLower) : .Text = r_dt.Rows(i).Item("code2").ToString
                    .Col = .GetColFromID("name2".ToLower) : .Text = r_dt.Rows(i).Item("name2").ToString

                    lngCnt1 = 0
                    lngCnt2 = 0
                End If

                Dim strDays As String = ""
                strDays = r_dt.Rows(i).Item("days").ToString
                If strDays.Length = 8 Then
                    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2) + "-" + strDays.Substring(6, 2)
                ElseIf strDays.Length = 6 Then
                    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2)
                End If

                iCol = .GetColFromID(strDays)
                If iCol > 0 Then
                    .Row = .MaxRows
                    .Col = iCol + 0 : .Text = r_dt.Rows(i).Item("cnt1").ToString
                    .Col = iCol + 1 : .Text = r_dt.Rows(i).Item("cnt2").ToString
                    .Col = iCol + 2 : .Text = Format(Convert.ToDouble(r_dt.Rows(i).Item("cnt3").ToString), "0.00")

                    Dim strTmp As String = ""
                    strTmp = r_dt.Rows(i).Item("cnt3").ToString

                    lngCnt1 += Convert.ToInt32(r_dt.Rows(i).Item("cnt1").ToString)
                    lngCnt2 += Convert.ToInt32(r_dt.Rows(i).Item("cnt2").ToString)
                End If
            Next

            iCol = .GetColFromID("total")
            If iCol > 0 Then
                .Row = .MaxRows
                .Col = iCol + 0 : .Text = lngCnt1.ToString
                .Col = iCol + 1 : .Text = lngCnt2.ToString
                .Col = iCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.00")
            End If

            If rsStGbn = "01" Or rsStGbn = "02" Then
            Else
                .MaxRows += 1
                .Row = .MaxRows
                If .GetColFromID("name2".ToLower) > 0 Then
                    .Col = .GetColFromID("name2".ToLower) : .Text = "Total"
                End If

                For intIdx As Integer = .GetColFromID("name2") + 1 To .MaxCols Step 3
                    Dim lngRstCnt As Long = 0
                    Dim lngModCnt As Long = 0

                    For intRow As Integer = 1 To .MaxRows - 1
                        Dim strTmp As String

                        .Row = intRow
                        .Col = intIdx + 0 : strTmp = .Text
                        If strTmp = "" Then strTmp = "0"
                        lngRstCnt += Convert.ToInt32(strTmp)

                        .Col = intIdx + 1 : strTmp = .Text
                        If strTmp = "" Then strTmp = "0"
                        lngModCnt += Convert.ToInt32(strTmp)
                    Next

                    .Row = .MaxRows
                    .Col = intIdx + 0 : .Text = lngRstCnt.ToString
                    .Col = intIdx + 1 : .Text = lngModCnt.ToString

                    If lngRstCnt > 0 Then
                        .Col = intIdx + 2 : .Text = Format((lngModCnt / lngRstCnt) * 100, "0.00")
                    Else
                        .Col = intIdx + 2 : .Text = Format(0, "0.00")
                    End If

                Next
            End If

            .ReDraw = True
        End With

    End Sub

    Private Sub sbDisplay_List_Growth(ByVal rsStGbn As String, ByVal r_dt As DataTable, ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String)
        If r_dt.Rows.Count < 1 Then Return

        Dim sKey As String = ""
        Dim lngCnt1 As Long = 0
        Dim lngCnt2 As Long = 0
        Dim iCol As Integer = 0

        Dim alKeys As New ArrayList

        With Me.spdStatistics
            .ReDraw = False

            For i As Integer = 0 To r_dt.Rows.Count - 1

                sKey = r_dt.Rows(i).Item("code1").ToString + "/" + r_dt.Rows(i).Item("code2").ToString
                If alKeys.Contains(sKey) = False Then
                    alKeys.Add(sKey)

                    If .MaxRows > 0 Then
                        iCol = .GetColFromID("total")
                        If iCol > 0 Then
                            .Row = .MaxRows
                            '.Col = iCol + 0 : .Text = lngCnt1.ToString
                            .Col = iCol + 1 : .Text = lngCnt2.ToString
                            ' .Col = iCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.00")
                        End If
                    End If

                    .MaxRows += 1

                    .Row = .MaxRows
                    .Col = .GetColFromID("code1".ToLower) : .Text = r_dt.Rows(i).Item("code1").ToString
                    .Col = .GetColFromID("name1".ToLower) : .Text = r_dt.Rows(i).Item("name1").ToString
                    .Col = .GetColFromID("code2".ToLower) : .Text = r_dt.Rows(i).Item("code2").ToString
                    .Col = .GetColFromID("name2".ToLower) : .Text = r_dt.Rows(i).Item("name2").ToString

                    lngCnt1 = 0
                    lngCnt2 = 0
                End If

                Dim strDays As String = ""
                strDays = r_dt.Rows(i).Item("days").ToString
                If strDays.Length = 8 Then
                    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2) + "-" + strDays.Substring(6, 2)
                ElseIf strDays.Length = 6 Then
                    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2)
                End If

                iCol = .GetColFromID(strDays)
                If iCol > 0 Then
                    .Row = .MaxRows
                    .Col = iCol + 0 : .Text = r_dt.Rows(i).Item("cnt1").ToString
                    .Col = iCol + 1 : .Text = r_dt.Rows(i).Item("cnt2").ToString
                    .Col = iCol + 2 : .Text = Format(Convert.ToDouble(r_dt.Rows(i).Item("cnt3").ToString), "0.00")

                    Dim strTmp As String = ""
                    strTmp = r_dt.Rows(i).Item("cnt3").ToString

                    lngCnt1 += Convert.ToInt32(r_dt.Rows(i).Item("cnt1").ToString)
                    lngCnt2 += Convert.ToInt32(r_dt.Rows(i).Item("cnt2").ToString)
                End If
            Next

            iCol = .GetColFromID("total")
            If iCol > 0 Then
                .Row = .MaxRows
                .Col = iCol + 0 : .Text = lngCnt1.ToString
                .Col = iCol + 1 : .Text = lngCnt2.ToString
                .Col = iCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.00")
            End If

            If rsStGbn = "01" Or rsStGbn = "02" Then
            Else
                .MaxRows += 1
                .Row = .MaxRows
                If .GetColFromID("name2".ToLower) > 0 Then
                    .Col = .GetColFromID("name2".ToLower) : .Text = "Total"
                End If

                For intIdx As Integer = .GetColFromID("name2") + 1 To .MaxCols Step 3
                    Dim lngRstCnt As Long = 0
                    Dim lngModCnt As Long = 0

                    For intRow As Integer = 1 To .MaxRows - 1
                        Dim strTmp As String

                        .Row = intRow
                        .Col = intIdx + 0 : strTmp = .Text
                        If strTmp = "" Then strTmp = "0"
                        lngRstCnt += Convert.ToInt32(strTmp)

                        .Col = intIdx + 1 : strTmp = .Text
                        If strTmp = "" Then strTmp = "0"
                        lngModCnt += Convert.ToInt32(strTmp)
                    Next

                    .Row = .MaxRows
                    .Col = intIdx + 0 : .Text = lngRstCnt.ToString
                    .Col = intIdx + 1 : .Text = lngModCnt.ToString

                    If lngRstCnt > 0 Then
                        .Col = intIdx + 2 : .Text = Format((lngModCnt / lngRstCnt) * 100, "0.00")
                    Else
                        .Col = intIdx + 2 : .Text = Format(0, "0.00")
                    End If

                Next
            End If

            'ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String
            sbNullCntfill(rsDMYGbn, ra_sDMY, rsDT1, rsDT1, r_dt)

            sbTotalCnt(ra_sDMY)
            'sbTotalCnt(r_dt)


            .ReDraw = True
        End With

    End Sub
    Private Function fnCnt_days(ByVal rsSpccd As String, ByVal rs_sDMY As String, ByVal rsDMYGbn As String, ByVal r_dt As DataTable) As String

        Dim sDtDays As String = ""
        Dim sCode1 As String
        Dim sCntTmp As String = ""

        For ix As Integer = 0 To r_dt.Rows.Count - 1

            sCode1 = r_dt.Rows(ix).Item("code1").ToString()
            sDtDays = r_dt.Rows(ix).Item("days").ToString()

            If rsDMYGbn = "D" Then
                sDtDays = sDtDays.Substring(0, 4) + "-" + sDtDays.Substring(4, 2) + "-" + sDtDays.Substring(6, 2)
            ElseIf rsDMYGbn = "M" Then
                sDtDays = sDtDays.Substring(0, 4) + "-" + sDtDays.Substring(4, 2)
            ElseIf rsDMYGbn = "Y" Then
                sDtDays = sDtDays.Substring(0, 4) + "-" + sDtDays.Substring(4, 2)
            End If

            If sCode1 = rsSpccd Then
                If rs_sDMY = sDtDays Then
                    sCntTmp = r_dt.Rows(ix).Item("cnt1").ToString()
                    Exit For
                End If
            End If
        Next 'r_dt.rows

        Return sCntTmp

    End Function

    Private Sub sbNullCntfill(ByVal rsDMYGbn As String, ByVal ra_sDMY As String(), ByVal rsDT1 As String, ByVal rsDT2 As String, ByVal r_dt As DataTable)
        With spdStatistics

            Dim strDays As String = ""
            Dim sDtDays As String = ""
            Dim iCol As Integer = -1

            Dim sCode1 As String
            Dim sCode2 As String

            Dim sSpdCd1 As String
            Dim sSpdCd_old As String = ""
            Dim sSpdCnt_S As String = ""
            Dim sSpdCnt_T As String = ""

            Dim sCntTmp As String = ""
            Dim iGTCnt As Integer = 0

            '1) 조회 한 일/월/달 수의 개수만큼 데이터를 순차적으로 뿌리기위해 
            For ix As Integer = 0 To ra_sDMY.Length - 1 '일/월/달 조회개수 만큼 

                strDays = ra_sDMY(ix)
                '2) 해당 일/월/달에서 첫행의 검체의 해당달의 카운트를 찾는다 
                For iy As Integer = 0 To .MaxRows - 1 '스프레드 수 만큼 

                    If iy > 0 Then

                        .Row = iy
                        .Col = .GetColFromID("code1".ToLower) : sSpdCd1 = .Text
                        iCol = .GetColFromID(strDays)
                        .Col = iCol + 0 : sSpdCnt_S = .Text

                        If sSpdCd_old <> sSpdCd1 Then ' 검체가 바뀌었을때만 새로 찾기 

                            sCntTmp = fnCnt_days(sSpdCd1, strDays, rsDMYGbn, r_dt)

                        End If

                        If sSpdCnt_S = "" Then

                            .Col = iCol + 0 : .Text = IIf(sCntTmp = "", "0", sCntTmp).ToString
                            .Col = iCol + 1 : .Text = CStr(0)
                            .Col = iCol + 2 : .Text = "0.00"

                        End If

                        sSpdCd_old = sSpdCd1

                    End If 'iy > 0 End 

                Next 'spd.MaxRows

            Next


            'iCol = .GetColFromID(strDays)

            'If rsDMYGbn = "D" Then
            '    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2) + "-" + strDays.Substring(6, 2)
            'ElseIf rsDMYGbn = "M" Then
            '    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2)
            'ElseIf rsDMYGbn = "Y" Then
            '    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2)
            'End If


        End With
    End Sub

    Private Sub sbTotalCnt(ByVal ra_sDMY As String())

        '1) 스프레드에서 검체코드와 균코드를 순서대로 For 문으로 돌아서 키(검체코드+균코드) 찾아냄
        With spdStatistics
            Dim sCode1 As String
            Dim sCode2 As String
            Dim sName1 As String
            Dim sName2 As String

            Dim sSpdCd1 As String
            Dim sSpdCd2 As String

            Dim iCntSum As Integer = 0
            Dim iCdCnt As Integer = 0
            Dim sSpdKey As String = ""
            Dim sDtKey As String = ""

            Dim iCol As Integer = -1
            Dim iGTCnt As Integer = 0
            Dim strDays As String = ""
            Dim sSpdCnt_S As String = ""

            For i As Integer = 0 To .MaxRows - 1

                If i > 0 Then

                    .Row = i
                    .Col = .GetColFromID("code1".ToLower) : sCode1 = .Text
                    .Col = .GetColFromID("name1".ToLower) : sName1 = .Text
                    .Col = .GetColFromID("code2".ToLower) : sCode2 = .Text
                    .Col = .GetColFromID("name2".ToLower) : sName2 = .Text

                    '2) 해당키로 DT를 찾아서 sum 

                    sSpdKey = sCode1 + sCode2

                    For ix As Integer = 0 To ra_sDMY.Length - 1

                        strDays = ra_sDMY(ix)

                        iCol = .GetColFromID(strDays)
                        .Col = iCol + 0 : sSpdCnt_S = .Text

                        iCntSum = iCntSum + Convert.ToInt32(sSpdCnt_S)

                    Next

                    '3) total에 뿌려줌
                    iCol = .GetColFromID("total")
                    If iCol > 0 Then
                        .Row = i
                        .Col = iCol + 0 : .Text = iCntSum.ToString
                        .Col = iCol + 1 : iGTCnt = CInt(.Text)
                        .Col = iCol + 2 : .Text = Format((iGTCnt / iCntSum) * 100, "0.00")

                        iCntSum = 0
                    End If
                End If

                '4) 반복
            Next

        End With


    End Sub


    Private Sub sbTotalCnt(ByVal r_dt As DataTable)

        '1) 스프레드에서 검체코드와 균코드를 순서대로 For 문으로 돌아서 키(검체코드+균코드) 찾아냄
        With spdStatistics
            Dim sCode1 As String
            Dim sCode2 As String
            Dim sName1 As String
            Dim sName2 As String

            Dim sSpdCd1 As String
            Dim sSpdCd2 As String

            Dim iCntSum As Integer = 0
            Dim iCdCnt As Integer = 0
            Dim sSpdKey As String = ""
            Dim sDtKey As String = ""

            Dim iCol As Integer = -1
            Dim iGTCnt As Integer = 0

            For i As Integer = 0 To .MaxRows - 1

                If i > 0 Then

                    .Row = i
                    .Col = .GetColFromID("code1".ToLower) : sCode1 = .Text
                    .Col = .GetColFromID("name1".ToLower) : sName1 = .Text
                    .Col = .GetColFromID("code2".ToLower) : sCode2 = .Text
                    .Col = .GetColFromID("name2".ToLower) : sName2 = .Text

                    '2) 해당키로 DT를 찾아서 sum 

                    sSpdKey = sCode1 + sCode2

                    For ix As Integer = 0 To r_dt.Rows.Count - 1
                        sSpdCd1 = r_dt.Rows(ix).Item("code1").ToString()
                        sSpdCd2 = r_dt.Rows(ix).Item("code2").ToString()
                        sDtKey = sSpdCd1 + sSpdCd2

                        If sSpdKey = sDtKey Then

                            iCntSum = iCntSum + Convert.ToInt32(r_dt.Rows(ix).Item("cnt1").ToString)

                        End If
                    Next

                    '3) total에 뿌려줌
                    iCol = .GetColFromID("total")
                    If iCol > 0 Then
                        .Row = i
                        .Col = iCol + 0 : .Text = iCntSum.ToString
                        .Col = iCol + 1 : iGTCnt = CInt(.Text)
                        .Col = iCol + 2 : .Text = Format((iGTCnt / iCntSum) * 100, "0.00")

                        iCntSum = 0
                    End If
                End If

                '4) 반복
            Next

        End With


    End Sub

    Private Sub sbDisplay_List_AFB(ByVal rsStGbn As String, ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then Return

        Dim sKey As String = ""
        Dim lngCnt1 As Long = 0
        Dim lngCnt2 As Long = 0
        Dim iCol As Integer = 0

        Dim alKeys As New ArrayList

        With Me.spdStatistics
            .ReDraw = False

            For i As Integer = 0 To r_dt.Rows.Count - 1

                sKey = r_dt.Rows(i).Item("code1").ToString + "/" + r_dt.Rows(i).Item("code2").ToString ' MTB/LM20101 or NTM/LM20101 , MTB/LM20303 or NTM/LM20303
                If alKeys.Contains(sKey) = False Then
                    alKeys.Add(sKey)

                    If .MaxRows > 0 Then
                        iCol = .GetColFromID("total")
                        If iCol > 0 Then
                            .Row = .MaxRows
                            .Col = iCol + 0 : .Text = lngCnt1.ToString
                            .Col = iCol + 1 : .Text = lngCnt2.ToString
                            .Col = iCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.00")
                        End If
                    End If

                    .MaxRows += 1

                    .Row = .MaxRows
                    .Col = .GetColFromID("code1".ToLower) : .Text = r_dt.Rows(i).Item("code1").ToString   'MTB/NTM 판정
                    .Col = .GetColFromID("name1".ToLower) : .Text = r_dt.Rows(i).Item("name1").ToString   ' null
                    .Col = .GetColFromID("code2".ToLower) : .Text = r_dt.Rows(i).Item("code2").ToString   '검사코드
                    .Col = .GetColFromID("name2".ToLower) : .Text = r_dt.Rows(i).Item("name2").ToString   '검사명

                    lngCnt1 = 0
                    lngCnt2 = 0
                End If

                Dim strDays As String = ""
                strDays = r_dt.Rows(i).Item("days").ToString
                If strDays.Length = 8 Then
                    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2) + "-" + strDays.Substring(6, 2)
                ElseIf strDays.Length = 6 Then
                    strDays = strDays.Substring(0, 4) + "-" + strDays.Substring(4, 2)
                End If

                iCol = .GetColFromID(strDays)
                If iCol > 0 Then
                    .Row = .MaxRows
                    .Col = iCol + 0 : .Text = r_dt.Rows(i).Item("cnt1").ToString
                    Dim sCnt1 As String = r_dt.Rows(i).Item("cnt1").ToString
                    .Col = iCol + 1 : .Text = r_dt.Rows(i).Item("cnt2").ToString
                    Dim sCnt2 As String = r_dt.Rows(i).Item("cnt2").ToString
                    .Col = iCol + 2 : .Text = Format((CType(sCnt2, Long) / CType(sCnt1, Long)) * 100, "0.00")
                    '.Col = iCol + 2 : .Text = Format(Convert.ToDouble(r_dt.Rows(i).Item("cnt3").ToString), "0.00")

                    Dim strTmp As String = ""
                    strTmp = r_dt.Rows(i).Item("cnt3").ToString

                    lngCnt1 += Convert.ToInt32(r_dt.Rows(i).Item("cnt1").ToString)
                    lngCnt2 += Convert.ToInt32(r_dt.Rows(i).Item("cnt2").ToString)
                End If
            Next

            iCol = .GetColFromID("total")
            If iCol > 0 Then
                .Row = .MaxRows
                .Col = iCol + 0 : .Text = lngCnt1.ToString
                .Col = iCol + 1 : .Text = lngCnt2.ToString
                .Col = iCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.00")
            End If

            If rsStGbn = "01" Or rsStGbn = "02" Then
            Else
                .MaxRows += 1
                .Row = .MaxRows
                If .GetColFromID("name2".ToLower) > 0 Then
                    .Col = .GetColFromID("name2".ToLower) : .Text = "Total"
                End If

                For intIdx As Integer = .GetColFromID("name2") + 1 To .MaxCols Step 3
                    Dim lngRstCnt As Long = 0
                    Dim lngModCnt As Long = 0

                    For intRow As Integer = 1 To .MaxRows - 1
                        Dim strTmp As String

                        .Row = intRow
                        .Col = intIdx + 0 : strTmp = .Text
                        If strTmp = "" Then strTmp = "0"
                        lngRstCnt += Convert.ToInt32(strTmp)

                        .Col = intIdx + 1 : strTmp = .Text
                        If strTmp = "" Then strTmp = "0"
                        lngModCnt += Convert.ToInt32(strTmp)
                    Next

                    .Row = .MaxRows
                    .Col = intIdx + 0 : .Text = lngRstCnt.ToString
                    .Col = intIdx + 1 : .Text = lngModCnt.ToString

                    If lngRstCnt > 0 Then
                        .Col = intIdx + 2 : .Text = Format((lngModCnt / lngRstCnt) * 100, "0.00")
                    Else
                        .Col = intIdx + 2 : .Text = Format(0, "0.00")
                    End If

                Next
            End If

            .ReDraw = True
        End With

    End Sub

    Private Sub sbDisplay_List_AFB_NEW(ByVal rsStGbn As String, ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then Return

        With spdStatistics
            .ReDraw = False

            Dim a_Buf As String = ""
            Dim b_Buf As String = ""

            .MaxRows = r_dt.Rows.Count

            For ix As Integer = 1 To r_dt.Rows.Count

                .Row = ix

                a_Buf = r_dt.Rows(ix - 1).Item("testcd").ToString + "/" + r_dt.Rows(ix - 1).Item("tnmd").ToString

                If a_Buf = b_Buf Then

                    .Col = .GetColFromID("testcd") : .Text = ""
                    .Col = .GetColFromID("tnmd") : .Text = ""

                Else
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix - 1).Item("testcd").ToString
                    .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix - 1).Item("tnmd").ToString

                End If

                .Col = .GetColFromID("gbn") : .Text = r_dt.Rows(ix - 1).Item("name1").ToString

                Dim cnt As Long = Convert.ToInt32(r_dt.Rows(ix - 1).Item("stcntab1").ToString)
                Dim cnt_t As Long = Convert.ToInt32(r_dt.Rows(ix - 1).Item("stcnt").ToString)

                .Col = .GetColFromID("total") : .Text = cnt_t.ToString
                .Col = .Col + 1 : .Text = cnt.ToString
                .Col = .Col + 1 : .Text = Format((cnt / cnt_t) * 100, "0.00")

                .Col = .Col + 1 : .Text = cnt_t.ToString
                .Col = .Col + 1 : .Text = cnt.ToString
                .Col = .Col + 1 : .Text = Format((cnt / cnt_t) * 100, "0.00")

                b_Buf = a_Buf

            Next

            .ReDraw = True
        End With

    End Sub

    Private Sub sbDisplay_List_AFB2(ByVal rsStGbn As String, ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then Return

        Dim sKey As String = ""
        Dim lngCnt1 As Long = 0
        Dim lngCnt2 As Long = 0
        Dim iCol As Integer = 0

        Dim alKeys As New ArrayList

        With Me.spdStatistics
            .ReDraw = False

            For i As Integer = 0 To r_dt.Rows.Count - 1
                .Row = i + 1
                .Col = 0
                .Text = r_dt.Rows(i).Item("name1").ToString
                .Col = 1
                .Text = r_dt.Rows(i).Item("testcd").ToString
                .Col = 2
                .Text = r_dt.Rows(i).Item("tnmd").ToString
                .Col = 3
                .Text = r_dt.Rows(i).Item("stcnt").ToString




            Next



            .ReDraw = True
        End With

    End Sub

    Private Sub sbDisplay_List_Anti(ByVal rsStGbn As String, ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then Return

        Dim sKey As String = ""
        Dim lngCnt1 As Long = 0
        Dim lngCnt2 As Long = 0
        Dim lngCntr As Long = 0
        Dim lngCnts As Long = 0
        Dim lngCnti As Long = 0

        Dim iCol As Integer = 0
        Dim alKeys As New ArrayList

        With Me.spdStatistics
            .ReDraw = False

            For i As Integer = 0 To r_dt.Rows.Count - 1

                sKey = r_dt.Rows(i).Item("code1").ToString + "/" + r_dt.Rows(i).Item("code2").ToString
                If alKeys.Contains(sKey) = False Then
                    alKeys.Add(sKey)

                    If .MaxRows > 0 Then
                        iCol = .GetColFromID("total")
                        If iCol > 0 Then
                            .Row = .MaxRows
                            .Col = iCol + 0 : .Text = lngCnt1.ToString
                            .Col = iCol + 1 : .Text = lngCnt2.ToString
                            .Col = iCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.00")

                            Dim iCnt As Integer = iCol + 2
                            If Me.chkAntiR.Checked Then
                                .Col = iCnt + 1 : .Text = lngCntr.ToString
                                .Col = iCnt + 2 : .Text = Format((lngCntr / lngCnt1) * 100, "0.00")

                                iCnt += 2
                            End If

                            If Me.chkAntiS.Checked Then
                                .Col = iCnt + 1 : .Text = lngCnts.ToString
                                .Col = iCnt + 2 : .Text = Format((lngCnts / lngCnt1) * 100, "0.00")

                                iCnt += 2
                            End If

                            If Me.chkAntiS.Checked Then
                                .Col = iCnt + 1 : .Text = lngCnti.ToString
                                .Col = iCnt + 2 : .Text = Format((lngCnti / lngCnt1) * 100, "0.00")
                            End If
                        End If
                    End If

                    .MaxRows += 1

                    .Row = .MaxRows
                    .Col = .GetColFromID("code1".ToLower) : .Text = r_dt.Rows(i).Item("code1").ToString
                    .Col = .GetColFromID("name1".ToLower) : .Text = r_dt.Rows(i).Item("name1").ToString
                    .Col = .GetColFromID("code2".ToLower) : .Text = r_dt.Rows(i).Item("code2").ToString
                    .Col = .GetColFromID("name2".ToLower) : .Text = r_dt.Rows(i).Item("name2").ToString

                    lngCnt1 = 0 : lngCnt2 = 0
                    lngCntr = 0 : lngCnts = 0 : lngCnti = 0
                End If

                Dim sDays As String = ""
                sDays = r_dt.Rows(i).Item("days").ToString
                If sDays.Length = 8 Then
                    sDays = sDays.Substring(0, 4) + "-" + sDays.Substring(4, 2) + "-" + sDays.Substring(6, 2)
                ElseIf sDays.Length = 6 Then
                    sDays = sDays.Substring(0, 4) + "-" + sDays.Substring(4, 2)
                End If

                iCol = .GetColFromID(sDays)
                If iCol > 0 Then
                    .Row = .MaxRows
                    .Col = iCol + 0 : .Text = r_dt.Rows(i).Item("cnt1").ToString
                    .Col = iCol + 1 : .Text = r_dt.Rows(i).Item("cnt2").ToString
                    .Col = iCol + 2 : .Text = Format(Convert.ToDouble(r_dt.Rows(i).Item("cnt3").ToString), "0.00")

                    Dim iCnt As Integer = iCol + 2

                    If Me.chkAntiR.Checked Then
                        .Col = iCnt + 1 : .Text = r_dt.Rows(i).Item("cntr").ToString
                        .Col = iCnt + 2 : .Text = Format(Convert.ToDouble(r_dt.Rows(i).Item("cntr_p").ToString), "0.00")

                        iCnt += 2
                    End If


                    If Me.chkAntiS.Checked Then
                        .Col = iCnt + 1 : .Text = r_dt.Rows(i).Item("cnts").ToString
                        .Col = iCnt + 2 : .Text = Format(Convert.ToDouble(r_dt.Rows(i).Item("cnts_p").ToString), "0.00")

                        iCnt += 2
                    End If


                    If Me.chkAntiI.Checked Then
                        .Col = iCnt + 1 : .Text = r_dt.Rows(i).Item("cnti").ToString
                        .Col = iCnt + 2 : .Text = Format(Convert.ToDouble(r_dt.Rows(i).Item("cnti_p").ToString), "0.00")
                    End If


                    Dim strTmp As String = ""
                    strTmp = r_dt.Rows(i).Item("cnt3").ToString

                    lngCnt1 += Convert.ToInt32(r_dt.Rows(i).Item("cnt1").ToString)
                    lngCnt2 += Convert.ToInt32(r_dt.Rows(i).Item("cnt2").ToString)
                    lngCntr += Convert.ToInt32(r_dt.Rows(i).Item("cntr").ToString)
                    lngCnts += Convert.ToInt32(r_dt.Rows(i).Item("cnts").ToString)
                    lngCnti += Convert.ToInt32(r_dt.Rows(i).Item("cnti").ToString)
                End If
            Next

            iCol = .GetColFromID("total")
            If iCol > 0 Then
                .Row = .MaxRows
                .Col = iCol + 0 : .Text = lngCnt1.ToString
                .Col = iCol + 1 : .Text = lngCnt2.ToString
                .Col = iCol + 2 : .Text = Format((lngCnt2 / lngCnt1) * 100, "0.00")

                Dim iCnt As Integer = iCol + 2
                If Me.chkAntiR.Checked Then
                    .Col = iCnt + 1 : .Text = lngCntr.ToString
                    .Col = iCnt + 2 : .Text = Format((lngCntr / lngCnt1) * 100, "0.00")

                    iCnt += 2
                End If

                If Me.chkAntiS.Checked Then
                    .Col = iCnt + 1 : .Text = lngCnts.ToString
                    .Col = iCnt + 2 : .Text = Format((lngCnts / lngCnt1) * 100, "0.00")

                    iCnt += 2
                End If

                If Me.chkAntiS.Checked Then
                    .Col = iCnt + 1 : .Text = lngCnti.ToString
                    .Col = iCnt + 2 : .Text = Format((lngCnti / lngCnt1) * 100, "0.00")
                End If
            End If

            .ReDraw = True
        End With

    End Sub

    Private Function fnDisplayStatistics() As Boolean

        Dim bReturn As Boolean = False

        Try
            Dim sStGbn As String = ""
            Dim sStType As String = "", sDMYGbn As String = "", sDT1 As String = "", sDT2 As String = ""
            Dim sIO As String = "", sDept As String = "", sWard As String = ""
            Dim sBacGen As String = "", sSpcCd As String = ""
            Dim sTclsCd As String = "", sBacCd As String = "", strAntiRst As String = ""
            Dim a_sDMY As String() = Nothing
            Dim iDMYDiff As Integer = 0, iSum As Integer = 0, iCnt As Integer = 0

            '통계 구분
            sStGbn = Ctrl.Get_Code(Me.cboStGbn)

            '기준시간 구분
            If Me.rdoOptDT1.Checked Then
                sStType = "O"
            ElseIf Me.rdoOptDT2.Checked Then '20131126 정선영 추가, 접수일시 구분
                sStType = "T"
            ElseIf Me.rdoOptDT3.Checked Then
                sStType = "F"
            End If

            If Me.dtpDT1.Value > Me.dtpDT2.Value Then
                MsgBox("날짜구간 설정이 잘못되었습니다. 시작을 끝보다 작거나 같게 설정하십시요!!")

                Return False
            End If

            '> 일별/월별/연별 구분
            If Me.rdoDay.Checked Then
                '일별
                sDT1 = Me.dtpDT1.Value.ToString("yyyy-MM-dd")
                sDT2 = Me.dtpDT2.Value.ToString("yyyy-MM-dd")
                sDMYGbn = "D"

                ReDim a_sDMY(0)
                a_sDMY(0) = sDT1 + " ~ " + sDT2

            ElseIf Me.rdoMonth.Checked Then
                '월별
                sDT1 = Me.dtpDT1.Value.ToString("yyyy-MM")
                sDT2 = Me.dtpDT2.Value.ToString("yyyy-MM")

                sDMYGbn = "M"

                iDMYDiff = CInt(DateDiff(DateInterval.Month, CDate(sDT1), CDate(sDT2)))

                If iDMYDiff > miMaxDiffMonth - 1 Then
                    MsgBox("월별로는 " & miMaxDiffMonth.ToString & "개월 까지의 검사통계를 조회할 수 있습니다. 날짜구간을 다시 설정하십시요!!")

                    Return False
                End If

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Month, i - 1, CDate(sDT1)).ToString("yyyy-MM")
                Next

            ElseIf Me.rdoYear.Checked Then
                '연별
                sDT1 = Me.dtpDT1.Value.ToString("yyyy")
                sDT2 = Me.dtpDT2.Value.ToString("yyyy")

                sDMYGbn = "Y"

                iDMYDiff = CInt(DateDiff(DateInterval.Year, CDate(sDT1 + "-01"), CDate(sDT2 + "-12")))

                If iDMYDiff > miMaxDiffMonth - 1 Then
                    MsgBox("연별로는 " & miMaxDiffYear.ToString & "년 까지의 검사통계를 조회할 수 있습니다. 날짜구간을 다시 설정하십시요!!")

                    Return False
                End If

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Year, i - 1, CDate(sDT1 + "-01")).ToString("yyyy")
                Next

            End If

            '전체

            '외래/입원
            If Me.rdoIOO.Checked Then
                sIO = "O"
            ElseIf Me.rdoIOI.Checked Then
                sIO = "I"
            ElseIf Me.rdoIOC.Checked Then
                sIO = "C"
            End If

            '진료과
            If Me.rdoIOO.Checked Or Me.rdoIOC.Checked Then sDept = Me.cboDept.Text.Split("|"c)(1)

            '병동
            If Me.rdoWardS.Enabled And Me.rdoWardS.Checked Then sWard = Me.cboWard.Text.Split("|"c)(1)

            '배양균속
            If Me.rdoBacGenS.Checked Then
                sBacGen = Ctrl.Get_Code(Me.cboBacGen)
            End If

            '항균제 결과
            If chkAntiR.Checked Then
                strAntiRst = "1"
            Else
                strAntiRst = "0"
            End If

            If chkAntiS.Checked Then
                strAntiRst += "1"
            Else
                strAntiRst += "0"
            End If

            If chkAntiI.Checked Then
                strAntiRst += "1"
            Else
                strAntiRst += "0"
            End If

            '검체
            'If Me.rdoSpcCdS.Checked Then
            '    ' sSpcCd = Ctrl.Get_Code(Me.cboSpcCd)
            '    sSpcCd = Me.txtSpcCd.Tag.ToString.Trim
            'End If
            '검체
            If Me.rdoSpcCdS.Checked Then
                With Me.spdspc
                    For i As Integer = 1 To .MaxRows
                        .Col = .GetColFromID("chk")
                        .Row = i

                        If .Text = "1" Then
                            .Col = .GetColFromID("spccd")
                            .Row = i

                            sSpcCd += "'" & .Text & "',"
                        End If
                    Next
                End With

                If sSpcCd = "" Then
                    MsgBox("통계를 위한 검체를 선택해 주십시요!!")

                    Return False
                Else
                    sSpcCd = sSpcCd.Substring(0, sSpcCd.Length - 1)
                End If
            End If

            '검사
            If Me.rdoTestS.Checked Then
                With Me.spdTest
                    For i As Integer = 1 To .MaxRows
                        .Col = .GetColFromID("CHK")
                        .Row = i

                        If .Text = "1" Then
                            .Col = .GetColFromID("TESTCD")
                            .Row = i

                            sTclsCd += "'" & .Text & "',"
                        End If
                    Next
                End With

                If sTclsCd = "" Then
                    MsgBox("통계를 위한 검사를 선택해 주십시요!!")

                    Return False
                Else
                    sTclsCd = sTclsCd.Substring(0, sTclsCd.Length - 1)
                End If
            End If

            '배양균
            If Me.rdoBacS.Checked Then
                With Me.spdBac
                    For i As Integer = 1 To .MaxRows
                        .Col = .GetColFromID("chk")
                        .Row = i

                        If .Text = "1" Then
                            .Col = .GetColFromID("baccd")
                            .Row = i

                            sBacCd += "'" & .Text & "',"
                        End If
                    Next
                End With

                If sBacCd = "" Then
                    MsgBox("통계를 위한 배양균을 선택해 주십시요!!")

                    Return False
                Else
                    sBacCd = sBacCd.Substring(0, sBacCd.Length - 1)
                End If
            End If

            sbInitialize_spdStatistics(sStGbn, a_sDMY)

            Dim dt As New DataTable

            Select Case sStGbn
                Case "01"
                    dt = (New SrhFn).fnGet_M_Bac_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen, sSpcCd, sTclsCd, sBacCd, chkSameCd.Checked)
                Case "02"
                    dt = (New SrhFn).fnGet_M_Anti_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen, sSpcCd, sTclsCd, sBacCd, strAntiRst, chkSameCd.Checked)
                Case "03"
                    dt = (New SrhFn).fnGet_M_MRSA_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen)
                Case "04"
                    dt = (New SrhFn).fnGet_M_VRE_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen)
                Case "05"
                    dt = (New SrhFn).fnGet_M_IRPA_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen)
                Case "06"
                    dt = (New SrhFn).fnGet_M_IRAB_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen)
                Case "07"
                    dt = (New SrhFn).fnGet_M_EESBL_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen)
                Case "08"
                    dt = (New SrhFn).fnGet_M_KESBL_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen)
                Case "09"
                    dt = (New SrhFn).fnGet_M_VRSA_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen)
                Case "10"
                    dt = (New SrhFn).fnGet_M_Group_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sBacGen)
                Case "11"
                    'dt = (New SrhFn).fnGet_M_AFB_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard)U   JMNNNNNNNNNNNNNNNNNNNNNNNNNNN 
                    'dt = (New SrhFn).fnGet_M_AFB_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard)
                    dt = (New SrhFn).fnGet_M_AFB_Statistics3(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sIO, sDept, sWard, sSpcCd, sTclsCd)
            End Select

            If sStGbn = "02" Then
                sbDisplay_List_Anti(sStGbn, dt)
            ElseIf sStGbn = "11" Then
                'sbDisplay_List_AFB(sStGbn, dt)
                'sbDisplay_List_AFB(sStGbn, dt)
                sbDisplay_List_AFB_NEW(sStGbn, dt)
            ElseIf sStGbn = "01" Then
                sbDisplay_List_Growth(sStGbn, dt, sDMYGbn, a_sDMY, sDT1, sDT2)
            Else
                sbDisplay_List(sStGbn, dt)
            End If

            Return True
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

            Return False
        End Try
    End Function

    Private Sub sbDisplay_dept()

        Try
            Dim dt As DataTable

            dt = OCSAPP.OcsLink.SData.fnGet_DeptList

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboDept.Items.Add(dt.Rows(i).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(i).Item("deptcd").ToString)
                Next

                If Me.cboDept.Items.Count > 0 Then Me.cboDept.SelectedIndex = 0
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_bacgen()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_BacGen_List()

            Me.cboBacGen.Items.Clear()
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboBacGen.Items.Add("[" & dt.Rows(i).Item("bacgencd").ToString & "]" & " " & dt.Rows(i).Item("bacgennmd").ToString)
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_spc()

        Try
            Dim dt As DataTable
            Dim iCol As Integer = 0
            dt = LISAPP.COMM.CdFn.fnGet_Spc_List("", "", "", "", "", "", "")

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                With spdspc
                    .ReDraw = False

                    .MaxRows = dt.Rows.Count

                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)


                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = dt.Rows(i).Item(j).ToString
                            End If
                        Next
                    Next

                    .ReDraw = True
                End With
            End If


            'Me.cboSpcCd.Items.Clear()

            'If dt.Rows.Count > 0 Then
            '    For i As Integer = 0 To dt.Rows.Count - 1
            '        Me.cboSpcCd.Items.Add("[" & dt.Rows(i).Item(0).ToString & "]" & " " & dt.Rows(i).Item(1).ToString)
            '    Next
            'End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_spc_save()

        Try

            If IO.File.Exists(XmlFile) = False Then Return
            Dim SPCCD As String() = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "SPCCD").Split("^"c)

            If SPCCD.Count <= 0 And Ctrl.Get_Code(Me.cboStGbn) <> "11" Then Return

            With spdspc
                .ReDraw = False

                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("spccd")

                    If SPCCD.Contains(.Text) Then
                        .Col = .GetColFromID("chk")
                        .Text = "1"
                    End If

                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_test()

        Try
            Dim dt As DataTable
            Dim iCol As Integer = 0

            dt = LISAPP.COMM.CdFn.fnGet_test_ParentSingle(PRG_CONST.PART_MicroBio, "")

            If Ctrl.Get_Code(Me.cboStGbn) = "11" Then
                dt = LISAPP.COMM.CdFn.fnGet_test_list("M2", "", "")
                Dim dr As DataRow() = dt.Select("testcd in ('LM20101', 'LM20102', 'LM20302', 'LM20303')")
                dt = Fn.ChangeToDataTable(dr)
            End If


            spdTest.MaxRows = 0

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                With spdTest
                    .ReDraw = False

                    .MaxRows = dt.Rows.Count

                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(dt.Columns(j).ColumnName.ToUpper)


                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = dt.Rows(i).Item(j).ToString
                            End If
                        Next
                    Next

                    .ReDraw = True
                End With
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Bac()

        Try
            Dim dt As DataTable
            Dim iCol As Integer = 0
            Dim strBacGenCd As String = ""

            If Me.rdoBacGenS.Checked Then
                strBacGenCd = Ctrl.Get_Code(Me.cboBacGen)
            End If

            If strBacGenCd = "" Then
                MsgBox("균속을 선택한 후 사용하세요.", MsgBoxStyle.Information)
                rdoBacA.Checked = True
                Exit Sub
            End If

            dt = LISAPP.COMM.CdFn.fnGet_Bac_List(strBacGenCd, False, "")

            spdBac.MaxRows = 0

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                With spdBac
                    .ReDraw = False

                    .MaxRows = dt.Rows.Count

                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = dt.Rows(i).Item(j).ToString
                            End If
                        Next
                    Next

                    .ReDraw = True
                End With
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_ward()

        Try
            Dim dt As DataTable

            dt = OCSAPP.OcsLink.SData.fnGet_WardList

            Me.cboWard.Items.Clear()

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboWard.Items.Add(dt.Rows(i).Item("wardnm").ToString + Space(200) + "|" + dt.Rows(i).Item("wardno").ToString)
                Next

                If Me.cboWard.Items.Count > 0 Then Me.cboWard.SelectedIndex = 0
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize()

        Try
            miSelectKey = 1
            Dim sCurSysDate As String = ""

            Me.rdoDay.Checked = True
            '------------------------------
            Me.rdoOptDT1.Checked = True
            '------------------------------
            sCurSysDate = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")
            Me.dtpDT1.CustomFormat = "yyyy-MM-dd" : Me.dtpDT1.Value = CType(sCurSysDate & " 00:00:00", Date)
            Me.dtpDT2.CustomFormat = "yyyy-MM-dd" : Me.dtpDT2.Value = CType(sCurSysDate & " 23:59:59", Date)
            '------------------------------
            Me.rdoIOA.Checked = True

            Me.rdoDeptA.Checked = True
            Me.pnlDept.Enabled = False
            Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False

            Me.rdoWardA.Checked = True
            Me.pnlWard.Enabled = False
            Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False
            '------------------------------
            Me.rdoBacGenA.Checked = True
            Me.cboBacGen.SelectedIndex = -1 : Me.cboBacGen.Enabled = False
            '------------------------------
            Me.rdoSpcCdA.Checked = True
            Me.cboSpcCd.SelectedIndex = -1 : Me.cboSpcCd.Enabled = False
            Me.spdspc.MaxRows = 0
            '------------------------------

            Me.rdoTestA.Checked = True
            Me.spdTest.MaxRows = 0

            Me.rdoBacA.Checked = True
            Me.spdBac.MaxRows = 0



            Dim bAuthority As Boolean = USER_SKILL.Authority("T01", mi_Analysis_Or_Reanalysis)

            cboStGbn.SelectedIndex = 0

            Me.btnAnalysis.Enabled = bAuthority

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbInitialize_spdStatistics(ByVal rs_StGbn As String, ByVal ra_sDMY As String())

        Try
            With Me.spdStatistics
                .ReDraw = False

                .MaxRows = 0

                Select Case rs_StGbn
                    Case "01"
                        .MaxCols = 4

                        .Row = 0 : .Col = 1 : .ColID = "code1" : .ColHidden = False : .set_ColWidth(2, 7)
                        .Row = 0 : .Col = 2 : .ColID = "code2" : .ColHidden = False : .set_ColWidth(3, 8)
                        .Row = 0 : .Col = 3 : .ColID = "name1" : .ColHidden = False : .set_ColWidth(4, 15)
                        .Row = 0 : .Col = 4 : .ColID = "name2" : .ColHidden = False : .set_ColWidth(5, 25)

                        .Row = 0
                        .Col = 1 : .Text = "검체코드"
                        .Col = 2 : .Text = "배양균코드"
                        .Col = 3 : .Text = "검체명"
                        .Col = 4 : .Text = "배양균명"

                        .Row = -1 : .Col = 1 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                        .Row = -1 : .Col = 2 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                        .Row = -1 : .Col = 3 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                        .Row = -1 : .Col = 4 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft

                        .Row = 0 : .Col = 1 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 2 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 3 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 4 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .ColsFrozen = 4

                    Case "11" '<20150126 결핵균 통계 추가 
                        .MaxCols = 4

                        '.Row = 0 : .Col = 1 : .ColID = "code1" : .ColHidden = False : .set_ColWidth(2, 7) '결핵판정
                        '.Row = 0 : .Col = 2 : .ColID = "code2" : .ColHidden = False : .set_ColWidth(3, 8) '검사코드
                        '.Row = 0 : .Col = 3 : .ColID = "name1" : .ColHidden = True : .set_ColWidth(4, 15) '?
                        '.Row = 0 : .Col = 4 : .ColID = "name2" : .ColHidden = False : .set_ColWidth(5, 25) '검사명

                        .Row = 0 : .Col = 1 : .ColID = "testcd" : .ColHidden = False : .set_ColWidth(1, 7) '검사코드
                        .Row = 0 : .Col = 2 : .ColID = "tnmd" : .ColHidden = False : .set_ColWidth(2, 20) '검사명
                        .Row = 0 : .Col = 3 : .ColID = "name1" : .ColHidden = True : .set_ColWidth(4, 8) '?
                        .Row = 0 : .Col = 4 : .ColID = "gbn" : .ColHidden = False : .set_ColWidth(5, 8) '결핵판정

                        .Row = 0
                        '.Col = 1 : .Text = "결핵판정"
                        '.Col = 2 : .Text = "검사코드"
                        '.Col = 3 : .Text = ""
                        '.Col = 4 : .Text = "검사명"

                        .Col = 1 : .Text = "검사코드"
                        .Col = 2 : .Text = "검사명"
                        .Col = 3 : .Text = ""
                        .Col = 4 : .Text = "결핵판정"

                        .Row = -1 : .Col = 1 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                        .Row = -1 : .Col = 2 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                        .Row = -1 : .Col = 3 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = -1 : .Col = 4 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .Row = 0 : .Col = 1 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 2 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 3 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 4 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .ColsFrozen = 4
                    Case Else
                        .MaxCols = 4

                        .Row = 0 : .Col = 1 : .ColID = "code1" : .ColHidden = False : .set_ColWidth(2, 7)
                        .Row = 0 : .Col = 2 : .ColID = "code2" : .ColHidden = False : .set_ColWidth(3, 8)
                        .Row = 0 : .Col = 3 : .ColID = "name1" : .ColHidden = False : .set_ColWidth(4, 25)
                        .Row = 0 : .Col = 4 : .ColID = "name2" : .ColHidden = False : .set_ColWidth(5, 20)

                        .Row = 0
                        .Col = 1 : .Text = "배양균코드"
                        .Col = 2 : .Text = "항균제코드"
                        .Col = 3 : .Text = "배양균명"
                        .Col = 4 : .Text = "항균제명"

                        .Row = -1 : .Col = 1 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                        .Row = -1 : .Col = 2 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                        .Row = -1 : .Col = 3 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                        .Row = -1 : .Col = 4 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft

                        .Row = 0 : .Col = 1 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 2 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 3 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Row = 0 : .Col = 4 : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .ColsFrozen = 4

                End Select

                If rs_StGbn = "02" Then
                    If ra_sDMY.Length > 0 Then
                        Dim iCnt As Integer = 0
                        If Me.chkAntiR.Checked Then iCnt += 2
                        If Me.chkAntiS.Checked Then iCnt += 2
                        If Me.chkAntiI.Checked Then iCnt += 2

                        .MaxCols += 3 + iCnt

                        .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                        .Col = 5 : .ColID = "total" : .Text = "Total" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                        .Col = 5 : .Text = "T" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Col = 6 : .Text = "(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Col = 7 : .Text = "T(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        iCnt = 0
                        If Me.chkAntiR.Checked Then
                            .Col = 7 + iCnt + 1 : .Text = "R(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                            .Col = 7 + iCnt + 2 : .Text = "R(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                            iCnt += 2
                        End If

                        If Me.chkAntiS.Checked Then
                            .Col = 7 + iCnt + 1 : .Text = "S(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                            .Col = 7 + iCnt + 2 : .Text = "S(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                            iCnt += 2
                        End If

                        If Me.chkAntiI.Checked Then
                            .Col = 7 + iCnt + 1 : .Text = "I(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                            .Col = 7 + iCnt + 2 : .Text = "I(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                            iCnt += 2
                        End If

                        .AddCellSpan(5, 0, 7 + iCnt, 1)
                    End If

                    For i As Integer = 0 To ra_sDMY.Length - 1
                        Dim iCnt As Integer = 0
                        If Me.chkAntiR.Checked Then iCnt += 2
                        If Me.chkAntiS.Checked Then iCnt += 2
                        If Me.chkAntiI.Checked Then iCnt += 2

                        .MaxCols += 3 + iCnt

                        .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                        .Col = .MaxCols - (2 + iCnt) : .ColID = ra_sDMY(i) : .Text = ra_sDMY(i) : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                        .Col = .MaxCols - (2 + iCnt) + 0 : .Text = "T" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Col = .MaxCols - (2 + iCnt) + 1 : .Text = "(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Col = .MaxCols - (2 + iCnt) + 2 : .Text = "T(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        iCnt = 0
                        If Me.chkAntiR.Checked Then
                            .Col = .MaxCols - (2 + iCnt) + 1 : .Text = "R(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                            .Col = .MaxCols - (2 + iCnt) + 2 : .Text = "R(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                            iCnt += 2
                        End If

                        If Me.chkAntiS.Checked Then
                            .Col = .MaxCols - (2 + iCnt) + 1 : .Text = "S(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                            .Col = .MaxCols - (2 + iCnt) + 2 : .Text = "S(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                            iCnt += 2
                        End If

                        If Me.chkAntiI.Checked Then
                            .Col = .MaxCols - (2 + iCnt) + 1 : .Text = "I(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                            .Col = .MaxCols - (2 + iCnt) + 2 : .Text = "I(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                            iCnt += 2
                        End If

                        .AddCellSpan(.MaxCols - (2 + iCnt), 0, .MaxCols, 1)
                    Next

                ElseIf rs_StGbn = "11" Then

                    .MaxCols += 3
                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                    .Col = 5 : .ColID = "total" : .Text = "Total" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                    .Col = 5 : .Text = "T" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .Col = 6 : .Text = "(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .Col = 7 : .Text = "(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                    .AddCellSpan(5, 0, 7, 1)

                    .MaxCols += 3

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                    .Col = .MaxCols - 2 : .ColID = "cnt_t" : .Text = ra_sDMY(0) : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                    .Col = .MaxCols - 2 : .Text = "T" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .Col = .MaxCols - 1 : .Text = "(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .Col = .MaxCols - 0 : .Text = "(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                    .AddCellSpan(.MaxCols - 2, 0, .MaxCols, 1)

                Else
                    If ra_sDMY.Length > 0 Then
                        .MaxCols += 3

                        .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                        .Col = 5 : .ColID = "total" : .Text = "Total" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                        .Col = 5 : .Text = "T" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Col = 6 : .Text = "(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Col = 7 : .Text = "(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .AddCellSpan(5, 0, 7, 1)
                    End If

                    For i As Integer = 0 To ra_sDMY.Length - 1
                        .MaxCols += 3

                        .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                        .Col = .MaxCols - 2 : .ColID = ra_sDMY(i) : .Text = ra_sDMY(i) : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                        .Col = .MaxCols - 2 : .Text = "T" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Col = .MaxCols - 1 : .Text = "(+)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                        .Col = .MaxCols - 0 : .Text = "(%)" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                        .AddCellSpan(.MaxCols - 2, 0, .MaxCols, 1)
                    Next
                End If


                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        sbInitialize()
    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents tclStatistics As System.Windows.Forms.TabControl
    Friend WithEvents tpgVar As System.Windows.Forms.TabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpDT2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDT1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDay As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pnlIO As System.Windows.Forms.Panel
    Friend WithEvents pnlDept As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents grp02 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents pnlWkGrp As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboStGbn As System.Windows.Forms.ComboBox
    Friend WithEvents cboWard As System.Windows.Forms.ComboBox
    Friend WithEvents rdoWardS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoWardA As System.Windows.Forms.RadioButton
    Friend WithEvents cboDept As System.Windows.Forms.ComboBox
    Friend WithEvents rdoDeptS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDeptA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOO As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOI As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOptDT3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOptDT1 As System.Windows.Forms.RadioButton
    Friend WithEvents cboBacGen As System.Windows.Forms.ComboBox
    Friend WithEvents cboSpcCd As System.Windows.Forms.ComboBox
    Friend WithEvents rdoSpcCdA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSpcCdS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTestS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTestA As System.Windows.Forms.RadioButton
    Friend WithEvents spdTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlWard As System.Windows.Forms.Panel

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGT05))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.tclStatistics = New System.Windows.Forms.TabControl()
        Me.tpgVar = New System.Windows.Forms.TabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.spdStatistics = New AxFPSpreadADO.AxfpSpread()
        Me.split1 = New System.Windows.Forms.Splitter()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.spdspc = New AxFPSpreadADO.AxfpSpread()
        Me.cboSpcCd = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.pnlWkGrp = New System.Windows.Forms.Panel()
        Me.rdoSpcCdS = New System.Windows.Forms.RadioButton()
        Me.rdoSpcCdA = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cboStGbn = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlSlip = New System.Windows.Forms.Panel()
        Me.rdoBacGenS = New System.Windows.Forms.RadioButton()
        Me.rdoBacGenA = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdoYear = New System.Windows.Forms.RadioButton()
        Me.rdoMonth = New System.Windows.Forms.RadioButton()
        Me.rdoDay = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rdoOptDT3 = New System.Windows.Forms.RadioButton()
        Me.rdoOptDT2 = New System.Windows.Forms.RadioButton()
        Me.rdoOptDT1 = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpDT1 = New System.Windows.Forms.DateTimePicker()
        Me.dtpDT2 = New System.Windows.Forms.DateTimePicker()
        Me.cboBacGen = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlIO = New System.Windows.Forms.Panel()
        Me.rdoIOC = New System.Windows.Forms.RadioButton()
        Me.rdoIOO = New System.Windows.Forms.RadioButton()
        Me.rdoIOI = New System.Windows.Forms.RadioButton()
        Me.rdoIOA = New System.Windows.Forms.RadioButton()
        Me.pnlDept = New System.Windows.Forms.Panel()
        Me.rdoDeptS = New System.Windows.Forms.RadioButton()
        Me.rdoDeptA = New System.Windows.Forms.RadioButton()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.cboWard = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlWard = New System.Windows.Forms.Panel()
        Me.rdoWardS = New System.Windows.Forms.RadioButton()
        Me.rdoWardA = New System.Windows.Forms.RadioButton()
        Me.grp02 = New System.Windows.Forms.GroupBox()
        Me.chkSameCd = New System.Windows.Forms.CheckBox()
        Me.spdBac = New AxFPSpreadADO.AxfpSpread()
        Me.pnlAntiRst = New System.Windows.Forms.Panel()
        Me.chkAntiI = New System.Windows.Forms.CheckBox()
        Me.chkAntiS = New System.Windows.Forms.CheckBox()
        Me.chkAntiR = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rdoBacS = New System.Windows.Forms.RadioButton()
        Me.rdoBacA = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.spdTest = New AxFPSpreadADO.AxfpSpread()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.rdoTestS = New System.Windows.Forms.RadioButton()
        Me.rdoTestA = New System.Windows.Forms.RadioButton()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblColor = New System.Windows.Forms.Label()
        Me.txtSpcCd = New System.Windows.Forms.TextBox()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.btnSearch = New CButtonLib.CButton()
        Me.btnAnalysis = New CButtonLib.CButton()
        Me.btnSave = New CButtonLib.CButton()
        Me.tclStatistics.SuspendLayout()
        Me.tpgVar.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.spdStatistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdspc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlWkGrp.SuspendLayout()
        Me.pnlSlip.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlIO.SuspendLayout()
        Me.pnlDept.SuspendLayout()
        Me.pnlWard.SuspendLayout()
        Me.grp02.SuspendLayout()
        CType(Me.spdBac, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAntiRst.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.SuspendLayout()
        '
        'tclStatistics
        '
        Me.tclStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tclStatistics.Controls.Add(Me.tpgVar)
        Me.tclStatistics.Location = New System.Drawing.Point(0, 0)
        Me.tclStatistics.Name = "tclStatistics"
        Me.tclStatistics.SelectedIndex = 0
        Me.tclStatistics.Size = New System.Drawing.Size(1182, 723)
        Me.tclStatistics.TabIndex = 0
        '
        'tpgVar
        '
        Me.tpgVar.BackColor = System.Drawing.Color.Transparent
        Me.tpgVar.Controls.Add(Me.Panel5)
        Me.tpgVar.Controls.Add(Me.split1)
        Me.tpgVar.Controls.Add(Me.Panel4)
        Me.tpgVar.Location = New System.Drawing.Point(4, 22)
        Me.tpgVar.Name = "tpgVar"
        Me.tpgVar.Size = New System.Drawing.Size(1174, 697)
        Me.tpgVar.TabIndex = 0
        Me.tpgVar.Text = "조회조건설정"
        Me.tpgVar.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.GroupBox2)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(348, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(826, 697)
        Me.Panel5.TabIndex = 128
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox2.Controls.Add(Me.spdStatistics)
        Me.GroupBox2.Location = New System.Drawing.Point(0, -10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(826, 704)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'spdStatistics
        '
        Me.spdStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdStatistics.DataSource = Nothing
        Me.spdStatistics.Location = New System.Drawing.Point(3, 17)
        Me.spdStatistics.Name = "spdStatistics"
        Me.spdStatistics.OcxState = CType(resources.GetObject("spdStatistics.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStatistics.Size = New System.Drawing.Size(820, 684)
        Me.spdStatistics.TabIndex = 0
        '
        'split1
        '
        Me.split1.BackColor = System.Drawing.SystemColors.Control
        Me.split1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.split1.Location = New System.Drawing.Point(343, 0)
        Me.split1.MinSize = 224
        Me.split1.Name = "split1"
        Me.split1.Size = New System.Drawing.Size(5, 697)
        Me.split1.TabIndex = 127
        Me.split1.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.cboStGbn)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.pnlSlip)
        Me.Panel4.Controls.Add(Me.Panel1)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.dtpDT1)
        Me.Panel4.Controls.Add(Me.dtpDT2)
        Me.Panel4.Controls.Add(Me.cboBacGen)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.pnlIO)
        Me.Panel4.Controls.Add(Me.cboDept)
        Me.Panel4.Controls.Add(Me.cboWard)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.pnlWard)
        Me.Panel4.Controls.Add(Me.grp02)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(343, 697)
        Me.Panel4.TabIndex = 24
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.spdspc)
        Me.GroupBox1.Controls.Add(Me.cboSpcCd)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.pnlWkGrp)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 180)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(337, 145)
        Me.GroupBox1.TabIndex = 129
        Me.GroupBox1.TabStop = False
        '
        'spdspc
        '
        Me.spdspc.DataSource = Nothing
        Me.spdspc.Location = New System.Drawing.Point(2, 37)
        Me.spdspc.Name = "spdspc"
        Me.spdspc.OcxState = CType(resources.GetObject("spdspc.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdspc.Size = New System.Drawing.Size(329, 95)
        Me.spdspc.TabIndex = 65
        '
        'cboSpcCd
        '
        Me.cboSpcCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboSpcCd.Location = New System.Drawing.Point(202, 12)
        Me.cboSpcCd.Name = "cboSpcCd"
        Me.cboSpcCd.Size = New System.Drawing.Size(137, 20)
        Me.cboSpcCd.TabIndex = 64
        Me.cboSpcCd.Tag = "TCDGBN_01"
        Me.cboSpcCd.Visible = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label20.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(3, 12)
        Me.Label20.Margin = New System.Windows.Forms.Padding(0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(92, 21)
        Me.Label20.TabIndex = 62
        Me.Label20.Text = "검체구분"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlWkGrp
        '
        Me.pnlWkGrp.BackColor = System.Drawing.Color.Linen
        Me.pnlWkGrp.Controls.Add(Me.rdoSpcCdS)
        Me.pnlWkGrp.Controls.Add(Me.rdoSpcCdA)
        Me.pnlWkGrp.Location = New System.Drawing.Point(96, 12)
        Me.pnlWkGrp.Name = "pnlWkGrp"
        Me.pnlWkGrp.Size = New System.Drawing.Size(105, 21)
        Me.pnlWkGrp.TabIndex = 63
        '
        'rdoSpcCdS
        '
        Me.rdoSpcCdS.BackColor = System.Drawing.Color.Linen
        Me.rdoSpcCdS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSpcCdS.ForeColor = System.Drawing.Color.Black
        Me.rdoSpcCdS.Location = New System.Drawing.Point(54, 1)
        Me.rdoSpcCdS.Name = "rdoSpcCdS"
        Me.rdoSpcCdS.Size = New System.Drawing.Size(48, 19)
        Me.rdoSpcCdS.TabIndex = 13
        Me.rdoSpcCdS.Text = "선택"
        Me.rdoSpcCdS.UseVisualStyleBackColor = False
        '
        'rdoSpcCdA
        '
        Me.rdoSpcCdA.BackColor = System.Drawing.Color.Linen
        Me.rdoSpcCdA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSpcCdA.ForeColor = System.Drawing.Color.Black
        Me.rdoSpcCdA.Location = New System.Drawing.Point(4, 1)
        Me.rdoSpcCdA.Name = "rdoSpcCdA"
        Me.rdoSpcCdA.Size = New System.Drawing.Size(48, 19)
        Me.rdoSpcCdA.TabIndex = 11
        Me.rdoSpcCdA.Text = "전체"
        Me.rdoSpcCdA.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Lavender
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(96, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 21)
        Me.Label6.TabIndex = 127
        Me.Label6.Text = "진료과"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label17.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(3, 4)
        Me.Label17.Margin = New System.Windows.Forms.Padding(0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(92, 21)
        Me.Label17.TabIndex = 52
        Me.Label17.Text = "통계구분"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboStGbn
        '
        Me.cboStGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboStGbn.Items.AddRange(New Object() {"[01] 미생물균 양성자율", "[02] 미생물균에서 항생제 검사", "[03] MRSA 분리율 (%)", "[04] VRE 분리율 (%)", "[05] IRPA 분리율 (%)", "[06] IRAB 분리율 (%)", "[09] VRSA 분리율 (%)", "[11] 미생물  결핵 양성자율 "})
        Me.cboStGbn.Location = New System.Drawing.Point(96, 4)
        Me.cboStGbn.Name = "cboStGbn"
        Me.cboStGbn.Size = New System.Drawing.Size(242, 20)
        Me.cboStGbn.TabIndex = 56
        Me.cboStGbn.Tag = ""
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(3, 26)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 21)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "일별/월별구분"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlSlip
        '
        Me.pnlSlip.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.pnlSlip.Controls.Add(Me.rdoBacGenS)
        Me.pnlSlip.Controls.Add(Me.rdoBacGenA)
        Me.pnlSlip.Location = New System.Drawing.Point(96, 156)
        Me.pnlSlip.Name = "pnlSlip"
        Me.pnlSlip.Size = New System.Drawing.Size(105, 21)
        Me.pnlSlip.TabIndex = 123
        '
        'rdoBacGenS
        '
        Me.rdoBacGenS.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.rdoBacGenS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBacGenS.ForeColor = System.Drawing.Color.Black
        Me.rdoBacGenS.Location = New System.Drawing.Point(54, 1)
        Me.rdoBacGenS.Name = "rdoBacGenS"
        Me.rdoBacGenS.Size = New System.Drawing.Size(46, 19)
        Me.rdoBacGenS.TabIndex = 13
        Me.rdoBacGenS.Text = "선택"
        Me.rdoBacGenS.UseVisualStyleBackColor = False
        '
        'rdoBacGenA
        '
        Me.rdoBacGenA.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.rdoBacGenA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBacGenA.ForeColor = System.Drawing.Color.Black
        Me.rdoBacGenA.Location = New System.Drawing.Point(4, 1)
        Me.rdoBacGenA.Name = "rdoBacGenA"
        Me.rdoBacGenA.Size = New System.Drawing.Size(46, 19)
        Me.rdoBacGenA.TabIndex = 11
        Me.rdoBacGenA.Text = "전체"
        Me.rdoBacGenA.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Beige
        Me.Panel1.Controls.Add(Me.rdoYear)
        Me.Panel1.Controls.Add(Me.rdoMonth)
        Me.Panel1.Controls.Add(Me.rdoDay)
        Me.Panel1.Location = New System.Drawing.Point(96, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(203, 21)
        Me.Panel1.TabIndex = 25
        '
        'rdoYear
        '
        Me.rdoYear.BackColor = System.Drawing.Color.Beige
        Me.rdoYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoYear.Location = New System.Drawing.Point(146, 1)
        Me.rdoYear.Name = "rdoYear"
        Me.rdoYear.Size = New System.Drawing.Size(48, 19)
        Me.rdoYear.TabIndex = 14
        Me.rdoYear.Text = "연별"
        Me.rdoYear.UseVisualStyleBackColor = False
        '
        'rdoMonth
        '
        Me.rdoMonth.BackColor = System.Drawing.Color.Beige
        Me.rdoMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoMonth.Location = New System.Drawing.Point(82, 1)
        Me.rdoMonth.Name = "rdoMonth"
        Me.rdoMonth.Size = New System.Drawing.Size(48, 19)
        Me.rdoMonth.TabIndex = 12
        Me.rdoMonth.Text = "월별"
        Me.rdoMonth.UseVisualStyleBackColor = False
        '
        'rdoDay
        '
        Me.rdoDay.BackColor = System.Drawing.Color.Beige
        Me.rdoDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDay.Location = New System.Drawing.Point(4, 1)
        Me.rdoDay.Name = "rdoDay"
        Me.rdoDay.Size = New System.Drawing.Size(60, 19)
        Me.rdoDay.TabIndex = 11
        Me.rdoDay.Text = "일구간"
        Me.rdoDay.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(3, 48)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 21)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "기준시간 구분"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.AliceBlue
        Me.Panel2.Controls.Add(Me.rdoOptDT3)
        Me.Panel2.Controls.Add(Me.rdoOptDT2)
        Me.Panel2.Controls.Add(Me.rdoOptDT1)
        Me.Panel2.Location = New System.Drawing.Point(96, 48)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(241, 20)
        Me.Panel2.TabIndex = 26
        '
        'rdoOptDT3
        '
        Me.rdoOptDT3.BackColor = System.Drawing.Color.AliceBlue
        Me.rdoOptDT3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOptDT3.Location = New System.Drawing.Point(161, 1)
        Me.rdoOptDT3.Name = "rdoOptDT3"
        Me.rdoOptDT3.Size = New System.Drawing.Size(72, 19)
        Me.rdoOptDT3.TabIndex = 12
        Me.rdoOptDT3.Text = "보고일시"
        Me.rdoOptDT3.UseVisualStyleBackColor = False
        '
        'rdoOptDT2
        '
        Me.rdoOptDT2.BackColor = System.Drawing.Color.AliceBlue
        Me.rdoOptDT2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOptDT2.Location = New System.Drawing.Point(84, 1)
        Me.rdoOptDT2.Name = "rdoOptDT2"
        Me.rdoOptDT2.Size = New System.Drawing.Size(72, 19)
        Me.rdoOptDT2.TabIndex = 13
        Me.rdoOptDT2.Text = "접수일시"
        Me.rdoOptDT2.UseVisualStyleBackColor = False
        '
        'rdoOptDT1
        '
        Me.rdoOptDT1.BackColor = System.Drawing.Color.AliceBlue
        Me.rdoOptDT1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOptDT1.Location = New System.Drawing.Point(4, 1)
        Me.rdoOptDT1.Name = "rdoOptDT1"
        Me.rdoOptDT1.Size = New System.Drawing.Size(72, 19)
        Me.rdoOptDT1.TabIndex = 11
        Me.rdoOptDT1.Text = "처방일시"
        Me.rdoOptDT1.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(3, 70)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 21)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "날짜구간 설정"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDT1
        '
        Me.dtpDT1.CustomFormat = "yyyy-MM-dd"
        Me.dtpDT1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDT1.Location = New System.Drawing.Point(96, 70)
        Me.dtpDT1.Name = "dtpDT1"
        Me.dtpDT1.Size = New System.Drawing.Size(96, 21)
        Me.dtpDT1.TabIndex = 28
        Me.dtpDT1.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'dtpDT2
        '
        Me.dtpDT2.CustomFormat = "yyyy-MM-dd"
        Me.dtpDT2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDT2.Location = New System.Drawing.Point(221, 70)
        Me.dtpDT2.Name = "dtpDT2"
        Me.dtpDT2.Size = New System.Drawing.Size(96, 21)
        Me.dtpDT2.TabIndex = 29
        Me.dtpDT2.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'cboBacGen
        '
        Me.cboBacGen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBacGen.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBacGen.Location = New System.Drawing.Point(202, 156)
        Me.cboBacGen.Name = "cboBacGen"
        Me.cboBacGen.Size = New System.Drawing.Size(137, 20)
        Me.cboBacGen.TabIndex = 60
        Me.cboBacGen.Tag = "TCDGBN_01"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(199, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 16)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "~"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(3, 156)
        Me.Label18.Margin = New System.Windows.Forms.Padding(0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(92, 21)
        Me.Label18.TabIndex = 58
        Me.Label18.Text = "배양균속 구분"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(3, 92)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 21)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "외래/입원구분"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlIO
        '
        Me.pnlIO.BackColor = System.Drawing.Color.Cornsilk
        Me.pnlIO.Controls.Add(Me.rdoIOC)
        Me.pnlIO.Controls.Add(Me.rdoIOO)
        Me.pnlIO.Controls.Add(Me.rdoIOI)
        Me.pnlIO.Controls.Add(Me.rdoIOA)
        Me.pnlIO.Controls.Add(Me.pnlDept)
        Me.pnlIO.Location = New System.Drawing.Point(96, 91)
        Me.pnlIO.Name = "pnlIO"
        Me.pnlIO.Size = New System.Drawing.Size(220, 21)
        Me.pnlIO.TabIndex = 40
        '
        'rdoIOC
        '
        Me.rdoIOC.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOC.ForeColor = System.Drawing.Color.Black
        Me.rdoIOC.Location = New System.Drawing.Point(170, 1)
        Me.rdoIOC.Name = "rdoIOC"
        Me.rdoIOC.Size = New System.Drawing.Size(48, 19)
        Me.rdoIOC.TabIndex = 43
        Me.rdoIOC.Text = "수탁"
        Me.rdoIOC.UseVisualStyleBackColor = False
        '
        'rdoIOO
        '
        Me.rdoIOO.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOO.ForeColor = System.Drawing.Color.Black
        Me.rdoIOO.Location = New System.Drawing.Point(60, 1)
        Me.rdoIOO.Name = "rdoIOO"
        Me.rdoIOO.Size = New System.Drawing.Size(48, 19)
        Me.rdoIOO.TabIndex = 13
        Me.rdoIOO.Text = "외래"
        Me.rdoIOO.UseVisualStyleBackColor = False
        '
        'rdoIOI
        '
        Me.rdoIOI.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOI.ForeColor = System.Drawing.Color.Black
        Me.rdoIOI.Location = New System.Drawing.Point(116, 1)
        Me.rdoIOI.Name = "rdoIOI"
        Me.rdoIOI.Size = New System.Drawing.Size(48, 19)
        Me.rdoIOI.TabIndex = 12
        Me.rdoIOI.Text = "입원"
        Me.rdoIOI.UseVisualStyleBackColor = False
        '
        'rdoIOA
        '
        Me.rdoIOA.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOA.ForeColor = System.Drawing.Color.Black
        Me.rdoIOA.Location = New System.Drawing.Point(4, 1)
        Me.rdoIOA.Name = "rdoIOA"
        Me.rdoIOA.Size = New System.Drawing.Size(48, 19)
        Me.rdoIOA.TabIndex = 11
        Me.rdoIOA.Text = "전체"
        Me.rdoIOA.UseVisualStyleBackColor = False
        '
        'pnlDept
        '
        Me.pnlDept.BackColor = System.Drawing.Color.Honeydew
        Me.pnlDept.Controls.Add(Me.rdoDeptS)
        Me.pnlDept.Controls.Add(Me.rdoDeptA)
        Me.pnlDept.Location = New System.Drawing.Point(78, 20)
        Me.pnlDept.Name = "pnlDept"
        Me.pnlDept.Size = New System.Drawing.Size(104, 21)
        Me.pnlDept.TabIndex = 42
        '
        'rdoDeptS
        '
        Me.rdoDeptS.BackColor = System.Drawing.Color.Honeydew
        Me.rdoDeptS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDeptS.ForeColor = System.Drawing.Color.Black
        Me.rdoDeptS.Location = New System.Drawing.Point(53, 1)
        Me.rdoDeptS.Name = "rdoDeptS"
        Me.rdoDeptS.Size = New System.Drawing.Size(46, 19)
        Me.rdoDeptS.TabIndex = 13
        Me.rdoDeptS.Text = "선택"
        Me.rdoDeptS.UseVisualStyleBackColor = False
        '
        'rdoDeptA
        '
        Me.rdoDeptA.BackColor = System.Drawing.Color.Honeydew
        Me.rdoDeptA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDeptA.ForeColor = System.Drawing.Color.Black
        Me.rdoDeptA.Location = New System.Drawing.Point(4, 1)
        Me.rdoDeptA.Name = "rdoDeptA"
        Me.rdoDeptA.Size = New System.Drawing.Size(46, 19)
        Me.rdoDeptA.TabIndex = 11
        Me.rdoDeptA.Text = "전체"
        Me.rdoDeptA.UseVisualStyleBackColor = False
        '
        'cboDept
        '
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboDept.Location = New System.Drawing.Point(151, 113)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.Size = New System.Drawing.Size(188, 20)
        Me.cboDept.TabIndex = 43
        Me.cboDept.Tag = ""
        '
        'cboWard
        '
        Me.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWard.Location = New System.Drawing.Point(266, 135)
        Me.cboWard.Name = "cboWard"
        Me.cboWard.Size = New System.Drawing.Size(73, 20)
        Me.cboWard.TabIndex = 46
        Me.cboWard.Tag = "TCDGBN_01"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Lavender
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(96, 134)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 21)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "병동 선택"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlWard
        '
        Me.pnlWard.BackColor = System.Drawing.Color.LavenderBlush
        Me.pnlWard.Controls.Add(Me.rdoWardS)
        Me.pnlWard.Controls.Add(Me.rdoWardA)
        Me.pnlWard.Location = New System.Drawing.Point(161, 134)
        Me.pnlWard.Name = "pnlWard"
        Me.pnlWard.Size = New System.Drawing.Size(104, 21)
        Me.pnlWard.TabIndex = 45
        '
        'rdoWardS
        '
        Me.rdoWardS.BackColor = System.Drawing.Color.LavenderBlush
        Me.rdoWardS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWardS.ForeColor = System.Drawing.Color.Black
        Me.rdoWardS.Location = New System.Drawing.Point(53, 1)
        Me.rdoWardS.Name = "rdoWardS"
        Me.rdoWardS.Size = New System.Drawing.Size(46, 19)
        Me.rdoWardS.TabIndex = 13
        Me.rdoWardS.Text = "선택"
        Me.rdoWardS.UseVisualStyleBackColor = False
        '
        'rdoWardA
        '
        Me.rdoWardA.BackColor = System.Drawing.Color.LavenderBlush
        Me.rdoWardA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWardA.ForeColor = System.Drawing.Color.Black
        Me.rdoWardA.Location = New System.Drawing.Point(4, 1)
        Me.rdoWardA.Name = "rdoWardA"
        Me.rdoWardA.Size = New System.Drawing.Size(46, 19)
        Me.rdoWardA.TabIndex = 11
        Me.rdoWardA.Text = "전체"
        Me.rdoWardA.UseVisualStyleBackColor = False
        '
        'grp02
        '
        Me.grp02.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grp02.Controls.Add(Me.chkSameCd)
        Me.grp02.Controls.Add(Me.spdBac)
        Me.grp02.Controls.Add(Me.pnlAntiRst)
        Me.grp02.Controls.Add(Me.Panel3)
        Me.grp02.Controls.Add(Me.Label1)
        Me.grp02.Controls.Add(Me.Label13)
        Me.grp02.Controls.Add(Me.spdTest)
        Me.grp02.Controls.Add(Me.Panel9)
        Me.grp02.Controls.Add(Me.Label21)
        Me.grp02.Controls.Add(Me.lblColor)
        Me.grp02.Location = New System.Drawing.Point(0, 318)
        Me.grp02.Name = "grp02"
        Me.grp02.Size = New System.Drawing.Size(342, 373)
        Me.grp02.TabIndex = 23
        Me.grp02.TabStop = False
        '
        'chkSameCd
        '
        Me.chkSameCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSameCd.AutoSize = True
        Me.chkSameCd.Location = New System.Drawing.Point(3, 358)
        Me.chkSameCd.Name = "chkSameCd"
        Me.chkSameCd.Size = New System.Drawing.Size(102, 16)
        Me.chkSameCd.TabIndex = 136
        Me.chkSameCd.Text = "대표코드 적용"
        Me.chkSameCd.UseVisualStyleBackColor = True
        '
        'spdBac
        '
        Me.spdBac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdBac.DataSource = Nothing
        Me.spdBac.Location = New System.Drawing.Point(3, 209)
        Me.spdBac.Name = "spdBac"
        Me.spdBac.OcxState = CType(resources.GetObject("spdBac.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdBac.Size = New System.Drawing.Size(334, 143)
        Me.spdBac.TabIndex = 121
        '
        'pnlAntiRst
        '
        Me.pnlAntiRst.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlAntiRst.Controls.Add(Me.chkAntiI)
        Me.pnlAntiRst.Controls.Add(Me.chkAntiS)
        Me.pnlAntiRst.Controls.Add(Me.chkAntiR)
        Me.pnlAntiRst.Enabled = False
        Me.pnlAntiRst.Location = New System.Drawing.Point(96, 15)
        Me.pnlAntiRst.Name = "pnlAntiRst"
        Me.pnlAntiRst.Size = New System.Drawing.Size(146, 21)
        Me.pnlAntiRst.TabIndex = 126
        '
        'chkAntiI
        '
        Me.chkAntiI.AutoSize = True
        Me.chkAntiI.Location = New System.Drawing.Point(103, 3)
        Me.chkAntiI.Name = "chkAntiI"
        Me.chkAntiI.Size = New System.Drawing.Size(30, 16)
        Me.chkAntiI.TabIndex = 2
        Me.chkAntiI.Text = "I"
        Me.chkAntiI.UseVisualStyleBackColor = True
        '
        'chkAntiS
        '
        Me.chkAntiS.AutoSize = True
        Me.chkAntiS.Location = New System.Drawing.Point(53, 3)
        Me.chkAntiS.Name = "chkAntiS"
        Me.chkAntiS.Size = New System.Drawing.Size(30, 16)
        Me.chkAntiS.TabIndex = 1
        Me.chkAntiS.Text = "S"
        Me.chkAntiS.UseVisualStyleBackColor = True
        '
        'chkAntiR
        '
        Me.chkAntiR.AutoSize = True
        Me.chkAntiR.Checked = True
        Me.chkAntiR.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAntiR.Location = New System.Drawing.Point(4, 3)
        Me.chkAntiR.Name = "chkAntiR"
        Me.chkAntiR.Size = New System.Drawing.Size(30, 16)
        Me.chkAntiR.TabIndex = 0
        Me.chkAntiR.Text = "R"
        Me.chkAntiR.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Thistle
        Me.Panel3.Controls.Add(Me.rdoBacS)
        Me.Panel3.Controls.Add(Me.rdoBacA)
        Me.Panel3.Location = New System.Drawing.Point(96, 186)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(112, 22)
        Me.Panel3.TabIndex = 120
        '
        'rdoBacS
        '
        Me.rdoBacS.BackColor = System.Drawing.Color.Thistle
        Me.rdoBacS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBacS.ForeColor = System.Drawing.Color.Black
        Me.rdoBacS.Location = New System.Drawing.Point(60, 1)
        Me.rdoBacS.Name = "rdoBacS"
        Me.rdoBacS.Size = New System.Drawing.Size(48, 19)
        Me.rdoBacS.TabIndex = 13
        Me.rdoBacS.Text = "선택"
        Me.rdoBacS.UseVisualStyleBackColor = False
        '
        'rdoBacA
        '
        Me.rdoBacA.BackColor = System.Drawing.Color.Thistle
        Me.rdoBacA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBacA.ForeColor = System.Drawing.Color.Black
        Me.rdoBacA.Location = New System.Drawing.Point(4, 1)
        Me.rdoBacA.Name = "rdoBacA"
        Me.rdoBacA.Size = New System.Drawing.Size(48, 19)
        Me.rdoBacA.TabIndex = 11
        Me.rdoBacA.Text = "전체"
        Me.rdoBacA.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(3, 186)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 22)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "배양균 선택"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(3, 15)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 21)
        Me.Label13.TabIndex = 125
        Me.Label13.Text = "항균제 결과"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdTest
        '
        Me.spdTest.DataSource = Nothing
        Me.spdTest.Location = New System.Drawing.Point(3, 60)
        Me.spdTest.Name = "spdTest"
        Me.spdTest.OcxState = CType(resources.GetObject("spdTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTest.Size = New System.Drawing.Size(334, 123)
        Me.spdTest.TabIndex = 116
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Thistle
        Me.Panel9.Controls.Add(Me.rdoTestS)
        Me.Panel9.Controls.Add(Me.rdoTestA)
        Me.Panel9.Location = New System.Drawing.Point(96, 37)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(112, 21)
        Me.Panel9.TabIndex = 65
        '
        'rdoTestS
        '
        Me.rdoTestS.BackColor = System.Drawing.Color.Thistle
        Me.rdoTestS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTestS.ForeColor = System.Drawing.Color.Black
        Me.rdoTestS.Location = New System.Drawing.Point(60, 1)
        Me.rdoTestS.Name = "rdoTestS"
        Me.rdoTestS.Size = New System.Drawing.Size(48, 19)
        Me.rdoTestS.TabIndex = 13
        Me.rdoTestS.Text = "선택"
        Me.rdoTestS.UseVisualStyleBackColor = False
        '
        'rdoTestA
        '
        Me.rdoTestA.BackColor = System.Drawing.Color.Thistle
        Me.rdoTestA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTestA.ForeColor = System.Drawing.Color.Black
        Me.rdoTestA.Location = New System.Drawing.Point(4, 1)
        Me.rdoTestA.Name = "rdoTestA"
        Me.rdoTestA.Size = New System.Drawing.Size(48, 19)
        Me.rdoTestA.TabIndex = 11
        Me.rdoTestA.Text = "전체"
        Me.rdoTestA.UseVisualStyleBackColor = False
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label21.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(3, 37)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(92, 21)
        Me.Label21.TabIndex = 64
        Me.Label21.Text = "검사 선택"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblColor
        '
        Me.lblColor.AutoSize = True
        Me.lblColor.BackColor = System.Drawing.Color.Gray
        Me.lblColor.ForeColor = System.Drawing.Color.White
        Me.lblColor.Location = New System.Drawing.Point(253, 70)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(47, 12)
        Me.lblColor.TabIndex = 135
        Me.lblColor.Text = "Label12"
        Me.lblColor.Visible = False
        '
        'txtSpcCd
        '
        Me.txtSpcCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSpcCd.Location = New System.Drawing.Point(218, 729)
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.ReadOnly = True
        Me.txtSpcCd.Size = New System.Drawing.Size(337, 21)
        Me.txtSpcCd.TabIndex = 128
        Me.txtSpcCd.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems1
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker2
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1069, 725)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(107, 25)
        Me.btnExit.TabIndex = 203
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems2
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4859813!
        Me.btnClear.FocalPoints.CenterPtY = 0.16!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker4
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(961, 725)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 202
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems3
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
        Me.btnExcel.FocalPoints.FocusPtX = 0.03738318!
        Me.btnExcel.FocalPoints.FocusPtY = 0.04!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker6
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(853, 725)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(107, 25)
        Me.btnExcel.TabIndex = 201
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnSearch.ColorFillBlend = CBlendItems4
        Me.btnSearch.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSearch.Corners.All = CType(6, Short)
        Me.btnSearch.Corners.LowerLeft = CType(6, Short)
        Me.btnSearch.Corners.LowerRight = CType(6, Short)
        Me.btnSearch.Corners.UpperLeft = CType(6, Short)
        Me.btnSearch.Corners.UpperRight = CType(6, Short)
        Me.btnSearch.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSearch.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSearch.FocalPoints.CenterPtX = 0.4859813!
        Me.btnSearch.FocalPoints.CenterPtY = 0.16!
        Me.btnSearch.FocalPoints.FocusPtX = 0.0!
        Me.btnSearch.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.FocusPtTracker = DesignerRectTracker8
        Me.btnSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = Nothing
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.ImageIndex = 0
        Me.btnSearch.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSearch.Location = New System.Drawing.Point(745, 725)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSearch.SideImage = Nothing
        Me.btnSearch.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSearch.Size = New System.Drawing.Size(107, 25)
        Me.btnSearch.TabIndex = 200
        Me.btnSearch.Text = "통계조회"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSearch.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnAnalysis
        '
        Me.btnAnalysis.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnAnalysis.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnAnalysis.ColorFillBlend = CBlendItems5
        Me.btnAnalysis.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnAnalysis.Corners.All = CType(6, Short)
        Me.btnAnalysis.Corners.LowerLeft = CType(6, Short)
        Me.btnAnalysis.Corners.LowerRight = CType(6, Short)
        Me.btnAnalysis.Corners.UpperLeft = CType(6, Short)
        Me.btnAnalysis.Corners.UpperRight = CType(6, Short)
        Me.btnAnalysis.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnAnalysis.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnAnalysis.FocalPoints.CenterPtX = 0.4859813!
        Me.btnAnalysis.FocalPoints.CenterPtY = 0.16!
        Me.btnAnalysis.FocalPoints.FocusPtX = 0.0!
        Me.btnAnalysis.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnAnalysis.FocusPtTracker = DesignerRectTracker10
        Me.btnAnalysis.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnAnalysis.ForeColor = System.Drawing.Color.White
        Me.btnAnalysis.Image = Nothing
        Me.btnAnalysis.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAnalysis.ImageIndex = 0
        Me.btnAnalysis.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnAnalysis.Location = New System.Drawing.Point(4, 725)
        Me.btnAnalysis.Name = "btnAnalysis"
        Me.btnAnalysis.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnAnalysis.SideImage = Nothing
        Me.btnAnalysis.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnAnalysis.Size = New System.Drawing.Size(160, 25)
        Me.btnAnalysis.TabIndex = 204
        Me.btnAnalysis.Text = "미생물 통계분석/재분석"
        Me.btnAnalysis.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnAnalysis.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSave.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnSave.ColorFillBlend = CBlendItems6
        Me.btnSave.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSave.Corners.All = CType(6, Short)
        Me.btnSave.Corners.LowerLeft = CType(6, Short)
        Me.btnSave.Corners.LowerRight = CType(6, Short)
        Me.btnSave.Corners.UpperLeft = CType(6, Short)
        Me.btnSave.Corners.UpperRight = CType(6, Short)
        Me.btnSave.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSave.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSave.FocalPoints.CenterPtX = 0.4859813!
        Me.btnSave.FocalPoints.CenterPtY = 0.16!
        Me.btnSave.FocalPoints.FocusPtX = 0.0!
        Me.btnSave.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSave.FocusPtTracker = DesignerRectTracker12
        Me.btnSave.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Image = Nothing
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSave.ImageIndex = 0
        Me.btnSave.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSave.Location = New System.Drawing.Point(165, 725)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSave.SideImage = Nothing
        Me.btnSave.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSave.Size = New System.Drawing.Size(91, 25)
        Me.btnSave.TabIndex = 205
        Me.btnSave.Text = "설정저장"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSave.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnSave.Visible = False
        '
        'FGT05
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1182, 753)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnAnalysis)
        Me.Controls.Add(Me.txtSpcCd)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.tclStatistics)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGT05"
        Me.Text = "미생물 통계"
        Me.tclStatistics.ResumeLayout(False)
        Me.tpgVar.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.spdStatistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.spdspc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlWkGrp.ResumeLayout(False)
        Me.pnlSlip.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlIO.ResumeLayout(False)
        Me.pnlDept.ResumeLayout(False)
        Me.pnlWard.ResumeLayout(False)
        Me.grp02.ResumeLayout(False)
        Me.grp02.PerformLayout()
        CType(Me.spdBac, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAntiRst.ResumeLayout(False)
        Me.pnlAntiRst.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnExcel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sBuf As String = ""

        With spdStatistics
            .ReDraw = False

            .Col = 2 : .Row = 1 : If .Text = "" Then Exit Sub

            .MaxRows = .MaxRows + 2
            .InsertRows(1, 2)

            .Col = 1 : .Col2 = .MaxCols
            .Row = 1 : .Row2 = 2
            .BlockMode = True
            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .BlockMode = False

            For i As Integer = 1 To .MaxCols
                .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0 : sBuf = .Text
                .Col = i : .Row = 1 : .Text = sBuf

                .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1 : sBuf = .Text
                .Col = i : .Row = 2 : .Text = sBuf
            Next

            If .ExportToExcel("statistics.xls", "Statistics", "") Then
                Process.Start("statistics.xls")
            End If

            .DeleteRows(1, 2)
            .MaxRows -= 2

            .ReDraw = True
        End With
    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Try
            Me.Close()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            fnDisplayStatistics()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub rdoDept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDeptA.CheckedChanged, rdoDeptS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoDeptA.Checked Then
                '전체
                Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False

            ElseIf Me.rdoDeptS.Checked Then
                '선택
                If Not Me.cboDept.Items.Count > 0 Then
                    sbDisplay_dept()
                End If

                If Me.cboDept.Items.Count = 0 Then Return

                Me.cboDept.SelectedIndex = 0 : Me.cboDept.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoIO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIOA.CheckedChanged, rdoIOO.CheckedChanged, rdoIOI.CheckedChanged, rdoIOC.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoIOA.Checked Then
                '전체
                Me.rdoDeptA.Checked = True
                Me.rdoWardA.Checked = True

                Me.pnlDept.Enabled = False
                Me.pnlWard.Enabled = False

                Me.cboDept.Enabled = False
                Me.cboWard.Enabled = False

            ElseIf Me.rdoIOO.Checked Then
                '외래

                Me.rdoWardA.Checked = True
                Me.cboDept.Enabled = True
                Me.pnlDept.Enabled = True
                Me.pnlWard.Enabled = False
                sbDisplay_dept()

            ElseIf Me.rdoIOI.Checked Then
                '입원
                Me.rdoDeptA.Checked = True
                Me.cboDept.Enabled = False
                Me.pnlDept.Enabled = False
                Me.pnlWard.Enabled = True

            ElseIf Me.rdoIOC.Checked Then
                '수탁
                Me.rdoWardA.Checked = True
                Me.cboDept.Enabled = False
                Me.pnlDept.Enabled = True
                Me.pnlWard.Enabled = False


            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub rdoTest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoTestA.CheckedChanged, rdoTestS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoTestA.Checked Then
                '전체
                Me.spdTest.MaxRows = 0

            ElseIf Me.rdoTestS.Checked Then
                '선택
                sbDisplay_test()

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub rdoWard_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoWardA.CheckedChanged, rdoWardS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoWardA.Checked Then
                '전체
                Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False

            ElseIf Me.rdoWardS.Checked Then
                '선택
                If Not Me.cboWard.Items.Count > 0 Then
                    sbDisplay_ward()
                End If

                If Me.cboWard.Items.Count = 0 Then Return

                Me.cboWard.SelectedIndex = 0 : Me.cboWard.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub rdoSpcCd_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSpcCdA.CheckedChanged, rdoSpcCdS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            '전체
            If Me.rdoSpcCdA.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOSPCCDA" Then
                Me.cboSpcCd.SelectedIndex = -1 : Me.cboSpcCd.Enabled = False : Me.txtSpcCd.Text = "" : Me.txtSpcCd.Tag = ""
                Me.spdspc.MaxRows = 0
            ElseIf rdoSpcCdS.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOSPCCDS" Then

                If Not Me.cboSpcCd.Items.Count > 0 Then
                    sbDisplay_spc()
                    sbDisplay_spc_save()
                End If

                If Me.cboSpcCd.Items.Count = 0 Then Return

                Me.cboSpcCd.SelectedIndex = 0 : Me.cboSpcCd.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub cboTest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStGbn.SelectedIndexChanged, cboBacGen.SelectedIndexChanged, cboBacGen.SelectedIndexChanged, cboSpcCd.SelectedIndexChanged

        If miSelectKey = 1 Then Return

        Try
            If CType(sender, Windows.Forms.ComboBox).Name.ToLower = "cbostgbn" Then
                If CType(sender, Windows.Forms.ComboBox).Text.Substring(0, 4) = "[02]" Then
                    pnlAntiRst.Enabled = True
                Else
                    pnlAntiRst.Enabled = False
                End If
            End If

            If CType(sender, Windows.Forms.ComboBox).Name.ToLower = "cbospccd" Then
                If txtSpcCd.Text = "" Then
                    txtSpcCd.Text = Ctrl.Get_Name(Me.cboSpcCd)
                    txtSpcCd.Tag = "'" + Ctrl.Get_Code(Me.cboSpcCd) + "'"
                Else
                    txtSpcCd.Text = txtSpcCd.Text + "," + Ctrl.Get_Name(Me.cboSpcCd)
                    txtSpcCd.Tag = txtSpcCd.Tag.ToString + "," + "'" + Ctrl.Get_Code(Me.cboSpcCd) + "'"
                End If

            End If

            Me.rdoTestA.Checked = True
            Me.spdTest.MaxRows = 0
            Me.rdoBacA.Checked = True
            Me.spdBac.MaxRows = 0

            Me.Panel3.Visible = True
            Me.spdBac.Visible = True
            Me.Label1.Visible = True
            Me.btnSave.Visible = False '설정저장

            If Ctrl.Get_Code(Me.cboStGbn) = "11" Then
                Me.Panel3.Visible = False
                Me.spdBac.Visible = False
                Me.Label1.Visible = False
                Me.btnSave.Visible = True '설정저장

                XmlSetting_Reading() ' JJH xml 저장된 설정값 세팅
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    ' jjh 결핵통계 설정값 불러오기(xml)
    Private Sub XmlSetting_Reading()
        Dim sFn As String = "Private Sub XmlSetting_Reading()"

        Try

            If IO.File.Exists(XmlFile) = False Then Return

            Dim DAY As String = "", OPTDT As String = "", DATES As String = "", DATEE As String = "", IOGBN As String = "", DEPTCD As String = "", WARD_SEL As String = "", WARDCD As String = "", _
                SPCCD_SEL As String = "", SPCCD As String(), TESTCD_SEL As String = "", TESTCD As String()

            DAY = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "DAY")
            OPTDT = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "OPTDT")

            Dim DATE_BUF As String() = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "DATE").Split("^"c)
            DATES = DATE_BUF(0) : DATEE = DATE_BUF(1)

            IOGBN = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "IOGBN")
            DEPTCD = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "DEPTCD")
            WARD_SEL = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "WARDSEL")
            WARDCD = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "WARDCD")
            SPCCD_SEL = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "SPCCDSEL")
            SPCCD = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "SPCCD").Split("^"c)
            TESTCD_SEL = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "TESTCDSEL")
            TESTCD = COMMON.CommXML.getOneElementXML(sDir, XmlFile, "TESTCD").Split("^"c)

            '<< 일별/월별구분
            Select Case DAY
                Case "D" '일구간
                    Me.rdoDay.Checked = True
                Case "M" '월별
                    Me.rdoMonth.Checked = True
                Case "Y" '연별
                    Me.rdoYear.Checked = True
            End Select

            '<< 기준시간 구분
            Select Case OPTDT
                Case "O" '처방일시
                    Me.rdoOptDT1.Checked = True
                Case "T" '접수일시
                    Me.rdoOptDT2.Checked = True
                Case "R" '보고일시
                    Me.rdoOptDT3.Checked = True
            End Select

            '<< 날짜구간 설정
            dtpDT1.Value = CDate(DATES)
            dtpDT2.Value = CDate(DATEE)

            '<< 외래/입원구분
            Select Case IOGBN
                Case "A"
                    Me.rdoIOA.Checked = True
                Case "O"
                    Me.rdoIOO.Checked = True

                    If Not Me.cboDept.Items.Count > 0 Then
                        sbDisplay_dept()
                    End If

                    Me.cboDept.SelectedIndex = Convert.ToInt16(DEPTCD)
                Case "I"
                    Me.rdoIOI.Checked = True

                    If Not Me.cboWard.Items.Count > 0 Then
                        sbDisplay_ward()
                    End If

                    If WARD_SEL = "S" Then
                        Me.rdoWardS.Checked = True
                        Me.cboWard.SelectedIndex = Convert.ToInt16(WARDCD)
                    End If
                Case "C"
                    Me.rdoIOC.Checked = True
            End Select

            If SPCCD_SEL = "A" Then
                Me.rdoSpcCdA.Checked = True
            Else
                Me.rdoSpcCdS.Checked = True
                With spdspc
                    For ix As Integer = 1 To .MaxRows
                        .Row = ix
                        .Col = .GetColFromID("spccd") : Dim spc_buf As String = .Text

                        If SPCCD.Contains(spc_buf) Then
                            .Col = .GetColFromID("chk") : .Text = "1"
                        End If

                    Next
                End With
            End If

            If TESTCD_SEL = "A" Then
                Me.rdoTestA.Checked = True
            Else
                Me.rdoTestS.Checked = True
                With spdTest
                    For ix As Integer = 1 To .MaxRows
                        .Row = ix
                        .Col = .GetColFromID("TESTCD") : Dim test_buf As String = .Text

                        If TESTCD.Contains(test_buf) Then
                            .Col = .GetColFromID("CHK") : .Text = "1"
                        End If

                    Next
                End With
            End If




        Catch ex As Exception
            Fn.log(sFn, Err)
        End Try

    End Sub

    Private Sub spdTest_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdTest.ButtonClicked

        If miSelectKey = 1 Then Return

        Try
            Dim iChkCnt As Integer = 0

            With Me.spdTest
                For i As Integer = 1 To .MaxRows
                    .Col = 1 : .Row = i : Dim sChk As String = .Text

                    If sChk = "1" Then
                        iChkCnt += 1
                    End If
                Next
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub btnAnalysis_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnalysis.Click

        Try
            '> m_fgt02_anal의 Control 값 변경
            If Me.rdoOptDT1.Checked Then
                m_fgt05_anal.lblDay.Text = "처방일자"
            ElseIf rdoOptDT2.Checked Then
                m_fgt05_anal.lblDay.Text = "접수일자" '20131126 정선영 추가
            ElseIf Me.rdoOptDT3.Checked Then
                m_fgt05_anal.lblDay.Text = "보고일자"
            End If

            Dim dtB As Date, dtE As Date

            If Me.rdoDay.Checked Then
                '일별
                dtB = Me.dtpDT1.Value
                dtE = Me.dtpDT2.Value

                m_fgt05_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt05_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            ElseIf Me.rdoMonth.Checked Then
                '월별
                dtB = CDate(Me.dtpDT1.Value.ToString("yyyy-MM") + "-" + "01")
                dtE = CDate(Me.dtpDT2.Value.ToString("yyyy-MM") + "-" + Date.DaysInMonth(Me.dtpDT2.Value.Year, Me.dtpDT2.Value.Month).ToString("00"))

                m_fgt05_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt05_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            ElseIf Me.rdoYear.Checked Then
                '연별
                dtB = CDate(Me.dtpDT1.Value.ToString("yyyy") + "-" + "01-01")
                dtE = CDate(Me.dtpDT2.Value.ToString("yyyy") + "-" + "12-31")

                m_fgt05_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt05_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            End If

            m_fgt05_anal.fnDisplay_ResultOfAnalysis()

            m_fgt05_anal.TopLevel = True
            m_fgt05_anal.Show()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub rdoDayMonthYear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDay.CheckedChanged, rdoMonth.CheckedChanged, rdoYear.CheckedChanged

        If miSelectKey = 1 Then Return

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoDay.Checked Then
                '일별 체크 시
                Me.dtpDT1.CustomFormat = "yyyy-MM-dd"
                Me.dtpDT2.CustomFormat = "yyyy-MM-dd"

            ElseIf Me.rdoMonth.Checked Then
                '월별 체크 시
                Me.dtpDT1.CustomFormat = "yyyy-MM"
                Me.dtpDT2.CustomFormat = "yyyy-MM"

            ElseIf Me.rdoYear.Checked Then
                '연별 체크 시
                Me.dtpDT1.CustomFormat = "yyyy"
                Me.dtpDT2.CustomFormat = "yyyy"

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub rdoBacGen_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoBacGenA.CheckedChanged, rdoBacGenS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoBacGenA.Checked Then
                '전체
                Me.cboBacGen.SelectedIndex = -1 : Me.cboBacGen.Enabled = False
                spdBac.MaxRows = 0

            ElseIf Me.rdoBacGenS.Checked Then
                '선택
                If Not Me.cboBacGen.Items.Count > 0 Then
                    sbDisplay_bacgen()
                End If

                If Me.cboBacGen.Items.Count = 0 Then Return

                Me.cboBacGen.SelectedIndex = 0 : Me.cboBacGen.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub rdoBac_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoBacA.CheckedChanged, rdoBacS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoBacA.Checked Then
                '전체
                Me.spdBac.MaxRows = 0

            ElseIf Me.rdoBacS.Checked Then
                '선택
                sbDisplay_Bac()

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub FGT05_FontChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.FontChanged
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGT05_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_ButtonClick(Nothing, Nothing)
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnClear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdStatistics.MaxRows = 0
    End Sub

    Private Sub rdoOptDT2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoOptDT2.CheckedChanged

    End Sub

    Private Sub FGT05_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DS_FormDesige.sbInti(Me)
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

    End Sub

    Private Sub FGT02_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    '> Control Event
    Private Sub FGT02_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If m_fgt05_anal.mbAnalyzing Then
            e.Cancel = True

            MsgBox(m_fgt05_anal.Text + "을 진행 중이라 종료할 수 없습니다. 나중에 다시 시도하십시요!!", MsgBoxStyle.Exclamation)

            Return
        End If

        If m_fgt05_anal IsNot Nothing Then
            m_fgt05_anal.fbForceClose = True
            m_fgt05_anal.Dispose()
            m_fgt05_anal.Close()
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sFn As String = "btnSave_Click"

        Try

            If IO.File.Exists(XmlFile) Then
                IO.File.Delete(XmlFile)
            End If

            Dim DAY As String = "", OPTDT As String = "", DATES As String = "", DATEE As String = "", IOGBN As String = "", DEPTCD As String = "", WARD_SEL As String = "", WARDCD As String = "", _
                SPCCD_SEL As String = "", SPCCD As String = "", TESTCD_SEL As String = "", TESTCD As String = ""


            '<< 일별/월별구분
            If Me.rdoDay.Checked Then '일구간
                DAY = "D"
            ElseIf Me.rdoMonth.Checked Then '월별
                DAY = "M"
            ElseIf Me.rdoYear.Checked Then '연별
                DAY = "Y"
            End If


            '<< 기준시간 구분
            If Me.rdoOptDT1.Checked Then '처방일시
                OPTDT = "O"
            ElseIf Me.rdoOptDT2.Checked Then '접수일시
                OPTDT = "T"
            ElseIf Me.rdoOptDT3.Checked Then '보고일시
                OPTDT = "R"
            End If


            '<< 날짜구간 설정
            Select Case DAY
                Case "D"
                    DATES = Me.dtpDT1.Value.ToString("yyyy-MM-dd")
                    DATEE = Me.dtpDT2.Value.ToString("yyyy-MM-dd")
                Case "M"
                    DATES = Me.dtpDT1.Value.ToString("yyyy-MM")
                    DATEE = Me.dtpDT2.Value.ToString("yyyy-MM")
                Case "Y"
                    DATES = Me.dtpDT1.Value.ToString("yyyy")
                    DATEE = Me.dtpDT2.Value.ToString("yyyy")
            End Select


            '<< 외래/입원구분
            If Me.rdoIOA.Checked Then '전체
                IOGBN = "A"
            ElseIf Me.rdoIOO.Checked Then '외래
                IOGBN = "O"
            ElseIf Me.rdoIOI.Checked Then '입원
                IOGBN = "I"
            ElseIf Me.rdoIOC.Checked Then '수탁
                IOGBN = "C"
            End If


            '<< 진료과 (외래일때만)
            If IOGBN = "O" Then
                DEPTCD = Me.cboDept.SelectedIndex.ToString
            End If


            '<< 병동 선택 (입원일때만)
            If IOGBN = "I" Then
                WARD_SEL = IIf(Me.rdoWardA.Checked, "A", "S").ToString

                '<< 병동
                If WARD_SEL = "S" Then
                    WARDCD = Me.cboWard.SelectedIndex.ToString
                End If
            End If


            '<< 검체구분
            SPCCD_SEL = IIf(Me.rdoSpcCdA.Checked, "A", "S").ToString

            If SPCCD_SEL = "S" Then

                With spdspc
                    For IX As Integer = 1 To .MaxRows
                        .Row = IX
                        .Col = .GetColFromID("chk") : Dim chk As String = .Text

                        If chk = "1" Then
                            .Col = .GetColFromID("spccd") : Dim spccd_buf As String = .Text
                            If SPCCD <> "" Then
                                SPCCD += "^" + spccd_buf
                            Else
                                SPCCD = spccd_buf
                            End If
                        End If
                    Next
                End With

            End If


            '<< 검사구분
            TESTCD_SEL = IIf(Me.rdoTestA.Checked, "A", "S").ToString

            If TESTCD_SEL = "S" Then
                With spdTest
                    For ix As Integer = 1 To .MaxRows
                        .Row = ix
                        .Col = .GetColFromID("CHK") : Dim chk As String = .Text

                        If chk = "1" Then
                            .Col = .GetColFromID("TESTCD") : Dim testcd_buf As String = .Text
                            If TESTCD <> "" Then
                                TESTCD += "^" + testcd_buf
                            Else
                                TESTCD = testcd_buf
                            End If
                        End If

                    Next

                End With
            End If

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            Dim XmlWrite As Xml.XmlTextWriter = Nothing
            XmlWrite = New Xml.XmlTextWriter(XmlFile, System.Text.Encoding.GetEncoding("utf-8"))

            With XmlWrite
                .Formatting = Xml.Formatting.Indented
                .Indentation = 4
                .IndentChar = " "c
                .WriteStartDocument(False)

                .WriteStartElement("ROOT")
                .WriteStartElement("SET_M.AFB")

                .WriteElementString("DAY", DAY)
                .WriteElementString("OPTDT", OPTDT)
                .WriteElementString("DATE", DATES + "^" + DATEE)
                .WriteElementString("IOGBN", IOGBN)
                .WriteElementString("DEPTCD", DEPTCD)
                .WriteElementString("WARDSEL", WARD_SEL)
                .WriteElementString("WARDCD", WARDCD)
                .WriteElementString("SPCCDSEL", SPCCD_SEL)
                .WriteElementString("SPCCD", SPCCD)
                .WriteElementString("TESTCDSEL", TESTCD_SEL)
                .WriteElementString("TESTCD", TESTCD)

                .WriteEndElement()
                .Close()
            End With

            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "저장되었습니다.")

        Catch ex As Exception
            Fn.log(sFn, Err)
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
