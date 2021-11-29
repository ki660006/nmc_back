Imports System.Windows.Forms
'Imports System.IO
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN

Public Class FGCDBLD_TRAN_MGT
    Private mTnsjubsuno As String
    Public Sub New(ByVal rsTnsjubsuno As String)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        mTnsjubsuno = rsTnsjubsuno
    End Sub

    Private Sub FGCDBLD_TRAN_Mgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            sbDisplay_cmt()
            Dim dt As DataTable = (New DA_CDHELP_TRAN_MGT).fnGet_Tns_Info(mTnsjubsuno)
            fn_Display_patinfo(dt)
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub sbDisplay_cmt()

        Dim sFn As String = "Sub sbDisplay_slip()"

        Try
            Dim dt As DataTable = (New DA_CDHELP_TRAN_MGT).fnGet_cmtcontlist()

            Me.cboMgtCmt.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboMgtCmt.Items.Add("[" + dt.Rows(ix).Item("cmtcd").ToString + "] " + dt.Rows(ix).Item("cmtcont").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Public Sub fn_Display_patinfo(ByVal rsDt As DataTable)
        Try
            Dim arPatinfo() As String = Split(rsDt.Rows(0).Item("patinfo").ToString, "|")

            Me.lblTnsjubsuno.Text = rsDt.Rows(0).Item("tnsjubsuno").ToString
            Me.lblDeptNm.Text = rsDt.Rows(0).Item("deptcd").ToString
            Me.lblOrdDt.Text = rsDt.Rows(0).Item("orddt").ToString
            Me.lblWard_SR.Text = rsDt.Rows(0).Item("wardno").ToString
            Me.lblDoctor.Text = rsDt.Rows(0).Item("doctorcd").ToString
            Me.lblRegNo.Text = rsDt.Rows(0).Item("regno").ToString
            Me.lblPatNm.Text = arPatinfo(0)
            Me.lblIdNo.Text = arPatinfo(3) '주민등록 번호 나오게 할려면 -> arPatinfo(6) + "-" + arPatinfo(7)
            Me.lblSex.Text = rsDt.Rows(0).Item("sex").ToString
            Me.lblAge.Text = rsDt.Rows(0).Item("age").ToString
            Me.lblTkDt.Text = rsDt.Rows(0).Item("jubsudt").ToString
            Me.lblTkID.Text = rsDt.Rows(0).Item("jubsuid").ToString

            Dim dt As DataTable = (New DA_CDHELP_TRAN_MGT).fnGet_UsrNm(STU_AUTHORITY.UsrID)
            Me.lblDLMCaller.Text = STU_AUTHORITY.UsrID
            Me.txtDLMCaller.Text = dt.Rows(0).Item("loginusrnm").ToString
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Public Sub fn_init_form()
        mTnsjubsuno = ""

        Me.lblTnsjubsuno.Text = "" '
        Me.lblDeptNm.Text = ""
        Me.lblOrdDt.Text = ""
        Me.lblWard_SR.Text = ""
        Me.lblDoctor.Text = ""
        Me.lblRegNo.Text = ""
        Me.lblPatNm.Text = ""
        Me.lblIdNo.Text = ""
        Me.lblSex.Text = ""
        Me.lblAge.Text = ""
        Me.lblTkDt.Text = ""
        Me.lblTkID.Text = ""

        Me.txtDLMCaller.Text = "" '진검통화자
        Me.lblDLMCaller.Text = "" '진검통화자id
        Me.txtCMCaller.Text = "" '임상통화자
        Me.txtCmtCont.Text = "" '통화내용

        Me.chkHb10.Checked = False
        Me.chkCBC.Checked = False
        Me.chkALL.Checked = False
        Me.chkExcept.Checked = False
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtDLMCaller_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDLMCaller.KeyDown
        Try
            Dim dt As DataTable = (New DA_CDHELP_TRAN_MGT).fnGet_UsrNm(Me.txtDLMCaller.Text.ToString)
            Me.txtDLMCaller.Text = dt.Rows(0).Item("loginusrnm").ToString
        Catch ex As Exception

        Finally
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim objTranMgtIfo As New STU_TRAN_MGT_INFO
            Dim fnExe As New DA_CDHELP_TRAN_MGT

            objTranMgtIfo.TNSJUBSUNO = Me.lblTnsjubsuno.Text.Trim.ToString
            objTranMgtIfo.REGID = USER_INFO.USRID.ToString
            objTranMgtIfo.REGNO = Me.lblRegNo.Text.Trim.ToString
            objTranMgtIfo.DLMCaller = Me.lblDLMCaller.Text.Trim.ToString
            objTranMgtIfo.CMCaller = Me.txtCMCaller.Text.Trim.ToString
            objTranMgtIfo.CALLCMTCONT = Me.txtCmtCont.Text.Trim.ToString
            objTranMgtIfo.HB10 = IIf(Me.chkHb10.Checked, "Y", "N").ToString
            objTranMgtIfo.CBC = IIf(Me.chkCBC.Checked, "Y", "N").ToString
            objTranMgtIfo.ALL = IIf(Me.chkALL.Checked, "Y", "N").ToString
            objTranMgtIfo.EXCEPT = IIf(Me.chkExcept.Checked, "Y", "N").ToString

            Dim chkSave As Boolean = fnExe.fnExe_TRAN_MGT(objTranMgtIfo)

            If chkSave Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상 등록 되었습니다.")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "등록에 실패하였습니다.")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        Finally
        End Try
    End Sub

    Private Sub cboMgtCmt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboMgtCmt.SelectedIndexChanged

        If Me.cboMgtCmt.Text <> "" Then
            Me.txtCmtCont.Text = cboMgtCmt.Text.Substring(cboMgtCmt.Text.IndexOf("]") + 1).Trim + vbCrLf
        End If
    End Sub
End Class

Public Class STU_TRAN_MGT_INFO
    Public Shared TNSJUBSUNO As String = ""         ' 수혈접수번호
    Public Shared REGNO As String = ""              ' 등록번호
    Public Shared REGID As String = ""              ' 등록자
    Public Shared DLMCaller As String = ""          ' 진검통화자
    Public Shared CMCaller As String = ""           ' 임상통화자
    Public Shared CALLCMTCONT As String = ""        ' 통화내용

    '요청 체크 사항
    Public Shared HB10 As String = ""               ' Hb>10g/dL
    Public Shared CBC As String = ""                ' CBC F/U
    Public Shared ALL As String = ""                ' 모두요청 
    Public Shared EXCEPT As String = ""             ' EXCEPT 
End Class

Public Class DA_CDHELP_TRAN_MGT
    Private Const msFile As String = "File : FGCDBLD_TRAN_MGT.vb, Class : DA_CDHELP_TRAN_MGT" + vbTab
    Private m_dbCn As OracleConnection
    Private m_dbTran As OracleTransaction

    Public Function fnExe_TRAN_MGT(ByVal objTranMgtIfo As STU_TRAN_MGT_INFO) As Boolean
        Dim sFn As String = " fnGet_Help_Info([String]) As DataTable"

        Try
            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()
            Dim dbDa As OracleDataAdapter
            Dim dbCmd As New OracleCommand
            Dim dt As New DataTable
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            sSql += "  insert into LBC10m (REGDT, TNSJUBSUNO, REGNO, REGID, DLMCALLER,          " + vbCrLf
            sSql += "                      CMCALLER, CMTCONT, HGYN, CBCYN, ALLYN, ECPTYN, SEQ)  " + vbCrLf '20211125 jhs seq 추가
            sSql += "  values( fn_ack_sysdate(), :tnsjubsuno, :regno, :regid, :dlmcaller,       " + vbCrLf
            sSql += "          :cmcaller, :cmtcont, :hgyn, :cbcyn, :allyn, :ecptyn             " + vbCrLf
            sSql += "         ,(selecT case when nvl(max(seq),0) = 0 then '1'                   " + vbCrLf
            sSql += "                       when max(seq) > 0 then to_char(max(seq) + 1) end    " + vbCrLf
            sSql += "           from lbc10m where tnsjubsuno = :tnsjubsuno) )                   " + vbCrLf

            With dbCmd
                .Connection = m_dbCn
                .CommandType = CommandType.Text
                .CommandText = sSql

                .Parameters.Clear()
                .Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = objTranMgtIfo.TNSJUBSUNO
                .Parameters.Add("regno", OracleDbType.Varchar2).Value = objTranMgtIfo.REGNO
                .Parameters.Add("regid", OracleDbType.Varchar2).Value = objTranMgtIfo.REGID
                .Parameters.Add("dlmcaller", OracleDbType.Varchar2).Value = objTranMgtIfo.DLMCaller
                .Parameters.Add("cmcaller", OracleDbType.Varchar2).Value = objTranMgtIfo.CMCaller
                .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = objTranMgtIfo.CALLCMTCONT
                .Parameters.Add("hgyn", OracleDbType.Varchar2).Value = objTranMgtIfo.HB10
                .Parameters.Add("cbcyn", OracleDbType.Varchar2).Value = objTranMgtIfo.CBC
                .Parameters.Add("allyn", OracleDbType.Varchar2).Value = objTranMgtIfo.ALL
                .Parameters.Add("ecptyn", OracleDbType.Varchar2).Value = objTranMgtIfo.EXCEPT
                .Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = objTranMgtIfo.TNSJUBSUNO

                iRet = .ExecuteNonQuery

            End With

            m_dbTran.Commit()

            If iRet = 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            m_dbTran.Rollback()
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        Finally
            m_dbTran.Dispose() : m_dbTran = Nothing
            If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
            m_dbCn.Dispose() : m_dbCn = Nothing
        End Try

    End Function

    Public Function fnGet_Tns_Info(ByVal rsTnsjubsuno As String) As DataTable
        Dim sFn As String = " fnGet_Help_Info([String]) As DataTable"

        Try
            m_dbCn = GetDbConnection()
            Dim dbDa As OracleDataAdapter
            Dim dbCmd As New OracleCommand

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += "Select lb40.tnsjubsuno, lb40.sex, lb40.age, lb40.regno" + vbCrLf
            sSql += " , FN_ACK_GET_PAT_INFO(lb40.regno, '', '') patinfo" + vbCrLf
            sSql += " , FN_ACK_GET_dept_NAME(lb40.iogbn, lb40.deptcd) deptcd" + vbCrLf
            sSql += " , FN_ACK_GET_WARD_NAME(lb40.wardno)  wardno" + vbCrLf
            sSql += " , FN_ACK_GET_USR_NAME(lb40.jubsuid) jubsuid" + vbCrLf
            sSql += " , FN_ACK_GET_dr_NAME(doctorcd) doctorcd" + vbCrLf
            sSql += " , fn_ack_date_str(lb40.jubsudt, 'yyyy-mm-dd hh24:mi:ss') jubsudt" + vbCrLf
            sSql += " , fn_ack_date_str(lb40.orddt, 'yyyy-mm-dd hh24:mi:ss') orddt" + vbCrLf
            sSql += "  From lb040m lb40" + vbCrLf
            sSql += "  Where lb40.tnsjubsuno = '" + rsTnsjubsuno + "'" + vbCrLf

            With dbCmd
                dbCmd.Connection = m_dbCn
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql
            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            If m_dbCn.State = ConnectionState.Open Then
                m_dbCn.Close() : m_dbCn.Dispose()
            End If

            m_dbCn = Nothing
        End Try

    End Function

    Public Function fnGet_UsrNm(ByVal rsUsrId As String) As DataTable
        Dim sFn As String = " fnGet_Help_Info([String]) As DataTable"

        Try
            m_dbCn = GetDbConnection()
            Dim dbDa As OracleDataAdapter
            Dim dbCmd As New OracleCommand
            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += " Select FN_ACK_GET_USR_NAME('" + rsUsrId + "') loginusrnm from dual"

            With dbCmd
                dbCmd.Connection = m_dbCn
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql
            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            If m_dbCn.State = ConnectionState.Open Then
                m_dbCn.Close() : m_dbCn.Dispose()
            End If

            m_dbCn = Nothing
        End Try

    End Function


    Public Function fnGet_cmtcontlist() As DataTable
        Dim sFn As String = " fnGet_Help_Info([String]) As DataTable"

        Try
            m_dbCn = GetDbConnection()
            Dim dbDa As OracleDataAdapter
            Dim dbCmd As New OracleCommand
            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql += " selecT cmtcd , cmtcont " + vbCrLf
            sSql += "   from lf410m " + vbCrLf
            sSql += "  where cmtgbn = '4' " + vbCrLf
            sSql += "    and delflg = '0' " + vbCrLf

            With dbCmd
                dbCmd.Connection = m_dbCn
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql
            End With
            dbDa = New OracleDataAdapter(dbCmd)

            dt.Reset()
            dbDa.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        Finally
            If m_dbCn.State = ConnectionState.Open Then
                m_dbCn.Close() : m_dbCn.Dispose()
            End If

            m_dbCn = Nothing
        End Try

    End Function
End Class
