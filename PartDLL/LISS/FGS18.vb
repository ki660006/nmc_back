'>> 최종보고 수정사유 조회
Imports System.Windows.Forms
Imports COMMON.CommFN 
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Imports LISAPP.APP_S.RstSrh

Public Class FGS18  
    Private Const msXML As String = "\XML"

    Private Const msFile As String = "File : FGS18.vb, Class : S01" & vbTab
    Private msSlipFile As String = Application.StartupPath + msXML + "\FGS18_SLIP.XML"

    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        With spdList
            For ix As Integer = 1 To .MaxCols

                .Row = 0 : .Col = ix
                If .ColHidden = False Then
                    stu_item = New STU_PrtItemInfo


                    If .ColID = "regno" Or .ColID = "patnm" Or .ColID = "sexage" Or .ColID = "deptcd" Or .ColID = "tnmd" Or _
                       .ColID = "fnid" Or .ColID = "fndt" Or .ColID = "viewrst" Or .ColID = "cmtcont" Or .ColID = "bcno" Then
                        stu_item.CHECK = "1"
                    Else
                        stu_item.CHECK = "0"
                    End If

                    If .ColID = "prerst" Then
                        stu_item.TITLE = "수정전 결과"
                    ElseIf .ColID = "prefnid" Then
                        stu_item.TITLE = "수정전 보고자"
                    ElseIf .ColID = "prefndt" Then
                        stu_item.TITLE = "수정전 보고일"
                    ElseIf .ColID = "viewrst" Then
                        stu_item.TITLE = "수정후 결과"
                    ElseIf .ColID = "fnid" Then
                        stu_item.TITLE = "수정후 보고자"
                    ElseIf .ColID = "fndt" Then
                        stu_item.TITLE = "수정전 보고일"
                    Else

                        stu_item.TITLE = .Text
                    End If
                    stu_item.FIELD = .ColID

                    If .ColID = "cmtcont" Then
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10 + 50).ToString
                    Else
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString
                    End If
                    alItems.Add(stu_item)
                End If
            Next

        End With

        Return alItems

    End Function

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim alPrint As New ArrayList

            With Me.spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    Dim sBuf() As String = rsTitle_Item.Split("|"c)
                    Dim alItem As New ArrayList

                    For ix As Integer = 0 To sBuf.Length - 1

                        If sBuf(ix) = "" Then Exit For

                        Dim iCol As Integer = .GetColFromID(sBuf(ix).Split("^"c)(1))

                        If iCol > 0 Then

                            Dim sTitle As String = sBuf(ix).Split("^"c)(0)
                            Dim sField As String = sBuf(ix).Split("^"c)(1)
                            Dim sWidth As String = sBuf(ix).Split("^"c)(2)

                            .Row = iRow
                            .Col = .GetColFromID(sField) : Dim sVal As String = .Text

                            alItem.Add(sVal + "^" + sTitle + "^" + sWidth + "^")
                        End If
                    Next

                    Dim objPat As New FGS00_PATINFO

                    With objPat
                        .alItem = alItem
                    End With

                    alPrint.Add(objPat)
                Next
            End With

            If alPrint.Count > 0 Then
                Dim prt As New FGS00_PRINT

                prt.mbLandscape = True  '-- false : 세로, true : 가로
                prt.msTitle = "최종보고 수정사유 목록"
                prt.maPrtData = alPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbSearch()
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = fnGet_FnModify_List(Me.dtpTkDtS.Text.Replace("-", ""), Me.dtpTkDtE.Text.Replace("-", ""), Ctrl.Get_Code(cboPartSlip))

            Me.spdList.MaxRows = 0
            If dt.Rows.Count < 1 Then Return

            If Not dt Is Nothing Then
                Dim iCol As Integer = 0

                With Me.spdList
                    .ReDraw = False
                    For iRow As Integer = 1 To dt.Rows.Count
                        .MaxRows += 1
                        For iCnt As Integer = 0 To dt.Columns.Count - 1
                            iCol = .GetColFromID(dt.Columns(iCnt).ColumnName.ToLower)

                            If iCol > -1 Then
                                .Col = iCol
                                .Row = .MaxRows

                                .Text = dt.Rows(iRow - 1).Item(dt.Columns(iCnt).ColumnName).ToString().Replace(Chr(13), "").Replace(Chr(10), "").Trim
                            End If
                        Next
                    Next
                    .ReDraw = True
                End With
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim sFn As String = ""

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbSearch()

            Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception

        End Try
    End Sub


    Private Sub FGS18_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS18_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DS_FormDesige.sbInti(Me)

        Me.spdList.MaxRows = 0

        Me.dtpTkDtS.Value = CDate(Format(Now, "yyyy-MM-dd"))
        Me.dtpTkDtE.Value = CDate(Format(Now, "yyyy-MM-dd"))

        Dim dt As DataTable

        dt = LISAPP.COMM.cdfn.fnGet_Slip_List(, True)

        Me.cboPartSlip.Items.Clear()
        Me.cboPartSlip.Items.Add(" - 전체 - ")
        For ix As Integer = 0 To dt.Rows.Count - 1
            cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString.Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString.Trim)
        Next

        Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msSlipFile, "SLIP")

        If Me.cboPartSlip.Items.Count > Val(sTmp) Then Me.cboPartSlip.SelectedIndex = CInt(IIf(sTmp = "", 0, sTmp))


    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim sFn As String = ""

        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = ""

        Try
            Me.spdList.MaxRows = 0

            dtpTkDtS.Value = CDate(Format(Now, "yyyy-MM-dd"))
            dtpTkDtE.Value = CDate(Format(Now, "yyyy-MM-dd"))


        Catch ex As Exception

        End Try
    End Sub


    Private Sub FGS18_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim sFn As String = ""

        Try
            Select Case e.KeyCode
                Case Keys.F4
                    btnClear_Click(Nothing, Nothing)
                Case Keys.Escape
                    btnExit_Click(Nothing, Nothing)
                Case Else

            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged
        Dim sFn As String = ""

        Try
            COMMON.CommXML.setOneElementXML(msXML, msSlipFile, "SLIP", cboPartSlip.SelectedIndex.ToString)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try
            Dim sBuf As String = ""

            With Me.spdList
                .ReDraw = False
                .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

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

                If .ExportToExcel("cancel_list.xls", "cancel list", "") Then
                    Process.Start("cancel_list.xls")
                End If

                .DeleteRows(1, 2)
                .MaxRows -= 2
                .ReDraw = True
            End With
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
        
    End Sub
End Class