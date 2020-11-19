Imports Oracle.DataAccess.Client

Imports COMMON.CommFN

Public Class FGPOPUPST_PBS_S01
    Private Const msFile As String = "File : FGPOPUPST_VRST.vb, Class : FGPOPUPST_VRST" & vbTab
    Private msTestCd As String = ""
    Private m_dbCn As OracleConnection

    Private Sub sbDisplay_Data()
        Try
            Dim dt As DataTable = DA_ST_PBS.fnGet_EtcInfo(msTestCd, m_dbCn)

            If dt.Rows.Count < 1 Then Return

            Dim sBuf() As String = dt.Rows(0).Item("etcinfo").ToString.Split("|"c)

            With spdFlag
                .MaxRows = sBuf.Length
                For ix As Integer = 0 To sBuf.Length - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("gbn") : .Text = sBuf(ix).Split("^"c)(0)
                    .Col = .GetColFromID("flag") : .Text = sBuf(ix).Split("^"c)(1)
                    .Col = .GetColFromID("cmt") : .Text = sBuf(ix).Split("^"c)(2)
                Next
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub spdFlag_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdFlag.KeyDownEvent

        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        With spdFlag
            If .ActiveRow <> .MaxRows Or .ActiveCol <> .MaxCols Then Return

            .MaxRows += 1
        End With

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try

            Dim sValue As String = ""

            With spdFlag
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("flag") : Dim sFlag As String = .Text
                    .Col = .GetColFromID("cmt") : Dim sCmt As String = .Text
                    .Col = .GetColFromID("gbn") : Dim sGbn As String = .Text

                    If sFlag <> "" Then
                        sValue += sGbn + "^" + sFlag + "^" + sCmt + "|"
                    End If
                Next
            End With

            If DA_ST_PBS.fnExec_Insert(msTestCd, sValue, m_dbcn) Then
                Me.Close()
            Else
                MsgBox("데이타를 저장하지 못 했습니다.!!", MsgBoxStyle.Critical, Me.Text)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
    End Sub

    Public Sub New(ByVal rsTestCd As String, ByVal r_dbCn As OracleConnection)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        msTestCd = rsTestCd
        m_dbCn = r_dbCn
    End Sub

    Private Sub FGPOPUPST_PBS_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Windows.Forms.Keys.Escape Then
            btnClose_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then
            btnSave_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub FGPOPUPST_PBS_S01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        sbDisplay_Data()
    End Sub

    Private Sub mnuAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAdd.Click
        With spdFlag
            .Row = .ActiveRow
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow
        End With
    End Sub

    Private Sub mnuDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDel.Click
        With spdFlag
            .Row = .ActiveRow
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1
        End With
    End Sub

End Class
