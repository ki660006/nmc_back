Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports PISAPP.DPIS01.Coll_PIS.SData

Imports System.Windows.Forms
Imports System.Drawing

Public Class FGCancel_BC_PIS
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGC02.vb, Class : FGC02010" & vbTab

    Dim mbAction As Boolean = False
    Dim maBcNos As New ArrayList
    Dim mbQueryMode As Boolean = False

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal rsIoGbn As String, ByVal raBcNos As ArrayList) As Boolean

        maBcNos = raBcNos

        Me.spdList.MaxRows = 0
        If raBcNos.Count > 0 Then
            Me.lblNo.Visible = False : Me.txtNo.Visible = False
            Me.spdList.Height += Me.spdList.Top - Me.lblNo.Top
            Me.spdList.Top = Me.lblNo.Top
        Else
            Me.txtNo.Focus()
        End If

        sbDisplay_CmtCont(rsIoGbn)
        sbDisplay_BcnoInfo(raBcNos, False)
        Me.ShowDialog(r_frm)


        Return mbAction
    End Function

    Private Sub sbDisplay_CmtCont(ByVal rsIoGbn As String)
        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_cmtcont_etc(IIf(rsIoGbn = "I", "0", "1").ToString, True)

            Me.cboCancel.Items.Clear()
            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboCancel.Items.Add("[" + dt.Rows(ix).Item("cmtcd").ToString + "] " + dt.Rows(ix).Item("cmtcont").ToString)
            Next

            If Me.cboCancel.Items.Count > 0 Then Me.cboCancel.SelectedIndex = 0

        Catch ex As Exception

        End Try

    End Sub

    Private Sub sbDisplay_BcnoInfo(ByVal raBcNos As ArrayList, ByVal rbAddMode As Boolean)

        Try
            Dim strBcNos As String = ""
            mbQueryMode = True

            For ix As Integer = 0 To raBcNos.Count - 1
                If ix > 0 Then strBcNos += ","

                strBcNos += "'" + raBcNos.Item(ix).ToString + "'"
            Next

            With Me.spdList

                Dim dt As DataTable = fnGet_CollectInfo_bcnos(strBcNos)
                Dim iRow As Integer = 0

                .ReDraw = False

                If rbAddMode Then
                    iRow = .MaxRows
                    .MaxRows += dt.Rows.Count
                Else
                    iRow = 0
                    .MaxRows = dt.Rows.Count
                End If

                If dt.Rows.Count < 1 Then .ReDraw = True : Return

                For ix As Integer = 0 To dt.Rows.Count - 1
                    iRow += 1

                    .Row = iRow
                    .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeScientific : .Text = ""

                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                    .Col = .GetColFromID("testnms") : .Text = dt.Rows(ix).Item("testnms").ToString
                    .Col = .GetColFromID("bcno_fkocs") : .Text = dt.Rows(ix).Item("bcno_fkocs").ToString

                    If dt.Rows(ix).Item("rstflg").ToString <> "0" Then
                        .Col = .GetColFromID("status") : .Text = "결"
                        .BackColor = Me.lblRst.BackColor : .ForeColor = Me.lblRst.ForeColor

                    ElseIf dt.Rows(ix).Item("spcflg").ToString = "3" Then
                        .Col = .GetColFromID("status") : .Text = "접"
                        .BackColor = Me.lblOrdFlgT.BackColor : .ForeColor = Me.lblOrdFlgT.ForeColor

                    ElseIf dt.Rows(ix).Item("spcflg").ToString = "2" Then
                        .Col = .GetColFromID("status") : .Text = "채"
                        .BackColor = Me.lblOrdFlgC.BackColor : .ForeColor = Me.lblOrdFlgC.ForeColor

                        .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox : .Text = "1"

                    ElseIf dt.Rows(ix).Item("spcflg").ToString = "1" Then
                        .Col = .GetColFromID("status") : .Text = "바"
                        .BackColor = Me.lblOrdFlgB.BackColor : .ForeColor = Me.lblOrdFlgB.ForeColor

                        .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox : .Text = "1"

                    Else
                        .Col = .GetColFromID("status") : .Text = ""
                        .BackColor = Me.lblNoColl.BackColor : .ForeColor = Me.lblNoColl.ForeColor
                    End If
                Next

                '-- FKOCS
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk")
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeScientific Then
                        .Col = .GetColFromID("bcno_fkocs") : Dim sBcNo_fkocs As String = .Text + ","

                        For ix2 As Integer = 1 To .MaxRows
                            .Row = ix2
                            .Col = .GetColFromID("chk")
                            If ix <> ix2 And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                                .Col = .GetColFromID("bcno") : Dim sBcno As String = .Text.Replace("-", "").Trim + ","

                                If sBcNo_fkocs.IndexOf(sBcno) >= 0 Then
                                    .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                                End If
                            End If
                        Next
                    End If
                Next

            End With

            mbQueryMode = False
        Catch ex As Exception
            mbQueryMode = False

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mbAction = False
        Me.Close()
    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Try
            Dim sMsgErr As String = ""

            With Me.spdList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")

                    If sChk = "1" Then
                        Dim dt As DataTable = fnGet_Collect_CancelData(sBcNo)
                        Dim arlCancelData As New ArrayList

                        If dt.Rows.Count > 0 Then

                            Dim objCancelInfo As New PISAPP.DPIS01.Coll_PIS.Coll_Cancel_ITEM

                            With objCancelInfo
                                .CANCELRMK = txtCmtCont.Text
                                .BCNO = dt.Rows(0).Item("bcno").ToString
                                .TESTCD = dt.Rows(0).Item("tclscd").ToString
                                .SPCCD = dt.Rows(0).Item("spccd").ToString
                                .TCDGBN = dt.Rows(0).Item("tcdgbn").ToString
                                .IOGBN = dt.Rows(0).Item("iogbn").ToString
                                .FKOCS = dt.Rows(0).Item("fkocs").ToString
                                .OWNGBN = dt.Rows(0).Item("owngbn").ToString
                                .TORDCD = dt.Rows(0).Item("tordcd").ToString
                                .SPCFLG = dt.Rows(0).Item("spcflg").ToString
                                .REGNO = dt.Rows(0).Item("regno").ToString
                            End With


                            With (New PISAPP.DPIS01.Coll_PIS.Exec_Canecl)
                                .CancelRMK = Me.txtCmtCont.Text
                                .CancelCd = Ctrl.Get_Code(cboCancel)

                                ' 관리자 Wittyman만 가능함 MTS적용 유무
                                .NotApplyMTS = False

                                .ExecuteDo(objCancelInfo, USER_INFO.USRID)

                                If .ErrFlag Then
                                    sMsgErr += .ErrMsg + ","
                                End If
                            End With
                        End If
                    End If
                Next
            End With

            If sMsgErr = "" Then
                mbAction = True
                Me.Close()
            Else
                sbDisplay_BcnoInfo(maBcNos, False)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub spdList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdList.ButtonClicked

        If mbQueryMode Then Return
        Dim iRow As Integer = e.row

        With Me.spdList
            If e.col <> .GetColFromID("chk") Then Return

            .Row = iRow
            .Col = .GetColFromID("chk") : Dim sChk As String = .Text
            .Col = .GetColFromID("bcno_fkocs") : Dim sBcNo_fkcos As String = .Text + ","

            For ix As Integer = 1 To .MaxRows
                If iRow <> ix Then
                    .Row = ix
                    .Col = .GetColFromID("chk")
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "").Trim + ","

                        If sBcNo_fkcos.IndexOf(sBcNo) >= 0 Then
                            mbQueryMode = True
                            .Col = .GetColFromID("chk") : .Text = sChk
                            mbQueryMode = False
                        End If
                    End If
                End If
            Next
        End With

    End Sub

    Private Sub FGCancel_BC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
    End Sub

    Private Sub cboCancel_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCancel.SelectedValueChanged

        If Me.cboCancel.Text <> "" Then
            txtCmtCont.Text = Ctrl.Get_Name(cboCancel)
        End If

    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        If e.row < 1 Then Return

        Me.spdList_ButtonClicked(spdList, New AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent(spdList.GetColFromID("chk"), e.row, 0))
    End Sub

    Private Sub txtBcNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.Click
        Me.txtNo.Focus()
        Me.txtNo.SelectAll()
    End Sub

    Private Sub txtBcNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.GotFocus
        Me.txtNo.SelectAll()
    End Sub

    Private Sub txtNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNo.KeyDown
        Dim sFn As String = "txtNo_KeyDown"

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtNo.Text = "" Then Return

        Try
            Dim alBcNo As New ArrayList

            If Me.lblNo.Text = "검체번호" Then
                Dim sBcNo As String = Me.txtNo.Text.Replace("-", "")

                If Len(sBcNo) = 11 Or Len(sBcNo) = 12 Then
                    sBcNo = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(Mid(sBcNo, 1, 11))
                End If

                alBcNo.Add(sBcNo)
            Else

                ' 등록번호는 8자리가 안되는것 0으로 채운다
                If IsNumeric(Me.txtNo.Text.Substring(0, 1)) Then
                    Me.txtNo.Text = Me.txtNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                Else
                    Me.txtNo.Text = Me.txtNo.Text.Substring(0, 1).ToUpper + Me.txtNo.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If

                Dim sRegNo As String = Me.txtNo.Text

                Dim objHelp As New CDHELP.FGCDHELP01
                Dim alList As New ArrayList

                Dim dt As DataTable = LISAPP.APP_J.TkFn.fnGet_Coll_PatInfo(sRegNo, "", "")

                objHelp.FormText = "접수 대상자 조회"
                objHelp.MaxRows = 15
                objHelp.OnRowReturnYN = True

                objHelp.AddField("'' CHK", "", 3, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
                objHelp.AddField("bcno", "검체번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("regno", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("patnm", "성명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("sexage", "성별/나이", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("orddt", "처방일시", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("doctornm", "의뢰의사", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("deptward", "진료과 및 병동", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("tnmds", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

                Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
                Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtNo)

                alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + Me.txtNo.Height + 80, dt)

                If alList.Count = 0 Then Return

                For ix As Integer = 0 To alList.Count - 1
                    alBcNo.Add(alList.Item(ix).ToString.Split("|"c)(0))
                Next
            End If

            sbDisplay_BcnoInfo(alBcNo, True)

            Me.txtBcNo_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
        End Try

    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Dim CommFn As New COMMON.CommFN.Fn
        Fn.SearchToggle(Me.lblNo, btnToggle, enumToggle.BcnoToRegno, Me.txtNo)
        Me.txtNo.Text = ""
        Me.txtNo.Focus()
    End Sub
End Class