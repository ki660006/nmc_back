Imports Oracle.DataAccess.Client

Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Public Class FGPOPUPST_VRST4
    Private Const msFile As String = "File : FGPOPUPST_VRST4.vb, Class : FGPOPUPST_VRST4" & vbTab

    Private m_dbCn As OracleConnection
    Private msBcNo As String = ""
    Private msTClsCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""
    Private msReturn As String = ""
    Private msResult As String = ""
    Private mbSave As Boolean = False

    Private Function fnHan_PadRight(ByVal rsBuf As String, ByVal riLen As Integer) As String
        Dim a_btBuf As Byte() = System.Text.Encoding.Default.GetBytes(rsBuf)
        Dim sReturn As String = ""

        If a_btBuf.Length > riLen Then
            sReturn = System.Text.Encoding.Default.GetString(a_btBuf)
        Else
            sReturn = System.Text.Encoding.Default.GetString(a_btBuf) + "".PadRight(riLen - a_btBuf.Length)
        End If

        Return sReturn
    End Function

    Private Sub sbDisplay_cmt()

        Me.spdList.MaxRows = 0

        Dim dt As DataTable = (New DA_ST_GV).fnGet_Result_GV(m_dbCn, msBcNo)

        If dt.Rows.Count < 1 Then
            msReturn = "   없음"
            Return
        End If

        Dim sValue As String = ""
        Dim alTOSLIP As New ArrayList
        Dim sSlip As String = ""
        Dim sRst As String = ""
        Dim iLine As Integer = 0

        With spdList
            .ReDraw = False
            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim sCmt As String = ""

                If dt.Rows(ix).Item("cdseqt").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqt").ToString
                ElseIf dt.Rows(ix).Item("cdseqc").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqc").ToString
                ElseIf dt.Rows(ix).Item("cdseqd").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqd").ToString
                ElseIf dt.Rows(ix).Item("cdseqp").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqp").ToString
                ElseIf dt.Rows(ix).Item("cdseqh").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqh").ToString
                ElseIf dt.Rows(ix).Item("cdseql").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseql").ToString
                End If

                If sCmt <> "" Then
                    .MaxRows += 1
                    .Row = .MaxRows
                    .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("cmt") : .Text = sCmt
                End If

                If alTOSLIP.Contains(dt.Rows(ix).Item("tordslipnm").ToString) Then
                Else
                    alTOSLIP.Add(dt.Rows(ix).Item("tordslipnm").ToString)
                    sSlip += dt.Rows(ix).Item("tordslipnm").ToString + vbCrLf

                    iLine += 1
                End If

                sRst += Space(3)
                If dt.Rows(ix).Item("tordslipnm").ToString.Length = 5 Then
                    sRst += ("[" + dt.Rows(ix).Item("tordslipnm").ToString + "  ]").PadRight(15, " "c)
                Else
                    sRst += ("[" + dt.Rows(ix).Item("tordslipnm").ToString + "]").PadRight(14, " "c)
                End If

                sRst += fnHan_PadRight(dt.Rows(ix).Item("tnmp").ToString, 30)
                sRst += dt.Rows(ix).Item("hlmark").ToString.PadRight(2, " "c)
                sRst += dt.Rows(ix).Item("viewrst").ToString.PadRight(14, " "c)
                sRst += dt.Rows(ix).Item("reftxt").ToString.PadRight(20, " "c)
                sRst += dt.Rows(ix).Item("rstunit").ToString.PadRight(10, " "c)
                sRst += "(" + dt.Rows(ix).Item("fndt").ToString + ")" + vbCrLf

            Next

            .ReDraw = True
        End With

        msReturn = sRst

    End Sub

    Private Function fnGet_Verify() As String

        Dim sValue As String = ""
        Dim sTotCmt As String = ""
        Dim iLine As Integer = 0

        With Me.spdList
            For iRow As Integer = 1 To .MaxRows

                .Row = iRow
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("cmt") : Dim sCmt As String = .Text

                If sChk = "1" Then
                    sTotCmt += sCmt + vbCrLf
                End If

            Next

        End With

        If sTotCmt = "" Then sTotCmt = PRG_CONST.Tail_GV_NormalComment

        sValue += "■ 비정상 결과 혹은 유의한 결과를 보이는 항목" + vbCrLf + msReturn + vbCrLf + vbCrLf

        sValue += "■ 검증방법" + vbCrLf
        sValue += "       ● Calibration                   ● Internal Quality Control" + vbCrLf
        sValue += "       ● Delta Check Verification      ● Panic/alert Value Veritication" + vbCrLf
        sValue += "       ● Repeat/Recheck                ○ Other" + vbCrLf + vbCrLf

        sValue += "■ 검증/판독 소견" + vbCrLf + sTotCmt + vbCrLf + vbCrLf

        Return sValue

    End Function

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_dbCn As OracleConnection, _
                                   ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            m_dbCn = r_dbCn
            msBcNo = rsBcNo
            msTClsCd = rsTClsCd
            msTNm = rsTNm

            sbDisplay_cmt()

            Me.Cursor = Windows.Forms.Cursors.Default

            Me.ShowDialog()

            Dim al_return As New ArrayList

            If mbSave Then
                Dim STU_StDataInfo As STU_StDataInfo

                STU_StDataInfo = New STU_StDataInfo
                STU_StDataInfo.Data = msResult
                STU_StDataInfo.Alignment = 0
                al_return.Add(STU_StDataInfo)

                STU_StDataInfo = Nothing
            End If

            Return al_return

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New ArrayList

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        msResult = fnGet_Verify()
        mbSave = True
        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        msResult = ""
        mbSave = False
        Me.Close()
    End Sub
End Class

