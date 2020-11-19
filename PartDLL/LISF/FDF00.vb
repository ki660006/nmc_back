'>>> [00] 사용자
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class FDF00
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF00.vb, Class : FDF00" + vbTab
    Private msUSDT As String = FixedVariable.gsUSDT
    Private msUEDT As String = FixedVariable.gsUEDT
    Private mchildctrlcol As New Collection
    Private miSelectKey As Integer = 0        'miSelectKey = 0, 1
    Private miAddModeKey As Integer = 0       'miAddModeKey = 0, 1, 2

    Private mobjDAF As New LISAPP.APP_F_USR
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents lblOther As System.Windows.Forms.Label
    Friend WithEvents lblUsrLvl As System.Windows.Forms.Label
    Friend WithEvents lblUsrPwdT As System.Windows.Forms.Label
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents chkDelFlg As System.Windows.Forms.CheckBox
    Friend WithEvents txtTelNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

    Private Function fnCollectItemTable_90(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_90(ByVal asRegDT As String) As LISAPP.ItemTableCollection"

        Try
            Dim it90 As New LISAPP.ItemTableCollection
            Dim iCol As Integer = 0

            With it90
                .SetItemTable("usrid", 1, 1, Me.txtUsrID.Text)
                .SetItemTable("regdt", 2, 1, rsRegDT)
                .SetItemTable("regid", 3, 1, USER_INFO.USRID)
                .SetItemTable("regip", 4, 1, USER_INFO.LOCALIP)
                .SetItemTable("usrnm", 5, 1, Me.txtUsrNm.Text)
                .SetItemTable("medino", 6, 1, Me.txtMediNo.Text)
                .SetItemTable("other", 7, 1, Me.txtOther.Text)
                '.SetItemTable("delflg", 8, 1, IIf(chkDelFlg.Checked, "1", "0").ToString)

                If chkEmptyPWD.Checked Then
                    .SetItemTable("usrpwd", 8, 1, "")
                    iCol = 8
                Else
                    iCol = 7
                End If

                .SetItemTable("usrlvl", iCol + 1, 1, Me.cboUsrLvl.SelectedItem.ToString.Substring(1, 1))
                .SetItemTable("drspyn", iCol + 2, 1, IIf(Me.chkDrSpYN.Checked, "1", "0").ToString)
                .SetItemTable("delflg", iCol + 3, 1, IIf(Me.chkDelFlg.Checked, "1", "0").ToString)


            End With
            Return it90
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function fnCollectItemTable_91(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_91() As LISAPP.ItemTableCollection"

        Try
            Dim it91 As New LISAPP.ItemTableCollection
            Dim sChk As String = ""
            Dim iCnt As Integer = 0
            Dim sMnuID_New As String = "00", sMnuLvl_New As String = "0"
            Dim iMnuLvl_New As Integer = 0
            Dim sMnuID_Old As String = "00", sMnuLvl_Old As String = "0"
            Dim iMnuLvl_Old As Integer = 0
            Dim sMnuID_Cur As String = "", sMnuLvl_Cur As String = ""
            Dim iMnuLvl_Cur As Integer = 0
            Dim sMnuID_Pre As String = "", sMnuLvl_Pre As String = ""
            Dim iMnuLvl_Pre As Integer = 0

            With spdMnu
                For i As Integer = 1 To .MaxRows
                    'CHK=1, MNUID=2, ISPARENT=3, MNULVL=4, PARENTID=5
                    .Col = 1 : .Row = i : sChk = .Text
                    .Col = 2 : .Row = i : sMnuID_Cur = .Text
                    .Col = 3 : .Row = i : Dim sIsParent_Cur As String = .Text
                    .Col = 4 : .Row = i : sMnuLvl_Cur = .Text
                    '-- .Col = 5 : .Row = i : sParentID_Cur = .Text

                    iMnuLvl_Cur = CType(sMnuLvl_Cur, Integer)

                    If sChk = "1" Then
                        iCnt += 1

                        If Not iCnt = 1 Then
                            If sMnuID_Pre.Substring(0, 2) = sMnuID_Cur.Substring(0, 2) Then
                                '같은 그룹
                                If sMnuLvl_Pre = sMnuLvl_Cur Then
                                    '같은 레벨
                                    sMnuID_New = (CType(sMnuID_Old, Integer) + 1).ToString.PadLeft(sMnuID_Old.Length, "0"c)
                                    sMnuLvl_New = sMnuLvl_Old
                                Else
                                    '다른 레벨
                                    If iMnuLvl_Pre - iMnuLvl_Cur < 0 Then
                                        '하위로 내려감
                                        sMnuID_New = sMnuID_Old & "00"
                                        sMnuLvl_New = (CType(sMnuLvl_Old, Integer) + 1).ToString
                                        'sMnuLvl_New = (sMnuID_New.Length / 2 - 1).ToString 
                                    Else
                                        '상위로 올라감
                                        Dim iCurLen As Integer = sMnuID_Old.Length - 2
                                        sMnuID_New = (CType(sMnuID_Old.Substring(0, iCurLen), Integer) + 1).ToString.PadLeft(iCurLen, "0"c)
                                        sMnuLvl_New = (CType(sMnuLvl_Old, Integer) - 1).ToString
                                    End If
                                End If
                            Else
                                '다른 그룹
                                sMnuID_New = (CType(sMnuID_Old.Substring(0, 2), Integer) + 1).ToString.PadLeft(2, "0"c)
                                sMnuLvl_New = "0"
                            End If
                        End If

                        iMnuLvl_New = CType(sMnuLvl_New, Integer)

                        it91.SetItemTable("usrid", 1, iCnt, Me.txtUsrID.Text)
                        it91.SetItemTable("mnuidnew", 2, iCnt, sMnuID_New)
                        it91.SetItemTable("isparent", 3, iCnt, sIsParent_Cur)
                        it91.SetItemTable("mnulvl", 4, iCnt, sMnuLvl_New)
                        it91.SetItemTable("mnuid", 5, iCnt, sMnuID_Cur)
                        it91.SetItemTable("regdt", 6, iCnt, rsRegDT)
                        it91.SetItemTable("regid", 7, iCnt, USER_INFO.USRID)
                        it91.SetItemTable("regip", 8, iCnt, USER_INFO.LOCALIP)

                        sMnuID_Pre = sMnuID_Cur
                        sMnuLvl_Pre = sMnuLvl_Cur
                        iMnuLvl_Pre = iMnuLvl_Cur

                        sMnuID_Old = sMnuID_New
                        sMnuLvl_Old = sMnuLvl_New
                        iMnuLvl_Old = iMnuLvl_New
                    End If
                Next
            End With

            fnCollectItemTable_91 = it91
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            fnCollectItemTable_91 = New LISAPP.ItemTableCollection
        End Try
    End Function

    Private Function fnCollectItemTable_93(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_93() As LISAPP.ItemTableCollection"

        Try
            Dim it93 As New LISAPP.ItemTableCollection
            Dim iCnt As Integer = 0

            With spdSkill
                For i As Integer = 1 To .MaxRows
                    .Col = 1 : .Row = i : Dim sChk As String = .Text
                    .Col = 2 : .Row = i : Dim sSklGrp As String = .Text
                    .Col = 3 : .Row = i : Dim sSklCD As String = .Text

                    If sChk = "1" Then
                        iCnt += 1
                        it93.SetItemTable("usrid", 1, iCnt, txtUsrID.Text)
                        it93.SetItemTable("sklgrp", 2, iCnt, sSklGrp)
                        it93.SetItemTable("sklcd", 3, iCnt, sSklCD)
                        it93.SetItemTable("regdt", 4, iCnt, rsRegDT)
                        it93.SetItemTable("regid", 5, iCnt, USER_INFO.USRID)
                        it93.SetItemTable("regip", 6, iCnt, USER_INFO.LOCALIP)
                    End If
                Next
            End With

            Return it93
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return New LISAPP.ItemTableCollection
        End Try
    End Function

    Private Function fnCollectItemTable_97(ByVal rsRegDT As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_93() As LISAPP.ItemTableCollection"

        Try
            Dim it97 As New LISAPP.ItemTableCollection

            it97.SetItemTable("usrid", 1, 1, Me.txtUsrID.Text)
            it97.SetItemTable("fldgbn", 2, 1, "1")
            it97.SetItemTable("fldval", 3, 1, Me.txtTelNo.Text)
            it97.SetItemTable("regdt", 4, 1, rsRegDT)
            it97.SetItemTable("regid", 5, 1, USER_INFO.USRID)
            it97.SetItemTable("regip", 6, 1, USER_INFO.LOCALIP)

            Return it97

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

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
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFindConflict(ByVal rsUsrID As String) As String
        Dim sFn As String = ""

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetRecentUsrInfo(rsUsrID)

            If DTable.Rows.Count > 0 Then
                Return "사용자ID(" + DTable.Rows(0).Item(0).ToString + ")는 이미 사용 중입니다." + vbCrLf + vbCrLf + _
                       "확인하여 주십시요!!"
            Else
                Return ""
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
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
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyyMMddHHmmss")
        End Try

    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnRegSpc() As Boolean"

        Try
            Dim it90 As New LISAPP.ItemTableCollection
            Dim it91 As New LISAPP.ItemTableCollection
            Dim it93 As New LISAPP.ItemTableCollection
            Dim it97 As New LISAPP.ItemTableCollection

            Dim iRegType90 As Integer = 0, iRegType91 As Integer = 0, iRegType93 As Integer = 0, iRegType97 As Integer = 0
            Dim sRegDT As String = ""

            iRegType90 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType91 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType93 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)
            iRegType97 = CType(IIf(CType(Me.Owner, FGF01).rdoWorkOpt2.Checked, 0, 1), Integer)

            sRegDT = fnGetSystemDT()

            it90 = fnCollectItemTable_90(sRegDT)
            it91 = fnCollectItemTable_91(sRegDT)
            it93 = fnCollectItemTable_93(sRegDT)
            it97 = fnCollectItemTable_97(sRegDT)

            If mobjDAF.TransUsrInfo(it90, iRegType90, it91, iRegType91, it93, iRegType93, it97, iRegType97, Me.txtUsrID.Text) Then
                fnReg = True
            Else
                fnReg = False
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Public Function fnValidate() As Boolean
        Dim sFn As String = "Private Function fnValidate() As Boolean"

        Try
            If Me.txtUsrID.Text = "" Then
                MsgBox("사용자ID를 입력하여 주십시요!!", MsgBoxStyle.Critical)
                Return False
            End If

            'If IsNumeric(Me.txtUsrID.Text.Trim) Then
            '    MsgBox("사용자ID가 숫자입니다. 확인하여 주십시요!!", MsgBoxStyle.Critical)
            '    Return False
            'End If

            If Me.cboUsrLvl.SelectedIndex = -1 Then
                MsgBox("사용자레벨을 선택하여 주십시요!!", MsgBoxStyle.Critical)
                Return False
            End If

            If Not IsNothing(Me.Owner) Then
                If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                    Dim sBuf As String = fnFindConflict(Me.txtUsrID.Text)

                    If Not sBuf = "" Then
                        MsgBox(sBuf, MsgBoxStyle.Critical)
                        Return False
                    End If

                    If Not chkEmptyPWD.Checked Then
                        MsgBox("신규 사용자 등록 시에는 항상 [사용자암호 초기화]를 선택해야 합니다!!", MsgBoxStyle.Critical)
                        Return False
                    End If
                End If
            End If

            'ErrProvider
            If Not errpd.GetError(CType(Me.Owner, FGF01).btnReg) = "" Then
                MsgBox("Error Provider : " & errpd.GetError(CType(Me.Owner, FGF01).btnReg), MsgBoxStyle.Critical)
                Return False
            End If

            Return True
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return False
        End Try
    End Function

    Public Sub sbDisplayCdDetail(ByVal rsUsrID As String)
        Dim sFn As String = ""

        Try
            miSelectKey = 1

            sbDisplayCdDetail_Usr(rsUsrID)
            sbDisplayCdDetail_Mnu(rsUsrID)
            sbDisplayCdDetail_Skill(rsUsrID)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Usr(ByVal rsUsrID As String)
        Dim sFn As String = "Private Sub sbDisplayCdDetail(ByVal asBuf As String, ByVal asTCd As String)"
        Dim iCol As Integer = 0

        Try

            Dim cctrl As System.Windows.Forms.Control
            Dim iCurIndex As Integer = -1

            Dim dt As DataTable = mobjDAF.GetUsrInfo(rsUsrID)

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

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Mnu(ByVal rsUsrID As String)
        Dim sFn As String = ""

        Try

            Dim iCol As Integer = 0, iParent As Integer = 0

            Dim dt As DataTable = mobjDAF.GetUsrMnuInfo(rsUsrID)

            If dt.Rows.Count < 1 Then Return

            With spdMnu
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim.Replace("-", " ")
                        End If

                        '색상 처리
                        If dt.Columns(j).ColumnName = "mnulvl" Then
                            iParent = CType(IIf(dt.Rows(i).Item(j).ToString = "0", 1, 0), Integer)
                        End If
                    Next

                    If iParent = 1 Then
                        .Col = .GetColFromID("mnunm") : .Row = i + 1 : .BackColor = Drawing.Color.LavenderBlush
                    Else
                        .Col = .GetColFromID("mnunm") : .Row = i + 1 : .BackColor = Drawing.Color.White
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_Skill(ByVal rsUsrID As String)
        Dim sFn As String = ""

        Try

            Dim iCol As Integer = 0, iParent As Integer = 0

            Dim dt As DataTable = mobjDAF.GetUsrSkillInfo(rsUsrID)

            If dt.Rows.Count < 0 Then Return

            With spdSkill
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If

                        '색상 처리
                        If dt.Columns(j).ColumnName = "sklcd" Then
                            iParent = CType(IIf(dt.Rows(i).Item(j).ToString = "1", 1, 0), Integer)
                        End If
                    Next

                    If iParent = 1 Then
                        .Col = .GetColFromID("mnunm") : .Row = i + 1 : .BackColor = Drawing.Color.LavenderBlush
                    Else
                        .Col = .GetColFromID("mnunm") : .Row = i + 1 : .BackColor = Drawing.Color.White
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            If USER_INFO.USRLVL = "S" Then
                btnDel.Enabled = True
            Else
                btnDel.Enabled = False
            End If

            miSelectKey = 1

            sbInitialize_ErrProvider()
            sbInitialize_Control()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    Private Sub sbInitialize_ErrProvider()
        Dim sFn As String = "sbInitializeControl_ErrProvider()"

        Try
            errpd.Dispose()
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_Control(Optional ByVal riMode As Integer = 0)
        Dim sFn As String = "Private Sub sbInitializeControl_Control(Optional ByVal iMode As Integer = 0)"

        Try
            If riMode = 0 Then
                'tpgSpc1 초기화

                Me.txtUsrID.Text = "" : Me.btnDel.Visible = False

                Me.txtUsrNm.Text = "" : Me.txtUsrPWD.Text = "" '
                Me.cboUsrLvl.SelectedIndex = -1
                Me.txtMediNo.Text = "" : Me.chkDrSpYN.Checked = False : Me.chkEmptyPWD.Checked = False
                Me.chkDelFlg.Checked = False
                Me.txtTelNo.Text = ""

                'txtUsrID0.Text = ""
                Me.txtRegDT.Text = "" : Me.txtRegID.Text = "" : Me.txtOther.Text = "" : Me.txtRegNm.Text = ""

                sbDisplayCdDetail_Mnu("")
                sbDisplayCdDetail_Skill("")

                If Not IsNothing(Me.Owner) Then
                    If CType(Me.Owner, FGF01).rdoWorkOpt2.Checked Then
                        Me.chkEmptyPWD.Checked = True
                    Else
                        Me.chkEmptyPWD.Checked = False
                    End If
                End If
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbInitialize_CtrlCollection()
        mchildctrlcol = Nothing

        mchildctrlcol = New Collection
    End Sub

    Private Sub sbInitialize_spdMnu()
        With spdMnu
            .MaxRows = 0
        End With
    End Sub

    Private Sub sbInitialize_spdSkill()
        With spdSkill
            .MaxRows = 0
        End With
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
    Friend WithEvents tpg1 As System.Windows.Forms.TabPage
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents txtUsrNm As System.Windows.Forms.TextBox
    Friend WithEvents lblUsrID As System.Windows.Forms.Label
    Friend WithEvents txtUsrID As System.Windows.Forms.TextBox
    Friend WithEvents txtUsrPWD As System.Windows.Forms.TextBox
    Friend WithEvents cboUsrLvl As System.Windows.Forms.ComboBox
    Friend WithEvents lblMedino As System.Windows.Forms.Label
    Friend WithEvents chkDrSpYN As System.Windows.Forms.CheckBox
    Friend WithEvents txtMediNo As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblMenu As System.Windows.Forms.Label
    Friend WithEvents lblSkil As System.Windows.Forms.Label
    Friend WithEvents spdMnu As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdSkill As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblUsrNm As System.Windows.Forms.Label
    Friend WithEvents chkEmptyPWD As System.Windows.Forms.CheckBox
    Friend WithEvents lblText As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF00))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tpg1 = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.grpCdInfo2 = New System.Windows.Forms.GroupBox
        Me.lblSkil = New System.Windows.Forms.Label
        Me.spdSkill = New AxFPSpreadADO.AxfpSpread
        Me.lblMenu = New System.Windows.Forms.Label
        Me.spdMnu = New AxFPSpreadADO.AxfpSpread
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.txtTelNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtMediNo = New System.Windows.Forms.TextBox
        Me.txtOther = New System.Windows.Forms.TextBox
        Me.lblMedino = New System.Windows.Forms.Label
        Me.lblOther = New System.Windows.Forms.Label
        Me.cboUsrLvl = New System.Windows.Forms.ComboBox
        Me.lblUsrLvl = New System.Windows.Forms.Label
        Me.lblUsrNm = New System.Windows.Forms.Label
        Me.txtUsrNm = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.chkDelFlg = New System.Windows.Forms.CheckBox
        Me.btnDel = New System.Windows.Forms.Button
        Me.lblUsrPwdT = New System.Windows.Forms.Label
        Me.chkEmptyPWD = New System.Windows.Forms.CheckBox
        Me.lblText = New System.Windows.Forms.Label
        Me.txtUsrPWD = New System.Windows.Forms.TextBox
        Me.chkDrSpYN = New System.Windows.Forms.CheckBox
        Me.lblUsrID = New System.Windows.Forms.Label
        Me.txtUsrID = New System.Windows.Forms.TextBox
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.tclSpc.SuspendLayout()
        Me.tpg1.SuspendLayout()
        Me.grpCdInfo2.SuspendLayout()
        CType(Me.spdSkill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdMnu, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(792, 605)
        Me.pnlTop.TabIndex = 117
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tpg1)
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
        'tpg1
        '
        Me.tpg1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpg1.Controls.Add(Me.txtRegNm)
        Me.tpg1.Controls.Add(Me.grpCdInfo2)
        Me.tpg1.Controls.Add(Me.txtRegDT)
        Me.tpg1.Controls.Add(Me.lblUserNm)
        Me.tpg1.Controls.Add(Me.lblRegDT)
        Me.tpg1.Controls.Add(Me.txtRegID)
        Me.tpg1.Controls.Add(Me.grpCdInfo1)
        Me.tpg1.Controls.Add(Me.grpCd)
        Me.tpg1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tpg1.Location = New System.Drawing.Point(4, 21)
        Me.tpg1.Name = "tpg1"
        Me.tpg1.Size = New System.Drawing.Size(780, 576)
        Me.tpg1.TabIndex = 0
        Me.tpg1.Text = "사용자기본정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(670, 544)
        Me.txtRegNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(100, 21)
        Me.txtRegNm.TabIndex = 4
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'grpCdInfo2
        '
        Me.grpCdInfo2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo2.Controls.Add(Me.lblSkil)
        Me.grpCdInfo2.Controls.Add(Me.spdSkill)
        Me.grpCdInfo2.Controls.Add(Me.lblMenu)
        Me.grpCdInfo2.Controls.Add(Me.spdMnu)
        Me.grpCdInfo2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo2.Location = New System.Drawing.Point(8, 110)
        Me.grpCdInfo2.Name = "grpCdInfo2"
        Me.grpCdInfo2.Size = New System.Drawing.Size(764, 427)
        Me.grpCdInfo2.TabIndex = 3
        Me.grpCdInfo2.TabStop = False
        Me.grpCdInfo2.Text = "사용자권한설정"
        '
        'lblSkil
        '
        Me.lblSkil.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSkil.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSkil.ForeColor = System.Drawing.Color.White
        Me.lblSkil.Location = New System.Drawing.Point(388, 23)
        Me.lblSkil.Name = "lblSkil"
        Me.lblSkil.Size = New System.Drawing.Size(368, 20)
        Me.lblSkil.TabIndex = 119
        Me.lblSkil.Text = "사용자기능설정"
        Me.lblSkil.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdSkill
        '
        Me.spdSkill.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdSkill.DataSource = Nothing
        Me.spdSkill.Location = New System.Drawing.Point(388, 46)
        Me.spdSkill.Name = "spdSkill"
        Me.spdSkill.OcxState = CType(resources.GetObject("spdSkill.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSkill.Size = New System.Drawing.Size(368, 375)
        Me.spdSkill.TabIndex = 118
        '
        'lblMenu
        '
        Me.lblMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblMenu.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMenu.ForeColor = System.Drawing.Color.White
        Me.lblMenu.Location = New System.Drawing.Point(8, 23)
        Me.lblMenu.Name = "lblMenu"
        Me.lblMenu.Size = New System.Drawing.Size(368, 20)
        Me.lblMenu.TabIndex = 117
        Me.lblMenu.Text = "사용자메뉴설정"
        Me.lblMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdMnu
        '
        Me.spdMnu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdMnu.DataSource = Nothing
        Me.spdMnu.Location = New System.Drawing.Point(8, 46)
        Me.spdMnu.Name = "spdMnu"
        Me.spdMnu.OcxState = CType(resources.GetObject("spdMnu.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdMnu.Size = New System.Drawing.Size(368, 375)
        Me.spdMnu.TabIndex = 116
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(455, 544)
        Me.txtRegDT.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 0
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(585, 544)
        Me.lblUserNm.Margin = New System.Windows.Forms.Padding(0)
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
        Me.lblRegDT.Location = New System.Drawing.Point(370, 544)
        Me.lblRegDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
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
        Me.txtRegID.Location = New System.Drawing.Point(670, 544)
        Me.txtRegID.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(100, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.txtTelNo)
        Me.grpCdInfo1.Controls.Add(Me.Label1)
        Me.grpCdInfo1.Controls.Add(Me.txtMediNo)
        Me.grpCdInfo1.Controls.Add(Me.txtOther)
        Me.grpCdInfo1.Controls.Add(Me.lblMedino)
        Me.grpCdInfo1.Controls.Add(Me.lblOther)
        Me.grpCdInfo1.Controls.Add(Me.cboUsrLvl)
        Me.grpCdInfo1.Controls.Add(Me.lblUsrLvl)
        Me.grpCdInfo1.Controls.Add(Me.lblUsrNm)
        Me.grpCdInfo1.Controls.Add(Me.txtUsrNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 59)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 44)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "사용자기본정보"
        '
        'txtTelNo
        '
        Me.txtTelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTelNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTelNo.Location = New System.Drawing.Point(554, 16)
        Me.txtTelNo.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTelNo.MaxLength = 10
        Me.txtTelNo.Name = "txtTelNo"
        Me.txtTelNo.Size = New System.Drawing.Size(85, 21)
        Me.txtTelNo.TabIndex = 12
        Me.txtTelNo.Tag = "telno"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(505, 16)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 21)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "연락처"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMediNo
        '
        Me.txtMediNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMediNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtMediNo.Location = New System.Drawing.Point(428, 16)
        Me.txtMediNo.Margin = New System.Windows.Forms.Padding(1)
        Me.txtMediNo.MaxLength = 20
        Me.txtMediNo.Name = "txtMediNo"
        Me.txtMediNo.Size = New System.Drawing.Size(70, 21)
        Me.txtMediNo.TabIndex = 7
        Me.txtMediNo.Tag = "medino"
        '
        'txtOther
        '
        Me.txtOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOther.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtOther.Location = New System.Drawing.Point(684, 16)
        Me.txtOther.Margin = New System.Windows.Forms.Padding(1)
        Me.txtOther.MaxLength = 10
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(69, 21)
        Me.txtOther.TabIndex = 8
        Me.txtOther.Tag = "other"
        '
        'lblMedino
        '
        Me.lblMedino.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblMedino.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMedino.ForeColor = System.Drawing.Color.White
        Me.lblMedino.Location = New System.Drawing.Point(366, 16)
        Me.lblMedino.Margin = New System.Windows.Forms.Padding(0)
        Me.lblMedino.Name = "lblMedino"
        Me.lblMedino.Size = New System.Drawing.Size(61, 21)
        Me.lblMedino.TabIndex = 0
        Me.lblMedino.Text = "면허번호"
        Me.lblMedino.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOther
        '
        Me.lblOther.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOther.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOther.ForeColor = System.Drawing.Color.White
        Me.lblOther.Location = New System.Drawing.Point(645, 16)
        Me.lblOther.Margin = New System.Windows.Forms.Padding(0)
        Me.lblOther.Name = "lblOther"
        Me.lblOther.Size = New System.Drawing.Size(38, 21)
        Me.lblOther.TabIndex = 11
        Me.lblOther.Text = "기타"
        Me.lblOther.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboUsrLvl
        '
        Me.cboUsrLvl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsrLvl.DropDownWidth = 130
        Me.cboUsrLvl.Items.AddRange(New Object() {"[1] 일반", "[S] 관리자", "[N] 간호사(병동)", "[R] 간호사(외래)", "[E] 간호사(진료지원)", "[W] 메뉴(병동)", "[O] 메뉴(외래)", "[P] 메뉴(진료지원)"})
        Me.cboUsrLvl.Location = New System.Drawing.Point(238, 17)
        Me.cboUsrLvl.Margin = New System.Windows.Forms.Padding(1)
        Me.cboUsrLvl.Name = "cboUsrLvl"
        Me.cboUsrLvl.Size = New System.Drawing.Size(123, 20)
        Me.cboUsrLvl.TabIndex = 6
        Me.cboUsrLvl.Tag = "usrlvl_01"
        '
        'lblUsrLvl
        '
        Me.lblUsrLvl.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUsrLvl.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUsrLvl.ForeColor = System.Drawing.Color.White
        Me.lblUsrLvl.Location = New System.Drawing.Point(167, 16)
        Me.lblUsrLvl.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUsrLvl.Name = "lblUsrLvl"
        Me.lblUsrLvl.Size = New System.Drawing.Size(70, 21)
        Me.lblUsrLvl.TabIndex = 0
        Me.lblUsrLvl.Text = "사용자레벨"
        Me.lblUsrLvl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUsrNm
        '
        Me.lblUsrNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUsrNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUsrNm.ForeColor = System.Drawing.Color.White
        Me.lblUsrNm.Location = New System.Drawing.Point(8, 16)
        Me.lblUsrNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUsrNm.Name = "lblUsrNm"
        Me.lblUsrNm.Size = New System.Drawing.Size(68, 21)
        Me.lblUsrNm.TabIndex = 0
        Me.lblUsrNm.Text = "사용자명"
        Me.lblUsrNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUsrNm
        '
        Me.txtUsrNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUsrNm.Location = New System.Drawing.Point(77, 16)
        Me.txtUsrNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtUsrNm.MaxLength = 20
        Me.txtUsrNm.Name = "txtUsrNm"
        Me.txtUsrNm.Size = New System.Drawing.Size(84, 21)
        Me.txtUsrNm.TabIndex = 5
        Me.txtUsrNm.Tag = "usrnm"
        '
        'grpCd
        '
        Me.grpCd.Controls.Add(Me.chkDelFlg)
        Me.grpCd.Controls.Add(Me.btnDel)
        Me.grpCd.Controls.Add(Me.lblUsrPwdT)
        Me.grpCd.Controls.Add(Me.chkEmptyPWD)
        Me.grpCd.Controls.Add(Me.lblText)
        Me.grpCd.Controls.Add(Me.txtUsrPWD)
        Me.grpCd.Controls.Add(Me.chkDrSpYN)
        Me.grpCd.Controls.Add(Me.lblUsrID)
        Me.grpCd.Controls.Add(Me.txtUsrID)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 9)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        Me.grpCd.Text = "사용자 ID"
        '
        'chkDelFlg
        '
        Me.chkDelFlg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkDelFlg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkDelFlg.ForeColor = System.Drawing.Color.Black
        Me.chkDelFlg.Location = New System.Drawing.Point(579, 16)
        Me.chkDelFlg.Margin = New System.Windows.Forms.Padding(1)
        Me.chkDelFlg.Name = "chkDelFlg"
        Me.chkDelFlg.Size = New System.Drawing.Size(93, 21)
        Me.chkDelFlg.TabIndex = 10
        Me.chkDelFlg.Tag = "delflg"
        Me.chkDelFlg.Text = "사용안함"
        Me.chkDelFlg.UseVisualStyleBackColor = False
        '
        'btnDel
        '
        Me.btnDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnDel.Enabled = False
        Me.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDel.ForeColor = System.Drawing.Color.White
        Me.btnDel.Location = New System.Drawing.Point(684, 13)
        Me.btnDel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(72, 27)
        Me.btnDel.TabIndex = 6
        Me.btnDel.Text = "코드삭제"
        Me.btnDel.UseVisualStyleBackColor = False
        '
        'lblUsrPwdT
        '
        Me.lblUsrPwdT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUsrPwdT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUsrPwdT.ForeColor = System.Drawing.Color.Black
        Me.lblUsrPwdT.Location = New System.Drawing.Point(188, 16)
        Me.lblUsrPwdT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUsrPwdT.Name = "lblUsrPwdT"
        Me.lblUsrPwdT.Size = New System.Drawing.Size(70, 21)
        Me.lblUsrPwdT.TabIndex = 9
        Me.lblUsrPwdT.Text = "사용자암호"
        Me.lblUsrPwdT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkEmptyPWD
        '
        Me.chkEmptyPWD.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkEmptyPWD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkEmptyPWD.ForeColor = System.Drawing.Color.Black
        Me.chkEmptyPWD.Location = New System.Drawing.Point(479, 16)
        Me.chkEmptyPWD.Margin = New System.Windows.Forms.Padding(1)
        Me.chkEmptyPWD.Name = "chkEmptyPWD"
        Me.chkEmptyPWD.Size = New System.Drawing.Size(93, 21)
        Me.chkEmptyPWD.TabIndex = 4
        Me.chkEmptyPWD.Text = "암호초기화"
        Me.chkEmptyPWD.UseVisualStyleBackColor = False
        '
        'lblText
        '
        Me.lblText.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblText.Location = New System.Drawing.Point(73, 0)
        Me.lblText.Margin = New System.Windows.Forms.Padding(0)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(186, 12)
        Me.lblText.TabIndex = 8
        Me.lblText.Text = "(영문 또는 영문+숫자 입력)"
        '
        'txtUsrPWD
        '
        Me.txtUsrPWD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrPWD.Enabled = False
        Me.txtUsrPWD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUsrPWD.Location = New System.Drawing.Point(259, 16)
        Me.txtUsrPWD.Margin = New System.Windows.Forms.Padding(1)
        Me.txtUsrPWD.MaxLength = 20
        Me.txtUsrPWD.Name = "txtUsrPWD"
        Me.txtUsrPWD.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtUsrPWD.Size = New System.Drawing.Size(123, 21)
        Me.txtUsrPWD.TabIndex = 2
        Me.txtUsrPWD.Tag = "usrpwd_vw"
        '
        'chkDrSpYN
        '
        Me.chkDrSpYN.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkDrSpYN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkDrSpYN.ForeColor = System.Drawing.Color.Black
        Me.chkDrSpYN.Location = New System.Drawing.Point(387, 16)
        Me.chkDrSpYN.Margin = New System.Windows.Forms.Padding(1)
        Me.chkDrSpYN.Name = "chkDrSpYN"
        Me.chkDrSpYN.Size = New System.Drawing.Size(90, 21)
        Me.chkDrSpYN.TabIndex = 3
        Me.chkDrSpYN.Tag = "drspyn"
        Me.chkDrSpYN.Text = "진료의여부"
        Me.chkDrSpYN.UseVisualStyleBackColor = False
        '
        'lblUsrID
        '
        Me.lblUsrID.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUsrID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUsrID.ForeColor = System.Drawing.Color.White
        Me.lblUsrID.Location = New System.Drawing.Point(7, 16)
        Me.lblUsrID.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUsrID.Name = "lblUsrID"
        Me.lblUsrID.Size = New System.Drawing.Size(68, 21)
        Me.lblUsrID.TabIndex = 0
        Me.lblUsrID.Text = "사용자ID"
        Me.lblUsrID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUsrID
        '
        Me.txtUsrID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrID.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUsrID.Location = New System.Drawing.Point(76, 16)
        Me.txtUsrID.Margin = New System.Windows.Forms.Padding(1)
        Me.txtUsrID.MaxLength = 10
        Me.txtUsrID.Name = "txtUsrID"
        Me.txtUsrID.Size = New System.Drawing.Size(110, 21)
        Me.txtUsrID.TabIndex = 1
        Me.txtUsrID.Tag = "usrid"
        '
        'FDF00
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FDF00"
        Me.Text = "[00] 사용자"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.tclSpc.ResumeLayout(False)
        Me.tpg1.ResumeLayout(False)
        Me.tpg1.PerformLayout()
        Me.grpCdInfo2.ResumeLayout(False)
        CType(Me.spdSkill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdMnu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim sFn As String = "Private Sub btnDel_Click"

        If Me.txtUsrID.Text = "" Then Return

        Try
            Dim sMsg As String = ""

            sMsg = ""
            sMsg += Me.lblUsrID.Text + " : " + Me.txtUsrID.Text + vbCrLf
            sMsg += Me.lblUsrNm.Text + " : " + Me.txtUsrNm.Text + vbCrLf + vbCrLf
            sMsg += "의 " + "코드를 삭제 하시겠습니까?" + vbCrLf + vbCrLf + vbCrLf
            sMsg += ">>> " + Me.btnDel.Text + "는 주의를 요하는 작업이므로 신중히 실행하시기 바랍니다!!" + vbTab + vbCrLf

            If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo, Me.btnDel.Text + " 확인") = MsgBoxResult.No Then Return

            Dim bReturn As Boolean = mobjDAF.TransUsrInfo_DEL(Me.txtUsrID.Text)

            If bReturn Then
                MsgBox("해당 코드가 삭제되었습니다!!", MsgBoxStyle.Information)

                CType(Me.Owner, FGF01).sbRefreshCdList()
            Else
                MsgBox("해당 코드 삭제에 실패하였습니다!!", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub spdMnu_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdMnu.ButtonClicked
        Dim sFn As String = "spdMnu_ButtonClicked"

        If miSelectKey = 1 Then Return

        Try
            With spdMnu
                If e.col = 1 Then
                    'CHK=1, MNUID=2, ISPARENT=3, MNULVL=4, PARENTID=5
                    .Col = 1 : .Row = e.row : Dim sChk As String = .Text
                    .Col = 2 : .Row = e.row : Dim sMnuID As String = .Text
                    .Col = 3 : .Row = e.row : Dim sIsParent As String = .Text
                    .Col = 4 : .Row = e.row : Dim sMnuLvl As String = .Text
                    .Col = 5 : .Row = e.row : Dim sParentID As String = .Text

                    miSelectKey = 1

                    Select Case sChk
                        Case "0"
                            Select Case sIsParent
                                Case "0"
                                    '### 자식 Uncheck !!
                                    '       자식이 모두 Uncheck시 부모 Uncheck
                                    Dim iChkSum As Integer = 0

                                    '아래쪽으로 체크
                                    For i As Integer = e.row + 1 To .MaxRows
                                        .Col = 4 : .Row = i : Dim sMnuLvl2 As String = .Text

                                        If sMnuLvl = sMnuLvl2 Then
                                            .Col = 1 : .Row = i : Dim sChk2 As String = .Text

                                            If sChk2 = "1" Then iChkSum += 1
                                        Else
                                            Exit For
                                        End If
                                    Next

                                    '위쪽으로 체크
                                    For i As Integer = e.row - 1 To 1 Step -1
                                        .Col = 4 : .Row = i : Dim sMnuLvl2 As String = .Text

                                        If sMnuLvl = sMnuLvl2 Then
                                            .Col = 1 : .Row = i : Dim sChk2 As String = .Text

                                            If sChk2 = "1" Then iChkSum += 1
                                        Else
                                            .Col = 2 : .Row = i : Dim sMnuID2 As String = .Text

                                            If sMnuID2 = sParentID And iChkSum = 0 Then
                                                .Col = 1 : .Row = i : .Text = "0"

                                                Exit For
                                            End If
                                        End If
                                    Next

                                    'If iChkSum = 0 Then
                                    '    For i As Integer = e.row - 1 To 1 Step -1
                                    '        .Col = 2 : .Row = i : Dim sMnuID2 As String = .Text

                                    '        If sMnuID2 = sParentID Then
                                    '            .Col = 1 : .Row = i : .Text = "0"

                                    '            Exit For
                                    '        End If
                                    '    Next
                                    'End If
                                Case "1"
                                    '### 부모 Uncheck !!
                                    '       자식 메뉴 All Uncheck
                                    For i As Integer = e.row + 1 To .MaxRows
                                        .Col = 4 : .Row = i : Dim sMnuLvl2 As String = .Text

                                        If sMnuLvl = sMnuLvl2 Then
                                            Exit For
                                        Else
                                            .Col = 1 : .Row = i : .Text = "0"
                                        End If
                                    Next
                            End Select
                        Case "1"
                            Select Case sIsParent
                                Case "0"
                                    '### 자식 Check !!
                                    '       부모 Uncheck시 Check 
                                    Dim iChkSum As Integer = 0

                                    '위쪽으로 체크
                                    For i As Integer = e.row - 1 To 1 Step -1
                                        .Col = 4 : .Row = i : Dim sMnuLvl2 As String = .Text

                                        If sMnuLvl = sMnuLvl2 Then
                                            .Col = 1 : .Row = i : Dim sChk2 As String = .Text

                                            If sChk2 = "1" Then iChkSum += 1
                                        Else
                                            .Col = 2 : .Row = i : Dim sMnuID2 As String = .Text

                                            If sMnuID2 = sParentID Then
                                                .Col = 1 : .Row = i : .Text = "1"

                                                Exit For
                                            End If
                                        End If
                                    Next
                                Case "1"
                                    '### 부모 Check !!
                                    '       자식 메뉴 All Check
                                    For i As Integer = e.row + 1 To .MaxRows
                                        .Col = 4 : .Row = i : Dim sMnuLvl2 As String = .Text

                                        If sMnuLvl = sMnuLvl2 Then
                                            Exit For
                                        Else
                                            .Col = 1 : .Row = i : .Text = "1"
                                        End If
                                    Next
                            End Select
                    End Select
                End If
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            miSelectKey = 0
        End Try
    End Sub

    '< add freety 2007/07/20 : 전체 선택 기능 추가
    Private Sub spdMnuSkill_BlockSelected(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BlockSelectedEvent) Handles spdMnu.BlockSelected, spdSkill.BlockSelected
        Dim sFn As String = "spdMnuSkill_BlockSelected"

        If e.blockCol <> e.blockCol2 Then Return
        If e.blockRow <> e.blockRow2 Then Return

        Try
            Dim iRow As Integer = 0

            Dim spd As AxFPSpreadADO.AxfpSpread = Nothing

            If CType(sender, AxFPSpreadADO.AxfpSpread).Name.ToUpper.EndsWith("MNU") Then
                spd = Me.spdMnu
            Else
                spd = Me.spdSkill
            End If

            If e.blockCol <> spd.GetColFromID("CHK") Then Return

            With spd
                iRow = .SearchCol(e.blockCol, 0, .MaxRows, "1", FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
            End With

            miSelectKey = 1

            With spd
                .Col = e.blockCol : .Col2 = e.blockCol
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True

                If iRow < 1 Then
                    .Text = "1"
                Else
                    .Text = ""
                End If

                .BlockMode = False
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub FDF00_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                CType(Me.Owner, FGF01).btnReg_ButtonClick(Nothing, Nothing)
            Case Windows.Forms.Keys.F6
                CType(Me.Owner, FGF01).btnClear_ButtonClick(Nothing, Nothing)
        End Select
    End Sub

    Private Sub txtUsrID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsrID.KeyDown, txtUsrPWD.KeyDown, txtUsrNm.KeyDown, txtMediNo.KeyDown, txtOther.KeyDown, cboUsrLvl.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub cboUsrLvl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUsrLvl.SelectedIndexChanged
        Try
            Dim sLevel As String = Ctrl.Get_Code(cboUsrLvl)
            If sLevel = "" Then Return

            sLevel = "LEVEL_" + sLevel

            sbDisplayCdDetail_Mnu(sLevel)
            sbDisplayCdDetail_Skill(sLevel)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try
    End Sub
End Class
