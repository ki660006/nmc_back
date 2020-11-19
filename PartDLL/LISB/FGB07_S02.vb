Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB07_S02
    Private Const msFile As String = "File : FGC31.vb, Class : FGC31" & vbTab

    Private ms_tns_no As String = ""

    Private Sub sbDisplay_Data()
        Try
            Dim dt As DataTable = CGDA_BT.fnGet_BldQntChg_List(ms_tns_no)
            If dt.Rows.Count < 1 Then Return

            With Me.spdList
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("tnsjubsuno") : .Text = dt.Rows(ix).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("comnm") : .Text = dt.Rows(ix).Item("comnm").ToString
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                    .Col = .GetColFromID("sexage") : .Text = dt.Rows(ix).Item("sexage").ToString
                    .Col = .GetColFromID("deptnm") : .Text = dt.Rows(ix).Item("deptnm").ToString
                    .Col = .GetColFromID("wardroom") : .Text = dt.Rows(ix).Item("wardroom").ToString
                    .Col = .GetColFromID("doctornm") : .Text = dt.Rows(ix).Item("doctornm").ToString
                    .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString
                    .Col = .GetColFromID("comnm_chg") : .Text = dt.Rows(ix).Item("comnm_chg").ToString
                    .Col = .GetColFromID("owngbn") : .Text = dt.Rows(ix).Item("owngbn").ToString
                    .Col = .GetColFromID("iogbn") : .Text = dt.Rows(ix).Item("iogbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = dt.Rows(ix).Item("fkocs").ToString
                    .Col = .GetColFromID("ordinfo") : .Text = dt.Rows(ix).Item("ordinfo").ToString
                    .Col = .GetColFromID("spccd_chg") : .Text = dt.Rows(ix).Item("spccd").ToString
                    .Col = .GetColFromID("comordcd_chg") : .Text = dt.Rows(ix).Item("comordcd_chg").ToString
                    .Col = .GetColFromID("sugacd_chg") : .Text = dt.Rows(ix).Item("sugacd_chg").ToString
                    .Col = .GetColFromID("usrdept") : .Text = dt.Rows(ix).Item("usrdept").ToString
                    .Col = .GetColFromID("chk") : .Text = ""
                Next

            End With
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub


    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsTnsNo As String)
        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ms_tns_no = rsTnsNo
    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Try
            With Me.spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text

                    If sChk = "1" Then

                        Dim sOrdInfo As String = ""
                        Dim sUsrDept As String = ""

                        Dim stu As New STU_TNSCHG

                        .Row = iRow
                        .Col = .GetColFromID("ordinfo") : sOrdInfo = .Text
                        .Col = .GetColFromID("usrdept") : sUsrDept = .Text

                        .Col = .GetColFromID("regno") : stu.REGNO = .Text
                        .Col = .GetColFromID("ordinfo") : sOrdInfo = .Text
                        .Col = .GetColFromID("regno") : stu.REGNO = .Text
                        .Col = .GetColFromID("fkocs") : stu.IOFLAG = .Text.Substring(0, 1)
                        .Col = .GetColFromID("fkocs") : stu.EXECPRCPUNIQNO = .Text.Split("/"c)(3)
                        .Col = .GetColFromID("comordcd_chg") : stu.ORDCD_CHG = .Text
                        .Col = .GetColFromID("spccd_chg") : stu.SPCCD_CHG = .Text
                        .Col = .GetColFromID("sugacd_chg") : stu.SUGACD_CHG = .Text
                        .Col = .GetColFromID("tnsjubsuno") : stu.TNSNO = .Text.Replace("-", "")

                        stu.ADMDATE = sOrdInfo.Split("/"c)(0)           ' 내원일자
                        stu.CRETNO = sOrdInfo.Split("/"c)(1)            ' 내원 생성번호
                        stu.MEDAMTESTMYN = sOrdInfo.Split("/"c)(2)      ' 진찰료산정여부

                        stu.ORDDATE = sOrdInfo.Split("/"c)(3)           ' 처방일자
                        stu.ORDNO = sOrdInfo.Split("/"c)(4)             ' 처방번호
                        stu.ORDHISTNO = sOrdInfo.Split("/"c)(5)         ' 처방번호 his

                        If stu.IOFLAG = "I" Then
                            stu.ORDSTATCD = "100"                              ' 처방상태코드
                        Else
                            stu.ORDSTATCD = "000"                              ' 처방상태코드
                        End If
                        stu.BLDNO_CHG = ""                              ' 변경 혈액번호
                        stu.DEPTCD_USR = sUsrDept.Split("/"c)(0)        ' 부서코드
                        stu.DEPTNM_USR = sUsrDept.Split("/"c)(0)        ' 부서명

                        Dim sRet As String = (New WEBSERVER.CGWEB_B).ExecuteDo_Bldqnt_chg(stu, "lis")

                        If sRet.StartsWith("00") Then
                        Else
                            Throw (New Exception(sRet.Substring(2)))
                        End If
                    End If
                Next
            End With

            Me.Close()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGB07_S02_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Me.spdList.MaxRows = 0
            sbDisplay_Data()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try

    End Sub


End Class