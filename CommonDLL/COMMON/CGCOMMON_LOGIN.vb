Imports COMMON.CommFN
Imports System.Windows.Forms


Namespace CommLogin

    Public Class STU_UserInfo
        Public USRID As String = ""
        Public USRNM As String = ""
        Public USRPW As String = ""
        Public USRLVL As String = ""
        Public MEDINO As String = ""    '-- 의사 면허번호
        Public OTHER As String = ""     '-- 기타 내용
        Public DRSPYN As String = ""    '-- 특진의 여부
        Public DELFLG As String = ""    '-- 사용종료 여부
        Public LOCALIP As String = ""

        '-- Auto Login 할 때 정보
        Public N_FLG As String = ""         '-- 작업구분(Collect or Result)
        Public N_IOGBN As String = ""       '-- 입외구분(W:병동, O:외래)
        Public N_UID As String = ""         '-- 사용자ID
        Public N_UNM As String = ""         '-- 사용자명
        Public N_WARDorDEPT As String = ""  '-- 병동 또는 진료과
        Public N_REGNO As String = ""       '-- 등록번호

        '-- 기간지나면 PW 바꾸기 위한...
        Public USRPW_OLD As String = ""

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub Clear()
            USRID = ""
            USRNM = ""
            USRPW = ""
            USRLVL = ""
            MEDINO = ""
            OTHER = ""
            DRSPYN = ""
            DELFLG = ""
            LOCALIP = ""

            N_FLG = ""
            N_IOGBN = ""
            N_UID = ""
            N_UNM = ""
            N_WARDorDEPT = ""
            N_REGNO = ""
        End Sub
    End Class

    Public Class STU_UserSkill
        Private Const sFile As String = "File : CGDA_LOGIN.vb, Class : DA01.DataAccess.clsUserSkill" & vbTab

        Private m_dt_Authority As New DataTable
        Private m_dt_Authority_MST As New DataTable

        Public Sub New()
            MyBase.new()
        End Sub

        Public Sub Clear()
            m_dt_Authority.Clear()
            m_dt_Authority_MST.Clear()
        End Sub

        Public WriteOnly Property SetAuthority() As DataTable
            Set(ByVal Value As DataTable)
                m_dt_Authority = New DataTable
                m_dt_Authority = Value
            End Set
        End Property

        Public WriteOnly Property Authority_MST() As DataTable
            Set(ByVal Value As DataTable)
                m_dt_Authority_MST = New DataTable
                m_dt_Authority_MST = Value
            End Set
        End Property

        ' 사용가능 기능 DataTable로 리턴
        Public Function GetAuthority() As DataTable
            Dim sFn As String = "Public Function GetAuthority() As DataTable"

            Try
                'SKLGRP, SKLCD, SKLDESC 
                Return m_dt_Authority

            Catch ex As Exception
                Fn.log(sFile & sFn, Err)

                Return New DataTable

            End Try
        End Function

        ' 해당기능 권한유/무
        Public Function Authority(ByVal rsGrp As String, ByVal riSikSeq As Integer, Optional ByRef rsDesc As String = "") As Boolean
            Dim sFn As String = "Public Function Authority(String, String, [String]) As Boolean"
            Dim sWhere As String = ""

            Authority = False

            rsDesc = ""
            Try

                sWhere += "sklgrp = '" + rsGrp + "' AND sklcd = " + riSikSeq.ToString
                Dim dr As DataRow() = m_dt_Authority.Select(sWhere)

                ' 사용가능한 기능인경우 
                If dr.LongLength > 0 Then
                    Authority = True
                    rsDesc = dr(0).Item("skldesc").ToString.Trim
                Else
                    dr = m_dt_Authority_MST.Select(sWhere)
                    If dr.LongLength > 0 Then
                        rsDesc = dr(0).Item("skldesc").ToString.Trim
                    Else
                        rsDesc = ""
                    End If
                End If

            Catch ex As Exception
                Fn.log(sFile & sFn, Err)
            End Try

        End Function
    End Class

    Public Class STU_CONST
        Private Const sFile As String = "File : COMMON.vb, Class : _CONST" + vbTab

        Private m_dt As New DataTable   'field : clsitem(0:전체, 1:검체분류, 2:검사부서), clscd, clsval, clsflg

        Public Shared CdSep1 As String = "/"
        Public Shared CdSep2 As String = "ː"

        Public Shared CalcRst_DefFmt As String = "0.000"

        Public Shared DeptCd_MC As String = "MC"

        Public Shared Key_spd_Ctrl As Integer = 2
        Public Shared Key_spd_Shift As Integer = 1

        Public Shared Len_BcNo As Integer = 15
        Public Shared Len_BcNo_Prt As Integer = 11
        Public Shared Len_BcNo_Prt_Child As Integer = 12
        Public Shared Len_BcNo_Full As Integer = 18

        Public Shared Max_BcNoSeq As Integer = 9999

        Public Shared Tab_Space As String = "".PadRight(4)

        Public WriteOnly Property Set_DataTable() As DataTable
            Set(ByVal Value As DataTable)
                m_dt = New DataTable
                m_dt = Value
            End Set
        End Property

        Public Sub New()
            MyBase.new()
        End Sub

        Public Sub Clear()
            m_dt.Clear()
        End Sub

        '-- 병원 전산오픈일시
        Public Function OPEN_DATE() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '005'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return "19000101000000"
                End If
            Catch ex As Exception
                Return "19000101000000"
            End Try
        End Function

        '-- CSM 서버 IP
        Public Function CSM_SERVER_IP() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '008'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return "127.0.0.1"
                End If
            Catch ex As Exception
                Return "127.0.0.1"
            End Try
        End Function

        '-- CSM 서버 Port
        Public Function CSM_SERVER_PORT() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '009'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return "7009"
                End If
            Catch ex As Exception
                Return "7009"
            End Try
        End Function

        '-- 검체코드 길이
        Public Function Len_SpcCd() As Integer
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '002'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return Convert.ToInt16(dr(0).Item("clsval").ToString.Trim)
                Else
                    Return 4
                End If
            Catch ex As Exception
                Return 4
            End Try
        End Function

        '-- 환자이름 길이
        Public Function Len_PatNm() As Integer
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '004'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return Convert.ToInt16(dr(0).Item("clsval").ToString.Trim)
                Else
                    Return 40
                End If
            Catch ex As Exception
                Return 40
            End Try
        End Function

        '-- 등록번호 길이
        Public Function Len_RegNo() As Integer
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '003'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return Convert.ToInt16(dr(0).Item("clsval").ToString.Trim)
                Else
                    Return 10
                End If
            Catch ex As Exception
                Return 10
            End Try
        End Function


        '-- 헌혈번호 앞자리(2)
        Public Function HOSPITAL_DONER_NO() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '013'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString
                Else
                    Return "10"
                End If
            Catch ex As Exception
                Return "10"
            End Try
        End Function

        '-- 헌혈관리 병원기관코드
        Public Function HOSPITAL_DONER_INSTCD() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '014'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString
                Else
                    Return "10"
                End If
            Catch ex As Exception
                Return "10"
            End Try
        End Function

        '-- 병동채혈에서 채혈버튼 레이블
        Public Function BUTTON_COLL_WARD() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '21' AND clscd = '001'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 병동일괄 채혈에서 채혈버튼 레이블
        Public Function BUTTON_COLL_BATCH() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '21' AND clscd = '002'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 외래채혈에서 접수처리 여부
        Public Function BUTTON_COLL_TAKEYN() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '21' AND clscd = '003'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function


        '-- 병동일괄 채혈에서 채혈버튼 레이블
        Public Function BUTTON_COLL_TAKEYN_COLDT() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '21' AND clscd = '004'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        ' 응급실 병동
        Public Function WARD_ER() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '04' AND clscd = '002'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function


        ' 후 수납 진료과
        Public Function DEPT_NOSUNAB() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '21' AND clscd = '005'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        ' 진단검사실 진료과코드
        Public Function DEPT_LAB() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '27' AND clscd = '004'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        ' 응급실 진료과코드
        Public Function DEPT_ER() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '04' AND clscd = '001'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        ' 건강검진실 진료과코드
        Public Function DEPT_HC() As ArrayList
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '06'")
                Dim alValue As New ArrayList

                If dr.Length < 1 Then Return New ArrayList

                For ix As Integer = 0 To dr.Length - 1
                    alValue.Add(dr(ix).Item("clsval").ToString.Trim)
                Next

                Return alValue
            Catch ex As Exception
                Return New ArrayList
            End Try

        End Function

        '-- 병원명
        Public Function HOSPITAL_NAME() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '001'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 병원코드
        Public Function HOSPITAL_CODE() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '015'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        Public Function SERVERIP() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '30' AND clscd = '001'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        Public Function SERVERIP_DEV() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '30' AND clscd = '003'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        Public Function SITECD() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '30' AND clscd = '002'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- SMS Server Connect String
        Public Function SMS_CONNECTSTR() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '01' AND clscd = '006'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 결과서출력에 하단 표시 문구(병원 진단검사의학과)
        Public Function Tail_WorkList() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '02' AND clscd = '001'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 결과서출력에 하단 표시 문구(병원 핵의학과)
        Public Function Tail_WorkListR() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '02' AND clscd = '005'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 당일검사 결과지는...
        Public Function Tail_RstReport() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '02' AND clscd = '002'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 본 검사실은 대한진단검사...
        Public Function Tail_RstReport2() As String
            Try

                Dim sfilenm As String = Application.ExecutablePath()

                Dim afilenm() As String = sfilenm.Split(CChar("\"))

                Dim dr As DataRow()

                sfilenm = afilenm(afilenm.Length - 1)

                If sfilenm.ToUpper = "ACK@LISO.EXE" Then
                    dr = m_dt.Select("clsitem = '02' AND clscd = '003'")
                ElseIf sfilenm.ToUpper = "ACK@RISO.EXE" Then
                    dr = m_dt.Select("clsitem = '02' AND clscd = '007'")
                End If

                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 검사실 주소
        Public Function Tail_Address() As String
            Try
                Dim sfilenm As String = Application.ExecutablePath()

                Dim afilenm() As String = sfilenm.Split(CChar("\"))

                Dim dr As DataRow()

                sfilenm = afilenm(afilenm.Length - 1)

                If sfilenm.ToUpper = "ACK@LISO.EXE" Then
                    dr = m_dt.Select("clsitem = '02' AND clscd = '004'")
                ElseIf sfilenm.ToUpper = "ACK@RISO.EXE" Then
                    dr = m_dt.Select("clsitem = '02' AND clscd = '006'")
                End If

                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 종합검증 Normal Comment 문구
        Public Function Tail_GV_NormalComment() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '27' AND clscd = '003'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 검체분류:종합검증
        Public Function BCCLS_GeneralVerify() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'A' AND clsval = '1'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clscd").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try
        End Function

        '-- 검체분류:미생물
        Public Function BCCLS_MicorBio() As ArrayList
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'A' AND clsval = '2'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    For ix As Integer = 0 To dr.Length - 1
                        alValue.Add(dr(ix).Item("clscd").ToString.Trim)
                    Next

                    Return alValue
                Else
                    Return New ArrayList
                End If
            Catch ex As Exception

                Return New ArrayList
            End Try
        End Function

        '-- 검체분류:혈액은행
        Public Function BCCLS_BloodBank() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'A' AND clsval = '3'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clscd").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try
        End Function

        '-- 검체분류:위탁검체
        Public Function BCCLS_ExLab() As ArrayList
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'A' AND clsval = '6'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    For ix As Integer = 0 To dr.Length - 1
                        alValue.Add(dr(ix).Item("clscd").ToString.Trim)
                    Next

                    Return alValue
                Else
                    Return New ArrayList
                End If
            Catch ex As Exception

                Return New ArrayList
            End Try

        End Function

        '-- 검체분류:X-Match용 검체
        Public Function BCCLS_BldCrossMatch() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'A' AND clsval = '7'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clscd").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try
        End Function

        '-- 검체분류:핵의학
        Public Function BCCLS_RIS() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'A' AND clsval = '8'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clscd").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try
        End Function

        '-- 검사부서:종합검증
        Public Function PART_GeneralVerify() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'B' AND clsval = '1'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clscd").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try
        End Function

        '-- 검사부서:미생물
        Public Function PART_MicroBio() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'B' AND clsval = '2'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clscd").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 검사부서:혈액은행
        Public Function PART_BloodBank() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'B' AND clsval = '3'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    Return dr(0).Item("clscd").ToString.Trim

                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 검사부서:폐기능
        Public Function PART_PulmonaryFunction() As String
            Return ""
        End Function

        '-- 검사부서:핵의학
        Public Function PART_RIA() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'B' AND clsval = '4'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clscd").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 검사분야:외주검사
        Public Function SLIP_ExLab() As ArrayList
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = 'C'")
                Dim alValue As New ArrayList

                If dr.Length > 0 Then
                    For ix As Integer = 0 To dr.Length - 1
                        alValue.Add(dr(ix).Item("clscd").ToString.Trim)
                    Next
                    Return alValue
                Else
                    Return New ArrayList
                End If
            Catch ex As Exception
                Return New ArrayList
            End Try

        End Function

        '-- 검사분야:ICU
        Public Function SLIP_POCT_ICU() As String
            Return ""
        End Function

        '-- 검사코드:종합검증 
        Public Function TEST_GV() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '27' AND clscd = '001'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 검체코드:종합검증
        Public Function SPC_GV() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '27' AND clscd = '002'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 추가검사코드:종합검증 
        Public Function TEST_GV_ADD() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '27' AND clscd = '006'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 처방코드:종합검증 
        Public Function TEST_GV_ORDCD() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '27' AND clscd = '005'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 처방코드:미생물 추가처방 MIC 
        Public Function TEST_MICRO_ORDCD() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '27' AND clscd = '007'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function
        '-- 처방코드:미생물 추가처방 Disk 선택 추가 20150427 허용석
        Public Function TEST_MICRO_ORDCD2() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '27' AND clscd = '008'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function
        '20150427 END

        '-- 결과등록화면에서 검체 선택 여부
        Public Function RST_BCNO_CHECK() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '28' AND clscd = '001'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 결과보고 단위(0:분야, 1:검체)
        Public Function RST_BCNO_EXE() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '28' AND clscd = '002'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 조회 : 이상자 조회에서 환자단위로 정렬
        Public Function S01_CHECKED() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '24' AND clscd = '001'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 조회 : 이상자 조회에서 환자단위로 정렬
        Public Function S01_PASS_VIEW() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '24' AND clscd = '002'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try

        End Function

        '-- 혈액출고후 경과 시간(분)
        Public Function BLD_OUT_TIME() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '29' AND clscd = '001'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return "30"
                End If
            Catch ex As Exception
                Return "30"
            End Try
        End Function

        '-- 혈액출고후 경과 시간(분)
        Public Function BLD_LABEL_SUM_YN() As String
            Try
                Dim dr As DataRow() = m_dt.Select("clsitem = '29' AND clscd = '002'")

                If dr.Length > 0 Then
                    Return dr(0).Item("clsval").ToString.Trim
                Else
                    Return "Y"
                End If
            Catch ex As Exception
                Return "Y"
            End Try
        End Function

        Public Function BCPRTNM_Transfusion() As String
            Return "Transfusion"
        End Function

        Public Function Flg_BF() As String
            Return "B"
        End Function

        Public Function Flg_ER() As String
            Return "Y"
        End Function

        Public Function Flg_Ord() As String
            Return "0"
        End Function

        Public Function Flg_BcPrt() As String
            Return "1"
        End Function

        Public Function Flg_Coll() As String
            Return "2"
        End Function

        Public Function Flg_Pass() As String
            Return "3"
        End Function

        Public Function Flg_Tk() As String
            Return "4"
        End Function

        Public Function Flg_NoRst() As String
            Return "0"
        End Function

        Public Function Flg_Rst() As String
            Return "1"
        End Function

        Public Function Flg_Mw() As String
            Return "2"
        End Function

        Public Function Flg_Fn() As String
            Return "3"
        End Function

        Public Function Flg_Regular() As String
            Return "R"
        End Function

        Public Function Flg_Add() As String
            Return "R"
        End Function

        Public Function Bank_DonorBldNo() As String
            Return "--"
        End Function

    End Class

    Public Class LOGIN
        Public Shared USER_INFO As New STU_UserInfo
        Public Shared USER_SKILL As New STU_UserSkill
        Public Shared PRG_CONST As New STU_CONST
    End Class

    Public Class STU_PRGINFO
        Public BCPRTFLG As String = "0"     '-- 병동 바코드 사용여부
        Public AUTOTKFLG As String = "0"    '-- 외래 채혈에서 자동접수 사용여부
        Public PASSFLG As String = "0"      '-- 검체전달 사용 여부
        Public TK2JUBSUFLG As String = "0"  '-- 2CK 접수 사용 여부
        Public RSTMWFLG As String = "0"     '-- 중간보고 사용 여부 
        Public RSTTNSFLG As String = "0"    '-- 출고정보 결과연동 여부 

        Public Sub New()
            MyBase.New()
        End Sub
    End Class

    Public Class PROGRAM
        Public Shared PRGINFO As New STU_PRGINFO
    End Class

    Public Class STU_AUTHORITY
        Public Shared FNReg As String = ""         ' 결과검증권한      
        Public Shared FNUpdate As String = ""      ' 최종보고 수정 권한
        Public Shared PDFNReg As String = ""       ' Panic 최종보고 권한
        Public Shared DFNReg As String = ""        ' Delta 최종보고 권한
        Public Shared CFNReg As String = ""        ' Critical 최종보고 권한
        Public Shared AFNReg As String = ""        ' Alert 최종보고 권한
        Public Shared RstUpdate As String = "1"    ' 결과 수정기능
        Public Shared UsrID As String = "ACK"      ' 로긴 아이디
        Public Shared RstClear As String = ""      ' 결과 소거기능
    End Class

End Namespace
