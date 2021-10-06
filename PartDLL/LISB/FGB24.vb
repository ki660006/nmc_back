' 수술환자 확정 조회

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Imports OCSAPP.OcsLink
Imports LISAPP.APP_BT

Public Class FGB24

    Private Sub sbDisplay_Data()
        Try
            Dim dt As DataTable = SData.fnGet_OpInfo_List(Me.dtpOpdt.Text.Replace("-", ""))
            Dim rsRegno As String
            Dim dt_bld As DataTable : Dim dtAnti As DataTable

            With Me.spdList
                .ReDraw = False
                .MaxRows = 0
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("oproom") : .Text = dt.Rows(ix).Item("oproom").ToString        '-- 수술실
                    .Col = .GetColFromID("opseqno") : .Text = dt.Rows(ix).Item("opseqno").ToString      '-- 수술순번
                    .Col = .GetColFromID("oprating") : .Text = dt.Rows(ix).Item("oprating").ToString    '-- 수술구분
                    .Col = .GetColFromID("optmkind") : .Text = dt.Rows(ix).Item("optmkind").ToString    '-- 신청구분
                    .Col = .GetColFromID("optm") : .Text = dt.Rows(ix).Item("optm").ToString            '-- 수술시간
                    .Col = .GetColFromID("deptcd") : .Text = dt.Rows(ix).Item("meddept").ToString       '-- 진료과코드
                    .Col = .GetColFromID("opdrnm") : .Text = dt.Rows(ix).Item("opdrnm").ToString        '-- 집도의
                    '20210106 jhs 주치의 명 컬럼 추가
                    .Col = .GetColFromID("medispclnm") : .Text = dt.Rows(ix).Item("medispclnm").ToString '-- 주치의
                    '---------------------------------------
                    .Col = .GetColFromID("iogbn") : .Text = dt.Rows(ix).Item("patsect").ToString        '-- 입외구분
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("patno").ToString          '-- 등록번호
                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString          '-- 환자명
                    rsRegno = dt.Rows(ix).Item("patno").ToString
                    .Col = .GetColFromID("opcnt") : .Text = dt.Rows(ix).Item("opcnt").ToString          '-- 수술회차
                    .Col = .GetColFromID("sexage") : .Text = dt.Rows(ix).Item("sexage").ToString        '-- 성별/나이
                    .Col = .GetColFromID("wardroom") : .Text = dt.Rows(ix).Item("wardroom").ToString    '-- 병동/병실
                    .Col = .GetColFromID("opname") : .Text = dt.Rows(ix).Item("opname").ToString        '-- 수술명
                    .Col = .GetColFromID("anethdr") : .Text = dt.Rows(ix).Item("anethcd").ToString      '-- 마취방법
                    .Col = .GetColFromID("opcancel") : .Text = dt.Rows(ix).Item("opcancel").ToString    '-- 취소여부
                    .Col = .GetColFromID("location") : .Text = dt.Rows(ix).Item("opstat").ToString      '-- 상황

                    dt_bld = CGDA_BT.fnGet_ABORh(rsRegno)
                    dtAnti = CGDA_BT.fnGet_AntibodyTest_Rst(rsRegno) 'AntiBody Screen 검사 , 결과일 추가 (2019-07-02)

                    If dt_bld.Rows.Count > 0 Then
                        .Col = .GetColFromID("aborh") : .Text = dt_bld.Rows(0).Item("aborh").ToString
                    End If

                    If dtAnti.Rows.Count > 0 Then
                        .Col = .GetColFromID("rst") : .Text = dtAnti.Rows(0).Item("rst").ToString
                        .Col = .GetColFromID("rstdt") : .Text = dtAnti.Rows(0).Item("rstdt").ToString
                    End If

                    If dt.Rows(ix).Item("oprating").ToString.StartsWith("응급") Then
                        .Col = -1 : .ForeColor = Color.Red
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Me.spdList.ReDraw = True
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E", ex.Message)
        End Try
    End Sub

    Private Sub FGB24_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB24_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
        Me.dtpOpdt.Value = Now
        Me.spdList.MaxRows = 0
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdList.MaxRows = 0
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.Cursor = Cursors.WaitCursor

        sbDisplay_Data()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sTime As String = Format(Now, "yyyyMMdd")

        With Me.spdList
            .ReDraw = False

            .MaxRows += 3
            .InsertRows(1, 3)

            .Col = 8
            .Row = 1
            .Text = "수술환자 확정 리스트"
            .FontBold = True
            .FontSize = 15
            .ForeColor = System.Drawing.Color.Black

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 3 : .Row2 = 3
            .Clip = sColHeaders

            '.InsertRows(4, 1)

            If Me.spdList.ExportToExcel("c:\수술환자확정조회_" & sTime & ".xls", "TransfList", "") Then
                Process.Start("c:\수술환자확정조회_" & sTime & ".xls")
            End If

            .DeleteRows(1, 3)
            .MaxRows -= 3

            .ReDraw = True
        End With
    End Sub

    Private Sub FGB24_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub spdList_DblClick(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick
        With spdList

            .Row = .ActiveRow
            .Col = .GetColFromID("regno")
            Dim sRegno As String = .Text

            Dim obj As New FGB06_S01(sRegno)
            obj.Show()

        End With
    End Sub
End Class