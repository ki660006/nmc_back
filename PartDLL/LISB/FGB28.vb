Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports LISAPP.APP_BT

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports CDHELP.FGCDHELPFN

Public Class FGB28

    Private m_stdt As String = ""
    Private m_endt As String = ""
    Private msGwaList As String = ""
    Private msSuHyulList As String = ""
    Private m_tnsjubsuno As String = ""
    Private m_Bldno As String = ""
    Private Sub FGB28_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        sbGet_Data_LisCmt()
        sbGet_Data_ListSuHyul()
        sbDisp_Init()
    End Sub
    Public Sub sbDisp_Init()
        Me.spdList.MaxRows = 0
        Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
        Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

        Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
        Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        With spdList
            .MaxRows = 0
        End With
    End Sub

    Private Sub btnQuery_Click(sender As Object, e As EventArgs) Handles btnQuery.Click
        m_stdt = dtpDateS.Text.Replace("-", "").Replace(" ", "")
        m_endt = dtpDateE.Text.Replace("-", "").Replace(" ", "")

        sbDisplay_Data()
    End Sub
    Private Sub sbGet_Data_LisCmt()
        Dim sFn As String = "Private Sub sbGet_Data_LisCmt"
        Try
            Dim dt As DataTable = CGDA_BT.fnGet_BloodTat_Input_Gwa()

            If dt.Rows.Count > 0 Then
                Dim sCmt As String = "".PadLeft(6, " "c) + Chr(9)
                For iCnt As Integer = 0 To dt.Rows.Count - 1
                    sCmt += dt.Rows(iCnt).Item("clsval").ToString().Trim() + Chr(9)
                Next

                msGwaList = sCmt
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
    Private Sub sbGet_Data_ListSuHyul()
        Dim sFn As String = "Private Sub sbGet_Data_LisCmt"
        Try
            Dim dt As DataTable = CGDA_BT.fnGet_BloodTat_Input_SuHyul()

            If dt.Rows.Count > 0 Then
                Dim sCmt As String = "".PadLeft(6, " "c) + Chr(9)
                For iCnt As Integer = 0 To dt.Rows.Count - 1
                    sCmt += dt.Rows(iCnt).Item("clsval").ToString().Trim() + Chr(9)
                Next

                msSuHyulList = sCmt
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
    Private Sub sbDisplay_Data()
        Try
            Dim dt As DataTable = CGDA_BT.fnGet_BloodTat_Input(m_stdt, m_endt, Me.txtRegno.Text, Me.txtTnsjubsuno.Text)
            Dim tempTnsjubsuno As String = ""
            Dim tempBldno As String = ""

            With Me.spdList
                .MaxRows = 0

                If dt.Rows.Count < 1 Then Return

                .ReDraw = False
                .MaxRows = dt.Rows.Count
                m_tnsjubsuno = ""
                m_Bldno = ""
                For i As Integer = 1 To dt.Rows.Count
                    tempTnsjubsuno = dt.Rows(i - 1).Item("tnsjubsuno").ToString() ' 현재 로우 수혈접수번호 넣기
                    For j As Integer = 1 To dt.Columns.Count
                        Dim iCol As Integer = .GetColFromID(dt.Columns(j - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            If dt.Columns(j - 1).ColumnName.ToLower() = "roomno" Then
                                .Row = i
                                Dim tempList As String = msGwaList
                                Dim gwa As String = dt.Rows(i - 1).Item(j - 1).ToString()
                                Dim tempdt As DataTable = CGDA_BT.fnGet_BloodTat_Input_Gwa(gwa) ' 설정한 데이터가 동일한지 체크 중복 제거 
                                If tempdt.Rows.Count = 0 Then
                                    tempList += gwa + Chr(9)
                                End If

                                .TypeComboBoxList = tempList
                                .Text = dt.Rows(i - 1).Item("seletedRoomno").ToString()
                            ElseIf dt.Columns(j - 1).ColumnName.ToLower() = "vartnsgbn" Then
                                .TypeComboBoxList = msSuHyulList
                                .Text = dt.Rows(i - 1).Item("vartnsgbn").ToString()
                            Else
                                .Row = i
                                .Text = dt.Rows(i - 1).Item(j - 1).ToString()
                                If m_tnsjubsuno = tempTnsjubsuno Then ' 현재접수번호와 이전접수번호 비교 및 현재접수번호여도 시퀀스 체크 
                                    If sbDisp_column(dt.Columns(j - 1).ColumnName.ToLower()) = False Then '특정 컬럼은 무조건 보여야하는 조건
                                        .ForeColor = Color.White
                                    End If
                                End If
                            End If
                        End If
                    Next
                    m_tnsjubsuno = tempTnsjubsuno '한 로우전 수혈접수번호 넣기 
                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
    Private Function sbDisp_column(ByVal rsColNm As String) As Boolean
        Try
            If rsColNm = "bldno" Or rsColNm = "vartnsgbn" Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Private Function fn_Dt_Flag() As String
        Dim sReturn As String = ""

        Return sReturn

    End Function

    Private Sub txtRegno_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRegno.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        Try
            sbDisplay_Data()
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtTnsjubsuno_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTnsjubsuno.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        Try
            sbDisplay_Data()
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnUpd_Click(sender As Object, e As EventArgs) Handles btnUpd.Click
        sbExe_BldTatInput("1")
    End Sub
    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        sbExe_BldTatInput("2")
    End Sub

    'rsgbn = 1 -> 저장 rsgbn = 2 -> 삭제 
    Private Sub sbExe_BldTatInput(ByVal rsGbn As String)
        Dim chkBool As Boolean = True
        Dim msgContent As String = "Y나 N이 아닙니다. Y나 N을 입력해주세요."
        Dim chkSeq As Integer = 0
        Try
            With spdList
                For i = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : Dim strChk As String = .Text

                    If strChk = "1" And chkBool = True Then
                        chkSeq += 1
                        .Col = .GetColFromID("tnsjubsuno") : Dim sTnsjubsuno As String = .Text
                        .Col = .GetColFromID("regno") : Dim sRegno As String = .Text
                        .Col = .GetColFromID("roomno") : Dim sGwa As String = .Text
                        .Col = .GetColFromID("bldno") : Dim sBldno As String = .Text.Replace("-", "")
                        .Col = .GetColFromID("vartnsgbn") : Dim sVartnsgbn As String = IIf(.Text.Trim <> "", "Y", "").ToString

                        Dim rsbldTatInput As BldTatInput = New BldTatInput

                        rsbldTatInput.TNSJUBSUNO = sTnsjubsuno
                        rsbldTatInput.REGNO = sRegno
                        rsbldTatInput.GWA = sGwa
                        rsbldTatInput.BLDNO = sBldno
                        rsbldTatInput.VARYN = sVartnsgbn

                        If rsGbn = "1" Then
                            chkBool = (New TnsReg).fn_BldTat_Input_Upd(rsbldTatInput)
                        ElseIf rsGbn = "2" Then
                            Dim dt As DataTable = CGDA_BT.fnGet_BloodTat_Input_tns(sTnsjubsuno, sRegno, sBldno)
                            If dt.Rows.Count > 0 Then
                                chkBool = (New TnsReg).fn_BldTat_Input_Del(rsbldTatInput)
                            End If
                        End If

                        If chkBool = False Then
                            If rsGbn = "1" Then
                                fn_PopMsg(Me, "I"c, "저장 중 문제가 발생했습니다. " + vbCrLf + "관리자에게 문의해 주세요.")
                            ElseIf rsGbn = "2" Then
                                fn_PopMsg(Me, "I"c, "삭제 중 문제가 발생했습니다. " + vbCrLf + "관리자에게 문의해 주세요.")
                            End If
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

    Private Sub FGB28_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.F4 Then
            btnClear_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Windows.Forms.Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If
    End Sub
End Class