Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports Oracle.DataAccess.Client

Imports COMMON.SVar
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports DBORA.DbProvider

Public Class FGPOPUPST_BM_NMC
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_BM.vb, Class : FGPOPUPST_BM" & vbTab

    Private Const mc_iXmargin_right As Integer = 5
    Private Const mc_iYmargin_bottom As Integer = 20

    Private m_frm As Windows.Forms.Form
    Private m_dbCn As OracleConnection
    Private msBcNo As String = ""
    Private msTestCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

    Private msResult As String = ""
    Private mbSave As Boolean = False
    Private mbActivated As Boolean = False

    Public ReadOnly Property Append() As Boolean
        Get
            Append = False
        End Get
    End Property

    Public WriteOnly Property UserID() As String
        Set(ByVal Value As String)
            msUsrID = Value
        End Set
    End Property

    Private Sub sbDisplay_Init()

    End Sub

    Private Sub sbPrint_Data(ByVal rsData() As String)
        Dim prt As New PRT_ST_BM

        prt.sbPrint(rsData, msBcNo, msTestCd)

    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcNo As String, ByVal rsTestCd As String)
        Try
            sbDisplay_Init()

            Dim dt As DataTable = DA_ST_PBS_NMC.fnGet_Rst_SubInfo(rsBcNo, rsTestCd, m_dbCn)

            If dt.Rows.Count < 1 Then
                Dim sBuf() As String = DA_ST_BM_NMC.fnGet_Rst_WBCHbPLT(rsBcNo, m_dbCn).Split("|"c)

                Me.txtCBC_Hb.Text = sBuf(0) : Me.txtCBC_WBC.Text = sBuf(1) : Me.txtCBC_PLT.Text = sBuf(2)

                Return
            End If

            For ix As Integer = 0 To dt.Rows.Count - 1
                Select Case dt.Rows(ix).Item("testcd").ToString.Trim
                    Case msTestCd + Me.txtSlideno1.Tag.ToString
                        If dt.Rows(ix).Item("viewrst").ToString.Trim.Length > 0 Then
                            Me.txtSlideno1.Text = dt.Rows(ix).Item("viewrst").ToString.Trim.Substring(0, 1)
                            Me.txtSlideno2.Text = dt.Rows(ix).Item("viewrst").ToString.Trim.Replace("B", "")
                        End If

                    Case msTestCd + Me.txtCBC_Hb.Tag.ToString

                        Me.txtCBC_Hb.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtCBC_WBC.Tag.ToString

                        Me.txtCBC_WBC.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtCBC_PLT.Tag.ToString

                        Me.txtCBC_PLT.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtRBC.Tag.ToString

                        Me.txtRBC.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtWBC.Tag.ToString

                        Me.txtWBC.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtPLT.Tag.ToString

                        Me.txtPLT.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtCell.Tag.ToString

                        Me.txtCell.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtTCount.Tag.ToString

                        Me.txtTCount.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtM.Tag.ToString

                        Me.txtM.Text = dt.Rows(ix).Item("viewrst").ToString.Trim.Replace(":1", "")

                    Case msTestCd + Me.txtOther.Tag.ToString

                        Me.txtOther.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtMega.Tag.ToString

                        Me.txtMega.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtBm.Tag.ToString

                        Me.txtBm.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case msTestCd + Me.txtHemato.Tag.ToString

                        Me.txtHemato.Text = dt.Rows(ix).Item("viewrst").ToString.Trim

                    Case Else

                        With Me.spdDiff
                            For iRow As Integer = 2 To 7
                                For iCol As Integer = 2 To 8 Step 3
                                    .Row = iRow
                                    .Col = iCol

                                    If msTestCd + .CellTag.ToString = dt.Rows(ix).Item("testcd").ToString Then
                                        .Text = dt.Rows(ix).Item("viewrst").ToString
                                    End If
                                Next
                            Next
                        End With
                End Select
            Next

        Catch ex As Exception

        End Try

    End Sub

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_dbCn As OracleConnection, _
                                    ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm
        m_dbCn = r_dbCn
        msBcNo = rsBcNo
        msTestCd = rsTClsCd
        msTNm = rsTNm

        Try
            sbDisplay_Data(rsBcNo, rsTClsCd)

            Me.ShowDialog(r_frm)

            Dim STU_StDataInfo As STU_StDataInfo
            Dim al_return As New ArrayList

            If mbSave Then

                STU_StDataInfo = New STU_StDataInfo
                STU_StDataInfo.Data = msResult
                STU_StDataInfo.Alignment = 0
                al_return.Add(STU_StDataInfo)
                STU_StDataInfo = Nothing
            End If

            Return al_return

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function

    Private Function fnGet_Report() As String

        Dim sValue As String = ""
        Dim sBuf() As String
        Dim sLeft As String = Space(4)

        Try

            sValue += sLeft + Me.lblSlide.Text + " " + Me.txtSlideno1.Text + " - " + Me.txtSlideno2.Text + vbCrLf + vbCrLf
            sValue += sLeft + Me.lblPb.Text + Space(10) + "Hb - WBC - PLT" + Space(14)
            sValue += Me.txtCBC_Hb.Text + " - " + Me.txtCBC_WBC.Text + " - " + Me.txtCBC_PLT.Text + " " + Me.lblK.Text + vbCrLf

            sValue += sLeft + "-".PadLeft(90, "-"c) + vbCrLf

            sBuf = Me.txtRBC.Text.Split(Chr(13))
            sValue += sLeft + Space(3) + Me.lblRBC.Text + " "
            For ix As Integer = 0 To sBuf.Length - 1
                If ix = 0 Then
                    sValue += sBuf(ix).Replace(vbLf, "") + vbCrLf
                Else
                    sValue += sLeft + Space(13) + sBuf(ix).Replace(vbLf, "") + vbCrLf
                End If
            Next

            sBuf = Me.txtWBC.Text.Split(Chr(13))
            sValue += sLeft + Space(3) + Me.lblWBC.Text + " "
            For ix As Integer = 0 To sBuf.Length - 1
                If ix = 0 Then
                    sValue += sBuf(ix).Replace(vbLf, "") + vbCrLf
                Else
                    sValue += sLeft + Space(13) + sBuf(ix).Replace(vbLf, "") + vbCrLf
                End If
            Next

            sBuf = Me.txtPLT.Text.Split(Chr(13))
            sValue += sLeft + Space(3) + Me.lblPLT.Text + " "
            For ix As Integer = 0 To sBuf.Length - 1
                If ix = 0 Then
                    sValue += sBuf(ix).Replace(vbLf, "") + vbCrLf
                Else
                    sValue += sLeft + Space(13) + sBuf(ix).Replace(vbLf, "") + vbCrLf
                End If
            Next

            sValue += sLeft + "-".PadLeft(90, "-"c) + vbCrLf

            sValue += sLeft + Me.lblDiif.Text + vbCrLf
            sValue += sLeft + Space(3) + Me.lblCell.Text + " " + Me.txtCell.Text + Space(10)
            sValue += Me.lblTcount.Text + " " + Me.txtTCount.Text + " %" + Space(10)
            sValue += Me.lblM.Text + " " + Me.txtM.Text + " " + Me.lblE.Text + vbCrLf + vbCrLf

            With Me.spdDiff
                For iRow As Integer = 2 To 7
                    sValue += sLeft + Space(3)
                    .Row = iRow
                    .Col = 1 : sValue += .Text.Trim.PadRight(15, " "c)
                    .Col = 2 : sValue += (.Text.Trim + " %").PadRight(10, " "c)

                    .Col = 4 : sValue += .Text.Trim.PadRight(20, " "c)
                    .Col = 5 : sValue += (.Text.Trim + " %").PadRight(10, " "c)

                    .Col = 7 : sValue += .Text.Trim.PadRight(20, " "c)
                    .Col = 8 : sValue += (.Text.Trim + " %").PadRight(10, " "c) + vbCrLf
                Next
            End With

            sValue += vbCrLf

            sBuf = Me.txtOther.Text.Split(Chr(13))
            sValue += sLeft + Space(3) + Me.lblOther.Text + vbCrLf
            For ix As Integer = 0 To sBuf.Length - 1
                sValue += sLeft + Space(6) + sBuf(ix).Replace(vbLf, "") + vbCrLf
            Next

            sValue += vbCrLf

            sBuf = Me.txtMega.Text.Split(Chr(13))
            sValue += sLeft + Space(3) + Me.lblMega.Text + vbCrLf
            For ix As Integer = 0 To sBuf.Length - 1
                sValue += sLeft + Space(6) + sBuf(ix).Replace(vbLf, "") + vbCrLf
            Next

            sValue += vbCrLf

            sBuf = Me.txtBm.Text.Split(Chr(13))
            sValue += sLeft + Space(3) + Me.lblBM.Text + vbCrLf
            For ix As Integer = 0 To sBuf.Length - 1
                sValue += sLeft + Space(6) + sBuf(ix).Replace(vbLf, "") + vbCrLf
            Next

            sValue += sLeft + "-".PadLeft(90, "-"c) + vbCrLf

            sBuf = Me.txtHemato.Text.Split(Chr(13))
            sValue += sLeft + Me.lblHemato.Text + vbCrLf
            For ix As Integer = 0 To sBuf.Length - 1
                sValue += sLeft + Space(3) + sBuf(ix).Replace(vbLf, "") + vbCrLf
            Next

        Catch ex As Exception

        Finally
            fnGet_Report = sValue
        End Try

    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        msResult = ""
        mbSave = False
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            Dim sRstInfo As String = ""

            sRstInfo += msTestCd + Me.txtSlideno1.Tag.ToString + "^" + Me.txtSlideno1.Text + Me.txtSlideno2.Text + "|"
            If Me.txtCBC_Hb.Text <> "" Then sRstInfo += msTestCd + Me.txtCBC_Hb.Tag.ToString + "^" + Me.txtCBC_Hb.Text + "|"
            If Me.txtCBC_Hb.Text <> "" Then sRstInfo += msTestCd + Me.txtCBC_WBC.Tag.ToString + "^" + Me.txtCBC_WBC.Text + "|"
            If Me.txtCBC_PLT.Text <> "" Then sRstInfo += msTestCd + Me.txtCBC_PLT.Tag.ToString + "^" + Me.txtCBC_PLT.Text + "|"

            If Me.txtRBC.Text <> "" Then sRstInfo += msTestCd + Me.txtRBC.Tag.ToString + "^" + Me.txtRBC.Text + "|"
            If Me.txtWBC.Text <> "" Then sRstInfo += msTestCd + Me.txtWBC.Tag.ToString + "^" + Me.txtWBC.Text + "|"
            If Me.txtPLT.Text <> "" Then sRstInfo += msTestCd + Me.txtPLT.Tag.ToString + "^" + Me.txtPLT.Text + "|"

            If Me.txtCell.Text <> "" Then sRstInfo += msTestCd + Me.txtCell.Tag.ToString + "^" + Me.txtCell.Text + "|"
            If Me.txtTCount.Text <> "" Then sRstInfo += msTestCd + Me.txtTCount.Tag.ToString + "^" + Me.txtTCount.Text + "|"
            sRstInfo += msTestCd + Me.txtM.Tag.ToString + "^" + Me.txtM.Text + ":1" + "|"


            If Me.txtOther.Text <> "" Then sRstInfo += msTestCd + Me.txtOther.Tag.ToString + "^" + Me.txtOther.Text + "|"
            If Me.txtMega.Text <> "" Then sRstInfo += msTestCd + Me.txtMega.Tag.ToString + "^" + Me.txtMega.Text + "|"
            If Me.txtBm.Text <> "" Then sRstInfo += msTestCd + Me.txtBm.Tag.ToString + "^" + Me.txtBm.Text + "|"
            If Me.txtHemato.Text <> "" Then sRstInfo += msTestCd + Me.txtHemato.Tag.ToString + "^" + Me.txtHemato.Text + "|"

            With Me.spdDiff
                For iRow As Integer = 2 To 7
                    For iCol As Integer = 2 To 8 Step 3
                        .Row = iRow
                        .Col = iCol

                        If .Text <> "" Then
                            sRstInfo += msTestCd + .CellTag.ToString + "^" + .Text + "|"
                        End If
                    Next
                Next
            End With

            Dim bRet As Boolean = DA_ST_BM_NMC.fnExe_Insert(msBcNo, sRstInfo)

            If bRet = False Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "데이타시 저장시 오류가 발생했습니다.!!")
                mbSave = False
                Return
            Else
                msResult = fnGet_Report()
                mbSave = True
                Me.Close()
            End If

        Catch ex As Exception
            mbSave = False
        End Try

    End Sub

    Private Sub txtWBC_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCBC_Hb.GotFocus, txtCBC_WBC.GotFocus, txtCBC_PLT.GotFocus, txtRBC.GotFocus, txtWBC.GotFocus, txtPLT.GotFocus, txtOther.GotFocus, txtTCount.GotFocus, txtM.GotFocus, txtMega.GotFocus

        CType(sender, Windows.Forms.TextBox).SelectAll()

    End Sub

    Private Sub txtWBC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCBC_Hb.KeyDown, txtCBC_WBC.KeyDown, txtCBC_PLT.KeyDown, txtTCount.KeyDown, txtM.KeyDown, txtMega.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Dim sRstCont As String = DA_ST_BM.fnGet_CodeToRst(msTestCd, CType(sender, System.Windows.Forms.TextBox).Text, m_dbCn)
        If sRstCont <> "" Then CType(sender, System.Windows.Forms.TextBox).Text = sRstCont

        If CType(sender, Windows.Forms.TextBox).Name = "txtM" Then
            With spdDiff
                .Row = 2 : .Col = 2 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                .Focus()
            End With

        Else
            SendKeys.Send("{TAB}")
        End If

    End Sub

    Private Sub spdRst1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdDiff.GotFocus
        'With CType(sender, AxFPSpreadADO.AxfpSpread)
        '    .Row = 2 : .Col = 2 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
        'End With
    End Sub

    Private Sub spdRst1_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdDiff.KeyDownEvent
        If e.keyCode <> Keys.Enter Then Return

        With spdDiff
            .Row = .ActiveRow
            .Col = .ActiveCol : Dim strRstCont As String = DA_ST_BM.fnGet_CodeToRst(msTestCd, .Text, m_dbCn)
            If strRstCont <> "" Then .Text = strRstCont

            If .ActiveCol = 2 And .ActiveRow = 7 Then
                .Row = 1 : .Col = 5 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            ElseIf .ActiveCol = 5 And .ActiveRow = 7 Then
                .Row = 1 : .Col = 8 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            ElseIf .ActiveCol = 8 And .ActiveRow = 8 Then
                txtOther.Focus()
            End If
        End With
    End Sub

    Private Sub btnHelp_bm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_bm.Click, btnHelp_hemato.Click, btnHelp_mega.Click, btnHelp_oth.Click, btnHelp_p.Click, btnHelp_r.Click, btnHelp_w.Click
        Try
            Dim objBtn As Windows.Forms.Button = CType(sender, Windows.Forms.Button)
            Dim objTxt As Windows.Forms.TextBox

            Dim sTestCd As String

            Select Case objBtn.Name.ToLower
                Case "btnhelp_r" : sTestCd = Me.txtRBC.Tag.ToString : objTxt = Me.txtRBC
                Case "btnhelp_w" : sTestCd = Me.txtWBC.Tag.ToString : objTxt = Me.txtWBC
                Case "btnhelp_p" : sTestCd = Me.txtPLT.Tag.ToString : objTxt = Me.txtPLT
                Case "btnhelp_oth" : sTestCd = Me.txtOther.Tag.ToString : objTxt = Me.txtOther
                Case "btnhelp_bm" : sTestCd = Me.txtOther.Tag.ToString : objTxt = Me.txtOther
                Case "btnhelp_hemato" : sTestCd = Me.txtHemato.Tag.ToString : objTxt = Me.txtHemato
                Case "btnHelp_mega" : sTestCd = Me.txtMega.Tag.ToString : objTxt = Me.txtMega
            End Select

            Dim iHeight As Integer = Convert.ToInt32(objBtn.Height)
            Dim iWidth As Integer = Convert.ToInt32(objBtn.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Me.Top + objBtn.Top + Ctrl.menuHeight - 50

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Me.Left + objBtn.Left
            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - objBtn.Width)

            Dim dt As DataTable = DA_ST_BM_NMC.fnGet_RstCd_Info(msTestCd + sTestCd)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "결과코드 정보"
            objHelp.MaxRows = 15

            objHelp.AddField("rstcont", "내용", 100, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then objTxt.Text = alList.Item(0).ToString.Split("|"c)(0)

        Catch ex As Exception

        End Try

    End Sub
End Class


Public Class DA_ST_BM_NMC

    '--  결과코드
    Public Shared Function fnGet_RstCd_Info(ByVal rsTestCd As String) As DataTable
        Dim oleDbCn As OracleConnection = GetDbConnection()
        Dim oleDbDa As oracleDataAdapter
        Dim oleDbCmd As New oracleCommand

        Try
            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT rstcont"
            sSql += "  FROM lf083m"
            sSql += " WHERE testcd = :testcd"
            sSql += "   AND spccd  = '" + "".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"

            oleDbCmd.Connection = oleDbCn
            oleDbCmd.CommandType = CommandType.Text
            oleDbCmd.CommandText = sSql

            oleDbDa = New oracleDataAdapter(oleDbCmd)

            With oleDbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
            End With

            dt.Reset()
            oleDbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Return New DataTable
        End Try

    End Function

    '-- 서브결과
    Public Shared Function fnGet_Rst_SubInfo(ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
        Dim oleDbCn As OracleConnection = GetDbConnection()
        Dim oleDbDa As oracleDataAdapter
        Dim oleDbCmd As New oracleCommand

        Try
            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT r.testcd, r.viewrst, r.eqflag"
            sSql += "  FROM lr010m r,"
            sSql += " WHERE r.bcno   = :bcno"
            sSql += "   AND r.testcd LIKE :testcd || '%'"

            oleDbCmd.Connection = oleDbCn
            oleDbCmd.CommandType = CommandType.Text
            oleDbCmd.CommandText = sSql

            oleDbDa = New oracleDataAdapter(oleDbCmd)

            With oleDbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
            End With

            dt.Reset()
            oleDbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Return New DataTable
        End Try

    End Function

    Public Shared Function fnGet_Rst_WBCHbPLT(ByVal rsBcNo As String, ByVal r_DbCn As oracleConnection) As String

        Dim oledbcn As oracleConnection = r_DbCn
        Dim oledbda As oracleDataAdapter
        Dim oledbcmd As New oracleCommand

        Dim dt As New DataTable

        Dim sSql As String = ""
        Try
            sSql = ""
            sSql += "SELECT fn_ack_get_refrst_bm(:bcno) FROM DUAL"

            oledbcmd.Connection = oledbcn
            oledbcmd.CommandType = CommandType.Text
            oledbcmd.CommandText = sSql

            oledbda = New oracleDataAdapter(oledbcmd)

            With oledbda
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            dt.Reset()
            oledbda.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return "|||"
            End If

        Catch ex As Exception
            Return "|||"
        End Try

    End Function

    Public Shared Function fnExe_Insert(ByVal rsBcNo As String, ByVal rsRstInfo As String) As Boolean

        Try

            'Dim sBuf() As String = rsRstInfo.Split("|"c)
            'Dim stuSample As New STU_SampleInfo
            'Dim arlRstInfo As New ArrayList
            'Dim arlSUCC As New ArrayList

            'stuSample.BCNo = rsBcNo
            'stuSample.EqCd = ""
            'stuSample.UsrID = USER_INFO.USRID
            'stuSample.UsrIP = USER_INFO.LOCALIP
            'stuSample.IntSeqNo = ""
            'stuSample.Rack = ""
            'stuSample.Pos = ""
            'stuSample.EqBCNo = ""

            'stuSample.SenderID = ""
            'stuSample.RegStep = "1"

            'If sBuf.Length < 1 Then Return False

            'For ix As Integer = 0 To sBuf.Length - 1
            '    If sBuf(ix) <> "" Then
            '        Dim stuResult As New STU_RstInfo

            '        stuResult.TestCd = sBuf(ix).Split("^"c)(0)
            '        stuResult.OrgRst = sBuf(ix).Split("^"c)(1)
            '        stuResult.RstCmt = ""

            '        arlRstInfo.Add(stuResult)
            '    End If

            'Next

            'Dim da_regrst As New LISAPP.APP_R.RegFn
            'Dim iRet As Integer = da_regrst.RegServer(arlRstInfo, stuSample, arlSUCC, False)

            'If iRet < 1 Then
            '    Return False
            'Else
            '    Return True
            'End If

            Return True
        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try

    End Function

    Public Shared Function fnExe_LRS11M(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsRst As String) As Boolean

        Dim oleDbCn As OracleConnection = GetDbConnection()
        Dim oleDbTrans As oracleTransaction = oleDbCn.BeginTransaction()

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim OleDbCmd As New oracleCommand

            Dim sqlDoc As String = ""
            Dim intRet As Integer = 0

            With OleDbCmd
                .Connection = oleDbCn
                .Transaction = oleDbTrans
                .CommandType = CommandType.Text
                .CommandText = "delete lrs11m where bcno = :bcno and testcd = :testcd"

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd

                .ExecuteNonQuery()

                sqlDoc = ""
                sqlDoc += "insert into lrs11m(  bcno,  testcd,  rsttxt, rstdt )"
                sqlDoc += "            values( :bcno, :testcd, :rsttxt, fn_ack_sysdate)"

                .CommandText = sqlDoc

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
                .Parameters.Add("rsttxt", OracleDbType.Varchar2).Value = rsRst

                intRet = .ExecuteNonQuery()
            End With

            If intRet = 0 Then
                oleDbTrans.Rollback()
            Else
                oleDbTrans.Commit()
            End If

            Return True

        Catch ex As Exception

            oleDbTrans.Rollback()
            MsgBox(ex.Message)
            Return False
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

End Class


Public Class PRT_ST_BM_NMC
    Private Const msFile As String = "File : PGPOUPST_BM.vb, Class : POPUPWIN" + vbTab

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 0
    Private msgTop As Single = 0

    Private msgPosX() As Single
    Private msgPosY() As Single

    Private msPrtData() As String

    Public Sub sbPrint(ByVal rsBuf() As String, ByVal rsBcNo As String, ByVal rsTclsCd As String)
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        msPrtData = rsBuf

        Try

            Dim prtDialog As New PrintDialog

            prtDialog.Document = prtR

            prtR.DocumentName = "ACK_" + rsBcNo + "_" + rsTclsCd

            AddHandler prtR.PrintPage, AddressOf sbPrintPage
            AddHandler prtR.BeginPrint, AddressOf sbPrintData
            AddHandler prtR.EndPrint, AddressOf sbReport
            prtR.Print()

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0
        Dim sngTmp As Single = 0
        Dim strTmp As String = ""

        Dim fnt_Label As New Font("굴림체", 9, FontStyle.Bold)
        Dim fnt_Rst As New Font("굴림체", 9, FontStyle.Regular)
        Dim fnt_ULine As New Font("굴림체", 9, FontStyle.Underline)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width '- 15
        msgHeight = e.PageBounds.Bottom '- 12
        msgLeft = 5
        msgTop = 2

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = fnt_Rst.GetHeight(e.Graphics)

        Dim rect As New Drawing.RectangleF

        sngTmp = msgWidth / 3

        '-- 1) 0, 1, 2, 3
        sngPosY = msgTop
        rect = New Drawing.RectangleF(msgLeft, sngPosY + sngPrtH * 0, sngTmp, sngPrtH)
        e.Graphics.DrawString("말초혈액도말 관찰 소견", fnt_Label, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(msgLeft + sngTmp, sngPosY + sngPrtH * 0, sngTmp, sngPrtH)
        e.Graphics.DrawString("WBC - Hb - PLT - Reti", fnt_Rst, Drawing.Brushes.Black, rect, sf_c)

        strTmp = msPrtData(0) + " - " + msPrtData(1) + " - " + msPrtData(2) + "k - " + msPrtData(3) + "%"

        rect = New Drawing.RectangleF(msgLeft + sngTmp * 2, sngPosY + sngPrtH * 0, sngTmp, sngPrtH)
        e.Graphics.DrawString(strTmp, fnt_Rst, Drawing.Brushes.Black, rect, sf_r)

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrtH * 1, msgWidth, sngPosY + sngPrtH * 1)

        '-- 2) 적혈구
        sngPosY += sngPrtH

        Dim strBuf() As String = msPrtData(4).Split(Chr(13))

        If strBuf.Length > 0 Then
            rect = New Drawing.RectangleF(msgLeft, sngPosY, 100, sngPrtH * strBuf.Length)
            e.Graphics.DrawString("적혈구", fnt_Label, Drawing.Brushes.Black, rect, sf_l)

            For intIdx As Integer = 0 To strBuf.Length - 1
                rect = New Drawing.RectangleF(msgLeft, sngPosY + sngPrtH * intIdx, 100, msgWidth - msgLeft)
                e.Graphics.DrawString(strBuf(intIdx).Replace(vbLf, ""), fnt_Rst, Drawing.Brushes.Black, rect, sf_l)
            Next

            sngPosY += sngPrtH * strBuf.Length
        Else
            sngPosY += sngPrtH * 3
        End If

        '-- 3) 백혈구
        strBuf = msPrtData(5).Split(Chr(13))

        If strBuf.Length > 0 Then
            rect = New Drawing.RectangleF(msgLeft, sngPosY, 100, sngPrtH * strBuf.Length)
            e.Graphics.DrawString("백혈구", fnt_Label, Drawing.Brushes.Black, rect, sf_l)

            For intIdx As Integer = 0 To strBuf.Length - 1
                rect = New Drawing.RectangleF(msgLeft, sngPosY + sngPrtH * intIdx, 100, msgWidth - msgLeft)
                e.Graphics.DrawString(strBuf(intIdx).Replace(vbLf, ""), fnt_Rst, Drawing.Brushes.Black, rect, sf_l)
            Next

            sngPosY += sngPrtH * strBuf.Length
        Else
            sngPosY += sngPrtH * 3
        End If

        '-- 4) 혈소판
        strBuf = msPrtData(6).Split(Chr(13))

        If strBuf.Length > 0 Then
            rect = New Drawing.RectangleF(msgLeft, sngPosY, 100, sngPrtH * strBuf.Length)
            e.Graphics.DrawString("혈소판", fnt_Label, Drawing.Brushes.Black, rect, sf_l)

            For intIdx As Integer = 0 To strBuf.Length - 1
                rect = New Drawing.RectangleF(msgLeft, sngPosY + sngPrtH * intIdx, 100, msgWidth - msgLeft)
                e.Graphics.DrawString(strBuf(intIdx).Replace(vbLf, ""), fnt_Rst, Drawing.Brushes.Black, rect, sf_l)
            Next

            sngPosY += sngPrtH * strBuf.Length
        Else
            sngPosY += sngPrtH * 3
        End If

        '-- 5) 7, 8
        sngTmp = msgWidth / 3

        rect = New Drawing.RectangleF(msgLeft, sngPosY + sngPrtH * 0, sngTmp, sngPrtH * 2)
        e.Graphics.DrawString("골수천자도말 관찰 소견", fnt_Label, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(msgLeft + sngTmp, sngPosY + sngPrtH * 0, 100, sngPrtH * 2)
        e.Graphics.DrawString("Total Count", fnt_Rst, Drawing.Brushes.Black, rect, sf_c)

        strTmp = msPrtData(7).PadLeft(10, " "c) + " 세포"
        rect = New Drawing.RectangleF(msgLeft + sngTmp + 100, sngPosY + sngPrtH * 0, sngTmp - 100, sngPrtH * 2)
        e.Graphics.DrawString(strTmp, fnt_Rst, Drawing.Brushes.Black, rect, sf_c)

        strTmp = "M : E ratio " + msPrtData(8).PadLeft(10, " "c) + " : 1"
        rect = New Drawing.RectangleF(msgLeft + sngTmp * 2, sngPosY + sngPrtH * 0, sngTmp, sngPrtH * 2)
        e.Graphics.DrawString(strTmp, fnt_Rst, Drawing.Brushes.Black, rect, sf_c)

        '-- 6) 
        sngPosY += sngPrtH

        Dim sgPosX(0 To 6) As Single
        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(0) + 100
        sgPosX(2) = sgPosX(1) + 100
        sgPosX(3) = sgPosX(2) + 100
        sgPosX(4) = sgPosX(3) + 100
        sgPosX(5) = sgPosX(4) + 100
        sgPosX(6) = msgWidth

        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("Blast", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString(msPrtData(9).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(2), sngPosY, sgPosX(3) - sgPosX(2), sngPrtH)
        e.Graphics.DrawString("Pronormoblast", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sngPosY, sgPosX(4) - sgPosX(3), sngPrtH)
        e.Graphics.DrawString(msPrtData(15).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(4), sngPosY, sgPosX(5) - sgPosX(4), sngPrtH)
        e.Graphics.DrawString("Lymphocyte", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(5), sngPosY, sgPosX(6) - sgPosX(5), sngPrtH)
        e.Graphics.DrawString(msPrtData(21).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        sngPosY += sngPrtH
        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("Promyeloyte", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(2) - sgPosX(1), sngPrtH)
        e.Graphics.DrawString(msPrtData(10).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(2), sngPosY, sgPosX(3) - sgPosX(2), sngPrtH)
        e.Graphics.DrawString("Basophilic N.", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sngPosY, sgPosX(4) - sgPosX(3), sngPrtH)
        e.Graphics.DrawString(msPrtData(16).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(4), sngPosY, sgPosX(5) - sgPosX(4), sngPrtH)
        e.Graphics.DrawString("Plasma cell", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(5), sngPosY, sgPosX(6) - sgPosX(5), sngPrtH)
        e.Graphics.DrawString(msPrtData(22).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        sngPosY += sngPrtH
        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("Myelocyte", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(2) - sgPosX(1), sngPrtH)
        e.Graphics.DrawString(msPrtData(11).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(2), sngPosY, sgPosX(3) - sgPosX(2), sngPrtH)
        e.Graphics.DrawString("Polychromatophilic N.", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sngPosY, sgPosX(4) - sgPosX(3), sngPrtH)
        e.Graphics.DrawString(msPrtData(17).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(4), sngPosY, sgPosX(5) - sgPosX(4), sngPrtH)
        e.Graphics.DrawString("Histiocyte", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(5), sngPosY, sgPosX(6) - sgPosX(5), sngPrtH)
        e.Graphics.DrawString(msPrtData(23).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        sngPosY += sngPrtH
        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("Metamyelocyte", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(2) - sgPosX(1), sngPrtH)
        e.Graphics.DrawString(msPrtData(12).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(2), sngPosY, sgPosX(3) - sgPosX(2), sngPrtH)
        e.Graphics.DrawString("Orthochromic N.", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sngPosY, sgPosX(4) - sgPosX(3), sngPrtH)
        e.Graphics.DrawString(msPrtData(18).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(4), sngPosY, sgPosX(5) - sgPosX(4), sngPrtH)
        e.Graphics.DrawString("Monocyte", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(5), sngPosY, sgPosX(6) - sgPosX(5), sngPrtH)
        e.Graphics.DrawString(msPrtData(24).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        sngPosY += sngPrtH
        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("Band form", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(2) - sgPosX(1), sngPrtH)
        e.Graphics.DrawString(msPrtData(13).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(2), sngPosY, sgPosX(3) - sgPosX(2), sngPrtH)
        e.Graphics.DrawString("Basophilic series", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sngPosY, sgPosX(4) - sgPosX(3), sngPrtH)
        e.Graphics.DrawString(msPrtData(19).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(4), sngPosY, sgPosX(5) - sgPosX(4), sngPrtH)
        e.Graphics.DrawString("Immature mono", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(5), sngPosY, sgPosX(6) - sgPosX(5), sngPrtH)
        e.Graphics.DrawString(msPrtData(25).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        sngPosY += sngPrtH
        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("Neutrophil", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(2) - sgPosX(1), sngPrtH)
        e.Graphics.DrawString(msPrtData(14).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(2), sngPosY, sgPosX(3) - sgPosX(2), sngPrtH)
        e.Graphics.DrawString("Eosinophilic series", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sngPosY, sgPosX(4) - sgPosX(3), sngPrtH)
        e.Graphics.DrawString(msPrtData(20).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(4), sngPosY, sgPosX(5) - sgPosX(4), sngPrtH)
        e.Graphics.DrawString("Immature cells", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(5), sngPosY, sgPosX(4) - sgPosX(5), sngPrtH)
        e.Graphics.DrawString(msPrtData(26).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        sngPosY += sngPrtH
        rect = New Drawing.RectangleF(sgPosX(4), sngPosY, sgPosX(5) - sgPosX(4), sngPrtH)
        e.Graphics.DrawString("Others", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(5), sngPosY, sgPosX(6) - sgPosX(5), sngPrtH)
        e.Graphics.DrawString(msPrtData(27).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        '-- 5)
        sngPosY += sngPrtH
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 5)
        e.Graphics.DrawString(msPrtData(28).PadLeft(20, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_l)

        '-- 6)
        sngPosY += sngPrtH
        strTmp = "●     BMB - Inadequate specimen"
        rect = New Drawing.RectangleF(msgLeft + 100, sngPosY, msgWidth - 100, sngPrtH)
        e.Graphics.DrawString(strTmp, fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        '-- 7)
        sngPosY += sngPrtH
        ReDim sgPosX(0 To 4)
        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(1) + 200
        sgPosX(2) = sgPosX(1) + 200
        sgPosX(3) = sgPosX(2) + 200
        sgPosX(4) = msgWidth

        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("골수 저장철 평가", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(2) - sgPosX(1), sngPrtH)
        e.Graphics.DrawString(msPrtData(29).PadLeft(30, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(2), sngPosY, sgPosX(3) - sgPosX(2), sngPrtH)
        e.Graphics.DrawString("Chromosome/FISH", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sngPosY, sgPosX(4) - sgPosX(3), sngPrtH)
        e.Graphics.DrawString(msPrtData(30).PadLeft(30, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        '-- 8)
        sngPosY += sngPrtH
        ReDim sgPosX(0 To 2)
        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(1) + 200
        sgPosX(2) = msgWidth

        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("Clot section", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, sgPosX(2) - sgPosX(1), sngPrtH)
        e.Graphics.DrawString(msPrtData(31), fnt_ULine, Drawing.Brushes.Black, rect, sf_l)

        '-- 9)
        sngPosY += sngPrtH
        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("Clot section", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, 400, sngPrtH)
        e.Graphics.DrawString(msPrtData(32).PadLeft(30, " "c), fnt_ULine, Drawing.Brushes.Black, rect, sf_l)

        '-- 10)
        sngPosY += sngPrtH
        sngTmp = msgWidth / 5

        ReDim sgPosX(0 To 5)
        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(0) + sngTmp
        sgPosX(2) = sgPosX(1) + sngTmp
        sgPosX(3) = sgPosX(2) + sngTmp
        sgPosX(4) = sgPosX(3) + sngTmp
        sgPosX(5) = msgWidth

        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH)
        e.Graphics.DrawString("세포화학염색", fnt_Label, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1), sngPosY, 200, sngPrtH)
        e.Graphics.DrawString("MPO", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(1) + 200, sngPosY, sgPosX(2) - sgPosX(1) - 200, sngPrtH)
        e.Graphics.DrawString(msPrtData(33), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(2), sngPosY, 200, sngPrtH)
        e.Graphics.DrawString("SBB", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(2) + 200, sngPosY, sgPosX(3) - sgPosX(2) - 200, sngPrtH)
        e.Graphics.DrawString(msPrtData(34), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(3), sngPosY, 200, sngPrtH)
        e.Graphics.DrawString("PAS", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3) + 200, sngPosY, sgPosX(4) - sgPosX(3) - 200, sngPrtH)
        e.Graphics.DrawString(msPrtData(35), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(4), sngPosY, 200, sngPrtH)
        e.Graphics.DrawString("ANAE", fnt_Rst, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(4) + 200, sngPosY, sgPosX(5) - sgPosX(4) - 200, sngPrtH)
        e.Graphics.DrawString(msPrtData(36), fnt_ULine, Drawing.Brushes.Black, rect, sf_c)

        '-- 11)
        sngPosY += sngPrtH
        ReDim sgPosX(0 To 2)
        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(1) + 200
        sgPosX(2) = msgWidth

        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH * 4)
        e.Graphics.DrawString("의견 정리", fnt_Label, Drawing.Brushes.Black, rect, sf_l)

        For intIdx As Integer = 0 To 3
            rect = New Drawing.RectangleF(sgPosX(1), sngPosY + sngPrtH * intIdx, sgPosX(2) - sgPosX(1), sngPrtH)
            e.Graphics.DrawString(msPrtData(37 + intIdx), fnt_Rst, Drawing.Brushes.Black, rect, sf_l)
        Next

        '-- 12)
        sngPosY += sngPrtH * 4
        rect = New Drawing.RectangleF(sgPosX(0), sngPosY, sgPosX(1) - sgPosX(0), sngPrtH * 4)
        e.Graphics.DrawString("혈액학젹 진단", fnt_Label, Drawing.Brushes.Black, rect, sf_l)

        For intIdx As Integer = 0 To 3
            rect = New Drawing.RectangleF(sgPosX(1), sngPosY + sngPrtH * intIdx, sgPosX(2) - sgPosX(1), sngPrtH)
            e.Graphics.DrawString(msPrtData(40 + intIdx), fnt_Rst, Drawing.Brushes.Black, rect, sf_l)
        Next

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)
        e.HasMorePages = False

    End Sub

End Class