'>>> [17] 항균제
Imports System.Windows.Forms
Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports common.commlogin.login
Imports COMMON.CommConst

Public Class FDF17
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF17.vb, Class : FDF17" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_ANTI
    Friend WithEvents btnGetExcel As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents lblSameCd As System.Windows.Forms.Label
    Friend WithEvents txtSameCd As System.Windows.Forms.TextBox

    Private Sub sbEditUseDt_Edit(ByVal rsUseTag As String, ByVal rsUseDt As String)
        Dim sFn As String = "Sub sbEditUseDt_Edit"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            rsUseDt = rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", "")

            '> 사용중복 조사
            dt = mobjDAF.GetUsUeDupl_Anti(Me.txtAntiCd.Text, Me.txtUSDT.Text, rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransTestInfo_UPD_US(Me.txtAntiCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransTestInfo_UPD_UE(Me.txtAntiCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseDt)
            End If

            If bReturn Then
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + "가 수정되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox(IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString + " 수정에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            dt = mobjDAF.GetUsUeCd_Anti(Me.txtAntiCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransTestInfo_DEL(Me.txtAntiCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If bReturn Then
                MsgBox("해당 코드가 삭제되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox("해당 코드 삭제에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub sbEditUseDt(ByVal rsUseTag As String)
        Dim sFn As String = "Public Sub sbEditUseDt"

        Try
            Dim fgf02 As New FGF03

            With fgf02
                .txtCd.Text = Me.txtAntiCd.Text
                .txtNm.Text = Me.txtAntiNm.Text

                .lblUseDt.Text = IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString
                .lblUseDtA.Text = IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString
                .btnEditUseDt.Text = .btnEditUseDt.Text.Replace("사용일시", IIf(rsUseTag.ToUpper = "USDT", "시작일시", "종료일시").ToString)
                .txtUseDt.Text = IIf(rsUseTag.ToUpper = "USDT", Me.txtUSDT.Text, Me.txtUEDT.Text).ToString

                .Owner = Me
                .StartPosition = Windows.Forms.FormStartPosition.CenterParent
                .ShowDialog()
            End With

            If IsDate(Me.AccessibleName) Then
                If CDate(Me.AccessibleName) = Date.MinValue Then
                    'Delete
                    sbEditUseDt_Del()
                Else
                    'Edit
                    sbEditUseDt_Edit(rsUseTag, Me.AccessibleName)
                End If

            Else
                Return

            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.AccessibleName = ""

        End Try
    End Sub

    Private Function fnCollectItemTable_230(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_230(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it230 As New LISAPP.ItemTableCollection

            With it230
                .SetItemTable("ANTICD", 1, 1, txtAntiCd.Text, OracleDbType.Varchar2)
                .SetItemTable("USDT", 2, 1, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, OracleDbType.Varchar2)

                If txtUEDT.Text = "" Then
                    .SetItemTable("UEDT", 3, 1, msUEDT, OracleDbType.Varchar2)
                Else
                    .SetItemTable("UEDT", 3, 1, txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), OracleDbType.Varchar2)
                End If

                .SetItemTable("REGDT", 4, 1, rsRegDT, OracleDbType.Varchar2)
                .SetItemTable("REGID", 5, 1, USER_INFO.USRID, OracleDbType.Varchar2)
                .SetItemTable("REGIP", 6, 1, USER_INFO.LOCALIP, OracleDbType.Varchar2)
                .SetItemTable("ANTINM", 7, 1, txtAntiNm.Text, OracleDbType.Varchar2)
                .SetItemTable("ANTINMS", 8, 1, txtAntiNmS.Text, OracleDbType.Varchar2)
                .SetItemTable("ANTINMD", 9, 1, txtAntiNmD.Text, OracleDbType.Varchar2)
                .SetItemTable("ANTINMP", 10, 1, txtAntiNmP.Text, OracleDbType.Varchar2)
                .SetItemTable("ANTIIFCD", 11, 1, txtIFCd.Text, OracleDbType.Varchar2)
                .SetItemTable("ANTIWNCD", 12, 1, txtWNCd.Text, OracleDbType.Varchar2)
                .SetItemTable("DISPSEQ", 13, 1, IIf(txtDispSeq.Text = "", "999", Me.txtDispSeq.Text).ToString, OracleDbType.Int32)
                .SetItemTable("SAMECD", 14, 1, txtSameCd.Text, OracleDbType.Varchar2)
            End With

            fnCollectItemTable_230 = it230
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

            fnCollectItemTable_230 = New LISAPP.ItemTableCollection
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it230 As New LISAPP.ItemTableCollection
            Dim iRegType230 As Integer = 0
            Dim sRegDT As String

            iRegType230 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it230 = fnCollectItemTable_230(sRegDT)

            If mobjDAF.TransAntiInfo(it230, iRegType230, txtAntiCd.Text, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsAntiCd As String, ByVal rsUsDt As String) As String
        Dim sFn As String = ""

        Try
            Dim dt As DataTable = mobjDAF.GetRecentAntiInfo(rsAntiCd, rsUsDt)

            If dt.Rows.Count > 0 Then
                Return "시작일시가 " + dt.Rows(0).Item(0).ToString + "인 동일 " + Me.lblAntiCd.Text + "가 존재합니다." + vbCrLf + vbCrLf + _
                       "시작일시를 재조정 하십시요!!"
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

        fnValidate = False

        Try
            If Len(txtAntiCd.Text.Trim) < 1 Then
                MsgBox("항균제코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(txtAntiNm.Text.Trim) < 1 Then
                MsgBox("항균제명을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(txtAntiNmS.Text.Trim) < 1 Then
                MsgBox("항균제명(약어)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(txtAntiNmD.Text.Trim) < 1 Then
                MsgBox("항균제명(화면)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Len(txtAntiNmP.Text.Trim) < 1 Then
                MsgBox("항균제명(출력)을 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtDispSeq.Text <> "" Then
                If IsNumeric(txtDispSeq.Text) = False Then
                    MsgBox("정렬순서를 숫자로 입력하여 주십시요!!", MsgBoxStyle.Critical)
                    Exit Function
                End If
            End If

            If Not IsDate(txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(txtAntiCd.Text, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HH:mm:ss").Replace(":", ""))

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Exit Function
            End If

            fnValidate = True

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsAntiCd As String, ByVal rsUsDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_Anti(rsAntiCd, rsUsDt)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Anti(ByVal rsAntiCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Bac(ByVal asBacCd As String, ByVal asUSDT As String)"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mobjDAF.GetAntiInfo(rsAntiCd, rsUsDt)

            '입력용 컨트롤이 모두 업데이트되므로 초기화할 필요는 없다.
            '''    sbInitialize()

            ''초기화할 것은 Query라벨
            'sbInitialize_Test_QueryLabel()

            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()

            sbInitialize_CtrlCollection()

            Ctrl.FindChildControl(Me.Controls, mchildctrlcol)

            If dt.Rows.Count < 1 Then Return

            For i As Integer = 0 To dt.Rows.Count - 1
                For Each cctrl In mchildctrlcol
                    For j As Integer = 0 To dt.Columns.Count - 1
                        If cctrl.Tag.ToString.ToUpper = dt.Columns(j).ColumnName().ToUpper Then
                            mchildctrlcol.Remove(1)

                            If TypeOf (cctrl) Is System.Windows.Forms.ComboBox Then
                                If cctrl.Tag.ToString.EndsWith("_01") = True Then
                                    iCurIndex = -1

                                    For k As Integer = 0 To CType(cctrl, System.Windows.Forms.ComboBox).Items.Count - 1
                                        If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.EndsWith(dt.Rows(i).Item(j).ToString) = True Then
                                            iCurIndex = k

                                            Exit For
                                        End If

                                        If CType(cctrl, Windows.Forms.ComboBox).Items.Item(k).ToString.StartsWith(dt.Rows(i).Item(j).ToString) = True Then
                                            iCurIndex = k

                                            Exit For
                                        End If
                                    Next

                                    CType(cctrl, Windows.Forms.ComboBox).SelectedIndex = iCurIndex
                                End If

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.TextBox Then
                                cctrl.Text = dt.Rows(i).Item(j).ToString.Trim

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.CheckBox Then
                                CType(cctrl, System.Windows.Forms.CheckBox).Checked = CType(IIf(dt.Rows(i).Item(j).ToString = "1", True, False), Boolean)

                            ElseIf TypeOf (cctrl) Is System.Windows.Forms.RadioButton Then
                                CType(cctrl, System.Windows.Forms.RadioButton).Checked = CType(IIf(dt.Rows(i).Item(j).ToString = "1", True, False), Boolean)

                            End If

                            Exit For
                        End If
                    Next
                Next
            Next

            If Not IsNothing(Me.Owner) Then
                If Not CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Me.txtUSDay.Text = rsUsDt.Insert(4, "-").Insert(7, "-").Substring(0, 10)
                    Me.dtpUSTime.Value = CDate(rsUsDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":"))
                End If
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRID = "ACK" Then btnGetExcel.Visible = True

            If USER_INFO.USRLVL = "S" Then      '권한이 있어야 "사용종료"를 할 수 있음
                btnUE.Enabled = True
            Else
                btnUE.Enabled = False
            End If

            miSelectKey = 1

            sbInitialize_ErrProvider()

            sbInitialize_Control()
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn As String = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_Control(Optional ByVal iMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If iMode = 0 Then
                txtAntiCd.Text = "" : btnUE.Visible = False
                txtAntiNm.Text = "" : txtAntiNmS.Text = "" : txtAntiNmD.Text = "" : txtAntiNmP.Text = ""
                txtIFCd.Text = "" : txtWNCd.Text = "" : txtDispSeq.Text = "" : txtSameCd.Text = ""
                txtUSDT.Text = "" : txtUEDT.Text = "" : txtRegDT.Text = "" : txtRegID.Text = "" : txtRegNm.Text = ""
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_CtrlCollection()
        mchildctrlcol = Nothing

        mchildctrlcol = New Collection
    End Sub

    Public Sub sbSetNewUSDT()
        Dim sFn As String = ""

        Try
            Dim sDate As String = fnGetSystemDT()
            sDate = sDate.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")

#If DEBUG Then
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 0, CType(sDate, Date)), "yyyy-MM-dd 00:00:00")
#Else
            Dim sSysDT As String = Format(DateAdd(DateInterval.Day, 1, CType(sDate, Date)), "yyyy-MM-dd 00:00:00")
#End If
            miSelectKey = 1

            txtUSDay.Text = sSysDT.Substring(0, 10)
            dtpUSDay.Value = CType(sSysDT, Date)
            dtpUSTime.Value = CType(sSysDT, Date)
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
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
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblAntiNmS As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblAntiNmP As System.Windows.Forms.Label
    Friend WithEvents lblAntiNmD As System.Windows.Forms.Label
    Friend WithEvents lblAntiNm As System.Windows.Forms.Label
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents txtAntiNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtAntiNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtAntiNm As System.Windows.Forms.TextBox
    Friend WithEvents txtAntiNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtAntiCd As System.Windows.Forms.TextBox
    Friend WithEvents txtWNCd As System.Windows.Forms.TextBox
    Friend WithEvents lblWNCd As System.Windows.Forms.Label
    Friend WithEvents txtIFCd As System.Windows.Forms.TextBox
    Friend WithEvents lblIFCd As System.Windows.Forms.Label
    Friend WithEvents lblDispSeq As System.Windows.Forms.Label
    Friend WithEvents txtDispSeq As System.Windows.Forms.TextBox
    Friend WithEvents lblAntiCd As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF17))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtUEDT = New System.Windows.Forms.TextBox
        Me.lblUEDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.txtUSDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.lblUSDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.lblSameCd = New System.Windows.Forms.Label
        Me.txtSameCd = New System.Windows.Forms.TextBox
        Me.txtDispSeq = New System.Windows.Forms.TextBox
        Me.lblDispSeq = New System.Windows.Forms.Label
        Me.lblAntiNmS = New System.Windows.Forms.Label
        Me.txtAntiNmS = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblAntiNmP = New System.Windows.Forms.Label
        Me.txtAntiNmP = New System.Windows.Forms.TextBox
        Me.lblAntiNmD = New System.Windows.Forms.Label
        Me.txtAntiNmD = New System.Windows.Forms.TextBox
        Me.lblAntiNm = New System.Windows.Forms.Label
        Me.txtAntiNm = New System.Windows.Forms.TextBox
        Me.txtWNCd = New System.Windows.Forms.TextBox
        Me.lblWNCd = New System.Windows.Forms.Label
        Me.txtIFCd = New System.Windows.Forms.TextBox
        Me.lblIFCd = New System.Windows.Forms.Label
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnGetExcel = New System.Windows.Forms.Button
        Me.txtAntiCd = New System.Windows.Forms.TextBox
        Me.lblAntiCd = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker
        Me.txtUSDay = New System.Windows.Forms.TextBox
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker
        Me.lblUSDayTime = New System.Windows.Forms.Label
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.SuspendLayout()
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'pnlTop
        '
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.Controls.Add(Me.tclSpc)
        resources.ApplyResources(Me.pnlTop, "pnlTop")
        Me.pnlTop.Name = "pnlTop"
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tbcTpg)
        resources.ApplyResources(Me.tclSpc, "tclSpc")
        Me.tclSpc.Name = "tclSpc"
        Me.tclSpc.SelectedIndex = 0
        Me.tclSpc.TabStop = False
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.txtUEDT)
        Me.tbcTpg.Controls.Add(Me.lblUEDT)
        Me.tbcTpg.Controls.Add(Me.txtRegDT)
        Me.tbcTpg.Controls.Add(Me.txtUSDT)
        Me.tbcTpg.Controls.Add(Me.lblUserNm)
        Me.tbcTpg.Controls.Add(Me.lblRegDT)
        Me.tbcTpg.Controls.Add(Me.lblUSDT)
        Me.tbcTpg.Controls.Add(Me.txtRegID)
        Me.tbcTpg.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg.Controls.Add(Me.grpCd)
        resources.ApplyResources(Me.tbcTpg, "tbcTpg")
        Me.tbcTpg.Name = "tbcTpg"
        '
        'txtRegNm
        '
        resources.ApplyResources(Me.txtRegNm, "txtRegNm")
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtUEDT
        '
        resources.ApplyResources(Me.txtUEDT, "txtUEDT")
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Name = "txtUEDT"
        Me.txtUEDT.ReadOnly = True
        Me.txtUEDT.TabStop = False
        Me.txtUEDT.Tag = "UEDT"
        '
        'lblUEDT
        '
        resources.ApplyResources(Me.lblUEDT, "lblUEDT")
        Me.lblUEDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Tag = ""
        '
        'txtRegDT
        '
        resources.ApplyResources(Me.txtRegDT, "txtRegDT")
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'txtUSDT
        '
        resources.ApplyResources(Me.txtUSDT, "txtUSDT")
        Me.txtUSDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.TabStop = False
        Me.txtUSDT.Tag = "USDT"
        '
        'lblUserNm
        '
        resources.ApplyResources(Me.lblUserNm, "lblUserNm")
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Name = "lblUserNm"
        '
        'lblRegDT
        '
        resources.ApplyResources(Me.lblRegDT, "lblRegDT")
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Name = "lblRegDT"
        '
        'lblUSDT
        '
        resources.ApplyResources(Me.lblUSDT, "lblUSDT")
        Me.lblUSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUSDT.ForeColor = System.Drawing.Color.Black
        Me.lblUSDT.Name = "lblUSDT"
        '
        'txtRegID
        '
        resources.ApplyResources(Me.txtRegID, "txtRegID")
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCdInfo1
        '
        resources.ApplyResources(Me.grpCdInfo1, "grpCdInfo1")
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.lblSameCd)
        Me.grpCdInfo1.Controls.Add(Me.txtSameCd)
        Me.grpCdInfo1.Controls.Add(Me.txtDispSeq)
        Me.grpCdInfo1.Controls.Add(Me.lblDispSeq)
        Me.grpCdInfo1.Controls.Add(Me.lblAntiNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtAntiNmS)
        Me.grpCdInfo1.Controls.Add(Me.Label10)
        Me.grpCdInfo1.Controls.Add(Me.lblAntiNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtAntiNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblAntiNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtAntiNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblAntiNm)
        Me.grpCdInfo1.Controls.Add(Me.txtAntiNm)
        Me.grpCdInfo1.Controls.Add(Me.txtWNCd)
        Me.grpCdInfo1.Controls.Add(Me.lblWNCd)
        Me.grpCdInfo1.Controls.Add(Me.txtIFCd)
        Me.grpCdInfo1.Controls.Add(Me.lblIFCd)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.TabStop = False
        '
        'lblSameCd
        '
        Me.lblSameCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        resources.ApplyResources(Me.lblSameCd, "lblSameCd")
        Me.lblSameCd.ForeColor = System.Drawing.Color.White
        Me.lblSameCd.Name = "lblSameCd"
        '
        'txtSameCd
        '
        Me.txtSameCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtSameCd, "txtSameCd")
        Me.txtSameCd.Name = "txtSameCd"
        Me.txtSameCd.Tag = "SAMECD"
        '
        'txtDispSeq
        '
        Me.txtDispSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtDispSeq, "txtDispSeq")
        Me.txtDispSeq.Name = "txtDispSeq"
        Me.txtDispSeq.Tag = "DISPSEQ"
        '
        'lblDispSeq
        '
        Me.lblDispSeq.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        resources.ApplyResources(Me.lblDispSeq, "lblDispSeq")
        Me.lblDispSeq.ForeColor = System.Drawing.Color.White
        Me.lblDispSeq.Name = "lblDispSeq"
        '
        'lblAntiNmS
        '
        Me.lblAntiNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        resources.ApplyResources(Me.lblAntiNmS, "lblAntiNmS")
        Me.lblAntiNmS.ForeColor = System.Drawing.Color.White
        Me.lblAntiNmS.Name = "lblAntiNmS"
        '
        'txtAntiNmS
        '
        Me.txtAntiNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtAntiNmS, "txtAntiNmS")
        Me.txtAntiNmS.Name = "txtAntiNmS"
        Me.txtAntiNmS.Tag = "ANTINMS"
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'lblAntiNmP
        '
        Me.lblAntiNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        resources.ApplyResources(Me.lblAntiNmP, "lblAntiNmP")
        Me.lblAntiNmP.ForeColor = System.Drawing.Color.White
        Me.lblAntiNmP.Name = "lblAntiNmP"
        '
        'txtAntiNmP
        '
        Me.txtAntiNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtAntiNmP, "txtAntiNmP")
        Me.txtAntiNmP.Name = "txtAntiNmP"
        Me.txtAntiNmP.Tag = "ANTINMP"
        '
        'lblAntiNmD
        '
        Me.lblAntiNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        resources.ApplyResources(Me.lblAntiNmD, "lblAntiNmD")
        Me.lblAntiNmD.ForeColor = System.Drawing.Color.White
        Me.lblAntiNmD.Name = "lblAntiNmD"
        '
        'txtAntiNmD
        '
        Me.txtAntiNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtAntiNmD, "txtAntiNmD")
        Me.txtAntiNmD.Name = "txtAntiNmD"
        Me.txtAntiNmD.Tag = "ANTINMD"
        '
        'lblAntiNm
        '
        Me.lblAntiNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        resources.ApplyResources(Me.lblAntiNm, "lblAntiNm")
        Me.lblAntiNm.ForeColor = System.Drawing.Color.White
        Me.lblAntiNm.Name = "lblAntiNm"
        '
        'txtAntiNm
        '
        Me.txtAntiNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtAntiNm, "txtAntiNm")
        Me.txtAntiNm.Name = "txtAntiNm"
        Me.txtAntiNm.Tag = "ANTINM"
        '
        'txtWNCd
        '
        Me.txtWNCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtWNCd, "txtWNCd")
        Me.txtWNCd.Name = "txtWNCd"
        Me.txtWNCd.Tag = "ANTIWNCD"
        '
        'lblWNCd
        '
        Me.lblWNCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        resources.ApplyResources(Me.lblWNCd, "lblWNCd")
        Me.lblWNCd.ForeColor = System.Drawing.Color.White
        Me.lblWNCd.Name = "lblWNCd"
        '
        'txtIFCd
        '
        Me.txtIFCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtIFCd, "txtIFCd")
        Me.txtIFCd.Name = "txtIFCd"
        Me.txtIFCd.Tag = "ANTIIFCD"
        '
        'lblIFCd
        '
        Me.lblIFCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        resources.ApplyResources(Me.lblIFCd, "lblIFCd")
        Me.lblIFCd.ForeColor = System.Drawing.Color.White
        Me.lblIFCd.Name = "lblIFCd"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnGetExcel)
        Me.grpCd.Controls.Add(Me.txtAntiCd)
        Me.grpCd.Controls.Add(Me.lblAntiCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.dtpUSTime)
        Me.grpCd.Controls.Add(Me.txtUSDay)
        Me.grpCd.Controls.Add(Me.dtpUSDay)
        Me.grpCd.Controls.Add(Me.lblUSDayTime)
        resources.ApplyResources(Me.grpCd, "grpCd")
        Me.grpCd.Name = "grpCd"
        Me.grpCd.TabStop = False
        '
        'btnGetExcel
        '
        resources.ApplyResources(Me.btnGetExcel, "btnGetExcel")
        Me.btnGetExcel.Name = "btnGetExcel"
        Me.btnGetExcel.TabStop = False
        Me.btnGetExcel.UseVisualStyleBackColor = True
        '
        'txtAntiCd
        '
        Me.txtAntiCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.txtAntiCd, "txtAntiCd")
        Me.txtAntiCd.Name = "txtAntiCd"
        Me.txtAntiCd.Tag = "ANTICD"
        '
        'lblAntiCd
        '
        resources.ApplyResources(Me.lblAntiCd, "lblAntiCd")
        Me.lblAntiCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblAntiCd.ForeColor = System.Drawing.Color.White
        Me.lblAntiCd.Name = "lblAntiCd"
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        resources.ApplyResources(Me.btnUE, "btnUE")
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Name = "btnUE"
        Me.btnUE.TabStop = False
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'dtpUSTime
        '
        resources.ApplyResources(Me.dtpUSTime, "dtpUSTime")
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        resources.ApplyResources(Me.txtUSDay, "txtUSDay")
        Me.txtUSDay.Name = "txtUSDay"
        '
        'dtpUSDay
        '
        resources.ApplyResources(Me.dtpUSDay, "dtpUSDay")
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Name = "dtpUSDay"
        Me.dtpUSDay.TabStop = False
        Me.dtpUSDay.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'lblUSDayTime
        '
        resources.ApplyResources(Me.lblUSDayTime, "lblUSDayTime")
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Name = "lblUSDayTime"
        '
        'FDF17
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.pnlTop)
        Me.KeyPreview = True
        Me.Name = "FDF17"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If txtAntiCd.Text = "" Then Exit Sub

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "항균제코드 : " & txtAntiCd.Text & vbCrLf
            sMsg &= "항균제명   : " & txtAntiNm.Text & vbCrLf & vbCrLf
            sMsg &= "을(를) 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mobjDAF.TransAntiInfo_UE(txtAntiCd.Text, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString, sUeDate + sUeTime) Then
                MsgBox("해당 항균제정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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

    Private Sub dtpUSDay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpUSDay.ValueChanged
        If miSelectKey = 1 Then Exit Sub
        If txtUSDay.Text.Trim = "" Then Exit Sub

        txtUSDay.Text = Format(dtpUSDay.Value, "yyyy-MM-dd").Substring(0, 10)
    End Sub

    Private Sub txtAntiNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtAntiNm.Validating
        If miSelectKey = 1 Then Exit Sub

        If txtAntiNmS.Text.Trim = "" Then
            If txtAntiNm.Text.Length > txtAntiNmS.MaxLength Then
                txtAntiNmS.Text = txtAntiNm.Text.Substring(0, txtAntiNmS.MaxLength)
            Else
                txtAntiNmS.Text = txtAntiNm.Text
            End If
        End If

        If txtAntiNmD.Text.Trim = "" Then
            If txtAntiNm.Text.Length > txtAntiNmD.MaxLength Then
                txtAntiNmD.Text = txtAntiNm.Text.Substring(0, txtAntiNmD.MaxLength)
            Else
                txtAntiNmD.Text = txtAntiNm.Text
            End If
        End If

        If txtAntiNmP.Text.Trim = "" Then
            If txtAntiNm.Text.Length > txtAntiNmP.MaxLength Then
                txtAntiNmP.Text = txtAntiNm.Text.Substring(0, txtAntiNmP.MaxLength)
            Else
                txtAntiNmP.Text = txtAntiNm.Text
            End If
        End If
    End Sub

    Private Sub btnGetExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetExcel.Click
        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim dt As New DataTable

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open("c:\as\항생제코드.xls")

            xlsWkS = CType(xlsWkB.Sheets("drug"), Excel.Worksheet)

            For iLine As Integer = 2 To 132
                Dim sUSDT As String = "20010101000000"
                Dim sUEDT As String = "30000101000000"

                Dim sAntiCd As String = xlsWkS.Range("A" + CStr(iLine)).Value.ToString
                Dim sAntiNmd As String = xlsWkS.Range("B" + CStr(iLine)).Value.ToString

                dt = mobjDAF.GetAntiInfo(sAntiCd, sUSDT)
                If dt.Rows.Count < 1 Then

                    Dim it230 As New LISAPP.ItemTableCollection

                    Dim sRegDT As String = fnGetSystemDT()

                    With it230
                        .SetItemTable("ANTICD", 1, 1, sAntiCd)
                        .SetItemTable("USDT", 2, 1, sUSDT)
                        .SetItemTable("UEDT", 3, 1, sUEDT)

                        .SetItemTable("REGDT", 4, 1, "20110623000000")
                        .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                        .SetItemTable("REGIP", 6, 1, USER_INFO.LOCALIP)
                        .SetItemTable("ANTINM", 7, 1, sAntiNmd)
                        .SetItemTable("ANTINMS", 8, 1, sAntiNmd)
                        .SetItemTable("ANTINMD", 9, 1, sAntiNmd)
                        .SetItemTable("ANTINMP", 10, 1, sAntiNmd)
                        .SetItemTable("ANTIIFCD", 11, 1, "")
                        .SetItemTable("ANTIWNCD", 12, 1, "")
                        .SetItemTable("DISPSEQ", 13, 1, "")
                    End With

                    mobjDAF.TransAntiInfo(it230, 0, sAntiCd, sUSDT)
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If Not xlsWkS Is Nothing Then xlsWkS = Nothing
            If Not xlsWkB Is Nothing Then xlsWkB.Close(False) : xlsWkB = Nothing
            If Not xlsApp Is Nothing Then xlsApp.Quit() : xlsApp = Nothing
        End Try

    End Sub

    Private Sub FDF17_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtAntiCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAntiCd.KeyDown, txtAntiNm.KeyDown, txtAntiNmD.KeyDown, txtAntiNmP.KeyDown, txtAntiNmS.KeyDown, txtDispSeq.KeyDown, txtIFCd.KeyDown, txtWNCd.KeyDown, txtSameCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub
End Class
