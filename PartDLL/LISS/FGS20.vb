'>>> 병원체검체검사 자동등록
Imports System.Windows.Forms
Imports System.Text
Imports COMMON.CommFN
Imports COMMON.SVar
Imports LISAPP.APP_S.RstSrh
Imports COMMON.CommLogin.LOGIN
Imports System.Drawing

Public Class FGS20
    Private Const msXMLDir As String = "\XML"
    Private msSlipFile As String = Application.StartupPath & msXMLDir & "\FGS20_SLIP.XML"
    Private mbQuery As Boolean = False
    Private m_tooltip As New Windows.Forms.ToolTip
    Private m_Groupcd As String = ""

    Private Sub sbFilterOff()
        With Me.spdList
            .ReDraw = False

            For i As Integer = 1 To .MaxRows
                .Row = i
                If .RowHidden Then
                    .RowHidden = False
                End If
            Next

            .ShadowColor = System.Drawing.Color.FromArgb(224, 224, 224)

            .ReDraw = True
        End With

    End Sub


    Private Sub sbDisplay_Init()

        Me.spdList.MaxRows = 0
        txtSelTest.Text = ""

    End Sub

    Protected Sub sbDisplay_DataView(ByVal r_dt As DataTable)

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList
            Dim iRow As Integer = 0
            Dim sKey As String = ""

            With spd
                .MaxRows = 0

                .ReDraw = False
                .MaxRows = 0

                For ix1 As Integer = 1 To r_dt.Rows.Count
                    If sKey <> r_dt.Rows(ix1 - 1).Item("bcno").ToString + r_dt.Rows(ix1 - 1).Item("testcd").ToString Then
                        .MaxRows += 1
                        iRow += 1
                    End If
                    sKey = r_dt.Rows(ix1 - 1).Item("bcno").ToString + r_dt.Rows(ix1 - 1).Item("testcd").ToString

                    For ix2 As Integer = 1 To r_dt.Columns.Count
                        Dim intCol As Integer = 0

                        intCol = .GetColFromID(r_dt.Columns(ix2 - 1).ColumnName.ToLower())

                        If intCol > 0 Then
                            .Row = iRow
                            .Col = intCol

                            If .Col > -1 And r_dt.Rows(ix1 - 1).Item(ix2 - 1).ToString() <> "" Then
                                .Text = r_dt.Rows(ix1 - 1).Item(ix2 - 1).ToString()
                            End If
                        End If
                    Next
                Next
                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    Private Sub sbDisplay_Data(ByVal rsBcno As String)
        Try


            Dim sOrdDtS As String = "", sOrdDtE As String = "", sIoGbn As String = ""
            Dim sDW_Items As String = ""
            Dim sDW_Gbn As String = ""
            Dim introw As Integer = 0

            Dim rdoGbn As String = ""

            If Me.rdoAll.Checked Then
                rdoGbn = ""
            ElseIf Me.rdoNSend.Checked Then
                rdoGbn = "X"
            ElseIf Me.rdoSend.Checked Then
                rdoGbn = "Y"
            End If

            sOrdDtS = Me.dtpDate0.Text.Replace("-", "")
            sOrdDtE = Me.dtpDateE0.Text.Replace("-", "")

            Dim sTestCds As String = ""

            If Me.txtSelTest.Text <> "" Then
                sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")
                ' sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"
            End If
            sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"

            Dim dt As DataTable = fn_get_HosRst(sOrdDtS, sOrdDtE, sTestCds, Ctrl.Get_Code(Me.cboPartSlip), rsBcno)

            If dt.Rows.Count < 1 Then Return

            If rdoGbn <> "" Then
                If rdoGbn = "X" Then
                    dt = Fn.ChangeToDataTable(dt.Select("state <> 'Y'"))
                ElseIf rdoGbn = "Y" Then
                    dt = Fn.ChangeToDataTable(dt.Select("state = '" + rdoGbn + "'"))
                End If
            End If

            If Me.rdoRstExY.Checked Then
                Dim sRstex As String = Ctrl.Get_Name(Me.cboRstEx)
                dt = Fn.ChangeToDataTable(dt.Select("orgrsts like '%" + sRstex + "%'"))
            End If

            If rdoAnd.Checked Then

                Dim sWhere As String = ""

                If Me.chkBaccd.Checked Then
                    If Me.txtBacFilter.Text.Trim <> "" Then
                        Dim sBaccd As String = Me.txtBacFilter.Tag.ToString
                        If sBaccd.IndexOf(",") > -1 Then
                            Dim sbaclist() As String = sBaccd.Split(","c)
                            Dim sbacsum As String = ""
                            For ix As Integer = 0 To sbaclist.Length - 1
                                sbacsum += "'" + sbaclist(ix).ToString() + "'"
                                If ix < sbaclist.Length - 1 Then
                                    sbacsum += ","
                                End If
                            Next
                            ' dt = Fn.ChangeToDataTable(dt.Select("bacrst in (" + sbacsum + ")"))
                            sWhere += "bacrst in (" + sbacsum + ")"
                        Else
                            'dt = Fn.ChangeToDataTable(dt.Select("bacrst = '" + sBaccd + "'"))
                            sWhere += "bacrst = '" + sBaccd + "'"
                        End If

                    End If
                End If

                If Me.chkAnti.Checked Then

                    If Me.txtAntiFilter.Tag.ToString.Trim <> "" Then
                        'Dim sAnticd As String = Me.txtAntiFilter.Text.Replace(Chr(3), "")
                        'dt = Fn.ChangeToDataTable(dt.Select("antirst like '%" + sAnticd + "%'"))
                        With Me.spdList
                            '1) 필터의 갯수만큼 split
                            Dim sAntis() As String = Me.txtAntiFilter.Tag.ToString.Split(Chr(3))
                            Dim sAntirst As String = ""
                            If sAntis.Length > 0 Then
                                '2) 스플릿한 갯수만큼 for(1) 로 있으면 add 없으면 pass
                                Dim dt_merge As New DataTable
                                For ix As Integer = 0 To sAntis.Length - 1
                                    sAntirst = sAntis(ix)

                                    If sAntirst.Trim <> "" Then
                                        If sWhere <> "" Then
                                            sWhere += "antirst like '%" + sAntirst + "%'"
                                        Else
                                            sWhere += "and antirst like '%" + sAntirst + "%'"
                                        End If

                                    End If
                                Next

                            End If
                            '##########################중복데이터 처리할 부분 해야함
                        End With
                    End If

                End If

                dt = Fn.ChangeToDataTable(dt.Select(sWhere, ""))

            ElseIf rdoOr.Checked Then
                If Me.chkBaccd.Checked Then
                    If Me.txtBacFilter.Text.Trim <> "" Then
                        Dim sBaccd As String = Me.txtBacFilter.Tag.ToString
                        If sBaccd.IndexOf(",") > -1 Then
                            Dim sbaclist() As String = sBaccd.Split(","c)
                            Dim sbacsum As String = ""
                            For ix As Integer = 0 To sbaclist.Length - 1
                                sbacsum += "'" + sbaclist(ix).ToString() + "'"
                                If ix < sbaclist.Length - 1 Then
                                    sbacsum += ","
                                End If
                            Next
                            dt = Fn.ChangeToDataTable(dt.Select("bacrst in (" + sbacsum + ")"))
                        Else
                            dt = Fn.ChangeToDataTable(dt.Select("bacrst = '" + sBaccd + "'"))
                        End If

                    End If
                End If

                If Me.chkAnti.Checked Then

                    If Me.txtAntiFilter.Tag.ToString.Trim <> "" Then
                        'Dim sAnticd As String = Me.txtAntiFilter.Text.Replace(Chr(3), "")
                        'dt = Fn.ChangeToDataTable(dt.Select("antirst like '%" + sAnticd + "%'"))
                        With Me.spdList
                            '1) 필터의 갯수만큼 split
                            Dim sAntis() As String = Me.txtAntiFilter.Tag.ToString.Split(Chr(3))
                            Dim sAntirst As String = ""
                            If sAntis.Length > 0 Then
                                '2) 스플릿한 갯수만큼 for(1) 로 있으면 add 없으면 pass
                                Dim dt_merge As New DataTable
                                For ix As Integer = 0 To sAntis.Length - 1
                                    sAntirst = sAntis(ix)

                                    If sAntirst.Trim <> "" Then
                                        Dim dt_filter As DataTable = Fn.ChangeToDataTable(dt.Select("antirst like '%" + sAntirst + "%'"))

                                        If dt_filter.Rows.Count > 0 Then
                                            dt_merge.Merge(dt_filter)
                                        End If
                                    End If
                                Next
                                dt.Merge(dt_merge)
                            End If
                            '##########################중복데이터 처리할 부분 해야함
                        End With
                    End If

                End If

            End If

          

            If dt.Rows.Count < 1 Then Return

            dt = Fn.ChangeToDataTable(dt.Select("", "bcno"))



            Dim sBcnoO As String = ""
            Dim sBcnoN As String = ""

            With Me.spdList
                .ReDraw = False
                '.MaxRows = 0

                For ix As Integer = 0 To dt.Rows.Count - 1

                    '의뢰기관
                    sBcnoN = dt.Rows(ix).Item("bcno").ToString()

                    'If .MaxRows > 0 Then
                    '    For i As Integer = 0 To .MaxRows - 1
                    '        .Col = .GetColFromID("bcno")
                    '        If .Text = sBcnoN Then
                    '            MsgBox("동일한 검체번호가 존재합니다.")
                    '            Return
                    '        End If
                    '    Next
                    'End If


                    If sBcnoN <> sBcnoO Then
                        .MaxRows += 1
                        .Row = .MaxRows

                        .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                        .Col = .GetColFromID("reqhospicd") : .Text = dt.Rows(ix).Item("hospinm").ToString
                        .Col = .GetColFromID("reqhospinm") : .Text = dt.Rows(ix).Item("hospital").ToString
                        .Col = .GetColFromID("reqhospiusr") : .Text = dt.Rows(ix).Item("usrnm").ToString
                        '검체정보
                        .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                        .Col = .GetColFromID("sex") : .Text = dt.Rows(ix).Item("sex").ToString
                        .Col = .GetColFromID("birth") : .Text = dt.Rows(ix).Item("birth").ToString
                        .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                        .Col = .GetColFromID("deptcd") : .Text = dt.Rows(ix).Item("deptcd").ToString

                        '검체를 질본코드로 변환 
                        If dt.Rows(ix).Item("spc").ToString <> "" Then
                            Dim sSpcinfo As String = dt.Rows(ix).Item("spc").ToString
                            .Col = .GetColFromID("spc")
                            If fnGetRefcd(sSpcinfo.Split("/"c)(0)) <> "" Then
                                .Text = fnGetRefcd(sSpcinfo.Split("/"c)(0))
                                .Tag = sSpcinfo.Split("/"c)(1)
                            Else
                                .Text = sSpcinfo.Split("/"c)(1)
                            End If
                        End If

                        .Col = .GetColFromID("spcetc") : .Text = dt.Rows(ix).Item("spcnmd").ToString
                        .Col = .GetColFromID("test") : .Text = dt.Rows(ix).Item("etc").ToString
                        .Col = .GetColFromID("testetc") : .Text = dt.Rows(ix).Item("etc2").ToString
                        '병원체
                        Dim sRefcd As String = dt.Rows(ix).Item("refcd").ToString

                        If sRefcd = "" Then
                            Dim dt_refcd As New DataTable
                            dt_refcd = fn_get_refcd_for_bcno(dt.Rows(ix).Item("bcno").ToString)
                            If dt_refcd.Rows.Count > 0 Then
                                sRefcd = dt_refcd.Rows(0).Item("refcd").ToString
                            ElseIf dt_refcd.Rows.Count < 1 Then
                                sRefcd = ""
                            End If
                            .Col = .GetColFromID("refcd") : .Text = sRefcd
                        Else
                            ' m_Groupcd = dt_refcd.Rows(0).Item("groupcd").ToString
                            .Col = .GetColFromID("refcd") : .Text = dt.Rows(ix).Item("refcd").ToString.Replace(",", "")
                        End If


                        '발생정보
                        .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString
                        .Col = .GetColFromID("fndt") : .Text = dt.Rows(ix).Item("fndt").ToString
                        '검사기관
                        .Col = .GetColFromID("hospino") : .Text = dt.Rows(ix).Item("hospinm2").ToString
                        .Col = .GetColFromID("testusr") : .Text = dt.Rows(ix).Item("fnnm").ToString '검사 최종보고 자 
                        .Col = .GetColFromID("rptusr") : .Text = dt.Rows(ix).Item("rptnm").ToString '질본보고자 ( 로그인 자 )

                        .Col = .GetColFromID("errmsg") : .Text = dt.Rows(ix).Item("errmsg").ToString
                        .Col = .GetColFromID("orgrsts") : .Text = dt.Rows(ix).Item("orgrsts").ToString

                        If dt.Rows(ix).Item("state").ToString = "Y" Then
                            .Col = .GetColFromID("state") : .Text = "전송완료"
                            .BackColor = Color.LightGreen

                            Dim dt_reginfo As DataTable = fn_get_HosRst_Reginfo(dt.Rows(ix).Item("bcno").ToString)

                            If dt_reginfo.Rows.Count <= 0 Then
                                .Col = .GetColFromID("rptusr") : .Text = ""
                            Else
                                .Col = .GetColFromID("rptusr") : .Text = dt_reginfo.Rows(0).Item("rptusr").ToString

                                Dim sRtnmsg As String = dt_reginfo.Rows(0).Item("sendmsg").ToString()
                                Dim aRtnmsgInfo() As String = sRtnmsg.Split("&"c)

                                For i As Integer = 0 To aRtnmsgInfo.Length - 3
                                    If aRtnmsgInfo(i).Split("="c)(0) = "spm_ty_list" Then
                                        .Col = .GetColFromID("spc") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    ElseIf aRtnmsgInfo(i).Split("="c)(0) = "spm_ty_etc" Then
                                        .Col = .GetColFromID("spcetc") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    ElseIf aRtnmsgInfo(i).Split("="c)(0) = "inspct_mth_ty_list" Then
                                        .Col = .GetColFromID("test") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    ElseIf aRtnmsgInfo(i).Split("="c)(0) = "inspct_mth_ty_etc" Then
                                        .Col = .GetColFromID("testetc") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    ElseIf aRtnmsgInfo(i).Split("="c)(0) = "pthgogan_cd" Then
                                        .Col = .GetColFromID("refcd") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    End If
                                Next

                            End If

                        ElseIf dt.Rows(ix).Item("state").ToString = "N" Then
                            .Col = .GetColFromID("state") : .Text = "전송실패"
                            .BackColor = Color.Coral
                        Else
                            .Col = .GetColFromID("state") : .Text = "미전송"
                        End If

                        sBcnoO = sBcnoN
                    End If

                Next
                .ReDraw = True
            End With



            'If dt.Rows.Count < 1 Then
            '    MsgBox("조회된 항목이 없습니다.")
            '    Return
            'End If


            'With Me.spdList
            '    .ReDraw = False
            '    .MaxRows += 1
            '    intRow = .MaxRows
            '    For ix As Integer = 0 To dt.Rows.Count - 1
            '        .Row = introw

            '        '의뢰기관
            '        .Col = .GetColFromID("reqhospicd") : .Text = dt.Rows(ix).Item("hospinm").ToString
            '        .Col = .GetColFromID("reqhospinm") : .Text = dt.Rows(ix).Item("hospital").ToString
            '        .Col = .GetColFromID("reqhospiusr") : .Text = dt.Rows(ix).Item("usrnm").ToString
            '        '검체정보
            '        .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
            '        .Col = .GetColFromID("sex") : .Text = dt.Rows(ix).Item("sex").ToString
            '        .Col = .GetColFromID("birth") : .Text = dt.Rows(ix).Item("birth").ToString
            '        .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
            '        .Col = .GetColFromID("deptcd") : .Text = dt.Rows(ix).Item("deptcd").ToString

            '        If dt.Rows(ix).Item("spc").ToString <> "" Then
            '            Dim sSpcinfo As String = dt.Rows(ix).Item("spc").ToString
            '            .Col = .GetColFromID("spc")
            '            If fnGetRefcd(sSpcinfo.Split("/"c)(0)) <> "" Then
            '                .Text = fnGetRefcd(sSpcinfo.Split("/"c)(0))
            '                .Tag = sSpcinfo.Split("/"c)(1)
            '            Else
            '                .Text = sSpcinfo.Split("/"c)(1)
            '            End If

            '        End If
            '        .Col = .GetColFromID("spcetc") : .Text = dt.Rows(ix).Item("spcnmd").ToString
            '        .Col = .GetColFromID("test") : .Text = dt.Rows(ix).Item("etc").ToString
            '        .Col = .GetColFromID("testetc") : .Text = dt.Rows(ix).Item("etc2").ToString
            '        '병원체
            '        .Col = .GetColFromID("refcd") : .Text = dt.Rows(ix).Item("refcd").ToString
            '        '발생정보
            '        .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString
            '        .Col = .GetColFromID("fndt") : .Text = dt.Rows(ix).Item("fndt").ToString
            '        '검사기관
            '        .Col = .GetColFromID("hospino") : .Text = dt.Rows(ix).Item("hospinm2").ToString
            '        .Col = .GetColFromID("testusr") : .Text = dt.Rows(ix).Item("fnnm").ToString '검사 최종보고 자 
            '        .Col = .GetColFromID("rptusr") : .Text = dt.Rows(ix).Item("rptnm").ToString '질본보고자 ( 로그인 자 )
            '        .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
            '        .Col = .GetColFromID("errmsg") : .Text = dt.Rows(ix).Item("errmsg").ToString
            '        .Col = .GetColFromID("orgrsts") : .Text = dt.Rows(ix).Item("orgrsts").ToString



            '        If dt.Rows(ix).Item("state").ToString = "Y" Then
            '            .Col = .GetColFromID("state") : .Text = "전송완료"
            '            .BackColor = Color.LightGreen
            '        ElseIf dt.Rows(ix).Item("state").ToString = "N" Then
            '            .Col = .GetColFromID("state") : .Text = "전송실패"
            '            .BackColor = Color.Coral
            '        Else
            '            .Col = .GetColFromID("state") : .Text = "미전송"
            '        End If


            '    Next
            '    .ReDraw = True
            'End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub
    Private Sub sbSpccd_Map()
        Try
            Dim dt As DataTable = fnGetRefSpccd()


            With Me.spdRefSpccd
                .MaxRows = 0

                For ix As Integer = 0 To dt.Rows.Count - 1
                    Dim sSpccd As String = dt.Rows(ix).Item("spccd").ToString
                    Dim sRefcd As String = dt.Rows(ix).Item("refcd").ToString

                    If sSpccd.IndexOf(",") > 0 Then

                        Dim sSpccds As String() = sSpccd.Split(","c)

                        For iy As Integer = 0 To sSpccds.Length - 1

                            .MaxRows += 1
                            .Row = .MaxRows
                            .Col = .GetColFromID("spccd") : .Text = sSpccds(iy)
                            .Col = .GetColFromID("refcd") : .Text = sRefcd

                        Next
                    Else
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("spccd") : .Text = sSpccd
                        .Col = .GetColFromID("refcd") : .Text = sRefcd

                    End If

                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Data()

        Try
            Me.spdList.MaxRows = 0

            m_Groupcd = ""

            Dim sOrdDtS As String = "", sOrdDtE As String = "", sIoGbn As String = ""
            Dim sDW_Items As String = ""
            Dim sDW_Gbn As String = ""

            If txtSelTest.Text = "" Then
                MsgBox("검사항목을 선택하세요.")
                Return
            End If

            sOrdDtS = Me.dtpDate0.Text.Replace("-", "")
            sOrdDtE = Me.dtpDateE0.Text.Replace("-", "")
            Dim sTestCds As String = ""

            If Me.txtSelTest.Text <> "" Then
                sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")
                ' sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"
            End If
            sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"

            Dim rdoGbn As String = ""

            If Me.rdoAll.Checked Then
                rdoGbn = ""
            ElseIf Me.rdoNSend.Checked Then
                rdoGbn = "X"
            ElseIf Me.rdoSend.Checked Then
                rdoGbn = "Y"
            End If

            If Ctrl.Get_Code(Me.cboPartSlip).Substring(0, 1) = "O" Then
                MsgBox("외부의뢰 검사는 신고하실수 없습니다.")
            End If

            ''''조회'''
            Dim dt As DataTable = fn_get_HosRst(sOrdDtS, sOrdDtE, sTestCds, Ctrl.Get_Code(Me.cboPartSlip), "")
            If dt.Rows.Count < 1 Then Return

            If rdoGbn <> "" Then
                If rdoGbn = "X" Then
                    dt = Fn.ChangeToDataTable(dt.Select("state <> 'Y'"))
                ElseIf rdoGbn = "Y" Then
                    dt = Fn.ChangeToDataTable(dt.Select("state = '" + rdoGbn + "'"))
                End If
            End If

            '2018-07-19 yjh 전송구분 미전송 또는 전송으로 놨을 때 select 되어 내용이 없을 경우 리턴
            If dt.Rows.Count < 1 Then Return

            If Me.rdoRstExY.Checked Then
                Dim sRstex As String = Ctrl.Get_Name(Me.cboRstEx)
                dt = Fn.ChangeToDataTable(dt.Select("orgrsts like '%" + sRstex + "%'"))
            End If

            '2018-07-19 select 되어 내용이 없을 경우 리턴
            If dt.Rows.Count < 1 Then Return

            If rdoAnd.Checked Then

                Dim sWhere As String = ""

                If Me.chkBaccd.Checked Then
                    If Me.txtBacFilter.Text.Trim <> "" Then
                        Dim sBaccd As String = Me.txtBacFilter.Tag.ToString
                        If sBaccd.IndexOf(",") > -1 Then
                            Dim sbaclist() As String = sBaccd.Split(","c)
                            Dim sbacsum As String = ""
                            For ix As Integer = 0 To sbaclist.Length - 1
                                sbacsum += "'" + sbaclist(ix).ToString() + "'"
                                If ix < sbaclist.Length - 1 Then
                                    sbacsum += ","
                                End If
                            Next
                            ' dt = Fn.ChangeToDataTable(dt.Select("bacrst in (" + sbacsum + ")"))
                            sWhere += "bacrst in (" + sbacsum + ")"
                        Else
                            'dt = Fn.ChangeToDataTable(dt.Select("bacrst = '" + sBaccd + "'"))
                            sWhere += "bacrst = '" + sBaccd + "'"
                        End If

                    End If
                End If

                If Me.chkAnti.Checked Then

                    If Me.txtAntiFilter.Tag.ToString.Trim <> "" Then
                        'Dim sAnticd As String = Me.txtAntiFilter.Text.Replace(Chr(3), "")
                        'dt = Fn.ChangeToDataTable(dt.Select("antirst like '%" + sAnticd + "%'"))
                        With Me.spdList
                            '1) 필터의 갯수만큼 split
                            Dim sAntis() As String = Me.txtAntiFilter.Tag.ToString.Split(Chr(3))
                            Dim sAntirst As String = ""
                            If sAntis.Length > 0 Then
                                '2) 스플릿한 갯수만큼 for(1) 로 있으면 add 없으면 pass
                                Dim dt_merge As New DataTable
                                For ix As Integer = 0 To sAntis.Length - 1
                                    sAntirst = sAntis(ix)

                                    If sAntirst.Trim <> "" Then
                                        If sWhere <> "" Then
                                            sWhere += " and antirst like '%" + sAntirst + "%'"
                                        Else
                                            sWhere += " antirst like '%" + sAntirst + "%'"
                                        End If

                                    End If
                                Next

                            End If
                            '##########################중복데이터 처리할 부분 해야함
                        End With
                    End If

                End If

                dt = Fn.ChangeToDataTable(dt.Select(sWhere, ""))

            ElseIf rdoOr.Checked Then
                If Me.chkBaccd.Checked Then
                    If Me.txtBacFilter.Text.Trim <> "" Then
                        Dim sBaccd As String = Me.txtBacFilter.Tag.ToString
                        If sBaccd.IndexOf(",") > -1 Then
                            Dim sbaclist() As String = sBaccd.Split(","c)
                            Dim sbacsum As String = ""
                            For ix As Integer = 0 To sbaclist.Length - 1
                                sbacsum += "'" + sbaclist(ix).ToString() + "'"
                                If ix < sbaclist.Length - 1 Then
                                    sbacsum += ","
                                End If
                            Next
                            dt = Fn.ChangeToDataTable(dt.Select("bacrst in (" + sbacsum + ")"))
                        Else
                            dt = Fn.ChangeToDataTable(dt.Select("bacrst = '" + sBaccd + "'"))
                        End If

                    End If
                End If

                If Me.chkAnti.Checked Then

                    If Me.txtAntiFilter.Tag.ToString.Trim <> "" Then
                        'Dim sAnticd As String = Me.txtAntiFilter.Text.Replace(Chr(3), "")
                        'dt = Fn.ChangeToDataTable(dt.Select("antirst like '%" + sAnticd + "%'"))
                        With Me.spdList
                            '1) 필터의 갯수만큼 split
                            Dim sAntis() As String = Me.txtAntiFilter.Tag.ToString.Split(Chr(3))
                            Dim sAntirst As String = ""
                            If sAntis.Length > 0 Then
                                '2) 스플릿한 갯수만큼 for(1) 로 있으면 add 없으면 pass
                                Dim dt_merge As New DataTable
                                For ix As Integer = 0 To sAntis.Length - 1
                                    sAntirst = sAntis(ix)

                                    If sAntirst.Trim <> "" Then
                                        Dim dt_filter As DataTable = Fn.ChangeToDataTable(dt.Select("antirst like '%" + sAntirst + "%'"))

                                        If dt_filter.Rows.Count > 0 Then
                                            dt_merge.Merge(dt_filter)
                                        End If
                                    End If
                                Next
                                dt.Merge(dt_merge)
                            End If
                            '##########################중복데이터 처리할 부분 해야함
                        End With
                    End If

                End If

            End If

            If dt.Rows.Count < 1 Then Return

            dt = Fn.ChangeToDataTable(dt.Select("", "bcno"))

            Dim sBcnoO As String = ""
            Dim sBcnoN As String = ""

            With Me.spdList
                .ReDraw = False
                .MaxRows = 0

                For ix As Integer = 0 To dt.Rows.Count - 1

                    '의뢰기관
                    sBcnoN = dt.Rows(ix).Item("bcno").ToString()

                    If sBcnoN <> sBcnoO Then
                        .MaxRows += 1
                        .Row = ix + 1

                        'With Me.spdList
                        '    .ReDraw = False
                        '    .MaxRows = dt.Rows.Count
                        '    For ix As Integer = 0 To dt.Rows.Count - 1
                        '        .Row = ix + 1

                        

                        '의뢰기관
                        .Col = .GetColFromID("reqhospicd") : .Text = dt.Rows(ix).Item("hospinm").ToString
                        .Col = .GetColFromID("reqhospinm") : .Text = dt.Rows(ix).Item("hospital").ToString
                        .Col = .GetColFromID("reqhospiusr") : .Text = dt.Rows(ix).Item("usrnm").ToString
                        '검체정보
                        .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                        .Col = .GetColFromID("sex") : .Text = dt.Rows(ix).Item("sex").ToString
                        .Col = .GetColFromID("birth") : .Text = dt.Rows(ix).Item("birth").ToString
                        .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                        .Col = .GetColFromID("deptcd") : .Text = dt.Rows(ix).Item("deptcd").ToString

                        'bch 10.24  요청사항 추가 : 신고당일은 신고로 신고이전은 신고완료로 둘다 아니면 비워진상태로 
                        .Col = .GetColFromID("decla") : .Text = dt.Rows(ix).Item("decla").ToString

                        '검체를 질본코드로 변환 
                        If dt.Rows(ix).Item("spc").ToString <> "" Then
                            Dim sSpcinfo As String = dt.Rows(ix).Item("spc").ToString
                            .Col = .GetColFromID("spc")
                            If fnGetRefcd(sSpcinfo.Split("/"c)(0)) <> "" Then
                                .Text = fnGetRefcd(sSpcinfo.Split("/"c)(0))
                                .Tag = sSpcinfo.Split("/"c)(1)
                            Else
                                .Text = sSpcinfo.Split("/"c)(1)
                            End If
                        End If

                        .Col = .GetColFromID("spcetc") : .Text = dt.Rows(ix).Item("spcnmd").ToString
                        .Col = .GetColFromID("test") : .Text = dt.Rows(ix).Item("etc").ToString
                        .Col = .GetColFromID("testetc") : .Text = dt.Rows(ix).Item("etc2").ToString
                        '병원체
                        Dim sRefcd As String = dt.Rows(ix).Item("refcd").ToString

                        If sRefcd = "" Then
                            Dim dt_refcd As New DataTable
                            dt_refcd = fn_get_refcd_for_bcno(dt.Rows(ix).Item("bcno").ToString)
                            If dt_refcd.Rows.Count > 0 Then
                                sRefcd = dt_refcd.Rows(0).Item("refcd").ToString
                            ElseIf dt_refcd.Rows.Count < 1 Then
                                sRefcd = ""
                            End If
                            .Col = .GetColFromID("refcd") : .Text = sRefcd
                        Else
                            ' m_Groupcd = dt_refcd.Rows(0).Item("groupcd").ToString
                            .Col = .GetColFromID("refcd") : .Text = dt.Rows(ix).Item("refcd").ToString.Replace(",", "")
                        End If


                        '발생정보
                        .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString
                        .Col = .GetColFromID("fndt") : .Text = dt.Rows(ix).Item("fndt").ToString
                        '검사기관
                        .Col = .GetColFromID("hospino") : .Text = dt.Rows(ix).Item("hospinm2").ToString
                        .Col = .GetColFromID("testusr") : .Text = dt.Rows(ix).Item("fnnm").ToString '검사 최종보고 자 
                        .Col = .GetColFromID("rptusr") : .Text = dt.Rows(ix).Item("rptnm").ToString '질본보고자 ( 로그인 자 )
                        .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                        .Col = .GetColFromID("errmsg") : .Text = dt.Rows(ix).Item("errmsg").ToString
                        .Col = .GetColFromID("orgrsts") : .Text = dt.Rows(ix).Item("orgrsts").ToString

                        If dt.Rows(ix).Item("state").ToString = "Y" Then
                            .Col = .GetColFromID("state") : .Text = "전송완료"
                            .BackColor = Color.LightGreen

                            Dim dt_reginfo As DataTable = fn_get_HosRst_Reginfo(dt.Rows(ix).Item("bcno").ToString)

                            If dt_reginfo.Rows.Count <= 0 Then
                                .Col = .GetColFromID("rptusr") : .Text = ""
                            Else
                                .Col = .GetColFromID("rptusr") : .Text = dt_reginfo.Rows(0).Item("rptusr").ToString

                                Dim sRtnmsg As String = dt_reginfo.Rows(0).Item("sendmsg").ToString()
                                Dim aRtnmsgInfo() As String = sRtnmsg.Split("&"c)

                                '2018-08-28 yjh url 끝부분 spm_ty_list, inspct_mth_ty_list 같은 내용 반복되는 부분 제외
                                'For i As Integer = 0 To aRtnmsgInfo.Length - 1
                                For i As Integer = 0 To aRtnmsgInfo.Length - 3
                                    If aRtnmsgInfo(i).Split("="c)(0) = "spm_ty_list" Then
                                        .Col = .GetColFromID("spc") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    ElseIf aRtnmsgInfo(i).Split("="c)(0) = "spm_ty_etc" Then
                                        .Col = .GetColFromID("spcetc") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    ElseIf aRtnmsgInfo(i).Split("="c)(0) = "inspct_mth_ty_list" Then
                                        .Col = .GetColFromID("test") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    ElseIf aRtnmsgInfo(i).Split("="c)(0) = "inspct_mth_ty_etc" Then
                                        .Col = .GetColFromID("testetc") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    ElseIf aRtnmsgInfo(i).Split("="c)(0) = "pthgogan_cd" Then
                                        .Col = .GetColFromID("refcd") : .Text = aRtnmsgInfo(i).Split("="c)(1)
                                    End If
                                Next

                            End If

                        ElseIf dt.Rows(ix).Item("state").ToString = "N" Then
                            .Col = .GetColFromID("state") : .Text = "전송실패"
                            .BackColor = Color.Coral
                        Else
                            .Col = .GetColFromID("state") : .Text = "미전송"
                        End If

                        sBcnoO = sBcnoN
                    End If



                Next
                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub FGS17_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape : btnExit_ButtonClick(Nothing, Nothing)
            Case Keys.F4 : btnClear_ButtonClick(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnClear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Init()
    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGS17_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.WindowState = FormWindowState.Maximized

        '-- 서버날짜로 설정
        '  Me.dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")) 'CDate(dtpDate1.Value.AddDays(-1))

        sbDisplay_Init()
        sbDisplay_cboRstEx()
        sbDisplay_slip()

        Me.txtAntiFilter.Tag = ""
        Me.txtBacFilter.Tag = ""

    End Sub

    Private Sub sbDisplay_cboRstEx()
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_RstEx_List()

            Me.cboRstEx.Items.Clear()

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboRstEx.Items.Add("[" + dt.Rows(ix).Item("rstcd").ToString.Trim + "] " + dt.Rows(ix).Item("rstex").ToString)
            Next

            Me.cboRstEx.SelectedIndex = 0

            If Me.rdoRstExN.Checked Then
                Me.cboRstEx.Enabled = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sbDisplay_slip()
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List(, True)

            Me.cboPartSlip.Items.Clear()
            'cboSection.Items.Add("[--] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString.Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msSlipFile, "SLIP")

            If sTmp <> "" Then
                Me.cboPartSlip.SelectedIndex = CInt(IIf(sTmp = "", 0, sTmp))
            Else
                Me.cboPartSlip.SelectedIndex = 0
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            sbSpccd_Map()

            Me.spdList.MaxRows = 0

            If Me.txtBcNo.Text <> "" Then
                sbDisplay_Data(Me.txtBcNo.Text)
            Else
                sbDisplay_Data()
            End If


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub
    Private Function fnGetRefcd(ByVal rsSpccd As String) As String
        Try
            Dim sRtn As String = ""

            If rsSpccd <> "" Then

                With Me.spdRefSpccd
                    If .MaxRows > 0 Then
                        For ix As Integer = 0 To .MaxRows - 1
                            .Row = ix + 1
                            .Col = .GetColFromID("spccd")
                            Dim sSpdspc As String = .Text
                            If rsSpccd = sSpdspc Then
                                .Col = .GetColFromID("refcd")
                                sRtn = .Text
                                Return sRtn
                            End If
                        Next
                    End If

                End With
                
            End If

            Return sRtn
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Private Sub btnFilterN_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        sbFilterOff()
    End Sub


    Private Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", cboPartSlip.SelectedIndex.ToString)
        txtSelTest.Text = ""
    End Sub



    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim arlPrint As New ArrayList

            With spdList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    Dim strBuf() As String = rsTitle_Item.Split("|"c)
                    Dim arlItem As New ArrayList

                    For intIdx As Integer = 0 To strBuf.Length - 1

                        If strBuf(intIdx) = "" Then Exit For

                        Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

                        If intCol > 0 Then

                            Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                            Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                            Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                            .Row = intRow
                            .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                            arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                        End If
                    Next

                    Dim objPat As New FGS00_PATINFO

                    With objPat
                        .alItem = arlItem
                    End With

                    arlPrint.Add(objPat)
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGS00_PRINT

                prt.mbLandscape = True  '-- false : 세로, true : 가로
                prt.msTitle = "재검 내역 조회"
                prt.maPrtData = arlPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub


    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        With spdList
            For ix As Integer = 1 To .MaxCols

                .Row = 0 : .Col = ix
                If .ColID = "rst1" Or .ColID = "rst2" Or .ColID = "rst3" Or .ColID = "rst4" Or .ColID = "rst5" Then
                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                End If
                If .ColHidden = False Then
                    stu_item = New STU_PrtItemInfo

                    If .ColID = "regno" Or .ColID = "patnm" Or .ColID = "vbcno" Or .ColID = "regdt" Or .ColID = "regid" Or .ColID = "tnms" Or _
                       .ColID = "viewrst" Or .ColID = "rst1" Or .ColID = "rst2" Then
                        stu_item.CHECK = "1"
                    Else
                        stu_item.CHECK = "0"
                    End If

                    If .ColID = "rst1" Then
                        stu_item.TITLE = "재검 1차"
                    ElseIf .ColID = "rst2" Then
                        stu_item.TITLE = "재검 2차"
                    ElseIf .ColID = "rst3" Then
                        stu_item.TITLE = "재검 3차"
                    ElseIf .ColID = "rst4" Then
                        stu_item.TITLE = "재검 4차"
                    ElseIf .ColID = "rst5" Then
                        stu_item.TITLE = "재검 5차"
                    Else
                        stu_item.TITLE = .Text
                    End If

                    stu_item.FIELD = .ColID

                    If .ColID = "tatcont" Then
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10 + 50).ToString
                    Else
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString
                    End If
                    alItems.Add(stu_item)
                End If
            Next

        End With

        Return alItems

    End Function


    Private Sub btnCdHelp_test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Try

            Dim sWGrpCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sPartSlip As String = ""

            sPartSlip = Ctrl.Get_Code(Me.cboPartSlip)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_list_req(sPartSlip, sTGrpCd, sWGrpCd, , "")
            Dim a_dr As DataRow() = dt.Select("", "sort1, sort2, testcd") '(tcdgbn = 'P'OR titleyn = '0')

            dt = Fn.ChangeToDataTable(a_dr)
            objHelp.FormText = "검사목록"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            If Me.txtSelTest.Text <> "" Then objHelp.KeyCodes = Me.txtSelTest.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmp", "출력명", 0, , , True)
            objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("tcdgbn", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            'objHelp.AddField("titleyn", "titleyn", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sTestCds As String = "", sTestNmds As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sTestCd As String = aryList.Item(ix).ToString.Split("|"c)(2)
                    Dim sTnmd As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sTestCds += "|" : sTestNmds += "|"
                    End If

                    sTestCds += sTestCd : sTestNmds += sTnmd
                Next

                Me.txtSelTest.Text = sTestNmds.Replace("|", ",")
                Me.txtSelTest.Tag = sTestCds + "^" + sTestNmds
            Else
                Me.txtSelTest.Text = ""
                Me.txtSelTest.Tag = ""
            End If


            Me.spdList.MaxRows = 0
            ' sbDisplay_Test()

            ' COMMON.CommXML.setOneElementXML(msXMLDir, msTESTFile, "TEST", Me.txtSelTest.Tag.ToString)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            Dim sBuf As String = ""

            With spdList
                .ReDraw = False

                .Col = 2 : .Row = 2 : If .Text = "" Then Exit Sub

                .MaxRows = .MaxRows + 1
                .InsertRows(1, 1)

                For i As Integer = 1 To .MaxCols
                    .Col = i : .Row = 0 : sBuf = .Text
                    .Col = i : .Row = 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = sBuf
                Next


                If .ExportToExcel("병원체_검사결과_신고.xls", "병원체_검사결과_신고", "") Then
                    Process.Start("병원체_검사결과_신고.xls")
                End If


                .DeleteRows(1, 1)
                .MaxRows -= 1

                .ReDraw = True
            End With


            'Dim sBuf As String = ""

            'With Me.spdList


            '    .ReDraw = False
            '    .Col = 2 : .Row = 2 : If .Text = "" Then Exit Sub

            '    '.MaxRows = .MaxRows + 2
            '    '.InsertRows(1, 2)

            '    .Col = 1 : .Col2 = .MaxCols
            '    .Row = 1 : .Row2 = 2
            '    .BlockMode = True
            '    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
            '    .BlockMode = False

            '    .Col = 1 : .Col2 = .MaxCols
            '    .Row = 2 : .Row2 = 2
            '    .BlockMode = True
            '    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            '    .BlockMode = False

            '    For i As Integer = 1 To .MaxCols

            '        .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0 : sBuf = .Text
            '        .Col = i : .Row = 1 : .Text = sBuf

            '        .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1 : sBuf = .Text
            '        .Col = i : .Row = 2 : .Text = sBuf

            '        '만약 해당 컬림이 Hidden이라면 해당 컬럼의 내용을 삭제한다.
            '        .Row = 0
            '        If .ColHidden.Equals(True) Then
            '            '.DeleteCols(i, 1)
            '            For i2 As Integer = 1 To .MaxRows
            '                .Row = i2
            '                .Text = ""
            '            Next
            '        End If

            '    Next

            '    If .ExportToExcel("병원체_검사결과_신고.xls", "병원체_검사결과_신고", "") Then
            '        Process.Start("병원체_검사결과_신고.xls")
            '    End If

            '    .DeleteRows(1, 2)
            '    .MaxRows -= 2
            '    .ReDraw = True
            'End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown

        Dim dt As New DataTable
        Dim bFind As Boolean = False
        Dim sBcNo As String = ""

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        If Trim(txtBcNo.Text).Length = 0 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "검체번호를 입력해주세요.!!")
        Else
            sBcNo = Trim(txtBcNo.Text).Replace("-", "")

            If Len(sBcNo) = 11 Or Len(sBcNo) = 12 Then
                sBcNo = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(sBcNo.Substring(0, 11))
            End If

            If sBcNo.Length = 14 Then sBcNo += "0"

            Me.txtBcNo.Text = sBcNo

            If sBcNo.Substring(7, 1) = "O" Then
                MsgBox("외부 의뢰검사는 등록하실 수 없습니다.")
            End If

            With Me.spdList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("bcno")
                    If .Text = sBcNo Then
                        MessageBox.Show("이미 리스트에 있는 검체입니다.!!")
                        Me.txtBcNo.Text = ""
                        Return
                    End If

                Next
            End With

            sbSpccd_Map()
            sbDisplay_Data(Me.txtBcNo.Text)

        End If
        Me.txtBcNo.SelectAll()
        Me.txtBcNo.Focus()

    End Sub

    Private Sub spdList_DblClick(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick

        With Me.spdList


            Dim sRetVal As String = ""
            .Row = e.row
            .Col = e.col

            If .GetColFromID("spc") = e.col Then
                Dim frm As New FGS20_S01

                Dim sSpcVal As String = .Text

                If IsNumeric(sSpcVal) Then
                    sRetVal = frm.fnDisplayResult()
                Else
                    sRetVal = frm.fnDisplayResult(sSpcVal)
                End If



                If sRetVal.IndexOf(":") > 0 Then
                    .Text = sRetVal.Split(":"c)(0)
                    .Col = e.col + 1
                    .Text = sRetVal.Split(":"c)(1)
                Else
                    .Text = sRetVal
                End If

            ElseIf .GetColFromID("test") = e.col Then
                Dim frm As New FGS20_S02

                sRetVal = frm.fnDisplayResult()

                If sRetVal.IndexOf(":") > 0 Then
                    .Text = sRetVal.Split(":"c)(0)
                    .Col = e.col + 1
                    .Text = sRetVal.Split(":"c)(1)
                Else
                    .Text = sRetVal
                End If

            ElseIf .GetColFromID("refcd") = e.col Then
                Dim frm As New FGS20_S03
                sRetVal = frm.fnDisplayResult()
                .Text = sRetVal

            End If

        End With

    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        Try

            With Me.spdList

                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    For icol As Integer = 1 To .MaxCols - 1
                        .Col = icol
                        If icol = .GetColFromID("reqhospicd") Then
                            .BackColor = System.Drawing.Color.Honeydew
                        ElseIf icol = .GetColFromID("patnm") Then
                            .BackColor = System.Drawing.Color.AliceBlue
                        ElseIf icol = .GetColFromID("state") Then

                        Else
                            .BackColor = Color.White
                        End If
                        '.BackColor = System.Drawing.Color.White
                    Next

                Next

                For icol As Integer = 0 To .MaxCols - 1
                    .Row = e.row
                    .Col = icol
                    If icol <> .GetColFromID("state") Then
                        .BackColor = System.Drawing.Color.Lavender
                    End If
                Next

                If .GetColFromID("chk") = e.col Then
                    .Col = .GetColFromID("state")
                    If .Text = "전송완료" Then
                        MsgBox("전송완료 된 항목입니다.")
                    End If

                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub


    Private Sub btnRegOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegOne.Click
        Try
            Dim frm As New FGS20_S04

            Dim arrRefinfo As New ArrayList

            Dim sRHospiCd As String = ""
            Dim sRHospiNm As String = ""
            Dim sRHospiUsr As String = ""

            Dim sSpcName As String = ""
            Dim sSpcSex As String = ""
            Dim sSpcBirth As String = ""
            Dim sSpcRegno As String = ""
            Dim sSpcDept As String = ""

            Dim sSpcSpc As String = ""
            Dim sSpcSpcEtc As String = ""
            Dim sTest As String = ""
            Dim sTestEtc As String = ""
            Dim sRefcd As String = ""

            Dim sTkdt As String = ""
            Dim sFndt As String = ""
            Dim sTestUsr As String = ""
            Dim sBcno As String = ""
            Dim sRptusr As String = ""

            Dim objRef As New REFLIST
            With spdList
                If .ActiveRow < 0 Then
                    MsgBox("전송하실 검체를 선택해 주세요 ")
                    Return
                End If
                .Row = .ActiveRow
                'bch 10.24  요청사항 추가 : 신고당일은 신고로 신고이전은 신고완료로 둘다 아니면 비워진상태로  

                '의뢰기관 
                .Col = .GetColFromID("reqhospicd") : sRHospiCd = .Text
                .Col = .GetColFromID("reqhospinm") : sRHospiNm = .Text
                .Col = .GetColFromID("reqhospiusr") : sRHospiUsr = .Text
                '검체정보
                .Col = .GetColFromID("patnm") : sSpcName = .Text
                .Col = .GetColFromID("sex") : sSpcSex = .Text
                .Col = .GetColFromID("birth") : sSpcBirth = .Text
                .Col = .GetColFromID("regno") : sSpcRegno = .Text
                .Col = .GetColFromID("deptcd") : sSpcDept = .Text
                '검체,검사방법,병원체코드
                .Col = .GetColFromID("spc") : sSpcSpc = .Text
                .Col = .GetColFromID("spcetc") : sSpcSpcEtc = .Text
                .Col = .GetColFromID("test") : sTest = .Text
                .Col = .GetColFromID("testetc") : sTestEtc = .Text
                .Col = .GetColFromID("refcd") : sRefcd = .Text
                '
                .Col = .GetColFromID("tkdt") : sTkdt = .Text '14
                .Col = .GetColFromID("fndt") : sfndt = .Text '15
                .Col = .GetColFromID("testusr") : sTestUsr = .Text '16
                .Col = .GetColFromID("bcno") : sBcno = .Text '17
                .Col = .GetColFromID("rptusr") : sRptusr = .Text ' 18

            End With

            With objRef

                .RHospiCd = sRHospiCd
                .RHospiNm = sRHospiNm
                .RHospiUsr = sRHospiUsr
                .SpcName = sSpcName
                .SpcSex = sSpcSex
                .SpcBirTh = sSpcBirth
                .SpcRegno = sSpcRegno
                .SpcDept = sSpcDept
                .Spc = sSpcSpc
                .Spcetc = sSpcSpcEtc
                .Test = sTest
                .Testetc = sTestetc
                .Refcd = sRefcd
                .TestUsr = sTestUsr
                .Tkdt = sTkdt
                .fndt = sFndt
                .RptUsr = sRptusr
                .Bcno = sBcno
                .Groupcd = fn_get_Groupcd(sRefcd)
            End With

            arrRefinfo.Add(objRef)

            Dim sRetval As String = frm.DisplayForm(arrRefinfo)

            Me.spdList.MaxRows = 0
            If Me.txtBcNo.Text <> "" Then
                sbDisplay_Data(Me.txtBcNo.Text)
            Else
                sbDisplay_Data()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        

    End Sub

    Private Sub btnGetReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetReg.Click
        Try

            Dim arrRefinfo As New ArrayList

            Dim sHospiCd As String = ""
            Dim sHospiNm As String = ""
            Dim sHospiUsr As String = ""
            Dim sSpcName As String = ""
            Dim sSpcSex As String = ""
            Dim sSpcBirth As String = ""
            Dim sSpcRegno As String = ""
            Dim sSpcDept As String = ""

            Dim objRef As New REFLIST

            'End With
            Dim sUrl As String = "https://is.cdc.go.kr/ccbase/pages/certLoginNb.jsp?callbackUrl=https://is.cdc.go.kr/tids/anids/session/getinfo.dj"
            ' Dim sRet As String = (New WEBSERVER.CGWEB_S).fnGetRegist_for_KCDC(sUrl)

            Dim process As New Process
            process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\internet Explorer\iexplore.exe"
            process.StartInfo.Arguments = sUrl
            process.Start()

            'Dim web = New WebBrowser
            'WebBr.Navigate(New Uri(sUrl))
            'Dim strin As String = WebBr.Document.Cookie


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


      

    End Sub

    Private Sub btnRegAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnregAll.Click


        Try

            Dim objRef As New REFLIST

            Dim arrRefinfo As New ArrayList

            Dim sRHospiCd As String = "", sRHospiNm As String = "", sRHospiUsr As String = ""
            Dim sSpcName As String = "", sSpcSex As String = "", sSpcBirth As String = "", sSpcRegno As String = "", sSpcDept As String = ""
            Dim sSpcSpc As String = "", sSpcSpcetc As String = "", sTest As String = "", sTestetc As String = "", sRefcd As String = ""
            Dim sTkdt As String = ""
            Dim sfndt As String = ""
            Dim sTestUsr As String = "", sBcno As String = "", sRptusr As String = ""

            Dim iCnt As Integer = 0

            Dim iRet As Boolean = False
            With Me.spdList

                For ix As Integer = 0 To .MaxRows - 1
                    .Row = ix + 1 '<<< 20171130 일괄보고 안되는 부분 수정 
                    .Col = .GetColFromID("chk")
                    Dim sChk As String = .Text

                    If sChk = "1" Then
                        iCnt += 1
                        .Col = .GetColFromID("reqhospicd") : sRHospiCd = .Text
                        .Col = .GetColFromID("reqhospinm") : sRHospiNm = .Text
                        .Col = .GetColFromID("reqhospiusr") : sRHospiUsr = .Text
                        .Col = .GetColFromID("patnm") : sSpcName = .Text '4
                        .Col = .GetColFromID("sex") : sSpcSex = .Text '5
                        If sSpcSex = "남" Then
                            sSpcSex = "M"
                        ElseIf sSpcSex = "여" Then
                            sSpcSex = "F"
                        End If
                        .Col = .GetColFromID("birth") : sSpcBirth = .Text '6
                        .Col = .GetColFromID("regno") : sSpcRegno = .Text '7
                        .Col = .GetColFromID("deptcd") : sSpcDept = .Text '8
                        .Col = .GetColFromID("spc") : sSpcSpc = .Text '9
                        If sSpcSpc = "" Then
                            MsgBox("검체종류코드는 필수 입력사항입니다.")
                            Return

                        ElseIf IsNumeric(sSpcSpc) = False Then
                            MsgBox("검체종류코드는 코드로 입력 바랍니다.")
                            Return
                        End If

                        .Col = .GetColFromID("spcetc") : sSpcSpcetc = .Text '10
                        .Col = .GetColFromID("test") : sTest = .Text '11

                        If sTest = "" Then
                            MsgBox("검사방법코드는 필수 입력사항입니다.")
                            Return
                        ElseIf IsNumeric(sTest) = False Then
                            MsgBox("검체종류코드는 코드로 입력 바랍니다.")
                            Return
                        End If

                        .Col = .GetColFromID("testetc") : sTestetc = .Text '12
                        .Col = .GetColFromID("refcd") : sRefcd = .Text '13

                        If sRefcd = "" Then
                            MsgBox("병원체코드는 필수 입력사항입니다")
                        End If

                        .Col = .GetColFromID("tkdt") : sTkdt = .Text '14
                        .Col = .GetColFromID("fndt") : sfndt = .Text '15
                        .Col = .GetColFromID("testusr") : sTestUsr = .Text '3
                        .Col = .GetColFromID("bcno") : sBcno = .Text '3

                        .Col = .GetColFromID("rptusr") : sRptusr = .Text '<<<20180807 보고자 저장 추가

                        With objRef
                            .RHospiCd = sRHospiCd
                            .RHospiNm = sRHospiNm
                            .RHospiUsr = sRHospiUsr
                            .SpcName = sSpcName
                            .SpcSex = sSpcSex
                            .SpcBirTh = sSpcBirth
                            .SpcRegno = sSpcRegno
                            .SpcDept = sSpcDept
                            .Spc = sSpcSpc
                            .Spcetc = sSpcSpcetc
                            .Test = sTest
                            .Testetc = sTestetc
                            .Refcd = sRefcd
                            .TestUsr = sTestUsr
                            .Tkdt = sTkdt
                            .fndt = sfndt
                            .RptUsr = sRptusr
                        End With

                        arrRefinfo.Add(objRef)

                        'sURL += "&rm_info=" '16) 비고정보
                        'sURL += "&hsptl_swbser=" '17) 병원 소프트웨어 개발사 (사업자)
                        'sURL += "&hsptl_swknd=" '18) 병원 소프트웨어 종류 (버전)
                        'sURL += "&dplct_at=0" '19)중복여부 test시에는 0으로 보낼것 [필]
                        'sURL += "&rspns_mssage_ty=0" '20) 응답 형식 0 :xml , 1:json [필]


                        'URL 전송 
                        Dim sRetVal As String = (New WEBSERVER.CGWEB_S).fnRegWebServer_for_KCDC(arrRefinfo)
                        'URL 과 return 값을 받음 
                        Dim sSaveResult As String() = sRetVal.Split(Chr(124))

                        Dim sUrl As String = sSaveResult(0) 'URL
                        Dim sRtnVal As String = sSaveResult(1) 'return 값

                        '전송 결과 처리 
                        Dim sRtn As String = fnGetResultvalue(sRtnVal)

                        '전송한 url과 결과 insert 
                        Dim objRst As New LISAPP.APP_R.AxRstFn
                        Dim iret2 As Boolean = objRst.fnIns_LR080M(sBcno, sRtn.Split("|"c)(0), sUrl, sRtnVal, CStr(IIf(sRtn.Split("|"c)(0) = "Y", "", sRtn)), sSpcSpc, sSpcSpcetc, sTest, sTestetc, sRefcd, sRptusr)

                        '전송상태 표시 
                        .Col = .GetColFromID("state")
                        If sRtn.Split("|"c)(0) = "Y" Then
                            .Text = "등록성공"
                            .BackColor = Color.LimeGreen
                            'sbDisplay_Data()
                        Else
                            .Text = "등록실패"
                            .BackColor = Color.Coral
                            .Col = .GetColFromID("errmsg") : .Text = sRtn
                            Return
                        End If

                    End If
                Next

                If iCnt = 0 Then
                    MsgBox("체크된 항목이 없습니다." + CStr(iCnt))
                    Return
                End If

                sbDisplay_Data()

                Return

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function fnGetResultvalue(ByVal rsResult As String) As String
        Try
            Dim sRtn As String = ""

            Dim iPosMsg As Integer = rsResult.IndexOf("message")
            Dim iPosCddt As Integer = rsResult.IndexOf("code_dt")
            Dim iPosStat As Integer = rsResult.IndexOf("stat")

            Dim sMsg As String = ""
            Dim sCddt As String = ""
            Dim sStat As String = ""

            'rsResult.IndexOf("")

            sMsg = rsResult.Substring(iPosMsg).Replace("", "|").Replace("", "^")
            sCddt = rsResult.Substring(iPosCddt).Replace("", "|").Replace("", "^")
            sStat = rsResult.Substring(iPosStat).Replace("", "|").Replace("", "^")

            sMsg = sMsg.Split("^"c)(0).Split("|"c)(1)
            sCddt = sCddt.Split("^"c)(0).Split("|"c)(1)
            sStat = sStat.Split("^"c)(0).Split("|"c)(1)


            If sCddt = "2001" Then
                sRtn = "Y" + "|" + sCddt + "|" + sMsg + "|" + sStat
            Else
                sRtn = "N" + "|" + sCddt + "|" + sMsg + "|" + sStat
            End If

            Return sRtn

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Function fnInsURLResult(ByVal rsBcno As String, ByVal rsRtxmlinfo As String ) As Boolean
        Try

            '1 전송한 URL의 BCNO 가지고 URL 정보저장한 내역이 있는 지 확인하고 seq를 반환한다 
            Dim dt_seq As DataTable = fnGetURLseqinfo(rsBcno)
            Dim iSeq As Integer = 0

            '1-1 dt가 없을경우 seq = 1 , 있을경우 seq + 1
            If dt_seq.Rows.Count > 0 Then
                iSeq += 1
            Else
                iSeq = 1
            End If

            '2 리턴받은 xml 정보를 insert 한다. 

        Catch ex As Exception

        End Try

    End Function

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        If Me.chkAll.Text = "True" Then

            With Me.spdList

                For ix As Integer = 1 To .MaxRows - 1
                    .Col = 1
                    .Text = "1"

                 
                Next

            End With
        End If
    End Sub

    Private Sub btnUTF8EN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUTF8EN.Click
        Dim sSTr As String = (New WEBSERVER.CGWEB_S).UTF8EN(Me.TextBox1.Text)
        ' Dim sSTr As String = (New WEBSERVER.CGWEB_S).encode(Me.TextBox1.Text)

        Me.TextBox2.Text = sSTr
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim sBcno As String = "20170802M1000000"
            Return

           ' Dim dt_seq As DataTable = fnGetURLseqinfo(sBcno)
            'Dim iSeq As Integer = 0

            ''1-1 dt가 없을경우 seq = 1 , 있을경우 seq + 1
            'If dt_seq.Rows.Count > 0 Then
            '    iSeq = CInt(dt_seq.Rows(0).Item("seq").ToString())
            '    iSeq += 1
            'Else
            '    iSeq = 1
            'End If
            Dim objRst As New LISAPP.APP_R.AxRstFn
            Dim iret As Boolean = objRst.fnIns_LR080M(sBcno, "N", "testS", "TestR", "")
            '    Dim iret As Boolean = (New LISAPP.APP_R.AxRstFn).(sBcno, "0", "N", "testS", "TestR")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Me.txtErrmsg.Text = fnRtnmsg(Me.textRst.Text.ToString)

    End Sub

    Public Function fnRtnmsg(ByVal rsRtnmsg As String) As String

        Dim iPos As Integer = rsRtnmsg.IndexOf("parameters")

        Dim sURLResult As String = ""
        Dim sURLParameters As String = ""
        Dim sReturn As String = ""

        If iPos > 0 Then
            sURLResult = rsRtnmsg.Substring(0, iPos)
            sURLParameters = rsRtnmsg.Substring(iPos)

            sURLResult = sURLResult.Replace("result", "|")

            Dim sParse_step1 As String() = sURLResult.Split("|"c)

            If sParse_step1.Length > 1 Then
                For ix As Integer = 1 To sParse_step1.Length - 1

                    Dim sParse_step2 As String() = sParse_step1(ix).Split(Chr(3))

                    For iy As Integer = 0 To sParse_step2.Length - 1
                        sReturn += sParse_step2(iy) + vbCrLf
                    Next

                Next
            End If
           
        End If

        Return sReturn

    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        sbSpccd_Map()
    End Sub

    Private Sub rdoRstExY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoRstExY.Click, rdoRstExN.Click
        If Me.rdoRstExY.Checked Then
            Me.cboRstEx.Enabled = True
        Else
            Me.cboRstEx.Enabled = False
        End If
    End Sub
    
    Private Sub btnWebKDCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWebKDCD.Click
        Dim sUrl As String = "https://is.cdc.go.kr/tids/anids/pthgogan/pthgoganList.vp?"
        Dim sOgcr As String = "cn=국립중앙의료원,ou=건강보험,ou=MOHW RA센터,ou=등록기관,ou=licensedCA,o=KICA,c=KR"

        sUrl += "&ogcr=" + UTF8EN(sOgcr)

        Dim process As New Process
        process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\internet Explorer\iexplore.exe"
        process.StartInfo.Arguments = sUrl
        process.Start()

    End Sub

    Public Function UTF8EN(ByVal T As String) As String
        Dim Bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(T)
        Dim S(UBound(Bytes)) As String
        Dim ResultStr As New StringBuilder
        Dim TempStr As String
        For Each b As Byte In Bytes

            '//공백값처리를 위해 공백값은 "+"로 표현한다.
            If b = 32 Then
                TempStr = "+"
            Else

                '//인코딩값중 한글이 아닌 (영어,기호,숫자)문자는 그냥 문자로..
                '//한글은 설정된 아스키코드앞에 %표시하고 아스키코드를 16진수 값으로 변환
                'TempStr = CType(IIf(b < 32 Or b > 127, "%" & Hex(b), Chr(b)), String)
                TempStr = CType(IIf(b < 32 Or b > 127, "%" & Hex(b), Chr(b)), String)

                If (b < 32 Or b > 127) Then  '한글 
                    TempStr = "%" & Hex(b)
                Else
                    If (b = 61 Or b = 44) Then '특수문자일경우 ( = , ) 
                        TempStr = "%" & Hex(b)
                    Else
                        TempStr = Chr(b)
                    End If

                End If

            End If

            '//변환된 값을 StringBuilder에 저장한다.
            ResultStr.Append(TempStr)

        Next

        'For i As Integer = 0 To UBound(Bytes)
        '    S(i) = Join({"%", Hex(Bytes(i))}, vbNullString)
        'Next

        Return ResultStr.ToString
    End Function

    Private Sub btnBacfilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBacfilter.Click
        Try

            Dim sWGrpCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sPartSlip As String = ""

            sPartSlip = Ctrl.Get_Code(Me.cboPartSlip)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Bac_List("", False, "")
            Dim a_dr As DataRow() = dt.Select("", "") '(tcdgbn = 'P'OR titleyn = '0')

            dt = Fn.ChangeToDataTable(a_dr)
            objHelp.FormText = "균코드"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            If Me.txtSelTest.Text <> "" Then objHelp.KeyCodes = Me.txtSelTest.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("baccd", "균코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("bacnmd", "균명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            'objHelp.AddField("bacgencd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            '
            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If aryList.Count > 0 Then
                'If aryList.Count > 1 Then
                '    MsgBox("균코드를 1개만 선택해주세요")
                'End If
                Dim sTestCds As String = "", sTestNmds As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sTestCd As String = aryList.Item(ix).ToString.Split("|"c)(0) '20171101
                    Dim sTnmd As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sTestCds += "|" : sTestNmds += "|"
                    End If

                    sTestCds += sTestCd : sTestNmds += sTnmd
                Next

                Me.txtBacFilter.Text = sTestNmds.Replace("|", ",")
                Me.txtBacFilter.Tag = sTestCds.Replace("|", ",")
            Else
                Me.txtBacFilter.Text = ""
                Me.txtBacFilter.Tag = ""
            End If


            If Me.txtBacFilter.Text <> "" Then
                Me.chkBaccd.Checked = True
            End If



        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnAntiFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAntiFilter.Click
        Try

            Dim sWGrpCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sPartSlip As String = ""

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnCdHelp_test)

            Dim objHelp As New FGS20_S05

            Me.txtAntiFilter.Text = objHelp.fnDisplay()
            Me.txtAntiFilter.Tag = Me.txtAntiFilter.Text

            If Me.txtAntiFilter.Text <> "" Then
                Me.chkAnti.Checked = True
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtBacFilter_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBacFilter.MouseHover

        If Me.txtBacFilter.Tag.ToString <> Nothing Then
            If Me.txtBacFilter.Tag.ToString <> "" Then
                Ctrl.Set_ToolTip(Me.txtBacFilter, Me.txtBacFilter.Tag.ToString, m_tooltip)
            End If
        End If
      

    End Sub

    Private Sub txtAntiFilter_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAntiFilter.MouseHover
        If Me.txtAntiFilter.Tag.ToString <> Nothing Then
            If Me.txtAntiFilter.Tag.ToString <> "" Then
                Ctrl.Set_ToolTip(Me.txtAntiFilter, Me.txtAntiFilter.Tag.ToString, m_tooltip)
            End If
        End If
    End Sub


    Private Sub rdoOr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoOr.Click, rdoAnd.Click
        If Me.rdoAnd.Checked Then
            Me.rdoOr.Checked = False
        ElseIf Me.rdoOr.Checked Then
            Me.rdoAnd.Checked = False
        End If
    End Sub

    Private Sub btnWebReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWebReq.Click
        Dim sUrl As String = "https://is.cdc.go.kr/tids/anids/piacpt/piAcptList.vp?"
        Dim sOgcr As String = "cn=국립중앙의료원,ou=건강보험,ou=MOHW RA센터,ou=등록기관,ou=licensedCA,o=KICA,c=KR"

        'https://is.cdc.go.kr/tids/anids/piacpt/piAcptList.vp? +  신고항목 (“&신고항목명 = 신고항목값, ... ”) 

        sUrl += "&ogcr=" + UTF8EN(sOgcr)

        Dim process As New Process
        process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\internet Explorer\iexplore.exe"
        process.StartInfo.Arguments = sUrl
        process.Start()
    End Sub
End Class

