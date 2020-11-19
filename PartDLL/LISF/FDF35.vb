'>>> [35] 혈액은행 관련검사 설정

Imports COMMON.CommFN
Imports COMMON.CommStu
Imports COMMON.commlogin.login

Imports POPUPWIN
Imports COMMON.CommConst

Public Class FDF35
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF35.vb, Class : FDF35" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_BLD_REF

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Public gsModDT As String = ""
    Public gsModID As String = ""

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboTSectNmD As System.Windows.Forms.ComboBox
    Friend WithEvents txtTSectCd As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxRow As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMaxCol As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdSpc As AxFPSpreadADO.AxfpSpread

    Public Sub sbDisplayCdDetail()
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            Call sbDisplayCdDetail_slip()
            Call sbDisplayCdDetail_Test()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_slip()

        Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List

        cboSlip.Items.Clear()

        dt = LISAPP.COMM.cdfn.fnGet_Slip_List
        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next
        End If

        If cboSlip.Items.Count > 0 Then cboSlip.SelectedIndex = 0

    End Sub
    Private Sub sbDisplayCdDetail_Test()
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Calc_RST()"
        Dim iCol As Integer = 0

        Try
            Dim dt As DataTable
            Dim cctrl As System.Windows.Forms.Control = Nothing
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetBldRefInfo()
            Else
                dt = mobjDAF.GetBldRefInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""))
            End If

            '초기화
            sbInitialize()

            If dt.Rows.Count < 1 Then Return

            miSelectKey = 1

            With dt
                Me.spdTestList.MaxRows = 0

                Me.txtRegDT.Text = .Rows(0).Item("regdt").ToString()
                Me.txtRegID.Text = .Rows(0).Item("regid").ToString()
                Me.txtRegNm.Text = .Rows(0).Item("regnm").ToString()
            End With

            With spdTestList
                .MaxRows = dt.Rows.Count

                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    .Row = intIdx + 1
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(intIdx).Item("testcd").ToString
                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(intIdx).Item("spccd").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(intIdx).Item("tnmd").ToString
                    .Col = .GetColFromID("dispseq") : .Text = dt.Rows(intIdx).Item("dispseq").ToString
                    .Col = .GetColFromID("tordgbn") : .Text = dt.Rows(intIdx).Item("tordgbn").ToString
                    .Col = .GetColFromID("dordgbn") : .Text = dt.Rows(intIdx).Item("dordgbn").ToString
                    .Col = .GetColFromID("aordgbn") : .Text = dt.Rows(intIdx).Item("aordgbn").ToString
                    .Col = .GetColFromID("trstgbn") : .Text = dt.Rows(intIdx).Item("trstgbn").ToString
                    .Col = .GetColFromID("drstgbn") : .Text = dt.Rows(intIdx).Item("drstgbn").ToString
                    .Col = .GetColFromID("arstgbn") : .Text = dt.Rows(intIdx).Item("arstgbn").ToString

                    Select Case dt.Rows(intIdx).Item("bbgbn").ToString
                        Case "1" : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 1
                        Case "2" : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 2
                        Case "3" : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 3
                        Case "6" : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 4
                        Case "7" : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 5
                        Case "9" : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 6
                        Case "A" : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 7
                        Case "B" : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 8
                        Case Else : .Col = .GetColFromID("bbgbn") : .TypeComboBoxCurSel = 0
                    End Select

                Next
            End With

            Me.txtModDT.Text = gsModDT
            Me.txtModNm.Text = gsModID

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
                Me.spdTestList.MaxRows = 0
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModNm.Text = "" : Me.txtRegNm.Text = ""
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

            If spdTestList.MaxRows = 0 Then
                MsgBox("관련검사를 선택하세요!!", MsgBoxStyle.Critical)
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
            Dim it14 As New LISAPP.ItemTableCollection
            Dim iRegType14 As Integer = 0
            Dim sRegDT As String

            iRegType14 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it14 = fnCollectItemTable_14(sRegDT)
            If it14.ItemTables.Count < 1 Then
                fnReg = False
                Exit Function
            End If

            If mobjDAF.TransBldRefInfo(it14, iRegType14, USER_INFO.USRID) Then
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

    Private Function fnCollectItemTable_14(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_14(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it14 As New LISAPP.ItemTableCollection

            With it14
                For iRow As Integer = 1 To spdTestList.MaxRows
                    Dim sTestCd As String = "", sSpcCd As String = "", sDispSeq As String = "", sBBGbn As String = "0"
                    Dim sDOrdGbn As String = "0", sTOrdGbn As String = "0", sAOrdGbn As String = "0"
                    Dim sDRstGbn As String = "0", sTRstGbn As String = "0", sARstGbn As String = "0"

                    spdTestList.Row = iRow
                    spdTestList.Col = spdTestList.GetColFromID("testcd") : sTestCd = spdTestList.Text
                    spdTestList.Col = spdTestList.GetColFromID("spccd") : sSpcCd = spdTestList.Text
                    spdTestList.Col = spdTestList.GetColFromID("bbgbn") : sBBGbn = spdTestList.Text
                    spdTestList.Col = spdTestList.GetColFromID("tordgbn") : sTOrdGbn = spdTestList.Text
                    spdTestList.Col = spdTestList.GetColFromID("dordgbn") : sDOrdGbn = spdTestList.Text
                    spdTestList.Col = spdTestList.GetColFromID("aordgbn") : sAOrdGbn = spdTestList.Text
                    spdTestList.Col = spdTestList.GetColFromID("trstgbn") : sTRstGbn = spdTestList.Text
                    spdTestList.Col = spdTestList.GetColFromID("drstgbn") : sDRstGbn = spdTestList.Text
                    spdTestList.Col = spdTestList.GetColFromID("arstgbn") : sARstGbn = spdTestList.Text

                    If sBBGbn <> "" Then
                        sBBGbn = Ctrl.Get_Code(sBBGbn)
                    Else
                        sBBGbn = "0"
                    End If

                    spdTestList.Col = spdTestList.GetColFromID("dispseq") : sDispSeq = spdTestList.Text

                    .SetItemTable("TESTCD", 1, iRow, sTestCd)
                    .SetItemTable("SPCCD", 2, iRow, sSpcCd)
                    .SetItemTable("BBGBN", 3, iRow, sBBGbn)
                    .SetItemTable("TORDGBN", 4, iRow, sTOrdGbn)
                    .SetItemTable("TRSTGBN", 5, iRow, sTRstGbn)
                    .SetItemTable("DORDGBN", 6, iRow, sDOrdGbn)
                    .SetItemTable("DRSTGBN", 7, iRow, sDRstGbn)
                    .SetItemTable("AORDGBN", 8, iRow, sAOrdGbn)
                    .SetItemTable("ARSTGBN", 9, iRow, sARstGbn)
                    .SetItemTable("DISPSEQ", 10, iRow, sDispSeq)
                    .SetItemTable("REGDT", 11, iRow, rsRegDT)
                    .SetItemTable("REGID", 12, iRow, USER_INFO.USRID)
                    .SetItemTable("REGIP", 13, iRow, USER_INFO.LOCALIP)
                Next
            End With

            fnCollectItemTable_14 = it14

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing

        End Try
    End Function


    Private Sub btnAddTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSlip.Click
        Dim sFn As String = "btnAddTest_Click"

        Try
            Dim sTSectCd_cbo As String = Ctrl.Get_Code(Me.cboSlip)
            Dim sTSectCd_spd As String = ""

            Dim iHeight As Integer = Convert.ToInt32(spdTestList.Height)
            Dim iWidth As Integer = Convert.ToInt32(spdTestList.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Me.Top + Me.btnAddSlip.Top + Me.btnAddSlip.Height + Ctrl.menuHeight

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = CType(Me.Owner, FGF01).Width + Me.Left + Me.btnAddSlip.Left

            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - Me.btnAddSlip.Width)


            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_testspc_list(Ctrl.Get_Code(cboSlip), "")
            Dim a_dr As DataRow() = dt.Select("tcdgbn IN ('S', 'P', 'C', 'B')", "")
            dt = Fn.ChangeToDataTable(a_dr)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "검사정보"
            objHelp.MaxRows = 15

            objHelp.AddField("chk", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "구분", 0, , , True)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                For intIdx As Integer = 0 To alList.Count - 1
                    Dim strBuf() As String = alList.Item(intIdx).ToString.Split("|"c)

                    With spdTestList
                        Dim blnFind As Boolean = False
                        For intRow As Integer = 1 To spdTestList.MaxRows
                            Dim strTclsCd As String = "", strSpcCd As String = ""
                            .Row = intRow
                            .Col = .GetColFromID("testcd") : strTclsCd = .Text
                            .Col = .GetColFromID("spccd") : strSpcCd = .Text

                            If strTclsCd = strBuf(0) And strSpcCd = strBuf(1) Then
                                blnFind = True
                                Exit For
                            End If
                        Next

                        If blnFind = False Then
                            .MaxRows += 1
                            .Row = .MaxRows
                            .Col = .GetColFromID("testcd") : .Text = strBuf(0)
                            .Col = .GetColFromID("spccd") : .Text = strBuf(1)
                            .Col = .GetColFromID("tnmd") : .Text = strBuf(2)

                        End If
                    End With
                Next

            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Sub

    Private Sub spdTclsList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdTestList.DblClick

        Dim sMsg As String = ""

        With spdTestList

            .Col = .GetColFromID("tnmd")
            .Row = spdTestList.ActiveRow
            Dim tnmd As String = .Text
            sMsg = tnmd + " : 검사를 제외하겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1
            End If



        End With

    End Sub

    Private Sub FDF35_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    
    Private Sub FDF35_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
    End Sub
End Class