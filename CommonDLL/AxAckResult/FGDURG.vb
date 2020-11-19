Public Class FGDRUG
    Private msREGNO As String = ""
    Private msOrdDtS As String = ""
    Private msOrdDtE As String = ""
    Private msSlipCd As String = ""

    Public WriteOnly Property SLIPCD() As String
        Set(ByVal value As String)
            msSlipCd = value
        End Set
    End Property

    Public WriteOnly Property RegNo() As String
        Set(ByVal value As String)
            msREGNO = value
        End Set
    End Property

    Public WriteOnly Property OrdDtS() As String
        Set(ByVal value As String)
            msOrdDtS = value
        End Set
    End Property

    Public WriteOnly Property OrdDtE() As String
        Set(ByVal value As String)
            msOrdDtE = value
        End Set
    End Property

    Public Sub sbDisplay_Data()

        Try
            sbDisplay_IGDT()
            sbDisplay_Data(msOrdDtS, msOrdDtE)

            Me.dtpDateS.Value = CType(msOrdDtS.Insert(4, "-").Insert(7, "-").Substring(0, 10), Date)
            Me.dtpDateE.Value = CType(msOrdDtE.Insert(4, "-").Insert(7, "-").Substring(0, 10), Date)

            Me.ShowDialog()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub sbDisplay_DataView(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub Display_ResultView(DataTable)"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdDrug

            With spd
                .MaxRows = 0

                .ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For iRow As Integer = 1 To r_dt.Rows.Count

                    For ix2 As Integer = 1 To r_dt.Columns.Count
                        Dim iCol As Integer = .GetColFromID(r_dt.Columns(ix2 - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Row = iRow
                            .Col = iCol : .Text = r_dt.Rows(iRow - 1).Item(ix2 - 1).ToString()
                        End If
                    Next
                Next
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            Me.spdDrug.ReDraw = True
        End Try
    End Sub

    Private Sub sbDisplay_Data(ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, Optional ByVal rsIgdtCd As String = "")
        Try
            Dim dt As New DataTable

            dt = OCSAPP.OcsLink.SData.fnGet_PatInfo_Durg(msREGNO, rsOrdDtS, rsOrdDtE, msSlipCd, rsIgdtCd)
            sbDisplay_DataView(dt)

        Catch ex As Exception
            Throw (New Exception(ex.Message))
        End Try

    End Sub

    Private Sub sbDisplay_IGDT()
        Try
            Me.cboDCom.Items.Clear()
            Me.cboDCom.Items.Add("[ ] 전체")

            Dim dt As New DataTable

            dt = (New LISAPP.APP_F_DCOMCD).GetDcomCdInfo(msSlipCd, "0")

            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Me.cboDCom.Items.Add("[" + dt.Rows(ix).Item("dcomcd").ToString + "]  " + dt.Rows(ix).Item("dcomnm").ToString)
                Next
            End If

            Me.cboDCom.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGDRUG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Dim strIgdtCd As String = ""

        If cboDCom.SelectedIndex > 0 Then
            strIgdtCd = cboDCom.Text.Substring(1, cboDCom.Text.IndexOf("]") - 1)
        End If

        sbDisplay_Data(msOrdDtS, msOrdDtE, strIgdtCd)

    End Sub

    Private Sub dtpDateS_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateS.ValueChanged
        msOrdDtS = Format(dtpDateS.Value, "yyyy-MM-dd")
    End Sub

    Private Sub dtpDateE_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDateE.ValueChanged
        msOrdDtE = Format(dtpDateE.Value, "yyyy-MM-dd")
    End Sub
End Class