'>>> [06] 용기
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO

Imports COMMON.CommFN
Imports COMMON.commlogin.login
Imports COMMON.CommConst

Public Class FDF06
    Inherits System.Windows.Forms.Form

    Private Const mcFile As String = "File : FDF06.vb, Class : FDF06" + vbTab
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_TUBE
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picBuf As System.Windows.Forms.PictureBox
    Friend WithEvents btnReg_img As CButtonLib.CButton
    Friend WithEvents txtFileNm As System.Windows.Forms.TextBox
    Friend WithEvents cbotubecolor As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnFileOpen As CButtonLib.CButton

    Private Function fnImgFile_Get(ByVal rsFileNm As String) As Byte()

        Try
            Dim fs As IO.FileStream = New IO.FileStream(rsFileNm, IO.FileMode.Open, IO.FileAccess.Read)
            Dim br As IO.BinaryReader = New IO.BinaryReader(fs)

            Dim a_btReturn() As Byte = br.ReadBytes(CType(fs.Length, Integer))

            br.Close()
            fs.Close()

            Return a_btReturn

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Function

    Private Sub sbEditUseDt_Del()
        Dim sFn As String = "Sub sbEditUseDt_Del"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            '> 코드사용여부 조사
            dt = mobjDAF.GetUsUeCd_Tube(Me.txtTubeCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용중인 코드입니다. 그래도 삭제하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "삭제 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            bReturn = mobjDAF.TransTubeInfo_DEL(Me.txtTubeCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID)

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

    Private Sub sbEditUseDt_Edit(ByVal rsUseTag As String, ByVal rsUseDt As String)
        Dim sFn As String = "Sub sbEditUseDt_Edit"

        Try
            Dim bReturn As Boolean = False
            Dim dt As New DataTable

            rsUseDt = rsUseDt.Replace("-", "").Replace(":", "").Replace(" ", "")
            '> 사용중복 조사
            dt = mobjDAF.GetUsUeDupl_Tube(Me.txtTubeCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), rsUseTag.ToUpper, rsUseDt)

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                If MsgBox("사용일시 구간에 동일한 코드가 존재합니다. 그래도 수정하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "사용일시 구간 동일코드 확인") = MsgBoxResult.No Then
                    Return
                End If
            End If

            If rsUseTag.ToUpper = "USDT" Then
                bReturn = mobjDAF.TransTubeInfo_UPD_US(Me.txtTubeCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
            ElseIf rsUseTag.ToUpper = "UEDT" Then
                bReturn = mobjDAF.TransTubeInfo_UPD_UE(Me.txtTubeCd.Text, Me.txtUSDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), USER_INFO.USRID, rsUseDt)
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

    Public Sub sbEditUseDt(ByVal rsUseTag As String)
        Dim sFn As String = "Public Sub sbEditUseDt"

        Try
            Dim fgf02 As New FGF03

            With fgf02
                .txtCd.Text = Me.txtTubeCd.Text
                .txtNm.Text = Me.txtTubeNm.Text

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

    Private Function fnCollectItemTable_40(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_40() As LISAPP.ItemTableCollection"

        Try
            Dim it40 As New LISAPP.ItemTableCollection

            With it40
                .SetItemTable("TUBECD", 1, 1, Me.txtTubeCd.Text)
                .SetItemTable("USDT", 2, 1, Me.txtUSDay.Text.Replace("-", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString)

                If txtUEDT.Text = "" Then
                    .SetItemTable("UEDT", 3, 1, msUEDT)
                Else
                    .SetItemTable("UEDT", 3, 1, Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", ""))
                End If

                .SetItemTable("REGDT", 4, 1, rsRegDT)
                .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                .SetItemTable("TUBENM", 6, 1, Me.txtTubeNm.Text)
                .SetItemTable("TUBENMS", 7, 1, Me.txtTubeNmS.Text)
                .SetItemTable("TUBENMD", 8, 1, Me.txtTubeNmD.Text)
                .SetItemTable("TUBENMP", 9, 1, Me.txtTubeNmP.Text)
                .SetItemTable("TUBENMBP", 10, 1, Me.txtTubeNmBP.Text)
                .SetItemTable("TUBEVOL", 11, 1, Me.txtVol.Text)
                .SetItemTable("TUBEUNIT", 12, 1, Me.txtUnit.Text)
                .SetItemTable("TUBEIFCD", 13, 1, Me.txtIFCd.Text)
                .SetItemTable("REGIP", 14, 1, USER_INFO.LOCALIP)
                .SetItemTable("TUBECOLOR", 15, 1, Me.cbotubecolor.SelectedItem.ToString)
            End With

            Return it40
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
            Return New LISAPP.ItemTableCollection
        End Try
    End Function

    Private Function fnFindChildControl(ByVal actrlCol As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control
        Dim sFn As String = "Private Function fnFindChildControl(ByVal actrlCol As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control"

        Try
            Dim ctrl As System.Windows.Forms.Control

            For Each ctrl In actrlCol
                If ctrl.Controls.Count > 0 Then
                    fnFindChildControl(ctrl.Controls)
                ElseIf ctrl.Controls.Count = 0 Then
                    If CType(ctrl.Tag, String) <> "" Then
                        mchildctrlcol.Add(ctrl)
                    End If
                End If
            Next
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsTubeCd As String, ByVal rsUsDt As String) As String
        Dim sFn As String = ""

        Try

            Dim dt As DataTable = mobjDAF.GetRecentTubeInfo(rsTubeCd, rsUsDt)

            If dt.Rows.Count > 0 Then
                Return "시작일시가 " + dt.Rows(0).Item(0).ToString + "인 동일 용기 코드가 존재합니다." + vbCrLf + vbCrLf + _
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
                fnGetSystemDT = dt.Rows(0).Item(0).ToString
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

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnReg() As Boolean"

        Try
            Dim it40 As New LISAPP.ItemTableCollection
            Dim iRegType40 As Integer = 0
            Dim sRegDT As String

            iRegType40 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it40 = fnCollectItemTable_40(sRegDT)

            If mobjDAF.TransTubeInfo(it40, iRegType40, Me.txtTubeCd.Text, Me.txtUSDay.Text.Replace("-", "").Replace(":", "").Replace(" ", "") + Format(Me.dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID) Then
                fnReg = True
            Else
                fnReg = False
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        fnValidate = False

        Try
            If Len(Me.txtTubeCd.Text.Trim) < 2 Then
                MsgBox("용기코드를 (정확히) 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsDate(Me.txtUSDay.Text) Then
                MsgBox("시작일시를 정확히 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtTubeCd.Text, txtUSDay.Text.Replace("-", "") + Format(dtpUSTime.Value, "HHmmss").ToString)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Exit Function
                    End If
                End If
            End If

            If Me.txtTubeNm.Text.Trim = "" Then
                MsgBox("용기명을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If txtTubeNmS.Text.Trim = "" Then
                MsgBox("용기명(약어)를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTubeNmD.Text.Trim = "" Then
                MsgBox("용기명(화면)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
            End If

            If Me.txtTubeNmP.Text.Trim = "" Then
                MsgBox("용기명(출력)을 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Exit Function
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

    Public Sub sbDisplayCdDetail(ByVal rsTubeCd As String, ByVal rsUsDt As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_Tube(rsTubeCd, rsUsDt)
            sbDisplayCdDetail_img(rsTubeCd)

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Tube(ByVal rsTubeCd As String, ByVal rsUsDt As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_Tube()"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mobjDAF.GetTubeInfo(rsTubeCd, rsUsDt)

            '초기화할 것은 ErrorProvider
            sbInitialize_ErrProvider()

            sbInitialize_CtrlCollection()

            fnFindChildControl(Me.Controls)

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

    Private Sub sbDisplayCdDetail_img(ByVal rsTubeCd As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail_img()"
        Dim iCol As Integer = 0

        Try

            Dim a_btBuf As Byte() = mobjDAF.GetTubeInfo_img(rsTubeCd)
            Dim sDir As String = Application.StartupPath + "\Image"
            Dim sFileNm = sDir + "\Tube_" + rsTubeCd + ".jpg"

            If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

            Dim fs As IO.FileStream

            If a_btBuf IsNot Nothing Then

                If IO.File.Exists(sFileNm) Then
                    Try
                        Threading.Thread.Sleep(100)
                        IO.File.Delete(sFileNm)
                    Catch ex As Exception
                        Me.txtFileNm.Text = sFileNm

                        Dim bmpTmp As Bitmap = New Bitmap(sFileNm)

                        Me.picBuf.Image = CType(bmpTmp, Image)
                        Return
                    End Try
                End If

                fs = New IO.FileStream(sFileNm, IO.FileMode.Create, FileAccess.Write)

            Else
                Me.picBuf.Image = Nothing

                Return
            End If

            Dim bw As IO.BinaryWriter = New IO.BinaryWriter(fs)

            bw.Write(a_btBuf)
            bw.Flush()

            bw.Close()
            fs.Close()

            Me.txtFileNm.Text = sFileNm

            Dim bmpBuf As Bitmap = New Bitmap(sFileNm)

            Me.picBuf.SizeMode = PictureBoxSizeMode.Zoom

            Me.picBuf.Image = CType(bmpBuf, Image)

            bmpBuf = Nothing
            fs = Nothing
            'Clipboard.SetDataObject(Me.picBuf.Image)

            'Me.picBuf.Image.Dispose()
            'Me.picBuf.Image = Nothing

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRID = "ACK" Then btnExcel.Visible = True
            If USER_INFO.USRLVL = "S" Then
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
                'tpg1 초기화
                txtTubeCd.Text = "" : btnUE.Visible = False
                txtTubeNm.Text = "" : txtTubeNmS.Text = "" : txtTubeNmD.Text = "" : txtTubeNmP.Text = "" : txtTubeNmBP.Text = ""
                txtVol.Text = "" : txtUnit.Text = "" : txtIFCd.Text = ""
                txtUSDT.Text = "" : txtUEDT.Text = "" : txtRegDT.Text = "" : txtRegID.Text = "" : txtRegNm.Text = ""

                Me.picBuf.Image = Nothing

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

            Me.txtUSDay.Text = sSysDT.Substring(0, 10)
            Me.dtpUSDay.Value = CType(sSysDT, Date)
            Me.dtpUSTime.Value = CType(sSysDT, Date)
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
    Friend WithEvents txtUEDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUEDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtUSDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblUSDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents dtpUSTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUSDay As System.Windows.Forms.TextBox
    Friend WithEvents dtpUSDay As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents lblTubeCd As System.Windows.Forms.Label
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents lblTubeNmS As System.Windows.Forms.Label
    Friend WithEvents lblTubeNmP As System.Windows.Forms.Label
    Friend WithEvents lblTubeNmD As System.Windows.Forms.Label
    Friend WithEvents lblTubeNm As System.Windows.Forms.Label
    Friend WithEvents lblTubeNmBP As System.Windows.Forms.Label
    Friend WithEvents lblVol As System.Windows.Forms.Label
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents lblIFCd As System.Windows.Forms.Label
    Friend WithEvents txtTubeNmS As System.Windows.Forms.TextBox
    Friend WithEvents txtTubeNmP As System.Windows.Forms.TextBox
    Friend WithEvents txtTubeNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtTubeNm As System.Windows.Forms.TextBox
    Friend WithEvents txtTubeNmBP As System.Windows.Forms.TextBox
    Friend WithEvents txtVol As System.Windows.Forms.TextBox
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents txtIFCd As System.Windows.Forms.TextBox
    Friend WithEvents txtTubeCd As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF06))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tclSpc = New System.Windows.Forms.TabControl()
        Me.tbcTpg = New System.Windows.Forms.TabPage()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.txtUEDT = New System.Windows.Forms.TextBox()
        Me.lblUEDT = New System.Windows.Forms.Label()
        Me.txtRegDT = New System.Windows.Forms.TextBox()
        Me.txtUSDT = New System.Windows.Forms.TextBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblRegDT = New System.Windows.Forms.Label()
        Me.lblUSDT = New System.Windows.Forms.Label()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox()
        Me.cbotubecolor = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnReg_img = New CButtonLib.CButton()
        Me.txtFileNm = New System.Windows.Forms.TextBox()
        Me.btnFileOpen = New CButtonLib.CButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.picBuf = New System.Windows.Forms.PictureBox()
        Me.lblIFCd = New System.Windows.Forms.Label()
        Me.txtIFCd = New System.Windows.Forms.TextBox()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.lblVol = New System.Windows.Forms.Label()
        Me.txtVol = New System.Windows.Forms.TextBox()
        Me.lblTubeNmBP = New System.Windows.Forms.Label()
        Me.lblTubeNmS = New System.Windows.Forms.Label()
        Me.txtTubeNmS = New System.Windows.Forms.TextBox()
        Me.lblTubeNmP = New System.Windows.Forms.Label()
        Me.txtTubeNmP = New System.Windows.Forms.TextBox()
        Me.lblTubeNmD = New System.Windows.Forms.Label()
        Me.txtTubeNmD = New System.Windows.Forms.TextBox()
        Me.lblTubeNm = New System.Windows.Forms.Label()
        Me.txtTubeNm = New System.Windows.Forms.TextBox()
        Me.txtTubeNmBP = New System.Windows.Forms.TextBox()
        Me.grpCd = New System.Windows.Forms.GroupBox()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btnUE = New System.Windows.Forms.Button()
        Me.dtpUSTime = New System.Windows.Forms.DateTimePicker()
        Me.txtUSDay = New System.Windows.Forms.TextBox()
        Me.dtpUSDay = New System.Windows.Forms.DateTimePicker()
        Me.lblUSDayTime = New System.Windows.Forms.Label()
        Me.lblTubeCd = New System.Windows.Forms.Label()
        Me.txtTubeCd = New System.Windows.Forms.TextBox()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.picBuf, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 116
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tbcTpg)
        Me.tclSpc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tclSpc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tclSpc.ItemSize = New System.Drawing.Size(84, 17)
        Me.tclSpc.Location = New System.Drawing.Point(0, 0)
        Me.tclSpc.Name = "tclSpc"
        Me.tclSpc.SelectedIndex = 0
        Me.tclSpc.Size = New System.Drawing.Size(788, 601)
        Me.tclSpc.TabIndex = 0
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
        Me.tbcTpg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcTpg.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg.Name = "tbcTpg"
        Me.tbcTpg.Size = New System.Drawing.Size(780, 576)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "용기정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(704, 540)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 16
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtUEDT
        '
        Me.txtUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUEDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUEDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUEDT.Location = New System.Drawing.Point(316, 540)
        Me.txtUEDT.Name = "txtUEDT"
        Me.txtUEDT.ReadOnly = True
        Me.txtUEDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUEDT.TabIndex = 0
        Me.txtUEDT.TabStop = False
        Me.txtUEDT.Tag = "UEDT"
        '
        'lblUEDT
        '
        Me.lblUEDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUEDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUEDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUEDT.ForeColor = System.Drawing.Color.Black
        Me.lblUEDT.Location = New System.Drawing.Point(218, 540)
        Me.lblUEDT.Name = "lblUEDT"
        Me.lblUEDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUEDT.TabIndex = 0
        Me.lblUEDT.Tag = ""
        Me.lblUEDT.Text = "종료일시(선택)"
        Me.lblUEDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(510, 540)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 0
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'txtUSDT
        '
        Me.txtUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtUSDT.BackColor = System.Drawing.Color.LightGray
        Me.txtUSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUSDT.Location = New System.Drawing.Point(108, 539)
        Me.txtUSDT.Name = "txtUSDT"
        Me.txtUSDT.ReadOnly = True
        Me.txtUSDT.Size = New System.Drawing.Size(100, 21)
        Me.txtUSDT.TabIndex = 0
        Me.txtUSDT.TabStop = False
        Me.txtUSDT.Tag = "USDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(619, 540)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 21)
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
        Me.lblRegDT.Location = New System.Drawing.Point(425, 540)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUSDT
        '
        Me.lblUSDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUSDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUSDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDT.ForeColor = System.Drawing.Color.Black
        Me.lblUSDT.Location = New System.Drawing.Point(10, 539)
        Me.lblUSDT.Name = "lblUSDT"
        Me.lblUSDT.Size = New System.Drawing.Size(97, 21)
        Me.lblUSDT.TabIndex = 0
        Me.lblUSDT.Text = "시작일시(선택)"
        Me.lblUSDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(704, 540)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.cbotubecolor)
        Me.grpCdInfo1.Controls.Add(Me.Label2)
        Me.grpCdInfo1.Controls.Add(Me.btnReg_img)
        Me.grpCdInfo1.Controls.Add(Me.txtFileNm)
        Me.grpCdInfo1.Controls.Add(Me.btnFileOpen)
        Me.grpCdInfo1.Controls.Add(Me.Label1)
        Me.grpCdInfo1.Controls.Add(Me.picBuf)
        Me.grpCdInfo1.Controls.Add(Me.lblIFCd)
        Me.grpCdInfo1.Controls.Add(Me.txtIFCd)
        Me.grpCdInfo1.Controls.Add(Me.lblUnit)
        Me.grpCdInfo1.Controls.Add(Me.txtUnit)
        Me.grpCdInfo1.Controls.Add(Me.lblVol)
        Me.grpCdInfo1.Controls.Add(Me.txtVol)
        Me.grpCdInfo1.Controls.Add(Me.lblTubeNmBP)
        Me.grpCdInfo1.Controls.Add(Me.lblTubeNmS)
        Me.grpCdInfo1.Controls.Add(Me.txtTubeNmS)
        Me.grpCdInfo1.Controls.Add(Me.lblTubeNmP)
        Me.grpCdInfo1.Controls.Add(Me.txtTubeNmP)
        Me.grpCdInfo1.Controls.Add(Me.lblTubeNmD)
        Me.grpCdInfo1.Controls.Add(Me.txtTubeNmD)
        Me.grpCdInfo1.Controls.Add(Me.lblTubeNm)
        Me.grpCdInfo1.Controls.Add(Me.txtTubeNm)
        Me.grpCdInfo1.Controls.Add(Me.txtTubeNmBP)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 76)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 457)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "용기정보"
        '
        'cbotubecolor
        '
        Me.cbotubecolor.FormattingEnabled = True
        Me.cbotubecolor.Items.AddRange(New Object() {"", "빨강색", "주황색", "노랑색", "초록색", "하늘색", "파랑색", "남색", "보라색", "흰색", "검은색"})
        Me.cbotubecolor.Location = New System.Drawing.Point(107, 217)
        Me.cbotubecolor.Name = "cbotubecolor"
        Me.cbotubecolor.Size = New System.Drawing.Size(121, 20)
        Me.cbotubecolor.TabIndex = 166
        Me.cbotubecolor.Tag = "TUBECOLOR_01"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(8, 216)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 21)
        Me.Label2.TabIndex = 165
        Me.Label2.Text = "용기 색상"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnReg_img
        '
        Me.btnReg_img.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnReg_img.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_img.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnReg_img.ColorFillBlend = CBlendItems1
        Me.btnReg_img.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg_img.Corners.All = CType(6, Short)
        Me.btnReg_img.Corners.LowerLeft = CType(6, Short)
        Me.btnReg_img.Corners.LowerRight = CType(6, Short)
        Me.btnReg_img.Corners.UpperLeft = CType(6, Short)
        Me.btnReg_img.Corners.UpperRight = CType(6, Short)
        Me.btnReg_img.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg_img.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg_img.FocalPoints.CenterPtX = 1.0!
        Me.btnReg_img.FocalPoints.CenterPtY = 1.0!
        Me.btnReg_img.FocalPoints.FocusPtX = 0.0!
        Me.btnReg_img.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_img.FocusPtTracker = DesignerRectTracker2
        Me.btnReg_img.Image = Nothing
        Me.btnReg_img.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_img.ImageIndex = 0
        Me.btnReg_img.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg_img.Location = New System.Drawing.Point(444, 290)
        Me.btnReg_img.Name = "btnReg_img"
        Me.btnReg_img.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg_img.SideImage = Nothing
        Me.btnReg_img.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg_img.Size = New System.Drawing.Size(95, 21)
        Me.btnReg_img.TabIndex = 164
        Me.btnReg_img.Text = "이미지 저장"
        Me.btnReg_img.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg_img.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'txtFileNm
        '
        Me.txtFileNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFileNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtFileNm.Location = New System.Drawing.Point(434, 23)
        Me.txtFileNm.MaxLength = 0
        Me.txtFileNm.Name = "txtFileNm"
        Me.txtFileNm.Size = New System.Drawing.Size(160, 21)
        Me.txtFileNm.TabIndex = 163
        Me.txtFileNm.Tag = "TUBENMP"
        Me.txtFileNm.Visible = False
        '
        'btnFileOpen
        '
        Me.btnFileOpen.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnFileOpen.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFileOpen.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems2.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnFileOpen.ColorFillBlend = CBlendItems2
        Me.btnFileOpen.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnFileOpen.Corners.All = CType(6, Short)
        Me.btnFileOpen.Corners.LowerLeft = CType(6, Short)
        Me.btnFileOpen.Corners.LowerRight = CType(6, Short)
        Me.btnFileOpen.Corners.UpperLeft = CType(6, Short)
        Me.btnFileOpen.Corners.UpperRight = CType(6, Short)
        Me.btnFileOpen.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnFileOpen.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnFileOpen.FocalPoints.CenterPtX = 1.0!
        Me.btnFileOpen.FocalPoints.CenterPtY = 1.0!
        Me.btnFileOpen.FocalPoints.FocusPtX = 0.0!
        Me.btnFileOpen.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFileOpen.FocusPtTracker = DesignerRectTracker4
        Me.btnFileOpen.Image = Nothing
        Me.btnFileOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFileOpen.ImageIndex = 0
        Me.btnFileOpen.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnFileOpen.Location = New System.Drawing.Point(336, 290)
        Me.btnFileOpen.Name = "btnFileOpen"
        Me.btnFileOpen.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnFileOpen.SideImage = Nothing
        Me.btnFileOpen.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnFileOpen.Size = New System.Drawing.Size(107, 21)
        Me.btnFileOpen.TabIndex = 162
        Me.btnFileOpen.Text = "이미지 가져오기"
        Me.btnFileOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnFileOpen.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(336, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 21)
        Me.Label1.TabIndex = 161
        Me.Label1.Text = "용기이미지"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picBuf
        '
        Me.picBuf.BackColor = System.Drawing.Color.White
        Me.picBuf.Location = New System.Drawing.Point(336, 47)
        Me.picBuf.Name = "picBuf"
        Me.picBuf.Size = New System.Drawing.Size(203, 241)
        Me.picBuf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBuf.TabIndex = 160
        Me.picBuf.TabStop = False
        '
        'lblIFCd
        '
        Me.lblIFCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblIFCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIFCd.ForeColor = System.Drawing.Color.Black
        Me.lblIFCd.Location = New System.Drawing.Point(8, 194)
        Me.lblIFCd.Name = "lblIFCd"
        Me.lblIFCd.Size = New System.Drawing.Size(97, 21)
        Me.lblIFCd.TabIndex = 14
        Me.lblIFCd.Text = "IF 코드"
        Me.lblIFCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIFCd
        '
        Me.txtIFCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIFCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtIFCd.Location = New System.Drawing.Point(106, 194)
        Me.txtIFCd.MaxLength = 10
        Me.txtIFCd.Name = "txtIFCd"
        Me.txtIFCd.Size = New System.Drawing.Size(68, 21)
        Me.txtIFCd.TabIndex = 11
        Me.txtIFCd.Tag = "TUBEIFCD"
        '
        'lblUnit
        '
        Me.lblUnit.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUnit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUnit.ForeColor = System.Drawing.Color.Black
        Me.lblUnit.Location = New System.Drawing.Point(8, 172)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(97, 21)
        Me.lblUnit.TabIndex = 12
        Me.lblUnit.Text = "단위 Unit"
        Me.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUnit
        '
        Me.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUnit.Location = New System.Drawing.Point(106, 172)
        Me.txtUnit.MaxLength = 10
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(68, 21)
        Me.txtUnit.TabIndex = 10
        Me.txtUnit.Tag = "TUBEUNIT"
        '
        'lblVol
        '
        Me.lblVol.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblVol.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblVol.ForeColor = System.Drawing.Color.Black
        Me.lblVol.Location = New System.Drawing.Point(8, 150)
        Me.lblVol.Name = "lblVol"
        Me.lblVol.Size = New System.Drawing.Size(97, 21)
        Me.lblVol.TabIndex = 10
        Me.lblVol.Text = "용량 Vol."
        Me.lblVol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtVol
        '
        Me.txtVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVol.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtVol.Location = New System.Drawing.Point(106, 150)
        Me.txtVol.MaxLength = 10
        Me.txtVol.Name = "txtVol"
        Me.txtVol.Size = New System.Drawing.Size(68, 21)
        Me.txtVol.TabIndex = 9
        Me.txtVol.Tag = "TUBEVOL"
        '
        'lblTubeNmBP
        '
        Me.lblTubeNmBP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTubeNmBP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTubeNmBP.ForeColor = System.Drawing.Color.White
        Me.lblTubeNmBP.Location = New System.Drawing.Point(8, 113)
        Me.lblTubeNmBP.Name = "lblTubeNmBP"
        Me.lblTubeNmBP.Size = New System.Drawing.Size(97, 21)
        Me.lblTubeNmBP.TabIndex = 0
        Me.lblTubeNmBP.Text = "용기명(바코드)"
        Me.lblTubeNmBP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTubeNmS
        '
        Me.lblTubeNmS.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTubeNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTubeNmS.ForeColor = System.Drawing.Color.White
        Me.lblTubeNmS.Location = New System.Drawing.Point(8, 47)
        Me.lblTubeNmS.Name = "lblTubeNmS"
        Me.lblTubeNmS.Size = New System.Drawing.Size(97, 21)
        Me.lblTubeNmS.TabIndex = 0
        Me.lblTubeNmS.Text = "용기명(약어)"
        Me.lblTubeNmS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTubeNmS
        '
        Me.txtTubeNmS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeNmS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTubeNmS.Location = New System.Drawing.Point(106, 47)
        Me.txtTubeNmS.MaxLength = 10
        Me.txtTubeNmS.Name = "txtTubeNmS"
        Me.txtTubeNmS.Size = New System.Drawing.Size(128, 21)
        Me.txtTubeNmS.TabIndex = 5
        Me.txtTubeNmS.Tag = "TUBENMS"
        '
        'lblTubeNmP
        '
        Me.lblTubeNmP.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTubeNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTubeNmP.ForeColor = System.Drawing.Color.White
        Me.lblTubeNmP.Location = New System.Drawing.Point(8, 91)
        Me.lblTubeNmP.Name = "lblTubeNmP"
        Me.lblTubeNmP.Size = New System.Drawing.Size(97, 21)
        Me.lblTubeNmP.TabIndex = 0
        Me.lblTubeNmP.Text = "용기명(출력)"
        Me.lblTubeNmP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTubeNmP
        '
        Me.txtTubeNmP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeNmP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTubeNmP.Location = New System.Drawing.Point(106, 91)
        Me.txtTubeNmP.MaxLength = 20
        Me.txtTubeNmP.Name = "txtTubeNmP"
        Me.txtTubeNmP.Size = New System.Drawing.Size(128, 21)
        Me.txtTubeNmP.TabIndex = 7
        Me.txtTubeNmP.Tag = "TUBENMP"
        '
        'lblTubeNmD
        '
        Me.lblTubeNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTubeNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTubeNmD.ForeColor = System.Drawing.Color.White
        Me.lblTubeNmD.Location = New System.Drawing.Point(8, 69)
        Me.lblTubeNmD.Name = "lblTubeNmD"
        Me.lblTubeNmD.Size = New System.Drawing.Size(97, 21)
        Me.lblTubeNmD.TabIndex = 0
        Me.lblTubeNmD.Text = "용기명(화면)"
        Me.lblTubeNmD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTubeNmD
        '
        Me.txtTubeNmD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeNmD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTubeNmD.Location = New System.Drawing.Point(106, 69)
        Me.txtTubeNmD.MaxLength = 20
        Me.txtTubeNmD.Name = "txtTubeNmD"
        Me.txtTubeNmD.Size = New System.Drawing.Size(128, 21)
        Me.txtTubeNmD.TabIndex = 6
        Me.txtTubeNmD.Tag = "TUBENMD"
        '
        'lblTubeNm
        '
        Me.lblTubeNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTubeNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTubeNm.ForeColor = System.Drawing.Color.White
        Me.lblTubeNm.Location = New System.Drawing.Point(8, 25)
        Me.lblTubeNm.Name = "lblTubeNm"
        Me.lblTubeNm.Size = New System.Drawing.Size(97, 21)
        Me.lblTubeNm.TabIndex = 0
        Me.lblTubeNm.Text = "용기명"
        Me.lblTubeNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTubeNm
        '
        Me.txtTubeNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTubeNm.Location = New System.Drawing.Point(106, 25)
        Me.txtTubeNm.MaxLength = 20
        Me.txtTubeNm.Name = "txtTubeNm"
        Me.txtTubeNm.Size = New System.Drawing.Size(128, 21)
        Me.txtTubeNm.TabIndex = 4
        Me.txtTubeNm.Tag = "TUBENM"
        '
        'txtTubeNmBP
        '
        Me.txtTubeNmBP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeNmBP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTubeNmBP.Location = New System.Drawing.Point(106, 113)
        Me.txtTubeNmBP.MaxLength = 10
        Me.txtTubeNmBP.Name = "txtTubeNmBP"
        Me.txtTubeNmBP.Size = New System.Drawing.Size(68, 21)
        Me.txtTubeNmBP.TabIndex = 8
        Me.txtTubeNmBP.Tag = "TUBENMBP"
        '
        'grpCd
        '
        Me.grpCd.Controls.Add(Me.btnExcel)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Controls.Add(Me.dtpUSTime)
        Me.grpCd.Controls.Add(Me.txtUSDay)
        Me.grpCd.Controls.Add(Me.dtpUSDay)
        Me.grpCd.Controls.Add(Me.lblUSDayTime)
        Me.grpCd.Controls.Add(Me.lblTubeCd)
        Me.grpCd.Controls.Add(Me.txtTubeCd)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 66)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        Me.grpCd.Text = "용기 코드"
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(604, 22)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(62, 25)
        Me.btnExcel.TabIndex = 9
        Me.btnExcel.TabStop = False
        Me.btnExcel.Text = "Excel"
        Me.btnExcel.UseVisualStyleBackColor = True
        Me.btnExcel.Visible = False
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(686, 20)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 6
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'dtpUSTime
        '
        Me.dtpUSTime.CustomFormat = "HH:mm:ss"
        Me.dtpUSTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSTime.Location = New System.Drawing.Point(200, 15)
        Me.dtpUSTime.Name = "dtpUSTime"
        Me.dtpUSTime.Size = New System.Drawing.Size(56, 21)
        Me.dtpUSTime.TabIndex = 2
        Me.dtpUSTime.TabStop = False
        Me.dtpUSTime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'txtUSDay
        '
        Me.txtUSDay.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUSDay.Location = New System.Drawing.Point(101, 15)
        Me.txtUSDay.MaxLength = 10
        Me.txtUSDay.Name = "txtUSDay"
        Me.txtUSDay.Size = New System.Drawing.Size(77, 21)
        Me.txtUSDay.TabIndex = 1
        Me.txtUSDay.Text = "1990-01-01"
        '
        'dtpUSDay
        '
        Me.dtpUSDay.CustomFormat = "yyyy-MM-dd"
        Me.dtpUSDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUSDay.Location = New System.Drawing.Point(179, 15)
        Me.dtpUSDay.Name = "dtpUSDay"
        Me.dtpUSDay.Size = New System.Drawing.Size(20, 21)
        Me.dtpUSDay.TabIndex = 2
        Me.dtpUSDay.TabStop = False
        Me.dtpUSDay.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'lblUSDayTime
        '
        Me.lblUSDayTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Location = New System.Drawing.Point(8, 15)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(92, 21)
        Me.lblUSDayTime.TabIndex = 0
        Me.lblUSDayTime.Text = "시작일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTubeCd
        '
        Me.lblTubeCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTubeCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTubeCd.ForeColor = System.Drawing.Color.White
        Me.lblTubeCd.Location = New System.Drawing.Point(8, 37)
        Me.lblTubeCd.Name = "lblTubeCd"
        Me.lblTubeCd.Size = New System.Drawing.Size(92, 21)
        Me.lblTubeCd.TabIndex = 0
        Me.lblTubeCd.Text = "용기코드"
        Me.lblTubeCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTubeCd
        '
        Me.txtTubeCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTubeCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTubeCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTubeCd.Location = New System.Drawing.Point(101, 37)
        Me.txtTubeCd.MaxLength = 2
        Me.txtTubeCd.Name = "txtTubeCd"
        Me.txtTubeCd.Size = New System.Drawing.Size(31, 21)
        Me.txtTubeCd.TabIndex = 3
        Me.txtTubeCd.Tag = "TUBECD"
        '
        'FDF06
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF06"
        Me.Text = "[06] 용기"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.picBuf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnUE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUE.Click
        Dim sFn As String = "Private Sub btnUE_Click"

        Dim objFrm As Windows.Forms.Form
        Dim sUeDate As String
        Dim sUeTime As String

        If Me.txtTubeCd.Text = "" Then Exit Sub

        Try
            If fnGetSystemDT() >= Me.txtUEDT.Text.Replace("-", "").Replace(":", "").Replace(" ", "") Then
                MsgBox("이미 사용종료된 항목입니다. 확인하여 주십시요!!")
                Return
            End If

            Dim sMsg As String = "용기코드   : " + Me.txtTubeCd.Text + vbCrLf
            sMsg += "용기명     : " + Me.txtTubeNm.Text + vbCrLf + vbCrLf
            sMsg += "을(를) 사용종료하시겠습니까?"

            objFrm = New FGF02
            CType(objFrm, FGF02).LABEL() = sMsg
            objFrm.ShowDialog()
            If CType(objFrm, FGF02).ACTION.ToString <> "YES" Then Exit Sub

            sUeDate = CType(objFrm, FGF02).UEDate.ToString.Replace("-", "")
            sUeTime = CType(objFrm, FGF02).UETime.ToString.Replace(":", "")

            If mobjDAF.TransTubeInfo_UE(Me.txtTubeCd.Text, Me.txtUSDay.Text.Replace("-", "").Replace(":", "").Replace(" ", "") + Format(dtpUSTime.Value, "HHmmss").ToString, USER_INFO.USRID, sUeDate + sUeTime) Then
                MsgBox("해당 용기정보가 사용종료 되었습니다!!", MsgBoxStyle.Information)

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
        If Me.txtUSDay.Text.Trim = "" Then Exit Sub

        Me.txtUSDay.Text = Format(dtpUSDay.Value, "yyyy-MM-dd").Substring(0, 10)
    End Sub

    Private Sub txtTubeNm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTubeNm.Validating
        If miSelectKey = 1 Then Exit Sub


        If txtTubeNmS.Text.Trim = "" Then
            If txtTubeNm.Text.Length > txtTubeNmS.MaxLength Then
                txtTubeNmS.Text = txtTubeNm.Text.Substring(0, txtTubeNmS.MaxLength)
            Else
                txtTubeNmS.Text = txtTubeNm.Text
            End If
        End If

        If txtTubeNmD.Text.Trim = "" Then
            If txtTubeNm.Text.Length > txtTubeNmD.MaxLength Then
                txtTubeNmD.Text = txtTubeNm.Text.Substring(0, txtTubeNmD.MaxLength)
            Else
                txtTubeNmD.Text = txtTubeNm.Text
            End If
        End If

        If txtTubeNmP.Text.Trim = "" Then
            If txtTubeNm.Text.Length > txtTubeNmP.MaxLength Then
                txtTubeNmP.Text = txtTubeNm.Text.Substring(0, txtTubeNmP.MaxLength)
            Else
                txtTubeNmP.Text = txtTubeNm.Text
            End If
        End If
    End Sub

    Private Sub btnGetExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        Dim intLine As Integer = 2
        Dim strTubeCd As String = ""
        Dim strTubeNm As String = ""
        Dim strTubeNms As String = ""
        Dim strTubeNmd As String = ""
        Dim strTubeNmp As String = ""
        Dim strTubeNmbp As String = ""

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim dt As New DataTable

        Try
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open("c:\as\용기코드.xls")

            xlsWkS = CType(xlsWkB.Sheets("Sheet1"), Excel.Worksheet)

            For intLine = 2 To 70
                strTubeCd = xlsWkS.Range("B" + CStr(intLine)).Value.ToString
                strTubeNm = xlsWkS.Range("C" + CStr(intLine)).Value.ToString
                strTubeNms = xlsWkS.Range("C" + CStr(intLine)).Value.ToString
                strTubeNmd = xlsWkS.Range("C" + CStr(intLine)).Value.ToString
                strTubeNmp = "" 'xlsWkS.Range("B" + CStr(intLine)).Value.ToString
                strTubeNmbp = "" 'xlsWkS.Range("B" + CStr(intLine)).Value.ToString

                strTubeCd = strTubeCd
                dt = mobjDAF.GetTubeInfo("", "")
                Dim a_dr As DataRow()

                a_dr = dt.Select("TUBECD = '" + strTubeCd + "'")

                dt = Fn.ChangeToDataTable(a_dr)
                If dt.Rows.Count < 1 Then

                    Dim it40 As New LISAPP.ItemTableCollection
                    Dim sRegDT As String

                    sRegDT = fnGetSystemDT()

                    With it40
                        .SetItemTable("TUBECD", 1, 1, strTubeCd)
                        .SetItemTable("USDT", 2, 1, "2000-01-01 00:00:00")
                        .SetItemTable("UEDT", 3, 1, msUEDT)

                        .SetItemTable("REGDT", 4, 1, sRegDT)
                        .SetItemTable("REGID", 5, 1, USER_INFO.USRID)
                        .SetItemTable("TUBENM", 6, 1, strTubeNm)
                        .SetItemTable("TUBENMS", 7, 1, strTubeNms)
                        .SetItemTable("TUBENMD", 8, 1, strTubeNmd)
                        .SetItemTable("TUBENMP", 9, 1, strTubeNmp)
                        .SetItemTable("TUBENMBP", 10, 1, strTubeNmbp)
                        .SetItemTable("TUBEVOL", 11, 1, "")
                        .SetItemTable("TUBEUNIT", 12, 1, "")
                        .SetItemTable("TUBEIFCD", 13, 1, "")
                        .SetItemTable("REGIP", 14, 1, USER_INFO.LOCALIP)
                    End With

                    If mobjDAF.TransTubeInfo(it40, 0, strTubeCd, "2000-01-01 00:00:00", USER_INFO.USRID) Then
                    Else
                        MsgBox("등록오류")
                    End If

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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FDF06_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Dim sDir As String = Application.StartupPath + "\Image"
            Dim sFileNm = sDir + "\Tube_*.jpg"

            Threading.Thread.Sleep(1000)
            IO.File.Delete(sFileNm)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FDF06_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Private Sub txtTubeCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTubeCd.KeyDown, txtTubeNm.KeyDown, txtTubeNmBP.KeyDown, txtTubeNmD.KeyDown, txtTubeNmP.KeyDown, txtTubeNmS.KeyDown, txtIFCd.KeyDown, txtUnit.KeyDown, txtVol.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub btnFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFileOpen.Click
        Dim sFn As String = "Private Sub btnFileOpen_Click"

        Try
            Dim filedlg As New OpenFileDialog

            filedlg.Multiselect = False
            filedlg.Title = "그림 파일 불러오기"
            filedlg.Filter = "그림파일(*.bmp;*jpg;*.gif;*.tif)|*.bmp;*.jpg;*.gif;*.tif|모든파일(*.*)|*.*"

            If filedlg.ShowDialog() = DialogResult.OK Then
                If filedlg.FileName.Length > 0 Then
                    Me.txtFileNm.Text = filedlg.FileName

                    Dim bmpBuf As Bitmap = New Bitmap(filedlg.FileName)

                    Me.picBuf.Image = CType(bmpBuf, Image)

                    'Clipboard.SetDataObject(Me.picBuf.Image)

                    'Me.picBuf.Image.Dispose()
                    'Me.picBuf.Image = Nothing
                End If
            End If


        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub btnReg_img_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_img.Click

        If Me.txtFileNm.Text = "" Then
            MsgBox("가져온 이미지가 없습니다.!!")
            Return
        End If


        Dim btFile As Byte() = fnImgFile_Get(Me.txtFileNm.Text)

        If mobjDAF.TransTubeInfo_Img(Me.txtTubeCd.Text, btFile) Then

        Else
            MsgBox("이미지 저장을 실패했습니다.!!")
        End If
    End Sub

    Private Sub FDF06_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim sDir As String = Application.StartupPath + "\Image"
            Dim sFileNm = sDir + "\Tube_*.jpg"

            IO.File.Delete(sFileNm)
            Threading.Thread.Sleep(100)

        Catch ex As Exception

        End Try

    End Sub
End Class
