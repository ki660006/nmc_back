Imports COMMON.CommFN

Public Class FGR08_S01
    Inherits System.Windows.Forms.Form

    Public gsRegNo As String = ""
    Public gsPatNm As String = ""
    Public gsSexAge As String = ""
    Public gsIdNo As String = ""

    Public gsOrdDt As String = ""
    Public gsDeptNm As String = ""
    Public gsDoctorNm As String = ""
    Public gsWardRoom As String = ""

    Public gsTel As String = ""
    Public gsAddr1 As String = ""
    Public gsAddr2 As String = ""
    Public gsNowDate As String = ""

    Public Sub sbDisplay_PatInfo()
        Me.txtRegNo.Text = gsRegNo
        Me.txtPatNm.Text = gsPatNm
        Me.txtSexAge.Text = gsSexAge
        Me.txtIdNo.Text = gsIdNo
        Me.txtIdNo.Text = Me.txtIdNo.Text.Substring(0, 8) + "******"

        Me.txtWardRoom.Text = gsWardRoom

        Me.txtTel.Text = gsTel
        Me.txtAddr1.Text = gsAddr1
        Me.txtAddr2.Text = gsAddr2
    End Sub

    Public Sub sbDisplay_SujinInfo()
        Try

            Dim dt As DataTable = OCSAPP.OcsLink.NMC.fnGet_SujinInfo(gsRegNo) '.Text.Trim())

            Me.lblSujinCount.Text = ">> 대상환자 건수 : " + dt.Rows.Count.ToString + " 건이 조회되었습니다."

            If dt.Rows.Count = 0 Then
                Me.spdList.MaxRows = dt.Rows.Count
                Return
            End If

            With Me.spdList
                .ReDraw = False
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("ordtype") : .Text = dt.Rows(ix).Item("ordtype").ToString
                    .Col = .GetColFromID("indate") : .Text = dt.Rows(ix).Item("indate").ToString
                    .Col = .GetColFromID("outdate") : .Text = dt.Rows(ix).Item("outdate").ToString
                    
                    .Col = .GetColFromID("deptnm") : .Text = dt.Rows(ix).Item("deptnm").ToString
                    .Col = .GetColFromID("orddrnm") : .Text = dt.Rows(ix).Item("orddrnm").ToString
                    .Col = .GetColFromID("diagnm") : .Text = dt.Rows(ix).Item("diagnm").ToString
                    .Col = .GetColFromID("inyn") : .Text = dt.Rows(ix).Item("inyn").ToString
                    .Col = .GetColFromID("wardroom") : .Text = dt.Rows(ix).Item("wardroom").ToString
                    .Col = .GetColFromID("key") : .Text = dt.Rows(ix).Item("key").ToString

                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Sub sbDisplay_OrdDtInfo(ByVal rsRegno As String, ByVal rsOrdDt As String, ByVal rsCretno As String, ByVal sIoGbn As String)
        Try

            Dim dt As DataTable = OCSAPP.OcsLink.NMC.fnGet_OrdDateInfo(rsRegno, rsOrdDt, rsCretno, sIoGbn)
            Dim sDate As String = ""

            If dt.Rows.Count = 0 Then
                Me.spdOrdDt.MaxRows = 0
                Return
            End If

            With Me.spdOrdDt
                .ReDraw = False
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1

                    .Row = ix + 1
                    .Col = .GetColFromID("orddate") : .TypeButtonText = dt.Rows(ix).Item("orddate").ToString
                    .Col = .GetColFromID("key") : .Text = dt.Rows(ix).Item("key").ToString
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Sub sbDisplay_OrdInfo(ByVal rsRegNo As String, ByVal rsOrdDt As String, ByVal rsIoGbn As String)
        Try

            Dim sBfSlipnm As String = ""
            Dim dt As DataTable = OCSAPP.OcsLink.NMC.fnGet_OrdInfo(rsRegNo, rsOrdDt, rsIoGbn) '.Text.Trim())

            If dt.Rows.Count = 0 Then
                Me.spdOrdInfo.MaxRows = 0
                Return
            End If

            With Me.spdOrdInfo
                .ReDraw = False
                .MaxRows = 0
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1

                    .Row = ix + 1
                    .Col = .GetColFromID("prcpstatcd") : .Text = dt.Rows(ix).Item("prcpstatcd").ToString
                    '.Col = .GetColFromID("prcpkindcd") : .Text = dt.Rows(ix).Item("prcpkindcd").ToString
                    .Col = .GetColFromID("prcpclscd") : .Text = dt.Rows(ix).Item("prcpclscd").ToString
                    .Col = .GetColFromID("dcyn") : .Text = dt.Rows(ix).Item("dcyn").ToString
                    .Col = .GetColFromID("hosinhosoutflag") : .Text = dt.Rows(ix).Item("hosinhosoutflag").ToString
                    .Col = .GetColFromID("prcpnm") : .Text = dt.Rows(ix).Item("prcpnm").ToString
                    .Col = .GetColFromID("prcpvol") : .Text = dt.Rows(ix).Item("prcpvol").ToString
                    .Col = .GetColFromID("drprcpetc7") : .Text = dt.Rows(ix).Item("drprcpetc7").ToString
                    .Col = .GetColFromID("prcpvolunitnm") : .Text = dt.Rows(ix).Item("prcpvolunitnm").ToString
                    .Col = .GetColFromID("prcpqty") : .Text = dt.Rows(ix).Item("prcpqty").ToString
                    .Col = .GetColFromID("drprcpetc8") : .Text = dt.Rows(ix).Item("drprcpetc8").ToString
                    .Col = .GetColFromID("prcptims") : .Text = dt.Rows(ix).Item("prcptims").ToString

                    .Col = .GetColFromID("prcpdayno") : .Text = dt.Rows(ix).Item("prcpdayno").ToString
                    .Col = .GetColFromID("spcnm") : .Text = dt.Rows(ix).Item("spcnm").ToString
                    .Col = .GetColFromID("prcpmixno") : .Text = dt.Rows(ix).Item("prcpmixno").ToString
                    .Col = .GetColFromID("prcpdelivefact") : .Text = dt.Rows(ix).Item("prcptims").ToString
                    .Col = .GetColFromID("prcpdrnm") : .Text = dt.Rows(ix).Item("prcpdrnm").ToString

                    .Col = .GetColFromID("rstval") : .Text = dt.Rows(ix).Item("rstval").ToString()
                    .Col = .GetColFromID("key") : .Text = dt.Rows(ix).Item("key").ToString()


                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Sub sbInitialize()
        Me.txtRegNo.Text = ""
        Me.txtPatNm.Text = ""
        Me.txtSexAge.Text = ""
        Me.txtIdNo.Text = ""

        Me.txtWardRoom.Text = ""

        Me.txtTel.Text = ""
        Me.txtAddr1.Text = ""
        Me.txtAddr2.Text = ""
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        If e.row < 1 Then Exit Sub

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Dim sKeyInfo() As String

        With Me.spdList
            .Row = e.row
            .Col = .GetColFromID("key") : sKeyInfo = .Text.Split("/"c)
        End With

        If sKeyInfo.Length > 1 Then
            sbDisplay_OrdDtInfo(sKeyInfo(0), sKeyInfo(1), sKeyInfo(2), sKeyInfo(3))

            sbDisplay_OrdInfo(sKeyInfo(0), sKeyInfo(1), sKeyInfo(3))
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub spdOrdDt_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdOrdDt.ButtonClicked

        Try
            If e.row < 1 Then Return

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            With Me.spdOrdDt
                .Row = e.row
                .Col = .GetColFromID("key") : Dim sKeyInfo() As String = .Text.Split("/"c)

                sbDisplay_OrdInfo(sKeyInfo(1), sKeyInfo(2), sKeyInfo(0))

            End With
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub tbcAllInfo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcAllInfo.SelectedIndexChanged

        Try
            Select Case tbcAllInfo.SelectedIndex
                Case 0
                    sbDisplay_SujinInfo()
                Case 1
                    sbDisplay_PastOpInfo()
                Case 2
                    sbDisplay_PastTnsInfo()
            End Select

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try

    End Sub

    Public Sub sbDisplay_PastTnsInfo()
        Try

            Dim sBfSlipnm As String = ""
            Dim dt5 As DataTable = LISAPP.APP_BT.CGDA_BT.fn_GetPastTnsList(gsRegNo, gsNowDate)

            Me.lblTnscount.Text = ">> 수혈 건수 : " + dt5.Rows.Count.ToString + " 건이 조회되었습니다."

            If dt5.Rows.Count = 0 Then
                spdTnsInfo.MaxRows = 0
                Return
            End If

            With spdTnsInfo
                .ReDraw = False
                .MaxRows = dt5.Rows.Count
                For ix As Integer = 0 To dt5.Rows.Count - 1

                    .Row = ix + 1
                    .Col = .GetColFromID("tnsjubsuno") : .Text = dt5.Rows(ix).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("tnsgbn") : .Text = dt5.Rows(ix).Item("tnsgbn").ToString
                    .Col = .GetColFromID("comnm") : .Text = dt5.Rows(ix).Item("comnm").ToString
                    .Col = .GetColFromID("reqqnt") : .Text = dt5.Rows(ix).Item("reqqnt").ToString
                    .Col = .GetColFromID("outqnt") : .Text = dt5.Rows(ix).Item("outqnt").ToString
                    .Col = .GetColFromID("rtnqnt") : .Text = dt5.Rows(ix).Item("rtnqnt").ToString
                    .Col = .GetColFromID("abnqnt") : .Text = dt5.Rows(ix).Item("abnqnt").ToString
                    .Col = .GetColFromID("cancelqnt") : .Text = dt5.Rows(ix).Item("cancelqnt").ToString

                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            'Fn.log(msFile & sFn, Err)
            'MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub sbDisplay_PastOpInfo()
        Try

            Dim sBfSlipnm As String = ""
            Dim dt6 As DataTable = OCSAPP.OcsLink.NMC.fnGet_PastOpInfo(gsRegNo)

            Me.lblOpcount.Text = ">> 수술 건수 : " + dt6.Rows.Count.ToString + " 건이 조회되었습니다."

            If dt6.Rows.Count = 0 Then
                spdOpInfo.MaxRows = 0
                Return
            End If

            With spdOpInfo
                .ReDraw = False
                .MaxRows = dt6.Rows.Count
                For ix As Integer = 0 To dt6.Rows.Count - 1

                    .Row = ix + 1
                    .Col = .GetColFromID("opdate") : .Text = dt6.Rows(ix).Item("opdate").ToString
                    .Text = .Text.Substring(0, 4) + "-" + .Text.Substring(4, 2) + "-" + .Text.Substring(6, 2)

                    .Col = .GetColFromID("opname") : .Text = dt6.Rows(ix).Item("opname").ToString
                    .Col = .GetColFromID("deptnm") : .Text = dt6.Rows(ix).Item("deptnm").ToString
                    .Col = .GetColFromID("opdr") : .Text = dt6.Rows(ix).Item("opdr").ToString
                    .Col = .GetColFromID("anethdr") : .Text = dt6.Rows(ix).Item("anethdr").ToString
                    .Col = .GetColFromID("anethgbn") : .Text = dt6.Rows(ix).Item("anethgbn").ToString
                    .Col = .GetColFromID("remark") : .Text = dt6.Rows(ix).Item("remark").ToString

                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            'Fn.log(msFile & sFn, Err)
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub spdList_OrdInfo_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdOrdInfo.ClickEvent
        If e.row < 1 Then Exit Sub

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        With Me.spdOrdInfo
            If e.col = .GetColFromID("rstval") Then
                .Row = e.row
                .Col = .GetColFromID("key") : Dim sKeyInfo() As String = .Text.Split("/"c)
                .Col = .GetColFromID("prcpnm") : Dim sOrdNm As String = .Text
                .Col = .GetColFromID("rstval")

                If .Text = "SET검사결과" Then
                    Dim setrstinfo As New FGR08_S02

                    With setrstinfo
                        .sbDisplay_SetRstInfo("1", gsRegNo, sKeyInfo(1), sKeyInfo(2), sOrdNm)
                        .ShowDialog()
                    End With
                ElseIf .Text = "판독결과" Then
                    Dim setrstinfo As New FGR08_S02

                    With setrstinfo
                        .sbDisplay_SetRstInfo("2", gsRegNo, sKeyInfo(1), sKeyInfo(2), sOrdNm)
                        .ShowDialog()
                    End With
                End If
            End If
        End With

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

End Class