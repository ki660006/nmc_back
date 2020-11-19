'/****************************************************************************************************************/
'/*                                                                                                              */
'/* Project Name : NEW LIS Laboratory Information System()                                                       */
'/*                                                                                                              */
'/*                                                                                                              */
'/* FileName     : FGCDHELP99.vb                                                                                 */
'/* PartName     : 공통 팝업                                                                                     */
'/* Description  : 쿼리를 함수로 호출하여 공통적으로 사용 할 팝업을 호출                                         */
'/* Parameter    : rofrm     (팝업이 호출될 폼  : ex) me                                                         */
'/*                rsPopNm   (팝업 타이틀)                                                                       */
'/*                ralArg    (조건이 적용될 만큼의 ArrayList - union 이 걸린경우 같은 조건을 적용 할 ArrayList)  */      
'/*                ralHeader (스프레드에 적용할 헤더숫자 ArrayList - 조건콤보의 리스트로 이용됨)                 */    
'/*                riRtnCnt  (돌려받을 컬럼의 갯수 - 헤더의 숫자보다 클경우 visible = false)                     */ 
'/*                rsArg     (팝업이 실행 될때 조회조건으로 사용될 값 - 설정할경우 조건 텍스트 박스에 표시됨     */ 
'/*                rsMulty   (여러건을 선택 하여 값을 넘겨 줄 수 있도록 설정 : Y (구조체클래스 ArrayList return) */ 
'/*                riGubun   (콤보의 초기 index 설정값)                                                          */ 
'/*                rsFixArg  (조회조건 픽스시킬지 여부)                                                          */ 
'/*                riTop     (팝업이 실행될 top)                                                                 */ 
'/*                riLeft    (팝업이 실행될 left)                                                                */ 
'/* Design       : 2010-09-26 Lee Hyung Taek                                                                     */
'/* Coded        :                                                                                               */
'/* Modified     :                                                                                               */
'/*                                                                                                              */
'/*                                                                                                              */
'/*                                                                                                              */
'/****************************************************************************************************************/

Imports System.Windows.Forms
Imports System.Drawing

Imports Common.CommFN
Imports Common.CommFN.CGCOMMON13
Imports Common.SVar
Imports COMMON.CommLogin.LOGIN

Public Class FGCDHELP99
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGCDHELP99.vb, Class : FGCDHELP99" & vbTab
    Private msMulty As String
    Private miArgCnt As Integer
    Private malArg As New ArrayList
    Private malRtn As New ArrayList
    Private miColCnt As Integer
    Private miChkRow As Integer = -1
    Private miCurRow As Integer = -1
    Private msFnStr As String

    ' 팝업 화면 호출부
    Public Function fn_DisplayPop(ByVal rofrm As Windows.Forms.Form, ByVal rsPopNm As String, ByVal rsFnNm As String, ByVal ralArg As ArrayList, ByVal ralHeader As ArrayList, ByVal riRtnCnt As Integer, ByVal rsArg As String, Optional ByVal rsMulty As String = "N"c, Optional ByVal riGubun As Integer = 0, Optional ByVal rsFixArg As String = "N"c, Optional ByVal riTop As Integer = 0, Optional ByVal riLeft As Integer = 0) As ArrayList
        Dim la_Result As New ArrayList
        Dim li_Top As Integer
        Dim li_Left As Integer
        Dim li_Gbn As Integer
        Dim sqlDoc As String
        Dim ldt_List As New DataTable
        Dim lal_Rtn As New ArrayList

        Dim sFn As String = "Public Function fn_DisplayPop(ByVal rofrm As Windows.Forms.Form, ByVal rsPopNm As String, ByVal rsFnNm As String, ByVal ralArg As ArrayList, ByVal ralHeader As ArrayList, ByVal riRtnCnt As Integer, ByVal rsArg As String, Optional ByVal rsMulty As String = N, Optional ByVal rsGubun As String = 1, Optional ByVal riTop As Integer = 0, Optional ByVal riLeft As Integer = 0) As ArrayList"

        Try
            Me.Text = rsPopNm

            ' 팝업위치가 없다면 화면 중앙에 팝업 호출
            If riTop = 0 Then
                Me.StartPosition = FormStartPosition.CenterParent
            Else
                li_Top = riTop
            End If

            If riLeft = 0 Then
                Me.StartPosition = FormStartPosition.CenterParent
            Else
                li_Left = riLeft
            End If

            msMulty = rsMulty   ' 멀티 체크 여부 설정
            malArg = ralArg     ' 조건 파라미터 배열 (배열의 크기만큼 조건을 반영)
            miColCnt = riRtnCnt ' 리턴 받을 컬럼의 갯수 (세팅할 컬럼의 갯수)
            msFnStr = rsFnNm    ' 쿼리 스트링을 가져올 함수명

            ' 텍스트 형식으로 받은 함수 호출하기
            Dim objFn As New CDHELP.FGCDHELPFN

            ' 전체 조회시 혹은 조건 조회 
            If rsArg = "" Then
                li_Gbn = 99
            Else
                li_Gbn = riGubun
            End If

            ' 헤더 세팅 (표시될 항목, 스프레드에 기억될 총갯수)
            sb_SetHeader(ralHeader, riRtnCnt)

            ' 조건을 넘길경우 조회조건으로 넘어온 값을 텍스트 박스에 표시
            If rsArg <> "" Then
                txtSearch.Text = rsArg
            End If

            ' 쿼리 스트링 얻어옴
            sqlDoc = CallByName(objFn, rsFnNm, CallType.Method, li_Gbn)

            ' 콤보 설정
            sb_SetCombo(ralHeader, riGubun)

            If rsFixArg = "Y"c Then
                cboGubun.Enabled = False
                txtSearch.Enabled = False
            End If

            Me.Top = li_Top
            Me.Left = li_Left

            ' 조건으로 받은 데이터가 있을경우에만 조회
            If rsArg.Length > 0 Then
                ' 데이터 테이블 가져옴
                ldt_List = CDHELP.FGCDHELPFN.fn_RtnDataList(sqlDoc, ralArg)

                ' 데이터 스프레드에 뿌리기
                sb_setData(spdSearchList, riRtnCnt, ldt_List)
            End If

            If ldt_List.Rows.Count = 1 Then
                If msMulty = "N"c Then
                    For z As Integer = 0 To riRtnCnt - 1
                        lal_Rtn.Add(ldt_List.Rows(0).Item(z).ToString)
                    Next

                    malRtn = lal_Rtn
                Else
                    Dim lcls_RtnData As New clsRtnData
                    Dim lal_RtnData As New ArrayList

                    For u As Integer = 0 To 9
                        If riRtnCnt > u Then
                            lal_RtnData.Add(ldt_List.Rows(0).Item(u).ToString)
                        Else
                            lal_RtnData.Add(" "c)
                        End If
                    Next

                    lcls_RtnData.RTNDATA0 = lal_RtnData(0)
                    lcls_RtnData.RTNDATA1 = lal_RtnData(1)
                    lcls_RtnData.RTNDATA2 = lal_RtnData(2)
                    lcls_RtnData.RTNDATA3 = lal_RtnData(3)
                    lcls_RtnData.RTNDATA4 = lal_RtnData(4)
                    lcls_RtnData.RTNDATA5 = lal_RtnData(5)
                    lcls_RtnData.RTNDATA6 = lal_RtnData(6)
                    lcls_RtnData.RTNDATA7 = lal_RtnData(7)
                    lcls_RtnData.RTNDATA8 = lal_RtnData(8)
                    lcls_RtnData.RTNDATA9 = lal_RtnData(9)

                    lal_Rtn.Add(lcls_RtnData)

                    malRtn = lal_Rtn
                End If

            Else
                Me.ShowDialog(rofrm)
            End If

            Return malRtn
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, msFile + sFn + vbCrLf + ex.Message)
            Return New ArrayList
        End Try

    End Function

    ' 콤보박스 데이터 세팅
    Private Sub sb_SetCombo(ByVal ralSet As ArrayList, ByVal riGubun As Integer)
        For i As Integer = 0 To ralSet.Count - 1
            cboGubun.Items.Add(ralSet(i))
        Next

        cboGubun.Text = ralSet(riGubun)

    End Sub

    ' 스프레드 헤더 설정
    Private Sub sb_SetHeader(ByVal ralHeader As ArrayList, ByVal riRtnCnt As Integer)
        Dim li_Col As Integer

        With spdSearchList
            .ReDraw = False

            For i As Integer = 0 To riRtnCnt - 1
                .MaxCols += 1
                li_Col = .MaxCols

                .Col = li_Col
                .ColID = "POPDATA" + i.ToString

                ' 컬럼의 길이 설정 
                If ralHeader.Count = 2 Then
                    Select Case i
                        Case 0
                            .set_ColWidth(.Col, 16)
                        Case 1
                            .set_ColWidth(.Col, 25)
                        Case Else
                            .set_ColWidth(.Col, 15)
                    End Select
                ElseIf ralHeader.Count = 3 Then
                    Select Case i
                        Case 0
                            .set_ColWidth(.Col, 9)
                        Case 1
                            .set_ColWidth(.Col, 8)
                        Case 2
                            .set_ColWidth(.Col, 25)
                        Case Else
                            .set_ColWidth(.Col, 15)
                    End Select
                Else
                    .ScrollBars = FPSpreadADO.ScrollBarsConstants.ScrollBarsBoth
                    .set_ColWidth(.Col, 13)
                End If

                .Row = 0

                If ralHeader.Count > i Then
                    .Text = ralHeader(i)
                Else
                    .ColHidden = False
                End If

            Next
            .ReDraw = True
        End With
    End Sub

    ' 스프레드의 컬럼 데이터 세팅
    Private Sub sb_setData(ByVal rSpd As AxFPSpreadADO.AxfpSpread, ByVal riColCnt As Integer, ByVal rdt As DataTable)
        Dim sFn As String = "Private Sub sb_setData(ByVal rSpd As AxFPSpreadADO.AxfpSpread, ByVal riColCnt As Integer, ByVal rdt As DataTable)"

        Try
            If rdt.Rows.Count < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조회된 자료가 없습니다.")
                txtSearch.Focus()
                Return
            End If

            With rSpd
                .ReDraw = False
                .MaxRows = 0

                For i As Integer = 0 To rdt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    For k As Integer = 0 To riColCnt - 1
                        .Col = .GetColFromID("POPDATA" + k.ToString) : .Text = rdt.Rows(i).Item(k).ToString
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    Next

                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        Finally
            rSpd.ReDraw = True
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        malRtn = New ArrayList

        Me.Close()
    End Sub

    ' 멀티 체크 팝업이 아닐경우 멀티 체크 방지 및 체크박스 외 클릭시에도 체크 되도록
    Private Sub spdSearchList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdSearchList.ClickEvent
        Dim ls_Chk As String

        With spdSearchList
            .Row = e.row
            .Col = .GetColFromID("chk") : ls_Chk = .Text

            .Col = e.col

            If .ColID <> "chk" Then
                If ls_Chk = "1" Then
                    .Col = .GetColFromID("chk") : .Value = "0"c
                Else
                    .Col = .GetColFromID("chk") : .Value = "1"c
                End If
            End If

            If ls_Chk <> "1"c Then
                miCurRow = e.row
            Else
                miCurRow = -1
            End If

            If msMulty = "N"c Then
                If miChkRow <> -1 Then
                    .Row = miChkRow
                    .Col = .GetColFromID("chk") : .Value = "0"c
                End If
            End If

            miChkRow = e.row

        End With

    End Sub

    ' 선택 버튼 클릭
    Private Sub btnChoose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChoose.Click
        Dim lal_rtnStr As New ArrayList
        Dim ls_chk As String
        Dim li_cnt As Integer = 0
        Dim sFn As String = "Private Sub btnChoose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChoose.Click"

        Try
            ' 한 건 선택 팝업일 경우
            If msMulty = "N"c Then
                If miCurRow = -1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "선택된 항목이 없습니다.")
                    Return
                Else
                    For i As Integer = 1 To miColCnt + 1
                        With spdSearchList
                            .Row = miCurRow
                            .Col = i

                            ' 선택 컬럼의 항목은 배열리스트에 넘기지 않는다
                            If i <> .GetColFromID("chk") Then
                                lal_rtnStr.Add(.Text)
                            End If

                        End With

                    Next

                    malRtn = lal_rtnStr

                    Me.Close()
                End If

            Else
                With spdSearchList
                    Dim lal_RtnData As New ArrayList

                    For i As Integer = 0 To .MaxRows
                        .Row = i
                        .Col = .GetColFromID("chk") : ls_chk = .Text

                        If ls_chk = "1" Then
                            li_cnt += 1
                            Dim lcls_RtnData As New clsRtnData

                            For u As Integer = 0 To 11
                                If miColCnt + 2 > u Then
                                    spdSearchList.Col = u
                                    lal_RtnData.Add(.Text)
                                Else
                                    lal_RtnData.Add(" "c)
                                End If
                            Next

                            ' 0, 1 번째 컬럼은 로우넘버와 체크박스이기에 넘기지 않는다.
                            lcls_RtnData.RTNDATA0 = lal_RtnData(2)
                            lcls_RtnData.RTNDATA1 = lal_RtnData(3)
                            lcls_RtnData.RTNDATA2 = lal_RtnData(4)
                            lcls_RtnData.RTNDATA3 = lal_RtnData(5)
                            lcls_RtnData.RTNDATA4 = lal_RtnData(6)
                            lcls_RtnData.RTNDATA5 = lal_RtnData(7)
                            lcls_RtnData.RTNDATA6 = lal_RtnData(8)
                            lcls_RtnData.RTNDATA7 = lal_RtnData(9)
                            lcls_RtnData.RTNDATA8 = lal_RtnData(10)
                            lcls_RtnData.RTNDATA9 = lal_RtnData(11)

                            lal_rtnStr.Add(lcls_RtnData)
                        End If
                    Next

                    If li_cnt < 1 Then
                        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "선택된 항목이 없습니다.")
                        Return
                    Else
                        malRtn = lal_rtnStr
                    End If
                End With
            End If
            Me.Close()
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    Private Sub FGCDHELP99_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        DS_FormDesige.sbInti(Me)
        Me.txtSearch.Focus()
    End Sub

    Private Sub FGCDHELP99_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnChoose_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim sFn As String = "Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"

        Dim objFn As New CDHELP.FGCDHELPFN
        Dim sSql As String = ""
        Dim dt As DataTable
        Dim alParm As New ArrayList

        If Me.txtSearch.Text.Length < 1 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "검색조건을 입력 후 조회 하시기 바랍니다.")
            Me.txtSearch.Focus()
            Return
        End If

        For i As Integer = 0 To malArg.Count - 1
            alParm.Add(Me.txtSearch.Text)
        Next

        ' 쿼리 스트링 얻어옴
        sSql = CallByName(objFn, msFnStr, CallType.Method, cboGubun.SelectedIndex)

        ' 데이터 테이블 가져옴
        dt = CDHELP.FGCDHELPFN.fn_RtnDataList(sSql, alParm)

        ' 데이터 스프레드에 뿌리기
        sb_setData(spdSearchList, miColCnt, dt)

        Me.txtSearch.Focus()

    End Sub

    Private Sub spdSearchList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdSearchList.DblClick
        Dim sFn As String = "Private Sub btnChoose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChoose.Click"
        Dim lal_rtnStr As New ArrayList

        Try
            ' 한 건 선택 팝업일 경우
            If msMulty = "N"c Then
                For i As Integer = 1 To miColCnt + 1
                    With spdSearchList
                        .Row = e.row
                        .Col = i

                        If i <> .GetColFromID("chk") Then
                            lal_rtnStr.Add(.Text)
                        End If

                    End With

                Next

                malRtn = lal_rtnStr

                Me.Close()
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "C"c, "선택할 항목을 체크 한 후 선택 버튼을 누르시기 바랍니다.")
                Return
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Private Sub txtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        txtSearch.SelectAll()
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim ls_Search As String = txtSearch.Text

        If ls_Search.Length() < 1 Then Return

        If e.KeyCode = Keys.Enter Then
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub


End Class

#Region " 팝업 멀티 선택 리턴 데이터 구조체 "
Public Class clsRtnData
    Public RTNDATA0 As String ' 리턴데이터1
    Public RTNDATA1 As String
    Public RTNDATA2 As String
    Public RTNDATA3 As String
    Public RTNDATA4 As String
    Public RTNDATA5 As String
    Public RTNDATA6 As String
    Public RTNDATA7 As String
    Public RTNDATA8 As String
    Public RTNDATA9 As String
End Class
#End Region