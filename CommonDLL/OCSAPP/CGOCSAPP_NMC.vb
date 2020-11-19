Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Namespace OcsLink

    Public Class NMC
        Private Const msFile As String = "File : CGDA_OCS2.vb, Class : SData2@OcsLink" & vbTab

        Public Shared Function fnGet_SujinInfo(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Sujin_Info(String) As DataTable"

            Try
                Dim al As New ArrayList
                Dim sSql As String = "pkg_ack_ocs.pkg_get_sujin_regno"

                al.Add(New OracleParameter("rs_regno", rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_OrdInfo(ByVal rsRegNo As String, ByVal rsOrdDt As String, ByVal rsIogbn As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_OrdInfo(String, string, string) As DataTable"

            Try
                Dim al As New ArrayList
                Dim sSql As String = "pkg_ack_ocs.pkg_get_sujin_ordinfo"

                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_orddt", rsOrdDt))
                al.Add(New OracleParameter("rs_iotype", rsIogbn))

                DbCommand()
                Return DbExecuteQuery(sSql, al, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_OrdDateInfo(ByVal rsRegNo As String, ByVal rsOrdDt As String, ByVal rsCretNo As String, ByVal rsIoGbn As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_OrdDateInfo(String, string, string) As DataTable"

            Try
                Dim al As New ArrayList
                Dim sSql As String = "pkg_ack_ocs.pkg_get_sujin_orddt"


                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_orddt", rsOrdDt))
                al.Add(New OracleParameter("rs_cretno", rsCretNo))
                al.Add(New OracleParameter("rs_iotype", rsIoGbn))

                DbCommand()
                Return DbExecuteQuery(sSql, al, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_PastOpInfo(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_PastOpInfo(String) As DataTable"

            Try
                Dim al As New ArrayList
                Dim sSql As String = "pkg_ack_ocs.pkg_get_op_regno"

                al.Add(New OracleParameter("rs_regno", rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al, False)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_SetRstInfo(ByVal rsRegNo As String, ByVal rsOrdDt As String, ByVal rsOrdNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_SetRstInfo(String, String, String) As DataTable"

            Try
                Dim al As New ArrayList
                Dim sSql As String = "pkg_ack_ocs.pkg_get_sujin_rst"

                al.Add(New OracleParameter("rs_regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("rs_orddt", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))
                al.Add(New OracleParameter("rs_ordseqno", OracleDbType.Int64, rsOrdNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

    End Class
End Namespace

