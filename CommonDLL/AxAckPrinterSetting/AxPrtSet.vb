Imports Microsoft.Win32
Imports System.Windows.Forms
Imports System.Drawing

Public Class AxPrtSet
    Private Const mc_iHeight As Integer = 30

    Private mbSkip As Boolean = False

    Private mbSharePrinter As Boolean = False
    Private mbTcpIpPrinter As Boolean = False

    Private msPrinterName As String = ""
    Private msPrinterModeName As String = ""

    Private m_enumPrinterMode As enumPrinterMode

    Private m_printerparams As New PrinterParams

    Public Property PrinterMode() As enumPrinterMode
        Get
            Return m_enumPrinterMode
        End Get

        Set(ByVal value As enumPrinterMode)
            m_enumPrinterMode = value

            If value = enumPrinterMode.Barcode Then
                Me.lblTitle.Text = "���ڵ�������"

            ElseIf value = enumPrinterMode.Normal Then
                Me.lblTitle.Text = "���������"

            End If
        End Set
    End Property

    Public ReadOnly Property PrinterModeName() As String
        Get
            If m_enumPrinterMode = enumPrinterMode.Barcode Then
                Return "���ڵ�������"

            ElseIf m_enumPrinterMode = enumPrinterMode.Normal Then
                Return "���������"

            Else
                Return ""

            End If
        End Get
    End Property

    Public Property PrinterName() As String
        Get
            Return msPrinterName
        End Get

        Set(ByVal value As String)
            If value Is Nothing Then Return
            If value = String.Empty Then Return

            msPrinterName = value

            sbDisp_PrinterList()
            sbDisp_CurrentPrinter()

            '< add freety 2007/12/07 : PrinterName �����ϸ� �ڵ����� ����ǵ��� �߰���
            sbSet_PrinterName()
            '>
        End Set
    End Property

    Public ReadOnly Property PrinterParameters() As PrinterParams
        Get
            Return m_printerparams
        End Get
    End Property

    Public ReadOnly Property SharePrinter() As Boolean
        Get
            Return mbSharePrinter
        End Get
    End Property

    Public ReadOnly Property TcpIpPrinter() As Boolean
        Get
            Return mbTcpIpPrinter
        End Get
    End Property

    Public Sub GetPrinterCurrentForm()
        Dim sFn As String = "Public Sub GetPrinterCurrentForm()"

        Try
            Dim sFrmNm As String = Me.ParentForm.Name

            Dim sFileNm As String = Application.StartupPath + "\XML" + "\" + "PRTCFG_" + sFrmNm + "_" + m_enumPrinterMode.ToString + ".xml"

            If IO.File.Exists(sFileNm) = False Then Return

            Dim xtr As Xml.XmlTextReader = New Xml.XmlTextReader(sFileNm)

            With xtr
                .ReadStartElement("ROOT")

                msPrinterName = .ReadElementString("PrinterName")

                .ReadEndElement()

                .Close()
            End With

        Catch ex As Exception
            MsgBox(sFn + " : " + ex.Message)

        Finally
            sbDisp_CurrentPrinter()

        End Try
    End Sub

    Private Sub sbDelete_CurrentPrinter()
        Dim sFn As String = "sbDelete_CurrentPrinter"

        Try
            If m_printerparams.PrinterName = "" Then Return

            Dim sFrmNm As String = Me.ParentForm.Name
            Dim sFileNm As String = Application.StartupPath + "\XML" + "\" + "PRTCFG_" + sFrmNm + "_" + m_enumPrinterMode.ToString + ".xml"
            Dim sFrmTxt As String = Me.ParentForm.Text
            Dim sPrinterNm As String = m_printerparams.PrinterName

            Dim sMsg As String = ""

            sMsg = ""
            sMsg += "ȭ�� : " + sFrmTxt + vbCrLf + vbCrLf
            sMsg += m_enumPrinterMode.ToString + " : " + sPrinterNm + vbCrLf + vbCrLf + vbCrLf
            sMsg += "�� ������ ������ �ʱ�ȭ�Ͻðڽ��ϱ�?"

            If MsgBox(sMsg, MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "������ ���� �ʱ�ȭ Ȯ��") = MsgBoxResult.No Then Return

            If IO.File.Exists(sFileNm) Then
                IO.File.Delete(sFileNm)

                msPrinterName = ""
            End If

        Catch ex As Exception
            MsgBox(sFn + " : " + ex.Message)

        End Try
    End Sub

    Private Sub sbDisp_CurrentPrinter()
        Dim sFn As String = "sbDisp_CurrentPrinter"

        Try
            Dim iExist As Integer = 0

            mbSkip = True

            For i As Integer = 1 To Me.cboPrinters.Items.Count
                'MsgBox(Me.cboPrinters.Items(i - 1).ToString)
                If msPrinterName.Trim = Me.cboPrinters.Items(i - 1).ToString.Trim Then
                    iExist = i

                    Exit For
                End If
            Next

            If iExist > 0 Then
                Me.cboPrinters.SelectedIndex = iExist - 1

                If msPrinterName.StartsWith("\\") Then
                    mbSharePrinter = True
                Else
                    mbSharePrinter = False
                End If

                'Printer Parameters ���ϱ�
                If mbSharePrinter Then
                    sbGet_PrinterParams_Shared()
                Else
                    sbGet_PrinterParams_Local_TcpIp()
                End If
            Else
                If msPrinterName.Length > 0 Then
                    MsgBox("�ش� �̸��� �����͸� ã�� �� �����ϴ�. Ȯ���Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Exclamation)
                End If

                Me.cboPrinters.SelectedIndex = -1
                msPrinterName = ""
                mbSharePrinter = False
                mbTcpIpPrinter = False
            End If

        Catch ex As Exception
            MsgBox(sFn + " : " + ex.Message)

        Finally
            mbSkip = False

        End Try
    End Sub

    Private Sub sbDisp_PrinterList()
        Dim sFn As String = "sbGet_PrinterParams_Shared"

        Try
            With Me.cboPrinters
                .Items.Clear()

                For Each sPrtNm As String In Printing.PrinterSettings.InstalledPrinters
                    .Items.Add(sPrtNm)
                Next
            End With

        Catch ex As Exception
            MsgBox(sFn + " : " + ex.Message)

        End Try
    End Sub

    Private Sub sbGet_PrinterParams_Local_TcpIp()
        Dim sFn As String = "sbGet_PrinterParams_Local_TcpIp"

        Try
            'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\HP Color LaserJet 3550

            Dim sPrinterNm As String = msPrinterName
            Dim sPath As String = ""

            sPath += "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\" + sPrinterNm

            Dim rk As RegistryKey = Registry.LocalMachine.OpenSubKey(sPath)

            m_printerparams.IPAddress = ""
            m_printerparams.PortNo = ""
            m_printerparams.PrinterDriver = rk.GetValue("Printer Driver").ToString()
            m_printerparams.PrinterName = msPrinterName
            m_printerparams.PrinterPort = rk.GetValue("Port").ToString()
            m_printerparams.ShareName = rk.GetValue("Share Name").ToString()

            If m_printerparams.PrinterPort.StartsWith("IP_") Then
                mbTcpIpPrinter = True

                'HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Monitors\Standard TCP/IP Port\Ports\IP_61.33.78.172
                sPath = "SYSTEM\CurrentControlSet\Control\Print\Monitors\Standard TCP/IP Port\Ports\" + m_printerparams.PrinterPort

                rk = Registry.LocalMachine.OpenSubKey(sPath)

                m_printerparams.IPAddress = rk.GetValue("IPAddress").ToString()
                m_printerparams.PortNo = rk.GetValue("PortNumber").ToString()
            Else
                mbTcpIpPrinter = False
            End If

        Catch ex As Exception
            MsgBox(sFn + " : " + ex.Message)

        End Try
    End Sub

    Private Sub sbGet_PrinterParams_Shared()
        Dim sFn As String = "sbGet_PrinterParams_Shared"

        Try
            'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\LanMan Print Services\Servers\10.5.3.57\Printers\SATO CL408e

            If msPrinterName.Split(CChar("\")).Length <> 4 Then
                MsgBox("������ �̸��� ������ �ֽ��ϴ�. Ȯ���Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Exclamation)

                Return
            End If

            Dim sComputerNm As String = msPrinterName.Split(CChar("\"))(2)
            Dim sPrinterNm As String = msPrinterName.Split(CChar("\"))(3)
            Dim sPath As String = ""

            sPath += "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\LanMan Print Services\Servers\"
            sPath += sComputerNm + "\Printers\" + sPrinterNm

            Dim rk As RegistryKey = Registry.LocalMachine.OpenSubKey(sPath)

            If rk Is Nothing Then
                'WIN98�� �����͸� ������ ���
                'HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\,,e06n01,SATO
                sPath = ""
                sPath += "SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Printers\" + msPrinterName.Replace("\", ",")

                rk = Registry.LocalMachine.OpenSubKey(sPath)

                If rk Is Nothing Then
                    MsgBox("������ ���θ� ���� �� �����ϴ�. Ȯ���Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Exclamation)

                    m_printerparams.IPAddress = ""
                    m_printerparams.PortNo = ""
                    m_printerparams.PrinterDriver = ""
                    m_printerparams.PrinterName = msPrinterName
                    m_printerparams.PrinterPort = ""
                    m_printerparams.ShareName = ""

                    Return
                Else
                    m_printerparams.IPAddress = sComputerNm
                    m_printerparams.PortNo = ""
                    m_printerparams.PrinterDriver = rk.GetValue("Printer Driver").ToString()
                    m_printerparams.PrinterName = msPrinterName
                    m_printerparams.PrinterPort = rk.GetValue("Port").ToString()
                    m_printerparams.ShareName = sPrinterNm
                End If
            Else
                m_printerparams.IPAddress = sComputerNm
                m_printerparams.PortNo = ""
                m_printerparams.PrinterDriver = rk.GetValue("Printer Driver").ToString()
                m_printerparams.PrinterName = msPrinterName
                m_printerparams.PrinterPort = rk.GetValue("Port").ToString()
                m_printerparams.ShareName = rk.GetValue("Share Name").ToString()
            End If

        Catch ex As Exception
            MsgBox(sFn + " : " + ex.Message)

        End Try
    End Sub

    Private Sub sbSet_PrinterName()
        Dim sFn As String = "Private Sub sbSet_PrinterParams()"

        Dim sFrmNm As String = Me.ParentForm.Name

        Dim sFileNm As String = Application.StartupPath + "\XML" + "\" + "PRTCFG_" + sFrmNm + "_" + m_enumPrinterMode.ToString + ".xml"

        Try
            Dim xtw As Xml.XmlTextWriter = New Xml.XmlTextWriter(sFileNm, System.Text.Encoding.GetEncoding("EUC-KR"))

            With xtw
                .Formatting = Xml.Formatting.Indented

                .WriteStartDocument(False)

                .WriteStartElement("ROOT")

                .WriteElementString("PrinterName", m_printerparams.PrinterName)

                .WriteEndElement()

                .Close()
            End With

        Catch ex As Exception
            MsgBox(sFn + " : " + ex.Message)

        End Try
    End Sub

    '> Event
    Private Sub AxPrtSet_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sbDisp_PrinterList()
    End Sub

    Private Sub AxPrtSet_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Height > mc_iHeight Then
            Me.Height = mc_iHeight
        End If

        Me.Refresh()
    End Sub

    Private Sub cboPrinters_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboPrinters.KeyDown
        If e.KeyCode <> Keys.Delete Then Return

        sbDelete_CurrentPrinter()

        sbDisp_CurrentPrinter()
    End Sub

    Private Sub cboPrinters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPrinters.SelectedIndexChanged
        If mbSkip Then Return

        If Me.cboPrinters.SelectedItem Is Nothing Then Return

        msPrinterName = Me.cboPrinters.SelectedItem.ToString()

        If msPrinterName.StartsWith("\\") Then
            mbSharePrinter = True
        Else
            mbSharePrinter = False
        End If

        'Printer Parameters ���ϱ�
        If mbSharePrinter Then
            sbGet_PrinterParams_Shared()
        Else
            sbGet_PrinterParams_Local_TcpIp()
        End If

        sbSet_PrinterName()
    End Sub
End Class

Public Class PrinterParams
    Public IPAddress As String = ""
    Public PortNo As String = ""
    Public PrinterDriver As String = ""
    Public PrinterName As String = ""
    Public PrinterPort As String = ""
    Public ShareName As String = ""
End Class

Public Enum enumPrinterMode
    Normal = 0
    Barcode = 1
End Enum