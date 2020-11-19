Imports COMMON.CommFN

Public Class FGS09_S01
    Inherits System.Windows.Forms.Form

    Private mbSave As Boolean = False
    Private msResult As String = ""
    Private mbCtTest As Boolean = False
    Private mbMicroBioYn As Boolean = False

    Public Function Display_Result(ByVal rbMicroBioYn As Boolean, ByVal rbCtTest As Boolean, ByVal rbSpcYn As Boolean) As String

        Try
            mbMicroBioYn = rbMicroBioYn
            mbCtTest = rbCtTest
            Me.chkSpc.Checked = rbSpcYn

            Me.ShowDialog()

            Return msResult

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

            Return msResult
        Finally
        End Try

    End Function

    Private Sub sbDisplay_Init()

        With spdTest
            .Col = 1 : .Col2 = .MaxCols
            .Row = 1 : .Row2 = .MaxRows
            .BlockMode = True
            .Action = FPSpreadADO.ActionConstants.ActionClearText
            .BlockMode = False

            .MaxRows = 24
            For ix As Integer = 1 To 24
                .Row = ix
                .Col = .GetColFromID("cid") : .Text = Convert.ToChar(ix + 64)
            Next

            sbDisplay_Slip()
        End With
    End Sub

    Private Sub sbDisplay_Slip()

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List(, , , mbMicroBioYn, , mbCtTest)

            If dt.Rows.Count < 1 Then Return

            Me.cboSlipCd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboSlipCd.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            If Me.cboSlipCd.Items.Count > 0 Then Me.cboSlipCd.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Function fnDisplay_rstcont(ByVal rsTestcd As String) As String

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TestRst_list(rsTestcd)

            If dt.Rows.Count < 1 Then Return ""

            Dim sRstValue As String = ""

            For ix As Integer = 0 To dt.Rows.Count - 1
                sRstValue += dt.Rows(ix).Item("rstcont").ToString + Convert.ToChar(9)
            Next

            Return sRstValue

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
            Return ""
        End Try

    End Function

    Private Function fnGet_CalcForm() As String

        Try
            Dim sCF As String = Me.txtCalc.Text.Trim.Replace(" and ", " AND ").Replace(" or ", " OR ")
            Dim bErrCF As Boolean = False

            bErrCF = Fn.FindErrCalcFormula(sCF.Replace("[", "").Replace("]", "").Replace(" AND ", "+").Replace(" OR ", "-"))

            If bErrCF Then
                MsgBox("계산식에 오류가 있습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)
                Return ""
            End If

            sCF = sCF.Replace(" AND ".ToString, "+")
            sCF = sCF.Replace(" OR ".ToString, "-")
            For ix As Integer = 1 To 24
                sCF = sCF.Replace("[" + Convert.ToChar(64 + ix).ToString + "]", ix.ToString)
            Next

            Try
                With Me.spdCalBuf
                    .Col = 1 : .Row = 1 : .Text = ""
                    .Formula = sCF

                    If IsNumeric(.Text) Then

                    Else
                        MsgBox("계산식에 오류가 있습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)
                        Return ""
                    End If
                End With

                Dim sCalForm As String = Me.txtCalc.Text.Trim
                Dim sCalView As String = Me.txtCalc.Text.Trim
                Dim bNumber As Boolean = False

                With Me.spdTest
                    For ix As Integer = 1 To .MaxRows
                        .Row = ix
                        .Col = .GetColFromID("cid") : Dim sCId As String = .Text
                        .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpccd As String = .Text
                        .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
                        .Col = .GetColFromID("qrygbn") : Dim sQryGbn As String = .Text
                        .Col = .GetColFromID("value") : Dim sValue As String = .Text

                        If sTestcd = "" Then Exit For

                        Select Case sQryGbn.ToLower
                            Case "like *"
                                sCalForm = sCalForm.Replace("[" + sCId + "]", "#TEST = '" + sTestcd + sSpccd + "' AND #ORGRST LIKE '" + sValue + "%'")
                                sCalView = sCalView.Replace("[" + sCId + "]", sTnmd + " " + " LIKE '" + sValue + "%'")
                            Case "* like"
                                sCalForm = sCalForm.Replace("[" + sCId + "]", "#TEST = '" + sTestcd + sSpccd + "' AND #ORGRST LIKE '%" + sValue + "'")
                                sCalView = sCalView.Replace("[" + sCId + "]", sTnmd + " " + sQryGbn + " '%" + sValue + "'")
                            Case "* like *"
                                sCalForm = sCalForm.Replace("[" + sCId + "]", "#TEST = '" + sTestcd + sSpccd + "' AND #ORGRST LIKE '%" + sValue + "%'")
                                sCalView = sCalView.Replace("[" + sCId + "]", sTnmd + " LIKE '%" + sValue + "%'")
                            Case "="
                                sCalForm = sCalForm.Replace("[" + sCId + "]", "#TEST = '" + sTestcd + sSpccd + "' AND #ORGRST " + sQryGbn + " '" + sValue + "'")
                                sCalView = sCalView.Replace("[" + sCId + "]", "" + sTnmd + " " + sQryGbn + " '" + sValue + "'")
                            Case Else
                                bNumber = True
                                sCalForm = sCalForm.Replace("[" + sCId + "]", "#TEST = '" + sTestcd + sSpccd + "' AND TO_NUMBER(#ORGRST) " + sQryGbn + " " + sValue)
                                sCalView = sCalView.Replace("[" + sCId + "]", "" + sTnmd + " " + sQryGbn + " " + sValue)
                        End Select
                    Next
                End With

                If bNumber Then
                    sCalForm = "((" + sCalForm + ") AND trim(translate(#ORGRST, '1234567890.', '           ')) IS NULL" + ")"
                End If
                Return sCalForm + "|" + sCalView

            Catch ex As Exception
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "계산식에 오류가 있습니다. 확인하여 주십시요!!")
                Return ""
            End Try

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Function

    Private Sub FGS09_S01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
        sbDisplay_Init()
    End Sub

    Private Sub spdTest_Clicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdTest.ButtonClicked

        If e.col <> Me.spdTest.GetColFromID("cdhelp_test") Then Return

        Try
            Dim sTestCd As String = Ctrl.Get_Code(Me.spdTest, "testcd", e.row)
            Dim sSpcCd As String = Ctrl.Get_Code(Me.spdTest, "spccd", e.row)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.spdTest) + (e.row * 11)

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.spdTest) + 30

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As New DataTable
            dt = LISAPP.COMM.CdFn.fnGet_testspc_list(Ctrl.Get_Code(cboSlipCd), "", sTestCd, sSpcCd)

            Dim a_dr As DataRow() = dt.Select("titleyn = '0'", "")

            dt = Fn.ChangeToDataTable(a_dr)

            objHelp.FormText = "검사정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            aryList = objHelp.Display_Result(Me, iLeft, iTop, dt)
            If aryList.Count > 0 Then
                With Me.spdTest
                    .Row = e.row
                    .Col = .GetColFromID("testcd") : .Text = aryList.Item(0).ToString.Split("|"c)(0).Trim
                    If Me.chkSpc.Checked Then
                        .Col = .GetColFromID("spccd") : .Text = aryList.Item(0).ToString.Split("|"c)(1).Trim
                    End If
                    .Col = .GetColFromID("tnmd") : .Text = aryList.Item(0).ToString.Split("|"c)(2).Trim

                    .Col = .GetColFromID("value")
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeComboBox
                    .TypeComboBoxEditable = True
                    .TypeComboBoxAutoSearch = FPSpreadADO.TypeComboAutoSearchConstants.TypeComboBoxAutoSearchSingleChar
                    .TypeComboBoxList = fnDisplay_rstcont(aryList.Item(0).ToString.Split("|"c)(0)).Trim

                End With


            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub spdTest_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdTest.KeyDownEvent
        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        With Me.spdTest
            If .ActiveCol = .GetColFromID("testcd") Or .ActiveCol = .GetColFromID("spccd") Then
                spdTest_Clicked(spdTest, New AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent(.GetColFromID("cdhelp_test"), .ActiveRow, 0))
            End If
        End With

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        msResult = ""
        mbSave = False
        Me.Close()

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        msResult = fnGet_CalcForm()

        If msResult <> "" Then
            mbSave = True
            Me.Close()
        End If

    End Sub

End Class