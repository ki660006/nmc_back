'Imports COMMON.CommFN
Imports System.IO

Public Class SendSocket
    Private Const mcFile$ = "File : CSOCKET.vb, Class : SendSocket" + vbTab

    Private msServer As String = "192.168.1.84"
    Private miPort As Integer = 9105
    Private mcsocket As System.Net.Sockets.Socket
    Private Event DataArrival(ByVal sMsg As String)

    ' Socket Error 로그
    Public Shared Sub SocketLog(ByVal sLog As String, ByVal sMsg As String)
        Dim sFile As String
        Dim sDir As String

        sDir = Environment.CurrentDirectory + "\SocketLog"

        If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

        sFile = sDir & "\Err" & Format(Now, "yyyy-MM-dd") & ".txt"
        Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

        sw.WriteLine(Now())

        sw.WriteLine(vbTab & sLog)

        'sw.WriteLine(vbTab & "Err Number : " & e.Number)
        sw.WriteLine(vbTab & "Err Description : " & sMsg)

        sw.Close()
    End Sub

    Public Shared Sub SendLog(ByVal sSource As String, ByVal sMsg As String)
        Dim sFile As String
        Dim sDir As String
        Dim sIP As String = ""

        sDir = Environment.CurrentDirectory + "\SendLog"

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

    Private Sub sbConnectCliSocketToSvrSocket()
        mcsocket = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)

        Try
            'Dim lngIP As Long = fnConvIPStrToIPLong(msServer)
            'Dim ipep As New System.Net.IPEndPoint(lngIP, miPort)

            Dim ipep As New System.Net.IPEndPoint(System.Net.IPAddress.Parse(msServer), miPort)

            If mcsocket.Connected = False Then
                mcsocket.Connect(ipep)
            End If
        Catch ae As ArgumentNullException
            Debug.Write("ArgumentNullException : ", ae.ToString())
        Catch se As System.Net.Sockets.SocketException
            Debug.Write("SocketException : ", se.ToString())
        Catch e As Exception
            Debug.Write("Unexpected exception : ", e.ToString())
        Finally
        End Try
    End Sub

    Public Sub sbConnectCliSocketToSvrSocket(ByVal asServer As String, ByVal aiPort As Integer)
        mcsocket = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)

        Try

            Dim ipep As New System.Net.IPEndPoint(System.Net.IPAddress.Parse(asServer), aiPort)

            If mcsocket.Connected = False Then
                mcsocket.Connect(ipep)

                msServer = asServer
                miPort = aiPort
            End If
        Catch ae As ArgumentNullException
            Debug.Write("ArgumentNullException : ", ae.ToString())
        Catch se As System.Net.Sockets.SocketException
            Debug.Write("SocketException : ", se.ToString())
        Catch e As Exception
            Debug.Write("Unexpected exception : ", e.ToString())
        Finally
        End Try
    End Sub

    'Private Sub sbConnectCliSocketToSvrSocket(ByVal asSource As String)
    '    mcsocket = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)

    '    Try
    '        Select Case asSource
    '            Case "AL"
    '                Dim sPath$ = System.Windows.Forms.Application.StartupPath()
    '                msServer = DP_Common.getOneElementXML(sPath & "\XML\", sPath & "\XML\AutoLabeler.xml", "AutoLabeler.Address")
    '                miPort = CType(DP_Common.getOneElementXML(sPath & "\XML\", sPath & "\XML\AutoLabeler.xml", "AutoLabeler.Port"), Integer)
    '            Case "TLA"
    '                Dim sPath$ = System.Windows.Forms.Application.StartupPath()
    '                msServer = DP_Common.getOneElementXML(sPath & "\XML\", sPath & "\XML\TLA.xml", "TLA.Address")
    '                miPort = CType(DP_Common.getOneElementXML(sPath & "\XML\", sPath & "\XML\TLA.xml", "TLA.Port"), Integer)
    '        End Select

    '        Debug.WriteLine(Format(Now, "yyyy-MM-dd HH:mm:ss"))

    '        Dim lngIP As Long = fnConvIPStrToIPLong(msServer)
    '        Dim ipep As New System.Net.IPEndPoint(System.Net.IPAddress.Parse(msServer), miPort)

    '        Debug.WriteLine(Format(Now, "yyyy-MM-dd HH:mm:ss"))

    '        If mcsocket.Connected = False Then
    '            mcsocket.Connect(ipep)
    '        End If

    '    Catch ae As ArgumentNullException
    '        Debug.Write("ArgumentNullException : ", ae.ToString())
    '    Catch se As System.Net.Sockets.SocketException
    '        Debug.Write("SocketException : ", se.ToString())
    '    Catch e As Exception
    '        Debug.Write("Unexpected exception : ", e.ToString())
    '    Finally
    '    End Try
    'End Sub

    Private Sub sbDisconnectCliSocketFromSvrSocket()
        If Not mcsocket Is Nothing Then
            If mcsocket.Connected Then
                mcsocket.Close()
                mcsocket = Nothing
            End If
        End If
    End Sub

    Public Sub sbDispose()
        sbDisconnectCliSocketFromSvrSocket()
    End Sub

    Private Function fnConvIPStrToIPLong(ByVal asIP As String) As Long
        Dim sFn$ = ""

        Try
            Dim arrTmp As String() = asIP.Split("."c)

            If arrTmp.Length <> 4 Then
                arrTmp = "127.0.0.1".Split("."c)
            End If

            Dim lngIP As Long = 0

            For i As Integer = 0 To arrTmp.Length - 1
                lngIP += CType(CType(arrTmp(i), Integer) * (2 ^ (8 * i)), Long)
            Next

            Return lngIP
        Catch ex As Exception
            '127.0.0.1에 해당하는 IPLong
            Return CType(127 + (2 ^ 24), Long)
        End Try
    End Function

    Public Function fnSendMsg(ByVal asMsg As String) As Boolean
        Dim sFn$ = "Public Function fnSendMsg(ByVal asMsg As String) As Boolean"

        Dim bFn As Boolean = False
        Dim bytemsg As Byte() = System.Text.Encoding.Default.GetBytes(asMsg)
        Dim ibyteSent As Integer = 0

        Try
            sbConnectCliSocketToSvrSocket()

            If mcsocket.Connected Then
                ibyteSent = mcsocket.Send(bytemsg)

                If ibyteSent > 0 Then
                    bFn = True
                End If
            End If
        Catch ae As ArgumentNullException
            Debug.Write("ArgumentNullException : ", ae.ToString)
            SocketLog(mcFile + sFn, "ArgumentNullException : " + ae.ToString)
        Catch se As System.Net.Sockets.SocketException
            Debug.Write("SocketException : ", se.ToString)
            SocketLog(mcFile + sFn, "SocketException(" + se.ErrorCode.ToString + ")" + se.ToString)
        Catch e As Exception
            Debug.Write("Unexpected Exception : ", e.ToString)
            SocketLog(mcFile + sFn, "Unexpected Exception : " + e.ToString)
        Finally
            sbDisconnectCliSocketFromSvrSocket()
            fnSendMsg = bFn
        End Try
    End Function

    'Public Function fnSendMsg(ByVal asSource As String, ByVal asMsg As String) As Boolean
    '    Dim sFn$ = "Public Function fnSendMsg(ByVal asMsg As String) As Boolean"

    '    Dim bFn As Boolean = False
    '    Dim bytemsg As Byte() = System.Text.Encoding.Default.GetBytes(asMsg)
    '    Dim ibyteSent As Integer = 0

    '    Try
    '        sbConnectCliSocketToSvrSocket(asSource)

    '        ibyteSent = mcsocket.Send(bytemsg)

    '        If ibyteSent > 0 Then
    '            bFn = True

    '            SendLog(asSource, asMsg)
    '        End If
    '    Catch ae As ArgumentNullException
    '        Debug.Write("ArgumentNullException : ", ae.ToString)
    '        SocketLog(mcFile + sFn, "ArgumentNullException : " + ae.ToString)
    '    Catch se As System.Net.Sockets.SocketException
    '        Debug.Write("SocketException : ", se.ToString)
    '        SocketLog(mcFile + sFn, "SocketException(" + se.ErrorCode.ToString + ")" + se.ToString)
    '    Catch e As Exception
    '        Debug.Write("Unexpected Exception : ", e.ToString)
    '        SocketLog(mcFile + sFn, "Unexpected Exception : " + e.ToString)
    '    Finally
    '        sbDisconnectCliSocketFromSvrSocket()
    '        fnSendMsg = bFn
    '    End Try
    'End Function

    Public Function fnSendMsg(ByVal asServer As String, ByVal aiPort As Integer, ByVal asSource As String, ByVal asMsg As String) As Boolean
        Dim sFn$ = "Public Function fnSendMsg(ByVal asMsg As String) As Boolean"

        Dim bFn As Boolean = False
        Dim bytemsg As Byte() = System.Text.Encoding.Default.GetBytes(asMsg)
        Dim ibyteSent As Integer = 0

        Try
            sbConnectCliSocketToSvrSocket(asServer, aiPort)

            ibyteSent = mcsocket.Send(bytemsg)

            If ibyteSent > 0 Then
                bFn = True

                SendLog(asSource, asMsg)
            End If
        Catch ae As ArgumentNullException
            Debug.Write("ArgumentNullException : ", ae.ToString)
            SocketLog(mcFile + sFn, "ArgumentNullException : " + ae.ToString)
        Catch se As System.Net.Sockets.SocketException
            Debug.Write("SocketException : ", se.ToString)
            SocketLog(mcFile + sFn, "SocketException(" + se.ErrorCode.ToString + ")" + se.ToString)
        Catch e As Exception
            Debug.Write("Unexpected Exception : ", e.ToString)
            SocketLog(mcFile + sFn, "Unexpected Exception : " + e.ToString)
        Finally
            sbDisconnectCliSocketFromSvrSocket()
            fnSendMsg = bFn
        End Try
    End Function

    Public Function fnSendMsgOneConn(ByVal asSource As String, ByVal asMsg As String) As Boolean
        Dim sFn$ = "Public Function fnSendMsg(ByVal asMsg As String) As Boolean"

        Dim bFn As Boolean = False
        Dim bytemsg As Byte() = System.Text.Encoding.Default.GetBytes(asMsg)
        Dim ibyteSent As Integer = 0

        Try
            ibyteSent = mcsocket.Send(bytemsg)

            If ibyteSent > 0 Then
                bFn = True

                SendLog(asSource, asMsg)
            End If
        Catch ae As ArgumentNullException
            Debug.Write("ArgumentNullException : ", ae.ToString)
            SocketLog(mcFile + sFn, "ArgumentNullException : " + ae.ToString)
        Catch se As System.Net.Sockets.SocketException
            Debug.Write("SocketException : ", se.ToString)
            SocketLog(mcFile + sFn, "SocketException(" + se.ErrorCode.ToString + ")" + se.ToString)
        Catch e As Exception
            Debug.Write("Unexpected Exception : ", e.ToString)
            SocketLog(mcFile + sFn, "Unexpected Exception : " + e.ToString)
        Finally
            fnSendMsgOneConn = bFn
        End Try
    End Function
End Class

Public Class ClientSocket
    Inherits SendSocket

    Public Sub New()

    End Sub
End Class
