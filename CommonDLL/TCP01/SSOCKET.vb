Public Class ServerSocket
    Private Const mcMaxThread As Integer = 5

    Private Shared mssocket As System.Net.Sockets.Socket
    Private Shared miPort As Integer = 0
    Private Shared mthread As System.Threading.Thread
    Private Shared miCnt As Integer = 0

    Private Shared mbStopThread As Boolean = False
    Private Shared mbEndThread As Boolean = False
    Private Shared mTimer As System.Threading.Timer

    Private Shared miServerSocketType As Integer = 0

    Private Enum ServerSocketType
        Connection_EveryTime = 0
        Connection_OneTime = 1
    End Enum

    Public Shared Event AfterReceived(ByVal sRcvMsg As String)

    Shared socketCur As System.Net.Sockets.Socket

    Public Sub New(ByVal aiPort As Integer, Optional ByVal aiServerSocketType As Integer = 0)
        Try
            mssocket = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
            mssocket.Bind(New System.Net.IPEndPoint(System.Net.IPAddress.Any, aiPort))
            mssocket.Listen(0)
            miPort = aiPort
            miServerSocketType = aiServerSocketType

            mTimer = New System.Threading.Timer(New System.Threading.TimerCallback(AddressOf sbTimerTick), Nothing, 100, 10000)
            'Delay Time = 0으로 하면 CallBack함수가 바로 호출된다.
            'Dim tmr As New System.Threading.Timer(New System.Threading.TimerCallback(AddressOf sbTimerTick), Nothing, 0, 100)

        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub Dispose()
        mbStopThread = True
        mbEndThread = True

        If Not IsNothing(mTimer) Then
            mTimer.Dispose()
        End If

        If Not IsNothing(mssocket) Then
            mssocket.Close()
        End If

        'Dim sendsocket As TLA.SendSocket
        'sendsocket = New TLA.SendSocket

        'sendsocket.fnSendMsg("")

        'sendsocket.sbDispose()
        'sendsocket = Nothing
    End Sub

    'Public Shared Sub sbOpenSvrSocket(ByVal aiPort As Integer, Optional ByVal aiServerSocketType As Integer = 0)
    '    Try
    '        mssocket = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
    '        mssocket.Bind(New System.Net.IPEndPoint(System.Net.IPAddress.Any, aiPort))
    '        mssocket.Listen(0)
    '        miPort = aiPort
    '        miServerSocketType = aiServerSocketType

    '        mTimer = New System.Threading.Timer(New System.Threading.TimerCallback(AddressOf sbTimerTick), Nothing, 1000, 10000)
    '        'Delay Time = 0으로 하면 CallBack함수가 바로 호출된다.
    '        'Dim tmr As New System.Threading.Timer(New System.Threading.TimerCallback(AddressOf sbTimerTick), Nothing, 0, 100)

    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Shared Sub sbTimerTick(ByVal objSender As Object)
        If Not mssocket.Poll(100, Net.Sockets.SelectMode.SelectRead) Then
            Exit Sub
        End If

        'Timer Stop
        mTimer.Change(-1, -1)

        If miCnt >= mcMaxThread Then
            'Timer Restart
            mTimer.Change(100, 10000)

            Exit Sub
        End If

        mthread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf ThreadProc))
        mthread.IsBackground = True
        mthread.Start()

        SyncLock mthread
            miCnt += 1
        End SyncLock

        'Timer Restart
        mTimer.Change(100, 10000)
    End Sub

    Protected Shared Sub ThreadProc()
        Dim byteBuffer(1024) As Byte
        Dim iRecvByte As Integer = 0
        Dim sTemp As String = ""

        socketCur = mssocket.Accept

        Debug.WriteLine("ThreadProc - " & socketCur.Handle.ToString)

        While Not mbStopThread
            If socketCur.Available > 0 Then
                iRecvByte = socketCur.Receive(byteBuffer)

                If iRecvByte > 0 Then
                    SyncLock System.Threading.Thread.CurrentThread
                        RaiseEvent AfterReceived(System.Text.Encoding.Default.GetString(byteBuffer))

                        'socketCur(클라이언트)에 전달할 메세지
                        sTemp = "가나다abc"
                        If sTemp.Length > 0 Then
                            socketCur.Send(System.Text.Encoding.Default.GetBytes(sTemp))
                        End If
                    End SyncLock
                End If

                If miServerSocketType = ServerSocketType.Connection_OneTime Then
                    Exit While
                End If
            End If

            Dim tmpsocket As New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)


            If Not socketCur.Connected Then
                mbStopThread = True
            End If
        End While

        If socketCur.Connected Then
            socketCur.Close()
        End If

        SyncLock System.Threading.Thread.CurrentThread
            miCnt -= 1
        End SyncLock

        'If miMulti = 0 Then
        '    If socketCur.Connected Then
        '        'socketCur.Close()

        '        'SyncLock System.Threading.Thread.CurrentThread
        '        '    miCnt -= 1
        '        'End SyncLock

        '        mTimer.Change(-1, -1)

        '        mthread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf ThreadProc))

        '        mthread.Start()

        '        SyncLock mthread
        '            miCnt += 1
        '        End SyncLock

        '        'Timer Restart
        '        mTimer.Change(100, 100)
        '    End If
        'Else
        '    If socketCur.Connected Then
        '        socketCur.Close()

        '        SyncLock System.Threading.Thread.CurrentThread
        '            miCnt -= 1
        '        End SyncLock
        '    End If

        '    Dim thread As New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf ThreadProc))
        '    thread.Start()
        'End If

        'If mbEndThread Then
        '    mssocket.Close()
        'Else
        '    If miMulti > 0 Then
        '        miCnt += 1
        '    Else
        '        mthreadstart = New System.Threading.ThreadStart(AddressOf ThreadProc)

        '        mthread = New System.Threading.Thread(mthreadstart)

        '        mthread.IsBackground = True

        '        mbStopThread = False
        '        mbEndThread = False

        '        mthread.Start()
        '    End If
        'End If
    End Sub
End Class
