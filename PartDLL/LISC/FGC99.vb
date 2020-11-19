Imports COMMON
Imports COMMON.CommFN
Imports System.Runtime
Imports System.Runtime.InteropServices
Public Class FGC99

    'Declare Ansi Function PDCheck Lib "eirspdc.dll" Alias "PDCheck" (ByVal JuminNo As String, ByVal PatNm As String, ByVal MprscIssueAdmin As String, ByVal PrscAdminName As String, ByVal PrscPresDt As String, ByVal AppIssueAdmin As String, ByVal AppIssueCode As String) As String


    Private Sub sb_dll_Load()
        Dim sFn As String = "sb_dll_Load"

        Dim invas_buf As New InvAs
        Dim sRet As String = ""

        Try
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath _
                              + "\eirspdc.dll", "PDCheck")

                '.SetProperty("UserID", USER_INFO.USRID)

                Dim a_objParam() As Object
                ReDim a_objParam(4)

                If Me.txtIdno.Text.Trim = "" Then Return
                If Me.txtPatnm.Text.Trim = "" Then Return

                a_objParam(0) = Me.txtIdno.Text
                a_objParam(1) = Me.txtPatnm.Text
                a_objParam(2) = Me.txtAdminCd.Text
                a_objParam(3) = Me.txtAdminNm.Text
                a_objParam(4) = Me.txtOrddt.Text
                a_objParam(5) = Me.txtSwCode.Text
                a_objParam(6) = Me.txtSwCfmCd.Text

                sRet = CType(.InvokeMember("PDCheck", a_objParam), String)

                Me.txtRetVal.Text = sRet

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sb_dll_Load_2()
        Try
            Dim asmb_Order As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom("D:\Works\NMC_NEW\LIS\ACK@LISo\MainExe\Main\bin\eirspdc.dll")

            Dim type_Order As Type = asmb_Order.GetType("TLA02.SIEMENS01")
            Dim objBuf As Object = Nothing

            Dim a_objParam() As Object = Nothing
            Dim fi As System.Reflection.FieldInfo = Nothing
            Dim sMemberNm As String = ""
            Dim objReturn As Object = Nothing

            ReDim a_objParam(0)

            If Me.txtIdno.Text.Trim = "" Then Return
            If Me.txtPatnm.Text.Trim = "" Then Return

            a_objParam(0) = Me.txtIdno.Text
            a_objParam(1) = Me.txtPatnm.Text
            a_objParam(2) = Me.txtAdminCd.Text
            a_objParam(3) = Me.txtAdminNm.Text
            a_objParam(4) = Me.txtOrddt.Text
            a_objParam(5) = Me.txtSwCode.Text
            a_objParam(6) = Me.txtSwCfmCd.Text

            sMemberNm = "PDCheck"

            If (objBuf Is Nothing) Then objBuf = Activator.CreateInstance(type_Order)

            objReturn = type_Order.InvokeMember(sMemberNm, System.Reflection.BindingFlags.InvokeMethod, Nothing, objBuf, a_objParam)

            Dim sRet As String = CStr(objReturn)

        Catch ex As Exception
            MsgBox (ex.Message )
        End Try

    End Sub

    Private Sub sb_dll_Load_3()
        Try
            Dim a_objParam() As Object = Nothing

            Dim sRetv As String = ""
            Dim pJuminNo As String = "8001011212121"                 ' //수진자 주민등록번호
            Dim pPatNm As String = "박정언"                          ' //수진자 이름
            Dim pMprscIssueAdmin As String = "11101318"              '  //요양기관 코드
            Dim pPrscAdminName As String = "국립중앙의료원"          ' //요양기관 명칭
            Dim pPrscPresDt As String = "20180102"                   ' //기준일자(점검일자(처방일자))
            Dim pAppIssueAdmin As String = "11101318"                ' //청구SW업체코드 (요양기관코드)
            Dim pAppIssueCode As String = "D09278512011202412083065720112" ' //청구SW인증코드 (병원문의 DUR 개발서버 전송시 : 요양기관번호 + '0000000000000000000000')
            'Dim pAppIssueCode As String = "111013180000000000000000000000" ' //청구SW인증코드 (병원문의 DUR 개발서버 전송시 : 요양기관번호 + '0000000000000000000000')

            If pJuminNo = "" Then Return
            If pPatNm = "" Then Return

            sRetv = PDCheck(pJuminNo, pPatNm, pMprscIssueAdmin, pPrscAdminName, pPrscPresDt, pAppIssueAdmin, pAppIssueCode)

            sRetv = sRetv.Replace(Chr(9), "/") + Chr(9)

            Me.txtRetVal.Text = sRetv

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    <DllImport("eirspdc.dll", SetLastError:=True, _
    CharSet:=CharSet.Ansi, ExactSpelling:=True, _
    CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function PDCheck(ByVal JuminNo As String, ByVal PatNm As String, ByVal MprscIssueAdmin As String, ByVal PrscAdminName As String, ByVal PrscPresDt As String, ByVal AppIssueAdmin As String, ByVal AppIssueCode As String) As String

    End Function

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Me.txtPatnm.Text = ""
        Me.txtIdno.Text = ""

    End Sub

    Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        sb_dll_Load_3()
    End Sub
End Class