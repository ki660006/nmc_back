'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System                                          */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_COMMON02.vb                                                       */
'/* PartName     :                                                                        */
'/* Description  : Menu 공통함수 Class                                                    */
'/* Design       : 2003-10-24 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System.Windows.Forms
Imports LISCOM.ComFN

Namespace Menu
    Public Class UserDefined
        Private Const msFile As String = "File : CGCOM_MENU.vb, Class : COMMMON.Menu.UserDefined" + vbTab

        Private Shared Function fnGetParentMenuItem(ByVal roMenuItem As MenuItem, ByVal rsMenuIdx As String, ByVal rsMenuNm As String) As MenuItem
            Dim objMenuItem As MenuItem
            Dim ix As Integer = CInt(rsMenuIdx.Substring(0, 2))
            Dim sIx As String = ""

            If rsMenuIdx.Length > 2 Then
                sIx = rsMenuIdx.Substring(2)
                objMenuItem = fnGetParentMenuItem(roMenuItem.MenuItems(ix), rsMenuIdx, rsMenuNm)
            Else
                If rsMenuNm <> "" Then
                    roMenuItem.MenuItems.Add(rsMenuNm)
                    objMenuItem = roMenuItem.MenuItems(ix)
                Else
                    objMenuItem = roMenuItem
                End If
            End If

            fnGetParentMenuItem = objMenuItem

        End Function

        Private Shared Function fnGetOrgMenuItem(ByVal roMenuItem As MenuItem, ByVal rsMenuIdx As String) As MenuItem

            Dim objMenuItem As MenuItem
            Dim ix As Integer = CInt(rsMenuIdx.Substring(0, 2))
            Dim sIx As String = ""

            If rsMenuIdx.Length > 2 Then
                sIx = rsMenuIdx.Substring(2)
                If roMenuItem.MenuItems.Count > ix Then
                    objMenuItem = fnGetOrgMenuItem(roMenuItem.MenuItems(ix), rsMenuIdx.Substring(2))
                Else
                    objMenuItem = Nothing
                End If

            Else
                If roMenuItem.MenuItems.Count > ix Then
                    objMenuItem = roMenuItem.MenuItems(ix)
                Else
                    objMenuItem = Nothing
                End If

            End If

            fnGetOrgMenuItem = objMenuItem

        End Function


        Public Shared Function GetMenu(ByVal r_dt As DataTable, ByVal r_mainmnu As MainMenu) As MainMenu
            Dim sFn As String = "Public Shared Function GetMenu(DataTable, MainMenu) As MainMenu"

            Dim mainmnuNew As New MainMenu
            Dim mnuitemBuf As New MenuItem
            Dim mnuitemClone As MenuItem

            Dim iMnuIndex As Integer = 0
            Dim sMnuIndex As String = ""

            Dim sMnuNm As String = ""
            Dim sIsParent As String = ""
            Dim sMnuId_new As String = ""
            Dim sMnuId_org As String = ""

            Dim sMnuLvl_cur As String = ""
            Dim sMnuLvl_pre As String = ""

            Try
                mainmnuNew.MenuItems.Clear()
                If r_dt.Rows.Count > 0 Then
                    For i As Integer = 1 To r_dt.Rows.Count

                        With r_dt.Rows(i - 1)
                            sIsParent = .Item("isparent").ToString.Trim
                            sMnuId_new = .Item("mnuidnew").ToString.Trim
                            sMnuId_org = .Item("mnuid").ToString.Trim
                            sMnuNm = .Item("mnunm").ToString.Trim.Replace("^", "&")
                            sMnuLvl_cur = .Item("mnulvl").ToString.Trim
                        End With

                        'If sMnuNm.IndexOf("반납/폐기 건수") > 0 Then MsgBox("AAA")

                        Debug.WriteLine("MenuNm : " + sMnuNm + ", Parent : " + sIsParent + ", MenuLvl : " + sMnuLvl_cur)

                        If sIsParent = "1" Then
                            ' Parent 메뉴 선택
                            iMnuIndex = CInt(sMnuId_new.Substring(0, 2))
                            sMnuIndex = sMnuId_new.Substring(2)

                            If sMnuIndex = "" Then
                                ' Root메뉴 생성
                                mainmnuNew.MenuItems.Add(sMnuNm)

                                If iMnuIndex > mainmnuNew.MenuItems.Count - 1 Then iMnuIndex = mainmnuNew.MenuItems.Count - 1

                                mnuitemBuf = mainmnuNew.MenuItems(iMnuIndex)

                                If sMnuNm.Equals("창(&W)") = True Then mnuitemBuf.MdiList = True

                            Else
                                mnuitemBuf = fnGetParentMenuItem(mainmnuNew.MenuItems(iMnuIndex), sMnuIndex, sMnuNm)

                            End If

                        Else
                            If sIsParent = "0" And sMnuLvl_cur = "0" Then
                                ' Pareant 항목이 없고 Root에 생성될 경우 Root에 바로 복사한다.

                                ' 복사할 메뉴 선택 
                                iMnuIndex = CInt(sMnuId_org.Substring(0, 2))
                                sMnuIndex = sMnuId_org.Substring(2)
                                mnuitemClone = fnGetOrgMenuItem(r_mainmnu.MenuItems(iMnuIndex), sMnuIndex)

                                mainmnuNew.MenuItems.Add(mnuitemClone.CloneMenu)

                            Else
                                If sIsParent = "0" And sMnuLvl_cur < sMnuLvl_pre Then
                                    iMnuIndex = CInt(sMnuId_new.Substring(0, 2))
                                    sMnuIndex = sMnuId_new.Substring(2)

                                    mnuitemBuf = fnGetParentMenuItem(mainmnuNew.MenuItems(iMnuIndex), sMnuIndex, "")
                                End If

                                ' 복사할 메뉴 선택 
                                iMnuIndex = CInt(sMnuId_org.Substring(0, 2))
                                sMnuIndex = sMnuId_org.Substring(2)
                                mnuitemClone = fnGetOrgMenuItem(r_mainmnu.MenuItems(iMnuIndex), sMnuIndex, sMnuNm)

                                If Not IsNothing(mnuitemClone) Then
                                    mnuitemBuf.MenuItems.Add(mnuitemClone.CloneMenu)
                                Else
                                    Dim sErr As String = ""

                                    sErr = ""
                                    sErr += "사용자별 메뉴구성에 오류가 있습니다. 관리자에게 연락주시기 바랍니다. " & vbCrLf & vbCrLf
                                    sErr += "메뉴명: " + sMnuNm

                                    MsgBox(sErr, MsgBoxStyle.Exclamation, "메뉴구성")
                                    Fn.log(sErr)
                                End If

                            End If

                        End If

                        sMnuLvl_pre = sMnuLvl_cur
                    Next

                End If

                GetMenu = mainmnuNew

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try

        End Function

        Private Shared Function fnGetOrgMenuItem(ByVal roMenuItem As MenuItem, ByVal rsMenuIdx As String, ByVal rsMenuTxt As String) As MenuItem
            Dim objMenuItem As MenuItem
            Dim ix As Integer = CInt(rsMenuIdx.Substring(0, 2))
            Dim sIx As String = ""

            If rsMenuIdx.Length > 2 Then
                sIx = rsMenuIdx.Substring(2)
                If roMenuItem.MenuItems.Count > ix Then
                    objMenuItem = fnGetOrgMenuItem(roMenuItem.MenuItems(ix), rsMenuIdx.Substring(2))
                Else
                    objMenuItem = Nothing
                End If

            Else
                objMenuItem = Nothing

                For Each mnuItem As MenuItem In roMenuItem.MenuItems
                    If mnuItem.Text.ToUpper = rsMenuTxt.ToUpper Then
                        objMenuItem = mnuItem

                        Exit For
                    End If
                Next
            End If

            fnGetOrgMenuItem = objMenuItem

        End Function

    End Class

End Namespace