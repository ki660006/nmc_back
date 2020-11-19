Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports DBORA.DbProvider

Public Class ITEMSAVE
    Private msFile As String = "File : AxAckItemSave.ITEMSAVE.vb, Class : ITEMSAVE" & vbTab

    Private msFormID As String = ""
    Private msUsrID As String = ""
    Private msItemGbn As String = ""
    Private msItemCds As String = ""
    Private msSpcGbn As String = ""
    Private mbMicroBioYn As Boolean = False
    Private mbBloodBankYn As Boolean = False
    Private mbAllPartYn As Boolean = True

    Public Event ListDblClick(ByVal rsItemCds As String, ByVal rsItemNms As String)
    
    '-- 혈액은행 조회
    Public WriteOnly Property BloodBankYn() As Boolean
        Set(ByVal Value As Boolean)
            mbBloodBankYn = Value
        End Set
    End Property

    '-- 모든 부서 조회
    Public WriteOnly Property AllPartYn() As Boolean
        Set(ByVal Value As Boolean)
            mbAllPartYn = Value
        End Set
    End Property

    '-- 미생물 부서만 적용 여부
    Public WriteOnly Property MicroBioYn() As Boolean
        Set(ByVal Value As Boolean)
            mbMicroBioYn = Value
        End Set
    End Property

    '-- Form Name
    Public WriteOnly Property FORMID() As String
        Set(ByVal Value As String)
            msFormID = Value
        End Set
    End Property

    '-- 사용자ID
    Public WriteOnly Property USRID() As String
        Set(ByVal Value As String)
            msUsrID = Value
        End Set
    End Property

    '-- Item 구분
    Public WriteOnly Property ITEMGBN() As String
        Set(ByVal Value As String)
            msItemGbn = Value
            sbDisplay_ItemList()
        End Set
    End Property

    '-- Item Code List(코드와 코드사이는 콤마로 구분)
    Public WriteOnly Property ITMECDS() As String
        Set(ByVal Value As String)
            msItemCds = Value
        End Set
    End Property

    '-- 검체코드 구분(NONE:검체구분 없음)
    Public WriteOnly Property SPCGBN() As String
        Set(ByVal Value As String)
            msSpcGbn = Value
        End Set
    End Property

    Public Sub sbDisplay_ItemList()
        Dim sFn As String = "Sub sbDisplay_ItemList()"

        Try
            Dim sUsrId As String = IIf(rdoMe.Checked, msUsrID, "").ToString
            Dim dt As DataTable = DA_ITEM_SAVE.fnGet_Item_SaveList(msFormID, sUsrId, msItemGbn)

            lstItem.Items.Clear()
            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                lstItem.Items.Add(dt.Rows(ix).Item("savenm").ToString + Space(200) + "|" + dt.Rows(ix).Item("usrid").ToString + "|" + dt.Rows(ix).Item("itemgbn").ToString)
            Next

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)

        End Try
    End Sub

    Private Sub lstItem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstItem.DoubleClick

        If Me.lstItem.Text = "" Then Return

        Dim sSaveNm As String = Me.lstItem.Text.Split("|"c)(0).Trim
        Dim sUsrid As String = Me.lstItem.Text.Split("|"c)(1).Trim
        Dim sItemGbn As String = Me.lstItem.Text.Split("|"c)(2).Trim

        Dim dt As DataTable = DA_ITEM_SAVE.fnGet_Item_Save_Test(msFormID, sUsrid, sItemGbn, sSaveNm, msSpcGbn)
        Dim sCodes As String = "", sNames As String = ""

        For ix As Integer = 0 To dt.Rows.Count - 1

            If ix <> 0 Then
                sCodes += "|"
                sNames += "|"
            End If

            sCodes += dt.Rows(ix).Item("testcd").ToString.Trim
            sNames += dt.Rows(ix).Item("tnmd").ToString.Trim
        Next

        RaiseEvent ListDblClick(sCodes, sNames)
    End Sub

    Private Sub bntSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntSave.Click

        Dim frm As New FGSECTECT

        frm.Display_Result(msFormID, msUsrID, msSpcGbn, msItemGbn, mbMicroBioYn, mbBloodBankYn, mbAllPartYn)

        DA_ITEM_SAVE.fnGet_Item_SaveList(msFormID, msUsrID, msItemGbn)

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click

        Try
            Dim sSaveNm As String = Me.lstItem.Text.Split("|"c)(0).Trim
            Dim sUsrid As String = Me.lstItem.Text.Split("|"c)(1).Trim
            Dim sItemGbn As String = Me.lstItem.Text.Split("|"c)(2).Trim

            Dim bRet As Boolean = DA_ITEM_SAVE.fnExe_Del_lf096m(msFormID, msUsrID, sItemGbn, sSaveNm)
            If bRet = False Then
                MsgBox("데이타 삭제시 오류가 발생했습니다.!!")
            Else
                sbDisplay_ItemList()
            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Sub rdoAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAll.Click, rdoMe.Click

        sbDisplay_ItemList()

    End Sub

End Class


Public Class DA_ITEM_SAVE

    Public Shared Function fnGet_Item_SaveList(ByVal rsFormID As String, ByVal rsUsrID As String, ByVal rsItemGbn As String) As DataTable
        Dim sFn As String = "Function fnGet_Item_SaveList(String, String, String) As DataTable"
        Try
            Dim oledbcn As oracleConnection = GetDbConnection()
            Dim oledbda As oracleDataAdapter
            Dim oledbcmd As New oracleCommand

            Dim dt As New DataTable
            Dim sSql As String = ""

            sSql = ""
            sSql += "SELECT DISTINCT savenm, usrid, itemgbn"
            sSql += "  FROM lf096m"
            sSql += " WHERE formid = :formid"

            If rsUsrID <> "" Then sSql += "   AND usrid = :usrid"
            If rsItemGbn <> "" Then sSql += "   AND itemgbn = :itemgbn"
            sSql += " ORDER BY SAVENM"

            oledbcmd.Connection = oledbcn
            oledbcmd.CommandType = CommandType.Text
            oledbcmd.CommandText = sSql

            oledbda = New oracleDataAdapter(oledbcmd)

            With oledbda
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("formid", OracleDbType.Varchar2).Value = rsFormID

                If rsUsrID <> "" Then .SelectCommand.Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrID
                If rsItemGbn <> "" Then .SelectCommand.Parameters.Add("itemgbn", OracleDbType.Varchar2).Value = rsItemGbn
            End With

            dt.Reset()
            oledbda.Fill(dt)

            Return dt

        Catch ex As Exception
            MsgBox("Error " & sFn & vbCrLf & Err.Description)
            Return New DataTable
        End Try
    End Function

    Public Shared Function fnGet_Item_Save_Test(ByVal rsFormID As String, ByVal rsUsrID As String, ByVal rsItemGbn As String, ByVal rsSaveNm As String, ByVal rsSpcGbn As String) As DataTable

        Dim sFn As String = "Function fnGet_Item_Save_Test(String, String, String, String) As DataTable"
        Try
            Dim oledbcn As oracleConnection = GetDbConnection()
            Dim oledbda As New oracleDataAdapter
            Dim oledbcmd As New oracleCommand

            Dim dt As New DataTable
            Dim sSql As String = ""

            If rsSpcGbn = "NONE" Then

                sSql += "SELECT a.testcd, MAX(b.tnmd) tnmd, NVL(a.dispseq, 999) dispseq"
                sSql += "  FROM lf096m a, lf060m b"
                sSql += " WHERE a.formid = :formid"
                sSql += "   AND a.usrid  = :usrid"
                If rsItemGbn <> "" Then sSql += "   AND a.itemgbn = :itemgbn"
                sSql += "   AND a.savenm = :savenm"
                sSql += "   AND a.testcd = b.testcd"
                sSql += "   AND b.usdt  <= fn_ack_sysdate"
                sSql += "   AND b.uedt  >  fn_ack_sysdate"
                sSql += " GROUP BY a.testcd, a.dispseq"
                sSql += " ORDER BY dispseq"

            Else
                sSql += "SELECT RPAD(a.testcd, 7, ' ') || ' ' || a.spccd testcd, b.tnmd, NVL(a.dispseq, 999) dispseq"
                sSql += "  FROM lf096m a, lf060m b"
                sSql += " WHERE a.formid = :formid"
                sSql += "   AND a.usrid  = :usrid"
                If rsItemGbn <> "" Then sSql += "   AND a.itemgbn = :itemgbn"
                sSql += "   AND a.savenm = :savenm"
                sSql += "   AND a.testcd = b.testcd"
                sSql += "   AND a.spccd  = b.spccd"
                sSql += "   AND b.usdt  <= fn_ack_sysdate"
                sSql += "   AND b.uedt  >  fn_ack_sysdate"
                sSql += " ORDER BY dispseq"

            End If

            oledbcmd.Connection = oledbcn
            oledbcmd.CommandType = CommandType.Text
            oledbcmd.CommandText = sSql

            oledbda = New oracleDataAdapter(oledbcmd)

            With oledbda
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("formid", OracleDbType.Varchar2).Value = rsFormID
                .SelectCommand.Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrID
                If rsItemGbn <> "" Then .SelectCommand.Parameters.Add("itemgbn", OracleDbType.Varchar2).Value = rsItemGbn
                .SelectCommand.Parameters.Add("savenm", OracleDbType.Varchar2).Value = rsSaveNm
            End With

            dt.Reset()
            oledbda.Fill(dt)

            Return dt

        Catch ex As Exception
            MsgBox("Error " & sFn & vbCrLf & Err.Description)

            Return New DataTable
        End Try
    End Function

    Public Shared Function fnExe_Reg_lf096m(ByVal rsFormId As String, ByVal rsUsrId As String, ByVal rsItemGbn As String, ByVal rsSaveNm As String, ByVal rsSpcGbn As String, ByVal rsTestCds As String) As Boolean

        Dim oleDbCn As OracleConnection = GetDbConnection()
        Dim oleDbTrans As OracleTransaction = oleDbCn.BeginTransaction()

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim OleDbCmd As New oracleCommand

            Dim sqlDoc As String = ""
            Dim intRet As Integer = 0

            With OleDbCmd
                .Connection = oleDbCn
                .Transaction = oleDbTrans
                .CommandType = CommandType.Text

                sqlDoc = ""
                sqlDoc += "INSERT INTO lf096h "
                sqlDoc += "SELECT fn_ack_sysdate, '" + rsUsrId + "', :modip, a.* "
                sqlDoc += "  FROM lf096m a"
                sqlDoc += " WHERE formid  = :formid"
                sqlDoc += "   AND usrid   = :usrid"
                sqlDoc += "   AND itemgbn = :itemgbn"
                sqlDoc += "   AND savenm  = :savenm"

                .CommandText = sqlDoc

                .Parameters.Clear()
                .Parameters.Add("formid", OracleDbType.Varchar2).Value = rsFormId
                .Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrId
                .Parameters.Add("modip", OracleDbType.Varchar2).Value = COMMON.CommLogin.LOGIN.USER_INFO.LOCALIP
                .Parameters.Add("itemgbn", OracleDbType.Varchar2).Value = rsItemGbn
                .Parameters.Add("savenm", OracleDbType.Varchar2).Value = rsSaveNm.Trim

                .ExecuteNonQuery()

                .CommandText = "DELETE lf096m WHERE formid = :formid AND usrid = :usrid AND itemgbn = :itemgbn AND savenm = :savenm"

                .Parameters.Clear()
                .Parameters.Add("formid", OracleDbType.Varchar2).Value = rsFormId
                .Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrId
                .Parameters.Add("itemgbn", OracleDbType.Varchar2).Value = rsItemGbn
                .Parameters.Add("savenm", OracleDbType.Varchar2).Value = rsSaveNm.Trim

                .ExecuteNonQuery()

                Dim sBuf() As String = rsTestCds.Split(","c)

                For ix As Integer = 0 To sBuf.Length - 1
                    sqlDoc = ""
                    sqlDoc += "INSERT INTO lf096m(  formid,  usrid,  itemgbn,  savenm,  testcd,  spccd,  dispseq, regdt,           regid,  regip )"
                    sqlDoc += "            VALUES( :formid, :usrid, :itemgbn, :savenm, :testcd, :spccd, :dispseq, fn_ack_sysdate, :regid, :regip )"

                    .CommandText = sqlDoc

                    .Parameters.Clear()
                    .Parameters.Add("formid", OracleDbType.Varchar2).Value = rsFormId
                    .Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrId
                    .Parameters.Add("itemgbn", OracleDbType.Varchar2).Value = rsItemGbn
                    .Parameters.Add("savenm", OracleDbType.Varchar2).Value = rsSaveNm.Trim

                    If rsSpcGbn = "NONE" Then
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = sBuf(ix)
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = "NONE"
                    Else
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = sBuf(ix).Substring(0, 7).Trim
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = sBuf(ix).Substring(8).Trim
                    End If
                    .Parameters.Add("dispseq", OracleDbType.Int32).Value = (ix + 1).ToString
                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                    .Parameters.Add("regip", OracleDbType.Varchar2).Value = COMMON.CommLogin.LOGIN.USER_INFO.LOCALIP

                    intRet = .ExecuteNonQuery()

                Next
            End With

            If intRet = 0 Then
                oleDbTrans.Rollback()
            Else
                oleDbTrans.Commit()
            End If

            Return True

        Catch ex As Exception

            oleDbTrans.Rollback()
            MsgBox(ex.Message)
            Return False
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Shared Function fnExe_Del_lf096m(ByVal rsFormId As String, ByVal rsUsrId As String, ByVal rsItemGbn As String, ByVal rsSaveNm As String) As Boolean

        Dim oleDbCn As OracleConnection = GetDbConnection()
        Dim oleDbTrans As oracleTransaction = oleDbCn.BeginTransaction()

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim OleDbCmd As New oracleCommand

            Dim sqlDoc As String = ""
            Dim intRet As Integer = 0

            With OleDbCmd
                .Connection = oleDbCn
                .Transaction = oleDbTrans
                .CommandType = CommandType.Text
                ' <20121015 사용자 아이디별로 삭제 말고 전체삭제 가능하도록
                .CommandText = "DELETE lf096m WHERE formid = :formid AND itemgbn = :itemgbn AND savenm = :savenm"


                .Parameters.Clear()
                .Parameters.Add("formid", OracleDbType.Varchar2).Value = rsFormId
                .Parameters.Add("itemgbn", OracleDbType.Varchar2).Value = rsItemGbn
                .Parameters.Add("savenm", OracleDbType.Varchar2).Value = rsSaveNm.Trim

                intRet = .ExecuteNonQuery()

            End With

            If intRet = 0 Then
                oleDbTrans.Rollback()
            Else
                oleDbTrans.Commit()
            End If

            Return True

        Catch ex As Exception

            oleDbTrans.Rollback()
            MsgBox(ex.Message)
            Return False
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Public Shared Function fnGet_SlipInfo(ByVal mbMicroBioYn As Boolean, ByVal mbbloodbankYn As Boolean, Optional ByVal rsAllGbn As Boolean = False) As DataTable
        Dim oledbcn As oracleConnection = GetDbConnection()
        Dim oledbda As oracleDataAdapter
        Dim oledbcmd As New oracleCommand

        Dim dt As New DataTable
        Dim sSql As String = ""

        Try
            sSql = ""
            sSql += "SELECT DISTINCT partcd || slipcd slipcd, slipnmd, NVL(dispseq, 999) dispseq"
            sSql += "  FROM lf021m"
            sSql += " WHERE usdt <= fn_ack_sysdate"
            sSql += "   AND uedt >  fn_ack_sysdate"
            If mbMicroBioYn Then
                sSql += "   AND partcd IN (SELECT partcd FROM lf020m WHERE partgbn = '2' AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
            ElseIf mbbloodbankYn Then
                sSql += "   AND partcd IN (SELECT partcd FROM lf020m WHERE partgbn = '3' AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
            ElseIf rsAllGbn = False Then
                sSql += "   AND partcd NOT IN (SELECT partcd FROM lf020m WHERE partgbn IN ('2', '3') AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"
            End If

            sSql += " ORDER BY 3, 1"

            oledbcmd.Connection = oledbcn
            oledbcmd.CommandType = CommandType.Text
            oledbcmd.CommandText = sSql

            oledbda = New oracleDataAdapter(oledbcmd)

            dt.Reset()
            oledbda.Fill(dt)

            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return New DataTable

        End Try

    End Function


    Public Shared Function fnGet_Slip_TestList(ByVal rsSlipCd As String, ByVal rsSpcGbn As String) As DataTable

        Dim oledbCn As oracleConnection = GetDbConnection()
        Dim oledbDa As oracleDataAdapter
        Dim oledbCmd As New oracleCommand

        Dim dt As New DataTable
        Dim sSql As String = ""

        Try
            sSql = ""

            If rsSpcGbn = "NONE" Then
                sSql += "SELECT testcd, MIN(tnmd) tnmd, max(dispseql) dispseq"
            Else
                sSql += "SELECT RPAD(testcd, 7, ' ') || ' ' || spccd testcd, MIN(tnmd) tnmd, max(dispseql) dispseq"
            End If
            sSql += "  FROM lf060m"
            sSql += " WHERE partcd = :partcd"
            sSql += "   AND slipcd = :slipcd"
            sSql += "   AND usdt  <= fn_ack_sysdate"
            sSql += "   AND uedt  >  fn_ack_sysdate"
            sSql += "   AND tcdgbn IN ('B', 'S', 'P', 'C')"

            If rsSpcGbn = "NONE" Then
                sSql += " GROUP BY testcd"
            Else
                sSql += " GROUP BY testcd, spccd"
            End If

            sSql += " ORDER BY 3, 1"

            oledbCmd.Connection = oledbCn
            oledbCmd.CommandType = CommandType.Text
            oledbCmd.CommandText = sSql

            oledbDa = New oracleDataAdapter(oledbCmd)

            With oledbDa
                .SelectCommand.Parameters.Clear()
                .SelectCommand.Parameters.Add("partcd", OracleDbType.Varchar2).Value = rsSlipCd.Substring(0, 1)
                .SelectCommand.Parameters.Add("slipcd", OracleDbType.Varchar2).Value = rsSlipCd.Substring(1, 1)
            End With

            dt.Reset()
            oledbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Return New DataTable
        End Try

    End Function

    
End Class



