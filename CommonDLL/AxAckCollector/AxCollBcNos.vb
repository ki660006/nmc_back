Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports PRTAPP.APP_BC

Public Class AxCollBcNos
    Private Const mc_iHeight As Integer = 26

    Private m_prtparams As AxAckPrinterSetting.PrinterParams

    Private msFldSep As String = CStr(Chr(32))
    'Private msFldSep As String = CStr(Chr(3))
    Private msFind_TNmBp As String = ""

    Private miMaxLenCmt As Integer = 34

    Private msSymbolMore As String = " ..."



    Private msXmlDir As String = System.Windows.Forms.Application.StartupPath & "\XML"
    Private msXmlFile As String = ""
    Private miSelPRTID As Integer = 0
    Private mlPRTInfo As New ArrayList
    Private msLoadFrm As String = ""

    'Private Class BcPrtItem
    '    Public STX As String = ""
    '    Public PrtBcNo As String = ""
    '    Public RegNo As String = ""
    '    Public PatNm As String = ""
    '    Public SexAge As String = ""
    '    Public TSectGbn As String = ""
    '    Public DeptWard As String = ""
    '    Public OrdDay As String = ""
    '    Public IOFlg As String = ""
    '    Public BcNo As String = ""
    '    Public HRegNo As String = ""
    '    Public TkDt As String = ""
    '    Public OrdPart As String = ""
    '    Public TubeNm As String = ""
    '    Public RePrt As String = ""
    '    Public Cmt As String = ""
    '    Public ETC1 As String = ""
    '    Public ETC2 As String = ""
    '    Public ETC3 As String = ""
    '    Public ETC4 As String = ""  '검사그룹 
    '    Public ETC5 As String = ""  'Cross Matching 항목 체크 
    '    Public ETC6 As String = ""  'Remark
    '    Public EndCd As String = ""
    'End Class

    Private Function fnFind_BcPrtItem(ByVal r_listcollData As List(Of STU_CollectInfo), ByVal rbReprint As Boolean) As STU_BCPRTINFO
        Dim sFn As String = "Private Function fnFind_BcPrtItem(List(Of STU_CollectInfo), Boolean) As String"

        Try
            Dim bpi As New STU_BCPRTINFO

            With bpi
                .BCNOPRT = r_listcollData.Item(0).PRTBCNO
                .REGNO = r_listcollData.Item(0).REGNO.Trim
                .PATNM = r_listcollData.Item(0).PATNM.Trim
                .SEXAGE = r_listcollData.Item(0).SEX.Trim + "/" + r_listcollData.Item(0).AGE.Trim
                .BCCLSCD = r_listcollData.Item(0).BCCLSCD

                Dim ABOCHK As String = OCSAPP.OcsLink.SData.fnget_ABO(r_listcollData.Item(0).REGNO)
                .ABOCHK = ABOCHK

                If r_listcollData.Item(0).IOGBN = "O" Then
                    .DEPTWARD = r_listcollData.Item(0).DEPTABBR.Trim
                Else
                    .DEPTWARD = r_listcollData.Item(0).WARDABBR.Trim + "/" + r_listcollData.Item(0).ROOMNO.Trim
                End If

                .IOGBN = r_listcollData.Item(0).IOGBN
                .BCNO = Fn.BCNO_View(r_listcollData.Item(0).BCNO)
                .HREGNO = r_listcollData.Item(0).HREGNO.Trim
                .TUBENM = r_listcollData.Item(0).TUBENMBP.Trim

                Dim sTNmBP As String = ""
                Dim sTmpTgrpnm As String = ""

                If .BCCLSCD = PRG_CONST.BCCLS_BldCrossMatch Then
                    sTNmBP = r_listcollData.Item(0).TNMBP.Trim + msFldSep + r_listcollData.Count.ToString + "unit(s)"
                Else
                    For r As Integer = 1 To r_listcollData.Count
                        Dim collData As STU_CollectInfo = CType(r_listcollData(r - 1), STU_CollectInfo)

                        Dim sTNmOne As String = collData.TNMBP.Trim.Trim

                        If sTNmOne.IndexOf(">") >= 0 Then
                            sTNmOne = sTNmOne.Substring(0, sTNmOne.IndexOf(">")).Trim
                        End If

                        If sTNmOne.IndexOf("<") >= 0 Then
                            sTNmOne = sTNmOne.Substring(sTNmOne.IndexOf("<") + 1).Trim
                        End If

                        If sTNmBP.Length > 0 Then sTNmBP += msFldSep

                        If Fn.LengthH(sTNmBP + sTNmOne) > miMaxLenCmt - msSymbolMore.Length Then
                            If r = r_listcollData.Count Then
                                If Fn.LengthH(sTNmBP + sTNmOne) > miMaxLenCmt Then
                                    sTNmBP = sTNmBP.Trim + msSymbolMore
                                Else
                                    sTNmBP += sTNmOne
                                End If
                            Else
                                sTNmBP = sTNmBP.Trim + msSymbolMore
                            End If

                            Exit For
                        Else
                            sTNmBP += sTNmOne
                        End If

                        If collData.TGRPNM <> "" Then
                            If sTmpTgrpnm.IndexOf(collData.TGRPNM) < 0 Then
                                sTmpTgrpnm += collData.TGRPNM
                            End If
                        End If
                    Next
                End If

                .TESTNMS = sTNmBP

                Dim sStat As String = ""

                For r As Integer = 1 To r_listcollData.Count
                    sStat = r_listcollData.Item(r - 1).STATGBN

                    If sStat <> "" Then Exit For
                Next

                '응급(1) 
                .EMER = sStat

                If .BCCLSCD.StartsWith("P") Then
                    '검체명(10)
                    .SPCNM = r_listcollData.Item(0).SPCNMBP
                Else
                    '검체명(10)
                    .SPCNM = Fn.PadRightH(r_listcollData.Item(0).SPCNMBP, 10)
                End If

                '감염정보(10)
                .INFINFO = r_listcollData.Item(0).INFINFO

                '기타4 -> 검사그룹(12)
                .TGRPNM = sTmpTgrpnm

                '.ETC5 = Fn.PadRightH(r_listcollData.Item(0).BPGBN, 1)
                For ix As Integer = 0 To r_listcollData.Count - 1
                    Dim sBcCnt As String = r_listcollData.Item(ix).BCCNT

                    If sBcCnt = "A" Then sBcCnt = "2"

                    If sBcCnt <> "" Then
                        If .BCCNT = "B" Then
                        ElseIf Val(sBcCnt) > Val(.BCCNT) Then
                            .BCCNT = r_listcollData.Item(ix).BCCNT
                        End If
                    End If
                Next

                .REMARK = r_listcollData.Item(0).REMARK.Trim

            End With

            Return bpi

        Catch ex As Exception
            Me.txtBcNos.Text = sFn + " - " + ex.Message

            Return Nothing

        End Try
    End Function

     Private Function fnFind_Matched_TNmBp(ByVal r_buf As STU_CollectInfo) As Boolean
        If r_buf.TNMBP.ToUpper.Replace(">", " ").Replace("<", " ") = msFind_TNmBp.ToUpper.Replace(">", " ").Replace("<", " ") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public UseEndocrine As Boolean = False
    Public UseSeparated As Boolean = False

    Public Event OnQueryCollectBc(ByRef riReprtMode As Integer)
    Public Event OnQuerySeparatedBc(ByRef riReprtMode As Integer)

    Public Property BcPrinterParams() As AxAckPrinterSetting.PrinterParams
        Get
            Return m_prtparams
        End Get

        Set(ByVal value As AxAckPrinterSetting.PrinterParams)
            m_prtparams = value
        End Set
    End Property

    Public Sub Clear()
        Me.lblBcNOsCnt.Text = ""
        Me.txtBcNos.Text = ""
    End Sub

    Public Sub DisplayBarcode(ByVal r_al_BcNos As ArrayList)
        Dim sFn As String = "Public Sub DisplayBarcode(ArrayList)"

        Try
            Clear()

            Dim sBcNos As String = ""
            Dim iBcTotCnt As Integer = 0

            For i As Integer = 1 To r_al_BcNos.Count
                Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_BcNos(i - 1), List(Of STU_CollectInfo))

                Dim sBcNo As String = listcollData.Item(0).BCNO

                If sBcNos.Length > 0 Then sBcNos += ", "
                sBcNos += sBcNo.Replace("-", "")

                iBcTotCnt += 1
            Next

            Me.lblBcNOsCnt.Text = iBcTotCnt.ToString + "장"
            Me.txtBcNos.Text = sBcNos

        Catch ex As Exception
            Me.txtBcNos.Text = sFn + " - " + ex.Message

        End Try
    End Sub

    Public Sub PrintBarcode_NotSuNab(ByVal r_al_BcNos As ArrayList, ByVal rsPrinterName As String)
        Dim sFn As String = "Public Sub PrintBarcode_NotSuNab(ArrayList)"

        Try
            Clear()

            Dim alPrtData As New ArrayList
            Dim sBcNos As String = ""

            For ix As Integer = 0 To r_al_BcNos.Count - 1
                Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_BcNos(ix), List(Of STU_CollectInfo))
                Dim bpi As STU_BCPRTINFO = fnFind_BcPrtItem(listcollData, False)

                alPrtData.Add(bpi)

                If ix > 0 Then sBcNos += ","
                sBcNos += listcollData(0).BCNO
            Next

            Dim bReturn As Boolean = False

            Call (New BCPrinter(msLoadFrm)).PrintDo(alPrtData, True, rsPrinterName)

            Me.lblBcNOsCnt.Text = alPrtData.Count.ToString + "장"
            Me.txtBcNos.Text = sBcNos.Trim()

        Catch ex As Exception
            Me.txtBcNos.Text = sFn + " - " + ex.Message

        End Try
    End Sub

    Private Sub PrintBarcode(ByVal r_al_BcNos As ArrayList, ByVal rsPrinterName As String, ByVal rbFirst As Boolean)

        Dim sFn As String = "Public Sub PrintBarcode(ArrayList)"

        Try
            Clear()

            Dim alPrtData As New ArrayList
            Dim sBcNos As String = ""

            For ix As Integer = 0 To r_al_BcNos.Count - 1
                Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_BcNos(ix), List(Of STU_CollectInfo))
                Dim bpi As STU_BCPRTINFO = fnFind_BcPrtItem(listcollData, False)

                alPrtData.Add(bpi)

                If ix > 0 Then sBcNos += ","
                sBcNos += listcollData(0).BCNO
            Next

            Dim bReturn As Boolean = False

            Call (New BCPrinter(msLoadFrm)).PrintDo(alPrtData, rbFirst, rsPrinterName)

            Me.lblBcNOsCnt.Text = alPrtData.Count.ToString + "장"
            Me.txtBcNos.Text = sBcNos.Trim()

        Catch ex As Exception
            Me.txtBcNos.Text = sFn + " - " + ex.Message

        End Try
    End Sub

    Private Sub PrintBarcode_pis(ByVal r_al_BcNos As ArrayList, ByVal rsPrinterName As String, ByVal rbFirst As Boolean)

        Dim sFn As String = "Public Sub PrintBarcode_pis(ArrayList)"

        Try
            Clear()

            Dim alPrtData As New ArrayList
            Dim sBcNos As String = ""

            For ix As Integer = 0 To r_al_BcNos.Count - 1
                Dim listcollData As List(Of STU_CollectInfo) = CType(r_al_BcNos(ix), List(Of STU_CollectInfo))
                Dim bpi As STU_BCPRTINFO = fnFind_BcPrtItem(listcollData, False)

                alPrtData.Add(bpi)

                If ix > 0 Then sBcNos += ","
                sBcNos += listcollData(0).BCNO
            Next

            Dim bReturn As Boolean = False

            Call (New BCPrinter(msLoadFrm)).PrintDo_pis(alPrtData, rbFirst, rsPrinterName)

            Me.lblBcNOsCnt.Text = alPrtData.Count.ToString + "장"
            Me.txtBcNos.Text = sBcNos.Trim()

        Catch ex As Exception
            Me.txtBcNos.Text = sFn + " - " + ex.Message

        End Try
    End Sub

    Public Sub PrintBarcode(ByVal r_al_BcNos As ArrayList, ByVal rsLoadFrm As String, ByVal rsPrinterName As String, ByVal rbFirst As Boolean)

        msLoadFrm = rsLoadFrm

        PrintBarcode(r_al_BcNos, rsPrinterName, rbFirst)
    End Sub

    Public Sub PrintBarcode_pis(ByVal r_al_BcNos As ArrayList, ByVal rsLoadFrm As String, ByVal rsPrinterName As String, ByVal rbFirst As Boolean)

        msLoadFrm = rsLoadFrm

        PrintBarcode_pis(r_al_BcNos, rsPrinterName, rbFirst)
    End Sub

    '<----- Control Event ----->
    Private Sub AxCollBcNos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Height > mc_iHeight Then
            Me.Height = mc_iHeight
        End If
    End Sub
End Class


Public Class clsPRTInfo
    Public PRTID As String = ""
    Public PRTNM As String = ""
    Public OUTIP As String = ""
    Public OUTPORT As String = ""
    Public SUPPORTIP As String = ""
    Public SELECTED As String = ""
    Public IOPORT As String = ""
    Public LEFTMARGIN As String = ""
    Public TOPMARGIN As String = ""

    Public Sub New()
        MyBase.new()
    End Sub
End Class