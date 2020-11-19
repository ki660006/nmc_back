Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst
Public Class FGF11_S02

    Public msTestCd As String = ""
    Public msSpcCd As String = ""
    Public marrlist As New ArrayList
    Private mo_DAF As New LISAPP.APP_F_TEST

    Private Const msFile As String = "File : FGF11_S02.vb, Class : FGF11_S02" + vbTab
    Public Sub New(ByVal rsTestcd As String, ByVal rsSpccd As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        msTestCd = rsTestcd
        msSpcCd = rsSpccd

    End Sub

    Private Sub FGF11_S02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sFn As String = "Private Sub FGF11_S02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
        Try
            Dim dt As DataTable = New DataTable
            Dim dt2 As DataTable = New DataTable
            Dim iCol As Integer = 0

            dt = mo_DAF.GetTestInfo_detail2(msTestCd, msSpcCd, "FGF11_S02")

            dt2 = mo_DAF.GetTestInfo_detail3(msTestCd, msSpcCd)

            Me.txtTestcd.Text = msTestCd
            Me.TxtSpccd.Text = msSpcCd
            If dt2.Rows.Count > 0 Then
                Me.TxtTnmd.Text = dt2.Rows(0).Item("tnmd").ToString
                Me.Txtspcnmd.Text = dt2.Rows(0).Item("spcnmd").ToString
            End If

            '스프레드 초기화
            sbInitialize_spdDTest()

            If dt.Rows.Count < 1 Then Return

            With spdDTest
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbInitialize_spdDTest()
        Dim sFn As String = "Private Sub sbInitializeControl_spdDTest()"

        Try
            With spdDTest
                .ReDraw = False : .MaxRows = 0 : .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnDTDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDTDel.Click, btnINSERT.Click
        Dim sFn As String = "Private Sub btnDTDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDTDel.Click, btnRTDel.Click"

        Try
            If CType(sender, Windows.Forms.Button).Name.StartsWith("btnD") Then
                sbDelDtestlist()
                FGF11_S02_Load(Nothing, Nothing)
                'sbDelCheckedRow(spdDTest, 1)
            ElseIf CType(sender, Windows.Forms.Button).Name.StartsWith("btnI") Then
                sbInsertTestcd()
                FGF11_S02_Load(Nothing, Nothing)
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
    Private Sub sbDelDtestlist()
        Dim CTESTLIST As New DTESTLIST : Dim arrTList As New ArrayList
        With spdDTest
            For i As Integer = 1 To .MaxRows
                .Row = i
                .Col = .GetColFromID("chk")
                If .Text = "1" Then
                    CTESTLIST = New DTESTLIST
                    .Col = .GetColFromID("testcd") : CTESTLIST.TESTCD = .Text
                    .Col = .GetColFromID("spccd") : CTESTLIST.SPCCCD = .Text

                    arrTList.Add(CTESTLIST)

                End If

            Next
        End With
        
        If mo_DAF.DELDTEST(arrTList, msTestCd, msSpcCd) = True Then
            MsgBox("선택된 항목이 정상 삭제되었습니다.!!", MsgBoxStyle.Information)
        End If

    End Sub
    Private Sub sbInsertTestcd()
        Dim sFn As String = "Handles btnReg_dispseql.ButtonClick"

        Dim frmChild As Windows.Forms.Form
        Dim sDispSeqGbn As String = "L"


        frmChild = New FGF11_S03(txtTestcd.Text, TxtSpccd.Text)

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = System.Windows.Forms.FormWindowState.Normal
        frmChild.Activate()
        frmChild.ShowDialog()

    End Sub
    Private Sub sbDelCheckedRow(ByVal aspd As AxFPSpreadADO.AxfpSpread, ByVal aiChkCol As Integer)
        Dim sFn As String = "Private Sub sbDelCheckedRow(ByVal aspd As AxFPSpreadADO.AxfpSpread, ByVal aiChkCol As Integer)"

        Try
            Dim sChk As String = ""

            With aspd
                For i As Integer = 1 To .MaxRows
                    For j As Integer = i To .MaxRows
                        .Col = aiChkCol : .Row = j : sChk = .Text

                        If sChk = "1" Then
                            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                            .MaxRows -= 1
                            i = j - 1

                            Exit For
                        End If
                    Next

                    If i > .MaxRows Then
                        Exit For
                    End If
                Next
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnMaxRowAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaxRowAdd.Click

        Try
            spdDTest.MaxRows += 1
        Catch ex As Exception

        End Try
    End Sub
End Class