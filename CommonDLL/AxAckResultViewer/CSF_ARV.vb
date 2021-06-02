Imports System.Windows.Forms
Imports System.Drawing

Imports Common.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class ChartInfo
    Public sRstDte As String = ""
    Public sRstVal As String = ""
End Class


'< add freety 2007/04/24
Public Class SF_Disp_RstInfo
    Private Const mc_sFile As String = "File : CSHAREFN.vb, Class : SF_Disp_RstInfo" & vbTab

    '채혈일시, 접수일시, 보고일시
    Public lblCollDt As Windows.Forms.Label
    Public lblTkDt As Windows.Forms.Label
    Public lblFnDt As Windows.Forms.Label

    'spdPatInfo
    Public spdPatInfo As AxFPSpreadADO.AxfpSpread

    'spdRst
    Public spdRst As AxFPSpreadADO.AxfpSpread

    '검체번호, 작업번호, 바코드번호
    Public txtBcNo As Windows.Forms.TextBox
    Public txtWkNo As Windows.Forms.TextBox
    Public txtPrtBcNo As Windows.Forms.TextBox

    '검체명, 진단명, 투여약물, 의뢰의사Remark
    Public lblSpcNm As Windows.Forms.Label
    Public lblDiagNm As Windows.Forms.Label
    Public lblDrugNm As Windows.Forms.Label
    Public lblRemark As Windows.Forms.Label

    '결과상태, 결과저장, 중간보고, 최종보고
    Public lblSampleStatus As Windows.Forms.Label
    Public lblReg As Windows.Forms.Label
    Public lblMW As Windows.Forms.Label
    Public lblFN As Windows.Forms.Label

    Public tooltip As Windows.Forms.ToolTip

    Public dt_RstUsr As DataTable

    Private mbAdd_DblClick As Boolean = False


    Public Sub DisplayInit()
        Dim sFn As String = "DisplayInit"

        Try
            '채혈일시, 접수일시, 보고일시
            Me.lblCollDt.Text = ""
            Me.lblTkDt.Text = ""
            Me.lblFnDt.Text = ""

            'spdPatInfo
            With Me.spdPatInfo
                .ClearRange(1, 1, .MaxCols, 1, True)
            End With

            If mbAdd_DblClick = False Then
                AddHandler spdPatInfo.DblClick, AddressOf sbProc_spdPatInfo_DblClick

                mbAdd_DblClick = True
            End If

            '검체번호, 작업번호, 바코드번호
            Me.txtBcNo.Text = ""
            Me.txtWkNo.Text = ""
            Me.txtPrtBcNo.Text = ""

            '검체명, 진단명, 투여약물, 의뢰의사Remark
            Me.lblSpcNm.Text = ""
            Me.lblDiagNm.Text = ""
            Me.lblDrugNm.Text = ""
            Me.lblRemark.Text = ""

            '결과상태, 결과저장, 중간보고, 최종보고
            Me.lblSampleStatus.Text = ""
            Me.lblReg.Text = ""
            Me.lblMW.Text = ""
            Me.lblFN.Text = ""

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub DisplayInit(ByVal rsType As String)
        Dim sFn As String = "DisplayInit"

        Try
            If rsType = "P" Then
                '채혈일시, 접수일시, 보고일시
                Me.lblCollDt.Text = ""
                Me.lblTkDt.Text = ""
                Me.lblFnDt.Text = ""

                'spdPatInfo
                With Me.spdPatInfo
                    .ClearRange(1, 1, .MaxCols, 1, True)
                End With

                '검체번호, 작업번호, 바코드번호
                Me.txtBcNo.Text = ""
                Me.txtWkNo.Text = ""
                Me.txtPrtBcNo.Text = ""

                '검체명, 진단명, 투여약물, 의뢰의사Remark
                Me.lblSpcNm.Text = ""
                Me.lblDiagNm.Text = ""
                Me.lblDrugNm.Text = ""
                Me.lblRemark.Text = ""
            End If

            If rsType = "R" Then
                '결과상태, 결과저장, 중간보고, 최종보고
                Me.lblSampleStatus.Text = ""
                Me.lblReg.Text = ""
                Me.lblMW.Text = ""
                Me.lblFN.Text = ""
            End If

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub Display_BcNo_Exact_Item(ByVal rsBcNo As String)
        If Not rsBcNo.Length = "yyyyMMddXX1234n".Length Then Return

        Try
            spdRst.ReDraw = False

            With spdRst
                For i As Integer = 1 To .MaxRows
                    .Row = i

                    .Col = .GetColFromID("검체번호") : Dim sBcNo As String = .Text
                    .Col = .GetColFromID("다중라인") : Dim sMultiLine As String = .Text

                    If Not sBcNo = rsBcNo And Not sMultiLine = "1" Then
                        .Col = .GetColFromID("검사항목명")
                        .BackColor = Drawing.Color.WhiteSmoke

                        .Col = .GetColFromID("실제결과")
                        .Lock = True
                    End If
                Next
            End With

        Catch ex As Exception

        Finally
            spdRst.ReDraw = True

        End Try
    End Sub

    Public Sub Display_RegNm(ByVal rsBcNo As String)
        Dim sFn As String = "Sub Display_RegNm()"

        Try
            Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_RstUsrInfo(rsBcNo)

            Dim sID As String = ""
            Dim sNM As String = ""
            Dim sDT As String = ""

            DisplayInit("R")

            Dim a_dr As DataRow()

            a_dr = dt.Select("rstflg >= '1'", "regdt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("regid").ToString()
                sNM = a_dr(i - 1).Item("regnm").ToString()
                sDT = a_dr(i - 1).Item("regdt").ToString()

                If Not sID + sNM + sDT = "" Then
                    Me.lblSampleStatus.Text = "결과저장"
                    Me.lblReg.Text = sDT + vbCrLf + sNM

                    Exit For
                End If
            Next

            a_dr = dt.Select("rstflg >= '2'", "mwdt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("mwid").ToString()
                sNM = a_dr(i - 1).Item("mwnm").ToString()
                sDT = a_dr(i - 1).Item("mwdt").ToString()

                If Not sID + sNM + sDT = "" Then
                    Me.lblSampleStatus.Text = "중간보고"
                    Me.lblMW.Text = sDT + vbCrLf + sNM

                    Exit For
                End If
            Next

            a_dr = dt.Select("rstflag = '3'", "fndt desc")

            For i As Integer = 1 To a_dr.Length
                sID = a_dr(i - 1).Item("fnid").ToString()
                sNM = a_dr(i - 1).Item("fnnm").ToString()
                sDT = a_dr(i - 1).Item("fndt").ToString()

                If Not sID + sNM + sDT = "" Then
                    If Me.lblFnDt.Text = "" Then
                        Me.lblSampleStatus.Text = "예비보고"
                    Else
                        Me.lblSampleStatus.Text = "최종보고"
                    End If

                    Me.lblFN.Text = sDT + vbCrLf + sNM

                    Exit For
                End If
            Next

            If Me.lblSampleStatus.Text = "최종보고" Then
                Me.lblSampleStatus.BackColor = Drawing.Color.FromArgb(128, 128, 255)
                Me.lblSampleStatus.ForeColor = Drawing.Color.White
            Else
                Me.lblSampleStatus.BackColor = Drawing.Color.FromArgb(255, 192, 128)
                Me.lblSampleStatus.ForeColor = Drawing.Color.Black
            End If

            dt_RstUsr = dt.Copy

        Catch ex As Exception
            Fn.log(mc_sFile & sFn, Err)
            MsgBox(mc_sFile & sFn & vbCrLf & ex.Message)

        End Try
    End Sub

    Public Sub Display_RegNm_Test(ByVal rsTClsCd As String)
        Dim sFn As String = "Sub Display_RegNm_Test()"

        Try
            Dim sID As String = ""
            Dim sNM As String = ""
            Dim sDT As String = ""

            '결과저장, 중간보고, 최종보고
            Me.lblReg.Text = ""
            Me.lblMW.Text = ""
            Me.lblFN.Text = ""

            Dim a_dr As DataRow()

            a_dr = dt_RstUsr.Select("tclscd = '" + rsTClsCd + "'")

            If a_dr.Length < 1 Then Return

            Dim sRstFlag As String = a_dr(0).Item("rstflag").ToString()

            If sRstFlag = "" Then Return

            For i As Integer = 1 To Convert.ToInt32(sRstFlag)
                If i = 1 Then
                    sID = a_dr(0).Item("regid").ToString()
                    sNM = a_dr(0).Item("regnm").ToString()
                    sDT = a_dr(0).Item("regdt").ToString()

                    If Not sID + sNM + sDT = "" Then
                        Me.lblReg.Text = sDT + vbCrLf + sNM
                    End If
                ElseIf i = 2 Then
                    sID = a_dr(0).Item("mwid").ToString()
                    sNM = a_dr(0).Item("mwnm").ToString()
                    sDT = a_dr(0).Item("mwdt").ToString()

                    If Not sID + sNM + sDT = "" Then
                        Me.lblMW.Text = sDT + vbCrLf + sNM
                    End If
                ElseIf i = 3 Then
                    sID = a_dr(0).Item("fnid").ToString()
                    sNM = a_dr(0).Item("fnnm").ToString()
                    sDT = a_dr(0).Item("fndt").ToString()

                    If Not sID + sNM + sDT = "" Then
                        Me.lblFN.Text = sDT + vbCrLf + sNM
                    End If
                End If
            Next

        Catch ex As Exception
            Fn.log(mc_sFile & sFn, Err)
            MsgBox(mc_sFile & sFn & vbCrLf & ex.Message)

        End Try
    End Sub

    Public Function Find_Diff_ABO_Type() As Boolean
        Dim sFn As String = "Find_Diff_ABO_Type"

        Try
            Dim iRow As Integer = 0
            Dim iDiff As Integer = 0

            Dim sRstCur As String = ""
            Dim sRstPre As String = ""

            With spdRst
                iRow = .SearchCol(.GetColFromID("혈액은행검사종류"), 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                If iRow < 1 Then Return False

                For i As Integer = 1 To .MaxRows
                    .Row = i

                    .Col = .GetColFromID("혈액은행검사종류")

                    If Not .Text = "" Then
                        .Col = .GetColFromID("실제결과")
                        sRstCur = .Text.Trim

                        .Col = .GetColFromID("이전실제결과S")
                        sRstPre = .Text.Trim

                        If sRstCur.Length * sRstPre.Length > 0 Then
                            If sRstCur <> sRstPre Then
                                iDiff += 1
                            End If
                        End If
                    End If
                Next

                If iDiff > 0 Then
                    If MsgBox("입력한 혈액혈 결과가 이전 결과와 다릅니다. 계속하시겠습니까?", MsgBoxStyle.Critical Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return False
                End If
            End With

        Catch ex As Exception
            Fn.log(mc_sFile & sFn, Err)
            MsgBox(mc_sFile & sFn & vbCrLf & ex.Message)

        End Try
    End Function

    Private Sub sbProc_spdPatInfo_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        Dim sFn As String = "Sub sbProc_spdPatInfo_DblClick"

        Try
            If e.col < 1 Then Return
            If e.row < 1 Then Return

            Dim spd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

            If Not (e.col = spd.GetColFromID("regno") Or e.col = spd.GetColFromID("idno")) Then Return

            '환자정보 조회 기능 권한
            If USER_SKILL.Authority("R01", 7) = False Then Return

            Dim sRegNo As String = Ctrl.Get_Code(spd, spd.GetColFromID("regno"), 1)

            Me.lblRemark.FindForm.Cursor = Cursors.WaitCursor

            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_Current(sRegNo)

            Me.lblRemark.FindForm.Cursor = Cursors.Default

            If dt.Rows.Count = 0 Then
                MsgBox("OCS에서 환자정보를 찾을 수 없습니다!!", MsgBoxStyle.Information)

                Return
            End If

            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.lblRemark)
            Dim iTop As Integer = Ctrl.menuHeight + Ctrl.FindControlTop(Me.lblRemark) + Me.lblRemark.Height

            Dim patinfo As New PATINFO

            With patinfo
                .Left = iLeft
                .Top = iTop

                .RegNo = dt.Rows(0).Item("regno").ToString()
                .PatNm = dt.Rows(0).Item("patnm").ToString()
                .SexAge = dt.Rows(0).Item("sexage").ToString()
                .IdNo = dt.Rows(0).Item("idno").ToString()

                .OrdDt = dt.Rows(0).Item("orddt").ToString()
                .DeptNm = dt.Rows(0).Item("deptnm").ToString()
                .DoctorNm = dt.Rows(0).Item("doctornm").ToString()
                .WardRoom = dt.Rows(0).Item("wardroom").ToString()

                .Tel = (dt.Rows(0).Item("tel1").ToString() + " / " + dt.Rows(0).Item("tel2").ToString()).Trim
                If .Tel.StartsWith("/") Then .Tel = .Tel.Substring(1)
                If .Tel.EndsWith("/") Then .Tel = .Tel.Substring(0, .Tel.Length - 1)

                .Addr1 = dt.Rows(0).Item("addr1").ToString().Trim
                .Addr2 = dt.Rows(0).Item("addr2").ToString().Trim

                .Display_PatInfo()

                .ShowDialog()
            End With

        Catch ex As Exception
            Fn.log(mc_sFile & sFn, Err)
            MsgBox(mc_sFile & sFn & vbCrLf & ex.Message)

        Finally
            Me.lblRemark.FindForm.Cursor = Cursors.Default

        End Try
    End Sub
End Class

Public Class PrintResult
    Private Const mc_sFile As String = "File : TOTRST01.vb, Class : PrintResult" & vbTab

    '1 point = 1 / 72 inch, 1 inch = 2.5399 cm, 1 Margin(Bounds) point = 1 / 100 inch
    Public Left_Margin_cm As Single = 0
    Public Right_Margin_cm As Single = 0
    Public Top_Margin_cm As Single = 0
    Public Bottom_Margin_cm As Single = 0

    Public UseCustomPaper As Boolean = False
    Public Landscape As Boolean = False

    Public Title As String = ""
    Public Labels As ArrayList = Nothing
    Public Headers As ArrayList = Nothing
    Public Cols As ArrayList = Nothing
    Public Tail As String = ""
    Public PrintDateTime As String = ""

    Public Separator As String = Convert.ToChar(1)

    Public FontSize_Title As Single = 16
    Public FontSize_Between_Title_Header As Single = 10
    Public FontSize_Header As Single = 10
    Public FontSize_Body As Single = 9
    Public FontSize_CharLine As Single = 8.65
    Public FontSize_Tail As Single = 10

    Public PaperSize_Height As Integer = 100
    Public PaperSize_Width As Integer = 100

    Public CharLine As Char = Convert.ToChar(FixedVariable.gsCharLine)
    Public CharLine2 As Char = Convert.ToChar(FixedVariable.gsCharLine2)

    Protected Inch_per_DrawPt As Integer = 72
    Protected DrawPt_per_inch As Single = 1 / 72
    Protected Inch_per_Cm As Single = 2.5399
    Protected Cm_per_inch As Single = 1 / 2.5399
    Protected Inch_per_MarginPt As Integer = 100
    Protected MarginPt_per_inch As Single = 1 / 100
    Protected DrawPt_per_MarginPt As Single = 72 / 100
    Protected MarginPt_per_DrawPt As Single = 100 / 72
    Protected Cm_per_DrawPt As Single = 2.5399 / 72
    Protected DrawPt_per_Cm As Single = 72 / 2.5399
    Protected Cm_per_MarginPt As Single = 2.5399 / 100
    Protected MarginPt_per_Cm As Single = 100 / 2.5399

    Protected p_spd As AxFPSpreadADO.AxfpSpread

    Protected psngX As Single = 0
    Protected psngY As Single = 0
    Protected psngW As Single = 0
    Protected psngH As Single = 0

    Protected psngPrtX As Single = 0
    Protected psngPrtY As Single = 0
    Protected piRow_Body As Integer = 0
    Protected piRow_Start As Integer = 1
    Protected piRow_Body2 As Integer = 0
    Protected piRow_Start2 As Integer = 1

    Protected psSEP As String = " "

    Protected psFontName As String = "굴림체"

    Private mcSEP As Char = Convert.ToChar(1)

    Protected WithEvents p_pd As Drawing.Printing.PrintDocument

    Private m_ppdialog As Windows.Forms.PrintPreviewDialog

    Public mPrtPreview As Boolean  '-- 2007-10-25 YOOEJ ADD

    Dim sss As String = ""
    '20210311 jhs 페이지 넘버
    Private rowNum As Integer = 1
    Private totPages As Integer
    Private nowPage As Integer
    '--------------------------------------

    Public Sub CreatePdialog()
        m_ppdialog = New Windows.Forms.PrintPreviewDialog
    End Sub

    Public Overridable Function Find_Height_Row(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Single
        Dim sFn As String = "Function Find_Height_Row"

        Try
            Dim sngLineHeight As Single = 0

            Dim sLine As String = Ctrl.Get_Code(p_spd, "tnm", riRow)
            Dim sBcNo As String = ""

            Dim iLastSpcInfo As Integer = riRow

            If sLine.StartsWith(CharLine.ToString()) Then
                For i As Integer = riRow + 1 To p_spd.MaxRows
                    sBcNo = Ctrl.Get_Code(p_spd, "bcno", i)

                    If sBcNo.Length > 0 Then
                        Exit For
                    End If

                    iLastSpcInfo = i
                Next
            End If

            sngLineHeight = (New Drawing.Font(psFontName, FontSize_Body)).GetHeight(e.Graphics) * (iLastSpcInfo - riRow + 1)

            Return sngLineHeight

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Function Find_Height_Tail(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
        Dim sFn As String = "Function Find_Height_Tail"

        Try
            Dim sngLineHeight_L As Single = 0
            Dim sngLineHeight_T As Single = 0

            sngLineHeight_L = (New Drawing.Font(psFontName, FontSize_CharLine)).GetHeight(e.Graphics)
            sngLineHeight_T = (New Drawing.Font(psFontName, FontSize_Tail)).GetHeight(e.Graphics)

            Dim iLineCnt As Integer = 0

            If PRG_CONST.Tail_RstReport.IndexOf(vbCrLf) > 0 Then
                Dim sBuf As String = PRG_CONST.Tail_RstReport.Replace(vbCrLf, mcSEP)

                iLineCnt = sBuf.Split(mcSEP).Length
            Else
                iLineCnt = 1
            End If

            Return Convert.ToSingle(sngLineHeight_L + iLineCnt * sngLineHeight_T)

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Function Print(ByVal r_spd As AxFPSpreadADO.AxfpSpread) As Integer
        Dim sFn As String = "Function Print"

        Try
            '20210531 jhs 페이지 초기화
            nowPage = 1
            '-------------------------------
            p_pd = New Drawing.Printing.PrintDocument

            If UseCustomPaper Then
                p_pd.DefaultPageSettings.PaperSize = New Drawing.Printing.PaperSize("Custom01", PaperSize_Width, PaperSize_Height)
            End If

            p_pd.DefaultPageSettings.Landscape = Landscape

            p_spd = r_spd

            piRow_Start = 1

            p_pd.Print()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Sub ShowDialog()
        m_ppdialog.ShowDialog()
    End Sub

    Public Overridable Function PrintPreviewMulti(ByVal r_spd As AxFPSpreadADO.AxfpSpread) As Integer
        Dim sFn As String = "Function PrintPreview"

        Try
            p_pd = New Drawing.Printing.PrintDocument

            If UseCustomPaper Then
                p_pd.DefaultPageSettings.PaperSize = New Drawing.Printing.PaperSize("Custom01", PaperSize_Width, PaperSize_Height)
            End If

            p_pd.DefaultPageSettings.Landscape = Landscape

            'Dim ppdialog As New Windows.Forms.PrintPreviewDialog

            'm_ppdialog = ppdialog

            m_ppdialog.Document = p_pd

            p_spd = r_spd

            piRow_Start = 1

            m_ppdialog.StartPosition = FormStartPosition.CenterParent
            m_ppdialog.Width = Convert.ToInt32(r_spd.Height * 4 / 3)
            m_ppdialog.Height = r_spd.Height

            'ppdialog.ShowDialog()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Function PrintPreview(ByVal r_spd As AxFPSpreadADO.AxfpSpread) As Integer
        Dim sFn As String = "Function PrintPreview"

        Try
            '20210531 jhs 페이지 초기화
            nowPage = 1
            '-------------------------------
            p_pd = New Drawing.Printing.PrintDocument

            If UseCustomPaper Then
                p_pd.DefaultPageSettings.PaperSize = New Drawing.Printing.PaperSize("Custom01", PaperSize_Width, PaperSize_Height)
            End If

            p_pd.DefaultPageSettings.Landscape = Landscape

            Dim ppdialog As New Windows.Forms.PrintPreviewDialog

            'm_ppdialog = ppdialog

            ppdialog.Document = p_pd

            p_spd = r_spd

            piRow_Start = 1

            ppdialog.StartPosition = FormStartPosition.CenterParent
            ppdialog.Width = Convert.ToInt32(r_spd.Height * 4 / 3)
            ppdialog.Height = r_spd.Height

            ppdialog.ShowDialog()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Sub BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles p_pd.BeginPrint
        piRow_Start = 1
        piRow_Body = 0
    End Sub



    Public Overridable Sub RenderPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles p_pd.PrintPage
        Dim sFn As String = "Sub RenderPage"

        e.Graphics.PageUnit = Drawing.GraphicsUnit.Point

        Try
            '여백 조정
            Dim iAutoMargin As Integer = 0

            If Left_Margin_cm = 0 Then iAutoMargin += 1
            If Right_Margin_cm = 0 Then iAutoMargin += 1
            If Top_Margin_cm = 0 Then iAutoMargin += 1
            If Bottom_Margin_cm = 0 Then iAutoMargin += 1

            If iAutoMargin > 0 Then
                psngX = e.MarginBounds.X * DrawPt_per_MarginPt
                psngY = e.MarginBounds.Y * DrawPt_per_MarginPt
                psngW = e.MarginBounds.Width * DrawPt_per_MarginPt
                psngH = e.MarginBounds.Height * DrawPt_per_MarginPt
            Else
                psngX = Left_Margin_cm * DrawPt_per_Cm
                psngY = Top_Margin_cm * DrawPt_per_Cm
                psngW = e.PageBounds.Width * DrawPt_per_MarginPt - (Left_Margin_cm + Right_Margin_cm) * DrawPt_per_Cm
                psngH = e.PageBounds.Height * DrawPt_per_MarginPt - (Top_Margin_cm + Bottom_Margin_cm) * DrawPt_per_Cm
            End If

            Dim iNewPage As Integer = 0

            psngPrtX = psngX
            psngPrtY = psngY

            With p_spd
                '20210531 jhs 페이지 넘버 총페이지수 
                totPages = Math.Round(CDbl(.MaxRows) / 63)
                '----------------------
                For i As Integer = piRow_Start To .MaxRows
                    If i = piRow_Start Then
                        iNewPage = 0
                    Else
                        ' If psngPrtY + Find_Height_Row(e, i) + Find_Height_Tail(e) > psngY + psngH Then

                        'If psngPrtY + Find_Height_Tail(e) > psngY + psngH Then
                        '    iNewPage = -1
                        '    nowPage += 1
                        'Else
                        '    iNewPage = 1
                        'End If

                        '20210311 jhs 프린트 바텀에 겹쳐져서 넘어가는 현상 해결
                        If rowNum = 63 Then
                            iNewPage = -1
                        Else
                            If psngPrtY + Find_Height_Tail(e) > psngY + psngH Then
                                iNewPage = -1
                            Else
                                iNewPage = 1
                            End If
                        End If
                        '-----------------------------

                    End If
                    Dim TEST As Double = (i / 64)

                    If iNewPage < 1 Then
                        If iNewPage = -1 Then
                            RenderPage_Tail(e, True)

                            e.HasMorePages = True

                            piRow_Start = i

                            piRow_Body = 0

                            rowNum = 1
                            Return
                        End If

                        psngPrtY = RenderPage_Title(e)

                        'psngPrtY = RenderPage_Headers(e)

                        'psngPrtY = RenderPage_Cols(e)

                        'RenderPage_Labels(e)
                    End If

                    psngPrtY = RenderPage_Body(e, i)


                    rowNum += 1
                    'If iNewPage < 1 Then
                    '    psngPrtY = RenderPage_Cols(e)
                    'End If
                Next

                RenderPage_Tail(e, False)
            End With

            rowNum = 1

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Overridable Function RenderPage_Body(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Single
        Dim sFn As String = "Function RenderPage_Body"

        Try
            Dim font As Drawing.Font = New Drawing.Font(psFontName, FontSize_Body)
            'End If

            Dim sBcNo As String = Ctrl.Get_Code(p_spd, "bcno", riRow)

            Dim sBuf As String = ""
            Dim sBuf_tnm As String = Ctrl.Get_Code(p_spd, "tnm", riRow)
            Dim sBuf_viewrst As String = Ctrl.Get_Code(p_spd, "viewrst", riRow)
            Dim sBuf_judgmark As String = Ctrl.Get_Code(p_spd, "hlmark", riRow)
            Dim sBuf_reftxt As String = Ctrl.Get_Code(p_spd, "reftxt", riRow)
            Dim sBuf_rstunit As String = Ctrl.Get_Code(p_spd, "rstunit", riRow)
            Dim sBuf_RegDt As String = Ctrl.Get_Code(p_spd, "rstdt", riRow)

            If sBuf_RegDt <> "" Then sBuf_RegDt = sBuf_RegDt.Substring(0, 10)

            Dim sngX_tnm As Single = 0, sngW_tnm As Single = 0
            Dim sngX_viewrst As Single = 0, sngW_viewrst As Single = 0
            Dim sngX_judgmark As Single = 0, sngW_judgmark As Single = 0
            Dim sngX_panicmark As Single = 0, sngW_panicmark As Single = 0
            Dim sngX_reftxt As Single = 0, sngW_reftxt As Single = 0
            Dim sngX_rstunit As Single = 0, sngW_rstunit As Single = 0
            Dim sngX_RegDt As Single = 0, sngW_RegDt As Single = 0

            Dim sngLineHeight As Single
            sngLineHeight = (New Drawing.Font(psFontName, FontSize_Body)).GetHeight(e.Graphics)


            If sBuf_tnm.StartsWith(CharLine.ToString()) Or sBuf_tnm.StartsWith(CharLine2.ToString()) Then
                '라인인 경우는 라인 로직에 따라 처리 후 Return
                Dim iLineLen As Integer = 0
                Dim fontL As Drawing.Font

                If sBuf_tnm.EndsWith("B") Then
                    fontL = New Drawing.Font(psFontName, FontSize_CharLine, FontStyle.Bold)
                Else
                    fontL = New Drawing.Font(psFontName, FontSize_CharLine, FontStyle.Regular)
                End If


                If sBuf_tnm.EndsWith("2") Then
                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine2.ToString) = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine2.ToString)
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine2.ToString) + 1
                    End If

                Else
                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine.ToString) = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString)
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString) + 1
                    End If

                End If

                '< 진하게찍는 경우 길이 하나 줄임.
                If sBuf_tnm.EndsWith("B") Then
                    iLineLen -= 1
                End If

                ''< 두줄 찍히는 경우 길이 하나 줄임. 
                'If sBuf_tnm.EndsWith("2") Then
                '    iLineLen -= 1
                'End If


                If sBuf_tnm.EndsWith("2") Then
                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine2), fontL, Drawing.Brushes.Black, _
                                psngX, psngPrtY)
                Else
                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                psngX, psngPrtY)
                End If


                psngPrtY += sngLineHeight
                Return psngPrtY
            End If

            If Cols Is Nothing Then
                sBuf = sBuf_tnm + " " + sBuf_viewrst + " " + sBuf_judgmark + " " + sBuf_reftxt + " " + sBuf_rstunit
                e.Graphics.DrawString(sBuf, font, Drawing.Brushes.Black, psngX + psngPrtX, psngPrtY)
            Else
                For i As Integer = 1 To Cols.Count
                    Select Case CType(Cols(i - 1), PrintCfg).PrtID
                        Case "tnm"
                            sngX_tnm = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_tnm = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "hlmark"
                            sngX_judgmark = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_judgmark = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "viewrst"
                            sngX_viewrst = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_viewrst = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "reftxt" '참고치
                            sngX_reftxt = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_reftxt = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "rstunit" '단위
                            sngX_rstunit = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_rstunit = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                        Case "regdt"
                            sngX_RegDt = psngX + CType(Cols(i - 1), PrintCfg).PrtX_Cm * DrawPt_per_Cm
                            sngW_RegDt = CType(Cols(i - 1), PrintCfg).PrtSize_Cm * DrawPt_per_Cm

                    End Select
                Next

                For i As Integer = 1 To Cols.Count
                    Dim rectF As Drawing.RectangleF
                    Dim sf As New Drawing.StringFormat
                    Dim prtcfg As PrintCfg = CType(Cols(i - 1), PrintCfg)

                    Select Case CType(Cols(i - 1), PrintCfg).PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.Right, PrintCfg.Align.Center
                            rectF = New Drawing.RectangleF(psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY, _
                                                            prtcfg.PrtSize_Cm * DrawPt_per_Cm, font.GetHeight(e.Graphics))

                        Case PrintCfg.Align.PageLeft, PrintCfg.Align.PageRight, PrintCfg.Align.PageCenter
                            rectF = New Drawing.RectangleF(psngX, psngPrtY, _
                                                            psngW, font.GetHeight(e.Graphics) + 1)
                    End Select

                    sf.LineAlignment = StringAlignment.Center

                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.PageLeft
                            sf.Alignment = StringAlignment.Near

                        Case PrintCfg.Align.Right, PrintCfg.Align.PageRight
                            sf.Alignment = StringAlignment.Far

                        Case PrintCfg.Align.Center, PrintCfg.Align.PageCenter
                            sf.Alignment = StringAlignment.Center

                    End Select

                    If sBcNo.Length = 0 Then
                        '타이틀 및 소견
                        Select Case prtcfg.PrtID
                            Case "tnm"
                                If Not sBuf_tnm = "" Then
                                    e.Graphics.DrawString(sBuf_tnm, font, Drawing.Brushes.Black, psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY)
                                    'e.Graphics.DrawString(sBuf_tnm, font, Drawing.Brushes.Black, rectF, sf)
                                End If
                        End Select
                    Else
                        '검사결과
                        Select Case prtcfg.PrtID
                            Case "tnm"
                                e.Graphics.DrawString(sBuf_tnm, font, Drawing.Brushes.Black, rectF, sf)
                                'e.Graphics.DrawString(sBuf_tnm, font, Drawing.Brushes.Black, psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY)
                            Case "hlmark"
                                If sBuf_judgmark.Length > 0 Then
                                    If sBuf_judgmark = "H" Then
                                        sBuf_judgmark = "▲"
                                    ElseIf sBuf_judgmark = "L" Then
                                        sBuf_judgmark = "▼"
                                    End If
                                    e.Graphics.DrawString(sBuf_judgmark, font, Drawing.Brushes.Black, rectF, sf)
                                End If

                            Case "viewrst"
                                If e.Graphics.MeasureString(sBuf_viewrst + " ", font).Width > sngX_reftxt - sngX_viewrst Then
                                    If sBuf_tnm.Length = 0 Then
                                        If sBuf_reftxt.Length = 0 Then
                                            If sBuf_rstunit.Length = 0 Then
                                                e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, rectF, sf)
                                            Else
                                                e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, _
                                                                        sngX_rstunit - e.Graphics.MeasureString(sBuf_viewrst + " ", font).Width, psngPrtY)
                                            End If
                                        Else
                                            e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, _
                                                                    sngX_reftxt - e.Graphics.MeasureString(sBuf_viewrst + " ", font).Width, psngPrtY)
                                        End If
                                    Else
                                        e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, rectF, sf)
                                    End If
                                Else
                                    e.Graphics.DrawString(sBuf_viewrst, font, Drawing.Brushes.Black, rectF, sf)
                                End If

                            Case "reftxt"
                                If sBuf_reftxt.Length > 0 Then
                                    e.Graphics.DrawString(sBuf_reftxt, font, Drawing.Brushes.Black, rectF, sf)
                                End If

                            Case "rstunit"
                                If sBuf_rstunit.Length > 0 Then
                                    e.Graphics.DrawString(sBuf_rstunit, font, Drawing.Brushes.Black, rectF, sf)
                                End If

                            Case "regdt"
                                If sBuf_RegDt.Length > 0 Then
                                    e.Graphics.DrawString(sBuf_RegDt, font, Drawing.Brushes.Black, rectF, sf)
                                End If
                        End Select
                    End If
                Next
            End If

            psngPrtY += sngLineHeight

            Return psngPrtY

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            'Page내의 Body만의 Row 수
            piRow_Body += 1

        End Try
    End Function


    Public Overridable Function RenderPage_Cols(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
        Dim sFn As String = "Sub RenderPage_Cols"

        Try
            If Cols Is Nothing Then Return psngPrtY

            Dim font_col As Drawing.Font

            For i As Integer = 1 To Cols.Count
                Dim prtcfg As PrintCfg = CType(Cols(i - 1), PrintCfg)
                Dim font As Drawing.Font = prtcfg.PrtFont

                Dim rectF As Drawing.RectangleF
                Dim sf As New Drawing.StringFormat

                Dim fontL As New Drawing.Font(psFontName, FontSize_CharLine)
                Dim iLineLen As Integer = 0

                'Cols Upper Line 표시
                If i = 1 Then
                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine.ToString) = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString)
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString) + 1
                    End If

                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, psngPrtY)

                    psngPrtY += fontL.GetHeight(e.Graphics)
                End If

                If prtcfg.PrtText.Length > 0 Then
                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.Right, PrintCfg.Align.Center
                            rectF = New Drawing.RectangleF(psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY, _
                                                            prtcfg.PrtSize_Cm * DrawPt_per_Cm, prtcfg.PrtFont.GetHeight(e.Graphics))

                        Case PrintCfg.Align.PageLeft, PrintCfg.Align.PageRight, PrintCfg.Align.PageCenter
                            rectF = New Drawing.RectangleF(psngX, psngPrtY, _
                                                            psngW, prtcfg.PrtFont.GetHeight(e.Graphics) + 1)
                    End Select

                    sf.LineAlignment = StringAlignment.Center

                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.PageLeft
                            sf.Alignment = StringAlignment.Near

                        Case PrintCfg.Align.Right, PrintCfg.Align.PageRight
                            sf.Alignment = StringAlignment.Far

                        Case PrintCfg.Align.Center, PrintCfg.Align.PageCenter
                            sf.Alignment = StringAlignment.Center

                    End Select

                    e.Graphics.DrawString(prtcfg.PrtText, font, Drawing.Brushes.Black, rectF, sf)

                    font_col = font
                End If

                'Cols Lower Line 표시
                If i = Cols.Count Then
                    psngPrtY += font_col.GetHeight(e.Graphics)

                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine.ToString) = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString)
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString) + 1
                    End If

                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, psngPrtY)

                    psngPrtY += fontL.GetHeight(e.Graphics)
                End If
            Next

            Return psngPrtY

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Function RenderPage_Headers(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
        Dim sFn As String = "Function RenderPage_Headers"

        Dim iY As Integer = 0

        Try
            'Between Title and Header : 빈 공간 추가
            Dim font_th As New Drawing.Font(psFontName, FontSize_Between_Title_Header)
            Dim sngHeight_th As Single = font_th.GetHeight(e.Graphics)

            e.Graphics.DrawString("", font_th, Drawing.Brushes.White, psngX, psngPrtY)

            psngPrtY += sngHeight_th

            If Headers Is Nothing Then Return psngPrtY

            Dim font_h As Drawing.Font

            For i As Integer = 1 To Headers.Count
                Dim prtcfg As PrintCfg = CType(Headers(i - 1), PrintCfg)
                Dim font As Drawing.Font = prtcfg.PrtFont

                Dim rectF As Drawing.RectangleF
                Dim sf As New Drawing.StringFormat

                Dim fontL As New Drawing.Font(psFontName, FontSize_CharLine)
                Dim iLineLen As Integer = 0

                'Headers Upper Line 표시
                If i = 1 Then
                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod CharLine.ToString.Length = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ CharLine.ToString.Length
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ CharLine.ToString.Length + 1
                    End If

                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, psngPrtY)

                    psngPrtY += fontL.GetHeight(e.Graphics)
                End If

                If prtcfg.PrtText.Length > 0 Then
                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.Right, PrintCfg.Align.Center
                            rectF = New Drawing.RectangleF(psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngPrtY, _
                                                            prtcfg.PrtSize_Cm * DrawPt_per_Cm, prtcfg.PrtFont.GetHeight(e.Graphics))

                        Case PrintCfg.Align.PageLeft, PrintCfg.Align.PageRight, PrintCfg.Align.PageCenter
                            rectF = New Drawing.RectangleF(psngX, psngPrtY, _
                                                            psngW, prtcfg.PrtFont.GetHeight(e.Graphics) + 1)
                    End Select

                    sf.LineAlignment = StringAlignment.Center

                    Select Case prtcfg.PrtAlign
                        Case PrintCfg.Align.Left, PrintCfg.Align.PageLeft
                            sf.Alignment = StringAlignment.Near

                        Case PrintCfg.Align.Right, PrintCfg.Align.PageRight
                            sf.Alignment = StringAlignment.Far

                        Case PrintCfg.Align.Center, PrintCfg.Align.PageCenter
                            sf.Alignment = StringAlignment.Center

                    End Select

                    e.Graphics.DrawString(prtcfg.PrtText, font, Drawing.Brushes.Black, rectF, sf)

                    font_h = font
                End If

                'Headers Lower Line 표시
                If i = Cols.Count Then
                    psngPrtY += font_h.GetHeight(e.Graphics)

                    If FixedVariable.FindLineLength(FontSize_CharLine) Mod CharLine.ToString.Length = 0 Then
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ CharLine.ToString.Length
                    Else
                        iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ CharLine.ToString.Length + 1
                    End If

                    e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, psngPrtY)

                    psngPrtY += fontL.GetHeight(e.Graphics)
                End If
            Next

            Return psngPrtY

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Overridable Sub RenderPage_Labels(ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Dim sFn As String = "Sub RenderPage_Labels"

        Try
            If Labels Is Nothing Then Return

            For i As Integer = 3 To Labels.Count
                Dim prtcfg As PrintCfg = CType(Labels(i - 1), PrintCfg)
                Dim font As Drawing.Font = prtcfg.PrtFont

                Dim rectF As Drawing.RectangleF

                Select Case prtcfg.PrtAlign
                    Case PrintCfg.Align.Left, PrintCfg.Align.Right, PrintCfg.Align.Center
                        rectF = New Drawing.RectangleF(psngX + prtcfg.PrtX_Cm * DrawPt_per_Cm, psngY + prtcfg.PrtY_Cm * DrawPt_per_Cm, _
                                                        prtcfg.PrtSize_Cm * DrawPt_per_Cm, prtcfg.PrtFont.GetHeight(e.Graphics))

                    Case PrintCfg.Align.PageLeft, PrintCfg.Align.PageRight, PrintCfg.Align.PageCenter
                        rectF = New Drawing.RectangleF(psngX, psngY + prtcfg.PrtY_Cm * DrawPt_per_Cm, _
                                                        psngW, prtcfg.PrtFont.GetHeight(e.Graphics) + 1)
                End Select

                Dim sf As New Drawing.StringFormat

                sf.LineAlignment = StringAlignment.Center

                Select Case prtcfg.PrtAlign
                    Case PrintCfg.Align.Left, PrintCfg.Align.PageLeft
                        sf.Alignment = StringAlignment.Near

                    Case PrintCfg.Align.Right, PrintCfg.Align.PageRight
                        sf.Alignment = StringAlignment.Far

                    Case PrintCfg.Align.Center, PrintCfg.Align.PageCenter
                        sf.Alignment = StringAlignment.Center

                End Select

                e.Graphics.DrawString(prtcfg.PrtText, font, Drawing.Brushes.Black, rectF, sf)
            Next

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Overridable Sub RenderPage_Tail(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal rbMore As Boolean)
        Dim sFn As String = "Sub RenderPage_Tail"

        Try
            Dim fontL As New Drawing.Font(psFontName, FontSize_CharLine)
            Dim fontT As New Drawing.Font(psFontName, FontSize_Tail)
            Dim sf As New Drawing.StringFormat

            Dim sTail As String = PRG_CONST.Tail_RstReport
            sTail += vbCrLf
            sTail += PRG_CONST.Tail_RstReport2
            sTail += vbCrLf
            sTail += vbCrLf
            sTail += PRG_CONST.Tail_Address

            'Tail 바로 앞 Line 표시
            Dim iLineLen As Integer = 0
            Dim iLineCnt As Integer = 0

            If FixedVariable.FindLineLength(FontSize_CharLine) Mod Fn.LengthH(CharLine.ToString) = 0 Then
                iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString)
            Else
                iLineLen = FixedVariable.FindLineLength(FontSize_CharLine) \ Fn.LengthH(CharLine.ToString) + 1
            End If

            If sTail.IndexOf(vbCrLf) > 0 Then
                sTail = sTail.Replace(vbCrLf, mcSEP)

                iLineCnt = sTail.Split(mcSEP).Length
            Else
                iLineCnt = 1
            End If

            e.Graphics.DrawString("".PadRight(iLineLen, CharLine), fontL, Drawing.Brushes.Black, _
                                    psngX, Convert.ToSingle(psngY + psngH - iLineCnt * fontT.GetHeight(e.Graphics) - fontL.GetHeight(e.Graphics)))

            'Tail 텍스트 표시
            For i As Integer = 1 To iLineCnt
                If iLineCnt = 1 Then
                    e.Graphics.DrawString(sTail, fontT, Drawing.Brushes.Black, _
                                    psngX, Convert.ToSingle(psngY + psngH - (iLineCnt - i) * fontT.GetHeight(e.Graphics) - fontL.GetHeight(e.Graphics)))
                Else
                    If i = iLineCnt Then
                        fontT = New Drawing.Font(psFontName, FontSize_Tail, FontStyle.Regular)
                    Else
                        fontT = New Drawing.Font(psFontName, FontSize_Tail, FontStyle.Bold)
                    End If
                    e.Graphics.DrawString(sTail.Split(mcSEP)(i - 1), fontT, Drawing.Brushes.Black, _
                                    psngX, Convert.ToSingle(psngY + psngH - (iLineCnt - i) * fontT.GetHeight(e.Graphics) - fontL.GetHeight(e.Graphics)))
                End If
            Next

            '20210531 jhs 페이지넘버 표시    (ex) [페이지 / 총페이지] )
            e.Graphics.DrawString("[ " + nowPage.ToString + " / " + totPages.ToString + " ]", fontT, Drawing.Brushes.Black, psngX + 460, Convert.ToSingle(psngY + psngH) - 10)
            nowPage += 1
            If nowPage > totPages Then
                nowPage = 1
            End If
            '--------------------------------

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Overridable Function RenderPage_Title(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
        Dim sFn As String = "Function RenderPage_Title"

        Try
            Dim font As New Drawing.Font(psFontName, FontSize_Title, FontStyle.Bold) 'Or FontStyle.Underline)
            Dim sngPrtH As Single

            Dim sf As New Drawing.StringFormat
            sf.LineAlignment = StringAlignment.Center
            sf.Alignment = Drawing.StringAlignment.Center

            sngPrtH = Convert.ToSingle(font.GetHeight(e.Graphics))

            Dim bax2 As New Drawing.Rectangle(Convert.ToInt32(psngX + 3), Convert.ToInt32(psngPrtY), 240, Convert.ToInt32(sngPrtH * 4))
            Dim rect1 As New Drawing.RectangleF(psngX, psngPrtY, psngW, sngPrtH * 4)

            e.Graphics.DrawString(Title, font, Drawing.Brushes.Black, rect1, sf)

#If DEBUG Then
            Dim rect As Drawing.Rectangle = New Drawing.Rectangle(Convert.ToInt32(psngX), Convert.ToInt32(psngPrtY), Convert.ToInt32(psngW), Convert.ToInt32(psngH))

            e.Graphics.DrawRectangle(Pens.LightSlateGray, rect)
#End If
            'Return : 변경된 Y
            Return psngPrtY + font.GetHeight(e.Graphics) * 3 '6

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Public Sub New()

    End Sub
End Class

Public Class SpecimenInfo
    Public BcNo As String = ""
    Public SpcNm As String = ""
    Public RegNo As String = ""
    Public PatNm As String = ""
    Public SexAge As String = ""
    Public IdNo As String = ""

    Public Height As String = ""
    Public Weight As String = ""
    Public Injong As String = ""
    Public AboRh As String = ""

    Public DeptNm As String = ""
    Public DeptCd As String = ""
    Public WardRoom As String = ""
    Public EntDt As String = ""
    Public OrdDt As String = ""
    Public DoctorNm As String = ""
    Public CollDt As String = ""
    Public CollUsr As String = ""
    Public TkDt As String = ""
    Public TkUsr As String = ""
    Public RegDt As String = ""
    Public RegUsr As String = ""
    Public MwUsr As String = ""
    Public TestDt As String = ""
    Public TestUsr As String = ""
    Public FnDt As String = ""
    Public FnUsr As String = ""
    Public LabDrNm As String = ""
    Public DiagNm As String = ""
    Public DrugNm As String = ""
    Public Address As String = ""
    Public Remark As String = ""
    Public Remark2 As String = ""

    Public Slip As String = ""
    Public slipName As String = ""
    Public RstCmt As String = ""
    Public InfInfo As String = ""   '-- 감염정보
    Public ErFlg As String = ""     '-- 응급여부

End Class
