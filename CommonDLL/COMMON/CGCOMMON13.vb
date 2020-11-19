'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_COMMON01.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : 공통함수 Class                                                         */
'/* Design       : 2010-09-08 이형택                                                      */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.SVar
Imports AxFPSpreadADO

Namespace CommFN
    Public Class CGCOMMON13
        Private Const msFile As String = "File : CGCOMMON.dll, Class : CGCOMMON13" & vbTab

        '/*****************************************************************************************/
        '/*                                                                                       */
        '/* Function Name : fn_GetSelectItem                                                      */
        '/* Parameter     : rDt   (배열을 구성할 데이터 테이블)                                   */
        '/*                 rICnt (리턴 받을 배열의 크기)                                         */
        '/* Design        : 2010-09-09 이형택                                                     */
        '/* Description   : 한줄의 데이터테이블을 받아 그 내용을 배열로 리턴                      */
        '/*                                                                                       */
        '/*****************************************************************************************/
        Public Shared Function fn_GetSelectItem(ByVal rDt As DataTable, ByVal rICnt As Integer) As ArrayList
            Dim la_rtnValue As New ArrayList

            For i As Integer = 0 To rICnt - 1
                la_rtnValue.Add(rDt.Rows(0).Item(i).ToString().Split("|"c)(0).Trim)

            Next
            '
            Return la_rtnValue
        End Function

        '/*****************************************************************************************/
        '/*                                                                                       */
        '/* Function Name : fn_GetBloodColor                                                      */
        '/* Parameter     : rsAbo (혈액형 정보)                                                   */
        '/* Design        : 2010-09-08 이형택                                                     */
        '/* Description   : 혈액팩의 혈액형 고유 색을 리턴                                        */
        '/*                                                                                       */
        '/*****************************************************************************************/
        Public Shared Function fnGet_BloodColor(ByVal rsAbo As String) As Color
            Dim sFn As String = "Public Shared Function fnGet_BloodColor(ByVal rsCodeName As String) As String"

            Dim lc_RtnVal As Color

            If rsAbo = "" Then
                lc_RtnVal = Color.Red
            Else
                If rsAbo = "A" Then
                    lc_RtnVal = Color.Gold
                ElseIf rsAbo = "B" Then
                    lc_RtnVal = Color.Red
                ElseIf rsAbo = "O" Then
                    lc_RtnVal = Color.Blue
                ElseIf rsAbo = "AB" Then
                    lc_RtnVal = Color.Black
                Else
                    lc_RtnVal = Color.Red
                End If
            End If

            Return lc_RtnVal

        End Function

        '/*****************************************************************************************/
        '/*                                                                                       */
        '/* Function Name : sb_SetStBarSearchCnt                                                  */
        '/* Parameter     : riCnt (조회 건수)                                                     */
        '/* Design        : 2010-09-28 이형택                                                     */
        '/* Description   : 조회 건 수를 상태표시줄에 표시                                        */
        '/*                                                                                       */
        '/*****************************************************************************************/
        Public Shared Sub sb_SetStBarSearchCnt(ByVal rICnt As Integer)
            Dim ls_SetTxt As String

            If rICnt = 0 Then
                DS_StatusBar.setTextStatusBar("조회된 자료가 없습니다.")
            ElseIf rICnt < 0 Then
                DS_StatusBar.setTextStatusBar("조회중 오류가 발생 하였습니다.")
            Else
                ls_SetTxt = rICnt.ToString + "건의 자료가 조회 되었습니다."
                DS_StatusBar.setTextStatusBar(ls_SetTxt)
            End If

        End Sub

    End Class
End Namespace
