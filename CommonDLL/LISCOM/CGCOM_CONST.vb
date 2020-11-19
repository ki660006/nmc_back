
Namespace ComConst
    Public Class FixedVariable

        '< add freety 2005/07/28
        Public Shared RstFlagR As String = "△"
        Public Shared RstFlagM As String = "○"
        Public Shared RstFlagF As String = "◆"
        Public Shared Color_FN As Drawing.Color = Drawing.Color.DarkGreen
        Public Shared Color_AM_Bg As Drawing.Color = Drawing.Color.FromArgb(255, 255, 150)
        Public Shared Color_AM_Fg As Drawing.Color = Drawing.Color.FromArgb(0, 0, 0)
        Public Shared Color_CM_Bg As Drawing.Color = Drawing.Color.FromArgb(255, 150, 255)
        Public Shared Color_CM_Fg As Drawing.Color = Drawing.Color.FromArgb(255, 255, 255)
        Public Shared Color_DM_Bg As Drawing.Color = Drawing.Color.FromArgb(150, 255, 150)
        Public Shared Color_DM_Fg As Drawing.Color = Drawing.Color.FromArgb(0, 128, 64)
        Public Shared Color_HM_Bg As Drawing.Color = Drawing.Color.FromArgb(255, 230, 231)
        Public Shared Color_HM_Fg As Drawing.Color = Drawing.Color.FromArgb(255, 0, 0)
        Public Shared Color_LM_Bg As Drawing.Color = Drawing.Color.FromArgb(221, 240, 255)
        Public Shared Color_LM_Fg As Drawing.Color = Drawing.Color.FromArgb(0, 0, 255)
        Public Shared Color_PM_Bg As Drawing.Color = Drawing.Color.FromArgb(150, 150, 255)
        Public Shared Color_PM_Fg As Drawing.Color = Drawing.Color.FromArgb(255, 255, 255)
        Public Shared RstDelimeter As String = ", "
        Public Shared BacGenCd_Nogrowth As String = "--"
        Public Shared BacGenCd_Nogen As String = "00"
        Public Shared Rst_Nogrowth As String = "↓"
        Public Shared Rst_Growth As String = "↑"
        Public Shared BacTestMTD As String = "D"
        Public Shared RmkDelimeter As String = "   "

        Public Shared ExeFileName As String = "ACK@RIS.exe"
        Public Shared Msg_NoTk As String = "(*미접수*)"
        Public Shared Msg_NoRpt As String = "(검사중…)"
        Public Shared Msg_Cmt As String = "<소견>"
        Public Shared Msg_Cmt_bcno As String = "<검사분야 소견>"
        Public Shared Msg_Cmt_Indent As String = "  "
        Public Shared Msg_Cmt_Dot As String = "ㆍ"
        Public Shared Len_Line As Integer = 110
        Public Shared Len_Line2 As Integer = 220

        Public Shared Len_TnsJubsuNo As Integer = 13
        Public Shared Len_TnsJubsuNo_Full As Integer = 15
        Public Shared IPAddress_Scope As String = "192.168."
        Public Shared CharLine As String = "─"
        Public Shared CharLine2 As String = "="

        Public Shared USDT As String = "19900101000000"
        Public Shared UEDT As String = "30000101000000"

        Public Shared Function FindLineLength(ByVal rsgFontSize As Single) As Integer
            Select Case Convert.ToInt32(rsgFontSize)
                Case 10
                    Return 100

                Case 9
                    Return 116

                Case Else
                    Return 0

            End Select
        End Function
    End Class
End Namespace

