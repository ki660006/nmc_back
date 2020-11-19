
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Imports LISAPP.APP_J
Imports LISAPP.APP_J.TkFn

Public Class FGABNQUERY
    Inherits System.Windows.Forms.Form
    Public msRegNo As String = ""
    Private Const msFile As String = "File : FGABNQUERY.vb, Class : AxAckResult" + vbTab

    Public Sub New(ByVal rsRegNo As String)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        msRegNo = rsRegNo

        sbFormInitialize()

    End Sub

    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub fnFormInitialize()"
        Dim objCommFn As New Fn

        Try

            spdAbn.MaxRows = 0

            ' 로그인정보 설정
            'Me.lblUserId.Text = USER_INFO.USRID
            'Me.lblUserNm.Text = USER_INFO.USRNM

            Dim dt As DataTable = fnGet_Abn_List_regno(msRegNo)

            sbDisplaySpread(dt)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub sbDisplaySpread(ByVal rsdt As DataTable)

        If rsdt.Rows.Count <= 0 Then Return

        With spdAbn
            .MaxRows = rsdt.Rows.Count
            For i As Integer = 0 To rsdt.Rows.Count - 1
                .Row = i + 1
                .Col = .GetColFromID("patnm") : .Text = rsdt.Rows(i).Item("patnm").ToString.Trim
                .Col = .GetColFromID("regno") : .Text = rsdt.Rows(i).Item("regno").ToString.Trim
                .Col = .GetColFromID("sexage") : .Text = rsdt.Rows(i).Item("sexage").ToString.Trim
                .Col = .GetColFromID("bcno") : .Text = rsdt.Rows(i).Item("bcno").ToString.Trim
                .Col = .GetColFromID("cmtcont") : .Text = rsdt.Rows(i).Item("cmtcont").ToString.Trim
                .Col = .GetColFromID("tnmd") : .Text = rsdt.Rows(i).Item("tnmd").ToString.Trim
                .Col = .GetColFromID("spcnmd") : .Text = rsdt.Rows(i).Item("spcnmd").ToString.Trim
                .Col = .GetColFromID("viewrst") : .Text = rsdt.Rows(i).Item("viewrst").ToString.Trim
                .Col = .GetColFromID("fndt") : .Text = rsdt.Rows(i).Item("fndt").ToString.Trim
                .Col = .GetColFromID("regdt") : .Text = rsdt.Rows(i).Item("regdt").ToString.Trim
                .Col = .GetColFromID("regid") : .Text = rsdt.Rows(i).Item("regid").ToString.Trim
                .Col = .GetColFromID("fnid") : .Text = rsdt.Rows(i).Item("fnid").ToString.Trim
                .set_RowHeight(i + 1, .get_MaxTextRowHeight(i + 1))
            Next
        End With
    End Sub
End Class