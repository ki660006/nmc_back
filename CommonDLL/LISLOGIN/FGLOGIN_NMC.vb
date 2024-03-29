﻿Imports System.Windows.Forms
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports LOGIN.CONFIG.FN

Public Class FGLOGIN_NMC
    Private Const msFile As String = "File : FGLOGIN01.vb, Class : LOGIN01" & vbTab
    Public mbLock As Boolean = False

    Private msPreUsrId As String = ""
    Private msXmlDir As String = Application.StartupPath & "\XML"

    Public Event EventBtnClicked(ByVal asVal As String)

    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"

        Try
            Me.Tag = "Load"

            ' 화면 정리
            sbFormClear()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    ' 화면정리
    Private Sub sbFormClear()
        Dim sFn As String = "Private Sub sbFormClear()"

        Try
            Me.txtUsrID.Text = ""
            Me.txtUsrPW.Text = ""

            Me.lblLoginMsg.Text = ""

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try


    End Sub

    ' ID 로드
    Private Sub sbLoadID()
        Dim sDir As String = msXmlDir
        Dim sFile As String = sDir + "\LOGIN01_USRINFO.XML"

        If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

        If Dir(sFile) <> "" Then
            Dim XMLReader As Xml.XmlTextReader = New Xml.XmlTextReader(sFile)
            With XMLReader
                .ReadStartElement("ROOT")
                Me.txtUsrID.Text = .ReadElementString("USRID")
                .ReadEndElement()
                .Close()
            End With
        End If

    End Sub

    ' ID 저장
    Private Sub sbSaveID(ByVal abSave As Boolean)
        Dim sDir As String = msXmlDir
        Dim sFile As String = sDir + "\LOGIN01_USRINFO.XML"

        If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

        If abSave Then
            Dim XMLWriter As Xml.XmlTextWriter = New Xml.XmlTextWriter(sFile, System.Text.Encoding.GetEncoding("EUC-KR"))
            With XMLWriter
                .Formatting = Xml.Formatting.Indented
                .WriteStartDocument(False)
                .WriteStartElement("ROOT")
                .WriteElementString("USRID", Me.txtUsrID.Text)
                .WriteEndElement()
                .Close()
            End With
        Else

        End If


    End Sub

    ' 데이타 유효성 체크
    Private Function fnValidation() As Boolean
        Dim sFn As String = "Private Function fnValidation() As Boolean"

        Try
            If Me.txtUsrID.Text = "" Then
                MsgBox("아이디를 입력해 주십시오.!!", MsgBoxStyle.Information, "사용자 로그인")
                Me.txtUsrID.Focus()
                Return False
            End If

            If Me.txtUsrID.Text.Length = 1 Then
                MsgBox("사용할 수 없는 아이디 입니다.  확인하세요.!!", MsgBoxStyle.Information, "사용자 로그인")
                Me.txtUsrID.Focus()
                Return False
            End If

            If Me.txtUsrPW.Text = "" Then
                MsgBox("비밀번호를 입력해 주십시오", MsgBoxStyle.Information, "사용자 로그인")
                Me.txtUsrPW.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return False
        End Try

    End Function

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        sbFormInitialize()

    End Sub

    Private Sub FGLOGIN_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If CType(Me.Tag, String) = "Load" Then
            If Me.txtUsrID.Text <> "" Then
                Me.txtUsrPW.Focus()
            End If
            Me.Tag = ""
        End If

    End Sub

    Private Sub FGLOGIN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape And btnCancel.Visible = True Then
            Me.btnCancel_Click(Nothing, Nothing)
        ElseIf e.Control = True And e.KeyCode = Keys.F11 Then
            Me.cboChange_srv.Visible = True
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RaiseEvent EventBtnClicked("")
        'Me.Close()
    End Sub

    Private Sub FGLOGIN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sFn As String = "Private Sub FGLOGIN01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load"

        Try
            If mbLock = True Then
                Me.txtUsrID.Text = USER_INFO.USRID
                msPreUsrId = txtUsrID.Text

                Me.Text = "Lock"
                Me.picTitle.Image = imlTitle.Images(1)
                Me.btnFrmMinimize.Visible = True

                Me.lblLoginMsg.Text = "※ 이 프로그램은 사용중이며, 잠겨있습니다." + vbCrLf + _
                                       "　 " + USER_INFO.USRID & "(" + USER_INFO.USRNM + ") 또는 관리자만이 이 프로그램 잠금을 해제 할 수 있습니다. "

            Else
                Me.Text = "LogIn"
                Me.picTitle.Image = imlTitle.Images(0)

                ' 이전에 로그인정보 로드
                sbLoadID()

            End If

            Me.lblServer.Text = (New COMMON.CommDb.Info).GetConnStr.DESCRIPTION

        Catch ex As Exception
            Fn.Log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub btnFrmMinimize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFrmMinimize.Click
        COMMON.CommFN.MdiMain.Frm.WindowState = FormWindowState.Minimized
        COMMON.CommFN.MdiMain.Frm.Enabled = True
        Me.Tag = "Load"

    End Sub

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim sFn As String = "Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click"

        Try
            If fnValidation() = False Then Exit Sub

            If LOGIN.CONFIG.FN.fnGetUsrInfo(Me.txtUsrID.Text) Then
                If LOGIN.CONFIG.FN.fnGet_UsrPwd(Me.txtUsrID.Text, Me.txtUsrPW.Text) Or _
                   USER_INFO.DELFLG = "1" Or _
                   (USER_INFO.USRLVL = "W" Or USER_INFO.USRLVL = "N" Or USER_INFO.USRLVL = "E" Or USER_INFO.USRLVL = "R") Then

                    If USER_INFO.DELFLG = "1" Then
                        ' 사용종료 DELFLG 
                        MsgBox(USER_INFO.USRID & "은(는) 사용종료된 아이디입니다." & vbCrLf & vbCrLf & "다른 아이디로 로그인해 주십시오.", MsgBoxStyle.Information, Me.Text)
                        Me.txtUsrPW.Text = ""
                        Me.txtUsrID.Text = ""
                        Me.txtUsrID.Focus()

                    ElseIf USER_INFO.USRLVL = "W" Or USER_INFO.USRLVL = "N" Or _
                           USER_INFO.USRLVL = "O" Or USER_INFO.USRLVL = "R" Or _
                           USER_INFO.USRLVL = "P" Then
                        MsgBox("간호사 아이디는 로그인이 불가능합니다." & vbCrLf & vbCrLf & "다른 아이디로 로그인해 주십시오.", MsgBoxStyle.Information, Me.Text)
                        Me.txtUsrPW.Text = ""
                        Me.txtUsrID.Text = ""
                        Me.txtUsrID.Focus()

                    Else
                        If mbLock = False Or (mbLock = True And msPreUsrId = USER_INFO.USRID) Then
                            sbSaveID(chkSaveID.Checked)

                            RaiseEvent EventBtnClicked("Ok")
                            Me.Close()

                        Else
                            MsgBox(lblLoginMsg.Text, MsgBoxStyle.Information, "사용자 로그인")
                            Me.txtUsrID.Text = msPreUsrId
                            Me.txtUsrPW.Text = ""
                            Me.txtUsrPW.Focus()

                        End If

                    End If

                ElseIf USER_INFO.USRID <> "" And USER_INFO.USRPW = "" And mbLock = False Then
                    ' 신규ID 또는 비밀번호 Null인경우 ( 비밀번호 확인 )  
                    If USER_INFO.USRPW_OLD <> "" Then
                        MsgBox("새로운 비밀번호를 입력해 주세요.!!", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "비밀번호 변경")
                    End If

                    Dim objFrm As New FGLOGIN_S01
                    Dim sNewPw As String
                    With objFrm
                        .msUID = Me.txtUsrID.Text
                        .msUPW = IIf(USER_INFO.USRPW_OLD <> "", "", Trim(Me.txtUsrPW.Text)).ToString
                        .ShowDialog(Me)
                        sNewPw = .msUPW
                    End With
                    objFrm.Dispose()

                    If sNewPw <> "" Then
                        ' 새로운 비밀번호 생성됨
                        Me.txtUsrPW.Text = sNewPw
                        Me.btnLogin_Click(Nothing, Nothing)
                    Else
                        ' 새로운 비밀번호 취소
                        Me.txtUsrPW.Text = ""
                        Me.txtUsrPW.Focus()
                    End If
                Else
                    MsgBox("비밀번호가 일치하지 않습니다.", MsgBoxStyle.Information, "사용자 로그인")
                    Me.txtUsrPW.Focus()
                    Me.txtUsrPW.SelectAll()

                End If

            Else
                MsgBox("존재하지 않는 아이디입니다.", MsgBoxStyle.Information, "사용자 로그인")
                Me.txtUsrID.Focus()
                Me.txtUsrID.SelectAll()

            End If

        Catch ex As Exception
            Fn.Log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub txtUsrID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsrID.GotFocus, txtUsrPW.GotFocus
        CType(sender, Windows.Forms.TextBox).SelectAll()
    End Sub

    Private Sub txtUsrID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsrID.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Me.txtUsrPW.Focus()

    End Sub

    Private Sub txtUsrPW_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsrPW.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        btnLogin_Click(Nothing, Nothing)

    End Sub

    Private Sub cboChange_srv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboChange_srv.SelectedIndexChanged
        If Me.cboChange_srv.Text = "" Then Return

        Dim stuCStr As COMMON.CommDb.STU_CONNSTR

        stuCStr = (New COMMON.CommDb.Info).GetConnStr

        With stuCStr
            .USEDP = "2"
            .PROVIDER = stuCStr.PROVIDER       'SQLOLEDB, MSDAORA
            .CATEGORY = stuCStr.CATEGORY

            .USERID = "lisif"
            .PASSWORD = "lisif"

            If Me.cboChange_srv.Text.StartsWith("[1]") Then
                '-- 운영서버(1)
                .DATASOURCE = "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST = 10.95.21.143)(PORT=1521))(ADDRESS=(PROTOCOL=TCP)(HOST=10.95.21.144)(PORT=1521))(LOAD_BALANCE=NO)(CONNECT_DATA=(SERVER= DEDICATED)(SERVICE_NAME=EMRDB)(FAILOVER_MODE=(TYPE=SELECT)(METHOD=BASIC)(RETRIES=180)(DELAY=5))))"
                .DESCRIPTION = "PROD_EMRDB1"
                If Not MdiMain.Frm Is Nothing Then
                    MdiMain.Frm.Text.Replace("NMC", "PROD_EMRDB1").Replace("EMRDB_DEV", "PROD_EMRDB1").Replace("MIGDB", "PROD_EMRDB1").Replace("PROD_EMRDB2", "PROD_EMRDB1")
                End If
            ElseIf Me.cboChange_srv.Text.StartsWith("[2]") Then
                '-- 운영서버(2)
                .DATASOURCE = "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.95.21.141)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = EMRDB)))"
                .DESCRIPTION = "PROD_EMRDB2"

                If Not MdiMain.Frm Is Nothing Then
                    MdiMain.Frm.Text.Replace("NMC", "PROD_EMRDB2").Replace("EMRDB_DEV", "PROD_EMRDB2").Replace("MIGDB", "PROD_EMRDB2").Replace("PROD_EMRDB1", "PROD_EMRDB2")
                End If
            ElseIf Me.cboChange_srv.Text.StartsWith("[3]") Then
                '-- 개발서버
                .DATASOURCE = "(DESCRIPTION =(ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.95.21.201)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = EMRDB)))"
                .DESCRIPTION = "EMRDB_DEV"

                If Not MdiMain.Frm Is Nothing Then
                    MdiMain.Frm.Text.Replace("NMC", "EMRDB_DEV").Replace("MIGDB", "EMRDB_DEV").Replace("PROD_EMRDB2", "EMRDB_DEV").Replace("PROD_EMRDB1", "EMRDB_DEV")
                End If
            ElseIf Me.cboChange_srv.Text.StartsWith("[4]") Then
                '-- 교육서버
                .DATASOURCE = "(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.95.21.107)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = EMRDB)))" '201310 162->107로 변경됨
                .DESCRIPTION = "MIG_11G"

                If Not MdiMain.Frm Is Nothing Then
                    MdiMain.Frm.Text.Replace("NMC", "MIGDB").Replace("EMRDB_DEV", "MIGDB").Replace("PROD_EMRDB2", "MIGDB").Replace("PROD_EMRDB1", "MIGDB")
                End If

            Else
                '-- 회사서버
                .DATASOURCE = "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 14.35.234.249)(PORT = 1521)) ) (CONNECT_DATA =(SERVICE_NAME = NMC)))"
                .DESCRIPTION = "NMC"

                If Not MdiMain.Frm Is Nothing Then
                    MdiMain.Frm.Text.Replace("EMRDB_DEV", "NMC").Replace("MIGDB ", "NMC").Replace("PROD_EMRDB2", "NMC").Replace("PROD_EMRDB1", "NMC")
                End If

                .USERID = "oras1"
                .PASSWORD = "oras1"
            End If

        End With

        If (New COMMON.CommDb.Info).SetConnStr(stuCStr) = True Then
            Me.lblServer.Text = stuCStr.DESCRIPTION
            Try
                FileCopy(Application.StartupPath + "\XML\DBSERVER.XML", Application.StartupPath + "\DEP\XML\DBSERVER.XML")
            Catch ex As Exception

            End Try
        End If

        If Me.txtUsrID.Text = "" Then Me.txtUsrID.Focus() Else Me.txtUsrPW.Focus()

    End Sub

End Class