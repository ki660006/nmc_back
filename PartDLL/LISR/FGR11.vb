'>>> 보관 검체 관리

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_DB
Imports LISAPP.APP_KS.KsFn
Imports LISAPP.APP_KS.ExecFn

Public Class FGR11
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : R01.dll, Class : FGR11" & vbTab

    Dim m_KeepBcno As New STU_KsRack
    Dim m_ToKeepBcno As New STU_KsRack
    Dim miMaxRow As Integer
    Dim miMaxCol As Integer
    Dim msToRackID As String = ""      ' 보관검체 이동시 옮길 rackid
    Dim msRealBcno As String = ""       ' 실제로 넘겨줄 완벽한 검체번호의 형태
    Dim msComment As String = ""       ' 클릭이벤트 발생했을때 해당 검체가 가지고 있는 보관 Comment (LK010M의 OTHER)
    Dim miIdx_RackId As Integer
    Dim COM_01 As New COMMON.CommFN.Fn
    Dim m_Comm As New ServerDateTime
    Dim miClick_Row As Integer = 0
    Dim miClick_Col As Integer = 0

    Private mbMicroBio As Boolean = False
    Private m_tooltip As New Windows.Forms.ToolTip

    Private Const msXMLDir As String = "\XML"
    Private msBCCLS As String = Application.StartupPath + msXMLDir & "\FGR11_BCCLSINFO.XML"

    Dim mi_DownRow As Integer = 0
    Dim mi_DownCol As Integer = 0       ' MouseDownEvent 가 발생했을때 해당 col 저장

    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboBccls As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents spdPatInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtWorkNo As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnSpcView As System.Windows.Forms.Button
    Friend WithEvents lblSpcCds As System.Windows.Forms.Label
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents btnNo As System.Windows.Forms.Button
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnDiscard_All As CButtonLib.CButton
    Friend WithEvents btnDiscard As CButtonLib.CButton
    ' MouseDownEvent 가 발생했을때 해당 row 저장

    Private Sub sbDisplay_KeepInfo()
        sbClearData_All()

        Dim dt As DataTable = fnGet_KsRackInfo(Me.cboRackID.Text, Ctrl.Get_Code(Me.cboBccls))

        If dt.Rows.Count > 0 Then
            Me.lblSpcCds.Text = ""
            For IX As Integer = 0 To dt.Rows.Count - 1
                Me.lblSpcCds.Text += dt.Rows(IX).Item("spccd").ToString + "|"
            Next

            With dt.Rows(0)
                m_KeepBcno.Bcclscd = .Item("bcclscd").ToString()
                m_KeepBcno.RackId = .Item("rackid").ToString()
                m_KeepBcno.SpcCd = .Item("spccd").ToString()
                m_KeepBcno.RegDt = .Item("regdt").ToString()
                m_KeepBcno.RegId = .Item("regid").ToString()
                Me.txtMRow.Text = .Item("maxrow").ToString()
                Me.txtMCol.Text = .Item("maxcol").ToString()

                m_KeepBcno.AlarmTerm = .Item("alarmterm").ToString() : Me.txtAlarm.Text = m_KeepBcno.AlarmTerm

            End With

            With Me.spdManage
                .MaxRows = CType(txtMRow.Text.Trim, Integer) : miMaxRow = .MaxRows
                .MaxCols = CType(txtMCol.Text.Trim, Integer) : miMaxCol = .MaxCols
                .Refresh()  ' 안해도 무방함 ㅡ.ㅡa

                .ClearRange(1, 1, .MaxCols, .MaxRows, True)  ' 새 화면으로 clear
                .BlockMode = True
                .Col = 1 : .Col2 = .MaxCols : .Row = 1 : .Row2 = .MaxRows
                .BackColor = System.Drawing.Color.White
                .BlockMode = False

                .ReDraw = False

                Me.cboToCol.Items.Clear()
                Me.cboToRow.Items.Clear()
                For i As Integer = 1 To .MaxRows
                    For j As Integer = 1 To .MaxCols
                        .set_ColWidth(j, 13)
                        .set_RowHeight(i, 40)

                        cboToCol.Items.Add(j.ToString)

                    Next

                    cboToRow.Items.Add(i.ToString)
                Next

                .ReDraw = True

            End With

            ' spread에 보관 검체들을 보여준다. 
            dt = fnGet_KsBcnoInfo(m_KeepBcno)

            If dt.Rows.Count > 0 Then
                sbShwo_BcnoList(dt)
            Else
                Exit Sub
            End If
        Else
            MsgBox("데이터가 존재하지 않습니다. 다시 확인하세요", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub


    Private Function fnGetColRow(ByVal aiX As Integer, ByVal aiY As Integer) As RowCol
        Dim sfn As String = "Private Function fnGetColRow(ByVal aiX As Integer, ByVal aiY As Integer) As RowCol"

        Try
            Dim CR As New RowCol
            Dim SpdWidth As Integer = spdManage.Width - 22 - 18
            Dim SpdHeight As Integer = spdManage.Height - 23 - 18
            Dim intCellWidth As Integer
            Dim intCellHeight As Integer

            With CR
                intCellWidth = CInt(Fix(SpdWidth / 10))
                intCellHeight = CInt(Fix(SpdHeight / 10))

                'intCellWidth = CInt(Fix(SpdWidth / spdManage.MaxCols))
                'intCellHeight = CInt(Fix(SpdHeight / spdManage.MaxRows))
                'Debug.WriteLine(" Cell W, H : " & intCellWidth.ToString & ", " & intCellHeight.ToString)

                .Col = CInt(Fix((aiX - 22) / intCellWidth)) + 1
                .Row = CInt(Fix((aiY - 23) / intCellHeight)) + 1
            End With

            fnGetColRow = CR

        Catch ex As Exception
            Fn.log(msFile & sfn, Err)

        End Try

    End Function

    Private Sub sbDisplay_Bccls()
        Dim sFn As String = "sbDisplay_Bccls"

        Try
            Me.cboBccls.Items.Clear()

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Bccls_List()

            For i As Integer = 1 To dt.Rows.Count
                Me.cboBccls.Items.Add("[" + dt.Rows(i - 1).Item("bcclscd").ToString() + "] " + dt.Rows(i - 1).Item("bcclsnmd").ToString())
            Next

            Dim sTmp As String = ""

            sTmp = COMMON.CommXML.getOneElementXML(msXMLDir, msBCCLS, "BCCLS")

            If Val(sTmp) > Me.cboBccls.Items.Count Then
                Me.cboBccls.SelectedIndex = 0
            Else
                Me.cboBccls.SelectedIndex = CInt(IIf(sTmp = "", 0, sTmp))
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbGet_RackID(ByVal rsBcclscd As String)        ' RackID 가져오기

        Dim dt As DataTable = fnGet_KsRackInfo("", rsBcclscd)

        Me.cboRackID.Items.Clear()
        Me.cboToRack_ID.Items.Clear()

        Me.lblSpcCds.Text = ""

        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1
                If Me.cboRackID.Items.Contains(dt.Rows(ix).Item("rackid").ToString().Trim) Then
                Else
                    Me.cboRackID.Items.Add(dt.Rows(ix).Item("rackid").ToString().Trim)
                    Me.cboToRack_ID.Items.Add(dt.Rows(ix).Item("rackid").ToString().Trim)
                End If

                '-- 저장가능 검체
                Me.lblSpcCds.Text += dt.Rows(ix).Item("spccd").ToString + "|"
            Next
        Else
            MsgBox("보관검체 Rack ID를 가져오지 못했습니다", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

    End Sub

    Private Sub sbDisplayInit_grpPatInfo()
        Dim sFn As String = "sbDisplayInit_grpPatInfo"

        Try
            'spdPatInfo
            With Me.spdPatInfo
                .ClearRange(1, 1, .MaxCols, 1, True)
            End With

            '검체번호, 작업번호, 바코드번호
            Me.txtBcNo.Text = ""
            Me.txtWorkNo.Text = ""

            '검체명
            Me.lblSpcNm.Text = ""
            Me.lblSpcCd.Text = ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_BcNo_PatInfo(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_BcNo_PatInfo"

        Try
            rsBcNo = rsBcNo.Replace("-", "")

            Dim dt As DataTable

            '화면초기화
            sbDisplayInit_grpPatInfo()

            dt = LISAPP.APP_SP.fnGet_SpcInfo_bcno(rsBcNo)

            ' 접수된 검체가 있는지 확인
            If dt.Rows.Count = 0 Then
                MsgBox("접수된 검체가 없습니다!!")
                Return
            End If

            With Me.spdPatInfo
                Dim sPatInfo() As String = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

                .Row = 1
                .Col = .GetColFromID("orddt") : .Text = dt.Rows(0).Item("orddt").ToString
                .Col = .GetColFromID("regno") : .Text = dt.Rows(0).Item("regno").ToString
                .Col = .GetColFromID("patnm") : .Text = sPatInfo(0)
                .Col = .GetColFromID("sexage") : .Text = dt.Rows(0).Item("sexage").ToString
                .Col = .GetColFromID("idno") : .Text = sPatInfo(3)
                .Col = .GetColFromID("doctornm") : .Text = dt.Rows(0).Item("doctornm").ToString
                .Col = .GetColFromID("deptcd") : .Text = dt.Rows(0).Item("deptcd").ToString
                .Col = .GetColFromID("wardroom") : .Text = dt.Rows(0).Item("wardroom").ToString
            End With

            '검체번호, 작업번호, 바코드번호
            Me.txtBcNo.Text = dt.Rows(0).Item("bcno").ToString()
            Me.txtWorkNo.Text = dt.Rows(0).Item("wkno").ToString()

            '검체명, 진단명, 투여약물, 의뢰의사Remark
            Me.lblSpcCd.Text = dt.Rows(0).Item("spccd").ToString()
            Me.lblSpcNm.Text = dt.Rows(0).Item("spcnmd").ToString()
            DP_Common.setToolTip(Me.CreateGraphics, Me.lblSpcNm, Me.lblSpcNm.Text, m_tooltip)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbShwo_BcnoList(ByVal as_BcnoList As DataTable)   ' 보관 검체들 보여주기
        Try
            Dim strBcno As String = ""
            Dim strInsert_BCNO As String = ""

            Dim objBcnoTable As DataTable
            Dim PastBcno As String = ""     ' 경과시간
            Dim strPastDt As String = ""    ' 채혈시간
            Dim strAlarm As String = txtAlarm.Text.Trim      ' Alarm 표시일
            Dim NowTime As Date = m_Comm.GetDateTime

            Dim dbHour As Double
            If Not strAlarm.Equals("") Then  ' 알람 표시일이 있을경우
                dbHour = CType(txtAlarm.Text.Trim, Double) * 24
            Else
                dbHour = 0
            End If

            With spdManage
                For intRow As Integer = 0 To miMaxRow - 1
                    For iRow As Integer = 0 To as_BcnoList.Rows.Count - 1
                        If CType(as_BcnoList.Rows(iRow).Item("NUMROW"), Integer) = intRow + 1 Then
                            For intCol As Integer = 0 To miMaxCol - 1
                                For iCol As Integer = 0 To as_BcnoList.Columns.Count - 1
                                    If CType(as_BcnoList.Rows(iRow).Item("NUMCOL"), Integer) = intCol + 1 Then
                                        strBcno = as_BcnoList.Rows(iRow).Item("BCNO").ToString()
                                        strInsert_BCNO = strBcno.Substring(0, 8) + "-" + strBcno.Substring(8, 2) + "-" + strBcno.Substring(10, 4) + "-" + strBcno.Substring(14, 1)
                                        .Col = intCol + 1
                                        .Row = intRow + 1
                                        .Text = strInsert_BCNO

                                        '  Alarm 표시일보다 경과시간이 큰 경우 검체번호를 빨간색으로 보여주자.
                                        If dbHour <> 0 Then     ' Alarm 표시일이 있을경우

                                            objBcnoTable = fnGet_KsBcnoInfo(strBcno, m_KeepBcno)

                                            If objBcnoTable.Rows.Count > 0 Then
                                                strPastDt = objBcnoTable.Rows(0).Item("colldt").ToString()
                                            Else
                                                Exit For
                                            End If

                                            Dim PastTime As Double = 0

                                            If IsDate(strPastDt) Then
                                                PastTime = DateDiff(DateInterval.Hour, CType(strPastDt, Date), NowTime)
                                            Else
                                                MsgBox(.Col.ToString + "행, " + .Row.ToString + "열의 검체번호 : " + strBcno + "는 취소된 바코드입니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

                                                Exit For
                                            End If

                                            If dbHour <= PastTime Then
                                                .BlockMode = True
                                                .Col = intCol + 1 : .Col2 = intCol + 1 : .Row = intRow + 1 : .Row2 = intRow + 1
                                                .ForeColor = System.Drawing.Color.Red
                                                .BlockMode = False
                                            End If
                                        End If

                                        Exit For
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next
            End With
        Catch ex As Exception

        End Try
        

    End Sub

    Private Sub sbShowBCNO_Info(ByVal r_dt As DataTable)

        Dim NowTime As Date = m_Comm.GetDateTime

        With r_dt.Rows(0)
            Dim sBcno = .Item("bcno").ToString().Trim
            Dim sWorkNo = .Item("wkno").ToString().Trim
            Dim sCollDt = .Item("colldt").ToString().Trim
            Dim sRegNo = .Item("regno").ToString().Trim

            Me.txtBcNo.Text = Fn.BCNO_View(sBcno, True)

            If sWorkNo.Length = 14 Then
                Me.txtWorkNo.Text = Fn.WKNO_View(sWorkNo)
            End If

            txtComment.Text = .Item("OTHER").ToString()
            txtTime.Text = Fn.TimeElapsed(CType(sCollDt, Date), NowTime)
        End With

    End Sub

    Private Sub sbClearData_All()

        Me.lblSpcCds.Text = ""
        Me.txtMRow.Text = "" : Me.txtMCol.Text = "" : Me.txtAlarm.Text = "" : Me.txtComment.Text = ""
        With Me.spdManage
            .MaxRows = 10 : .MaxCols = 10
            .ClearRange(1, 1, .MaxCols, .MaxRows, False)
        End With
        Me.txtBcNo.Text = "" : Me.txtWorkNo.Text = ""

        With Me.spdPatInfo
            .Col = 1 : .Col2 = .MaxCols
            .Row = 1 : .Row2 = 1
            .BlockMode = True
            .Action = FPSpreadADO.ActionConstants.ActionClearText
            .BlockMode = False
        End With

        Me.lblSpcNm.Text = ""
        Me.cboToRow.SelectedIndex = -1 : Me.cboToCol.SelectedIndex = -1

    End Sub

    Private Class RowCol
        Public Col As Integer
        Public Row As Integer

        Public Sub New()

        End Sub
    End Class


#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbClearData_All()

    End Sub

    Public Sub New(ByVal rbMicroBio As Boolean)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbClearData_All()

        mbMicroBio = rbMicroBio

        If mbMicroBio Then
            msBCCLS = Application.StartupPath + msXMLDir & "\FGR11_BCCLSINFO_M.XML"
        End If

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
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtAlarm As System.Windows.Forms.TextBox
    Friend WithEvents txtMCol As System.Windows.Forms.TextBox
    Friend WithEvents txtMRow As System.Windows.Forms.TextBox
    Friend WithEvents cboRackID As System.Windows.Forms.ComboBox
    Friend WithEvents btnMove As System.Windows.Forms.Button
    Friend WithEvents cboToCol As System.Windows.Forms.ComboBox
    Friend WithEvents cboToRow As System.Windows.Forms.ComboBox
    Friend WithEvents cboToRack_ID As System.Windows.Forms.ComboBox
    Friend WithEvents spdManage As AxFPSpreadADO.AxfpSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR11))
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
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lblSpcCds = New System.Windows.Forms.Label
        Me.btnSpcView = New System.Windows.Forms.Button
        Me.cboBccls = New System.Windows.Forms.ComboBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtAlarm = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtMCol = New System.Windows.Forms.TextBox
        Me.txtMRow = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboRackID = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.spdManage = New AxFPSpreadADO.AxfpSpread
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnDiscard_All = New CButtonLib.CButton
        Me.btnDiscard = New CButtonLib.CButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnMove = New System.Windows.Forms.Button
        Me.cboToRack_ID = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cboToCol = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.cboToRow = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtTime = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBcNo = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtWorkNo = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lblSpcCd = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.lblSpcNm = New System.Windows.Forms.Label
        Me.spdPatInfo = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnModify = New System.Windows.Forms.Button
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnToggle = New System.Windows.Forms.Button
        Me.lblSearch = New System.Windows.Forms.Label
        Me.txtNo = New System.Windows.Forms.TextBox
        Me.btnNo = New System.Windows.Forms.Button
        Me.GroupBox5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.spdManage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblSpcCds)
        Me.GroupBox5.Controls.Add(Me.btnSpcView)
        Me.GroupBox5.Controls.Add(Me.cboBccls)
        Me.GroupBox5.Controls.Add(Me.Label16)
        Me.GroupBox5.Controls.Add(Me.txtAlarm)
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.txtMCol)
        Me.GroupBox5.Controls.Add(Me.txtMRow)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.cboRackID)
        Me.GroupBox5.Controls.Add(Me.Label14)
        Me.GroupBox5.Location = New System.Drawing.Point(4, -2)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(444, 80)
        Me.GroupBox5.TabIndex = 148
        Me.GroupBox5.TabStop = False
        '
        'lblSpcCds
        '
        Me.lblSpcCds.Location = New System.Drawing.Point(289, 17)
        Me.lblSpcCds.Name = "lblSpcCds"
        Me.lblSpcCds.Size = New System.Drawing.Size(148, 13)
        Me.lblSpcCds.TabIndex = 158
        '
        'btnSpcView
        '
        Me.btnSpcView.Location = New System.Drawing.Point(368, 33)
        Me.btnSpcView.Name = "btnSpcView"
        Me.btnSpcView.Size = New System.Drawing.Size(70, 43)
        Me.btnSpcView.TabIndex = 157
        Me.btnSpcView.Text = "저장가능 검체종류"
        '
        'cboBccls
        '
        Me.cboBccls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBccls.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBccls.Location = New System.Drawing.Point(69, 10)
        Me.cboBccls.Margin = New System.Windows.Forms.Padding(0)
        Me.cboBccls.Name = "cboBccls"
        Me.cboBccls.Size = New System.Drawing.Size(213, 20)
        Me.cboBccls.TabIndex = 145
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label16.Location = New System.Drawing.Point(5, 10)
        Me.Label16.Margin = New System.Windows.Forms.Padding(0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 20)
        Me.Label16.TabIndex = 144
        Me.Label16.Text = "검체분류"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAlarm
        '
        Me.txtAlarm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtAlarm.Location = New System.Drawing.Point(309, 55)
        Me.txtAlarm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAlarm.MaxLength = 3
        Me.txtAlarm.Name = "txtAlarm"
        Me.txtAlarm.ReadOnly = True
        Me.txtAlarm.Size = New System.Drawing.Size(28, 21)
        Me.txtAlarm.TabIndex = 143
        Me.txtAlarm.Text = "999"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(229, 55)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(80, 21)
        Me.Label13.TabIndex = 142
        Me.Label13.Text = "Alarm 표시일"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMCol
        '
        Me.txtMCol.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtMCol.Location = New System.Drawing.Point(180, 55)
        Me.txtMCol.Margin = New System.Windows.Forms.Padding(0)
        Me.txtMCol.Name = "txtMCol"
        Me.txtMCol.ReadOnly = True
        Me.txtMCol.Size = New System.Drawing.Size(24, 21)
        Me.txtMCol.TabIndex = 140
        Me.txtMCol.Text = "99"
        '
        'txtMRow
        '
        Me.txtMRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtMRow.Location = New System.Drawing.Point(69, 55)
        Me.txtMRow.Margin = New System.Windows.Forms.Padding(0)
        Me.txtMRow.Name = "txtMRow"
        Me.txtMRow.ReadOnly = True
        Me.txtMRow.Size = New System.Drawing.Size(24, 21)
        Me.txtMRow.TabIndex = 139
        Me.txtMRow.Text = "99"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.SlateGray
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(116, 55)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 21)
        Me.Label4.TabIndex = 138
        Me.Label4.Text = "Max Col"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SlateGray
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(5, 55)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 21)
        Me.Label1.TabIndex = 137
        Me.Label1.Text = "Max Row"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboRackID
        '
        Me.cboRackID.Location = New System.Drawing.Point(69, 33)
        Me.cboRackID.Margin = New System.Windows.Forms.Padding(0)
        Me.cboRackID.Name = "cboRackID"
        Me.cboRackID.Size = New System.Drawing.Size(107, 20)
        Me.cboRackID.TabIndex = 136
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.Location = New System.Drawing.Point(5, 33)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 20)
        Me.Label14.TabIndex = 121
        Me.Label14.Text = "Rack ID"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel6.Controls.Add(Me.spdManage)
        Me.Panel6.Location = New System.Drawing.Point(5, 185)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1262, 650)
        Me.Panel6.TabIndex = 151
        '
        'spdManage
        '
        Me.spdManage.DataSource = Nothing
        Me.spdManage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdManage.Location = New System.Drawing.Point(0, 0)
        Me.spdManage.Name = "spdManage"
        Me.spdManage.OcxState = CType(resources.GetObject("spdManage.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdManage.Size = New System.Drawing.Size(1258, 646)
        Me.spdManage.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.btnExit)
        Me.Panel5.Controls.Add(Me.btnClear)
        Me.Panel5.Controls.Add(Me.btnDiscard_All)
        Me.Panel5.Controls.Add(Me.btnDiscard)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 841)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1272, 34)
        Me.Panel5.TabIndex = 152
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems1
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
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker2
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1171, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(93, 25)
        Me.btnExit.TabIndex = 41
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems2
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
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker4
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1073, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(97, 25)
        Me.btnClear.TabIndex = 40
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnDiscard_All
        '
        Me.btnDiscard_All.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDiscard_All.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnDiscard_All.ColorFillBlend = CBlendItems3
        Me.btnDiscard_All.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnDiscard_All.Corners.All = CType(6, Short)
        Me.btnDiscard_All.Corners.LowerLeft = CType(6, Short)
        Me.btnDiscard_All.Corners.LowerRight = CType(6, Short)
        Me.btnDiscard_All.Corners.UpperLeft = CType(6, Short)
        Me.btnDiscard_All.Corners.UpperRight = CType(6, Short)
        Me.btnDiscard_All.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnDiscard_All.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnDiscard_All.FocalPoints.CenterPtX = 0.5104167!
        Me.btnDiscard_All.FocalPoints.CenterPtY = 0.48!
        Me.btnDiscard_All.FocalPoints.FocusPtX = 0.0!
        Me.btnDiscard_All.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDiscard_All.FocusPtTracker = DesignerRectTracker6
        Me.btnDiscard_All.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDiscard_All.ForeColor = System.Drawing.Color.White
        Me.btnDiscard_All.Image = Nothing
        Me.btnDiscard_All.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDiscard_All.ImageIndex = 0
        Me.btnDiscard_All.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnDiscard_All.Location = New System.Drawing.Point(879, 5)
        Me.btnDiscard_All.Name = "btnDiscard_All"
        Me.btnDiscard_All.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnDiscard_All.SideImage = Nothing
        Me.btnDiscard_All.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDiscard_All.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnDiscard_All.Size = New System.Drawing.Size(96, 25)
        Me.btnDiscard_All.TabIndex = 38
        Me.btnDiscard_All.Text = "일괄폐기"
        Me.btnDiscard_All.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDiscard_All.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnDiscard_All.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnDiscard
        '
        Me.btnDiscard.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDiscard.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnDiscard.ColorFillBlend = CBlendItems4
        Me.btnDiscard.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnDiscard.Corners.All = CType(6, Short)
        Me.btnDiscard.Corners.LowerLeft = CType(6, Short)
        Me.btnDiscard.Corners.LowerRight = CType(6, Short)
        Me.btnDiscard.Corners.UpperLeft = CType(6, Short)
        Me.btnDiscard.Corners.UpperRight = CType(6, Short)
        Me.btnDiscard.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnDiscard.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnDiscard.FocalPoints.CenterPtX = 0.5416667!
        Me.btnDiscard.FocalPoints.CenterPtY = 0.72!
        Me.btnDiscard.FocalPoints.FocusPtX = 0.0!
        Me.btnDiscard.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = True
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDiscard.FocusPtTracker = DesignerRectTracker8
        Me.btnDiscard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDiscard.ForeColor = System.Drawing.Color.White
        Me.btnDiscard.Image = Nothing
        Me.btnDiscard.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDiscard.ImageIndex = 0
        Me.btnDiscard.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnDiscard.Location = New System.Drawing.Point(976, 5)
        Me.btnDiscard.Name = "btnDiscard"
        Me.btnDiscard.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnDiscard.SideImage = Nothing
        Me.btnDiscard.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDiscard.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnDiscard.Size = New System.Drawing.Size(96, 25)
        Me.btnDiscard.TabIndex = 39
        Me.btnDiscard.Text = "폐기"
        Me.btnDiscard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDiscard.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnDiscard.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnMove)
        Me.GroupBox2.Controls.Add(Me.cboToRack_ID)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.cboToCol)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.cboToRow)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(709, 73)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(300, 84)
        Me.GroupBox2.TabIndex = 154
        Me.GroupBox2.TabStop = False
        '
        'btnMove
        '
        Me.btnMove.Location = New System.Drawing.Point(104, 9)
        Me.btnMove.Margin = New System.Windows.Forms.Padding(0)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(68, 23)
        Me.btnMove.TabIndex = 155
        Me.btnMove.Text = "이동"
        '
        'cboToRack_ID
        '
        Me.cboToRack_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToRack_ID.Location = New System.Drawing.Point(87, 36)
        Me.cboToRack_ID.Margin = New System.Windows.Forms.Padding(0)
        Me.cboToRack_ID.Name = "cboToRack_ID"
        Me.cboToRack_ID.Size = New System.Drawing.Size(116, 20)
        Me.cboToRack_ID.TabIndex = 150
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label9.Location = New System.Drawing.Point(3, 36)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 20)
        Me.Label9.TabIndex = 149
        Me.Label9.Text = "To Rack ID"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.IndianRed
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label8.Location = New System.Drawing.Point(3, 10)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 20)
        Me.Label8.TabIndex = 148
        Me.Label8.Text = "보관검체이동"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboToCol
        '
        Me.cboToCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToCol.Location = New System.Drawing.Point(88, 58)
        Me.cboToCol.Margin = New System.Windows.Forms.Padding(0)
        Me.cboToCol.Name = "cboToCol"
        Me.cboToCol.Size = New System.Drawing.Size(54, 20)
        Me.cboToCol.TabIndex = 154
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label11.Location = New System.Drawing.Point(3, 58)
        Me.Label11.Margin = New System.Windows.Forms.Padding(0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 20)
        Me.Label11.TabIndex = 153
        Me.Label11.Text = "가로"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboToRow
        '
        Me.cboToRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToRow.Location = New System.Drawing.Point(225, 58)
        Me.cboToRow.Margin = New System.Windows.Forms.Padding(0)
        Me.cboToRow.Name = "cboToRow"
        Me.cboToRow.Size = New System.Drawing.Size(56, 20)
        Me.cboToRow.TabIndex = 152
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label10.Location = New System.Drawing.Point(143, 58)
        Me.Label10.Margin = New System.Windows.Forms.Padding(0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 20)
        Me.Label10.TabIndex = 151
        Me.Label10.Text = "세로"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtTime)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txtBcNo)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.txtWorkNo)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Location = New System.Drawing.Point(454, 73)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(252, 83)
        Me.GroupBox3.TabIndex = 155
        Me.GroupBox3.TabStop = False
        '
        'txtTime
        '
        Me.txtTime.BackColor = System.Drawing.Color.White
        Me.txtTime.Location = New System.Drawing.Point(177, 9)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.Size = New System.Drawing.Size(72, 21)
        Me.txtTime.TabIndex = 164
        Me.txtTime.Text = "1234:59:59"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.SlateGray
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(111, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 21)
        Me.Label3.TabIndex = 163
        Me.Label3.Text = "경과시간"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBcNo
        '
        Me.txtBcNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtBcNo.Location = New System.Drawing.Point(69, 34)
        Me.txtBcNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.ReadOnly = True
        Me.txtBcNo.Size = New System.Drawing.Size(180, 21)
        Me.txtBcNo.TabIndex = 162
        Me.txtBcNo.Text = "20031016-BB-0001"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.SlateGray
        Me.Label18.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label18.Location = New System.Drawing.Point(3, 34)
        Me.Label18.Margin = New System.Windows.Forms.Padding(0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 22)
        Me.Label18.TabIndex = 161
        Me.Label18.Text = "검체번호"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.IndianRed
        Me.Label19.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label19.Location = New System.Drawing.Point(3, 10)
        Me.Label19.Margin = New System.Windows.Forms.Padding(0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(93, 20)
        Me.Label19.TabIndex = 160
        Me.Label19.Text = "보관검체정보"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtWorkNo
        '
        Me.txtWorkNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWorkNo.Location = New System.Drawing.Point(69, 58)
        Me.txtWorkNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtWorkNo.Name = "txtWorkNo"
        Me.txtWorkNo.ReadOnly = True
        Me.txtWorkNo.Size = New System.Drawing.Size(180, 21)
        Me.txtWorkNo.TabIndex = 159
        Me.txtWorkNo.Text = "20031016-BB-0001"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.SlateGray
        Me.Label20.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.White
        Me.Label20.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label20.Location = New System.Drawing.Point(3, 58)
        Me.Label20.Margin = New System.Windows.Forms.Padding(0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 21)
        Me.Label20.TabIndex = 158
        Me.Label20.Text = "작업번호"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblSpcCd)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.lblSpcNm)
        Me.GroupBox4.Controls.Add(Me.spdPatInfo)
        Me.GroupBox4.Location = New System.Drawing.Point(454, -2)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(556, 80)
        Me.GroupBox4.TabIndex = 156
        Me.GroupBox4.TabStop = False
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSpcCd.Location = New System.Drawing.Point(433, 9)
        Me.lblSpcCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(39, 20)
        Me.lblSpcCd.TabIndex = 158
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSpcCd.Visible = False
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(3, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(93, 20)
        Me.Label17.TabIndex = 152
        Me.Label17.Text = "환자/검체 정보"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.SlateGray
        Me.Label21.ForeColor = System.Drawing.Color.White
        Me.Label21.Location = New System.Drawing.Point(109, 10)
        Me.Label21.Margin = New System.Windows.Forms.Padding(0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(68, 20)
        Me.Label21.TabIndex = 150
        Me.Label21.Text = "검체명"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcNm
        '
        Me.lblSpcNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSpcNm.Location = New System.Drawing.Point(177, 10)
        Me.lblSpcNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(375, 20)
        Me.lblSpcNm.TabIndex = 151
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'spdPatInfo
        '
        Me.spdPatInfo.DataSource = Nothing
        Me.spdPatInfo.Location = New System.Drawing.Point(3, 33)
        Me.spdPatInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.spdPatInfo.Name = "spdPatInfo"
        Me.spdPatInfo.OcxState = CType(resources.GetObject("spdPatInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdPatInfo.Size = New System.Drawing.Size(549, 42)
        Me.spdPatInfo.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnModify)
        Me.GroupBox1.Controls.Add(Me.txtComment)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 73)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(444, 109)
        Me.GroupBox1.TabIndex = 157
        Me.GroupBox1.TabStop = False
        '
        'btnModify
        '
        Me.btnModify.Location = New System.Drawing.Point(108, 9)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(68, 23)
        Me.btnModify.TabIndex = 156
        Me.btnModify.Text = "저장"
        '
        'txtComment
        '
        Me.txtComment.BackColor = System.Drawing.Color.White
        Me.txtComment.Location = New System.Drawing.Point(3, 33)
        Me.txtComment.MaxLength = 200
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(434, 70)
        Me.txtComment.TabIndex = 139
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Lavender
        Me.Label12.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label12.Location = New System.Drawing.Point(6, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 21)
        Me.Label12.TabIndex = 137
        Me.Label12.Text = "보관 Comment"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(670, 160)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(36, 21)
        Me.btnToggle.TabIndex = 160
        Me.btnToggle.Text = "↔"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(455, 160)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(68, 21)
        Me.lblSearch.TabIndex = 159
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNo
        '
        Me.txtNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNo.Location = New System.Drawing.Point(523, 160)
        Me.txtNo.MaxLength = 18
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(129, 21)
        Me.txtNo.TabIndex = 158
        '
        'btnNo
        '
        Me.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNo.Location = New System.Drawing.Point(653, 160)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(16, 21)
        Me.btnNo.TabIndex = 161
        Me.btnNo.Text = "↙"
        '
        'FGR11
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1272, 875)
        Me.Controls.Add(Me.btnNo)
        Me.Controls.Add(Me.btnToggle)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.txtNo)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "FGR11"
        Me.Text = "보관 검체 관리"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        CType(Me.spdManage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cboRackID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRackID.SelectedIndexChanged

        Dim sBcclsCd As String = ""
        Dim sRackId As String = ""

        miIdx_RackId = cboRackID.SelectedIndex  ' 다른 곳으로 이동시키는 경우 강제로 이벤트 발생하여 다시 화면에 뿌려주기 위해!!!

        sbDisplay_KeepInfo()

    End Sub

    Private Sub spdManage_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdManage.KeyDownEvent
        If e.keyCode = Windows.Forms.Keys.Enter Then

            Dim sBcno As String = ""
            Dim sBcno_In As String = ""
            Dim m_CommDBFN As New LISAPP.APP_DB.DbFn
            Dim dt As DataTable

            Dim stuKsInfo As New STU_KsRack

            With spdManage
                .Row = .ActiveRow
                .Col = .ActiveCol : sBcno = .Text

                stuKsInfo.Bcclscd = Ctrl.Get_Code(Me.cboBccls.Text)
                stuKsInfo.RackId = Me.cboRackID.SelectedItem.ToString
                stuKsInfo.Bcclscd = Ctrl.Get_Code(Me.cboBccls.Text)
                stuKsInfo.SpcCd = ""
                stuKsInfo.NumRow = .ActiveRow.ToString
                stuKsInfo.NumCol = .ActiveCol.ToString

                ' 동일한 자리에 이미 검체가 존재하는지 여부를 판별
                dt = fnGet_Bcno_YesNo("A", stuKsInfo)

                If dt.Rows.Count > 0 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관 검체가 존재합니다. 다른곳을 선택하세요")
                    Me.cboRackID_SelectedIndexChanged(miIdx_RackId, Nothing)
                    Return
                End If
            End With

            With spdManage
                If Not sBcno.Equals("") Then
                    If sBcno.Length.Equals(11) Then     ' 바코드로 찍은 경우 -> 적합한 검체번호 형식으로 바꿔줘야 함!
                        sBcno = m_CommDBFN.GetBCPrtToView(sBcno)

                    ElseIf sBcno.Length.Equals(15) Then   ' 앞의 년도 2자리를 빼고 보여줘야 함!
                    Else
                        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "잘못된 검체번호입니다. 다시 확인하세요")
                        .ClearRange(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False)
                        Return
                    End If

                    .Col = .ActiveCol : m_KeepBcno.NumCol = CType(.ActiveCol, String)
                    .Row = .ActiveRow : m_KeepBcno.NumRow = CType(.ActiveRow, String)
                    .Text = sBcno.ToUpper()

                    sbDisplay_BcNo_PatInfo(sBcno.ToUpper)
                    m_KeepBcno.SpcCd = Me.lblSpcCd.Text

                    If Me.lblSpcCds.Text.IndexOf("0".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "|") >= 0 Then
                    Else
                        If Me.lblSpcCds.Text.IndexOf(m_KeepBcno.SpcCd + "|") < 0 Then
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "Rack ID[" + cboRackID.Text + "]에 넣을 수 없는 검체번호 입니다." + vbCrLf + _
                                                                  "저장가능 검체를 확인해 보세요.")
                            .ClearRange(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False)
                            Return
                        End If
                    End If
                    Insert_KeepBcno(m_KeepBcno, sBcno.ToUpper, txtComment.Text.Trim)  ' LK010M에 보관검체정보 Insert
                End If
            End With
        End If

    End Sub

    Private Sub spdManage_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdManage.ClickEvent
        'Debug.WriteLine("clickEvent")

        Me.txtBcNo.Text = "" : Me.txtWorkNo.Text = "" : Me.txtTime.Text = ""
        Me.lblSpcNm.Text = ""

        For ix As Integer = 1 To spdPatInfo.MaxCols
            With spdPatInfo
                .Row = 1
                .Col = ix : .Text = ""
            End With
        Next

        Dim sBCNO As String = ""

        With spdManage
            .Col = e.col : .Col2 = e.col : .Row = e.row : .Row2 = e.row
            If .BackColor.Equals(System.Drawing.Color.White) Then
                Me.txtComment.Text = ""
            End If
        End With

        With spdManage
            .Row = e.row : .Col = e.col
            sBCNO = .Text

            ' 화면을 새로 여는경우 초기값 0을 가지고 있음 (바로전의 클릭된것을 기억하고 있다가 다른cell이 클릭되면 white로 색상변경)
            If miClick_Col <> 0 And miClick_Row <> 0 Then
                .BlockMode = True
                .Col = miClick_Col : .Col2 = miClick_Col : .Row = miClick_Row : .Row2 = miClick_Row
                .BackColor = System.Drawing.Color.White
                .BlockMode = False
            End If

            .BlockMode = True
            .Col = e.col : .Col2 = e.col : .Row = e.row : .Row2 = e.row
            .BackColor = System.Drawing.Color.Pink
            .BlockMode = False

            miClick_Col = e.col
            miClick_Row = e.row

        End With

        If sBCNO <> "" Then   ' 검체번호가 존재할때 -> LJ010M 에서 보관검체정보를 갖고와서 보여준다.
            msRealBcno = sBCNO.Replace("-", "")
            sbDisplay_BcNo_PatInfo(msRealBcno)
            m_KeepBcno.SpcCd = Me.lblSpcCd.Text

            Dim dt As DataTable = fnGet_KsBcnoInfo(msRealBcno, m_KeepBcno)
            If dt.Rows.Count > 0 Then
                sbShowBCNO_Info(dt)
            End If

            If Me.txtComment.Text = "" And PRG_CONST.BCCLS_MicorBio.Contains(m_KeepBcno.Bcclscd) Then
                Me.txtComment.Text = fnGet_KsBcno_cmt(msRealBcno)
            End If
        Else    ' 비어있는 Cell인 경우 
            Exit Sub
        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbClearData_All()
    End Sub

    Private Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If fnExe_KeepBcnoComment(Me.txtComment.Text.Trim, msRealBcno, m_KeepBcno) = True Then
            MsgBox("정상적으로 저장되었습니다", MsgBoxStyle.Information, Me.Text)
        Else
            Exit Sub
        End If
    End Sub

    Private Sub cboToRack_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboToRack_ID.SelectedIndexChanged

        msToRackID = cboToRack_ID.SelectedItem.ToString()

        Dim sMaxRow As String = ""    ' 이동할 rack의 MaxRow
        Dim sMaxCol As String = ""    ' 이동할 rack의 MaxCol
        Dim sBcclsCd As String = ""

        sBcclsCd = Ctrl.Get_Code(Me.cboBccls)

        Dim dt As DataTable = fnGet_KsRackInfo(msToRackID, sBcclsCd)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                m_ToKeepBcno.Bcclscd = .Item("bcclscd").ToString()
                m_ToKeepBcno.RackId = .Item("rackid").ToString()
                m_ToKeepBcno.SpcCd = .Item("spccd").ToString()
                m_ToKeepBcno.RegDt = .Item("regdt").ToString()
                m_ToKeepBcno.RegId = .Item("regid").ToString()

                sMaxRow = .Item("maxrow").ToString()
                sMaxCol = .Item("maxcol").ToString()
            End With
        Else
            MsgBox("데이터가 없습니다. 다시 선택해주세요", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        cboToRow.Items.Clear()      ' combobox 안의 내용을 clear시킴
        cboToCol.Items.Clear()

        For iRow As Integer = 1 To CType(sMaxRow, Integer)     ' ex) maxrow가 10 이면 1~10 까지 add시킨다
            cboToRow.Items.Add(iRow)
        Next
        For iCol As Integer = 1 To CType(sMaxCol, Integer)
            cboToCol.Items.Add(iCol)
        Next

    End Sub

    Private Sub btnMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim sToRow As String = ""     ' 이동할 rack의 Row
        Dim sToCol As String = ""

        If cboToRow.SelectedItem.ToString().Trim.Equals("") Or cboToCol.SelectedItem.ToString().Trim.Equals("") Then
            MsgBox("이동할 위치를 올바르게 선택해 주세요", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        sToRow = Me.cboToRow.SelectedItem.ToString() : m_ToKeepBcno.NumRow = sToRow
        sToCol = Me.cboToCol.SelectedItem.ToString() : m_ToKeepBcno.NumCol = sToCol
        m_ToKeepBcno.RackId = Me.cboToRack_ID.Text

        sbBcno_Move(sToRow, sToCol)

    End Sub


    Private Sub btnDiscard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiscard.Click
        Dim strBCNO As String = ""

        With spdManage
            .Row = .ActiveRow : .Col = .ActiveCol
            strBCNO = .Text
        End With

        If Not strBCNO.Equals("") Then
            If MsgBox("선택한 검체를 폐기하시겠습니까?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                If Discard_Bcno(m_KeepBcno, msRealBcno) = True Then
                    cboRackID_SelectedIndexChanged(miIdx_RackId, Nothing)
                    With spdManage
                        .ClearRange(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False)

                        .BlockMode = True
                        .Col = .ActiveCol : .Col2 = .ActiveCol : .Row = .ActiveRow : .Row2 = .ActiveRow
                        .BackColor = System.Drawing.Color.White
                        .BlockMode = False
                    End With

                    MsgBox("정상적으로 폐기되었습니다", MsgBoxStyle.Information, Me.Text)
                    sbDisplay_KeepInfo()
                Else
                    MsgBox("폐기되지 못했습니다. 다시 시도하세요", MsgBoxStyle.Information, Me.Text)
                    Exit Sub

                End If
            Else
                Exit Sub
            End If
        Else
            MsgBox("폐기할 검체를 선택해 주세요", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

    End Sub

    Private Sub btnDiscard_All_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiscard_All.Click
        Dim arrBcnoList As New ArrayList
        Dim objBcnoTable As DataTable

        If MsgBox("일괄폐기 하시겠습니까?", MsgBoxStyle.OkCancel, Me.Text) = MsgBoxResult.Ok Then

            objBcnoTable = fnGet_KsBcnoINfo(m_KeepBcno)

            If objBcnoTable.Rows.Count > 0 Then
                For intCnt As Integer = 0 To objBcnoTable.Rows.Count - 1
                    arrBcnoList.Add(objBcnoTable.Rows(intCnt).Item("BCNO").ToString)
                Next
            End If

            If DiscardAll_Bcno(m_KeepBcno, arrBcnoList) = True Then
                MsgBox("정상적으로 폐기되었습니다", MsgBoxStyle.Information, Me.Text)

                sbDisplay_KeepInfo()

            Else
                MsgBox("폐기되지 못했습니다. 다시 시도하세요", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        End If
    End Sub

    Private Sub FGR25_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGM11_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        sbDisplay_Bccls()

    End Sub

    Private Sub cboBccls_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBccls.SelectedIndexChanged

        sbClearData_All()
        If Me.cboBccls.SelectedIndex < 0 Then Exit Sub

        sbGet_RackID(Ctrl.Get_Code(Me.cboBccls))

        COMMON.CommXML.setOneElementXML(msXMLDir, msBCCLS, "BCCLS", Me.cboBccls.SelectedIndex.ToString)

    End Sub

    Private Sub btnSpcView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSpcView.Click

        Dim dt As DataTable = fnGet_Use_Spcinfo(Ctrl.Get_Code(Me.cboBccls), Ctrl.Get_Code(Me.cboRackID))
        Dim objHelp As New CDHELP.FGCDHELP01

        objHelp.FormText = btnSpcView.Text
        objHelp.MaxRows = 15

        objHelp.AddField("spccd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
        objHelp.AddField("spcnmd", "검체명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

        Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
        Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnSpcView)

        objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - btnSpcView.Left, pntFrmXY.Y + pntCtlXY.Y + btnSpcView.Height + 80, dt)

    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click

        Dim CommFn As New COMMON.CommFN.Fn
        Fn.SearchToggle(Me.lblSearch, Me.btnToggle, enumToggle.BcnoToRegno, Me.txtNo)
        Me.txtNo.Focus()

    End Sub

    Private Sub txtNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.Click
        Me.txtNo.SelectAll()
    End Sub

    Private Sub txtNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.GotFocus
        Me.txtNo.SelectAll()
    End Sub

    Private Sub txtNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNo.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            btnNo_Click(Me.btnNo, Nothing)
        End If
    End Sub

    Private Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click

        Dim sRegNo As String = ""
        Dim sBcNo As String = ""

        If Me.lblSearch.Text = "검체번호" Then
            sBcNo = Me.txtNo.Text

            If sBcNo.Length = 14 Then sBcNo = Me.txtNo.Text + "0"
            If sBcNo.Length = 12 Then sBcNo = sBcNo.Substring(0, 11)

            If sBcNo.Length = 11 Or sBcNo.Length = 10 Then
                sBcNo = LISAPP.COMM.BcnoFn.fnFind_BcNo(sBcNo)
            End If
        Else
            sRegNo = Me.txtNo.Text
        End If

        Dim dt As DataTable = fnGet_KsBcno_Regno(Ctrl.Get_Code(Me.cboBccls), sRegNo, sBcNo)

        Dim objHelp As New CDHELP.FGCDHELP01
        Dim alList As New ArrayList

        objHelp.FormText = "보관검체 위치"

        objHelp.MaxRows = 15
        objHelp.OnRowReturnYN = False

        objHelp.AddField("bcclsnmd", "검체분류", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
        objHelp.AddField("rackid", "Rack Id", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
        objHelp.AddField("numrow", "Row", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
        objHelp.AddField("numcol", "Col", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
        objHelp.AddField("bcno", "검체번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
        objHelp.AddField("regno", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
        objHelp.AddField("patnm", "성명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
        objHelp.AddField("sexage", "Sex/Age", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
        objHelp.AddField("deptnm", "진료과", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
        objHelp.AddField("wardroom", "병동/병실", 16, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

        Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
        Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnSpcView)

        alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - btnSpcView.Left, pntFrmXY.Y + pntCtlXY.Y + btnSpcView.Height + 80, dt)
        If alList.Count > 0 Then
            Dim sBcclsCd As String = alList.Item(0).ToString.Split("|"c)(0)
            Dim sRackId As String = alList.Item(0).ToString.Split("|"c)(1)
            Dim sNumRow As String = alList.Item(0).ToString.Split("|"c)(2)
            Dim sNumCol As String = alList.Item(0).ToString.Split("|"c)(3)

            For ix As Integer = 0 To Me.cboBccls.Items.Count - 1
                If Me.cboBccls.Items.Item(ix).ToString.EndsWith(sBcclsCd) = True Then
                    Me.cboBccls.SelectedIndex = ix
                    Exit For
                End If
            Next

            For ix As Integer = 0 To Me.cboRackID.Items.Count - 1
                If Me.cboRackID.Items.Item(ix).ToString.EndsWith(sRackId) = True Then
                    Me.cboRackID.SelectedIndex = ix
                    Exit For
                End If
            Next

            sbDisplay_KeepInfo()

            spdManage_ClickEvent(spdManage, New AxFPSpreadADO._DSpreadEvents_ClickEvent(Convert.ToInt16(sNumCol), Convert.ToInt16(sNumRow)))

        End If


        Me.txtNo.Text = ""

    End Sub

    Private Sub spdManage_MouseDownEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseDownEvent) Handles spdManage.MouseDownEvent
        'Debug.WriteLine("DN :" & e.x.ToString & ", " & e.y.ToString)

        Dim CR As New RowCol
        CR = fnGetColRow(e.x, e.y)
        'Debug.WriteLine("DN :" & CR.Col.ToString & ", " & CR.Row.ToString)

        mi_DownRow = CR.Row
        mi_DownCol = CR.Col

        ' **** add *********************************************************
        With spdManage
            .Col = mi_DownRow
            .Row = mi_DownCol
            msRealBcno = .Text.Trim
        End With

        If msRealBcno.Equals("") Then
            msRealBcno = ""
        End If
        ' ******************************************************************

    End Sub

    Private Sub spdManage_MouseUpEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseUpEvent) Handles spdManage.MouseUpEvent
        ' Debug.WriteLine("UP" & e.x.ToString & ", " & e.y.ToString)

        Dim sBcclsCd As String = ""
        If cboBccls.SelectedIndex < 0 Then Exit Sub

        sBcclsCd = Ctrl.Get_Code(cboBccls)

        Dim CR As New RowCol
        CR = fnGetColRow(e.x, e.y)
        'Debug.WriteLine("UP :" & CR.Col.ToString & ", " & CR.Row.ToString)

        If mi_DownRow = CR.Row And mi_DownCol = CR.Col Then       ' 그 자리 클릭 -> MouseUpEvent 타지 않게 한다.
            Exit Sub
        End If

        Dim dt As DataTable = fnGet_KsRackInfo(Me.cboRackID.Text, sBcclsCd)

        If dt.Rows.Count > 0 Then
            With dt.Rows(0)
                m_ToKeepBcno.Bcclscd = .Item("bcclscd").ToString()
                m_ToKeepBcno.RackId = .Item("rackid").ToString()
                m_ToKeepBcno.SpcCd = .Item("spccd").ToString()
                m_ToKeepBcno.RegDt = .Item("regdt").ToString()
                m_ToKeepBcno.RegId = .Item("regid").ToString()

                m_ToKeepBcno.NumRow = CR.Row.ToString
                m_ToKeepBcno.NumCol = CR.Col.ToString
            End With
        End If

        sbBcno_Move(CR.Row.ToString, CR.Col.ToString)
    End Sub

    Private Sub sbBcno_Move(ByVal rsRow As String, ByVal rsCol As String)

        ' 새로운 위치에 이미 샘플이 있는지 체크한뒤 없으면 이동하기!!
        Dim dt As DataTable = fnGet_Bcno_YesNo(rsRow, rsCol, , m_ToKeepBcno)

        If dt.Rows.Count > 0 Then  ' 검체 있는 경우
            fn_PopMsg(Me, "I"c, "보관 검체가 존재합니다. 다른곳을 선택하세요")
            Exit Sub
        Else    ' 선택한 곳으로 검체 insert -> 기존 위치의 정보 delete 
            If msRealBcno <> "" Then
                If InsertBcno_NewPlace(m_KeepBcno, msRealBcno, m_ToKeepBcno, msComment) = True Then
                    cboRackID_SelectedIndexChanged(miIdx_RackId, Nothing)       ' Refresh 기능

                    fn_PopMsg(Me, "I"c, "정상적으로 이동되었습니다")
                Else
                    fn_PopMsg(Me, "I"c, "이동에 실패했습니다. 다시 시도하세요")
                    Exit Sub
                End If
            Else
                fn_PopMsg(Me, "I"c, "이동할 검체를 선택해 주세요")
                Exit Sub
            End If
        End If

    End Sub
End Class
