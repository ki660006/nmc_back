
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports LISAPP.APP_S
Imports LISAPP.APP_S.PatHisFn

Public Class FGS99

    Private Sub btnExcelOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelOpen.Click

        Dim xlsApp As Excel.Application = Nothing
        Dim xlsWkB As Excel.Workbook = Nothing
        Dim xlsWkS As Excel.Worksheet = Nothing

        If Dir("c:\실시비교", FileAttribute.Directory) = "" Then MkDir("c:\실시비교")

        ofdExLab.InitialDirectory = "c:\실시비교"

        ofdExLab.Filter = "Excel files (*.xls)|*.xls"
        ofdExLab.FilterIndex = 2            ' 파일 대화 상자에서 현재 선택한 필터의 인덱스를 가져오거나 설정
        ofdExLab.RestoreDirectory = True    ' 대화상자를 닫기전 대화상자에서 현재 디렉터리를 복원할지 여부를 나타내는 값을 가져오거나 설정



        If ofdExLab.ShowDialog() = DialogResult.OK Then
            xlsApp = CType(GetObject("", "Excel.Application"), Excel.Application)
            xlsWkB = xlsApp.Workbooks.Open(ofdExLab.FileName)

            xlsWkS = CType(xlsWkB.Sheets(1), Excel.Worksheet)
            Me.txtPath.Text = ofdExLab.FileName
            Dim alList As New ArrayList
            Dim intLine As Integer = 0

            Do While True

                intLine += 1

                Dim sYear As String = ""
                Try
                    sYear = xlsWkS.Range("A" + CStr(intLine)).Value.ToString()
                    If sYear = "년" Then
                        intLine += 1
                        sYear = xlsWkS.Range("A" + CStr(intLine)).Value.ToString()
                    End If
                Catch ex As Exception
                    Exit Do
                End Try


                Dim sYmd As String = xlsWkS.Range("B" + CStr(intLine)).Value.ToString
                Dim sPrepee As String = xlsWkS.Range("C" + CStr(intLine)).Value.ToString
                Dim sOrddt As String = xlsWkS.Range("D" + CStr(intLine)).Value.ToString
                Dim sRegno As String = xlsWkS.Range("E" + CStr(intLine)).Value.ToString
                Dim sDept As String = xlsWkS.Range("F" + CStr(intLine)).Value.ToString
                Dim sDoctor As String = xlsWkS.Range("G" + CStr(intLine)).Value.ToString
                Dim sTordcd As String = xlsWkS.Range("H" + CStr(intLine)).Value.ToString
                Dim sOrdnm As String = xlsWkS.Range("I" + CStr(intLine)).Value.ToString
                Dim sCfmdept As String = xlsWkS.Range("J" + CStr(intLine)).Value.ToString
                'Dim sExecYn As String = xlsWkS.Range("K" + CStr(intLine)).Value.ToString
                'Dim sExecDt As String = xlsWkS.Range("L" + CStr(intLine)).Value.ToString

                If sYear Is Nothing Then Exit Do
                If sYmd Is Nothing Then sYmd = ""
                If sPrepee Is Nothing Then sPrepee = ""
                If sOrddt Is Nothing Then sOrddt = ""
                If sRegno Is Nothing Then sRegno = ""
                If sDept Is Nothing Then sDept = ""
                If sDoctor Is Nothing Then sDoctor = ""
                If sTordcd Is Nothing Then sTordcd = ""
                If sOrdnm Is Nothing Then sOrdnm = ""
                If sCfmdept Is Nothing Then sCfmdept = ""
                'If sExecYn Is Nothing Then sExecYn = ""
                'If sExecDt Is Nothing Then sExecDt = ""

                Dim objExcel As New ExcelInfo

                objExcel.Year = sYear
                objExcel.Ymd = sYmd
                objExcel.Prepee = sPrepee
                objExcel.Orddt = sOrddt
                objExcel.Regno = sRegno
                objExcel.Dept = sDept
                objExcel.Doctor = sDoctor
                objExcel.Tordcd = sTordcd
                objExcel.Ordnm = sOrdnm
                objExcel.Cfmdept = sCfmdept
                'objExcel.ExecYn = sExecYn
                'objExcel.ExecDt = sExecDt

                If sCfmdept = "진단검사의학과" Then
                    alList.Add(objExcel)
                End If
            Loop

            If alList.Count > 0 Then
                For ix As Integer = 0 To alList.Count - 1
                    With spdExcel
                        .MaxRows = ix + 1
                        .Row = ix + 1
                        .Col = .GetColFromID("year") : .Text = CType(alList(ix), ExcelInfo).Year
                        .Col = .GetColFromID("ymd") : .Text = CType(alList(ix), ExcelInfo).Ymd
                        .Col = .GetColFromID("prepee") : .Text = CType(alList(ix), ExcelInfo).Prepee
                        .Col = .GetColFromID("orddt") : .Text = CType(alList(ix), ExcelInfo).Orddt
                        .Col = .GetColFromID("regno") : .Text = CType(alList(ix), ExcelInfo).Regno
                        .Col = .GetColFromID("dept") : .Text = CType(alList(ix), ExcelInfo).Dept
                        .Col = .GetColFromID("doctor") : .Text = CType(alList(ix), ExcelInfo).Doctor
                        .Col = .GetColFromID("tordcd") : .Text = CType(alList(ix), ExcelInfo).Tordcd
                        .Col = .GetColFromID("ordnm") : .Text = CType(alList(ix), ExcelInfo).Ordnm
                        .Col = .GetColFromID("cfmdept") : .Text = CType(alList(ix), ExcelInfo).Cfmdept
                        '.Col = .GetColFromID("execyn") : .Text = CType(alList(ix), ExcelInfo).ExecYn
                        '.Col = .GetColFromID("execdt") : .Text = CType(alList(ix), ExcelInfo).ExecDt

                    End With

                Next

            End If


        End If

        'SbSearch()


    End Sub

    Private Sub SbSearch()
        Try
            Dim sRegno As String = ""
            Dim sOrddt As String = ""
            Dim sTordCd As String = ""
            Dim sDept As String = ""
            With Me.spdExcel
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("regno") : sRegno = .Text
                    .Col = .GetColFromID("orddt") : sOrddt = .Text
                    .Col = .GetColFromID("tordcd") : sTordCd = .Text
                    .Col = .GetColFromID("dept") : sDept = .Text
                    Dim dt As DataTable = fnGet_EmrvsLis_state(sRegno, sOrddt, sTordCd, sDept)

                    If dt.Rows.Count = 1 Then
                        .Col = .GetColFromID("ordstat") : .Text = dt.Rows(0).Item("prcpstatcd").ToString
                        .Col = .GetColFromID("spcflg") : .Text = dt.Rows(0).Item("spcflg").ToString
                        .Col = .GetColFromID("rstflg") : .Text = dt.Rows(0).Item("rstflg").ToString

                    ElseIf dt.Rows.Count = 0 Then
                        .Col = .GetColFromID("ordstat") : .Text = "데이터없음"
                        .Col = .GetColFromID("spcflg") : .Text = "데이터없음"
                        .Col = .GetColFromID("rstflg") : .Text = "데이터없음"
                    Else
                        .Col = .GetColFromID("ordstat") : .Text = "오류"
                        .Col = .GetColFromID("spcflg") : .Text = "오류"
                        .Col = .GetColFromID("rstflg") : .Text = "오류"
                    End If

                Next

            End With
        Catch ex As Exception
            MsgBox("매칭오류")

        End Try
        
    End Sub

    Private Sub btnMatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMatch.Click
        SbSearch()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            Dim sFilenm As String = Me.txtPath.Text

            sFilenm = sFilenm.Substring(0, sFilenm.Length - 4)
            With spdExcel
                .ReDraw = False

                .Row = 1
                .MaxRows += 1
                .Action = FPSpreadADO.ActionConstants.ActionInsertRow

                For intCol As Integer = 1 To .MaxCols
                    .Row = 0 : .Col = intCol : Dim strTmp As String = .Text
                    .Row = 1 : .Col = intCol : .Text = strTmp
                Next

                If spdExcel.MaxRows < 1 Then MsgBox("조회후 출력이 가능합니다.", MsgBoxStyle.Information, Me.Text)
                If spdExcel.ExportToExcel(sFilenm + "_변환후.xls", "List", "") Then Process.Start(sFilenm + "_변환후.xls")

                .Row = 1
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1

                .ReDraw = True
            End With
        Catch ex As Exception

        End Try

        


    End Sub
End Class

Public Class ExcelInfo
    Public Year As String = ""
    Public Ymd As String = ""
    Public Prepee As String = ""
    Public Orddt As String = ""
    Public Regno As String = ""
    Public Dept As String = ""
    Public Doctor As String = ""
    Public Tordcd As String = ""
    Public Ordnm As String = ""
    Public Cfmdept As String = ""
    Public ExecYn As String = ""
    Public ExecDt As String = ""
End Class