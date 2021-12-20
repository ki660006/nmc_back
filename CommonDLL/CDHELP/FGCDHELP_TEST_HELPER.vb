
Imports System.Drawing
Imports System.Windows.Forms
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN

Public Class FGCDHELP_TEST_HELPER
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGCDHELP02.vb, Class : FGCDHELP02" & vbTab
    Private malData As New ArrayList
    Private malField As New ArrayList
    Private malAlias As New ArrayList
    Private msKeyCodes As String = ""

    Private miMaxRows As Integer
    Private miColsFrozen As Integer = 0

    Private msTableNm As String = ""
    Private msWhere As String = ""
    Private msGroupBy As String = ""
    Private msOrderBy As String = ""
    Private msDistinct As Boolean = False

    Private mbOneRowReturnYn As Boolean = True

    Private msFormText As String = ""
    Public miLeftPos As Integer = 0
    Private miTopPos As Integer = 0

    Private mbOK As Boolean = False
    Private m_dt_data As DataTable

    Private msSEP_Display As String = ", "

    Public miWidth As Integer = 920
    Public mbBloodBankYN As Boolean = False
    Private Const msXMLDir As String = "\XML"
    Private msPartSlip As String = Application.StartupPath + msXMLDir & "\CDHELP_SLIP.XML"


    Public WriteOnly Property BloodBankYN() As Boolean
        Set(ByVal value As Boolean)
            mbBloodBankYN = value
        End Set
    End Property
    Public WriteOnly Property OnRowReturnYN() As Boolean
        Set(ByVal Value As Boolean)
            mbOneRowReturnYn = Value
        End Set
    End Property

    Public WriteOnly Property TableNm() As String
        Set(ByVal Value As String)
            msTableNm = Value
        End Set
    End Property

    Public WriteOnly Property Where() As String
        Set(ByVal value As String)
            msWhere = value
        End Set
    End Property

    Public WriteOnly Property GroupBy() As String
        Set(ByVal value As String)
            msGroupBy = value
        End Set
    End Property

    Public WriteOnly Property Distinct() As Boolean
        Set(ByVal value As Boolean)
            msDistinct = value
        End Set
    End Property

    Public WriteOnly Property OrderBy() As String
        Set(ByVal value As String)
            msOrderBy = value
        End Set
    End Property

    Public WriteOnly Property FormText() As String
        Set(ByVal value As String)
            msFormText = value
        End Set
    End Property

    Public WriteOnly Property MaxRows() As Integer
        Set(ByVal value As Integer)
            miMaxRows = value
        End Set
    End Property

    Public WriteOnly Property ColsFrozen() As Integer
        Set(ByVal Value As Integer)
            miColsFrozen = Value
        End Set
    End Property

    Public WriteOnly Property KeyCodes() As String
        Set(ByVal Value As String)
            msKeyCodes = Value
        End Set
    End Property

    Private Sub sbDisplayColumnNm(ByVal riCol As Integer)
        Dim sColNm As String = ""

        With Me.spdCdList
            .Col = riCol : .Row = 0 : sColNm = .Text
        End With

        Me.lblFieldNm.Text = sColNm
        Me.lblFieldNm.Tag = riCol
    End Sub

    Private Sub sbChangeBackColor(ByVal riCol As Integer, ByVal riCol2 As Integer, ByVal riRow As Integer, ByVal riRow2 As Integer)
        Dim sFn As String = "Sub sbChangeBackColor"

        Try
            With Me.spdCdList
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
            Me.spdCdList.ReDraw = True

        End Try
    End Sub

    Private Sub sbFindList(ByVal rsBuf As String)
        Dim sFn As String = "Sub sbFindList"

        Try
            If Me.lblFieldNm.Tag Is Nothing Then Return
            If IsNumeric(Me.lblFieldNm.Tag) = False Then Return

            Dim iCol As Integer = Convert.ToInt16(Me.lblFieldNm.Tag)

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdCdList

            With spd
                'If rsBuf = "" Then Return

                Dim iFindRow As Integer = .SearchCol(iCol, 0, .MaxRows, rsBuf, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)

                Do
                    Dim sCd As String = Ctrl_Helper.Get_Code(spd, iCol, iFindRow)

                    'If sCd.StartsWith(rsBuf) Then '20210610 jhs 검색한 문자가 중간에 있는것도 포함하여 찾기 
                    If sCd.IndexOf(rsBuf) Then
                        Exit Do
                    Else
                        iFindRow = .SearchCol(iCol, iFindRow, .MaxRows, rsBuf, FPSpreadADO.SearchFlagsConstants.SearchFlagsPartialMatch)
                    End If
                Loop While iFindRow > 0

                If iFindRow < 0 Then iFindRow = 0

                If iFindRow < 1 Then Return

                If iCol = 1 Then
                    spd.Col = iCol
                Else
                    spd.Col = iCol - 1
                End If

                sbChangeBackColor(1, .MaxCols, iFindRow, iFindRow)

                'spd.Row = iFindRow
                'spd.Action = FPSpreadADO.ActionConstants.ActionGotoCell

                spd.Col = iCol
                spd.Action = FPSpreadADO.ActionConstants.ActionActiveCell

            End With

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message + vbCrLf + msFile)

        Finally

        End Try
    End Sub

    Public Sub AddField(ByVal rsField As String, ByVal rsTitle As String, _
                        Optional ByVal riWidth As Integer = 15, Optional ByVal riAlign As FPSpreadADO.TypeHAlignConstants = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, _
                        Optional ByVal rsFormat As String = "", Optional ByVal bHidden As Boolean = False, Optional ByVal rsAlias As String = "", Optional ByVal rsKeyFieldYN As String = "")
        Dim clsField As New Field_Info_Helper

        With clsField
            .strField = rsField
            .strTitle = rsTitle
            .intWidth = riWidth
            .intAlign = riAlign
            .strFormat = rsFormat
            .blnHidden = bHidden

            If rsAlias = "" Then
                rsAlias = rsField
                Dim intPos As Integer = rsAlias.IndexOf(".")

                Do While intPos >= 0
                    rsAlias = rsAlias.Substring(rsField.IndexOf(".") + 1)
                    intPos = rsAlias.IndexOf(".")
                Loop
            End If

            .strAlias = rsAlias
            .strKeyFieldYN = rsKeyFieldYN

        End With

        malField.Add(clsField)

    End Sub

    Private Sub sbDisplayInit()
        Dim intCol As Integer = 0
        Dim intWith As Integer = 0

        Me.Width = 1024 : Me.Height = 768
        With spdCdList
            .ReDraw = False

            .MaxRows = CInt(IIf(miMaxRows = 0, 10, miMaxRows).ToString)

            .Col = 1 : .Col2 = .MaxCols
            .Row = 2 : .Row2 = .MaxRows
            .BlockMode = True
            .Action = FPSpreadADO.ActionConstants.ActionClearText
            .BlockMode = False

            .MaxCols = malField.Count

            For intCol = 0 To malField.Count - 1
                .Row = 0
                .Col = intCol + 1
                If CType(malField(intCol), Field_Info_Helper).strFormat = "CHECKBOX" Then
                    .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

                    .Row = -1
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox
                    .TypeCheckText = ""
                    .Text = ""
                    btnOK.Text = "선택" : chkSel.Visible = True
                Else
                    .Text = CType(malField(intCol), Field_Info_Helper).strTitle
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                End If

                .ColHidden = CType(malField(intCol), Field_Info_Helper).blnHidden
                .set_ColWidth(intCol + 1, CType(malField(intCol), Field_Info_Helper).intWidth)

                .ColID = CType(malField(intCol), Field_Info_Helper).strField.ToLower
                intWith += CType(malField(intCol), Field_Info_Helper).intWidth
                .Row = -1
                .TypeHAlign = CType(malField(intCol), Field_Info_Helper).intAlign
            Next

            .VisibleRows = .MaxRows : .VisibleCols = .MaxCols
            '.AutoSize = True

            .AutoSize = False

            If 33 + CInt(intWith * 8.2) + 10 > 700 Then
                Me.Width = 300 + CInt(intWith * 8.2) '- 100
            Else
                Me.Width = 300 + CInt(intWith * 8.2) + 10
            End If

            .Height = miMaxRows * (.get_RowHeight(1) + 8.5) + 20

            Me.Height = .Height + 75

            .MaxRows = 0


            If miColsFrozen > 0 Then
                .ColsFrozen = miColsFrozen
            Else
                If CType(malField(0), Field_Info_Helper).strFormat = "CHECKBOX" Then
                    .ColsFrozen = 2
                Else
                    .ColsFrozen = 1
                End If
            End If

        End With


        Me.Refresh()

    End Sub

    Private Sub sbDisplay_Data(Optional ByVal rsQry As String = "")

        Dim sSql As String = ""

        Dim ix As Integer = 0
        Dim iCol As Integer = 0

        sSql = "select "
        If msDistinct Then sSql += "distinct "

        For ix = 0 To malField.Count - 1
            sSql += IIf(ix = 0, "", ", ").ToString + CType(malField(ix), Field_Info_Helper).strField
        Next

        sSql += "  from " + msTableNm
        If msWhere <> "" Then
            sSql += " where " + msWhere
        End If

        If rsQry <> "" Then
            If sSql.IndexOf("where") > 0 Then
                sSql += "   and " + rsQry
            Else
                sSql += " where " + rsQry
            End If
        End If

        If msGroupBy <> "" Then
            sSql += " group by " + msGroupBy
        End If

        If msOrderBy <> "" Then
            sSql += " order by " + msOrderBy
        End If

        Dim dt As DataTable = (New DA_CD_HELPER).Get_HelpData(sSql)
        m_dt_data = dt

        If dt.Rows.Count > 0 Then
            For ix = 0 To dt.Rows.Count - 1
                With spdCdList
                    .MaxRows += 1

                    .Row = .MaxRows

                    For iCol = 0 To malField.Count - 1
                        .Col = iCol + 1 : .Text = dt.Rows(ix).Item(iCol).ToString.Trim
                        If CType(malField(iCol), Field_Info_Helper).strKeyFieldYN <> "" Then
                            If msKeyCodes.IndexOf(dt.Rows(ix).Item(iCol).ToString.Trim + "|") >= 0 Then
                                .Col = 1
                                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                                    .Col = 1 : .Text = "1"
                                End If
                            End If
                        End If
                    Next
                End With
            Next
        End If

        With spdCdList
            .Row = 1 : .Col = 1
            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                sbDisplayColumnNm(2)
            Else
                sbDisplayColumnNm(1)
            End If

            If .MaxRows < miMaxRows Then .MaxRows = miMaxRows
        End With


        spdCdList.ReDraw = True
        txtCd.Focus()

    End Sub

    Private Sub sbDisplay_Data(ByVal r_dt As DataTable)

        Dim intIdx As Integer = 0
        Dim intCol As Integer = 0

        m_dt_data = r_dt

        If r_dt.Rows.Count > 0 Then
            Me.spdCdList.MaxRows = r_dt.Rows.Count
            For intIdx = 0 To r_dt.Rows.Count - 1
                With spdCdList
                    .Row = intIdx + 1
                    For ix As Integer = 0 To r_dt.Columns.Count - 1
                        Dim iCol As Integer = .GetColFromID(r_dt.Columns(ix).ColumnName.ToLower)
                        If iCol > 0 Then
                            .Col = iCol : .Text = r_dt.Rows(intIdx).Item(ix).ToString.Trim
                            If CType(malField(iCol - 1), Field_Info_Helper).strKeyFieldYN <> "" Then
                                If msKeyCodes.IndexOf(r_dt.Rows(intIdx).Item(ix).ToString.Trim + "|") Then
                                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                                        .Col = 1 : .Text = "1"
                                    End If
                                End If
                            End If
                        End If
                    Next
                End With
            Next

        End If

        With spdCdList
            .Row = 1 : .Col = 1
            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                sbDisplayColumnNm(2)
            Else
                sbDisplayColumnNm(1)
            End If

            If .MaxRows < miMaxRows Then .MaxRows = miMaxRows
        End With


        spdCdList.ReDraw = True
        txtCd.Focus()

    End Sub

    Private Sub sbDisplay_Data_Convert(Optional ByVal rsQry As String = "")

        Dim sSql As String = ""

        Dim intIdx As Integer = 0
        Dim intCol As Integer = 0

        sSql = "select "
        If msDistinct Then sSql += "distinct "

        For intIdx = 0 To malField.Count - 1
            sSql += IIf(intIdx = 0, "", ", ").ToString + CType(malField(intIdx), Field_Info_Helper).strField
        Next

        sSql += "  from " + msTableNm
        If msWhere <> "" Then
            sSql += " where " + msWhere
        End If

        If rsQry <> "" Then
            If sSql.IndexOf("where") > 0 Then
                sSql += "   and " + rsQry
            Else
                sSql += " where " + rsQry
            End If
        End If

        If msGroupBy <> "" Then
            sSql += " group by " + msGroupBy
        End If

        If msOrderBy <> "" Then
            sSql += " order by " + msOrderBy
        End If

        Dim dt As DataTable = (New DA_CD_HELPER).Get_HelpData(sSql)
        m_dt_data = dt

        Dim dt_tgrp As DataTable = m_dt_data.Clone()

        Dim sTGrpCd_p As String = ""
        Dim sTGrpCd_c As String = ""

        For i As Integer = 1 To m_dt_data.Rows.Count
            Dim dr As DataRow

            sTGrpCd_c = m_dt_data.Rows(i - 1).Item("tgrpcd").ToString()

            If i = 1 Then
                'Row 생성
                dr = dt_tgrp.NewRow()
            Else
                If sTGrpCd_c.Equals(sTGrpCd_p) = False Then
                    'Row 추가
                    dt_tgrp.Rows.Add(dr)

                    'Row 재생성
                    dr = dt_tgrp.NewRow()
                End If
            End If

            dr.Item("tgrpcd") = m_dt_data.Rows(i - 1).Item("tgrpcd")
            dr.Item("tgrpnmd") = m_dt_data.Rows(i - 1).Item("tgrpnmd")

            If dr.Item("testcd").ToString().Length > 0 Then dr.Item("testcd") = dr.Item("testcd").ToString() + msSEP_Display
            dr.Item("testcd") = dr.Item("testcd").ToString() + m_dt_data.Rows(i - 1).Item("testcd").ToString()

            '맨 마지막에도 추가
            If i = m_dt_data.Rows.Count Then
                dt_tgrp.Rows.Add(dr)
            End If

            sTGrpCd_p = sTGrpCd_c
        Next



        If dt_tgrp.Rows.Count > 0 Then
            For intIdx = 0 To dt_tgrp.Rows.Count - 1
                With spdCdList
                    .MaxRows += 1

                    .Row = .MaxRows

                    For intCol = 0 To malField.Count - 1
                        .Col = intCol + 1 : .Text = dt_tgrp.Rows(intIdx).Item(intCol).ToString.Trim
                        If CType(malField(intCol), Field_Info_Helper).strKeyFieldYN <> "" Then
                            If msKeyCodes.IndexOf(dt_tgrp.Rows(intIdx).Item(intCol).ToString.Trim + "|") >= 0 Then
                                .Col = 1
                                If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                                    .Col = 1 : .Text = "1"
                                End If
                            End If
                        End If
                    Next
                End With
            Next
        End If

        With spdCdList
            .Row = 1 : .Col = 1
            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then
                sbDisplayColumnNm(2)
            Else
                sbDisplayColumnNm(1)
            End If

            If .MaxRows < miMaxRows Then .MaxRows = miMaxRows
        End With


        spdCdList.ReDraw = True
        txtCd.Focus()

    End Sub

    Public Function Display_Result(ByVal rofrm As Windows.Forms.Form, ByVal riLeftPos As Integer, ByVal riTopPos As Integer) As ArrayList
        Dim sFn As String = "Function Display_Result"

        Try

            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbDisplayInit()
            sbDisplay_Data()

            Me.Cursor = Windows.Forms.Cursors.Default

            If rofrm.Left + rofrm.Width < Me.Width + riLeftPos Then
                miLeftPos = rofrm.Left + rofrm.Width - Me.Width
                If miLeftPos < 0 Or miLeftPos > rofrm.Left + rofrm.Width Then miLeftPos = 10
            Else
                miLeftPos = riLeftPos
            End If

            If rofrm.Top + rofrm.Height < Me.Height + riTopPos Then
                miTopPos = rofrm.Top + rofrm.Height - Me.Height
                If miTopPos < 0 Or miTopPos > rofrm.Top + rofrm.Height Then miTopPos = 10
            Else
                miTopPos = riTopPos
            End If

            Me.Width = spdCdList.Width + 20

            If mbOneRowReturnYn And m_dt_data.Rows.Count = 0 Then
                btnEsc_Click(Nothing, Nothing)
            ElseIf mbOneRowReturnYn And m_dt_data.Rows.Count = 1 Then
                spdCdList_DblClick(spdCdList, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
            Else
                Me.ShowDialog(rofrm)
            End If

            Return malData

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New ArrayList
        Finally

            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Function

    Public Function Display_Result(ByVal rofrm As Windows.Forms.Form, ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                   ByVal r_dt As DataTable) As ArrayList
        Dim sFn As String = "Function Display_Result"

        Try

            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbDisplayInit()
            sbDisplay_Data(r_dt)

            Me.Cursor = Windows.Forms.Cursors.Default

            If MdiMain.Frm.Location.X + rofrm.Left + rofrm.Width < Me.Width + riLeftPos Then
                miLeftPos = MdiMain.Frm.Location.X + rofrm.Left + rofrm.Width - Me.Width
                If miLeftPos < 0 Or miLeftPos > rofrm.Left + rofrm.Width Then miLeftPos = MdiMain.Frm.Location.X + 10
            Else
                miLeftPos = riLeftPos
            End If

            If rofrm.Top + rofrm.Height < Me.Height + riTopPos Then
                miTopPos = rofrm.Top + rofrm.Height - Me.Height
                If miTopPos < 0 Or miTopPos > rofrm.Top + rofrm.Height Then miTopPos = 10
            Else
                miTopPos = riTopPos
            End If

            Me.Width = spdCdList.Width + 20

            If mbOneRowReturnYn And m_dt_data.Rows.Count = 0 Then
                btnEsc_Click(Nothing, Nothing)
            ElseIf mbOneRowReturnYn And m_dt_data.Rows.Count = 1 Then
                spdCdList_DblClick(spdCdList, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
            Else
                Me.ShowDialog(rofrm)
            End If

            Return malData

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New ArrayList
        Finally

            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Function

    Public Function Display_Result(ByVal rofrm As Windows.Forms.Form, ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                               ByVal r_dt As DataTable, ByVal bFixPos As Boolean) As ArrayList
        Dim sFn As String = "Function Display_Result"

        Try

            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbDisplayInit()
            sbDisplay_Data(r_dt)

            Me.Cursor = Windows.Forms.Cursors.Default

            If bFixPos Then
                miLeftPos = riLeftPos
                miTopPos = riTopPos
            Else
                If MdiMain.Frm.Location.X + rofrm.Left + rofrm.Width < Me.Width + riLeftPos Then
                    miLeftPos = MdiMain.Frm.Location.X + rofrm.Left + rofrm.Width - Me.Width
                    If miLeftPos < 0 Or miLeftPos > rofrm.Left + rofrm.Width Then miLeftPos = MdiMain.Frm.Location.X + 10
                Else
                    miLeftPos = riLeftPos
                End If

                If rofrm.Top + rofrm.Height < Me.Height + riTopPos Then
                    miTopPos = rofrm.Top + rofrm.Height - Me.Height
                    If miTopPos < 0 Or miTopPos > rofrm.Top + rofrm.Height Then miTopPos = 10
                Else
                    miTopPos = riTopPos
                End If
            End If

            Me.Width = spdCdList.Width + 20

            If mbOneRowReturnYn And m_dt_data.Rows.Count = 0 Then
                btnEsc_Click(Nothing, Nothing)
            ElseIf mbOneRowReturnYn And m_dt_data.Rows.Count = 1 Then
                spdCdList_DblClick(spdCdList, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
            Else
                Me.ShowDialog(rofrm)
            End If

            Return malData

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New ArrayList
        Finally

            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Function

    Public Function Display_Result_Convert(ByVal rofrm As Windows.Forms.Form, ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                           ByVal rdbCn As OracleConnection) As ArrayList
        Dim sFn As String = "Function Display_Result_Convert"

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbDisplayInit()
            sbDisplay_Data_Convert()

            Me.Cursor = Windows.Forms.Cursors.Default

            If rofrm.Left + rofrm.Width < Me.Width + riLeftPos Then
                miLeftPos = rofrm.Left + rofrm.Width - Me.Width
                If miLeftPos < 0 Or miLeftPos > rofrm.Left + rofrm.Width Then miLeftPos = 10
            Else
                miLeftPos = riLeftPos
            End If

            If rofrm.Top + rofrm.Height < Me.Height + riTopPos Then
                miTopPos = rofrm.Top + rofrm.Height - Me.Height
                If miTopPos < 0 Or miTopPos > rofrm.Top + rofrm.Height Then miTopPos = 10
            Else
                miTopPos = riTopPos
            End If

            Me.Width = spdCdList.Width + 20

            If mbOneRowReturnYn And m_dt_data.Rows.Count = 0 Then
                btnEsc_Click(Nothing, Nothing)
            ElseIf mbOneRowReturnYn And m_dt_data.Rows.Count = 1 Then
                spdCdList_DblClick(spdCdList, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
            Else
                Me.ShowDialog(rofrm)
            End If

            Return malData

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return New ArrayList
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Function


    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim intRow As Integer = 0
        Dim intCol As Integer = 0

        Dim clsField As New Field_Info_Helper
        Dim sBuf As String

        If btnOK.Text = "조회" Then
            Dim strFiler As String = ""
            Dim dt As DataTable = New DataTable

            If txtCd.Text <> "" Then
                strFiler = CType(malField(CInt(lblFieldNm.Tag.ToString) - 1), Field_Info_Helper).strAlias + " like '" + txtCd.Text + "%'"
            End If

            Dim a_dr As DataRow()
            a_dr = m_dt_data.Select(strFiler)

            If a_dr.Length > 0 Then
                For ix As Integer = 0 To a_dr.Length - 1
                    With spdCdList
                        .MaxRows = a_dr.Length

                        .Row = ix + 1

                        For iCol = 0 To malField.Count - 1
                            .Col = iCol + 1 : .Text = a_dr(ix).Item(intCol).ToString()
                        Next
                    End With
                Next
            End If
            If spdCdList.MaxRows < miMaxRows Then spdCdList.MaxRows = miMaxRows
        Else
            malData = New ArrayList
            For iRow = 1 To spdCdList.MaxRows

                sBuf = ""
                With spdCdList

                    .Row = iRow
                    .Col = 1 : Dim sChk As String = .Text
                    .Col = 2 : Dim sTmp As String = .Text

                    If sChk = "1" And sTmp <> "" Then
                        For iCol = 2 To .MaxCols
                            .Row = iRow
                            .Col = iCol : sBuf += .Text + "|"
                        Next
                    End If
                End With

                If sBuf <> "" Then
                    malData.Add(sBuf)
                End If
            Next
            mbOK = True
            Me.Close()
        End If

    End Sub

    Private Sub btnEsc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsc.Click
        malData = New ArrayList
        Me.Close()
    End Sub

    Private Sub spdCdList_AfterUserSort(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_AfterUserSortEvent) Handles spdCdList.AfterUserSort

        If e.col < 1 Then Exit Sub

        sbDisplayColumnNm(e.col)
    End Sub

    Private Sub spdCdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdCdList.ClickEvent
        If e.row = 0 Then
            sbDisplayColumnNm(e.col)
        Else
            sbChangeBackColor(1, spdCdList.MaxCols, e.row, e.row)
        End If
    End Sub

    Private Sub spdCdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdCdList.DblClick
        Dim intCol As Integer = 0
        Dim strBuf$ = ""

        malData = New ArrayList

        For intCol = 1 To spdCdList.MaxCols
            With spdCdList
                .Row = e.row
                .Col = intCol
                If .CellType <> FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then strBuf += .Text + "|"
            End With
        Next

        malData.Add(strBuf)
        mbOK = True
        Me.Close()

    End Sub

    Private Sub FGCDHELP_TEST_HELPER_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                btnEsc_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGCDHELP_TEST_HELPER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Left = miLeftPos
        Me.Top = miTopPos

        Me.Text = msFormText

        Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msPartSlip, "PARTSLIP")

        Try
            If sTmp.IndexOf("^"c) < 0 Then
                Me.cboQryGbn.SelectedIndex = 1
                sbDisplay_slip() ' 검사분야 표시 
                If sTmp = "" Then
                    Me.cboPartSlip.SelectedIndex = 0
                Else
                    If CInt(sTmp) < Me.cboPartSlip.Items.Count Then
                        Me.cboPartSlip.SelectedIndex = CInt(sTmp)
                    Else
                        If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0
                    End If
                End If
            Else
                Me.cboQryGbn.SelectedIndex = CInt(sTmp.Split("^"c)(0))
                If Me.cboQryGbn.SelectedIndex = 0 Then
                    sbDisplay_part() ' 검사부서 표시 
                Else
                    sbDisplay_slip() ' 검사분야 표시 
                End If

                If CInt(sTmp.Split("^"c)(1)) < Me.cboPartSlip.Items.Count Then
                    Me.cboPartSlip.SelectedIndex = CInt(sTmp.Split("^"c)(1))
                Else
                    If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub sbDisplay_part()

        Try
            'Dim dt As DataTable = DA_CD_HELPER.fnGet_Part_List(, IIf(mbBloodBankYN, "3", "0").ToString)
            Dim dt As DataTable = DA_CD_HELPER.fnGet_Part_List()

            Me.cboPartSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_slip()

        Dim sFn As String = "Sub sbDisplay_slip()"

        Try
            Dim time As String = DateTime.Now.ToString("yyyy-MM-dd-HH24-mm-ss")

            'Dim dt As DataTable = DA_CD_HELPER.fnGet_Slip_List(time.Replace("-", ""), False, mbBloodBankYN)
            Dim dt As DataTable = DA_CD_HELPER.fnGet_Slip_List(time.Replace("-", ""))

            Me.cboPartSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub txtCd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFn As String = ""

        Try
            If Me.lblFieldNm.Text.Trim().EndsWith("코드") Then
                Me.txtCd.CharacterCasing = Windows.Forms.CharacterCasing.Upper
            Else
                Me.txtCd.CharacterCasing = Windows.Forms.CharacterCasing.Normal
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtCd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCd.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim sFiler As String = ""
            Dim dt As New DataTable

            If Me.txtCd.Text <> "" Then
                'sFiler = CType(malField(CInt(lblFieldNm.Tag.ToString) - 1), Field_Info_Helper).strAlias + " like '%" + txtCd.Text + "%'"
                sFiler = CType(malField(CInt(lblFieldNm.Tag.ToString) - 1), Field_Info_Helper).strAlias + " like '%" + txtCd.Text + "%'" '20210610 검색한 것 목록이 중간에 있는것도 조회 될 수 있도록 수정
            End If

            Dim a_dr As DataRow()
            a_dr = m_dt_data.Select(sFiler)

            If a_dr.Length > 0 Then
                For ix As Integer = 0 To a_dr.Length - 1
                    With spdCdList
                        .MaxRows = a_dr.Length

                        .Row = ix + 1

                        For ix2 As Integer = 0 To m_dt_data.Columns.Count - 1
                            Dim iCol As Integer = .GetColFromID(m_dt_data.Columns(ix2).ColumnName.ToLower)
                            If iCol > 0 Then
                                .Col = iCol : .Text = a_dr(ix).Item(ix2).ToString()
                            End If
                        Next
                    End With
                Next
            End If

            If spdCdList.MaxRows < miMaxRows Then spdCdList.MaxRows = miMaxRows
        End If

    End Sub

    Private Sub txtCd_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCd.TextChanged
        Try
            If Me.spdCdList.MaxRows < 1 Then Return

            sbFindList(Me.txtCd.Text)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkSel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        With spdCdList
            For intRow As Integer = 0 To .MaxRows
                .Row = intRow
                .Col = 2
                If .Text <> "" Then
                    .Col = 1 : .Text = IIf(chkSel.Checked, "1", "").ToString
                End If
            Next
        End With

    End Sub

    Private Sub cboQryGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQryGbn.SelectedIndexChanged

        If Me.cboQryGbn.SelectedIndex = 0 Then
            sbDisplay_part()
        Else
            sbDisplay_slip()
        End If

        If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0

    End Sub

    Private Sub cboPartSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged

        COMMON.CommXML.setOneElementXML(msXMLDir, msPartSlip, "PARTSLIP", Me.cboQryGbn.SelectedIndex.ToString + "^" + Me.cboPartSlip.SelectedIndex.ToString)

        Dim dt As DataTable

        dt = DA_CD_HELPER.fnGet_Help_Info(Ctrl.Get_Code(Me.cboPartSlip))

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        sbDisplayInit()
        sbDisplay_Data(dt)

        Me.Cursor = Windows.Forms.Cursors.Default


    End Sub

 
 
End Class

Public Class DA_CD_HELPER

    Public Function Get_HelpData(ByVal rsSql As String) As DataTable
        Dim dbCn As OracleConnection = GetDbConnection()

        Try
            Dim dbDa As OracleDataAdapter
            Dim dbCmd As New OracleCommand

            Dim dt As New DataTable

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = rsSql

            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Return New DataTable
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If
        End Try
    End Function

    Public Shared Function fnGet_Help_Info(ByVal rsPartSlip As String) As DataTable
        Dim sFn As String = " fnGet_Help_Info([String]) As DataTable"


        Dim dbCn As OracleConnection = GetDbConnection()

        Try
            Dim dbDa As OracleDataAdapter
            Dim dbCmd As New OracleCommand

            Dim dt As New DataTable
            Dim sSql As String = ""


            sSql += "SELECT f6.testcd, MAX(f6.tnmd) tnmd, f6.partcd || f6.slipcd partslip, f6.tordslip," + vbCrLf
            sSql += "       f6.tordcd, f2.slipnmd, f10.tordslipnm" + vbCrLf
            sSql += "  FROM lf060m f6, lf021m f2, lf100m f10" + vbCrLf
            sSql += " WHERE f6.partcd   = f2.partcd" + vbCrLf
            sSql += "   AND f6.slipcd   = f2.slipcd" + vbCrLf
            sSql += "   AND f6.tordslip = f10.tordslip" + vbCrLf
            sSql += "   AND f6.usdt    <= fn_ack_sysdate" + vbCrLf
            sSql += "   AND f6.uedt    >  fn_ack_sysdate" + vbCrLf
            sSql += "   AND f2.usdt    <= fn_ack_sysdate" + vbCrLf
            sSql += "   AND f2.uedt    >  fn_ack_sysdate" + vbCrLf
            sSql += "   AND f10.usdt   <= fn_ack_sysdate" + vbCrLf
            sSql += "   AND f10.uedt   >  fn_ack_sysdate" + vbCrLf
            sSql += "   AND f6.tcdgbn  <> 'C'" + vbCrLf
            sSql += "   AND f6.ordhide <> '1' " + vbCrLf

            If rsPartSlip <> "" Then
                sSql += "   AND f6.partcd  = '" + rsPartSlip.Substring(0, 1) + "'"
            End If

            If rsPartSlip.Length > 1 Then
                sSql += "   AND f6.slipcd = '" + rsPartSlip.Substring(1, 1) + "'"
            End If

            sSql += " GROUP BY f6.testcd, f6.partcd, f6.slipcd, f6.tordslip," + vbCrLf
            sSql += "       f6.tordcd, f2.slipnmd, f10.tordslipnm" + vbCrLf

            sSql += " UNION " + vbCrLf
            sSql += "SELECT f6.testcd, MAX(f6.tnmd) tnmd, f6.partcd || f6.slipcd partslip, f6.tordslip," + vbCrLf
            sSql += "       f6.tordcd, f2.slipnmd, f10.tordslipnm" + vbCrLf
            sSql += "  FROM rf060m f6, rf021m f2, lf100m f10" + vbCrLf
            sSql += " WHERE f6.partcd   = f2.partcd" + vbCrLf
            sSql += "   AND f6.slipcd   = f2.slipcd" + vbCrLf
            sSql += "   AND f6.tordslip = f10.tordslip" + vbCrLf
            sSql += "   AND f6.usdt    <= fn_ack_sysdate" + vbCrLf
            sSql += "   AND f6.uedt    >  fn_ack_sysdate" + vbCrLf
            sSql += "   AND f2.usdt    <= fn_ack_sysdate" + vbCrLf
            sSql += "   AND f2.uedt    >  fn_ack_sysdate" + vbCrLf
            sSql += "   AND f10.usdt   <= fn_ack_sysdate" + vbCrLf
            sSql += "   AND f10.uedt   >  fn_ack_sysdate" + vbCrLf
            sSql += "   AND f6.tcdgbn  <> 'C'" + vbCrLf

            If rsPartSlip <> "" Then
                sSql += "   AND f6.partcd  = '" + rsPartSlip.Substring(0, 1) + "'"
            End If

            If rsPartSlip.Length > 1 Then
                sSql += "   AND f6.slipcd = '" + rsPartSlip.Substring(1, 1) + "'"
            End If

            sSql += " GROUP BY f6.testcd, f6.partcd, f6.slipcd, f6.tordslip," + vbCrLf
            sSql += "       f6.tordcd, f2.slipnmd, f10.tordslipnm" + vbCrLf


            With dbCmd
                dbCmd.Connection = dbCn
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = sSql

            End With

            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            If dbCn.State = ConnectionState.Open Then
                dbCn.Close() : dbCn.Dispose()
            End If

            dbCn = Nothing
        End Try

    End Function
    ' 분야 목록 조회
    Public Shared Function fnGet_Part_List(Optional ByVal rbTake2Yn As Boolean = False, Optional ByVal rsViewGbn As String = "") As DataTable
        Dim sFn As String = "Function fnGet_Part_List() As DataTable"
        Try
            Dim sSql As String = ""

            sSql += "SELECT a.partcd, a.partnmd, MAX(b.dispseq) dispseq"
            sSql += "  FROM lf020m a, lf021m b"
            sSql += " WHERE a.partcd = b.partcd"
            sSql += "   AND a.usdt  <= fn_ack_sysdate"
            sSql += "   AND a.uedt  >  fn_ack_sysdate"
            sSql += "   AND b.usdt  <= fn_ack_sysdate"
            sSql += "   AND b.uedt  >  fn_ack_sysdate"

            If rsViewGbn <> "" Then
                sSql += "   AND NVL(a.partgbn, '0') = '" + rsViewGbn + "'"
            End If

            If rbTake2Yn Then
                sSql += "   AND NVL(b.take2yn, '0') = '1'"
            End If

            sSql += " GROUP BY a.partcd, a.partnmd"
            sSql += " ORDER BY dispseq, a.partcd"

            DbCommand()
            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        End Try
    End Function

    ' 검사분야 조회
    Public Shared Function fnGet_Slip_List(Optional ByVal rsUsDt As String = "", Optional ByVal rbAll As Boolean = True) As DataTable
        Dim sFn As String = "Function fnGet_Slip_List() As DataTable"
        Try
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            rsUsDt = rsUsDt.Replace("-", "").Replace(":", "").Replace(" ", "")
            If rsUsDt.Length = 8 Then rsUsDt += "000000"

            sSql += "SELECT DISTINCT" + vbCrLf
            sSql += "       partcd || slipcd slipcd, slipnmd, dispseq" + vbCrLf
            sSql += "  FROM lf021m" + vbCrLf

            If rsUsDt = "" Then
                sSql += " WHERE usdt <= fn_ack_sysdate" + vbCrLf
                sSql += "   AND uedt >  fn_ack_sysdate" + vbCrLf
            Else
                sSql += " WHERE usdt <= :usdt" + vbCrLf
                sSql += "   AND uedt >  :usdt" + vbCrLf

                alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
            End If

            sSql += "   AND partcd IN (SELECT partcd FROM lf020m WHERE partgbn IN ('0', '1', '2', '3'))" + vbCrLf
            sSql += " ORDER BY dispseq, slipcd" + vbCrLf

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        End Try
    End Function

End Class


Public Class Field_Info_Helper
    Public strField As String
    Public strAlias As String
    Public strTitle As String
    Public intWidth As Integer
    Public intAlign As FPSpreadADO.TypeHAlignConstants
    Public strFormat As String
    Public blnHidden As Boolean

    Public strKeyFieldYN As String
End Class

Public Class Ctrl_Helper
    Private Const msFile As String = "File : FGCDHELP02.vb, Class : Ctrl_Helper" & vbTab

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

End Class