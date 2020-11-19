'>>> [20] 특수검사 보고서
Imports Oracle.DataAccess.Client
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF20
    Inherits System.Windows.Forms.Form
    Private Const mcFile As String = "File : FDF20.vb, Class : FDF20" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_SPTEST
    Private miStSubCnt As Integer = 0
    Private miStSubSeq As Integer = 0

    Private m_al_StSub As New ArrayList
    Private m_al_TestCd As New ArrayList

    Public gsModDT As String = ""

    Public gsModID As String = ""

    Friend WithEvents btnPreView As System.Windows.Forms.Button
    Friend WithEvents lblTestCdHlp As System.Windows.Forms.Label
    Friend WithEvents lblSubTestHlp As System.Windows.Forms.Label
    Friend WithEvents btnSelTest As System.Windows.Forms.Button
    Friend WithEvents btnCdHelp_tsub As System.Windows.Forms.Button
    Friend WithEvents btnCdHelp_tref As System.Windows.Forms.Button
    Friend WithEvents lblDbFld As System.Windows.Forms.Label
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox

    Protected WithEvents p_pd As Drawing.Printing.PrintDocument



    Private Function fnCollectItemTable_31(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_31(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it31 As New LISAPP.ItemTableCollection

            With it31
                For i As Integer = 1 To Convert.ToInt32(Me.txtStSubCnt.Text)
                    .SetItemTable("TESTCD", 1, i, Me.txtTestCd.Text)
                    .SetItemTable("SPCCD", 2, i, "0".PadLeft(PRG_CONST.Len_SpcCd, "0"c))
                    .SetItemTable("STSUBSEQ", 3, i, i.ToString())
                    .SetItemTable("REGDT", 4, i, rsRegDT)
                    .SetItemTable("REGID", 5, i, USER_INFO.USRID)
                    .SetItemTable("STSUBNM", 6, i, CType(m_al_StSub(i - 1), StSubInfo).Name)
                    .SetItemTable("STSUBTYPE", 7, i, CType(m_al_StSub(i - 1), StSubInfo).Type)
                    .SetItemTable("IMGTYPE", 8, i, CType(m_al_StSub(i - 1), StSubInfo).ImgType)
                    .SetItemTable("IMGSIZEW", 9, i, CType(m_al_StSub(i - 1), StSubInfo).ImgSizeW)
                    .SetItemTable("IMGSIZEH", 10, i, CType(m_al_StSub(i - 1), StSubInfo).ImgSizeH)
                    .SetItemTable("STSUBRTF", 11, i, CType(m_al_StSub(i - 1), StSubInfo).RTF, OracleDbType.Clob)
                    .SetItemTable("STSUBCNT", 12, i, Me.txtStSubCnt.Text)
                    .SetItemTable("STRSTTXTR", 13, i, Me.txtStRstTxtR.Text)
                    .SetItemTable("STRSTTXTM", 14, i, Me.txtStRstTxtM.Text)
                    .SetItemTable("STRSTTXTF", 15, i, Me.txtStRstTxtF.Text)
                    .SetItemTable("STSUBEXPRG", 16, i, CType(m_al_StSub(i - 1), StSubInfo).ExPrg)
                    .SetItemTable("REGIP", 17, i, USER_INFO.LOCALIP)

                    If IsNumeric(Me.lblFirst.Text) Then
                        .SetItemTable("STSUBFIRST", 18, i, Me.lblFirst.Text)
                    Else
                        .SetItemTable("STSUBFIRST", 18, i, "")
                    End If
                Next
            End With

            fnCollectItemTable_31 = it31

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it31 As New LISAPP.ItemTableCollection
            Dim iRegType31 As Integer = 0
            Dim sRegDT As String

            iRegType31 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it31 = fnCollectItemTable_31(sRegDT)

            If mobjDAF.TransSpTestInfo(it31, iRegType31, Me.txtTestCd.Text, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal asTClsCd As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentSpTestInfo(asTClsCd)

            If dt.Rows.Count > 0 Then
                Return "검사코드 " + dt.Rows(0).Item(0).ToString + "에는 이미 특수검사 보고서 내용이 존재합니다." + vbCrLf + vbCrLf + _
                       "코드를 재조정 하십시요!!"
            Else
                Return ""
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return "Error"
        End Try
    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim dt As DataTable = mobjDAF.GetNewRegDT

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                MsgBox("시스템의 날짜를 초기화하지 못했습니다. 관리자에게 문의하시기 바랍니다!!", MsgBoxStyle.Information)
                Return Format(Now, "yyyyMMddHHmmss")

            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")
        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        Dim bErr As Boolean = False

        Try
            If Len(Me.txtTestCd.Text.Trim) < 1 Or Len(Me.txtTNmD.Text.Trim) < 1 Then
                MsgBox("검사코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Return False
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtTestCd.Text)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Return False
                    End If
                End If
            End If

            If IsNumeric(Me.txtStSubCnt.Text) = False Then
                bErr = True
            Else
                If Convert.ToInt32(Me.txtStSubCnt.Text) < 1 Then
                    bErr = True
                End If
            End If

            If bErr Then
                MsgBox("특수결과 입력구분 개수를 확인하여 주십시요!!", MsgBoxStyle.Critical)
                Return False
            End If

            '이미지만인 경우의 체크
            If Me.rdoStSubType2.Checked Then
                If IsNumeric(Ctrl.Get_Code(Me.cboImgType)) = False Then
                    MsgBox("이미지 크기를 선택하여 주십시요!!", MsgBoxStyle.Critical)
                    Return False
                End If

                '고정
                If Ctrl.Get_Code(Me.cboImgType) = "1" Then
                    If IsNumeric(Me.txtImgSizeW.Text) = False Or IsNumeric(Me.txtImgSizeH.Text) = False Then
                        MsgBox("이미지 너비와 높이를 모두 입력해 주십시요!!", MsgBoxStyle.Critical)
                        Return False
                    End If
                End If
            End If

            '현재 탭의 StSubInfo 저장
            sbStSub_Get(Convert.ToInt32(Me.tbcStSubSeq.SelectedTab.Text))

            Return True

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsTestCd As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_SpTest(rsTestCd)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_SpTest(ByVal rsTestCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_SpTest(String)"
        Dim iCol As Integer = 0

        Try
            Dim dt As New DataTable
            Dim iCurIndex As Integer = -1

            If gsModDT.Equals("") Or gsModID.Equals("") Then
                dt = mobjDAF.GetSpTestInfo(rsTestCd)
            Else
                dt = mobjDAF.GetSpTestInfo(gsModDT.Replace("-", "").Replace(":", "").Replace(" ", ""), gsModID, rsTestCd)
            End If

            '초기화
            sbInitialize()

            If dt.Rows.Count < 1 Then Return

            miSelectKey = 1

            With dt
                Me.txtTestCd.Text = .Rows(0).Item("testcd").ToString()
                Me.txtTNmD.Text = .Rows(0).Item("tnmd").ToString()

                For i As Integer = 1 To dt.Rows.Count
                    Dim si As New StSubInfo

                    si.Name = .Rows(i - 1).Item("stsubnm").ToString()
                    si.Type = .Rows(i - 1).Item("stsubtype").ToString()
                    si.ImgType = .Rows(i - 1).Item("imgtype").ToString()
                    si.ImgSizeW = .Rows(i - 1).Item("imgsizew").ToString()
                    si.ImgSizeH = .Rows(i - 1).Item("imgsizeh").ToString()
                    si.RTF = .Rows(i - 1).Item("stsubrtf").ToString()
                    si.ExPrg = .Rows(i - 1).Item("stsubexprg").ToString()

                    m_al_StSub(i - 1) = si

                    si = Nothing
                Next
            End With

            Dim a_dr As DataRow() = dt.Select("", "regdt desc")

            With a_dr(0)
                Me.txtStRstTxtR.Text = .Item("strsttxtr").ToString()
                Me.txtStRstTxtM.Text = .Item("strsttxtm").ToString()
                Me.txtStRstTxtF.Text = .Item("strsttxtf").ToString()

                Me.txtStSubCnt.Text = .Item("stsubcnt").ToString()
                miStSubCnt = Convert.ToInt32(Me.txtStSubCnt.Text)

                Me.lblFirst.Text = .Item("stsubfirst").ToString()

                '초기화
                miStSubSeq = 0

                For i As Integer = 1 To miStSubCnt
                    Me.tbcStSubSeq.TabPages.Add(New Windows.Forms.TabPage(i.ToString()))
                Next

                sbStSub_Set(1)

                Me.txtRegDT.Text = .Item("regdt").ToString()
                Me.txtRegID.Text = .Item("regid").ToString()
                Me.txtModNm.Text = .Item("modnm").ToString()
                Me.txtRegNm.Text = .Item("regnm").ToString()
            End With

            Me.txtModDT.Text = gsModDT
            Me.txtModID.Text = gsModID

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then      '권한이 있어야 "사용종료"를 할 수 있음
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

            miSelectKey = 1

            sbInitialize_Control()

            sbInitialize_DbField()

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbInitialize_Control(Optional ByVal riMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If riMode = 0 Then
                Me.txtTestCd.Text = "" : Me.txtTNmD.Text = "" : Me.btnUE.Visible = False
                Me.txtStRstTxtR.Text = "" : Me.txtStRstTxtM.Text = "" : Me.txtStRstTxtF.Text = ""
                Me.txtStSubCnt.Text = "0" : Me.tbcStSubSeq.TabPages.Clear()
                Me.txtStSubNm.Text = ""
                Me.rdoStSubType0.Checked = True : Me.cboImgType.SelectedIndex = -1 : Me.cboImgType.Enabled = False
                Me.txtImgSizeW.Text = "" : Me.txtImgSizeW.Enabled = False : Me.txtImgSizeH.Text = "" : Me.txtImgSizeH.Enabled = False
                Me.rtbSt.set_SelRTF("", True)
                Me.txtStSubExPrg.Text = ""
                Me.chkFirst.Checked = False : Me.lblFirst.Text = ""
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = ""
                Me.txtModDT.Text = "" : Me.txtModID.Text = "" : Me.txtRegNm.Text = ""

                miStSubCnt = 0

                m_al_StSub.Clear()

                For i As Integer = 1 To Me.txtStSubCnt.MaxLength * 10 - 1
                    Dim si As New StSubInfo

                    m_al_StSub.Add(si)

                    si = Nothing
                Next

                m_al_StSub.TrimToSize()
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_DbField()
        Dim sFn As String = "Private Sub sbInitialize_DbField()"

        Try
            With Me.spdDbFld
                .MaxCols = 24

                For j As Integer = 1 To .MaxCols
                    .Row = -1
                    .Col = j
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                Next

                .Row = 1

                .Col = 1
                .Text = "처방일시(16)"
                .ColID = Convert.ToChar(2) + "A" + "16".PadRight(13) + Convert.ToChar(3)

                .Col = 2
                .Text = "등록번호(" + PRG_CONST.Len_RegNo.ToString + ")"
                .ColID = Convert.ToChar(2) + "B" + PRG_CONST.Len_RegNo.ToString.PadRight(5) + Convert.ToChar(3)

                .Col = 3
                .Text = "성명(20)"
                .ColID = Convert.ToChar(2) + "C" + "20".PadRight(17) + Convert.ToChar(3)

                .Col = 4
                .Text = "Sex/Age(5)"
                .ColID = Convert.ToChar(2) + "D" + "5".PadRight(2) + Convert.ToChar(3)

                .Col = 5
                .Text = "주민등록번호(14)"
                .ColID = Convert.ToChar(2) + "E" + "14".PadRight(11) + Convert.ToChar(3)

                .Col = 6
                .Text = "의뢰의사(20)"
                .ColID = Convert.ToChar(2) + "F" + "20".PadRight(17) + Convert.ToChar(3)

                .Col = 7
                .Text = "주치의(20)"
                .ColID = Convert.ToChar(2) + "X" + "20".PadRight(17) + Convert.ToChar(3)

                .Col = 8
                .Text = "진료과(20)"
                .ColID = Convert.ToChar(2) + "G" + "20".PadRight(17) + Convert.ToChar(3)

                .Col = 9
                .Text = "병동/병실(20)"
                .ColID = Convert.ToChar(2) + "H" + "20".PadRight(17) + Convert.ToChar(3)

                .Col = 10
                .Text = "입원일(10)"
                .ColID = Convert.ToChar(2) + "I" + "10".PadRight(7) + Convert.ToChar(3)

                .Col = 11
                .Text = "검체번호(18)"
                .ColID = Convert.ToChar(2) + "J" + "18".PadRight(15) + Convert.ToChar(3)

                .Col = 12
                .Text = "검체명(30)"
                .ColID = Convert.ToChar(2) + "K" + "30".PadRight(27) + Convert.ToChar(3)

                .Col = 13
                .Text = "진단명(60)"
                .ColID = Convert.ToChar(2) + "L" + "60".PadRight(57) + Convert.ToChar(3)

                .Col = 14
                .Text = "투여약물(60)"
                .ColID = Convert.ToChar(2) + "M" + "60".PadRight(57) + Convert.ToChar(3)

                .Col = 15
                .Text = "의뢰의사 Remark(60)"
                .ColID = Convert.ToChar(2) + "N" + "60".PadRight(57) + Convert.ToChar(3)

                .Col = 16
                .Text = "채혈일시(16)"
                .ColID = Convert.ToChar(2) + "O" + "16".PadRight(13) + Convert.ToChar(3)

                .Col = 17
                .Text = "접수일시(16)"
                .ColID = Convert.ToChar(2) + "P" + "16".PadRight(13) + Convert.ToChar(3)

                .Col = 18
                .Text = "보고일시(16)"
                .ColID = Convert.ToChar(2) + "Q" + "16".PadRight(13) + Convert.ToChar(3)

                .Col = 19
                .Text = "보고자(20)"
                .ColID = Convert.ToChar(2) + "R" + "20".PadRight(17) + Convert.ToChar(3)

                .Col = 20
                .Text = "중간보고일시(16)"
                .ColID = Convert.ToChar(2) + "S" + "16".PadRight(13) + Convert.ToChar(3)

                .Col = 21
                .Text = "중간보고자(20)"
                .ColID = Convert.ToChar(2) + "T" + "17".PadRight(17) + Convert.ToChar(3)

                .Col = 22
                .Text = "사용자 의사면허(6)"
                .ColID = Convert.ToChar(2) + "U" + "3".PadRight(6) + Convert.ToChar(3)

                .Col = 23
                .Text = "소견(2000)"
                .ColID = Convert.ToChar(2) + "V" + "1997".PadRight(2000) + Convert.ToChar(3)

                .Col = 24
                .Text = "의뢰의사 면허번호(10)"
                .ColID = Convert.ToChar(2) + "W" + "7".PadRight(7) + Convert.ToChar(3)

                'Autosize
                For j As Integer = 1 To .MaxCols
                    .set_ColWidth(j, .get_MaxTextColWidth(j) + 1)
                Next
            End With
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_CtrlCollection()
        mchildctrlcol = Nothing

        mchildctrlcol = New Collection
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbInitialize()
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
    Friend WithEvents pnlFill As System.Windows.Forms.Panel
    Friend WithEvents lblGuide As System.Windows.Forms.Label
    Friend WithEvents lblImgSizeH As System.Windows.Forms.Label
    Friend WithEvents lblImgSizeW As System.Windows.Forms.Label
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents txtTNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents lblImgSize As System.Windows.Forms.Label
    Friend WithEvents pnlGbn As System.Windows.Forms.Panel
    Friend WithEvents lblSRstGbn As System.Windows.Forms.Label
    Friend WithEvents lblStRstTxtF As System.Windows.Forms.Label
    Friend WithEvents lblStRstTxtM As System.Windows.Forms.Label
    Friend WithEvents lblStRstTxtR As System.Windows.Forms.Label
    Friend WithEvents lblStRsttext As System.Windows.Forms.Label
    Friend WithEvents lblLine1 As System.Windows.Forms.Label
    Friend WithEvents lblLine2 As System.Windows.Forms.Label
    Friend WithEvents lblStSubNm As System.Windows.Forms.Label
    Friend WithEvents txtStSubCnt As System.Windows.Forms.TextBox
    Friend WithEvents txtStRstTxtF As System.Windows.Forms.TextBox
    Friend WithEvents txtStRstTxtM As System.Windows.Forms.TextBox
    Friend WithEvents txtStRstTxtR As System.Windows.Forms.TextBox
    Friend WithEvents tbcStSubSeq As System.Windows.Forms.TabControl
    Friend WithEvents txtStSubNm As System.Windows.Forms.TextBox
    Friend WithEvents rdoStSubType0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoStSubType2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoStSubType1 As System.Windows.Forms.RadioButton
    Friend WithEvents cboImgType As System.Windows.Forms.ComboBox
    Friend WithEvents txtImgSizeH As System.Windows.Forms.TextBox
    Friend WithEvents txtImgSizeW As System.Windows.Forms.TextBox
    Friend WithEvents spdDbFld As AxFPSpreadADO.AxfpSpread
    Friend WithEvents rtbSt As AxAckRichTextBox.AxAckRichTextBox
    Friend WithEvents lblStSubExPrg As System.Windows.Forms.Label
    Friend WithEvents txtStSubExPrg As System.Windows.Forms.TextBox
    Friend WithEvents chkFirst As System.Windows.Forms.CheckBox
    Friend WithEvents lblFirst As System.Windows.Forms.Label
    Friend WithEvents btnAll As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF20))
        Me.pnlFill = New System.Windows.Forms.Panel
        Me.lblStSubExPrg = New System.Windows.Forms.Label
        Me.txtStSubNm = New System.Windows.Forms.TextBox
        Me.lblStSubNm = New System.Windows.Forms.Label
        Me.lblLine2 = New System.Windows.Forms.Label
        Me.lblStRstTxtF = New System.Windows.Forms.Label
        Me.lblStRstTxtM = New System.Windows.Forms.Label
        Me.lblStRstTxtR = New System.Windows.Forms.Label
        Me.txtStRstTxtF = New System.Windows.Forms.TextBox
        Me.txtStRstTxtM = New System.Windows.Forms.TextBox
        Me.txtStRstTxtR = New System.Windows.Forms.TextBox
        Me.lblStRsttext = New System.Windows.Forms.Label
        Me.lblLine1 = New System.Windows.Forms.Label
        Me.spdDbFld = New AxFPSpreadADO.AxfpSpread
        Me.lblGuide = New System.Windows.Forms.Label
        Me.txtImgSizeH = New System.Windows.Forms.TextBox
        Me.txtImgSizeW = New System.Windows.Forms.TextBox
        Me.lblImgSizeH = New System.Windows.Forms.Label
        Me.lblImgSizeW = New System.Windows.Forms.Label
        Me.txtModID = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.txtStSubCnt = New System.Windows.Forms.TextBox
        Me.lblImgSize = New System.Windows.Forms.Label
        Me.cboImgType = New System.Windows.Forms.ComboBox
        Me.tbcStSubSeq = New System.Windows.Forms.TabControl
        Me.pnlGbn = New System.Windows.Forms.Panel
        Me.rdoStSubType0 = New System.Windows.Forms.RadioButton
        Me.rdoStSubType2 = New System.Windows.Forms.RadioButton
        Me.rdoStSubType1 = New System.Windows.Forms.RadioButton
        Me.lblSRstGbn = New System.Windows.Forms.Label
        Me.rtbSt = New AxAckRichTextBox.AxAckRichTextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.lblDbFld = New System.Windows.Forms.Label
        Me.btnCdHelp_tref = New System.Windows.Forms.Button
        Me.btnCdHelp_tsub = New System.Windows.Forms.Button
        Me.btnSelTest = New System.Windows.Forms.Button
        Me.lblSubTestHlp = New System.Windows.Forms.Label
        Me.lblTestCdHlp = New System.Windows.Forms.Label
        Me.btnPreView = New System.Windows.Forms.Button
        Me.btnAll = New System.Windows.Forms.Button
        Me.lblFirst = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.txtTNmD = New System.Windows.Forms.TextBox
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.lblTestCd = New System.Windows.Forms.Label
        Me.txtStSubExPrg = New System.Windows.Forms.TextBox
        Me.chkFirst = New System.Windows.Forms.CheckBox
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.pnlFill.SuspendLayout()
        CType(Me.spdDbFld, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGbn.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlFill
        '
        Me.pnlFill.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlFill.Controls.Add(Me.lblStSubExPrg)
        Me.pnlFill.Controls.Add(Me.txtStSubNm)
        Me.pnlFill.Controls.Add(Me.lblStSubNm)
        Me.pnlFill.Controls.Add(Me.lblLine2)
        Me.pnlFill.Controls.Add(Me.lblStRstTxtF)
        Me.pnlFill.Controls.Add(Me.lblStRstTxtM)
        Me.pnlFill.Controls.Add(Me.lblStRstTxtR)
        Me.pnlFill.Controls.Add(Me.txtStRstTxtF)
        Me.pnlFill.Controls.Add(Me.txtStRstTxtM)
        Me.pnlFill.Controls.Add(Me.txtStRstTxtR)
        Me.pnlFill.Controls.Add(Me.lblStRsttext)
        Me.pnlFill.Controls.Add(Me.lblLine1)
        Me.pnlFill.Controls.Add(Me.spdDbFld)
        Me.pnlFill.Controls.Add(Me.lblGuide)
        Me.pnlFill.Controls.Add(Me.txtImgSizeH)
        Me.pnlFill.Controls.Add(Me.txtImgSizeW)
        Me.pnlFill.Controls.Add(Me.lblImgSizeH)
        Me.pnlFill.Controls.Add(Me.lblImgSizeW)
        Me.pnlFill.Controls.Add(Me.txtModID)
        Me.pnlFill.Controls.Add(Me.lblModNm)
        Me.pnlFill.Controls.Add(Me.lblModDT)
        Me.pnlFill.Controls.Add(Me.txtRegDT)
        Me.pnlFill.Controls.Add(Me.lblUserNm)
        Me.pnlFill.Controls.Add(Me.lblRegDT)
        Me.pnlFill.Controls.Add(Me.txtRegID)
        Me.pnlFill.Controls.Add(Me.txtStSubCnt)
        Me.pnlFill.Controls.Add(Me.lblImgSize)
        Me.pnlFill.Controls.Add(Me.cboImgType)
        Me.pnlFill.Controls.Add(Me.tbcStSubSeq)
        Me.pnlFill.Controls.Add(Me.pnlGbn)
        Me.pnlFill.Controls.Add(Me.lblSRstGbn)
        Me.pnlFill.Controls.Add(Me.rtbSt)
        Me.pnlFill.Controls.Add(Me.grpCd)
        Me.pnlFill.Controls.Add(Me.txtModDT)
        Me.pnlFill.Controls.Add(Me.txtModNm)
        Me.pnlFill.Controls.Add(Me.txtRegNm)
        Me.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFill.Location = New System.Drawing.Point(0, 0)
        Me.pnlFill.Name = "pnlFill"
        Me.pnlFill.Size = New System.Drawing.Size(792, 605)
        Me.pnlFill.TabIndex = 43
        '
        'lblStSubExPrg
        '
        Me.lblStSubExPrg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStSubExPrg.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblStSubExPrg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblStSubExPrg.ForeColor = System.Drawing.Color.White
        Me.lblStSubExPrg.Location = New System.Drawing.Point(18, 537)
        Me.lblStSubExPrg.Name = "lblStSubExPrg"
        Me.lblStSubExPrg.Size = New System.Drawing.Size(153, 21)
        Me.lblStSubExPrg.TabIndex = 77
        Me.lblStSubExPrg.Text = "특수결과 연동 프로그램"
        Me.lblStSubExPrg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStSubNm
        '
        Me.txtStSubNm.Location = New System.Drawing.Point(182, 115)
        Me.txtStSubNm.MaxLength = 30
        Me.txtStSubNm.Name = "txtStSubNm"
        Me.txtStSubNm.Size = New System.Drawing.Size(588, 21)
        Me.txtStSubNm.TabIndex = 76
        '
        'lblStSubNm
        '
        Me.lblStSubNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblStSubNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblStSubNm.ForeColor = System.Drawing.Color.White
        Me.lblStSubNm.Location = New System.Drawing.Point(16, 114)
        Me.lblStSubNm.Name = "lblStSubNm"
        Me.lblStSubNm.Size = New System.Drawing.Size(165, 21)
        Me.lblStSubNm.TabIndex = 75
        Me.lblStSubNm.Text = "특수결과 입력구분 명칭"
        Me.lblStSubNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLine2
        '
        Me.lblLine2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine2.Location = New System.Drawing.Point(15, 84)
        Me.lblLine2.Name = "lblLine2"
        Me.lblLine2.Size = New System.Drawing.Size(756, 2)
        Me.lblLine2.TabIndex = 74
        '
        'lblStRstTxtF
        '
        Me.lblStRstTxtF.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblStRstTxtF.Location = New System.Drawing.Point(568, 56)
        Me.lblStRstTxtF.Name = "lblStRstTxtF"
        Me.lblStRstTxtF.Size = New System.Drawing.Size(54, 20)
        Me.lblStRstTxtF.TabIndex = 0
        Me.lblStRstTxtF.Text = "최종보고"
        Me.lblStRstTxtF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStRstTxtM
        '
        Me.lblStRstTxtM.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblStRstTxtM.Location = New System.Drawing.Point(354, 56)
        Me.lblStRstTxtM.Name = "lblStRstTxtM"
        Me.lblStRstTxtM.Size = New System.Drawing.Size(54, 20)
        Me.lblStRstTxtM.TabIndex = 0
        Me.lblStRstTxtM.Text = "중간보고"
        Me.lblStRstTxtM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStRstTxtR
        '
        Me.lblStRstTxtR.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblStRstTxtR.Location = New System.Drawing.Point(139, 56)
        Me.lblStRstTxtR.Name = "lblStRstTxtR"
        Me.lblStRstTxtR.Size = New System.Drawing.Size(54, 20)
        Me.lblStRstTxtR.TabIndex = 0
        Me.lblStRstTxtR.Text = "결과저장"
        Me.lblStRstTxtR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStRstTxtF
        '
        Me.txtStRstTxtF.Location = New System.Drawing.Point(624, 56)
        Me.txtStRstTxtF.MaxLength = 200
        Me.txtStRstTxtF.Name = "txtStRstTxtF"
        Me.txtStRstTxtF.Size = New System.Drawing.Size(148, 21)
        Me.txtStRstTxtF.TabIndex = 3
        '
        'txtStRstTxtM
        '
        Me.txtStRstTxtM.Location = New System.Drawing.Point(410, 56)
        Me.txtStRstTxtM.MaxLength = 200
        Me.txtStRstTxtM.Name = "txtStRstTxtM"
        Me.txtStRstTxtM.Size = New System.Drawing.Size(152, 21)
        Me.txtStRstTxtM.TabIndex = 2
        '
        'txtStRstTxtR
        '
        Me.txtStRstTxtR.Location = New System.Drawing.Point(195, 56)
        Me.txtStRstTxtR.MaxLength = 200
        Me.txtStRstTxtR.Name = "txtStRstTxtR"
        Me.txtStRstTxtR.Size = New System.Drawing.Size(152, 21)
        Me.txtStRstTxtR.TabIndex = 1
        '
        'lblStRsttext
        '
        Me.lblStRsttext.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblStRsttext.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblStRsttext.ForeColor = System.Drawing.Color.White
        Me.lblStRsttext.Location = New System.Drawing.Point(16, 56)
        Me.lblStRsttext.Name = "lblStRsttext"
        Me.lblStRsttext.Size = New System.Drawing.Size(116, 21)
        Me.lblStRsttext.TabIndex = 0
        Me.lblStRsttext.Text = "일반결과 대체문구"
        Me.lblStRsttext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLine1
        '
        Me.lblLine1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLine1.Location = New System.Drawing.Point(16, 48)
        Me.lblLine1.Name = "lblLine1"
        Me.lblLine1.Size = New System.Drawing.Size(756, 2)
        Me.lblLine1.TabIndex = 66
        '
        'spdDbFld
        '
        Me.spdDbFld.DataSource = Nothing
        Me.spdDbFld.Location = New System.Drawing.Point(16, 198)
        Me.spdDbFld.Name = "spdDbFld"
        Me.spdDbFld.OcxState = CType(resources.GetObject("spdDbFld.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDbFld.Size = New System.Drawing.Size(580, 35)
        Me.spdDbFld.TabIndex = 10
        '
        'lblGuide
        '
        Me.lblGuide.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblGuide.Location = New System.Drawing.Point(564, 148)
        Me.lblGuide.Name = "lblGuide"
        Me.lblGuide.Size = New System.Drawing.Size(204, 20)
        Me.lblGuide.TabIndex = 0
        Me.lblGuide.Text = "( 단위 : 픽셀,  37.8 픽셀 = 1 센티 )"
        Me.lblGuide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtImgSizeH
        '
        Me.txtImgSizeH.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtImgSizeH.Location = New System.Drawing.Point(520, 148)
        Me.txtImgSizeH.MaxLength = 4
        Me.txtImgSizeH.Name = "txtImgSizeH"
        Me.txtImgSizeH.Size = New System.Drawing.Size(40, 21)
        Me.txtImgSizeH.TabIndex = 9
        '
        'txtImgSizeW
        '
        Me.txtImgSizeW.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtImgSizeW.Location = New System.Drawing.Point(444, 148)
        Me.txtImgSizeW.MaxLength = 4
        Me.txtImgSizeW.Name = "txtImgSizeW"
        Me.txtImgSizeW.Size = New System.Drawing.Size(40, 21)
        Me.txtImgSizeW.TabIndex = 8
        '
        'lblImgSizeH
        '
        Me.lblImgSizeH.Location = New System.Drawing.Point(488, 148)
        Me.lblImgSizeH.Name = "lblImgSizeH"
        Me.lblImgSizeH.Size = New System.Drawing.Size(32, 20)
        Me.lblImgSizeH.TabIndex = 0
        Me.lblImgSizeH.Text = "높이"
        Me.lblImgSizeH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblImgSizeW
        '
        Me.lblImgSizeW.Location = New System.Drawing.Point(412, 148)
        Me.lblImgSizeW.Name = "lblImgSizeW"
        Me.lblImgSizeW.Size = New System.Drawing.Size(32, 20)
        Me.lblImgSizeW.TabIndex = 0
        Me.lblImgSizeW.Text = "너비"
        Me.lblImgSizeW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(299, 574)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(68, 21)
        Me.txtModID.TabIndex = 58
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        Me.txtModID.Visible = False
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(213, 574)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(85, 21)
        Me.lblModNm.TabIndex = 0
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(6, 574)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(85, 20)
        Me.lblModDT.TabIndex = 0
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(504, 574)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 52
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(625, 574)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(85, 21)
        Me.lblUserNm.TabIndex = 0
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(418, 574)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(85, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(711, 574)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 53
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'txtStSubCnt
        '
        Me.txtStSubCnt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtStSubCnt.Location = New System.Drawing.Point(132, 92)
        Me.txtStSubCnt.MaxLength = 1
        Me.txtStSubCnt.Name = "txtStSubCnt"
        Me.txtStSubCnt.Size = New System.Drawing.Size(20, 21)
        Me.txtStSubCnt.TabIndex = 4
        Me.txtStSubCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblImgSize
        '
        Me.lblImgSize.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblImgSize.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblImgSize.ForeColor = System.Drawing.Color.White
        Me.lblImgSize.Location = New System.Drawing.Point(247, 148)
        Me.lblImgSize.Name = "lblImgSize"
        Me.lblImgSize.Size = New System.Drawing.Size(76, 20)
        Me.lblImgSize.TabIndex = 0
        Me.lblImgSize.Text = "이미지 크기"
        Me.lblImgSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboImgType
        '
        Me.cboImgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboImgType.Items.AddRange(New Object() {"[0] 자동", "[1] 고정"})
        Me.cboImgType.Location = New System.Drawing.Point(328, 148)
        Me.cboImgType.Name = "cboImgType"
        Me.cboImgType.Size = New System.Drawing.Size(76, 20)
        Me.cboImgType.TabIndex = 7
        '
        'tbcStSubSeq
        '
        Me.tbcStSubSeq.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.tbcStSubSeq.HotTrack = True
        Me.tbcStSubSeq.Location = New System.Drawing.Point(185, 92)
        Me.tbcStSubSeq.Name = "tbcStSubSeq"
        Me.tbcStSubSeq.SelectedIndex = 0
        Me.tbcStSubSeq.Size = New System.Drawing.Size(584, 19)
        Me.tbcStSubSeq.TabIndex = 5
        '
        'pnlGbn
        '
        Me.pnlGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.pnlGbn.Controls.Add(Me.rdoStSubType0)
        Me.pnlGbn.Controls.Add(Me.rdoStSubType2)
        Me.pnlGbn.Controls.Add(Me.rdoStSubType1)
        Me.pnlGbn.ForeColor = System.Drawing.Color.Black
        Me.pnlGbn.Location = New System.Drawing.Point(16, 148)
        Me.pnlGbn.Name = "pnlGbn"
        Me.pnlGbn.Size = New System.Drawing.Size(230, 20)
        Me.pnlGbn.TabIndex = 6
        '
        'rdoStSubType0
        '
        Me.rdoStSubType0.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rdoStSubType0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoStSubType0.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoStSubType0.ForeColor = System.Drawing.Color.Black
        Me.rdoStSubType0.Location = New System.Drawing.Point(12, 1)
        Me.rdoStSubType0.Name = "rdoStSubType0"
        Me.rdoStSubType0.Size = New System.Drawing.Size(52, 18)
        Me.rdoStSubType0.TabIndex = 0
        Me.rdoStSubType0.Text = "일반"
        Me.rdoStSubType0.UseVisualStyleBackColor = False
        '
        'rdoStSubType2
        '
        Me.rdoStSubType2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rdoStSubType2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoStSubType2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoStSubType2.ForeColor = System.Drawing.Color.Black
        Me.rdoStSubType2.Location = New System.Drawing.Point(152, 1)
        Me.rdoStSubType2.Name = "rdoStSubType2"
        Me.rdoStSubType2.Size = New System.Drawing.Size(72, 18)
        Me.rdoStSubType2.TabIndex = 2
        Me.rdoStSubType2.Text = "이미지만"
        Me.rdoStSubType2.UseVisualStyleBackColor = False
        '
        'rdoStSubType1
        '
        Me.rdoStSubType1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rdoStSubType1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoStSubType1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoStSubType1.ForeColor = System.Drawing.Color.Black
        Me.rdoStSubType1.Location = New System.Drawing.Point(72, 1)
        Me.rdoStSubType1.Name = "rdoStSubType1"
        Me.rdoStSubType1.Size = New System.Drawing.Size(72, 18)
        Me.rdoStSubType1.TabIndex = 1
        Me.rdoStSubType1.Text = "텍스트만"
        Me.rdoStSubType1.UseVisualStyleBackColor = False
        '
        'lblSRstGbn
        '
        Me.lblSRstGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSRstGbn.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSRstGbn.ForeColor = System.Drawing.Color.White
        Me.lblSRstGbn.Location = New System.Drawing.Point(16, 92)
        Me.lblSRstGbn.Name = "lblSRstGbn"
        Me.lblSRstGbn.Size = New System.Drawing.Size(165, 21)
        Me.lblSRstGbn.TabIndex = 0
        Me.lblSRstGbn.Text = "특수결과 입력구분     (개)"
        Me.lblSRstGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rtbSt
        '
        Me.rtbSt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rtbSt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rtbSt.Location = New System.Drawing.Point(16, 241)
        Me.rtbSt.Name = "rtbSt"
        Me.rtbSt.Size = New System.Drawing.Size(726, 294)
        Me.rtbSt.TabIndex = 11
        '
        'grpCd
        '
        Me.grpCd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.lblDbFld)
        Me.grpCd.Controls.Add(Me.btnCdHelp_tref)
        Me.grpCd.Controls.Add(Me.btnCdHelp_tsub)
        Me.grpCd.Controls.Add(Me.btnSelTest)
        Me.grpCd.Controls.Add(Me.lblSubTestHlp)
        Me.grpCd.Controls.Add(Me.lblTestCdHlp)
        Me.grpCd.Controls.Add(Me.btnPreView)
        Me.grpCd.Controls.Add(Me.btnAll)
        Me.grpCd.Controls.Add(Me.lblFirst)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.txtTNmD)
        Me.grpCd.Controls.Add(Me.txtTestCd)
        Me.grpCd.Controls.Add(Me.lblTestCd)
        Me.grpCd.Controls.Add(Me.txtStSubExPrg)
        Me.grpCd.Controls.Add(Me.chkFirst)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(772, 560)
        Me.grpCd.TabIndex = 0
        Me.grpCd.TabStop = False
        '
        'lblDbFld
        '
        Me.lblDbFld.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDbFld.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDbFld.ForeColor = System.Drawing.Color.White
        Me.lblDbFld.Location = New System.Drawing.Point(8, 172)
        Me.lblDbFld.Name = "lblDbFld"
        Me.lblDbFld.Size = New System.Drawing.Size(165, 21)
        Me.lblDbFld.TabIndex = 185
        Me.lblDbFld.Text = "데이터베이스 필드 참조"
        Me.lblDbFld.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCdHelp_tref
        '
        Me.btnCdHelp_tref.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_tref.Image = CType(resources.GetObject("btnCdHelp_tref.Image"), System.Drawing.Image)
        Me.btnCdHelp_tref.Location = New System.Drawing.Point(659, 212)
        Me.btnCdHelp_tref.Name = "btnCdHelp_tref"
        Me.btnCdHelp_tref.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_tref.TabIndex = 184
        Me.btnCdHelp_tref.UseVisualStyleBackColor = True
        '
        'btnCdHelp_tsub
        '
        Me.btnCdHelp_tsub.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_tsub.Image = CType(resources.GetObject("btnCdHelp_tsub.Image"), System.Drawing.Image)
        Me.btnCdHelp_tsub.Location = New System.Drawing.Point(659, 190)
        Me.btnCdHelp_tsub.Name = "btnCdHelp_tsub"
        Me.btnCdHelp_tsub.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_tsub.TabIndex = 183
        Me.btnCdHelp_tsub.UseVisualStyleBackColor = True
        '
        'btnSelTest
        '
        Me.btnSelTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSelTest.Image = CType(resources.GetObject("btnSelTest.Image"), System.Drawing.Image)
        Me.btnSelTest.Location = New System.Drawing.Point(153, 16)
        Me.btnSelTest.Name = "btnSelTest"
        Me.btnSelTest.Size = New System.Drawing.Size(26, 21)
        Me.btnSelTest.TabIndex = 182
        Me.btnSelTest.UseVisualStyleBackColor = True
        '
        'lblSubTestHlp
        '
        Me.lblSubTestHlp.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSubTestHlp.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSubTestHlp.ForeColor = System.Drawing.Color.Black
        Me.lblSubTestHlp.Location = New System.Drawing.Point(594, 212)
        Me.lblSubTestHlp.Name = "lblSubTestHlp"
        Me.lblSubTestHlp.Size = New System.Drawing.Size(64, 21)
        Me.lblSubTestHlp.TabIndex = 174
        Me.lblSubTestHlp.Text = "연관검사"
        Me.lblSubTestHlp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTestCdHlp
        '
        Me.lblTestCdHlp.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTestCdHlp.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCdHlp.ForeColor = System.Drawing.Color.Black
        Me.lblTestCdHlp.Location = New System.Drawing.Point(594, 190)
        Me.lblTestCdHlp.Name = "lblTestCdHlp"
        Me.lblTestCdHlp.Size = New System.Drawing.Size(64, 21)
        Me.lblTestCdHlp.TabIndex = 78
        Me.lblTestCdHlp.Text = "검사항목"
        Me.lblTestCdHlp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnPreView
        '
        Me.btnPreView.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPreView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreView.Location = New System.Drawing.Point(534, 532)
        Me.btnPreView.Name = "btnPreView"
        Me.btnPreView.Size = New System.Drawing.Size(96, 24)
        Me.btnPreView.TabIndex = 98
        Me.btnPreView.Text = "미리보기"
        '
        'btnAll
        '
        Me.btnAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAll.Location = New System.Drawing.Point(636, 532)
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(96, 24)
        Me.btnAll.TabIndex = 97
        Me.btnAll.Text = "전체보기(+A)"
        '
        'lblFirst
        '
        Me.lblFirst.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFirst.Location = New System.Drawing.Point(436, 534)
        Me.lblFirst.Name = "lblFirst"
        Me.lblFirst.Size = New System.Drawing.Size(20, 17)
        Me.lblFirst.TabIndex = 96
        Me.lblFirst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFirst.Visible = False
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(692, 13)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 95
        Me.btnUE.Text = "사용종료"
        Me.btnUE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'txtTNmD
        '
        Me.txtTNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTNmD.Location = New System.Drawing.Point(180, 16)
        Me.txtTNmD.Name = "txtTNmD"
        Me.txtTNmD.ReadOnly = True
        Me.txtTNmD.Size = New System.Drawing.Size(486, 21)
        Me.txtTNmD.TabIndex = 1
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestCd.Location = New System.Drawing.Point(80, 16)
        Me.txtTestCd.MaxLength = 7
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(72, 21)
        Me.txtTestCd.TabIndex = 0
        Me.txtTestCd.Tag = "TESTCD"
        '
        'lblTestCd
        '
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(8, 16)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(71, 21)
        Me.lblTestCd.TabIndex = 7
        Me.lblTestCd.Text = "검사코드"
        Me.lblTestCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtStSubExPrg
        '
        Me.txtStSubExPrg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtStSubExPrg.Location = New System.Drawing.Point(164, 533)
        Me.txtStSubExPrg.MaxLength = 30
        Me.txtStSubExPrg.Name = "txtStSubExPrg"
        Me.txtStSubExPrg.Size = New System.Drawing.Size(140, 21)
        Me.txtStSubExPrg.TabIndex = 78
        '
        'chkFirst
        '
        Me.chkFirst.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkFirst.Location = New System.Drawing.Point(309, 535)
        Me.chkFirst.Name = "chkFirst"
        Me.chkFirst.Size = New System.Drawing.Size(124, 18)
        Me.chkFirst.TabIndex = 79
        Me.chkFirst.Text = "시작 탭으로 설정"
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(92, 574)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 56
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(299, 574)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 184
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(711, 574)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 78
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'FDF20
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlFill)
        Me.Name = "FDF20"
        Me.Text = "[20] 특수검사 보고서"
        Me.pnlFill.ResumeLayout(False)
        Me.pnlFill.PerformLayout()
        CType(Me.spdDbFld, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGbn.ResumeLayout(False)
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbStSub_Get(ByVal aiSeq As Integer)
        If aiSeq < 1 Then Return

        Dim si As New StSubInfo

        si.Name = Me.txtStSubNm.Text

        If Me.rdoStSubType0.Checked Then
            si.Type = "0"
        ElseIf Me.rdoStSubType1.Checked Then
            si.Type = "1"
        ElseIf Me.rdoStSubType2.Checked Then
            si.Type = "2"
        End If

        '0 : 일반, 1 : 텍스트만, 2 : 이미지만
        Select Case si.Type
            Case "0", "1"
                si.RTF = Me.rtbSt.get_SelRTF(True).Trim

            Case "2"
                si.ImgType = Ctrl.Get_Code(Me.cboImgType)
                si.ImgSizeW = Me.txtImgSizeW.Text
                si.ImgSizeH = Me.txtImgSizeH.Text

        End Select

        si.ExPrg = Me.txtStSubExPrg.Text

        m_al_StSub(aiSeq - 1) = si

        si = Nothing
    End Sub

    Private Sub sbStSub_Set(ByVal aiSeq As Integer)
        If aiSeq < 1 Then Return

        Me.txtStSubNm.Text = CType(m_al_StSub(aiSeq - 1), StSubInfo).Name

        Dim rdo As Windows.Forms.RadioButton

        '0 : 일반, 1 : 텍스트만, 2 : 이미지만
        Select Case CType(m_al_StSub(aiSeq - 1), StSubInfo).Type
            Case "0"
                rdo = Me.rdoStSubType0

            Case "1"
                rdo = Me.rdoStSubType1

            Case "2"
                rdo = Me.rdoStSubType2

            Case Else
                rdo = Me.rdoStSubType0

        End Select

        miSelectKey = 1 : rdo.Checked = True : miSelectKey = 0

        rdoStSubType_CheckedChanged(rdo, Nothing)

        '0 : 일반, 1 : 텍스트만, 2 : 이미지만
        Select Case CType(m_al_StSub(aiSeq - 1), StSubInfo).Type
            Case "0", "1"
                Me.rtbSt.set_SelRTF(CType(m_al_StSub(aiSeq - 1), StSubInfo).RTF, True)

            Case "2"
                miSelectKey = 1
                Me.cboImgType.SelectedIndex = Convert.ToInt32(CType(m_al_StSub(aiSeq - 1), StSubInfo).ImgType)
                miSelectKey = 0

                cboImgType_SelectedIndexChanged(Nothing, Nothing)

                '0 : 자동, 1 : 고정
                If CType(m_al_StSub(aiSeq - 1), StSubInfo).ImgType = "1" Then
                    Me.txtImgSizeW.Text = CType(m_al_StSub(aiSeq - 1), StSubInfo).ImgSizeW
                    Me.txtImgSizeH.Text = CType(m_al_StSub(aiSeq - 1), StSubInfo).ImgSizeH
                End If

        End Select

        Me.txtStSubExPrg.Text = CType(m_al_StSub(aiSeq - 1), StSubInfo).ExPrg

        If Me.lblFirst.Text = aiSeq.ToString() Then
            Me.chkFirst.Checked = True
        Else
            Me.chkFirst.Checked = False
        End If

        '이전 StSubSeq 할당
        miStSubSeq = aiSeq
    End Sub

    Private Sub sbStSub_View(ByVal aiCnt As Integer)
        Dim sRTF As String = "", sRTF_All As String = ""

        If Me.tbcStSubSeq.TabPages.Count > 0 Then
            '현재 탭의 StSubInfo 저장
            sbStSub_Get(Convert.ToInt32(Me.tbcStSubSeq.SelectedTab.Text))
        End If

        For i As Integer = 1 To aiCnt
            sRTF = CType(m_al_StSub(i - 1), StSubInfo).RTF.Trim

            If aiCnt = 1 Then
                sRTF_All = sRTF

                Exit For
            End If

            If i = 1 Then
                '맨 마지막 제거
                sRTF_All += sRTF.Substring(0, sRTF.Length - 1)

            ElseIf i = aiCnt Then
                '맨 처음 제거
                sRTF_All += sRTF.Substring(1)

            Else
                '맨 처음과 맨 마지막 제거
                If sRTF.Length > 2 Then
                    sRTF_All += sRTF.Substring(1, sRTF.Length - 2)
                End If
            End If
        Next

        Dim fdf20 As New FDF20

        fdf20.rtbSt.set_SelRTF(sRTF_All, True)
        fdf20.rtbSt.set_Lock(True)

        fdf20.ShowDialog(Me)
    End Sub

    '<----- Control Event ----->
    Private Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        If Me.tbcStSubSeq.TabPages.Count < 1 Then Return

        sbStSub_View(Me.tbcStSubSeq.TabPages.Count)
    End Sub

    Private Sub cboImgType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboImgType.SelectedIndexChanged
        If miSelectKey = 1 Then Return

        Dim sType As String = Ctrl.Get_Code(Me.cboImgType)

        If sType = "" Then Return

        '0 : 자동, 1 : 고정
        Select Case sType
            Case "0"
                Me.txtImgSizeW.Text = "" : Me.txtImgSizeW.Enabled = False : Me.txtImgSizeH.Text = "" : Me.txtImgSizeH.Enabled = False

            Case "1"
                Me.txtImgSizeW.Text = "" : Me.txtImgSizeW.Enabled = True : Me.txtImgSizeH.Text = "" : Me.txtImgSizeH.Enabled = True

        End Select
    End Sub

    Private Sub chkFirst_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFirst.CheckedChanged
        If miSelectKey = 1 Then Return

        If Me.tbcStSubSeq.TabPages.Count = 0 Then Return

        If Me.chkFirst.Checked Then
            Me.lblFirst.Text = Me.tbcStSubSeq.SelectedTab.Text
        Else
            If Me.lblFirst.Text = Me.tbcStSubSeq.SelectedTab.Text Then
                Me.lblFirst.Text = ""
            End If
        End If
    End Sub

    Private Sub rdoStSubType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoStSubType0.CheckedChanged, rdoStSubType1.CheckedChanged, rdoStSubType2.CheckedChanged
        If miSelectKey = 1 Then Return

        Dim sType As String = CType(sender, Windows.Forms.RadioButton).Name.Substring(12, 1)

        '초기화
        Me.rtbSt.set_SelRTF("", True)
        Me.txtStSubExPrg.Text = ""

        '0 : 일반, 1 : 텍스트만, 2 : 이미지만
        Select Case sType
            Case "0"
                Me.cboImgType.SelectedIndex = -1 : Me.cboImgType.Enabled = False

                Me.rtbSt.Enabled = True

                Me.rtbSt.set_Change_ButtonState_Image(True)

                Me.txtStSubExPrg.Enabled = True

            Case "1"
                Me.cboImgType.SelectedIndex = -1 : Me.cboImgType.Enabled = False

                Me.rtbSt.Enabled = True

                Me.rtbSt.set_Change_ButtonState_Image(False)

                Me.txtStSubExPrg.Enabled = True

            Case "2"
                Me.cboImgType.SelectedIndex = 0 : Me.cboImgType.Enabled = True

                Me.rtbSt.Enabled = False

                '-- 2008/02/26 YEJ 막음
                'Me.txtStSubExPrg.Enabled = False

        End Select
    End Sub

    Private Sub spdDbFld_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdDbFld.DblClick
        With Me.spdDbFld
            .Col = e.col
            Dim sColID As String = .ColID

            Me.rtbSt.set_SelText(sColID)
            Me.rtbSt.set_Focus()
        End With
    End Sub

    Private Sub txtOnlyNumber(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStSubCnt.KeyPress, txtImgSizeH.KeyPress, txtImgSizeW.KeyPress
        If Char.IsControl(e.KeyChar) Or Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtStSubCnt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStSubCnt.TextChanged
        If miSelectKey = 1 Then Return

        Dim iStSubCnt As Integer = -1

        Try
            If IsNumeric(Me.txtStSubCnt.Text) = False Then
                Return
            End If

            iStSubCnt = Convert.ToInt32(Me.txtStSubCnt.Text)

            If iStSubCnt = miStSubCnt Then Return

            If Me.tbcStSubSeq.TabPages.Count > 0 Then
                '현재 탭의 StSubInfo 저장
                sbStSub_Get(Convert.ToInt32(Me.tbcStSubSeq.SelectedTab.Text))
            End If

            'TabPages 제거시 SelectedIndexChanged 이벤트 발생됨
            miSelectKey = 1
            Me.tbcStSubSeq.TabPages.Clear()
            miSelectKey = 0

            '초기화
            miStSubSeq = 0

            For i As Integer = 1 To iStSubCnt
                Me.tbcStSubSeq.TabPages.Add(New Windows.Forms.TabPage(i.ToString()))
            Next

        Catch ex As Exception

        Finally
            miStSubCnt = iStSubCnt
        End Try
    End Sub

    Private Sub txtStSubCnt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtStSubCnt.Validating
        If miSelectKey = 1 Then Return

        If IsNumeric(Me.txtStSubCnt.Text) = False Then
            miStSubCnt = -1
            Me.txtStSubCnt.Text = "0"
            txtStSubCnt_TextChanged(Nothing, Nothing)
        End If
    End Sub

     Private Sub tbcStSubSeq_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcStSubSeq.SelectedIndexChanged
        If miSelectKey = 1 Then Return

        If CType(sender, Windows.Forms.TabControl).TabPages.Count = 0 Then Return

        Dim iStSubSeq As Integer = Convert.ToInt32(CType(sender, Windows.Forms.TabControl).SelectedTab.Text)

        Try
            '이전 StSubSeq의 내용 저장
            If miStSubSeq > 0 Then
                sbStSub_Get(miStSubSeq)
            End If

            sbStSub_Set(iStSubSeq)

        Catch ex As Exception

        Finally
            miStSubSeq = iStSubSeq

        End Try
    End Sub

    Private Sub tbcStSubSeq_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles tbcStSubSeq.ControlAdded
        If miSelectKey = 1 Then Return

        If CType(sender, Windows.Forms.TabControl).TabPages.Count > 1 Then Return

        sbStSub_Set(1)
    End Sub

    Private Sub btnPreView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreView.Click
        If Me.tbcStSubSeq.TabPages.Count < 1 Then Return

        sbStSub_View2(Me.tbcStSubSeq.TabPages.Count)
    End Sub

    Private Sub sbStSub_View2(ByVal aiCnt As Integer)
        Dim sRTF As String = "", sRTF_All As String = ""

        If Me.tbcStSubSeq.TabPages.Count > 0 Then
            '현재 탭의 StSubInfo 저장
            sbStSub_Get(Convert.ToInt32(Me.tbcStSubSeq.SelectedTab.Text))
        End If

        For i As Integer = 1 To aiCnt
            sRTF = CType(m_al_StSub(i - 1), StSubInfo).RTF.Trim

            If aiCnt = 1 Then
                sRTF_All = sRTF

                Exit For
            End If

            If i = 1 Then
                '맨 마지막 제거
                sRTF_All += sRTF.Substring(0, sRTF.Length - 1)

            ElseIf i = aiCnt Then
                '맨 처음 제거
                sRTF_All += sRTF.Substring(1)

            Else
                '맨 처음과 맨 마지막 제거
                If sRTF.Length > 2 Then
                    sRTF_All += sRTF.Substring(1, sRTF.Length - 2)
                End If
            End If
        Next

        Dim fdf20_1 As New FDF20_1

        fdf20_1.rtbSt.set_SelRTF(sRTF_All, True)
        fdf20_1.rtbSt.set_Lock(True)
        fdf20_1.rtbPrint.Rtf = sRTF_All

        'fdf20_1.sbImgMake()
        fdf20_1.ShowDialog()
    End Sub

    Private Sub btnCdHelp_tsub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp_tsub.Click
        Try

            Dim iHeight As Integer = Convert.ToInt32(btnCdHelp_tsub.Height)
            Dim iWidth As Integer = Convert.ToInt32(btnCdHelp_tsub.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Me.Top + Me.btnCdHelp_tsub.Top + Ctrl.menuHeight - 50

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = CType(Me.Owner, FGF01).Width + Me.Left + Me.btnCdHelp_tsub.Left
            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - Me.btnCdHelp_tsub.Width)

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_WithParent(Me.txtTestCd.Text)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "검사항목 코드"
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "", 0, , , True)
            objHelp.AddField("tnmp", "", 0, , , True)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                Dim str As String = Convert.ToChar(2) + "Z" + alList.Item(0).ToString.Split("|"c)(0) + Convert.ToChar(3)

                Me.rtbSt.set_SelText(str)
                Me.rtbSt.set_Focus()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCdHelp_tref_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp_tref.Click
        Try

            Dim iHeight As Integer = Convert.ToInt32(btnCdHelp_tsub.Height)
            Dim iWidth As Integer = Convert.ToInt32(btnCdHelp_tsub.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Me.Top + Me.btnCdHelp_tref.Top + Ctrl.menuHeight - 50

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = CType(Me.Owner, FGF01).Width + Me.Left + Me.btnCdHelp_tref.Left
            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - Me.btnCdHelp_tref.Width)

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_WithReference(Me.txtTestCd.Text)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "검사항목 코드"
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                Dim strTmp As String = alList.Item(0).ToString.Split("|"c)(0).PadLeft(7, " "c) + alList.Item(0).ToString.Split("|"c)(1)
                Dim str As String = Convert.ToChar(2) + "Y" + strTmp + Convert.ToChar(3)

                Me.rtbSt.set_SelText(str)
                Me.rtbSt.set_Focus()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        If txtTestCd.Text = "" Then Exit Sub

        Try
            Dim sMsg As String = "검사코드   : " + Me.txtTestCd.Text + vbCrLf
            sMsg += "검사명     : " + Me.txtTNmD.Text + vbCrLf
            sMsg += "의 특수검사 보고서를 사용종료하시겠습니까?"

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

            If mobjDAF.TransSpTestInfo_UE(Me.txtTestCd.Text, USER_INFO.USRID) Then
                MsgBox("해당 검사의 특수검사 보고서가 사용종료 되었습니다!!", MsgBoxStyle.Information)

                sbInitialize()
                CType(Me.Owner, FGF01).sbDeleteCdList()
            Else
                MsgBox("사용종료에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub FDF20_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnSelTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelTest.Click
        Try

            Dim iHeight As Integer = Convert.ToInt32(btnCdHelp_tsub.Height)
            Dim iWidth As Integer = Convert.ToInt32(btnCdHelp_tsub.Width)

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = 140 'Me.Top + Me.btnTCLSCDHlp.Top + Ctrl.menuHeight - 50

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = 1200 'CType(Me.Owner, FGF01).Width + Me.Left + Me.btnTCLSCDHlp.Left
            'Left --> 오른쪽에 맞춰지도록 설정
            iLeft = iLeft - (iWidth - Me.btnCdHelp_tsub.Width)

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_ParentSingle("", "", Me.txtTestCd.Text)
            Dim sSql As String = "ctgbn = '1' AND tcdgbn <> 'C'"

            If Me.txtTestCd.Text <> "" Then
                sSql += " AND testcd = '" + Me.txtTestCd.Text + "'"
            End If

            Dim a_dr As DataRow() = dt.Select(sSql, "")

            dt = Fn.ChangeToDataTable(a_dr)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            objHelp.FormText = "검사항목 코드"

            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "", 0, , , True)
            objHelp.AddField("tnmp", "", 0, , , True)
            objHelp.AddField("exlabnmd", "위탁기관명", 16, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then
                Dim testcd As String = alList.Item(0).ToString.Split("|"c)(0)
                Dim tnm As String = alList.Item(0).ToString.Split("|"c)(1)

                Me.txtTestCd.Text = testcd
                Me.txtTNmD.Text = tnm

                Me.rtbSt.set_Focus()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtTestCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestCd.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        Me.txtTNmD.Text = ""
        If Me.txtTestCd.Text = "" Then Return
        btnSelTest_Click(Nothing, Nothing)

    End Sub

 End Class

Public Class StSubInfo
    Public Name As String = ""
    Public Type As String = ""
    Public ImgType As String = ""
    Public ImgSizeW As String = ""
    Public ImgSizeH As String = ""
    Public RTF As String = ""
    Public ExPrg As String = ""
End Class