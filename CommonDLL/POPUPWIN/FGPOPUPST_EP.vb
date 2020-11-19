Imports COMMON.SVar

Imports LisDbCommand = InterSystems.Data.CacheClient.CacheCommand
Imports LisDbConnection = InterSystems.Data.CacheClient.CacheConnection
Imports LisDbDataAdapter = InterSystems.Data.CacheClient.CacheDataAdapter
Imports LisDbParameter = InterSystems.Data.CacheClient.CacheParameter
Imports LisDbTransaction = InterSystems.Data.CacheClient.CacheTransaction
Imports LisDbType = System.Data.DbType

Public Class FGPOPUPST_EP
    Inherits System.Windows.Forms.Form

    Private Const mc_sFile As String = "File : FGPOPUPST_EP.vb, Class : FGPOPUPST_EP" & vbTab

    Private Const mc_iXmargin_right As Integer = 5
    Private Const mc_iYmargin_bottom As Integer = 20

    Private m_frm As Windows.Forms.Form
    Private m_oledbcn As LisDbConnection
    Private msBcNo As String = ""
    Private msTClsCd As String = ""
    Private msTNm As String = ""
    Private msUsrID As String = ""

    Private msCrLf As String = Convert.ToChar(13) + Convert.ToChar(10)

    Private mbSave As Boolean = False
    Private mbActivated As Boolean = False

    Public ReadOnly Property Append() As Boolean
        Get
            Append = False
        End Get
    End Property

    Public WriteOnly Property UserID() As String
        Set(ByVal Value As String)
            msUsrID = Value
        End Set
    End Property

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal r_oledbcn As LisDbConnection, _
                                    ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsTNm As String) As ArrayList
        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm
        m_oledbcn = r_oledbcn
        msBcNo = rsBcNo
        msTClsCd = rsTClsCd
        msTNm = rsTNm

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbDisplayInit()

            sbDisplay_EP_Graph()

            sbDisplay_EP_Result()

            Me.Cursor = Windows.Forms.Cursors.Default

            Me.ShowDialog(r_frm)

            Dim stdatainfo As New StDataInfo
            Dim al_return As New ArrayList

            If mbSave Then
                '1) EP 그래프
                stdatainfo = New StDataInfo
                stdatainfo.Data = Me.picEP.Image
                stdatainfo.Alignment = 2
                al_return.Add(stdatainfo)
                stdatainfo = Nothing

                '2) 정렬문제 해결위한 Buffer -> 위와 같은 정렬 사용
                stdatainfo = New StDataInfo
                stdatainfo.Data = msCrLf + msCrLf
                stdatainfo.Alignment = 2
                al_return.Add(stdatainfo)
                stdatainfo = Nothing

                '3) EP 결과
                stdatainfo = New StDataInfo
                stdatainfo.Data = Me.txtEP.Text
                stdatainfo.Alignment = 0
                al_return.Add(stdatainfo)
                stdatainfo = Nothing
            End If

            Return al_return

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents picEP As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtEP As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_EP))
        Me.picEP = New System.Windows.Forms.PictureBox
        Me.txtEP = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        CType(Me.picEP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picEP
        '
        Me.picEP.BackColor = System.Drawing.Color.White
        Me.picEP.Location = New System.Drawing.Point(181, 90)
        Me.picEP.Name = "picEP"
        Me.picEP.Size = New System.Drawing.Size(349, 174)
        Me.picEP.TabIndex = 76
        Me.picEP.TabStop = False
        '
        'txtEP
        '
        Me.txtEP.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtEP.Location = New System.Drawing.Point(4, 376)
        Me.txtEP.Multiline = True
        Me.txtEP.Name = "txtEP"
        Me.txtEP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEP.Size = New System.Drawing.Size(724, 148)
        Me.txtEP.TabIndex = 77
        Me.txtEP.TabStop = False
        Me.txtEP.Text = resources.GetString("txtEP.Text")
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SlateBlue
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(66, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "EP Graph"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Navy
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(4, 352)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 79
        Me.Label2.Text = "EP Result"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSave
        '
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(504, 536)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(108, 36)
        Me.btnSave.TabIndex = 80
        Me.btnSave.Text = "저장(F2)"
        '
        'btnClose
        '
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(620, 536)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 36)
        Me.btnClose.TabIndex = 81
        Me.btnClose.Text = "닫기(Esc)"
        '
        'FGPOPUPST_EP
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(732, 589)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtEP)
        Me.Controls.Add(Me.picEP)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(740, 616)
        Me.MinimumSize = New System.Drawing.Size(740, 616)
        Me.Name = "FGPOPUPST_EP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "특수검사 모듈"
        CType(Me.picEP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Function fnGet_MaxValue(ByRef strValue() As String) As Integer

        Dim intMax As Integer
        Dim intTmp As Integer

        For i As Integer = 0 To strValue.Length - 1
            intTmp = Convert.ToInt32(strValue(i).Substring(1, 3), 16)
            If intTmp > intMax Then intMax = intTmp
        Next
        fnGet_MaxValue = intMax

    End Function

    Private Sub sbDisplay_EP_Graph()
        Dim sFn As String = "sbDisplay_EP_Graph"
        Dim intErr As Integer = 0

        '-- 2007.08.08 유은자 수정해야 하는 사항
        Try
            Dim dt As DataTable = (New DA_ST_EP).Get_EP_Graph(m_oledbcn, msBcNo, msTClsCd)

            If dt Is Nothing Then Return
            If dt.Rows.Count < 1 Then Return

            Dim sData_grp As String = dt.Rows(0).Item("graphdata").ToString().Split("|"c)(0)
            Dim sData_line As String = dt.Rows(0).Item("graphdata").ToString().Split("|"c)(1)
            Dim sData_Flag As String = ""

            If dt.Rows(0).Item("graphdata").ToString().Split("|"c).Length > 3 Then
                sData_Flag = dt.Rows(0).Item("graphdata").ToString().Split("|"c)(2) + "|" + dt.Rows(0).Item("graphdata").ToString().Split("|"c)(3)
            Else
                sData_Flag = "A|"
            End If


            Dim sGArea() As String
            Dim iPArea() As Integer

            If sData_grp.Length Mod 4 > 0 Or sData_grp.Length = 0 Then
                MsgBox("그래프의 길이에 오류가 있습니다. 확인하여 주십시요!!", MsgBoxStyle.Critical)
                Return
            End If

            'Pair로 존재해야함
            If (sData_line.Length Mod 4) Mod 2 > 0 Then
                MsgBox("Error!! : Length of coordinate peak!!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            '가장자리의 여백 설정
            Dim iMargin_Left As Integer = 20
            Dim iMargin_Right As Integer = 20
            Dim iMargin_Top As Integer = 10
            Dim iMargin_Bottom As Integer = 20

            Dim iHeight As Integer = picEP.Size.Height - iMargin_Top - iMargin_Bottom
            Dim iWidth As Integer = picEP.Size.Width - iMargin_Left - iMargin_Right

            '0) 이미지 및 그래픽 개체 생성
            Dim bmpEP As New System.Drawing.Bitmap(picEP.Width, picEP.Height)

            Dim g As Drawing.Graphics = Drawing.Graphics.FromImage(bmpEP)

            '-- 그래프 데이타
            Erase sGArea
            For i As Integer = 1 To sData_grp.Length \ 4
                ReDim Preserve sGArea(i - 1)
                sGArea(i - 1) = sData_grp.Substring((i - 1) * 4, 4)
            Next
            '-- Peack
            For i As Integer = 1 To sData_line.Length \ 4
                ReDim Preserve iPArea(i - 1)
                iPArea(i - 1) = sGArea.Length - CInt(sData_line.Substring((i - 1) * 4, 4))
            Next

            Dim iX_len As Integer = sGArea.Length
            Dim iY_max As Integer = fnGet_MaxValue(sGArea)

            Dim sngXDot As Single
            Dim sngYDot As Single

            sngXDot = Convert.ToSingle(iWidth) / Convert.ToSingle(sGArea.Length)

            sngYDot = 1
            If iHeight < iY_max Then
                sngYDot = Convert.ToSingle(iHeight / iY_max) '* 100
            End If

            '1) 초기화
            g.Clear(Drawing.Color.White)

            '2) 테두리 그리기
            g.DrawRectangle(New Drawing.Pen(System.Drawing.Color.Black, 1), 0, 0, Me.picEP.Width - 1, Me.picEP.Height - 1)

            '2-1) 10 분할
            For i As Integer = 1 To 9
                g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 1), 0, iMargin_Top + iHeight - (iHeight \ 10) * i, 5, iMargin_Top + iHeight - (iHeight \ 10) * i)
                g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 1), picEP.Size.Width, iMargin_Top + iHeight - (iHeight \ 10) * i, picEP.Size.Width - 5, iMargin_Top + iHeight - (iHeight \ 10) * i)
            Next

            '2-2) Base Line
            g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 2), iMargin_Left, iMargin_Top + iHeight, iMargin_Left + iWidth, iMargin_Top + iHeight)

            '3) 그래프 그리기
            Dim sngX As Single = 0, sngX_p As Single = 0
            Dim sngY As Single = 0, sngY_p As Single = iMargin_Top + iHeight

            '3-1) Edited Curve
            For i As Integer = 0 To sGArea.Length - 1
                Dim iData As Integer = 0

                iData = Convert.ToInt32(sGArea(i).Substring(1, 3), 16)     '16진수값 10진수로 변환
                sngX = Convert.ToSingle(iMargin_Left) + sngXDot * i

                '-- 2010/03/08 YEJ (urine 검체때문)
                '-- sngY = Convert.ToSingle(iMargin_Top + iHeight) - (sngYDot * iData)

                If sData_Flag.Split("|"c)(0) = "A" Then
                    sngY = Convert.ToSingle((iMargin_Top + iHeight) - (sngYDot * iData))
                ElseIf sData_Flag.Split("|"c)(0) = "M" Then
                    ''If Val(sData_Flag.Split("|"c)(1)) > 99 Then
                    ''    sngY = Convert.ToSingle(iMargin_Top + iHeight) - Convert.ToSingle((sngYDot * iData) / Convert.ToSingle(Val(sData_Flag.Split("|"c)(1)) / 100))
                    ''Else
                    ''    sngY = Convert.ToSingle(iMargin_Top + iHeight) - (sngYDot * iData)
                    ''End If
                    sngY = Convert.ToSingle(iMargin_Top + iHeight) - Convert.ToSingle((sngYDot * iData) / Convert.ToSingle(Val(sData_Flag.Split("|"c)(1)) / 100))
                End If
                '-- 2010/03/08 YEJ End

                Select Case sGArea(i).Substring(0, 1)
                    Case "0", "4"   '-- Normal, Del min SEP
                        If i > 0 Then
                            g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 1), sngX_p, sngY_p, sngX, sngY)
                        End If
                        sngX_p = sngX
                        sngY_p = sngY

                        Dim iPeak As Integer = 0

                        'Peak Area에 속하는지 조사
                        Try
                            If iPArea.Length \ 4 >= 2 Then
                                For j As Integer = 0 To (iPArea.Length - 1) \ 2
                                    If iPArea(2 * j) <= i And i <= iPArea(2 * j + 1) Then
                                        If iPArea(2 * j) = i Then
                                            iPeak = 1
                                        Else
                                            iPeak = i
                                        End If

                                        If iPeak > 0 Then
                                            Exit For
                                        End If
                                    End If
                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        ''Peak Area 존재하면 Fill Black
                        'If iPeak > 0 And i > 1 Then
                        '    If iPeak = 1 Then
                        '        g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 1), sngX_p, sngY_p, sngX, sngY)
                        '    Else
                        '        sngX = Convert.ToSingle(iMargin_Left + sngXDot * (i - 1))
                        '        For j As Integer = 1 To iWidth
                        '            sngY = iMargin_Top + iHeight - sngYDot * Convert.ToInt32(sGArea(i - 1).Substring(1, 3), 16) + ((Convert.ToInt32(sGArea(i - 1).Substring(1, 3), 16) - Convert.ToInt32(sGArea(i - 1).Substring(1, 3), 16)) / (sngXDot * j) * sngYDot)
                        '            g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 1), sngX + sngXDot * j, sngY_p, sngX + sngXDot * j, sngY)
                        '        Next
                        '    End If
                        'End If
                        'Peak Area 존재하면 Fill Black
                        If iPeak > 0 And i > 1 Then
                            g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 1), sngX_p, iMargin_Top + iHeight, sngX, sngY)
                        End If
                    Case "8", "C"   '-- Min SEP, Manually insert min SEP
                        If i > 0 Then
                            If Not (sGArea(i).Substring(0, 1) = "1" Or sGArea(i).Substring(0, 1) = "5") Then
                                g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 1), sngX_p, sngY_p, sngX, sngY)
                            End If
                        End If
                        sngX_p = sngX
                        sngY_p = sngY

                        'Fraction Line
                        g.DrawLine(New Drawing.Pen(Drawing.Color.Black, 2), sngX, sngY, sngX, iMargin_Top + iHeight + 5)
                    Case "1", "5"   '-- Del Frac, Del Frac & Del min SEP
                        sngX_p = sngX
                        sngY_p = sngY
                End Select
            Next

            Me.picEP.Image = bmpEP

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_EP_Result()
        Dim sFn As String = "sbDisplay_EP_Result"

        Try
            Dim dt As DataTable = (New DA_ST_EP).Get_EP_Result(m_oledbcn, msBcNo, msTClsCd)

            If dt Is Nothing Then Return
            If dt.Rows.Count < 1 Then Return

            sbDisplay_EP_Result_Pair(dt)

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_EP_Result_Pair(ByVal r_dt As DataTable)
        Dim sFn As String = "sbDisplay_EP_Result_Pair"

        Try
            Dim sBuf As String = ""
            Dim sLeftMargin As String = "".PadRight(4)

            Dim fn As COMMON.CommFn.Fn

            sBuf += sLeftMargin + fn.PadRightH("검사항목명", 16)
            sBuf += fn.PadRightH("결과", 10)
            sBuf += fn.PadRightH("", 5)
            sBuf += fn.PadRightH("%", 15)
            If (New DA_ST_EP).Get_Ep_Spc_UrineYn(m_oledbcn, msBcNo) = "Y" Then
                sBuf += fn.PadRightH("참고치(mg/dL)", 20)
            Else
                sBuf += fn.PadRightH("참고치(g/dL)", 20)
            End If
            sBuf += fn.PadRightH("참고치(%)", 30)
            sBuf += msCrLf

            sBuf += sLeftMargin + "".PadRight(95, "-"c)
            sBuf += msCrLf

            'Dim a_dr() As DataRow
            'a_dr = r_dt.Select("", "tclscd asc")

            'FRTNO(, FRTNM, FRTRST, FRTCONC, FRTHL, FRTREF, FRTGBN)

            For intIdx As Integer = 0 To r_dt.Rows.Count - 1

                If r_dt.Rows(intIdx).Item("frtgbn").ToString() = "T" Then
                    sBuf += sLeftMargin + "".PadRight(95, "-"c)
                    sBuf += msCrLf
                End If

                Dim strHL_p As String = r_dt.Rows(intIdx).Item("frthl").ToString().PadRight(2, "0"c)
                Dim strHL_c As String = ""

                strHL_c = strHL_p.Substring(1) : If strHL_c = "0" Then strHL_c = ""
                strHL_p = strHL_p.Substring(0, 1) : If strHL_p = "0" Then strHL_p = ""

                Dim strRef_p As String = r_dt.Rows(intIdx).Item("frtref").ToString()
                Dim strRef_c As String = ""

                If strRef_p.IndexOf("^") > 0 Then
                    strRef_c = strRef_p.Substring(strRef_p.IndexOf("^") + 1)
                    strRef_p = strRef_p.Substring(0, strRef_p.IndexOf("^"))
                End If

                sBuf += sLeftMargin + fn.PadRightH(r_dt.Rows(intIdx).Item("frtnm").ToString(), 16)
                sBuf += fn.PadRightH(r_dt.Rows(intIdx).Item("frtconc").ToString(), 10)
                sBuf += fn.PadRightH(strHL_c, 5)
                sBuf += fn.PadRightH(r_dt.Rows(intIdx).Item("frtrst").ToString(), 10)
                sBuf += fn.PadRightH(strHL_p, 5)
                sBuf += fn.PadRightH(strRef_c, 20)
                sBuf += fn.PadRightH(strRef_p, 30)
                sBuf += msCrLf
            Next

            Me.txtEP.Text = sBuf

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_EP_Result_Single(ByVal r_dt As DataTable)
        Dim sFn As String = "sbDisplay_EP_Result_Single"

        Try
            Dim sBuf As String = ""
            Dim sLeftMargin As String = "".PadRight(4)

            Dim fn As COMMON.CommFn.Fn

            sBuf += sLeftMargin + fn.PadRightH("검사항목명", 25)
            sBuf += fn.PadRightH("결과", 10)
            sBuf += fn.PadRightH("", 5)
            sBuf += fn.PadRightH("참고치", 20)
            sBuf += fn.PadRightH("", 15)
            sBuf += fn.PadRightH("", 20)
            sBuf += msCrLf

            sBuf += sLeftMargin + "".PadRight(95, "-"c)
            sBuf += msCrLf

            Dim a_dr() As DataRow

            a_dr = r_dt.Select("", "tclscd asc")

            For i As Integer = 1 To a_dr.Length
                sBuf += sLeftMargin + fn.PadRightH(a_dr(i - 1).Item("tnms").ToString(), 25)
                sBuf += fn.PadRightH(a_dr(i - 1).Item("viewrst").ToString(), 10)
                sBuf += fn.PadRightH(a_dr(i - 1).Item("judgmark").ToString() + a_dr(i - 1).Item("panicmark").ToString(), 5)
                sBuf += fn.PadRightH(a_dr(i - 1).Item("reftxt").ToString(), 20)
                sBuf += fn.PadRightH("", 15)
                sBuf += fn.PadRightH("", 20)
                sBuf += msCrLf
            Next

            Me.txtEP.Text = sBuf

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            '타이틀
            Me.Text += " ː " + msTNm

            '위치
            Dim iLeft As Integer = COMMON.CommFn.Ctrl.FindControlLeft(m_frm)
            Dim iTop As Integer = COMMON.CommFn.Ctrl.FindControlTop(m_frm) + COMMON.CommFn.Ctrl.menuHeight

            iLeft += m_frm.Width - Me.Width - mc_iXmargin_right
            iTop += m_frm.Height - Me.Height - mc_iYmargin_bottom

            Me.Left = iLeft
            Me.Top = iTop

            '초기화
            Me.txtEP.Text = ""

        Catch ex As Exception
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    '<----- Control Event ----->
    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            btnClose_Click(Me.btnClose, Nothing)
        ElseIf e.KeyCode = Windows.Forms.Keys.F2 Then
            btnSave_Click(Me.btnSave, Nothing)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        mbSave = False

        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        mbSave = True

        Me.Close()
    End Sub
End Class

Public Class DA_ST_EP
    Public Function Get_Ep_Spc_UrineYn(ByVal r_oledbcn As LisDbConnection, ByVal rsBcNo As String) As String
        Dim oledbcn As LisDbConnection = r_oledbcn
        Dim OleDbCmd As New LisDbCommand
        Dim OleDbDA As LisDbDataAdapter
        Dim dt As New DataTable

        Dim sSql As String = ""

        sSql = ""
        sSql += " select distinct f3.spcnm"
        sSql += "   from kll.lj011m j, kll.lf030m f3"
        sSql += "  where j.bcno = ?"
        sSql += "    and j.spccd = f3.spccd"
        sSql += "    and f3.usdt <= j.colldt"
        sSql += "    and f3.uedt > j.colldt"

        OleDbCmd.Connection = oledbcn
        OleDbCmd.CommandType = CommandType.Text
        OleDbCmd.CommandText = sSql

        OleDbDA = New LisDbDataAdapter(OleDbCmd)

        With OleDbDA
            .SelectCommand.Parameters.Clear()
            .SelectCommand.Parameters.Add("bcno", OleDb.OleDbType.VarChar).Value = rsBcNo
        End With

        dt.Reset()
        OleDbDA.Fill(dt)

        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("spcnm").ToString.ToLower().IndexOf("urine") >= 0 Then
                Return "Y"
            Else
                Return ""
            End If
        Else
            Return ""
        End If

    End Function

    Public Function Get_EP_Graph(ByVal r_oledbcn As LisDbConnection, ByVal rsBcNo As String, ByVal rsTClsCd As String) As DataTable
        Dim oledbcn As LisDbConnection = r_oledbcn
        Dim OleDbCmd As New LisDbCommand
        Dim OleDbDA As LisDbDataAdapter
        Dim dt As New DataTable

        Dim sSql As String = ""

        sSql = ""
        sSql += " select bcno, graphdata, linedata"
        sSql += "   from kll.lrg10m"
        sSql += "  where bcno = ?"
        sSql += "    and tclscd = ?"

        OleDbCmd.Connection = oledbcn
        OleDbCmd.CommandType = CommandType.Text
        OleDbCmd.CommandText = sSql

        OleDbDA = New LisDbDataAdapter(OleDbCmd)

        With OleDbDA
            .SelectCommand.Parameters.Clear()
            .SelectCommand.Parameters.Add("bcno", OleDb.OleDbType.VarChar).Value = rsBcNo
            .SelectCommand.Parameters.Add("tclscd", OleDb.OleDbType.VarChar).Value = rsTClsCd
        End With

        dt.Reset()
        OleDbDA.Fill(dt)

        Return dt
    End Function

    Public Function Get_EP_Result(ByVal r_oledbcn As LisDbConnection, ByVal rsBcNo As String, ByVal rsTClsCd As String) As DataTable
        Dim oledbcn As LisDbConnection = r_oledbcn
        Dim OleDbCmd As New LisDbCommand
        Dim OleDbDA As LisDbDataAdapter

        Dim dt As New DataTable

        Dim sSql As String = ""

        sSql = ""
        sSql += "select FRTNO, FRTNM, FRTRST, FRTCONC, FRTHL, FRTREF, FRTGBN "
        sSql += "from kll.LRI10M "
        sSql += "where BCNO = ? "
        sSql += "and tclscd like ? || '%' "

        OleDbCmd.Connection = oledbcn
        OleDbCmd.CommandType = CommandType.Text
        OleDbCmd.CommandText = sSql

        OleDbDA = New LisDbDataAdapter(OleDbCmd)

        With OleDbDA
            .SelectCommand.Parameters.Clear()
            .SelectCommand.Parameters.Add("bcno", OleDb.OleDbType.VarChar).Value = rsBcNo
            .SelectCommand.Parameters.Add("tclscd", OleDb.OleDbType.VarChar).Value = rsTClsCd
        End With

        dt.Reset()
        OleDbDA.Fill(dt)

        Return dt
    End Function


End Class
