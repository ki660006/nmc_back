Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing

Namespace ComFN

    Public Class Fn

        ' Error 로그
        Public Shared Sub Log(ByVal rsLog As String)
            Dim sDir As String = Environment.CurrentDirectory + "\ErrLog"
            Dim sFile As String = sDir + "\Err" + Format(Now, "yyyy-MM-dd") + ".txt"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(Now())
            sw.WriteLine(vbTab + rsLog)
            sw.Close()

        End Sub

        ' Error 로그
        Public Shared Sub Log(ByVal rsLog As String, ByVal e As ErrObject)
            Dim sDir As String = Environment.CurrentDirectory + "\ErrLog"
            Dim sFile As String = sDir + "\Err" + Format(Now, "yyyy-MM-dd") + ".txt"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(Now())
            sw.WriteLine(rsLog)

            sw.WriteLine(vbTab + "Err Number : " + e.Number.ToString())
            sw.WriteLine(vbTab + "Err Description : " + e.Description)

            sw.Close()

        End Sub

        Public Shared Sub ExclamationErrMsg(ByVal e As ErrObject, ByVal rsTitle As String, Optional ByVal rsErrMsg As String = "")
            Dim sErrMsg As String = ""

            If rsErrMsg <> "" Then sErrMsg = rsErrMsg & vbCrLf
            sErrMsg += e.Description

            MsgBox(sErrMsg, MsgBoxStyle.Exclamation, rsTitle + " - 오류번호:" & Err.Number.ToString)
        End Sub

        ' Error 로그
        Public Shared Sub LogFile(ByVal rsLog As String, ByVal rsFileNm As String, Optional ByVal rsPath As String = "")
            Dim sDir As String = Environment.CurrentDirectory + IIf(rsPath = "", "", "\").ToString() + rsPath
            Dim sFile As String = sDir + "\" + rsFileNm & ".txt"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(rsLog)
            sw.Close()

        End Sub


        ' Error 로그 ArrayList
        Public Shared Sub log(ByVal r_al_Log As ArrayList, ByVal e As ErrObject)
            Dim sDir As String = Environment.CurrentDirectory + "\ERRLog"
            Dim sFile As String = sDir + "\ERR" + Format(Now, "yyyy-MM-dd") + ".txt"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(Now())

            For ix = 0 To r_al_Log.Count - 1
                sw.WriteLine(vbTab + r_al_Log.Item(ix).ToString.ToLower.Replace("from", vbCrLf + vbTab & "from").Replace("select", vbCrLf + vbTab + "select").Replace("where", vbCrLf + vbTab + "where") + vbCrLf)
            Next
            sw.WriteLine(vbTab + "Err Number : " + e.Number.ToString)
            sw.WriteLine(vbTab + "Err Description : " + e.Description)

            sw.Close()

        End Sub


        '-- 컨트롤의 X, Y위치 반환
        Public Shared Function CtrlLocationXY(ByVal aoControl As Control) As System.Drawing.Point
            Dim objCtrl As Control = CType(aoControl, Control)
            Dim PointXY As Point

            '-- 특정 Control의 상위 컨트롤 알아내기.
            Do While Not objCtrl Is Nothing
                'Debug.WriteLine(objCtrl.Name() & ", " & objCtrl.Left.ToString & ", " & objCtrl.Top.ToString)

                PointXY.X += objCtrl.Left
                PointXY.Y += objCtrl.Top

                objCtrl = objCtrl.Parent()
            Loop

            If PointXY.X < 0 Then PointXY.X = 0
            If PointXY.Y < 0 Then PointXY.Y = 0

            CtrlLocationXY = PointXY
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
                        End If
                    End If

                    If bReturn Then
                        Return bReturn
                    End If
                Next

                Return bReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try
        End Function


    End Class

End Namespace
