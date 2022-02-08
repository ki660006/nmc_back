Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports LISAPP.APP_BT

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports CDHELP.FGCDHELPFN


Public Class FGB27
    Dim m_stdt As String = ""
    Dim m_endt As String = ""
    Dim m_tnsjubsuno As String = ""
    Dim m_seq As String = ""
    Private Sub FGB27_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        sbDisp_Init()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        With spdTnsTranList
            .MaxRows = 0
        End With
    End Sub

    Public Sub sbDisp_Init()
        Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

        Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
        Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")
    End Sub

    Private Sub btnQuery_Click(sender As Object, e As EventArgs) Handles btnQuery.Click

        m_stdt = dtpDateS.Text.Replace("-", "").Replace(" ", "")
        m_endt = dtpDateE.Text.Replace("-", "").Replace(" ", "")

        sbDisplay_Data()
    End Sub

    Private Function fn_Dt_Flag() As String
        Dim sReturn As String = ""

        'radio button 조건
        If Me.chkHB.Checked Then
            sReturn = "hgyn = 'Y'"
        ElseIf Me.chkALL.Checked Then
            sReturn = "cbcyn = 'Y'"
        ElseIf Me.chkALL.Checked Then
            sReturn = "allyn = 'Y'"
        ElseIf Me.chkExc.Checked Then
            sReturn = "ecptyn = 'Y'"
        End If

        Return sReturn

    End Function

    Private Sub sbDisplay_Data()
        Try
            Dim dt As DataTable = CGDA_BT.fnGet_trans_mgt(m_stdt, m_endt)
            Dim tempTnsjubsuno As String = ""
            Dim tempSeq As String = ""

            With Me.spdTnsTranList
                .MaxRows = 0

                If dt.Rows.Count < 1 Then Return

                Dim Dt_Flag As String = fn_Dt_Flag()
                Dim a_dr As DataRow()

                a_dr = dt.Select(Dt_Flag, "")

                dt = Fn.ChangeToDataTable(a_dr)

                .ReDraw = False
                .MaxRows = dt.Rows.Count
                m_tnsjubsuno = ""
                m_seq = ""
                For i As Integer = 1 To dt.Rows.Count
                    tempTnsjubsuno = dt.Rows(i - 1).Item("tnsjubsuno").ToString() ' 현재 로우 수혈접수번호 넣기
                    tempSeq = dt.Rows(i - 1).Item("seq").ToString() ' 현재 로우 수혈접수번호 넣기
                    For j As Integer = 1 To dt.Columns.Count
                        Dim iCol As Integer
                        iCol = .GetColFromID(dt.Columns(j - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            If m_tnsjubsuno = tempTnsjubsuno And m_seq = tempSeq Then ' 현재접수번호와 이전접수번호 비교 및 현재접수번호여도 시퀀스 체크 
                                .Row = i
                                .Text = dt.Rows(i - 1).Item(j - 1).ToString()
                                If sbDisp_column(dt.Columns(j - 1).ColumnName.ToLower()) = False Then '특정 컬럼은 무조건 보여야하는 조건
                                    .ForeColor = Color.White
                                End If
                            Else
                                .Row = i
                                If dt.Columns(j - 1).ColumnName.ToLower() = "chk" Then ' 체크박스 넣는 부분
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox
                                Else ' 글자 넣는 부분
                                    .Text = dt.Rows(i - 1).Item(j - 1).ToString()
                                End If
                            End If
                        End If
                    Next
                    m_tnsjubsuno = tempTnsjubsuno '한 로우전 수혈접수번호 넣기 
                    m_seq = tempSeq
                Next
            End With

            sbDisplay_TnsTotal()

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub
    Private Sub sbDisplay_TnsTotal()
        Try
            Dim dt As DataTable = CGDA_BT.fnGet_trans_mgt_total(m_stdt, m_endt)

            With Me.spdTnsTotal
                .MaxRows = 0
                .MaxRows = 4
                For i = 1 To .MaxRows
                    .Row = i
                    Select Case i
                        Case 1
                            .Col = .GetColFromID("rprttype") : .Text = "Hg>10 g/dL"
                            .Col = .GetColFromID("total") : .Text = dt.Rows(0).Item("hgyn").ToString()
                        Case 2
                            .Col = .GetColFromID("rprttype") : .Text = "CBC F/U"
                            .Col = .GetColFromID("total") : .Text = dt.Rows(0).Item("cbcyn").ToString()
                        Case 3
                            .Col = .GetColFromID("rprttype") : .Text = "모두요청"
                            .Col = .GetColFromID("total") : .Text = dt.Rows(0).Item("allyn").ToString()
                        Case 4
                            .Col = .GetColFromID("rprttype") : .Text = "제외 대상"
                            .Col = .GetColFromID("total") : .Text = dt.Rows(0).Item("ecptyn").ToString()
                    End Select
                Next
            End With
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Function sbDisp_column(ByVal rsColNm As String) As Boolean
        Try
            If rsColNm = "state" Or rsColNm = "outdt" Or rsColNm = "rtndt" Or rsColNm = "affndt" Or rsColNm = "afviewrst" Or rsColNm = "bldno" Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Private Sub btnUpd_Click(sender As Object, e As EventArgs) Handles btnUpd.Click
        Dim chkBool As Boolean = True
        Dim msgContent As String = "Y나 N이 아닙니다. Y나 N을 입력해주세요."
        Dim chkSeq As Integer = 0
        Try
            With spdTnsTranList
                For i = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                    If strChk = "1" And chkBool = True Then
                        chkSeq += 1
                        .Col = .GetColFromID("tnsjubsuno") : Dim sTnsjubsuno As String = .Text
                        .Col = .GetColFromID("hgyn") : Dim sHgyn As String = .Text.ToUpper().Trim
                        .Col = .GetColFromID("allyn") : Dim sAllyn As String = .Text.ToUpper().Trim
                        .Col = .GetColFromID("cbcyn") : Dim sCbcyn As String = .Text.ToUpper().Trim
                        .Col = .GetColFromID("ecptyn") : Dim sEcpyn As String = .Text.ToUpper().Trim
                        .Col = .GetColFromID("cmcaller") : Dim sCmcaller As String = .Text.Trim
                        .Col = .GetColFromID("seq") : Dim sSeq As String = .Text
                        .Col = .GetColFromID("regno") : Dim sRegno As String = .Text

                        If (sHgyn <> "Y") And (sHgyn <> "N") Then chkBool = False
                        If sAllyn <> "Y" And sAllyn <> "N" Then chkBool = False
                        If sCbcyn <> "Y" And sCbcyn <> "N" Then chkBool = False
                        If sEcpyn <> "Y" And sEcpyn <> "N" Then chkBool = False

                        If chkBool = False Then
                            fn_PopMsg(Me, "I"c, "'Y' 나 'N' 만 입력 가능합니다. 다시 입력 해주세요.")
                            Exit For
                        End If

                        Dim tnstran As TnsTranList = New TnsTranList

                        tnstran.TNSJUBSUNO = sTnsjubsuno
                        tnstran.REGNO = sRegno
                        tnstran.HGYN = sHgyn
                        tnstran.ALLYN = sAllyn
                        tnstran.CBCYN = sCbcyn
                        tnstran.ECPYN = sEcpyn
                        tnstran.SEQ = sSeq
                        tnstran.CMCALLER = sCmcaller


                        chkBool = (New TnsReg).fn_trans_mgt_upd(tnstran)
                        If chkBool = False Then
                            fn_PopMsg(Me, "I"c, "수정 중 문제가 발생했습니다. " + vbCrLf + "관리자에게 문의해 주세요.")
                            Exit For
                        End If
                    End If
                Next
                If chkSeq = 0 Then
                    fn_PopMsg(Me, "I"c, "선택한 관리목록이 없습니다. 체크한 후 진행해 주세요.")
                End If
            End With

            sbDisplay_Data()

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim arlDel As New ArrayList
        Dim chkBool As Boolean = True
        Dim msgContent As String = "Y나 N이 아닙니다. Y나 N을 입력해주세요."
        Dim chkSeq As Integer = 0
        Try
            With spdTnsTranList

                If fn_PopConfirm(Me, "E"c, "정말 삭제를 하시겠습니까?") Then
                    For i = 1 To .MaxRows
                        .Row = i
                        .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                        If strChk = "1" And chkBool = True Then
                            chkSeq += 1
                            .Col = .GetColFromID("tnsjubsuno") : Dim sTnsjubsuno As String = .Text
                            .Col = .GetColFromID("seq") : Dim sSeq As String = .Text
                            .Col = .GetColFromID("regno") : Dim sRegno As String = .Text

                            Dim tnstran As TnsTranList = New TnsTranList

                            tnstran.TNSJUBSUNO = sTnsjubsuno
                            tnstran.REGNO = sRegno
                            tnstran.SEQ = sSeq

                            chkBool = (New TnsReg).fn_trans_mgt_del(tnstran)
                            If chkBool = False Then
                                fn_PopMsg(Me, "I"c, "수정 중 문제가 발생했습니다. " + vbCrLf + "관리자에게 문의해 주세요.")
                                Exit For
                            End If
                        End If
                    Next

                    If chkSeq = 0 Then
                        fn_PopMsg(Me, "I"c, "선택한 관리목록이 없습니다. 체크한 후 진행해 주세요.")
                    End If
                Else
                    chkSeq += 1
                End If

            End With

            sbDisplay_Data()

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGB27_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.F4 Then
            btnClear_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Windows.Forms.Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub rdo_Checked_gbx_rboRprtStat(sender As Object, e As EventArgs) Handles chkHB.Click, chkCBC.Click, chkExc.Click, chkALL.Click
        Dim selRdoBtn As CheckBox = CType(sender, CheckBox)

        Dim sRdoTxt As String = selRdoBtn.Text
        Select Case sRdoTxt
            Case "Hb>10g/dL"
                chkALL.Checked = False
                chkCBC.Checked = False
                chkExc.Checked = False
            Case "모두요청"
                chkHB.Checked = False
                chkCBC.Checked = False
                chkExc.Checked = False
            Case "CBC F/U"
                chkHB.Checked = False
                chkALL.Checked = False
                chkExc.Checked = False
            Case "제외대상"
                chkHB.Checked = False
                chkALL.Checked = False
                chkCBC.Checked = False
        End Select

    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            With spdTnsTranList
                .ReDraw = False

                .Row = 1
                .MaxRows += 1
                .Action = FPSpreadADO.ActionConstants.ActionInsertRow

                For intCol As Integer = 1 To .MaxCols
                    .Row = 0 : .Col = intCol : Dim strTmp As String = .Text
                    .Row = 1 : .Col = intCol : .Text = strTmp
                Next

                If spdTnsTranList.MaxRows < 1 Then MsgBox("조회후 출력이 가능합니다.", MsgBoxStyle.Information, Me.Text)
                If spdTnsTranList.ExportToExcel("TnsTranList.xls", "TnsTranList", "") Then Process.Start("TnsTranList.xls")

                .Row = 1
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1

                .ReDraw = True
            End With
        Catch ex As Exception

        End Try

    End Sub
End Class


