'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : FGCDHELFPN.vb                                                          */
'/* PartName     :                                                                        */
'/* Description  : 공통 팝업의 쿼리문을 리턴해주는 함수 모음                              */
'/* Design       : 2010-09-27 이형택                                                      */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System.Windows.Forms
Imports System.Drawing
Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports AxFPSpreadADO
Imports DBORA.DbProvider

Public Class FGCDHELPFN
    Private Const msFile As String = "File : FGCDHELPFN.vb, Class : FGCDHELPFN" & vbTab

    Public Sub New()
        MyBase.New()
    End Sub

    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_RtnDataList                                                        */
    '/* Parameter     : rsSql  (쿼리 스트링)                                                  */
    '/*                 ralArg (쿼리 조건 데이터)                                             */      
    '/* Design        : 2010-09-28 이형택                                                     */
    '/* Description   : 받아온 쿼리스트링의 데이터를 추출하여 데이터테이블로 리턴             */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Function fn_RtnDataList(ByVal rsSql As String, ByVal ralArg As ArrayList) As DataTable
        Dim sFn As String = "Public Shared Function fn_RtnDataList(ByVal rsSql As String, ByVal ralArg As ArrayList) As DataTable"
        Dim alParm As New ArrayList

        Try
            If ralArg.Count > 0 Then
                For ix As Integer = 0 To ralArg.Count - 1
                    alParm.Add(New OracleParameter("param" + (ix + 1).ToString, ralArg(ix)))
                Next
            End If

            Return DbExecuteQuery(rsSql, alParm)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Function

    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_PopMsg                                                             */
    '/* Parameter     : rofrm    (팝업이 호출될 폼  : ex) me                                  */
    '/*                 rsGubun  (메세지 이미지 구분 : I : information, S : 상태표시줄        */
    '/*                                              , E : error, C : caption                 */      
    '/*                 rsMsgTxt (설정할 메세지)                                              */      
    '/* Design        : 2010-09-29 이형택                                                     */
    '/* Description   : Message box                                                           */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Sub fn_PopMsg(ByVal rofrm As Windows.Forms.Form, ByVal rsGubun As String, ByVal rsMsgTxt As String)
        If rsGubun <> "I"c And rsGubun <> "E"c And rsGubun <> "S"c And rsGubun <> "C"c Then
            MsgBox("메세지 구분이 정확하지 않습니다.", MsgBoxStyle.Critical)
            Return
        End If

        Dim objMsg As New FGCDMSG01

        If rsGubun = "S"c Then
            DS_StatusBar.setTextStatusBar(rsMsgTxt)
        Else
            objMsg.sb_DisplayMsg(rofrm, rsGubun, rsMsgTxt)
        End If

    End Sub

    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_PopConfirm                                                         */
    '/* Parameter     : rofrm    (팝업이 호출될 폼  : ex) me                                  */
    '/*                 rsGubun  (메세지 이미지 구분 : I : information,                       */
    '/*                                              , E : error, C : caption                 */      
    '/*                 rsMsgTxt (설정할 메세지)                                              */     
    '/* Design        : 2010-09-29 이형택                                                     */
    '/* Description   : Confirm Message box  (상태표시줄 기능 없음)                           */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Function fn_PopConfirm(ByVal rofrm As Windows.Forms.Form, ByVal rsGubun As String, ByVal rsMsgTxt As String) As Boolean
        Dim objMsg As New FGCDMSG01
        Dim lb_RtnValue As Boolean

        If rsGubun <> "I"c And rsGubun <> "E"c And rsGubun <> "C"c Then
            MsgBox("메세지 구분이 정확하지 않습니다.", MsgBoxStyle.Critical)
            Return False
        End If

        lb_RtnValue = objMsg.fn_DisplayConfirm(rofrm, rsGubun, rsMsgTxt)

        Return lb_RtnValue
    End Function

#Region " 팝업 조회쿼리 "
    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_PopGetPatList                                                      */
    '/* Parameter     : riGbn (콤보의 셀렉트아이템)                                           */
    '/* Design        : 2010-09-28 이형택                                                     */
    '/* Description   : 환자번호, 환자명을 리턴                                               */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Function fn_PopGetPatList(ByVal riGbn As Integer) As String
        Dim ls_rtnValue As String = ""

        ls_rtnValue += "SELECT patno bunho, patnm suname"
        ls_rtnValue += "  FROM vw_ack_ocs_pat_info"
        ls_rtnValue += " WHERE instcd = '" + COMMON.CommLogin.LOGIN.PRG_CONST.SITECD + "'"
        If riGbn = 0 Then
            ls_rtnValue += "   AND patno =  :param1"
        ElseIf riGbn = 1 Then
            ls_rtnValue += "   AND patnm LIKE :param1 || '%'"
        Else

        End If

        ls_rtnValue += " UNION ALL "
        ls_rtnValue += "SELECT bunho, suname"
        ls_rtnValue += "  FROM mts0002_lis"

        If riGbn = 0 Then
            ls_rtnValue += " WHERE bunho = :param2"
        ElseIf riGbn = 1 Then
            ls_rtnValue += " WHERE suname LIKE :param2 || '%'"
        Else

        End If

        Return ls_rtnValue
    End Function

    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_PopKeepBcno                                                        */
    '/* Parameter     :                                                                       */
    '/* Design        : 2010-10-11 이형택                                                     */
    '/* Description   : 보관검체번호를 리턴                                                   */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Function fn_PopKeepBcno(ByVal riGbn As Integer) As String
        Dim ls_rtnValue As String = ""

        ls_rtnValue += "SELECT keepspcno                                      "
        ls_rtnValue += "     , fn_ack_date_str(ustm, 'yyyy-MM-dd hh24:mi:ss') as ustm "
        ls_rtnValue += "     , fn_ack_date_str(uetm, 'yyyy-MM-dd hh24:mi:ss') as uetm "
        ls_rtnValue += "     , regno                                          "
        ls_rtnValue += "  FROM lb080m                                         "
        ls_rtnValue += " WHERE regno = :param1                                      "
        ls_rtnValue += "   and uetm  > fn_ack_date_str(fn_ack_sysdate, 'yyyymmddhh24miss') " '<20130212 보관검체3일까지가져오도록수정 
        ls_rtnValue += "ORDER BY uetm desc                                    "

        Return ls_rtnValue
    End Function

    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_PopGetUserList                                                     */
    '/* Parameter     : riGbn (콤보의 셀렉트아이템)                                           */
    '/* Design        : 2010-10-12 이형택                                                     */
    '/* Description   : 유저, 유져명을 리턴                                                   */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Function fn_PopGetUserList(ByVal riGbn As Integer) As String
        Dim ls_rtnValue As String = ""

        ls_rtnValue += "SELECT usrid usrid "
        ls_rtnValue += "     , usrnm usrnm "
        ls_rtnValue += "  FROM vw_ack_ocs_user_info"
        ls_rtnValue += " WHERE startdt <= SYSDATE"
        ls_rtnValue += "   AND enddt   >  SYSDATE"

        If riGbn = 0 Then
            ls_rtnValue += " AND usrid LIKE '%' || :param1 || '%'"
        ElseIf riGbn = 1 Then
            ls_rtnValue += " AND usrnm LIKE '%' || :param1 || '%'"
        Else

        End If

        Return ls_rtnValue
    End Function

    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_CmtList                                                            */
    '/* Parameter     : riGbn  (반납/폐기 구분)                                               */
    '/* Parameter     : rsSelf (자체폐기 구분)                                                */
    '/* Design        : 2010-10-15 이형택                                                     */
    '/* Description   : 반납/폐기사유 콤보 조회                                               */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Function fn_CmtList(ByVal riGbn As Integer, Optional ByVal rsSelf As String = "") As DataTable
        Dim sFn As String = "Public Shared Function fn_CmtList(ByVal riGbn As Integer) As DataTable"
        Dim sSql As String = ""

        Try
            sSql += "SELECT '[' || cmtgbn || cmtcd || '] ' ||  cmtcont as cmt, cmtcd "
            sSql += "  FROM lf170m "
            sSql += " WHERE cmtgbn = '" + riGbn.ToString + "' "

            If rsSelf = "" Then
                If riGbn = 0 Then
                    sSql += "   AND cmtgbn = '0' " ' 일반 반납/폐기
                Else
                    sSql += "   AND cmtgbn = '1' " ' 일반 반납/폐기
                End If

            ElseIf rsSelf = "1" Then
                sSql += "   AND realgbn = '0' " ' 자체폐기
            ElseIf rsSelf = "2" Then
                sSql += "   AND chggbn = '1' "  ' 교환
            End If

            Return DbExecuteQuery(sSql)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Function

    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_RtnDoc                                                             */
    '/* Parameter     : rsId (반납/폐기 의뢰의사 아이디)                                      */
    '/* Design        : 2010-10-19 이형택                                                     */
    '/* Description   : 반납/폐기사유 의뢰의사 리턴                                           */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Function fn_RtnDoc(ByVal rsId As String) As String
        Dim sFn As String = "Public Shared Function fn_CmtList(ByVal riGbn As Integer) As DataTable"
        Dim sqlDoc As String = ""
        Dim aryList As New ArrayList
        Dim ls_RtnValue As String = ""
        Dim dt As DataTable

        Try
            sqlDoc += "SELECT fn_ack_get_dr_name('" + rsId + "') rtnnm FROM DUAL"

            dt = DbExecuteQuery(sqlDoc, aryList)

            ls_RtnValue = dt.Rows(0).Item("rtnnm").ToString

            Return ls_RtnValue
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

            ls_RtnValue = ""
        End Try

    End Function

    '/*****************************************************************************************/
    '/*                                                                                       */
    '/* Function Name : fn_PopGetRtnReqList                                                   */
    '/* Parameter     : riGbn (콤보의 셀렉트아이템)                                           */
    '/* Design        : 2010-09-28 이형택                                                     */
    '/* Description   : 유저아이디, 유저명을 리턴                                             */
    '/*                                                                                       */
    '/*****************************************************************************************/
    Public Shared Function fn_PopGetRtnReqList(ByVal riGbn As Integer) As String
        Dim sSql As String = ""

        sSql += "SELECT drcd doctor, drnm doctor_name, deptcd doctor_gwa "
        sSql += "  FROM vw_ack_ocs_dr_info "
        sSql += " WHERE startdt <= SYSDATE"
        sSql += "   AND enddt   >  SYSDATE"

        If riGbn = 0 Then
            sSql += "   AND drcd LIKE '%' || :param1 || '%'"
        ElseIf riGbn = 1 Then
            sSql += "   AND drnm LIKE '%' || :param1 || '%'"
        ElseIf riGbn = 2 Then
            sSql += "   AND deptcd LIKE '%' || :param1 || '%'"
        Else

        End If

        Return sSql
    End Function
#End Region

End Class



