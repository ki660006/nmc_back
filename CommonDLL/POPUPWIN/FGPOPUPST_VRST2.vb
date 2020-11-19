Imports Oracle.DataAccess.Client

Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports DBORA.DbProvider

Public Class FGPOPUPST_VRST2
    Private Const msFile As String = "File : FGPOPUPST_VRST2.vb, Class : FGPOPUPST_VRST2" & vbTab

    Private m_dbCn As OracleConnection
    Private msBcNo As String = ""
    Private msTClsCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

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


    Private Function fnGet_Verify() As String

        Dim sValue As String = ""
        Dim alTOSLIP As New ArrayList
        Dim sSlip As String = ""
        Dim sRst As String = ""
        Dim sCmt As String = ""
        Dim iLine As Integer = 0

        Dim dt As DataTable = (New DA_ST_GV).fnGet_Result_GV(m_dbCn, msBcNo)

        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1

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

                iLine += 1

                If dt.Rows(ix).Item("cdseqt").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqt").ToString + vbCrLf
                    iLine += 1
                ElseIf dt.Rows(ix).Item("cdseqc").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqc").ToString + vbCrLf
                    iLine += 1
                ElseIf dt.Rows(ix).Item("cdseqd").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqd").ToString + vbCrLf
                    iLine += 1
                ElseIf dt.Rows(ix).Item("cdseqp").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqp").ToString + vbCrLf
                    iLine += 1
                ElseIf dt.Rows(ix).Item("cdseqh").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseqh").ToString + vbCrLf
                    iLine += 1
                ElseIf dt.Rows(ix).Item("cdseql").ToString <> "" Then
                    sCmt += dt.Rows(ix).Item("cdseql").ToString + vbCrLf
                    iLine += 1
                End If
            Next
        Else
            sRst = "   없음"
            sCmt = PRG_CONST.Tail_GV_NormalComment

            'strCmt += "      * 현재까지 의뢰된 검사중 종합검증 결과, 참고치를 유의하게 벗어나는 결과항목은 없습니다." + vbCrLf
            'strCmt += "        단, 입원 초기(입원 1-2일 사이)에 작성된 종합검증 판독이후에 시행된 검사는 고려되지 않습니다."

            iLine += 4
        End If

        sSlip = ""
        'strSlip += "   일반혈액검사, 혈액응고검사, 일반화학검사" 

        'strValue += "■ 검증항목" + vbCrLf + strSlip + vbCrLf + vbCrLf

        sValue += "■ 비정상 결과 혹은 유의한 결과를 보이는 항목" + vbCrLf + sRst + vbCrLf + vbCrLf

        sValue += "■ 검증방법" + vbCrLf
        sValue += "       ● Calibration                   ● Internal Quality Control" + vbCrLf
        sValue += "       ● Delta Check Verification      ● Panic/alert Value Veritication" + vbCrLf
        sValue += "       ● Repeat/Recheck                ○ Other" + vbCrLf + vbCrLf

        sValue += "■ 검증/판독 소견" + vbCrLf + sCmt + vbCrLf + vbCrLf

        'For intIdx As Integer = intLine To 21
        '    strValue += vbCrLf
        'Next

        fnGet_Verify = sValue

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

            Dim STU_StDataInfo As STU_StDataInfo
            Dim al_return As New ArrayList

            STU_StDataInfo = New STU_StDataInfo
            STU_StDataInfo.Data = fnGet_Verify()
            STU_StDataInfo.Alignment = 0
            al_return.Add(STU_StDataInfo)

            STU_StDataInfo = Nothing

            Return al_return

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New ArrayList

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Function

End Class

Public Class DA_ST_GV


    Public Function fnGet_Result_GV(ByVal r_dbCn As OracleConnection, ByVal rsBcNo As String) As DataTable

        Dim dbCn As OracleConnection = r_dbCn
        If r_dbCn Is Nothing Then dbCn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Try
            Dim sSql As String = "pkg_ack_rst.pkg_get_result_gv"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.StoredProcedure
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("io_cursor", OracleDbType.RefCursor).Value = ""
                .SelectCommand.Parameters("io_cursor").Direction = ParameterDirection.Output
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            Return dt
        Catch ex As Exception
            Return New DataTable
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

       
    End Function

End Class
