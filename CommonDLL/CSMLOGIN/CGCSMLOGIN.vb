Module CSM_VB

    Public Declare Function CSMConnect Lib "CSM_ClientVB.dll" (ByVal ip As String, ByVal port As Long) As Boolean
    Public Declare Function CSMDisconnect Lib "CSM_ClientVB.dll" () As Boolean
    Public Declare Function VB_CSMInit Lib "CSM_ClientVB.dll" () As IntPtr
    Public Declare Function VB_CSMSetCert Lib "CSM_ClientVB.dll" (ByVal id As String, ByVal dn As String, ByVal password As String) As IntPtr
    Public Declare Function VB_CSMGetCert Lib "CSM_ClientVB.dll" (ByVal id As String) As IntPtr
    Public Declare Function VB_CSMDelCert Lib "CSM_ClientVB.dll" (ByVal id As String) As IntPtr
    Public Declare Function VB_CSMLocalDelCert Lib "CSM_ClientVB.dll" (ByVal id As String, ByVal dn As String) As IntPtr
    Public Declare Function VB_CSMChangePassword Lib "CSM_ClientVB.dll" (ByVal id As String, ByVal oldPassword As String, ByVal newPassword As String) As IntPtr
    Public Declare Function CSMGetErrorCode Lib "CSM_ClientVB.dll" () As Integer
    Public Declare Function VB_CSMGetErrorMsg Lib "CSM_ClientVB.dll" () As IntPtr
    Public Declare Function VB_CSMVerifyPassword Lib "CSM_ClientVB.dll" (ByVal dn As String, ByVal password As String) As IntPtr
    Public Declare Function CSMIsCertNew Lib "CSM_ClientVB.dll" (ByVal id As String, ByVal dn As String, ByVal password As String, ByVal mode As Long) As Integer

    'redefine function

    'CSMInit
    Public Function CSMInit() As String
        CSMInit = Runtime.InteropServices.Marshal.PtrToStringAuto(VB_CSMInit())
    End Function

    'CSMSetCert
    Public Function CSMSetCert(ByVal id As String, ByVal dn As String, ByVal password As String) As String
        CSMSetCert = Runtime.InteropServices.Marshal.PtrToStringAuto(VB_CSMSetCert(id, dn, password))
    End Function

    'CSMGetCert
    Public Function CSMGetCert(ByVal id As String) As String
        CSMGetCert = Runtime.InteropServices.Marshal.PtrToStringAuto(VB_CSMGetCert(id))
    End Function

    'CSMDelCert
    Public Function CSMDelCert(ByVal id As String) As String
        CSMDelCert = Runtime.InteropServices.Marshal.PtrToStringAuto(VB_CSMDelCert(id))
    End Function

    'CSMLocalDelCert
    Public Function CSMLocalDelCert(ByVal id As String, ByVal dn As String) As String
        CSMLocalDelCert = Runtime.InteropServices.Marshal.PtrToStringAuto(VB_CSMLocalDelCert(id, dn))
    End Function

    'CSMChangePassword
    Public Function CSMChangePassword(ByVal id As String, ByVal oldPassword As String, ByVal newPassword As String) As String
        CSMChangePassword = Runtime.InteropServices.Marshal.PtrToStringAuto(VB_CSMChangePassword(id, oldPassword, newPassword))
    End Function

    'CSMGetErrorMsg
    Public Function CSMGetErrorMsg() As String
        CSMGetErrorMsg = Runtime.InteropServices.Marshal.PtrToStringAuto(VB_CSMGetErrorMsg())
    End Function

    'CSMVerifyPassword
    Public Function CSMVerifyPassword(ByVal dn As String, ByVal password As String) As String
        CSMVerifyPassword = Runtime.InteropServices.Marshal.PtrToStringAuto(VB_CSMVerifyPassword(dn, password))
    End Function
End Module
