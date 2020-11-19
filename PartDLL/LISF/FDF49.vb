'>>> [49] 종합검증 검사항목 소견설정
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst


Public Class FDF49
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF11.vb, Class : FDF49" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2
    Private msCDSep As String = ""

    Private mobjDAF As New LISAPP.APP_F_VCMT_TCLS

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Private Sub sbDisplay_Part()
        Dim sFn As String = "Sub sbDisplay_Part()"
        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Part_List()

            cboPartCd.Items.Clear()
            cboPartCd.Items.Add("[ ] 전체")
            For intIdx As Integer = 0 To dt.Rows.Count - 1
                cboPartCd.Items.Add("[" + dt.Rows(intIdx).Item("partcd").ToString + "] " + dt.Rows(intIdx).Item("partnmd").ToString)
            Next

            If cboPartCd.Items.Count > 0 Then cboPartCd.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Function fnCollectItemTable_32(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_32(String) As LISAPP.ItemTableCollection"

        Try
            Dim it32 As New LISAPP.ItemTableCollection

            With spdItem

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("testcd") : Dim strTclsCd As String = .Text
                    .Col = .GetColFromID("cdseqnl") : Dim strCdSeqNL As String = .Text
                    .Col = .GetColFromID("cdseqnh") : Dim strCdSeqNH As String = .Text
                    .Col = .GetColFromID("cdseqp") : Dim strCdSeqP As String = .Text
                    .Col = .GetColFromID("cdseqd") : Dim strCdSeqD As String = .Text
                    .Col = .GetColFromID("cdseqcl") : Dim strCdSeqCl As String = .Text
                    .Col = .GetColFromID("cdseqch") : Dim strCdSeqCH As String = .Text
                    .Col = .GetColFromID("reflt") : Dim strRefLT As String = .Text
                    .Col = .GetColFromID("reflts") : Dim strRefLTs As String = Ctrl.Get_Code(.Text)
                    .Col = .GetColFromID("cdseqt") : Dim strCdSeqT As String = .Text

                    If strTclsCd = "" Then Exit For

                    it32.SetItemTable("TESTCD", 1, intRow, strTclsCd)
                    it32.SetItemTable("CDSEQNL", 2, intRow, strCdSeqNL)
                    it32.SetItemTable("CDSEQNH", 3, intRow, strCdSeqNH)
                    it32.SetItemTable("CDSEQP", 4, intRow, strCdSeqP)
                    it32.SetItemTable("CDSEQD", 5, intRow, strCdSeqD)
                    it32.SetItemTable("CDSEQCL", 6, intRow, strCdSeqCl)
                    it32.SetItemTable("CDSEQCH", 7, intRow, strCdSeqCH)
                    it32.SetItemTable("REFLT", 8, intRow, strRefLT)
                    it32.SetItemTable("REFLTS", 9, intRow, strRefLTs)
                    it32.SetItemTable("CDSEQT", 10, intRow, strCdSeqT)
                    it32.SetItemTable("REGDT", 11, intRow, rsRegDT)
                    it32.SetItemTable("REGID", 12, intRow, USER_INFO.USRID)
                    it32.SetItemTable("REGIP", 13, intRow, USER_INFO.LOCALIP)

                Next

            End With

            fnCollectItemTable_32 = it32
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it32 As New LISAPP.ItemTableCollection
            Dim iRegType32 As Integer = 0
            Dim sRegDT As String

            iRegType32 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it32 = fnCollectItemTable_32(sRegDT)

            If mobjDAF.TransVCmtTclsInfo(it32, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsRstDt As String) As String
        Dim sFn As String = ""

        Try

            Return ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return "Error"
        End Try
    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim dt As DataTable = mobjDAF.GetNewRegDT

            If dt.Rows.Count > 0 Then
                fnGetSystemDT = dt.Rows(0).Item(0).ToString
            Else
                MsgBox("시스템의 날짜를 초기화하지 못했습니다. 관리자에게 문의하시기 바랍니다!!", MsgBoxStyle.Information)
                Return Format(Now, "yyyyMMddHHmmss")
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")
        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtRegDT.Text)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail()
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_VCmt_Tcls()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_VCmt_Tcls()
        Dim sFn As String = "Private Sub sbDisplayCdDetail_VCmt_Tcls()"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetVCmtTclsInfo()
            Else
                dt = mobjDAF.GetVCmtTclsInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID)
            End If


            sbInitialize()

            Ctrl.FindChildControl(Me.Controls, mchildctrlcol)

            spdItem.MaxRows = 0
            If dt.Rows.Count > 0 Then
                Me.txtRegDT.Text = dt.Rows(0).Item("regdt").ToString()
                Me.txtRegNm.Text = dt.Rows(0).Item("regnm").ToString()

                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    With spdItem
                        .MaxRows += 1
                        .Row = .MaxRows

                        .Col = .GetColFromID("testcd") : .Text = dt.Rows(intIdx).Item("testcd").ToString
                        .Col = .GetColFromID("tnmd") : .Text = dt.Rows(intIdx).Item("tnmd").ToString
                        .Col = .GetColFromID("cdseqnl") : .Text = dt.Rows(intIdx).Item("cdseqnl").ToString
                        .Col = .GetColFromID("cdseqnh") : .Text = dt.Rows(intIdx).Item("cdseqnh").ToString
                        .Col = .GetColFromID("cdseqp") : .Text = dt.Rows(intIdx).Item("cdseqp").ToString
                        .Col = .GetColFromID("cdseqd") : .Text = dt.Rows(intIdx).Item("cdseqd").ToString
                        .Col = .GetColFromID("cdseqcl") : .Text = dt.Rows(intIdx).Item("cdseqcl").ToString
                        .Col = .GetColFromID("cdseqch") : .Text = dt.Rows(intIdx).Item("cdseqch").ToString
                        .Col = .GetColFromID("reflt") : .Text = dt.Rows(intIdx).Item("reflt").ToString


                        If dt.Rows(intIdx).Item("reflts").ToString.Trim = "" Or dt.Rows(intIdx).Item("reflts").ToString.Trim = "" Then
                            .Col = .GetColFromID("reflts") : .TypeComboBoxCurSel = -1
                        Else
                            .Col = .GetColFromID("reflts") : .TypeComboBoxCurSel = Convert.ToInt16(dt.Rows(intIdx).Item("reflts").ToString)
                        End If
                        .Col = .GetColFromID("cdseqt") : .Text = dt.Rows(intIdx).Item("cdseqt").ToString
                    End With
                Next

            Else
                Exit Sub
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then      '권한이 있어야 "사용종료"를 할 수 있음
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

            miSelectKey = 1

            sbInitialize_ErrProvider()

            sbInitialize_Control()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn As String = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_Control(Optional ByVal iMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If iMode = 0 Then

                Me.txtTestCd.Text = "" : Me.btnUE.Visible = False

                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtRegNm.Text = ""

            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_CtrlCollection()
        mchildctrlcol = Nothing

        mchildctrlcol = New Collection
    End Sub

    Private Sub sbHelp_Spread(ByVal riCol As Integer, ByVal riRow As Integer)
        Try

            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim strValue As String

            With spdItem
                .Row = riRow : .Col = riCol : strValue = .Text
            End With

            objHelp.FormText = "종합검증 소견 정보"
            objHelp.TableNm = "lf320m"

            If strValue = "" Then
                objHelp.Where = "CDSEP = 'CMT'"
            Else
                If strValue.IndexOf("?") < 0 Then strValue += "%"
                objHelp.Where = "CDSEP = 'CMT' and " + _
                                "CDSEQ like '" + strValue.Replace("?", "%").ToUpper + "'"
            End If


            objHelp.OrderBy = "CDSEQ"
            objHelp.MaxRows = 20
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("CDSEQ", "코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("CDCONT", "내용", 50, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("CDTITLE", "제목", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(spdItem)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y)

            With spdItem
                .Row = riRow : .Col = riCol
                If aryList.Count > 0 Then
                    .Text = aryList.Item(0).ToString.Split("|"c)(0)
                Else
                    .Text = ""
                End If
            End With
        Catch ex As Exception

        Finally
        End Try

    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        'If txtTclsCd.Text = "" Then Exit Sub

        Try
            Dim sMsg As String = "종합정보 검사항목 소견설정를 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransVCmtTclsInfo_UE(USER_INFO.USRID) Then
                MsgBox("종합정보 검사항목 소견설정이 사용종료 되었습니다!!", MsgBoxStyle.Information)

                sbInitialize()
                CType(Me.Owner, FGF01).sbDeleteCdList()
            Else
                MsgBox("사용종료에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsCdSep As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        msCDSep = rsCdSep

    End Sub

    Private Sub FDF49_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FDF49_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        sbDisplay_Part()
        spdItem.MaxRows = 0

    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_Test.Click

        Try
            Dim strTcls As String = txtTestCd.Text
            Dim strPartCd As String = Ctrl.Get_Code(cboPartCd)

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_list(Ctrl.Get_Code(Me.cboPartCd), "", "", Me.txtTestCd.Text)
            Dim sSql As String = "((tcdgbn IN ('B', 'P') AND titleyn = '1') OR tcdgbn IN ('S', 'C'))"
            Dim a_dr As DataRow() = dt.Select(sSql, "")
            dt = Fn.ChangeToDataTable(a_dr)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim strCodes As String = ""

            With spdItem
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("testcd") : strCodes += .Text + "|"
                Next
            End With

            objHelp.FormText = "검사코드 정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 20
            objHelp.KeyCodes = strCodes

            objHelp.AddField("'' chk", "", 3, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, "CHECKBOX", , "CHK")
            objHelp.AddField("testcd", "검사코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(txtTestCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y, dt)

            If alList.Count > 0 Then
                For intIdx As Integer = 0 To alList.Count - 1

                    With spdItem
                        Dim blnFind As Boolean = False
                        For intRow As Integer = 1 To .MaxRows
                            .Row = intRow
                            .Col = .GetColFromID("testcd")
                            If .Text = alList.Item(intIdx).ToString.Split("|"c)(0) Then
                                blnFind = True
                                Exit For
                            End If
                        Next

                        If blnFind = False Then
                            .MaxRows += 1
                            .Row = .MaxRows
                            .Col = .GetColFromID("testcd") : .Text = alList.Item(intIdx).ToString.Split("|"c)(0)
                            .Col = .GetColFromID("tnmd") : .Text = alList.Item(intIdx).ToString.Split("|"c)(1)
                        End If
                    End With
                Next
            End If

            txtTestCd.Text = ""

        Catch ex As Exception

        Finally
        End Try

    End Sub

    Private Sub txtTclsCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If txtTestCd.Text = "" Then Return

        Try
            btnCdHelp_test_Click(Nothing, Nothing)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub spdItem_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdItem.DblClick

        If e.col = spdItem.GetColFromID("cdseqt") Or e.col > spdItem.GetColFromID("tnmd") Then Return

        Dim strTnmd As String = ""

        With spdItem
            .Row = .ActiveRow
            .Col = .GetColFromID("tnmd") : strTnmd = .Text

            If strTnmd <> "" Then
                If MsgBox("검사항목 [" + strTnmd + "]를(을) 삭제하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    .Row = .ActiveRow
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1
                End If
            End If
        End With

    End Sub

    Private Sub spdItem_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdItem.KeyDownEvent

        If e.keyCode <> Keys.Enter Then Return
        If spdItem.ActiveRow < 1 Then Return

        With spdItem
            .Col = .ActiveCol
            If .ColID.IndexOf("seq") >= 0 Then
                sbHelp_Spread(spdItem.ActiveCol, spdItem.ActiveRow)
            End If
        End With

    End Sub

    Private Sub mnuHelp_cmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelp_cmt.Click
        Try

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_CmtList_GV()

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "종합소견 정보"
            objHelp.MaxRows = 20

            objHelp.AddField("cdseq", "코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("cdcont", "내용", 100, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(txtTestCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y, dt)

            If alList.Count > 0 Then
                Dim sCont As String = alList.Item(0).ToString.Split("|"c)(0)

                With spdItem
                    .Row = .ActiveRow
                    .Col = .ActiveCol : .Text = sCont
                End With
            End If

        Catch ex As Exception

        Finally
        End Try
    End Sub
End Class