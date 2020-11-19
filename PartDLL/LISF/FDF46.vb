'>>> [46] 성분제제 설정
Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class FDF46
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF46.vb, Class : FDF46" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0
    Private miAddModeKey As Integer = 0

    Private mobjDAF As New LISAPP.APP_F_DCOMCD

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Private Sub sbDisplay_Slip()

        Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()

        cboSlip.Items.Clear()
        cboSlip.Items.Add("[00] 공통")

        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next
        End If

        cboSlip.SelectedIndex = 0
    End Sub

    Private Sub sbDisplay_SungBunCd()
        Dim dt As DataTable = mobjDAF.GetSangBunInfo()

        spdList.MaxRows = dt.Rows.Count
        If dt.Rows.Count = 0 Then Return

        For ix As Integer = 0 To dt.Rows.Count - 1
            With spdList
                .Row = ix + 1
                .Col = .GetColFromID("cd") : .Text = ix.ToString("D4") 'dt.Rows(ix).Item("sungbun_code").ToString
                .Col = .GetColFromID("nm") : .Text = dt.Rows(ix).Item("sungbun_name").ToString
            End With
        Next

    End Sub

    Public Sub sbDisplayCdDetail(ByVal rsSlipCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            Call sbDisplayCdDetail_DComCd(rsSlipCd)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_DComCd(ByVal rsSlipCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_DComCd(String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control = Nothing
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetDcomCdInfo(rsSlipCd)
            Else
                dt = mobjDAF.GetDcomCdInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsSlipCd)
            End If

            '초기화
            sbInitialize()

            If dt.Rows.Count < 1 Then Return

            miSelectKey = 1

            Me.txtRegDT.Text = dt.Rows(0).Item("regdt").ToString()
            Me.txtRegID.Text = dt.Rows(0).Item("regid").ToString()

            Me.txtModNm.Text = dt.Rows(0).Item("modnm").ToString()
            Me.txtRegNm.Text = dt.Rows(0).Item("regnm").ToString()

            Dim blnFind As Boolean = False

            ' 2010-12-21 이형택 검사분야 콤보 세팅 오류 수정
            Dim li_FIdx As Integer = 0

            For intIdx As Integer = 0 To cboSlip.Items.Count - 1
                cboSlip.SelectedIndex = intIdx

                If cboSlip.Text.IndexOf(rsSlipCd) > -1 Then
                    li_FIdx = intIdx
                    blnFind = True
                    Exit For
                End If

            Next

            If Not blnFind Then
                cboSlip.SelectedIndex = 0
            Else
                cboSlip.SelectedIndex = li_FIdx
            End If

            spdAddList.MaxRows = dt.Rows.Count

            For intIdx As Integer = 0 To dt.Rows.Count - 1
                With spdAddList
                    spdAddList.Row = intIdx + 1
                    .Col = .GetColFromID("cd") : .Text = dt.Rows(intIdx).Item("drugcomcd").ToString
                    .Col = .GetColFromID("nm") : .Text = dt.Rows(intIdx).Item("drugcomnm").ToString
                End With
            Next

            Me.txtModDT.Text = gsModDT
            Me.txtModID.Text = gsModID

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then
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
                Me.cboSlip.SelectedIndex = 0
                Me.spdAddList.MaxRows = 0

                With spdList
                    .Col = 1 : .Col2 = 1
                    .Row = 1 : .Row2 = .MaxRows
                    .BlockMode = True
                    .Action = FPSpreadADO.ActionConstants.ActionClearText
                    .BlockMode = False
                End With

                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtRegNm.Text = "" : Me.txtModNm.Text = ""
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            Dim intCnt As Integer = 0

            If spdAddList.MaxRows > 0 Then intCnt += 1

            If intCnt > 0 Then
            Else
                MsgBox("성분제제를 선택하세요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = ""

        Try
            Dim it43 As New LISAPP.ItemTableCollection
            Dim iRegType43 As Integer = 0
            Dim sRegDT As String

            iRegType43 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it43 = fnCollectItemTable_43(sRegDT)
            If it43.ItemTables.Count < 1 Then
                fnReg = False
                Exit Function
            End If

            If mobjDAF.TransDcomCdInfo(it43, iRegType43, cboSlip.Text.Substring(1, cboSlip.Text.IndexOf("]") - 1), USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            fnReg = False
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetNewRegDT

            If DTable.Rows.Count > 0 Then
                Return DTable.Rows(0).Item(0).ToString
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

    Private Function fnCollectItemTable_43(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_43(BString) As LISAPP.ItemTableCollection"

        Try
            Dim it43 As New LISAPP.ItemTableCollection

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAddList
            Dim strCalForm$ = ""
            Dim intCnt As Integer = 0

            With it43
                For intRow As Integer = 1 To spd.MaxRows

                    Dim strCd As String = "", strNm As String = ""

                    intCnt += 1

                    spd.Row = intRow
                    spd.Col = spd.GetColFromID("cd") : strCd = spd.Text
                    spd.Col = spd.GetColFromID("nm") : strNm = spd.Text

                    .SetItemTable("PARTCD", 1, intCnt, Ctrl.Get_Code(cboSlip).Substring(0, 1))
                    .SetItemTable("SLIPCD", 2, intCnt, Ctrl.Get_Code(cboSlip).Substring(1, 1))
                    .SetItemTable("DRUGCOMCD", 3, intCnt, strCd)
                    .SetItemTable("DRUGCOMNM", 4, intCnt, strNm)
                    .SetItemTable("REGDT", 5, intCnt, rsRegDT)
                    .SetItemTable("REGID", 6, intCnt, USER_INFO.USRID)
                    .SetItemTable("REGIP", 7, intCnt, USER_INFO.LOCALIP)
                Next
            End With

            fnCollectItemTable_43 = it43

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing

        End Try
    End Function

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        DS_FormDesige.sbInti(Me)

        sbDisplay_Slip()
        SBdISPLAY_sungbunCd()

        sbInitialize()

    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        For iRow As Integer = 1 To Me.spdList.MaxRows
            Dim sChk As String = "", sCode As String = "", sName As String = ""

            With spdList
                .Row = iRow
                .Col = .GetColFromID("chk") : sChk = .Text
                .Col = .GetColFromID("cd") : sCode = .Text
                .Col = .GetColFromID("nm") : sName = .Text
            End With

            If sChk = "1" Then
                With spdAddList
                    Dim blnFlag As Boolean = False
                    For ix As Integer = 1 To spdAddList.MaxRows
                        .Row = ix
                        .Col = .GetColFromID("nm")

                        If .Text = sName Then
                            blnFlag = True
                            Exit For
                        End If
                    Next

                    If blnFlag = False Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("cd") : .Text = sCode
                        .Col = .GetColFromID("nm") : .Text = sName
                    End If

                End With
            End If
        Next

    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click

        For iRow As Integer = 1 To Me.spdList.MaxRows
            Dim sChk As String = "", sCode As String = "", sName As String = ""

            With Me.spdAddList
                .Row = iRow
                .Col = .GetColFromID("chk") : sChk = .Text
                .Col = .GetColFromID("cd") : sCode = .Text
                .Col = .GetColFromID("nm") : sName = .Text

                If sChk = "1" Then
                    .Row = iRow
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1

                    iRow -= 1
                    If iRow < 0 Then Exit For
                End If
            End With
        Next

    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click"
        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If cboSlip.Text = "" Then Return

        Try

            Dim sMsg As String = "   파트 : " + Me.cboSlip.Text + vbCrLf
            sMsg += "   의 성분제제 설정을 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString
            sUeTime = CType(objFrm, FGF02).UETime.ToString

            If mobjDAF.TransDcomCdInfo_UE(Ctrl.Get_Code(Me.cboSlip), USER_INFO.USRID) Then
                MsgBox("해당 파트에 성분제제 설정이 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub FDF46_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub
End Class