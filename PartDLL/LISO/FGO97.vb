Imports DP01.DataProvider
Imports COMMON.CommFN
Imports System.IO
Imports COMMON.FVar.AppCfg

Public Class FGO97

    Private Sub txtRegno_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown
        If e.KeyCode = Keys.Enter Then
            sbExeUpdateMTS0002_OCS(Me.txtRegno.Text)
        End If
    End Sub

    Private Sub sbExeUpdateMTS0002_OCS(ByVal rsRegno As String)
        Try
            Dim sSql As String = ""
            Dim al As New ArrayList
            Dim dt As New DataTable
            Dim iret As Integer = 0

            '환자정보 불러오기
            sSql = ""
            sSql += " SELECT  a.bunho, a.suname,a.sex, to_char(to_date(a.birth, 'yyyy-mm-dd'),'yyyy-mm-dd') AS birth, a.sujumin1 || a.sujumin2 ,a.sujumin1,a.sujumin2,a.address1 " + vbCrLf
            sSql += "   FROM vw_mts0002 a" + vbCrLf
            sSql += "  WHERE a.bunho = ? " + vbCrLf

            al.Clear()
            al.Add(New OleDb.OleDbParameter("bunho", rsRegno))

            LisDbCommand(False, True)
            dt = LisDbExecuteQuery(sSql, al, True, False)

            If dt.Rows.Count > 0 Then
                ' MsgBox("성공")
                '환자 정보 중간테이블 삭제
                sSql = ""
                sSql += "delete mts0002_ocs "
                sSql += "where bunho = ?"

                al.Clear()
                al.Add(New OleDb.OleDbParameter("bunho", dt.Rows(0).Item("bunho").ToString))

                LisDbCommand()
                iret += LisDbExecute(sSql, al, True)

                '환자 정보 중간테이블 insert
                sSql = ""
                sSql += "INSERT INTO mts0002_ocs (sys_date, user_id,bunho, suname,birth,sujumin1, sujumin2, zip_code1, zip_code2,address1,address2,tel1,tel2, sex, gubun_name, sogae, vip, injong )"
                sSql += "                 VALUES ( '',         '',   ?,    ?,     ?,    ?,         ?,        '',         '',        ?,      '',      '',  '', ?  , ''        , ''     ,'',    '')"

                al.Clear()
                al.Add(New OleDb.OleDbParameter("bunho", dt.Rows(0).Item("bunho").ToString))
                al.Add(New OleDb.OleDbParameter("suname", dt.Rows(0).Item("suname").ToString))
                al.Add(New OleDb.OleDbParameter("birth", dt.Rows(0).Item("birth").ToString))
                al.Add(New OleDb.OleDbParameter("sujumin1", dt.Rows(0).Item("sujumin1").ToString))
                al.Add(New OleDb.OleDbParameter("sujumin2", dt.Rows(0).Item("sujumin2").ToString))
                al.Add(New OleDb.OleDbParameter("address1", dt.Rows(0).Item("address1").ToString))
                al.Add(New OleDb.OleDbParameter("sex", dt.Rows(0).Item("sex").ToString))

                LisDbCommand()
                iret += LisDbExecute(sSql, al, True)

            Else
                MsgBox("실패")
            End If

            
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class