'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGCOMMON_DES.vb                                                        */
'/* PartName     :                                                                        */
'/* Description  : 데이타 간단한암호화                                                    */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Namespace CommFN
    ' 현재 한글은 암호화 안됨
    Public Class DES
        Private Const sFile As String = "File : CGCOMMON_DES.vb, Class : CommFN.DES" & vbTab
        Protected msKey As Byte = 25

        Protected Function Key(ByVal rsKey As String) As Byte

            Dim utf8 As New System.Text.UTF8Encoding ' Create a UTF-8 encoding.
            Dim encodedBytes As Byte() = utf8.GetBytes(rsKey)
            Dim iKey As Integer

            If rsKey.Length > 0 Then
                If UBound(encodedBytes) Mod 2 = 0 Then
                    iKey = encodedBytes(UBound(encodedBytes))
                Else
                    iKey = encodedBytes(0)
                End If

                iKey = CType(iKey.ToString.Substring(0, 1), Integer)

                Return CType(iKey, Byte)

            Else
                Return 25
            End If

        End Function

        Public Function Encode(ByVal rsVal As String, ByVal rsKey As String) As String
            Dim utf8 As New System.Text.UTF8Encoding ' Create a UTF-8 encoding.
            Dim sString As String = ""

            msKey = Key(rsKey)

            Dim encodedBytes As Byte() = utf8.GetBytes(rsVal)
            Dim b As Byte
            For Each b In encodedBytes
                sString &= Chr(b Xor msKey)
            Next b

            Return sString

        End Function

        Public Function Decode(ByVal rsVal As String, ByVal rsKey As String) As String
            Dim utf8 As New System.Text.UTF8Encoding ' Create a UTF-8 encoding.
            Dim sString As String = ""

            msKey = Key(rsKey)

            Dim encodedBytes As Byte() = utf8.GetBytes(rsVal)
            Dim b As Byte
            For Each b In encodedBytes
                sString &= ChrW((Not (b) Xor Not (msKey)))
            Next b

            Return sString
        End Function
    End Class

#Region " 간단한 암호화2 : Class HashMD5 "
    Public Class HashMD5
        Public Function Encrypt(ByVal rsVal1 As String, ByVal rsVal2 As String) As String
            'UserPW MD5 Hash
            Dim md5 As System.Security.Cryptography.MD5 = New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim hash() As Byte = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes((rsVal2.ToCharArray)))
            Dim b64 As String = Convert.ToBase64String(hash)

            If Asc(rsVal1.Substring(0, 1)) Mod 2 = 0 Then
                Encrypt = StrReverse(b64).Substring(2)
            Else
                Encrypt = b64.Substring(0, b64.Length - 2)
            End If

        End Function
    End Class
#End Region
End Namespace


