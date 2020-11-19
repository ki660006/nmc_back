Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing
Imports Oracle.DataAccess.Client

Imports COMMON.SVar
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports DBORA.DbProvider

Public Class FGPOPUPST_BM
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGPOPUPST_BM.vb, Class : FGPOPUPST_BM" & vbTab

    Private Const mc_iXmargin_right As Integer = 5
    Private Const mc_iYmargin_bottom As Integer = 20

    Private m_frm As Windows.Forms.Form
    Private m_dbcn As OracleConnection

    Private msBcNo As String = ""
    Private msTClsCd As String = ""
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

        prt.sbPrint(rsData, msBcNo, msTClsCd)

    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal r_DbCn As OracleConnection)

        Try
            Dim sBuf() As String = DA_ST_BM.fnGet_Rst_WBCHbPLT(rsBcNo, r_DbCn).Split("|"c)

            Me.txtWBC.Text = sBuf(0) : Me.txtHb.Text = sBuf(1) : Me.txtPLT.Text = sBuf(2)

            Dim strTmp As String = DA_ST_BM.fnGet_Rst_Bm(rsBcNo, rsTclsCd, r_DbCn)
            If strTmp = "" Then Return

            strTmp += "|||||||||||||||||||||||||||||||||||"

            sBuf = strTmp.Split("|"c)

            Me.txtWBC.Text = sBuf(0) : Me.txtHb.Text = sBuf(1) : Me.txtPLT.Text = sBuf(2) : Me.txtReti.Text = sBuf(3)

            Me.txtCmt1.Text = sBuf(4) : Me.txtCmt2.Text = sBuf(5) : Me.txtCmt3.Text = sBuf(6)
            Me.txtTCnt.Text = sBuf(7) : Me.txtM.Text = sBuf(8)

            With Me.spdRst1
                For intIdx As Integer = 2 To 7
                    .Col = 2 : .Row = intIdx : .Text = sBuf(9 - 2 + intIdx)
                    .Col = 5 : .Row = intIdx : .Text = sBuf(15 - 2 + intIdx)
                    .Col = 8 : .Row = intIdx : .Text = sBuf(21 - 2 + intIdx)
                Next

                .Col = 8 : .Row = 8 : .Text = sBuf(27)

                .Row = 2 : .Col = 2 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            End With

            Me.txtComment.Text = sBuf(28)
            Me.txtBMB.Text = sBuf(29)

            With Me.spdRst2
                .Row = 2 : .Col = 2 : .Text = sBuf(30)
                .Row = 3 : .Col = 2 : .Text = sBuf(31)

                .Row = 5 : .Col = 2 : .Text = sBuf(32)
                .Row = 7 : .Col = 2 : .Text = sBuf(33)

                .Row = 9 : .Col = 3 : .Text = sBuf(34)
                .Row = 9 : .Col = 5 : .Text = sBuf(35)
                .Row = 9 : .Col = 7 : .Text = sBuf(36)
                .Row = 9 : .Col = 9 : .Text = sBuf(37)

                .Row = 11 : .Col = 3 : .Text = sBuf(38)
                .Row = 12 : .Col = 3 : .Text = sBuf(39)
                .Row = 13 : .Col = 3 : .Text = sBuf(40)
                .Row = 14 : .Col = 3 : .Text = sBuf(41)
                .Row = 15 : .Col = 3 : .Text = sBuf(42)
                .Row = 16 : .Col = 3 : .Text = sBuf(43)
                .Row = 17 : .Col = 3 : .Text = sBuf(44)
                .Row = 18 : .Col = 3 : .Text = sBuf(45)

                .Row = 20 : .Col = 3 : .Text = sBuf(46) : .Row = 20 : .Col = 10 : .Text = sBuf(47)
                .Row = 21 : .Col = 3 : .Text = sBuf(48) : .Row = 21 : .Col = 10 : .Text = sBuf(49)
                .Row = 22 : .Col = 3 : .Text = sBuf(50) : .Row = 22 : .Col = 10 : .Text = sBuf(51)
                .Row = 23 : .Col = 3 : .Text = sBuf(52) : .Row = 23 : .Col = 10 : .Text = sBuf(53)

                .Row = 2 : .Col = 2 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            End With

        Catch ex As Exception

        End Try
    End Sub

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_dBcn As OracleConnection, _
                                    ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm
        m_dbcn = r_dBcn
        msBcNo = rsBcNo
        msTClsCd = rsTClsCd
        msTNm = rsTNm

        Try
            sbDisplay_Data(rsBcNo, rsTClsCd, r_dBcn)

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

        Dim strVal As String = ""
        Dim strBuf() As String

        Try
            Dim strTmp As String = ""

            strVal += "  말초혈액도말 관찰 소견" + Space(10) + "WBC - Hb - PLT - Reti" + Space(14)
            strVal += (Me.txtWBC.Text + " - " + Me.txtHb.Text + " - " + Me.txtPLT.Text + "k - " + Me.txtReti.Text + "%").PadLeft(20, " "c) + vbCrLf

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            strBuf = txtCmt1.Text.Split(Chr(13))
            strVal += "  적혈구: "
            For intIdx As Integer = 0 To strBuf.Length - 1
                If intIdx = 0 Then
                    strVal += strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                Else
                    strVal += Space(10) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                End If
            Next

            strBuf = txtCmt2.Text.Split(Chr(13))
            strVal += "  백혈구: "
            For intIdx As Integer = 0 To strBuf.Length - 1
                If intIdx = 0 Then
                    strVal += strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                Else
                    strVal += Space(10) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                End If
            Next

            strBuf = txtCmt3.Text.Split(Chr(13))
            strVal += "  혈소판: "
            For intIdx As Integer = 0 To strBuf.Length - 1
                If intIdx = 0 Then
                    strVal += strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                Else
                    strVal += Space(10) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                End If
            Next

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            strVal += "  골수천자도말 관찰 소견" + Space(10) + ("Totol Count  " + txtTCnt.Text + " 세포").PadRight(30, " "c) + Space(5)
            strVal += ("M : E ratio  " + Me.txtM.Text + " : 1").PadLeft(20, " "c) + vbCrLf

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            strBuf = txtCmt1.Text.Split(Chr(13))

            With Me.spdRst1
                .Row = 2 : .Col = 2 : strVal += "  Blast".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 2 : .Col = 5 : strVal += "Pronormoblast".PadRight(24, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 2 : .Col = 8 : strVal += "Lymphocyte".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + vbCrLf

                .Row = 3 : .Col = 2 : strVal += "  Promyelocyte".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 3 : .Col = 5 : strVal += "Basophilic N.".PadRight(24, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 3 : .Col = 8 : strVal += "Plasma cell".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + vbCrLf

                .Row = 4 : .Col = 2 : strVal += "  Myelocyte".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 4 : .Col = 5 : strVal += "Ploychromatophilic N.".PadRight(24, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 4 : .Col = 8 : strVal += "Histiocyte".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + vbCrLf

                .Row = 5 : .Col = 2 : strVal += "  Metamyelocyte".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 5 : .Col = 5 : strVal += "Orthochromic N.".PadRight(24, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 5 : .Col = 8 : strVal += "Monocyte".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + vbCrLf

                .Row = 6 : .Col = 2 : strVal += "  Band form".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 6 : .Col = 5 : strVal += "Basophilic series".PadRight(24, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 6 : .Col = 8 : strVal += "Immature mono".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + vbCrLf

                .Row = 7 : .Col = 2 : strVal += "  Neutrophil".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 7 : .Col = 5 : strVal += "Eosinophilic series".PadRight(24, " "c) + .Text.PadLeft(10, " "c) + " %" + Space(2)
                .Row = 7 : .Col = 8 : strVal += "Immature cells".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + vbCrLf

                .Row = 8 : .Col = 8 : If .Text <> "" Then strVal += Space(68) + "Others".PadRight(16, " "c) + .Text.PadLeft(10, " "c) + " %" + vbCrLf
            End With

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            strBuf = txtComment.Text.Split(Chr(13))
            For intIdx As Integer = 0 To strBuf.Length - 1
                strVal += Space(4) + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
            Next

            strVal += Space(4) + "● BMB - " + txtBMB.Text + vbCrLf

            strVal += "-".PadLeft(100, "-"c) + vbCrLf

            With spdRst2
                .Row = 2 : .Col = 2 : strVal += "  골수 저장철 평가  " + .Text + vbCrLf
                .Row = 3 : .Col = 2 : strBuf = .Text.Split(Chr(13))
                For intIdx As Integer = 0 To strBuf.Length - 1
                    If intIdx = 0 Then
                        strVal += "  Chromosome/FLSH   " + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                    Else
                        strVal += "                    " + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                    End If
                Next

                strVal += "-".PadLeft(100, "-"c) + vbCrLf

                .Row = 5 : .Col = 2 : strVal += "  Clot section      " + .Text + vbCrLf
                .Row = 7 : .Col = 2 : strVal += "  Surface Marker    " + .Text + vbCrLf

                strVal += "-".PadLeft(100, "-"c) + vbCrLf

                .Row = 9 : .Col = 3 : strVal += "  세포화학염색      " + "MPO " + .Text.PadRight(10, " "c)
                .Row = 9 : .Col = 5 : strVal += "SBB " + .Text.PadRight(10, " "c)
                .Row = 9 : .Col = 7 : strVal += "PAS " + .Text.PadRight(10, " "c)
                .Row = 9 : .Col = 9 : strVal += "ANAE " + .Text.PadRight(10, " "c)

                strVal += "-".PadLeft(100, "-"c) + vbCrLf

                .Row = 11 : .Col = 3 : strVal += "  의견 정리         " : strBuf = .Text.Split(Chr(13))
                For intIdx As Integer = 0 To strBuf.Length - 1
                    If intIdx = 0 Then
                        strVal += "1) " + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                    Else
                        strVal += Space(20) + "   " + strBuf(intIdx).Replace(vbLf, "") + vbCrLf
                    End If
                Next

                .Row = 12 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "2) " + .Text + vbCrLf
                .Row = 13 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "3) " + .Text + vbCrLf
                .Row = 14 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "4) " + .Text + vbCrLf
                .Row = 15 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "5) " + .Text + vbCrLf
                .Row = 16 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "6) " + .Text + vbCrLf
                .Row = 17 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "7) " + .Text + vbCrLf
                .Row = 18 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "8) " + .Text + vbCrLf

                strVal += "-".PadLeft(100, "-"c) + vbCrLf

                .Row = 20 : .Col = 3 : strVal += "  혈액학적 진단     " + "1. " + .Text.PadRight(60, " "c)
                .Row = 20 : .Col = 10 : If .Text <> "" Then strVal += "| " + .Text + vbCrLf

                .Row = 21 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "2. " + .Text.PadRight(60, " "c)
                .Row = 20 : .Col = 10 : If .Text <> "" Then strVal += "| " + .Text + vbCrLf

                .Row = 22 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "3. " + .Text.PadRight(60, " "c)
                .Row = 22 : .Col = 10 : If .Text <> "" Then strVal += "| " + .Text + vbCrLf

                .Row = 23 : .Col = 3 : If .Text <> "" Then strVal += Space(20) + "4. " + .Text.PadRight(60, " "c)
                .Row = 24 : .Col = 10 : If .Text <> "" Then strVal += "| " + .Text + vbCrLf

            End With


        Catch ex As Exception

        Finally
            fnGet_Report = strVal
        End Try

    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        msResult = ""
        mbSave = False
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim strRst As String = ""

        strRst += Me.txtWBC.Text + "|" + Me.txtHb.Text + "|" + Me.txtPLT.Text + "|" + Me.txtReti.Text + "|"
        strRst += Me.txtCmt1.Text + "|" + Me.txtCmt2.Text + "|" + Me.txtCmt3.Text + "|"
        strRst += Me.txtTCnt.Text + "|" + Me.txtM.Text + "|"

        With Me.spdRst1
            For intIdx As Integer = 2 To 7
                .Row = intIdx : .Col = 2 : strRst += .Text + "|"
            Next

            For intIdx As Integer = 2 To 7
                .Row = intIdx : .Col = 5 : strRst += .Text + "|"
            Next

            For intIdx As Integer = 2 To 8
                .Row = intIdx : .Col = 8 : strRst += .Text + "|"
            Next
        End With

        strRst += Me.txtComment.Text + "|"
        strRst += Me.txtBMB.Text + "|"

        With Me.spdRst2
            .Row = 2 : .Col = 2 : strRst += .Text + "|"
            .Row = 3 : .Col = 2 : strRst += .Text + "|"

            .Row = 5 : .Col = 2 : strRst += .Text + "|"

            .Row = 7 : .Col = 2 : strRst += .Text + "|"

            .Row = 9 : .Col = 3 : strRst += .Text + "|"
            .Row = 9 : .Col = 5 : strRst += .Text + "|"
            .Row = 9 : .Col = 7 : strRst += .Text + "|"
            .Row = 9 : .Col = 9 : strRst += .Text + "|"

            .Row = 11 : .Col = 3 : strRst += .Text + "|"
            .Row = 12 : .Col = 3 : strRst += .Text + "|"
            .Row = 13 : .Col = 3 : strRst += .Text + "|"
            .Row = 14 : .Col = 3 : strRst += .Text + "|"
            .Row = 15 : .Col = 3 : strRst += .Text + "|"
            .Row = 16 : .Col = 3 : strRst += .Text + "|"
            .Row = 17 : .Col = 3 : strRst += .Text + "|"
            .Row = 18 : .Col = 3 : strRst += .Text + "|"

            .Row = 20 : .Col = 3 : strRst += .Text + "|" : .Row = 20 : .Col = 10 : strRst += .Text + "|"
            .Row = 21 : .Col = 3 : strRst += .Text + "|" : .Row = 21 : .Col = 10 : strRst += .Text + "|"
            .Row = 22 : .Col = 3 : strRst += .Text + "|" : .Row = 22 : .Col = 10 : strRst += .Text + "|"
            .Row = 23 : .Col = 3 : strRst += .Text + "|" : .Row = 23 : .Col = 10 : strRst += .Text + "|"
        End With

        If DA_ST_BM.fnExe_LRS11M(msBcNo, msTClsCd, strRst, m_dbcn) Then
            'sbPrint_Data(strRst.Split("|"c))
            msResult = fnGet_Report()
            mbSave = True
            Me.Close()
        Else
            mbSave = False
        End If

    End Sub

    Private Sub txtWBC_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWBC.GotFocus, txtHb.GotFocus, txtPLT.GotFocus, txtReti.GotFocus, txtCmt1.GotFocus, txtCmt2.GotFocus, txtCmt3.GotFocus, txtComment.GotFocus, txtTCnt.GotFocus, txtM.GotFocus, txtBMB.GotFocus

        CType(sender, Windows.Forms.TextBox).SelectAll()

    End Sub

    Private Sub txtWBC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWBC.KeyDown, txtHb.KeyDown, txtPLT.KeyDown, txtReti.KeyDown, txtTCnt.KeyDown, txtM.KeyDown, txtBMB.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Dim strRstCont As String = DA_ST_BM.fnGet_CodeToRst(msTClsCd, CType(sender, System.Windows.Forms.TextBox).Text, m_dbcn)
        If strRstCont <> "" Then CType(sender, System.Windows.Forms.TextBox).Text = strRstCont

        If CType(sender, Windows.Forms.TextBox).Name = "txtM" Then
            With spdRst1
                .Row = 2 : .Col = 2 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                .Focus()
            End With
        ElseIf CType(sender, Windows.Forms.TextBox).Name = "txtBMB" Then
            With spdRst2
                .Row = 2 : .Col = 2 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                .Focus()
            End With
        Else
            SendKeys.Send("{TAB}")
        End If

    End Sub

    Private Sub spdRst1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdRst1.GotFocus, spdRst2.GotFocus
        'With CType(sender, AxFPSpreadADO.AxfpSpread)
        '    .Row = 2 : .Col = 2 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
        'End With
    End Sub

    Private Sub spdRst1_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdRst1.KeyDownEvent
        If e.keyCode <> Keys.Enter Then Return

        With spdRst1
            .Row = .ActiveRow
            .Col = .ActiveCol : Dim strRstCont As String = DA_ST_BM.fnGet_CodeToRst(msTClsCd, .Text, m_dbcn)
            If strRstCont <> "" Then .Text = strRstCont

            If .ActiveCol = 2 And .ActiveRow = 7 Then
                .Row = 1 : .Col = 5 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            ElseIf .ActiveCol = 5 And .ActiveRow = 7 Then
                .Row = 1 : .Col = 8 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            ElseIf .ActiveCol = 8 And .ActiveRow = 8 Then
                txtComment.Focus()
            End If
        End With
    End Sub

    Private Sub spdRst2_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdRst2.KeyDownEvent
        If e.keyCode <> Keys.Tab Then Return

        With spdRst2
            If .ActiveCol >= 2 And .ActiveRow = 3 Then
                .Row = 5 : .Col = 2 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            ElseIf .ActiveCol >= 3 And .ActiveRow = 11 Then
                .Row = 12 : .Col = 3 : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
            End If
        End With

    End Sub
End Class


Public Class DA_ST_BM
    Private Const msFile As String = "File : FGPOPUPST_BM.vb, Class : DA_ST_BM" & vbTab

    Public Shared Function fnGet_CodeToRst(ByVal rsTclsCd As String, ByVal rsKeyPad As String, ByVal r_DbCn As oracleConnection) As String
        Dim dbcn As OracleConnection = r_DbCn
        If r_DbCn Is Nothing Then dbcn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Dim dt As New DataTable

        Dim sSql As String = ""
        Try
            sSql = ""
            sSql += "SELECT rstcont FROM lf083m WHERE testcd = :testcd AND spccd = :spccd AND keypad = :keypad"

            dbCmd.Connection = dbcn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                .SelectCommand.Parameters.Add("spccd", OracleDbType.Varchar2).Value = "".PadRight(PRG_CONST.Len_SpcCd, "0"c)
                .SelectCommand.Parameters.Add("keypad", OracleDbType.Varchar2).Value = rsKeyPad
            End With

            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_DbCn Is Nothing Then
                If dbcn.State = ConnectionState.Open Then dbcn.Close()
                dbcn.Dispose() : dbcn = Nothing
            End If
        End Try

    End Function


    Public Shared Function fnGet_Rst_WBCHbPLT(ByVal rsBcNo As String, ByVal r_dbCn As OracleConnection) As String

        Dim dbCn As OracleConnection = r_DbCn
        If r_DbCn Is Nothing Then dbCn = GetDbConnection()

        Dim dbCmd As New OracleCommand

        Dim sSql As String = ""
        Try
            sSql = ""
            sSql += "SELECT fn_ack_get_refrst_bm(:bcno) FROM DUAL"

            dbCmd.Connection = dbCn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return "|||"
            End If
        Catch ex As Exception
            Return "|||"
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If
        End Try

    End Function

    Public Shared Function fnGet_Rst_Bm(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal r_DbCn As oracleConnection) As String


        Dim dbcn As OracleConnection = r_DbCn
        If r_DbCn Is Nothing Then dbcn = GetDbConnection()
        Dim dbCmd As New OracleCommand

        Dim sSql As String = ""
        Try
            sSql = ""
            sSql += "select rsttxt from lrs11m a, lj010m j"
            sSql += " where a.bcno    = :bcno"
            sSql += "   and a.tclscd  = :tclscd"
            sSql += "   and a.bcno    = j.bcno"
            sSql += "   and j.spcflag = '2'"

            dbCmd.Connection = dbcn
            dbCmd.CommandType = CommandType.Text
            dbCmd.CommandText = sSql

            Dim dbDa As OracleDataAdapter
            dbDa = New OracleDataAdapter(dbCmd)

            With dbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .SelectCommand.Parameters.Add("tclscd", OracleDbType.Varchar2).Value = rsTclsCd
            End With

            Dim dt As New DataTable
            dt.Reset()
            dbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            If r_DbCn Is Nothing Then
                If dbcn.State = ConnectionState.Open Then dbcn.Close()
                dbcn.Dispose() : dbcn = Nothing
            End If
        End Try


    End Function

    Public Shared Function fnExe_LRS11M(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsRst As String, ByVal r_dbCn As OracleConnection) As Boolean
        Dim sFn As String = "fnExe_LRS11M"
        Dim dbCn As OracleConnection = r_dbCn
        If dbCn Is Nothing Then dbCn = GetDbConnection()

        Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
        Dim dbCmd As New OracleCommand

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sqlDoc As String = ""
            Dim intRet As Integer = 0

            With dbCmd
                .Connection = dbCn
                .Transaction = dbTran
                .CommandType = CommandType.Text
                .CommandText = "delete lrs11m where bcno = :bcno and testcd = :testcd"

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd

                .ExecuteNonQuery()

                sqlDoc = ""
                sqlDoc += "insert into lrs11m(  bcno,  testcd,  rsttxt, rstdt )"
                sqlDoc += "            values( :bcno, :testcd, :rsttxt, fn_ack_sysdate)"

                .CommandText = sqlDoc

                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                .Parameters.Add("rsttxt", OracleDbType.Varchar2).Value = rsRst

                intRet = .ExecuteNonQuery()
            End With

            If intRet = 0 Then
                dbTran.Rollback()
            Else
                dbTran.Commit()
            End If

            Return True

        Catch ex As Exception
            dbTran.Rollback()
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        Finally
            dbCmd.Dispose() : dbCmd = Nothing
            dbTran.Dispose() : dbTran = Nothing
            If r_dbCn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing
            End If

            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function
End Class


Public Class PRT_ST_BM
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