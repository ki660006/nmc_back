Imports System.Drawing

Namespace ComFN
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

    Public Class MdiMain
        Public Shared Frm As Windows.Forms.Form
    End Class

End Namespace

