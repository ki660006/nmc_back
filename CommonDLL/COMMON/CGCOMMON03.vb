'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_COMMON03.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : 공통함수 Class                                                         */
'/* Design       : 2003-07-29 Ju Jin Ho                                                   */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System.IO
Imports System.Net
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommConst
Imports COMMON.SVar

Namespace CommFN

#Region " 결과저장 Class 선언"
    ' 일반 검사항목 표시 클래스
    ' 검토 결과 : DataTable 만으로 가능
    ' 결과저장시 사용
    Public Class ResultInfo_Test
        Public mBCNO As String = ""         ' 검체번호
        Public mDGTestCd As String = ""     ' 대표검사코드
        Public mTestCd As String = ""
        Public mSpcCd As String = ""        ' 검체코드
        Public mDetailYN As String = ""     ' 상세항목여부 1 : 상세항목
        Public mViewRst As String = ""      ' 결과
        Public mOrgRst As String = ""       ' 원결과
        Public mOrgViewRst As String = ""   ' 실제결과
        Public mRstCmt As String = ""
        Public mBFBCNO As String = ""       ' 이전검체번호
        Public mBFFNDT As String = ""        ' 이전최종보고시간
        Public mTestNm As String = ""       ' 검사항목명
        Public mUpdateYN As String = ""     ' 결과수정여부
        Public mPanicMark As String = ""    ' 패닉판정
        Public mDeltaMark As String = ""    ' 델타판정
        Public mCriticalMark As String = "" ' 패닉판정
        Public mAlertMark As String = ""    ' 델타판정
        Public mHLMark As String = ""        ' 참고치판정
        Public mBFORGRST As String = ""     ' 이전실제결과
        Public mBFVIEWRST As String = ""    ' 이전보이는결과
        Public mBatchCmt As String = ""     ' 배치Comment
        Public mBatchRstChk As String = ""  ' 배치결과 확인
        Public mEqCd As String = ""         ' 장비코드
        Public mIntSeqNo As String = ""     ' 인터페이스 순번
        Public mRack As String = ""         ' Rack
        Public mPos As String = ""          ' Pos
        Public mEQBCNO As String = ""       ' 장비검체번호
        Public mEqFlag As String = ""       ' 장비Flag
        Public mRefTxt As String = ""       ' 참고치
        Public mRstFlg As String = ""       ' 결과 상태

        Public mCfmNm As String = ""        ' 확인의
        Public mCfmSign As String = ""      ' 확인의 인증

    End Class

    '-- 재검 항목 저장시 사용
    Public Class RERUN_INFO
        Public msBcNo As String = ""
        Public msTestCd As String = ""
        Public msRerunGbn As String = ""

        Public msRstFlg As String = ""
    End Class

    Public Class DTESTLIST
        Public TESTCD As String = ""
        Public SPCCCD As String = ""
        Public USDT As String = ""
    End Class

    ' 소견결과 클래스
    Public Class ResultInfo_Bac
        Public TestCd As String = ""
        Public SpcCd As String = ""

        Public BacGenCd As String = ""
        Public BacCd As String = ""
        Public BacSeq As String = ""
        Public Ranking As String = ""
        Public TestMtd As String = ""
        Public IncRst As String = ""
        Public BacCmt As String = ""
    End Class

    Public Class ResultInfo_Anti
        Public TestCd As String = ""
        Public SpcCd As String = ""

        Public BacCd As String = ""
        Public BacSeq As String = ""
        Public AntiCd As String = ""
        Public TestMtd As String = ""
        Public DecRst As String = ""
        Public AntiRst As String = ""
        Public RptYn As String = ""
    End Class

    Public Class ResultInfo_Cmt
        Public BcNo As String = ""
        Public PartSlip As String = ""
        Public TestCd As String = ""
        Public RstSeq As String = ""
        Public Cmt As String = ""
        Public RstFlg As String = ""
    End Class

    Public Class LIS_CVR_INFO

        '-- 2020-05-29 JJH CVR등록
        Public Orddt As String = ""
        Public Fkocs As String = ""
        Public Tnmd As String = ""
        Public Testcd As String = ""
        Public Rst As String = ""
        Public Rstdt As String = ""
        Public Rstid As String = ""
        Public RstUnit As String = ""

    End Class

    Public Class GCLAB_Data

        Public sCSTCD As String = ""        '병원코드 
        Public sSAMPLENO As String = ""     '검체번호
        Public sCSTITEMCD As String = ""    '검사코드
        Public sCSTITEMNM As String = ""    '검사명
        Public sHOSNO As String = ""        '환자번호
        Public sPATNM As String = ""        '환자명
        Public sSAMPLECD As String = ""     '검체코드
        Public sSAMPLENM As String = ""     '검체명
        Public sBIRDTE As String = ""       '주민번호
        Public sSEX As String = ""          '성별
        Public sHOSLOC As String = ""       '병동
        Public sHOSPLC As String = ""       '진료과
        Public sSAMDTE As String = ""       '채혈일자
        Public sSAMTME As String = ""       '채혈시간
        Public sDOCNM As String = ""        '의사명

    End Class


    '삼광의료재단 데이터 연동을 위한 변수 클래스
    Public Class SML_Data
        Public sCUCD As String = "" '병원구분코드(거래처코드)
        Public sJSDT As String = "" '접수일자
        Public sKSEQ As String = "" '검체ID
        Public sHGCD As String = "" '병원검사코드
        Public sHGNM As String = "" '병원검사명
        Public sKCCD As String = "" '검체코드
        Public sKCNM As String = "" '검체명
        Public sCHNO As String = "" '차트번호
        Public sPTNM As String = "" '수진자명
        Public sJNID As String = "" '주민번호
        Public sSEXX As String = "" '성별
        Public sAGEE As String = "" '나이
        Public sMENM As String = "" '의사명
        Public sWARD As String = "" '병동
        Public sJKNM As String = "" '진료과
        Public sPIDT As String = "" '채취일자
    End Class


    Public Class NCOV_Cancel

        Public sBcno As String = ""
        Public sTestcd As String = ""
        Public sGbn As String = ""
        Public sSeq As String = ""
        Public sDtgbn As String = ""

    End Class

#End Region


    Public Class DP_Common
        Private Const sFile As String = "File : CGCOMMON03.vb, Class : DP_Common" & vbTab

        Public Shared Sub setToolTip(ByVal e As Drawing.Graphics, ByVal wc As Control, ByVal sText As String, ByVal tp As ToolTip)

            Dim textWidth As Integer = CInt(e.MeasureString(sText, wc.Font).Width)

            If wc.Width < textWidth Then
                Dim sStr As String
                Dim iWidth As Integer = wc.Width - 10

                For i As Integer = 1 To sText.Length
                    sStr = Mid(sText, 1, i) & "..."
                    If iWidth < CInt(e.MeasureString(sStr, wc.Font).Width) Then
                        sStr = Mid(sText, 1, i - 1) & "..."
                        wc.Text = sStr
                        Exit For
                    End If
                Next
            Else
                '< yjlee 2009-03-18 
                '< # 이전 ToolTip을 가지고 있음으로 수정 
                sText = ""

            End If
            tp.SetToolTip(wc, sText)
            '> yjlee 2009-03-18

        End Sub

        Public Shared Function getTipLine(ByVal sStr As String) As String
            getTipLine = Space(4) & sStr & Space(4) & vbCrLf
        End Function

        ' listbox 위치 찾기
        ' Log
        Public Shared Sub sbFindPosition(ByVal r_Object As Windows.Forms.ListBox, ByVal rsRst As String)
            Dim sFn As String = "Sub sbFindPosition(ByVal lstCode As Windows.Forms.ListBox, ByVal strRst As String)"
            Try
                Dim x As Integer = -1
                If Convert.ToString(rsRst) = "" Then
                    r_Object.ClearSelected()
                Else
                    x = r_Object.FindString(Convert.ToString(rsRst), x)
                    If x <> -1 Then
                        r_Object.SetSelected(x, True)
                        r_Object.Focus()
                    End If
                End If

            Catch ex As Exception
                COMMON.CommFN.Fn.log(sFile & sFn, Err)
                MsgBox("Error " & sFile & sFn & vbCrLf & Err.Description)
            End Try
        End Sub

        ' 검사항목별 결과코드 표시
        ' Log
        Public Shared Sub sbDispaly_test_rstcd(ByVal r_dt As DataTable, ByVal rsTestCd As String, ByVal r_ListBox As Windows.Forms.ListBox)
            Dim sFn As String = "Sub displayGeneralRstCD(ByVal dtGRstCd As DataTable, ByVal sTestCd As String, ByVal lstCode As Windows.Forms.ListBox)"
            Try
                Dim sTmp As String = ""
                Dim dr As DataRow() = r_dt.Select("testcd = '" & rsTestCd & "'")

                r_ListBox.Items.Clear()

                If dr.Length > 0 Then
                    Dim r As DataRow
                    For Each r In dr
                        sTmp = ""
                        sTmp = r.Item("keypad").ToString + Microsoft.VisualBasic.vbTab + r.Item("rstcont").ToString + Microsoft.VisualBasic.vbTab

                        r_ListBox.Items.Add(sTmp)
                    Next r
                    r_ListBox.BringToFront()
                    r_ListBox.Show()
                Else
                    r_ListBox.Hide()
                End If

            Catch ex As Exception
                COMMON.CommFN.Fn.log(sFile & sFn, Err)
                '#If DEBUG Then
                MsgBox("Error " & sFile & sFn & vbCrLf & Err.Description)
                '#End If
            End Try
        End Sub

        '< add freety 2005/08/17 : RstFlag에 따른 결과상태 기호화
        Public Shared Function fnFind_Symbol_By_RstFlg(ByVal rsRstFlg As String) As String
            Select Case rsRstFlg
                Case ""
                    Return ""

                Case "1"
                    Return FixedVariable.gsRstFlagR

                Case "2"
                    Return FixedVariable.gsRstFlagM

                Case "F"
                    Return FixedVariable.gsRstFlagM

                Case "3"
                    Return FixedVariable.gsRstFlagF

                Case Else
                    Return ""

            End Select
        End Function
    End Class

    '< test
    Public MustInherit Class Person
        Private mgID As Guid = Guid.NewGuid
        Private mstrName As String = ""
        Public Property ID() As Guid
            Get
                Return mgID
            End Get
            Set(ByVal Value As Guid)
                mgID = Value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return mstrName
            End Get
            Set(ByVal Value As String)
                mstrName = Value
            End Set
        End Property
    End Class

    Public Class Customer
        Inherits Person
        Implements IPrintableObject

        Private mstrPhone As String = ""
        Public Property Phone() As String
            Get
                Return mstrPhone
            End Get
            Set(ByVal Value As String)
                mstrPhone = Value
            End Set
        End Property

        Private Sub Print() _
            Implements IPrintableObject.Print, IPrintableObject.PrintPreview
            Dim p As New ObjectPrinter
            p.PrintPreview(Me)
        End Sub

        Private Sub RenderPage(ByVal sender As Object, _
            ByVal ev As System.Drawing.Printing.PrintPageEventArgs) _
            Implements IPrintableObject.RenderPage

            Dim PrintFont As New Font("Arial", 10)
            Dim LineHeight As Single = PrintFont.GetHeight(ev.Graphics)
            Dim LeftMargin As Single = ev.MarginBounds.Left
            Dim yPos As Single = ev.MarginBounds.Top

            ev.Graphics.DrawString("ID : " & ID.ToString, PrintFont, Brushes.Black, _
                LeftMargin, yPos, New StringFormat)

            yPos += LineHeight
            ev.Graphics.DrawString("Name : " & Name, PrintFont, Brushes.Black, _
                LeftMargin, yPos, New StringFormat)

            ev.HasMorePages = False

        End Sub
    End Class
    '> test

    Public Interface IPrintableObject
        Sub Print()
        Sub PrintPreview()
        Sub RenderPage(ByVal sender As Object, _
            ByVal ev As System.Drawing.Printing.PrintPageEventArgs)
    End Interface

    Public Class PrinterBase
        Protected Const ms1InCm As Single = 2.5399
        Protected Const msPoint As Single = 72

        Public Function CmToPoint(ByVal iCm As Single) As Single
            CmToPoint = CType((iCm / ms1InCm) * msPoint, Single)
        End Function
    End Class

    Public Class ObjectPrinter
        Private WithEvents MyDoc As PrintDocument
        Private printObject As IPrintableObject

        Public mbLandscape As Boolean = False

        Public Sub Print(ByVal obj As IPrintableObject)
            printObject = obj
            MyDoc = New PrintDocument
            MyDoc.DefaultPageSettings.Landscape = mbLandscape
            MyDoc.Print()
        End Sub

        Public Sub PrintPreview(ByVal obj As IPrintableObject)
            Dim PPdlg As PrintPreviewDialog = New PrintPreviewDialog

            printObject = obj

            MyDoc = New PrintDocument
            MyDoc.DefaultPageSettings.Landscape = mbLandscape
            PPdlg.Document = MyDoc
            PPdlg.ShowDialog()
        End Sub

        Private Sub PrintPage(ByVal sender As Object, _
            ByVal ev As System.Drawing.Printing.PrintPageEventArgs) Handles MyDoc.PrintPage

            'ev.Graphics.PageUnit = GraphicsUnit.Point

            printObject.RenderPage(sender, ev)
        End Sub
    End Class

    Public Class MainServerDateTime
        Public Shared mServerDateTime As Date
        Public Shared mKeyInDateTime As Date
    End Class

    Public Class PDCAMsg
        Public bP As Boolean
        Public bD As Boolean
        Public bC As Boolean
        Public bA As Boolean

        Public Sub New()
            bP = False
            bD = False
            bC = False
            bA = False
        End Sub

        Public msMsgP As String = "Panic "
        Public msMsgD As String = "Delta "
        Public msMsgC As String = "Critical "
        Public msMsgA As String = "Alert "

        Public msMsgRst As String = "결과값이 있습니다. "
        Public msMsgNot As String = "보고를 할 수 있는 권한이 없습니다."
        Public msMsgNotFN As String = "최종보고를 할 수 있는 권한이 없습니다. 결과저장이 됩니다."
        Public msMsgFNUpdate As String = "최종보고를 수정할 수 있는 권한이 없습니다."
    End Class

    '< add freety 2005/08/22
    Public Class PrintCfg
        Public Enum Align
            Left = 0
            Center = 1
            Right = 2
            PageLeft = 3
            PageCenter = 4
            PageRight = 5
        End Enum

        Public PrtAlign As Align = Align.Left
        Public PrtID As String = ""
        Public PrtSize As Integer = 0
        Public PrtText As String = ""

        Public PrtX_Cm As Single = 0
        Public PrtY_Cm As Single = 0
        Public PrtSize_Cm As Single = 0
        Public PrtFont As Drawing.Font = New Drawing.Font("굴림체", 10)
    End Class

    Public Class PrintList
        Private Const mc_sFile As String = "File : CGCOMMON03.vb, Class : PrintList" & vbTab

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
        Public Tail As String = ""
        Public PrintDateTime As String = ""

        Public Separator As String = Convert.ToChar(1)
        Public EachLen_UDI As Integer = 5                   'Each Length Of User Defined Item

        Public FontSize_Title As Integer = 14
        Public FontSize_Between_Title_Header As Integer = 14
        Public FontSize_Header As Integer = 9
        Public FontSize_Body As Integer = 9
        Public FontSize_Tail As Integer = 9

        Public PaperSize_Height As Integer = 150
        Public PaperSize_Width As Integer = 150

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

        Protected piRow_Start As Integer = 1

        Protected psSEP As String = " "

        Protected msFontName As String = "굴림체"

        Private mcSEP As Char = Convert.ToChar(1)

        Protected WithEvents p_pd As Drawing.Printing.PrintDocument

        Public Overridable Function Find_Height_Row(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Single
            Dim sFn As String = "Function Find_Height_Row"

            Try
                Dim iLine As Integer = 1

                With p_spd
                    Dim sTNmP As String = Ctrl.Get_Code(p_spd, "tnmp", riRow)

                    sTNmP = sTNmP.Replace(Separator, mcSEP)

                    Dim sTNm_Tot As String = ""

                    For i As Integer = 1 To sTNmP.Split(mcSEP).Length
                        Dim sTNm As String = sTNmP.Split(mcSEP)(i - 1)

                        If Fn.LengthH(sTNm) > EachLen_UDI Then
                            sTNm = Fn.SubstringH(sTNm, 0, EachLen_UDI)
                        Else
                            sTNm = Fn.PadRightH(sTNm, EachLen_UDI)
                        End If

                        If sTNm_Tot.Length > 0 Then
                            sTNm_Tot += psSEP
                        End If

                        If Fn.LengthH(sTNm_Tot + sTNm) > Find_MaxLen_UDI(e) Then
                            iLine += 1
                            sTNm_Tot = sTNm
                        Else
                            sTNm_Tot += sTNm
                        End If
                    Next
                End With

                Dim sngLineHeight As Single = 0

                sngLineHeight = (New Drawing.Font(msFontName, FontSize_Body)).GetHeight(e.Graphics)

                If riRow Mod 5 = 0 Then
                    Return iLine * (2 * sngLineHeight + sngLineHeight / 2)
                Else
                    Return iLine * (2 * sngLineHeight)
                End If

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function

        Public Overridable Function Find_Height_Tail(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
            Dim sFn As String = "Function Find_Height_Tail"

            Try
                Dim sngLineHeight As Single = 0

                sngLineHeight = (New Drawing.Font(msFontName, FontSize_Tail)).GetHeight(e.Graphics)

                Return Convert.ToSingle(1.5 * sngLineHeight)

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function

        Public Overridable Function Find_MaxLen_Body(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer
            Dim sFn As String = "Function Find_MaxLen_Body"

            Try
                Dim font As Drawing.Font = New Drawing.Font(msFontName, FontSize_Body)

                For i As Integer = 1 To Integer.MaxValue
                    If e.Graphics.MeasureString(New String("0"c, i), font).Width > psngW Then
                        Return i - 1
                    End If
                Next

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function

        Public Overridable Function Find_MaxLen_UDI(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer
            Dim sFn As String = "Function Find_MaxLen_UDI"

            'Find Max Length Of User Defined Item
            Try
                Dim iMaxLen_Body As Integer = Find_MaxLen_Body(e)

                Dim iTotLen_Header As Integer = 0

                'User Defined Item의 Header가 맨 마지막으로 설정되어야함
                For i As Integer = 1 To Headers.Count - 1
                    If i > 1 Then
                        iTotLen_Header += psSEP.Length
                    End If

                    iTotLen_Header += CType(Headers(i - 1), PrintCfg).PrtSize
                Next

                Return iMaxLen_Body - iTotLen_Header

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function

        Public Overridable Function Print(ByVal r_spd As AxFPSpreadADO.AxfpSpread) As Integer
            Dim sFn As String = "Function Print"

            Try
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

        Public Overridable Function PrintPreview(ByVal r_spd As AxFPSpreadADO.AxfpSpread) As Integer
            Dim sFn As String = "Function PrintPreview"

            Try
                p_pd = New Drawing.Printing.PrintDocument

                If UseCustomPaper Then
                    p_pd.DefaultPageSettings.PaperSize = New Drawing.Printing.PaperSize("Custom01", PaperSize_Width, PaperSize_Height)
                End If

                p_pd.DefaultPageSettings.Landscape = Landscape

                Dim ppdialog As New Windows.Forms.PrintPreviewDialog

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
                    For i As Integer = piRow_Start To .MaxRows
                        If i = piRow_Start Then
                            iNewPage = 0
                        Else
                            If psngPrtY + Find_Height_Row(e, i) + Find_Height_Tail(e) > psngY + psngH Then
                                iNewPage = -1
                            Else
                                iNewPage = 1
                            End If
                        End If

                        If iNewPage < 1 Then
                            If iNewPage = -1 Then
                                RenderPage_Tail(e, True)

                                e.HasMorePages = True

                                piRow_Start = i

                                Return
                            End If

                            psngPrtY = RenderPage_Title(e)

                            psngPrtY = RenderPage_Headers(e)

                            RenderPage_Labels(e)
                        End If

                        psngPrtY = RenderPage_Body_UDI(e, i, "".PadRight(RenderPage_Body_Base(e, i)))
                    Next

                    RenderPage_Tail(e, False)
                End With

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Sub

        Public Overridable Function RenderPage_Body_Base(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Integer
            Dim sFn As String = "Function RenderPage_Body_Base"

            Try
                Dim sLine As String = ""

                With p_spd
                    'User Defined Item이 아닌 내용 표시 : seq가 맨 처음, UDI가 맨 마지막이어야 함
                    For i As Integer = 1 To Headers.Count - 1
                        Dim prtcfg As PrintCfg = CType(Headers(i - 1), PrintCfg)

                        Dim iCol As Integer = 0
                        Dim sBuf As String = ""

                        If prtcfg.PrtID.Length > 0 And prtcfg.PrtSize > 0 Then
                            If prtcfg.PrtID = "seq" Then
                                sLine = Fn.PadLeftH(riRow.ToString(), prtcfg.PrtSize)
                            Else
                                iCol = .GetColFromID(prtcfg.PrtID)

                                If iCol > 0 Then
                                    sBuf = Ctrl.Get_Code(p_spd, iCol, riRow)
                                    sLine += psSEP
                                    sLine += Fn.PadRightH(sBuf, prtcfg.PrtSize)
                                End If
                            End If
                        End If
                    Next

                    Dim font As Drawing.Font = New Drawing.Font(msFontName, FontSize_Body)

                    e.Graphics.DrawString(sLine, font, Drawing.Brushes.Black, psngPrtX, psngPrtY)
                End With

                Return Fn.LengthH(sLine + psSEP)

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function

        Public Overridable Function RenderPage_Body_UDI(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer, ByVal rsPre As String) As Single
            Dim sFn As String = "Function RenderPage_Body_UDI"

            Try
                Dim font As Drawing.Font = New Drawing.Font(msFontName, FontSize_Body)

                'User Defined Item 내용 표시
                Dim sTNmP As String = Ctrl.Get_Code(p_spd, "tnmp", riRow)

                sTNmP = sTNmP.Replace(Separator, mcSEP)

                Dim sTNm_Tot As String = ""

                Dim sngLineHeight As Single
                sngLineHeight = (New Drawing.Font(msFontName, FontSize_Body)).GetHeight(e.Graphics)

                For i As Integer = 1 To sTNmP.Split(mcSEP).Length
                    Dim sTNm As String = sTNmP.Split(mcSEP)(i - 1)

                    If Fn.LengthH(sTNm) > EachLen_UDI Then
                        sTNm = Fn.SubstringH(sTNm, 0, EachLen_UDI)
                    Else
                        sTNm = Fn.PadRightH(sTNm, EachLen_UDI)
                    End If

                    If sTNm_Tot.Length > 0 Then
                        sTNm_Tot += psSEP
                    End If

                    If Fn.LengthH(sTNm_Tot + sTNm) > Find_MaxLen_UDI(e) Then
                        e.Graphics.DrawString(rsPre + sTNm_Tot, font, Drawing.Brushes.Black, psngX, psngPrtY)
                        psngPrtY += 2 * sngLineHeight

                        'sTNm_Tot 초기화
                        sTNm_Tot = sTNm
                    Else
                        sTNm_Tot += sTNm
                    End If
                Next

                e.Graphics.DrawString(rsPre + sTNm_Tot, font, Drawing.Brushes.Black, psngX, psngPrtY)
                psngPrtY += 2 * sngLineHeight

                If riRow Mod 5 = 0 Then
                    e.Graphics.DrawLine(Drawing.Pens.LightGray, psngX, psngPrtY, psngX + psngW, psngPrtY)
                    psngPrtY += sngLineHeight / 2
                End If

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
                Dim font_th As New Drawing.Font(msFontName, FontSize_Between_Title_Header)
                Dim sngHeight_th As Single = font_th.GetHeight(e.Graphics)

                e.Graphics.DrawString("", font_th, Drawing.Brushes.White, psngX, psngPrtY)

                psngPrtY += sngHeight_th

                'Header Upper Line 표시
                Dim font_h As New Drawing.Font(msFontName, FontSize_Header)
                Dim sngHeight_h As Single = font_h.GetHeight(e.Graphics)

                e.Graphics.DrawLine(Drawing.Pens.Black, psngX, Convert.ToSingle(psngPrtY + (sngHeight_h / 2)), psngX + psngW, Convert.ToSingle(psngPrtY + (sngHeight_h / 2)))

                psngPrtY += sngHeight_h

                'Header : 텍스트 표시
                Dim sHeader As String = ""

                For i As Integer = 1 To Headers.Count
                    Dim prtcfg As PrintCfg = CType(Headers(i - 1), PrintCfg)

                    If prtcfg.PrtSize > 0 Then
                        If sHeader.Length > 0 Then sHeader += psSEP

                        sHeader += Fn.PadRightH(prtcfg.PrtText, prtcfg.PrtSize)
                    End If
                Next

                e.Graphics.DrawString(sHeader, font_h, Drawing.Brushes.Black, psngX, psngPrtY)

                psngPrtY += sngHeight_h

                'Header Lower Line 표시
                e.Graphics.DrawLine(Drawing.Pens.Black, psngX, Convert.ToSingle(psngPrtY + (sngHeight_h / 2)), psngX + psngW, Convert.ToSingle(psngPrtY + (sngHeight_h / 2)))

                psngPrtY += sngHeight_h

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

                For i As Integer = 1 To Labels.Count
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
                Dim font As New Drawing.Font(msFontName, FontSize_Tail)
                Dim sf As New Drawing.StringFormat
                Dim rectF As Drawing.RectangleF

                'Tail 바로 앞 Line 표시
                e.Graphics.DrawLine(Drawing.Pens.Black, psngPrtX, Convert.ToSingle(psngY + psngH - 1.5 * font.GetHeight(e.Graphics)), _
                                                            psngPrtX + psngW, Convert.ToSingle(psngY + psngH - 1.5 * font.GetHeight(e.Graphics)))

                'Tail 텍스트 표시
                sf.LineAlignment = StringAlignment.Center
                sf.Alignment = Drawing.StringAlignment.Near
                rectF = New Drawing.RectangleF(psngPrtX, psngY + psngH - font.GetHeight(e.Graphics), psngW, font.GetHeight(e.Graphics))
                e.Graphics.DrawString(Tail, font, Drawing.Brushes.Black, rectF, sf)

                '출력일시, 계속 여부 표시
                sf.LineAlignment = StringAlignment.Center
                sf.Alignment = StringAlignment.Far
                rectF = New Drawing.RectangleF(psngPrtX, psngY + psngH - font.GetHeight(e.Graphics), psngW, font.GetHeight(e.Graphics))

                Dim sBuf As String = "출력일시 " + PrintDateTime

                If rbMore Then
                    sBuf += "  - 계속 -"
                Else
                    sBuf += "  -  끝  -"
                End If

                e.Graphics.DrawString(sBuf, font, Drawing.Brushes.Black, rectF, sf)

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Sub

        Public Overridable Function RenderPage_Title(ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Single
            Dim sFn As String = "Function RenderPage_Title"

            Try
                Dim font As New Drawing.Font(msFontName, FontSize_Title, FontStyle.Bold)

                Dim sf As New Drawing.StringFormat
                sf.LineAlignment = StringAlignment.Center
                sf.Alignment = Drawing.StringAlignment.Center

                Dim rectF As New Drawing.RectangleF(psngX, psngPrtY, psngW, Convert.ToSingle(font.GetHeight(e.Graphics)))

                e.Graphics.DrawString(Title, font, Drawing.Brushes.Black, rectF, sf)

#If DEBUG Then
                Dim rect As Drawing.Rectangle = New Drawing.Rectangle(Convert.ToInt32(psngX), Convert.ToInt32(psngPrtY), Convert.ToInt32(psngW), Convert.ToInt32(psngH))

                e.Graphics.DrawRectangle(Pens.LightSlateGray, rect)
#End If

                'Return : 변경된 Y
                Return psngPrtY + font.GetHeight(e.Graphics)

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function
    End Class

    Public Class PrintList_Barcode
        Inherits PrintList

        Private Const mc_sFile As String = "File : CGCOMMON03.vb, Class : PrintList_Barcode" & vbTab

        Public FontName_BarCd_Symb As String = "Code39(1:2)"
        Public FontSize_BarCd_Symb As Integer = 12

        Public FontName_BarCd_Text As String = "굴림체"
        Public FontSize_BarCd_Text As Integer = 6

        Public BarCd_Space As Integer = 18

        Public BarCd_Symb_Col As String = ""
        Public BarCd_Text_Col As String = ""

        Public BarCd_Symb_Cm As Single = 0
        Public BarCd_Text_Cm As Single = 0

        Public Overrides Function RenderPage_Body_Base(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Integer
            Dim sFn As String = "Function RenderPage_Body_Base"

            Try
                Dim sLine As String = ""

                With p_spd
                    'User Defined Item이 아닌 내용 표시 : seq가 맨 처음, UDI가 맨 마지막이어야 함
                    For i As Integer = 1 To Headers.Count - 1
                        Dim prtcfg As PrintCfg = CType(Headers(i - 1), PrintCfg)

                        Dim iCol As Integer = 0
                        Dim sBuf As String = ""

                        If prtcfg.PrtID.Length > 0 And prtcfg.PrtSize > 0 Then
                            If prtcfg.PrtID = "seq" Then
                                sLine = Fn.PadLeftH(riRow.ToString(), prtcfg.PrtSize)

                                '< Override
                                sLine += psSEP
                                sLine += "".PadRight(BarCd_Space)

                                Dim font_bs As Drawing.Font = New Drawing.Font(FontName_BarCd_Symb, FontSize_BarCd_Symb)
                                Dim font_bt As Drawing.Font = New Drawing.Font(FontName_BarCd_Text, FontSize_BarCd_Text)

                                Dim sBarCd_s As String = ""
                                Dim sBarCd_t As String = ""

                                iCol = .GetColFromID(BarCd_Symb_Col)

                                If iCol > 0 Then
                                    sBarCd_s = Ctrl.Get_Code(p_spd, iCol, riRow)
                                End If

                                iCol = .GetColFromID(BarCd_Text_Col)

                                If iCol > 0 Then
                                    sBarCd_t = Ctrl.Get_Code(p_spd, iCol, riRow)
                                End If

                                '바코드 표시
                                If sBarCd_s.Length > 0 And sBarCd_t.Length > 0 Then
                                    e.Graphics.DrawString("*" + sBarCd_s + "*", font_bs, Drawing.Brushes.Black, _
                                                            psngX + BarCd_Symb_Cm * DrawPt_per_Cm, psngPrtY)

                                    If sBarCd_t.Length = 16 Then
                                        If sBarCd_s.Length = 11 Then
                                            sBarCd_t += "-" + sBarCd_s.Substring(sBarCd_s.Length - 1, 1)
                                        Else
                                            sBarCd_t += "-" + "0"
                                        End If
                                    End If

                                    'sky20080108------------>>>
                                    '    e.Graphics.DrawString(sBarCd_t, font_bt, Drawing.Brushes.Black, _
                                    '                            psngX + BarCd_Text_Cm * Drawpt_per_Cm, psngPrtY + font_bs.GetHeight(e.Graphics))
                                    'End If
                                    e.Graphics.DrawString(sBarCd_s, font_bt, Drawing.Brushes.Black, _
                                                          psngX + BarCd_Text_Cm * DrawPt_per_Cm, psngPrtY + font_bs.GetHeight(e.Graphics))
                                    '--------------------------
                                End If
                                '>
                            Else
                                iCol = .GetColFromID(prtcfg.PrtID)

                                If iCol > 0 Then
                                    sBuf = Ctrl.Get_Code(p_spd, iCol, riRow)
                                    sLine += psSEP
                                    sLine += Fn.PadRightH(sBuf, prtcfg.PrtSize)
                                End If
                            End If
                        End If
                    Next

                    Dim font As Drawing.Font = New Drawing.Font(msFontName, FontSize_Body)

                    e.Graphics.DrawString(sLine, font, Drawing.Brushes.Black, psngPrtX, psngPrtY)
                End With

                Return Fn.LengthH(sLine + psSEP)

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function

        Public Overrides Function Find_Height_Row(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Single

        End Function
    End Class

    Public Class PrintList_SelTestOnly
        Inherits PrintList_Barcode

        Private Const mc_sFile As String = "File : CGCOMMON03.vb, Class : PrintList_SelTestOnly" & vbTab

        Public TClsCds As ArrayList = Nothing
        Public Separator_TClsCd As String = Convert.ToChar(1)

        Private mcSEP As Char = Convert.ToChar(1)

        Public Overrides Function Find_Height_Row(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer) As Single
            Dim sFn As String = "Function Find_Height_Row"

            Try
                Dim iLine As Integer = 1

                Dim iTotLen_UDI As Integer = TClsCds.Count * EachLen_UDI + (TClsCds.Count - 1) + psSEP.Length

                iLine = iTotLen_UDI \ Find_MaxLen_UDI(e) + 1

                Dim sngLineHeight As Single = 0

                sngLineHeight = (New Drawing.Font(msFontName, FontSize_Body)).GetHeight(e.Graphics)

                If riRow Mod 5 = 0 Then
                    Return iLine * (2 * sngLineHeight + sngLineHeight / 2)
                Else
                    Return iLine * (2 * sngLineHeight)
                End If

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function

        Public Overrides Function RenderPage_Body_UDI(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal riRow As Integer, ByVal rsPre As String) As Single
            Dim sFn As String = "Function RenderPage_Body_UDI"

            Try
                Dim font As Drawing.Font = New Drawing.Font(msFontName, FontSize_Body)

                'User Defined Item 내용 표시
                Dim sTNmP As String = Ctrl.Get_Code(p_spd, "tnmp", riRow)
                Dim sTClsCd As String = Ctrl.Get_Code(p_spd, "tclscd", riRow)

                sTNmP = sTNmP.Replace(Separator, mcSEP)
                sTClsCd = sTClsCd.Replace(Separator_TClsCd, mcSEP)

                Dim sTNm_Sort As String = ""

                'TClsCds의 순서에 맞춰 변경
                For c As Integer = 1 To TClsCds.Count
                    Dim iMatch As Integer = 0

                    For i As Integer = 1 To sTClsCd.Split(mcSEP).Length
                        If TClsCds(c - 1).ToString().Equals(sTClsCd.Split(mcSEP)(i - 1)) Then
                            iMatch = i
                            Exit For
                        End If
                    Next

                    If sTNm_Sort.Length > 0 Then sTNm_Sort += mcSEP

                    If iMatch > 0 Then
                        sTNm_Sort += sTNmP.Split(mcSEP)(iMatch - 1)
                    Else
                        sTNm_Sort += " "
                    End If
                Next

                'Sort -> Original
                sTNmP = sTNm_Sort

                Dim sTNm_Tot As String = ""

                Dim sngLineHeight As Single
                sngLineHeight = (New Drawing.Font(msFontName, FontSize_Body)).GetHeight(e.Graphics)

                For i As Integer = 1 To sTNmP.Split(mcSEP).Length
                    Dim sTNm As String = sTNmP.Split(mcSEP)(i - 1)

                    If Fn.LengthH(sTNm) > EachLen_UDI Then
                        sTNm = Fn.SubstringH(sTNm, 0, EachLen_UDI)
                    Else
                        sTNm = Fn.PadRightH(sTNm, EachLen_UDI)
                    End If

                    If sTNm_Tot.Length > 0 Then
                        sTNm_Tot += psSEP
                    End If

                    If Fn.LengthH(sTNm_Tot + sTNm) > Find_MaxLen_UDI(e) Then
                        e.Graphics.DrawString(rsPre + sTNm_Tot, font, Drawing.Brushes.Black, psngX, psngPrtY)
                        psngPrtY += 2 * sngLineHeight

                        'sTNm_Tot 초기화
                        sTNm_Tot = sTNm
                    Else
                        sTNm_Tot += sTNm
                    End If
                Next

                e.Graphics.DrawString(rsPre + sTNm_Tot, font, Drawing.Brushes.Black, psngX, psngPrtY)
                psngPrtY += 2 * sngLineHeight

                If riRow Mod 5 = 0 Then
                    e.Graphics.DrawLine(Drawing.Pens.LightGray, psngX, psngPrtY, psngX + psngW, psngPrtY)
                    psngPrtY += sngLineHeight / 2
                End If

                Return psngPrtY

            Catch ex As Exception
                Fn.log(mc_sFile + sFn, Err)
                MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

            End Try
        End Function
    End Class
End Namespace

