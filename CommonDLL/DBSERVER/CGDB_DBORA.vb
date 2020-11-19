Imports System.IO
Imports System.Data.OracleClient
Imports COMMON.CommFN

Public Class DbORA
    Private Const msFile As String = "File : CGDB_DbORA.vb, Class : DbORA" & vbTab

    Private Shared msDbConnStr As String = ""
    Private Shared m_DbCn As OracleConnection
    Private Shared m_DbCmd As OracleCommand
    Private Shared m_DbTran As OracleTransaction

    Protected Shared p_DbCn As OracleConnection
    Protected Shared p_DbCmd As OracleCommand
    Protected Shared p_DbTran As OracleTransaction

End Class
