Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports Oracle.DataAccess.Client
Imports LISAPP.APP_R
Imports DBORA.DbProvider


Public Class FGR99
    Private msFLD As String = Convert.ToChar(124).ToString()
    Private msFS_ As String = Convert.ToChar(28).ToString()
    Private msETX As String = Convert.ToChar(3).ToString()

    Private msSOH As String = Convert.ToChar(1).ToString()
    Private msSTX As String = Convert.ToChar(2).ToString()
    'Private msETX As String = Convert.ToChar(3).ToString()
    Private msEOT As String = Convert.ToChar(4).ToString()
    Private msENQ As String = Convert.ToChar(5).ToString()
    Private msACK As String = Convert.ToChar(6).ToString()
    Private msTAB As String = Convert.ToChar(9).ToString()
    Private msLF_ As String = Convert.ToChar(10).ToString()
    Private msCR_ As String = Convert.ToChar(13).ToString()
    Private msDC1 As String = Convert.ToChar(17).ToString()
    Private msDC2 As String = Convert.ToChar(18).ToString()
    Private msDC3 As String = Convert.ToChar(19).ToString()
    Private msDC4 As String = Convert.ToChar(20).ToString()
    Private msNAK As String = Convert.ToChar(21).ToString()
    Private msSYN As String = Convert.ToChar(22).ToString()
    Private msETB As String = Convert.ToChar(23).ToString()
    'Private msFS_ As String = Convert.ToChar(28).ToString()
    Private msGS_ As String = Convert.ToChar(29).ToString()
    Private msRS_ As String = Convert.ToChar(30).ToString()
    'Private msFLD As String = Convert.ToChar(124).ToString()
    Private msMAN As String = "@MAN"

    Private m_OleDbCn As OracleConnection
    Private m_OleDbTrans As OracleTransaction

    Private Sub FGR99_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TextBox1.Text = msFLD
        Me.TextBox2.Text = msFS_
        Me.TextBox3.Text = msETX
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click


        Try
            Dim sRegStep As String = ""
            If Me.RadioButton1.Checked Then
                sRegStep = "2"
            ElseIf Me.RadioButton2.Checked Then
                sRegStep = "3"
            End If
            Dim sEqBCNo As String = "", sEqCd As String = "", sUsrID As String = "", sRow As String = ""
            Dim sIntSeqNo As String = "", sRack As String = "", sPos As String = ""
            Dim sQcFlag As String = "", sQcDay As String = "", rsQc As String = ""

            Dim sSampleInfo As String = ""
            Dim sTSvrCd As String = ""
            Dim sTRst1 As String = ""
            Dim sTRst2 As String = ""

            'sEqBCNo = a_sSampInfo(i - 1).Split(msFLD)(0)
            'sEqCd = a_sSampInfo(i - 1).Split(msFLD)(1)
            'sUsrID = a_sSampInfo(i - 1).Split(msFLD)(2)
            'sRow = a_sSampInfo(i - 1).Split(msFLD)(3)
            'sIntSeqNo = a_sSampInfo(i - 1).Split(msFLD)(4)
            'sRack = a_sSampInfo(i - 1).Split(msFLD)(5)
            'sPos = a_sSampInfo(i - 1).Split(msFLD)(6)
            'sRegStep = a_sSampInfo(i - 1).Split(msFLD)(7)
            'sQcFlag = a_sSampInfo(i - 1).Split(msFLD)(8)
            'Private msFLD As String = Convert.ToChar(124).ToString()
            'Private msFS_ As String = Convert.ToChar(28).ToString()
            'Private msETX As String = Convenrt.ToChar(3).ToString()

            If Me.chkLogMode.Checked Then
                sSampleInfo = Me.txtSamplinfo.Text.Trim
                sTSvrCd = Me.txtTest.Text.Trim
                sTRst1 = Me.txtRst1.Text.Trim
                sTRst2 = Me.txtRst2.Text.Trim
            Else
                With Me.spdList
                    Dim i As Integer = 0
                    For ix As Integer = 0 To .MaxRows - 1
                        .Row = ix + 1


                        .Col = .GetColFromID("orgrst")
                        Dim sTempRst As String = .Text
                        If sTempRst <> "" Then
                            '--rsTRst1
                            sTRst1 += .Text + Chr(124) 'FLD

                            If i = 0 Then
                                '-----SampleInfo
                                .Col = .GetColFromID("prtbcno") : sEqBCNo = .Text : sSampleInfo += sEqBCNo + Chr(124)
                                .Col = .GetColFromID("eqcd") : sEqCd = .Text : sSampleInfo += sEqCd + Chr(124)
                                sUsrID = "ACK" : sSampleInfo += sUsrID + Chr(124)
                                sRow = CStr(ix) : sSampleInfo += sRow + Chr(124)
                                sSampleInfo += sIntSeqNo + Chr(124)
                                sSampleInfo += sRack + Chr(124)
                                sPos = "" : sSampleInfo += sPos + Chr(124)
                                sSampleInfo += sRegStep + Chr(124)
                                .Col = .GetColFromID("qcflag") : sQcFlag = .Text : sSampleInfo += sQcFlag + Chr(124)
                                If rsQc <> "" Then
                                    sSampleInfo += sQcDay + Chr(124)
                                End If
                                i += 1
                            End If

                            '-------------------
                            '--rsTSvrCd
                            .Col = .GetColFromID("tclscd") : sTSvrCd += .Text + Chr(124) 'FLD
                            '-------------------
                            ''--rsTRst1
                            '.Col = .GetColFromID("orgrst") : sTRst1 += .Text + Chr(3) 'ETX
                            ''-------------------
                            '--rsTRst2
                            sTRst2 += "" + Chr(124) '+ Chr(3) 'ETX
                            '-------------------
                        End If
                    Next
                    sSampleInfo += Chr(3) 'ETX
                    sTSvrCd += Chr(3)
                    sTRst1 += Chr(3) 'ETX
                    sTRst2 += Chr(3) 'ETX

                End With
            End If



            'ByVal rsSender As String, ByVal rsSampInfo As String, ByVal rsTSvrCd As String, ByVal rsTRst1 As String, ByVal rsTRst2 As String, Optional ByVal riServer As Integer = 0
            Dim sRet As String = RegServer("", sSampleInfo, sTSvrCd, sTRst1, sTRst2, 0)

            MsgBox(sRet)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function RegServer(ByVal rsSender As String, ByVal rsSampInfo As String, ByVal rsTSvrCd As String, ByVal rsTRst1 As String, ByVal rsTRst2 As String, Optional ByVal riServer As Integer = 0) As String
        Dim sFn As String = "Function RegServer(String, String, String, String, String) As ArrayList"

        Dim sEqBCNo As String = "", sEqCd As String = "", sUsrID As String = "", sRow As String = ""
        Dim sIntSeqNo As String = "", sRack As String = "", sPos As String = "", sRegStep As String = "", sQcFlag As String = ""

        Dim sQcDay As String = ""

        Dim a_sSampInfo() As String
        Dim a_sTSvrCd() As String
        Dim a_sTRst1() As String
        Dim a_sTRst2() As String

        Dim sReturn As String = ""

        rsSampInfo = Replace(rsSampInfo, msFS_, " ")
        rsTSvrCd = Replace(rsTSvrCd, msFS_, " ")
        rsTRst1 = Replace(rsTRst1, msFS_, " ")
        rsTRst2 = Replace(rsTRst2, msFS_, " ")

        Try
            'rsSender --> Sender ID, 로그 기준명칭(\/:* 등이 안들어간 문자) 장비코드 또는 명칭 등 AAA3호기

            'rsSampInfo.Split("|"c)(0) --> 바코드#,  샘플인 경우는 바코드#, QC인 경우는 Control ID로 넘어와야 함.
            'rsSampInfo.Split("|"c)(1) --> EqCd,
            'rsSampInfo.Split("|"c)(2) --> User ID,  (=> REGID, MWID, FNID로 사용될 사용자아이디 <=)
            'rsSampInfo.Split("|"c)(3) --> "" 
            'rsSampInfo.Split("|"c)(4) --> IntSeqNo, 담당자별 결과저장 및 보고(검사장비 탭) No  에 해당. 인터페이스일련번호, 향후 장비일련번호 등으로 사용해야할 것임.
            'rsSampInfo.Split("|"c)(5) --> Rack,     담당자별 결과저장 및 보고(검사장비 탭) Rack에 해당. 장비의 Rack
            'rsSampInfo.Split("|"c)(6) --> Pos,      담당자별 결과저장 및 보고(검사장비 탭) Pos 에 해당. 장비의 Pos
            'rsSampInfo.Split("|"c)(7) --> RegStep,  없는 경우는 일단 RegStep = 3 최종보고로 처리하면 됨.
            'rsSampInfo.Split("I"c)(8) --> QcFlag,   Qc인 경우 Q로

            '< add freety 2006/11/02 : QcDay 추가
            'rsSampInfo.Split("|"c)(9) --> QcDay
            '>

            If rsSampInfo.Split(CChar(msFLD)).Length < 8 Then
                Return sReturn
            End If

            If rsTSvrCd.Split(CChar(msFLD)).Length < 1 Then
                Return sReturn
            End If

            If rsTRst1.Split(CChar(msFLD)).Length < 1 Then
                Return sReturn
            End If

            If rsTRst2.Split(CChar(msFLD)).Length < 1 Then
                Return sReturn
            End If

            'Dim sCnStr As String = msCnStr_Test

            'If UseOneDbCn Then
            '    If riServer = 0 Then
            '        sbConnectOne()
            '    Else
            '        sbConnectOne(sCnStr)
            '    End If
            'Else
            '    If riServer = 0 Then
            '        sbConnect()
            '    Else
            '        sbConnect(sCnStr)
            '    End If
            'End If

            a_sSampInfo = rsSampInfo.Split(CChar(msETX))
            a_sTSvrCd = rsTSvrCd.Split(CChar(msETX))
            a_sTRst1 = rsTRst1.Split(CChar(msETX))
            a_sTRst2 = rsTRst2.Split(CChar(msETX))

            For i As Integer = 1 To a_sSampInfo.Length
                If a_sSampInfo(i - 1) = "" Then Exit For

                Dim sBCNo As String = a_sSampInfo(i - 1).Split(CChar(msFLD))(0)

                If sBCNo.Length > 10 Then
                    sBCNo = sBCNo.Substring(0, 11)
                End If

                sEqBCNo = a_sSampInfo(i - 1).Split(CChar(msFLD))(0)
                sEqCd = a_sSampInfo(i - 1).Split(CChar(msFLD))(1)
                sUsrID = a_sSampInfo(i - 1).Split(CChar(msFLD))(2)
                sRow = a_sSampInfo(i - 1).Split(CChar(msFLD))(3)
                sIntSeqNo = a_sSampInfo(i - 1).Split(CChar(msFLD))(4)
                sRack = a_sSampInfo(i - 1).Split(CChar(msFLD))(5)
                sPos = a_sSampInfo(i - 1).Split(CChar(msFLD))(6)
                sRegStep = a_sSampInfo(i - 1).Split(CChar(msFLD))(7)
                sQcFlag = a_sSampInfo(i - 1).Split(CChar(msFLD))(8)

                '< add freety 2006/11/02 : QcDay 추가
                If a_sSampInfo(i - 1).Split(CChar(msFLD)).Length >= 10 Then
                    sQcDay = a_sSampInfo(i - 1).Split(CChar(msFLD))(9)
                End If
                '>
                '<<<20171027 커넥션이 없어서 위치 변경
                m_OleDbCn = GetDbConnection()
                m_OleDbTrans = m_OleDbCn.BeginTransaction()


                ' Dim dbCn As OracleConnection = GetDbConnection()
                'Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

                Dim sRstFlag As String = ""

                If Not sQcFlag = "Q" Then
                    '해당 샘플의 모든 결과가 이미 최종보고 되었는지 조사 --> 해당 샘플을 못찾거나 RstFlag = "3" 이면 Return 0
                    sRstFlag = fnGetBCNoAndRstFlag(sBCNo)
                End If

                If sRstFlag = "{NULL}" Or sRstFlag = "3" Then
                    If String.IsNullOrEmpty(sReturn) = False Then sReturn += msFLD
                    sReturn += "F"

                    Continue For
                End If

                Dim rstinfo As STU_RstInfo
                Dim al_rstinfo As New ArrayList

                For j As Integer = 1 To a_sTSvrCd(i - 1).Split(CChar(msFLD)).Length
                    If a_sTSvrCd(i - 1) = "" Then Exit For

                    If Not a_sTSvrCd(i - 1).Split(CChar(msFLD))(j - 1) = "" Then
                        rstinfo = New STU_RstInfo

                        rstinfo.TestCd = a_sTSvrCd(i - 1).Split(CChar(msFLD))(j - 1)
                        rstinfo.OrgRst = a_sTRst1(i - 1).Split(CChar(msFLD))(j - 1)
                        rstinfo.RstCmt = a_sTRst2(i - 1).Split(CChar(msFLD))(j - 1)

                        al_rstinfo.Add(rstinfo)
                    End If
                Next

                Dim sampinfo As New STU_SampleInfo

                With sampinfo
                    .BCNo = sBCNo
                    .EqCd = sEqCd
                    .UsrID = sUsrID
                    .IntSeqNo = sIntSeqNo
                    .Rack = sRack
                    .Pos = sPos
                    .EqBCNo = sEqBCNo
                    ' .QcDay = sQcDay
                    .SenderID = rsSender

                    '장비특징별 RegStep 재조정
                    If IsNumeric(sRegStep) = False Then
                        sRegStep = "3"
                    End If

                    If sQcFlag = "Q" Then
                        .RegStep = "2"
                    Else
                        .RegStep = fnGetNewRegStepByEq(sEqCd, sRegStep, al_rstinfo, sampinfo)
                    End If

                    If .RegStep = "0" Then
                        If String.IsNullOrEmpty(sReturn) = False Then sReturn += msFLD
                        sReturn += "M"

                        Continue For
                    End If
                End With
                '<<<20171027 커넥션이 위에 없어서 위치 위로 변경 
                'm_OleDbCn = DP01.DPLisDb.GetOleDbConnection()
                'm_OleDbTrans = m_OleDbCn.BeginTransaction()

                'Transaction Start
                'm_OleDbTrans = m_OleDbCn.BeginTransaction()

                Dim al_succ As New ArrayList

                Dim da_regrst As New LISAPP.APP_R.RegFn(m_OleDbCn, m_OleDbTrans)
                Dim iReg As Integer = 0

                If sQcFlag = "Q" Then
                    'iReg = da_regrst.RegServerQC(al_rstinfo, sampinfo, al_succ)
                Else
                    If Me.chkPoct.Checked Then
                        iReg = da_regrst.RegServer(al_rstinfo, sampinfo, al_succ, "p", Format(Now, "yyyymmdd").ToString)

                    Else
                        iReg = da_regrst.RegServer_igra(al_rstinfo, sampinfo, al_succ)
                    End If

                End If

                If iReg > 0 Then
                    m_OleDbTrans.Commit()

                    If String.IsNullOrEmpty(sReturn) = False Then sReturn += msFLD
                    sReturn += "1"
                    sReturn = sReturn + Chr(3) + CStr(iReg)
                Else
                    m_OleDbTrans.Rollback()

                    If String.IsNullOrEmpty(sReturn) = False Then sReturn += msFLD
                    sReturn += "0"
                    sReturn = sReturn + Chr(3) + CStr(iReg)
                End If
            Next

            Return sReturn

        Catch ex As Exception
            m_OleDbTrans.Rollback()

            If m_OleDbCn IsNot Nothing Then
                m_OleDbCn.Close()
                m_OleDbCn.Dispose()
                m_OleDbCn = Nothing
            End If

            If m_OleDbTrans IsNot Nothing Then
                m_OleDbTrans.Dispose()
                m_OleDbTrans = Nothing

            End If

            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)

            If String.IsNullOrEmpty(sReturn) = False Then sReturn += msFLD
            sReturn += "0"

            Return sReturn

        Finally
            ' If Not UseOneDbCn Then sbClose()

        End Try
    End Function

    Private Function fnGetBCNoAndRstFlag(ByRef rsBCNo As String) As String
        Dim sFn As String = "Function fnGetBCNoAndRstFlag(ByRef rsBCNo As String) As String"

        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim OleDbDa As OracleDataAdapter = Nothing
        Dim OleDbParam As OleDb.OleDbParameter = Nothing

        Try
            If Not rsBCNo.Length.Equals(11) Then
                Return ""
            End If

            sSql = ""
            sSql += " select bcno, rstflg"
            sSql += "   from lj010m"
            sSql += "  where bcno = fn_ack_get_bcno_normal(:bcno)"

            Dim OleDbCmd As New oracleCommand

            OleDbCmd.Connection = m_OleDbCn
            OleDbCmd.Transaction = m_OleDbTrans
            OleDbCmd.CommandType = CommandType.Text
            OleDbCmd.CommandText = sSql

            With OleDbCmd
                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBCNo
            End With

            OleDbDa = New OracleDataAdapter(OleDbCmd)

            OleDbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                rsBCNo = dt.Rows(0).Item(0).ToString
                Return dt.Rows(0).Item(1).ToString
            Else
                Return "{NULL}"
            End If



        Catch ex As Exception
            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)
            Return "{NULL}"

        End Try
    End Function

    Private Function fnGetNewRegStepByEq(ByVal rsEqCd As String, ByVal rsRegStep As String, ByVal r_al_rstinfo As ArrayList, ByVal r_sampinfo As STU_SampleInfo) As String
        Dim sFn As String = "Function fnGetNewRegStepByEq(ByVal rsEqCd As String, ByVal rsRegStep As String, ByVal r_al_rstinfo As ArrayList, ByVal r_sampinfo As DA01.SampleInfo) As String"

        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim OleDbCmd As oracleCommand
        Dim OleDbDa As OracleDataAdapter = Nothing
        Dim OleDbParam As OleDb.OleDbParameter = Nothing

        Try
            Select Case rsEqCd
                Case "004", "005"           'LH755
                    'CBC + DIFF 인데 CBC 검사만 한 경우 --> 중간보고
                    sSql = " select fn_find_diff(?) from dual"

                    OleDbCmd = New oracleCommand

                    OleDbCmd.Connection = m_OleDbCn
                    OleDbCmd.Transaction = m_OleDbTrans
                    OleDbCmd.CommandType = CommandType.Text
                    OleDbCmd.CommandText = sSql

                    With OleDbCmd
                        .Parameters.Clear()

                        '--- BEGIN Parameter ---
                        OleDbParam = New OleDb.OleDbParameter()

                        With OleDbParam
                            .OleDbType = OleDb.OleDbType.VarChar
                            .ParameterName = "bcno_01"
                            .Value = r_sampinfo.BCNo
                        End With

                        .Parameters.Add(OleDbParam)

                        OleDbParam = Nothing
                        '--- END__ Parameter ---
                    End With

                    OleDbDa = New OracleDataAdapter(OleDbCmd)

                    OleDbDa.Fill(dt)

                    Dim sDiffCd As String = ""
                    Dim bExist As Boolean = False

                    If dt.Rows.Count > 0 Then
                        sDiffCd = dt.Rows(0).Item(0).ToString
                    End If

                    If Not sDiffCd = "" Then
                        For i As Integer = 1 To r_al_rstinfo.Count
                            If CType(r_al_rstinfo(i - 1), STU_RstInfo).TestCd.IndexOf(sDiffCd) >= 0 Then
                                bExist = True

                                Exit For
                            End If
                        Next

                        If bExist = False Then
                            If rsRegStep = "3" Then
                                rsRegStep = "2"
                            End If
                        End If
                    End If

                    'Do Slide Scan, Perform Manual Diff Count, Re-Run The Sample --> 중간보고
                    For i As Integer = 1 To r_al_rstinfo.Count
                        If CType(r_al_rstinfo(i - 1), STU_RstInfo).RstCmt.ToUpper.IndexOf("Do Slide Scan".ToUpper) >= 0 _
                            Or CType(r_al_rstinfo(i - 1), STU_RstInfo).RstCmt.ToUpper.IndexOf("Perform Manual Diff Count".ToUpper) >= 0 _
                                Or CType(r_al_rstinfo(i - 1), STU_RstInfo).RstCmt.ToUpper.IndexOf("Re-Run The Sample".ToUpper) >= 0 Then
                            If rsRegStep = "3" Then
                                rsRegStep = "2"
                            End If

                            Exit For
                        End If
                    Next

                Case "006", "007"           'Miditron
                    'Microscopy 오더가 있는 경우 --> 중간보고
                    sSql = " select fn_find_microscopy(?) from dual"

                    OleDbCmd = New oracleCommand

                    OleDbCmd.Connection = m_OleDbCn
                    OleDbCmd.Transaction = m_OleDbTrans
                    OleDbCmd.CommandType = CommandType.Text
                    OleDbCmd.CommandText = sSql

                    With OleDbCmd
                        .Parameters.Clear()

                        '--- BEGIN Parameter ---
                        OleDbParam = New OleDb.OleDbParameter()

                        With OleDbParam
                            .OleDbType = OleDb.OleDbType.VarChar
                            .ParameterName = "bcno_01"
                            .Value = r_sampinfo.BCNo
                        End With

                        .Parameters.Add(OleDbParam)

                        OleDbParam = Nothing
                        '--- END__ Parameter ---
                    End With

                    OleDbDa = New OracleDataAdapter(OleDbCmd)

                    OleDbDa.Fill(dt)

                    Dim sMicroCd As String = ""

                    If dt.Rows.Count > 0 Then
                        sMicroCd = dt.Rows(0).Item(0).ToString
                    End If

                    If Not sMicroCd = "" Then
                        If rsRegStep = "3" Then
                            rsRegStep = "2"
                        End If
                    End If

                    '< yjlee 2008-07-10 명지병원 elecsys2010 의 장비코드 변경으로 인하여 수정
                    'Case "008"
                Case "044"                  'Elecsys2010
                    '> yjlee 2008-07-10 명지병원 

                    '< add yjlee 2010/04/07 ACTH검사 추가 
                    '< add freety 2007/01/25
                    Dim sCd_ACTH(5) As String '= "LS921"

                    sCd_ACTH(0) = "LS921"
                    sCd_ACTH(1) = "LS922"
                    sCd_ACTH(2) = "LS923"
                    sCd_ACTH(3) = "LS924"
                    sCd_ACTH(4) = "LS925"
                    sCd_ACTH(5) = "LS926"

                    '수동등록이 아니고 자동등록인 경우 ACTH 검사가 포함된 경우 --> 등록안함
                    If r_sampinfo.SenderID.EndsWith(msMAN) = False Then
                        For i As Integer = 1 To r_al_rstinfo.Count
                            For ii As Integer = 1 To sCd_ACTH.Length
                                If CType(r_al_rstinfo(i - 1), STU_RstInfo).TestCd = sCd_ACTH(ii - 1).ToString() Then
                                    rsRegStep = "0"

                                    Exit For
                                End If
                            Next
                        Next
                    End If
                    '> 

            End Select

            Return rsRegStep

        Catch ex As Exception
            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)

            Return rsRegStep

        End Try
    End Function

    Private Sub txtBcprtno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcprtno.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Enter Then

                'Dim obj As New TLA02.SIEMENS01
                Dim dt As DataTable
                If Me.chkRerun.Checked Then
                    dt = GetOrderInfo_test(Me.txtBcprtno.Text.Trim, "R", 0)

                Else
                    If Me.chkPoct.Checked Then
                        dt = GetOrderInfo(Me.txtBcprtno.Text.Trim, 0) ' 0 운영 , 1 개발
                    Else
                        dt = GetOrderInfo(Me.txtBcprtno.Text.Trim, 0) ' 0 운영 , 1 개발
                    End If


                End If


                If dt.Rows.Count > 0 Then
                    With Me.spdList
                        .MaxRows = dt.Rows.Count
                        For i As Integer = 0 To .MaxRows - 1
                            .Row = i + 1
                            .Col = .GetColFromID("prtbcno") : .Text = dt.Rows(i).Item("prtbcno").ToString
                            .Col = .GetColFromID("tclscd") : .Text = dt.Rows(i).Item("tclscd").ToString
                            .Col = .GetColFromID("spccd") : .Text = dt.Rows(i).Item("spccd").ToString
                            .Col = .GetColFromID("eqcd") : .Text = "012"

                        Next
                    End With
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    'Qc 바코드 번호를 bcno로 변경
    Private Function fnConvPrtBCNoToBCNo_Qc(ByVal rsBCNo As String) As String
        Dim sBCNo As String = ""

        ''2010년에 2009년 바코드 사용하는 경우
        'If Format(Now, "yyyy") < Left(Format(Now, "yyyy"), 3) & Mid(Trim(rsBCNo), 3, 1) Then
        '    sBCNo = CStr(CInt(Left(Format(Now, "yyyy"), 3)) - 1) & Mid(Trim(rsBCNo), 3, 9)
        'Else
        '    sBCNo = Left(Format(Now, "yyyy"), 3) & Mid(Trim(rsBCNo), 3, 9)
        'End If

        Return sBCNo
    End Function
    '검체번호로 검체정보가져오기(POCT) 
    Public Function GetOrderInfo_POCT(ByVal rsBcno As String, Optional ByVal riServer As Integer = 0) As DataTable
        Dim sFn As String = "Function GetOrderBegin(Optional ByVal rsCnStr As String = "") As DataTable"

        Dim sBcno As String = ""
        Dim sTemp As String = rsBcno.Substring(0, 2)
        Dim sTemp2 As String = ""

        If rsBcno.Substring(0, 2) = "99" Then
            sBcno = fnConvPrtBCNoToBCNo_Qc(rsBcno)
        ElseIf rsBcno.Length < 14 Then
            '김포우리의 경우 일반은 11자리 미생물은 12자리 임
            If rsBcno.Length = 12 Then
                sBcno = LISAPP.COMM.BcnoFn.fnFind_BcNo(rsBcno)
            Else
                sBcno = GetBCPrtToView(rsBcno, riServer)
            End If

        End If

        If sBcno = "" Then Return Nothing

        Try
            Dim dt As DataTable = GetOrderList_Bcno(sBcno)

            Return dt

        Catch ex As Exception
            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)
            Return Nothing

        End Try
    End Function
    '검체번호로 검체정보가져오기 
    Public Function GetOrderInfo(ByVal rsBcno As String, Optional ByVal riServer As Integer = 0) As DataTable
        Dim sFn As String = "Function GetOrderBegin(Optional ByVal rsCnStr As String = "") As DataTable"

        Dim sBcno As String = ""
        Dim sTemp As String = rsBcno.Substring(0, 2)
        Dim sTemp2 As String = ""

        Dim objCommDBFN As New LISAPP.APP_DB.DbFn

        If rsBcno.Substring(0, 2) = "99" Then
            sBcno = fnConvPrtBCNoToBCNo_Qc(rsBcno)
        ElseIf rsBcno.Length < 14 Then
            '김포우리의 경우 일반은 11자리 미생물은 12자리 임
            If rsBcno.Length = 12 Then
                sBcno = LISAPP.COMM.BcnoFn.fnFind_BcNo(rsBcno)
            Else
                sBcno = objCommDBFN.GetBCPrtToView(rsBcno)
            End If

        End If

        If sBcno = "" Then Return Nothing

        Try
            Dim dt As DataTable = GetOrderList_Bcno(sBcno)

            Return dt

        Catch ex As Exception
            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)
            Return Nothing

        End Try
    End Function
    '검체번호로 검체 정보 조회
    Private Function fnGetOrder_BCNO(ByVal rsBcno As String) As DataTable

        Dim OleDbCmd As New oracleCommand
        Dim OleDbDa As OracleDataAdapter = Nothing
        Dim OleDbParam As OleDb.OleDbParameter = Nothing

        m_OleDbCn = GetDbConnection()
        'm_OleDbTrans = m_OleDbCn.BeginTransaction()

        Dim dt As New DataTable
        Dim sbSql As System.Text.StringBuilder = New System.Text.StringBuilder(200)
        Try

            sbSql.AppendLine(" select FN_GET_PRTBCNO_FROM_BCNO(r10.bcno) prtbcno ")
            sbSql.AppendLine("       , r10.bcno")
            sbSql.AppendLine("       , j10.regno ")
            sbSql.AppendLine("       , j10.patnm ")
            sbSql.AppendLine("       , j10.sex ")
            sbSql.AppendLine("       , to_char(j10.age) age ")
            sbSql.AppendLine("       , r10.tclscd ")
            sbSql.AppendLine("       , f60.tnm  ")
            sbSql.AppendLine("       , r10.spccd  ")
            sbSql.AppendLine("       , f30.spcnm ")
            sbSql.AppendLine("       , j10.statgbn ")
            sbSql.AppendLine("      , j10.sectcd||j10.tsectcd sectcd ")
            sbSql.AppendLine("       , to_char(r10.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt ")
            sbSql.AppendLine("  from lr010m r10, lf060m f60 , lj010m j10 , lf030m f30 ")
            sbSql.AppendLine(" where r10.tclscd = f60.tclscd      ")
            sbSql.AppendLine("   and r10.spccd = f60.spccd     ")
            sbSql.AppendLine("   and r10.tkdt >= f60.usdt     ")
            sbSql.AppendLine("   and r10.tkdt <  f60.uedt     ")
            'sbSql.AppendLine("   and nvl(r10.rstflag, '0') = '0' ")
            'sbSql.AppendLine("   and nvl(r10.orgrst, ' ') = ' ' ")
            sbSql.AppendLine("   and trim(f60.tcdgbn) in ('S', 'P' ,'C')")
            sbSql.AppendLine("   and r10.bcno = ?     ")
            sbSql.AppendLine("   and j10.bcno = r10.bcno      ")
            sbSql.AppendLine("   and f30.spccd = r10.spccd      ")
            sbSql.AppendLine("   and f30.usdt <= r10.tkdt      ")
            sbSql.AppendLine("   and f30.uedt >= r10.tkdt     ")
            'sbSql.AppendLine("   and j10.sectcd||j10.tsectcd  in(  'A0' ,'S1')")
            'sbSql.AppendLine(" union ")
            'sbSql.AppendLine(" select '99'||substr( r.bcno,4) as prtbcno ")
            'sbSql.AppendLine("        , r.bcno , '' regno , '' patno , '' sex  , '' age ")
            'sbSql.AppendLine("        , r.examcode as tclscd ")
            'sbSql.AppendLine("        , '' as tnm , '' as spccd , '' as spcnm , '' as statgbn , '' as sectcd  ")
            'sbSql.AppendLine("        , to_char (tkdt , 'yyyy-MM-dd hh:mi:ss' ) tkdt ")
            'sbSql.AppendLine("   from QCR_REST r")
            'sbSql.AppendLine("  where bcno = ? ")
            'sbSql.AppendLine("    and nvl(orgrst , '-') = '-' ")
            'sbSql.AppendLine("    and nvl(rstflag , '0') = '0' ")


            OleDbCmd.Connection = m_OleDbCn
            OleDbCmd.CommandType = CommandType.Text
            OleDbCmd.CommandText = sbSql.ToString()

            With OleDbCmd
                .Parameters.Clear()

                '--- BEGIN Parameter ---
                OleDbParam = New OleDb.OleDbParameter()

                With OleDbParam
                    .OleDbType = OleDb.OleDbType.VarChar
                    .ParameterName = "bcno"
                    .Value = rsBcno
                End With

                .Parameters.Add(OleDbParam)

                OleDbParam = Nothing
                '--- END__ Parameter ---


                '--- BEGIN Parameter ---
                OleDbParam = New OleDb.OleDbParameter()

                With OleDbParam
                    .OleDbType = OleDb.OleDbType.VarChar
                    .ParameterName = "bcno"
                    .Value = rsBcno
                End With

                .Parameters.Add(OleDbParam)

                OleDbParam = Nothing
                '--- END__ Parameter ---
            End With

            OleDbDa = New OracleDataAdapter(OleDbCmd)

            OleDbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If m_OleDbCn IsNot Nothing Then
                m_OleDbCn.Close()
                m_OleDbCn.Dispose()
                m_OleDbCn = Nothing
            End If
        End Try

    End Function
    Public Function GetOrderList_Bcno_poct(ByVal rsBCNo As String, Optional ByVal riServer As Integer = 0) As DataTable
        Dim sFn As String = "Function GetBCPrtToView(ByVal rsBCNO As String) As String"

        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim OleDbDa As OracleDataAdapter = Nothing
        Dim OleDbParam As OleDb.OleDbParameter = Nothing

        m_OleDbCn = GetDbConnection()
        'm_OleDbTrans = m_OleDbCn.BeginTransaction()

        Dim sTable As String = ""

        sTable = "lj011m"

        Try
            sSql += "   select FN_GET_PRTBCNO_FROM_BCNO(r10.bcno) prtbcno                       " + vbCrLf
            sSql += "                   , r10.bcno                                              " + vbCrLf
            sSql += "                   , j10.regno                                             " + vbCrLf
            sSql += "                   , j10.patnm                                             " + vbCrLf
            sSql += "                   , j10.sex                                               " + vbCrLf
            sSql += "                   , to_char(j10.age) age                                  " + vbCrLf
            sSql += "                   , r10.tclscd                                            " + vbCrLf
            sSql += "                   , f60.tnm                                               " + vbCrLf
            sSql += "                   , r10.spccd                                             " + vbCrLf
            sSql += "                   , f30.spcnm                                             " + vbCrLf
            sSql += "                   , j10.statgbn                                           " + vbCrLf
            sSql += "                  , j10.sectcd||j10.tsectcd sectcd                         " + vbCrLf
            sSql += "                   , to_char(r10.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt       " + vbCrLf
            sSql += "              from " + sTable + " j11, lf060m f60 , lj010m j10 , lf030m f30        " + vbCrLf
            sSql += "             where j11.tclscd = f60.tclscd                                 " + vbCrLf
            sSql += "               and j11.spccd = f60.spccd                                   " + vbCrLf
            sSql += "               and r10.tkdt >= f60.usdt                                    " + vbCrLf
            sSql += "               and r10.tkdt <  f60.uedt                                    " + vbCrLf
            sSql += "                  and trim(f60.tcdgbn) in ('S', 'P' ,'C')                  " + vbCrLf
            sSql += "               and r10.bcno = ?                                            " + vbCrLf
            sSql += "               and j10.bcno = r10.bcno                                     " + vbCrLf
            sSql += "               and f30.spccd = r10.spccd                                   " + vbCrLf
            sSql += "               and f30.usdt <= r10.tkdt                                    " + vbCrLf
            sSql += "               and f30.uedt >= r10.tkdt                                    " + vbCrLf

            Dim OleDbCmd As New oracleCommand

            OleDbCmd.Connection = m_OleDbCn
            OleDbCmd.Transaction = m_OleDbTrans
            OleDbCmd.CommandType = CommandType.Text
            OleDbCmd.CommandText = sSql

            With OleDbCmd
                .Parameters.Clear()

                '--- BEGIN Parameter ---
                OleDbParam = New OleDb.OleDbParameter()

                With OleDbParam
                    .OleDbType = OleDb.OleDbType.VarChar
                    .ParameterName = "bcno_01"
                    .Value = rsBCNo
                End With

                .Parameters.Add(OleDbParam)

                OleDbParam = Nothing
                '--- END__ Parameter ---
            End With

            OleDbDa = New OracleDataAdapter(OleDbCmd)

            OleDbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)
            Return Nothing
        Finally
            If m_OleDbCn IsNot Nothing Then
                m_OleDbCn.Close()
                m_OleDbCn.Dispose()
                m_OleDbCn = Nothing
            End If

        End Try
    End Function

    Public Function GetOrderList_Bcno(ByVal rsBCNo As String, Optional ByVal riServer As Integer = 0) As DataTable
        Dim sFn As String = "Function GetBCPrtToView(ByVal rsBCNO As String) As String"

        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim OleDbDa As OracleDataAdapter = Nothing
        Dim OleDbParam As OleDb.OleDbParameter = Nothing

        m_OleDbCn = GetDbConnection()
        'm_OleDbTrans = m_OleDbCn.BeginTransaction()

        Dim sTable As String = ""

        If rsBCNo.Substring(8, 1) = "M" Then
            sTable = "lm010m"
        Else
            sTable = "lr010m"
        End If

        Try
            sSql += "   select fn_ack_get_bcno_prt(r10.bcno) prtbcno                       " + vbCrLf
            sSql += "                   , r10.bcno                                              " + vbCrLf
            sSql += "                   , j10.regno                                             " + vbCrLf
            sSql += "                   , j10.patnm                                             " + vbCrLf
            sSql += "                   , j10.sex                                               " + vbCrLf
            sSql += "                   , to_char(j10.age) age                                  " + vbCrLf
            sSql += "                   , r10.tclscd                                            " + vbCrLf
            sSql += "                   , f60.tnm                                               " + vbCrLf
            sSql += "                   , r10.spccd                                             " + vbCrLf
            sSql += "                   , f30.spcnm                                             " + vbCrLf
            sSql += "                   , j10.statgbn                                           " + vbCrLf
            sSql += "                  , j10.bcclscd sectcd                         " + vbCrLf
            sSql += "                   , to_date(r10.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt       " + vbCrLf
            sSql += "              from " + sTable + " r10, lf060m f60 , lj010m j10 , lf030m f30        " + vbCrLf
            sSql += "             where r10.testcd = f60.testcd                                 " + vbCrLf
            sSql += "               and r10.spccd = f60.spccd                                   " + vbCrLf
            sSql += "               and r10.tkdt >= f60.usdt                                    " + vbCrLf
            sSql += "               and r10.tkdt <  f60.uedt                                    " + vbCrLf
            sSql += "                  and trim(f60.tcdgbn) in ('S', 'P' ,'C')                  " + vbCrLf
            sSql += "               and r10.bcno = :bcno                                            " + vbCrLf
            sSql += "               and j10.bcno = r10.bcno                                     " + vbCrLf
            sSql += "               and f30.spccd = r10.spccd                                   " + vbCrLf
            sSql += "               and f30.usdt <= r10.tkdt                                    " + vbCrLf
            sSql += "               and f30.uedt >= r10.tkdt                                    " + vbCrLf

            Dim OleDbCmd As New oracleCommand

            OleDbCmd.Connection = m_OleDbCn
            OleDbCmd.Transaction = m_OleDbTrans
            OleDbCmd.CommandType = CommandType.Text
            OleDbCmd.CommandText = sSql

            With OleDbCmd
                .Parameters.Clear()
                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBCNo

            End With

            OleDbDa = New OracleDataAdapter(OleDbCmd)

            OleDbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)
            Return Nothing
        Finally
            If m_OleDbCn IsNot Nothing Then
                m_OleDbCn.Close()
                m_OleDbCn.Dispose()
                m_OleDbCn = Nothing
            End If

        End Try
    End Function

    '검체번호로 검체정보가져오기 (테스트용 재검)
    Public Function GetOrderInfo_test(ByVal rsBcno As String, ByVal rsRerun As String, Optional ByVal riServer As Integer = 0) As DataTable
        Dim sFn As String = "Function GetOrderBegin(Optional ByVal rsCnStr As String = "") As DataTable"

        Dim sBcno As String = ""
        If rsBcno.Length < 14 Then
            sBcno = GetBCPrtToView(rsBcno, riServer)
        End If

        If sBcno = "" Then Return Nothing

        '    Dim sCnStr As String = msCnStr_Test

        Try
            '< add freety 2005/03/22
            'If UseOneDbCn Then
            '    If riServer = 0 Then
            '        sbConnectOne()
            '    Else
            '        sbConnectOne(sCnStr)
            '    End If
            'Else
            '    If riServer = 0 Then
            '        sbConnect()
            '    Else
            '        sbConnect(sCnStr)
            '    End If
            'End If
            '> add freety 2005/03/22
            'Dim sFromDt As String = GetLastTkDt()
            'Dim sToDt As String = GetServerTime()

            Dim dt As DataTable = fnGetOrder_BCNO_Rerun(sBcno)

            Return dt

        Catch ex As Exception
            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)
            Return Nothing

        End Try
    End Function
    Public Function GetBCPrtToView(ByVal rsBCNo As String, Optional ByVal riServer As Integer = 0) As String
        Dim sFn As String = "Function GetBCPrtToView(ByVal rsBCNO As String) As String"

        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim OleDbDa As OracleDataAdapter = Nothing
        Dim OleDbParam As OleDb.OleDbParameter = Nothing

        m_OleDbCn = GetDbConnection()
        m_OleDbTrans = m_OleDbCn.BeginTransaction()

        Try
            sSql = " select fn_get_bcno_from_prtbcno(?) from dual"

            Dim OleDbCmd As New oracleCommand

            OleDbCmd.Connection = m_OleDbCn
            OleDbCmd.Transaction = m_OleDbTrans
            OleDbCmd.CommandType = CommandType.Text
            OleDbCmd.CommandText = sSql

            With OleDbCmd
                .Parameters.Clear()

                '--- BEGIN Parameter ---
                OleDbParam = New OleDb.OleDbParameter()

                With OleDbParam
                    .OleDbType = OleDb.OleDbType.VarChar
                    .ParameterName = "bcno_01"
                    .Value = rsBCNo
                End With

                .Parameters.Add(OleDbParam)

                OleDbParam = Nothing
                '--- END__ Parameter ---
            End With

            OleDbDa = New OracleDataAdapter(OleDbCmd)

            OleDbDa.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return ""
            End If

        Catch ex As Exception
            'IFFn.Log(m_cFile + sFn + vbCrLf + ex.Message)
            Return ""
        Finally
            If m_OleDbCn IsNot Nothing Then
                m_OleDbCn.Close()
                m_OleDbCn.Dispose()
                m_OleDbCn = Nothing
            End If

        End Try
    End Function

    '검체번호로 검체 정보 조회 (테스트용 재검)
    Private Function fnGetOrder_BCNO_Rerun(ByVal rsBcno As String) As DataTable
        Dim OleDbCmd As New oracleCommand
        Dim OleDbDa As OracleDataAdapter = Nothing
        Dim OleDbParam As OleDb.OleDbParameter = Nothing

        Dim dt As New DataTable
        Dim sbSql As System.Text.StringBuilder = New System.Text.StringBuilder(200)


        sbSql.AppendLine(" select FN_GET_PRTBCNO_FROM_BCNO(r10.bcno) prtbcno ")
        sbSql.AppendLine("       , r10.bcno")
        sbSql.AppendLine("       , j10.regno ")
        sbSql.AppendLine("       , j10.patnm ")
        sbSql.AppendLine("       , j10.sex ")
        sbSql.AppendLine("       , j10.age  ")
        sbSql.AppendLine("       , r10.tclscd ")
        sbSql.AppendLine("       , f60.tnm  ")
        sbSql.AppendLine("       , r10.spccd  ")
        sbSql.AppendLine("       , f30.spcnm ")
        sbSql.AppendLine("       , j10.statgbn ")
        sbSql.AppendLine("      , j10.sectcd||j10.tsectcd sectcd ")
        sbSql.AppendLine("       , to_char(r10.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt ")
        sbSql.AppendLine("  from lr010m r10, lf060m f60 , lj010m j10 , lf030m f30 ")
        sbSql.AppendLine(" where r10.tclscd = f60.tclscd      ")
        sbSql.AppendLine("   and r10.spccd = f60.spccd     ")
        sbSql.AppendLine("   and r10.tkdt >= f60.usdt     ")
        sbSql.AppendLine("   and r10.tkdt <  f60.uedt     ")
        sbSql.AppendLine("   and nvl(r10.rstflag, '0') < '3' ")
        ' sbSql.AppendLine("   and nvl(r10.orgrst, ' ') = ' ' ")
        sbSql.AppendLine("   and trim(f60.tcdgbn) in ('S', 'P' ,'C')")
        sbSql.AppendLine("   and r10.bcno = ?     ")
        sbSql.AppendLine("   and j10.bcno = r10.bcno      ")
        sbSql.AppendLine("   and f30.spccd = r10.spccd      ")
        sbSql.AppendLine("   and f30.usdt <= r10.tkdt      ")
        sbSql.AppendLine("   and f30.uedt >= r10.tkdt     ")
        sbSql.AppendLine("   and j10.sectcd||j10.tsectcd  in(  'A0' ,'S1')")

        OleDbCmd.Connection = m_OleDbCn
        OleDbCmd.CommandType = CommandType.Text
        OleDbCmd.CommandText = sbSql.ToString()

        With OleDbCmd
            .Parameters.Clear()

            '--- BEGIN Parameter ---
            OleDbParam = New OleDb.OleDbParameter()

            With OleDbParam
                .OleDbType = OleDb.OleDbType.VarChar
                .ParameterName = "bcno"
                .Value = rsBcno
            End With

            .Parameters.Add(OleDbParam)

            OleDbParam = Nothing
            '--- END__ Parameter ---

        End With

        OleDbDa = New OracleDataAdapter(OleDbCmd)

        OleDbDa.Fill(dt)

        Return dt
    End Function

    Private Sub RadioButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.Click, RadioButton1.Click
        If Me.RadioButton1.Checked Then
            Me.RadioButton2.Checked = False
        Else
            Me.RadioButton2.Checked = True
        End If
    End Sub
End Class