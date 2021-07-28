'>>> 검체 전달 및 접수

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports common.commlogin.login

Imports LISAPP.APP_DB
Imports LISAPP.APP_J
Imports LISAPP.APP_J.TkFn

Public Class FGJ05
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGJ05.vb, Class : J01" & vbTab

    Declare Function waveOutGetNumDevs Lib "winmm.dll" () As Long
    Declare Function PlaySound Lib "winmm.dll" _
        Alias "PlaySoundA" (ByVal lpszName As String, _
        ByVal hModule As Long, ByVal dwFlags As Long) _
        As Long

    Public Const SND_APPLICATION As Long = &H80
    Public Const SND_ASYNC As Long = &H1
    Public Const SND_FILENAME As Long = &H20000
    Public Const SND_NODEFAULT As Long = &H2

    Public HasSound As Boolean
    Public msBcClsCd As String = ""
    Public mbLoad As Boolean = False

    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblBcclsNm3 As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm2 As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm1 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CButton1 As CButtonLib.CButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents lblBcColor1 As System.Windows.Forms.Label
    Friend WithEvents lblBcColor3 As System.Windows.Forms.Label
    Friend WithEvents lblBcColor2 As System.Windows.Forms.Label
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents txtPassId As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblPassNm As System.Windows.Forms.Label
    Friend WithEvents chkBarInit As System.Windows.Forms.CheckBox
    Public WithEvents lblBcColor0 As System.Windows.Forms.Label

#Region " Form내부 함수 "
    Private Sub sbPrint_BarCode(ByVal rsBcNo As String)

        Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(Me.Name)
        Dim alBcNo As New ArrayList

        Try
            Dim dt As DataTable = fnGet_Jubsu_BarCode_Info(rsBcNo, "J")

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(ix).Item("mbttype").ToString = "2" Then
                    objBCPrt.PrintDo_Micro(alBcNo, "1")
                Else
                    objBCPrt.PrintDo(alBcNo, "1")
                End If
            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub sbDisplay_Color_bccls()
        Dim sFn As String = "Private Sub sbDisplay_Color_bccls"
        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_bccls_color
            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(ix).Item("colorgbn").ToString
                        Case "1"
                            lblBcclsNm1.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor1.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor1.ForeColor = COLOR_BCCLSCD.FrColor(dt.Rows(ix).Item("colorgbn").ToString)
                        Case "2"
                            lblBcclsNm2.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor2.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor2.ForeColor = COLOR_BCCLSCD.FrColor(dt.Rows(ix).Item("colorgbn").ToString)
                        Case "3"
                            lblBcclsNm3.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor3.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor3.ForeColor = COLOR_BCCLSCD.FrColor(dt.Rows(ix).Item("colorgbn").ToString)
                    End Select
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
        End Try

    End Sub

    Private Sub sbSetWorkNo(ByVal rsBcNo As String, ByVal rsWorkNo As String)

        Dim strBcno As String

        For intRow As Integer = spdList.MaxRows To 1 Step -1
            With spdList
                .Row = intRow
                .Col = .GetColFromID("bcno_none")
                strBcno = .Text

                If strBcno.Substring(0, 14) = rsBcNo.Substring(0, 14) Then
                    .Row = intRow
                    .Col = .GetColFromID("workno_old")
                    If .Text = "" Then
                        .Col = .GetColFromID("workno_old")
                        .Text = rsWorkNo.Replace("-", "")
                    End If
                End If
            End With
        Next

    End Sub

    ' Form초기화
    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"

        Try
            ' 로그인정보 설정
            lblUserId.Text = USER_INFO.USRID
            lblUserNm.Text = USER_INFO.USRNM

            '-- 서버날짜로 설정
            dtpCollDt0.Value = CDate((New ServerDateTime).GetDate("-"))
            dtpCollDt1.Value = dtpCollDt0.Value

            sbSpreadColHidden(True)

            rdoGbnBatch.Checked = True
            rdoGbn_Click(rdoGbnBatch, Nothing)

            ' 기본 바코드프린터 설정
            Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    ' 화면 정리
    Private Sub sbFormClear(ByVal rsGbn As String)
        Dim sFn As String = "Private Sub sbFormClear(String)"

        Try

            If rsGbn = "ALL" Then
                Me.txtSearch.Text = ""

                Me.spdList.MaxRows = 0

                Me.lblCollDt.Text = ""
                Me.lblCollNm.Text = ""
                Me.lblSpcNm.Text = ""
                Me.lblRemark.Text = ""

            ElseIf rsGbn = "SPREAD" Then
                Me.lblCollDt.Text = ""
                Me.lblCollNm.Text = ""
                Me.lblSpcNm.Text = ""
                Me.lblRemark.Text = ""
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 칼럼 Hidden 유무
    Private Sub sbSpreadColHidden(ByVal rbFlag As Boolean)
        Dim sFn As String = "Private Sub fnSpreadColHidden(ByVal abFlag As Boolean)"

        Try
            With spdList
                .Col = .GetColFromID("spcflg") : .ColHidden = rbFlag
                .Col = .GetColFromID("wkgrpcd") : .ColHidden = rbFlag
                .Col = .GetColFromID("workno_old") : .ColHidden = rbFlag
                .Col = .GetColFromID("bcno_none") : .ColHidden = rbFlag
                .Col = .GetColFromID("tkyn") : .ColHidden = rbFlag
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 검체분류
    Private Sub sbDisplay_bccls()
        Dim sFn As String = "Private Sub sbDisplay_bccls()"

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Bccls_List()
            cboBcclsCd.Items.Clear()
            cboBcclsCd.Items.Add("[  ] 전체")

            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    cboBcclsCd.Items.Add("[" + dt.Rows(ix).Item("bcclscd").ToString + "] " + dt.Rows(ix).Item("bcclsnmd").ToString)

                    If dt.Rows(ix).Item("bcclscd").ToString = msBcClsCd Then cboBcclsCd.SelectedIndex = cboBcclsCd.Items.Count - 1
                Next
            End If

            If msBcClsCd = "" Then cboBcclsCd.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 검체선택후 해당 내역 표시
    ' 개별항목 접수는 바로 접수 처리, 일괄항목 접수는 리스트 표시 
    Private Sub sbDisplay_Data(ByVal rsBcno As String, ByVal riCnt As Integer)
        Dim sfn As String = "Private Sub sbDisplay_Data(String, Integer)"
        Dim objFn As New Fn

        Try
            rsBcno = rsBcno.Replace("-", "")

            If Fn.SpdColSearch(spdList, rsBcno, spdList.GetColFromID("bcno_none")) = 0 Then

                Dim dt As DataTable = fnGet_Coll_PatInfo_bcno(rsBcno)

                If dt.Rows.Count > 0 Then

                    If rdoGbnBatch.Checked = True Then
                        With spdList
                            .MaxRows += 1
                            .Row = 1
                            .InsertRows(1, 1)

                            sbDisplay_DataView(dt.Rows(0), 1, rsBcno)
                        End With
                    Else
                        With spdList
                            .MaxRows += 1
                            sbDisplay_DataView(dt.Rows(0), .MaxRows, rsBcno)
                            txtSearch.Focus()
                        End With
                    End If
                End If
            Else
                txtSearch.Focus()
            End If

            sbChangeTopRow()

        Catch ex As Exception
            Fn.log(msFile & sfn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 조회한 DaraRow의 내용을 Spread에 표시 
    ' 정은 수정중 2010-09-13
    Private Sub sbDisplay_DataView(ByVal r_dr As DataRow, ByVal riRow As Integer, ByVal rsBcNo As String)
        Dim sFn As String = "Private Sub fnViewSelect(ByVal aoData As DataRow, ByVal aiRow As Integer)"

        Dim sPatInfo() As String

        sPatInfo = r_dr.Item("patinfo").ToString.Split("|"c)

        Try
            With spdList
                .Row = riRow
                .Col = .GetColFromID("bcno") : .Text = r_dr.Item("bcno").ToString.Trim
                .Col = .GetColFromID("regno") : .Text = r_dr.Item("regno").ToString.Trim
                .Col = .GetColFromID("orddt") : .Text = r_dr.Item("orddt").ToString.Trim
                .Col = .GetColFromID("patnm") : .Text = r_dr.Item("patnm").ToString.Trim
                .Col = .GetColFromID("sexage") : .Text = r_dr.Item("sexage").ToString.Trim
                .Col = .GetColFromID("doctornm") : .Text = r_dr.Item("doctornm").ToString.Trim
                .Col = .GetColFromID("deptward") : .Text = r_dr.Item("deptward").ToString.Trim
                .Col = .GetColFromID("spcnmd") : .Text = r_dr.Item("spcnmd").ToString.Trim
                .Col = .GetColFromID("spcnmd") : .Text = r_dr.Item("spcnmd").ToString.Trim
                .Col = .GetColFromID("tnmds") : .Text = r_dr.Item("tnmds").ToString.Trim
                .Col = .GetColFromID("statgbn")

                If r_dr.Item("statgbn").ToString.Trim <> "" Then
                    .ForeColor = System.Drawing.Color.Red : .FontBold = True
                    .Text = "Y"
                    .set_RowHeight(riRow, 12.27)
                Else
                    .Text = ""
                End If

                Select Case r_dr.Item("colorgbn").ToString.Trim
                    Case "1"  '''혈액은행
                        .BackColor = Me.lblBcColor1.BackColor
                        .ForeColor = Me.lblBcColor1.ForeColor
                    Case "2"  ''' 외부 
                        .BackColor = Me.lblBcColor2.BackColor
                        .ForeColor = Me.lblBcColor2.ForeColor
                    Case "3"  ''' 기타 
                        .BackColor = Me.lblBcColor3.BackColor
                        .ForeColor = Me.lblBcColor3.ForeColor
                    Case Else
                        .BackColor = Me.lblBcColor0.BackColor
                        .ForeColor = Me.lblBcColor0.ForeColor
                End Select

                .Col = .GetColFromID("bcno_none") : .Text = r_dr.Item("bcno").ToString.Trim.Replace("-", "")
                .Col = .GetColFromID("workno_old") : .Text = r_dr.Item("workno_old").ToString.Trim
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 개별접수
    Private Sub sbReg_Web(ByVal rsBcNo As String)
        Dim sFn As String = "Private Sub sbReg_Web(String)"
        Try
            Dim sWkno_old As String = ""
            Dim bUseWkno_old As Boolean = False
            Dim sRetMsg As String = ""

            rsBcNo = rsBcNo.Replace("-", "")
            sWkno_old = fnGet_Workno_old(rsBcNo)    '-- 이전 작업버너호

            ' 과거 작업번호가 있는경우
            If sWkno_old <> "" Then
                If MsgBox("검체번호[ " + Fn.BCNO_View(rsBcNo, True) + " ]의 이전 작업번호가 있습니다. " + vbCrLf + vbCrLf + _
                          "이전 작업번호를 사용하시겠습니까? ", MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.Yes Then
                    bUseWkno_old = True
                Else
                    bUseWkno_old = False
                End If
            End If

            Dim sRet As String = (New WEBSERVER.CGWEB_J).ExecuteDo_TaKe(rsBcNo, Me.txtPassId.Text, IIf(bUseWkno_old, "Y", "N").ToString, "lis")

            If sRet.Substring(0, 2) <> "00" Then
                Throw (New Exception(sRet.Substring(2)))
            Else
                sbPrint_BarCode(rsBcNo) '-- 바코드 출력 루틴

                With Me.spdList
                    Dim iRow As Integer = .SearchCol(.GetColFromID("bcno_none"), 0, .MaxRows, rsBcNo, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRow < 1 Then Return

                    .Row = iRow

                    If sRet.Substring(2) <> "" Then
                        .Col = .GetColFromID("workno") : .Text = sRet.Substring(2)
                    Else
                        .Col = .GetColFromID("workno") : .Text = "-"
                    End If

                    ' 접수완료시 BackColor변경
                    .Row = iRow : .Col = -1
                    .BackColor = Drawing.Color.FromArgb(213, 255, 170)
                End With

            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub


    ' 일괄접수
    Private Sub sbReg_Web()
        Dim sFn As String = "Private Sub sbReg_Web()"

        Try

            Dim alBcno_Err As New ArrayList
            Dim bJobFlag As Boolean = True

            If Me.rdoGbnList.Checked = True Then
                If MsgBox("조회된 리스트 모두 일괄접수 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Return
            End If

            With Me.spdList
                If .MaxRows > 0 Then
                    For ix As Integer = .MaxRows To 1 Step -1
                        Dim bUseWkno_old As Boolean = False
                        Dim sWkno_old As String = ""
                        Dim sBcNo As String = ""

                        .Row = ix
                        .Col = .GetColFromID("workno")

                        If .Text.Trim = "" Then
                            '미접수된 항목만
                            .Col = .GetColFromID("workno_old") : sWkno_old = .Text.Trim
                            .Col = .GetColFromID("bcno") : sBcNo = .Text.ToString.Replace("-", "")

                            ' 과거 작업번호가 있는경우
                            If sWkno_old <> "" Then
                                If MsgBox("검체번호[ " + Fn.BCNO_View(sBcNo, True) & " ]의 이전 작업번호가 있습니다. " + vbCrLf + vbCrLf + _
                                          "이전 작업번호를 사용하시겠습니까? ", MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.Yes Then
                                    bUseWkno_old = True
                                Else
                                    bUseWkno_old = False
                                End If
                            End If
                            'procedure = pro_ack_exe_take
                            Dim sRet As String = (New WEBSERVER.CGWEB_J).ExecuteDo_TaKe(sBcNo, Me.txtPassId.Text, IIf(bUseWkno_old, "Y", "N").ToString, "lis")

                            If sRet.StartsWith("00") Then
                                sbPrint_BarCode(sBcNo)                             '-- 바코드 출력 루틴

                                With spdList
                                    .Row = ix

                                    If sRet.Substring(2) <> "" Then
                                        .Col = .GetColFromID("workno") : .Text = sRet.Substring(2)   ' 작업번호 [-]구분으로 표시하기
                                    Else
                                        .Col = .GetColFromID("workno") : .Text = "-"
                                    End If

                                    ' 접수완료시 BackColor변경
                                    .Row = ix : .Col = -1
                                    .BackColor = Drawing.Color.FromArgb(213, 255, 170)
                                    .Col = 0
                                    .Action = FPSpreadADO.ActionConstants.ActionGotoCell
                                End With
                            Else
                                alBcno_Err.Add(sBcNo)
                                bJobFlag = False
                            End If
                        End If
                    Next
                End If
            End With

            If bJobFlag = True Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 접수 되었습니다.")

                'If rdoGbnBatch.Checked Then
                '    Me.spdList.ReDraw = False
                '    Me.spdList.MaxRows = 0
                '    Me.spdList.ReDraw = True

                'End If
            Else
                Dim sErrMsg As String = "검체번호"

                For ix As Integer = 0 To alBcno_Err.Count - 1

                    If ix > 0 Then sErrMsg += ", "
                    sErrMsg += alBcno_Err(ix).ToString
                Next

                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "검체번호 [" + sErrMsg + "] 는" + vbCrLf + "접수시에 오류가 발생했습니다.!!")
                'MsgBox("일괄접수할 환자가 없습니다.", MsgBoxStyle.Critical, Me.Text)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 개별접수
    Private Sub sbReg(ByVal rsBcNo As String)
        Dim sFn As String = "Private Sub sbReg(String)"
        Dim objJubSu As New LISAPP.APP_J.TAKE
        Dim sWorkNos As String = ""

        Try
            Dim sWkno_old As String
            Dim bUseWkno_old As Boolean = False
            Dim alBcno As New ArrayList

            If Me.lblPassNm.Text = "" Then
                MsgBox("검체전달자 사원번호가 입력되지 않았습니다.!!", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, Me.Text)
                Me.spdList.MaxRows = Me.spdList.MaxRows - 1
                Return
            End If

            rsBcNo = rsBcNo.Replace("-", "")
            ' 이전 작업번호 조회 
            sWkno_old = fnGet_Workno_old(rsBcNo)
            ' 과거 작업번호가 있는경우
            If sWkno_old <> "" Then
                If MsgBox("검체번호[ " + Fn.BCNO_View(rsBcNo, True) + " ]의 이전 작업번호가 있습니다. " + vbCrLf + vbCrLf + _
                          "이전 작업번호를 사용하시겠습니까? ", MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.Yes Then
                    bUseWkno_old = True
                Else
                    bUseWkno_old = False
                End If
            End If

            Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(Me.Name)

            With objJubSu
                ' 이전 작업번호 사용시 처리 
                If bUseWkno_old = True Then .UseWknoOld = "Y"

                If .ExecuteDo(rsBcNo, Me.txtPassId.Text, sWorkNos) = False Then
                    Throw (New Exception(sWorkNos))
                Else

                    '-- 바코드 출력 루틴
                    sbPrint_BarCode(rsBcNo)
                    '-- 바코드 출력 루틴 끝

                    With spdList
                        .Row = .MaxRows
                        If sWorkNos <> "" Then
                            .Col = .GetColFromID("workno") : .Text = sWorkNos
                        Else
                            .Col = .GetColFromID("workno") : .Text = "-"
                        End If

                        ' 접수완료시 BackColor변경
                        .Row = .MaxRows : .Col = -1
                        .BackColor = Drawing.Color.FromArgb(213, 255, 170)
                        .Row = .MaxRows : .Col = 1 : .Action = FPSpreadADO.ActionConstants.ActionGotoCell

                    End With

                    'Me.txtPassId.Text = "" : Me.lblPassNm.Text = ""
                    Me.txtPassId.Focus()

                End If
            End With

        Catch ex As Exception
            spdList.MaxRows -= 1


            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 일괄접수
    Private Sub sbReg()
        Dim sFn As String = "Private Sub sbReg()"
        Dim objJubSu As New LISAPP.APP_J.TAKE

        Try
            Dim sRecID As String = ""
            Dim sRecNm As String = ""
            Dim sRecDT As String = Format(Now, "yyyy-MM-dd HH:mm:ss")

            Dim sWorkNos As String = ""
            Dim sBcNo As String = ""
            Dim bJobFlag As Boolean = False
            Dim bTranFlag As Boolean = False

            Dim sWorkno_old As String = ""
            Dim bUseWkno_old As Boolean = False

            Dim alBcno As New ArrayList
            Dim alBcno_Tran As New ArrayList

            If Me.lblPassNm.Text = "" Then
                MsgBox("검체전달자 사원번호가 입력되지 않았습니다.!!", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, Me.Text)
                Return
            End If

            If rdoGbnList.Checked = True Then
                ' 리스트 일괄접수시 Message 처리 
                If MsgBox("조회된 리스트 모두 일괄접수 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(Me.Name)

            '< 2009-03-11 부천순천향병원 위해 
            With spdList
                If .MaxRows > 0 Then
                    For ix As Integer = .MaxRows To 1 Step -1
                        bUseWkno_old = False

                        .Row = ix
                        .Col = .GetColFromID("workno")

                        If .Text.Trim = "" Then
                            '미접수된 항목만
                            .Col = .GetColFromID("workno_old") : sWorkno_old = .Text.Trim
                            .Col = .GetColFromID("bcno") : sBcNo = .Text.ToString.Replace("-", "")

                            ' 과거 작업번호가 있는경우
                            If sWorkno_old <> "" Then
                                If MsgBox("검체번호[ " + Fn.BCNO_View(sBcNo, True) & " ]의 이전 작업번호가 있습니다. " + vbCrLf + vbCrLf + _
                                          "이전 작업번호를 사용하시겠습니까? ", MsgBoxStyle.Question Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.Yes Then
                                    bUseWkno_old = True
                                Else
                                    bUseWkno_old = False
                                End If
                            End If

                            With objJubSu
                                .Init()
                                ' 이전 작업번호 사용시 처리 
                                If bUseWkno_old = True Then .UseWknoOld = "Y"

                                If .ExecuteDo(sBcNo, Me.txtPassId.Text, sWorkNos) = False Then
                                    Throw (New Exception(sWorkNos))
                                    Return
                                Else
                                    '-- 바코드 출력 루틴
                                    sbPrint_BarCode(sBcNo)
                                    '-- 바코드 출력 루틴 끝

                                    With spdList
                                        .Row = ix
                                        '.Col = .GetColFromID("statgbn") : .Text = "1"
                                        If sWorkNos <> "" Then
                                            .Col = .GetColFromID("workno") : .Text = sWorkNos   ' 작업번호 [-]구분으로 표시하기
                                        Else
                                            .Col = .GetColFromID("workno") : .Text = "-"
                                        End If

                                        ' 접수완료시 BackColor변경
                                        .Row = ix : .Col = -1
                                        .BackColor = Drawing.Color.FromArgb(213, 255, 170)
                                    End With

                                    bJobFlag = True ' 접수처리 구분
                                End If
                            End With
                            alBcno_Tran.Add(sBcNo)
                        End If
                    Next

                End If
            End With

            If bJobFlag = True Then
                MsgBox("정상적으로 접수 되었습니다.", MsgBoxStyle.Information, Me.Text)

                'Me.txtPassId.Text = "" : Me.lblPassNm.Text = ""

                'If rdoGbnBatch.Checked Then
                '    Me.spdList.ReDraw = False
                '    Me.spdList.MaxRows = 0
                '    Me.spdList.ReDraw = True

                'End If
            Else
                MsgBox("일괄접수할 환자가 없습니다.", MsgBoxStyle.Critical, Me.Text)
            End If

            Me.txtPassId.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 선택한 항목 리스트에서 삭제
    Private Sub sbDeleteRow()
        Dim sFn As String = "Private Sub sbDeleteRow()"

        Try
            If rdoGbnOne.Checked = True Then Exit Sub

            With spdList
                If .IsBlockSelected = True Or .SelectionCount > 0 Then
                    If .SelectionCount = 1 Then
                        Dim sBcno As String
                        Dim sPatnm As String

                        ' 단일 삭제
                        .Row = .SelBlockRow
                        .Col = .GetColFromID("bcno") : sBcno = .Text
                        .Col = .GetColFromID("patnm") : sPatnm = .Text

                        If sBcno <> "" Then
                            If MsgBox("[검체번호: " + sBcno + ", 성명: " + sPatnm + "] 항목을" + vbCrLf + vbCrLf + _
                                      "리스트에서 삭제 하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Me.Text) = MsgBoxResult.Yes Then
                                .DeleteRows(.SelBlockRow, 1) : .MaxRows -= 1
                                sbFormClear("SPREAD")
                            End If
                        End If

                    ElseIf .SelectionCount > 0 Then

                        If .SelBlockRow > 0 Then
                            If MsgBox("[" + .SelBlockRow.ToString + "번 ~" + .SelBlockRow2.ToString + "번] 항목을" & vbCrLf & vbCrLf _
                                    & "리스트에서 삭제 하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Me.Text) = MsgBoxResult.Yes Then
                                With spdList
                                    .DeleteRows(.SelBlockRow, .SelBlockRow2 - .SelBlockRow + 1) : .MaxRows -= .SelBlockRow2 - .SelBlockRow + 1
                                End With
                                sbFormClear("SPREAD")
                            End If
                        End If

                    End If
                End If

            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

#End Region


#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbFormInitialize()

    End Sub

    Public Sub New(ByVal rsBcClsCd As String)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        msBcClsCd = rsBcClsCd
        sbFormInitialize()
    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents rdoGbnOne As System.Windows.Forms.RadioButton
    Friend WithEvents rdoGbnBatch As System.Windows.Forms.RadioButton
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents lblCollDt As System.Windows.Forms.Label
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents lblCollNm As System.Windows.Forms.Label
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlButton As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboBcclsCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents rdoGbnList As System.Windows.Forms.RadioButton
    Friend WithEvents grpInputSelect As System.Windows.Forms.GroupBox
    Friend WithEvents grpListSelect As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents dtpCollDt1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpCollDt0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnSelBCPRT As System.Windows.Forms.Button
    Friend WithEvents lblBarPrinter As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGJ05))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.grpInputSelect = New System.Windows.Forms.GroupBox
        Me.btnToggle = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblPassNm = New System.Windows.Forms.Label
        Me.txtPassId = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboBcclsCd = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.rdoGbnList = New System.Windows.Forms.RadioButton
        Me.rdoGbnOne = New System.Windows.Forms.RadioButton
        Me.rdoGbnBatch = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblCollDt = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblSpcNm = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblCollNm = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlButton = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.btnSelBCPRT = New System.Windows.Forms.Button
        Me.lblBarPrinter = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblUserId = New System.Windows.Forms.Label
        Me.btnExcel = New CButtonLib.CButton
        Me.btnReg = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.grpListSelect = New System.Windows.Forms.GroupBox
        Me.dtpCollDt1 = New System.Windows.Forms.DateTimePicker
        Me.CButton1 = New CButtonLib.CButton
        Me.btnQuery = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtpCollDt0 = New System.Windows.Forms.DateTimePicker
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblBcColor0 = New System.Windows.Forms.Label
        Me.lblBcColor3 = New System.Windows.Forms.Label
        Me.lblBcColor2 = New System.Windows.Forms.Label
        Me.lblBcColor1 = New System.Windows.Forms.Label
        Me.lblBcclsNm3 = New System.Windows.Forms.Label
        Me.lblBcclsNm2 = New System.Windows.Forms.Label
        Me.lblBcclsNm1 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblRemark = New System.Windows.Forms.Label
        Me.chkBarInit = New System.Windows.Forms.CheckBox
        Me.grpInputSelect.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.pnlButton.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.grpListSelect.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpInputSelect
        '
        Me.grpInputSelect.Controls.Add(Me.btnToggle)
        Me.grpInputSelect.Controls.Add(Me.txtSearch)
        Me.grpInputSelect.Controls.Add(Me.lblSearch)
        Me.grpInputSelect.Location = New System.Drawing.Point(851, -3)
        Me.grpInputSelect.Name = "grpInputSelect"
        Me.grpInputSelect.Size = New System.Drawing.Size(265, 36)
        Me.grpInputSelect.TabIndex = 2
        Me.grpInputSelect.TabStop = False
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(221, 11)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(40, 21)
        Me.btnToggle.TabIndex = 1
        Me.btnToggle.Text = "<->"
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(85, 11)
        Me.txtSearch.MaxLength = 18
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(135, 21)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(4, 11)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(80, 21)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Location = New System.Drawing.Point(4, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1219, 475)
        Me.Panel1.TabIndex = 4
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1215, 471)
        Me.spdList.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblPassNm)
        Me.GroupBox1.Controls.Add(Me.txtPassId)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cboBcclsCd)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Panel15)
        Me.GroupBox1.Location = New System.Drawing.Point(4, -3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(844, 36)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblPassNm
        '
        Me.lblPassNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblPassNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPassNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPassNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPassNm.Location = New System.Drawing.Point(153, 11)
        Me.lblPassNm.Name = "lblPassNm"
        Me.lblPassNm.Size = New System.Drawing.Size(79, 21)
        Me.lblPassNm.TabIndex = 1
        Me.lblPassNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPassId
        '
        Me.txtPassId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPassId.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtPassId.Location = New System.Drawing.Point(88, 11)
        Me.txtPassId.MaxLength = 18
        Me.txtPassId.Name = "txtPassId"
        Me.txtPassId.Size = New System.Drawing.Size(64, 21)
        Me.txtPassId.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(7, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 21)
        Me.Label10.TabIndex = 98
        Me.Label10.Text = "사원번호"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboBcclsCd
        '
        Me.cboBcclsCd.DropDownHeight = 200
        Me.cboBcclsCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBcclsCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBcclsCd.IntegralHeight = False
        Me.cboBcclsCd.ItemHeight = 12
        Me.cboBcclsCd.Location = New System.Drawing.Point(665, 11)
        Me.cboBcclsCd.Margin = New System.Windows.Forms.Padding(0)
        Me.cboBcclsCd.Name = "cboBcclsCd"
        Me.cboBcclsCd.Size = New System.Drawing.Size(170, 20)
        Me.cboBcclsCd.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(245, 11)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(80, 21)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "접수구분"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(585, 11)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 21)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "검체분류"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Controls.Add(Me.rdoGbnList)
        Me.Panel15.Controls.Add(Me.rdoGbnOne)
        Me.Panel15.Controls.Add(Me.rdoGbnBatch)
        Me.Panel15.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.Panel15.Location = New System.Drawing.Point(326, 11)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(247, 21)
        Me.Panel15.TabIndex = 97
        '
        'rdoGbnList
        '
        Me.rdoGbnList.AutoSize = True
        Me.rdoGbnList.Enabled = False
        Me.rdoGbnList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbnList.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoGbnList.ForeColor = System.Drawing.Color.Black
        Me.rdoGbnList.Location = New System.Drawing.Point(157, 1)
        Me.rdoGbnList.Name = "rdoGbnList"
        Me.rdoGbnList.Size = New System.Drawing.Size(82, 16)
        Me.rdoGbnList.TabIndex = 4
        Me.rdoGbnList.Tag = "2"
        Me.rdoGbnList.Text = "리스트접수"
        '
        'rdoGbnOne
        '
        Me.rdoGbnOne.AutoSize = True
        Me.rdoGbnOne.Checked = True
        Me.rdoGbnOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbnOne.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoGbnOne.ForeColor = System.Drawing.Color.Black
        Me.rdoGbnOne.Location = New System.Drawing.Point(5, 1)
        Me.rdoGbnOne.Name = "rdoGbnOne"
        Me.rdoGbnOne.Size = New System.Drawing.Size(70, 16)
        Me.rdoGbnOne.TabIndex = 2
        Me.rdoGbnOne.TabStop = True
        Me.rdoGbnOne.Tag = "0"
        Me.rdoGbnOne.Text = "개별접수"
        '
        'rdoGbnBatch
        '
        Me.rdoGbnBatch.AutoSize = True
        Me.rdoGbnBatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbnBatch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoGbnBatch.ForeColor = System.Drawing.Color.Black
        Me.rdoGbnBatch.Location = New System.Drawing.Point(81, 1)
        Me.rdoGbnBatch.Name = "rdoGbnBatch"
        Me.rdoGbnBatch.Size = New System.Drawing.Size(70, 16)
        Me.rdoGbnBatch.TabIndex = 3
        Me.rdoGbnBatch.Tag = "1"
        Me.rdoGbnBatch.Text = "일괄접수"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.lblCollDt)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblSpcNm)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.lblCollNm)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(691, 511)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(234, 82)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        '
        'lblCollDt
        '
        Me.lblCollDt.BackColor = System.Drawing.Color.White
        Me.lblCollDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCollDt.ForeColor = System.Drawing.Color.Black
        Me.lblCollDt.Location = New System.Drawing.Point(76, 34)
        Me.lblCollDt.Name = "lblCollDt"
        Me.lblCollDt.Size = New System.Drawing.Size(153, 21)
        Me.lblCollDt.TabIndex = 3
        Me.lblCollDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(5, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 21)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "채 혈 자"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcNm
        '
        Me.lblSpcNm.BackColor = System.Drawing.Color.White
        Me.lblSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSpcNm.ForeColor = System.Drawing.Color.Black
        Me.lblSpcNm.Location = New System.Drawing.Point(76, 12)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(153, 21)
        Me.lblSpcNm.TabIndex = 1
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(5, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 21)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "채혈일시"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCollNm
        '
        Me.lblCollNm.BackColor = System.Drawing.Color.White
        Me.lblCollNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCollNm.ForeColor = System.Drawing.Color.Black
        Me.lblCollNm.Location = New System.Drawing.Point(76, 56)
        Me.lblCollNm.Name = "lblCollNm"
        Me.lblCollNm.Size = New System.Drawing.Size(153, 21)
        Me.lblCollNm.TabIndex = 5
        Me.lblCollNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(5, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "검 체 명"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlButton
        '
        Me.pnlButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlButton.Controls.Add(Me.Panel5)
        Me.pnlButton.Controls.Add(Me.lblUserNm)
        Me.pnlButton.Controls.Add(Me.lblUserId)
        Me.pnlButton.Controls.Add(Me.btnExcel)
        Me.pnlButton.Controls.Add(Me.btnReg)
        Me.pnlButton.Controls.Add(Me.btnClear)
        Me.pnlButton.Controls.Add(Me.btnExit)
        Me.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButton.Location = New System.Drawing.Point(0, 595)
        Me.pnlButton.Name = "pnlButton"
        Me.pnlButton.Size = New System.Drawing.Size(1228, 34)
        Me.pnlButton.TabIndex = 7
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkBarInit)
        Me.Panel5.Controls.Add(Me.btnSelBCPRT)
        Me.Panel5.Controls.Add(Me.lblBarPrinter)
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Location = New System.Drawing.Point(4, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(311, 24)
        Me.Panel5.TabIndex = 163
        '
        'btnSelBCPRT
        '
        Me.btnSelBCPRT.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSelBCPRT.ForeColor = System.Drawing.Color.Black
        Me.btnSelBCPRT.Image = CType(resources.GetObject("btnSelBCPRT.Image"), System.Drawing.Image)
        Me.btnSelBCPRT.Location = New System.Drawing.Point(281, 0)
        Me.btnSelBCPRT.Name = "btnSelBCPRT"
        Me.btnSelBCPRT.Size = New System.Drawing.Size(30, 24)
        Me.btnSelBCPRT.TabIndex = 103
        Me.btnSelBCPRT.UseVisualStyleBackColor = False
        '
        'lblBarPrinter
        '
        Me.lblBarPrinter.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBarPrinter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBarPrinter.ForeColor = System.Drawing.Color.Black
        Me.lblBarPrinter.Location = New System.Drawing.Point(94, 0)
        Me.lblBarPrinter.Name = "lblBarPrinter"
        Me.lblBarPrinter.Size = New System.Drawing.Size(186, 24)
        Me.lblBarPrinter.TabIndex = 102
        Me.lblBarPrinter.Text = "AUTO LABELER (외래채혈실)"
        Me.lblBarPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 22)
        Me.Label7.TabIndex = 101
        Me.Label7.Text = " 출력프린터"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(416, 8)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(76, 20)
        Me.lblUserNm.TabIndex = 1
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(344, 8)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(68, 20)
        Me.lblUserId.TabIndex = 0
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems1
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker2
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(821, 4)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(96, 25)
        Me.btnExcel.TabIndex = 188
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems2
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.5!
        Me.btnReg.FocalPoints.CenterPtY = 0.0!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker4
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(918, 4)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(100, 25)
        Me.btnReg.TabIndex = 187
        Me.btnReg.Text = "일괄접수(F5)"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.5!
        Me.btnClear.FocalPoints.CenterPtY = 0.0!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1019, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 186
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1120, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(97, 25)
        Me.btnExit.TabIndex = 185
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'grpListSelect
        '
        Me.grpListSelect.Controls.Add(Me.dtpCollDt1)
        Me.grpListSelect.Controls.Add(Me.CButton1)
        Me.grpListSelect.Controls.Add(Me.btnQuery)
        Me.grpListSelect.Controls.Add(Me.Label6)
        Me.grpListSelect.Controls.Add(Me.Label4)
        Me.grpListSelect.Controls.Add(Me.dtpCollDt0)
        Me.grpListSelect.Location = New System.Drawing.Point(851, -3)
        Me.grpListSelect.Name = "grpListSelect"
        Me.grpListSelect.Size = New System.Drawing.Size(337, 36)
        Me.grpListSelect.TabIndex = 3
        Me.grpListSelect.TabStop = False
        '
        'dtpCollDt1
        '
        Me.dtpCollDt1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpCollDt1.Location = New System.Drawing.Point(186, 11)
        Me.dtpCollDt1.Name = "dtpCollDt1"
        Me.dtpCollDt1.Size = New System.Drawing.Size(86, 21)
        Me.dtpCollDt1.TabIndex = 1
        '
        'CButton1
        '
        Me.CButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CButton1.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton1.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems5.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.CButton1.ColorFillBlend = CBlendItems5
        Me.CButton1.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.CButton1.Corners.All = CType(6, Short)
        Me.CButton1.Corners.LowerLeft = CType(6, Short)
        Me.CButton1.Corners.LowerRight = CType(6, Short)
        Me.CButton1.Corners.UpperLeft = CType(6, Short)
        Me.CButton1.Corners.UpperRight = CType(6, Short)
        Me.CButton1.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.CButton1.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.CButton1.FocalPoints.CenterPtX = 0.4545455!
        Me.CButton1.FocalPoints.CenterPtY = 0.6190476!
        Me.CButton1.FocalPoints.FocusPtX = 0.0!
        Me.CButton1.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton1.FocusPtTracker = DesignerRectTracker10
        Me.CButton1.Image = Nothing
        Me.CButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton1.ImageIndex = 0
        Me.CButton1.ImageSize = New System.Drawing.Size(16, 16)
        Me.CButton1.Location = New System.Drawing.Point(275, 11)
        Me.CButton1.Name = "CButton1"
        Me.CButton1.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.CButton1.SideImage = Nothing
        Me.CButton1.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CButton1.SideImageSize = New System.Drawing.Size(48, 48)
        Me.CButton1.Size = New System.Drawing.Size(58, 21)
        Me.CButton1.TabIndex = 169
        Me.CButton1.Text = "조회"
        Me.CButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.CButton1.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnQuery.Location = New System.Drawing.Point(278, 12)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(47, 18)
        Me.btnQuery.TabIndex = 2
        Me.btnQuery.Text = "조회"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(4, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 21)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "채혈구간"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(173, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "~"
        '
        'dtpCollDt0
        '
        Me.dtpCollDt0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpCollDt0.Location = New System.Drawing.Point(85, 11)
        Me.dtpCollDt0.Name = "dtpCollDt0"
        Me.dtpCollDt0.Size = New System.Drawing.Size(88, 21)
        Me.dtpCollDt0.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.lblBcColor0)
        Me.GroupBox2.Controls.Add(Me.lblBcColor3)
        Me.GroupBox2.Controls.Add(Me.lblBcColor2)
        Me.GroupBox2.Controls.Add(Me.lblBcColor1)
        Me.GroupBox2.Controls.Add(Me.lblBcclsNm3)
        Me.GroupBox2.Controls.Add(Me.lblBcclsNm2)
        Me.GroupBox2.Controls.Add(Me.lblBcclsNm1)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Location = New System.Drawing.Point(928, 511)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(295, 83)
        Me.GroupBox2.TabIndex = 165
        Me.GroupBox2.TabStop = False
        '
        'lblBcColor0
        '
        Me.lblBcColor0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcColor0.BackColor = System.Drawing.Color.White
        Me.lblBcColor0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor0.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor0.Location = New System.Drawing.Point(9, 28)
        Me.lblBcColor0.Name = "lblBcColor0"
        Me.lblBcColor0.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor0.TabIndex = 203
        Me.lblBcColor0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBcColor0.Visible = False
        '
        'lblBcColor3
        '
        Me.lblBcColor3.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblBcColor3.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor3.Location = New System.Drawing.Point(207, 49)
        Me.lblBcColor3.Name = "lblBcColor3"
        Me.lblBcColor3.Size = New System.Drawing.Size(18, 18)
        Me.lblBcColor3.TabIndex = 25
        Me.lblBcColor3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor2
        '
        Me.lblBcColor2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblBcColor2.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor2.Location = New System.Drawing.Point(111, 49)
        Me.lblBcColor2.Name = "lblBcColor2"
        Me.lblBcColor2.Size = New System.Drawing.Size(18, 18)
        Me.lblBcColor2.TabIndex = 24
        Me.lblBcColor2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor1
        '
        Me.lblBcColor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(19, Byte), Integer))
        Me.lblBcColor1.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor1.Location = New System.Drawing.Point(8, 49)
        Me.lblBcColor1.Name = "lblBcColor1"
        Me.lblBcColor1.Size = New System.Drawing.Size(18, 18)
        Me.lblBcColor1.TabIndex = 23
        Me.lblBcColor1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcclsNm3
        '
        Me.lblBcclsNm3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm3.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm3.Location = New System.Drawing.Point(228, 49)
        Me.lblBcclsNm3.Name = "lblBcclsNm3"
        Me.lblBcclsNm3.Size = New System.Drawing.Size(62, 16)
        Me.lblBcclsNm3.TabIndex = 22
        Me.lblBcclsNm3.Text = "기타"
        Me.lblBcclsNm3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBcclsNm2
        '
        Me.lblBcclsNm2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm2.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm2.Location = New System.Drawing.Point(132, 49)
        Me.lblBcclsNm2.Name = "lblBcclsNm2"
        Me.lblBcclsNm2.Size = New System.Drawing.Size(69, 16)
        Me.lblBcclsNm2.TabIndex = 21
        Me.lblBcclsNm2.Text = "외부의뢰"
        Me.lblBcclsNm2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBcclsNm1
        '
        Me.lblBcclsNm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm1.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm1.Location = New System.Drawing.Point(31, 49)
        Me.lblBcclsNm1.Name = "lblBcclsNm1"
        Me.lblBcclsNm1.Size = New System.Drawing.Size(75, 16)
        Me.lblBcclsNm1.TabIndex = 20
        Me.lblBcclsNm1.Text = "혈액은행"
        Me.lblBcclsNm1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(5, 13)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(285, 23)
        Me.Label15.TabIndex = 19
        Me.Label15.Text = "범   례"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1165, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 12)
        Me.Label3.TabIndex = 167
        Me.Label3.Text = "라디오박스들 그룹 숨어있음"
        Me.Label3.Visible = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.lblRemark)
        Me.GroupBox5.Location = New System.Drawing.Point(2, 511)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(686, 82)
        Me.GroupBox5.TabIndex = 169
        Me.GroupBox5.TabStop = False
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(6, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 62)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "의뢰의사 Remark"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRemark
        '
        Me.lblRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRemark.BackColor = System.Drawing.Color.White
        Me.lblRemark.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRemark.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRemark.Location = New System.Drawing.Point(75, 13)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(605, 63)
        Me.lblRemark.TabIndex = 8
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkBarInit
        '
        Me.chkBarInit.AutoSize = True
        Me.chkBarInit.Location = New System.Drawing.Point(71, 5)
        Me.chkBarInit.Name = "chkBarInit"
        Me.chkBarInit.Size = New System.Drawing.Size(15, 14)
        Me.chkBarInit.TabIndex = 225
        Me.chkBarInit.UseVisualStyleBackColor = True
        '
        'FGJ05
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1228, 629)
        Me.Controls.Add(Me.grpInputSelect)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grpListSelect)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.pnlButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "FGJ05"
        Me.Text = "검체 전달 및 접수"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpInputSelect.ResumeLayout(False)
        Me.grpInputSelect.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.pnlButton.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.grpListSelect.ResumeLayout(False)
        Me.grpListSelect.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region " 메인 버튼 처리 "
    ' Function Key정의
    Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim sFn As String = "Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown"

        'F4 : 화면정리 
        'F5 : 일괄접수
        'F10: 화면종료

        If e.KeyCode = Keys.F5 Then
            btnReg_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.F4 Then
            btnClear_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()

        ElseIf e.KeyCode = Keys.Delete Then
            ' 일괄 및 리스트접수시 리스트에서 선택항목 삭제처리 ( Delete Key ) 
            Try
                Debug.WriteLine("Mybase_KeyDown")
                If Not rdoGbnOne.Checked = True Then sbDeleteRow()

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

            End Try
        End If
    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.ButtonClick"

        Try
            'sbReg()
            sbReg_Web()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = "Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.ButtonClick"

        Try
            'Me.txtPassId.Text = "" : Me.lblPassNm.Text = ""

            sbFormClear("ALL")

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region

#Region " Control Event 처리 "

    Private Sub btnToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Dim CommFn As New COMMON.CommFN.Fn
        Fn.SearchToggle(lblSearch, btnToggle, enumToggle.BcnoToRegno, txtSearch)
        txtSearch.Text = ""
        txtSearch.Focus()
    End Sub

    Private Sub rdoGbn_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rdoGbnOne.KeyPress, rdoGbnBatch.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : cboBcclsCd.Focus()
        End If
    End Sub

    Private Sub cboSect_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboBcclsCd.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : txtSearch.Focus()
        End If
    End Sub

    Private Sub rdoGbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoGbnOne.Click, rdoGbnBatch.Click, rdoGbnList.Click
        Dim sFn As String = "Handles rdoGbn.Click"

        Try
            grpInputSelect.Visible = False
            grpListSelect.Visible = False

            If rdoGbnOne.Checked Then
                btnReg.Enabled = False
                grpInputSelect.Visible = True
                spdList.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
                txtSearch.Focus()
            ElseIf rdoGbnBatch.Checked Then
                btnReg.Enabled = True
                grpInputSelect.Visible = True
                spdList.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeExtended
                txtSearch.Focus()
            ElseIf rdoGbnList.Checked Then
                btnReg.Enabled = True
                grpListSelect.Visible = True
                spdList.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeExtended
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus
        txtSearch.SelectAll()
    End Sub


    Public Overridable Sub sbChangeTopRow()
        Dim sFn As String = "Sub sbChangeTopRow"

        Try
            With Me.spdList
                Dim iHeight As Integer = .Height
                Dim dblRowHeight As Double
                Dim iTwips As Integer

                .RowHeightToTwips(.MaxRows, CSng(.get_RowHeight(.MaxRows)), iTwips)
                dblRowHeight = iTwips / 15

                If .MaxRows >= (CInt(iHeight / dblRowHeight) - 1) Then
                    .ReDraw = False
                    .TopRow = .MaxRows - (CInt(iHeight / dblRowHeight) - 1) + 2
                    .ReDraw = True
                End If
            End With

        Catch ex As Exception
            'ViewMsgMain(sFn + ":" + "CFBASE - " + ex.Message)

        Finally
            Me.spdList.ReDraw = True

        End Try
    End Sub


    Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick
        Dim sFn As String = "Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick"

        Try
            sbDeleteRow()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub spdList_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdList.RightClick
        Dim sFn As String = "Private Sub spdList_RightClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles spdList.RightClick"

        Try
            sbDeleteRow()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub spdList_TextTipFetch(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles spdList.TextTipFetch
        Fn.SpreadToolTipView(spdList, Me.CreateGraphics, e, spdList.GetColFromID("orddt"), True)
    End Sub

    Private Sub dtpCollDt_ValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpCollDt0.KeyPress, dtpCollDt1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    ' 해당 검체전달구간으로 조회
    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Dim sFn As String = "Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click"

        Try
            Dim dt As DataTable = fnGet_Coll_PatList(dtpCollDt0.Text, dtpCollDt1.Text, Ctrl.Get_Code(cboBcclsCd))

            sbFormClear("ALL")

            If dt.Rows.Count > 0 Then
                With spdList
                    For ix As Integer = 0 To dt.Rows.Count - 1

                        .MaxRows += 1
                        sbDisplay_DataView(dt.Rows(ix), .MaxRows, dt.Rows(ix).Item("bcno").ToString().Replace("-", ""))

                    Next
                End With
            Else
                MsgBox("검체전달일자구간에 해당하는 환자가 없습니다.", MsgBoxStyle.Critical, Me.Text)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

#End Region

    Private Sub btnSelBCPRT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelBCPRT.Click
        Dim sFn As String = "Private Sub btnSelBCPRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelBCPRT.Click"
        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC("FGJ05", Me.chkBarInit.Checked)

        Try
            objFrm.ShowDialog()
            lblBarPrinter.Text = objFrm.mPrinterName

            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    '엑셀연동
    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        With spdList
            .ReDraw = False

            .MaxRows += 4
            .InsertRows(1, 3)

            .Col = 8
            .Row = 1
            .Text = "일괄 접수 리스트"
            .FontBold = True
            .FontSize = 15
            .ForeColor = System.Drawing.Color.Red

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 3 : .Row2 = 3
            .Clip = sColHeaders

            .InsertRows(4, 1)

            If spdList.ExportToExcel("WorkList_" + Now.ToShortDateString() + ".xls", "Worklist", "") Then
                Process.Start("WorkList_" + Now.ToShortDateString() + ".xls")
            End If

            .DeleteRows(1, 4)
            .MaxRows -= 4

            .ReDraw = True

        End With
    End Sub

    Private Sub txtSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Dim sFn As String = ""

        Try
            Me.txtSearch.Focus()
            Me.txtSearch.SelectAll()
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGJ05_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sFn As String = ""

        Try
            'If USER_INFO.USRLVL = "S" Then
            '    rdoGbn2.Enabled = True
            'End If
            sbFormClear("ALL")

            sbDisplay_bccls()
            sbDisplay_Color_bccls()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FG_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    ' 접수시 검체번호나 등록번호 입력후 엔터 
    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim sFn As String = "Handles txtSearch.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim sRegNo As String = ""
            Dim sBcNo As String = ""

            Me.txtSearch.Text = Me.txtSearch.Text.Replace("-", "").Trim()

            If Me.txtSearch.Text <> "" Then

                If Me.lblSearch.Text = "검체번호" Then
                    '검체번호 선택시 처리내용
                    If Me.txtSearch.Text.Length = 11 Then
                        ' 바코드에서 직접 입력시

                        ' 바코드번호(검체번호)를 표시형 검체번호로 변경
                        Dim objCommDBFN As New LISAPP.APP_DB.DbFn
                        Me.txtSearch.Text = objCommDBFN.GetBCPrtToView(Me.txtSearch.Text)

                    ElseIf Me.txtSearch.Text.Length < PRG_CONST.Len_BcNo - 1 Then
                        MsgBox("잘못된 검체번호 입니다.", MsgBoxStyle.Critical, Me.Text)
                        txtSearch.Focus()
                        Exit Sub
                    End If

                    Dim sBcclsCd As String = Ctrl.Get_Code(cboBcclsCd)

                    ' 검체번호 조회시 해당계 체크
                    If sBcclsCd <> "" Then
                        If sBcclsCd.Trim <> Me.txtSearch.Text.Substring(8, 2) Then
                            MsgBox(Ctrl.Get_Name(cboBcclsCd) + "의 검체가 아닙니다.", MsgBoxStyle.Critical, Me.Text)
                            txtSearch.SelectAll()
                            'txtSearch.Text = ""
                            txtSearch.Focus()
                            Return
                        End If
                    End If

                    sBcNo = Me.txtSearch.Text
                Else
                    ' 등록번호는 8자리가 안되는것 0으로 채운다
                    If IsNumeric(Me.txtSearch.Text.Substring(0, 1)) Then
                        Me.txtSearch.Text = Me.txtSearch.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                    Else
                        Me.txtSearch.Text = Me.txtSearch.Text.Substring(0, 1).ToUpper + Me.txtSearch.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                    End If

                    sRegNo = Me.txtSearch.Text
                End If

            End If

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            Dim dt As DataTable = fnGet_Coll_PatInfo(sRegNo, sBcNo, Ctrl.Get_Code(cboBcclsCd))

            objHelp.FormText = "접수 대상자 조회"
            objHelp.MaxRows = 15
            objHelp.OnRowReturnYN = True

            objHelp.AddField("'' CHK", "", 3, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("bcno", "검체번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("regno", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("patnm", "성명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sexage", "성별/나이", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("orddt", "처방일시", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("doctornm", "의뢰의사", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("deptward", "진료과 및 병동", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmds", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtSearch)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + Me.txtSearch.Height + 80, dt)

            If alList.Count > 0 Then
                sbFormClear("SPREAD") ' 화면정리 
                For ix As Integer = 0 To alList.Count - 1
                    Dim sBcNo_tmp As String = alList.Item(ix).ToString.Split("|"c)(0).Replace("-", "")

                    sbDisplay_Data(sBcNo_tmp, alList.Count)
                    If rdoGbnOne.Checked = True Then
                        ' 개별접수인경우 자동 접수
                        sbReg(sBcNo_tmp)
                        sbReg_Web(sBcNo_tmp)
                    End If
                Next

                Me.txtSearch.SelectAll()
                'Me.txtSearch.Text = ""
            Else
                If Me.lblSearch.Text = "검체번호" Then
                    dt = fnGet_bcno_state(Me.txtSearch.Text) ''' 바코드발행, 접수상태 조회 

                    If dt.Rows.Count > 0 Then
                        Dim sSpcFlg As String = CStr(dt.Rows(0).Item("spcflg"))

                        If sSpcFlg = "4" Then
                            MsgBox("이미 접수된 검체번호 입니다.", MsgBoxStyle.Critical, Me.Text)
                        ElseIf sSpcFlg = "1" Then
                            MsgBox("채혈일시 등록이 필요합니다.", MsgBoxStyle.Critical, Me.Text)
                        ElseIf sSpcFlg = "0" Then
                            MsgBox("채혈취소된 검체번호 입니다.", MsgBoxStyle.Critical, Me.Text)
                        ElseIf sSpcFlg = "R" Then
                            MsgBox("Reject된 검체번호 입니다.", MsgBoxStyle.Critical, Me.Text)
                        End If
                    Else

                        MsgBox("해당하는 검체번호가 없습니다.", MsgBoxStyle.Critical, Me.Text)
                    End If
                Else

                    MsgBox("해당하는 환자가 없습니다.", MsgBoxStyle.Critical, Me.Text)
                End If

                Me.txtSearch.SelectAll()
                'Me.txtSearch.Text = ""
            End If
            Me.txtSearch.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub


    Private Sub pnlButton_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlButton.DoubleClick
        If USER_INFO.USRLVL <> "S" Then Exit Sub

#If DEBUG Then
        Static blnChk As Boolean = False

        '-- 컬럼내용모두 보기/감추기
        sbSpreadColHidden(blnChk)
        blnChk = Not blnChk
#End If

    End Sub

    Private Sub txtPassId_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassId.Click
        Me.txtPassId.SelectionStart = 0
        Me.txtPassId.SelectAll()
    End Sub

    Private Sub txtPassId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassId.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Me.spdList.ReDraw = False
            Me.spdList.MaxRows = 0
            Me.spdList.ReDraw = True

            Me.lblPassNm.Text = OCSAPP.OcsLink.SData.fnGet_OcsUsr_Info(Me.txtPassId.Text)
            If Me.lblPassNm.Text <> "" Then Me.txtSearch.Focus()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub
End Class