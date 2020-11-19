'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGCOMMON_CTRL.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : 컨트롤 공통 함수 정의 Ctrl                                             */
'/* Design       : 2003-07-10 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Namespace CommConst
    Public Class FixedVariable

        '< add freety 2005/07/28
        Public Shared gsRstFlagR As String = "△"
        Public Shared gsRstFlagM As String = "○"
        Public Shared gsRstFlagF As String = "◆"
        Public Shared g_color_FN As Drawing.Color = Drawing.Color.DarkGreen
        Public Shared g_color_AM_Bg As Drawing.Color = Drawing.Color.FromArgb(255, 255, 150)
        Public Shared g_color_AM_Fg As Drawing.Color = Drawing.Color.FromArgb(0, 0, 0)
        Public Shared g_color_CM_Bg As Drawing.Color = Drawing.Color.FromArgb(255, 150, 255)
        Public Shared g_color_CM_Fg As Drawing.Color = Drawing.Color.FromArgb(255, 255, 255)
        Public Shared g_color_DM_Bg As Drawing.Color = Drawing.Color.FromArgb(150, 255, 150)
        Public Shared g_color_DM_Fg As Drawing.Color = Drawing.Color.FromArgb(0, 128, 64)
        Public Shared g_color_HM_Bg As Drawing.Color = Drawing.Color.FromArgb(255, 230, 231)
        Public Shared g_color_HM_Fg As Drawing.Color = Drawing.Color.FromArgb(255, 0, 0)
        Public Shared g_color_LM_Bg As Drawing.Color = Drawing.Color.FromArgb(221, 240, 255)
        Public Shared g_color_LM_Fg As Drawing.Color = Drawing.Color.FromArgb(0, 0, 255)
        Public Shared g_color_PM_Bg As Drawing.Color = Drawing.Color.FromArgb(150, 150, 255)
        Public Shared g_color_PM_Fg As Drawing.Color = Drawing.Color.FromArgb(255, 255, 255)
        Public Shared gsRstDelimeter As String = ", "
        Public Shared gsBacGenCd_Nogrowth As String = "--"
        Public Shared gsBacGenCd_Nogen As String = "00"
        Public Shared gsRst_Nogrowth As String = "↓"
        Public Shared gsRst_Growth As String = "↑"
        Public Shared gsBacTestMTD As String = "D"
        Public Shared gsRmkDelimeter As String = "   "

        Public Shared gsExeFileName As String = "ACK@LIS.exe"
        Public Shared gsMsg_NoTk As String = "(*미접수*)"
        Public Shared gsMsg_NoRpt As String = "(검사중…)"
        Public Shared gsMsg_Cmt As String = "<소견>"
        Public Shared gsMsg_Cmt_bcno As String = "<검사분야 소견>"
        Public Shared gsMsg_Cmt_Indent As String = "  "
        Public Shared gsMsg_Cmt_Dot As String = "ㆍ"
        Public Shared giLen_Line As Integer = 110
        Public Shared giLen_Line2 As Integer = 220

        Public Shared giLen_TnsJubsuNo As Integer = 13
        Public Shared giLen_TnsJubsuNo_Full As Integer = 15
        Public Shared gsIPAddress_Scope As String = "192.168."
        Public Shared gsCharLine As String = "─"
        Public Shared gsCharLine2 As String = "="

        Public Shared gsUSDT As String = "20010101000000"
        Public Shared gsUEDT As String = "30000101000000"


        Public Shared Function FindLineLength(ByVal rsngFontSize As Single) As Integer
            Select Case Convert.ToInt32(rsngFontSize)
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
