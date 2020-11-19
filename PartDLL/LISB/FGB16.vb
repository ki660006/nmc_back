' 혈액입출고현황

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB16
    Public mdTree1 As DataTable
    Public mdTree2 As DataTable

    Private Sub FGB16_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB16_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB16_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-").Substring(0, 7) + "-01")
        Me.dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdIOList)
        DS_SpreadDesige.sbInti(spdDetail)

        Me.spdIOList.MaxRows = 0
        Me.spdDetail.MaxRows = 0
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As New DataTable

        Me.spdIOList.MaxRows = 0
        Me.spdDetail.MaxRows = 0

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ' 조회
            dt = CGDA_BT.fn_InOutBldList(Format(Me.dtpDate0.Value, "yyyyMMdd"), Format(Me.dtpDate1.Value, "yyyyMMdd"))

            sb_DisplayDataList(dt)


        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdIOList.MaxRows = 0
        Me.spdDetail.MaxRows = 0
    End Sub

    Private Sub sb_DisplayDataList(ByVal r_dt As DataTable)
        Try
            With Me.spdIOList
                .MaxRows = 0
                If r_dt.Rows.Count < 2 Then
                    sb_SetStBarSearchCnt(0)
                    Return
                End If

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(ix).Item("comcd").ToString.Trim
                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(ix).Item("comnm").ToString.Trim
                    .Col = .GetColFromID("aip") : .Text = r_dt.Rows(ix).Item("aip").ToString.Trim
                    .Col = .GetColFromID("aop") : .Text = r_dt.Rows(ix).Item("aop").ToString.Trim
                    .Col = .GetColFromID("aim") : .Text = r_dt.Rows(ix).Item("aim").ToString.Trim
                    .Col = .GetColFromID("aom") : .Text = r_dt.Rows(ix).Item("aom").ToString.Trim
                    .Col = .GetColFromID("bip") : .Text = r_dt.Rows(ix).Item("bip").ToString.Trim
                    .Col = .GetColFromID("bop") : .Text = r_dt.Rows(ix).Item("bop").ToString.Trim
                    .Col = .GetColFromID("bim") : .Text = r_dt.Rows(ix).Item("bim").ToString.Trim
                    .Col = .GetColFromID("bom") : .Text = r_dt.Rows(ix).Item("bom").ToString.Trim
                    .Col = .GetColFromID("oip") : .Text = r_dt.Rows(ix).Item("oip").ToString.Trim
                    .Col = .GetColFromID("oop") : .Text = r_dt.Rows(ix).Item("oop").ToString.Trim
                    .Col = .GetColFromID("oim") : .Text = r_dt.Rows(ix).Item("oim").ToString.Trim
                    .Col = .GetColFromID("oom") : .Text = r_dt.Rows(ix).Item("oom").ToString.Trim
                    .Col = .GetColFromID("abip") : .Text = r_dt.Rows(ix).Item("abip").ToString.Trim
                    .Col = .GetColFromID("abop") : .Text = r_dt.Rows(ix).Item("abop").ToString.Trim
                    .Col = .GetColFromID("abim") : .Text = r_dt.Rows(ix).Item("abim").ToString.Trim
                    .Col = .GetColFromID("abom") : .Text = r_dt.Rows(ix).Item("abom").ToString.Trim
                    .Col = .GetColFromID("sumiq") : .Text = r_dt.Rows(ix).Item("sumiq").ToString.Trim
                    .Col = .GetColFromID("sumoq") : .Text = r_dt.Rows(ix).Item("sumoq").ToString.Trim
                Next

                sb_SetStBarSearchCnt(r_dt.Rows.Count - 1)

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdIOList.ReDraw = True

        End Try
    End Sub

    Private Sub spdIOList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdIOList.ClickEvent

        Try
            Dim sComcd As String = ""

            With spdIOList
                If .MaxRows < 1 Then Return

                .Row = e.row
                .Col = .GetColFromID("comcd") : sComcd = .Text
            End With

            Dim dt As DataTable = CGDA_BT.fn_InOutDetail1(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), sComcd)
            mdTree1 = CGDA_BT.fn_InOutDetail2(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), sComcd)
            mdTree2 = CGDA_BT.fn_InOutDetail3(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), sComcd)

            sb_DisplayDetail(dt)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sb_DisplayDetail(ByVal r_dt As DataTable)
        Try
            With Me.spdDetail
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then
                    Return
                End If

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("level1")
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                    .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)
                    .TypePictCenter = True

                    .Col = .GetColFromID("level2")
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                    .Col = .GetColFromID("period") : .Text = r_dt.Rows(ix).Item("period").ToString
                    .Col = .GetColFromID("fwdqty") : .Text = r_dt.Rows(ix).Item("fwdqty").ToString
                    .Col = .GetColFromID("inqty") : .Text = r_dt.Rows(ix).Item("inqty").ToString
                    .Col = .GetColFromID("outqty") : .Text = r_dt.Rows(ix).Item("outqty").ToString
                    .Col = .GetColFromID("remainqty") : .Text = r_dt.Rows(ix).Item("remainqty").ToString
                    .Col = .GetColFromID("sortorder") : .Text = r_dt.Rows(ix).Item("sortorder").ToString
                    .Col = .GetColFromID("tree1") : .Text = r_dt.Rows(ix).Item("tree1").ToString
                    .Col = .GetColFromID("tree2") : .Text = r_dt.Rows(ix).Item("tree2").ToString
                    .Col = .GetColFromID("tlevel") : .Text = r_dt.Rows(ix).Item("tlevel").ToString
                    .Col = .GetColFromID("tree_filter") : .Text = r_dt.Rows(ix).Item("tree_filter").ToString
                    .Col = .GetColFromID("tree_filter2") : .Text = r_dt.Rows(ix).Item("tree_filter2").ToString
                    .Col = .GetColFromID("subcode") : .Text = r_dt.Rows(ix).Item("subcode").ToString
                    .Col = .GetColFromID("subcode2") : .Text = r_dt.Rows(ix).Item("subcode2").ToString
                Next

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdDetail.ReDraw = True

        End Try
    End Sub

    Private Sub spdDetail_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdDetail.ClickEvent
        Dim ls_level As String
        Dim ls_Tree As String
        Dim ls_SubCode As String

        With Me.spdDetail
            .Row = e.row

            If e.col = .GetColFromID("level1") Then
                .Col = .GetColFromID("tlevel") : ls_level = .Text
                .Col = .GetColFromID("tree1") : ls_Tree = .Text

                If ls_level = "1"c Then
                    If ls_Tree = "+" Then
                        .Col = .GetColFromID("tree1") : .Text = "-"c
                        .Col = .GetColFromID("level1")
                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Minus)

                        sb_TreeCreateLevel1()
                    Else
                        .Col = .GetColFromID("tree1") : .Text = "+"c
                        .Col = .GetColFromID("level1")
                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)

                        sb_TreeDeleteLevel1()
                    End If
                Else
                    Return
                End If
            ElseIf e.col = .GetColFromID("level2") Then
                .Col = .GetColFromID("tlevel") : ls_level = .Text
                .Col = .GetColFromID("tree2") : ls_Tree = .Text
                .Col = .GetColFromID("subcode2") : ls_SubCode = .Text

                If ls_level = "2"c Then
                    If ls_Tree = "+" Then
                        .Col = .GetColFromID("tree2") : .Text = "-"c
                        .Col = .GetColFromID("level2")
                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Minus)

                        sb_TreeCreateLevel2(ls_SubCode)
                    Else
                        .Col = .GetColFromID("tree2") : .Text = "+"c
                        .Col = .GetColFromID("level2")
                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)

                        sb_TreeDeleteLevel2(ls_SubCode)
                    End If
                Else
                    Return
                End If
            End If

        End With
    End Sub

    Private Sub sb_TreeCreateLevel1()
        Try
            With spdDetail
                If mdTree1.Rows.Count < 1 Then
                    Return
                End If

                .ReDraw = False

                For i As Integer = 0 To mdTree1.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("level1")
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                    .Col = .GetColFromID("level2")
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                    .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)
                    .TypePictCenter = True

                    .Col = .GetColFromID("period") : .Text = mdTree1.Rows(i).Item("period").ToString
                    .Col = .GetColFromID("fwdqty") : .Text = mdTree1.Rows(i).Item("fwdqty").ToString
                    .Col = .GetColFromID("inqty") : .Text = mdTree1.Rows(i).Item("inqty").ToString
                    .Col = .GetColFromID("outqty") : .Text = mdTree1.Rows(i).Item("outqty").ToString
                    .Col = .GetColFromID("remainqty") : .Text = mdTree1.Rows(i).Item("remainqty").ToString
                    .Col = .GetColFromID("sortorder") : .Text = mdTree1.Rows(i).Item("sortorder").ToString
                    .Col = .GetColFromID("tree1") : .Text = mdTree1.Rows(i).Item("tree1").ToString
                    .Col = .GetColFromID("tree2") : .Text = mdTree1.Rows(i).Item("tree2").ToString
                    .Col = .GetColFromID("tlevel") : .Text = mdTree1.Rows(i).Item("tlevel").ToString
                    .Col = .GetColFromID("tree_filter") : .Text = mdTree1.Rows(i).Item("tree_filter").ToString
                    .Col = .GetColFromID("tree_filter2") : .Text = mdTree1.Rows(i).Item("tree_filter2").ToString
                    .Col = .GetColFromID("subcode") : .Text = mdTree1.Rows(i).Item("subcode").ToString
                    .Col = .GetColFromID("subcode2") : .Text = mdTree1.Rows(i).Item("subcode2").ToString
                Next


                ' 다중 Sort를 위한 설정
                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True
                .SortBy = FPSpreadADO.SortByConstants.SortByRow
                .set_SortKey(1, .GetColFromID("sortorder"))
                .set_SortKey(2, .GetColFromID("period"))
                .set_SortKey(3, .GetColFromID("tlevel"))
                .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(3, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .Action = FPSpreadADO.ActionConstants.ActionSort
                .BlockMode = False

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdDetail.ReDraw = True

        End Try
    End Sub

    Private Sub sb_TreeCreateLevel2(ByVal rsSubCode As String)
        Try
            With Me.spdDetail
                If mdTree2.Rows.Count < 1 Then
                    Return
                End If

                .ReDraw = False

                For ix As Integer = 0 To mdTree2.Rows.Count - 1
                    If mdTree2.Rows(ix).Item("subcode2").ToString = rsSubCode Then
                        .MaxRows += 1
                        .Row = .MaxRows

                        .Col = .GetColFromID("level1")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                        .Col = .GetColFromID("level2")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                        .Col = .GetColFromID("period") : .Text = mdTree2.Rows(ix).Item("period").ToString
                        .Col = .GetColFromID("fwdqty") : .Text = mdTree2.Rows(ix).Item("fwdqty").ToString
                        .Col = .GetColFromID("inqty") : .Text = mdTree2.Rows(ix).Item("inqty").ToString
                        .Col = .GetColFromID("outqty") : .Text = mdTree2.Rows(ix).Item("outqty").ToString
                        .Col = .GetColFromID("remainqty") : .Text = mdTree2.Rows(ix).Item("remainqty").ToString
                        .Col = .GetColFromID("sortorder") : .Text = mdTree2.Rows(ix).Item("sortorder").ToString
                        .Col = .GetColFromID("tree1") : .Text = mdTree2.Rows(ix).Item("tree1").ToString
                        .Col = .GetColFromID("tree2") : .Text = mdTree2.Rows(ix).Item("tree2").ToString
                        .Col = .GetColFromID("tlevel") : .Text = mdTree2.Rows(ix).Item("tlevel").ToString
                        .Col = .GetColFromID("tree_filter") : .Text = mdTree2.Rows(ix).Item("tree_filter").ToString
                        .Col = .GetColFromID("tree_filter2") : .Text = mdTree2.Rows(ix).Item("tree_filter2").ToString
                        .Col = .GetColFromID("subcode") : .Text = mdTree2.Rows(ix).Item("subcode").ToString
                        .Col = .GetColFromID("subcode2") : .Text = mdTree2.Rows(ix).Item("subcode2").ToString
                    End If
                Next


                ' 다중 Sort를 위한 설정
                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True
                .SortBy = FPSpreadADO.SortByConstants.SortByRow
                .set_SortKey(1, .GetColFromID("sortorder"))
                .set_SortKey(2, .GetColFromID("period"))
                .set_SortKey(3, .GetColFromID("tlevel"))
                .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(3, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .Action = FPSpreadADO.ActionConstants.ActionSort
                .BlockMode = False

            End With
        Catch ex As Exception
            Me.spdDetail.ReDraw = True
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdDetail.ReDraw = True

        End Try

    End Sub

    Private Sub sb_TreeDeleteLevel1()

        With Me.spdDetail
            .ReDraw = False

            For i As Integer = .MaxRows To 1 Step -1
                .Row = i
                .Col = .GetColFromID("tlevel") : Dim sLevel As String = .Text

                If sLevel <> "1"c Then
                    .DeleteRows(i, 1)
                    .MaxRows += -1
                End If
            Next

            .ReDraw = True
        End With
    End Sub

    Private Sub sb_TreeDeleteLevel2(ByVal rsDFlg As String)

        With Me.spdDetail
            .ReDraw = False

            For i As Integer = .MaxRows To 1 Step -1
                .Row = i
                .Col = .GetColFromID("tlevel") : Dim sLevel As String = .Text
                .Col = .GetColFromID("subcode2") : Dim sSubCode As String = .Text

                If sLevel = "3"c And sSubCode = rsDFlg Then
                    .DeleteRows(i, 1)
                    .MaxRows += -1
                End If
            Next

            .ReDraw = True
        End With
    End Sub

    Private Sub btnTExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTExcel.Click
        Dim sTime As String = Format(Now, "yyyyMMdd")
        Dim sBuf As String = ""

        With Me.spdIOList
            .ReDraw = False

            .MaxRows += 6 : .InsertRows(1, 6)

            .Row = 1
            .Col = 4
            .Text = "혈액 입고/출고 현황 조회"
            .FontBold = True
            .FontSize = 20
            .ForeColor = System.Drawing.Color.Red

            .Row = 3
            .Col = 3
            .Text = "조회구간 : " & Format(dtpDate0.Value, "yyyy-MM-dd") & " ~ " & Format(dtpDate1.Value, "yyyy-MM-dd")

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 5 : .Row2 = 5
            .Clip = sColHeaders

            For i As Integer = 1 To .MaxCols
                .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1 : sBuf = .Text
                .Col = i : .Row = 6 : .Text = sBuf
            Next

            If spdIOList.ExportToExcel("c:\혈액입출고현황조회_" & sTime & ".xls", "입출고현황조회", "") Then
                Process.Start("c:\혈액입출고현황조회_" & sTime & ".xls")
            End If

            .DeleteRows(1, 6)
            .MaxRows -= 6

            .ReDraw = True
        End With
    End Sub
End Class