Imports COMMON.CommFN

Public Class FGSECTECT
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGSECTECT.vb, Class : FGSECTECT" & vbTab

    Private mbSave As Boolean = False
    Private msFormId As String = ""
    Private msUsrID As String = ""
    Private msItemGbn As String = ""
    Private msSpcGbn As String = ""
    Private mbMicroBioYn As Boolean = False
    Private mbBloodBankYn As Boolean = False
    Private mbAllPartYn As Boolean = True

    Public Sub Display_Result(ByVal rsFormId As String, ByVal rsUsrId As String, ByVal rsSpcGbn As String, ByVal rsItemGbn As String, _
                              ByVal rbMicroBioYn As Boolean, ByVal rbBloodBankYn As Boolean, ByVal rbAllPartYn As Boolean)
        Dim sFn As String = "Function Display_Result"

        Try
            msFormId = rsFormId
            msUsrID = rsUsrId
            msItemGbn = rsItemGbn
            msSpcGbn = rsSpcGbn
            mbMicroBioYn = rbMicroBioYn
            mbBloodBankYn = rbBloodBankYn
            mbAllPartYn = rbAllPartYn

            Me.ShowDialog()

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_Savenm(ByVal rsItemGbn As String)
        Dim sFn As String = "Sub sbDisplay_savenm"
        Try
            Dim dt As DataTable = DA_ITEM_SAVE.fnGet_Item_SaveList(msFormId, msUsrID, rsItemGbn)

            If dt.Rows.Count < 1 Then Return

            cboSaveNm.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboSaveNm.Items.Add(dt.Rows(ix).Item("savenm").ToString)
            Next

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Slip()
        Dim sFn As String = "Sub sbDisplay_Slip"
        Try
            Dim dt As DataTable = DA_ITEM_SAVE.fnGet_SlipInfo(mbMicroBioYn, mbBloodBankYn, mbAllPartYn)

            If dt.Rows.Count < 1 Then Return

            Me.cboSlipCd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlipCd.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)

                If msItemGbn = dt.Rows(ix).Item("slipcd").ToString Then cboSlipCd.SelectedIndex = ix
            Next

            'If cboSlipCd.Items.Count > 0 Then cboSlipCd.SelectedIndex = 0

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub sbDisplay_Slip_TestList(ByVal rsSlipCd As String)
        Dim sFn As String = "Sub sbDisplay_Slip_TestList"
        Try
            If msItemGbn <> "ALL" Then spdSelList.MaxRows = 0

            Dim dt As DataTable = DA_ITEM_SAVE.fnGet_Slip_TestList(rsSlipCd, msSpcGbn)

            If dt.Rows.Count < 1 Then Return

            With spdTestList
                .ReDraw = True
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                Next
                .ReDraw = False
            End With

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Save_TestList()
        Dim sFn As String = "Sub sbDisplay_Save_TestList"
        Try
            If cboSaveNm.Text = "" Then Return

            Dim dt As DataTable = DA_ITEM_SAVE.fnGet_Item_Save_Test(msFormId, msUsrID, IIf(msItemGbn = "ALL", msItemGbn, Ctrl.Get_Code(cboSlipCd)).ToString, cboSaveNm.Text, msSpcGbn)

            If dt.Rows.Count < 1 Then Return

            With Me.spdSelList
                .ReDraw = True
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                Next
                .ReDraw = False
            End With

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub


    Private Sub sbDisplay_Tgrp_SectTestInfo(ByVal rsTgrpCd As String, ByVal rsSaveNm As String)
        Dim sFn As String = "Sub sbDisplay_Tgrp_TestList"
        Try
            Dim dt As DataTable = DA_ITEM_SAVE.fnGet_Item_Save_Test(msFormId, msUsrID, rsTgrpCd, rsSaveNm, msSpcGbn)

            If dt.Rows.Count < 1 Then Return

            With spdSelList
                .ReDraw = True
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                Next
                .ReDraw = False
            End With

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub FGSECTECT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.Escape
                btnExit_Click(Nothing, Nothing)
            Case Windows.Forms.Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Windows.Forms.Keys.F2
                btnSave_Click(Nothing, Nothing)

        End Select
    End Sub


    Private Sub FGSECTECT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DS_FormDesige.sbInti(Me)

        spdSelList.MaxRows = 0
        spdTestList.MaxRows = 0

        sbDisplay_Slip()
        If msItemGbn = "ALL" Then sbDisplay_Savenm("")

    End Sub

    Private Sub cboSlipCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlipCd.SelectedIndexChanged

        sbDisplay_Slip_TestList(Ctrl.Get_Code(cboSlipCd))

        If msItemGbn <> "ALL" Then sbDisplay_Savenm(Ctrl.Get_Code(cboSlipCd))
        If cboSaveNm.Text <> "" Then sbDisplay_Tgrp_SectTestInfo(Ctrl.Get_Code(cboSlipCd), cboSaveNm.Text)

    End Sub

    Private Sub cboSaveNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboSaveNm.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return
        If Me.cboSaveNm.Text = "" Then Me.spdSelList.MaxRows = 0 : Return

        sbDisplay_Save_TestList()

    End Sub

    Private Sub cboSaveNm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboSaveNm.KeyUp
        For ix As Integer = 0 To cboSaveNm.Items.Count - 1
            If cboSaveNm.Items(ix).ToString = cboSaveNm.Text Then
                cboSaveNm.SelectedIndex = ix
                Exit For
            End If
        Next
    End Sub

    Private Sub cboSaveNm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSaveNm.SelectedIndexChanged
        sbDisplay_Save_TestList()
    End Sub

    Private Sub txtSaveNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return
        If cboSaveNm.Text <> "" Then sbDisplay_Tgrp_SectTestInfo(Ctrl.Get_Code(cboSlipCd), cboSaveNm.Text)

    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Dim sTestCd As String = ""
        Dim sTnmd As String = ""
        Dim sChk As String = ""

        For ix As Integer = 1 To spdTestList.MaxRows
            With spdTestList
                .Row = ix
                .Col = .GetColFromID("testcd") : sTestCd = .Text
                .Col = .GetColFromID("tnmd") : sTnmd = .Text
                .Col = .GetColFromID("chk") : sChk = .Text
            End With

            If sChk = "1" Then
                Dim bFind As Boolean = False
                With spdSelList
                    For ix2 As Integer = 1 To .MaxRows
                        .Row = ix2
                        .Col = .GetColFromID("testcd")
                        If .Text = sTestCd Then
                            bFind = True
                            Exit For
                        End If
                    Next

                    If bFind = False Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("testcd") : .Text = sTestCd
                        .Col = .GetColFromID("tnmd") : .Text = sTnmd
                    End If
                End With
            End If
        Next

        With spdTestList
            .Col = .GetColFromID("chk") : .Col2 = .GetColFromID("chk")
            .Row = 1 : .Row2 = .MaxRows
            .BlockMode = True
            .Action = FPSpreadADO.ActionConstants.ActionClearText
            .BlockMode = False
        End With

    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click

        With spdSelList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                If schk = "1" Then
                    .Row = ix
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1

                    ix -= 1

                End If

                If ix < 0 Then Exit For
            Next
        End With
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If Me.cboSaveNm.Text = "" Then
            MsgBox("저장이름을 입력해 주세요.!!")
            Return
        End If

        Dim sTestCds As String = ""

        With spdSelList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                If ix <> 1 Then sTestCds += ","
                sTestCds += sTestCd
            Next
        End With

        Dim bRet As Boolean = DA_ITEM_SAVE.fnExe_Reg_lf096m(msFormId, msUsrID, IIf(msItemGbn = "ALL", msItemGbn, Ctrl.Get_Code(cboSlipCd)).ToString, Me.cboSaveNm.Text, msSpcGbn, sTestCds)

        If bRet = False Then
            MsgBox("데이타 저장에 실패했습니다.!!")
        Else
            If msItemGbn = "ALL" Then sbDisplay_Savenm(msItemGbn)
            cboSaveNm.Text = ""
            spdSelList.MaxRows = 0
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click

        With spdSelList
            Dim iRow As Integer = .ActiveRow

            If iRow < 2 Then Return

            .Row = iRow
            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
            .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text

            .Row = iRow - 1
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            .Col = .GetColFromID("testcd") : .Text = sTestCd
            .Col = .GetColFromID("tnmd") : .Text = sTnmd

            .Row = iRow + 1
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

            .Row = iRow - 1
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell

        End With
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click

        With spdSelList
            Dim iRow As Integer = .ActiveRow

            If iRow = .MaxRows Then Return

            .Row = iRow
            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
            .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text

            .Row = iRow + 2
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            .Col = .GetColFromID("testcd") : .Text = sTestCd
            .Col = .GetColFromID("tnmd") : .Text = sTnmd

            .Row = iRow
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

            .Row = iRow + 1
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell

        End With
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Me.spdSelList.MaxRows = 0
        Me.spdTestList.MaxRows = 0
        Me.cboSaveNm.Text = ""

    End Sub
End Class

