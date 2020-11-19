Public Class AxCalcResult
    Private msBcNo As String = ""
    Private msSexAge As String = ""

    Public Event OnSelectedCalcRstInfos(ByVal r_al As ArrayList)

    Public WriteOnly Property BcNo() As String
        Set(ByVal value As String)
            msBcNo = value

            sbDisp_CalcState(msBcNo, msSexAge)
        End Set
    End Property

    Public WriteOnly Property SEXAGE() As String
        Set(ByVal value As String)
            msSexAge = value
        End Set
    End Property

    Private Sub sbDisp_CalcRstInfo(ByVal rsBcNo As String, ByVal rsSexAge As String)
        Dim sFn As String = "Private Sub sbDisp_CalcRstInfo(String)"

        Try
            Dim fcalcrst As New FCALCRST

            Dim iLeft As Integer = COMMON.CommFN.Ctrl.FindControlLeft(Me) - (fcalcrst.Width - Me.Width) + COMMON.CommFN.Ctrl.frm_borderWidth(Me.ParentForm) * 2
            Dim iTop As Integer = COMMON.CommFN.Ctrl.FindControlTop(Me) + Me.Height + COMMON.CommFN.Ctrl.frm_titlebarHeight(Me.ParentForm) * 2 + COMMON.CommFN.Ctrl.frm_borderWidth(Me.ParentForm) * 2

            With fcalcrst
                .FrmLocation = New Drawing.Point(iLeft, iTop)
                .msSexAge = rsSexAge
                .txtBcNo.Text = rsBcNo
                .ShowDialog(Me.ParentForm)

                If .CalcRstInfos.Count > 0 Then
                    RaiseEvent OnSelectedCalcRstInfos(.CalcRstInfos)
                End If
            End With

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub sbDisp_CalcState(ByVal rsBcNo As String, ByVal rsSexAge As String)
        Dim sFn As String = "Private Sub sbDisp_CalcState(String)"

        Try
            Dim dt As DataTable = DB_CALC.fnGet_CalcState_BcNo(rsBcNo)

            Dim bExist As Boolean = False

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    bExist = True
                End If
            End If

            If bExist = False Then
                Me.Visible = False

                Return
            End If

            Me.Visible = True

            Dim bFinal As Boolean = False

            If dt.Rows(0).Item("minrstflg").ToString > "2" Then
                bFinal = True
            End If

            If Not bFinal Then
                If dt.Rows(0).Item("calview").ToString <> "A" Then
                    bFinal = True
                End If
            End If

            If bFinal Then Return


            '> 계산식 결과보기 팝업
            sbDisp_CalcRstInfo(rsBcNo, rsSexAge)

        Catch ex As Exception
            Throw New Exception(ex.Message + " @" + sFn)

        End Try
    End Sub

    Private Sub btnCalcRst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcRst.Click
        sbDisp_CalcRstInfo(msBcNo, msSexAge)
    End Sub
End Class

Public Class CalcRstInfo
    Public TestCd As String = ""
    Public OrgRst As String = ""
End Class