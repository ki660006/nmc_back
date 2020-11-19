Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Public Class LoginPopWin
    Private Const msFile As String = "File : CGLOGIN.vb, Class : LOGIN.LoginPopWin" + vbTab

    Private m_frm_LogIn As LOGIN.FGLOGIN_NMC
    Private msExecGbn As String

    Public Event EventBtnClicked(ByVal asVal As String, ByVal rsExecGbn As String)

    '인터페이스 동적 참조 추가
    Public msUSRNM As String = ""
    Public msUSRID As String = ""


    ' 로그인
    Public Sub LogInDo(ByVal r_Frm As System.Windows.Forms.Form, Optional ByVal rbFrmLock As Boolean = False)
        Dim sFn As String = "Public Sub LogInDo(System.Windows.Forms.Form, [Boolean])"

        Try
            If rbFrmLock = False Then
                msExecGbn = "LogIn"
            Else
                msExecGbn = "Lock"
            End If

            ' 열려있는 모든폼 종료
            sbFormAllClose(r_Frm, rbFrmLock)

            r_Frm.Enabled = False
            m_frm_LogIn = New LOGIN.FGLOGIN_NMC
            m_frm_LogIn.mbLock = rbFrmLock
            r_Frm.AddOwnedForm(m_frm_LogIn)

            m_frm_LogIn.Show()

            AddHandler m_frm_LogIn.EventBtnClicked, AddressOf sbEventBtnClicked

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 자동로그인 처리
    Public Function LoginDo(ByVal r_al_ParaInfo As ArrayList) As Boolean
        Dim sFn As String = "Public Function LoginDo(ArrayList) As Boolean"

        Try
            LoginDo = False

            ' 자동로그인ID 
            Select Case r_al_ParaInfo(0).ToString
                Case "WARD", "OUT", "PAT"
                    If LOGIN.CONFIG.FN.fnGetUsrInfo(r_al_ParaInfo(0).ToString.Trim) = True Then

                        ' 간호사정보 설정
                        With USER_INFO
                            .N_UID = r_al_ParaInfo(2).ToString.Trim
                            .N_UNM = r_al_ParaInfo(3).ToString.Trim

                            .N_FLG = r_al_ParaInfo(1).ToString.Trim         '-- 메뉴구분
                            .N_WARDorDEPT = r_al_ParaInfo(4).ToString.Trim      '-- WARD:병동코드, OUT:과코드, PAT:등록번호

                            '-- 등록번호
                            If r_al_ParaInfo.Count > 5 Then
                                .N_REGNO = r_al_ParaInfo(5).ToString.Trim
                            End If

                            .N_IOGBN = r_al_ParaInfo(0).ToString.Trim
                        End With

                        ' LIS에 존재 유/무에 따라 수정/등록 처리한다.
                        If LOGIN.CONFIG.FN.fnExe_NurseUser(r_al_ParaInfo) = True Then LoginDo = True
                    End If
                Case "LIS"
                    If LOGIN.CONFIG.FN.fnGetUsrInfo(r_al_ParaInfo(2).ToString) = True Then

                        ' LIS에 존재 유/무에 따라 수정/등록 처리한다.
                        If LOGIN.CONFIG.FN.fnExe_NurseUser(r_al_ParaInfo) = True Then LoginDo = True
                    End If

            End Select

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    ' 로그아웃 
    Public Sub LogOutDo(ByVal aoFrm As System.Windows.Forms.Form)
        Dim sFn As String = "Public Sub LogOutDo(System.Windows.Forms.Form)"

        Try
            msExecGbn = "LogOut"

            ' 열려있는 모든폼 종료
            sbFormAllClose(aoFrm, False)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 프로그램 Locked
    Public Sub Locking(ByVal aoFrm As System.Windows.Forms.Form)
        Dim sFn As String = ""

        Try
            LogInDo(aoFrm, True)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 열려있는 모든폼 종료
    Private Sub sbFormAllClose(ByVal r_Frm As System.Windows.Forms.Form, ByVal rbLock As Boolean)
        Dim sFn As String = "Private Sub sbFormAllClose(System.Windows.Forms.Form, Boolean)"
        Dim iMdiChildrenCnt As Integer = 0

        Try
            If rbLock = False Then
                ' Owned(기초마스터 관리) 화면 종료
                For ix As Integer = 1 To r_Frm.OwnedForms.Length
                    If r_Frm.OwnedForms(ix - 1).Text.IndexOf("기초마스터 관리") >= 0 Or r_Frm.OwnedForms(ix - 1).Text.IndexOf("검사코드 등록") >= 0 Then
                        r_Frm.OwnedForms(0).Dispose()
                    End If
                Next

                ' MdiChildren화면 종료
                iMdiChildrenCnt = r_Frm.MdiChildren.Length
                For ix As Integer = 1 To iMdiChildrenCnt
                    'r_Frm.MdiChildren(0).Dispose()
                    r_Frm.MdiChildren(0).Close()
                Next

            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    Public Sub sbEventBtnClicked(ByVal asVal As String)
        ' 인터페이스 동적 참조 추가
        msUSRNM = USER_INFO.USRNM
        msUSRID = USER_INFO.USRID

        RaiseEvent EventBtnClicked(asVal, msExecGbn)

        'm_frm_LogIn.Dispose()
    End Sub

End Class
