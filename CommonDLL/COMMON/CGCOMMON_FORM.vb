Imports System
Imports System.Windows.Forms
Imports System.Drawing

Namespace CommFN
#Region " 사용자별 메뉴설정시 전역으로 사용할 MDIForm 및 메뉴정의 변수 : Class MdiMain "

    Public Class MdiMain
        Public Shared Frm As Windows.Forms.Form
        Public Shared FrmMenu As Windows.Forms.MainMenu
        Public Shared DB_Active_YN As String = ""
        Public Shared Db_ConnectTimeOut As String = ""

    End Class

    Public Class MdiTabControl
        Private Const msFile As String = "File : CGCOMMON_FORM.vb, Class : CommFN.MdiTabControl" & vbTab

        Public Shared Sub sbTabPageMove(ByVal r_frm As Form)
            Dim sFn As String = "Private Sub sbTabPageMove(Form)"

            Try
                Dim mnuTab As TabControl = DS_TabControl.MENU_TABCONTROL
                Dim sFormText As String = r_frm.Text
                If sFormText.IndexOf("ː") > 0 Then sFormText = sFormText.Substring(sFormText.IndexOf("ː") + 1).Replace("]", "")

                For ix As Integer = 0 To mnuTab.TabCount - 1
                    If mnuTab.TabPages(ix).Text = sFormText Then
                        mnuTab.TabPages.Remove(mnuTab.TabPages(ix))
                        Exit For
                    End If
                Next

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Fn.ExclamationErrMsg(Err, "MidPicBox - " + sFn)

            End Try
        End Sub

        Public Shared Sub sbTabPageAdd(ByVal r_frm As Form)
            Dim sFn As String = "public Sub sbTabPageAdd(Form)"

            Try
                Dim mnuTab As TabControl = DS_TabControl.MENU_TABCONTROL
                Dim iExist As Integer = -1
                Dim sFormText As String = r_frm.Text
                If sFormText.IndexOf("ː") > 0 Then sFormText = sFormText.Substring(sFormText.IndexOf("ː") + 1)

                For ix As Integer = 0 To mnuTab.TabCount - 1
                    If mnuTab.TabPages(ix).Text = sFormText Then
                        iExist = ix

                        Exit For
                    End If
                Next

                If iExist < 0 Then
                    mnuTab.TabPages.Add(sFormText)
                    mnuTab.TabPages(mnuTab.TabCount - 1).Text = sFormText

                    iExist = mnuTab.TabCount - 1
                End If

                mnuTab.SelectedIndex = iExist

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Fn.ExclamationErrMsg(Err, "MidPicBox - " + sFn)

            End Try
        End Sub

    End Class

    Public Class DS_FormDesige
        Private Shared moCtrlcol As Collection

        Public Shared Sub sbInti(ByVal ro_frm As System.Windows.Forms.Form)


            Dim objCtrl As System.Windows.Forms.Control

            moCtrlcol = New Collection

            fnFindControl(ro_frm.Controls)

            For Each objCtrl In moCtrlcol
                'If TypeOf (objCtrl) Is CButtonLib.CButton Then
                '    With CType(objCtrl, CButtonLib.CButton)
                '        .Font = New Font("굴림체", 9, FontStyle.Regular)

                '        .BackColor = Drawing.Color.FromArgb(236, 242, 255)
                '        .BorderColor = Color.Black

                '    End With

                'Else
                If TypeOf (objCtrl) Is AxFPSpreadADO.AxfpSpread Then

                    With CType(objCtrl, AxFPSpreadADO.AxfpSpread)
                        .Font = New Font("굴림체", 9, FontStyle.Regular)

                        .SelBackColor = Drawing.Color.FromArgb(213, 215, 255)
                        .SelForeColor = SystemColors.InactiveBorder

                        .ShadowColor = Drawing.Color.FromArgb(165, 186, 222)
                        .ShadowDark = Color.DimGray
                        .ShadowText = SystemColors.ControlText

                        .GrayAreaBackColor = Drawing.Color.FromArgb(236, 242, 255)
                    End With
                Else
                    'objCtrl.Font = New Font("굴림체", 9, FontStyle.Regular)
                End If
            Next
        End Sub

        Private Shared Function fnFindControl(ByVal ra_FrmCtrl As System.Windows.Forms.Control.ControlCollection) As System.Windows.Forms.Control
            Dim sFn As String = "Private Function fnFindChildControl(System.Windows.Forms.Control.ControlCollection, Collection) "

            Try
                Dim ctrl As System.Windows.Forms.Control

                For Each ctrl In ra_FrmCtrl
                    If ctrl.Controls.Count > 0 Then
                        fnFindControl(ctrl.Controls)
                    ElseIf ctrl.Controls.Count = 0 Then
                        moCtrlcol.Add(ctrl)
                    End If
                Next
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Function

    End Class

    Public Class DS_SpreadDesige
        Public Shared Sub sbInti(ByVal ro_spd As AxFPSpreadADO.AxfpSpread)

            With ro_spd
                .Font = New Font("굴림체", 9, FontStyle.Regular)

                .SelBackColor = Drawing.Color.FromArgb(213, 215, 255)
                .SelForeColor = SystemColors.InactiveBorder

                .ShadowColor = Drawing.Color.FromArgb(165, 186, 222)
                .ShadowDark = Color.DimGray
                .ShadowText = SystemColors.ControlText

                .GrayAreaBackColor = Drawing.Color.FromArgb(236, 242, 255)

            End With
        End Sub

    End Class

    Public Class DS_TabControl
        Public Shared MENU_TABCONTROL As Windows.Forms.TabControl
    End Class

    Public Class DS_StatusBar
        Public Shared MAIN_StatusBar As Windows.Forms.StatusBar

        Public Shared Sub setTextStatusBar(ByVal sStr As String)
            MAIN_StatusBar.Panels.Item(1).Text = sStr
        End Sub
    End Class

    Public Class DS_ProgressBar
        Public Shared MAIN_ProgressBar As Windows.Forms.ProgressBar
        Public Shared MAIN_pnlProgress As Windows.Forms.Panel


        Public Shared Property pvisible() As Boolean
            Get
                pvisible = MAIN_pnlProgress.Visible
            End Get
            Set(ByVal Value As Boolean)
                MAIN_pnlProgress.Visible = Value
                System.Windows.Forms.Application.DoEvents()
            End Set
        End Property

        Public Shared Sub PerformStep()
            MAIN_ProgressBar.PerformStep()
            System.Windows.Forms.Application.DoEvents()
        End Sub

    End Class

#End Region


End Namespace

