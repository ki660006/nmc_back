Imports COMMON.CommFN
Imports COMMON.CommPrint
Imports Microsoft.Win32

Public Class BCPRT
    Private Const mcFile As String = "File : BCPRT.vb, Class : BCPRT" + vbTab

    Public BCPRINTERS As ArrayList

    Public Sub New()
        BCPRINTERS = New ArrayList

        Dim objCFG(4) As BCPRINTER_CFG

        objCFG(0) = New BCPRINTER_CFG

        With objCFG(0) 
            .PrinterID = 0
            .PrinterName = "사용안함".PadRight(20, " "c)
            .SupportTCPIP = True
            .PortNo = 13734 
            BCPRINTERS.Add(objCFG(0))
        End With

        objCFG(1) = New BCPRINTER_CFG

        With objCFG(1)
            .PrinterID = 1
            .PrinterName = "일반프린트".PadRight(20, " "c)
            .SupportTCPIP = True
        End With

        BCPRINTERS.Add(objCFG(1))

        objCFG(2) = New BCPRINTER_CFG

        With objCFG(2)
            .PrinterID = 2
            .PrinterName = "SATO(CT400)".PadRight(20, " ")
            .SupportTCPIP = True
        End With

        BCPRINTERS.Add(objCFG(2))

        objCFG(3) = New BCPRINTER_CFG

        With objCFG(3)
            .PrinterID = 3
            .PrinterName = "SATO(CL408)".PadRight(20, " ")
            .SupportTCPIP = True
        End With

        BCPRINTERS.Add(objCFG(3))

        objCFG(4) = New BCPRINTER_CFG

        With objCFG(4)
            .PrinterID = 4
            .PrinterName = "LUKHAN".PadRight(20, " ")
            .SupportTCPIP = True
        End With

        BCPRINTERS.Add(objCFG(4))

    End Sub

    Public Function BarCodePrtOut(ByVal ra_PrtData As ArrayList, ByVal riPrinterID As Integer, _
                                  ByVal rsPrintPort As String, ByVal rsSocketIP As String, ByVal rbFirst As Boolean, _
                                  Optional ByVal riLeftMargin As Integer = 0, _
                                  Optional ByVal riTopMargin As Integer = 0, _
                                  Optional ByVal rsBarType As String = "CODABA") As Boolean

        Dim sFn As String = "BarCodePrtOut"

        Try
            Dim objBC As New Object

            Select Case riPrinterID
                Case 0
                    Return True
                Case 1
                    objBC = New PPRINT
                Case 2
                    objBC = New SATO
                Case 3
                    objBC = New SATO_CL408
                Case 4
                    objBC = New LUKHAN_DRV
            End Select

            If riPrinterID = 2 Then
                If objBC.BarCodePrtOut(ra_PrtData, rsPrintPort, rsSocketIP, rbFirst, riLeftMargin, riTopMargin, rsBarType) Then
                    Return True
                Else
                    Return False
                End If
            Else
                If objBC.BarCodePrtOut(ra_PrtData, rsPrintPort, rsSocketIP, rbFirst, riLeftMargin, riTopMargin, rsBarType) Then
                    Return True
                Else
                    Return False
                End If
            End If
          

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    Public Function BarCodePrtOut_ris(ByVal ra_PrtData As ArrayList, ByVal riPrinterID As Integer, _
                                  ByVal rsPrintPort As String, ByVal rsSocketIP As String, ByVal rbFirst As Boolean, _
                                  Optional ByVal riLeftMargin As Integer = 0, _
                                  Optional ByVal riTopMargin As Integer = 0, _
                                  Optional ByVal rsBarType As String = "CODABA") As Boolean

        Dim sFn As String = "BarCodePrtOut_ris"

        Try
            Dim objBC As New Object

            Select Case riPrinterID
                Case 0
                    Return True
                Case 1
                    objBC = New PPRINT
                Case 2
                    objBC = New SATO
                Case 3
                    objBC = New SATO_CL408
                Case 4
                    objBC = New LUKHAN_DRV
            End Select

            If objBC.BarCodePrtOut_RIS(ra_PrtData, rsPrintPort, rsSocketIP, rbFirst, riLeftMargin, riTopMargin, rsBarType) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
            Return False
        End Try
    End Function

    Public Function BarCodePrtOut_Blood(ByVal ra_PrtData As ArrayList, ByVal riCopy As Integer, ByVal riPrinterID As Integer, _
                                        ByVal rsPrintPort As String, ByVal rsSocketIP As String, _
                                        Optional ByVal riLeftMargin As Integer = 0, _
                                        Optional ByVal riTopMargin As Integer = 0) As Boolean


        Dim sFn As String = "BarCodePrtOut_Blood"

        Try
            Dim objBC As New Object

            Select Case riPrinterID
                Case 0
                    Return True
                Case 1
                    objBC = New PPRINT
                Case 2
                    objBC = New SATO
                Case 3
                    objBC = New SATO_CL408
                Case 4
                    objBC = New LUKHAN_DRV

            End Select

            If objBC.BarCodePrtOut_BLD(ra_PrtData, riCopy, rsSocketIP, rsPrintPort, riLeftMargin, riTopMargin) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try

    End Function

    Public Function BarCodePrtOut_Micro(ByVal ra_PrtData As ArrayList, ByVal riPrinterID As Integer, _
                                        ByVal rsPrintPort As String, ByVal rsSocketIP As String, _
                                        Optional ByVal riLeftMargin As Integer = 0, _
                                        Optional ByVal riTopMargin As Integer = 0, _
                                        Optional ByVal rsBarType As String = "CODABA") As Boolean

        Dim sFn As String = "BarCodePrtOut"
        Dim objBC As New Object

        'Try

        '    Select Case riPrinterID
        '        Case 0
        '            Return True
        '        Case 1
        '            objBC = New PPRINT
        '        Case 2
        '            objBC = New SATO
        '            'Case 3
        '            '    objBC = New EX2
        '            'Case 4
        '            '    objBC = New ALLEGRO2
        '    End Select

        '    If objBC.BarCodePrtOut_Micro(ra_PrtData, rsPrintPort, rsSocketIP, riLeftMargin, riLeftMargin) Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'Catch ex As Exception
        '    Fn.log(mcFile + sFn, Err)
        '    MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        '    Return False
        'End Try
    End Function

    Public Function BarCodePrtOut_PIS(ByVal ra_PrtData As ArrayList, ByVal riPrinterID As Integer, _
                                 ByVal rsPrintPort As String, ByVal rsSocketIP As String, ByVal rbFirst As Boolean, _
                                 Optional ByVal riLeftMargin As Integer = 0, _
                                 Optional ByVal riTopMargin As Integer = 0, _
                                 Optional ByVal rsBarType As String = "CODABA") As Boolean

        Dim sFn As String = "BarCodePrtOut"

        'Try
        '    Dim objBC As New Object

        '    Select Case riPrinterID
        '        Case 0
        '            Return True
        '        Case 1
        '            objBC = New PPRINT
        '        Case 2
        '            objBC = New SATO
        '            'Case 2
        '            '    objBC = New LUKHAN_DRV
        '            'Case 3
        '            '    objBC = New EX2
        '            'Case 4
        '            '    objBC = New ALLEGRO2
        '    End Select

        '    If objBC.BarCodePrtOut_pis(ra_PrtData, rsPrintPort, rsSocketIP, rbFirst, riLeftMargin, riTopMargin, rsBarType) Then
        '        Return True
        '    Else
        '        Return False
        '    End If

        'Catch ex As Exception
        '    Fn.log(mcFile + sFn, Err)
        '    MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        '    Return False
        'End Try
    End Function

End Class

Public Class BCPRINTER_CFG
    Public PrinterID As Integer
    Public PrinterName As String
    Public SupportTCPIP As Boolean
    Public PortNo As Integer
    Public IOPort As String
    Public LEFTMargin As Integer

    Public Sub New()

    End Sub
End Class

Public Class BarPrtParams
    Public IPAddress As String = ""
    Public PortNo As String = ""
    Public PrinterDriver As String = ""
    Public PrinterName As String = ""
    Public PrinterPort As String = ""
    Public ShareName As String = ""
End Class

Public Class Print_Set
    Private m_printerparams As New BarPrtParams

    Public Function fnGet_PrinterParams_Shared(ByVal rsPrintName As String, ByRef rbTcpIP As Boolean) As BarPrtParams
        Dim sFn As String = "Function fnGet_PrinterParams_Shared(String) as BarPrtParams"

        Try
            'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\LanMan Print Services\Servers\10.5.3.57\Printers\SATO CL408e

            If rsPrintName.Split(CChar("\")).Length <> 4 Then
                'MsgBox("프린터 이름에 오류가 있습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)
                Return fnGet_PrinterParams_Local_TcpIp(rsPrintName, rbTcpIP)
            End If

            Dim sComputerNm As String = rsPrintName.Split(CChar("\"))(2)
            Dim sPrinterNm As String = rsPrintName.Split(CChar("\"))(3)
            Dim sPath As String = ""

            sPath += "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\LanMan Print Services\Servers\"
            sPath += sComputerNm + "\Printers\" + sPrinterNm

            Dim rk As RegistryKey = Registry.LocalMachine.OpenSubKey(sPath)

            If rk Is Nothing Then
                'WIN98에 프린터를 공유할 경우
                'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\,,e06n01,SATO
                sPath = ""
                sPath += "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\" + rsPrintName.Replace("\", ",")

                rk = Registry.LocalMachine.OpenSubKey(sPath)

                If rk Is Nothing Then
                    MsgBox("프린터 정로를 읽을 수 없습니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

                    m_printerparams.IPAddress = ""
                    m_printerparams.PortNo = ""
                    m_printerparams.PrinterDriver = ""
                    m_printerparams.PrinterName = rsPrintName
                    m_printerparams.PrinterPort = ""
                    m_printerparams.ShareName = ""

                    Return Nothing
                Else
                    m_printerparams.IPAddress = sComputerNm
                    m_printerparams.PortNo = ""
                    m_printerparams.PrinterDriver = rk.GetValue("Printer Driver").ToString()
                    m_printerparams.PrinterName = rsPrintName
                    m_printerparams.PrinterPort = rk.GetValue("Port").ToString()
                    m_printerparams.ShareName = sPrinterNm
                End If
            Else
                m_printerparams.IPAddress = sComputerNm
                m_printerparams.PortNo = ""
                m_printerparams.PrinterDriver = rk.GetValue("Printer Driver").ToString()
                m_printerparams.PrinterName = rsPrintName
                m_printerparams.PrinterPort = rk.GetValue("Port").ToString()
                m_printerparams.ShareName = rk.GetValue("Share Name").ToString()
            End If

            Return m_printerparams

        Catch ex As Exception
            MsgBox(sFn + " : " + ex.Message)

            Return Nothing

        End Try
    End Function

    Private Function fnGet_PrinterParams_Local_TcpIp(ByVal rsPrintName As String, ByRef rbTcpIpYn As Boolean) As BarPrtParams

        Dim sFn As String = "fnGet_PrinterParams_Local_TcpIp(String) as BarPrtPrams"

        Try
            'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\HP Color LaserJet 3550

            Dim sPrinterNm As String = rsPrintName
            Dim sPath As String = ""

            sPath += "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\" + sPrinterNm

            Dim rk As RegistryKey = Registry.LocalMachine.OpenSubKey(sPath)

            m_printerparams.IPAddress = ""
            m_printerparams.PortNo = ""
            m_printerparams.PrinterDriver = rk.GetValue("Printer Driver").ToString()
            m_printerparams.PrinterName = rsPrintName
            m_printerparams.PrinterPort = rk.GetValue("Port").ToString()
            m_printerparams.ShareName = rk.GetValue("Share Name").ToString()

            If m_printerparams.PrinterPort.StartsWith("IP_") Then
                rbTcpIpYn = True

                'HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Monitors\Standard TCP/IP Port\Ports\IP_61.33.78.172
                sPath = "SYSTEM\CurrentControlSet\Control\Print\Monitors\Standard TCP/IP Port\Ports\" + m_printerparams.PrinterPort

                rk = Registry.LocalMachine.OpenSubKey(sPath)

                m_printerparams.IPAddress = rk.GetValue("IPAddress").ToString()
                m_printerparams.PortNo = rk.GetValue("PortNumber").ToString()
            Else
                rbTcpIpYn = False
            End If

            Return m_printerparams

        Catch ex As Exception
            'MsgBox(sFn + " : " + ex.Message)

            Return Nothing
        End Try
    End Function

End Class