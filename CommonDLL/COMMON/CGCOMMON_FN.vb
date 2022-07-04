'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGCOMMON_FN.vb                                                         */
'/* PartName     :                                                                        */
'/* Description  : 공통함수 정의                                                          */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports COMMON.CommLogin
Imports COMMON.SVar
Imports COMMON.CommConst

Namespace CommFN

#Region " 공통 함수 정의 : Class Fn "
    Public Class Fn
        Private Const msFile As String = "File : CGCOMMON_FN.vb, Class : CommFN.Fn" & vbTab

        Public Shared BrockenChar As Char = Chr(32)

        '메쏘드 오버로드, 물품입고에서 유효기간은 오직 날짜만 입력받도록 하기 위한 함수, Format에서"yyyy-MM-dd" -> 대소문자에 따라 결과값이 달라지므로 주의해야 함
        Public Shared Function ToDateQueryString(ByVal paramStr As String) As String
            If IsDate(paramStr) Then paramStr = Format(CDate(paramStr), "yyyy-MM-dd")
            ToDateQueryString = "TO_DATE( '" & paramStr & "', 'YYYY-MM-DD' )"
        End Function


        '-- 날짜 String형 오라클 Insert하는 문자열로 변환    
        Public Shared Function ToDateInsStr(ByVal asDate As String) As String
            If asDate.Equals("") Then
                ToDateInsStr = "NULL"
            Else
                If IsDate(asDate) Then asDate = Format(CDate(asDate), "yyyy-MM-dd HH:mm:ss")
                ToDateInsStr = "TO_DATE( '" & asDate & "', 'YYYY-MM-DD HH24:MI:SS' )"
            End If
        End Function

        '날짜 입력 받으면 일 단위의 쿼리를 만들기 위해 사용됨
        Public Shared Function toDateFromAndToQueryWithDay(ByVal paramDate As Date, ByVal paramIsFrom As Boolean) As String
            Dim strDate As String
            Dim strNormal As String
            Dim strPlus As String

            strNormal = Format(paramDate, "yyyy-MM-dd")

            If paramIsFrom = True Then
                strDate = " To_DATE( '" + strNormal + " 00:00:00','YYYY-MM-DD HH24:MI:SS') "
            Else
                strPlus = Format(paramDate.AddDays(1), "yyyy-MM-dd")
                strDate = " To_DATE( '" + strPlus + " 00:00:00','YYYY-MM-DD HH24:MI:SS') "
            End If

            toDateFromAndToQueryWithDay = strDate

        End Function


        Public Shared Function GetServerDateTime() As Date
            Dim sFn As String = "GetServerDateTime"

            Try

                Return Now

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try
        End Function

        Public Shared Function Format_ConvDay10(ByVal rsDay As String) As String
            Dim sFn As String = "Function Format_Day8ToDay10"

            Try
                If rsDay.Length < 8 Then Return ""

                rsDay = rsDay.Replace("-", "").Replace("/", "").Replace(":", "").Replace(",", "").Replace(" ", "")

                rsDay = (rsDay + "".PadRight(8, CChar("0"))).Substring(0, 8)

                Dim a_sBuf(rsDay.Length - 1) As String

                For i As Integer = 1 To rsDay.Length
                    a_sBuf(i - 1) = rsDay.Substring(i - 1, 1)
                Next

                Dim sReturn As String = String.Format("{0}{1}{2}{3}-{4}{5}-{6}{7}", a_sBuf)

                If IsDate(sReturn) = False Then
                    sReturn = ""
                End If

                Return sReturn

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        '<< 2020-07-03 JJH 텍스트 바이트수만큼 자르기
        Public Shared Function CHK_LENGTHB(ByVal Text As String, ByVal LengthB As Integer) As String
            '                                   텍스트                 제한 바이트수
            Try

                Dim chkLengthB As Integer = 0
                Dim rText As String = ""

                For i = 1 To Len(Text)

                    If Asc(Mid(Text, i, 1)) > 0 Then
                        chkLengthB += 1
                    Else
                        chkLengthB += 3
                    End If

                    If chkLengthB <= LengthB Then
                        rText = Mid(Text, 1, i)
                    Else
                        Continue For
                    End If

                Next

                If chkLengthB > LengthB Then
                    Return rText
                Else
                    Return Text
                End If


            Catch ex As Exception
                Return Text
            End Try

        End Function

        Public Shared Function Chk_Byte(ByVal Text As String) As Integer
            '                                텍스트             
            Try

                Dim lengthB As Integer = 0

                For i = 1 To Len(Text)
                    If Asc(Mid(Text, i, 1)) > 0 Then
                        lengthB += 1
                    Else
                        lengthB += 3
                    End If
                Next

                Return lengthB

            Catch ex As Exception
                Throw (New Exception(ex.Message))
            End Try

        End Function

        ' Error 로그
        Public Shared Sub logFile(ByVal sLog As String, ByVal rsFileNm As String, Optional ByVal rsPath As String = "")
            Dim sFile As String
            Dim sDir As String

            sDir = Application.StartupPath & IIf(rsPath = "", "", "\").ToString() & rsPath '& rsFileNm

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\" & rsFileNm & ".txt"
            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            'sw.WriteLine(Now())
            sw.WriteLine(sLog)
            sw.Close()

        End Sub

        ' Error 로그
        Public Shared Sub log(ByVal sLog As String)
            Dim sFile As String
            Dim sDir As String

            sDir = Application.StartupPath & "\ErrLog"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\Err" & Format(Now, "yyyy-MM-dd") & ".txt"
            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(Now())
            sw.WriteLine(vbTab & sLog)
            sw.Close()

        End Sub

        ' Error 로그
        Public Shared Sub log(ByVal sLog As String, ByVal e As ErrObject)
            Dim sFile As String
            Dim sDir As String

            sDir = Application.StartupPath & "\ErrLog"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\Err" & Format(Now, "yyyy-MM-dd") & ".txt"
            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(Now())

            sw.WriteLine(sLog)

            sw.WriteLine(vbTab & "Err Number : " & e.Number)
            sw.WriteLine(vbTab & "Err Description : " & e.Description)

            sw.Close()

        End Sub


        ' Error 로그
        Public Shared Sub log(ByVal sLog As String, ByVal e As String)
            Dim sFile As String
            Dim sDir As String

            sDir = Application.StartupPath & "\ErrLog"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\Err" & Format(Now, "yyyy-MM-dd") & ".txt"
            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(Now())

            sw.WriteLine(vbTab & sLog)
            sw.WriteLine(sLog)

            sw.WriteLine(vbTab & "Err Number : " & e)
            sw.WriteLine(vbTab & "Err Description : " & e)

            sw.Close()

        End Sub
        '20210312 jhs 검사코드 에 대한 로그 남길때 사용하는 로그 함수 (wbc diffcount에서 사용하고있음 20210312)
        ' 검사 로그
        Public Shared Sub log(ByVal rsTestInfoList As ArrayList)
            Dim sFile As String
            Dim sDir As String

            sDir = Application.StartupPath & "\TestCdLog"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\TestDate" & Format(Now, "yyyy-MM-dd") & ".txt"
            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine("--------------------------------------------------------------------------")
            sw.WriteLine(Now())
            '-----------------------------------------------------------
            '형식 
            '검체번호/검사코드/검체코드/분야/결과값
            '/중간보고자/중간보고일시/최종보고자/최종보고일시/중간보고체크여부/1.저장 전, 2.저장 후
            '-----------------------------------------------------------
            For i = 0 To rsTestInfoList.Count - 1
                Dim testinfo As TESTINFO_LOG = CType(rsTestInfoList(i), TESTINFO_LOG)
                sw.WriteLine(testinfo.BCNO + "/" + testinfo.TESTCD + "/" + testinfo.SPCCD + "/" + testinfo.PARTCD + testinfo.SLIPCD + "/" + testinfo.VIEWRST + "/" + testinfo.MWID + "/" + testinfo.MWDT + "/" + testinfo.FNID + "/" + testinfo.FNDT + "/" + testinfo.CHKMW.ToString + "/" + testinfo.ProcessNum)
            Next
            sw.WriteLine("--------------------------------------------------------------------------")
            sw.Close()

        End Sub
        '----------------------------------------------------------------------------------

        ' Error 로그 ArrayList
        Public Shared Sub log(ByVal alLog As ArrayList, ByVal e As ErrObject)
            Dim sFile As String
            Dim sDir As String

            sDir = Application.StartupPath & "\ERRLog"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\ERR" & Format(Now, "yyyy-MM-dd") & ".txt"
            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)
            Dim intLoop As Integer
            sw.WriteLine(Now())
            For intLoop = 0 To alLog.Count - 1
                sw.WriteLine(vbTab & Replace(Replace(Replace(alLog.Item(intLoop).ToString, "from", vbCrLf & vbTab & "from"), "select", vbCrLf & vbTab & "select"), "where", vbCrLf & vbTab & "where") & vbCrLf)
            Next
            sw.WriteLine(vbTab & "Err Number : " & e.Number)
            sw.WriteLine(vbTab & "Err Description : " & e.Description)

            sw.Close()

        End Sub

        Public Shared Sub ExclamationErrMsg(ByVal e As ErrObject, ByVal rsTitle As String, Optional ByVal rsErrMsg As String = "")
            Dim sErrMsg As String = ""

            If rsErrMsg <> "" Then sErrMsg = rsErrMsg + vbCrLf
            sErrMsg += e.Description
#If Not Debug Then
                    MsgBox(sErrMsg, MsgBoxStyle.Exclamation, rsTitle + " - 오류번호:" + Err.Number.ToString)
#End If
        End Sub

        ' AutoLabeler(AL) 로그
        Public Shared Sub ALsendlog(ByVal sLog As String, ByVal e As String)
            Dim sFile As String
            Dim sDir As String

            sDir = Application.StartupPath & "\SocketLog"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\Err" & Format(Now, "yyyy-MM-dd") & ".txt"
            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(Now())

            sw.WriteLine(vbTab & sLog)

            'sw.WriteLine(vbTab & "Err Number : " & e.Number)
            sw.WriteLine(vbTab & "Err Description : " & e)

            sw.Close()
        End Sub

        ' log data sended to AutoLabeler or TLA
        Public Shared Sub SendLog(ByVal sSource As String, ByVal sMsg As String)
            Dim sFile As String
            Dim sDir As String
            Dim sIP As String = ""

            sDir = Application.StartupPath & "\SendLog"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\" & sSource & "_" & Format(Now, "yyyy-MM-dd") & ".txt"

            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            Dim sHostName$ = System.Net.Dns.GetHostName

            Dim iphostentry As System.Net.IPHostEntry = System.Net.Dns.Resolve(sHostName)

            For Each addresslistCur As System.Net.IPAddress In iphostentry.AddressList
                If addresslistCur.ToString.StartsWith("192") Then
                    sIP = addresslistCur.ToString

                    Exit For
                End If
            Next

            sw.WriteLine("IP : " & sIP & ", Message : " & sMsg)

            sw.Close()
        End Sub

        ' Socket Error 로그
        Public Shared Sub SocketLog(ByVal sLog As String, ByVal sMsg As String)
            Dim sFile As String
            Dim sDir As String

            sDir = Application.StartupPath & "\SocketLog"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            sFile = sDir & "\Err" & Format(Now, "yyyy-MM-dd") & ".txt"
            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(Now())

            sw.WriteLine(vbTab & sLog)

            'sw.WriteLine(vbTab & "Err Number : " & e.Number)
            sw.WriteLine(vbTab & "Err Description : " & sMsg)

            sw.Close()
        End Sub


        '-- 컨트롤의 X, Y위치 반환
        Public Shared Function CtrlLocationXY(ByVal aoControl As Control) As System.Drawing.Point
            Dim objCtrl As Control = CType(aoControl, Control)
            Dim PointXY As Point

            '-- 특정 Control의 상위 컨트롤 알아내기.
            Do While Not objCtrl Is Nothing
                'Debug.WriteLine(objCtrl.Name() & ", " & objCtrl.Left.ToString & ", " & objCtrl.Top.ToString)

                If TypeOf (objCtrl) Is System.Windows.Forms.Form Then
                    PointXY.X += MdiMain.Frm.Location.X + objCtrl.Left + 4
                    'PointXY.X += 1000 + objCtrl.Left + 4 '수정
                    PointXY.Y += objCtrl.Top + 4
                Else
                    PointXY.X += objCtrl.Left
                    PointXY.Y += objCtrl.Top
                End If

                objCtrl = objCtrl.Parent()
            Loop

            If PointXY.X < 0 Then PointXY.X = 0
            If PointXY.Y < 0 Then PointXY.Y = 0

            Return PointXY
        End Function

        Public Shared Sub SearchToggle(ByRef aoLabel As System.Windows.Forms.Label, ByRef aoButton As System.Windows.Forms.Button,
                                     ByVal aeGbn As enumToggle, Optional ByRef aoText As System.Windows.Forms.TextBox = Nothing)

            Dim strText As String = ""
            Dim objForeColor As System.Drawing.Color
            Dim objBackColor As System.Drawing.Color

            Dim objButton As System.Windows.Forms.Button = CType(aoButton, System.Windows.Forms.Button)
            Dim strTag As String = CType(objButton.Tag, String)

            If aeGbn = enumToggle.RegnoToName Then
                If strTag = "1" Then
                    strText = "등록번호"
                    objBackColor = System.Drawing.Color.Navy
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = "0"

                    If Not IsNothing(aoText) Then
                        aoText.MaxLength = Login.PRG_CONST.Len_RegNo
                        aoText.ImeMode = ImeMode.Disable
                    End If
                Else
                    strText = "성    명"
                    objBackColor = System.Drawing.Color.Green
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"

                    If Not IsNothing(aoText) Then
                        aoText.MaxLength = Login.PRG_CONST.Len_PatNm
                        aoText.ImeMode = ImeMode.Hangul
                    End If
                End If

            ElseIf aeGbn = enumToggle.BcnoToRegno Then
                If strTag = "1" Then
                    strText = "검체번호"
                    objBackColor = System.Drawing.Color.FromArgb(165, 0, 123)
                    objForeColor = System.Drawing.Color.White

                    objButton.Tag = ""

                    '< mod freety 2005/08/03
                    '# YYYYMMDD-AB-12345-0 로 복사하여 입력도 가능하도록 처리
                    'If Not IsNothing(aoText) Then aoText.MaxLength = 15
                    If Not IsNothing(aoText) Then aoText.MaxLength = Login.PRG_CONST.Len_BcNo_Full
                Else
                    strText = "등록번호"
                    objBackColor = System.Drawing.Color.FromArgb(82, 97, 165)
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = "1"

                    If Not IsNothing(aoText) Then aoText.MaxLength = Login.PRG_CONST.Len_RegNo
                End If

            ElseIf aeGbn = enumToggle.Regno_Name_Bcno Then
                If strTag = "2" Then
                    strText = "등록번호"
                    objBackColor = System.Drawing.Color.FromArgb(82, 97, 165)
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = ""

                    If Not IsNothing(aoText) Then
                        aoText.MaxLength = Login.PRG_CONST.Len_RegNo
                        aoText.ImeMode = ImeMode.Disable
                    End If
                ElseIf strTag = "1" Then
                    strText = "검체번호"
                    objBackColor = System.Drawing.Color.FromArgb(165, 0, 123)
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = "2"

                    If Not IsNothing(aoText) Then
                        aoText.MaxLength = Login.PRG_CONST.Len_BcNo_Full
                        aoText.ImeMode = ImeMode.Disable
                    End If
                Else
                    strText = "성    명"
                    objBackColor = System.Drawing.Color.Green
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"

                    If Not IsNothing(aoText) Then
                        aoText.MaxLength = Login.PRG_CONST.Len_PatNm
                        aoText.ImeMode = ImeMode.Hangul
                    End If
                End If

            ElseIf aeGbn = enumToggle.ReportdtToRequestdt Then
                If strTag = "1" Then
                    strText = "보고일자"
                    objBackColor = System.Drawing.Color.Navy
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = ""
                Else
                    strText = "의뢰일자"
                    objBackColor = System.Drawing.Color.Green
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"
                End If

            ElseIf aeGbn = enumToggle.ReportdtToJubsudt Then
                If strTag = "1" Then
                    strText = "보고일자"
                    objBackColor = System.Drawing.Color.Navy
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = ""
                Else
                    strText = "접수일자"
                    objBackColor = System.Drawing.Color.Green
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"
                End If

            ElseIf aeGbn = enumToggle.DondtToRegdt Then
                If strTag = "1" Then
                    strText = "헌혈일자"
                    objBackColor = System.Drawing.Color.Navy
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = ""
                Else
                    strText = "등록일자"
                    objBackColor = System.Drawing.Color.Green
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"
                End If

            ElseIf aeGbn = enumToggle.IdnoToPName Then
                If strTag = "1" Then
                    strText = "주민등록번호"
                    objBackColor = System.Drawing.Color.Navy
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = ""

                    '< mod freety 2005/08/03
                    '# YYMMDD-12345678 로 복사하여 입력도 가능하도록 처리
                    'If Not IsNothing(aoText) Then aoText.MaxLength = 13
                    If Not IsNothing(aoText) Then
                        aoText.MaxLength = 14
                        aoText.ImeMode = ImeMode.Disable
                    End If
                Else
                    strText = "성    명"
                    objBackColor = System.Drawing.Color.Green
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"

                    If Not IsNothing(aoText) Then
                        aoText.MaxLength = Login.PRG_CONST.Len_PatNm
                        aoText.ImeMode = ImeMode.Hangul
                    End If
                End If

            ElseIf aeGbn = enumToggle.OrddtToJubsudt Then
                If strTag = "1" Then
                    strText = "처방일자"
                    objBackColor = System.Drawing.Color.Navy
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = ""

                Else
                    strText = "접수일자"
                    objBackColor = System.Drawing.Color.Green
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"

                End If

            ElseIf aeGbn = enumToggle.TransfusionToRegno Then
                If strTag = "1" Then
                    strText = "수혈의뢰접수번호"
                    objBackColor = System.Drawing.Color.Purple
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = ""

                    '< mod freety 2005/08/03
                    '# YYYYMMDD-T-1234 로 복사하여 입력도 가능하도록 처리
                    'If Not IsNothing(aoText) Then aoText.MaxLength = 13
                    If Not IsNothing(aoText) Then aoText.MaxLength = FixedVariable.giLen_TnsJubsuNo_Full
                Else
                    strText = "등록번호"
                    objBackColor = System.Drawing.Color.Navy
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = "1"

                    If Not IsNothing(aoText) Then aoText.MaxLength = Login.PRG_CONST.Len_RegNo
                End If

            ElseIf aeGbn = enumToggle.OrddtToOutdt Then
                If strTag = "1" Then
                    strText = "처방일자"
                    objBackColor = System.Drawing.Color.DarkSlateBlue
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = ""

                Else
                    strText = "출고일자"
                    objBackColor = System.Drawing.Color.Brown
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"

                End If

            ElseIf aeGbn = enumToggle.OutdtToOrddt Then
                If strTag = "1" Then
                    strText = "출고일자"
                    objBackColor = System.Drawing.Color.DarkSlateBlue
                    objForeColor = System.Drawing.Color.White
                    objButton.Tag = ""

                Else
                    strText = "처방일자"
                    objBackColor = System.Drawing.Color.Brown
                    objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                    objButton.Tag = "1"
                End If

            End If

            With aoLabel
                .Text = strText
                .BackColor = objBackColor
                .ForeColor = objForeColor
            End With

        End Sub

        ' Spread Header를 컬럼명으로 설정
        Public Shared Sub SpdSetColName(ByRef aoSpread As AxFPSpreadADO.AxfpSpread)
            Dim strHeaderName As String

            With aoSpread
                For intCol As Integer = 0 To .MaxCols
                    .Row = 0 : .Col = intCol
                    strHeaderName = .Text.ToString
                    .ColID = strHeaderName
                Next
            End With

        End Sub

        ' 컬럼기준 내용검색 
        ' 처음 찾은Row값 반환
        ' 없으면 0 반환
        Public Shared Function SpdColSearch(ByVal aoSpread As AxFPSpreadADO.AxfpSpread,
                                            ByVal asStr As String, ByVal aiCol As Integer,
                                            Optional ByVal aiStRow As Integer = 0) As Integer
            Dim intRetVal As Integer
            SpdColSearch = 0
            With aoSpread
                intRetVal = .SearchCol(aiCol, aiStRow, .MaxRows, asStr, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                If intRetVal <> -1 Then SpdColSearch = intRetVal
            End With

        End Function


        ' 검체번호 [-]구분으로 표시하기
        Public Shared Function BCNO_PIS_View(ByVal rsBcNo As String) As String

            Dim sDate As String = ""
            Dim sSpcSeqNo As String = ""

            If rsBcNo.Length.Equals(13) Then
                sDate = rsBcNo.Substring(0, 8)
                sSpcSeqNo = rsBcNo.Substring(8, 5)

                Return sDate + "-" + sSpcSeqNo
            Else
                Return rsBcNo
            End If

        End Function

        ' 검체번호 [-]구분으로 표시하기
        Public Shared Function BCNO_View(ByVal rsBcNo As String, Optional ByVal rbFullGbn As Boolean = False) As String

            Dim sDate As String = ""
            Dim sBcClsCd As String = ""
            Dim sSpcSeqNo As String = ""
            Dim sPlural As String = ""

            If rsBcNo.Length.Equals(15) Or rsBcNo.Length.Equals(14) Then
                sDate = rsBcNo.Substring(0, 8)
                sBcClsCd = rsBcNo.Substring(8, 2)
                sSpcSeqNo = rsBcNo.Substring(10, 4)

                If rbFullGbn = True Then
                    sPlural = "0"
                    If rsBcNo.Length = 15 Then sPlural = rsBcNo.Substring(14, 1)
                    BCNO_View = sDate + "-" + sBcClsCd + "-" + sSpcSeqNo + "-" + sPlural
                Else
                    BCNO_View = sDate + "-" + sBcClsCd + "-" + sSpcSeqNo
                End If
            Else
                BCNO_View = rsBcNo
            End If

        End Function

        ' 작업번호 [-]구분으로 표시하기
        Public Shared Function WKNO_View(ByVal rsWkNo As String) As String

            Dim sDate As String = ""
            Dim sWorkGrpCd As String = ""
            Dim sWKSeqNo As String = ""

            If rsWkNo.Length.Equals(14) Then
                sDate = rsWkNo.Substring(0, 8)
                sWorkGrpCd = rsWkNo.Substring(8, 2)
                sWKSeqNo = rsWkNo.Substring(10, 4)

                Return sDate + "-" + sWorkGrpCd + "-" + sWKSeqNo
            Else
                Return rsWkNo
            End If

        End Function

        ' 수혈의뢰접수번호 [-]구분으로 표시하기
        Public Shared Function TNSNO_View(ByVal rsTnsNo As String) As String
            Dim sDate As String = ""
            Dim sTnsCd As String = ""
            Dim sTnsSeqNo As String = ""

            If rsTnsNo.Length.Equals(13) Then
                sDate = rsTnsNo.Substring(0, 8)
                sTnsCd = rsTnsNo.Substring(8, 1)
                sTnsSeqNo = rsTnsNo.Substring(9, 4)

                TNSNO_View = sDate + "-" + sTnsCd + "-" + sTnsSeqNo

            Else
                TNSNO_View = rsTnsNo
            End If

        End Function

        ' 혈액번호 [-]구분으로 표시하기
        Public Shared Function BLDNO_View(ByVal rsBldNo As String) As String
            Dim sDonGgn As String = ""
            Dim sYY As String = ""
            Dim sBdSeqNo As String = ""
            Dim sBdSubSeqNo As String = ""

            If rsBldNo.Length.Equals(10) Then
                sDonGgn = rsBldNo.Substring(0, 2)
                sYY = rsBldNo.Substring(2, 2)
                sBdSeqNo = rsBldNo.Substring(4, 6)
                BLDNO_View = sDonGgn + "-" + sYY + "-" + sBdSeqNo

            ElseIf rsBldNo.Length.Equals(11) Then
                sDonGgn = rsBldNo.Substring(0, 2)
                sYY = rsBldNo.Substring(2, 2)
                sBdSeqNo = rsBldNo.Substring(4, 6)
                sBdSubSeqNo = rsBldNo.Substring(10, 1)

                BLDNO_View = sDonGgn + "-" + sYY + "-" + sBdSeqNo + "-" + sBdSubSeqNo

            Else
                BLDNO_View = rsBldNo

            End If

        End Function

        ' ToolTip 보이기
        ' 선 설정: Spread.TextTip = AxFPSpreadADO.TextTipConstants.TextTipFloating
        Public Shared Sub SpreadToolTipView(ByVal aoSpd As AxFPSpreadADO.AxfpSpread, ByVal aoGraphics As System.Drawing.Graphics,
                                            ByVal e As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent,
                                            ByVal aiCol As Integer, ByVal abMultiLine As Boolean,
                                            Optional ByVal asToolTipText As String = "")
            If e.row < 1 Then Exit Sub

            Dim strTText As String
            With aoSpd
                Select Case e.col
                    Case aiCol
                        e.multiLine = 0
                        If abMultiLine = True Then e.multiLine = FPSpreadADO.TextTipFetchMultilineConstants.TextTipFetchMultilineMultiple

                        .SetTextTipAppearance("굴림체", 9, False, False, &HDFFFFF&, &H800000)
                        e.showTip = True
                        If asToolTipText <> "" Then
                            strTText = asToolTipText
                        Else
                            .Col = e.col : .Row = e.row
                            strTText = .Text
                        End If

                        If strTText <> "" Then
                            e.tipWidth = GetToolTipWidth(aoGraphics, strTText, .Font)
                            e.tipText = strTText
                        Else
                            e.showTip = False
                        End If

                    Case Else
                        e.showTip = False
                End Select
            End With
        End Sub

        Public Shared Function GetToolTipWidth(ByVal aoGraphics As System.Drawing.Graphics, ByVal asText As String, ByVal aoFont As System.Drawing.Font) As Integer
            Dim arrText() As String = Split(asText, vbCrLf)
            Dim sngMaxText As Single
            Dim sngTextWidth As Single

            For intRow As Integer = 0 To UBound(arrText)
                sngTextWidth = aoGraphics.MeasureString(arrText(intRow), aoFont).Width
                If sngMaxText < sngTextWidth Then
                    sngMaxText = sngTextWidth
                End If
            Next

            GetToolTipWidth = CInt(sngMaxText) * 15
        End Function

        ' adtDate1 계산할 일시
        ' adtDate2 현재일시( 예: sysdate )
        ' adtDate2의 값에서 adtDate1의 값을 빼서 두 값 사이의 시간차를 구함
        Public Shared Function TimeElapsed(ByVal adtDate1 As Date, ByVal adtDate2 As Date) As String
            Dim lngHH As Long
            Dim lngMI As Long
            Dim lngSS As Long

            lngHH = DateDiff(DateInterval.Hour, adtDate1, adtDate2) : adtDate1 = DateAdd(DateInterval.Hour, lngHH, adtDate1)
            lngMI = DateDiff(DateInterval.Minute, adtDate1, adtDate2) : adtDate1 = DateAdd(DateInterval.Minute, lngMI, adtDate1)
            lngSS = DateDiff(DateInterval.Second, adtDate1, adtDate2)

            TimeElapsed = Format(lngHH, "0#") & ":" & Format(lngMI, "0#") & ":" & Format(lngSS, "0#")
        End Function

        Public Shared Function LengthH(ByVal asHangulMix As String) As Integer
            Try
                Dim unicodEnCoding As System.Text.Encoding = System.Text.Encoding.Unicode
                Dim iLen% = 0

                ' Convert the string into a byte[].
                Dim unicodeBytes As Byte() = unicodEnCoding.GetBytes(asHangulMix)

                For i As Integer = 0 To (unicodeBytes.Length \ 2) - 1
                    If unicodeBytes(2 * i + 1).GetHashCode() > 0 Then
                        iLen += 2
                    Else
                        iLen += 1
                    End If
                Next

                LengthH = iLen
            Catch ex As Exception
                LengthH = Nothing
                Throw New System.Exception("Hangul 2 Byte 처리 오류")
            End Try
        End Function

        Public Shared Function SubstringH(ByVal asHangulMix As String, ByVal aiIndex As Integer) As String
            Try
                Dim asciiEncoding As System.Text.Encoding = System.Text.Encoding.ASCII
                Dim unicodeEnCoding As System.Text.Encoding = System.Text.Encoding.Unicode

                Dim iLen% = 0
                Dim iL% = 0, iU% = 0
                Dim sBuf$ = ""

                ' Convert the string into a byte[].
                Dim unicodeBytes As Byte() = unicodeEnCoding.GetBytes(asHangulMix)

                For i As Integer = 0 To unicodeBytes.Length - 1
                    If i Mod 2 = 0 Then
                        iL = unicodeBytes(i).GetHashCode
                    Else
                        iU = unicodeBytes(i).GetHashCode

                        If iU > 0 Then
                            iLen += 2
                        Else
                            iLen += 1
                        End If

                        If aiIndex + 1 <= iLen Then
                            sBuf += ChrW(iL + iU * 256)
                        End If
                    End If
                Next

                SubstringH = sBuf
            Catch ex As Exception
                SubstringH = ""
                Throw New System.Exception("Hangul 2 Byte 처리 오류")
            End Try
        End Function

        Public Shared Function SubstringH(ByVal asHangulMix As String, ByVal aiIndex As Integer, ByVal aiLength As Integer) As String
            Try
                Dim asciiEncoding As System.Text.Encoding = System.Text.Encoding.ASCII
                Dim unicodeEnCoding As System.Text.Encoding = System.Text.Encoding.Unicode

                Dim iLen% = 0
                Dim iL% = 0, iU% = 0
                Dim sBuf$ = ""
                Dim iBroken% = 0

                ' Convert the string into a byte[].
                Dim unicodeBytes As Byte() = unicodeEnCoding.GetBytes(asHangulMix)

                For i As Integer = 0 To unicodeBytes.Length - 1
                    If i Mod 2 = 0 Then
                        iL = unicodeBytes(i).GetHashCode
                    Else
                        iU = unicodeBytes(i).GetHashCode

                        If iU > 0 Then
                            iLen += 2
                            iBroken = 1
                        Else
                            iLen += 1
                        End If

                        If iLen > aiLength + aiIndex Then
                            Exit For
                        Else
                            If aiIndex + 1 <= iLen Then
                                sBuf += ChrW(iL + iU * 256)
                            End If
                        End If
                    End If

                    iBroken = 0
                Next

                If LengthH(sBuf) = aiLength Then
                    SubstringH = sBuf
                Else
                    If iBroken = 1 Then
                        SubstringH = sBuf & BrockenChar
                    Else
                        SubstringH = sBuf
                    End If
                End If
            Catch ex As Exception
                SubstringH = "".PadRight(aiLength)
                Throw New System.Exception("Hangul 2 Byte 처리 오류")

                Return ""
            End Try
        End Function

        Public Shared Function SubstringH(ByVal asHangulMix As String, ByVal aiIndex As Integer, ByVal aiLength As Integer, ByVal aiAscii As Integer) As String
            Try
                Dim asciiEncoding As System.Text.Encoding = System.Text.Encoding.ASCII
                Dim unicodeEnCoding As System.Text.Encoding = System.Text.Encoding.Unicode

                Dim iLen% = 0
                Dim iL% = 0, iU% = 0
                Dim sBuf$ = ""
                Dim iBroken% = 0

                ' Convert the string into a byte[].
                Dim unicodeBytes As Byte() = unicodeEnCoding.GetBytes(asHangulMix)

                For i As Integer = 0 To unicodeBytes.Length - 1
                    If i Mod 2 = 0 Then
                        iL = unicodeBytes(i).GetHashCode
                    Else
                        iU = unicodeBytes(i).GetHashCode

                        If iU > 0 Then
                            iLen += 2
                            iBroken = 1
                        Else
                            iLen += 1
                        End If

                        If iLen > aiLength + aiIndex Then
                            Exit For
                        Else
                            If aiIndex + 1 <= iLen Then
                                sBuf += ChrW(iL + iU * 256)
                            End If
                        End If
                    End If

                    iBroken = 0
                Next

                If LengthH(sBuf) = aiLength Then
                    SubstringH = sBuf
                Else
                    If iBroken = 1 Then
                        SubstringH = sBuf & Convert.ToChar(aiAscii)
                    Else
                        SubstringH = sBuf
                    End If
                End If
            Catch ex As Exception
                SubstringH = "".PadRight(aiLength)
                Throw New System.Exception("Hangul 2 Byte 처리 오류")
            End Try
        End Function

        ' 이 문자열의 문자를 오른쪽으로 맞추고 지정한 길이만큼 왼쪽의 안쪽 여백을 공백 문자로 채웁니다. ( 한글2바이트 처리 )
        ' 만약 문자열이 지정길이보다 크면 지정길이의 문자열값만 취한다.
        Public Shared Function PadLeftH(ByVal asHangulMix As String, ByVal aiLength As Integer) As String
            Dim sFn As String = "Public Shared Function RightPadingH(ByVal asHangulMix As String, ByVal aiLength As Integer) As String"
            Dim aiHangulLength As Integer = LengthH(asHangulMix)

            Try
                If aiLength < aiHangulLength Then
                    PadLeftH = SubstringH(asHangulMix, 0, aiLength)
                Else
                    PadLeftH = Space(aiLength - LengthH(asHangulMix)) & asHangulMix
                End If

            Catch ex As Exception
                PadLeftH = "".PadLeft(aiLength)
                Fn.log(msFile & sFn, Err)

            End Try

        End Function

        ' 이 문자열의 문자를 왼쪽으로 맞추고 지정한 길이만큼 오른쪽의 안쪽 여백을 공백 문자로 채웁니다. ( 한글2바이트 처리 )
        ' 만약 문자열이 지정길이보다 크면 지정길이의 문자열값만 취한다.
        Public Shared Function PadRightH(ByVal asHangulMix As String, ByVal aiLength As Integer) As String
            Dim sFn As String = "Public Shared Function RightPadingH(ByVal asHangulMix As String, ByVal aiLength As Integer) As String"
            Dim aiHangulLength As Integer = LengthH(asHangulMix)

            Try
                If aiLength < aiHangulLength Then
                    PadRightH = SubstringH(asHangulMix, 0, aiLength)
                Else
                    PadRightH = asHangulMix & Space(aiLength - LengthH(asHangulMix))
                End If

            Catch ex As Exception
                PadRightH = "".PadRight(aiLength)
                Fn.log(msFile & sFn, Err)

            End Try

        End Function

        Public Shared Function fnCalcAge(ByVal as_LeftID As String, ByVal as_NowDate As Date, Optional ByVal as_RightID As String = "") As String ' 나이계산
            Dim sFn As String = "Shared Function fnCalcAge(ByVal as_LeftID As String, ByVal as_NowDate As Date) As Date "

            Dim strIDYear As String = ""
            Dim strIDMonth As String = ""
            Dim strIDDay As String = ""
            Dim dtLeftJumin As Date
            Dim strAge As Long

            Try
                If as_LeftID.Trim.Length = 8 Then       ' ocs 에서 birth로 가져온 경우 (ex) 1928-12-10 )
                    strIDYear = as_LeftID.Substring(0, 4)
                    strIDMonth = as_LeftID.Substring(4, 2)
                    strIDDay = as_LeftID.Substring(6, 2)

                ElseIf as_LeftID.Trim.Length = 6 Then   ' 주민등록번호로 조회하는 경우
                    strIDYear = as_LeftID.Substring(0, 2)
                    strIDMonth = as_LeftID.Substring(2, 2)
                    strIDDay = as_LeftID.Substring(4, 2)
                End If

                If IsDate(strIDYear & "-" & strIDMonth & "-" & strIDDay) = False Then ' IsDate - > 식이 날짜로 변환될수 있는지!!
                    MsgBox("주민등록번호를 확인하세요")
                    Exit Function
                Else
                    If CType(as_RightID.Substring(0, 1), Integer) < 3 Then
                        If as_LeftID.Trim.Length = 8 Then
                            dtLeftJumin = CType(strIDYear & "-" & strIDMonth & "-" & strIDDay, Date)
                        Else
                            dtLeftJumin = CType("19" & strIDYear & "-" & strIDMonth & "-" & strIDDay, Date)
                        End If
                    Else
                        If as_LeftID.Trim.Length = 8 Then
                            dtLeftJumin = CType(strIDYear & "-" & strIDMonth & "-" & strIDDay, Date)
                        Else
                            dtLeftJumin = CType("20" & strIDYear & "-" & strIDMonth & "-" & strIDDay, Date)
                        End If
                    End If
                End If

                ' 생일 날짜가 나중에 없는지 확인
                If dtLeftJumin > as_NowDate Then
                    dtLeftJumin = as_NowDate
                End If

                ' 오늘과 생일 날짜 간의 연도 수를 계산
                strAge = DateDiff(DateInterval.Year, dtLeftJumin, as_NowDate)

                ' 생일 날짜가 올해에 나타나지 않으면 나이에서 1을 뺀다
                If DateSerial(Year(as_NowDate), Month(dtLeftJumin), Microsoft.VisualBasic.DateAndTime.Day(dtLeftJumin)) > as_NowDate Then
                    strAge -= 1
                End If

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
            Finally
                fnCalcAge = CType(strAge, String)
            End Try

        End Function

        ' 라인그리기( 해당 Row의 Top ) 
        Public Shared Sub DrawBorderLineTop(ByVal aoSpread As AxFPSpreadADO.AxfpSpread, ByVal aiRow As Integer,
                                            Optional ByVal aiStartCol As Integer = 1, Optional ByVal aiEndCol As Integer = -1)
            Dim sFn As String = "Public Shared Sub DrawBorderLineTop(ByVal aoSpread As AxFPSpreadADO.AxfpSpread, ByVal aiRow As Integer)"

            Try
                With aoSpread
                    If aiEndCol = -1 Then aiEndCol = .MaxCols
                    .SetCellBorder(aiStartCol, aiRow, aiEndCol, aiRow, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexTop,
                                   Convert.ToUInt32(Microsoft.VisualBasic.RGB(128, 128, 128)),
                                   FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
                End With

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try

        End Sub

        ' ABO의 혈액혈 글자 색상
        Public Shared Function GetBldFrColor(ByVal asABO As String) As Drawing.Color
            Dim sFn As String = ""

            Try
                GetBldFrColor = System.Drawing.Color.SeaGreen
                Select Case asABO.ToUpper
                    Case "A" : GetBldFrColor = System.Drawing.Color.Goldenrod
                    Case "B" : GetBldFrColor = System.Drawing.Color.Crimson
                    Case "O" : GetBldFrColor = System.Drawing.Color.RoyalBlue
                    Case "AB" : GetBldFrColor = System.Drawing.Color.Black
                End Select

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try

        End Function

        Public Shared Function RemoveRightCrLf(ByVal rsBuf As String) As String
            Dim sFn As String = "RemoveRightCrLf"

            Try
                Do
                    rsBuf = rsBuf.TrimEnd(" ".ToCharArray())

                    If rsBuf.EndsWith(vbCrLf) Then
                        rsBuf = rsBuf.Substring(0, rsBuf.Length - 2)
                    Else
                        Exit Do
                    End If
                Loop While (True)

                Return rsBuf

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        Public Shared Function SplitFixedLength(ByVal rsMix As String, ByVal riLen As Integer) As ArrayList
            Dim sFn As String = "SplitFixedLength"

            Try
                Dim al_buf As New ArrayList

                Dim sBuf_n As String = ""

                Dim iBrokenAscii As Integer = 1

                Do
                    If LengthH(rsMix) <= riLen Then
                        If LengthH(rsMix) > 0 Then
                            al_buf.Add(rsMix)
                        End If

                        Exit Do
                    Else
                        '한글이 중간에 잘리는 경우를 위한 처리
                        sBuf_n = SubstringH(rsMix, 0, riLen, iBrokenAscii)

                        If sBuf_n.EndsWith(Convert.ToChar(iBrokenAscii)) Then
                            al_buf.Add(SubstringH(rsMix, 0, riLen - 1))
                            rsMix = SubstringH(rsMix, riLen - 1)
                        Else
                            al_buf.Add(SubstringH(rsMix, 0, riLen))
                            rsMix = SubstringH(rsMix, riLen)
                        End If
                    End If
                Loop While (True)

                Return al_buf

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        Public Shared Function ChangeToDataTable(ByVal ra_dr() As DataRow) As DataTable
            Dim sFn As String = "ChangeToDataTable"

            Try
                Dim dt As New DataTable

                If ra_dr.Length < 1 Then Return dt

                With ra_dr(0).Table
                    'Column 추가
                    For j As Integer = 1 To .Columns.Count
                        Dim dc As DataColumn = New DataColumn
                        dc.ColumnName = ra_dr(0).Table.Columns(j - 1).ColumnName
                        dc.DataType = .Columns(j - 1).DataType
                        dc.Caption = .Columns(j - 1).Caption

                        dt.Columns.Add(dc)
                    Next
                End With

                For i As Integer = 1 To ra_dr.Length
                    'Row 추가
                    Dim dr As DataRow = dt.NewRow()

                    For j As Integer = 1 To dt.Columns.Count
                        dr.Item(j - 1) = ra_dr(i - 1).Item(j - 1)
                    Next

                    dt.Rows.Add(dr)
                Next

                Return dt

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function


        Public Shared Function Format_Day8ToDay10(ByVal rsDay As String) As String
            Dim sFn As String = "Function Format_Day8ToDay10"

            Try
                If Not rsDay.Length = 8 Then Return ""

                Dim a_sBuf(rsDay.Length - 1) As String

                For i As Integer = 1 To rsDay.Length
                    a_sBuf(i - 1) = rsDay.Substring(i - 1, 1)
                Next

                Return "".Format("{0}{1}{2}{3}-{4}{5}-{6}{7}", a_sBuf).ToString

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        Public Shared Function GetIPAddress(ByVal rsHostName As String) As String
            Dim sFn As String = "Function GetIPAddress"

            Try
                Dim sIPAddress As String = ""

                For Each ipaddr As Net.IPAddress In Net.Dns.GetHostByName(rsHostName).AddressList
                    If ipaddr.ToString().StartsWith(FixedVariable.gsIPAddress_Scope) Then
                        sIPAddress = ipaddr.ToString()

                        Exit For
                    End If
                Next

                If sIPAddress.Length = 0 Then
                    sIPAddress = Net.Dns.GetHostByName(rsHostName).AddressList(0).ToString()
                End If

                Return sIPAddress

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        Public Shared Function AddColToDataTable(ByVal r_dt As DataTable, ByVal r_al_dci As ArrayList, ByVal r_al_dcv As ArrayList) As DataTable
            Dim sFn As String = "AddColToDataTable"

            Try
                Dim dt As New DataTable

                If r_dt.Rows.Count < 1 Then Return dt
                If r_al_dci.Count <> r_al_dcv.Count Then Return dt

                With r_dt
                    'Column 추가
                    For j As Integer = 1 To .Columns.Count
                        Dim dc As DataColumn = New DataColumn
                        dc.ColumnName = .Columns(j - 1).ColumnName
                        dc.DataType = .Columns(j - 1).DataType
                        dc.Caption = .Columns(j - 1).Caption

                        dt.Columns.Add(dc)
                    Next

                    For j As Integer = 1 To r_al_dci.Count
                        Dim dc As DataColumn = New DataColumn
                        dc.ColumnName = CType(r_al_dci(j - 1), STU_DataColInfo).ColName
                        dc.DataType = CType(r_al_dci(j - 1), STU_DataColInfo).ColType
                        dc.Caption = CType(r_al_dci(j - 1), STU_DataColInfo).ColCapt

                        dt.Columns.Add(dc)
                    Next
                End With

                For i As Integer = 1 To r_dt.Rows.Count
                    'Row 추가
                    Dim dr As DataRow = dt.NewRow()

                    For j As Integer = 1 To r_dt.Columns.Count
                        dr.Item(j - 1) = r_dt.Rows(i - 1).Item(j - 1)
                    Next

                    For j As Integer = 1 To r_al_dcv.Count
                        dr.Item(r_dt.Columns.Count + j - 1) = r_al_dcv(j - 1)
                    Next

                    dt.Rows.Add(dr)
                Next

                Return dt

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        Public Shared Function FindErrCalcFormula(ByVal rsCF As String) As Boolean
            Dim sFn As String = "FindErrCalcFormula"

            Dim bReturn As Boolean = False

            Try
                Dim sCFBuf As String = rsCF.Replace(" ", "")

                '계산식 자체 오류 구하기 -> 1A  A1  AA  ?.  .?  A(  )A  1(  )1

                For i As Integer = 0 To sCFBuf.Length - 2
                    Dim sBuf As String = sCFBuf.Substring(i, 2)

                    '1 : 0~9  ¶ 2 : A~J  ¶ 3 : (  ¶ 4 : )  ¶ 5 : .  ¶ 6 : +,-,*,/
                    Dim iType1 As Integer = 0
                    Dim iType2 As Integer = 0

                    If Convert.ToInt32(Convert.ToChar(sBuf.Substring(0, 1))) >= 48 And Convert.ToInt32(Convert.ToChar(sBuf.Substring(0, 1))) <= 57 Then
                        iType1 = 1
                    ElseIf Convert.ToInt32(Convert.ToChar(sBuf.Substring(0, 1))) >= 65 And Convert.ToInt32(Convert.ToChar(sBuf.Substring(0, 1))) <= 74 Then
                        iType1 = 2
                    Else
                        Select Case sBuf.Substring(0, 1)
                            Case "(" : iType1 = 3
                            Case ")" : iType1 = 4
                            Case "." : iType1 = 5
                            Case "+", "-", "*", "/" : iType1 = 6
                            Case "^" : iType1 = 7
                        End Select
                    End If

                    If Convert.ToInt32(Convert.ToChar(sBuf.Substring(1, 1))) >= 48 And Convert.ToInt32(Convert.ToChar(sBuf.Substring(1, 1))) <= 57 Then
                        iType2 = 1
                    ElseIf Convert.ToInt32(Convert.ToChar(sBuf.Substring(1, 1))) >= 65 And Convert.ToInt32(Convert.ToChar(sBuf.Substring(1, 1))) <= 74 Then
                        iType2 = 2
                    Else
                        Select Case sBuf.Substring(1, 1)
                            Case "(" : iType2 = 3
                            Case ")" : iType2 = 4
                            Case "." : iType2 = 5
                            Case "+", "-", "*", "/" : iType2 = 6
                            Case "^" : iType2 = 7
                        End Select
                    End If

                    If iType1 = 1 Then
                        If iType2 = 1 Then
                            '11
                            bReturn = False
                        ElseIf iType2 = 2 Then
                            '1A
                            bReturn = True
                        ElseIf iType2 = 3 Then
                            '1(
                            bReturn = True
                        ElseIf iType2 = 4 Then
                            '1)
                            bReturn = False
                        ElseIf iType2 = 5 Then
                            '1.
                            bReturn = False
                        ElseIf iType2 = 6 Then
                            '1+
                            bReturn = False
                        ElseIf iType2 = 7 Then
                            '^
                            bReturn = False
                        End If
                    End If

                    If bReturn Then
                        Return bReturn
                    End If

                    If iType1 = 2 Then
                        If iType2 = 1 Then
                            'A1
                            bReturn = True
                        ElseIf iType2 = 2 Then
                            'AA
                            bReturn = True
                        ElseIf iType2 = 3 Then
                            'A(
                            bReturn = True
                        ElseIf iType2 = 4 Then
                            'A)
                            bReturn = False
                        ElseIf iType2 = 5 Then
                            'A.
                            bReturn = True
                        ElseIf iType2 = 6 Then
                            'A+
                            bReturn = False
                        ElseIf iType2 = 7 Then
                            '^
                            bReturn = False
                        End If
                    End If

                    If bReturn Then
                        Return bReturn
                    End If

                    If iType1 = 3 Then
                        If iType2 = 1 Then
                            '(1
                            bReturn = False
                        ElseIf iType2 = 2 Then
                            '(A
                            bReturn = False
                        ElseIf iType2 = 3 Then
                            '((
                            bReturn = False
                        ElseIf iType2 = 4 Then
                            '()
                            bReturn = True
                        ElseIf iType2 = 5 Then
                            '(.
                            bReturn = True
                        ElseIf iType2 = 6 Then
                            '(+
                            bReturn = True
                        ElseIf iType2 = 7 Then
                            '^
                            bReturn = False
                        End If
                    End If

                    If bReturn Then
                        Return bReturn
                    End If

                    If iType1 = 4 Then
                        If iType2 = 1 Then
                            ')1
                            bReturn = True
                        ElseIf iType2 = 2 Then
                            ')A
                            bReturn = True
                        ElseIf iType2 = 3 Then
                            ')(
                            bReturn = False
                        ElseIf iType2 = 4 Then
                            '))
                            bReturn = False
                        ElseIf iType2 = 5 Then
                            ').
                            bReturn = True
                        ElseIf iType2 = 6 Then
                            ')+
                            bReturn = False
                        ElseIf iType2 = 7 Then
                            '^
                            bReturn = False
                        End If
                    End If

                    If bReturn Then
                        Return bReturn
                    End If

                    If iType1 = 5 Then
                        If iType2 = 1 Then
                            '.1
                            bReturn = False
                        ElseIf iType2 = 2 Then
                            '.A
                            bReturn = True
                        ElseIf iType2 = 3 Then
                            '.(
                            bReturn = True
                        ElseIf iType2 = 4 Then
                            '.)
                            bReturn = True
                        ElseIf iType2 = 5 Then
                            '..
                            bReturn = True
                        ElseIf iType2 = 6 Then
                            '.+
                            bReturn = True
                        ElseIf iType2 = 7 Then
                            '^
                            bReturn = False
                        End If
                    End If

                    If bReturn Then
                        Return bReturn
                    End If

                    If iType1 = 6 Then
                        If iType2 = 1 Then
                            '+1
                            bReturn = False
                        ElseIf iType2 = 2 Then
                            '+A
                            bReturn = False
                        ElseIf iType2 = 3 Then
                            '+(
                            bReturn = False
                        ElseIf iType2 = 4 Then
                            '+)
                            bReturn = True
                        ElseIf iType2 = 5 Then
                            '+.
                            bReturn = True
                        ElseIf iType2 = 6 Then
                            '++
                            bReturn = True
                        ElseIf iType2 = 7 Then
                            '^
                            bReturn = False
                        End If
                    End If

                    If iType1 = 7 Then
                        If iType2 = 1 Then
                            '+1
                            bReturn = False
                        ElseIf iType2 = 2 Then
                            '+A
                            bReturn = False
                        ElseIf iType2 = 3 Then
                            '+(
                            bReturn = False
                        ElseIf iType2 = 4 Then
                            '+)
                            bReturn = True
                        ElseIf iType2 = 5 Then
                            '+.
                            bReturn = True
                        ElseIf iType2 = 6 Then
                            '++
                            bReturn = True
                        ElseIf iType2 = 7 Then
                            '^
                            bReturn = False
                        End If
                    End If

                    If bReturn Then
                        Return bReturn
                    End If
                Next

                Return bReturn

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        Public Shared Function ChgColToDataTable(ByVal r_dt As DataTable, ByVal r_al_src As ArrayList, ByVal r_al_trg As ArrayList) As DataTable
            Dim sFn As String = "ChgColToDataTable"

            Try
                Dim dt As DataTable = r_dt.Copy

                If r_dt.Rows.Count < 1 Then Return dt

                With dt
                    For j As Integer = 1 To .Columns.Count
                        For k As Integer = 1 To r_al_src.Count
                            If .Columns(j - 1).ColumnName.ToUpper = r_al_src(k - 1).ToString.ToUpper Then
                                .Columns(j - 1).ColumnName = r_al_trg(k - 1).ToString

                                Exit For
                            End If
                        Next
                    Next
                End With

                Return dt

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        '< yjlee 2009-01-21 
        Public Shared Function Format_Time6ToTime8(ByVal rsTime As String) As String
            Dim sFn As String = "Function Format_Time6ToTime8"

            Try
                If Not rsTime.Length = 6 Then Return ""

                Dim a_sBuf(rsTime.Length - 1) As String

                For i As Integer = 1 To rsTime.Length
                    a_sBuf(i - 1) = rsTime.Substring(i - 1, 1)
                Next

                Return String.Format("{0}{1}:{2}{3}:{4}{5}", a_sBuf)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try
        End Function

        '> yjlee 2009-01-21 

        Public Shared Sub sbNumericTextBox(ByVal r_o_text As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
            Dim KeyAscii As Integer

            KeyAscii = Asc(e.KeyChar)
            Select Case KeyAscii
                Case 48 To 57 '숫자 0-9 
                Case 8, 13   '백스페이스 캐리지 리턴

                Case 45  '마이너스 기호
                    ' 이 숫자는 오직 마이너스 기호만을 가질 수 있다.
                    ' 따라서 이미 하나를 가지고 있다면 하나는 버린다.
                    If InStr(r_o_text.Text, "-") <> 0 Then KeyAscii = 0

                    ' 삽입 지점이 0이 아닌 경우(필드의 시작이 아닌 경우)에는
                    ' 마이너스 기호를 버린다.(마이너스 기호는 맨 처음이 아니면 안되기 때문이다.)
                    If r_o_text.SelectionStart <> 0 Then KeyAscii = 0

                Case 46                 '소솟점 기호(마침표)
                    '소수점을 가지고 있다면, 버린다.
                    If InStr(r_o_text.Text, ".") <> 0 Then KeyAscii = 0

                Case Else
                    ' 다른키에 대해서는 처리를 하지 않는다
                    KeyAscii = 0
            End Select
            If KeyAscii = 0 Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        End Sub

    End Class
#End Region

#Region " 주민등록 번호 체크 : Class IDChk "
    Public Class IDChk
        Private mErrorMessage As String = ""

        Public ReadOnly Property ErrorMessagd() As String
            Get
                ErrorMessagd = mErrorMessage
            End Get
        End Property

        '-- 주민등록체크 
        Public Function IdNoChk(ByVal asIDNO As String) As Boolean
            Dim arrIDNO() As Char
            Dim arrIntIDNO(12) As Integer
            Dim intCnt As Integer

            Dim intChkDigit As Integer

            '-- [-]문자 제거
            asIDNO = asIDNO.Replace("-", "").Trim

            If Not asIDNO.Length.Equals(13) Then
                mErrorMessage = "잘못된 주민등록번호입니다."
                IdNoChk = False
                Exit Function
            End If

            '-- 1바이트의 글자로 나누어 정수형으로 변환
            arrIDNO = asIDNO.ToCharArray
            For intCnt = 0 To 12
                If Char.IsNumber(arrIDNO(intCnt)) = False Then
                    mErrorMessage = "잘못된 주민등록번호입니다."
                    IdNoChk = False
                    Exit Function
                End If

                arrIntIDNO(intCnt) = CInt(arrIDNO(intCnt).ToString)
            Next

            intChkDigit = arrIntIDNO(0) * 2 + arrIntIDNO(1) * 3 + arrIntIDNO(2) * 4 _
            + arrIntIDNO(3) * 5 + arrIntIDNO(4) * 6 + arrIntIDNO(5) * 7

            intChkDigit += arrIntIDNO(6) * 8 + arrIntIDNO(7) * 9 + arrIntIDNO(8) * 2 _
                         + arrIntIDNO(9) * 3 + arrIntIDNO(10) * 4 + arrIntIDNO(11) * 5

            intChkDigit = intChkDigit Mod 11
            intChkDigit = 11 - intChkDigit
            intChkDigit = intChkDigit Mod 10

            If Not intChkDigit.Equals(arrIntIDNO(12)) Then
                mErrorMessage = "잘못된 주민등록번호입니다."
                IdNoChk = False
                Exit Function
            End If

            IdNoChk = True

        End Function
    End Class
#End Region


#Region " 동적 어셈블리 호출 관련 : Class InvAs - add freety 2006/03/21"
    Public Class InvAs
        Private m_asmb_buf As Reflection.Assembly = Nothing
        Private m_type_buf As Type = Nothing
        Private m_objBuf As Object = Nothing

        Public Function InvokeMember(ByVal rsMethodNm As String, ByVal ra_objParam() As Object) As Object
            If m_objBuf Is Nothing Then m_objBuf = Activator.CreateInstance(m_type_buf)

            Dim a_methodinfo_buf() As Reflection.MethodInfo = m_type_buf.GetMethods()

            For Each methodinfo_buf As Reflection.MethodInfo In a_methodinfo_buf
                If methodinfo_buf.Name = rsMethodNm Then
                    Return m_type_buf.InvokeMember(rsMethodNm, Reflection.BindingFlags.InvokeMethod, Nothing, m_objBuf, ra_objParam)
                End If
            Next
        End Function

        Public Function InvokeMember(ByVal robjBuf As Object, ByVal rsMethodNm As String, ByVal ra_objParam() As Object) As Object
            If m_objBuf Is Nothing Then m_objBuf = Activator.CreateInstance(m_type_buf)

            Dim a_methodinfo_buf() As Reflection.MethodInfo = m_type_buf.GetMethods()

            For Each methodinfo_buf As Reflection.MethodInfo In a_methodinfo_buf
                If methodinfo_buf.Name = rsMethodNm Then
                    Return m_type_buf.InvokeMember(rsMethodNm, Reflection.BindingFlags.InvokeMethod, Nothing, robjBuf, ra_objParam)
                End If
            Next
        End Function

        Public Sub LoadAssembly(ByVal rsAsmbFileNm As String, ByVal rsAsmbNm As String)
            'Assembly Load
            m_asmb_buf = Reflection.Assembly.LoadFrom(rsAsmbFileNm)

            'Assembly Type
            m_type_buf = m_asmb_buf.GetType(rsAsmbNm)
        End Sub

        Public Function GetField(ByVal rsVarNm As String) As Object
            If m_objBuf Is Nothing Then m_objBuf = Activator.CreateInstance(m_type_buf)

            Dim fieldinfo_buf As Reflection.FieldInfo = m_type_buf.GetField(rsVarNm)

            If Not fieldinfo_buf Is Nothing Then
                Return fieldinfo_buf.GetValue(m_objBuf)
            Else
                Return Nothing
            End If
        End Function

        Public Function GetProperty(ByVal rsVarNm As String) As Object
            If m_objBuf Is Nothing Then m_objBuf = Activator.CreateInstance(m_type_buf)

            Dim propinfo_buf As Reflection.PropertyInfo = m_type_buf.GetProperty(rsVarNm)

            If Not propinfo_buf Is Nothing Then
                Return propinfo_buf.GetValue(m_objBuf, Nothing)
            Else
                Return Nothing
            End If
        End Function

        Public Sub SetField(ByVal rsVarNm As String, ByVal robjParam As Object)
            If m_objBuf Is Nothing Then m_objBuf = Activator.CreateInstance(m_type_buf)

            Dim fieldinfo_buf As Reflection.FieldInfo = m_type_buf.GetField(rsVarNm)

            If Not fieldinfo_buf Is Nothing Then
                fieldinfo_buf.SetValue(m_objBuf, robjParam)
            End If
        End Sub

        Public Sub SetProperty(ByVal rsVarNm As String, ByVal robjParam As Object)

            Try
                If m_objBuf Is Nothing Then m_objBuf = Activator.CreateInstance(m_type_buf)

                Dim propinfo_buf As Reflection.PropertyInfo = m_type_buf.GetProperty(rsVarNm)

                If Not propinfo_buf Is Nothing Then
                    propinfo_buf.SetValue(m_objBuf, robjParam, Nothing)
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

    End Class
#End Region

#Region " Enum 정의 "

    ' 입원, 외래구분
    Public Enum enumPatGbn
        외래 = 0
        입원 = 1
    End Enum

    ' 수평정렬
    Public Enum enumHAlign
        Left = 0
        Rignt = 1
        Center = 2
    End Enum

    ' 수직정렬
    Public Enum enumVAlign
        Top = 0
        Bottom = 1
        Mid = 2
    End Enum

    ' CodeHelp Type
    Public Enum enumCodeHelpFrm
        ShowOnly = -1   '- 데이타 보이기 전용
        Normal = 0      '- 기본형태 CodeHelp
        Check = 1       '- 체크박스형 CodeHelp
    End Enum

    Public Enum enumToggle
        RegnoToName = 0         '- 등록번호 <-> 성명
        BcnoToRegno = 1         '- 검체번호 <-> 등록번호
        ReportdtToRequestdt = 2 '- 보고일자 <-> 의뢰일자
        DondtToRegdt = 3        '- 헌혈일자 <-> 등록일자 
        IdnoToPName = 4         '- 주민등록번호 <-> 성명
        OrddtToJubsudt = 5      '- 처방일자 <-> 접수일자  
        TransfusionToRegno = 6  '- 수혈의뢰접수번호 <-> 등록번호
        OrddtToOutdt = 7        '- 처방일자 <-> 출고일자
        Regno_Name_Bcno = 8     '- 등록번호 -> 성명 -> 검체번호 -> 등록번호 ...
        OutdtToOrddt = 9        '- 출고일자 <-> 처방일자
        ReportdtToJubsudt = 10    '- 보고일자 <-> 접수일자
    End Enum

    Public Enum enumSectGbn
        혈액은행 = 0
        분자유전 = 1

        '-- 2007/10/30 ssh 원자력병원용.
        해부병리 = 2
        핵의학 = 3

        '-- 2008/01/08 yej 순천향부천병원
        TAT_OVERTIME = 9
    End Enum

    Public Enum enumPlusMinus
        Plus = 0
        Minus = 1
    End Enum

    Public Enum enumChkBox
        UnCheck = 0
        Check = 1
    End Enum

    Public Enum enumSIR
        R = 0
        I = 1
        S = 2
    End Enum

    Public Enum enumReportChk
        UnCheck = 0
        Check = 1
    End Enum

    ' 헌혈구분
    Public Enum enumDonGbn
        일반 = 0
        성분 = 1
        지정 = 2
        자가 = 3
    End Enum

    Public Enum enumBCPRT
        None = 0   ' 출력안함
        수동 = 1   ' 일반프린터
        자동 = 2   ' Autolabeler
    End Enum

    Public Enum enumCANCEL
        채혈접수취소 = 0
        채혈취소 = 1
        접수취소 = 2
        REJECT = 3
        BLOOD_REJECT = 4
        일괄채혈취소 = 5
        부적합검등록 = 6
    End Enum

    Public Enum enumTnsGbn
        Prep = 1
        수혈 = 2
        응급 = 3
        Irr = 4
    End Enum

    Public Enum enumBTest
        Abo_C = 1
        Rh = 2
        Abo_S = 3
        'CrossMatching_C = 4
        'CrossMatching_S = 5
        Screening_C = 6
        CrossMatching_P = 7
        CrossMatching_T = 8
        Irra = 9
    End Enum

    Public Enum enumSID
        LIS = 0
        OCS = 1
        LIS_MSSQL = 2

        LIS_LIVE = 0
        LIS_DEV1 = 1
        LIS_DEV2 = 2
    End Enum

    Public Enum enumImgPatGbn
        입원 = 0
        외래 = 1
        예약 = 2
    End Enum

    Public Enum enumJobGbn
        미채혈 = 0
        바코드 = 1
        채혈 = 2
        접수 = 3
        보고 = 4
    End Enum

    Public Enum enumBloodTest
        Ab_SCR = 6
        sHb = 0
        sPLT = 0
        saPTT = 0
        sPT_sec = 0
        sPT_percent = 0
        sPT_inr = 0
        sHct = 0
        sAb_ID = 0
    End Enum

#End Region

#Region " 채혈관리, 접수관리에서 미채혈, 채혈, 접수, 보고의 BackColor, ForeColor 설정 : Class JobColor"
    Public Class JobGbn
        Public Shared Function FrColor(ByVal aeJobGbn As enumJobGbn) As Color
            If aeJobGbn = enumJobGbn.미채혈 Then
                ' Red
                FrColor = System.Drawing.Color.FromArgb(255, 0, 128)

            ElseIf aeJobGbn = enumJobGbn.바코드 Then
                FrColor = System.Drawing.Color.Black

            ElseIf aeJobGbn = enumJobGbn.채혈 Then
                ' Gray
                FrColor = System.Drawing.Color.Black
            ElseIf aeJobGbn = enumJobGbn.접수 Then
                ' Green
                FrColor = System.Drawing.Color.FromArgb(0, 64, 0)
            ElseIf aeJobGbn = enumJobGbn.보고 Then
                ' Purple
                FrColor = System.Drawing.Color.FromArgb(0, 0, 94)
            End If
        End Function

        ' abAddContrast - False: Default 
        '                 True : 약간 진하게
        Public Shared Function BkColor(ByVal aeJobGbn As enumJobGbn, Optional ByVal abAddContrast As Boolean = False) As Color
            If aeJobGbn = enumJobGbn.미채혈 Then
                ' Red
                If abAddContrast = True Then BkColor = System.Drawing.Color.FromArgb(255, 227, 227) _
                                        Else BkColor = System.Drawing.Color.FromArgb(255, 234, 234)
            ElseIf aeJobGbn = enumJobGbn.바코드 Then
                ' Gray
                If abAddContrast = True Then BkColor = System.Drawing.Color.FromArgb(238, 238, 238) _
                                        Else BkColor = System.Drawing.Color.FromArgb(244, 244, 244)
            ElseIf aeJobGbn = enumJobGbn.채혈 Then
                ' Gray
                If abAddContrast = True Then BkColor = System.Drawing.Color.FromArgb(238, 238, 238) _
                                        Else BkColor = System.Drawing.Color.FromArgb(244, 244, 244)
            ElseIf aeJobGbn = enumJobGbn.접수 Then
                ' Green
                If abAddContrast = True Then BkColor = System.Drawing.Color.FromArgb(234, 249, 228) _
                                        Else BkColor = System.Drawing.Color.FromArgb(234, 255, 234)
            ElseIf aeJobGbn = enumJobGbn.보고 Then
                ' Purple
                If abAddContrast = True Then BkColor = System.Drawing.Color.FromArgb(234, 228, 249) _
                                        Else BkColor = System.Drawing.Color.FromArgb(234, 234, 255)
            End If
        End Function

    End Class

#End Region

#Region " 혈액은행 or 분자유전계의 Backcolor 설정 : Class COLOR_BCCLSCD"
    Public Class COLOR_BCCLSCD
        Public Shared Function FrColor(ByVal rsColorGbn As String) As Color
            Select Case rsColorGbn
                Case "1" : FrColor = Color.Black
                Case "2" : FrColor = Color.Black
                Case "3" : FrColor = Color.Black
                Case Else : FrColor = Color.Black
            End Select
        End Function

        Public Shared Function BkColor(ByVal rsColorGbn As String) As Color
            Select Case rsColorGbn
                Case "1" : BkColor = System.Drawing.Color.FromArgb(205, 200, 19)
                Case "2" : BkColor = System.Drawing.Color.LightSteelBlue
                Case "3" : BkColor = System.Drawing.Color.FromArgb(208, 82, 90)
                Case Else : BkColor = Color.White
            End Select
        End Function
    End Class

#End Region

#Region " Wave화일 Play : Class PlayWave "
    Public Class PlayWave
        Private Const sFile As String = "File : CGCOMMON01.vb, Class : PlayWave" & vbTab
        Declare Function waveOutGetNumDevs Lib "winmm.dll" () As Long
        Declare Function PlaySound Lib "winmm.dll" Alias "PlaySoundA" (ByVal lpszName As String, ByVal hModule As Long, ByVal dwFlags As Long) As Long

        Private Const SND_APPLICATION As Integer = &H80
        Private Const SND_ASYNC As Integer = &H1
        Private Const SND_FILENAME As Integer = &H20000
        Private Const SND_NODEFAULT As Integer = &H2

        Private HasSound As Boolean

        Private mTrd As System.Threading.Thread
        Private mWaveFile As String

        Public Function IsSoundSupported() As Boolean
            Dim sFn As String = "Public Function IsSoundSupported() As Boolean"
            Try
                If (waveOutGetNumDevs > 0) Then IsSoundSupported = True
            Catch ex As Exception
                'Fn.log(sFile & sFn, Err)
                'Throw (New Exception(ex.Message, ex))
            End Try
        End Function

        Public Sub Play(ByVal asWaveFile As String)
            Dim sFn As String = "Public Sub Play(ByVal sFile As String)"
            Try
                mWaveFile = asWaveFile
                mTrd = New System.Threading.Thread(AddressOf fnPlay)
                With mTrd
                    .IsBackground = True
                    .Start()
                End With

            Catch ex As Exception
                'Fn.log(sFile & sFn, Err)

            End Try

        End Sub

        Private Sub fnPlay()
            Dim sFn As String = "Private Sub fnPlay()"
            Try
                HasSound = IsSoundSupported()
                If HasSound Then
                    Call PlaySound(mWaveFile, 0, SND_FILENAME Or SND_NODEFAULT)
                Else
                    For intCtr As Integer = 0 To 3
                        Beep()
                    Next
                End If

            Catch ex As Exception
                'Fn.log(sFile & sFn, Err)
                'Throw (New Exception(ex.Message, ex))
            End Try
        End Sub

    End Class
#End Region

#Region " 이미지리스트에서 이미지 가져오기 : Class GetimgList "
    Public Class GetImgList
        Private Shared objImgFrm As New FGCOMMON01

        Public Shared Function getPlusMinus(ByVal aeImdex As enumPlusMinus) As Image
            getPlusMinus = objImgFrm.imlPlusMinus.Images(aeImdex)
        End Function

        Public Shared Function getChkBox(ByVal aeImdex As enumChkBox) As Image
            getChkBox = objImgFrm.imIChkBox.Images(aeImdex)
        End Function

        Public Shared Function getMultiRst() As Image
            getMultiRst = objImgFrm.imlMultiRst.Images(0)
        End Function

        Public Shared Function getSIR(ByVal aeImdex As enumSIR) As Image
            getSIR = objImgFrm.imlSIR.Images(aeImdex)
        End Function

        Public Shared Function getReportChk(ByVal aeImdex As enumReportChk) As Image
            getReportChk = objImgFrm.imlReportChk.Images(aeImdex)
        End Function

        Public Shared Function getSingleSel() As Image
            getSingleSel = objImgFrm.imlSingleSel.Images(0)
        End Function

        Public Shared Function getPatGbn(ByVal aeImdex As enumImgPatGbn) As Image
            getPatGbn = objImgFrm.imlPatGbn.Images(aeImdex)
        End Function

        Public Shared Function getImgOther(ByVal asImgAlias As String) As Image
            Select Case asImgAlias.ToUpper
                Case "BULLET"
                    Return objImgFrm.picBullet.Image

                Case "TXT"
                    Return objImgFrm.picTxt.Image

                Case "LEAF"
                    Return objImgFrm.picLeaf.Image
                Case Else
                    Return Nothing
            End Select
        End Function
    End Class
#End Region


End Namespace

