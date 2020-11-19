'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGCOMMON_CTRL.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : 컨트롤 공통 함수 정의 Ctrl                                             */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports System.Windows.Forms

Namespace CommFN

    Public Class Ctrl
        Private Const msFile As String = "File : CGCOMMON_CTRL.vb, Class : CommFN.Ctrl" & vbTab

        Public Shared menuHeight As Integer = 70

        Public Shared color_LightRed As System.Drawing.Color = System.Drawing.Color.MistyRose

        Public Shared spd_redraw_maxrow As Integer = 50

        Public Shared ReadOnly Property frm_borderWidth(ByVal r_frm As Form) As Integer
            Get
                Return CInt((r_frm.Width - r_frm.ClientSize.Width) / 2)
            End Get
        End Property

        Public Shared ReadOnly Property frm_titlebarHeight(ByVal r_frm As Form) As Integer
            Get
                Return r_frm.Height - r_frm.ClientSize.Height
            End Get
        End Property

        Public Shared Sub Excel_Column_Info(ByVal r_ctrl As Control, ByVal r_spd As AxFPSpreadADO.AxfpSpread)
            Dim frmcm As New FGCOMMON01

            Dim spd As AxFPSpreadADO.AxfpSpread = frmcm.spdTemp

            spd.MaxRows = 3
            spd.MaxCols = r_spd.MaxCols + 1

            'Row = 1
            spd.SetText(1, 1, "ColNo")

            For j As Integer = 1 To r_spd.MaxCols
                spd.SetText(j + 1, 1, j.ToString)
            Next

            'Row = 2
            spd.SetText(1, 2, "ColID")

            For j As Integer = 1 To r_spd.MaxCols
                r_spd.Col = j
                Dim sColID As String = r_spd.ColID

                spd.SetText(j + 1, 2, sColID)
            Next

            'Row = 3
            spd.SetText(1, 3, "Header")

            For j As Integer = 1 To r_spd.MaxCols
                r_spd.Col = j
                r_spd.Row = 0
                Dim sColNm As String = r_spd.Text

                spd.SetText(j + 1, 3, sColNm)
            Next

            Dim sFileNm As String = Application.StartupPath + "\" + r_ctrl.Name + "_" + r_spd.Name + ".xls"

            If spd.ExportToExcel(sFileNm, "Spread Info", "") Then
                Process.Start(sFileNm)
            End If
        End Sub

        Public Shared Sub DisplayShowAllCols(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal rbAllCols As Boolean, ByRef r_al_HiddenCols As ArrayList)
            Dim sFn As String = "Public Shared Sub DisplayShowAllCols(AxvaSpread, Boolean, ArrayList)"

            Try
                With r_spd
                    If rbAllCols Then
                        If r_al_HiddenCols Is Nothing Then r_al_HiddenCols = New ArrayList

                        r_al_HiddenCols.Clear()

                        For i As Integer = 1 To .MaxCols
                            .Col = i

                            If .ColHidden Then
                                r_al_HiddenCols.Add(i)

                                .ColHidden = False
                            End If
                        Next
                    Else
                        If r_al_HiddenCols Is Nothing Then Return

                        For i As Integer = 1 To r_al_HiddenCols.Count
                            .Col = CInt(r_al_HiddenCols(i - 1))

                            .ColHidden = True
                        Next
                    End If
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            End Try
        End Sub

        Public Shared Sub DisplayFastAfterSelect(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable, ByVal rsCaseOpt As String)
            Dim sFn As String = "Sub DisplayFastAfterSelect"

            Try
                With r_spd
                    If r_dt Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = r_dt.Rows.Count

                    Dim a_objBuf(.MaxRows - 1, .MaxCols - 1) As Object

                    For c As Integer = 1 To .MaxCols
                        Dim sColId As String = ""

                        .Col = c
                        .Row = 0
                        sColId = .ColID

                        If sColId = "" Then sColId = .Text

                        Dim bEmpty As Boolean = False

                        If sColId = "" Then
                            bEmpty = True
                        End If

                        Dim dc As DataColumn = r_dt.Columns.Item(sColId)

                        If dc Is Nothing Then
                            bEmpty = True

                            If rsCaseOpt = "U" Then
                                dc = r_dt.Columns.Item(sColId.ToUpper)

                                If dc IsNot Nothing Then
                                    sColId = sColId.ToUpper
                                    bEmpty = False
                                End If

                            ElseIf rsCaseOpt = "L" Then
                                dc = r_dt.Columns.Item(sColId.ToLower)

                                If dc IsNot Nothing Then
                                    sColId = sColId.ToLower
                                    bEmpty = False
                                End If

                            End If

                        End If

                        For r As Integer = 1 To r_dt.Rows.Count
                            If bEmpty Then
                                a_objBuf(r - 1, c - 1) = DBNull.Value
                            Else
                                a_objBuf(r - 1, c - 1) = r_dt.Rows(r - 1).Item(sColId)
                            End If
                        Next
                    Next

                    .SetArray(1, 1, a_objBuf)

                    .ReDraw = True
                    .Refresh()
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Function CheckFormObject(ByVal r_frm As Windows.Forms.Form, ByVal rsFrmText As String) As Windows.Forms.Form
            Dim sFn As String = "Function CheckFormObject"

            Dim frm_return As Windows.Forms.Form = Nothing

            Try
                Dim frm_MdiParent As Windows.Forms.Form

                If r_frm.IsMdiContainer Then
                    frm_MdiParent = r_frm
                Else
                    frm_MdiParent = r_frm.MdiParent
                End If

                '< add yjlee 2009-03-30 
                If frm_MdiParent Is Nothing Then Return frm_return
                '> add yjlee 2009-03-30 

                For i As Integer = 1 To frm_MdiParent.OwnedForms.Length
                    Dim sFormNm As String = frm_MdiParent.OwnedForms(i - 1).Text
                    If sFormNm.IndexOf("ː") > 0 Then sFormNm = sFormNm.Substring(sFormNm.IndexOf("ː") + 1)

                    If sFormNm = rsFrmText Then
                        frm_return = frm_MdiParent.OwnedForms(i - 1)

                        Exit For
                    End If
                Next

                For i As Integer = 1 To frm_MdiParent.MdiChildren.Length
                    Dim sFormNm As String = frm_MdiParent.MdiChildren(i - 1).Text
                    If sFormNm.IndexOf("ː") > 0 Then sFormNm = sFormNm.Substring(sFormNm.IndexOf("ː") + 1)

                    If sFormNm = rsFrmText Then
                        frm_return = frm_MdiParent.MdiChildren(i - 1)

                        Exit For
                    End If
                Next

                Return frm_return

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)
                Return Nothing
            End Try
        End Function

        Public Shared Sub CheckNoAll(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer)
            Dim sFn As String = "Sub CheckYesAll"

            Try
                With r_spd
                    .ReDraw = False

                    .Col = riCol : .Col2 = riCol
                    .Row = 1 : .Row2 = .MaxRows
                    .BlockMode = True
                    .Text = ""
                    .BlockMode = False
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub CheckNoAll(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal rbCheckBoxOnly As Boolean)
            Dim sFn As String = "Sub CheckYesAll"

            Try
                If rbCheckBoxOnly = False Then
                    CheckNoAll(r_spd, riCol)

                    Return
                End If

                With r_spd
                    .ReDraw = False

                    For i As Integer = 1 To .MaxRows
                        .Col = riCol
                        .Row = i

                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            .Text = ""
                        End If
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub CheckYesAll(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer)
            Dim sFn As String = "Sub CheckYesAll"

            Try
                Dim iCol As Integer = 0

                With r_spd
                    .ReDraw = False

                    .Row = 1 : iCol = riCol

                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                        .Col = riCol : .Col2 = riCol
                        .Row = 1 : .Row2 = .MaxRows
                        .BlockMode = True
                        .Text = "1"
                        .BlockMode = False

                    End If
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub CheckYesAll(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal rbCheckBoxOnly As Boolean)
            Dim sFn As String = "Sub CheckYesAll"

            Try
                If rbCheckBoxOnly = False Then
                    CheckYesAll(r_spd, riCol)

                    Return
                End If

                With r_spd
                    .ReDraw = False

                    For i As Integer = 1 To .MaxRows
                        .Col = riCol
                        .Row = i

                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            .Text = "1"
                        End If
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub ChangeBackColor(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal riCol2 As Integer, ByVal riRow As Integer, ByVal riRow2 As Integer)
            Dim sFn As String = "Sub ChangeBackColor"

            Try
                With r_spd
                    .ReDraw = False

                    .Col = 1 : .Col2 = .MaxCols
                    .Row = 1 : .Row2 = .MaxRows
                    .BlockMode = True
                    .BackColor = System.Drawing.Color.White
                    .BlockMode = False

                    If riRow < 1 Or riRow2 < 1 Then Return

                    .Col = riCol : .Col2 = riCol2
                    .Row = riRow : .Row2 = riRow2
                    .BlockMode = True
                    .BackColor = System.Drawing.Color.FromArgb(220, 220, 255)
                    .BlockMode = False
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub ChangeColor(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal riRow As Integer)
            Dim sFn As String = "Sub ChangeColor"

            If riCol < 1 Then Return
            If riRow < 1 Then Return

            Try
                With r_spd
                    .Col = riCol
                    .Row = riRow

                    Dim sText As String = .Text
                    Dim sBuf As String = ""
                    If Not .CellTag Is Nothing Then sBuf = .CellTag.ToString()

                    If sBuf = "" Then
                        If sText = sBuf Then
                            .BackColor = System.Drawing.Color.White
                        Else
                            .BackColor = System.Drawing.Color.LemonChiffon
                        End If
                    Else
                        If sText = sBuf Then
                            .BackColor = System.Drawing.Color.White
                        Else
                            .BackColor = System.Drawing.Color.Lavender
                        End If
                    End If
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            End Try
        End Sub

        Public Shared Sub DisplayAfterDelete(ByVal r_spd As AxFPSpreadADO.AxfpSpread)
            Dim sFn As String = "Sub DisplayAfterDelete"

            Try
                With r_spd
                    If .ActiveRow < 1 Then Return

                    .DeleteRows(.ActiveRow, 1)

                    .MaxRows -= 1
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub


        Public Shared Sub DisplayAfterSelect(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable)
            Dim sFn As String = "Sub DisplayAfterSelect"

            Try
                With r_spd
                    If r_dt Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = r_dt.Rows.Count

                    For i As Integer = 1 To r_dt.Rows.Count
                        For j As Integer = 1 To r_dt.Columns.Count
                            Dim iCol As Integer = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToLower())

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i
                                .Text = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim
                                .CellTag = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim
                            End If
                        Next
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DisplayAfterSelect(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable, ByVal rsCaseOpt As String)
            Dim sFn As String = "Sub DisplayAfterSelect"

            Try
                With r_spd
                    If r_dt Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = r_dt.Rows.Count

                    For i As Integer = 1 To r_dt.Rows.Count
                        For j As Integer = 1 To r_dt.Columns.Count
                            Dim iCol As Integer = 0

                            If rsCaseOpt = "U" Then
                                iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToUpper())
                            ElseIf rsCaseOpt = "L" Then
                                iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToLower())
                            Else
                                iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName)
                            End If

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i
                                .Text = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim
                                .CellTag = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim
                            End If
                        Next
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DisplayAfterSelect(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable, ByVal rsCaseOpt As String, ByVal rbTextOnly As Boolean)
            Dim sFn As String = "Sub DisplayAfterSelect"

            Try
                With r_spd
                    If r_dt Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = r_dt.Rows.Count

                    For i As Integer = 1 To r_dt.Rows.Count
                        For j As Integer = 1 To r_dt.Columns.Count
                            Dim iCol As Integer = 0

                            If rsCaseOpt = "U" Then
                                iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToUpper())
                            ElseIf rsCaseOpt = "L" Then
                                iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToLower())
                            Else
                                iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName)
                            End If

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i
                                .Text = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim

                                If rbTextOnly = False Then
                                    .CellTag = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim
                                End If
                            End If
                        Next
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DisplayAfterSelect(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable, ByVal rbTextOnly As Boolean)
            Dim sFn As String = "Sub DisplayAfterSelect"

            Try
                With r_spd
                    If r_dt Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = r_dt.Rows.Count

                    For i As Integer = 1 To r_dt.Rows.Count
                        For j As Integer = 1 To r_dt.Columns.Count
                            Dim iCol As Integer = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToLower())

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i
                                .Text = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim

                                If rbTextOnly = False Then
                                    .CellTag = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim
                                End If
                            End If
                        Next
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DisplayAfterSelect(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal ra_dr As DataRow())
            Dim sFn As String = "Sub DisplayAfterSelect"

            Try
                With r_spd
                    If ra_dr Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = ra_dr.Length

                    For i As Integer = 1 To ra_dr.Length
                        For j As Integer = 1 To ra_dr(i - 1).Table.Columns.Count
                            Dim iCol As Integer = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i
                                .Text = ra_dr(i - 1).Item(j - 1).ToString().Trim
                                .CellTag = ra_dr(i - 1).Item(j - 1).ToString().Trim
                            End If
                        Next
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DisplayAfterSelect(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal ra_dr As DataRow(), ByVal rbTextOnly As Boolean)
            Dim sFn As String = "Sub DisplayAfterSelect"

            Try
                With r_spd
                    If ra_dr Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = ra_dr.Length

                    For i As Integer = 1 To ra_dr.Length
                        '표시 속도와 관련 미리 Redraw
                        If i > spd_redraw_maxrow * ra_dr.Length / 100 Then .ReDraw = True

                        For j As Integer = 1 To ra_dr(i - 1).Table.Columns.Count
                            Dim iCol As Integer = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i
                                .Text = ra_dr(i - 1).Item(j - 1).ToString().Trim

                                If rbTextOnly = False Then
                                    .CellTag = ra_dr(i - 1).Item(j - 1).ToString().Trim
                                End If
                            End If
                        Next
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DisplayAfterSelect(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal ra_dr As DataRow(), ByVal rbTextOnly As Boolean, ByVal rbFastRedraw As Boolean)
            Dim sFn As String = "Sub DisplayAfterSelect"

            Try
                With r_spd
                    If ra_dr Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = ra_dr.Length

                    For i As Integer = 1 To ra_dr.Length
                        If rbFastRedraw Then
                            '표시 속도와 관련 미리 Redraw
                            If i > spd_redraw_maxrow * ra_dr.Length / 100 Then .ReDraw = True
                        End If

                        For j As Integer = 1 To ra_dr(i - 1).Table.Columns.Count
                            Dim iCol As Integer = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower())

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i
                                .Text = ra_dr(i - 1).Item(j - 1).ToString().Trim

                                If rbTextOnly = False Then
                                    .CellTag = ra_dr(i - 1).Item(j - 1).ToString().Trim
                                End If
                            End If
                        Next
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DisplayAfterSelect_Forward(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable)
            Dim sFn As String = "Sub DisplayAfterSelect"

            Try
                With r_spd
                    If r_dt Is Nothing Then
                        .MaxRows = 0

                        Return
                    End If

                    .MaxRows = 0

                    .ReDraw = False

                    .MaxRows = r_dt.Rows.Count

                    For i As Integer = 1 To r_dt.Rows.Count
                        For j As Integer = 1 To r_dt.Columns.Count
                            .Col = j
                            .Row = i
                            .Text = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim
                        Next
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Function FindCheckedItem(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol_Check As Integer, ByVal riCol_Item As Integer) As ArrayList
            Dim al_return As New ArrayList

            Try
                With r_spd
                    For i As Integer = 1 To .MaxRows
                        .Col = riCol_Check
                        .Row = i

                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            If .Text = "1" Then
                                Dim sValue As String = Ctrl.Get_Code(r_spd, riCol_Item, i)
                                If al_return.Contains(sValue) Then
                                Else
                                    al_return.Add(sValue)
                                End If
                            End If
                        End If
                    Next
                End With

                Return al_return

            Catch ex As Exception
                Return al_return

            End Try
        End Function

        Public Shared Function FindChildControl(ByVal actrlCol As System.Windows.Forms.Control.ControlCollection, ByRef r_col As Microsoft.VisualBasic.Collection) As System.Windows.Forms.Control
            Dim sFn$ = "Function FindChildControl"

            Try
                Dim ctrl As System.Windows.Forms.Control

                For Each ctrl In actrlCol
                    If ctrl.Controls.Count > 0 Then

                        FindChildControl(ctrl.Controls, r_col)

                    ElseIf ctrl.Controls.Count = 0 Then

                        If CType(ctrl.Tag, String) <> "" Then
                            r_col.Add(ctrl)
                        End If

                    End If
                Next

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            End Try
        End Function

        Public Shared Function FindControlLeft(ByVal r_ctrl As System.Windows.Forms.Control) As Integer
            Dim ctrl As Control = r_ctrl
            Dim iLeft As Integer = ctrl.Left

            Try
                Do
                    If ctrl.Parent Is Nothing Then
                        Exit Do
                    End If

                    iLeft += ctrl.Parent.Left

                    ctrl = ctrl.Parent
                Loop Until ctrl.Parent Is Nothing

                Return iLeft

            Catch
                Return 0

            End Try
        End Function

        Public Shared Function FindControlTop(ByVal r_ctrl As System.Windows.Forms.Control) As Integer
            Dim ctrl As Control = r_ctrl
            Dim iTop As Integer = ctrl.Top

            Try
                Do
                    If ctrl.Parent Is Nothing Then
                        Exit Do
                    End If

                    iTop += ctrl.Parent.Top

                    ctrl = ctrl.Parent
                Loop Until ctrl.Parent Is Nothing

                Return iTop

            Catch
                Return 0

            End Try
        End Function

        Public Shared Function FindMatchRow(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_al_colid As ArrayList, ByVal r_al_value As ArrayList, ByRef riEndRow As Integer) As Integer
            Dim iStartRow As Integer = 0

            Try
                riEndRow = 0

                If r_al_colid Is Nothing Then Return 0
                If r_al_colid.Count = 0 Then Return 0

                If r_al_value Is Nothing Then Return 0
                If r_al_value.Count = 0 Then Return 0

                If r_al_colid.Count <> r_al_value.Count Then Return 0

                With r_spd
                    For r As Integer = 1 To .MaxRows
                        Dim al_value_spd As New ArrayList

                        For i As Integer = 1 To r_al_colid.Count
                            al_value_spd.Add(Get_Code(r_spd, r_al_colid(i - 1).ToString(), r))
                        Next

                        Dim iMatchCnt As Integer = 0

                        For i As Integer = 1 To r_al_colid.Count
                            If al_value_spd(i - 1).ToString() = r_al_value(i - 1).ToString() Then
                                iMatchCnt += 1
                            End If
                        Next

                        If iMatchCnt = r_al_colid.Count Then
                            If iStartRow = 0 Then iStartRow = r

                            riEndRow = r
                        Else
                            If iStartRow > 0 Then Exit For
                        End If

                        al_value_spd = Nothing
                    Next
                End With

                Return iStartRow
            Catch
                Return 0
            End Try
        End Function

        Public Shared Function Get_Name(ByVal r_cbo As System.Windows.Forms.ComboBox) As String
            Dim sFn As String = "Function Get_Name"

            Try
                Dim sCd As String = ""

                If Not r_cbo.SelectedItem Is Nothing Then sCd = r_cbo.SelectedItem.ToString()

                If sCd.IndexOf("[") < 0 Or sCd.IndexOf("]") < 0 Then
                    sCd = sCd
                Else
                    sCd = sCd.Substring(sCd.IndexOf("]") + 1).ToString
                End If

                Return sCd.Trim

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Name(ByVal rsBuf As String) As String
            Dim sFn As String = "Function Get_Name"

            Try
                Dim sCd As String = rsBuf

                If sCd.IndexOf("[") < 0 Or sCd.IndexOf("]") < 0 Then
                    sCd = sCd
                Else
                    sCd = sCd.Substring(sCd.IndexOf("]") + 1).ToString
                End If

                Return sCd.Trim

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code(ByVal rsBuf As String) As String
            Dim sFn As String = "Function Get_Code"

            Try
                Dim sCd As String = rsBuf

                If sCd.IndexOf("[") < 0 Or sCd.IndexOf("]") < 0 Then
                    sCd = ""
                Else
                    sCd = sCd.Substring(sCd.IndexOf("[") + 1, sCd.IndexOf("]") - (sCd.IndexOf("[") + 1))
                End If

                Return sCd.Trim

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code(ByVal r_cbo As System.Windows.Forms.ComboBox) As String
            Dim sFn As String = "Function Get_Code"

            Try
                Dim sCd As String = ""

                If Not r_cbo.SelectedItem Is Nothing Then sCd = r_cbo.SelectedItem.ToString()

                If sCd.IndexOf("[") < 0 Or sCd.IndexOf("]") < 0 Then
                    sCd = ""
                Else
                    sCd = sCd.Substring(sCd.IndexOf("[") + 1, sCd.IndexOf("]") - (sCd.IndexOf("[") + 1))
                End If

                Return sCd.Trim

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목.
        Public Shared Function Get_Comcd(ByVal rsBuf As String) As String
            Dim sFn As String = "Function Get_Code"
            Dim a As Integer
            Dim b As Integer

            a = rsBuf.IndexOf("/")

            Try
                Dim sCd As String = rsBuf

                sCd = rsBuf.Substring(0, a)


                Return sCd.Trim

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목.
        Public Shared Function Get_Comcd2(ByVal rsBuf As String) As String
            Dim sFn As String = "Function Get_Code"
            Dim a As Integer
            Dim b As Integer

            a = rsBuf.IndexOf("/")

            Try
                Dim sCd As String = rsBuf

                If rsBuf.Length < 7 Then

                    sCd = ""
                Else
                    sCd = rsBuf.Substring(a + 1, 5)

                End If

                Return sCd.Trim

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function


        Public Shared Function Get_Code(ByVal r_ctrl As System.Windows.Forms.Control) As String
            Dim sFn As String = "Function Get_Code"

            Try
                Dim sCd As String = r_ctrl.Text

                If sCd.IndexOf("[") < 0 Or sCd.IndexOf("]") < 0 Then
                    sCd = ""
                Else
                    sCd = sCd.Substring(sCd.IndexOf("[") + 1, sCd.IndexOf("]") - (sCd.IndexOf("[") + 1))
                End If

                Return sCd.Trim

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal rsColID As String, ByVal riRow As Integer) As String
            Dim sFn As String = "Function Get_Code"

            Try
                Dim sCd As String = ""

                With r_spd
                    .Col = .GetColFromID(rsColID)
                    .Row = riRow
                    sCd = .Text
                End With

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try

        End Function

        Public Shared Function Get_Code(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal rsColID As String, ByVal riRow As Integer, ByVal rbBracketed As Boolean) As String
            Dim sFn As String = "Function Get_Code"

            Try
                Dim sCd As String = ""

                With r_spd
                    .Col = .GetColFromID(rsColID)
                    .Row = riRow
                    sCd = .Text
                End With

                If rbBracketed Then
                    If sCd.IndexOf("[") < 0 Or sCd.IndexOf("]") < 0 Then
                        sCd = ""
                    Else
                        sCd = sCd.Substring(sCd.IndexOf("[") + 1, sCd.IndexOf("]") - (sCd.IndexOf("[") + 1))
                    End If
                End If

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal riRow As Integer) As String
            Dim sFn As String = "Function Get_Code"

            Try
                Dim sCd As String = ""

                With r_spd
                    .Col = riCol
                    .Row = riRow
                    sCd = .Text
                End With

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal riRow As Integer, ByVal rbBracketed As Boolean) As String
            Dim sFn As String = "Function Get_Code"

            Try
                Dim sCd As String = ""

                With r_spd
                    .Col = riCol
                    .Row = riRow
                    sCd = .Text
                End With

                If rbBracketed Then
                    If sCd.IndexOf("[") < 0 Or sCd.IndexOf("]") < 0 Then
                        sCd = ""
                    Else
                        sCd = sCd.Substring(sCd.IndexOf("[") + 1, sCd.IndexOf("]") - (sCd.IndexOf("[") + 1))
                    End If
                End If

                Return sCd.Trim

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code_Tag(ByVal r_ctrl As System.Windows.Forms.Control) As String
            Dim sFn As String = "Function Get_Code_Tag"

            Try
                Dim sCd As String = ""

                If Not r_ctrl.Tag Is Nothing Then sCd = r_ctrl.Tag.ToString()

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code_Tag(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal rsColID As String, ByVal riRow As Integer) As String
            Dim sFn As String = "Function Get_Code_Tag"

            Try
                Dim sCd As String = ""

                With r_spd
                    .Col = .GetColFromID(rsColID)
                    .Row = riRow
                    If Not .CellTag Is Nothing Then sCd = .CellTag
                End With

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code_Tag(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal riRow As Integer) As String
            Dim sFn As String = "Function Get_Code_Tag"

            Try
                Dim sCd As String = ""

                With r_spd
                    .Col = riCol
                    .Row = riRow
                    If Not .CellTag Is Nothing Then sCd = .CellTag
                End With

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Item(ByVal r_cbo As System.Windows.Forms.ComboBox) As String
            Dim sFn As String = "Function Get_Item"

            Try
                Dim sCd As String = ""

                If Not r_cbo.SelectedItem Is Nothing Then sCd = r_cbo.SelectedItem.ToString()

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Sub Set_ToolTip(ByVal r_ctrl As Windows.Forms.Control, ByVal rsText As String, ByVal r_tp As ToolTip)
            Dim sFn As String = "Sub Set_ToolTip"

            Try
                Dim sngTextWidth As Single = r_ctrl.CreateGraphics.MeasureString(rsText, r_ctrl.Font).Width
                Dim sngTextHeight As Single = r_ctrl.CreateGraphics.MeasureString(rsText, r_ctrl.Font).Height

                If r_ctrl.Height < sngTextHeight Then
                    r_tp.SetToolTip(r_ctrl, rsText)

                    Return
                End If

                If r_ctrl.Width < sngTextWidth Then
                    r_tp.SetToolTip(r_ctrl, rsText)

                    For i As Integer = rsText.Length To 1 Step -1
                        Dim sBuf As String = rsText.Substring(0, i) + "..."

                        If r_ctrl.Width > r_ctrl.CreateGraphics.MeasureString(sBuf, r_ctrl.Font).Width Then
                            r_ctrl.Text = sBuf
                            Return
                        End If
                    Next
                    '< yjlee 2010-06-15 현재 텍스트의 길이가 이전 텍스트의 길이보다 작을 경우 이전텍스트 ToolTip이 보이므로
                    ' 무조건 자신의 ToolTip으로 변경 
                Else
                    r_tp.SetToolTip(r_ctrl, rsText)
                    '> yjlee 2010-06-15  
                End If

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            End Try
        End Sub

        Public Shared Sub ToggleCheck(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal rbCheckBoxOnly As Boolean)
            Dim sFn As String = "Sub ToggleCheck"

            Try
                With r_spd
                    .ReDraw = False

                    Dim iCheckedRow As Integer = .SearchCol(riCol, 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iCheckedRow > 0 Then
                        'Uncheck
                        .Col = riCol : .Col2 = riCol
                        .Row = 1 : .Row2 = .MaxRows
                        .BlockMode = True
                        .Text = ""
                        .BlockMode = False
                    Else
                        'Check
                        If rbCheckBoxOnly Then
                            For i As Integer = 1 To .MaxRows
                                .Col = riCol
                                .Row = i

                                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                                    .Text = "1"
                                End If
                            Next
                        Else
                            .Col = riCol : .Col2 = riCol
                            .Row = 1 : .Row2 = .MaxRows
                            .BlockMode = True
                            .Text = "1"
                            .BlockMode = False
                        End If
                    End If
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DisplayAfterSelect_Append(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable, ByVal rsCaseOpt As String, ByVal rbTextOnly As Boolean, ByVal rbFastRedraw As Boolean, _
                                                        ByVal r_al_CompId As ArrayList)
            Dim sFn As String = "Sub DisplayAfterSelect_Append"

            Try
                With r_spd
                    If r_dt Is Nothing Then
                        Return
                    End If

                    .ReDraw = False

                    For i As Integer = 1 To r_dt.Rows.Count
                        If rbFastRedraw Then
                            '표시 속도와 관련 미리 Redraw
                            If i > spd_redraw_maxrow * r_dt.Rows.Count / 100 Then .ReDraw = True
                        End If

                        Dim al_CompVal As New ArrayList
                        Dim sCompVal_tot As String = ""

                        For j As Integer = 1 To r_al_CompId.Count
                            Dim iC As Integer = .GetColFromID(r_al_CompId(j - 1).ToString)

                            If iC < 1 Then
                                MsgBox("Column ID 오류가 발생하였습니다 -> " + r_al_CompId(j - 1).ToString, MsgBoxStyle.Exclamation)

                                Return
                            End If

                            If sCompVal_tot.Length > 0 Then sCompVal_tot += ", "

                            sCompVal_tot += r_dt.Rows(i - 1).Item(r_al_CompId(j - 1).ToString).ToString

                            al_CompVal.Add(r_dt.Rows(i - 1).Item(r_al_CompId(j - 1).ToString))
                        Next

                        Dim iMatchRow As Integer = FindMatchRow(r_spd, r_al_CompId, al_CompVal, 0)

                        If iMatchRow > 0 Then
                            MsgBox("동일한 내용(" + sCompVal_tot + ")이 " + iMatchRow.ToString + " 행에 존재하므로 추가하지 않습니다.", MsgBoxStyle.Exclamation)
                        Else
                            .MaxRows += 1

                            .Col = 1 : .Col2 = .MaxCols
                            .Row = .MaxRows : .Row2 = .MaxRows
                            .BlockMode = True
                            .FontBold = True
                            .BlockMode = False

                            For j As Integer = 1 To r_dt.Columns.Count
                                Dim iCol As Integer = 0

                                If rsCaseOpt = "U" Then
                                    iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToUpper())
                                ElseIf rsCaseOpt = "L" Then
                                    iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToLower())
                                Else
                                    iCol = .GetColFromID(r_dt.Columns(j - 1).ColumnName)
                                End If

                                If iCol > 0 Then
                                    .Col = iCol
                                    .Row = .MaxRows
                                    .Text = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim

                                    If rbTextOnly = False Then
                                        .CellTag = r_dt.Rows(i - 1).Item(j - 1).ToString().Trim
                                    End If
                                End If
                            Next
                        End If
                    Next
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        '< yjlee 2090-01-21 
        Public Shared Sub CheckSelRow(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal riRowB As Integer, ByVal riRowE As Integer, ByVal rbChk As Boolean)
            Dim sFn As String = "Sub CheckSelRow"

            Try
                If riRowB < 1 Then Return
                If riRowE < 1 Then Return
                If riRowB > riRowE Then Return

                If riCol < 1 Then Return

                With r_spd
                    .ReDraw = False

                    .Col = riCol : .Col2 = riCol
                    .Row = riRowB : .Row2 = riRowE
                    .BlockMode = True
                    .Text = IIf(rbChk, "1", "").ToString
                    .BlockMode = False

                    .ReDraw = True
                    .Refresh()
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Sub DeleteCheckedRow(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer)
            Dim sFn As String = "Sub DeleteCheckedRow"

            If riCol < 1 Then Return

            Try
                With r_spd
                    If .ActiveCol = riCol Then
                        If riCol < .MaxCols Then
                            .SetActiveCell(riCol + 1, .ActiveRow)
                        Else
                            If riCol > 1 Then
                                .SetActiveCell(riCol - 1, .ActiveRow)
                            End If
                        End If

                        .SetActiveCell(riCol, .ActiveRow)
                    End If

                    .ReDraw = False

                    For i As Integer = .MaxRows To 1 Step -1
                        .Col = riCol
                        .Row = i

                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            If .Text = "1" Then
                                .DeleteRows(i, 1)
                                .MaxRows -= 1
                            End If
                        End If
                    Next

                    .ReDraw = True
                    .Refresh()
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Function Get_Code_Note(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal rsColID As String, ByVal riRow As Integer) As String
            Dim sFn As String = "Function Get_Code_Note"

            Try
                Dim sCd As String = ""

                With r_spd
                    .Col = .GetColFromID(rsColID)
                    .Row = riRow
                    If Not .CellNote Is Nothing Then sCd = .CellNote
                End With

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function

        Public Shared Function Get_Code_Note(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol As Integer, ByVal riRow As Integer) As String
            Dim sFn As String = "Function Get_Code_Note"

            Try
                Dim sCd As String = ""

                With r_spd
                    .Col = riCol
                    .Row = riRow
                    If Not .CellNote Is Nothing Then sCd = .CellNote
                End With

                Return sCd

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

                Return ""

            End Try
        End Function


        Public Shared Sub MoveSelRow(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riColB As Integer, ByVal riColE As Integer, ByVal riRowB As Integer, ByVal riRowE As Integer, ByVal rbUp As Boolean)
            Dim sFn As String = "Sub MoveSelRow"

            Try
                If riRowB < 1 Then Return
                If riRowE < 1 Then Return
                If riRowB > riRowE Then Return

                If rbUp Then
                    If riRowB = 1 Then Return
                End If

                If rbUp = False Then
                    If riRowE = r_spd.MaxRows Then Return
                End If

                Dim iCnt As Integer = riRowE - riRowB + 1

                With r_spd
                    .ReDraw = False

                    .MaxRows += iCnt
                    .InsertRows(CInt(IIf(rbUp, riRowB - 1, riRowE + 2)), iCnt)

                    .CopyRowRange(CInt(IIf(rbUp, riRowB + iCnt, riRowB)), CInt(IIf(rbUp, riRowE + iCnt, riRowE)), CInt(IIf(rbUp, riRowB - 1, riRowE + 2)))

                    .DeleteRows(CInt(IIf(rbUp, riRowB + iCnt, riRowB)), iCnt)
                    .MaxRows -= iCnt

                    If rbUp Then
                        If .IsBlockSelected Then
                            .ClearSelection()

                            .SetSelection(riColB, riRowB - 1, riColE, riRowE - 1)
                        Else
                            .SetActiveCell(.ActiveCol, .ActiveRow - 1)
                        End If
                    Else
                        If .IsBlockSelected Then
                            .ClearSelection()

                            .SetSelection(riColB, riRowB + 1, riColE, riRowE + 1)
                        Else
                            .SetActiveCell(.ActiveCol, .ActiveRow + 1)
                        End If
                    End If

                    .ReDraw = True
                    .Refresh()
                End With

            Catch ex As Exception
                MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

            Finally
                r_spd.ReDraw = True

            End Try
        End Sub

        Public Shared Function FindCheckedRow(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal riCol_Check As Integer) As ArrayList
            Dim al_return As New ArrayList

            Try
                With r_spd
                    For i As Integer = 1 To .MaxRows
                        .Col = riCol_Check
                        .Row = i

                        If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                            If .Text = "1" Then
                                al_return.Add(i)
                            End If
                        End If
                    Next
                End With

                Return al_return

            Catch ex As Exception
                Return al_return

            End Try
        End Function

        '> yjlee 2009-01-21 

    End Class

End Namespace

